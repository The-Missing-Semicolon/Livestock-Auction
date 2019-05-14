namespace Livestock_Auction
{
    partial class ucExhibitorCheckout
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
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabBuyers = new System.Windows.Forms.TabPage();
            this.tabReceipt = new System.Windows.Forms.TabPage();
            this.rptExhibitorReceipt = new Microsoft.Reporting.WinForms.ReportViewer();
            this.clsExhibitorBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.clsPurchaseBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tabMain.SuspendLayout();
            this.tabReceipt.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.clsExhibitorBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.clsPurchaseBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.tabBuyers);
            this.tabMain.Controls.Add(this.tabReceipt);
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Location = new System.Drawing.Point(0, 0);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(642, 564);
            this.tabMain.TabIndex = 0;
            this.tabMain.SelectedIndexChanged += new System.EventHandler(this.tabMain_SelectedIndexChanged);
            // 
            // tabBuyers
            // 
            this.tabBuyers.Location = new System.Drawing.Point(4, 22);
            this.tabBuyers.Name = "tabBuyers";
            this.tabBuyers.Padding = new System.Windows.Forms.Padding(3);
            this.tabBuyers.Size = new System.Drawing.Size(634, 538);
            this.tabBuyers.TabIndex = 0;
            this.tabBuyers.Text = "Buyers";
            this.tabBuyers.UseVisualStyleBackColor = true;
            // 
            // tabReceipt
            // 
            this.tabReceipt.Controls.Add(this.rptExhibitorReceipt);
            this.tabReceipt.Location = new System.Drawing.Point(4, 22);
            this.tabReceipt.Name = "tabReceipt";
            this.tabReceipt.Padding = new System.Windows.Forms.Padding(3);
            this.tabReceipt.Size = new System.Drawing.Size(634, 538);
            this.tabReceipt.TabIndex = 1;
            this.tabReceipt.Text = "Receipt";
            this.tabReceipt.UseVisualStyleBackColor = true;
            // 
            // rptExhibitorReceipt
            // 
            this.rptExhibitorReceipt.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "Livestock_Auction_clsExhibitor";
            reportDataSource1.Value = this.clsExhibitorBindingSource;
            reportDataSource2.Name = "Livestock_Auction_clsPurchase";
            reportDataSource2.Value = this.clsPurchaseBindingSource;
            this.rptExhibitorReceipt.LocalReport.DataSources.Add(reportDataSource1);
            this.rptExhibitorReceipt.LocalReport.DataSources.Add(reportDataSource2);
            this.rptExhibitorReceipt.LocalReport.ReportEmbeddedResource = "Livestock_Auction.Reports.rptExhibitorReceipt.rdlc";
            this.rptExhibitorReceipt.Location = new System.Drawing.Point(3, 3);
            this.rptExhibitorReceipt.Name = "rptExhibitorReceipt";
            this.rptExhibitorReceipt.Size = new System.Drawing.Size(628, 532);
            this.rptExhibitorReceipt.TabIndex = 0;
            // 
            // clsExhibitorBindingSource
            // 
            this.clsExhibitorBindingSource.DataSource = typeof(Livestock_Auction.DB.clsExhibitor);
            // 
            // clsPurchaseBindingSource
            // 
            this.clsPurchaseBindingSource.DataSource = typeof(Livestock_Auction.DB.clsPurchase);
            // 
            // ucExhibitorCheckout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabMain);
            this.Name = "ucExhibitorCheckout";
            this.Size = new System.Drawing.Size(642, 564);
            this.tabMain.ResumeLayout(false);
            this.tabReceipt.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.clsExhibitorBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.clsPurchaseBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tabBuyers;
        private System.Windows.Forms.TabPage tabReceipt;
        private Microsoft.Reporting.WinForms.ReportViewer rptExhibitorReceipt;
        private System.Windows.Forms.BindingSource clsExhibitorBindingSource;
        private System.Windows.Forms.BindingSource clsPurchaseBindingSource;
    }
}
