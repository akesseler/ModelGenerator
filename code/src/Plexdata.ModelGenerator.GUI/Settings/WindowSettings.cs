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

using Plexdata.ModelGenerator.Gui.Models;
using System;
using System.Xml.Serialization;

namespace Plexdata.ModelGenerator.Gui.Settings
{
    public class WindowSettings
    {
        #region Private Fields 

        private Location location;

        private Dimension dimension;

        #endregion

        #region Construction

        public WindowSettings()
            : this(0, 0, 0, 0)
        {
        }

        public WindowSettings(Int32 left, Int32 top, Int32 width, Int32 height)
            : base()
        {
            this.Location = new Location(left, top);
            this.Dimension = new Dimension(width, height);
        }

        #endregion

        #region Public Properties

        [XmlElement("location")]
        public Location Location
        {
            get
            {
                return this.location;
            }
            set
            {
                if (value is null)
                {
                    throw new ArgumentException($"{this.Location} must not be null,", nameof(value));
                }

                if (this.location != value)
                {
                    this.location = value;
                }
            }
        }

        [XmlElement("dimension")]
        public Dimension Dimension
        {
            get
            {
                return this.dimension;
            }
            set
            {
                if (value is null)
                {
                    throw new ArgumentException($"{this.Dimension} must not be null,", nameof(value));
                }

                if (this.dimension != value)
                {
                    this.dimension = value;
                }
            }
        }

        #endregion
    }
}
