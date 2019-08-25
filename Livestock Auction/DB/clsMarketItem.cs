using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Runtime.Serialization;
using System.Data;

namespace Livestock_Auction.DB
{
    public class clsMarketItems : clsAuctionDataCollection<IDAuctionDataKey, clsMarketItem, DB.Setup.MarketItem>
    {       
        public clsMarketItems(IDbConnection DatabaseConnection) : base(DatabaseConnection)
        {
        }

        public int NewTagNumber(clsMarketItem MarketItem)
        {
            int TagNumber = 0;

            //Query the database for the next purchase index
            IDbCommand dbLoad =  m_dbConn.CreateCommand();
            dbLoad.CommandText = "SELECT MAX(TagNumber) + 1 FROM vExhibits_Current WHERE MarketItem=@MarketItem";
            IDbDataParameter param = dbLoad.CreateParameter();
            param.ParameterName = "@MarketItem";
            param.Value = MarketItem.MarketID;
            dbLoad.Parameters.Add(param);

            //dbLoad.Parameters.AddWithValue("@MarketItem", MarketItem.MarketID);

            object objTagNumber = dbLoad.ExecuteScalar();
            if (objTagNumber == null || objTagNumber == DBNull.Value)
            {
                TagNumber = 0;
            }
            else
            {
                TagNumber = (int)objTagNumber;
            }

            //TODO: Check for uncommited purchases...

            return TagNumber;
        }

        public clsMarketItem this[string MarketType]
        {
            get
            {
                if (MarketType != null)
                {
                    lock (m_dictCollection)
                    {
                        foreach (clsMarketItem item in m_dictCollection.Values)
                        {
                            if (item.MarketType.ToLowerInvariant().Trim() == MarketType.ToLowerInvariant().Trim())
                            {
                                return item;
                            }
                        }
                    }
                }
                return null;
            }
        }

        public clsMarketItem this[clsMarketItem MarketItem]
        {
            get
            {
                return this[MarketItem.MarketID];
            }
        }

        public clsMarketItem AdditionalPurchase
        {
            get
            {
                lock (m_dictCollection)
                {
                    return m_dictCollection[0];
                }
            }
        }
    }

    [Serializable]
    public class clsMarketItem : AuctionData, IComparable<clsMarketItem>, ISerializable
    {
        int m_iMarketID;
        string m_sMarketType;
        double m_fMarketValue;
        string m_sMarketUnits;
        bool m_bAllowAdvertising;
        bool m_bValidDisposition;
        bool m_bSellByPound;

        public clsMarketItem()
        {

        }

        public clsMarketItem(int ID, string Type, double Value, string Units, bool AllowAdvertising, bool ValidDisposition, bool SellByPound)
        {
            m_iMarketID = ID;
            m_sMarketType = Type;
            m_fMarketValue = Value;
            m_sMarketUnits = Units;
            m_bAllowAdvertising = AllowAdvertising;
            m_bValidDisposition = ValidDisposition;
            m_bSellByPound = SellByPound;
        }

        public override void Load(IDataReader dbReader)
        {
            m_iMarketID = (Int32)dbReader["ID"];
            m_sMarketType = dbReader["MarketItem"].ToString();
            m_fMarketValue = System.Convert.ToDouble((decimal)dbReader["MarketPrice"]);
            m_sMarketUnits = dbReader["MarketUnits"].ToString();
            m_bAllowAdvertising = (bool)dbReader["AllowAdvertising"];
            m_bValidDisposition = (bool)dbReader["ValidDisposition"];
            m_bSellByPound = (bool)dbReader["SellByPound"];
        }

        protected override void DBCommit(DB.CommitAction Action, IDbConnection DatabaseConnection, IDbTransaction Transaction)
        {
            if (this.MarketID == -1)
            {
                //Generate a record ID
                IDbCommand dbTempCommit = DatabaseConnection.CreateCommand();
                dbTempCommit.Transaction = Transaction;
                string sTmpName = Guid.NewGuid().ToString();

                //It would seem that you can't put parameters into a SELECT clause (at last in SQL CE). 
                dbTempCommit.CommandText = "INSERT INTO Market (CommitAction, ID, MarketItem, MarketPrice, MarketUnits, AllowAdvertising, ValidDisposition) SELECT '" + ((int)CommitAction.Modify).ToString() + "' AS CommitAction, MAX(ID) + 1 AS ID, '" + sTmpName + "' AS MarketItem, 0 AS MarketValue, '' AS MarketUnits, 0 AS AllowAdvertising, 0 AS ValidDisposition FROM Market";
                dbTempCommit.ExecuteNonQuery();

                dbTempCommit.CommandText = "SELECT ID FROM Market WHERE MarketItem = @MarketItem ORDER BY CommitDate DESC";
                IDbDataParameter tmpparam = dbTempCommit.CreateParameter();
                tmpparam.ParameterName = "@MarketItem";
                tmpparam.Value = sTmpName;
                dbTempCommit.Parameters.Add(tmpparam);

                this.MarketID = (int)dbTempCommit.ExecuteScalar();

                //TODO: Hack because of getdate SQL CE precision, once that is fixed this wait can be removed.
                System.Threading.Thread.Sleep(10);
            }


            IDbCommand dbCommit = DatabaseConnection.CreateCommand();
            dbCommit.Transaction = Transaction;
            dbCommit.CommandText = "INSERT INTO Market (CommitAction, ID, MarketItem, MarketPrice, MarketUnits, AllowAdvertising, ValidDisposition, SellByPound) VALUES (@CommitAction, @MarketID, @MarketType, @MarketValue, @MarketUnits, @AllowAdvertising, @ValidDisposition, @SellByPound)";
            
            IDbDataParameter param = dbCommit.CreateParameter();
            param.ParameterName = "@CommitAction";
            param.Value = (int)Action;
            dbCommit.Parameters.Add(param);

            param = dbCommit.CreateParameter();
            param.ParameterName = "@MarketID";
            param.Value = this.MarketID;
            dbCommit.Parameters.Add(param);
            
            param = dbCommit.CreateParameter();
            param.ParameterName = "@MarketType";
            param.Value = this.MarketType;
            dbCommit.Parameters.Add(param);

            param = dbCommit.CreateParameter();
            param.ParameterName = "@MarketValue";
            param.Value = this.MarketValue;
            dbCommit.Parameters.Add(param);

            param = dbCommit.CreateParameter();
            param.ParameterName = "@MarketUnits";
            param.Value = this.MarketUnits;
            dbCommit.Parameters.Add(param);

            param = dbCommit.CreateParameter();
            param.ParameterName = "@AllowAdvertising";
            param.Value = this.AllowAdvertising;
            dbCommit.Parameters.Add(param);

            param = dbCommit.CreateParameter();
            param.ParameterName = "@ValidDisposition";
            param.Value = this.ValidDisposition;
            dbCommit.Parameters.Add(param);

            param = dbCommit.CreateParameter();
            param.ParameterName = "@SellByPound";
            param.Value = this.SellByPound;
            dbCommit.Parameters.Add(param);
            
            dbCommit.ExecuteNonQuery();
        }

