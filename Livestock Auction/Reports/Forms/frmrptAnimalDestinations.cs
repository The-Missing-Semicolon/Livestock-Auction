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
    public partial class frmrptAnimalDestinations : Form
    {
        public frmrptAnimalDestinations()
        {
            InitializeComponent();
        }

        private void frmrptAnimalDestinations_Load(object sender, EventArgs e)
        {
            clsExhibitBindingSource.DataSource = clsDB.Exhibits;
            this.rptAnimalDestinations.RefreshReport();
        }
    }
}
