using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Collections;
using System.Runtime.Serialization;
using System.Data;

namespace Livestock_Auction.DB
{
    public class ExhibitKey : AuctionDataKey
    {
        int m_iMarketItem;
        int m_iTagNumber;

        public ExhibitKey()
        {

        }

        public ExhibitKey(int MarketKey, int TagNumber)
        {
            m_iMarketItem = MarketKey;
            m_iTagNumber = TagNumber;
        }

        public override void Load(IDataReader dbReader)
        {
            m_iMarketItem = (Int32)dbReader["MarketItem"];
            m_iTagNumber = (Int32)dbReader["TagNumber"];
        }

        public override int GetHashCode()
        {
            return m_iMarketItem * 100 + m_iTagNumber;
        }

        public override bool Equals(object obj)
        {
            ExhibitKey Key = (ExhibitKey)obj;
            if (obj.GetType() == this.GetType())
            {
                return (this.m_iMarketItem == Key.m_iMarketItem) && (this.m_iTagNumber == Key.m_iTagNumber);
            }
            else
            {
                return false;
            }
        }
    }

    public class clsExhibits : clsAuctionDataCollection<ExhibitKey, clsExhibit, DB.Setup.Exhibits>, IEnumerable<clsExhibit>
    {
        public clsExhibits(IDbConnection Connection)
            : base(Connection)
        {
        }

        public override void ConnectEvents()
        {
            foreach (DB.clsExhibit Exhibit in this)
            {
                Exhibit.RefreshListItem();
            }

            clsDB.Exhibitors.Updated += new EventHandler<DatabaseUpdatedEventArgs>(Exhibitors_Updated);
            clsDB.Market.Updated += new EventHandler<DatabaseUpdatedEventArgs>(Market_Updated);
            clsDB.Purchases.Updated += new EventHandler<DatabaseUpdatedEventArgs>(Purchases_Updated);
            clsDB.Buyers.Updated += new EventHandler<DatabaseUpdatedEventArgs>(Buyers_Updated);
        }

        //Updated Exhibitors handler, keeps exhbitor's name up-to-date
        void Exhibitors_Updated(object sender, DatabaseUpdatedEventArgs e)
        {
            foreach (DB.clsExhibitor Exhibitor in e.UpdatedItems.Keys)
            {
                foreach (DB.clsExhibit Exhibit in Exhibitor.Exhibits)
                {
                    if (clsDB.Exhibits[Exhibit].ListView == null)
                    {
                        clsDB.Exhibits[Exhibit].RefreshExhibitor();
                    }
                    else
                    {
                        clsDB.Exhibits[Exhibit].ListView.Invoke(new System.Windows.Forms.MethodInvoker(clsDB.Exhibits[Exhibit].RefreshExhibitor));
                    }
                }
            }
        }

        //Updated MarketItem handler, keeps market fields (item and value) up-to-date
        void Market_Updated(object sender, DatabaseUpdatedEventArgs e)
        {
            foreach (DB.clsMarketItem MarketItem in e.UpdatedItems.Keys)
            {
                foreach (DB.clsExhibit Exhibit in clsDB.Exhibits)
                {
                    if (Exhibit.MarketItem == MarketItem)
                    {
                        if (Exhibit.ListView == null)
                        {
                            Exhibit.RefreshMarketItem();
                        }
                        else
                        {
                            Exhibit.ListView.Invoke(new System.Windows.Forms.MethodInvoker(Exhibit.RefreshMarketItem));
                        }
                    }    
                }
            }
        }

        //Updated Purchases handler, purchase information up-to-date
        void Purchases_Updated(object sender, DatabaseUpdatedEventArgs e)
        {
            foreach (DB.clsPurchase Purchase in e.UpdatedItems.Keys)
            {
                if (clsDB.Purchases[Purchase] != null && clsDB.Purchases[Purchase].Exhibit != null)
                {
                    if (clsDB.Purchases[Purchase].Exhibit.ListView == null)
                    {
                        clsDB.Purchases[Purchase].Exhibit.RefreshPurchase();
                    }
                    else
                    {
                        clsDB.Purchases[Purchase].Exhibit.ListView.Invoke(new System.Windows.Forms.MethodInvoker(clsDB.Purchases[Purchase].Exhibit.RefreshPurchase));
                    }
                }
            }
        }

        //Updated buyers handler, keeps purchase buyer information up-to-date
        void Buyers_Updated(object sender, DatabaseUpdatedEventArgs e)
        {
            foreach (DB.clsBuyer Buyer in e.UpdatedItems.Keys)
            {
                if (clsDB.Purchases != null)
                {
                    foreach (DB.clsPurchase Purchase in clsDB.Purchases.GetPurchasesByBuyer(Buyer.BuyerNumber))
                        if (clsDB.Purchases[Purchase] != null && clsDB.Purchases[Purchase].Exhibit != null)
                    {
                        if (clsDB.Purchases[Purchase].Exhibit.ListView == null)
                        {
                            clsDB.Purchases[Purchase].Exhibit.RefreshPurchase();
                        }
                        else
                        {
                            clsDB.Purchases[Purchase].Exhibit.ListView.Invoke(new System.Windows.Forms.MethodInvoker(clsDB.Purchases[Purchase].Exhibit.RefreshPurchase));
                        }
                    }
                }
            }
        }

        public int NewPurchaseIndex(clsExhibit Exhibit)
        {
            int PurchaseIndex = 0;

            //Query the database for the next purchase index
            IDbCommand dbLoad = m_dbConn.CreateCommand();
            dbLoad.CommandText = "SELECT Max(PurchaseIndex) + 1 FROM vPurchases_Current WHERE ExhibitTag=@ExhibitTag and ExhibitItem=@ExhibitItem";
            IDbDataParameter param = dbLoad.CreateParameter();
            param.ParameterName = "@ExhibitTag";
            param.Value = Exhibit.TagNumber;
            dbLoad.Parameters.Add(param);
            param = dbLoad.CreateParameter();
            param.ParameterName = "@ExhibitItem";
            param.Value = Exhibit.MarketItem.MarketID;
            dbLoad.Parameters.Add(param);

            object objIndex = dbLoad.ExecuteScalar();
            if (objIndex == null || objIndex == DBNull.Value)
            {
                PurchaseIndex = 0;
            }
            else
            {
                PurchaseIndex = (int)objIndex;
            }

            //TODO: Check for uncommited purchases...
            return PurchaseIndex;
        }