        public int NewTag()
        {
            return clsDB.Market.NewTagNumber(this);
        }

        public override int GetHashCode()
        {
            return m_iMarketID;
        }

        public static bool operator ==(clsMarketItem Item1, clsMarketItem Item2)
        {
            if ((object)Item1 == null && (object)Item2 == null)
            {
                return true;
            }
            else if ((object)Item1 == null || (object)Item2 == null)
            {
                return false;
            }
            else
            {
                return (Item1.CompareTo(Item2) == 0);
            }
        }
        public static bool operator !=(clsMarketItem Item1, clsMarketItem Item2)
        {
            return !(Item1 == Item2);
        }

        #region IComparable<clsMarketItem> Members

        public int CompareTo(clsMarketItem other)
        {
            return this.MarketID.CompareTo(other.MarketID);
        }

        #endregion

        #region ISerializable Members
        protected clsMarketItem(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new System.ArgumentNullException("info");
            }
            m_iMarketID = (int)info.GetValue("iMarketID", typeof(int));
            m_sMarketType = (string)info.GetValue("sMarketType", typeof(string));
            m_fMarketValue = (double)info.GetValue("fMarketValue", typeof(double));
            m_sMarketUnits = (string)info.GetValue("sMarketUnits", typeof(string));
            m_bAllowAdvertising = (bool)info.GetValue("bAllowAdvertising", typeof(bool));
            m_bValidDisposition = (bool)info.GetValue("bValidDisposition", typeof(bool));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new System.ArgumentNullException("info");
            }
            info.AddValue("iMarketID", m_iMarketID);
            info.AddValue("sMarketType", m_sMarketType);
            info.AddValue("fMarketValue", m_fMarketValue);
            info.AddValue("sMarketUnits", m_sMarketUnits);
            info.AddValue("bAllowAdvertising", m_bAllowAdvertising);
            info.AddValue("bValidDisposition", m_bValidDisposition);
        }
        #endregion

        public int MarketID
        {
            get
            {
                return m_iMarketID;
            }
            set
            {
                m_iMarketID = value;
            }
        }

        public string MarketType
        {
            get
            {
                return m_sMarketType;
            }
            set
            {
                m_sMarketType = value;
            }
        }

        public double MarketValue
        {
            get
            {
                return m_fMarketValue;
            }
            set
            {
                m_fMarketValue = value;
            }
        }

        public string MarketUnits
        {
            get
            {
                return m_sMarketUnits;
            }
            set
            {
                m_sMarketUnits = value;
            }
        }

        public string MarketValue_String
        {
            get
            {
                if (m_bAllowAdvertising)
                {
                    return m_fMarketValue.ToString(String.Format("$#,##0.00/{0}", m_sMarketUnits));
                }
                else
                {
                    return "N/A";
                }
            }
        }

        public bool AllowAdvertising
        {
            get
            {
                return m_bAllowAdvertising;
            }
            set
            {
                m_bAllowAdvertising = value;
            }
        }

        public bool ValidDisposition
        {
            get
            {
                return m_bValidDisposition;
            }
            set
            {
                m_bValidDisposition = value;
            }
        }

        public bool SellByPound
        {
            get
            {
                return m_bSellByPound;
            }
            set
            {
                m_bSellByPound = value;
            }
        }
    }

    namespace Setup
    {
        public class MarketItem : AuctionDBSetup
        {
            public MarketItem()
            {
                TABLE_NAME = "Market";

                TABLE_COLUMNS = new List<SQLColumn>() {
                    new SQLColumn("ID", "int", "0", true),
                    new SQLColumn("MarketItem", "nvarchar(50)", "", false),
                    new SQLColumn("MarketPrice", "money", "0", false),
                    new SQLColumn("MarketUnits", "nvarchar(20)", "", false),
                    new SQLColumn("AllowAdvertising", "bit", "0", false),
                    new SQLColumn("ValidDisposition", "bit", "0", false),
                    new SQLColumn("SellByPound", "bit", "0", false),
                };
            }

            //Use the post setup steps to insert the additional payment record
            public override void SQLPostSetupSteps(IDbConnection DatabaseConnection)
            {
                clsMarketItem AddPayment = new clsMarketItem(0, "Additional Payment", 0, "", false, false, false);
                AddPayment.Commit(DatabaseConnection);
            }
        }
    }
}
