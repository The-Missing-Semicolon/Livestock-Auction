using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace Livestock_Auction
{
    public partial class ucBuyerCheckout : UserControl
    {
        double[] m_fAmountOwed;
        NameOrder m_CurrentOrder = NameOrder.LastFirst;
        int m_iBuyerID = -1;

        DB.clsBuyer CurrentBuyer;
        double fTotalPayed = 0;

        public ucBuyerCheckout()
        {
            InitializeComponent();
        }

        private void txtBuyerNumber_Leave(object sender, EventArgs e)
        {
            int iBuyerNumber = 0;

            if (int.TryParse(txtBuyerNumber.Text, out iBuyerNumber))
            {
                DB.clsBuyer cBuyer = clsDB.Buyers[iBuyerNumber];
                if (cBuyer != null)
                {
                    if (iBuyerNumber != m_iBuyerID)
                    {
                        //Buyer number was changed since the last time LoadBuyer was called
                        if (!cBuyer.GhostBuyer)
                        {
                            txtNameFirst.Text = cBuyer.Name.First;
                            txtNameLast.Text = cBuyer.Name.Last;
                            txtNameCompany.Text = cBuyer.CompanyName;
                            tabMain.SelectedTab = tabPurchases;
                            cmdLoad.Enabled = false;
                            LoadBuyer(iBuyerNumber);
                        }
                        else
                        {
                            MessageBox.Show(string.Format("Buyer {0} is not registered, register this buyer before continuing.", iBuyerNumber), "Unregistered Buyer", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            txtBuyerNumber.Focus();
                            ClearBuyer(iBuyerNumber);
                        }
                    }
                    else if (iBuyerNumber == m_iBuyerID)
                    {
                        //Buyer number was not changed since the last time LoadBuyer was called
                        cmdLoad.Text = "Refresh";
                        cmdLoad.Enabled = true;
                    }
                }
            }
        }

        private void cmdLoad_Click(object sender, EventArgs e)
        {
            int iBuyerNumber = 0;
            if (int.TryParse(txtBuyerNumber.Text, out iBuyerNumber))
            {
                DB.clsBuyer cBuyer = clsDB.Buyers[iBuyerNumber];
                if (cBuyer != null)
                {
                    txtNameFirst.Text = cBuyer.Name.First;
                    txtNameLast.Text = cBuyer.Name.Last;
                    txtNameCompany.Text = cBuyer.CompanyName;
                    tabMain.SelectedTab = tabPurchases;
                    cmdLoad.Enabled = false;
                    LoadBuyer(iBuyerNumber);
                }
                else
                {
                    MessageBox.Show(string.Format("Buyer number {0} is not registered", iBuyerNumber), "Unregistered Buyer", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
        }

        private void txtBuyerNumber_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtBuyerNumber_Leave(sender, e);
            }
        }

        private void txtBuyerNumber_TextChanged(object sender, EventArgs e)
        {
            int BuyerNumber = 0;
            if (int.TryParse(txtBuyerNumber.Text, out BuyerNumber) && clsDB.Buyers[BuyerNumber] != null)
            {
                if (m_iBuyerID == BuyerNumber)
                {
                    cmdLoad.Text = "Refresh";
                }
                else
                {
                    cmdLoad.Text = "Load";
                }
                cmdLoad.Enabled = true;
            }
            else
            {
                cmdLoad.Enabled = false;
            }
        }

        private void tabMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            //If the user selected the "Payment" tab, load the receipt
            if (tabMain.SelectedTab == tabPayment)
            {
                if (CheckoutGrid.Valid)
                {
                    RefreshPayments();
                    RefreshReport();
                }
                else
                {
                    MessageBox.Show("There is an error in the checkout grid. Please correct before continuing.\r\n\r\nEnsure that a sale condition is selected for all purchases, a disposition is selected for purchases where the buyer pays full price, and that disposition details are included when necessary.");
                    tabMain.SelectedTab = tabPurchases;
                }
            }
        }

        #region Tab 1 - Purchases

        private void CheckoutGrid_SelectedItemChanged(object sender, EventArgs e)
        {
            tscmdEditPurchase.Enabled = (CheckoutGrid.SelectedItem != null);
            tscmdDeletePurchase.Enabled = (CheckoutGrid.SelectedItem != null);
        }

        private void CheckoutGrid_TotalChanged(object sender, EventArgs e)
        {
            RefreshPayments();
        }

        private void tscmdAddPurchase_Click(object sender, EventArgs e)
        {
            if (m_iBuyerID >= 0)
            {
                frmNewPurchase NewPurchase = null;

                if (CheckoutGrid.SelectedItem != null)
                {
                    NewPurchase = new frmNewPurchase(clsDB.Buyers[m_iBuyerID], CheckoutGrid.SelectedItem.Purchase.Exhibit);
                }
                else
                {
                    NewPurchase = new frmNewPurchase(clsDB.Buyers[m_iBuyerID], null);
                }

                if (NewPurchase.ShowDialog() == DialogResult.OK)
                {
                    if (NewPurchase.Purchase != null)
                    {
                        clsDB.Market.Commit(DB.CommitAction.Modify, NewPurchase.MarketItem);
                        clsDB.Exhibits.Commit(DB.CommitAction.Modify, NewPurchase.Exhibit);
                        clsDB.Purchases.Commit(DB.CommitAction.Modify, NewPurchase.Purchase);
                        CheckoutGrid.Add(NewPurchase.Purchase);

                        RefreshPayments();
                    }
                }
            }
            else
            {
                MessageBox.Show("No buyer selected", "No Buyer", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void tscmdEditPurchase_Click(object sender, EventArgs e)
        {
            if (m_iBuyerID >= 0 && CheckoutGrid.SelectedItem != null)
            {
                frmNewPurchase EditPurchase = new frmNewPurchase(CheckoutGrid.SelectedItem.Purchase);
                if (EditPurchase.ShowDialog() == DialogResult.OK)
                {
                    clsDB.Market.Commit(DB.CommitAction.Modify, EditPurchase.MarketItem);
                    clsDB.Exhibits.Commit(DB.CommitAction.Modify, EditPurchase.Exhibit);
                    clsDB.Purchases.Commit(DB.CommitAction.Modify, EditPurchase.Purchase);

                    CheckoutGrid.SelectedItem.Purchase = EditPurchase.Purchase;
                }
            }
            else if (m_iBuyerID < 0)
            {
                MessageBox.Show("No buyer selected", "No Buyer", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                MessageBox.Show("No purchase selected", "No Buyer", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void tscmdDeletePurchase_Click(object sender, EventArgs e)
        {
            if (m_iBuyerID >= 0 && CheckoutGrid.SelectedItem != null)
            {
                //Check a few more things...
                //  Was this exhibit part of the auction (or was it an additional payment)?
                bool bAuctionExhibit = false;
                bool bMultiPurchase = false;
                bool bConfirmed = false;
                foreach (DB.clsAuctionIndex AuctionExhibit in clsDB.Auction.Items.Values)
                {
                    if (AuctionExhibit.Exhibit == CheckoutGrid.SelectedItem.Purchase.Exhibit)
                    {
                        bAuctionExhibit = true;
                        break;
                    }
                }
                //  Are the multiple purchases associated with this exhibit?
                bMultiPurchase = (CheckoutGrid.SelectedItem.Purchase.Exhibit.Purchases.Count > 1);

                //  Get confirmation from the user
                if (bAuctionExhibit && !bMultiPurchase)
                {
                    bConfirmed = (MessageBox.Show("!!WARNING!!\r\nThis purchase is the only purchase of an item that appeared in the auction! Deleting it will make that item unpurchased in the auction.\r\n\r\nAre you sure you want to delete this purchase?", "Delete Purchase", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes);
                }
                else
                {
                    bConfirmed = (MessageBox.Show("Are you sure you want to delete this purchase?", "Delete Purchase", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes);
                }

                //  Actually delete the purchase if the user has confirmed
                if (bConfirmed)
                {
                    clsDB.Purchases.Commit(DB.CommitAction.Delete, CheckoutGrid.SelectedItem.Purchase);
                    CheckoutGrid.Remove(CheckoutGrid.SelectedItem.Purchase);
                }
            }
            else if (m_iBuyerID < 0)
            {
                MessageBox.Show("No buyer selected", "No Buyer", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                MessageBox.Show("No purchase selected", "No Buyer", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        #endregion

        #region Tab 2 - Payments
        private void lsvPayments_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            PositionTotalRemaining();
        }

        private void lsvPayments_SizeChanged(object sender, EventArgs e)
        {
            PositionTotalRemaining();
        }

        private void lsvPayments_LocationChanged(object sender, EventArgs e)
        {
            PositionTotalRemaining();
        }

        private void cmdClearAddr_Click(object sender, EventArgs e)
        {
            ClearBillingAddress();
        }

        private void cmdCopyAddr_Click(object sender, EventArgs e)
        {
            CopyBillingAddress();
        }

        private void txtCheckNumber_TextChanged(object sender, EventArgs e)
        {
            if (txtCheckNumber.Text.Length > 0)
            {
                radPayedCheck.Checked = true;
            }
        }

        private void lsvPayments_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lsvPayments.SelectedItems.Count > 0)
            {
                cmdRemovePayment.Enabled = true;
            }
            else
            {
                cmdRemovePayment.Enabled = false;
            }
        }

        private void cmdAddPayment_Click(object sender, EventArgs e)
        {
            double fPaymentAmount = 0.0;
            int iCheckNumber = -1;
            if (double.TryParse(txtPaymentAmount.Text.Trim(), out fPaymentAmount) && (radPayedCash.Checked || radPayedCheck.Checked || radPayedCredit.Checked))
            {
                DB.clsPayment Payment = null;
                if (radPayedCash.Checked)
                {
                    Payment = new DB.clsPayment(m_iBuyerID, DB.clsPayment.PaymentMethod.Cash, iCheckNumber, fPaymentAmount, "");
                }
                else if (radPayedCredit.Checked)
                {
                    Payment = new DB.clsPayment(m_iBuyerID, DB.clsPayment.PaymentMethod.Credit, iCheckNumber, fPaymentAmount, "");
                }
                else if (radPayedCheck.Checked)
                {
                    if (int.TryParse(txtCheckNumber.Text, out iCheckNumber))
                    {
                        Payment = new DB.clsPayment(m_iBuyerID, DB.clsPayment.PaymentMethod.Check, iCheckNumber, fPaymentAmount, "");
                    }
                    else
                    {
                        MessageBox.Show("Enter a valid check number.", "New Payment", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }

                if (Payment != null)
                {
                    clsDB.Payments.Commit(DB.CommitAction.Modify, Payment);
                    RefreshPayments();

                    ResetPaymentForm();
                }
            }
            else if (!radPayedCash.Checked && !radPayedCheck.Checked)
            {
                MessageBox.Show("Select a payment type.", "New Payment", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (txtPaymentAmount.Text.Trim().Length <= 0)
            {
                MessageBox.Show("Enter a payment amount.", "New Payment", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                MessageBox.Show("Enter a valid payment amount. The payment amount should include only numbers.", "New Payment", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void cmdRemovePayment_Click(object sender, EventArgs e)
        {
            if (lsvPayments.SelectedItems.Count == 1)
            {
                if (MessageBox.Show("Are you sure you want to remove this payment?", "Remove Payment", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    DB.clsPayment Payment = (DB.clsPayment)lsvPayments.SelectedItems[0].Tag;
                    clsDB.Payments.Commit(DB.CommitAction.Delete, Payment);
                    RefreshPayments();
                }
            }
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

        private void cmdCommit_Click(object sender, EventArgs e)
        {
            //If there is money remaining to be billed, check to see if there is a billing address
            //Note: the comparison here takes in to account imprecision in floating point values
            if ((CheckoutGrid.TotalOwed - fTotalPayed >= 0.01) && (txtAddressee.Text.Trim().Length <= 0 || txtStreet.Text.Trim().Length <= 0 || txtState.Text.Trim().Length <= 0 || txtCity.Text.Trim().Length <= 0 || txtZip.Text.Trim().Length <= 0))
            {
                MessageBox.Show("Buyer has not paid for entire purchase. Either enter remaining payments or enter billing address", "Checkout", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                //If the user changed the buyer's billing address, save the changes
                DB.clsBuyer Buyer = clsDB.Buyers[m_iBuyerID];
                if ((txtAddressee.Text.Trim().Length > 0 && Buyer.BillingAddresse != txtAddressee.Text.Trim()) || (txtStreet.Text.Trim().Length > 0 && Buyer.Billing.Street != txtStreet.Text.Trim()) || (txtState.Text.Trim().Length > 0 && Buyer.Billing.State != txtState.Text.Trim()) || (txtCity.Text.Trim().Length > 0 && Buyer.Billing.City != txtCity.Text.Trim()) || (txtZip.Text.Trim().Length > 0 && Buyer.Billing.Zip.ToString("00000") != txtZip.Text.Trim()))
                {
                    if (txtAddressee.Text.Trim().Length > 0 && Buyer.BillingAddresse != txtAddressee.Text.Trim())
                    {
                        Buyer.BillingAddresse = txtAddressee.Text.Trim();
                    }
                    if (txtStreet.Text.Trim().Length > 0 && Buyer.Billing.Street != txtStreet.Text.Trim())
                    {
                        Buyer.Billing.Street = txtStreet.Text.Trim();
                    }
                    if (txtState.Text.Trim().Length > 0 && Buyer.Billing.State != txtState.Text.Trim())
                    {
                        Buyer.Billing.State = txtState.Text.Trim();
                    }
                    if (txtCity.Text.Trim().Length > 0 && Buyer.Billing.City != txtCity.Text.Trim())
                    {
                        Buyer.Billing.City = txtCity.Text.Trim();
                    }
                    if (txtZip.Text.Trim().Length > 0 && Buyer.Billing.Zip.ToString("00000") != txtZip.Text.Trim())
                    {
                        int iZipCode = 0;
                        if (int.TryParse(txtZip.Text.Trim(), out iZipCode))
                        {
                            Buyer.Billing.Zip = iZipCode;
                        }
                    }
                }
                //Mark the buyer as checked out
                Buyer.CheckedOut = true;

                //Commit this record and print the receipt
                clsDB.Buyers.Commit(DB.CommitAction.Modify, Buyer);
                RefreshReport();

                //The rendering complete event will show the print dialog and unsubscribe from this event
                rptReceipt.RenderingComplete += new RenderingCompleteEventHandler(rptReceipt_PrintOnRenderingComplete);
            }
        }

        void rptReceipt_PrintOnRenderingComplete(object sender, RenderingCompleteEventArgs e)
        {
            //This event handler is mapped during the cmdCommit_Click event.
            rptReceipt.PrintDialog();
            rptReceipt.RenderingComplete -= new RenderingCompleteEventHandler(rptReceipt_PrintOnRenderingComplete);
        }


        private void LoadBillingAddress(DB.clsBuyer Buyer)
        {
            txtAddressee.Text = Buyer.BillingAddresse;
            txtStreet.Text = Buyer.Billing.Street;
            txtCity.Text = Buyer.Billing.City;
            txtState.Text = Buyer.Billing.State;
            txtZip.Text = Buyer.Billing.Zip.ToString("00000");
        }

        private void ClearBillingAddress()
        {
            txtAddressee.Text = "";
            txtStreet.Text = "";
            txtState.Text = "";
            txtCity.Text = "";
            txtZip.Text = "";
        }

        private void CopyBillingAddress()
        {
            txtAddressee.Text = clsDB.Buyers[m_iBuyerID].Name.First + " " + clsDB.Buyers[m_iBuyerID].Name.Last;
            if (clsDB.Buyers[m_iBuyerID].CompanyName.Trim() != "")
            {
                txtAddressee.Text += "\r\n" + clsDB.Buyers[m_iBuyerID].CompanyName;
            }
            txtStreet.Text = clsDB.Buyers[m_iBuyerID].Address.Street;
            txtState.Text = clsDB.Buyers[m_iBuyerID].Address.State;
            txtCity.Text = clsDB.Buyers[m_iBuyerID].Address.City;
            txtZip.Text = clsDB.Buyers[m_iBuyerID].Address.Zip.ToString();
        }

        public void RefreshPayments()
        {
            lsvPayments.Items.Clear();
            fTotalPayed = 0;
            if (clsDB.Payments[m_iBuyerID] != null)
            {
                foreach (DB.clsPayment Payment in clsDB.Payments[m_iBuyerID].Values)
                {
                    ListViewItem lviPayment = lsvPayments.Items.Add(Payment.Method_String);
                    lviPayment.Tag = Payment;
                    lviPayment.SubItems.Add(Payment.Amount.ToString("$#,##0.00"));
                    lviPayment.SubItems.Add(Payment.Surcharge.ToString("$#,##0.00"));
                    lviPayment.SubItems.Add((Payment.Amount + Payment.Surcharge).ToString("$#,##0.00"));
                    fTotalPayed += Payment.Amount;
                }
            }
            RefreshReport();
            lblTotalRemaining.Text = (CheckoutGrid.TotalOwed - fTotalPayed).ToString("$#,##0.00");
        }

        public void ResetPaymentForm()
        {
            radPayedCash.Checked = false;
            radPayedCheck.Checked = false;
            txtCheckNumber.Text = "";
            txtPaymentAmount.Text = "";
        }

        private void PositionTotalRemaining()
        {
            int iTotalPosition = lsvPayments.Left;
            for (int i = 0; i < colAmount.Index; i++)
            {
                iTotalPosition += lsvPayments.Columns[i].Width;
            }
            if (iTotalPosition >= lblRemaining.Right)
            {
                lblTotalRemaining.Left = iTotalPosition;
            }
            else
            {
                lblTotalRemaining.Left = lblRemaining.Right;
            }
            
        }

        #endregion

        public void ClearBuyer(int BuyerNumber)
        {
            m_iBuyerID = BuyerNumber;
            tabPurchases.Select();

            CheckoutGrid.Clear();
            ClearBillingAddress();
            PositionTotalRemaining();
            //RefreshPayments();
        }

        public void LoadBuyer(DB.clsBuyer Buyer)
        {
            txtBuyerNumber.Text = Buyer.BuyerNumber.ToString();
            txtNameFirst.Text = Buyer.Name.First;
            txtNameLast.Text = Buyer.Name.Last;
            LoadBuyer(Buyer.BuyerNumber);
        }

        public void LoadBuyer(int BuyerNumber)
        {
            m_iBuyerID = BuyerNumber;

            List<DB.clsPurchase> Purchases = clsDB.Purchases.GetPurchasesByBuyer(m_iBuyerID);
            CheckoutGrid.Clear();

            tscmdEditPurchase.Enabled = false;
            tscmdDeletePurchase.Enabled = false;

            ResetPaymentForm();

            m_fAmountOwed = new double[Purchases.Count];
            foreach (DB.clsPurchase Purchase in Purchases)
            {
                CheckoutGrid.Add(Purchase);
            }

            LoadBillingAddress(clsDB.Buyers[m_iBuyerID]);

            if (tabMain.SelectedTab == tabPayment)
            {
                RefreshPayments();
            }

            PositionTotalRemaining();
        }

        public void SetNameOrder(NameOrder NewOrder)
        {
            if (NewOrder != m_CurrentOrder)
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

            m_CurrentOrder = NewOrder;
        }

        public void RefreshReport()
        {
            if (tabMain.SelectedTab == tabPayment)
            {
                BindingSource BuyerBinding = new BindingSource(this.components);
                BindingSource PurchaseBinding = new BindingSource(this.components);
                BindingSource PaymentBinding = new BindingSource(this.components);
                BindingSource SettingsBinding = new BindingSource(this.components);

                if (m_iBuyerID >= 0)
                {
                    BuyerBinding.DataSource = clsDB.Buyers[m_iBuyerID];
                    PurchaseBinding.DataSource = clsDB.Purchases.GetPurchasesByBuyer(m_iBuyerID);
                    if (clsDB.Payments[m_iBuyerID] != null)
                    {
                        PaymentBinding.DataSource = clsDB.Payments[m_iBuyerID].Values;
                    }
                    else
                    {
                        PaymentBinding.DataSource = null;
                    }

                    SettingsBinding.DataSource = clsDB.Settings;

                    rptReceipt.Reset();
                    rptReceipt.LocalReport.ReportEmbeddedResource = "Livestock_Auction.Reports.rptBuyerReceipt.rdlc";
                    rptReceipt.LocalReport.DataSources.Add(new ReportDataSource("Livestock_Auction_clsBuyer", BuyerBinding));
                    rptReceipt.LocalReport.DataSources.Add(new ReportDataSource("Livestock_Auction_clsPurchase", PurchaseBinding));
                    rptReceipt.LocalReport.DataSources.Add(new ReportDataSource("Livestock_Auction_clsPayment", PaymentBinding));
                    rptReceipt.LocalReport.DataSources.Add(new ReportDataSource("Livestock_Auction_clsSettings", SettingsBinding));
                    rptReceipt.RefreshReport();
                }
            }
        }
    }
}
