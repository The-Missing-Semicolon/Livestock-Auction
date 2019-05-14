using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Runtime.Serialization;
using System.Data;
using System.Data.Common;
using System.Data.SqlServerCe;

namespace Livestock_Auction.DB
{
    public class clsAuctionDataCollection<KeyType, AuctionType, SetupClass> : IEnumerable<AuctionType>
        where KeyType : AuctionDataKey, new()
        where AuctionType : AuctionData, new()
        where SetupClass : Setup.AuctionDBSetup, new()
    {
        public event EventHandler<DatabaseUpdatedEventArgs> Updated;

        protected IDbConnection m_dbConn;
        protected DateTime m_dtLastUpdate;

        protected Dictionary<KeyType, AuctionType> m_dictCollection;

        protected SetupClass m_Setup = null;

        public static SetupClass Setup
        {
            get
            {
                return new SetupClass();
            }
        }

        public clsAuctionDataCollection(IDbConnection DatabaseConnection)
        {
            m_dbConn = DatabaseConnection;
            m_dtLastUpdate = (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue;
            Load();
        }

        public virtual void Load()
        {
            m_dictCollection = new Dictionary<KeyType, AuctionType>();
            m_Setup = new SetupClass();
            IDataReader DataReader = m_Setup.SQLLoadDataQuery(m_dbConn).ExecuteReader();

            while (DataReader.Read())
            {
                AuctionType AuctionElement = new AuctionType();
                AuctionElement.Load(DataReader);

                KeyType AuctionKey = new KeyType();
                AuctionKey.Load(DataReader);

                lock (m_dictCollection)
                {
                    m_dictCollection.Add(AuctionKey, AuctionElement);
                }

                if (DataReader["LastUpdated"] is DateTime)
                {
                    m_dtLastUpdate = (DateTime)DataReader["LastUpdated"];
                }
                else
                {
                    m_dtLastUpdate = DateTime.Parse(DataReader["LastUpdated"].ToString());
                }
            }
            DataReader.Close();
        }

        public virtual void ConnectEvents()
        {
        }

        public virtual void Update(IDbConnection Connection)
        {
            try
            {
                DatabaseUpdatedEventArgs UpdateArgs = new DatabaseUpdatedEventArgs();
                IDbCommand dbUpdate = m_Setup.SQLUpdateDataQuery(Connection);

                IDbDataParameter param = dbUpdate.CreateParameter();
                param.ParameterName = "@LastUpdate";
                if (param is SqlCeParameter)
                {
                    param.Value = m_dtLastUpdate.ToString("O");
                }
                else
                {
                    param.DbType = DbType.DateTime2;
                    param.Value = m_dtLastUpdate;
                }
                
                dbUpdate.Parameters.Add(param);

                IDataReader DataReader = dbUpdate.ExecuteReader();

                try
                {
                    while (DataReader.Read())
                    {
                        try
                        {

                            //Apply the changes to the dictionary
                            AuctionType AuctionElement = new AuctionType();
                            AuctionElement.Load(DataReader);

                            KeyType AuctionKey = new KeyType();
                            AuctionKey.Load(DataReader);

                            lock (m_dictCollection)
                            {
                                if ((DB.CommitAction)int.Parse(DataReader["CommitAction"].ToString()) == DB.CommitAction.Modify)
                                {
                                    //Modifications and addtions....
                                    if (m_dictCollection.ContainsKey(AuctionKey))
                                    {
                                        m_dictCollection[AuctionKey] = AuctionElement;
                                    }
                                    else
                                    {
                                        m_dictCollection.Add(AuctionKey, AuctionElement);
                                    }
                                }
                                else
                                {
                                    //Deletes.....
                                    m_dictCollection.Remove(AuctionKey);
                                }
                            }

                            //Keep Track up what was changed for the updated event
                            UpdateArgs.UpdatedItems.Add(AuctionElement, (DB.CommitAction)int.Parse(DataReader["CommitAction"].ToString()));

                            //Keep track of the last update time
                            DateTime dtLastUpdate;
                            if (DataReader["CommitDate"] is System.String)
                            {
                                //TODO: Give preference to sql server datatype.
                                dtLastUpdate = DateTime.Parse((string)DataReader["CommitDate"]);
                            }
                            else
                            {
                                dtLastUpdate = (DateTime)DataReader["CommitDate"];
                            }
                            if (dtLastUpdate > m_dtLastUpdate)
                            {
                                m_dtLastUpdate = dtLastUpdate;
                            }
                        }
                        catch (Exception ex)
                        {
                            clsLogger.WriteLog(string.Format("An error occured processing update for auction data:\r\nMessage:{1}\r\nStack Trace:\r\n{2}\r\n", ex.Message, ex.StackTrace));
                        }
                    }
                }
                catch (Exception ex)
                {
                    clsLogger.WriteLog(string.Format("An error occured updating auction data while the reader was open:\r\nMessage:{0}\r\nStack Trace:\r\n{1}\r\n", ex.Message, ex.StackTrace));
                }
                DataReader.Close();

                //Call the updated event if nessesary
                if (UpdateArgs.UpdatedItems.Count > 0)
                {
                    onUpdated(UpdateArgs);
                }
            }
            catch (Exception ex)
            {
                clsLogger.WriteLog(string.Format("An error occured updating auction data:\r\nMessage:{0}\r\nStack Trace:\r\n{1}\r\n", ex.Message, ex.StackTrace));
            }
        }

        public void Commit(DB.CommitAction Action, DB.AuctionData Record)
        {
            try
            {
                if (m_dbConn.State != System.Data.ConnectionState.Open)
                {
                    clsLogger.WriteLog("Lost database connection during commit, attempting to reconnect...");
                    m_dbConn.Open();
                }
            }
            catch (Exception ex)
            {
                clsLogger.WriteLog(string.Format("An error occured while connecting to the database during commit:\r\nConnection String:{0}\r\nMessage:{1}\r\nStack Trace:\r\n{2}", m_dbConn.ConnectionString, ex.Message, ex.StackTrace));
            }

            if (Action == DB.CommitAction.Modify)
            {
                Record.Commit(m_dbConn);
            }
            else
            {
                Record.Delete(m_dbConn);
            }
            Update();
        }

        public void Commit(DB.CommitAction Action, DB.AuctionData Record, IDbTransaction Transaction)
        {
            if (Action == DB.CommitAction.Modify)
            {
                Record.Commit(m_dbConn, Transaction);
            }
            else
            {
                Record.Delete(m_dbConn, Transaction);
            }
        }

        public void Update()
        {
            try
            {
                if (m_dbConn.State != System.Data.ConnectionState.Open)
                {
                    clsLogger.WriteLog("Lost database connection during update, attempting to reconnect...");
                    m_dbConn.Open();
                }
            }
            catch (Exception ex)
            {
                clsLogger.WriteLog(string.Format("An error occured while connecting to the database during updaate:\r\nConnection String:{0}\r\nMessage:{1}\r\nStack Trace:\r\n{2}", m_dbConn.ConnectionString, ex.Message, ex.StackTrace));
            }
            Update(m_dbConn);
        }

        protected virtual void onUpdated(DatabaseUpdatedEventArgs e)
        {
            if (Updated != null)
            {
                Updated(this, e);
            }
        }

        #region IEnumerable<AuctionType> Members

        public IEnumerator<AuctionType> GetEnumerator()
        {
            //For thread saftey, return an enumerator for a copy of the dictionary
            AuctionType[] DataCopy = null;
            lock (m_dictCollection)
            {
                DataCopy = new AuctionType[m_dictCollection.Values.Count];
                m_dictCollection.Values.CopyTo(DataCopy, 0);
            }

            return DataCopy.AsEnumerable().GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            //For thread saftey, return an enumerator for a copy of the dictionary
            AuctionType[] DataCopy = null;
            lock (m_dictCollection)
            {
                DataCopy = new AuctionType[m_dictCollection.Values.Count];
                m_dictCollection.Values.CopyTo(DataCopy, 0);
            }

            return DataCopy.GetEnumerator();
        }

        #endregion

        public AuctionType this[KeyType Key]
        {
            get
            {
                AuctionType data = null;
                lock (m_dictCollection)
                {
                    if (m_dictCollection.ContainsKey(Key))
                    {
                        data = m_dictCollection[Key];
                    }
                }
                return data;
            }
        }

        public int Count
        {
            get
            {
                int iCount = 0;
                lock (m_dictCollection)
                {
                    iCount = m_dictCollection.Count;
                }
                return iCount;
            }
        }
    }

    public abstract class AuctionDataKey
    {
        public AuctionDataKey()
        {
        }

        public virtual void Load(IDataReader dbReader)
        {

        }
    }

    public class IDAuctionDataKey : AuctionDataKey
    {
        int m_iID = -1;

        public IDAuctionDataKey()
        {
        }

        public IDAuctionDataKey(int ID)
        {
            m_iID = ID;
        }

        public override void Load(IDataReader dbReader)
        {
            m_iID = (Int32)dbReader["ID"];
        }

        public override int GetHashCode()
        {
            return m_iID;
        }

        public override bool Equals(object obj)
        {
            IDAuctionDataKey Key = (IDAuctionDataKey)obj;
            if (obj.GetType() == this.GetType())
            {
                return (this.m_iID == Key.m_iID);
            }
            else
            {
                return false;
            }
        }

        public static implicit operator int(IDAuctionDataKey Key)
        {
            return Key.m_iID;
        }

        public static implicit operator IDAuctionDataKey(int Key)
        {
            return new IDAuctionDataKey(Key);
        }
    }

    //Abstract class used for the "DatabaseUpdatedEventArgs" class
    public abstract class AuctionData : System.Windows.Forms.ListViewItem
    {
        //Initialize the AuctionData from a data reader object
        public virtual void Load(IDataReader dbReader)
        {
            // Unfortunately, C# (at least as of version 3.5) does not seem to
            //  allow for virtual constructors or the passing of arguments to
            //  constructors on generic types.

            // There are some work arounds posted for this online, including
            //  Activator.CreateInstance, but all of the documentation warns
            //  about signifigant preformance hits when compared to calling the
            //  constructor directly (not that that is likely to matter on this
            //  scale). Doing this the "right" way also allows the compiler to
            //  catch the errors when I mess it up.
        }

        protected virtual void DBCommit(DB.CommitAction Action, IDbConnection DatabaseConnection, IDbTransaction Transaction)
        {

        }

        public void Commit(IDbConnection DatabaseConnection, IDbTransaction Transaction)
        {
            DBCommit(DB.CommitAction.Modify, DatabaseConnection, Transaction);
        }

        public void Commit(IDbConnection DatabaseConnection)
        {
            DBCommit(DB.CommitAction.Modify, DatabaseConnection, null);
        }

        public void Delete(IDbConnection DatabaseConnection, IDbTransaction Transaction)
        {
            DBCommit(DB.CommitAction.Delete, DatabaseConnection, Transaction);
        }

        public void Delete(IDbConnection DatabaseConnection)
        {
            DBCommit(DB.CommitAction.Delete, DatabaseConnection, null);
        }


    }

    namespace Setup
    {
        public abstract class AuctionDBSetup
        {
            public static void BuildTable(AuctionDBSetup DBSetup, IDbConnection DatabaseConnection)
            {
                //Create the table...
                try
                {
                    DBSetup.SQLGenerateTable(DatabaseConnection).ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 2714)
                    {
                        //The table already exists
                        //TODO: The table already exists, check the columns and add any missing ones
                    }
                    else
                    {
                        throw ex;
                    }
                }

                //Create views for SqlConnections only (Sql CE does not support views)
                if (DatabaseConnection is SqlConnection)
                {
                    IDbCommand dbCurrentView = DBSetup.SQLGenerateViewCurrent(DatabaseConnection);
                    try
                    {
                        dbCurrentView.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        if (ex.Number == 2714)
                        {
                            //The view already exists, change it to an ALTER view statement
                            if (dbCurrentView.CommandText.StartsWith("CREATE"))
                            {
                                dbCurrentView.CommandText = dbCurrentView.CommandText.Remove(0, "CREATE".Length);
                                dbCurrentView.CommandText = "ALTER" + dbCurrentView.CommandText;
                                dbCurrentView.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            throw ex;
                        }
                    }

                    IDbCommand dbJournalView = DBSetup.SQLGenerateViewJournal(DatabaseConnection);
                    try
                    {
                        dbJournalView.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        if (ex.Number == 2714)
                        {
                            //The view already exists, change it to an ALTER view statement
                            if (dbJournalView.CommandText.StartsWith("CREATE"))
                            {
                                dbJournalView.CommandText = dbCurrentView.CommandText.Remove(0, "CREATE".Length);
                                dbJournalView.CommandText = "ALTER" + dbCurrentView.CommandText;
                                dbJournalView.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            throw ex;
                        }
                    }
                }

                DBSetup.SQLPostSetupSteps(DatabaseConnection);

            }

            public static void CopyData(AuctionDBSetup DBSetup, IDbConnection FromDatabase, IDbConnection ToDatabase)
            {
                DBSetup.SQLCopyData(FromDatabase, ToDatabase);
            }

            public struct SQLColumn
            {
                public string Name;
                public string Type;
                public string DefaultValue;
                public bool PrimaryKey;

                public SQLColumn(string ColumnName, string ColumnType, string ColumnDefaultValue, bool ColumnIsPrimaryKey)
                {
                    Name = ColumnName;
                    Type = ColumnType;
                    DefaultValue = ColumnDefaultValue;
                    PrimaryKey = ColumnIsPrimaryKey;
                }
            }


            public string TABLE_NAME;

            protected const string SFMT_CURRENT = "v{0}_Current";
            protected const string SFMT_JOURNAL = "v{0}_Journal";

            public List<SQLColumn> TABLE_COLUMNS;

            protected string m_sCachedLoadQuery = "";
            protected string m_sCachedUpdateQuery = "";

            //Build the command to generate the table for the database. This can
            //  be overridden if the the table needs to be structured differently.
            public virtual IDbCommand SQLGenerateTable(IDbConnection DatabaseConnection)
            {
                string sCode = "";
                string sKey = "CommitDate, ";
                sCode += string.Format("CREATE TABLE {0} (", TABLE_NAME);
                if (DatabaseConnection is SqlConnection)
                {
                    sCode += "CommitDate DateTime2(7) DEFAULT (sysdatetime()),";
                }
                else
                {
                    //SQL Server Compact edition 3.5 converts DateTime2 to nvarchar...
                    //  See https://msdn.microsoft.com/en-us/library/ms173018.aspx

                    //TODO: getdate() only has precision down to the minute, that is not going to fly for this purpose. Need to find a SQLCE function date function that (ideally) has sub-second precision.
                    //  Alternatively, modify code to provide the time for SQL CE connections.
                    //sCode += "CommitDate nvarchar(27) DEFAULT (getdate()),";
                    sCode += "CommitDate nvarchar(27) DEFAULT (CONVERT(nvarchar, GETDATE(), 126)),";
                }
                sCode += "CommitAction int, ";
                foreach (SQLColumn Column in TABLE_COLUMNS)
                {
                    sCode += string.Format("{0} {1}, ", Column.Name, Column.Type);
                    if (Column.PrimaryKey)
                    {
                        sKey += string.Format("{0}, ", Column.Name);
                    }
                }
                sCode += string.Format(" PRIMARY KEY ({0}) )", sKey.TrimEnd(',', ' '));

                IDbCommand dbComm = DatabaseConnection.CreateCommand();
                dbComm.CommandText = sCode;

                return dbComm;
            }

            //Copy table data from one database to another
            public virtual void SQLCopyData(IDbConnection FromDatabase, IDbConnection ToDatabase)
            {
                //Delete any existing data in the table...
                IDbCommand dbComm = ToDatabase.CreateCommand();
                dbComm.CommandText = string.Format("DELETE FROM {0}", TABLE_NAME);
                dbComm.ExecuteNonQuery();

                //Build query for selecting existing records
                string[] sColumns = new string[TABLE_COLUMNS.Count];
                for (int i = 0; i < TABLE_COLUMNS.Count; i++)
                {
                    sColumns[i] = TABLE_COLUMNS[i].Name;
                }
                
                dbComm = FromDatabase.CreateCommand();
                dbComm.CommandText = string.Format("SELECT CommitDate, CommitAction, {0} FROM {1} ORDER BY CommitDate ASC", string.Join(", ", sColumns), TABLE_NAME);

                IDataReader dbSource = dbComm.ExecuteReader();
                while (dbSource.Read())
                {
                    IDbCommand dbInsert = ToDatabase.CreateCommand();
                    //dbInsert.CommandText = string.Format("INSERT INTO {0} (CommitDate, CommitAction, {1}) VALUES (@CommitDate, @CommitAction, @{2})", TABLE_NAME, string.Join(", ", sColumns), string.Join(", @", sColumns));
                    dbInsert.CommandText = string.Format("INSERT INTO {0} (CommitAction, {1}) VALUES (@CommitAction, @{2})", TABLE_NAME, string.Join(", ", sColumns), string.Join(", @", sColumns));

                    IDbDataParameter dbParam = dbInsert.CreateParameter();
                    //dbParam.ParameterName = "@CommitDate";
                    //if (dbSource.GetValue(0) is DateTime)
                    //{
                    //    dbParam.Value = ((DateTime)dbSource.GetValue(0)).ToString("yyyy-MM-dd hh:mm:ss.fffffff");
                    //}
                    //else
                    //{
                    //    dbParam.Value = dbSource.GetValue(0);
                    //}
                    //dbInsert.Parameters.Add(dbParam);

                    dbParam = dbInsert.CreateParameter();
                    dbParam.ParameterName = "@CommitAction";
                    dbParam.Value = dbSource.GetValue(1);
                    dbInsert.Parameters.Add(dbParam);

                    for (int i = 0; i < TABLE_COLUMNS.Count; i++)
                    {
                        dbParam = dbInsert.CreateParameter();
                        dbParam.ParameterName = string.Format("@{0}", TABLE_COLUMNS[i].Name);
                        dbParam.Value = dbSource.GetValue(i + 2);
                        dbInsert.Parameters.Add(dbParam);
                    }
                    try
                    {
                        dbInsert.ExecuteNonQuery();
                    }
                    catch (System.Data.Common.DbException)
                    {
                        System.Threading.Thread.Sleep(50);
                        dbInsert.ExecuteNonQuery();
                    }
                    
                }
                dbSource.Close();
            }

            //Build the command to generate the "Current" view. This view is represents
            //  the current state of the table, and is used to more efficently load the
            //  inital state of the database when the "Load" method is called. Can be
            //  overriden if a class requires slightly different functionality
            public virtual IDbCommand SQLGenerateViewCurrent(IDbConnection DatabaseConnection)
            {
                string sCode = "";
                //Build the select
                sCode += string.Format("CREATE VIEW {0} AS SELECT {1}.CommitDate, ", ViewCurrent, TABLE_NAME);

                foreach (SQLColumn Column in TABLE_COLUMNS)
                {
                    sCode += string.Format("{0}.{1}, ", TABLE_NAME, Column.Name);
                }
                sCode += "LastUpdate.LastUpdated";
                //Build the last modified subquery
                sCode += string.Format(" FROM {0} INNER JOIN (SELECT ", TABLE_NAME);
                foreach (SQLColumn Column in TABLE_COLUMNS)
                {
                    if (Column.PrimaryKey)
                    {
                        sCode += string.Format("{0}, ", Column.Name);
                    }
                }
                sCode += string.Format("MAX(CommitDate) AS CommitDate FROM {0} GROUP BY ", TABLE_NAME);
                //Setting up the group by for the last modified query
                foreach (SQLColumn Column in TABLE_COLUMNS)
                {
                    if (Column.PrimaryKey)
                    {
                        sCode += string.Format("{0}, ", Column.Name);
                    }
                }
                sCode = sCode.TrimEnd(',', ' ');

                sCode += string.Format(") AS LastModified ON {0}.CommitDate = LastModified.CommitDate", TABLE_NAME);
                //Setup the join criteria for the subquery
                foreach (SQLColumn Column in TABLE_COLUMNS)
                {
                    if (Column.PrimaryKey)
                    {
                        sCode += string.Format(" AND {0}.{1} = LastModified.{1}", TABLE_NAME, Column.Name);
                    }
                }
                //Add in the last updated query
                sCode += string.Format(", (SELECT Max(CommitDate) AS LastUpdated FROM {0}) AS LastUpdate", TABLE_NAME);

                //Filter out deleted records
                sCode += string.Format(" WHERE {0}.CommitAction = 0", TABLE_NAME);

                IDbCommand dbComm = DatabaseConnection.CreateCommand();
                dbComm.CommandText = sCode;
                return dbComm;
            }

            //Build the command to generate the "Journal" view. This view is represents
            //  the most current changes to the table, and is used to more efficently
            //  query for updates to the table "Update" method is called. Can be
            //  overriden if the class requires slightly different functionality.
            public virtual IDbCommand SQLGenerateViewJournal(IDbConnection DatabaseConnection)
            {
                string sCode = "";
                //Build the select
                sCode += string.Format("CREATE VIEW {0} AS SELECT {1}.CommitDate, {1}.CommitAction", ViewJournal, TABLE_NAME);
                foreach (SQLColumn Column in TABLE_COLUMNS)
                {
                    sCode += string.Format(", {0}.{1}", TABLE_NAME, Column.Name);
                }
                //Start building the subquery
                sCode += string.Format(" FROM {0} INNER JOIN (SELECT ", TABLE_NAME);
                foreach (SQLColumn Column in TABLE_COLUMNS)
                {
                    if (Column.PrimaryKey)
                    {
                        sCode += string.Format("{0}, ", Column.Name);
                    }
                }
                //Add in the group by condition for the sub query
                sCode += string.Format(" MAX(CommitDate) AS CommitDate FROM {0} GROUP BY ", TABLE_NAME);
                foreach (SQLColumn Column in TABLE_COLUMNS)
                {
                    if (Column.PrimaryKey)
                    {
                        sCode += string.Format("{0}, ", Column.Name);
                    }
                }
                sCode = sCode.TrimEnd(',', ' ');
                sCode += string.Format(") AS LastModified ON {0}.CommitDate = LastModified.CommitDate", TABLE_NAME);
                //Setup the join criteria for the subquery
                foreach (SQLColumn Column in TABLE_COLUMNS)
                {
                    if (Column.PrimaryKey)
                    {
                        sCode += string.Format(" AND {0}.{1} = LastModified.{1}", TABLE_NAME, Column.Name);
                    }
                }

                IDbCommand dbComm = DatabaseConnection.CreateCommand();
                dbComm.CommandText = sCode;
                return dbComm;
            }

            //Build and cache the load query. This is the query that is used to
            //  efficiently initalize the collection.
            public virtual IDbCommand SQLLoadDataQuery(IDbConnection DatabaseConnection)
            {
                if (m_sCachedLoadQuery == "")
                {
                    //Build the select
                    m_sCachedLoadQuery += string.Format("SELECT {1}.CommitDate, ", ViewCurrent, TABLE_NAME);

                    foreach (SQLColumn Column in TABLE_COLUMNS)
                    {
                        m_sCachedLoadQuery += string.Format("{0}.{1}, ", TABLE_NAME, Column.Name);
                    }
                    m_sCachedLoadQuery += "LastUpdate.LastUpdated";
                    //Build the last modified subquery
                    m_sCachedLoadQuery += string.Format(" FROM {0} INNER JOIN (SELECT ", TABLE_NAME);
                    foreach (SQLColumn Column in TABLE_COLUMNS)
                    {
                        if (Column.PrimaryKey)
                        {
                            m_sCachedLoadQuery += string.Format("{0}, ", Column.Name);
                        }
                    }
                    m_sCachedLoadQuery += string.Format("MAX(CommitDate) AS CommitDate FROM {0} GROUP BY ", TABLE_NAME);
                    //Setting up the group by for the last modified query
                    foreach (SQLColumn Column in TABLE_COLUMNS)
                    {
                        if (Column.PrimaryKey)
                        {
                            m_sCachedLoadQuery += string.Format("{0}, ", Column.Name);
                        }
                    }
                    m_sCachedLoadQuery = m_sCachedLoadQuery.TrimEnd(',', ' ');

                    m_sCachedLoadQuery += string.Format(") AS LastModified ON {0}.CommitDate = LastModified.CommitDate", TABLE_NAME);
                    //Setup the join criteria for the subquery
                    foreach (SQLColumn Column in TABLE_COLUMNS)
                    {
                        if (Column.PrimaryKey)
                        {
                            m_sCachedLoadQuery += string.Format(" AND {0}.{1} = LastModified.{1}", TABLE_NAME, Column.Name);
                        }
                    }
                    //Add in the last updated query
                    m_sCachedLoadQuery += string.Format(", (SELECT Max(CommitDate) AS LastUpdated FROM {0}) AS LastUpdate", TABLE_NAME);

                    //Filter out deleted records
                    m_sCachedLoadQuery += string.Format(" WHERE {0}.CommitAction = 0", TABLE_NAME);
                }

                IDbCommand dbComm = DatabaseConnection.CreateCommand();
                dbComm.CommandText = m_sCachedLoadQuery;
                return dbComm;
            }

            //Build and cache the update query. This is the query that is used to
            //  efficiently update the collection.
            public virtual IDbCommand SQLUpdateDataQuery(IDbConnection DatabaseConnection)
            {
                if (m_sCachedUpdateQuery == "")
                {
                    //Build the select
                    m_sCachedUpdateQuery += string.Format("SELECT {1}.CommitDate, {1}.CommitAction", ViewJournal, TABLE_NAME);
                    foreach (SQLColumn Column in TABLE_COLUMNS)
                    {
                        m_sCachedUpdateQuery += string.Format(", {0}.{1}", TABLE_NAME, Column.Name);
                    }
                    //Start building the subquery
                    m_sCachedUpdateQuery += string.Format(" FROM {0} INNER JOIN (SELECT ", TABLE_NAME);
                    foreach (SQLColumn Column in TABLE_COLUMNS)
                    {
                        if (Column.PrimaryKey)
                        {
                            m_sCachedUpdateQuery += string.Format("{0}, ", Column.Name);
                        }
                    }
                    //Add in the group by condition for the sub query
                    m_sCachedUpdateQuery += string.Format(" MAX(CommitDate) AS CommitDate FROM {0} GROUP BY ", TABLE_NAME);
                    foreach (SQLColumn Column in TABLE_COLUMNS)
                    {
                        if (Column.PrimaryKey)
                        {
                            m_sCachedUpdateQuery += string.Format("{0}, ", Column.Name);
                        }
                    }
                    m_sCachedUpdateQuery = m_sCachedUpdateQuery.TrimEnd(',', ' ');
                    m_sCachedUpdateQuery += string.Format(") AS LastModified ON {0}.CommitDate = LastModified.CommitDate", TABLE_NAME);
                    //Setup the join criteria for the subquery
                    foreach (SQLColumn Column in TABLE_COLUMNS)
                    {
                        if (Column.PrimaryKey)
                        {
                            m_sCachedUpdateQuery += string.Format(" AND {0}.{1} = LastModified.{1}", TABLE_NAME, Column.Name);
                        }
                    }
                    m_sCachedUpdateQuery += string.Format(" WHERE {0}.CommitDate > @LastUpdate ORDER BY ", TABLE_NAME);
                    foreach (SQLColumn Column in TABLE_COLUMNS)
                    {
                        if (Column.PrimaryKey)
                        {
                            m_sCachedUpdateQuery += string.Format("{0}, ", Column.Name);
                        }
                    }
                    m_sCachedUpdateQuery = m_sCachedUpdateQuery.TrimEnd(',', ' ');
                }

                IDbCommand dbComm = DatabaseConnection.CreateCommand();
                dbComm.CommandText = m_sCachedUpdateQuery;
                return dbComm;
            }

            //Any steps to run after the table and views are created. Could be
            //  used to insert default records, create additional views, or
            //  create stored procedures
            public virtual void SQLPostSetupSteps(IDbConnection DatabaseConnection)
            {
                return;
            }

            public string ViewCurrent
            {
                get
                {
                    return string.Format(SFMT_CURRENT, TABLE_NAME);
                }
            }

            public string ViewJournal
            {
                get
                {
                    return string.Format(SFMT_JOURNAL, TABLE_NAME);
                }
            }
        }
    }

    delegate void UpdateInvoker(Dictionary<AuctionData, DB.CommitAction> UpdatedItems);

    public class DatabaseUpdatedEventArgs : EventArgs
    {
        Dictionary<AuctionData, DB.CommitAction> dictUpdates;

        public DatabaseUpdatedEventArgs()
        {
            dictUpdates = new Dictionary<AuctionData, DB.CommitAction>();
        }

        public Dictionary<AuctionData, DB.CommitAction> UpdatedItems
        {
            get
            {
                return dictUpdates;
            }
        }
    }
}
