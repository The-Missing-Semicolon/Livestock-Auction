using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace Livestock_Auction.DB
{
    public class clsSettings
    {
        string s_eventName;
        double s_fairPercent;
        double s_ccPercent;

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
                        ('eventName', 'Cecil County Fair Livestock Auction'), 
                        ('fairFee', '0.05'), 
                        ('ccFee', '0.0275')";
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
                                s_eventName = reader["sValue"].ToString();
                                break;
                            case "fairFee":
                                //s_fairPercent = (double)(reader["sValue"].ToString());
                                s_fairPercent = Convert.ToDouble(reader["sValue"]);
                                break;
                            case "ccFee":
                                s_ccPercent = Convert.ToDouble(reader["sValue"]);
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

        public string EventName
        {
            get
            {
                return s_eventName;
            }
            set
            {
                s_eventName = value;
            }
        }

        public double FairFee
        {
            get
            {
                return s_fairPercent;
            }
            set
            {
                s_fairPercent = value;
            }
        }

        public double CCFee
        {
            get
            {
                return s_ccPercent;
            }
            set
            {
                s_ccPercent = value;
            }
        }



    }
}
