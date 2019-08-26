namespace Livestock_Auction
{
    partial class frmAuctionOrder
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAuctionOrder));
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabpgEdit = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lsvOrder = new System.Windows.Forms.ListView();
            this.colOrder = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colTagNumber = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colExhibitorNumber = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colExhibitorName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colExhibitChampion = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colExhibitRateOfGain = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colExhibitType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colExhibitWeight = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lsvUnsorted = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this.tsEdit = new System.Windows.Forms.ToolStrip();
            this.tscmdGenerateOrder = new System.Windows.Forms.ToolStripButton();
            this.tabpgReport = new System.Windows.Forms.TabPage();
            this.rptAuctionOrder = new Microsoft.Reporting.WinForms.ReportViewer();
            this.lsvHistory = new System.Windows.Forms.ListView();
            this.colRevisionIndex = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colRevisionDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colRecordsAffected = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colReverted = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panHistory = new System.Windows.Forms.Panel();
            this.tsHistory = new System.Windows.Forms.ToolStrip();
            this.tscmdRevert = new System.Windows.Forms.ToolStripButton();
            this.panMain = new System.Windows.Forms.Panel();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.tsCmbOrderType = new System.Windows.Forms.ToolStripComboBox();
            this.clsAuctionIndexBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tabMain.SuspendLayout();
            this.tabpgEdit.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tsEdit.SuspendLayout();
            this.tabpgReport.SuspendLayout();
            this.panHistory.SuspendLayout();
            this.tsHistory.SuspendLayout();
            this.panMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.clsAuctionIndexBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.tabpgEdit);
            this.tabMain.Controls.Add(this.tabpgReport);
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Location = new System.Drawing.Point(0, 0);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(594, 749);
            this.tabMain.TabIndex = 0;
            this.tabMain.SelectedIndexChanged += new System.EventHandler(this.tabMain_SelectedIndexChanged);
            // 
            // tabpgEdit
            // 
            this.tabpgEdit.Controls.Add(this.splitContainer1);
            this.tabpgEdit.Controls.Add(this.tsEdit);
            this.tabpgEdit.Location = new System.Drawing.Point(4, 22);
            this.tabpgEdit.Name = "tabpgEdit";
            this.tabpgEdit.Padding = new System.Windows.Forms.Padding(3);
            this.tabpgEdit.Size = new System.Drawing.Size(586, 723);
            this.tabpgEdit.TabIndex = 0;
            this.tabpgEdit.Text = "Edit";
            this.tabpgEdit.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 28);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Size = new System.Drawing.Size(580, 692);
            this.splitContainer1.SplitterDistance = 346;
            this.splitContainer1.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lsvOrder);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(580, 346);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Current Auction Order";
            // 
            // lsvOrder
            // 
            this.lsvOrder.AllowDrop = true;
            this.lsvOrder.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colOrder,
            this.colTagNumber,
            this.colExhibitorNumber,
            this.colExhibitorName,
            this.colExhibitChampion,
            this.colExhibitRateOfGain,
            this.colExhibitType,
            this.colExhibitWeight});
            this.lsvOrder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lsvOrder.FullRowSelect = true;
            this.lsvOrder.HideSelection = false;
            this.lsvOrder.Location = new System.Drawing.Point(3, 16);
            this.lsvOrder.Name = "lsvOrder";
            this.lsvOrder.Size = new System.Drawing.Size(574, 327);
            this.lsvOrder.TabIndex = 0;
            this.lsvOrder.UseCompatibleStateImageBehavior = false;
            this.lsvOrder.View = System.Windows.Forms.View.Details;
            this.lsvOrder.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.lsvOrder_ItemDrag);
            this.lsvOrder.DragDrop += new System.Windows.Forms.DragEventHandler(this.lsvOrder_DragDrop);
            this.lsvOrder.DragEnter += new System.Windows.Forms.DragEventHandler(this.lsvOrder_DragEnter);
            this.lsvOrder.DragOver += new System.Windows.Forms.DragEventHandler(this.lsvOrder_DragOver);
            this.lsvOrder.DragLeave += new System.EventHandler(this.lsvOrder_DragLeave);
            // 
            // colOrder
            // 
            this.colOrder.Text = "Order";
            // 
            // colTagNumber
            // 
            this.colTagNumber.Text = "Tag #";
            this.colTagNumber.Width = 70;
            // 
            // colExhibitorNumber
            // 
            this.colExhibitorNumber.Text = "Exhibitor #";
            this.colExhibitorNumber.Width = 82;
            // 
            // colExhibitorName
            // 
            this.colExhibitorName.Text = "Exhibitor Name";
            this.colExhibitorName.Width = 134;
            // 
            // colExhibitChampion
            // 
            this.colExhibitChampion.Text = "Champion Status";
            this.colExhibitChampion.Width = 94;
            // 
            // colExhibitRateOfGain
            // 
            this.colExhibitRateOfGain.Text = "Rate Of Gain";
            this.colExhibitRateOfGain.Width = 87;
            // 
            // colExhibitType
            // 
            this.colExhibitType.Text = "Exhibit Type";
            this.colExhibitType.Width = 79;
            // 
            // colExhibitWeight
            // 
            this.colExhibitWeight.Text = "Exhibit Weight";
            this.colExhibitWeight.Width = 93;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lsvUnsorted);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(580, 342);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Unsorted Exhibits";
            // 
            // lsvUnsorted
            // 
            this.lsvUnsorted.AllowDrop = true;
            this.lsvUnsorted.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader1,
            this.columnHeader6,
            this.columnHeader7});
            this.lsvUnsorted.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lsvUnsorted.FullRowSelect = true;
            this.lsvUnsorted.HideSelection = false;
            this.lsvUnsorted.Location = new System.Drawing.Point(3, 16);
            this.lsvUnsorted.MultiSelect = false;
            this.lsvUnsorted.Name = "lsvUnsorted";
            this.lsvUnsorted.Size = new System.Drawing.Size(574, 295);
            this.lsvUnsorted.TabIndex = 1;
            this.lsvUnsorted.UseCompatibleStateImageBehavior = false;
            this.lsvUnsorted.View = System.Windows.Forms.View.Details;
            this.lsvUnsorted.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.lsvUnsorted_ItemDrag);
            this.lsvUnsorted.DragDrop += new System.Windows.Forms.DragEventHandler(this.lsvUnsorted_DragDrop);
            this.lsvUnsorted.DragEnter += new System.Windows.Forms.DragEventHandler(this.lsvUnsorted_DragEnter);
            this.lsvUnsorted.DragOver += new System.Windows.Forms.DragEventHandler(this.lsvUnsorted_DragOver);
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Tag #";
            this.columnHeader2.Width = 70;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Exhibitor #";
            this.columnHeader3.Width = 82;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Exhibitor Name";
            this.columnHeader4.Width = 134;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Champion Status";
            this.columnHeader5.Width = 94;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Rate Of Gain";
            this.columnHeader1.Width = 79;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Exhibit Type";
            this.columnHeader6.Width = 79;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Exhibit Weight";
            this.columnHeader7.Width = 93;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label1.Location = new System.Drawing.Point(3, 311);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(574, 28);
            this.label1.TabIndex = 2;
            this.label1.Text = "Add items to the auction order by draging them from the unsorted exhibits to the " +
    "auction order. Remove them by dragging them from the auction order to the unsort" +
    "ed items.";
            // 
            // tsEdit
            // 
            this.tsEdit.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tscmdGenerateOrder,
            this.tsCmbOrderType});
            this.tsEdit.Location = new System.Drawing.Point(3, 3);
            this.tsEdit.Name = "tsEdit";
            this.tsEdit.Size = new System.Drawing.Size(580, 25);
            this.tsEdit.TabIndex = 1;
            this.tsEdit.Text = "toolStrip1";
            // 
            // tscmdGenerateOrder
            // 
            this.tscmdGenerateOrder.Image = ((System.Drawing.Image)(resources.GetObject("tscmdGenerateOrder.Image")));
            this.tscmdGenerateOrder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tscmdGenerateOrder.Name = "tscmdGenerateOrder";
            this.tscmdGenerateOrder.Size = new System.Drawing.Size(164, 22);
            this.tscmdGenerateOrder.Text = "Regenerate Auction Order";
            this.tscmdGenerateOrder.Click += new System.EventHandler(this.tscmdGenerateOrder_Click);
            // 
            // tabpgReport
            // 
            this.tabpgReport.Controls.Add(this.rptAuctionOrder);
            this.tabpgReport.Location = new System.Drawing.Point(4, 22);
            this.tabpgReport.Name = "tabpgReport";
            this.tabpgReport.Padding = new System.Windows.Forms.Padding(3);
            this.tabpgReport.Size = new System.Drawing.Size(586, 723);
            this.tabpgReport.TabIndex = 1;
            this.tabpgReport.Text = "Report";
            this.tabpgReport.UseVisualStyleBackColor = true;
            // 
            // rptAuctionOrder
            // 
            this.rptAuctionOrder.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "Livestock_Auction_clsAuctionIndex";
            reportDataSource1.Value = this.clsAuctionIndexBindingSource;
            this.rptAuctionOrder.LocalReport.DataSources.Add(reportDataSource1);
            this.rptAuctionOrder.LocalReport.ReportEmbeddedResource = "Livestock_Auction.Reports.rptAuctionOrder.rdlc";
            this.rptAuctionOrder.Location = new System.Drawing.Point(3, 3);
            this.rptAuctionOrder.Name = "rptAuctionOrder";
            this.rptAuctionOrder.Size = new System.Drawing.Size(580, 717);
            this.rptAuctionOrder.TabIndex = 0;
            // 
            // lsvHistory
            // 
            this.lsvHistory.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colRevisionIndex,
            this.colRevisionDate,
            this.colRecordsAffected,
            this.colReverted});
            this.lsvHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lsvHistory.FullRowSelect = true;
            this.lsvHistory.HideSelection = false;
            this.lsvHistory.Location = new System.Drawing.Point(0, 25);
            this.lsvHistory.MultiSelect = false;
            this.lsvHistory.Name = "lsvHistory";
            this.lsvHistory.Size = new System.Drawing.Size(387, 724);
            this.lsvHistory.TabIndex = 1;
            this.lsvHistory.UseCompatibleStateImageBehavior = false;
            this.lsvHistory.View = System.Windows.Forms.View.Details;
            this.lsvHistory.SelectedIndexChanged += new System.EventHandler(this.lsvHistory_SelectedIndexChanged);
            // 
            // colRevisionIndex
            // 
            this.colRevisionIndex.Text = "Revision Number";
            this.colRevisionIndex.Width = 94;
            // 
            // colRevisionDate
            // 
            this.colRevisionDate.Text = "Revision Date";
            this.colRevisionDate.Width = 175;
            // 
            // colRecordsAffected
            // 
            this.colRecordsAffected.Text = "Records Affected";
            this.colRecordsAffected.Width = 106;
            // 
            // colReverted
            // 
            this.colReverted.Text = "Reverted";
            // 
            // panHistory
            // 
            this.panHistory.Controls.Add(this.lsvHistory);
            this.panHistory.Controls.Add(this.tsHistory);
            this.panHistory.Dock = System.Windows.Forms.DockStyle.Right;
            this.panHistory.Location = new System.Drawing.Point(597, 0);
            this.panHistory.Name = "panHistory";
            this.panHistory.Size = new System.Drawing.Size(387, 749);
            this.panHistory.TabIndex = 2;
            // 
            // tsHistory
            // 
            this.tsHistory.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tscmdRevert});
            this.tsHistory.Location = new System.Drawing.Point(0, 0);
            this.tsHistory.Name = "tsHistory";
            this.tsHistory.Size = new System.Drawing.Size(387, 25);
            this.tsHistory.TabIndex = 2;
            this.tsHistory.Text = "toolStrip1";
            // 
            // tscmdRevert
            // 
            this.tscmdRevert.Enabled = false;
            this.tscmdRevert.Image = ((System.Drawing.Image)(resources.GetObject("tscmdRevert.Image")));
            this.tscmdRevert.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tscmdRevert.Name = "tscmdRevert";
            this.tscmdRevert.Size = new System.Drawing.Size(170, 22);
            this.tscmdRevert.Text = "Switch to Selected Revision";
            this.tscmdRevert.Click += new System.EventHandler(this.tscmdRevert_Click);
            // 
            // panMain
            // 
            this.panMain.Controls.Add(this.tabMain);
            this.panMain.Controls.Add(this.splitter1);
            this.panMain.Controls.Add(this.panHistory);
            this.panMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panMain.Location = new System.Drawing.Point(0, 0);
            this.panMain.Name = "panMain";
            this.panMain.Size = new System.Drawing.Size(984, 749);
            this.panMain.TabIndex = 3;
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter1.Location = new System.Drawing.Point(594, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 749);
            this.splitter1.TabIndex = 3;
            this.splitter1.TabStop = false;
            // 
            // tsCmbOrderType
            // 
            this.tsCmbOrderType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tsCmbOrderType.Items.AddRange(new object[] {
            "Cecil County Fair",
            "Solanco Fair"});
            this.tsCmbOrderType.MaxDropDownItems = 2;
            this.tsCmbOrderType.Name = "tsCmbOrderType";
            this.tsCmbOrderType.Size = new System.Drawing.Size(121, 25);
            // 
            // clsAuctionIndexBindingSource
            // 
            this.clsAuctionIndexBindingSource.DataSource = typeof(Livestock_Auction.DB.clsAuctionIndex);
            // 
            // frmAuctionOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 749);
            this.Controls.Add(this.panMain);
            this.Name = "frmAuctionOrder";
            this.Text = "Edit Auction Order";
            this.Load += new System.EventHandler(this.frmAuctionOrder_Load);
            this.tabMain.ResumeLayout(false);
            this.tabpgEdit.ResumeLayout(false);
            this.tabpgEdit.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.tsEdit.ResumeLayout(false);
            this.tsEdit.PerformLayout();
            this.tabpgReport.ResumeLayout(false);
            this.panHistory.ResumeLayout(false);
            this.panHistory.PerformLayout();
            this.tsHistory.ResumeLayout(false);
            this.tsHistory.PerformLayout();
            this.panMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.clsAuctionIndexBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tabpgEdit;
        private System.Windows.Forms.TabPage tabpgReport;
        private System.Windows.Forms.ListView lsvHistory;
        private System.Windows.Forms.Panel panHistory;
        private System.Windows.Forms.ToolStrip tsHistory;
        private System.Windows.Forms.ColumnHeader colRevisionIndex;
        private System.Windows.Forms.ColumnHeader colRevisionDate;
        private System.Windows.Forms.ColumnHeader colRecordsAffected;
        private System.Windows.Forms.Panel panMain;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.ListView lsvOrder;
        private System.Windows.Forms.ToolStrip tsEdit;
        private System.Windows.Forms.ToolStripButton tscmdGenerateOrder;
        private System.Windows.Forms.ColumnHeader colOrder;
        private System.Windows.Forms.ColumnHeader colExhibitorNumber;
        private System.Windows.Forms.ColumnHeader colExhibitorName;
        private System.Windows.Forms.ColumnHeader colExhibitChampion;
        private System.Windows.Forms.ColumnHeader colExhibitType;
        private System.Windows.Forms.ColumnHeader colExhibitWeight;
        private System.Windows.Forms.ToolStripButton tscmdRevert;
        private Microsoft.Reporting.WinForms.ReportViewer rptAuctionOrder;
        private System.Windows.Forms.BindingSource clsAuctionIndexBindingSource;
        private System.Windows.Forms.ColumnHeader colTagNumber;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListView lsvUnsorted;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ColumnHeader colReverted;
        private System.Windows.Forms.ColumnHeader colExhibitRateOfGain;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ToolStripComboBox tsCmbOrderType;
    }
}