
namespace Pinnacle.ReportFormate
{
    partial class OutPassLiveReport
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
            this.crystalReportViewer1 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.refreshToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.butheader = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.combocompcode = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.todate = new System.Windows.Forms.DateTimePicker();
            this.comboformate = new System.Windows.Forms.ComboBox();
            this.butView = new System.Windows.Forms.Button();
            this.frmdate = new System.Windows.Forms.DateTimePicker();
            this.combohostel = new System.Windows.Forms.ComboBox();
            this.txtsearch = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.contextMenuStrip2.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
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
            this.crystalReportViewer1.Location = new System.Drawing.Point(6, 113);
            this.crystalReportViewer1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.crystalReportViewer1.Name = "crystalReportViewer1";
            this.crystalReportViewer1.Size = new System.Drawing.Size(1251, 390);
            this.crystalReportViewer1.TabIndex = 0;
            this.crystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshToolStripMenuItem1});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(167, 26);
            // 
            // refreshToolStripMenuItem1
            // 
            this.refreshToolStripMenuItem1.Name = "refreshToolStripMenuItem1";
            this.refreshToolStripMenuItem1.Size = new System.Drawing.Size(166, 22);
            this.refreshToolStripMenuItem1.Text = "Without OutTime";
            this.refreshToolStripMenuItem1.Click += new System.EventHandler(this.refreshToolStripMenuItem1_Click);
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
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.butheader);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.crystalReportViewer1);
            this.panel1.Location = new System.Drawing.Point(2, 3);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3);
            this.panel1.Size = new System.Drawing.Size(1263, 510);
            this.panel1.TabIndex = 44;
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
            this.butheader.Size = new System.Drawing.Size(1257, 32);
            this.butheader.TabIndex = 464;
            this.butheader.Text = "OutPassLiveReport";
            this.butheader.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.butheader.UseVisualStyleBackColor = false;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.combocompcode);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.todate);
            this.panel2.Controls.Add(this.comboformate);
            this.panel2.Controls.Add(this.butView);
            this.panel2.Controls.Add(this.frmdate);
            this.panel2.Controls.Add(this.combohostel);
            this.panel2.Controls.Add(this.txtsearch);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Location = new System.Drawing.Point(3, 39);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1259, 70);
            this.panel2.TabIndex = 1;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.InfoText;
            this.label5.Location = new System.Drawing.Point(21, 44);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 14);
            this.label5.TabIndex = 103;
            this.label5.Text = "Division";
            // 
            // combocompcode
            // 
            this.combocompcode.FormattingEnabled = true;
            this.combocompcode.Location = new System.Drawing.Point(90, 35);
            this.combocompcode.Name = "combocompcode";
            this.combocompcode.Size = new System.Drawing.Size(145, 26);
            this.combocompcode.TabIndex = 102;
            this.combocompcode.SelectedIndexChanged += new System.EventHandler(this.combocompcode_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(285, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 16);
            this.label2.TabIndex = 97;
            this.label2.Text = "Hostel Name";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(544, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 16);
            this.label4.TabIndex = 96;
            this.label4.Text = "As On Date";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1098, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(22, 18);
            this.label3.TabIndex = 95;
            this.label3.Text = "To";
            this.label3.Visible = false;
            // 
            // todate
            // 
            this.todate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.todate.Location = new System.Drawing.Point(1125, 9);
            this.todate.Name = "todate";
            this.todate.Size = new System.Drawing.Size(121, 25);
            this.todate.TabIndex = 94;
            this.todate.Visible = false;
            // 
            // comboformate
            // 
            this.comboformate.FormattingEnabled = true;
            this.comboformate.Items.AddRange(new object[] {
            "Excel",
            "PDF",
            "CSV",
            "Word"});
            this.comboformate.Location = new System.Drawing.Point(923, 4);
            this.comboformate.Name = "comboformate";
            this.comboformate.Size = new System.Drawing.Size(122, 26);
            this.comboformate.TabIndex = 93;
            this.comboformate.SelectedIndexChanged += new System.EventHandler(this.comboformate_SelectedIndexChanged);
            // 
            // butView
            // 
            this.butView.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.butView.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.butView.Font = new System.Drawing.Font("Roboto Black", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butView.Location = new System.Drawing.Point(819, 0);
            this.butView.Name = "butView";
            this.butView.Size = new System.Drawing.Size(75, 30);
            this.butView.TabIndex = 5;
            this.butView.Text = "View";
            this.butView.UseVisualStyleBackColor = true;
            this.butView.Click += new System.EventHandler(this.butView_Click);
            // 
            // frmdate
            // 
            this.frmdate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.frmdate.Location = new System.Drawing.Point(652, 4);
            this.frmdate.Name = "frmdate";
            this.frmdate.Size = new System.Drawing.Size(121, 25);
            this.frmdate.TabIndex = 4;
            // 
            // combohostel
            // 
            this.combohostel.FormattingEnabled = true;
            this.combohostel.Items.AddRange(new object[] {
            "ALL",
            "PASS CANCEL"});
            this.combohostel.Location = new System.Drawing.Point(379, 35);
            this.combohostel.Name = "combohostel";
            this.combohostel.Size = new System.Drawing.Size(276, 26);
            this.combohostel.TabIndex = 3;
            this.combohostel.SelectedIndexChanged += new System.EventHandler(this.combocompcode_SelectedIndexChanged);
            // 
            // txtsearch
            // 
            this.txtsearch.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtsearch.Location = new System.Drawing.Point(90, 4);
            this.txtsearch.Name = "txtsearch";
            this.txtsearch.Size = new System.Drawing.Size(413, 25);
            this.txtsearch.TabIndex = 1;
            this.txtsearch.TextChanged += new System.EventHandler(this.txtsearch_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(18, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Search";
            // 
            // printDialog1
            // 
            this.printDialog1.UseEXDialog = true;
            // 
            // OutPassLiveReport
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.ClientSize = new System.Drawing.Size(1265, 517);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "OutPassLiveReport";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.Text = "OutPassLiveReport";
            this.Load += new System.EventHandler(this.OutPassLiveReport_Load);
            this.contextMenuStrip2.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DateTimePicker frmdate;
        private System.Windows.Forms.ComboBox combohostel;
        private System.Windows.Forms.TextBox txtsearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button butView;
        private System.Windows.Forms.ComboBox comboformate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker todate;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem1;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox combocompcode;
        private System.Windows.Forms.Button butheader;
    }
}