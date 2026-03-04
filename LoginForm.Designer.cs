namespace Pinnacle
{
    partial class LoginForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.btn_sumbit = new System.Windows.Forms.Button();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.refreshToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.btn_Exit = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txt_password = new System.Windows.Forms.TextBox();
            this.txtusername = new System.Windows.Forms.TextBox();
            this.buttblcreate = new System.Windows.Forms.Button();
            this.txtproductid = new System.Windows.Forms.TextBox();
            this.txtproductkey = new System.Windows.Forms.TextBox();
            this.txtexperiencedays = new System.Windows.Forms.TextBox();
            this.combo_compcode = new System.Windows.Forms.TextBox();
            this.ErrProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.combofinyear = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.contextMenuStrip2.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ErrProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_sumbit
            // 
            this.btn_sumbit.BackColor = System.Drawing.Color.Transparent;
            this.btn_sumbit.ContextMenuStrip = this.contextMenuStrip2;
            this.btn_sumbit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btn_sumbit.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_sumbit.ForeColor = System.Drawing.Color.Navy;
            this.btn_sumbit.Location = new System.Drawing.Point(206, 164);
            this.btn_sumbit.Name = "btn_sumbit";
            this.btn_sumbit.Size = new System.Drawing.Size(93, 35);
            this.btn_sumbit.TabIndex = 4;
            this.btn_sumbit.Text = "Sign In";
            this.btn_sumbit.UseVisualStyleBackColor = false;
            this.btn_sumbit.Click += new System.EventHandler(this.Btn_sumbit_Click);
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
            // btn_Exit
            // 
            this.btn_Exit.BackColor = System.Drawing.Color.Transparent;
            this.btn_Exit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btn_Exit.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Exit.ForeColor = System.Drawing.Color.Navy;
            this.btn_Exit.Location = new System.Drawing.Point(305, 164);
            this.btn_Exit.Name = "btn_Exit";
            this.btn_Exit.Size = new System.Drawing.Size(86, 35);
            this.btn_Exit.TabIndex = 5;
            this.btn_Exit.Text = "Exit";
            this.btn_Exit.UseVisualStyleBackColor = false;
            this.btn_Exit.Click += new System.EventHandler(this.Btn_Exit_Click);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Navy;
            this.label2.Location = new System.Drawing.Point(131, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 18);
            this.label2.TabIndex = 4;
            this.label2.Text = "CompCode";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Navy;
            this.label3.Location = new System.Drawing.Point(131, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 18);
            this.label3.TabIndex = 5;
            this.label3.Text = "UserName";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Navy;
            this.label4.Location = new System.Drawing.Point(131, 114);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 18);
            this.label4.TabIndex = 6;
            this.label4.Text = "Password";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button1.ContextMenuStrip = this.contextMenuStrip1;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 72F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(10, 26);
            this.button1.Margin = new System.Windows.Forms.Padding(0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(114, 109);
            this.button1.TabIndex = 11;
            this.button1.UseVisualStyleBackColor = false;
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
            // txt_password
            // 
            this.txt_password.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_password.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_password.ForeColor = System.Drawing.Color.Navy;
            this.txt_password.Location = new System.Drawing.Point(208, 112);
            this.txt_password.Name = "txt_password";
            this.txt_password.PasswordChar = '*';
            this.txt_password.Size = new System.Drawing.Size(192, 23);
            this.txt_password.TabIndex = 3;
            this.txt_password.Enter += new System.EventHandler(this.Txt_password_Enter);
            this.txt_password.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_password_KeyDown);
            this.txt_password.Validating += new System.ComponentModel.CancelEventHandler(this.txt_password_Validating);
            // 
            // txtusername
            // 
            this.txtusername.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtusername.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtusername.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtusername.ForeColor = System.Drawing.Color.Navy;
            this.txtusername.Location = new System.Drawing.Point(208, 86);
            this.txtusername.Name = "txtusername";
            this.txtusername.Size = new System.Drawing.Size(192, 23);
            this.txtusername.TabIndex = 2;
            this.txtusername.Enter += new System.EventHandler(this.txtusername_Enter);
            this.txtusername.Validating += new System.ComponentModel.CancelEventHandler(this.txtusername_Validating);
            // 
            // buttblcreate
            // 
            this.buttblcreate.BackColor = System.Drawing.Color.Transparent;
            this.buttblcreate.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.buttblcreate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttblcreate.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttblcreate.ForeColor = System.Drawing.Color.Transparent;
            this.buttblcreate.Location = new System.Drawing.Point(61, 179);
            this.buttblcreate.Name = "buttblcreate";
            this.buttblcreate.Size = new System.Drawing.Size(93, 35);
            this.buttblcreate.TabIndex = 12;
            this.buttblcreate.UseVisualStyleBackColor = false;
            this.buttblcreate.Visible = false;
            this.buttblcreate.Click += new System.EventHandler(this.Buttblcreate_Click);
            // 
            // txtproductid
            // 
            this.txtproductid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtproductid.Enabled = false;
            this.txtproductid.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtproductid.ForeColor = System.Drawing.Color.Navy;
            this.txtproductid.Location = new System.Drawing.Point(453, 51);
            this.txtproductid.Name = "txtproductid";
            this.txtproductid.Size = new System.Drawing.Size(25, 23);
            this.txtproductid.TabIndex = 13;
            this.txtproductid.Visible = false;
            // 
            // txtproductkey
            // 
            this.txtproductkey.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtproductkey.Enabled = false;
            this.txtproductkey.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtproductkey.ForeColor = System.Drawing.Color.Navy;
            this.txtproductkey.Location = new System.Drawing.Point(453, 80);
            this.txtproductkey.Name = "txtproductkey";
            this.txtproductkey.Size = new System.Drawing.Size(25, 23);
            this.txtproductkey.TabIndex = 14;
            this.txtproductkey.Visible = false;
            // 
            // txtexperiencedays
            // 
            this.txtexperiencedays.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtexperiencedays.Enabled = false;
            this.txtexperiencedays.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtexperiencedays.ForeColor = System.Drawing.Color.Navy;
            this.txtexperiencedays.Location = new System.Drawing.Point(453, 22);
            this.txtexperiencedays.Name = "txtexperiencedays";
            this.txtexperiencedays.Size = new System.Drawing.Size(25, 23);
            this.txtexperiencedays.TabIndex = 15;
            this.txtexperiencedays.Visible = false;
            // 
            // combo_compcode
            // 
            this.combo_compcode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.combo_compcode.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.combo_compcode.Location = new System.Drawing.Point(208, 59);
            this.combo_compcode.Name = "combo_compcode";
            this.combo_compcode.Size = new System.Drawing.Size(192, 22);
            this.combo_compcode.TabIndex = 1;
            this.combo_compcode.TextChanged += new System.EventHandler(this.combo_compcode_TextChanged);
            this.combo_compcode.Validating += new System.ComponentModel.CancelEventHandler(this.combo_compcode_Validating);
            // 
            // ErrProvider
            // 
            this.ErrProvider.ContainerControl = this;
            // 
            // combofinyear
            // 
            this.combofinyear.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.combofinyear.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.combofinyear.ContextMenuStrip = this.contextMenuStrip1;
            this.combofinyear.Enabled = false;
            this.combofinyear.FormattingEnabled = true;
            this.combofinyear.Items.AddRange(new object[] {
            "",
            ""});
            this.combofinyear.Location = new System.Drawing.Point(208, 35);
            this.combofinyear.Name = "combofinyear";
            this.combofinyear.Size = new System.Drawing.Size(191, 21);
            this.combofinyear.TabIndex = 0;
            this.combofinyear.SelectedIndexChanged += new System.EventHandler(this.combofinyear_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Navy;
            this.label1.Location = new System.Drawing.Point(131, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 18);
            this.label1.TabIndex = 18;
            this.label1.Text = "FinYear";
            // 
            // LoginForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(443, 217);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.combofinyear);
            this.Controls.Add(this.combo_compcode);
            this.Controls.Add(this.txtexperiencedays);
            this.Controls.Add(this.txtproductkey);
            this.Controls.Add(this.txtproductid);
            this.Controls.Add(this.buttblcreate);
            this.Controls.Add(this.txtusername);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txt_password);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btn_Exit);
            this.Controls.Add(this.btn_sumbit);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoginForm";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pinnacle Software Limited";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.LoginForm_FormClosed);
            this.Load += new System.EventHandler(this.LoginForm_Load);
            this.contextMenuStrip2.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ErrProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_sumbit;
        private System.Windows.Forms.Button btn_Exit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txt_password;
        private System.Windows.Forms.TextBox txtusername;
        private System.Windows.Forms.Button buttblcreate;
        private System.Windows.Forms.TextBox txtproductid;
        private System.Windows.Forms.TextBox txtproductkey;
        private System.Windows.Forms.TextBox txtexperiencedays;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem1;
        private System.Windows.Forms.TextBox combo_compcode;
        private System.Windows.Forms.ErrorProvider ErrProvider;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox combofinyear;
    }
}