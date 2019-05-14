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
    public partial class ucCheckoutFooter : ucCheckoutGridElement
    {
        public ucCheckoutFooter()
        {
            InitializeComponent();
            tlpMainPanel = tlpMain;
        }
    }
}
