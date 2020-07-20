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

using Plexdata.ModelGenerator.Defines;
using Plexdata.ModelGenerator.Gui.Controls;
using Plexdata.ModelGenerator.Gui.Dialogs;
using Plexdata.ModelGenerator.Gui.Extensions;
using Plexdata.ModelGenerator.Gui.Settings;
using Plexdata.ModelGenerator.GUI.Dialogs;
using Plexdata.ModelGenerator.Interfaces;
using Plexdata.ModelGenerator.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Plexdata.ModelGenerator.Gui
{
    public partial class MainWindow : Form
    {
        #region Private Fields

        private ProgramSettings settings = null;

        private String source = String.Empty;

        #endregion

        #region Construction

        public MainWindow()
            : base()
        {
            this.InitializeComponent();

            this.Text = InfoDialog.Title;
        }

        #endregion

        #region Private Properties

        private Boolean IsValidFilename
        {
            get
            {
                return !String.IsNullOrWhiteSpace(this.txtFilename.Text) && File.Exists(this.txtFilename.Text);
            }
        }

        private Boolean IsValidSource
        {
            get
            {
                return !String.IsNullOrWhiteSpace(this.source);
            }
        }

        #endregion

        #region Protected Methods

        protected override void OnLoad(EventArgs args)
        {
            base.OnLoad(args);

            if (SettingsSerializer.Load(out ProgramSettings settings))
            {
                this.settings = settings;
            }
            else
            {
                this.settings = new ProgramSettings();
            }

            this.settings.MainWindow.EnsureDisplayAttributes(this);

            this.tbcCode.TabPages.Clear();
            this.tbbPlay.Enabled = false;
            this.tbbSave.Enabled = false;

            this.cmbSourceType.DataSource = Enum.GetValues(typeof(SourceType));
            this.cmbTargetType.DataSource = Enum.GetValues(typeof(TargetType));
            this.cmbMemberType.DataSource = Enum.GetValues(typeof(MemberType));
            this.cmbAttributeType.DataSource = Enum.GetValues(typeof(AttributeType));

            this.txtFilename.Text = this.settings.SourceSettings.Filename;
            this.txtRootClass.Text = this.settings.SourceSettings.RootClass;
            this.txtNamespace.Text = this.settings.SourceSettings.Namespace;
            this.cmbSourceType.SelectedItem = (SourceType)this.settings.SourceSettings.SourceType;
            this.cmbTargetType.SelectedItem = (TargetType)this.settings.SourceSettings.TargetType;
            this.cmbMemberType.SelectedItem = (MemberType)this.settings.SourceSettings.MemberType;
            this.cmbAttributeType.SelectedItem = (AttributeType)this.settings.SourceSettings.AttributeType;
            this.chkAllInOne.Checked = this.settings.SourceSettings.AllInOne;
        }

        protected override void OnFormClosing(FormClosingEventArgs args)
        {
            this.settings.SourceSettings.Filename = this.txtFilename.Text;
            this.settings.SourceSettings.RootClass = this.txtRootClass.Text;
            this.settings.SourceSettings.Namespace = this.txtNamespace.Text;
            this.settings.SourceSettings.SourceType = (Int32)this.cmbSourceType.SelectedItem;
            this.settings.SourceSettings.TargetType = (Int32)this.cmbTargetType.SelectedItem;
            this.settings.SourceSettings.MemberType = (Int32)this.cmbMemberType.SelectedItem;
            this.settings.SourceSettings.AttributeType = (Int32)this.cmbAttributeType.SelectedItem;
            this.settings.SourceSettings.AllInOne = this.chkAllInOne.Checked;

            SettingsSerializer.Save(this.settings);

            base.OnFormClosing(args);
        }

        protected override void OnMove(EventArgs args)
        {
            base.OnMove(args);
            this.settings?.MainWindow.ApplyLocation(this);
        }

        protected override void OnResizeEnd(EventArgs args)
        {
            base.OnResizeEnd(args);
            this.settings?.MainWindow.ApplyDimension(this);
        }

        #endregion

        #region Private Methods

        private void OnButtonExitClick(Object sender, EventArgs args)
        {
            Application.Exit();
        }

        private void OnButtonPlayClick(Object sender, EventArgs args)
        {
            try
            {
                this.tbbSave.Enabled = false;

                String source = this.source;

                if (String.IsNullOrWhiteSpace(source))
                {
                    source = System.IO.File.ReadAllText(this.txtFilename.Text);
                }

                GeneratorSettings settings = new GeneratorSettings
                {
                    RootClass = this.txtRootClass.Text,
                    Namespace = this.txtNamespace.Text,
                    IsAllInOne = this.chkAllInOne.Checked,
                    SourceType = (SourceType)this.cmbSourceType.SelectedItem,
                    TargetType = (TargetType)this.cmbTargetType.SelectedItem,
                    MemberType = (MemberType)this.cmbMemberType.SelectedItem,
                    AttributeType = (AttributeType)this.cmbAttributeType.SelectedItem
                };

                IEnumerable<ICode> codes = Generators.ModelGenerator.Generate(settings, source);

                this.tbcCode.TabPages.Clear();

                foreach (ICode code in codes)
                {
                    CodeTabPage page = new CodeTabPage(code);

                    this.tbcCode.Controls.Add(page);
                }
            }
            catch (Exception exception)
            {
                this.ShowError(exception);
                return;
            }
            finally
            {
                this.tbbSave.Enabled = this.tbcCode.TabPages.Count > 0;
            }
        }

        private void OnButtonSaveClick(Object sender, EventArgs args)
        {
            try
            {
                SaveDialog dialog = new SaveDialog
                {
                    Folder = this.settings.TargetSettings.Folder,
                    Overwrite = this.settings.TargetSettings.Overwrite
                };

                if (dialog.ShowDialog(this) != DialogResult.OK)
                {
                    return;
                }

                foreach (CodeTabPage current in this.tbcCode.TabPages.OfType<CodeTabPage>())
                {
                    current.Save(dialog.Folder, dialog.Overwrite);
                }

                this.settings.TargetSettings.Folder = dialog.Folder;
                this.settings.TargetSettings.Overwrite = dialog.Overwrite;
            }
            catch (Exception exception)
            {
                this.ShowError(exception);
            }
        }

        private void OnButtonInfoClick(Object sender, EventArgs args)
        {
            InfoDialog dialog = new InfoDialog();

            dialog.ShowDialog(this);
        }

        private void OnButtonLoadFileClick(Object sender, EventArgs args)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                CheckFileExists = true,
                Multiselect = false,
                RestoreDirectory = true,
                Filter = "All Files (*.*)|*.*|JSON Files (*.json)|*.json|XML Files (*.xml)|*.xml",
                FilterIndex = (Int32)this.cmbSourceType.SelectedItem + 1
            };

            if (dialog.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            this.txtFilename.Text = dialog.FileName;
            this.cmbSourceType.SelectedItem = (SourceType)(dialog.FilterIndex - 1);
            this.tbbPlay.Enabled = this.IsValidFilename;
            this.txtFilename.Enabled = this.IsValidFilename;

            this.source = String.Empty;
        }

        private void OnButtonCodeTextClick(Object sender, EventArgs args)
        {
            SourceDialog dialog = new SourceDialog(this.source, (SourceType)this.cmbSourceType.SelectedItem);

            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                this.source = dialog.Source;
                this.cmbSourceType.SelectedItem = dialog.Type;
            }

            this.txtFilename.Enabled = !this.IsValidSource;
            this.tbbPlay.Enabled = this.IsValidFilename || this.IsValidSource;
        }

        private void OnSourceTypeSelectedIndexChanged(Object sender, EventArgs args)
        {
            Boolean enabled = (SourceType)this.cmbSourceType.SelectedItem != SourceType.Xml;

            this.txtRootClass.Enabled = enabled;
            this.cmbAttributeType.Enabled = enabled;
        }

        private void ShowError(String message)
        {
            MessageBox.Show(this, message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ShowError(Exception exception)
        {
            this.ShowError(exception.Message);
        }

        private void OnFilenameTextChanged(Object sender, EventArgs args)
        {
            this.tbbPlay.Enabled = this.IsValidFilename || this.IsValidSource;
        }

        #endregion
    }
}
