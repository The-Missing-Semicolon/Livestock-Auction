namespace Livestock_Auction
{
    partial class frmConnect
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.grbDatabase = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbDBName = new System.Windows.Forms.ComboBox();
            this.cmbDBServer = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.grbAuthentication = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.chkTrustedAuthentication = new System.Windows.Forms.CheckBox();
            this.cmdConnect = new System.Windows.Forms.Button();
            this.cmdOpenOrCreate = new System.Windows.Forms.Button();
            this.dlgOpen = new System.Windows.Forms.OpenFileDialog();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.grbDatabase.SuspendLayout();
            this.grbAuthentication.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // grbDatabase
            // 
            this.grbDatabase.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.grbDatabase.Controls.Add(this.label7);
            this.grbDatabase.Controls.Add(this.label6);
            this.grbDatabase.Controls.Add(this.cmbDBName);
            this.grbDatabase.Controls.Add(this.cmbDBServer);
            this.grbDatabase.Controls.Add(this.label2);
            this.grbDatabase.Controls.Add(this.label1);
            this.grbDatabase.Location = new System.Drawing.Point(6, 55);
            this.grbDatabase.Name = "grbDatabase";
            this.grbDatabase.Size = new System.Drawing.Size(511, 78);
            this.grbDatabase.TabIndex = 0;
            this.grbDatabase.TabStop = false;
            this.grbDatabase.Text = "Database Server";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label7.Location = new System.Drawing.Point(268, 45);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(237, 30);
            this.label7.TabIndex = 6;
            this.label7.Text = "Name of existing database to open or new database to create";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label6.Location = new System.Drawing.Point(268, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(237, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Likely in the format <IP Address>\\SQLEXPRESS";
            // 
            // cmbDBName
            // 
            this.cmbDBName.FormattingEnabled = true;
            this.cmbDBName.Location = new System.Drawing.Point(99, 45);
            this.cmbDBName.Name = "cmbDBName";
            this.cmbDBName.Size = new System.Drawing.Size(163, 21);
            this.cmbDBName.TabIndex = 5;
            this.cmbDBName.Leave += new System.EventHandler(this.cmbDBName_Leave);
            // 
            // cmbDBServer
            // 
            this.cmbDBServer.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbDBServer.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbDBServer.FormattingEnabled = true;
            this.cmbDBServer.Location = new System.Drawing.Point(99, 19);
            this.cmbDBServer.Name = "cmbDBServer";
            this.cmbDBServer.Size = new System.Drawing.Size(163, 21);
            this.cmbDBServer.TabIndex = 4;
            this.cmbDBServer.Leave += new System.EventHandler(this.cmbDBServer_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Database";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Server\\Instance";
            // 
            // grbAuthentication
            // 
            this.grbAuthentication.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.grbAuthentication.Controls.Add(this.label8);
            this.grbAuthentication.Controls.Add(this.label4);
            this.grbAuthentication.Controls.Add(this.label3);
            this.grbAuthentication.Controls.Add(this.txtPassword);
            this.grbAuthentication.Controls.Add(this.txtUserName);
            this.grbAuthentication.Controls.Add(this.chkTrustedAuthentication);
            this.grbAuthentication.Location = new System.Drawing.Point(9, 139);
            this.grbAuthentication.Name = "grbAuthentication";
            this.grbAuthentication.Size = new System.Drawing.Size(508, 100);
            this.grbAuthentication.TabIndex = 1;
            this.grbAuthentication.TabStop = false;
            this.grbAuthentication.Text = "Authentication";
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label8.Location = new System.Drawing.Point(268, 20);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(234, 64);
            this.label8.TabIndex = 7;
            this.label8.Text = "Check \"Trusted Authentication\" if connecting a local SQL Server instance. Provide" +
    " username and password of a SQL Server user for remote connections.";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 71);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Password";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Username";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(74, 68);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(188, 20);
            this.txtPassword.TabIndex = 2;
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(74, 42);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(188, 20);
            this.txtUserName.TabIndex = 1;
            // 
            // chkTrustedAuthentication
            // 
            this.chkTrustedAuthentication.AutoSize = true;
            this.chkTrustedAuthentication.Location = new System.Drawing.Point(6, 19);
            this.chkTrustedAuthentication.Name = "chkTrustedAuthentication";
            this.chkTrustedAuthentication.Size = new System.Drawing.Size(133, 17);
            this.chkTrustedAuthentication.TabIndex = 0;
            this.chkTrustedAuthentication.Text = "Trusted Authentication";
            this.chkTrustedAuthentication.UseVisualStyleBackColor = true;
            this.chkTrustedAuthentication.CheckedChanged += new System.EventHandler(this.chkTrustedAuthentication_CheckedChanged);
            // 
            // cmdConnect
            // 
            this.cmdConnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdConnect.Location = new System.Drawing.Point(9, 243);
            this.cmdConnect.Name = "cmdConnect";
            this.cmdConnect.Size = new System.Drawing.Size(509, 23);
            this.cmdConnect.TabIndex = 2;
            this.cmdConnect.Text = "Connect to Server";
            this.cmdConnect.UseVisualStyleBackColor = true;
            this.cmdConnect.Click += new System.EventHandler(this.cmdConnect_Click);
            // 
            // cmdOpenOrCreate
            // 
            this.cmdOpenOrCreate.Location = new System.Drawing.Point(6, 243);
            this.cmdOpenOrCreate.Name = "cmdOpenOrCreate";
            this.cmdOpenOrCreate.Size = new System.Drawing.Size(258, 23);
            this.cmdOpenOrCreate.TabIndex = 5;
            this.cmdOpenOrCreate.Text = "Open or Create File";
            this.cmdOpenOrCreate.UseVisualStyleBackColor = true;
            this.cmdOpenOrCreate.Click += new System.EventHandler(this.cmdOpenOrCreate_Click);
            // 
            // dlgOpen
            // 
            this.dlgOpen.CheckFileExists = false;
            this.dlgOpen.CheckPathExists = false;
            this.dlgOpen.DefaultExt = "sdf";
            this.dlgOpen.FileName = "AuctionData.sdf";
            this.dlgOpen.Filter = "SQL Compact Database (*.sdf)|*.sdf";
            this.dlgOpen.Title = "Open Database";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.grbDatabase);
            this.groupBox1.Controls.Add(this.grbAuthentication);
            this.groupBox1.Controls.Add(this.cmdConnect);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(524, 281);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Connect to or Create Dataabase";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label5.Location = new System.Drawing.Point(6, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(511, 36);
            this.label5.TabIndex = 3;
            this.label5.Text = "Connect to a local or remote SQL Server instance and open an existing database or" +
    " create a new one for online collaborative use. This database can later be expor" +
    "ted of offline use.";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.cmdOpenOrCreate);
            this.groupBox2.Location = new System.Drawing.Point(542, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(273, 281);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Create or Open Local File";
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label9.Location = new System.Drawing.Point(6, 16);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(261, 51);
            this.label9.TabIndex = 4;
            this.label9.Text = "Create or open a local file for offline usage. This file can later be imported in" +
    "to a database for collaborative use.";
            // 
            // frmConnect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(827, 305);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmConnect";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Open Database";
            this.HelpButtonClicked += new System.ComponentModel.CancelEventHandler(this.frmConnect_HelpButtonClicked);
            this.Load += new System.EventHandler(this.frmConnect_Load);
            this.grbDatabase.ResumeLayout(false);
            this.grbDatabase.PerformLayout();
            this.grbAuthentication.ResumeLayout(false);
            this.grbAuthentication.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grbDatabase;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbDBServer;
        private System.Windows.Forms.ComboBox cmbDBName;
        private System.Windows.Forms.GroupBox grbAuthentication;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.CheckBox chkTrustedAuthentication;
        private System.Windows.Forms.Button cmdConnect;
        private System.Windows.Forms.Button cmdOpenOrCreate;
        private System.Windows.Forms.OpenFileDialog dlgOpen;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label9;
    }
}