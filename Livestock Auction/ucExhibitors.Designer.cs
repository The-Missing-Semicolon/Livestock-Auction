namespace Livestock_Auction
{
    partial class ucExhibitors
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucExhibitors));
            this.txtExhibitorNumber = new System.Windows.Forms.TextBox();
            this.txtNameLast = new System.Windows.Forms.TextBox();
            this.txtNameFirst = new System.Windows.Forms.TextBox();
            this.txtZip = new System.Windows.Forms.TextBox();
            this.txtState = new System.Windows.Forms.TextBox();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblNameLast = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblNameFirst = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.chkAddresses = new System.Windows.Forms.CheckBox();
            this.cmdCommit = new System.Windows.Forms.Button();
            this.txtCity = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmdNew = new System.Windows.Forms.Button();
            this.lsvExhibitors = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmdDelete = new System.Windows.Forms.Button();
            this.cmdCopy = new System.Windows.Forms.Button();
            this.panMain = new System.Windows.Forms.Panel();
            this.panDisplay = new System.Windows.Forms.Panel();
            this.tsFilter = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.tstxtFilterNumber = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tstxtFilterNameFirst = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.tstxtFilterNameLast = new System.Windows.Forms.ToolStripTextBox();
            this.tscmdFilterHasExhibits = new System.Windows.Forms.ToolStripButton();
            this.splMain = new System.Windows.Forms.Splitter();
            this.panEntry = new System.Windows.Forms.Panel();
            this.elvExhibits = new Livestock_Auction.Helpers.EditableListView();
            this.imglstExhibits = new System.Windows.Forms.ImageList(this.components);
            this.grbAddress = new System.Windows.Forms.GroupBox();
            this.cmdCheckout = new System.Windows.Forms.Button();
            this.grbName = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNameNick = new System.Windows.Forms.TextBox();
            this.panSub = new System.Windows.Forms.Panel();
            this.splSub = new System.Windows.Forms.Splitter();
            this.panMain.SuspendLayout();
            this.panDisplay.SuspendLayout();
            this.tsFilter.SuspendLayout();
            this.panEntry.SuspendLayout();
            this.grbAddress.SuspendLayout();
            this.grbName.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtExhibitorNumber
            // 
            this.txtExhibitorNumber.Location = new System.Drawing.Point(6, 32);
            this.txtExhibitorNumber.Name = "txtExhibitorNumber";
            this.txtExhibitorNumber.Size = new System.Drawing.Size(87, 20);
            this.txtExhibitorNumber.TabIndex = 1;
            this.txtExhibitorNumber.Leave += new System.EventHandler(this.txtExhibitorNumber_Leave);
            // 
            // txtNameLast
            // 
            this.txtNameLast.Enabled = false;
            this.txtNameLast.Location = new System.Drawing.Point(99, 32);
            this.txtNameLast.Name = "txtNameLast";
            this.txtNameLast.Size = new System.Drawing.Size(98, 20);
            this.txtNameLast.TabIndex = 2;
            this.txtNameLast.Leave += new System.EventHandler(this.txtNameLast_Leave);
            // 
            // txtNameFirst
            // 
            this.txtNameFirst.Enabled = false;
            this.txtNameFirst.Location = new System.Drawing.Point(203, 32);
            this.txtNameFirst.Name = "txtNameFirst";
            this.txtNameFirst.Size = new System.Drawing.Size(98, 20);
            this.txtNameFirst.TabIndex = 3;
            this.txtNameFirst.Leave += new System.EventHandler(this.txtNameFirst_Leave);
            // 
            // txtZip
            // 
            this.txtZip.Enabled = false;
            this.txtZip.Location = new System.Drawing.Point(331, 36);
            this.txtZip.Name = "txtZip";
            this.txtZip.Size = new System.Drawing.Size(53, 20);
            this.txtZip.TabIndex = 7;
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
            this.txtState.Location = new System.Drawing.Point(185, 36);
            this.txtState.Name = "txtState";
            this.txtState.Size = new System.Drawing.Size(34, 20);
            this.txtState.TabIndex = 5;
            // 
            // txtAddress
            // 
            this.txtAddress.Enabled = false;
            this.txtAddress.Location = new System.Drawing.Point(6, 36);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(173, 20);
            this.txtAddress.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "Exhibitor Number";
            // 
            // lblNameLast
            // 
            this.lblNameLast.AutoSize = true;
            this.lblNameLast.Location = new System.Drawing.Point(96, 16);
            this.lblNameLast.Name = "lblNameLast";
            this.lblNameLast.Size = new System.Drawing.Size(58, 13);
            this.lblNameLast.TabIndex = 17;
            this.lblNameLast.Text = "Last Name";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "Street Address";
            // 
            // lblNameFirst
            // 
            this.lblNameFirst.AutoSize = true;
            this.lblNameFirst.Location = new System.Drawing.Point(200, 16);
            this.lblNameFirst.Name = "lblNameFirst";
            this.lblNameFirst.Size = new System.Drawing.Size(57, 13);
            this.lblNameFirst.TabIndex = 18;
            this.lblNameFirst.Text = "First Name";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(182, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 13);
            this.label5.TabIndex = 21;
            this.label5.Text = "State";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(328, 20);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(22, 13);
            this.label6.TabIndex = 23;
            this.label6.Text = "Zip";
            // 
            // chkAddresses
            // 
            this.chkAddresses.AutoSize = true;
            this.chkAddresses.Location = new System.Drawing.Point(6, 0);
            this.chkAddresses.Name = "chkAddresses";
            this.chkAddresses.Size = new System.Drawing.Size(64, 17);
            this.chkAddresses.TabIndex = 9;
            this.chkAddresses.TabStop = false;
            this.chkAddresses.Text = "Address";
            this.chkAddresses.UseVisualStyleBackColor = true;
            this.chkAddresses.CheckedChanged += new System.EventHandler(this.chkAddresses_CheckedChanged);
            // 
            // cmdCommit
            // 
            this.cmdCommit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCommit.Location = new System.Drawing.Point(792, 26);
            this.cmdCommit.Name = "cmdCommit";
            this.cmdCommit.Size = new System.Drawing.Size(79, 140);
            this.cmdCommit.TabIndex = 8;
            this.cmdCommit.Text = "Commit>>";
            this.cmdCommit.UseVisualStyleBackColor = true;
            this.cmdCommit.Click += new System.EventHandler(this.cmdCommit_Click);
            this.cmdCommit.Enter += new System.EventHandler(this.cmdCommit_Enter);
            this.cmdCommit.Leave += new System.EventHandler(this.cmdCommit_Leave);
            // 
            // txtCity
            // 
            this.txtCity.Enabled = false;
            this.txtCity.Location = new System.Drawing.Point(223, 36);
            this.txtCity.Name = "txtCity";
            this.txtCity.Size = new System.Drawing.Size(102, 20);
            this.txtCity.TabIndex = 6;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(220, 20);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(24, 13);
            this.label7.TabIndex = 22;
            this.label7.Text = "City";
            // 
            // cmdNew
            // 
            this.cmdNew.Location = new System.Drawing.Point(0, 0);
            this.cmdNew.Name = "cmdNew";
            this.cmdNew.Size = new System.Drawing.Size(88, 20);
            this.cmdNew.TabIndex = 11;
            this.cmdNew.Text = "New Record";
            this.cmdNew.UseVisualStyleBackColor = true;
            this.cmdNew.Click += new System.EventHandler(this.cmdNew_Click);
            // 
            // lsvExhibitors
            // 
            this.lsvExhibitors.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.lsvExhibitors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lsvExhibitors.FullRowSelect = true;
            this.lsvExhibitors.HideSelection = false;
            this.lsvExhibitors.Location = new System.Drawing.Point(0, 25);
            this.lsvExhibitors.Name = "lsvExhibitors";
            this.lsvExhibitors.Size = new System.Drawing.Size(874, 383);
            this.lsvExhibitors.TabIndex = 14;
            this.lsvExhibitors.TabStop = false;
            this.lsvExhibitors.UseCompatibleStateImageBehavior = false;
            this.lsvExhibitors.View = System.Windows.Forms.View.Details;
            this.lsvExhibitors.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lsvExhibitors_ColumnClick);
            this.lsvExhibitors.SelectedIndexChanged += new System.EventHandler(this.lsvExhibitors_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Ex#";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Name";
            this.columnHeader2.Width = 126;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Address";
            this.columnHeader3.Width = 209;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Exhibits";
            // 
            // cmdDelete
            // 
            this.cmdDelete.Enabled = false;
            this.cmdDelete.Location = new System.Drawing.Point(188, 0);
            this.cmdDelete.Name = "cmdDelete";
            this.cmdDelete.Size = new System.Drawing.Size(88, 20);
            this.cmdDelete.TabIndex = 13;
            this.cmdDelete.Text = "Delete Record";
            this.cmdDelete.UseVisualStyleBackColor = true;
            this.cmdDelete.Click += new System.EventHandler(this.cmdDelete_Click);
            // 
            // cmdCopy
            // 
            this.cmdCopy.Enabled = false;
            this.cmdCopy.Location = new System.Drawing.Point(94, 0);
            this.cmdCopy.Name = "cmdCopy";
            this.cmdCopy.Size = new System.Drawing.Size(88, 20);
            this.cmdCopy.TabIndex = 12;
            this.cmdCopy.Text = "Copy Record";
            this.cmdCopy.UseVisualStyleBackColor = true;
            this.cmdCopy.Click += new System.EventHandler(this.cmdCopy_Click);
            // 
            // panMain
            // 
            this.panMain.Controls.Add(this.panDisplay);
            this.panMain.Controls.Add(this.splMain);
            this.panMain.Controls.Add(this.panEntry);
            this.panMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panMain.Location = new System.Drawing.Point(0, 0);
            this.panMain.Name = "panMain";
            this.panMain.Size = new System.Drawing.Size(874, 577);
            this.panMain.TabIndex = 20;
            // 
            // panDisplay
            // 
            this.panDisplay.Controls.Add(this.lsvExhibitors);
            this.panDisplay.Controls.Add(this.tsFilter);
            this.panDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panDisplay.Location = new System.Drawing.Point(0, 0);
            this.panDisplay.Name = "panDisplay";
            this.panDisplay.Size = new System.Drawing.Size(874, 408);
            this.panDisplay.TabIndex = 22;
            // 
            // tsFilter
            // 
            this.tsFilter.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel3,
            this.tstxtFilterNumber,
            this.toolStripLabel1,
            this.tstxtFilterNameFirst,
            this.toolStripLabel2,
            this.tstxtFilterNameLast,
            this.tscmdFilterHasExhibits});
            this.tsFilter.Location = new System.Drawing.Point(0, 0);
            this.tsFilter.Name = "tsFilter";
            this.tsFilter.Size = new System.Drawing.Size(874, 25);
            this.tsFilter.TabIndex = 20;
            this.tsFilter.Text = "toolStrip1";
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(54, 22);
            this.toolStripLabel3.Text = "Number:";
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
            // tscmdFilterHasExhibits
            // 
            this.tscmdFilterHasExhibits.CheckOnClick = true;
            this.tscmdFilterHasExhibits.Image = ((System.Drawing.Image)(resources.GetObject("tscmdFilterHasExhibits.Image")));
            this.tscmdFilterHasExhibits.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tscmdFilterHasExhibits.Name = "tscmdFilterHasExhibits";
            this.tscmdFilterHasExhibits.Size = new System.Drawing.Size(90, 22);
            this.tscmdFilterHasExhibits.Text = "Has Exhibits";
            this.tscmdFilterHasExhibits.CheckedChanged += new System.EventHandler(this.Filter_Changed);
            // 
            // splMain
            // 
            this.splMain.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splMain.Location = new System.Drawing.Point(0, 408);
            this.splMain.Name = "splMain";
            this.splMain.Size = new System.Drawing.Size(874, 3);
            this.splMain.TabIndex = 24;
            this.splMain.TabStop = false;
            // 
            // panEntry
            // 
            this.panEntry.Controls.Add(this.cmdDelete);
            this.panEntry.Controls.Add(this.cmdCopy);
            this.panEntry.Controls.Add(this.elvExhibits);
            this.panEntry.Controls.Add(this.cmdNew);
            this.panEntry.Controls.Add(this.cmdCommit);
            this.panEntry.Controls.Add(this.grbAddress);
            this.panEntry.Controls.Add(this.cmdCheckout);
            this.panEntry.Controls.Add(this.grbName);
            this.panEntry.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panEntry.Location = new System.Drawing.Point(0, 411);
            this.panEntry.Name = "panEntry";
            this.panEntry.Size = new System.Drawing.Size(874, 166);
            this.panEntry.TabIndex = 23;
            // 
            // elvExhibits
            // 
            this.elvExhibits.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.elvExhibits.FullRowSelect = true;
            this.elvExhibits.HideSelection = false;
            this.elvExhibits.Location = new System.Drawing.Point(0, 97);
            this.elvExhibits.Name = "elvExhibits";
            this.elvExhibits.OwnerDraw = true;
            this.elvExhibits.Size = new System.Drawing.Size(786, 69);
            this.elvExhibits.SmallImageList = this.imglstExhibits;
            this.elvExhibits.TabIndex = 21;
            this.elvExhibits.Template = null;
            this.elvExhibits.UseCompatibleStateImageBehavior = false;
            this.elvExhibits.View = System.Windows.Forms.View.Details;
            // 
            // imglstExhibits
            // 
            this.imglstExhibits.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imglstExhibits.ImageStream")));
            this.imglstExhibits.TransparentColor = System.Drawing.Color.Transparent;
            this.imglstExhibits.Images.SetKeyName(0, "Invalid.png");
            this.imglstExhibits.Images.SetKeyName(1, "Valid.png");
            // 
            // grbAddress
            // 
            this.grbAddress.Controls.Add(this.txtAddress);
            this.grbAddress.Controls.Add(this.label7);
            this.grbAddress.Controls.Add(this.label6);
            this.grbAddress.Controls.Add(this.txtCity);
            this.grbAddress.Controls.Add(this.label5);
            this.grbAddress.Controls.Add(this.label3);
            this.grbAddress.Controls.Add(this.txtZip);
            this.grbAddress.Controls.Add(this.txtState);
            this.grbAddress.Controls.Add(this.chkAddresses);
            this.grbAddress.Location = new System.Drawing.Point(396, 26);
            this.grbAddress.Name = "grbAddress";
            this.grbAddress.Size = new System.Drawing.Size(390, 65);
            this.grbAddress.TabIndex = 19;
            this.grbAddress.TabStop = false;
            // 
            // cmdCheckout
            // 
            this.cmdCheckout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCheckout.Location = new System.Drawing.Point(792, 0);
            this.cmdCheckout.Name = "cmdCheckout";
            this.cmdCheckout.Size = new System.Drawing.Size(79, 20);
            this.cmdCheckout.TabIndex = 10;
            this.cmdCheckout.Text = "Checkout >>";
            this.cmdCheckout.UseVisualStyleBackColor = true;
            this.cmdCheckout.Click += new System.EventHandler(this.cmdCheckout_Click);
            // 
            // grbName
            // 
            this.grbName.Controls.Add(this.label2);
            this.grbName.Controls.Add(this.txtNameNick);
            this.grbName.Controls.Add(this.label1);
            this.grbName.Controls.Add(this.txtExhibitorNumber);
            this.grbName.Controls.Add(this.lblNameLast);
            this.grbName.Controls.Add(this.txtNameLast);
            this.grbName.Controls.Add(this.lblNameFirst);
            this.grbName.Controls.Add(this.txtNameFirst);
            this.grbName.Location = new System.Drawing.Point(0, 26);
            this.grbName.Name = "grbName";
            this.grbName.Size = new System.Drawing.Size(390, 65);
            this.grbName.TabIndex = 15;
            this.grbName.TabStop = false;
            this.grbName.Text = "Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(305, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 20;
            this.label2.Text = "Nick Name";
            // 
            // txtNameNick
            // 
            this.txtNameNick.Location = new System.Drawing.Point(308, 31);
            this.txtNameNick.Name = "txtNameNick";
            this.txtNameNick.Size = new System.Drawing.Size(76, 20);
            this.txtNameNick.TabIndex = 19;
            // 
            // panSub
            // 
            this.panSub.Dock = System.Windows.Forms.DockStyle.Right;
            this.panSub.Location = new System.Drawing.Point(877, 0);
            this.panSub.Name = "panSub";
            this.panSub.Size = new System.Drawing.Size(384, 577);
            this.panSub.TabIndex = 21;
            this.panSub.Visible = false;
            // 
            // splSub
            // 
            this.splSub.Dock = System.Windows.Forms.DockStyle.Right;
            this.splSub.Location = new System.Drawing.Point(874, 0);
            this.splSub.Name = "splSub";
            this.splSub.Size = new System.Drawing.Size(3, 577);
            this.splSub.TabIndex = 22;
            this.splSub.TabStop = false;
            this.splSub.Visible = false;
            // 
            // ucExhibitors
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panMain);
            this.Controls.Add(this.splSub);
            this.Controls.Add(this.panSub);
            this.Name = "ucExhibitors";
            this.Size = new System.Drawing.Size(1261, 577);
            this.panMain.ResumeLayout(false);
            this.panDisplay.ResumeLayout(false);
            this.panDisplay.PerformLayout();
            this.tsFilter.ResumeLayout(false);
            this.tsFilter.PerformLayout();
            this.panEntry.ResumeLayout(false);
            this.grbAddress.ResumeLayout(false);
            this.grbAddress.PerformLayout();
            this.grbName.ResumeLayout(false);
            this.grbName.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtExhibitorNumber;
        private System.Windows.Forms.TextBox txtNameLast;
        private System.Windows.Forms.TextBox txtNameFirst;
        private System.Windows.Forms.TextBox txtZip;
        private System.Windows.Forms.TextBox txtState;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblNameLast;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblNameFirst;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox chkAddresses;
        private System.Windows.Forms.Button cmdCommit;
        private System.Windows.Forms.TextBox txtCity;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button cmdNew;
        private System.Windows.Forms.ListView lsvExhibitors;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Button cmdDelete;
        private System.Windows.Forms.Button cmdCopy;
        private System.Windows.Forms.Panel panMain;
        private System.Windows.Forms.Panel panSub;
        private System.Windows.Forms.Splitter splSub;
        private System.Windows.Forms.Button cmdCheckout;
        private System.Windows.Forms.GroupBox grbName;
        private System.Windows.Forms.GroupBox grbAddress;
        private System.Windows.Forms.TextBox txtNameNick;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ToolStrip tsFilter;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox tstxtFilterNameFirst;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripTextBox tstxtFilterNameLast;
        private System.Windows.Forms.ToolStripButton tscmdFilterHasExhibits;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripTextBox tstxtFilterNumber;
        private Helpers.EditableListView elvExhibits;
        private System.Windows.Forms.Panel panDisplay;
        private System.Windows.Forms.Panel panEntry;
        private System.Windows.Forms.Splitter splMain;
        private System.Windows.Forms.ImageList imglstExhibits;
    }
}
