
namespace Pinnacle.ReportFormate
{
    partial class FuelAverageReport
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
            this.butfooter = new System.Windows.Forms.Button();
            this.butheader = new System.Windows.Forms.Button();
            this.crystalReportViewer1 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.todate = new System.Windows.Forms.DateTimePicker();
            this.comboformate = new System.Windows.Forms.ComboBox();
            this.butView = new System.Windows.Forms.Button();
            this.frmdate = new System.Windows.Forms.DateTimePicker();
            this.combocompcode = new System.Windows.Forms.ComboBox();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.checkdatabase = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.butfooter);
            this.panel1.Controls.Add(this.butheader);
            this.panel1.Controls.Add(this.crystalReportViewer1);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3);
            this.panel1.Size = new System.Drawing.Size(1265, 510);
            this.panel1.TabIndex = 98;
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
            this.butfooter.Location = new System.Drawing.Point(3, 497);
            this.butfooter.Margin = new System.Windows.Forms.Padding(0);
            this.butfooter.Name = "butfooter";
            this.butfooter.Size = new System.Drawing.Size(1259, 10);
            this.butfooter.TabIndex = 455;
            this.butfooter.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.butfooter.UseVisualStyleBackColor = false;
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
            this.butheader.Size = new System.Drawing.Size(1259, 30);
            this.butheader.TabIndex = 454;
            this.butheader.Text = "FUEL AVERAGE REPORT";
            this.butheader.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.butheader.UseVisualStyleBackColor = false;
            // 
            // crystalReportViewer1
            // 
            this.crystalReportViewer1.ActiveViewIndex = -1;
            this.crystalReportViewer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.crystalReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crystalReportViewer1.Cursor = System.Windows.Forms.Cursors.Default;
            this.crystalReportViewer1.Location = new System.Drawing.Point(6, 87);
            this.crystalReportViewer1.Name = "crystalReportViewer1";
            this.crystalReportViewer1.Size = new System.Drawing.Size(1253, 407);
            this.crystalReportViewer1.TabIndex = 40;
            this.crystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.checkdatabase);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.todate);
            this.panel2.Controls.Add(this.comboformate);
            this.panel2.Controls.Add(this.butView);
            this.panel2.Controls.Add(this.frmdate);
            this.panel2.Controls.Add(this.combocompcode);
            this.panel2.Location = new System.Drawing.Point(6, 39);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1265, 45);
            this.panel2.TabIndex = 39;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(519, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 16);
            this.label4.TabIndex = 96;
            this.label4.Text = "From";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(701, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(21, 16);
            this.label3.TabIndex = 95;
            this.label3.Text = "To";
            // 
            // todate
            // 
            this.todate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.todate.Location = new System.Drawing.Point(728, 9);
            this.todate.Name = "todate";
            this.todate.Size = new System.Drawing.Size(121, 22);
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
            this.comboformate.Location = new System.Drawing.Point(964, 9);
            this.comboformate.Name = "comboformate";
            this.comboformate.Size = new System.Drawing.Size(122, 24);
            this.comboformate.TabIndex = 93;
            // 
            // butView
            // 
            this.butView.Location = new System.Drawing.Point(868, 7);
            this.butView.Name = "butView";
            this.butView.Size = new System.Drawing.Size(75, 25);
            this.butView.TabIndex = 5;
            this.butView.Text = "View";
            this.butView.UseVisualStyleBackColor = true;
            this.butView.Click += new System.EventHandler(this.butView_Click);
            // 
            // frmdate
            // 
            this.frmdate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.frmdate.Location = new System.Drawing.Point(574, 10);
            this.frmdate.Name = "frmdate";
            this.frmdate.Size = new System.Drawing.Size(121, 22);
            this.frmdate.TabIndex = 4;
            this.frmdate.Value = new System.DateTime(2021, 6, 1, 19, 6, 0, 0);
            // 
            // combocompcode
            // 
            this.combocompcode.FormattingEnabled = true;
            this.combocompcode.Items.AddRange(new object[] {
            "",
            "TOKEN CANCEL"});
            this.combocompcode.Location = new System.Drawing.Point(333, 11);
            this.combocompcode.Name = "combocompcode";
            this.combocompcode.Size = new System.Drawing.Size(180, 24);
            this.combocompcode.TabIndex = 3;
            this.combocompcode.SelectedIndexChanged += new System.EventHandler(this.combocompcode_SelectedIndexChanged);
            // 
            // printDialog1
            // 
            this.printDialog1.UseEXDialog = true;
            // 
            // checkdatabase
            // 
            this.checkdatabase.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.checkdatabase.AutoSize = true;
            this.checkdatabase.Checked = true;
            this.checkdatabase.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkdatabase.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.checkdatabase.Location = new System.Drawing.Point(1103, 10);
            this.checkdatabase.Name = "checkdatabase";
            this.checkdatabase.Size = new System.Drawing.Size(85, 20);
            this.checkdatabase.TabIndex = 107;
            this.checkdatabase.Text = "PAYROLL";
            this.checkdatabase.UseVisualStyleBackColor = true;
            this.checkdatabase.CheckedChanged += new System.EventHandler(this.checkdatabase_CheckedChanged);
            // 
            // FuelAverageReport
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.ClientSize = new System.Drawing.Size(1270, 515);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FuelAverageReport";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.Text = "FuelAverageReport";
            this.Load += new System.EventHandler(this.FuelAverageReport_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker todate;
        private System.Windows.Forms.ComboBox comboformate;
        private System.Windows.Forms.Button butView;
        private System.Windows.Forms.DateTimePicker frmdate;
        private System.Windows.Forms.ComboBox combocompcode;
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.Button butheader;
        private System.Windows.Forms.Button butfooter;
        private System.Windows.Forms.CheckBox checkdatabase;
    }
}