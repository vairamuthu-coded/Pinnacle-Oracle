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
    public partial class SecurityDelivery : Form,ToolStripAccess
    {
        Models.Master mas = new Models.Master();
        Models.UserRights sm = new Models.UserRights();
        Models.SecInventryModel si = new Models.SecInventryModel();
        private static SecurityDelivery _instance;
        PinnacleMdi mdi = new PinnacleMdi(); byte[] bytes;
      //  private static string s1 = ConfigurationManager.ConnectionStrings["ConnectionString1"].ConnectionString;
        //static MongoClient client = new MongoClient();
        //static IMongoDatabase db = client.GetDatabase("tiplagf");
        //static IMongoCollection<Pinnacle.Models.Employee> collection = db.GetCollection<Pinnacle.Models.Employee>("Employee");
        public static SecurityDelivery Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new SecurityDelivery();
                GlobalVariables.CurrentForm = _instance; return _instance;
            }
        }
        public SecurityDelivery()
        {
            InitializeComponent();
            Class.Users.ScreenName = "SecurityDelivery";
            Class.Users.IPADDRESS = GenFun.GetLocalIPAddress();
            Class.Users.CREATED = Convert.ToDateTime(System.DateTime.Now.ToString("dd-MM-yyyy") + " " + System.DateTime.Now.ToLongTimeString());
            Class.Users.SysDate = Convert.ToString(System.DateTime.Now.ToString("dd-MM-yyyy"));
            Class.Users.SysTime = Convert.ToString(DateTime.Now.ToLongTimeString().ToString());
            si.SystemDate = Class.Users.SysDate;
            si.IpAddress = Class.Users.IPADDRESS;


     
            tabControl1.TabPages.Remove(tabGOODSIN);
            tabControl1.TabPages.Remove(tabGOODSOut);

           // tabControl1.TabPages.Add(tabGOODSINOUT);
            
            GlobalVariables.HeaderName.Text = Class.Users.ScreenName;
         
            butheader.BackColor = Class.Users.BackColors;
          
            panel2.BackColor = Class.Users.BackColors;
          
            button1.BackColor = Class.Users.BackColors;
            button5.BackColor = Class.Users.BackColors;

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
            
            
            txt_inventryid.Enabled = false; Class.Users.UserTime = 0;
            //  txt_gatename.Text = si.GateName;
           
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
                DataTable dt1 = mas.Loginfinyear(Class.Users.Finyear,Class.Users.COMPCODE, "ASPTBLSEQMAS");
                if (dt1.Rows.Count > 0)
                {
                    combofinyearIN.DisplayMember = "finyear";
                    combofinyearIN.ValueMember = "gtfinancialyearid";
                    combofinyearIN.DataSource = dt1;
                    Class.Users.Finyear = dt1.Rows[0]["finyear"].ToString();
                    combofinyearout.DisplayMember = "finyear";
                    combofinyearout.ValueMember = "gtfinancialyearid";
                    combofinyearout.DataSource = dt1;
                }
               
               
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
           
            txt_qrcodein.Text = "";
            txtqrcodeout.Text = "";
            txt_qrcodein.Focus(); Class.Users.UserTime = 0;
            butheader.BackColor = Class.Users.BackColors;
            panel2.BackColor = Class.Users.BackColors;

          
            button1.BackColor = Class.Users.BackColors;
            button5.BackColor = Class.Users.BackColors;
          
            this.BackColor = Class.Users.BackColors;
          
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
      

        }

       
      

        private void Panelbutexit_Click(object sender, EventArgs e)
        {
           
        }

        private void Panelbutinexit_Click(object sender, EventArgs e)
        {
      Class.Users.Intimation = "PAYROLL";
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
          Class.Users.Intimation = "PAYROLL";
             Class.Users.UserTime = 0;
        }

        private void Bunin_Click(object sender, EventArgs e)
        {
        
            
            //tabControl1.TabPages.Remove(tabGOODSIN);
            //tabControl1.TabPages.Remove(tabGOODSOut);
 Class.Users.Intimation = "PAYROLL";
      Class.Users.UserTime = 0;

        }

    
      

        private void Butempoutwardclear_Click(object sender, EventArgs e)
        {
      
            bytes = null;  Class.Users.UserTime = 0;
 Class.Users.Intimation = "PAYROLL";
      
        }



        private void Button6_Click(object sender, EventArgs e)
        {
            //tabControl1.TabPages.Remove(tabPage5); tabControl1.SelectTab(tabPage2); 
        }

        

        private void TabControl1_Click(object sender, EventArgs e)
        {
       
            tabControl1.TabPages.Remove(tabGOODSIN); 
            tabControl1.TabPages.Remove(tabGOODSOut);
            Class.Users.UserTime = 0;
        }

        private void Butempinward_Click(object sender, EventArgs e)
        {
            Class.Users.UserTime = 0; Class.Users.Intimation = "PAYROLL";
           
        }

        private void Butempout_Click(object sender, EventArgs e)
        {
            Class.Users.UserTime = 0; Class.Users.Intimation = "PAYROLL";

            
        }

        private void Btnempinwardclear_Click(object sender, EventArgs e)
        {
         
            tabControl1.TabPages.Remove(tabGOODSIN);
            tabControl1.TabPages.Remove(tabGOODSOut);
            Class.Users.Intimation = "PAYROLL";
            bytes = null; Class.Users.UserTime = 0;
        }
        public void autonoIN()
        {
            if (txt_qrcodein.Text != "")
            {
                try
                {
                   
                        string sel1 = "SELECT  MAX(TO_NUMBER(A."+combo_compcode.Text+"IN))+1  ID  FROM  ASPINVENTRY A JOIN GTCOMPMAST B ON A.COMPCODE=B.GTCOMPMASTID   JOIN gtfinancialyear C ON C.GTFINANCIALYEARID=A.FINYEAR and C.CURRENTFINYR = 'T'   JOIN ASPTBLSEQMAS D ON D.COMPCODE=A.COMPCODE AND  D.COMPCODE=B.GTCOMPMASTID   and A.FINYEAR=C.GTFINANCIALYEARID AND D.FINYEAR=A.FINYEAR   AND   D.ACTIVE='T'  WHERE C.FINYR='" + Class.Users.Finyear + "' and A.COMPCODE='" + Class.Users.COMPCODE + "' AND  A.CREATEDON >=  SUBSTR(D.CREATEDON,0,10)";

                        DataSet ds = Utility.ExecuteSelectQuery(sel1, "ASPINVENTRY");
                        DataTable dt = ds.Tables["ASPINVENTRY"];
                        Int64 cnt = Convert.ToInt64("0" + dt.Rows[0]["ID"].ToString());
                        if (cnt == 0)
                        {
                            string sel2 = "  SELECT MAX(TO_NUMBER(A.INWARDNO)) INWARDNO    FROM  ASPTBLSEQMAS A JOIN GTCOMPMAST B ON A.COMPCODE=B.GTCOMPMASTID JOIN gtfinancialyear C ON C.GTFINANCIALYEARID=A.FINYEAR  and C.CURRENTFINYR='T'  AND  A.ACTIVE='T' AND  A."+combo_compcode.Text+"IN='T'  WHERE A.FINYEAR='" + combofinyearIN.SelectedValue + "' AND A.COMPCODE='" + Class.Users.COMPCODE + "'";
                            DataSet ds2 = Utility.ExecuteSelectQuery(sel2, "ASPTBLSEQMAS");
                            DataTable dt2 = ds2.Tables["ASPTBLSEQMAS"];
                            txtgatedcno1.Text = dt2.Rows[0]["INWARDNO"].ToString();
                        //txtgatedcno1.Text = Convert.ToString(txtgatedcno1.Text);
                        txtgatedcnoIN.Text = Class.Users.Finyear + "/" + Class.Users.HCompcode + "/" + txtgatedcno1.Text;
                            return;
                        }
                        else
                        {

                            txtgatedcno1.Text = Convert.ToInt64("0" + dt.Rows[0]["ID"].ToString()).ToString();
                        //si.OTHIN = Convert.ToString(txtgatedcno1.Text);
                        txtgatedcnoIN.Text = Class.Users.Finyear + "/" + Class.Users.HCompcode + "/" + txtgatedcno1.Text;
                            btnsavesIN.Focus();
                            return;
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
                   
                        string sel1 = "SELECT  MAX(TO_NUMBER(A."+combo_compcodeout.Text+"OUT))+1  ID  FROM  ASPINVENTRY A JOIN GTCOMPMAST B ON A.COMPCODE=B.GTCOMPMASTID   JOIN gtfinancialyear C ON C.GTFINANCIALYEARID=A.FINYEAR and C.CURRENTFINYR = 'T'   JOIN ASPTBLSEQMAS D ON D.COMPCODE=A.COMPCODE AND  D.COMPCODE=B.GTCOMPMASTID   and A.FINYEAR=C.GTFINANCIALYEARID AND D.FINYEAR=A.FINYEAR   AND   D.ACTIVE='T'  WHERE C.FINYR='" + Class.Users.Finyear + "' and A.COMPCODE='" + Class.Users.COMPCODE + "' AND  A.CREATEDON >=  SUBSTR(D.CREATEDON,0,10)";
                        DataSet ds3 = Utility.ExecuteSelectQuery(sel1, "ASPINVENTRY");
                        DataTable dt3 = ds3.Tables["ASPINVENTRY"];
                        Int64 cnt = Convert.ToInt64("0" + dt3.Rows[0]["ID"].ToString());
                        if (cnt == 0)
                        {

                            string sel4 = "  SELECT MAX(A.OUTWARDNO ) OUTWARDNO    FROM  ASPTBLSEQMAS A JOIN GTCOMPMAST B ON A.COMPCODE=B.GTCOMPMASTID JOIN gtfinancialyear C ON C.GTFINANCIALYEARID=A.FINYEAR  and C.CURRENTFINYR='T'  AND  A.ACTIVE='T' AND  A."+combo_compcodeout.Text+"OUT='T'   WHERE A.FINYEAR='" + combofinyearout.SelectedValue + "' AND A.COMPCODE='" + Class.Users.COMPCODE + "'";
                            DataSet ds4 = Utility.ExecuteSelectQuery(sel4, "ASPTBLSEQMAS");
                            DataTable dt4 = ds4.Tables["ASPTBLSEQMAS"];
                            txtgatedcno1.Text = dt4.Rows[0]["OUTWARDNO"].ToString();
                        // si.OTHOUT = Convert.ToString(txtgatedcno1.Text);
                        txtgatedcnoout.Text = Class.Users.Finyear + "/" + Class.Users.HCompcode + "/" + txtgatedcno1.Text; return;
                        }
                        else
                        {

                            txtgatedcno1.Text = Convert.ToInt64("0" + dt3.Rows[0]["ID"].ToString()).ToString();
                        //si.OTHOUT = Convert.ToString(txtgatedcno1.Text);
                        txtgatedcnoout.Text = Class.Users.Finyear + "/" + Class.Users.HCompcode + "/" + txtgatedcno1.Text; return;
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
                string sel0 = "  SELECT A." + combo_compcode.Text + "IN    FROM  ASPINVENTRY A  WHERE   A.COMPCODE='" + combo_compcode.SelectedValue + "'";
                DataSet ds0 = Utility.ExecuteSelectQuery(sel0, "ASPINVENTRY");
                DataTable dt0 = ds0.Tables["ASPINVENTRY"];
                if (dt0 == null)
                {
                    string altertable = "alter table ASPINVENTRY add " + combo_compcode.Text + "IN  VARCHAR2(25)";
                    Utility.ExecuteNonQuery(altertable);
                    string altertable1 = "alter table ASPINVENTRY add " + combo_compcode.Text + "OUT  VARCHAR2(25)";
                    Utility.ExecuteNonQuery(altertable1);

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

                        string ins = "INSERT INTO ASPINVENTRY(FINYEAR,GATEDCNO,QrCode,Category,"+combo_compcode.Text+ "IN,SystemDate,SystemTime,CompCode,userName,GateName,Modified,CreatedOn,IpAddress)" +
                            "  VALUES(" + combofinyearIN.SelectedValue + ",'" + txtgatedcnoIN.Text + "','" + txt_qrcodein.Text.ToUpper() + "','" + txtcategoryin.Text  + "','" + txtgatedcno1.Text + "'," +
                            "'" +txt_sysdate.Text+ "','" + txt_systime.Text + "','" + combo_compcode.SelectedValue + "','" + combo_username.SelectedValue + "','" + txt_gatename.Text.ToUpper() + "' ,to_date('" + Convert.ToDateTime(System.DateTime.Now.ToString()).ToString() + "', 'dd/MM/yyyy hh24:MI:SS'),to_date('" + Convert.ToDateTime(System.DateTime.Now.ToString()) + "', 'dd/MM/yyyy hh24:MI:SS'),'" + txt_ipaddress.Text + "')";
                        Utility.ExecuteNonQuery(ins);
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
                string sel0 = "  SELECT A." + combo_compcodeout.Text + "IN    FROM  ASPINVENTRY A  WHERE   A.COMPCODE='" + combo_compcodeout.SelectedValue + "'";
                DataSet ds0 = Utility.ExecuteSelectQuery(sel0, "ASPINVENTRY");
                DataTable dt0 = ds0.Tables["ASPINVENTRY"];
                if (dt0 == null)
                {
                    string altertable = "alter table ASPINVENTRY add " + combo_compcodeout.Text + "IN  VARCHAR2(25)";
                    Utility.ExecuteNonQuery(altertable);
                    string altertable1 = "alter table ASPINVENTRY add " + combo_compcodeout.Text + "OUT  VARCHAR2(25)";
                    Utility.ExecuteNonQuery(altertable1);

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
                        string ins = "INSERT INTO ASPINVENTRY(FINYEAR,GATEDCNO,QrCode,Category," + combo_compcodeout.Text + "OUT,SystemDate,SystemTime,CompCode,userName,GateName,Modified,CreatedOn,IpAddress)" +
                            "  VALUES('" + combofinyearout.SelectedValue + "','" + txtgatedcnoout.Text + "','" + txtqrcodeout.Text + "','" + txtcategoryout.Text + "','"+ txtgatedcno1.Text+"'," +
                            "'" + txt_sysdate.Text + "','" + txt_systime.Text + "','" + combo_compcodeout.SelectedValue + "','" + combo_usernameout.SelectedValue + "','" + txtgatenameout.Text.ToUpper() + "' ,to_date('" + Convert.ToDateTime(System.DateTime.Now.ToString()).ToString() + "', 'dd/MM/yyyy hh24:MI:SS'),to_date('" + Convert.ToDateTime(System.DateTime.Now.ToString()) + "', 'dd/MM/yyyy hh24:MI:SS'),'" + txt_ipaddress.Text + "')";
                        Utility.ExecuteNonQuery(ins);
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
      
           
            txtqrcodeout.Select();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            tabControl1.TabPages.Add(tabGOODSIN);
            tabControl1.SelectTab(tabGOODSIN);
            Class.Users.UserTime = 0; Class.Users.Intimation = "";
    
            
            txt_qrcodein.Select();
        }

        private void TabEMPInOut_Click(object sender, EventArgs e)
        {
         
        }

      
        private void Btngoodsoutexit_Click(object sender, EventArgs e)
        {
            this.tabControl1.TabPages.Remove(tabGOODSOut);
            tabControl1.TabPages.Add(tabGOODSIN); Class.Users.Intimation = "";
            tabControl1.SelectTab(tabGOODSIN); Class.Users.UserTime = 0;
            lblRecordnoOUT.Visible = false; lblRecorddatetimeOUT.Visible = false;
            lblRecordnoOUT.Text = ""; lblRecorddatetimeOUT.Text = ""; Class.Users.UserTime = 0;
            txt_qrcodein.Focus();
        }

        private void BTNGoodsINEXIT_Click(object sender, EventArgs e)
        {
            this.tabControl1.TabPages.Remove(tabGOODSIN);
            tabControl1.TabPages.Add(tabGOODSOut); Class.Users.Intimation = "";
            tabControl1.SelectTab(tabGOODSOut); Class.Users.UserTime = 0;
            lblRecordno.Visible = false; lblRecorddatetime.Visible = false;
            lblRecordno.Text = ""; lblRecorddatetime.Text = ""; Class.Users.UserTime = 0;
            txtqrcodeout.Focus();
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
            
        }

        private void btninwardclear_Click(object sender, EventArgs e)
        {
           
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
                //DataTable findt = mas.finyear();
                //combofinyearIN.DataSource = findt;
                //combofinyearIN.DisplayMember = "FINYEAR";
                //combofinyearIN.ValueMember = "gtfinancialyearid";
                //combofinyearout.DataSource = findt;
                //combofinyearout.DisplayMember = "FINYEAR";
                //combofinyearout.ValueMember = "gtfinancialyearid";
                //Class.Users.Finyear = findt.Rows[0]["FINYEAR"].ToString();

            }
            if (tabControl1.SelectedTab == tabControl1.TabPages["tabEMPInOut"])//your specific tabname
            {
                Class.Users.Intimation = "PAYROLL";
                //DataTable findt = mas.finyear();
                //combofinyearIN.DataSource = findt;
                //combofinyearIN.DisplayMember = "FINYEAR";
                //combofinyearIN.ValueMember = "gtfinancialyearid";
                //Class.Users.Finyear = findt.Rows[0]["FINYEAR"].ToString();
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
