using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace Livestock_Auction.DB
{
    public class clsSettings
    {
        string m_eventName;
        string m_fairName;
        string m_fairAddress;
        string m_fairCity;
        string m_fairState;
        int m_fairZipcode;
        string m_fairPhone;

        int m_fairYear;
        double m_fairPercent;
        double m_ccPercent;

        public clsSettings()
        {
        }

        public bool CreateSettingsTable(System.Data.IDbConnection DatabaseConnection)
        {
            if (DatabaseConnection is SqlConnection)
            {
                clsLogger.WriteLog(string.Format("Creating settings database."));

                //Create the database
                //TODO: Screen the database name for invalid characters
                //TODO: Prompt for additional parameters (such as folder)

                try
                {
                    System.Data.IDbCommand tableCreation = DatabaseConnection.CreateCommand();
                    tableCreation.CommandText = @"CREATE TABLE [Settings]( 
                        [sKey] [nvarchar](max) NOT NULL,
                        [sValue][nvarchar](max) NOT NULL
                    )";
                    tableCreation.ExecuteNonQuery();

                    tableCreation.CommandText = @"insert into Settings values 
                        ('eventName', 'Livestock Auction'), 
                        ('fairName', 'Cecil County Fair'),
                        ('address', 'PO Box 84'),
                        ('city', 'Childs'),
                        ('state', 'MD'),
                        ('zipcode', '21916'),
                        ('phone', '410-392-3440'),
                        ('eventYear','" + DateTime.Now.Year.ToString() + @"'),
                        ('fairFee', '0.05'), 
                        ('ccFee', '0.0275')";
                    Console.WriteLine(tableCreation.CommandText);
                    tableCreation.ExecuteNonQuery();
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

            return true;
        }

        public bool LoadSettingsFromDB(System.Data.IDbConnection DatabaseConnection)
        {
            try
            {
                System.Data.IDbCommand readSettings = DatabaseConnection.CreateCommand();
                readSettings.CommandText = @"SELECT * FROM Settings";
                using (System.Data.IDataReader reader = readSettings.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        switch (reader["sKey"].ToString())
                        {
                            case "eventName":
                                m_eventName = reader["sValue"].ToString();
                                break;
                            case "fairName":
                                m_fairName = reader["sValue"].ToString();
                                break;
                            case "address":
                                m_fairAddress = reader["sValue"].ToString();
                                break;
                            case "city":
                                m_fairCity = reader["sValue"].ToString();
                                break;
                            case "state":
                                m_fairState = reader["sValue"].ToString();
                                break;
                            case "zipcode":
                                m_fairZipcode = Convert.ToInt32(reader["sValue"].ToString());
                                break;
                            case "phone":
                                m_fairPhone = reader["sValue"].ToString();
                                break;
                            case "eventYear":
                                m_fairYear = Convert.ToInt32(reader["sValue"].ToString());
                                break;

                            case "fairFee":
                                m_fairPercent = Convert.ToDouble(reader["sValue"]);
                                break;
                            case "ccFee":
                                m_ccPercent = Convert.ToDouble(reader["sValue"]);
                                break;

                        }

                    }
                }
            }
            catch (SqlException ex)
            {
                //TODO: Log Exception
                throw ex;
            }
            catch (Exception ex)
            {
                //TODO: Log Exception
                return false;
            }


            return true;
        }

        public void WriteSettingsToDB(System.Data.IDbConnection DatabaseConnection, string setting, string value)
        {
            System.Data.IDbCommand updateCmd = DatabaseConnection.CreateCommand();
            updateCmd.CommandText = "UPDATE Settings SET sValue = @val WHERE sKey = @key";

            var param = updateCmd.CreateParameter();
            param.ParameterName = "@val";
            param.Value = value;
            param.DbType = System.Data.DbType.String;
            param.Size = 255;
            updateCmd.Parameters.Add(param);

            param = updateCmd.CreateParameter();
            param.ParameterName = "@key";
            param.Value = setting;
            param.DbType = System.Data.DbType.String;
            param.Size = 255;
            updateCmd.Parameters.Add(param);

            //updateCmd.Prepare();
            updateCmd.ExecuteNonQuery();

        }

        public void SettingsLoadHandler(System.Data.IDbConnection DatabaseConnection, SqlException ex)
        {
            if (ex.Number == 208)
            {
                //Catch object not found
                //TODO: Catch missing columns
                if (System.Windows.Forms.MessageBox.Show("Settings table not found in database, do you wish to create it?", "Table not found", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
                {
                    try
                    {
                        CreateSettingsTable(DatabaseConnection);
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

        public void SettingsBuildHandler(Exception ex)
        {
            System.Windows.Forms.MessageBox.Show("Failed to create settings table.", "Failed to create table", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
        }

        public void SettingsReloadHandler(Exception ex)
        {
            System.Windows.Forms.MessageBox.Show("Failed to load settings.", "Failed to load", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
        }

        public bool CreateTableForExport(System.Data.IDbConnection DatabaseConnection)
        {
            try
            {
                System.Data.IDbCommand tableCreation = DatabaseConnection.CreateCommand();
                tableCreation.CommandText = @"CREATE TABLE [Settings]( 
                        [sKey] [nvarchar](256) NOT NULL,
                        [sValue][nvarchar](256) NOT NULL
                    )";
                tableCreation.ExecuteNonQuery();
                Console.WriteLine("Settings Table Created!");

                tableCreation.CommandText = @"insert into Settings values ('eventName', '" + m_eventName + @"')"; tableCreation.ExecuteNonQuery();

                tableCreation.CommandText = @"insert into Settings values ('fairName', '" + m_fairName + @"')"; tableCreation.ExecuteNonQuery();
                tableCreation.CommandText = @"insert into Settings values ('address', '" + m_fairAddress + @"')"; tableCreation.ExecuteNonQuery();
                tableCreation.CommandText = @"insert into Settings values ('city', '" + m_fairCity + @"')"; tableCreation.ExecuteNonQuery();
                tableCreation.CommandText = @"insert into Settings values ('state', '" + m_fairState + @"')"; tableCreation.ExecuteNonQuery();
                tableCreation.CommandText = @"insert into Settings values ('zipcode', '" + m_fairZipcode.ToString() + @"')"; tableCreation.ExecuteNonQuery();
                tableCreation.CommandText = @"insert into Settings values ('phone', '" + m_fairPhone + @"')"; tableCreation.ExecuteNonQuery();
                tableCreation.CommandText = @"insert into Settings values ('eventYear','" + m_fairYear.ToString() + @"')"; tableCreation.ExecuteNonQuery();
                tableCreation.CommandText = @"insert into Settings values ('fairFee', '" + m_fairPercent.ToString() + @"')"; tableCreation.ExecuteNonQuery();
                tableCreation.CommandText = @"insert into Settings values ('ccFee', '" + m_ccPercent.ToString() + @"')"; tableCreation.ExecuteNonQuery();
               
                return true;
            }
            catch (SqlException ex)
            {
                //TODO: Log Exception
                Console.WriteLine("SQLException: " + ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                //TODO: Log Exception
                Console.WriteLine("General Exception: " + ex.Message);
                return false;
            }
        }

        public bool ImportTableFromSQLCE(System.Data.IDbConnection DatabaseConnection)
        {
            LoadSettingsFromDB(DatabaseConnection);
            Console.WriteLine("Settings Loaded!");

            WriteSettingsToDB(clsDB.Connection, "eventName", m_eventName);
            WriteSettingsToDB(clsDB.Connection, "fairName", m_fairName);
            WriteSettingsToDB(clsDB.Connection, "address", m_fairAddress);
            WriteSettingsToDB(clsDB.Connection, "city", m_fairCity);
            WriteSettingsToDB(clsDB.Connection, "state", m_fairState);
            WriteSettingsToDB(clsDB.Connection, "zipcode", m_fairZipcode.ToString());
            WriteSettingsToDB(clsDB.Connection, "phone", m_fairPhone);

            WriteSettingsToDB(clsDB.Connection, "eventYear", m_fairYear.ToString());
            WriteSettingsToDB(clsDB.Connection, "fairFee", m_fairPercent.ToString());
            WriteSettingsToDB(clsDB.Connection, "ccFee", m_ccPercent.ToString());
            return true;
        }

        public string EventName { get => m_eventName; set => m_eventName = value; }

        public string FairName
        {
            get
            {
                return m_fairName;
            }
            set
            {
                m_fairName = value;
            }
        }

        public string FairAddress
        {
            get
            {
                return m_fairAddress;
            }
            set
            {
                m_fairAddress = value;
            }
        }

        public string FairCity
        {
            get
            {
                return m_fairCity;
            }
            set
            {
                m_fairCity = value;
            }
        }

        public string FairState
        {
            get
            {
                return m_fairState;
            }
            set
            {
                m_fairState = value;
            }
        }

        public int FairZipCode
        {
            get
            {
                return m_fairZipcode;
            }
            set
            {
                m_fairZipcode = value;
            }
        }

        public string FairPhone
        {
            get
            {
                return m_fairPhone;
            }
            set
            {
                m_fairPhone = value;
            }
        }

        public int FairYear
        {
            get
            {
                return m_fairYear;
            }
            set
            {
                m_fairYear = value;
            }
        }


        public double FairFee
        {
            get
            {
                return m_fairPercent;
            }
            set
            {
                m_fairPercent = value;
            }
        }

        public double CreditCardFee
        {
            get
            {
                return m_ccPercent;
            }
            set
            {
                m_ccPercent = value;
            }
        }



    }
}
