using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Drawing;
using System.Windows.Forms;
namespace Pinnacle
{
    public partial class LoginForm : Form
    {
        Models.Master mas = new Models.Master();
        Models.UserRights sm = new Models.UserRights(); 

        private static string s = ConfigurationManager.ConnectionStrings["ConnectionString1"].ConnectionString;
        String[] data = s.Split(',');
        string[] parts;
        string systemuser = "";
        public LoginForm()
        {
            InitializeComponent();
  
            Class.Users.ConString = s;
            // -----------------------------
            // SAFE SPLIT
            // -----------------------------
            parts = s.Split(',');
            string[] parts2 = s.Split(';');

            string d0 = parts.Length > 0 ? parts[0] : "";
            string d1 = parts.Length > 1 ? parts[1] : "";
            string d2 = parts.Length > 2 ? parts[2] : "";
            string d3 = parts.Length > 3 ? parts[3] : "";

            string d2U = d2.ToUpper();
            string d3U = d3.ToUpper();

            // -----------------------------------------
            // COMMON ASSIGNMENTS
            // -----------------------------------------
            Class.Users.HUnitSub = d2U;
            Class.Users.HCompcode = d1.ToUpper();
            Class.Users.Intimation = "";
            Class.Users.ProjectID = "";


            // -----------------------------------------
            // 1) HOSTEL LOGIC
            // -----------------------------------------
            if (d2U == "HOSTEL")
            {
                Class.Users.HUnit = "HOSTEL";
            }

            // -----------------------------------------
            // 2) CANTEEN LOGIC
            // -----------------------------------------
            else if (d3U == "CANTEEN")
            {
                Class.Users.HUnit = "CANTEEN";
                Class.Users.HCompcode = d2U;
                combo_compcode.Text = d2U;
            }
            else
            {
                // default case
                Class.Users.HUnit = "";

                if (parts.Length >= 4)
                {
                    Class.Users.HUnit = d3;
                    Class.Users.Intimation = "PAYROLL";
                    Class.Users.ProjectID = "PSSPAYROLL";
                }
            }


            // -----------------------------------------
            // 3) HOSTEL TYPE BASED ON GENDER CODE
            // -----------------------------------------
            switch (d2U)
            {
                case "F":
                    Class.Users.ProjectID = "PSSPAYROLL";
                    Class.Users.HostelName = "WOMENS HOSTEL";
                    Class.Users.Intimation = "PAYROLL";
                    break;

                case "M":
                    Class.Users.ProjectID = "PSSPAYROLL";
                    Class.Users.HostelName = "AGF";
                    Class.Users.Intimation = "PAYROLL";
                    break;

                case "G":
                    Class.Users.ProjectID = "PSSPAYROLL";
                    Class.Users.HostelName = "GENTS STAFF HOSTEL";
                    Class.Users.Intimation = "PAYROLL";
                    break;

                case "W":
                    Class.Users.ProjectID = "PSSPAYROLL";
                    Class.Users.HostelName = "WORKING GENTS HOSTEL";
                    Class.Users.Intimation = "PAYROLL";
                    break;
            }


            // -----------------------------------------
            // 4) SPECIAL COMPANIES
            // -----------------------------------------
            string[] specialCodes = { "AGF", "VEL", "FLFD", "AGFM", "AGFMGII" };

            if (specialCodes.Equals(d2U))
            {
                combo_compcode.Text = d2U;
                Class.Users.Intimation = "PAYROLL";

                if (d2U == "AGF")
                    Class.Users.HostelName = "AGF";
              
            }


            // -----------------------------------------
            // 5) PROCESS PROJECT ID (SECOND SPLIT)
            // -----------------------------------------
            if (parts2.Length > 1)
            {
                string[] kv = parts2[1].Split('=');

                if (kv.Length > 1)
                    Class.Users.ProjectID = kv[1].ToUpper();
            }

            // -----------------------------------------
            // 6) FINAL ASSIGNMENTS
            // -----------------------------------------
            Class.Users.DataBase = parts2[0] + "  CompCode : " + Class.Users.HCompcode;
            combo_compcode.Text = Class.Users.HCompcode;      
        
            logo();


        }
        void logo()
        {
            try
            {
                Class.Users.Intimation = "PAYROLL";
                DataTable dtCC = new DataTable();
                dtCC = Utility.SQLQuery("SELECT LOGO AS EMPIMAGE FROM EDOCIMAGE WHERE IMGNAME = 'COMPLOGO' AND COMPANYID ='" + Class.Users.HCompcode + "' ");

                if (dtCC.Rows.Count > 0)
                {
                    foreach (DataRow myRow in dtCC.Rows)
                    {

                        if (myRow["EMPIMAGE"].ToString() != "")
                        {
                            byte[] bytes = (byte[])myRow["EMPIMAGE"];
                            Image img = Models.Device.ByteArrayToImage(bytes);

                            Class.Users.HCompcode = combo_compcode.Text;
                            button1.BackgroundImage = img;
                            this.BackColor = System.Drawing.Color.White; return;
                        }
                    }
                }
                else
                {
                    
                    dtCC = Utility.SQLQuery("SELECT LOGO AS EMPIMAGE FROM EDOCIMAGE WHERE IMGNAME = 'COMPLOGOID' AND COMPANYID ='" + Class.Users.HCompcode + "' ");
                    if (dtCC.Rows.Count > 0)
                    {
                        foreach (DataRow myRow in dtCC.Rows)
                        {

                            if (myRow["EMPIMAGE"].ToString() != "")
                            {
                                byte[] bytes = (byte[])myRow["EMPIMAGE"];
                                Image img = Models.Device.ByteArrayToImage(bytes);

                                Class.Users.HCompcode = combo_compcode.Text;
                                button1.BackgroundImage = img;
                                this.BackColor = System.Drawing.Color.White; return;
                            }
                        }
                    }
                    else
                    {
                        Class.Users.HCompcode = combo_compcode.Text;
                        if (Class.Users.HUnitSub == "pssagfsample") { button1.BackgroundImage = Pinnacle.Properties.Resources.Anugraha_logo; }
                        else
                        {
                            button1.BackgroundImage = Pinnacle.Properties.Resources.pinacle;
                        }
                    }
                }
            }
            catch (Exception EX) { if (data[2] == "pssagfsample") { button1.BackgroundImage = Pinnacle.Properties.Resources.Anugraha_logo; } }

        }
        private void combo_compcode_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(combo_compcode.Text))
            {
                e.Cancel = true;
                combo_compcode.Focus();
                ErrProvider.SetError(combo_compcode, "Invalid CompCoode");
            }
            else
            {
                e.Cancel = false;
                ErrProvider.SetError(txtusername, null);
            }
        }
        private void txtusername_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //if (string.IsNullOrEmpty(txtusername.Text))
            //{
            //    e.Cancel = true;
            //    txtusername.Focus();
            //    ErrProvider.SetError(txtusername, "Invalid Username");
            //}
            //else
            //{
            //    e.Cancel = false;
            //    ErrProvider.SetError(txtusername, null);
            //} 
        }
        private void txt_password_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //if (string.IsNullOrEmpty(txt_password.Text))
            //{
            //    e.Cancel = true;
            //    txt_password.Focus();
            //    ErrProvider.SetError(txt_password, "Invalid Password");
            //}
            //else
            //{
            //    e.Cancel = false;
            //    ErrProvider.SetError(txt_password, null);
            //}

        }
        private void Btn_sumbit_Click(object sender, EventArgs e)
        {
            try
            {
                Class.Users.OSUser = Environment.UserName;
               
                // Basic validation
                if (string.IsNullOrWhiteSpace(combo_compcode.Text))
                {
                    MessageBox.Show("Compcode is Empty ", "null", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtusername.Text))
                {
                    MessageBox.Show("UserName is Empty ", "null", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (string.IsNullOrWhiteSpace(txt_password.Text))
                {
                    MessageBox.Show("Password is Empty", "null", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Common assignments
          
                Class.Users.HCompcode = combo_compcode.Text;

                string comp = combo_compcode.Text.ToUpper();
                //string[] parts = txtusername.Text.Split(',');
                //string baseUser = parts[0].Trim();
                //string suffix = parts.Length > 1 ? parts[2].Trim().ToUpper() : "";

                // Always set HUserName = base part
                //Class.Users.HUserName = baseUser;
                //txtusername.Text = baseUser;

                // Default
                Class.Users.Intimation = "PAYROLL";
                Class.Users.ProjectID = "PSSPAYROLL";

                // Special AGFM case
                //if (comp == "AGFM" && baseUser == "HR")
                //{
                //    switch (suffix)
                //    {
                //        case "F":
                //            Class.Users.HostelName = "WOMENS HOSTEL";
                //            break;

                //        case "M":
                //            Class.Users.HostelName = "ALL";
                //            break;

                //        case "G":
                //            Class.Users.HostelName = "GENTS STAFF HOSTEL";
                //            break;

                //        case "W":
                //            Class.Users.HostelName = "WORKING GENTS HOSTEL";
                //            break;

                //        default:
                //            // "HR" only (no suffix)
                //            Class.Users.HostelName = comp;
                //            break;
                //    }
                //}

                // Continue your existing code from here (unchanged)
                combo_compcode.Focus();
                Class.Users.HUserName = txtusername.Text;

                string SS = sm.Encrypt(txt_password.Text);
                Class.Users.PWORD = SS;
                Class.Users.HCompcode = combo_compcode.Text;

                systemuser = Environment.UserName;

                DataTable dtCC = Utility.SQLQuery(
                    "select MAX(TO_NUMBER(A.SESSIONTIME)) AS SESSIONTIME " +
                    "from asptblusermas A join gtcompmast b on a.compcode=b.gtcompmastid " +
                    "where b.compcode='" + Class.Users.HCompcode + "' and  a.username='" + Class.Users.HUserName + "'"
                );

                Class.Users.LoginTime = Convert.ToInt32(dtCC.Rows[0]["SESSIONTIME"].ToString());

                string sel10 =
                    "SELECT X.SID,X.SERIAL#, X.OSUSER,X.LOGON_TIME,X.STATUS,X.PREV_EXEC_START ,X.LAST_CALL_ET,X.program " +
                    "FROM( SELECT A.SID,A.SERIAL#, A.OSUSER,A.LOGON_TIME,A.STATUS,A.PREV_EXEC_START ,A.LAST_CALL_ET,a.program " +
                    "FROM v$SESSION a where A.PROGRAM='Pinnacle.exe' " +
                    "UNION " +
                    "select A.SID,A.SERIAL#, A.OSUSER,A.LOGON_TIME,A.STATUS,A.PREV_EXEC_START ,A.LAST_CALL_ET,a.program " +
                    "FROM v$SESSION a where A.PROGRAM='Pinnacle.exe')X " +
                    "WHERE X.OSUSER='" + systemuser + "' ORDER BY 1";

                DataSet ds10 = Utility.ExecuteSelectQuery(sel10, "dual");
                DataTable dt10 = ds10.Tables["dual"];

                if (dt10.Rows.Count > 1)
                {
                    Application.Exit();
                }

                if (dt10.Rows.Count == 1)
                {
                    string sel =
                    "select  distinct a.compcode as cc ,b.userid, b.username ,a.compname ,b.gatename,a.gtcompmastid,b.SESSIONTIME " +
                    "from gtcompmast  a join asptblusermas b on a.gtcompmastid = b.compcode " +
                    "where a.compcode='" + Class.Users.HCompcode + "' and b.username='" + Class.Users.HUserName +
                    "' and b.pasword = '" + Class.Users.PWORD + "' and  b.active='T' order by 1";

                    DataSet ds = Utility.ExecuteSelectQuery(sel, "asptblusermas");
                    DataTable dt = ds.Tables["asptblusermas"];

                    if (dt.Rows.Count > 0)
                    {
                        if (Convert.ToInt32("0" + dt.Rows[0]["SESSIONTIME"]) <= 0)
                        {
                            Class.Users.UserTime = Convert.ToInt32("0" + dt.Rows[0]["SESSIONTIME"]);
                            string sql = @"UPDATE ASPTBLUSERMAS   SET SESSIONTIME = :SESSIONTIME               WHERE USERID = :USERID               AND COMPCODE = :COMPCODE";
                            System.Data.OracleClient.OracleParameter[] parameters ={
                                new System.Data.OracleClient.OracleParameter("SESSIONTIME", OracleDbType.Int32)
                                { Value = 600 },

                                new System.Data.OracleClient.OracleParameter("USERID", OracleDbType.Int32)
                                { Value = dt.Rows[0]["userid"] },

                                new System.Data.OracleClient.OracleParameter("COMPCODE", OracleDbType.Int32)
                                { Value = dt.Rows[0]["gtcompmastid"] }
                            };

                            int result = Utility.ExecuteNonQuery(sql, parameters);

                            if (result > 0)
                                MessageBox.Show("Session Time Updated");
                            else
                                MessageBox.Show("Update Failed");

                        }

                        Class.Users.HUserName = dt.Rows[0]["username"].ToString();
                        Class.Users.USERID = Convert.ToInt64(dt.Rows[0]["userid"].ToString());
                        Class.Users.HGateName = DateTime.Now.Year + "/" + dt.Rows[0]["gatename"];
                        Class.Users.HCompName = dt.Rows[0]["compname"].ToString();
                        Class.Users.COMPCODE = Convert.ToInt64(dt.Rows[0]["gtcompmastid"]);
                        Class.Users.LoginTime = Convert.ToInt64(dt.Rows[0]["SESSIONTIME"]);

                        systemuser = Environment.UserName;

                        DataTable dt1 = sm.headerdropdowns(Class.Users.HCompcode, Class.Users.HUserName);
                        Class.Users.GlobleVariable =
                            dt1.Rows.Count > 0 && (dt1.Rows[0]["LOGIN"].ToString() == "T" || Class.Users.HUserName == "VAIRAM");

                        this.Hide();
                        PinnacleMdi si = new PinnacleMdi();
                        si.Show();
                    }
                    else
                    {
                        MessageBox.Show("Invalid UserName and Password ", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch
            {
                MessageBox.Show("Invalid.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            

        }
        private void LoginForm_Load(object sender, EventArgs e)
        {
            try
            {
                txtusername.Text = ""; txt_password.Text = "";               
                systemuser = Environment.UserName;
                Class.Users.Intimation = "PAYROLL";
                Class.Users.HUserName = txtusername.Text;

                string sel10 = "SELECT X.SID,X.SERIAL#, X.OSUSER,X.LOGON_TIME,X.STATUS,X.PREV_EXEC_START ,X.LAST_CALL_ET,X.program  FROM( SELECT A.SID,A.SERIAL#, A.OSUSER,A.LOGON_TIME,A.STATUS,A.PREV_EXEC_START ,A.LAST_CALL_ET,a.program FROM gV$SESSION a  where  A.PROGRAM='Pinnacle.exe'   UNION  select A.SID,A.SERIAL#, A.OSUSER,A.LOGON_TIME,A.STATUS,A.PREV_EXEC_START ,A.LAST_CALL_ET,a.program  FROM gV$SESSION a   where    A.PROGRAM='Pinnacle.exe' )X WHERE X.OSUSER='" + systemuser + "'  ORDER BY x.LOGON_TIME ASC";
                DataSet ds10 = Utility.ExecuteSelectQuery(sel10, "GTCOMPMAST");
                DataTable dt10 = ds10.Tables["GTCOMPMAST"];
                if (dt10.Rows.Count >= 2)
                {
                    try
                    {                        
                        foreach (DataRow str in dt10.Rows)
                        {
                            string del11 = "ALTER SYSTEM KILL SESSION '" + str[0].ToString() + "," + str[1].ToString() + "' IMMEDIATE";
                            Utility.ExecuteNonQuery(del11);
                        }
                    }
                    catch (Exception ex)
                    {
                        Application.Exit();
                    }
                }
                if (Class.Users.HUnitSub == "pssagfsample")
                {

                    combofinyear.Text = System.DateTime.Now.Year.ToString(); txtusername.Select();
                    Class.Users.Finyear = System.DateTime.Now.Year.ToString();
                }
                else
                {
                    DataTable findt = mas.finyear();
                    combofinyear.DataSource = findt;
                    combofinyear.DisplayMember = "FINYEAR";
                    combofinyear.ValueMember = "gtfinancialyearid"; txtusername.Select();
                    Class.Users.Finyear = findt.Rows[0]["FINYEAR"].ToString();
                }
            }
            catch(Exception ex)
            {
                //MessageBox.Show("Invalid."+ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

     
            

     

  
      
        private void Btn_Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Txt_password_Enter(object sender, EventArgs e)
        {
            txt_password.BackColor = System.Drawing.Color.Wheat;
            System.Windows.Forms.Clipboard.Clear();
        }

        

        private void LoginForm_FormClosed(object sender, FormClosedEventArgs e)
        {

            Application.Exit();     
        }

        private void Buttblcreate_Click(object sender, EventArgs e)
        {
            Tables.butdroptable tt = new Tables.butdroptable();
            tt.Show(); //Class.Users.UserTime = 0;
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            buttblcreate.Visible = true;
           
        }

        private void refreshToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            
                systemuser = Environment.UserName;
            string sel10 = "SELECT X.SID,X.SERIAL# FROM( SELECT A.SID,A.SERIAL#, A.OSUSER,A.LOGON_TIME,A.STATUS,A.PREV_EXEC_START ,A.LAST_CALL_ET,a.program FROM v$SESSION a  where  A.PROGRAM='Pinnacle.exe'   UNION  select A.SID,A.SERIAL#, A.OSUSER,A.LOGON_TIME,A.STATUS,A.PREV_EXEC_START ,A.LAST_CALL_ET,a.program  FROM v$SESSION a   where    A.PROGRAM='Pinnacle.exe' )X WHERE X.OSUSER='" + systemuser + "'  ORDER BY 1";
            DataSet ds10 = Utility.ExecuteSelectQuery(sel10, "dual");
            DataTable dt10 = ds10.Tables["dual"];
            if (dt10.Rows.Count > 0)
            {
                foreach (DataRow str in dt10.Rows)
                {
                    string del11 = "ALTER SYSTEM KILL SESSION '" + str[0].ToString() + "," + str[1].ToString() + "' IMMEDIATE";
                    Utility.ExecuteNonQuery(del11);
                }
               
            }
            else
            {
                
            }

        }

        private void txt_password_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
            }

            if (e.Control == true)
            {
                switch (e.KeyCode)
                {
                    case Keys.C:
                    case Keys.P:
                    case Keys.X:
                        e.Handled = true;
                        txt_password.SelectionLength = 0;
                        break;
                }
            }
        }

        private void txtusername_Enter(object sender, EventArgs e)
        {
            System.Windows.Forms.Clipboard.Clear();
        }

        private void combo_compcode_TextChanged(object sender, EventArgs e)
        {
            if (combo_compcode.Text.Length>=3)
            {
                logo();
            }
        }

        private void combofinyear_SelectedIndexChanged(object sender, EventArgs e)
        {
            Class.Users.Finyear = combofinyear.Text;
        }
    }
}
