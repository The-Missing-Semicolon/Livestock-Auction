using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Collections;
using System.Runtime.Serialization;
using System.Data;

namespace Livestock_Auction.DB
{
    public class clsExhibitors : clsAuctionDataCollection<IDAuctionDataKey, clsExhibitor, DB.Setup.Exhibitors>
    {
        public clsExhibitors(IDbConnection DatabaseConnection)
            : base(DatabaseConnection)
        {
        }

        public override void ConnectEvents()
        {
            foreach (DB.clsExhibitor Exhibitor in this)
            {
                Exhibitor.RefreshListItem();
                Exhibitor.RefreshExhibits();
            }

            //Use the updated exhbits handler to keep the exhibit count on exhibitors up-to-date
            clsDB.Exhibits.Updated += new EventHandler<DatabaseUpdatedEventArgs>(Exhibits_Updated);
        }

        //Updated Exhibits handler, keeps exhbitor's exhibit count up-to-date
        void Exhibits_Updated(object sender, DatabaseUpdatedEventArgs e)
        {
            foreach (DB.clsExhibit Exhibit in e.UpdatedItems.Keys)
            {
                if (Exhibit.Exhibitor != null)
                {
                    if (Exhibit.Exhibitor.ListView == null)
                    {
                        Exhibit.Exhibitor.RefreshExhibits();
                    }
                    else
                    {
                        Exhibit.Exhibitor.ListView.Invoke(new System.Windows.Forms.MethodInvoker(Exhibit.Exhibitor.RefreshExhibits));
                    }
                }
            }
        }

        public List<clsExhibitor> Find(string FirstName, string LastName)
        {
            List<clsExhibitor> lstExhibitors = new List<clsExhibitor>();

            IDbCommand dbLoad = m_dbConn.CreateCommand();
            if (FirstName != "" && LastName != "")
            {
                dbLoad.CommandText = "SELECT ID, NameFirst, NameLast, NameNick, Address, City, State, Zip FROM vExhibitors_Current WHERE NameFirst='" + FirstName + "' AND NameLast='" + LastName + "'";
            }
            else if (LastName != "")
            {
                dbLoad.CommandText = "SELECT ID, NameFirst, NameLast, NameNick, Address, City, State, Zip FROM vExhibitors_Current WHERE NameLast='" + LastName + "'";
            }
            else if (FirstName != "")
            {
                dbLoad.CommandText = "SELECT ID, NameFirst, NameLast, NameNick, Address, City, State, Zip FROM vExhibitors_Current WHERE NameFirst='" + FirstName + "'";
            }

            IDataReader ExhibitorReader = dbLoad.ExecuteReader();

            while (ExhibitorReader.Read())
            {
                clsExhibitor Ex = new clsExhibitor(int.Parse(ExhibitorReader["ID"].ToString()), ExhibitorReader["NameFirst"].ToString(), ExhibitorReader["NameLast"].ToString(), ExhibitorReader["NameNick"].ToString());

                lstExhibitors.Add(Ex);
            }
            ExhibitorReader.Close();

            return lstExhibitors;
        }

        public clsExhibitor this[clsExhibitor Exhibitor]
        {
            get
            {
                if (m_dictCollection.ContainsKey(Exhibitor.ExhibitorNumber))
                {
                    return m_dictCollection[Exhibitor.ExhibitorNumber];
                }
                else
                {
                    return null;
                }
            }
            set
            {
                m_dictCollection[Exhibitor.ExhibitorNumber] = value;
            }
        }
    }

    [Serializable]
    public class clsExhibitor : AuctionData, IComparable<clsExhibitor>, ISerializable
    {
        // Total count of the number of columns in the list view for this auction data class
        const int TOTAL_LV_COLUMNS = 4;
        // Enumeration used to identify columns
        public enum ExhibitorColumns
        {
            Number = 0,
            Name = 1,
            Address = 2,
            Exhibits = 3
        }

        public class ExhibitorViewSorter : IComparer
        {
            ExhibitorColumns[] eSortColumns = new ExhibitorColumns[TOTAL_LV_COLUMNS];
            SortOrder[] eSortOrders = new SortOrder[TOTAL_LV_COLUMNS];

            public ExhibitorViewSorter()
            {
                //Initalize the current sort state so all of the columns are
                //  sorted ascending in the order they appear on the screen
                //  (this will put buyer number first).
                for (int i = 0; i < TOTAL_LV_COLUMNS; i++)
                {
                    eSortColumns[i] = (ExhibitorColumns)i;
                    eSortOrders[i] = SortOrder.Ascending;
                }
            }

