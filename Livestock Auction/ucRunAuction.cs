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
    public partial class ucRunAuction : UserControl
    {
        public enum PurchaseEntryColumns
        {
            Bid = 0,
            Buyer_Number = 1,
            Buyer_Name = 2,
            Total_Bid= 3,
            Remove = 4,
            Add = 5
        }

        int m_iCurAutionIndex = 0;

        Dictionary<int, List<EditablePurchaseListViewItem>> m_dictSavePurchases;

        public ucRunAuction()
        {
            InitializeComponent();

            m_dictSavePurchases = new Dictionary<int, List<EditablePurchaseListViewItem>>();

            elvPurchases.Columns.Add("Bid");            //Text Box W/ Numeric Mask
            elvPurchases.Columns.Add("Buyer #");        //Text Box W/ Numeric Mask
            elvPurchases.Columns.Add("Buyer");          //Fixed text
            elvPurchases.Columns.Add("Total");          //Fixed text
            elvPurchases.Columns.Add("Remove");         //Button
            elvPurchases.Columns.Add("Add Purchase");   //Button

            elvPurchases.Columns[(int)PurchaseEntryColumns.Bid].Width = 75;
            elvPurchases.Columns[(int)PurchaseEntryColumns.Buyer_Number].Width = 75;
            elvPurchases.Columns[(int)PurchaseEntryColumns.Buyer_Name].Width = 175;
            elvPurchases.Columns[(int)PurchaseEntryColumns.Total_Bid].Width = 75;
            elvPurchases.Columns[(int)PurchaseEntryColumns.Remove].Width = 100;
            elvPurchases.Columns[(int)PurchaseEntryColumns.Add].Width = 100;


            lsvAuctionOrder.ListViewItemSorter = new DB.clsAuctionIndex.AuctionIndexSorter();

            elvPurchases.Items.ItemAdded += Items_ItemAdded;
            elvPurchases.Items.ItemRemoved += Items_ItemRemoved;

            ReloadGrid();

            clsDB.Auction.Updated += new EventHandler<DB.DatabaseUpdatedEventArgs>(AuctionOrder_Updated);
        }

        void AuctionOrder_Updated(object sender, DB.DatabaseUpdatedEventArgs e)
        {
            this.Invoke(new DB.UpdateInvoker(UpdateGrid_AuctionOrder), e.UpdatedItems);
        }

        private void lsvAuctionOrder_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            ((DB.clsAuctionIndex.AuctionIndexSorter)lsvAuctionOrder.ListViewItemSorter).SetSortColumn((DB.clsAuctionIndex.AuctionIndexColumns)e.Column);
            lsvAuctionOrder.Sort();
        }

        private void lsvAuctionOrder_MouseUp(object sender, MouseEventArgs e)
        {
            ListViewItem lviSelected = lsvAuctionOrder.GetItemAt(e.X, e.Y);
            if (lviSelected != null)
            {
                int iCurIndex = int.Parse(lviSelected.Text);
                if (iCurIndex != m_iCurAutionIndex)
                {
                    LoadAuctionEntry(iCurIndex);
                }
            }
        }

        //Save the current entry and advance ot the next one
        private void cmdNext_Click(object sender, EventArgs e)
        {
            //Save the purchases assocaited with the current entry
            DB.clsAuctionIndex CurAuctionEntry = clsDB.Auction.Items[m_iCurAutionIndex];

            //Check all of the purchases to make sure they are valid
            List<DB.clsPurchase> NewPurchases = new List<DB.clsPurchase>();
            Dictionary<int, DB.clsPurchase> RemovedPurchases = null;
            if (CurAuctionEntry.Exhibit.Purchases != null)
            {
                // Initalize the list of purchases being removed to all of the
                //  purchases, items will be removed from this list as they are
                //  accounted for while looping over the items in the list view.
                RemovedPurchases = new Dictionary<int, DB.clsPurchase>(CurAuctionEntry.Exhibit.Purchases);
            }
            else
            {
                RemovedPurchases = new Dictionary<int, DB.clsPurchase>();
            }
            foreach (EditablePurchaseListViewItem PurchaseItem in elvPurchases.Items)
            {
                if (PurchaseItem.Purchase == null && (PurchaseItem.SubItems[0].Value != null || PurchaseItem.SubItems[1].Value != null))
                {
                    MessageBox.Show("Enter buyer number and bid ammount for all purchases before continuing.", "Incomplete Purchase", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    NewPurchases.Clear();
                    return;
                }
                else if (PurchaseItem.Purchase != null && PurchaseItem.PurchaseModified)
                {
                    //Warn if the buyer has already checked out
                    if (PurchaseItem.Purchase.Buyer != null && PurchaseItem.Purchase.Buyer.CheckedOut)
                    {
                        if (MessageBox.Show(string.Format("Buyer #{0} has already checked out. They will be marked as not checked out. Continue?", PurchaseItem.Purchase.BuyerID), "Buyer checked out", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                        {
                            NewPurchases.Clear();
                            PurchaseItem.SubItems[(int)ucRunAuction.PurchaseEntryColumns.Buyer_Number].Focus();
                            return;
                        }
                    }

                    NewPurchases.Add(PurchaseItem.Purchase);
                }

                //Keep track of what purchases were removed
                if (PurchaseItem.Purchase != null)
                {
                    RemovedPurchases.Remove(PurchaseItem.Purchase.PurchaseIndex);
                }
            }

            //Commit all of the removed purchases
            foreach (DB.clsPurchase OldPurchase in RemovedPurchases.Values)
            {
                clsDB.Purchases.Commit(DB.CommitAction.Delete, OldPurchase);
            }

            //Commit all of the new purchases
            foreach (DB.clsPurchase NewPurchase in NewPurchases)
            {
                clsDB.Purchases.Commit(DB.CommitAction.Modify, NewPurchase);
                if (NewPurchase.Buyer != null && NewPurchase.Buyer.CheckedOut)
                {
                    NewPurchase.Buyer.CheckedOut = false;
                    clsDB.Buyers.Commit(DB.CommitAction.Modify, NewPurchase.Buyer);
                }
            }

            //Clear out the g_dictSavePurchases entry for this element
            if (m_dictSavePurchases.ContainsKey(m_iCurAutionIndex))
            {
                m_dictSavePurchases.Remove(m_iCurAutionIndex);
            }

            //Commit the comment text
            if (txtComments.Text.Trim().Length > 0 && txtComments.Text.Trim() != CurAuctionEntry.Exhibit.Comments)
            {
                CurAuctionEntry.Exhibit.Comments = txtComments.Text.Trim();
                clsDB.Exhibits.Commit(DB.CommitAction.Modify, CurAuctionEntry.Exhibit);
            }
            
            //Advance to the next exhibit
            if (m_iCurAutionIndex < clsDB.Auction.Items.Count)
            {
                //Note that LoadAuctionEntry will update the value of g_iCurAutionIndex
                LoadAuctionEntry(m_iCurAutionIndex + 1);

                //Find the item in the list and ensure it is selected
                foreach (ListViewItem Item in lsvAuctionOrder.Items)
                {
                    if (Item.Text.Trim() == m_iCurAutionIndex.ToString())
                    {
                        Item.Selected = true;
                        Item.EnsureVisible();
                    }
                }
            }
        }

        private void cmdReset_Click(object sender, EventArgs e)
        {
            //Clear out the m_dictSavePurchases entry for this element
            if (m_dictSavePurchases.ContainsKey(m_iCurAutionIndex))
            {
                m_dictSavePurchases.Remove(m_iCurAutionIndex);
            }

            LoadAuctionEntry(m_iCurAutionIndex);
        }

        void Items_ItemAdded(object sender, Helpers.EditableItemCollectionEventArgs e)
        {
            EditablePurchaseListViewItem Item = null;
            if (e.Item is EditablePurchaseListViewItem)
            {
                if (!m_dictSavePurchases.ContainsKey(m_iCurAutionIndex))
                {
                    m_dictSavePurchases.Add(m_iCurAutionIndex, new List<EditablePurchaseListViewItem>());
                }

                Item = (EditablePurchaseListViewItem)e.Item;
                if (!m_dictSavePurchases[m_iCurAutionIndex].Contains(Item))
                {
                    m_dictSavePurchases[m_iCurAutionIndex].Add(Item);
                }
            }
        }

        void Items_ItemRemoved(object sender, Helpers.EditableItemCollectionEventArgs e)
        {
            EditablePurchaseListViewItem Item = null;
            if (e.Item is EditablePurchaseListViewItem)
            {
                Item = (EditablePurchaseListViewItem)e.Item;
                if (m_dictSavePurchases.ContainsKey(m_iCurAutionIndex))
                {
                    if (m_dictSavePurchases[m_iCurAutionIndex].Contains(Item))
                    {
                        m_dictSavePurchases[m_iCurAutionIndex].Remove(Item);
                    }

                    if (m_dictSavePurchases[m_iCurAutionIndex].Count >= 0)
                    {
                        m_dictSavePurchases.Remove(m_iCurAutionIndex);
                    }
                }
            }
        }


        private void ReloadGrid()
        {
            lsvAuctionOrder.Items.Clear();
            foreach (DB.clsAuctionIndex Index in clsDB.Auction.Items.Values)
            {
                lsvAuctionOrder.Items.Add(Index);
            }

            lsvAuctionOrder.Sort();
        }

        private void UpdateGrid_AuctionOrder(Dictionary<DB.AuctionData, DB.CommitAction> UpdatedItems)
        {
            foreach (DB.clsAuctionIndex Index in UpdatedItems.Keys)
            {
                //Is the item already on the list?
                bool bFound = false;
                for (int i = 0; i < lsvAuctionOrder.Items.Count && !bFound; i++)
                {
                    if ((DB.clsAuctionIndex)lsvAuctionOrder.Items[i] == Index)
                    {
                        //The item was found on the list, update or remove it?
                        if (UpdatedItems[Index] == DB.CommitAction.Modify && TestFilter(Index) == DB.CommitAction.Modify)
                        {
                            //Update it, noting that if it's sort position has
                            //  changed  (e.g., their first name was changed,
                            //  and the user has sorted by name) the sort
                            //  function on the list view will take care of
                            //  moving it.
                            clsDB.Auction[Index].Selected = lsvAuctionOrder.Items[i].Selected;
                            if (lsvAuctionOrder.Items.IndexOf(clsDB.Auction[Index]) != i)
                            {
                                lsvAuctionOrder.Items.Remove(clsDB.Auction[Index]);
                            }
                            lsvAuctionOrder.Items[i] = clsDB.Auction[Index];
                        }
                        else
                        {
                            lsvAuctionOrder.Items[i].Remove();
                        }
                        bFound = true;
                    }
                }

                //The item was not found on the list and needs to be added
                if (!bFound && UpdatedItems[Index] == DB.CommitAction.Modify)
                {
                    lsvAuctionOrder.Items.Add(clsDB.Auction[Index]);
                }
            }
        }

        private DB.CommitAction TestFilter(DB.clsAuctionIndex Buyer)
        {
            //There are no filters on this screen
            return DB.CommitAction.Modify;
        }


        //Populate the entry screen for the exhibit
        private void LoadAuctionEntry(int ID)
        {
            ListViewItem lviCurrentItem = null;
            //Find the previous item to in the grid
            foreach (ListViewItem Item in lsvAuctionOrder.Items)
            {
                if (Item.Text.Trim() == m_iCurAutionIndex.ToString())
                {
                    lviCurrentItem = Item;
                }
            }

            if (lviCurrentItem != null)
            {
                //Highlight the old entry if there are unsaved changes
                if (m_dictSavePurchases.ContainsKey(m_iCurAutionIndex))
                {
                    //Have any of the unsaved items been modified?
                    bool bModified = false;
                    foreach (EditablePurchaseListViewItem item in m_dictSavePurchases[m_iCurAutionIndex])
                    {
                        if (item.PurchaseEntered)
                        {
                            bModified = true;
                            break;
                        }
                    }

                    if (bModified)
                    {
                        lviCurrentItem.Font = new Font(lviCurrentItem.Font, FontStyle.Italic);
                    }
                    else
                    {
                        lviCurrentItem.Font = new Font(lviCurrentItem.Font, FontStyle.Regular);
                    }
                }
                else
                {
                    lviCurrentItem.Font = new Font(lviCurrentItem.Font, FontStyle.Regular);
                }
            }

            m_iCurAutionIndex = ID;
            lblCurOrder.Text = ID.ToString();
            lblMaxOrder.Text = "of " + clsDB.Auction.Items.Count.ToString();

            if (clsDB.Auction.Items.ContainsKey(ID) && clsDB.Auction.Items[ID].Exhibit != null)
            {
                DB.clsAuctionIndex CurAuctionEntry = clsDB.Auction.Items[ID];

                //Update the description
                lblDescription.Text = "Market " + CurAuctionEntry.Exhibit.MarketItem.MarketType + ", Tag Number: " + CurAuctionEntry.Exhibit.TagNumber + ", Weight/Qty: " + CurAuctionEntry.Exhibit.WeightString;
                if (CurAuctionEntry.Exhibit.ChampionStatus != DB.ChampionState.Other)
                {
                    lblDescription.Text = (string)(CurAuctionEntry.Exhibit.ChampionStatus) + " " + lblDescription.Text;
                }

                lblExhibitor.Text = CurAuctionEntry.Exhibit.Exhibitor.Name.First + " " + CurAuctionEntry.Exhibit.Exhibitor.Name.Last;
                txtComments.Text = CurAuctionEntry.Exhibit.Comments;

                elvPurchases.Items.Clear();

                //Are there any unsaved purchases for this item...
                if (m_dictSavePurchases.ContainsKey(m_iCurAutionIndex))
                {
                    //Add them to the purchases list view
                    foreach (EditablePurchaseListViewItem Item in m_dictSavePurchases[m_iCurAutionIndex])
                    {
                        elvPurchases.Items.Add(Item);
                    }
                    m_dictSavePurchases[m_iCurAutionIndex][0].SubItems[0].Focus();
                }
                else
                {
                    //Check to see if purchases have already been entered for this exhibit
                    if (clsDB.Purchases[CurAuctionEntry.Exhibit] != null && clsDB.Purchases[CurAuctionEntry.Exhibit].Count > 0)
                    {
                        //There are currently purchases in the database, load them
                        foreach (DB.clsPurchase Purchase in clsDB.Purchases[CurAuctionEntry.Exhibit].Values)
                        {
                            elvPurchases.Items.Add(new EditablePurchaseListViewItem(CurAuctionEntry, Purchase));
                        }
                    }
                    else
                    {
                        //There are currently no purchases in the databse, add an empty record to get started
                        EditablePurchaseListViewItem StartItem = new EditablePurchaseListViewItem(CurAuctionEntry);
                        elvPurchases.Items.Add(StartItem);
                        StartItem.SubItems[0].Focus();
                    }
                }

                cmdNext.Enabled = true;
            }
            else
            {
                lblDescription.Text = "";
                lblExhibitor.Text = "";
                txtComments.Text = "";
                elvPurchases.Items.Clear();
                cmdNext.Enabled = false;
            }
        }
    }

    //Editable List view class used for entering purchase data
    public class EditablePurchaseListViewItem : Helpers.EditableListViewItem
    {
        DB.clsAuctionIndex m_dbAuctionEntry;
        DB.clsPurchase m_dbPurchase;

        double m_fOriginalBid = -1;
        int m_iOriginalBuyer = -1;

        bool m_bInvalidBidConfirmed = false;
        bool m_bInvalidBuyerConfirmed = false;

        public EditablePurchaseListViewItem(DB.clsAuctionIndex AuctionEntry)
        {
            m_dbAuctionEntry = AuctionEntry;
            RefreshColumns();
            ConstructorArgs = new object[] { AuctionEntry };
        }

        public EditablePurchaseListViewItem(DB.clsAuctionIndex AuctionEntry, DB.clsPurchase Purchase)
        {
            m_dbPurchase = Purchase;
            m_dbAuctionEntry = AuctionEntry;
            
            m_fOriginalBid = Purchase.FinalBid;
            m_iOriginalBuyer = Purchase.BuyerID;

            RefreshColumns();
            RefreshPurchase(Purchase);
            ConstructorArgs = new object[] { AuctionEntry, Purchase };
        }

        private void RefreshColumns()
        {
            base.SubItems.Clear();
            base.SubItems.Add(new EditableTextBoxListViewSubItem("Bid"));
            ((EditableTextBoxListViewSubItem)base.SubItems[(int)ucRunAuction.PurchaseEntryColumns.Bid]).FormatString = "$0.00/" + m_dbAuctionEntry.Exhibit.MarketItem.MarketUnits;
            ((EditableTextBoxListViewSubItem)base.SubItems[(int)ucRunAuction.PurchaseEntryColumns.Bid]).LostFocus += BidListViewItem_LostFocus;
            base.SubItems.Add(new EditableTextBoxListViewSubItem("Buyer #"));
            ((EditableTextBoxListViewSubItem)base.SubItems[(int)ucRunAuction.PurchaseEntryColumns.Buyer_Number]).LostFocus += BuyerNumberListViewItem_LostFocus;
            base.SubItems.Add(new EditableListViewSubItem("Buyer Name"));
            base.SubItems.Add(new EditableListViewSubItem("Total Bid"));
            base.SubItems.Add(new Helpers.EditableListViewItem.EditableButtonListViewSubItem("Remove"));
            ((EditableButtonListViewSubItem)base.SubItems[(int)ucRunAuction.PurchaseEntryColumns.Remove]).Click += lviRemove_Click;
            base.SubItems.Add(new Helpers.EditableListViewItem.EditableButtonListViewSubItem("Add Purchase"));
            ((EditableButtonListViewSubItem)base.SubItems[(int)ucRunAuction.PurchaseEntryColumns.Add]).Click += lviAdd_Click;

        }

        private void RefreshPurchase(DB.clsPurchase Purchase)
        {
            this.SubItems[(int)ucRunAuction.PurchaseEntryColumns.Bid].Value = Purchase.FinalBid;
            this.SubItems[(int)ucRunAuction.PurchaseEntryColumns.Buyer_Number].Value = Purchase.BuyerID;
            if (Purchase.Buyer != null)
            {
                this.SubItems[(int)ucRunAuction.PurchaseEntryColumns.Buyer_Name].Value = Purchase.Buyer.Name.ToString();
            }
            else
            {
                this.SubItems[(int)ucRunAuction.PurchaseEntryColumns.Buyer_Name].Value = "Buyer not found";
            }
            this.SubItems[(int)ucRunAuction.PurchaseEntryColumns.Total_Bid].Value = Purchase.TotalBid_String;
        }

        private void lviRemove_Click(object sender, EventArgs e)
        {
            Helpers.EditableListView listview = null;
            if (this.ListView != null)
            {
                listview = (Helpers.EditableListView)(this.ListView);
                listview.Items.Remove(this);

                if (listview.Items.Count <= 0)
                {
                    EditablePurchaseListViewItem lviNew = (EditablePurchaseListViewItem)listview.Items.Add(new EditablePurchaseListViewItem(m_dbAuctionEntry));
                    lviNew.EnsureVisible();
                    lviNew.ImageIndex = 0;
                    lviNew.SubItems[0].Focus();    
                }
            }
        }

        void lviAdd_Click(object sender, EventArgs e)
        {
            Helpers.EditableListView listview = null;
            if (this.ListView != null)
            {
                listview = (Helpers.EditableListView)(this.ListView);
                EditablePurchaseListViewItem lviNew = (EditablePurchaseListViewItem)listview.Items.Add(new EditablePurchaseListViewItem(m_dbAuctionEntry));
                lviNew.EnsureVisible();
                lviNew.ImageIndex = 0;
                lviNew.SubItems[0].Focus();
            }
        }

        void BidListViewItem_LostFocus(object sender, Helpers.FocusEventArgs e)
        {
            double fBid = -1;
            string sBid = "";
            if (this.SubItems[(int)ucRunAuction.PurchaseEntryColumns.Bid].Value != null)
            {
                sBid = this.SubItems[(int)ucRunAuction.PurchaseEntryColumns.Bid].Value.ToString().Trim();
                if (sBid.Length > 0)
                {
                    sBid = sBid.Replace("$", "");
                    sBid = sBid.Replace("/" + m_dbAuctionEntry.Exhibit.MarketItem.MarketUnits, "");
                    if (double.TryParse(sBid, out fBid))
                    {
                        if(m_dbAuctionEntry.Exhibit.MarketItem.SellByPound)
                        {
                            this.SubItems[(int)ucRunAuction.PurchaseEntryColumns.Total_Bid].Value = (fBid * m_dbAuctionEntry.Exhibit.Weight).ToString("$0.00");
                        } else
                        {
                            this.SubItems[(int)ucRunAuction.PurchaseEntryColumns.Total_Bid].Value = (fBid).ToString("$0.00");
                        }

                        
                    }
                    else
                    {
                        if (!m_bInvalidBidConfirmed)
                        {
                            m_bInvalidBidConfirmed = true;
                            MessageBox.Show("Bid must be an floating point number", "Invalid Bid", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            this.SubItems[(int)ucRunAuction.PurchaseEntryColumns.Bid].Focus();
                        }
                    }
                }
            }
        }

        void BuyerNumberListViewItem_LostFocus(object sender, Helpers.FocusEventArgs e)
        {
            int iBuyerNumber = -1;
            if (this.SubItems[(int)ucRunAuction.PurchaseEntryColumns.Buyer_Number].Value != null)
            {
                //Ensure a valid integer was entered
                try
                {
                    iBuyerNumber = int.Parse(this.SubItems[(int)ucRunAuction.PurchaseEntryColumns.Buyer_Number].Value.ToString());
                }
                catch (FormatException ex)
                {
                    if (!m_bInvalidBuyerConfirmed)
                    {
                        m_bInvalidBuyerConfirmed = true;
                        MessageBox.Show("Buyer number must be an integer", "Invalid Buyer", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        this.SubItems[(int)ucRunAuction.PurchaseEntryColumns.Buyer_Number].Focus();
                    }
                }
            }

            if (iBuyerNumber >= 0)
            {
                //Make sure the buyer is valid
                DB.clsBuyer cBuyer = clsDB.Buyers[iBuyerNumber];
                if (cBuyer != null)
                {
                    //Display the buyer name
                    if (cBuyer.CompanyName.Trim().Length > 0)
                    {
                        this.SubItems[(int)ucRunAuction.PurchaseEntryColumns.Buyer_Name].Value = cBuyer.Name.First + " " + cBuyer.Name.Last + " from " + cBuyer.CompanyName;
                    }
                    else
                    {
                        this.SubItems[(int)ucRunAuction.PurchaseEntryColumns.Buyer_Name].Value = cBuyer.Name.First + " " + cBuyer.Name.Last;
                    }

                    //Warn if this buyer has already checked out
                    if (cBuyer.CheckedOut)
                    {
                        this.SubItems[(int)ucRunAuction.PurchaseEntryColumns.Buyer_Name].Value = "[Checked Out] " + this.SubItems[(int)ucRunAuction.PurchaseEntryColumns.Buyer_Name].Text;
                        this.SubItems[(int)ucRunAuction.PurchaseEntryColumns.Buyer_Name].Font = new Font(this.SubItems[(int)ucRunAuction.PurchaseEntryColumns.Buyer_Name].Font, FontStyle.Italic);
                        this.SubItems[(int)ucRunAuction.PurchaseEntryColumns.Buyer_Name].ForeColor = Color.Red;
                    }
                    else
                    {
                        this.SubItems[(int)ucRunAuction.PurchaseEntryColumns.Buyer_Name].Font = new Font(this.SubItems[(int)ucRunAuction.PurchaseEntryColumns.Buyer_Name].Font, FontStyle.Regular);
                        this.SubItems[(int)ucRunAuction.PurchaseEntryColumns.Buyer_Name].ForeColor = this.ForeColor;
                    }
                }
                else
                {
                    this.SubItems[(int)ucRunAuction.PurchaseEntryColumns.Buyer_Name].Value = "Buyer Not Found";
                    this.SubItems[(int)ucRunAuction.PurchaseEntryColumns.Buyer_Name].Font = new Font(this.SubItems[(int)ucRunAuction.PurchaseEntryColumns.Buyer_Name].Font, FontStyle.Italic);
                    this.SubItems[(int)ucRunAuction.PurchaseEntryColumns.Buyer_Name].ForeColor = Color.Red;
                }
            }
        }

        protected override void OnItemChanged(ItemEventArgs e)
        {
            base.OnItemChanged(e);

            if (this.Purchase != null)
            {
                this.ImageIndex = 1;
            }
            else if (this.SubItems[0].Value == null && this.SubItems[1].Value == null)
            {
                this.ImageIndex = 2;
            }
            else
            {
                this.ImageIndex = 0;
            }
        }

        public DB.clsPurchase Purchase
        {
            get
            {
                float fBid = -1;
                int iBuyer = -1;

                if (float.TryParse(((string)(base.SubItems[(int)ucRunAuction.PurchaseEntryColumns.Bid]).Value), out fBid) &&
                    int.TryParse(((string)(base.SubItems[(int)ucRunAuction.PurchaseEntryColumns.Buyer_Number]).Value), out iBuyer))
                {
                    if (m_dbPurchase == null)
                    {
                        m_dbPurchase = new DB.clsPurchase(m_dbAuctionEntry.Exhibit.NewPurchase(), m_dbAuctionEntry.Exhibit.TagNumber, m_dbAuctionEntry.Exhibit.MarketItem.MarketID, iBuyer, m_dbAuctionEntry.Exhibit.Exhibitor.ExhibitorNumber, fBid);
                    }
                    else
                    {
						m_dbPurchase.BuyerID = iBuyer;
                        m_dbPurchase.FinalBid = fBid;
                    }
                }
                return m_dbPurchase;
            }
        }

        //Returns true if the purchase data was changed from the original purchase object passed to the constructor
        public bool PurchaseModified
        {
            get
            {
                if (m_dbPurchase != null)
                {
                    float fBid = -1;
                    int iBuyer = -1;

                    if (float.TryParse(((string)(base.SubItems[(int)ucRunAuction.PurchaseEntryColumns.Bid]).Value), out fBid) &&
                        int.TryParse(((string)(base.SubItems[(int)ucRunAuction.PurchaseEntryColumns.Buyer_Number]).Value), out iBuyer))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return true;
                }
            }
        }

        //Returns true if data was entered into the text boxes and it is different from the original purchase
        public bool PurchaseEntered
        {
            get
            {
                double fBid = -1;
                int iBuyer = -1;

                if (base.SubItems[(int)ucRunAuction.PurchaseEntryColumns.Bid].Value != null)
                {
                    double.TryParse(((string)(base.SubItems[(int)ucRunAuction.PurchaseEntryColumns.Bid]).Value.ToString()), out fBid);
                }

                if (base.SubItems[(int)ucRunAuction.PurchaseEntryColumns.Buyer_Number].Value != null)
                {
                    int.TryParse(((string)(base.SubItems[(int)ucRunAuction.PurchaseEntryColumns.Buyer_Number]).Value), out iBuyer);
                }

                //Was anything entered in to the text boxes
                if (fBid > -1 || iBuyer > -1)
                {
                    //Was there a purchase assocaiated with this entry?
                    if (m_dbPurchase == null)
                    {
                        //No, this must have been modified
                        return true;
                    }
                    else
                    {
                        return (m_iOriginalBuyer != iBuyer || m_fOriginalBid != fBid);
                    }
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
