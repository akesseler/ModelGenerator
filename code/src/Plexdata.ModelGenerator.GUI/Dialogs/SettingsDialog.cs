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
using Plexdata.ModelGenerator.Gui.Extensions;
using Plexdata.ModelGenerator.Gui.Models;
using Plexdata.ModelGenerator.Gui.Properties;
using Plexdata.ModelGenerator.Gui.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace Plexdata.ModelGenerator.Gui.Dialogs
{
    public partial class SettingsDialog : Form
    {
        #region Private Fields

        private CreateSettings settings;

        #endregion

        #region Construction

        public SettingsDialog()
            : this(null)
        {
        }

        public SettingsDialog(CreateSettings settings)
        {
            this.InitializeComponent();

            this.Settings = settings;
        }

        #endregion

        #region Public Properties

        public CreateSettings Settings
        {
            get
            {
                return this.settings;
            }
            private set
            {
                if (value is null)
                {
                    value = new CreateSettings();
                }

                this.settings = (CreateSettings)value.Clone();
            }
        }

        #endregion

        #region Protected Events

        protected override void OnLoad(EventArgs args)
        {
            base.OnLoad(args);

            // TODO: Add more settings...

            this.chkEnabled.Checked = !this.settings.Adjustment.Ignore;
            this.cmbCasing.DataSource = Enum.GetValues(typeof(CasingType));
            this.cmbCasing.SelectedItem = (CasingType)this.settings.Adjustment.Casing;
            this.txtPrefix.Text = this.settings.Adjustment.Prefix;
            this.txtSuffix.Text = this.settings.Adjustment.Suffix;

            this.lstJsonReplacements.DataSource = this.CreateDataSource(this.settings.JsonReplacements);
            this.ApplyColumnWidth(this.lstJsonReplacements);

            this.lstXmlReplacements.DataSource = this.CreateDataSource(this.settings.XmlReplacements);
            this.ApplyColumnWidth(this.lstXmlReplacements);

        }

        #endregion

        #region Private Events

        private void OnButtonHelpClick(Object sender, CancelEventArgs args)
        {
            args.Cancel = true;
            this.ShowHtml("model-generator-settings-help", Resources.HtmlSettingsHelp);
        }

        private void OnButtonApplyClick(Object sender, EventArgs args)
        {
            // TODO: Add more settings...

            this.settings.Adjustment.Ignore = !this.chkEnabled.Checked;
            this.settings.Adjustment.Casing = (Int32)this.cmbCasing.SelectedItem;

            this.settings.Adjustment.Prefix = this.txtPrefix.Text;
            this.settings.Adjustment.Suffix = this.txtSuffix.Text;

            this.Settings.JsonReplacements = this.FilterDataSource(this.lstJsonReplacements.DataSource);
            this.Settings.XmlReplacements = this.FilterDataSource(this.lstXmlReplacements.DataSource);
        }

        #endregion

        #region Private Methods

        private void ApplyColumnWidth(DataGridView view)
        {
            foreach (DataGridViewColumn column in view.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        private Object CreateDataSource(Replacement[] replacements)
        {
            return new BindingList<Replacement>(new List<Replacement>(replacements));
        }

        private Replacement[] FilterDataSource(Object replacements)
        {
            List<Replacement> result = new List<Replacement>();

            foreach (Replacement current in (BindingList<Replacement>)replacements)
            {
                if (this.IsAccepted(current))
                {
                    result.Add(current);
                }
            }

            return result.ToArray();
        }

        private Boolean IsAccepted(Replacement replacement)
        {
            if (replacement is null)
            {
                return false;
            }

            if (String.IsNullOrWhiteSpace(replacement.Source))
            {
                return false;
            }

            if (String.IsNullOrWhiteSpace(replacement.Target))
            {
                replacement.Target = null;
            }

            return true;
        }

        #endregion
    }
}
