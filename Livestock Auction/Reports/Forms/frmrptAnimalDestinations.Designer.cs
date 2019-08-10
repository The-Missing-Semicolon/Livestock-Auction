namespace Livestock_Auction.Reports.Forms
{
    partial class frmrptAnimalDestinations
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.clsExhibitBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.rptAnimalDestinations = new Microsoft.Reporting.WinForms.ReportViewer();
            this.clsSettingsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.clsExhibitBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.clsSettingsBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // clsExhibitBindingSource
            // 
            this.clsExhibitBindingSource.DataSource = typeof(Livestock_Auction.DB.clsExhibit);
            // 
            // rptAnimalDestinations
            // 
            this.rptAnimalDestinations.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "Livestock_Auction_clsExhibit";
            reportDataSource1.Value = this.clsExhibitBindingSource;
            reportDataSource2.Name = "Livestock_Auction_clsSettings";
            reportDataSource2.Value = this.clsSettingsBindingSource;
            this.rptAnimalDestinations.LocalReport.DataSources.Add(reportDataSource1);
            this.rptAnimalDestinations.LocalReport.DataSources.Add(reportDataSource2);
            this.rptAnimalDestinations.LocalReport.ReportEmbeddedResource = "Livestock_Auction.Reports.rptAnimalDestinations.rdlc";
            this.rptAnimalDestinations.Location = new System.Drawing.Point(0, 0);
            this.rptAnimalDestinations.Name = "rptAnimalDestinations";
            this.rptAnimalDestinations.Size = new System.Drawing.Size(607, 491);
            this.rptAnimalDestinations.TabIndex = 0;
            // 
            // clsSettingsBindingSource
            // 
            this.clsSettingsBindingSource.DataSource = typeof(Livestock_Auction.DB.clsSettings);
            // 
            // frmrptAnimalDestinations
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(607, 491);
            this.Controls.Add(this.rptAnimalDestinations);
            this.Name = "frmrptAnimalDestinations";
            this.Text = "Animal Destinations";
            this.Load += new System.EventHandler(this.frmrptAnimalDestinations_Load);
            ((System.ComponentModel.ISupportInitialize)(this.clsExhibitBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.clsSettingsBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer rptAnimalDestinations;
        private System.Windows.Forms.BindingSource clsExhibitBindingSource;
        private System.Windows.Forms.BindingSource clsSettingsBindingSource;
    }
}