        public List<clsExhibit> FindByExhibitor(int iExhibitorNumber)
        {
            List<clsExhibit> ReturnValue = new List<clsExhibit>();
            lock (clsDB.Exhibits)
            {
                foreach (clsExhibit Exhibit in clsDB.Exhibits)
                {
                    if (Exhibit.ExhibitorNumber == iExhibitorNumber)
                    {
                        ReturnValue.Add(Exhibit);
                    }
                }
            }
            return ReturnValue;
        }

        public clsExhibit this[clsExhibit Exhibit]
        {
            get
            {
                return this[Exhibit.TagNumber, Exhibit.MarketID];
            }
        }

        public clsExhibit this[int TagNumber, int MarketID]
        {
            get
            {
                ExhibitKey Key = new ExhibitKey(MarketID, TagNumber);
                if (m_dictCollection.ContainsKey(Key))
                {
                    return m_dictCollection[Key];
                }
                else
                {
                    return null;
                }
            }
        }

        public void ExportFairToWorkbook(OfficeOpenXml.ExcelPackage outputPackage)
        {
            OfficeOpenXml.ExcelWorksheet sheetFair = outputPackage.Workbook.Worksheets.Add("Fair");

            //Setup the headers
            sheetFair.Row(1).Style.Font.Bold = true;

            sheetFair.Cells["A1"].Value = "Tag Number";
            sheetFair.Cells["B1"].Value = "Market Item";
            sheetFair.Cells["C1"].Value = "Weight";
            sheetFair.Cells["D1"].Value = "Buyer";
            sheetFair.Cells["E1"].Value = "Phone Number";

            int iCurRow = 2;
            foreach (clsExhibit Exhibit in this)
            {
                if (Exhibit.Destination == clsExhibit.AnimalDestination.Fair)
                {
                    sheetFair.SetValue(iCurRow, 1, Exhibit.TagNumber);
                    sheetFair.SetValue(iCurRow, 2, Exhibit.MarketItem.MarketType);
                    sheetFair.SetValue(iCurRow, 3, Exhibit.Weight);
                    sheetFair.Cells[string.Format("C{0}", iCurRow)].Style.Numberformat.Format = "#### \"" + Exhibit.MarketItem.MarketUnits + "\"";

                    if (Exhibit.Purchases != null && Exhibit.Purchases.Count > 0)
                    {
                        clsBuyer Buyer = Exhibit.Purchases[0].Buyer;
                        if (Buyer.CompanyName.Length > 0 && Buyer.Name.ToString().Trim().Length > 0)
                        {
                            sheetFair.SetValue(iCurRow, 4, Buyer.Name.ToString() + " from " + Buyer.CompanyName);
                        }
                        else if (Buyer.CompanyName.Length > 0)
                        {
                            sheetFair.SetValue(iCurRow, 4, Buyer.CompanyName);
                        }
                        else if (Buyer.Name.ToString().Trim().Length > 0)
                        {
                            sheetFair.SetValue(iCurRow, 4, Buyer.Name.ToString());
                        }
                        sheetFair.SetValue(iCurRow, 5, Buyer.PhoneNumber.ToString());
                    }
                    iCurRow++;
                }
            }

            sheetFair.Column(1).AutoFit();
            sheetFair.Column(2).AutoFit();
            sheetFair.Column(3).AutoFit();
            sheetFair.Column(4).AutoFit();
            sheetFair.Column(5).AutoFit();
        }

        public void ExportNewHollandToWorkbook(OfficeOpenXml.ExcelPackage outputPackage)
        {
            OfficeOpenXml.ExcelWorksheet sheetNewHolland = outputPackage.Workbook.Worksheets.Add("New Holland");

            //Setup the headers
            sheetNewHolland.Row(1).Style.Font.Bold = true;

            sheetNewHolland.Cells["A1"].Value = "Tag Number";
            sheetNewHolland.Cells["B1"].Value = "Market Item";
            sheetNewHolland.Cells["C1"].Value = "Weight";
            sheetNewHolland.Cells["D1"].Value = "Market Price";
            sheetNewHolland.Cells["E1"].Value = "Total";

            int iCurRow = 2;
            foreach (clsExhibit Exhibit in this)
            {
                if (Exhibit.Destination == clsExhibit.AnimalDestination.New_Holland)
                {
                    sheetNewHolland.SetValue(iCurRow, 1, Exhibit.TagNumber);
                    sheetNewHolland.SetValue(iCurRow, 2, Exhibit.MarketItem.MarketType);
                    sheetNewHolland.SetValue(iCurRow, 3, Exhibit.Weight);
                    sheetNewHolland.Cells[string.Format("C{0}", iCurRow)].Style.Numberformat.Format = "#### \"" + Exhibit.MarketItem.MarketUnits + "\"";
                    sheetNewHolland.SetValue(iCurRow, 4, Exhibit.MarketItem.MarketValue);
                    sheetNewHolland.SetValue(iCurRow, 5, (Exhibit.MarketItem.MarketValue * Exhibit.Weight));
                    iCurRow++;
                }
            }

            if (iCurRow > 2)
            {
                sheetNewHolland.Cells[string.Format("E{0}", iCurRow)].Formula = string.Format("SUM(E2:E{0})", iCurRow - 1);
                iCurRow--;
                sheetNewHolland.Cells[string.Format("D2:D{0}", iCurRow)].Style.Numberformat.Format = "$0.00";
                sheetNewHolland.Cells[string.Format("E2:E{0}", iCurRow + 1)].Style.Numberformat.Format = "$#,##0.00";
            }
            sheetNewHolland.Column(1).AutoFit();
            sheetNewHolland.Column(2).AutoFit();
            sheetNewHolland.Column(3).AutoFit();
            sheetNewHolland.Column(4).AutoFit();
            sheetNewHolland.Column(5).AutoFit();
        }
    }

