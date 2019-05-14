namespace Livestock_Auction
{
    partial class frmBrowseName
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
            this.lsvNames = new System.Windows.Forms.ListView();
            this.colNameFirst = new System.Windows.Forms.ColumnHeader();
            this.colNameLast = new System.Windows.Forms.ColumnHeader();
            this.colNameNick = new System.Windows.Forms.ColumnHeader();
            this.SuspendLayout();
            // 
            // lsvNames
            // 
            this.lsvNames.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colNameFirst,
            this.colNameNick,
            this.colNameLast});
            this.lsvNames.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lsvNames.FullRowSelect = true;
            this.lsvNames.Location = new System.Drawing.Point(0, 0);
            this.lsvNames.Name = "lsvNames";
            this.lsvNames.Size = new System.Drawing.Size(429, 387);
            this.lsvNames.TabIndex = 0;
            this.lsvNames.UseCompatibleStateImageBehavior = false;
            this.lsvNames.View = System.Windows.Forms.View.Details;
            this.lsvNames.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lsvNames_MouseDoubleClick);
            // 
            // colNameFirst
            // 
            this.colNameFirst.Text = "First Name";
            // 
            // colNameLast
            // 
            this.colNameLast.Text = "Last Name";
            this.colNameLast.Width = 141;
            // 
            // colNameNick
            // 
            this.colNameNick.Text = "Nick Name";
            this.colNameNick.Width = 117;
            // 
            // frmBrowseName
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(429, 387);
            this.Controls.Add(this.lsvNames);
            this.Name = "frmBrowseName";
            this.Text = "Browse Names";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmBrowseName_FormClosed);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lsvNames;
        private System.Windows.Forms.ColumnHeader colNameFirst;
        private System.Windows.Forms.ColumnHeader colNameNick;
        private System.Windows.Forms.ColumnHeader colNameLast;
    }
}