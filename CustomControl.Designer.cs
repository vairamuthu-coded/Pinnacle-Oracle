namespace Pinnacle
{
    partial class CustomControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlIconBackground = new System.Windows.Forms.Panel();
            this.lblUserName = new System.Windows.Forms.Label();
            this.lblSubTitle = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.butUserName = new System.Windows.Forms.Button();
            this.pnlUserImage = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlUserImage)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlIconBackground
            // 
            this.pnlIconBackground.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlIconBackground.BackColor = System.Drawing.Color.Teal;
            this.pnlIconBackground.ForeColor = System.Drawing.Color.White;
            this.pnlIconBackground.Location = new System.Drawing.Point(7, 4);
            this.pnlIconBackground.Margin = new System.Windows.Forms.Padding(0);
            this.pnlIconBackground.Name = "pnlIconBackground";
            this.pnlIconBackground.Size = new System.Drawing.Size(276, 10);
            this.pnlIconBackground.TabIndex = 2;
            this.pnlIconBackground.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlIconBackground_Paint);
            // 
            // lblUserName
            // 
            this.lblUserName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblUserName.AutoSize = true;
            this.lblUserName.BackColor = System.Drawing.Color.Transparent;
            this.lblUserName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblUserName.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserName.ForeColor = System.Drawing.Color.Maroon;
            this.lblUserName.Location = new System.Drawing.Point(130, 2);
            this.lblUserName.Margin = new System.Windows.Forms.Padding(0);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(50, 19);
            this.lblUserName.TabIndex = 1;
            this.lblUserName.Text = "items";
            this.lblUserName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblUserName.Visible = false;
            // 
            // lblSubTitle
            // 
            this.lblSubTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSubTitle.AutoSize = true;
            this.lblSubTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblSubTitle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblSubTitle.Font = new System.Drawing.Font("Palatino Linotype", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubTitle.ForeColor = System.Drawing.Color.Red;
            this.lblSubTitle.Location = new System.Drawing.Point(83, 5);
            this.lblSubTitle.Margin = new System.Windows.Forms.Padding(0);
            this.lblSubTitle.Name = "lblSubTitle";
            this.lblSubTitle.Size = new System.Drawing.Size(32, 16);
            this.lblSubTitle.TabIndex = 3;
            this.lblSubTitle.Text = "Rate";
            this.lblSubTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblSubTitle.Visible = false;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel1.Controls.Add(this.butUserName);
            this.panel1.Controls.Add(this.pnlUserImage);
            this.panel1.Controls.Add(this.lblSubTitle);
            this.panel1.Controls.Add(this.lblUserName);
            this.panel1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(7, 12);
            this.panel1.Margin = new System.Windows.Forms.Padding(10, 5, 10, 10);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(276, 71);
            this.panel1.TabIndex = 4;
            // 
            // butUserName
            // 
            this.butUserName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.butUserName.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.butUserName.BackColor = System.Drawing.Color.Transparent;
            this.butUserName.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.butUserName.FlatAppearance.BorderSize = 0;
            this.butUserName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.butUserName.Font = new System.Drawing.Font("Roboto Black", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butUserName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.butUserName.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.butUserName.Location = new System.Drawing.Point(3, 33);
            this.butUserName.Name = "butUserName";
            this.butUserName.Size = new System.Drawing.Size(270, 35);
            this.butUserName.TabIndex = 4;
            this.butUserName.Text = "items";
            this.butUserName.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.butUserName.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.butUserName.UseVisualStyleBackColor = false;
            // 
            // pnlUserImage
            // 
            this.pnlUserImage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlUserImage.BackColor = System.Drawing.Color.Transparent;
            this.pnlUserImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pnlUserImage.InitialImage = null;
            this.pnlUserImage.Location = new System.Drawing.Point(223, 2);
            this.pnlUserImage.Margin = new System.Windows.Forms.Padding(0);
            this.pnlUserImage.Name = "pnlUserImage";
            this.pnlUserImage.Size = new System.Drawing.Size(50, 28);
            this.pnlUserImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pnlUserImage.TabIndex = 0;
            this.pnlUserImage.TabStop = false;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Teal;
            this.button1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Teal;
            this.button1.Location = new System.Drawing.Point(7, 81);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(276, 2);
            this.button1.TabIndex = 5;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // CustomControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnlIconBackground);
            this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Margin = new System.Windows.Forms.Padding(10);
            this.Name = "CustomControl";
            this.Padding = new System.Windows.Forms.Padding(7);
            this.Size = new System.Drawing.Size(290, 90);
            this.MouseEnter += new System.EventHandler(this.CustomControl_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.CustomControl_MouseLeave);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlUserImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel pnlIconBackground;
        private System.Windows.Forms.PictureBox pnlUserImage;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Label lblSubTitle;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button butUserName;
        private System.Windows.Forms.Button button1;
    }
}