    public class clsExhibitsEnumerator : IEnumerator<clsExhibit>
    {
        Dictionary<int, Dictionary<int, clsExhibit>> dictExhibits;
        int iCurTagIndex = 0;
        int iCurMarketIndex = -1;

        public clsExhibitsEnumerator(Dictionary<int, Dictionary<int, clsExhibit>> Exhibits)
        {
            dictExhibits = Exhibits;
        }


        #region IEnumerator<clsExhibit> Members

        public clsExhibit Current
        {
            get
            {
                return dictExhibits.Values.ToArray()[iCurTagIndex].Values.ToArray()[iCurMarketIndex];
            }
        }

        public void Dispose()
        {
            
        }

        object System.Collections.IEnumerator.Current
        {
            get
            {
                return dictExhibits.Values.ToArray()[iCurTagIndex].Values.ToArray()[iCurMarketIndex];
            }
        }

        public bool MoveNext()
        {
            iCurMarketIndex++;
            if (dictExhibits.Keys.Count > iCurTagIndex)
            {
                if (iCurMarketIndex >= dictExhibits[dictExhibits.Keys.ToArray()[iCurTagIndex]].Keys.Count)
                {
                    iCurMarketIndex = 0;
                    iCurTagIndex++;
                }

                if (iCurTagIndex >= dictExhibits.Keys.Count)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        public void Reset()
        {
            iCurTagIndex = 0;
            iCurMarketIndex = -1;
        }

        #endregion
    }

    [Serializable]
    public class clsExhibit : AuctionData, IComparable<clsExhibit>, ISerializable
    {
        // Total count of the number of columns in the list view for this auction data class
        const int TOTAL_LV_COLUMNS = 11;
        // Enumeration used to identify columns
        public enum ExhibitColumns
        {
            TagNumber = 0,
            Exhibitor = 1,
            Item = 2, 
            Value = 3,
            ChampionStatus = 4,
            RateOfGain = 5,
            Weight = 6,
            TakeBack = 7,
            Include = 8,
            Purchases = 9,
            Disposition = 10
        }

        public class ExhibitViewSorter : IComparer
        {
            ExhibitColumns[] eSortColumns = new ExhibitColumns[TOTAL_LV_COLUMNS];
            SortOrder[] eSortOrders = new SortOrder[TOTAL_LV_COLUMNS];

            public ExhibitViewSorter()
            {
                //Initalize the current sort state so all of the columns are
                //  sorted ascending in the order they appear on the screen
                //  (this will put buyer number first).
                for (int i = 0; i < TOTAL_LV_COLUMNS; i++)
                {
                    eSortColumns[i] = (ExhibitColumns)i;
                    eSortOrders[i] = SortOrder.Ascending;
                }
            }

            public void SetSortColumn(ExhibitColumns Column)
            {
                if (eSortColumns[0] == Column)
                {
                    //If the user just re-sorted on the same column, reverse the order
                    if (eSortOrders[0] == SortOrder.Ascending)
                    {
                        eSortOrders[0] = SortOrder.Descending;
                    }
                    else
                    {
                        eSortOrders[0] = SortOrder.Ascending;
                    }
                }
                else
                {
                    //Find the new item's existing place in the list
                    int iOldPos;
                    for (iOldPos = 0; iOldPos < TOTAL_LV_COLUMNS && eSortColumns[iOldPos] != Column; iOldPos++) { }

                    //Move all of the existing sort above the new one down one to make room
                    for (int i = iOldPos - 1; i >= 0; i--)
                    {
                        eSortColumns[i + 1] = eSortColumns[i];
                        eSortOrders[i + 1] = eSortOrders[i];
                    }

                    //Put the new sort at the top of the list
                    eSortColumns[0] = Column;
                    eSortOrders[0] = SortOrder.Ascending;
                }
            }

            public int Compare(object x, object y)
            {
                for (int i = 0; i < TOTAL_LV_COLUMNS; i++)
                {
                    int ans = 0;
                    if (eSortColumns[i] == ExhibitColumns.TagNumber)
                    {
                        ans = ((clsExhibit)x).TagNumber.CompareTo(((clsExhibit)y).TagNumber);
                    }
                    else if (eSortColumns[i] == ExhibitColumns.Exhibitor)
                    {
                        ans = ((clsExhibit)x).Exhibitor.Name.CompareTo(((clsExhibit)y).Exhibitor.Name);
                    }
                    else if (eSortColumns[i] == ExhibitColumns.Item)
                    {
                        if (((clsExhibit)x).MarketItem != null && ((clsExhibit)y).MarketItem != null)
                        {
                            ans = ((clsExhibit)x).MarketItem.MarketType.CompareTo(((clsExhibit)y).MarketItem.MarketType);
                        }
                        else if (((clsExhibit)x).MarketItem == null && ((clsExhibit)y).MarketItem != null)
                        {
                            ans = 1;
                        }
                        else if (((clsExhibit)x).MarketItem != null && ((clsExhibit)y).MarketItem == null)
                        {
                            ans = -1;
                        }
                        else if (((clsExhibit)x).MarketItem != null && ((clsExhibit)y).MarketItem == null)
                        {
                            ans = ((clsExhibit)x).MarketID.CompareTo(((clsExhibit)y).MarketID);
                        }
                    }
                    else if (eSortColumns[i] == ExhibitColumns.Value)
                    {
                        if (((clsExhibit)x).MarketItem != null && ((clsExhibit)y).MarketItem != null)
                        {
                            ans = ((clsExhibit)x).MarketItem.MarketValue.CompareTo(((clsExhibit)y).MarketItem.MarketValue);
                        }
                        else if (((clsExhibit)x).MarketItem == null && ((clsExhibit)y).MarketItem != null)
                        {
                            ans = 1;
                        }
                        else if (((clsExhibit)x).MarketItem != null && ((clsExhibit)y).MarketItem == null)
                        {
                            ans = -1;
                        }
                        else if (((clsExhibit)x).MarketItem != null && ((clsExhibit)y).MarketItem == null)
                        {
                            ans = ((clsExhibit)x).MarketID.CompareTo(((clsExhibit)y).MarketID);
                        }
                    }
                    else if (eSortColumns[i] == ExhibitColumns.ChampionStatus)
                    {
                        ans = ((clsExhibit)x).ChampionStatus.CompareTo(((clsExhibit)y).ChampionStatus);
                    }
                    else if (eSortColumns[i] == ExhibitColumns.RateOfGain)
                    {
                        ans = ((clsExhibit)x).RateOfGain.CompareTo(((clsExhibit)y).RateOfGain);
                    }
                    else if (eSortColumns[i] == ExhibitColumns.Weight)
                    {
                        ans = ((clsExhibit)x).Weight.CompareTo(((clsExhibit)y).Weight);
                    }
                    else if (eSortColumns[i] == ExhibitColumns.TakeBack)
                    {
                        ans = ((clsExhibit)x).TakeBack.CompareTo(((clsExhibit)y).TakeBack);
                    }
                    else if (eSortColumns[i] == ExhibitColumns.Include)
                    {
                        ans = ((clsExhibit)x).Include.CompareTo(((clsExhibit)y).Include);
                    }
                    else if (eSortColumns[i] == ExhibitColumns.Purchases)
                    {
                        if (((clsExhibit)x).Purchases == null && ((clsExhibit)y).Purchases == null)
                        {
                            ans = 0;
                        }
                        else if (((clsExhibit)x).Purchases == null && ((clsExhibit)y).Purchases != null)
                        {
                            ans = -1;
                        }
                        else if (((clsExhibit)x).Purchases != null && ((clsExhibit)y).Purchases == null)
                        {
                            ans = 1;
                        }
                        else
                        {
                            ans = ((clsExhibit)x).Purchases.Count.CompareTo(((clsExhibit)y).Purchases.Count);
                            if (ans == 0 && ((clsExhibit)x).Purchases.Count == 1)
                            {
                                ans = ((clsExhibit)x).Purchases.Values.First().BuyerID.CompareTo(((clsExhibit)y).Purchases.Values.First().BuyerID);
                            }
                        }
                    }
                    else if (eSortColumns[i] == ExhibitColumns.Disposition)
                    {
                        ans = ((clsExhibit)x).Destination.CompareTo(((clsExhibit)y).Destination);
                    }
                    if (ans != 0)
                    {
                        if (eSortOrders[i] == SortOrder.Ascending)
                        {
                            return ans;
                        }
                        else if (eSortOrders[i] == SortOrder.Descending)
                        {
                            return -ans;
                        }
                    }
                }
                return 0;
            }
        }


        public enum AnimalDestination
        {
            Seller_Unknown = -2,
            Buyer_Unknown = -1,
            Not_Sold = 0,
            HauledByBuyer = 1,
            KeptBySeller = 2,
            Fair = 3,
            New_Holland = 4,
            Buyer_SpecialInstructions = 5,
            HauledBySeller = 6
        }

        private int m_iTagNumber;
        private int m_iExhibitor;
        private clsExhibitor m_Exhibitor;
        private int m_iMarketItem;
        private clsMarketItem m_MarketItem;
        private DB.ChampionState m_ChampionStatus;
        private bool m_bRateOfGain;
        private int m_iWeight;
        private DB.NoYes m_eTakeBack;
        private bool m_bInclude;
        private string m_sComments;

        private int iPurchaseIndex = -1;


        public clsExhibit()
        {

        }

        public clsExhibit(int TagNumber, int Exhibitor, int MarketItem, DB.ChampionState ChampionStatus, bool RateOfGain, int Weight, DB.NoYes TakeBack, bool Include, string Comments)
        {
            m_iTagNumber = TagNumber;
            m_iExhibitor = Exhibitor;
            m_Exhibitor = clsDB.Exhibitors[m_iExhibitor];
            m_iMarketItem = MarketItem;
            m_MarketItem = clsDB.Market[MarketItem];
            m_ChampionStatus = ChampionStatus;
            m_bRateOfGain = RateOfGain;
            m_iWeight = Weight;
            m_eTakeBack = TakeBack;
            m_bInclude = Include;
            m_sComments = Comments;

            this.UseItemStyleForSubItems = false;
            RefreshListItem();
        }

        public override void Load(IDataReader dbReader)
        {
            m_iTagNumber = (Int32)dbReader["TagNumber"];
            m_iExhibitor = (Int32)dbReader["Exhibitor"];
            m_Exhibitor = clsDB.Exhibitors[m_iExhibitor];
            m_iMarketItem = (Int32)dbReader["MarketItem"];
            m_MarketItem = clsDB.Market[m_iMarketItem];
            m_ChampionStatus = (DB.ChampionState)((Int32)dbReader["ChampionStatus"]);
            m_bRateOfGain = (bool)dbReader["RateOfGain"];
            m_iWeight = (Int32)dbReader["Weight"];
            m_eTakeBack = (DB.NoYes)dbReader["TakeBack"];
            m_bInclude = (bool)dbReader["Include"];
            m_sComments = dbReader["Comments"].ToString();

            this.UseItemStyleForSubItems = false;
            RefreshListItem();
        }

        protected override void DBCommit(DB.CommitAction Action, IDbConnection DatabaseConnection, IDbTransaction Transaction)
        {
            IDbCommand dbCommit = DatabaseConnection.CreateCommand();
            dbCommit.Transaction = Transaction;
            dbCommit.CommandText = "INSERT INTO Exhibits (CommitAction, TagNumber, Exhibitor, MarketItem, ChampionStatus, RateOfGain, Weight, TakeBack, Include, Comments) VALUES (@CommitAction, @TagNumber, @Exhibitor, @MarketItem, @ChampionStatus, @RateOfGain, @Weight, @TakeBack, @Include, @Comments)";

            IDbDataParameter param = dbCommit.CreateParameter();
            param.ParameterName = "@CommitAction";
            param.Value = (int)Action;
            dbCommit.Parameters.Add(param);

            param = dbCommit.CreateParameter();
            param.ParameterName = "@TagNumber";
            param.Value = this.TagNumber;
            dbCommit.Parameters.Add(param);

            param = dbCommit.CreateParameter();
            param.ParameterName = "@Exhibitor";
            param.Value = this.ExhibitorNumber;
            dbCommit.Parameters.Add(param);

            param = dbCommit.CreateParameter();
            param.ParameterName = "@MarketItem";
            param.Value = this.MarketItem.MarketID;
            dbCommit.Parameters.Add(param);

            param = dbCommit.CreateParameter();
            param.ParameterName = "@ChampionStatus";
            param.Value = (int)(this.ChampionStatus);
            dbCommit.Parameters.Add(param);

            param = dbCommit.CreateParameter();
            param.ParameterName = "@RateOfGain";
            param.Value = this.RateOfGain;
            dbCommit.Parameters.Add(param);

            param = dbCommit.CreateParameter();
            param.ParameterName = "@Weight";
            param.Value = this.Weight;
            dbCommit.Parameters.Add(param);

            param = dbCommit.CreateParameter();
            param.ParameterName = "@TakeBack";
            param.Value = this.TakeBack;
            dbCommit.Parameters.Add(param);

            param = dbCommit.CreateParameter();
            param.ParameterName = "@Include";
            param.Value = this.Include;
            dbCommit.Parameters.Add(param);

            param = dbCommit.CreateParameter();
            param.ParameterName = "@Comments";
            param.Value = this.Comments;
            dbCommit.Parameters.Add(param);

            dbCommit.ExecuteNonQuery();
        }

        public void RefreshListItem()
        {
            if (base.SubItems.Count <= TOTAL_LV_COLUMNS)
            {
                SubItems.Clear();
                for (int i = 0; i < TOTAL_LV_COLUMNS; i++)
                {
                    SubItems.Add(new ListViewSubItem());
                }
            }
            SubItems[(int)ExhibitColumns.TagNumber].Text = this.TagNumber.ToString();
            RefreshExhibitor();
            RefreshMarketItem();

            SubItems[(int)ExhibitColumns.ChampionStatus].Text = this.ChampionStatus;
                        
            if (this.RateOfGain)
            {
                SubItems[(int)ExhibitColumns.RateOfGain].Text = "Rate of Gain";
            }
            else
            {
                SubItems[(int)ExhibitColumns.RateOfGain].Text = "";
            }
            SubItems[(int)ExhibitColumns.Weight].Text = this.WeightString;
            if (this.TakeBack == DB.NoYes.Yes)
            {
                SubItems[(int)ExhibitColumns.TakeBack].Text = "Yes";
            }
            else if (this.TakeBack == DB.NoYes.No)
            {
                SubItems[(int)ExhibitColumns.TakeBack].Text = "No";
            }
            else
            {
                SubItems[(int)ExhibitColumns.TakeBack].Text = "";
            }

            if (this.Include)
            {
                SubItems[(int)ExhibitColumns.Include].Text = "Yes";
            }
            else
            {
                SubItems[(int)ExhibitColumns.Include].Text = "No";
            }

            RefreshPurchase();
        }

        public void RefreshExhibitor()
        {
            if (this.Exhibitor != null)
            {
                SubItems[(int)ExhibitColumns.Exhibitor].Text = this.Exhibitor.Name.ToString();
                SubItems[(int)ExhibitColumns.Exhibitor].Font = new System.Drawing.Font(this.Font, System.Drawing.FontStyle.Regular);
                SubItems[(int)ExhibitColumns.Exhibitor].ForeColor = this.ForeColor;
            }
            else
            {
                SubItems[(int)ExhibitColumns.Exhibitor].Text = string.Format("Exhibitor #{0}", this.ExhibitorNumber);
                SubItems[(int)ExhibitColumns.Exhibitor].Font = new System.Drawing.Font(this.Font, System.Drawing.FontStyle.Italic);
                SubItems[(int)ExhibitColumns.Exhibitor].ForeColor = System.Drawing.Color.Red;
            }
        }

        public void RefreshMarketItem()
        {
            if (this.MarketItem != null)
            {
                SubItems[(int)ExhibitColumns.Item].Text = this.MarketItem.MarketType;
                if (this.MarketItem.AllowAdvertising)
                {
                    SubItems[(int)ExhibitColumns.Value].Text = this.MarketItem.MarketValue.ToString("$0.00/" + MarketItem.MarketUnits);
                    SubItems[(int)ExhibitColumns.Value].Font = new System.Drawing.Font(this.Font, System.Drawing.FontStyle.Regular);
                    SubItems[(int)ExhibitColumns.Value].ForeColor = this.ForeColor;
                }
                else
                {
                    SubItems[(int)ExhibitColumns.Value].Text = "N/A";
                    SubItems[(int)ExhibitColumns.Value].Font = new System.Drawing.Font(this.Font, System.Drawing.FontStyle.Italic);
                    SubItems[(int)ExhibitColumns.Value].ForeColor = System.Drawing.Color.LightGray;
                }
            }
            else
            {
                SubItems[(int)ExhibitColumns.Item].Text = string.Format("Market Item #{0}", this.MarketID);
                SubItems[(int)ExhibitColumns.Item].Font = new System.Drawing.Font(this.Font, System.Drawing.FontStyle.Italic);
                SubItems[(int)ExhibitColumns.Item].ForeColor = System.Drawing.Color.Red;
                SubItems[(int)ExhibitColumns.Value].Text = "";
            }
        }

        public void RefreshPurchase()
        {
            if (this.Purchases != null)
            {
                if (this.Purchases.Count > 1)
                {
                    //There are multiple purchases for this item
                    SubItems[(int)ExhibitColumns.Purchases].Text = this.Purchases.Count().ToString("# Purchases");
                    SubItems[(int)ExhibitColumns.Purchases].Font = new System.Drawing.Font(this.Font, System.Drawing.FontStyle.Italic);
                    SubItems[(int)ExhibitColumns.Purchases].ForeColor = this.ForeColor;
                }
                else
                {
                    //There is one purchase
                    if (this.Purchases.First().Value.Buyer != null)
                    {
                        //Display the buyer information
                        if (this.Purchases.First().Value.Buyer.CompanyName.Trim().Length > 0)
                        {
                            SubItems[(int)ExhibitColumns.Purchases].Text = string.Format("{0}, {1} {2} from {3}", this.Purchases.First().Value.Buyer.BuyerNumber, this.Purchases.First().Value.Buyer.Name.First, this.Purchases.First().Value.Buyer.Name.Last, this.Purchases.First().Value.Buyer.CompanyName);
                        }
                        else
                        {
                            SubItems[(int)ExhibitColumns.Purchases].Text = string.Format("{0}, {1} {2}", this.Purchases.First().Value.Buyer.BuyerNumber, this.Purchases.First().Value.Buyer.Name.First, this.Purchases.First().Value.Buyer.Name.Last);
                        }
                        SubItems[(int)ExhibitColumns.Purchases].Font = new System.Drawing.Font(this.Font, System.Drawing.FontStyle.Regular);
                        SubItems[(int)ExhibitColumns.Purchases].ForeColor = this.ForeColor;


                    }
                    else
                    {
                        //Buyer number is not registered
                        SubItems[(int)ExhibitColumns.Purchases].Text = string.Format("{0}, Unregistered Buyer", this.Purchases.First().Value.BuyerID);
                        SubItems[(int)ExhibitColumns.Purchases].Font = new System.Drawing.Font(this.Font, System.Drawing.FontStyle.Italic);
                        SubItems[(int)ExhibitColumns.Purchases].ForeColor = System.Drawing.Color.Red;
                    }
                }

                //Where is the animal going?
                if (this.Purchases.First().Value.ConditionOfSale == clsPurchase.enSaleCondition.PayFullPrice)
                {
                    if (this.MarketItem != null)
                    {
                        if (this.MarketItem.ValidDisposition)
                        {
                            SubItems[(int)ExhibitColumns.Disposition].Font = new System.Drawing.Font(this.Font, System.Drawing.FontStyle.Regular);
                            SubItems[(int)ExhibitColumns.Disposition].ForeColor = this.ForeColor;
                            if (this.Purchases.First().Value.DestinationOfAnimal == clsPurchase.enAnimalDestination.Buyer)
                            {
                                SubItems[(int)ExhibitColumns.Disposition].Text = "Hauled by Buyer";
                            }
                            else if (this.Purchases.First().Value.DestinationOfAnimal == clsPurchase.enAnimalDestination.Seller)
                            {
                                SubItems[(int)ExhibitColumns.Disposition].Text = "Hauled by Seller";
                            }
                            else if (this.Purchases.First().Value.DestinationOfAnimal == clsPurchase.enAnimalDestination.Fair)
                            {
                                SubItems[(int)ExhibitColumns.Disposition].Text = "Hauled by Fair";
                            }
                            else if (this.Purchases.First().Value.DestinationOfAnimal == clsPurchase.enAnimalDestination.SpecialInstructions)
                            {
                                SubItems[(int)ExhibitColumns.Disposition].Text = "Special Instructions";
                            }
                            else
                            {
                                SubItems[(int)ExhibitColumns.Disposition].Text = "Kept by Buyer/Not Set";
                                SubItems[(int)ExhibitColumns.Disposition].Font = new System.Drawing.Font(this.Font, System.Drawing.FontStyle.Italic);
                                SubItems[(int)ExhibitColumns.Disposition].ForeColor = System.Drawing.Color.Red;
                            }
                        }
                        else
                        {
                            SubItems[(int)ExhibitColumns.Disposition].Text = "N/A";
                            SubItems[(int)ExhibitColumns.Disposition].Font = new System.Drawing.Font(this.Font, System.Drawing.FontStyle.Italic);
                            SubItems[(int)ExhibitColumns.Disposition].ForeColor = System.Drawing.Color.LightGray;
                        }
                    }
                }
                else if (this.Purchases.First().Value.ConditionOfSale == clsPurchase.enSaleCondition.PayAdvertising)
                {
                    SubItems[(int)ExhibitColumns.Disposition].Font = new System.Drawing.Font(this.Font, System.Drawing.FontStyle.Regular);
                    SubItems[(int)ExhibitColumns.Disposition].ForeColor = this.ForeColor;
                    if (this.TakeBack == DB.NoYes.No)
                    {
                        SubItems[(int)ExhibitColumns.Disposition].Text = "New Holland";
                    }
                    else if (this.TakeBack == DB.NoYes.Yes)
                    {
                        SubItems[(int)ExhibitColumns.Disposition].Text = "Return to Seller";
                    }
                    else
                    {
                        SubItems[(int)ExhibitColumns.Disposition].Text = "Return to Seller/Not Set";
                        SubItems[(int)ExhibitColumns.Disposition].Font = new System.Drawing.Font(this.Font, System.Drawing.FontStyle.Italic);
                        SubItems[(int)ExhibitColumns.Disposition].ForeColor = System.Drawing.Color.Red;
                    }
                }
                else if (this.Purchases.First().Value.ConditionOfSale == clsPurchase.enSaleCondition.NotSet)
                {
                    SubItems[(int)ExhibitColumns.Disposition].Text = "Not Checked Out";
                    SubItems[(int)ExhibitColumns.Disposition].Font = new System.Drawing.Font(this.Font, System.Drawing.FontStyle.Italic);
                }
            }
            else
            {
                SubItems[(int)ExhibitColumns.Purchases].Text = "";
                SubItems[(int)ExhibitColumns.Disposition].Text = "";
            }
        }

        // Returns a new purchase index for this exhibit
        public int NewPurchase()
        {
            if (iPurchaseIndex >= 0)
            {
                iPurchaseIndex++;
                return iPurchaseIndex;
            }
            else
            {
                iPurchaseIndex = clsDB.Exhibits.NewPurchaseIndex(this);
                return iPurchaseIndex;
            }
        }

        public static bool operator ==(clsExhibit Exhibit1, clsExhibit Exhibit2)
        {
            if ((object)Exhibit1 == null && (object)Exhibit2 == null)
            {
                return true;
            }
            else if ((object)Exhibit1 == null || (object)Exhibit2 == null)
            {
                return false;
            }
            else
            {
                return (Exhibit1.CompareTo(Exhibit2) == 0);
            }
        }
        public static bool operator !=(clsExhibit Exhibit1, clsExhibit Exhibit2)
        {
            return !(Exhibit1 == Exhibit2);
        }

        #region IComparable<clsExhibit> Members

        public int CompareTo(clsExhibit other)
        {
            if (this.TagNumber != other.TagNumber)
            {
                return this.TagNumber.CompareTo(other.TagNumber);
            }
            else
            {
                return this.MarketID.CompareTo(other.MarketID);
            }
        }

        #endregion

        #region ISerializable Members
        protected clsExhibit(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new System.ArgumentNullException("info");
            }
            m_iTagNumber = (int)info.GetValue("iTagNumber", typeof(int));
            m_iExhibitor = (int)info.GetValue("iExhibitor", typeof(int));
            m_Exhibitor = (clsExhibitor)info.GetValue("cExhibitor", typeof(clsExhibitor));
            m_iMarketItem = (int)info.GetValue("iMarketItem", typeof(int));
            m_MarketItem = (clsMarketItem)info.GetValue("cMarketItem", typeof(clsMarketItem));
            m_ChampionStatus = (int)info.GetValue("eChampionStatus", typeof(int));
            m_bRateOfGain = (bool)info.GetValue("bRateOfGain", typeof(bool));
            m_iWeight = (int)info.GetValue("iWeight", typeof(int));
            m_eTakeBack = (DB.NoYes)info.GetValue("eTakeBack", typeof(DB.NoYes));
            m_bInclude = (bool)info.GetValue("bInclude", typeof(bool));
            m_sComments = (string)info.GetValue("sComments", typeof(string));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new System.ArgumentNullException("info");
            }
            info.AddValue("iTagNumber", m_iTagNumber);
            info.AddValue("iExhibitor", m_iExhibitor);
            info.AddValue("cExhibitor", m_Exhibitor);
            info.AddValue("iMarketItem", m_iMarketItem);
            info.AddValue("cMarketItem", m_MarketItem);
            info.AddValue("eChampionStatus", m_ChampionStatus);
            info.AddValue("bRateOfGain", m_bRateOfGain);
            info.AddValue("iWeight", m_iWeight);
            info.AddValue("eTakeBack", m_eTakeBack);
            info.AddValue("bInclude", m_bInclude);
            info.AddValue("sComments", m_sComments);
        }
        #endregion


