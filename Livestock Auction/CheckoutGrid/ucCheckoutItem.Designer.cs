namespace Livestock_Auction
{
    partial class ucCheckoutItem
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
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.lblBidTotal = new Livestock_Auction.Helpers.TransparentLabel();
            this.lblItemTag = new Livestock_Auction.Helpers.TransparentLabel();
            this.lblRecipientName = new Livestock_Auction.Helpers.TransparentLabel();
            this.lblItemType = new Livestock_Auction.Helpers.TransparentLabel();
            this.lblRecipientNumber = new Livestock_Auction.Helpers.TransparentLabel();
            this.lblItemQty = new Livestock_Auction.Helpers.TransparentLabel();
            this.lblBid = new Livestock_Auction.Helpers.TransparentLabel();
            this.lblTotalOwed = new Livestock_Auction.Helpers.TransparentLabel();
            this.cmbDisposition = new System.Windows.Forms.ComboBox();
            this.txtDispositionSpecify = new System.Windows.Forms.TextBox();
            this.panTakeTurnBack = new Livestock_Auction.Helpers.TransparentPanel();
            this.radSaleFullPrice = new System.Windows.Forms.RadioButton();
            this.radSaleAdvertising = new System.Windows.Forms.RadioButton();
            this.tlpMain.SuspendLayout();
            this.panTakeTurnBack.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            this.tlpMain.AutoSize = true;
            this.tlpMain.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tlpMain.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tlpMain.ColumnCount = 8;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 99F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 107F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 73F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 178F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 72F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 271F));
            this.tlpMain.Controls.Add(this.lblBidTotal, 4, 0);
            this.tlpMain.Controls.Add(this.lblItemTag, 1, 1);
            this.tlpMain.Controls.Add(this.lblRecipientName, 0, 0);
            this.tlpMain.Controls.Add(this.lblItemType, 1, 0);
            this.tlpMain.Controls.Add(this.lblRecipientNumber, 0, 1);
            this.tlpMain.Controls.Add(this.lblItemQty, 2, 0);
            this.tlpMain.Controls.Add(this.lblBid, 3, 0);
            this.tlpMain.Controls.Add(this.lblTotalOwed, 6, 0);
            this.tlpMain.Controls.Add(this.cmbDisposition, 7, 0);
            this.tlpMain.Controls.Add(this.txtDispositionSpecify, 7, 1);
            this.tlpMain.Controls.Add(this.panTakeTurnBack, 5, 0);
            this.tlpMain.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Margin = new System.Windows.Forms.Padding(0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 2;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tlpMain.Size = new System.Drawing.Size(969, 47);
            this.tlpMain.TabIndex = 0;
            this.tlpMain.MouseEnter += new System.EventHandler(this.MouseEnter_Highlight);
            this.tlpMain.MouseLeave += new System.EventHandler(this.MouseLeave_Highlight);
            this.tlpMain.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MouseUp_Select);
            // 
            // lblBidTotal
            // 
            this.lblBidTotal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblBidTotal.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblBidTotal.Location = new System.Drawing.Point(364, 1);
            this.lblBidTotal.Margin = new System.Windows.Forms.Padding(0);
            this.lblBidTotal.Name = "lblBidTotal";
            this.tlpMain.SetRowSpan(this.lblBidTotal, 2);
            this.lblBidTotal.Size = new System.Drawing.Size(80, 45);
            this.lblBidTotal.TabIndex = 7;
            this.lblBidTotal.Text = "$#,###.##";
            this.lblBidTotal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblItemTag
            // 
            this.lblItemTag.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblItemTag.Location = new System.Drawing.Point(104, 24);
            this.lblItemTag.Name = "lblItemTag";
            this.lblItemTag.Size = new System.Drawing.Size(101, 22);
            this.lblItemTag.TabIndex = 4;
            this.lblItemTag.Text = "Tag #";
            this.lblItemTag.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRecipientName
            // 
            this.lblRecipientName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRecipientName.Location = new System.Drawing.Point(4, 1);
            this.lblRecipientName.Name = "lblRecipientName";
            this.lblRecipientName.Size = new System.Drawing.Size(93, 22);
            this.lblRecipientName.TabIndex = 1;
            this.lblRecipientName.Text = "First Last Name";
            this.lblRecipientName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblItemType
            // 
            this.lblItemType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblItemType.Location = new System.Drawing.Point(104, 1);
            this.lblItemType.Name = "lblItemType";
            this.lblItemType.Size = new System.Drawing.Size(101, 22);
            this.lblItemType.TabIndex = 3;
            this.lblItemType.Text = "Animal/Item";
            this.lblItemType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRecipientNumber
            // 
            this.lblRecipientNumber.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRecipientNumber.Location = new System.Drawing.Point(4, 24);
            this.lblRecipientNumber.Name = "lblRecipientNumber";
            this.lblRecipientNumber.Size = new System.Drawing.Size(93, 22);
            this.lblRecipientNumber.TabIndex = 2;
            this.lblRecipientNumber.Text = "####";
            this.lblRecipientNumber.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblItemQty
            // 
            this.lblItemQty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblItemQty.Location = new System.Drawing.Point(209, 1);
            this.lblItemQty.Margin = new System.Windows.Forms.Padding(0);
            this.lblItemQty.Name = "lblItemQty";
            this.tlpMain.SetRowSpan(this.lblItemQty, 2);
            this.lblItemQty.Size = new System.Drawing.Size(73, 45);
            this.lblItemQty.TabIndex = 5;
            this.lblItemQty.Text = "## Units";
            this.lblItemQty.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblBid
            // 
            this.lblBid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblBid.Location = new System.Drawing.Point(283, 1);
            this.lblBid.Margin = new System.Windows.Forms.Padding(0);
            this.lblBid.Name = "lblBid";
            this.tlpMain.SetRowSpan(this.lblBid, 2);
            this.lblBid.Size = new System.Drawing.Size(80, 45);
            this.lblBid.TabIndex = 6;
            this.lblBid.Text = "$##.##/Unit";
            this.lblBid.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTotalOwed
            // 
            this.lblTotalOwed.AutoSize = true;
            this.lblTotalOwed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotalOwed.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalOwed.Location = new System.Drawing.Point(624, 1);
            this.lblTotalOwed.Margin = new System.Windows.Forms.Padding(0);
            this.lblTotalOwed.Name = "lblTotalOwed";
            this.tlpMain.SetRowSpan(this.lblTotalOwed, 2);
            this.lblTotalOwed.Size = new System.Drawing.Size(72, 45);
            this.lblTotalOwed.TabIndex = 7;
            this.lblTotalOwed.Text = "$#,###.##";
            this.lblTotalOwed.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmbDisposition
            // 
            this.cmbDisposition.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbDisposition.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbDisposition.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbDisposition.Enabled = false;
            this.cmbDisposition.FormattingEnabled = true;
            this.cmbDisposition.Items.AddRange(new object[] {
            "(not set)",
            "Hauled by Buyer",
            "Hauled by Other",
            "Galvinell",
            "Other Instructions"});
            this.cmbDisposition.Location = new System.Drawing.Point(697, 1);
            this.cmbDisposition.Margin = new System.Windows.Forms.Padding(0);
            this.cmbDisposition.Name = "cmbDisposition";
            this.cmbDisposition.Size = new System.Drawing.Size(271, 21);
            this.cmbDisposition.TabIndex = 10;
            this.cmbDisposition.Text = "Hauled by Buyer";
            this.cmbDisposition.SelectedIndexChanged += new System.EventHandler(this.cmbDisposition_SelectedIndexChanged);
            this.cmbDisposition.TextUpdate += new System.EventHandler(this.cmbDisposition_TextUpdate);
            this.cmbDisposition.MouseEnter += new System.EventHandler(this.MouseEnter_Highlight);
            this.cmbDisposition.MouseLeave += new System.EventHandler(this.MouseLeave_Highlight);
            this.cmbDisposition.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MouseUp_Select);
            // 
            // txtDispositionSpecify
            // 
            this.txtDispositionSpecify.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDispositionSpecify.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDispositionSpecify.Enabled = false;
            this.txtDispositionSpecify.Location = new System.Drawing.Point(697, 24);
            this.txtDispositionSpecify.Margin = new System.Windows.Forms.Padding(0);
            this.txtDispositionSpecify.Name = "txtDispositionSpecify";
            this.txtDispositionSpecify.Size = new System.Drawing.Size(271, 20);
            this.txtDispositionSpecify.TabIndex = 11;
            this.txtDispositionSpecify.TextChanged += new System.EventHandler(this.txtDispositionSpecify_TextChanged);
            this.txtDispositionSpecify.MouseEnter += new System.EventHandler(this.MouseEnter_Highlight);
            this.txtDispositionSpecify.MouseLeave += new System.EventHandler(this.MouseLeave_Highlight);
            this.txtDispositionSpecify.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MouseUp_Select);
            // 
            // panTakeTurnBack
            // 
            this.panTakeTurnBack.Controls.Add(this.radSaleFullPrice);
            this.panTakeTurnBack.Controls.Add(this.radSaleAdvertising);
            this.panTakeTurnBack.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panTakeTurnBack.Location = new System.Drawing.Point(445, 1);
            this.panTakeTurnBack.Margin = new System.Windows.Forms.Padding(0);
            this.panTakeTurnBack.Name = "panTakeTurnBack";
            this.panTakeTurnBack.Size = new System.Drawing.Size(178, 22);
            this.panTakeTurnBack.TabIndex = 8;
            // 
            // radSaleFullPrice
            // 
            this.radSaleFullPrice.Location = new System.Drawing.Point(3, 1);
            this.radSaleFullPrice.Margin = new System.Windows.Forms.Padding(0);
            this.radSaleFullPrice.Name = "radSaleFullPrice";
            this.radSaleFullPrice.Size = new System.Drawing.Size(69, 21);
            this.radSaleFullPrice.TabIndex = 9;
            this.radSaleFullPrice.TabStop = true;
            this.radSaleFullPrice.Text = "Full Price";
            this.radSaleFullPrice.UseVisualStyleBackColor = true;
            this.radSaleFullPrice.CheckedChanged += new System.EventHandler(this.radSaleFullPrice_CheckedChanged);
            this.radSaleFullPrice.MouseEnter += new System.EventHandler(this.MouseEnter_Highlight);
            this.radSaleFullPrice.MouseLeave += new System.EventHandler(this.MouseLeave_Highlight);
            this.radSaleFullPrice.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MouseUp_Select);
            // 
            // radSaleAdvertising
            // 
            this.radSaleAdvertising.Location = new System.Drawing.Point(81, 1);
            this.radSaleAdvertising.Margin = new System.Windows.Forms.Padding(0);
            this.radSaleAdvertising.Name = "radSaleAdvertising";
            this.radSaleAdvertising.Size = new System.Drawing.Size(87, 21);
            this.radSaleAdvertising.TabIndex = 10;
            this.radSaleAdvertising.TabStop = true;
            this.radSaleAdvertising.Text = "Advertising";
            this.radSaleAdvertising.UseVisualStyleBackColor = true;
            this.radSaleAdvertising.CheckedChanged += new System.EventHandler(this.radSaleAdvertising_CheckedChanged);
            this.radSaleAdvertising.MouseEnter += new System.EventHandler(this.MouseEnter_Highlight);
            this.radSaleAdvertising.MouseLeave += new System.EventHandler(this.MouseLeave_Highlight);
            this.radSaleAdvertising.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MouseUp_Select);
            // 
            // ucCheckoutItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.tlpMain);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ucCheckoutItem";
            this.Size = new System.Drawing.Size(969, 47);
            this.tlpMain.ResumeLayout(false);
            this.tlpMain.PerformLayout();
            this.panTakeTurnBack.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private Livestock_Auction.Helpers.TransparentLabel lblRecipientName;
        private Livestock_Auction.Helpers.TransparentLabel lblRecipientNumber;
        private Livestock_Auction.Helpers.TransparentLabel lblItemTag;
        private Livestock_Auction.Helpers.TransparentLabel lblItemType;
        private Livestock_Auction.Helpers.TransparentLabel lblItemQty;
        private Livestock_Auction.Helpers.TransparentLabel lblBid;
        private System.Windows.Forms.RadioButton radSaleAdvertising;
        private System.Windows.Forms.RadioButton radSaleFullPrice;
        private Livestock_Auction.Helpers.TransparentLabel lblTotalOwed;
        private Livestock_Auction.Helpers.TransparentLabel lblBidTotal;
        private System.Windows.Forms.ComboBox cmbDisposition;
        private System.Windows.Forms.TextBox txtDispositionSpecify;
        private Livestock_Auction.Helpers.TransparentPanel panTakeTurnBack;
    }
}
