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

using Plexdata.ModelGenerator.Models;
using System;
using System.Collections.Generic;

namespace Plexdata.ModelGenerator.Creators
{
    internal class CodeCreator
    {
        #region Private Fields

        private readonly GeneratorSettings settings;

        #endregion

        #region Construction

        public CodeCreator(GeneratorSettings settings)
            : base()
        {
            this.settings = settings ?? throw new ArgumentNullException(nameof(settings), "Generator settings must not be null.");

            if (!this.settings.IsValid)
            {
                throw new ArgumentOutOfRangeException(nameof(settings), "Generator settings are not considered as valid.");
            }
        }

        #endregion

        #region Public Methods

        public IEnumerable<Code> Create(IEnumerable<Class> sources)
        {
            if (sources == null)
            {
                throw new ArgumentNullException(nameof(sources));
            }

            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            List<Code> result = new List<Code>();

            if (this.settings.IsAllInOne)
            {
                result.Add(Code.FromClassList(sources, this.settings));
            }
            else
            {
                foreach (Class source in sources)
                {
                    result.Add(Code.FromClassItem(source, this.settings));
                }
            }

            return result;
        }

        #endregion
    }
}
