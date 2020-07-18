﻿/*
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
using System.Data.Entity.Design.PluralizationServices;
using System.Globalization;

namespace Plexdata.ModelGenerator.Extensions
{
    internal static class StringExtension
    {
        #region Private Fields

        private static readonly PluralizationService service = null;

        #endregion

        #region Construction

        static StringExtension()
        {
            StringExtension.service = PluralizationService.CreateService(CultureInfo.CreateSpecificCulture("en"));
        }

        #endregion

        #region Public Methods

        public static String ToPlural(this String value)
        {
            if (!String.IsNullOrWhiteSpace(value))
            {
                value = StringExtension.service.Pluralize(value);
            }

            return value;
        }

        public static String ToSingular(this String value)
        {
            if (!String.IsNullOrWhiteSpace(value))
            {
                value = StringExtension.service.Singularize(value);
            }

            return value;
        }

        #endregion
    }
}
