
namespace Pinnacle.ReportFormate
{
    partial class MovementReport
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
            this.rToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lbl_Header = new System.Windows.Forms.Label();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.outTimeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.withoutInTimeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.withoutInTimeOutTimeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hostelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.checkdatabase = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.lblcount = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.combohostel = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.comboidcardsearch = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.todate = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.comboformate = new System.Windows.Forms.ComboBox();
            this.butView = new System.Windows.Forms.Button();
            this.frmdate = new System.Windows.Forms.DateTimePicker();
            this.combocompcode = new System.Windows.Forms.ComboBox();
            this.txtsearch = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.butheader = new System.Windows.Forms.Button();
            this.butfooter = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.crystalReportViewer1 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.contextMenuStrip1.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(114, 26);
            // 
            // rToolStripMenuItem
            // 
            this.rToolStripMenuItem.Name = "rToolStripMenuItem";
            this.rToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.rToolStripMenuItem.Text = "Refresh";
            this.rToolStripMenuItem.Click += new System.EventHandler(this.rToolStripMenuItem_Click);
            // 
            // lbl_Header
            // 
            this.lbl_Header.AutoSize = true;
            this.lbl_Header.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.lbl_Header.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Header.ForeColor = System.Drawing.Color.White;
            this.lbl_Header.Location = new System.Drawing.Point(3, 8);
            this.lbl_Header.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_Header.Name = "lbl_Header";
            this.lbl_Header.Size = new System.Drawing.Size(12, 16);
            this.lbl_Header.TabIndex = 46;
            this.lbl_Header.Text = ".";
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.outTimeToolStripMenuItem,
            this.withoutInTimeToolStripMenuItem,
            this.withoutInTimeOutTimeToolStripMenuItem,
            this.hostelToolStripMenuItem});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(209, 92);
            // 
            // outTimeToolStripMenuItem
            // 
            this.outTimeToolStripMenuItem.Name = "outTimeToolStripMenuItem";
            this.outTimeToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.outTimeToolStripMenuItem.Text = "OutTime Only";
            this.outTimeToolStripMenuItem.Click += new System.EventHandler(this.outTimeToolStripMenuItem_Click);
            // 
            // withoutInTimeToolStripMenuItem
            // 
            this.withoutInTimeToolStripMenuItem.Name = "withoutInTimeToolStripMenuItem";
            this.withoutInTimeToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.withoutInTimeToolStripMenuItem.Text = "InTime & OutTime Only";
            this.withoutInTimeToolStripMenuItem.Click += new System.EventHandler(this.withoutInTimeToolStripMenuItem_Click);
            // 
            // withoutInTimeOutTimeToolStripMenuItem
            // 
            this.withoutInTimeOutTimeToolStripMenuItem.Name = "withoutInTimeOutTimeToolStripMenuItem";
            this.withoutInTimeOutTimeToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.withoutInTimeOutTimeToolStripMenuItem.Text = "Without InTime & OutTime";
            this.withoutInTimeOutTimeToolStripMenuItem.Click += new System.EventHandler(this.withoutInTimeOutTimeToolStripMenuItem_Click);
            // 
            // hostelToolStripMenuItem
            // 
            this.hostelToolStripMenuItem.Name = "hostelToolStripMenuItem";
            this.hostelToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.hostelToolStripMenuItem.Text = "Hostel";
            this.hostelToolStripMenuItem.Click += new System.EventHandler(this.hostelToolStripMenuItem_Click);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.comboBox1);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.checkdatabase);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.lblcount);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.combohostel);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.comboidcardsearch);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.todate);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.comboformate);
            this.panel2.Controls.Add(this.butView);
            this.panel2.Controls.Add(this.frmdate);
            this.panel2.Controls.Add(this.combocompcode);
            this.panel2.Controls.Add(this.txtsearch);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Location = new System.Drawing.Point(6, 40);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1273, 69);
            this.panel2.TabIndex = 47;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(1040, 31);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(212, 18);
            this.label8.TabIndex = 107;
            this.label8.Text = "Database Changed on 20-April-2023";
            // 
            // checkdatabase
            // 
            this.checkdatabase.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.checkdatabase.AutoSize = true;
            this.checkdatabase.Checked = true;
            this.checkdatabase.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkdatabase.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.checkdatabase.Location = new System.Drawing.Point(1043, 6);
            this.checkdatabase.Name = "checkdatabase";
            this.checkdatabase.Size = new System.Drawing.Size(82, 22);
            this.checkdatabase.TabIndex = 106;
            this.checkdatabase.Text = "PAYROLL";
            this.checkdatabase.UseVisualStyleBackColor = true;
            this.checkdatabase.CheckedChanged += new System.EventHandler(this.checkdatabase_CheckedChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.SystemColors.InfoText;
            this.label7.Location = new System.Drawing.Point(807, 7);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(91, 14);
            this.label7.TabIndex = 105;
            this.label7.Text = "DownLoadType";
            // 
            // lblcount
            // 
            this.lblcount.AutoSize = true;
            this.lblcount.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblcount.ForeColor = System.Drawing.Color.Maroon;
            this.lblcount.Location = new System.Drawing.Point(807, 48);
            this.lblcount.Name = "lblcount";
            this.lblcount.Size = new System.Drawing.Size(58, 16);
            this.lblcount.TabIndex = 104;
            this.lblcount.Text = "Division";
            this.lblcount.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(412, 42);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(88, 16);
            this.label6.TabIndex = 103;
            this.label6.Text = "Hostel Name";
            // 
            // combohostel
            // 
            this.combohostel.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.combohostel.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.combohostel.FormattingEnabled = true;
            this.combohostel.Items.AddRange(new object[] {
            "",
            "",
            "----"});
            this.combohostel.Location = new System.Drawing.Point(506, 36);
            this.combohostel.Name = "combohostel";
            this.combohostel.Size = new System.Drawing.Size(174, 26);
            this.combohostel.TabIndex = 102;
            this.combohostel.SelectedIndexChanged += new System.EventHandler(this.combohostel_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.InfoText;
            this.label5.Location = new System.Drawing.Point(12, 42);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 14);
            this.label5.TabIndex = 101;
            this.label5.Text = "Division";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.InfoText;
            this.label3.Location = new System.Drawing.Point(612, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 14);
            this.label3.TabIndex = 100;
            this.label3.Text = "IDCard ";
            // 
            // comboidcardsearch
            // 
            this.comboidcardsearch.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comboidcardsearch.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboidcardsearch.ContextMenuStrip = this.contextMenuStrip1;
            this.comboidcardsearch.DropDownHeight = 100;
            this.comboidcardsearch.FormattingEnabled = true;
            this.comboidcardsearch.IntegralHeight = false;
            this.comboidcardsearch.Location = new System.Drawing.Point(686, 5);
            this.comboidcardsearch.Name = "comboidcardsearch";
            this.comboidcardsearch.Size = new System.Drawing.Size(115, 26);
            this.comboidcardsearch.TabIndex = 99;
            this.comboidcardsearch.SelectedIndexChanged += new System.EventHandler(this.comboidcardsearch_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.InfoText;
            this.label2.Location = new System.Drawing.Point(464, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 14);
            this.label2.TabIndex = 98;
            this.label2.Text = "To";
            // 
            // todate
            // 
            this.todate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.todate.Location = new System.Drawing.Point(490, 7);
            this.todate.Name = "todate";
            this.todate.Size = new System.Drawing.Size(116, 25);
            this.todate.TabIndex = 97;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.InfoText;
            this.label4.Location = new System.Drawing.Point(290, 11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(36, 14);
            this.label4.TabIndex = 96;
            this.label4.Text = "From";
            // 
            // comboformate
            // 
            this.comboformate.FormattingEnabled = true;
            this.comboformate.Items.AddRange(new object[] {
            "Excel",
            "PDF",
            "CSV",
            "Word"});
            this.comboformate.Location = new System.Drawing.Point(904, 5);
            this.comboformate.Name = "comboformate";
            this.comboformate.Size = new System.Drawing.Size(122, 26);
            this.comboformate.TabIndex = 93;
            this.comboformate.SelectedIndexChanged += new System.EventHandler(this.comboformate_SelectedIndexChanged_1);
            // 
            // butView
            // 
            this.butView.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.butView.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.butView.Location = new System.Drawing.Point(686, 34);
            this.butView.Name = "butView";
            this.butView.Size = new System.Drawing.Size(115, 30);
            this.butView.TabIndex = 5;
            this.butView.Text = "View";
            this.butView.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.butView.UseVisualStyleBackColor = false;
            this.butView.Click += new System.EventHandler(this.butView_Click);
            // 
            // frmdate
            // 
            this.frmdate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.frmdate.Location = new System.Drawing.Point(332, 5);
            this.frmdate.Name = "frmdate";
            this.frmdate.Size = new System.Drawing.Size(126, 25);
            this.frmdate.TabIndex = 4;
            // 
            // combocompcode
            // 
            this.combocompcode.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.combocompcode.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.combocompcode.DropDownHeight = 90;
            this.combocompcode.FormattingEnabled = true;
            this.combocompcode.IntegralHeight = false;
            this.combocompcode.Location = new System.Drawing.Point(85, 36);
            this.combocompcode.Name = "combocompcode";
            this.combocompcode.Size = new System.Drawing.Size(95, 26);
            this.combocompcode.TabIndex = 3;
            this.combocompcode.SelectedIndexChanged += new System.EventHandler(this.combocompcode_SelectedIndexChanged);
            // 
            // txtsearch
            // 
            this.txtsearch.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtsearch.Location = new System.Drawing.Point(85, 6);
            this.txtsearch.Name = "txtsearch";
            this.txtsearch.Size = new System.Drawing.Size(199, 25);
            this.txtsearch.TabIndex = 1;
            this.txtsearch.TextChanged += new System.EventHandler(this.txtsearch_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.label1.Location = new System.Drawing.Point(12, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "Search";
            // 
            // printDialog1
            // 
            this.printDialog1.UseEXDialog = true;
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
            this.butheader.Size = new System.Drawing.Size(1276, 32);
            this.butheader.TabIndex = 463;
            this.butheader.Text = "MOVEMENT REPORT";
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
            this.butfooter.Location = new System.Drawing.Point(1, 492);
            this.butfooter.Margin = new System.Windows.Forms.Padding(0);
            this.butfooter.Name = "butfooter";
            this.butfooter.Size = new System.Drawing.Size(1282, 10);
            this.butfooter.TabIndex = 464;
            this.butfooter.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.butfooter.UseVisualStyleBackColor = false;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.crystalReportViewer1);
            this.panel1.Controls.Add(this.butheader);
            this.panel1.Controls.Add(this.lbl_Header);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Location = new System.Drawing.Point(1, 1);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3);
            this.panel1.Size = new System.Drawing.Size(1282, 499);
            this.panel1.TabIndex = 465;
            // 
            // crystalReportViewer1
            // 
            this.crystalReportViewer1.ActiveViewIndex = -1;
            this.crystalReportViewer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.crystalReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crystalReportViewer1.ContextMenuStrip = this.contextMenuStrip2;
            this.crystalReportViewer1.Cursor = System.Windows.Forms.Cursors.Default;
            this.crystalReportViewer1.Location = new System.Drawing.Point(6, 109);
            this.crystalReportViewer1.Margin = new System.Windows.Forms.Padding(0);
            this.crystalReportViewer1.Name = "crystalReportViewer1";
            this.crystalReportViewer1.Size = new System.Drawing.Size(1273, 382);
            this.crystalReportViewer1.TabIndex = 48;
            this.crystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            // 
            // comboBox1
            // 
            this.comboBox1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comboBox1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBox1.DropDownHeight = 90;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.IntegralHeight = false;
            this.comboBox1.Location = new System.Drawing.Point(260, 36);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(146, 26);
            this.comboBox1.TabIndex = 108;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.SystemColors.InfoText;
            this.label9.Location = new System.Drawing.Point(202, 42);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(52, 14);
            this.label9.TabIndex = 109;
            this.label9.Text = "PerType";
            // 
            // MovementReport
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Teal;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(1284, 503);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.butfooter);
            this.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MovementReport";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.Text = "MovementReport";
            this.Load += new System.EventHandler(this.MovementReport_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.contextMenuStrip2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lbl_Header;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DateTimePicker todate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboformate;
        private System.Windows.Forms.Button butView;
        private System.Windows.Forms.DateTimePicker frmdate;
        private System.Windows.Forms.ComboBox combocompcode;
        private System.Windows.Forms.TextBox txtsearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem rToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem outTimeToolStripMenuItem;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboidcardsearch;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox combohostel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ToolStripMenuItem withoutInTimeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem withoutInTimeOutTimeToolStripMenuItem;
        private System.Windows.Forms.Label lblcount;
        private System.Windows.Forms.ToolStripMenuItem hostelToolStripMenuItem;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button butheader;
        private System.Windows.Forms.Button butfooter;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox checkdatabase;
        private System.Windows.Forms.Label label8;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}