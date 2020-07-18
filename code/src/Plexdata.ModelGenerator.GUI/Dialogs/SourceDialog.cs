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
using Plexdata.ModelGenerator.Defines;
using Plexdata.ModelGenerator.Gui.Events;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace Plexdata.ModelGenerator.Gui.Dialogs
{
    public partial class SourceDialog : Form
    {
        #region Private Fields

        private readonly String caption = String.Empty;

        private SourceType type = SourceType.Unknown;

        #endregion

        #region Construction

        public SourceDialog()
            : this(null, SourceType.Unknown)
        {
        }

        public SourceDialog(String source, SourceType type)
            : base()
        {
            this.InitializeComponent();

            this.caption = this.Text;

            this.txtSource.IsEditing = this.chkEditing.Checked;

            this.txtSource.OnPaste += this.OnSourcePaste;

            this.ApplyContent(source, type);
        }

        #endregion

        #region Public Properties

        public String Source
        {
            get
            {
                return (this.txtSource.Text ?? String.Empty).Trim();
            }
            private set
            {
                this.txtSource.Text = (value ?? String.Empty).Trim();
                this.txtSource.Select(0, 0);
            }
        }

        public SourceType Type
        {
            get
            {
                return this.type;
            }
            private set
            {
                this.type = value;
                this.SetCaption(this.type.ToString().ToUpper());
            }
        }

        #endregion

        #region Private Events

        private void OnSourcePaste(Object sender, ClipboardEventArgs args)
        {
            String result;

            if (this.TryFormatJson(args.Text, out result))
            {
                this.Source = result;
                this.Type = SourceType.Json;
                return;
            }

            if (this.TryFormatXml(args.Text, out result))
            {
                this.Source = result;
                this.Type = SourceType.Xml;
                return;
            }

            this.ShowError("Unable to paste document, neither as JSON nor as XML. Maybe enable edit mode and try again.");
        }

        private void OnEditingCheckedChanged(Object sender, EventArgs args)
        {
            this.txtSource.IsEditing = this.chkEditing.Checked;
        }

        #endregion

        #region Private Methods

        private void ApplyContent(String source, SourceType type)
        {
            try
            {
                switch (type)
                {
                    case SourceType.Json:
                        this.Source = this.FormatJson(source);
                        this.Type = SourceType.Json;
                        break;
                    case SourceType.Xml:
                        this.Source = this.FormatXml(source);
                        this.Type = SourceType.Xml;
                        break;
                    default:
                        this.Source = null;
                        this.Type = SourceType.Unknown;
                        break;
                }
            }
            catch (Exception exception)
            {
                this.ShowError(exception);
            }
        }

        private Boolean TryFormatJson(String source, out String result)
        {
            result = String.Empty;

            try
            {
                result = this.FormatJson(source);
                return true;
            }
            catch (Exception exception)
            {
                System.Diagnostics.Debug.WriteLine(exception);
                return false;
            }
        }

        private Boolean TryFormatXml(String source, out String result)
        {
            result = String.Empty;

            try
            {
                result = this.FormatXml(source);
                return true;
            }
            catch (Exception exception)
            {
                System.Diagnostics.Debug.WriteLine(exception);
                return false;
            }
        }

        private String FormatJson(String source)
        {
            if (String.IsNullOrWhiteSpace(source))
            {
                return source;
            }

            Object parsed = JsonConvert.DeserializeObject(source);
            return JsonConvert.SerializeObject(parsed, Newtonsoft.Json.Formatting.Indented);
        }

        private String FormatXml(String source)
        {
            if (String.IsNullOrWhiteSpace(source))
            {
                return source;
            }

            XmlDocument document = this.LoadDocument(source);
            Encoding encoding = this.ReadEncoding(document);

            using (MemoryStream stream = new MemoryStream())
            {
                using (XmlTextWriter writer = new XmlTextWriter(stream, encoding))
                {
                    writer.Formatting = System.Xml.Formatting.Indented;
                    document.Save(writer);

                    return encoding.GetString(stream.ToArray());
                }
            }
        }

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

        private Encoding ReadEncoding(XmlDocument document)
        {
            XmlDeclaration declaration = document.ChildNodes.OfType<XmlDeclaration>().FirstOrDefault();

            if (declaration != null && !String.IsNullOrWhiteSpace(declaration.Encoding))
            {
                return Encoding.GetEncoding(declaration.Encoding);
            }

            return Encoding.UTF8;
        }

        private void SetCaption(String text)
        {
            if (String.IsNullOrWhiteSpace(text))
            {
                this.Text = this.caption;
            }
            else
            {
                this.Text = $"{this.caption} - {text}";
            }
        }

        private void ShowError(String message)
        {
            MessageBox.Show(this, message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ShowError(Exception exception)
        {
            this.ShowError(exception.Message);
        }

        #endregion
    }
}
