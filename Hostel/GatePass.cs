using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading.Tasks;

namespace Pinnacle.Hostel
{
    public partial class GatePass : Form, ToolStripAccess
    {
        private static GatePass _instance;

        public GatePass()
        {
            InitializeComponent();
            panelprint.Hide();
            Class.Users.IPADDRESS = GenFun.GetLocalIPAddress();
            Class.Users.SysDate = Convert.ToString(System.DateTime.Now.ToString("dd-MM-yyyy"));
            Class.Users.SysTime = Convert.ToString(DateTime.Now.ToLongTimeString().ToString());
            btnhostelsave.Focus();
            DateTime dateForButton = DateTime.Now;
            frmdate.Value = dateForButton;
            btnhostelsave.Visible = false;
            tabControl1.TabPages.Remove(tabPage3);
            GlobalVariables.HeaderName.Text = Class.Users.ScreenName; GlobalVariables.CurrentForm = this;
         
        }

        public void ReadOnlys()
        {

        }
        ListView listfilter = new ListView();
        public static GatePass Instance
        {
            get { if (_instance == null) _instance = new GatePass(); 
                GlobalVariables.CurrentForm = _instance; return _instance; }

        }
        Models.Master mas = new Models.Master();
        Models.UserRights sm = new Models.UserRights();
        Models.MenuName c = new Models.MenuName();
        Models.Device dev = new Models.Device(); byte[] qrbytes; byte[] bytes;

        private bool AGFbIsConnected = false;
        private bool AGFMbIsConnected = false;
        private bool AGFMGIIbIsConnected = false;
        private bool AGFCbIsConnected = false;
        zkemkeeper.CZKEMClass axCZKEM1 = new zkemkeeper.CZKEMClass();
        private bool bIsConnected = false;//the boolean value identifies whether the device is connected
        private int iMachineNumber;//the serial number of the device.After connecting the device ,this value will be changed.      
        string sdwEnrollNumber = "";
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


        public void News()
        {
            GridLoad(); reason(); empty();
            if (Class.Users.HUserName == "VAIRAM")
            {
                tabControl1.TabPages.Remove(tabPage3);
                tabControl1.TabPages.Add(tabPage3);
            }
            else
            {
                tabControl1.TabPages.Remove(tabPage3);
            }
           
        }

