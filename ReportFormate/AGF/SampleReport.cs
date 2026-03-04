using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Windows.Forms;

namespace Pinnacle.ReportFormate.AGF
{
    public partial class SampleReport : Form,ToolStripAccess
    {
        private static SampleReport _instance;
       
        public static SampleReport Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new SampleReport();
                GlobalVariables.CurrentForm = _instance; return _instance;
            }
        }
        public SampleReport()
        {
            InitializeComponent();
        }

        public void News()
        {
            Class.Users.UserTime = 0;           
            this.BackColor = Class.Users.BackColors;
            this.Font = Class.Users.FontName;
            butheader.BackColor = Class.Users.BackColors;
            dataGridView1.Rows.Clear();
            Class.Users.TableName = "ASPTBLBUYSAMOUT";
            Class.Users.TableNameGrid = "ASPTBLBUYSAMOUT";
            GlobalVariables.HideCols = new string[] { "ASPTBLBUYSAMOUTID"};
            GlobalVariables.WidthCols = new Int32[] {0,70,100,100,230,250,200,50,80,100,100,50 };
            GlobalVariables.Query = "SELECT '', B.AGFSAMPLE,F.BRAND,B.STYLENAME,B.FABRIC ,B.FABRICCONTENT, B.COLORNAME,B.SIZENAME,B.MFYEAR ,A.CREATEDON as outdate,''RESONSEPERSON,''BRAND1    FROM ASPTBLBUYSAMOUT A JOIN ASPTBLBUYSAM B ON A.AGFSAMPLE=B.AGFSAMPLE  JOIN GTCOMPMAST C ON C.GTCOMPMASTID=A.COMPCODE    JOIN ASPTBLRACKMAS D ON D.ASPTBLRACKMASID=A.RACK   JOIN ASPTBLBINMAS E ON E.ASPTBLBINMASID=A.BIN  JOIN ASPTBLBRANDMAS F ON F.ASPTBLBRANDMASID=A.BRAND         JOIN ASPTBLSEASONMAS G ON G.ASPTBLSEASONMASID=A.SEASON   JOIN ASPTBLSAMTYPEMAS Q ON Q.ASPTBLSAMTYPEMASID=B.SAMPLETYPE   JOIN ASPTBLSAMDEPTMAS R ON R.ASPTBLSAMDEPTMASID=B.DEPARTMENT  JOIN ASPTBLSAMCATMAS S ON S.ASPTBLSAMCATMASID=B.CATEGORY  JOIN ASPTBLGGMAS T ON T.ASPTBLGGMASID=B.GAUGE     JOIN ASPTBLORDPACKMAS  U ON U.ASPTBLORDPACKMASID=B.ORDERPACKTYPE  JOIN ASPTBLCURMAS V ON V.ASPTBLCURMASID=B.CURRENCYNAME WHERE a.ASPTBLBUYSAMOUTID<0";
            CommonFunctions.AddGridColumn(dataGridView1, GlobalVariables.Query, GlobalVariables.HideCols, GlobalVariables.WidthCols);
            CommonFunctions.SetRowNumber(dataGridView1);
            BrandSearchLoad();combobuyer.Select();
        }
        public void GridLoad()
        {

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

        public void Searchs(int id)
        {
            
        }

        public void Deletes(int id)
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
            GlobalVariables.MdiPanel.Show();
            News();
            GlobalVariables.HeaderName.Text = "";
            GlobalVariables.TabCtrl.TabPages.RemoveAt(GlobalVariables.TabCtrl.SelectedIndex);
            this.Hide();
        }

        public void BrandSearchLoad()
        {
            string sel = " SELECT 0 AS ASPTBLBRANDMASID , '' AS BRAND FROM DUAL UNION ALL SELECT DISTINCT A.ASPTBLBRANDMASID,  A.BRAND    FROM  ASPTBLBRANDMAS A   join asptblbuysam b on A.ASPTBLBRANDMASID=B.BRAND  WHERE A.ACTIVE='T' ";
            DataSet ds = Utility.ExecuteSelectQuery(sel, "ASPTBLBRANDMAS");
            DataTable dt = ds.Tables["ASPTBLBRANDMAS"];

            combobuyer.DataSource = dt;
            combobuyer.DisplayMember = "BRAND";
            combobuyer.ValueMember = "ASPTBLBRANDMASID";

        }
        private void comboname_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboname.Text == "ALL")
            {

                string sel = "SELECT   A.AGFSAMPLE,F.BRAND,B.STYLENAME,B.FABRIC ,B.FABRICCONTENT,B.COLORNAME,B.SIZENAME,B.MFYEAR ,A.DATE2 AS OUTDATE,I.RESONSEPERSON,J.BRAND AS BRAND1 FROM ASPTBLBUYSAMOUT A JOIN ASPTBLBUYSAM B ON A.AGFSAMPLE=B.AGFSAMPLE   JOIN GTCOMPMAST C ON C.GTCOMPMASTID=A.COMPCODE    JOIN ASPTBLRACKMAS D ON D.ASPTBLRACKMASID=A.RACK   JOIN ASPTBLBINMAS E ON E.ASPTBLBINMASID=A.BIN JOIN ASPTBLBRANDMAS F ON F.ASPTBLBRANDMASID=A.BRAND         JOIN ASPTBLSEASONMAS G ON G.ASPTBLSEASONMASID=A.SEASON    JOIN ASPTBLSAMTYPEMAS Q ON Q.ASPTBLSAMTYPEMASID=B.SAMPLETYPE   join ASPTBLRESMAS i on I.ASPTBLRESMASID=A.RECEIVER    JOIN ASPTBLBRANDMAS J ON J.ASPTBLBRANDMASID=I.BRAND  WHERE ORDER BY A.DATE2,A.AGFSAMPLE DESC";

                DataSet ds = Utility.ExecuteSelectQuery(sel, "ASPTBLBUYSAMOUT");
                DataTable dt = ds.Tables["ASPTBLBUYSAMOUT"];
                if (dt.Rows.Count > 0)
                {
                    dataGridView1.Rows.Clear();
                    BindGrid(dataGridView1, dt);
                    CommonFunctions.SetRowNumber(dataGridView1);
                }
                else
                {
                    dataGridView1.Rows.Clear();
                }

                lbltotal.Refresh();
                lbltotal.Text = "Total " + dt.Rows.Count;
               
                // }
            }
            if (comboname.Text != "" && combobuyer.Text != "")
            {
                string sel = "SELECT   A.AGFSAMPLE,F.BRAND,B.STYLENAME,B.FABRIC ,B.FABRICCONTENT,B.COLORNAME,B.SIZENAME,B.MFYEAR ,A.DATE2 AS OUTDATE,I.RESONSEPERSON,J.BRAND AS BRAND1 FROM ASPTBLBUYSAMOUT A JOIN ASPTBLBUYSAM B ON A.AGFSAMPLE=B.AGFSAMPLE   JOIN GTCOMPMAST C ON C.GTCOMPMASTID=A.COMPCODE    JOIN ASPTBLRACKMAS D ON D.ASPTBLRACKMASID=A.RACK   JOIN ASPTBLBINMAS E ON E.ASPTBLBINMASID=A.BIN JOIN ASPTBLBRANDMAS F ON F.ASPTBLBRANDMASID=A.BRAND         JOIN ASPTBLSEASONMAS G ON G.ASPTBLSEASONMASID=A.SEASON    JOIN ASPTBLSAMTYPEMAS Q ON Q.ASPTBLSAMTYPEMASID=B.SAMPLETYPE   join ASPTBLRESMAS i on I.ASPTBLRESMASID=A.RECEIVER    JOIN ASPTBLBRANDMAS J ON J.ASPTBLBRANDMASID=I.BRAND  WHERE I.RESONSEPERSON='" + comboname.Text + "' and J.ASPTBLBRANDMASID='" + combobuyer.SelectedValue + "'    AND A.OUTWARD='T' AND B.INWARD IS NULL ORDER BY A.DATE2,A.AGFSAMPLE DESC";
                DataSet ds = Utility.ExecuteSelectQuery(sel, "ASPTBLBUYSAMOUT");
                DataTable dt = ds.Tables["ASPTBLBUYSAMOUT"];
                if (dt.Rows.Count > 0)
                {
                    dataGridView1.Rows.Clear();
                    BindGrid(dataGridView1, dt);
                    CommonFunctions.SetRowNumber(dataGridView1);
                }
                else
                {
                    dataGridView1.Rows.Clear();
       
                }
                label2.Refresh();
                label2.Text = "Total " + dt.Rows.Count;
                lbltotal.Refresh();
                lbltotal.Text = "Total " + dt.Rows.Count;
                

            }

        }
        int rowcount = 0;
        private void BindGrid1(DataGridView grid1, DataTable dt1)
        {
          
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                grid1.Rows.Add();
                rowcount = dataGridView2.Rows.Count - 1;
               grid1.Rows[rowcount].Cells[0].Value = dt1.Rows[i]["ASPTBLBUYSAMOUTID"].ToString();
                grid1.Rows[rowcount].Cells[1].Value = dt1.Rows[i]["BRAND"].ToString();
                grid1.Rows[rowcount].Cells[2].Value = dt1.Rows[i]["RECEIVER"].ToString();
                grid1.Rows[rowcount].Cells[3].Value = dt1.Rows[i]["COUNT"].ToString();
                grid1.Rows[rowcount].DefaultCellStyle.BackColor = rowcount % 2 == 0 ? Class.Users.Color1 : Class.Users.Color2;

            }

        }
        private void BindGrid(DataGridView grid, DataTable dt1)
        {
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                grid.Rows.Add();
                rowcount = dataGridView1.Rows.Count - 1;
                //grid.Rows[rowcount].Cells[0].Value = dt1.Rows[i]["ASPTBLBUYSAMOUTID"].ToString();
                grid.Rows[rowcount].Cells[1].Value = dt1.Rows[i]["AGFSAMPLE"].ToString();
                grid.Rows[rowcount].Cells[2].Value = dt1.Rows[i]["BRAND"].ToString();
                grid.Rows[rowcount].Cells[3].Value = dt1.Rows[i]["STYLENAME"].ToString();
                grid.Rows[rowcount].Cells[4].Value = dt1.Rows[i]["FABRIC"].ToString();
                grid.Rows[rowcount].Cells[5].Value = dt1.Rows[i]["FABRICCONTENT"].ToString();
                grid.Rows[rowcount].Cells[6].Value = dt1.Rows[i]["COLORNAME"].ToString();
                grid.Rows[rowcount].Cells[7].Value = dt1.Rows[i]["SIZENAME"].ToString();               
                grid.Rows[rowcount].Cells[8].Value = dt1.Rows[i]["MFYEAR"].ToString();
                grid.Rows[rowcount].Cells[9].Value = dt1.Rows[i]["OUTDATE"].ToString();
                grid.Rows[rowcount].Cells[10].Value = dt1.Rows[i]["RESONSEPERSON"].ToString();
                grid.Rows[rowcount].Cells[11].Value = dt1.Rows[i]["BRAND1"].ToString();
                //bytes = (byte[])dt1.Rows[0]["GARMENTIMAGE"];
                //Image img = Models.Device.ByteArrayToImage(bytes);
                //imageList1.Images.Add(img);
                //grid.Rows[rowcount].Cells[10].Value = imageList1;



                grid.Rows[rowcount].DefaultCellStyle.BackColor = rowcount % 2 == 0 ? Class.Users.Color1 : Class.Users.Color2;

            }
           
        }
        private void combodate_SelectedIndexChanged(object sender, EventArgs e)
        {
          
        }

        private void SampleReport_Load(object sender, EventArgs e)
        {
            //Class.Users.SearchQuery = "select '' RECEIVER from dual union all select 'ALL' RECEIVER from dual union all select distinct c.RESONSEPERSON as RECEIVER from ASPTBLBUYSAMOUT a  JOIN ASPTBLBUYSAM B ON A.AGFSAMPLE=B.AGFSAMPLE join asptblresmas c on C.ASPTBLRESMASID=A.RECEIVER WHERE A.OUTWARD='T' AND B.INWARD IS NULL";
            //    Class.Users.ds = Utility.ExecuteSelectQuery(Class.Users.SearchQuery, "ASPTBLBUYSAMOUT");
            //    Class.Users.dt = Class.Users.ds.Tables["ASPTBLBUYSAMOUT"];
            //    if (Class.Users.dt.Rows.Count > 0)
            //    {
            //        comboname.DisplayMember = "RECEIVER";
            //        comboname.ValueMember = "RECEIVER";
            //        comboname.DataSource = Class.Users.dt;
            //    }
            
        }

        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl2.SelectedTab == tabControl2.TabPages["tabPage3"])//your specific tabname
            {
                GlobalVariables.WidthCols = null; GlobalVariables.Query = null; GlobalVariables.HideCols = null;
                GlobalVariables.HideCols = new string[] { "ASPTBLBUYSAMOUTID" };
                GlobalVariables.WidthCols = new Int32[] { 0, 200, 100,100 };
                GlobalVariables.Query = "  select '' ASPTBLBUYSAMOUTID,'' BRAND,C.RESONSEPERSON AS RECEIVER ,'' COUNT from ASPTBLBUYSAMOUT A    JOIN ASPTBLRESMAS C ON C.ASPTBLRESMASID=A.RECEIVER  where A.ASPTBLBUYSAMOUTID<=0";
                CommonFunctions.AddGridColumn(dataGridView2, GlobalVariables.Query, GlobalVariables.HideCols, GlobalVariables.WidthCols);
                if (dataGridView2.Columns.Count > 0)
                {
                    string sel = "SELECT    '0' ASPTBLBUYSAMOUTID,J.BRAND,  I.RESONSEPERSON AS RECEIVER , sum(A.pcs) count  FROM ASPTBLBUYSAMOUT A JOIN ASPTBLBUYSAM B ON A.AGFSAMPLE=B.AGFSAMPLE   JOIN GTCOMPMAST C ON C.GTCOMPMASTID=A.COMPCODE    JOIN ASPTBLRACKMAS D ON D.ASPTBLRACKMASID=A.RACK   JOIN ASPTBLBINMAS E ON E.ASPTBLBINMASID=A.BIN JOIN ASPTBLBRANDMAS F ON F.ASPTBLBRANDMASID=A.BRAND         JOIN ASPTBLSEASONMAS G ON G.ASPTBLSEASONMASID=A.SEASON     JOIN ASPTBLSAMTYPEMAS Q ON Q.ASPTBLSAMTYPEMASID=B.SAMPLETYPE   join ASPTBLRESMAS i on I.ASPTBLRESMASID=A.RECEIVER    JOIN ASPTBLBRANDMAS J ON J.ASPTBLBRANDMASID=i.BRAND  WHERE B.INWARD IS NULL  group by J.BRAND, I.RESONSEPERSON ORDER BY  J.BRAND ";
                    DataSet ds = Utility.ExecuteSelectQuery(sel, "ASPTBLBUYSAMOUT");
                    DataTable dtT = ds.Tables["ASPTBLBUYSAMOUT"];
                    if (dtT.Rows.Count > 0)
                    {
                        dataGridView2.Rows.Clear();
                        dataGridView2.DataSource = null;
                        BindGrid1(dataGridView2, dtT);
                        //dataGridView2.DataSource = null;
                        //dataGridView2.DataSource = dt;
                    }
                    else
                    {
                        dataGridView2.Rows.Clear();
                    }

                    string sel0 = "SELECT  sum(A.pcs) count  FROM ASPTBLBUYSAMOUT A JOIN ASPTBLBUYSAM B ON A.AGFSAMPLE=B.AGFSAMPLE   JOIN GTCOMPMAST C ON C.GTCOMPMASTID=A.COMPCODE    JOIN ASPTBLRACKMAS D ON D.ASPTBLRACKMASID=A.RACK   JOIN ASPTBLBINMAS E ON E.ASPTBLBINMASID=A.BIN JOIN ASPTBLBRANDMAS F ON F.ASPTBLBRANDMASID=A.BRAND         JOIN ASPTBLSEASONMAS G ON G.ASPTBLSEASONMASID=A.SEASON     JOIN ASPTBLSAMTYPEMAS Q ON Q.ASPTBLSAMTYPEMASID=B.SAMPLETYPE   join ASPTBLRESMAS i on I.ASPTBLRESMASID=A.RECEIVER    JOIN ASPTBLBRANDMAS J ON J.ASPTBLBRANDMASID=i.BRAND  WHERE B.INWARD IS NULL  "; 
                    DataSet ds0 = Utility.ExecuteSelectQuery(sel0, "ASPTBLBUYSAMOUT");
                    DataTable dtT0 = ds0.Tables["ASPTBLBUYSAMOUT"];
                    lbltotal.Refresh();
                    lbltotal.Text = "Total " + dtT0.Rows[0]["count"].ToString();
                    label2.Refresh();
                    label2.Text = "Total " + dtT0.Rows[0]["count"].ToString();
                    CommonFunctions.SetRowNumber(dataGridView2);
                }
            }
            else
            {
                lbltotal.Refresh();
                lbltotal.Text = "Total " + dataGridView1.Rows.Count;
            }
            if (tabControl2.SelectedTab == tabControl2.TabPages["tabPage4"])//your specific tabname
            {
                //dataGridView4.Rows.Clear();
                //string sel0 = "SELECT A.DATE1,B.COMPCODE,b.compname,C.BRAND,A.AGFSAMPLE,E.SEASON || ' /' ||  A.MFYEAR || '' AS SEASON,F.DEPARTMENT || '(' ||  O.CATEGORY || ')' AS DEPARTMENT ,O.CATEGORY,A.STYLENAME,A.SUBSTYLE,A.FABRIC ,A.FABRICCONTENT,A.COUNTS, K.GG AS GAUGE,A.GSM, A.COLORNAME,P.ORDERPACKTYPE,A.SIZENAME,N.CURRENCYNAME,A.REMARKS,A.ACTIVE,A.RISK1,A.RISK2,A.RISK3,A.RISK4,A.RISK5,A.FABRICCOMPLIANT,A.REMARKS,A.MFYEAR    FROM ASPTBLBUYSAM A JOIN GTCOMPMAST B ON A.COMPCODE=B.GTCOMPMASTID   join asptblBRANDMAS C ON C.ASPTBLBRANDMASID=A.BRAND  JOIN ASPTBLSEASONMAS E ON E.ASPTBLSEASONMASID=A.SEASON   JOIN ASPTBLSAMDEPTMAS F ON F.ASPTBLSAMDEPTMASID=A.DEPARTMENT   JOIN ASPTBLGGMAS K ON K.ASPTBLGGMASID=A.GAUGE         JOIN ASPTBLCURMAS N ON N.ASPTBLCURMASID=A.CURRENCYNAME JOIN ASPTBLSAMCATMAS O ON O.ASPTBLSAMCATMASID=A.CATEGORY      JOIN ASPTBLORDPACKMAS  P ON P.ASPTBLORDPACKMASID=A.ORDERPACKTYPE   JOIN ASPTBLBUYSAMINW Q ON  Q.AGFSAMPLE=A.AGFSAMPLE     WHERE   Q.INWARD='T' AND  Q.OUTWARD='T'   ORDER by a.AGFSAMPLE ASC";
                //DataSet ds0 = Utility.ExecuteSelectQuery(sel0, "ASPTBLBUYSAM");
                //DataTable dtT = ds0.Tables["ASPTBLBUYSAM"];

                //if (dtT.Rows.Count > 0)
                //{
                //    dataGridView4.DataSource = dtT;


                //}
            }
        }
        OpenFileDialog open = new OpenFileDialog();
        byte[] bytes;
        int cnt = 1;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            cnt++;
            int id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[1].Value);
            Class.Users.Paramid = id;
            string sel0 = "SELECT GARMENTIMAGE   FROM ASPTBLBUYSAM A JOIN GTCOMPMAST B ON A.COMPCODE=B.GTCOMPMASTID join asptblBRANDMAS C ON C.ASPTBLBRANDMASID=A.BRAND  JOIN ASPTBLSEASONMAS E ON E.ASPTBLSEASONMASID=A.SEASON JOIN ASPTBLSAMDEPTMAS F ON F.ASPTBLSAMDEPTMASID=A.DEPARTMENT  JOIN ASPTBLGGMAS K ON K.ASPTBLGGMASID=A.GAUGE    JOIN ASPTBLCURMAS N ON N.ASPTBLCURMASID=A.CURRENCYNAME JOIN ASPTBLSAMCATMAS O ON O.ASPTBLSAMCATMASID=A.CATEGORY  JOIN ASPTBLORDPACKMAS  P ON P.ASPTBLORDPACKMASID=A.ORDERPACKTYPE join ASPTBLSAMTYPEMAS Q ON Q.ASPTBLSAMTYPEMASID=A.SAMPLETYPE where A.AGFSAMPLE=" + Convert.ToInt64("0" + id);
            DataSet ds0 = Utility.ExecuteSelectQuery(sel0, "ASPTBLBUYSAM");
            DataTable dt0 = ds0.Tables["ASPTBLBUYSAM"];
            if (dt0 != null)
            {
                bytes = (byte[])dt0.Rows[0]["GARMENTIMAGE"];
                Image img = Models.Device.ByteArrayToImage(bytes);
                picturegarmentimage.Image = img;
                if (checkBox1.Checked == true)
                {
                    ReportFormate.AGF.PopUp pop = new PopUp();
                    pop.FormClosed += Pop_FormClosed;
                    pop.Show();
                }

            }
        }

        private void Pop_FormClosed(object sender, FormClosedEventArgs e)
        {
           
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Class.Users.SearchQuery = "select '' RECEIVER from dual union all select 'ALL' RECEIVER from dual union all select distinct A.RECEIVER from ASPTBLBUYSAMOUT a ";
            Class.Users.ds = Utility.ExecuteSelectQuery(Class.Users.SearchQuery, "ASPTBLBUYSAMOUT");
            Class.Users.dt = Class.Users.ds.Tables["ASPTBLBUYSAMOUT"];
            if (Class.Users.dt.Rows.Count > 0)
            {
                comboname.DisplayMember = "RECEIVER";
                comboname.ValueMember = "RECEIVER";
                comboname.DataSource = Class.Users.dt;
            }
        }

  
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void downLoadToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (dataGridView1.Rows.Count > 0)
            {
                Cursor.Current = Cursors.WaitCursor;
                Microsoft.Office.Interop.Excel.Application xcelapp = new Microsoft.Office.Interop.Excel.Application();
                xcelapp.Application.Workbooks.Add(Type.Missing);

                for (int i = 1; i < dataGridView1.Columns.Count + 1; i++)
                {
                    xcelapp.Cells[1, i] = dataGridView1.Columns[i - 1].HeaderText;

                }
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {
                        xcelapp.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[j].Value;

                    }
                }

                xcelapp.Columns.AutoFit();
                xcelapp.Visible = true;
                Cursor.Current = Cursors.Default;
            }
        }

        private void downloadToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (dataGridView2.Rows.Count > 0)
            {
                Cursor.Current = Cursors.WaitCursor;
                Microsoft.Office.Interop.Excel.Application xcelapp = new Microsoft.Office.Interop.Excel.Application();
                xcelapp.Application.Workbooks.Add(Type.Missing);

                for (int i = 1; i < dataGridView2.Columns.Count + 1; i++)
                {
                    xcelapp.Cells[1, i] = dataGridView2.Columns[i - 1].HeaderText;

                }
                for (int i = 0; i < dataGridView2.Rows.Count; i++)
                {
                    for (int j = 0; j < dataGridView2.Columns.Count; j++)
                    {
                        xcelapp.Cells[i + 2, j + 1] = dataGridView2.Rows[i].Cells[j].Value;

                    }
                }

                xcelapp.Columns.AutoFit();
                xcelapp.Visible = true;
                Cursor.Current = Cursors.Default;
            }
        }

        private  void combobuyer_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(combobuyer.SelectedValue) > 0)
                {
                    string sel = "select   A.RESONSEPERSON, A.ASPTBLRESMASID,B.BRAND  from ASPTBLRESMAS a join asptblbrandmas b  on A.BRANd=B.ASPTBLBRANDMASID  WHERE A.ACTIVE='T' and  B.BRAND='" + combobuyer.Text + "' order by 1 ";
                    DataSet ds = Utility.ExecuteSelectQuery(sel, "ASPTBLBRANDMAS");
                    DataTable dt =  ds.Tables["ASPTBLBRANDMAS"];

                    if (dt.Rows.Count>0)
                    {
                        comboname.DisplayMember = "RESONSEPERSON";
                        comboname.ValueMember = "ASPTBLRESMASID";
                        comboname.DataSource = dt;
                    }
                }
                
            }
            catch(Exception ex) { }
        }

        private void butdatewise_Click(object sender, EventArgs e)
        {

            string sel = "SELECT A.ASPTBLBUYSAMOUTID,A.AGFSAMPLE,F.BRAND,B.STYLENAME,B.FABRIC ,B.FABRICCONTENT,B.COLORNAME,B.SIZENAME,B.MFYEAR ,A.DATE2 AS OUTDATE,I.RESONSEPERSON,J.BRAND AS BRAND1 FROM ASPTBLBUYSAMOUT A JOIN ASPTBLBUYSAM B ON A.AGFSAMPLE=B.AGFSAMPLE   JOIN GTCOMPMAST C ON C.GTCOMPMASTID=A.COMPCODE    JOIN ASPTBLRACKMAS D ON D.ASPTBLRACKMASID=A.RACK   JOIN ASPTBLBINMAS E ON E.ASPTBLBINMASID=A.BIN JOIN ASPTBLBRANDMAS F ON F.ASPTBLBRANDMASID=A.BRAND         JOIN ASPTBLSEASONMAS G ON G.ASPTBLSEASONMASID=A.SEASON    JOIN ASPTBLSAMTYPEMAS Q ON Q.ASPTBLSAMTYPEMASID=B.SAMPLETYPE   join ASPTBLRESMAS i on I.ASPTBLRESMASID=A.RECEIVER    JOIN ASPTBLBRANDMAS J ON J.ASPTBLBRANDMASID=I.BRAND  WHERE I.RESONSEPERSON='" + comboname.Text + "' and J.ASPTBLBRANDMASID='" + combobuyer.SelectedValue + "'  and   a.date2 between TO_DATE('" + dateTimePicker1.Value.ToString("dd-MM-yyyy") + "','dd-MM-yyyy') and TO_DATE('" + dateTimePicker2.Value.ToString("dd-MM-yyyy") + "','dd-MM-yyyy')  AND A.OUTWARD='T' AND B.INWARD IS NULL ORDER BY A.OUTDATE DESC";
            DataSet ds = Utility.ExecuteSelectQuery(sel, "ASPTBLBUYSAMOUT");
            DataTable dt = ds.Tables["ASPTBLBUYSAMOUT"];
            if (dt.Rows.Count > 0)
            {
                dataGridView1.Rows.Clear();
                BindGrid(dataGridView1, dt);

            }
            else
            {
                dataGridView1.Rows.Clear();
            }

            label2.Refresh();
            label2.Text = "Total " + dt.Rows.Count;
            lbltotal.Refresh();
            lbltotal.Text = "Total " + dt.Rows.Count;
            CommonFunctions.SetRowNumber(dataGridView1);
        }

        private void comboname_SelectedValueChanged(object sender, EventArgs e)
        {


            //if (Convert.ToInt32(comboname.SelectedValue) >0)
            //{
            //    //  string sel = "SELECT DISTINCT B.AGFSAMPLE,B.STYLENAME,B.FABRIC ,B.FABRICCONTENT, B.COLORNAME,B.SIZENAME,B.MFYEAR ,A.CREATEDON as OUTDATE    FROM ASPTBLBUYSAMOUT A JOIN ASPTBLBUYSAM B ON A.AGFSAMPLE=B.AGFSAMPLE  JOIN GTCOMPMAST C ON C.GTCOMPMASTID=A.COMPCODE    JOIN ASPTBLRACKMAS D ON D.ASPTBLRACKMASID=A.RACK   JOIN ASPTBLBINMAS E ON E.ASPTBLBINMASID=A.BIN  JOIN ASPTBLBRANDMAS F ON F.ASPTBLBRANDMASID=A.BRAND         JOIN ASPTBLSEASONMAS G ON G.ASPTBLSEASONMASID=A.SEASON   JOIN ASPTBLSAMTYPEMAS Q ON Q.ASPTBLSAMTYPEMASID=B.SAMPLETYPE   JOIN ASPTBLSAMDEPTMAS R ON R.ASPTBLSAMDEPTMASID=B.DEPARTMENT  JOIN ASPTBLSAMCATMAS S ON S.ASPTBLSAMCATMASID=B.CATEGORY  JOIN ASPTBLGGMAS T ON T.ASPTBLGGMASID=B.GAUGE     JOIN ASPTBLORDPACKMAS  U ON U.ASPTBLORDPACKMASID=B.ORDERPACKTYPE  JOIN ASPTBLCURMAS V ON V.ASPTBLCURMASID=B.CURRENCYNAME where  A.OUTWARD='T'  ";
            //   // string sel = "SELECT   A.AGFSAMPLE,B.STYLENAME,B.FABRIC ,B.FABRICCONTENT, B.COLORNAME,B.SIZENAME,B.MFYEAR ,A.DATE2 as OUTDATE    FROM ASPTBLBUYSAMOUT A JOIN ASPTBLBUYSAM B ON A.AGFSAMPLE=B.AGFSAMPLE  JOIN GTCOMPMAST C ON C.GTCOMPMASTID=A.COMPCODE    JOIN ASPTBLRESMAS U ON U.ASPTBLRESMASID=A.RECEIVER   join asptblbuysaminw d on D.agfsample=A.agfsample AND D.ASPTBLBUYSAMINWID=A.ASPTBLBUYSAMINWID AND  A.ASPTBLBUYSAMID=B.ASPTBLBUYSAMID   WHERE A.OUTWARD='T'";
            //    string sel = "SELECT   A.AGFSAMPLE,B.STYLENAME,B.FABRIC ,B.FABRICCONTENT, B.COLORNAME,B.SIZENAME,B.MFYEAR ,A.DATE2 as OUTDATE    FROM ASPTBLBUYSAMOUT A JOIN ASPTBLBUYSAM B ON A.AGFSAMPLE=B.AGFSAMPLE  JOIN GTCOMPMAST C ON C.GTCOMPMASTID=A.COMPCODE    JOIN ASPTBLRESMAS U ON U.ASPTBLRESMASID=A.RECEIVER   join asptblbuysaminw d on D.agfsample=A.agfsample AND D.ASPTBLBUYSAMINWID=A.ASPTBLBUYSAMINWID AND  A.ASPTBLBUYSAMID=B.ASPTBLBUYSAMID   WHERE  A.OUTWARD='T' AND B.INWARD IS NULL ORDER BY A.DATE2 DESC";
            //    DataSet ds = Utility.ExecuteSelectQuery(sel, "ASPTBLBUYSAMOUT");
            //    DataTable dt = ds.Tables["ASPTBLBUYSAMOUT"];
            //    if (dt.Rows.Count > 0)
            //    {
            //        dataGridView1.Rows.Clear();
            //        BindGrid(dataGridView1, dt);

            //    }
            //    label2.Refresh();
            //    label2.Text = "Total " + dt.Rows.Count;
         
            //    CommonFunctions.SetRowNumber(dataGridView1);
            //}
           
               
           
        }

        private void dOWNLOADToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (dataGridView4.Rows.Count > 0)
            {
                Cursor.Current = Cursors.WaitCursor;
                Microsoft.Office.Interop.Excel.Application xcelapp = new Microsoft.Office.Interop.Excel.Application();
                xcelapp.Application.Workbooks.Add(Type.Missing);

                for (int i = 1; i < dataGridView4.Columns.Count + 1; i++)
                {
                    xcelapp.Cells[1, i] = dataGridView4.Columns[i - 1].HeaderText;

                }
                for (int i = 0; i < dataGridView4.Rows.Count; i++)
                {
                    for (int j = 0; j < dataGridView4.Columns.Count; j++)
                    {
                        xcelapp.Cells[i + 2, j + 1] = dataGridView4.Rows[i].Cells[j].Value;

                    }
                }

                xcelapp.Columns.AutoFit();
                xcelapp.Visible = true;
                Cursor.Current = Cursors.Default;
            }
        }
    }
}
