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

using Plexdata.ModelGenerator.Extensions;
using System;
using System.Text;

namespace Plexdata.ModelGenerator.Models
{
    internal class Property : Result
    {
        #region Construction

        public Property(Entity source)
            : base(source)
        {
        }

        #endregion

        #region Public Properties

        public String AttributeName
        {
            get
            {
                if (base.IsArray)
                {
                    return base.Name.ToSingular();
                }
                else if (base.IsOrigin)
                {
                    return base.Origin;
                }
                else
                {
                    return base.Name;
                }
            }
        }

        #endregion

        #region Public Methods

        public override String ToString()
        {
            StringBuilder builder = new StringBuilder(base.ToString());

            if (builder.Length > 0) { builder.Append(", "); }

            builder.AppendFormat("{0}: {1}, ", nameof(this.AttributeName), this.AttributeName);

            return builder.ToString().TrimEnd(' ', ',');
        }

        #endregion
    }
}
