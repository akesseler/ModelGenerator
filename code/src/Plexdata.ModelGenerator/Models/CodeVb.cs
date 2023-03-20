/*
 * MIT License
 * 
 * Copyright (c) 2023 plexdata.de
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
using System;
using System.Collections.Generic;
using System.Linq;

namespace Plexdata.ModelGenerator.Models
{
    internal sealed class CodeVb : Code
    {
        #region Construction

        public CodeVb()
            : base()
        {
        }

        #endregion

        #region Protected Properties

        protected override String ArrayTypeFormat
        {
            get
            {
                return "List(Of {0})";
            }
        }

        protected override String XmlElementNameFormat
        {
            get
            {
                return "ElementName:=\"{0}\"";
            }
        }

        protected override String XmlNamespaceFormat
        {
            get
            {
                return "[Namespace]:=\"{0}\"";
            }
        }

        #endregion

        #region Protected Methods

        protected override String GetFilename(String name)
        {
            String filename = name;
            String extension = ".vb";

            if (!filename.EndsWith(extension, StringComparison.InvariantCultureIgnoreCase))
            {
                filename = $"{filename}{extension}";
            }

            return filename;
        }

        protected override void AddHeader(IList<String> lines, GeneratorSettings settings)
        {
            base.AddHeader("'", lines, settings);
        }

        protected override void AddImports(Class source, IList<String> lines, GeneratorSettings settings)
        {
            List<String> items = source.Namespaces.ToList();

            if (settings.SourceType == SourceType.Json && source.Properties.Any(x => x.IsOrigin))
            {
                items.AddRange(settings.PackageNamespaces);
            }

            if (settings.SourceType == SourceType.Xml)
            {
                items.AddRange(settings.PackageNamespaces);
            }

            items.Sort();

            foreach (String item in items)
            {
                lines.Add($"Imports {item}");
            }

            lines.Add(String.Empty);
        }

        protected override void AddNamespaceOpen(IList<String> lines, GeneratorSettings settings)
        {
            lines.Add($"Namespace {settings.Namespace}");
            lines.Add(String.Empty);
        }

        protected override void AddClassOpen(Class source, IList<String> lines, GeneratorSettings settings)
        {
            String indent = base.GetIndent(1);

            if (base.TryGetAttribute(settings, source, out String attribute))
            {
                lines.Add($"{indent}<{attribute}>");
            }

            lines.Add($"{indent}Public Class {source.Name}");
            lines.Add(String.Empty);
        }

        protected override void AddProperties(Class source, IList<String> lines, GeneratorSettings settings)
        {
            String indent = base.GetIndent(2);

            // {{0}} => Type
            // {{1}} => Name

            String format = String.Empty;

            if (settings.MemberType == MemberType.Property)
            {
                format = $"{indent}Public Property {{1}} As {{0}}";
            }

            if (settings.MemberType == MemberType.Field)
            {
                format = $"{indent}Public {{1}} As {{0}}";
            }

            foreach (Property property in source.Properties)
            {
                if (property.IsComment)
                {
                    lines.Add($"{indent}' {property.Comment}");
                }

                if (base.TryGetAttribute(settings, property, out String attribute))
                {
                    lines.Add($"{indent}<{attribute}>");
                }

                lines.Add(String.Format(format, base.GetTypeDescriptor(property), base.GetPropertyName(source, property)));

                lines.Add(String.Empty);
            }

            lines.RemoveAt(lines.Count - 1);
        }

        protected override void AddClassClose(IList<String> lines)
        {
            lines.Add(String.Empty);
            lines.Add($"{base.GetIndent(1)}End Class");
        }

        protected override void AddNamespaceClose(IList<String> lines)
        {
            lines.Add(String.Empty);
            lines.Add("End Namespace");
        }

        #endregion
    }
}
