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

using Plexdata.ModelGenerator.Defines;
using Plexdata.ModelGenerator.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Design.PluralizationServices;
using System.Globalization;
using System.Text;

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

        /// <summary>
        /// Creates a name representing an object or class name.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method creates a string that can be used as object or class name by 
        /// applying naming compliant rules.
        /// </para>
        /// </remarks>
        /// <param name="source">
        /// The string to be processed.
        /// </param>
        /// <returns>
        /// A string representing a valid object or class name.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// This exception is thrown in case of parameter <paramref name="source"/> 
        /// either is null or empty or consists only of whitespaces.
        /// </exception>
        /// <seealso cref="ClearSource(String)"/>
        /// <seealso cref="SplitSource(String)"/>
        /// <seealso cref="UnitePieces(List{String}, Boolean)"/>
        /// <seealso cref="ArrangeName(StringBuilder)"/>
        public static String CreateObjectName(this String source, AdjustmentSettings settings)
        {
            if (String.IsNullOrWhiteSpace(source))
            {
                throw new ArgumentOutOfRangeException(nameof(source), $"Parameter '{nameof(source)}' must not be null, empty or whitespace.");
            }

            return source
                .ClearSource()
                .SplitSource()
                .UnitePieces(!settings.IsCasing)
                .ArrangeName();
        }

        /// <summary>
        /// Creates a name representing a member name.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method creates a string that can be used as member name by applying 
        /// naming compliant rules as well as by enriching the result by user defined 
        /// adjustment settings.
        /// </para>
        /// </remarks>
        /// <param name="source">
        /// The string to be processed.
        /// </param>
        /// <param name="settings">
        /// The adjustments to be applied.
        /// </param>
        /// <param name="plural">
        /// Last member name part should be turned into its plural version.
        /// </param>
        /// <returns>
        /// A string representing a valid member name.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// This exception is thrown in case of parameter <paramref name="source"/> 
        /// either is null or empty or consists only of whitespaces.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// This exception is thrown in case of parameter <paramref name="settings"/> 
        /// is null.
        /// </exception>
        /// <seealso cref="ClearSource(String)"/>
        /// <seealso cref="SplitSource(String)"/>
        /// <seealso cref="ApplyPlural(List{String}, Boolean)"/>
        /// <seealso cref="UnitePieces(List{String}, Boolean)"/>
        /// <seealso cref="ArrangeName(StringBuilder, AdjustmentSettings)"/>
        public static String CreateMemberName(this String source, AdjustmentSettings settings, Boolean plural)
        {
            if (String.IsNullOrWhiteSpace(source))
            {
                throw new ArgumentOutOfRangeException(nameof(source), $"Parameter '{nameof(source)}' must not be null, empty or whitespace.");
            }

            if (settings is null)
            {
                throw new ArgumentNullException(nameof(settings), $"Parameter '{nameof(settings)}' must not be null.");
            }

            return source
                 .ClearSource()
                 .SplitSource()
                 .ApplyPlural(plural)
                 .UnitePieces(!settings.IsCasing)
                 .ArrangeName(settings);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Converts value of <paramref name="source"/> into a valid member name string.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method converts value of <paramref name="source"/> into a valid member 
        /// name string by removing any invalid character.
        /// </para>
        /// <para>
        /// The resulting string only includes digits and letters. Additionally, character 
        /// 'N' is prepended if value of <paramref name="source"/> starts with a digit.
        /// </para>
        /// </remarks>
        /// <param name="source">
        /// The string to be processed.
        /// </param>
        /// <returns>
        /// A string that can be used as member name.
        /// </returns>
        private static String ClearSource(this String source)
        {
            StringBuilder builder = new StringBuilder(128);

            foreach (Char current in source.ToCharArray())
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

        /// <summary>
        /// Splits value of <paramref name="source"/> into its pieces at each uppercase letter.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method divides value of <paramref name="source"/> at each uppercase letter. 
        /// Such a string to divide must consist only of digits and letters.
        /// </para>
        /// <para>
        /// This very useful algorithm has been found on the Internet under 
        /// https://stackoverflow.com/a/65210919.
        /// </para>
        /// </remarks>
        /// <param name="source">
        /// The string to be processed.
        /// </param>
        /// <returns>
        /// The list of string pieces starting with an upper letter.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// This is just a reminder if something went wrong in the method calling stack and 
        /// should never happen.
        /// </exception>
        private static List<String> SplitSource(this String source)
        {
            const Int32 isNone = 0;
            const Int32 isDigit = 1;
            const Int32 isUpper = 2;
            const Int32 isLower = 3;

            List<String> result = new List<String>();
            StringBuilder buffer = new StringBuilder(128);
            Int32 previous = isNone;

            foreach (Char character in source.ToCharArray())
            {
                Int32 current;

                if (Char.IsDigit(character))
                {
                    current = isDigit;
                }
                else if (Char.IsUpper(character))
                {
                    current = isUpper;
                }
                else if (Char.IsLower(character))
                {
                    current = isLower;
                }
                else
                {
                    throw new InvalidOperationException();
                }

                if ((previous == isNone) || (previous == current))
                {
                    buffer.Append(character);
                }
                else if ((previous == isUpper) && (current == isLower))
                {
                    if (buffer.Length > 1)
                    {
                        result.Add(buffer.ToString().Substring(0, buffer.Length - 1));

                        buffer.Remove(0, buffer.Length - 1);
                    }

                    buffer.Append(character);
                }
                else
                {
                    result.Add(buffer.ToString());
                    buffer.Clear();
                    buffer.Append(character);
                }

                previous = current;
            }

            if (buffer.Length != 0)
            {
                result.Add(buffer.ToString());
            }

            return result.FixIdsDescriptorMismatch();
        }

        /// <summary>
        /// Applies plural explicitly to the last list item.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method applies plural explicitly to the last list item.
        /// </para>
        /// </remarks>
        /// <param name="pieces">
        /// Item list to be processed.
        /// </param>
        /// <param name="plural">
        /// Plural is applied when true, otherwise result remains unchanged.
        /// </param>
        /// <returns>
        /// Source item list with a modification of last item.
        /// </returns>
        private static List<String> ApplyPlural(this List<String> pieces, Boolean plural)
        {
            if (plural && pieces.Count > 0)
            {
                pieces[pieces.Count - 1] = StringExtension.service.Pluralize(pieces[pieces.Count - 1]);
            }

            return pieces;
        }

        /// <summary>
        /// Reconciles strings of value <paramref name="pieces"/> by turning each first letter 
        /// into upper cases and each other letter into lower cases.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method reconciles strings of value <paramref name="pieces"/> by turning each 
        /// first letter into upper cases and each other letter into lower cases and combines 
        /// them.
        /// </para>
        /// </remarks>
        /// <param name="pieces">
        /// Member name pieces to be reconciled and combined.
        /// </param>
        /// <param name="unmodified">
        /// All items in <paramref name="pieces"/> are copied unmodified if true. Otherwise, 
        /// every first letter is converted to uppercase and every next letter to lowercase.
        /// </param>
        /// <returns>
        /// A string that can be used as member name.
        /// </returns>
        private static StringBuilder UnitePieces(this List<String> pieces, Boolean unmodified)
        {
            StringBuilder builder = new StringBuilder(128);

            for (Int32 outer = 0; outer < pieces.Count; outer++)
            {
                Char[] chars = pieces[outer].ToCharArray();

                for (Int32 inner = 0; !unmodified && inner < chars.Length; inner++)
                {
                    if (inner == 0)
                    {
                        chars[inner] = Char.ToUpperInvariant(chars[inner]);
                    }
                    else
                    {
                        chars[inner] = Char.ToLowerInvariant(chars[inner]);
                    }
                }

                builder.Append(chars);
            }

            return builder;
        }

        /// <summary>
        /// Converts <paramref name="source"/> into a string.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method is actually just to keep the same look and feel.
        /// </para>
        /// </remarks>
        /// <param name="source">
        /// The string to be processed.
        /// </param>
        /// <returns>
        /// The processed string.
        /// </returns>
        private static String ArrangeName(this StringBuilder source)
        {
            return source.ToString();
        }

        /// <summary>
        /// Applies provided adjustment settings depending on their state.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method applies provided adjustment settings depending on their state.
        /// </para>
        /// <para>
        /// The prefix is prepended as it is, but only if enabled. The first character is turned 
        /// into upper case, but only if enabled and if Pascal-Case is used. The first character 
        /// is turned into lower case, but only if enabled and if Camel-Case is used. The suffix 
        /// is appended if enabled and as it is. 
        /// </para>
        /// </remarks>
        /// <param name="source">
        /// The string to be processed.
        /// </param>
        /// <param name="settings">
        /// The settings to be applied.
        /// </param>
        /// <returns>
        /// A string with applied user adjustment settings that can be used member name.
        /// </returns>
        private static String ArrangeName(this StringBuilder source, AdjustmentSettings settings)
        {
            StringBuilder result = new StringBuilder(128);

            if (settings.IsPrefix)
            {
                result.Append(settings.Prefix);
            }

            if (settings.IsCasing)
            {
                if (settings.Casing == CasingType.PascalCase)
                {
                    source[0] = Char.ToUpperInvariant(source[0]);
                }

                if (settings.Casing == CasingType.CamelCase)
                {
                    source[0] = Char.ToLowerInvariant(source[0]);
                }
            }

            result.Append(source.ToString());

            if (settings.IsSuffix)
            {
                result.Append(settings.Suffix);
            }

            return result.ToString();
        }

        /// <summary>
        /// Fixes each descriptor part named "IDs".
        /// </summary>
        /// <remarks>
        /// <para>
        /// An item descriptor part "IDs" might be very common. But the algorithm in 
        /// <see cref="SplitSource(String)"/> splits it into the two items [I] and [Ds], 
        /// what is actually wrong. Therefore, this mismatch will be fixed by recombining 
        /// both parts.
        /// </para>
        /// </remarks>
        /// <param name="source">
        /// The list of naming parts to be fixed.
        /// </param>
        /// <returns>
        /// The fixed list of naming parts.
        /// </returns>
        private static List<String> FixIdsDescriptorMismatch(this List<String> source)
        {
            // HACK: Fixing of very common "IDs" descriptor.
            // An item descriptor part "IDs" might be very common. But the algorithm
            // in "SplitSource()" splits it into two items (see examples below), and
            // this mismatch has to be fixed by recombining both parts.
            // 
            // Examples:
            //   * "IDs"           => [I], [Ds]
            //   * "HelperIDs"     => [Helper], [I], [Ds]
            //   * "HelperIDsList" => [Helper], [I], [Ds], [List] 

            Int32 index = source.IndexOf("I");

            if (index >= 0 && index + 1 < source.Count && source[index + 1] == "Ds")
            {
                source[index] += source[index + 1];

                source.RemoveAt(index + 1);
            }

            return source;
        }

        #endregion
    }
}
