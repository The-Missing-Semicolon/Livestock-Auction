using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Livestock_Auction.Reports.Forms
{
    public partial class frmrptReceipts : Form
    {
        int m_iCurExhibitorIndex = 0;
        int m_iCurBuyerIndex = 0;
        List<int> m_lstiExhibitorKeys = null;
        List<int> m_lstiBuyerKeys = null;

        public frmrptReceipts()
        {
            InitializeComponent();
            m_lstiExhibitorKeys = new List<int>();
            m_lstiBuyerKeys = new List<int>();

            foreach (DB.clsExhibitor Exhibitor in clsDB.Exhibitors)
            {
                if (Exhibitor.Exhibits.Count > 0)
                {
                    m_lstiExhibitorKeys.Add(Exhibitor.ExhibitorNumber);
                }
            }
            foreach (DB.clsBuyer Buyer in clsDB.Buyers)
            {
                if (clsDB.Purchases.GetPurchasesByBuyer(Buyer.BuyerNumber).Count > 0)
                {
                    m_lstiBuyerKeys.Add(Buyer.BuyerNumber);
                }
                
            }
            UpdateStatusBar();
        }

        private void frmrptExhibiorReceipts_Load(object sender, EventArgs e)
        {
            if (tabMain.SelectedTab == tabPageExhibitors)
            {
                if (m_lstiExhibitorKeys.Count > 0)
                { 
                    RefreshReport(m_lstiExhibitorKeys[m_iCurExhibitorIndex]);
                }
                else
                {
                    MessageBox.Show("No Exhibitors Registered", "Receipts");
                }
            }
            else if (tabMain.SelectedTab == tabPageBuyers)
            {
                if (m_lstiBuyerKeys.Count > 0)
                {
                    RefreshReport(m_lstiBuyerKeys[m_iCurBuyerIndex]);
                }
                else
                {
                    MessageBox.Show("No Buyers Registered", "Receipts");
                }
            }
        }

        private void tscmdBack_Click(object sender, EventArgs e)
        {
            if (tabMain.SelectedTab == tabPageExhibitors && m_iCurExhibitorIndex > 0)
            {
                m_iCurExhibitorIndex--;
                RefreshReport(m_lstiExhibitorKeys[m_iCurExhibitorIndex]);
            }
            else if (tabMain.SelectedTab == tabPageBuyers && m_iCurBuyerIndex > 0)
            {
                m_iCurBuyerIndex--;
                RefreshReport(m_lstiBuyerKeys[m_iCurBuyerIndex]);
            }
            UpdateStatusBar();
        }

        private void tscmdNext_Click(object sender, EventArgs e)
        {
            if (tabMain.SelectedTab == tabPageExhibitors && m_iCurExhibitorIndex < m_lstiExhibitorKeys.Count - 1)
            {
                m_iCurExhibitorIndex++;
                RefreshReport(m_lstiExhibitorKeys[m_iCurExhibitorIndex]);
            }
            else if (tabMain.SelectedTab == tabPageBuyers && m_iCurBuyerIndex < m_lstiBuyerKeys.Count - 1)
            {
                m_iCurBuyerIndex++;
                RefreshReport(m_lstiBuyerKeys[m_iCurBuyerIndex]);
            }
            UpdateStatusBar();
        }
        
        private void tscmdSaveAll_Click(object sender, EventArgs e)
        {
            if (dlgFileSave.ShowDialog() == DialogResult.OK)
            {
                // This code is heavily borrowed from James Johnson's answer to the thread "Merging Reports Generated in Pdf
                //  format by using SSRS and ASP.NET" on Stack Overflow
                // http://stackoverflow.com/questions/7691204/merging-reports-generated-in-pdf-format-by-using-ssrs-and-asp-net
                int iSourceReceiptIndex = 0;
                Byte[] ReportBytes = null;
                if (tabMain.SelectedTab == tabPageExhibitors)
                {
                    ReportBytes = RenderReport(rptExhibitorReceipt, m_lstiExhibitorKeys[iSourceReceiptIndex]);
                }
                else if (tabMain.SelectedTab == tabPageBuyers)
                {
                    ReportBytes = RenderReport(rptBuyerReceipt, m_lstiBuyerKeys[iSourceReceiptIndex]);
                }

                iTextSharp.text.pdf.PdfReader reader = new iTextSharp.text.pdf.PdfReader(ReportBytes);

                iTextSharp.text.Document outDoc = new iTextSharp.text.Document(reader.GetPageSizeWithRotation(1));
                iTextSharp.text.pdf.PdfWriter writer = iTextSharp.text.pdf.PdfWriter.GetInstance(outDoc, dlgFileSave.OpenFile());
                outDoc.Open();

                iTextSharp.text.pdf.PdfImportedPage page;
                iTextSharp.text.pdf.PdfContentByte contentByte = writer.DirectContent;

                while ((tabMain.SelectedTab == tabPageExhibitors && iSourceReceiptIndex < m_lstiExhibitorKeys.Count) || (tabMain.SelectedTab == tabPageBuyers && iSourceReceiptIndex < m_lstiBuyerKeys.Count))
                {
                    int pageIndex = 0;
                    while (pageIndex < reader.NumberOfPages)
                    {
                        pageIndex++;
                        outDoc.SetPageSize(reader.GetPageSizeWithRotation(pageIndex));
                        outDoc.NewPage();

                        page = writer.GetImportedPage(reader, pageIndex);

                        contentByte.AddTemplate(page, 1f, 0, 0, 1f, 0, 0);
                    }
                    iSourceReceiptIndex++;

                    if ((tabMain.SelectedTab == tabPageExhibitors && iSourceReceiptIndex < m_lstiExhibitorKeys.Count) || (tabMain.SelectedTab == tabPageBuyers && iSourceReceiptIndex < m_lstiBuyerKeys.Count))
                    {
                        if (tabMain.SelectedTab == tabPageExhibitors)
                        {
                            ReportBytes = RenderReport(rptExhibitorReceipt, m_lstiExhibitorKeys[iSourceReceiptIndex]);
                        }
                        else if (tabMain.SelectedTab == tabPageBuyers)
                        {
                            ReportBytes = RenderReport(rptBuyerReceipt, m_lstiBuyerKeys[iSourceReceiptIndex]);
                        }
                        reader = new iTextSharp.text.pdf.PdfReader(ReportBytes);
                    }
                }

                outDoc.Close();
            }
        }


        private void tstxtGoTo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Goto();
            }
        }

        private void tscmdGoto_Click(object sender, EventArgs e)
        {
            Goto();
        }

        private void Goto()
        {
            int iReceiptNumber = 0;
            int iNewIndex = -1;
            if (int.TryParse(tstxtGoTo.Text.Trim(), out iReceiptNumber))
            {
                if (tabMain.SelectedTab == tabPageExhibitors)
                {
                    if (clsDB.Exhibitors[iReceiptNumber] != null)
                    {
                        iNewIndex = m_lstiExhibitorKeys.IndexOf(iReceiptNumber);
                        if (iNewIndex < 0)
                        {
                            MessageBox.Show(string.Format("Exhibitor {0} has no exhibits for sale.", iReceiptNumber.ToString()), "Exhibitor", MessageBoxButtons.OK);
                        }
                        else
                        {
                            m_iCurExhibitorIndex = iNewIndex;
                            UpdateStatusBar();
                        }
                        RefreshReport(iReceiptNumber);
                    }
                    else
                    {
                        MessageBox.Show(string.Format("Exhibitor {0} was not found.", iReceiptNumber.ToString()), "Exhibitor", MessageBoxButtons.OK);
                    }
                }
                if (tabMain.SelectedTab == tabPageBuyers)
                {
                    if (clsDB.Buyers[iReceiptNumber] != null)
                    {
                        iNewIndex = m_lstiBuyerKeys.IndexOf(iReceiptNumber);
                        if (iNewIndex < 0)
                        {
                            MessageBox.Show(string.Format("Buyer {0} has made no purchases for sale.", iReceiptNumber.ToString()), "Buyer", MessageBoxButtons.OK);
                        }
                        else
                        {
                            m_iCurBuyerIndex = iNewIndex;
                            UpdateStatusBar();
                        }
                        RefreshReport(iReceiptNumber);
                    }
                }
            }
            else
            {
                MessageBox.Show(string.Format("'{0}' is not a valid number.", tstxtGoTo.Text.Trim()), "Receipt Number", MessageBoxButtons.OK);
            }
        }

        private void UpdateStatusBar()
        {
            if (tabMain.SelectedTab == tabPageExhibitors)
            {
                tslblReceiptStatus.Text = string.Format("Exhibitor {0} of {1}", (m_iCurExhibitorIndex + 1).ToString(), m_lstiExhibitorKeys.Count);
            }
            else if (tabMain.SelectedTab == tabPageBuyers)
            {
                tslblReceiptStatus.Text = string.Format("Buyer {0} of {1}", (m_iCurBuyerIndex + 1).ToString(), m_lstiBuyerKeys.Count);
            }
        }

        private void RefreshReport(int iReceiptNumber)
        {
            if (tabMain.SelectedTab == tabPageExhibitors)
            {
                clsExhibitorBindingSource.DataSource = clsDB.Exhibitors[iReceiptNumber];
                clsPurchaseBindingSource.DataSource = clsDB.Exhibitors[iReceiptNumber].Purchases;
                this.rptExhibitorReceipt.RefreshReport();
            }
            else if (tabMain.SelectedTab == tabPageBuyers)
            {
                clsBuyerBindingSource.DataSource = clsDB.Buyers[iReceiptNumber];
                clsPurchaseBindingSource.DataSource = clsDB.Purchases.GetPurchasesByBuyer(iReceiptNumber);
                if (clsDB.Payments[iReceiptNumber] != null)
                {
                    clsPaymentBindingSource.DataSource = clsDB.Payments[iReceiptNumber].Values;
                }
                else
                {
                    clsPaymentBindingSource.DataSource = null;
                }
                clsSettingsBindingSource.DataSource = clsDB.Settings;
                this.rptBuyerReceipt.RefreshReport();
            }
        }

        private Byte[] RenderReport(Microsoft.Reporting.WinForms.ReportViewer ReceiptReport, int iReceiptNumber)
        {
            Microsoft.Reporting.WinForms.Warning[] rptWarnings;
            string[] srptStreamIDs;
            string srptMimeType;
            string srptEncoding;
            string srptExtensions;

            RefreshReport(iReceiptNumber);
            return ReceiptReport.LocalReport.Render("PDF", null, out srptMimeType, out srptEncoding, out srptExtensions, out srptStreamIDs, out rptWarnings);
        }

        private void tabMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabMain.SelectedTab == tabPageExhibitors)
            {
                tslblPersonNumber.Text = "Exhibitor Number:";
                if (m_lstiExhibitorKeys.Count > 0)
                {
                    RefreshReport(m_lstiExhibitorKeys[m_iCurExhibitorIndex]);
                }
                else
                {
                    MessageBox.Show("No exhibitors are registered", "Receipts");
                }
                
            }
            else if (tabMain.SelectedTab == tabPageBuyers)
            {
                tslblPersonNumber.Text = "Buyer Number:";
                if (m_lstiBuyerKeys.Count > 0)
                {
                    RefreshReport(m_lstiBuyerKeys[m_iCurBuyerIndex]);
                }
                else
                {
                    MessageBox.Show("No buyers are registered", "Receipts");
                }
            }
            UpdateStatusBar();
        }
    }
}
