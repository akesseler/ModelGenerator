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

using Plexdata.ModelGenerator.Creators;
using Plexdata.ModelGenerator.Defines;
using Plexdata.ModelGenerator.Interfaces;
using Plexdata.ModelGenerator.Models;
using Plexdata.ModelGenerator.Parsers;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Plexdata.ModelGenerator.Generators
{
    public static class ModelGenerator
    {
        #region Construction

        static ModelGenerator()
        {
        }

        #endregion

        #region Public Methods

        public static IEnumerable<ICode> Generate(GeneratorSettings settings, String source)
        {
            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            if (String.IsNullOrWhiteSpace(source))
            {
                throw new ArgumentOutOfRangeException(nameof(source), "Source string must not be null, empty, or contain only whitespaces.");
            }

            IEntityParser parser = null;

            switch (settings.SourceType)
            {
                case SourceType.Json:
                    parser = new JsonEntityParser(settings);
                    break;
                case SourceType.Xml:
                    parser = new XmlEntityParser(settings);
                    break;
                default:
                    throw new InvalidEnumArgumentException(nameof(settings.SourceType), (Int32)settings.SourceType, typeof(SourceType));
            }

            Entity entity = parser.Parse(source);

            IEnumerable<Class> classes = ModelGenerator.CreateClasses(settings, entity);

            IEnumerable<ICode> results = ModelGenerator.CreateResults(settings, classes);

            return results;
        }

        #endregion

        #region Private Methods

        private static IEnumerable<Class> CreateClasses(GeneratorSettings settings, Entity entity)
        {
            ClassCreator creator = new ClassCreator();

            return creator.Create(entity);
        }

        private static IEnumerable<Code> CreateResults(GeneratorSettings settings, IEnumerable<Class> classes)
        {
            CodeCreator creator = new CodeCreator(settings);

            return creator.Create(classes);
        }

        #endregion
    }
}
