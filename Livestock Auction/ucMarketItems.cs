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
    public partial class ucMarketItems : UserControl
    {
        public ucMarketItems()
        {
            InitializeComponent();
            ReloadGrid();

            clsDB.Market.Updated += new EventHandler<DB.DatabaseUpdatedEventArgs>(Market_Updated);
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            int iMarketID = -1;
            double fItemValue = 0;

            if ((txtID.Text.Trim().Length == 0 || int.TryParse(txtID.Text, out iMarketID)) && double.TryParse(txtItemValue.Text, out fItemValue))
            {
                DB.clsMarketItem MarketItem = new DB.clsMarketItem(iMarketID, txtItemName.Text.Trim(), fItemValue, txtItemUnits.Text.Trim(), chkAllowAdvertising.Checked, chkValidDisposition.Checked, chkSellByPound.Checked);
                clsDB.Market.Commit(DB.CommitAction.Modify, MarketItem);

                LoadRecord(null);
            }
            else if (txtID.Text.Trim().Length != 0 && !int.TryParse(txtID.Text, out iMarketID))
            {
                MessageBox.Show("Market ID must be an integer", "Invalid Market ID", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtID.Focus();
            }
            else if (!double.TryParse(txtItemValue.Text, out fItemValue))
            {
                MessageBox.Show("Market Value must be a number", "Invalid Market Value", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtItemValue.Focus();
            }
        }

        void Market_Updated(object sender, DB.DatabaseUpdatedEventArgs e)
        {
            Delegate temp = new DB.UpdateInvoker(UpdateGrid);

            if (e != null && e.UpdatedItems != null && this != null && temp != null)
            {
                this.Invoke(temp, e.UpdatedItems);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Something is null");
            }
        }

        private void cmdRemove_Click(object sender, EventArgs e)
        {
            int iExID;

            if (int.TryParse(txtID.Text, out iExID))
            {
                DB.clsMarketItem market = clsDB.Market[iExID];

                txtItemName.Text = market.MarketType;
                txtItemValue.Text = market.MarketValue.ToString();
                if (MessageBox.Show("Are you sure you want to delete this record?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    clsDB.Market.Commit(DB.CommitAction.Delete, market);
                }
            }
        }

        private void ReloadGrid()
        {
            lsvMarket.Items.Clear();
            foreach (DB.clsMarketItem MarketItem in clsDB.Market)
            {
                ListViewItem lviNewItem = lsvMarket.Items.Add(MarketItem.MarketID.ToString());
                lviNewItem.SubItems.Add(MarketItem.MarketType);
                lviNewItem.SubItems.Add(MarketItem.MarketValue.ToString("$0.00"));
                lviNewItem.SubItems.Add(MarketItem.MarketUnits);
                lviNewItem.SubItems.Add(MarketItem.AllowAdvertising ? "Allowed" : "");
                lviNewItem.SubItems.Add(MarketItem.ValidDisposition ? "Required" : "");
                lviNewItem.SubItems.Add(MarketItem.SellByPound ? "Pound" : "Item");
                lviNewItem.Tag = MarketItem;
            }
        }

        private void UpdateGrid(Dictionary<DB.AuctionData, DB.CommitAction> UpdatedItems)
        {
            foreach (DB.clsMarketItem Item in UpdatedItems.Keys)
            {
                bool bFound = false;
                for (int i = 0; i < lsvMarket.Items.Count; i++)
                {
                    if ((DB.clsMarketItem)lsvMarket.Items[i].Tag == Item)
                    {
                        if (UpdatedItems[Item] == DB.CommitAction.Modify)
                        {
                            lsvMarket.Items[i].SubItems[1].Text = clsDB.Market[Item].MarketType;
                            lsvMarket.Items[i].SubItems[2].Text = clsDB.Market[Item].MarketValue.ToString("$0.00");
                            lsvMarket.Items[i].SubItems[3].Text = clsDB.Market[Item].MarketUnits;
                            lsvMarket.Items[i].SubItems[4].Text = clsDB.Market[Item].AllowAdvertising ? "Allowed" : "";
                            lsvMarket.Items[i].SubItems[5].Text = clsDB.Market[Item].ValidDisposition ? "Required" : "";
                            lsvMarket.Items[i].SubItems[6].Text = clsDB.Market[Item].SellByPound ? "Pound" : "Item";
                        }
                        else
                        {
                            lsvMarket.Items[i].Remove();
                        }
                        bFound = true;
                        break;
                    }
                }
                if (!bFound && UpdatedItems[Item] == DB.CommitAction.Modify)
                {
                    ListViewItem lviEx = lsvMarket.Items.Add(Item.MarketID.ToString());
                    lviEx.Tag = Item;
                    lviEx.SubItems.Add(clsDB.Market[Item].MarketType);
                    lviEx.SubItems.Add(clsDB.Market[Item].MarketValue.ToString("$0.00"));
                    lviEx.SubItems.Add(clsDB.Market[Item].MarketUnits);
                    lviEx.SubItems.Add(clsDB.Market[Item].AllowAdvertising ? "Allowed" : "");
                    lviEx.SubItems.Add(clsDB.Market[Item].ValidDisposition ? "Required" : "");
                    lviEx.SubItems.Add(clsDB.Market[Item].SellByPound ? "Pound" : "Item");
                }
            }
        }

        private void lsvMarket_SelectedIndexChanged(object sender, EventArgs e)
        {
            DB.clsMarketItem Item = null;
            
            if (lsvMarket.SelectedItems.Count > 0)
            {
                Item = (DB.clsMarketItem)lsvMarket.SelectedItems[0].Tag;
            }
            
            LoadRecord(Item);
        }

        private void txtID_Leave(object sender, EventArgs e)
        {
            int iMarketID = -1;
            if (txtID.Text.Trim().Length > 0 && int.TryParse(txtID.Text.Trim(), out iMarketID))
            {
                DB.clsMarketItem Item = clsDB.Market[iMarketID];
                if (Item != null)
                {
                    LoadRecord(Item);
                }
            }
        }

        private void LoadRecord(DB.clsMarketItem Item)
        {
            if (Item != null)
            {
                txtID.Text = Item.MarketID.ToString();
                txtItemName.Text = Item.MarketType;
                txtItemValue.Text = Item.MarketValue.ToString();
                txtItemUnits.Text = Item.MarketUnits;
                chkAllowAdvertising.Checked = Item.AllowAdvertising;
                chkValidDisposition.Checked = Item.ValidDisposition;
                chkSellByPound.Checked = Item.SellByPound;
            }
            else
            {
                txtID.Text = "";
                txtItemName.Text = "";
                txtItemValue.Text = "";
                txtItemUnits.Text = "";
                chkAllowAdvertising.Checked = false;
                chkValidDisposition.Checked = false;
                chkSellByPound.Checked = false;
                txtID.Focus();
            }
        }

        private void cmdClear_Click(object sender, EventArgs e)
        {
            LoadRecord(null);
        }
    }
}