        public int TagNumber
        {
            get
            {
                return m_iTagNumber;
            }
            set
            {
                m_iTagNumber = value;
                SubItems[(int)ExhibitColumns.TagNumber].Text = this.TagNumber.ToString();
            }
        }
        public int ExhibitorNumber
        {
            get
            {
                return m_iExhibitor;
            }
            set
            {
                m_iExhibitor = value;
            }
        }
        public clsExhibitor Exhibitor
        {
            get
            {
                if (clsDB.Exhibitors != null)
                {
                    return clsDB.Exhibitors[m_iExhibitor];
                }
                else
                {
                    return m_Exhibitor;
                }
            }
            set
            {
                m_iExhibitor = value.ExhibitorNumber;
                RefreshExhibitor();
            }
        }
        public int MarketID
        {
            get
            {
                return m_iMarketItem;
            }
        }
        public clsMarketItem MarketItem
        {
            get
            {
                if (clsDB.Market != null)
                {
                    return clsDB.Market[m_iMarketItem];
                }
                else
                {
                    return m_MarketItem;
                }
            }
            set
            {
                m_iMarketItem = value.MarketID;
                SubItems[(int)ExhibitColumns.Item].Text = this.MarketItem.MarketType;
                SubItems[(int)ExhibitColumns.Value].Text = this.MarketItem.MarketValue.ToString("$0.00");
            }
        }
        public DB.ChampionState ChampionStatus
        {
            get
            {
                return m_ChampionStatus;
            }
            set
            {
                m_ChampionStatus = value;
                SubItems[(int)ExhibitColumns.ChampionStatus].Text = this.m_ChampionStatus;
            }
        }
        public string ChampionStatusText
        {
            get
            {
                string sText = "";
                if (m_bRateOfGain)
                {
                    sText += "Rate of Gain";
                    if (((string)this.ChampionStatus).Length > 0)
                    {
                        sText += " & ";
                    }
                }

                sText += this.ChampionStatus;

                return sText;
            }
        }
        public bool RateOfGain
        {
            get
            {
                return m_bRateOfGain;
            }
            set
            {
                m_bRateOfGain = value;
                SubItems[(int)ExhibitColumns.RateOfGain].Text = this.RateOfGainText;
            }
        }
        public string RateOfGainText
        {
            get
            {
                if (this.RateOfGain)
                {
                    return "Rate of Gain";
                }
                else
                {
                    return "";
                }
            }
        }
        public int Weight
        {
            get
            {
                return m_iWeight;
            }
            set
            {
                m_iWeight = value;
                SubItems[(int)ExhibitColumns.Weight].Text = this.WeightString;
            }
        }
        public DB.NoYes TakeBack
        {
            get
            {
                return m_eTakeBack;
            }
            set
            {
                m_eTakeBack = value;
                if (this.TakeBack == DB.NoYes.Yes)
                {
                    SubItems[(int)ExhibitColumns.TakeBack].Text = "Yes";
                }
                else if (this.TakeBack == DB.NoYes.No)
                {
                    SubItems[(int)ExhibitColumns.TakeBack].Text = "No";
                }
                else
                {
                    SubItems[(int)ExhibitColumns.TakeBack].Text = "";
                }
            }
        }
        public string WeightString
        {
            get
            {
                if (this.MarketItem != null)
                {
                    return m_iWeight.ToString("0 " + this.MarketItem.MarketUnits);
                }
                else
                {
                    return m_iWeight.ToString("0 ???");
                }
            }
        }
        public string ExhibitorName
        {
            get
            {
                return this.Exhibitor.Name.ToString();
            }
        }

