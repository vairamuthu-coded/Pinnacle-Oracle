using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace Pinnacle.ReportFormate
{
    public partial class MovementReport : Form,ToolStripAccess
    {
      

        private static MovementReport _instance;
        public static MovementReport Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new MovementReport();
                GlobalVariables.CurrentForm = _instance; return _instance;
            }
        }
        public MovementReport()
        {
            InitializeComponent();
            GlobalVariables.HeaderName.Text = Class.Users.ScreenName; GlobalVariables.CurrentForm = this;
            butheader.BackColor = Class.Users.BackColors;
            butfooter.BackColor = Class.Users.BackColors;
            
        }

  

        Models.UserRights sm = new Models.UserRights(); Models.Master mas = new Models.Master();
        Report.MovementReport rd = new Report.MovementReport();
        Report.MovementReport rd2 = new Report.MovementReport();
       // Report.MovementReport1 rd1 = new Report.MovementReport1();
        DataTable dtgeneral = new DataTable();


        public void ReadOnlys()
        {

        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void COMBCODELOAD()
        {
            try
            {
                DataTable dt3 = new DataTable();
                
                    string sel3 = "SELECT 0 GTCOMPMASTID, ''COMPCODE FROM dual  union all  SELECT DISTINCT B.GTCOMPMASTID, B.COMPCODE FROM HOSTELLIVEDATA A JOIN GTCOMPMAST B ON B.COMPCODE= A.COMPCODE UNION ALL SELECT DISTINCT A.GTCOMPMASTID, A.COMPCODE FROM  GTCOMPMAST A WHERE A.COMPCODE='AGF' ORDER BY 1";
                    DataSet ds3 = Utility.ExecuteSelectQuery(sel3, "GTCOMPMAST");
                    dt3 = ds3.Tables["GTCOMPMAST"];
                    if (dt3.Rows.Count > 0)
                    {
                        combocompcode.DisplayMember = "COMPCODE";
                        combocompcode.ValueMember = "COMPCODE";
                        combocompcode.DataSource = dt3;
                    }
                
                    string sel4 = "SELECT 0 GTCOMPMASTID, ''COMPCODE FROM dual  union all   SELECT DISTINCT 1 AS GTCOMPMASTID, 'PASS MISSED' AS COMPCODE  FROM DUAL A  UNION ALL SELECT DISTINCT 2 AS GTCOMPMASTID, 'NATIVE' AS COMPCODE  FROM DUAL A UNION ALL SELECT DISTINCT 3 AS GTCOMPMASTID, 'SECURITY' AS COMPCODE  FROM DUAL A UNION ALL SELECT DISTINCT 4 AS GTCOMPMASTID, 'TOTAL COUNT' AS COMPCODE  FROM DUAL A  UNION ALL SELECT DISTINCT 5 AS GTCOMPMASTID, 'REMARKS' AS COMPCODE  FROM DUAL A UNION ALL SELECT DISTINCT 6 AS GTCOMPMASTID, 'LEAVE' AS COMPCODE  FROM DUAL A UNION ALL SELECT DISTINCT 7 AS GTCOMPMASTID, 'HOSTEL OUTING' AS COMPCODE  FROM DUAL A  UNION ALL SELECT DISTINCT 8 AS GTCOMPMASTID, 'RESIGNATION' AS COMPCODE  FROM DUAL A  UNION ALL SELECT DISTINCT 9 AS GTCOMPMASTID, 'WITHOUT-PHOTO' AS COMPCODE  FROM DUAL A   UNION ALL SELECT DISTINCT 13 AS GTCOMPMASTID, 'HOSTEL' AS COMPCODE  FROM DUAL A UNION ALL SELECT DISTINCT 14 AS GTCOMPMASTID, 'PERSONAL' AS COMPCODE  FROM DUAL A ORDER BY 1";
                    DataSet ds4 = Utility.ExecuteSelectQuery(sel4, "GTCOMPMAST");
                    DataTable dt4 = ds4.Tables["GTCOMPMAST"];
                    if (dt4.Rows.Count > 0)
                    {
                        comboBox1.DisplayMember = "COMPCODE";
                        comboBox1.ValueMember = "COMPCODE";
                        comboBox1.DataSource = dt4;


                    }
                
              
            }
            catch (Exception ex)
            {
                MessageBox.Show("Data Source Not Connected" + ex.Message);
            }
        }
        private void Exit_Click(object sender, EventArgs e)
        {
            crystalReportViewer1.ReportSource = null;
            this.Hide();
        }



        private void MovementReport_Load(object sender, EventArgs e)
        {

            this.txtsearch.Focus(); comboformate.SelectedIndex = 0;
            frmdate.Value = DateTime.Now.AddDays(0); todate.Value = DateTime.Now.AddDays(0);
            hostelload();
            COMBCODELOAD();
            
            if (Class.Users.HUserName == "HR" || Class.Users.HUserName == "VAIRAM" || Class.Users.HostelName == "MENS HOTEL" || Class.Users.HostelName == "WORKING GENTS HOSTEL" || Class.Users.HostelName == "WOMENS HOSTEL" || Class.Users.HostelName == "GENTS STAFF HOSTEL" || Class.Users.HostelName == "BOYS HOSTEL")
            {
                combohostel.Enabled = true;
            }
            else
            {
                combohostel.Enabled = false;
            }
        }
        private void combocompcode_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (combocompcode.Text == "")
            //{
            //    string sel2 = "SELECT to_char(A.ASPTBLHOSTELGATEPASSID) as ASPTBLHOSTELGATEPASSID,B.COMPNAME AS COMPCODE,D.MIDCARD ,C.FNAME ,E.MNNAME1 DISPNAME,substr(A.SYSTEMDATE,1,10) AS CONTACTNO,F.HOSTELNAME , F.ROOMNO,G.REASON,A.PERMISSIONHRS,A.INTIME,A.OUTTIME,  A.REMARKS || A.REMARKS1 AS REMARKS ,A.TIMEDIFF as BLOCKFLOOR   FROM ASPTBLHOSTELGATEPASS A  JOIN   GTCOMPMAST B ON B.GTCOMPMASTID = A.COMPCODE  JOIN   HREMPLOYMAST    C ON C.COMPCODE=B.GTCOMPMASTID AND C.COMPCODE=A.COMPCODE   JOIN HREMPLOYDETAILS D ON D.HREMPLOYMASTID=C.HREMPLOYMASTID AND D.IDCARD=C.IDCARDNO AND  D.DEPTNAME=A.DEPARTMENT AND D.MIDCARD=A.IDCARDNO    JOIN GTDEPTDESGMAST  E ON E.GTDEPTDESGMASTID = A.DEPARTMENT  JOIN HOSTELLIVEDATA F ON F.COMPCODE=B.COMPCODE AND F.IDCARDNO=C.IDCARDNO AND F.IDCARDNO=D.IDCARD  AND D.IDCARD=F.IDCARDNO JOIN ASPTBLREASONMAS G ON G.ASPTBLREASONMASID=A.REASON  JOIN  HRECONTACTDETAILS H ON H.HREMPLOYMASTID=C.HREMPLOYMASTID WHERE   B.COMPCODE='"+combocompcode.Text+"' AND A.MODIFIED between TO_DATE('" + frmdate.Value.ToString("dd-MM-yyyy") + "','dd-MM-yyyy') and TO_DATE('" + todate.Value.ToString("dd-MM-yyyy") + "','dd-MM-yyyy') ORDER BY 1";

            //    DataSet ds2 = Utility.ExecuteSelectQuery(sel2, "ASPTBLHOSTELGATEPASS");
            //    DataTable dt2 = ds2.Tables["ASPTBLHOSTELGATEPASS"];
            //    rd.SetDataSource(dt2);
            //    crystalReportViewer1.ReportSource = null;
            //    crystalReportViewer1.ReportSource = rd;

            //    crystalReportViewer1.Refresh(); txtsearch.Text = "";
            //}
            idcardsearch(combocompcode.Text);
            Class.Users.UserTime = 0;
            combohostel.Text = ""; combohostel.SelectedIndex = -1;
        }

        public void DownLoads()
        {
           

            if (comboformate.Text != "")
            {
                ////string  d = Convert.ToString(System.DateTime.Now.ToString());
                ////String[] dd = d.Split('-');
                ////String[] dd1 = dd[2].Split(':');
                ////string ddd = dd[0].ToString() + dd[1].ToString() + dd1[0].ToString() +  dd1[1].ToString() + dd1[2].ToString();

                DialogResult result = MessageBox.Show("Do you want to '" + comboformate.Text + "' Formate ??", "" + comboformate.Text + "PRINT OUT", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (result.Equals(DialogResult.OK))
                {
                    if (combocompcode.Text == "TOTAL COUNT")//your specific tabname
                    {
                        switch (comboformate.Text)
                        {
                            case "Word":
                                rd2.ExportToDisk(ExportFormatType.WordForWindows, "d:\\'" + System.DateTime.Now.ToString("dd-MM-yyyy") + "  " + combocompcode.Text + "'MovementReport.doc");
                                break;

                            case "Excel":                               
                                rd2.ExportToDisk(ExportFormatType.ExcelWorkbook, "d:\\'" + System.DateTime.Now.ToString("dd-MM-yyyy") + "  " + combocompcode.Text + "'MovementReport.xls");
                                break;

                            case "PDF":
                                rd2.ExportToDisk(ExportFormatType.PortableDocFormat, "d:\\'" + System.DateTime.Now.ToString("dd-MM-yyyy") + "  " + combocompcode.Text + "'MovementReport.pdf");
                                break;

                            case "CSV":
                                rd2.ExportToDisk(ExportFormatType.CharacterSeparatedValues, "d:\\'" + System.DateTime.Now.ToString("dd-MM-yyyy") + "  " + combocompcode.Text + "'MovementReport.csv");
                                break;
                        }
                    }
                    else
                    {
                        switch (comboformate.Text)
                        {
                            case "Word":
                                rd.ExportToDisk(ExportFormatType.WordForWindows, "d:\\'" + System.DateTime.Now.ToString("dd-MM-yyyy") + "  " + combocompcode.Text + "'MovementReport.doc");
                                break;

                            case "Excel":
                                
                                rd.ExportToDisk(ExportFormatType.ExcelWorkbook, "d:\\'" + System.DateTime.Now.ToString("dd-MM-yyyy") + "  " + combocompcode.Text + "'MovementReport.xls");
                                break;

                            case "PDF":
                                rd.ExportToDisk(ExportFormatType.PortableDocFormat, "d:\\'" + System.DateTime.Now.ToString("dd-MM-yyyy") + "  " + combocompcode.Text + "'MovementReport.pdf");
                                break;

                            case "CSV":
                                rd.ExportToDisk(ExportFormatType.CharacterSeparatedValues, "d:\\'" + System.DateTime.Now.ToString("dd-MM-yyyy") + "  " + combocompcode.Text + "'MovementReport.csv");
                                break;
                        }
                    }
                }
                else
                {

                }
            }
            else
            {
                MessageBox.Show("Pls Select Combo Box Value");
            }
        }
        private void hostelload()
        {
            try
            {
                DataTable dt3 = new DataTable();
                // if (combohostel.Text == "MENS HOTEL" || combohostel.Text == "WORKING GENTS HOSTEL" || combohostel.Text == "WOMENS HOSTEL" || combohostel.Text == "GENTS STAFF HOSTEL" || combohostel.Text == "BOYS HOSTEL")
                // {
                Pinnacle.Models.Hostel hos = new Pinnacle.Models.Hostel();
                dt3 = hos.HostelName();                
                if (dt3.Rows.Count > 0)
                {
                    combohostel.DisplayMember = "HOSTELNAME";
                    combohostel.ValueMember = "HOSTELNAME";
                    combohostel.DataSource = dt3;
                    

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Data Source Not Connected" + ex.Message);
            }
        }
        private void idcardsearch(string s )
        {
            try
            {
                string sel3 = ""; DataTable dt3 = new DataTable();
                if (combohostel.Text == "WOMENS HOSTEL" || combohostel.Text == "WORKING GENTS HOSTEL" || combohostel.Text == "GENTS STAFF HOSTEL")
                {
                    sel3 = "SELECT NULL IDCARDNO FROM DUAL             UNION   SELECT  A.IDCARDNO  FROM ASPTBLHOSTELGATEPASS A  JOIN GTCOMPMAST B ON B.GTCOMPMASTID= A.COMPCODE      where B.COMPCODE='" + s + "'  and a.hostelname='" + combohostel.Text + "' ORDER BY IDCARDNO DESC";
                    DataSet ds3 = Utility.ExecuteSelectQuery(sel3, "ASPTBLHOSTELGATEPASS");
                    dt3 = ds3.Tables["ASPTBLHOSTELGATEPASS"];
                }
                else
                {
                    sel3 = "SELECT NULL IDCARDNO FROM DUAL             UNION   SELECT  A.IDCARDNO  FROM ASPTBLHOSTELGATEPASS A  JOIN GTCOMPMAST B ON B.GTCOMPMASTID= A.COMPCODE        where B.COMPCODE='" + s + "' ORDER BY IDCARDNO DESC";
                    DataSet ds4 = Utility.ExecuteSelectQuery(sel3, "ASPTBLHOSTELGATEPASS");
                    dt3 = ds4.Tables["ASPTBLHOSTELGATEPASS"];
                }
                if (dt3.Rows.Count > 0)
                {
                    comboidcardsearch.DisplayMember = "IDCARDNO";
                    comboidcardsearch.ValueMember = "IDCARDNO";
                    comboidcardsearch.DataSource = dt3;
                    

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Data Source Not Connected" + ex.Message);
            }
        }


        private void butView_Click(object sender, EventArgs e)
        {
            try
            {
                lblcount.Visible = false; dtgeneral = null; Class.Users.UserTime = 0;
                
                if (combocompcode.Text == "" && combohostel.Text =="WORKING GENTS HOSTEL" || combocompcode.Text == "" && combohostel.Text == "WOMENS HOSTEL" || combocompcode.Text == "" && combohostel.Text == "GENTS STAFF HOSTEL")
                {
                    string sel2 = "SELECT DISTINCT to_char(A.ASPTBLHOSTELGATEPASSID) as ASPTBLHOSTELGATEPASSID,B.COMPNAME2 AS COMPCODE,'" + combocompcode.Text + "' as DESIGNATION, D.MIDCARD ,C.FNAME ,E.MNNAME1 AS DISPNAME, substr(a.SYSTEMDATE,0,10)  AS CONTACTNO,a.HOSTELNAME , A.HOSTELROOM1 as ROOMNO,G.REASON,'HOSTELNAME' as  PERMISSIONHRS ,A.INTIME,A.OUTTIME,  A.REMARKS || ' - ' || A.REMARKS1 AS REMARKS ,A.TIMEDIFF as BLOCKFLOOR   FROM ASPTBLHOSTELGATEPASS A  JOIN   GTCOMPMAST B ON B.GTCOMPMASTID = A.COMPCODE  JOIN   HREMPLOYMAST    C ON C.COMPCODE=B.GTCOMPMASTID  JOIN HREMPLOYDETAILS D ON D.HREMPLOYMASTID=C.HREMPLOYMASTID AND D.IDCARD=C.IDCARDNO AND  D.DEPTNAME=A.DEPARTMENT AND D.MIDCARD=A.IDCARDNO     JOIN GTDEPTDESGMAST  E ON E.GTDEPTDESGMASTID = A.DEPARTMENT  JOIN ASPTBLREASONMAS G ON G.ASPTBLREASONMASID=A.REASON     JOIN  HRECONTACTDETAILS H ON H.HREMPLOYMASTID=C.HREMPLOYMASTID     WHERE  A.HOSTELNAME='" + combohostel.Text + "' AND A.MODIFIED between TO_DATE('" + frmdate.Value.ToString("dd-MM-yyyy") + "','dd-MM-yyyy') and TO_DATE('" + todate.Value.ToString("dd-MM-yyyy") + "','dd-MM-yyyy') ORDER BY 1";
                    DataSet ds2 = Utility.ExecuteSelectQuery(sel2, "ASPTBLHOSTELGATEPASS");
                    dtgeneral = ds2.Tables["ASPTBLHOSTELGATEPASS"];
                    rd.SetDataSource(dtgeneral);
                    crystalReportViewer1.ReportSource = null;
                    crystalReportViewer1.ReportSource = rd;
                    crystalReportViewer1.Refresh();
                    lblcount.Visible = true; lblcount.Text = "Total '"+ combohostel.Text + "' Count:-" + dtgeneral.Rows.Count.ToString();
                    txtsearch.Text = ""; return;
                }
               
                if (comboBox1.Text != null && combohostel.Text != null && combocompcode.Text == "AGF" || comboBox1.Text != null && combohostel.Text != null && combocompcode.Text == "AGFM" && combohostel.Text != null || combocompcode.Text == "AGFMGII" && combohostel.Text != null || combocompcode.Text == "AGFC" && combohostel.Text != null || combocompcode.Text == "AGFP" && combohostel.Text != null || combocompcode.Text == "AGFK" && combohostel.Text != null || combocompcode.Text == "AGFMGII")
                {
                    string sel2 = "SELECT DISTINCT to_char(A.ASPTBLHOSTELGATEPASSID) as ASPTBLHOSTELGATEPASSID,A.HOSTELNAME, D.MIDCARD ,C.FNAME ,E.MNNAME1 AS DISPNAME, substr(a.SYSTEMDATE,0,10)  AS CONTACTNO,A.HOSTELNAME , '' AS ROOMNO,G.REASON,'COMPCODE' as  PERMISSIONHRS ,A.INTIME,A.OUTTIME,  A.REMARKS || ' - ' || A.REMARKS1 AS REMARKS ,A.TIMEDIFF as BLOCKFLOOR,B.COMPNAME2 AS COMPCODE  FROM ASPTBLHOSTELGATEPASS A  JOIN   GTCOMPMAST B ON B.GTCOMPMASTID = A.COMPCODE  JOIN   HREMPLOYMAST    C ON C.COMPCODE=B.GTCOMPMASTID  JOIN HREMPLOYDETAILS D ON D.HREMPLOYMASTID=C.HREMPLOYMASTID AND D.IDCARD=C.IDCARDNO AND  D.DEPTNAME=A.DEPARTMENT AND D.MIDCARD=A.IDCARDNO     JOIN GTDEPTDESGMAST  E ON E.GTDEPTDESGMASTID = A.DEPARTMENT   JOIN ASPTBLREASONMAS G ON G.ASPTBLREASONMASID=A.REASON     JOIN  HRECONTACTDETAILS H ON H.HREMPLOYMASTID=C.HREMPLOYMASTID     WHERE    A.MODIFIED between TO_DATE('" + frmdate.Value.ToString("dd-MM-yyyy") + "','dd-MM-yyyy') and TO_DATE('" + todate.Value.ToString("dd-MM-yyyy") + "','dd-MM-yyyy') AND A.HOSTELNAME='" + combocompcode.Text + "' ORDER BY 1";
                    DataSet ds2 = Utility.ExecuteSelectQuery(sel2, "ASPTBLHOSTELGATEPASS");
                    dtgeneral = ds2.Tables["ASPTBLHOSTELGATEPASS"];
                    rd.SetDataSource(dtgeneral);
                    crystalReportViewer1.ReportSource = null;
                    crystalReportViewer1.ReportSource = rd;
                    crystalReportViewer1.Refresh();
                    lblcount.Visible = true; lblcount.Text = "Total  '" + combohostel.Text + "'  Count:-" + dtgeneral.Rows.Count.ToString();
                    txtsearch.Text = ""; return;
                }
                if (combohostel.Text == "MANUAL PASS")
                {
                    string sel2 = "SELECT DISTINCT to_char(A.ASPTBLHOSTELGATEPASSID) as ASPTBLHOSTELGATEPASSID,A.HOSTELNAME,'" + combocompcode.Text + "' as DESIGNATION, D.MIDCARD ,C.FNAME ,E.MNNAME1 AS DISPNAME, substr(a.SYSTEMDATE,0,10)  AS CONTACTNO,A.HOSTELNAME , '' AS ROOMNO,G.REASON,'COMPCODE' as  PERMISSIONHRS ,A.INTIME,A.OUTTIME,  A.REMARKS || ' - ' || A.REMARKS1 AS REMARKS ,A.TIMEDIFF as BLOCKFLOOR,B.COMPNAME2 AS COMPCODE  FROM ASPTBLHOSTELGATEPASS A  JOIN   GTCOMPMAST B ON B.GTCOMPMASTID = A.COMPCODE  JOIN   HREMPLOYMAST    C ON C.COMPCODE=B.GTCOMPMASTID  JOIN HREMPLOYDETAILS D ON D.HREMPLOYMASTID=C.HREMPLOYMASTID AND D.IDCARD=C.IDCARDNO AND  D.DEPTNAME=A.DEPARTMENT AND D.MIDCARD=A.IDCARDNO     JOIN GTDEPTDESGMAST  E ON E.GTDEPTDESGMASTID = A.DEPARTMENT   JOIN ASPTBLREASONMAS G ON G.ASPTBLREASONMASID=A.REASON     JOIN  HRECONTACTDETAILS H ON H.HREMPLOYMASTID=C.HREMPLOYMASTID     WHERE A.MODIFIED between TO_DATE('" + frmdate.Value.ToString("dd-MM-yyyy") + "','dd-MM-yyyy') and TO_DATE('" + todate.Value.ToString("dd-MM-yyyy") + "','dd-MM-yyyy') AND A.MANUALPASS='MANUAL PASS'  ORDER BY 1";
                    DataSet ds2 = Utility.ExecuteSelectQuery(sel2, "ASPTBLHOSTELGATEPASS");
                    dtgeneral = ds2.Tables["ASPTBLHOSTELGATEPASS"];
                    rd.SetDataSource(dtgeneral);
                    crystalReportViewer1.ReportSource = null;
                    crystalReportViewer1.ReportSource = rd;
                    crystalReportViewer1.Refresh();
                    lblcount.Visible = true; lblcount.Text = "Total  '" + combohostel.Text + "'  Count:-" + dtgeneral.Rows.Count.ToString();
                    txtsearch.Text = ""; return;
                }
                if (comboBox1.Text == "PASS MISSED")
                {
                    string sel2 = "SELECT to_char(A.ASPTBLHOSTELGATEPASSID) as ASPTBLHOSTELGATEPASSID,B.COMPNAME2 AS COMPCODE,'"+ Class.Users.HCompcode + "' as DESIGNATION, D.MIDCARD ,C.FNAME ,E.MNNAME1 AS DISPNAME, substr(A.SYSTEMDATE,1,10) AS CONTACTNO,F.HOSTELNAME , F.ROOMNO,G.REASON,'ALL' as  PERMISSIONHRS ,A.INTIME,A.OUTTIME,  A.REMARKS || ' - ' || A.REMARKS1 AS REMARKS,A.TIMEDIFF as BLOCKFLOOR   FROM ASPTBLHOSTELGATEPASS A  JOIN   GTCOMPMAST B ON B.GTCOMPMASTID = A.COMPCODE  JOIN   HREMPLOYMAST    C ON C.COMPCODE=B.GTCOMPMASTID  JOIN HREMPLOYDETAILS D ON D.HREMPLOYMASTID=C.HREMPLOYMASTID AND D.IDCARD=C.IDCARDNO AND D.MIDCARD=A.IDCARDNO     JOIN GTDEPTDESGMAST  E ON E.GTDEPTDESGMASTID = A.DEPARTMENT  JOIN HOSTELLIVEDATA F ON F.COMPCODE=B.COMPCODE AND F.IDCARDNO=C.IDCARDNO    AND F.IDCARDNO=D.IDCARD  AND D.IDCARD=F.IDCARDNO JOIN ASPTBLREASONMAS G ON G.ASPTBLREASONMASID=A.REASON     JOIN  HRECONTACTDETAILS H ON H.HREMPLOYMASTID=C.HREMPLOYMASTID     WHERE A.PASSMISSED='T' AND A.HOSTELNAME='"+Class.Users.HostelName+ "'  ORDER BY A.ASPTBLHOSTELGATEPASSID DESC";
                    DataSet ds2 = Utility.ExecuteSelectQuery(sel2, "ASPTBLHOSTELGATEPASS");
                    dtgeneral = ds2.Tables["ASPTBLHOSTELGATEPASS"];
                    rd.SetDataSource(dtgeneral);
                    crystalReportViewer1.ReportSource = null;
                    crystalReportViewer1.ReportSource = rd;
                    crystalReportViewer1.Refresh();
                    lblcount.Visible = true; lblcount.Text = "Total PASS MISSED Employee Count:-" + dtgeneral.Rows.Count.ToString();
                    txtsearch.Text = ""; return;
                }
                if (comboBox1.Text == "PERSONAL")
                {
                    string sel2 = "SELECT to_char(A.ASPTBLHOSTELGATEPASSID) as ASPTBLHOSTELGATEPASSID,B.COMPNAME2 AS COMPCODE,'"+Class.Users.HCompcode+"' as DESIGNATION, D.MIDCARD ,C.FNAME ,E.MNNAME1 AS DISPNAME, substr(A.SYSTEMDATE,1,10) AS CONTACTNO,'"+Class.Users.HostelName+ "' AS HOSTELNAME , '' AS ROOMNO,G.REASON,'ALL' as  PERMISSIONHRS ,A.INTIME,A.OUTTIME,  A.REMARKS || ' - ' || A.REMARKS1 AS REMARKS ,A.TIMEDIFF as BLOCKFLOOR   FROM ASPTBLHOSTELGATEPASS A  JOIN   GTCOMPMAST B ON B.GTCOMPMASTID = A.COMPCODE  JOIN   HREMPLOYMAST    C ON C.COMPCODE=B.GTCOMPMASTID  JOIN HREMPLOYDETAILS D ON D.HREMPLOYMASTID=C.HREMPLOYMASTID AND D.IDCARD=C.IDCARDNO AND D.MIDCARD=A.IDCARDNO     JOIN GTDEPTDESGMAST  E ON E.GTDEPTDESGMASTID = A.DEPARTMENT JOIN ASPTBLREASONMAS G ON G.ASPTBLREASONMASID=A.REASON     JOIN  HRECONTACTDETAILS H ON H.HREMPLOYMASTID=C.HREMPLOYMASTID     WHERE A.HOSTELNAME='" + combohostel.Text + "' AND  G.REASON='" + comboBox1.Text + "'  AND A.MODIFIED between TO_DATE('" + frmdate.Value.ToString("dd-MM-yyyy") + "','dd-MM-yyyy') and TO_DATE('" + todate.Value.ToString("dd-MM-yyyy") + "','dd-MM-yyyy')  ORDER BY A.ASPTBLHOSTELGATEPASSID DESC";
                    DataSet ds2 = Utility.ExecuteSelectQuery(sel2, "ASPTBLHOSTELGATEPASS");
                    dtgeneral = ds2.Tables["ASPTBLHOSTELGATEPASS"];
                    rd.SetDataSource(dtgeneral);
                    crystalReportViewer1.ReportSource = null;
                    crystalReportViewer1.ReportSource = rd;
                    crystalReportViewer1.Refresh();
                    lblcount.Visible = true; lblcount.Text = "Total Count:-" + dtgeneral.Rows.Count.ToString();
                    txtsearch.Text = ""; return;
                }
                if (comboBox1.Text == "NATIVE" && combohostel.Text != "" && txtsearch.Text == "")
                {

                    // string sel2 = "SELECT TO_char(A.ASPTBLHOSTELGATEPASSID) as ASPTBLHOSTELGATEPASSID,B.COMPNAME AS COMPCODE,'ALL' as DESIGNATION, D.MIDCARD ,C.FNAME ,E.MNNAME1 AS DISPNAME, substr(A.SYSTEMDATE,1,10) AS CONTACTNO, a.HOSTELNAME , '' as ROOMNO,G.REASON,'HOSTELNAME' as  PERMISSIONHRS ,A.INTIME,A.OUTTIME,  A.REMARKS,A.TIMEDIFF as BLOCKFLOOR  ,A.MODIFIED    FROM ASPTBLHOSTELGATEPASS A  JOIN   GTCOMPMAST B ON B.GTCOMPMASTID = A.COMPCODE    JOIN   HREMPLOYMAST    C ON C.COMPCODE=B.GTCOMPMASTID AND C.COMPCODE=A.COMPCODE AND C.HREMPLOYMASTID=A.EMPNAME     JOIN HREMPLOYDETAILS D ON D.HREMPLOYMASTID=C.HREMPLOYMASTID AND D.IDCARD=C.IDCARDNO    AND  D.DEPTNAME=A.DEPARTMENT AND D.MIDCARD=A.IDCARDNO   JOIN GTDEPTDESGMAST  E ON E.GTDEPTDESGMASTID = A.DEPARTMENT    JOIN ASPTBLREASONMAS G ON G.ASPTBLREASONMASID=A.REASON    WHERE A.NATIVE='T'   AND A.HOSTELNAME='" + combohostel.Text + "'  AND A.MODIFIED between TO_DATE('" + frmdate.Value.ToString("dd-MM-yyyy") + "','dd-MM-yyyy') and TO_DATE('" + todate.Value.ToString("dd-MM-yyyy") + "','dd-MM-yyyy')  ORDER BY 1";
                    string sel2 = "SELECT DISTINCT TO_CHAR(A.ASPTBLHOSTELGATEPASSID) AS ASPTBLHOSTELGATEPASSID,B.COMPNAME2 AS COMPCODE,'' AS DESIGNATION,D.MIDCARD ,C.FNAME ,E.MNNAME1 AS DISPNAME, SUBSTR(A.SYSTEMDATE,0,10)  AS CONTACTNO,(SELECT F.HOSTELNAME FROM HOSTELLIVEDATA F WHERE F.COMPCODE=B.COMPCODE AND F.IDCARDNO=C.IDCARDNO    AND F.IDCARDNO=D.IDCARD   AND D.IDCARD=F.IDCARDNO) HOSTELNAME ,(SELECT F.ROOMNO FROM HOSTELLIVEDATA F WHERE F.COMPCODE=B.COMPCODE AND F.IDCARDNO=C.IDCARDNO    AND F.IDCARDNO=D.IDCARD   AND D.IDCARD=F.IDCARDNO) ROOMNO,G.REASON,'HOSTELNAME' AS  PERMISSIONHRS ,A.INTIME,A.OUTTIME,  A.REMARKS || ' - ' || A.REMARKS1 AS REMARKS ,A.TIMEDIFF AS BLOCKFLOOR   FROM ASPTBLHOSTELGATEPASS A  JOIN   GTCOMPMAST B ON B.GTCOMPMASTID = A.COMPCODE  JOIN   HREMPLOYMAST    C ON C.COMPCODE=B.GTCOMPMASTID  JOIN HREMPLOYDETAILS D ON D.HREMPLOYMASTID=C.HREMPLOYMASTID AND D.IDCARD=C.IDCARDNO AND  D.DEPTNAME=A.DEPARTMENT AND D.MIDCARD=A.IDCARDNO    JOIN GTDEPTDESGMAST  E ON E.GTDEPTDESGMASTID = A.DEPARTMENT    JOIN ASPTBLREASONMAS G ON G.ASPTBLREASONMASID=A.REASON    JOIN  HRECONTACTDETAILS H ON H.HREMPLOYMASTID=C.HREMPLOYMASTID WHERE A.NATIVE='T'   AND A.HOSTELNAME='" + combohostel.Text + "'  AND A.MODIFIED between TO_DATE('" + frmdate.Value.ToString("dd-MM-yyyy") + "','dd-MM-yyyy') and TO_DATE('" + todate.Value.ToString("dd-MM-yyyy") + "','dd-MM-yyyy')  ORDER BY 1";
                    DataSet ds2 = Utility.ExecuteSelectQuery(sel2, "ASPTBLHOSTELGATEPASS");
                    // dtgeneral = null;
                    dtgeneral = ds2.Tables["ASPTBLHOSTELGATEPASS"];
                    rd.SetDataSource(dtgeneral);
                    crystalReportViewer1.ReportSource = null;
                    crystalReportViewer1.ReportSource = rd; lblcount.Visible = true; lblcount.Text = "Total Native Employee Count:-" + dtgeneral.Rows.Count.ToString();
                    crystalReportViewer1.Refresh(); txtsearch.Text = ""; return;
                }
                if (comboBox1.Text == "NATIVE" && combohostel.Text != "" && txtsearch.Text != "")
                {

                    // string sel2 = "SELECT TO_char(A.ASPTBLHOSTELGATEPASSID) as ASPTBLHOSTELGATEPASSID,B.COMPNAME AS COMPCODE,'ALL' as DESIGNATION, D.MIDCARD ,C.FNAME ,E.MNNAME1 AS DISPNAME, substr(A.SYSTEMDATE,1,10) AS CONTACTNO, a.HOSTELNAME , '' as ROOMNO,G.REASON,'HOSTELNAME' as  PERMISSIONHRS ,A.INTIME,A.OUTTIME,  A.REMARKS,A.TIMEDIFF as BLOCKFLOOR  ,A.MODIFIED    FROM ASPTBLHOSTELGATEPASS A  JOIN   GTCOMPMAST B ON B.GTCOMPMASTID = A.COMPCODE    JOIN   HREMPLOYMAST    C ON C.COMPCODE=B.GTCOMPMASTID AND C.COMPCODE=A.COMPCODE AND C.HREMPLOYMASTID=A.EMPNAME     JOIN HREMPLOYDETAILS D ON D.HREMPLOYMASTID=C.HREMPLOYMASTID AND D.IDCARD=C.IDCARDNO    AND  D.DEPTNAME=A.DEPARTMENT AND D.MIDCARD=A.IDCARDNO   JOIN GTDEPTDESGMAST  E ON E.GTDEPTDESGMASTID = A.DEPARTMENT    JOIN ASPTBLREASONMAS G ON G.ASPTBLREASONMASID=A.REASON    WHERE A.NATIVE='T'   AND A.HOSTELNAME='" + combohostel.Text + "'  AND A.MODIFIED between TO_DATE('" + frmdate.Value.ToString("dd-MM-yyyy") + "','dd-MM-yyyy') and TO_DATE('" + todate.Value.ToString("dd-MM-yyyy") + "','dd-MM-yyyy')  ORDER BY 1";
                    string sel2 = "SELECT DISTINCT TO_CHAR(A.ASPTBLHOSTELGATEPASSID) AS ASPTBLHOSTELGATEPASSID,B.COMPNAME2 AS COMPCODE,'' AS DESIGNATION,D.MIDCARD ,C.FNAME ,E.MNNAME1 AS DISPNAME, SUBSTR(A.SYSTEMDATE,0,10)  AS CONTACTNO,(SELECT F.HOSTELNAME FROM HOSTELLIVEDATA F WHERE F.COMPCODE=B.COMPCODE AND F.IDCARDNO=C.IDCARDNO    AND F.IDCARDNO=D.IDCARD   AND D.IDCARD=F.IDCARDNO) HOSTELNAME ,(SELECT F.ROOMNO FROM HOSTELLIVEDATA F WHERE F.COMPCODE=B.COMPCODE AND F.IDCARDNO=C.IDCARDNO    AND F.IDCARDNO=D.IDCARD   AND D.IDCARD=F.IDCARDNO) ROOMNO,G.REASON,'HOSTELNAME' AS  PERMISSIONHRS ,A.INTIME,A.OUTTIME,  A.REMARKS || ' - ' || A.REMARKS1 AS REMARKS ,A.TIMEDIFF AS BLOCKFLOOR   FROM ASPTBLHOSTELGATEPASS A  JOIN   GTCOMPMAST B ON B.GTCOMPMASTID = A.COMPCODE  JOIN   HREMPLOYMAST    C ON C.COMPCODE=B.GTCOMPMASTID  JOIN HREMPLOYDETAILS D ON D.HREMPLOYMASTID=C.HREMPLOYMASTID AND D.IDCARD=C.IDCARDNO AND  D.DEPTNAME=A.DEPARTMENT AND D.MIDCARD=A.IDCARDNO    JOIN GTDEPTDESGMAST  E ON E.GTDEPTDESGMASTID = A.DEPARTMENT    JOIN ASPTBLREASONMAS G ON G.ASPTBLREASONMASID=A.REASON    JOIN  HRECONTACTDETAILS H ON H.HREMPLOYMASTID=C.HREMPLOYMASTID WHERE A.NATIVE='T'   AND A.HOSTELNAME='" + combohostel.Text + "'  AND A.MODIFIED between TO_DATE('" + frmdate.Value.ToString("dd-MM-yyyy") + "','dd-MM-yyyy') and TO_DATE('" + todate.Value.ToString("dd-MM-yyyy") + "','dd-MM-yyyy') and a.idcardno='"+txtsearch.Text+"'  ORDER BY 1";
                    DataSet ds2 = Utility.ExecuteSelectQuery(sel2, "ASPTBLHOSTELGATEPASS");
                    // dtgeneral = null;
                    dtgeneral = ds2.Tables["ASPTBLHOSTELGATEPASS"];
                    rd.SetDataSource(dtgeneral);
                    crystalReportViewer1.ReportSource = null;
                    crystalReportViewer1.ReportSource = rd; lblcount.Visible = true; lblcount.Text = "Total Native Employee Count:-" + dtgeneral.Rows.Count.ToString();
                    crystalReportViewer1.Refresh(); txtsearch.Text = ""; return;
                }
                if (comboBox1.Text == "TOTAL COUNT" && combohostel.Text != "")
                {
                    //  dtgeneral = null;
                    // string sel2 = "SELECT B.COMPCODE,G.REASON AS PLACE,  COUNT(A.ASPTBLHOSTELGATEPASSID)COUNT,1 ORD   FROM ASPTBLHOSTELGATEPASS A  JOIN   GTCOMPMAST B ON B.GTCOMPMASTID = A.COMPCODE JOIN HREMPLOYMAST    C ON C.COMPCODE = B.GTCOMPMASTID  JOIN HREMPLOYDETAILS D ON D.HREMPLOYMASTID = C.HREMPLOYMASTID AND D.IDCARD = C.IDCARDNO AND D.DEPTNAME = A.DEPARTMENT AND D.MIDCARD = A.IDCARDNO     JOIN GTDEPTDESGMAST  E ON E.GTDEPTDESGMASTID = A.DEPARTMENT  JOIN HOSTELLIVEDATA F ON F.COMPCODE = B.COMPCODE AND F.IDCARDNO = C.IDCARDNO AND F.IDCARDNO = D.IDCARD AND F.HOSTELNAME = A.HOSTELNAME  AND D.IDCARD = F.IDCARDNO JOIN ASPTBLREASONMAS G ON G.ASPTBLREASONMASID = A.REASON JOIN HRECONTACTDETAILS H ON H.HREMPLOYMASTID = C.HREMPLOYMASTID WHERE NOT E.mnname1 = 'SECURITY' AND A.HOSTELNAME = 'WORKING GENTS HOSTEL' and A.MODIFIED between TO_DATE('11-10-2021', 'dd-MM-yyyy') and TO_DATE('11-10-2021','dd-MM-yyyy') GROUP BY B.COMPCODE,G.REASON UNION ALL SELECT B.COMPCODE,'TOTAL' PLACE,  COUNT(A.ASPTBLHOSTELGATEPASSID)COUNT,2 ORD FROM ASPTBLHOSTELGATEPASS A  JOIN GTCOMPMAST B ON B.GTCOMPMASTID = A.COMPCODE  JOIN HREMPLOYMAST    C ON C.COMPCODE = B.GTCOMPMASTID  JOIN HREMPLOYDETAILS D ON D.HREMPLOYMASTID = C.HREMPLOYMASTID AND D.IDCARD = C.IDCARDNO AND D.DEPTNAME = A.DEPARTMENT AND D.MIDCARD = A.IDCARDNO     JOIN GTDEPTDESGMAST  E ON E.GTDEPTDESGMASTID = A.DEPARTMENT  JOIN HOSTELLIVEDATA F ON F.COMPCODE = B.COMPCODE AND F.IDCARDNO = C.IDCARDNO AND F.IDCARDNO = D.IDCARD AND F.HOSTELNAME = A.HOSTELNAME  AND D.IDCARD = F.IDCARDNO JOIN ASPTBLREASONMAS G ON G.ASPTBLREASONMASID = A.REASON JOIN HRECONTACTDETAILS H ON H.HREMPLOYMASTID = C.HREMPLOYMASTID  WHERE NOT E.mnname1 = 'SECURITY' AND A.HOSTELNAME = 'WORKING GENTS HOSTEL' and A.MODIFIED between TO_DATE('11-10-2021', 'dd-MM-yyyy') and TO_DATE('11-10-2021','dd-MM-yyyy')     GROUP BY B.COMPCODE     ORDER BY 1,3";
                    string sel2 = "SELECT b.compcode,  G.REASON AS PLACE, A.ASPTBLHOSTELGATEPASSID COUNT ,'HOSTELNAME' as  PERMISSIONHRS   FROM ASPTBLHOSTELGATEPASS A  JOIN   GTCOMPMAST B ON B.GTCOMPMASTID = A.COMPCODE     JOIN HREMPLOYMAST    C ON C.COMPCODE = B.GTCOMPMASTID  JOIN HREMPLOYDETAILS D ON D.HREMPLOYMASTID = C.HREMPLOYMASTID AND D.IDCARD = C.IDCARDNO AND D.DEPTNAME = A.DEPARTMENT     AND D.MIDCARD = A.IDCARDNO     JOIN GTDEPTDESGMAST  E ON E.GTDEPTDESGMASTID = A.DEPARTMENT  JOIN HOSTELLIVEDATA F ON F.COMPCODE = B.COMPCODE AND F.IDCARDNO = C.IDCARDNO     AND F.IDCARDNO = D.IDCARD AND F.HOSTELNAME = A.HOSTELNAME  AND D.IDCARD = F.IDCARDNO JOIN ASPTBLREASONMAS G ON G.ASPTBLREASONMASID = A.REASON   JOIN HRECONTACTDETAILS H ON H.HREMPLOYMASTID = C.HREMPLOYMASTID  WHERE NOT E.mnname1='SECURITY' AND A.HOSTELNAME='" + combohostel.Text + "'  AND A.MODIFIED between TO_DATE('" + frmdate.Value.ToString("dd-MM-yyyy") + "','dd-MM-yyyy') and TO_DATE('" + todate.Value.ToString("dd-MM-yyyy") + "','dd-MM-yyyy')           ORDER BY 1";
                    DataSet ds2 = Utility.ExecuteSelectQuery(sel2, "ASPTBLHOSTELGATEPASS");
                    dtgeneral = ds2.Tables["ASPTBLHOSTELGATEPASS"];
                    crystalReportViewer1.ReportSource = null;
                    rd2.SetDataSource(dtgeneral);
                    crystalReportViewer1.ReportSource = rd2;
                    crystalReportViewer1.Refresh();

                    lblcount.Text = "Total  Count:-" + dtgeneral.Rows[0]["COUNT"].ToString();

                    lblcount.Visible = true;
                    txtsearch.Text = ""; return;
                }

                if (comboBox1.Text == "SECURITY")
                {

                    string sel2 = "SELECT to_char(A.ASPTBLHOSTELGATEPASSID) as ASPTBLHOSTELGATEPASSID,B.COMPNAME2 AS COMPCODE,'ALL' as DESIGNATION, D.MIDCARD ,C.FNAME ,E.MNNAME1 AS DISPNAME, substr(A.SYSTEMDATE,1,10) AS CONTACTNO,F.HOSTELNAME , F.ROOMNO,G.REASON,'HOSTELNAME' as  PERMISSIONHRS ,A.INTIME,A.OUTTIME, A.REMARKS || ' - ' || A.REMARKS1 AS REMARKS ,A.TIMEDIFF as BLOCKFLOOR   FROM ASPTBLHOSTELGATEPASS A  JOIN   GTCOMPMAST B ON B.GTCOMPMASTID = A.COMPCODE  JOIN   HREMPLOYMAST    C ON C.COMPCODE=B.GTCOMPMASTID  JOIN HREMPLOYDETAILS D ON D.HREMPLOYMASTID=C.HREMPLOYMASTID AND D.IDCARD=C.IDCARDNO AND  D.DEPTNAME=A.DEPARTMENT AND D.MIDCARD=A.IDCARDNO     JOIN GTDEPTDESGMAST  E ON E.GTDEPTDESGMASTID = A.DEPARTMENT  JOIN HOSTELLIVEDATA F ON F.COMPCODE=B.COMPCODE AND F.IDCARDNO=C.IDCARDNO    AND F.IDCARDNO=D.IDCARD  AND D.IDCARD=F.IDCARDNO JOIN ASPTBLREASONMAS G ON G.ASPTBLREASONMASID=A.REASON     JOIN  HRECONTACTDETAILS H ON H.HREMPLOYMASTID=C.HREMPLOYMASTID     WHERE E.mnname1='SECURITY'    AND A.MODIFIED between TO_DATE('" + frmdate.Value.ToString("dd-MM-yyyy") + "','dd-MM-yyyy') and TO_DATE('" + todate.Value.ToString("dd-MM-yyyy") + "','dd-MM-yyyy')  ORDER BY 1";
                    DataSet ds2 = Utility.ExecuteSelectQuery(sel2, "ASPTBLHOSTELGATEPASS");
                    // dtgeneral = null;
                    dtgeneral = ds2.Tables["ASPTBLHOSTELGATEPASS"];
                    rd.SetDataSource(dtgeneral);
                    crystalReportViewer1.ReportSource = null;
                    crystalReportViewer1.ReportSource = rd;
                    crystalReportViewer1.Refresh();
                    lblcount.Visible = true; lblcount.Text = "Total  Count:-" + dtgeneral.Rows.Count.ToString();
                    txtsearch.Text = ""; return;
                }


                if (comboBox1.Text == "REMARKS" && combohostel.Text == "")
                {

                    string sel2 = "SELECT to_char(A.ASPTBLHOSTELGATEPASSID) as ASPTBLHOSTELGATEPASSID,B.COMPNAME2 AS COMPCODE,'" + Class.Users.HCompcode + "' as DESIGNATION, D.MIDCARD ,C.FNAME ,E.MNNAME1 AS DISPNAME, substr(A.SYSTEMDATE,1,10) AS CONTACTNO,'HOSTELNAME' AS PERMISSIONHRS, '' ROOMNO,G.REASON,'ALL' as  PERMISSIONHRS ,A.INTIME,A.OUTTIME, A.REMARKS || ' - ' || A.REMARKS1 AS REMARKS ,A.TIMEDIFF as BLOCKFLOOR   FROM ASPTBLHOSTELGATEPASS A  JOIN   GTCOMPMAST B ON B.GTCOMPMASTID = A.COMPCODE  JOIN   HREMPLOYMAST    C ON C.COMPCODE=B.GTCOMPMASTID  JOIN HREMPLOYDETAILS D ON D.HREMPLOYMASTID=C.HREMPLOYMASTID AND D.IDCARD=C.IDCARDNO AND  D.DEPTNAME=A.DEPARTMENT AND D.MIDCARD=A.IDCARDNO     JOIN GTDEPTDESGMAST  E ON E.GTDEPTDESGMASTID = A.DEPARTMENT  JOIN ASPTBLREASONMAS G ON G.ASPTBLREASONMASID=A.REASON    WHERE NOT E.mnname1='SECURITY'   AND A.REMARKS IS NOT NULL AND A.HOSTELNAME='" + Class.Users.HostelName+ "' ORDER BY A.ASPTBLHOSTELGATEPASSID asc";
                    DataSet ds2 = Utility.ExecuteSelectQuery(sel2, "ASPTBLHOSTELGATEPASS");
                    //  dtgeneral = null;
                    dtgeneral = ds2.Tables["ASPTBLHOSTELGATEPASS"];
                    rd.SetDataSource(dtgeneral);
                    crystalReportViewer1.ReportSource = null;
                    crystalReportViewer1.ReportSource = rd;
                    crystalReportViewer1.Refresh(); lblcount.Visible = true; lblcount.Text = "Total  Count:-" + dtgeneral.Rows.Count.ToString(); txtsearch.Text = ""; return;
                }
                if (comboBox1.Text == "REMARKS" && combohostel.Text != "")
                {

                    string sel2 = "SELECT to_char(A.ASPTBLHOSTELGATEPASSID) as ASPTBLHOSTELGATEPASSID,B.COMPNAME2 AS COMPCODE,'ALL' as DESIGNATION, D.MIDCARD ,C.FNAME ,E.MNNAME1 AS DISPNAME, substr(A.SYSTEMDATE,1,10) AS CONTACTNO,F.HOSTELNAME , F.ROOMNO,G.REASON,'HOSTELNAME' as  PERMISSIONHRS ,A.INTIME,A.OUTTIME,  A.REMARKS || ' - ' || A.REMARKS1 AS REMARKS ,A.TIMEDIFF as BLOCKFLOOR   FROM ASPTBLHOSTELGATEPASS A  JOIN   GTCOMPMAST B ON B.GTCOMPMASTID = A.COMPCODE  JOIN   HREMPLOYMAST    C ON C.COMPCODE=B.GTCOMPMASTID  JOIN HREMPLOYDETAILS D ON D.HREMPLOYMASTID=C.HREMPLOYMASTID AND D.IDCARD=C.IDCARDNO AND  D.DEPTNAME=A.DEPARTMENT AND D.MIDCARD=A.IDCARDNO     JOIN GTDEPTDESGMAST  E ON E.GTDEPTDESGMASTID = A.DEPARTMENT  JOIN HOSTELLIVEDATA F ON F.COMPCODE=B.COMPCODE AND F.IDCARDNO=C.IDCARDNO    AND F.IDCARDNO=D.IDCARD  AND D.IDCARD=F.IDCARDNO JOIN ASPTBLREASONMAS G ON G.ASPTBLREASONMASID=A.REASON     JOIN  HRECONTACTDETAILS H ON H.HREMPLOYMASTID=C.HREMPLOYMASTID  WHERE NOT E.mnname1='SECURITY'  AND G.REASON='" + comboBox1.Text + "' AND A.HOSTELNAME='" + combohostel.Text + "' AND  A.MODIFIED between TO_DATE('" + frmdate.Value.ToString("dd-MM-yyyy") + "','dd-MM-yyyy') and TO_DATE('" + todate.Value.ToString("dd-MM-yyyy") + "','dd-MM-yyyy') AND A.REMARKS IS NOT NULL ORDER BY 1";
                    DataSet ds2 = Utility.ExecuteSelectQuery(sel2, "ASPTBLHOSTELGATEPASS");
                    //  dtgeneral = null;
                    dtgeneral = ds2.Tables["ASPTBLHOSTELGATEPASS"];
                    rd.SetDataSource(dtgeneral);
                    crystalReportViewer1.ReportSource = null;
                    crystalReportViewer1.ReportSource = rd;
                    crystalReportViewer1.Refresh(); lblcount.Visible = true; lblcount.Text = "Total  Count:-" + dtgeneral.Rows.Count.ToString(); txtsearch.Text = ""; return;
                }
                if (comboBox1.Text == "RESIGNATION" || comboBox1.Text == "LEAVE" || comboBox1.Text == "HOSTEL OUTING")
                {
                    string sel2 = "SELECT to_char(A.ASPTBLHOSTELGATEPASSID) as ASPTBLHOSTELGATEPASSID,B.COMPNAME2 AS COMPCODE,'ALL' as DESIGNATION, D.MIDCARD ,C.FNAME ,E.MNNAME1 DISPNAME, substr(A.SYSTEMDATE,1,10) AS CONTACTNO,F.HOSTELNAME , F.ROOMNO,G.REASON,'HOSTELNAME' as  PERMISSIONHRS ,A.INTIME,A.OUTTIME, A.REMARKS || ' - ' || A.REMARKS1 AS REMARKS ,A.TIMEDIFF as BLOCKFLOOR   FROM ASPTBLHOSTELGATEPASS A  JOIN   GTCOMPMAST B ON B.GTCOMPMASTID = A.COMPCODE  JOIN   HREMPLOYMAST    C ON C.COMPCODE=B.GTCOMPMASTID  JOIN HREMPLOYDETAILS D ON D.HREMPLOYMASTID=C.HREMPLOYMASTID AND D.IDCARD=C.IDCARDNO AND  D.DEPTNAME=A.DEPARTMENT AND D.MIDCARD=A.IDCARDNO     JOIN GTDEPTDESGMAST  E ON E.GTDEPTDESGMASTID = A.DEPARTMENT  JOIN HOSTELLIVEDATA F ON F.COMPCODE=B.COMPCODE AND F.IDCARDNO=C.IDCARDNO    AND F.IDCARDNO=D.IDCARD  AND D.IDCARD=F.IDCARDNO JOIN ASPTBLREASONMAS G ON G.ASPTBLREASONMASID=A.REASON     JOIN  HRECONTACTDETAILS H ON H.HREMPLOYMASTID=C.HREMPLOYMASTID  WHERE NOT E.mnname1='SECURITY' AND G.REASON='" + comboBox1.Text+"' AND A.HOSTELNAME='" + combohostel.Text + "'  AND  A.MODIFIED between TO_DATE('" + frmdate.Value.ToString("dd-MM-yyyy") + "','dd-MM-yyyy') and TO_DATE('" + todate.Value.ToString("dd-MM-yyyy") + "','dd-MM-yyyy')  ORDER BY 1";
                    DataSet ds2 = Utility.ExecuteSelectQuery(sel2, "ASPTBLHOSTELGATEPASS");
                    //  dtgeneral = null;
                    dtgeneral = ds2.Tables["ASPTBLHOSTELGATEPASS"];
                    rd.SetDataSource(dtgeneral);
                    crystalReportViewer1.ReportSource = null;
                    crystalReportViewer1.ReportSource = rd;
                    crystalReportViewer1.Refresh(); lblcount.Visible = true; lblcount.Text = "Total  Count:-" + dtgeneral.Rows.Count.ToString(); txtsearch.Text = ""; return;
                }
                if (comboBox1.Text == "HOSTEL")
                {
                    string sel2 = "SELECT to_char(A.ASPTBLHOSTELGATEPASSID) as ASPTBLHOSTELGATEPASSID,B.COMPNAME2 AS COMPCODE,'ALL' as DESIGNATION, D.MIDCARD ,C.FNAME ,E.MNNAME1 DISPNAME, substr(A.SYSTEMDATE,1,10) AS CONTACTNO,F.HOSTELNAME , F.ROOMNO,G.REASON,'HOSTELNAME' as  PERMISSIONHRS ,A.INTIME,A.OUTTIME, A.REMARKS || ' - ' || A.REMARKS1 AS REMARKS ,A.TIMEDIFF as BLOCKFLOOR   FROM ASPTBLHOSTELGATEPASS A  JOIN   GTCOMPMAST B ON B.GTCOMPMASTID = A.COMPCODE  JOIN   HREMPLOYMAST    C ON C.COMPCODE=B.GTCOMPMASTID  JOIN HREMPLOYDETAILS D ON D.HREMPLOYMASTID=C.HREMPLOYMASTID AND D.IDCARD=C.IDCARDNO AND  D.DEPTNAME=A.DEPARTMENT AND D.MIDCARD=A.IDCARDNO     JOIN GTDEPTDESGMAST  E ON E.GTDEPTDESGMASTID = A.DEPARTMENT  JOIN HOSTELLIVEDATA F ON F.COMPCODE=B.COMPCODE AND F.IDCARDNO=C.IDCARDNO    AND F.IDCARDNO=D.IDCARD  AND D.IDCARD=F.IDCARDNO JOIN ASPTBLREASONMAS G ON G.ASPTBLREASONMASID=A.REASON     JOIN  HRECONTACTDETAILS H ON H.HREMPLOYMASTID=C.HREMPLOYMASTID  WHERE NOT E.mnname1='SECURITY' AND G.REASON='" + comboBox1.Text + "' AND A.HOSTELNAME='" + combohostel.Text + "'  AND  A.MODIFIED between TO_DATE('" + frmdate.Value.ToString("dd-MM-yyyy") + "','dd-MM-yyyy') and TO_DATE('" + todate.Value.ToString("dd-MM-yyyy") + "','dd-MM-yyyy')  ORDER BY 1";
                    DataSet ds2 = Utility.ExecuteSelectQuery(sel2, "ASPTBLHOSTELGATEPASS");
                    //  dtgeneral = null;
                    dtgeneral = ds2.Tables["ASPTBLHOSTELGATEPASS"];
                    rd.SetDataSource(dtgeneral);
                    crystalReportViewer1.ReportSource = null;
                    crystalReportViewer1.ReportSource = rd;
                    crystalReportViewer1.Refresh(); lblcount.Visible = true; lblcount.Text = "Total  Count:-" + dtgeneral.Rows.Count.ToString(); txtsearch.Text = ""; return;
                }
               
                if (comboBox1.Text == "WITHOUT-PHOTO" && combohostel.Text != "")
                {
                    //   string sel2 = "SELECT  x.MIDCARD,x.HREMPLOYMASTID,x.FNAME,x.DISPNAME,x.photo,x.REMARKS from ( SELECT B.MIDCARD,A.HREMPLOYMASTID,CONCAT(a.fname ,concat('-',B.MIDCARD) ) as FNAME,E.MNNAME1 as DISPNAME,  H.IMAGEBYTES AS PHOTO,C.COMPCODE as REMARKS FROM    HREMPLOYMAST    A   JOIN HREMPLOYDETAILS B ON A.HREMPLOYMASTID=B.HREMPLOYMASTID AND B.IDACTIVE='YES' AND A.IDCARDNO=B.IDCARD   JOIN   GTCOMPMAST C ON C.GTCOMPMASTID = A.COMPCODE  JOIN GTDEPTDESGMAST  E ON E.GTDEPTDESGMASTID = B.DEPTNAME    JOIN HRECONTACTDETAILS F ON E.COMPCODE = A.COMPCODE AND E.COMPCODE = C.GTCOMPMASTID   AND F.HREMPLOYMASTID = A.HREMPLOYMASTID AND F.HREMPLOYMASTID = B.HREMPLOYMASTID    join hostellivedata g on G.COMPCODE=C.COMPCODE and G.IDCARDNO=B.IDCARD and B.HOSTEL='YES'  left outer join  ASPTBLEMP h on  H.COMPCODE=c.GTCOMPMASTID and H.IDCARDNO=B.MIDCARD   and H.EMPID=A.HREMPLOYMASTID   )x where x.photo is null   ORDER BY 6";
                    string sel2 = "SELECT  x.MIDCARD,x.HREMPLOYMASTID,x.FNAME,x.DISPNAME,x.photo,x.REMARKS , '-' as ASPTBLHOSTELGATEPASSID,x.COMPCODE,x.DESIGNATION,x.CONTACTNO,x.HOSTELNAME ,x.ROOMNO,x.REASON,x.PERMISSIONHRS,x.INTIME,x.OUTTIME, x.BLOCKFLOOR from ( SELECT B.MIDCARD,a.HREMPLOYMASTID,CONCAT(a.fname ,concat('-',B.MIDCARD) ) as FNAME,E.MNNAME1 as DISPNAME,  H.IMAGEBYTES AS PHOTO, '-' as ASPTBLHOSTELGATEPASSID,c.compname2 AS COMPCODE,'ALL' as DESIGNATION,C.COMPCODE as REMARKS,'-' as CONTACTNO,'-' as HOSTELNAME ,'-' as ROOMNO,'-' as REASON,'-' as PERMISSIONHRS,'-' as INTIME,'-' as OUTTIME,  '-' as  BLOCKFLOOR  FROM    HREMPLOYMAST    A   JOIN HREMPLOYDETAILS B ON A.HREMPLOYMASTID=B.HREMPLOYMASTID AND B.IDACTIVE='YES' AND A.IDCARDNO=B.IDCARD   JOIN   GTCOMPMAST C ON C.GTCOMPMASTID = A.COMPCODE  JOIN GTDEPTDESGMAST  E ON E.GTDEPTDESGMASTID = B.DEPTNAME    JOIN HRECONTACTDETAILS F ON E.COMPCODE = A.COMPCODE AND E.COMPCODE = C.GTCOMPMASTID   AND F.HREMPLOYMASTID = A.HREMPLOYMASTID AND F.HREMPLOYMASTID = B.HREMPLOYMASTID    join hostellivedata g on G.COMPCODE=C.COMPCODE and G.IDCARDNO=B.IDCARD and B.HOSTEL='YES'  left outer join  ASPTBLEMP h on  H.COMPCODE=c.GTCOMPMASTID and H.IDCARDNO=B.MIDCARD   and H.EMPID=A.HREMPLOYMASTID   )x where x.photo is null   ORDER BY 6";
                    DataSet ds2 = Utility.ExecuteSelectQuery(sel2, "HREMPLOYMAST");
                    //  dtgeneral = null;
                    dtgeneral = ds2.Tables["HREMPLOYMAST"];
                    rd.SetDataSource(dtgeneral);
                    crystalReportViewer1.ReportSource = null;
                    crystalReportViewer1.ReportSource = rd;
                    crystalReportViewer1.Refresh(); lblcount.Visible = true; lblcount.Text = "Total  Count:-" + dtgeneral.Rows.Count.ToString(); txtsearch.Text = ""; return;
                }

               

                if (comboBox1.Text == "" && combohostel.Text != "" && comboidcardsearch.Text == "" )
                {
                    string sel2 = "SELECT to_char(A.ASPTBLHOSTELGATEPASSID) as ASPTBLHOSTELGATEPASSID,B.COMPNAME2 AS COMPCODE,'ALL' as DESIGNATION, D.MIDCARD ,C.FNAME ,E.MNNAME1 DISPNAME, substr(A.SYSTEMDATE,1,10) AS CONTACTNO,F.HOSTELNAME , F.ROOMNO,G.REASON,A.PERMISSIONHRS,A.INTIME,A.OUTTIME,  A.REMARKS || ' - ' || A.REMARKS1 AS REMARKS,A.TIMEDIFF as BLOCKFLOOR   FROM ASPTBLHOSTELGATEPASS A  JOIN   GTCOMPMAST B ON B.GTCOMPMASTID = A.COMPCODE  JOIN   HREMPLOYMAST    C ON C.COMPCODE=B.GTCOMPMASTID  JOIN HREMPLOYDETAILS D ON D.HREMPLOYMASTID=C.HREMPLOYMASTID AND D.IDCARD=C.IDCARDNO AND  D.DEPTNAME=A.DEPARTMENT AND D.MIDCARD=A.IDCARDNO     JOIN GTDEPTDESGMAST  E ON E.GTDEPTDESGMASTID = A.DEPARTMENT  JOIN HOSTELLIVEDATA F ON F.COMPCODE=B.COMPCODE AND F.IDCARDNO=C.IDCARDNO    AND F.IDCARDNO=D.IDCARD  AND D.IDCARD=F.IDCARDNO JOIN ASPTBLREASONMAS G ON G.ASPTBLREASONMASID=A.REASON     JOIN  HRECONTACTDETAILS H ON H.HREMPLOYMASTID=C.HREMPLOYMASTID WHERE NOT E.mnname1='SECURITY'  AND A.HOSTELNAME='" + combohostel.Text + "' and  A.MODIFIED between TO_DATE('" + frmdate.Value.ToString("dd-MM-yyyy") + "','dd-MM-yyyy') and TO_DATE('" + todate.Value.ToString("dd-MM-yyyy") + "','dd-MM-yyyy') ORDER BY 1";
                    DataSet ds2 = Utility.ExecuteSelectQuery(sel2, "ASPTBLHOSTELGATEPASS");
                     dtgeneral = null;
                    dtgeneral = ds2.Tables["ASPTBLHOSTELGATEPASS"];
                    rd.SetDataSource(dtgeneral);
                    crystalReportViewer1.ReportSource = null;
                    crystalReportViewer1.ReportSource = rd;
                    crystalReportViewer1.Refresh(); lblcount.Visible = true; lblcount.Text = "Total  Count:-" + dtgeneral.Rows.Count.ToString(); txtsearch.Text = ""; return;
                }
                if (comboBox1.Text == "" && combohostel.Text == "" && comboidcardsearch.Text != "")
                {
                    string sel2 = "SELECT DISTINCT to_char(A.ASPTBLHOSTELGATEPASSID) as ASPTBLHOSTELGATEPASSID,B.COMPNAME2 AS COMPCODE,'" + combocompcode.Text + "' as DESIGNATION, D.MIDCARD ,C.FNAME ,E.MNNAME1 DISPNAME, substr(A.SYSTEMDATE,1,10) AS CONTACTNO,F.HOSTELNAME , F.ROOMNO,G.REASON,A.PERMISSIONHRS,A.INTIME,A.OUTTIME,A.REMARKS || ' - ' || A.REMARKS1 AS REMARKS,A.TIMEDIFF as BLOCKFLOOR   FROM ASPTBLHOSTELGATEPASS A  JOIN   GTCOMPMAST B ON B.GTCOMPMASTID = A.COMPCODE  JOIN   HREMPLOYMAST    C ON C.COMPCODE=B.GTCOMPMASTID  JOIN HREMPLOYDETAILS D ON D.HREMPLOYMASTID=C.HREMPLOYMASTID AND D.IDCARD=C.IDCARDNO AND  D.DEPTNAME=A.DEPARTMENT AND D.MIDCARD=A.IDCARDNO     JOIN GTDEPTDESGMAST  E ON E.GTDEPTDESGMASTID = A.DEPARTMENT  JOIN HOSTELLIVEDATA F ON F.COMPCODE=B.COMPCODE AND F.IDCARDNO=C.IDCARDNO    AND F.IDCARDNO=D.IDCARD  AND D.IDCARD=F.IDCARDNO JOIN ASPTBLREASONMAS G ON G.ASPTBLREASONMASID=A.REASON     JOIN  HRECONTACTDETAILS H ON H.HREMPLOYMASTID=C.HREMPLOYMASTID     WHERE  D.MIDCARD='" + comboidcardsearch.Text + "'    ORDER BY 1";
                    DataSet ds2 = Utility.ExecuteSelectQuery(sel2, "ASPTBLHOSTELGATEPASS");
                    dtgeneral = ds2.Tables["ASPTBLHOSTELGATEPASS"];
                    rd.SetDataSource(dtgeneral);
                    crystalReportViewer1.ReportSource = null;
                    crystalReportViewer1.ReportSource = rd;
                    crystalReportViewer1.Refresh();
                    lblcount.Visible = true; lblcount.Text = "Total  Count:-" + dtgeneral.Rows.Count.ToString();
                    txtsearch.Text = ""; return;
                }
                if (dtgeneral == null)
                {
                    crystalReportViewer1.ReportSource = null; crystalReportViewer1.Refresh();
                }
                crystalReportViewer1.Zoom(130);
            }
            catch (Exception EX)
            { crystalReportViewer1.ReportSource = null; crystalReportViewer1.Refresh(); }
           
        }

        private void txtsearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtsearch.Text.Length >=4)
                {
                   
                        string sel2 = "SELECT to_char(A.ASPTBLHOSTELGATEPASSID) as ASPTBLHOSTELGATEPASSID,B.COMPNAME AS COMPCODE,'ALL' as DESIGNATION, D.MIDCARD ,C.FNAME ,E.MNNAME1 DISPNAME, substr(A.SYSTEMDATE,1,10) AS CONTACTNO,a.HOSTELNAME , a.HOSTELROOM1 as ROOMNO,G.REASON,'HOSTELNAME' as  PERMISSIONHRS ,A.INTIME,A.OUTTIME, A.REMARKS || ' - ' || A.REMARKS1 AS REMARKS ,A.TIMEDIFF as BLOCKFLOOR   FROM ASPTBLHOSTELGATEPASS A  JOIN   GTCOMPMAST B ON B.GTCOMPMASTID = A.COMPCODE  JOIN   HREMPLOYMAST    C ON C.COMPCODE=B.GTCOMPMASTID  JOIN HREMPLOYDETAILS D ON D.HREMPLOYMASTID=C.HREMPLOYMASTID AND D.IDCARD=C.IDCARDNO AND  D.DEPTNAME=A.DEPARTMENT AND D.MIDCARD=A.IDCARDNO     JOIN GTDEPTDESGMAST  E ON E.GTDEPTDESGMASTID = A.DEPARTMENT JOIN ASPTBLREASONMAS G ON G.ASPTBLREASONMASID=A.REASON     JOIN  HRECONTACTDETAILS H ON H.HREMPLOYMASTID=C.HREMPLOYMASTID WHERE   D.MIDCARD LIKE'%" + txtsearch.Text + "%' OR  C.FNAME LIKE'%" + txtsearch.Text + "%'   ORDER BY 1";
                        DataSet ds2 = Utility.ExecuteSelectQuery(sel2, "ASPTBLHOSTELGATEPASS");
                        dtgeneral = null;
                        dtgeneral = ds2.Tables["ASPTBLHOSTELGATEPASS"];
             
                    
                    if (dtgeneral.Rows.Count > 0)
                    {
                        rd.SetDataSource(dtgeneral);
                        crystalReportViewer1.ReportSource = null;
                        crystalReportViewer1.ReportSource = rd;

                        crystalReportViewer1.Refresh(); txtsearch.Focus();
                    }
                }
                //if (combocompcode.Text != null && txtsearch.Text != "")
                //{
                //    string sel2 = "SELECT to_char(A.ASPTBLHOSTELGATEPASSID) as ASPTBLHOSTELGATEPASSID,B.COMPNAME AS COMPCODE,'" + combocompcode.Text + "' as DESIGNATION, D.MIDCARD ,C.FNAME ,E.MNNAME1 DISPNAME, substr(A.SYSTEMDATE,1,10) AS CONTACTNO,F.HOSTELNAME , F.ROOMNO,G.REASON,A.PERMISSIONHRS,A.INTIME,A.OUTTIME,  A.REMARKS,A.TIMEDIFF as BLOCKFLOOR   FROM ASPTBLHOSTELGATEPASS A  JOIN   GTCOMPMAST B ON B.GTCOMPMASTID = A.COMPCODE  JOIN   HREMPLOYMAST    C ON C.COMPCODE=B.GTCOMPMASTID  JOIN HREMPLOYDETAILS D ON D.HREMPLOYMASTID=C.HREMPLOYMASTID AND D.IDCARD=C.IDCARDNO AND  D.DEPTNAME=A.DEPARTMENT AND D.MIDCARD=A.IDCARDNO     JOIN GTDEPTDESGMAST  E ON E.GTDEPTDESGMASTID = A.DEPARTMENT  JOIN HOSTELLIVEDATA F ON F.COMPCODE=B.COMPCODE AND F.IDCARDNO=C.IDCARDNO    AND F.IDCARDNO=D.IDCARD  AND D.IDCARD=F.IDCARDNO JOIN ASPTBLREASONMAS G ON G.ASPTBLREASONMASID=A.REASON     JOIN  HRECONTACTDETAILS H ON H.HREMPLOYMASTID=C.HREMPLOYMASTID WHERE NOT E.mnname1='SECURITY'  AND A.HOSTELNAME='" + combohostel.Text + "' and  A.MODIFIED between TO_DATE('" + frmdate.Value.ToString("dd-MM-yyyy") + "','dd-MM-yyyy') and TO_DATE('" + todate.Value.ToString("dd-MM-yyyy") + "','dd-MM-yyyy') AND  D.MIDCARD LIKE'%" + txtsearch.Text + "%'  OR  C.FNAME LIKE'%" + txtsearch.Text + "%' ORDER BY 1";

                //    DataSet ds2 = Utility.ExecuteSelectQuery(sel2, "ASPTBLHOSTELGATEPASS");
                //    dtgeneral = null;
                //    dtgeneral = ds2.Tables["ASPTBLHOSTELGATEPASS"];
                //    rd.SetDataSource(dtgeneral);
                //    crystalReportViewer1.ReportSource = null;
                //    crystalReportViewer1.ReportSource = rd;

                //    crystalReportViewer1.Refresh(); txtsearch.Focus();
                //}
                else
                {
                    // butView_Click(sender,e);
                }
            }
            catch (Exception EX)
            { MessageBox.Show(EX.Message); }
        }

        private void comboformate_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

       

        public void Prints()
        {
            try
            {
                
                    if (printDialog1.ShowDialog() == DialogResult.OK)
                    {
                        //    string sel2 = "SELECT to_char(A.ASPTBLHOSTELGATEPASSID) as ASPTBLHOSTELGATEPASSID,B.COMPNAME AS COMPCODE,'" + combocompcode.Text + "' as DESIGNATION, D.MIDCARD ,C.FNAME ,E.MNNAME1 DISPNAME, substr(A.SYSTEMDATE,1,10) AS CONTACTNO,F.HOSTELNAME , F.ROOMNO,G.REASON,A.PERMISSIONHRS,A.INTIME,A.OUTTIME,  A.REMARKS,A.TIMEDIFF as BLOCKFLOOR   FROM ASPTBLHOSTELGATEPASS A  JOIN   GTCOMPMAST B ON B.GTCOMPMASTID = A.COMPCODE  JOIN   HREMPLOYMAST    C ON C.COMPCODE=B.GTCOMPMASTID  JOIN HREMPLOYDETAILS D ON D.HREMPLOYMASTID=C.HREMPLOYMASTID AND D.IDCARD=C.IDCARDNO AND  D.DEPTNAME=A.DEPARTMENT AND D.MIDCARD=A.IDCARDNO     JOIN GTDEPTDESGMAST  E ON E.GTDEPTDESGMASTID = A.DEPARTMENT  JOIN HOSTELLIVEDATA F ON F.COMPCODE=B.COMPCODE AND F.IDCARDNO=C.IDCARDNO    AND F.IDCARDNO=D.IDCARD  AND D.IDCARD=F.IDCARDNO JOIN ASPTBLREASONMAS G ON G.ASPTBLREASONMASID=A.REASON     JOIN  HRECONTACTDETAILS H ON H.HREMPLOYMASTID=C.HREMPLOYMASTID    WHERE  B.COMPCODE='" + combocompcode.Text + "' AND A.MODIFIED between TO_DATE('" + frmdate.Value.ToString("dd-MM-yyyy") + "','dd-MM-yyyy') and TO_DATE('" + todate.Value.ToString("dd-MM-yyyy") + "','dd-MM-yyyy') ORDER BY 1";
                        //    DataSet ds2 = Utility.ExecuteSelectQuery(sel2, "ASPTBLHOSTELGATEPASS");
                        //    DataTable dt2 = ds2.Tables["ASPTBLHOSTELGATEPASS"];

                        if (combocompcode.Text == "TOTAL COUNT" && combohostel.Text != "")
                        {

                            //rd2.SetDataSource(dtgeneral);                       
                            //crystalReportViewer1.ReportSource = null;
                            //crystalReportViewer1.ReportSource = rd2;
                            //crystalReportViewer1.Refresh();
                            //.PrintOptions.PrinterName = printDialog1.PrinterSettings.PrinterName;
                            //rd2.PrintToPrinter(printDialog1.PrinterSettings.Copies, printDialog1.PrinterSettings.Collate, printDialog1.PrinterSettings.FromPage, printDialog1.PrinterSettings.ToPage);

                            //CrystalDecisions.CrystalReports.Engine.ReportDocument reportdocument = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                            //reportdocument.Load(Application.StartupPath + "\\Report\\MovementReport2.rpt");
                            //reportdocument.SetDataSource(dtgeneral);
                            rd.PrintOptions.PrinterName = printDialog1.PrinterSettings.PrinterName;
                            rd.PrintToPrinter(printDialog1.PrinterSettings.Copies, printDialog1.PrinterSettings.Collate, printDialog1.PrinterSettings.FromPage, printDialog1.PrinterSettings.ToPage);

                        }
                        else
                        {



                            //CrystalDecisions.CrystalReports.Engine.ReportDocument reportdocument = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                            //rd.Load(Application.StartupPath + "\\Report\\MovementReport.rpt");
                            //rd.SetDataSource(dtgeneral);
                            rd.PrintOptions.PrinterName = printDialog1.PrinterSettings.PrinterName;
                            rd.PrintToPrinter(printDialog1.PrinterSettings.Copies, printDialog1.PrinterSettings.Collate, printDialog1.PrinterSettings.FromPage, printDialog1.PrinterSettings.ToPage);

                           
                        }
                    }
                
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }

        private void rToolStripMenuItem_Click(object sender, EventArgs e)
        {
            comboidcardsearch.Items.Clear();
        }

        private void outTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (combohostel.Text == "WOMENS HOSTEL" || combohostel.Text == "WORKING GENTS HOSTEL" || combohostel.Text == "GENTS STAFF HOSTEL")
            {
                //combocompcode.Text = "OutTime Only";
                string sel2 = "SELECT to_char(A.ASPTBLHOSTELGATEPASSID) as ASPTBLHOSTELGATEPASSID,B.COMPNAME AS COMPCODE,'ALL' as DESIGNATION, D.MIDCARD ,C.FNAME ,E.MNNAME1 DISPNAME, substr(A.SYSTEMDATE,1,10) AS CONTACTNO,F.HOSTELNAME , F.ROOMNO,G.REASON,'HOSTELNAME' as  PERMISSIONHRS ,A.INTIME,A.OUTTIME, A.REMARKS || ' - ' || A.REMARKS1 AS REMARKS ,A.TIMEDIFF as BLOCKFLOOR FROM ASPTBLHOSTELGATEPASS A  JOIN   GTCOMPMAST B ON B.GTCOMPMASTID = A.COMPCODE  JOIN   HREMPLOYMAST    C ON C.COMPCODE=B.GTCOMPMASTID  JOIN HREMPLOYDETAILS D ON D.HREMPLOYMASTID=C.HREMPLOYMASTID AND D.IDCARD=C.IDCARDNO AND  D.DEPTNAME=A.DEPARTMENT AND D.MIDCARD=A.IDCARDNO     JOIN GTDEPTDESGMAST  E ON E.GTDEPTDESGMASTID = A.DEPARTMENT  JOIN HOSTELLIVEDATA F ON F.COMPCODE=B.COMPCODE AND F.IDCARDNO=C.IDCARDNO    AND F.IDCARDNO=D.IDCARD  AND D.IDCARD=F.IDCARDNO JOIN ASPTBLREASONMAS G ON G.ASPTBLREASONMASID=A.REASON     JOIN  HRECONTACTDETAILS H ON H.HREMPLOYMASTID=C.HREMPLOYMASTID WHERE NOT E.mnname1='SECURITY'  AND A.INTIME IS   NULL    AND A.OUTTIME IS NOT  NULL  AND A.HOSTELNAME='" + combohostel.Text + "' and  A.MODIFIED between TO_DATE('" + frmdate.Value.ToString("dd-MM-yyyy") + "','dd-MM-yyyy') and TO_DATE('" + todate.Value.ToString("dd-MM-yyyy") + "','dd-MM-yyyy') ORDER BY 1";

                DataSet ds2 = Utility.ExecuteSelectQuery(sel2, "ASPTBLHOSTELGATEPASS");
                DataTable dt2 = ds2.Tables["ASPTBLHOSTELGATEPASS"];
                rd.SetDataSource(dt2);
                crystalReportViewer1.ReportSource = null;
                crystalReportViewer1.ReportSource = rd;
                lblcount.Visible = true; lblcount.Text = "Total  Count:-" + dt2.Rows.Count.ToString();
                crystalReportViewer1.Refresh(); txtsearch.Text = "";
            }
            else
            {
                //combocompcode.Text = "OutTime Only";
                string sel2 = "SELECT to_char(A.ASPTBLHOSTELGATEPASSID) as ASPTBLHOSTELGATEPASSID,B.COMPNAME AS COMPCODE,'" + combocompcode.Text + "' as DESIGNATION, D.MIDCARD ,C.FNAME ,E.MNNAME1 DISPNAME, substr(A.SYSTEMDATE,1,10) AS CONTACTNO,A.HOSTELNAME , '' AS ROOMNO,G.REASON,'COMPCODE' as  PERMISSIONHRS ,A.INTIME,A.OUTTIME, A.REMARKS || ' - ' || A.REMARKS1 AS REMARKS ,A.TIMEDIFF as BLOCKFLOOR  FROM ASPTBLHOSTELGATEPASS A  JOIN   GTCOMPMAST B ON B.GTCOMPMASTID = A.COMPCODE  JOIN   HREMPLOYMAST    C ON C.COMPCODE=B.GTCOMPMASTID  JOIN HREMPLOYDETAILS D ON D.HREMPLOYMASTID=C.HREMPLOYMASTID AND D.IDCARD=C.IDCARDNO AND  D.DEPTNAME=A.DEPARTMENT AND D.MIDCARD=A.IDCARDNO     JOIN GTDEPTDESGMAST  E ON E.GTDEPTDESGMASTID = A.DEPARTMENT   JOIN ASPTBLREASONMAS G ON G.ASPTBLREASONMASID=A.REASON     JOIN  HRECONTACTDETAILS H ON H.HREMPLOYMASTID=C.HREMPLOYMASTID     WHERE A.INTIME IS   NULL    AND A.OUTTIME IS NOT  NULL   AND  A.HOSTELNAME='" + combocompcode.Text + "' AND A.MODIFIED between TO_DATE('" + frmdate.Value.ToString("dd-MM-yyyy") + "','dd-MM-yyyy') and TO_DATE('" + todate.Value.ToString("dd-MM-yyyy") + "','dd-MM-yyyy') ORDER BY 1";

                DataSet ds2 = Utility.ExecuteSelectQuery(sel2, "ASPTBLHOSTELGATEPASS");
                DataTable dt2 = ds2.Tables["ASPTBLHOSTELGATEPASS"];
                rd.SetDataSource(dt2);
                crystalReportViewer1.ReportSource = null;
                crystalReportViewer1.ReportSource = rd;
                lblcount.Visible = true; lblcount.Text = "Total  Count:-" + dt2.Rows.Count.ToString();
                crystalReportViewer1.Refresh(); txtsearch.Text = "";
            }
        }

        private void News_Click(object sender, EventArgs e)
        {

        }

        private void combohostel_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void comboidcardsearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboidcardsearch.Text != "")
            {
                Class.Users.UserTime = 0;
                if (Class.Users.HostelName == "WOMENS HOSTEL" || Class.Users.HostelName == "WORKING GENTS HOSTEL" || Class.Users.HostelName == "WORKING GENTS HOSTEL")
                {
                    string sel2 = "SELECT to_char(A.ASPTBLHOSTELGATEPASSID) as ASPTBLHOSTELGATEPASSID,B.COMPNAME2 AS COMPCODE, '" + combocompcode.Text + "' as  DESIGNATION, D.MIDCARD ,C.FNAME ,E.MNNAME1 DISPNAME, substr(A.SYSTEMDATE,1,10) AS CONTACTNO,F.HOSTELNAME , F.ROOMNO,G.REASON,'HOSTELNAME' as  PERMISSIONHRS ,A.INTIME,A.OUTTIME, A.REMARKS || ' - ' || A.REMARKS1 AS REMARKS ,A.TIMEDIFF as BLOCKFLOOR  FROM ASPTBLHOSTELGATEPASS A  JOIN   GTCOMPMAST B ON B.GTCOMPMASTID = A.COMPCODE  JOIN   HREMPLOYMAST    C ON C.COMPCODE=B.GTCOMPMASTID  JOIN HREMPLOYDETAILS D ON D.HREMPLOYMASTID=C.HREMPLOYMASTID AND D.IDCARD=C.IDCARDNO AND  D.DEPTNAME=A.DEPARTMENT AND D.MIDCARD=A.IDCARDNO     JOIN GTDEPTDESGMAST  E ON E.GTDEPTDESGMASTID = A.DEPARTMENT   LEFT JOIN HOSTELLIVEDATA F ON F.COMPCODE=B.COMPCODE AND F.IDCARDNO=C.IDCARDNO    AND F.IDCARDNO=D.IDCARD  AND D.IDCARD=F.IDCARDNO JOIN ASPTBLREASONMAS G ON G.ASPTBLREASONMASID=A.REASON     JOIN  HRECONTACTDETAILS H ON H.HREMPLOYMASTID=C.HREMPLOYMASTID   WHERE  D.MIDCARD='" + comboidcardsearch.Text + "'    ORDER BY A.ASPTBLHOSTELGATEPASSID DESC";
                    DataSet ds2 = Utility.ExecuteSelectQuery(sel2, "ASPTBLHOSTELGATEPASS");
                    dtgeneral = null;
                    dtgeneral = ds2.Tables["ASPTBLHOSTELGATEPASS"];
                    
                }
                else
                {

                    string sel2 = "SELECT to_char(A.ASPTBLHOSTELGATEPASSID) as ASPTBLHOSTELGATEPASSID,B.COMPNAME2 AS COMPCODE,'" + combocompcode.Text + "'  as DESIGNATION, D.MIDCARD ,C.FNAME ,E.MNNAME1 DISPNAME, substr(A.SYSTEMDATE,1,10) AS CONTACTNO,'" + combocompcode.Text+ "'  HOSTELNAME , ''ROOMNO,G.REASON,'COMPCODE' as  PERMISSIONHRS ,A.INTIME,A.OUTTIME, A.REMARKS || ' - ' || A.REMARKS1 AS REMARKS ,A.TIMEDIFF as BLOCKFLOOR   FROM ASPTBLHOSTELGATEPASS A  JOIN   GTCOMPMAST B ON B.GTCOMPMASTID = A.COMPCODE  JOIN   HREMPLOYMAST    C ON C.COMPCODE=B.GTCOMPMASTID  JOIN HREMPLOYDETAILS D ON D.HREMPLOYMASTID=C.HREMPLOYMASTID AND D.IDCARD=C.IDCARDNO AND  D.DEPTNAME=A.DEPARTMENT AND D.MIDCARD=A.IDCARDNO     JOIN GTDEPTDESGMAST  E ON E.GTDEPTDESGMASTID = A.DEPARTMENT    JOIN ASPTBLREASONMAS G ON G.ASPTBLREASONMASID=A.REASON     JOIN  HRECONTACTDETAILS H ON H.HREMPLOYMASTID=C.HREMPLOYMASTID   WHERE D.MIDCARD='" + comboidcardsearch.Text + "'    ORDER BY A.ASPTBLHOSTELGATEPASSID DESC";
                    DataSet ds2 = Utility.ExecuteSelectQuery(sel2, "ASPTBLHOSTELGATEPASS");
                    dtgeneral = null;
                    dtgeneral = ds2.Tables["ASPTBLHOSTELGATEPASS"];
                    
                }
                if (dtgeneral != null)
                {
                    rd.SetDataSource(dtgeneral);
                    crystalReportViewer1.ReportSource = null;
                    crystalReportViewer1.ReportSource = rd;

                    crystalReportViewer1.Refresh(); comboidcardsearch.Focus();
                }
                else
                {
                    crystalReportViewer1.ReportSource = null;
                    crystalReportViewer1.Refresh(); comboidcardsearch.Focus();
                }
                    lblcount.Visible = true; lblcount.Text = "Total  Count:-" + Convert.ToInt32("0" + dtgeneral.Rows.Count.ToString()); txtsearch.Text = ""; comboidcardsearch.Text = ""; return;

            }

        }

        private void Prints_Click_1(object sender, EventArgs e)
        {

        }

        private void Pdfs_Click(object sender, EventArgs e)
        {

        }

        private void DownLoads_Click_1(object sender, EventArgs e)
        {

        }

        private void comboformate_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void withoutInTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Class.Users.HostelName == "MENS HOTEL" || Class.Users.HostelName == "WORKING GENTS HOSTEL" || Class.Users.HostelName == "WOMENS HOSTEL" || Class.Users.HostelName == "GENTS STAFF HOSTEL" || Class.Users.HostelName == "BOYS HOSTEL")
            {
                string sel2 = "SELECT to_char(A.ASPTBLHOSTELGATEPASSID) as ASPTBLHOSTELGATEPASSID,B.COMPNAME AS COMPCODE,'ALL' as DESIGNATION, D.MIDCARD ,C.FNAME ,E.MNNAME1 DISPNAME, substr(A.SYSTEMDATE,1,10) AS CONTACTNO,F.HOSTELNAME , F.ROOMNO,G.REASON,'HOSTELNAME' as  PERMISSIONHRS ,A.INTIME,A.OUTTIME,A.REMARKS || ' - ' || A.REMARKS1 AS REMARKS ,A.TIMEDIFF as BLOCKFLOOR   FROM ASPTBLHOSTELGATEPASS A  JOIN   GTCOMPMAST B ON B.GTCOMPMASTID = A.COMPCODE  JOIN   HREMPLOYMAST    C ON C.COMPCODE=B.GTCOMPMASTID  JOIN HREMPLOYDETAILS D ON D.HREMPLOYMASTID=C.HREMPLOYMASTID AND D.IDCARD=C.IDCARDNO AND  D.DEPTNAME=A.DEPARTMENT AND D.MIDCARD=A.IDCARDNO     JOIN GTDEPTDESGMAST  E ON E.GTDEPTDESGMASTID = A.DEPARTMENT  JOIN HOSTELLIVEDATA F ON F.COMPCODE=B.COMPCODE AND F.IDCARDNO=C.IDCARDNO    AND F.IDCARDNO=D.IDCARD  AND D.IDCARD=F.IDCARDNO JOIN ASPTBLREASONMAS G ON G.ASPTBLREASONMASID=A.REASON     JOIN  HRECONTACTDETAILS H ON H.HREMPLOYMASTID=C.HREMPLOYMASTID WHERE NOT E.mnname1='SECURITY'  AND A.INTIME IS   NOT NULL  AND A.OUTTIME IS NOT  NULL AND A.HOSTELNAME='" + combohostel.Text + "' and  A.MODIFIED between TO_DATE('" + frmdate.Value.ToString("dd-MM-yyyy") + "','dd-MM-yyyy') and TO_DATE('" + todate.Value.ToString("dd-MM-yyyy") + "','dd-MM-yyyy') ORDER BY 1";
                //combocompcode.Text = "InTime & OutTime Only";
                DataSet ds2 = Utility.ExecuteSelectQuery(sel2, "ASPTBLHOSTELGATEPASS");
                DataTable dt2 = ds2.Tables["ASPTBLHOSTELGATEPASS"];
                rd.SetDataSource(dt2);
                crystalReportViewer1.ReportSource = null;
                crystalReportViewer1.ReportSource = rd;

                crystalReportViewer1.Refresh();
                lblcount.Visible = true; lblcount.Text = "Total  Count:-" + dt2.Rows.Count.ToString();
                txtsearch.Text = "";
            }
            else
            {
             //   string sel2 = "SELECT to_char(A.ASPTBLHOSTELGATEPASSID) as ASPTBLHOSTELGATEPASSID,B.COMPNAME AS COMPCODE,'" + combocompcode.Text + "' as DESIGNATION, D.MIDCARD ,C.FNAME ,E.MNNAME1 DISPNAME, substr(A.SYSTEMDATE,1,10) AS CONTACTNO,'' AS HOSTELNAME , ''AS ROOMNO,G.REASON,A.PERMISSIONHRS,A.INTIME,A.OUTTIME,  A.REMARKS || A.REMARKS1 AS REMARKS ,A.TIMEDIFF as BLOCKFLOOR   FROM ASPTBLHOSTELGATEPASS A  JOIN   GTCOMPMAST B ON B.GTCOMPMASTID = A.COMPCODE  JOIN   HREMPLOYMAST    C ON C.COMPCODE=B.GTCOMPMASTID  JOIN HREMPLOYDETAILS D ON D.HREMPLOYMASTID=C.HREMPLOYMASTID AND D.IDCARD=C.IDCARDNO AND  D.DEPTNAME=A.DEPARTMENT AND D.MIDCARD=A.IDCARDNO     JOIN GTDEPTDESGMAST  E ON E.GTDEPTDESGMASTID = A.DEPARTMENT  JOIN ASPTBLREASONMAS G ON G.ASPTBLREASONMASID=A.REASON     JOIN  HRECONTACTDETAILS H ON H.HREMPLOYMASTID=C.HREMPLOYMASTID WHERE A.INTIME IS   NOT NULL  AND A.OUTTIME IS NOT  NULL AND A.HOSTELNAME='" + combocompcode.Text + "' and  A.MODIFIED between TO_DATE('" + frmdate.Value.ToString("dd-MM-yyyy") + "','dd-MM-yyyy') and TO_DATE('" + todate.Value.ToString("dd-MM-yyyy") + "','dd-MM-yyyy') ORDER BY 1";
                string sel2 = "SELECT to_char(A.ASPTBLHOSTELGATEPASSID) as ASPTBLHOSTELGATEPASSID,B.COMPNAME AS COMPCODE,'" + combocompcode.Text + "' as DESIGNATION, D.MIDCARD ,C.FNAME ,E.MNNAME1 DISPNAME, substr(A.SYSTEMDATE,1,10) AS CONTACTNO,A.HOSTELNAME , '' AS ROOMNO,G.REASON,'COMPCODE' as  PERMISSIONHRS ,A.INTIME,A.OUTTIME, A.REMARKS || ' - ' || A.REMARKS1 AS REMARKS ,A.TIMEDIFF as BLOCKFLOOR   FROM ASPTBLHOSTELGATEPASS A  JOIN   GTCOMPMAST B ON B.GTCOMPMASTID = A.COMPCODE  JOIN   HREMPLOYMAST    C ON C.COMPCODE=B.GTCOMPMASTID  JOIN HREMPLOYDETAILS D ON D.HREMPLOYMASTID=C.HREMPLOYMASTID AND D.IDCARD=C.IDCARDNO AND  D.DEPTNAME=A.DEPARTMENT AND D.MIDCARD=A.IDCARDNO     JOIN GTDEPTDESGMAST  E ON E.GTDEPTDESGMASTID = A.DEPARTMENT   JOIN ASPTBLREASONMAS G ON G.ASPTBLREASONMASID=A.REASON     JOIN  HRECONTACTDETAILS H ON H.HREMPLOYMASTID=C.HREMPLOYMASTID     WHERE A.INTIME IS NOT  NULL AND A.OUTTIME IS NOT  NULL AND A.HOSTELNAME='" + combocompcode.Text + "' AND A.MODIFIED between TO_DATE('" + frmdate.Value.ToString("dd-MM-yyyy") + "','dd-MM-yyyy') and TO_DATE('" + todate.Value.ToString("dd-MM-yyyy") + "','dd-MM-yyyy') ORDER BY 1";

                DataSet ds2 = Utility.ExecuteSelectQuery(sel2, "ASPTBLHOSTELGATEPASS");
                DataTable dt2 = ds2.Tables["ASPTBLHOSTELGATEPASS"];
                rd.SetDataSource(dt2);
                crystalReportViewer1.ReportSource = null;
                crystalReportViewer1.ReportSource = rd;

                crystalReportViewer1.Refresh();
                lblcount.Visible = true; lblcount.Text = "Total  Count:-" + dt2.Rows.Count.ToString();
                txtsearch.Text = "";
            }
        }

        private void withoutInTimeOutTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Class.Users.HostelName == "MENS HOTEL" || Class.Users.HostelName == "WORKING GENTS HOSTEL" || Class.Users.HostelName == "WOMENS HOSTEL" || Class.Users.HostelName == "GENTS STAFF HOSTEL" || Class.Users.HostelName == "BOYS HOSTEL")
            {
                string sel2 = "SELECT to_char(A.ASPTBLHOSTELGATEPASSID) as ASPTBLHOSTELGATEPASSID,B.COMPNAME AS COMPCODE,'ALL' as DESIGNATION, D.MIDCARD ,C.FNAME ,E.MNNAME1 DISPNAME, substr(A.SYSTEMDATE,1,10) AS CONTACTNO,F.HOSTELNAME , F.ROOMNO,G.REASON,A.PERMISSIONHRS,A.INTIME,A.OUTTIME, A.REMARKS || ' - ' || A.REMARKS1 AS REMARKS ,A.TIMEDIFF as BLOCKFLOOR, 'HOSTELNAME' as  PERMISSIONHRS    FROM ASPTBLHOSTELGATEPASS A  JOIN   GTCOMPMAST B ON B.GTCOMPMASTID = A.COMPCODE  JOIN   HREMPLOYMAST    C ON C.COMPCODE=B.GTCOMPMASTID  JOIN HREMPLOYDETAILS D ON D.HREMPLOYMASTID=C.HREMPLOYMASTID AND D.IDCARD=C.IDCARDNO AND  D.DEPTNAME=A.DEPARTMENT AND D.MIDCARD=A.IDCARDNO     JOIN GTDEPTDESGMAST  E ON E.GTDEPTDESGMASTID = A.DEPARTMENT  JOIN HOSTELLIVEDATA F ON F.COMPCODE=B.COMPCODE AND F.IDCARDNO=C.IDCARDNO    AND F.IDCARDNO=D.IDCARD  AND D.IDCARD=F.IDCARDNO JOIN ASPTBLREASONMAS G ON G.ASPTBLREASONMASID=A.REASON     JOIN  HRECONTACTDETAILS H ON H.HREMPLOYMASTID=C.HREMPLOYMASTID WHERE NOT E.mnname1='SECURITY'  AND A.INTIME IS  NULL AND A.OUTTIME IS  NULL AND A.HOSTELNAME='" + combohostel.Text + "' and  A.MODIFIED between TO_DATE('" + frmdate.Value.ToString("dd-MM-yyyy") + "','dd-MM-yyyy') and TO_DATE('" + todate.Value.ToString("dd-MM-yyyy") + "','dd-MM-yyyy') ORDER BY 1";

                DataSet ds2 = Utility.ExecuteSelectQuery(sel2, "ASPTBLHOSTELGATEPASS");
                DataTable dt2 = ds2.Tables["ASPTBLHOSTELGATEPASS"];
                // combocompcode.Text = "Without InTime & OutTime";
                rd.SetDataSource(dt2);
                crystalReportViewer1.ReportSource = null;
                crystalReportViewer1.ReportSource = rd;
                lblcount.Visible = true; lblcount.Text = "Total  Count:-" + dt2.Rows.Count.ToString();
                crystalReportViewer1.Refresh(); txtsearch.Text = "";
            }
            else
            {
                //string sel2 = "SELECT to_char(A.ASPTBLHOSTELGATEPASSID) as ASPTBLHOSTELGATEPASSID,B.COMPNAME AS COMPCODE,'" + combocompcode.Text + "' as  DESIGNATION, D.MIDCARD ,C.FNAME ,E.MNNAME1 DISPNAME, substr(A.SYSTEMDATE,1,10) AS CONTACTNO,A.HOSTELNAME , ''AS ROOMNO,G.REASON,A.PERMISSIONHRS,A.INTIME,A.OUTTIME,  A.REMARKS || A.REMARKS1 AS REMARKS ,A.TIMEDIFF as BLOCKFLOOR   FROM ASPTBLHOSTELGATEPASS A  JOIN   GTCOMPMAST B ON B.GTCOMPMASTID = A.COMPCODE  JOIN   HREMPLOYMAST    C ON C.COMPCODE=B.GTCOMPMASTID  JOIN HREMPLOYDETAILS D ON D.HREMPLOYMASTID=C.HREMPLOYMASTID AND D.IDCARD=C.IDCARDNO AND  D.DEPTNAME=A.DEPARTMENT AND D.MIDCARD=A.IDCARDNO     JOIN GTDEPTDESGMAST  E ON E.GTDEPTDESGMASTID = A.DEPARTMENT   JOIN ASPTBLREASONMAS G ON G.ASPTBLREASONMASID=A.REASON     JOIN  HRECONTACTDETAILS H ON H.HREMPLOYMASTID=C.HREMPLOYMASTID WHERE A.INTIME IS NULL AND A.OUTTIME IS  NULL AND A.HOSTELNAME='" + combocompcode.Text + "' and  A.MODIFIED between TO_DATE('" + frmdate.Value.ToString("dd-MM-yyyy") + "','dd-MM-yyyy') and TO_DATE('" + todate.Value.ToString("dd-MM-yyyy") + "','dd-MM-yyyy') ORDER BY 1";
                string sel2 = "SELECT to_char(A.ASPTBLHOSTELGATEPASSID) as ASPTBLHOSTELGATEPASSID,B.COMPNAME AS COMPCODE,'" + combocompcode.Text + "' as DESIGNATION, D.MIDCARD ,C.FNAME ,E.MNNAME1 DISPNAME, substr(A.SYSTEMDATE,1,10) AS CONTACTNO,A.HOSTELNAME , '' AS ROOMNO,G.REASON, 'COMPCODE' as  PERMISSIONHRS  ,A.INTIME,A.OUTTIME,A.REMARKS || ' - ' || A.REMARKS1 AS REMARKS ,A.TIMEDIFF as BLOCKFLOOR   FROM ASPTBLHOSTELGATEPASS A  JOIN   GTCOMPMAST B ON B.GTCOMPMASTID = A.COMPCODE  JOIN   HREMPLOYMAST    C ON C.COMPCODE=B.GTCOMPMASTID  JOIN HREMPLOYDETAILS D ON D.HREMPLOYMASTID=C.HREMPLOYMASTID AND D.IDCARD=C.IDCARDNO AND  D.DEPTNAME=A.DEPARTMENT AND D.MIDCARD=A.IDCARDNO     JOIN GTDEPTDESGMAST  E ON E.GTDEPTDESGMASTID = A.DEPARTMENT   JOIN ASPTBLREASONMAS G ON G.ASPTBLREASONMASID=A.REASON     JOIN  HRECONTACTDETAILS H ON H.HREMPLOYMASTID=C.HREMPLOYMASTID WHERE A.INTIME IS NULL AND A.OUTTIME IS  NULL AND A.HOSTELNAME='" + combocompcode.Text + "' AND A.MODIFIED between TO_DATE('" + frmdate.Value.ToString("dd-MM-yyyy") + "','dd-MM-yyyy') and TO_DATE('" + todate.Value.ToString("dd-MM-yyyy") + "','dd-MM-yyyy') ORDER BY 1";

                DataSet ds2 = Utility.ExecuteSelectQuery(sel2, "ASPTBLHOSTELGATEPASS");
                DataTable dt2 = ds2.Tables["ASPTBLHOSTELGATEPASS"];
                // combocompcode.Text = "Without InTime & OutTime";
                rd.SetDataSource(dt2);
                crystalReportViewer1.ReportSource = null;
                crystalReportViewer1.ReportSource = rd;
                lblcount.Visible = true; lblcount.Text = "Total  Count:-" + dt2.Rows.Count.ToString();
                crystalReportViewer1.Refresh(); txtsearch.Text = "";
            }
        }

        public void News()
        {
            hostelload();
            COMBCODELOAD(); txtsearch.Text = "";
            butheader.BackColor = Class.Users.BackColors;
            butfooter.BackColor = Class.Users.BackColors;

          
            crystalReportViewer1.ReportSource = null;
            crystalReportViewer1.Refresh();
           
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

        public void Pdfs()
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
            GlobalVariables.MdiPanel.Show();
            News();
            GlobalVariables.HeaderName.Text = "";
            GlobalVariables.TabCtrl.TabPages.RemoveAt(GlobalVariables.TabCtrl.SelectedIndex);
            this.Hide();
          
        }

        public void GridLoad()
        {
           
        }

        private void hostelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sel2 = "SELECT to_char(A.ASPTBLHOSTELGATEPASSID) as ASPTBLHOSTELGATEPASSID,B.COMPNAME AS COMPCODE,'ALL' as DESIGNATION, D.MIDCARD ,C.FNAME ,E.MNNAME1 DISPNAME, substr(A.SYSTEMDATE,1,10) AS CONTACTNO,F.HOSTELNAME , F.ROOMNO,G.REASON,A.PERMISSIONHRS,A.INTIME,A.OUTTIME,A.REMARKS || ' - ' || A.REMARKS1 AS REMARKS ,A.TIMEDIFF as BLOCKFLOOR, 'HOSTEL NAME' as  PERMISSIONHRS    FROM ASPTBLHOSTELGATEPASS A  JOIN   GTCOMPMAST B ON B.GTCOMPMASTID = A.COMPCODE  JOIN   HREMPLOYMAST    C ON C.COMPCODE=B.GTCOMPMASTID  JOIN HREMPLOYDETAILS D ON D.HREMPLOYMASTID=C.HREMPLOYMASTID AND D.IDCARD=C.IDCARDNO AND  D.DEPTNAME=A.DEPARTMENT AND D.MIDCARD=A.IDCARDNO     JOIN GTDEPTDESGMAST  E ON E.GTDEPTDESGMASTID = A.DEPARTMENT  JOIN HOSTELLIVEDATA F ON F.COMPCODE=B.COMPCODE AND F.IDCARDNO=C.IDCARDNO    AND F.IDCARDNO=D.IDCARD  AND D.IDCARD=F.IDCARDNO JOIN ASPTBLREASONMAS G ON G.ASPTBLREASONMASID=A.REASON     JOIN  HRECONTACTDETAILS H ON H.HREMPLOYMASTID=C.HREMPLOYMASTID  WHERE NOT E.mnname1='SECURITY' AND G.REASON='HOSTEL' AND A.HOSTELNAME='" + Class.Users.HostelName + "'  AND  A.MODIFIED between TO_DATE('" + frmdate.Value.ToString("dd-MM-yyyy") + "','dd-MM-yyyy') and TO_DATE('" + todate.Value.ToString("dd-MM-yyyy") + "','dd-MM-yyyy')  ORDER BY 1";
            DataSet ds2 = Utility.ExecuteSelectQuery(sel2, "ASPTBLHOSTELGATEPASS");         
            dtgeneral = ds2.Tables["ASPTBLHOSTELGATEPASS"];
            rd.SetDataSource(dtgeneral);
            crystalReportViewer1.ReportSource = null;
            crystalReportViewer1.ReportSource = rd;
            crystalReportViewer1.Refresh(); lblcount.Visible = true; lblcount.Text = "Total  Count:-" + dtgeneral.Rows.Count.ToString(); txtsearch.Text = ""; return;

        }

        public void Searchs(int id)
        {
            throw new NotImplementedException();
        }

        public void Deletes(int id)
        {
            throw new NotImplementedException();
        }

        private void checkdatabase_CheckedChanged(object sender, EventArgs e)
        {
            mas.DatabaseCheck(checkdatabase);
        }
    }
}
