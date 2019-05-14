using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Livestock_Auction.Helpers
{
    /*  Label control that is transparent to events.
     *  They simply get passed to the parent container of the label.
    */

    public class TransparentLabel : Label
    {
        protected override void WndProc(ref Message m)
        {
            //Overridden to pass events through to the parent container. Design mode is
            //  detected so the the labels can still be manipulated by the form designer.
            if (!this.DesignMode)
            {
                const int WM_NCHITTEST = 0x0084;
                const int HTTRANSPARENT = (-1);

                if (m.Msg == WM_NCHITTEST)
                {
                    m.Result = (IntPtr)HTTRANSPARENT;
                }
                else
                {
                    base.WndProc(ref m);
                }
            }
            else
            {
                base.WndProc(ref m);
            }
        }
    }

    /*  Panel control that is transparent to events.
     *  They simply get passed to the parent container of the panel.
    */
    class TransparentPanel : Panel
    {
        protected override void WndProc(ref Message m)
        {
            //Overridden to pass events through to the parent container. Design mode is
            //  detected so the the labels can still be manipulated by the form designer.
            if (!this.DesignMode)
            {
                const int WM_NCHITTEST = 0x0084;
                const int HTTRANSPARENT = (-1);

                if (m.Msg == WM_NCHITTEST)
                {
                    m.Result = (IntPtr)HTTRANSPARENT;
                }
                else
                {
                    base.WndProc(ref m);
                }
            }
            else
            {
                base.WndProc(ref m);
            }
        }
    }
}
