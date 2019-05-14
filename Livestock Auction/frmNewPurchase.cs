using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;

namespace Livestock_Auction
{
    public partial class frmNewPurchase : Form
    {
        DB.clsBuyer CurrentBuyer = null;
        DB.clsExhibit CurrentExhibit = null;
        int CurrentPurchaseIndex = int.MinValue;

        public frmNewPurchase()
        {
            InitializeComponent();
            ExhibitorEntry.Text = "Recipient";
            TogglePurchaseMode(radNewPurchase.Checked);
        }

        public frmNewPurchase(DB.clsPurchase Purchase)
        {
            InitializeComponent();
            ExhibitorEntry.Text = "Recipient";

            this.Exhibit = Purchase.Exhibit;
            this.Recipient = Purchase.Recipient;
            this.CurrentBuyer = Purchase.Buyer;
            txtPurchasePrice.Text = Purchase.FinalBid.ToString();
            CurrentPurchaseIndex = Purchase.PurchaseIndex;

            radNewPurchase.Checked = (Purchase.Exhibit.MarketItem != clsDB.Market.AdditionalPurchase);
            radAdditionalPayment.Checked = (Purchase.Exhibit.MarketItem == clsDB.Market.AdditionalPurchase);

            TogglePurchaseMode(Purchase.Exhibit.MarketItem != clsDB.Market.AdditionalPurchase);
        }

        public frmNewPurchase(DB.clsBuyer Buyer, DB.clsExhibit Exhibit)
        {
            InitializeComponent();
            ExhibitorEntry.Text = "Recipient";
            
            CurrentBuyer = Buyer;
            if (Exhibit != null && Exhibit.MarketItem != clsDB.Market.AdditionalPurchase)
            {
                this.Exhibit = Exhibit;
            }
            if (Exhibit != null)
            {
                this.Recipient = Exhibit.Exhibitor;
            }

            TogglePurchaseMode(radNewPurchase.Checked);
        }

        private void ResetAutocomplete()
        {
            ExhibitorEntry.ResetAutoComplete();
            MarketItemEntry.ResetAutoComplete();

            txtChampion.AutoCompleteCustomSource.Clear();
            txtChampion.AutoCompleteCustomSource.AddRange(DB.ChampionState.Values);
        }

        private void frmNewPurchase_Load(object sender, EventArgs e)
        {
            ResetAutocomplete();
        }
        
        private void ExhibitorEntry_Leave(object sender, EventArgs e)
        {
            if (radAdditionalPayment.Checked)
            {
                TogglePurchaseMode(false);
            }
            MarketItemEntry.Focus();
        }

        private void txtChampion_TextChanged(object sender, EventArgs e)
        {
            //TODO: Test this and make sure it works like the original
            DB.ChampionState state = txtChampion.Text;
            if (state.Valid)
            {
                txtChampion.Text = state;
                txtChampion.Tag = state;
            }
        }


