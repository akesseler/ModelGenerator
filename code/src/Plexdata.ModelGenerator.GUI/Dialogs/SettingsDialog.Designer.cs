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

namespace Plexdata.ModelGenerator.Gui.Dialogs
{
    partial class SettingsDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsDialog));
            this.btnApply = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.grpReplacements = new System.Windows.Forms.GroupBox();
            this.tabReplacements = new System.Windows.Forms.TabControl();
            this.pagJsonReplacements = new System.Windows.Forms.TabPage();
            this.lstJsonReplacements = new System.Windows.Forms.DataGridView();
            this.pagXmlReplacements = new System.Windows.Forms.TabPage();
            this.lstXmlReplacements = new System.Windows.Forms.DataGridView();
            this.grpAdjustment = new System.Windows.Forms.GroupBox();
            this.txtSuffix = new System.Windows.Forms.TextBox();
            this.txtPrefix = new System.Windows.Forms.TextBox();
            this.lblSuffix = new System.Windows.Forms.Label();
            this.lblPrefix = new System.Windows.Forms.Label();
            this.lblCasing = new System.Windows.Forms.Label();
            this.cmbCasing = new System.Windows.Forms.ComboBox();
            this.chkEnabled = new System.Windows.Forms.CheckBox();
            this.grpReplacements.SuspendLayout();
            this.tabReplacements.SuspendLayout();
            this.pagJsonReplacements.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lstJsonReplacements)).BeginInit();
            this.pagXmlReplacements.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lstXmlReplacements)).BeginInit();
            this.grpAdjustment.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnApply
            // 
            this.btnApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApply.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnApply.Location = new System.Drawing.Point(416, 426);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.TabIndex = 2;
            this.btnApply.Text = "&Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.OnButtonApplyClick);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(497, 426);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // grpReplacements
            // 
            this.grpReplacements.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpReplacements.Controls.Add(this.tabReplacements);
            this.grpReplacements.Location = new System.Drawing.Point(12, 139);
            this.grpReplacements.Name = "grpReplacements";
            this.grpReplacements.Size = new System.Drawing.Size(560, 281);
            this.grpReplacements.TabIndex = 1;
            this.grpReplacements.TabStop = false;
            this.grpReplacements.Text = "Replacements";
            // 
            // tabReplacements
            // 
            this.tabReplacements.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabReplacements.Controls.Add(this.pagJsonReplacements);
            this.tabReplacements.Controls.Add(this.pagXmlReplacements);
            this.tabReplacements.Location = new System.Drawing.Point(6, 19);
            this.tabReplacements.Name = "tabReplacements";
            this.tabReplacements.SelectedIndex = 0;
            this.tabReplacements.Size = new System.Drawing.Size(548, 256);
            this.tabReplacements.TabIndex = 0;
            // 
            // pagJsonReplacements
            // 
            this.pagJsonReplacements.Controls.Add(this.lstJsonReplacements);
            this.pagJsonReplacements.Location = new System.Drawing.Point(4, 22);
            this.pagJsonReplacements.Name = "pagJsonReplacements";
            this.pagJsonReplacements.Padding = new System.Windows.Forms.Padding(3);
            this.pagJsonReplacements.Size = new System.Drawing.Size(540, 230);
            this.pagJsonReplacements.TabIndex = 0;
            this.pagJsonReplacements.Text = "JSON";
            this.pagJsonReplacements.UseVisualStyleBackColor = true;
            // 
            // lstJsonReplacements
            // 
            this.lstJsonReplacements.BackgroundColor = System.Drawing.SystemColors.Window;
            this.lstJsonReplacements.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.lstJsonReplacements.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstJsonReplacements.Location = new System.Drawing.Point(3, 3);
            this.lstJsonReplacements.Name = "lstJsonReplacements";
            this.lstJsonReplacements.Size = new System.Drawing.Size(534, 224);
            this.lstJsonReplacements.TabIndex = 0;
            // 
            // pagXmlReplacements
            // 
            this.pagXmlReplacements.Controls.Add(this.lstXmlReplacements);
            this.pagXmlReplacements.Location = new System.Drawing.Point(4, 22);
            this.pagXmlReplacements.Name = "pagXmlReplacements";
            this.pagXmlReplacements.Padding = new System.Windows.Forms.Padding(3);
            this.pagXmlReplacements.Size = new System.Drawing.Size(540, 230);
            this.pagXmlReplacements.TabIndex = 1;
            this.pagXmlReplacements.Text = "XML";
            this.pagXmlReplacements.UseVisualStyleBackColor = true;
            // 
            // lstXmlReplacements
            // 
            this.lstXmlReplacements.BackgroundColor = System.Drawing.SystemColors.Window;
            this.lstXmlReplacements.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.lstXmlReplacements.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstXmlReplacements.Location = new System.Drawing.Point(3, 3);
            this.lstXmlReplacements.Name = "lstXmlReplacements";
            this.lstXmlReplacements.Size = new System.Drawing.Size(534, 224);
            this.lstXmlReplacements.TabIndex = 1;
            // 
            // grpAdjustment
            // 
            this.grpAdjustment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpAdjustment.Controls.Add(this.txtSuffix);
            this.grpAdjustment.Controls.Add(this.txtPrefix);
            this.grpAdjustment.Controls.Add(this.lblSuffix);
            this.grpAdjustment.Controls.Add(this.lblPrefix);
            this.grpAdjustment.Controls.Add(this.lblCasing);
            this.grpAdjustment.Controls.Add(this.cmbCasing);
            this.grpAdjustment.Controls.Add(this.chkEnabled);
            this.grpAdjustment.Location = new System.Drawing.Point(12, 12);
            this.grpAdjustment.Name = "grpAdjustment";
            this.grpAdjustment.Size = new System.Drawing.Size(560, 121);
            this.grpAdjustment.TabIndex = 0;
            this.grpAdjustment.TabStop = false;
            this.grpAdjustment.Text = "Adjustment";
            // 
            // txtSuffix
            // 
            this.txtSuffix.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSuffix.Location = new System.Drawing.Point(90, 95);
            this.txtSuffix.Name = "txtSuffix";
            this.txtSuffix.Size = new System.Drawing.Size(464, 20);
            this.txtSuffix.TabIndex = 6;
            // 
            // txtPrefix
            // 
            this.txtPrefix.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPrefix.Location = new System.Drawing.Point(90, 69);
            this.txtPrefix.Name = "txtPrefix";
            this.txtPrefix.Size = new System.Drawing.Size(464, 20);
            this.txtPrefix.TabIndex = 4;
            // 
            // lblSuffix
            // 
            this.lblSuffix.AutoSize = true;
            this.lblSuffix.Location = new System.Drawing.Point(6, 98);
            this.lblSuffix.Name = "lblSuffix";
            this.lblSuffix.Size = new System.Drawing.Size(77, 13);
            this.lblSuffix.TabIndex = 5;
            this.lblSuffix.Text = "Member Suffix:";
            // 
            // lblPrefix
            // 
            this.lblPrefix.AutoSize = true;
            this.lblPrefix.Location = new System.Drawing.Point(6, 72);
            this.lblPrefix.Name = "lblPrefix";
            this.lblPrefix.Size = new System.Drawing.Size(77, 13);
            this.lblPrefix.TabIndex = 3;
            this.lblPrefix.Text = "Member Prefix:";
            // 
            // lblCasing
            // 
            this.lblCasing.AutoSize = true;
            this.lblCasing.Location = new System.Drawing.Point(6, 45);
            this.lblCasing.Name = "lblCasing";
            this.lblCasing.Size = new System.Drawing.Size(77, 13);
            this.lblCasing.TabIndex = 1;
            this.lblCasing.Text = "Casing Format:";
            // 
            // cmbCasing
            // 
            this.cmbCasing.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbCasing.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCasing.FormattingEnabled = true;
            this.cmbCasing.Location = new System.Drawing.Point(90, 42);
            this.cmbCasing.Name = "cmbCasing";
            this.cmbCasing.Size = new System.Drawing.Size(464, 21);
            this.cmbCasing.TabIndex = 2;
            // 
            // chkEnabled
            // 
            this.chkEnabled.AutoSize = true;
            this.chkEnabled.Location = new System.Drawing.Point(6, 19);
            this.chkEnabled.Name = "chkEnabled";
            this.chkEnabled.Size = new System.Drawing.Size(196, 17);
            this.chkEnabled.TabIndex = 0;
            this.chkEnabled.Text = "Enable usage of adjustment feature.";
            this.chkEnabled.UseVisualStyleBackColor = true;
            // 
            // SettingsDialog
            // 
            this.AcceptButton = this.btnApply;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(584, 461);
            this.Controls.Add(this.grpAdjustment);
            this.Controls.Add(this.grpReplacements);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.btnCancel);
            this.DoubleBuffered = true;
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(600, 500);
            this.Name = "SettingsDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.HelpButtonClicked += new System.ComponentModel.CancelEventHandler(this.OnButtonHelpClick);
            this.grpReplacements.ResumeLayout(false);
            this.tabReplacements.ResumeLayout(false);
            this.pagJsonReplacements.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lstJsonReplacements)).EndInit();
            this.pagXmlReplacements.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lstXmlReplacements)).EndInit();
            this.grpAdjustment.ResumeLayout(false);
            this.grpAdjustment.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox grpReplacements;
        private System.Windows.Forms.TabControl tabReplacements;
        private System.Windows.Forms.TabPage pagJsonReplacements;
        private System.Windows.Forms.DataGridView lstJsonReplacements;
        private System.Windows.Forms.TabPage pagXmlReplacements;
        private System.Windows.Forms.DataGridView lstXmlReplacements;
        private System.Windows.Forms.GroupBox grpAdjustment;
        private System.Windows.Forms.ComboBox cmbCasing;
        private System.Windows.Forms.CheckBox chkEnabled;
        private System.Windows.Forms.Label lblCasing;
        private System.Windows.Forms.TextBox txtSuffix;
        private System.Windows.Forms.TextBox txtPrefix;
        private System.Windows.Forms.Label lblSuffix;
        private System.Windows.Forms.Label lblPrefix;
    }
}