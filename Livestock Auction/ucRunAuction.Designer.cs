namespace Livestock_Auction
{
    partial class ucRunAuction
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucRunAuction));
            this.lsvAuctionOrder = new System.Windows.Forms.ListView();
            this.colOrder = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colTagNumber = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colExhibitorNumber = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colExhibitorName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colAnimalChampion = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colAnimalType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colAnimalWeight = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colBuyerNumber = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colBuyerName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colWinningBid = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colCheckedOut = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmdNext = new System.Windows.Forms.Button();
            this.grpExhibit = new System.Windows.Forms.GroupBox();
            this.txtComments = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblExhibitor = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblMaxOrder = new System.Windows.Forms.Label();
            this.lblCurOrder = new System.Windows.Forms.Label();
            this.panMain = new System.Windows.Forms.Panel();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panPurchases = new System.Windows.Forms.Panel();
            this.elvPurchases = new Livestock_Auction.Helpers.EditableListView();
            this.imglstPurchase = new System.Windows.Forms.ImageList(this.components);
            this.panEntry = new System.Windows.Forms.Panel();
            this.cmdReset = new System.Windows.Forms.Button();
            this.grpExhibit.SuspendLayout();
            this.panMain.SuspendLayout();
            this.panPurchases.SuspendLayout();
            this.panEntry.SuspendLayout();
            this.SuspendLayout();
            // 
            // lsvAuctionOrder
            // 
            this.lsvAuctionOrder.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colOrder,
            this.colTagNumber,
            this.colExhibitorNumber,
            this.colExhibitorName,
            this.colAnimalChampion,
            this.colAnimalType,
            this.colAnimalWeight,
            this.colBuyerNumber,
            this.colBuyerName,
            this.colWinningBid,
            this.colCheckedOut});
            this.lsvAuctionOrder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lsvAuctionOrder.FullRowSelect = true;
            this.lsvAuctionOrder.HideSelection = false;
            this.lsvAuctionOrder.Location = new System.Drawing.Point(0, 0);
            this.lsvAuctionOrder.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.lsvAuctionOrder.MultiSelect = false;
            this.lsvAuctionOrder.Name = "lsvAuctionOrder";
            this.lsvAuctionOrder.ShowGroups = false;
            this.lsvAuctionOrder.Size = new System.Drawing.Size(1113, 538);
            this.lsvAuctionOrder.TabIndex = 0;
            this.lsvAuctionOrder.UseCompatibleStateImageBehavior = false;
            this.lsvAuctionOrder.View = System.Windows.Forms.View.Details;
            this.lsvAuctionOrder.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lsvAuctionOrder_ColumnClick);
            this.lsvAuctionOrder.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lsvAuctionOrder_MouseUp);
            // 
            // colOrder
            // 
            this.colOrder.Text = "Order";
            // 
            // colTagNumber
            // 
            this.colTagNumber.Text = "Tag #";
            // 
            // colExhibitorNumber
            // 
            this.colExhibitorNumber.Text = "Exhibitor #";
            this.colExhibitorNumber.Width = 115;
            // 
            // colExhibitorName
            // 
            this.colExhibitorName.Text = "Exhibitor Name";
            this.colExhibitorName.Width = 118;
            // 
            // colAnimalChampion
            // 
            this.colAnimalChampion.Text = "Animal Champion";
            this.colAnimalChampion.Width = 103;
            // 
            // colAnimalType
            // 
            this.colAnimalType.Text = "Animal Type";
            this.colAnimalType.Width = 102;
            // 
            // colAnimalWeight
            // 
            this.colAnimalWeight.Text = "Animal Weight";
            this.colAnimalWeight.Width = 103;
            // 
            // colBuyerNumber
            // 
            this.colBuyerNumber.Text = "Buyer #";
            this.colBuyerNumber.Width = 109;
            // 
            // colBuyerName
            // 
            this.colBuyerName.Text = "Buyer Name";
            this.colBuyerName.Width = 105;
            // 
            // colWinningBid
            // 
            this.colWinningBid.Text = "Winning Bid";
            this.colWinningBid.Width = 115;
            // 
            // colCheckedOut
            // 
            this.colCheckedOut.Text = "Checked Out";
            // 
            // cmdNext
            // 
            this.cmdNext.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdNext.Enabled = false;
            this.cmdNext.Location = new System.Drawing.Point(722, 0);
            this.cmdNext.Name = "cmdNext";
            this.cmdNext.Size = new System.Drawing.Size(66, 60);
            this.cmdNext.TabIndex = 4;
            this.cmdNext.Text = "Next Exhibit";
            this.cmdNext.UseVisualStyleBackColor = true;
            this.cmdNext.Click += new System.EventHandler(this.cmdNext_Click);
            // 
            // grpExhibit
            // 
            this.grpExhibit.BackColor = System.Drawing.SystemColors.Control;
            this.grpExhibit.Controls.Add(this.txtComments);
            this.grpExhibit.Controls.Add(this.label8);
            this.grpExhibit.Controls.Add(this.lblDescription);
            this.grpExhibit.Controls.Add(this.lblExhibitor);
            this.grpExhibit.Controls.Add(this.label10);
            this.grpExhibit.Controls.Add(this.lblMaxOrder);
            this.grpExhibit.Controls.Add(this.lblCurOrder);
            this.grpExhibit.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grpExhibit.Location = new System.Drawing.Point(0, 89);
            this.grpExhibit.Name = "grpExhibit";
            this.grpExhibit.Size = new System.Drawing.Size(1113, 106);
            this.grpExhibit.TabIndex = 39;
            this.grpExhibit.TabStop = false;
            this.grpExhibit.Text = "Exhibit";
            // 
            // txtComments
            // 
            this.txtComments.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtComments.Location = new System.Drawing.Point(88, 45);
            this.txtComments.Multiline = true;
            this.txtComments.Name = "txtComments";
            this.txtComments.Size = new System.Drawing.Size(1019, 52);
            this.txtComments.TabIndex = 6;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(85, 28);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(50, 13);
            this.label8.TabIndex = 45;
            this.label8.Text = "Exhibitor:";
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(145, 15);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(70, 13);
            this.lblDescription.TabIndex = 44;
            this.lblDescription.Text = "lblDescription";
            // 
            // lblExhibitor
            // 
            this.lblExhibitor.AutoSize = true;
            this.lblExhibitor.Location = new System.Drawing.Point(145, 28);
            this.lblExhibitor.Name = "lblExhibitor";
            this.lblExhibitor.Size = new System.Drawing.Size(57, 13);
            this.lblExhibitor.TabIndex = 46;
            this.lblExhibitor.Text = "lblExhibitor";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(85, 15);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(63, 13);
            this.label10.TabIndex = 43;
            this.label10.Text = "Description:";
            // 
            // lblMaxOrder
            // 
            this.lblMaxOrder.AutoSize = true;
            this.lblMaxOrder.Location = new System.Drawing.Point(6, 55);
            this.lblMaxOrder.Name = "lblMaxOrder";
            this.lblMaxOrder.Size = new System.Drawing.Size(40, 13);
            this.lblMaxOrder.TabIndex = 42;
            this.lblMaxOrder.Text = "of ###";
            this.lblMaxOrder.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCurOrder
            // 
            this.lblCurOrder.AutoSize = true;
            this.lblCurOrder.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurOrder.Location = new System.Drawing.Point(2, 15);
            this.lblCurOrder.Name = "lblCurOrder";
            this.lblCurOrder.Size = new System.Drawing.Size(77, 39);
            this.lblCurOrder.TabIndex = 41;
            this.lblCurOrder.Text = "###";
            this.lblCurOrder.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panMain
            // 
            this.panMain.Controls.Add(this.lsvAuctionOrder);
            this.panMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panMain.Location = new System.Drawing.Point(0, 0);
            this.panMain.Name = "panMain";
            this.panMain.Size = new System.Drawing.Size(1113, 538);
            this.panMain.TabIndex = 52;
            // 
            // splitter1
            // 
            this.splitter1.Cursor = System.Windows.Forms.Cursors.HSplit;
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter1.Location = new System.Drawing.Point(0, 538);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(1113, 3);
            this.splitter1.TabIndex = 1;
            this.splitter1.TabStop = false;
            // 
            // panPurchases
            // 
            this.panPurchases.BackColor = System.Drawing.SystemColors.Control;
            this.panPurchases.Controls.Add(this.cmdReset);
            this.panPurchases.Controls.Add(this.elvPurchases);
            this.panPurchases.Controls.Add(this.cmdNext);
            this.panPurchases.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panPurchases.Location = new System.Drawing.Point(0, 0);
            this.panPurchases.Margin = new System.Windows.Forms.Padding(0);
            this.panPurchases.Name = "panPurchases";
            this.panPurchases.Size = new System.Drawing.Size(1113, 89);
            this.panPurchases.TabIndex = 53;
            // 
            // elvPurchases
            // 
            this.elvPurchases.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.elvPurchases.FullRowSelect = true;
            this.elvPurchases.HideSelection = false;
            this.elvPurchases.Location = new System.Drawing.Point(0, 0);
            this.elvPurchases.Name = "elvPurchases";
            this.elvPurchases.OwnerDraw = true;
            this.elvPurchases.Size = new System.Drawing.Size(716, 89);
            this.elvPurchases.SmallImageList = this.imglstPurchase;
            this.elvPurchases.TabIndex = 3;
            this.elvPurchases.Template = null;
            this.elvPurchases.UseCompatibleStateImageBehavior = false;
            this.elvPurchases.View = System.Windows.Forms.View.Details;
            // 
            // imglstPurchase
            // 
            this.imglstPurchase.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imglstPurchase.ImageStream")));
            this.imglstPurchase.TransparentColor = System.Drawing.Color.Transparent;
            this.imglstPurchase.Images.SetKeyName(0, "Invalid.png");
            this.imglstPurchase.Images.SetKeyName(1, "Valid.png");
            // 
            // panEntry
            // 
            this.panEntry.BackColor = System.Drawing.Color.Blue;
            this.panEntry.Controls.Add(this.panPurchases);
            this.panEntry.Controls.Add(this.grpExhibit);
            this.panEntry.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panEntry.Location = new System.Drawing.Point(0, 541);
            this.panEntry.Name = "panEntry";
            this.panEntry.Size = new System.Drawing.Size(1113, 195);
            this.panEntry.TabIndex = 54;
            // 
            // cmdReset
            // 
            this.cmdReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdReset.Location = new System.Drawing.Point(722, 66);
            this.cmdReset.Name = "cmdReset";
            this.cmdReset.Size = new System.Drawing.Size(66, 23);
            this.cmdReset.TabIndex = 5;
            this.cmdReset.Text = "Reset";
            this.cmdReset.UseVisualStyleBackColor = true;
            this.cmdReset.Click += new System.EventHandler(this.cmdReset_Click);
            // 
            // ucRunAuction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panMain);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.panEntry);
            this.Name = "ucRunAuction";
            this.Size = new System.Drawing.Size(1113, 736);
            this.grpExhibit.ResumeLayout(false);
            this.grpExhibit.PerformLayout();
            this.panMain.ResumeLayout(false);
            this.panPurchases.ResumeLayout(false);
            this.panEntry.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lsvAuctionOrder;
        private System.Windows.Forms.ColumnHeader colOrder;
        private System.Windows.Forms.ColumnHeader colExhibitorNumber;
        private System.Windows.Forms.ColumnHeader colExhibitorName;
        private System.Windows.Forms.ColumnHeader colAnimalType;
        private System.Windows.Forms.ColumnHeader colAnimalWeight;
        private System.Windows.Forms.ColumnHeader colBuyerNumber;
        private System.Windows.Forms.ColumnHeader colBuyerName;
        private System.Windows.Forms.ColumnHeader colWinningBid;
        private System.Windows.Forms.ColumnHeader colAnimalChampion;
        private System.Windows.Forms.Button cmdNext;
        private System.Windows.Forms.GroupBox grpExhibit;
        private System.Windows.Forms.Label lblMaxOrder;
        private System.Windows.Forms.Label lblCurOrder;
        private System.Windows.Forms.ColumnHeader colTagNumber;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblExhibitor;
        private System.Windows.Forms.Panel panMain;
        private System.Windows.Forms.TextBox txtComments;
        private System.Windows.Forms.Panel panPurchases;
        private System.Windows.Forms.Panel panEntry;
        private System.Windows.Forms.ColumnHeader colCheckedOut;
        private Helpers.EditableListView elvPurchases;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.ImageList imglstPurchase;
        private System.Windows.Forms.Button cmdReset;
    }
}
