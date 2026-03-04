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
    public partial class AboutMaster : Form,ToolStripAccess
    {
        private static AboutMaster _instance;
      
        public static AboutMaster Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new AboutMaster();
                GlobalVariables.CurrentForm = _instance; 
                return _instance;
            }
        }
        Models.Master mas = new Models.Master();
        Models.UserRights sm = new Models.UserRights();
        public AboutMaster()
        {
            InitializeComponent();
            GlobalVariables.HeaderName.Text = Class.Users.ScreenName; 
        }

  
        private void AboutMaster_Load(object sender, EventArgs e)
        {
            txtproductid.Text = ComputerInfo.GetComputerId();
            KeyManager km = new KeyManager(txtproductid.Text);
            LicenseInfo lic = new LicenseInfo();
            int value = km.LoadSuretyFile(string.Format(@"{0}\VAI.lic", Application.StartupPath),ref lic);
            string productKey = lic.ProductKey;
            if(km.ValidKey(ref productKey))
            {
                KeyValuesClass kv = new KeyValuesClass();
                if (km.DisassembleKey(productKey, ref kv))
                {
                    txtproductkeys.Text = productKey;
                    if (kv.Type == LicenseType.TRIAL)
                    {
                        txtexperiencedays.Text = string.Format(@"{0} days", (kv.Expiration - DateTime.Now.Date).Days);
                        txtlicensetype.Text = LicenseType.TRIAL.ToString();
                    }
                    else
                    {
                        txtexperiencedays.Text = string.Format(@"{0} days", (kv.Expiration - DateTime.Now.Date).Days);
                        txtlicensetype.Text = LicenseType.FULL.ToString();
                    }
                }
            }
        }

        private void butAbout_Click(object sender, EventArgs e)
        {

        }

        private void Exit_Click(object sender, EventArgs e)
        {
            this.Hide();
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
        public void GridLoad()
        {
           
        }
        public void Exit()
        {
            this.Hide();

            GlobalVariables.MdiPanel.Show(); GlobalVariables.HeaderName.Text = "";
        }

        public void ReadOnlys()
        {
            throw new NotImplementedException();
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
