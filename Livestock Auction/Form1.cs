using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Livestock_Auction
{
    public enum NameOrder
    {
        FirstLast,
        LastFirst
    }


    public partial class frmMain : Form
    {
        ucRunAuction AuctionPanel = null;
        ucExhibitors ExhibitorsPanel = null;
        ucExhibits AnimalsPanel = null;
        ucBuyer BuyersPanel = null;
        ucMarketItems MarketPanel = null;
        ucBuyerCheckout BuyerCheckoutPanel = null;

        NameOrder CurrentNameOrder = NameOrder.LastFirst;

        //These values are set to the inital text of the corresponding status fields
        string FORMAT_STATUSAUCTION = "";
        string FORMAT_STATUSCHECKOUT = "";
        string FORMAT_STATUSREGISTRATION = "";

        public frmMain()
        {
            InitializeComponent();
            FORMAT_STATUSAUCTION = tslblAuctionStatus.Text;
            FORMAT_STATUSCHECKOUT = tslblCheckoutStatus.Text;
            FORMAT_STATUSREGISTRATION = tslblRegistrationStatus.Text;
        }

        public frmMain(System.Data.IDbConnection dbConn)
        {
            InitializeComponent();
            FORMAT_STATUSAUCTION = tslblAuctionStatus.Text;
            FORMAT_STATUSCHECKOUT = tslblCheckoutStatus.Text;
            FORMAT_STATUSREGISTRATION = tslblRegistrationStatus.Text;

            try
            {
                clsDB.Connect(dbConn);
                UpdateStatusBar();

                //After revisions to the status bar, the values are only dependent on purchases and buyers
                clsDB.Buyers.Updated += new EventHandler<DB.DatabaseUpdatedEventArgs>(Database_Updated);
                //clsDB.Exhibitors.Updated += new EventHandler<DatabaseUpdatedEventArgs>(Database_Updated);
                clsDB.Exhibits.Updated += new EventHandler<DB.DatabaseUpdatedEventArgs>(Database_Updated);
                clsDB.Purchases.Updated += new EventHandler<DB.DatabaseUpdatedEventArgs>(Database_Updated);
                clsDB.Auction.Updated += new EventHandler<DB.DatabaseUpdatedEventArgs>(Database_Updated);

            }
            catch (Exception ex)
            {
                MessageBox.Show("An error has occured\r\nMessage: " + ex.Message);
                clsLogger.WriteLog(string.Format("An error occured connecting to the database:\r\nMessage:{0}\r\nStack Trace:\r\n{1}", ex.Message, ex.StackTrace));
            }

        }

        private void Database_Updated(object sender, DB.DatabaseUpdatedEventArgs e)
        {
            try
            {
                UpdateStatusBar();
            }
            catch (Exception ex)
            {
                clsLogger.WriteLog(string.Format("An error occured updating the statusbar:\r\nMessage:{0}\r\nStack Trace:\r\n{1}", ex.Message, ex.StackTrace));
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                spcMain.Panel2Collapsed = true;

                AuctionPanel = new ucRunAuction();
                tabAuction.Controls.Add(AuctionPanel);
                AuctionPanel.Dock = DockStyle.Fill;
                AuctionPanel.BringToFront();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error has occured\r\nMessage: " + ex.Message);
                clsLogger.WriteLog(string.Format("An error occured connecting to the database:\r\nMessage:{0}\r\nStack Trace:\r\n{1}", ex.Message, ex.StackTrace));
            }
        }

        private void tabMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabMain.SelectedTab == tabAuction)
            {
                if (AuctionPanel == null)
                {
                    AuctionPanel = new ucRunAuction();
                    tabAuction.Controls.Add(AuctionPanel);
                    AuctionPanel.Dock = DockStyle.Fill;

                    AuctionPanel.BringToFront();
                }
            }
            else if (tabMain.SelectedTab == tabExhibitors)
            {
                if (ExhibitorsPanel == null)
                {
                    ExhibitorsPanel = new ucExhibitors();
                    tabExhibitors.Controls.Add(ExhibitorsPanel);
                    ExhibitorsPanel.Dock = DockStyle.Fill;

                    ExhibitorsPanel.SetNameOrder(CurrentNameOrder);

                    ExhibitorsPanel.BringToFront();
                }
            }
            else if (tabMain.SelectedTab == tabAnimals)
            {
                if (AnimalsPanel == null)
                {
                    AnimalsPanel = new ucExhibits();
                    tabAnimals.Controls.Add(AnimalsPanel);
                    AnimalsPanel.Dock = DockStyle.Fill;

                    AnimalsPanel.SetNameOrder(CurrentNameOrder);

                    AnimalsPanel.BringToFront();
                }
            }
            else if (tabMain.SelectedTab == tabBuyers)
            {
                if (BuyersPanel == null)
                {
                    BuyersPanel = new ucBuyer();
                    tabBuyers.Controls.Add(BuyersPanel);
                    BuyersPanel.Dock = DockStyle.Fill;

                    BuyersPanel.SetNameOrder(CurrentNameOrder);

                    BuyersPanel.BringToFront();
                }
            }
            else if (tabMain.SelectedTab == tabMarket)
            {
                if (MarketPanel == null)
                {
                    MarketPanel = new ucMarketItems();
                    MarketPanel.Dock = DockStyle.Fill;
                    tabMarket.Controls.Add(MarketPanel);
                    
                    MarketPanel.BringToFront();
                }
            }
            else if (tabMain.SelectedTab == tabBuyerCheckout)
            {
                if (BuyerCheckoutPanel == null)
                {
                    try
                    {
                        BuyerCheckoutPanel = new ucBuyerCheckout();
                        tabBuyerCheckout.Controls.Add(BuyerCheckoutPanel);
                        BuyerCheckoutPanel.Dock = DockStyle.Fill;

                        BuyerCheckoutPanel.SetNameOrder(CurrentNameOrder);

                        BuyerCheckoutPanel.BringToFront();
                    }
                    catch (System.IO.FileNotFoundException Ex)
                    {
                        MessageBox.Show("Error loading report viewer", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void tscmdMoveMarket_Click(object sender, EventArgs e)
        {
            MoveTab(tabMarket, tscmdMoveMarket);
        }

        private void tscmdMoveBuyers_Click(object sender, EventArgs e)
        {
            MoveTab(tabBuyers, tscmdMoveBuyers);
        }

        private void tscmdMoveAnimals_Click(object sender, EventArgs e)
        {
            MoveTab(tabAnimals, tscmdMoveAnimals);
        }

        private void tscmdMoveExhibitors_Click(object sender, EventArgs e)
        {
            MoveTab(tabExhibitors, tscmdMoveExhibitors);
        }

        private void tscmdMoveAuction_Click(object sender, EventArgs e)
        {
            MoveTab(tabAuction, tscmdMoveAuction);
        }

        private void tscmdMoveBuyerCheckout_Click(object sender, EventArgs e)
        {
            MoveTab(tabBuyerCheckout, tscmdMoveBuyerCheckout);
        }




        private void MoveTab(TabPage MovingTab, ToolStripButton Sender)
        {
            if (MovingTab.Parent == tabMain)
            {
                spcMain.Panel2Collapsed = false;
                tabMain.TabPages.Remove(MovingTab);
                tabSide.TabPages.Add(MovingTab);
                Sender.Alignment = ToolStripItemAlignment.Left;
                Sender.Text = "<< Move";
                tabSide.SelectedTab = MovingTab;

                if (tabMain.TabPages.Count == 0)
                {
                    spcMain.Panel1Collapsed = true;
                }
            }
            else
            {
                spcMain.Panel1Collapsed = false;
                tabSide.TabPages.Remove(MovingTab);
                tabMain.TabPages.Add(MovingTab);
                Sender.Alignment = ToolStripItemAlignment.Right;
                Sender.Text = "Move >>";
                tabMain.SelectedTab = MovingTab;

                if (tabSide.TabPages.Count == 0)
                {
                    spcMain.Panel2Collapsed = true;
                }
                else
                {
                    spcMain.Panel2Collapsed = false;
                }
            }
        }

        private void tsmnuNameOrderFirstLast_Click(object sender, EventArgs e)
        {
            tsmnuNameOrderLastFirst.Checked = false;
            if (BuyersPanel != null)
            {
                BuyersPanel.SetNameOrder(NameOrder.FirstLast);
            }
            if (ExhibitorsPanel != null)
            {
                ExhibitorsPanel.SetNameOrder(NameOrder.FirstLast);
            }
            if (AnimalsPanel != null)
            {
                AnimalsPanel.SetNameOrder(NameOrder.FirstLast);
            }
            if (BuyerCheckoutPanel != null)
            {
                BuyerCheckoutPanel.SetNameOrder(NameOrder.FirstLast);
            }

            CurrentNameOrder = NameOrder.FirstLast;
        }

        private void tsmnuNameOrderLastFirst_Click(object sender, EventArgs e)
        {
            tsmnuNameOrderFirstLast.Checked = false;
            if (BuyersPanel != null)
            {
                BuyersPanel.SetNameOrder(NameOrder.LastFirst);
            }
            if (ExhibitorsPanel != null)
            {
                ExhibitorsPanel.SetNameOrder(NameOrder.LastFirst);
            }
            if (AnimalsPanel != null)
            {
                AnimalsPanel.SetNameOrder(NameOrder.LastFirst);
            }
            if (BuyerCheckoutPanel != null)
            {
                BuyerCheckoutPanel.SetNameOrder(NameOrder.LastFirst);
            }

            CurrentNameOrder = NameOrder.LastFirst;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            clsDB.Disconnect();
            Application.Exit();
        }

        private void tscmdShowRunAuction_Click(object sender, EventArgs e)
        {
            if (tscmdShowRunAuction.Checked)
            {
                if (!tabMain.TabPages.Contains(tabAuction))
                {
                    tabMain.TabPages.Add(tabAuction);
                }
            }
            else
            {
                if (tabMain.TabPages.Contains(tabAuction))
                {
                    tabMain.TabPages.Remove(tabAuction);
                }
            }
        }

        private void tscmdShowExhibitors_Click(object sender, EventArgs e)
        {
            if (tscmdShowExhibitors.Checked)
            {
                if (!tabMain.TabPages.Contains(tabExhibitors))
                {
                    tabMain.TabPages.Add(tabExhibitors);
                }
            }
            else
            {
                if (tabMain.TabPages.Contains(tabExhibitors))
                {
                    tabMain.TabPages.Remove(tabExhibitors);
                }
            }
        }

        private void tscmdShowExhibits_Click(object sender, EventArgs e)
        {
            if (tscmdShowExhibits.Checked)
            {
                if (!tabMain.TabPages.Contains(tabAnimals))
                {
                    tabMain.TabPages.Add(tabAnimals);
                }
            }
            else
            {
                if (tabMain.TabPages.Contains(tabAnimals))
                {
                    tabMain.TabPages.Remove(tabAnimals);
                }
            }
        }

        private void tscmdShowBuyers_Click(object sender, EventArgs e)
        {
            if (tscmdShowBuyers.Checked)
            {
                if (!tabMain.TabPages.Contains(tabBuyers))
                {
                    tabMain.TabPages.Add(tabBuyers);
                }
            }
            else
            {
                if (tabMain.TabPages.Contains(tabBuyers))
                {
                    tabMain.TabPages.Remove(tabBuyers);
                }
            }
        }

        private void tscmdShowMarketItems_Click(object sender, EventArgs e)
        {
            if (tscmdShowMarketItems.Checked)
            {
                if (!tabMain.TabPages.Contains(tabMarket))
                {
                    tabMain.TabPages.Add(tabMarket);
                }
            }
            else
            {
                if (tabMain.TabPages.Contains(tabMarket))
                {
                    tabMain.TabPages.Remove(tabMarket);
                }
            }
        }

        private void tscmdShowBuyerCheckout_Click(object sender, EventArgs e)
        {
            if (tscmdShowBuyerCheckout.Checked)
            {
                if (!tabMain.TabPages.Contains(tabBuyerCheckout))
                {
                    tabMain.TabPages.Add(tabBuyerCheckout);
                }
            }
            else
            {
                if (tabMain.TabPages.Contains(tabBuyerCheckout))
                {
                    tabMain.TabPages.Remove(tabBuyerCheckout);
                }
            }
        }

        private void UpdateStatusBar()
        {
            int iPurchases = 0;
            foreach (DB.clsAuctionIndex Exhibit in clsDB.Auction)
            {
                if (Exhibit.Exhibit != null && Exhibit.Exhibit.Purchases != null)
                {
                    iPurchases++;
                }
            }
            tslblAuctionStatus.Text = string.Format(FORMAT_STATUSAUCTION, iPurchases, clsDB.Auction.Items.Count);

            int iBuyersWhoPurchased = 0;
            int iBuyersWhoCheckedout = 0;
            foreach (DB.clsBuyer Buyer in clsDB.Buyers)
            {
                if (clsDB.Purchases.GetPurchasesByBuyer(Buyer.BuyerNumber).Count > 0)
                {
                    iBuyersWhoPurchased++;
                }
                if (Buyer.CheckedOut)
                {
                    iBuyersWhoCheckedout++;
                }
            }
            tslblCheckoutStatus.Text = string.Format(FORMAT_STATUSCHECKOUT, iBuyersWhoCheckedout, iBuyersWhoPurchased);

            tslblRegistrationStatus.Text = string.Format(FORMAT_STATUSREGISTRATION, clsDB.Buyers.Count);
            tslblPurchases.Text = string.Format("Gross Sold: {0:C2}", clsDB.Purchases.GrossSold);
        }

        private void tscmdReportAnimalDestinations_Click(object sender, EventArgs e)
        {
            Reports.Forms.frmrptAnimalDestinations Report = new Livestock_Auction.Reports.Forms.frmrptAnimalDestinations();
            Report.Show();
        }

        private void tscmdReportsWeightSold_Click(object sender, EventArgs e)
        {
            Reports.Forms.frmrptWeightSold Report = new Livestock_Auction.Reports.Forms.frmrptWeightSold();
            Report.Show();
        }

        private void exhibitorReceiptsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reports.Forms.frmrptReceipts Report = new Livestock_Auction.Reports.Forms.frmrptReceipts();
            Report.Show();
        }

        private void auctionOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAuctionOrder frmOrder = new frmAuctionOrder();
            try
            {
                frmOrder.Show();
            }
            catch (System.IO.FileNotFoundException ex)
            {
                MessageBox.Show("Error loading report viewer", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnucmdExportDatabase_Click(object sender, EventArgs e)
        {
            if (dlgExport.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                clsDB.ExportDatabase(dlgExport.FileName);
                MessageBox.Show("Export Complete");
            }
        }

        private void mnucmdImportExhibitors_Click(object sender, EventArgs e)
        {
            dlgOpenImport.Filter = "WinFair Exhibitors|Exhibito.db";
            dlgOpenImport.FileName = "Exhibito.DB";
            if (dlgOpenImport.ShowDialog() == DialogResult.OK)
            {
                Helpers.Paradox.ParadoxFile ImportFile = new Livestock_Auction.Helpers.Paradox.ParadoxFile(dlgOpenImport.OpenFile());
                frmWinFairImport WinFair = new frmWinFairImport(dlgOpenImport.FileName, ImportFile);
                WinFair.ShowDialog();
            }
        }

        private void mnucmdImportHistory_Click(object sender, EventArgs e)
        {
            ImportData(true, false);
        }

        private void mnucmdImportDatabase_Click(object sender, EventArgs e)
        {
            ImportData(false, true);
        }

        private void lnkImport_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ImportData(false, false);
        }

        private void ImportData(bool bHistory, bool bCurrent)
        {
            dlgOpenImport.Filter = "SQL Compact Export|*.sdf";
            dlgOpenImport.FileName = "";
            if (dlgOpenImport.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(dlgOpenImport.FileName))
                {
                    string sDbConnString = string.Format("Data Source={0};File Mode=Read Write;Persist Security Info=False;", dlgOpenImport.FileName);
                    System.Data.SqlServerCe.SqlCeConnection dbConnection = new System.Data.SqlServerCe.SqlCeConnection(sDbConnString);
                    try
                    {
                        dbConnection.Open();

                        frmSDFImport ImportFile = new frmSDFImport(dbConnection, bHistory, bCurrent);
                        ImportFile.ShowDialog();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(String.Format("Failed to open file: {0}", ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("File not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void lnkMarket_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            tabMain.SelectedTab = tabMarket;
        }

        private void lnkExhibits_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            tabMain.SelectedTab = tabAnimals;
        }

        private void lnkAuctionOrder_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAuctionOrder frmOrder = new frmAuctionOrder();
            try
            {
                frmOrder.Show();
            }
            catch (System.IO.FileNotFoundException ex)
            {
                MessageBox.Show("Error loading report viewer", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lnkBuyers_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            tabMain.SelectedTab = tabBuyers;
        }

        private void lnkRunAuction_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            tabMain.SelectedTab = tabAuction;
        }

        private void lnkBuyerCheckout_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            tabMain.SelectedTab = tabBuyerCheckout;
        }
        
        private void lnkReceipts_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Reports.Forms.frmrptReceipts Report = new Livestock_Auction.Reports.Forms.frmrptReceipts();
            Report.Show();
        }

        private void mnucmdExportToExcel_Click(object sender, EventArgs e)
        {
            if (dlgExportExcel.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                clsDB.ExportToExcel(dlgExportExcel.FileName);
            }
        }
    }
}
