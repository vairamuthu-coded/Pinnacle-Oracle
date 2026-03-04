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
    public partial class VehicleTableMatch : Form,ToolStripAccess
    {
        public VehicleTableMatch()
        {
            InitializeComponent();
          
            Class.Users.IPADDRESS = GenFun.GetLocalIPAddress();
            Class.Users.CREATED = Convert.ToDateTime(System.DateTime.Now.ToString("dd/MM/yyyy") + " " + System.DateTime.Now.ToLongTimeString());
            Class.Users.SysDate = Convert.ToString(System.DateTime.Now.ToString("dd/MM/yyyy"));
            Class.Users.SysTime = Convert.ToString(DateTime.Now.ToLongTimeString().ToString());
            GlobalVariables.HeaderName.Text = Class.Users.ScreenName; GlobalVariables.CurrentForm = this;
            butheader.BackColor = Class.Users.BackColors;          
            panel2.BackColor = Class.Users.BackColors;
            butfooter.BackColor = Class.Users.BackColors;
        }

      

        private static VehicleTableMatch _instance;
        Models.Master mas = new Models.Master();
        Models.UserRights sm = new Models.UserRights();
        byte[] bytes; private int rowIndex = 0;
        public static VehicleTableMatch Instance
        {
            get { if (_instance == null) _instance = new VehicleTableMatch(); GlobalVariables.CurrentForm = _instance; return _instance; }

        }

        Report.VehicleTableMatch rd = new Report.VehicleTableMatch();
  

   

        private void butView_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboformate.Text == "AUVIT")
                {
                    string sel2 = "SELECT DISTINCT  A.VEHICLENO,A.OWNERSHIPP AS  COMPCODE ,A.VEHICLENAME AS VEHICLENOS ,A.VEHINCHARGE AS FNAME FROM HRVEHMAST A JOIN GTCOMPMAST B ON A.COMPCODE=B.GTCOMPMASTID  join hrvehtypemast c on C.HRVEHTYPEMASTID=A.VEHICLETYPE  WHERE A.VCATEGORY='COMPANY' AND A.VEHINCHARGE IS NOT NULL AND A.ACTIVE='T' AND  C.VEHTYPE='TWO WHEELER'    ORDER BY 1";

                    DataSet ds2 = Utility.ExecuteSelectQuery(sel2, "ASPTBLVEHTOKEN");
                    DataTable dt2 = ds2.Tables["ASPTBLVEHTOKEN"];
                    rd.SetDataSource(dt2);
                    crystalReportViewer1.ReportSource = null;
                    crystalReportViewer1.ReportSource = rd;
                    crystalReportViewer1.Refresh();
                }
                else
                {
                    string sel2 = "SELECT DISTINCT e.COMPCODE, a.VEHICLENAME as VEHICLENOS,  a.VEHICLENO,d.FNAME  FROM ASPTBLVEHMAS A JOIN GTGENITEMMAST B ON A.FUELTYPE=B.GTGENITEMMASTID  JOIN HRVEHTYPEMAST C ON C.HRVEHTYPEMASTID=A.VEHICLETYPE  JOIN HREMPLOYMAST D ON  D.HREMPLOYMASTID=A.PARTYNAME  JOIN GTCOMPMAST E ON E.GTCOMPMASTID=D.COMPCODE     WHERE  B.FT='T' AND B.ITEMNAME='PETROL' AND C.VEHTYPE='TWO WHEELER'   AND A.VCATEGORY='PRIVATE'   AND E.COMPCODE='AGF' OR E.COMPCODE='AGFMGII' order by 3";
                    DataSet ds2 = Utility.ExecuteSelectQuery(sel2, "ASPTBLVEHTOKEN");
                    DataTable dt2 = ds2.Tables["ASPTBLVEHTOKEN"];

                    rd.SetDataSource(dt2);
                    crystalReportViewer1.ReportSource = null;
                    crystalReportViewer1.ReportSource = rd;

                    crystalReportViewer1.Refresh();
                }
            }
            catch (Exception EX)
            { MessageBox.Show(EX.Message); }
        }

        public void DownLoads()
        {
            if (comboformate.Text != "")
            {

                DialogResult result = MessageBox.Show("Do you want to '" + comboformate.Text + "' Formate ??", "" + comboformate.Text + "PRINT OUT", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (result.Equals(DialogResult.OK))
                {
                    // ExportFormatType formatType = ExportFormatType.NoFormat;                    
                    switch (comboformate.Text)
                    {


                        case "AUVIT":
                            // formatType = ExportFormatType.Excel;
                            rd.ExportToDisk(ExportFormatType.ExcelWorkbook, "d:\\'" + combocompcode.Text + "'AuvitTable.xls");
                            break;

                        case "DOTNET":
                            rd.ExportToDisk(ExportFormatType.ExcelWorkbook, "d:\\'" + combocompcode.Text + "'DotNetTable.xls");
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

        private void comboformate_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void Prints_Click(object sender, EventArgs e)
        {

        }

        public void News()
        {
            butheader.BackColor = Class.Users.BackColors;
            panel2.BackColor = Class.Users.BackColors;
            butfooter.BackColor = Class.Users.BackColors;
            DataTable dt = mas.comcode1();
            combocompcode.ValueMember = "GTCOMPMASTID";
            combocompcode.DisplayMember = "COMPCODE";
            combocompcode.DataSource = dt;

        
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

        public void ReadOnlys()
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
    }
}
