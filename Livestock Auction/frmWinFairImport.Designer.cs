namespace Livestock_Auction
{
    partial class frmWinFairImport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmWinFairImport));
            this.lsvExhibitors = new System.Windows.Forms.ListView();
            this.colID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colNameFirst = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colNameLast = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colAddressStreet = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colAddressCity = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colAddressState = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colAddressZip = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tslblFileName = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslblTotalRecords = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslblExhbitors = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslblNewRecord = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslblModifiedRecords = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsMain = new System.Windows.Forms.ToolStrip();
            this.tscmdImportRecords = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1.SuspendLayout();
            this.tsMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // lsvExhibitors
            // 
            this.lsvExhibitors.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colID,
            this.colNameFirst,
            this.colNameLast,
            this.colAddressStreet,
            this.colAddressCity,
            this.colAddressState,
            this.colAddressZip,
            this.colStatus});
            this.lsvExhibitors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lsvExhibitors.HideSelection = false;
            this.lsvExhibitors.Location = new System.Drawing.Point(0, 25);
            this.lsvExhibitors.Name = "lsvExhibitors";
            this.lsvExhibitors.Size = new System.Drawing.Size(757, 370);
            this.lsvExhibitors.TabIndex = 0;
            this.lsvExhibitors.UseCompatibleStateImageBehavior = false;
            this.lsvExhibitors.View = System.Windows.Forms.View.Details;
            // 
            // colID
            // 
            this.colID.Text = "ID";
            // 
            // colNameFirst
            // 
            this.colNameFirst.Text = "First Name";
            this.colNameFirst.Width = 91;
            // 
            // colNameLast
            // 
            this.colNameLast.Text = "Last Name";
            this.colNameLast.Width = 92;
            // 
            // colAddressStreet
            // 
            this.colAddressStreet.Text = "Address";
            this.colAddressStreet.Width = 144;
            // 
            // colAddressCity
            // 
            this.colAddressCity.Text = "City";
            // 
            // colAddressState
            // 
            this.colAddressState.Text = "State";
            // 
            // colAddressZip
            // 
            this.colAddressZip.Text = "Zip Code";
            // 
            // colStatus
            // 
            this.colStatus.Text = "Status";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslblFileName,
            this.tslblTotalRecords,
            this.tslblExhbitors,
            this.tslblNewRecord,
            this.tslblModifiedRecords});
            this.statusStrip1.Location = new System.Drawing.Point(0, 395);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(757, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "ssMain";
            // 
            // tslblFileName
            // 
            this.tslblFileName.Name = "tslblFileName";
            this.tslblFileName.Size = new System.Drawing.Size(45, 17);
            this.tslblFileName.Text = "File: {0}";
            // 
            // tslblTotalRecords
            // 
            this.tslblTotalRecords.Name = "tslblTotalRecords";
            this.tslblTotalRecords.Size = new System.Drawing.Size(94, 17);
            this.tslblTotalRecords.Text = "{0} Total Records";
            // 
            // tslblExhbitors
            // 
            this.tslblExhbitors.Name = "tslblExhbitors";
            this.tslblExhbitors.Size = new System.Drawing.Size(136, 17);
            this.tslblExhbitors.Text = "Found {0} 4-H Exhibitors";
            // 
            // tslblNewRecord
            // 
            this.tslblNewRecord.Name = "tslblNewRecord";
            this.tslblNewRecord.Size = new System.Drawing.Size(132, 17);
            this.tslblNewRecord.Text = "{0} Records to be added";
            // 
            // tslblModifiedRecords
            // 
            this.tslblModifiedRecords.Name = "tslblModifiedRecords";
            this.tslblModifiedRecords.Size = new System.Drawing.Size(143, 17);
            this.tslblModifiedRecords.Text = "{0} Records to be updated";
            // 
            // tsMain
            // 
            this.tsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tscmdImportRecords});
            this.tsMain.Location = new System.Drawing.Point(0, 0);
            this.tsMain.Name = "tsMain";
            this.tsMain.Size = new System.Drawing.Size(757, 25);
            this.tsMain.TabIndex = 2;
            this.tsMain.Text = "tsMain";
            // 
            // tscmdImportRecords
            // 
            this.tscmdImportRecords.Image = global::Livestock_Auction.Properties.Resources.import;
            this.tscmdImportRecords.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tscmdImportRecords.Name = "tscmdImportRecords";
            this.tscmdImportRecords.Size = new System.Drawing.Size(108, 22);
            this.tscmdImportRecords.Text = "Import Records";
            this.tscmdImportRecords.Click += new System.EventHandler(this.tscmdImportRecords_Click);
            // 
            // frmWinFairImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(757, 417);
            this.Controls.Add(this.lsvExhibitors);
            this.Controls.Add(this.tsMain);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmWinFairImport";
            this.Text = "WinFair Import";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tsMain.ResumeLayout(false);
            this.tsMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lsvExhibitors;
        private System.Windows.Forms.ColumnHeader colID;
        private System.Windows.Forms.ColumnHeader colNameFirst;
        private System.Windows.Forms.ColumnHeader colNameLast;
        private System.Windows.Forms.ColumnHeader colAddressStreet;
        private System.Windows.Forms.ColumnHeader colAddressCity;
        private System.Windows.Forms.ColumnHeader colAddressState;
        private System.Windows.Forms.ColumnHeader colAddressZip;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tslblFileName;
        private System.Windows.Forms.ToolStripStatusLabel tslblExhbitors;
        private System.Windows.Forms.ToolStrip tsMain;
        private System.Windows.Forms.ToolStripButton tscmdImportRecords;
        private System.Windows.Forms.ColumnHeader colStatus;
        private System.Windows.Forms.ToolStripStatusLabel tslblNewRecord;
        private System.Windows.Forms.ToolStripStatusLabel tslblModifiedRecords;
        private System.Windows.Forms.ToolStripStatusLabel tslblTotalRecords;
    }
}