namespace Livestock_Auction
{
    partial class frmSDFImport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSDFImport));
            this.chkHistBuyerHistory = new System.Windows.Forms.CheckBox();
            this.chkHistExhibitors = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblCountExhibits = new System.Windows.Forms.Label();
            this.lblCountExhibitors = new System.Windows.Forms.Label();
            this.lblCountBuyers = new System.Windows.Forms.Label();
            this.lblDateNewest = new System.Windows.Forms.Label();
            this.lblDateOldest = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkHistAll = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.chkImportHistory = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chkImportCurrent = new System.Windows.Forms.CheckBox();
            this.chkCurAll = new System.Windows.Forms.CheckBox();
            this.chkCurExhibits = new System.Windows.Forms.CheckBox();
            this.chkCurPurchases = new System.Windows.Forms.CheckBox();
            this.chkCurOrder = new System.Windows.Forms.CheckBox();
            this.chkCurBuyers = new System.Windows.Forms.CheckBox();
            this.cmdImport = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkHistBuyerHistory
            // 
            this.chkHistBuyerHistory.AutoSize = true;
            this.chkHistBuyerHistory.Enabled = false;
            this.chkHistBuyerHistory.Location = new System.Drawing.Point(31, 86);
            this.chkHistBuyerHistory.Name = "chkHistBuyerHistory";
            this.chkHistBuyerHistory.Size = new System.Drawing.Size(120, 17);
            this.chkHistBuyerHistory.TabIndex = 0;
            this.chkHistBuyerHistory.Text = "Import Buyer History";
            this.chkHistBuyerHistory.UseVisualStyleBackColor = true;
            this.chkHistBuyerHistory.CheckedChanged += new System.EventHandler(this.chkHistoryCheckbox_CheckedChanged);
            // 
            // chkHistExhibitors
            // 
            this.chkHistExhibitors.AutoSize = true;
            this.chkHistExhibitors.Enabled = false;
            this.chkHistExhibitors.Location = new System.Drawing.Point(31, 109);
            this.chkHistExhibitors.Name = "chkHistExhibitors";
            this.chkHistExhibitors.Size = new System.Drawing.Size(103, 17);
            this.chkHistExhibitors.TabIndex = 1;
            this.chkHistExhibitors.Text = "Import Exhibitors";
            this.chkHistExhibitors.UseVisualStyleBackColor = true;
            this.chkHistExhibitors.CheckedChanged += new System.EventHandler(this.chkHistoryCheckbox_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Oldest Record:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblCountExhibits);
            this.groupBox1.Controls.Add(this.lblCountExhibitors);
            this.groupBox1.Controls.Add(this.lblCountBuyers);
            this.groupBox1.Controls.Add(this.lblDateNewest);
            this.groupBox1.Controls.Add(this.lblDateOldest);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(512, 63);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Statistics";
            // 
            // lblCountExhibits
            // 
            this.lblCountExhibits.AutoSize = true;
            this.lblCountExhibits.Location = new System.Drawing.Point(406, 42);
            this.lblCountExhibits.Name = "lblCountExhibits";
            this.lblCountExhibits.Size = new System.Drawing.Size(81, 13);
            this.lblCountExhibits.TabIndex = 10;
            this.lblCountExhibits.Text = "lblCountExhibits";
            // 
            // lblCountExhibitors
            // 
            this.lblCountExhibitors.AutoSize = true;
            this.lblCountExhibitors.Location = new System.Drawing.Point(406, 29);
            this.lblCountExhibitors.Name = "lblCountExhibitors";
            this.lblCountExhibitors.Size = new System.Drawing.Size(90, 13);
            this.lblCountExhibitors.TabIndex = 9;
            this.lblCountExhibitors.Text = "lblCountExhibitors";
            // 
            // lblCountBuyers
            // 
            this.lblCountBuyers.AutoSize = true;
            this.lblCountBuyers.Location = new System.Drawing.Point(406, 16);
            this.lblCountBuyers.Name = "lblCountBuyers";
            this.lblCountBuyers.Size = new System.Drawing.Size(77, 13);
            this.lblCountBuyers.TabIndex = 8;
            this.lblCountBuyers.Text = "lblCountBuyers";
            // 
            // lblDateNewest
            // 
            this.lblDateNewest.AutoSize = true;
            this.lblDateNewest.Location = new System.Drawing.Point(104, 29);
            this.lblDateNewest.Name = "lblDateNewest";
            this.lblDateNewest.Size = new System.Drawing.Size(76, 13);
            this.lblDateNewest.TabIndex = 7;
            this.lblDateNewest.Text = "lblDateNewest";
            // 
            // lblDateOldest
            // 
            this.lblDateOldest.AutoSize = true;
            this.lblDateOldest.Location = new System.Drawing.Point(104, 16);
            this.lblDateOldest.Name = "lblDateOldest";
            this.lblDateOldest.Size = new System.Drawing.Size(70, 13);
            this.lblDateOldest.TabIndex = 4;
            this.lblDateOldest.Text = "lblDateOldest";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(269, 42);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(120, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Exhibits Registered:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(269, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(131, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Exhibitors Registered:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(269, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(114, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Buyers Registered:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Newst Record:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkHistAll);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.chkImportHistory);
            this.groupBox2.Controls.Add(this.chkHistBuyerHistory);
            this.groupBox2.Controls.Add(this.chkHistExhibitors);
            this.groupBox2.Location = new System.Drawing.Point(12, 81);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(239, 183);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "groupBox2";
            // 
            // chkHistAll
            // 
            this.chkHistAll.AutoSize = true;
            this.chkHistAll.Enabled = false;
            this.chkHistAll.Location = new System.Drawing.Point(9, 59);
            this.chkHistAll.Name = "chkHistAll";
            this.chkHistAll.Size = new System.Drawing.Size(37, 17);
            this.chkHistAll.TabIndex = 14;
            this.chkHistAll.Text = "All";
            this.chkHistAll.UseVisualStyleBackColor = true;
            this.chkHistAll.CheckedChanged += new System.EventHandler(this.chkHistAll_CheckedChanged);
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label9.Location = new System.Drawing.Point(6, 20);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(227, 36);
            this.label9.TabIndex = 5;
            this.label9.Text = "Import data from the previous years auction to accelerate data entry";
            // 
            // chkImportHistory
            // 
            this.chkImportHistory.AutoSize = true;
            this.chkImportHistory.Location = new System.Drawing.Point(0, 0);
            this.chkImportHistory.Name = "chkImportHistory";
            this.chkImportHistory.Size = new System.Drawing.Size(127, 17);
            this.chkImportHistory.TabIndex = 0;
            this.chkImportHistory.Text = "Import Historical Data";
            this.chkImportHistory.UseVisualStyleBackColor = true;
            this.chkImportHistory.CheckedChanged += new System.EventHandler(this.chkImportHistory_CheckedChanged);
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label6.Location = new System.Drawing.Point(6, 20);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(254, 36);
            this.label6.TabIndex = 6;
            this.label6.Text = "Import current data that was entered externally from the database, or restore dat" +
    "a from a backup.";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chkImportCurrent);
            this.groupBox3.Controls.Add(this.chkCurAll);
            this.groupBox3.Controls.Add(this.chkCurExhibits);
            this.groupBox3.Controls.Add(this.chkCurPurchases);
            this.groupBox3.Controls.Add(this.chkCurOrder);
            this.groupBox3.Controls.Add(this.chkCurBuyers);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Location = new System.Drawing.Point(257, 81);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(266, 183);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "groupBox3";
            // 
            // chkImportCurrent
            // 
            this.chkImportCurrent.AutoSize = true;
            this.chkImportCurrent.Location = new System.Drawing.Point(0, 0);
            this.chkImportCurrent.Name = "chkImportCurrent";
            this.chkImportCurrent.Size = new System.Drawing.Size(118, 17);
            this.chkImportCurrent.TabIndex = 6;
            this.chkImportCurrent.Text = "Import Current Data";
            this.chkImportCurrent.UseVisualStyleBackColor = true;
            this.chkImportCurrent.CheckedChanged += new System.EventHandler(this.chkImportCurrent_CheckedChanged);
            // 
            // chkCurAll
            // 
            this.chkCurAll.AutoSize = true;
            this.chkCurAll.Enabled = false;
            this.chkCurAll.Location = new System.Drawing.Point(9, 59);
            this.chkCurAll.Name = "chkCurAll";
            this.chkCurAll.Size = new System.Drawing.Size(37, 17);
            this.chkCurAll.TabIndex = 13;
            this.chkCurAll.Text = "All";
            this.chkCurAll.UseVisualStyleBackColor = true;
            this.chkCurAll.CheckedChanged += new System.EventHandler(this.chkCurAll_CheckedChanged);
            // 
            // chkCurExhibits
            // 
            this.chkCurExhibits.AutoSize = true;
            this.chkCurExhibits.Enabled = false;
            this.chkCurExhibits.Location = new System.Drawing.Point(27, 86);
            this.chkCurExhibits.Name = "chkCurExhibits";
            this.chkCurExhibits.Size = new System.Drawing.Size(131, 17);
            this.chkCurExhibits.TabIndex = 12;
            this.chkCurExhibits.Text = "Exhibitors and Exhibits";
            this.chkCurExhibits.UseVisualStyleBackColor = true;
            this.chkCurExhibits.CheckedChanged += new System.EventHandler(this.chkCurrentCheckbox_CheckedChanged);
            // 
            // chkCurPurchases
            // 
            this.chkCurPurchases.AutoSize = true;
            this.chkCurPurchases.Enabled = false;
            this.chkCurPurchases.Location = new System.Drawing.Point(27, 155);
            this.chkCurPurchases.Name = "chkCurPurchases";
            this.chkCurPurchases.Size = new System.Drawing.Size(146, 17);
            this.chkCurPurchases.TabIndex = 11;
            this.chkCurPurchases.Text = "Purchases and Checkout";
            this.chkCurPurchases.UseVisualStyleBackColor = true;
            this.chkCurPurchases.CheckedChanged += new System.EventHandler(this.chkCurrentCheckbox_CheckedChanged);
            // 
            // chkCurOrder
            // 
            this.chkCurOrder.AutoSize = true;
            this.chkCurOrder.Enabled = false;
            this.chkCurOrder.Location = new System.Drawing.Point(27, 109);
            this.chkCurOrder.Name = "chkCurOrder";
            this.chkCurOrder.Size = new System.Drawing.Size(91, 17);
            this.chkCurOrder.TabIndex = 8;
            this.chkCurOrder.Text = "Auction Order";
            this.chkCurOrder.UseVisualStyleBackColor = true;
            this.chkCurOrder.CheckedChanged += new System.EventHandler(this.chkCurrentCheckbox_CheckedChanged);
            // 
            // chkCurBuyers
            // 
            this.chkCurBuyers.AutoSize = true;
            this.chkCurBuyers.Enabled = false;
            this.chkCurBuyers.Location = new System.Drawing.Point(27, 132);
            this.chkCurBuyers.Name = "chkCurBuyers";
            this.chkCurBuyers.Size = new System.Drawing.Size(58, 17);
            this.chkCurBuyers.TabIndex = 7;
            this.chkCurBuyers.Text = "Buyers";
            this.chkCurBuyers.UseVisualStyleBackColor = true;
            this.chkCurBuyers.CheckedChanged += new System.EventHandler(this.chkCurrentCheckbox_CheckedChanged);
            // 
            // cmdImport
            // 
            this.cmdImport.Enabled = false;
            this.cmdImport.Location = new System.Drawing.Point(12, 270);
            this.cmdImport.Name = "cmdImport";
            this.cmdImport.Size = new System.Drawing.Size(512, 23);
            this.cmdImport.TabIndex = 6;
            this.cmdImport.Text = "Import";
            this.cmdImport.UseVisualStyleBackColor = true;
            this.cmdImport.Click += new System.EventHandler(this.cmdImport_Click);
            // 
            // frmSDFImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(537, 302);
            this.Controls.Add(this.cmdImport);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmSDFImport";
            this.Text = "Import File";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox chkHistBuyerHistory;
        private System.Windows.Forms.CheckBox chkHistExhibitors;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblCountExhibits;
        private System.Windows.Forms.Label lblCountExhibitors;
        private System.Windows.Forms.Label lblCountBuyers;
        private System.Windows.Forms.Label lblDateNewest;
        private System.Windows.Forms.Label lblDateOldest;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chkImportHistory;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox chkImportCurrent;
        private System.Windows.Forms.CheckBox chkCurBuyers;
        private System.Windows.Forms.CheckBox chkCurExhibits;
        private System.Windows.Forms.CheckBox chkCurPurchases;
        private System.Windows.Forms.CheckBox chkCurOrder;
        private System.Windows.Forms.CheckBox chkHistAll;
        private System.Windows.Forms.CheckBox chkCurAll;
        private System.Windows.Forms.Button cmdImport;
    }
}