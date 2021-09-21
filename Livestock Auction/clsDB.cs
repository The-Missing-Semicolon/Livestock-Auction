using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Threading;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Livestock_Auction
{
    namespace DB
    {
        public enum CommitAction
        {
            Modify = 0,
            Delete = 1,
            None = 2
        }

        public enum NoYes
        {
            NotSet = 0,
            No = 1,
            Yes = 2
        }

        //ChampionStatus Type
        //  Encapsulates enumeration as well as converting between integer types
        // for storage and strings for display.
        public class ChampionState : IComparable<ChampionState>, IComparable<int>, ISerializable
        {
            public const int Grand_Champion = 1;
            public const int Reserve_Champion = 2;
            public const int Other = 3;
            public const int Undefined = 0;

            //public const int Rate_Of_Gain = 1;
            //public const int Grand_Champion_ROG = 2;
            //public const int Grand_Champion = 3;
            //public const int Reserve_Champion_ROG = 4;
            //public const int Reserve_Champion = 5;
            //public const int Other = 6;
            //public const int Undefined = 0;


            static Dictionary<int, string> dictChampionStatus = new Dictionary<int, string>()
            {
                {ChampionState.Grand_Champion, "Grand Champion"},
                {ChampionState.Reserve_Champion, "Reserve Champion"},
                {ChampionState.Other, "Other"}
            };

            //static Dictionary<int, string> dictChampionFullStrings = new Dictionary<int, string>()
            //{
            //    {ChampionState.Rate_Of_Gain, "Rate of Gain"},
            //    {ChampionState.Grand_Champion_ROG, "Grand Champion & Rate of Gain"},
            //    {ChampionState.Grand_Champion, "Grand Champion"},
            //    {ChampionState.Reserve_Champion_ROG, "Reserve Champion & Rate of Gain"},
            //    {ChampionState.Reserve_Champion, "Reserve Champion"},
            //    {ChampionState.Other, "Other"}
            //};
            
            int m_iState = Undefined;

            private ChampionState(int State)
            {
                m_iState = State;
            }

            private ChampionState(string State)
            {
                if (State != null)
                {
                    if (State != "")
                    {
                        foreach (KeyValuePair<int, string> Champion in dictChampionStatus)
                        {
                            if (Champion.Value.ToLowerInvariant().Trim() == State.ToLowerInvariant().Trim())
                            {
                                m_iState = Champion.Key;
                            }
                        }
                    }
                    else
                    {
                        m_iState = ChampionState.Other;
                    }
                }
            }

            public bool Valid
            {
                get
                {
                    if (dictChampionStatus.ContainsKey(m_iState))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            public int CompareTo(ChampionState other)
            {
                return m_iState.CompareTo(other.m_iState);
            }

            public int CompareTo(int other)
            {
                return m_iState.CompareTo(other);
            }

            public override string ToString()
            {
                if (m_iState == Other)
                {
                    return "";
                }
                else if (dictChampionStatus.ContainsKey(m_iState))
                {
                    return dictChampionStatus[m_iState];
                }
                else
                {
                    return string.Format("ERROR: Undefined Champion State {0}", m_iState);
                }
            }

            static public string[] Values
            {
                get
                {
                    string[] temp = new string[dictChampionStatus.Count];
                    dictChampionStatus.Values.CopyTo(temp, 0);
                    return temp;
                }
            }

            public static bool operator ==(ChampionState a, ChampionState b)
            {
                if (System.Object.ReferenceEquals(a, b))
                {
                    return true;
                }

                if ((object)a == null || (object)b == null)
                {
                    return false;
                }

                return a.m_iState == b.m_iState;
            }

            public static bool operator !=(ChampionState a, ChampionState b)
            {
                return !(a == b);
            }

            public static bool operator ==(ChampionState a, int b)
            {
                if ((object)a == null)
                {
                    return false;
                }

                return a.m_iState == b;
            }

            public static bool operator !=(ChampionState a, int b)
            {
                return !(a == b);
            }

            static public implicit operator string(ChampionState State)
            {
                return State.ToString();
            }
            static public implicit operator ChampionState(string State)
            {
                return new ChampionState(State);
            }

            static public implicit operator int(ChampionState State)
            {
                return State.m_iState;
            }
            static public implicit operator ChampionState(int State)
            {
                return new ChampionState(State);
            }

            #region ISerializable Members
            protected ChampionState(SerializationInfo info, StreamingContext context)
            {
                if (info == null)
                {
                    throw new System.ArgumentNullException("info");
                }
                m_iState = (int)info.GetValue("m_State", typeof(int));
            }
            
            public void GetObjectData(SerializationInfo info, StreamingContext context)
            {
                if (info == null)
                {
                    throw new System.ArgumentNullException("info");
                }
                info.AddValue("m_State", m_iState);
            }

            #endregion
        }

    }

    static public class clsDB
    {
        public const string DB_CONNECTION_TEMPLATE = "Persist Security Info=True;Data Source={0};Initial Catalog={1};{2};Pooling=false";
        public const string DB_CONNECTION_UNTRUSTED_TEMPLATE = "User ID={0};Password={1};Trusted_Connection=False";
        public const string DB_CONNECTION_TRUSTED_TEMPLATE = "Trusted_Connection=True";



        static Thread m_DBMonitor;

        static System.Data.IDbConnection m_dbConn;
        static System.Data.IDbConnection m_dbBgConn;

        static DB.clsExhibitors m_dbExhibitors;
        static DB.clsMarketItems m_dbMarket;
        static DB.clsExhibits m_dbExhibits;
        static DB.clsBuyers m_dbBuyers;
        static DB.clsAuctionOrder m_dbAuctionOrder;
        static DB.clsPurchases m_dbPurchases;
        static DB.clsPayments m_dbPayments;

        static DB.clsSettings m_settings = new DB.clsSettings();

        static bool m_bConnected = false;

        static ManualResetEvent m_mreThreadWait;
        static Mutex m_PauseMonitorThread;

        static private void DBMonitorThread()
        {
            clsLogger.WriteLog("Starting background thread");
            
            //Create a database connection for the background thread so foreground and background readers do not conflict with each other
            if (m_dbConn is SqlConnection)
            {
                m_dbBgConn = new SqlConnection(m_dbConn.ConnectionString);
            }
            else if (m_dbConn is System.Data.SqlServerCe.SqlCeConnection)
            {
                m_dbBgConn = new System.Data.SqlServerCe.SqlCeConnection(m_dbConn.ConnectionString);
            }
            

            //Connect to the database and make sure the background connection is attached to the same database as the foreground
            m_dbBgConn.Open();
            if (m_dbBgConn.Database != m_dbConn.Database)
            {
                System.Data.IDbCommand dbUse = m_dbBgConn.CreateCommand();
                dbUse.CommandText = string.Format("USE [{0}]", m_dbConn.Database);
                dbUse.ExecuteNonQuery();
            }

            while (m_bConnected)
            {
                m_mreThreadWait.WaitOne(5000);
                m_mreThreadWait.Reset();

                m_PauseMonitorThread.WaitOne();

                try
                {
                    if (m_dbBgConn.State != System.Data.ConnectionState.Open)
                    {
                        m_dbBgConn.Open();
                    }
                }
                catch (Exception ex)
                {
                    clsLogger.WriteLog(string.Format("An error occured while connecting to the database:\r\nConnection String:{0}\r\nMessage:{1}\r\nStack Trace:\r\n{2}", m_dbBgConn.ConnectionString, ex.Message, ex.StackTrace));
                }

                try
                {
                    m_dbExhibitors.Update(m_dbBgConn);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(DateTime.Now.ToString() + " An error occured updating exhibitors: " + ex.Message);
                    clsLogger.WriteLog(string.Format("An error occured updating exhibitors:\r\nMessage:{0}\r\nStack Trace:\r\n{1}", ex.Message, ex.StackTrace));
                }
    
                try
                {
                    m_dbMarket.Update(m_dbBgConn);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(DateTime.Now.ToString() + " An error occured updating market: " + ex.Message);
                    clsLogger.WriteLog(string.Format("An error occured updating market:\r\nMessage:{0}\r\nStack Trace:\r\n{1}", ex.Message, ex.StackTrace));
                }
    
                try
                {
                    m_dbExhibits.Update(m_dbBgConn);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(DateTime.Now.ToString() + " An error occured updating exhibits: " + ex.Message);
                    clsLogger.WriteLog(string.Format("An error occured updating exhibits:\r\nMessage:{0}\r\nStack Trace:\r\n{1}", ex.Message, ex.StackTrace));
                }
                
                try
                {
                    m_dbBuyers.Update(m_dbBgConn);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(DateTime.Now.ToString() + " An error occured updating buyers: " + ex.Message);
                    clsLogger.WriteLog(string.Format("An error occured updating buyers:\r\nMessage:{0}\r\nStack Trace:\r\n{1}", ex.Message, ex.StackTrace));
                }

                try
                {
                    m_dbPurchases.Update(m_dbBgConn);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(DateTime.Now.ToString() + " An error occured updating purchases: " + ex.Message);
                    clsLogger.WriteLog(string.Format("An error occured updating purchases:\r\nMessage:{0}\r\nStack Trace:\r\n{1}", ex.Message, ex.StackTrace));
                }

                try
                {
                    m_dbPayments.Update(m_dbBgConn);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(DateTime.Now.ToString() + " An error occured updating payments: " + ex.Message);
                    clsLogger.WriteLog(string.Format("An error occured updating payments:\r\nMessage:{0}\r\nStack Trace:\r\n{1}", ex.Message, ex.StackTrace));
                }

                try
                {
                    m_dbAuctionOrder.Update(m_dbBgConn);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(DateTime.Now.ToString() + " An error occured updating auction order: " + ex.Message);
                    clsLogger.WriteLog(string.Format("An error occured updating auction order:\r\nMessage:{0}\r\nStack Trace:\r\n{1}", ex.Message, ex.StackTrace));
                }

                m_PauseMonitorThread.ReleaseMutex();
                System.Diagnostics.Debug.WriteLine(DateTime.Now.ToString() + " Update Complete");
            }
            m_dbBgConn.Close();
            System.Diagnostics.Debug.WriteLine(DateTime.Now.ToString() + " Database monitor thread terminated");
            clsLogger.WriteLog("Database monitor thread terminated");
        }

        static public bool CreateDatabase(System.Data.IDbConnection DatabaseConnection, string DatabaseName)
        {
            if (DatabaseConnection is SqlConnection)
            {
                clsLogger.WriteLog(string.Format("Creating database {0}", DatabaseName));

                //Create the database
                //TODO: Screen the database name for invalid characters
                //TODO: Prompt for additional parameters (such as folder)

                try
                {
                    System.Data.IDbCommand dbCreation = DatabaseConnection.CreateCommand();
                    dbCreation.CommandText = string.Format("CREATE DATABASE {0}", DatabaseName);
                    dbCreation.ExecuteNonQuery();

                    dbCreation.CommandText = string.Format("USE {0}", DatabaseName);
                    dbCreation.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    //TODO: Log Exception
                    return false;
                }
                catch (Exception ex)
                {
                    //TODO: Log Exception
                    return false;
                }
            }

            //Create the tables
            m_settings.CreateSettingsTable(DatabaseConnection);

            DB.Setup.AuctionDBSetup.BuildTable(DB.clsExhibitors.Setup, DatabaseConnection);
            DB.Setup.AuctionDBSetup.BuildTable(DB.clsMarketItems.Setup, DatabaseConnection);
            DB.Setup.AuctionDBSetup.BuildTable(DB.clsExhibits.Setup, DatabaseConnection);
            DB.Setup.AuctionDBSetup.BuildTable(DB.clsBuyers.Setup, DatabaseConnection);
            DB.Setup.AuctionDBSetup.BuildTable(DB.clsPurchases.Setup, DatabaseConnection);
            DB.Setup.AuctionDBSetup.BuildTable(DB.clsPayments.Setup, DatabaseConnection);
            DB.Setup.AuctionDBSetup.BuildTable(DB.clsAuctionOrder.Setup, DatabaseConnection);

            return true;
        }

        static public void Connect(System.Data.IDbConnection DatabaseConnection)
        {
            try
            {
                clsLogger.WriteLog("Connecting to database");
                m_dbConn = DatabaseConnection;
                Initalize();
                clsLogger.WriteLog(string.Format("Connected to database\r\n\tExhibitors: {0}\r\n\tExhibits: {1}\r\n\tBuyers: {2}\r\n\tGross Sold: {3:C2}", m_dbExhibitors.Count, m_dbExhibits.Count, m_dbBuyers.Count, m_dbPurchases.GrossSold));
            }
            catch (Exception ex)
            {
                string sMessage = "An error occured connecting to the database\r\n\r\n" + ex.Message;
                if (ex.InnerException != null)
                {
                    sMessage += "\r\n" + ex.InnerException.Message;
                }
                System.Windows.Forms.MessageBox.Show(sMessage, "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

                clsLogger.WriteLog(string.Format("An error occured connecting to the database:\r\nMessage:{0}\r\nStack Trace:\r\n{1}", ex.Message, ex.StackTrace));
                if (ex.InnerException != null)
                {
                    clsLogger.WriteLog(string.Format("   Inner Exception:\r\nMessage:{0}\r\nStack Trace:\r\n{1}", ex.InnerException.Message, ex.InnerException.StackTrace));
                }
            }
        }

        static private void Initalize()
        {
            //Initialize Settings object (Copies the form of the other loads)
            try { m_settings.LoadSettingsFromDB(m_dbConn); } catch (SqlException ex) { try { m_settings.SettingsLoadHandler(m_dbConn, ex); } catch (Exception ex2) { m_settings.SettingsBuildHandler(ex2); } try { m_settings.LoadSettingsFromDB(m_dbConn); } catch (Exception ex2) { m_settings.SettingsReloadHandler(ex2); } }    catch (Exception ex) { throw new Exception("Failed to load Market Items", ex); }
            
            //The general logic here is, try to initalize the auction data collection object, if there is a sql error, then try to handle it, and try again
            try { m_dbMarket = new DB.clsMarketItems(m_dbConn); }           catch (SqlException ex) { try { SqlLoadHandler(DB.clsMarketItems.Setup, ex); }  catch (Exception ex2) { SqlBuildHandler(DB.clsMarketItems.Setup, ex2); }    try { m_dbMarket = new DB.clsMarketItems(m_dbConn); }           catch (Exception ex2) { SqlReloadHandler(DB.clsMarketItems.Setup, ex2); }   }   catch (Exception ex) { throw new Exception("Failed to load Market Items", ex); }
            try { m_dbExhibitors = new DB.clsExhibitors(m_dbConn); }        catch (SqlException ex) { try { SqlLoadHandler(DB.clsExhibitors.Setup, ex); }   catch (Exception ex2) { SqlBuildHandler(DB.clsExhibitors.Setup, ex2); }     try { m_dbExhibitors = new DB.clsExhibitors(m_dbConn); }        catch (Exception ex2) { SqlReloadHandler(DB.clsExhibitors.Setup, ex2); }    }   catch (Exception ex) { throw new Exception("Failed to load Exhibitors", ex); }
            try { m_dbExhibits = new DB.clsExhibits(m_dbConn); }            catch (SqlException ex) { try { SqlLoadHandler(DB.clsExhibits.Setup, ex); }     catch (Exception ex2) { SqlBuildHandler(DB.clsExhibits.Setup, ex2); }       try { m_dbExhibits = new DB.clsExhibits(m_dbConn); }            catch (Exception ex2) { SqlReloadHandler(DB.clsExhibits.Setup, ex2); }      }   catch (Exception ex) { throw new Exception("Failed to load Exhibits", ex); }
            try { m_dbPurchases = new DB.clsPurchases(m_dbConn); }          catch (SqlException ex) { try { SqlLoadHandler(DB.clsPurchases.Setup, ex); }    catch (Exception ex2) { SqlBuildHandler(DB.clsPurchases.Setup, ex2); }      try { m_dbPurchases = new DB.clsPurchases(m_dbConn); }          catch (Exception ex2) { SqlReloadHandler(DB.clsPurchases.Setup, ex2); }     }   catch (Exception ex) { throw new Exception("Failed to load Purchases", ex); }
            try { m_dbPayments = new DB.clsPayments(m_dbConn); }            catch (SqlException ex) { try { SqlLoadHandler(DB.clsPayments.Setup, ex); }     catch (Exception ex2) { SqlBuildHandler(DB.clsPayments.Setup, ex2); }       try { m_dbPayments = new DB.clsPayments(m_dbConn); }            catch (Exception ex2) { SqlReloadHandler(DB.clsPayments.Setup, ex2); }      }   catch (Exception ex) { throw new Exception("Failed to load Payments", ex); }
            try { m_dbBuyers = new DB.clsBuyers(m_dbConn); }                catch (SqlException ex) { try { SqlLoadHandler(DB.clsBuyers.Setup, ex); }       catch (Exception ex2) { SqlBuildHandler(DB.clsBuyers.Setup, ex2); }         try { m_dbBuyers = new DB.clsBuyers(m_dbConn); }                catch (Exception ex2) { SqlReloadHandler(DB.clsBuyers.Setup, ex2); }        }   catch (Exception ex) { throw new Exception("Failed to load Buyers", ex); }
            try { m_dbAuctionOrder = new DB.clsAuctionOrder(m_dbConn); }    catch (SqlException ex) { try { SqlLoadHandler(DB.clsAuctionOrder.Setup, ex); } catch (Exception ex2) { SqlBuildHandler(DB.clsAuctionOrder.Setup, ex2); }   try { m_dbAuctionOrder = new DB.clsAuctionOrder(m_dbConn); }    catch (Exception ex2) { SqlReloadHandler(DB.clsAuctionOrder.Setup, ex2); }  }   catch (Exception ex) { throw new Exception("Failed to load Auction Order", ex); }

            //Some of the database classes require being mapped into other
            //  classes update events to keep the list items up-to-date.
            //  Doing this as part of the initialization was starting to create
            //  conflicting dependencies to the order in which the classes are
            //  loaded.
            m_dbMarket.ConnectEvents();
            m_dbExhibitors.ConnectEvents();
            m_dbExhibits.ConnectEvents();
            m_dbPurchases.ConnectEvents();
            m_dbPayments.ConnectEvents();
            m_dbBuyers.ConnectEvents();
            m_dbAuctionOrder.ConnectEvents();
            
            m_bConnected = true;
            if (m_DBMonitor == null)
            {
                m_DBMonitor = new Thread(DBMonitorThread);
                m_DBMonitor.Name = "DBMonitorThread";
                m_mreThreadWait = new ManualResetEvent(true);
                m_PauseMonitorThread = new Mutex();
                m_DBMonitor.Start();
            }
        }

        static private void SqlLoadHandler(DB.Setup.AuctionDBSetup TableSetup, SqlException ex)
        {
            if (ex.Number == 208)
            {
                //Catch object not found
                //TODO: Catch missing columns
                if (System.Windows.Forms.MessageBox.Show(string.Format("{0} table not found in database, do you wish to create it?", TableSetup.TABLE_NAME), "Table not found", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
                {
                    try
                    {
                        DB.Setup.AuctionDBSetup.BuildTable(TableSetup, m_dbConn);
                    }
                    catch (SqlException ex2)
                    {
                        throw ex2;
                    }
                }
            }
            else
            {
                throw ex;
            }
        }

        static private void SqlBuildHandler(DB.Setup.AuctionDBSetup TableSetup, Exception ex)
        {
            System.Windows.Forms.MessageBox.Show(string.Format("Failed to create table {0}.", TableSetup.TABLE_NAME), "Failed to create table", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
        }

        static private void SqlReloadHandler(DB.Setup.AuctionDBSetup TableSetup, Exception ex)
        {
            System.Windows.Forms.MessageBox.Show(string.Format("Failed to load {0}.", TableSetup.TABLE_NAME), "Failed to load", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
        }

        static public void Disconnect()
        {
            m_bConnected = false;

            m_mreThreadWait.Set();
        }

        static public void ExportDatabase(string sFileName)
        {
            string sDbConnString = string.Format("Data Source={0};File Mode=Exclusive;Persist Security Info=False;", sFileName);

            //The user was already prompted about overwriting the file...
            if (File.Exists(sFileName))
            {
                File.Delete(sFileName);
            }

            //Create and build the database
            System.Data.SqlServerCe.SqlCeEngine engine = new System.Data.SqlServerCe.SqlCeEngine(sDbConnString);
            engine.CreateDatabase();
            
            System.Data.SqlServerCe.SqlCeConnection dbExportConnection = new System.Data.SqlServerCe.SqlCeConnection(sDbConnString);
            dbExportConnection.Open();
            
            //Enumerate over the Setup classes to build the database
            Assembly asm = Assembly.GetExecutingAssembly();
            foreach (Type t in asm.GetTypes())
            {
                if (t.Namespace == "Livestock_Auction.DB.Setup")
                {
                    if (t.IsSubclassOf(typeof(Livestock_Auction.DB.Setup.AuctionDBSetup)))
                    {
                        DB.Setup.AuctionDBSetup SetupClass = (DB.Setup.AuctionDBSetup)Activator.CreateInstance(t);
                        DB.Setup.AuctionDBSetup.BuildTable(SetupClass, dbExportConnection);

                        DB.Setup.AuctionDBSetup.CopyData(SetupClass, m_dbConn, dbExportConnection);
                    }
                }
            }

            //Write Settings table
            m_settings.CreateTableForExport(dbExportConnection);

            dbExportConnection.Close();
        }

        static public void ImportDatabase(System.Data.IDbConnection SourceConnection, bool bExhibits, bool bAuctionOrder, bool bBuyers, bool bPurchases)
        {
            m_PauseMonitorThread.WaitOne();
            if (bExhibits)
            {
                //Import market items, exhibits, and exhibitors
                DB.Setup.AuctionDBSetup.CopyData(new DB.Setup.MarketItem(), SourceConnection, m_dbConn);
                DB.Setup.AuctionDBSetup.CopyData(new DB.Setup.Exhibitors(), SourceConnection, m_dbConn);
                DB.Setup.AuctionDBSetup.CopyData(new DB.Setup.Exhibits(), SourceConnection, m_dbConn);
            }

            if (bAuctionOrder)
            {
                //Import the action order, requires bExhibits
                DB.Setup.AuctionDBSetup.CopyData(new DB.Setup.AuctionOrder(), SourceConnection, m_dbConn);
            }

            if (bBuyers)
            {
                //Import Buyers
                DB.Setup.AuctionDBSetup.CopyData(new DB.Setup.Buyers(), SourceConnection, m_dbConn);
                (new DB.Setup.Buyers()).SQLCopyHistory(SourceConnection, m_dbConn);
            }

            if (bPurchases)
            {
                //Import Purchases and payments, requires buyers and exhibits
                DB.Setup.AuctionDBSetup.CopyData(new DB.Setup.Purchases(), SourceConnection, m_dbConn);
                DB.Setup.AuctionDBSetup.CopyData(new DB.Setup.Payments(), SourceConnection, m_dbConn);
            }

            //Import Settings Table
            m_settings.ImportTableFromSQLCE(SourceConnection);

            m_PauseMonitorThread.ReleaseMutex();
        }

        static public void ImportHistory(System.Data.IDbConnection SourceConnection, bool bBuyerHistory, bool bExhibits)
        {
            m_PauseMonitorThread.WaitOne();

            if (bBuyerHistory)
            {
                (new DB.Setup.Buyers()).SQLImportHistory(SourceConnection, m_dbConn);
            }
            if (bExhibits)
            {
                DB.Setup.AuctionDBSetup.CopyData(new DB.Setup.Exhibitors(), SourceConnection, m_dbConn);
            }

            m_PauseMonitorThread.ReleaseMutex();
        }

        static public void ExportToExcel(string sFileName)
        {
            OfficeOpenXml.ExcelPackage outfile = new OfficeOpenXml.ExcelPackage();

            clsDB.Buyers.ExportToWorkbook(outfile);
            clsDB.Purchases.ExportToWorkbook(outfile);
            clsDB.Exhibits.ExportFairToWorkbook(outfile);
            clsDB.Exhibits.ExportNewHollandToWorkbook(outfile);

            FileStream outstream = new FileStream(sFileName, FileMode.Create);
            outfile.SaveAs(outstream);
            outstream.Close();
           
            System.Windows.Forms.MessageBox.Show("Export Complete", "Export");
        }

        static public void ExportTurnBackListToExcel(string sFileName)
        {
            OfficeOpenXml.ExcelPackage outfile = new OfficeOpenXml.ExcelPackage();

            foreach (DB.clsMarketItem mItem in clsDB.Market)
            {
                if ((mItem.MarketID > 0) && (mItem.MarketValue > 0))
                {
                    outfile.Workbook.Worksheets.Add(mItem.MarketType);
                    clsDB.Purchases.ExportTurnBackList(outfile, mItem.MarketType);
                }
            }


            FileStream outstream = new FileStream(sFileName, FileMode.Create);
            outfile.SaveAs(outstream);
            outstream.Close();

            System.Windows.Forms.MessageBox.Show("Export Complete", "Export");

        }


        //Helper function to query for a list of all last names from the buyer history.
        static public string[] BuyerHistory_LastNames()
        {
            return BuyerHistory_FindNames("SELECT NameLast AS Name FROM BuyerHistory WHERE NameLast IS NOT NULL AND NameLast != '' GROUP BY NameLast");
        }

        //Helper function to query for a list of last names associated with the specified first name from the buyer history.
        static public string[] BuyerHistory_LastNames(string FirstName)
        {
            //Some simple string escaping
            FirstName = FirstName.Replace("'", "''");
            FirstName = FirstName.Replace("%", "");

            string[] sNames = BuyerHistory_FindNames("SELECT NameLast AS Name FROM BuyerHistory WHERE NameFirst LIKE '%" + FirstName + "%' GROUP BY NameLast");
            return sNames;
        }

        //Helper function to query for a list of all first names from the buyer history.
        static public string[] BuyerHistory_FirstNames()
        {
            return BuyerHistory_FindNames("SELECT NameFirst AS Name FROM BuyerHistory WHERE NameLast IS NOT NULL AND NameLast != '' GROUP BY NameFirst");
        }

        //Helper function to query for first names associated with the specified last name from the buyer history.
        static public string[] BuyerHistory_FirstNames(string LastName)
        {
            //Some simple string escaping
            LastName = LastName.Replace("'", "''");
            LastName = LastName.Replace("%", "");

            string[] sNames = BuyerHistory_FindNames("SELECT NameFirst AS Name FROM BuyerHistory WHERE NameLast LIKE '%" + LastName + "%' GROUP BY NameFirst");
            return sNames;
        }

        //Helper function to make a list of names from from a query. Intended to be used to query names from the buyer history.
        static private string[] BuyerHistory_FindNames(string SQLCommand)
        {
            List<string> lstLastNames = new List<string>();
            System.Data.IDbCommand dbLoad = m_dbConn.CreateCommand();
            dbLoad.Connection = m_dbConn;
            dbLoad.CommandText = SQLCommand;

            System.Data.IDataReader dbNameReader = dbLoad.ExecuteReader();
            while (dbNameReader.Read())
            {
                lstLastNames.Add(dbNameReader["Name"].ToString());
            }
            dbNameReader.Close();
            return lstLastNames.ToArray();
        }

        //Helper function to create a new buyer based on the first and last name from the buyer history
        static public DB.clsBuyer FindBuyer(int BuyerNumber, string FirstName, string LastName)
        {
            DB.clsBuyer Buyer = null;
            System.Data.IDbCommand dbLoad = m_dbConn.CreateCommand();
            dbLoad.CommandText = "SELECT NameFirst, NameLast, CompanyName, Address, City, State, Zip, PhoneNumber FROM BuyerHistory WHERE NameFirst=@FirstName and NameLast=@LastName";

            System.Data.IDbDataParameter dbParam = dbLoad.CreateParameter();
            dbParam.ParameterName = "@FirstName";
            dbParam.Value = FirstName;
            dbLoad.Parameters.Add(dbParam);

            dbParam = dbLoad.CreateParameter();
            dbParam.ParameterName = "@LastName";
            dbParam.Value = LastName;
            dbLoad.Parameters.Add(dbParam);

            System.Data.IDataReader dbBuyerReader = dbLoad.ExecuteReader();
            if (dbBuyerReader.Read())
            {
                Buyer = new DB.clsBuyer(BuyerNumber, FirstName, LastName, dbBuyerReader["CompanyName"].ToString(), dbBuyerReader["Address"].ToString(), dbBuyerReader["City"].ToString(), dbBuyerReader["State"].ToString(), (int)dbBuyerReader["Zip"], dbBuyerReader["PhoneNumber"].ToString());
            }
            dbBuyerReader.Close();
            return Buyer;
        }

        static public System.Data.IDbConnection Connection
        {
            get
            {
                return m_dbConn;
            }
        }

        static public bool Connected
        {
            get
            {
                return m_bConnected;
            }
        }

        static public DB.clsExhibitors Exhibitors
        {
            get
            {
                return m_dbExhibitors;
            }
        }

        static public DB.clsBuyers Buyers
        {
            get
            {
                return m_dbBuyers;
            }
        }

        static public DB.clsMarketItems Market
        {
            get
            {
                return m_dbMarket;
            }
        }

        static public DB.clsExhibits Exhibits
        {
            get
            {
                return m_dbExhibits;
            }
        }

        static public DB.clsAuctionOrder Auction
        {
            get
            {
                return m_dbAuctionOrder;
            }
        }

        static public DB.clsPurchases Purchases
        {
            get
            {
                return m_dbPurchases;
            }
        }

        static public DB.clsPayments Payments
        {
            get
            {
                return m_dbPayments;
            }
        }

        static public DB.clsSettings Settings
        {
            get
            {
                return m_settings;
            }
        }
    }

    public static class GeoDatabase
    {
        //Structure defines the record format of the CityStateZip binary
        //  resources. Note, something potentially for future investigation,
        //  doing an "Any CPU" build seems to cause a problem with the packing
        //  in this structure that results in a compiler error.
        [StructLayout(LayoutKind.Explicit, Size = 36, Pack = 4)]
        private class CityStateZipRecord
        {
            [FieldOffset(0)]
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
            public string StateAbbr = "";
            [FieldOffset(4)]
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 28)]
            public string City = "";
            [FieldOffset(32)]
            [MarshalAs(UnmanagedType.U4)]
            public int ZipCode = 0;
        }

        //Helper function to parse and search the geo data resource
        static private CityStateZipRecord SearchStateCityZipData(BinaryReader CityStateZipData, string State, string City, int Zip)
        {
            //Preform a binary search against the provided state/city/zip data
            //  resource, using the provided critieria (either State,
            //  State/City, or Zip code). Caller must provide the correctly
            //  sorted resource.
            string sPrimarySearch = "";
            string sSecondarySearch = "";
            string sPrimaryResult = "";
            string sSecondaryResult = "";

            if (State != "")
            {
                sPrimarySearch = State;
                if (City != "")
                {
                    sSecondarySearch = City;
                }
            }
            else if (Zip != 0)
            {
                sPrimarySearch = Zip.ToString("00000");
            }

            int iTotalRecords = CityStateZipData.ReadInt32();
            int iSeekRecords = (int)Math.Ceiling(iTotalRecords / 2.0);
            CityStateZipRecord CurrentRecord = new CityStateZipRecord();
            CityStateZipData.BaseStream.Seek((iSeekRecords) * Marshal.SizeOf(CurrentRecord), SeekOrigin.Current);

            //Preform a binary search for the state
            while ((sPrimarySearch != sPrimaryResult || (sSecondarySearch != "" && sSecondarySearch != sSecondaryResult)) && iSeekRecords > 0)
            {
                //Read the record
                byte[] bytes = CityStateZipData.ReadBytes(Marshal.SizeOf(CurrentRecord));
                GCHandle handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
                CurrentRecord = (CityStateZipRecord)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(CityStateZipRecord));

                //Pickout the fields for comparison based on the provided search terms
                if (State != "")
                {
                    sPrimaryResult = CurrentRecord.StateAbbr;
                    if (City != "")
                    {
                        sSecondaryResult = CurrentRecord.City;
                    }
                }
                else if (Zip != 0)
                {
                    sPrimaryResult = CurrentRecord.ZipCode.ToString("00000");
                }

                if (iSeekRecords > 1)
                {
                    iSeekRecords = (int)Math.Ceiling(iSeekRecords / 2.0);
                }
                else
                {
                    iSeekRecords = 0;
                }

                if (sPrimaryResult.CompareTo(sPrimarySearch) < 0 || (sSecondarySearch != "" && (sPrimaryResult.CompareTo(sPrimarySearch) == 0 && sSecondaryResult.CompareTo(sSecondarySearch) < 0)))
                {
                    CityStateZipData.BaseStream.Seek(1 * (iSeekRecords - 1) * Marshal.SizeOf(CurrentRecord), SeekOrigin.Current);
                }
                else if (sPrimaryResult.CompareTo(sPrimarySearch) > 0 || (sSecondarySearch != "" && (sPrimaryResult.CompareTo(sPrimarySearch) == 0 && sSecondaryResult.CompareTo(sSecondarySearch) > 0)))
                {
                    if (CityStateZipData.BaseStream.Position > ((iSeekRecords + 1) * Marshal.SizeOf(CurrentRecord) + 4))
                    {
                        CityStateZipData.BaseStream.Seek(-1 * (iSeekRecords + 1) * Marshal.SizeOf(CurrentRecord), SeekOrigin.Current);
                    }
                    else
                    {
                        CityStateZipData.BaseStream.Seek(4, SeekOrigin.Begin);
                    }
                }
            }

            return CurrentRecord;
        }

        //Helper function to query the geo data for a list of cities based on state
        static public string[] FindCityByState(string State)
        {
            List<string> lsCities = new List<string>();

            //Open the City/State/Zip data resource that is sorted by State/City
            BinaryReader brCityStateZipData = new BinaryReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("Livestock_Auction.DB.CityStateZipDataByStateCity.bin"));

            CityStateZipRecord CurrentRecord = SearchStateCityZipData(brCityStateZipData, State, "", 0);

            if (CurrentRecord.StateAbbr == State)
            {
                byte[] bytes;
                GCHandle handle;

                //At least one instance of the state was found
                //  Find the first city in the state
                while (CurrentRecord.StateAbbr == State && brCityStateZipData.BaseStream.Position > 4)
                {
                    //Don't seek past the beginning of the file...
                    if (brCityStateZipData.BaseStream.Position > 2 * Marshal.SizeOf(CurrentRecord))
                    {
                        brCityStateZipData.BaseStream.Seek(-2 * Marshal.SizeOf(CurrentRecord), SeekOrigin.Current);
                        bytes = brCityStateZipData.ReadBytes(Marshal.SizeOf(CurrentRecord));
                        handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
                        CurrentRecord = (CityStateZipRecord)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(CityStateZipRecord));
                    }
                    else
                    {
                        brCityStateZipData.BaseStream.Seek(4, SeekOrigin.Begin);
                    }
                }

                //Now read all of the cities for the state
                bytes = brCityStateZipData.ReadBytes(Marshal.SizeOf(CurrentRecord));
                handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
                CurrentRecord = (CityStateZipRecord)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(CityStateZipRecord));
                while (CurrentRecord.StateAbbr == State)
                {
                    if (lsCities.Count <= 0 || lsCities[lsCities.Count - 1] != CurrentRecord.City)
                    {
                        lsCities.Add(CurrentRecord.City);
                    }

                    bytes = brCityStateZipData.ReadBytes(Marshal.SizeOf(CurrentRecord));
                    handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
                    CurrentRecord = (CityStateZipRecord)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(CityStateZipRecord));
                }
            }

            return lsCities.ToArray();
        }

        //Helper function to query the geo data for a list of cities based on state
        static public string[] FindZipByStateCity(string State, string City)
        {
            List<string> lsZipCodes = new List<string>();

            //Open the City/State/Zip data resource that is sorted by State/City
            BinaryReader brCityStateZipData = new BinaryReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("Livestock_Auction.DB.CityStateZipDataByStateCity.bin"));

            CityStateZipRecord CurrentRecord = SearchStateCityZipData(brCityStateZipData, State, City, 0);

            if (CurrentRecord.StateAbbr == State && CurrentRecord.City == City)
            {
                byte[] bytes;
                GCHandle handle;

                //At least one instance of the state/city was found
                //  Find the first zip code in the city
                while (CurrentRecord.StateAbbr == State && CurrentRecord.City == City && brCityStateZipData.BaseStream.Position > 4)
                {
                    if (brCityStateZipData.BaseStream.Position > 2 * Marshal.SizeOf(CurrentRecord))
                    {
                        brCityStateZipData.BaseStream.Seek(-2 * Marshal.SizeOf(CurrentRecord), SeekOrigin.Current);
                        bytes = brCityStateZipData.ReadBytes(Marshal.SizeOf(CurrentRecord));
                        handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
                        CurrentRecord = (CityStateZipRecord)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(CityStateZipRecord));
                    }
                    else
                    {
                        brCityStateZipData.BaseStream.Seek(4, SeekOrigin.Begin);
                    }
                }

                //Now read all of the zip codes for the city
                bytes = brCityStateZipData.ReadBytes(Marshal.SizeOf(CurrentRecord));
                handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
                CurrentRecord = (CityStateZipRecord)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(CityStateZipRecord));
                while (CurrentRecord.StateAbbr == State && CurrentRecord.City == City)
                {
                    lsZipCodes.Add(CurrentRecord.ZipCode.ToString("00000"));
                    bytes = brCityStateZipData.ReadBytes(Marshal.SizeOf(CurrentRecord));
                    handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
                    CurrentRecord = (CityStateZipRecord)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(CityStateZipRecord));
                }
            }

            return lsZipCodes.ToArray();
        }

        //Helper function to query the geo data for a state based on a zip code
        static public string FindStateByZip(int ZipCode)
        {
            //Open the City/State/Zip data resource that is sorted by Zip Code
            BinaryReader brCityStateZipData = new BinaryReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("Livestock_Auction.DB.CityStateZipDataByZip.bin"));
            CityStateZipRecord CurrentRecord = SearchStateCityZipData(brCityStateZipData, "", "", ZipCode);
            if (CurrentRecord.ZipCode == ZipCode)
            {
                return CurrentRecord.StateAbbr;
            }
            else
            {
                return "";
            }
        }

        //Helper function to query the geo data for a city based on a zip code
        static public string FindCityByZip(int ZipCode)
        {
            //Open the City/State/Zip data resource that is sorted by Zip Code
            BinaryReader brCityStateZipData = new BinaryReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("Livestock_Auction.DB.CityStateZipDataByZip.bin"));
            CityStateZipRecord CurrentRecord = SearchStateCityZipData(brCityStateZipData, "", "", ZipCode);
            if (CurrentRecord.ZipCode == ZipCode)
            {
                return CurrentRecord.City;
            }
            else
            {
                return "";
            }
        }
    }
}