            public void SetSortColumn(ExhibitorColumns Column)
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
                    if (eSortColumns[i] == ExhibitorColumns.Number)
                    {
                        ans = ((clsExhibitor)x).ExhibitorNumber.CompareTo(((clsExhibitor)y).ExhibitorNumber);
                    }
                    else if (eSortColumns[i] == ExhibitorColumns.Name)
                    {
                        ans = ((clsExhibitor)x).Name.CompareTo(((clsExhibitor)y).Name);
                    }
                    else if (eSortColumns[i] == ExhibitorColumns.Address)
                    {
                        ans = ((clsExhibitor)x).Address.CompareTo(((clsExhibitor)y).Address);
                    }
                    else if (eSortColumns[i] == ExhibitorColumns.Exhibits)
                    {
                        ans = ((clsExhibitor)x).ExhibitCount.CompareTo(((clsExhibitor)y).ExhibitCount);
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

        int m_iExNumber = 0;
        int m_iExhibits = 0;
        Types.Name m_ExhibitorName = new Types.Name();
        Types.Address m_ExhibitorAddr = new Types.Address();

        public clsExhibitor()
        {

        }

        public clsExhibitor(int ExhibitorNumber, string FirstName, string LastName, string NickName)
        {
            m_iExNumber = ExhibitorNumber;
            m_ExhibitorName.First = FirstName;
            m_ExhibitorName.Last = LastName;
            m_ExhibitorName.Nick = NickName;
            m_ExhibitorAddr.Street = "";
            m_ExhibitorAddr.City = "";
            m_ExhibitorAddr.State = "";
            m_ExhibitorAddr.Zip = -1;

            if (clsDB.Exhibits != null)
            {
                RefreshListItem();
                RefreshExhibits();
            }
        }

        public clsExhibitor(int ExhibitorNumber, string FirstName, string LastName, string NickName, string Address, string City, string State, int Zip)
        {
            m_iExNumber = ExhibitorNumber;
            m_ExhibitorName.First = FirstName;
            m_ExhibitorName.Last = LastName;
            m_ExhibitorName.Nick = NickName;
            m_ExhibitorAddr.Street = Address;
            m_ExhibitorAddr.City = City;
            m_ExhibitorAddr.State = State;
            m_ExhibitorAddr.Zip = Zip;

            if (clsDB.Exhibits != null)
            {
                RefreshListItem();
                RefreshExhibits();
            }
        }

        public override void Load(IDataReader dbReader)
        {
            m_iExNumber = (Int32)dbReader["ID"];
            m_ExhibitorName.First = dbReader["NameFirst"].ToString();
            m_ExhibitorName.Last = dbReader["NameLast"].ToString();
            m_ExhibitorName.Nick = dbReader["NameNick"].ToString();

            m_ExhibitorAddr.Street = dbReader["Address"].ToString();
            m_ExhibitorAddr.City = dbReader["City"].ToString();
            m_ExhibitorAddr.State = dbReader["State"].ToString();
            if (dbReader["Zip"] != System.DBNull.Value)
            {
                m_ExhibitorAddr.Zip = (Int32)dbReader["Zip"];
            }
            else
            {
                m_ExhibitorAddr.Zip = -1;
            }

            if (clsDB.Exhibits != null)
            {
                RefreshListItem();
                RefreshExhibits();
            }
        }

        protected override void DBCommit(DB.CommitAction Action, IDbConnection DatabaseConnection, IDbTransaction Transaction)
        {
            IDbCommand dbCommit = DatabaseConnection.CreateCommand();
            dbCommit.Transaction = Transaction;
            dbCommit.CommandText = "INSERT INTO Exhibitors (CommitAction, ID, NameFirst, NameLast, NameNick, Address, City, State, Zip) VALUES (@CommitAction, @ID, @NameFirst, @NameLast, @NameNick, @Address, @City, @State, @Zip)";

            IDbDataParameter param = dbCommit.CreateParameter();
            param.ParameterName = "@CommitAction";
            param.Value = (int)Action;
            dbCommit.Parameters.Add(param);

            param = dbCommit.CreateParameter();
            param.ParameterName = "@ID";
            param.Value = this.ExhibitorNumber;
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
            param.ParameterName = "@NameNick";
            param.Value = this.Name.Nick;
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

            dbCommit.ExecuteNonQuery();
        }

        public void RefreshListItem()
        {
            if (base.SubItems.Count <= TOTAL_LV_COLUMNS)
            {
                SubItems.Clear();
                SubItems.Add(new ListViewSubItem());
                SubItems.Add(new ListViewSubItem());
                SubItems.Add(new ListViewSubItem());
                SubItems.Add(new ListViewSubItem());
            }
            SubItems[(int)ExhibitorColumns.Number].Text = this.ExhibitorNumber.ToString();
            SubItems[(int)ExhibitorColumns.Name].Text = this.Name.ToString();
            SubItems[(int)ExhibitorColumns.Address].Text = this.Address.ToString();
            SubItems[(int)ExhibitorColumns.Exhibits].Text = this.ExhibitCount.ToString();
        }

        public void RefreshExhibits()
        {
            m_iExhibits = this.Exhibits.Count;
            SubItems[(int)ExhibitorColumns.Exhibits].Text = this.ExhibitCount.ToString();
        }

        public static bool operator ==(clsExhibitor Exhibitor1, clsExhibitor Exhibitor2)
        {
            if ((object)Exhibitor1 == null && (object)Exhibitor2 == null)
            {
                return true;
            }
            else if ((object)Exhibitor1 == null || (object)Exhibitor2 == null)
            {
                return false;
            }
            else
            {
                return (Exhibitor1.CompareTo(Exhibitor2) == 0);
            }
        }
        public static bool operator !=(clsExhibitor Exhibitor1, clsExhibitor Exhibitor2)
        {
            return !(Exhibitor1 == Exhibitor2);
        }

        public override bool Equals(object obj)
        {
            return (this == (clsExhibitor)obj);
        }
        public override int GetHashCode()
        {
            return this.ExhibitorNumber;
        }
        
        #region IComparable<clsExhibitor> Members

        public int CompareTo(clsExhibitor other)
        {
            return this.ExhibitorNumber.CompareTo(other.ExhibitorNumber);
        }

        #endregion

        #region ISerializable Members
        protected clsExhibitor(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new System.ArgumentNullException("info");
            }
            m_iExNumber = (int)info.GetValue("iExNumber", typeof(int));
            m_iExhibits = (int)info.GetValue("iExhibits", typeof(int));
            m_ExhibitorName = (Types.Name)info.GetValue("ExhibitorName", typeof(Types.Name));
            m_ExhibitorAddr = (Types.Address)info.GetValue("ExhibitorAddr", typeof(Types.Address));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new System.ArgumentNullException("info");
            }
            info.AddValue("iExNumber", m_iExNumber);
            info.AddValue("iExhibits", m_iExhibits);
            info.AddValue("ExhibitorName", m_ExhibitorName);
            info.AddValue("ExhibitorAddr", m_ExhibitorAddr);
        }
        #endregion


