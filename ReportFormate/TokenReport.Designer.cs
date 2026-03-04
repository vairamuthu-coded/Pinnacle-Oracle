
namespace Pinnacle.ReportFormate
{
    partial class TokenReport
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
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.crystalReportViewer1 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.butheader = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.checkdatabase = new System.Windows.Forms.CheckBox();
            this.lblcount = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.combobunk = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.todate = new System.Windows.Forms.DateTimePicker();
            this.comboformate = new System.Windows.Forms.ComboBox();
            this.butView = new System.Windows.Forms.Button();
            this.frmdate = new System.Windows.Forms.DateTimePicker();
            this.combocompcode = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtsearch = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.crystalReportViewer2 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.contextMenuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(114, 26);
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.refreshToolStripMenuItem.Text = "Refresh";
            this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.tabControl1);
            this.panel1.Controls.Add(this.butheader);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(1, 1);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3);
            this.panel1.Size = new System.Drawing.Size(1239, 461);
            this.panel1.TabIndex = 41;
            // 
            // crystalReportViewer1
            // 
            this.crystalReportViewer1.ActiveViewIndex = -1;
            this.crystalReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crystalReportViewer1.Cursor = System.Windows.Forms.Cursors.Default;
            this.crystalReportViewer1.DisplayBackgroundEdge = false;
            this.crystalReportViewer1.DisplayStatusBar = false;
            this.crystalReportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crystalReportViewer1.EnableDrillDown = false;
            this.crystalReportViewer1.EnableRefresh = false;
            this.crystalReportViewer1.Location = new System.Drawing.Point(3, 3);
            this.crystalReportViewer1.Name = "crystalReportViewer1";
            this.crystalReportViewer1.Size = new System.Drawing.Size(1212, 306);
            this.crystalReportViewer1.TabIndex = 42;
            this.crystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
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
            this.butheader.Location = new System.Drawing.Point(3, 3);
            this.butheader.Margin = new System.Windows.Forms.Padding(0);
            this.butheader.Name = "butheader";
            this.butheader.Size = new System.Drawing.Size(1233, 32);
            this.butheader.TabIndex = 462;
            this.butheader.Text = "TOKEN REPORT";
            this.butheader.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.butheader.UseVisualStyleBackColor = false;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.checkdatabase);
            this.panel2.Controls.Add(this.lblcount);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.combobunk);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.todate);
            this.panel2.Controls.Add(this.comboformate);
            this.panel2.Controls.Add(this.butView);
            this.panel2.Controls.Add(this.frmdate);
            this.panel2.Controls.Add(this.combocompcode);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.txtsearch);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Location = new System.Drawing.Point(6, 40);
            this.panel2.Margin = new System.Windows.Forms.Padding(5);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(3);
            this.panel2.Size = new System.Drawing.Size(1261, 63);
            this.panel2.TabIndex = 41;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(875, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(212, 18);
            this.label1.TabIndex = 108;
            this.label1.Text = "Database Changed on 20-April-2023";
            // 
            // checkdatabase
            // 
            this.checkdatabase.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.checkdatabase.AutoSize = true;
            this.checkdatabase.Checked = true;
            this.checkdatabase.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkdatabase.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.checkdatabase.Location = new System.Drawing.Point(635, 37);
            this.checkdatabase.Name = "checkdatabase";
            this.checkdatabase.Size = new System.Drawing.Size(82, 22);
            this.checkdatabase.TabIndex = 107;
            this.checkdatabase.Text = "PAYROLL";
            this.checkdatabase.UseVisualStyleBackColor = true;
            this.checkdatabase.CheckedChanged += new System.EventHandler(this.checkdatabase_CheckedChanged);
            // 
            // lblcount
            // 
            this.lblcount.AutoSize = true;
            this.lblcount.Location = new System.Drawing.Point(900, 41);
            this.lblcount.Name = "lblcount";
            this.lblcount.Size = new System.Drawing.Size(42, 18);
            this.lblcount.TabIndex = 102;
            this.lblcount.Text = "Count";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 37);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(64, 18);
            this.label6.TabIndex = 100;
            this.label6.Text = "Fuel Type";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(266, 33);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(71, 18);
            this.label7.TabIndex = 99;
            this.label7.Text = "PetrolBunk";
            // 
            // combobunk
            // 
            this.combobunk.FormattingEnabled = true;
            this.combobunk.Items.AddRange(new object[] {
            "Excel",
            "PDF",
            "CSV",
            "Word"});
            this.combobunk.Location = new System.Drawing.Point(341, 33);
            this.combobunk.Name = "combobunk";
            this.combobunk.Size = new System.Drawing.Size(288, 26);
            this.combobunk.TabIndex = 97;
            this.combobunk.SelectedIndexChanged += new System.EventHandler(this.combobunk_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(266, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 18);
            this.label4.TabIndex = 96;
            this.label4.Text = "From";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(472, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(22, 18);
            this.label3.TabIndex = 95;
            this.label3.Text = "To";
            // 
            // todate
            // 
            this.todate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.todate.Location = new System.Drawing.Point(508, 4);
            this.todate.Name = "todate";
            this.todate.Size = new System.Drawing.Size(121, 25);
            this.todate.TabIndex = 94;
            this.todate.Value = new System.DateTime(2021, 6, 30, 19, 6, 0, 0);
            // 
            // comboformate
            // 
            this.comboformate.FormattingEnabled = true;
            this.comboformate.Items.AddRange(new object[] {
            "Excel",
            "PDF",
            "CSV",
            "Word"});
            this.comboformate.Location = new System.Drawing.Point(635, 5);
            this.comboformate.Name = "comboformate";
            this.comboformate.Size = new System.Drawing.Size(122, 26);
            this.comboformate.TabIndex = 93;
            this.comboformate.SelectedIndexChanged += new System.EventHandler(this.comboformate_SelectedIndexChanged);
            // 
            // butView
            // 
            this.butView.Location = new System.Drawing.Point(772, 10);
            this.butView.Name = "butView";
            this.butView.Size = new System.Drawing.Size(97, 44);
            this.butView.TabIndex = 5;
            this.butView.Text = "View";
            this.butView.UseVisualStyleBackColor = true;
            this.butView.Click += new System.EventHandler(this.butView_Click);
            // 
            // frmdate
            // 
            this.frmdate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.frmdate.Location = new System.Drawing.Point(342, 4);
            this.frmdate.Name = "frmdate";
            this.frmdate.Size = new System.Drawing.Size(121, 25);
            this.frmdate.TabIndex = 4;
            this.frmdate.Value = new System.DateTime(2021, 6, 1, 19, 6, 0, 0);
            // 
            // combocompcode
            // 
            this.combocompcode.FormattingEnabled = true;
            this.combocompcode.Location = new System.Drawing.Point(79, 33);
            this.combocompcode.Name = "combocompcode";
            this.combocompcode.Size = new System.Drawing.Size(176, 26);
            this.combocompcode.TabIndex = 3;
            this.combocompcode.SelectedIndexChanged += new System.EventHandler(this.combocompcode_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(287, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 18);
            this.label2.TabIndex = 2;
            // 
            // txtsearch
            // 
            this.txtsearch.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtsearch.Location = new System.Drawing.Point(79, 5);
            this.txtsearch.Name = "txtsearch";
            this.txtsearch.Size = new System.Drawing.Size(176, 25);
            this.txtsearch.TabIndex = 1;
            this.txtsearch.TextChanged += new System.EventHandler(this.txtsearch_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 14);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 18);
            this.label5.TabIndex = 0;
            this.label5.Text = "Search";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // printDialog1
            // 
            this.printDialog1.UseEXDialog = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(7, 112);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1226, 343);
            this.tabControl1.TabIndex = 463;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.crystalReportViewer1);
            this.tabPage1.Location = new System.Drawing.Point(4, 27);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1218, 312);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "TokenDetails";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.crystalReportViewer2);
            this.tabPage2.Location = new System.Drawing.Point(4, 27);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1218, 312);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Date Wise";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // crystalReportViewer2
            // 
            this.crystalReportViewer2.ActiveViewIndex = -1;
            this.crystalReportViewer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crystalReportViewer2.Cursor = System.Windows.Forms.Cursors.Default;
            this.crystalReportViewer2.DisplayBackgroundEdge = false;
            this.crystalReportViewer2.DisplayStatusBar = false;
            this.crystalReportViewer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crystalReportViewer2.EnableDrillDown = false;
            this.crystalReportViewer2.EnableRefresh = false;
            this.crystalReportViewer2.Location = new System.Drawing.Point(3, 3);
            this.crystalReportViewer2.Name = "crystalReportViewer2";
            this.crystalReportViewer2.Size = new System.Drawing.Size(1212, 306);
            this.crystalReportViewer2.TabIndex = 43;
            this.crystalReportViewer2.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            // 
            // TokenReport
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1241, 468);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "TokenReport";
            this.Padding = new System.Windows.Forms.Padding(1, 0, 1, 1);
            this.Text = "TokenReport";
            this.Load += new System.EventHandler(this.TokenDetails_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker todate;
        private System.Windows.Forms.ComboBox comboformate;
        private System.Windows.Forms.Button butView;
        private System.Windows.Forms.DateTimePicker frmdate;
        private System.Windows.Forms.ComboBox combocompcode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtsearch;
        private System.Windows.Forms.Label label5;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.ComboBox combobunk;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblcount;
        private System.Windows.Forms.Button butheader;
        private System.Windows.Forms.CheckBox checkdatabase;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer2;
    }
}