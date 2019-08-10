using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Livestock_Auction
{
    public partial class frmSettings : Form
    {
        public frmSettings()
        {
            InitializeComponent();
            txtEventName.Text = clsDB.Settings.EventName;
            txtFairName.Text = clsDB.Settings.FairName;
            txtAddress.Text = clsDB.Settings.FairAddress;
            txtCity.Text = clsDB.Settings.FairCity;
            txtState.Text = clsDB.Settings.FairState;
            txtZipcode.Text = clsDB.Settings.FairZipCode.ToString();
            txtPhone.Text = clsDB.Settings.FairPhone;

            txtYear.Text = clsDB.Settings.FairYear.ToString();
            txtFairFee.Text = String.Format("{0:0.00}", clsDB.Settings.FairFee*100);
            txtCreditCardFee.Text = String.Format("{0:0.00}", clsDB.Settings.CreditCardFee*100);
        }

        private void CmdSave_Click(object sender, EventArgs e)
        {
            if (txtEventName.TextLength > 0) { clsDB.Settings.EventName = txtEventName.Text.Trim(); }
            if (txtFairName.TextLength > 0) { clsDB.Settings.FairName = txtFairName.Text.Trim(); }
            if (txtAddress.TextLength > 0) { clsDB.Settings.FairAddress = txtAddress.Text.Trim(); }
            if (txtCity.TextLength > 0) { clsDB.Settings.FairCity = txtCity.Text.Trim(); }
            if (txtState.TextLength > 0) { clsDB.Settings.FairState = txtState.Text.Trim(); }
            if (txtZipcode.TextLength > 0) { if (Int32.TryParse(txtZipcode.Text, out int test)) { clsDB.Settings.FairZipCode = test; } }
            if (txtPhone.TextLength > 0) { clsDB.Settings.FairPhone = txtPhone.Text.Trim(); }

            if (txtYear.TextLength > 0) { if (Int32.TryParse(txtYear.Text, out int test)) { clsDB.Settings.FairYear = test; } }
            if (txtFairFee.TextLength > 0) { if (Double.TryParse(txtFairFee.Text, out double test)) { clsDB.Settings.FairFee = test / 100.0; } }
            if (txtCreditCardFee.TextLength > 0) { if (Double.TryParse(txtCreditCardFee.Text, out double test)) { clsDB.Settings.CreditCardFee = test / 100.0; } }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void CmdCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void FrmSettings_Load(object sender, EventArgs e)
        {

        }
    }
}
