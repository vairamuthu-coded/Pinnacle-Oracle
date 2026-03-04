namespace Pinnacle.Canteen
{
    partial class ItemMaster
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
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.label10 = new System.Windows.Forms.Label();
            this.checkactive = new System.Windows.Forms.CheckBox();
            this.txtspecialcost = new System.Windows.Forms.TextBox();
            this.txtempcost = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.pictureitem = new System.Windows.Forms.PictureBox();
            this.txtitemname = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtcontcost = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtitemcode = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtitemid = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lblcanitemtotal = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.butDatewise = new System.Windows.Forms.Button();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.txtitemsearch = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.listcanitem = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureitem)).BeginInit();
            this.tabControl2.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel1.Controls.Add(this.tabControl1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3);
            this.panel1.Size = new System.Drawing.Size(1234, 495);
            this.panel1.TabIndex = 35;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(3, 3);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(1);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1228, 489);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.White;
            this.tabPage1.Controls.Add(this.dateTimePicker2);
            this.tabPage1.Controls.Add(this.label10);
            this.tabPage1.Controls.Add(this.checkactive);
            this.tabPage1.Controls.Add(this.txtspecialcost);
            this.tabPage1.Controls.Add(this.txtempcost);
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Controls.Add(this.pictureitem);
            this.tabPage1.Controls.Add(this.txtitemname);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.txtcontcost);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.txtitemcode);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.txtitemid);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.tabControl2);
            this.tabPage1.ForeColor = System.Drawing.Color.Black;
            this.tabPage1.Location = new System.Drawing.Point(4, 28);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1220, 457);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Item Master";
            this.tabPage1.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.CustomFormat = "dd-MM-yyyy";
            this.dateTimePicker2.Enabled = false;
            this.dateTimePicker2.Font = new System.Drawing.Font("Roboto", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker2.Location = new System.Drawing.Point(413, 52);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(128, 33);
            this.dateTimePicker2.TabIndex = 24;
            this.dateTimePicker2.Visible = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Roboto", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(24, 189);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(128, 25);
            this.label10.TabIndex = 23;
            this.label10.Text = "Special Cost";
            // 
            // checkactive
            // 
            this.checkactive.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkactive.AutoSize = true;
            this.checkactive.Font = new System.Drawing.Font("Roboto", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkactive.Location = new System.Drawing.Point(205, 249);
            this.checkactive.Name = "checkactive";
            this.checkactive.Size = new System.Drawing.Size(89, 29);
            this.checkactive.TabIndex = 5;
            this.checkactive.Text = "Active";
            this.checkactive.UseVisualStyleBackColor = true;
            this.checkactive.CheckedChanged += new System.EventHandler(this.Checkactive_CheckedChanged);
            // 
            // txtspecialcost
            // 
            this.txtspecialcost.Font = new System.Drawing.Font("Roboto Black", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtspecialcost.Location = new System.Drawing.Point(205, 189);
            this.txtspecialcost.MaxLength = 3;
            this.txtspecialcost.Name = "txtspecialcost";
            this.txtspecialcost.Size = new System.Drawing.Size(99, 54);
            this.txtspecialcost.TabIndex = 4;
            this.txtspecialcost.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtspecialcost_KeyPress);
            // 
            // txtempcost
            // 
            this.txtempcost.Font = new System.Drawing.Font("Roboto Black", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtempcost.Location = new System.Drawing.Point(205, 133);
            this.txtempcost.MaxLength = 3;
            this.txtempcost.Name = "txtempcost";
            this.txtempcost.Size = new System.Drawing.Size(99, 50);
            this.txtempcost.TabIndex = 3;
            this.txtempcost.TextChanged += new System.EventHandler(this.txtempcost_TextChanged);
            this.txtempcost.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtempcost_KeyPress);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Roboto", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(24, 148);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(152, 25);
            this.label9.TabIndex = 21;
            this.label9.Text = "Employee Cost";
            // 
            // pictureitem
            // 
            this.pictureitem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureitem.Location = new System.Drawing.Point(310, 133);
            this.pictureitem.Name = "pictureitem";
            this.pictureitem.Size = new System.Drawing.Size(231, 169);
            this.pictureitem.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureitem.TabIndex = 17;
            this.pictureitem.TabStop = false;
            this.pictureitem.Click += new System.EventHandler(this.Pictureitem_Click);
            // 
            // txtitemname
            // 
            this.txtitemname.Font = new System.Drawing.Font("Roboto", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtitemname.Location = new System.Drawing.Point(205, 92);
            this.txtitemname.MaxLength = 20;
            this.txtitemname.Name = "txtitemname";
            this.txtitemname.Size = new System.Drawing.Size(336, 33);
            this.txtitemname.TabIndex = 2;
            this.txtitemname.TextChanged += new System.EventHandler(this.Txtitemname_TextChanged);
            this.txtitemname.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtitemname_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Roboto", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(24, 282);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(161, 25);
            this.label6.TabIndex = 14;
            this.label6.Text = "Contractor Cost";
            this.label6.Visible = false;
            this.label6.Click += new System.EventHandler(this.Label6_Click);
            // 
            // txtcontcost
            // 
            this.txtcontcost.Font = new System.Drawing.Font("Roboto Black", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtcontcost.Location = new System.Drawing.Point(205, 282);
            this.txtcontcost.MaxLength = 3;
            this.txtcontcost.Name = "txtcontcost";
            this.txtcontcost.ReadOnly = true;
            this.txtcontcost.Size = new System.Drawing.Size(99, 54);
            this.txtcontcost.TabIndex = 2;
            this.txtcontcost.Text = "0";
            this.txtcontcost.Visible = false;
            this.txtcontcost.TextChanged += new System.EventHandler(this.Txtitemcost_TextChanged);
            this.txtcontcost.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtcontcost_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Roboto", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(23, 92);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(109, 25);
            this.label4.TabIndex = 10;
            this.label4.Text = "ItemName";
            this.label4.Click += new System.EventHandler(this.Label4_Click);
            // 
            // txtitemcode
            // 
            this.txtitemcode.Enabled = false;
            this.txtitemcode.Font = new System.Drawing.Font("Roboto", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtitemcode.Location = new System.Drawing.Point(205, 50);
            this.txtitemcode.Name = "txtitemcode";
            this.txtitemcode.Size = new System.Drawing.Size(336, 33);
            this.txtitemcode.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Roboto", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(24, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 25);
            this.label3.TabIndex = 7;
            this.label3.Text = "Item Code";
            // 
            // txtitemid
            // 
            this.txtitemid.Enabled = false;
            this.txtitemid.Font = new System.Drawing.Font("Roboto", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtitemid.Location = new System.Drawing.Point(205, 9);
            this.txtitemid.Name = "txtitemid";
            this.txtitemid.Size = new System.Drawing.Size(336, 33);
            this.txtitemid.TabIndex = 0;
            this.txtitemid.TextChanged += new System.EventHandler(this.Txtitemid_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Roboto", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(24, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 25);
            this.label2.TabIndex = 5;
            this.label2.Text = "ID";
            this.label2.Click += new System.EventHandler(this.Label2_Click);
            // 
            // tabControl2
            // 
            this.tabControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl2.Controls.Add(this.tabPage2);
            this.tabControl2.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl2.Location = new System.Drawing.Point(558, 9);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(656, 442);
            this.tabControl2.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.lblcanitemtotal);
            this.tabPage2.Controls.Add(this.panel3);
            this.tabPage2.Controls.Add(this.panel2);
            this.tabPage2.Controls.Add(this.listcanitem);
            this.tabPage2.Font = new System.Drawing.Font("Palatino Linotype", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPage2.Location = new System.Drawing.Point(4, 28);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(648, 410);
            this.tabPage2.TabIndex = 0;
            this.tabPage2.Text = "Item Master Details";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // lblcanitemtotal
            // 
            this.lblcanitemtotal.AutoSize = true;
            this.lblcanitemtotal.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblcanitemtotal.Font = new System.Drawing.Font("Palatino Linotype", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblcanitemtotal.Location = new System.Drawing.Point(3, 386);
            this.lblcanitemtotal.Name = "lblcanitemtotal";
            this.lblcanitemtotal.Size = new System.Drawing.Size(33, 16);
            this.lblcanitemtotal.TabIndex = 21;
            this.lblcanitemtotal.Text = "Total";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(3, 402);
            this.panel3.Margin = new System.Windows.Forms.Padding(0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(642, 5);
            this.panel3.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.panel2.Controls.Add(this.butDatewise);
            this.panel2.Controls.Add(this.dateTimePicker1);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.txtitemsearch);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Font = new System.Drawing.Font("Roboto", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(642, 41);
            this.panel2.TabIndex = 1;
            // 
            // butDatewise
            // 
            this.butDatewise.Location = new System.Drawing.Point(501, 2);
            this.butDatewise.Name = "butDatewise";
            this.butDatewise.Size = new System.Drawing.Size(104, 36);
            this.butDatewise.TabIndex = 13;
            this.butDatewise.Text = "Submit";
            this.butDatewise.UseVisualStyleBackColor = true;
            this.butDatewise.Visible = false;
            this.butDatewise.Click += new System.EventHandler(this.butDatewise_Click);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(348, 3);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(147, 33);
            this.dateTimePicker1.TabIndex = 12;
            this.dateTimePicker1.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Roboto", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(290, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 23);
            this.label1.TabIndex = 11;
            this.label1.Text = "Date";
            this.label1.Visible = false;
            // 
            // txtitemsearch
            // 
            this.txtitemsearch.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtitemsearch.Font = new System.Drawing.Font("Roboto", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtitemsearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtitemsearch.Location = new System.Drawing.Point(86, 3);
            this.txtitemsearch.Name = "txtitemsearch";
            this.txtitemsearch.Size = new System.Drawing.Size(184, 30);
            this.txtitemsearch.TabIndex = 5;
            this.txtitemsearch.TextChanged += new System.EventHandler(this.Txtitemsearch_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Roboto", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(8, 6);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(67, 23);
            this.label8.TabIndex = 7;
            this.label8.Text = "Search";
            // 
            // listcanitem
            // 
            this.listcanitem.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.listcanitem.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listcanitem.BackColor = System.Drawing.Color.White;
            this.listcanitem.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listcanitem.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader7,
            this.columnHeader6});
            this.listcanitem.ContextMenuStrip = this.contextMenuStrip1;
            this.listcanitem.Font = new System.Drawing.Font("Roboto", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listcanitem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.listcanitem.FullRowSelect = true;
            this.listcanitem.GridLines = true;
            this.listcanitem.HideSelection = false;
            this.listcanitem.Location = new System.Drawing.Point(6, 53);
            this.listcanitem.Name = "listcanitem";
            this.listcanitem.Size = new System.Drawing.Size(636, 324);
            this.listcanitem.TabIndex = 0;
            this.listcanitem.UseCompatibleStateImageBehavior = false;
            this.listcanitem.View = System.Windows.Forms.View.Details;
            this.listcanitem.ItemActivate += new System.EventHandler(this.listcanitem_ItemActivate);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "SNo";
            this.columnHeader1.Width = 50;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "ID";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Item Code";
            this.columnHeader3.Width = 120;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Item Name";
            this.columnHeader4.Width = 190;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "EmpType";
            this.columnHeader7.Width = 0;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Cost";
            this.columnHeader6.Width = 50;
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
            // ItemMaster
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1240, 501);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ItemMaster";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.Text = "Item Master";
            this.Load += new System.EventHandler(this.ItemMaster_Load);
            this.panel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureitem)).EndInit();
            this.tabControl2.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ListView listcanitem;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pictureitem;
        private System.Windows.Forms.TextBox txtitemname;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtcontcost;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox checkactive;
        private System.Windows.Forms.TextBox txtitemcode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtitemid;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtitemsearch;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblcanitemtotal;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.TextBox txtempcost;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtspecialcost;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Button butDatewise;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
    }
}