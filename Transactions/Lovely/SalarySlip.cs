using CrystalDecisions.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Pinnacle.Transactions.Lovely
{
    public partial class SalarySlip : Form,ToolStripAccess
    {
        private static SalarySlip _instance;
        public static SalarySlip Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new SalarySlip();
                GlobalVariables.CurrentForm = _instance; 
                GlobalVariables.HeaderName.Text = "";
                return _instance;
            }
        }
        public SalarySlip()
        {
            InitializeComponent();
            Class.Users.IPADDRESS = GenFun.GetLocalIPAddress();
           // tabControl1.TabPages.Remove(tabPage2); 
        }

        public void News()
        {
            panel1.BackColor = Class.Users.BackColors;          
            flowLayoutPanel1.Font = Class.Users.FontName;
            this.BackColor = Class.Users.BackColors;
            this.Font = Class.Users.FontName;
            Class.Users.UserTime = 0;
            companyload(); Finyearload();
            flowcontrolbind(combofinyear.Text, combocompcode.Text);
        }


        Models.Master mas = new Models.Master();
        Models.UserRights sm = new Models.UserRights();
        ListView listfilter = new ListView();
        ListView listfilter1 = new ListView(); DataTable dtgeneral = new DataTable();
        Byte[] bytes;
        DataTable dtprint1 = new DataTable();
        DataTable dtprint2 = new DataTable();
        DataTable dtprint3 = new DataTable();
        ListView allip = new ListView();
        string[] s; int i = 0;



        public zkemkeeper.CZKEMClass axCZKEM1 = new zkemkeeper.CZKEMClass();
        private bool bIsConnected = false;//the boolean value identifies whether the device is connected
        private int iMachineNumber=20;//the serial number of the device.After connecting the device ,this value will be changed.      
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




        public void Finyearload()
        {
            try
            {
                string sel0 = "SELECT DISTINCT  E.GTFINANCIALYEARID,  E.FINYR AS  Finyear,E.CURRENTFINYR FROM  GTFINANCIALYEAR  E ORDER BY 2 ";//WHERE E.CURRENTFINYR='T'
                DataSet ds0 = Utility.ExecuteSelectQuery(sel0, "hremploymast");
                DataTable dt = ds0.Tables["hremploymast"];
                combofinyear.DisplayMember = "Finyear";
                combofinyear.ValueMember = "GTFINANCIALYEARID";
                combofinyear.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("companyload: " + ex.Message, " Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        public void companyload()
        {
            try
            {
                string sel0 = "SELECT DISTINCT B.GTCOMPMASTID,B.COMPCODE FROM  HREMPLOYMAST A JOIN GTCOMPMAST B ON A.COMPCODE=B.GTCOMPMASTID  JOIN HREMPLOYDETAILS C ON A.HREMPLOYMASTID=C.HREMPLOYMASTID JOIN GTFINANCIALYEAR E ON E.GTFINANCIALYEARID=C.FINYEAR  where   b.compcode='" + Class.Users.HCompcode + "' order by 2";
                DataSet ds0 = Utility.ExecuteSelectQuery(sel0, "hremploymast");
                DataTable dt = ds0.Tables["hremploymast"];

                combocompcode.DisplayMember = "COMPCODE";
                combocompcode.ValueMember = "GTCOMPMASTID";
                combocompcode.DataSource = dt;

            }
            catch (Exception ex)
            {
                MessageBox.Show("companyload: " + ex.Message, " Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        private void flowcontrolbind(string finyear, string compcode)
        {

            if (finyear.Length > 1 && compcode.Length > 1)
            {
                string sel0 = " select  distinct A.asptblpayslipperID, A.PAYMONTH,A.FINYEAR,b.compcode from asptblpayslipper a JOIN GTCOMPMAST B ON B.GTCOMPMASTID=A.COMPCODE  where     b.compcode='" + compcode + "' and a.username='"+Class.Users.HUserName+"' order by 1";//A.FINYEAR='" + finyear + "' AND
                DataSet ds0 = Utility.ExecuteSelectQuery(sel0, "LOPPLhpayroll");
                DataTable dt = ds0.Tables["LOPPLhpayroll"];
                Class.Users.Finyear = "";
                if (dt.Rows.Count > 0)
                {
                    Class.Users.UserTime = 0;
                    Class.Users.Finyear = dt.Rows[0]["FINYEAR"].ToString();
                    UserControls.MonthControl[] items = new UserControls.MonthControl[dt.Rows.Count];
                    Class.Users.PayPeriod = ""; flowLayoutPanel1.Controls.Clear();
                    
                    foreach (DataRow myRow in dt.Rows)
                    {

                        items[i] = new UserControls.MonthControl(); items[i].compcode.Visible = false; items[i].finyear.Visible = false;
                        items[i].panelheader.BackColor = Class.Users.BackColors;
                        items[i].month.ForeColor = Class.Users.BackColors;
                        items[i].compcode.Text = Convert.ToString(myRow["COMPCODE"].ToString());
                        items[i].finyear.Text = Convert.ToString(myRow["Finyear"].ToString());
                        Class.Users.Finyear = Convert.ToString(myRow["Finyear"].ToString());
                        items[i].month.Text = Convert.ToString(myRow["PAYMONTH"].ToString());
                        Class.Users.PayPeriod = Convert.ToString(myRow["PAYMONTH"].ToString());
                        items[i].month.Click += Month_Click;
                        flowLayoutPanel1.Controls.Add(items[i]);
                    }
                }
                else
                {
                    string sel1 = "SELECT AA.PAYPERIOD as PAYMONTH,AA.FINYR as Finyear,AA.COMPCODE FROM MONTHLYPAYFRQ AA WHERE AA.STDT = ( SELECT MAX(B.STDT) FROM LOPPLHPAYROLL A JOIN MONTHLYPAYFRQ B ON A.PAYPERIOD = B.PAYPERIOD )";
                    DataSet ds1 = Utility.ExecuteSelectQuery(sel1, "MONTHLYPAYFRQ");
                    DataTable dt1 = ds1.Tables["MONTHLYPAYFRQ"];
                    if (dt1.Rows.Count > 0)
                    {
                        Class.Users.UserTime = 0;
                        Class.Users.Finyear = dt1.Rows[0]["FINYEAR"].ToString();

                        UserControls.MonthControl[] items = new UserControls.MonthControl[dt1.Rows.Count];
                        Class.Users.PayPeriod = ""; flowLayoutPanel1.Controls.Clear();
                        foreach (DataRow myRow in dt1.Rows)
                        {
                            
                            items[i] = new UserControls.MonthControl(); items[i].compcode.Visible = false; items[i].finyear.Visible = false;
                            items[i].panelheader.BackColor = Class.Users.BackColors;
                            items[i].month.ForeColor = Class.Users.BackColors;
                            items[i].compcode.Text = Convert.ToString(myRow["COMPCODE"].ToString());
                            items[i].finyear.Text = Convert.ToString(myRow["Finyear"].ToString());
                            items[i].month.Text = Convert.ToString(myRow["PAYMONTH"].ToString());
                            Class.Users.Finyear = Convert.ToString(myRow["Finyear"].ToString());
                            Class.Users.PayPeriod = Convert.ToString(myRow["PAYMONTH"].ToString());
                            items[i].month.Click += Month_Click;
                            flowLayoutPanel1.Controls.Add(items[i]);
                     
                        }
                    }

                }



            }

     
        }

        private void Month_Click(object sender, EventArgs e)
        {

            s = sender.ToString().Split(','); Cursor = Cursors.WaitCursor;
            try
            {

                lvLogs1.Items.Clear();

                int k = 0;
                iIndex = 0;


                iGLCount = 0;

                string ip = "";

                Class.Users.UserTime = 0;
                DataTable dt0 = new DataTable();               
                    dt0 = Utility.SQLQuery("SELECT  C.MACIP  FROM  ASPTBLMACHINEMAS A   JOIN   GTCOMPMAST B ON B.GTCOMPMASTID = A.COMPCODE    JOIN ASPTBLMACIP C ON C.ASPTBLMACIPID=A.IPADDRESS  JOIN  ASPTBLUSERMAS D ON D.COMPCODE=B.GTCOMPMASTID  AND D.USERID=A.WARDENNAME  JOIN ASPTBLMACHINEMAS E ON E.COMPCODE=B.GTCOMPMASTID AND E.WARDENNAME=D.USERID AND C.ASPTBLMACIPID=E.IPADDRESS AND E.ACTIVE='T' and B.COMPCODE='" + Class.Users.HCompcode + "' and D.username='" + Class.Users.HUserName + "' ");
               
                if (dt0.Rows.Count > 0)
                {
                    int maxip = dt0.Rows.Count;
                    if (maxip == 0)
                    {
                        MessageBox.Show("IP Address not assign this User.   : " + Class.Users.HUserName);
                    }
                    if (maxip == 1)
                    {
                        int i = 0;
                        for (i = 0; i < maxip; i++)
                        {
                            if (bIsConnected == false)
                            {
                                bIsConnected = axCZKEM1.Connect_Net(dt0.Rows[i]["MACIP"].ToString(), Convert.ToInt32(4370));
                            }
                            else
                            {
                                bIsConnected = true;
                            }
                            ip = dt0.Rows[i]["MACIP"].ToString();
                            if (bIsConnected == true)
                            { 

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
                                    Cursor = Cursors.Default;
                                    MessageBox.Show("No Data Found this Machine...." + ip.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                                axCZKEM1.EnableDevice(iMachineNumber, true);//enable the device    

                            }
                            else
                            {
                                axCZKEM1.GetLastError(ref idwErrorCode);
                                Cursor = Cursors.Default;
                                MessageBox.Show("Unable to connect the device , ErrorCode=" + idwErrorCode.ToString() + "---IP-----" + dt0.Rows[i]["MACIP"].ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                tabControl1.SelectTab(tabPage1);
                                return;
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("One More IP are Enabled. IP Count is  : " + maxip.ToString());
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message.ToString());

            }
            if (lvLogs1.Items.Count >= 1)
            {

                var idd = lvLogs1.Items[lvLogs1.Items.Count - 1].SubItems[1].Text;
                string sel0 = "select  distinct   A.FINYEAR, C.IDCARD ,b.FNAME,d.compcode ,a.payperiod   from LOPPLhpayroll a  JOIN HREMPLOYMAST B ON A.EMPID=B.HREMPLOYMASTID   JOIN HREMPLOYDETAILS C ON C.HREMPLOYMASTID=B.HREMPLOYMASTID AND B.IDCARDNO=C.IDCARD AND C.IDACTIVE='YES'   JOIN GTCOMPMAST D ON D.GTCOMPMASTID=B.COMPCODE  where  A.FINYEAR='" + Class.Users.Finyear + "' AND  D.compcode='" + combocompcode.Text + "' AND  A.PAYPERIOD='" + s[1].Substring(7) + "' AND  C.OLDIDNO='" + idd + "' order by 1";
                DataSet ds0 = Utility.ExecuteSelectQuery(sel0, "LOPPLhpayroll");
                DataTable dt = ds0.Tables["LOPPLhpayroll"];
                if (dt.Rows.Count > 0)
                {

                    foreach (DataRow myRow in dt.Rows)
                    {

                        Class.Users.IDCARDNO = Convert.ToInt64(myRow["IDCARD"].ToString());
                        Class.Users.PayPeriod = Convert.ToString(myRow["payperiod"].ToString());
                    }
                    if (checkspecified.Checked == true)
                    {
                        MidCardButton_Click(sender, e);
                    }
                }
                else
                {
                    Cursor = Cursors.Default;
                    MessageBox.Show("This IDCard not found in PayRoll", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Question);
                }
            }
            else
            {
                Cursor = Cursors.Default;
                MessageBox.Show("No data found in Finger Print-Machine in Today" + System.DateTime.Now.ToShortDateString(), "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
            Cursor = Cursors.Default;

        }
        string folderLocation = "C:\\PaySlip-Download\\";
      
        private void MidCardButton_Click(object sender, EventArgs e)
        {
            string sel0 = "";
            if (Class.Users.HUserName == "GROUP1" || Class.Users.HUserName == "GROUP2" || Class.Users.HUserName == "GROUP3" || Class.Users.HUserName == "GROUP4")
            {
              sel0 = "SELECT XX.FINYEAR,XX.COMPCODE,XX.COMPNAME,XX.FNAME,XX.IDCARDNO,XX.BRANCH,XX.DESIGNATION,       XX.PAYPERIOD,XX.NOOFDAYS,XX.LOP,XX.EBASIC,XX.EHRA,XX.EDA,XX.ESPL,XX.ERPF,XX.ERESI,XX.EMESSFEES,XX.EOTHDED,XX.ADV,XX.SADV,       XX.BANK,XX.PAYCAT,     XX.IT,XX.EGROSS,XX.NETPAY,XX.BASIC,XX.DA,XX.HRA,XX.SPL,XX.TOTDED,XX.COMPLOGO,XX.DG,XX.PAYCAT1,NVL(YYY.TAKEN,0) TAKEN,     NVL(YYY.CLOSING,0) CLOSING,YYY.IDCARD,NVL(YYY.ELEAVE,0) ELEAVE,XX.EMPTYPE,XX.WALLO  TEST1 ,XX.EWALLO TEST2 ,XX.ETRDED TEST3  FROM (SELECT AA.FINYEAR,AA.COMPCODE,AA.COMPNAME,AA.FNAME,AA.IDCARDNO,AA.BRANCH,AA.DESIGNATION, AA.PAYPERIOD,AA.NOOFDAYS,AA.LOP,AA.EBASIC,AA.EHRA,AA.EDA,AA.ESPL,  AA.ERPF,AA.ERESI,AA.EMESSFEES,AA.EOTHDED,     AA.ADV,AA.SADV,AA.BANK,AA.PAYCAT,                AA.IT,AA.EGROSS,AA.NETPAY,AA.BASIC,AA.DA,AA.HRA,AA.SPL,AA.TOTDED,AA.COMPLOGO,  AA.DG,AA.PAYCAT1,AA.EMPTYPE ,AA.WALLO ,AA.EWALLO,AA.ETRDED  FROM ( SELECT TO_CHAR(TO_DATE(H.MPGDT,'DD/MM/YY'),'DD/MM/YYYY') AS FINYEAR,C.COMPCODE,C.COMPNAME,B.FNAME,B.IDCARDNO,  A.BRANCH,D.DESIGNATION,     A.PAYPERIOD,     A.WDAYS NOOFDAYS,A.MDAYS-A.WDAYS LOP,A.EBASIC, A.EHRA,                 A.EDA,A.ESPL,A.PF ERPF,A.ESI ERESI,  A.MESSFEES EMESSFEES,(A.EOTHDED+ROUND((CASE WHEN A.ECDED > 0 THEN  ( A.WDAYS*(450/A.MDAYS) ) ELSE 0 END),2)+A.ETDED) EOTHDED,A.LOAN ADV,A.SADV,CASE WHEN A.BANK = 'CASH' THEN ' ' ELSE A.BANK END BANK,A.PAYCAT,A.TDSN IT,A.EGROSS, ROUND(A.NETPAY-ROUND((CASE WHEN A.ECDED > 0 THEN  ( A.WDAYS*(450/A.MDAYS) ) ELSE 0 END),2)+A.ECDED+A.ETRDED,0) NETPAY  ,A.BASIC*2 BASIC,A.DA*2 DA,A.HRA*2 HRA,A.SPL*2 SPL,  A.TOTDED+ROUND((CASE WHEN A.ECDED > 0 THEN  ( A.WDAYS*(450/A.MDAYS) ) ELSE 0 END),2)-A.ECDED-A.ETRDED TOTDED  ,(SELECT LOGO FROM EDOCIMAGE1 WHERE IMGNAME='LOGO') COMPLOGO, (SELECT LOGO FROM EDOCIMAGE1      WHERE IMGNAME='DGGM') DG,TO_CHAR(TO_DATE(G.STDT,'DD/MM/YY'),'DD/MM/YYYY')||' TO '||TO_CHAR(TO_DATE(G.ENDT,'DD/MM/YY'),'DD/MM/YYYY') PAYCAT1,'{EMPTYPE}' EMPTYPE ,A.WALLO*2 WALLO   ,   A.EWALLO  ,0 ETRDED   FROM  LOPPLHPAYROLL A JOIN HREMPLOYMAST B ON B.HREMPLOYMASTID=A.EMPID JOIN GTCOMPMAST C ON C.GTCOMPMASTID=B.COMPCODE    JOIN HRDESIGNATIONMAST D ON D.HRDESIGNATIONMASTID=A.DESIGNATION JOIN HREMPLOYDETAILS E ON E.HREMPLOYMASTID=B.HREMPLOYMASTID    JOIN HRBANDMAST F ON F.HRBANDMASTID=E.BAND JOIN MONTHLYPAYFRQ G ON G.PAYPERIOD=A.PAYPERIOD JOIN HRMFRQ H ON H.PAYPERIOD=A.PAYPERIOD WHERE      H.PAYPERIOD='" + Class.Users.PayPeriod + "' AND A.PAYPERIOD='" + Class.Users.PayPeriod + "' AND A.PCTYPE='ACTUAL' AND B.IDCARDNO='" + Class.Users.IDCARDNO + "' ) AA )XX,       (SELECT YY.TAKEN,YY.CLOSING,YY.IDCARD,YY.ELEAVE FROM (SELECT  DD.BRANCHNAME BRANCH,AA.IDCARD,BB.FNAME EMPNAME,BB.GENDER,EE.DISPNAME DEPT,FF.DESIGNATION,GG.BANDID ,      AA.SHIFTCNT WDAYS,AA.ELEAVE,AA.TAKEN,AA.TAKEN1,CASE WHEN (AA.ELEAVE-AA.TAKEN1) < 0  THEN 0 ELSE (AA.ELEAVE-AA.TAKEN1) END CLOSING FROM (SELECT A.IDCARD,SUM(A.SHIFTCNT) SHIFTCNT,      ROUND(SUM(A.ELEAVE),3) ELEAVE,SUM(A.TAKEN) TAKEN,SUM(A.TAKEN1) TAKEN1 FROM ( SELECT A.IDCARD,A.SHIFTCNT,0 ELEAVE,0 TAKEN,0 TAKEN1 FROM HREMPWDAY A       UNION ALL       SELECT A.EMPID IDCARD,A.SHIFTCNT,0 ELEAVE,0 TAKEN,0 TAKEN1 FROM HDATTA A,MONTHLYPAYFRQ B,HRLELIGIBLE C WHERE A.PAYPERIOD = B.PAYPERIOD AND B.PAYPERIOD1 = C.PAYPERIOD       AND A.DOCDATE <= (SELECT AA.ENDT FROM MONTHLYPAYFRQ AA WHERE AA.PAYPERIOD='" + Class.Users.PayPeriod + "') AND A.SHIFTCNT > 0 UNION ALL SELECT A.EMPID IDCARD,0 SHIFTCNT,      A.SHIFTCNT*A.LEAVE ELEAVE,0 TAKEN,0 TAKEN1 FROM HDATTA A,MONTHLYPAYFRQ B,HRLELIGIBLE C WHERE A.PAYPERIOD = B.PAYPERIOD AND B.PAYPERIOD1 = C.PAYPERIOD       AND A.DOCDATE <= (SELECT AA.ENDT FROM MONTHLYPAYFRQ AA WHERE AA.PAYPERIOD='" + Class.Users.PayPeriod + "') AND A.SHIFTCNT > 0       AND B.FINYR = (SELECT AA.FINYR FROM MONTHLYPAYFRQ AA    WHERE AA.PAYPERIOD='" + Class.Users.PayPeriod + "')       UNION ALL SELECT TO_NUMBER(A.IDNOCHK) IDCARD,0 SHIFTCNT,0 ELEAVE,(0-SUM(A.STKOPBAL)) TAKEN,(0-SUM(A.STKOPBAL)) TAKEN1 FROM HRLEAVEREGMAST A       WHERE A.LRDATE BETWEEN ( SELECT AA.STDT FROM MONTHLYPAYFRQ AA WHERE AA.PAYPERIOD ='" + Class.Users.PayPeriod + "' )       AND (SELECT AA.ENDT FROM MONTHLYPAYFRQ AA WHERE AA.PAYPERIOD='" + Class.Users.PayPeriod + "') AND A.LTYPE <> 'ENC'       AND A.FINYEAR = (SELECT AA.FINYR FROM MONTHLYPAYFRQ AA WHERE AA.PAYPERIOD='" + Class.Users.PayPeriod + "')         GROUP BY A.IDNOCHK UNION ALL SELECT TO_NUMBER(A.IDNOCHK) IDCARD,0 SHIFTCNT,0 ELEAVE,0 TAKEN,(0-SUM(A.STKOPBAL)) TAKEN1 FROM HRLEAVEREGMAST A       WHERE A.LRDATE < ( SELECT AA.STDT FROM MONTHLYPAYFRQ AA WHERE AA.PAYPERIOD ='" + Class.Users.PayPeriod + "' )       AND A.FINYEAR = (SELECT AA.FINYR FROM MONTHLYPAYFRQ AA WHERE AA.PAYPERIOD='" + Class.Users.PayPeriod + "')       GROUP BY A.IDNOCHK UNION ALL SELECT TO_NUMBER(A.IDNOCHK) IDCARD,0 SHIFTCNT,0 ELEAVE,0 TAKEN,(0-SUM(A.STKOPBAL)) TAKEN1 FROM HRLEAVEREGMAST A       WHERE A.LRDATE = ( SELECT AA.STDT FROM MONTHLYPAYFRQ AA WHERE AA.PAYPERIOD ='" + Class.Users.PayPeriod + "') AND A.LTYPE = 'ENC'       AND ( SELECT TRIM(TO_CHAR(AA.STDT,'DD/MM')) FROM MONTHLYPAYFRQ AA WHERE AA.PAYPERIOD ='" + Class.Users.PayPeriod + "') = '01/01'        AND A.FINYEAR = (SELECT AA.FINYR FROM MONTHLYPAYFRQ AA WHERE AA.PAYPERIOD='" + Class.Users.PayPeriod + "')         GROUP BY A.IDNOCHK        UNION ALL       SELECT TO_NUMBER(BB.IDCARD) IDCARD,A.OLHDCNT SHIFTCNT,0 ELEAVE,0 TAKEN,0 TAKEN1 FROM HRONDUTYDET A,HREMPLOYDETAILS BB, MONTHLYPAYFRQ B,HRLELIGIBLE C       WHERE A.IDCARD = BB.HREMPLOYDETAILSID AND A.PAYPERIOD = B.PAYPERIOD AND B.PAYPERIOD1 = C.PAYPERIOD       AND A.ODATE  <= (SELECT AA.ENDT FROM MONTHLYPAYFRQ AA WHERE AA.PAYPERIOD='" + Class.Users.PayPeriod + "')       UNION ALL       SELECT TO_NUMBER(BB.IDCARD) IDCARD,0 SHIFTCNT,A.OLHDCNT*0.05 ELEAVE,0 TAKEN,0 TAKEN1 FROM HRONDUTYDET A,HREMPLOYDETAILS BB, MONTHLYPAYFRQ B,HRLELIGIBLE C       WHERE A.IDCARD = BB.HREMPLOYDETAILSID AND A.PAYPERIOD = B.PAYPERIOD AND B.PAYPERIOD1 = C.PAYPERIOD       AND A.ODATE  <= (SELECT AA.ENDT FROM MONTHLYPAYFRQ AA WHERE AA.PAYPERIOD='" + Class.Users.PayPeriod + "')       AND B.FINYR = (SELECT AA.FINYR FROM MONTHLYPAYFRQ AA WHERE AA.PAYPERIOD='" + Class.Users.PayPeriod + "') ) A             GROUP BY A.IDCARD HAVING ( SUM(A.SHIFTCNT) >= 240 AND SUM(A.ELEAVE) > 0 ) ) AA,HREMPLOYMAST BB,HREMPLOYDETAILS CC,BRANCHMAST DD,GTDEPTDESGMAST EE,HRDESIGNATIONMAST FF,      HRBANDMAST GG,GTCOMPMAST HH WHERE AA.IDCARD = CC.IDCARD AND BB.HREMPLOYMASTID = CC.HREMPLOYMASTID AND BB.WORKNATURE = DD.BRANCHMASTID       AND EE.GTDEPTDESGMASTID = CC.DEPTNAME AND FF.HRDESIGNATIONMASTID = CC.DESIGNATION AND GG.HRBANDMASTID = CC.BAND AND HH.GTCOMPMASTID = BB.COMPCODE )  YY ) YYY WHERE XX.IDCARDNO=YYY.IDCARD(+) ORDER BY XX.FNAME";
            }
            else
            {
                sel0 = "SELECT XX.FINYEAR,XX.COMPCODE,XX.COMPNAME,XX.FNAME,XX.IDCARDNO,XX.BRANCH,XX.DESIGNATION,       XX.PAYPERIOD,XX.NOOFDAYS,XX.LOP,XX.EBASIC,XX.EHRA,XX.EDA,XX.ESPL,XX.ERPF,XX.ERESI,XX.EMESSFEES,XX.EOTHDED,XX.ADV,XX.SADV,       XX.BANK,XX.PAYCAT,        XX.IT,XX.EGROSS,XX.NETPAY,XX.BASIC,XX.DA,XX.HRA,XX.SPL,XX.TOTDED,XX.COMPLOGO,XX.DG,XX.PAYCAT1,NVL(YYY.TAKEN,0) TAKEN,              NVL(YYY.CLOSING,0) CLOSING,YYY.IDCARD,NVL(YYY.ELEAVE,0) ELEAVE,XX.EMPTYPE,XX.WALLO  test1 ,XX.EWALLO test2 ,XX.ETRDED test3  FROM (SELECT AA.FINYEAR,AA.COMPCODE,AA.COMPNAME,AA.FNAME,AA.IDCARDNO,AA.BRANCH,AA.DESIGNATION, AA.PAYPERIOD,AA.NOOFDAYS,AA.LOP,AA.EBASIC,AA.EHRA,AA.EDA,AA.ESPL,AA.ERPF,AA.ERESI,AA.EMESSFEES,AA.EOTHDED,     AA.ADV,AA.SADV,AA.BANK,AA.PAYCAT,                AA.IT,AA.EGROSS,AA.NETPAY,AA.BASIC,AA.DA,AA.HRA,AA.SPL,AA.TOTDED,AA.COMPLOGO,AA.DG,AA.PAYCAT1,AA.EMPTYPE ,AA.WALLO ,AA.EWALLO,AA.ETRDED                 FROM ( SELECT TO_CHAR(TO_DATE(G.MPGDT,'DD/MM/YY'),'DD/MM/YYYY') FINYEAR,C.COMPCODE,C.COMPNAME,B.FNAME,B.IDCARDNO,A.BRANCH,D.DESIGNATION,     A.PAYPERIOD,     A.WDAYS NOOFDAYS,A.MDAYS-A.WDAYS LOP,A.EBASIC, A.EHRA,                 A.EDA,A.ESPL,A.PF ERPF,A.ESI ERESI,A.MESSFEES EMESSFEES,(A.EOTHDED+A.ECDED+A.ETDED) EOTHDED,A.LOAN ADV,A.SADV,CASE WHEN A.BANK = 'CASH' THEN ' ' ELSE A.BANK END BANK,A.PAYCAT,                  A.TDSN IT,A.EGROSS,A.NETPAY,A.BASIC*2 BASIC,A.DA*2 DA,A.HRA*2 HRA,A.SPL*2 SPL,A.TOTDED,(SELECT LOGO FROM EDOCIMAGE1 WHERE IMGNAME='LOGO') COMPLOGO, (SELECT LOGO FROM EDOCIMAGE1     WHERE IMGNAME='DGGM') DG,TO_CHAR(TO_DATE(G.STDT,'DD/MM/YY'),'DD/MM/YYYY')||' TO '||TO_CHAR(TO_DATE(G.ENDT,'DD/MM/YY'),'DD/MM/YYYY') PAYCAT1,'{EMPTYPE}' EMPTYPE ,A.WALLO*2 WALLO   ,A.EWALLO  ,A.ETRDED   FROM  LOPPLHPAYROLL A JOIN HREMPLOYMAST B ON B.HREMPLOYMASTID=A.EMPID JOIN GTCOMPMAST C ON C.GTCOMPMASTID=B.COMPCODE JOIN HRDESIGNATIONMAST D ON D.HRDESIGNATIONMASTID=A.DESIGNATION JOIN HREMPLOYDETAILS E ON E.HREMPLOYMASTID=B.HREMPLOYMASTID JOIN HRBANDMAST F ON F.HRBANDMASTID=E.BAND JOIN MONTHLYPAYFRQ G ON G.PAYPERIOD=A.PAYPERIOD WHERE  A.PAYPERIOD= '" + Class.Users.PayPeriod + "' AND A.PCTYPE='ACTUAL' and B.IDCARDNO= '" + Class.Users.IDCARDNO + "' ) AA ) XX, (SELECT YY.TAKEN,YY.CLOSING,YY.IDCARD,YY.ELEAVE FROM (SELECT  DD.BRANCHNAME BRANCH,AA.IDCARD,BB.FNAME EMPNAME,BB.GENDER,EE.DISPNAME DEPT,FF.DESIGNATION,GG.BANDID ,AA.SHIFTCNT WDAYS,AA.ELEAVE,AA.TAKEN,AA.TAKEN1,CASE WHEN (AA.ELEAVE-AA.TAKEN1) < 0  THEN 0 ELSE (AA.ELEAVE-AA.TAKEN1) END CLOSING FROM (SELECT A.IDCARD,SUM(A.SHIFTCNT) SHIFTCNT,ROUND(SUM(A.ELEAVE),3) ELEAVE,SUM(A.TAKEN) TAKEN,SUM(A.TAKEN1) TAKEN1 FROM ( SELECT A.IDCARD,A.SHIFTCNT,0 ELEAVE,0 TAKEN,0 TAKEN1 FROM HREMPWDAY A UNION ALL SELECT A.EMPID IDCARD,A.SHIFTCNT,0 ELEAVE,0 TAKEN,0 TAKEN1 FROM HDATTA A,MONTHLYPAYFRQ B,HRLELIGIBLE C WHERE A.PAYPERIOD = B.PAYPERIOD AND B.PAYPERIOD1 = C.PAYPERIOD AND A.DOCDATE <= (SELECT AA.ENDT FROM MONTHLYPAYFRQ AA WHERE AA.PAYPERIOD= '" + Class.Users.PayPeriod + "') AND A.SHIFTCNT > 0 UNION ALL SELECT A.EMPID IDCARD,0 SHIFTCNT,A.SHIFTCNT*A.LEAVE ELEAVE,0 TAKEN,0 TAKEN1 FROM HDATTA A,MONTHLYPAYFRQ B,HRLELIGIBLE C WHERE A.PAYPERIOD = B.PAYPERIOD AND B.PAYPERIOD1 = C.PAYPERIOD AND A.DOCDATE <= (SELECT AA.ENDT FROM MONTHLYPAYFRQ AA WHERE AA.PAYPERIOD= '" + Class.Users.PayPeriod + "') AND A.SHIFTCNT > 0 AND B.FINYR = (SELECT AA.FINYR FROM MONTHLYPAYFRQ AA WHERE AA.PAYPERIOD= '" + Class.Users.PayPeriod + "') UNION ALL SELECT TO_NUMBER(A.IDNOCHK) IDCARD,0 SHIFTCNT,0 ELEAVE,(0-SUM(A.STKOPBAL)) TAKEN,(0-SUM(A.STKOPBAL)) TAKEN1 FROM HRLEAVEREGMAST A WHERE A.LRDATE BETWEEN ( SELECT AA.STDT FROM MONTHLYPAYFRQ AA WHERE AA.PAYPERIOD ='" + Class.Users.PayPeriod + "' ) AND (SELECT AA.ENDT FROM MONTHLYPAYFRQ AA WHERE AA.PAYPERIOD= '" + Class.Users.PayPeriod + "') AND A.LTYPE <> 'ENC' AND A.FINYEAR = (SELECT AA.FINYR FROM MONTHLYPAYFRQ AA WHERE AA.PAYPERIOD= '" + Class.Users.PayPeriod + "') GROUP BY A.IDNOCHK UNION ALL SELECT TO_NUMBER(A.IDNOCHK) IDCARD,0 SHIFTCNT,0 ELEAVE,0 TAKEN,(0-SUM(A.STKOPBAL)) TAKEN1 FROM HRLEAVEREGMAST A WHERE A.LRDATE < ( SELECT AA.STDT FROM MONTHLYPAYFRQ AA WHERE AA.PAYPERIOD ='" + Class.Users.PayPeriod + "' ) AND A.FINYEAR = (SELECT AA.FINYR FROM MONTHLYPAYFRQ AA WHERE AA.PAYPERIOD= '" + Class.Users.PayPeriod + "') GROUP BY A.IDNOCHK UNION ALL SELECT TO_NUMBER(A.IDNOCHK) IDCARD,0 SHIFTCNT,0 ELEAVE,0 TAKEN,(0-SUM(A.STKOPBAL)) TAKEN1 FROM HRLEAVEREGMAST A WHERE A.LRDATE = ( SELECT AA.STDT FROM MONTHLYPAYFRQ AA WHERE AA.PAYPERIOD ='" + Class.Users.PayPeriod + "' ) AND A.LTYPE = 'ENC' AND ( SELECT TRIM(TO_CHAR(AA.STDT,'DD/MM')) FROM MONTHLYPAYFRQ AA WHERE AA.PAYPERIOD ='" + Class.Users.PayPeriod + "') = '01/01'  AND A.FINYEAR = (SELECT AA.FINYR FROM MONTHLYPAYFRQ AA WHERE AA.PAYPERIOD= '" + Class.Users.PayPeriod + "')   GROUP BY A.IDNOCHK  UNION ALL SELECT TO_NUMBER(BB.IDCARD) IDCARD,A.OLHDCNT SHIFTCNT,0 ELEAVE,0 TAKEN,0 TAKEN1 FROM HRONDUTYDET A,HREMPLOYDETAILS BB, MONTHLYPAYFRQ B,HRLELIGIBLE C WHERE A.IDCARD = BB.HREMPLOYDETAILSID AND A.PAYPERIOD = B.PAYPERIOD AND B.PAYPERIOD1 = C.PAYPERIOD AND A.ODATE  <= (SELECT AA.ENDT FROM MONTHLYPAYFRQ AA WHERE AA.PAYPERIOD= '" + Class.Users.PayPeriod + "') UNION ALL SELECT TO_NUMBER(BB.IDCARD) IDCARD,0 SHIFTCNT,A.OLHDCNT*0.05 ELEAVE,0 TAKEN,0 TAKEN1 FROM HRONDUTYDET A,HREMPLOYDETAILS BB, MONTHLYPAYFRQ B,HRLELIGIBLE C WHERE A.IDCARD = BB.HREMPLOYDETAILSID AND A.PAYPERIOD = B.PAYPERIOD AND B.PAYPERIOD1 = C.PAYPERIOD AND A.ODATE  <= (SELECT AA.ENDT FROM MONTHLYPAYFRQ AA WHERE AA.PAYPERIOD= '" + Class.Users.PayPeriod + "') AND B.FINYR = (SELECT AA.FINYR FROM MONTHLYPAYFRQ AA WHERE AA.PAYPERIOD= '" + Class.Users.PayPeriod + "') ) A GROUP BY A.IDCARD HAVING ( SUM(A.SHIFTCNT) >= 240 AND SUM(A.ELEAVE) > 0 ) ) AA,HREMPLOYMAST BB,HREMPLOYDETAILS CC,BRANCHMAST DD,GTDEPTDESGMAST EE,HRDESIGNATIONMAST FF,HRBANDMAST GG,GTCOMPMAST HH WHERE AA.IDCARD = CC.IDCARD AND BB.HREMPLOYMASTID = CC.HREMPLOYMASTID AND BB.WORKNATURE = DD.BRANCHMASTID AND EE.GTDEPTDESGMASTID = CC.DEPTNAME AND FF.HRDESIGNATIONMASTID = CC.DESIGNATION AND GG.HRBANDMASTID = CC.BAND AND HH.GTCOMPMASTID = BB.COMPCODE )  YY ) YYY WHERE XX.IDCARDNO=YYY.IDCARD(+) ORDER BY XX.FNAME";
             
            }
                DataSet ds0 = Utility.ExecuteSelectQuery(sel0, "HREMPLOYMAST");
            dtprint1 = ds0.Tables["HREMPLOYMAST"];

            Class.Users.UserTime = 0;
            string sel2 = "SELECT A.EMPNAME,A.IDCARDNO, A.signature,(SELECT AA.signature FROM ASPTBLEMP AA WHERE AA.ACTIVE='T') as msignature,to_char(c.doj,'dd-MM-YYYY') as dateofjoin,a.BYTESNAME as nameintamil,a.BYTESDESIGN as designintamil   FROM ASPTBLEMP A  JOIN GTCOMPMAST B ON A.COMPCODE=B.GTCOMPMASTID   join HREMPLOYDETAILS c on C.IDCARD=A.IDCARDNO WHERE A.IDCARDNO ='" + Class.Users.IDCARDNO + "' and  B.COMPCODE = '" + Class.Users.HCompcode + "' ";// where  A.FINYEAR='" + combofinyear.Text + "' AND  D.compcode='" + combocompcode.Text + "'  and a.payperiod='" + Class.Users.PayPeriod + "' AND  C.IDCARD='" + s[1].Substring(7).TrimEnd() + "' order by 1";
            DataSet ds2 = Utility.ExecuteSelectQuery(sel2, "ASPTBLEMP");
            dtprint2 = ds2.Tables["ASPTBLEMP"];

            string sel3 = "SELECT A.FROMDATE||' - '||A.TODATE PERIOD,A.BUYER OTMIN,ROUND(((((BASIC+DA+SPL)/26)/8)/60)*2*A.BUYER,2) OTAMT,  CASE WHEN B.ESI = 'YES' THEN ROUND(ROUND(((((BASIC+DA+SPL)/26)/8)/60)*2*A.BUYER,2)*0.75/100,2) ELSE 0 END ESI,  ROUND(((((BASIC+DA+SPL)/26)/8)/60)*2*A.BUYER,2) - CASE WHEN B.ESI = 'YES' THEN ROUND(ROUND(((((BASIC+DA+SPL)/26)/8)/60)*2*A.BUYER,2)*0.75/100,2) ELSE 0 END NETAMOUNT FROM OTSTAMENT A JOIN HREMPLOYDETAILS B ON A.EMPMAID = B.HREMPLOYMASTID WHERE B.IDACTIVE = 'YES'  AND  A.PAYPERIOD ='" + Class.Users.PayPeriod + "' AND B.IDCARD= '" + Class.Users.IDCARDNO + "' AND A.BUYER > 0  ORDER BY A.FROMDATE";
            DataSet ds3 = Utility.ExecuteSelectQuery(sel3, "OTSTAMENT");
            dtprint3 = ds3.Tables["OTSTAMENT"];

            if (dtprint1.Rows.Count > 0)
            {
                crystalReportViewer1.ReportSource = null; crystalReportViewer1.Refresh();
                CrystalDecisions.CrystalReports.Engine.ReportDocument reportdocument = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                reportdocument.Load(Application.StartupPath + "\\Report\\Lovely\\LovelySalarySlip1.rpt");
                reportdocument.Database.Tables["DataTable1"].SetDataSource(dtprint1);               
                reportdocument.Database.Tables["DataTable2"].SetDataSource(dtprint2);
                reportdocument.Database.Tables["DataTable3"].SetDataSource(dtprint3);
                crystalReportViewer1.ReportSource = reportdocument;
                if (!Directory.Exists(folderLocation)) { Directory.CreateDirectory(folderLocation); }
                reportdocument.ExportToDisk(ExportFormatType.PortableDocFormat, folderLocation + "-" + dtprint1.Rows[0]["FNAME"].ToString() + "-" + dtprint1.Rows[0]["PAYPERIOD"].ToString() + " PaySlip.pdf");
                reportdocument.ExportToDisk(ExportFormatType.Excel, folderLocation + "-" + dtprint1.Rows[0]["FNAME"].ToString() + "-" + dtprint1.Rows[0]["PAYPERIOD"].ToString() + " PaySlip.xls");
                datasave();
                reportdocument.PrintOptions.PrinterName = printDialog1.PrinterSettings.PrinterName;
                 reportdocument.PrintToPrinter(printDialog1.PrinterSettings.Copies, printDialog1.PrinterSettings.Collate, printDialog1.PrinterSettings.FromPage, printDialog1.PrinterSettings.ToPage);

                if (bIsConnected == true)
                {
                    int idwErrorCode = 0;
                    int iDataFlag = 5;
                    axCZKEM1.EnableDevice(iMachineNumber, false);//disable the device
                    if (axCZKEM1.ClearGLog(iMachineNumber))
                    {
                        axCZKEM1.RefreshData(iMachineNumber);//the data in the device should be refreshed
                    }
                    else
                    {
                        axCZKEM1.GetLastError(ref idwErrorCode);
                        MessageBox.Show("Operation failed,ErrorCode=" + idwErrorCode.ToString(), "Error");
                    }
                    axCZKEM1.EnableDevice(iMachineNumber, true);//enable the device
                }

            }
        }


        private void EmpSalarySlip_Load(object sender, EventArgs e)
        {
            try
            {
               
                Class.Users.UserTime = 0;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message.ToString());
                Cursor = Cursors.Default;
            }
        }

    

        private void combocompcode_SelectedIndexChanged(object sender, EventArgs e)
        {
        
        }

        private void combofinyear_SelectedIndexChanged(object sender, EventArgs e)
        {
          
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if(tabControl1.SelectedTab.Text==tabPage1.Text)
            //{
            //    flowLayoutPanel1.Controls.Clear();
            //    flowcontrolbind(combofinyear.Text, combocompcode.Text);
            //}
            //else
            //{
               
            //}
        }
        
      
        public void GridLoad()
        {
           // Finyearload();
        }
        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            companyload(); Finyearload(); flowLayoutPanel1.Controls.Clear();
            flowcontrolbind(combofinyear.Text, combocompcode.Text);

        }

        private void refreshToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            companyload(); Finyearload(); flowLayoutPanel1.Controls.Clear();
            flowcontrolbind(combofinyear.Text, combocompcode.Text);
        }

        private void Prints_Click(object sender, EventArgs e)
        {
           
        }

        private void datasave()
        {
            string ins = "insert into ASPTBLPRINT(FINYEAR,COMPCODE,PAYPERIOD,IDCARDNO,EMPNAME,IPADDRESS,CREATEDON,CREATEDBY,MODIFIEDON,DATETIME)values('" + Class.Users.Finyear + "','" + Class.Users.COMPCODE + "','" + Class.Users.PayPeriod + "','" + Class.Users.IDCARDNO + "','" + Class.Users.HostelName + "','" + Class.Users.IPADDRESS + "','" + System.DateTime.Now.ToString() + "','" + Class.Users.HUserName + "','" + System.DateTime.Now.ToString() + "',to_date('" + System.DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss") + "', 'dd-MM-yyyy HH:mi:ss'))";
            Utility.ExecuteNonQuery(ins);
        }

        public void Saves()
        {
           
        }

        public void Prints()
        {
            CrystalDecisions.CrystalReports.Engine.ReportDocument reportdocument = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            reportdocument.Load(Application.StartupPath + "\\Report\\Lovely\\SalarySlip.rpt");
            reportdocument.Database.Tables["DataTable1"].SetDataSource(dtprint1);
            reportdocument.Database.Tables["DataTable3"].SetDataSource(dtprint2);
            reportdocument.Database.Tables["DataTable2"].SetDataSource(dtprint3);
            crystalReportViewer1.ReportSource = reportdocument;
            if (!Directory.Exists(folderLocation)) { Directory.CreateDirectory(folderLocation); }
            reportdocument.ExportToDisk(ExportFormatType.PortableDocFormat, folderLocation + "-" + dtprint1.Rows[0]["FNAME"].ToString() + "-" + dtprint1.Rows[0]["PAYPERIOD"].ToString() + " PaySlip.pdf");
            reportdocument.PrintOptions.PrinterName = printDialog1.PrinterSettings.PrinterName;
            reportdocument.PrintToPrinter(printDialog1.PrinterSettings.Copies, printDialog1.PrinterSettings.Collate, printDialog1.PrinterSettings.FromPage, printDialog1.PrinterSettings.ToPage);
            datasave();
            dtprint1 = null;
            dtprint2 = null; dtprint3 = null;

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
            GlobalVariables.MdiPanel.Show();
            News();
            GlobalVariables.HeaderName.Text = "";
            GlobalVariables.TabCtrl.TabPages.RemoveAt(GlobalVariables.TabCtrl.SelectedIndex);
        }

        public void Searchs(int id)
        {
            throw new NotImplementedException();
        }

        public void Deletes(int id)
        {
            throw new NotImplementedException();
        }

        public void ReadOnlys()
        {
            throw new NotImplementedException();
        }

        private void checkspecified_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
