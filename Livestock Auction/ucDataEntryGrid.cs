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
    public partial class ucDataEntryGrid : UserControl
    {
        private struct Column
        {
            public Column(string HeaderText)
            {
                Text = HeaderText;
            }

            public string Text;
        }

        List<Column> lstColumns = new List<Column>();

        public ucDataEntryGrid()
        {
            InitializeComponent();

            lstColumns.Add(new Column("Ex#"));
            lstColumns.Add(new Column("Last Name"));
            lstColumns.Add(new Column("First Name"));
            lstColumns.Add(new Column("Street Address"));
            lstColumns.Add(new Column("City"));
            lstColumns.Add(new Column("State"));
            lstColumns.Add(new Column("Zip"));
        }

        private void AddColumn(string ColName)
        {
            lstColumns.Add(new Column("Zip"));
        }

        private void CreateNewRecord()
        {

        }



        private void tscmdNewRecord_Click(object sender, EventArgs e)
        {
            
        }





    }
}
