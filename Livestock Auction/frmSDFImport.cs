using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Livestock_Auction
{
    public partial class frmSDFImport : Form
    {
        System.Data.IDbConnection m_dbConn;

        public frmSDFImport()
        {
            InitializeComponent();
            m_dbConn = null;
        }

        public frmSDFImport(System.Data.IDbConnection DatabaseConnection, bool History, bool Current)
        {
            InitializeComponent();
            m_dbConn = DatabaseConnection;
            DisplayStatistics(m_dbConn);

            if (History)
            {
                chkImportHistory.Checked = true;
                chkHistAll.Checked = true;
            }
            else if (Current)
            {
                chkImportCurrent.Checked = true;
                chkCurAll.Checked = true;
            }
        }

        private void DisplayStatistics(System.Data.IDbConnection DatabaseConnection)
        {
            //Display the start and end dates of the file
            try
            {
                System.Data.IDbCommand dbDateCommand = DatabaseConnection.CreateCommand();
                dbDateCommand.CommandText = "SELECT MIN(mindate) as mindate, MAX(maxdate) as maxdate FROM (" +
                                            "SELECT MIN(CommitDate) as mindate, MAX(CommitDate) as maxdate FROM Purchases UNION " +
                                            "SELECT MIN(CommitDate) as mindate, MAX(CommitDate) as maxdate FROM Payments UNION " +
                                            "SELECT MIN(CommitDate) as mindate, MAX(CommitDate) as maxdate FROM Market UNION " +
                                            "SELECT MIN(CommitDate) as mindate, MAX(CommitDate) as maxdate FROM Exhibits UNION " +
                                            "SELECT MIN(CommitDate) as mindate, MAX(CommitDate) as maxdate FROM Exhibitors UNION " +
                                            "SELECT MIN(CommitDate) as mindate, MAX(CommitDate) as maxdate FROM Buyers UNION " +
                                            "SELECT MIN(CommitDate) as mindate, MAX(CommitDate) as maxdate FROM AuctionOrder) AS dates";

                IDataReader dbDateReader = dbDateCommand.ExecuteReader();
                dbDateReader.Read();
                lblDateOldest.Text = DateTime.Parse((string)dbDateReader["mindate"]).ToShortDateString();
                lblDateNewest.Text = DateTime.Parse((string)dbDateReader["maxdate"]).ToShortDateString();
                dbDateReader.Close();
            }
            catch (System.Data.Common.DbException ex)
            {
                lblDateOldest.Text = "Failed to read file";
                lblDateNewest.Text = ex.Message;
                ErrorTip(lblDateOldest, ex.Message);
                ErrorTip(lblDateNewest, ex.Message);
            }

            //Display the counts
            try
            {
                lblCountExhibitors.Text = GetRecordCount(DatabaseConnection, new DB.Setup.Exhibitors()).ToString();
            }
            catch (System.Data.Common.DbException ex)
            {
                lblCountExhibitors.Text = "Error";
                ErrorTip(lblCountExhibitors, ex.Message);
            }
            
            try
            {
                lblCountBuyers.Text = GetRecordCount(DatabaseConnection, new DB.Setup.Buyers()).ToString();
            }
            catch (System.Data.Common.DbException ex)
            {
                lblCountBuyers.Text = "Error";
                ErrorTip(lblCountBuyers, ex.Message);
            }
            
            try
            {
                lblCountExhibits.Text = GetRecordCount(DatabaseConnection, new DB.Setup.Exhibits()).ToString();
            }
            catch (System.Data.Common.DbException ex)
            {
                lblCountExhibits.Text = "Error";
                ErrorTip(lblCountExhibits, ex.Message);
            }
        }

        private int GetRecordCount(System.Data.IDbConnection DatabaseConnection, DB.Setup.AuctionDBSetup SetupClass)
        {
            System.Data.IDbCommand dbExhibitorsCommand = SetupClass.SQLLoadDataQuery(DatabaseConnection);
            dbExhibitorsCommand.CommandText = string.Format("SELECT COUNT(*) FROM ({0}) as records", dbExhibitorsCommand.CommandText);
            return (int)dbExhibitorsCommand.ExecuteScalar();
        }

        private void ErrorTip(Control control, string caption)
        {
            ToolTip ttError = new ToolTip();
            ttError.ToolTipIcon = ToolTipIcon.Error;
            ttError.IsBalloon = false;
            ttError.ToolTipTitle = "Error";
            ttError.SetToolTip(control, caption);
        }

        private void chkImportHistory_CheckedChanged(object sender, EventArgs e)
        {
            if (chkImportHistory.Checked)
            {
                chkImportCurrent.Checked = false;
            }

            chkHistAll.Enabled = chkImportHistory.Checked;
            chkHistBuyerHistory.Enabled = chkImportHistory.Checked && !chkHistAll.Checked;
            chkHistExhibitors.Enabled = chkImportHistory.Checked && !chkHistAll.Checked;

            cmdImport.Enabled = (chkImportHistory.Checked && (chkHistBuyerHistory.Checked || chkHistExhibitors.Checked)) || (chkImportCurrent.Checked && (chkCurExhibits.Checked || chkCurOrder.Checked || chkCurBuyers.Checked || chkCurPurchases.Checked));
        }

        private void chkImportCurrent_CheckedChanged(object sender, EventArgs e)
        {
            if (chkImportCurrent.Checked)
            {
                chkImportHistory.Checked = false;
            }

            chkCurAll.Enabled = chkImportCurrent.Checked;
            chkCurBuyers.Enabled = chkImportCurrent.Checked && !chkCurAll.Checked && !chkCurPurchases.Checked;
            chkCurExhibits.Enabled = chkImportCurrent.Checked && !chkCurAll.Checked && !chkCurOrder.Checked && !chkCurPurchases.Checked;
            chkCurOrder.Enabled = chkImportCurrent.Checked && !chkCurAll.Checked;
            chkCurPurchases.Enabled = chkImportCurrent.Checked && !chkCurAll.Checked;

            cmdImport.Enabled = (chkImportHistory.Checked && (chkHistBuyerHistory.Checked || chkHistExhibitors.Checked)) || (chkImportCurrent.Checked && (chkCurExhibits.Checked || chkCurOrder.Checked || chkCurBuyers.Checked || chkCurPurchases.Checked));
        }

        private void chkHistAll_CheckedChanged(object sender, EventArgs e)
        {

            chkHistBuyerHistory.Checked = chkHistAll.Checked;
            chkHistExhibitors.Checked = chkHistAll.Checked;

            chkHistBuyerHistory.Enabled = !chkHistAll.Checked;
            chkHistExhibitors.Enabled = !chkHistAll.Checked;
        }

        private void chkCurAll_CheckedChanged(object sender, EventArgs e)
        {
            chkCurBuyers.Checked = chkCurAll.Checked;
            chkCurExhibits.Checked = chkCurAll.Checked;
            chkCurOrder.Checked = chkCurAll.Checked;
            chkCurPurchases.Checked = chkCurAll.Checked;

            chkCurBuyers.Enabled = chkImportCurrent.Checked && !chkCurAll.Checked && !chkCurPurchases.Checked;
            chkCurExhibits.Enabled = chkImportCurrent.Checked && !chkCurAll.Checked;
            chkCurOrder.Enabled = chkImportCurrent.Checked && !chkCurAll.Checked && !chkCurOrder.Checked && !chkCurPurchases.Checked;
            chkCurPurchases.Enabled = chkImportCurrent.Checked && !chkCurAll.Checked;
        }

        private void chkCurrentCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (sender == chkCurOrder && chkCurOrder.Checked)
            {
                chkCurExhibits.Checked = true;
            }

            if (sender == chkCurPurchases && chkCurPurchases.Checked)
            {
                chkCurExhibits.Checked = true;
                chkCurBuyers.Checked = true;
            }

            chkCurExhibits.Enabled = chkImportCurrent.Checked && !chkCurAll.Checked && !chkCurOrder.Checked && !chkCurPurchases.Checked;
            chkCurBuyers.Enabled = chkImportCurrent.Checked && !chkCurAll.Checked && !chkCurPurchases.Checked;

            cmdImport.Enabled = (chkImportHistory.Checked && (chkHistBuyerHistory.Checked || chkHistExhibitors.Checked)) || (chkImportCurrent.Checked && (chkCurExhibits.Checked || chkCurOrder.Checked || chkCurBuyers.Checked || chkCurPurchases.Checked));
        }

        private void chkHistoryCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            cmdImport.Enabled = (chkImportHistory.Checked && (chkHistBuyerHistory.Checked || chkHistExhibitors.Checked)) || (chkImportCurrent.Checked && (chkCurExhibits.Checked || chkCurOrder.Checked || chkCurBuyers.Checked || chkCurPurchases.Checked));
        }
        


        private void cmdImport_Click(object sender, EventArgs e)
        {
            cmdImport.Enabled = false;
            if (chkImportCurrent.Checked)
            {
                clsDB.ImportDatabase(m_dbConn, chkCurExhibits.Checked, chkCurOrder.Checked, chkCurBuyers.Checked, chkCurPurchases.Checked);

                MessageBox.Show("Finshed importing database");
                this.Close();
            }
            else if (chkImportHistory.Checked)
            {
                clsDB.ImportHistory(m_dbConn, chkHistBuyerHistory.Checked, chkHistExhibitors.Checked);
                MessageBox.Show("Finshed importing database history");
                this.Close();
            }


            cmdImport.Enabled = true;
        }
    }
}
