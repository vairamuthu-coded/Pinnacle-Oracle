using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.Shared;

namespace Pinnacle.ReportFormate
{
    public partial class PendingOutPassReport : Form,ToolStripAccess
    {
        public PendingOutPassReport()
        {
            InitializeComponent();
            GlobalVariables.HeaderName.Text = Class.Users.ScreenName; GlobalVariables.CurrentForm = this;
            butheader.BackColor = Class.Users.BackColors;
            this.BackColor = Class.Users.BackColors;
        }

      


        private static PendingOutPassReport _instance;
        public static PendingOutPassReport Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new PendingOutPassReport();
                GlobalVariables.CurrentForm = _instance; return _instance;
            }
        }
        public void ReadOnlys()
        {

        }
        Models.UserRights sm = new Models.UserRights(); Models.Master mas = new Models.Master();
        Report.PendingOutPassReport rd = new Report.PendingOutPassReport();
        
    


        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
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

     

        private void PendingOutPassReport_Load(object sender, EventArgs e)
        {

            this.txtsearch.Focus(); comboformate.SelectedIndex = 1;          
            frmdate.Value = DateTime.Now.AddDays(0); todate.Value = DateTime.Now.AddDays(0);
            hostelload(); COMBCODELOAD();
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
           
        }
        private void COMBCODELOAD()
        {
            try
            {
                //string sel3 = "SELECT DISTINCT 9 AS GTCOMPMASTID, '' AS COMPCODE  FROM DUAL A  UNION ALL  SELECT DISTINCT 10 AS GTCOMPMASTID, 'AGF' AS COMPCODE  FROM DUAL A  UNION ALL  SELECT DISTINCT 11 AS GTCOMPMASTID, 'FLF' AS COMPCODE  FROM DUAL A  UNION ALL   SELECT DISTINCT 12 AS GTCOMPMASTID, 'FLFD' AS COMPCODE  FROM DUAL A ORDER BY 1";
                //DataSet ds3 = Utility.ExecuteSelectQuery(sel3, "GTCOMPMAST");
                //DataTable dt3 = ds3.Tables["GTCOMPMAST"];

                Pinnacle.Models.Hostel hos = new Models.Hostel();
                DataTable dt3 = hos.HostelCompcode();
                if (dt3.Rows.Count > 0)
                {
                    combocompcode.DisplayMember = "COMPCODE";
                    combocompcode.ValueMember = "COMPCODE";
                    combocompcode.DataSource = dt3;


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Data Source Not Connected" + ex.Message);
            }
        }
        public void DownLoads()
        {
            if (comboformate.Text != "")
            {

                DialogResult result = MessageBox.Show("Do you want to '" + comboformate.Text + "' Formate ??", "" + comboformate.Text + "PRINT OUT", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (result.Equals(DialogResult.OK))
                {
                    if (combohostel.Text != "")
                    {
                        switch (comboformate.Text)
                        {
                            case "Word":
                                rd.ExportToDisk(ExportFormatType.WordForWindows, "d:\\'" + combohostel.Text + "'PendingOutPassReport.doc");
                                break;

                            case "Excel":
                                rd.ExportToDisk(ExportFormatType.ExcelWorkbook, "d:\\'" + combohostel.Text + "'PendingOutPassReport.xls");
                                break;

                            case "PDF":
                                rd.ExportToDisk(ExportFormatType.PortableDocFormat, "d:\\'" + combohostel.Text + "'PendingOutPassReport.pdf");
                                break;

                            case "CSV":
                                rd.ExportToDisk(ExportFormatType.CharacterSeparatedValues, "d:\\'" + combohostel.Text + "'PendingOutPassReport.csv");
                                break;
                        }
                    }
                    if (combocompcode.Text != "")
                    {
                        switch (comboformate.Text)
                        {
                            case "Word":
                                rd.ExportToDisk(ExportFormatType.WordForWindows, "d:\\'" + combocompcode.Text + "'PendingOutPassReport.doc");
                                break;

                            case "Excel":
                                rd.ExportToDisk(ExportFormatType.ExcelWorkbook, "d:\\'" + combocompcode.Text + "'PendingOutPassReport.xls");
                                break;

                            case "PDF":
                                rd.ExportToDisk(ExportFormatType.PortableDocFormat, "d:\\'" + combocompcode.Text + "'PendingOutPassReport.pdf");
                                break;

                            case "CSV":
                                rd.ExportToDisk(ExportFormatType.CharacterSeparatedValues, "d:\\'" + combocompcode.Text + "'PendingOutPassReport.csv");
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
                //if (Class.Users.HostelName == "WOMENS HOSTEL")
                //{
                //    string sel3 = "SELECT DISTINCT '' AS HOSTELNAME  FROM DUAL A UNION ALL select DISTINCT  A.HOSTELNAME from HOSTELLIVEDATA A  JOIN GTCOMPMAST B ON A.COMPCODE=B.COMPCODE  WHERE A.HOSTELNAME='" + Class.Users.HostelName + "' ORDER BY HOSTELNAME desc";
                //    DataSet ds3 = Utility.ExecuteSelectQuery(sel3, "HOSTELLIVEDATA");
                //    dt3 = ds3.Tables["HOSTELLIVEDATA"];
                //}
                //if (Class.Users.HostelName != "WOMENS HOSTEL")
                //{
                //    string sel3 = "SELECT DISTINCT '' AS HOSTELNAME  FROM DUAL A UNION ALL select DISTINCT  A.HOSTELNAME from HOSTELLIVEDATA A JOIN GTCOMPMAST B ON A.COMPCODE=B.COMPCODE WHERE A.HOSTELNAME NOT IN ('WOMENS HOSTEL')  AND B.COMPCODE='" + Class.Users.HostelName + "' ORDER BY HOSTELNAME desc";
                //    DataSet ds3 = Utility.ExecuteSelectQuery(sel3, "HOSTELLIVEDATA");
                //    dt3 = ds3.Tables["HOSTELLIVEDATA"];
                //}
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


        private void butView_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt2 = new DataTable();
                Class.Users.Intimation = "PAYROLL";
                if (combocompcode.Text =="" && combohostel.Text == "WOMENS HOSTEL" || combocompcode.Text == "" && combohostel.Text == "WORKING GENTS HOSTEL" || combocompcode.Text == "" && combohostel.Text == "GENTS STAFF HOSTEL")
                {
                    
                    string sel2 = "SELECT to_char(A.ASPTBLHOSTELGATEPASSID) as ASPTBLHOSTELGATEPASSID,B.COMPNAME2 AS COMPCODE,D.MIDCARD ,C.FNAME ,E.MNNAME1 AS DISPNAME,substr(A.SYSTEMDATE,1,10) AS CONTACTNO,F.HOSTELNAME,F.BLOCKFLOOR , F.ROOMNO,G.REASON,A.PERMISSIONHRS,A.INTIME,A.OUTTIME,A.OUTTIME, A.REMARKS   FROM ASPTBLHOSTELGATEPASS A  JOIN   GTCOMPMAST B ON B.GTCOMPMASTID = A.COMPCODE  JOIN   HREMPLOYMAST    C ON C.COMPCODE=B.GTCOMPMASTID AND C.COMPCODE=A.COMPCODE   JOIN HREMPLOYDETAILS D ON D.HREMPLOYMASTID=C.HREMPLOYMASTID AND D.IDCARD=C.IDCARDNO AND  D.DEPTNAME=A.DEPARTMENT AND D.MIDCARD=A.IDCARDNO    JOIN GTDEPTDESGMAST  E ON E.GTDEPTDESGMASTID = A.DEPARTMENT  JOIN HOSTELLIVEDATA F ON F.COMPCODE=B.COMPCODE AND F.IDCARDNO=C.IDCARDNO AND F.IDCARDNO=D.IDCARD   AND D.IDCARD=F.IDCARDNO JOIN ASPTBLREASONMAS G ON G.ASPTBLREASONMASID=A.REASON  JOIN  HRECONTACTDETAILS H ON H.HREMPLOYMASTID=C.HREMPLOYMASTID   WHERE NOT  E.mnname1='SECURITY' AND  A.HOSTELNAME='" + combohostel.Text + "' AND A.INTIME IS NULL and A.outtime IS not NULL  ORDER BY 1";

                    DataSet ds2 = Utility.ExecuteSelectQuery(sel2, "ASPTBLHOSTELGATEPASS");
                    dt2 = ds2.Tables["ASPTBLHOSTELGATEPASS"];
                    rd.SetDataSource(dt2);
                    crystalReportViewer1.ReportSource = null;
                    crystalReportViewer1.ReportSource = rd;

                    crystalReportViewer1.Refresh(); txtsearch.Text = "";
                }
                if (combocompcode.Text != "")
                {
                   
                    string sel2 = "SELECT to_char(A.ASPTBLHOSTELGATEPASSID) as ASPTBLHOSTELGATEPASSID,B.COMPNAME2 AS COMPCODE,D.MIDCARD ,C.FNAME ,E.MNNAME1 AS DISPNAME,substr(A.SYSTEMDATE,1,10) AS CONTACTNO,'"+combocompcode.Text+"' as HOSTELNAME,''as BLOCKFLOOR , '' as ROOMNO,G.REASON,A.PERMISSIONHRS,A.INTIME,A.OUTTIME,A.OUTTIME, A.REMARKS   FROM ASPTBLHOSTELGATEPASS A  JOIN   GTCOMPMAST B ON B.GTCOMPMASTID = A.COMPCODE  JOIN   HREMPLOYMAST    C ON C.COMPCODE=B.GTCOMPMASTID AND C.COMPCODE=A.COMPCODE    JOIN HREMPLOYDETAILS D ON D.HREMPLOYMASTID=C.HREMPLOYMASTID AND D.IDCARD=C.IDCARDNO AND  D.DEPTNAME=A.DEPARTMENT AND D.MIDCARD=A.IDCARDNO      JOIN GTDEPTDESGMAST  E ON E.GTDEPTDESGMASTID = A.DEPARTMENT    JOIN ASPTBLREASONMAS G ON G.ASPTBLREASONMASID=A.REASON    WHERE   A.HOSTELNAME='" + combocompcode.Text + "' AND A.INTIME IS NULL and A.outtime IS not NULL   ORDER BY 1";

                    DataSet ds2 = Utility.ExecuteSelectQuery(sel2, "ASPTBLHOSTELGATEPASS");
                    dt2 = ds2.Tables["ASPTBLHOSTELGATEPASS"];
                    rd.SetDataSource(dt2);
                    crystalReportViewer1.ReportSource = null;
                    crystalReportViewer1.ReportSource = rd;

                    crystalReportViewer1.Refresh(); txtsearch.Text = "";
                }
                if (dt2.Rows.Count < 0)
                {
                    crystalReportViewer1.ReportSource = null;
                    crystalReportViewer1.Refresh();
                }
            }
            catch (Exception EX)
            {
                crystalReportViewer1.ReportSource = null;
                crystalReportViewer1.Refresh();
            }
        }

        private void txtsearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtsearch.Text != "")
                {
                    DataTable dt2 = new DataTable();
                    if (combohostel.Text == "WOMENS HOSTEL" || combohostel.Text == "WORKING GENTS HOSTEL" || combohostel.Text == "GENTS STAFF HOSTEL")
                    {
                        string sel2 = "SELECT to_char(A.ASPTBLHOSTELGATEPASSID) as ASPTBLHOSTELGATEPASSID,B.COMPNAME2 AS COMPCODE,D.MIDCARD ,C.FNAME ,D.DEPT AS DISPNAME,substr(A.SYSTEMDATE,1,10) AS CONTACTNO,F.HOSTELNAME,F.BLOCKFLOOR , F.ROOMNO,G.REASON,A.PERMISSIONHRS,A.INTIME,A.OUTTIME,A.OUTTIME, A.REMARKS   FROM ASPTBLHOSTELGATEPASS A  JOIN   GTCOMPMAST B ON B.GTCOMPMASTID = A.COMPCODE  JOIN   HREMPLOYMAST    C ON C.COMPCODE=B.GTCOMPMASTID AND C.COMPCODE=A.COMPCODE   JOIN HREMPLOYDETAILS D ON D.HREMPLOYMASTID=C.HREMPLOYMASTID AND D.IDCARD=C.IDCARDNO AND  D.DEPTNAME=A.DEPARTMENT AND D.MIDCARD=A.IDCARDNO    JOIN GTDEPTDESGMAST  E ON E.GTDEPTDESGMASTID = A.DEPARTMENT  JOIN HOSTELLIVEDATA F ON F.COMPCODE=B.COMPCODE AND F.IDCARDNO=C.IDCARDNO AND F.IDCARDNO=D.IDCARD AND F.HOSTELNAME=A.HOSTELNAME  AND D.IDCARD=F.IDCARDNO JOIN ASPTBLREASONMAS G ON G.ASPTBLREASONMASID=A.REASON  JOIN  HRECONTACTDETAILS H ON H.HREMPLOYMASTID=C.HREMPLOYMASTID   WHERE   A.INTIME IS NULL AND A.MODIFIED= TO_DATE('" + frmdate.Value.ToString("dd-MM-yyyy") + "','dd-MM-yyyy') AND A.OUTTIME IS NOT NULL AND  D.MIDCARD LIKE'%" + txtsearch.Text + "%'  ORDER BY A.ASPTBLHOSTELGATEPASSID DESC";//AND A.INTIME IS NULL AND D.MIDCARD LIKE'%" + txthostelgatesearch.Text + "%' OR C.FNAME LIKE'%" + txthostelgatesearch.Text + "%' 
                        DataSet ds2 = Utility.ExecuteSelectQuery(sel2, "ASPTBLHOSTELGATEPASS");
                        dt2 = ds2.Tables["ASPTBLHOSTELGATEPASS"];
                    }
                    else
                    {
                        
                            string sel3 = "SELECT to_char(A.ASPTBLHOSTELGATEPASSID) as ASPTBLHOSTELGATEPASSID,B.COMPNAME2 AS COMPCODE,D.MIDCARD ,C.FNAME ,D.DEPT AS DISPNAME,substr(A.SYSTEMDATE,1,10) AS CONTACTNO,'" + Class.Users.HostelName + "' AS HOSTELNAME,'' AS BLOCKFLOOR , '' AS ROOMNO,G.REASON,A.PERMISSIONHRS,A.INTIME,A.OUTTIME,A.OUTTIME, A.REMARKS   FROM ASPTBLHOSTELGATEPASS A  JOIN   GTCOMPMAST B ON B.GTCOMPMASTID = A.COMPCODE  JOIN   HREMPLOYMAST    C ON C.COMPCODE=B.GTCOMPMASTID AND C.COMPCODE=A.COMPCODE   JOIN HREMPLOYDETAILS D ON D.HREMPLOYMASTID=C.HREMPLOYMASTID AND D.IDCARD=C.IDCARDNO AND  D.DEPTNAME=A.DEPARTMENT AND D.MIDCARD=A.IDCARDNO    JOIN GTDEPTDESGMAST  E ON E.GTDEPTDESGMASTID = A.DEPARTMENT   JOIN ASPTBLREASONMAS G ON G.ASPTBLREASONMASID=A.REASON  JOIN  HRECONTACTDETAILS H ON H.HREMPLOYMASTID=C.HREMPLOYMASTID where   A.HOSTELNAME='" + combohostel.Text + "' AND    A.INTIME IS NULL and A.outtime IS not NULL  AND A.MODIFIED= TO_DATE('" + frmdate.Value.ToString("dd-MM-yyyy") + "','dd-MM-yyyy') AND A.OUTTIME IS NOT NULL AND  D.MIDCARD LIKE'%" + txtsearch.Text + "%'   ORDER BY 1";
                            DataSet ds3 = Utility.ExecuteSelectQuery(sel3, "ASPTBLHOSTELGATEPASS");
                            dt2 = ds3.Tables["ASPTBLHOSTELGATEPASS"];
                        
                    }
                    rd.SetDataSource(dt2);
                    crystalReportViewer1.ReportSource = null;
                    crystalReportViewer1.ReportSource = rd;

                    crystalReportViewer1.Refresh(); txtsearch.Focus();
                }
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

        private void refreshToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                string sel2 = ""; DataTable dt2 = new DataTable();
                if (combohostel.Text == "WOMENS HOSTEL" || combohostel.Text == "WORKING GENTS HOSTEL" || combohostel.Text == "GENTS STAFF HOSTEL")
                {
                    sel2 = "SELECT to_char(A.ASPTBLHOSTELGATEPASSID) as ASPTBLHOSTELGATEPASSID,B.COMPNAME2 AS COMPCODE,'ALL' as DESIGNATION, D.MIDCARD ,C.FNAME ,D.DEPT AS DISPNAME, substr(A.SYSTEMDATE,1,10) AS CONTACTNO,F.HOSTELNAME , F.ROOMNO,G.REASON,A.PERMISSIONHRS,A.INTIME,A.OUTTIME,  A.REMARKS,A.TIMEDIFF as BLOCKFLOOR   FROM ASPTBLHOSTELGATEPASS A  JOIN   GTCOMPMAST B ON B.GTCOMPMASTID = A.COMPCODE  JOIN   HREMPLOYMAST    C ON C.COMPCODE=B.GTCOMPMASTID  JOIN HREMPLOYDETAILS D ON D.HREMPLOYMASTID=C.HREMPLOYMASTID AND D.IDCARD=C.IDCARDNO AND  D.DEPTNAME=A.DEPARTMENT AND D.MIDCARD=A.IDCARDNO     JOIN GTDEPTDESGMAST  E ON E.GTDEPTDESGMASTID = A.DEPARTMENT  JOIN HOSTELLIVEDATA F ON F.COMPCODE=B.COMPCODE AND F.IDCARDNO=C.IDCARDNO    AND F.IDCARDNO=D.IDCARD AND F.HOSTELNAME=A.HOSTELNAME  AND D.IDCARD=F.IDCARDNO JOIN ASPTBLREASONMAS G ON G.ASPTBLREASONMASID=A.REASON     JOIN  HRECONTACTDETAILS H ON H.HREMPLOYMASTID=C.HREMPLOYMASTID WHERE NOT E.mnname1='SECURITY'  AND A.INTIME IS  NULL AND A.OUTTIME IS  NULL AND A.HOSTELNAME='" + combohostel.Text + "' and  A.MODIFIED between TO_DATE('" + frmdate.Value.ToString("dd-MM-yyyy") + "','dd-MM-yyyy') and TO_DATE('" + todate.Value.ToString("dd-MM-yyyy") + "','dd-MM-yyyy') ORDER BY 1";
                    DataSet ds2 = Utility.ExecuteSelectQuery(sel2, "ASPTBLHOSTELGATEPASS");
                    dt2 = ds2.Tables["ASPTBLHOSTELGATEPASS"];
                }
                else
                {
                    sel2 = "SELECT to_char(A.ASPTBLHOSTELGATEPASSID) as ASPTBLHOSTELGATEPASSID,B.COMPNAME2 AS COMPCODE,D.MIDCARD ,C.FNAME ,D.DEPT AS DISPNAME,substr(A.SYSTEMDATE,1,10) AS CONTACTNO,'" + combocompcode.Text + "' AS HOSTELNAME,'' AS BLOCKFLOOR , '' AS ROOMNO,G.REASON,A.PERMISSIONHRS,A.INTIME,A.OUTTIME,A.OUTTIME, A.REMARKS   FROM ASPTBLHOSTELGATEPASS A  JOIN   GTCOMPMAST B ON B.GTCOMPMASTID = A.COMPCODE  JOIN   HREMPLOYMAST    C ON C.COMPCODE=B.GTCOMPMASTID AND C.COMPCODE=A.COMPCODE   JOIN HREMPLOYDETAILS D ON D.HREMPLOYMASTID=C.HREMPLOYMASTID AND D.IDCARD=C.IDCARDNO AND  D.DEPTNAME=A.DEPARTMENT AND D.MIDCARD=A.IDCARDNO    JOIN GTDEPTDESGMAST  E ON E.GTDEPTDESGMASTID = A.DEPARTMENT   JOIN ASPTBLREASONMAS G ON G.ASPTBLREASONMASID=A.REASON  JOIN  HRECONTACTDETAILS H ON H.HREMPLOYMASTID=C.HREMPLOYMASTID where   A.HOSTELNAME='" + combocompcode.Text + "' AND    A.INTIME IS NULL and A.outtime IS NOT  NULL  AND A.MODIFIED= TO_DATE('" + frmdate.Value.ToString("dd-MM-yyyy") + "','dd-MM-yyyy') ORDER BY 1";
                    DataSet ds3 = Utility.ExecuteSelectQuery(sel2, "ASPTBLHOSTELGATEPASS");
                    dt2 = ds3.Tables["ASPTBLHOSTELGATEPASS"];
                }
                rd.SetDataSource(dt2);
                crystalReportViewer1.ReportSource = null;
                crystalReportViewer1.ReportSource = rd;

                crystalReportViewer1.Refresh();txtsearch.Text = "";
            }
            catch (Exception EX)
            { MessageBox.Show(EX.Message); }
        }

        public void Prints()
        {
            try
            {

                if (printDialog1.ShowDialog() == DialogResult.OK)
                {
                    string sel2 = ""; DataTable dt2 = new DataTable();
                    if (combohostel.Text == "WOMENS HOSTEL" || combohostel.Text == "WORKING GENTS HOSTEL" || combohostel.Text == "GENTS STAFF HOSTEL")
                    {
                        sel2 = "SELECT to_char(A.ASPTBLHOSTELGATEPASSID) as ASPTBLHOSTELGATEPASSID,B.COMPNAME2 AS COMPCODE,D.MIDCARD ,C.FNAME ,D.DEPT AS DISPNAME,substr(A.SYSTEMDATE,1,10) AS CONTACTNO,F.HOSTELNAME,F.BLOCKFLOOR , F.ROOMNO,G.REASON,A.PERMISSIONHRS,A.INTIME,A.OUTTIME,A.OUTTIME, A.REMARKS   FROM ASPTBLHOSTELGATEPASS A  JOIN   GTCOMPMAST B ON B.GTCOMPMASTID = A.COMPCODE  JOIN   HREMPLOYMAST    C ON C.COMPCODE=B.GTCOMPMASTID AND C.COMPCODE=A.COMPCODE   JOIN HREMPLOYDETAILS D ON D.HREMPLOYMASTID=C.HREMPLOYMASTID AND D.IDCARD=C.IDCARDNO AND  D.DEPTNAME=A.DEPARTMENT AND D.MIDCARD=A.IDCARDNO    JOIN GTDEPTDESGMAST  E ON E.GTDEPTDESGMASTID = A.DEPARTMENT  JOIN HOSTELLIVEDATA F ON F.COMPCODE=B.COMPCODE AND F.IDCARDNO=C.IDCARDNO AND F.IDCARDNO=D.IDCARD AND F.HOSTELNAME=A.HOSTELNAME  AND D.IDCARD=F.IDCARDNO JOIN ASPTBLREASONMAS G ON G.ASPTBLREASONMASID=A.REASON  JOIN  HRECONTACTDETAILS H ON H.HREMPLOYMASTID=C.HREMPLOYMASTID   WHERE NOT  E.mnname1='SECURITY' AND   A.HOSTELNAME='" + combohostel.Text + "' AND    A.INTIME IS NULL and A.outtime IS not NULL  AND A.MODIFIED= TO_DATE('" + frmdate.Value.ToString("dd-MM-yyyy") + "','dd-MM-yyyy') ORDER BY 1";
                        DataSet ds2 = Utility.ExecuteSelectQuery(sel2, "ASPTBLHOSTELGATEPASS");
                        dt2 = ds2.Tables["ASPTBLHOSTELGATEPASS"];
                    }
                    else
                    {
                        sel2 = "SELECT to_char(A.ASPTBLHOSTELGATEPASSID) as ASPTBLHOSTELGATEPASSID,B.COMPNAME2 AS COMPCODE,D.MIDCARD ,C.FNAME ,D.DEPT AS DISPNAME,substr(A.SYSTEMDATE,1,10) AS CONTACTNO,'" + Class.Users.HostelName + "' AS HOSTELNAME,'' AS BLOCKFLOOR , '' AS ROOMNO,G.REASON,A.PERMISSIONHRS,A.INTIME,A.OUTTIME,A.OUTTIME, A.REMARKS   FROM ASPTBLHOSTELGATEPASS A  JOIN   GTCOMPMAST B ON B.GTCOMPMASTID = A.COMPCODE  JOIN   HREMPLOYMAST    C ON C.COMPCODE=B.GTCOMPMASTID AND C.COMPCODE=A.COMPCODE   JOIN HREMPLOYDETAILS D ON D.HREMPLOYMASTID=C.HREMPLOYMASTID AND D.IDCARD=C.IDCARDNO AND  D.DEPTNAME=A.DEPARTMENT AND D.MIDCARD=A.IDCARDNO    JOIN GTDEPTDESGMAST  E ON E.GTDEPTDESGMASTID = A.DEPARTMENT   JOIN ASPTBLREASONMAS G ON G.ASPTBLREASONMASID=A.REASON  JOIN  HRECONTACTDETAILS H ON H.HREMPLOYMASTID=C.HREMPLOYMASTID where   A.HOSTELNAME='" + combohostel.Text + "' AND    A.INTIME IS NULL and A.outtime IS not NULL  AND A.MODIFIED= TO_DATE('" + frmdate.Value.ToString("dd-MM-yyyy") + "','dd-MM-yyyy') ORDER BY 1";
                        DataSet ds3 = Utility.ExecuteSelectQuery(sel2, "ASPTBLHOSTELGATEPASS");
                        dt2 = ds3.Tables["ASPTBLHOSTELGATEPASS"];
                    }
                    CrystalDecisions.CrystalReports.Engine.ReportDocument reportdocument = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                    reportdocument.Load(Application.StartupPath + "\\Report\\PendingOutPassReport.rpt");
                    reportdocument.SetDataSource(dt2);
                    reportdocument.PrintOptions.PrinterName = printDialog1.PrinterSettings.PrinterName;
                    reportdocument.PrintToPrinter(printDialog1.PrinterSettings.Copies, printDialog1.PrinterSettings.Collate, printDialog1.PrinterSettings.FromPage, printDialog1.PrinterSettings.ToPage);

                }

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void News()
        {
            butheader.BackColor = Class.Users.BackColors;
            this.BackColor = Class.Users.BackColors;
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

        public void GridLoad()
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

        private void combohostel_SelectedIndexChanged(object sender, EventArgs e)
        {
            combocompcode.Text = "";
        }
    }
}
