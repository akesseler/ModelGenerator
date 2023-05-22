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
using Plexdata.ModelGenerator.Models;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace Plexdata.ModelGenerator.Parsers
{
    internal class XmlEntityParser : JsonEntityParser
    {
        #region Construction

        public XmlEntityParser(GeneratorSettings settings)
            : base(settings)
        {
        }

        #endregion

        #region Public Methods

        public override Entity Parse(String source)
        {
            if (String.IsNullOrWhiteSpace(source))
            {
                throw new ArgumentOutOfRangeException(nameof(source));
            }

            XmlDocument document = this.LoadDocument(source);

            String xmlns = document.DocumentElement.Attributes["xmlns"]?.Value;

            document.DocumentElement.Attributes.RemoveAll();

            this.ClearComments(document);

            Entity result = base.Parse(JsonConvert.SerializeXmlNode(document.DocumentElement));

            result = this.GetFixedResult(result, xmlns);

            return result;
        }

        #endregion

        #region Private Methods

        private XmlDocument LoadDocument(String source)
        {
            // Everything is complicated when using XML. WTF... Method LoadXml() 
            // crashes as soon as a source string contains a leading UTF preamble, 
            // such as 0xEF, 0xBB, 0xBF. Thus, load XML from a stream instead to 
            // avoid such as crash.
            XmlDocument result = new XmlDocument();

            Byte[] buffer = Encoding.UTF8.GetBytes(source);

            using (MemoryStream stream = new MemoryStream(buffer))
            {
                result.Load(stream);
            }

            return result;
        }

        private void ClearComments(XmlNode parent)
        {
            for (Int32 index = 0; index < parent.ChildNodes.Count; index++)
            {
                XmlNode child = parent.ChildNodes[index];

                if (child is XmlComment)
                {
                    parent.RemoveChild(child);
                    index--;
                }
                else
                {
                    this.ClearComments(child);
                }
            }
        }

        private Entity GetFixedResult(Entity source, String xmlns)
        {
            if (source is null)
            {
                return source;
            }

            if (source.Parent is null)
            {
                if (source.Children.Count() > 1)
                {
                    throw new InvalidOperationException("An entity considered as root element contains more than one children.");
                }

                // Root class must be removed.

                source = source.Children.FirstOrDefault();

                if (source is null)
                {
                    return source;
                }

                source.ReviseXmlNamespace(xmlns);
            }

            return source;
        }

        #endregion
    }
}
