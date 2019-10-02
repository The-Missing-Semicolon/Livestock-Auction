namespace Livestock_Auction
{
    partial class ucBuyerCheckout
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource3 = new Microsoft.Reporting.WinForms.ReportDataSource();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucBuyerCheckout));
            this.label1 = new System.Windows.Forms.Label();
            this.txtBuyerNumber = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmdLoad = new System.Windows.Forms.Button();
            this.txtNameCompany = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblNameLast = new System.Windows.Forms.Label();
            this.txtNameLast = new System.Windows.Forms.TextBox();
            this.lblNameFirst = new System.Windows.Forms.Label();
            this.txtNameFirst = new System.Windows.Forms.TextBox();
            this.tsPurchases = new System.Windows.Forms.ToolStrip();
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabPurchases = new System.Windows.Forms.TabPage();
            this.CheckoutGrid = new Livestock_Auction.CheckoutGrid.ucCheckoutGrid();
            this.tabPayment = new System.Windows.Forms.TabPage();
            this.rptReceipt = new Microsoft.Reporting.WinForms.ReportViewer();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmdRemovePayment = new System.Windows.Forms.Button();
            this.lblRemaining = new System.Windows.Forms.Label();
            this.grbAddPayment = new System.Windows.Forms.GroupBox();
            this.radPayedCredit = new System.Windows.Forms.RadioButton();
            this.cmdAddPayment = new System.Windows.Forms.Button();
            this.txtPaymentAmount = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtCheckNumber = new System.Windows.Forms.TextBox();
            this.radPayedCash = new System.Windows.Forms.RadioButton();
            this.radPayedCheck = new System.Windows.Forms.RadioButton();
            this.lblTotalRemaining = new System.Windows.Forms.Label();
            this.cmdCommit = new System.Windows.Forms.Button();
            this.lsvPayments = new System.Windows.Forms.ListView();
            this.colMethod = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colAmount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colSurcharge = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colTotal = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtAddressee = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtStreet = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtCity = new System.Windows.Forms.TextBox();
            this.cmdClearAddr = new System.Windows.Forms.Button();
            this.txtZip = new System.Windows.Forms.TextBox();
            this.txtState = new System.Windows.Forms.TextBox();
            this.cmdCopyAddr = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.tscmdEditPurchase = new System.Windows.Forms.ToolStripButton();
            this.tscmdAddPurchase = new System.Windows.Forms.ToolStripButton();
            this.tscmdDeletePurchase = new System.Windows.Forms.ToolStripButton();
            this.clsBuyerBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.clsPurchaseBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.clsPaymentBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.groupBox1.SuspendLayout();
            this.tsPurchases.SuspendLayout();
            this.tabMain.SuspendLayout();
            this.tabPurchases.SuspendLayout();
            this.tabPayment.SuspendLayout();
            this.panel1.SuspendLayout();
            this.grbAddPayment.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.clsBuyerBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.clsPurchaseBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.clsPaymentBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "Buyer Number";
            // 
            // txtBuyerNumber
            // 
            this.txtBuyerNumber.Location = new System.Drawing.Point(6, 32);
            this.txtBuyerNumber.Name = "txtBuyerNumber";
            this.txtBuyerNumber.Size = new System.Drawing.Size(87, 20);
            this.txtBuyerNumber.TabIndex = 1;
            this.txtBuyerNumber.TextChanged += new System.EventHandler(this.txtBuyerNumber_TextChanged);
            this.txtBuyerNumber.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtBuyerNumber_KeyUp);
            this.txtBuyerNumber.Leave += new System.EventHandler(this.txtBuyerNumber_Leave);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmdLoad);
            this.groupBox1.Controls.Add(this.txtNameCompany);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtBuyerNumber);
            this.groupBox1.Controls.Add(this.lblNameLast);
            this.groupBox1.Controls.Add(this.txtNameLast);
            this.groupBox1.Controls.Add(this.lblNameFirst);
            this.groupBox1.Controls.Add(this.txtNameFirst);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(926, 58);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Buyer Information";
            // 
            // cmdLoad
            // 
            this.cmdLoad.Enabled = false;
            this.cmdLoad.Location = new System.Drawing.Point(531, 32);
            this.cmdLoad.Name = "cmdLoad";
            this.cmdLoad.Size = new System.Drawing.Size(75, 20);
            this.cmdLoad.TabIndex = 5;
            this.cmdLoad.Text = "Load";
            this.cmdLoad.UseVisualStyleBackColor = true;
            this.cmdLoad.Click += new System.EventHandler(this.cmdLoad_Click);
            // 
            // txtNameCompany
            // 
            this.txtNameCompany.Enabled = false;
            this.txtNameCompany.Location = new System.Drawing.Point(387, 32);
            this.txtNameCompany.Name = "txtNameCompany";
            this.txtNameCompany.Size = new System.Drawing.Size(138, 20);
            this.txtNameCompany.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(384, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "Company Name";
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
            // txtNameLast
            // 
            this.txtNameLast.Enabled = false;
            this.txtNameLast.Location = new System.Drawing.Point(99, 32);
            this.txtNameLast.Name = "txtNameLast";
            this.txtNameLast.Size = new System.Drawing.Size(138, 20);
            this.txtNameLast.TabIndex = 2;
            // 
            // lblNameFirst
            // 
            this.lblNameFirst.AutoSize = true;
            this.lblNameFirst.Location = new System.Drawing.Point(240, 16);
            this.lblNameFirst.Name = "lblNameFirst";
            this.lblNameFirst.Size = new System.Drawing.Size(57, 13);
            this.lblNameFirst.TabIndex = 18;
            this.lblNameFirst.Text = "First Name";
            // 
            // txtNameFirst
            // 
            this.txtNameFirst.Enabled = false;
            this.txtNameFirst.Location = new System.Drawing.Point(243, 32);
            this.txtNameFirst.Name = "txtNameFirst";
            this.txtNameFirst.Size = new System.Drawing.Size(138, 20);
            this.txtNameFirst.TabIndex = 3;
            // 
            // tsPurchases
            // 
            this.tsPurchases.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tscmdAddPurchase,
            this.tscmdEditPurchase,
            this.tscmdDeletePurchase});
            this.tsPurchases.Location = new System.Drawing.Point(3, 3);
            this.tsPurchases.Name = "tsPurchases";
            this.tsPurchases.Size = new System.Drawing.Size(912, 25);
            this.tsPurchases.TabIndex = 59;
            this.tsPurchases.Text = "toolStrip1";
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.tabPurchases);
            this.tabMain.Controls.Add(this.tabPayment);
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Location = new System.Drawing.Point(0, 58);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(926, 571);
            this.tabMain.TabIndex = 57;
            this.tabMain.SelectedIndexChanged += new System.EventHandler(this.tabMain_SelectedIndexChanged);
            // 
            // tabPurchases
            // 
            this.tabPurchases.Controls.Add(this.CheckoutGrid);
            this.tabPurchases.Controls.Add(this.tsPurchases);
            this.tabPurchases.Location = new System.Drawing.Point(4, 22);
            this.tabPurchases.Name = "tabPurchases";
            this.tabPurchases.Padding = new System.Windows.Forms.Padding(3);
            this.tabPurchases.Size = new System.Drawing.Size(918, 545);
            this.tabPurchases.TabIndex = 0;
            this.tabPurchases.Text = "1. Purchases";
            this.tabPurchases.UseVisualStyleBackColor = true;
            // 
            // CheckoutGrid
            // 
            this.CheckoutGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CheckoutGrid.Location = new System.Drawing.Point(3, 28);
            this.CheckoutGrid.Margin = new System.Windows.Forms.Padding(0);
            this.CheckoutGrid.Name = "CheckoutGrid";
            this.CheckoutGrid.Size = new System.Drawing.Size(912, 514);
            this.CheckoutGrid.TabIndex = 58;
            this.CheckoutGrid.SelectedItemChanged += new System.EventHandler<System.EventArgs>(this.CheckoutGrid_SelectedItemChanged);
            this.CheckoutGrid.TotalChanged += new System.EventHandler<System.EventArgs>(this.CheckoutGrid_TotalChanged);
            // 
            // tabPayment
            // 
            this.tabPayment.Controls.Add(this.rptReceipt);
            this.tabPayment.Controls.Add(this.splitter1);
            this.tabPayment.Controls.Add(this.panel1);
            this.tabPayment.Location = new System.Drawing.Point(4, 22);
            this.tabPayment.Name = "tabPayment";
            this.tabPayment.Padding = new System.Windows.Forms.Padding(3);
            this.tabPayment.Size = new System.Drawing.Size(918, 545);
            this.tabPayment.TabIndex = 1;
            this.tabPayment.Text = "2. Payment";
            this.tabPayment.UseVisualStyleBackColor = true;
            // 
            // rptReceipt
            // 
            this.rptReceipt.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "Livestock_Auction_clsBuyer";
            reportDataSource1.Value = this.clsBuyerBindingSource;
            reportDataSource2.Name = "Livestock_Auction_clsPurchase";
            reportDataSource2.Value = this.clsPurchaseBindingSource;
            reportDataSource3.Name = "Livestock_Auction_clsPayment";
            reportDataSource3.Value = this.clsPaymentBindingSource;
            this.rptReceipt.LocalReport.DataSources.Add(reportDataSource1);
            this.rptReceipt.LocalReport.DataSources.Add(reportDataSource2);
            this.rptReceipt.LocalReport.DataSources.Add(reportDataSource3);
            this.rptReceipt.LocalReport.ReportEmbeddedResource = "Livestock_Auction.Reports.rptBuyerReceipt.rdlc";
            this.rptReceipt.Location = new System.Drawing.Point(3, 3);
            this.rptReceipt.Name = "rptReceipt";
            this.rptReceipt.ShowBackButton = false;
            this.rptReceipt.ShowDocumentMapButton = false;
            this.rptReceipt.ShowFindControls = false;
            this.rptReceipt.ShowPrintButton = false;
            this.rptReceipt.ShowStopButton = false;
            this.rptReceipt.Size = new System.Drawing.Size(498, 539);
            this.rptReceipt.TabIndex = 1;
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter1.Location = new System.Drawing.Point(501, 3);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 539);
            this.splitter1.TabIndex = 59;
            this.splitter1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cmdRemovePayment);
            this.panel1.Controls.Add(this.lblRemaining);
            this.panel1.Controls.Add(this.grbAddPayment);
            this.panel1.Controls.Add(this.lblTotalRemaining);
            this.panel1.Controls.Add(this.cmdCommit);
            this.panel1.Controls.Add(this.lsvPayments);
            this.panel1.Controls.Add(this.groupBox4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(504, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(411, 539);
            this.panel1.TabIndex = 58;
            // 
            // cmdRemovePayment
            // 
            this.cmdRemovePayment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdRemovePayment.Enabled = false;
            this.cmdRemovePayment.Location = new System.Drawing.Point(272, 324);
            this.cmdRemovePayment.Name = "cmdRemovePayment";
            this.cmdRemovePayment.Size = new System.Drawing.Size(132, 23);
            this.cmdRemovePayment.TabIndex = 9;
            this.cmdRemovePayment.Text = "Remove Payment";
            this.cmdRemovePayment.UseVisualStyleBackColor = true;
            this.cmdRemovePayment.Click += new System.EventHandler(this.cmdRemovePayment_Click);
            // 
            // lblRemaining
            // 
            this.lblRemaining.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblRemaining.AutoSize = true;
            this.lblRemaining.BackColor = System.Drawing.Color.Transparent;
            this.lblRemaining.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRemaining.Location = new System.Drawing.Point(3, 327);
            this.lblRemaining.Name = "lblRemaining";
            this.lblRemaining.Size = new System.Drawing.Size(76, 16);
            this.lblRemaining.TabIndex = 65;
            this.lblRemaining.Text = "Remaining:";
            // 
            // grbAddPayment
            // 
            this.grbAddPayment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grbAddPayment.Controls.Add(this.radPayedCredit);
            this.grbAddPayment.Controls.Add(this.cmdAddPayment);
            this.grbAddPayment.Controls.Add(this.txtPaymentAmount);
            this.grbAddPayment.Controls.Add(this.label11);
            this.grbAddPayment.Controls.Add(this.txtCheckNumber);
            this.grbAddPayment.Controls.Add(this.radPayedCash);
            this.grbAddPayment.Controls.Add(this.radPayedCheck);
            this.grbAddPayment.Location = new System.Drawing.Point(3, 6);
            this.grbAddPayment.Name = "grbAddPayment";
            this.grbAddPayment.Size = new System.Drawing.Size(402, 63);
            this.grbAddPayment.TabIndex = 63;
            this.grbAddPayment.TabStop = false;
            this.grbAddPayment.Text = "Add Payment";
            // 
            // radPayedCredit
            // 
            this.radPayedCredit.AutoSize = true;
            this.radPayedCredit.Location = new System.Drawing.Point(61, 14);
            this.radPayedCredit.Name = "radPayedCredit";
            this.radPayedCredit.Size = new System.Drawing.Size(52, 17);
            this.radPayedCredit.TabIndex = 9;
            this.radPayedCredit.TabStop = true;
            this.radPayedCredit.Text = "Credit";
            this.radPayedCredit.UseVisualStyleBackColor = true;
            // 
            // cmdAddPayment
            // 
            this.cmdAddPayment.Location = new System.Drawing.Point(164, 35);
            this.cmdAddPayment.Name = "cmdAddPayment";
            this.cmdAddPayment.Size = new System.Drawing.Size(113, 23);
            this.cmdAddPayment.TabIndex = 8;
            this.cmdAddPayment.Text = "Add Payment";
            this.cmdAddPayment.UseVisualStyleBackColor = true;
            this.cmdAddPayment.Click += new System.EventHandler(this.cmdAddPayment_Click);
            // 
            // txtPaymentAmount
            // 
            this.txtPaymentAmount.Location = new System.Drawing.Point(58, 37);
            this.txtPaymentAmount.Name = "txtPaymentAmount";
            this.txtPaymentAmount.Size = new System.Drawing.Size(100, 20);
            this.txtPaymentAmount.TabIndex = 7;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 40);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(46, 13);
            this.label11.TabIndex = 6;
            this.label11.Text = "Amount:";
            // 
            // txtCheckNumber
            // 
            this.txtCheckNumber.Location = new System.Drawing.Point(228, 13);
            this.txtCheckNumber.Name = "txtCheckNumber";
            this.txtCheckNumber.Size = new System.Drawing.Size(92, 20);
            this.txtCheckNumber.TabIndex = 5;
            this.txtCheckNumber.TextChanged += new System.EventHandler(this.txtCheckNumber_TextChanged);
            // 
            // radPayedCash
            // 
            this.radPayedCash.AutoSize = true;
            this.radPayedCash.Location = new System.Drawing.Point(6, 14);
            this.radPayedCash.Name = "radPayedCash";
            this.radPayedCash.Size = new System.Drawing.Size(49, 17);
            this.radPayedCash.TabIndex = 2;
            this.radPayedCash.TabStop = true;
            this.radPayedCash.Text = "Cash";
            this.radPayedCash.UseVisualStyleBackColor = true;
            // 
            // radPayedCheck
            // 
            this.radPayedCheck.AutoSize = true;
            this.radPayedCheck.Location = new System.Drawing.Point(119, 14);
            this.radPayedCheck.Name = "radPayedCheck";
            this.radPayedCheck.Size = new System.Drawing.Size(103, 17);
            this.radPayedCheck.TabIndex = 3;
            this.radPayedCheck.TabStop = true;
            this.radPayedCheck.Text = "Check, Check #";
            this.radPayedCheck.UseVisualStyleBackColor = true;
            // 
            // lblTotalRemaining
            // 
            this.lblTotalRemaining.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTotalRemaining.AutoSize = true;
            this.lblTotalRemaining.BackColor = System.Drawing.Color.Transparent;
            this.lblTotalRemaining.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalRemaining.Location = new System.Drawing.Point(145, 324);
            this.lblTotalRemaining.Name = "lblTotalRemaining";
            this.lblTotalRemaining.Size = new System.Drawing.Size(49, 20);
            this.lblTotalRemaining.TabIndex = 64;
            this.lblTotalRemaining.Text = "$0.00";
            // 
            // cmdCommit
            // 
            this.cmdCommit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCommit.Location = new System.Drawing.Point(3, 513);
            this.cmdCommit.Name = "cmdCommit";
            this.cmdCommit.Size = new System.Drawing.Size(399, 23);
            this.cmdCommit.TabIndex = 61;
            this.cmdCommit.Text = "Commit and Print Receipt";
            this.cmdCommit.UseVisualStyleBackColor = true;
            this.cmdCommit.Click += new System.EventHandler(this.cmdCommit_Click);
            // 
            // lsvPayments
            // 
            this.lsvPayments.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lsvPayments.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colMethod,
            this.colAmount,
            this.colSurcharge,
            this.colTotal});
            this.lsvPayments.FullRowSelect = true;
            this.lsvPayments.HideSelection = false;
            this.lsvPayments.Location = new System.Drawing.Point(3, 75);
            this.lsvPayments.MultiSelect = false;
            this.lsvPayments.Name = "lsvPayments";
            this.lsvPayments.Size = new System.Drawing.Size(402, 243);
            this.lsvPayments.TabIndex = 62;
            this.lsvPayments.UseCompatibleStateImageBehavior = false;
            this.lsvPayments.View = System.Windows.Forms.View.Details;
            this.lsvPayments.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.lsvPayments_ColumnWidthChanging);
            this.lsvPayments.SelectedIndexChanged += new System.EventHandler(this.lsvPayments_SelectedIndexChanged);
            this.lsvPayments.LocationChanged += new System.EventHandler(this.lsvPayments_LocationChanged);
            this.lsvPayments.SizeChanged += new System.EventHandler(this.lsvPayments_SizeChanged);
            // 
            // colMethod
            // 
            this.colMethod.Text = "Method";
            this.colMethod.Width = 168;
            // 
            // colAmount
            // 
            this.colAmount.Text = "Amount";
            // 
            // colSurcharge
            // 
            this.colSurcharge.Text = "Surcharge";
            this.colSurcharge.Width = 65;
            // 
            // colTotal
            // 
            this.colTotal.Text = "Total";
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.txtAddressee);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.txtStreet);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.txtCity);
            this.groupBox4.Controls.Add(this.cmdClearAddr);
            this.groupBox4.Controls.Add(this.txtZip);
            this.groupBox4.Controls.Add(this.txtState);
            this.groupBox4.Controls.Add(this.cmdCopyAddr);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Location = new System.Drawing.Point(3, 353);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(401, 157);
            this.groupBox4.TabIndex = 60;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Confirm Billing Address";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 13);
            this.label4.TabIndex = 43;
            this.label4.Text = "Addressee:";
            // 
            // txtAddressee
            // 
            this.txtAddressee.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAddressee.Location = new System.Drawing.Point(74, 45);
            this.txtAddressee.Multiline = true;
            this.txtAddressee.Name = "txtAddressee";
            this.txtAddressee.Size = new System.Drawing.Size(321, 34);
            this.txtAddressee.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 88);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 13);
            this.label5.TabIndex = 44;
            this.label5.Text = "Street:";
            // 
            // txtStreet
            // 
            this.txtStreet.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStreet.Location = new System.Drawing.Point(74, 85);
            this.txtStreet.Multiline = true;
            this.txtStreet.Name = "txtStreet";
            this.txtStreet.Size = new System.Drawing.Size(321, 34);
            this.txtStreet.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(114, 128);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(27, 13);
            this.label6.TabIndex = 46;
            this.label6.Text = "City:";
            // 
            // txtCity
            // 
            this.txtCity.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCity.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtCity.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtCity.Location = new System.Drawing.Point(147, 125);
            this.txtCity.Name = "txtCity";
            this.txtCity.Size = new System.Drawing.Size(167, 20);
            this.txtCity.TabIndex = 11;
            this.txtCity.Leave += new System.EventHandler(this.txtCity_Leave);
            // 
            // cmdClearAddr
            // 
            this.cmdClearAddr.Location = new System.Drawing.Point(131, 19);
            this.cmdClearAddr.Name = "cmdClearAddr";
            this.cmdClearAddr.Size = new System.Drawing.Size(71, 20);
            this.cmdClearAddr.TabIndex = 7;
            this.cmdClearAddr.Text = "Clear ->";
            this.cmdClearAddr.UseVisualStyleBackColor = true;
            this.cmdClearAddr.Click += new System.EventHandler(this.cmdClearAddr_Click);
            // 
            // txtZip
            // 
            this.txtZip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtZip.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtZip.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtZip.Location = new System.Drawing.Point(351, 125);
            this.txtZip.Name = "txtZip";
            this.txtZip.Size = new System.Drawing.Size(44, 20);
            this.txtZip.TabIndex = 12;
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
            this.txtState.Location = new System.Drawing.Point(74, 125);
            this.txtState.Name = "txtState";
            this.txtState.Size = new System.Drawing.Size(34, 20);
            this.txtState.TabIndex = 10;
            this.txtState.Leave += new System.EventHandler(this.txtState_Leave);
            // 
            // cmdCopyAddr
            // 
            this.cmdCopyAddr.Location = new System.Drawing.Point(6, 19);
            this.cmdCopyAddr.Name = "cmdCopyAddr";
            this.cmdCopyAddr.Size = new System.Drawing.Size(119, 20);
            this.cmdCopyAddr.TabIndex = 6;
            this.cmdCopyAddr.Text = "Copy Buyer Info ->";
            this.cmdCopyAddr.UseVisualStyleBackColor = true;
            this.cmdCopyAddr.Click += new System.EventHandler(this.cmdCopyAddr_Click);
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(320, 128);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(25, 13);
            this.label8.TabIndex = 50;
            this.label8.Text = "Zip:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 128);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 13);
            this.label7.TabIndex = 49;
            this.label7.Text = "State:";
            // 
            // tscmdEditPurchase
            // 
            this.tscmdEditPurchase.Enabled = false;
            this.tscmdEditPurchase.Image = global::Livestock_Auction.Properties.Resources.edit;
            this.tscmdEditPurchase.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tscmdEditPurchase.Name = "tscmdEditPurchase";
            this.tscmdEditPurchase.Size = new System.Drawing.Size(98, 22);
            this.tscmdEditPurchase.Text = "Edit Purchase";
            this.tscmdEditPurchase.Click += new System.EventHandler(this.tscmdEditPurchase_Click);
            // 
            // tscmdAddPurchase
            // 
            this.tscmdAddPurchase.Image = ((System.Drawing.Image)(resources.GetObject("tscmdAddPurchase.Image")));
            this.tscmdAddPurchase.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tscmdAddPurchase.Name = "tscmdAddPurchase";
            this.tscmdAddPurchase.Size = new System.Drawing.Size(100, 22);
            this.tscmdAddPurchase.Text = "Add Purchase";
            this.tscmdAddPurchase.Click += new System.EventHandler(this.tscmdAddPurchase_Click);
            // 
            // tscmdDeletePurchase
            // 
            this.tscmdDeletePurchase.Enabled = false;
            this.tscmdDeletePurchase.Image = global::Livestock_Auction.Properties.Resources.exclude;
            this.tscmdDeletePurchase.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tscmdDeletePurchase.Name = "tscmdDeletePurchase";
            this.tscmdDeletePurchase.Size = new System.Drawing.Size(111, 22);
            this.tscmdDeletePurchase.Text = "Delete Purchase";
            this.tscmdDeletePurchase.Click += new System.EventHandler(this.tscmdDeletePurchase_Click);
            // 
            // clsBuyerBindingSource
            // 
            this.clsBuyerBindingSource.DataSource = typeof(Livestock_Auction.DB.clsBuyer);
            // 
            // clsPurchaseBindingSource
            // 
            this.clsPurchaseBindingSource.DataSource = typeof(Livestock_Auction.DB.clsPurchase);
            // 
            // clsPaymentBindingSource
            // 
            this.clsPaymentBindingSource.DataSource = typeof(Livestock_Auction.DB.clsPayment);
            // 
            // ucBuyerCheckout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.tabMain);
            this.Controls.Add(this.groupBox1);
            this.Name = "ucBuyerCheckout";
            this.Size = new System.Drawing.Size(926, 629);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tsPurchases.ResumeLayout(false);
            this.tsPurchases.PerformLayout();
            this.tabMain.ResumeLayout(false);
            this.tabPurchases.ResumeLayout(false);
            this.tabPurchases.PerformLayout();
            this.tabPayment.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.grbAddPayment.ResumeLayout(false);
            this.grbAddPayment.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.clsBuyerBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.clsPurchaseBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.clsPaymentBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBuyerNumber;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblNameLast;
        private System.Windows.Forms.TextBox txtNameLast;
        private System.Windows.Forms.Label lblNameFirst;
        private System.Windows.Forms.TextBox txtNameFirst;
        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tabPurchases;
        private System.Windows.Forms.TabPage tabPayment;
        private Microsoft.Reporting.WinForms.ReportViewer rptReceipt;
        private System.Windows.Forms.BindingSource clsPaymentBindingSource;
        private System.Windows.Forms.BindingSource clsPurchaseBindingSource;
        private Livestock_Auction.CheckoutGrid.ucCheckoutGrid CheckoutGrid;
        private System.Windows.Forms.ToolStrip tsPurchases;
        private System.Windows.Forms.ToolStripButton tscmdAddPurchase;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblRemaining;
        private System.Windows.Forms.GroupBox grbAddPayment;
        private System.Windows.Forms.Button cmdRemovePayment;
        private System.Windows.Forms.Button cmdAddPayment;
        private System.Windows.Forms.TextBox txtPaymentAmount;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtCheckNumber;
        private System.Windows.Forms.RadioButton radPayedCash;
        private System.Windows.Forms.RadioButton radPayedCheck;
        private System.Windows.Forms.Label lblTotalRemaining;
        private System.Windows.Forms.Button cmdCommit;
        private System.Windows.Forms.ListView lsvPayments;
        private System.Windows.Forms.ColumnHeader colMethod;
        private System.Windows.Forms.ColumnHeader colAmount;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtAddressee;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtStreet;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtCity;
        private System.Windows.Forms.Button cmdClearAddr;
        private System.Windows.Forms.TextBox txtZip;
        private System.Windows.Forms.TextBox txtState;
        private System.Windows.Forms.Button cmdCopyAddr;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ToolStripButton tscmdEditPurchase;
        private System.Windows.Forms.ToolStripButton tscmdDeletePurchase;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtNameCompany;
        private System.Windows.Forms.BindingSource clsBuyerBindingSource;
        private System.Windows.Forms.Button cmdLoad;
        private System.Windows.Forms.RadioButton radPayedCredit;
        private System.Windows.Forms.ColumnHeader colTotal;
        private System.Windows.Forms.ColumnHeader colSurcharge;

    }
}
