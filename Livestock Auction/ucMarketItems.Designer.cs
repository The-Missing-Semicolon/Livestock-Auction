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
            this.colSelBy = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
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
            this.colSelBy});
            this.lsvMarket.FullRowSelect = true;
            this.lsvMarket.HideSelection = false;
            this.lsvMarket.Location = new System.Drawing.Point(0, 0);
            this.lsvMarket.Name = "lsvMarket";
            this.lsvMarket.Size = new System.Drawing.Size(695, 302);
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
            // cmdSave
            // 
            this.cmdSave.Location = new System.Drawing.Point(582, 306);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(113, 23);
            this.cmdSave.TabIndex = 12;
            this.cmdSave.Text = "Save";
            this.cmdSave.UseVisualStyleBackColor = true;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // txtItemName
            // 
            this.txtItemName.Location = new System.Drawing.Point(57, 322);
            this.txtItemName.Name = "txtItemName";
            this.txtItemName.Size = new System.Drawing.Size(152, 20);
            this.txtItemName.TabIndex = 4;
            // 
            // txtItemValue
            // 
            this.txtItemValue.Location = new System.Drawing.Point(215, 322);
            this.txtItemValue.Name = "txtItemValue";
            this.txtItemValue.Size = new System.Drawing.Size(100, 20);
            this.txtItemValue.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(57, 306);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Animal/Item";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(212, 306);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Price per Item/Unit";
            // 
            // cmdRemove
            // 
            this.cmdRemove.Location = new System.Drawing.Point(582, 364);
            this.cmdRemove.Name = "cmdRemove";
            this.cmdRemove.Size = new System.Drawing.Size(113, 23);
            this.cmdRemove.TabIndex = 14;
            this.cmdRemove.Text = "Remove Selected";
            this.cmdRemove.UseVisualStyleBackColor = true;
            this.cmdRemove.Click += new System.EventHandler(this.cmdRemove_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(-3, 306);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Market ID";
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(0, 322);
            this.txtID.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(51, 20);
            this.txtID.TabIndex = 2;
            this.txtID.Leave += new System.EventHandler(this.txtID_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(318, 306);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Units";
            // 
            // txtItemUnits
            // 
            this.txtItemUnits.Location = new System.Drawing.Point(321, 322);
            this.txtItemUnits.Name = "txtItemUnits";
            this.txtItemUnits.Size = new System.Drawing.Size(71, 20);
            this.txtItemUnits.TabIndex = 8;
            // 
            // chkAllowAdvertising
            // 
            this.chkAllowAdvertising.AutoSize = true;
            this.chkAllowAdvertising.Location = new System.Drawing.Point(413, 305);
            this.chkAllowAdvertising.Name = "chkAllowAdvertising";
            this.chkAllowAdvertising.Size = new System.Drawing.Size(106, 17);
            this.chkAllowAdvertising.TabIndex = 9;
            this.chkAllowAdvertising.Text = "Allow Advertising";
            this.chkAllowAdvertising.UseVisualStyleBackColor = true;
            // 
            // chkValidDisposition
            // 
            this.chkValidDisposition.AutoSize = true;
            this.chkValidDisposition.Location = new System.Drawing.Point(413, 325);
            this.chkValidDisposition.Name = "chkValidDisposition";
            this.chkValidDisposition.Size = new System.Drawing.Size(103, 17);
            this.chkValidDisposition.TabIndex = 10;
            this.chkValidDisposition.Text = "Valid Disposition";
            this.chkValidDisposition.UseVisualStyleBackColor = true;
            // 
            // cmdClear
            // 
            this.cmdClear.Location = new System.Drawing.Point(582, 335);
            this.cmdClear.Name = "cmdClear";
            this.cmdClear.Size = new System.Drawing.Size(113, 23);
            this.cmdClear.TabIndex = 13;
            this.cmdClear.Text = "Clear";
            this.cmdClear.UseVisualStyleBackColor = true;
            this.cmdClear.Click += new System.EventHandler(this.cmdClear_Click);
            // 
            // chkSellByPound
            // 
            this.chkSellByPound.AutoSize = true;
            this.chkSellByPound.Location = new System.Drawing.Point(413, 347);
            this.chkSellByPound.Name = "chkSellByPound";
            this.chkSellByPound.Size = new System.Drawing.Size(92, 17);
            this.chkSellByPound.TabIndex = 11;
            this.chkSellByPound.Text = "Sell By Pound";
            this.chkSellByPound.UseVisualStyleBackColor = true;
            // 
            // colSelBy
            // 
            this.colSelBy.Text = "Sell By";
            // 
            // ucMarketItems
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chkSellByPound);
            this.Controls.Add(this.cmdClear);
            this.Controls.Add(this.chkValidDisposition);
            this.Controls.Add(this.chkAllowAdvertising);
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
            this.Name = "ucMarketItems";
            this.Size = new System.Drawing.Size(695, 388);
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
    }
}
