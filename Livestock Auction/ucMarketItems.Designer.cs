namespace Livestock_Auction
{
    partial class ucMarketItems
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
            this.colItem = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colPrice = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lsvMarket = new System.Windows.Forms.ListView();
            this.colID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colUnits = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colAdvertising = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDisposition = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colSelBy = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colAdvertDestination = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmdSave = new System.Windows.Forms.Button();
            this.txtItemName = new System.Windows.Forms.TextBox();
            this.txtItemValue = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmdRemove = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtID = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtItemUnits = new System.Windows.Forms.TextBox();
            this.chkAllowAdvertising = new System.Windows.Forms.CheckBox();
            this.chkValidDisposition = new System.Windows.Forms.CheckBox();
            this.cmdClear = new System.Windows.Forms.Button();
            this.chkSellByPound = new System.Windows.Forms.CheckBox();
            this.txtAdvertDestination = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // colItem
            // 
            this.colItem.Text = "Animal/Item";
            this.colItem.Width = 139;
            // 
            // colPrice
            // 
            this.colPrice.Text = "Price";
            this.colPrice.Width = 95;
            // 
            // lsvMarket
            // 
            this.lsvMarket.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lsvMarket.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colID,
            this.colItem,
            this.colPrice,
            this.colUnits,
            this.colAdvertising,
            this.colDisposition,
            this.colSelBy,
            this.colAdvertDestination});
            this.lsvMarket.FullRowSelect = true;
            this.lsvMarket.HideSelection = false;
            this.lsvMarket.Location = new System.Drawing.Point(0, 0);
            this.lsvMarket.Margin = new System.Windows.Forms.Padding(4);
            this.lsvMarket.Name = "lsvMarket";
            this.lsvMarket.Size = new System.Drawing.Size(925, 371);
            this.lsvMarket.TabIndex = 15;
            this.lsvMarket.UseCompatibleStateImageBehavior = false;
            this.lsvMarket.View = System.Windows.Forms.View.Details;
            this.lsvMarket.SelectedIndexChanged += new System.EventHandler(this.lsvMarket_SelectedIndexChanged);
            // 
            // colID
            // 
            this.colID.Text = "Market ID";
            // 
            // colUnits
            // 
            this.colUnits.Text = "Units";
            // 
            // colAdvertising
            // 
            this.colAdvertising.Text = "Advertising";
            this.colAdvertising.Width = 75;
            // 
            // colDisposition
            // 
            this.colDisposition.Text = "Disposition";
            this.colDisposition.Width = 80;
            // 
            // colSelBy
            // 
            this.colSelBy.Text = "Sell By";
            // 
            // colAdvertDestination
            // 
            this.colAdvertDestination.Text = "Advertising Destination";
            this.colAdvertDestination.Width = 160;
            // 
            // cmdSave
            // 
            this.cmdSave.Location = new System.Drawing.Point(776, 377);
            this.cmdSave.Margin = new System.Windows.Forms.Padding(4);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(151, 28);
            this.cmdSave.TabIndex = 12;
            this.cmdSave.Text = "Save";
            this.cmdSave.UseVisualStyleBackColor = true;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // txtItemName
            // 
            this.txtItemName.Location = new System.Drawing.Point(76, 396);
            this.txtItemName.Margin = new System.Windows.Forms.Padding(4);
            this.txtItemName.Name = "txtItemName";
            this.txtItemName.Size = new System.Drawing.Size(201, 22);
            this.txtItemName.TabIndex = 4;
            // 
            // txtItemValue
            // 
            this.txtItemValue.Location = new System.Drawing.Point(287, 396);
            this.txtItemValue.Margin = new System.Windows.Forms.Padding(4);
            this.txtItemValue.Name = "txtItemValue";
            this.txtItemValue.Size = new System.Drawing.Size(132, 22);
            this.txtItemValue.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(76, 377);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Animal/Item";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(283, 377);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(124, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "Price per Item/Unit";
            // 
            // cmdRemove
            // 
            this.cmdRemove.Location = new System.Drawing.Point(776, 448);
            this.cmdRemove.Margin = new System.Windows.Forms.Padding(4);
            this.cmdRemove.Name = "cmdRemove";
            this.cmdRemove.Size = new System.Drawing.Size(151, 28);
            this.cmdRemove.TabIndex = 14;
            this.cmdRemove.Text = "Remove Selected";
            this.cmdRemove.UseVisualStyleBackColor = true;
            this.cmdRemove.Click += new System.EventHandler(this.cmdRemove_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(-4, 377);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 17);
            this.label3.TabIndex = 0;
            this.label3.Text = "Market ID";
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(0, 396);
            this.txtID.Margin = new System.Windows.Forms.Padding(0, 4, 4, 4);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(67, 22);
            this.txtID.TabIndex = 2;
            this.txtID.Leave += new System.EventHandler(this.txtID_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(424, 377);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 17);
            this.label4.TabIndex = 7;
            this.label4.Text = "Units";
            // 
            // txtItemUnits
            // 
            this.txtItemUnits.Location = new System.Drawing.Point(428, 396);
            this.txtItemUnits.Margin = new System.Windows.Forms.Padding(4);
            this.txtItemUnits.Name = "txtItemUnits";
            this.txtItemUnits.Size = new System.Drawing.Size(93, 22);
            this.txtItemUnits.TabIndex = 8;
            // 
            // chkAllowAdvertising
            // 
            this.chkAllowAdvertising.AutoSize = true;
            this.chkAllowAdvertising.Location = new System.Drawing.Point(10, 23);
            this.chkAllowAdvertising.Margin = new System.Windows.Forms.Padding(4);
            this.chkAllowAdvertising.Name = "chkAllowAdvertising";
            this.chkAllowAdvertising.Size = new System.Drawing.Size(62, 21);
            this.chkAllowAdvertising.TabIndex = 9;
            this.chkAllowAdvertising.Text = "Allow";
            this.chkAllowAdvertising.UseVisualStyleBackColor = true;
            this.chkAllowAdvertising.CheckedChanged += new System.EventHandler(this.chkAllowAdvertising_CheckedChanged);
            // 
            // chkValidDisposition
            // 
            this.chkValidDisposition.AutoSize = true;
            this.chkValidDisposition.Location = new System.Drawing.Point(551, 400);
            this.chkValidDisposition.Margin = new System.Windows.Forms.Padding(4);
            this.chkValidDisposition.Name = "chkValidDisposition";
            this.chkValidDisposition.Size = new System.Drawing.Size(134, 21);
            this.chkValidDisposition.TabIndex = 10;
            this.chkValidDisposition.Text = "Valid Disposition";
            this.chkValidDisposition.UseVisualStyleBackColor = true;
            // 
            // cmdClear
            // 
            this.cmdClear.Location = new System.Drawing.Point(776, 412);
            this.cmdClear.Margin = new System.Windows.Forms.Padding(4);
            this.cmdClear.Name = "cmdClear";
            this.cmdClear.Size = new System.Drawing.Size(151, 28);
            this.cmdClear.TabIndex = 13;
            this.cmdClear.Text = "Clear";
            this.cmdClear.UseVisualStyleBackColor = true;
            this.cmdClear.Click += new System.EventHandler(this.cmdClear_Click);
            // 
            // chkSellByPound
            // 
            this.chkSellByPound.AutoSize = true;
            this.chkSellByPound.Location = new System.Drawing.Point(551, 427);
            this.chkSellByPound.Margin = new System.Windows.Forms.Padding(4);
            this.chkSellByPound.Name = "chkSellByPound";
            this.chkSellByPound.Size = new System.Drawing.Size(118, 21);
            this.chkSellByPound.TabIndex = 11;
            this.chkSellByPound.Text = "Sell By Pound";
            this.chkSellByPound.UseVisualStyleBackColor = true;
            // 
            // txtAdvertDestination
            // 
            this.txtAdvertDestination.Enabled = false;
            this.txtAdvertDestination.Location = new System.Drawing.Point(183, 21);
            this.txtAdvertDestination.Margin = new System.Windows.Forms.Padding(4);
            this.txtAdvertDestination.Name = "txtAdvertDestination";
            this.txtAdvertDestination.Size = new System.Drawing.Size(271, 22);
            this.txtAdvertDestination.TabIndex = 16;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.chkAllowAdvertising);
            this.groupBox1.Controls.Add(this.txtAdvertDestination);
            this.groupBox1.Location = new System.Drawing.Point(3, 425);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(518, 53);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Advertising";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(100, 24);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 17);
            this.label5.TabIndex = 18;
            this.label5.Text = "Destination";
            // 
            // ucMarketItems
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.chkSellByPound);
            this.Controls.Add(this.cmdClear);
            this.Controls.Add(this.chkValidDisposition);
            this.Controls.Add(this.txtItemUnits);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmdRemove);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtItemValue);
            this.Controls.Add(this.txtItemName);
            this.Controls.Add(this.cmdSave);
            this.Controls.Add(this.lsvMarket);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ucMarketItems";
            this.Size = new System.Drawing.Size(927, 478);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ColumnHeader colItem;
        private System.Windows.Forms.ColumnHeader colPrice;
        private System.Windows.Forms.ListView lsvMarket;
        private System.Windows.Forms.Button cmdSave;
        private System.Windows.Forms.TextBox txtItemName;
        private System.Windows.Forms.TextBox txtItemValue;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button cmdRemove;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.ColumnHeader colID;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtItemUnits;
        private System.Windows.Forms.ColumnHeader colUnits;
        private System.Windows.Forms.CheckBox chkAllowAdvertising;
        private System.Windows.Forms.ColumnHeader colAdvertising;
        private System.Windows.Forms.CheckBox chkValidDisposition;
        private System.Windows.Forms.ColumnHeader colDisposition;
        private System.Windows.Forms.Button cmdClear;
        private System.Windows.Forms.CheckBox chkSellByPound;
        private System.Windows.Forms.ColumnHeader colSelBy;
        private System.Windows.Forms.TextBox txtAdvertDestination;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ColumnHeader colAdvertDestination;
    }
}
