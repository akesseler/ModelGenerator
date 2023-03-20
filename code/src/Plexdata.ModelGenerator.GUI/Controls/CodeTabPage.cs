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

using Plexdata.ModelGenerator.Interfaces;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Plexdata.ModelGenerator.Gui.Controls
{
    public class CodeTabPage : TabPage
    {
        #region Private Fields 

        private readonly ICode code = null;

        private readonly RichTextBox edit = null;

        #endregion

        #region Construction

        public CodeTabPage(ICode code)
            : base()
        {
            this.code = code ?? throw new ArgumentNullException(nameof(code));
            this.edit = new RichTextBox();

            this.Controls.Add(this.edit);

            this.TabIndex = 0;
            this.Location = new Point(4, 22);
            this.Name = this.code.Filename;
            this.Text = this.code.Filename;
            this.ToolTipText = String.IsNullOrWhiteSpace(this.code.Location) ? this.code.Filename : this.code.Location;
            this.Padding = new Padding(3);
            this.Size = new Size(768, 235);
            this.UseVisualStyleBackColor = true;

            this.edit.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.edit.BorderStyle = BorderStyle.FixedSingle;
            this.edit.Location = new Point(6, 6);
            this.edit.Name = "txtEdit";
            this.edit.Size = new Size(756, 223);
            this.edit.TabIndex = 0;
            this.edit.Text = this.code.Content;
            this.edit.WordWrap = false;
            this.edit.Multiline = true;
            this.edit.AcceptsTab = true;
            this.edit.Font = new Font("Consolas", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
        }

        #endregion

        #region Public Methods

        public void Save(String folder, Boolean overwrite)
        {
            try
            {
                this.code.Save(folder, overwrite);
                this.Text = this.code.Filename;
                this.ToolTipText = this.code.Location;
            }
            catch (Exception exception)
            {
                System.Diagnostics.Debug.WriteLine(exception);
                throw;
            }
        }

        #endregion
    }
}
