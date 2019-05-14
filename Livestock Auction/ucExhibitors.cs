using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Livestock_Auction
{
    public partial class ucExhibitors : UserControl
    {
        private enum EntryMode
        {
            NoRecord,
            NewRecord,
            ModifyRecord,
            ChangeID
        }

        public enum ExhibitEntryColumns
        {
            Market_Item = 0,
            Tag_Number = 1,
            Champion_Status = 2,
            Rate_Of_Gain = 3,
            Weight = 4,
            Take_Back = 5,
            Include = 6,
            Remove = 7
        }

        DB.clsExhibitor CurExhibitor = null;
        EntryMode CurrentMode = EntryMode.NoRecord;
        ucExhibitorCheckout CheckoutPanel = null;

        NameOrder CurrentOrder = NameOrder.LastFirst;

        public ucExhibitors()
        {
            InitializeComponent();
            elvExhibits.Columns.Add("Market Item");         //Text Box W/ Auto-Complete
            elvExhibits.Columns.Add("Tag Number");          //Text Box W/ Numeric Mask
            elvExhibits.Columns.Add("Champion Status");     //Text Box W/ Auto-Complete
            elvExhibits.Columns.Add("Rate of Gain");        //Check box
            elvExhibits.Columns.Add("Weight");              //Text Box W/ Numeric Mask
            elvExhibits.Columns.Add("Take Back");           //Radio Buttons (Yes/No/Not Set)
            elvExhibits.Columns.Add("Include in Auction");  //Check box
            elvExhibits.Columns.Add("Remove Exhibit");      //Remove Exhibit

            elvExhibits.Columns[(int)ExhibitEntryColumns.Market_Item].Width = 100;
            elvExhibits.Columns[(int)ExhibitEntryColumns.Tag_Number].Width = 75;
            elvExhibits.Columns[(int)ExhibitEntryColumns.Champion_Status].Width = 100;
            elvExhibits.Columns[(int)ExhibitEntryColumns.Rate_Of_Gain].Width = 100;
            elvExhibits.Columns[(int)ExhibitEntryColumns.Take_Back].Width = 175;
            elvExhibits.Columns[(int)ExhibitEntryColumns.Include].Width = 100;
            elvExhibits.Columns[(int)ExhibitEntryColumns.Remove].Width = 100;

            elvExhibits.Template = null;

            lsvExhibitors.ListViewItemSorter = new DB.clsExhibitor.ExhibitorViewSorter();

            ReloadGrid();

            SetMode(EntryMode.NoRecord, null);

            clsDB.Exhibitors.Updated += new EventHandler<DB.DatabaseUpdatedEventArgs>(Exhibitors_Updated);
        }


        void Exhibitors_Updated(object sender, DB.DatabaseUpdatedEventArgs e)
        {
            this.Invoke(new DB.UpdateInvoker(UpdateGrid), e.UpdatedItems);
        }

        private void lsvExhibitors_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            ((DB.clsExhibitor.ExhibitorViewSorter)lsvExhibitors.ListViewItemSorter).SetSortColumn((DB.clsExhibitor.ExhibitorColumns)e.Column);
            lsvExhibitors.Sort();
        }

        private void chkAddresses_CheckedChanged(object sender, EventArgs e)
        {
            txtAddress.Enabled = chkAddresses.Checked;
            txtState.Enabled = chkAddresses.Checked;
            txtZip.Enabled = chkAddresses.Checked;
            txtCity.Enabled = chkAddresses.Checked;

            SetMode(CurrentMode, CurExhibitor);
        }


        private void cmdCommit_Click(object sender, EventArgs e)
        {
            CommitRecord();
        }

        private void cmdCommit_Leave(object sender, EventArgs e)
        {
            cmdCommit.TabIndex = 8;
        }

        private void cmdCommit_Enter(object sender, EventArgs e)
        {
            cmdCommit.TabIndex = 0;
        }

        private void txtExhibitorNumber_Leave(object sender, EventArgs e)
        {
            int iExID;
            if (int.TryParse(txtExhibitorNumber.Text, out iExID))
            {
                ClearError(txtExhibitorNumber);

                if (CurrentMode == EntryMode.NoRecord)
                {
                    DB.clsExhibitor exhibitor = clsDB.Exhibitors[iExID];
                    if (exhibitor != null)
                    {
                        LoadRecord(exhibitor);
                    }
                    else
                    {
                        NewRecord();
                    }

                    if (CurrentOrder == NameOrder.LastFirst)
                    {
                        txtNameLast.Focus();
                    }
                    else
                    {
                        txtNameFirst.Focus();
                    }
                }
                else if (CurrentMode == EntryMode.ChangeID)
                {
                    
                    if (clsDB.Exhibitors[iExID] != null)
                    {
                        MessageBox.Show("A record with this ID already exists", "Record Exists", MessageBoxButtons.OK);
                        if (CurrentOrder == NameOrder.LastFirst)
                        {
                            txtNameLast.Focus();
                        }
                        else
                        {
                            txtNameFirst.Focus();
                        }
                    }
                }
            }
            else
            {
                if (txtExhibitorNumber.Text.Trim().Length > 0)
                {
                    ErrorField(txtExhibitorNumber);
                    txtExhibitorNumber.Focus();
                }
            }
        }

        private void txtNameLast_Leave(object sender, EventArgs e)
        {
            if (((TextBox)sender).Text == "")
            {
                ErrorField((TextBox)sender);
                ((TextBox)sender).Focus();
            }
            else
            {
                ClearError((TextBox)sender);
            }
        }

        private void txtNameFirst_Leave(object sender, EventArgs e)
        {
            if (((TextBox)sender).Text == "")
            {
                ErrorField((TextBox)sender);
                ((TextBox)sender).Focus();
            }
            else
            {
                ClearError((TextBox)sender);
            }
        }

        private void lsvExhibitors_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lsvExhibitors.SelectedItems.Count > 0)
            {
                if (lsvExhibitors.Tag == null || (int)lsvExhibitors.Tag != ((DB.clsExhibitor)lsvExhibitors.SelectedItems[0]).ExhibitorNumber)
                {
                    lsvExhibitors.Tag = ((DB.clsExhibitor)lsvExhibitors.SelectedItems[0]).ExhibitorNumber;
                    LoadRecord((DB.clsExhibitor)lsvExhibitors.SelectedItems[0]);
                    if (CheckoutPanel != null)
                    {
                        CheckoutPanel.LoadExhibitor((DB.clsExhibitor)lsvExhibitors.SelectedItems[0]);
                    }
                }
            }
            else
            {
                ClearRecord();
            }
        }

        private void cmdNew_Click(object sender, EventArgs e)
        {
            ClearRecord();
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            int iExID;

            if (int.TryParse(txtExhibitorNumber.Text, out iExID))
            {
                DB.clsExhibitor exhibitor = clsDB.Exhibitors[iExID];
                if (exhibitor != null)
                {
                    LoadRecord(exhibitor);
                    if (MessageBox.Show("Are you sure you want to delete this record?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        clsDB.Exhibitors.Commit(DB.CommitAction.Delete, exhibitor);
                    }
                }
            }
        }

        private void cmdCopy_Click(object sender, EventArgs e)
        {
            int iExID;
            if (int.TryParse(txtExhibitorNumber.Text, out iExID))
            {
                CopyRecord(clsDB.Exhibitors[iExID]);
                txtExhibitorNumber.Focus();
            }
        }

        private void cmdCheckout_Click(object sender, EventArgs e)
        {
            if (CheckoutPanel == null)
            {
                CheckoutPanel = new ucExhibitorCheckout();
                panSub.Visible = true;
                splSub.Visible = true;
                panSub.Controls.Add(CheckoutPanel);
                CheckoutPanel.Dock = DockStyle.Fill;
                if (lsvExhibitors.SelectedItems.Count > 0)
                {
                    CheckoutPanel.LoadExhibitor((DB.clsExhibitor)lsvExhibitors.SelectedItems[0]);
                }

                cmdCheckout.Text = "Checkout <<";
            }
            else
            {
                CheckoutPanel.Dispose();
                CheckoutPanel = null;
                panSub.Visible = false;
                splSub.Visible = false;
                cmdCheckout.Text = "Checkout >>";
            }
        }








        private void SetMode(EntryMode NewMode, DB.clsExhibitor Exhibitor)
        {
            CurExhibitor = Exhibitor;
            CurrentMode = NewMode;
            switch (NewMode)
            {
                case EntryMode.NoRecord:
                    txtExhibitorNumber.Enabled = true;
                    txtNameFirst.Enabled = false;
                    txtNameLast.Enabled = false;
                    txtNameNick.Enabled = false;
                    txtAddress.Enabled = false;
                    txtState.Enabled = false;
                    txtZip.Enabled = false;
                    txtCity.Enabled = false;
                    cmdCommit.Enabled = false;
                    cmdCommit.Text = "Commit";
                    break;
                case EntryMode.ModifyRecord:
                    txtExhibitorNumber.Enabled = false;
                    txtNameFirst.Enabled = true;
                    txtNameLast.Enabled = true;
                    txtNameNick.Enabled = true;
                    txtAddress.Enabled = chkAddresses.Checked;
                    txtState.Enabled = chkAddresses.Checked;
                    txtZip.Enabled = chkAddresses.Checked;
                    txtCity.Enabled = chkAddresses.Checked;
                    cmdCommit.Enabled = true;
                    if (CurExhibitor == null)
                    {
                        cmdCommit.Text = "Commit new record";
                    }
                    else
                    {
                        cmdCommit.Text = "Commit modified record";
                    }
                    break;
                case EntryMode.ChangeID:
                    txtExhibitorNumber.Enabled = true;
                    txtNameFirst.Enabled = true;
                    txtNameLast.Enabled = true;
                    txtNameNick.Enabled = true;
                    txtAddress.Enabled = chkAddresses.Checked;
                    txtState.Enabled = chkAddresses.Checked;
                    txtZip.Enabled = chkAddresses.Checked;
                    txtCity.Enabled = chkAddresses.Checked;
                    cmdCommit.Enabled = true;
                    cmdCommit.Text = "Commit new record";
                    break;
            }
        }

        private void ErrorField(TextBox ErrorBox)
        {
            ErrorBox.BackColor = Color.Red;
            ErrorBox.ForeColor = Color.White;
        }

        private void ClearError(TextBox ErrorBox)
        {
            ErrorBox.BackColor = Color.White;
            ErrorBox.ForeColor = Color.Black;
        }

        private void ReloadGrid()
        {
            lsvExhibitors.Items.Clear();
            foreach (DB.clsExhibitor Exhibitor in clsDB.Exhibitors)
            {
                lsvExhibitors.Items.Add(Exhibitor);
            }
            if (lsvExhibitors.Items.Count > 0)
            {
                lsvExhibitors.Items[lsvExhibitors.Items.Count - 1].EnsureVisible();
            }
        }

        private void UpdateGrid(Dictionary<DB.AuctionData, DB.CommitAction> UpdatedItems)
        {
            foreach (DB.clsExhibitor Exhibitor in UpdatedItems.Keys)
            {
                //Check the filters against this item
                bool bFound = false;
                //Is the item already on the list?
                for (int i = 0; i < lsvExhibitors.Items.Count; i++)
                {
                    if ((DB.clsExhibitor)lsvExhibitors.Items[i] == Exhibitor)
                    {
                        //The item was found on the list, update or remove it?
                        if (UpdatedItems[Exhibitor] == DB.CommitAction.Modify && TestFilter(Exhibitor) == DB.CommitAction.Modify)
                        {
                            //Update it, noting that if it's sort position has
                            //  changed  (e.g., their first name was changed,
                            //  and the user has sorted by name) the sort
                            //  function on the list view will take care of
                            //  moving it.
                            clsDB.Exhibitors[Exhibitor].Selected = lsvExhibitors.Items[i].Selected;
                            lsvExhibitors.Items[i] = clsDB.Exhibitors[Exhibitor];
                        }
                        else
                        {
                            //Remove it
                            lsvExhibitors.Items[i].Remove();
                        }
                        bFound = true;
                        break;
                    }
                }
                //The item was not found on the list and needs to be added...
                if (!bFound && UpdatedItems[Exhibitor] == DB.CommitAction.Modify && TestFilter(Exhibitor) == DB.CommitAction.Modify)
                {
                    //Add it, noting that the sort function on the grid seems to
                    //  take care of getting it into the right place
                    lsvExhibitors.Items.Add(Exhibitor);
                        
                }
            }
        }

        //Apply the filter constraints to the items currently in the list
        private void Filter_Changed(object sender, EventArgs e)
        {
            Dictionary<DB.AuctionData, DB.CommitAction> dictFilterUpdate = new Dictionary<DB.AuctionData, DB.CommitAction>();

            //Loop through the current listview and see what comes off...
            foreach (DB.clsExhibitor Exhibitor in lsvExhibitors.Items)
            {
                if (TestFilter(Exhibitor) == DB.CommitAction.Delete)
                {
                    dictFilterUpdate.Add(Exhibitor, DB.CommitAction.Delete);
                }
            }

            //Loop through the current cache and see what matches...
            foreach (DB.clsExhibitor Exhibitor in clsDB.Exhibitors)
            {
                if (TestFilter(Exhibitor) == DB.CommitAction.Modify)
                {
                    dictFilterUpdate.Add(Exhibitor, DB.CommitAction.Modify);
                }
            }

            //Call the update grid event
            UpdateGrid(dictFilterUpdate);
        }

        private DB.CommitAction TestFilter(DB.clsExhibitor Exhibitor)
        {
            if ((tstxtFilterNumber.Text.Trim().Length <= 0 || Exhibitor.ExhibitorNumber.ToString().Contains(tstxtFilterNumber.Text.Trim())) &&                      //Exhibitor Number
                (tstxtFilterNameFirst.Text.Trim().Length <= 0 || Exhibitor.Name.First.ToLowerInvariant().Contains(tstxtFilterNameFirst.Text.ToLowerInvariant()) || Exhibitor.Name.Nick.ToLowerInvariant().Contains(tstxtFilterNameFirst.Text.ToLowerInvariant())) && //First or Nick Name
                (tstxtFilterNameLast.Text.Trim().Length <= 0 || Exhibitor.Name.Last.ToLowerInvariant().Contains(tstxtFilterNameLast.Text.ToLowerInvariant())) &&    //Last Name
                (!tscmdFilterHasExhibits.Checked || Exhibitor.ExhibitCount > 0)                                                                                     //Has Exhibits
               )
            {
                //Items match the filters
                return DB.CommitAction.Modify;
            }
            else
            {
                //Item does not match the filters
                return DB.CommitAction.Delete;
            }
        }

        private void LoadRecord(DB.clsExhibitor Ex)
        {
            SetMode(EntryMode.ModifyRecord, Ex);
            txtExhibitorNumber.Enabled = false;

            txtExhibitorNumber.Text = Ex.ExhibitorNumber.ToString();
            txtNameFirst.Text = Ex.Name.First;
            txtNameLast.Text = Ex.Name.Last;
            txtNameNick.Text = Ex.Name.Nick;
            txtAddress.Text = Ex.Address.Street;
            txtState.Text = Ex.Address.State;
            txtCity.Text = Ex.Address.City;
            if (Ex.Address.Zip > 0)
            {
                txtZip.Text = Ex.Address.Zip.ToString();
            }
            else
            {
                txtZip.Text = "";
            }
            if (Ex.Address.State.Length > 0 || Ex.Address.State.Length > 0 || Ex.Address.City.Length > 0 || Ex.Address.Zip > 0)
            {
                chkAddresses.Checked = true;
            }
            else
            {
                chkAddresses.Checked = false;
            }

            elvExhibits.Items.Clear();
            elvExhibits.Template = new EditableExhibitListViewItem(Ex);
            foreach (DB.clsExhibit Exhibit in Ex.Exhibits)
            {
                elvExhibits.Items.Add(new EditableExhibitListViewItem(Exhibit));
            }

            cmdCopy.Enabled = true;
            cmdDelete.Enabled = true;
        }

        private void ClearRecord()
        {
            SetMode(EntryMode.NoRecord, null);
            txtNameFirst.Text = "";
            txtNameLast.Text = "";
            txtNameNick.Text = "";
            txtAddress.Text = "";
            txtState.Text = "";
            txtZip.Text = "";
            txtCity.Text = "";
            ClearError(txtNameFirst);
            ClearError(txtNameLast);
            ClearError(txtNameNick);

            int iExNumber;
            if (int.TryParse(txtExhibitorNumber.Text, out iExNumber))
            {
                txtExhibitorNumber.Text = (iExNumber + 1).ToString();
            }
            else
            {
                txtExhibitorNumber.Text = "";
            }

            elvExhibits.Items.Clear();
            elvExhibits.Template = null;

            lsvExhibitors.SelectedItems.Clear();
            lsvExhibitors.Tag = null;

            cmdCopy.Enabled = false;
            cmdDelete.Enabled = false;
        }

        private void NewRecord()
        {
            int iExNumber = -1;
            SetMode(EntryMode.ModifyRecord, null);
            txtNameFirst.Text = "";
            txtNameLast.Text = "";
            txtAddress.Text = "";
            txtState.Text = "";
            txtZip.Text = "";
            txtCity.Text = "";

            cmdCopy.Enabled = false;
            cmdDelete.Enabled = false;

            elvExhibits.Items.Clear();
            if (int.TryParse(txtExhibitorNumber.Text, out iExNumber))
            {
                elvExhibits.Template = new EditableExhibitListViewItem(new DB.clsExhibitor(iExNumber, "", "", ""));

                foreach (DB.clsExhibit Exhibit in clsDB.Exhibits.FindByExhibitor(iExNumber))
                {
                    elvExhibits.Items.Add(new EditableExhibitListViewItem(Exhibit));
                }
            }
        }

        private void CopyRecord(DB.clsExhibitor Ex)
        {
            SetMode(EntryMode.ChangeID, null);
            txtExhibitorNumber.Enabled = true;
            txtExhibitorNumber.Text = "";
            txtNameFirst.Text = Ex.Name.First;
            txtNameLast.Text = Ex.Name.Last;
            txtAddress.Text = Ex.Address.Street;
            txtState.Text = Ex.Address.State;
            txtCity.Text = Ex.Address.City;
            txtZip.Text = Ex.Address.Zip.ToString();
        }

        private void CommitRecord()
        {
            List<DB.clsExhibit> lstExhibits = new List<DB.clsExhibit>();
            int iExNum;
            if (int.TryParse(txtExhibitorNumber.Text, out iExNum))
            {
                // Ensure the template record in the exhibits listing is hidden,
                //  or it will be an invalid record.
                elvExhibits.HideTemplate();


                //Build the exhibitor object
                DB.clsExhibitor Exhibitor = new DB.clsExhibitor(iExNum, txtNameFirst.Text, txtNameLast.Text, txtNameNick.Text);
                if (chkAddresses.Checked)
                {
                    int iZip = -1;
                    int.TryParse(txtZip.Text, out iZip);

                    Exhibitor.Address.Street = txtAddress.Text;
                    Exhibitor.Address.State = txtState.Text;
                    Exhibitor.Address.City = txtCity.Text;
                    Exhibitor.Address.Zip = iZip;
                }

                // Validate the exhibits list, and back it up before the
                //  exhibitor is comitted. Otherwise it would be wiped out by
                //  the update event.
                if (elvExhibits.Items.Count > 0)
                {
                    foreach (EditableExhibitListViewItem elviExhibit in elvExhibits.Items)
                    {
                        if (elviExhibit.Exhibit == null)
                        {
                            //The record is invalid
                            return;
                        }
                        else
                        {
                            //The record is valid, back it up.
                            lstExhibits.Add(elviExhibit.Exhibit);
                        }
                    }
                }

                clsDB.Exhibitors.Commit(DB.CommitAction.Modify, Exhibitor);

                if (Exhibitor.Exhibits.Count > 0 || lstExhibits.Count > 0)
                {
                    //Loop through the list of exhibits in the entry screen and update them in the database, though keep track of which ones were NOT modified
                    List<DB.clsExhibit> OriginalList = new List<DB.clsExhibit>(Exhibitor.Exhibits);
                    foreach (DB.clsExhibit Exhibit in lstExhibits)
                    {
                        for (int i = 0; i < OriginalList.Count; i++)
                        {
                            if (OriginalList[i].TagNumber == Exhibit.TagNumber &&
                                OriginalList[i].MarketItem == Exhibit.MarketItem)
                            {
                                OriginalList.RemoveAt(i);
                                break;
                            }
                        }
                        clsDB.Exhibits.Commit(DB.CommitAction.Modify, Exhibit);
                    }
                    System.Threading.Thread.Sleep(500);
                    //Delete any exhibits that were not modified in the database.
                    foreach (DB.clsExhibit Exhibit in OriginalList)
                    {
                        clsDB.Exhibits.Commit(DB.CommitAction.Delete, Exhibit);
                    }
                }

                ClearRecord();
            }
        }

        public void SetNameOrder(NameOrder NewOrder)
        {
            if (NewOrder != CurrentOrder)
            {
                Point ptLastNameTextPos = txtNameLast.Location;
                Point ptLastNameLblPos = lblNameLast.Location;
                int iLastNameTabOrder = txtNameLast.TabIndex;

                txtNameLast.Location = txtNameFirst.Location;
                lblNameLast.Location = lblNameFirst.Location;
                txtNameLast.TabIndex = txtNameFirst.TabIndex;

                txtNameFirst.Location = ptLastNameTextPos;
                lblNameFirst.Location = ptLastNameLblPos;
                txtNameFirst.TabIndex = iLastNameTabOrder;
            }

            CurrentOrder = NewOrder;
        }
    }

    public class EditableExhibitListViewItem : Helpers.EditableListViewItem
    {
        DB.clsExhibit m_dbExhibit = null;
        DB.clsExhibitor m_dbExhibitor = null;

        public EditableExhibitListViewItem(DB.clsExhibitor Exhibitor)
            : base("")
        {
            m_dbExhibitor = Exhibitor;
            RefreshColumns();

            ConstructorArgs = new object[] { Exhibitor };
        }

        public EditableExhibitListViewItem(DB.clsExhibit Exhibit)
            : base("")
        {
            m_dbExhibit = Exhibit;
            m_dbExhibitor = Exhibit.Exhibitor;
            RefreshColumns();
            RefreshExhibit(Exhibit);

            ConstructorArgs = new object[] { Exhibit };
        }

        private void RefreshColumns()
        {
            this.SubItems.Clear();
            base.SubItems.Add(new EditableTextBoxListViewSubItem("Market Item"));
            ((EditableTextBoxListViewSubItem)base.SubItems[(int)ucExhibitors.ExhibitEntryColumns.Market_Item]).TextBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            ((EditableTextBoxListViewSubItem)base.SubItems[(int)ucExhibitors.ExhibitEntryColumns.Market_Item]).TextBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
            ((EditableTextBoxListViewSubItem)base.SubItems[(int)ucExhibitors.ExhibitEntryColumns.Market_Item]).TextBox.AutoCompleteCustomSource = new AutoCompleteStringCollection();
            ((EditableTextBoxListViewSubItem)base.SubItems[(int)ucExhibitors.ExhibitEntryColumns.Market_Item]).TextBox.LostFocus += txtMarketType_LostFoucs;
            foreach (DB.clsMarketItem MarketItem in clsDB.Market)
            {
                ((EditableTextBoxListViewSubItem)base.SubItems[(int)ucExhibitors.ExhibitEntryColumns.Market_Item]).TextBox.AutoCompleteCustomSource.Add(MarketItem.MarketType);
            }

            base.SubItems.Add(new Helpers.EditableListViewItem.EditableTextBoxListViewSubItem("Tag Number"));

            base.SubItems.Add(new Helpers.EditableListViewItem.EditableTextBoxListViewSubItem("Champaion Status"));
            AutoCompleteStringCollection acsChampionSource = new AutoCompleteStringCollection();
            acsChampionSource.AddRange(DB.ChampionState.Values);
            ((EditableTextBoxListViewSubItem)base.SubItems[(int)ucExhibitors.ExhibitEntryColumns.Champion_Status]).TextBox.AutoCompleteCustomSource = acsChampionSource;
            ((EditableTextBoxListViewSubItem)base.SubItems[(int)ucExhibitors.ExhibitEntryColumns.Champion_Status]).TextBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            ((EditableTextBoxListViewSubItem)base.SubItems[(int)ucExhibitors.ExhibitEntryColumns.Champion_Status]).TextBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
            ((EditableTextBoxListViewSubItem)base.SubItems[(int)ucExhibitors.ExhibitEntryColumns.Champion_Status]).TextBox.LostFocus += txtChampionStatus_LostFocus;

            base.SubItems.Add(new Helpers.EditableListViewItem.EditableCheckBoxListViewSubItem("Rate of Gain"));

            base.SubItems.Add(new Helpers.EditableListViewItem.EditableTextBoxListViewSubItem("Weight"));
            base.SubItems.Add(new Helpers.EditableListViewItem.EditableRadioListViewSubItem("Take Back"));
            ((Helpers.EditableListViewItem.EditableRadioListViewSubItem)base.SubItems[(int)ucExhibitors.ExhibitEntryColumns.Take_Back]).Add("Not Set");
            ((Helpers.EditableListViewItem.EditableRadioListViewSubItem)base.SubItems[(int)ucExhibitors.ExhibitEntryColumns.Take_Back]).Add("No");
            ((Helpers.EditableListViewItem.EditableRadioListViewSubItem)base.SubItems[(int)ucExhibitors.ExhibitEntryColumns.Take_Back]).Add("Yes");
            base.SubItems.Add(new Helpers.EditableListViewItem.EditableCheckBoxListViewSubItem("Include"));
            base.SubItems.Add(new Helpers.EditableListViewItem.EditableButtonListViewSubItem("Remove"));
            ((EditableButtonListViewSubItem)base.SubItems[(int)ucExhibitors.ExhibitEntryColumns.Remove]).Click += EditableExhibitListViewItem_Click;
        }

        private void RefreshExhibit(DB.clsExhibit Exhibit)
        {
            if (Exhibit.MarketItem != null)
            {
                this.SubItems[(int)ucExhibitors.ExhibitEntryColumns.Market_Item].Value = Exhibit.MarketItem.MarketType;
            }
            this.SubItems[(int)ucExhibitors.ExhibitEntryColumns.Tag_Number].Value = Exhibit.TagNumber;
            this.SubItems[(int)ucExhibitors.ExhibitEntryColumns.Champion_Status].Value = Exhibit.ChampionStatus;
            this.SubItems[(int)ucExhibitors.ExhibitEntryColumns.Rate_Of_Gain].Value = Exhibit.RateOfGain;
            this.SubItems[(int)ucExhibitors.ExhibitEntryColumns.Weight].Value = Exhibit.Weight;
            this.SubItems[(int)ucExhibitors.ExhibitEntryColumns.Take_Back].Value = Exhibit.TakeBack;
            this.SubItems[(int)ucExhibitors.ExhibitEntryColumns.Include].Value = Exhibit.Include;
        }

        private void txtChampionStatus_LostFocus(object sender, EventArgs e)
        {
            TextBox txtChampionStatus = ((EditableTextBoxListViewSubItem)base.SubItems[(int)ucExhibitors.ExhibitEntryColumns.Champion_Status]).TextBox;
            DB.ChampionState State = txtChampionStatus.Text;
            if (State.Valid)
            {
                txtChampionStatus.Text = State;
            }
        }

        void txtMarketType_LostFoucs(object sender, EventArgs e)
        {
            TextBox txtMarketType = ((EditableTextBoxListViewSubItem)base.SubItems[(int)ucExhibitors.ExhibitEntryColumns.Market_Item]).TextBox;
            foreach (DB.clsMarketItem MarketItem in clsDB.Market)
            {
                if (txtMarketType.Text.ToLowerInvariant() == MarketItem.MarketType.ToLowerInvariant())
                {
                    txtMarketType.Text = MarketItem.MarketType;
                }
            }
        }

        void EditableExhibitListViewItem_Click(object sender, EventArgs e)
        {
            if (this.ListView != null)
            {
                this.ListView.Items.Remove(this);
            }
        }

        protected override void OnItemChanged(ItemEventArgs e)
        {
            base.OnItemChanged(e);

            if (this.Exhibit != null)
            {
                this.ImageIndex = 1;
            }
            else
            {
                this.ImageIndex = 0;
            }
        }

        public DB.clsExhibit Exhibit
        {
            get
            {
                DB.clsMarketItem dbMarket = clsDB.Market[((string)(base.SubItems[(int)ucExhibitors.ExhibitEntryColumns.Market_Item]).Value)];
                int iTagNumber = -1;
                int iWeight = -1;

                //Are any of the subitems null?
                foreach (EditableListViewSubItem subitem in base.SubItems)
                {
                    if (subitem.Value == null && base.SubItems.IndexOf(subitem) != (int)(ucExhibitors.ExhibitEntryColumns.Include) && base.SubItems.IndexOf(subitem) != (int)(ucExhibitors.ExhibitEntryColumns.Remove))
                    {
                        return null;
                    }
                }

                if (dbMarket != null &&
                    int.TryParse(((string)(base.SubItems[(int)ucExhibitors.ExhibitEntryColumns.Tag_Number]).Value), out iTagNumber) &&
                    ((DB.ChampionState)((string)base.SubItems[(int)ucExhibitors.ExhibitEntryColumns.Champion_Status].Value)).Valid &&
                    int.TryParse(((string)(base.SubItems[(int)ucExhibitors.ExhibitEntryColumns.Weight]).Value), out iWeight))
                {
                    m_dbExhibit = new DB.clsExhibit(iTagNumber, m_dbExhibitor != null ? m_dbExhibitor.ExhibitorNumber : m_dbExhibit.ExhibitorNumber, dbMarket.MarketID, (string)base.SubItems[(int)ucExhibitors.ExhibitEntryColumns.Champion_Status].Value, (base.SubItems[(int)ucExhibitors.ExhibitEntryColumns.Rate_Of_Gain].Value != null && (bool)base.SubItems[(int)ucExhibitors.ExhibitEntryColumns.Rate_Of_Gain].Value == true), iWeight, ((DB.NoYes)base.SubItems[(int)ucExhibitors.ExhibitEntryColumns.Take_Back].Value), (base.SubItems[(int)ucExhibitors.ExhibitEntryColumns.Include].Value != null && (bool)base.SubItems[(int)ucExhibitors.ExhibitEntryColumns.Include].Value == true), "");
                    return m_dbExhibit;
                }
                return null;
            }
        }
    }
}
