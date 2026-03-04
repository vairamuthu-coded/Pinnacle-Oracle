using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Pinnacle.Hostel
{
    public partial class GatePassManual : Form,ToolStripAccess
    {
        private static GatePassManual _instance;
        public GatePassManual()
        {
            InitializeComponent();
            Class.Users.Intimation = "PAYROLL";
            DateTime dateForButton = DateTime.Now;
            frmdate.Value = dateForButton;
            Class.Users.IPADDRESS = GenFun.GetLocalIPAddress();
            Class.Users.SysDate = Convert.ToString(System.DateTime.Now.ToString("dd-MM-yyyy"));
            Class.Users.SysTime = Convert.ToString(DateTime.Now.ToLongTimeString().ToString());
            usercheck(Class.Users.HCompcode, Class.Users.HUserName, Class.Users.ScreenName);
        
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
                       if (dt1.Rows[r]["DELETES"].ToString() == "T") { GlobalVariables.Deletes.Visible = false;txtintime.Enabled = true;txtoutime.Enabled = true; } else { GlobalVariables.Deletes.Visible = false; txtintime.Enabled = false; txtoutime.Enabled = false; }
                       
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
        public static GatePassManual Instance
        {
            get
            {
                if (_instance == null) _instance = new GatePassManual();
                GlobalVariables.CurrentForm = _instance; return _instance;
            }

        }
        Models.Master mas = new Models.Master();
        Models.UserRights sm = new Models.UserRights();
        private void hostelload(string MID)
        {
            try
            {

                // if (combohostel.Text == "MENS HOTEL" || combohostel.Text == "WORKING GENTS HOSTEL" || combohostel.Text == "WOMENS HOSTEL" || combohostel.Text == "GENTS STAFF HOSTEL" || combohostel.Text == "BOYS HOSTEL")
                // {

                Class.Users.Intimation = "PAYROLL";
                string sel1 = "SELECT   '' AS HOSTELNAME FROM DUAL  UNION ALL SELECT   'AGFMGII' AS HOSTELNAME FROM DUAL  UNION ALL  SELECT A.HOSTELNAME FROM HOSTELLIVEDATA A JOIN GTCOMPMAST B ON B.COMPCODE= A.COMPCODE   JOIN HREMPLOYMAST C ON C.COMPCODE = B.GTCOMPMASTID AND C.IDCARDNO = A.IDCARDNO   JOIN HREMPLOYDETAILS D ON C.HREMPLOYMASTID = D.HREMPLOYMASTID AND D.HOSTEL='YES' AND D.IDACTIVE='YES'   JOIN GTDEPTDESGMAST E ON E.GTDEPTDESGMASTID = D.DEPTNAME   WHERE D.MIDCARD='" + MID+ "' ORDER BY HOSTELNAME DESC";
                DataSet ds = Utility.ExecuteSelectQuery(sel1, "HOSTELLIVEDATA");
               DataTable dt3 = ds.Tables["HOSTELLIVEDATA"];

                if (dt3.Rows.Count > 0)
                {
                    combohosteltype.DisplayMember = "HOSTELNAME";
                    combohosteltype.ValueMember = "HOSTELNAME";
                    combohosteltype.DataSource = dt3;


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Data Source Not Connected" + ex.Message);
            }
        }
        private void reason()
        {
            try
            {
                string sel3 = " select 0 as ASPTBLREASONMASID,'' REASON from dual union all  SELECT  C.ASPTBLREASONMASID,C.REASON  FROM  GTCOMPMAST A JOIN  asptblusermas B ON A.GTCOMPMASTID= B.COMPCODE   JOIN ASPTBLREASONMAS C ON C.COMPCODE=A.GTCOMPMASTID     WHERE C.ACTIVE='T'   AND A.COMPCODE='" + Class.Users.HCompcode + "'   AND B.USERNAME='" + Class.Users.HUserName + "'";
                DataSet ds3 = Utility.ExecuteSelectQuery(sel3, "ASPTBLREASONMAS");
                DataTable dt3 = ds3.Tables["ASPTBLREASONMAS"];
                if (dt3.Rows.Count > 0)
                {
                    comboreason.DisplayMember = "REASON";
                    comboreason.ValueMember = "ASPTBLREASONMASID";
                    comboreason.DataSource = dt3;


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Data Source Not Connected" + ex.Message);
            }
        }
        private void passnocheck(string PSS)
        {
            
                Class.Users.Intimation = "PAYROLL";
                string sel = "SELECT DISTINCT A.ASPTBLHOSTELGATEPASSID,D.MIDCARD AS IDCARDNO,G.REASON ,G.ASPTBLREASONMASID,A.HOSTELNAME, '' HOSTELBLOCK,'' AS HOSTELROOM,   '' CONTACTNO,substr(A.OUTTIME, 11, 18) outtime,substr(A.INTIME, 11, 18) intime FROM ASPTBLHOSTELGATEPASS A  JOIN GTCOMPMAST B ON B.GTCOMPMASTID = A.COMPCODE     JOIN HREMPLOYMAST    C ON C.COMPCODE = B.GTCOMPMASTID AND C.COMPCODE = A.COMPCODE   JOIN HREMPLOYDETAILS D ON D.HREMPLOYMASTID = C.HREMPLOYMASTID AND D.IDCARD = C.IDCARDNO    AND D.DEPTNAME = A.DEPARTMENT AND D.MIDCARD = A.IDCARDNO   JOIN GTDEPTDESGMAST  E ON E.GTDEPTDESGMASTID = A.DEPARTMENT       JOIN ASPTBLREASONMAS G ON G.ASPTBLREASONMASID = A.REASON      WHERE  A.ASPTBLHOSTELGATEPASSID='" + PSS+"'  ORDER BY A.ASPTBLHOSTELGATEPASSID DESC";
                DataSet ds3 = Utility.ExecuteSelectQuery(sel, "ASPTBLHOSTELGATEPASS");
                DataTable dtP3 = ds3.Tables["ASPTBLHOSTELGATEPASS"];
                comboBox1.DataSource = null;txtoutime.Text = "";txtintime.Text = "";
            if (dtP3.Rows.Count > 0)
            {
                comboBox1.DisplayMember = "IDCARDNO";
                comboBox1.ValueMember = "IDCARDNO";
                comboBox1.DataSource = dtP3;
                comboreason.DisplayMember = "REASON";
                comboreason.ValueMember = "ASPTBLREASONMASID";
                comboreason.DataSource = dtP3;
                txtoutime.Text = dtP3.Rows[0]["outtime"].ToString();
                txtintime.Text = dtP3.Rows[0]["intime"].ToString();
            }
            
        }
        private void passnocheck()
        {
            combopassno.DataSource = null;
                string sel= "SELECT 0 ASPTBLHOSTELGATEPASSID,'' AS IDCARDN,'' ASttime,'' AS intime  FROM DUAL UNION ALL SELECT DISTINCT X.ASPTBLHOSTELGATEPASSID,X.IDCARDNO,X.outtime,X.intime  FROM(SELECT  A.ASPTBLHOSTELGATEPASSID,D.MIDCARD AS IDCARDNO,substr(A.OUTTIME, 11, 18) outtime,substr(A.INTIME, 11, 18) intime FROM ASPTBLHOSTELGATEPASS A  JOIN GTCOMPMAST B ON B.GTCOMPMASTID = A.COMPCODE     JOIN HREMPLOYMAST    C ON C.COMPCODE = B.GTCOMPMASTID AND C.COMPCODE = A.COMPCODE   JOIN HREMPLOYDETAILS D ON D.HREMPLOYMASTID = C.HREMPLOYMASTID AND D.IDCARD = C.IDCARDNO    AND D.DEPTNAME = A.DEPARTMENT AND D.MIDCARD = A.IDCARDNO   JOIN GTDEPTDESGMAST  E ON E.GTDEPTDESGMASTID = A.DEPARTMENT       JOIN ASPTBLREASONMAS G ON G.ASPTBLREASONMASID = A.REASON      WHERE A.MODIFIED= TO_DATE('" + frmdate.Value.ToString("dd-MM-yyyy") + "','dd-MM-yyyy')  AND A.INTIME IS NULL UNION  ALL SELECT  A.ASPTBLHOSTELGATEPASSID,D.MIDCARD AS IDCARDNO,substr(A.OUTTIME, 11, 18) outtime,substr(A.INTIME, 11, 18) intime FROM ASPTBLHOSTELGATEPASS A  JOIN GTCOMPMAST B ON B.GTCOMPMASTID = A.COMPCODE     JOIN HREMPLOYMAST    C ON C.COMPCODE = B.GTCOMPMASTID AND C.COMPCODE = A.COMPCODE   JOIN HREMPLOYDETAILS D ON D.HREMPLOYMASTID = C.HREMPLOYMASTID AND D.IDCARD = C.IDCARDNO    AND D.DEPTNAME = A.DEPARTMENT AND D.MIDCARD = A.IDCARDNO   JOIN GTDEPTDESGMAST  E ON E.GTDEPTDESGMASTID = A.DEPARTMENT       JOIN ASPTBLREASONMAS G ON G.ASPTBLREASONMASID = A.REASON  WHERE A.MODIFIED= TO_DATE('" + frmdate.Value.ToString("dd-MM-yyyy") + "','dd-MM-yyyy')    AND A.OUTTIME IS NULL     ) X ORDER BY 1";
                DataSet ds3 = Utility.ExecuteSelectQuery(sel, "ASPTBLHOSTELGATEPASS");
                DataTable dt3 = ds3.Tables["ASPTBLHOSTELGATEPASS"];
                if (dt3.Rows.Count > 0)
                {
                    combopassno.DisplayMember = "ASPTBLHOSTELGATEPASSID";
                    combopassno.ValueMember = "ASPTBLHOSTELGATEPASSID";
                    combopassno.DataSource = dt3;
                    lblpasscount.Refresh();
                    lblpasscount.Text = "Total :" + dt3.Rows.Count;

                }
           
        }
        private void IDCARDlOAD()
        {
            try
            {
                string sel3 = "SELECT DISTINCT B.MIDCARD   FROM HREMPLOYMAST A  JOIN HREMPLOYDETAILS B ON A.HREMPLOYMASTID=B.HREMPLOYMASTID JOIN GTCOMPMAST C ON C.GTCOMPMASTID=A.COMPCODE WHERE B.IDACTIVE='YES' and C.COMPCODE NOT  in 'AGF'  AND C.COMPCODE NOT  in 'VEL' AND C.COMPCODE NOT  in 'FLF'  AND C.COMPCODE NOT  in 'FLFD'  ORDER BY 1";
                DataSet ds3 = Utility.ExecuteSelectQuery(sel3, "HREMPLOYMAST");
                DataTable dt3 = ds3.Tables["HREMPLOYMAST"];
                if (dt3.Rows.Count > 0)
                {
                    comboBox1.DisplayMember = "MIDCARD";
                    comboBox1.ValueMember = "MIDCARD";
                    comboBox1.DataSource = dt3;


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Data Source Not Connected" + ex.Message);
            }
        }
        private void GatePassManual_Load(object sender, EventArgs e)
        {
         
            frmdate.Text = DateTime.Now.ToShortDateString();
            News();
        }

        public void News()
        {
            Class.Users.Intimation = "PAYROLL";
            reason(); IDCARDlOAD(); GridLoad();
                   Class.Users.UserTime = 0;txtempid.Text = "";combopassno.Text = "";
            butheader.Text = " GATE PASS - MANUAL ENTRY   " + System.DateTime.Now.ToString("MMMM") + "   - " + System.DateTime.Now.Year;

            txtcontactno.Text = ""; comboBox1.Text = "";
            combohostel.Text = "";
            combohostelblock.Text = "";
            combohostelroom.Text = ""; panel3.BackColor = Class.Users.BackColors;
            panel1.BackColor = Class.Users.BackColors;
            comboempname.Text = "";
            combo_dept.Text = "";
            txtsysdate.Text = "";
            txtsystime.Text = "";
            comboreason.Text = ""; comboreason.SelectedIndex = -1;
            txtmanualTime.Text = "";
            txtintime.Text = ""; txtoutime.Text = ""; txtRemarks.Text = "";
            comboreason.Enabled = true;
            txtmanualTime.Enabled = true; checknative.Checked = false;
            panel4.BackColor = Class.Users.BackColors;
            txtRemarks.Text = "";
            txtpermissionhrs.Enabled = true;
            butheader.BackColor = Class.Users.BackColors;
            this.Font = Class.Users.FontName;
            comboBox1.Focus();
        }

        public void Saves()
        {
            try
            {
                Class.Users.Intimation = "PAYROLL";
                DateTime endtime = Convert.ToDateTime(frmdate.Value.ToString("dd-MM-yyyy").Substring(0, 10) + " " + txtintime.Text);
                var statetime1 = Convert.ToDateTime(todate.Value.ToString("dd-MM-yyyy").Substring(0, 10) + " " + txtoutime.Text);
             
                TimeSpan differ2 = endtime - statetime1;
                Class.Users.UserTime = 0; Class.Users.Intimation = "PAYROLL";
                if (comboBox1.Text == "---select---") { MessageBox.Show("Invalid IdCardNo"); comboBox1.Focus();return; }
                if (txtoutime.Text == "" || txtoutime.Text.Length < 5) { MessageBox.Show("Invalid OutTime"); txtoutime.Select(); return; }
                if (txtintime.Text == "" || txtintime.Text.Length<5) { MessageBox.Show("Invalid InTime"); txtintime.Select(); return; }
                if (txtRemarks.Text == "") { MessageBox.Show("Invalid Manual Pass Number Field"); txtRemarks.Select(); return; }
                if (comboreason.Text == "") { MessageBox.Show("Invalid Reason Field"); comboreason.Focus(); return; }

                if (comboBox1.Text != "" && txtRemarks.Text != "" && txtintime.Text != "" && txtoutime.Text != "")
                {
                    if (Convert.ToInt32("0" + comboreason.SelectedValue) >= 1 && txtpermissionhrs.Text != "" && txtmanualTime.Text != "")
                    {
                        string native = "", rem = "", manual = "";
                        if (checknative.Checked == true) { native = "T"; } else { native = "F"; }

                        DateTime modified = Convert.ToDateTime(System.DateTime.Now.ToString("dd-MM-yyyy"));
                        DateTime CreatedOn = Convert.ToDateTime(System.DateTime.Now.ToString("dd-MM-yyyy") + " " + System.DateTime.Now.ToLongTimeString());
                        if (comboreason.Text == "HOSTEL")
                        {
                            rem = "HOSTEL" + txtRemarks.Text;
                        }
                        else
                        {
                            rem = "";
                        }
                        //if (combopassno.Text.Length > 7)
                        //{
                        //    string ins = "update  ASPTBLHOSTELGATEPASS set REASON='" + comboreason.SelectedValue + "' ,HOSTELNAME='" + combohosteltype.Text + "',Remarks='" + txtRemarks.Text + "',MANUALTIME='" + frmdate.Value.ToString("dd-MM-yyyy") + " " + txtoutime.Text + ":00',INTIME='" + todate.Value.ToString("dd-MM-yyyy") + " " + txtintime.Text + ":00',TIMEDIFF='" + differ1.ToString() + "' WHERE A.ASPTBLHOSTELGATEPASSID='" + combopassno.Text;
                        //    Utility.ExecuteNonQuery(ins);
                        //}


                        //if (combopassno.Text.Length <= 0 && txtempid.Text=="")
                        //{
                        //    manual = "MANUAL PASS";
                        //    string ins = "INSERT INTO ASPTBLHOSTELGATEPASS(COMPCODE,  IDCARDNO,  EMPNAME ,  DEPARTMENT,  HOSTELNAME, HOSTELBLOCK,HOSTELROOM,HOSTELBLOCK1,HOSTELROOM1,CONTACTNO, SYSTEMDATE,  SYSTEMTIME ,REASON,MANUALTIME,USERNAME,  MODIFIED,  CREATEDON,  IPADDRESS1,IPADDRESS,PERMISSIONHRS,MANUALPASS,Remarks,NATIVE,FINYEAR,MONTH,OUTTIME,INTIME,TIMEDIFF)VALUES(" + txtcompcode.Text + ",'" + txtidcardno.Text + "','" + txtempname.Text + "','" + txtdept.Text + "' ,'" + combohostel.Text + "' ,'" + txtidcardno.Text + "' ,'" + txtidcardno.Text + "' ,'" + combohostelblock.Text + "','" + combohostelroom.Text + "' ,'" + txtcontactno.Text + "','" + txtsysdate.Text + "','" + txtsystime.Text + "','" + comboreason.SelectedValue + "' ,'" + Convert.ToDateTime(txtmanualTime.Text) + "'," + Class.Users.USERID + ",to_date('" + Convert.ToDateTime(todate.Value).ToString().Substring(0, 10) + "', 'dd-MM-yyyy'),to_date('" + Convert.ToDateTime(CreatedOn.ToString()) + "', 'dd/MM/yyyy hh24:MI:SS'),'00','" + Class.Users.IPADDRESS + "', '" + Convert.ToDateTime(txtpermissionhrs.Text) + "','" + manual + "','" + txtRemarks.Text + "','" + native + "','" + System.DateTime.Now.Year + "','" + System.DateTime.Now.Date.ToString("MMMM") + "','" + frmdate.Value.ToString("dd-MM-yyyy") + " " + txtoutime.Text + ":00','" + todate.Value.ToString("dd-MM-yyyy") + " " + txtintime.Text + ":00','" + differ1.ToString() + "')";
                        //    Utility.ExecuteNonQuery(ins);

                        //    News();
                        //    Cursor = Cursors.Default;
                        //    MessageBox.Show("Record Saved Successfully", " Gate Pass ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //    combopassno.Focus();
                        //}
                        //else
                        //{
                        //    manual = combohosteltype.Text;
                        //if (combopassno.Text == "")
                        //{
                        //    combopassno.Text = txtempid.Text;
                        //}

                        string SEL1 = "SELECT A.* FROM ASPTBLHOSTELGATEPASS A  WHERE A.ASPTBLHOSTELGATEPASSID='" + combopassno.Text + "' and  a.idcardno='" + comboBox1.Text + "'";
                        DataSet ds1 = Utility.ExecuteSelectQuery(SEL1, "ASPTBLHOSTELGATEPASS");
                        DataTable dt1 = ds1.Tables["ASPTBLHOSTELGATEPASS"];
                        if (dt1.Rows.Count > 0)
                        {
                            DateTime statetime;
                            
                                statetime = Convert.ToDateTime(System.DateTime.Now.ToString());
                       
                                statetime = Convert.ToDateTime(dt1.Rows[0]["SYSTEMDATE"].ToString());
                          
                            TimeSpan differ = endtime.Subtract(statetime);
           
                            string ins2 = "INSERT INTO ASPTBLHOSTELGATEPASS1(ASPTBLHOSTELGATEPASSid,COMPCODE,  IDCARDNO,  EMPNAME ,  DEPARTMENT,  HOSTELNAME, HOSTELBLOCK,HOSTELROOM,HOSTELBLOCK1,HOSTELROOM1,CONTACTNO, SYSTEMDATE,  SYSTEMTIME ,REASON,MANUALTIME,USERNAME,  MODIFIED,  CREATEDON,  IPADDRESS1,IPADDRESS,PERMISSIONHRS,Remarks,NATIVE,FINYEAR,MONTH,MANUALPASS,INTIME,Remarks1,TIMEDIFF)VALUES(" + dt1.Rows[0]["ASPTBLHOSTELGATEPASSid"].ToString() + "," + dt1.Rows[0]["COMPCODE"].ToString() + ",'" + dt1.Rows[0]["IDCARDNO"].ToString() + "','" + dt1.Rows[0]["EMPNAME"].ToString() + "','" + dt1.Rows[0]["DEPARTMENT"].ToString() + "' ,'" + dt1.Rows[0]["HOSTELNAME"].ToString() + "' ,'" + dt1.Rows[0]["HOSTELBLOCK"].ToString() + "' ,'" + dt1.Rows[0]["HOSTELROOM"].ToString() + "' ,'" + dt1.Rows[0]["HOSTELBLOCK1"].ToString() + "','" + dt1.Rows[0]["HOSTELROOM1"].ToString() + "' ,'" + dt1.Rows[0]["CONTACTNO"].ToString() + "','" + System.DateTime.Now.ToString() + "','" + dt1.Rows[0]["SYSTEMTIME"].ToString() + "','" + comboreason.SelectedValue + "' ,'" + Convert.ToDateTime(txtmanualTime.Text) + "'," + Class.Users.USERID + ",to_date('" + Convert.ToDateTime(System.DateTime.Now.ToString()).ToString().Substring(0, 10) + "', 'dd-MM-yyyy'),to_date('" + Convert.ToDateTime(CreatedOn.ToString()) + "', 'dd/MM/yyyy hh24:MI:SS'),'" + dt1.Rows[0]["ipaddress1"].ToString() + "','" + Class.Users.IPADDRESS + "', '" + Convert.ToDateTime(dt1.Rows[0]["PERMISSIONHRS"].ToString()) + "','MANUAL PASS','" + native + "','" + System.DateTime.Now.Year + "','" + System.DateTime.Now.Date.ToString("MMMM") + "','" + txtRemarks.Text+ "','" + System.DateTime.Now.ToString() + "','" + dt1.Rows[0]["ipaddress"].ToString() + "','" + differ + "')";
                            Utility.ExecuteNonQuery(ins2);
                            string UP = "update  ASPTBLHOSTELGATEPASS set USERNAME='" + Class.Users.USERID + "', INTIME='" + System.DateTime.Now.ToString("dd-MM-yyyy").Substring(0, 10) + " " + txtintime.Text + "',TIMEDIFF='" + differ + "',REMARKS='" + txtRemarks.Text + " - MANUAL UPDATE" + "',MODIFIED=to_date('" + Convert.ToDateTime(todate.Value).ToString().Substring(0, 10) + "', 'dd-MM-yyyy'),IPADDRESS='" + Class.Users.IPADDRESS + "',MANUALPASS='" + txtRemarks.Text + "' WHERE ASPTBLHOSTELGATEPASSID='" + combopassno.Text + "'";
                            Utility.ExecuteNonQuery(UP);
                            News();
                            MessageBox.Show("Record Updated Successfully", " Gate Pass ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            combopassno.Focus();
                        }
                        //}

                    }
                    else
                    {
                        Cursor.Current = Cursors.Default;
                        MessageBox.Show("pls Select Mandatary Fields", " Gate Pass ", MessageBoxButtons.OK, MessageBoxIcon.Error); comboreason.Focus(); comboreason.BackColor = System.Drawing.Color.Red;
                        return;
                    }

                }
                else
                {
                    txtRemarks.Select(); Cursor.Current = Cursors.Default;
                    MessageBox.Show("Pls Enter Manual Pass No Fields", "Invalid");
                }
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show("Gate Pass Cancelled    " + ex.Message.ToString() + "", " Gate Pass ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return;
              
            }
        }

        public void Prints()
        {
            throw new NotImplementedException();
        }

        public void Searchs()
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

        public void Deletes()
        {
            throw new NotImplementedException();
        }

        public void ReadOnlys()
        {
            throw new NotImplementedException();
        }

        public void Imports()
        {
            throw new NotImplementedException();
        }

        public void Pdfs()
        {
            throw new NotImplementedException();
        }

        public void ChangePasswords()
        {
            throw new NotImplementedException();
        }

        public void DownLoads()
        {
            throw new NotImplementedException();
        }

        public void ChangeSkins()
        {
            throw new NotImplementedException();
        }

        public void Logins()
        {
            throw new NotImplementedException();
        }

        public void GlobalSearchs()
        {
            throw new NotImplementedException();
        }

        public void TreeButtons()
        {
            throw new NotImplementedException();
        }

        public void Exit()
        {

            GlobalVariables.MdiPanel.Show();
            this.Hide();
            News();
            GlobalVariables.HeaderName.Text = "";
            GlobalVariables.TabCtrl.TabPages.RemoveAt(GlobalVariables.TabCtrl.SelectedIndex);

        }

        public void GridLoad()
        {
            try
            {
                Class.Users.Intimation = "PAYROLL";
                listfilter.Items.Clear(); lvLogs.Items.Clear();
               int iGLCount = 1;
                DataTable dt = new DataTable();
                
                    string sel0 = "SELECT DISTINCT A.ASPTBLHOSTELGATEPASSID,D.MIDCARD AS IDCARDNO,C.FNAME AS EMPNAME,E.MNNAME1 AS DEPARTMENT, A.HOSTELNAME, '' HOSTELBLOCK,'' AS HOSTELROOM,   '' CONTACTNO,substr(A.OUTTIME,11,18) outtime,substr(A.INTIME,11,18) intime,substr(A.OUTTIME,0,10) outdate,a.modified indate   FROM ASPTBLHOSTELGATEPASS A  JOIN   GTCOMPMAST B ON B.GTCOMPMASTID = A.COMPCODE     JOIN   HREMPLOYMAST    C ON C.COMPCODE=B.GTCOMPMASTID AND C.COMPCODE=A.COMPCODE   JOIN HREMPLOYDETAILS D ON D.HREMPLOYMASTID=C.HREMPLOYMASTID AND D.IDCARD=C.IDCARDNO    AND  D.DEPTNAME=A.DEPARTMENT AND D.MIDCARD=A.IDCARDNO   JOIN GTDEPTDESGMAST  E ON E.GTDEPTDESGMASTID = A.DEPARTMENT       JOIN ASPTBLREASONMAS G ON G.ASPTBLREASONMASID=A.REASON      WHERE   A.MODIFIED= TO_DATE('" + dateTimePicker2.Value.ToString("dd-MM-yyyy") + "', 'dd-MM-yyyy') AND A.MANUALPASS='MANUAL PASS'  ORDER BY A.ASPTBLHOSTELGATEPASSID DESC";
                    DataSet ds1 = Utility.ExecuteSelectQuery(sel0, "ASPTBLHOSTELGATEPASS");
                    dt = ds1.Tables["ASPTBLHOSTELGATEPASS"]; lvLogs.Columns[5].Text = "CompCode";

              
                if (dt.Rows.Count > 0)
                {

                    foreach (DataRow myRow in dt.Rows)
                    {
                        ListViewItem list = new ListViewItem();

                        list.Text = iGLCount.ToString();
                        list.SubItems.Add(myRow["ASPTBLHOSTELGATEPASSID"].ToString());
                        list.SubItems.Add(myRow["IDCARDNO"].ToString());
                        list.SubItems.Add(myRow["EMPNAME"].ToString());
                        list.SubItems.Add(myRow["DEPARTMENT"].ToString());
                        if (Class.Users.HostelName == "GENTS STAFF HOSTEL" || Class.Users.HostelName == "WORKING GENTS HOSTEL" || Class.Users.HostelName == "WOMENS HOSTEL")
                        {
                            list.SubItems.Add(myRow["HOSTELNAME"].ToString());
                            list.SubItems.Add(myRow["HOSTELBLOCK"].ToString());
                            list.SubItems.Add(myRow["HOSTELROOM"].ToString());
                        }
                        else
                        {
                            list.SubItems.Add(myRow["HOSTELNAME"].ToString());
                            list.SubItems.Add("");
                            list.SubItems.Add("");
                        }
                        list.SubItems.Add(myRow["CONTACTNO"].ToString());
                        list.SubItems.Add(myRow["outtime"].ToString());
                        list.SubItems.Add(myRow["intime"].ToString());
                        list.SubItems.Add(myRow["outdate"].ToString());
                        list.SubItems.Add(myRow["indate"].ToString());
                        this.listfilter.Items.Add((ListViewItem)list.Clone());
                        if (iGLCount % 2 == 0)
                        {
                            list.BackColor = Color.White;
                        }
                        else
                        {
                            list.BackColor = Color.WhiteSmoke;
                        }
                        lvLogs.Items.Add(list);
                        iGLCount++;
                    }
                    lblattcount.Text = "Total Count    :" + lvLogs.Items.Count;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtidcardno_TextChanged(object sender, EventArgs e)
        {

        }
        string master = ""; DataTable dt = new DataTable();
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            hostelload(comboBox1.Text.ToString());
        }

        private void txtoutime_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsNumber(e.KeyChar) || e.KeyChar == ':' || e.KeyChar == (char)Keys.Back);
        }

        private void txtoutime_TextChanged(object sender, EventArgs e)
        {
            
            
                //if (txtoutime.Text.Length >= 5)
                //{
                //    string[] ss = txtoutime.Text.Split(':');
                //    if (ss.Length > 1)
                //    {
                //        int s1 = Convert.ToInt32("0" + ss[0].ToString());
                //        int s2 = Convert.ToInt32("0" + ss[1].ToString());

                //        if (s1 >= 24 || s2 >= 59) { MessageBox.Show("Invalid  ." + txtoutime.Text); }
                //    }
                //    else
                //    {
                //        MessageBox.Show("Invalid  ." + txtoutime.Text);
                //    txtoutime.Text = ""; return;
                //}
                //}
            
            
        }

        private void txtintime_TextChanged(object sender, EventArgs e)
        {
            
            
                //if (txtintime.Text.Length >= 5)
                //{
                //    string[] ss = txtintime.Text.Split(':');
                //    if (ss.Length > 1)
                //    {
                //        int s1 = Convert.ToInt32("0" + ss[0].ToString());
                //        int s2 = Convert.ToInt32("0" + ss[1].ToString());

                //        if (s1 >= 24 || s2 >= 59) { MessageBox.Show("Invalid  ." + txtintime.Text); }
                //    }
                //    else
                //    {
                //        MessageBox.Show("Invalid  ." + txtintime.Text);
                //    txtintime.Text = "";return;
                //    }
                //}
            
        }

        private void txtintime_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsNumber(e.KeyChar) || e.KeyChar == ':' || e.KeyChar == (char)Keys.Back);

        }
        ListView listfilter = new ListView();
        private void butview_Click(object sender, EventArgs e)
        {
            try
            {
                listfilter.Items.Clear(); lvLogs.Items.Clear();
               int iGLCount = 1;
                DataTable dt = new DataTable();

              
                    string sel0 = "SELECT DISTINCT A.ASPTBLHOSTELGATEPASSID,D.MIDCARD AS IDCARDNO,C.FNAME AS EMPNAME,E.MNNAME1 AS DEPARTMENT, A.HOSTELNAME, '' HOSTELBLOCK,'' AS HOSTELROOM,   '' CONTACTNO,substr(A.OUTTIME,11,18) outtime,substr(A.INTIME,11,18) intime ,substr(A.OUTTIME,0,10) outdate,A.MODIFIED AS indate  FROM ASPTBLHOSTELGATEPASS A  JOIN   GTCOMPMAST B ON B.GTCOMPMASTID = A.COMPCODE     JOIN   HREMPLOYMAST    C ON C.COMPCODE=B.GTCOMPMASTID AND C.COMPCODE=A.COMPCODE   JOIN HREMPLOYDETAILS D ON D.HREMPLOYMASTID=C.HREMPLOYMASTID AND D.IDCARD=C.IDCARDNO    AND  D.DEPTNAME=A.DEPARTMENT AND D.MIDCARD=A.IDCARDNO   JOIN GTDEPTDESGMAST  E ON E.GTDEPTDESGMASTID = A.DEPARTMENT       JOIN ASPTBLREASONMAS G ON G.ASPTBLREASONMASID=A.REASON      WHERE  A.MODIFIED= TO_DATE('" + dateTimePicker2.Value.ToString("dd-MM-yyyy") + "', 'dd-MM-yyyy') AND  A.INTIME IS NULL  ORDER BY A.ASPTBLHOSTELGATEPASSID DESC";
                    DataSet ds1 = Utility.ExecuteSelectQuery(sel0, "ASPTBLHOSTELGATEPASS");
                    dt = ds1.Tables["ASPTBLHOSTELGATEPASS"];
               
                if (dt.Rows.Count > 0)
                {

                    foreach (DataRow myRow in dt.Rows)
                    {
                        ListViewItem list = new ListViewItem();

                        list.Text = iGLCount.ToString();
                        list.SubItems.Add(myRow["ASPTBLHOSTELGATEPASSID"].ToString());
                        list.SubItems.Add(myRow["IDCARDNO"].ToString());
                        list.SubItems.Add(myRow["EMPNAME"].ToString());
                        list.SubItems.Add(myRow["DEPARTMENT"].ToString());
                        if (Class.Users.HostelName == "GENTS STAFF HOSTEL" || Class.Users.HostelName == "WORKING GENTS HOSTEL" || Class.Users.HostelName == "WOMENS HOSTEL")
                        {
                            list.SubItems.Add(myRow["HOSTELNAME"].ToString());
                            list.SubItems.Add(myRow["HOSTELBLOCK"].ToString());
                            list.SubItems.Add(myRow["HOSTELROOM"].ToString());
                        }
                        else
                        {
                            list.SubItems.Add(myRow["HOSTELNAME"].ToString());
                            list.SubItems.Add("");
                            list.SubItems.Add("");
                        }
                        list.SubItems.Add(myRow["CONTACTNO"].ToString());
                        list.SubItems.Add(myRow["outtime"].ToString());
                        list.SubItems.Add(myRow["intime"].ToString());
                        list.SubItems.Add(myRow["outdate"].ToString());
                        list.SubItems.Add(myRow["indate"].ToString());
                        this.listfilter.Items.Add((ListViewItem)list.Clone());
                        if (iGLCount % 2 == 0)
                        {
                            list.BackColor = Color.White;
                        }
                        else
                        {
                            list.BackColor = Color.WhiteSmoke;
                        }
                        lvLogs.Items.Add(list);
                        iGLCount++;
                    }
                    lblattcount.Text = "Total Count    :" + lvLogs.Items.Count;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void lvLogs_ItemActivate(object sender, EventArgs e)
        {
            try
            {
             
                if (lvLogs.Items.Count > 0)
                {

                  
                    txtempid.Text = Convert.ToString(lvLogs.SelectedItems[0].SubItems[1].Text);
                    DataTable dt = new DataTable();
                    dt.Rows.Clear();
                    if (Class.Users.HostelName == "WORKING GENTS HOSTEL" || Class.Users.HostelName == "WOMENS HOSTEL" || Class.Users.HostelName == "GENTS STAFF HOSTEL")
                    {
                        string sel0 = "SELECT A.ASPTBLHOSTELGATEPASSID,B.COMPCODE, D.MIDCARD AS IDCARDNO,C.FNAME AS EMPNAME,E.MNNAME1 AS DEPARTMENT,F.HOSTELNAME,F.BLOCKFLOOR AS HOSTELBLOCK, F.ROOMNO AS HOSTELROOM,   B.PHONENO AS  CONTACTNO, A.MANUALTIME,G.REASON,A.PERMISSIONHRS,A.SYSTEMDATE,A.SYSTEMTIME,substr(A.OUTTIME,12,5) outtime,substr(A.INTIME,12,5) INTIME,I.QRCODE, '' EMPIMAGE,A.REMARKS, A.IPADDRESS, A.IPADDRESS1,    A.NATIVE,a.modified,SUBSTR(A.OUTTIME,0,10) AS  MANUALTIME1  FROM ASPTBLHOSTELGATEPASS A  JOIN   GTCOMPMAST B ON B.GTCOMPMASTID = A.COMPCODE    JOIN   HREMPLOYMAST    C ON C.COMPCODE=B.GTCOMPMASTID AND C.COMPCODE=A.COMPCODE AND C.HREMPLOYMASTID=A.EMPNAME     JOIN HREMPLOYDETAILS D ON D.HREMPLOYMASTID=C.HREMPLOYMASTID AND D.IDCARD=C.IDCARDNO    AND  D.DEPTNAME=A.DEPARTMENT AND D.MIDCARD=A.IDCARDNO   JOIN GTDEPTDESGMAST  E ON E.GTDEPTDESGMASTID = A.DEPARTMENT  JOIN HOSTELLIVEDATA F ON F.COMPCODE=B.COMPCODE    AND F.IDCARDNO=C.IDCARDNO AND F.IDCARDNO=D.IDCARD   AND D.IDCARD=F.IDCARDNO   JOIN ASPTBLREASONMAS G ON G.ASPTBLREASONMASID=A.REASON      LEFT OUTER JOIN ASPTBLEMP I ON I.COMPCODE=B.GTCOMPMASTID AND I.IDCARDNO=A.IDCARDNO   WHERE     A.ASPTBLHOSTELGATEPASSID=" + txtempid.Text + "  ORDER BY A.ASPTBLHOSTELGATEPASSID desc";
                        DataSet ds0 = Utility.ExecuteSelectQuery(sel0, "ASPTBLHOSTELGATEPASS");
                        dt = ds0.Tables["ASPTBLHOSTELGATEPASS"];
                    }
                    else
                    {
                        string sel1 = "SELECT  A.ASPTBLHOSTELGATEPASSID,B.COMPCODE,D.MIDCARD AS IDCARDNO,C.FNAME AS EMPNAME,E.DISPNAME AS DEPARTMENT,B.PHONENO AS  CONTACTNO,B.COMPCODE AS HOSTELNAME,'' AS HOSTELBLOCK, '' AS HOSTELROOM,A.MANUALTIME,G.REASON,A.PERMISSIONHRS,A.SYSTEMDATE,A.SYSTEMTIME,substr(A.OUTTIME,12,5) outtime,substr(A.INTIME,12,5) INTIME,I.QRCODE,'' EMPIMAGE,A.REMARKS, A.IPADDRESS, A.IPADDRESS1,    A.NATIVE,a.modified,SUBSTR(A.OUTTIME,0,10) AS  MANUALTIME1 FROM ASPTBLHOSTELGATEPASS A  JOIN   GTCOMPMAST B ON B.GTCOMPMASTID = A.COMPCODE           JOIN   HREMPLOYMAST    C ON C.COMPCODE=B.GTCOMPMASTID AND C.COMPCODE=A.COMPCODE     JOIN HREMPLOYDETAILS D ON D.HREMPLOYMASTID=C.HREMPLOYMASTID         AND D.IDCARD=C.IDCARDNO AND  D.DEPTNAME=A.DEPARTMENT AND D.MIDCARD=A.IDCARDNO       JOIN GTDEPTDESGMAST  E ON E.GTDEPTDESGMASTID = A.DEPARTMENT         JOIN ASPTBLREASONMAS G ON G.ASPTBLREASONMASID=A.REASON       LEFT OUTER JOIN ASPTBLEMP I ON I.COMPCODE=B.GTCOMPMASTID AND I.IDCARDNO=A.IDCARDNO WHERE A.ASPTBLHOSTELGATEPASSID=" + txtempid.Text;
                        DataSet ds = Utility.ExecuteSelectQuery(sel1, "ASPTBLHOSTELGATEPASS");
                        dt = ds.Tables["ASPTBLHOSTELGATEPASS"];
                    }
                    frmdate.Value = Convert.ToDateTime(dt.Rows[0]["MANUALTIME1"].ToString());
                    todate.Value = Convert.ToDateTime(dt.Rows[0]["modified"].ToString());
                    comboBox1.Text = Convert.ToString(dt.Rows[0]["IDCARDNO"].ToString());
                    txtempid.Text = Convert.ToString(dt.Rows[0]["ASPTBLHOSTELGATEPASSID"].ToString());
                    combopassno.Text = Convert.ToString(dt.Rows[0]["ASPTBLHOSTELGATEPASSID"].ToString());
                    combo_compcode.Text = Convert.ToString(dt.Rows[0]["COMPCODE"].ToString());
                    txtidcardno.Text = Convert.ToString(dt.Rows[0]["IDCARDNO"].ToString());
                    comboempname.Text = Convert.ToString(dt.Rows[0]["EMPNAME"].ToString());
                    combo_dept.Text = Convert.ToString(dt.Rows[0]["DEPARTMENT"].ToString());
                    combohosteltype.Text = Convert.ToString(dt.Rows[0]["HOSTELNAME"].ToString());
                    combohostelblock.Text = Convert.ToString(dt.Rows[0]["HOSTELBLOCK"].ToString());
                    combohostelroom.Text = Convert.ToString(dt.Rows[0]["HOSTELROOM"].ToString());
                    txtmanualTime.Text = Convert.ToDateTime(dt.Rows[0]["MANUALTIME"].ToString()).ToString("HH:mm:ss");
                    comboreason.Text = Convert.ToString(dt.Rows[0]["REASON"].ToString());
                    txtpermissionhrs.Text = Convert.ToDateTime(dt.Rows[0]["PERMISSIONHRS"].ToString()).ToString("HH:mm:ss");
                    txtsysdate.Text = Convert.ToString(dt.Rows[0]["SYSTEMDATE"].ToString());
                    txtsystime.Text = Convert.ToString(dt.Rows[0]["SYSTEMTIME"].ToString());
                    txtoutime.Text = Convert.ToString(dt.Rows[0]["OUTTIME"].ToString().Trim());
                    txtintime.Text = Convert.ToString(dt.Rows[0]["INTIME"].ToString().Trim());
                    txtRemarks.Text = Convert.ToString(dt.Rows[0]["REMARKS"].ToString());
                    txtcontactno.Text = Convert.ToString(dt.Rows[0]["CONTACTNO"].ToString());
                   
                    if (dt.Rows[0]["NATIVE"].ToString() == "T")
                    {
                        checknative.Checked = true;
                    }
                    else
                    {
                        checknative.Checked = false;
                    }
                 
                    if(Class.Users.HUserName=="VAIRAM" || Class.Users.IPADDRESS=="192.168.101.15")
                    {
                       txtoutime.Enabled = true;txtintime.Enabled = true;
                        comboreason.Enabled = true;
                    }
                    else
                    {
                        txtoutime.Enabled = false; txtintime.Enabled = false;
                        comboreason.Enabled = false;
                    }
                   
                    txtmanualTime.Enabled = false;
                    txtpermissionhrs.Enabled = false;

                    // txtintime.Enabled = false;txtoutime.Enabled = false;

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void comboreason_SelectedIndexChanged(object sender, EventArgs e)
        {
            Class.Users.UserTime = 0;
            if (comboreason.Text == "NATIVE")
            {
                checknative.Checked = true;
            }
            else
            {
                checknative.Checked = false;
            }
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Class.Users.Intimation = "PAYROLL";
        }

        private void txthostelgatesearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int item0 = 0;
                if (txthostelgatesearch.Text.Length > 1)
                {
                    lvLogs.Items.Clear();
                    foreach (ListViewItem item in listfilter.Items)
                    {
                        ListViewItem list = new ListViewItem();
                        if (listfilter.Items[item0].SubItems[2].ToString().Contains(txthostelgatesearch.Text) || listfilter.Items[item0].SubItems[3].ToString().Contains(txthostelgatesearch.Text) || listfilter.Items[item0].SubItems[4].ToString().Contains(txthostelgatesearch.Text) || listfilter.Items[item0].SubItems[5].ToString().Contains(txthostelgatesearch.Text))
                        {


                            list.Text = listfilter.Items[item0].SubItems[0].Text;
                            list.SubItems.Add(listfilter.Items[item0].SubItems[1].Text);
                            list.SubItems.Add(listfilter.Items[item0].SubItems[2].Text);
                            list.SubItems.Add(listfilter.Items[item0].SubItems[3].Text);
                            list.SubItems.Add(listfilter.Items[item0].SubItems[4].Text);
                            list.SubItems.Add(listfilter.Items[item0].SubItems[5].Text);
                            list.SubItems.Add(listfilter.Items[item0].SubItems[6].Text);
                            list.SubItems.Add(listfilter.Items[item0].SubItems[7].Text);
                            list.SubItems.Add(listfilter.Items[item0].SubItems[8].Text);
                            list.SubItems.Add(listfilter.Items[item0].SubItems[9].Text);
                            list.SubItems.Add(listfilter.Items[item0].SubItems[10].Text);
                            if (item0 % 2 == 0)
                            {
                                list.BackColor = Color.White;
                            }
                            else
                            {
                                list.BackColor = Color.WhiteSmoke;
                            }
                            lvLogs.Items.Add(list);


                        }
                        item0++;
                    }

                }
                else
                {
                    ListView ll = new ListView(); item0 = 1;
                    lvLogs.Items.Clear();
                    foreach (ListViewItem item in listfilter.Items)
                    {

                        if (item0 % 2 == 0)
                        {
                            item.BackColor = Color.White;
                        }
                        else
                        {
                            item.BackColor = Color.WhiteSmoke;
                        }
                        this.lvLogs.Items.Add((ListViewItem)item.Clone());
                        item0++;
                    }
                    lblattcount.Text = "Total Count: " + lvLogs.Items.Count;
                }


            }
            catch (Exception ex)
            {

            }
        }

        private void combohosteltype_SelectedIndexChanged(object sender, EventArgs e)
        {
            Class.Users.Intimation = "PAYROLL";
            if (comboBox1.Text != "" && combohosteltype.Text != "")
            {
                dt.Rows.Clear();
                if (combohosteltype.Text != "AGFMGII")
                {
                    string sel1 = "SELECT '" + System.DateTime.Now.Year + "' AS FINYEAR,C.HREMPLOYMASTID AS ASPTBLEMPID, B.COMPCODE ,B.GTCOMPMASTID,D.MIDCARD,C.FNAME,E.MNNAME1 as DISPNAME,E.GTDEPTDESGMASTID ,'' as HOSTELNAME,A.BLOCKFLOOR,A.ROOMNO,A.IDCARDNO ,b.phoneno || b.faxno as CONTACTNO FROM HOSTELLIVEDATA A JOIN GTCOMPMAST B ON B.COMPCODE= A.COMPCODE   JOIN HREMPLOYMAST C ON C.COMPCODE = B.GTCOMPMASTID AND C.IDCARDNO = A.IDCARDNO   JOIN HREMPLOYDETAILS D ON C.HREMPLOYMASTID = D.HREMPLOYMASTID AND D.HOSTEL='YES' AND D.IDACTIVE='YES'   JOIN GTDEPTDESGMAST E ON E.GTDEPTDESGMASTID = D.DEPTNAME   WHERE D.MIDCARD=" + comboBox1.Text.ToString();
                    DataSet ds = Utility.ExecuteSelectQuery(sel1, "HOSTELLIVEDATA");
                    dt = ds.Tables["HOSTELLIVEDATA"];
                    master = "Hostel Master";
                }
               if(dt.Rows.Count<=0 || dt==null)
                {

                    string sel2 = "SELECT '" + System.DateTime.Now.Year + "' AS FINYEAR,C.HREMPLOYMASTID AS ASPTBLEMPID, B.COMPCODE ,B.GTCOMPMASTID,D.MIDCARD,C.FNAME,E.MNNAME1 as DISPNAME,E.GTDEPTDESGMASTID,'AGFMGII' AS HOSTELNAME,'0' as BLOCKFLOOR,'0' AS ROOMNO,C.IDCARDNO,B.PHONENO || B.FAXNO AS CONTACTNO  FROM  GTCOMPMAST B JOIN  HREMPLOYMAST C ON C.COMPCODE = B.GTCOMPMASTID    JOIN HREMPLOYDETAILS D ON C.HREMPLOYMASTID = D.HREMPLOYMASTID   JOIN GTDEPTDESGMAST E ON E.GTDEPTDESGMASTID = D.DEPTNAME  AND D.IDACTIVE='YES'   WHERE D.MIDCARD='" + comboBox1.Text.ToString() + "'   ORDER BY  C.IDCARDNO DESC ";
                    DataSet ds2 = Utility.ExecuteSelectQuery(sel2, "HREMPLOYDETAILS");
                    dt = ds2.Tables["HREMPLOYDETAILS"];
                    master = "";
                    master = "Employee Master";
                }
                if (dt.Rows.Count > 0)
                {
                    txtidcardno.Text = Convert.ToString(comboBox1.Text);
                    txtcompcode.Text = Convert.ToString(dt.Rows[0]["GTCOMPMASTID"].ToString());
                    combo_compcode.Text = Convert.ToString(dt.Rows[0]["COMPCODE"].ToString());
                    comboBox1.Text = Convert.ToString(dt.Rows[0]["MIDCARD"].ToString());
                    comboempname.Text = Convert.ToString(dt.Rows[0]["FNAME"].ToString());
                    txtempname.Text = Convert.ToString(dt.Rows[0]["ASPTBLEMPID"].ToString());
                    combo_dept.Text = Convert.ToString(dt.Rows[0]["DISPNAME"].ToString());
                    txtdept.Text = Convert.ToString(dt.Rows[0]["GTDEPTDESGMASTID"].ToString());
                    txthostelroom.Text = Convert.ToString(dt.Rows[0]["ROOMNO"].ToString());
                    combohostel.Text = Convert.ToString(combohosteltype.Text);
                    combohostelroom.Text = Convert.ToString(dt.Rows[0]["ROOMNO"].ToString());
                    txthostelblock.Text = Convert.ToString(dt.Rows[0]["BLOCKFLOOR"].ToString());
                    combohostelblock.Text = Convert.ToString(dt.Rows[0]["BLOCKFLOOR"].ToString());
                    txtcontactno.Text = Convert.ToString(dt.Rows[0]["CONTACTNO"].ToString());
                    txtsysdate.Text = Convert.ToString(Convert.ToDateTime(System.DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss tt")));
                    txtsystime.Text = Convert.ToString(System.DateTime.Now.ToString("HH:mm:ss tt"));
                    txtmanualTime.Text = Convert.ToString(DateTime.Now.ToString("HH:mm:ss"));                   
                    //todate.Value= Convert.ToDateTime(dt.Rows[0]["MODIFIED"].ToString().Substring(0,10));
                }
                else
                {
                    News();
                }
                comboreason.Focus();
            }
        }

        private void todate_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void passNumberRefreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            passnocheck();
        }

        private void combopassno_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (combopassno.Text.Length>7)
            {
                passnocheck(combopassno.Text);
            }
        }

        private void contextMenuStrip2_Opening(object sender, CancelEventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void lbloutime_Click(object sender, EventArgs e)
        {

        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }
    }
}
