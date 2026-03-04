using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace Pinnacle.Transactions.Lovely
{
    public partial class ALLSalarySlip : Form,ToolStripAccess
    {
        private static ALLSalarySlip _instance;
        public static ALLSalarySlip Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ALLSalarySlip();
                return _instance;
            }
        }
        public ALLSalarySlip()
        {
            InitializeComponent();
            Class.Users.IPADDRESS = GenFun.GetLocalIPAddress();
            Class.Users.CREATED = Convert.ToDateTime(System.DateTime.Now.ToString("dd-MMM-yyyy") + " " + System.DateTime.Now.ToLongTimeString());
            Class.Users.SysDate = Convert.ToString(System.DateTime.Now.ToString("dd-MM-yyyy"));
            Class.Users.SysTime = Convert.ToString(DateTime.Now.ToShortTimeString().ToString());
            GlobalVariables.HeaderName.Text = Class.Users.ScreenName;
            GlobalVariables.CurrentForm = this;
          

        }


        Models.Master mas = new Models.Master();
        ListView allip = new ListView();
        Models.UserRights sm = new Models.UserRights(); ListView listfilter = new ListView(); ListView listfilter2 = new ListView();
        int i = 0;
       
        DataTable dtprint0 = new DataTable();
        DataTable dtprint1 = new DataTable();
        DataTable dtprint2 = new DataTable();
        DataTable dtprint3 = new DataTable();
        DataTable dtgeneral = new DataTable();
        DataTable reversedDt = new DataTable();
        DataTable reversedDt1 = new DataTable();
        DataTable reversedDt2 = new DataTable();
        DataTable reversedDt3 = new DataTable();
        public void News()
        {

            crystalReportViewer1.ReportSource = null; crystalReportViewer1.Refresh();
            companyload1(); empty();
            txtsearch1.Select();
            combopayperiod.SelectedIndex = -1; combopayperiod.Text = "";
            comboband.SelectedIndex = -1; comboband.Text = ""; combotypename.SelectedIndex = -1; combotypename.Text = "";
            combogender.SelectedIndex = -1; combogender.Text = ""; combopayperiod.SelectedIndex = -1; combopayperiod.Text = ""; combobranch.SelectedIndex = -1; combobranch.Text = "";
            panel5.BackColor = Class.Users.BackColors;
            panel6.BackColor = Class.Users.BackColors;
            listView2.Font = Class.Users.FontName;          
            this.BackColor = Class.Users.BackColors;
            this.Font = Class.Users.FontName;
            lblprogross4.BackColor = Class.Users.BackColors;
            lblprogross4.Font = Class.Users.FontName;
        }

        private void ALLSalarySlip_Load(object sender, EventArgs e)
        {
           
            companyload1(); bandload1(); typenameload1(); genderoand(); paypreiodoand();
         // fromdatedateTimePicker1.Value = System.DateTime.Now.AddDays(-1);
            txtsearch1.Select();combostatement.SelectedIndex = 0;
        }
        public void GridLoad()
        {
            i = 1;
            if (combopayperiod.Text != "")
            {
                string sel1 = "";
                listView2.Items.Clear(); listfilter2.Items.Clear(); allip.Items.Clear();
                if (Class.Users.HUserName == "HO")
                {
                    sel1 = "SELECT aa.HREMPLOYMASTID, AA.IDCARD,B.OIDNO as MIDCARD,C.FNAME,'" + Class.Users.HCompcode + "' COMPCODE, aa.PAYPERIOD ,aa.BRANCH ,aa.BANDid,aa.GENDER,aa.TYPENAME FROM ( SELECT aa.HREMPLOYMASTID, AA.BRANCH,AA.BANDID,AA.GENDER,AA.IDCARD,AA.EMPNAME,AA.MACHINENAME,     AA.BASICPAY,AA.ACTWORKDAYS,AA.SALRYDAYS,AA.EBASIC,AA.EDA,AA.EHRA,ROUND(AA.EGROSS,2) EGROSS,AA.ESI,AA.PF,AA.TDSN,AA.MESSFEES, AA.OTHDED,AA.TOTDED,AA.BANK,AA.NETPAY,AA.SIGNATURE,AA.SADV,AA.LOAN,AA.COMPNAME, DENSE_RANK() OVER (PARTITION BY AA.GENDER,AA.BRANCH,AA.BANDID ORDER BY AA.EMPNAME) SNO,AA.ETDED,AA.ECDED,AA.STDT,AA.ENDT,aa.PAYPERIOD,aA.PAYTYPE,'" + combotypename.Text + "' TYPENAME FROM ( SELECT C.HREMPLOYMASTID, A.BRANCH,D.BANDID,C.GENDER,TO_NUMBER(B.IDCARD) IDCARD,C.FNAME EMPNAME,G.DESIGNATION MACHINENAME, A.BASIC*2 BASICPAY,F.WEEKLYHOLIDAYS+A.WDAYS ACTWORKDAYS,A.WDAYS SALRYDAYS,A.EBASIC,A.EDA,A.ESPL EHRA,A.EGROSS,A.ESI,A.PF,A.TDSN,A.MESSFEES, A.EOTHDED OTHDED,A.TOTDED,A.BANK,A.NETPAY,'' SIGNATURE,A.SADV,A.LOAN,H.COMPNAME,A.ETDED,A.ECDED, TO_CHAR(F.STDT,'dd/mm/yyyy') STDT,TO_CHAR(F.ENDT,'dd/mm/yyyy') ENDT,F.PAYPERIOD,A.PAYTYPE FROM LOPPLHPAYROLL A,HREMPLOYDETAILS B,HREMPLOYMAST C,HRBANDMAST D,MACHINEMAST E,MONTHLYPAYFRQ F,HRDESIGNATIONMAST G,GTCOMPMAST H WHERE A.EMPID = B.HREMPLOYMASTID AND B.HREMPLOYMASTID = C.HREMPLOYMASTID AND H.GTCOMPMASTID=C.COMPCODE AND B.BAND = D.HRBANDMASTID AND E.MACHINEMASTID = B.MACNAME AND F.PAYPERIOD = A.PAYPERIOD AND G.HRDESIGNATIONMASTID = B.DESIGNATION AND A.PCTYPE = 'ACTUAL' AND A.PAYPERIOD ='" + combopayperiod.Text + "'  AND (A.BRANCH ='" + combobranch.Text + "'  OR 'ALL' ='" + combobranch.Text + "' ) AND (D.BANDID ='" + comboband.Text + "' OR 'ALL' ='" + comboband.Text + "' ) AND (C.GENDER ='" + combogender.Text + "'  OR 'ALL' ='" + combogender.Text + "' ) AND A.BANK NOT IN ('BY CASH','CASH') AND ('" + combotypename.Text + "'= 'SALARY BANK-STATEMENT' OR 'ALL' = '" + combotypename.Text + "'  OR 'SALARY PERMANENT-BANK & CASH STATEMENT' ='" + combotypename.Text + "' ) AND (A.NETPAY > 0 OR A.WDAYS > 0 ) UNION ALL SELECT C.HREMPLOYMASTID,  A.BRANCH,D.BANDID,C.GENDER,TO_NUMBER(B.IDCARD) IDCARD,C.FNAME EMPNAME,G.DESIGNATION MACHINENAME, A.BASIC*2 BASICPAY,F.WEEKLYHOLIDAYS+A.WDAYS ACTWORKDAYS,A.WDAYS SALRYDAYS,A.EBASIC,A.EDA,A.ESPL EHRA,A.EGROSS,A.ESI,A.PF,A.TDSN,A.MESSFEES, A.EOTHDED OTHDED,A.TOTDED,A.BANK,A.NETPAY,'' SIGNATURE,A.SADV,A.LOAN,H.COMPNAME,A.ETDED,A.ECDED, TO_CHAR(F.STDT,'dd/mm/yyyy') STDT,TO_CHAR(F.ENDT,'dd/mm/yyyy') ENDT,F.PAYPERIOD,A.PAYTYPE FROM LOPPLHPAYROLL A,HREMPLOYDETAILS B,HREMPLOYMAST C,HRBANDMAST D,MACHINEMAST E,MONTHLYPAYFRQ F ,HRDESIGNATIONMAST G,GTCOMPMAST H WHERE A.EMPID = B.HREMPLOYMASTID AND B.HREMPLOYMASTID = C.HREMPLOYMASTID AND H.GTCOMPMASTID=C.COMPCODE AND B.BAND = D.HRBANDMASTID AND E.MACHINEMASTID = B.MACNAME AND F.PAYPERIOD = A.PAYPERIOD AND G.HRDESIGNATIONMASTID = B.DESIGNATION AND A.PCTYPE = 'ACTUAL' AND A.PAYPERIOD ='" + combopayperiod.Text + "'  AND (A.BRANCH ='" + combobranch.Text + "'  OR 'ALL' ='" + combobranch.Text + "' ) AND (D.BANDID ='" + comboband.Text + "'  OR 'ALL' ='" + comboband.Text + "' ) AND (C.GENDER ='" + combogender.Text + "'  OR 'ALL' ='" + combogender.Text + "' ) AND A.BANK IN ('BY CASH','CASH') AND ('" + combotypename.Text + "' = 'SALARY PERMANENT-CASH STATEMENT' OR 'ALL' ='" + combotypename.Text + "'  OR 'SALARY PERMANENT-BANK & CASH STATEMENT' ='" + combotypename.Text + "' ) AND C.EMPTYPE = 'Permanent' AND (A.NETPAY > 0 OR A.WDAYS > 0 ) UNION ALL SELECT C.HREMPLOYMASTID, A.BRANCH,D.BANDID,C.GENDER,TO_NUMBER(B.IDCARD) IDCARD,C.FNAME EMPNAME,G.DESIGNATION MACHINENAME, A.BASIC*2 BASICPAY,F.WEEKLYHOLIDAYS+A.WDAYS ACTWORKDAYS,A.WDAYS SALRYDAYS,A.EBASIC,A.EDA,A.ESPL EHRA,A.EGROSS,A.ESI,A.PF,A.TDSN,A.MESSFEES, A.EOTHDED OTHDED,A.TOTDED,A.BANK,A.NETPAY,'' SIGNATURE,A.SADV,A.LOAN,H.COMPNAME,A.ETDED,A.ECDED, TO_CHAR(F.STDT,'dd/mm/yyyy') STDT,TO_CHAR(F.ENDT,'dd/mm/yyyy') ENDT,F.PAYPERIOD,A.PAYTYPE FROM LOPPLHPAYROLL A,HREMPLOYDETAILS B,HREMPLOYMAST C,HRBANDMAST D,MACHINEMAST E,MONTHLYPAYFRQ F ,HRDESIGNATIONMAST G,GTCOMPMAST H WHERE A.EMPID = B.HREMPLOYMASTID AND B.HREMPLOYMASTID = C.HREMPLOYMASTID AND H.GTCOMPMASTID=C.COMPCODE AND B.BAND = D.HRBANDMASTID AND E.MACHINEMASTID = B.MACNAME AND F.PAYPERIOD = A.PAYPERIOD AND G.HRDESIGNATIONMASTID = B.DESIGNATION AND A.PCTYPE = 'ACTUAL' AND A.PAYPERIOD = '" + combopayperiod.Text + "'  AND (A.BRANCH ='" + combobranch.Text + "'  OR 'ALL' ='" + combobranch.Text + "' ) AND (D.BANDID ='" + comboband.Text + "'  OR 'ALL' ='" + comboband.Text + "' ) AND (C.GENDER ='" + combogender.Text + "'  OR 'ALL' ='" + combogender.Text + "' )  AND A.BANK IN ('BY CASH','CASH') AND ('" + combotypename.Text + "' = 'SALARY TRAINEE-STATEMENT' OR 'ALL' ='" + combotypename.Text + "' ) AND C.EMPTYPE = 'Trainee' AND (A.NETPAY > 0 OR A.WDAYS > 0 ) ORDER BY 1,2,3,5 ) AA ) AA JOIN HREMPLOYDETAILS B ON AA.IDCARD = B.IDCARD JOIN HREMPLOYMAST C ON C.HREMPLOYMASTID = B.HREMPLOYMASTID  ORDER BY AA.IDCARD";

                }
                if (Class.Users.HUserName != "HO")
                {
                    // string sel1 = "SELECT aa.HREMPLOYMASTID, AA.IDCARD,B.OIDNO as MIDCARD,C.FNAME,'" + Class.Users.HCompcode + "' COMPCODE, aa.PAYPERIOD ,aa.BRANCH ,aa.BANDid,aa.GENDER,aa.TYPENAME FROM ( SELECT aa.HREMPLOYMASTID, AA.BRANCH,AA.BANDID,AA.GENDER,AA.IDCARD,AA.EMPNAME,AA.MACHINENAME,     AA.BASICPAY,AA.ACTWORKDAYS,AA.SALRYDAYS,AA.EBASIC,AA.EDA,AA.EHRA,ROUND(AA.EGROSS,2) EGROSS,AA.ESI,AA.PF,AA.TDSN,AA.MESSFEES, AA.OTHDED,AA.TOTDED,AA.BANK,AA.NETPAY,AA.SIGNATURE,AA.SADV,AA.LOAN,AA.COMPNAME, DENSE_RANK() OVER (PARTITION BY AA.GENDER,AA.BRANCH,AA.BANDID ORDER BY AA.EMPNAME) SNO,AA.ETDED,AA.ECDED,AA.STDT,AA.ENDT,aa.PAYPERIOD,aA.PAYTYPE,'" + combotypename.Text + "' TYPENAME FROM ( SELECT C.HREMPLOYMASTID, A.BRANCH,D.BANDID,C.GENDER,TO_NUMBER(B.IDCARD) IDCARD,C.FNAME EMPNAME,G.DESIGNATION MACHINENAME, A.BASIC*2 BASICPAY,F.WEEKLYHOLIDAYS+A.WDAYS ACTWORKDAYS,A.WDAYS SALRYDAYS,A.EBASIC,A.EDA,A.ESPL EHRA,A.EGROSS,A.ESI,A.PF,A.TDSN,A.MESSFEES, A.EOTHDED OTHDED,A.TOTDED,A.BANK,A.NETPAY,'' SIGNATURE,A.SADV,A.LOAN,H.COMPNAME,A.ETDED,A.ECDED, TO_CHAR(F.STDT,'dd/mm/yyyy') STDT,TO_CHAR(F.ENDT,'dd/mm/yyyy') ENDT,F.PAYPERIOD,A.PAYTYPE FROM LOPPLHPAYROLL A,HREMPLOYDETAILS B,HREMPLOYMAST C,HRBANDMAST D,MACHINEMAST E,MONTHLYPAYFRQ F,HRDESIGNATIONMAST G,GTCOMPMAST H WHERE A.EMPID = B.HREMPLOYMASTID AND B.HREMPLOYMASTID = C.HREMPLOYMASTID AND H.GTCOMPMASTID=C.COMPCODE AND B.BAND = D.HRBANDMASTID AND E.MACHINEMASTID = B.MACNAME AND F.PAYPERIOD = A.PAYPERIOD AND G.HRDESIGNATIONMASTID = B.DESIGNATION AND A.PCTYPE = 'ACTUAL' AND A.PAYPERIOD ='" + combopayperiod.Text + "'  AND (A.BRANCH ='" + combobranch.Text + "'  OR 'ALL' ='" + combobranch.Text + "' ) AND (D.BANDID ='" + comboband.Text + "' OR 'ALL' ='" + comboband.Text + "' ) AND (C.GENDER ='" + combogender.Text + "'  OR 'ALL' ='" + combogender.Text + "' ) AND A.BANK NOT IN ('BY CASH','CASH') AND ('" + combotypename.Text + "'= 'SALARY BANK-STATEMENT' OR 'ALL' = '" + combotypename.Text + "'  OR 'SALARY PERMANENT-BANK & CASH STATEMENT' ='" + combotypename.Text + "' ) AND (A.NETPAY > 0 OR A.WDAYS > 0 ) UNION ALL SELECT C.HREMPLOYMASTID,  A.BRANCH,D.BANDID,C.GENDER,TO_NUMBER(B.IDCARD) IDCARD,C.FNAME EMPNAME,G.DESIGNATION MACHINENAME, A.BASIC*2 BASICPAY,F.WEEKLYHOLIDAYS+A.WDAYS ACTWORKDAYS,A.WDAYS SALRYDAYS,A.EBASIC,A.EDA,A.ESPL EHRA,A.EGROSS,A.ESI,A.PF,A.TDSN,A.MESSFEES, A.EOTHDED OTHDED,A.TOTDED,A.BANK,A.NETPAY,'' SIGNATURE,A.SADV,A.LOAN,H.COMPNAME,A.ETDED,A.ECDED, TO_CHAR(F.STDT,'dd/mm/yyyy') STDT,TO_CHAR(F.ENDT,'dd/mm/yyyy') ENDT,F.PAYPERIOD,A.PAYTYPE FROM LOPPLHPAYROLL A,HREMPLOYDETAILS B,HREMPLOYMAST C,HRBANDMAST D,MACHINEMAST E,MONTHLYPAYFRQ F ,HRDESIGNATIONMAST G,GTCOMPMAST H WHERE A.EMPID = B.HREMPLOYMASTID AND B.HREMPLOYMASTID = C.HREMPLOYMASTID AND H.GTCOMPMASTID=C.COMPCODE AND B.BAND = D.HRBANDMASTID AND E.MACHINEMASTID = B.MACNAME AND F.PAYPERIOD = A.PAYPERIOD AND G.HRDESIGNATIONMASTID = B.DESIGNATION AND A.PCTYPE = 'ACTUAL' AND A.PAYPERIOD ='" + combopayperiod.Text + "'  AND (A.BRANCH ='" + combobranch.Text + "'  OR 'ALL' ='" + combobranch.Text + "' ) AND (D.BANDID ='" + comboband.Text + "'  OR 'ALL' ='" + comboband.Text + "' ) AND (C.GENDER ='" + combogender.Text + "'  OR 'ALL' ='" + combogender.Text + "' ) AND A.BANK IN ('BY CASH','CASH') AND ('" + combotypename.Text + "' = 'SALARY PERMANENT-CASH STATEMENT' OR 'ALL' ='" + combotypename.Text + "'  OR 'SALARY PERMANENT-BANK & CASH STATEMENT' ='" + combotypename.Text + "' ) AND C.EMPTYPE = 'Permanent' AND (A.NETPAY > 0 OR A.WDAYS > 0 ) UNION ALL SELECT C.HREMPLOYMASTID, A.BRANCH,D.BANDID,C.GENDER,TO_NUMBER(B.IDCARD) IDCARD,C.FNAME EMPNAME,G.DESIGNATION MACHINENAME, A.BASIC*2 BASICPAY,F.WEEKLYHOLIDAYS+A.WDAYS ACTWORKDAYS,A.WDAYS SALRYDAYS,A.EBASIC,A.EDA,A.ESPL EHRA,A.EGROSS,A.ESI,A.PF,A.TDSN,A.MESSFEES, A.EOTHDED OTHDED,A.TOTDED,A.BANK,A.NETPAY,'' SIGNATURE,A.SADV,A.LOAN,H.COMPNAME,A.ETDED,A.ECDED, TO_CHAR(F.STDT,'dd/mm/yyyy') STDT,TO_CHAR(F.ENDT,'dd/mm/yyyy') ENDT,F.PAYPERIOD,A.PAYTYPE FROM LOPPLHPAYROLL A,HREMPLOYDETAILS B,HREMPLOYMAST C,HRBANDMAST D,MACHINEMAST E,MONTHLYPAYFRQ F ,HRDESIGNATIONMAST G,GTCOMPMAST H WHERE A.EMPID = B.HREMPLOYMASTID AND B.HREMPLOYMASTID = C.HREMPLOYMASTID AND H.GTCOMPMASTID=C.COMPCODE AND B.BAND = D.HRBANDMASTID AND E.MACHINEMASTID = B.MACNAME AND F.PAYPERIOD = A.PAYPERIOD AND G.HRDESIGNATIONMASTID = B.DESIGNATION AND A.PCTYPE = 'ACTUAL' AND A.PAYPERIOD = '" + combopayperiod.Text + "'  AND (A.BRANCH ='" + combobranch.Text + "'  OR 'ALL' ='" + combobranch.Text + "' ) AND (D.BANDID ='" + comboband.Text + "'  OR 'ALL' ='" + comboband.Text + "' ) AND (C.GENDER ='" + combogender.Text + "'  OR 'ALL' ='" + combogender.Text + "' )  AND A.BANK IN ('BY CASH','CASH') AND ('" + combotypename.Text + "' = 'SALARY TRAINEE-STATEMENT' OR 'ALL' ='" + combotypename.Text + "' ) AND C.EMPTYPE = 'Trainee' AND (A.NETPAY > 0 OR A.WDAYS > 0 ) ORDER BY 1,2,3,5 ) AA ) AA JOIN HREMPLOYDETAILS B ON AA.IDCARD = B.IDCARD JOIN HREMPLOYMAST C ON C.HREMPLOYMASTID = B.HREMPLOYMASTID  ORDER BY AA.IDCARD";
                    sel1 = "SELECT aa.HREMPLOYMASTID, AA.IDCARD,B.OIDNO as MIDCARD,C.FNAME,'" + Class.Users.HCompcode + "' COMPCODE, aa.PAYPERIOD ,aa.BRANCH ,aa.BANDid,aa.GENDER,aa.TYPENAME FROM ( SELECT aa.HREMPLOYMASTID, AA.BRANCH,AA.BANDID,AA.GENDER,AA.IDCARD,AA.EMPNAME,AA.MACHINENAME,     AA.BASICPAY,AA.ACTWORKDAYS,AA.SALRYDAYS,AA.EBASIC,AA.EDA,AA.EHRA,ROUND(AA.EGROSS,2) EGROSS,AA.ESI,AA.PF,AA.TDSN,AA.MESSFEES, AA.OTHDED,AA.TOTDED,AA.BANK,AA.NETPAY,AA.SIGNATURE,AA.SADV,AA.LOAN,AA.COMPNAME, DENSE_RANK() OVER (PARTITION BY AA.GENDER,AA.BRANCH,AA.BANDID ORDER BY AA.EMPNAME) SNO,AA.ETDED,AA.ECDED,AA.STDT,AA.ENDT,aa.PAYPERIOD,aA.PAYTYPE,'" + combotypename.Text + "' TYPENAME FROM ( SELECT C.HREMPLOYMASTID, A.BRANCH,D.BANDID,C.GENDER,TO_NUMBER(B.IDCARD) IDCARD,C.FNAME EMPNAME,G.DESIGNATION MACHINENAME, A.BASIC*2 BASICPAY,F.WEEKLYHOLIDAYS+A.WDAYS ACTWORKDAYS,A.WDAYS SALRYDAYS,A.EBASIC,A.EDA,A.ESPL EHRA,A.EGROSS,A.ESI,A.PF,A.TDSN,A.MESSFEES, A.EOTHDED OTHDED,A.TOTDED,A.BANK,A.NETPAY,'' SIGNATURE,A.SADV,A.LOAN,H.COMPNAME,A.ETDED,A.ECDED, TO_CHAR(F.STDT,'dd/mm/yyyy') STDT,TO_CHAR(F.ENDT,'dd/mm/yyyy') ENDT,F.PAYPERIOD,A.PAYTYPE FROM LOPPLHPAYROLL A,HREMPLOYDETAILS B,HREMPLOYMAST C,HRBANDMAST D,MACHINEMAST E,MONTHLYPAYFRQ F,HRDESIGNATIONMAST G,GTCOMPMAST H WHERE A.EMPID = B.HREMPLOYMASTID AND B.HREMPLOYMASTID = C.HREMPLOYMASTID AND H.GTCOMPMASTID=C.COMPCODE AND B.BAND = D.HRBANDMASTID AND E.MACHINEMASTID = B.MACNAME AND F.PAYPERIOD = A.PAYPERIOD AND G.HRDESIGNATIONMASTID = B.DESIGNATION AND A.PCTYPE = 'ACTUAL' AND A.PAYPERIOD ='" + combopayperiod.Text + "'  AND (A.BRANCH ='" + combobranch.Text + "'  OR 'ALL' ='" + combobranch.Text + "' ) AND (D.BANDID ='" + comboband.Text + "' OR 'ALL' ='" + comboband.Text + "' ) AND (C.GENDER ='" + combogender.Text + "'  OR 'ALL' ='" + combogender.Text + "' ) AND A.BANK NOT IN ('BY CASH','CASH') AND ('" + combotypename.Text + "'= 'SALARY BANK-STATEMENT' OR 'ALL' = '" + combotypename.Text + "'  OR 'SALARY PERMANENT-BANK & CASH STATEMENT' ='" + combotypename.Text + "' ) AND (A.NETPAY > 0 OR A.WDAYS > 0 ) UNION ALL SELECT C.HREMPLOYMASTID,  A.BRANCH,D.BANDID,C.GENDER,TO_NUMBER(B.IDCARD) IDCARD,C.FNAME EMPNAME,G.DESIGNATION MACHINENAME, A.BASIC*2 BASICPAY,F.WEEKLYHOLIDAYS+A.WDAYS ACTWORKDAYS,A.WDAYS SALRYDAYS,A.EBASIC,A.EDA,A.ESPL EHRA,A.EGROSS,A.ESI,A.PF,A.TDSN,A.MESSFEES, A.EOTHDED OTHDED,A.TOTDED,A.BANK,A.NETPAY,'' SIGNATURE,A.SADV,A.LOAN,H.COMPNAME,A.ETDED,A.ECDED, TO_CHAR(F.STDT,'dd/mm/yyyy') STDT,TO_CHAR(F.ENDT,'dd/mm/yyyy') ENDT,F.PAYPERIOD,A.PAYTYPE FROM LOPPLHPAYROLL A,HREMPLOYDETAILS B,HREMPLOYMAST C,HRBANDMAST D,MACHINEMAST E,MONTHLYPAYFRQ F ,HRDESIGNATIONMAST G,GTCOMPMAST H WHERE A.EMPID = B.HREMPLOYMASTID AND B.HREMPLOYMASTID = C.HREMPLOYMASTID AND H.GTCOMPMASTID=C.COMPCODE AND B.BAND = D.HRBANDMASTID AND E.MACHINEMASTID = B.MACNAME AND F.PAYPERIOD = A.PAYPERIOD AND G.HRDESIGNATIONMASTID = B.DESIGNATION AND A.PCTYPE = 'ACTUAL' AND A.PAYPERIOD ='" + combopayperiod.Text + "'  AND (A.BRANCH ='" + combobranch.Text + "'  OR 'ALL' ='" + combobranch.Text + "' ) AND (D.BANDID ='" + comboband.Text + "'  OR 'ALL' ='" + comboband.Text + "' ) AND (C.GENDER ='" + combogender.Text + "'  OR 'ALL' ='" + combogender.Text + "' ) AND A.BANK IN ('BY CASH','CASH') AND ('" + combotypename.Text + "' = 'SALARY PERMANENT-CASH STATEMENT' OR 'ALL' ='" + combotypename.Text + "'  OR 'SALARY PERMANENT-BANK & CASH STATEMENT' ='" + combotypename.Text + "' ) AND C.EMPTYPE = 'Permanent' AND (A.NETPAY > 0 OR A.WDAYS > 0 ) UNION ALL SELECT C.HREMPLOYMASTID, A.BRANCH,D.BANDID,C.GENDER,TO_NUMBER(B.IDCARD) IDCARD,C.FNAME EMPNAME,G.DESIGNATION MACHINENAME, A.BASIC*2 BASICPAY,F.WEEKLYHOLIDAYS+A.WDAYS ACTWORKDAYS,A.WDAYS SALRYDAYS,A.EBASIC,A.EDA,A.ESPL EHRA,A.EGROSS,A.ESI,A.PF,A.TDSN,A.MESSFEES, A.EOTHDED OTHDED,A.TOTDED,A.BANK,A.NETPAY,'' SIGNATURE,A.SADV,A.LOAN,H.COMPNAME,A.ETDED,A.ECDED, TO_CHAR(F.STDT,'dd/mm/yyyy') STDT,TO_CHAR(F.ENDT,'dd/mm/yyyy') ENDT,F.PAYPERIOD,A.PAYTYPE FROM LOPPLHPAYROLL A,HREMPLOYDETAILS B,HREMPLOYMAST C,HRBANDMAST D,MACHINEMAST E,MONTHLYPAYFRQ F ,HRDESIGNATIONMAST G,GTCOMPMAST H WHERE A.EMPID = B.HREMPLOYMASTID AND B.HREMPLOYMASTID = C.HREMPLOYMASTID AND H.GTCOMPMASTID=C.COMPCODE AND B.BAND = D.HRBANDMASTID AND E.MACHINEMASTID = B.MACNAME AND F.PAYPERIOD = A.PAYPERIOD AND G.HRDESIGNATIONMASTID = B.DESIGNATION AND A.PCTYPE = 'ACTUAL' AND A.PAYPERIOD = '" + combopayperiod.Text + "'  AND (A.BRANCH ='" + combobranch.Text + "'  OR 'ALL' ='" + combobranch.Text + "' ) AND (D.BANDID ='" + comboband.Text + "'  OR 'ALL' ='" + comboband.Text + "' ) AND (C.GENDER ='" + combogender.Text + "'  OR 'ALL' ='" + combogender.Text + "' )  AND A.BANK IN ('BY CASH','CASH') AND ('" + combotypename.Text + "' = 'SALARY TRAINEE-STATEMENT' OR 'ALL' ='" + combotypename.Text + "' ) AND C.EMPTYPE = 'Trainee' AND (A.NETPAY > 0 OR A.WDAYS > 0 ) ORDER BY 1,2,3,5 ) AA ) AA JOIN HREMPLOYDETAILS B ON AA.IDCARD = B.IDCARD JOIN HREMPLOYMAST C ON C.HREMPLOYMASTID = B.HREMPLOYMASTID AND C.LOCPLACE <> 1544713937669 ORDER BY AA.IDCARD";

                }
                if (Class.Users.HUserName == "GROUP4")
                {
                    // string sel1 = "SELECT aa.HREMPLOYMASTID, AA.IDCARD,B.OIDNO as MIDCARD,C.FNAME,'" + Class.Users.HCompcode + "' COMPCODE, aa.PAYPERIOD ,aa.BRANCH ,aa.BANDid,aa.GENDER,aa.TYPENAME FROM ( SELECT aa.HREMPLOYMASTID, AA.BRANCH,AA.BANDID,AA.GENDER,AA.IDCARD,AA.EMPNAME,AA.MACHINENAME,     AA.BASICPAY,AA.ACTWORKDAYS,AA.SALRYDAYS,AA.EBASIC,AA.EDA,AA.EHRA,ROUND(AA.EGROSS,2) EGROSS,AA.ESI,AA.PF,AA.TDSN,AA.MESSFEES, AA.OTHDED,AA.TOTDED,AA.BANK,AA.NETPAY,AA.SIGNATURE,AA.SADV,AA.LOAN,AA.COMPNAME, DENSE_RANK() OVER (PARTITION BY AA.GENDER,AA.BRANCH,AA.BANDID ORDER BY AA.EMPNAME) SNO,AA.ETDED,AA.ECDED,AA.STDT,AA.ENDT,aa.PAYPERIOD,aA.PAYTYPE,'" + combotypename.Text + "' TYPENAME FROM ( SELECT C.HREMPLOYMASTID, A.BRANCH,D.BANDID,C.GENDER,TO_NUMBER(B.IDCARD) IDCARD,C.FNAME EMPNAME,G.DESIGNATION MACHINENAME, A.BASIC*2 BASICPAY,F.WEEKLYHOLIDAYS+A.WDAYS ACTWORKDAYS,A.WDAYS SALRYDAYS,A.EBASIC,A.EDA,A.ESPL EHRA,A.EGROSS,A.ESI,A.PF,A.TDSN,A.MESSFEES, A.EOTHDED OTHDED,A.TOTDED,A.BANK,A.NETPAY,'' SIGNATURE,A.SADV,A.LOAN,H.COMPNAME,A.ETDED,A.ECDED, TO_CHAR(F.STDT,'dd/mm/yyyy') STDT,TO_CHAR(F.ENDT,'dd/mm/yyyy') ENDT,F.PAYPERIOD,A.PAYTYPE FROM LOPPLHPAYROLL A,HREMPLOYDETAILS B,HREMPLOYMAST C,HRBANDMAST D,MACHINEMAST E,MONTHLYPAYFRQ F,HRDESIGNATIONMAST G,GTCOMPMAST H WHERE A.EMPID = B.HREMPLOYMASTID AND B.HREMPLOYMASTID = C.HREMPLOYMASTID AND H.GTCOMPMASTID=C.COMPCODE AND B.BAND = D.HRBANDMASTID AND E.MACHINEMASTID = B.MACNAME AND F.PAYPERIOD = A.PAYPERIOD AND G.HRDESIGNATIONMASTID = B.DESIGNATION AND A.PCTYPE = 'ACTUAL' AND A.PAYPERIOD ='" + combopayperiod.Text + "'  AND (A.BRANCH ='" + combobranch.Text + "'  OR 'ALL' ='" + combobranch.Text + "' ) AND (D.BANDID ='" + comboband.Text + "' OR 'ALL' ='" + comboband.Text + "' ) AND (C.GENDER ='" + combogender.Text + "'  OR 'ALL' ='" + combogender.Text + "' ) AND A.BANK NOT IN ('BY CASH','CASH') AND ('" + combotypename.Text + "'= 'SALARY BANK-STATEMENT' OR 'ALL' = '" + combotypename.Text + "'  OR 'SALARY PERMANENT-BANK & CASH STATEMENT' ='" + combotypename.Text + "' ) AND (A.NETPAY > 0 OR A.WDAYS > 0 ) UNION ALL SELECT C.HREMPLOYMASTID,  A.BRANCH,D.BANDID,C.GENDER,TO_NUMBER(B.IDCARD) IDCARD,C.FNAME EMPNAME,G.DESIGNATION MACHINENAME, A.BASIC*2 BASICPAY,F.WEEKLYHOLIDAYS+A.WDAYS ACTWORKDAYS,A.WDAYS SALRYDAYS,A.EBASIC,A.EDA,A.ESPL EHRA,A.EGROSS,A.ESI,A.PF,A.TDSN,A.MESSFEES, A.EOTHDED OTHDED,A.TOTDED,A.BANK,A.NETPAY,'' SIGNATURE,A.SADV,A.LOAN,H.COMPNAME,A.ETDED,A.ECDED, TO_CHAR(F.STDT,'dd/mm/yyyy') STDT,TO_CHAR(F.ENDT,'dd/mm/yyyy') ENDT,F.PAYPERIOD,A.PAYTYPE FROM LOPPLHPAYROLL A,HREMPLOYDETAILS B,HREMPLOYMAST C,HRBANDMAST D,MACHINEMAST E,MONTHLYPAYFRQ F ,HRDESIGNATIONMAST G,GTCOMPMAST H WHERE A.EMPID = B.HREMPLOYMASTID AND B.HREMPLOYMASTID = C.HREMPLOYMASTID AND H.GTCOMPMASTID=C.COMPCODE AND B.BAND = D.HRBANDMASTID AND E.MACHINEMASTID = B.MACNAME AND F.PAYPERIOD = A.PAYPERIOD AND G.HRDESIGNATIONMASTID = B.DESIGNATION AND A.PCTYPE = 'ACTUAL' AND A.PAYPERIOD ='" + combopayperiod.Text + "'  AND (A.BRANCH ='" + combobranch.Text + "'  OR 'ALL' ='" + combobranch.Text + "' ) AND (D.BANDID ='" + comboband.Text + "'  OR 'ALL' ='" + comboband.Text + "' ) AND (C.GENDER ='" + combogender.Text + "'  OR 'ALL' ='" + combogender.Text + "' ) AND A.BANK IN ('BY CASH','CASH') AND ('" + combotypename.Text + "' = 'SALARY PERMANENT-CASH STATEMENT' OR 'ALL' ='" + combotypename.Text + "'  OR 'SALARY PERMANENT-BANK & CASH STATEMENT' ='" + combotypename.Text + "' ) AND C.EMPTYPE = 'Permanent' AND (A.NETPAY > 0 OR A.WDAYS > 0 ) UNION ALL SELECT C.HREMPLOYMASTID, A.BRANCH,D.BANDID,C.GENDER,TO_NUMBER(B.IDCARD) IDCARD,C.FNAME EMPNAME,G.DESIGNATION MACHINENAME, A.BASIC*2 BASICPAY,F.WEEKLYHOLIDAYS+A.WDAYS ACTWORKDAYS,A.WDAYS SALRYDAYS,A.EBASIC,A.EDA,A.ESPL EHRA,A.EGROSS,A.ESI,A.PF,A.TDSN,A.MESSFEES, A.EOTHDED OTHDED,A.TOTDED,A.BANK,A.NETPAY,'' SIGNATURE,A.SADV,A.LOAN,H.COMPNAME,A.ETDED,A.ECDED, TO_CHAR(F.STDT,'dd/mm/yyyy') STDT,TO_CHAR(F.ENDT,'dd/mm/yyyy') ENDT,F.PAYPERIOD,A.PAYTYPE FROM LOPPLHPAYROLL A,HREMPLOYDETAILS B,HREMPLOYMAST C,HRBANDMAST D,MACHINEMAST E,MONTHLYPAYFRQ F ,HRDESIGNATIONMAST G,GTCOMPMAST H WHERE A.EMPID = B.HREMPLOYMASTID AND B.HREMPLOYMASTID = C.HREMPLOYMASTID AND H.GTCOMPMASTID=C.COMPCODE AND B.BAND = D.HRBANDMASTID AND E.MACHINEMASTID = B.MACNAME AND F.PAYPERIOD = A.PAYPERIOD AND G.HRDESIGNATIONMASTID = B.DESIGNATION AND A.PCTYPE = 'ACTUAL' AND A.PAYPERIOD = '" + combopayperiod.Text + "'  AND (A.BRANCH ='" + combobranch.Text + "'  OR 'ALL' ='" + combobranch.Text + "' ) AND (D.BANDID ='" + comboband.Text + "'  OR 'ALL' ='" + comboband.Text + "' ) AND (C.GENDER ='" + combogender.Text + "'  OR 'ALL' ='" + combogender.Text + "' )  AND A.BANK IN ('BY CASH','CASH') AND ('" + combotypename.Text + "' = 'SALARY TRAINEE-STATEMENT' OR 'ALL' ='" + combotypename.Text + "' ) AND C.EMPTYPE = 'Trainee' AND (A.NETPAY > 0 OR A.WDAYS > 0 ) ORDER BY 1,2,3,5 ) AA ) AA JOIN HREMPLOYDETAILS B ON AA.IDCARD = B.IDCARD JOIN HREMPLOYMAST C ON C.HREMPLOYMASTID = B.HREMPLOYMASTID  ORDER BY AA.IDCARD";
                    sel1 = "SELECT aa.HREMPLOYMASTID, AA.IDCARD,B.OIDNO as MIDCARD,C.FNAME,'" + Class.Users.HCompcode + "' COMPCODE, aa.PAYPERIOD ,aa.BRANCH ,aa.BANDid,aa.GENDER,aa.TYPENAME FROM ( SELECT aa.HREMPLOYMASTID, AA.BRANCH,AA.BANDID,AA.GENDER,AA.IDCARD,AA.EMPNAME,AA.MACHINENAME,     AA.BASICPAY,AA.ACTWORKDAYS,AA.SALRYDAYS,AA.EBASIC,AA.EDA,AA.EHRA,ROUND(AA.EGROSS,2) EGROSS,AA.ESI,AA.PF,AA.TDSN,AA.MESSFEES, AA.OTHDED,AA.TOTDED,AA.BANK,AA.NETPAY,AA.SIGNATURE,AA.SADV,AA.LOAN,AA.COMPNAME, DENSE_RANK() OVER (PARTITION BY AA.GENDER,AA.BRANCH,AA.BANDID ORDER BY AA.EMPNAME) SNO,AA.ETDED,AA.ECDED,AA.STDT,AA.ENDT,aa.PAYPERIOD,aA.PAYTYPE,'" + combotypename.Text + "' TYPENAME FROM ( SELECT C.HREMPLOYMASTID, A.BRANCH,D.BANDID,C.GENDER,TO_NUMBER(B.IDCARD) IDCARD,C.FNAME EMPNAME,G.DESIGNATION MACHINENAME, A.BASIC*2 BASICPAY,F.WEEKLYHOLIDAYS+A.WDAYS ACTWORKDAYS,A.WDAYS SALRYDAYS,A.EBASIC,A.EDA,A.ESPL EHRA,A.EGROSS,A.ESI,A.PF,A.TDSN,A.MESSFEES, A.EOTHDED OTHDED,A.TOTDED,A.BANK,A.NETPAY,'' SIGNATURE,A.SADV,A.LOAN,H.COMPNAME,A.ETDED,A.ECDED, TO_CHAR(F.STDT,'dd/mm/yyyy') STDT,TO_CHAR(F.ENDT,'dd/mm/yyyy') ENDT,F.PAYPERIOD,A.PAYTYPE FROM LOPPLHPAYROLL A,HREMPLOYDETAILS B,HREMPLOYMAST C,HRBANDMAST D,MACHINEMAST E,MONTHLYPAYFRQ F,HRDESIGNATIONMAST G,GTCOMPMAST H WHERE A.EMPID = B.HREMPLOYMASTID AND B.HREMPLOYMASTID = C.HREMPLOYMASTID AND H.GTCOMPMASTID=C.COMPCODE AND B.BAND = D.HRBANDMASTID AND E.MACHINEMASTID = B.MACNAME AND F.PAYPERIOD = A.PAYPERIOD AND G.HRDESIGNATIONMASTID = B.DESIGNATION AND A.PCTYPE = 'ACTUAL' AND A.PAYPERIOD ='" + combopayperiod.Text + "'  AND (A.BRANCH ='" + combobranch.Text + "'  OR 'ALL' ='" + combobranch.Text + "' ) AND (D.BANDID ='" + comboband.Text + "' OR 'ALL' ='" + comboband.Text + "' ) AND (C.GENDER ='" + combogender.Text + "'  OR 'ALL' ='" + combogender.Text + "' ) AND A.BANK NOT IN ('BY CASH','CASH') AND ('" + combotypename.Text + "'= 'SALARY BANK-STATEMENT' OR 'ALL' = '" + combotypename.Text + "'  OR 'SALARY PERMANENT-BANK & CASH STATEMENT' ='" + combotypename.Text + "' ) AND (A.NETPAY > 0 OR A.WDAYS > 0 ) UNION ALL SELECT C.HREMPLOYMASTID,  A.BRANCH,D.BANDID,C.GENDER,TO_NUMBER(B.IDCARD) IDCARD,C.FNAME EMPNAME,G.DESIGNATION MACHINENAME, A.BASIC*2 BASICPAY,F.WEEKLYHOLIDAYS+A.WDAYS ACTWORKDAYS,A.WDAYS SALRYDAYS,A.EBASIC,A.EDA,A.ESPL EHRA,A.EGROSS,A.ESI,A.PF,A.TDSN,A.MESSFEES, A.EOTHDED OTHDED,A.TOTDED,A.BANK,A.NETPAY,'' SIGNATURE,A.SADV,A.LOAN,H.COMPNAME,A.ETDED,A.ECDED, TO_CHAR(F.STDT,'dd/mm/yyyy') STDT,TO_CHAR(F.ENDT,'dd/mm/yyyy') ENDT,F.PAYPERIOD,A.PAYTYPE FROM LOPPLHPAYROLL A,HREMPLOYDETAILS B,HREMPLOYMAST C,HRBANDMAST D,MACHINEMAST E,MONTHLYPAYFRQ F ,HRDESIGNATIONMAST G,GTCOMPMAST H WHERE A.EMPID = B.HREMPLOYMASTID AND B.HREMPLOYMASTID = C.HREMPLOYMASTID AND H.GTCOMPMASTID=C.COMPCODE AND B.BAND = D.HRBANDMASTID AND E.MACHINEMASTID = B.MACNAME AND F.PAYPERIOD = A.PAYPERIOD AND G.HRDESIGNATIONMASTID = B.DESIGNATION AND A.PCTYPE = 'ACTUAL' AND A.PAYPERIOD ='" + combopayperiod.Text + "'  AND (A.BRANCH ='" + combobranch.Text + "'  OR 'ALL' ='" + combobranch.Text + "' ) AND (D.BANDID ='" + comboband.Text + "'  OR 'ALL' ='" + comboband.Text + "' ) AND (C.GENDER ='" + combogender.Text + "'  OR 'ALL' ='" + combogender.Text + "' ) AND A.BANK IN ('BY CASH','CASH') AND ('" + combotypename.Text + "' = 'SALARY PERMANENT-CASH STATEMENT' OR 'ALL' ='" + combotypename.Text + "'  OR 'SALARY PERMANENT-BANK & CASH STATEMENT' ='" + combotypename.Text + "' ) AND C.EMPTYPE = 'Permanent' AND (A.NETPAY > 0 OR A.WDAYS > 0 ) UNION ALL SELECT C.HREMPLOYMASTID, A.BRANCH,D.BANDID,C.GENDER,TO_NUMBER(B.IDCARD) IDCARD,C.FNAME EMPNAME,G.DESIGNATION MACHINENAME, A.BASIC*2 BASICPAY,F.WEEKLYHOLIDAYS+A.WDAYS ACTWORKDAYS,A.WDAYS SALRYDAYS,A.EBASIC,A.EDA,A.ESPL EHRA,A.EGROSS,A.ESI,A.PF,A.TDSN,A.MESSFEES, A.EOTHDED OTHDED,A.TOTDED,A.BANK,A.NETPAY,'' SIGNATURE,A.SADV,A.LOAN,H.COMPNAME,A.ETDED,A.ECDED, TO_CHAR(F.STDT,'dd/mm/yyyy') STDT,TO_CHAR(F.ENDT,'dd/mm/yyyy') ENDT,F.PAYPERIOD,A.PAYTYPE FROM LOPPLHPAYROLL A,HREMPLOYDETAILS B,HREMPLOYMAST C,HRBANDMAST D,MACHINEMAST E,MONTHLYPAYFRQ F ,HRDESIGNATIONMAST G,GTCOMPMAST H WHERE A.EMPID = B.HREMPLOYMASTID AND B.HREMPLOYMASTID = C.HREMPLOYMASTID AND H.GTCOMPMASTID=C.COMPCODE AND B.BAND = D.HRBANDMASTID AND E.MACHINEMASTID = B.MACNAME AND F.PAYPERIOD = A.PAYPERIOD AND G.HRDESIGNATIONMASTID = B.DESIGNATION AND A.PCTYPE = 'ACTUAL' AND A.PAYPERIOD = '" + combopayperiod.Text + "'  AND (A.BRANCH ='" + combobranch.Text + "'  OR 'ALL' ='" + combobranch.Text + "' ) AND (D.BANDID ='" + comboband.Text + "'  OR 'ALL' ='" + comboband.Text + "' ) AND (C.GENDER ='" + combogender.Text + "'  OR 'ALL' ='" + combogender.Text + "' )  AND A.BANK IN ('BY CASH','CASH') AND ('" + combotypename.Text + "' = 'SALARY TRAINEE-STATEMENT' OR 'ALL' ='" + combotypename.Text + "' ) AND C.EMPTYPE = 'Trainee' AND (A.NETPAY > 0 OR A.WDAYS > 0 ) ORDER BY 1,2,3,5 ) AA ) AA JOIN HREMPLOYDETAILS B ON AA.IDCARD = B.IDCARD JOIN HREMPLOYMAST C ON C.HREMPLOYMASTID = B.HREMPLOYMASTID AND C.LOCPLACE <> 1544713937669 ORDER BY AA.IDCARD";

                }
                DataSet ds1 = Utility.ExecuteSelectQuery(sel1, "HREMPLOYMAST");
                DataTable dt1 = ds1.Tables["HREMPLOYMAST"];
                if (dt1.Rows.Count > 0)
                {
                    foreach (DataRow myRow in dt1.Rows)
                    {
                        ListViewItem list = new ListViewItem();


                        list.SubItems.Add(i.ToString());
                        list.SubItems.Add(myRow["HREMPLOYMASTID"].ToString());
                        list.SubItems.Add(myRow["IDCARD"].ToString());
                        list.SubItems.Add(myRow["MIDCARD"].ToString());
                        list.SubItems.Add(myRow["FNAME"].ToString());
                        list.SubItems.Add(myRow["COMPCODE"].ToString());
                        list.SubItems.Add(myRow["PAYPERIOD"].ToString());
                        if (i % 2 == 0)
                        {
                            list.BackColor = Color.White;

                        }
                        else
                        {
                            list.BackColor = Color.WhiteSmoke;

                        }
                        this.listfilter2.Items.Add((ListViewItem)list.Clone());
                        listView2.Items.Add(list); lbllistviewcount.Refresh();
                        lbllistviewcount.Text = "Total Count    : " + listView2.Items.Count;
                        i++;
                    }
                }
                else
                {
                    MessageBox.Show("No Data Found");
                }
            }
            else
            {
                MessageBox.Show("Pls Select Mandatory Fields");
            }
        }
        void empty()
        {
            txtsearch1.Text = "";

            if (dtprint1 != null || dtprint0 != null)
            {
               allip.Items.Clear(); 
                 dtprint0 = null; dtprint1 = null; dtprint2 = null; dtprint3 = null;
                lblprogross4.Visible = false;
                Cursor = Cursors.Default;chkall.Checked = false;
                txtsearch1.Select();

            }
            
        }



        public void companyload1()
        {
            try
            {
                string sel = "SELECT '' BRANCH FROM DUAL  union SELECT A.BRANCHNAME BRANCH FROM BRANCHMAST A UNION SELECT 'ALL' BRANCH FROM DUAL";
                DataSet ds1 = Utility.ExecuteSelectQuery(sel, "BRANCHMAST");
                DataTable dt1 = ds1.Tables["BRANCHMAST"];
                combobranch.DisplayMember = "BRANCH";
                combobranch.ValueMember = "BRANCH";
                combobranch.DataSource = dt1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("companyload1: " + ex.Message, " Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        
        public void bandload1()
        {
            try
            {
                string sel = "SELECT '' BANDID FROM DUAL  union SELECT A.BANDID FROM HRBANDMAST A WHERE A.MACTIVE = 'T' UNION SELECT 'ALL' BANDID FROM DUAL";
                DataSet ds1 = Utility.ExecuteSelectQuery(sel, "HRBANDMAST");
                DataTable dt1 = ds1.Tables["HRBANDMAST"];
                comboband.DisplayMember = "BANDID";
                comboband.ValueMember = "BANDID";
                comboband.DataSource = dt1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("companyload1: " + ex.Message, " Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        public void typenameload1()
        {
            try
            {
                string sel = "SELECT '' TYPE FROM DUAL  union SELECT 'SALARY BANK-STATEMENT' TYPE FROM DUAL UNION SELECT 'SALARY PERMANENT-CASH STATEMENT' TYPE FROM DUAL UNION SELECT 'SALARY PERMANENT-BANK & CASH STATEMENT' TYPE FROM DUAL UNION SELECT 'SALARY TRAINEE-STATEMENT' TYPE FROM DUAL UNION SELECT 'ALL' TYPE FROM DUAL";
                DataSet ds1 = Utility.ExecuteSelectQuery(sel, "DUAL");
                DataTable dt1 = ds1.Tables["DUAL"];
                combotypename.DisplayMember = "TYPE";
                combotypename.ValueMember = "TYPE";
                combotypename.DataSource = dt1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("companyload1: " + ex.Message, " Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        
        public void genderoand()
        {
            try
            {
                string sel = "SELECT '' GENDER FROM DUAL  union  SELECT 'MALE' GENDER FROM DUAL UNION SELECT 'FEMALE' GENDER FROM DUAL UNION SELECT 'ALL' GENDER FROM DUAL";
                DataSet ds1 = Utility.ExecuteSelectQuery(sel, "DUAL");
                DataTable dt1 = ds1.Tables["DUAL"];
                combogender.DisplayMember = "GENDER";
                combogender.ValueMember = "GENDER";
                combogender.DataSource = dt1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("companyload1: " + ex.Message, " Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        public void paypreiodoand()
        {
            try
            {
                string sel = "SELECT '' PAYPERIOD FROM DUAL  union SELECT A.PAYPERIOD PAYPERIOD FROM MONTHLYPAYFRQ  A UNION SELECT 'ALL' PAYPERIOD FROM DUAL";
                DataSet ds1 = Utility.ExecuteSelectQuery(sel, "MONTHLYPAYFRQ");
                DataTable dt1 = ds1.Tables["MONTHLYPAYFRQ"];
                combopayperiod.DisplayMember = "PAYPERIOD";
                combopayperiod.ValueMember = "PAYPERIOD";
                combopayperiod.DataSource = dt1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("companyload1: " + ex.Message, " Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        ReportDocument cryRpt = new ReportDocument();
        Report.Lovely.ALLPaySlip rd = new Report.Lovely.ALLPaySlip();
        Report.Lovely.LovelySalarySlip1 rd1 = new Report.Lovely.LovelySalarySlip1();
        string ORD = "";
        byte[] bytes;
       
        public void Pdfs()
        {
            
               
           
        }

        private void listView2_ItemActivate(object sender, EventArgs e)
        {
            Class.Users.UserTime = 0;
            if (combostatement.Text == "SALARYSLIP")
            {
                string sel0 = "";
                Class.Users.PayPeriod = ""; Class.Users.IDCARDNO = 0;
                Class.Users.PayPeriod = listView2.SelectedItems[0].SubItems[7].Text;
                Class.Users.IDCARDNO = Convert.ToInt32(listView2.SelectedItems[0].SubItems[3].Text);
                if (Class.Users.HUserName == "GROUP1" || Class.Users.HUserName == "GROUP2" || Class.Users.HUserName == "GROUP3" || Class.Users.HUserName == "GROUP4")
                {
                    sel0 = "SELECT XX.FINYEAR,XX.COMPCODE,XX.COMPNAME,XX.FNAME,XX.IDCARDNO,XX.BRANCH,XX.DESIGNATION,       XX.PAYPERIOD,XX.NOOFDAYS,XX.LOP,XX.EBASIC,XX.EHRA,XX.EDA,XX.ESPL,XX.ERPF,XX.ERESI,XX.EMESSFEES,XX.EOTHDED,XX.ADV,XX.SADV,       XX.BANK,XX.PAYCAT,     XX.IT,XX.EGROSS,XX.NETPAY,XX.BASIC,XX.DA,XX.HRA,XX.SPL,XX.TOTDED,XX.COMPLOGO,XX.DG,XX.PAYCAT1,NVL(YYY.TAKEN,0) TAKEN,     NVL(YYY.CLOSING,0) CLOSING,YYY.IDCARD,NVL(YYY.ELEAVE,0) ELEAVE,XX.EMPTYPE,XX.WALLO  TEST1 ,XX.EWALLO TEST2 ,XX.ETRDED TEST3  FROM (SELECT AA.FINYEAR,AA.COMPCODE,AA.COMPNAME,AA.FNAME,AA.IDCARDNO,AA.BRANCH,AA.DESIGNATION, AA.PAYPERIOD,AA.NOOFDAYS,AA.LOP,AA.EBASIC,AA.EHRA,AA.EDA,AA.ESPL,  AA.ERPF,AA.ERESI,AA.EMESSFEES,AA.EOTHDED,     AA.ADV,AA.SADV,AA.BANK,AA.PAYCAT,                AA.IT,AA.EGROSS,AA.NETPAY,AA.BASIC,AA.DA,AA.HRA,AA.SPL,AA.TOTDED,AA.COMPLOGO,  AA.DG,AA.PAYCAT1,AA.EMPTYPE ,AA.WALLO ,AA.EWALLO,AA.ETRDED  FROM ( SELECT TO_CHAR(TO_DATE(H.MPGDT,'DD/MM/YY'),'DD/MM/YYYY') AS FINYEAR,C.COMPCODE,C.COMPNAME,B.FNAME,B.IDCARDNO,  A.BRANCH,D.DESIGNATION,     A.PAYPERIOD,A.WDAYS NOOFDAYS,A.MDAYS-A.WDAYS LOP,A.EBASIC, A.EHRA,A.EDA,A.ESPL,A.PF ERPF,A.ESI ERESI,  A.MESSFEES EMESSFEES,(A.EOTHDED+ROUND((CASE WHEN A.ECDED > 0 THEN  ( A.WDAYS*(450/A.MDAYS) ) ELSE 0 END),2)+A.ETDED) EOTHDED,A.LOAN ADV,A.SADV,CASE WHEN A.BANK = 'CASH' THEN ' ' ELSE A.BANK END BANK,A.PAYCAT,A.TDSN IT,A.EGROSS, ROUND(A.NETPAY-ROUND((CASE WHEN A.ECDED > 0 THEN  ( A.WDAYS*(450/A.MDAYS) ) ELSE 0 END),2)+A.ECDED+A.ETRDED,0) NETPAY  ,A.BASIC*2 BASIC,A.DA*2 DA,A.HRA*2 HRA,A.SPL*2 SPL,  A.TOTDED+ROUND((CASE WHEN A.ECDED > 0 THEN  ( A.WDAYS*(450/A.MDAYS) ) ELSE 0 END),2)-A.ECDED-A.ETRDED TOTDED  ,(SELECT LOGO FROM EDOCIMAGE1 WHERE IMGNAME='LOGO') COMPLOGO, (SELECT LOGO FROM EDOCIMAGE1      WHERE IMGNAME='DGGM') DG,TO_CHAR(TO_DATE(G.STDT,'DD/MM/YY'),'DD/MM/YYYY')||' TO '||TO_CHAR(TO_DATE(G.ENDT,'DD/MM/YY'),'DD/MM/YYYY') PAYCAT1,'{EMPTYPE}' EMPTYPE ,A.WALLO*2 WALLO   ,   A.EWALLO  ,0 ETRDED   FROM  LOPPLHPAYROLL A JOIN HREMPLOYMAST B ON B.HREMPLOYMASTID=A.EMPID JOIN GTCOMPMAST C ON C.GTCOMPMASTID=B.COMPCODE    JOIN HRDESIGNATIONMAST D ON D.HRDESIGNATIONMASTID=A.DESIGNATION JOIN HREMPLOYDETAILS E ON E.HREMPLOYMASTID=B.HREMPLOYMASTID    JOIN HRBANDMAST F ON F.HRBANDMASTID=E.BAND JOIN MONTHLYPAYFRQ G ON G.PAYPERIOD=A.PAYPERIOD JOIN HRMFRQ H ON H.PAYPERIOD=A.PAYPERIOD WHERE      H.PAYPERIOD='" + Class.Users.PayPeriod + "' AND A.PAYPERIOD='" + Class.Users.PayPeriod + "' AND A.PCTYPE='ACTUAL' AND B.IDCARDNO='" + Class.Users.IDCARDNO + "' ) AA )XX,       (SELECT YY.TAKEN,YY.CLOSING,YY.IDCARD,YY.ELEAVE FROM (SELECT  DD.BRANCHNAME BRANCH,AA.IDCARD,BB.FNAME EMPNAME,BB.GENDER,EE.DISPNAME DEPT,FF.DESIGNATION,GG.BANDID ,      AA.SHIFTCNT WDAYS,AA.ELEAVE,AA.TAKEN,AA.TAKEN1,CASE WHEN (AA.ELEAVE-AA.TAKEN1) < 0  THEN 0 ELSE (AA.ELEAVE-AA.TAKEN1) END CLOSING FROM (SELECT A.IDCARD,SUM(A.SHIFTCNT) SHIFTCNT,      ROUND(SUM(A.ELEAVE),3) ELEAVE,SUM(A.TAKEN) TAKEN,SUM(A.TAKEN1) TAKEN1 FROM ( SELECT A.IDCARD,A.SHIFTCNT,0 ELEAVE,0 TAKEN,0 TAKEN1 FROM HREMPWDAY A       UNION ALL       SELECT A.EMPID IDCARD,A.SHIFTCNT,0 ELEAVE,0 TAKEN,0 TAKEN1 FROM HDATTA A,MONTHLYPAYFRQ B,HRLELIGIBLE C WHERE A.PAYPERIOD = B.PAYPERIOD AND B.PAYPERIOD1 = C.PAYPERIOD       AND A.DOCDATE <= (SELECT AA.ENDT FROM MONTHLYPAYFRQ AA WHERE AA.PAYPERIOD='" + Class.Users.PayPeriod + "') AND A.SHIFTCNT > 0 UNION ALL SELECT A.EMPID IDCARD,0 SHIFTCNT,      A.SHIFTCNT*A.LEAVE ELEAVE,0 TAKEN,0 TAKEN1 FROM HDATTA A,MONTHLYPAYFRQ B,HRLELIGIBLE C WHERE A.PAYPERIOD = B.PAYPERIOD AND B.PAYPERIOD1 = C.PAYPERIOD       AND A.DOCDATE <= (SELECT AA.ENDT FROM MONTHLYPAYFRQ AA WHERE AA.PAYPERIOD='" + Class.Users.PayPeriod + "') AND A.SHIFTCNT > 0       AND B.FINYR = (SELECT AA.FINYR FROM MONTHLYPAYFRQ AA    WHERE AA.PAYPERIOD='" + Class.Users.PayPeriod + "')       UNION ALL SELECT TO_NUMBER(A.IDNOCHK) IDCARD,0 SHIFTCNT,0 ELEAVE,(0-SUM(A.STKOPBAL)) TAKEN,(0-SUM(A.STKOPBAL)) TAKEN1 FROM HRLEAVEREGMAST A       WHERE A.LRDATE BETWEEN ( SELECT AA.STDT FROM MONTHLYPAYFRQ AA WHERE AA.PAYPERIOD ='" + Class.Users.PayPeriod + "' )       AND (SELECT AA.ENDT FROM MONTHLYPAYFRQ AA WHERE AA.PAYPERIOD='" + Class.Users.PayPeriod + "') AND A.LTYPE <> 'ENC'       AND A.FINYEAR = (SELECT AA.FINYR FROM MONTHLYPAYFRQ AA WHERE AA.PAYPERIOD='" + Class.Users.PayPeriod + "')         GROUP BY A.IDNOCHK UNION ALL SELECT TO_NUMBER(A.IDNOCHK) IDCARD,0 SHIFTCNT,0 ELEAVE,0 TAKEN,(0-SUM(A.STKOPBAL)) TAKEN1 FROM HRLEAVEREGMAST A       WHERE A.LRDATE < ( SELECT AA.STDT FROM MONTHLYPAYFRQ AA WHERE AA.PAYPERIOD ='" + Class.Users.PayPeriod + "' )       AND A.FINYEAR = (SELECT AA.FINYR FROM MONTHLYPAYFRQ AA WHERE AA.PAYPERIOD='" + Class.Users.PayPeriod + "')       GROUP BY A.IDNOCHK UNION ALL SELECT TO_NUMBER(A.IDNOCHK) IDCARD,0 SHIFTCNT,0 ELEAVE,0 TAKEN,(0-SUM(A.STKOPBAL)) TAKEN1 FROM HRLEAVEREGMAST A       WHERE A.LRDATE = ( SELECT AA.STDT FROM MONTHLYPAYFRQ AA WHERE AA.PAYPERIOD ='" + Class.Users.PayPeriod + "') AND A.LTYPE = 'ENC'       AND ( SELECT TRIM(TO_CHAR(AA.STDT,'DD/MM')) FROM MONTHLYPAYFRQ AA WHERE AA.PAYPERIOD ='" + Class.Users.PayPeriod + "') = '01/01'        AND A.FINYEAR = (SELECT AA.FINYR FROM MONTHLYPAYFRQ AA WHERE AA.PAYPERIOD='" + Class.Users.PayPeriod + "')         GROUP BY A.IDNOCHK        UNION ALL       SELECT TO_NUMBER(BB.IDCARD) IDCARD,A.OLHDCNT SHIFTCNT,0 ELEAVE,0 TAKEN,0 TAKEN1 FROM HRONDUTYDET A,HREMPLOYDETAILS BB, MONTHLYPAYFRQ B,HRLELIGIBLE C       WHERE A.IDCARD = BB.HREMPLOYDETAILSID AND A.PAYPERIOD = B.PAYPERIOD AND B.PAYPERIOD1 = C.PAYPERIOD       AND A.ODATE  <= (SELECT AA.ENDT FROM MONTHLYPAYFRQ AA WHERE AA.PAYPERIOD='" + Class.Users.PayPeriod + "')       UNION ALL       SELECT TO_NUMBER(BB.IDCARD) IDCARD,0 SHIFTCNT,A.OLHDCNT*0.05 ELEAVE,0 TAKEN,0 TAKEN1 FROM HRONDUTYDET A,HREMPLOYDETAILS BB, MONTHLYPAYFRQ B,HRLELIGIBLE C       WHERE A.IDCARD = BB.HREMPLOYDETAILSID AND A.PAYPERIOD = B.PAYPERIOD AND B.PAYPERIOD1 = C.PAYPERIOD       AND A.ODATE  <= (SELECT AA.ENDT FROM MONTHLYPAYFRQ AA WHERE AA.PAYPERIOD='" + Class.Users.PayPeriod + "')       AND B.FINYR = (SELECT AA.FINYR FROM MONTHLYPAYFRQ AA WHERE AA.PAYPERIOD='" + Class.Users.PayPeriod + "') ) A             GROUP BY A.IDCARD HAVING ( SUM(A.SHIFTCNT) >= 240 AND SUM(A.ELEAVE) > 0 ) ) AA,HREMPLOYMAST BB,HREMPLOYDETAILS CC,BRANCHMAST DD,GTDEPTDESGMAST EE,HRDESIGNATIONMAST FF,      HRBANDMAST GG,GTCOMPMAST HH WHERE AA.IDCARD = CC.IDCARD AND BB.HREMPLOYMASTID = CC.HREMPLOYMASTID AND BB.WORKNATURE = DD.BRANCHMASTID       AND EE.GTDEPTDESGMASTID = CC.DEPTNAME AND FF.HRDESIGNATIONMASTID = CC.DESIGNATION AND GG.HRBANDMASTID = CC.BAND AND HH.GTCOMPMASTID = BB.COMPCODE )  YY ) YYY WHERE XX.IDCARDNO=YYY.IDCARD(+) ORDER BY XX.FNAME";
                    DataSet ds1 = Utility.ExecuteSelectQuery(sel0, "HREMPLOYMAST");
                    dtprint1 = ds1.Tables["HREMPLOYMAST"];
                }
               
                else
                {
                    sel0 = "SELECT XX.FINYEAR,XX.COMPCODE,XX.COMPNAME,XX.FNAME,XX.IDCARDNO,XX.BRANCH,XX.DESIGNATION,       XX.PAYPERIOD,XX.NOOFDAYS,XX.LOP,XX.EBASIC,XX.EHRA,XX.EDA,XX.ESPL,XX.ERPF,XX.ERESI,XX.EMESSFEES,XX.EOTHDED,XX.ADV,XX.SADV,       XX.BANK,XX.PAYCAT,        XX.IT,XX.EGROSS,XX.NETPAY,XX.BASIC,XX.DA,XX.HRA,XX.SPL,XX.TOTDED,XX.COMPLOGO,XX.DG,XX.PAYCAT1,NVL(YYY.TAKEN,0) TAKEN,              NVL(YYY.CLOSING,0) CLOSING,YYY.IDCARD,NVL(YYY.ELEAVE,0) ELEAVE,XX.EMPTYPE,XX.WALLO  test1 ,XX.EWALLO test2 ,XX.ETRDED test3  FROM (SELECT AA.FINYEAR,AA.COMPCODE,AA.COMPNAME,AA.FNAME,AA.IDCARDNO,AA.BRANCH,AA.DESIGNATION, AA.PAYPERIOD,AA.NOOFDAYS,AA.LOP,AA.EBASIC,AA.EHRA,AA.EDA,AA.ESPL,AA.ERPF,AA.ERESI,AA.EMESSFEES,AA.EOTHDED,     AA.ADV,AA.SADV,AA.BANK,AA.PAYCAT,                AA.IT,AA.EGROSS,AA.NETPAY,AA.BASIC,AA.DA,AA.HRA,AA.SPL,AA.TOTDED,AA.COMPLOGO,AA.DG,AA.PAYCAT1,AA.EMPTYPE ,AA.WALLO ,AA.EWALLO,AA.ETRDED FROM ( SELECT TO_CHAR(TO_DATE(G.MPGDT,'DD/MM/YY'),'DD/MM/YYYY') FINYEAR,C.COMPCODE,C.COMPNAME,B.FNAME,B.IDCARDNO,A.BRANCH,D.DESIGNATION,     A.PAYPERIOD,     A.WDAYS NOOFDAYS,A.MDAYS-A.WDAYS LOP,A.EBASIC, A.EHRA,A.EDA,A.ESPL,A.PF ERPF,A.ESI ERESI,A.MESSFEES EMESSFEES,(A.EOTHDED+A.ECDED+A.ETDED) EOTHDED,A.LOAN ADV,A.SADV,CASE WHEN A.BANK = 'CASH' THEN ' ' ELSE A.BANK END BANK,A.PAYCAT,A.TDSN IT,A.EGROSS,A.NETPAY,A.BASIC*2 BASIC,A.DA*2 DA,A.HRA*2 HRA,A.SPL*2 SPL,A.TOTDED,(SELECT LOGO FROM EDOCIMAGE1 WHERE IMGNAME='LOGO') COMPLOGO, (SELECT LOGO FROM EDOCIMAGE1     WHERE IMGNAME='DGGM') DG,TO_CHAR(TO_DATE(G.STDT,'DD/MM/YY'),'DD/MM/YYYY')||' TO '||TO_CHAR(TO_DATE(G.ENDT,'DD/MM/YY'),'DD/MM/YYYY') PAYCAT1,'{EMPTYPE}' EMPTYPE ,A.WALLO*2 WALLO   ,A.EWALLO  ,A.ETRDED   FROM  LOPPLHPAYROLL A JOIN HREMPLOYMAST B ON B.HREMPLOYMASTID=A.EMPID JOIN GTCOMPMAST C ON C.GTCOMPMASTID=B.COMPCODE JOIN HRDESIGNATIONMAST D ON D.HRDESIGNATIONMASTID=A.DESIGNATION JOIN HREMPLOYDETAILS E ON E.HREMPLOYMASTID=B.HREMPLOYMASTID JOIN HRBANDMAST F ON F.HRBANDMASTID=E.BAND JOIN MONTHLYPAYFRQ G ON G.PAYPERIOD=A.PAYPERIOD WHERE  A.PAYPERIOD= '" + Class.Users.PayPeriod + "' AND A.PCTYPE='ACTUAL' and B.IDCARDNO= '" + Class.Users.IDCARDNO + "' ) AA ) XX, (SELECT YY.TAKEN,YY.CLOSING,YY.IDCARD,YY.ELEAVE FROM (SELECT  DD.BRANCHNAME BRANCH,AA.IDCARD,BB.FNAME EMPNAME,BB.GENDER,EE.DISPNAME DEPT,FF.DESIGNATION,GG.BANDID ,AA.SHIFTCNT WDAYS,AA.ELEAVE,AA.TAKEN,AA.TAKEN1,CASE WHEN (AA.ELEAVE-AA.TAKEN1) < 0  THEN 0 ELSE (AA.ELEAVE-AA.TAKEN1) END CLOSING FROM (SELECT A.IDCARD,SUM(A.SHIFTCNT) SHIFTCNT,ROUND(SUM(A.ELEAVE),3) ELEAVE,SUM(A.TAKEN) TAKEN,SUM(A.TAKEN1) TAKEN1 FROM ( SELECT A.IDCARD,A.SHIFTCNT,0 ELEAVE,0 TAKEN,0 TAKEN1 FROM HREMPWDAY A UNION ALL SELECT A.EMPID IDCARD,A.SHIFTCNT,0 ELEAVE,0 TAKEN,0 TAKEN1 FROM HDATTA A,MONTHLYPAYFRQ B,HRLELIGIBLE C WHERE A.PAYPERIOD = B.PAYPERIOD AND B.PAYPERIOD1 = C.PAYPERIOD AND A.DOCDATE <= (SELECT AA.ENDT FROM MONTHLYPAYFRQ AA WHERE AA.PAYPERIOD= '" + Class.Users.PayPeriod + "') AND A.SHIFTCNT > 0 UNION ALL SELECT A.EMPID IDCARD,0 SHIFTCNT,A.SHIFTCNT*A.LEAVE ELEAVE,0 TAKEN,0 TAKEN1 FROM HDATTA A,MONTHLYPAYFRQ B,HRLELIGIBLE C WHERE A.PAYPERIOD = B.PAYPERIOD AND B.PAYPERIOD1 = C.PAYPERIOD AND A.DOCDATE <= (SELECT AA.ENDT FROM MONTHLYPAYFRQ AA WHERE AA.PAYPERIOD= '" + Class.Users.PayPeriod + "') AND A.SHIFTCNT > 0 AND B.FINYR = (SELECT AA.FINYR FROM MONTHLYPAYFRQ AA WHERE AA.PAYPERIOD= '" + Class.Users.PayPeriod + "') UNION ALL SELECT TO_NUMBER(A.IDNOCHK) IDCARD,0 SHIFTCNT,0 ELEAVE,(0-SUM(A.STKOPBAL)) TAKEN,(0-SUM(A.STKOPBAL)) TAKEN1 FROM HRLEAVEREGMAST A WHERE A.LRDATE BETWEEN ( SELECT AA.STDT FROM MONTHLYPAYFRQ AA WHERE AA.PAYPERIOD ='" + Class.Users.PayPeriod + "' ) AND (SELECT AA.ENDT FROM MONTHLYPAYFRQ AA WHERE AA.PAYPERIOD= '" + Class.Users.PayPeriod + "') AND A.LTYPE <> 'ENC' AND A.FINYEAR = (SELECT AA.FINYR FROM MONTHLYPAYFRQ AA WHERE AA.PAYPERIOD= '" + Class.Users.PayPeriod + "') GROUP BY A.IDNOCHK UNION ALL SELECT TO_NUMBER(A.IDNOCHK) IDCARD,0 SHIFTCNT,0 ELEAVE,0 TAKEN,(0-SUM(A.STKOPBAL)) TAKEN1 FROM HRLEAVEREGMAST A WHERE A.LRDATE < ( SELECT AA.STDT FROM MONTHLYPAYFRQ AA WHERE AA.PAYPERIOD ='" + Class.Users.PayPeriod + "' ) AND A.FINYEAR = (SELECT AA.FINYR FROM MONTHLYPAYFRQ AA WHERE AA.PAYPERIOD= '" + Class.Users.PayPeriod + "') GROUP BY A.IDNOCHK UNION ALL SELECT TO_NUMBER(A.IDNOCHK) IDCARD,0 SHIFTCNT,0 ELEAVE,0 TAKEN,(0-SUM(A.STKOPBAL)) TAKEN1 FROM HRLEAVEREGMAST A WHERE A.LRDATE = ( SELECT AA.STDT FROM MONTHLYPAYFRQ AA WHERE AA.PAYPERIOD ='" + Class.Users.PayPeriod + "' ) AND A.LTYPE = 'ENC' AND ( SELECT TRIM(TO_CHAR(AA.STDT,'DD/MM')) FROM MONTHLYPAYFRQ AA WHERE AA.PAYPERIOD ='" + Class.Users.PayPeriod + "') = '01/01'  AND A.FINYEAR = (SELECT AA.FINYR FROM MONTHLYPAYFRQ AA WHERE AA.PAYPERIOD= '" + Class.Users.PayPeriod + "')   GROUP BY A.IDNOCHK  UNION ALL SELECT TO_NUMBER(BB.IDCARD) IDCARD,A.OLHDCNT SHIFTCNT,0 ELEAVE,0 TAKEN,0 TAKEN1 FROM HRONDUTYDET A,HREMPLOYDETAILS BB, MONTHLYPAYFRQ B,HRLELIGIBLE C WHERE A.IDCARD = BB.HREMPLOYDETAILSID AND A.PAYPERIOD = B.PAYPERIOD AND B.PAYPERIOD1 = C.PAYPERIOD AND A.ODATE  <= (SELECT AA.ENDT FROM MONTHLYPAYFRQ AA WHERE AA.PAYPERIOD= '" + Class.Users.PayPeriod + "') UNION ALL SELECT TO_NUMBER(BB.IDCARD) IDCARD,0 SHIFTCNT,A.OLHDCNT*0.05 ELEAVE,0 TAKEN,0 TAKEN1 FROM HRONDUTYDET A,HREMPLOYDETAILS BB, MONTHLYPAYFRQ B,HRLELIGIBLE C WHERE A.IDCARD = BB.HREMPLOYDETAILSID AND A.PAYPERIOD = B.PAYPERIOD AND B.PAYPERIOD1 = C.PAYPERIOD AND A.ODATE  <= (SELECT AA.ENDT FROM MONTHLYPAYFRQ AA WHERE AA.PAYPERIOD= '" + Class.Users.PayPeriod + "') AND B.FINYR = (SELECT AA.FINYR FROM MONTHLYPAYFRQ AA WHERE AA.PAYPERIOD= '" + Class.Users.PayPeriod + "') ) A GROUP BY A.IDCARD HAVING ( SUM(A.SHIFTCNT) >= 240 AND SUM(A.ELEAVE) > 0 ) ) AA,HREMPLOYMAST BB,HREMPLOYDETAILS CC,BRANCHMAST DD,GTDEPTDESGMAST EE,HRDESIGNATIONMAST FF,HRBANDMAST GG,GTCOMPMAST HH WHERE AA.IDCARD = CC.IDCARD AND BB.HREMPLOYMASTID = CC.HREMPLOYMASTID AND BB.WORKNATURE = DD.BRANCHMASTID AND EE.GTDEPTDESGMASTID = CC.DEPTNAME AND FF.HRDESIGNATIONMASTID = CC.DESIGNATION AND GG.HRBANDMASTID = CC.BAND AND HH.GTCOMPMASTID = BB.COMPCODE )  YY ) YYY WHERE XX.IDCARDNO=YYY.IDCARD(+) ORDER BY XX.FNAME";
                    DataSet ds1 = Utility.ExecuteSelectQuery(sel0, "HREMPLOYMAST");
                    dtprint1 = ds1.Tables["HREMPLOYMAST"];
                }
               
             
                if (dtprint1.Rows.Count > 0)
                {
                    string sel2 = "SELECT A.EMPNAME,A.IDCARDNO, A.signature,(SELECT AA.signature FROM ASPTBLEMP AA WHERE AA.ACTIVE='T') as msignature,to_char(c.doj,'dd-MM-YYYY') as dateofjoin,a.BYTESNAME as nameintamil,a.BYTESDESIGN as designintamil   FROM ASPTBLEMP A  JOIN GTCOMPMAST B ON A.COMPCODE=B.GTCOMPMASTID   join HREMPLOYDETAILS c on C.IDCARD=A.IDCARDNO WHERE A.IDCARDNO ='" + listView2.SelectedItems[0].SubItems[3].Text + "' and  B.COMPCODE = '" + Class.Users.HCompcode + "' ";// where  A.FINYEAR='" + combofinyear.Text + "' AND  D.compcode='" + combocompcode.Text + "'  and a.payperiod='" + allip.Items[i].SubItems[7].Text + "' AND  C.IDCARD='" + s[1].Substring(7).TrimEnd() + "' order by 1";
                    DataSet ds2 = Utility.ExecuteSelectQuery(sel2, "ASPTBLEMP");
                    dtprint2 = ds2.Tables["ASPTBLEMP"];
                    string sel3 = "SELECT A.FROMDATE||' - '||A.TODATE PERIOD,A.BUYER OTMIN," +
                        "ROUND(((((BASIC+DA+SPL)/26)/8)/60)*2*A.BUYER,2) OTAMT, " +
                        " CASE WHEN B.ESI = 'YES' THEN ROUND(ROUND(((((BASIC+DA+SPL)/26)/8)/60)*2*A.BUYER,2)*0.75/100,2) ELSE 0 END ESI,  ROUND(((((BASIC+DA+SPL)/26)/8)/60)*2*A.BUYER,2) - CASE WHEN B.ESI = 'YES' THEN ROUND(ROUND(((((BASIC+DA+SPL)/26)/8)/60)*2*A.BUYER,2)*0.75/100,2) ELSE 0 END NETAMOUNT,B.IDCARD AS IDCARDNO FROM OTSTAMENT A JOIN HREMPLOYDETAILS B ON A.EMPMAID = B.HREMPLOYMASTID WHERE B.IDACTIVE = 'YES'  AND  A.PAYPERIOD ='" + listView2.SelectedItems[0].SubItems[7].Text + "' AND B.IDCARD= '" + listView2.SelectedItems[0].SubItems[3].Text + "' AND A.BUYER > 0  ORDER BY A.FROMDATE";
                    DataSet ds3 = Utility.ExecuteSelectQuery(sel3, "OTSTAMENT");
                    dtprint3 = ds3.Tables["OTSTAMENT"];
                    rd1.Database.Tables["DataTable1"].SetDataSource(dtprint1);
                    rd1.Database.Tables["DataTable2"].SetDataSource(dtprint2);
                    rd1.Database.Tables["DataTable3"].SetDataSource(dtprint3);
                    crystalReportViewer1.Refresh(); crystalReportViewer1.ReportSource = null;
                    crystalReportViewer1.ReportSource = rd1;
                    empty();
                }
                else
                {
                    MessageBox.Show("No Data Found");
                }
            }

            if (combostatement.Text == "STATEMENT")
            {
                for (i = 0; i < allip.Items.Count; i++)
                {
                    string sel0 = ""; DataSet ds0; DataTable dt0;
                    if (Class.Users.HUserName == "GROUP1" || Class.Users.HUserName == "GROUP2" || Class.Users.HUserName == "GROUP3" || Class.Users.HUserName == "GROUP4")
                    {
                        sel0 = "SELECT AA.BRANCH,AA.BANDID,AA.GENDER,AA.IDCARD,AA.EMPNAME,AA.MACHINENAME,DENSE_RANK() OVER (PARTITION BY AA.GENDER,AA.BRANCH,AA.BANDID ORDER BY AA.EMPNAME) SNO,     AA.BASICPAY,AA.ACTWORKDAYS,AA.SALRYDAYS,AA.EBASIC,AA.EDA,AA.EHRA,ROUND(AA.EGROSS,2) EGROSS,AA.ESI,AA.PF,AA.TDSN,AA.MESSFEES, AA.OTHDED,AA.TOTDED,AA.BANK,AA.NETPAY,   AA.SADV,AA.LOAN, AA.ETDED,AA.ECDED,AA.STDT,AA.ENDT,AA.COMPNAME,(select bb.SIGNATURE from  asptblemp bb where BB.IDCARDNO IN '" + allip.Items[i].SubItems[3].Text + "' ) as  SIGNATURE,'' Test1,aa.ewallo as Test2,aa.etrded as Test3       FROM ( SELECT A.BRANCH,D.BANDID,C.GENDER,TO_NUMBER(B.IDCARD) IDCARD,C.FNAME EMPNAME,G.DESIGNATION MACHINENAME, A.BASIC*2 BASICPAY,F.WEEKLYHOLIDAYS+A.WDAYS ACTWORKDAYS,       A.WDAYS SALRYDAYS,A.EBASIC,A.EDA,A.ESPL EHRA,A.EGROSS,A.ESI,A.PF,A.TDSN,A.MESSFEES, A.EOTHDED OTHDED,  A.TOTDED-A.ECDED-A.ETRDED+(CASE WHEN A.ECDED > 0 THEN ROUND((A.WDAYS*(450/A.MDAYS)),2) ELSE 0 END) TOTDED,A.BANK,  ROUND(A.NETPAY+A.ECDED+A.ETRDED-(CASE WHEN A.ECDED > 0 THEN ROUND((A.WDAYS*(450/A.MDAYS)),2) ELSE 0 END),0) NETPAY,'' SIGNATURE,A.SADV,A.LOAN,H.COMPNAME,  A.ETDED,CASE WHEN A.ECDED > 0 THEN ROUND((A.WDAYS*(450/A.MDAYS)),2) ELSE 0 END+A.ETDED ECDED,      TO_CHAR(F.STDT,'dd/mm/yyyy') STDT,TO_CHAR(F.ENDT,'dd/mm/yyyy') ENDT,A.EWALLO,  A.ETRDED-A.ETRDED ETRDED    FROM LOPPLHPAYROLL A,HREMPLOYDETAILS B,HREMPLOYMAST C,HRBANDMAST D,MACHINEMAST E,MONTHLYPAYFRQ F,     HRDESIGNATIONMAST G,GTCOMPMAST H WHERE A.EMPID = B.HREMPLOYMASTID AND B.HREMPLOYMASTID = C.HREMPLOYMASTID AND H.GTCOMPMASTID=C.COMPCODE AND B.BAND = D.HRBANDMASTID      AND E.MACHINEMASTID = B.MACNAME AND F.PAYPERIOD = A.PAYPERIOD AND G.HRDESIGNATIONMASTID = B.DESIGNATION AND A.PCTYPE = 'ACTUAL'      AND A.PAYPERIOD ='" + allip.Items[i].SubItems[7].Text + "' AND H.COMPCODE='" + allip.Items[i].SubItems[6].Text + "'  and B.IDCARD='" + allip.Items[i].SubItems[3].Text + "' AND A.BANK NOT IN ('BY CASH','CASH') AND (A.NETPAY > 0 OR A.WDAYS > 0 )      UNION ALL SELECT A.BRANCH,D.BANDID,C.GENDER,TO_NUMBER(B.IDCARD) IDCARD,C.FNAME EMPNAME,G.DESIGNATION MACHINENAME, A.BASIC*2 BASICPAY,F.WEEKLYHOLIDAYS+A.WDAYS ACTWORKDAYS,     A.WDAYS SALRYDAYS,A.EBASIC,A.EDA,A.ESPL EHRA,A.EGROSS,A.ESI,A.PF,A.TDSN,A.MESSFEES, A.EOTHDED OTHDED,A.TOTDED,A.BANK,A.NETPAY,'' SIGNATURE,A.SADV,A.LOAN,H.COMPNAME,A.ETDED,     A.ECDED+A.ETDED ECDED, TO_CHAR(F.STDT,'dd/mm/yyyy') STDT,TO_CHAR(F.ENDT,'dd/mm/yyyy') ENDT,A.EWALLO,A.ETRDED-A.ETRDED ETRDED      FROM LOPPLHPAYROLL A,HREMPLOYDETAILS B,HREMPLOYMAST C,HRBANDMAST D,MACHINEMAST E,MONTHLYPAYFRQ F ,HRDESIGNATIONMAST G,GTCOMPMAST H WHERE A.EMPID = B.HREMPLOYMASTID      AND B.HREMPLOYMASTID = C.HREMPLOYMASTID AND H.GTCOMPMASTID=C.COMPCODE AND B.BAND = D.HRBANDMASTID AND E.MACHINEMASTID = B.MACNAME AND F.PAYPERIOD = A.PAYPERIOD      AND G.HRDESIGNATIONMASTID = B.DESIGNATION AND A.PCTYPE = 'ACTUAL' AND A.PAYPERIOD ='" + allip.Items[i].SubItems[7].Text + "' AND H.COMPCODE='" + allip.Items[i].SubItems[6].Text + "'  and B.IDCARD='" + allip.Items[i].SubItems[3].Text + "' AND A.BANK IN ('BY CASH','CASH')       AND C.EMPTYPE = 'Permanent' AND (A.NETPAY > 0 OR A.WDAYS > 0 )  UNION ALL SELECT A.BRANCH,D.BANDID,C.GENDER,TO_NUMBER(B.IDCARD) IDCARD,C.FNAME EMPNAME,G.DESIGNATION MACHINENAME,      A.BASIC*2 BASICPAY,F.WEEKLYHOLIDAYS+A.WDAYS ACTWORKDAYS,A.WDAYS SALRYDAYS,A.EBASIC,A.EDA,A.ESPL EHRA,A.EGROSS,A.ESI,A.PF,A.TDSN,A.MESSFEES, A.EOTHDED OTHDED,A.TOTDED,     A.BANK,A.NETPAY,'' SIGNATURE,A.SADV,A.LOAN,H.COMPNAME,A.ETDED,A.ECDED+A.ETDED ECDED, TO_CHAR(F.STDT,'dd/mm/yyyy') STDT,TO_CHAR(F.ENDT,'dd/mm/yyyy') ENDT,A.EWALLO,A.ETRDED-A.ETRDED ETRDED FROM LOPPLHPAYROLL A,HREMPLOYDETAILS B,HREMPLOYMAST C,HRBANDMAST D,MACHINEMAST E,MONTHLYPAYFRQ F ,HRDESIGNATIONMAST G,GTCOMPMAST H WHERE A.EMPID = B.HREMPLOYMASTID AND B.HREMPLOYMASTID = C.HREMPLOYMASTID AND H.GTCOMPMASTID=C.COMPCODE AND B.BAND = D.HRBANDMASTID AND E.MACHINEMASTID = B.MACNAME AND F.PAYPERIOD = A.PAYPERIOD AND G.HRDESIGNATIONMASTID = B.DESIGNATION  AND A.PCTYPE = 'ACTUAL'  AND A.PAYPERIOD ='" + allip.Items[i].SubItems[7].Text + "' AND H.COMPCODE='" + allip.Items[i].SubItems[6].Text + "'  and B.IDCARD='" + allip.Items[i].SubItems[3].Text + "' AND A.BANK IN ('BY CASH','CASH')   AND C.EMPTYPE = 'Trainee'  AND (A.NETPAY > 0 OR A.WDAYS > 0 )  ) AA   ORDER BY 3,2";
                        ds0 = Utility.ExecuteSelectQuery(sel0, "HREMPLOYMAST");
                        dt0 = ds0.Tables["HREMPLOYMAST"];
                    }
                 
                    else
                    {
                        sel0 = "SELECT AA.BRANCH,AA.BANDID,AA.GENDER,AA.IDCARD,AA.EMPNAME,AA.MACHINENAME,DENSE_RANK() OVER (PARTITION BY AA.GENDER,AA.BRANCH,AA.BANDID ORDER BY AA.EMPNAME) SNO,     AA.BASICPAY,AA.ACTWORKDAYS,AA.SALRYDAYS,AA.EBASIC,AA.EDA,AA.EHRA,ROUND(AA.EGROSS,2) EGROSS,AA.ESI,AA.PF,AA.TDSN,AA.MESSFEES, AA.OTHDED,AA.TOTDED,AA.BANK,AA.NETPAY,     AA.SADV,AA.LOAN, AA.ETDED,AA.ECDED,AA.STDT,AA.ENDT,AA.COMPNAME,(select bb.SIGNATURE from  asptblemp bb where BB.IDCARDNO IN '" + allip.Items[i].SubItems[3].Text + "' ) as  SIGNATURE,'' Test1,aa.ewallo as Test2,aa.etrded as Test3     FROM ( SELECT A.BRANCH,D.BANDID,C.GENDER,TO_NUMBER(B.IDCARD) IDCARD,C.FNAME EMPNAME,G.DESIGNATION MACHINENAME, A.BASIC*2 BASICPAY,F.WEEKLYHOLIDAYS+A.WDAYS ACTWORKDAYS,     A.WDAYS SALRYDAYS,A.EBASIC,A.EDA,A.ESPL EHRA,A.EGROSS,A.ESI,A.PF,A.TDSN,A.MESSFEES, A.EOTHDED OTHDED,A.TOTDED,A.BANK,A.NETPAY,'' SIGNATURE,A.SADV,A.LOAN,H.COMPNAME,A.ETDED,A.ECDED+A.ETDED ECDED,      TO_CHAR(F.STDT,'dd/mm/yyyy') STDT,TO_CHAR(F.ENDT,'dd/mm/yyyy') ENDT,A.EWALLO,A.ETRDED FROM LOPPLHPAYROLL A,HREMPLOYDETAILS B,HREMPLOYMAST C,HRBANDMAST D,MACHINEMAST E,MONTHLYPAYFRQ F,     HRDESIGNATIONMAST G,GTCOMPMAST H WHERE A.EMPID = B.HREMPLOYMASTID AND B.HREMPLOYMASTID = C.HREMPLOYMASTID AND H.GTCOMPMASTID=C.COMPCODE AND B.BAND = D.HRBANDMASTID      AND E.MACHINEMASTID = B.MACNAME AND F.PAYPERIOD = A.PAYPERIOD AND G.HRDESIGNATIONMASTID = B.DESIGNATION AND A.PCTYPE = 'ACTUAL'      AND A.PAYPERIOD ='" + allip.Items[i].SubItems[7].Text + "' AND H.COMPCODE='" + allip.Items[i].SubItems[6].Text + "'  and B.IDCARD='" + allip.Items[i].SubItems[3].Text + "' AND A.BANK NOT IN ('BY CASH','CASH') AND (A.NETPAY > 0 OR A.WDAYS > 0 )      UNION ALL SELECT A.BRANCH,D.BANDID,C.GENDER,TO_NUMBER(B.IDCARD) IDCARD,C.FNAME EMPNAME,G.DESIGNATION MACHINENAME, A.BASIC*2 BASICPAY,F.WEEKLYHOLIDAYS+A.WDAYS ACTWORKDAYS,     A.WDAYS SALRYDAYS,A.EBASIC,A.EDA,A.ESPL EHRA,A.EGROSS,A.ESI,A.PF,A.TDSN,A.MESSFEES, A.EOTHDED OTHDED,A.TOTDED,A.BANK,A.NETPAY,'' SIGNATURE,A.SADV,A.LOAN,H.COMPNAME,A.ETDED,     A.ECDED+A.ETDED ECDED, TO_CHAR(F.STDT,'dd/mm/yyyy') STDT,TO_CHAR(F.ENDT,'dd/mm/yyyy') ENDT,A.EWALLO,A.ETRDED      FROM LOPPLHPAYROLL A,HREMPLOYDETAILS B,HREMPLOYMAST C,HRBANDMAST D,MACHINEMAST E,MONTHLYPAYFRQ F ,HRDESIGNATIONMAST G,GTCOMPMAST H WHERE A.EMPID = B.HREMPLOYMASTID      AND B.HREMPLOYMASTID = C.HREMPLOYMASTID AND H.GTCOMPMASTID=C.COMPCODE AND B.BAND = D.HRBANDMASTID AND E.MACHINEMASTID = B.MACNAME AND F.PAYPERIOD = A.PAYPERIOD      AND G.HRDESIGNATIONMASTID = B.DESIGNATION AND A.PCTYPE = 'ACTUAL' AND A.PAYPERIOD ='" + allip.Items[i].SubItems[7].Text + "' AND H.COMPCODE='" + allip.Items[i].SubItems[6].Text + "'  and B.IDCARD='" + allip.Items[i].SubItems[3].Text + "' AND A.BANK IN ('BY CASH','CASH')       AND C.EMPTYPE = 'Permanent' AND (A.NETPAY > 0 OR A.WDAYS > 0 )  UNION ALL SELECT A.BRANCH,D.BANDID,C.GENDER,TO_NUMBER(B.IDCARD) IDCARD,C.FNAME EMPNAME,G.DESIGNATION MACHINENAME,      A.BASIC*2 BASICPAY,F.WEEKLYHOLIDAYS+A.WDAYS ACTWORKDAYS,A.WDAYS SALRYDAYS,A.EBASIC,A.EDA,A.ESPL EHRA,A.EGROSS,A.ESI,A.PF,A.TDSN,A.MESSFEES, A.EOTHDED OTHDED,A.TOTDED,     A.BANK,A.NETPAY,'' SIGNATURE,A.SADV,A.LOAN,H.COMPNAME,A.ETDED,A.ECDED+A.ETDED ECDED, TO_CHAR(F.STDT,'dd/mm/yyyy') STDT,TO_CHAR(F.ENDT,'dd/mm/yyyy') ENDT,A.EWALLO,A.ETRDED FROM LOPPLHPAYROLL A,HREMPLOYDETAILS B,HREMPLOYMAST C,HRBANDMAST D,MACHINEMAST E,MONTHLYPAYFRQ F ,HRDESIGNATIONMAST G,GTCOMPMAST H WHERE A.EMPID = B.HREMPLOYMASTID AND B.HREMPLOYMASTID = C.HREMPLOYMASTID AND H.GTCOMPMASTID=C.COMPCODE AND B.BAND = D.HRBANDMASTID AND E.MACHINEMASTID = B.MACNAME AND F.PAYPERIOD = A.PAYPERIOD AND G.HRDESIGNATIONMASTID = B.DESIGNATION  AND A.PCTYPE = 'ACTUAL' AND A.PAYPERIOD ='" + allip.Items[i].SubItems[7].Text + "' AND H.COMPCODE='" + allip.Items[i].SubItems[6].Text + "'  and B.IDCARD='" + allip.Items[i].SubItems[3].Text + "' AND A.BANK IN ('BY CASH','CASH')   AND C.EMPTYPE = 'Trainee'  AND (A.NETPAY > 0 OR A.WDAYS > 0 )  ) AA   ORDER BY 3,2";
                        ds0 = Utility.ExecuteSelectQuery(sel0, "HREMPLOYMAST");
                        dt0 = ds0.Tables["HREMPLOYMAST"];
                    }
                    if (dt0.Rows.Count > 0)
                    {
                        foreach (DataRow porow in dt0.Rows)
                        {
                            if (dtprint0 == null)
                            {
                                dtprint0 = dt0.Clone();

                            }
                            dtprint0.Rows.Add();
                            string sel1 = "SELECT A.IDCARDNO FROM ASPTBLEMP A WHERE A.IDCARDNO='" + allip.Items[i].SubItems[3].Text + "'";
                            DataSet ds1 = Utility.ExecuteSelectQuery(sel1, "ASPTBLEMP");
                            DataTable dt1 = ds1.Tables["ASPTBLEMP"];

                            dtprint0.Rows[i]["BRANCH"] = porow.ItemArray[0].ToString();
                            dtprint0.Rows[i]["BANDID"] = porow.ItemArray[1].ToString();
                            dtprint0.Rows[i]["GENDER"] = porow.ItemArray[2].ToString();
                            dtprint0.Rows[i]["IDCARD"] = porow.ItemArray[3].ToString();
                            dtprint0.Rows[i]["EMPNAME"] = porow.ItemArray[4].ToString();
                            dtprint0.Rows[i]["MACHINENAME"] = porow.ItemArray[5].ToString();
                            dtprint0.Rows[i]["SNO"] = porow.ItemArray[6].ToString();
                            dtprint0.Rows[i]["BASICPAY"] = porow.ItemArray[7].ToString();
                            dtprint0.Rows[i]["ACTWORKDAYS"] = porow.ItemArray[8].ToString();
                            dtprint0.Rows[i]["SALRYDAYS"] = porow.ItemArray[9].ToString();

                            dtprint0.Rows[i]["EBASIC"] = porow.ItemArray[10].ToString();
                            dtprint0.Rows[i]["EDA"] = porow.ItemArray[11].ToString();
                            dtprint0.Rows[i]["EHRA"] = porow.ItemArray[12].ToString();
                            dtprint0.Rows[i]["EGROSS"] = porow.ItemArray[13].ToString();
                            dtprint0.Rows[i]["ESI"] = porow.ItemArray[14].ToString();
                            dtprint0.Rows[i]["PF"] = porow.ItemArray[15].ToString();
                            dtprint0.Rows[i]["TDSN"] = porow.ItemArray[16].ToString();
                            dtprint0.Rows[i]["MESSFEES"] = porow.ItemArray[17].ToString();
                            dtprint0.Rows[i]["OTHDED"] = porow.ItemArray[18].ToString();
                            if (porow.ItemArray[19].ToString() == "") { }
                            else
                            {
                                dtprint0.Rows[i]["TOTDED"] = porow.ItemArray[19].ToString();
                            }
                            dtprint0.Rows[i]["BANK"] = porow.ItemArray[20].ToString();
                            if (porow.ItemArray[21].ToString() == "") { }
                            else
                            {
                                dtprint0.Rows[i]["NETPAY"] = porow.ItemArray[21].ToString();
                            }
                            if (porow.ItemArray[22].ToString() == "") { }
                            else
                            {
                                dtprint0.Rows[i]["SADV"] = porow.ItemArray[22].ToString();
                            }
                            if (porow.ItemArray[23].ToString() == "") { }
                            else
                            {
                                dtprint0.Rows[i]["LOAN"] = porow.ItemArray[23].ToString();
                            }
                            if (porow.ItemArray[24].ToString() == "") { }
                            else
                            {
                                dtprint0.Rows[i]["ETDED"] = porow.ItemArray[24].ToString();
                            }
                            if (porow.ItemArray[25].ToString() == "") { }
                            else
                            {
                                dtprint0.Rows[i]["ECDED"] = porow.ItemArray[25].ToString();
                            }
                            dtprint0.Rows[i]["STDT"] = porow.ItemArray[26].ToString();
                            dtprint0.Rows[i]["ENDT"] = porow.ItemArray[27].ToString();
                            dtprint0.Rows[i]["COMPNAME"] = porow.ItemArray[28].ToString();

                            if (dt1.Rows.Count > 0)
                            {
                                foreach (DataRow myRow in dt0.Rows)
                                {

                                    bytes = (byte[])myRow.ItemArray[29];

                                    dtprint0.Rows[i]["SIGNATURE"] = bytes;




                                }
                            }
                            dtprint0.Rows[i]["TEST1"] = combopayperiod.Text;
                            if (porow.ItemArray[31].ToString() == "") { }
                            else
                            {
                                dtprint0.Rows[i]["TEST2"] = Convert.ToDouble("0" + porow.ItemArray[31].ToString());
                            }
                            if (porow.ItemArray[32].ToString() == "") { }
                            else
                            {
                                dtprint0.Rows[i]["TEST3"] = Convert.ToDouble("0" + porow.ItemArray[32].ToString());

                            }
                        }

                    }
                    else
                    {
                        if (dtprint0 == null)
                        {
                            dtprint0 = dt0.Clone();
                        }
                        dtprint0.Rows.Add();

                        dtprint0.Rows[i]["BANDID"] = "This IDCard '" + allip.Items[i].SubItems[4].Text + "' does't have in EmployeeMaster ";
                        dtprint0.Rows[i]["GENDER"] = "NILL";
                        dtprint0.Rows[i]["IDCARD"] = allip.Items[i].SubItems[4].Text;
                        dtprint0.Rows[i]["EMPNAME"] = allip.Items[i].SubItems[5].Text;
                    }




                    if (dtprint0.Rows.Count > 0)
                    {

                        rd.Database.Tables["DataTable1"].SetDataSource(dtprint0);
                        crystalReportViewer1.Refresh(); crystalReportViewer1.ReportSource = null;
                        crystalReportViewer1.ReportSource = rd;





                    }

                }
                empty();
            }
           
        }

        public void DownLoads()
        {
            try
            {
                if (allip.Items.Count > 0)
                {
                    Class.Users.UserTime = 0;
                    i = 0; string folderLocation1 = "D:\\PaySlipDownload\\" + combopayperiod.Text + "\\";
                    progressBar2.Minimum = 0; int cnt = 0; int k = 0;
                    progressBar2.Maximum = allip.Items.Count;
                    Cursor = Cursors.WaitCursor; rd1.PrintOptions.PrinterName = printDialog1.PrinterSettings.PrinterName;
                    if (combostatement.Text == "SALARYSLIP")
                    {
                        crystalReportViewer1.ReportSource = null; crystalReportViewer1.Refresh();
                        Cursor = Cursors.WaitCursor;
                        progressBar2.Minimum = 0;
                        progressBar2.Maximum = allip.Items.Count;
                        progressBar2.Maximum = allip.Items.Count; lblprogross4.Visible = true;

                        foreach (ListViewItem item in allip.Items)
                        {
                            string sel0 = "";
                            Class.Users.PayPeriod = ""; Class.Users.IDCARDNO = 0;
                            Class.Users.PayPeriod = item.SubItems[7].Text;
                            Class.Users.IDCARDNO = Convert.ToInt64(item.SubItems[3].Text);
                            if (Class.Users.HUserName == "GROUP1" || Class.Users.HUserName == "GROUP2" || Class.Users.HUserName == "GROUP3" || Class.Users.HUserName == "GROUP4")
                            {
                                sel0 = "SELECT XX.FINYEAR,XX.COMPCODE,XX.COMPNAME,XX.FNAME,XX.IDCARDNO,XX.BRANCH,XX.DESIGNATION,       XX.PAYPERIOD,XX.NOOFDAYS,XX.LOP,XX.EBASIC,XX.EHRA,XX.EDA,XX.ESPL,XX.ERPF,XX.ERESI,XX.EMESSFEES,XX.EOTHDED,XX.ADV,XX.SADV,       XX.BANK,XX.PAYCAT,     XX.IT,XX.EGROSS,XX.NETPAY,XX.BASIC,XX.DA,XX.HRA,XX.SPL,XX.TOTDED,XX.COMPLOGO,XX.DG,XX.PAYCAT1,NVL(YYY.TAKEN,0) TAKEN,     NVL(YYY.CLOSING,0) CLOSING,YYY.IDCARD,NVL(YYY.ELEAVE,0) ELEAVE,XX.EMPTYPE,XX.WALLO  TEST1 ,XX.EWALLO TEST2 ,XX.ETRDED TEST3  FROM (SELECT AA.FINYEAR,AA.COMPCODE,AA.COMPNAME,AA.FNAME,AA.IDCARDNO,AA.BRANCH,AA.DESIGNATION, AA.PAYPERIOD,AA.NOOFDAYS,AA.LOP,AA.EBASIC,AA.EHRA,AA.EDA,AA.ESPL,  AA.ERPF,AA.ERESI,AA.EMESSFEES,AA.EOTHDED,     AA.ADV,AA.SADV,AA.BANK,AA.PAYCAT,                AA.IT,AA.EGROSS,AA.NETPAY,AA.BASIC,AA.DA,AA.HRA,AA.SPL,AA.TOTDED,AA.COMPLOGO,  AA.DG,AA.PAYCAT1,AA.EMPTYPE ,AA.WALLO ,AA.EWALLO,AA.ETRDED  FROM ( SELECT TO_CHAR(TO_DATE(H.MPGDT,'DD/MM/YY'),'DD/MM/YYYY') AS FINYEAR,C.COMPCODE,C.COMPNAME,B.FNAME,B.IDCARDNO,  A.BRANCH,D.DESIGNATION,     A.PAYPERIOD,     A.WDAYS NOOFDAYS,A.MDAYS-A.WDAYS LOP,A.EBASIC, A.EHRA,                 A.EDA,A.ESPL,A.PF ERPF,A.ESI ERESI,  A.MESSFEES EMESSFEES,(A.EOTHDED+ROUND((CASE WHEN A.ECDED > 0 THEN  ( A.WDAYS*(450/A.MDAYS) ) ELSE 0 END),2)+A.ETDED) EOTHDED,A.LOAN ADV,A.SADV,CASE WHEN A.BANK = 'CASH' THEN ' ' ELSE A.BANK END BANK,A.PAYCAT,A.TDSN IT,A.EGROSS, ROUND(A.NETPAY-ROUND((CASE WHEN A.ECDED > 0 THEN  ( A.WDAYS*(450/A.MDAYS) ) ELSE 0 END),2)+A.ECDED+A.ETRDED,0) NETPAY  ,A.BASIC*2 BASIC,A.DA*2 DA,A.HRA*2 HRA,A.SPL*2 SPL,  A.TOTDED+ROUND((CASE WHEN A.ECDED > 0 THEN  ( A.WDAYS*(450/A.MDAYS) ) ELSE 0 END),2)-A.ECDED-A.ETRDED TOTDED  ,(SELECT LOGO FROM EDOCIMAGE1 WHERE IMGNAME='LOGO') COMPLOGO, (SELECT LOGO FROM EDOCIMAGE1      WHERE IMGNAME='DGGM') DG,TO_CHAR(TO_DATE(G.STDT,'DD/MM/YY'),'DD/MM/YYYY')||' TO '||TO_CHAR(TO_DATE(G.ENDT,'DD/MM/YY'),'DD/MM/YYYY') PAYCAT1,'{EMPTYPE}' EMPTYPE ,A.WALLO*2 WALLO   ,   A.EWALLO  ,0 ETRDED   FROM  LOPPLHPAYROLL A JOIN HREMPLOYMAST B ON B.HREMPLOYMASTID=A.EMPID JOIN GTCOMPMAST C ON C.GTCOMPMASTID=B.COMPCODE    JOIN HRDESIGNATIONMAST D ON D.HRDESIGNATIONMASTID=A.DESIGNATION JOIN HREMPLOYDETAILS E ON E.HREMPLOYMASTID=B.HREMPLOYMASTID    JOIN HRBANDMAST F ON F.HRBANDMASTID=E.BAND JOIN MONTHLYPAYFRQ G ON G.PAYPERIOD=A.PAYPERIOD JOIN HRMFRQ H ON H.PAYPERIOD=A.PAYPERIOD WHERE      H.PAYPERIOD='" + Class.Users.PayPeriod + "' AND A.PAYPERIOD='" + Class.Users.PayPeriod + "' AND A.PCTYPE='ACTUAL' AND B.IDCARDNO='" + Class.Users.IDCARDNO + "' ) AA )XX,       (SELECT YY.TAKEN,YY.CLOSING,YY.IDCARD,YY.ELEAVE FROM (SELECT  DD.BRANCHNAME BRANCH,AA.IDCARD,BB.FNAME EMPNAME,BB.GENDER,EE.DISPNAME DEPT,FF.DESIGNATION,GG.BANDID ,      AA.SHIFTCNT WDAYS,AA.ELEAVE,AA.TAKEN,AA.TAKEN1,CASE WHEN (AA.ELEAVE-AA.TAKEN1) < 0  THEN 0 ELSE (AA.ELEAVE-AA.TAKEN1) END CLOSING FROM (SELECT A.IDCARD,SUM(A.SHIFTCNT) SHIFTCNT,      ROUND(SUM(A.ELEAVE),3) ELEAVE,SUM(A.TAKEN) TAKEN,SUM(A.TAKEN1) TAKEN1 FROM ( SELECT A.IDCARD,A.SHIFTCNT,0 ELEAVE,0 TAKEN,0 TAKEN1 FROM HREMPWDAY A       UNION ALL       SELECT A.EMPID IDCARD,A.SHIFTCNT,0 ELEAVE,0 TAKEN,0 TAKEN1 FROM HDATTA A,MONTHLYPAYFRQ B,HRLELIGIBLE C WHERE A.PAYPERIOD = B.PAYPERIOD AND B.PAYPERIOD1 = C.PAYPERIOD       AND A.DOCDATE <= (SELECT AA.ENDT FROM MONTHLYPAYFRQ AA WHERE AA.PAYPERIOD='" + Class.Users.PayPeriod + "') AND A.SHIFTCNT > 0 UNION ALL SELECT A.EMPID IDCARD,0 SHIFTCNT,      A.SHIFTCNT*A.LEAVE ELEAVE,0 TAKEN,0 TAKEN1 FROM HDATTA A,MONTHLYPAYFRQ B,HRLELIGIBLE C WHERE A.PAYPERIOD = B.PAYPERIOD AND B.PAYPERIOD1 = C.PAYPERIOD       AND A.DOCDATE <= (SELECT AA.ENDT FROM MONTHLYPAYFRQ AA WHERE AA.PAYPERIOD='" + Class.Users.PayPeriod + "') AND A.SHIFTCNT > 0       AND B.FINYR = (SELECT AA.FINYR FROM MONTHLYPAYFRQ AA    WHERE AA.PAYPERIOD='" + Class.Users.PayPeriod + "')       UNION ALL SELECT TO_NUMBER(A.IDNOCHK) IDCARD,0 SHIFTCNT,0 ELEAVE,(0-SUM(A.STKOPBAL)) TAKEN,(0-SUM(A.STKOPBAL)) TAKEN1 FROM HRLEAVEREGMAST A       WHERE A.LRDATE BETWEEN ( SELECT AA.STDT FROM MONTHLYPAYFRQ AA WHERE AA.PAYPERIOD ='" + Class.Users.PayPeriod + "' )       AND (SELECT AA.ENDT FROM MONTHLYPAYFRQ AA WHERE AA.PAYPERIOD='" + Class.Users.PayPeriod + "') AND A.LTYPE <> 'ENC'       AND A.FINYEAR = (SELECT AA.FINYR FROM MONTHLYPAYFRQ AA WHERE AA.PAYPERIOD='" + Class.Users.PayPeriod + "')         GROUP BY A.IDNOCHK UNION ALL SELECT TO_NUMBER(A.IDNOCHK) IDCARD,0 SHIFTCNT,0 ELEAVE,0 TAKEN,(0-SUM(A.STKOPBAL)) TAKEN1 FROM HRLEAVEREGMAST A       WHERE A.LRDATE < ( SELECT AA.STDT FROM MONTHLYPAYFRQ AA WHERE AA.PAYPERIOD ='" + Class.Users.PayPeriod + "' )       AND A.FINYEAR = (SELECT AA.FINYR FROM MONTHLYPAYFRQ AA WHERE AA.PAYPERIOD='" + Class.Users.PayPeriod + "')       GROUP BY A.IDNOCHK UNION ALL SELECT TO_NUMBER(A.IDNOCHK) IDCARD,0 SHIFTCNT,0 ELEAVE,0 TAKEN,(0-SUM(A.STKOPBAL)) TAKEN1 FROM HRLEAVEREGMAST A       WHERE A.LRDATE = ( SELECT AA.STDT FROM MONTHLYPAYFRQ AA WHERE AA.PAYPERIOD ='" + Class.Users.PayPeriod + "') AND A.LTYPE = 'ENC'       AND ( SELECT TRIM(TO_CHAR(AA.STDT,'DD/MM')) FROM MONTHLYPAYFRQ AA WHERE AA.PAYPERIOD ='" + Class.Users.PayPeriod + "') = '01/01'        AND A.FINYEAR = (SELECT AA.FINYR FROM MONTHLYPAYFRQ AA WHERE AA.PAYPERIOD='" + Class.Users.PayPeriod + "')         GROUP BY A.IDNOCHK        UNION ALL       SELECT TO_NUMBER(BB.IDCARD) IDCARD,A.OLHDCNT SHIFTCNT,0 ELEAVE,0 TAKEN,0 TAKEN1 FROM HRONDUTYDET A,HREMPLOYDETAILS BB, MONTHLYPAYFRQ B,HRLELIGIBLE C       WHERE A.IDCARD = BB.HREMPLOYDETAILSID AND A.PAYPERIOD = B.PAYPERIOD AND B.PAYPERIOD1 = C.PAYPERIOD       AND A.ODATE  <= (SELECT AA.ENDT FROM MONTHLYPAYFRQ AA WHERE AA.PAYPERIOD='" + Class.Users.PayPeriod + "')       UNION ALL       SELECT TO_NUMBER(BB.IDCARD) IDCARD,0 SHIFTCNT,A.OLHDCNT*0.05 ELEAVE,0 TAKEN,0 TAKEN1 FROM HRONDUTYDET A,HREMPLOYDETAILS BB, MONTHLYPAYFRQ B,HRLELIGIBLE C       WHERE A.IDCARD = BB.HREMPLOYDETAILSID AND A.PAYPERIOD = B.PAYPERIOD AND B.PAYPERIOD1 = C.PAYPERIOD       AND A.ODATE  <= (SELECT AA.ENDT FROM MONTHLYPAYFRQ AA WHERE AA.PAYPERIOD='" + Class.Users.PayPeriod + "')       AND B.FINYR = (SELECT AA.FINYR FROM MONTHLYPAYFRQ AA WHERE AA.PAYPERIOD='" + Class.Users.PayPeriod + "') ) A             GROUP BY A.IDCARD HAVING ( SUM(A.SHIFTCNT) >= 240 AND SUM(A.ELEAVE) > 0 ) ) AA,HREMPLOYMAST BB,HREMPLOYDETAILS CC,BRANCHMAST DD,GTDEPTDESGMAST EE,HRDESIGNATIONMAST FF,      HRBANDMAST GG,GTCOMPMAST HH WHERE AA.IDCARD = CC.IDCARD AND BB.HREMPLOYMASTID = CC.HREMPLOYMASTID AND BB.WORKNATURE = DD.BRANCHMASTID       AND EE.GTDEPTDESGMASTID = CC.DEPTNAME AND FF.HRDESIGNATIONMASTID = CC.DESIGNATION AND GG.HRBANDMASTID = CC.BAND AND HH.GTCOMPMASTID = BB.COMPCODE )  YY ) YYY WHERE XX.IDCARDNO=YYY.IDCARD(+) ORDER BY XX.FNAME";
                            }
                            else
                            {
                                sel0 = "SELECT XX.FINYEAR,XX.COMPCODE,XX.COMPNAME,XX.FNAME,XX.IDCARDNO,XX.BRANCH,XX.DESIGNATION,       XX.PAYPERIOD,XX.NOOFDAYS,XX.LOP,XX.EBASIC,XX.EHRA,XX.EDA,XX.ESPL,XX.ERPF,XX.ERESI,XX.EMESSFEES,XX.EOTHDED,XX.ADV,XX.SADV,       XX.BANK,XX.PAYCAT,        XX.IT,XX.EGROSS,XX.NETPAY,XX.BASIC,XX.DA,XX.HRA,XX.SPL,XX.TOTDED,XX.COMPLOGO,XX.DG,XX.PAYCAT1,NVL(YYY.TAKEN,0) TAKEN,  NVL(YYY.CLOSING,0) CLOSING,YYY.IDCARD,NVL(YYY.ELEAVE,0) ELEAVE,XX.EMPTYPE,XX.WALLO  test1 ,XX.EWALLO test2 ,XX.ETRDED test3  FROM (SELECT AA.FINYEAR,AA.COMPCODE,AA.COMPNAME,AA.FNAME,AA.IDCARDNO,AA.BRANCH,AA.DESIGNATION, AA.PAYPERIOD,AA.NOOFDAYS,AA.LOP,AA.EBASIC,AA.EHRA,AA.EDA,AA.ESPL,AA.ERPF,AA.ERESI,AA.EMESSFEES,AA.EOTHDED,     AA.ADV,AA.SADV,AA.BANK,AA.PAYCAT,                AA.IT,AA.EGROSS,AA.NETPAY,AA.BASIC,AA.DA,AA.HRA,AA.SPL,AA.TOTDED,AA.COMPLOGO,AA.DG,AA.PAYCAT1,AA.EMPTYPE ,AA.WALLO ,AA.EWALLO,AA.ETRDED      FROM ( SELECT A.FINYEAR,C.COMPCODE,C.COMPNAME,B.FNAME,B.IDCARDNO,A.BRANCH,D.DESIGNATION,     A.PAYPERIOD,     A.WDAYS NOOFDAYS,A.MDAYS-A.WDAYS LOP,A.EBASIC, A.EHRA,  A.EDA,A.ESPL,A.PF ERPF,A.ESI ERESI,A.MESSFEES EMESSFEES,(A.EOTHDED+A.ECDED+A.ETDED) EOTHDED,A.LOAN ADV,A.SADV,CASE WHEN A.BANK = 'CASH' THEN ' ' ELSE A.BANK END BANK,A.PAYCAT,                  A.TDSN IT,A.EGROSS,A.NETPAY,A.BASIC*2 BASIC,A.DA*2 DA,A.HRA*2 HRA,A.SPL*2 SPL,A.TOTDED,(SELECT LOGO FROM EDOCIMAGE1 WHERE IMGNAME='LOGO') COMPLOGO, (SELECT LOGO FROM EDOCIMAGE1     WHERE IMGNAME='DGGM') DG,TO_CHAR(TO_DATE(G.STDT,'DD/MM/YY'),'DD/MM/YYYY')||' TO '||TO_CHAR(TO_DATE(G.ENDT,'DD/MM/YY'),'DD/MM/YYYY') PAYCAT1,'{EMPTYPE}' EMPTYPE ,A.WALLO*2 WALLO   ,A.EWALLO  ,A.ETRDED   FROM  LOPPLHPAYROLL A JOIN HREMPLOYMAST B ON B.HREMPLOYMASTID=A.EMPID JOIN GTCOMPMAST C ON C.GTCOMPMASTID=B.COMPCODE JOIN HRDESIGNATIONMAST D ON D.HRDESIGNATIONMASTID=A.DESIGNATION JOIN HREMPLOYDETAILS E ON E.HREMPLOYMASTID=B.HREMPLOYMASTID JOIN HRBANDMAST F ON F.HRBANDMASTID=E.BAND JOIN MONTHLYPAYFRQ G ON G.PAYPERIOD=A.PAYPERIOD WHERE  A.PAYPERIOD= '" + Class.Users.PayPeriod + "' AND A.PCTYPE='ACTUAL' and B.IDCARDNO= '" + Class.Users.IDCARDNO + "' ) AA ) XX, (SELECT YY.TAKEN,YY.CLOSING,YY.IDCARD,YY.ELEAVE FROM (SELECT  DD.BRANCHNAME BRANCH,AA.IDCARD,BB.FNAME EMPNAME,BB.GENDER,EE.DISPNAME DEPT,FF.DESIGNATION,GG.BANDID ,AA.SHIFTCNT WDAYS,AA.ELEAVE,AA.TAKEN,AA.TAKEN1,CASE WHEN (AA.ELEAVE-AA.TAKEN1) < 0  THEN 0 ELSE (AA.ELEAVE-AA.TAKEN1) END CLOSING FROM (SELECT A.IDCARD,SUM(A.SHIFTCNT) SHIFTCNT,ROUND(SUM(A.ELEAVE),3) ELEAVE,SUM(A.TAKEN) TAKEN,SUM(A.TAKEN1) TAKEN1 FROM ( SELECT A.IDCARD,A.SHIFTCNT,0 ELEAVE,0 TAKEN,0 TAKEN1 FROM HREMPWDAY A UNION ALL SELECT A.EMPID IDCARD,A.SHIFTCNT,0 ELEAVE,0 TAKEN,0 TAKEN1 FROM HDATTA A,MONTHLYPAYFRQ B,HRLELIGIBLE C WHERE A.PAYPERIOD = B.PAYPERIOD AND B.PAYPERIOD1 = C.PAYPERIOD AND A.DOCDATE <= (SELECT AA.ENDT FROM MONTHLYPAYFRQ AA WHERE AA.PAYPERIOD= '" + Class.Users.PayPeriod + "') AND A.SHIFTCNT > 0 UNION ALL SELECT A.EMPID IDCARD,0 SHIFTCNT,A.SHIFTCNT*A.LEAVE ELEAVE,0 TAKEN,0 TAKEN1 FROM HDATTA A,MONTHLYPAYFRQ B,HRLELIGIBLE C WHERE A.PAYPERIOD = B.PAYPERIOD AND B.PAYPERIOD1 = C.PAYPERIOD AND A.DOCDATE <= (SELECT AA.ENDT FROM MONTHLYPAYFRQ AA WHERE AA.PAYPERIOD= '" + Class.Users.PayPeriod + "') AND A.SHIFTCNT > 0 AND B.FINYR = (SELECT AA.FINYR FROM MONTHLYPAYFRQ AA WHERE AA.PAYPERIOD= '" + Class.Users.PayPeriod + "') UNION ALL SELECT TO_NUMBER(A.IDNOCHK) IDCARD,0 SHIFTCNT,0 ELEAVE,(0-SUM(A.STKOPBAL)) TAKEN,(0-SUM(A.STKOPBAL)) TAKEN1 FROM HRLEAVEREGMAST A WHERE A.LRDATE BETWEEN ( SELECT AA.STDT FROM MONTHLYPAYFRQ AA WHERE AA.PAYPERIOD ='" + Class.Users.PayPeriod + "' ) AND (SELECT AA.ENDT FROM MONTHLYPAYFRQ AA WHERE AA.PAYPERIOD= '" + Class.Users.PayPeriod + "') AND A.LTYPE <> 'ENC' AND A.FINYEAR = (SELECT AA.FINYR FROM MONTHLYPAYFRQ AA WHERE AA.PAYPERIOD= '" + Class.Users.PayPeriod + "') GROUP BY A.IDNOCHK UNION ALL SELECT TO_NUMBER(A.IDNOCHK) IDCARD,0 SHIFTCNT,0 ELEAVE,0 TAKEN,(0-SUM(A.STKOPBAL)) TAKEN1 FROM HRLEAVEREGMAST A WHERE A.LRDATE < ( SELECT AA.STDT FROM MONTHLYPAYFRQ AA WHERE AA.PAYPERIOD ='" + Class.Users.PayPeriod + "' ) AND A.FINYEAR = (SELECT AA.FINYR FROM MONTHLYPAYFRQ AA WHERE AA.PAYPERIOD= '" + Class.Users.PayPeriod + "') GROUP BY A.IDNOCHK UNION ALL SELECT TO_NUMBER(A.IDNOCHK) IDCARD,0 SHIFTCNT,0 ELEAVE,0 TAKEN,(0-SUM(A.STKOPBAL)) TAKEN1 FROM HRLEAVEREGMAST A WHERE A.LRDATE = ( SELECT AA.STDT FROM MONTHLYPAYFRQ AA WHERE AA.PAYPERIOD ='" + Class.Users.PayPeriod + "' ) AND A.LTYPE = 'ENC' AND ( SELECT TRIM(TO_CHAR(AA.STDT,'DD/MM')) FROM MONTHLYPAYFRQ AA WHERE AA.PAYPERIOD ='" + Class.Users.PayPeriod + "') = '01/01'  AND A.FINYEAR = (SELECT AA.FINYR FROM MONTHLYPAYFRQ AA WHERE AA.PAYPERIOD= '" + Class.Users.PayPeriod + "')   GROUP BY A.IDNOCHK  UNION ALL SELECT TO_NUMBER(BB.IDCARD) IDCARD,A.OLHDCNT SHIFTCNT,0 ELEAVE,0 TAKEN,0 TAKEN1 FROM HRONDUTYDET A,HREMPLOYDETAILS BB, MONTHLYPAYFRQ B,HRLELIGIBLE C WHERE A.IDCARD = BB.HREMPLOYDETAILSID AND A.PAYPERIOD = B.PAYPERIOD AND B.PAYPERIOD1 = C.PAYPERIOD AND A.ODATE  <= (SELECT AA.ENDT FROM MONTHLYPAYFRQ AA WHERE AA.PAYPERIOD= '" + Class.Users.PayPeriod + "') UNION ALL SELECT TO_NUMBER(BB.IDCARD) IDCARD,0 SHIFTCNT,A.OLHDCNT*0.05 ELEAVE,0 TAKEN,0 TAKEN1 FROM HRONDUTYDET A,HREMPLOYDETAILS BB, MONTHLYPAYFRQ B,HRLELIGIBLE C WHERE A.IDCARD = BB.HREMPLOYDETAILSID AND A.PAYPERIOD = B.PAYPERIOD AND B.PAYPERIOD1 = C.PAYPERIOD AND A.ODATE  <= (SELECT AA.ENDT FROM MONTHLYPAYFRQ AA WHERE AA.PAYPERIOD= '" + Class.Users.PayPeriod + "') AND B.FINYR = (SELECT AA.FINYR FROM MONTHLYPAYFRQ AA WHERE AA.PAYPERIOD= '" + Class.Users.PayPeriod + "') ) A GROUP BY A.IDCARD HAVING ( SUM(A.SHIFTCNT) >= 240 AND SUM(A.ELEAVE) > 0 ) ) AA,HREMPLOYMAST BB,HREMPLOYDETAILS CC,BRANCHMAST DD,GTDEPTDESGMAST EE,HRDESIGNATIONMAST FF,HRBANDMAST GG,GTCOMPMAST HH WHERE AA.IDCARD = CC.IDCARD AND BB.HREMPLOYMASTID = CC.HREMPLOYMASTID AND BB.WORKNATURE = DD.BRANCHMASTID AND EE.GTDEPTDESGMASTID = CC.DEPTNAME AND FF.HRDESIGNATIONMASTID = CC.DESIGNATION AND GG.HRBANDMASTID = CC.BAND AND HH.GTCOMPMASTID = BB.COMPCODE )  YY ) YYY WHERE XX.IDCARDNO=YYY.IDCARD(+) ORDER BY XX.FNAME";

                            }

                            DataSet ds1 = Utility.ExecuteSelectQuery(sel0, "HREMPLOYMAST");
                            dtprint1 = ds1.Tables["HREMPLOYMAST"];
                            if (dtprint1.Rows.Count > 0)
                            {
                                string sel2 = "SELECT A.EMPNAME,A.IDCARDNO, A.signature,(SELECT AA.signature FROM ASPTBLEMP AA WHERE AA.ACTIVE='T') as msignature,to_char(c.doj,'dd-MM-YYYY') as dateofjoin,a.BYTESNAME as nameintamil,a.BYTESDESIGN as designintamil   FROM ASPTBLEMP A  JOIN GTCOMPMAST B ON A.COMPCODE=B.GTCOMPMASTID   join HREMPLOYDETAILS c on C.IDCARD=A.IDCARDNO WHERE A.IDCARDNO ='" + item.SubItems[3].Text + "' and  B.COMPCODE = '" + Class.Users.HCompcode + "' ";// where  A.FINYEAR='" + combofinyear.Text + "' AND  D.compcode='" + combocompcode.Text + "'  and a.payperiod='" + allip.Items[i].SubItems[7].Text + "' AND  C.IDCARD='" + s[1].Substring(7).TrimEnd() + "' order by 1";
                                DataSet ds2 = Utility.ExecuteSelectQuery(sel2, "ASPTBLEMP");
                                dtprint2 = ds2.Tables["ASPTBLEMP"];

                                string sel3 = "SELECT A.FROMDATE||' - '||A.TODATE PERIOD,A.BUYER OTMIN," +
                                    "ROUND(((((BASIC+DA+SPL)/26)/8)/60)*2*A.BUYER,2) OTAMT, " +
                                    " CASE WHEN B.ESI = 'YES' THEN ROUND(ROUND(((((BASIC+DA+SPL)/26)/8)/60)*2*A.BUYER,2)*0.75/100,2) ELSE 0 END ESI,  ROUND(((((BASIC+DA+SPL)/26)/8)/60)*2*A.BUYER,2) - CASE WHEN B.ESI = 'YES' THEN ROUND(ROUND(((((BASIC+DA+SPL)/26)/8)/60)*2*A.BUYER,2)*0.75/100,2) ELSE 0 END NETAMOUNT,B.IDCARD AS IDCARDNO FROM OTSTAMENT A JOIN HREMPLOYDETAILS B ON A.EMPMAID = B.HREMPLOYMASTID WHERE B.IDACTIVE = 'YES'  AND  A.PAYPERIOD ='" + item.SubItems[7].Text + "' AND B.IDCARD= '" + item.SubItems[3].Text + "' AND A.BUYER > 0  ORDER BY A.FROMDATE";
                                DataSet ds3 = Utility.ExecuteSelectQuery(sel3, "OTSTAMENT");
                                dtprint3 = ds3.Tables["OTSTAMENT"];
                                //rd1.Database.Tables["DataTable1"].SetDataSource(dtprint1); 
                                //rd1.Database.Tables["DataTable2"].SetDataSource(dtprint2);
                                //rd1.Database.Tables["DataTable3"].SetDataSource(dtprint3);


                                //if (!Directory.Exists(folderLocation1)) { Directory.CreateDirectory(folderLocation1); }
                               
                                //rd1.ExportToDisk(ExportFormatType.PortableDocFormat, folderLocation1 + item.Index.ToString() +" - "+item.SubItems[3].Text + "  - " + item.SubItems[5].Text + "  " + "PaySlip.pdf");

                            }
                            decimal per = Convert.ToDecimal(100 / GenFun.ToDecimal(allip.Items.Count)) * item.Index;
                            lblprogross4.Text = " Downloading.... : " + ((int)per).ToString("N0") + " %" + " IDCardNo:   " + item.SubItems[3].Text + "  ----- " + item.SubItems[5].Text + "           Count  :  " + item.Index + "   Total Record:   " + allip.Items.Count.ToString();
                            lblprogross4.Refresh();
                            progressBar2.Value = item.Index + 1;Class.Users.LoginTime = 1800;
                        }

                        if (!Directory.Exists(folderLocation1)) { Directory.CreateDirectory(folderLocation1); }
                        rd1.Database.Tables["DataTable1"].SetDataSource(dtprint1);
                        rd1.Database.Tables["DataTable2"].SetDataSource(dtprint2);
                        rd1.Database.Tables["DataTable3"].SetDataSource(dtprint3);
                        rd1.ExportToDisk(ExportFormatType.PortableDocFormat, folderLocation1 + "  " + "PaySlip.pdf");

                        Cursor = Cursors.Default; progressBar2.Value = 0; lblprogross4.Text = ""; lblprogross4.Visible = false;

                        MessageBox.Show("PaySlip Download Completed in D drive");
                    }
                    if (combostatement.Text == "STATEMENT")
                    {
                        string folderLocation = "D:\\StatementDownload\\" + combopayperiod.Text + "\\";
                        if (!Directory.Exists(folderLocation)) { Directory.CreateDirectory(folderLocation); }

                        crystalReportViewer1.ReportSource = null; crystalReportViewer1.Refresh();
                        string sel3 = "SELECT LOGO  FROM EDOCIMAGE1   WHERE IMGNAME='LOGO'";
                        DataSet ds3 = Utility.ExecuteSelectQuery(sel3, "EDOCIMAGE1");
                        DataTable dt3 = ds3.Tables["EDOCIMAGE1"];
                        for (i = 0; i < allip.Items.Count; i++)
                        {
                            string sel0 = ""; DataSet ds0; DataTable dt0;
                            if (Class.Users.HUserName == "GROUP1" || Class.Users.HUserName == "GROUP2" || Class.Users.HUserName == "GROUP3" || Class.Users.HUserName == "GROUP4")
                            {
                                sel0 = "SELECT AA.BRANCH,AA.BANDID,AA.GENDER,AA.IDCARD,AA.EMPNAME,AA.MACHINENAME,DENSE_RANK() OVER (PARTITION BY AA.GENDER,AA.BRANCH,AA.BANDID ORDER BY AA.EMPNAME) SNO,     AA.BASICPAY,AA.ACTWORKDAYS,AA.SALRYDAYS,AA.EBASIC,AA.EDA,AA.EHRA,ROUND(AA.EGROSS,2) EGROSS,AA.ESI,AA.PF,AA.TDSN,AA.MESSFEES, AA.OTHDED,AA.TOTDED,AA.BANK,AA.NETPAY,   AA.SADV,AA.LOAN, AA.ETDED,AA.ECDED,AA.STDT,AA.ENDT,AA.COMPNAME,(select bb.SIGNATURE from  asptblemp bb where BB.IDCARDNO IN '" + allip.Items[i].SubItems[3].Text + "' ) as  SIGNATURE,'' Test1,aa.ewallo as Test2,aa.etrded as Test3       FROM ( SELECT A.BRANCH,D.BANDID,C.GENDER,TO_NUMBER(B.IDCARD) IDCARD,C.FNAME EMPNAME,G.DESIGNATION MACHINENAME, A.BASIC*2 BASICPAY,F.WEEKLYHOLIDAYS+A.WDAYS ACTWORKDAYS,       A.WDAYS SALRYDAYS,A.EBASIC,A.EDA,A.ESPL EHRA,A.EGROSS,A.ESI,A.PF,A.TDSN,A.MESSFEES, A.EOTHDED OTHDED,  A.TOTDED-A.ECDED-A.ETRDED+(CASE WHEN A.ECDED > 0 THEN ROUND((A.WDAYS*(450/A.MDAYS)),2) ELSE 0 END) TOTDED,A.BANK,  ROUND(A.NETPAY+A.ECDED+A.ETRDED-(CASE WHEN A.ECDED > 0 THEN ROUND((A.WDAYS*(450/A.MDAYS)),2) ELSE 0 END),0) NETPAY,'' SIGNATURE,A.SADV,A.LOAN,H.COMPNAME,  A.ETDED,CASE WHEN A.ECDED > 0 THEN ROUND((A.WDAYS*(450/A.MDAYS)),2) ELSE 0 END+A.ETDED ECDED,      TO_CHAR(F.STDT,'dd/mm/yyyy') STDT,TO_CHAR(F.ENDT,'dd/mm/yyyy') ENDT,A.EWALLO,  A.ETRDED-A.ETRDED ETRDED    FROM LOPPLHPAYROLL A,HREMPLOYDETAILS B,HREMPLOYMAST C,HRBANDMAST D,MACHINEMAST E,MONTHLYPAYFRQ F,     HRDESIGNATIONMAST G,GTCOMPMAST H WHERE A.EMPID = B.HREMPLOYMASTID AND B.HREMPLOYMASTID = C.HREMPLOYMASTID AND H.GTCOMPMASTID=C.COMPCODE AND B.BAND = D.HRBANDMASTID      AND E.MACHINEMASTID = B.MACNAME AND F.PAYPERIOD = A.PAYPERIOD AND G.HRDESIGNATIONMASTID = B.DESIGNATION AND A.PCTYPE = 'ACTUAL'      AND A.PAYPERIOD ='" + allip.Items[i].SubItems[7].Text + "' AND H.COMPCODE='" + allip.Items[i].SubItems[6].Text + "'  and B.IDCARD='" + allip.Items[i].SubItems[3].Text + "' AND A.BANK NOT IN ('BY CASH','CASH') AND (A.NETPAY > 0 OR A.WDAYS > 0 )      UNION ALL SELECT A.BRANCH,D.BANDID,C.GENDER,TO_NUMBER(B.IDCARD) IDCARD,C.FNAME EMPNAME,G.DESIGNATION MACHINENAME, A.BASIC*2 BASICPAY,F.WEEKLYHOLIDAYS+A.WDAYS ACTWORKDAYS,     A.WDAYS SALRYDAYS,A.EBASIC,A.EDA,A.ESPL EHRA,A.EGROSS,A.ESI,A.PF,A.TDSN,A.MESSFEES, A.EOTHDED OTHDED,A.TOTDED,A.BANK,A.NETPAY,'' SIGNATURE,A.SADV,A.LOAN,H.COMPNAME,A.ETDED,     A.ECDED+A.ETDED ECDED, TO_CHAR(F.STDT,'dd/mm/yyyy') STDT,TO_CHAR(F.ENDT,'dd/mm/yyyy') ENDT,A.EWALLO,A.ETRDED-A.ETRDED ETRDED     FROM LOPPLHPAYROLL A,HREMPLOYDETAILS B,HREMPLOYMAST C,HRBANDMAST D,MACHINEMAST E,MONTHLYPAYFRQ F ,HRDESIGNATIONMAST G,GTCOMPMAST H WHERE A.EMPID = B.HREMPLOYMASTID      AND B.HREMPLOYMASTID = C.HREMPLOYMASTID AND H.GTCOMPMASTID=C.COMPCODE AND B.BAND = D.HRBANDMASTID AND E.MACHINEMASTID = B.MACNAME AND F.PAYPERIOD = A.PAYPERIOD      AND G.HRDESIGNATIONMASTID = B.DESIGNATION AND A.PCTYPE = 'ACTUAL' AND A.PAYPERIOD ='" + allip.Items[i].SubItems[7].Text + "' AND H.COMPCODE='" + allip.Items[i].SubItems[6].Text + "'  and B.IDCARD='" + allip.Items[i].SubItems[3].Text + "' AND A.BANK IN ('BY CASH','CASH')       AND C.EMPTYPE = 'Permanent' AND (A.NETPAY > 0 OR A.WDAYS > 0 )  UNION ALL SELECT A.BRANCH,D.BANDID,C.GENDER,TO_NUMBER(B.IDCARD) IDCARD,C.FNAME EMPNAME,G.DESIGNATION MACHINENAME,      A.BASIC*2 BASICPAY,F.WEEKLYHOLIDAYS+A.WDAYS ACTWORKDAYS,A.WDAYS SALRYDAYS,A.EBASIC,A.EDA,A.ESPL EHRA,A.EGROSS,A.ESI,A.PF,A.TDSN,A.MESSFEES, A.EOTHDED OTHDED,A.TOTDED,     A.BANK,A.NETPAY,'' SIGNATURE,A.SADV,A.LOAN,H.COMPNAME,A.ETDED,A.ECDED+A.ETDED ECDED, TO_CHAR(F.STDT,'dd/mm/yyyy') STDT,TO_CHAR(F.ENDT,'dd/mm/yyyy') ENDT,A.EWALLO,A.ETRDED-A.ETRDED ETRDED FROM LOPPLHPAYROLL A,HREMPLOYDETAILS B,HREMPLOYMAST C,HRBANDMAST D,MACHINEMAST E,MONTHLYPAYFRQ F ,HRDESIGNATIONMAST G,GTCOMPMAST H WHERE A.EMPID = B.HREMPLOYMASTID AND B.HREMPLOYMASTID = C.HREMPLOYMASTID AND H.GTCOMPMASTID=C.COMPCODE AND B.BAND = D.HRBANDMASTID AND E.MACHINEMASTID = B.MACNAME AND F.PAYPERIOD = A.PAYPERIOD AND G.HRDESIGNATIONMASTID = B.DESIGNATION  AND A.PCTYPE = 'ACTUAL'  AND A.PAYPERIOD ='" + allip.Items[i].SubItems[7].Text + "' AND H.COMPCODE='" + allip.Items[i].SubItems[6].Text + "'  and B.IDCARD='" + allip.Items[i].SubItems[3].Text + "' AND A.BANK IN ('BY CASH','CASH')   AND C.EMPTYPE = 'Trainee'  AND (A.NETPAY > 0 OR A.WDAYS > 0 )  ) AA   ORDER BY 3,2";
                                ds0 = Utility.ExecuteSelectQuery(sel0, "HREMPLOYMAST");
                                dt0 = ds0.Tables["HREMPLOYMAST"];
                            }
                            else
                            {

                                sel0 = "SELECT AA.BRANCH,AA.BANDID,AA.GENDER,AA.IDCARD,AA.EMPNAME,AA.MACHINENAME,DENSE_RANK() OVER (PARTITION BY AA.GENDER,AA.BRANCH,AA.BANDID ORDER BY AA.EMPNAME) SNO,     AA.BASICPAY,AA.ACTWORKDAYS,AA.SALRYDAYS,AA.EBASIC,AA.EDA,AA.EHRA,ROUND(AA.EGROSS,2) EGROSS,AA.ESI,AA.PF,AA.TDSN,AA.MESSFEES, AA.OTHDED,AA.TOTDED,AA.BANK,AA.NETPAY,     AA.SADV,AA.LOAN, AA.ETDED,AA.ECDED,AA.STDT,AA.ENDT,AA.COMPNAME,(select bb.SIGNATURE from  asptblemp bb where BB.IDCARDNO IN'" + allip.Items[i].SubItems[3].Text + "' ) as  SIGNATURE,''TEST1 ,aa.ewallo as Test2,aa.etrded as Test3     FROM ( SELECT A.BRANCH,D.BANDID,C.GENDER,TO_NUMBER(B.IDCARD) IDCARD,C.FNAME EMPNAME,G.DESIGNATION MACHINENAME, A.BASIC*2 BASICPAY,F.WEEKLYHOLIDAYS+A.WDAYS ACTWORKDAYS,     A.WDAYS SALRYDAYS,A.EBASIC,A.EDA,A.ESPL EHRA,A.EGROSS,A.ESI,A.PF,A.TDSN,A.MESSFEES, A.EOTHDED OTHDED,A.TOTDED,A.BANK,A.NETPAY,'' SIGNATURE,A.SADV,A.LOAN,H.COMPNAME,A.ETDED,A.ECDED+A.ETDED ECDED,      TO_CHAR(F.STDT,'dd/mm/yyyy') STDT,TO_CHAR(F.ENDT,'dd/mm/yyyy') ENDT,A.EWALLO,A.ETRDED FROM LOPPLHPAYROLL A,HREMPLOYDETAILS B,HREMPLOYMAST C,HRBANDMAST D,MACHINEMAST E,MONTHLYPAYFRQ F,     HRDESIGNATIONMAST G,GTCOMPMAST H WHERE A.EMPID = B.HREMPLOYMASTID AND B.HREMPLOYMASTID = C.HREMPLOYMASTID AND H.GTCOMPMASTID=C.COMPCODE AND B.BAND = D.HRBANDMASTID      AND E.MACHINEMASTID = B.MACNAME AND F.PAYPERIOD = A.PAYPERIOD AND G.HRDESIGNATIONMASTID = B.DESIGNATION AND A.PCTYPE = 'ACTUAL'      AND A.PAYPERIOD ='" + allip.Items[i].SubItems[7].Text + "' AND H.COMPCODE='" + allip.Items[i].SubItems[6].Text + "'  and B.IDCARD='" + allip.Items[i].SubItems[3].Text + "' AND A.BANK NOT IN ('BY CASH','CASH') AND (A.NETPAY > 0 OR A.WDAYS > 0 )      UNION ALL SELECT A.BRANCH,D.BANDID,C.GENDER,TO_NUMBER(B.IDCARD) IDCARD,C.FNAME EMPNAME,G.DESIGNATION MACHINENAME, A.BASIC*2 BASICPAY,F.WEEKLYHOLIDAYS+A.WDAYS ACTWORKDAYS,     A.WDAYS SALRYDAYS,A.EBASIC,A.EDA,A.ESPL EHRA,A.EGROSS,A.ESI,A.PF,A.TDSN,A.MESSFEES, A.EOTHDED OTHDED,A.TOTDED,A.BANK,A.NETPAY,'' SIGNATURE,A.SADV,A.LOAN,H.COMPNAME,A.ETDED,     A.ECDED+A.ETDED ECDED, TO_CHAR(F.STDT,'dd/mm/yyyy') STDT,TO_CHAR(F.ENDT,'dd/mm/yyyy') ENDT,A.EWALLO,A.ETRDED      FROM LOPPLHPAYROLL A,HREMPLOYDETAILS B,HREMPLOYMAST C,HRBANDMAST D,MACHINEMAST E,MONTHLYPAYFRQ F ,HRDESIGNATIONMAST G,GTCOMPMAST H WHERE A.EMPID = B.HREMPLOYMASTID      AND B.HREMPLOYMASTID = C.HREMPLOYMASTID AND H.GTCOMPMASTID=C.COMPCODE AND B.BAND = D.HRBANDMASTID AND E.MACHINEMASTID = B.MACNAME AND F.PAYPERIOD = A.PAYPERIOD      AND G.HRDESIGNATIONMASTID = B.DESIGNATION AND A.PCTYPE = 'ACTUAL' AND A.PAYPERIOD ='" + allip.Items[i].SubItems[7].Text + "' AND H.COMPCODE='" + allip.Items[i].SubItems[6].Text + "'  and B.IDCARD='" + allip.Items[i].SubItems[3].Text + "' AND A.BANK IN ('BY CASH','CASH')       AND C.EMPTYPE = 'Permanent' AND (A.NETPAY > 0 OR A.WDAYS > 0 )  UNION ALL SELECT A.BRANCH,D.BANDID,C.GENDER,TO_NUMBER(B.IDCARD) IDCARD,C.FNAME EMPNAME,G.DESIGNATION MACHINENAME,      A.BASIC*2 BASICPAY,F.WEEKLYHOLIDAYS+A.WDAYS ACTWORKDAYS,A.WDAYS SALRYDAYS,A.EBASIC,A.EDA,A.ESPL EHRA,A.EGROSS,A.ESI,A.PF,A.TDSN,A.MESSFEES, A.EOTHDED OTHDED,A.TOTDED,     A.BANK,A.NETPAY,'' SIGNATURE,A.SADV,A.LOAN,H.COMPNAME,A.ETDED,A.ECDED+A.ETDED ECDED, TO_CHAR(F.STDT,'dd/mm/yyyy') STDT,TO_CHAR(F.ENDT,'dd/mm/yyyy') ENDT,A.EWALLO,A.ETRDED FROM LOPPLHPAYROLL A,HREMPLOYDETAILS B,HREMPLOYMAST C,HRBANDMAST D,MACHINEMAST E,MONTHLYPAYFRQ F ,HRDESIGNATIONMAST G,GTCOMPMAST H WHERE A.EMPID = B.HREMPLOYMASTID AND B.HREMPLOYMASTID = C.HREMPLOYMASTID AND H.GTCOMPMASTID=C.COMPCODE AND B.BAND = D.HRBANDMASTID AND E.MACHINEMASTID = B.MACNAME AND F.PAYPERIOD = A.PAYPERIOD AND G.HRDESIGNATIONMASTID = B.DESIGNATION  AND A.PCTYPE = 'ACTUAL' AND A.PAYPERIOD ='" + allip.Items[i].SubItems[7].Text + "' AND H.COMPCODE='" + allip.Items[i].SubItems[6].Text + "'  and B.IDCARD='" + allip.Items[i].SubItems[3].Text + "' AND A.BANK IN ('BY CASH','CASH')   AND C.EMPTYPE = 'Trainee'  AND (A.NETPAY > 0 OR A.WDAYS > 0 )  ) AA   ORDER BY 3,2";

                                ds0 = Utility.ExecuteSelectQuery(sel0, "HREMPLOYMAST");
                                dt0 = ds0.Tables["HREMPLOYMAST"];
                            }
                            if (dt0.Rows.Count > 0)
                            {
                                foreach (DataRow porow in dt0.Rows)
                                {
                                    if (dtprint0 == null)
                                    {
                                        dtprint0 = dt0.Clone();

                                    }
                                    dtprint0.Rows.Add();
                                    string sel1 = "SELECT A.SIGNATURE FROM ASPTBLEMP A WHERE A.IDCARDNO='" + allip.Items[i].SubItems[3].Text + "'";
                                    DataSet ds1 = Utility.ExecuteSelectQuery(sel1, "ASPTBLEMP");
                                    DataTable dt1 = ds1.Tables["ASPTBLEMP"];
                                    for (int p = 0; p < dt1.Rows.Count; p++)
                                    {
                                        if (dt1.Rows.Count > 1)
                                        {
                                            dt1.Rows.RemoveAt(p);
                                        }
                                    }
                                    dtprint0.Rows[i]["TEST1"] = combopayperiod.Text;
                                    dtprint0.Rows[i]["BRANCH"] = porow.ItemArray[0].ToString();
                                    dtprint0.Rows[i]["BANDID"] = porow.ItemArray[1].ToString();
                                    dtprint0.Rows[i]["GENDER"] = porow.ItemArray[2].ToString();
                                    dtprint0.Rows[i]["IDCARD"] = porow.ItemArray[3].ToString();
                                    dtprint0.Rows[i]["EMPNAME"] = porow.ItemArray[4].ToString();
                                    dtprint0.Rows[i]["MACHINENAME"] = porow.ItemArray[5].ToString();
                                    dtprint0.Rows[i]["SNO"] = i.ToString();
                                    dtprint0.Rows[i]["BASICPAY"] = porow.ItemArray[7].ToString();
                                    dtprint0.Rows[i]["ACTWORKDAYS"] = porow.ItemArray[8].ToString();
                                    dtprint0.Rows[i]["SALRYDAYS"] = porow.ItemArray[9].ToString();

                                    dtprint0.Rows[i]["EBASIC"] = porow.ItemArray[10].ToString();
                                    dtprint0.Rows[i]["EDA"] = porow.ItemArray[11].ToString();
                                    dtprint0.Rows[i]["EHRA"] = porow.ItemArray[12].ToString();
                                    dtprint0.Rows[i]["EGROSS"] = porow.ItemArray[13].ToString();
                                    dtprint0.Rows[i]["ESI"] = porow.ItemArray[14].ToString();
                                    dtprint0.Rows[i]["PF"] = porow.ItemArray[15].ToString();
                                    dtprint0.Rows[i]["TDSN"] = porow.ItemArray[16].ToString();
                                    dtprint0.Rows[i]["MESSFEES"] = porow.ItemArray[17].ToString();
                                    dtprint0.Rows[i]["OTHDED"] = porow.ItemArray[18].ToString();

                                    dtprint0.Rows[i]["TOTDED"] = porow.ItemArray[19].ToString();

                                    dtprint0.Rows[i]["BANK"] = porow.ItemArray[20].ToString();

                                    dtprint0.Rows[i]["NETPAY"] = porow.ItemArray[21].ToString();
                                    dtprint0.Rows[i]["SADV"] = porow.ItemArray[22].ToString();
                                    if (porow.ItemArray[23].ToString() != "")
                                    {
                                        dtprint0.Rows[i]["LOAN"] = porow.ItemArray[23].ToString();
                                    }
                                    if (porow.ItemArray[24].ToString() != "")
                                    {
                                        dtprint0.Rows[i]["ETDED"] = porow.ItemArray[24].ToString();
                                    }
                                    if (porow.ItemArray[25].ToString() != "")
                                    {
                                        dtprint0.Rows[i]["ECDED"] = porow.ItemArray[25].ToString();
                                    }
                                    if (porow.ItemArray[26].ToString() != "")
                                    {
                                        dtprint0.Rows[i]["STDT"] = porow.ItemArray[26].ToString();
                                    }
                                    if (porow.ItemArray[27].ToString() != "")
                                    {
                                        dtprint0.Rows[i]["ENDT"] = porow.ItemArray[27].ToString();
                                    }
                                    dtprint0.Rows[i]["COMPNAME"] = porow.ItemArray[28].ToString();

                                    if (dt1.Rows.Count > 0)
                                    {
                                        foreach (DataRow myRow in dt1.Rows)
                                        {

                                            bytes = (byte[])myRow.ItemArray[0];

                                            dtprint0.Rows[i]["SIGNATURE"] = bytes;



                                        }
                                    }


                                    dtprint0.Rows[i]["TEST2"] = Convert.ToDouble("0" + porow.ItemArray[31].ToString());
                                    dtprint0.Rows[i]["TEST3"] = Convert.ToDouble("0" + porow.ItemArray[32].ToString());

                                }

                            }
                            else
                            {
                                dtprint0.Rows.Add();
                                dtprint0.Rows[i]["IDCARD"] = allip.Items[i].SubItems[3].Text;
                                dtprint0.Rows[i]["EMPNAME"] = allip.Items[i].SubItems[4].Text;
                                dtprint0.Rows[i]["SNO"] = i.ToString();
                            }


                            if (dtprint0.Rows.Count > 0)
                            {

                                lblprogross4.Visible = true;
                                lblprogross4.Refresh();
                                progressBar2.Value = i;
                                decimal per = Convert.ToDecimal(100 / GenFun.ToDecimal(allip.Items.Count)) * (i);
                                lblprogross4.Text = " Processing.... : " + ((int)per).ToString("N0") + " %" + " IDCardNo:   " + allip.Items[i].SubItems[3].Text + "     " + allip.Items[i].SubItems[5].Text + "        Count  :  " + i + "   Total Record:   " + allip.Items.Count.ToString();


                                rd.Database.Tables["DataTable1"].SetDataSource(dtprint0);
                                rd.Database.Tables["DataTable3"].SetDataSource(dt3);
                              




                                //datasave();

                            }
                          


                        }
                        if (dtprint0.Rows.Count > 0)
                        {
                            rd.ExportToDisk(ExportFormatType.PortableDocFormat, folderLocation + i.ToString() + " - " + "ALLPaySlip.pdf");
                        }
                        MessageBox.Show("Statement Download Completed in D drive");
                        empty(); lblprogross4.Text = ""; progressBar2.Value = 0;
                    }

                    Cursor = Cursors.Default;
                    lblprogross4.Text = ""; progressBar2.Value = 0; empty();
                }
                else
                {
                    MessageBox.Show("Pls Select Checkbox in ListView .");
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        




        }
        private void datasave()
        {
            //string ins = "insert into ASPTBLPRINT(FINYEAR,COMPCODE,PAYPERIOD,IDCARDNO,EMPNAME,IPADDRESS,CREATEDON,CREATEDBY,MODIFIEDON,DATETIME)values('" + Class.Users.Finyear + "','" + Class.Users.COMPCODE + "','" + allip.Items[i].SubItems[7].Text + "','" + allip.Items[i].SubItems[3].Text + "','" + Class.Users.HostelName + "','" + Class.Users.IPADDRESS + "','" + System.DateTime.Now.ToString() + "','" + Class.Users.HUserName + "','" + System.DateTime.Now.ToString() + "',to_date('" + System.DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss") + "', 'dd-MM-yyyy HH:mi:ss'))";
            //Utility.ExecuteNonQuery(ins);
        }
     

        public void Prints()
    {
            if (printDialog1.ShowDialog() == DialogResult.OK)//,b.intime,b.intime1, b.outtime,b.outtime1,c.address,c.pincode 
            {
               
                
            }
           
    }

    private void txtsearch1_TextChanged(object sender, EventArgs e)
    {
        try
        {
                Class.Users.UserTime = 0;
                int item0 = 0; listView2.Items.Clear();
            if (txtsearch1.Text.Length >= 1)
            {
                foreach (ListViewItem item in listfilter2.Items)
                {
                    ListViewItem list = new ListViewItem();
                    if (listfilter2.Items[item0].SubItems[3].ToString().Contains(txtsearch1.Text.ToUpper()) || listfilter2.Items[item0].SubItems[4].ToString().Contains(txtsearch1.Text.ToUpper()) || listfilter2.Items[item0].SubItems[5].ToString().Contains(txtsearch1.Text.ToUpper()))
                    {
                        list.Text = listfilter2.Items[item0].SubItems[0].Text;
                        list.SubItems.Add(listfilter2.Items[item0].SubItems[1].Text);
                        list.SubItems.Add(listfilter2.Items[item0].SubItems[2].Text);
                        list.SubItems.Add(listfilter2.Items[item0].SubItems[3].Text);
                        list.SubItems.Add(listfilter2.Items[item0].SubItems[4].Text);
                        list.SubItems.Add(listfilter2.Items[item0].SubItems[5].Text);
                        list.SubItems.Add(listfilter2.Items[item0].SubItems[6].Text);
                        list.SubItems.Add(listfilter2.Items[item0].SubItems[7].Text);
                        if (item0 % 2 == 0)
                        {
                            list.BackColor = Color.WhiteSmoke;
                        }
                        else
                        {
                            list.BackColor = Color.White;
                        }
                        listView2.Items.Add(list);


                    }
                    item0++;
                }
                lbltotal2.Text = "Total Count: " + listView2.Items.Count;
            }
            else
            {
                ListView ll = new ListView();
                listView2.Items.Clear();
                foreach (ListViewItem item in listfilter2.Items)
                {


                    this.listView2.Items.Add((ListViewItem)item.Clone());

                    if (item0 % 2 == 0)
                    {
                        item.BackColor = Color.WhiteSmoke;
                    }
                    else
                    {
                        item.BackColor = Color.White;
                    }

                    item0++;
                }
                lbltotal2.Text = "Total Count: " + listView2.Items.Count;
            }


        }
        catch (Exception ex)
        {
            //MessageBox.Show("---" + ex.ToString());
        }
    }
         
      
        private void combomonth_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }



    private void combobranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToInt64(combobranch.SelectedValue) > 0)
            {
                Cursor = Cursors.WaitCursor;
                    string sel1 = "select '' as PAYMONTH  from dual   union all select    A.PAYMONTH  from asptblpayslipper a   JOIN GTCOMPMAST D ON D.GTCOMPMASTID=A.COMPCODE      join asptblusermas e on E.COMPCODE = D.GTCOMPMASTID  AND E.USERNAME = A.USERNAME where D.compcode='" + combobranch.Text + "'  order by 1";
                DataSet ds1 = Utility.ExecuteSelectQuery(sel1, "asptblpayslipper");
                DataTable dt1 = ds1.Tables["asptblpayslipper"];
                combopayperiod.DisplayMember = "PAYMONTH";
                combopayperiod.ValueMember = "PAYMONTH";
                combopayperiod.DataSource = dt1;
                Cursor = Cursors.Default;
            }
        }
        catch (Exception ex)
        {

        }
    }

  
    private void refreshToolStripMenuItem1_Click(object sender, EventArgs e)
    {

    }


        private void chkall_CheckedChanged(object sender, EventArgs e)
        {
            if (chkall.Checked == true)
            {
                int listCount = listView2.Items.Count;
                foreach (ListViewItem item in listView2.Items)
                {
                    item.Checked = true;
                }
            }
            else
            {
                foreach (ListViewItem item in listView2.Items)
                {
                    item.Checked = false;
                }
            }
        }

   

   
    private void listView2_ItemChecked(object sender, ItemCheckedEventArgs e)
    {
        try
        {
            
            
                ListViewItem it2 = new ListViewItem();
                if (e.Item.Checked == true)
                {
                    e.Item.SubItems[0].Text = "T";
                    e.Item.BackColor = System.Drawing.SystemColors.MenuHighlight;
                    e.Item.ForeColor = Color.White;
                    it2.SubItems.Add(e.Item.SubItems[1].Text);
                    it2.SubItems.Add(e.Item.SubItems[2].Text);
                    it2.SubItems.Add(e.Item.SubItems[3].Text);
                    it2.SubItems.Add(e.Item.SubItems[4].Text);
                    it2.SubItems.Add(e.Item.SubItems[5].Text);
                    it2.SubItems.Add(e.Item.SubItems[6].Text);
                    it2.SubItems.Add(e.Item.SubItems[7].Text);
                    allip.Items.Add(it2);
                    Cursor = Cursors.Default;
                   
                }
                if (e.Item.Checked == false && e.Item.SubItems[0].Text == "T")
                {

                    e.Item.SubItems[0].Text = "F"; e.Item.BackColor = System.Drawing.SystemColors.ControlLightLight;
                    e.Item.ForeColor = Color.Black;
                    for (int c = 0; c < allip.Items.Count; c++)
                    {

                        if (e.Item.SubItems[3].Text == allip.Items[c].SubItems[3].Text)
                        {
                            allip.Items[c].Remove();
                            e.Item.SubItems[0].Text = "";
                            c--;
                           
                        }
                    }

                }

           
           
        }
        catch (Exception ex) { MessageBox.Show("       ---  " + e.Item.ToString() + "             ======================" + ex.ToString()); }
    }

   
   
  

  
 

    private void checkword_CheckedChanged(object sender, EventArgs e)
    {
      
    }

    private void lblprogross4_Click(object sender, EventArgs e)
    {

    }

        
        public void Saves()
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

       

        public void ChangePasswords()
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

        private void chechide_CheckedChanged(object sender, EventArgs e)
        {
            //if (chechide.Checked == true)
            //{
            //    listView2.Visible = false;

            //    this.crystalReportViewer1.ActiveViewIndex = -1;
            //    this.crystalReportViewer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Left)       | System.Windows.Forms.AnchorStyles.Right)));       
            //    this.crystalReportViewer1.Cursor = System.Windows.Forms.Cursors.Default;
            //    this.crystalReportViewer1.Font = Class.Users.FontName;              
            //    this.crystalReportViewer1.ForeColor = System.Drawing.Color.Transparent;
            //    this.crystalReportViewer1.Location = new System.Drawing.Point(9, 74);
            //    this.crystalReportViewer1.Name = "crystalReportViewer1";
            //    this.crystalReportViewer1.Size = new System.Drawing.Size(1330, 460);
            //    this.crystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            //    this.crystalReportViewer1.ToolPanelWidth = 205;


            //              }
            //else
            //{
            //    listView2.Visible = true;
             
            //    this.crystalReportViewer1.Location = new System.Drawing.Point(9, 74);
            //    this.crystalReportViewer1.Size = new System.Drawing.Size(916, 460);
            //}
        }

        private void combostatement_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void buttonok_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor; 
            GridLoad();
            Cursor = Cursors.Default; Class.Users.UserTime = 0;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
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

        public void ReadOnlys()
        {
            throw new NotImplementedException();
        }
    }
}