        public bool Include
        {
            get
            {
                return m_bInclude;
            }
            set
            {
                m_bInclude = value;
            }
        }

        public string Comments
        {
            get
            {
                return m_sComments;
            }
            set
            {
                m_sComments = value;
            }
        }
        public Dictionary<int, clsPurchase> Purchases
        {
            get
            {
                if (clsDB.Purchases != null)
                {
                    return clsDB.Purchases[this];
                }
                else
                {
                    return null;
                }
            }
        }
        public AnimalDestination Destination
        {
            get
            {
                bool bBuyerKeeping = false;
                clsPurchase.enAnimalDestination eBuyerDestination = clsPurchase.enAnimalDestination.NotSet;

                if (clsDB.Purchases[this] != null)
                {
                    foreach (clsPurchase Purchase in clsDB.Purchases[this].Values)
                    {
                        if (Purchase.ConditionOfSale == clsPurchase.enSaleCondition.PayFullPrice)
                        {
                            bBuyerKeeping = true;
                            eBuyerDestination = Purchase.DestinationOfAnimal;
                        }
                    }

                    if (!bBuyerKeeping && m_eTakeBack == DB.NoYes.Yes)
                    {
                        return AnimalDestination.KeptBySeller;
                    }
                    else if (!bBuyerKeeping && m_eTakeBack == DB.NoYes.No)
                    {
                        return AnimalDestination.New_Holland;
                    }
                    else if (!bBuyerKeeping)
                    {
                        return AnimalDestination.Seller_Unknown;
                    }
                    else if (bBuyerKeeping && eBuyerDestination == clsPurchase.enAnimalDestination.Fair)
                    {
                        return AnimalDestination.Fair;
                    }
                    else if (bBuyerKeeping && eBuyerDestination == clsPurchase.enAnimalDestination.SpecialInstructions)
                    {
                        return AnimalDestination.Buyer_SpecialInstructions;
                    }
                    else if (bBuyerKeeping && eBuyerDestination == clsPurchase.enAnimalDestination.Seller)
                    {
                        return AnimalDestination.HauledBySeller;
                    }
                    else if (bBuyerKeeping && eBuyerDestination != clsPurchase.enAnimalDestination.NotSet)
                    {
                        return AnimalDestination.HauledByBuyer;
                    }
                    else
                    {
                        return AnimalDestination.Buyer_Unknown;
                    }
                }
                else
                {
                    return AnimalDestination.Not_Sold;
                }
            }
        }

