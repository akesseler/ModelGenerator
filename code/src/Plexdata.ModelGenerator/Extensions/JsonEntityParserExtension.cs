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

using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace Plexdata.ModelGenerator.Extensions
{
    internal static class JsonEntityParserExtension
    {
        #region Private Fields

        private static readonly List<Delegate> IntegerDelegates = new List<Delegate>()
        {
            new Func<Object, Type>(JsonEntityParserExtension.TryGetTypeOfInt32),
            new Func<Object, Type>(JsonEntityParserExtension.TryGetTypeOfInt64),
            new Func<Object, Type>(JsonEntityParserExtension.TryGetTypeOfUInt32),
            new Func<Object, Type>(JsonEntityParserExtension.TryGetTypeOfUInt64),
        };

        private static readonly List<Delegate> FloatDelegates = new List<Delegate>()
        {
            new Func<Object, Type>(JsonEntityParserExtension.TryGetTypeOfDecimal),
            new Func<Object, Type>(JsonEntityParserExtension.TryGetTypeOfDouble),
        };

        private static readonly List<Delegate> StringDelegates = new List<Delegate>()
        {
            new Func<Object, Type>(JsonEntityParserExtension.TryGetTypeOfBoolean),
            new Func<Object, Type>(JsonEntityParserExtension.TryGetTypeOfGuid),
            new Func<Object, Type>(JsonEntityParserExtension.TryGetTypeOfBytes),
            new Func<Object, Type>(JsonEntityParserExtension.TryGetTypeOfUri),
            new Func<Object, Type>(JsonEntityParserExtension.TryGetTypeOfDateTime),
            new Func<Object, Type>(JsonEntityParserExtension.TryGetTypeOfString),
        };

        #endregion

        #region Public Methods

        public static Type GetValueType(this JValue source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            List<Delegate> delegates = new List<Delegate>();

            switch (source.Type)
            {
                case JTokenType.None:
                case JTokenType.Constructor:
                case JTokenType.Comment:
                case JTokenType.Undefined:
                case JTokenType.Raw:
                case JTokenType.Property:
                case JTokenType.Array:
                    throw new NotSupportedException($"Type of {source.Type} is not supported to get a proper value type from.");
                case JTokenType.Object:
                    return typeof(Object);
                case JTokenType.Boolean:
                    return typeof(Boolean);
                case JTokenType.Date:
                    return typeof(DateTime);
                case JTokenType.Null:
                    return typeof(Object);
                case JTokenType.Bytes: // Not yet seen (but handle by string conversion).
                    return typeof(Byte[]);
                case JTokenType.Guid: // Not yet seen (but handle by string conversion).
                    return typeof(Guid);
                case JTokenType.Uri: // Not yet seen (but handle by string conversion).
                    return typeof(Uri);
                case JTokenType.TimeSpan: // Not yet seen (but handle by string conversion).
                    return typeof(TimeSpan);
                case JTokenType.Integer:
                    delegates = JsonEntityParserExtension.IntegerDelegates;
                    break;
                case JTokenType.Float:
                    delegates = JsonEntityParserExtension.FloatDelegates;
                    break;
                case JTokenType.String:
                default:
                    delegates = JsonEntityParserExtension.StringDelegates;
                    break;
            }

            try
            {
                foreach (Delegate current in delegates)
                {
                    Type result = current.DynamicInvoke(source.Value) as Type;

                    if (result != null)
                    {
                        return result;
                    }
                }
            }
            catch (Exception exception)
            {
                if (exception is TargetInvocationException && exception.InnerException != null)
                {
                    Debug.WriteLine(exception.InnerException);
                }
                else
                {
                    Debug.WriteLine(exception);
                }
            }

            return null;
        }

        #endregion

        #region Integer Delegates

        private static Type TryGetTypeOfInt32(Object value)
        {
            if (Int32.TryParse(value?.ToString(), out Int32 result))
            {
                return typeof(Int32);
            }

            return null;
        }

        private static Type TryGetTypeOfInt64(Object value)
        {
            if (Int64.TryParse(value?.ToString(), out Int64 result))
            {
                return typeof(Int64);
            }

            return null;
        }

        private static Type TryGetTypeOfUInt32(Object value)
        {
            if (UInt32.TryParse(value?.ToString(), out UInt32 result))
            {
                return typeof(UInt32);
            }

            return null;
        }

        private static Type TryGetTypeOfUInt64(Object value)
        {
            if (UInt64.TryParse(value?.ToString(), out UInt64 result))
            {
                return typeof(UInt64);
            }

            return null;
        }

        #endregion

        #region Float Delegates

        private static Type TryGetTypeOfDecimal(Object value)
        {
            if (Decimal.TryParse(value?.ToString(), out Decimal result))
            {
                return typeof(Decimal);
            }

            return null;
        }

        private static Type TryGetTypeOfDouble(Object value)
        {
            if (Double.TryParse(value?.ToString(), out Double result))
            {
                return typeof(Double);
            }

            return null;
        }

        #endregion

        #region String Delegates

        private static Type TryGetTypeOfGuid(Object value)
        {
            if (Guid.TryParse(value?.ToString(), out Guid result))
            {
                return typeof(Guid);
            }

            return null;
        }

        private static Type TryGetTypeOfBoolean(Object value)
        {
            if (Boolean.TrueString.Equals(value?.ToString(), StringComparison.InvariantCultureIgnoreCase) ||
                Boolean.FalseString.Equals(value?.ToString(), StringComparison.InvariantCultureIgnoreCase))
            {
                return typeof(Boolean);
            }

            return null;
        }

        private static Type TryGetTypeOfBytes(Object value)
        {
            try
            {
                String result = value?.ToString();

                if (String.IsNullOrWhiteSpace(result))
                {
                    return null;
                }

                if (!result.EndsWith("="))
                {
                    return null;
                }

                if (result.Length % 4 != 0)
                {
                    return null;
                }

                String decoded = Encoding.UTF8.GetString(Convert.FromBase64String(result));
                String encoded = Convert.ToBase64String(Encoding.UTF8.GetBytes(decoded));

                if (!result.Equals(encoded, StringComparison.InvariantCultureIgnoreCase))
                {
                    return null;
                }

                return typeof(Byte[]);
            }
            catch
            {
                return null;
            }
        }

        private static Type TryGetTypeOfUri(Object value)
        {
            try
            {
                String source = value?.ToString();

                if (!Uri.IsWellFormedUriString(source, UriKind.Absolute))
                {
                    return null;
                }

                if (!Uri.TryCreate(source, UriKind.Absolute, out Uri result))
                {
                    return null;
                }

                if (result.Scheme != Uri.UriSchemeHttp && result.Scheme != Uri.UriSchemeHttps)
                {
                    return null;
                }

                return typeof(Uri);
            }
            catch
            {
                return null;
            }
        }

        private static Type TryGetTypeOfDateTime(Object value)
        {
            if (DateTime.TryParse(value?.ToString(), out DateTime result))
            {
                return typeof(DateTime);
            }

            return null;
        }

        private static Type TryGetTypeOfString(Object value)
        {
            try
            {
                foreach (Delegate current in JsonEntityParserExtension.IntegerDelegates)
                {
                    Type result = current.DynamicInvoke(value) as Type;

                    if (result != null)
                    {
                        return result;
                    }
                }
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
            }

            try
            {
                foreach (Delegate current in JsonEntityParserExtension.FloatDelegates)
                {
                    Type result = current.DynamicInvoke(value) as Type;

                    if (result != null)
                    {
                        return result;
                    }
                }
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
            }

            return typeof(String);
        }

        #endregion
    }
}