        private void cmdOk_Click(object sender, EventArgs e)
        {
            if (this.Purchase != null)
            {
                //Warn if this will modify an existing exhibit
                DB.clsExhibit NewExhibit = clsDB.Exhibits[Exhibit];
                if (NewExhibit != null && (NewExhibit.ChampionStatus != Exhibit.ChampionStatus || NewExhibit.Weight != Exhibit.Weight || NewExhibit.TakeBack != Exhibit.TakeBack))
                {
                    if (MessageBox.Show("Are you sure you want to modify the current exhibit? This may alter the value of an existing purchase.", "Modify Exhibit", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        this.DialogResult = DialogResult.OK;
                    }
                }
                else
                {
                    this.DialogResult = DialogResult.OK;
                }
            }
            else
            {
                if (Recipient == null)
                {
                    MessageBox.Show("Select an recipient");
                }
                else if (MarketItem == null)
                {
                    MessageBox.Show("Select a market item");
                }
                else if (Exhibit == null)
                {
                    MessageBox.Show("Enter the exhibit");
                }
                else if (Purchase == null)
                {
                    MessageBox.Show("Enter the purchase price");
                }
            }
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void cmdClearExhibitor_Click(object sender, EventArgs e)
        {
            Recipient = null;
        }

        private void cmdClearMarketItem_Click(object sender, EventArgs e)
        {
            MarketItem = null;
        }

        private void cmdClearExhibit_Click(object sender, EventArgs e)
        {
            // Note that simply setting Exhibit to null would clear the exhibitor and market item entries as well.
            txtTagNumber.Text = "";
            txtChampion.Text = "";
            txtWeight.Text = "";
            txtPurchasePrice.Text = "";
            radTakeBackNo.Checked = false;
            radTakeBackYes.Checked = false;
        }

        private void txtTagNumber_Leave(object sender, EventArgs e)
        {
            // Pre-populate the rest of the exhibit fields if the exibit already exists
            int iTagNumber = -1;
            if (int.TryParse(txtTagNumber.Text, out iTagNumber))
            {
                if (MarketItem != null)
                {
                    DB.clsExhibit CurrentExhibit = clsDB.Exhibits[iTagNumber, MarketItem.MarketID];
                    if (CurrentExhibit != null)
                    {
                        Exhibit = CurrentExhibit;
                    }
                }
                txtChampion.Focus();
            }
            else
            {
                cmdNewTag.Focus();
            }
        }

        private void cmdNewTag_Click(object sender, EventArgs e)
        {
            if (MarketItem != null)
            {
                txtTagNumber.Text = MarketItem.NewTag().ToString();
            }
        }

        private void radAdditionalPayment_CheckedChanged(object sender, EventArgs e)
        {
            if (radAdditionalPayment.Checked)
            {
                TogglePurchaseMode(false);
            }
        }

        private void radNewPurchase_CheckedChanged(object sender, EventArgs e)
        {
            if (radNewPurchase.Checked)
            {
                TogglePurchaseMode(true);
            }
        }

        public DB.clsBuyer Buyer
        {
            set
            {
                CurrentBuyer = value;
            }
        }

        public DB.clsExhibitor Recipient
        {
            get
            {
                return ExhibitorEntry.Exhibitor;
            }
            set
            {
                ExhibitorEntry.Exhibitor = value;
            }
        }

        public DB.clsMarketItem MarketItem
        {
            get
            {
                return MarketItemEntry.MarketItem;
            }
            set
            {
                MarketItemEntry.MarketItem = value;
            }
        }

        public DB.clsExhibit Exhibit
        {
            get
            {
                int iTagNumber = -1;
                int iWeight = -1;
                DB.NoYes TakeBack = DB.NoYes.NotSet;

                if (radTakeBackNo.Checked)
                {
                    TakeBack = DB.NoYes.No;
                }
                else if (radTakeBackYes.Checked)
                {
                    TakeBack = DB.NoYes.Yes;
                }
                if (MarketItem != null && Recipient != null && int.TryParse(txtTagNumber.Text, out iTagNumber) && int.TryParse(txtWeight.Text, out iWeight) && txtChampion.Tag != null && TakeBack != DB.NoYes.NotSet)
                {
                    DB.clsExhibit SelectedExhibit = clsDB.Exhibits[iTagNumber, MarketItem.MarketID];
                    if (SelectedExhibit == null)
                    {
                        SelectedExhibit = new DB.clsExhibit(iTagNumber, Recipient.ExhibitorNumber, MarketItem.MarketID, (DB.ChampionState)txtChampion.Tag, chkRateOfGain.Checked, iWeight, TakeBack, false, "");
                    }
                    else
                    {
                        SelectedExhibit.ChampionStatus = (DB.ChampionState)txtChampion.Tag;
                        SelectedExhibit.Weight = iWeight;
                        SelectedExhibit.TakeBack = TakeBack;
                    }
                    return SelectedExhibit;
                }
                return CurrentExhibit;
            }
            set
            {
                if (value != null)
                {
                    CurrentExhibit = value;
                    if (Recipient == null)
                    {
                        Recipient = value.Exhibitor;
                    }
                    MarketItem = value.MarketItem;
                    txtTagNumber.Text = value.TagNumber.ToString();
                    txtChampion.Text = value.ChampionStatus;
                    txtChampion.Tag = value.ChampionStatus;
                    chkRateOfGain.Checked = value.RateOfGain;
                    txtWeight.Text = value.Weight.ToString();
                    if (value.TakeBack == DB.NoYes.Yes)
                    {
                        radTakeBackNo.Checked = false;
                        radTakeBackYes.Checked = true;
                    }
                    else if (value.TakeBack == DB.NoYes.No)
                    {
                        radTakeBackNo.Checked = true;
                        radTakeBackYes.Checked = false;
                    }
                }
                else
                {
                    MarketItem = null;
                    txtTagNumber.Text = "";
                    txtChampion.Text = "";
                    txtChampion.Tag = DB.ChampionState.Other;
                    chkRateOfGain.Checked = false;
                    txtWeight.Text = "";
                    radTakeBackNo.Checked = false;
                    radTakeBackYes.Checked = false;
                }
            }
        }

        public DB.clsPurchase Purchase
        {
            get
            {
                if (Recipient != null && Exhibit != null && CurrentBuyer != null)
                {
                    double dFinalBid = 0;
                    if (double.TryParse(txtPurchasePrice.Text, out dFinalBid))
                    {
                        if (CurrentPurchaseIndex == int.MinValue)
                        {
                            CurrentPurchaseIndex = Exhibit.NewPurchase();
                            return new DB.clsPurchase(CurrentPurchaseIndex, Exhibit, CurrentBuyer, Recipient, dFinalBid);
                        }
                        else
                        {
                            DB.clsPurchase Current = clsDB.Purchases[Exhibit, CurrentPurchaseIndex];
                            if (Current != null)
                            {
                                Current.BuyerID = CurrentBuyer.BuyerNumber;
                                Current.RecipientID = Recipient.ExhibitorNumber;
                                Current.FinalBid = dFinalBid;
                                return Current;
                            }
                            else
                            {
                                CurrentPurchaseIndex = Exhibit.NewPurchase();
                                return new DB.clsPurchase(CurrentPurchaseIndex, Exhibit, CurrentBuyer, Recipient, dFinalBid);
                            }
                        }
                    }
                }
                return null;
            }
            set
            {
                Exhibit = value.Exhibit;
                Recipient = value.Recipient;
                CurrentBuyer = value.Buyer;
                txtPurchasePrice.Text = value.FinalBid.ToString();
            }
        }


        private void TogglePurchaseMode(bool FullEntry)
        {
            DB.clsExhibit SavedExhibit = null;

            MarketItemEntry.Enabled = FullEntry;
            grbExhibit.Enabled = FullEntry;
            cmdClearMarketItem.Enabled = FullEntry;
            cmdClearExhibit.Enabled = FullEntry;

            if (FullEntry || (CurrentExhibit != null && CurrentExhibit.MarketItem == clsDB.Market.AdditionalPurchase))
            {
                Exhibit = CurrentExhibit;
            }
            else
            {
                //Save the current exhibit so CurrentExhibit won't be overwritten with the "Additional Payment" purchase. This if block takes care of preserving any data the user may have entered.
                if (Exhibit == null || Exhibit.MarketItem == clsDB.Market.AdditionalPurchase)
                {
                    SavedExhibit = CurrentExhibit;
                }
                else
                {
                    SavedExhibit = Exhibit;
                }

                //If the user selected "Additional Payment", create a new "Additional Payment" exhibit
                if (Recipient != null)
                {
                    Exhibit = new DB.clsExhibit(clsDB.Market[clsDB.Market.AdditionalPurchase.MarketID].NewTag(), Recipient.ExhibitorNumber, clsDB.Market.AdditionalPurchase.MarketID, DB.ChampionState.Other, false, 1, DB.NoYes.No, false, "");
                }
                CurrentExhibit = SavedExhibit;
            }
        }
    }
}
