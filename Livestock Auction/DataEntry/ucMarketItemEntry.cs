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
    public partial class ucMarketItemEntry : UserControl
    {
        EntryModes EntryMode = EntryModes.New;

        public ucMarketItemEntry()
        {
            InitializeComponent();
        }

        private void ucMarketItemEntry_Load(object sender, EventArgs e)
        {
            ResetAutoComplete();
        }

        private void ucMarketItemEntry_Enter(object sender, EventArgs e)
        {
            //txtMarketItem.TabIndex = 0;
            txtMarketItem.Focus();
        }

        private void txtMarketItem_Enter(object sender, EventArgs e)
        {
            //txtMarketItem.TabIndex = 2;
        }

        private void txtMarketItem_Leave(object sender, EventArgs e)
        {
            if (txtMarketItem.Text.Trim() != "")
            {
                bool bFound = false;
                foreach (DB.clsMarketItem MarketItem in clsDB.Market)   //This loop was nessesary so a case insensitive search could be preformed
                {
                    if (txtMarketItem.Text.Trim().Equals(MarketItem.MarketType, StringComparison.InvariantCultureIgnoreCase))
                    {
                        txtMarketID.Text = MarketItem.MarketID.ToString();
                        txtMarketItem.Text = MarketItem.MarketType;
                        txtMarketValue.Text = MarketItem.MarketValue.ToString();
                        txtMarketUnits.Text = MarketItem.MarketUnits;
                        chkAllowAdvertising.Checked = MarketItem.AllowAdvertising;
                        chkValidDisposition.Checked = MarketItem.ValidDisposition;
                        chkSellByPound.Checked = MarketItem.SellByPound;
                        lblMarketWarning.Visible = false;

                        bFound = true;
                        this.OnLeave(new EventArgs());
                        break;
                    }
                }
                if (!bFound)
                {
                    lblMarketWarning.Visible = true;
                }
            }
        }

        private void txtMarketValue_Leave(object sender, EventArgs e)
        {
            if (txtMarketValue.Text.Trim() == "")
            {
                txtMarketValue.Text = "0";
            }
            txtMarketValue.Text = double.Parse(txtMarketValue.Text).ToString("0.00");
        }

        public void ResetAutoComplete()
        {
            txtMarketItem.AutoCompleteCustomSource.Clear();
            if (clsDB.Connected)
            {
                string[] sMarketItems = new string[clsDB.Market.Count];

                int i = 0;
                foreach (DB.clsMarketItem MarketItem in clsDB.Market)
                {
                    sMarketItems[i] = MarketItem.MarketType;
                    i++;
                }
                txtMarketItem.AutoCompleteCustomSource.AddRange(sMarketItems);
            }
        }

        public void Clear()
        {
            txtMarketID.Text = "";
            txtMarketItem.Text = "";
            txtMarketValue.Text = "";
            txtMarketUnits.Text = "";
            chkAllowAdvertising.Checked = false;
            chkValidDisposition.Checked = false;
            chkSellByPound.Checked = false;
            txtAdvertDestination.Text = "";
            lblMarketWarning.Visible = false;
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

        public int MarketID
        {
            set
            {
                if (clsDB.Market[value] != null)
                {
                    this.MarketItem = clsDB.Market[value];
                }
                else
                {
                    Clear();
                    txtMarketID.Text = value.ToString();
                }
            }
        }

        public DB.clsMarketItem MarketItem
        {
            get
            {
                int iMarketID = -1;
                double dMarketValue = 0;
                if ((txtMarketID.Text.Trim().Length == 0 || int.TryParse(txtMarketID.Text, out iMarketID)) && double.TryParse(txtMarketValue.Text, out dMarketValue) && txtMarketItem.Text.Trim().Length > 0)
                {
                    return new DB.clsMarketItem(iMarketID, txtMarketItem.Text.Trim(), dMarketValue, txtMarketUnits.Text.Trim(), chkAllowAdvertising.Checked, chkValidDisposition.Checked, chkSellByPound.Checked, txtAdvertDestination.Text.Trim());
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (value != null)
                {
                    txtMarketID.Text = value.MarketID.ToString();
                    txtMarketItem.Text = value.MarketType;
                    txtMarketValue.Text = value.MarketValue.ToString("0.00");
                    txtMarketUnits.Text = value.MarketUnits;
                    chkAllowAdvertising.Checked = value.AllowAdvertising;
                    chkValidDisposition.Checked = value.ValidDisposition;
                    chkSellByPound.Checked = value.SellByPound;
                    txtAdvertDestination.Text = value.AdvertDestination;
                }
                else
                {
                    Clear();
                }
            }
        }

        private void chkAllowAdvertising_CheckedChanged(object sender, EventArgs e)
        {
            txtAdvertDestination.Enabled = chkAllowAdvertising.Checked;
        }
    }
}
