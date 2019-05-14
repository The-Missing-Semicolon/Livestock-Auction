namespace Livestock_Auction.Reports.Forms
{
    partial class frmrptReceipts
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmrptReceipts));
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource3 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource4 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource5 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tscmdBack = new System.Windows.Forms.ToolStripButton();
            this.tscmdNext = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tslblPersonNumber = new System.Windows.Forms.ToolStripLabel();
            this.tstxtGoTo = new System.Windows.Forms.ToolStripTextBox();
            this.tscmdGoto = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tscmdSaveAll = new System.Windows.Forms.ToolStripButton();
            this.rptExhibitorReceipt = new Microsoft.Reporting.WinForms.ReportViewer();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tslblReceiptStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.dlgFileSave = new System.Windows.Forms.SaveFileDialog();
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabPageExhibitors = new System.Windows.Forms.TabPage();
            this.tabPageBuyers = new System.Windows.Forms.TabPage();
            this.rptBuyerReceipt = new Microsoft.Reporting.WinForms.ReportViewer();
            this.clsExhibitorBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.clsPurchaseBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.clsBuyerBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.clsPaymentBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.tabMain.SuspendLayout();
            this.tabPageExhibitors.SuspendLayout();
            this.tabPageBuyers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.clsExhibitorBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.clsPurchaseBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.clsBuyerBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.clsPaymentBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tscmdBack,
            this.tscmdNext,
            this.toolStripSeparator1,
            this.tslblPersonNumber,
            this.tstxtGoTo,
            this.tscmdGoto,
            this.toolStripSeparator2,
            this.tscmdSaveAll});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1008, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tscmdBack
            // 
            this.tscmdBack.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tscmdBack.Image = ((System.Drawing.Image)(resources.GetObject("tscmdBack.Image")));
            this.tscmdBack.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tscmdBack.Name = "tscmdBack";
            this.tscmdBack.Size = new System.Drawing.Size(23, 22);
            this.tscmdBack.Text = "Previous Receipt";
            this.tscmdBack.Click += new System.EventHandler(this.tscmdBack_Click);
            // 
            // tscmdNext
            // 
            this.tscmdNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tscmdNext.Image = ((System.Drawing.Image)(resources.GetObject("tscmdNext.Image")));
            this.tscmdNext.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tscmdNext.Name = "tscmdNext";
            this.tscmdNext.Size = new System.Drawing.Size(23, 22);
            this.tscmdNext.Text = "Next Receipt";
            this.tscmdNext.Click += new System.EventHandler(this.tscmdNext_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tslblPersonNumber
            // 
            this.tslblPersonNumber.Name = "tslblPersonNumber";
            this.tslblPersonNumber.Size = new System.Drawing.Size(103, 22);
            this.tslblPersonNumber.Text = "Exhibitor Number:";
            // 
            // tstxtGoTo
            // 
            this.tstxtGoTo.Name = "tstxtGoTo";
            this.tstxtGoTo.Size = new System.Drawing.Size(100, 25);
            this.tstxtGoTo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tstxtGoTo_KeyDown);
            // 
            // tscmdGoto
            // 
            this.tscmdGoto.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tscmdGoto.Image = ((System.Drawing.Image)(resources.GetObject("tscmdGoto.Image")));
            this.tscmdGoto.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tscmdGoto.Name = "tscmdGoto";
            this.tscmdGoto.Size = new System.Drawing.Size(23, 22);
            this.tscmdGoto.Text = "Go to";
            this.tscmdGoto.Click += new System.EventHandler(this.tscmdGoto_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tscmdSaveAll
            // 
            this.tscmdSaveAll.Image = ((System.Drawing.Image)(resources.GetObject("tscmdSaveAll.Image")));
            this.tscmdSaveAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tscmdSaveAll.Name = "tscmdSaveAll";
            this.tscmdSaveAll.Size = new System.Drawing.Size(115, 22);
            this.tscmdSaveAll.Text = "Save All Receipts";
            this.tscmdSaveAll.Click += new System.EventHandler(this.tscmdSaveAll_Click);
            // 
            // rptMain
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
            this.rptExhibitorReceipt.Name = "rptMain";
            this.rptExhibitorReceipt.Size = new System.Drawing.Size(994, 579);
            this.rptExhibitorReceipt.TabIndex = 1;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslblReceiptStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 636);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1008, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tslblExhibitor
            // 
            this.tslblReceiptStatus.Name = "tslblExhibitor";
            this.tslblReceiptStatus.Size = new System.Drawing.Size(87, 17);
            this.tslblReceiptStatus.Text = "Exhibitor # of #";
            // 
            // dlgFileSave
            // 
            this.dlgFileSave.DefaultExt = "pdf";
            this.dlgFileSave.Filter = "PDF Files (*.pdf)|*.pdf";
            this.dlgFileSave.Title = "Save Receipts";
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.tabPageExhibitors);
            this.tabMain.Controls.Add(this.tabPageBuyers);
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Location = new System.Drawing.Point(0, 25);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(1008, 611);
            this.tabMain.TabIndex = 3;
            this.tabMain.SelectedIndexChanged += new System.EventHandler(this.tabMain_SelectedIndexChanged);
            // 
            // tabPageExhibitors
            // 
            this.tabPageExhibitors.Controls.Add(this.rptExhibitorReceipt);
            this.tabPageExhibitors.Location = new System.Drawing.Point(4, 22);
            this.tabPageExhibitors.Name = "tabPageExhibitors";
            this.tabPageExhibitors.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageExhibitors.Size = new System.Drawing.Size(1000, 585);
            this.tabPageExhibitors.TabIndex = 0;
            this.tabPageExhibitors.Text = "Exhibitor Receipts";
            this.tabPageExhibitors.UseVisualStyleBackColor = true;
            // 
            // tabPageBuyers
            // 
            this.tabPageBuyers.Controls.Add(this.rptBuyerReceipt);
            this.tabPageBuyers.Location = new System.Drawing.Point(4, 22);
            this.tabPageBuyers.Name = "tabPageBuyers";
            this.tabPageBuyers.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageBuyers.Size = new System.Drawing.Size(1000, 585);
            this.tabPageBuyers.TabIndex = 1;
            this.tabPageBuyers.Text = "Buyer Receipts";
            this.tabPageBuyers.UseVisualStyleBackColor = true;
            // 
            // rptBuyerReceipt
            // 
            this.rptBuyerReceipt.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource3.Name = "Livestock_Auction_clsBuyer";
            reportDataSource3.Value = this.clsBuyerBindingSource;
            reportDataSource4.Name = "Livestock_Auction_clsPurchase";
            reportDataSource4.Value = this.clsPurchaseBindingSource;
            reportDataSource5.Name = "Livestock_Auction_clsPayment";
            reportDataSource5.Value = this.clsPaymentBindingSource;
            this.rptBuyerReceipt.LocalReport.DataSources.Add(reportDataSource3);
            this.rptBuyerReceipt.LocalReport.DataSources.Add(reportDataSource4);
            this.rptBuyerReceipt.LocalReport.DataSources.Add(reportDataSource5);
            this.rptBuyerReceipt.LocalReport.ReportEmbeddedResource = "Livestock_Auction.Reports.rptBuyerReceipt.rdlc";
            this.rptBuyerReceipt.Location = new System.Drawing.Point(3, 3);
            this.rptBuyerReceipt.Name = "rptBuyerReceipt";
            this.rptBuyerReceipt.Size = new System.Drawing.Size(994, 579);
            this.rptBuyerReceipt.TabIndex = 2;
            // 
            // clsExhibitorBindingSource
            // 
            this.clsExhibitorBindingSource.DataSource = typeof(Livestock_Auction.DB.clsExhibitor);
            // 
            // clsPurchaseBindingSource
            // 
            this.clsPurchaseBindingSource.DataSource = typeof(Livestock_Auction.DB.clsPurchase);
            // 
            // clsBuyerBindingSource
            // 
            this.clsBuyerBindingSource.DataSource = typeof(Livestock_Auction.DB.clsBuyer);
            // 
            // clsPaymentBindingSource
            // 
            this.clsPaymentBindingSource.DataSource = typeof(Livestock_Auction.DB.clsPayment);
            // 
            // frmrptExhibiorReceipts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 658);
            this.Controls.Add(this.tabMain);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "frmrptExhibiorReceipts";
            this.Text = "Exhibitor Receipts";
            this.Load += new System.EventHandler(this.frmrptExhibiorReceipts_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tabMain.ResumeLayout(false);
            this.tabPageExhibitors.ResumeLayout(false);
            this.tabPageBuyers.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.clsExhibitorBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.clsPurchaseBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.clsBuyerBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.clsPaymentBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tscmdBack;
        private System.Windows.Forms.ToolStripButton tscmdNext;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripTextBox tstxtGoTo;
        private System.Windows.Forms.ToolStripButton tscmdGoto;
        private System.Windows.Forms.ToolStripButton tscmdSaveAll;
        private Microsoft.Reporting.WinForms.ReportViewer rptExhibitorReceipt;
        private System.Windows.Forms.BindingSource clsExhibitorBindingSource;
        private System.Windows.Forms.BindingSource clsPurchaseBindingSource;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tslblReceiptStatus;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.SaveFileDialog dlgFileSave;
        private System.Windows.Forms.ToolStripLabel tslblPersonNumber;
        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tabPageExhibitors;
        private System.Windows.Forms.TabPage tabPageBuyers;
        private Microsoft.Reporting.WinForms.ReportViewer rptBuyerReceipt;
        private System.Windows.Forms.BindingSource clsBuyerBindingSource;
        private System.Windows.Forms.BindingSource clsPaymentBindingSource;
    }
}