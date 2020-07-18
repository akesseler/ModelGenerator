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

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Plexdata.ModelGenerator.Extensions;
using Plexdata.ModelGenerator.Interfaces;
using Plexdata.ModelGenerator.Models;
using System;

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

            Entity result = null;

            if (input.Type == JTokenType.Object)
            {
                result = this.Parse(input as JObject, this.settings.RootClass.ToSingular());
            }
            else if (input.Type == JTokenType.Array)
            {
                result = this.Parse(input as JArray, this.settings.RootClass.ToPlural());
            }
            else
            {
                throw new NotSupportedException($"Type of {input.Type} is not supported as top level instance.");
            }

            return result;
        }

        #endregion

        #region Private Methods

        private Entity Parse(JObject source, String name)
        {
            Entity result = new Entity(name);

            foreach (JToken token in source.PropertyValues())
            {
                Entity child = this.Parse(token);

                result.AddEntity(child);
            }

            return result;
        }

        private Entity Parse(JArray source, String name)
        {
            Entity result = new Entity(name, typeof(Array));

            name = name.ToSingular();

            foreach (JToken token in source.Children())
            {
                if (token.Type == JTokenType.Object)
                {
                    result.AddEntity(this.Parse(token.Value<JObject>(), name));
                }
                else
                {
                    result.AddEntity(this.Parse(token));
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

            Entity entity = new Entity(name);

            foreach (JToken token in source.Children())
            {
                entity.AddEntity(this.Parse(token));
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

            name = name.ToPlural();

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
                    return new Entity(name, typeof(Object), "TODO: Review object type.");
                default:
                    return new Entity(name, source.GetValueType());
            }
        }

        #endregion
    }
}
