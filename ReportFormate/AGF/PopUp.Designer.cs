
namespace Pinnacle.ReportFormate.AGF
{
    partial class PopUp
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
            this.picturegarmentimage = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picturegarmentimage)).BeginInit();
            this.SuspendLayout();
            // 
            // picturegarmentimage
            // 
            this.picturegarmentimage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.picturegarmentimage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picturegarmentimage.Location = new System.Drawing.Point(0, 0);
            this.picturegarmentimage.Name = "picturegarmentimage";
            this.picturegarmentimage.Size = new System.Drawing.Size(380, 237);
            this.picturegarmentimage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picturegarmentimage.TabIndex = 0;
            this.picturegarmentimage.TabStop = false;
            // 
            // PopUp
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(380, 237);
            this.Controls.Add(this.picturegarmentimage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Location = new System.Drawing.Point(100, 100);
            this.Name = "PopUp";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Text = "PopUp";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PopUp_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.picturegarmentimage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picturegarmentimage;
    }
}