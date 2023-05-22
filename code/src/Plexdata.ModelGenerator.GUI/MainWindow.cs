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
using Plexdata.ModelGenerator.Gui.Controls;
using Plexdata.ModelGenerator.Gui.Dialogs;
using Plexdata.ModelGenerator.Gui.Extensions;
using Plexdata.ModelGenerator.Gui.Properties;
using Plexdata.ModelGenerator.Gui.Settings;
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

        #region Private Methods (Events)

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

                GeneratorSettings settings = new GeneratorSettings()
                {
                    RootClass = this.txtRootClass.Text,
                    Namespace = this.txtNamespace.Text,
                    IsAllInOne = this.chkAllInOne.Checked,
                    SourceType = (SourceType)this.cmbSourceType.SelectedItem,
                    TargetType = (TargetType)this.cmbTargetType.SelectedItem,
                    MemberType = (MemberType)this.cmbMemberType.SelectedItem,
                    AttributeType = (AttributeType)this.cmbAttributeType.SelectedItem
                };

                settings.Adjustment.Ignore = this.settings.CreateSettings.Adjustment.Ignore;
                settings.Adjustment.Casing = (CasingType)this.settings.CreateSettings.Adjustment.Casing;
                settings.Adjustment.Prefix = this.settings.CreateSettings.Adjustment.Prefix;
                settings.Adjustment.Suffix = this.settings.CreateSettings.Adjustment.Suffix;

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

        private void OnButtonCodeClick(Object sender, EventArgs args)
        {
            SourceDialog dialog = new SourceDialog((SourceType)this.cmbSourceType.SelectedItem, this.settings.CreateSettings);

            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                this.source = dialog.Source;
                this.cmbSourceType.SelectedItem = dialog.Type;
            }

            this.tbbPlay.Enabled = this.IsValidSource;

            if (this.tbbPlay.Enabled)
            {
                this.tbbPlay.PerformClick();
                this.source = String.Empty;
            }

            this.tbbPlay.Enabled = this.IsValidSource || this.IsValidFilename;
        }

        private void OnButtonSaveClick(Object sender, EventArgs args)
        {
            try
            {
                SaveDialog dialog = new SaveDialog()
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

        private void OnButtonSettingsClick(Object sender, EventArgs args)
        {
            SettingsDialog dialog = new SettingsDialog(this.settings.CreateSettings);

            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                this.settings.CreateSettings = dialog.Settings;
            }
        }

        private void OnButtonHelpClick(Object sender, EventArgs args)
        {
            this.ShowHtml("model-generator-general-help", Resources.HtmlGeneralHelp);
        }

        private void OnButtonInfoClick(Object sender, EventArgs args)
        {
            InfoDialog dialog = new InfoDialog();

            dialog.ShowDialog(this);
        }

        private void OnButtonLoadFileClick(Object sender, EventArgs args)
        {
            OpenFileDialog dialog = new OpenFileDialog()
            {
                CheckFileExists = true,
                Multiselect = false,
                RestoreDirectory = true,
                InitialDirectory = this.GetInitialDirectory(),
                Filter = "All Files (*.*)|*.*|JSON Files (*.json)|*.json|XML Files (*.xml)|*.xml",
                FilterIndex = (Int32)this.cmbSourceType.SelectedItem + 1
            };

            if (dialog.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            // BUG: Selected "SourceType" may remain "Unknown" if not chosen by user.
            this.txtFilename.Text = dialog.FileName;
            this.cmbSourceType.SelectedItem = (SourceType)(dialog.FilterIndex - 1);
            this.tbbPlay.Enabled = this.IsValidFilename;

            this.source = String.Empty;
        }

        private void OnSourceTypeSelectedIndexChanged(Object sender, EventArgs args)
        {
            Boolean enabled = (SourceType)this.cmbSourceType.SelectedItem != SourceType.Xml;

            this.txtRootClass.Enabled = enabled;
            this.cmbAttributeType.Enabled = enabled;
        }

        private void OnFilenameTextChanged(Object sender, EventArgs args)
        {
            this.tbbPlay.Enabled = this.IsValidFilename;
        }

        #endregion

        #region Private Methods (Others)

        private String GetInitialDirectory()
        {
            if (String.IsNullOrWhiteSpace(this.txtFilename.Text))
            {
                return String.Empty;
            }

            try
            {
                return Path.GetDirectoryName(this.txtFilename.Text);
            }
            catch
            {
                return String.Empty;
            }
        }

        #endregion
    }
}
