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
using Plexdata.ModelGenerator.Defines;
using Plexdata.ModelGenerator.Gui.Events;
using Plexdata.ModelGenerator.Gui.Extensions;
using Plexdata.ModelGenerator.Gui.Models;
using Plexdata.ModelGenerator.Gui.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        private readonly CreateSettings settings = null;
        private SourceType type = SourceType.Unknown;

        #endregion

        #region Construction

        public SourceDialog()
            : this(SourceType.Unknown, null)
        {
        }

        public SourceDialog(SourceType type, CreateSettings settings)
            : base()
        {
            this.InitializeComponent();

            this.caption = this.Text;
            this.settings = settings ?? new CreateSettings();

            this.txtSource.IsEditing = false; // Edit mode distinction is no longer supported.
            this.txtSource.OnPaste += this.OnSourcePaste;
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

        #region Protected Methods

        protected override void OnClosing(CancelEventArgs args)
        {
            base.OnClosing(args);

            if (base.DialogResult != DialogResult.OK)
            {
                return;
            }

            if (this.Type != SourceType.Unknown && !String.IsNullOrWhiteSpace(this.Source))
            {
                return;
            }

            String result;

            if (this.Type == SourceType.Unknown && this.TryFormatJson(this.Source, out result))
            {
                this.Source = result;
                this.Type = SourceType.Json;
                return;
            }

            if (this.Type == SourceType.Unknown && this.TryFormatXml(this.Source, out result))
            {
                this.Source = result;
                this.Type = SourceType.Xml;
                return;
            }

            this.Source = String.Empty;
            this.Type = SourceType.Unknown;
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

            this.ShowError("Unable to paste document, neither as JSON nor as XML.");
        }

        #endregion

        #region Private Methods

        private Boolean TryFormatJson(String source, out String result)
        {
            result = String.Empty;

            // This is certainly not really an elegant solution
            // but unfortunately necessary to figure out valid
            // source data.
            try
            {
                result = this.FormatJson(source, true);
                return true;
            }
            catch
            {
                try
                {
                    result = this.FormatJson(source, false);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        private Boolean TryFormatXml(String source, out String result)
        {
            result = String.Empty;

            // This is certainly not really an elegant solution
            // but unfortunately necessary to figure out valid
            // source data.
            try
            {
                result = this.FormatXml(source, true);
                return true;
            }
            catch
            {
                try
                {
                    result = this.FormatXml(source, false);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        private String FormatJson(String source, Boolean replace)
        {
            if (String.IsNullOrWhiteSpace(source))
            {
                return source;
            }

            if (replace)
            {
                source = this.Replace(source, this.settings.JsonReplacements);
            }

            Object parsed = JsonConvert.DeserializeObject(source);
            return JsonConvert.SerializeObject(parsed, Newtonsoft.Json.Formatting.Indented);
        }

        private String FormatXml(String source, Boolean replace)
        {
            if (String.IsNullOrWhiteSpace(source))
            {
                return source;
            }

            if (replace)
            {
                source = this.Replace(source, this.settings.XmlReplacements);
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

        private String Replace(String source, IEnumerable<Replacement> replacements)
        {
            String result = source;

            foreach (Replacement replacement in replacements)
            {
                result = result.Replace(replacement.Source, replacement.Target);
            }

            return result;
        }

        #endregion
    }
}
