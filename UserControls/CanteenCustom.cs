using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Pinnacle.UserControls
{
    public partial class CanteenCustom : UserControl
    {
        public CanteenCustom()
        {
            InitializeComponent();
        }
        public Image userimage
        {
            get { return pnlUserImage.BackgroundImage; }
            set { pnlUserImage.BackgroundImage = value;  }
        }
        public int ImageHeight
        {
            get { return pnlUserImage.Height; }
            set { pnlUserImage.Height = (int)value; }
        }
        public Color backcolor
        {
            get { return this.BackColor; }
            set { this.BackColor = value; }
        }
        public Panel Panelcolor
        {
            get { return panel1; }
            set { panel1 = value; }
        }

        public Label LabelItems
        {
            get { return labelitems; }
            set { labelitems = value; }
        }
        public Button menuname
        {
            get { return butUserName; }
            set { butUserName = value; }
        }
        public Button Butdate
        {
            get { return butdate; }
            set { butdate = value; }
        }
        public Label username
        {
            get { return lblUserName; }
            set { lblUserName = value; }
        }
        public Label ActualCost
        {
            get { return lblactual; }
            set { lblactual = value; }
        }
 
        public Panel iconbackground
        {
            get { return pnlIconBackground; }
            set { pnlIconBackground = value; }
        }
        private void pnlUserImage_Click(object sender, EventArgs e)
        {

        }

        private void butUserName_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
