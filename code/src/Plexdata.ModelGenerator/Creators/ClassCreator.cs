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

using Plexdata.ModelGenerator.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Plexdata.ModelGenerator.Creators
{
    internal class ClassCreator
    {
        #region Construction

        public ClassCreator()
            : base()
        {
        }

        #endregion

        #region Public Methods

        public IEnumerable<Class> Create(Entity source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            List<Entity> entities = new List<Entity>();

            Entity clone = source.Clone() as Entity;

            this.AddClasses(clone, entities);

            IEnumerable<Class> result = entities.Select(x => new Class(x));

            return result;
        }

        #endregion

        #region Private Methods

        private void AddClasses(Entity source, List<Entity> result)
        {
            if (!source.IsClass && !source.IsArray)
            {
                return;
            }

            Boolean include = true;

            foreach (Entity current in result)
            {
                if (this.IsEqual(source, current))
                {
                    include = false;
                }
                else
                {
                    this.AddMissingChildren(source, result);
                }
            }

            if (include && source.IsClass)
            {
                result.Add(source);
            }

            foreach (Entity entity in source.Children)
            {
                this.AddClasses(entity, result);
            }
        }

        private Boolean IsEqual(Entity entity1, Entity entity2)
        {
            if (entity1.IsClass && entity2.IsClass && entity1.ObjectName == entity2.ObjectName)
            {
                foreach (Entity current in entity1.Children)
                {
                    if (!this.Contains(current, entity2.Children))
                    {
                        return false;
                    }
                }

                return true;
            }

            return false;
        }

        private void AddMissingChildren(Entity source, List<Entity> result)
        {
            Entity existing = this.FindExistingClass(source, result);

            if (existing != null)
            {
                foreach (Entity outer in source.Children)
                {
                    Entity other = this.FindOther(outer, existing.Children);

                    if (other != null)
                    {
                        this.ChangeType(outer, other);
                    }
                    else
                    {
                        existing.AddChildEntity(outer);
                    }
                }
            }
        }

        private Entity FindExistingClass(Entity source, List<Entity> result)
        {
            foreach (Entity entity in result)
            {
                if (entity.IsClass && source.IsClass && entity.ObjectName == source.ObjectName)
                {
                    return entity;
                }
            }

            return null;
        }

        private Boolean Contains(Entity source, IEnumerable<Entity> entities)
        {
            return this.FindOther(source, entities) != null;
        }

        private Entity FindOther(Entity source, IEnumerable<Entity> entities)
        {
            foreach (Entity entity in entities)
            {
                if (entity.ObjectName == source.ObjectName)
                {
                    return entity;
                }
            }

            return null;
        }

        private void ChangeType(Entity source, Entity other)
        {
            if (source.MemberType == other.MemberType)
            {
                return;
            }

            if (other.MemberType == typeof(Int32))
            {
                if (source.MemberType == typeof(Int64))
                {
                    other.ReviseMemberType(source.MemberType);
                    return;
                }
            }

            if (other.MemberType == typeof(UInt32))
            {
                if (source.MemberType == typeof(UInt64))
                {
                    other.ReviseMemberType(source.MemberType);
                    return;
                }
            }
        }

        #endregion
    }
}
