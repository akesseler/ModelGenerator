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

using System;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace Plexdata.ModelGenerator.Gui.Models
{
    [XmlType("replacement")]
    [DebuggerDisplay("{GetDebuggerDisplay(),nq}")]
    public class Replacement : ICloneable
    {
        #region Private Fields 

        private String source = String.Empty;
        private String target = String.Empty;

        #endregion

        #region Construction

        public Replacement()
            : this(null, null)
        {
        }

        public Replacement(String source, String target)
        {
            this.Source = source;
            this.Target = target;
        }

        private Replacement(Replacement other)
        {
            this.Source = other.Source;
            this.Target = other.Target;
        }

        #endregion

        #region Public Properties

        [XmlAttribute("source")]
        public String Source
        {
            get
            {
                return this.source;
            }
            set
            {
                if (value is null)
                {
                    value = String.Empty;
                }

                if (this.source != value)
                {
                    this.source = value;
                }
            }
        }

        [XmlAttribute("target")]
        public String Target
        {
            get
            {
                return this.target;
            }
            set
            {
                if (value is null)
                {
                    value = String.Empty;
                }

                if (this.target != value)
                {
                    this.target = value;
                }
            }
        }

        #endregion

        #region Public Methods

        public Object Clone()
        {
            return new Replacement(this);
        }

        #endregion

        #region Private Methods

        private String GetDebuggerDisplay()
        {
            return $"{nameof(this.Source)}: '{this.Source ?? "null"}'; {nameof(this.Target)}: '{this.Target ?? "null"}'";
        }

        #endregion
    }
}
