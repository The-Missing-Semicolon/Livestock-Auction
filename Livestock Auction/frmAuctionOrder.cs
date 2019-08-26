using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Livestock_Auction
{
    public partial class frmAuctionOrder : Form
    {
        int m_iSelectedRevision = -1;

        List<DB.clsAuctionIndex> m_lstSelectedOrder;
        List<DB.clsExhibit> m_lstSelectedUnsorted;

        public frmAuctionOrder()
        {
            InitializeComponent();
        }

        private void frmAuctionOrder_Load(object sender, EventArgs e)
        {
            tsCmbOrderType.SelectedIndex = 0;
            m_iSelectedRevision = clsDB.Auction.LatestRevision;

            m_lstSelectedOrder = clsDB.Auction.LoadRevision(m_iSelectedRevision);
            m_lstSelectedUnsorted = clsDB.Auction.LoadUnsortedExhibits(m_iSelectedRevision);

            ReloadAuctionGrid();
            ReloadUnsortedGrid();
            ReloadRevisions();
            this.rptAuctionOrder.RefreshReport();
        }

        private void tabMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabMain.SelectedTab == tabpgReport)
            {
                this.clsAuctionIndexBindingSource.DataSource = m_lstSelectedOrder;
                this.rptAuctionOrder.RefreshReport();
            }
            else if (tabMain.SelectedTab == tabpgEdit)
            {
                ReloadAuctionGrid();
                ReloadUnsortedGrid();
                m_iSelectedRevision = clsDB.Auction.LatestRevision;
                ReloadRevisions();
            }
        }

        private void tscmdGenerateOrder_Click(object sender, EventArgs e)
        {
            switch(tsCmbOrderType.Text)
            {
                case "Cecil County Fair":
                    clsDB.Auction.GenerateAuctionOrder();
                    break;
                case "Solanco Fair":
                    clsDB.Auction.GenerateAuctionOrderSolanco();
                    break;
            }

            
            m_iSelectedRevision = clsDB.Auction.LatestRevision;
            m_lstSelectedOrder = clsDB.Auction.LoadRevision(m_iSelectedRevision);
            m_lstSelectedUnsorted = clsDB.Auction.LoadUnsortedExhibits(m_iSelectedRevision);

            ReloadAuctionGrid();
            ReloadUnsortedGrid();
            ReloadRevisions();
        }

        private void lsvHistory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lsvHistory.SelectedItems.Count > 0)
            {
                if (int.Parse(lsvHistory.SelectedItems[0].Text) != m_iSelectedRevision)
                {
                    m_iSelectedRevision = int.Parse(lsvHistory.SelectedItems[0].Text);
                    m_lstSelectedOrder = clsDB.Auction.LoadRevision(m_iSelectedRevision);
                    m_lstSelectedUnsorted =  clsDB.Auction.LoadUnsortedExhibits(m_iSelectedRevision);
                    if (tabMain.SelectedTab == tabpgEdit)
                    {
                        ReloadAuctionGrid();
                        ReloadRevisions();
                        ReloadUnsortedGrid();
                    }
                    else if (tabMain.SelectedTab == tabpgReport)
                    {
                        this.clsAuctionIndexBindingSource.DataSource = m_lstSelectedOrder;
                        this.rptAuctionOrder.RefreshReport();
                    }
                }
            }
        }

        private void tscmdRevert_Click(object sender, EventArgs e)
        {
            if (m_iSelectedRevision >= 0)
            {
                clsDB.Auction.RevertToRevision(m_iSelectedRevision);
                m_iSelectedRevision = clsDB.Auction.LatestRevision;
                m_lstSelectedOrder = clsDB.Auction.LoadRevision(m_iSelectedRevision);
                m_lstSelectedUnsorted = clsDB.Auction.LoadUnsortedExhibits(m_iSelectedRevision);

                ReloadAuctionGrid();
                ReloadUnsortedGrid();
                ReloadRevisions();
            }
        }

        private void lsvOrder_ItemDrag(object sender, ItemDragEventArgs e)
        {
            int iAuctionIndex = lsvOrder.FocusedItem.Index + 1;
            DB.clsAuctionIndex AuctionIndex = clsDB.Auction.Items[iAuctionIndex];
            lsvOrder.DoDragDrop(AuctionIndex, DragDropEffects.Move);
        }

        private void lsvOrder_DragEnter(object sender, DragEventArgs e)
        {
            if (lsvOrder.Items.Count > 0)
            {
                if (e.Data.GetDataPresent(typeof(DB.clsAuctionIndex)) || e.Data.GetDataPresent(typeof(DB.clsExhibit)))
                {
                    e.Effect = DragDropEffects.Move;
                }
            }
        }

        private void lsvOrder_DragLeave(object sender, EventArgs e)
        {
            //Hide the insertion marker
            lsvOrder.InsertionMark.Index = -1;
        }

        private void lsvOrder_DragOver(object sender, DragEventArgs e)
        {
            if ((e.Data.GetDataPresent(typeof(DB.clsAuctionIndex)) || e.Data.GetDataPresent(typeof(DB.clsExhibit))) && (m_iSelectedRevision == clsDB.Auction.LatestRevision))
            {
                Point cp = lsvOrder.PointToClient(new Point(e.X, e.Y));
                int iHoverIndex = lsvOrder.InsertionMark.NearestIndex(cp);

                if (iHoverIndex >= 0)
                {
                    int iMidPoint = lsvOrder.Items[iHoverIndex].Bounds.Top + (lsvOrder.Items[iHoverIndex].Bounds.Height / 2);
                    if (cp.Y > iMidPoint)
                    {
                        lsvOrder.InsertionMark.AppearsAfterItem = true;
                    }
                    else
                    {
                        lsvOrder.InsertionMark.AppearsAfterItem = false;
                    }
                    lsvOrder.InsertionMark.Index = iHoverIndex;
                    lsvOrder.Items[iHoverIndex].EnsureVisible();
                }
                e.Effect = DragDropEffects.Move;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void lsvOrder_DragDrop(object sender, DragEventArgs e)
        {
            //TODO: I probably broke drag & drop when I got rid of the tags. This should be tested. I replaced it with IndexOf.
            if (m_iSelectedRevision == clsDB.Auction.LatestRevision)
            {
                if (e.Data.GetDataPresent(typeof(DB.clsAuctionIndex)) || e.Data.GetDataPresent(typeof(DB.clsExhibit)))
                {
                    Point cp = lsvOrder.PointToClient(new Point(e.X, e.Y));
                    int iHoverIndex = lsvOrder.InsertionMark.NearestIndex(cp);
                    if (iHoverIndex >= 0)
                    {
                        int iMidPoint = lsvOrder.Items[iHoverIndex].Bounds.Top + (lsvOrder.Items[iHoverIndex].Bounds.Height / 2);
                        if (cp.Y > iMidPoint)
                        {
                            iHoverIndex += 1;   //Insert it after the current element
                        }

                        if (e.Data.GetDataPresent(typeof(DB.clsAuctionIndex)))
                        {
                            DB.clsAuctionIndex AuctionIndex = (DB.clsAuctionIndex)e.Data.GetData(typeof(DB.clsAuctionIndex));
                            clsDB.Auction.ChangeOrder(AuctionIndex, iHoverIndex + 1);
                        }
                        else if (e.Data.GetDataPresent(typeof(DB.clsExhibit)))
                        {
                            DB.clsExhibit Exhibit = (DB.clsExhibit)e.Data.GetData(typeof(DB.clsExhibit));
                            clsDB.Auction.InsertExhibit(iHoverIndex + 1, Exhibit);
                        }
                    }
                    else if (e.Data.GetDataPresent(typeof(DB.clsExhibit)))
                    {
                        DB.clsExhibit Exhibit = (DB.clsExhibit)e.Data.GetData(typeof(DB.clsExhibit));
                        clsDB.Auction.AppendExhibit(Exhibit);
                    }

                    m_iSelectedRevision = clsDB.Auction.LatestRevision;
                    m_lstSelectedOrder = clsDB.Auction.LoadRevision(m_iSelectedRevision);
                    m_lstSelectedUnsorted = clsDB.Auction.LoadUnsortedExhibits(m_iSelectedRevision);

                    ReloadAuctionGrid();
                    ReloadUnsortedGrid();
                    ReloadRevisions();
                }
            }
            else
            {
                MessageBox.Show("To modify the auction order, you must be working on the most recent revision. Please either select the latest revision, or revert the the selected revision", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void lsvUnsorted_ItemDrag(object sender, ItemDragEventArgs e)
        {
            DB.clsExhibit Exhibit = (DB.clsExhibit)lsvUnsorted.FocusedItem.Tag;
            Exhibit = clsDB.Exhibits[Exhibit];
            lsvOrder.DoDragDrop(Exhibit, DragDropEffects.Move);
        }

        private void lsvUnsorted_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(DB.clsAuctionIndex)))
            {
                e.Effect = DragDropEffects.Move;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void lsvUnsorted_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(DB.clsAuctionIndex)))
            {
                e.Effect = DragDropEffects.Move;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void lsvUnsorted_DragDrop(object sender, DragEventArgs e)
        {
            if (m_iSelectedRevision == clsDB.Auction.LatestRevision)
            {
                if (e.Data.GetDataPresent(typeof(DB.clsAuctionIndex)))
                {
                    DB.clsAuctionIndex AuctionIndex = (DB.clsAuctionIndex)e.Data.GetData(typeof(DB.clsAuctionIndex));
                    clsDB.Auction.RemoveExhibit(AuctionIndex);

                    m_iSelectedRevision = clsDB.Auction.LatestRevision;
                    m_lstSelectedOrder = clsDB.Auction.LoadRevision(m_iSelectedRevision);
                    m_lstSelectedUnsorted = clsDB.Auction.LoadUnsortedExhibits(m_iSelectedRevision);

                    ReloadAuctionGrid();
                    ReloadUnsortedGrid();                    
                    ReloadRevisions();
                }
            }
            else
            {
                MessageBox.Show("To modify the auction order, you must be working on the most recent revision. Please either select the latest revision, or revert the the selected revision", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }




        private void ReloadAuctionGrid()
        {
            if (m_iSelectedRevision >= 0)
            {
                lsvOrder.Items.Clear();
                foreach (DB.clsAuctionIndex Entry in m_lstSelectedOrder)
                {
                    ListViewItem NewItem = lsvOrder.Items.Add(string.Format("{0}.{1}.", Entry.AuctionIndex, Entry.SubIndex));
                    NewItem.Tag = Entry;

                    if (Entry.Exhibit != null)
                    {
                        NewItem.SubItems.Add(Entry.Exhibit.TagNumber.ToString());
                        NewItem.SubItems.Add(Entry.Exhibit.ExhibitorNumber.ToString());
                        NewItem.SubItems.Add(Entry.Exhibit.Exhibitor.Name.First + " " + Entry.Exhibit.Exhibitor.Name.Last);
                        NewItem.SubItems.Add(Entry.Exhibit.ChampionStatus);
                        NewItem.SubItems.Add(Entry.Exhibit.RateOfGainText);
                        NewItem.SubItems.Add(Entry.Exhibit.MarketItem.MarketType);
                        NewItem.SubItems.Add(Entry.Exhibit.Weight.ToString());
                    }
                    else
                    {
                        if (clsDB.Market[Entry.MarketType] != null)
                        {
                            NewItem.SubItems.Add(string.Format("ERROR: Failed to load {0} exhibit with tag {1}", clsDB.Market[Entry.MarketType].MarketType, Entry.ExhibitTag.ToString()));
                        }
                        else
                        {
                            NewItem.SubItems.Add(string.Format("ERROR: Failed to load {0} exhibit with tag {1}", Entry.MarketType.ToString(), Entry.ExhibitTag.ToString()));
                        }
                    }
                }
                lsvOrder.Sort();
            }
        }

        private void ReloadUnsortedGrid()
        {
            if (m_iSelectedRevision >= 0)
            {
                lsvUnsorted.Items.Clear();
                foreach (DB.clsExhibit Ex in m_lstSelectedUnsorted)
                {
                    ListViewItem NewItem = lsvUnsorted.Items.Add(Ex.TagNumber.ToString());
                    NewItem.Tag = Ex;
                    NewItem.SubItems.Add(Ex.ExhibitorNumber.ToString());
                    NewItem.SubItems.Add(Ex.Exhibitor.Name.First + " " + Ex.Exhibitor.Name.Last);
                    NewItem.SubItems.Add(Ex.ChampionStatus);
                    NewItem.SubItems.Add(Ex.RateOfGainText);
                    NewItem.SubItems.Add(Ex.MarketItem.MarketType);
                    NewItem.SubItems.Add(Ex.Weight.ToString());
                }
                lsvUnsorted.Sort();
            }
        }

        private void ReloadRevisions()
        {
            lsvHistory.Items.Clear();
            foreach (DB.clsAuctionRevision Rev in clsDB.Auction.RevisionHistory.Values)
            {
                if (!Rev.Reverted)   //For the time being, only show non-reverted records
                {
                    ListViewItem lviItem = lsvHistory.Items.Add(Rev.RevisionIndex.ToString());
                    lviItem.SubItems.Add(Rev.RevisionDate.ToString());
                    lviItem.SubItems.Add(Rev.RecordsAffected.ToString());
                    if (Rev.Reverted)
                    {
                        lviItem.SubItems.Add("Yes");
                        lviItem.ForeColor = Color.LightGray;
                    }
                    else
                    {
                        lviItem.SubItems.Add("No");
                        lviItem.ForeColor = SystemColors.ControlText;
                    }
                    if (Rev.RevisionIndex == m_iSelectedRevision)
                    {
                        lviItem.Selected = true;
                        lviItem.Focused = true;
                    }
                }
            }
        }
    }
}
