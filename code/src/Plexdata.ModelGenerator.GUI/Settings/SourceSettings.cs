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
using Plexdata.ModelGenerator.Models;
using System;
using System.Xml.Serialization;

namespace Plexdata.ModelGenerator.Gui.Settings
{
    public class SourceSettings
    {
        #region Private Fields 

        private String filename;

        private Boolean allInOne;

        private String rootClass;

        private String nameSpace;

        private Int32 sourceType;

        private Int32 targetType;

        private Int32 memberType;

        private Int32 attributeType;

        #endregion

        #region Construction

        public SourceSettings()
            : base()
        {
            GeneratorSettings defaults = new GeneratorSettings();

            this.AllInOne = defaults.IsAllInOne;
            this.RootClass = defaults.RootClass;
            this.Namespace = defaults.Namespace;
            this.SourceType = (Int32)defaults.SourceType;
            this.TargetType = (Int32)defaults.TargetType;
            this.MemberType = (Int32)defaults.MemberType;
            this.AttributeType = (Int32)defaults.AttributeType;
        }

        #endregion

        #region Public Properties

        [XmlElement("filename")]
        public String Filename
        {
            get
            {
                return (this.filename ?? String.Empty).Trim();
            }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    value = String.Empty;
                }

                this.filename = value.Trim();
            }
        }

        [XmlElement("all-in-one")]
        public Boolean AllInOne
        {
            get
            {
                return this.allInOne;
            }
            set
            {
                this.allInOne = value;
            }
        }

        [XmlElement("root-class")]
        public String RootClass
        {
            get
            {
                return (this.rootClass ?? String.Empty).Trim();
            }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    value = String.Empty;
                }

                this.rootClass = value.Trim();
            }
        }

        [XmlElement("namespace")]
        public String Namespace
        {
            get
            {
                return (this.nameSpace ?? String.Empty).Trim();
            }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    value = String.Empty;
                }

                this.nameSpace = value.Trim();
            }
        }

        [XmlElement("source-type")]
        public Int32 SourceType
        {
            get
            {
                return this.sourceType;
            }
            set
            {
                if (!Enum.IsDefined(typeof(SourceType), value))
                {
                    value = (Int32)Defines.SourceType.Unknown;
                }

                this.sourceType = value;
            }
        }

        [XmlElement("target-type")]
        public Int32 TargetType
        {
            get
            {
                return this.targetType;
            }
            set
            {
                if (!Enum.IsDefined(typeof(TargetType), value))
                {
                    value = (Int32)Defines.TargetType.Unknown;
                }

                this.targetType = value;
            }
        }

        [XmlElement("member-type")]
        public Int32 MemberType
        {
            get
            {
                return this.memberType;
            }
            set
            {
                if (!Enum.IsDefined(typeof(MemberType), value))
                {
                    value = (Int32)Defines.MemberType.Unknown;
                }

                this.memberType = value;
            }
        }

        [XmlElement("attribute-type")]
        public Int32 AttributeType
        {
            get
            {
                return this.attributeType;
            }
            set
            {
                if (!Enum.IsDefined(typeof(AttributeType), value))
                {
                    value = (Int32)Defines.AttributeType.Unknown;
                }

                this.attributeType = value;
            }
        }

        #endregion
    }
}
