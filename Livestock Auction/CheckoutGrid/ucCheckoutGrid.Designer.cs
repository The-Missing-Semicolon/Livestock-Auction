namespace Livestock_Auction.CheckoutGrid
{
    partial class ucCheckoutGrid
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
            this.flpGrid = new System.Windows.Forms.FlowLayoutPanel();
            this.pancolHeader = new System.Windows.Forms.Panel();
            this.pancolItem = new System.Windows.Forms.Panel();
            this.pancolQuantity = new System.Windows.Forms.Panel();
            this.pancolBidUnit = new System.Windows.Forms.Panel();
            this.pancolBidTotal = new System.Windows.Forms.Panel();
            this.pancolSaleCondition = new System.Windows.Forms.Panel();
            this.pancolTotalOwed = new System.Windows.Forms.Panel();
            this.pancolDisposition = new System.Windows.Forms.Panel();
            this.pancolFiller = new System.Windows.Forms.Panel();
            this.splcolDisposition = new System.Windows.Forms.Splitter();
            this.pancolDispositionOpt = new System.Windows.Forms.Panel();
            this.cmbcolSetDisposition = new System.Windows.Forms.ComboBox();
            this.cmdcolCopyDisposition = new System.Windows.Forms.Button();
            this.cmdcolDisposition = new System.Windows.Forms.Button();
            this.splcolTotalOwed = new System.Windows.Forms.Splitter();
            this.cmdcolTotalOwed = new System.Windows.Forms.Button();
            this.splcolSaleCondition = new System.Windows.Forms.Splitter();
            this.pancolSaleConditionOpt = new Livestock_Auction.Helpers.TransparentPanel();
            this.radSaleAdvertising = new System.Windows.Forms.RadioButton();
            this.radSaleFullPrice = new System.Windows.Forms.RadioButton();
            this.cmdcolSaleCondition = new System.Windows.Forms.Button();
            this.splcolBidTotal = new System.Windows.Forms.Splitter();
            this.cmdcolBidTotal = new System.Windows.Forms.Button();
            this.splcolBidUnit = new System.Windows.Forms.Splitter();
            this.cmdcolBidUnit = new System.Windows.Forms.Button();
            this.splcolQuantity = new System.Windows.Forms.Splitter();
            this.cmdcolQuantity = new System.Windows.Forms.Button();
            this.splcolItem = new System.Windows.Forms.Splitter();
            this.cmdcolItem = new System.Windows.Forms.Button();
            this.splcolExhibitor = new System.Windows.Forms.Splitter();
            this.cmdcolRecipient = new System.Windows.Forms.Button();
            this.panFooter = new System.Windows.Forms.Panel();
            this.lblGrandTotal = new System.Windows.Forms.Label();
            this.ttCopyDisposition = new System.Windows.Forms.ToolTip(this.components);
            this.pancolHeader.SuspendLayout();
            this.pancolItem.SuspendLayout();
            this.pancolQuantity.SuspendLayout();
            this.pancolBidUnit.SuspendLayout();
            this.pancolBidTotal.SuspendLayout();
            this.pancolSaleCondition.SuspendLayout();
            this.pancolTotalOwed.SuspendLayout();
            this.pancolDisposition.SuspendLayout();
            this.pancolDispositionOpt.SuspendLayout();
            this.pancolSaleConditionOpt.SuspendLayout();
            this.panFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // flpGrid
            // 
            this.flpGrid.AutoScroll = true;
            this.flpGrid.BackColor = System.Drawing.SystemColors.Window;
            this.flpGrid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flpGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpGrid.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpGrid.Location = new System.Drawing.Point(0, 41);
            this.flpGrid.Margin = new System.Windows.Forms.Padding(0);
            this.flpGrid.Name = "flpGrid";
            this.flpGrid.Size = new System.Drawing.Size(1154, 299);
            this.flpGrid.TabIndex = 0;
            this.flpGrid.WrapContents = false;
            // 
            // pancolHeader
            // 
            this.pancolHeader.Controls.Add(this.pancolItem);
            this.pancolHeader.Controls.Add(this.splcolExhibitor);
            this.pancolHeader.Controls.Add(this.cmdcolRecipient);
            this.pancolHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pancolHeader.Location = new System.Drawing.Point(0, 0);
            this.pancolHeader.Margin = new System.Windows.Forms.Padding(0);
            this.pancolHeader.Name = "pancolHeader";
            this.pancolHeader.Size = new System.Drawing.Size(1154, 41);
            this.pancolHeader.TabIndex = 1;
            // 
            // pancolItem
            // 
            this.pancolItem.Controls.Add(this.pancolQuantity);
            this.pancolItem.Controls.Add(this.splcolItem);
            this.pancolItem.Controls.Add(this.cmdcolItem);
            this.pancolItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pancolItem.Location = new System.Drawing.Point(100, 0);
            this.pancolItem.Margin = new System.Windows.Forms.Padding(0);
            this.pancolItem.Name = "pancolItem";
            this.pancolItem.Size = new System.Drawing.Size(1054, 41);
            this.pancolItem.TabIndex = 2;
            // 
            // pancolQuantity
            // 
            this.pancolQuantity.Controls.Add(this.pancolBidUnit);
            this.pancolQuantity.Controls.Add(this.splcolQuantity);
            this.pancolQuantity.Controls.Add(this.cmdcolQuantity);
            this.pancolQuantity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pancolQuantity.Location = new System.Drawing.Point(104, 0);
            this.pancolQuantity.Margin = new System.Windows.Forms.Padding(0);
            this.pancolQuantity.Name = "pancolQuantity";
            this.pancolQuantity.Size = new System.Drawing.Size(950, 41);
            this.pancolQuantity.TabIndex = 2;
            // 
            // pancolBidUnit
            // 
            this.pancolBidUnit.Controls.Add(this.pancolBidTotal);
            this.pancolBidUnit.Controls.Add(this.splcolBidUnit);
            this.pancolBidUnit.Controls.Add(this.cmdcolBidUnit);
            this.pancolBidUnit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pancolBidUnit.Location = new System.Drawing.Point(75, 0);
            this.pancolBidUnit.Margin = new System.Windows.Forms.Padding(0);
            this.pancolBidUnit.Name = "pancolBidUnit";
            this.pancolBidUnit.Size = new System.Drawing.Size(875, 41);
            this.pancolBidUnit.TabIndex = 2;
            // 
            // pancolBidTotal
            // 
            this.pancolBidTotal.Controls.Add(this.pancolSaleCondition);
            this.pancolBidTotal.Controls.Add(this.splcolBidTotal);
            this.pancolBidTotal.Controls.Add(this.cmdcolBidTotal);
            this.pancolBidTotal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pancolBidTotal.Location = new System.Drawing.Point(84, 0);
            this.pancolBidTotal.Margin = new System.Windows.Forms.Padding(0);
            this.pancolBidTotal.Name = "pancolBidTotal";
            this.pancolBidTotal.Size = new System.Drawing.Size(791, 41);
            this.pancolBidTotal.TabIndex = 3;
            // 
            // pancolSaleCondition
            // 
            this.pancolSaleCondition.Controls.Add(this.pancolTotalOwed);
            this.pancolSaleCondition.Controls.Add(this.splcolSaleCondition);
            this.pancolSaleCondition.Controls.Add(this.pancolSaleConditionOpt);
            this.pancolSaleCondition.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pancolSaleCondition.Location = new System.Drawing.Point(85, 0);
            this.pancolSaleCondition.Margin = new System.Windows.Forms.Padding(0);
            this.pancolSaleCondition.Name = "pancolSaleCondition";
            this.pancolSaleCondition.Size = new System.Drawing.Size(706, 41);
            this.pancolSaleCondition.TabIndex = 3;
            // 
            // pancolTotalOwed
            // 
            this.pancolTotalOwed.Controls.Add(this.pancolDisposition);
            this.pancolTotalOwed.Controls.Add(this.splcolTotalOwed);
            this.pancolTotalOwed.Controls.Add(this.cmdcolTotalOwed);
            this.pancolTotalOwed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pancolTotalOwed.Location = new System.Drawing.Point(203, 0);
            this.pancolTotalOwed.Margin = new System.Windows.Forms.Padding(0);
            this.pancolTotalOwed.Name = "pancolTotalOwed";
            this.pancolTotalOwed.Size = new System.Drawing.Size(503, 41);
            this.pancolTotalOwed.TabIndex = 7;
            // 
            // pancolDisposition
            // 
            this.pancolDisposition.Controls.Add(this.pancolFiller);
            this.pancolDisposition.Controls.Add(this.splcolDisposition);
            this.pancolDisposition.Controls.Add(this.pancolDispositionOpt);
            this.pancolDisposition.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pancolDisposition.Location = new System.Drawing.Point(68, 0);
            this.pancolDisposition.Margin = new System.Windows.Forms.Padding(0);
            this.pancolDisposition.Name = "pancolDisposition";
            this.pancolDisposition.Size = new System.Drawing.Size(435, 41);
            this.pancolDisposition.TabIndex = 8;
            // 
            // pancolFiller
            // 
            this.pancolFiller.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pancolFiller.Location = new System.Drawing.Point(203, 0);
            this.pancolFiller.Name = "pancolFiller";
            this.pancolFiller.Size = new System.Drawing.Size(232, 41);
            this.pancolFiller.TabIndex = 6;
            // 
            // splcolDisposition
            // 
            this.splcolDisposition.Location = new System.Drawing.Point(200, 0);
            this.splcolDisposition.Margin = new System.Windows.Forms.Padding(0);
            this.splcolDisposition.Name = "splcolDisposition";
            this.splcolDisposition.Size = new System.Drawing.Size(3, 41);
            this.splcolDisposition.TabIndex = 5;
            this.splcolDisposition.TabStop = false;
            this.splcolDisposition.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splcolHeader_SplitterMoved);
            // 
            // pancolDispositionOpt
            // 
            this.pancolDispositionOpt.Controls.Add(this.cmbcolSetDisposition);
            this.pancolDispositionOpt.Controls.Add(this.cmdcolCopyDisposition);
            this.pancolDispositionOpt.Controls.Add(this.cmdcolDisposition);
            this.pancolDispositionOpt.Dock = System.Windows.Forms.DockStyle.Left;
            this.pancolDispositionOpt.Location = new System.Drawing.Point(0, 0);
            this.pancolDispositionOpt.Name = "pancolDispositionOpt";
            this.pancolDispositionOpt.Size = new System.Drawing.Size(200, 41);
            this.pancolDispositionOpt.TabIndex = 0;
            // 
            // cmbcolSetDisposition
            // 
            this.cmbcolSetDisposition.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbcolSetDisposition.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbcolSetDisposition.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbcolSetDisposition.FormattingEnabled = true;
            this.cmbcolSetDisposition.Items.AddRange(new object[] {
            "(not set)",
            "Hauled by Buyer",
            "Hauled by Seller",
            "Hauled by Fair",
            "Other Instructions"});
            this.cmbcolSetDisposition.Location = new System.Drawing.Point(0, 20);
            this.cmbcolSetDisposition.Margin = new System.Windows.Forms.Padding(0);
            this.cmbcolSetDisposition.Name = "cmbcolSetDisposition";
            this.cmbcolSetDisposition.Size = new System.Drawing.Size(179, 21);
            this.cmbcolSetDisposition.TabIndex = 11;
            this.cmbcolSetDisposition.Text = "(not set)";
            this.cmbcolSetDisposition.SelectedIndexChanged += new System.EventHandler(this.cmbcolSetDisposition_SelectedIndexChanged);
            // 
            // cmdcolCopyDisposition
            // 
            this.cmdcolCopyDisposition.BackgroundImage = global::Livestock_Auction.Properties.Resources.CopyRecord_Small;
            this.cmdcolCopyDisposition.Dock = System.Windows.Forms.DockStyle.Right;
            this.cmdcolCopyDisposition.FlatAppearance.BorderSize = 0;
            this.cmdcolCopyDisposition.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdcolCopyDisposition.Location = new System.Drawing.Point(179, 20);
            this.cmdcolCopyDisposition.Margin = new System.Windows.Forms.Padding(0);
            this.cmdcolCopyDisposition.Name = "cmdcolCopyDisposition";
            this.cmdcolCopyDisposition.Size = new System.Drawing.Size(21, 21);
            this.cmdcolCopyDisposition.TabIndex = 12;
            this.ttCopyDisposition.SetToolTip(this.cmdcolCopyDisposition, "Copy disposition details");
            this.cmdcolCopyDisposition.UseVisualStyleBackColor = true;
            this.cmdcolCopyDisposition.Click += new System.EventHandler(this.cmdcolCopyDisposition_Click);
            // 
            // cmdcolDisposition
            // 
            this.cmdcolDisposition.Dock = System.Windows.Forms.DockStyle.Top;
            this.cmdcolDisposition.Location = new System.Drawing.Point(0, 0);
            this.cmdcolDisposition.Margin = new System.Windows.Forms.Padding(0);
            this.cmdcolDisposition.Name = "cmdcolDisposition";
            this.cmdcolDisposition.Size = new System.Drawing.Size(200, 20);
            this.cmdcolDisposition.TabIndex = 4;
            this.cmdcolDisposition.Text = "Disposition";
            this.cmdcolDisposition.UseVisualStyleBackColor = true;
            this.cmdcolDisposition.Resize += new System.EventHandler(this.cmdcolHeader_Resized);
            // 
            // splcolTotalOwed
            // 
            this.splcolTotalOwed.Location = new System.Drawing.Point(65, 0);
            this.splcolTotalOwed.Margin = new System.Windows.Forms.Padding(0);
            this.splcolTotalOwed.Name = "splcolTotalOwed";
            this.splcolTotalOwed.Size = new System.Drawing.Size(3, 41);
            this.splcolTotalOwed.TabIndex = 5;
            this.splcolTotalOwed.TabStop = false;
            this.splcolTotalOwed.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splcolHeader_SplitterMoved);
            // 
            // cmdcolTotalOwed
            // 
            this.cmdcolTotalOwed.Dock = System.Windows.Forms.DockStyle.Left;
            this.cmdcolTotalOwed.Location = new System.Drawing.Point(0, 0);
            this.cmdcolTotalOwed.Margin = new System.Windows.Forms.Padding(0);
            this.cmdcolTotalOwed.Name = "cmdcolTotalOwed";
            this.cmdcolTotalOwed.Size = new System.Drawing.Size(65, 41);
            this.cmdcolTotalOwed.TabIndex = 4;
            this.cmdcolTotalOwed.Text = "Total Owed";
            this.cmdcolTotalOwed.UseVisualStyleBackColor = true;
            this.cmdcolTotalOwed.Resize += new System.EventHandler(this.cmdcolHeader_Resized);
            // 
            // splcolSaleCondition
            // 
            this.splcolSaleCondition.Location = new System.Drawing.Point(200, 0);
            this.splcolSaleCondition.Margin = new System.Windows.Forms.Padding(0);
            this.splcolSaleCondition.Name = "splcolSaleCondition";
            this.splcolSaleCondition.Size = new System.Drawing.Size(3, 41);
            this.splcolSaleCondition.TabIndex = 5;
            this.splcolSaleCondition.TabStop = false;
            this.splcolSaleCondition.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splcolHeader_SplitterMoved);
            // 
            // pancolSaleConditionOpt
            // 
            this.pancolSaleConditionOpt.Controls.Add(this.radSaleAdvertising);
            this.pancolSaleConditionOpt.Controls.Add(this.radSaleFullPrice);
            this.pancolSaleConditionOpt.Controls.Add(this.cmdcolSaleCondition);
            this.pancolSaleConditionOpt.Dock = System.Windows.Forms.DockStyle.Left;
            this.pancolSaleConditionOpt.Location = new System.Drawing.Point(0, 0);
            this.pancolSaleConditionOpt.Name = "pancolSaleConditionOpt";
            this.pancolSaleConditionOpt.Size = new System.Drawing.Size(200, 41);
            this.pancolSaleConditionOpt.TabIndex = 8;
            this.pancolSaleConditionOpt.Resize += new System.EventHandler(this.cmdcolHeader_Resized);
            // 
            // radSaleAdvertising
            // 
            this.radSaleAdvertising.AutoSize = true;
            this.radSaleAdvertising.Location = new System.Drawing.Point(81, 20);
            this.radSaleAdvertising.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.radSaleAdvertising.Name = "radSaleAdvertising";
            this.radSaleAdvertising.Size = new System.Drawing.Size(77, 17);
            this.radSaleAdvertising.TabIndex = 6;
            this.radSaleAdvertising.TabStop = true;
            this.radSaleAdvertising.Text = "Advertising";
            this.radSaleAdvertising.UseVisualStyleBackColor = true;
            this.radSaleAdvertising.Click += new System.EventHandler(this.radSaleHeader_Clicked);
            // 
            // radSaleFullPrice
            // 
            this.radSaleFullPrice.AutoSize = true;
            this.radSaleFullPrice.Location = new System.Drawing.Point(3, 20);
            this.radSaleFullPrice.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.radSaleFullPrice.Name = "radSaleFullPrice";
            this.radSaleFullPrice.Size = new System.Drawing.Size(68, 17);
            this.radSaleFullPrice.TabIndex = 5;
            this.radSaleFullPrice.TabStop = true;
            this.radSaleFullPrice.Text = "Full Price";
            this.radSaleFullPrice.UseVisualStyleBackColor = true;
            this.radSaleFullPrice.Click += new System.EventHandler(this.radSaleHeader_Clicked);
            // 
            // cmdcolSaleCondition
            // 
            this.cmdcolSaleCondition.Dock = System.Windows.Forms.DockStyle.Top;
            this.cmdcolSaleCondition.Location = new System.Drawing.Point(0, 0);
            this.cmdcolSaleCondition.Margin = new System.Windows.Forms.Padding(0);
            this.cmdcolSaleCondition.Name = "cmdcolSaleCondition";
            this.cmdcolSaleCondition.Size = new System.Drawing.Size(200, 20);
            this.cmdcolSaleCondition.TabIndex = 4;
            this.cmdcolSaleCondition.Text = "Sale Condition";
            this.cmdcolSaleCondition.UseVisualStyleBackColor = true;
            // 
            // splcolBidTotal
            // 
            this.splcolBidTotal.Location = new System.Drawing.Point(82, 0);
            this.splcolBidTotal.Margin = new System.Windows.Forms.Padding(0);
            this.splcolBidTotal.Name = "splcolBidTotal";
            this.splcolBidTotal.Size = new System.Drawing.Size(3, 41);
            this.splcolBidTotal.TabIndex = 2;
            this.splcolBidTotal.TabStop = false;
            this.splcolBidTotal.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splcolHeader_SplitterMoved);
            // 
            // cmdcolBidTotal
            // 
            this.cmdcolBidTotal.Dock = System.Windows.Forms.DockStyle.Left;
            this.cmdcolBidTotal.Location = new System.Drawing.Point(0, 0);
            this.cmdcolBidTotal.Margin = new System.Windows.Forms.Padding(0);
            this.cmdcolBidTotal.Name = "cmdcolBidTotal";
            this.cmdcolBidTotal.Size = new System.Drawing.Size(82, 41);
            this.cmdcolBidTotal.TabIndex = 1;
            this.cmdcolBidTotal.Text = "Total Bid";
            this.cmdcolBidTotal.UseVisualStyleBackColor = true;
            this.cmdcolBidTotal.Resize += new System.EventHandler(this.cmdcolHeader_Resized);
            // 
            // splcolBidUnit
            // 
            this.splcolBidUnit.Location = new System.Drawing.Point(81, 0);
            this.splcolBidUnit.Margin = new System.Windows.Forms.Padding(0);
            this.splcolBidUnit.Name = "splcolBidUnit";
            this.splcolBidUnit.Size = new System.Drawing.Size(3, 41);
            this.splcolBidUnit.TabIndex = 2;
            this.splcolBidUnit.TabStop = false;
            this.splcolBidUnit.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splcolHeader_SplitterMoved);
            // 
            // cmdcolBidUnit
            // 
            this.cmdcolBidUnit.Dock = System.Windows.Forms.DockStyle.Left;
            this.cmdcolBidUnit.Location = new System.Drawing.Point(0, 0);
            this.cmdcolBidUnit.Margin = new System.Windows.Forms.Padding(0);
            this.cmdcolBidUnit.Name = "cmdcolBidUnit";
            this.cmdcolBidUnit.Size = new System.Drawing.Size(81, 41);
            this.cmdcolBidUnit.TabIndex = 1;
            this.cmdcolBidUnit.Text = "Bid/Unit";
            this.cmdcolBidUnit.UseVisualStyleBackColor = true;
            this.cmdcolBidUnit.Resize += new System.EventHandler(this.cmdcolHeader_Resized);
            // 
            // splcolQuantity
            // 
            this.splcolQuantity.Location = new System.Drawing.Point(72, 0);
            this.splcolQuantity.Margin = new System.Windows.Forms.Padding(0);
            this.splcolQuantity.Name = "splcolQuantity";
            this.splcolQuantity.Size = new System.Drawing.Size(3, 41);
            this.splcolQuantity.TabIndex = 1;
            this.splcolQuantity.TabStop = false;
            this.splcolQuantity.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splcolHeader_SplitterMoved);
            // 
            // cmdcolQuantity
            // 
            this.cmdcolQuantity.Dock = System.Windows.Forms.DockStyle.Left;
            this.cmdcolQuantity.Location = new System.Drawing.Point(0, 0);
            this.cmdcolQuantity.Margin = new System.Windows.Forms.Padding(0);
            this.cmdcolQuantity.Name = "cmdcolQuantity";
            this.cmdcolQuantity.Size = new System.Drawing.Size(72, 41);
            this.cmdcolQuantity.TabIndex = 0;
            this.cmdcolQuantity.Text = "Weight/Qty";
            this.cmdcolQuantity.UseVisualStyleBackColor = true;
            this.cmdcolQuantity.Resize += new System.EventHandler(this.cmdcolHeader_Resized);
            // 
            // splcolItem
            // 
            this.splcolItem.Location = new System.Drawing.Point(101, 0);
            this.splcolItem.Margin = new System.Windows.Forms.Padding(0);
            this.splcolItem.Name = "splcolItem";
            this.splcolItem.Size = new System.Drawing.Size(3, 41);
            this.splcolItem.TabIndex = 1;
            this.splcolItem.TabStop = false;
            this.splcolItem.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splcolHeader_SplitterMoved);
            // 
            // cmdcolItem
            // 
            this.cmdcolItem.Dock = System.Windows.Forms.DockStyle.Left;
            this.cmdcolItem.Location = new System.Drawing.Point(0, 0);
            this.cmdcolItem.Margin = new System.Windows.Forms.Padding(0);
            this.cmdcolItem.Name = "cmdcolItem";
            this.cmdcolItem.Size = new System.Drawing.Size(101, 41);
            this.cmdcolItem.TabIndex = 0;
            this.cmdcolItem.Text = "Animal/Item";
            this.cmdcolItem.UseVisualStyleBackColor = true;
            this.cmdcolItem.Resize += new System.EventHandler(this.cmdcolHeader_Resized);
            // 
            // splcolExhibitor
            // 
            this.splcolExhibitor.Location = new System.Drawing.Point(97, 0);
            this.splcolExhibitor.Margin = new System.Windows.Forms.Padding(0);
            this.splcolExhibitor.Name = "splcolExhibitor";
            this.splcolExhibitor.Size = new System.Drawing.Size(3, 41);
            this.splcolExhibitor.TabIndex = 1;
            this.splcolExhibitor.TabStop = false;
            this.splcolExhibitor.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splcolHeader_SplitterMoved);
            // 
            // cmdcolRecipient
            // 
            this.cmdcolRecipient.Dock = System.Windows.Forms.DockStyle.Left;
            this.cmdcolRecipient.Location = new System.Drawing.Point(0, 0);
            this.cmdcolRecipient.Margin = new System.Windows.Forms.Padding(0);
            this.cmdcolRecipient.Name = "cmdcolRecipient";
            this.cmdcolRecipient.Size = new System.Drawing.Size(97, 41);
            this.cmdcolRecipient.TabIndex = 0;
            this.cmdcolRecipient.Text = "Recipient";
            this.cmdcolRecipient.UseVisualStyleBackColor = true;
            this.cmdcolRecipient.Resize += new System.EventHandler(this.cmdcolHeader_Resized);
            // 
            // panFooter
            // 
            this.panFooter.Controls.Add(this.lblGrandTotal);
            this.panFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panFooter.Location = new System.Drawing.Point(0, 340);
            this.panFooter.Name = "panFooter";
            this.panFooter.Size = new System.Drawing.Size(1154, 22);
            this.panFooter.TabIndex = 2;
            // 
            // lblGrandTotal
            // 
            this.lblGrandTotal.AutoEllipsis = true;
            this.lblGrandTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGrandTotal.Location = new System.Drawing.Point(647, 0);
            this.lblGrandTotal.Name = "lblGrandTotal";
            this.lblGrandTotal.Size = new System.Drawing.Size(89, 20);
            this.lblGrandTotal.TabIndex = 0;
            this.lblGrandTotal.Text = "$0,000.00";
            // 
            // ttCopyDisposition
            // 
            this.ttCopyDisposition.AutomaticDelay = 250;
            this.ttCopyDisposition.AutoPopDelay = 10000;
            this.ttCopyDisposition.InitialDelay = 250;
            this.ttCopyDisposition.ReshowDelay = 50;
            this.ttCopyDisposition.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.ttCopyDisposition.ToolTipTitle = "Copy Details";
            // 
            // ucCheckoutGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flpGrid);
            this.Controls.Add(this.panFooter);
            this.Controls.Add(this.pancolHeader);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ucCheckoutGrid";
            this.Size = new System.Drawing.Size(1154, 362);
            this.Load += new System.EventHandler(this.ucCheckoutGrid_Load);
            this.pancolHeader.ResumeLayout(false);
            this.pancolItem.ResumeLayout(false);
            this.pancolQuantity.ResumeLayout(false);
            this.pancolBidUnit.ResumeLayout(false);
            this.pancolBidTotal.ResumeLayout(false);
            this.pancolSaleCondition.ResumeLayout(false);
            this.pancolTotalOwed.ResumeLayout(false);
            this.pancolDisposition.ResumeLayout(false);
            this.pancolDispositionOpt.ResumeLayout(false);
            this.pancolSaleConditionOpt.ResumeLayout(false);
            this.pancolSaleConditionOpt.PerformLayout();
            this.panFooter.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flpGrid;
        private System.Windows.Forms.Panel pancolHeader;
        private System.Windows.Forms.Button cmdcolRecipient;
        private System.Windows.Forms.Splitter splcolExhibitor;
        private System.Windows.Forms.Panel pancolItem;
        private System.Windows.Forms.Button cmdcolItem;
        private System.Windows.Forms.Splitter splcolItem;
        private System.Windows.Forms.Panel pancolQuantity;
        private System.Windows.Forms.Button cmdcolQuantity;
        private System.Windows.Forms.Splitter splcolQuantity;
        private System.Windows.Forms.Panel pancolBidUnit;
        private System.Windows.Forms.Button cmdcolBidUnit;
        private System.Windows.Forms.Splitter splcolBidUnit;
        private System.Windows.Forms.Panel pancolBidTotal;
        private System.Windows.Forms.Splitter splcolBidTotal;
        private System.Windows.Forms.Button cmdcolBidTotal;
        private System.Windows.Forms.Panel pancolSaleCondition;
        private System.Windows.Forms.Button cmdcolSaleCondition;
        private System.Windows.Forms.Splitter splcolSaleCondition;
        private System.Windows.Forms.Panel pancolTotalOwed;
        private System.Windows.Forms.Splitter splcolTotalOwed;
        private System.Windows.Forms.Button cmdcolTotalOwed;
        private System.Windows.Forms.Panel pancolDisposition;
        private System.Windows.Forms.Splitter splcolDisposition;
        private System.Windows.Forms.Button cmdcolDisposition;
        private System.Windows.Forms.Panel panFooter;
        private System.Windows.Forms.Label lblGrandTotal;
        private System.Windows.Forms.ToolTip ttCopyDisposition;
        private System.Windows.Forms.Panel pancolFiller;
        private Helpers.TransparentPanel pancolSaleConditionOpt;
        private System.Windows.Forms.RadioButton radSaleAdvertising;
        private System.Windows.Forms.RadioButton radSaleFullPrice;
        private System.Windows.Forms.Panel pancolDispositionOpt;
        private System.Windows.Forms.ComboBox cmbcolSetDisposition;
        private System.Windows.Forms.Button cmdcolCopyDisposition;
    }
}
