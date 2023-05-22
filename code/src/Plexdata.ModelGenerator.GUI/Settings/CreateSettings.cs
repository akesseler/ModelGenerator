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

using Plexdata.ModelGenerator.Gui.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace Plexdata.ModelGenerator.Gui.Settings
{
    public class CreateSettings : ICloneable
    {
        #region Private Fields 

        private Adjustment adjustment = null;
        private Replacement[] jsonReplacements = null;
        private Replacement[] xmlReplacements = null;

        #endregion

        #region Construction

        public CreateSettings()
        {
            this.Adjustment = null;
            this.JsonReplacements = this.CreateJsonReplacementDefaults();
            this.XmlReplacements = this.CreateXmlReplacementDefaults();
        }

        private CreateSettings(CreateSettings other)
        {
            this.Adjustment = other.Adjustment;
            this.JsonReplacements = this.Clone(other.JsonReplacements);
            this.XmlReplacements = this.Clone(other.XmlReplacements);

        }

        #endregion

        #region Public Properties

        [XmlElement("adjustment")]
        public Adjustment Adjustment
        {
            get
            {
                return this.adjustment;
            }
            set
            {
                if (value is null)
                {
                    value = new Adjustment();
                }

                this.adjustment = value;
            }
        }

        [XmlArray("json-replacements")]
        public Replacement[] JsonReplacements
        {
            get
            {
                return this.jsonReplacements;
            }
            set
            {
                if (value is null)
                {
                    value = new Replacement[0];
                }

                this.jsonReplacements = value.Where(x => x != null).ToArray();
            }
        }

        [XmlArray("xml-replacements")]
        public Replacement[] XmlReplacements
        {
            get
            {
                return this.xmlReplacements;
            }
            set
            {
                if (value is null)
                {
                    value = new Replacement[0];
                }

                this.xmlReplacements = value.Where(x => x != null).ToArray();
            }
        }

        #endregion

        #region Private Methods

        public Object Clone()
        {
            return new CreateSettings(this);
        }

        #endregion

        #region Private Methods

        private Replacement[] CreateJsonReplacementDefaults()
        {
            return new Replacement[]
            {
                new Replacement("boolean", "\"false\""),
                new Replacement("string", "\"string\""),
                new Replacement("token", "\"token\""),
                new Replacement("integer", "0"),
                new Replacement("number", "0.0"),
                new Replacement("datetime", "\"\\/Date(0)\\/\""),
            };
        }

        private Replacement[] CreateXmlReplacementDefaults()
        {
            return new Replacement[]
            {
                new Replacement("Boolean", "False"),
                new Replacement("Integer", "0"),
                new Replacement("Float", "0.0"),
                new Replacement("Double", "0.0"),
                new Replacement("Date", "1970-01-01T00:00:00"),
                new Replacement("DateTime", "1970-01-01T00:00:00"),
            };
        }

        private Replacement[] Clone(IEnumerable<Replacement> source)
        {
            List<Replacement> result = new List<Replacement>();

            if (source?.Any() ?? false)
            {
                foreach (Replacement current in source)
                {
                    result.Add((Replacement)current.Clone());
                }
            }

            return result.ToArray();
        }

        #endregion
    }
}
