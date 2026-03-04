
namespace Pinnacle.Transactions
{
    partial class IDCardPrintDetails
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.listView2 = new System.Windows.Forms.ListView();
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader15 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader17 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader16 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader13 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader14 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.lbltotal2 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.butfront1 = new System.Windows.Forms.Button();
            this.butback2 = new System.Windows.Forms.Button();
            this.combocompcode = new System.Windows.Forms.ComboBox();
            this.checkword = new System.Windows.Forms.CheckBox();
            this.chkall = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtsearch1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.comboPayPeriod1 = new System.Windows.Forms.ComboBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.butback = new System.Windows.Forms.Button();
            this.butfront = new System.Windows.Forms.Button();
            this.crystalReportViewer2 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.crystalReportViewer1 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel5.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.tabControl1);
            this.panel1.Location = new System.Drawing.Point(4, 4);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1188, 481);
            this.panel1.TabIndex = 21;
            // 
            // tabControl1
            // 
            this.tabControl1.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1188, 481);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.AutoScroll = true;
            this.tabPage1.BackColor = System.Drawing.Color.White;
            this.tabPage1.Controls.Add(this.listView2);
            this.tabPage1.Controls.Add(this.flowLayoutPanel1);
            this.tabPage1.Controls.Add(this.panel6);
            this.tabPage1.Controls.Add(this.panel5);
            this.tabPage1.Location = new System.Drawing.Point(4, 28);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1180, 449);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "IDCard Print Formate";
            // 
            // listView2
            // 
            this.listView2.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.listView2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView2.BackColor = System.Drawing.Color.White;
            this.listView2.BackgroundImageTiled = true;
            this.listView2.CheckBoxes = true;
            this.listView2.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader10,
            this.columnHeader11,
            this.columnHeader12,
            this.columnHeader15,
            this.columnHeader17,
            this.columnHeader16,
            this.columnHeader13,
            this.columnHeader14,
            this.columnHeader1});
            this.listView2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listView2.ForeColor = System.Drawing.SystemColors.InfoText;
            this.listView2.FullRowSelect = true;
            this.listView2.GridLines = true;
            this.listView2.HideSelection = false;
            this.listView2.Location = new System.Drawing.Point(933, 46);
            this.listView2.Name = "listView2";
            this.listView2.RightToLeftLayout = true;
            this.listView2.Size = new System.Drawing.Size(241, 304);
            this.listView2.TabIndex = 16;
            this.listView2.UseCompatibleStateImageBehavior = false;
            this.listView2.View = System.Windows.Forms.View.Details;
            this.listView2.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.listView2_ItemChecked);
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "";
            this.columnHeader10.Width = 25;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "S.No";
            this.columnHeader11.Width = 43;
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "ID";
            this.columnHeader12.Width = 0;
            // 
            // columnHeader15
            // 
            this.columnHeader15.Text = "IDCard No";
            this.columnHeader15.Width = 0;
            // 
            // columnHeader17
            // 
            this.columnHeader17.Text = "MIDCardNo";
            // 
            // columnHeader16
            // 
            this.columnHeader16.Text = "Employee Name";
            this.columnHeader16.Width = 300;
            // 
            // columnHeader13
            // 
            this.columnHeader13.Text = "CompCode";
            this.columnHeader13.Width = 0;
            // 
            // columnHeader14
            // 
            this.columnHeader14.Text = "Month";
            this.columnHeader14.Width = 0;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Address";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.Snow;
            this.flowLayoutPanel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 46);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(924, 304);
            this.flowLayoutPanel1.TabIndex = 51;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.panel6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel6.Controls.Add(this.lbltotal2);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel6.Location = new System.Drawing.Point(3, 425);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(1174, 21);
            this.panel6.TabIndex = 18;
            // 
            // lbltotal2
            // 
            this.lbltotal2.AutoSize = true;
            this.lbltotal2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbltotal2.ForeColor = System.Drawing.Color.White;
            this.lbltotal2.Location = new System.Drawing.Point(0, 5);
            this.lbltotal2.Name = "lbltotal2";
            this.lbltotal2.Size = new System.Drawing.Size(35, 16);
            this.lbltotal2.TabIndex = 14;
            this.lbltotal2.Text = "Total";
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.Gainsboro;
            this.panel5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel5.Controls.Add(this.butfront1);
            this.panel5.Controls.Add(this.butback2);
            this.panel5.Controls.Add(this.combocompcode);
            this.panel5.Controls.Add(this.checkword);
            this.panel5.Controls.Add(this.chkall);
            this.panel5.Controls.Add(this.label5);
            this.panel5.Controls.Add(this.txtsearch1);
            this.panel5.Controls.Add(this.label3);
            this.panel5.Controls.Add(this.label4);
            this.panel5.Controls.Add(this.comboPayPeriod1);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.ForeColor = System.Drawing.Color.Black;
            this.panel5.Location = new System.Drawing.Point(3, 3);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1174, 37);
            this.panel5.TabIndex = 14;
            // 
            // butfront1
            // 
            this.butfront1.BackColor = System.Drawing.Color.Transparent;
            this.butfront1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.butfront1.FlatAppearance.BorderSize = 0;
            this.butfront1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.butfront1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.butfront1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butfront1.ForeColor = System.Drawing.Color.Black;
            this.butfront1.Location = new System.Drawing.Point(848, 2);
            this.butfront1.Name = "butfront1";
            this.butfront1.Size = new System.Drawing.Size(75, 29);
            this.butfront1.TabIndex = 13;
            this.butfront1.Text = "Front";
            this.butfront1.UseVisualStyleBackColor = false;
            this.butfront1.Click += new System.EventHandler(this.butfront1_Click);
            // 
            // butback2
            // 
            this.butback2.BackColor = System.Drawing.Color.Transparent;
            this.butback2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.butback2.FlatAppearance.BorderSize = 0;
            this.butback2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.butback2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.butback2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butback2.ForeColor = System.Drawing.Color.Black;
            this.butback2.Location = new System.Drawing.Point(942, 2);
            this.butback2.Name = "butback2";
            this.butback2.Size = new System.Drawing.Size(75, 29);
            this.butback2.TabIndex = 12;
            this.butback2.Text = "Back";
            this.butback2.UseVisualStyleBackColor = false;
            this.butback2.Click += new System.EventHandler(this.butback2_Click);
            // 
            // combocompcode
            // 
            this.combocompcode.FormattingEnabled = true;
            this.combocompcode.Location = new System.Drawing.Point(101, 5);
            this.combocompcode.Name = "combocompcode";
            this.combocompcode.Size = new System.Drawing.Size(121, 24);
            this.combocompcode.TabIndex = 11;
            this.combocompcode.SelectedIndexChanged += new System.EventHandler(this.combocompcode_SelectedIndexChanged);
            // 
            // checkword
            // 
            this.checkword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkword.AutoSize = true;
            this.checkword.BackColor = System.Drawing.Color.Transparent;
            this.checkword.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkword.ForeColor = System.Drawing.Color.Black;
            this.checkword.Location = new System.Drawing.Point(1037, 9);
            this.checkword.Name = "checkword";
            this.checkword.Size = new System.Drawing.Size(57, 19);
            this.checkword.TabIndex = 10;
            this.checkword.Text = "Word";
            this.checkword.UseVisualStyleBackColor = false;
            // 
            // chkall
            // 
            this.chkall.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkall.AutoSize = true;
            this.chkall.BackColor = System.Drawing.Color.Transparent;
            this.chkall.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkall.ForeColor = System.Drawing.Color.Black;
            this.chkall.Location = new System.Drawing.Point(1113, 8);
            this.chkall.Name = "chkall";
            this.chkall.Size = new System.Drawing.Size(48, 19);
            this.chkall.TabIndex = 9;
            this.chkall.Text = "ALL";
            this.chkall.UseVisualStyleBackColor = false;
            this.chkall.CheckedChanged += new System.EventHandler(this.chkall_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(18, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 16);
            this.label5.TabIndex = 7;
            this.label5.Text = "CompCode";
            // 
            // txtsearch1
            // 
            this.txtsearch1.Location = new System.Drawing.Point(683, 6);
            this.txtsearch1.Name = "txtsearch1";
            this.txtsearch1.Size = new System.Drawing.Size(143, 22);
            this.txtsearch1.TabIndex = 6;
            this.txtsearch1.TextChanged += new System.EventHandler(this.txtsearch1_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(323, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 16);
            this.label3.TabIndex = 5;
            this.label3.Text = "Month";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(598, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 16);
            this.label4.TabIndex = 4;
            this.label4.Text = "Search";
            // 
            // comboPayPeriod1
            // 
            this.comboPayPeriod1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comboPayPeriod1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboPayPeriod1.FormattingEnabled = true;
            this.comboPayPeriod1.Items.AddRange(new object[] {
            "-----------"});
            this.comboPayPeriod1.Location = new System.Drawing.Point(400, 5);
            this.comboPayPeriod1.Name = "comboPayPeriod1";
            this.comboPayPeriod1.Size = new System.Drawing.Size(166, 24);
            this.comboPayPeriod1.TabIndex = 1;
            this.comboPayPeriod1.SelectedIndexChanged += new System.EventHandler(this.comboPayPeriod1_SelectedIndexChanged);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.panel2);
            this.tabPage3.Controls.Add(this.crystalReportViewer2);
            this.tabPage3.Controls.Add(this.crystalReportViewer1);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(1180, 452);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "ID Card Report Formate";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.panel2.Controls.Add(this.butback);
            this.panel2.Controls.Add(this.butfront);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.ForeColor = System.Drawing.Color.White;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1174, 29);
            this.panel2.TabIndex = 15;
            // 
            // butback
            // 
            this.butback.ForeColor = System.Drawing.Color.Black;
            this.butback.Location = new System.Drawing.Point(550, 3);
            this.butback.Name = "butback";
            this.butback.Size = new System.Drawing.Size(75, 23);
            this.butback.TabIndex = 1;
            this.butback.Text = "Back";
            this.butback.UseVisualStyleBackColor = true;
            this.butback.Click += new System.EventHandler(this.butback_Click);
            // 
            // butfront
            // 
            this.butfront.ForeColor = System.Drawing.Color.Black;
            this.butfront.Location = new System.Drawing.Point(422, 3);
            this.butfront.Name = "butfront";
            this.butfront.Size = new System.Drawing.Size(75, 23);
            this.butfront.TabIndex = 0;
            this.butfront.Text = "Front";
            this.butfront.UseVisualStyleBackColor = true;
            this.butfront.Click += new System.EventHandler(this.butfront_Click);
            // 
            // crystalReportViewer2
            // 
            this.crystalReportViewer2.ActiveViewIndex = -1;
            this.crystalReportViewer2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.crystalReportViewer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crystalReportViewer2.Cursor = System.Windows.Forms.Cursors.Default;
            this.crystalReportViewer2.Location = new System.Drawing.Point(1156, 0);
            this.crystalReportViewer2.Name = "crystalReportViewer2";
            this.crystalReportViewer2.Size = new System.Drawing.Size(14, 322);
            this.crystalReportViewer2.TabIndex = 1;
            this.crystalReportViewer2.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            this.crystalReportViewer2.Visible = false;
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
            this.crystalReportViewer1.Location = new System.Drawing.Point(3, 38);
            this.crystalReportViewer1.Name = "crystalReportViewer1";
            this.crystalReportViewer1.Size = new System.Drawing.Size(1173, 387);
            this.crystalReportViewer1.TabIndex = 0;
            this.crystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            // 
            // printDialog1
            // 
            this.printDialog1.UseEXDialog = true;
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
            // IDCardPrintDetails
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1198, 491);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "IDCardPrintDetails";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.Text = "IDCardPrintDetails";
            this.Load += new System.EventHandler(this.IDCardPrintDetails_Load);
            this.panel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ListView listView2;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.ColumnHeader columnHeader12;
        private System.Windows.Forms.ColumnHeader columnHeader15;
        private System.Windows.Forms.ColumnHeader columnHeader17;
        private System.Windows.Forms.ColumnHeader columnHeader16;
        private System.Windows.Forms.ColumnHeader columnHeader13;
        private System.Windows.Forms.ColumnHeader columnHeader14;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label lbltotal2;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.ComboBox combocompcode;
        private System.Windows.Forms.CheckBox checkword;
        private System.Windows.Forms.CheckBox chkall;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtsearch1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboPayPeriod1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.TabPage tabPage3;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer1;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button butback;
        private System.Windows.Forms.Button butfront;
        private System.Windows.Forms.Button butfront1;
        private System.Windows.Forms.Button butback2;
    }
}