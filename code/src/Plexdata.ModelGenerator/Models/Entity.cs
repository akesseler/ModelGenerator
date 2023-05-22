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
using System.Collections.Generic;
using System.Text;

namespace Plexdata.ModelGenerator.Models
{
    internal class Entity : ICloneable
    {
        #region Private Fields 

        private readonly List<Entity> children = new List<Entity>();

        #endregion

        #region Construction

        private Entity()
            : base()
        {
        }

        private Entity(String sourceName, Type memberType, String comment)
            : this()
        {
            this.SourceName = sourceName;
            this.ObjectName = String.Empty;
            this.MemberName = String.Empty;
            this.MemberType = memberType ?? typeof(Object);
            this.Comment = (comment ?? String.Empty).Trim();
            this.Parent = null;
            this.XmlNamespace = null;
        }

        private Entity(Entity other)
            : this()
        {
            this.SourceName = other.SourceName;
            this.ObjectName = other.ObjectName;
            this.MemberName = other.MemberName;
            this.MemberType = other.MemberType;
            this.Comment = other.Comment;
            this.Parent = other.Parent;
            this.XmlNamespace = other.XmlNamespace;

            foreach (Entity child in other.children)
            {
                this.AddChildEntity(child.Clone() as Entity);
            }
        }

        #endregion

        #region Public Properties

        public String SourceName
        {
            get;
            private set;
        }

        public String ObjectName
        {
            get;
            private set;
        }

        public String MemberName
        {
            get;
            private set;
        }

        public Type MemberType
        {
            get;
            private set;
        }

        public String Comment
        {
            get;
            private set;
        }

        public Entity Parent
        {
            get;
            private set;
        }

        public String XmlNamespace
        {
            get;
            private set;
        }

        public IEnumerable<Entity> Children
        {
            get
            {
                return this.children;
            }
        }

        public Boolean IsClass
        {
            get
            {
                return this.MemberType == typeof(Object) && this.children.Count > 0;
            }
        }

        public Boolean IsArray
        {
            get
            {
                return this.MemberType == typeof(Array) && this.children.Count > 0;
            }
        }

        public Boolean IsComment
        {
            get
            {
                return !String.IsNullOrWhiteSpace(this.Comment);
            }
        }

        public Boolean IsXmlNamespace
        {
            get
            {
                return !String.IsNullOrWhiteSpace(this.XmlNamespace);
            }
        }

        #endregion

        #region Public Methods

        public static Entity Create(AdjustmentSettings settings, String sourceName, Type memberType = null, String comment = null)
        {
            if (settings is null)
            {
                throw new ArgumentNullException(nameof(settings), $"Parameter '{nameof(settings)}' cannot be null.");
            }

            if (String.IsNullOrWhiteSpace(sourceName))
            {
                throw new ArgumentOutOfRangeException(nameof(sourceName), $"Parameter '{nameof(sourceName)}' cannot be null, empty or whitespace.");
            }

            Boolean plural = memberType == typeof(Array);

            return new Entity(sourceName, memberType, comment)
            {
                ObjectName = sourceName.CreateObjectName(settings),
                MemberName = sourceName.CreateMemberName(settings, plural),
            };
        }

        public void AddChildEntity(Entity childEntity)
        {
            if (childEntity is null)
            {
                return;
            }

            childEntity.Parent = this;

            this.children.Add(childEntity);
        }

        public void AddMemberNameIndex(Int32 index)
        {
            this.MemberName += index.ToString();
        }

        public void ReviseMemberType(Type memberType)
        {
            this.MemberType = memberType ?? typeof(Object);
        }

        public void ReviseComment(String comment)
        {
            this.Comment = (comment ?? String.Empty).Trim();
        }

        public void ReviseXmlNamespace(String xmlns)
        {
            if (this.IsClass)
            {
                this.XmlNamespace = xmlns ?? String.Empty;
            }

            foreach (Entity current in this.Children)
            {
                current.ReviseXmlNamespace(xmlns);
            }
        }

        public Object Clone()
        {
            return new Entity(this);
        }

        public override String ToString()
        {
            StringBuilder builder = new StringBuilder(256);

            builder.AppendFormat("{0}: {1}, ", nameof(this.SourceName), this.SourceName);
            builder.AppendFormat("{0}: {1}, ", nameof(this.ObjectName), this.ObjectName);
            builder.AppendFormat("{0}: {1}, ", nameof(this.MemberName), this.MemberName);
            builder.AppendFormat("{0}: {1}, ", nameof(this.MemberType), this.MemberType);

            if (this.IsClass)
            {
                builder.AppendFormat("{0}: {1}, ", nameof(this.IsClass), Boolean.TrueString);
            }

            if (this.IsArray)
            {
                builder.AppendFormat("{0}: {1}, ", nameof(this.IsArray), Boolean.TrueString);
            }

            if (this.IsComment)
            {
                builder.AppendFormat("{0}: {1}, ", nameof(this.IsComment), Boolean.TrueString);
            }

            if (this.IsXmlNamespace)
            {
                builder.AppendFormat("{0}: {1}, ", nameof(this.IsXmlNamespace), Boolean.TrueString);
            }

            if (this.children.Count > 0)
            {
                builder.AppendFormat("{0}.{1}: {2}, ", nameof(this.Children), nameof(this.children.Count), this.children.Count);
            }

            return builder.ToString().TrimEnd(' ', ',');
        }

        #endregion
    }
}
