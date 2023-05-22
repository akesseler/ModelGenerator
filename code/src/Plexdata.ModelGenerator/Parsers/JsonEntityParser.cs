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

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Plexdata.ModelGenerator.Extensions;
using Plexdata.ModelGenerator.Interfaces;
using Plexdata.ModelGenerator.Models;
using System;
using System.Linq;
using System.Xml;

namespace Plexdata.ModelGenerator.Parsers
{
    internal class JsonEntityParser : IEntityParser
    {
        #region Private Fields 

        private readonly GeneratorSettings settings;

        #endregion

        #region Construction

        public JsonEntityParser(GeneratorSettings settings)
        {
            this.settings = settings ?? throw new ArgumentNullException(nameof(settings), "Generator settings must not be null.");

            if (!this.settings.IsValid)
            {
                throw new ArgumentOutOfRangeException(nameof(settings), "Generator settings are not considered as valid.");
            }
        }

        #endregion

        // TODO: For XML the "Array Issue" might be fixed in a later version.
        // With "Array Issue" is meant that currently each array type is put in a separate class
        // which contains just one single member of type List<...>. For XML this could/should be
        // fixed by removing this single child and using the attributes [XmlArray("ParentTypeName")]
        // and [XmlArrayItem("ChildTypeName")] instead.

        #region Public Methods

        public virtual Entity Parse(String source)
        {
            if (String.IsNullOrWhiteSpace(source))
            {
                throw new ArgumentOutOfRangeException(nameof(source));
            }

            if (!(JsonConvert.DeserializeObject(source) is JContainer input))
            {
                throw new InvalidOperationException("Unable to deserialize source string.");
            }

            if (input.Type == JTokenType.Object)
            {
                return this.Parse(input as JObject, this.settings.RootClass);
            }

            if (input.Type == JTokenType.Array)
            {
                return this.Parse(input as JArray, this.settings.RootClass);
            }

            throw new NotSupportedException($"Type of {input.Type} is not supported as top level instance.");
        }

        #endregion

        #region Private Methods

        private Entity Parse(JObject source, String name)
        {
            Entity result = Entity.Create(this.settings.Adjustment, name);

            foreach (JToken token in source.PropertyValues())
            {
                result.AddChildEntity(this.Parse(token));
            }

            return result;
        }

        private Entity Parse(JArray source, String name)
        {
            Entity result = Entity.Create(this.settings.Adjustment, name, typeof(Array));

            foreach (JToken token in source.Children())
            {
                if (token.Type == JTokenType.Object)
                {
                    result.AddChildEntity(this.Parse(token.Value<JObject>(), name));
                }
                else
                {
                    result.AddChildEntity(this.Parse(token));
                }
            }

            return result;
        }

        private Entity Parse(JToken source)
        {
            if (source is JObject @object)
            {
                return this.Parse(@object);
            }

            if (source is JArray array)
            {
                return this.Parse(array);
            }

            if (source is JProperty property)
            {
                return this.Parse(property);
            }

            if (source is JValue value)
            {
                return this.Parse(value);
            }

            return null;
        }

        private Entity Parse(JObject source)
        {
            String name = source.Type.ToString();

            if (source.Parent is JProperty property)
            {
                name = property.Name;
            }

            Entity entity = Entity.Create(this.settings.Adjustment, name);

            // Can't be put into derived class.
            if (this.IsXmlCDataSection(source))
            {
                entity.ReviseComment(
                    "TODO: See also https://learn.microsoft.com/en-us/dotnet/api/system.xml.xmlcdatasection " +
                    "and see https://learn.microsoft.com/de-de/dotnet/api/system.xml.xmldocument.createcdatasection");

                entity.ReviseMemberType(typeof(XmlCDataSection));

                return entity;
            }

            foreach (JToken token in source.Children())
            {
                entity.AddChildEntity(this.Parse(token));
            }

            return entity;
        }

        private Entity Parse(JArray source)
        {
            String name = source.Type.ToString();

            if (source.Parent is JProperty property)
            {
                name = property.Name;
            }

            return this.Parse(source, name);
        }

        private Entity Parse(JProperty source)
        {
            if (source.Value is JValue value)
            {
                return this.Parse(value);
            }
            else if (source.Value is JObject @object)
            {
                return this.Parse(@object);
            }
            else if (source.Value is JArray array)
            {
                return this.Parse(array);
            }

            throw new NotSupportedException($"Property value type of {source?.Value?.GetType()?.ToString() ?? "NULL"} is not supported.");
        }

        private Entity Parse(JValue source)
        {
            JProperty property = source?.Parent as JProperty;

            String name = property?.Name ?? source.Type.ToString();

            switch (source.Type)
            {
                case JTokenType.Null:
                    return Entity.Create(this.settings.Adjustment, name, typeof(Object), "TODO: Review object type.");
                default:
                    return Entity.Create(this.settings.Adjustment, name, source.GetValueType());
            }
        }

        private Boolean IsXmlCDataSection(JObject source)
        {
            JEnumerable<JToken> tokens = source.Children();

            if (tokens.Count() == 1 && tokens.First() is JProperty property)
            {
                return property.Name.Equals("#cdata-section", StringComparison.InvariantCultureIgnoreCase);
            }

            return false;
        }

        #endregion
    }
}