        public string Destination_String
        {
            get
            {
                if (Destination == AnimalDestination.HauledByBuyer)
                {
                    return "Hauled by Buyer";
                }
                else if (Destination == AnimalDestination.KeptBySeller)
                {
                    return "Kept by Seller";
                }
                else if (Destination == AnimalDestination.New_Holland)
                {
                    return "New Holland";
                }
                else if (Destination == AnimalDestination.Fair)
                {
                    return "Hauled by Fair";
                }
                else if (Destination == AnimalDestination.Buyer_SpecialInstructions)
                {
                    return "Special instructions from buyer";
                }
                else if (Destination == AnimalDestination.Buyer_Unknown)
                {
                    return "Buyer, unspecified";
                }
                else if (Destination == AnimalDestination.Seller_Unknown)
                {
                    return "Seller, unspecified";
                }
                else if(Destination == AnimalDestination.HauledBySeller)
                {
                    return "Hauled by Seller";
                }
                else
                {
                    return "unspecified";
                }
            }
        }

        public string Buyer_SpecialInstructions
        {
            get
            {
                if (clsDB.Purchases[this] != null)
                {
                    string sBuyerInstructions = "";
                    foreach (clsPurchase Purchase in clsDB.Purchases[this].Values)
                    {
                        if (Purchase.ConditionOfSale == clsPurchase.enSaleCondition.PayFullPrice && Purchase.DestinationOfAnimal == clsPurchase.enAnimalDestination.SpecialInstructions)
                        {
                            sBuyerInstructions = Purchase.HaulSpecialInstructions;
                        }
                    }

                    return sBuyerInstructions;
                }
                else
                {
                    return "";
                }
            }
        }

