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

using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace Plexdata.ModelGenerator.GUI.Dialogs
{
    public partial class SaveDialog : Form
    {
        #region Construction

        public SaveDialog()
            : base()
        {
            this.InitializeComponent();
        }

        #endregion

        #region Public Properties

        public Boolean Overwrite
        {
            get
            {
                return this.chkOverwrite.Checked;
            }
            set
            {
                this.chkOverwrite.Checked = value;
            }
        }

        public String Folder
        {
            get
            {
                return (this.txtFolder.Text ?? String.Empty).Trim();
            }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    value = String.Empty;
                }

                this.txtFolder.Text = value.Trim();
            }
        }

        #endregion

        #region Protected Methods

        protected override void OnLoad(EventArgs args)
        {
            base.OnLoad(args);

            this.chkOverwrite.Checked = this.Overwrite;
            this.txtFolder.Text = this.Folder;
        }

        protected override void OnClosing(CancelEventArgs args)
        {
            if (base.DialogResult == DialogResult.OK && String.IsNullOrWhiteSpace(this.txtFolder.Text))
            {
                MessageBox.Show(this, "Choose an output folder beforehand.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                args.Cancel = true;
            }

            base.OnClosing(args);
        }

        #endregion

        #region Private Methods

        private void OnFolderButtonClick(Object sender, EventArgs args)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog
            {
                ShowNewFolderButton = true,
                SelectedPath = Directory.Exists(this.Folder) ? this.Folder : String.Empty,
                Description = "Choose the folder where generated files should be saved."
            };

            if (dialog.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            this.Folder = dialog.SelectedPath;
        }

        #endregion
    }
}
