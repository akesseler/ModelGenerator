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
using System.Text;

namespace Plexdata.ModelGenerator.Models
{
    internal class Result
    {
        #region Construction

        protected Result(Entity entity)
            : base()
        {
            this.Entity = entity ?? throw new ArgumentNullException(nameof(entity));
        }

        #endregion

        #region Public Properties

        public Entity Entity { get; } = null;

        public virtual String Name
        {
            get
            {
                return this.Entity.Name;
            }
        }

        public virtual Boolean IsArray
        {
            get
            {
                return this.Entity.IsArray;
            }
        }

        public virtual Boolean IsClass
        {
            get
            {
                return this.Entity.IsClass;
            }
        }

        public virtual Boolean IsOrigin
        {
            get
            {
                return this.Entity.IsOrigin;
            }
        }

        public virtual String Origin
        {
            get
            {
                return this.Entity.Origin;
            }
        }

        public virtual Boolean IsComment
        {
            get
            {
                return !String.IsNullOrWhiteSpace(this.Comment);
            }
        }

        public virtual String Comment
        {
            get
            {
                return this.Entity.Comment;
            }
        }

        #endregion

        #region Public Methods

        public override String ToString()
        {
            StringBuilder builder = new StringBuilder(128);

            builder.AppendFormat("{0}: {1}, ", nameof(this.Name), this.Name);
            builder.AppendFormat("{0}: {1}, ", nameof(this.Entity.Type), this.Entity.Type);

            if (this.IsOrigin)
            {
                builder.AppendFormat("{0}: {1}, ", nameof(this.Origin), this.Origin);
            }

            if (this.IsClass)
            {
                builder.AppendFormat("{0}: {1}, ", nameof(this.IsClass), Boolean.TrueString);
            }

            if (this.IsArray)
            {
                builder.AppendFormat("{0}: {1}, ", nameof(this.IsArray), Boolean.TrueString);
            }

            return builder.ToString().TrimEnd(' ', ',');
        }

        #endregion
    }
}
