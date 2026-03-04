namespace Pinnacle.Transactions
{
    partial class CompanyWiseSecurityInventry
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
            this.components = new System.ComponentModel.Container();
            this.combocompcode = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_Header = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.todate = new System.Windows.Forms.DateTimePicker();
            this.frmdate = new System.Windows.Forms.DateTimePicker();
            this.label12 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtsearch = new System.Windows.Forms.TextBox();
            this.lblattsearch = new System.Windows.Forms.Label();
            this.butview = new System.Windows.Forms.Button();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.panel1 = new System.Windows.Forms.Panel();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.checkdatabase = new System.Windows.Forms.CheckBox();
            this.comboformate = new System.Windows.Forms.ComboBox();
            this.QrCodeCrystalReport1 = new Pinnacle.Report.QrCodeCrystalReport();
            this.crystalReportViewer1 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SecurityCrystalReport3 = new Pinnacle.Report.SecurityCrystalReport();
            this.SecurityCrystalReport1 = new Pinnacle.Report.SecurityCrystalReport();
            this.CompanywiseSecurityReport2 = new Pinnacle.Report.CompanywiseSecurityReport();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lbloutward = new System.Windows.Forms.Label();
            this.lblInward = new System.Windows.Forms.Label();
            this.lblcount = new System.Windows.Forms.Label();
            this.butheader = new System.Windows.Forms.Button();
            this.butfooter = new System.Windows.Forms.Button();
            this.contextMenuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // combocompcode
            // 
            this.combocompcode.FormattingEnabled = true;
            this.combocompcode.Location = new System.Drawing.Point(97, 8);
            this.combocompcode.Name = "combocompcode";
            this.combocompcode.Size = new System.Drawing.Size(101, 22);
            this.combocompcode.TabIndex = 1;
            this.combocompcode.SelectedIndexChanged += new System.EventHandler(this.Combocompcode_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(32, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 14);
            this.label1.TabIndex = 2;
            this.label1.Text = "CompCode";
            this.label1.Click += new System.EventHandler(this.Label1_Click);
            // 
            // lbl_Header
            // 
            this.lbl_Header.AutoSize = true;
            this.lbl_Header.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.lbl_Header.Font = new System.Drawing.Font("Palatino Linotype", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Header.ForeColor = System.Drawing.Color.White;
            this.lbl_Header.Location = new System.Drawing.Point(9, -1);
            this.lbl_Header.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_Header.Name = "lbl_Header";
            this.lbl_Header.Size = new System.Drawing.Size(14, 21);
            this.lbl_Header.TabIndex = 32;
            this.lbl_Header.Text = ".";
            this.lbl_Header.Click += new System.EventHandler(this.Lbl_Header_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(114, 26);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.ContextMenuStrip1_Opening);
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.refreshToolStripMenuItem.Text = "Refresh";
            this.refreshToolStripMenuItem.Click += new System.EventHandler(this.RefreshToolStripMenuItem_Click);
            // 
            // todate
            // 
            this.todate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.todate.Location = new System.Drawing.Point(612, 9);
            this.todate.MaxDate = new System.DateTime(9998, 1, 31, 0, 0, 0, 0);
            this.todate.Name = "todate";
            this.todate.Size = new System.Drawing.Size(90, 20);
            this.todate.TabIndex = 89;
            this.todate.Value = new System.DateTime(2021, 1, 22, 16, 14, 0, 0);
            this.todate.ValueChanged += new System.EventHandler(this.Todate_ValueChanged);
            // 
            // frmdate
            // 
            this.frmdate.CalendarFont = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.frmdate.CalendarForeColor = System.Drawing.Color.RoyalBlue;
            this.frmdate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.frmdate.Location = new System.Drawing.Point(432, 9);
            this.frmdate.MinDate = new System.DateTime(2020, 5, 1, 0, 0, 0, 0);
            this.frmdate.Name = "frmdate";
            this.frmdate.Size = new System.Drawing.Size(90, 20);
            this.frmdate.TabIndex = 88;
            this.frmdate.Value = new System.DateTime(2020, 12, 16, 15, 33, 19, 0);
            this.frmdate.ValueChanged += new System.EventHandler(this.Frmdate_ValueChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Black;
            this.label12.Location = new System.Drawing.Point(541, 11);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(52, 18);
            this.label12.TabIndex = 87;
            this.label12.Text = "To Date";
            this.label12.Click += new System.EventHandler(this.Label12_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(345, 11);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(71, 18);
            this.label10.TabIndex = 86;
            this.label10.Text = "From Date";
            this.label10.Click += new System.EventHandler(this.Label10_Click);
            // 
            // txtsearch
            // 
            this.txtsearch.Location = new System.Drawing.Point(782, 8);
            this.txtsearch.Name = "txtsearch";
            this.txtsearch.Size = new System.Drawing.Size(151, 20);
            this.txtsearch.TabIndex = 85;
            this.txtsearch.TextChanged += new System.EventHandler(this.Txtsearch_TextChanged);
            // 
            // lblattsearch
            // 
            this.lblattsearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblattsearch.AutoSize = true;
            this.lblattsearch.BackColor = System.Drawing.Color.Transparent;
            this.lblattsearch.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblattsearch.ForeColor = System.Drawing.Color.Black;
            this.lblattsearch.Location = new System.Drawing.Point(717, 8);
            this.lblattsearch.Name = "lblattsearch";
            this.lblattsearch.Size = new System.Drawing.Size(50, 18);
            this.lblattsearch.TabIndex = 84;
            this.lblattsearch.Text = "Search";
            this.lblattsearch.Click += new System.EventHandler(this.Lblattsearch_Click);
            // 
            // butview
            // 
            this.butview.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.butview.BackColor = System.Drawing.Color.Transparent;
            this.butview.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butview.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.butview.Location = new System.Drawing.Point(960, 5);
            this.butview.Margin = new System.Windows.Forms.Padding(0);
            this.butview.Name = "butview";
            this.butview.Size = new System.Drawing.Size(80, 25);
            this.butview.TabIndex = 90;
            this.butview.Text = " View";
            this.butview.UseVisualStyleBackColor = false;
            this.butview.Click += new System.EventHandler(this.Butview_Click);
            // 
            // printDialog1
            // 
            this.printDialog1.UseEXDialog = true;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.comboBox1);
            this.panel1.Controls.Add(this.checkdatabase);
            this.panel1.Controls.Add(this.comboformate);
            this.panel1.Controls.Add(this.butview);
            this.panel1.Controls.Add(this.combocompcode);
            this.panel1.Controls.Add(this.todate);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.frmdate);
            this.panel1.Controls.Add(this.lblattsearch);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.txtsearch);
            this.panel1.Controls.Add(this.label10);
            this.panel1.ForeColor = System.Drawing.Color.White;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1227, 36);
            this.panel1.TabIndex = 91;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "IN",
            "OUT"});
            this.comboBox1.Location = new System.Drawing.Point(214, 9);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(101, 22);
            this.comboBox1.TabIndex = 109;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged_1);
            // 
            // checkdatabase
            // 
            this.checkdatabase.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.checkdatabase.AutoSize = true;
            this.checkdatabase.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.checkdatabase.Location = new System.Drawing.Point(1177, 8);
            this.checkdatabase.Name = "checkdatabase";
            this.checkdatabase.Size = new System.Drawing.Size(73, 18);
            this.checkdatabase.TabIndex = 108;
            this.checkdatabase.Text = "PAYROLL";
            this.checkdatabase.UseVisualStyleBackColor = true;
            this.checkdatabase.CheckedChanged += new System.EventHandler(this.checkdatabase_CheckedChanged);
            // 
            // comboformate
            // 
            this.comboformate.FormattingEnabled = true;
            this.comboformate.Items.AddRange(new object[] {
            "---",
            "Word",
            "Excel",
            "PDF",
            "CSV"});
            this.comboformate.Location = new System.Drawing.Point(1077, 7);
            this.comboformate.Name = "comboformate";
            this.comboformate.Size = new System.Drawing.Size(76, 22);
            this.comboformate.TabIndex = 92;
            this.comboformate.SelectedIndexChanged += new System.EventHandler(this.ComboBox1_SelectedIndexChanged);
            // 
            // crystalReportViewer1
            // 
            this.crystalReportViewer1.ActiveViewIndex = -1;
            this.crystalReportViewer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.crystalReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crystalReportViewer1.Cursor = System.Windows.Forms.Cursors.Default;
            this.crystalReportViewer1.DisplayStatusBar = false;
            this.crystalReportViewer1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.crystalReportViewer1.ForeColor = System.Drawing.Color.White;
            this.crystalReportViewer1.Location = new System.Drawing.Point(13, 39);
            this.crystalReportViewer1.Name = "crystalReportViewer1";
            this.crystalReportViewer1.Size = new System.Drawing.Size(973, 389);
            this.crystalReportViewer1.TabIndex = 92;
            this.crystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            this.crystalReportViewer1.Load += new System.EventHandler(this.CrystalReportViewer1_Load_1);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel2.Controls.Add(this.lbloutward);
            this.panel2.Controls.Add(this.lblInward);
            this.panel2.Controls.Add(this.lblcount);
            this.panel2.Controls.Add(this.crystalReportViewer1);
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel2.Location = new System.Drawing.Point(0, 37);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1227, 443);
            this.panel2.TabIndex = 93;
            // 
            // lbloutward
            // 
            this.lbloutward.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbloutward.AutoSize = true;
            this.lbloutward.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbloutward.Location = new System.Drawing.Point(1049, 131);
            this.lbloutward.Name = "lbloutward";
            this.lbloutward.Size = new System.Drawing.Size(146, 16);
            this.lbloutward.TabIndex = 95;
            this.lbloutward.Text = "Outward Total Record";
            // 
            // lblInward
            // 
            this.lblInward.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblInward.AutoSize = true;
            this.lblInward.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInward.Location = new System.Drawing.Point(1049, 99);
            this.lblInward.Name = "lblInward";
            this.lblInward.Size = new System.Drawing.Size(136, 16);
            this.lblInward.TabIndex = 94;
            this.lblInward.Text = "Inward Total Record";
            // 
            // lblcount
            // 
            this.lblcount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblcount.AutoSize = true;
            this.lblcount.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblcount.Location = new System.Drawing.Point(1049, 71);
            this.lblcount.Name = "lblcount";
            this.lblcount.Size = new System.Drawing.Size(80, 16);
            this.lblcount.TabIndex = 93;
            this.lblcount.Text = "Total Count";
            // 
            // butheader
            // 
            this.butheader.BackColor = System.Drawing.Color.Teal;
            this.butheader.Dock = System.Windows.Forms.DockStyle.Top;
            this.butheader.FlatAppearance.BorderSize = 0;
            this.butheader.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.butheader.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.butheader.Font = new System.Drawing.Font("Roboto Black", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butheader.ForeColor = System.Drawing.Color.White;
            this.butheader.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.butheader.Location = new System.Drawing.Point(0, 0);
            this.butheader.Margin = new System.Windows.Forms.Padding(0);
            this.butheader.Name = "butheader";
            this.butheader.Size = new System.Drawing.Size(1227, 32);
            this.butheader.TabIndex = 461;
            this.butheader.Text = "COMPANY WISE SECURITY INVENTORY";
            this.butheader.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.butheader.UseVisualStyleBackColor = false;
            // 
            // butfooter
            // 
            this.butfooter.BackColor = System.Drawing.Color.Teal;
            this.butfooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.butfooter.FlatAppearance.BorderSize = 0;
            this.butfooter.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.butfooter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.butfooter.Font = new System.Drawing.Font("Roboto Black", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butfooter.ForeColor = System.Drawing.Color.White;
            this.butfooter.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.butfooter.Location = new System.Drawing.Point(0, 490);
            this.butfooter.Margin = new System.Windows.Forms.Padding(0);
            this.butfooter.Name = "butfooter";
            this.butfooter.Size = new System.Drawing.Size(1227, 10);
            this.butfooter.TabIndex = 462;
            this.butfooter.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.butfooter.UseVisualStyleBackColor = false;
            // 
            // CompanyWiseSecurityInventry
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(1227, 500);
            this.Controls.Add(this.butfooter);
            this.Controls.Add(this.butheader);
            this.Controls.Add(this.lbl_Header);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("Roboto Black", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CompanyWiseSecurityInventry";
            this.Text = "CompanyWiseSecurityInventry";
            this.Load += new System.EventHandler(this.CompanyWiseSecurityInventry_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox combocompcode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl_Header;
        private System.Windows.Forms.DateTimePicker todate;
        private System.Windows.Forms.DateTimePicker frmdate;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtsearch;
        private System.Windows.Forms.Label lblattsearch;
        private System.Windows.Forms.Button butview;
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox comboformate;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        private Report.CompanywiseSecurityReport CompanywiseSecurityReport2;
        private Report.QrCodeCrystalReport QrCodeCrystalReport1;
        private Report.SecurityCrystalReport SecurityCrystalReport1;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer1;
        private Report.SecurityCrystalReport SecurityCrystalReport3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblcount;
        private System.Windows.Forms.Label lbloutward;
        private System.Windows.Forms.Label lblInward;
        private System.Windows.Forms.Button butheader;
        private System.Windows.Forms.Button butfooter;
        private System.Windows.Forms.CheckBox checkdatabase;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}