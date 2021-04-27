
namespace EwatchDisconnectionWizardV2.Views.ChungHsinUserControl
{
    partial class ChumgHsinConnectionUserControl
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
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.ReceivextraTabPage = new DevExpress.XtraTab.XtraTabPage();
            this.AigridControl = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.ReceivextraTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AigridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Appearance.Font = new System.Drawing.Font("微軟正黑體", 20F);
            this.xtraTabControl1.Appearance.Options.UseFont = true;
            this.xtraTabControl1.AppearancePage.Header.Font = new System.Drawing.Font("微軟正黑體", 14F);
            this.xtraTabControl1.AppearancePage.Header.Options.UseFont = true;
            this.xtraTabControl1.AppearancePage.HeaderActive.Font = new System.Drawing.Font("微軟正黑體", 14F);
            this.xtraTabControl1.AppearancePage.HeaderActive.Options.UseFont = true;
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.Location = new System.Drawing.Point(0, 0);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.ReceivextraTabPage;
            this.xtraTabControl1.Size = new System.Drawing.Size(1716, 800);
            this.xtraTabControl1.TabIndex = 1;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.ReceivextraTabPage});
            // 
            // ReceivextraTabPage
            // 
            this.ReceivextraTabPage.Controls.Add(this.AigridControl);
            this.ReceivextraTabPage.Name = "ReceivextraTabPage";
            this.ReceivextraTabPage.Size = new System.Drawing.Size(1714, 764);
            this.ReceivextraTabPage.Text = "Receive連線狀況";
            // 
            // AigridControl
            // 
            this.AigridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AigridControl.Location = new System.Drawing.Point(0, 0);
            this.AigridControl.MainView = this.gridView1;
            this.AigridControl.Name = "AigridControl";
            this.AigridControl.Size = new System.Drawing.Size(1714, 764);
            this.AigridControl.TabIndex = 0;
            this.AigridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.AigridControl;
            this.gridView1.Name = "gridView1";
            // 
            // ChumgHsinConnectionUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.xtraTabControl1);
            this.Name = "ChumgHsinConnectionUserControl";
            this.Size = new System.Drawing.Size(1716, 800);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.ReceivextraTabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.AigridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage ReceivextraTabPage;
        private DevExpress.XtraGrid.GridControl AigridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
    }
}
