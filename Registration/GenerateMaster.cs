using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FoxLearn.License;

namespace Pinnacle.Registration
{
    public partial class GenerateMaster : Form,ToolStripAccess
    {
        private static GenerateMaster _instance;
       
        public static GenerateMaster Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new GenerateMaster();
                GlobalVariables.CurrentForm = _instance; return _instance;
            }
        }

        Models.Master mas = new Models.Master();
        Models.UserRights sm = new Models.UserRights();
        public GenerateMaster()
        {
            InitializeComponent();
            GlobalVariables.HeaderName.Text = Class.Users.ScreenName; GlobalVariables.CurrentForm = this;
        }
        public void ReadOnlys()
        {

        }

        int totaldays = 0;
    
       
        const int ProductCode = 1;
        private void GenerateMaster_Load(object sender, EventArgs e)
        {
            combolicensetype.SelectedIndex = 0;
            txtproductid.Text = ComputerInfo.GetComputerId();
        }

        private void butGenerate_Click(object sender, EventArgs e)
        {
        
            KeyManager km = new KeyManager(txtproductid.Text);
            KeyValuesClass kv;
            string productKey = string.Empty;
            if (combolicensetype.SelectedIndex == 0)
            {
                kv = new KeyValuesClass()
                {
                    Type = LicenseType.FULL,
                    Header = Convert.ToByte(9),
                    Footer = Convert.ToByte(6),
                    ProductCode = (byte)ProductCode,
                    Edition = Edition.ENTERPRISE,
                    Version = 1
                };
                if (!km.GenerateKey(kv, ref productKey))
                {
                    txtproductkeys.Text = "Error";
                }
            }
            else
            {
                kv = new KeyValuesClass()
                {
                    Type = LicenseType.TRIAL,
                    Header = Convert.ToByte(9),
                    Footer = Convert.ToByte(6),
                    ProductCode = (byte)ProductCode,
                    Edition = Edition.ENTERPRISE,
                    Version = 1,
                    Expiration = DateTime.Now.Date.AddDays(Convert.ToInt32("0" + totaldays))
                };
                if (!km.GenerateKey(kv, ref productKey))               
                    txtproductkeys.Text = "Error";
                
            }
            txtproductkeys.Text = productKey;
        }

    

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            DateTime StartingDate = Convert.ToDateTime(dateTimePicker1.Value.Date.ToString());
            DateTime EndingDate = Convert.ToDateTime(dateTimePicker2.Value.Date.ToString());
            TimeSpan countdays = EndingDate.Subtract(StartingDate);
            totaldays = Convert.ToInt32(countdays.Days);
            txtexperiencedays.Text = totaldays.ToString();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            totaldays = 0;
            DateTime StartingDate = Convert.ToDateTime(dateTimePicker1.Value.Date.ToString());
            DateTime EndingDate = Convert.ToDateTime(dateTimePicker2.Value.Date.ToString());
            TimeSpan countdays = EndingDate.Subtract(StartingDate);
            totaldays = Convert.ToInt32(countdays.Days);
            txtexperiencedays.Text = totaldays.ToString();
        }

        public void News()
        {
           
        }

        public void Saves()
        {
           
        }

        public void Prints()
        {
           
        }

        public void Searchs()
        {
           
        }

        public void Deletes()
        {
           
        }

        public void Imports()
        {
           
        }

        public void Pdfs()
        {
           
        }

        public void ChangePasswords()
        {
           
        }

        public void DownLoads()
        {
           
        }

        public void ChangeSkins()
        {
           
        }

        public void Logins()
        {
           
        }

        public void GlobalSearchs()
        {
           
        }

        public void TreeButtons()
        {
           
        }

        public void Exit()
        {
            this.Hide();
            GlobalVariables.MdiPanel.Show(); GlobalVariables.HeaderName.Text = "";
        }

        public void GridLoad()
        {
           
        }

        public void Searchs(int id)
        {
            throw new NotImplementedException();
        }

        public void Deletes(int id)
        {
            throw new NotImplementedException();
        }
    }
}
