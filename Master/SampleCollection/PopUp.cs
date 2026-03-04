using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Pinnacle.Master.SampleCollection
{
    public partial class PopUp : Form
    {
        public PopUp()
        {
            InitializeComponent();
            this.Font = Class.Users.FontName;
            button1.ForeColor = Class.Users.Color1;
            button1.BackColor = Class.Users.BackColors;           
            button1.Text = Class.Users.TableName;           

            pictureBox1.Image = Class.Users.StaticPicture.Image;
        }

        private void PopUp_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Hide();
        }

        private void PopUp_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
        }
    }
}
