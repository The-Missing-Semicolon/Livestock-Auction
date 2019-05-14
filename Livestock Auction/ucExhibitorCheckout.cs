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
    public partial class ucExhibitorCheckout : UserControl
    {
        DB.clsExhibitor CurrentExhibitor = null;

        public ucExhibitorCheckout()
        {
            InitializeComponent();
        }

        public void LoadExhibitor(DB.clsExhibitor Exhibitor)
        {
            CurrentExhibitor = Exhibitor;
            tabMain.SelectedTab = tabReceipt;
            if (tabMain.SelectedTab == tabReceipt && CurrentExhibitor != null)
            {
                clsExhibitorBindingSource.DataSource = CurrentExhibitor;
                clsPurchaseBindingSource.DataSource = CurrentExhibitor.Purchases;
                rptExhibitorReceipt.RefreshReport();
            }
        }

        private void tabMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabMain.SelectedTab == tabReceipt && CurrentExhibitor != null)
            {
                clsExhibitorBindingSource.DataSource = CurrentExhibitor;
                clsPurchaseBindingSource.DataSource = CurrentExhibitor.Purchases;
                rptExhibitorReceipt.RefreshReport();
            }
        }
    }
}
