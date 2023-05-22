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
using System.Xml.Serialization;

namespace Plexdata.ModelGenerator.Gui.Models
{
    [XmlType("location")]
    [DebuggerDisplay("{GetDebuggerDisplay(),nq}")]
    public class Location
    {
        #region Private Fields 

        private Int32 left = 0;
        private Int32 top = 0;

        #endregion

        #region Construction

        public Location()
            : this(0, 0)
        {
        }

        public Location(Int32 left, Int32 top)
            : base()
        {
            this.Left = left;
            this.Top = top;
        }

        #endregion

        #region Public Properties

        [XmlAttribute("left")]
        public Int32 Left
        {
            get
            {
                return this.left;
            }
            set
            {
                if (this.left != value)
                {
                    this.left = value;
                }
            }
        }

        [XmlAttribute("top")]
        public Int32 Top
        {
            get
            {
                return this.top;
            }
            set
            {
                if (this.top != value)
                {
                    this.top = value;
                }
            }
        }

        #endregion

        #region Private Methods

        private String GetDebuggerDisplay()
        {
            return $"{nameof(this.Left)}: '{this.Left}'; {nameof(this.Top)}: '{this.Top}'";
        }

        #endregion
    }
}