        public int ExhibitorNumber
        {
            get
            {
                return m_iExNumber;
            }
        }

        public new DB.Types.Name Name
        {
            get
            {
                return m_ExhibitorName;
            }
        }

        public DB.Types.Address Address
        {
            set
            {
                m_ExhibitorAddr = value;
            }
            get
            {
                return m_ExhibitorAddr;
            }
        }

        public int ExhibitCount
        {
            get
            {
                return m_iExhibits;
            }
        }

        public List<clsExhibit> Exhibits
        {
            get
            {
                return clsDB.Exhibits.FindByExhibitor(this.ExhibitorNumber);
            }
        }

        public List<clsPurchase> Purchases
        {
            get
            {
                List<clsPurchase> ReturnValue = new List<clsPurchase>();
                foreach (clsPurchase Purchase in clsDB.Purchases)
                {
                    if (Purchase.Recipient == this)
                    {
                        ReturnValue.Add(Purchase);
                    }
                }

                return ReturnValue;
            }
        }
    }

    namespace Setup
    {
        public class Exhibitors : AuctionDBSetup
        {
            public Exhibitors()
            {
                TABLE_NAME = "Exhibitors";

                TABLE_COLUMNS = new List<SQLColumn>() {
                    new SQLColumn("ID", "int", "0", true),
                    new SQLColumn("NameFirst", "nvarchar(100)", "", false),
	                new SQLColumn("NameLast", "nvarchar(100)", "", false),
	                new SQLColumn("NameNick", "nvarchar(100)", "", false),
	                new SQLColumn("Address", "nvarchar(200)", "", false),
	                new SQLColumn("City", "nvarchar(100)", "", false),
	                new SQLColumn("State", "nvarchar(100)", "", false),
	                new SQLColumn("Zip", "int", "0", false)
                };
            }
        }
    }
}
