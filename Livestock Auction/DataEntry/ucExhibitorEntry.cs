using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Livestock_Auction.DataEntry
{
    public partial class ucExhibitorEntry : UserControl
    {
        NameOrder CurrentOrder = NameOrder.FirstLast;
        EntryModes EntryMode = EntryModes.New;

        string sWarningText = "";   //Warning text is pulled from the starting text lblExhibitorWarning label.

        public ucExhibitorEntry()
        {
            InitializeComponent();
            sWarningText = lblExhibitorWarning.Text;
            lblExhibitorWarning.Text = string.Format(sWarningText, grpEntry.Text);
        }

        private void ucExhibitorEntry_Load(object sender, EventArgs e)
        {
            ResetAutoComplete();
        }

        private void txtExhibitorNumber_Leave(object sender, EventArgs e)
        {
            if (txtExhibitorNumber.Text.Trim() != "")
            {
                int iExNumber = 0;
                if (int.TryParse(txtExhibitorNumber.Text.Trim(), out iExNumber))
                {
                    DB.clsExhibitor exhibitor = clsDB.Exhibitors[iExNumber];
                    if (exhibitor != null)
                    {
                        txtNameFirst.Text = exhibitor.Name.First;
                        txtNameLast.Text = exhibitor.Name.Last;
                        txtNameNick.Text = exhibitor.Name.Nick;
                        lblExhibitorWarning.Visible = false;
                    }
                    else
                    {
                        lblExhibitorWarning.Visible = true;
                    }
                }
            }
        }

        private void txtNameFirst_Leave(object sender, EventArgs e)
        {
            if (txtNameFirst.Text.Trim() != "")
            {
                List<DB.clsExhibitor> lstExhibitors = new List<DB.clsExhibitor>();// clsDB.Exhibitors.Find(txtNameFirst.Text.Trim(), txtNameLast.Text.Trim());
                foreach (DB.clsExhibitor Ex in clsDB.Exhibitors)
                {
                    if ((txtNameFirst.Text.Trim().Length == 0 || Ex.Name.First == txtNameFirst.Text.Trim()) && (txtNameLast.Text.Trim().Length == 0 || Ex.Name.Last == txtNameLast.Text.Trim()))
                    {
                        lstExhibitors.Add(Ex);
                    }
                }

                if (lstExhibitors.Count == 1 && EntryMode == EntryModes.Lookup)
                {
                    txtExhibitorNumber.Text = lstExhibitors[0].ExhibitorNumber.ToString();
                    txtNameFirst.Text = lstExhibitors[0].Name.First;
                    txtNameLast.Text = lstExhibitors[0].Name.Last;
                    txtNameNick.Text = lstExhibitors[0].Name.Nick;
                    lblExhibitorWarning.Visible = false;

                    this.OnLeave(new EventArgs());
                }
                else
                {
                    //If the first name is first, then populate the autocomplete for last, otherwise the exhibitor doesn't exist at this point
                    if (CurrentOrder == NameOrder.FirstLast)
                    {
                        txtNameLast.AutoCompleteCustomSource.Clear();
                        foreach (DB.clsExhibitor Ex in lstExhibitors)
                        {
                            txtNameLast.AutoCompleteCustomSource.Add(Ex.Name.Last);
                        }
                    }
                    else if (txtExhibitorNumber.Text.Trim() == "")
                    {
                        txtExhibitorNumber.Focus();
                    }
                }
            }
        }

        private void txtNameLast_Leave(object sender, EventArgs e)
        {
            if (txtNameLast.Text.Trim() != "")
            {
                List<DB.clsExhibitor> lstExhibitors = new List<DB.clsExhibitor>();// clsDB.Exhibitors.Find(txtNameFirst.Text.Trim(), txtNameLast.Text.Trim());
                foreach (DB.clsExhibitor Ex in clsDB.Exhibitors)
                {
                    if ((txtNameFirst.Text.Trim().Length == 0 || Ex.Name.First == txtNameFirst.Text.Trim()) && (txtNameLast.Text.Trim().Length == 0 || Ex.Name.Last == txtNameLast.Text.Trim()))
                    {
                        lstExhibitors.Add(Ex);
                    }
                }

                if (lstExhibitors.Count == 1 && EntryMode == EntryModes.Lookup)
                {
                    txtExhibitorNumber.Text = lstExhibitors[0].ExhibitorNumber.ToString();
                    txtNameFirst.Text = lstExhibitors[0].Name.First;
                    txtNameLast.Text = lstExhibitors[0].Name.Last;
                    txtNameNick.Text = lstExhibitors[0].Name.Nick;
                    lblExhibitorWarning.Visible = false;

                    this.OnLeave(new EventArgs());
                }
                else
                {
                    //If the last name is first, then populate the autocomplete for first, otherwise the exhibitor doesn't exist at this point
                    if (CurrentOrder == NameOrder.LastFirst)
                    {
                        txtNameFirst.AutoCompleteCustomSource.Clear();
                        foreach (DB.clsExhibitor Ex in lstExhibitors)
                        {
                            txtNameFirst.AutoCompleteCustomSource.Add(Ex.Name.First);
                        }
                    }
                    else if (txtExhibitorNumber.Text.Trim() == "")
                    {
                        txtExhibitorNumber.Focus();
                    }
                }
            }
        }

        public void ResetAutoComplete()
        {
            txtNameFirst.AutoCompleteCustomSource.Clear();
            txtNameLast.AutoCompleteCustomSource.Clear();

            if (clsDB.Connected)
            {
                //TODO: This is not thread safe...
                string[] sFirstName = new string[clsDB.Exhibitors.Count];
                string[] sLastName = new string[clsDB.Exhibitors.Count];
                int i = 0;
                foreach (DB.clsExhibitor Ex in clsDB.Exhibitors)
                {
                    sFirstName[i] = Ex.Name.First;
                    sLastName[i] = Ex.Name.Last;
                    i++;
                }

                txtNameFirst.AutoCompleteCustomSource.AddRange(sFirstName);
                txtNameLast.AutoCompleteCustomSource.AddRange(sLastName);
            }
        }


        public void SetNameOrder(NameOrder NewOrder)
        {
            if (NewOrder != CurrentOrder)
            {
                Point ptLastNameTextPos = txtNameLast.Location;
                Point ptLastNameLblPos = lblNameLast.Location;
                Point ptLastNameCmdPos = cmdBrowseLastName.Location;
                int iLastNameTabOrder = txtNameLast.TabIndex;

                txtNameLast.Location = txtNameFirst.Location;
                lblNameLast.Location = lblNameFirst.Location;
                txtNameLast.TabIndex = txtNameFirst.TabIndex;
                cmdBrowseLastName.Location = cmdBrowseFirstName.Location;

                txtNameFirst.Location = ptLastNameTextPos;
                lblNameFirst.Location = ptLastNameLblPos;
                txtNameFirst.TabIndex = iLastNameTabOrder;
                cmdBrowseFirstName.Location = ptLastNameCmdPos;
            }

            CurrentOrder = NewOrder;
        }

        public void Clear()
        {
            txtExhibitorNumber.Text = "";
            txtNameFirst.Text = "";
            txtNameLast.Text = "";
            txtNameNick.Text = "";
        }

        public override string Text
        {
            get
            {
                return grpEntry.Text;
            }
            set
            {
                grpEntry.Text = value;
                lblExhibitorWarning.Text = string.Format(sWarningText, value);
            }
        }

        public EntryModes Mode
        {
            get
            {
                return EntryMode;
            }
            set
            {
                EntryMode = value;
            }
        }

        public int ExhibitorNumber
        {
            set
            {
                if (clsDB.Exhibitors[value] != null)
                {
                    this.Exhibitor = clsDB.Exhibitors[value];
                }
                else
                {
                    Clear();
                    txtExhibitorNumber.Text = value.ToString();
                }
            }
        }

        public DB.clsExhibitor Exhibitor
        {
            get
            {
                int iExNum = 0;
                if (int.TryParse(txtExhibitorNumber.Text.Trim(), out iExNum))
                {
                    if (txtNameFirst.Text.Trim().Length > 0 && txtNameLast.Text.Trim().Length > 0)
                    {
                        if (clsDB.Exhibitors[iExNum] != null)
                        {
                            DB.clsExhibitor Ex = clsDB.Exhibitors[iExNum];
                            Ex.Name.First = txtNameFirst.Text;
                            Ex.Name.Last = txtNameLast.Text;
                            Ex.Name.Nick = txtNameNick.Text;
                            return Ex;
                        }
                        else
                        {
                            return new DB.clsExhibitor(iExNum, txtNameFirst.Text.Trim(), txtNameLast.Text.Trim(), txtNameNick.Text.Trim());
                        }
                    }
                }
                return null;
            }
            set
            {
                if (value != null)
                {
                    txtExhibitorNumber.Text = value.ExhibitorNumber.ToString();
                    txtNameFirst.Text = value.Name.First;
                    txtNameLast.Text = value.Name.Last;
                    txtNameNick.Text = value.Name.Nick;
                }
                else
                {
                    Clear();
                }
            }
        }

        private void cmdBrowseFirstName_Click(object sender, EventArgs e)
        {
            DB.Types.Name SelectedName = frmBrowseName.BrowseNames("", txtNameLast.Text.Trim());
            if (SelectedName != null)
            {
                txtNameFirst.Text = SelectedName.First;
                txtNameLast.Text = SelectedName.Last;
                txtNameNick.Text = SelectedName.Nick;
                txtNameLast.Focus();
            }
        }

        private void cmdBrowseLastName_Click(object sender, EventArgs e)
        {
            DB.Types.Name SelectedName = frmBrowseName.BrowseNames(txtNameFirst.Text.Trim(), "");
            if (SelectedName != null)
            {
                txtNameFirst.Text = SelectedName.First;
                txtNameLast.Text = SelectedName.Last;
                txtNameNick.Text = SelectedName.Nick;
                txtNameFirst.Focus();
            }
        }
    }
}
