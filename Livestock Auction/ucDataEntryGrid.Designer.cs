namespace Livestock_Auction
{
    partial class ucDataEntryGrid
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucDataEntryGrid));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tscmdNewRecord = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tscmdNewRecord});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(653, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tscmdNewRecord
            // 
            this.tscmdNewRecord.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tscmdNewRecord.Image = ((System.Drawing.Image)(resources.GetObject("tscmdNewRecord.Image")));
            this.tscmdNewRecord.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tscmdNewRecord.Name = "tscmdNewRecord";
            this.tscmdNewRecord.Size = new System.Drawing.Size(23, 22);
            this.tscmdNewRecord.Text = "New Record";
            this.tscmdNewRecord.Click += new System.EventHandler(this.tscmdNewRecord_Click);
            // 
            // ucDataEntryGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.toolStrip1);
            this.Name = "ucDataEntryGrid";
            this.Size = new System.Drawing.Size(653, 503);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tscmdNewRecord;
    }
}
