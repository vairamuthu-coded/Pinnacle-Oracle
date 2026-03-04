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
    public partial class RegistrationMaster : Form,ToolStripAccess
    {
        private static RegistrationMaster _instance;
      
        public static RegistrationMaster Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new RegistrationMaster();
                GlobalVariables.CurrentForm = _instance; return _instance;
            }
        }
        Models.Master mas = new Models.Master();
        Models.UserRights sm = new Models.UserRights();
        int totaldays = 0;
        public RegistrationMaster()
        {
            InitializeComponent();
            GlobalVariables.HeaderName.Text = Class.Users.ScreenName; GlobalVariables.CurrentForm = this;
        }

        public void ReadOnlys()
        {

        }

        const int ProductCode = 1;

        private void butRegistration_Click(object sender, EventArgs e)
        {
            KeyManager km = new KeyManager(txtproductid.Text);
            string productKey = txtproductkey.Text;
            if (km.ValidKey(ref productKey))
            {
                KeyValuesClass kv = new KeyValuesClass();
                if (km.DisassembleKey(productKey, ref kv))
                {
                    LicenseInfo lic = new LicenseInfo();
                    lic.ProductKey = productKey;
                    lic.FullName = "Pinnacle";
                    if (kv.Type == LicenseType.TRIAL)
                    {
                        lic.Day = kv.Expiration.Day;
                        lic.Month = kv.Expiration.Month;
                        lic.Year = kv.Expiration.Year;
                        km.SaveSuretyFile(string.Format(@"{0}\Pinnacle.lic", Application.StartupPath), lic);

                        txtexperiencedays.Text = string.Format(@"{0}", (kv.Expiration - DateTime.Now.Date).Days);
                        var chk = "";
                        if (checkactive.Checked == true) chk = "T"; else chk = "F";
                        string sel = "select a.productkeys from  asptblregistration a where a.productkeys='" + txtproductkey.Text + "' and a.active='" + chk + "';";
                        DataSet ds = Utility.ExecuteSelectQuery(sel, "asptblregistration");
                        DataTable dt = ds.Tables["asptblregistration"];
                        if (dt.Rows.Count > 0)
                        {

                            MessageBox.Show("You have Updated", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else if (dt.Rows.Count != 0 && Convert.ToInt64("0" + txtregistrationid.Text) == 0 || txtregistrationid.Text == "")
                        {
                            string ins = "insert into asptblregistration(finyear,fromdate,todate,productid,productkeys,active,compcode ,username,ipaddress,createdon,createdby,modifiedon,days)values('" + Class.Users.Finyear + "','" + System.DateTime.Now.ToString() + "','" + kv.Expiration + "','" + txtproductid.Text + "','" + txtproductkey.Text + "','" + chk + "','" + Class.Users.COMPCODE + "','" + Class.Users.USERID + "','" + Class.Users.IPADDRESS + "','" + Class.Users.CREATED + "','" + Class.Users.HUserName + "','" + Class.Users.CREATED + "','" + txtexperiencedays.Text + "')";
                            Utility.ExecuteNonQuery(ins); GridLoad(); empty();
                            MessageBox.Show("You have registered", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            string ins = "update  asptblregistration set finyear='" + Class.Users.Finyear + "',fromdate='" + System.DateTime.Now.ToString() + "',todate='" + kv.Expiration + "',productid='" + txtproductid.Text + "',productkeys='" + txtproductkey.Text + "',active='" + chk + "',compcode='" + Class.Users.COMPCODE + "',username='" + Class.Users.USERID + "',ipaddress='" + Class.Users.IPADDRESS + "',createdon='" + Class.Users.CREATED + "',createdby='" + Class.Users.HUserName + "',modifiedon='" + Class.Users.CREATED + "'  ,days='" + txtexperiencedays.Text + "' where asptblregistrationid='" + txtregistrationid.Text + "';";
                            Utility.ExecuteNonQuery(ins);
                            GridLoad(); empty();
                            MessageBox.Show("You have Updated", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Your Product Key is Invalid", "Message", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }

            }
            else
            {
                MessageBox.Show("Invalid License Key", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtproductkey.Text = ""; checkactive.Checked = false;
            }
        }
      public  void GridLoad()
        {
            //try
            //{
            //    listView1.Items.Clear();
            //    string sel1 = "select asptblregistrationid,productid from asptblregistration;";
            //    DataSet ds = Utility.ExecuteSelectQuery(sel1, "asptblregistration");
            //    DataTable dt = ds.Tables["asptblregistration"];
            //    if (dt.Rows.Count > 0)
            //    {
            //        int i = 1;
            //        foreach (DataRow myRow in dt.Rows)
            //        {
            //            ListViewItem list = new ListViewItem();
            //            list.SubItems.Add(i.ToString());
            //            list.SubItems.Add(myRow["asptblregistrationid"].ToString());
            //            list.SubItems.Add(myRow["productid"].ToString());

            //            listView1.Items.Add(list);
            //            i++;
            //        }
            //        lbltotal.Text = "Total Count    :" + listView1.Items.Count;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }
        void empty()
        {
            txtregistrationid.Text = "";
            txtproductkey.Text = "";

        }
        private void RegistrationMaster_Load(object sender, EventArgs e)
        {
            txtproductid.Text = ComputerInfo.GetComputerId(); GridLoad();
        }



        //private void butRegistration_Click(object sender, EventArgs e)
        //{
        //    KeyManager km = new KeyManager(txtproductid.Text);
        //    string productKey = txtproductkey.Text;
        //    if(km.ValidKey(ref productKey))
        //    {
        //        KeyValuesClass kv = new KeyValuesClass();
        //        if(km.DisassembleKey(productKey,ref kv))
        //        {
        //            LicenseInfo lic = new LicenseInfo();
        //            lic.ProductKey = productKey;
        //            lic.FullName = "FoxLearn";
        //            if(kv.Type==LicenseType.TRIAL)
        //            {
                     
        //                lic.Day = kv.Expiration.Day;
        //                lic.Month = kv.Expiration.Month;
        //                lic.Year = kv.Expiration.Year;

        //            }
        //            km.SaveSuretyFile(string.Format(@"{0}\VAI.lic",Application.StartupPath),lic);
        //            MessageBox.Show("You have registered","Message",MessageBoxButtons.OK,MessageBoxIcon.Information);
        //           // this.Close();


        //        }
        //        else
        //        {
        //            MessageBox.Show("Your Product Key is Invalid", "Message", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        //        }
              
        //    }

        //}

        //private void RegistrationMaster_Load(object sender, EventArgs e)
        //{
        //    txtproductid.Text = ComputerInfo.GetComputerId();
        //}

      
        private void label6_Click(object sender, EventArgs e)
        {

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

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
         
            DateTime StartingDate = Convert.ToDateTime(dateTimePicker1.Value.Date.ToString());
            DateTime EndingDate = Convert.ToDateTime(dateTimePicker2.Value.Date.ToString());
            TimeSpan countdays = EndingDate.Subtract(StartingDate);
            totaldays = Convert.ToInt32(countdays.Days);
            txtexperiencedays.Text = totaldays.ToString();
        }
      
        public void News()
        {
            empty();
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
