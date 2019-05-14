namespace Livestock_Auction.DataEntry
{
    partial class ucExhibitorEntry
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
            this.grpEntry = new System.Windows.Forms.GroupBox();
            this.cmdBrowseFirstName = new System.Windows.Forms.Button();
            this.cmdBrowseLastName = new System.Windows.Forms.Button();
            this.txtNameNick = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblExhibitorWarning = new System.Windows.Forms.Label();
            this.lblNameLast = new System.Windows.Forms.Label();
            this.lblNameFirst = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNameLast = new System.Windows.Forms.TextBox();
            this.txtNameFirst = new System.Windows.Forms.TextBox();
            this.txtExhibitorNumber = new System.Windows.Forms.TextBox();
            this.grpEntry.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpEntry
            // 
            this.grpEntry.Controls.Add(this.cmdBrowseFirstName);
            this.grpEntry.Controls.Add(this.cmdBrowseLastName);
            this.grpEntry.Controls.Add(this.txtNameNick);
            this.grpEntry.Controls.Add(this.label2);
            this.grpEntry.Controls.Add(this.lblExhibitorWarning);
            this.grpEntry.Controls.Add(this.lblNameLast);
            this.grpEntry.Controls.Add(this.lblNameFirst);
            this.grpEntry.Controls.Add(this.label1);
            this.grpEntry.Controls.Add(this.txtNameLast);
            this.grpEntry.Controls.Add(this.txtNameFirst);
            this.grpEntry.Controls.Add(this.txtExhibitorNumber);
            this.grpEntry.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpEntry.Location = new System.Drawing.Point(0, 0);
            this.grpEntry.Name = "grpEntry";
            this.grpEntry.Size = new System.Drawing.Size(500, 67);
            this.grpEntry.TabIndex = 1;
            this.grpEntry.TabStop = false;
            this.grpEntry.Text = "Exhibitor";
            // 
            // cmdBrowseFirstName
            // 
            this.cmdBrowseFirstName.Location = new System.Drawing.Point(158, 32);
            this.cmdBrowseFirstName.Name = "cmdBrowseFirstName";
            this.cmdBrowseFirstName.Size = new System.Drawing.Size(24, 20);
            this.cmdBrowseFirstName.TabIndex = 6;
            this.cmdBrowseFirstName.Text = "...";
            this.cmdBrowseFirstName.UseVisualStyleBackColor = true;
            this.cmdBrowseFirstName.Click += new System.EventHandler(this.cmdBrowseFirstName_Click);
            // 
            // cmdBrowseLastName
            // 
            this.cmdBrowseLastName.Location = new System.Drawing.Point(282, 32);
            this.cmdBrowseLastName.Name = "cmdBrowseLastName";
            this.cmdBrowseLastName.Size = new System.Drawing.Size(24, 20);
            this.cmdBrowseLastName.TabIndex = 9;
            this.cmdBrowseLastName.Text = "...";
            this.cmdBrowseLastName.UseVisualStyleBackColor = true;
            this.cmdBrowseLastName.Click += new System.EventHandler(this.cmdBrowseLastName_Click);
            // 
            // txtNameNick
            // 
            this.txtNameNick.Location = new System.Drawing.Point(312, 32);
            this.txtNameNick.Name = "txtNameNick";
            this.txtNameNick.Size = new System.Drawing.Size(98, 20);
            this.txtNameNick.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(309, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Nick Name";
            // 
            // lblExhibitorWarning
            // 
            this.lblExhibitorWarning.ForeColor = System.Drawing.Color.Red;
            this.lblExhibitorWarning.Location = new System.Drawing.Point(421, 23);
            this.lblExhibitorWarning.Name = "lblExhibitorWarning";
            this.lblExhibitorWarning.Size = new System.Drawing.Size(76, 36);
            this.lblExhibitorWarning.TabIndex = 10;
            this.lblExhibitorWarning.Text = "{0} will be added";
            this.lblExhibitorWarning.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblExhibitorWarning.Visible = false;
            // 
            // lblNameLast
            // 
            this.lblNameLast.AutoSize = true;
            this.lblNameLast.Location = new System.Drawing.Point(185, 16);
            this.lblNameLast.Name = "lblNameLast";
            this.lblNameLast.Size = new System.Drawing.Size(58, 13);
            this.lblNameLast.TabIndex = 7;
            this.lblNameLast.Text = "Last Name";
            // 
            // lblNameFirst
            // 
            this.lblNameFirst.AutoSize = true;
            this.lblNameFirst.Location = new System.Drawing.Point(60, 16);
            this.lblNameFirst.Name = "lblNameFirst";
            this.lblNameFirst.Size = new System.Drawing.Size(57, 13);
            this.lblNameFirst.TabIndex = 4;
            this.lblNameFirst.Text = "First Name";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Number";
            // 
            // txtNameLast
            // 
            this.txtNameLast.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtNameLast.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtNameLast.Location = new System.Drawing.Point(188, 32);
            this.txtNameLast.Name = "txtNameLast";
            this.txtNameLast.Size = new System.Drawing.Size(98, 20);
            this.txtNameLast.TabIndex = 8;
            this.txtNameLast.Leave += new System.EventHandler(this.txtNameLast_Leave);
            // 
            // txtNameFirst
            // 
            this.txtNameFirst.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtNameFirst.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtNameFirst.Location = new System.Drawing.Point(63, 32);
            this.txtNameFirst.Name = "txtNameFirst";
            this.txtNameFirst.Size = new System.Drawing.Size(98, 20);
            this.txtNameFirst.TabIndex = 4;
            this.txtNameFirst.Leave += new System.EventHandler(this.txtNameFirst_Leave);
            // 
            // txtExhibitorNumber
            // 
            this.txtExhibitorNumber.Location = new System.Drawing.Point(6, 32);
            this.txtExhibitorNumber.Name = "txtExhibitorNumber";
            this.txtExhibitorNumber.Size = new System.Drawing.Size(51, 20);
            this.txtExhibitorNumber.TabIndex = 3;
            this.txtExhibitorNumber.Leave += new System.EventHandler(this.txtExhibitorNumber_Leave);
            // 
            // ucExhibitorEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpEntry);
            this.Name = "ucExhibitorEntry";
            this.Size = new System.Drawing.Size(500, 67);
            this.Load += new System.EventHandler(this.ucExhibitorEntry_Load);
            this.grpEntry.ResumeLayout(false);
            this.grpEntry.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpEntry;
        private System.Windows.Forms.Label lblExhibitorWarning;
        private System.Windows.Forms.Label lblNameLast;
        private System.Windows.Forms.Label lblNameFirst;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNameLast;
        private System.Windows.Forms.TextBox txtNameFirst;
        private System.Windows.Forms.TextBox txtExhibitorNumber;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtNameNick;
        private System.Windows.Forms.Button cmdBrowseLastName;
        private System.Windows.Forms.Button cmdBrowseFirstName;
    }
}
