/*
 * MIT License
 * 
 * Copyright (c) 2020 plexdata.de
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

using Plexdata.ModelGenerator.Defines;
using Plexdata.ModelGenerator.Extensions;
using Plexdata.ModelGenerator.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Plexdata.ModelGenerator.Models
{
    internal abstract class Code : ICode
    {
        #region Private Fields 

        private List<String> lines = null;

        #endregion

        #region Construction

        protected Code()
            : base()
        {
            this.Filename = String.Empty;
            this.Location = String.Empty;
            this.Lines = null;
        }

        #endregion

        #region Public Properties

        public String Filename { get; private set; }

        public String Location { get; private set; }

        public IEnumerable<String> Lines
        {
            get
            {
                return this.lines;
            }
            private set
            {
                if (value == null)
                {
                    value = new List<String>();
                }

                this.lines = new List<String>(value);
            }
        }

        public String Content
        {
            get
            {
                return String.Join(Environment.NewLine, this.lines);
            }
        }

        #endregion

        #region Public Methods

        public static Code FromClassItem(Class source, GeneratorSettings settings)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            Code.ValidateSettings(settings);

            Code result = Code.CreateInstance(settings);

            List<String> lines = new List<String>();

            result.Filename = result.GetFilename(source.Name);
            result.AddHeader(lines, settings);
            result.AddImports(source, lines, settings);

            result.AddNamespaceOpen(lines, settings);

            result.AddClassOpen(source, lines, settings);

            result.AddProperties(source, lines, settings);

            result.AddClassClose(lines);

            result.AddNamespaceClose(lines);

            result.Lines = lines;

            return result;
        }

        public static Code FromClassList(IEnumerable<Class> sources, GeneratorSettings settings)
        {
            if (sources == null || !sources.Any())
            {
                throw new ArgumentNullException(nameof(sources));
            }

            Code.ValidateSettings(settings);

            Code result = Code.CreateInstance(settings);

            List<String> lines = new List<String>();

            result.Filename = result.GetFilename(settings.RootClass);
            result.AddHeader(lines, settings);

            List<String> imports = new List<String>();

            foreach (Class source in sources)
            {
                result.AddImports(source, imports, settings);
            }

            imports = imports
                .Where(x => !String.IsNullOrWhiteSpace(x))
                .Distinct()
                .OrderBy(x => x, new ImportsComparer(settings.TargetType))
                .ToList();

            imports.Add(String.Empty);

            lines.AddRange(imports);

            result.AddNamespaceOpen(lines, settings);

            foreach (Class source in sources)
            {
                result.AddClassOpen(source, lines, settings);
                result.AddProperties(source, lines, settings);
                result.AddClassClose(lines);
                lines.Add(String.Empty);
            }

            lines.RemoveAt(lines.Count - 1);

            result.AddNamespaceClose(lines);

            result.Lines = lines;

            return result;
        }

        public void Save(String folder, Boolean overwrite)
        {
            if (String.IsNullOrWhiteSpace(folder))
            {
                throw new ArgumentOutOfRangeException(nameof(folder), "Destination folder must not be null, empty, or contain only whitespaces.");
            }

            this.EnsureFolderExistence(folder);

            String filename = this.EnsureUniqueFilename(folder, this.Filename, overwrite);

            this.Location = Path.Combine(folder, filename);

            File.WriteAllText(this.Location, this.Content);
        }

        #endregion

        #region Protected Properties

        protected abstract String ArrayTypeFormat { get; }

        protected abstract String XmlElementNameFormat { get; }

        protected abstract String XmlNamespaceFormat { get; }

        #endregion

        #region Protected Methods

        protected abstract String GetFilename(String name);

        protected abstract void AddHeader(IList<String> lines, GeneratorSettings settings);

        protected abstract void AddImports(Class source, IList<String> lines, GeneratorSettings settings);

        protected abstract void AddNamespaceOpen(IList<String> lines, GeneratorSettings settings);

        protected abstract void AddClassOpen(Class source, IList<String> lines, GeneratorSettings settings);

        protected abstract void AddProperties(Class source, IList<String> lines, GeneratorSettings settings);

        protected abstract void AddClassClose(IList<String> lines);

        protected abstract void AddNamespaceClose(IList<String> lines);

        protected void AddHeader(String comment, IList<String> lines, GeneratorSettings settings)
        {
            lines.Add($"{comment} <auto-generated>");
            lines.Add($"{comment}");
            lines.Add($"{comment} This code was generated by a tool.");
            lines.Add($"{comment}");
            lines.Add($"{comment} Generator: {settings.ProductName}");
            lines.Add($"{comment} Version:   {settings.ProductVersion}.");
            lines.Add($"{comment} Timestamp: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");

            if (settings.HasPackageName)
            {
                lines.Add($"{comment}");
                lines.Add($"{comment} NuGet package \"{settings.PackageName}\" should ");
                lines.Add($"{comment} be installed to be able to use this code.");
            }

            lines.Add($"{comment}");
            lines.Add($"{comment} </auto-generated>");

            lines.Add(String.Empty);
        }

        protected String GetIndent(Int32 depth)
        {
            if (depth < 1)
            {
                return String.Empty;
            }

            return String.Empty.PadLeft(depth, '\t');
        }

        protected String GetPropertyName(Class parent, Property property)
        {
            const String suffix = "Value";

            if (parent is null)
            {
                throw new ArgumentNullException(nameof(parent));
            }

            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            if (property.Name == parent.Name)
            {
                return $"{property.Name}{suffix}";
            }

            if (property.IsOrigin)
            {
                if (property.Origin.StartsWith("@", StringComparison.InvariantCultureIgnoreCase))
                {
                    return property.Name;
                }

                if (property.Origin == "#text")
                {
                    return $"{suffix}";
                }
                else
                {
                    return $"{property.Origin}";
                }
            }
            else
            {
                return $"{property.Name}";
            }
        }

        protected String GetTypeDescriptor(Property property)
        {
            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            Entity source = property.Entity;

            String result = source.Type.Name;

            if (source.IsClass)
            {
                result = $"{source.Name}";
            }
            else if (source.IsArray)
            {
                List<String> entities = source.Children.Select(x => x.Name).Distinct().ToList();

                if (entities.Count != 1)
                {
                    throw new InvalidProgramException("More than one type found in child array");
                }

                Entity entity = source.Children.First();
                String label = String.Empty;

                if (entity.IsClass)
                {
                    label = entity.Name;
                }
                else
                {
                    label = entity.Type.Name;
                }

                result = String.Format(this.ArrayTypeFormat, label);
            }

            return result;
        }

        protected Boolean TryGetAttribute(GeneratorSettings settings, Class source, out String attribute)
        {
            attribute = String.Empty;

            if (settings is null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (settings.SourceType == SourceType.Xml)
            {
                if (source.IsClass)
                {
                    if (source.Entity.HasXmlNamespace)
                    {
                        attribute = String.Format("XmlRoot({0}, {1})",
                            String.Format(this.XmlElementNameFormat, source.ClassName),
                            String.Format(this.XmlNamespaceFormat, source.Entity.XmlNamespace));

                    }
                    else
                    {
                        attribute = String.Format("XmlRoot({0})", (source.ClassName));
                    }

                    return true;
                }
            }

            return false;
        }

        protected Boolean TryGetAttribute(GeneratorSettings settings, Property source, out String attribute)
        {
            attribute = String.Empty;

            if (settings is null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (settings.SourceType == SourceType.Json)
            {
                switch (settings.AttributeType)
                {
                    case AttributeType.Newtonsoft:
                        attribute = $"JsonProperty(\"{source.AttributeName}\")";
                        return true;
                    case AttributeType.Microsoft:
                        attribute = $"JsonPropertyName(\"{source.AttributeName}\")";
                        return true;
                    default:
                        return false;
                }
            }
            else if (settings.SourceType == SourceType.Xml)
            {
                if (source.Origin.StartsWith("@", StringComparison.InvariantCultureIgnoreCase))
                {
                    attribute = $"XmlAttribute(\"{source.Origin.TrimStart('@')}\")";
                    return true;
                }

                if (source.Origin.Equals("#text", StringComparison.InvariantCultureIgnoreCase))
                {
                    // Must be XmlText no matter what the real type is!
                    attribute = "XmlText";
                    return true;
                }

                attribute = $"XmlElement(\"{source.AttributeName}\")";
                return true;
            }

            return false;
        }

        #endregion

        #region Private Methods

        private static void ValidateSettings(GeneratorSettings settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            if (!settings.IsValid)
            {
                throw new InvalidOperationException("Generator settings are not considered as valid.");
            }

            if (settings.TargetType != TargetType.CSharp && settings.TargetType != TargetType.VisualBasic)
            {
                throw new NotSupportedException($"Target type of {settings.TargetType} is not supported");
            }
        }

        private static Code CreateInstance(GeneratorSettings settings)
        {
            if (settings.TargetType == TargetType.CSharp)
            {
                return new CodeCs();
            }

            if (settings.TargetType == TargetType.VisualBasic)
            {
                return new CodeVb();
            }

            throw new InvalidOperationException($"Type of {settings.TargetType} is not valid.");
        }

        #endregion

        #region Helper Classes

        private class ImportsComparer : IComparer<String>
        {
            private readonly TargetType target;

            public ImportsComparer(TargetType target)
                : base()
            {
                this.target = target;
            }

            public Int32 Compare(String x, String y)
            {
                if (x == null && y == null)
                {
                    return 0;
                }

                if (x == null && y != null)
                {
                    return -1;
                }

                if (x != null && y == null)
                {
                    return 1;
                }

                if (this.target == TargetType.CSharp)
                {
                    x = x.Trim().TrimEnd(';');
                    y = y.Trim().TrimEnd(';');
                }

                return String.Compare(x, y);
            }
        }

        #endregion
    }
}
