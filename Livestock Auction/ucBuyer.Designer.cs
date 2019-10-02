namespace Livestock_Auction
{
    partial class ucBuyer
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tscmdToggleCheckout = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
            this.tstxtFilterNumber = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tstxtFilterNameFirst = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.tstxtFilterNameLast = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.tstxtFilterCompanyName = new System.Windows.Forms.ToolStripTextBox();
            this.tscmdFilterHasPurchases = new System.Windows.Forms.ToolStripButton();
            this.tscmdFilterCheckedOut = new System.Windows.Forms.ToolStripButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtPhoneNumber = new System.Windows.Forms.TextBox();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtCity = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtZip = new System.Windows.Forms.TextBox();
            this.txtState = new System.Windows.Forms.TextBox();
            this.lsvBuyers = new System.Windows.Forms.ListView();
            this.colBuyerNumber = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colCompany = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colPhone = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colAddress = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colPurchases = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colCheckedOut = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtCompanyName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBuyerNumber = new System.Windows.Forms.TextBox();
            this.lblNameLast = new System.Windows.Forms.Label();
            this.txtNameLast = new System.Windows.Forms.TextBox();
            this.lblNameFirst = new System.Windows.Forms.Label();
            this.txtNameFirst = new System.Windows.Forms.TextBox();
            this.cmdCopy = new System.Windows.Forms.Button();
            this.cmdDelete = new System.Windows.Forms.Button();
            this.cmdNew = new System.Windows.Forms.Button();
            this.cmdCommit = new System.Windows.Forms.Button();
            this.panMain = new System.Windows.Forms.Panel();
            this.toolStrip1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tscmdToggleCheckout,
            this.toolStripSeparator1,
            this.toolStripLabel4,
            this.tstxtFilterNumber,
            this.toolStripLabel1,
            this.tstxtFilterNameFirst,
            this.toolStripLabel2,
            this.tstxtFilterNameLast,
            this.toolStripLabel3,
            this.tstxtFilterCompanyName,
            this.tscmdFilterHasPurchases,
            this.tscmdFilterCheckedOut});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1055, 25);
            this.toolStrip1.TabIndex = 26;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tscmdToggleCheckout
            // 
            this.tscmdToggleCheckout.Enabled = false;
            this.tscmdToggleCheckout.Image = global::Livestock_Auction.Properties.Resources.check;
            this.tscmdToggleCheckout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tscmdToggleCheckout.Name = "tscmdToggleCheckout";
            this.tscmdToggleCheckout.Size = new System.Drawing.Size(126, 22);
            this.tscmdToggleCheckout.Text = "Mark Checked Out";
            this.tscmdToggleCheckout.Click += new System.EventHandler(this.tscmdToggleCheckout_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel4
            // 
            this.toolStripLabel4.Name = "toolStripLabel4";
            this.toolStripLabel4.Size = new System.Drawing.Size(51, 22);
            this.toolStripLabel4.Text = "Number";
            // 
            // tstxtFilterNumber
            // 
            this.tstxtFilterNumber.Name = "tstxtFilterNumber";
            this.tstxtFilterNumber.Size = new System.Drawing.Size(100, 25);
            this.tstxtFilterNumber.TextChanged += new System.EventHandler(this.Filter_Changed);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(67, 22);
            this.toolStripLabel1.Text = "First Name:";
            // 
            // tstxtFilterNameFirst
            // 
            this.tstxtFilterNameFirst.Name = "tstxtFilterNameFirst";
            this.tstxtFilterNameFirst.Size = new System.Drawing.Size(100, 25);
            this.tstxtFilterNameFirst.TextChanged += new System.EventHandler(this.Filter_Changed);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(66, 22);
            this.toolStripLabel2.Text = "Last Name:";
            // 
            // tstxtFilterNameLast
            // 
            this.tstxtFilterNameLast.Name = "tstxtFilterNameLast";
            this.tstxtFilterNameLast.Size = new System.Drawing.Size(100, 25);
            this.tstxtFilterNameLast.TextChanged += new System.EventHandler(this.Filter_Changed);
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(59, 22);
            this.toolStripLabel3.Text = "Company";
            // 
            // tstxtFilterCompanyName
            // 
            this.tstxtFilterCompanyName.Name = "tstxtFilterCompanyName";
            this.tstxtFilterCompanyName.Size = new System.Drawing.Size(100, 25);
            this.tstxtFilterCompanyName.TextChanged += new System.EventHandler(this.Filter_Changed);
            // 
            // tscmdFilterHasPurchases
            // 
            this.tscmdFilterHasPurchases.Image = global::Livestock_Auction.Properties.Resources.filter;
            this.tscmdFilterHasPurchases.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tscmdFilterHasPurchases.Name = "tscmdFilterHasPurchases";
            this.tscmdFilterHasPurchases.Size = new System.Drawing.Size(80, 22);
            this.tscmdFilterHasPurchases.Text = "Purchases";
            this.tscmdFilterHasPurchases.ToolTipText = "Filter by \"Purchases\"";
            this.tscmdFilterHasPurchases.CheckedChanged += new System.EventHandler(this.Filter_Changed);
            this.tscmdFilterHasPurchases.Click += new System.EventHandler(this.tscmdFilterHasPurchases_Click);
            // 
            // tscmdFilterCheckedOut
            // 
            this.tscmdFilterCheckedOut.Image = global::Livestock_Auction.Properties.Resources.filter;
            this.tscmdFilterCheckedOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tscmdFilterCheckedOut.Name = "tscmdFilterCheckedOut";
            this.tscmdFilterCheckedOut.Size = new System.Drawing.Size(96, 22);
            this.tscmdFilterCheckedOut.Text = "Checked Out";
            this.tscmdFilterCheckedOut.ToolTipText = "Filter by \"Checked Out\"";
            this.tscmdFilterCheckedOut.CheckedChanged += new System.EventHandler(this.Filter_Changed);
            this.tscmdFilterCheckedOut.Click += new System.EventHandler(this.tscmdFilterCheckedOut_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.txtPhoneNumber);
            this.groupBox3.Controls.Add(this.txtAddress);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.txtCity);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.txtZip);
            this.groupBox3.Controls.Add(this.txtState);
            this.groupBox3.Location = new System.Drawing.Point(0, 344);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(493, 65);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Address";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(390, 20);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(78, 13);
            this.label8.TabIndex = 18;
            this.label8.Text = "Phone Number";
            // 
            // txtPhoneNumber
            // 
            this.txtPhoneNumber.Enabled = false;
            this.txtPhoneNumber.Location = new System.Drawing.Point(390, 36);
            this.txtPhoneNumber.MaxLength = 20;
            this.txtPhoneNumber.Name = "txtPhoneNumber";
            this.txtPhoneNumber.Size = new System.Drawing.Size(97, 20);
            this.txtPhoneNumber.TabIndex = 23;
            // 
            // txtAddress
            // 
            this.txtAddress.Enabled = false;
            this.txtAddress.Location = new System.Drawing.Point(6, 36);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(173, 20);
            this.txtAddress.TabIndex = 19;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(220, 20);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(24, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "City";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(328, 20);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(22, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "Zip";
            // 
            // txtCity
            // 
            this.txtCity.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtCity.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtCity.Enabled = false;
            this.txtCity.Location = new System.Drawing.Point(223, 36);
            this.txtCity.Name = "txtCity";
            this.txtCity.Size = new System.Drawing.Size(102, 20);
            this.txtCity.TabIndex = 21;
            this.txtCity.Leave += new System.EventHandler(this.txtCity_Leave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(180, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "State";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Street Address";
            // 
            // txtZip
            // 
            this.txtZip.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtZip.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtZip.Enabled = false;
            this.txtZip.Location = new System.Drawing.Point(331, 36);
            this.txtZip.Name = "txtZip";
            this.txtZip.Size = new System.Drawing.Size(53, 20);
            this.txtZip.TabIndex = 22;
            this.txtZip.Leave += new System.EventHandler(this.txtZip_Leave);
            // 
            // txtState
            // 
            this.txtState.AutoCompleteCustomSource.AddRange(new string[] {
            "AL",
            "AK",
            "AS",
            "AZ",
            "AR",
            "CA",
            "CO",
            "CT",
            "DE",
            "DC",
            "FM",
            "FL",
            "GA",
            "GU",
            "HI",
            "ID",
            "IL",
            "IN",
            "IA",
            "KS",
            "KY",
            "LA",
            "ME",
            "MH",
            "MD",
            "MA",
            "MI",
            "MN",
            "MS",
            "MO",
            "MT",
            "NE",
            "NV",
            "NH",
            "NJ",
            "NM",
            "NY",
            "NC",
            "ND",
            "MP",
            "OH",
            "OK",
            "OR",
            "PW",
            "PA",
            "PR",
            "RI",
            "SC",
            "SD",
            "TN",
            "TX",
            "UT",
            "VT",
            "VI",
            "VA",
            "WA",
            "WV",
            "WI",
            "WY"});
            this.txtState.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtState.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtState.Enabled = false;
            this.txtState.Location = new System.Drawing.Point(183, 36);
            this.txtState.Name = "txtState";
            this.txtState.Size = new System.Drawing.Size(34, 20);
            this.txtState.TabIndex = 20;
            this.txtState.Leave += new System.EventHandler(this.txtState_Leave);
            // 
            // lsvBuyers
            // 
            this.lsvBuyers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lsvBuyers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colBuyerNumber,
            this.colName,
            this.colCompany,
            this.colPhone,
            this.colAddress,
            this.colPurchases,
            this.colCheckedOut});
            this.lsvBuyers.FullRowSelect = true;
            this.lsvBuyers.HideSelection = false;
            this.lsvBuyers.Location = new System.Drawing.Point(0, 0);
            this.lsvBuyers.Name = "lsvBuyers";
            this.lsvBuyers.Size = new System.Drawing.Size(1055, 241);
            this.lsvBuyers.TabIndex = 0;
            this.lsvBuyers.UseCompatibleStateImageBehavior = false;
            this.lsvBuyers.View = System.Windows.Forms.View.Details;
            this.lsvBuyers.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lsvBuyers_ColumnClick);
            this.lsvBuyers.SelectedIndexChanged += new System.EventHandler(this.lsvBuyers_SelectedIndexChanged);
            // 
            // colBuyerNumber
            // 
            this.colBuyerNumber.Text = "Buyer Number";
            this.colBuyerNumber.Width = 79;
            // 
            // colName
            // 
            this.colName.Text = "Name";
            this.colName.Width = 115;
            // 
            // colCompany
            // 
            this.colCompany.Text = "Company";
            this.colCompany.Width = 170;
            // 
            // colPhone
            // 
            this.colPhone.Text = "Phone Number";
            this.colPhone.Width = 88;
            // 
            // colAddress
            // 
            this.colAddress.Text = "Address";
            this.colAddress.Width = 201;
            // 
            // colPurchases
            // 
            this.colPurchases.Text = "Purchases";
            this.colPurchases.Width = 67;
            // 
            // colCheckedOut
            // 
            this.colCheckedOut.Text = "Checked Out";
            this.colCheckedOut.Width = 94;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.txtCompanyName);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.txtBuyerNumber);
            this.groupBox2.Controls.Add(this.lblNameLast);
            this.groupBox2.Controls.Add(this.txtNameLast);
            this.groupBox2.Controls.Add(this.lblNameFirst);
            this.groupBox2.Controls.Add(this.txtNameFirst);
            this.groupBox2.Location = new System.Drawing.Point(0, 273);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(493, 65);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Name";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(281, 16);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(82, 13);
            this.label9.TabIndex = 8;
            this.label9.Text = "Company Name";
            // 
            // txtCompanyName
            // 
            this.txtCompanyName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtCompanyName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtCompanyName.Enabled = false;
            this.txtCompanyName.Location = new System.Drawing.Point(284, 32);
            this.txtCompanyName.Name = "txtCompanyName";
            this.txtCompanyName.Size = new System.Drawing.Size(203, 20);
            this.txtCompanyName.TabIndex = 12;
            this.txtCompanyName.Enter += new System.EventHandler(this.txtCompanyName_Enter);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Buyer Number";
            // 
            // txtBuyerNumber
            // 
            this.txtBuyerNumber.Location = new System.Drawing.Point(6, 32);
            this.txtBuyerNumber.Name = "txtBuyerNumber";
            this.txtBuyerNumber.Size = new System.Drawing.Size(74, 20);
            this.txtBuyerNumber.TabIndex = 9;
            this.txtBuyerNumber.Leave += new System.EventHandler(this.txtBuyerNumber_Leave);
            // 
            // lblNameLast
            // 
            this.lblNameLast.AutoSize = true;
            this.lblNameLast.Location = new System.Drawing.Point(83, 16);
            this.lblNameLast.Name = "lblNameLast";
            this.lblNameLast.Size = new System.Drawing.Size(58, 13);
            this.lblNameLast.TabIndex = 6;
            this.lblNameLast.Text = "Last Name";
            // 
            // txtNameLast
            // 
            this.txtNameLast.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtNameLast.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtNameLast.Enabled = false;
            this.txtNameLast.Location = new System.Drawing.Point(86, 32);
            this.txtNameLast.Name = "txtNameLast";
            this.txtNameLast.Size = new System.Drawing.Size(93, 20);
            this.txtNameLast.TabIndex = 10;
            this.txtNameLast.Enter += new System.EventHandler(this.txtNameLast_Enter);
            // 
            // lblNameFirst
            // 
            this.lblNameFirst.AutoSize = true;
            this.lblNameFirst.Location = new System.Drawing.Point(182, 16);
            this.lblNameFirst.Name = "lblNameFirst";
            this.lblNameFirst.Size = new System.Drawing.Size(57, 13);
            this.lblNameFirst.TabIndex = 7;
            this.lblNameFirst.Text = "First Name";
            // 
            // txtNameFirst
            // 
            this.txtNameFirst.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtNameFirst.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtNameFirst.Enabled = false;
            this.txtNameFirst.Location = new System.Drawing.Point(185, 32);
            this.txtNameFirst.Name = "txtNameFirst";
            this.txtNameFirst.Size = new System.Drawing.Size(93, 20);
            this.txtNameFirst.TabIndex = 11;
            this.txtNameFirst.Enter += new System.EventHandler(this.txtNameFirst_Enter);
            // 
            // cmdCopy
            // 
            this.cmdCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdCopy.Location = new System.Drawing.Point(94, 247);
            this.cmdCopy.Name = "cmdCopy";
            this.cmdCopy.Size = new System.Drawing.Size(88, 20);
            this.cmdCopy.TabIndex = 2;
            this.cmdCopy.Text = "Copy Record";
            this.cmdCopy.UseVisualStyleBackColor = true;
            this.cmdCopy.Click += new System.EventHandler(this.cmdCopy_Click);
            // 
            // cmdDelete
            // 
            this.cmdDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdDelete.Location = new System.Drawing.Point(188, 247);
            this.cmdDelete.Name = "cmdDelete";
            this.cmdDelete.Size = new System.Drawing.Size(88, 20);
            this.cmdDelete.TabIndex = 3;
            this.cmdDelete.Text = "Delete Record";
            this.cmdDelete.UseVisualStyleBackColor = true;
            this.cmdDelete.Click += new System.EventHandler(this.cmdDelete_Click);
            // 
            // cmdNew
            // 
            this.cmdNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdNew.Location = new System.Drawing.Point(0, 247);
            this.cmdNew.Name = "cmdNew";
            this.cmdNew.Size = new System.Drawing.Size(88, 20);
            this.cmdNew.TabIndex = 1;
            this.cmdNew.Text = "New Record";
            this.cmdNew.UseVisualStyleBackColor = true;
            this.cmdNew.Click += new System.EventHandler(this.cmdNew_Click);
            // 
            // cmdCommit
            // 
            this.cmdCommit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdCommit.Location = new System.Drawing.Point(499, 273);
            this.cmdCommit.Name = "cmdCommit";
            this.cmdCommit.Size = new System.Drawing.Size(75, 65);
            this.cmdCommit.TabIndex = 24;
            this.cmdCommit.Text = "Commit >>";
            this.cmdCommit.UseVisualStyleBackColor = true;
            this.cmdCommit.Click += new System.EventHandler(this.cmdCommit_Click);
            // 
            // panMain
            // 
            this.panMain.Controls.Add(this.cmdCommit);
            this.panMain.Controls.Add(this.cmdNew);
            this.panMain.Controls.Add(this.cmdDelete);
            this.panMain.Controls.Add(this.cmdCopy);
            this.panMain.Controls.Add(this.groupBox2);
            this.panMain.Controls.Add(this.lsvBuyers);
            this.panMain.Controls.Add(this.groupBox3);
            this.panMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panMain.Location = new System.Drawing.Point(0, 25);
            this.panMain.Name = "panMain";
            this.panMain.Size = new System.Drawing.Size(1055, 412);
            this.panMain.TabIndex = 25;
            // 
            // ucBuyer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panMain);
            this.Controls.Add(this.toolStrip1);
            this.Name = "ucBuyer";
            this.Size = new System.Drawing.Size(1055, 437);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panMain.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tscmdToggleCheckout;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtPhoneNumber;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtCity;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtZip;
        private System.Windows.Forms.TextBox txtState;
        private System.Windows.Forms.ListView lsvBuyers;
        private System.Windows.Forms.ColumnHeader colBuyerNumber;
        private System.Windows.Forms.ColumnHeader colName;
        private System.Windows.Forms.ColumnHeader colCompany;
        private System.Windows.Forms.ColumnHeader colPhone;
        private System.Windows.Forms.ColumnHeader colAddress;
        private System.Windows.Forms.ColumnHeader colCheckedOut;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtCompanyName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBuyerNumber;
        private System.Windows.Forms.Label lblNameLast;
        private System.Windows.Forms.TextBox txtNameLast;
        private System.Windows.Forms.Label lblNameFirst;
        private System.Windows.Forms.TextBox txtNameFirst;
        private System.Windows.Forms.Button cmdCopy;
        private System.Windows.Forms.Button cmdDelete;
        private System.Windows.Forms.Button cmdNew;
        private System.Windows.Forms.Button cmdCommit;
        private System.Windows.Forms.Panel panMain;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox tstxtFilterNameFirst;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripTextBox tstxtFilterNameLast;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripTextBox tstxtFilterCompanyName;
        private System.Windows.Forms.ColumnHeader colPurchases;
        private System.Windows.Forms.ToolStripButton tscmdFilterHasPurchases;
        private System.Windows.Forms.ToolStripButton tscmdFilterCheckedOut;
        private System.Windows.Forms.ToolStripLabel toolStripLabel4;
        private System.Windows.Forms.ToolStripTextBox tstxtFilterNumber;
    }
}
