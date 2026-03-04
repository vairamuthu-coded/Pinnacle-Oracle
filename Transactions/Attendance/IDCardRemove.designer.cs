
namespace Pinnacle.Transactions.Attendance
{
    partial class IDCardRemove
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.listremovechecklistip = new System.Windows.Forms.ListView();
            this.listremovecheck = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listremoveip = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listremovecon = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.lblprogress1 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.butconnect = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.butheader = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.listremovechecklistip);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1175, 450);
            this.panel1.TabIndex = 0;
            // 
            // listremovechecklistip
            // 
            this.listremovechecklistip.Activation = System.Windows.Forms.ItemActivation.TwoClick;
            this.listremovechecklistip.Alignment = System.Windows.Forms.ListViewAlignment.SnapToGrid;
            this.listremovechecklistip.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listremovechecklistip.BackColor = System.Drawing.Color.White;
            this.listremovechecklistip.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listremovechecklistip.CheckBoxes = true;
            this.listremovechecklistip.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.listremovecheck,
            this.listremoveip,
            this.listremovecon});
            this.listremovechecklistip.ForeColor = System.Drawing.Color.Black;
            this.listremovechecklistip.FullRowSelect = true;
            this.listremovechecklistip.GridLines = true;
            this.listremovechecklistip.HideSelection = false;
            this.listremovechecklistip.HoverSelection = true;
            this.listremovechecklistip.LabelEdit = true;
            this.listremovechecklistip.Location = new System.Drawing.Point(9, 29);
            this.listremovechecklistip.Margin = new System.Windows.Forms.Padding(0);
            this.listremovechecklistip.Name = "listremovechecklistip";
            this.listremovechecklistip.ShowItemToolTips = true;
            this.listremovechecklistip.Size = new System.Drawing.Size(259, 363);
            this.listremovechecklistip.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listremovechecklistip.TabIndex = 51;
            this.listremovechecklistip.UseCompatibleStateImageBehavior = false;
            this.listremovechecklistip.View = System.Windows.Forms.View.Details;
            this.listremovechecklistip.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.listremovechecklistip_ItemChecked);
            // 
            // listremovecheck
            // 
            this.listremovecheck.Text = "All";
            this.listremovecheck.Width = 0;
            // 
            // listremoveip
            // 
            this.listremoveip.Text = "IP Address";
            this.listremoveip.Width = 125;
            // 
            // listremovecon
            // 
            this.listremovecon.Text = "Con";
            this.listremovecon.Width = 117;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.lblprogress1);
            this.panel2.Controls.Add(this.progressBar1);
            this.panel2.Controls.Add(this.butconnect);
            this.panel2.ForeColor = System.Drawing.Color.Black;
            this.panel2.Location = new System.Drawing.Point(3, 395);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1160, 55);
            this.panel2.TabIndex = 50;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(325, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(606, 31);
            this.label1.TabIndex = 74;
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblprogress1
            // 
            this.lblprogress1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblprogress1.AutoSize = true;
            this.lblprogress1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblprogress1.ForeColor = System.Drawing.Color.Black;
            this.lblprogress1.Location = new System.Drawing.Point(9, 30);
            this.lblprogress1.Name = "lblprogress1";
            this.lblprogress1.Size = new System.Drawing.Size(78, 14);
            this.lblprogress1.TabIndex = 73;
            this.lblprogress1.Text = "lblprogress1";
            this.lblprogress1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.progressBar1.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.progressBar1.Location = new System.Drawing.Point(3, 6);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(305, 20);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar1.TabIndex = 72;
            // 
            // butconnect
            // 
            this.butconnect.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.butconnect.Location = new System.Drawing.Point(951, 6);
            this.butconnect.Name = "butconnect";
            this.butconnect.Size = new System.Drawing.Size(184, 33);
            this.butconnect.TabIndex = 0;
            this.butconnect.Text = "Delete From Machine";
            this.butconnect.UseVisualStyleBackColor = true;
            this.butconnect.Click += new System.EventHandler(this.butconnect_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Enabled = false;
            this.dataGridView1.Location = new System.Drawing.Point(271, 29);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(892, 363);
            this.dataGridView1.TabIndex = 49;
            // 
            // butheader
            // 
            this.butheader.BackColor = System.Drawing.Color.Teal;
            this.butheader.Dock = System.Windows.Forms.DockStyle.Top;
            this.butheader.FlatAppearance.BorderSize = 0;
            this.butheader.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.butheader.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.butheader.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butheader.ForeColor = System.Drawing.Color.White;
            this.butheader.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.butheader.Location = new System.Drawing.Point(0, 0);
            this.butheader.Margin = new System.Windows.Forms.Padding(0);
            this.butheader.Name = "butheader";
            this.butheader.Size = new System.Drawing.Size(1175, 26);
            this.butheader.TabIndex = 455;
            this.butheader.Text = "IDCARD REMOVE FROM BIOMETRIC MACHINE";
            this.butheader.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.butheader.UseVisualStyleBackColor = false;
            // 
            // IDCardRemove
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1175, 450);
            this.ControlBox = false;
            this.Controls.Add(this.butheader);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "IDCardRemove";
            this.Text = "IDCardRemove";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button butheader;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button butconnect;
        private System.Windows.Forms.ListView listremovechecklistip;
        private System.Windows.Forms.ColumnHeader listremovecheck;
        private System.Windows.Forms.ColumnHeader listremoveip;
        private System.Windows.Forms.ColumnHeader listremovecon;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label lblprogress1;
        private System.Windows.Forms.Label label1;
    }
}