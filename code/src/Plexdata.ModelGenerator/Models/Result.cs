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
using System.Collections.Generic;
using System.Text;

namespace Plexdata.ModelGenerator.Models
{
    internal class Result
    {
        #region Private Fields 

        private readonly Entity entity = null;

        #endregion 

        #region Construction

        protected Result(Entity entity)
            : base()
        {
            this.entity = entity ?? throw new ArgumentNullException(nameof(entity));
        }

        #endregion

        #region Public Properties

        public String SourceName
        {
            get
            {
                return this.entity.SourceName;
            }
        }

        public String ObjectName
        {
            get
            {
                return this.entity.ObjectName;
            }
        }

        public String MemberName
        {
            get
            {
                return this.entity.MemberName;
            }
        }

        public Type MemberType
        {
            get
            {
                return this.entity.MemberType;
            }
        }

        public virtual String Comment
        {
            get
            {
                return this.entity.Comment;
            }
        }

        public String XmlNamespace
        {
            get
            {
                return this.entity.XmlNamespace;
            }
        }

        public virtual Boolean IsArray
        {
            get
            {
                return this.entity.IsArray;
            }
        }

        public virtual Boolean IsClass
        {
            get
            {
                return this.entity.IsClass;
            }
        }

        public virtual Boolean IsComment
        {
            get
            {
                return this.entity.IsComment;
            }
        }

        public Boolean IsXmlNamespace
        {
            get
            {
                return this.entity.IsXmlNamespace;
            }
        }

        #endregion

        #region Protected Properties

        protected IEnumerable<Entity> Children
        {
            get
            {
                return this.entity.Children;
            }
        }

        #endregion

        #region Public Methods

        public override String ToString()
        {
            StringBuilder builder = new StringBuilder(256);

            builder.AppendFormat("{0}: {1}, ", nameof(this.SourceName), this.SourceName);
            builder.AppendFormat("{0}: {1}, ", nameof(this.ObjectName), this.ObjectName);
            builder.AppendFormat("{0}: {1}, ", nameof(this.MemberName), this.MemberName);
            builder.AppendFormat("{0}: {1}, ", nameof(this.MemberType), this.MemberType);
            builder.AppendFormat("{0}: {1}, ", nameof(this.IsClass), this.IsClass ? Boolean.TrueString : Boolean.FalseString);
            builder.AppendFormat("{0}: {1}, ", nameof(this.IsArray), this.IsArray ? Boolean.TrueString : Boolean.FalseString);
            builder.AppendFormat("{0}: {1}, ", nameof(this.IsComment), this.IsComment ? Boolean.TrueString : Boolean.FalseString);
            builder.AppendFormat("{0}: {1}, ", nameof(this.IsXmlNamespace), this.IsXmlNamespace ? Boolean.TrueString : Boolean.FalseString);

            return builder.ToString().TrimEnd(' ', ',');
        }

        #endregion
    }
}
