using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Livestock_Auction.Helpers.Paradox;

namespace Livestock_Auction
{
    public partial class frmWinFairImport : Form
    {
        List<DB.clsExhibitor> lstNewExhibitors;

        public frmWinFairImport()
        {
            InitializeComponent();
        }

        public frmWinFairImport(string FileName, Helpers.Paradox.ParadoxFile ImportFile)
        {
            int iRecordsChanged = 0;
            int iRecordsAdded = 0;

            InitializeComponent();
            lstNewExhibitors = new List<DB.clsExhibitor>();
            tslblFileName.Text = string.Format(tslblFileName.Text, FileName);
            tslblTotalRecords.Text = string.Format(tslblTotalRecords.Text, ImportFile.Rows.Count);

            int iDeDupExhibitorID = 0;
            foreach (Record Row in ImportFile.Rows)
            {
                // Only take records where the MemberID starts with "P" (indicating they are 4-H), and that have
                //  a first name, which will filter out all of the clubs
                if (Row["MemberID"].ToString().StartsWith("P") && Row["FirstName"].ToString().Trim().Length > 0)
                {
                    int iNewExhibitorID = 0;
                    int iZipCode = 0;
                    if (int.TryParse(Row["MemberID"].ToString().TrimStart('P'), out iNewExhibitorID) && iDeDupExhibitorID != iNewExhibitorID)
                    {
                        iDeDupExhibitorID = iNewExhibitorID;

                        if (!int.TryParse(Row["Zip"].ToString().Trim('-', ' ', '\t').Substring(0, 5), out iZipCode))
                        {
                            iZipCode = 0;
                        }

                        DB.clsExhibitor NewExhibitor = new DB.clsExhibitor(iNewExhibitorID, Row["FirstName"].ToString(), Row["LastName"].ToString(), "", Row["Address"].ToString(), Row["City"].ToString(), Row["State"].ToString(), iZipCode);

                        bool bModified = false;
                        bool bAdded = false;
                        if (clsDB.Exhibitors[iNewExhibitorID] == null)
                        {
                            lstNewExhibitors.Add(NewExhibitor);
                            iRecordsAdded++;
                            bAdded = true;
                        }
                        else
                        {
                            DB.clsExhibitor OldExhibitor = clsDB.Exhibitors[iNewExhibitorID];
                            if (OldExhibitor.Name != OldExhibitor.Name || OldExhibitor.Address != NewExhibitor.Address)
                            {
                                //Preserves the nick name
                                OldExhibitor.Name.First = NewExhibitor.Name.First;
                                OldExhibitor.Name.Last = OldExhibitor.Name.Last;
                                OldExhibitor.Address = NewExhibitor.Address;

                                //Add the old record to be updated
                                lstNewExhibitors.Add(OldExhibitor);
                                iRecordsChanged++;
                                bModified = true;
                            }
                        }

                        ListViewItem lviRow = lsvExhibitors.Items.Add(iDeDupExhibitorID.ToString());
                        lviRow.SubItems.Add(Row["FirstName"].ToString());
                        lviRow.SubItems.Add(Row["LastName"].ToString());
                        lviRow.SubItems.Add(Row["Address"].ToString());
                        lviRow.SubItems.Add(Row["City"].ToString());
                        lviRow.SubItems.Add(Row["State"].ToString());
                        lviRow.SubItems.Add(Row["Zip"].ToString().Trim('-', ' ', '\t').Substring(0, 5));
                        if (bAdded)
                        {
                            lviRow.SubItems.Add("New");
                        }
                        else if (bModified)
                        {
                            lviRow.SubItems.Add("Modified");
                        }
                        else
                        {
                            lviRow.SubItems.Add("");
                        }
                    }
                }
            }
            tslblExhbitors.Text = string.Format(tslblExhbitors.Text, lsvExhibitors.Items.Count);
            tslblNewRecord.Text = string.Format(tslblNewRecord.Text, iRecordsAdded);
            tslblModifiedRecords.Text = string.Format(tslblModifiedRecords.Text, iRecordsChanged);
        }

        private void tscmdImportRecords_Click(object sender, EventArgs e)
        {
            foreach (DB.clsExhibitor Exhibitor in lstNewExhibitors)
            {
                try
                {
                    clsDB.Exhibitors.Commit(DB.CommitAction.Modify, Exhibitor);
                }
                catch (System.Data.SqlClient.SqlException)
                {
                    try
                    {
                        System.Threading.Thread.Sleep(50);
                        clsDB.Exhibitors.Commit(DB.CommitAction.Modify, Exhibitor);
                    }
                    catch (System.Data.SqlClient.SqlException ex)
                    {
                        MessageBox.Show(string.Format("Failed to import exhibitor ID: {0}, sql exception: {1}", Exhibitor.ExhibitorNumber, ex.Message));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Failed to import exhibitor ID: {0}, exception: {1}", Exhibitor.ExhibitorNumber, ex.Message));
                }
                
            }
            MessageBox.Show("Import Complete", "Import WinFair Exhibitors");
        }
    }
}
