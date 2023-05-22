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
using System.IO;
using System.Reflection;

namespace Plexdata.ModelGenerator.Models
{
    public class GeneratorSettings
    {
        #region Construction

        public GeneratorSettings()
            : base()
        {
            this.IsAllInOne = true;
            this.RootClass = "RootClass";
            this.Namespace = "RootNamespace";
            this.SourceType = SourceType.Json;
            this.TargetType = TargetType.CSharp;
            this.MemberType = MemberType.Property;
            this.AttributeType = AttributeType.Newtonsoft;
            this.Adjustment = new AdjustmentSettings();
        }

        #endregion

        #region Public Properties

        public Boolean IsValid
        {
            get
            {
                return
                    !String.IsNullOrWhiteSpace(this.RootClass) &&
                    !String.IsNullOrWhiteSpace(this.Namespace) &&
                    this.SourceType != SourceType.Unknown &&
                    this.TargetType != TargetType.Unknown &&
                    this.MemberType != MemberType.Unknown &&
                    this.AttributeType != AttributeType.Unknown;
            }
        }

        public Boolean IsAllInOne { get; set; }

        public String RootClass { get; set; }

        public String Namespace { get; set; }

        public SourceType SourceType { get; set; }

        public TargetType TargetType { get; set; }

        public MemberType MemberType { get; set; }

        public AttributeType AttributeType { get; set; }

        public AdjustmentSettings Adjustment { get; private set; }

        public String ProductName
        {
            get
            {
                Assembly assembly = Assembly.GetAssembly(typeof(GeneratorSettings));

                Object[] attributes = assembly.GetCustomAttributes(typeof(AssemblyProductAttribute), false);

                if (attributes != null && attributes.Length > 0 && attributes[0] is AssemblyProductAttribute attribute)
                {
                    return attribute.Product;
                }

                return Path.GetFileNameWithoutExtension(assembly.CodeBase);
            }
        }

        public String ProductVersion
        {
            get
            {
                Assembly assembly = Assembly.GetAssembly(typeof(GeneratorSettings));

                Object[] attributes = assembly.GetCustomAttributes(typeof(AssemblyFileVersionAttribute), true);

                if (attributes != null && attributes.Length > 0 && attributes[0] is AssemblyFileVersionAttribute attribute)
                {
                    return attribute.Version;
                }

                return "?.?.?";
            }
        }

        public String PackageName
        {
            get
            {
                if (this.SourceType != SourceType.Json)
                {
                    return String.Empty;
                }

                switch (this.AttributeType)
                {
                    case AttributeType.Newtonsoft:
                        return "Newtonsoft.Json";
                    case AttributeType.Microsoft:
                        return "System.Text.Json";
                    default:
                        return String.Empty;
                }
            }
        }

        public Boolean HasPackageName
        {
            get
            {
                return this.SourceType == SourceType.Json && (
                    this.AttributeType == AttributeType.Newtonsoft ||
                    this.AttributeType == AttributeType.Microsoft
                );
            }
        }

        public IEnumerable<String> PackageNamespaces
        {
            get
            {
                if (this.SourceType == SourceType.Json)
                {
                    if (this.AttributeType == AttributeType.Newtonsoft)
                    {
                        yield return "Newtonsoft.Json";
                    }
                    else if (this.AttributeType == AttributeType.Microsoft)
                    {
                        yield return "System.Text.Json.Serialization";
                    }
                    else
                    {
                        yield return "Newtonsoft.Json";
                        yield return "System.Text.Json.Serialization";
                    }
                }
                else if (this.SourceType == SourceType.Xml)
                {
                    yield return "System.Xml.Serialization";
                }
            }
        }

        #endregion
    }
}
