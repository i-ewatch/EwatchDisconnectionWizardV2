
namespace EwatchDisconnectionWizardV2.Views.ChungHsinUserControl
{
    partial class DeviceConfigUserControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.SettingpanelControl = new DevExpress.XtraEditors.PanelControl();
            this.DeviceTypeEnumtextEdit = new DevExpress.XtraEditors.TextEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.ChangesimpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.AddsimpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.DeviceNametextEdit = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.DeleteCasesimpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.SearchsimpleButton = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SettingpanelControl)).BeginInit();
            this.SettingpanelControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DeviceTypeEnumtextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DeviceNametextEdit.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1263, 800);
            this.gridControl1.TabIndex = 3;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.DetailHeight = 400;
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            // 
            // SettingpanelControl
            // 
            this.SettingpanelControl.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.SettingpanelControl.Controls.Add(this.DeviceTypeEnumtextEdit);
            this.SettingpanelControl.Controls.Add(this.labelControl5);
            this.SettingpanelControl.Controls.Add(this.ChangesimpleButton);
            this.SettingpanelControl.Controls.Add(this.AddsimpleButton);
            this.SettingpanelControl.Controls.Add(this.DeviceNametextEdit);
            this.SettingpanelControl.Controls.Add(this.labelControl1);
            this.SettingpanelControl.Controls.Add(this.DeleteCasesimpleButton);
            this.SettingpanelControl.Controls.Add(this.SearchsimpleButton);
            this.SettingpanelControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SettingpanelControl.Location = new System.Drawing.Point(1263, 0);
            this.SettingpanelControl.Name = "SettingpanelControl";
            this.SettingpanelControl.Size = new System.Drawing.Size(453, 800);
            this.SettingpanelControl.TabIndex = 4;
            // 
            // DeviceTypeEnumtextEdit
            // 
            this.DeviceTypeEnumtextEdit.Enabled = false;
            this.DeviceTypeEnumtextEdit.Location = new System.Drawing.Point(138, 71);
            this.DeviceTypeEnumtextEdit.Name = "DeviceTypeEnumtextEdit";
            this.DeviceTypeEnumtextEdit.Properties.AllowFocused = false;
            this.DeviceTypeEnumtextEdit.Properties.Appearance.Font = new System.Drawing.Font("微軟正黑體", 18F);
            this.DeviceTypeEnumtextEdit.Properties.Appearance.Options.UseFont = true;
            this.DeviceTypeEnumtextEdit.Properties.Appearance.Options.UseTextOptions = true;
            this.DeviceTypeEnumtextEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.DeviceTypeEnumtextEdit.Properties.Mask.EditMask = "[0-9]*";
            this.DeviceTypeEnumtextEdit.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.DeviceTypeEnumtextEdit.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.DeviceTypeEnumtextEdit.Size = new System.Drawing.Size(300, 36);
            this.DeviceTypeEnumtextEdit.TabIndex = 24;
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("微軟正黑體", 20F);
            this.labelControl5.Appearance.Options.UseFont = true;
            this.labelControl5.Location = new System.Drawing.Point(18, 72);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(108, 34);
            this.labelControl5.TabIndex = 23;
            this.labelControl5.Text = "設備類型";
            // 
            // ChangesimpleButton
            // 
            this.ChangesimpleButton.AllowFocus = false;
            this.ChangesimpleButton.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.ChangesimpleButton.Appearance.Font = new System.Drawing.Font("微軟正黑體", 20F);
            this.ChangesimpleButton.Appearance.Options.UseBackColor = true;
            this.ChangesimpleButton.Appearance.Options.UseFont = true;
            this.ChangesimpleButton.Location = new System.Drawing.Point(157, 713);
            this.ChangesimpleButton.Name = "ChangesimpleButton";
            this.ChangesimpleButton.Size = new System.Drawing.Size(102, 40);
            this.ChangesimpleButton.TabIndex = 22;
            this.ChangesimpleButton.Text = "修改";
            this.ChangesimpleButton.Click += new System.EventHandler(this.ChangesimpleButton_Click);
            // 
            // AddsimpleButton
            // 
            this.AddsimpleButton.AllowFocus = false;
            this.AddsimpleButton.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.AddsimpleButton.Appearance.Font = new System.Drawing.Font("微軟正黑體", 20F);
            this.AddsimpleButton.Appearance.Options.UseBackColor = true;
            this.AddsimpleButton.Appearance.Options.UseFont = true;
            this.AddsimpleButton.Location = new System.Drawing.Point(18, 713);
            this.AddsimpleButton.Name = "AddsimpleButton";
            this.AddsimpleButton.Size = new System.Drawing.Size(102, 40);
            this.AddsimpleButton.TabIndex = 21;
            this.AddsimpleButton.Text = "新增";
            this.AddsimpleButton.Click += new System.EventHandler(this.AddsimpleButton_Click);
            // 
            // DeviceNametextEdit
            // 
            this.DeviceNametextEdit.Location = new System.Drawing.Point(138, 131);
            this.DeviceNametextEdit.Name = "DeviceNametextEdit";
            this.DeviceNametextEdit.Properties.AllowFocused = false;
            this.DeviceNametextEdit.Properties.Appearance.Font = new System.Drawing.Font("微軟正黑體", 18F);
            this.DeviceNametextEdit.Properties.Appearance.Options.UseFont = true;
            this.DeviceNametextEdit.Properties.Appearance.Options.UseTextOptions = true;
            this.DeviceNametextEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.DeviceNametextEdit.Properties.AutoHeight = false;
            this.DeviceNametextEdit.Size = new System.Drawing.Size(300, 36);
            this.DeviceNametextEdit.TabIndex = 12;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("微軟正黑體", 20F);
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(18, 132);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(108, 34);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "設備名稱";
            // 
            // DeleteCasesimpleButton
            // 
            this.DeleteCasesimpleButton.AllowFocus = false;
            this.DeleteCasesimpleButton.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.DeleteCasesimpleButton.Appearance.Font = new System.Drawing.Font("微軟正黑體", 20F);
            this.DeleteCasesimpleButton.Appearance.Options.UseBackColor = true;
            this.DeleteCasesimpleButton.Appearance.Options.UseFont = true;
            this.DeleteCasesimpleButton.Location = new System.Drawing.Point(296, 713);
            this.DeleteCasesimpleButton.Name = "DeleteCasesimpleButton";
            this.DeleteCasesimpleButton.Size = new System.Drawing.Size(142, 40);
            this.DeleteCasesimpleButton.TabIndex = 1;
            this.DeleteCasesimpleButton.Text = "刪除案場";
            this.DeleteCasesimpleButton.Click += new System.EventHandler(this.DeleteCasesimpleButton_Click);
            // 
            // SearchsimpleButton
            // 
            this.SearchsimpleButton.AllowFocus = false;
            this.SearchsimpleButton.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.SearchsimpleButton.Appearance.Font = new System.Drawing.Font("微軟正黑體", 20F);
            this.SearchsimpleButton.Appearance.Options.UseBackColor = true;
            this.SearchsimpleButton.Appearance.Options.UseFont = true;
            this.SearchsimpleButton.Location = new System.Drawing.Point(18, 12);
            this.SearchsimpleButton.Name = "SearchsimpleButton";
            this.SearchsimpleButton.Size = new System.Drawing.Size(420, 40);
            this.SearchsimpleButton.TabIndex = 0;
            this.SearchsimpleButton.Text = "查詢";
            this.SearchsimpleButton.Click += new System.EventHandler(this.SearchsimpleButton_Click);
            // 
            // DeviceConfigUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.SettingpanelControl);
            this.Controls.Add(this.gridControl1);
            this.Name = "DeviceConfigUserControl";
            this.Size = new System.Drawing.Size(1716, 800);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SettingpanelControl)).EndInit();
            this.SettingpanelControl.ResumeLayout(false);
            this.SettingpanelControl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DeviceTypeEnumtextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DeviceNametextEdit.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.PanelControl SettingpanelControl;
        private DevExpress.XtraEditors.TextEdit DeviceTypeEnumtextEdit;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.SimpleButton ChangesimpleButton;
        private DevExpress.XtraEditors.SimpleButton AddsimpleButton;
        private DevExpress.XtraEditors.TextEdit DeviceNametextEdit;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton DeleteCasesimpleButton;
        private DevExpress.XtraEditors.SimpleButton SearchsimpleButton;
    }
}
