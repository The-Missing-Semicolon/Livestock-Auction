using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Livestock_Auction.CheckoutGrid
{
    public partial class ucCheckoutGrid : UserControl
    {
        public class ItemChangedEventArgs : EventArgs
        {
            ucCheckoutItem ciItem = null;
            int iColumnChanged = 0;
            bool bItemValid = false;
            string sItemSummary = "";

            public ItemChangedEventArgs(ucCheckoutItem ItemChanged, ucCheckoutItem.ItemChangedEventArgs ChangedEvent)
            {
                ciItem = ItemChanged;
                iColumnChanged = ChangedEvent.ItemChanged;
                bItemValid = ChangedEvent.ItemValid;
                sItemSummary = ChangedEvent.ItemSummary;
            }

            public ucCheckoutItem ItemChanged
            {
                get
                {
                    return ciItem;
                }
            }

            public int ColumnChanged
            {
                get
                {
                    return iColumnChanged;
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



        const int COLUMN_SPACING = 2;

        //Indices of controls in header columns
        //  A nice alternative to indexing all of the components of the
        //  header. A Filler panel was added to ensure that even the
        //  last column has all three items
        const int COLHEAD_NEXTPANEL = 0;
        const int COLHEAD_SPLITTER = 1;
        const int COLHEAD_BUTTON = 2;

        const int COLFOOT_TEXT = 0;
        const int COLFOOT_BUTTON = 1;

        List<Panel> lstColumnHeaders;
        ucCheckoutItem ciSelectedItem = null;
        double fTotalOwed = 0;

        public event EventHandler<EventArgs> SelectedItemChanged;
        public event EventHandler<ItemChangedEventArgs> ItemChanged;
        public event EventHandler<EventArgs> TotalChanged;

        public ucCheckoutGrid()
        {
            InitializeComponent();
        }

        private void ucCheckoutGrid_Load(object sender, EventArgs e)
        {
            ResetHeaders();
        }

        private void cmdcolHeader_Resized(object sender, EventArgs e)
        {
            //Determine which column was resized and how big it is now
            int iColIndex = lstColumnHeaders.IndexOf((Panel)((Control)sender).Parent);
            float fColWidth = ((Control)sender).Parent.Controls[COLHEAD_BUTTON].Width + COLUMN_SPACING;

            //Resize the field inside all of the rows
            foreach (ucCheckoutGridElement Purchase in flpGrid.Controls)
            {
                Purchase.SetColumnWidth(iColIndex, fColWidth);
            }
        }

        private void splcolHeader_SplitterMoved(object sender, SplitterEventArgs e)
        {
            int iColIndex = lstColumnHeaders.IndexOf((Panel)((Control)sender).Parent);
            
            //If the position of the "total owed" column moved, move the grand total field on the bottom
            if (iColIndex < ucCheckoutItem.COLUMN_TOTALOWED)
            {
                lblGrandTotal.Left = pancolHeader.PointToClient(pancolTotalOwed.PointToScreen(cmdcolTotalOwed.Location)).X;
            }
            if (iColIndex < ucCheckoutItem.COLUMN_TOTALOWED + 1)
            {
                lblGrandTotal.Width = cmdcolTotalOwed.Width + COLUMN_SPACING;
            }
        }

        protected void onItemChanged(ItemChangedEventArgs e)
        {
            if (ItemChanged != null)
            {
                ItemChanged(this, e);
            }
        }

        protected void onSelectedItemChanged(EventArgs e)
        {
            if (SelectedItemChanged != null)
            {
                SelectedItemChanged(this, e);
            }
        }

        protected void onTotalChanged(EventArgs e)
        {
            if (TotalChanged != null)
            {
                TotalChanged(this, e);
            }
        }

        public void Clear()
        {
            ciSelectedItem = null;
            Item_ItemSelected(ciSelectedItem, new EventArgs());
            flpGrid.Controls.Clear();

            Recalculate();
        }


        public ucCheckoutItem Add(DB.clsPurchase Purchase)
        {
            //Create the checkout item
            ucCheckoutItem Item = new ucCheckoutItem(Purchase);

            //Set the column widths for the new purchase
            for (int i = 0; i < lstColumnHeaders.Count; i++)
            {
                float fColWidth = lstColumnHeaders[i].Controls[COLHEAD_BUTTON].Width + COLUMN_SPACING;
                Item.SetColumnWidth(i, fColWidth);
            }

            //Add the item to the list
            flpGrid.Controls.Add(Item);
            Recalculate();

            //Associate the events
            Item.ItemSelected += new EventHandler<EventArgs>(Item_ItemSelected);
            Item.ItemRecalculated += new EventHandler<EventArgs>(Item_ItemRecalculated);
            Item.ColumnChanged += new EventHandler<ucCheckoutItem.ItemChangedEventArgs>(Item_ColumnChanged);

            return Item;
        }

        public void Remove(DB.clsPurchase Purchase)
        {
            ucCheckoutItem FoundItem = null;
            foreach (ucCheckoutItem Item in flpGrid.Controls)
            {
                if (Item.Purchase == Purchase)
                {
                    FoundItem = Item;
                    break;
                }
            }

            if (FoundItem != null)
            {
                flpGrid.Controls.Remove(FoundItem);
                Recalculate();
            }
        }

        private void ResetHeaders()
        {
            //Create the lookup table for column header buttons
            lstColumnHeaders = new List<Panel>();
            lstColumnHeaders.Add(pancolHeader); //Exhibitor column panel doubles as the panel for the exhibitor, might rename this later.
            lstColumnHeaders.Add(pancolItem);
            lstColumnHeaders.Add(pancolQuantity);
            lstColumnHeaders.Add(pancolBidUnit);
            lstColumnHeaders.Add(pancolBidTotal);
            lstColumnHeaders.Add(pancolSaleCondition);
            lstColumnHeaders.Add(pancolTotalOwed);
            lstColumnHeaders.Add(pancolDisposition);

            this.SuspendLayout();
            for (int i = 0; i < lstColumnHeaders.Count; i++)
            {
                float fColWidth = lstColumnHeaders[i].Controls[COLHEAD_BUTTON].Width + COLUMN_SPACING;
                foreach (ucCheckoutGridElement Purchase in flpGrid.Controls)
                {
                    Purchase.SetColumnWidth(i, fColWidth);
                }
            }
            this.ResumeLayout();
        }

        private void Recalculate()
        {
            fTotalOwed = 0;
            if (flpGrid.Controls.Count > 0)
            {
                foreach (ucCheckoutGridElement Purchase in flpGrid.Controls)
                {
                    if (Purchase.GetType() == typeof(ucCheckoutItem))
                    {
                        if (((ucCheckoutItem)Purchase).CalculationError)
                        {
                            lblGrandTotal.Text = "$ #,###.##";
                            return;
                        }
                        else
                        {
                            fTotalOwed += ((ucCheckoutItem)Purchase).Purchase.TotalCharged;
                        }
                    }
                }
            }
            else
            {
                lblGrandTotal.Text = "No Purchases";
            }
            lblGrandTotal.Text = fTotalOwed.ToString("$ #,##0.00");
        }







        void Item_ItemSelected(object sender, EventArgs e)
        {
            if (sender != null)
            {
                //Unsubscribe from the event handler to prevent this from tripping the CheckedChanged event which would potentailly change all of the records
                cmbcolSetDisposition.SelectedIndexChanged -= new System.EventHandler(cmbcolSetDisposition_SelectedIndexChanged);

                //Deselect all of the other items
                foreach (Control Item in flpGrid.Controls)
                {
                    //If the current item is not the selected item, de-select it
                    if (Item != (Control)sender)
                    {
                        if (Item.GetType() == typeof(ucCheckoutItem))
                        {
                            ((ucCheckoutItem)Item).Selected = false;
                        }
                    }
                }

                //Set the currently selected item and fire the selected item changed event if necessary
                if (ciSelectedItem != (ucCheckoutItem)sender)
                {
                    ciSelectedItem = (ucCheckoutItem)sender;
                    onSelectedItemChanged(new EventArgs());
                }
                else
                {
                    ciSelectedItem = (ucCheckoutItem)sender;
                }


                //Update the header options
                //Sale condition radio buttons...
                if (ciSelectedItem.Purchase.ConditionOfSale == DB.clsPurchase.enSaleCondition.PayAdvertising)
                {
                    radSaleAdvertising.Checked = true;
                    radSaleFullPrice.Checked = false;
                }
                else if (ciSelectedItem.Purchase.ConditionOfSale == DB.clsPurchase.enSaleCondition.PayFullPrice)
                {
                    radSaleFullPrice.Checked = true;
                    radSaleAdvertising.Checked = false;
                }
                else
                {
                    radSaleAdvertising.Checked = false;
                    radSaleFullPrice.Checked = false;
                }

                //Disposition dropdown...
                if (ciSelectedItem.Disposition == DB.clsPurchase.enAnimalDestination.NotSet)
                {
                    cmbcolSetDisposition.Text = "(not set)";
                }
                else if (ciSelectedItem.Disposition == DB.clsPurchase.enAnimalDestination.Buyer)
                {
                    cmbcolSetDisposition.Text = "Hauled by Buyer";
                }
                else if (ciSelectedItem.Disposition == DB.clsPurchase.enAnimalDestination.Seller)
                {
                    cmbcolSetDisposition.Text = "Hauled by Seller";
                }
                else if (ciSelectedItem.Disposition == DB.clsPurchase.enAnimalDestination.Fair)
                {
                    cmbcolSetDisposition.Text = "Hauled by Fair";
                }
                else if (ciSelectedItem.Disposition == DB.clsPurchase.enAnimalDestination.SpecialInstructions)
                {
                    cmbcolSetDisposition.Text = "Other Instructions";
                }

                //Re-subscribe
                cmbcolSetDisposition.SelectedIndexChanged += new System.EventHandler(cmbcolSetDisposition_SelectedIndexChanged);
            }
            else
            {
                radSaleAdvertising.Checked = false;
                radSaleFullPrice.Checked = false;

                //Set the currently selected item and fire the selected item changed event if necessary
                if (ciSelectedItem != null)
                {
                    ciSelectedItem = null;
                    onSelectedItemChanged(new EventArgs());
                }
                else
                {
                    ciSelectedItem = null;
                }
            }
        }

        void Item_ItemRecalculated(object sender, EventArgs e)
        {
            Recalculate();
            onTotalChanged(new EventArgs());
        }

        void Item_ColumnChanged(object sender, ucCheckoutItem.ItemChangedEventArgs e)
        {
            if (ciSelectedItem != null && (object)ciSelectedItem == sender)
            {
                //Update the sale condition radio buttons if necessary
                if ((ciSelectedItem.SaleCondition == DB.clsPurchase.enSaleCondition.PayAdvertising && radSaleFullPrice.Checked) ||
                    (ciSelectedItem.SaleCondition == DB.clsPurchase.enSaleCondition.PayFullPrice && radSaleAdvertising.Checked) ||
                    (!radSaleAdvertising.Checked && !radSaleFullPrice.Checked && ciSelectedItem.SaleCondition != DB.clsPurchase.enSaleCondition.NotSet)
                    )
                {
                    //Update the radio buttons
                    if (ciSelectedItem.SaleCondition == DB.clsPurchase.enSaleCondition.PayAdvertising)
                    {
                        radSaleAdvertising.Checked = true;
                        radSaleFullPrice.Checked = false;
                    }
                    else if (ciSelectedItem.SaleCondition == DB.clsPurchase.enSaleCondition.PayFullPrice)
                    {
                        radSaleFullPrice.Checked = true;
                        radSaleAdvertising.Checked = false;
                    }
                }

                //Update the disposition dropdown
                if ((ciSelectedItem.Disposition == DB.clsPurchase.enAnimalDestination.NotSet && cmbcolSetDisposition.Text != "(not set)") ||
                    (ciSelectedItem.Disposition == DB.clsPurchase.enAnimalDestination.Buyer && cmbcolSetDisposition.Text != "Hauled by Buyer") ||
                    (ciSelectedItem.Disposition == DB.clsPurchase.enAnimalDestination.Seller && cmbcolSetDisposition.Text != "Hauled by Seller") ||
                    (ciSelectedItem.Disposition == DB.clsPurchase.enAnimalDestination.Fair && cmbcolSetDisposition.Text != "Hauled by Fair") ||
                    (ciSelectedItem.Disposition == DB.clsPurchase.enAnimalDestination.SpecialInstructions && cmbcolSetDisposition.Text != "Other Instructions"))
                {
                    //Unsubscribe from the event handlers
                    cmbcolSetDisposition.SelectedIndexChanged -= new System.EventHandler(cmbcolSetDisposition_SelectedIndexChanged);

                    if (ciSelectedItem.Disposition == DB.clsPurchase.enAnimalDestination.NotSet)
                    {
                        cmbcolSetDisposition.Text = "(not set)";
                    }
                    else if (ciSelectedItem.Disposition == DB.clsPurchase.enAnimalDestination.Buyer)
                    {
                        cmbcolSetDisposition.Text = "Hauled by Buyer";
                    }
                    else if (ciSelectedItem.Disposition == DB.clsPurchase.enAnimalDestination.Seller)
                    {
                        cmbcolSetDisposition.Text = "Hauled by Seller";
                    }
                    else if (ciSelectedItem.Disposition == DB.clsPurchase.enAnimalDestination.Fair)
                    {
                        cmbcolSetDisposition.Text = "Hauled by Fair";
                    }
                    else if (ciSelectedItem.Disposition == DB.clsPurchase.enAnimalDestination.SpecialInstructions)
                    {
                        cmbcolSetDisposition.Text = "Other Instructions";
                    }

                    //Re-subscribe to the event handlers
                    cmbcolSetDisposition.SelectedIndexChanged += new System.EventHandler(cmbcolSetDisposition_SelectedIndexChanged);
                }
            }
            try
            {
                ItemChangedEventArgs eventargs = new ItemChangedEventArgs((ucCheckoutItem)sender, e);
                onItemChanged(eventargs);
            }
            catch (InvalidCastException ex)
            {
            }
        }

        private void radSaleHeader_Clicked(object sender, EventArgs e)
        {
            SetAllSaleConditions();
        }

        private void cmbcolSetDisposition_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetAllDispositions(false);
        }

        private void cmdcolCopyDisposition_Click(object sender, EventArgs e)
        {
            SetAllDispositions(true);
        }

        private void SetAllSaleConditions()
        {
            DB.clsPurchase.enSaleCondition CurrentSaleCondition = DB.clsPurchase.enSaleCondition.NotSet;
            bool bWarnChange = false;

            //Determine the selected value
            if (radSaleAdvertising.Checked)
            {
                CurrentSaleCondition = DB.clsPurchase.enSaleCondition.PayAdvertising;
            }
            else if (radSaleFullPrice.Checked)
            {
                CurrentSaleCondition = DB.clsPurchase.enSaleCondition.PayFullPrice;
            }

            if (CurrentSaleCondition != DB.clsPurchase.enSaleCondition.NotSet)
            {
                //Check the current values of the check boxes, warn if any of them will be changed from a set value to a not set value
                foreach (ucCheckoutGridElement Element in flpGrid.Controls)
                {
                    ucCheckoutItem Purchase = null;
                    try
                    {
                        Purchase = (ucCheckoutItem)Element;
                    }
                    catch (InvalidCastException ex)
                    {

                    }

                    if (Purchase != null)
                    {
                        //Was the sale condition changed?
                        if (Purchase.SaleCondition != CurrentSaleCondition && Purchase.SaleCondition != DB.clsPurchase.enSaleCondition.NotSet)
                        {
                            //If sale condition is being changed to paying advertising, but the exhibit does not allow for paying only advertising, no change would be made
                            if (CurrentSaleCondition == DB.clsPurchase.enSaleCondition.PayAdvertising)
                            {
                                if (Purchase.Purchase.Exhibit.MarketItem.AllowAdvertising == true)
                                {
                                    bWarnChange = true;
                                }
                            }
                            else
                            {
                                bWarnChange = true;
                            }
                        }
                    }
                }

                //Set all the values contignent on user confirmation
                if (!bWarnChange || MessageBox.Show(string.Format("Change all sale conditions to {0}?", CurrentSaleCondition == DB.clsPurchase.enSaleCondition.PayAdvertising ? "Advertising" : "Full Price"), "Change sale condition", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    foreach (ucCheckoutGridElement Element in flpGrid.Controls)
                    {
                        ucCheckoutItem Purchase = null;
                        try
                        {
                            Purchase = (ucCheckoutItem)Element;
                        }
                        catch (InvalidCastException ex)
                        {

                        }

                        if (Purchase != null)
                        {
                            if (CurrentSaleCondition == DB.clsPurchase.enSaleCondition.PayAdvertising)
                            {
                                if (Purchase.Purchase.Exhibit.MarketItem.AllowAdvertising)
                                {
                                    Purchase.SaleCondition = CurrentSaleCondition;
                                }
                            }
                            else
                            {
                                Purchase.SaleCondition = CurrentSaleCondition;
                            }
                        }
                    }
                }
            }
        }

        private void SetAllDispositions(bool SetDetails)
        {
            DB.clsPurchase.enAnimalDestination CurrentDisposition = DB.clsPurchase.enAnimalDestination.NotSet;
            bool bWarnChange = false;
            string sDispositionDetails = "";

            //Determine the selected value
            if (cmbcolSetDisposition.Text == "Hauled by Buyer")
            {
                CurrentDisposition = DB.clsPurchase.enAnimalDestination.Buyer;
            }
            else if (cmbcolSetDisposition.Text == "Hauled by Seller")
            {
                CurrentDisposition = DB.clsPurchase.enAnimalDestination.Seller;
                //if (ciSelectedItem != null)
                //{
                //    sDispositionDetails = ciSelectedItem.DispositionDetails;
                //}
            }
            else if (cmbcolSetDisposition.Text == "Hauled by Fair")
            {
                CurrentDisposition = DB.clsPurchase.enAnimalDestination.Fair;
            }
            else if (cmbcolSetDisposition.Text == "Other Instructions")
            {
                CurrentDisposition = DB.clsPurchase.enAnimalDestination.SpecialInstructions;
                if (ciSelectedItem != null)
                {
                    sDispositionDetails = ciSelectedItem.DispositionDetails;
                }
            }
            else
            {
                CurrentDisposition = DB.clsPurchase.enAnimalDestination.NotSet;
            }

            if (CurrentDisposition != DB.clsPurchase.enAnimalDestination.NotSet)
            {
                //Check the current values of the drop downs, warn if any of them will be changed from a set value to a not set value
                if (SetDetails)
                {
                    bWarnChange = true;
                }
                else
                {
                    foreach (ucCheckoutGridElement Element in flpGrid.Controls)
                    {
                        ucCheckoutItem Purchase = null;
                        try
                        {
                            Purchase = (ucCheckoutItem)Element;
                        }
                        catch (InvalidCastException ex)
                        {

                        }

                        if (Purchase != null)
                        {
                            //Was the sale condition changed?
                            if (Purchase.Disposition != CurrentDisposition && Purchase.Disposition != DB.clsPurchase.enAnimalDestination.NotSet)
                            {
                                bWarnChange = true;
                            }
                        }
                    }
                }

                //Set all the values contignent on user confirmation
                if (!bWarnChange || MessageBox.Show(string.Format("Change all dispositions to {0}?", cmbcolSetDisposition.Text), "Change disposition", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    foreach (ucCheckoutGridElement Element in flpGrid.Controls)
                    {
                        ucCheckoutItem Purchase = null;
                        try
                        {
                            Purchase = (ucCheckoutItem)Element;
                        }
                        catch (InvalidCastException ex)
                        {

                        }

                        if (Purchase != null)
                        {
                            Purchase.Disposition = CurrentDisposition;
                            if (SetDetails)
                            {
                                Purchase.DispositionDetails = sDispositionDetails;
                            }
                        }
                    }
                }
            }
        }

        public ucCheckoutItem SelectedItem
        {
            get
            {
                return ciSelectedItem;
            }
        }

        public double TotalOwed
        {
            get
            {
                return fTotalOwed;
            }
        }

        public bool Valid
        {
            get
            {
                foreach (ucCheckoutGridElement Purchase in flpGrid.Controls)
                {
                    if (Purchase.GetType() == typeof(ucCheckoutItem))
                    {
                        if (((ucCheckoutItem)Purchase).CalculationError)
                        {
                            return false;
                        }
                        if (!((ucCheckoutItem)Purchase).Purchase.DestinationOfAnimal_Valid)
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
        }
    }
}
