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
using System.Globalization;
using System.Text;

namespace Plexdata.ModelGenerator.Models
{
    internal class Entity : ICloneable
    {
        #region Private Fields 

        private String name = String.Empty;

        private Type type = null;

        private String origin = String.Empty;

        private String comment = String.Empty;

        private readonly List<Entity> children = null;

        #endregion

        #region Construction

        private Entity()
            : base()
        {
            this.name = String.Empty;
            this.type = null;
            this.origin = String.Empty;
            this.comment = String.Empty;
            this.XmlNamespace = String.Empty;

            this.children = new List<Entity>();
        }

        public Entity(String name)
            : this(name, null, null)
        {
        }

        public Entity(String name, Type type)
            : this(name, type, null)
        {
        }

        public Entity(String name, Type type, String comment)
            : base()
        {
            this.Name = name;
            this.Type = type;
            this.Comment = comment;
            this.XmlNamespace = String.Empty;

            this.children = new List<Entity>();
        }

        #endregion

        #region Public Properties

        public String Name
        {
            get
            {
                return this.name;
            }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentOutOfRangeException(nameof(this.Name));
                }

                value = value.Trim();

                this.name = this.BuildName(value);

                if (!String.Equals(this.name, value))
                {
                    this.Origin = value;
                }
            }
        }

        public Type Type
        {
            get
            {
                return this.type;
            }
            set
            {
                if (value == null)
                {
                    value = typeof(Object);
                }

                this.type = value;
            }
        }

        public Entity Parent
        {
            get;
            private set;
        }

        public String Path
        {
            get
            {
                List<String> pieces = new List<String>();

                pieces.Insert(0, this.Name);

                Entity parent = this.Parent;

                while (parent != null)
                {
                    pieces.Insert(0, parent.Name);

                    parent = parent.Parent;
                }

                return String.Join(".", pieces);
            }
        }

        public String Namespace
        {
            get
            {
                return this.Type.Namespace;
            }
        }

        public String XmlNamespace
        {
            get;
            set;
        }

        public Boolean HasXmlNamespace
        {
            get
            {
                return !String.IsNullOrWhiteSpace(this.XmlNamespace);
            }
        }

        public String Origin
        {
            get
            {
                return this.origin;
            }
            private set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    value = String.Empty;
                }

                this.origin = value.Trim();
            }
        }

        public Boolean IsOrigin
        {
            get
            {
                return !String.IsNullOrWhiteSpace(this.Origin);
            }
        }

        public String Comment
        {
            get
            {
                return this.comment;
            }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    value = String.Empty;
                }

                this.comment = value.Trim();
            }
        }

        public virtual Boolean IsComment
        {
            get
            {
                return !String.IsNullOrWhiteSpace(this.Comment);
            }
        }

        public Boolean IsClass
        {
            get
            {
                return this.Type == typeof(Object) && this.children.Count > 0;
            }
        }

        public Boolean IsArray
        {
            get
            {
                return this.Type == typeof(Array) && this.children.Count > 0;
            }
        }

        public IEnumerable<Entity> Children
        {
            get
            {
                return this.children;
            }
        }

        public Int32 ChildCount
        {
            get
            {
                return this.children.Count;
            }
        }

        #endregion

        #region Public Methods

        public void AddEntity(Entity entity)
        {
            if (entity != null)
            {
                entity.Parent = this;

                this.children.Add(entity);
            }
        }

        public Object Clone()
        {
            Entity clone = new Entity();

            clone.name = this.name;
            clone.type = this.type;
            clone.origin = this.origin;
            clone.comment = this.comment;
            clone.XmlNamespace = this.XmlNamespace;

            foreach (Entity child in this.children)
            {
                clone.AddEntity(child.Clone() as Entity);
            }

            return clone;
        }

        public override String ToString()
        {
            StringBuilder builder = new StringBuilder(128);

            builder.AppendFormat("{0}: {1}, ", nameof(this.Name), this.Name);
            builder.AppendFormat("{0}: {1}, ", nameof(this.Type), this.Type);

            if (this.IsOrigin)
            {
                builder.AppendFormat("{0}: {1}, ", nameof(this.Origin), this.Origin);
            }

            if (this.IsClass)
            {
                builder.AppendFormat("{0}: {1}, ", nameof(this.IsClass), Boolean.TrueString);
            }

            if (this.IsComment)
            {
                builder.AppendFormat("{0}: {1}, ", nameof(this.Comment), Boolean.TrueString);
            }

            if (this.children.Count > 0)
            {
                builder.AppendFormat("{0}.{1}: {2}, ", nameof(this.Children), nameof(this.children.Count), this.children.Count);
            }

            return builder.ToString().TrimEnd(' ', ',');
        }

        #endregion

        #region Private Methods

        private String BuildName(String label)
        {
            StringBuilder builder = new StringBuilder(128);

            // Don't change label if it is already in right casing...

            foreach (Char current in label.ToCharArray())
            {
                if (Char.IsLetterOrDigit(current))
                {
                    builder.Append(current);
                }
            }

            if (builder.Length == label.Length)
            {
                if (Char.IsDigit(builder[0]))
                {
                    builder.Insert(0, 'N');
                }

                if (Char.IsLower(builder[0]))
                {
                    builder[0] = Char.ToUpper(builder[0]);
                }

                return builder.ToString();
            }

            builder.Clear();

            foreach (Char current in CultureInfo.InvariantCulture.TextInfo.ToTitleCase(label.ToLowerInvariant()).ToCharArray())
            {
                if (Char.IsLetterOrDigit(current))
                {
                    builder.Append(current);
                }
            }

            if (Char.IsDigit(builder[0]))
            {
                builder.Insert(0, 'N');
            }

            return builder.ToString();
        }

        #endregion
    }
}
