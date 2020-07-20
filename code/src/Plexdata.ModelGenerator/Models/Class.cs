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
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plexdata.ModelGenerator.Models
{
    internal class Class : Result
    {
        #region Private Fields 

        private List<Property> properties = null;

        #endregion

        #region Construction

        public Class(Entity source)
            : base(source)
        {
            if (!base.Entity.IsClass)
            {
                throw new InvalidCastException($"Entity {base.Entity.Name} could not be considered as class.");
            }
        }

        #endregion

        #region Public Properties

        public String ClassName
        {
            get
            {
                return base.IsOrigin ? base.Origin : base.Name;
            }
        }

        public IEnumerable<String> Namespaces
        {
            get
            {
                List<String> namespaces = base.Entity.Children.Select(x => x.Namespace).ToList();

                if (base.Entity.Children.Any(x => x.IsArray))
                {
                    namespaces.Add(typeof(List<Object>).Namespace);
                }

                return namespaces.Distinct();
            }
        }

        public IEnumerable<Property> Properties
        {
            get
            {
                if (this.properties == null || this.properties.Count != base.Entity.Children.Count())
                {
                    this.properties = new List<Property>();

                    foreach (Entity entity in base.Entity.Children)
                    {
                        this.properties.Add(new Property(entity));
                    }
                }

                return this.properties;
            }
        }

        #endregion

        #region Public Methods

        public override String ToString()
        {
            StringBuilder builder = new StringBuilder(base.ToString());

            if (builder.Length > 0) { builder.Append(", "); }

            builder.AppendFormat("{0}: {1}, ", nameof(this.ClassName), this.ClassName);

            builder.AppendFormat("{0}: {1}, ", nameof(this.Properties), this.properties?.Count ?? 0);

            return builder.ToString().TrimEnd(' ', ',');
        }

        #endregion
    }
}
