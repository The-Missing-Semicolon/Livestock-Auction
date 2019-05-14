namespace Livestock_Auction
{
    partial class ucExhibits
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucExhibits));
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtWeight = new System.Windows.Forms.TextBox();
            this.txtChampion = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.chkRateOfGain = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTagNumber = new System.Windows.Forms.TextBox();
            this.cmdCommit = new System.Windows.Forms.Button();
            this.lsvItems = new System.Windows.Forms.ListView();
            this.colTagNumber = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colExhibitor = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colMarketItem = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colMarketValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colChampion = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colRateOfGain = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colWeight = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colTakeBack = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colInclude = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colPurchases = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDisposition = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmdShowAll = new System.Windows.Forms.Button();
            this.cmdNewRecord = new System.Windows.Forms.Button();
            this.cmdDeleteRecord = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radTakeBackNo = new System.Windows.Forms.RadioButton();
            this.radTakeBackYes = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tscmdToggleInclude = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tstxtFilterTagNumber = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.tstxtFilterExhibitor = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.tstxtFilterItem = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
            this.tstxtFilterChampion = new System.Windows.Forms.ToolStripTextBox();
            this.tscmdFilterIncluded = new System.Windows.Forms.ToolStripButton();
            this.MarketItemEntry = new Livestock_Auction.DataEntry.ucMarketItemEntry();
            this.ExhibitorEntry = new Livestock_Auction.DataEntry.ucExhibitorEntry();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(254, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Weight";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(77, 22);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(54, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Champion";
            // 
            // txtWeight
            // 
            this.txtWeight.Location = new System.Drawing.Point(257, 38);
            this.txtWeight.Name = "txtWeight";
            this.txtWeight.Size = new System.Drawing.Size(50, 20);
            this.txtWeight.TabIndex = 11;
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
            this.txtChampion.Location = new System.Drawing.Point(80, 38);
            this.txtChampion.Name = "txtChampion";
            this.txtChampion.Size = new System.Drawing.Size(98, 20);
            this.txtChampion.TabIndex = 7;
            this.txtChampion.Leave += new System.EventHandler(this.txtChampion_Leave);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.chkRateOfGain);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.txtTagNumber);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.txtChampion);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.txtWeight);
            this.groupBox3.Location = new System.Drawing.Point(0, 460);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(314, 64);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Animal";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(181, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Rate of Gain";
            // 
            // chkRateOfGain
            // 
            this.chkRateOfGain.AutoSize = true;
            this.chkRateOfGain.Location = new System.Drawing.Point(184, 41);
            this.chkRateOfGain.Name = "chkRateOfGain";
            this.chkRateOfGain.Size = new System.Drawing.Size(50, 17);
            this.chkRateOfGain.TabIndex = 9;
            this.chkRateOfGain.Text = "ROG";
            this.chkRateOfGain.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Tag #";
            // 
            // txtTagNumber
            // 
            this.txtTagNumber.Location = new System.Drawing.Point(9, 38);
            this.txtTagNumber.Name = "txtTagNumber";
            this.txtTagNumber.Size = new System.Drawing.Size(65, 20);
            this.txtTagNumber.TabIndex = 5;
            // 
            // cmdCommit
            // 
            this.cmdCommit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdCommit.Location = new System.Drawing.Point(479, 463);
            this.cmdCommit.Name = "cmdCommit";
            this.cmdCommit.Size = new System.Drawing.Size(75, 64);
            this.cmdCommit.TabIndex = 16;
            this.cmdCommit.Text = "Commit";
            this.cmdCommit.UseVisualStyleBackColor = true;
            this.cmdCommit.Click += new System.EventHandler(this.cmdCommit_Click);
            // 
            // lsvItems
            // 
            this.lsvItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lsvItems.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colTagNumber,
            this.colExhibitor,
            this.colMarketItem,
            this.colMarketValue,
            this.colChampion,
            this.colRateOfGain,
            this.colWeight,
            this.colTakeBack,
            this.colInclude,
            this.colPurchases,
            this.colDisposition});
            this.lsvItems.FullRowSelect = true;
            this.lsvItems.Location = new System.Drawing.Point(0, 28);
            this.lsvItems.Name = "lsvItems";
            this.lsvItems.Size = new System.Drawing.Size(953, 261);
            this.lsvItems.TabIndex = 17;
            this.lsvItems.UseCompatibleStateImageBehavior = false;
            this.lsvItems.View = System.Windows.Forms.View.Details;
            this.lsvItems.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lsvItems_ColumnClick);
            this.lsvItems.SelectedIndexChanged += new System.EventHandler(this.lsvItems_SelectedIndexChanged);
            // 
            // colTagNumber
            // 
            this.colTagNumber.Text = "Tag #";
            // 
            // colExhibitor
            // 
            this.colExhibitor.Text = "Exhibitor";
            this.colExhibitor.Width = 100;
            // 
            // colMarketItem
            // 
            this.colMarketItem.Text = "Item";
            this.colMarketItem.Width = 68;
            // 
            // colMarketValue
            // 
            this.colMarketValue.Text = "Value";
            this.colMarketValue.Width = 46;
            // 
            // colChampion
            // 
            this.colChampion.Text = "Champion Status";
            this.colChampion.Width = 103;
            // 
            // colRateOfGain
            // 
            this.colRateOfGain.Text = "Rate Of Gain";
            this.colRateOfGain.Width = 77;
            // 
            // colWeight
            // 
            this.colWeight.Text = "Weight";
            // 
            // colTakeBack
            // 
            this.colTakeBack.Text = "Take Back";
            this.colTakeBack.Width = 75;
            // 
            // colInclude
            // 
            this.colInclude.Text = "Include In Auction";
            this.colInclude.Width = 98;
            // 
            // colPurchases
            // 
            this.colPurchases.Text = "Purchases";
            this.colPurchases.Width = 72;
            // 
            // colDisposition
            // 
            this.colDisposition.Text = "Disposition";
            this.colDisposition.Width = 68;
            // 
            // cmdShowAll
            // 
            this.cmdShowAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdShowAll.Enabled = false;
            this.cmdShowAll.Location = new System.Drawing.Point(274, 295);
            this.cmdShowAll.Name = "cmdShowAll";
            this.cmdShowAll.Size = new System.Drawing.Size(75, 23);
            this.cmdShowAll.TabIndex = 20;
            this.cmdShowAll.Text = "Show All";
            this.cmdShowAll.UseVisualStyleBackColor = true;
            // 
            // cmdNewRecord
            // 
            this.cmdNewRecord.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdNewRecord.Location = new System.Drawing.Point(0, 295);
            this.cmdNewRecord.Name = "cmdNewRecord";
            this.cmdNewRecord.Size = new System.Drawing.Size(131, 23);
            this.cmdNewRecord.TabIndex = 18;
            this.cmdNewRecord.Text = "Create New Record";
            this.cmdNewRecord.UseVisualStyleBackColor = true;
            this.cmdNewRecord.Click += new System.EventHandler(this.cmdNewRecord_Click);
            // 
            // cmdDeleteRecord
            // 
            this.cmdDeleteRecord.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdDeleteRecord.Enabled = false;
            this.cmdDeleteRecord.Location = new System.Drawing.Point(137, 295);
            this.cmdDeleteRecord.Name = "cmdDeleteRecord";
            this.cmdDeleteRecord.Size = new System.Drawing.Size(131, 23);
            this.cmdDeleteRecord.TabIndex = 19;
            this.cmdDeleteRecord.Text = "Delete Selected Record";
            this.cmdDeleteRecord.UseVisualStyleBackColor = true;
            this.cmdDeleteRecord.Click += new System.EventHandler(this.cmdDeleteRecord_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.radTakeBackNo);
            this.groupBox1.Controls.Add(this.radTakeBackYes);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(320, 460);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(153, 64);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Take Back";
            // 
            // radTakeBackNo
            // 
            this.radTakeBackNo.AutoSize = true;
            this.radTakeBackNo.Location = new System.Drawing.Point(56, 33);
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
            this.radTakeBackYes.Location = new System.Drawing.Point(7, 33);
            this.radTakeBackYes.Name = "radTakeBackYes";
            this.radTakeBackYes.Size = new System.Drawing.Size(43, 17);
            this.radTakeBackYes.TabIndex = 14;
            this.radTakeBackYes.TabStop = true;
            this.radTakeBackYes.Text = "Yes";
            this.radTakeBackYes.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(144, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Exhibitor wants animal back?";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tscmdToggleInclude,
            this.toolStripSeparator1,
            this.toolStripLabel1,
            this.tstxtFilterTagNumber,
            this.toolStripLabel2,
            this.tstxtFilterExhibitor,
            this.toolStripLabel3,
            this.tstxtFilterItem,
            this.toolStripLabel4,
            this.tstxtFilterChampion,
            this.tscmdFilterIncluded});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(953, 25);
            this.toolStrip1.TabIndex = 19;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tscmdToggleInclude
            // 
            this.tscmdToggleInclude.Enabled = false;
            this.tscmdToggleInclude.Image = ((System.Drawing.Image)(resources.GetObject("tscmdToggleInclude.Image")));
            this.tscmdToggleInclude.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tscmdToggleInclude.Name = "tscmdToggleInclude";
            this.tscmdToggleInclude.Size = new System.Drawing.Size(105, 22);
            this.tscmdToggleInclude.Text = "Exclude Exhibit";
            this.tscmdToggleInclude.Click += new System.EventHandler(this.tscmdToggleInclude_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(40, 22);
            this.toolStripLabel1.Text = "Tag #:";
            // 
            // tstxtFilterTagNumber
            // 
            this.tstxtFilterTagNumber.Name = "tstxtFilterTagNumber";
            this.tstxtFilterTagNumber.Size = new System.Drawing.Size(100, 25);
            this.tstxtFilterTagNumber.TextChanged += new System.EventHandler(this.Filter_Changed);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(56, 22);
            this.toolStripLabel2.Text = "Exhibitor:";
            // 
            // tstxtFilterExhibitor
            // 
            this.tstxtFilterExhibitor.Name = "tstxtFilterExhibitor";
            this.tstxtFilterExhibitor.Size = new System.Drawing.Size(100, 25);
            this.tstxtFilterExhibitor.TextChanged += new System.EventHandler(this.Filter_Changed);
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(34, 22);
            this.toolStripLabel3.Text = "Item:";
            // 
            // tstxtFilterItem
            // 
            this.tstxtFilterItem.Name = "tstxtFilterItem";
            this.tstxtFilterItem.Size = new System.Drawing.Size(100, 25);
            this.tstxtFilterItem.TextChanged += new System.EventHandler(this.Filter_Changed);
            // 
            // toolStripLabel4
            // 
            this.toolStripLabel4.Name = "toolStripLabel4";
            this.toolStripLabel4.Size = new System.Drawing.Size(66, 22);
            this.toolStripLabel4.Text = "Champion:";
            // 
            // tstxtFilterChampion
            // 
            this.tstxtFilterChampion.Name = "tstxtFilterChampion";
            this.tstxtFilterChampion.Size = new System.Drawing.Size(100, 25);
            this.tstxtFilterChampion.TextChanged += new System.EventHandler(this.Filter_Changed);
            // 
            // tscmdFilterIncluded
            // 
            this.tscmdFilterIncluded.CheckOnClick = true;
            this.tscmdFilterIncluded.Image = ((System.Drawing.Image)(resources.GetObject("tscmdFilterIncluded.Image")));
            this.tscmdFilterIncluded.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tscmdFilterIncluded.Name = "tscmdFilterIncluded";
            this.tscmdFilterIncluded.Size = new System.Drawing.Size(84, 22);
            this.tscmdFilterIncluded.Text = "Is Included";
            this.tscmdFilterIncluded.Click += new System.EventHandler(this.Filter_Changed);
            // 
            // MarketItemEntry
            // 
            this.MarketItemEntry.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.MarketItemEntry.Location = new System.Drawing.Point(0, 392);
            this.MarketItemEntry.MarketItem = null;
            this.MarketItemEntry.Mode = Livestock_Auction.DataEntry.EntryModes.New;
            this.MarketItemEntry.Name = "MarketItemEntry";
            this.MarketItemEntry.Size = new System.Drawing.Size(473, 68);
            this.MarketItemEntry.TabIndex = 2;
            // 
            // ExhibitorEntry
            // 
            this.ExhibitorEntry.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ExhibitorEntry.Exhibitor = null;
            this.ExhibitorEntry.Location = new System.Drawing.Point(0, 324);
            this.ExhibitorEntry.Mode = Livestock_Auction.DataEntry.EntryModes.Lookup;
            this.ExhibitorEntry.Name = "ExhibitorEntry";
            this.ExhibitorEntry.Size = new System.Drawing.Size(473, 62);
            this.ExhibitorEntry.TabIndex = 1;
            this.ExhibitorEntry.Leave += new System.EventHandler(this.ExhibitorEntry_Leave);
            // 
            // ucExhibits
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.MarketItemEntry);
            this.Controls.Add(this.ExhibitorEntry);
            this.Controls.Add(this.cmdDeleteRecord);
            this.Controls.Add(this.cmdNewRecord);
            this.Controls.Add(this.cmdShowAll);
            this.Controls.Add(this.lsvItems);
            this.Controls.Add(this.cmdCommit);
            this.Controls.Add(this.groupBox3);
            this.Name = "ucExhibits";
            this.Size = new System.Drawing.Size(953, 527);
            this.Load += new System.EventHandler(this.ucExhibits_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtWeight;
        private System.Windows.Forms.TextBox txtChampion;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button cmdCommit;
        private System.Windows.Forms.ListView lsvItems;
        private System.Windows.Forms.ColumnHeader colExhibitor;
        private System.Windows.Forms.ColumnHeader colMarketItem;
        private System.Windows.Forms.ColumnHeader colMarketValue;
        private System.Windows.Forms.ColumnHeader colChampion;
        private System.Windows.Forms.ColumnHeader colWeight;
        private System.Windows.Forms.Button cmdShowAll;
        private System.Windows.Forms.Button cmdNewRecord;
        private System.Windows.Forms.Button cmdDeleteRecord;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTagNumber;
        private Livestock_Auction.DataEntry.ucExhibitorEntry ExhibitorEntry;
        private Livestock_Auction.DataEntry.ucMarketItemEntry MarketItemEntry;
        private System.Windows.Forms.ColumnHeader colTagNumber;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton radTakeBackYes;
        private System.Windows.Forms.RadioButton radTakeBackNo;
        private System.Windows.Forms.ColumnHeader colTakeBack;
        private System.Windows.Forms.ColumnHeader colInclude;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tscmdToggleInclude;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox tstxtFilterTagNumber;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripTextBox tstxtFilterExhibitor;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripLabel toolStripLabel4;
        private System.Windows.Forms.ToolStripTextBox tstxtFilterChampion;
        private System.Windows.Forms.ToolStripButton tscmdFilterIncluded;
        private System.Windows.Forms.ToolStripTextBox tstxtFilterItem;
        private System.Windows.Forms.ColumnHeader colPurchases;
        private System.Windows.Forms.ColumnHeader colDisposition;
        private System.Windows.Forms.ColumnHeader colRateOfGain;
        private System.Windows.Forms.CheckBox chkRateOfGain;
        private System.Windows.Forms.Label label3;
    }
}
