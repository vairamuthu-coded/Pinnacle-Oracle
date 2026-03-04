using Pinnacle.Models.Canteen;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Windows.Forms;
using zkemkeeper;

namespace Pinnacle.Canteen
{
    public partial class CanteenItemMaster : Form,ToolStripAccess
    {
        private static CanteenItemMaster _instance;
        Models.Master mas = new Models.Master();
        Models.UserRights sm = new Models.UserRights();
        Models.MenuName c = new Models.MenuName();
        CanteenItemMasterModel CC = new CanteenItemMasterModel();
        public static CanteenItemMaster Instance
        {
            get { if (_instance == null) _instance = new CanteenItemMaster(); GlobalVariables.CurrentForm = _instance; return _instance; }

        }


        public CanteenItemMaster()
        {
            InitializeComponent();
            usercheck(Class.Users.HCompcode, Class.Users.HUserName, Class.Users.ScreenName);
            Class.Users.IPADDRESS = GenFun.GetLocalIPAddress();
       
            lblMarquee1.Visible = false;
            int DAYS = CallDays();
            timer1.Enabled = true;
          
            lblMarquee1.BackColor = Color.White;
            lblMarquee1.ForeColor = Class.Users.BackColors;
            tabControl1.TabPages[2].Enabled = false; 
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
                        if (dt1.Rows[r]["SAVES"].ToString() == "T") { GlobalVariables.Saves.Visible = true;  } else { GlobalVariables.Saves.Visible = false; }
                        if (dt1.Rows[r]["PRINTS"].ToString() == "T") { GlobalVariables.Prints.Visible = true; } else { GlobalVariables.Prints.Visible = false; }
                        if (dt1.Rows[r]["READONLY"].ToString() == "T") { GlobalVariables.ReadOnlys.Visible = false; } else { GlobalVariables.ReadOnlys.Visible = false; }
                        if (dt1.Rows[r]["SEARCH"].ToString() == "T") { GlobalVariables.Searchs.Visible = true; } else { GlobalVariables.Searchs.Visible = false; }
                        if (dt1.Rows[r]["DELETES"].ToString() == "T") { GlobalVariables.Deletes.Visible = true; } else { GlobalVariables.Deletes.Visible = false; }
                        if (dt1.Rows[r]["TREEBUTTON"].ToString() == "T") { GlobalVariables.TreeButtons.Visible = true; } else { GlobalVariables.TreeButtons.Visible = false; }
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

                GlobalVariables.Toolstrip1.Enabled = false;
            }

        }
         zkemkeeper.CZKEM axCZKEM1 = new zkemkeeper.CZKEM();
        private bool bIsConnected = false;//the boolean value identifies whether the device is connected
        private int iMachineNumber;//the serial number of the device.After connecting the device ,this value will be changed.      
        private static Int32 MyCount;
        //  private static Int32 ToIPCount;
        private bool bIsConnectedToIP = false;
        bool bAddControl = true;
        //  private static Int32 MyCountFinger;
        //  private static Int32 MyCountFace;
        string sdwEnrollNumber = "";
        string sName = "";
        string sPassword = "";
        int iPrivilege = 0;
        bool bEnabled = false;
        int idwFingerIndex = 0;
        string sTmpData = "";
        string sTmpData1 = "";
        int iTmpLength = 0;
        int iFlag = 0;
        string sEnabled = "";
        //  int idwTMachineNumber = 0;      
        int idwVerifyMode = 0;
        int idwInOutMode = 0;
        int idwYear = 0;
        int idwMonth = 0;
        int idwDay = 0;
        int idwHour = 0;
        int idwMinute = 0;
        int idwSecond = 0;
        int idwWorkcode = 0;
        int idwErrorCode = 0;
        int iGLCount = 0;
        int iIndex = 0;
        // int iUpdateFlag = 0;
        string suserid = "";
        int iFaceIndex = 50;//the only possible parameter value     
        int iLength = 0;
        string sLastEnrollNumber = "";//the former enrollnumber you have upload(define original value as 0)
        string sCardnumber = "";
        string MacIP = "";
        int i = 0;
        private void CanteenItemMaster_Load(object sender, EventArgs e)
        {
    
        }

        void comboitemload()
        {

            pop();

        }
        private void comboitem_SelectedIndexChanged(object sender, EventArgs e)
        {
          
               // pop();
           
        }
        public static decimal add(decimal num1, decimal num2)
        {
            decimal total;
            total = num1 + num2;
            return total;
        }

