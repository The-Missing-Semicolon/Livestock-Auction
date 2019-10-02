namespace Livestock_Auction
{
    partial class frmNewPurchase
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmNewPurchase));
            this.grbExhibit = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chkRateOfGain = new System.Windows.Forms.CheckBox();
            this.lblTakeBackYes = new System.Windows.Forms.Label();
            this.radTakeBackNo = new System.Windows.Forms.RadioButton();
            this.radTakeBackYes = new System.Windows.Forms.RadioButton();
            this.cmdNewTag = new System.Windows.Forms.Button();
            this.lblTagNumber = new System.Windows.Forms.Label();
            this.txtTagNumber = new System.Windows.Forms.TextBox();
            this.lblWeight = new System.Windows.Forms.Label();
            this.txtChampion = new System.Windows.Forms.TextBox();
            this.lblChampion = new System.Windows.Forms.Label();
            this.txtWeight = new System.Windows.Forms.TextBox();
            this.cmdOk = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdClearExhibitor = new System.Windows.Forms.Button();
            this.cmdClearMarketItem = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtPurchasePrice = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmdClearExhibit = new System.Windows.Forms.Button();
            this.radAdditionalPayment = new System.Windows.Forms.RadioButton();
            this.radNewPurchase = new System.Windows.Forms.RadioButton();
            this.MarketItemEntry = new Livestock_Auction.DataEntry.ucMarketItemEntry();
            this.ExhibitorEntry = new Livestock_Auction.DataEntry.ucExhibitorEntry();
            this.grbExhibit.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grbExhibit
            // 
            this.grbExhibit.Controls.Add(this.label2);
            this.grbExhibit.Controls.Add(this.chkRateOfGain);
            this.grbExhibit.Controls.Add(this.lblTakeBackYes);
            this.grbExhibit.Controls.Add(this.radTakeBackNo);
            this.grbExhibit.Controls.Add(this.radTakeBackYes);
            this.grbExhibit.Controls.Add(this.cmdNewTag);
            this.grbExhibit.Controls.Add(this.lblTagNumber);
            this.grbExhibit.Controls.Add(this.txtTagNumber);
            this.grbExhibit.Controls.Add(this.lblWeight);
            this.grbExhibit.Controls.Add(this.txtChampion);
            this.grbExhibit.Controls.Add(this.lblChampion);
            this.grbExhibit.Controls.Add(this.txtWeight);
            this.grbExhibit.Location = new System.Drawing.Point(12, 188);
            this.grbExhibit.Name = "grbExhibit";
            this.grbExhibit.Size = new System.Drawing.Size(401, 64);
            this.grbExhibit.TabIndex = 4;
            this.grbExhibit.TabStop = false;
            this.grbExhibit.Text = "Exhibit";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(183, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "ROG";
            // 
            // chkRateOfGain
            // 
            this.chkRateOfGain.AutoSize = true;
            this.chkRateOfGain.Location = new System.Drawing.Point(186, 40);
            this.chkRateOfGain.Name = "chkRateOfGain";
            this.chkRateOfGain.Size = new System.Drawing.Size(50, 17);
            this.chkRateOfGain.TabIndex = 10;
            this.chkRateOfGain.Text = "ROG";
            this.chkRateOfGain.UseVisualStyleBackColor = true;
            // 
            // lblTakeBackYes
            // 
            this.lblTakeBackYes.AutoSize = true;
            this.lblTakeBackYes.Location = new System.Drawing.Point(304, 22);
            this.lblTakeBackYes.Name = "lblTakeBackYes";
            this.lblTakeBackYes.Size = new System.Drawing.Size(66, 13);
            this.lblTakeBackYes.TabIndex = 13;
            this.lblTakeBackYes.Text = "Take Back?";
            // 
            // radTakeBackNo
            // 
            this.radTakeBackNo.AutoSize = true;
            this.radTakeBackNo.Location = new System.Drawing.Point(356, 39);
            this.radTakeBackNo.Name = "radTakeBackNo";
            this.radTakeBackNo.Size = new System.Drawing.Size(39, 17);
            this.radTakeBackNo.TabIndex = 15;
            this.radTakeBackNo.TabStop = true;
            this.radTakeBackNo.Text = "No";
            this.radTakeBackNo.UseVisualStyleBackColor = true;
            // 
            // radTakeBackYes
            // 
            this.radTakeBackYes.AutoSize = true;
            this.radTakeBackYes.Location = new System.Drawing.Point(307, 39);
            this.radTakeBackYes.Name = "radTakeBackYes";
            this.radTakeBackYes.Size = new System.Drawing.Size(43, 17);
            this.radTakeBackYes.TabIndex = 14;
            this.radTakeBackYes.TabStop = true;
            this.radTakeBackYes.Text = "Yes";
            this.radTakeBackYes.UseVisualStyleBackColor = true;
            // 
            // cmdNewTag
            // 
            this.cmdNewTag.Location = new System.Drawing.Point(59, 36);
            this.cmdNewTag.Name = "cmdNewTag";
            this.cmdNewTag.Size = new System.Drawing.Size(28, 23);
            this.cmdNewTag.TabIndex = 6;
            this.cmdNewTag.Text = "*";
            this.cmdNewTag.UseVisualStyleBackColor = true;
            this.cmdNewTag.Click += new System.EventHandler(this.cmdNewTag_Click);
            // 
            // lblTagNumber
            // 
            this.lblTagNumber.AutoSize = true;
            this.lblTagNumber.Location = new System.Drawing.Point(6, 22);
            this.lblTagNumber.Name = "lblTagNumber";
            this.lblTagNumber.Size = new System.Drawing.Size(36, 13);
            this.lblTagNumber.TabIndex = 4;
            this.lblTagNumber.Text = "Tag #";
            // 
            // txtTagNumber
            // 
            this.txtTagNumber.Location = new System.Drawing.Point(9, 38);
            this.txtTagNumber.Name = "txtTagNumber";
            this.txtTagNumber.Size = new System.Drawing.Size(50, 20);
            this.txtTagNumber.TabIndex = 5;
            this.txtTagNumber.Leave += new System.EventHandler(this.txtTagNumber_Leave);
            // 
            // lblWeight
            // 
            this.lblWeight.AutoSize = true;
            this.lblWeight.Location = new System.Drawing.Point(239, 22);
            this.lblWeight.Name = "lblWeight";
            this.lblWeight.Size = new System.Drawing.Size(62, 13);
            this.lblWeight.TabIndex = 11;
            this.lblWeight.Text = "Weight/Qty";
            // 
            // txtChampion
            // 
            this.txtChampion.AutoCompleteCustomSource.AddRange(new string[] {
            "Grand Champion",
            "Reserve Champion",
            "Rate of Gain",
            "Other"});
            this.txtChampion.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtChampion.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtChampion.Location = new System.Drawing.Point(93, 38);
            this.txtChampion.Name = "txtChampion";
            this.txtChampion.Size = new System.Drawing.Size(87, 20);
            this.txtChampion.TabIndex = 8;
            this.txtChampion.TextChanged += new System.EventHandler(this.txtChampion_TextChanged);
            // 
            // lblChampion
            // 
            this.lblChampion.AutoSize = true;
            this.lblChampion.Location = new System.Drawing.Point(90, 22);
            this.lblChampion.Name = "lblChampion";
            this.lblChampion.Size = new System.Drawing.Size(54, 13);
            this.lblChampion.TabIndex = 7;
            this.lblChampion.Text = "Champion";
            // 
            // txtWeight
            // 
            this.txtWeight.Location = new System.Drawing.Point(242, 38);
            this.txtWeight.Name = "txtWeight";
            this.txtWeight.Size = new System.Drawing.Size(59, 20);
            this.txtWeight.TabIndex = 12;
            // 
            // cmdOk
            // 
            this.cmdOk.Location = new System.Drawing.Point(12, 258);
            this.cmdOk.Name = "cmdOk";
            this.cmdOk.Size = new System.Drawing.Size(290, 23);
            this.cmdOk.TabIndex = 8;
            this.cmdOk.Text = "Ok";
            this.cmdOk.UseVisualStyleBackColor = true;
            this.cmdOk.Click += new System.EventHandler(this.cmdOk_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.Location = new System.Drawing.Point(308, 258);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(285, 23);
            this.cmdCancel.TabIndex = 9;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdClearExhibitor
            // 
            this.cmdClearExhibitor.Location = new System.Drawing.Point(518, 24);
            this.cmdClearExhibitor.Name = "cmdClearExhibitor";
            this.cmdClearExhibitor.Size = new System.Drawing.Size(75, 55);
            this.cmdClearExhibitor.TabIndex = 10;
            this.cmdClearExhibitor.Text = "Clear Recipient";
            this.cmdClearExhibitor.UseVisualStyleBackColor = true;
            this.cmdClearExhibitor.Click += new System.EventHandler(this.cmdClearExhibitor_Click);
            // 
            // cmdClearMarketItem
            // 
            this.cmdClearMarketItem.Location = new System.Drawing.Point(518, 127);
            this.cmdClearMarketItem.Name = "cmdClearMarketItem";
            this.cmdClearMarketItem.Size = new System.Drawing.Size(75, 55);
            this.cmdClearMarketItem.TabIndex = 11;
            this.cmdClearMarketItem.Text = "Clear Market Item";
            this.cmdClearMarketItem.UseVisualStyleBackColor = true;
            this.cmdClearMarketItem.Click += new System.EventHandler(this.cmdClearMarketItem_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtPurchasePrice);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(419, 188);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(93, 64);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Purchase";
            // 
            // txtPurchasePrice
            // 
            this.txtPurchasePrice.Location = new System.Drawing.Point(6, 38);
            this.txtPurchasePrice.Name = "txtPurchasePrice";
            this.txtPurchasePrice.Size = new System.Drawing.Size(79, 20);
            this.txtPurchasePrice.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Purchase Price";
            // 
            // cmdClearExhibit
            // 
            this.cmdClearExhibit.Location = new System.Drawing.Point(518, 197);
            this.cmdClearExhibit.Name = "cmdClearExhibit";
            this.cmdClearExhibit.Size = new System.Drawing.Size(75, 55);
            this.cmdClearExhibit.TabIndex = 12;
            this.cmdClearExhibit.Text = "Clear Exhibit/ Purchase";
            this.cmdClearExhibit.UseVisualStyleBackColor = true;
            this.cmdClearExhibit.Click += new System.EventHandler(this.cmdClearExhibit_Click);
            // 
            // radAdditionalPayment
            // 
            this.radAdditionalPayment.Appearance = System.Windows.Forms.Appearance.Button;
            this.radAdditionalPayment.Checked = true;
            this.radAdditionalPayment.Location = new System.Drawing.Point(12, 85);
            this.radAdditionalPayment.Name = "radAdditionalPayment";
            this.radAdditionalPayment.Size = new System.Drawing.Size(247, 24);
            this.radAdditionalPayment.TabIndex = 1;
            this.radAdditionalPayment.TabStop = true;
            this.radAdditionalPayment.Text = "Additional Payment";
            this.radAdditionalPayment.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radAdditionalPayment.UseVisualStyleBackColor = true;
            this.radAdditionalPayment.CheckedChanged += new System.EventHandler(this.radAdditionalPayment_CheckedChanged);
            // 
            // radNewPurchase
            // 
            this.radNewPurchase.Appearance = System.Windows.Forms.Appearance.Button;
            this.radNewPurchase.Location = new System.Drawing.Point(265, 86);
            this.radNewPurchase.Name = "radNewPurchase";
            this.radNewPurchase.Size = new System.Drawing.Size(247, 23);
            this.radNewPurchase.TabIndex = 2;
            this.radNewPurchase.Text = "New Purchase";
            this.radNewPurchase.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radNewPurchase.UseVisualStyleBackColor = true;
            this.radNewPurchase.CheckedChanged += new System.EventHandler(this.radNewPurchase_CheckedChanged);
            // 
            // MarketItemEntry
            // 
            this.MarketItemEntry.Enabled = false;
            this.MarketItemEntry.Location = new System.Drawing.Point(12, 115);
            this.MarketItemEntry.MarketItem = null;
            this.MarketItemEntry.Mode = Livestock_Auction.DataEntry.EntryModes.Lookup;
            this.MarketItemEntry.Name = "MarketItemEntry";
            this.MarketItemEntry.Size = new System.Drawing.Size(500, 67);
            this.MarketItemEntry.TabIndex = 3;
            // 
            // ExhibitorEntry
            // 
            this.ExhibitorEntry.Exhibitor = null;
            this.ExhibitorEntry.Location = new System.Drawing.Point(12, 12);
            this.ExhibitorEntry.Mode = Livestock_Auction.DataEntry.EntryModes.Lookup;
            this.ExhibitorEntry.Name = "ExhibitorEntry";
            this.ExhibitorEntry.Size = new System.Drawing.Size(500, 67);
            this.ExhibitorEntry.TabIndex = 0;
            this.ExhibitorEntry.Leave += new System.EventHandler(this.ExhibitorEntry_Leave);
            // 
            // frmNewPurchase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(601, 289);
            this.Controls.Add(this.radNewPurchase);
            this.Controls.Add(this.radAdditionalPayment);
            this.Controls.Add(this.cmdClearExhibit);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cmdClearMarketItem);
            this.Controls.Add(this.cmdClearExhibitor);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdOk);
            this.Controls.Add(this.grbExhibit);
            this.Controls.Add(this.MarketItemEntry);
            this.Controls.Add(this.ExhibitorEntry);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmNewPurchase";
            this.Text = "Add New Purchase";
            this.Load += new System.EventHandler(this.frmNewPurchase_Load);
            this.grbExhibit.ResumeLayout(false);
            this.grbExhibit.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Livestock_Auction.DataEntry.ucExhibitorEntry ExhibitorEntry;
        private Livestock_Auction.DataEntry.ucMarketItemEntry MarketItemEntry;
        private System.Windows.Forms.GroupBox grbExhibit;
        private System.Windows.Forms.Label lblTagNumber;
        private System.Windows.Forms.TextBox txtTagNumber;
        private System.Windows.Forms.Label lblWeight;
        private System.Windows.Forms.TextBox txtChampion;
        private System.Windows.Forms.Label lblChampion;
        private System.Windows.Forms.TextBox txtWeight;
        private System.Windows.Forms.Button cmdOk;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Button cmdNewTag;
        private System.Windows.Forms.Button cmdClearExhibitor;
        private System.Windows.Forms.Button cmdClearMarketItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtPurchasePrice;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cmdClearExhibit;
        private System.Windows.Forms.RadioButton radTakeBackNo;
        private System.Windows.Forms.RadioButton radTakeBackYes;
        private System.Windows.Forms.Label lblTakeBackYes;
        private System.Windows.Forms.RadioButton radAdditionalPayment;
        private System.Windows.Forms.RadioButton radNewPurchase;
        private System.Windows.Forms.CheckBox chkRateOfGain;
        private System.Windows.Forms.Label label2;
    }
}