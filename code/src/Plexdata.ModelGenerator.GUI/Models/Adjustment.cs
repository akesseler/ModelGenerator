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
using System.Diagnostics;
using System.Xml.Serialization;

namespace Plexdata.ModelGenerator.Gui.Models
{
    [XmlType("adjustment")]
    [DebuggerDisplay("{GetDebuggerDisplay(),nq}")]
    public class Adjustment : ICloneable
    {
        #region Private Fields 

        private Boolean ignore = true;
        private Int32 casing = (Int32)CasingType.Unmodified;
        private String prefix = String.Empty;
        private String suffix = String.Empty;

        #endregion

        #region Construction

        public Adjustment()
            : this(true, -1, null, null)
        {
        }

        public Adjustment(Boolean ignore, Int32 casing, String prefix, String suffix)
        {
            this.Ignore = ignore;
            this.Casing = casing;
            this.Prefix = prefix;
            this.Suffix = suffix;
        }

        private Adjustment(Adjustment other)
        {
            this.Ignore = other.Ignore;
            this.Casing = other.Casing;
            this.Prefix = other.Prefix;
            this.Suffix = other.Suffix;
        }

        #endregion

        #region Public Properties

        [XmlAttribute("ignore")]
        public Boolean Ignore
        {
            get
            {
                return this.ignore;
            }
            set
            {
                this.ignore = value;
            }
        }

        [XmlAttribute("casing")]
        public Int32 Casing
        {
            get
            {
                return this.casing;
            }
            set
            {
                if (!Enum.IsDefined(typeof(CasingType), value))
                {
                    value = (Int32)Defines.CasingType.Unmodified;
                }

                this.casing = value;
            }
        }

        [XmlElement("prefix")]
        public String Prefix
        {
            get
            {
                return this.prefix;
            }
            set
            {
                if (value is null)
                {
                    value = String.Empty;
                }

                if (this.prefix != value)
                {
                    this.prefix = value;
                }
            }
        }

        [XmlElement("suffix")]
        public String Suffix
        {
            get
            {
                return this.suffix;
            }
            set
            {
                if (value is null)
                {
                    value = String.Empty;
                }

                if (this.suffix != value)
                {
                    this.suffix = value;
                }
            }
        }

        #endregion

        #region Public Methods

        public Object Clone()
        {
            return new Adjustment(this);
        }

        #endregion

        #region Private Methods

        private String GetDebuggerDisplay()
        {
            return
                $"{nameof(this.Ignore)}: '{this.Ignore}'; {nameof(this.Casing)}: '{this.Casing}'; " +
                $"{nameof(this.Prefix)}: '{this.Prefix}'; {nameof(this.Suffix)}: '{this.Suffix}'";
        }

        #endregion
    }
}
