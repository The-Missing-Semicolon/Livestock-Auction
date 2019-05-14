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
    public partial class frmBrowseName : Form
    {
        public static DB.Types.Name BrowseNames(string FirstName, string LastName)
        {
            frmBrowseName dlgName = new frmBrowseName(FirstName, LastName);
            dlgName.ShowDialog();
            return dlgName.SelectedName;
        }


        private DB.Types.Name SelectedName = null;

        public frmBrowseName()
        {
            InitializeComponent();
        }

        public frmBrowseName(string FirstName, string LastName)
        {
            InitializeComponent();
            Search(FirstName, LastName);
        }

        private void lsvNames_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //Point ptClient = lsvNames.PointToClient(e.Location);
            Point ptClient = e.Location;
            ListViewItem lviSelected = lsvNames.GetItemAt(ptClient.X, ptClient.Y);
            if (lviSelected != null)
            {
                SelectedName = (DB.Types.Name)lviSelected.Tag;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void frmBrowseName_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void Search(string FirstName, string LastName)
        {
            foreach (DB.clsExhibitor Exhibitor in clsDB.Exhibitors)
            {
                if ((FirstName.Length > 0 && Exhibitor.Name.First.Contains(FirstName)) || LastName.Length > 0 && Exhibitor.Name.Last.Contains(LastName))
                {
                    ListViewItem lviName = lsvNames.Items.Add(Exhibitor.Name.First);
                    lviName.Tag = Exhibitor.Name;
                    lviName.SubItems.Add("\"" + Exhibitor.Name.Nick + "\"");
                    lviName.SubItems.Add(Exhibitor.Name.Last);
                }
            }
        }
    }
}
