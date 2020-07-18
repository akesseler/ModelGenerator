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

using System;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Plexdata.ModelGenerator.Gui.Models
{
    [XmlType("dimension")]
    [DebuggerDisplay("Width = {this.Width}, Height = {this.Height}")]
    public class Dimension
    {
        #region Private Fields 

        private Int32 width = 0;

        private Int32 height = 0;

        #endregion

        #region Construction

        public Dimension()
            : this(0, 0)
        {
        }

        public Dimension(Int32 width, Int32 height)
            : base()
        {
            this.Width = width;
            this.Height = height;
        }

        #endregion

        #region Public Properties

        [XmlAttribute("width")]
        public Int32 Width
        {
            get
            {
                return this.width;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException($"{this.Width} must be greater than zero,", nameof(value));
                }

                if (this.width != value)
                {
                    this.width = value;
                }
            }
        }

        [XmlAttribute("height")]
        public Int32 Height
        {
            get
            {
                return this.height;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException($"{this.Height} must be greater than zero,", nameof(value));
                }

                if (this.height != value)
                {
                    this.height = value;
                }
            }
        }

        #endregion
    }
}