        private void pop()
        {
            TimeSpan totaltime; int h1, m1, se1, h2, m2, se2 = 0;
          
            var currentDateTime = DateTime.Now; DateTime endtime = new DateTime();
            var currentTimeAlone = new TimeSpan(currentDateTime.Hour, currentDateTime.Minute, currentDateTime.Second);
            try
            {

               
                Class.Users.LoginTime = CallTime();
                Class.Users.UserTime = 0;
                flowLayoutPanel1.Controls.Clear(); 
                string sel = "SELECT A.ASPTBLMENPERDETID,   B.DOCDATE,E.CATEGORY,C.ITEMNAME1 ,C.EMPLOYEECOST,C.ITEMCOST, C.SPECIALCOST,c.ITEMIMAGE,A.FROMTIME FROM ASPTBLMENPERDET  A JOIN ASPTBLMENPER B ON A.ASPTBLMENPERID=B.ASPTBLMENPERID  JOIN ASPTBLCANITEMMAS C ON C.ASPTBLCANITEMMASID=A.ITEMNAME1 JOIN ASPTBLUSERMAS D ON D.COMPCODE=B.COMPCODE AND D.USERID=B.USERNAME  JOIN ASPTBLCANCATEGORYMAS E ON E.ASPTBLCANCATEGORYMASID = A.CATEGORY WHERE A.ACTIVE='T'  AND C.ACTIVE='T'  AND D.COMPCODE='" + Class.Users.COMPCODE + "'  AND D.USERID='" + Class.Users.USERID + "'  AND B.DOCDATE BETWEEN TO_DATE(SYSDATE) AND TO_DATE(SYSDATE+(SELECT DAYS FROM CANITEMDISPLAYDAYS))    AND TO_TIMESTAMP(TO_CHAR(SYSDATE,'DD/MM/YY HH24:MI:SS'),'DD/MM/YY HH24:MI:SS')  BETWEEN TO_TIMESTAMP(A.FROMDATE||' '||A.FROMTIME,'DD/MM/YY HH24:MI:SS') AND TO_TIMESTAMP(A.TODATE||' '||A.TOTIME,'DD/MM/YY HH24:MI:SS')   ORDER BY 2,1";
                DataSet ds = Utility.ExecuteSelectQuery(sel, "ASPTBLCANITEMMAS");
                DataTable dt = ds.Tables["ASPTBLCANITEMMAS"];
                if (dt != null)
                {

           
                    UserControls.CanteenCustom[] items = new UserControls.CanteenCustom[dt.Rows.Count];
                    foreach (DataRow myRow in dt.Rows)
                    {
                        decimal costcalc = 0;
                        costcalc = add(Convert.ToDecimal("0" + myRow["EMPLOYEECOST"].ToString()), 0);
                        items[i] = new UserControls.CanteenCustom();

                        if (myRow["ITEMIMAGE"].ToString() != "")
                        {
                            byte[] bytes = (byte[])myRow["ITEMIMAGE"];
                            Image img = Models.Device.ByteArrayToImage(bytes);
                            items[i].userimage = img;
                        }

                        items[i].menuname.Text = Convert.ToString(myRow["ITEMNAME1"].ToString() + "\r\n" + myRow["DOCDATE"].ToString().Substring(0, 10) + "\r\n" + myRow["FROMTIME"].ToString());
                        items[i].menuname.BackColor = Class.Users.BackColors;
                        items[i].menuname.ForeColor = Class.Users.Color2;
                        items[i].iconbackground.BackColor = Class.Users.BackColors;
                        items[i].Panelcolor.BackColor = Class.Users.Color1;
                        items[i].LabelItems.ForeColor = Class.Users.BackColors;
                        items[i].Butdate.ForeColor = Class.Users.BackColors;
                        items[i].LabelItems.Text = Convert.ToString(myRow["CATEGORY"].ToString());
                        items[i].Butdate.Text = Convert.ToString(myRow["DOCDATE"].ToString().Substring(0, 10));
                        items[i].ActualCost.BackColor = Class.Users.BackColors;
                        
                       
                        if (System.DateTime.Now.ToString("dd-MM-yyyy") == Convert.ToString(myRow["DOCDATE"].ToString().Substring(0, 10))) { 
                            items[i].ActualCost.Text ="AMOUNT : "+ Convert.ToString(myRow["SPECIALCOST"].ToString());
                        }
                        else
                        {
                            items[i].ActualCost.Text = "AMOUNT : "+Convert.ToString(myRow["ITEMCOST"].ToString());
                        }
                        flowLayoutPanel1.Controls.Add(items[i]);
                        items[i].menuname.Click += Menuname_Click;


                    }

                }
                else
                {
                    flowLayoutPanel1.Controls.Clear(); Class.Users.LoginTime = 0;
                }


            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally
            {

                string sel2 = " SELECT A.ASPTBLMENPERDETID FROM ASPTBLMENPERDET  A JOIN ASPTBLMENPER B ON A.ASPTBLMENPERID=B.ASPTBLMENPERID  JOIN ASPTBLCANITEMMAS C ON C.ASPTBLCANITEMMASID=A.ITEMNAME1  JOIN ASPTBLCANCATEGORYMAS D ON D.ASPTBLCANCATEGORYMASID=A.CATEGORY JOIN ASPTBLUSERMAS  E ON E.COMPCODE=B.COMPCODE AND E.USERID=B.USERNAME  WHERE A.ACTIVE='T'  AND C.ACTIVE='T'  AND E.COMPCODE='" + Class.Users.COMPCODE + "'  AND E.USERID='" + Class.Users.USERID + "'  AND  A.TODATE=TO_DATE('" + System.DateTime.Now.ToString("dd-MM-yyyy").Substring(0, 10) + "','dd-MM-yyyy') AND A.totime <='" + currentTimeAlone + "'    ORDER BY 1";//AND A.TOTIME >='" + currentTimeAlone + "'
                DataSet ds2 = Utility.ExecuteSelectQuery(sel2, "ASPTBLCANITEMMAS");
                DataTable dt2 = ds2.Tables["ASPTBLCANITEMMAS"];
                if (dt2.Rows.Count > 0)
                {
                    foreach (DataRow myRow in dt2.Rows)
                    {
                        string up = "update ASPTBLMENPERDET A set A.active='F'  WHERE A.ASPTBLMENPERDETID='" + myRow["ASPTBLMENPERDETID"].ToString() + "'";
                        Utility.ExecuteNonQuery(up);
                        Class.Users.LoginTime = 0;
                    }

                }
            }

        }


        private void TxtQuantity_ValueChanged(object sender, EventArgs e)
        {
            

        }
        private void TxtDays_ValueChanged(object sender, EventArgs e)
        {
           
        }

        string[] data0;
        string[] data1;
        string[] data2;
        private void Menuname_Click(object sender, EventArgs e)
        {
            try
            {
                lblMarquee1.Visible = false;
                Timercanteen_Tick(sender, e);
                if (lvLogs.Items.Count > 0)
                {
                    Class.Users.TOKENEMPID = 0; pictureemp.Image = null;
                    Class.Users.TOKENEMPID = Convert.ToInt64(lvLogs.Items[lvLogs.Items.Count - 1].SubItems[1].Text);

                    string sel = "SELECT  A.HREMPLOYMASTID,A.FNAME,B.MIDCARD FROM HREMPLOYMAST A JOIN HREMPLOYDETAILS B ON A.HREMPLOYMASTID=B.HREMPLOYMASTID  JOIN GTCOMPMAST C ON C.GTCOMPMASTID=A.COMPCODE  WHERE B.MIDCARD='" + Class.Users.TOKENEMPID + "'  AND B.IDACTIVE='YES'";//AND C.GTCOMPMASTID='" + Class.Users.COMPCODE + "'
                    DataSet ds = Utility.ExecuteSelectQuery(sel, "HREMPLOYMAST");
                    DataTable dt = ds.Tables["HREMPLOYMAST"];
                    if (dt.Rows.Count > 0)
                    {
                        lblempname.Text = "EMP NAME   :  " + dt.Rows[0]["FNAME"].ToString().ToUpper();
                        lblIdcardno.Text = "IDCARDNO    :  " + dt.Rows[0]["MIDCARD"].ToString();
                        Class.Users.IDCARDNO = Convert.ToInt64(Class.Users.TOKENEMPID);
                        timercanteen.Enabled = false;
                        Class.Users.CANTEENMENUNAME = "";
                        string s = sender.ToString();
                        string[] data = s.Split(',');
                    
                        Class.Users.CANTEENMENUNAME = data[1].Substring(7);
                        string ss = Class.Users.CANTEENMENUNAME.ToString();
                         data0 = ss.Split('\r');
                         data1 = data0[1].Split('\n');
                         data2 = data0[2].Split('\n');
                        Class.Users.DateTimes = Convert.ToDateTime(data1[1].ToString());
                        Class.Users.CANTEENMENUNAME = data0[0].ToString();
                        dateTimePicker1.Value = Convert.ToDateTime(data1[1].ToString() + " " + data2[1].ToString());
                    

                        if (Convert.ToInt64(Class.Users.TOKENEMPID) > 0 && Class.Users.CANTEENMENUNAME != "")
                        {
                            string sel1 = "SELECT  A.HREMPLOYMASTID,A.FNAME,B.MIDCARD,c.compcode FROM HREMPLOYMAST A JOIN HREMPLOYDETAILS B ON A.HREMPLOYMASTID=B.HREMPLOYMASTID join gtcompmast c on c.gtcompmastid=a.compcode  WHERE B.MIDCARD='" + Class.Users.TOKENEMPID + "'  AND B.IDACTIVE='YES'";//AND c.gtcompmastid='" + Class.Users.COMPCODE + "'
                            DataSet ds1 = Utility.ExecuteSelectQuery(sel1, "HREMPLOYMAST");
                            DataTable dt1 = ds1.Tables["HREMPLOYMAST"];
                            txtempid.Text = dt1.Rows[0]["HREMPLOYMASTID"].ToString();
                            comboempname.DisplayMember = "FNAME";
                            comboempname.ValueMember = "HREMPLOYMASTID";
                            comboidcardno.DisplayMember = "MIDCARD";
                            comboidcardno.ValueMember = "MIDCARD";
                            comboempname.DataSource = dt1;
                            comboidcardno.DataSource = dt1;
                            comboemptype.SelectedIndex = 0;
                            lblcompcode2.Text = dt1.Rows[0]["compcode"].ToString();
                            lblidcard2.Text =  dt1.Rows[0]["MIDCARD"].ToString() ;
                            lblempname2.Text = dt1.Rows[0]["FNAME"].ToString();
                            //lblempid2.Text = dt1.Rows[0]["MIDCARD"].ToString();
                            lbldate2.Text = Convert.ToDateTime(dateTimePicker1.Value.ToString()).ToString("dd-MM-yyyy");
                            lblqty2.Text = txtQuantity.Value.ToString();
                            lblnoofdays2.Text = txtDays.Value.ToString(); ;
                            lblcompcode.Text = Class.Users.HCompName;
                            lbldatetime.Text = System.DateTime.Now.ToString();
                            string sel2 = "SELECT C.ASPTBLCANITEMMASID,C.ITEMCODE, C.ITEMNAME1,C.EMPLOYEECOST , C.ITEMCOST,C.SPECIALCOST ,MAX(A.DOCDATE) AS DOCDATE,e.category FROM ASPTBLMENPER A JOIN ASPTBLMENPERDET B ON A.ASPTBLMENPERID=B.ASPTBLMENPERID     JOIN ASPTBLCANITEMMAS C ON C.ASPTBLCANITEMMASID=B.ITEMNAME1 AND C.ITEMCODE=B.ITEMCODE       JOIN ASPTBLUSERMAS D ON  D.USERID=A.USERNAME  AND D.COMPCODE=A.COMPCODE join asptblcancategorymas e on e.asptblcancategorymasid=b.category  WHERE  A.USERNAME='" + Class.Users.USERID + "' AND C.ITEMNAME1='" + Class.Users.CANTEENMENUNAME + "'  AND b.docdate=to_date('" + data1[1].ToString() + "','dd-MM-yyyy') AND b.fromtime='" + data2[1].ToString() + "'  AND B.ACTIVE='T'        GROUP BY C.ASPTBLCANITEMMASID,C.ITEMCODE, C.ITEMNAME1,C.EMPLOYEECOST ,C.ITEMCOST,C.SPECIALCOST,e.category";//A.COMPCODE='" + Class.Users.COMPCODE + "'  AND
                            DataSet ds2 = Utility.ExecuteSelectQuery(sel2, "ASPTBLCANITEMMAS");
                            DataTable dt2 = ds2.Tables["ASPTBLCANITEMMAS"];
                            comboitemcode.DisplayMember = "ITEMCODE";
                            comboitemcode.ValueMember = "ASPTBLCANITEMMASID";
                            comboitemname.DisplayMember = "ITEMNAME1";
                            comboitemname.ValueMember = "ASPTBLCANITEMMASID";
                           // txtitemcost.Text = "";
                            lblempid2.Text = dt2.Rows[0]["category"].ToString();
                            comboitemcode.DataSource = dt2;
                            comboitemname.DataSource = dt2;
                            lblitemname2.Text = dt2.Rows[0]["ITEMNAME1"].ToString();
                            lbltoken2.Text = dt2.Rows[0]["ASPTBLCANITEMMASID"].ToString();
                        
                            try
                            {

                                txtDays.Enabled = true;
                                if (Convert.ToInt64(Class.Users.TOKENEMPID) > 0)
                                {
                                    string sel3 = "SELECT  A.HREMPLOYMASTID,A.FNAME,B.MIDCARD FROM HREMPLOYMAST A JOIN HREMPLOYDETAILS B ON A.HREMPLOYMASTID=B.HREMPLOYMASTID   WHERE B.MIDCARD='" + Class.Users.TOKENEMPID + "' AND B.IDACTIVE='YES'";
                                    DataSet ds3 = Utility.ExecuteSelectQuery(sel3, "HREMPLOYMAST");
                                    DataTable dt3 = ds3.Tables["HREMPLOYMAST"];

                                    txtempid.Text = dt3.Rows[0]["HREMPLOYMASTID"].ToString();
                                    comboempname.DisplayMember = "FNAME";
                                    comboempname.ValueMember = "HREMPLOYMASTID";

                                    comboidcardno.DisplayMember = "MIDCARD";
                                    comboidcardno.ValueMember = "MIDCARD";
                                    comboempname.DataSource = dt3;
                                    comboidcardno.DataSource = dt3;

                                    string sel4 = "SELECT DISTINCT C.ASPTBLCANITEMMASID,C.ITEMCODE, C.ITEMNAME1,C.EMPLOYEECOST ,C.ITEMCOST,C.SPECIALCOST,to_date('" + System.DateTime.Now.ToString("dd-MM-yyyy") + "', 'dd-MM-yyyy') AS TODATE  ,e.category       FROM ASPTBLMENPER A JOIN ASPTBLMENPERDET B ON A.ASPTBLMENPERID = B.ASPTBLMENPERID            JOIN ASPTBLCANITEMMAS C ON C.ASPTBLCANITEMMASID = B.ITEMNAME1 AND C.ITEMCODE = B.ITEMCODE JOIN ASPTBLUSERMAS D ON  D.USERID = A.USERNAME  AND D.COMPCODE = A.COMPCODE   join asptblcancategorymas e on e.ASPTBLCANCATEGORYMASID=b.category    WHERE A.COMPCODE='" + Class.Users.COMPCODE + "'  AND A.USERNAME='" + Class.Users.USERID + "' AND C.ITEMNAME1='" + Class.Users.CANTEENMENUNAME + "'  AND e.category='" + dt2.Rows[0]["category"].ToString() + "' AND A.ACTIVE = 'T'";
                                    DataSet ds4 = Utility.ExecuteSelectQuery(sel4, "ASPTBLCANITEMMAS");
                                    DataTable dt4 = ds4.Tables["ASPTBLCANITEMMAS"];
                                    if (dt4.Rows.Count > 0)
                                    {
                                        comboitemcode.DisplayMember = "ITEMCODE";
                                        comboitemcode.ValueMember = "ASPTBLCANITEMMASID";
                                        comboitemname.DisplayMember = "ITEMNAME1";
                                        comboitemname.ValueMember = "ASPTBLCANITEMMASID";
                                        
                                        decimal V1 = Convert.ToDecimal("0" + dt4.Rows[0]["EMPLOYEECOST"].ToString());
                                        txtempcost.Text = V1.ToString();
                                        lblempid2.Text = dt4.Rows[0]["category"].ToString();
                                        decimal V3 = Convert.ToDecimal("0" + dt4.Rows[0]["SPECIALCOST"].ToString());
                                        txtspecialcost.Text = V3.ToString();
                                        comboitemcode.DataSource = dt4;
                                        comboitemname.DataSource = dt4; lblamount2.Text = "";
                                        if (dt4.Rows.Count > 0)
                                        {

                                            if (System.DateTime.Now.ToString("dd-MM-yyyy") == dateTimePicker1.Value.ToString("dd-MM-yyyy").Substring(0, 10))
                                            {
                                                decimal t1 = add(Convert.ToDecimal("0" + dt4.Rows[0]["EMPLOYEECOST"].ToString()), Convert.ToDecimal("0" + dt4.Rows[0]["SPECIALCOST"].ToString()));
                                                decimal totamt = Sum(Convert.ToDecimal("0" + txtQuantity.Value), t1, Convert.ToDecimal("0" + txtDays.Value));

                                                lblqty2.Text = txtQuantity.Value + " / Rate: " + t1;
                                                obj.Amount = lblqty2.Text;
                                                txtTotalAmount.Text = totamt.ToString();
                                                txtitemcost.Text = t1.ToString();
                                                lblamount2.Text ="Rs. "+Convert.ToDecimal("0" + dt4.Rows[0]["SPECIALCOST"].ToString());

                                            }
                                            else
                                            {
                                                decimal totamt = Sum(Convert.ToDecimal("0" + dt4.Rows[0]["EMPLOYEECOST"].ToString()), 1, 1);

                                                lblqty2.Text = txtQuantity.Value + " / Rate: " + dt4.Rows[0]["EMPLOYEECOST"].ToString();
                                                obj.Amount = lblqty2.Text;
                                                txtTotalAmount.Text = totamt.ToString();
                                                txtitemcost.Text = totamt.ToString();
                                                lblamount2.Text = "Rs. " + dt4.Rows[0]["ITEMCOST"].ToString();
                                            }

                                        }


                                    }


                                   
                                }
                                else
                                {
                                }
                                txtQuantity.Select();
                            }
                            catch (Exception ex) { }
                            if (lblempid2.Text != "")
                            {
                                string sel4 = "SELECT  count(a.category)as category,a.systemdate from asptblcantoken a where A.IDCARDNO='" + Class.Users.IDCARDNO + "' and   A.COMPCODE='" + Class.Users.COMPCODE + "'  AND A.USERNAME='" + Class.Users.USERID + "'   AND a.category='" + dt2.Rows[0]["category"].ToString() + "' AND a.tokendate=to_date('" + dateTimePicker1.Value.ToString("dd-MM-yyyy") + "', 'dd-MM-yyyy') group by a.systemdate";
                                DataSet ds4 = Utility.ExecuteSelectQuery(sel4, "asptblcantoken");
                                DataTable dt4 = ds4.Tables["asptblcantoken"];   
                                if (dt4.Rows.Count==0 || Convert.ToInt32("0" + dt4.Rows[0]["category"].ToString()) == 0)
                                {
                                    lblMarquee1.Visible = true;
                                    Savess();
                                    return;
                                }                            
                                else
                                {
                                    lblmar1.Visible = true; lblmar2.Visible = true; lblmar3.Visible = true;
                                    lblmar1.Refresh(); lblmar2.Refresh(); lblmar3.Refresh();
                                    lblmar1.ForeColor = Class.Users.BackColors; lblmar2.ForeColor = Class.Users.BackColors;  lblmar3.ForeColor = Class.Users.BackColors;
                                    lblmar1.BackColor = Color.White; lblmar2.BackColor = Color.White; lblmar3.BackColor = Color.White;
                                    lblMarquee1.Visible = false; lblMarquee1.Text = "";
                                   
                                    lblmar1.Text = "Dear " + lblempname2.Text.ToUpper() + "... ஏற்கனவே நீங்கள் இந்த டோக்கனை எடுத்துள்ளீர்கள் '" + dt2.Rows[0]["category"].ToString() + "' Date : '" + dt4.Rows[0]["systemdate"].ToString() + "'".Trim();
                                    lblmar2.Text = "Dear " + lblempname2.Text.ToUpper() + "... You have already taken this token                     '" + dt2.Rows[0]["category"].ToString() + "' Date : '" + dt4.Rows[0]["systemdate"].ToString() + "'".Trim();
                                    lblmar3.Text = "Dear " + lblempname2.Text.ToUpper() + "... आप पहले ही यह टोकन ले चुके हैं                           '" + dt2.Rows[0]["category"].ToString() + "' Date : '" + dt4.Rows[0]["systemdate"].ToString() + "'".Trim();

                                    
                                    Class.Users.IDCARDNO = 0; lblempname2.Text = "";


                                }
                            }
                            Class.Users.CANTEENMENUNAME = ""; Class.Users.TOKENEMPID = 0;
                            lblempid2.Text = "";


                        }

                    }
                    else
                    {
                        MessageBox.Show("This IDCard  '"+ Class.Users.TOKENEMPID + "' not  found in DataBase.", " Canteen", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Class.Users.TOKENEMPID = 0;
                        return;
                    }
                }
                else
                {
                    Class.Users.TOKENEMPID = 0; pictureemp.Image = null;
                    Cursor = Cursors.Default;  lblempname.Text = ""; lblIdcardno.Text = "";
                   


                }
            }
            catch (Exception EX)
            {
                MessageBox.Show( EX.Message, " Canteen", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                timercanteen.Enabled = false;
                lblMarquee1.Visible = true;
            }
        }

        private void Username_Click1(object sender, EventArgs e)
        {
           
        }

   

        private void CanteenItemMaster_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }

      
    

        private void ItemRefreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pop();
          
        }

      

        private  void Timercanteen_Tick(object sender, EventArgs e)
        {
            string ccode = "";
            try
            {
                ccode = Class.Users.HCompcode;
                lvLogs.Items.Clear();
               Cursor = Cursors.WaitCursor;
                int k = 0;

              //  timercanteen.Enabled = true;

                iGLCount = 0;

                string ip = "";
               

               DataTable dt = Utility.SQLQuery("SELECT DISTINCT A.ASPTBLMACIPID , A.MACIP     FROM  ASPTBLMACIP   A  JOIN ASPTBLMACHINEMAS B ON B.IPADDRESS=A.ASPTBLMACIPID        AND B.ACTIVE='T'      JOIN ASPTBLUSERMAS C ON  B.WARDENNAME=C.USERID      JOIN  GTCOMPMAST D ON D.GTCOMPMASTID=C.COMPCODE  AND B.COMPCODE=D.GTCOMPMASTID WHERE D.COMPCODE = '" + Class.Users.HCompcode + "' AND C.USERNAME = '" + Class.Users.HUserName + "'  AND A.ACTIVE='T'");

                int maxip = dt.Rows.Count;
                if (maxip >= 2) { MessageBox.Show("Multiple Ip Address Show in Canteen   : " + maxip.ToString() + "SELECT DISTINCT A.ASPTBLMACIPID , A.MACIP     FROM  ASPTBLMACIP   A  JOIN ASPTBLMACHINEMAS B ON B.IPADDRESS=A.ASPTBLMACIPID        AND B.ACTIVE='T'      JOIN ASPTBLUSERMAS C ON  B.WARDENNAME=C.USERID      JOIN  GTCOMPMAST D ON D.GTCOMPMASTID=C.COMPCODE  AND B.COMPCODE=D.GTCOMPMASTID WHERE D.COMPCODE = '" + Class.Users.HCompcode + "' AND C.USERNAME = '" + Class.Users.HUserName + "'  AND A.ACTIVE='T'") ; return; }
                else
                {
                    if (maxip == 0)
                    {
                        MessageBox.Show("IP Address does not assign this User.   : " + Class.Users.HUserName);
                        Cursor = Cursors.Default;
                    }
                    if (maxip >= 1)
                    {
                        Class.Users.UserTime = 0;
                        Ping ping = new Ping();
                        PingReply reply = ping.Send(dt.Rows[0]["MACIP"].ToString(), 1000);
                        if (reply.Status.ToString() == "Success")
                        {

                            int i = 0; iIndex = 0;
                            for (i = 0; i < maxip; i++)
                            {
                                if (bIsConnected == false)
                                {
                                    bIsConnected = axCZKEM1.Connect_Net(dt.Rows[i]["MACIP"].ToString(), Convert.ToInt32(4370));
                                }
                                ip = dt.Rows[i]["MACIP"].ToString();

                                if (bIsConnected == true)
                                {

                                    if (axCZKEM1.ReadGeneralLogData(iMachineNumber))//read all the attendance records to the memory
                                    {

                                        while (axCZKEM1.SSR_GetGeneralLogData(iMachineNumber, out sdwEnrollNumber, out idwVerifyMode, out idwInOutMode, out idwYear, out idwMonth, out idwDay, out idwHour, out idwMinute, out idwSecond, ref idwWorkcode))//get records from the memory
                                        {
                                            DateTime inputDate = new DateTime(idwYear, idwMonth, idwDay, idwHour, idwMinute, idwMinute);
                                            if (Convert.ToDateTime(inputDate) >= System.DateTime.Now.Date && Convert.ToDateTime(inputDate) <= System.DateTime.Now.Date.AddDays(1).AddTicks(-1))
                                            {

                                                string idcard = sdwEnrollNumber;
                                                // DataTable DT2=await CC.SelectCommond();
                                                string sel1 = "SELECT  A.HREMPLOYMASTID,A.FNAME,B.MIDCARD FROM HREMPLOYMAST A JOIN HREMPLOYDETAILS B ON A.HREMPLOYMASTID=B.HREMPLOYMASTID   WHERE B.MIDCARD='" + idcard + "' AND B.IDACTIVE='YES'";
                                                DataSet ds1 = Utility.ExecuteSelectQuery(sel1, "HREMPLOYMAST");
                                                DataTable dt2 = ds1.Tables["HREMPLOYMAST"];
                                                if (dt2.Rows.Count > 0)
                                                {
                                                    iGLCount++;

                                                    lvLogs.Items.Add(iGLCount.ToString());
                                                    lvLogs.Items[iIndex].SubItems.Add(idcard.ToString());//modify by Darcy on Nov.26 2009
                                                    lvLogs.Items[iIndex].SubItems.Add(dt2.Rows[0]["FNAME"].ToString());//modify by Darcy on Nov.26 2009
                                                    lvLogs.Items[iIndex].SubItems.Add(dt2.Rows[0]["MIDCARD"].ToString());//modify by Darcy on Nov.26 2009

                                                    iIndex++;

                                                }

                                            }

                                        }

                                    }
                                    else
                                    {
                                        Cursor = Cursors.Default;
                                        MessageBox.Show("No Data Found this Machine...." + ip.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                    axCZKEM1.EnableDevice(iMachineNumber, true);//enable the device    
                                }
                                else
                                {
                                    Cursor = Cursors.Default;
                                    axCZKEM1.GetLastError(ref idwErrorCode);

                                    MessageBox.Show("Unable to connect the device", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                }
                                Cursor = Cursors.Default;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Unable to connect the device", "Error. Pls check NetWork", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            Cursor = Cursors.Default;
                        }
                    }
                    Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                Cursor = Cursors.Default;
            }
        }

        private void Saves_Click(object sender, EventArgs e)
        {

        }

        private void News_Click(object sender, EventArgs e)
        {

        }
        int CallDays()
        {
            int DAYS = 0; 
            try
            {
                string sel = "SELECT DAYS FROM CANITEMDISPLAYDAYS";
                DataSet ds = Utility.ExecuteSelectQuery(sel, "CANITEMDISPLAYDAYS");
                DataTable dt = ds.Tables["CANITEMDISPLAYDAYS"];
                DAYS = Convert.ToInt32("0" + dt.Rows[0]["DAYS"].ToString());
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return DAYS;
        }
        int CallTime()
        {
            int times = 0;
            try
            {
                string sel = "SELECT times FROM CANITEMDISPLAYDAYS";
                DataSet ds = Utility.ExecuteSelectQuery(sel, "CANITEMDISPLAYDAYS");
                DataTable dt = ds.Tables["CANITEMDISPLAYDAYS"];                
                times = Convert.ToInt32("0" + dt.Rows[0]["times"].ToString());

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return times;
        }

        public void News()
        {
            try
            {
                this.Font = Class.Users.FontName;
                flowLayoutPanel1.Font = Class.Users.FontName;
                Class.Users.CANTEENMENUNAME = ""; Class.Users.TOKENEMPID = 0;
                lblIdcardno.Text = ""; lblempname.Text = "";
                pictureemp.Image = null;lblmar1.Text = ""; lblmar2.Text = ""; lblmar3.Text = "";

                lblmar1.Refresh(); lblmar2.Refresh(); lblmar3.Refresh();
                lblmar1.Visible = false ; lblmar2.Visible = false;  lblmar3.Visible = false;
                bIsConnected = false; lblMarquee1.Visible = true; lblMarquee1.ForeColor = Class.Users.BackColors;
                pop();
                string items = ""; lblMarquee1.Refresh(); lblMarquee1.Text = "";
                string sel = "SELECT distinct C.ITEMNAME1  FROM ASPTBLMENPERDET  A JOIN ASPTBLMENPER B ON A.ASPTBLMENPERID=B.ASPTBLMENPERID  JOIN ASPTBLCANITEMMAS C ON C.ASPTBLCANITEMMASID=A.ITEMNAME1 JOIN ASPTBLUSERMAS D ON D.COMPCODE=B.COMPCODE AND D.USERID=B.USERNAME  JOIN ASPTBLCANCATEGORYMAS E ON E.ASPTBLCANCATEGORYMASID = A.CATEGORY WHERE A.ACTIVE='T'  AND C.ACTIVE='T'  AND D.COMPCODE='" + Class.Users.COMPCODE + "'  AND D.USERID='" + Class.Users.USERID + "'  AND B.DOCDATE BETWEEN TO_DATE(SYSDATE) AND TO_DATE(SYSDATE)    AND TO_TIMESTAMP(TO_CHAR(SYSDATE,'DD/MM/YY HH24:MI:SS'),'DD/MM/YY HH24:MI:SS')  BETWEEN TO_TIMESTAMP(A.FROMDATE||' '||A.FROMTIME,'DD/MM/YY HH24:MI:SS') AND TO_TIMESTAMP(A.TODATE||' '||A.TOTIME,'DD/MM/YY HH24:MI:SS')   ORDER BY 1";
                DataSet ds = Utility.ExecuteSelectQuery(sel, "ASPTBLCANITEMMAS");
                DataTable dt = ds.Tables["ASPTBLCANITEMMAS"];
                foreach (DataRow row in dt.Rows)
                {
                    items += row["ITEMNAME1"].ToString() + " - ";
                }

                lblMarquee1.Text = "WELCOME TO " + Class.Users.HCompName + "-TODAY MENU ITEM  :" + items;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        Pinnacle.Models.MailModel obj = new Models.MailModel();
        QRCoder.QRCodeGenerator qc = new QRCoder.QRCodeGenerator();
        public void Saves()
        { }
            public void Savess()
        {
            try
            {

                TimeSpan totaltime;
                var currentDateTime = DateTime.Now;
                var currentTimeAlone = new TimeSpan(currentDateTime.Hour, currentDateTime.Minute, currentDateTime.Second);
                int count = Convert.ToInt32(txtQuantity.Value) * Convert.ToInt32(txtDays.Value);
                string token = System.DateTime.Now.Year + "/" + Class.Users.HCompcode + "CAN";

                decimal totamt = 0;
                string chk = ""; if (checkactive.Checked == true) { chk = "T"; } else { chk = "F"; }

                try
                {
                    //if (combooptions.Text == "SINGLE")
                    // {

                    for (int i = 0; i < count; i++)
                    {

                        totamt = 0;
                        string sel2 = "SELECT  NVL(MAX(A.ASPTBLCANTOKENID)+1,1) ID FROM ASPTBLCANTOKEN A ";
                        DataSet ds2 = Utility.ExecuteSelectQuery(sel2, "ASPTBLCANTOKEN");
                        DataTable dt2 = ds2.Tables["ASPTBLCANTOKEN"];
                        if (System.DateTime.Now.ToString("dd-MM-yyyy") == dateTimePicker1.Value.ToString("dd-MM-yyyy").Substring(0, 10))
                        {
                            string ins = "INSERT INTO ASPTBLCANTOKEN(TOKENNO,EMPID,EMPNAME,IDCARDNO,ITEMCODE,ITEMNAME1,ITEMCOST,ITEMQTY,NOOFDAYS ,TOTALAMOUNT,ACTIVE  ,USERNAME,MODIFIED,CREATEDON,IPADDRESS,TOKENNOCANCEL,EMPLOYEETYPE,TOKENOPTION,COMPCODE,FINYEAR,EMPLOYEECOST,SPECIALCOST,tokendate,TOKENTIME,SYSTEMDATE,TOKEN_FROMTIME,category)VALUES('" + token + "/" + dt2.Rows[0]["id"].ToString() + "','" + txtempid.Text + "','" + comboempname.SelectedValue + "','" + comboidcardno.Text + "','" + comboitemcode.SelectedValue + "','" + comboitemname.SelectedValue + "','" + txtitemcost.Text + "','" + txtQuantity.Text + "','" + txtDays.Text + "','" + txtTotalAmount.Text + "','" + chk + "','" + Class.Users.USERID + "','" + Convert.ToString(Class.Users.CREATED) + "','" + Convert.ToString(Class.Users.CREATED) + "','" + Class.Users.IPADDRESS + "','T','" + Class.Users.UniqueID + "','" + combooptions.Text + "','" + Class.Users.COMPCODE + "','" + Class.Users.Finyear + "','" + txtempcost.Text + "','" + txtspecialcost.Text + "',TO_DATE('" + dateTimePicker1.Value.ToString("dd-MM-yyyy") + "','dd-MM-yyyy'),'" + dateTimePicker1.Value.ToString("hh:mm:ss") + "',TO_DATE('" + System.DateTime.Now.ToString() + "','dd-MM-yyyy hh24:mi:ss'),'" + dateTimePicker1.Value.ToString("hh:mm:ss") + "','" + lblempid2.Text + "')";
                            Utility.ExecuteNonQuery(ins);
                        }
                        else
                        {
                            string ins = "INSERT INTO ASPTBLCANTOKEN(TOKENNO,EMPID,EMPNAME,IDCARDNO,ITEMCODE,ITEMNAME1,ITEMCOST,ITEMQTY,NOOFDAYS ,TOTALAMOUNT,ACTIVE  ,USERNAME,MODIFIED,CREATEDON,IPADDRESS,TOKENNOCANCEL,EMPLOYEETYPE,TOKENOPTION,COMPCODE,FINYEAR,EMPLOYEECOST,SPECIALCOST,tokendate,TOKENTIME,SYSTEMDATE,TOKEN_FROMTIME,category)VALUES('" + token + "/" + dt2.Rows[0]["id"].ToString() + "','" + txtempid.Text + "','" + comboempname.SelectedValue + "','" + comboidcardno.Text + "','" + comboitemcode.SelectedValue + "','" + comboitemname.SelectedValue + "','" + txtitemcost.Text + "','" + txtQuantity.Text + "','" + txtDays.Text + "','" + txtTotalAmount.Text + "','" + chk + "','" + Class.Users.USERID + "','" + Convert.ToString(Class.Users.CREATED) + "','" + Convert.ToString(Class.Users.CREATED) + "','" + Class.Users.IPADDRESS + "','T','" + Class.Users.UniqueID + "','" + combooptions.Text + "','" + Class.Users.COMPCODE + "','" + Class.Users.Finyear + "','" + txtempcost.Text + "','0',TO_DATE('" + dateTimePicker1.Value.ToString("dd-MM-yyyy") + "','dd-MM-yyyy'),'" + dateTimePicker1.Value.ToString("hh:mm:ss") + "',TO_DATE('" + System.DateTime.Now.ToString() + "','dd-MM-yyyy hh24:mi:ss'),'" + dateTimePicker1.Value.ToString("hh:mm:ss") + "','" + lblempid2.Text + "')";
                            Utility.ExecuteNonQuery(ins);
                        }
                        string sel3 = "select max(A.ASPTBLCANTOKENID) id3 FROM ASPTBLCANTOKEN A ";
                        DataSet ds3 = Utility.ExecuteSelectQuery(sel3, "ASPTBLCANTOKEN");
                        DataTable dt3 = ds3.Tables["ASPTBLCANTOKEN"];

                        if (dt3.Rows[0]["id3"].ToString() != "")
                        {
                            string sel4 = "SELECT  A.TOKENNO, B.HREMPLOYMASTID,B.FNAME,D.MIDCARD,C.ITEMCODE,C.ITEMNAME1, C.EMPLOYEECOST,A.ITEMQTY ,A.NOOFDAYS,A.TOTALAMOUNT,C.ITEMCOST,C.SPECIALCOST,E.COMPCODE FROM ASPTBLCANTOKEN A   JOIN HREMPLOYMAST B ON  A.EMPID = B.HREMPLOYMASTID  JOIN ASPTBLCANITEMMAS C ON C.ASPTBLCANITEMMASID = A.ITEMNAME1 JOIN HREMPLOYDETAILS D ON B.HREMPLOYMASTID=D.HREMPLOYMASTID JOIN GTCOMPMAST E ON E.GTCOMPMASTID=B.COMPCODE   WHERE A.ASPTBLCANTOKENID=" + dt3.Rows[0]["id3"].ToString();
                            DataSet ds4 = Utility.ExecuteSelectQuery(sel4, "ASPTBLCANTOKEN");
                            DataTable dt4 = ds4.Tables["ASPTBLCANTOKEN"];
                            lbltoken2.Text = dt4.Rows[0]["TOKENNO"].ToString();
                            // lblempid2.Text = dt4.Rows[0]["MIDCARD"].ToString();
                            lblcompcode2.Text = dt4.Rows[0]["COMPCODE"].ToString();
                            lblidcard2.Text = dt4.Rows[0]["MIDCARD"].ToString();
                            lblempname2.Text = dt4.Rows[0]["FNAME"].ToString().ToUpper();
                            lblitemname2.Text = dt4.Rows[0]["ITEMNAME1"].ToString();
                            lbldate2.Text = Convert.ToString(dateTimePicker1.Value.ToString().Substring(0, 10));
                            obj.ID = dt4.Rows[0]["TOKENNO"].ToString();
                            obj.VisitorName = dt4.Rows[0]["FNAME"].ToString();
                            obj.Company = Class.Users.HCompName;
                            obj.MobileNo = dt4.Rows[0]["MIDCARD"].ToString();
                            obj.Purpose = "Lunch Purpose";
                            if (dt4.Rows.Count > 0)
                            {


                                if (System.DateTime.Now.ToString("dd-MM-yyyy") == dateTimePicker1.Value.ToString("dd-MM-yyyy").Substring(0, 10))
                                {
                                    decimal t1 = add(Convert.ToDecimal("0" + dt4.Rows[0]["EMPLOYEECOST"].ToString()), Convert.ToDecimal("0" + dt4.Rows[0]["SPECIALCOST"].ToString()));
                                    totamt = Sum(Convert.ToDecimal("0" + txtQuantity.Value), t1, Convert.ToDecimal("0" + txtDays.Value));

                                    //lblitemcost.Text = "Rate : " + totamt;
                                    lblqty2.Text = txtQuantity.Value + " / Rate: " + t1;
                                    obj.Amount = lblqty2.Text;
                                    txtTotalAmount.Text = totamt.ToString();
                                    lblamount2.Text ="Rs ."+ Convert.ToDecimal("0" + dt4.Rows[0]["SPECIALCOST"].ToString());
                                   
                                }
                                else
                                {
                                    totamt = Sum(Convert.ToDecimal("0" + dt4.Rows[0]["EMPLOYEECOST"].ToString()), Convert.ToDecimal("0" + txtQuantity.Value), Convert.ToDecimal("0" + txtDays.Value));

                                    lblqty2.Text = txtQuantity.Value + " / Rate: " + dt4.Rows[0]["EMPLOYEECOST"].ToString();
                                    obj.Amount = lblqty2.Text;
                                    txtTotalAmount.Text = totamt.ToString();
                                    lblamount2.Text = "Rs ." + dt4.Rows[0]["ITEMCOST"].ToString();

                                    
                                }

                            }

                            lblnoofdays2.Text = txtDays.Value + "/" + count + "  " + "No's";
                            lblcompcode.Text = Class.Users.HCompName;
                            lbldatetime.Text = Convert.ToString(Class.Users.CREATED);
                            var mydata = qc.CreateQrCode(lbltoken2.Text, QRCoder.QRCodeGenerator.ECCLevel.L);
                            var code = new QRCoder.QRCode(mydata);
                            pictureBox1.Image = code.GetGraphic(50, Color.Black, Color.White, true);



                            printDocument1.PrinterSettings = printDialog1.PrinterSettings;
                            printDocument1.Print();
                        }
                    }

                    string sel0 = "SELECT  MIN(A.FROMDATE)  AS FROMDATE1, MIN(A.FROMTIME) AS FROMTIME1,MIN(A.TODATE) AS TODATE1, MIN(A.TOTIME) AS TOTIME1,MIN(A.SYSTEMTIME) AS SYSTEMTIME FROM ASPTBLMENPERDET  A JOIN ASPTBLMENPER B ON A.ASPTBLMENPERID=B.ASPTBLMENPERID  JOIN ASPTBLCANITEMMAS C ON C.ASPTBLCANITEMMASID=A.ITEMNAME1 JOIN ASPTBLUSERMAS D ON D.COMPCODE=B.COMPCODE AND D.USERID=B.USERNAME  WHERE A.ACTIVE='T'  AND C.ACTIVE='T'  AND D.COMPCODE='" + Class.Users.COMPCODE + "'  AND D.USERID='" + Class.Users.USERID + "' AND A.TODATE=TO_DATE('" + System.DateTime.Now.ToString("dd-MM-yyyy").Substring(0, 10) + "','dd-MM-yyyy')   ORDER BY 1";//AND A.TODATE=TO_DATE('"+dateTimePicker1.Value.ToString("dd-MM-yyyy")+"','dd-MM-yyyy')
                    DataSet ds0 = Utility.ExecuteSelectQuery(sel0, "ASPTBLCANITEMMAS");
                    DataTable dt0 = ds0.Tables["ASPTBLCANITEMMAS"];


                    if (dt0.Rows.Count > 0 && dt0.Rows[0]["totime1"].ToString() != "")
                    {
                        TimeSpan fromtime1 = TimeSpan.Parse(System.DateTime.Now.ToString("HH:mm:ss"));
                        TimeSpan totime1 = TimeSpan.Parse(dt0.Rows[0]["totime1"].ToString());
                        TimeSpan differ = totime1.Subtract(currentTimeAlone);
                        int h1 = differ.Hours * 60; int m1 = differ.Minutes * 60; int se1 = differ.Seconds;
                        int h2 = h1 + m1 + se1;
                        Class.Users.LoginTime = Convert.ToInt64(h2); Class.Users.UserTime = 1;
                    }


                }

                catch (Exception ex)
                {

                }
                finally
                {

                    //DataTable dt = Utility.SQLQuery("SELECT  C.MACIP  FROM  ASPTBLMACHINEMAS A   JOIN   GTCOMPMAST B ON B.GTCOMPMASTID = A.COMPCODE    JOIN ASPTBLMACIP C ON C.ASPTBLMACIPID = A.IPADDRESS  AND C.ACTIVE = 'T'   JOIN  ASPTBLUSERMAS D ON D.USERID = A.WARDENNAME  AND D.COMPCODE = B.GTCOMPMASTID  WHERE B.COMPCODE = '" + Class.Users.HCompcode + "' AND D.USERNAME = '" + Class.Users.HUserName + "' AND A.ACTIVE='T'  ORDER BY 1 ");
                    //bIsConnected = axCZKEM1.Connect_Net(dt.Rows[0]["MACIP"].ToString(), Convert.ToInt32(4370));
                    //if (bIsConnected == true)
                    //{
                    //    axCZKEM1.EnableDevice(iMachineNumber, false);//disable the device


                    //    axCZKEM1.EnableDevice(iMachineNumber, false);//disable the device
                    //    if (axCZKEM1.ClearGLog(iMachineNumber))
                    //    {
                    //        axCZKEM1.RefreshData(iMachineNumber);//the data in the device should be refreshed
                    //    }

                    //    axCZKEM1.EnableDevice(iMachineNumber, true);//enable the device

                    //}

                    Class.Users.UniqueID = ""; txtempcost.Text = "";
                    txtcontractconst.Text = "";
                    txtspecialcost.Text = "";
                }


            }
            catch (Exception ex)
            {

            }
        }

        public static decimal Sum(decimal num1, decimal num2)
        {
            Decimal total;
            total = num1 * num2;
            return total;
        }

        public static decimal add(decimal num1, decimal num2, decimal num3)
        {
            decimal total;
            total = num1 + num2 + num3;
            return total;
        }
        public static decimal Sum(decimal num1, decimal num2, decimal num3)
        {
            decimal total;
            total = num1 * num2 * num3;
            return total;
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

        public void ReadOnlys()
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
            GlobalVariables.MdiPanel.Show();
          
            GlobalVariables.HeaderName.Text = "";
            GlobalVariables.TabCtrl.TabPages.RemoveAt(GlobalVariables.TabCtrl.SelectedIndex);
           

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
          
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            comboitemload();
        }

        private void butdatewise_Click(object sender, EventArgs e)
        {
           
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //lblmar1.Width = 0; lblmar2.Width = 0; lblmar3.Width = 0; lblMarquee1.Width = 0;
            if (lblMarquee1.Left < 0 && (Math.Abs(lblMarquee1.Left) > lblMarquee1.Width))            
                lblMarquee1.Left = panel2.Width;
                lblMarquee1.Left -= 4;
           

            if (lblmar1.Left < 0 && (Math.Abs(lblmar1.Left) > lblmar1.Width))           
                lblmar1.Left = panel2.Width;
                lblmar1.Left -= 4;
            


            if (lblmar2.Left < 0 && (Math.Abs(lblmar2.Left) > lblmar2.Width))            
                lblmar2.Left = panel2.Width;
                lblmar2.Left -= 4;
            


            if (lblmar3.Left < 0 && (Math.Abs(lblmar3.Left) > lblmar3.Width))            
                lblmar3.Left = panel2.Width;
                lblmar3.Left -= 4;
            
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {
                e.Graphics.DrawString(lblheading.Text.ToUpper(), new Font("roboto", 12, FontStyle.Bold), Brushes.DarkBlue, 43, 32);

                e.Graphics.DrawString(lblcompcode1.Text.ToUpper(), new Font("roboto", 9, FontStyle.Bold), Brushes.DarkBlue, 16, 63);
                e.Graphics.DrawString(lblcompcode2.Text.ToUpper(), new Font("roboto", 9, FontStyle.Bold), Brushes.DarkBlue, 106, 63);

                e.Graphics.DrawString(lbltoken1.Text.ToUpper(), new Font("roboto", 9, FontStyle.Bold), Brushes.DarkBlue, 16, 90);
                e.Graphics.DrawString(lbltoken2.Text.ToUpper(), new Font("roboto", 9, FontStyle.Bold), Brushes.DarkBlue, 106, 90);


                e.Graphics.DrawString(lblidcard1.Text.ToUpper(), new Font("roboto", 9, FontStyle.Bold), Brushes.DarkBlue, 16, 120);
                e.Graphics.DrawString(lblidcard2.Text.ToUpper(), new Font("roboto", 9, FontStyle.Bold), Brushes.DarkBlue, 106, 120);

                e.Graphics.DrawString(lblempname1.Text.ToUpper(), new Font("roboto", 9, FontStyle.Bold), Brushes.DarkBlue, 16, 150);
                e.Graphics.DrawString(lblempname2.Text.ToUpper(), new Font("roboto", 9, FontStyle.Bold), Brushes.DarkBlue, 106, 150);

                e.Graphics.DrawString(lblempid1.Text.ToUpper(), new Font("roboto", 9, FontStyle.Bold), Brushes.DarkBlue, 16, 180);
                e.Graphics.DrawString(lblempid2.Text.ToUpper(), new Font("roboto", 9, FontStyle.Bold), Brushes.DarkBlue, 106, 180);

                e.Graphics.DrawString(lblitemname1.Text.ToUpper(), new Font("roboto", 9, FontStyle.Bold), Brushes.DarkBlue, 16, 210);
                e.Graphics.DrawString(lblitemname2.Text.ToUpper(), new Font("roboto", 9, FontStyle.Bold), Brushes.DarkBlue, 106, 210);

                e.Graphics.DrawString(lblamount1.Text.ToUpper(), new Font("roboto", 9, FontStyle.Bold), Brushes.DarkBlue, 16, 240);
                e.Graphics.DrawString(lblamount2.Text.ToUpper(), new Font("roboto", 9, FontStyle.Bold), Brushes.DarkBlue, 106, 240);


                //e.Graphics.DrawString(lbldate1.Text.ToUpper(), new Font("roboto", 9, FontStyle.Bold), Brushes.DarkBlue, 16, 240);
                //e.Graphics.DrawString(lbldate2.Text.ToUpper(), new Font("roboto", 9, FontStyle.Bold), Brushes.DarkBlue, 106, 240);

                e.Graphics.DrawString(lbldate1.Text.ToUpper(), new Font("roboto", 9, FontStyle.Bold), Brushes.DarkBlue, 16, 270);
                e.Graphics.DrawString(lbldate2.Text.ToUpper(), new Font("roboto", 9, FontStyle.Bold), Brushes.DarkBlue, 106, 270);


               

                e.Graphics.DrawString(lblcompcode.Text.ToUpper(), new Font("roboto", 8, FontStyle.Regular), Brushes.DarkBlue, 16, 310);
                e.Graphics.DrawString(lbldatetime.Text, new Font("roboto", 8, FontStyle.Bold), Brushes.DarkBlue, 16, 330);

              
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            finally
            {
               
            }
        }

        private void combooptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                txtDays.Enabled = true;
                if (Convert.ToInt64(Class.Users.TOKENEMPID) > 0)
                {
                    string sel1 = "SELECT  A.HREMPLOYMASTID,A.FNAME,B.MIDCARD FROM HREMPLOYMAST A JOIN HREMPLOYDETAILS B ON A.HREMPLOYMASTID=B.HREMPLOYMASTID   WHERE B.MIDCARD='" + Class.Users.TOKENEMPID + "' AND B.IDACTIVE='YES'";
                    DataSet ds1 = Utility.ExecuteSelectQuery(sel1, "HREMPLOYMAST");
                    DataTable dt1 = ds1.Tables["HREMPLOYMAST"];

                    txtempid.Text = dt1.Rows[0]["HREMPLOYMASTID"].ToString();
                    comboempname.DisplayMember = "FNAME";
                    comboempname.ValueMember = "HREMPLOYMASTID";

                    comboidcardno.DisplayMember = "MIDCARD";
                    comboidcardno.ValueMember = "MIDCARD";
                    comboempname.DataSource = dt1;
                    comboidcardno.DataSource = dt1;

                    string sel = "SELECT DISTINCT C.ASPTBLCANITEMMASID,C.ITEMCODE, C.ITEMNAME1,C.EMPLOYEECOST ,C.SPECIALCOST,to_date('" + System.DateTime.Now.ToString("dd-MM-yyyy") + "', 'dd-MM-yyyy') AS TODATE  ,e.category       FROM ASPTBLMENPER A JOIN ASPTBLMENPERDET B ON A.ASPTBLMENPERID = B.ASPTBLMENPERID            JOIN ASPTBLCANITEMMAS C ON C.ASPTBLCANITEMMASID = B.ITEMNAME1 AND C.ITEMCODE = B.ITEMCODE JOIN ASPTBLUSERMAS D ON  D.USERID = A.USERNAME  AND D.COMPCODE = A.COMPCODE   join asptblcancategorymas e on e.ASPTBLCANCATEGORYMASID=b.category    WHERE A.COMPCODE='" + Class.Users.COMPCODE + "'  AND A.USERNAME='" + Class.Users.USERID + "' AND C.ITEMNAME1='" + Class.Users.CANTEENMENUNAME + "' AND A.ACTIVE = 'T'";
                    DataSet ds = Utility.ExecuteSelectQuery(sel, "ASPTBLCANITEMMAS");
                    DataTable dt = ds.Tables["ASPTBLCANITEMMAS"];
                    if (dt.Rows.Count > 0)
                    {
                        comboitemcode.DisplayMember = "ITEMCODE";
                        comboitemcode.ValueMember = "ASPTBLCANITEMMASID";
                        comboitemname.DisplayMember = "ITEMNAME1";
                        comboitemname.ValueMember = "ASPTBLCANITEMMASID";
                
                        //txtitemcost.Text = "";
                        decimal V1 = Convert.ToDecimal("0" + dt.Rows[0]["EMPLOYEECOST"].ToString());
                        txtempcost.Text = V1.ToString();
                        lblempid2.Text = dt.Rows[0]["category"].ToString();
                        decimal V3 = Convert.ToDecimal("0" + dt.Rows[0]["SPECIALCOST"].ToString());
                        txtspecialcost.Text = V3.ToString();
                        comboitemcode.DataSource = dt;
                        comboitemname.DataSource = dt;
                        if (dt.Rows.Count > 0)
                        {

                            if (System.DateTime.Now.ToString("dd-MM-yyyy") == dateTimePicker1.Value.ToString("dd-MM-yyyy").Substring(0, 10))
                            {
                                decimal t1 = add(Convert.ToDecimal("0" + dt.Rows[0]["EMPLOYEECOST"].ToString()), Convert.ToDecimal("0" + dt.Rows[0]["SPECIALCOST"].ToString()));
                                decimal totamt = Sum(Convert.ToDecimal("0" + txtQuantity.Value), t1, Convert.ToDecimal("0" + txtDays.Value));

                                lblqty2.Text = txtQuantity.Value + " / Rate: " + t1;
                                obj.Amount = lblqty2.Text;
                                txtTotalAmount.Text = totamt.ToString();
                            }
                            else
                            {
                                decimal totamt = Sum(Convert.ToDecimal("0" + dt.Rows[0]["EMPLOYEECOST"].ToString()), Convert.ToDecimal("0" + txtQuantity.Value), Convert.ToDecimal("0" + txtDays.Value));

                                lblqty2.Text = txtQuantity.Value + " / Rate: " + dt.Rows[0]["EMPLOYEECOST"].ToString();
                                obj.Amount = lblqty2.Text;
                                txtTotalAmount.Text = totamt.ToString();
                            }

                        }


                    }


              
                }
                else
                {
                }
                txtQuantity.Select();
            }
            catch (Exception ex) { }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(tabControl1.SelectedTab== tabControl1.TabPages["tabPage3"])
            {
                this.Enabled = true;
                combomenu_SelectedIndexChanged(sender, e);
            }
        }
        Report.Canteen.CanteenReport rd = new Report.Canteen.CanteenReport();
        private void combomenu_SelectedIndexChanged(object sender, EventArgs e)
        {
         
        }

        private void butsubmit_Click(object sender, EventArgs e)
        {
            string sel = "select substr(AA.TOKENDATE,0,10) AS DOCDATE,AB.CATEGORY,  count(AA.ASPTBLCANTOKENID) as total ,AC.COMPNAME,AC.ADDRESS,(SELECT LOGO AS IMAGE FROM EDOCIMAGE WHERE IMGNAME = 'COMPLOGOID' AND COMPANYID ='" + Class.Users.HCompcode + "') AS IMAGE,AB.ASPTBLCANCATEGORYMASID,'"+dateTimePicker4.Value.ToString().Substring(0,10)+ "' AS  DataColumn2,'" + dateTimePicker5.Value.ToString().Substring(0, 10) + "' AS  DataColumn3 from asptblcantoken AA join ASPTBLCANCATEGORYMAS AB on AA.category = AB.category join gtcompmast ac on AC.GTCOMPMASTID=AA.COMPCODE  where AA.TOKENDATE BETWEEN TO_DATE('" + dateTimePicker4.Value.ToString().Substring(0, 10) + "','DD-MM-YY') AND TO_DATE('" + dateTimePicker5.Value.ToString().Substring(0, 10) + "','DD-MM-YY')   AND AA.COMPCODE='" + Class.Users.COMPCODE + "'  AND AA.USERNAME='" + Class.Users.USERID + "'  GROUP BY AA.TOKENDATE,AB.CATEGORY,AC.COMPNAME,AC.ADDRESS,AB.ASPTBLCANCATEGORYMASID ORDER BY 7";
            DataSet ds = Utility.ExecuteSelectQuery(sel, "ASPTBLCANITEMMAS");
            DataTable dt = ds.Tables["ASPTBLCANITEMMAS"];

            crystalReportViewer1.ReportSource = null;
            rd.SetDataSource(dt);
            crystalReportViewer1.ReportSource = rd;
            crystalReportViewer1.Refresh();
            Int64 total = 0;int i = 0;
            foreach(DataRow dr in dt.Rows)
            {
                total += Convert.ToInt64(dr["TOTAL"].ToString());
              
                i++;
            }
            lbltotal.Text = "Total : " + total.ToString();
            crystalReportViewer1.Zoom(150);
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sel = "select substr(AA.TOKENDATE,0,10) AS DOCDATE,AB.CATEGORY,  count(AA.ASPTBLCANTOKENID) as total  from asptblcantoken AA join ASPTBLCANCATEGORYMAS AB on AA.category = AB.category  where AA.TOKENDATE BETWEEN TO_DATE('" + dateTimePicker4.Value.ToString().Substring(0, 10) + "','DD-MM-YY') AND TO_DATE('" + dateTimePicker5.Value.ToString().Substring(0, 10) + "','DD-MM-YY')   AND AA.COMPCODE='" + Class.Users.COMPCODE + "'  AND AA.USERNAME='" + Class.Users.USERID + "'  GROUP BY AA.TOKENDATE,AB.CATEGORY ORDER BY 1";
            DataSet ds = Utility.ExecuteSelectQuery(sel, "ASPTBLCANITEMMAS");
            DataTable dt = ds.Tables["ASPTBLCANITEMMAS"];
            CrystalDecisions.CrystalReports.Engine.ReportDocument reportdocument = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            reportdocument.Load(Application.StartupPath + "\\Report\\Canteen\\CanteenReport.rpt");
            reportdocument.SetDataSource(dt);
            reportdocument.PrintOptions.PrinterName = printDialog1.PrinterSettings.PrinterName;
            reportdocument.PrintToPrinter(printDialog1.PrinterSettings.Copies, printDialog1.PrinterSettings.Collate, printDialog1.PrinterSettings.FromPage, printDialog1.PrinterSettings.ToPage);

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
