using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace Pinnacle.Transactions
{
    public partial class SecurityInventry : Form,ToolStripAccess
    {
        Models.Master mas = new Models.Master();
        Models.UserRights sm = new Models.UserRights(); Utility _utility = new Utility();
        Models.SecInventryModel si = new Models.SecInventryModel();
        private static SecurityInventry _instance;
        PinnacleMdi mdi = new PinnacleMdi(); byte[] bytes;
      //  private static string s1 = ConfigurationManager.ConnectionStrings["ConnectionString1"].ConnectionString;
        //static MongoClient client = new MongoClient();
        //static IMongoDatabase db = client.GetDatabase("tiplagf");
        //static IMongoCollection<Pinnacle.Models.Employee> collection = db.GetCollection<Pinnacle.Models.Employee>("Employee");
        public static SecurityInventry Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new SecurityInventry();
                GlobalVariables.CurrentForm = _instance; return _instance;
            }
        }
        public SecurityInventry()
        {
            InitializeComponent();
            Class.Users.ScreenName = "SecurityInventry";
            Class.Users.IPADDRESS = GenFun.GetLocalIPAddress();
            Class.Users.CREATED = Convert.ToDateTime(System.DateTime.Now.ToString("dd-MM-yyyy") + " " + System.DateTime.Now.ToLongTimeString());
            Class.Users.SysDate = Convert.ToString(System.DateTime.Now.ToString("dd-MM-yyyy"));
            Class.Users.SysTime = Convert.ToString(DateTime.Now.ToLongTimeString().ToString());
            si.SystemDate = Class.Users.SysDate;
            si.IpAddress = Class.Users.IPADDRESS;


            if (Class.Users.HostelName == "GENTS STAFF HOSTEL" || Class.Users.HostelName == "WOMENS HOSTEL" || Class.Users.HostelName == "WORKING GENTS HOSTEL")
            {
                tabControl1.TabPages.Remove(tabGOODSINOUT);
            }
            tabControl1.TabPages.Remove(tabGOODSIN);
            tabControl1.TabPages.Remove(tabGOODSOut);
            tabControl1.TabPages.Remove(tabEMPIn);
            tabControl1.TabPages.Remove(tabEMPOut);

            
            GlobalVariables.HeaderName.Text = Class.Users.ScreenName;
         
           /// butheader.BackColor = Class.Users.BackColors;
          
            panel2.BackColor = Class.Users.BackColors;
            panel6.BackColor = Class.Users.BackColors;
            panel7.BackColor = Class.Users.BackColors;
            panel8.BackColor = Class.Users.BackColors;
            button1.BackColor = Class.Users.BackColors;
            button5.BackColor = Class.Users.BackColors;
            panel9.BackColor = Class.Users.BackColors;
            panel4.BackColor = Class.Users.BackColors;
            this.BackColor = Class.Users.BackColors;
        }


        public void usercheck(string s, string ss, string sss)
        {


            DataTable dt1 = sm.headerdropdowns(s, ss, sss);
            if (dt1.Rows.Count > 0)
            {
                if (dt1.Rows[0]["Menuname"].ToString() == Class.Users.ScreenName)
                {
                    for (int r = 0; r < dt1.Rows.Count; r++)
                    {
                        if (dt1.Rows[r]["NEWS"].ToString() == "T") { GlobalVariables.News.Visible = true; } else { GlobalVariables.News.Visible = false; }
                        if (dt1.Rows[r]["SAVES"].ToString() == "T") { GlobalVariables.Saves.Visible = true; } else { GlobalVariables.Saves.Visible = false; }
                        if (dt1.Rows[r]["PRINTS"].ToString() == "T") { GlobalVariables.Prints.Visible = true; } else { GlobalVariables.Prints.Visible = false; }
                        if (dt1.Rows[r]["READONLY"].ToString() == "T") { this.Enabled = true; } else { this.Enabled = false; }
                        if (dt1.Rows[r]["SEARCH"].ToString() == "T") { GlobalVariables.Searchs.Visible = true; } else { GlobalVariables.Searchs.Visible = false; }
                        if (dt1.Rows[r]["DELETES"].ToString() == "T") { GlobalVariables.Deletes.Visible = true; } else { GlobalVariables.Deletes.Visible = false; }
                        if (dt1.Rows[r]["TREEBUTTON"].ToString() == "T") { GlobalVariables.TreeButtons.Visible = false; } else { GlobalVariables.TreeButtons.Visible = false; }
                        if (dt1.Rows[r]["GLOBALSEARCH"].ToString() == "T") { GlobalVariables.GlobalSearchs.Visible = true; } else { GlobalVariables.GlobalSearchs.Visible = false; }
                        if (dt1.Rows[r]["LOGIN"].ToString() == "T") { GlobalVariables.Logins.Visible = true; } else { GlobalVariables.Logins.Visible = false; }
                        if (dt1.Rows[r]["CHANGEPASSWORD"].ToString() == "T") { GlobalVariables.ChangePasswords.Visible = true; } else { GlobalVariables.ChangePasswords.Visible = false; }
                        if (dt1.Rows[r]["CHANGESKIN"].ToString() == "T") { GlobalVariables.ChangeSkins.Visible = true; } else { GlobalVariables.ChangeSkins.Visible = false; }
                        if (dt1.Rows[r]["DOWNLOAD"].ToString() == "T") { GlobalVariables.DownLoads.Visible = true; } else { GlobalVariables.DownLoads.Visible = false; }
                        if (dt1.Rows[r]["Pdf"].ToString() == "T") { GlobalVariables.Pdfs.Visible = true; } else { GlobalVariables.Pdfs.Visible = false; }
                        if (dt1.Rows[r]["Imports"].ToString() == "T") { GlobalVariables.Imports.Visible = true; } else { GlobalVariables.Imports.Visible = false; }
                      
                    }
                }


            }
            else
            {
                GlobalVariables.Saves.Visible = false;
                GlobalVariables.Prints.Visible = false;
                GlobalVariables.Prints.Visible = false;
                GlobalVariables.Searchs.Visible = false;
                GlobalVariables.Deletes.Visible = false;
                GlobalVariables.TreeButtons.Visible = false;
                GlobalVariables.GlobalSearchs.Visible = false;
                GlobalVariables.Logins.Visible = false;
                GlobalVariables.ChangePasswords.Visible = false;
                GlobalVariables.ChangeSkins.Visible = false;
                GlobalVariables.DownLoads.Visible = false;
                GlobalVariables.Pdfs.Visible = false;
                GlobalVariables.Imports.Visible = false;

            }

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

      

        private void SecurityInventry_Load(object sender, EventArgs e)
        {
            Class.Users.Intimation ="";
            if (Class.Users.HCompcode == "FLFD")
            {
                tabControl1.TabPages.Remove(tabEMPInOut);
                tabControl1.TabPages.Remove(tabPage6);
                tabControl1.TabPages.Remove(tabEMPInOut);
            }
            if (Class.Users.HCompcode == "VEL")
            {
                tabControl1.TabPages.Remove(tabEMPOut);
                tabControl1.TabPages.Remove(tabEMPIn);
                tabControl1.TabPages.Remove(tabEMPInOut);
            }
            
            txt_inventryid.Enabled = false; Class.Users.UserTime = 0;
            //  txt_gatename.Text = si.GateName;
            if (Class.Users.HostelName == "GENTS STAFF HOSTEL" || Class.Users.HostelName == "WORKING GENTS HOSTEL" || Class.Users.HostelName == "WOMENS HOSTEL")
            {
                tabControl1.TabPages.Remove(tabGOODSINOUT);
            }
            combo_username.DisplayMember = Class.Users.HUserName;
                txt_modifiedon.Text = Convert.ToString(Class.Users.CREATED);
                txt_createdon.Text = Convert.ToString(Class.Users.CREATED);
                txt_sysdate.Text = Convert.ToString(si.SystemDate);
                txt_systime.Text = Class.Users.SysTime;
                txt_ipaddress.Text = Convert.ToString(si.IpAddress);
                txt_gatename.Text = Class.Users.HGateName;

            txtinventryidout.Enabled = false;
          //  txtgatenameout.Text = si.GateName;

            combo_usernameout.DisplayMember = Class.Users.HUserName;
            txtmodifiedout.Text = Convert.ToString(Class.Users.CREATED);
            txtcreatedonout.Text = Convert.ToString(Class.Users.CREATED);
            txtsystemdateout.Text = Convert.ToString(si.SystemDate);
            txtsystemtimeout.Text = Class.Users.SysTime;
          
            txtipaddressout.Text = Convert.ToString(si.IpAddress);
            txtgatenameout.Text = Class.Users.HGateName;
            try
            {
               
                    DataTable dt = mas.findcomcode(Class.Users.HCompcode, Class.Users.HUserName);
                    if (dt.Rows.Count > 0)
                    {
                        combo_compcode.DisplayMember = "COMPCODE";
                        combo_compcode.ValueMember = "GTCOMPMASTID";
                        combo_compcode.DataSource = dt;

                        combo_compcodeout.DisplayMember = "COMPCODE";
                        combo_compcodeout.ValueMember = "GTCOMPMASTID";
                        combo_compcodeout.DataSource = dt;


                        combo_username.DisplayMember = "USERNAME";
                        combo_username.ValueMember = "USERID";
                        combo_username.DataSource = dt;


                    combo_usernameout.DisplayMember = "USERNAME";
                    combo_usernameout.ValueMember = "USERID";
                    combo_usernameout.DataSource = dt;





                }
                DataTable dt1 = mas.Loginfinyear(Class.Users.Finyear,Class.Users.COMPCODE);
                if (dt1.Rows.Count > 0)
                {
                    Class.Users.Intimation = "PAYROLL";
                    combofinyearIN.DisplayMember = "finyear";
                    combofinyearIN.ValueMember = "gtfinancialyearid";
                    combofinyearIN.DataSource = dt1;
                    Class.Users.Finyear = dt1.Rows[0]["finyear"].ToString();
                    combofinyearout.DisplayMember = "finyear";
                    combofinyearout.ValueMember = "gtfinancialyearid";
                    combofinyearout.DataSource = dt1;
                }
                //else
                //{
                //    Class.Users.Intimation = "";
                //    DataTable findt = mas.finyear();
                //    combofinyearIN.DisplayMember = "finyear";
                //    combofinyearIN.ValueMember = "gtfinancialyearid";
                //    combofinyearIN.DataSource = findt;
                //    Class.Users.Finyear = findt.Rows[0]["finyear"].ToString();
                //    combofinyearout.DisplayMember = "finyear";
                //    combofinyearout.ValueMember = "gtfinancialyearid";
                //    combofinyearout.DataSource = findt;
                //}
               
               
            }
            catch (Exception ex)
            {
                MessageBox.Show("Data Source Not Connected" + ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           // txtidcardno.Focus();
            timer1.Start();
            lblRecordnoOUT.Visible = false; lblRecorddatetimeOUT.Visible = false;
            lblRecordno.Visible = false; lblRecorddatetime.Visible = false;
        }

        private void Combo_compcode_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void empty()
        {
           
            txt_qrcodein.Text = ""; lblRecorddatetime1.Visible = false;
            txtqrcodeout.Text = ""; lblRecorddatetimeOUT1.Visible = false;
            txt_qrcodein.Focus(); Class.Users.UserTime = 0;
            //butheader.BackColor = Class.Users.BackColors;
            panel2.BackColor = Class.Users.BackColors;

            panel6.BackColor = Class.Users.BackColors;
            panel7.BackColor = Class.Users.BackColors;
            panel8.BackColor = Class.Users.BackColors;
            button1.BackColor = Class.Users.BackColors;
            button5.BackColor = Class.Users.BackColors;
            panel9.BackColor = Class.Users.BackColors;
            panel4.BackColor = Class.Users.BackColors;
            this.BackColor = Class.Users.BackColors;
            txtintime.Text = "";txtouttime.Text = "";txtremarks.Text = "";
        }

        private void Saves_Click(object sender, EventArgs e)
        {

           

        }

       

       
        private void Txt_qrcode_Leave(object sender, EventArgs e)
        {
            if (txt_qrcodein.Text.Length >= 16)
            {
                Saves_Click(sender, e);
            }
            txt_qrcodein.Focus();
        }

      

        private void SecurityInventry_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }

      

        //private void Exit_Click(object sender, EventArgs e)
        //{
         
        //    this.Close();
        //}
        

       
        private void SecurityInventry_FormClosing(object sender, FormClosingEventArgs e)
        {
            _instance = null;
          
        }


        public void News()
        {
            empty();
        }

       

        private void Txt_qrcode_TabIndexChanged_1(object sender, EventArgs e)
        {
            btnsavesIN.Focus();
        }



        public void Exit()
        {
            GlobalVariables.MdiPanel.Show();
            News();Class.Users.Intimation = "";
            GlobalVariables.HeaderName.Text = "";
            GlobalVariables.TabCtrl.TabPages.RemoveAt(GlobalVariables.TabCtrl.SelectedIndex);
            this.Hide();
        }

        private void RefreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            usercheck(Class.Users.HCompcode, Class.Users.HUserName, Class.Users.ScreenName);

        }
        private void Butempsave_Click(object sender, EventArgs e)
        {
      
          
        }

        private void empty1()
        {
            txtintime.Text="";
            txtouttime.Text = "";

        }

       
      

        private void Panelbutexit_Click(object sender, EventArgs e)
        {
           
        }

        private void Panelbutinexit_Click(object sender, EventArgs e)
        {
             tabControl1.SelectTab(tabEMPIn); Class.Users.Intimation = "PAYROLL";
        }
        private void Butsave_Click(object sender, EventArgs e)
        {
            
        }
        private void Txtouttime_TextChanged(object sender, EventArgs e)
        {
          
            //try
            //{
            //    if (txtouttime.Text.Length == 8)
            //    {

            //        string sel = "select COUNT(A.OUTTIME) AS CNT ,A.EMPNAME,A.MANUALTIME,A.PERMISSIONHRS FROM ASPTBLHOSTELGATEPASS A where A.ASPTBLHOSTELGATEPASSID=" + txtouttime.Text + "  GROUP BY A.EMPNAME,A.MANUALTIME,A.PERMISSIONHRS";
            //        DataSet ds = Utility.ExecuteSelectQuery(sel, "ASPTBLHOSTELGATEPASS");
            //        DataTable dt = ds.Tables["ASPTBLHOSTELGATEPASS"];
            //        if (Convert.ToString(dt.Rows.Count) == null)
            //        {
            //            MessageBox.Show("invlaid");
            //        }
            //        else
            //        {
            //            Int64 cc = Convert.ToInt64(dt.Rows[0]["CNT"].ToString());
            //            if (cc > 0)
            //            {
            //                MessageBox.Show("This Token-Number  Already  Process Completed       :" + txtouttime.Text, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //                txtouttime.Text = ""; lbloutdetails.Visible = false;
            //            }
            //            else
            //            {
            //                string ins = "UPDATE ASPTBLHOSTELGATEPASS set OUTTIME='" + System.DateTime.Now.ToString() + "' ,IPADDRESS='" + Class.Users.IPADDRESS + "' WHERE ASPTBLHOSTELGATEPASSID=" + txtouttime.Text;
            //                Utility.ExecuteNonQuery(ins);
            //                MessageBox.Show("Record Updated Successfully     : " + txtouttime.Text, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //               lbloutdetails.Visible = true;
            //                var endtime = TimeSpan.Parse(DateTime.Now.ToString("HH:mm"));
            //                var statetime = TimeSpan.Parse(dt.Rows[0]["MANUALTIME"].ToString()) + TimeSpan.Parse(dt.Rows[0]["PERMISSIONHRS"].ToString());
            //                TimeSpan differ = endtime - statetime;
            //                if (differ.Minutes < 0) { lbloutdetails.Text = "OutTime : " + dt.Rows[0]["MANUALTIME"].ToString() + "  Per-Time   : " + dt.Rows[0]["PERMISSIONHRS"].ToString() + "Remaining Hrs" + differ.ToString(); }
            //                else { lbloutdetails.Text = "OutTime: " + dt.Rows[0]["MANUALTIME"].ToString() + "  Per-Time   : " + dt.Rows[0]["PERMISSIONHRS"].ToString() + "\n" + "Delay Hrs    " + differ.ToString(); }

            //                string sel2 = "select d.imagefieldvalu AS EMPIMAGE from hremploymast a  join hremploydetails b on a.hremploymastid=b.hremploymastid join gtcompmast c on c.gtcompmastid=a.compcode left join hremploymastimage d on d.hremploymastid=a.hremploymastid  where b.hostel='YES' and c.compcode='" + Class.Users.HCompcode + "'  and  d.hremploymastid='" + dt.Rows[0]["EMPNAME"].ToString() + "'";
            //                DataSet ds2 = Utility.ExecuteSelectQuery(sel2, "hremploymast");
            //                DataTable dt2 = ds2.Tables["hremploymast"];
            //                if (dt2 != null)
            //                {
            //                    bytes = (byte[])dt2.Rows[0]["EMPIMAGE"];
            //                    Image img = Models.Device.ByteArrayToImage(bytes);
            //                    pictureoutimage.Image = img;


            //                }
            //                else
            //                {
            //                    pictureoutimage.Image = Pinnacle.Properties.Resources.close_image1;
            //                }

            //            }


            //        }
            //    }
               
            //}
            //catch (Exception ex)
            //{
            //   // MessageBox.Show("");
            //    MessageBox.Show("Invalid Token Number", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    txtouttime.Text = ""; Cursor = Cursors.Default; lbloutdetails.Visible = false;
            //}
        }

        private void Txtintime_TextChanged(object sender, EventArgs e)
        {
            //try
            //{
            //   var qr = txtintime.Text;
            //    string[] qrdata = qr.Split(',');
            //    if (txtintime.Text.Length == 8)
            //    {
                    
            //        string sel = "select COUNT(A.OUTTIME)AS CNT,A.EMPNAME,A.MANUALTIME,A.PERMISSIONHRS FROM ASPTBLHOSTELGATEPASS A where A.ASPTBLHOSTELGATEPASSID=" + txtintime.Text + " GROUP BY A.EMPNAME,A.MANUALTIME,A.PERMISSIONHRS";
            //        DataSet ds = Utility.ExecuteSelectQuery(sel, "ASPTBLHOSTELGATEPASS");
            //        DataTable dt = ds.Tables["ASPTBLHOSTELGATEPASS"];
            //        Int64 cc = Convert.ToInt64(dt.Rows[0]["CNT"].ToString());
            //        if (cc > 0)
            //        {

            //            string sel1 = "select COUNT(A.INTIME) CNT  FROM ASPTBLHOSTELGATEPASS A where A.ASPTBLHOSTELGATEPASSID=" + txtintime.Text;
            //            DataSet ds1 = Utility.ExecuteSelectQuery(sel1, "ASPTBLHOSTELGATEPASS");
            //            DataTable dt1 = ds1.Tables["ASPTBLHOSTELGATEPASS"];
            //            Int64 cc1 = Convert.ToInt64(dt1.Rows[0]["CNT"].ToString());
            //            if (cc1 > 0)
            //            {
            //                MessageBox.Show("This Token-Number  Already  Process Completed       :" + txtintime.Text, "Information", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //                lblalert.Visible = false;
            //                txtintime.Text = "";

            //            }
            //            else
            //            {

            //                string ins = "update  ASPTBLHOSTELGATEPASS set INTIME='" + System.DateTime.Now.ToString() + "',IPADDRESS='" + Class.Users.IPADDRESS + "' WHERE ASPTBLHOSTELGATEPASSID=" + txtintime.Text;
            //                Utility.ExecuteNonQuery(ins);
            //                lblalert.Visible = true;
            //                lblalert.Text = "OutTime.." + dt.Rows[0]["MANUALTIME"].ToString() + " PerMission Time" + dt.Rows[0]["PERMISSIONHRS"].ToString();
            //                var endtime = TimeSpan.Parse(DateTime.Now.ToString("HH:mm"));
            //                var statetime = TimeSpan.Parse(dt.Rows[0]["MANUALTIME"].ToString()) + TimeSpan.Parse(dt.Rows[0]["PERMISSIONHRS"].ToString());
            //                TimeSpan differ = endtime - statetime;
            //                if (differ.Minutes < 0) { lblalert.Text = "OutTime    : " + dt.Rows[0]["MANUALTIME"].ToString() + "  Permission Hrs   : " + dt.Rows[0]["PERMISSIONHRS"].ToString() + " Remaining Hrs    " + differ.ToString(); }
            //                else { lblalert.Text = "OutTime    : " + dt.Rows[0]["MANUALTIME"].ToString() + "  Permission Hrs   : " + dt.Rows[0]["PERMISSIONHRS"].ToString() + " Delay Hrs    " + differ.ToString(); }

            //                string sel2 = "select d.imagefieldvalu AS EMPIMAGE from hremploymast a  join hremploydetails b on a.hremploymastid=b.hremploymastid join gtcompmast c on c.gtcompmastid=a.compcode left join hremploymastimage d on d.hremploymastid=a.hremploymastid  where b.hostel='YES' and c.compcode='" + Class.Users.HCompcode + "'  and  d.hremploymastid='" + dt.Rows[0]["EMPNAME"].ToString() + "'";
            //                DataSet ds2 = Utility.ExecuteSelectQuery(sel2, "hremploymast");
            //                DataTable dt2 = ds2.Tables["hremploymast"];
            //                if (dt2 != null)
            //                {
            //                    bytes = (byte[])dt2.Rows[0]["EMPIMAGE"];
            //                    Image img = Models.Device.ByteArrayToImage(bytes);
            //                    pictureinwardimage.Image = img;
            //                }
            //                else
            //                {
            //                    pictureinwardimage.Image = Pinnacle.Properties.Resources.close_image1;
            //                }
            //                MessageBox.Show("Record Updated Successfully     : " + txtintime.Text, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //            }

            //        }
            //        else if (dt.Rows[0]["CNT"].ToString() == null)
            //        {
            //            MessageBox.Show("Invalid Token Number", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //            txtintime.Text = "";

            //        }
            //        if (cc == 0)
            //        {
            //          //  MessageBox.Show("");
            //            MessageBox.Show("Pls go to Outward entry", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //            txtintime.Text = ""; lblalert.Visible = false;
            //        }
                   
                    
            //    }
             
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("");
            //    MessageBox.Show("Invalid Token Number   :", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            //    txtintime.Text = ""; 
            //}
           
        }

        

        private void Butout_Click(object sender, EventArgs e)
        {
          
            //tabControl1.TabPages.Remove(tabGOODSIN);
            //tabControl1.TabPages.Remove(tabGOODSOut);
            tabControl1.TabPages.Add(tabEMPOut);
            tabControl1.SelectTab(tabEMPOut); Class.Users.Intimation = "PAYROLL";
            txtouttime.Focus(); Class.Users.UserTime = 0;
        }

        private void Bunin_Click(object sender, EventArgs e)
        {
        
            
            //tabControl1.TabPages.Remove(tabGOODSIN);
            //tabControl1.TabPages.Remove(tabGOODSOut);
            tabControl1.TabPages.Add(tabEMPIn);
            tabControl1.SelectTab(tabEMPIn); Class.Users.Intimation = "PAYROLL";
            txtintime.Focus(); Class.Users.UserTime = 0;

        }

    
      

        private void Butempoutwardclear_Click(object sender, EventArgs e)
        {
            txtouttime.Text = "";
            pictureoutimage.Image = null; 
            bytes = null; label32.Visible = false; Class.Users.UserTime = 0;
            tabControl1.TabPages.Remove(tabEMPOut); Class.Users.Intimation = "PAYROLL";
            tabControl1.SelectTab(tabEMPInOut);
        }



        private void Button6_Click(object sender, EventArgs e)
        {
            //tabControl1.TabPages.Remove(tabPage5); tabControl1.SelectTab(tabPage2); 
        }

        

        private void TabControl1_Click(object sender, EventArgs e)
        {
            tabControl1.TabPages.Remove(tabEMPIn);
            tabControl1.TabPages.Remove(tabEMPOut);
            tabControl1.TabPages.Remove(tabGOODSIN); 
            tabControl1.TabPages.Remove(tabGOODSOut);
            Class.Users.UserTime = 0;
        }

        private void Butempinward_Click(object sender, EventArgs e)
        {
            Class.Users.UserTime = 0; Class.Users.Intimation = "PAYROLL";
            try
            {
                if (txtintime.Text == "")
                {
                    MessageBox.Show("Empty Not Allowed      " + txtintime.Text, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtintime.Select();
                    return;
                }
                else
                {
                    var qr = txtintime.Text; string repass = "";
                    string[] qrdata = qr.Split(':', ',');
                    string ss = "";                    
                    string pmissed = ""; pictureinwardimage.Image = null;                   
                  
                    if (checkpassmissed.Checked == true) { ss = qrdata[0].Trim(); pmissed = "T"; repass = txtremarks.Text + " - " + "PASS MISSED."; } 
                    else { pmissed = "F"; repass = txtremarks.Text; ss = qrdata[1].Trim(); }

                    if (checkpassmissed.Checked == false && Class.Users.HCompcode == "AGF" || checkpassmissed.Checked == false &&  Class.Users.HCompcode == "VEL" || checkpassmissed.Checked == false &&  Class.Users.HCompcode == "FLFD" || checkpassmissed.Checked == false &&  Class.Users.HCompcode == "AGFM" || checkpassmissed.Checked == false &&  Class.Users.HCompcode == "AGFMGII")
                    {

                        if (ss.Length > 7)
                        {
                            txtintime.Text = "";
                            txtintime.Text = ss;
                            string sel = "select COUNT(A.OUTTIME)AS CNT,A.EMPNAME,SUBSTR(A.OUTTIME,11,9) AS MANUALTIME,SUBSTR(A.PERMISSIONHRS,11,9) AS PERMISSIONHRS,A.OUTTIME FROM ASPTBLHOSTELGATEPASS A where A.ASPTBLHOSTELGATEPASSID=" + txtintime.Text + " GROUP BY A.EMPNAME,A.OUTTIME,A.PERMISSIONHRS";
                            DataSet ds = Utility.ExecuteSelectQuery(sel,"ASPTBLHOSTELGATEPASS");
                            DataTable dt = ds.Tables["ASPTBLHOSTELGATEPASS"];

                            Int64 cc = Convert.ToInt64(dt.Rows[0]["CNT"].ToString());
                            if (cc > 0)
                            {
                                string sel1 = "select COUNT(A.INTIME) as CNT  FROM ASPTBLHOSTELGATEPASS A where A.ASPTBLHOSTELGATEPASSID=" + txtintime.Text;
                                DataSet ds1 = Utility.ExecuteSelectQuery(sel1, "ASPTBLHOSTELGATEPASS");
                                DataTable dt1 = ds1.Tables["ASPTBLHOSTELGATEPASS"];
                                Int64 cc1 = Convert.ToInt64("0" + dt1.Rows[0]["CNT"].ToString());
                                if (cc1 >= 1)
                                {
                                    MessageBox.Show("This Token-Number  Already  Process Completed       :" + txtintime.Text, "Information", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                    lblalert.Visible = false;
                                    txtintime.Text = ""; txtremarks.Text = "";
                                    txtintime.Select(); checkpassmissed.Checked = false;
                                }
                                else
                                {

                                    DateTime endtime = Convert.ToDateTime(System.DateTime.Now.ToString());
                                    DateTime statetime = DateTime.Parse(dt.Rows[0]["MANUALTIME"].ToString());
                                    var statetime1 = DateTime.Parse(dt.Rows[0]["OUTTIME"].ToString());
                                    TimeSpan differ = endtime.Subtract(statetime);
                                    TimeSpan differ1 = endtime - statetime1;
                                    string ins = "update  ASPTBLHOSTELGATEPASS set username='" + Class.Users.USERID + "' ,INTIME='" + System.DateTime.Now.ToString() + "',TIMEDIFF='" + differ1.ToString() + "',PASSMISSED='" + pmissed + "',REMARKS1='" + repass + "',IPADDRESS='" + Class.Users.IPADDRESS + "' WHERE ASPTBLHOSTELGATEPASSID=" + txtintime.Text;

                                    Utility.ExecuteNonQuery(ins);
                                    lblalert.Visible = true;
                                    lblalert.Text = "OutTime.." + dt.Rows[0]["OUTTIME"].ToString() + " PerMission Time" + dt.Rows[0]["MANUALTIME"].ToString();

                                    if (differ.Minutes < 0) { lblalert.Text = "OutTime    : " + dt.Rows[0]["OUTTIME"].ToString() + "  Permission Hrs   : " + dt.Rows[0]["PERMISSIONHRS"].ToString() + " Remaining Hrs    " + differ.ToString(); }
                                    else { lblalert.Text = "OutTime    : " + dt.Rows[0]["MANUALTIME"].ToString() + "  Permission Hrs   : " + endtime.ToString() + " Delay Hrs    " + differ.ToString(); }

                                    //string sel2 = "SELECT A.EMPIMAGE FROM  ASPTBLEMP A JOIN   GTCOMPMAST B ON B.gtcompmastid = A.COMPCODE  join ASPTBLHOSTELGATEPASS D on D.IDCARDNO=A.IDCARDNO WHERE D.ASPTBLHOSTELGATEPASSID=" + txtintime.Text;
                                    //DataSet ds2 = Utility.ExecuteSelectQuery(sel2, "ASPTBLEMP");
                                    //DataTable dt2 = ds2.Tables["ASPTBLEMP"];
                                    //if (dt2.Rows.Count > 0)
                                    //{
                                    //    bytes = (byte[])dt2.Rows[0]["EMPIMAGE"];
                                    //    Image img = Models.Device.ByteArrayToImage(bytes);
                                    //    pictureinwardimage.Image = img;
                                    //}
                                    //else
                                    //{
                                    //    pictureinwardimage.Image = Pinnacle.Properties.Resources.close_image1;
                                    //}
                                    // pictureinwardimage.Image = Pinnacle.Properties.Resources.close_image1;
                                    MessageBox.Show("Record Updated Successfully     : " + txtintime.Text, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    txtintime.Text = ""; txtremarks.Text = ""; txtintime.Select(); checkpassmissed.Checked = false;
                                }
                            }
                            else if (dt.Rows[0]["CNT"].ToString() == null)
                            {
                                MessageBox.Show("Invalid Token Number", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                txtintime.Text = ""; txtremarks.Text = ""; txtintime.Select(); checkpassmissed.Checked = false;

                            }
                            if (cc == 0)
                            {
                                MessageBox.Show("Pls go to Outward entry", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                txtintime.Text = ""; txtremarks.Text = ""; lblalert.Visible = false; txtintime.Select(); checkpassmissed.Checked = false;
                            }
                        }
                      
                        return;
                    }
                    if (checkpassmissed.Checked == true)
                    {
                        txtintime.Text = "";
                        txtintime.Text = ss;
                        string sel0 = "SELECT  MAX(A.ASPTBLHOSTELGATEPASSID) AS ID FROM ASPTBLHOSTELGATEPASS A WHERE A.IDCARDNO=" + txtintime.Text + " GROUP BY A.IDCARDNO";
                        DataSet ds0 = Utility.ExecuteSelectQuery(sel0, "ASPTBLHOSTELGATEPASS");
                        DataTable dt0 = ds0.Tables["ASPTBLHOSTELGATEPASS"];
                        if (dt0.Rows.Count > 0)
                        {

                            string sel = "select COUNT(A.OUTTIME)AS CNT,A.EMPNAME,A.MANUALTIME,SUBSTR(A.PERMISSIONHRS,11,9) AS PERMISSIONHRS,A.OUTTIME FROM ASPTBLHOSTELGATEPASS A where A.ASPTBLHOSTELGATEPASSID=" + dt0.Rows[0]["ID"].ToString() + " GROUP BY A.EMPNAME,A.MANUALTIME,A.PERMISSIONHRS,A.OUTTIME";
                            DataSet ds = Utility.ExecuteSelectQuery(sel, "ASPTBLHOSTELGATEPASS");
                            DataTable dt = ds.Tables["ASPTBLHOSTELGATEPASS"];
                            Int64 cc = Convert.ToInt64(dt.Rows[0]["CNT"].ToString());
                            if (cc > 0)
                            {

                                string sel1 = "select COUNT(A.INTIME) CNT,A.OUTTIME  FROM ASPTBLHOSTELGATEPASS A where A.ASPTBLHOSTELGATEPASSID=" + dt0.Rows[0]["ID"].ToString()+ " GROUP BY A.OUTTIME";
                                DataSet ds1 = Utility.ExecuteSelectQuery(sel1, "ASPTBLHOSTELGATEPASS");
                                DataTable dt1 = ds1.Tables["ASPTBLHOSTELGATEPASS"];
                                Int64 cc1 = Convert.ToInt64("0" + dt1.Rows[0]["CNT"].ToString());
                                if (cc1 >= 1)
                                {
                                    MessageBox.Show("This Token-Number  Already  Process Completed       :" + dt0.Rows[0]["ID"].ToString(), "Information", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                    lblalert.Visible = false;
                                    txtintime.Text = ""; txtremarks.Text = "";
                                    txtintime.Select(); checkpassmissed.Checked = false;
                                }
                                else
                                {

                                    DateTime endtime = Convert.ToDateTime(System.DateTime.Now.ToString());
                                    DateTime statetime = DateTime.Parse(dt.Rows[0]["MANUALTIME"].ToString());
                                    var statetime1 = DateTime.Parse(dt.Rows[0]["OUTTIME"].ToString());
                                    TimeSpan differ = endtime.Subtract(statetime);
                                    TimeSpan differ1 = endtime - statetime1;
                                    string ins = "update  ASPTBLHOSTELGATEPASS set INTIME='" + System.DateTime.Now.ToString() + "',TIMEDIFF='" + differ1.ToString() + "',PASSMISSED='" + pmissed + "',REMARKS1='" + repass + "',IPADDRESS='" + Class.Users.IPADDRESS + "' WHERE ASPTBLHOSTELGATEPASSID=" + dt0.Rows[0]["id"].ToString();
                                    Utility.ExecuteNonQuery(ins);
                                    lblalert.Visible = true;
                                    lblalert.Text = "OutTime.." + dt.Rows[0]["OUTTIME"].ToString() + " PerMission Time" + dt.Rows[0]["MANUALTIME"].ToString();

                                    if (differ.Minutes < 0) { lblalert.Text = "OutTime    : " + dt.Rows[0]["OUTTIME"].ToString() + "  Permission Hrs   : " + dt.Rows[0]["PERMISSIONHRS"].ToString() + " Remaining Hrs    " + differ.ToString(); }
                                    else { lblalert.Text = "OutTime    : " + dt.Rows[0]["OUTTIME"].ToString() + "  Permission Hrs   : " + endtime.ToString() + " Delay Hrs    " + differ.ToString(); }
                                    checkpassmissed.Checked = false;
                                    //string sel2 = "SELECT A.EMPIMAGE FROM  ASPTBLEMP A JOIN   GTCOMPMAST B ON B.gtcompmastid = A.COMPCODE  join ASPTBLHOSTELGATEPASS D on D.IDCARDNO=A.IDCARDNO AND D.COMPCODE=B.GTCOMPMASTID WHERE D.ASPTBLHOSTELGATEPASSID=" + dt0.Rows[0]["id"].ToString();
                                    //DataSet ds2 = Utility.ExecuteSelectQuery(sel2, "ASPTBLEMP");
                                    //DataTable dt2 = ds2.Tables["ASPTBLEMP"];
                                    //if (dt2.Rows.Count > 0)
                                    //{
                                    //    bytes = (byte[])dt2.Rows[0]["EMPIMAGE"];
                                    //    Image img = Models.Device.ByteArrayToImage(bytes);
                                    //    pictureinwardimage.Image = img;
                                    //}
                                    //else
                                    //{
                                    //    pictureinwardimage.Image = Pinnacle.Properties.Resources.close_image1;
                                    //}
                                    MessageBox.Show("Record Updated Successfully     : " + txtintime.Text, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    txtintime.Text = ""; txtremarks.Text = ""; txtintime.Select();
                                }

                            }
                            else if (dt.Rows[0]["CNT"].ToString() == null)
                            {
                                MessageBox.Show("Invalid Token Number", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                txtintime.Text = ""; txtremarks.Text = ""; txtintime.Select(); checkpassmissed.Checked = false;

                            }
                            if (cc == 0)
                            {
                                MessageBox.Show("Pls go to Outward entry", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                txtintime.Text = ""; txtremarks.Text = ""; lblalert.Visible = false; txtintime.Select(); checkpassmissed.Checked = false;
                            }

                        }
                        return;
                    }
                    else
                    {
                        if (ss.Length == 8)
                        {
                            txtintime.Text = "";
                            txtintime.Text = ss;
                            string sel = "select COUNT(A.OUTTIME)AS CNT,A.EMPNAME,A.MANUALTIME,SUBSTR(A.PERMISSIONHRS,11,9) AS PERMISSIONHRS,A.OUTTIME FROM ASPTBLHOSTELGATEPASS A where A.ASPTBLHOSTELGATEPASSID=" + txtintime.Text + " GROUP BY A.EMPNAME,A.MANUALTIME,A.PERMISSIONHRS,A.OUTTIME";
                            DataSet ds = Utility.ExecuteSelectQuery(sel, "ASPTBLHOSTELGATEPASS");
                            DataTable dt = ds.Tables["ASPTBLHOSTELGATEPASS"];
                            Int64 cc = Convert.ToInt64(dt.Rows[0]["CNT"].ToString());
                            if (cc > 0)
                            {

                                string sel1 = "select COUNT(A.INTIME) as CNT  FROM ASPTBLHOSTELGATEPASS A where A.ASPTBLHOSTELGATEPASSID=" + txtintime.Text;
                                DataSet ds1 = Utility.ExecuteSelectQuery(sel1, "ASPTBLHOSTELGATEPASS");
                                DataTable dt1 = ds1.Tables["ASPTBLHOSTELGATEPASS"];
                                Int64 cc1 = Convert.ToInt64("0" + dt1.Rows[0]["CNT"].ToString());
                                if (cc1 >= 1)
                                {
                                    MessageBox.Show("This Token-Number  Already  Process Completed       :" + txtintime.Text, "Information", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                    lblalert.Visible = false;
                                    txtintime.Text = ""; txtremarks.Text = "";
                                    txtintime.Select(); checkpassmissed.Checked = false;
                                }
                                else
                                {
                                   
                                    DateTime endtime = Convert.ToDateTime(System.DateTime.Now.ToString());
                                    DateTime statetime = DateTime.Parse(dt.Rows[0]["MANUALTIME"].ToString());
                                    var statetime1 = DateTime.Parse(dt.Rows[0]["OUTTIME"].ToString());
                                    TimeSpan differ = endtime.Subtract(statetime);
                                    TimeSpan differ1 = endtime - statetime1;

                                    string ins = "update  ASPTBLHOSTELGATEPASS set INTIME='" + System.DateTime.Now.ToString() + "',TIMEDIFF='" + differ1.ToString() + "',REMARKS1='" + repass + "',IPADDRESS='" + Class.Users.IPADDRESS + "' WHERE ASPTBLHOSTELGATEPASSID=" + txtintime.Text;

                                    Utility.ExecuteNonQuery(ins);
                                    lblalert.Visible = true;
                                    lblalert.Text = "OutTime.." + dt.Rows[0]["MANUALTIME"].ToString() + " PerMission Time" + dt.Rows[0]["MANUALTIME"].ToString();

                                    if (differ.Minutes < 0) { lblalert.Text = "OutTime    : " + dt.Rows[0]["OUTTIME"].ToString() + "  Permission Hrs   : " + dt.Rows[0]["PERMISSIONHRS"].ToString() + " Remaining Hrs    " + differ.ToString(); }
                                    else { lblalert.Text = "OutTime    : " + dt.Rows[0]["OUTTIME"].ToString() + "  Permission Hrs   : " + endtime.ToString() + " Delay Hrs    " + differ.ToString(); }

                                    string sel2 = "SELECT A.EMPIMAGE FROM  ASPTBLEMP A JOIN   GTCOMPMAST B ON B.gtcompmastid = A.COMPCODE  join ASPTBLHOSTELGATEPASS D on D.IDCARDNO=A.IDCARDNO WHERE D.ASPTBLHOSTELGATEPASSID=" + txtintime.Text;
                                    DataSet ds2 = Utility.ExecuteSelectQuery(sel2, "ASPTBLEMP");
                                    DataTable dt2 = ds2.Tables["ASPTBLEMP"];
                                    if (dt2.Rows.Count > 0)
                                    {
                                        bytes = (byte[])dt2.Rows[0]["EMPIMAGE"];
                                        Image img = Models.Device.ByteArrayToImage(bytes);
                                        pictureinwardimage.Image = img;
                                    }
                                    else
                                    {
                                        pictureinwardimage.Image = Pinnacle.Properties.Resources.close_image1;
                                    }
                                   // pictureinwardimage.Image = Pinnacle.Properties.Resources.close_image1;
                                    MessageBox.Show("Record Updated Successfully     : " + txtintime.Text, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    txtintime.Text = ""; txtremarks.Text = ""; txtintime.Select(); checkpassmissed.Checked = false;
                                }

                            }
                            else if (dt.Rows[0]["CNT"].ToString() == null)
                            {
                                MessageBox.Show("Invalid Token Number", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                txtintime.Text = ""; txtremarks.Text = ""; txtintime.Select(); checkpassmissed.Checked = false;

                            }
                            if (cc == 0)
                            {
                                MessageBox.Show("Pls go to Outward entry", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                txtintime.Text = ""; txtremarks.Text = ""; lblalert.Visible = false; txtintime.Select(); checkpassmissed.Checked = false;
                            }


                        }
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid Token Number   ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtremarks.Text = "";
                txtintime.Text = ""; txtintime.Select();
                //string selchek1 = "select a.ASPTBLSESSIONMASID   from  ASPTBLSESSIONMAS a join gtcompmast  b on a.compcode=b.gtcompmastid join asptblusermas c on  c.compcode = a.compcode AND C.COMPCODE=B.GTCOMPMASTID  and A.USERNAME=C.USERID   and B.compcode='" + Class.Users.HCompcode + "'      and C.username='" + Class.Users.HUserName + "' and C.pasword = '" + Class.Users.PWORD + "' ";//and A.SYSTEMDATE = to_date('" + System.DateTime.Now.ToString("dd-MM-yyyy") + "', 'dd-MM-yyyy') and  C.active='T'
                //DataSet dschk1 = Utility.ExecuteSelectQuery(selchek1, "ASPTBLSESSIONMAS");
                //DataTable dtchk1 = dschk1.Tables["ASPTBLSESSIONMAS"];

                //if (dtchk1.Rows.Count > 0)
                //{
                //    Class.Users.SessionID = Convert.ToInt32(dtchk1.Rows[0]["ASPTBLSESSIONMASID"].ToString());
                //    string del = "delete from  ASPTBLSESSIONMAS a where a.ASPTBLSESSIONMASID='" + Class.Users.SessionID + "'";
                //    Utility.ExecuteNonQuery(del);

                //    Application.Exit();
                //}
               

            
            }
        }

        private void Butempout_Click(object sender, EventArgs e)
        {
            Class.Users.UserTime = 0; Class.Users.Intimation = "PAYROLL";

            try
            {
                if (txtouttime.Text == "")
                {
                    MessageBox.Show("Empty Not Allowed      " + txtouttime.Text, "Information", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                    return;
                }
                else
                {
                    var qr = txtouttime.Text;
                    string[] qrdata = qr.Split(':', ',');
                    string ss = qrdata[1].Trim();
                    pictureoutimage.Image = null;
                    if (Class.Users.HCompcode == "AGF" || Class.Users.HCompcode == "VEL" || Class.Users.HCompcode == "FLFD" || Class.Users.HCompcode == "AGFM" || Class.Users.HCompcode == "AGFMGII")
                    {
                      
                        if (ss.Length == 8)
                        {
                            txtouttime.Text = "";
                            txtouttime.Text = ss;
                            string sel = "select COUNT(A.OUTTIME) AS CNT ,B.FNAME AS EMPNAME,A.MANUALTIME,A.PERMISSIONHRS,A.MODIFIED   FROM ASPTBLHOSTELGATEPASS A    JOIN HREMPLOYMAST B ON B.HREMPLOYMASTID=A.EMPNAME  where A.ASPTBLHOSTELGATEPASSID=" + txtouttime.Text + " and to_Date('" + System.DateTime.Now.ToString("dd-MM-yyyy") + "','dd-MM-yyyy') <= a.modified  GROUP BY B.FNAME,A.MANUALTIME,A.PERMISSIONHRS,A.MODIFIED";
                            DataSet ds = Utility.ExecuteSelectQuery(sel, "ASPTBLHOSTELGATEPASS");
                            DataTable dt = ds.Tables["ASPTBLHOSTELGATEPASS"];
                            if (Convert.ToString(dt.Rows.Count) == null || dt.Rows.Count <= 0)
                            {
                                MessageBox.Show("Invalid", "Invalid Date    '"+dt.Rows[0]["MODIFIED"].ToString()+"'", MessageBoxButtons.OK, MessageBoxIcon.Error); txtouttime.Text = ""; txtouttime.Select();
                            }
                            else
                            {
                                Int64 cc = Convert.ToInt64(dt.Rows[0]["CNT"].ToString());
                                if (cc > 0)
                                {
                                    MessageBox.Show("This Token-Number  Already  Process Completed       :" + txtouttime.Text, "Information", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                    txtouttime.Text = ""; label32.Visible = false; txtouttime.Select();
                                }
                                else
                                {
                                    string ins = "UPDATE ASPTBLHOSTELGATEPASS set OUTTIME='" + System.DateTime.Now.ToString() + "' , username='" + Class.Users.USERID + "' ,IPADDRESS='" + Class.Users.IPADDRESS + "' WHERE ASPTBLHOSTELGATEPASSID=" + txtouttime.Text;
                                    Utility.ExecuteNonQuery(ins);

                                    //var endtime = TimeSpan.Parse(DateTime.Now.ToString("HH:mm"));
                                    //var statetime = TimeSpan.Parse(dt.Rows[0]["MANUALTIME"].ToString()) + TimeSpan.Parse(dt.Rows[0]["PERMISSIONHRS"].ToString());
                                    //TimeSpan differ = endtime - statetime;
                                    //if (differ.Minutes < 0) { label32.Text = "OutTime : " + dt.Rows[0]["MANUALTIME"].ToString() + "  Per-Time   : " + dt.Rows[0]["PERMISSIONHRS"].ToString() + "Remaining Hrs" + differ.ToString(); }
                                    //else { label32.Text = "OutTime: " + dt.Rows[0]["MANUALTIME"].ToString() + "  Per-Time   : " + dt.Rows[0]["PERMISSIONHRS"].ToString() + "\n" + "Delay Hrs    " + differ.ToString(); }
                                    MessageBox.Show("Record Updated Successfully     : " + txtouttime.Text, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    label32.Visible = true; txtouttime.Text = "";
                                    txtouttime.Select();
                                    //  pictureoutimage.Image = Pinnacle.Properties.Resources.close_image1;
                                    //string sel2 = "select d.imagefieldvalue AS EMPIMAGE from hremploymast a  join hremploydetails b on a.hremploymastid=b.hremploymastid join gtcompmast c on c.gtcompmastid=a.compcode left join hremploymastimage d on d.hremploymastid=a.hremploymastid  where b.hostel='YES' and c.compcode='" + Class.Users.HCompcode + "'  and  d.hremploymastid='" + dt.Rows[0]["EMPNAME"].ToString() + "'";
                                    //DataSet ds2 = Utility.ExecuteSelectQuery(sel2, "hremploymast");
                                    //DataTable dt2 = ds2.Tables["hremploymast"];
                                    //if (dt2 != null)
                                    //{
                                    //    bytes = (byte[])dt2.Rows[0]["EMPIMAGE"];
                                    //    Image img = Models.Device.ByteArrayToImage(bytes);
                                    //    pictureoutimage.Image = img;


                                    //}
                                    //else
                                    //{
                                    //    pictureoutimage.Image = Pinnacle.Properties.Resources.close_image1;
                                    //}

                                }


                            }
                        }
                        return;
                    }
                 
                    else
                    {
                        if (ss.Length == 8)
                        {
                            txtouttime.Text = "";
                            txtouttime.Text = ss;
                            string sel = "select COUNT(A.OUTTIME)AS CNT,A.EMPNAME,A.MANUALTIME,SUBSTR(A.PERMISSIONHRS,11,9) AS PERMISSIONHRS,A.MODIFIED FROM ASPTBLHOSTELGATEPASS A where A.ASPTBLHOSTELGATEPASSID=" + txtouttime.Text + "  and to_Date('" + System.DateTime.Now.ToString("dd-MM-yyyy") + "','dd-MM-yyyy') <= a.modified GROUP BY A.EMPNAME,A.MANUALTIME,A.PERMISSIONHRS,A.MODIFIED";
                            DataSet ds = Utility.ExecuteSelectQuery(sel, "ASPTBLHOSTELGATEPASS");
                            DataTable dt = ds.Tables["ASPTBLHOSTELGATEPASS"];
                            if (Convert.ToString(dt.Rows.Count) == null || dt.Rows.Count <= 0)
                            {
                                MessageBox.Show("invlaid", "Invalid Date    '" + dt.Rows[0]["MODIFIED"].ToString() + "'", MessageBoxButtons.OK, MessageBoxIcon.Error); txtouttime.Text = ""; txtouttime.Select();
                            }
                            else
                            {
                                Int64 cc = Convert.ToInt64(dt.Rows[0]["CNT"].ToString());
                                if (cc > 0)
                                {
                                    MessageBox.Show("This Token-Number  Already  Process Completed       :" + txtouttime.Text, "Information", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                    txtouttime.Text = ""; label32.Visible = false; txtouttime.Select();
                                }
                                else
                                {
                                    string ins = "UPDATE ASPTBLHOSTELGATEPASS set OUTTIME='" + System.DateTime.Now.ToString() + "' ,IPADDRESS='" + Class.Users.IPADDRESS + "' WHERE ASPTBLHOSTELGATEPASSID=" + txtouttime.Text;
                                    Utility.ExecuteNonQuery(ins);

                                    DateTime endtime = Convert.ToDateTime(System.DateTime.Now.ToString());
                                    DateTime statetime = DateTime.Parse(dt.Rows[0]["MANUALTIME"].ToString());
                                    var statetime1 = DateTime.Parse(dt.Rows[0]["MANUALTIME"].ToString());
                                    TimeSpan differ = endtime.Subtract(statetime);

                                   // var endtime = TimeSpan.Parse(DateTime.Now.ToString("HH:mm:ss"));
                                  //  var statetime = TimeSpan.Parse(dt.Rows[0]["MANUALTIME"].ToString()) + TimeSpan.Parse(dt.Rows[0]["PERMISSIONHRS"].ToString());
                                  //  TimeSpan differ = endtime - statetime;

                                    if (differ.Minutes < 0) { label32.Text = "OutTime :  " + dt.Rows[0]["MANUALTIME"].ToString() + "  Per-Time   : " + dt.Rows[0]["PERMISSIONHRS"].ToString() + "    Remaining Hrs   :" + differ.ToString(); }
                                    else { label32.Text = "OutTime: " + dt.Rows[0]["MANUALTIME"].ToString() + "  Per-Time    : " + dt.Rows[0]["PERMISSIONHRS"].ToString() + "\n" + "     Delay Hrs   : " + differ.ToString(); }
                                    MessageBox.Show("Record Updated Successfully     : " + txtouttime.Text, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    label32.Visible = true;
                                  
                                    string sel2 = "SELECT A.EMPIMAGE FROM  ASPTBLEMP A JOIN   GTCOMPMAST B ON B.gtcompmastid = A.COMPCODE  join ASPTBLHOSTELGATEPASS D on D.IDCARDNO=A.IDCARDNO WHERE D.ASPTBLHOSTELGATEPASSID=" + txtouttime.Text;
                                    DataSet ds2 = Utility.ExecuteSelectQuery(sel2, "ASPTBLEMP");
                                    DataTable dt2 = ds2.Tables["ASPTBLEMP"];
                                    if (dt2.Rows.Count > 0)
                                    {
                                        bytes = (byte[])dt2.Rows[0]["EMPIMAGE"];
                                        Image img = Models.Device.ByteArrayToImage(bytes);
                                        pictureoutimage.Image = img;
                                    }
                                    else
                                    {
                                        pictureoutimage.Image = Pinnacle.Properties.Resources.close_image1;
                                    }

                                     txtouttime.Text = "";
                                    txtouttime.Select();
                                    //  pictureoutimage.Image = Pinnacle.Properties.Resources.close_image1;
                                    //string sel2 = "select d.imagefieldvalue AS EMPIMAGE from hremploymast a  join hremploydetails b on a.hremploymastid=b.hremploymastid join gtcompmast c on c.gtcompmastid=a.compcode left join hremploymastimage d on d.hremploymastid=a.hremploymastid  where b.hostel='YES' and c.compcode='" + Class.Users.HCompcode + "'  and  d.hremploymastid='" + dt.Rows[0]["EMPNAME"].ToString() + "'";
                                    //DataSet ds2 = Utility.ExecuteSelectQuery(sel2, "hremploymast");
                                    //DataTable dt2 = ds2.Tables["hremploymast"];
                                    //if (dt2 != null)
                                    //{
                                    //    bytes = (byte[])dt2.Rows[0]["EMPIMAGE"];
                                    //    Image img = Models.Device.ByteArrayToImage(bytes);
                                    //    pictureoutimage.Image = img;


                                    //}
                                    //else
                                    //{
                                    //    pictureoutimage.Image = Pinnacle.Properties.Resources.close_image1;
                                    //}

                                }


                            }
                        }
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid Token Number", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtouttime.Text = ""; Cursor = Cursors.Default; label32.Visible = false; txtouttime.Select();
                Class.Users.UserTime = 0;
                //string selchek1 = "select a.ASPTBLSESSIONMASID   from  ASPTBLSESSIONMAS a join gtcompmast  b on a.compcode=b.gtcompmastid join asptblusermas c on  c.compcode = a.compcode AND C.COMPCODE=B.GTCOMPMASTID  and A.USERNAME=C.USERID   and B.compcode='" + Class.Users.HCompcode + "'      and C.username='" + Class.Users.HUserName + "' and C.pasword = '" + Class.Users.PWORD + "' ";//and A.SYSTEMDATE = to_date('" + System.DateTime.Now.ToString("dd-MM-yyyy") + "', 'dd-MM-yyyy') and  C.active='T'
                //DataSet dschk1 = Utility.ExecuteSelectQuery(selchek1, "ASPTBLSESSIONMAS");
                //DataTable dtchk1 = dschk1.Tables["ASPTBLSESSIONMAS"];

                //if (dtchk1.Rows.Count > 0)
                //{
                //    Class.Users.SessionID = Convert.ToInt32(dtchk1.Rows[0]["ASPTBLSESSIONMASID"].ToString());
                //    string del = "delete from  ASPTBLSESSIONMAS a where a.ASPTBLSESSIONMASID='" + Class.Users.SessionID + "'";
                //    Utility.ExecuteNonQuery(del);

                //    Application.Exit();
                //}

            
            }
        }

        private void Btnempinwardclear_Click(object sender, EventArgs e)
        {
            pictureinwardimage.Image = null;
            txtintime.Text = ""; txtremarks.Text = "";
            tabControl1.TabPages.Remove(tabGOODSIN);
            tabControl1.TabPages.Remove(tabGOODSOut);
            tabControl1.TabPages.Remove(tabEMPIn);
            tabControl1.SelectTab(tabEMPInOut);
            lblalert.Visible = false; Class.Users.Intimation = "PAYROLL";
            bytes = null; Class.Users.UserTime = 0;
        }
        public void autonoIN()
        {
            if (txt_qrcodein.Text != "")
            {
                try
                {
                    if (Class.Users.HCompcode == "FLFD" && txtcategoryin.Text == "IN")
                    {
                        string sel1 = "SELECT  MAX(TO_NUMBER(A.FLFDIN))+1  ID  FROM  ASPINVENTRY A JOIN GTCOMPMAST B ON A.COMPCODE=B.GTCOMPMASTID   JOIN gtfinancialyear C ON C.GTFINANCIALYEARID=A.FINYEAR and C.CURRENTFINYR = 'T'   JOIN ASPTBLINOUTMAS D ON D.COMPCODE=A.COMPCODE AND  D.COMPCODE=B.GTCOMPMASTID   and A.FINYEAR=C.GTFINANCIALYEARID AND D.FINYEAR=A.FINYEAR   AND   D.ACTIVE='T'  WHERE C.FINYR='" + Class.Users.Finyear + "' and A.COMPCODE='" + Class.Users.COMPCODE + "' AND  A.CREATEDON >=  SUBSTR(D.CREATEDON,0,10)";

                        DataSet ds = Utility.ExecuteSelectQuery(sel1, "ASPINVENTRY");
                        DataTable dt = ds.Tables["ASPINVENTRY"];
                        Int64 cnt = Convert.ToInt64("0" + dt.Rows[0]["ID"].ToString());
                        if (cnt == 0)
                        {
                            string sel2 = "  SELECT MAX(TO_NUMBER(A.INWARDNO)) INWARDNO    FROM  ASPTBLINOUTMAS A JOIN GTCOMPMAST B ON A.COMPCODE=B.GTCOMPMASTID JOIN gtfinancialyear C ON C.GTFINANCIALYEARID=A.FINYEAR  and C.CURRENTFINYR='T'  AND  A.ACTIVE='T' AND  A.FLFDIN='T'  WHERE A.FINYEAR='" + combofinyearIN.SelectedValue + "' AND A.COMPCODE='" + Class.Users.COMPCODE + "'";
                            DataSet ds2 = Utility.ExecuteSelectQuery(sel2, "ASPTBLINOUTMAS");
                            DataTable dt2 = ds2.Tables["ASPTBLINOUTMAS"];
                            txtgatedcno1.Text = dt2.Rows[0]["INWARDNO"].ToString();
                            si.FLFDIN = Convert.ToString(txtgatedcno1.Text);
                            si.GateDcNo = Class.Users.Finyear + "/" + Class.Users.HCompcode + "/" + txtgatedcno1.Text;
                            return;
                        }
                        else
                        {

                            txtgatedcno1.Text = Convert.ToInt64("0" + dt.Rows[0]["ID"].ToString()).ToString();
                            si.FLFDIN = Convert.ToString(txtgatedcno1.Text);
                            si.GateDcNo = Class.Users.Finyear + "/" + Class.Users.HCompcode + "/" + txtgatedcno1.Text;
                            btnsavesIN.Focus();
                            return;
                        }
                    }

                    if (Class.Users.HCompcode == "VEL" && txtcategoryin.Text == "IN")
                    {
                        string sel1 = "SELECT  MAX(TO_NUMBER(A.VELIN))+1  ID  FROM  ASPINVENTRY A JOIN GTCOMPMAST B ON A.COMPCODE=B.GTCOMPMASTID   JOIN gtfinancialyear C ON C.GTFINANCIALYEARID=A.FINYEAR and C.CURRENTFINYR = 'T'   JOIN ASPTBLINOUTMAS D ON D.COMPCODE=A.COMPCODE AND  D.COMPCODE=B.GTCOMPMASTID   and A.FINYEAR=C.GTFINANCIALYEARID AND D.FINYEAR=A.FINYEAR   AND   D.ACTIVE='T'  WHERE C.FINYR='" + Class.Users.Finyear + "' and A.COMPCODE='" + Class.Users.COMPCODE + "' AND  A.CREATEDON >=  SUBSTR(D.CREATEDON,0,10)";

                     //   string sel1 = "SELECT MAX(TO_NUMBER(A.VELIN))+1 ID FROM  ASPINVENTRY A JOIN GTCOMPMAST B ON A.COMPCODE=B.GTCOMPMASTID JOIN gtfinancialyear C ON C.GTFINANCIALYEARID=A.FINYEAR  and C.CURRENTFINYR='T'  WHERE A.COMPCODE=" + Class.Users.COMPCODE;
                        DataSet ds = Utility.ExecuteSelectQuery(sel1, "ASPINVENTRY");
                        DataTable dt = ds.Tables["ASPINVENTRY"];
                        Int64 cnt = Convert.ToInt64("0" + dt.Rows[0]["ID"].ToString());
                        if (cnt == 0)
                        {
                            string sel2 = "  SELECT MAX(TO_NUMBER(A.INWARDNO)) as  INWARDNO   FROM  ASPTBLINOUTMAS A JOIN GTCOMPMAST B ON A.COMPCODE=B.GTCOMPMASTID JOIN gtfinancialyear C ON C.GTFINANCIALYEARID=A.FINYEAR  and C.CURRENTFINYR='T'  AND  A.ACTIVE='T' AND  A.VELIN='T'  WHERE A.FINYEAR='" + combofinyearIN.SelectedValue + "' AND A.COMPCODE='" + Class.Users.COMPCODE + "'";
                            DataSet ds2 = Utility.ExecuteSelectQuery(sel2, "ASPTBLINOUTMAS");
                            DataTable dt2 = ds2.Tables["ASPTBLINOUTMAS"];
                            txtgatedcno1.Text = dt2.Rows[0]["INWARDNO"].ToString();
                            si.VELIN = Convert.ToString(txtgatedcno1.Text);
                            si.GateDcNo = Class.Users.Finyear + "/" + Class.Users.HCompcode + "/" + txtgatedcno1.Text; return;
                        }
                        else
                        {

                            txtgatedcno1.Text = Convert.ToInt64("0" + dt.Rows[0]["ID"].ToString()).ToString();
                            si.VELIN = Convert.ToString(txtgatedcno1.Text);
                            si.GateDcNo = Class.Users.Finyear + "/" + Class.Users.HCompcode + "/" + txtgatedcno1.Text; return;
                        }
                    }
                    if (Class.Users.HCompcode == "AGF" && txtcategoryin.Text == "IN")
                    {
                        string sel1 = "SELECT  MAX(TO_NUMBER(A.AGFIN))+1  ID  FROM  ASPINVENTRY A JOIN GTCOMPMAST B ON A.COMPCODE=B.GTCOMPMASTID   JOIN gtfinancialyear C ON C.GTFINANCIALYEARID=A.FINYEAR and C.CURRENTFINYR = 'T'   JOIN ASPTBLINOUTMAS D ON D.COMPCODE=A.COMPCODE AND  D.COMPCODE=B.GTCOMPMASTID   and A.FINYEAR=C.GTFINANCIALYEARID AND D.FINYEAR=A.FINYEAR   AND   D.ACTIVE='T'  WHERE C.FINYR='" + Class.Users.Finyear + "' and A.COMPCODE='" + Class.Users.COMPCODE + "' AND  A.CREATEDON >=  SUBSTR(D.CREATEDON,0,10)";

                       // string sel1 = "SELECT MAX(TO_NUMBER(A.AGFIN))+1 ID FROM  ASPINVENTRY A JOIN GTCOMPMAST B ON A.COMPCODE=B.GTCOMPMASTID JOIN gtfinancialyear C ON C.GTFINANCIALYEARID=A.FINYEAR  and C.CURRENTFINYR='T'  WHERE A.COMPCODE=" + Class.Users.COMPCODE;
                        DataSet ds = Utility.ExecuteSelectQuery(sel1, "ASPINVENTRY");
                        DataTable dt = ds.Tables["ASPINVENTRY"];
                        Int64 cnt = Convert.ToInt64("0" + dt.Rows[0]["ID"].ToString());
                        if (cnt == 0)
                        {
                            string sel2 = " SELECT MAX(TO_NUMBER(A.INWARDNO)) as INWARDNO   FROM  ASPTBLINOUTMAS A JOIN GTCOMPMAST B ON A.COMPCODE=B.GTCOMPMASTID JOIN gtfinancialyear C ON C.GTFINANCIALYEARID=A.FINYEAR  and C.CURRENTFINYR='T'  AND  A.ACTIVE='T' AND  A.AGFIN='T' WHERE A.FINYEAR='" + combofinyearIN.SelectedValue + "' AND A.COMPCODE='" + Class.Users.COMPCODE + "' ";
                            DataSet ds2 = Utility.ExecuteSelectQuery(sel2, "ASPTBLINOUTMAS");
                            DataTable dt2 = ds2.Tables["ASPTBLINOUTMAS"];
                            txtgatedcno1.Text = dt2.Rows[0]["INWARDNO"].ToString();
                            si.AGFIN = Convert.ToString(txtgatedcno1.Text);
                            si.GateDcNo = Class.Users.Finyear + "/" + Class.Users.HCompcode + "/" + txtgatedcno1.Text;
                            return;
                        }
                        else
                        {

                            txtgatedcno1.Text = Convert.ToInt64("0" + dt.Rows[0]["ID"].ToString()).ToString();
                            si.AGFIN = Convert.ToString(txtgatedcno1.Text);
                            si.GateDcNo = Class.Users.Finyear + "/" + Class.Users.HCompcode + "/" + txtgatedcno1.Text; return;
                        }
                    }
                    if (Class.Users.HCompcode == "AGFC" && txtcategoryin.Text == "IN")
                    {
                        string sel1 = "SELECT  MAX(TO_NUMBER(A.AGFCIN))+1  ID  FROM  ASPINVENTRY A JOIN GTCOMPMAST B ON A.COMPCODE=B.GTCOMPMASTID   JOIN gtfinancialyear C ON C.GTFINANCIALYEARID=A.FINYEAR and C.CURRENTFINYR = 'T'   JOIN ASPTBLINOUTMAS D ON D.COMPCODE=A.COMPCODE AND  D.COMPCODE=B.GTCOMPMASTID   and A.FINYEAR=C.GTFINANCIALYEARID AND D.FINYEAR=A.FINYEAR   AND   D.ACTIVE='T'  WHERE C.FINYR='" + Class.Users.Finyear + "' and A.COMPCODE='" + Class.Users.COMPCODE + "' AND  A.CREATEDON >=  SUBSTR(D.CREATEDON,0,10)";

                        // string sel1 = "SELECT MAX(TO_NUMBER(A.AGFCIN))+1 ID FROM  ASPINVENTRY A JOIN GTCOMPMAST B ON A.COMPCODE=B.GTCOMPMASTID JOIN gtfinancialyear C ON C.GTFINANCIALYEARID=A.FINYEAR  and C.CURRENTFINYR='T'  WHERE A.COMPCODE=" + Class.Users.COMPCODE;
                        DataSet ds = Utility.ExecuteSelectQuery(sel1, "ASPINVENTRY");
                        DataTable dt = ds.Tables["ASPINVENTRY"];
                        Int64 cnt = Convert.ToInt64("0" + dt.Rows[0]["ID"].ToString());
                        if (cnt == 0)
                        {
                            string sel2 = " SELECT MAX(TO_NUMBER(A.INWARDNO)) as  INWARDNO   FROM  ASPTBLINOUTMAS A JOIN GTCOMPMAST B ON A.COMPCODE=B.GTCOMPMASTID JOIN gtfinancialyear C ON C.GTFINANCIALYEARID=A.FINYEAR  and C.CURRENTFINYR='T'  AND  A.ACTIVE='T' AND  A.AGFCIN='T' WHERE A.FINYEAR='" + combofinyearIN.SelectedValue + "' AND A.COMPCODE='" + Class.Users.COMPCODE + "' ";
                            DataSet ds2 = Utility.ExecuteSelectQuery(sel2, "ASPTBLINOUTMAS");
                            DataTable dt2 = ds2.Tables["ASPTBLINOUTMAS"];
                            txtgatedcno1.Text = dt2.Rows[0]["INWARDNO"].ToString();
                            si.AGFCIN = Convert.ToString(txtgatedcno1.Text);
                            si.GateDcNo = Class.Users.Finyear + "/" + Class.Users.HCompcode + "/" + txtgatedcno1.Text;
                            return;
                        }
                        else
                        {

                            txtgatedcno1.Text = Convert.ToInt64("0" + dt.Rows[0]["ID"].ToString()).ToString();
                            si.AGFCIN = Convert.ToString(txtgatedcno1.Text);
                            si.GateDcNo = Class.Users.Finyear + "/" + Class.Users.HCompcode + "/" + txtgatedcno1.Text; return;
                        }
                    }
                    if (Class.Users.HCompcode == "AGFK" && txtcategoryin.Text == "IN")
                    {
                        string sel1 = "SELECT  MAX(TO_NUMBER(A.AGFKIN))+1  ID  FROM  ASPINVENTRY A JOIN GTCOMPMAST B ON A.COMPCODE=B.GTCOMPMASTID   JOIN gtfinancialyear C ON C.GTFINANCIALYEARID=A.FINYEAR and C.CURRENTFINYR = 'T'   JOIN ASPTBLINOUTMAS D ON D.COMPCODE=A.COMPCODE AND  D.COMPCODE=B.GTCOMPMASTID   and A.FINYEAR=C.GTFINANCIALYEARID AND D.FINYEAR=A.FINYEAR   AND   D.ACTIVE='T'  WHERE C.FINYR='" + Class.Users.Finyear + "' and A.COMPCODE='" + Class.Users.COMPCODE + "' AND  A.CREATEDON >=  SUBSTR(D.CREATEDON,0,10)";

                        //string sel1 = "SELECT MAX(TO_NUMBER(A.AGFKIN))+1 ID FROM  ASPINVENTRY A JOIN GTCOMPMAST B ON A.COMPCODE=B.GTCOMPMASTID JOIN gtfinancialyear C ON C.GTFINANCIALYEARID=A.FINYEAR  and C.CURRENTFINYR='T'  WHERE A.COMPCODE=" + Class.Users.COMPCODE;
                        DataSet ds = Utility.ExecuteSelectQuery(sel1, "ASPINVENTRY");
                        DataTable dt = ds.Tables["ASPINVENTRY"];
                        Int64 cnt = Convert.ToInt64("0" + dt.Rows[0]["ID"].ToString());
                        if (cnt == 0)
                        {
                            string sel2 = " SELECT MAX(TO_NUMBER(A.INWARDNO)) as  INWARDNO   FROM  ASPTBLINOUTMAS A JOIN GTCOMPMAST B ON A.COMPCODE=B.GTCOMPMASTID JOIN gtfinancialyear C ON C.GTFINANCIALYEARID=A.FINYEAR  and C.CURRENTFINYR='T'  AND  A.ACTIVE='T' AND  A.AGFKIN='T' WHERE A.FINYEAR='" + combofinyearIN.SelectedValue + "' AND A.COMPCODE='" + Class.Users.COMPCODE + "' ";
                            DataSet ds2 = Utility.ExecuteSelectQuery(sel2, "ASPTBLINOUTMAS");
                            DataTable dt2 = ds2.Tables["ASPTBLINOUTMAS"];
                            txtgatedcno1.Text = dt2.Rows[0]["INWARDNO"].ToString();
                            si.AGFKIN = Convert.ToString(txtgatedcno1.Text);
                            si.GateDcNo = Class.Users.Finyear + "/" + Class.Users.HCompcode + "/" + txtgatedcno1.Text;
                            return;
                        }
                        else
                        {

                            txtgatedcno1.Text = Convert.ToInt64("0" + dt.Rows[0]["ID"].ToString()).ToString();
                            si.AGFKIN = Convert.ToString(txtgatedcno1.Text);
                            si.GateDcNo = Class.Users.Finyear + "/" + Class.Users.HCompcode + "/" + txtgatedcno1.Text; return;
                        }
                    }
                    if (Class.Users.HCompcode == "AGFP" && txtcategoryin.Text == "IN")
                    {
                        string sel1 = "SELECT  MAX(TO_NUMBER(A.AGFPIN))+1  ID  FROM  ASPINVENTRY A JOIN GTCOMPMAST B ON A.COMPCODE=B.GTCOMPMASTID   JOIN gtfinancialyear C ON C.GTFINANCIALYEARID=A.FINYEAR and C.CURRENTFINYR = 'T'   JOIN ASPTBLINOUTMAS D ON D.COMPCODE=A.COMPCODE AND  D.COMPCODE=B.GTCOMPMASTID   and A.FINYEAR=C.GTFINANCIALYEARID AND D.FINYEAR=A.FINYEAR   AND   D.ACTIVE='T'  WHERE C.FINYR='" + Class.Users.Finyear + "' and A.COMPCODE='" + Class.Users.COMPCODE + "' AND  A.CREATEDON >=  SUBSTR(D.CREATEDON,0,10)";

                        // string sel1 = "SELECT MAX(TO_NUMBER(A.AGFPIN))+1 ID FROM  ASPINVENTRY A JOIN GTCOMPMAST B ON A.COMPCODE=B.GTCOMPMASTID JOIN gtfinancialyear C ON C.GTFINANCIALYEARID=A.FINYEAR  and C.CURRENTFINYR='T'  WHERE A.COMPCODE=" + Class.Users.COMPCODE;
                        DataSet ds = Utility.ExecuteSelectQuery(sel1, "ASPINVENTRY");
                        DataTable dt = ds.Tables["ASPINVENTRY"];
                        Int64 cnt = Convert.ToInt64("0" + dt.Rows[0]["ID"].ToString());
                        if (cnt == 0)
                        {
                            string sel2 = " SELECT MAX(TO_NUMBER(A.INWARDNO)) as INWARDNO   FROM  ASPTBLINOUTMAS A JOIN GTCOMPMAST B ON A.COMPCODE=B.GTCOMPMASTID JOIN gtfinancialyear C ON C.GTFINANCIALYEARID=A.FINYEAR  and C.CURRENTFINYR='T'  AND  A.ACTIVE='T' AND  A.AGFPIN='T' WHERE A.FINYEAR='" + combofinyearIN.SelectedValue + "' AND A.COMPCODE='" + Class.Users.COMPCODE + "' ";
                            DataSet ds2 = Utility.ExecuteSelectQuery(sel2, "ASPTBLINOUTMAS");
                            DataTable dt2 = ds2.Tables["ASPTBLINOUTMAS"];
                            txtgatedcno1.Text = dt2.Rows[0]["INWARDNO"].ToString();
                            si.AGFPIN = Convert.ToString(txtgatedcno1.Text);
                            si.GateDcNo = Class.Users.Finyear + "/" + Class.Users.HCompcode + "/" + txtgatedcno1.Text;
                            return;
                        }
                        else
                        {

                            txtgatedcno1.Text = Convert.ToInt64("0" + dt.Rows[0]["ID"].ToString()).ToString();
                            si.AGFPIN = Convert.ToString(txtgatedcno1.Text);
                            si.GateDcNo = Class.Users.Finyear + "/" + Class.Users.HCompcode + "/" + txtgatedcno1.Text; return;
                        }
                    }
                    if (Class.Users.HCompcode == "AGFMGII" && txtcategoryin.Text == "IN")
                    {
                        string sel1 = "SELECT  MAX(TO_NUMBER(A.AGFMGIIIN))+1  ID  FROM  ASPINVENTRY A JOIN GTCOMPMAST B ON A.COMPCODE=B.GTCOMPMASTID   JOIN gtfinancialyear C ON C.GTFINANCIALYEARID=A.FINYEAR and C.CURRENTFINYR = 'T'   JOIN ASPTBLINOUTMAS D ON D.COMPCODE=A.COMPCODE AND  D.COMPCODE=B.GTCOMPMASTID   and A.FINYEAR=C.GTFINANCIALYEARID AND D.FINYEAR=A.FINYEAR   AND   D.ACTIVE='T'  WHERE C.FINYR='" + Class.Users.Finyear + "' and A.COMPCODE='" + Class.Users.COMPCODE + "' AND  A.CREATEDON >=  SUBSTR(D.CREATEDON,0,10)";

                        //string sel1 = "SELECT MAX(TO_NUMBER(A.AGFMGIIIN))+1 ID FROM  ASPINVENTRY A JOIN GTCOMPMAST B ON A.COMPCODE=B.GTCOMPMASTID JOIN gtfinancialyear C ON C.GTFINANCIALYEARID=A.FINYEAR  and C.CURRENTFINYR='T'  WHERE A.COMPCODE=" + Class.Users.COMPCODE;
                        DataSet ds = Utility.ExecuteSelectQuery(sel1, "ASPINVENTRY");
                        DataTable dt = ds.Tables["ASPINVENTRY"];
                        Int64 cnt = Convert.ToInt64("0" + dt.Rows[0]["ID"].ToString());
                        if (cnt == 0)
                        {
                            string sel2 = " SELECT MAX(TO_NUMBER(A.INWARDNO)) as INWARDNO   FROM  ASPTBLINOUTMAS A JOIN GTCOMPMAST B ON A.COMPCODE=B.GTCOMPMASTID JOIN gtfinancialyear C ON C.GTFINANCIALYEARID=A.FINYEAR  and C.CURRENTFINYR='T'  AND  A.ACTIVE='T' AND  A.AGFMGIIIN='T' WHERE A.FINYEAR='" + combofinyearIN.SelectedValue + "' AND A.COMPCODE='" + Class.Users.COMPCODE + "' ";
                            DataSet ds2 = Utility.ExecuteSelectQuery(sel2, "ASPTBLINOUTMAS");
                            DataTable dt2 = ds2.Tables["ASPTBLINOUTMAS"];
                            txtgatedcno1.Text = dt2.Rows[0]["INWARDNO"].ToString();
                            si.AGFMGIIIN = Convert.ToString(txtgatedcno1.Text);
                            si.GateDcNo = Class.Users.Finyear + "/" + Class.Users.HCompcode + "/" + txtgatedcno1.Text;
                            return;
                        }
                        else
                        {

                            txtgatedcno1.Text = Convert.ToInt64("0" + dt.Rows[0]["ID"].ToString()).ToString();
                            si.AGFMGIIIN = Convert.ToString(txtgatedcno1.Text);
                            si.GateDcNo = Class.Users.Finyear + "/" + Class.Users.HCompcode + "/" + txtgatedcno1.Text; return;
                        }
                    }
                    if (Class.Users.HCompcode == "AGFM" && txtcategoryin.Text == "IN")
                    {
                        string sel1 = "SELECT  MAX(TO_NUMBER(A.AGFMIN))+1  ID  FROM  ASPINVENTRY A JOIN GTCOMPMAST B ON A.COMPCODE=B.GTCOMPMASTID   JOIN gtfinancialyear C ON C.GTFINANCIALYEARID=A.FINYEAR and C.CURRENTFINYR = 'T'   JOIN ASPTBLINOUTMAS D ON D.COMPCODE=A.COMPCODE AND  D.COMPCODE=B.GTCOMPMASTID   and A.FINYEAR=C.GTFINANCIALYEARID AND D.FINYEAR=A.FINYEAR   AND   D.ACTIVE='T'  WHERE C.FINYR='" + Class.Users.Finyear + "' and A.COMPCODE='" + Class.Users.COMPCODE + "' AND  A.CREATEDON >=  SUBSTR(D.CREATEDON,0,10)";

                        // string sel1 = "SELECT MAX(TO_NUMBER(A.AGFMIN))+1 ID FROM  ASPINVENTRY A JOIN GTCOMPMAST B ON A.COMPCODE=B.GTCOMPMASTID JOIN gtfinancialyear C ON C.GTFINANCIALYEARID=A.FINYEAR  and C.CURRENTFINYR='T'  WHERE A.COMPCODE=" + Class.Users.COMPCODE;
                        DataSet ds = Utility.ExecuteSelectQuery(sel1, "ASPINVENTRY");
                        DataTable dt = ds.Tables["ASPINVENTRY"];
                        Int64 cnt = Convert.ToInt64("0" + dt.Rows[0]["ID"].ToString());
                        if (cnt == 0)
                        {
                            string sel2 = "  SELECT MAX(TO_NUMBER(A.INWARDNO)) as INWARDNO   FROM  ASPTBLINOUTMAS A JOIN GTCOMPMAST B ON A.COMPCODE=B.GTCOMPMASTID JOIN gtfinancialyear C ON C.GTFINANCIALYEARID=A.FINYEAR  and C.CURRENTFINYR='T' AND  A.ACTIVE='T' AND  A.AGFMIN='T' WHERE A.FINYEAR='" + combofinyearIN.SelectedValue + "' AND A.COMPCODE='" + Class.Users.COMPCODE + "'";
                            DataSet ds2 = Utility.ExecuteSelectQuery(sel2, "ASPTBLINOUTMAS");
                            DataTable dt2 = ds2.Tables["ASPTBLINOUTMAS"];
                            txtgatedcno1.Text = dt2.Rows[0]["INWARDNO"].ToString();
                            si.AGFMIN = Convert.ToString(txtgatedcno1.Text);
                            si.GateDcNo = Class.Users.Finyear + "/" + Class.Users.HCompcode + "/" + txtgatedcno1.Text; return;
                        }
                        else
                        {

                            txtgatedcno1.Text = Convert.ToInt64("0" + dt.Rows[0]["ID"].ToString()).ToString();
                            si.AGFMIN = Convert.ToString(txtgatedcno1.Text);
                            si.GateDcNo = Class.Users.Finyear + "/" + Class.Users.HCompcode + "/" + txtgatedcno1.Text; return;
                        }
                    }
                }
                catch (Exception EX)
                {
                    MessageBox.Show(EX.Message);
                }
                //if (Class.Users.HCompcode == "AGF" || Class.Users.HCompcode == "AGFM" || Class.Users.HCompcode == "FLF")
                //{
                // DataTable dt1 = si.select1();
                //    txt_inventryid.Text = dt1.Rows[0]["INVENTRYID"].ToString();
                //    si.InventryID = Convert.ToInt64("0" + dt1.Rows[0]["INVENTRYID"].ToString());
                //}
            }
            else
            {
                MessageBox.Show("pls Enter QR Code");
                txt_qrcodein.Select();
                return;
            }
        }

        public void autonoOUT()
        {
            if (txtqrcodeout.Text != "")
            {
                try
                {
                    if (Class.Users.HCompcode == "FLFD" && txtcategoryout.Text == "OUT")
                    {
                        string sel1 = "SELECT  MAX(TO_NUMBER(A.FLFDOUT))+1  ID  FROM  ASPINVENTRY A JOIN GTCOMPMAST B ON A.COMPCODE=B.GTCOMPMASTID   JOIN gtfinancialyear C ON C.GTFINANCIALYEARID=A.FINYEAR and C.CURRENTFINYR = 'T'   JOIN ASPTBLINOUTMAS D ON D.COMPCODE=A.COMPCODE AND  D.COMPCODE=B.GTCOMPMASTID   and A.FINYEAR=C.GTFINANCIALYEARID AND D.FINYEAR=A.FINYEAR   AND   D.ACTIVE='T'  WHERE C.FINYR='" + Class.Users.Finyear + "' and A.COMPCODE='" + Class.Users.COMPCODE + "' AND  A.CREATEDON >=  SUBSTR(D.CREATEDON,0,10)";
                        DataSet ds = Utility.ExecuteSelectQuery(sel1, "ASPINVENTRY");
                        DataTable dt = ds.Tables["ASPINVENTRY"];
                        Int64 cnt = Convert.ToInt64("0" + dt.Rows[0]["ID"].ToString());
                        if (cnt == 0)
                        {
                            string sel2 = "  SELECT MAX(A.OUTWARDNO ) OUTWARDNO     FROM  ASPTBLINOUTMAS A JOIN GTCOMPMAST B ON A.COMPCODE=B.GTCOMPMASTID JOIN gtfinancialyear C ON C.GTFINANCIALYEARID=A.FINYEAR  and C.CURRENTFINYR='T' AND  A.ACTIVE='T' AND  A.FLFDOUT='T'  WHERE A.FINYEAR='" + combofinyearout.SelectedValue + "' AND A.COMPCODE='" + Class.Users.COMPCODE + "' ";
                            DataSet ds2 = Utility.ExecuteSelectQuery(sel2, "ASPTBLINOUTMAS");
                            DataTable dt2 = ds2.Tables["ASPTBLINOUTMAS"];
                            txtgatedcno1.Text = dt2.Rows[0]["OUTWARDNO"].ToString();
                            si.FLFDOUT = Convert.ToString(txtgatedcno1.Text);
                            si.GateDcNo = Class.Users.Finyear + "/" + Class.Users.HCompcode + "/" + txtgatedcno1.Text;
                          
                            return;
                        }
                        else
                        {

                            txtgatedcno1.Text = Convert.ToInt64("0" + dt.Rows[0]["ID"].ToString()).ToString();
                            si.FLFDOUT = Convert.ToString(txtgatedcno1.Text);
                            si.GateDcNo = Class.Users.Finyear + "/" + Class.Users.HCompcode + "/" + txtgatedcno1.Text;
                            btnsaveOUT.Focus(); return;
                        }
                    }

                    if (Class.Users.HCompcode == "VEL" && txtcategoryout.Text == "OUT")
                    {
                        string sel1 = "SELECT  MAX(TO_NUMBER(A.VELOUT))+1  ID  FROM  ASPINVENTRY A JOIN GTCOMPMAST B ON A.COMPCODE=B.GTCOMPMASTID   JOIN gtfinancialyear C ON C.GTFINANCIALYEARID=A.FINYEAR and C.CURRENTFINYR = 'T'   JOIN ASPTBLINOUTMAS D ON D.COMPCODE=A.COMPCODE AND  D.COMPCODE=B.GTCOMPMASTID   and A.FINYEAR=C.GTFINANCIALYEARID AND D.FINYEAR=A.FINYEAR   AND   D.ACTIVE='T'  WHERE C.FINYR='" + Class.Users.Finyear + "' and A.COMPCODE='" + Class.Users.COMPCODE + "' AND  A.CREATEDON >=  SUBSTR(D.CREATEDON,0,10)";
                        DataSet ds = Utility.ExecuteSelectQuery(sel1, "ASPINVENTRY");
                        DataTable dt = ds.Tables["ASPINVENTRY"];
                        Int64 cnt = Convert.ToInt64("0" + dt.Rows[0]["ID"].ToString());
                        if (cnt == 0)
                        {
                            string sel2 = "  SELECT MAX(A.OUTWARDNO ) OUTWARDNO     FROM  ASPTBLINOUTMAS A JOIN GTCOMPMAST B ON A.COMPCODE=B.GTCOMPMASTID JOIN gtfinancialyear C ON C.GTFINANCIALYEARID=A.FINYEAR  and C.CURRENTFINYR='T' AND  A.ACTIVE='T' AND  A.VELOUT='T'  WHERE A.FINYEAR='" + combofinyearout.SelectedValue + "' AND A.COMPCODE='" + Class.Users.COMPCODE + "'";
                            DataSet ds2 = Utility.ExecuteSelectQuery(sel2, "ASPTBLINOUTMAS");
                            DataTable dt2 = ds2.Tables["ASPTBLINOUTMAS"];
                            txtgatedcno1.Text = dt2.Rows[0]["OUTWARDNO"].ToString();
                            si.VELOUT = Convert.ToString(txtgatedcno1.Text);
                            si.GateDcNo = Class.Users.Finyear + "/" + Class.Users.HCompcode + "/" + txtgatedcno1.Text; return;
                        }
                        else
                        {

                            txtgatedcno1.Text = Convert.ToInt64("0" + dt.Rows[0]["ID"].ToString()).ToString();
                            si.VELOUT = Convert.ToString(txtgatedcno1.Text);
                            si.GateDcNo = Class.Users.Finyear + "/" + Class.Users.HCompcode + "/" + txtgatedcno1.Text; return;
                        }
                    }
                    if (Class.Users.HCompcode == "AGF" && txtcategoryout.Text == "OUT")
                    {
                        string sel1 = "SELECT  MAX(TO_NUMBER(A.AGFOUT))+1  ID  FROM  ASPINVENTRY A JOIN GTCOMPMAST B ON A.COMPCODE=B.GTCOMPMASTID   JOIN gtfinancialyear C ON C.GTFINANCIALYEARID=A.FINYEAR and C.CURRENTFINYR = 'T'   JOIN ASPTBLINOUTMAS D ON D.COMPCODE=A.COMPCODE AND  D.COMPCODE=B.GTCOMPMASTID   and A.FINYEAR=C.GTFINANCIALYEARID AND D.FINYEAR=A.FINYEAR   AND   D.ACTIVE='T'  WHERE C.FINYR='" + Class.Users.Finyear + "' and A.COMPCODE='" + Class.Users.COMPCODE + "' AND  A.CREATEDON >=  SUBSTR(D.CREATEDON,0,10)";
                        DataSet ds3 = Utility.ExecuteSelectQuery(sel1, "ASPINVENTRY");
                        DataTable dt3 = ds3.Tables["ASPINVENTRY"];
                        Int64 cnt = Convert.ToInt64("0" + dt3.Rows[0]["ID"].ToString());
                        if (cnt == 0)
                        {

                            string sel4 = "  SELECT MAX(A.OUTWARDNO ) OUTWARDNO    FROM  ASPTBLINOUTMAS A JOIN GTCOMPMAST B ON A.COMPCODE=B.GTCOMPMASTID JOIN gtfinancialyear C ON C.GTFINANCIALYEARID=A.FINYEAR  and C.CURRENTFINYR='T'  AND  A.ACTIVE='T' AND  A.AGFOUT='T'   WHERE A.FINYEAR='" + combofinyearout.SelectedValue + "' AND A.COMPCODE='" + Class.Users.COMPCODE + "'";
                            DataSet ds4 = Utility.ExecuteSelectQuery(sel4, "ASPTBLINOUTMAS");
                            DataTable dt4 = ds4.Tables["ASPTBLINOUTMAS"];
                            txtgatedcno1.Text = dt4.Rows[0]["OUTWARDNO"].ToString();
                            si.AGFOUT = Convert.ToString(txtgatedcno1.Text);
                            si.GateDcNo = Class.Users.Finyear + "/" + Class.Users.HCompcode + "/" + txtgatedcno1.Text; return;
                        }
                        else
                        {

                            txtgatedcno1.Text = Convert.ToInt64("0" + dt3.Rows[0]["ID"].ToString()).ToString();
                            si.AGFOUT = Convert.ToString(txtgatedcno1.Text);
                            si.GateDcNo = Class.Users.Finyear + "/" + Class.Users.HCompcode + "/" + txtgatedcno1.Text; return;
                        }
                    }
                    if (Class.Users.HCompcode == "AGFC" && txtcategoryout.Text == "OUT")
                    {
                        string sel3 = "SELECT  MAX(TO_NUMBER(A.AGFCOUT))+1  ID  FROM  ASPINVENTRY A JOIN GTCOMPMAST B ON A.COMPCODE=B.GTCOMPMASTID   JOIN gtfinancialyear C ON C.GTFINANCIALYEARID=A.FINYEAR and C.CURRENTFINYR = 'T'   JOIN ASPTBLINOUTMAS D ON D.COMPCODE=A.COMPCODE AND  D.COMPCODE=B.GTCOMPMASTID   and A.FINYEAR=C.GTFINANCIALYEARID AND D.FINYEAR=A.FINYEAR   AND   D.ACTIVE='T'  WHERE C.FINYR='" + Class.Users.Finyear + "' and A.COMPCODE='" + Class.Users.COMPCODE + "' AND  A.CREATEDON >=  SUBSTR(D.CREATEDON,0,10)";

                     //   string sel3 = "SELECT  MAX(TO_NUMBER(A.AGFCOUT))+1  ID FROM  ASPINVENTRY A JOIN GTCOMPMAST B ON A.COMPCODE=B.GTCOMPMASTID JOIN gtfinancialyear C ON C.GTFINANCIALYEARID=A.FINYEAR  and C.CURRENTFINYR='T'  WHERE A.COMPCODE=" + Class.Users.COMPCODE;
                        DataSet ds3 = Utility.ExecuteSelectQuery(sel3, "ASPINVENTRY");
                        DataTable dt3 = ds3.Tables["ASPINVENTRY"];
                        Int64 cnt = Convert.ToInt64("0" + dt3.Rows[0]["ID"].ToString());
                        if (cnt == 0)
                        {
                            string sel4 = "  SELECT MAX(A.OUTWARDNO ) OUTWARDNO    FROM  ASPTBLINOUTMAS A JOIN GTCOMPMAST B ON A.COMPCODE=B.GTCOMPMASTID JOIN gtfinancialyear C ON C.GTFINANCIALYEARID=A.FINYEAR  and C.CURRENTFINYR='T'  AND  A.ACTIVE='T' AND  A.AGFCOUT='T'   WHERE A.FINYEAR='" + combofinyearout.SelectedValue + "' AND A.COMPCODE='" + Class.Users.COMPCODE + "'";
                            DataSet ds4 = Utility.ExecuteSelectQuery(sel4, "ASPTBLINOUTMAS");
                            DataTable dt4 = ds4.Tables["ASPTBLINOUTMAS"];
                            txtgatedcno1.Text = dt4.Rows[0]["OUTWARDNO"].ToString();
                            si.AGFCOUT = Convert.ToString(txtgatedcno1.Text);
                            si.GateDcNo = Class.Users.Finyear + "/" + Class.Users.HCompcode + "/" + txtgatedcno1.Text; return;
                        }
                        else
                        {

                            txtgatedcno1.Text = Convert.ToInt64("0" + dt3.Rows[0]["ID"].ToString()).ToString();
                            si.AGFCOUT = Convert.ToString(txtgatedcno1.Text);
                            si.GateDcNo = Class.Users.Finyear + "/" + Class.Users.HCompcode + "/" + txtgatedcno1.Text; return;
                        }
                    }
                    if (Class.Users.HCompcode == "AGFP" && txtcategoryout.Text == "OUT")
                    {
                        string sel3 = "SELECT  MAX(TO_NUMBER(A.AGFPOUT))+1  ID  FROM  ASPINVENTRY A JOIN GTCOMPMAST B ON A.COMPCODE=B.GTCOMPMASTID   JOIN gtfinancialyear C ON C.GTFINANCIALYEARID=A.FINYEAR and C.CURRENTFINYR = 'T'   JOIN ASPTBLINOUTMAS D ON D.COMPCODE=A.COMPCODE AND  D.COMPCODE=B.GTCOMPMASTID   and A.FINYEAR=C.GTFINANCIALYEARID AND D.FINYEAR=A.FINYEAR   AND   D.ACTIVE='T'  WHERE C.FINYR='" + Class.Users.Finyear + "' and A.COMPCODE='" + Class.Users.COMPCODE + "' AND  A.CREATEDON >=  SUBSTR(D.CREATEDON,0,10)";

                        //string sel3 = "SELECT  MAX(TO_NUMBER(A.AGFPOUT))+1  ID FROM  ASPINVENTRY A JOIN GTCOMPMAST B ON A.COMPCODE=B.GTCOMPMASTID JOIN gtfinancialyear C ON C.GTFINANCIALYEARID=A.FINYEAR  and C.CURRENTFINYR='T'  WHERE A.COMPCODE=" + Class.Users.COMPCODE;
                        DataSet ds3 = Utility.ExecuteSelectQuery(sel3, "ASPINVENTRY");
                        DataTable dt3 = ds3.Tables["ASPINVENTRY"];
                        Int64 cnt = Convert.ToInt64("0" + dt3.Rows[0]["ID"].ToString());
                        if (cnt == 0)
                        {
                            string sel4 = "  SELECT MAX(A.OUTWARDNO ) OUTWARDNO    FROM  ASPTBLINOUTMAS A JOIN GTCOMPMAST B ON A.COMPCODE=B.GTCOMPMASTID JOIN gtfinancialyear C ON C.GTFINANCIALYEARID=A.FINYEAR  and C.CURRENTFINYR='T'  AND  A.ACTIVE='T' AND  A.AGFPOUT='T'   WHERE A.FINYEAR='" + combofinyearout.SelectedValue + "' AND A.COMPCODE='" + Class.Users.COMPCODE + "'";
                            DataSet ds4 = Utility.ExecuteSelectQuery(sel4, "ASPTBLINOUTMAS");
                            DataTable dt4 = ds4.Tables["ASPTBLINOUTMAS"];
                            txtgatedcno1.Text = dt4.Rows[0]["OUTWARDNO"].ToString();
                            si.AGFPOUT = Convert.ToString(txtgatedcno1.Text);
                            si.GateDcNo = Class.Users.Finyear + "/" + Class.Users.HCompcode + "/" + txtgatedcno1.Text; return;
                        }
                        else
                        {

                            txtgatedcno1.Text = Convert.ToInt64("0" + dt3.Rows[0]["ID"].ToString()).ToString();
                            si.AGFPOUT = Convert.ToString(txtgatedcno1.Text);
                            si.GateDcNo = Class.Users.Finyear + "/" + Class.Users.HCompcode + "/" + txtgatedcno1.Text; return;
                        }
                    }
                    if (Class.Users.HCompcode == "AGFMGII" && txtcategoryout.Text == "OUT")
                    {
                        string sel3 = "SELECT  MAX(TO_NUMBER(A.AGFMGIIOUT))+1  ID  FROM  ASPINVENTRY A JOIN GTCOMPMAST B ON A.COMPCODE=B.GTCOMPMASTID   JOIN gtfinancialyear C ON C.GTFINANCIALYEARID=A.FINYEAR and C.CURRENTFINYR = 'T'   JOIN ASPTBLINOUTMAS D ON D.COMPCODE=A.COMPCODE AND  D.COMPCODE=B.GTCOMPMASTID   and A.FINYEAR=C.GTFINANCIALYEARID AND D.FINYEAR=A.FINYEAR   AND   D.ACTIVE='T'  WHERE C.FINYR='" + Class.Users.Finyear + "' and A.COMPCODE='" + Class.Users.COMPCODE + "' AND  A.CREATEDON >=  SUBSTR(D.CREATEDON,0,10)";

                        // string sel3 = "SELECT  MAX(TO_NUMBER(A.AGFMGIIOUT))+1  ID FROM  ASPINVENTRY A JOIN GTCOMPMAST B ON A.COMPCODE=B.GTCOMPMASTID JOIN gtfinancialyear C ON C.GTFINANCIALYEARID=A.FINYEAR  and C.CURRENTFINYR='T'  WHERE A.COMPCODE=" + Class.Users.COMPCODE;
                        DataSet ds3 = Utility.ExecuteSelectQuery(sel3, "ASPINVENTRY");
                        DataTable dt3 = ds3.Tables["ASPINVENTRY"];
                        Int64 cnt = Convert.ToInt64("0" + dt3.Rows[0]["ID"].ToString());
                        if (cnt == 0)
                        {
                            string sel4 = "  SELECT MAX(A.OUTWARDNO ) OUTWARDNO    FROM  ASPTBLINOUTMAS A JOIN GTCOMPMAST B ON A.COMPCODE=B.GTCOMPMASTID JOIN gtfinancialyear C ON C.GTFINANCIALYEARID=A.FINYEAR  and C.CURRENTFINYR='T'  AND  A.ACTIVE='T' AND  A.AGFMGIIOUT='T'   WHERE A.FINYEAR='" + combofinyearout.SelectedValue + "' AND A.COMPCODE='" + Class.Users.COMPCODE + "'";
                            DataSet ds4 = Utility.ExecuteSelectQuery(sel4, "ASPTBLINOUTMAS");
                            DataTable dt4 = ds4.Tables["ASPTBLINOUTMAS"];
                            txtgatedcno1.Text = dt4.Rows[0]["OUTWARDNO"].ToString();
                            si.AGFMGIIOUT = Convert.ToString(txtgatedcno1.Text);
                            si.GateDcNo = Class.Users.Finyear + "/" + Class.Users.HCompcode + "/" + txtgatedcno1.Text; return;
                        }
                        else
                        {

                            txtgatedcno1.Text = Convert.ToInt64("0" + dt3.Rows[0]["ID"].ToString()).ToString();
                            si.AGFMGIIOUT = Convert.ToString(txtgatedcno1.Text);
                            si.GateDcNo = Class.Users.Finyear + "/" + Class.Users.HCompcode + "/" + txtgatedcno1.Text; return;
                        }
                    }
                    if (Class.Users.HCompcode == "AGFM" && txtcategoryout.Text == "OUT")
                    {
                        string sel1 = "SELECT  MAX(TO_NUMBER(A.AGFMOUT))+1  ID  FROM  ASPINVENTRY A JOIN GTCOMPMAST B ON A.COMPCODE=B.GTCOMPMASTID   JOIN gtfinancialyear C ON C.GTFINANCIALYEARID=A.FINYEAR and C.CURRENTFINYR = 'T'   JOIN ASPTBLINOUTMAS D ON D.COMPCODE=A.COMPCODE AND  D.COMPCODE=B.GTCOMPMASTID   and A.FINYEAR=C.GTFINANCIALYEARID AND D.FINYEAR=A.FINYEAR   AND   D.ACTIVE='T'  WHERE C.FINYR='" + Class.Users.Finyear + "' and A.COMPCODE='" + Class.Users.COMPCODE + "' AND  A.CREATEDON >=  SUBSTR(D.CREATEDON,0,10)";

                        // string sel1 = "SELECT MAX(TO_NUMBER(A.AGFMOUT))+1  ID FROM  ASPINVENTRY A JOIN GTCOMPMAST B ON A.COMPCODE=B.GTCOMPMASTID JOIN gtfinancialyear C ON C.GTFINANCIALYEARID=A.FINYEAR  and C.CURRENTFINYR='T'  WHERE A.COMPCODE=" + Class.Users.COMPCODE;
                        DataSet ds = Utility.ExecuteSelectQuery(sel1, "ASPINVENTRY");
                        DataTable dt = ds.Tables["ASPINVENTRY"];
                        Int64 cnt = Convert.ToInt64("0" + dt.Rows[0]["ID"].ToString());
                        if (cnt == 0)
                        {
                            string sel2 = "  SELECT MAX(A.OUTWARDNO ) OUTWARDNO   FROM  ASPTBLINOUTMAS A JOIN GTCOMPMAST B ON A.COMPCODE=B.GTCOMPMASTID JOIN gtfinancialyear C ON C.GTFINANCIALYEARID=A.FINYEAR  and C.CURRENTFINYR='T' AND  A.ACTIVE='T' AND  A.AGFMOUT='T' WHERE A.FINYEAR='" + combofinyearout.SelectedValue + "' AND A.COMPCODE='" + Class.Users.COMPCODE + "' ";
                            DataSet ds2 = Utility.ExecuteSelectQuery(sel2, "ASPTBLINOUTMAS");
                            DataTable dt2 = ds2.Tables["ASPTBLINOUTMAS"];
                            txtgatedcno1.Text = dt2.Rows[0]["OUTWARDNO"].ToString();
                            si.AGFMOUT = Convert.ToString(txtgatedcno1.Text);
                            si.GateDcNo = Class.Users.Finyear + "/" + Class.Users.HCompcode + "/" + txtgatedcno1.Text; return;
                        }
                        else
                        {

                            txtgatedcno1.Text = Convert.ToInt64("0" + dt.Rows[0]["ID"].ToString()).ToString();
                            si.AGFMOUT = Convert.ToString(txtgatedcno1.Text);
                            si.GateDcNo = Class.Users.Finyear + "/" + Class.Users.HCompcode + "/" + txtgatedcno1.Text; return;
                        }
                    }
                }
                catch (Exception EX)
                {
                    MessageBox.Show(EX.Message);
                }
                //if (Class.Users.HCompcode == "AGF" || Class.Users.HCompcode == "AGFM" || Class.Users.HCompcode == "FLF")
                //{
                //    DataTable dt1 = si.select1();
                //    txt_inventryid.Text = dt1.Rows[0]["INVENTRYID"].ToString();
                //    si.InventryID = Convert.ToInt64("0" + dt1.Rows[0]["INVENTRYID"].ToString());
                //}
            }
            else
            {
                MessageBox.Show("Pls Enter Qr Code");
                txtqrcodeout.Select(); return;
            }
        }
        private void BtnsavesIN_Click(object sender, EventArgs e)
        {
            try
            {
                Class.Users.Intimation = "";
                Class.Users.UserTime = 0; Class.Users.Intimation = "";
                string[] words = txt_qrcodein.Text.Split(':');
                if (words[0].ToString() == "TOKENNO")
                {
                    MessageBox.Show("Invalid ", "Goods Outward Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txt_qrcodein.Text = ""; txt_qrcodein.Select();
                    return;
                }
                autonoIN();
                lblRecordno.Visible = true; lblRecorddatetime.Visible = true;
                lblRecordno.Text = ""; lblRecorddatetime.Text = "";                            
                si.QrCode = Convert.ToString(txt_qrcodein.Text.ToUpper());
                si.Category = Convert.ToString(txtcategoryin.Text.ToUpper());
                si.FinYear = Convert.ToInt64(combofinyearIN.SelectedValue);
                si.SystemDate = System.DateTime.Now.ToString("dd-MM-yyyy");
                si.SystemTime = Convert.ToString(DateTime.Now.ToLongTimeString().ToString());
                si.CompCode = Convert.ToInt64(combo_compcode.SelectedValue);
                si.UserName = Convert.ToInt64(combo_username.SelectedValue);
                si.GateName = Convert.ToString(txt_gatename.Text);
                si.Modified = Convert.ToDateTime(System.DateTime.Now.ToString("dd-MM-yyyy") + " " + System.DateTime.Now.ToLongTimeString());
                si.CreatedOn = Convert.ToDateTime(System.DateTime.Now.ToString("dd-MM-yyyy") + " " + System.DateTime.Now.ToLongTimeString());
                si.IpAddress = Convert.ToString(txt_ipaddress.Text);
                string c = Convert.ToString(si.InventryID);

                if (!string.IsNullOrEmpty(txt_qrcodein.Text))
                {

                    if (txt_qrcodein.Text.Length >= 10)
                    {
                        string sel = "SELECT A.QRCODE,A.GATEDCNO,A.CREATEDON FROM ASPINVENTRY A join GTCOMPMAST b on a.compcode=b.gtcompmastid where a.CATEGORY='" + txtcategoryin.Text+"'  and  a.QRCODE='" + si.QrCode + "' and a.compcode='" + si.CompCode + "'and a.finyear='" + si.FinYear + "'";
                        DataSet ds = Utility.ExecuteSelectQuery(sel, "ASPINVENTRY");
                        DataTable dt = ds.Tables["ASPINVENTRY"];
                        if (dt.Rows.Count > 0)
                        {
                            lblRecordno.Text = "This Dc-Number : " + si.QrCode;
                            lblRecorddatetime1.Visible = true;
                            lblRecorddatetime1.Text = "OUR GATE DCNO " + dt.Rows[0]["GATEDCNO"].ToString() + "     Date :" + dt.Rows[0]["CREATEDON"].ToString();
                            lblRecorddatetime.Text = "Already Exits . Child Record Found .  ";
                            txt_qrcodein.Select();
                            txt_qrcodein.Text = "";
                            txt_qrcodein.Text = "";
                            txt_qrcodein.Focus(); Class.Users.UserTime = 0;
                        }
                        else
                        {
                            if (Convert.ToInt32(txt_qrcodein.Text.Length) <= 25)
                            {
                                lblRecorddatetime1.Visible = false;
                                si = new Models.SecInventryModel(si.FinYear, si.GateDcNo, si.AGFIN, si.AGFOUT, si.AGFMIN, si.AGFMOUT, si.VELIN, si.VELOUT, si.FLFDIN, si.FLFDOUT, si.QrCode, si.Category, si.SystemDate, si.SystemTime, Class.Users.COMPCODE, Class.Users.USERID, Class.Users.HGateName, si.Modified, si.CreatedOn, si.IpAddress, si.OTHIN, si.OTHOUT);

                                DataTable dt2 = si.findcompcode();
                                DataTable dt1 = si.select(dt2.Rows[0]["INVENTRYID"].ToString(), Class.Users.COMPCODE, Convert.ToInt64(combofinyearIN.SelectedValue));
                                lblRecordno.Text = "Inward No : " + dt1.Rows[0]["GATEDCNO"].ToString();
                                lblRecorddatetime.Text = "Out Time : " + dt1.Rows[0]["SYSTEMDATE"].ToString() + "-" + dt1.Rows[0]["SYSTEMTIME"].ToString();

                                MessageBox.Show("Record Saved Successfully " + "        " + txt_qrcodein.Text, "Success Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                empty(); lblRecordno.Visible = false; lblRecorddatetime.Visible = false;
                                txtgatedcno1.Text = ""; txtcategoryin.Focus(); si.GateDcNo = "";
                            }
                            else
                            {
                               
                                    MessageBox.Show("'Invalid  ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    txtcategoryin.Focus();
                               
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("'Invalid  ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtcategoryin.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("'Qr Code Field'  Empty not Allowed  ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txt_qrcodein.Focus();

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Security Inventry " + "        " + ex.ToString(), "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt_qrcodein.Focus();
            }
            btnsavesIN.BackColor = Color.WhiteSmoke; txtgatedcno1.Text = "";
            btnsavesIN.ForeColor = Color.RoyalBlue; txt_qrcodein.Select(); 
        }

        private void BtnsaveOUT_Click(object sender, EventArgs e)
        {
            try
            {
                Class.Users.UserTime = 0; Class.Users.Intimation = "";
                string[] words = txtqrcodeout.Text.Split(':');
                if (words[0].ToString() == "TOKENNO")
                {
                    MessageBox.Show("Invalid", "Goods Outward Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtqrcodeout.Text = ""; txtqrcodeout.Select();
                    return;
                }
                autonoOUT();
                lblRecordnoOUT.Visible = true; lblRecorddatetimeOUT.Visible = true;
                lblRecordnoOUT.Text = ""; lblRecorddatetimeOUT.Text = "";               
                si.InventryID = Convert.ToInt64("0" + txtinventryidout.Text);
                si.FinYear = Convert.ToInt64(combofinyearout.SelectedValue);
                si.QrCode = Convert.ToString(txtqrcodeout.Text.ToUpper());
                si.Category = Convert.ToString(txtcategoryout.Text.ToUpper());
                si.SystemDate = System.DateTime.Now.ToString("dd-MM-yyyy");
                si.SystemTime = Convert.ToString(DateTime.Now.ToLongTimeString().ToString());
                si.CompCode = Convert.ToInt64("0"+combo_compcodeout.SelectedValue);
                si.UserName = Convert.ToInt64("0"+combo_usernameout.SelectedValue);
                si.GateName = Convert.ToString(txtgatenameout.Text);
                si.Modified = Convert.ToDateTime(System.DateTime.Now.ToString("dd-MM-yyyy") + " " + System.DateTime.Now.ToLongTimeString());
                si.CreatedOn = Convert.ToDateTime(System.DateTime.Now.ToString("dd-MM-yyyy") + " " + System.DateTime.Now.ToLongTimeString());
                si.IpAddress = Convert.ToString(txt_ipaddress.Text);
                string c = Convert.ToString(si.InventryID);
                if (!string.IsNullOrEmpty(txtqrcodeout.Text))
                {

                    if (txtqrcodeout.Text.Length >= 10)
                    {
                        string sel = "SELECT A.QRCODE ,a.GATEDCNO,A.CREATEDON FROM ASPINVENTRY A join GTCOMPMAST b on a.compcode=b.gtcompmastid where a.CATEGORY='" + txtcategoryout.Text + "'  and  a.QRCODE='" + si.QrCode + "' and a.compcode='" + si.CompCode + "'and a.finyear='" + si.FinYear + "'";
                        DataSet ds = Utility.ExecuteSelectQuery(sel, "ASPINVENTRY");
                        DataTable dt = ds.Tables["ASPINVENTRY"];
                        if (dt.Rows.Count > 0)
                        {
                            lblRecordnoOUT.Text = "This Dc-Number : " + si.QrCode;
                            lblRecorddatetimeOUT.Text = "Already Exits . Child Record Found . ";
                            lblRecorddatetimeOUT1.Visible = true;
                            lblRecorddatetimeOUT1.Text = "OUR GATE DCNO " + dt.Rows[0]["GATEDCNO"].ToString() + "     Date :" + dt.Rows[0]["CREATEDON"].ToString();
                            txtqrcodeout.Select();                  
                            txtqrcodeout.Text = "";
                            txtqrcodeout.Focus(); Class.Users.UserTime = 0;
                        }
                        else
                        {
                            if (Convert.ToInt32(txtqrcodeout.Text.Length) <= 25)
                            {
                                lblRecorddatetimeOUT1.Visible = false;
                                si = new Models.SecInventryModel(si.FinYear, si.GateDcNo, si.AGFIN, si.AGFOUT, si.AGFMIN, si.AGFMOUT, si.VELIN, si.VELOUT, si.FLFDIN, si.FLFDOUT, si.QrCode, si.Category, si.SystemDate, si.SystemTime, Class.Users.COMPCODE, Class.Users.USERID, Class.Users.HGateName, si.Modified, si.CreatedOn, si.IpAddress, si.OTHIN, si.OTHOUT);
                                DataTable dt2 = si.findcompcode();
                                DataTable dt1 = si.select(dt2.Rows[0]["INVENTRYID"].ToString(), Class.Users.COMPCODE, Convert.ToInt64(combofinyearout.SelectedValue));
                                lblRecordnoOUT.Text = "OutWard No : " + dt1.Rows[0]["GATEDCNO"].ToString();
                                lblRecorddatetimeOUT.Text = "Out Time : " + dt1.Rows[0]["SYSTEMDATE"].ToString() + "-" + dt1.Rows[0]["SYSTEMTIME"].ToString();

                                MessageBox.Show("Record Saved Successfully " + "        " + txt_qrcodein.Text, "Success Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                empty(); lblRecordnoOUT.Visible = false; lblRecorddatetimeOUT.Visible = false;
                                txtqrcodeout.Select();
                            }
                            else
                            {
                                MessageBox.Show("'Invalid  ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                txtqrcodeout.Select();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("'Invalid  ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtqrcodeout.Select();
                    }
                }
                else
                {
                    MessageBox.Show("'Qr Code Field'  Empty not Allowed  ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    txtqrcodeout.Select();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Security Inventry " + "        " + ex.ToString(), "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtqrcodeout.Select();
            }
            btnsavesIN.BackColor = Color.WhiteSmoke; txtgatedcno1.Text = "";
            btnsavesIN.ForeColor = Color.RoyalBlue; txtqrcodeout.Select();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            tabControl1.TabPages.Add(tabGOODSOut);
            tabControl1.SelectTab(tabGOODSOut);
            Class.Users.UserTime = 0; Class.Users.Intimation = "";
            tabControl1.TabPages.Remove(tabEMPIn);
            tabControl1.TabPages.Remove(tabEMPOut);
           
            txtqrcodeout.Select();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            tabControl1.TabPages.Add(tabGOODSIN);
            tabControl1.SelectTab(tabGOODSIN);
            Class.Users.UserTime = 0; Class.Users.Intimation = "";
            tabControl1.TabPages.Remove(tabEMPIn);
            tabControl1.TabPages.Remove(tabEMPOut);
            
            txt_qrcodein.Select();
        }

        private void TabEMPInOut_Click(object sender, EventArgs e)
        {
            txtintime.Focus();
        }

      
        private void Btngoodsoutexit_Click(object sender, EventArgs e)
        {
            this.tabControl1.TabPages.Remove(tabGOODSOut);txtqrcodeout.Text = "";
            tabControl1.TabPages.Add(tabGOODSIN); Class.Users.Intimation = "";
            tabControl1.SelectTab(tabGOODSIN); Class.Users.UserTime = 0;
            lblRecordnoOUT.Visible = false; lblRecorddatetimeOUT.Visible = false;
            lblRecordnoOUT.Text = ""; lblRecorddatetimeOUT.Text = ""; Class.Users.UserTime = 0;
        }

        private void BTNGoodsINEXIT_Click(object sender, EventArgs e)
        {
            this.tabControl1.TabPages.Remove(tabGOODSIN);txt_qrcodein.Text = "";
            tabControl1.TabPages.Add(tabGOODSOut); Class.Users.Intimation = "";
            tabControl1.SelectTab(tabGOODSOut); Class.Users.UserTime = 0;
            lblRecordno.Visible = false; lblRecorddatetime.Visible = false;
            lblRecordno.Text = ""; lblRecorddatetime.Text = ""; Class.Users.UserTime = 0;
        }

        private void TabGOODSINOUT_Click(object sender, EventArgs e)
        {

        }

        private void TabGOODSIN_Click(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {

        }

        private void Label24_Click(object sender, EventArgs e)
        {

        }

        private void Label24_Click_1(object sender, EventArgs e)
        {

        }

        private void TabGOODSOut_Click(object sender, EventArgs e)
        {

        }

        private void LblRecordnoOUT_Click(object sender, EventArgs e)
        {

        }

        private void btnoutclear_Click(object sender, EventArgs e)
        {
            txtouttime.Text = ""; txtouttime.Select();
        }

        private void btninwardclear_Click(object sender, EventArgs e)
        {
            txtintime.Text = ""; txtintime.Select(); txtremarks.Text = "";  checkpassmissed.Checked = false; pictureinwardimage.Image = null;
        }

        public void ReadOnlys()
        {
            throw new NotImplementedException();
        }

        private void txt_qrcode_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_qrcodein_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void panel5_Click(object sender, EventArgs e)
        {
            Class.Users.UserTime = 0;
        }

        public void Searchs(int id)
        {
            throw new NotImplementedException();
        }

        public void Deletes(int id)
        {
            throw new NotImplementedException();
        }

        private void tabControl1_TabIndexChanged(object sender, EventArgs e)
        {
           
            
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabControl1.TabPages["tabGOODSINOUT"])
            {
                Class.Users.Intimation = "";
        

            }
            if (tabControl1.SelectedTab == tabControl1.TabPages["tabEMPInOut"])//your specific tabname
            {
                Class.Users.Intimation = "PAYROLL";
               

            }
        }

        private void refreshToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Class.Users.Intimation = "PAYROLL";
        }

        private void rToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Class.Users.Intimation = "";
        }
    }
}