        public string AdvertDestination
        {
            get
            {
                string sAdvertDestination = "";

                if (Destination == AnimalDestination.New_Holland)
                {
                    sAdvertDestination = clsDB.Market[MarketID].AdvertDestination;
                } else
                {
                    sAdvertDestination = Destination_String;
                }

                return sAdvertDestination;
            }
        }

        public bool AnimalSold
        {
            get
            {
                return (clsDB.Purchases[this] != null);
            }
        }

        public string MarketValue_String
        {
            get
            {
                if (m_MarketItem.AllowAdvertising)
                {
                    return (m_MarketItem.MarketValue * m_iWeight).ToString("$#,##0.00") + " @ " + m_MarketItem.MarketValue_String;
                }
                else
                {
                    return "N/A";
                }
            }
        }

        public double GrossValue
        {
            get
            {
                if (clsDB.Purchases[this] != null)
                {
                    double fValue = 0;
                    foreach (clsPurchase Purchase in clsDB.Purchases[this].Values)
                    {
                        fValue += Purchase.TotalBid;
                    }

                    return fValue;
                }
                return 0;
            }
        }
    }

    namespace Setup
    {
        public class Exhibits : AuctionDBSetup
        {
            public Exhibits()
            {
                TABLE_NAME = "Exhibits";

                TABLE_COLUMNS = new List<SQLColumn>() {
                    new SQLColumn("TagNumber", "int", "", true),
                    new SQLColumn("Exhibitor", "int", "", false),
	                new SQLColumn("MarketItem", "int", "", true),
	                new SQLColumn("ChampionStatus", "int", "4", false),
                    new SQLColumn("RateOfGain", "bit", "", false),
	                new SQLColumn("Weight", "int", "", false),
	                new SQLColumn("TakeBack", "int", "", false),
                    new SQLColumn("Include", "bit", "", false),            //Include this exhibit in the auction
	                new SQLColumn("Comments", "nvarchar(1000)", "", false)
                };
            }
        }
    }
}
