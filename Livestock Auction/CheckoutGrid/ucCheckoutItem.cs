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
    public partial class ucCheckoutItem : ucCheckoutGridElement
    {
        public class ItemChangedEventArgs : EventArgs
        {
            int iItemChanged = 0;
            bool bItemValid = false;
            string sItemSummary = "";

            public ItemChangedEventArgs(int ColumnChanged, bool Valid, string Summary)
            {
                iItemChanged = ColumnChanged;
                bItemValid = Valid;
                sItemSummary = Summary;
            }

            public int ItemChanged
            {
                get
                {
                    return iItemChanged;
                }
            }

            public bool ItemValid
            {
                get
                {
                    return bItemValid;
                }
            }

            public string ItemSummary
            {
                get
                {
                    return sItemSummary;
                }
            }
        }


        public const int COLUMN_RECIPIENT = 0;
        public const int COLUMN_ANIMAL = 1;
        public const int COLUMN_WEIGHT = 2;
        public const int COLUMN_BID = 3;
        public const int COLUMN_TOTALBID = 4;
        public const int COLUMN_SALECONDITION = 5;
        public const int COLUMN_TOTALOWED = 6;
        public const int COLUMN_DISPOSITION = 7;


        DB.clsPurchase CurrentPurchase;
        
        bool bItemSelected = false;     //Indicates if this item is selected or not
        bool bCalculationError = false; //Inidcates information is missing for the total owed calculation

        public event EventHandler<EventArgs> ItemSelected;
        public event EventHandler<EventArgs> ItemRecalculated;
        public event EventHandler<ItemChangedEventArgs> ColumnChanged;

        public ucCheckoutItem()
        {
            InitializeComponent();
        }

        public ucCheckoutItem(DB.clsPurchase Purchase)
        {
            InitializeComponent();
            tlpMainPanel = tlpMain;
            LoadPurchase(Purchase);
        }

        private void MouseEnter_Highlight(object sender, EventArgs e)
        {
            if (bItemSelected)
            {
                tlpMain.BackColor = Color.DarkGray;
            }
            else
            {
                tlpMain.BackColor = Color.LightGray;
            }
        }

        private void MouseLeave_Highlight(object sender, EventArgs e)
        {
            // This event is used by all of the non-transparent controls on
            //  the form. Before making the form white again, check to see
            //  if the cursor is still on the control. Otherwise, there is
            //  a flicker when moving from a child control back over the panel.
            if (!tlpMain.Bounds.Contains(Control.MousePosition))
            {
                if (bItemSelected)
                {
                    tlpMain.BackColor = Color.LightGray;
                }
                else
                {
                    tlpMain.BackColor = SystemColors.Window;
                }
            }
        }
        #region GUI Updated
        private void MouseUp_Select(object sender, MouseEventArgs e)
        {
            this.Selected = true;
        }


        private void radSaleFullPrice_CheckedChanged(object sender, EventArgs e)
        {
            UpdateSaleCondition();
            Recalculate();
        }

        private void radSaleAdvertising_CheckedChanged(object sender, EventArgs e)
        {
            UpdateSaleCondition();
            Recalculate();
        }

        private void cmbDisposition_TextUpdate(object sender, EventArgs e)
        {
            UpdateDisposition(false);
        }

        private void cmbDisposition_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateDisposition(false);
        }

        private void txtDispositionSpecify_TextChanged(object sender, EventArgs e)
        {
            UpdateDisposition(true);
        }

        protected void onSelected(EventArgs e)
        {
            if (ItemSelected != null)
            {
                ItemSelected(this, e);
            }
        }

        protected void onRecalculated(EventArgs e)
        {
            if (ItemRecalculated != null)
            {
                ItemRecalculated(this, e);
            }
        }

        protected void onColumnChanged(ItemChangedEventArgs e)
        {
            if (ColumnChanged != null)
            {
                ColumnChanged(this, e);
            }
        }
        #endregion

        private void LoadPurchase(DB.clsPurchase Purchase)
        {
            CurrentPurchase = Purchase;

            //Recipient Information
            lblRecipientName.Text = Purchase.Recipient.Name.First + " " + Purchase.Recipient.Name.Last;
            lblRecipientNumber.Text = Purchase.Recipient.ExhibitorNumber.ToString();

            //Exhibit/Item Information
            lblItemType.Text = Purchase.Exhibit.MarketItem.MarketType;
            lblItemTag.Text = Purchase.Exhibit.TagNumber.ToString();
            lblItemQty.Text = Purchase.Exhibit.WeightString;

            //Bid Information
            lblBid.Text = Purchase.FinalBid_String;
            lblBidTotal.Text = Purchase.TotalBid_String;

            //TakeBack/TurnBack
            if (Purchase.ConditionOfSale == DB.clsPurchase.enSaleCondition.PayAdvertising)
            {
                radSaleAdvertising.Checked = true;
            }
            else if (Purchase.ConditionOfSale == DB.clsPurchase.enSaleCondition.PayFullPrice)
            {
                radSaleFullPrice.Checked = true;
            }
            else
            {
                radSaleAdvertising.Checked = false;
                radSaleFullPrice.Checked = false;

                if (!Purchase.Exhibit.MarketItem.AllowAdvertising)
                {
                    radSaleFullPrice.Checked = true;
                }
            }
            radSaleAdvertising.Enabled = Purchase.Exhibit.MarketItem.AllowAdvertising;
            radSaleFullPrice.Enabled = Purchase.Exhibit.MarketItem.AllowAdvertising;

            //Disposition
            if (!Purchase.Exhibit.MarketItem.ValidDisposition)
            {
                cmbDisposition.Enabled = false;
                txtDispositionSpecify.Enabled = false;
                cmbDisposition.SelectedIndex = 0;
            }
            else if (Purchase.DestinationOfAnimal > 0)
            {
                //Set the drop down box
                cmbDisposition.SelectedIndex = (int)(Purchase.DestinationOfAnimal);

                //Fill in the specify field
                if (Purchase.DestinationOfAnimal == DB.clsPurchase.enAnimalDestination.HauledBy)
                {
                    txtDispositionSpecify.Enabled = true;
                    txtDispositionSpecify.Text = CurrentPurchase.HauledBy;
                }
                else if (Purchase.DestinationOfAnimal == DB.clsPurchase.enAnimalDestination.SpecialInstructions)
                {
                    txtDispositionSpecify.Enabled = true;
                    txtDispositionSpecify.Text = CurrentPurchase.HaulSpecialInstructions;
                }
                else
                {
                    txtDispositionSpecify.Enabled = false;
                }
            }
            else
            {
                txtDispositionSpecify.Enabled = false;
                cmbDisposition.SelectedIndex = 0;
            }

            Recalculate();
        }

        // Recalculate the total ammount owed by the buyer for this purchase
        private void Recalculate()
        {
            bCalculationError = true;
            if (!radSaleAdvertising.Checked && !radSaleFullPrice.Checked)
            {
                //No sale condition was selected
                lblTotalOwed.Text = "Select sale condition";
                lblTotalOwed.ForeColor = Color.Red;
            }
            else
            {
                //Everything looks good
                bCalculationError = false;
                lblTotalOwed.ForeColor = Color.Black;
                if (radSaleAdvertising.Checked)
                {
                    CurrentPurchase.ConditionOfSale = DB.clsPurchase.enSaleCondition.PayAdvertising;
                }
                else if (radSaleFullPrice.Checked)
                {
                    CurrentPurchase.ConditionOfSale = DB.clsPurchase.enSaleCondition.PayFullPrice;
                }

                lblTotalOwed.Text = CurrentPurchase.TotalCharged.ToString("$#,##0.00");
            }

            //TODO: Maybe get fancy and only throw this if the total cost changes
            onRecalculated(new EventArgs());
        }

        private void UpdateSaleCondition()
        {
            bool bChanged = false;

            if (radSaleAdvertising.Checked)
            {
                //Is this actually a change?
                if (CurrentPurchase.ConditionOfSale != DB.clsPurchase.enSaleCondition.PayAdvertising)
                {
                    bChanged = true;
                    CurrentPurchase.ConditionOfSale = DB.clsPurchase.enSaleCondition.PayAdvertising;
                }
                cmbDisposition.Enabled = false;
                txtDispositionSpecify.Enabled = false;
            }
            else if (radSaleFullPrice.Checked)
            {
                //Is this actually a change?
                if (CurrentPurchase.ConditionOfSale != DB.clsPurchase.enSaleCondition.PayFullPrice)
                {
                    bChanged = true;
                    CurrentPurchase.ConditionOfSale = DB.clsPurchase.enSaleCondition.PayFullPrice;
                }
                cmbDisposition.Enabled = CurrentPurchase.Exhibit.MarketItem.ValidDisposition;
                txtDispositionSpecify.Enabled = CurrentPurchase.Exhibit.MarketItem.ValidDisposition && (Disposition == DB.clsPurchase.enAnimalDestination.HauledBy || Disposition == DB.clsPurchase.enAnimalDestination.SpecialInstructions);
            }
            else
            {
                //Is this actually a change?
                if (CurrentPurchase.ConditionOfSale != DB.clsPurchase.enSaleCondition.NotSet)
                {
                    bChanged = true;
                    CurrentPurchase.ConditionOfSale = DB.clsPurchase.enSaleCondition.NotSet;
                }
                cmbDisposition.Enabled = false;
                txtDispositionSpecify.Enabled = false;
            }

            bool Valid = (CurrentPurchase.ConditionOfSale != DB.clsPurchase.enSaleCondition.NotSet);
            string Summary = (CurrentPurchase.ConditionOfSale == DB.clsPurchase.enSaleCondition.PayAdvertising) ? "Advertising" : ((CurrentPurchase.ConditionOfSale == DB.clsPurchase.enSaleCondition.PayFullPrice) ? "Full Price" : "(unspecified)");

            onColumnChanged(new ItemChangedEventArgs(COLUMN_SALECONDITION, Valid, Summary));

            //Only commit if there was a change
            if (Purchase != null && bChanged)
            {
                clsDB.Purchases.Commit(DB.CommitAction.Modify, Purchase);
            }
        }

        private void UpdateDisposition(bool Textbox_Changed)
        {
            bool bChanged = false;

            //Set the drop down box
            if (cmbDisposition.Text == "Hauled by Buyer")
            {
                if (CurrentPurchase.DestinationOfAnimal != DB.clsPurchase.enAnimalDestination.SelfHauled)
                {
                    CurrentPurchase.DestinationOfAnimal = DB.clsPurchase.enAnimalDestination.SelfHauled;
                    bChanged = true;
                }
                txtDispositionSpecify.Enabled = false;
            }
            else if (cmbDisposition.Text == "Hauled by Other")
            {
                if (CurrentPurchase.DestinationOfAnimal != DB.clsPurchase.enAnimalDestination.HauledBy)
                {
                    CurrentPurchase.DestinationOfAnimal = DB.clsPurchase.enAnimalDestination.HauledBy;
                    bChanged = true;
                }
                txtDispositionSpecify.Enabled = CurrentPurchase.Exhibit.MarketItem.ValidDisposition;
                if (Textbox_Changed && CurrentPurchase.HauledBy.Trim() != txtDispositionSpecify.Text.Trim())
                {
                    CurrentPurchase.HauledBy = txtDispositionSpecify.Text;
                    bChanged = true;
                }
            }
            else if (cmbDisposition.Text == "Galvinell")
            {
                if (CurrentPurchase.DestinationOfAnimal != DB.clsPurchase.enAnimalDestination.Galvinell)
                {
                    CurrentPurchase.DestinationOfAnimal = DB.clsPurchase.enAnimalDestination.Galvinell;
                    bChanged = true;
                }
                txtDispositionSpecify.Enabled = false;
            }
            else if (cmbDisposition.Text == "Other Instructions")
            {
                if (CurrentPurchase.DestinationOfAnimal != DB.clsPurchase.enAnimalDestination.SpecialInstructions)
                {
                    CurrentPurchase.DestinationOfAnimal = DB.clsPurchase.enAnimalDestination.SpecialInstructions;
                    bChanged = true;
                }
                txtDispositionSpecify.Enabled = CurrentPurchase.Exhibit.MarketItem.ValidDisposition;
                if (Textbox_Changed && CurrentPurchase.HaulSpecialInstructions.Trim() != txtDispositionSpecify.Text.Trim())
                {
                    CurrentPurchase.HaulSpecialInstructions = txtDispositionSpecify.Text;
                    bChanged = true;
                }
            }

            onColumnChanged(new ItemChangedEventArgs(COLUMN_DISPOSITION, CurrentPurchase.DestinationOfAnimal_Valid, CurrentPurchase.DestinationOfAnimal_String));

            if (Purchase != null && bChanged)
            {
                clsDB.Purchases.Commit(DB.CommitAction.Modify, Purchase);
            }
        }


        public DB.clsPurchase Purchase
        {
            get
            {
                return CurrentPurchase;
            }
            set
            {
                LoadPurchase(value);
            }
        }

        public DB.clsPurchase.enSaleCondition SaleCondition
        {
            get
            {
                if (radSaleAdvertising.Checked)
                {
                    return DB.clsPurchase.enSaleCondition.PayAdvertising;
                }
                else if (radSaleFullPrice.Checked)
                {
                    return DB.clsPurchase.enSaleCondition.PayFullPrice;
                }
                else
                {
                    return DB.clsPurchase.enSaleCondition.NotSet;
                }
            }
            set
            {
                if (value == DB.clsPurchase.enSaleCondition.PayAdvertising)
                {
                    radSaleAdvertising.Checked = true;
                    radSaleFullPrice.Checked = false;
                }
                else if (value == DB.clsPurchase.enSaleCondition.PayFullPrice)
                {
                    radSaleFullPrice.Checked = true;
                    radSaleAdvertising.Checked = false;
                }
                else
                {
                    radSaleAdvertising.Checked = false;
                    radSaleFullPrice.Checked = false;
                }
            }
        }

        public DB.clsPurchase.enAnimalDestination Disposition
        {
            get
            {
                //Set the drop down box
                //TODO: These texts should be loaded from a central list that maps to the enumeration.
                if (cmbDisposition.Text == "Hauled by Buyer")
                {
                    return DB.clsPurchase.enAnimalDestination.SelfHauled;
                }
                else if (cmbDisposition.Text == "Hauled by Other")
                {
                    return DB.clsPurchase.enAnimalDestination.HauledBy;
                }
                else if (cmbDisposition.Text == "Galvinell")
                {
                    return DB.clsPurchase.enAnimalDestination.Galvinell;
                }
                else if (cmbDisposition.Text == "Other Instructions")
                {
                    return DB.clsPurchase.enAnimalDestination.SpecialInstructions;
                }
                else
                {
                    return DB.clsPurchase.enAnimalDestination.NotSet;
                }
            }
            set
            {
                if (value == DB.clsPurchase.enAnimalDestination.SelfHauled)
                {
                    cmbDisposition.Text = "Hauled by Buyer";
                    txtDispositionSpecify.Enabled = false;
                }
                else if (value == DB.clsPurchase.enAnimalDestination.HauledBy)
                {
                    cmbDisposition.Text = "Hauled by Other";
                    txtDispositionSpecify.Enabled = CurrentPurchase.Exhibit.MarketItem.ValidDisposition && (SaleCondition == DB.clsPurchase.enSaleCondition.PayFullPrice);
                }
                else if (value == DB.clsPurchase.enAnimalDestination.Galvinell)
                {
                    cmbDisposition.Text = "Galvinell";
                    txtDispositionSpecify.Enabled = false;
                }
                else if (value == DB.clsPurchase.enAnimalDestination.SpecialInstructions)
                {
                    cmbDisposition.Text = "Other Instructions";
                    txtDispositionSpecify.Enabled = (SaleCondition == DB.clsPurchase.enSaleCondition.PayFullPrice);
                }
                else
                {
                    cmbDisposition.Text = "(not set)";
                    txtDispositionSpecify.Enabled = false;
                }
            }
        }

        public string DispositionDetails
        {
            get
            {
                return txtDispositionSpecify.Text;
            }
            set
            {
                txtDispositionSpecify.Text = value;
            }
        }

        public bool Selected
        {
            get
            {
                return bItemSelected;
            }
            set
            {
                if (!bItemSelected && value)
                {
                    //The control was just selected
                    tlpMain.BackColor = Color.DarkGray;
                    onSelected(new EventArgs());
                }
                else if (bItemSelected && !value)
                {
                    //The control was just de-selected
                    if (tlpMain.Bounds.Contains(Control.MousePosition))
                    {
                        tlpMain.BackColor = Color.LightGray;
                    }
                    else
                    {
                        tlpMain.BackColor = Color.White;
                    }
                }
                bItemSelected = value;
            }
        }

        public bool CalculationError
        {
            get
            {
                return bCalculationError;
            }
        }
    }
}
