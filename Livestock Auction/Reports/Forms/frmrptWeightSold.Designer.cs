namespace Livestock_Auction.Reports.Forms
{
    partial class frmrptWeightSold
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource3 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.clsExhibitBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.clsSettingsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.clsExhibitBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.clsSettingsBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // clsExhibitBindingSource
            // 
            this.clsExhibitBindingSource.DataSource = typeof(Livestock_Auction.DB.clsExhibit);
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource2.Name = "Livestock_Auction_clsExhibit";
            reportDataSource2.Value = this.clsExhibitBindingSource;
            reportDataSource3.Name = "Livestock_Auction_clsSettings";
            reportDataSource3.Value = this.clsSettingsBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource3);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Livestock_Auction.Reports.rptWeightSold.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(594, 528);
            this.reportViewer1.TabIndex = 0;
            // 
            // clsSettingsBindingSource
            // 
            this.clsSettingsBindingSource.DataSource = typeof(Livestock_Auction.DB.clsSettings);
            // 
            // frmrptWeightSold
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(594, 528);
            this.Controls.Add(this.reportViewer1);
            this.Name = "frmrptWeightSold";
            this.Text = "frmrptWeightSold";
            this.Load += new System.EventHandler(this.frmrptWeightSold_Load);
            ((System.ComponentModel.ISupportInitialize)(this.clsExhibitBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.clsSettingsBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource clsExhibitBindingSource;
        private System.Windows.Forms.BindingSource clsSettingsBindingSource;
    }
}