        private void reason()
        {
            try
            {
                string sel3 = " select  A.ASPTBLREASONMASID,A.REASON  from ASPTBLREASONMAS a  JOIN GTCOMPMAST B ON B.GTCOMPMASTID=A.COMPCODE where A.REASON='PERSONAL'    AND A.ACTIVE='T'  AND B.COMPCODE='" + Class.Users.HCompcode + "'   UNION ALL     select  A.ASPTBLREASONMASID,A.REASON  from ASPTBLREASONMAS  a JOIN GTCOMPMAST B ON B.GTCOMPMASTID=A.COMPCODE where  A.REASON='OFFICIAL'  AND A.ACTIVE='T'  ";
                DataSet ds3 = Utility.ExecuteSelectQuery(sel3, "ASPTBLREASONMAS");
                DataTable dt3 = ds3.Tables["ASPTBLREASONMAS"];
                if (dt3.Rows.Count > 0)
                {
                    comboreason.DisplayMember = "REASON";
                    comboreason.ValueMember = "ASPTBLREASONMASID";
                    comboreason.DataSource = dt3;


                }
                string sel4 = " SELECT  C.ASPTBLREASONMASID,C.REASON  FROM  GTCOMPMAST A JOIN  asptblusermas B ON A.GTCOMPMASTID= B.COMPCODE   JOIN ASPTBLREASONMAS C ON C.COMPCODE=A.GTCOMPMASTID     WHERE C.ACTIVE='T'   AND A.COMPCODE='" + Class.Users.HCompcode + "'   AND B.USERNAME='" + Class.Users.HUserName + "'";
                DataSet ds4 = Utility.ExecuteSelectQuery(sel4, "ASPTBLREASONMAS");
                DataTable dt4 = ds4.Tables["ASPTBLREASONMAS"];
                if (dt4.Rows.Count > 0)
                {
                    comboBox1.DisplayMember = "REASON";
                    comboBox1.ValueMember = "ASPTBLREASONMASID";
                    comboBox1.DataSource = dt4;


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Data Source Not Connected" + ex.Message);
            }
        }

        private void HostelGatePass_Load(object sender, EventArgs e)
        {

            reason(); 

            frmdate.Text = DateTime.Now.ToShortDateString(); todate.Text = DateTime.Now.ToShortDateString();
            btnhostelsave.Focus();
            News();
        }


        private void LvLogs_ItemActivate(object sender, EventArgs e)
        {
            try
            {
                empty();
                if (lvLogs.Items.Count > 0)
                {

                     bytes = null;
                    txtempid.Text = Convert.ToString(lvLogs.SelectedItems[0].SubItems[1].Text);
                    DataTable dt = new DataTable();
                   
                        dt.Rows.Clear();
                    if (Class.Users.HostelName == "WORKING GENTS HOSTEL" || Class.Users.HostelName == "WOMENS HOSTEL" || Class.Users.HostelName == "GENTS STAFF HOSTEL")
                    {
                        string sel0 = "SELECT A.ASPTBLHOSTELGATEPASSID,B.COMPCODE, D.MIDCARD AS IDCARDNO,C.FNAME AS EMPNAME,E.MNNAME1 AS DEPARTMENT,F.HOSTELNAME,F.BLOCKFLOOR AS HOSTELBLOCK, F.ROOMNO AS HOSTELROOM,   B.PHONENO AS  CONTACTNO, A.MANUALTIME,G.REASON,A.PERMISSIONHRS,A.SYSTEMDATE,A.SYSTEMTIME,A.OUTTIME,A.INTIME,I.QRCODE, '' EMPIMAGE,A.REMARKS, A.IPADDRESS, A.IPADDRESS1,    A.NATIVE FROM ASPTBLHOSTELGATEPASS A  JOIN   GTCOMPMAST B ON B.GTCOMPMASTID = A.COMPCODE    JOIN   HREMPLOYMAST    C ON C.COMPCODE=B.GTCOMPMASTID AND C.COMPCODE=A.COMPCODE AND C.HREMPLOYMASTID=A.EMPNAME     JOIN HREMPLOYDETAILS D ON D.HREMPLOYMASTID=C.HREMPLOYMASTID AND D.IDCARD=C.IDCARDNO    AND  D.DEPTNAME=A.DEPARTMENT AND D.MIDCARD=A.IDCARDNO   JOIN GTDEPTDESGMAST  E ON E.GTDEPTDESGMASTID = A.DEPARTMENT  JOIN HOSTELLIVEDATA F ON F.COMPCODE=B.COMPCODE    AND F.IDCARDNO=C.IDCARDNO AND F.IDCARDNO=D.IDCARD   AND D.IDCARD=F.IDCARDNO   JOIN ASPTBLREASONMAS G ON G.ASPTBLREASONMASID=A.REASON      LEFT OUTER JOIN ASPTBLEMP I ON I.COMPCODE=B.GTCOMPMASTID AND I.IDCARDNO=A.IDCARDNO   WHERE     A.ASPTBLHOSTELGATEPASSID=" + txtempid.Text + "  ORDER BY A.ASPTBLHOSTELGATEPASSID desc";
                        DataSet ds0 = Utility.ExecuteSelectQuery(sel0, "ASPTBLHOSTELGATEPASS");
                        dt = ds0.Tables["ASPTBLHOSTELGATEPASS"];

                    }
                    else
                    {
                        string sel1 = "SELECT  A.ASPTBLHOSTELGATEPASSID,B.COMPCODE,D.MIDCARD AS IDCARDNO,C.FNAME AS EMPNAME,E.DISPNAME AS DEPARTMENT,B.PHONENO AS  CONTACTNO,B.COMPCODE AS HOSTELNAME,'' AS HOSTELBLOCK, '' AS HOSTELROOM,A.MANUALTIME,G.REASON,A.PERMISSIONHRS,A.SYSTEMDATE,A.SYSTEMTIME,A.OUTTIME,A.INTIME,I.QRCODE,'' EMPIMAGE,A.REMARKS, A.IPADDRESS, A.IPADDRESS1,    A.NATIVE FROM ASPTBLHOSTELGATEPASS A  JOIN   GTCOMPMAST B ON B.GTCOMPMASTID = A.COMPCODE      JOIN   HREMPLOYMAST    C ON C.COMPCODE=B.GTCOMPMASTID AND C.COMPCODE=A.COMPCODE     JOIN HREMPLOYDETAILS D ON D.HREMPLOYMASTID=C.HREMPLOYMASTID         AND D.IDCARD=C.IDCARDNO AND  D.DEPTNAME=A.DEPARTMENT AND D.MIDCARD=A.IDCARDNO       JOIN GTDEPTDESGMAST  E ON E.GTDEPTDESGMASTID = A.DEPARTMENT         JOIN ASPTBLREASONMAS G ON G.ASPTBLREASONMASID=A.REASON       LEFT OUTER JOIN ASPTBLEMP I ON I.COMPCODE=B.GTCOMPMASTID AND I.IDCARDNO=A.IDCARDNO  WHERE A.ASPTBLHOSTELGATEPASSID=" + txtempid.Text;
                        DataSet ds = Utility.ExecuteSelectQuery(sel1, "ASPTBLHOSTELGATEPASS");
                        dt = ds.Tables["ASPTBLHOSTELGATEPASS"];
                    }

                    txtempid.Text = Convert.ToString(dt.Rows[0]["ASPTBLHOSTELGATEPASSID"].ToString());
                    combo_compcode.Text = Convert.ToString(dt.Rows[0]["COMPCODE"].ToString());
                    txtidcardno.Text = Convert.ToString(dt.Rows[0]["IDCARDNO"].ToString());
               
                    comboempname.Text = Convert.ToString(dt.Rows[0]["EMPNAME"].ToString());
                    LBLNAME.Refresh();                   
                     LBLNAME.Text = "Name : " + Convert.ToString(dt.Rows[0]["EMPNAME"].ToString()) + ".----- Dept : " + Convert.ToString(dt.Rows[0]["DEPARTMENT"].ToString());
                    combo_dept.Text = Convert.ToString(dt.Rows[0]["DEPARTMENT"].ToString());
                    combohostel.Text = Convert.ToString(dt.Rows[0]["HOSTELNAME"].ToString());
                    combohostelblock.Text = Convert.ToString(dt.Rows[0]["HOSTELBLOCK"].ToString());
                    combohostelroom.Text = Convert.ToString(dt.Rows[0]["HOSTELROOM"].ToString());
                    txtmanualTime.Text = Convert.ToDateTime(dt.Rows[0]["MANUALTIME"].ToString()).ToString("HH:mm:ss");
                    comboreason.Text = Convert.ToString(dt.Rows[0]["REASON"].ToString());
                    txtpermissionhrs.Text = Convert.ToDateTime(dt.Rows[0]["PERMISSIONHRS"].ToString()).ToString("HH:mm:ss");
                    txtsysdate.Text = Convert.ToString(dt.Rows[0]["SYSTEMDATE"].ToString());
                    txtsystime.Text = Convert.ToString(dt.Rows[0]["SYSTEMTIME"].ToString());
                    txtoutime.Text = Convert.ToString(dt.Rows[0]["OUTTIME"].ToString());
                    if (Class.Users.IPADDRESS == "192.168.101.15") {
                        txtintime.Enabled = true;
                        txtintime.Text = Convert.ToString(dt.Rows[0]["INTIME"].ToString());
                    }
                    else
                    {
                        txtintime.Enabled = false;
                        txtintime.Text = Convert.ToString(dt.Rows[0]["INTIME"].ToString());
                    }
                    txtRemarks.Text = Convert.ToString(dt.Rows[0]["REMARKS"].ToString());
                    txtcontactno.Text= Convert.ToString(dt.Rows[0]["CONTACTNO"].ToString());
                    comboBox1.Text = Convert.ToString(dt.Rows[0]["REMARKS"].ToString());
                    QRCoder.QRCodeGenerator qc = new QRCoder.QRCodeGenerator();
                    var mydata = qc.CreateQrCode(txtempid.Text, QRCoder.QRCodeGenerator.ECCLevel.L);
                    var code = new QRCoder.QRCode(mydata);
                    qrbytes = Encoding.ASCII.GetBytes(txtempid.Text);
                    pictureBox1.Image = code.GetGraphic(50, Color.Black, Color.White, true);
                    if (dt.Rows[0]["EMPIMAGE"].ToString() != "")
                    {

                        bytes = (byte[])dt.Rows[0]["EMPIMAGE"];
                        Image img = Models.Device.ByteArrayToImage(bytes);
                        pictureempimage.Image = img;


                    }
                    else
                    {
                        pictureempimage.Image = Pinnacle.Properties.Resources.Anugraha_logo;
                    }
                    if (dt.Rows[0]["NATIVE"].ToString() == "T")
                    {
                        checknative.Checked = true;
                    }
                    else
                    {
                        checknative.Checked = false;
                    }
                    panelprint.Hide();
                   btnhostelsave.Enabled = false;
                   
                    comboreason.Enabled = false;
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

        private void Btnsaves_Click(object sender, EventArgs e)
        {

        }

        private void empty()
        {
            Class.Users.UserTime = 0; LBLNAME.Text = "";
            butheader.Text = " GATE PASS          " + System.DateTime.Now.ToString("MMMM") + "   - " + System.DateTime.Now.Year;
            Class.Users.Intimation = "PAYROLL";
            txtempid.Text = ""; pictureempimage.Image = Pinnacle.Properties.Resources.Anugraha_logo; pictureBox1.Image = null;

            txtcontactno.Text = "";txtidcardno.Text = ""; combo_compcode.Text = "";
            combohostel.Text = "";txtRemarks.Text = "";
            combohostelblock.Text = ""; btnhostelsave.Visible = false;
            combohostelroom.Text = "";
            comboempname.Text = "";
            combo_dept.Text = ""; txtdept.Text = ""; txthostelblock.Text = ""; txthostelroom.Text = "";

            txtsysdate.Text = "";
            txtsystime.Text = "";
            pictureBox1.Image = null;
            txtcompcode.Text = "";
            txtempname.Text = "";
            txtdept.Text = "";comboBox1.SelectedIndex = -1;comboBox1.Text = "";
            txthostelblock.Text = "";
            txthostelroom.Text = "";
            comboreason.Text = ""; comboreason.SelectedIndex = -1;
            txtmanualTime.Text = "";
            btnhostelsave.Enabled = true;
            txtintime.Text = ""; txtoutime.Text = ""; 
            comboreason.Enabled = true;
            txtmanualTime.Enabled = true; checknative.Checked = false;
            panel4.BackColor = Class.Users.BackColors;
            panel3.BackColor = Class.Users.BackColors;
            LBLNAME.BackColor = Class.Users.BackColors;
     
            txtpermissionhrs.Enabled = true; panelprint.Hide(); panelprint.Refresh(); butGetData.Visible = true;
            butheader.BackColor = Class.Users.BackColors;
           this.BackColor = Class.Users.BackColors;

            this.Font = Class.Users.FontName;
            lvLogs.Font = Class.Users.FontName;
            comboreason.Focus();

        }

        private void Butcancel_Click(object sender, EventArgs e)
        {
            panelprint.Hide();
        }

        public void Prints()
        {
            string sel1 = " SELECT MAX(A.ASPTBLHOSTELGATEPASSID) ID  FROM ASPTBLHOSTELGATEPASS A  ";
            DataSet ds = Utility.ExecuteSelectQuery(sel1, "ASPTBLHOSTELGATEPASS");
            DataTable dt = ds.Tables["ASPTBLHOSTELGATEPASS"];
            if (dt.Rows.Count > 0)
            {
                DataTable dt2 = new DataTable();
                string sel2 = "SELECT '" + System.DateTime.Now.Year + "' AS FINYEAR,H.DESIGNATION AS DESIGN,B.PHONENO  || ',' || B.FAXNO AS CONTACTAGF,A.ASPTBLHOSTELGATEPASSID AS TOKENNO ,B.COMPCODE,D.MIDCARD AS IDCARDNO,C.FNAME AS EMPNAME,E.MNNAME1 AS DEPARTMENT,A.CONTACTNO,F.HOSTELNAME,F.BLOCKFLOOR AS HOSTELBLOCK, F.ROOMNO AS HOSTELROOM,G.REASON,A.PERMISSIONHRS AS PERHRS,A.SYSTEMDATE ,A.QRCODE,'' EMPIMAGE  FROM ASPTBLHOSTELGATEPASS A  JOIN   GTCOMPMAST B ON B.GTCOMPMASTID = A.COMPCODE  JOIN   HREMPLOYMAST    C ON C.COMPCODE=B.GTCOMPMASTID AND C.COMPCODE=A.COMPCODE   JOIN HREMPLOYDETAILS D ON D.HREMPLOYMASTID=C.HREMPLOYMASTID AND D.IDCARD=C.IDCARDNO AND  D.DEPTNAME=A.DEPARTMENT AND D.MIDCARD=A.IDCARDNO  AND D.IDACTIVE='YES'   JOIN GTDEPTDESGMAST  E ON E.GTDEPTDESGMASTID = A.DEPARTMENT JOIN HOSTELLIVEDATA F ON F.COMPCODE=B.COMPCODE AND F.IDCARDNO=C.IDCARDNO AND F.IDCARDNO=D.IDCARD  AND D.IDCARD=F.IDCARDNO JOIN ASPTBLREASONMAS G ON G.ASPTBLREASONMASID=A.REASON     JOIN GTDESIGNATIONMAST H ON H.GTDESIGNATIONMASTID=D.DESIGNATION LEFT OUTER JOIN ASPTBLEMP I ON I.COMPCODE=B.GTCOMPMASTID AND I.IDCARDNO=A.IDCARDNO   WHERE   A.ASPTBLHOSTELGATEPASSID=" + Convert.ToInt64("0" + dt.Rows[0]["ID"].ToString());
                DataSet ds2 = Utility.ExecuteSelectQuery(sel2, "ASPTBLHOSTELGATEPASS");
                dt2 = ds2.Tables["ASPTBLHOSTELGATEPASS"];
                if (dt2.Rows.Count > 1)
                {
                    dt2.Rows.RemoveAt(0);
                }
                if (dt2.Rows.Count <= 0)
                {
                    string sel3 = "SELECT '" + System.DateTime.Now.Year + "' AS FINYEAR,H.DESIGNATION AS DESIGN,B.PHONENO  || ',' || B.FAXNO AS CONTACTAGF, A.ASPTBLHOSTELGATEPASSID  AS TOKENNO,B.COMPCODE,D.MIDCARD AS IDCARDNO,C.FNAME AS EMPNAME,E.MNNAME1 AS DEPARTMENT,A.CONTACTNO ,G.REASON,A.PERMISSIONHRS,A.SYSTEMDATE,A.QRCODE,'' EMPIMAGE   FROM ASPTBLHOSTELGATEPASS A   JOIN   GTCOMPMAST B ON B.GTCOMPMASTID = A.COMPCODE  JOIN   HREMPLOYMAST    C ON C.COMPCODE=B.GTCOMPMASTID AND C.COMPCODE=A.COMPCODE    JOIN HREMPLOYDETAILS D ON D.HREMPLOYMASTID=C.HREMPLOYMASTID AND D.IDCARD=C.IDCARDNO AND  D.DEPTNAME=A.DEPARTMENT AND D.MIDCARD=A.IDCARDNO   AND IDACTIVE='YES'   JOIN GTDEPTDESGMAST  E ON E.GTDEPTDESGMASTID = A.DEPARTMENT  JOIN ASPTBLREASONMAS G ON G.ASPTBLREASONMASID=A.REASON    JOIN GTDESIGNATIONMAST H ON H.GTDESIGNATIONMASTID=D.DESIGNATION  LEFT OUTER JOIN ASPTBLEMP I ON I.COMPCODE=B.GTCOMPMASTID AND I.IDCARDNO=A.IDCARDNO     WHERE   A.ASPTBLHOSTELGATEPASSID=" + Convert.ToInt64("0" + dt.Rows[0]["ID"].ToString());
                    DataSet ds3 = Utility.ExecuteSelectQuery(sel3, "ASPTBLHOSTELGATEPASS");
                    dt2 = ds3.Tables["ASPTBLHOSTELGATEPASS"];
                    if (dt2.Rows.Count > 1)
                    {
                        dt2.Rows.RemoveAt(0);
                    }
                }
                string IDD = "TOKENNO: " + Convert.ToString(dt2.Rows[0]["TOKENNO"].ToString()) + ",\nIDCARD : " + Convert.ToString(dt2.Rows[0]["IDCARDNO"].ToString()) + ",\nNAME   : " + Convert.ToString(dt2.Rows[0]["EMPNAME"].ToString());
                PictureBox picturegrcode = new PictureBox();
                MemoryStream stream = new MemoryStream();
                QRCoder.QRCodeGenerator qc = new QRCoder.QRCodeGenerator();
                var mydata1 = qc.CreateQrCode(IDD, QRCoder.QRCodeGenerator.ECCLevel.L);
                var code1 = new QRCoder.QRCode(mydata1);
                picturegrcode.Image = code1.GetGraphic(50, Color.Black, Color.White, true);
                picturegrcode.Image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                qrbytes = stream.ToArray();
                dt2.Rows[0]["QRCODE"] = qrbytes;
                crystalReportViewer1.ReportSource = null;
                CrystalDecisions.CrystalReports.Engine.ReportDocument reportdocument = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                if (Class.Users.HostelName == "WORKING GENTS HOSTEL" || Class.Users.HostelName == "WOMENS HOSTEL" || Class.Users.HostelName == "GENTS STAFF HOSTEL")
                {
                    reportdocument.Load(Application.StartupPath + "\\Report\\AGF\\HostelReport.rpt");
                    reportdocument.Database.Tables["DataTable1"].SetDataSource(dt2);
                }
                else
                {
                    reportdocument.Load(Application.StartupPath + "\\Report\\AGF\\OutPassReport.rpt");
                    reportdocument.Database.Tables["DataTable1"].SetDataSource(dt2);
                }
                if (Class.Users.HUserName == "HR")
                {
                    printDialog1 = new PrintDialog();
                    printDialog1.AllowSelection = true;
                    printDialog1.AllowSomePages = true;

                    if (printDialog1.ShowDialog() == DialogResult.OK)
                    {

                        crystalReportViewer1.ReportSource = reportdocument;
                        crystalReportViewer1.Refresh();
                        reportdocument.PrintOptions.PrinterName = printDialog1.PrinterSettings.PrinterName;
                        reportdocument.PrintToPrinter(printDialog1.PrinterSettings.Copies, printDialog1.PrinterSettings.Collate, printDialog1.PrinterSettings.FromPage, printDialog1.PrinterSettings.ToPage);
                        panelprint.Hide();
                        panelprint.Refresh();
                        btnhostelsave.Focus();
                    }
                }
                else
                {
                    crystalReportViewer1.ReportSource = reportdocument;
                    crystalReportViewer1.Refresh();
                    reportdocument.PrintOptions.PrinterName = printDialog1.PrinterSettings.PrinterName;
                    reportdocument.PrintToPrinter(printDialog1.PrinterSettings.Copies, printDialog1.PrinterSettings.Collate, printDialog1.PrinterSettings.FromPage, printDialog1.PrinterSettings.ToPage);
                    panelprint.Hide();
                    panelprint.Refresh();
                    btnhostelsave.Focus();
                }
            }

            panelprint.Visible = false;
            butprint1.Focus();
            btnhostelsave.Visible = false;
            butGetData.Visible = true; Class.Users.UserTime = 0;
        }
        private void Btnhostelsave_ClickAsync(object sender, EventArgs e)
        {
            try
            {
                if (comboreason.Text != "" && txtRemarks.Text != "")
                {

                    Class.Users.UserTime = 0; Class.Users.Intimation = "PAYROLL";
                    if (comboreason.Text == "PERSONAL" && txtRemarks.Text != null)
                    {
                        string sel0 = "SELECT count(*) cnt,a.idcardno,H.FNAME FROM ASPTBLHOSTELGATEPASS A  JOIN ASPTBLREASONMAS G ON G.ASPTBLREASONMASID=A.REASON   JOIN HREMPLOYMAST H ON H.HREMPLOYMASTID=A.EMPNAME  WHERE A.IDCARDNO='" + txtidcardno.Text + "'  AND A.FINYEAR='" + System.DateTime.Now.Year + "' AND A.MONTH='" + System.DateTime.Now.ToString("MMMM") + "'  and G.reason='PERSONAL' GROUP BY A.IDCARDNO,H.FNAME";
                        DataSet ds0 = Utility.ExecuteSelectQuery(sel0, "HREMPLOYDETAILS");
                        DataTable dt0 = ds0.Tables["HREMPLOYDETAILS"];
                        if (dt0.Rows.Count > 0)
                        {
                            
                                Cursor.Current = Cursors.Default;
                           
                                lvLogs1.Items.Clear();
                                Cursor = Cursors.Default;
                                
                                    int idwErrorCode = 0;

                            int iDataFlag = 1;
                            if (axCZKEM1.ClearData(iMachineNumber, iDataFlag))
                            {
                                axCZKEM1.RefreshData(iMachineNumber);//the data in the device should be refreshed
                            }
                            else
                            {
                                axCZKEM1.GetLastError(ref idwErrorCode);
                            }
                            axCZKEM1.EnableDevice(iMachineNumber, true);//enable the device
                            bIsConnected = false;



                        }
                    }

                    if (txtidcardno.Text != "" && txtRemarks.Text != null && comboreason.Text != "")
                    {
                        panelprint.Hide();
                        panelprint.Refresh(); string rem = "";
                        Cursor.Current = Cursors.WaitCursor;
                        if (Convert.ToInt32("0" + comboreason.SelectedValue) >= 1 && txtpermissionhrs.Text != "" && txtmanualTime.Text != "")
                        {
                            string native = "";
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
                            string ins = "INSERT INTO ASPTBLHOSTELGATEPASS(COMPCODE,  IDCARDNO,  EMPNAME ,  DEPARTMENT,  HOSTELNAME, HOSTELBLOCK,HOSTELROOM,HOSTELBLOCK1,HOSTELROOM1,CONTACTNO, SYSTEMDATE,  SYSTEMTIME ,REASON,MANUALTIME,USERNAME,  MODIFIED,  CREATEDON,  IPADDRESS1,IPADDRESS,PERMISSIONHRS,Remarks,NATIVE,FINYEAR,MONTH)VALUES(" + txtcompcode.Text + ",'" + txtidcardno.Text + "','" + txtempname.Text + "','" + txtdept.Text + "' ,'" + combohostel.Text + "' ,'" + txtidcardno.Text + "' ,'" + txtidcardno.Text + "' ,'" + combohostelblock.Text + "','" + combohostelroom.Text + "' ,'" + txtcontactno.Text + "','" + txtsysdate.Text + "','" + txtsystime.Text + "','" + comboreason.SelectedValue + "' ,'" + Convert.ToDateTime(txtmanualTime.Text) + "'," + Class.Users.USERID + ",to_date('" + Convert.ToDateTime(modified).ToString() + "', 'dd/MM/yyyy hh24:MI:SS'),to_date('" + Convert.ToDateTime(CreatedOn.ToString()) + "', 'dd/MM/yyyy hh24:MI:SS'),'" + txtipaddress.Text + "','" + Class.Users.IPADDRESS + "', '" + Convert.ToDateTime(txtpermissionhrs.Text) + "','" + txtRemarks.Text + "','" + native + "','" + System.DateTime.Now.Year + "','" + System.DateTime.Now.Date.ToString("MMMM") + "')";
                            Utility.ExecuteNonQuery(ins);

                           
                            Prints();
                                Butview_Click(sender, e);
                              
                            

                            Cursor = Cursors.Default;
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
                        MessageBox.Show("Pls Enter Remarks Fields", "Invalid");
                    }
                }
                else
                {
                    MessageBox.Show("PermissionType and Remarks Field is Empty  ", " Gate Pass ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return;
                    txtRemarks.Select();
                }
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                MessageBox.Show("Gate Pass Cancelled    " + ex.Message.ToString() + "", " Gate Pass ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return;
                Cursor = Cursors.Default;
            }
           
        }

        private void Txthostelgatesearch_TextChanged(object sender, EventArgs e)
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

        private void MenuRefreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bIsConnected = false;
        }

        private void ListViewRefreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Butview_Click(sender, e);
        }

        private void Pictureempimage_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    bytes = null;
            //    PictureBox p = sender as PictureBox;
            //    if (p != null)
            //    {


            //            p.Image = new Bitmap(pictureempimage.Image);
            //            bytes = Models.Device.ImageToByteArray(p);


            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}
        }
        public void GridLoad()
        {
            try
            {
                listfilter.Items.Clear(); lvLogs.Items.Clear();
                iGLCount = 1;
      
               
                DataTable dt = new DataTable();
               // if (lvLogs.Columns[5].Text == "HostelName") { lvLogs.Columns[5].Text == "CompCode" }
                if (Class.Users.HostelName == "WOMENS HOSTEL" || Class.Users.HostelName == "WORKING GENTS HOSTEL" || Class.Users.HostelName == "GENTS STAFF HOSTEL")
                {
                    string sel1 = "SELECT DISTINCT A.ASPTBLHOSTELGATEPASSID,D.MIDCARD AS IDCARDNO,C.FNAME AS EMPNAME,E.MNNAME1 AS DEPARTMENT,F.HOSTELNAME,F.BLOCKFLOOR AS HOSTELBLOCK, F.ROOMNO AS HOSTELROOM,   B.PHONENO AS  CONTACTNO,substr(A.OUTTIME,11,18) outtime,substr(A.INTIME,11,18) intime   FROM ASPTBLHOSTELGATEPASS A  JOIN   GTCOMPMAST B ON B.GTCOMPMASTID = A.COMPCODE    JOIN   HREMPLOYMAST    C ON C.COMPCODE=B.GTCOMPMASTID AND C.COMPCODE=A.COMPCODE AND C.HREMPLOYMASTID=A.EMPNAME     JOIN HREMPLOYDETAILS D ON D.HREMPLOYMASTID=C.HREMPLOYMASTID AND D.IDCARD=C.IDCARDNO    AND  D.DEPTNAME=A.DEPARTMENT AND D.MIDCARD=A.IDCARDNO   JOIN GTDEPTDESGMAST  E ON E.GTDEPTDESGMASTID = A.DEPARTMENT  JOIN HOSTELLIVEDATA F ON F.COMPCODE=B.COMPCODE    AND F.IDCARDNO=C.IDCARDNO AND F.IDCARDNO=D.IDCARD   AND D.IDCARD=F.IDCARDNO   JOIN ASPTBLREASONMAS G ON G.ASPTBLREASONMASID=A.REASON           WHERE  A.HOSTELNAME='" + Class.Users.HostelName + "'  AND A.MODIFIED= TO_DATE('" + frmdate.Value.ToString("dd-MM-yyyy") + "', 'dd-MM-yyyy') ORDER BY A.ASPTBLHOSTELGATEPASSID DESC";
                    DataSet ds = Utility.ExecuteSelectQuery(sel1, "ASPTBLHOSTELGATEPASS");
                    dt = ds.Tables["ASPTBLHOSTELGATEPASS"]; lvLogs.Columns[5].Text = "HostelName";
                   
                }
                //if (Class.Users.HostelName == "ALL")
                //{
                //    string sel1 = "SELECT DISTINCT A.ASPTBLHOSTELGATEPASSID,D.MIDCARD AS IDCARDNO,C.FNAME AS EMPNAME,E.MNNAME1 AS DEPARTMENT,F.HOSTELNAME,F.BLOCKFLOOR AS HOSTELBLOCK, F.ROOMNO AS HOSTELROOM,   B.PHONENO AS  CONTACTNO,substr(A.OUTTIME,11,18) outtime,substr(A.INTIME,11,18) intime   FROM ASPTBLHOSTELGATEPASS A  JOIN   GTCOMPMAST B ON B.GTCOMPMASTID = A.COMPCODE    JOIN   HREMPLOYMAST    C ON C.COMPCODE=B.GTCOMPMASTID AND C.COMPCODE=A.COMPCODE AND C.HREMPLOYMASTID=A.EMPNAME     JOIN HREMPLOYDETAILS D ON D.HREMPLOYMASTID=C.HREMPLOYMASTID AND D.IDCARD=C.IDCARDNO    AND  D.DEPTNAME=A.DEPARTMENT AND D.MIDCARD=A.IDCARDNO   JOIN GTDEPTDESGMAST  E ON E.GTDEPTDESGMASTID = A.DEPARTMENT  JOIN HOSTELLIVEDATA F ON F.COMPCODE=B.COMPCODE    AND F.IDCARDNO=C.IDCARDNO AND F.IDCARDNO=D.IDCARD   AND D.IDCARD=F.IDCARDNO   JOIN ASPTBLREASONMAS G ON G.ASPTBLREASONMASID=A.REASON           WHERE  A.MODIFIED= TO_DATE('" + frmdate.Value.ToString("dd-MM-yyyy") + "', 'dd-MM-yyyy') ORDER BY A.ASPTBLHOSTELGATEPASSID DESC";
                //    DataSet ds = Utility.ExecuteSelectQuery(sel1, "ASPTBLHOSTELGATEPASS");
                //    dt = ds.Tables["ASPTBLHOSTELGATEPASS"]; lvLogs.Columns[5].Text = "HostelName";
                   
                //}
                if (Class.Users.HostelName == "AGF" || Class.Users.HostelName == "AGFMGII" || Class.Users.HostelName == "AGFM" || Class.Users.HostelName == "AGFP" || Class.Users.HostelName == "AGFC" || Class.Users.HostelName == "AGFK" || Class.Users.HostelName == "FLF" || Class.Users.HostelName == "FLFD")
                {                  

                    string sel0 = "SELECT DISTINCT A.ASPTBLHOSTELGATEPASSID,D.MIDCARD AS IDCARDNO,C.FNAME AS EMPNAME,E.MNNAME1 AS DEPARTMENT, B.COMPCODE as HOSTELNAME, '' HOSTELBLOCK,'' AS HOSTELROOM,   '' CONTACTNO,substr(A.OUTTIME,11,18) outtime,substr(A.INTIME,11,18) intime   FROM ASPTBLHOSTELGATEPASS A  JOIN   GTCOMPMAST B ON B.GTCOMPMASTID = A.COMPCODE     JOIN   HREMPLOYMAST    C ON C.COMPCODE=B.GTCOMPMASTID AND C.COMPCODE=A.COMPCODE   JOIN HREMPLOYDETAILS D ON D.HREMPLOYMASTID=C.HREMPLOYMASTID AND D.IDCARD=C.IDCARDNO    AND  D.DEPTNAME=A.DEPARTMENT AND D.MIDCARD=A.IDCARDNO   JOIN GTDEPTDESGMAST  E ON E.GTDEPTDESGMASTID = A.DEPARTMENT       JOIN ASPTBLREASONMAS G ON G.ASPTBLREASONMASID=A.REASON      WHERE  A.HOSTELNAME='" + Class.Users.HostelName + "'  AND A.MODIFIED= TO_DATE('" + frmdate.Value.ToString("dd-MM-yyyy") + "', 'dd-MM-yyyy')  ORDER BY A.ASPTBLHOSTELGATEPASSID DESC";
                    DataSet ds1 = Utility.ExecuteSelectQuery(sel0, "ASPTBLHOSTELGATEPASS");
                    dt = ds1.Tables["ASPTBLHOSTELGATEPASS"]; lvLogs.Columns[5].Text = "CompCode";
                   
                }
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
        private void Butview_Click(object sender, EventArgs e)
        {
            try
            {
                listfilter.Items.Clear(); lvLogs.Items.Clear();
                iGLCount = 1;
                DataTable dt = new DataTable();

                if (Class.Users.HostelName == "WOMENS HOSTEL" || Class.Users.HostelName == "WORKING GENTS HOSTEL" || Class.Users.HostelName == "GENTS STAFF HOSTEL")
                {
                    string sel1 = "SELECT DISTINCT A.ASPTBLHOSTELGATEPASSID,D.MIDCARD AS IDCARDNO,C.FNAME AS EMPNAME,E.MNNAME1 AS DEPARTMENT,F.HOSTELNAME,F.BLOCKFLOOR AS HOSTELBLOCK, F.ROOMNO AS HOSTELROOM,   B.PHONENO AS  CONTACTNO,substr(A.OUTTIME,11,18) outtime,substr(A.INTIME,11,18) intime   FROM ASPTBLHOSTELGATEPASS A  JOIN   GTCOMPMAST B ON B.GTCOMPMASTID = A.COMPCODE    JOIN   HREMPLOYMAST    C ON C.COMPCODE=B.GTCOMPMASTID AND C.COMPCODE=A.COMPCODE AND C.HREMPLOYMASTID=A.EMPNAME     JOIN HREMPLOYDETAILS D ON D.HREMPLOYMASTID=C.HREMPLOYMASTID AND D.IDCARD=C.IDCARDNO    AND  D.DEPTNAME=A.DEPARTMENT AND D.MIDCARD=A.IDCARDNO   JOIN GTDEPTDESGMAST  E ON E.GTDEPTDESGMASTID = A.DEPARTMENT  JOIN HOSTELLIVEDATA F ON F.COMPCODE=B.COMPCODE    AND F.IDCARDNO=C.IDCARDNO AND F.IDCARDNO=D.IDCARD   AND D.IDCARD=F.IDCARDNO   JOIN ASPTBLREASONMAS G ON G.ASPTBLREASONMASID=A.REASON    WHERE  A.HOSTELNAME='" + Class.Users.HostelName + "'  AND  A.MODIFIED= TO_DATE('" + frmdate.Value.ToString("dd-MM-yyyy") + "', 'dd-MM-yyyy') ORDER BY A.ASPTBLHOSTELGATEPASSID DESC";
                    DataSet ds = Utility.ExecuteSelectQuery(sel1, "ASPTBLHOSTELGATEPASS");
                    dt = ds.Tables["ASPTBLHOSTELGATEPASS"];
                }
                //if (Class.Users.HostelName == "ALL")
                //{
                //    string sel1 = "SELECT DISTINCT A.ASPTBLHOSTELGATEPASSID,D.MIDCARD AS IDCARDNO,C.FNAME AS EMPNAME,E.MNNAME1 AS DEPARTMENT,F.HOSTELNAME,F.BLOCKFLOOR AS HOSTELBLOCK, F.ROOMNO AS HOSTELROOM,   B.PHONENO AS  CONTACTNO,substr(A.OUTTIME,11,18) outtime,substr(A.INTIME,11,18) intime   FROM ASPTBLHOSTELGATEPASS A  JOIN   GTCOMPMAST B ON B.GTCOMPMASTID = A.COMPCODE    JOIN   HREMPLOYMAST    C ON C.COMPCODE=B.GTCOMPMASTID AND C.COMPCODE=A.COMPCODE AND C.HREMPLOYMASTID=A.EMPNAME     JOIN HREMPLOYDETAILS D ON D.HREMPLOYMASTID=C.HREMPLOYMASTID AND D.IDCARD=C.IDCARDNO    AND  D.DEPTNAME=A.DEPARTMENT AND D.MIDCARD=A.IDCARDNO   JOIN GTDEPTDESGMAST  E ON E.GTDEPTDESGMASTID = A.DEPARTMENT  JOIN HOSTELLIVEDATA F ON F.COMPCODE=B.COMPCODE    AND F.IDCARDNO=C.IDCARDNO AND F.IDCARDNO=D.IDCARD   AND D.IDCARD=F.IDCARDNO   JOIN ASPTBLREASONMAS G ON G.ASPTBLREASONMASID=A.REASON  WHERE  A.MODIFIED= TO_DATE('" + frmdate.Value.ToString("dd-MM-yyyy") + "', 'dd-MM-yyyy') ORDER BY A.ASPTBLHOSTELGATEPASSID DESC";
                //    DataSet ds = Utility.ExecuteSelectQuery(sel1, "ASPTBLHOSTELGATEPASS");
                //    dt = ds.Tables["ASPTBLHOSTELGATEPASS"];
                //}
                if (Class.Users.HostelName == "AGF" || Class.Users.HostelName == "AGFMGII" || Class.Users.HostelName == "AGFM" || Class.Users.HostelName == "AGFP" || Class.Users.HostelName == "AGFC" || Class.Users.HostelName == "AGFK" || Class.Users.HostelName == "VEL" || Class.Users.HostelName == "FLFD")
                {
                  
                   string sel0 = "SELECT DISTINCT A.ASPTBLHOSTELGATEPASSID,D.MIDCARD AS IDCARDNO,C.FNAME AS EMPNAME,E.MNNAME1 AS DEPARTMENT, B.COMPCODE as HOSTELNAME, '' HOSTELBLOCK,'' AS HOSTELROOM,   '' CONTACTNO,substr(A.OUTTIME,11,18) outtime,substr(A.INTIME,11,18) intime   FROM ASPTBLHOSTELGATEPASS A  JOIN   GTCOMPMAST B ON B.GTCOMPMASTID = A.COMPCODE     JOIN   HREMPLOYMAST    C ON C.COMPCODE=B.GTCOMPMASTID AND C.COMPCODE=A.COMPCODE   JOIN HREMPLOYDETAILS D ON D.HREMPLOYMASTID=C.HREMPLOYMASTID AND D.IDCARD=C.IDCARDNO    AND  D.DEPTNAME=A.DEPARTMENT AND D.MIDCARD=A.IDCARDNO   JOIN GTDEPTDESGMAST  E ON E.GTDEPTDESGMASTID = A.DEPARTMENT       JOIN ASPTBLREASONMAS G ON G.ASPTBLREASONMASID=A.REASON      WHERE  A.HOSTELNAME='" + Class.Users.HostelName + "'  AND A.MODIFIED= TO_DATE('" + frmdate.Value.ToString("dd-MM-yyyy") + "', 'dd-MM-yyyy')  ORDER BY A.ASPTBLHOSTELGATEPASSID DESC";
                    DataSet ds1 = Utility.ExecuteSelectQuery(sel0, "ASPTBLHOSTELGATEPASS");
                    dt = ds1.Tables["ASPTBLHOSTELGATEPASS"];
                }
                if (dt.Rows.Count > 0)
                {
                    lvLogs.Columns[5].Text = "CompCode";
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
            finally
            {
                empty();
            }
        }
        string maip = "";
        private void ViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string ccode = "";

            int idwErrorCode = 0; maip = "";
            int iDataFlag = 1;
            try
            {
                Class.Users.UserTime = 0; iMachineNumber = 1;
                ccode = Class.Users.HCompcode;
                lvLogs1.Items.Clear();
                int k = 0;
                iIndex = 0;
                iGLCount = 0;
                string ip = "";
                txtipaddress.Text = "";
                DataTable dt = new DataTable();
                dt = Utility.SQLQuery("SELECT DISTINCT A.ASPTBLMACIPID , A.MACIP     FROM  ASPTBLMACIP   A  JOIN ASPTBLMACHINEMAS B ON B.IPADDRESS=A.ASPTBLMACIPID        AND B.ACTIVE='T'      JOIN ASPTBLUSERMAS C ON  B.WARDENNAME=C.USERID      JOIN  GTCOMPMAST D ON D.GTCOMPMASTID=C.COMPCODE  AND B.COMPCODE=D.GTCOMPMASTID WHERE D.COMPCODE = '" + Class.Users.HCompcode + "' AND C.USERNAME = '" + Class.Users.HUserName + "' AND B.MTYPE2='OUTPASS' AND A.ACTIVE='T'");//UNION SELECT DISTINCT B.HRMACIPENTRYDETID, B.MACIP  FROM ASPTBLMACHINEMAS A JOIN HRMACIPENTRYDET B ON B.HRMACIPENTRYDETID = A.IPADDRESS JOIN asptblusermas C ON C.userid = A.WARDENNAME JOIN GTCOMPMAST E ON   E.GTCOMPMASTID = A.COMPCODE AND E.GTCOMPMASTID = C.COMPCODE   AND B.CURMAC='YES' WHERE  A.ACTIVE = 'T' AND E.COMPCODE = '" + Class.Users.HCompcode + "' AND C.USERNAME = '" + Class.Users.HUserName + "'
                int maxip = dt.Rows.Count;
                if (maxip == 0)
                {
                    MessageBox.Show("IP Address not assign this User.   : " + Class.Users.HUserName);
                    Cursor.Current = Cursors.Default;
                }
                if (maxip == 1)
                {
                    int i = 0;

                    if (bIsConnected == false)
                    {
                        dt = Utility.SQLQuery("SELECT DISTINCT A.ASPTBLMACIPID , A.MACIP     FROM  ASPTBLMACIP   A  JOIN ASPTBLMACHINEMAS B ON B.IPADDRESS=A.ASPTBLMACIPID        AND B.ACTIVE='T'      JOIN ASPTBLUSERMAS C ON  B.WARDENNAME=C.USERID      JOIN  GTCOMPMAST D ON D.GTCOMPMASTID=C.COMPCODE  AND B.COMPCODE=D.GTCOMPMASTID WHERE D.COMPCODE = '" + Class.Users.HCompcode + "' AND C.USERNAME = '" + Class.Users.HUserName + "' AND B.MTYPE2='OUTPASS'");//UNION SELECT DISTINCT B.HRMACIPENTRYDETID, B.MACIP  FROM ASPTBLMACHINEMAS A JOIN HRMACIPENTRYDET B ON B.HRMACIPENTRYDETID = A.IPADDRESS JOIN asptblusermas C ON C.userid = A.WARDENNAME JOIN GTCOMPMAST E ON   E.GTCOMPMASTID = A.COMPCODE AND E.GTCOMPMASTID = C.COMPCODE   AND B.CURMAC='YES' WHERE  A.ACTIVE = 'T' AND E.COMPCODE = '" + Class.Users.HCompcode + "' AND C.USERNAME = '" + Class.Users.HUserName + "'
                        Class.Users.UserTime = 0; bIsConnected = false;
                        maip = dt.Rows[0]["MACIP"].ToString();
                        bIsConnected = axCZKEM1.Connect_Net(dt.Rows[0]["MACIP"].ToString(), Convert.ToInt32(4370));
                        label7.Refresh();
                        label7.Text = dt.Rows[i]["MACIP"].ToString();
                    }
                    if (bIsConnected == true)
                    {
                        ip = dt.Rows[0]["MACIP"].ToString();
                        lblip.Refresh(); lblip.Text = "Now Connected IP : " + dt.Rows[0]["MACIP"].ToString();
                        txtipaddress.Text = dt.Rows[0]["ASPTBLMACIPID"].ToString();
                        lvLogs1.Items.Clear();
                        if (axCZKEM1.ReadGeneralLogData(iMachineNumber))//read all the attendance records to the memory
                        {
                            while (axCZKEM1.SSR_GetGeneralLogData(iMachineNumber, out sdwEnrollNumber, out idwVerifyMode, out idwInOutMode, out idwYear, out idwMonth, out idwDay, out idwHour, out idwMinute, out idwSecond, ref idwWorkcode))//get records from the memory
                            {
                                DateTime inputDate = new DateTime(idwYear, idwMonth, idwDay);
                                if (Convert.ToDateTime(inputDate) >= frmdate.Value.Date)//&& Convert.ToDateTime(inputDate) <= todate.Value.Date.AddDays(1).AddTicks(-1)
                                {

                                    iGLCount++;
                                    lvLogs1.Items.Add(iGLCount.ToString());
                                    lvLogs1.Items[iIndex].SubItems.Add(sdwEnrollNumber);
                                    iIndex++;
                                }
                            }
                        }
                        else
                        {
                            Cursor.Current = Cursors.Default; butGetData.Visible = true; btnhostelsave.Visible = false; lblip.Text = "";
                            MessageBox.Show("No Data Found this Machine...." + ip.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        axCZKEM1.EnableDevice(iMachineNumber, true);//enable the device    
                        if (axCZKEM1.ClearData(iMachineNumber, iDataFlag))
                        {
                            axCZKEM1.RefreshData(iMachineNumber);//the data in the device should be refreshed
                        }
                        else
                        {
                            axCZKEM1.GetLastError(ref idwErrorCode);
                        }
                        axCZKEM1.EnableDevice(iMachineNumber, true);//enable the device
                    }
                    else
                    {
                        axCZKEM1.GetLastError(ref idwErrorCode); Class.Users.Bisconnectclear = false;
                        Cursor.Current = Cursors.Default; butGetData.Visible = true; btnhostelsave.Visible = false;
                        MessageBox.Show("Unable to connect the device , ErrorCode=" + idwErrorCode.ToString() + "---IP-----" + dt.Rows[i]["MACIP"].ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        return;
                    }

                }
                else
                {
                    Class.Users.Bisconnectclear = false;
                    MessageBox.Show("Only one IPAddress assign this User.pls check Machine Master   : " + Class.Users.HUserName);
                    return;
                }
            }
            catch (Exception ex)
            {
                butGetData.Visible = true; btnhostelsave.Visible = false;
                Class.Users.Bisconnectclear = false;
                MessageBox.Show(ex.Message.ToString());

            }

            try
            {
                empty();

                if (lvLogs1.Items.Count >= 1)
                {

                    var idd = lvLogs1.Items[lvLogs1.Items.Count - 1].SubItems[1].Text;

                    bytes = null; string master = "";
                    //pictureempimage.Image = null;
                    DataTable dt = new DataTable();


                    if (Class.Users.HostelName == "WORKING GENTS HOSTEL" || Class.Users.HostelName == "WOMENS HOSTEL" || Class.Users.HostelName == "GENTS STAFF HOSTEL")
                    {

                        string sel1 = "SELECT '" + System.DateTime.Now.Year + "' AS FINYEAR,C.HREMPLOYMASTID AS ASPTBLEMPID, B.COMPCODE ,B.GTCOMPMASTID,D.MIDCARD,C.FNAME,E.MNNAME1 as DISPNAME,E.GTDEPTDESGMASTID ,A.HOSTELNAME,A.BLOCKFLOOR,A.ROOMNO,A.IDCARDNO ,b.phoneno || b.faxno as CONTACTNO FROM HOSTELLIVEDATA A JOIN GTCOMPMAST B ON B.COMPCODE= A.COMPCODE   JOIN HREMPLOYMAST C ON C.COMPCODE = B.GTCOMPMASTID AND C.IDCARDNO = A.IDCARDNO   JOIN HREMPLOYDETAILS D ON C.HREMPLOYMASTID = D.HREMPLOYMASTID AND D.HOSTEL='YES' AND D.IDACTIVE='YES'   JOIN GTDEPTDESGMAST E ON E.GTDEPTDESGMASTID = D.DEPTNAME   WHERE D.MIDCARD=" + idd.ToString();
                        DataSet ds = Utility.ExecuteSelectQuery(sel1, "HOSTELLIVEDATA");
                        dt = ds.Tables["HOSTELLIVEDATA"];
                        if (dt.Rows.Count > 1)
                        {
                            dt.Rows.RemoveAt(0);
                        }

                        master = "Hostel Master";
                    }
                    else
                    {

                        string sel2 = "SELECT '" + System.DateTime.Now.Year + "' AS FINYEAR,C.HREMPLOYMASTID AS ASPTBLEMPID, B.COMPCODE ,B.GTCOMPMASTID,D.MIDCARD,C.FNAME,E.MNNAME1 as DISPNAME,E.GTDEPTDESGMASTID,'" + Class.Users.HCompcode + "' as HOSTELNAME,'0' as BLOCKFLOOR,'0' AS ROOMNO,C.IDCARDNO,B.PHONENO || B.FAXNO AS CONTACTNO  FROM  GTCOMPMAST B JOIN  HREMPLOYMAST C ON C.COMPCODE = B.GTCOMPMASTID    JOIN HREMPLOYDETAILS D ON C.HREMPLOYMASTID = D.HREMPLOYMASTID   JOIN GTDEPTDESGMAST E ON E.GTDEPTDESGMASTID = D.DEPTNAME  AND D.IDACTIVE='YES'   WHERE D.MIDCARD='" + idd.ToString() + "'   ORDER BY  C.IDCARDNO DESC ";
                        DataSet ds2 = Utility.ExecuteSelectQuery(sel2, "HREMPLOYDETAILS");
                        dt = ds2.Tables["HREMPLOYDETAILS"];
                        if (dt.Rows.Count > 1)
                        {
                            dt.Rows.RemoveAt(0);
                        }
                        master = "";
                        master = "Employee Master";
                    }

                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("This IDCardno  '" + idd + "' is empty in ( HR PAYROLL ) '" + master + "'", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {

                        txtempid.Text = Convert.ToString(dt.Rows[0]["ASPTBLEMPID"].ToString());
                        txtcompcode.Text = Convert.ToString(dt.Rows[0]["GTCOMPMASTID"].ToString());
                        combo_compcode.Text = dt.Rows[0]["COMPCODE"].ToString();                       
                        txtidcardno.Text = Convert.ToString(dt.Rows[0]["MIDCARD"].ToString());
                        comboempname.Text = Convert.ToString(dt.Rows[0]["FNAME"].ToString());
                        LBLNAME.Refresh();
                        LBLNAME.Text =" Name :" + Convert.ToString(dt.Rows[0]["FNAME"].ToString()) + ". -----   Dept : "+ Convert.ToString(dt.Rows[0]["DISPNAME"].ToString()); 
                        txtempname.Text = Convert.ToString(dt.Rows[0]["ASPTBLEMPID"].ToString());
                        combo_dept.Text = Convert.ToString(dt.Rows[0]["DISPNAME"].ToString());
                        txtdept.Text = Convert.ToString(dt.Rows[0]["GTDEPTDESGMASTID"].ToString());
                        txthostelroom.Text = Convert.ToString(dt.Rows[0]["ROOMNO"].ToString());
                        combohostel.Text = Convert.ToString(dt.Rows[0]["HOSTELNAME"].ToString());
                        combohostelroom.Text = Convert.ToString(dt.Rows[0]["ROOMNO"].ToString());
                        txthostelblock.Text = Convert.ToString(dt.Rows[0]["BLOCKFLOOR"].ToString());
                        combohostelblock.Text = Convert.ToString(dt.Rows[0]["BLOCKFLOOR"].ToString());
                        txtcontactno.Text = Convert.ToString(dt.Rows[0]["CONTACTNO"].ToString());
                        txtsysdate.Text = Convert.ToString(Convert.ToDateTime(System.DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss tt")));
                        txtsystime.Text = Convert.ToString(System.DateTime.Now.ToString("HH:mm:ss tt"));
                        txtmanualTime.Text = Convert.ToString(DateTime.Now.ToString("HH:mm:ss"));
                        string IDD = "TOKENNO: " + Convert.ToString(dt.Rows[0]["ASPTBLEMPID"].ToString()) + ",\nIDCARD : " + Convert.ToString(dt.Rows[0]["MIDCARD"].ToString()) + ",\nNAME   : " + Convert.ToString(dt.Rows[0]["FNAME"].ToString());

                        QRCoder.QRCodeGenerator qc = new QRCoder.QRCodeGenerator();
                        var mydata1 = qc.CreateQrCode(IDD, QRCoder.QRCodeGenerator.ECCLevel.L);
                        var code1 = new QRCoder.QRCode(mydata1);
                        pictureBox1.Image = code1.GetGraphic(50, Color.Black, Color.White, true);
                        string sel2 = ""; DataTable dt2 = new DataTable();
                        if (Class.Users.HostelName == "WOMENS HOSTEL" || Class.Users.HostelName == "WORKING GENTS HOSTEL" || Class.Users.HostelName == "GENTS STAFF HOSTEL")
                        {
                            sel2 = "SELECT to_char(A.ASPTBLHOSTELGATEPASSID) as ASPTBLHOSTELGATEPASSID,B.COMPNAME AS COMPCODE,D.MIDCARD ,C.FNAME ,E.MNNAME1 AS DISPNAME,substr(A.SYSTEMDATE,1,10) AS CONTACTNO,F.HOSTELNAME,F.BLOCKFLOOR , F.ROOMNO,G.REASON,A.PERMISSIONHRS,A.INTIME,A.OUTTIME, A.REMARKS   FROM ASPTBLHOSTELGATEPASS A  JOIN   GTCOMPMAST B ON B.GTCOMPMASTID = A.COMPCODE  JOIN   HREMPLOYMAST    C ON C.COMPCODE=B.GTCOMPMASTID AND C.COMPCODE=A.COMPCODE   JOIN HREMPLOYDETAILS D ON D.HREMPLOYMASTID=C.HREMPLOYMASTID AND D.IDCARD=C.IDCARDNO AND  D.DEPTNAME=A.DEPARTMENT AND D.MIDCARD=A.IDCARDNO    JOIN GTDEPTDESGMAST  E ON E.GTDEPTDESGMASTID = A.DEPARTMENT  JOIN HOSTELLIVEDATA F ON F.COMPCODE=B.COMPCODE AND F.IDCARDNO=C.IDCARDNO AND F.IDCARDNO=D.IDCARD  AND D.IDCARD=F.IDCARDNO JOIN ASPTBLREASONMAS G ON G.ASPTBLREASONMASID=A.REASON     WHERE  A.INTIME IS NULL and D.MIDCARD='" + dt.Rows[0]["MIDCARD"].ToString() + "'  AND  A.MODIFIED >= TO_DATE('" + System.DateTime.Now.ToString("dd-MM-yyyy") + "','dd-MM-yyyy')   ORDER BY 1";
                            DataSet ds2 = Utility.ExecuteSelectQuery(sel2, "ASPTBLHOSTELGATEPASS");
                            dt2 = ds2.Tables["ASPTBLHOSTELGATEPASS"];
                            if (dt2.Rows.Count > 1)
                            {
                                dt2.Rows.RemoveAt(0);
                            }
                        }
                        else
                        {

                            sel2 = "SELECT to_char(A.ASPTBLHOSTELGATEPASSID) as ASPTBLHOSTELGATEPASSID,B.COMPNAME AS COMPCODE,D.MIDCARD ,C.FNAME ,E.MNNAME1 AS DISPNAME,  substr(A.SYSTEMDATE,1,10) AS CONTACTNO,'" + Class.Users.HCompcode + "' as HOSTELNAME,''as BLOCKFLOOR , ''as ROOMNO,G.REASON,A.PERMISSIONHRS,A.INTIME,A.OUTTIME, A.REMARKS,D.IDCARD     FROM ASPTBLHOSTELGATEPASS A  JOIN   GTCOMPMAST B ON B.GTCOMPMASTID = A.COMPCODE  JOIN   HREMPLOYMAST    C ON C.COMPCODE=B.GTCOMPMASTID AND C.COMPCODE=A.COMPCODE       JOIN HREMPLOYDETAILS D ON D.HREMPLOYMASTID=C.HREMPLOYMASTID AND D.IDCARD=C.IDCARDNO AND  D.DEPTNAME=A.DEPARTMENT AND D.MIDCARD=A.IDCARDNO JOIN GTDEPTDESGMAST  E ON E.GTDEPTDESGMASTID = A.DEPARTMENT  JOIN ASPTBLREASONMAS G ON G.ASPTBLREASONMASID=A.REASON  WHERE  A.INTIME IS NULL and D.MIDCARD='" + dt.Rows[0]["MIDCARD"].ToString() + "'  AND  A.MODIFIED >= TO_DATE('" + System.DateTime.Now.ToString("dd-MM-yyyy") + "','dd-MM-yyyy')   ORDER BY 1";
                            DataSet ds3 = Utility.ExecuteSelectQuery(sel2, "ASPTBLHOSTELGATEPASS");
                            dt2 = ds3.Tables["ASPTBLHOSTELGATEPASS"];
                            if (dt.Rows.Count > 1)
                            {
                                dt.Rows.RemoveAt(0);
                            }
                        }
                        if (dt2.Rows.Count == 0)
                        {
                            butGetData.Visible = false; btnhostelsave.Visible = true;
                        }
                        else
                        {
                            MessageBox.Show("This Employee IDCard: -   '" + dt2.Rows[0]["MIDCARD"].ToString() + "====" + dt2.Rows[0]["FNAME"].ToString() + "'  not Closed Privious Pass.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            lvLogs1.Items.Clear(); btnhostelsave.Visible = false;
                            empty();
                        }


                        if (axCZKEM1.ClearData(iMachineNumber, iDataFlag))
                        {
                            axCZKEM1.RefreshData(iMachineNumber);//the data in the device should be refreshed
                        }
                        else
                        {
                            axCZKEM1.GetLastError(ref idwErrorCode);
                        }
                        axCZKEM1.EnableDevice(iMachineNumber, true);//enable the device
                        bIsConnected = false;

                    }
                }
                else
                {
                    butGetData.Visible = true; btnhostelsave.Visible = false;
                    MessageBox.Show("No Data Found in Finger Print Machine");
                }

                if (axCZKEM1.ClearData(iMachineNumber, iDataFlag))
                {
                    axCZKEM1.RefreshData(iMachineNumber);//the data in the device should be refreshed
                }
                else
                {
                    axCZKEM1.GetLastError(ref idwErrorCode);
                }
                axCZKEM1.EnableDevice(iMachineNumber, true);//enable the device
                bIsConnected = false;
            }
            catch (Exception ex)
            {
            
            
                MessageBox.Show(ex.ToString());
            }

            Cursor = Cursors.Default;
        }

        private void Comboreason_MouseHover(object sender, EventArgs e)
        {
            comboreason.BackColor = Color.White;
        }



        private void ReasonMasterRefreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            reason();
        }

        private void MenuRefreshToolStripMenuItem_Click_1(object sender, EventArgs e)
        {

        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {

        }

        public void Saves()
        {
            if (txtempid.Text != "" && txtRemarks.Text != "")
            {
                string native = ""; Class.Users.UserTime = 0;
                if (checknative.Checked == true) { native = "T"; } else { native = "F"; }
                if (txtintime.Text == "" || txtoutime.Text == "")
                {
                    DialogResult result = MessageBox.Show("This is Administrator Issue.Do You want to save this Record  'IDCardNo :-'" + txtempid.Text + "'' ,Name:-  '" + comboempname.Text + "'' this Record ??", "Gate Pass", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

                    if (result.Equals(DialogResult.OK))
                    {
                        DateTime statetime;
                        string sel3 = "SELECT '" + System.DateTime.Now.Year + "' AS FINYEAR,H.DESIGNATION AS DESIGN,B.PHONENO  || ',' || B.FAXNO AS CONTACTAGF, A.ASPTBLHOSTELGATEPASSID  AS TOKENNO,B.COMPCODE,D.MIDCARD AS IDCARDNO,C.FNAME AS EMPNAME,E.MNNAME1 AS DEPARTMENT,A.CONTACTNO ,G.REASON,A.PERMISSIONHRS,A.SYSTEMDATE,A.QRCODE,''EMPIMAGE   FROM ASPTBLHOSTELGATEPASS A   JOIN   GTCOMPMAST B ON B.GTCOMPMASTID = A.COMPCODE  JOIN   HREMPLOYMAST    C ON C.COMPCODE=B.GTCOMPMASTID AND C.COMPCODE=A.COMPCODE    JOIN HREMPLOYDETAILS D ON D.HREMPLOYMASTID=C.HREMPLOYMASTID AND D.IDCARD=C.IDCARDNO AND  D.DEPTNAME=A.DEPARTMENT AND D.MIDCARD=A.IDCARDNO      JOIN GTDEPTDESGMAST  E ON E.GTDEPTDESGMASTID = A.DEPARTMENT  JOIN ASPTBLREASONMAS G ON G.ASPTBLREASONMASID=A.REASON    JOIN GTDESIGNATIONMAST H ON H.GTDESIGNATIONMASTID=D.DESIGNATION  LEFT OUTER JOIN ASPTBLEMP I ON I.COMPCODE=B.GTCOMPMASTID AND I.IDCARDNO=A.IDCARDNO     WHERE   A.ASPTBLHOSTELGATEPASSID=" + txtempid.Text;
                        DataSet ds3 = Utility.ExecuteSelectQuery(sel3, "ASPTBLHOSTELGATEPASS");
                        DataTable dt3 = ds3.Tables["ASPTBLHOSTELGATEPASS"];
                        DateTime endtime = Convert.ToDateTime(System.DateTime.Now.ToString());
                        if (dt3.Rows.Count <= 0)
                        {
                            statetime = Convert.ToDateTime(System.DateTime.Now.ToString());
                        }
                        else
                        {
                            statetime = Convert.ToDateTime(dt3.Rows[0]["SYSTEMDATE"].ToString());
                        }                     
                        TimeSpan differ = endtime.Subtract(statetime);
                        TimeSpan differ1 = endtime - statetime;
                        if (dt3.Rows.Count > 0)
                        {
                            if (Class.Users.IPADDRESS == "192.168.101.15") {
                                string tt = txtintime.Text == "" ? System.DateTime.Now.ToString() : txtintime.Text;
                                TimeSpan differ2 =Convert.ToDateTime(txtintime.Text=="" ? System.DateTime.Now.ToString() : txtintime.Text) - statetime;
                                string ins = "update  ASPTBLHOSTELGATEPASS set OUTTIME='" + dt3.Rows[0]["SYSTEMDATE"].ToString() + "', INTIME='" + tt.ToString() + "',NATIVE='" + native + "', TIMEDIFF='" + differ2.ToString() + "',REMARKS='" + txtRemarks.Text + "',IPADDRESS='" + Class.Users.IPADDRESS + "' WHERE ASPTBLHOSTELGATEPASSID=" + txtempid.Text;
                                Utility.ExecuteNonQuery(ins);
                                MessageBox.Show("This is Record Saved Successfully.  'IDCardNo :-'" + txtempid.Text + "'' ,Name:-  '" + comboempname.Text + "'' this Record ??", "Gate Pass", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            }
                            else
                            {
                                string ins = "update  ASPTBLHOSTELGATEPASS set OUTTIME='" + dt3.Rows[0]["SYSTEMDATE"].ToString() + "', INTIME='" + System.DateTime.Now.ToString() + "',NATIVE='" + native + "', TIMEDIFF='" + differ1.ToString() + "',REMARKS='" + txtRemarks.Text + "-" + Class.Users.HUserName + "',IPADDRESS='" + Class.Users.IPADDRESS + "' WHERE ASPTBLHOSTELGATEPASSID=" + txtempid.Text;
                                Utility.ExecuteNonQuery(ins);
                            }
                        }
                        else
                        {
                            if (dt3.Rows.Count <= 0) {
                                MessageBox.Show("Invalid .  'IDCardNo :-'" + txtempid.Text + "'' ,Name:-  '" + comboempname.Text + "'' this Record ??", "Do not Save", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                string ins = "update  ASPTBLHOSTELGATEPASS set OUTTIME='" + dt3.Rows[0]["SYSTEMDATE"].ToString() + "', INTIME='" + System.DateTime.Now.ToString() + "',NATIVE='" + native + "', TIMEDIFF='" + differ1.ToString() + "',REMARKS='" + txtRemarks.Text + "-" + Class.Users.HUserName + "',IPADDRESS='" + Class.Users.IPADDRESS + "' WHERE ASPTBLHOSTELGATEPASSID=" + txtempid.Text;
                                Utility.ExecuteNonQuery(ins);
                                MessageBox.Show("This is Record Saved Successfully.  'IDCardNo :-'" + txtempid.Text + "'' ,Name:-  '" + comboempname.Text + "'' this Record ??", "Gate Pass", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            }
                        }
                        News();
                    }

                    else
                    {

                        MessageBox.Show("Invalid.  'IDCardNo :-'" + txtempid.Text + "'' ,Name:-  '" + comboempname.Text + "'' this Record ??", "Gate Pass", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                }
                else
                {
                    if (txtempid.Text != "" && txtintime.Text != "" && txtoutime.Text != "")
                    {
                        MessageBox.Show("Invalid.", "Gate Pass", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        empty();
                    }
                    else
                    {
                        MessageBox.Show("Invalid.  pls go to Your Administrator.??", "Gate Pass", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        empty();
                    }
                }
            }
            else
            {
                MessageBox.Show("Pls Enter Remarks Field    : ", "Employee Name :-" + comboempname.Text + " Remarks Field Empty", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtRemarks.Select();
            }
        }



        private void butprintcancel_Click(object sender, EventArgs e)
        {
            this.panelprint.Visible = false;
        }

        private void butGetData_Click(object sender, EventArgs e)
        {
            empty(); Cursor.Current = Cursors.WaitCursor;
            if (txtidcardno.Text == "")
            {
                
                Class.Users.Intimation = "PAYROLL"; 
                ViewToolStripMenuItem_Click(sender, e);
            }
            Cursor.Current = Cursors.Default;
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
            if (comboreason.Text == "PERSONAL")
            {
                txtRemarks.Text = comboreason.Text;
            }
            else
            {
                txtRemarks.Text = "";
            }
            comboBox1.Focus();
        }

        private void pictureempimage_MouseHover(object sender, EventArgs e)
        {
           
        }

        private void pictureempimage_MouseLeave(object sender, EventArgs e)
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
            GlobalVariables.MdiPanel.Show();
            this.Hide();
            empty();
            GlobalVariables.HeaderName.Text = "";
            GlobalVariables.TabCtrl.TabPages.RemoveAt(GlobalVariables.TabCtrl.SelectedIndex);

        }

        private void checkAGF_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void HostelGatePass_MouseClick(object sender, MouseEventArgs e)
        {
            
        }

        private void HostelGatePass_MouseDown(object sender, MouseEventArgs e)
        {
            
        }

        private void tabPage1_Click(object sender, EventArgs e)
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

        private void checkconnection_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtRemarks.Text = comboBox1.Text;
        }

     
    }
}
