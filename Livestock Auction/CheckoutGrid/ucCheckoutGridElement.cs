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
    public partial class ucCheckoutGridElement : UserControl
    {
        protected TableLayoutPanel tlpMainPanel = null;
        public ucCheckoutGridElement()
        {
            InitializeComponent();
        }


        public float GetColumnWidth(int ColumnIndex)
        {
            if (tlpMainPanel != null)
            {
                return tlpMainPanel.ColumnStyles[ColumnIndex].Width;
            }
            else
            {
                return 0;
            }
        }

        public void SetColumnWidth(int ColumnIndex, float Value)
        {
            if (tlpMainPanel != null)
            {
                tlpMainPanel.ColumnStyles[ColumnIndex].Width = Value;
            }
        }
    }
}
