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
namespace Pinnacle.Transactions
{
    public partial class CompanyWiseSecurityInventry : Form,ToolStripAccess
    {
        private static CompanyWiseSecurityInventry _instance;
        Models.Master mas = new Models.Master();
        Models.UserRights sm = new Models.UserRights();
        public CompanyWiseSecurityInventry()
        {
            InitializeComponent();
            GlobalVariables.HeaderName.Text = Class.Users.ScreenName; GlobalVariables.CurrentForm = this;
            Class.Users.UserTime = 0;
            butheader.BackColor = Class.Users.BackColors;     
            butfooter.BackColor = Class.Users.BackColors;
            mas.DatabaseCheck(checkdatabase);
        }

        public static CompanyWiseSecurityInventry Instance
        {
            get
            {
                if (_instance == null) _instance = new CompanyWiseSecurityInventry();
                GlobalVariables.CurrentForm = _instance;
                return _instance;
            }

        }



       
        string ss = System.DateTime.Now.ToShortDateString();
        public void ReadOnlys()
        {

        }
        private void CompanyWiseSecurityInventry_Load(object sender, EventArgs e)
        {
            DataTable dt1 = new DataTable();
            if (Class.Users.HUserName == "VAIRAM") { 
                dt1 = mas.comcode1();
            }
            else
            {
                dt1 = mas.findcomcode(Class.Users.HCompcode);

            }
            frmdate.Value = System.DateTime.Now.AddDays(0);
            todate.Value = System.DateTime.Now.AddDays(0); GlobalVariables.HeaderName.Text = Class.Users.ScreenName;
            if (dt1.Rows.Count > 0)
            {
                combocompcode.DisplayMember = "COMPCODE";
                combocompcode.ValueMember = "GTCOMPMASTID";
                combocompcode.DataSource = dt1;

            }

            this.txtsearch.Focus(); 
        }

      
     
    

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void Combocompcode_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
      
        private void Butview_Click(object sender, EventArgs e)
        {
            Report.SecurityCrystalReport rd = new Report.SecurityCrystalReport();
            try
            {
                Class.Users.UserTime = 0;Class.Users.Intimation = "";
                lblcount.Refresh(); lblInward.Refresh(); lbloutward.Refresh();
                string sel2 = " SELECT TO_CHAR(A.gatedcno) AS INVENTRYID ,B.COMPCODE,A.QRCODE,A.CATEGORY,C.GATENAME ,A.SYSTEMDATE, A.SYSTEMTIME, b.compname as USERNAME FROM ASPINVENTRY A JOIN GTCOMPMAST B ON A.COMPCODE = B.GTCOMPMASTID JOIN asptblusermas C ON C.USERID = A.USERNAME WHERE B.COMPCODE ='" + combocompcode.Text + "'   AND SUBSTR(A.MODIFIED,1,10)  between TO_DATE('" + frmdate.Value.ToString("dd-MM-yyyy") + "','DD-MM-YYYY') AND TO_DATE('" + todate.Value.ToString("dd-MM-yyyy") + "','DD-MM-YYYY')  ORDER BY a.INVENTRYID DESC";
                DataSet ds2 = Utility.ExecuteSelectQuery(sel2, "ASPINVENTRY");
                DataTable dt2 = ds2.Tables["ASPINVENTRY"];
                string sel3 = "SELECT count( A.CATEGORY) category  FROM ASPINVENTRY A JOIN GTCOMPMAST B ON A.COMPCODE = B.GTCOMPMASTID JOIN asptblusermas C ON C.USERID = A.USERNAME WHERE B.COMPCODE ='" + combocompcode.Text + "' AND SUBSTR(A.MODIFIED,1,10)  between TO_DATE('" + frmdate.Value.ToString("dd-MM-yyyy") + "','DD-MM-YYYY') AND TO_DATE('" + todate.Value.ToString("dd-MM-yyyy") + "','DD-MM-YYYY')  AND  A.CATEGORY='IN' ORDER BY 1";
                DataSet ds3 = Utility.ExecuteSelectQuery(sel3, "ASPINVENTRY");
                DataTable dt3 = ds3.Tables["ASPINVENTRY"];

                string sel4 = " SELECT count( A.CATEGORY) category  FROM ASPINVENTRY A JOIN GTCOMPMAST B ON A.COMPCODE = B.GTCOMPMASTID JOIN asptblusermas C ON C.USERID = A.USERNAME WHERE B.COMPCODE ='" + combocompcode.Text + "' AND SUBSTR(A.MODIFIED,1,10)  between TO_DATE('" + frmdate.Value.ToString("dd-MM-yyyy") + "','DD-MM-YYYY') AND TO_DATE('" + todate.Value.ToString("dd-MM-yyyy") + "','DD-MM-YYYY')  AND  A.CATEGORY='OUT' ORDER BY 1";
                DataSet ds4 = Utility.ExecuteSelectQuery(sel4, "ASPINVENTRY");
                DataTable dt4 = ds4.Tables["ASPINVENTRY"];
              DataTable  dtCC = Utility.SQLQuery("SELECT LOGO AS EMPIMAGE FROM EDOCIMAGE WHERE IMGNAME = 'COMPLOGO' AND COMPANYID ='" + Class.Users.HCompcode + "' ");



                if (dt2.Rows.Count > 0)
                {
                    rd.Database.Tables["DataTableCompwise"].SetDataSource(dt2);
                    if (dtCC.Rows.Count > 0)
                    {
                        rd.Database.Tables["DataTable1"].SetDataSource(dtCC);
                    }
                    
                    
               

                    crystalReportViewer1.ReportSource = rd;
                    crystalReportViewer1.Refresh(); lblcount.Text = "";                  
                    lblcount.Text = "Total Count  : " + dt2.Rows.Count.ToString(); 
                    lblInward.Text = "Inward Entry Count  : " +dt3.Rows[0]["category"].ToString();
                    lbloutward.Text = "Outward Entry Count : " + dt4.Rows[0]["category"].ToString();
                }
                else
                {
                    crystalReportViewer1.ReportSource = null;
                    crystalReportViewer1.Refresh(); lblcount.Text = "Total Count  : "; lblInward.Text = "Inward Entry Count  :"; lbloutward.Text = "Outward Entry Count  :";
                }
            }
            catch (Exception EX)
            { MessageBox.Show(EX.Message); }
        }



