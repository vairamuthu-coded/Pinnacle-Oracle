using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Pinnacle
{
    public partial class CustomControl : UserControl
    {
        public CustomControl()
        {
            InitializeComponent();
        }
       
             
        public Image userimage
        {
            get { return pnlUserImage.BackgroundImage;  }
            set { pnlUserImage.BackgroundImage = value; }
        }
        public int ImageHeight
        {
            get { return pnlUserImage.Height; }
            set { pnlUserImage.Height = (int)value; }
        }
        public int ImageWidth
        {
            get { return pnlUserImage.Width; }
            set { pnlUserImage.Width = (int)value; }
        }
        public Color backcolor
        {
            get { return this.BackColor; }
            set { this.BackColor = value; }
        }
        public Color Panelcolor
        {
            get { return this.panel1.BackColor; }
            set { this.panel1.BackColor = value; }
        }
        public Button menuname
        {
            get { return butUserName; }
            set { butUserName = value; }
        }
        public Label username
        {
            get { return lblUserName; }
            set { lblUserName = value; }
        }
        public Label subtitle
        {
            get { return lblSubTitle; }
            set { lblSubTitle = value; }
        }

        public Panel iconbackground
        {
            get { return pnlIconBackground; }
            set { pnlIconBackground = value; }
        }
       

        private void CustomControl_MouseEnter(object sender, EventArgs e)
        {
          //  BorderStyle = BorderStyle.None;
           // this.BackColor = Color.Teal;
         
          //  this.username.ForeColor = Color.Blue;
        }

        private void CustomControl_MouseLeave(object sender, EventArgs e)
        {
          // this.BorderStyle = BorderStyle.Fixed3D;
          // this.BackColor = Color.Pink;
           // this.username.ForeColor = Color.Blue;
        }

        private void pnlIconBackground_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
