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
    public class clsBuyers : clsAuctionDataCollection<IDAuctionDataKey, clsBuyer, DB.Setup.Buyers>
    {
        public clsBuyers(IDbConnection Connection)
            : base(Connection)
        {
        }

        public override void ConnectEvents()
        {
            //Now seems like a good time to check and see if any initial ghost buyers are needed...
            lock (m_dictCollection)
            {
                foreach (DB.clsPurchase Purchase in clsDB.Purchases)
                {
                    if (!m_dictCollection.ContainsKey(Purchase.BuyerID))
                    {
                        m_dictCollection.Add(Purchase.BuyerID, new clsBuyer(Purchase.BuyerID));
                    }
                }
            }

            //Use the updated purchases handler to keep the purchase count on buyers up-to-date
            clsDB.Purchases.Updated += new EventHandler<DatabaseUpdatedEventArgs>(Purchases_Updated);
        }

        //Updated purchases handler...
        void Purchases_Updated(object sender, DatabaseUpdatedEventArgs e)
        {
            foreach (DB.clsPurchase Purchase in e.UpdatedItems.Keys)
            {
                //Create a ghost buyer if the buyer does not exist
                lock (m_dictCollection)
                {
                    if (!m_dictCollection.ContainsKey(Purchase.BuyerID))
                    {
                        m_dictCollection.Add(Purchase.BuyerID, new clsBuyer(Purchase.BuyerID));
                    }
                }

                //Keep buyers purchase count up-to-date
                if (Purchase.Buyer != null)
                {
                    //TODO: This logic is not thread safe, it exists in other similar updated events as well
                    if (Purchase.Buyer.ListView == null)
                    {
                        Purchase.Buyer.RefreshPurchases();
                    }
                    else
                    {
                        Purchase.Buyer.ListView.Invoke(new System.Windows.Forms.MethodInvoker(Purchase.Buyer.RefreshPurchases));
                    }
                }
            }
        }

        public clsBuyer this[clsBuyer Buyer]
        {
            get
            {
                return this[Buyer.BuyerNumber];
            }
        }


        public void ExportToWorkbook(OfficeOpenXml.ExcelPackage outputPackage)
        {
            OfficeOpenXml.ExcelWorksheet sheetBuyer = outputPackage.Workbook.Worksheets.Add("Buyers");

            //Setup the headers
            sheetBuyer.Row(1).Style.Font.Bold = true;

            sheetBuyer.Cells["A1"].Value = "ID";
            sheetBuyer.Cells["B1"].Value = "Name";
            sheetBuyer.Cells["C1"].Value = "Company";
            sheetBuyer.Cells["D1"].Value = "Address";
            sheetBuyer.Cells["E1"].Value = "Phone Number";
            sheetBuyer.Cells["F1"].Value = "Purchases";
            sheetBuyer.Cells["G1"].Value = "Total Spent";
            
            //Enter Data
            int iCurRow = 2;
            foreach (clsBuyer Buyer in this)
            {
                sheetBuyer.SetValue(iCurRow, 1, Buyer.BuyerNumber);
                sheetBuyer.SetValue(iCurRow, 2, Buyer.Name.ToString());
                sheetBuyer.SetValue(iCurRow, 3, Buyer.CompanyName.ToString());
                sheetBuyer.SetValue(iCurRow, 4, Buyer.Address.ToString());
                sheetBuyer.SetValue(iCurRow, 5, Buyer.PhoneNumber.ToString());

                //Add up purchase count and total spent
                int iCount = 0;
                double dSpent = 0;
                foreach (clsPurchase Purchase in clsDB.Purchases.GetPurchasesByBuyer(Buyer.BuyerNumber))
                {
                    iCount++;
                    dSpent += Purchase.TotalCharged;

                }
                sheetBuyer.SetValue(iCurRow, 6, iCount);
                sheetBuyer.SetValue(iCurRow, 7, dSpent);

                iCurRow++;
            }

            iCurRow--;
            if (iCurRow > 1)
            {
                sheetBuyer.Cells[string.Format("G2:G{0}", iCurRow)].Style.Numberformat.Format = "$#,##0.00";
            }

            //Auto fit columns
            sheetBuyer.Column(1).AutoFit();
            sheetBuyer.Column(2).AutoFit();
            sheetBuyer.Column(3).AutoFit();
            sheetBuyer.Column(4).AutoFit();
            sheetBuyer.Column(5).AutoFit();
            sheetBuyer.Column(6).AutoFit();
            sheetBuyer.Column(7).AutoFit();
        }
    }

    [Serializable]
    public class clsBuyer : AuctionData, IComparable<clsBuyer>, ISerializable
    {
        // Total count of the number of columns in the list view for this auction data class
        const int TOTAL_LV_COLUMNS = 7;
        // Enumeration used to identify columns
        public enum BuyerColumns
        {
            Number = 0,
            Name = 1,
            Company = 2,
            Phone = 3,
            Address = 4,
            Purchases = 5,
            CheckedOut = 6
        }

        public class BuyerViewSorter : IComparer
        {
            BuyerColumns[] eSortColumns = new BuyerColumns[TOTAL_LV_COLUMNS];
            SortOrder[] eSortOrders = new SortOrder[TOTAL_LV_COLUMNS];

            public BuyerViewSorter()
            {
                //Initalize the current sort state so all of the columns are
                //  sorted ascending in the order they appear on the screen
                //  (this will put buyer number first).
                for (int i = 0; i < TOTAL_LV_COLUMNS; i++)
                {
                    eSortColumns[i] = (BuyerColumns)i;
                    eSortOrders[i] = SortOrder.Ascending;
                }
            }

            public void SetSortColumn(BuyerColumns Column)
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
                    if (eSortColumns[i] == BuyerColumns.Number)
                    {
                        ans = ((clsBuyer)x).BuyerNumber.CompareTo(((clsBuyer)y).BuyerNumber);
                    }
                    else if (eSortColumns[i] == BuyerColumns.Name)
                    {
                        ans = ((clsBuyer)x).m_BuyerName.CompareTo(((clsBuyer)y).m_BuyerName);
                    }
                    else if (eSortColumns[i] == BuyerColumns.Company)
                    {
                        ans = ((clsBuyer)x).CompanyName.CompareTo(((clsBuyer)y).CompanyName);
                    }
                    else if (eSortColumns[i] == BuyerColumns.Phone)
                    {
                        ans = ((clsBuyer)x).PhoneNumber.CompareTo(((clsBuyer)y).PhoneNumber);
                    }
                    else if (eSortColumns[i] == BuyerColumns.Address)
                    {
                        ans = ((clsBuyer)x).Address.CompareTo(((clsBuyer)y).Address);
                    }
                    else if (eSortColumns[i] == BuyerColumns.Purchases)
                    {
                        ans = ((clsBuyer)x).PurchaseCount.CompareTo(((clsBuyer)y).PurchaseCount);
                    }
                    else if (eSortColumns[i] == BuyerColumns.CheckedOut)
                    {
                        ans = ((clsBuyer)x).CheckedOut.CompareTo(((clsBuyer)y).CheckedOut);
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

        int m_iBuyerNum = 0;
        string m_sCompanyName = "";
        string m_sPhoneNumber = "";

        Types.Name m_BuyerName = new Types.Name();
        Types.Address m_BuyerAddress = new Types.Address();
        string m_sBillingAddresse = "";
        Types.Address m_BillingAddress = new Types.Address();
        int m_iPurchases = 0;

        bool m_bCheckedOut = false;
        bool m_bGhostBuyer = false; //Indicates the buyer has not been registered but has purchases

        public clsBuyer()
        {
        }

        public clsBuyer(int BuyerNumber)
        {
            m_iBuyerNum = BuyerNumber;
            m_bGhostBuyer = true;

            m_BuyerName.First = "Unregistered";
            m_BuyerName.Last = "Buyer";

            RefreshPurchases();
        }

        public clsBuyer(int BuyerNumber, string NameFirst, string NameLast, string CompanyName, string AddrStreet, string AddrCity, string AddrState, int AddrZip, string PhoneNumber)
        {
            m_iBuyerNum = BuyerNumber;
            m_BuyerName.First = NameFirst;
            m_BuyerName.Last = NameLast;
            m_sCompanyName = CompanyName;
            m_sPhoneNumber = PhoneNumber;

            m_BuyerAddress.City = AddrCity;
            m_BuyerAddress.State = AddrState;
            m_BuyerAddress.Street = AddrStreet;
            m_BuyerAddress.Zip = AddrZip;

            m_sBillingAddresse = "";
            m_BillingAddress.Street = "";
            m_BillingAddress.City = "";
            m_BillingAddress.State = "";
            m_BillingAddress.Zip = 0;

            this.UseItemStyleForSubItems = false;
            RefreshPurchases();
        }

        public override void Load(IDataReader dbReader)
        {
            m_iBuyerNum = (Int32)dbReader["ID"];

            m_BuyerName.First = dbReader["NameFirst"].ToString();
            m_BuyerName.Last = dbReader["NameLast"].ToString();
            m_sCompanyName = dbReader["CompanyName"].ToString();

            m_BuyerAddress.Street = dbReader["Address"].ToString();
            m_BuyerAddress.City = dbReader["City"].ToString();
            m_BuyerAddress.State = dbReader["State"].ToString();
            if (dbReader["Zip"] != System.DBNull.Value)
            {
                m_BuyerAddress.Zip = (Int32)dbReader["Zip"];
            }
            else
            {
                m_BuyerAddress.Zip = -1;
            }

            m_sPhoneNumber = dbReader["PhoneNumber"].ToString();

            m_sBillingAddresse = dbReader["BillAddr_Addressee"].ToString();

            m_BillingAddress.Street = dbReader["BillAddr_Street"].ToString();
            m_BillingAddress.City = dbReader["BillAddr_City"].ToString();
            m_BillingAddress.State = dbReader["BillAddr_State"].ToString();
            if (dbReader["BillAddr_Zip"] != System.DBNull.Value)
            {
                m_BillingAddress.Zip = (Int32)dbReader["BillAddr_Zip"];
            }
            else
            {
                m_BillingAddress.Zip = -1;
            }

            if (dbReader["CheckedOut"] != System.DBNull.Value)
            {
                m_bCheckedOut = (bool)dbReader["CheckedOut"];
            }
            else
            {
                m_bCheckedOut = false;
            }

            this.UseItemStyleForSubItems = false;
            RefreshPurchases();
        }

        protected override void DBCommit(DB.CommitAction Action, IDbConnection DatabaseConnection, IDbTransaction Transaction)
        {
            IDbCommand dbCommit = DatabaseConnection.CreateCommand();
            dbCommit.Transaction = Transaction;

            dbCommit.CommandText = "INSERT INTO Buyers (CommitAction, ID, NameFirst, NameLast, CompanyName, Address, City, State, Zip, PhoneNumber, BillAddr_Addressee, BillAddr_Street, BillAddr_City, BillAddr_State, BillAddr_Zip, CheckedOut) VALUES (@CommitAction, @ID, @NameFirst, @NameLast, @CompanyName, @Address, @City, @State, @Zip, @PhoneNumber, @BillAddr_Addressee, @BillAddr_Street, @BillAddr_City, @BillAddr_State, @BillAddr_Zip, @CheckedOut)";

            IDbDataParameter param = dbCommit.CreateParameter();
            param.ParameterName = "@CommitAction";
            param.Value = (int)Action;
            dbCommit.Parameters.Add(param);

            param = dbCommit.CreateParameter();
            param.ParameterName = "@ID";
            param.Value = this.BuyerNumber;
            dbCommit.Parameters.Add(param);

            param = dbCommit.CreateParameter();
            param.ParameterName = "@NameFirst";
            param.Value = this.Name.First;
            dbCommit.Parameters.Add(param);

            param = dbCommit.CreateParameter();
            param.ParameterName = "@NameLast";
            param.Value = this.Name.Last;
            dbCommit.Parameters.Add(param);

            param = dbCommit.CreateParameter();
            param.ParameterName = "@CompanyName";
            param.Value = this.CompanyName;
            dbCommit.Parameters.Add(param);

            param = dbCommit.CreateParameter();
            param.ParameterName = "@Address";
            param.Value = this.Address.Street;
            dbCommit.Parameters.Add(param);

            param = dbCommit.CreateParameter();
            param.ParameterName = "@City";
            param.Value = this.Address.City;
            dbCommit.Parameters.Add(param);

            param = dbCommit.CreateParameter();
            param.ParameterName = "@State";
            param.Value = this.Address.State;
            dbCommit.Parameters.Add(param);

            param = dbCommit.CreateParameter();
            param.ParameterName = "@Zip";
            param.Value = this.Address.Zip;
            dbCommit.Parameters.Add(param);

            param = dbCommit.CreateParameter();
            param.ParameterName = "@PhoneNumber";
            param.Value = this.PhoneNumber;
            dbCommit.Parameters.Add(param);

            param = dbCommit.CreateParameter();
            param.ParameterName = "@BillAddr_Addressee";
            param.Value = this.BillingAddresse;
            dbCommit.Parameters.Add(param);

            param = dbCommit.CreateParameter();
            param.ParameterName = "@BillAddr_Street";
            param.Value = this.Billing.Street;
            dbCommit.Parameters.Add(param);

            param = dbCommit.CreateParameter();
            param.ParameterName = "@BillAddr_City";
            param.Value = this.Billing.City;
            dbCommit.Parameters.Add(param);

            param = dbCommit.CreateParameter();
            param.ParameterName = "@BillAddr_State";
            param.Value = this.Billing.State;
            dbCommit.Parameters.Add(param);

            param = dbCommit.CreateParameter();
            param.ParameterName = "@BillAddr_Zip";
            param.Value = this.Billing.Zip;
            dbCommit.Parameters.Add(param);

            param = dbCommit.CreateParameter();
            param.ParameterName = "@CheckedOut";
            param.Value = this.CheckedOut;
            dbCommit.Parameters.Add(param);

            dbCommit.ExecuteNonQuery();
        }

        private void RefreshListItem()
        {
            if (base.SubItems.Count <= TOTAL_LV_COLUMNS)
            {
                SubItems.Clear();
                SubItems.Add(new ListViewSubItem());
                SubItems.Add(new ListViewSubItem());
                SubItems.Add(new ListViewSubItem());
                SubItems.Add(new ListViewSubItem());
                SubItems.Add(new ListViewSubItem());
                SubItems.Add(new ListViewSubItem());
                SubItems.Add(new ListViewSubItem());
            }

            if (m_bGhostBuyer)
            {
                SubItems[(int)BuyerColumns.Number].Font = new System.Drawing.Font(this.Font, System.Drawing.FontStyle.Italic);
                SubItems[(int)BuyerColumns.Name].Font = new System.Drawing.Font(this.Font, System.Drawing.FontStyle.Italic);
                SubItems[(int)BuyerColumns.Company].Font = new System.Drawing.Font(this.Font, System.Drawing.FontStyle.Italic);
                SubItems[(int)BuyerColumns.Address].Font = new System.Drawing.Font(this.Font, System.Drawing.FontStyle.Italic);
                SubItems[(int)BuyerColumns.Phone].Font = new System.Drawing.Font(this.Font, System.Drawing.FontStyle.Italic);
                SubItems[(int)BuyerColumns.Purchases].Font = new System.Drawing.Font(this.Font, System.Drawing.FontStyle.Italic);
                SubItems[(int)BuyerColumns.Number].ForeColor = System.Drawing.Color.LightGray;
                SubItems[(int)BuyerColumns.Name].ForeColor = System.Drawing.Color.LightGray;
                SubItems[(int)BuyerColumns.Company].ForeColor = System.Drawing.Color.LightGray;
                SubItems[(int)BuyerColumns.Address].ForeColor = System.Drawing.Color.LightGray;
                SubItems[(int)BuyerColumns.Phone].ForeColor = System.Drawing.Color.LightGray;
                SubItems[(int)BuyerColumns.Purchases].ForeColor = System.Drawing.Color.LightGray;
            }
            else
            {
                SubItems[(int)BuyerColumns.Number].Font = new System.Drawing.Font(this.Font, System.Drawing.FontStyle.Regular);
                SubItems[(int)BuyerColumns.Name].Font = new System.Drawing.Font(this.Font, System.Drawing.FontStyle.Regular);
                SubItems[(int)BuyerColumns.Company].Font = new System.Drawing.Font(this.Font, System.Drawing.FontStyle.Regular);
                SubItems[(int)BuyerColumns.Address].Font = new System.Drawing.Font(this.Font, System.Drawing.FontStyle.Regular);
                SubItems[(int)BuyerColumns.Phone].Font = new System.Drawing.Font(this.Font, System.Drawing.FontStyle.Regular);
                SubItems[(int)BuyerColumns.Purchases].Font = new System.Drawing.Font(this.Font, System.Drawing.FontStyle.Regular);
                SubItems[(int)BuyerColumns.Number].ForeColor = this.ForeColor;
                SubItems[(int)BuyerColumns.Name].ForeColor = this.ForeColor;
                SubItems[(int)BuyerColumns.Company].ForeColor = this.ForeColor;
                SubItems[(int)BuyerColumns.Address].ForeColor = this.ForeColor;
                SubItems[(int)BuyerColumns.Phone].ForeColor = this.ForeColor;
                SubItems[(int)BuyerColumns.Purchases].ForeColor = this.ForeColor;
            }

            SubItems[(int)BuyerColumns.Number].Text = m_iBuyerNum.ToString();
            SubItems[(int)BuyerColumns.Name].Text = string.Format("{0} {1}", m_BuyerName.First, m_BuyerName.Last);
            SubItems[(int)BuyerColumns.Company].Text = m_sCompanyName;
            SubItems[(int)BuyerColumns.Address].Text = m_BuyerAddress.ToString();
            SubItems[(int)BuyerColumns.Phone].Text = m_sPhoneNumber;
            SubItems[(int)BuyerColumns.Purchases].Text = m_iPurchases.ToString();
            if (m_bCheckedOut)
            {
                SubItems[(int)BuyerColumns.CheckedOut].Text = "Yes";
                SubItems[(int)BuyerColumns.CheckedOut].Font = new System.Drawing.Font(this.Font, System.Drawing.FontStyle.Regular);
                SubItems[(int)BuyerColumns.CheckedOut].ForeColor = this.ForeColor;
            }
            else if (m_iPurchases <= 0)
            {
                SubItems[(int)BuyerColumns.CheckedOut].Text = "N/A";
                SubItems[(int)BuyerColumns.CheckedOut].Font = new System.Drawing.Font(this.Font, System.Drawing.FontStyle.Italic);
                SubItems[(int)BuyerColumns.CheckedOut].ForeColor = System.Drawing.Color.LightGray;
            }
            else
            {
                SubItems[(int)BuyerColumns.CheckedOut].Text = "";
            }
        }

        public void RefreshPurchases()
        {
            m_iPurchases = clsDB.Purchases.GetPurchasesByBuyer(this.BuyerNumber).Count;
            RefreshListItem();
        }

        public static bool operator ==(clsBuyer Buyer1, clsBuyer Buyer2)
        {
            if ((object)Buyer1 == null && (object)Buyer2 == null)
            {
                return true;
            }
            else if ((object)Buyer1 == null || (object)Buyer2 == null)
            {
                return false;
            }
            else
            {
                return (Buyer1.CompareTo(Buyer2) == 0);
            }
        }
        public static bool operator !=(clsBuyer Buyer1, clsBuyer Buyer2)
        {
            return !(Buyer1 == Buyer2);
        }

        #region IComparable<clsExhibitor> Members

        public int CompareTo(clsBuyer other)
        {
            return this.BuyerNumber.CompareTo(other.BuyerNumber);
        }

        #endregion

        protected clsBuyer(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new System.ArgumentNullException("info");
            }
            m_iBuyerNum = (int)info.GetValue("iBuyerNum", typeof(int));
            m_sCompanyName = (string)info.GetValue("sCompanyName", typeof(string));
            m_sPhoneNumber = (string)info.GetValue("sPhoneNumber", typeof(string));
            m_BuyerName = (Types.Name)info.GetValue("BuyerName", typeof(Types.Name));
            m_BuyerAddress = (Types.Address)info.GetValue("BuyerAddress", typeof(Types.Address));
            m_sBillingAddresse = (string)info.GetValue("sBillingAddresse", typeof(string));
            m_BillingAddress = (Types.Address)info.GetValue("BillingAddress", typeof(Types.Address));
            m_iPurchases = (int)info.GetValue("iPurchases", typeof(int));
            m_bCheckedOut = (bool)info.GetValue("bCheckedOut", typeof(bool));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new System.ArgumentNullException("info");
            }

            info.AddValue("iBuyerNum", m_iBuyerNum);
            info.AddValue("sCompanyName", m_sCompanyName);
            info.AddValue("sPhoneNumber", m_sPhoneNumber);
            info.AddValue("BuyerName", m_BuyerName);
            info.AddValue("BuyerAddress", m_BuyerAddress);
            info.AddValue("sBillingAddresse", m_sBillingAddresse);
            info.AddValue("BillingAddress", m_BillingAddress);
            info.AddValue("iPurchases", m_iPurchases);
            info.AddValue("bCheckedOut", m_bCheckedOut);
        }

        public int BuyerNumber
        {
            get
            {
                return m_iBuyerNum;
            }
        }

        public Types.Name Name
        {
            get
            {
                return m_BuyerName;
            }
        }

        public string CompanyName
        {
            get
            {
                return m_sCompanyName;
            }
        }

        public Types.Address Address
        {
            set
            {
                m_BuyerAddress = value;
                SubItems[(int)BuyerColumns.Address].Text = m_BuyerAddress.ToString();
            }
            get
            {
                return m_BuyerAddress;
            }
        }

        public string BillingAddresse
        {
            set
            {
                m_sBillingAddresse = value;
            }
            get
            {
                return m_sBillingAddresse;
            }
        }

        public Types.Address Billing
        {
            set
            {
                m_BillingAddress = value;
            }
            get
            {
                return m_BillingAddress;
            }
        }

        public string PhoneNumber
        {
            get
            {
                return m_sPhoneNumber;
            }
        }

        public int PurchaseCount
        {
            get
            {
                return m_iPurchases;
            }
        }

        public bool CheckedOut
        {
            get
            {
                return m_bCheckedOut;
            }
            set
            {
                m_bCheckedOut = value;
                if (m_bCheckedOut)
                {
                    SubItems[(int)BuyerColumns.CheckedOut].Text = "Yes";
                    SubItems[(int)BuyerColumns.CheckedOut].Font = new System.Drawing.Font(this.Font, System.Drawing.FontStyle.Regular);
                    SubItems[(int)BuyerColumns.CheckedOut].ForeColor = this.ForeColor;
                }
                else if (m_iPurchases <= 0)
                {
                    SubItems[(int)BuyerColumns.CheckedOut].Text = "N/A";
                    SubItems[(int)BuyerColumns.CheckedOut].Font = new System.Drawing.Font(this.Font, System.Drawing.FontStyle.Italic);
                    SubItems[(int)BuyerColumns.CheckedOut].ForeColor = System.Drawing.Color.LightGray;
                }
                else
                {
                    SubItems[(int)BuyerColumns.CheckedOut].Text = "";
                }
            }
        }

        public bool GhostBuyer
        {
            get
            {
                return m_bGhostBuyer;
            }
        }
    }

    namespace Setup
    {
        public class Buyers : AuctionDBSetup
        {
            public Buyers()
            {
                TABLE_NAME = "Buyers";

                TABLE_COLUMNS = new List<SQLColumn>() {
                    new SQLColumn("ID", "int", "0", true),	/*Buyer Number*/
                    new SQLColumn("NameFirst", "nvarchar(100)", "", false),
	                new SQLColumn("NameLast", "nvarchar(100)", "", false),
	                new SQLColumn("NameNick", "nvarchar(100)", "", false),
	                new SQLColumn("CompanyName", "nvarchar(200)", "", false),
	                new SQLColumn("Address", "nvarchar(200)", "", false),
	                new SQLColumn("City", "nvarchar(100)", "", false),
	                new SQLColumn("State", "nvarchar(100)", "", false),
	                new SQLColumn("Zip", "int", "0", false),
	                new SQLColumn("PhoneNumber", "nvarchar(20)", "", false),
	                new SQLColumn("BillAddr_Addressee", "nvarchar(1000)", "", false),
	                new SQLColumn("BillAddr_Street", "nvarchar(1000)", "", false),
	                new SQLColumn("BillAddr_City", "nvarchar(200)", "", false),
	                new SQLColumn("BillAddr_State", "nvarchar(2)", "", false),
	                new SQLColumn("BillAddr_Zip", "int", "0", false),
	                new SQLColumn("CheckedOut", "bit", "0", false)
                };
            }

            //Use the post setup steps to create the buyer history table
            public override void SQLPostSetupSteps(IDbConnection DatabaseConnection)
            {
                IDbCommand dbSetup = DatabaseConnection.CreateCommand();

                dbSetup.CommandText = "" +
                "CREATE TABLE BuyerHistory (" +
                "   NameFirst nvarchar(100)," +
                "   NameLast nvarchar(100)," +
                "   CompanyName nvarchar(200)," +
                "   Address nvarchar(200)," +
                "   City nvarchar(100)," +
                "   State nvarchar(100)," +
                "   Zip int," +
                "   PhoneNumber nvarchar(20)" +
                ")";
                dbSetup.ExecuteNonQuery();
            }

            //Import the buyer table from the "FromDatabase" to the buyer history table in the "ToDatabase"
            public void SQLImportHistory(IDbConnection FromDatabase, IDbConnection ToDatabase)
            {
                int iRecordsAdded = 0;
                int iRecordsModified = 0;

                //TODO: Merge in buyer history

                //Prep both an update and an insert command
                IDbCommand dbCopyInsert = ToDatabase.CreateCommand();
                dbCopyInsert.CommandText = "" +
                "INSERT INTO BuyerHistory (" +
                "   NameFirst," +
                "   NameLast," +
                "   CompanyName," +
                "   Address," +
                "   City," +
                "   State," +
                "   Zip," +
                "   PhoneNumber" +
                ") VALUES (" +
                "   @NameFirst," +
                "   @NameLast," +
                "   @CompanyName," +
                "   @Address," +
                "   @City," +
                "   @State," +
                "   @Zip," +
                "   @PhoneNumber" +
                ")";

                IDbCommand dbCopyUpdate = ToDatabase.CreateCommand();
                dbCopyUpdate.CommandText  = "" +
                "UPDATE BuyerHistory SET" +
                "   CompanyName = @CompanyName," +
                "   Address = @Address," +
                "   City = @City," +
                "   State = @State," +
                "   Zip = @Zip," +
                "   PhoneNumber = @PhoneNumber " +
                "WHERE " +
                "   NameFirst = @NameFirst AND " +
                "   NameLast = @NameLast";
                IDataReader dbSourceBuyers = this.SQLLoadDataQuery(FromDatabase).ExecuteReader();

                while (dbSourceBuyers.Read())
                {
                    dbCopyInsert.Parameters.Clear();
                    dbCopyUpdate.Parameters.Clear();

                    IDbDataParameter dbParamNameFirst = dbCopyUpdate.CreateParameter();
                    dbParamNameFirst.ParameterName = "@NameFirst";
                    dbParamNameFirst.Value = dbSourceBuyers["NameFirst"].ToString();
                    dbCopyUpdate.Parameters.Add(dbParamNameFirst);


                    IDbDataParameter dbParamNameLast = dbCopyUpdate.CreateParameter();
                    dbParamNameLast.ParameterName = "@NameLast";
                    dbParamNameLast.Value = dbSourceBuyers["NameLast"].ToString();
                    dbCopyUpdate.Parameters.Add(dbParamNameLast);

                    IDbDataParameter dbParamCompanyName = dbCopyUpdate.CreateParameter();
                    dbParamCompanyName.ParameterName = "@CompanyName";
                    dbParamCompanyName.Value = dbSourceBuyers["CompanyName"].ToString();
                    dbCopyUpdate.Parameters.Add(dbParamCompanyName);

                    IDbDataParameter dbParamAddress = dbCopyUpdate.CreateParameter();
                    dbParamAddress.ParameterName = "@Address";
                    dbParamAddress.Value = dbSourceBuyers["Address"].ToString();
                    dbCopyUpdate.Parameters.Add(dbParamAddress);

                    IDbDataParameter dbParamCity = dbCopyUpdate.CreateParameter();
                    dbParamCity.ParameterName = "@City";
                    dbParamCity.Value = dbSourceBuyers["City"].ToString();
                    dbCopyUpdate.Parameters.Add(dbParamCity);

                    IDbDataParameter dbParamState = dbCopyUpdate.CreateParameter();
                    dbParamState.ParameterName = "@State";
                    dbParamState.Value = dbSourceBuyers["State"].ToString();
                    dbCopyUpdate.Parameters.Add(dbParamState);

                    IDbDataParameter dbParamZip = dbCopyUpdate.CreateParameter();
                    dbParamZip.ParameterName = "@Zip";
                    dbParamZip.Value = dbSourceBuyers["Zip"].ToString();
                    dbCopyUpdate.Parameters.Add(dbParamZip);

                    IDbDataParameter dbParamPhoneNumber = dbCopyUpdate.CreateParameter();
                    dbParamPhoneNumber.ParameterName = "@PhoneNumber";
                    dbParamPhoneNumber.Value = dbSourceBuyers["PhoneNumber"].ToString();
                    dbCopyUpdate.Parameters.Add(dbParamPhoneNumber);

                    //Attempt to update a record with the same name
                    int iRecordsAffected = dbCopyUpdate.ExecuteNonQuery();
                    if (iRecordsAffected == 0)
                    {
                        //There was no record to update, insert a new one
                        //Unfortunately, it seems parameter objects can't exist on more than one command at the same time.
                        //  Loop and move the parameters one by one to the insert command.
                        while (dbCopyUpdate.Parameters.Count > 0)
                        {
                            IDbDataParameter dbParam = (IDbDataParameter)dbCopyUpdate.Parameters[0];
                            dbCopyUpdate.Parameters.Remove(dbParam);
                            dbCopyInsert.Parameters.Add(dbParam);
                        }

                        dbCopyInsert.ExecuteNonQuery();
                    }
                }

                dbSourceBuyers.Close();
            }

            //Copy buyer history table from the "FromDatabase" to the buyer history table in the "ToDatabase"
            public void SQLCopyHistory(IDbConnection FromDatabase, IDbConnection ToDatabase)
            {
                int iRecordsAdded = 0;
                int iRecordsModified = 0;

                //TODO: Merge in buyer history

                //Prep both an update and an insert command
                IDbCommand dbCopyInsert = ToDatabase.CreateCommand();
                dbCopyInsert.CommandText = "" +
                "INSERT INTO BuyerHistory (" +
                "   NameFirst," +
                "   NameLast," +
                "   CompanyName," +
                "   Address," +
                "   City," +
                "   State," +
                "   Zip," +
                "   PhoneNumber" +
                ") VALUES (" +
                "   @NameFirst," +
                "   @NameLast," +
                "   @CompanyName," +
                "   @Address," +
                "   @City," +
                "   @State," +
                "   @Zip," +
                "   @PhoneNumber" +
                ")";

                IDbCommand dbCopyUpdate = ToDatabase.CreateCommand();
                dbCopyUpdate.CommandText = "" +
                "UPDATE BuyerHistory SET" +
                "   CompanyName = @CompanyName," +
                "   Address = @Address," +
                "   City = @City," +
                "   State = @State," +
                "   Zip = @Zip," +
                "   PhoneNumber = @PhoneNumber " +
                "WHERE " +
                "   NameFirst = @NameFirst AND " +
                "   NameLast = @NameLast";

                //Query the data from the previous database
                IDbCommand dbQueryHistory = FromDatabase.CreateCommand();
                dbQueryHistory.CommandText = "" +
                "SELECT " +
                "   NameFirst," +
                "   NameLast," +
                "   CompanyName," +
                "   Address," +
                "   City," +
                "   State," +
                "   Zip," +
                "   PhoneNumber " +
                "FROM " +
                "   BuyerHistory";
                IDataReader dbSourceBuyers = dbQueryHistory.ExecuteReader();

                while (dbSourceBuyers.Read())
                {
                    dbCopyInsert.Parameters.Clear();
                    dbCopyUpdate.Parameters.Clear();

                    IDbDataParameter dbParamNameFirst = dbCopyUpdate.CreateParameter();
                    dbParamNameFirst.ParameterName = "@NameFirst";
                    dbParamNameFirst.Value = dbSourceBuyers["NameFirst"].ToString();
                    dbCopyUpdate.Parameters.Add(dbParamNameFirst);


                    IDbDataParameter dbParamNameLast = dbCopyUpdate.CreateParameter();
                    dbParamNameLast.ParameterName = "@NameLast";
                    dbParamNameLast.Value = dbSourceBuyers["NameLast"].ToString();
                    dbCopyUpdate.Parameters.Add(dbParamNameLast);

                    IDbDataParameter dbParamCompanyName = dbCopyUpdate.CreateParameter();
                    dbParamCompanyName.ParameterName = "@CompanyName";
                    dbParamCompanyName.Value = dbSourceBuyers["CompanyName"].ToString();
                    dbCopyUpdate.Parameters.Add(dbParamCompanyName);

                    IDbDataParameter dbParamAddress = dbCopyUpdate.CreateParameter();
                    dbParamAddress.ParameterName = "@Address";
                    dbParamAddress.Value = dbSourceBuyers["Address"].ToString();
                    dbCopyUpdate.Parameters.Add(dbParamAddress);

                    IDbDataParameter dbParamCity = dbCopyUpdate.CreateParameter();
                    dbParamCity.ParameterName = "@City";
                    dbParamCity.Value = dbSourceBuyers["City"].ToString();
                    dbCopyUpdate.Parameters.Add(dbParamCity);

                    IDbDataParameter dbParamState = dbCopyUpdate.CreateParameter();
                    dbParamState.ParameterName = "@State";
                    dbParamState.Value = dbSourceBuyers["State"].ToString();
                    dbCopyUpdate.Parameters.Add(dbParamState);

                    IDbDataParameter dbParamZip = dbCopyUpdate.CreateParameter();
                    dbParamZip.ParameterName = "@Zip";
                    dbParamZip.Value = dbSourceBuyers["Zip"].ToString();
                    dbCopyUpdate.Parameters.Add(dbParamZip);

                    IDbDataParameter dbParamPhoneNumber = dbCopyUpdate.CreateParameter();
                    dbParamPhoneNumber.ParameterName = "@PhoneNumber";
                    dbParamPhoneNumber.Value = dbSourceBuyers["PhoneNumber"].ToString();
                    dbCopyUpdate.Parameters.Add(dbParamPhoneNumber);

                    //Attempt to update a record with the same name
                    int iRecordsAffected = dbCopyUpdate.ExecuteNonQuery();
                    if (iRecordsAffected == 0)
                    {
                        //There was no record to update, insert a new one
                        //Unfortunately, it seems parameter objects can't exist on more than one command at the same time.
                        //  Loop and move the parameters one by one to the insert command.
                        while (dbCopyUpdate.Parameters.Count > 0)
                        {
                            IDbDataParameter dbParam = (IDbDataParameter)dbCopyUpdate.Parameters[0];
                            dbCopyUpdate.Parameters.Remove(dbParam);
                            dbCopyInsert.Parameters.Add(dbParam);
                        }

                        dbCopyInsert.ExecuteNonQuery();
                    }
                }

                dbSourceBuyers.Close();
            }

        }

    }
}
