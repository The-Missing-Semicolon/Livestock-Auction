using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace Livestock_Auction
{
    public partial class frmConnect : Form
    {
        private class ConnectionHistoryItem
        {
            private string sServerName;
            private string sDatabaseName;
            private string sUserName;
            private string sPassword;
            private bool bTrustedAuthentication;

            public ConnectionHistoryItem(string HistoryRecord)
            {
                string[] HistoryItems = HistoryRecord.Split(':');

                sServerName = HistoryItems[0];
                sDatabaseName = HistoryItems[1];
                if (HistoryItems[2] == "T")
                {
                    bTrustedAuthentication = true;
                }
                else
                {
                    bTrustedAuthentication = false;
                    sUserName = HistoryItems[3];
                    sPassword = HistoryItems[4];
                }
            }

            public ConnectionHistoryItem(string Server, string Database)
            {
                sServerName = Server;
                sDatabaseName = Database;
                bTrustedAuthentication = true;
                sUserName = "";
            }

            public ConnectionHistoryItem(string Server, string Database, string User, string Password)
            {
                sServerName = Server;
                sDatabaseName = Database;
                bTrustedAuthentication = false;
                sUserName = User;
                sPassword = Password;
            }

            public string WriteHistoryRecord()
            {
                if (bTrustedAuthentication)
                {
                    return sServerName + ":" + sDatabaseName + ":T";
                }
                else
                {
                    return sServerName + ":" + sDatabaseName + ":U:" + sUserName + ":" + sPassword;
                }
            }

            public string ServerName
            {
                get
                {
                    return sServerName;
                }
            }
            public string DatabaseName
            {
                get
                {
                    return sDatabaseName;
                }
            }
            public bool TrustedAuthentication
            {
                get
                {
                    return bTrustedAuthentication;
                }
            }
            public string UserName
            {
                get
                {
                    return sUserName;
                }
            }
            public string Password
            {
                get
                {
                    return sPassword;
                }
            }

        }
        private List<ConnectionHistoryItem> ConnectionHistory = null;

        public frmConnect()
        {
            System.Threading.Thread.CurrentThread.Name = "Main";
            InitializeComponent();
        }

        private void frmConnect_Load(object sender, EventArgs e)
        {
            //Just so I don't have to remember to change the inital state if I choose to change the default option
            txtUserName.Enabled = !chkTrustedAuthentication.Checked;
            txtPassword.Enabled = !chkTrustedAuthentication.Checked;

            //Load the connection history
            LoadHistory();

            if (ConnectionHistory.Count > 0)
            {
                cmbDBServer.Text = ConnectionHistory[0].ServerName;
                cmbDBName.Text = ConnectionHistory[0].DatabaseName;
                chkTrustedAuthentication.Checked = ConnectionHistory[0].TrustedAuthentication;
                txtUserName.Text = ConnectionHistory[0].UserName;
                txtPassword.Text = ConnectionHistory[0].Password;

                foreach (ConnectionHistoryItem HistoryItem in ConnectionHistory)
                {
                    cmbDBServer.Items.Add(HistoryItem.ServerName);
                    cmbDBName.Items.Add(HistoryItem.DatabaseName);
                }

                cmdConnect.Focus();
            }
        }

        private void chkTrustedAuthentication_CheckedChanged(object sender, EventArgs e)
        {
            txtUserName.Enabled = !chkTrustedAuthentication.Checked;
            txtPassword.Enabled = !chkTrustedAuthentication.Checked;
        }

        private void cmdConnect_Click(object sender, EventArgs e)
        {
            System.Data.SqlClient.SqlConnection dbConn = null;
            string sConnStringTemplate = "";

            cmdConnect.Enabled = false;

            sConnStringTemplate = String.Format(clsDB.DB_CONNECTION_TEMPLATE, cmbDBServer.Text, "{0}", "{1}");
            if (chkTrustedAuthentication.Checked)
            {
                sConnStringTemplate = String.Format(sConnStringTemplate, "{0}", clsDB.DB_CONNECTION_TRUSTED_TEMPLATE);
            }
            else
            {
                sConnStringTemplate = String.Format(sConnStringTemplate, "{0}", String.Format(clsDB.DB_CONNECTION_UNTRUSTED_TEMPLATE, txtUserName.Text, txtPassword.Text));
            }
            dbConn = new SqlConnection(String.Format(sConnStringTemplate, cmbDBName.Text));


            try
            {
                dbConn.Open();
            }
            catch (SqlException ex)
            {
                //Was the problem that the specific database did not exist?
                //TODO: Error classes represent severity levels, use error numbers instead
                if (ex.Class == 11)
                {
                    //It looks like we could connect to the server, but the database was not found can we connect to the master database?
                    dbConn = new SqlConnection(String.Format(sConnStringTemplate, "master"));
                    try
                    {
                        dbConn.Open();
                    }
                    catch (SqlException ex2)
                    {
                        MessageBox.Show("[SqlException]Failed to connect to database.\r\n\r\n" + ex2.Message, "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception ex2)
                    {
                        MessageBox.Show("[Exception]An error occured while connecting to the database.\r\n\r\n" + ex2.Message, "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    
                    //TODO: Check to see if the database exists (at this point it might exist, but we don't have access to it)
                    if (dbConn.State == ConnectionState.Open)
                    {
                        //We were succesfull connecting to the master database, prompt the user to see if they want to create a new database.
                        if (MessageBox.Show(string.Format("Failed to find database '{0}', attempt to create new database?\r\n\r\n{1}", cmbDBName.Text, ex.Message), "New Database", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                        {
                            if (clsDB.CreateDatabase(dbConn, cmbDBName.Text))
                            {
                                //The background thread builds a new connection based on the connection string of this one
                                dbConn.Close();
                                dbConn.ConnectionString = String.Format(sConnStringTemplate, cmbDBName.Text);
                                dbConn.Open();
                            }
                            else
                            {
                                MessageBox.Show(string.Format("Failed to create database '{0}'.", cmbDBName.Text), "New Database", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                dbConn.Close();
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("[SqlException]Failed to connect to database.\r\n\r\n" + ex.Message, "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("[Exception]An error occured while connecting to the database.\r\n\r\n" + ex.Message, "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (dbConn.State == ConnectionState.Open)
            {
                if (chkTrustedAuthentication.Checked)
                {
                    AddHistory(new ConnectionHistoryItem(cmbDBServer.Text, cmbDBName.Text));
                }
                else
                {
                    AddHistory(new ConnectionHistoryItem(cmbDBServer.Text, cmbDBName.Text, txtUserName.Text, txtPassword.Text));
                }
                SaveHistory();
                frmMain MainForm = new frmMain(dbConn);
                MainForm.Show();

                this.Hide();
            }
            else
            {
                cmdConnect.Enabled = true;
            }
        }

        private void AddHistory(ConnectionHistoryItem NewItem)
        {
            //Add a new item to the front of the list
            ConnectionHistory.Insert(0, NewItem);

            //Make sure items don't appear more than once
            for (int i = 1; i < ConnectionHistory.Count; i++)
            {
                if (ConnectionHistory[i].ServerName == NewItem.ServerName && ConnectionHistory[i].DatabaseName == NewItem.DatabaseName)
                {
                    ConnectionHistory.RemoveAt(i);
                    i--;
                }
            }
        }

        private void LoadHistory()
        {
            ConnectionHistory = new List<ConnectionHistoryItem>();
            DirectoryInfo dirAppDir = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
            if (dirAppDir.GetDirectories("Livestock Auction").Length <= 0)
            {
                dirAppDir = dirAppDir.CreateSubdirectory("Livestock Auction");
            }
            else
            {
                dirAppDir = dirAppDir.GetDirectories("Livestock Auction")[0];
            }
            if (dirAppDir.GetFiles("history.txt").Length > 0)
            {
                FileInfo fiConfigFile = dirAppDir.GetFiles("history.txt")[0];
                StreamReader srConfig = new StreamReader(fiConfigFile.FullName);

                while (!srConfig.EndOfStream)
                {
                    ConnectionHistory.Add(new ConnectionHistoryItem(srConfig.ReadLine()));
                }

                srConfig.Close();
            }
        }

        private void SaveHistory()
        {
            if (ConnectionHistory.Count > 0)
            {
                DirectoryInfo dirAppDir = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
                if (dirAppDir.GetDirectories("Livestock Auction").Length <= 0)
                {
                    dirAppDir = dirAppDir.CreateSubdirectory("Livestock Auction");
                }
                else
                {
                    dirAppDir = dirAppDir.GetDirectories("Livestock Auction")[0];
                }

                StreamWriter swConfig;
                if (dirAppDir.GetFiles("history.txt").Length > 0)
                {
                    swConfig = new StreamWriter(dirAppDir.GetFiles("history.txt")[0].Open(FileMode.Truncate, FileAccess.Write));
                }
                else
                {
                    swConfig = new StreamWriter(new FileInfo(Path.Combine(dirAppDir.FullName, "history.txt")).Open(FileMode.Create, FileAccess.Write));
                }
                foreach (ConnectionHistoryItem HistoryItem in ConnectionHistory)
                {
                    swConfig.WriteLine(HistoryItem.WriteHistoryRecord());
                }
                swConfig.Close();
            }
        }

        private void cmbDBServer_Leave(object sender, EventArgs e)
        {
            cmbDBName.Items.Clear();
            foreach (ConnectionHistoryItem HistoryItem in ConnectionHistory)
            {
                if (cmbDBServer.Text == HistoryItem.ServerName || cmbDBServer.Text == "")
                {
                    cmbDBName.Items.Add(HistoryItem.DatabaseName);
                }
            }
        }

        private void cmbDBName_Leave(object sender, EventArgs e)
        {
            foreach (ConnectionHistoryItem HistoryItem in ConnectionHistory)
            {
                if (cmbDBServer.Text == HistoryItem.ServerName && cmbDBName.Text == HistoryItem.DatabaseName)
                {
                    if (HistoryItem.TrustedAuthentication)
                    {
                        chkTrustedAuthentication.Checked = true;
                        txtUserName.Text = "";
                    }
                    else
                    {
                        chkTrustedAuthentication.Checked = false;
                        txtUserName.Text = HistoryItem.UserName;
                    }
                }
            }
        }

        private void cmdOpenOrCreate_Click(object sender, EventArgs e)
        {
            if (dlgOpen.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string sDbConnString = string.Format("Data Source={0};File Mode=Read Write;Persist Security Info=False;", dlgOpen.FileName);
                System.Data.SqlServerCe.SqlCeConnection dbConnection = new System.Data.SqlServerCe.SqlCeConnection(sDbConnString);


                //Prompt the user if the file exists
                if (!File.Exists(dlgOpen.FileName))
                {
                    if (MessageBox.Show("Create Database?", "New File", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                    {
                        //Create the SQL CE database
                        System.Data.SqlServerCe.SqlCeEngine engine = new System.Data.SqlServerCe.SqlCeEngine(sDbConnString);
                        engine.CreateDatabase();

                        //Create the database structure within the SQL CE Database
                        dbConnection.Open();
                        clsDB.CreateDatabase(dbConnection, "");
                    }
                }
                else
                {
                    dbConnection.Open();
                }

                if (dbConnection != null && dbConnection.State == ConnectionState.Open)
                {
                    frmMain MainForm = new frmMain(dbConnection);
                    MainForm.Show();
                    this.Hide();
                }
            }
        }

        private void frmConnect_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
        }
    }
}
