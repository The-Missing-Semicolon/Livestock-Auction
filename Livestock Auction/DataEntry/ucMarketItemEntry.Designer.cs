namespace Livestock_Auction.DataEntry
{
    partial class ucMarketItemEntry
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chkAllowAdvertising = new System.Windows.Forms.CheckBox();
            this.txtMarketUnits = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblMarketWarning = new System.Windows.Forms.Label();
            this.txtMarketID = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtMarketValue = new System.Windows.Forms.TextBox();
            this.txtMarketItem = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.chkValidDisposition = new System.Windows.Forms.CheckBox();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkValidDisposition);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.chkAllowAdvertising);
            this.groupBox2.Controls.Add(this.txtMarketUnits);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.lblMarketWarning);
            this.groupBox2.Controls.Add(this.txtMarketID);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txtMarketValue);
            this.groupBox2.Controls.Add(this.txtMarketItem);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(473, 67);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Market Item";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(289, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Advertising";
            // 
            // chkAllowAdvertising
            // 
            this.chkAllowAdvertising.AutoSize = true;
            this.chkAllowAdvertising.Location = new System.Drawing.Point(292, 34);
            this.chkAllowAdvertising.Name = "chkAllowAdvertising";
            this.chkAllowAdvertising.Size = new System.Drawing.Size(51, 17);
            this.chkAllowAdvertising.TabIndex = 11;
            this.chkAllowAdvertising.Text = "Allow";
            this.chkAllowAdvertising.UseVisualStyleBackColor = true;
            // 
            // txtMarketUnits
            // 
            this.txtMarketUnits.Location = new System.Drawing.Point(225, 31);
            this.txtMarketUnits.Name = "txtMarketUnits";
            this.txtMarketUnits.Size = new System.Drawing.Size(61, 20);
            this.txtMarketUnits.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(222, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Units";
            // 
            // lblMarketWarning
            // 
            this.lblMarketWarning.ForeColor = System.Drawing.Color.Red;
            this.lblMarketWarning.Location = new System.Drawing.Point(418, 15);
            this.lblMarketWarning.Name = "lblMarketWarning";
            this.lblMarketWarning.Size = new System.Drawing.Size(55, 49);
            this.lblMarketWarning.TabIndex = 14;
            this.lblMarketWarning.Text = "Market item will be added";
            this.lblMarketWarning.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblMarketWarning.Visible = false;
            // 
            // txtMarketID
            // 
            this.txtMarketID.Location = new System.Drawing.Point(6, 32);
            this.txtMarketID.Name = "txtMarketID";
            this.txtMarketID.Size = new System.Drawing.Size(51, 20);
            this.txtMarketID.TabIndex = 3;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 16);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(18, 13);
            this.label8.TabIndex = 2;
            this.label8.Text = "ID";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(156, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(34, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Value";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(60, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(27, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Item";
            // 
            // txtMarketValue
            // 
            this.txtMarketValue.Location = new System.Drawing.Point(159, 32);
            this.txtMarketValue.Name = "txtMarketValue";
            this.txtMarketValue.Size = new System.Drawing.Size(60, 20);
            this.txtMarketValue.TabIndex = 7;
            this.txtMarketValue.Leave += new System.EventHandler(this.txtMarketValue_Leave);
            // 
            // txtMarketItem
            // 
            this.txtMarketItem.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtMarketItem.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtMarketItem.Location = new System.Drawing.Point(63, 32);
            this.txtMarketItem.Name = "txtMarketItem";
            this.txtMarketItem.Size = new System.Drawing.Size(90, 20);
            this.txtMarketItem.TabIndex = 5;
            this.txtMarketItem.Leave += new System.EventHandler(this.txtMarketItem_Leave);
            this.txtMarketItem.Enter += new System.EventHandler(this.txtMarketItem_Enter);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(354, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Disposition";
            // 
            // chkValidDisposition
            // 
            this.chkValidDisposition.AutoSize = true;
            this.chkValidDisposition.Location = new System.Drawing.Point(357, 34);
            this.chkValidDisposition.Name = "chkValidDisposition";
            this.chkValidDisposition.Size = new System.Drawing.Size(49, 17);
            this.chkValidDisposition.TabIndex = 13;
            this.chkValidDisposition.Text = "Valid";
            this.chkValidDisposition.UseVisualStyleBackColor = true;
            // 
            // ucMarketItemEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox2);
            this.Name = "ucMarketItemEntry";
            this.Size = new System.Drawing.Size(473, 67);
            this.Load += new System.EventHandler(this.ucMarketItemEntry_Load);
            this.Enter += new System.EventHandler(this.ucMarketItemEntry_Enter);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblMarketWarning;
        private System.Windows.Forms.TextBox txtMarketID;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtMarketValue;
        private System.Windows.Forms.TextBox txtMarketItem;
        private System.Windows.Forms.TextBox txtMarketUnits;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkAllowAdvertising;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chkValidDisposition;
    }
}
