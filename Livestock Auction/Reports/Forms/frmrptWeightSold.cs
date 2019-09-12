using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Livestock_Auction.Reports.Forms
{
    public partial class frmrptWeightSold : Form
    {
        public frmrptWeightSold()
        {
            InitializeComponent();
        }

        private void frmrptWeightSold_Load(object sender, EventArgs e)
        {
            clsExhibitBindingSource.DataSource = clsDB.Exhibits;
            clsSettingsBindingSource.DataSource = clsDB.Settings;
            this.reportViewer1.RefreshReport();
        }
    }
}
