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
    public partial class ucExhibits : UserControl
    {
        DB.clsExhibit SelectedItem;

        public ucExhibits()
        {
            InitializeComponent();

            lsvItems.ListViewItemSorter = new DB.clsExhibit.ExhibitViewSorter();
        }

        private void ucExhibits_Load(object sender, EventArgs e)
        {
            ResetAutocomplete();
            ReloadGrid();

            clsDB.Exhibits.Updated += new EventHandler<DB.DatabaseUpdatedEventArgs>(Exhibits_Updated);
        }

        void Exhibits_Updated(object sender, DB.DatabaseUpdatedEventArgs e)
        {
            this.Invoke(new DB.UpdateInvoker(UpdateGrid), e.UpdatedItems);
        }

        private void lsvItems_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            ((DB.clsExhibit.ExhibitViewSorter)lsvItems.ListViewItemSorter).SetSortColumn((DB.clsExhibit.ExhibitColumns)e.Column);
            lsvItems.Sort();
        }

        private void txtChampion_Leave(object sender, EventArgs e)
        {
            //TODO: Test this and make sure it works like the original
            DB.ChampionState state = txtChampion.Text;
            if (state.Valid)
            {
                txtChampion.Text = state;
                txtChampion.Tag = state;
            }
        }

        private void cmdCommit_Click(object sender, EventArgs e)
        {
            CommitRecord();
        }

        private void lsvItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            DateTime initTime = DateTime.Now;
            if (lsvItems.SelectedItems.Count > 0)
            {
                DB.clsExhibit Item = clsDB.Exhibits[(DB.clsExhibit)lsvItems.SelectedItems[0]];
                ModifyRecord(Item);

                tscmdToggleInclude.Enabled = true;
                if (((DB.clsExhibit)lsvItems.SelectedItems[0]).Include)
                {
                    tscmdToggleInclude.Text = "Exclude Exhibit";
                }
                else
                {
                    tscmdToggleInclude.Text = "Include Exhibit";
                }
            }
            else
            {
                CreateNewRecord();
                tscmdToggleInclude.Enabled = false;
            }
            System.Diagnostics.Debug.WriteLine("Record Selection: " + (DateTime.Now - initTime).Milliseconds);
        }

        private void cmdNewRecord_Click(object sender, EventArgs e)
        {
            CreateNewRecord();
        }

        private void cmdDeleteRecord_Click(object sender, EventArgs e)
        {
            if (SelectedItem != null)
            {
                clsDB.Exhibits.Commit(DB.CommitAction.Delete, clsDB.Exhibits[SelectedItem]);

                CreateNewRecord();
                ReloadGrid();
            }
        }

        private void cmdClear_Click(object sender, EventArgs e)
        {
            CreateNewRecord();
        }

        //Apply the filter constraints to the items currently in the list
        private void Filter_Changed(object sender, EventArgs e)
        {
            Dictionary<DB.AuctionData, DB.CommitAction> dictFilterUpdate = new Dictionary<DB.AuctionData, DB.CommitAction>();

            //Loop through the current listview and see what comes off...
            foreach (DB.clsExhibit Exhibit in lsvItems.Items)
            {
                if (TestFilter(Exhibit) == DB.CommitAction.Delete)
                {
                    dictFilterUpdate.Add(Exhibit, DB.CommitAction.Delete);
                }
            }

            //Loop through the current cache and see what matches...
            foreach (DB.clsExhibit Exhibit in clsDB.Exhibits)
            {
                if (TestFilter(Exhibit) == DB.CommitAction.Modify)
                {
                    dictFilterUpdate.Add(Exhibit, DB.CommitAction.Modify);
                }
            }

            //Call the update grid event
            UpdateGrid(dictFilterUpdate);
        }

        private DB.CommitAction TestFilter(DB.clsExhibit Exhibit)
        {
            if ((tstxtFilterTagNumber.Text.Trim().Length <= 0 || Exhibit.TagNumber.ToString().Contains(tstxtFilterTagNumber.Text.Trim())) &&                                      
                (tstxtFilterExhibitor.Text.Trim().Length <= 0 || Exhibit.Exhibitor.Name.ToString().ToLowerInvariant().Contains(tstxtFilterExhibitor.Text.Trim().ToLowerInvariant())) &&
                (tstxtFilterItem.Text.Trim().Length <= 0 || Exhibit.MarketItem.MarketType.ToLowerInvariant().Contains(tstxtFilterItem.Text.Trim().ToLowerInvariant())) &&
                (tstxtFilterChampion.Text.Trim().Length <= 0 || ((string)Exhibit.ChampionStatus).ToLowerInvariant().Contains(tstxtFilterChampion.Text.Trim().ToLowerInvariant())) &&
                (!tscmdFilterIncluded.Checked || Exhibit.Include)
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

        public void CreateNewRecord()
        {
            ExhibitorEntry.Clear();
            MarketItemEntry.Clear();

            txtTagNumber.Text = "";
            chkRateOfGain.Checked = false;
            txtChampion.Text = "";
            txtWeight.Text = "";
            radTakeBackYes.Checked = false;
            radTakeBackNo.Checked = false;

            cmdDeleteRecord.Enabled = false;
            SelectedItem = null;
            cmdCommit.Text = "Commit New Record";
            ResetAutocomplete();
        }

        public void ModifyRecord(DB.clsExhibit Item)
        {
            if (Item.Exhibitor != null)
            {
                ExhibitorEntry.Exhibitor = Item.Exhibitor;
            }
            else
            {
                ExhibitorEntry.ExhibitorNumber = Item.ExhibitorNumber;
            }
            
            if (Item.MarketItem != null)
            {
                MarketItemEntry.MarketItem = Item.MarketItem;
            }
            else
            {
                MarketItemEntry.MarketID = Item.MarketID;
            }

            txtTagNumber.Text = Item.TagNumber.ToString();

            txtChampion.Text = Item.ChampionStatus;
            
            txtChampion.Tag = Item.ChampionStatus;
            chkRateOfGain.Checked = Item.RateOfGain;
            txtWeight.Text = Item.Weight.ToString();

            if (Item.TakeBack == DB.NoYes.Yes)
            {
                radTakeBackYes.Checked = true;
            }
            else if (Item.TakeBack == DB.NoYes.No)
            {
                radTakeBackNo.Checked = true;
            }
            else
            {
                radTakeBackYes.Checked = false;
                radTakeBackNo.Checked = false;
            }

            cmdDeleteRecord.Enabled = true;

            cmdCommit.Text = "Commit Modified Record";

            SelectedItem = Item;
        }

        public void CommitRecord()
        {
            DB.clsExhibit Ex = null;
            int iTagNumber = 0;

            int iOldTagNumber = -1; //Used to identify record to remove when changing tag numbers
            int iOldMarketItem = -1;

            int iWeight = 0;

            if (!int.TryParse(txtTagNumber.Text, out iTagNumber))
            {
                MessageBox.Show(string.Format("'{0}' is not a valid tag number", txtTagNumber.Text), "Invalid tag number", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTagNumber.Focus();
            }
            else if (ExhibitorEntry.Exhibitor == null)
            {
                MessageBox.Show("Please select an exhibitor", "Exhibitor", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ExhibitorEntry.Focus();
            }
            else if (MarketItemEntry.MarketItem == null)
            {
                MessageBox.Show("Please select a market item", "Market Item", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                MarketItemEntry.Focus();
            }
            else if (!int.TryParse(txtWeight.Text, out iWeight))
            {
                MessageBox.Show(string.Format("'{0}' is not a valid weight", txtWeight.Text), "Invalid weight", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtWeight.Focus();
            }
            else
            {
                //Determine the value of "TakeBack" dependent on the values of radTakeBackYes and radTakeBackNo
                DB.NoYes eTakeBack;
                if (radTakeBackYes.Checked)
                {
                    eTakeBack = DB.NoYes.Yes;
                }
                else if (radTakeBackNo.Checked)
                {
                    eTakeBack = DB.NoYes.No;
                }
                else
                {
                    eTakeBack = DB.NoYes.NotSet;
                }

                if (SelectedItem == null)
                {
                    Ex = new DB.clsExhibit(iTagNumber, ExhibitorEntry.Exhibitor.ExhibitorNumber, MarketItemEntry.MarketItem.MarketID, (DB.ChampionState)txtChampion.Tag, chkRateOfGain.Checked, int.Parse(txtWeight.Text), eTakeBack, true, "");
                }
                else
                {
                    if (SelectedItem.TagNumber != iTagNumber)
                    {
                        if (MessageBox.Show(string.Format("Change tag number from {0} to {1}?", SelectedItem.TagNumber, iTagNumber), "Change Key", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        {
                            iOldTagNumber = SelectedItem.TagNumber;
                            iOldMarketItem = SelectedItem.MarketItem.MarketID;
                            Ex = new DB.clsExhibit(iTagNumber, ExhibitorEntry.Exhibitor.ExhibitorNumber, MarketItemEntry.MarketItem.MarketID, (DB.ChampionState)txtChampion.Tag, chkRateOfGain.Checked, int.Parse(txtWeight.Text), eTakeBack, true, "");
                        }
                    }
                    else
                    {
                        Ex = SelectedItem;
                        Ex.Exhibitor = ExhibitorEntry.Exhibitor;
                        Ex.MarketItem = MarketItemEntry.MarketItem;
                        Ex.RateOfGain = chkRateOfGain.Checked;
                        Ex.ChampionStatus = (DB.ChampionState)txtChampion.Tag;
                        Ex.Weight = int.Parse(txtWeight.Text);
                        Ex.TakeBack = eTakeBack;
                    }
                }

                if (clsDB.Exhibitors[ExhibitorEntry.Exhibitor.ExhibitorNumber] != null)
                {
                    clsDB.Exhibitors.Commit(DB.CommitAction.Modify, ExhibitorEntry.Exhibitor);
                }
                if (clsDB.Market[MarketItemEntry.MarketItem.MarketID] == null)
                {
                    clsDB.Market.Commit(DB.CommitAction.Modify, MarketItemEntry.MarketItem);
                }

                clsDB.Exhibits.Commit(DB.CommitAction.Modify, Ex);
                if (iOldTagNumber > 0)
                {
                    clsDB.Exhibits.Commit(DB.CommitAction.Delete, clsDB.Exhibits[iOldTagNumber, iOldMarketItem]);
                }

                ResetAutocomplete();
                ExhibitorEntry.Focus();
                CreateNewRecord();
            }
        }


        public void SelectExhibitor(DB.clsExhibitor Ex)
        {
            cmdShowAll.Enabled = true;

            ExhibitorEntry.Exhibitor = Ex;

            ReloadGrid();
        }


        private void ReloadGrid()
        {
            lsvItems.Items.Clear();
            foreach (DB.clsExhibit Item in clsDB.Exhibits)
            {
                lsvItems.Items.Add(Item);
            }
            if (lsvItems.Items.Count > 0)
            {
                lsvItems.Items[lsvItems.Items.Count - 1].EnsureVisible();
            }
        }

        private void UpdateGrid(Dictionary<DB.AuctionData, DB.CommitAction> UpdatedItems)
        {
            foreach (DB.clsExhibit Item in UpdatedItems.Keys)
            {
                bool bFound = false;
                for (int i = 0; i < lsvItems.Items.Count; i++)
                {
                    if ((DB.clsExhibit)lsvItems.Items[i] == Item)
                    {
                        if (UpdatedItems[Item] == DB.CommitAction.Modify && TestFilter(Item) == DB.CommitAction.Modify)
                        {
                            //Update it, noting that if it's sort position has
                            //  changed  (e.g., their first name was changed,
                            //  and the user has sorted by name) the sort
                            //  function on the list view will take care of
                            //  moving it.
                            clsDB.Exhibits[Item].Selected = lsvItems.Items[i].Selected;
                            lsvItems.Items[i] = clsDB.Exhibits[Item];
                        }
                        else
                        {
                            lsvItems.Items[i].Remove();
                        }
                        bFound = true;
                        break;
                    }
                }
                if (!bFound && UpdatedItems[Item] == DB.CommitAction.Modify && TestFilter(Item) == DB.CommitAction.Modify)
                {
                    lsvItems.Items.Add(Item);
                }
            }
        }

        private void ResetAutocomplete()
        {
            ExhibitorEntry.ResetAutoComplete();
            MarketItemEntry.ResetAutoComplete();

            txtChampion.AutoCompleteCustomSource.Clear();
            txtChampion.AutoCompleteCustomSource.AddRange(DB.ChampionState.Values);
        }

        public void SetNameOrder(NameOrder NewOrder)
        {
            ExhibitorEntry.SetNameOrder(NewOrder);
        }

        private void ExhibitorEntry_Leave(object sender, EventArgs e)
        {
            MarketItemEntry.Focus();
        }

        private void tscmdToggleInclude_Click(object sender, EventArgs e)
        {
            if (lsvItems.SelectedItems.Count > 0)
            {
                DB.clsExhibit SelectedItem = (DB.clsExhibit)lsvItems.SelectedItems[0];
                SelectedItem.Include = !SelectedItem.Include;
                clsDB.Exhibits.Commit(DB.CommitAction.Modify, SelectedItem);
            }
        }
    }
}
