
namespace Pinnacle.ReportFormate
{
    partial class MonthWiseFuelReport
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
            this.butheader = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.crystalReportViewer1 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.refreshToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.crystalReportViewer2 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.KiloMeter = new System.Windows.Forms.TabPage();
            this.crystalReportViewer3 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.crystalReportViewer4 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.checkdatabase = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.combovechineno = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.combobunk = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.todate = new System.Windows.Forms.DateTimePicker();
            this.comboformate = new System.Windows.Forms.ComboBox();
            this.butView = new System.Windows.Forms.Button();
            this.frmdate = new System.Windows.Forms.DateTimePicker();
            this.combocompcode = new System.Windows.Forms.ComboBox();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.contextMenuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.KiloMeter.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.panel2.SuspendLayout();
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
            this.panel1.Controls.Add(this.butheader);
            this.panel1.Controls.Add(this.tabControl1);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Location = new System.Drawing.Point(1, 1);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3);
            this.panel1.Size = new System.Drawing.Size(1265, 504);
            this.panel1.TabIndex = 39;
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
            this.butheader.Size = new System.Drawing.Size(1259, 32);
            this.butheader.TabIndex = 463;
            this.butheader.Text = "MONTH WISE FUEL REPORT";
            this.butheader.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.butheader.UseVisualStyleBackColor = false;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.KiloMeter);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(10, 118);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1249, 380);
            this.tabControl1.TabIndex = 42;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.White;
            this.tabPage1.Controls.Add(this.crystalReportViewer1);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1241, 351);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Month  & Date Wise Report";
            // 
            // crystalReportViewer1
            // 
            this.crystalReportViewer1.ActiveViewIndex = -1;
            this.crystalReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crystalReportViewer1.ContextMenuStrip = this.contextMenuStrip2;
            this.crystalReportViewer1.Cursor = System.Windows.Forms.Cursors.Default;
            this.crystalReportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crystalReportViewer1.Location = new System.Drawing.Point(3, 3);
            this.crystalReportViewer1.Name = "crystalReportViewer1";
            this.crystalReportViewer1.Size = new System.Drawing.Size(1235, 345);
            this.crystalReportViewer1.TabIndex = 41;
            this.crystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshToolStripMenuItem1});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(114, 26);
            // 
            // refreshToolStripMenuItem1
            // 
            this.refreshToolStripMenuItem1.Name = "refreshToolStripMenuItem1";
            this.refreshToolStripMenuItem1.Size = new System.Drawing.Size(113, 22);
            this.refreshToolStripMenuItem1.Text = "Refresh";
            this.refreshToolStripMenuItem1.Click += new System.EventHandler(this.refreshToolStripMenuItem1_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.White;
            this.tabPage2.Controls.Add(this.crystalReportViewer2);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1241, 351);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Month Wise Report";
            // 
            // crystalReportViewer2
            // 
            this.crystalReportViewer2.ActiveViewIndex = -1;
            this.crystalReportViewer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crystalReportViewer2.ContextMenuStrip = this.contextMenuStrip2;
            this.crystalReportViewer2.Cursor = System.Windows.Forms.Cursors.Default;
            this.crystalReportViewer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crystalReportViewer2.Location = new System.Drawing.Point(3, 3);
            this.crystalReportViewer2.Name = "crystalReportViewer2";
            this.crystalReportViewer2.Size = new System.Drawing.Size(1235, 345);
            this.crystalReportViewer2.TabIndex = 42;
            this.crystalReportViewer2.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            // 
            // KiloMeter
            // 
            this.KiloMeter.Controls.Add(this.crystalReportViewer3);
            this.KiloMeter.Location = new System.Drawing.Point(4, 25);
            this.KiloMeter.Name = "KiloMeter";
            this.KiloMeter.Padding = new System.Windows.Forms.Padding(3);
            this.KiloMeter.Size = new System.Drawing.Size(1241, 351);
            this.KiloMeter.TabIndex = 2;
            this.KiloMeter.Text = "Kilo Meter";
            this.KiloMeter.UseVisualStyleBackColor = true;
            // 
            // crystalReportViewer3
            // 
            this.crystalReportViewer3.ActiveViewIndex = -1;
            this.crystalReportViewer3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crystalReportViewer3.Cursor = System.Windows.Forms.Cursors.Default;
            this.crystalReportViewer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crystalReportViewer3.Location = new System.Drawing.Point(3, 3);
            this.crystalReportViewer3.Name = "crystalReportViewer3";
            this.crystalReportViewer3.Size = new System.Drawing.Size(1235, 345);
            this.crystalReportViewer3.TabIndex = 0;
            this.crystalReportViewer3.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.crystalReportViewer4);
            this.tabPage4.Location = new System.Drawing.Point(4, 25);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(1241, 351);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Vehicle Number Wise";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // crystalReportViewer4
            // 
            this.crystalReportViewer4.ActiveViewIndex = -1;
            this.crystalReportViewer4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crystalReportViewer4.ContextMenuStrip = this.contextMenuStrip2;
            this.crystalReportViewer4.Cursor = System.Windows.Forms.Cursors.Default;
            this.crystalReportViewer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crystalReportViewer4.Location = new System.Drawing.Point(3, 3);
            this.crystalReportViewer4.Name = "crystalReportViewer4";
            this.crystalReportViewer4.Size = new System.Drawing.Size(1235, 345);
            this.crystalReportViewer4.TabIndex = 43;
            this.crystalReportViewer4.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.ContextMenuStrip = this.contextMenuStrip2;
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.checkdatabase);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.combovechineno);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.combobunk);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.todate);
            this.panel2.Controls.Add(this.comboformate);
            this.panel2.Controls.Add(this.butView);
            this.panel2.Controls.Add(this.frmdate);
            this.panel2.Controls.Add(this.combocompcode);
            this.panel2.Location = new System.Drawing.Point(10, 47);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1249, 68);
            this.panel2.TabIndex = 40;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(943, 12);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(212, 18);
            this.label6.TabIndex = 105;
            this.label6.Text = "Database Changed on 20-April-2023";
            // 
            // checkdatabase
            // 
            this.checkdatabase.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.checkdatabase.AutoSize = true;
            this.checkdatabase.Checked = true;
            this.checkdatabase.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkdatabase.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.checkdatabase.Location = new System.Drawing.Point(683, 37);
            this.checkdatabase.Name = "checkdatabase";
            this.checkdatabase.Size = new System.Drawing.Size(82, 22);
            this.checkdatabase.TabIndex = 104;
            this.checkdatabase.Text = "PAYROLL";
            this.checkdatabase.UseVisualStyleBackColor = true;
            this.checkdatabase.CheckedChanged += new System.EventHandler(this.checkdatabase_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(680, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 18);
            this.label1.TabIndex = 103;
            this.label1.Text = "DownLoad Type";
            // 
            // combovechineno
            // 
            this.combovechineno.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.combovechineno.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.combovechineno.DropDownHeight = 80;
            this.combovechineno.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.combovechineno.FormattingEnabled = true;
            this.combovechineno.IntegralHeight = false;
            this.combovechineno.Items.AddRange(new object[] {
            ""});
            this.combovechineno.Location = new System.Drawing.Point(91, 5);
            this.combovechineno.Name = "combovechineno";
            this.combovechineno.Size = new System.Drawing.Size(182, 22);
            this.combovechineno.TabIndex = 100;
            this.combovechineno.SelectedIndexChanged += new System.EventHandler(this.combovechineno_SelectedIndexChanged_1);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 18);
            this.label2.TabIndex = 102;
            this.label2.Text = "Fuel Type";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(291, 42);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(71, 18);
            this.label7.TabIndex = 101;
            this.label7.Text = "PetrolBunk";
            // 
            // combobunk
            // 
            this.combobunk.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.combobunk.FormattingEnabled = true;
            this.combobunk.Items.AddRange(new object[] {
            "Excel",
            "PDF",
            "CSV",
            "Word"});
            this.combobunk.Location = new System.Drawing.Point(409, 37);
            this.combobunk.Name = "combobunk";
            this.combobunk.Size = new System.Drawing.Size(254, 22);
            this.combobunk.TabIndex = 100;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 11);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 18);
            this.label5.TabIndex = 97;
            this.label5.Text = "Veh-Search";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(291, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 18);
            this.label4.TabIndex = 96;
            this.label4.Text = "From";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(516, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(22, 18);
            this.label3.TabIndex = 95;
            this.label3.Text = "To";
            // 
            // todate
            // 
            this.todate.CalendarFont = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.todate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.todate.Location = new System.Drawing.Point(567, 7);
            this.todate.Name = "todate";
            this.todate.Size = new System.Drawing.Size(96, 25);
            this.todate.TabIndex = 94;
            this.todate.Value = new System.DateTime(2021, 7, 30, 13, 18, 0, 0);
            // 
            // comboformate
            // 
            this.comboformate.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comboformate.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboformate.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboformate.Items.AddRange(new object[] {
            "Excel",
            "PDF",
            "CSV",
            "Word"});
            this.comboformate.Location = new System.Drawing.Point(786, 7);
            this.comboformate.Name = "comboformate";
            this.comboformate.Size = new System.Drawing.Size(122, 22);
            this.comboformate.TabIndex = 93;
            this.comboformate.SelectedIndexChanged += new System.EventHandler(this.comboformate_SelectedIndexChanged);
            // 
            // butView
            // 
            this.butView.Location = new System.Drawing.Point(786, 33);
            this.butView.Name = "butView";
            this.butView.Size = new System.Drawing.Size(75, 26);
            this.butView.TabIndex = 5;
            this.butView.Text = "View";
            this.butView.UseVisualStyleBackColor = true;
            this.butView.Click += new System.EventHandler(this.butView_Click);
            // 
            // frmdate
            // 
            this.frmdate.CalendarFont = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.frmdate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.frmdate.Location = new System.Drawing.Point(409, 7);
            this.frmdate.Name = "frmdate";
            this.frmdate.Size = new System.Drawing.Size(90, 25);
            this.frmdate.TabIndex = 4;
            this.frmdate.Value = new System.DateTime(2021, 7, 1, 13, 18, 0, 0);
            // 
            // combocompcode
            // 
            this.combocompcode.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.combocompcode.FormattingEnabled = true;
            this.combocompcode.Items.AddRange(new object[] {
            ""});
            this.combocompcode.Location = new System.Drawing.Point(91, 33);
            this.combocompcode.Name = "combocompcode";
            this.combocompcode.Size = new System.Drawing.Size(182, 22);
            this.combocompcode.TabIndex = 3;
            this.combocompcode.SelectedIndexChanged += new System.EventHandler(this.combocompcode_SelectedIndexChanged);
            // 
            // printDialog1
            // 
            this.printDialog1.UseEXDialog = true;
            // 
            // MonthWiseFuelReport
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.ClientSize = new System.Drawing.Size(1270, 510);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MonthWiseFuelReport";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.Text = "MonthWiseFuelReport";
            this.Load += new System.EventHandler(this.MonthWiseFuelReport_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.contextMenuStrip2.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.KiloMeter.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
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
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        private System.Windows.Forms.Label label5;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox combobunk;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem1;
        private System.Windows.Forms.ComboBox combovechineno;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer1;
        private System.Windows.Forms.TabPage tabPage2;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer2;
        private System.Windows.Forms.TabPage KiloMeter;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer3;
        private System.Windows.Forms.TabPage tabPage4;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button butheader;
        private System.Windows.Forms.CheckBox checkdatabase;
        private System.Windows.Forms.Label label6;
    }
}