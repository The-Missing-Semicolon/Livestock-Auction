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
    public partial class ucBuyer : UserControl
    {
        ucBuyerCheckout BuyerCheckout = null;
        DB.clsBuyer CurBuyer;

        bool bCopyMode = false;

        NameOrder CurrentOrder = NameOrder.LastFirst;

        public ucBuyer()
        {
            InitializeComponent();

            lsvBuyers.ListViewItemSorter = new DB.clsBuyer.BuyerViewSorter();

            RefreshGrid();

            clsDB.Buyers.Updated += new EventHandler<DB.DatabaseUpdatedEventArgs>(Buyers_Updated);
        }

        void Buyers_Updated(object sender, DB.DatabaseUpdatedEventArgs e)
        {
            this.Invoke(new DB.UpdateInvoker(UpdateGrid), e.UpdatedItems);
        }

        private void lsvBuyers_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            ((DB.clsBuyer.BuyerViewSorter)lsvBuyers.ListViewItemSorter).SetSortColumn((DB.clsBuyer.BuyerColumns)e.Column);
            lsvBuyers.Sort();
        }

        private void lsvBuyers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lsvBuyers.SelectedItems.Count > 0)
            {
                DB.clsBuyer SelectedBuyer = (DB.clsBuyer)lsvBuyers.SelectedItems[0];
                LoadRecord(SelectedBuyer);

                if (BuyerCheckout != null)
                {
                    BuyerCheckout.LoadBuyer(SelectedBuyer);
                }

                tscmdToggleCheckout.Enabled = true;
                if (SelectedBuyer.CheckedOut)
                {
                    tscmdToggleCheckout.Text = "Unmark Checked Out";
                }
                else
                {
                    tscmdToggleCheckout.Text = "Mark Checked Out";
                }
            }
            else
            {
                tscmdToggleCheckout.Enabled = false;
            }
        }

        private void txtBuyerNumber_Leave(object sender, EventArgs e)
        {
            int iBuyerID = 0;
            if (int.TryParse(txtBuyerNumber.Text.Trim(), out iBuyerID))
            {
                DB.clsBuyer cBuyer = clsDB.Buyers[iBuyerID];
                if (cBuyer != null && !bCopyMode)
                {
                    //The user entered an existing buyer number, but is not trying to copy a record
                    LoadRecord(cBuyer);

                    if (CurrentOrder == NameOrder.LastFirst)
                    {
                        txtNameLast.Focus();
                    }
                    else
                    {
                        txtNameFirst.Focus();
                    }
                }
                else if (cBuyer != null && bCopyMode)
                {
                    //The user entered an existing buyer number, but has selected to copy a record
                    MessageBox.Show("This ID already exists");
                    txtBuyerNumber.Focus();
                }
                else
                {
                    //The user entered a non-existant buyer number
                    if (!bCopyMode)
                    {
                        txtBuyerNumber.Enabled = false;
                    }
                    CurBuyer = null;
                    txtNameFirst.Enabled = true;
                    txtNameLast.Enabled = true;
                    txtCompanyName.Enabled = true;
                    txtAddress.Enabled = true;
                    txtCity.Enabled = true;
                    txtState.Enabled = true;
                    txtZip.Enabled = true;
                    txtPhoneNumber.Enabled = true;

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

        private void cmdCommit_Click(object sender, EventArgs e)
        {
            int iZipCode = 0;
            int iBuyerNumber = 0;

            if (int.TryParse(txtBuyerNumber.Text, out iBuyerNumber))
            {
                int.TryParse(txtZip.Text, out iZipCode);

                DB.clsBuyer NewBuyer = new DB.clsBuyer(int.Parse(txtBuyerNumber.Text), txtNameFirst.Text, txtNameLast.Text, txtCompanyName.Text, txtAddress.Text, txtCity.Text, txtState.Text, iZipCode, txtPhoneNumber.Text);
                clsDB.Buyers.Commit(DB.CommitAction.Modify, NewBuyer);

                NewRecord();
            }
            txtBuyerNumber.Focus();
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            if (CurBuyer != null)
            {
                if (MessageBox.Show("Are you sure you want to delete Buyer #: " + CurBuyer.BuyerNumber + ", " + CurBuyer.Name.First + " " + CurBuyer.Name.Last, "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    clsDB.Buyers.Commit(DB.CommitAction.Delete, CurBuyer);
                    NewRecord();
                }
            }
            else
            {
                MessageBox.Show("No record is selected", "Delete Record", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void cmdCopy_Click(object sender, EventArgs e)
        {
            bCopyMode = true;
            txtBuyerNumber.Enabled = true;
        }

        private void txtState_Leave(object sender, EventArgs e)
        {
            txtState.Text = txtState.Text.ToUpperInvariant();
            if (txtState.Text.Trim().Length > 0)
            {
                txtCity.AutoCompleteCustomSource.Clear();

                string[] asCity = GeoDatabase.FindCityByState(txtState.Text);
                if (asCity != null)
                {
                    txtCity.AutoCompleteCustomSource.AddRange(asCity);
                }
            }
        }

        private void txtCity_Leave(object sender, EventArgs e)
        {
            if (txtCity.Text.Trim().Length > 0)
            {
                //Make sure the city entered is in the same case as it appears in the database
                foreach (string City in txtCity.AutoCompleteCustomSource)
                {
                    if (City.ToUpperInvariant() == txtCity.Text.Trim().ToUpperInvariant())
                    {
                        txtCity.Text = City;
                        break;
                    }
                }
                string[] sZipCodes = GeoDatabase.FindZipByStateCity(txtState.Text, txtCity.Text);
                txtZip.AutoCompleteCustomSource.Clear();


                if (sZipCodes.Length == 1)
                {
                    txtZip.Text = sZipCodes[0];
                }
                else if (sZipCodes.Length > 1)
                {
                    txtZip.AutoCompleteCustomSource.AddRange(sZipCodes);
                }
            }
        }

        private void txtZip_Leave(object sender, EventArgs e)
        {
            if (txtZip.Text.Trim().Length == 5)
            {
                int iZipCode = 0;
                if (int.TryParse(txtZip.Text.Trim(), out iZipCode))
                {
                    if (txtCity.Text.Trim().Length == 0)
                    {
                        txtCity.Text = GeoDatabase.FindCityByZip(iZipCode);
                    }

                    if (txtState.Text.Trim().Length == 0)
                    {
                        txtState.Text = GeoDatabase.FindStateByZip(iZipCode);
                    }
                }
            }
        }

        private void cmdNew_Click(object sender, EventArgs e)
        {
            NewRecord();
        }

        private void RefreshGrid()
        {
            lsvBuyers.Items.Clear();
            foreach (DB.clsBuyer Buyer in clsDB.Buyers)
            {
                lsvBuyers.Items.Add(Buyer);
            }
            if (lsvBuyers.Items.Count > 0)
            {
                lsvBuyers.Items[lsvBuyers.Items.Count - 1].EnsureVisible();
            }
        }

        private void UpdateGrid(Dictionary<DB.AuctionData, DB.CommitAction> UpdatedItems)
        {
            foreach (DB.clsBuyer Buyer in UpdatedItems.Keys)
            {
                //Check the filters against this item
                bool bFound = false;
                //Is the item already on the list?
                for (int i = 0; i < lsvBuyers.Items.Count; i++)
                {
                    if ((DB.clsBuyer)lsvBuyers.Items[i] == Buyer)
                    {
                        //The item was found on the list, update or remove it?
                        if (UpdatedItems[Buyer] == DB.CommitAction.Modify && TestFilter(Buyer) == DB.CommitAction.Modify)
                        {
                            //Update it, noting that if it's sort position has
                            //  changed  (e.g., their first name was changed,
                            //  and the user has sorted by name) the sort
                            //  function on the list view will take care of
                            //  moving it.
                            clsDB.Buyers[Buyer].Selected = lsvBuyers.Items[i].Selected;
                            lsvBuyers.Items[i] = clsDB.Buyers[Buyer];
                        }
                        else
                        {
                            //Remove it
                            lsvBuyers.Items[i].Remove();
                        }
                        bFound = true;
                        break;
                    }
                }
                //The item was not found on the list and needs to be added...
                if (!bFound && UpdatedItems[Buyer] == DB.CommitAction.Modify && TestFilter(Buyer) == DB.CommitAction.Modify)
                {
                    //Add it, noting that the sort function on the grid seems to
                    //  take care of getting it into the right place
                    lsvBuyers.Items.Add(clsDB.Buyers[Buyer]);
                }
            }
        }

        //Apply the filter constraints to the items currently in the list
        private void Filter_Changed(object sender, EventArgs e)
        {
            Dictionary<DB.AuctionData, DB.CommitAction> dictFilterUpdate = new Dictionary<DB.AuctionData, DB.CommitAction>();

            //Loop through the current listview and see what comes off...
            foreach (DB.clsBuyer Buyer in lsvBuyers.Items)
            {
                if (TestFilter(Buyer) == DB.CommitAction.Delete)
                {
                    dictFilterUpdate.Add(Buyer, DB.CommitAction.Delete);
                }
            }

            //Loop through the current cache and see what matches...
            foreach (DB.clsBuyer Buyer in clsDB.Buyers)
            {
                if (TestFilter(Buyer) == DB.CommitAction.Modify)
                {
                    dictFilterUpdate.Add(Buyer, DB.CommitAction.Modify);
                }
            }

            //Call the update grid event
            UpdateGrid(dictFilterUpdate);
        }

        private DB.CommitAction TestFilter(DB.clsBuyer Buyer)
        {
            if ((tstxtFilterNumber.Text.Trim().Length <= 0 || Buyer.BuyerNumber.ToString().Contains(tstxtFilterNumber.Text.Trim())) &&                                      //Buyer Number
                (tstxtFilterNameFirst.Text.Trim().Length <= 0 || Buyer.Name.First.ToLowerInvariant().Contains(tstxtFilterNameFirst.Text.Trim().ToLowerInvariant())) &&      //First Name
                (tstxtFilterNameLast.Text.Trim().Length <= 0 || Buyer.Name.Last.ToLowerInvariant().Contains(tstxtFilterNameLast.Text.Trim().ToLowerInvariant())) &&         //Last Name
                (tstxtFilterCompanyName.Text.Trim().Length <= 0 || Buyer.CompanyName.ToLowerInvariant().Contains(tstxtFilterCompanyName.Text.Trim().ToLowerInvariant())) && //Company Name
                (!tscmdFilterHasPurchases.Checked || Buyer.PurchaseCount > 0) &&                                                                                            //Has Purchases
                (!tscmdFilterCheckedOut.Checked || (tscmdFilterCheckedOut.CheckState == CheckState.Indeterminate && Buyer.CheckedOut) || (tscmdFilterCheckedOut.CheckState == CheckState.Checked && !Buyer.CheckedOut))//Has Checked Out
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

        private void NewRecord()
        {
            bCopyMode = false;
            CurBuyer = null;
            txtBuyerNumber.Enabled = true;
            txtNameFirst.Enabled = false;
            txtNameLast.Enabled = false;
            txtCompanyName.Enabled = false;
            txtAddress.Enabled = false;
            txtCity.Enabled = false;
            txtState.Enabled = false;
            txtZip.Enabled = false;
            txtPhoneNumber.Enabled = false;

            txtBuyerNumber.Text = "";
            txtNameFirst.Text = "";
            txtNameLast.Text = "";
            txtCompanyName.Text = "";
            txtAddress.Text = "";
            txtCity.Text = "";
            txtState.Text = "";
            txtZip.Text = "";
            txtPhoneNumber.Text = "";
        }

        private void LoadRecord(DB.clsBuyer Buyer)
        {
            bCopyMode = false;
            //clsBuyer Buyer = clsDB.Buyers.Items[BuyerID];

            txtBuyerNumber.Text = Buyer.BuyerNumber.ToString();
            txtNameFirst.Text = Buyer.Name.First;
            txtNameLast.Text = Buyer.Name.Last;
            txtCompanyName.Text = Buyer.CompanyName;
            txtAddress.Text = Buyer.Address.Street;
            txtCity.Text = Buyer.Address.City;
            txtState.Text = Buyer.Address.State;
            txtZip.Text = Buyer.Address.Zip.ToString();
            txtPhoneNumber.Text = Buyer.PhoneNumber;

            txtBuyerNumber.Enabled = false;
            txtNameFirst.Enabled = true;
            txtNameLast.Enabled = true;
            txtCompanyName.Enabled = true;
            txtAddress.Enabled = true;
            txtCity.Enabled = true;
            txtState.Enabled = true;
            txtZip.Enabled = true;
            txtPhoneNumber.Enabled = true;

            CurBuyer = Buyer;
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

        private void txtNameLast_Enter(object sender, EventArgs e)
        {
            //Populate the autocomplete for the last name based on the buyer history data
            txtNameLast.AutoCompleteCustomSource.Clear();
            if (txtNameFirst.Text.Length > 0)
            {
                string[] sNames = clsDB.BuyerHistory_LastNames(txtNameFirst.Text.Trim());
                if (sNames.Length == 1)
                {
                    txtNameLast.Text = sNames[0];
                }
                else
                {
                    txtNameLast.AutoCompleteCustomSource.AddRange(sNames);
                }
            }
            else
            {
                txtNameLast.AutoCompleteCustomSource.AddRange(clsDB.BuyerHistory_LastNames());
            }
        }

        private void txtNameFirst_Enter(object sender, EventArgs e)
        {
            //Populate the autocomplete for the first name based on the buyer history data
            txtNameFirst.AutoCompleteCustomSource.Clear();
            if (txtNameLast.Text.Length > 0)
            {
                string[] sNames = clsDB.BuyerHistory_FirstNames(txtNameLast.Text.Trim());

                if (sNames.Length == 1)
                {
                    txtNameFirst.Text = sNames[0];
                }
                else
                {
                    txtNameFirst.AutoCompleteCustomSource.AddRange(sNames);
                }
            }
            else
            {
                txtNameFirst.AutoCompleteCustomSource.AddRange(clsDB.BuyerHistory_FirstNames());
            }
        }

        private void txtCompanyName_Enter(object sender, EventArgs e)
        {
            int iBuyerNumber = 0;
            if (txtNameFirst.Text.Length > 0 && txtNameLast.Text.Length > 0 && int.TryParse(txtBuyerNumber.Text, out iBuyerNumber))
            {
                DB.clsBuyer HistoricalBuyer = clsDB.FindBuyer(iBuyerNumber, txtNameFirst.Text.Trim(), txtNameLast.Text.Trim());
                if (HistoricalBuyer != null)
                {
                    txtCompanyName.Text = HistoricalBuyer.CompanyName;
                    txtAddress.Text = HistoricalBuyer.Address.Street;
                    txtCity.Text = HistoricalBuyer.Address.City;
                    txtState.Text = HistoricalBuyer.Address.State;
                    txtZip.Text = HistoricalBuyer.Address.Zip.ToString("00000");
                    txtPhoneNumber.Text = HistoricalBuyer.PhoneNumber;
                }
            }
        }

        private void tscmdToggleCheckout_Click(object sender, EventArgs e)
        {
            if (lsvBuyers.SelectedItems.Count > 0)
            {
                DB.clsBuyer SelectedBuyer = (DB.clsBuyer)lsvBuyers.SelectedItems[0];
                SelectedBuyer.CheckedOut = !SelectedBuyer.CheckedOut;
                clsDB.Buyers.Commit(DB.CommitAction.Modify, SelectedBuyer);
            }
        }

        private void tscmdFilterCheckedOut_Click(object sender, EventArgs e)
        {
            if (tscmdFilterCheckedOut.CheckState == CheckState.Unchecked)
            {
                tscmdFilterCheckedOut.CheckState = CheckState.Indeterminate;
                tscmdFilterCheckedOut.Text = "Has Checked Out";
            }
            else if (tscmdFilterCheckedOut.CheckState == CheckState.Indeterminate)
            {
                tscmdFilterCheckedOut.CheckState = CheckState.Checked;
                tscmdFilterCheckedOut.Text = "Has Not Checked Out";
            }
            else if (tscmdFilterCheckedOut.CheckState == CheckState.Checked)
            {
                tscmdFilterCheckedOut.CheckState = CheckState.Unchecked;
                tscmdFilterCheckedOut.Text = "Checked Out";
            }
        }

        private void tscmdFilterHasPurchases_Click(object sender, EventArgs e)
        {
            if (tscmdFilterHasPurchases.CheckState == CheckState.Unchecked)
            {
                tscmdFilterHasPurchases.CheckState = CheckState.Checked;
                tscmdFilterHasPurchases.Text = "Has Purchases";
            }
            else if (tscmdFilterHasPurchases.CheckState == CheckState.Checked)
            {
                tscmdFilterHasPurchases.CheckState = CheckState.Unchecked;
                tscmdFilterHasPurchases.Text = "Purchases";
            }
        }
    }
}
