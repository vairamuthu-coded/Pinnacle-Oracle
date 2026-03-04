
namespace Pinnacle.Registration
{
    partial class GenerateMaster
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.butGenerate = new System.Windows.Forms.Button();
            this.txtproductkeys = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtexperiencedays = new System.Windows.Forms.TextBox();
            this.combolicensetype = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.checkactive = new System.Windows.Forms.CheckBox();
            this.txtproductid = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtgenerateid = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Location = new System.Drawing.Point(1, 1);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1290, 509);
            this.panel1.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(5, 4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1284, 506);
            this.tabControl1.TabIndex = 60;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.White;
            this.tabPage1.Controls.Add(this.dateTimePicker2);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.dateTimePicker1);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.butGenerate);
            this.tabPage1.Controls.Add(this.txtproductkeys);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.txtexperiencedays);
            this.tabPage1.Controls.Add(this.combolicensetype);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.checkactive);
            this.tabPage1.Controls.Add(this.txtproductid);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.txtgenerateid);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Location = new System.Drawing.Point(4, 27);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.tabPage1.Size = new System.Drawing.Size(1276, 475);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Generate Master";
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker2.Location = new System.Drawing.Point(376, 51);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(146, 25);
            this.dateTimePicker2.TabIndex = 28;
            this.dateTimePicker2.ValueChanged += new System.EventHandler(this.dateTimePicker2_ValueChanged);
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(280, 51);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(90, 18);
            this.label7.TabIndex = 27;
            this.label7.Text = "To Date";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(127, 51);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(147, 25);
            this.dateTimePicker1.TabIndex = 26;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(17, 56);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(104, 18);
            this.label6.TabIndex = 25;
            this.label6.Text = "From Date";
            // 
            // butGenerate
            // 
            this.butGenerate.Location = new System.Drawing.Point(164, 227);
            this.butGenerate.Name = "butGenerate";
            this.butGenerate.Size = new System.Drawing.Size(188, 38);
            this.butGenerate.TabIndex = 23;
            this.butGenerate.Text = "Generate";
            this.butGenerate.UseVisualStyleBackColor = true;
            this.butGenerate.Click += new System.EventHandler(this.butGenerate_Click);
            // 
            // txtproductkeys
            // 
            this.txtproductkeys.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtproductkeys.Location = new System.Drawing.Point(127, 165);
            this.txtproductkeys.MaxLength = 250;
            this.txtproductkeys.Name = "txtproductkeys";
            this.txtproductkeys.Size = new System.Drawing.Size(588, 25);
            this.txtproductkeys.TabIndex = 22;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(17, 172);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 18);
            this.label1.TabIndex = 21;
            this.label1.Text = "Product Keys";
            // 
            // txtexperiencedays
            // 
            this.txtexperiencedays.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtexperiencedays.Location = new System.Drawing.Point(127, 138);
            this.txtexperiencedays.MaxLength = 250;
            this.txtexperiencedays.Name = "txtexperiencedays";
            this.txtexperiencedays.Size = new System.Drawing.Size(588, 25);
            this.txtexperiencedays.TabIndex = 20;
            // 
            // combolicensetype
            // 
            this.combolicensetype.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.combolicensetype.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.combolicensetype.FormattingEnabled = true;
            this.combolicensetype.Items.AddRange(new object[] {
            "FULL",
            "TRIAL"});
            this.combolicensetype.Location = new System.Drawing.Point(127, 109);
            this.combolicensetype.Name = "combolicensetype";
            this.combolicensetype.Size = new System.Drawing.Size(588, 26);
            this.combolicensetype.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(17, 116);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(104, 18);
            this.label5.TabIndex = 19;
            this.label5.Text = "License Type";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(17, 145);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 18);
            this.label3.TabIndex = 17;
            this.label3.Text = "Experience Days";
            // 
            // checkactive
            // 
            this.checkactive.AutoSize = true;
            this.checkactive.Location = new System.Drawing.Point(127, 196);
            this.checkactive.Name = "checkactive";
            this.checkactive.Size = new System.Drawing.Size(66, 22);
            this.checkactive.TabIndex = 3;
            this.checkactive.Text = "Active";
            this.checkactive.UseVisualStyleBackColor = true;
            // 
            // txtproductid
            // 
            this.txtproductid.Location = new System.Drawing.Point(127, 82);
            this.txtproductid.MaxLength = 250;
            this.txtproductid.Name = "txtproductid";
            this.txtproductid.Size = new System.Drawing.Size(588, 25);
            this.txtproductid.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(17, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 18);
            this.label2.TabIndex = 14;
            this.label2.Text = "Product ID";
            // 
            // txtgenerateid
            // 
            this.txtgenerateid.Enabled = false;
            this.txtgenerateid.Location = new System.Drawing.Point(127, 22);
            this.txtgenerateid.Name = "txtgenerateid";
            this.txtgenerateid.Size = new System.Drawing.Size(588, 25);
            this.txtgenerateid.TabIndex = 13;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(17, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 18);
            this.label4.TabIndex = 12;
            this.label4.Text = "ID";
            // 
            // GenerateMaster
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.ClientSize = new System.Drawing.Size(1292, 514);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "GenerateMaster";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.Text = "GenerateMaster";
            this.Load += new System.EventHandler(this.GenerateMaster_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ComboBox combolicensetype;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox checkactive;
        private System.Windows.Forms.TextBox txtproductid;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtgenerateid;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtproductkeys;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button butGenerate;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtexperiencedays;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.Label label7;
    }
}