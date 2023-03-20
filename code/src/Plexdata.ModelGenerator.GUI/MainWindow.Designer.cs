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

namespace Plexdata.ModelGenerator.Gui
{
    partial class MainWindow
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.tbcCode = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tbsMain = new System.Windows.Forms.ToolStrip();
            this.tbbExit = new System.Windows.Forms.ToolStripButton();
            this.tbbPlay = new System.Windows.Forms.ToolStripButton();
            this.tbbSave = new System.Windows.Forms.ToolStripButton();
            this.tbbInfo = new System.Windows.Forms.ToolStripButton();
            this.txtFilename = new System.Windows.Forms.TextBox();
            this.txtRootClass = new System.Windows.Forms.TextBox();
            this.txtNamespace = new System.Windows.Forms.TextBox();
            this.cmbSourceType = new System.Windows.Forms.ComboBox();
            this.cmbTargetType = new System.Windows.Forms.ComboBox();
            this.cmbMemberType = new System.Windows.Forms.ComboBox();
            this.cmbAttributeType = new System.Windows.Forms.ComboBox();
            this.chkAllInOne = new System.Windows.Forms.CheckBox();
            this.grpSettings = new System.Windows.Forms.GroupBox();
            this.tlpSettings = new System.Windows.Forms.TableLayoutPanel();
            this.btnCodeText = new System.Windows.Forms.Button();
            this.btnLoadFile = new System.Windows.Forms.Button();
            this.lblFilename = new System.Windows.Forms.Label();
            this.lblRootClass = new System.Windows.Forms.Label();
            this.lblNamespace = new System.Windows.Forms.Label();
            this.lblSourceType = new System.Windows.Forms.Label();
            this.lblTargetType = new System.Windows.Forms.Label();
            this.lblMemberType = new System.Windows.Forms.Label();
            this.lblAttributeType = new System.Windows.Forms.Label();
            this.tipMain = new System.Windows.Forms.ToolTip(this.components);
            this.tbcCode.SuspendLayout();
            this.tbsMain.SuspendLayout();
            this.grpSettings.SuspendLayout();
            this.tlpSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbcCode
            // 
            this.tbcCode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbcCode.Controls.Add(this.tabPage1);
            this.tbcCode.Controls.Add(this.tabPage2);
            this.tbcCode.Location = new System.Drawing.Point(12, 175);
            this.tbcCode.Name = "tbcCode";
            this.tbcCode.SelectedIndex = 0;
            this.tbcCode.ShowToolTips = true;
            this.tbcCode.Size = new System.Drawing.Size(620, 274);
            this.tbcCode.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(612, 248);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Dummy1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(612, 248);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Dummy2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tbsMain
            // 
            this.tbsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbbExit,
            this.tbbPlay,
            this.tbbSave,
            this.tbbInfo});
            this.tbsMain.Location = new System.Drawing.Point(0, 0);
            this.tbsMain.Name = "tbsMain";
            this.tbsMain.Size = new System.Drawing.Size(644, 39);
            this.tbsMain.TabIndex = 0;
            // 
            // tbbExit
            // 
            this.tbbExit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbbExit.Image = global::Plexdata.ModelGenerator.Gui.Properties.Resources.ExitLarge;
            this.tbbExit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tbbExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbbExit.Name = "tbbExit";
            this.tbbExit.Size = new System.Drawing.Size(36, 36);
            this.tbbExit.Text = "Exit";
            this.tbbExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbbExit.ToolTipText = "Close window and exit application.";
            this.tbbExit.Click += new System.EventHandler(this.OnButtonExitClick);
            // 
            // tbbPlay
            // 
            this.tbbPlay.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbbPlay.Image = global::Plexdata.ModelGenerator.Gui.Properties.Resources.PlayLarge;
            this.tbbPlay.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tbbPlay.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbbPlay.Name = "tbbPlay";
            this.tbbPlay.Size = new System.Drawing.Size(36, 36);
            this.tbbPlay.Text = "Play";
            this.tbbPlay.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbbPlay.ToolTipText = "Run generator with current settings.";
            this.tbbPlay.Click += new System.EventHandler(this.OnButtonPlayClick);
            // 
            // tbbSave
            // 
            this.tbbSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbbSave.Image = global::Plexdata.ModelGenerator.Gui.Properties.Resources.SaveLarge;
            this.tbbSave.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tbbSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbbSave.Name = "tbbSave";
            this.tbbSave.Size = new System.Drawing.Size(36, 36);
            this.tbbSave.Text = "Save";
            this.tbbSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbbSave.ToolTipText = "Save all generated files.";
            this.tbbSave.Click += new System.EventHandler(this.OnButtonSaveClick);
            // 
            // tbbInfo
            // 
            this.tbbInfo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbbInfo.Image = global::Plexdata.ModelGenerator.Gui.Properties.Resources.InfoLarge;
            this.tbbInfo.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tbbInfo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbbInfo.Name = "tbbInfo";
            this.tbbInfo.Size = new System.Drawing.Size(36, 36);
            this.tbbInfo.Text = "Info";
            this.tbbInfo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbbInfo.ToolTipText = "Show program info dialog.";
            this.tbbInfo.Click += new System.EventHandler(this.OnButtonInfoClick);
            // 
            // txtFilename
            // 
            this.txtFilename.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtFilename.Location = new System.Drawing.Point(103, 3);
            this.txtFilename.Name = "txtFilename";
            this.txtFilename.Size = new System.Drawing.Size(168, 20);
            this.txtFilename.TabIndex = 1;
            this.tipMain.SetToolTip(this.txtFilename, "Provide a source file name.");
            this.txtFilename.TextChanged += new System.EventHandler(this.OnFilenameTextChanged);
            // 
            // txtRootClass
            // 
            this.tlpSettings.SetColumnSpan(this.txtRootClass, 3);
            this.txtRootClass.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtRootClass.Location = new System.Drawing.Point(103, 28);
            this.txtRootClass.Name = "txtRootClass";
            this.txtRootClass.Size = new System.Drawing.Size(218, 20);
            this.txtRootClass.TabIndex = 5;
            this.tipMain.SetToolTip(this.txtRootClass, "Choose a root class name.");
            // 
            // txtNamespace
            // 
            this.tlpSettings.SetColumnSpan(this.txtNamespace, 3);
            this.txtNamespace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtNamespace.Location = new System.Drawing.Point(103, 53);
            this.txtNamespace.Name = "txtNamespace";
            this.txtNamespace.Size = new System.Drawing.Size(218, 20);
            this.txtNamespace.TabIndex = 7;
            this.tipMain.SetToolTip(this.txtNamespace, "Choose a root namespace.");
            // 
            // cmbSourceType
            // 
            this.cmbSourceType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbSourceType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSourceType.FormattingEnabled = true;
            this.cmbSourceType.Location = new System.Drawing.Point(437, 3);
            this.cmbSourceType.Name = "cmbSourceType";
            this.cmbSourceType.Size = new System.Drawing.Size(168, 21);
            this.cmbSourceType.TabIndex = 10;
            this.tipMain.SetToolTip(this.cmbSourceType, "Select source type.");
            this.cmbSourceType.SelectedIndexChanged += new System.EventHandler(this.OnSourceTypeSelectedIndexChanged);
            // 
            // cmbTargetType
            // 
            this.cmbTargetType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbTargetType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTargetType.FormattingEnabled = true;
            this.cmbTargetType.Location = new System.Drawing.Point(437, 28);
            this.cmbTargetType.Name = "cmbTargetType";
            this.cmbTargetType.Size = new System.Drawing.Size(168, 21);
            this.cmbTargetType.TabIndex = 12;
            this.tipMain.SetToolTip(this.cmbTargetType, "Select target type.");
            // 
            // cmbMemberType
            // 
            this.cmbMemberType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbMemberType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMemberType.FormattingEnabled = true;
            this.cmbMemberType.Location = new System.Drawing.Point(437, 53);
            this.cmbMemberType.Name = "cmbMemberType";
            this.cmbMemberType.Size = new System.Drawing.Size(168, 21);
            this.cmbMemberType.TabIndex = 14;
            this.tipMain.SetToolTip(this.cmbMemberType, "Select member type.");
            // 
            // cmbAttributeType
            // 
            this.cmbAttributeType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbAttributeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAttributeType.FormattingEnabled = true;
            this.cmbAttributeType.Location = new System.Drawing.Point(437, 78);
            this.cmbAttributeType.Name = "cmbAttributeType";
            this.cmbAttributeType.Size = new System.Drawing.Size(168, 21);
            this.cmbAttributeType.TabIndex = 16;
            this.tipMain.SetToolTip(this.cmbAttributeType, "Select attribute type.");
            // 
            // chkAllInOne
            // 
            this.chkAllInOne.AutoSize = true;
            this.tlpSettings.SetColumnSpan(this.chkAllInOne, 3);
            this.chkAllInOne.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkAllInOne.Location = new System.Drawing.Point(103, 78);
            this.chkAllInOne.MaximumSize = new System.Drawing.Size(0, 17);
            this.chkAllInOne.Name = "chkAllInOne";
            this.chkAllInOne.Size = new System.Drawing.Size(218, 17);
            this.chkAllInOne.TabIndex = 8;
            this.chkAllInOne.Text = "Put all result classes into one output file.";
            this.chkAllInOne.UseVisualStyleBackColor = true;
            // 
            // grpSettings
            // 
            this.grpSettings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpSettings.Controls.Add(this.tlpSettings);
            this.grpSettings.Location = new System.Drawing.Point(12, 42);
            this.grpSettings.Name = "grpSettings";
            this.grpSettings.Size = new System.Drawing.Size(620, 127);
            this.grpSettings.TabIndex = 1;
            this.grpSettings.TabStop = false;
            this.grpSettings.Text = "&Settings";
            // 
            // tlpSettings
            // 
            this.tlpSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tlpSettings.ColumnCount = 7;
            this.tlpSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tlpSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tlpSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tlpSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpSettings.Controls.Add(this.btnCodeText, 3, 0);
            this.tlpSettings.Controls.Add(this.cmbAttributeType, 6, 3);
            this.tlpSettings.Controls.Add(this.btnLoadFile, 2, 0);
            this.tlpSettings.Controls.Add(this.chkAllInOne, 1, 3);
            this.tlpSettings.Controls.Add(this.cmbMemberType, 6, 2);
            this.tlpSettings.Controls.Add(this.lblFilename, 0, 0);
            this.tlpSettings.Controls.Add(this.cmbTargetType, 6, 1);
            this.tlpSettings.Controls.Add(this.txtFilename, 1, 0);
            this.tlpSettings.Controls.Add(this.cmbSourceType, 6, 0);
            this.tlpSettings.Controls.Add(this.lblRootClass, 0, 1);
            this.tlpSettings.Controls.Add(this.txtNamespace, 1, 2);
            this.tlpSettings.Controls.Add(this.txtRootClass, 1, 1);
            this.tlpSettings.Controls.Add(this.lblNamespace, 0, 2);
            this.tlpSettings.Controls.Add(this.lblSourceType, 5, 0);
            this.tlpSettings.Controls.Add(this.lblTargetType, 5, 1);
            this.tlpSettings.Controls.Add(this.lblMemberType, 5, 2);
            this.tlpSettings.Controls.Add(this.lblAttributeType, 5, 3);
            this.tlpSettings.Location = new System.Drawing.Point(6, 19);
            this.tlpSettings.Name = "tlpSettings";
            this.tlpSettings.RowCount = 4;
            this.tlpSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tlpSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tlpSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tlpSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tlpSettings.Size = new System.Drawing.Size(608, 102);
            this.tlpSettings.TabIndex = 0;
            // 
            // btnCodeText
            // 
            this.btnCodeText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCodeText.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnCodeText.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Wheat;
            this.btnCodeText.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCodeText.Image = global::Plexdata.ModelGenerator.Gui.Properties.Resources.ButtonCodeSmall;
            this.btnCodeText.Location = new System.Drawing.Point(302, 3);
            this.btnCodeText.Name = "btnCodeText";
            this.btnCodeText.Size = new System.Drawing.Size(19, 19);
            this.btnCodeText.TabIndex = 3;
            this.tipMain.SetToolTip(this.btnCodeText, "Provide an alternative source.");
            this.btnCodeText.UseVisualStyleBackColor = true;
            this.btnCodeText.Click += new System.EventHandler(this.OnButtonCodeTextClick);
            // 
            // btnLoadFile
            // 
            this.btnLoadFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnLoadFile.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnLoadFile.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Wheat;
            this.btnLoadFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoadFile.Image = global::Plexdata.ModelGenerator.Gui.Properties.Resources.ButtonDotsSmall;
            this.btnLoadFile.Location = new System.Drawing.Point(277, 3);
            this.btnLoadFile.Name = "btnLoadFile";
            this.btnLoadFile.Size = new System.Drawing.Size(19, 19);
            this.btnLoadFile.TabIndex = 2;
            this.tipMain.SetToolTip(this.btnLoadFile, "Choose a source file.");
            this.btnLoadFile.UseVisualStyleBackColor = true;
            this.btnLoadFile.Click += new System.EventHandler(this.OnButtonLoadFileClick);
            // 
            // lblFilename
            // 
            this.lblFilename.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFilename.Location = new System.Drawing.Point(3, 3);
            this.lblFilename.Margin = new System.Windows.Forms.Padding(3);
            this.lblFilename.Name = "lblFilename";
            this.lblFilename.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.lblFilename.Size = new System.Drawing.Size(94, 19);
            this.lblFilename.TabIndex = 0;
            this.lblFilename.Text = "&File Name:";
            // 
            // lblRootClass
            // 
            this.lblRootClass.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRootClass.Location = new System.Drawing.Point(3, 28);
            this.lblRootClass.Margin = new System.Windows.Forms.Padding(3);
            this.lblRootClass.Name = "lblRootClass";
            this.lblRootClass.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.lblRootClass.Size = new System.Drawing.Size(94, 19);
            this.lblRootClass.TabIndex = 4;
            this.lblRootClass.Text = "Root &Class:";
            // 
            // lblNamespace
            // 
            this.lblNamespace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNamespace.Location = new System.Drawing.Point(3, 53);
            this.lblNamespace.Margin = new System.Windows.Forms.Padding(3);
            this.lblNamespace.Name = "lblNamespace";
            this.lblNamespace.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.lblNamespace.Size = new System.Drawing.Size(94, 19);
            this.lblNamespace.TabIndex = 6;
            this.lblNamespace.Text = "Root &Namespace:";
            // 
            // lblSourceType
            // 
            this.lblSourceType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSourceType.Location = new System.Drawing.Point(337, 3);
            this.lblSourceType.Margin = new System.Windows.Forms.Padding(3);
            this.lblSourceType.Name = "lblSourceType";
            this.lblSourceType.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.lblSourceType.Size = new System.Drawing.Size(94, 19);
            this.lblSourceType.TabIndex = 9;
            this.lblSourceType.Text = "&Source Type:";
            // 
            // lblTargetType
            // 
            this.lblTargetType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTargetType.Location = new System.Drawing.Point(337, 28);
            this.lblTargetType.Margin = new System.Windows.Forms.Padding(3);
            this.lblTargetType.Name = "lblTargetType";
            this.lblTargetType.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.lblTargetType.Size = new System.Drawing.Size(94, 19);
            this.lblTargetType.TabIndex = 11;
            this.lblTargetType.Text = "&Target Type:";
            // 
            // lblMemberType
            // 
            this.lblMemberType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMemberType.Location = new System.Drawing.Point(337, 53);
            this.lblMemberType.Margin = new System.Windows.Forms.Padding(3);
            this.lblMemberType.Name = "lblMemberType";
            this.lblMemberType.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.lblMemberType.Size = new System.Drawing.Size(94, 19);
            this.lblMemberType.TabIndex = 13;
            this.lblMemberType.Text = "&Member Type:";
            // 
            // lblAttributeType
            // 
            this.lblAttributeType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAttributeType.Location = new System.Drawing.Point(337, 78);
            this.lblAttributeType.Margin = new System.Windows.Forms.Padding(3);
            this.lblAttributeType.Name = "lblAttributeType";
            this.lblAttributeType.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.lblAttributeType.Size = new System.Drawing.Size(94, 21);
            this.lblAttributeType.TabIndex = 15;
            this.lblAttributeType.Text = "&Attribute Type:";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(644, 461);
            this.Controls.Add(this.grpSettings);
            this.Controls.Add(this.tbsMain);
            this.Controls.Add(this.tbcCode);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(660, 500);
            this.Name = "MainWindow";
            this.Text = "Model Generator";
            this.tbcCode.ResumeLayout(false);
            this.tbsMain.ResumeLayout(false);
            this.tbsMain.PerformLayout();
            this.grpSettings.ResumeLayout(false);
            this.tlpSettings.ResumeLayout(false);
            this.tlpSettings.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tbcCode;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ToolStrip tbsMain;
        private System.Windows.Forms.ToolStripButton tbbExit;
        private System.Windows.Forms.ToolStripButton tbbPlay;
        private System.Windows.Forms.TextBox txtFilename;
        private System.Windows.Forms.TextBox txtRootClass;
        private System.Windows.Forms.TextBox txtNamespace;
        private System.Windows.Forms.ComboBox cmbSourceType;
        private System.Windows.Forms.ComboBox cmbTargetType;
        private System.Windows.Forms.ComboBox cmbMemberType;
        private System.Windows.Forms.ComboBox cmbAttributeType;
        private System.Windows.Forms.Button btnLoadFile;
        private System.Windows.Forms.Button btnCodeText;
        private System.Windows.Forms.ToolStripButton tbbSave;
        private System.Windows.Forms.CheckBox chkAllInOne;
        private System.Windows.Forms.GroupBox grpSettings;
        private System.Windows.Forms.Label lblFilename;
        private System.Windows.Forms.TableLayoutPanel tlpSettings;
        private System.Windows.Forms.Label lblRootClass;
        private System.Windows.Forms.Label lblNamespace;
        private System.Windows.Forms.Label lblSourceType;
        private System.Windows.Forms.Label lblTargetType;
        private System.Windows.Forms.Label lblMemberType;
        private System.Windows.Forms.Label lblAttributeType;
        private System.Windows.Forms.ToolTip tipMain;
        private System.Windows.Forms.ToolStripButton tbbInfo;
    }
}