        private void Txtsearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Report.SecurityCrystalReport rd = new Report.SecurityCrystalReport();
                if (txtsearch.Text.Length > 1)
                {
                    string sel2 = " SELECT TO_CHAR( A.gatedcno) as INVENTRYID,B.COMPCODE,A.QRCODE,A.CATEGORY,C.GATENAME,A.SYSTEMDATE, A.SYSTEMTIME, b.compname as USERNAME FROM ASPINVENTRY A JOIN GTCOMPMAST B ON A.COMPCODE = B.GTCOMPMASTID JOIN asptblusermas C ON C.USERID = A.USERNAME WHERE B.COMPCODE ='" + combocompcode.Text + "' AND A.QRCODE like'%" + txtsearch.Text + "%' ORDER BY a.INVENTRYID desc";
                    DataSet ds2 = Utility.ExecuteSelectQuery(sel2, "ASPINVENTRY");
                    DataTable dt2 = ds2.Tables["ASPINVENTRY"];

                    rd.SetDataSource(dt2);
                    crystalReportViewer1.ReportSource = null;
                    crystalReportViewer1.ReportSource = rd;
                    crystalReportViewer1.Refresh(); 
                    this.txtsearch.Focus();
                }
            }
            catch (Exception EX)
            { MessageBox.Show(EX.Message); }
        }



        public void Prints()
        {
            DialogResult result = MessageBox.Show("Do you want to Print ??", "Pass", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (result.Equals(DialogResult.OK))
            {

                crystalReportViewer1.PrintReport();

            }
            else
            {

            }
        }


        private void CrystalReportViewer1_Load(object sender, EventArgs e)
        {

        }

        public void Pdfs()
        {
            Report.SecurityCrystalReport rd = new Report.SecurityCrystalReport();
            DialogResult result = MessageBox.Show("Do you want to PDF Formate ??", "PRINT OUT", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (result.Equals(DialogResult.OK))
            {
                string sel2 = " SELECT TO_CHAR( A.INVENTRYID)INVENTRYID ,B.COMPCODE,A.QRCODE,A.CATEGORY,C.GATENAME,A.SYSTEMDATE, A.SYSTEMTIME, b.compname as USERNAME FROM ASPINVENTRY A JOIN GTCOMPMAST B ON A.COMPCODE = B.GTCOMPMASTID JOIN asptblusermas C ON C.USERID = A.USERNAME WHERE B.COMPCODE ='" + combocompcode.Text + "' AND SUBSTR(A.MODIFIED,1,10)  between TO_DATE('" + frmdate.Value.ToString("dd-MM-yyyy") + "','DD-MM-YYYY') AND TO_DATE('" + todate.Value.ToString("dd-MM-yyyy") + "','DD-MM-YYYY')  ORDER BY 1";
                DataSet ds2 = Utility.ExecuteSelectQuery(sel2, "ASPINVENTRY");
                DataTable dt2 = ds2.Tables["ASPINVENTRY"];
                if (dt2.Rows.Count > 0)
                {

                    rd.SetDataSource(dt2);

                    crystalReportViewer1.ReportSource = rd;
                    crystalReportViewer1.Refresh(); lblcount.Text = "";
                    lblcount.Text = "Total Count  : " + dt2.Rows.Count.ToString();
                    rd.ExportToDisk(ExportFormatType.PortableDocFormat, "d:\\'" + System.DateTime.Now.ToString("dd-MM-yyyy") + "  " + combocompcode.Text + "'SecurityGateInOutDetails.pdf");
                }
            }
            else
            {

            }
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public void DownLoads()
        {
            if (comboformate.Text != "")
            {
                Report.SecurityCrystalReport rd = new Report.SecurityCrystalReport();
                DialogResult result = MessageBox.Show("Do you want to '" + comboformate.Text + "' Formate ??", "" + comboformate.Text + "PRINT OUT", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (result.Equals(DialogResult.OK))
                {
                    switch (comboformate.Text)
                    {
                        case "Word":
                         //   rd.ExportToDisk(ExportFormatType.WordForWindows, "c:\\'" + combocompcode.Text + "'CustomerReport1.doc");
                            rd.ExportToDisk(ExportFormatType.WordForWindows, "d:\\'" + System.DateTime.Now.ToString("dd-MM-yyyy") + "  " + combocompcode.Text + "'SecurityGateInOutDetails.doc");

                            break;

                        case "Excel":
                           // rd.ExportToDisk(ExportFormatType.Excel, "c:\\'" + combocompcode.Text + "'CustomerReport.xls");
                            rd.ExportToDisk(ExportFormatType.ExcelWorkbook, "d:\\'" + System.DateTime.Now.ToString("dd-MM-yyyy") + "  " + combocompcode.Text + "'SecurityGateInOutDetails.xls");

                            break;

                        case "PDF":
                          //  rd.ExportToDisk(ExportFormatType.PortableDocFormat, "c:\\'" + combocompcode.Text + "'CustomerReport1.pdf");
                            rd.ExportToDisk(ExportFormatType.PortableDocFormat, "d:\\'" + System.DateTime.Now.ToString("dd-MM-yyyy") + "  " + combocompcode.Text + "'SecurityGateInOutDetails.pdf");
                            break;

                        case "CSV":
                           // rd.ExportToDisk(ExportFormatType.CharacterSeparatedValues, "c:\\'" + combocompcode.Text + "'CustomerReport.csv");
                            rd.ExportToDisk(ExportFormatType.CharacterSeparatedValues, "d:\\'" + System.DateTime.Now.ToString("dd-MM-yyyy") + "  " + combocompcode.Text + "'SecurityGateInOutDetails.pdf");

                            break;
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

        private void Lblattsearch_Click(object sender, EventArgs e)
        {

        }

        private void Todate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void Label10_Click(object sender, EventArgs e)
        {

        }

        private void Frmdate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void Label12_Click(object sender, EventArgs e)
        {

        }

        private void ContextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void RefreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void ToolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void Saves_Click(object sender, EventArgs e)
        {

        }

        private void Lbl_Header_Click(object sender, EventArgs e)
        {

        }

        private void CrystalReportViewer1_Load_1(object sender, EventArgs e)
        {

        }

        private void News_Click(object sender, EventArgs e)
        {

        }

        public void News()
        {
            butheader.BackColor = Class.Users.BackColors;           
       
            butfooter.BackColor = Class.Users.BackColors;
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
            GlobalVariables.MdiPanel.Show();
            News();
            GlobalVariables.HeaderName.Text = "";
            GlobalVariables.TabCtrl.TabPages.RemoveAt(GlobalVariables.TabCtrl.SelectedIndex);
            this.Hide();
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

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            try
            {
                Class.Users.UserTime = 0; Class.Users.Intimation = "";
                lblcount.Text = "Total Count  : "; lblInward.Text = "Inward Entry Count  :"; lbloutward.Text = "Outward Entry Count  :";
               
                string sel2 = " SELECT TO_CHAR(A.gatedcno) AS INVENTRYID ,B.COMPCODE,A.QRCODE,A.CATEGORY,C.GATENAME ,A.SYSTEMDATE, A.SYSTEMTIME, b.compname as USERNAME FROM ASPINVENTRY A JOIN GTCOMPMAST B ON A.COMPCODE = B.GTCOMPMASTID JOIN asptblusermas C ON C.USERID = A.USERNAME WHERE B.COMPCODE ='" + combocompcode.Text + "'   AND A.CATEGORY ='" + comboBox1.Text + "'  AND SUBSTR(A.MODIFIED,1,10)  between TO_DATE('" + frmdate.Value.ToString("dd-MM-yyyy") + "','DD-MM-YYYY') AND TO_DATE('" + todate.Value.ToString("dd-MM-yyyy") + "','DD-MM-YYYY')  ORDER BY a.INVENTRYID DESC";
                DataSet ds2 = Utility.ExecuteSelectQuery(sel2, "ASPINVENTRY");
                DataTable dt2 = ds2.Tables["ASPINVENTRY"];
                Report.SecurityCrystalReport rd1 = new Report.SecurityCrystalReport();

                string sel3 = "SELECT COUNT(A.CATEGORY)CNT  FROM  ASPINVENTRY A JOIN GTCOMPMAST B ON A.COMPCODE = B.GTCOMPMASTID JOIN asptblusermas C ON C.USERID = A.USERNAME WHERE B.COMPCODE ='" + combocompcode.Text + "' AND SUBSTR(A.MODIFIED,1,10)  between TO_DATE('" + frmdate.Value.ToString("dd-MM-yyyy") + "','DD-MM-YYYY') AND TO_DATE('" + todate.Value.ToString("dd-MM-yyyy") + "','DD-MM-YYYY')  AND  A.CATEGORY='IN' ORDER BY 1";
                DataSet ds3 = Utility.ExecuteSelectQuery(sel3, "ASPINVENTRY");
                DataTable dt3 = ds3.Tables["ASPINVENTRY"];

                string sel4 = " SELECT COUNT(A.CATEGORY)CNT  FROM ASPINVENTRY A JOIN GTCOMPMAST B ON A.COMPCODE = B.GTCOMPMASTID JOIN asptblusermas C ON C.USERID = A.USERNAME WHERE B.COMPCODE ='" + combocompcode.Text + "' AND SUBSTR(A.MODIFIED,1,10)  between TO_DATE('" + frmdate.Value.ToString("dd-MM-yyyy") + "','DD-MM-YYYY') AND TO_DATE('" + todate.Value.ToString("dd-MM-yyyy") + "','DD-MM-YYYY')  AND  A.CATEGORY='OUT' ORDER BY 1";
                DataSet ds4 = Utility.ExecuteSelectQuery(sel4, "ASPINVENTRY");
                DataTable dt4 = ds4.Tables["ASPINVENTRY"];
                DataTable dtCC = new DataTable();
                dtCC = Utility.SQLQuery("SELECT LOGO AS EMPIMAGE FROM EDOCIMAGE WHERE IMGNAME = 'COMPLOGO' AND COMPANYID ='" + Class.Users.HCompcode + "' ");
               
               

                if (dt2.Rows.Count > 0)
                {
                    rd1.Database.Tables["DataTableCompwise"].SetDataSource(dt2);
                    if (dtCC.Rows.Count > 0)
                    {
                        rd1.Database.Tables["DataTable1"].SetDataSource(dtCC);
                    }
                    
                    crystalReportViewer1.ReportSource = null;
                    crystalReportViewer1.ReportSource = rd1;
                    crystalReportViewer1.Refresh(); 
                    lblcount.Text = "Total Count  : " + dt2.Rows.Count.ToString();
                    lblInward.Text = "Inward Entry Count  : " + dt3.Rows[0]["CNT"].ToString();
                    lbloutward.Text = "Outward Entry Count : " + dt4.Rows[0]["CNT"].ToString();
                }
                else
                {
                    crystalReportViewer1.ReportSource = null;
                    crystalReportViewer1.Refresh(); lblcount.Text = "Total Count  : "; lblInward.Text = "Inward Entry Count  :"; lbloutward.Text = "Outward Entry Count  :";
                }


            }
            catch (Exception EX)
            { MessageBox.Show(EX.Message); }
        }

        private void checkdatabase_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
