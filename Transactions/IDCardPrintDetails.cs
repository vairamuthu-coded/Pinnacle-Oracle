using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace Pinnacle.Transactions
{
    public partial class IDCardPrintDetails : Form,ToolStripAccess
    {
        public IDCardPrintDetails()
        {
            InitializeComponent();
            GlobalVariables.HeaderName.Text = Class.Users.ScreenName;
            GlobalVariables.HeaderName.Text = Class.Users.ScreenName; GlobalVariables.CurrentForm = this;
           // tabControl1.TabPages.Remove(tabPage3);
        }

   
        Models.Master mas = new Models.Master();
        Models.UserRights sm = new Models.UserRights();
        private static IDCardPrintDetails _instance; string idcardcount = ""; Byte[] bytes;
        ListView listfilter = new ListView();
        ListView listfilter1 = new ListView();
        DataTable dtgeneral = new DataTable();       
        ListView allip = new ListView();
        DataTable reversedDt; DataTable reversedDt1;
        DataTable reversedDt2;

        public void ReadOnlys()
        {

        }
        public static IDCardPrintDetails Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new IDCardPrintDetails();
                GlobalVariables.CurrentForm = _instance; return _instance;
            }
        }

        public void MdiPaenl()
        {
        }

            Report.IDCard.FrontCrystalReport rd1 = new Report.IDCard.FrontCrystalReport();
        Report.IDCard.BackCrystalReport rd2 = new Report.IDCard.BackCrystalReport();
        Report.IDCard.IDCardFrontBackCrystalReport rd3 = new Report.IDCard.IDCardFrontBackCrystalReport();
        Report.IDCard.ReverseCrystalReport rd4 = new Report.IDCard.ReverseCrystalReport();
     
      public void News() { 
            // companyload(); GridLoad();
            crystalReportViewer1.ReportSource = null; flowLayoutPanel1.Controls.Clear();
            crystalReportViewer1.Refresh(); crystalReportViewer2.ReportSource = null;
            crystalReportViewer2.Refresh();
            dtgeneral.Rows.Clear(); idcardcount = ""; chkall.Checked = false; checkword.Checked = false; reversedDt = null;
            reversedDt1 = null;
            reversedDt2 = null;
            foreach (ListViewItem item in listView2.Items)
            {
                item.Checked = false;
            }
        }

       
        public void companyload()
        {
            try
            {
                string sel = "SELECT 0  GTCOMPMASTID,'---------' COMPCODE  FROM DUAL  UNION ALL SELECT  distinct   B.GTCOMPMASTID,B.COMPCODE  from hremploymast a  join  gtcompmast b on a.compcode=b.gtcompmastid   order by 2";// where B.compcode='" + Class.Users.HCompcode + "' order by 2";
                DataSet ds = Utility.ExecuteSelectQuery(sel, "gtcompmast");
                DataTable dt = ds.Tables["gtcompmast"];
                combocompcode.DisplayMember = "COMPCODE";
                combocompcode.ValueMember = "GTCOMPMASTID";
                combocompcode.DataSource = dt;

            }
            catch (Exception ex)
            {
                MessageBox.Show("companyload: " + ex.Message, " Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void IDCardPrintDetails_Load(object sender, EventArgs e)
        {
            companyload(); 
        }

        private void combocompcode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt64(combocompcode.SelectedValue) > 0)
                {
                    Cursor = Cursors.WaitCursor;
                    string sel1 = "select  distinct to_Date(a.DOCDATE,'dd-MM-YY') as  docdate from hremploymast a  join  gtcompmast b on a.compcode=b.gtcompmastid JOIN ASPTBLEMP D ON D.COMPCODE=B.GTCOMPMASTID AND D.COMPCODE=A.COMPCODE  where b.compcode='" + combocompcode.Text + "' order by 1";
                    DataSet ds1 = Utility.ExecuteSelectQuery(sel1, "hremploymast");
                    DataTable dt1 = ds1.Tables["hremploymast"];
                    comboPayPeriod1.DisplayMember = "docdate";
                    comboPayPeriod1.ValueMember = "docdate";
                    comboPayPeriod1.DataSource = dt1;
                    Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void comboPayPeriod1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt64(combocompcode.SelectedValue) > 0 && comboPayPeriod1.Text != "")
                {
                    lbltotal2.Text = "";
                    listView2.Items.Clear(); listfilter.Items.Clear();
                    txtsearch1.Text = ""; Cursor = Cursors.WaitCursor; checkword.Checked = false; chkall.Checked = false;
                    string sel1 = "SELECT  A.HREMPLOYMASTID,A.IDCARDNO  ,C.MIDCARD,C.MIDCARD  as midcard1, A.FNAME AS EMPNAME, B.compname,A.DOCDATE,B.ADDRESS  FROM  HREMPLOYMAST A JOIN GTCOMPMAST B ON A.COMPCODE=B.GTCOMPMASTID    JOIN HREMPLOYDETAILS C ON A.HREMPLOYMASTID=C.HREMPLOYMASTID  JOIN ASPTBLEMP D ON D.COMPCODE=B.GTCOMPMASTID AND D.COMPCODE=A.COMPCODE AND D.IDCARDNO=C.MIDCARD AND D.EMPID=A.HREMPLOYMASTID where  b.compcode='" + combocompcode.Text + "' and  a.docdate=to_date('" + comboPayPeriod1.Text + "','dd-MM-yyyy') order by 3";

                    DataSet ds = Utility.ExecuteSelectQuery(sel1, "hremploymast");
                    DataTable dt = ds.Tables["hremploymast"];
                    if (dt.Rows.Count > 0)
                    {
                        int j = 1;

                        foreach (DataRow myRow in dt.Rows)
                        {
                            ListViewItem list1 = new ListViewItem();
                            list1.SubItems.Add(j.ToString());
                            list1.SubItems.Add(myRow["hremploymastid"].ToString());
                            list1.SubItems.Add(myRow["idcardno"].ToString());
                            list1.SubItems.Add(myRow["midcard"].ToString());
                            list1.SubItems.Add(myRow["empname"].ToString());
                            list1.SubItems.Add(myRow["compname"].ToString());
                            list1.SubItems.Add(myRow["docdate"].ToString());
                            list1.SubItems.Add(myRow["Address"].ToString());
                            this.listfilter.Items.Add((ListViewItem)list1.Clone());                           
                            if (j % 2 == 0)
                            {
                                list1.BackColor = Color.WhiteSmoke;
                            }
                            else
                            {
                                list1.BackColor = Color.White;
                            }
                            listView2.Items.Add(list1);
                            j++;
                        }

                        lbltotal2.Text = "" + comboPayPeriod1.Text + " Total Record's : " + listView2.Items.Count;
                        //crystalReportViewer1.ReportSource = null; crystalReportViewer1.Refresh();
                    }

                    Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                // MessageBox.Show("Error: " + ex.Message, " Error ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        int i = 0; int j = 0;

        private void listView2_ItemActivate(object sender, EventArgs e)
        {   }

       


        private void butback_Click(object sender, EventArgs e)
        {

            if (dtgeneral.Rows.Count > 0)
            {
                var orderedRows = from row in dtgeneral.AsEnumerable().Reverse() select row;
                reversedDt = orderedRows.CopyToDataTable();
                if (reversedDt.Rows.Count > 0)
                {
                    reversedDt1.Rows.Clear();
                    reversedDt2.Rows.Clear();


                    cnt = 0;
                    foreach (DataRow dr in reversedDt.Rows)
                    {

                        if (cnt < 6)
                        {

                            reversedDt1.Rows.Add(dr.ItemArray);
                            reversedDt1.AcceptChanges();

                        }
                        cnt++;
                    }
                    cnt = 0;
                    foreach (DataRow dr1 in dtgeneral.Rows)
                    {

                        if (cnt < 6)
                        {



                            reversedDt2.Rows.Add(dr1.ItemArray);
                            reversedDt2.AcceptChanges();


                        }
                        cnt++;
                    }

                    if (reversedDt2.Rows.Count <= 6)
                    {
                        crystalReportViewer1.ReportSource = null;
                        CrystalDecisions.CrystalReports.Engine.ReportDocument reportdocument = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                        reportdocument.Load(Application.StartupPath + "\\Report\\IDCard\\ReverseCrystalReport.rpt");
                        reportdocument.Database.Tables["DataTable1"].SetDataSource(reversedDt1);
                        reportdocument.Database.Tables["DataTable2"].SetDataSource(reversedDt2);
                        crystalReportViewer1.ReportSource = reportdocument;
                        crystalReportViewer1.Refresh();
                         reportdocument.PrintOptions.PrinterName = printDialog1.PrinterSettings.PrinterName;
                         reportdocument.PrintToPrinter(printDialog1.PrinterSettings.Copies, printDialog1.PrinterSettings.Collate, printDialog1.PrinterSettings.FromPage, printDialog1.PrinterSettings.ToPage);

                    }
                }
            }
        }

        int cnt = 0;
        private void butfront_Click(object sender, EventArgs e)
        {


            if (dtgeneral.Rows.Count > 0)
            {
                var orderedRows = from row in dtgeneral.AsEnumerable().Reverse() select row;
                reversedDt = orderedRows.CopyToDataTable();
                if (reversedDt.Rows.Count > 0)
                {
                    reversedDt2.Rows.Clear();
                    reversedDt1.Rows.Clear();
                }
              
                cnt = 0;

                foreach (DataRow dr in dtgeneral.Rows)
                {

                    if (cnt < 6)
                    {

                        reversedDt1.Rows.Add(dr.ItemArray);
                        reversedDt1.AcceptChanges();

                    }
                    cnt++;
                }
                cnt = 0;
                foreach (DataRow dr1 in reversedDt.Rows)
                {

                    if (cnt < 6)
                    {



                        reversedDt2.Rows.Add(dr1.ItemArray);
                        reversedDt2.AcceptChanges();


                    }
                    cnt++;
                }

                if (reversedDt2.Rows.Count <= 6)
                {
                    crystalReportViewer1.ReportSource = null;
                    CrystalDecisions.CrystalReports.Engine.ReportDocument reportdocument = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                    reportdocument.Load(Application.StartupPath + "\\Report\\IDCard\\ReverseCrystalReport.rpt");
                    reportdocument.Database.Tables["DataTable1"].SetDataSource(reversedDt1);
                    reportdocument.Database.Tables["DataTable2"].SetDataSource(reversedDt2);
                    crystalReportViewer1.ReportSource = reportdocument;
                    crystalReportViewer1.Refresh();
                 //  reportdocument.PrintOptions.PrinterName = printDialog1.PrinterSettings.PrinterName;
                //  reportdocument.PrintToPrinter(printDialog1.PrinterSettings.Copies, printDialog1.PrinterSettings.Collate, printDialog1.PrinterSettings.FromPage, printDialog1.PrinterSettings.ToPage);

                }
            }
        }

        public void Prints()
        {
            DialogResult result = MessageBox.Show("Do You want to Print IDCard??", "Print", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (result.Equals(DialogResult.OK))
            {
                CrystalDecisions.CrystalReports.Engine.ReportDocument reportdocument = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                reportdocument.Load(Application.StartupPath + "\\Report\\IDCard\\ReverseCrystalReport.rpt");
                reportdocument.SetDataSource(dtgeneral);
                reportdocument.PrintOptions.PrinterName = printDialog1.PrinterSettings.PrinterName;
                reportdocument.PrintToPrinter(printDialog1.PrinterSettings.Copies, printDialog1.PrinterSettings.Collate, printDialog1.PrinterSettings.FromPage, printDialog1.PrinterSettings.ToPage);
                News();
            }
            else
            {

            }
        }


        private void listView2_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            try
            {
                i = 0;
                Cursor = Cursors.WaitCursor;
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


                    this.listfilter1.Items.Add((ListViewItem)it2.Clone());
                    //-----------------------------------------------------------------------------------
                    try
                    {

                        string sel0 = "SELECT   A.HREMPLOYMASTID,A.IDCARDNO  ,C.MIDCARD,C.MIDCARD as midcard1, A.FNAME ,A.DOCDATE,B.COMPNAME,B.ADDRESS,E.CADD AS permanentaddress,D.EMPIMAGE,A.FATHERNAME as father,to_char(a.dob,'dd-MM-yyyy') as dateofbirth,A.EMPTYPE AS NATUREOFEMPLOYEEMENT,A.BG as BLOODGROUP, to_char(C.DOJ,'dd-MM-yyyy') as dateofissue FROM  HREMPLOYMAST A JOIN GTCOMPMAST B ON A.COMPCODE=B.GTCOMPMASTID  JOIN HREMPLOYDETAILS C ON A.HREMPLOYMASTID=C.HREMPLOYMASTID  JOIN ASPTBLEMP D ON D.COMPCODE=B.GTCOMPMASTID AND D.COMPCODE=A.COMPCODE  AND D.IDCARDNO=C.MIDCARD AND D.EMPID=A.HREMPLOYMASTID JOIN HRECONTACTDETAILS E ON   E.HREMPLOYMASTID=A.HREMPLOYMASTID AND E.HREMPLOYMASTID=C.HREMPLOYMASTID where  c.MIDCARD='" + e.Item.SubItems[4].Text + "' and b.compcode='" + combocompcode.Text + "' and  a.docdate=to_date('" + e.Item.SubItems[7].Text.Substring(0, 10) + "','dd-MM-YY') order by 3";
                        DataSet ds0 = Utility.ExecuteSelectQuery(sel0, "hremploymast");
                        DataTable dt = ds0.Tables["hremploymast"];
                        FrontIDCardUserControl[] items = new FrontIDCardUserControl[dt.Rows.Count];

                        if (dtgeneral.Rows.Count == 0)
                        {
                            dtgeneral = dt.Clone();
                            reversedDt1 = dt.Clone();
                            reversedDt2 = dt.Clone();
                        }
                        foreach (DataRow row in dt.Rows)
                        {
                            dtgeneral.Rows.Add(row.ItemArray);
                        }


                        foreach (DataRow myRow in dt.Rows)
                        {
                            items[i] = new FrontIDCardUserControl();
                            items[i].compname.Text = Convert.ToString(myRow["COMPNAME"].ToString());
                            items[i].address.Text = Convert.ToString(myRow["ADDRESS"].ToString());

                            if (myRow["EMPIMAGE"].ToString() != "")
                            {

                                byte[] bytes = (byte[])myRow["EMPIMAGE"];
                                Image img = Models.Device.ByteArrayToImage(bytes);
                                items[i].empimage = img;
                            }
                            items[i].empname.Text = "I)  Name :" + Convert.ToString(myRow["FNAME"].ToString());
                            items[i].midcard.Text = "II) IDCardNo :" + Convert.ToString(myRow["MIDCARD"].ToString());
                            flowLayoutPanel1.Controls.Add(items[i]);
                            idcardcount += Convert.ToString(myRow["MIDCARD"].ToString()) + ",";
                            items[i].CloseButton.Text = Convert.ToString(myRow["MIDCARD"].ToString());
                            items[i].CloseButton.Click += CloseButton_Click;
                        }

                    }
                    catch (Exception ex)
                    {
                        Cursor = Cursors.Default;
                    }

                    //------------------------------------------------------------------------------------
                }
                if (e.Item.Checked == false && e.Item.SubItems[0].Text == "T")
                {
                    idcardcount = "";
                    e.Item.SubItems[0].Text = "F"; e.Item.BackColor = System.Drawing.SystemColors.ControlLightLight;
                    e.Item.ForeColor = Color.Black;
                    for (int c = 0; c < allip.Items.Count; c++)
                    {
                        DataRow dr = dtgeneral.Rows[i];
                        if (e.Item.SubItems[3].Text == allip.Items[c].SubItems[3].Text)
                        {


                            dr.Delete();
                            dtgeneral.AcceptChanges();


                            allip.Items[c].Remove();
                            e.Item.SubItems[0].Text = "";

                            c--;

                            Cursor = Cursors.Default;

                        }
                        idcardcount += Convert.ToString(allip.Items[c].SubItems[3].Text) + ",";

                    }

                }


            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                //   MessageBox.Show("       ---  " + e.Item.ToString() + "             ======================" + ex.ToString());

            }
            Cursor = Cursors.Default;

        }
        string[] s;
        
        private void CloseButton_Click(object sender, EventArgs e)
        {
             s = sender.ToString().Split(',');
            for (int c = 0; c < dtgeneral.Rows.Count; c++)
            {
                DataRow dr = dtgeneral.Rows[c];
                s = sender.ToString().Split(',');

                if (dr.ItemArray[2].ToString() == s[1].Substring(7))
                {
                    dr.Delete();
                   
                    dtgeneral.AcceptChanges(); flowLayoutPanel1.Controls.Remove((sender as Button).Parent);

                }
                //if (reversedDt.Rows.Count > 0)
                //{
                //    dr.Delete();
                //    reversedDt.AcceptChanges();
                //}
            }
            if (flowLayoutPanel1.Controls.Count < 1)
            {
                // News_Click(sender, e);
                News();
            }
            lbltotal2.Text = "Total count" + dtgeneral.Rows.Count;
        }
        //private void CloseButton1_Click(object sender, EventArgs e)
        //{


        //    for (int c = 0; c < dtgeneral.Rows.Count; c++)
        //    {
        //        DataRow dr = dtgeneral.Rows[c];
        //        s = sender.ToString().Split(',');
              
        //        if (dr.ItemArray[2].ToString() == s[1].Substring(7))
        //        {
        //            dr.Delete();
        //            dtgeneral.AcceptChanges(); flowLayoutPanel1.Controls.Remove((sender as Button).Parent);

        //        }
        //    }
        //    if (flowLayoutPanel1.Controls.Count < 1)
        //    {
        //        News_Click(sender, e);
        //    }
        //    lbltotal2.Text = "Total count" + dtgeneral.Rows.Count;
        //}

        private void chkall_CheckedChanged(object sender, EventArgs e)
        {
            try
            {


                if (chkall.Checked == true)
                {
                    Cursor = Cursors.WaitCursor;
                    int listCount = listView2.Items.Count;

                    foreach (ListViewItem item in listView2.Items)
                    {
                        item.Checked = true;
                    }
                    Cursor = Cursors.Default;

                }
                else
                {

                    News();

                    Cursor = Cursors.Default;

                }
                Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }
        }

        public void Pdfs()
        {
            DataTable reversedDt = new DataTable();
            var orderedRows = from row in dtgeneral.AsEnumerable().Reverse() select row;
            reversedDt = orderedRows.CopyToDataTable();
            if (reversedDt.Rows.Count > 0)
            {
                rd4.Database.Tables["DataTable1"].SetDataSource(dtgeneral);
                rd4.Database.Tables["DataTable2"].SetDataSource(reversedDt);
                crystalReportViewer1.ReportSource = null;
                crystalReportViewer1.Refresh();
                crystalReportViewer1.ReportSource = rd4;

            }

            //DialogResult result = MessageBox.Show("Do You want to Download??", "DownLoad", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            //if (result.Equals(DialogResult.OK))
            //{
            //    if (dtgeneral.Rows.Count > 0)
            //    {
            //        idcardcount = "";
            //        Cursor = Cursors.WaitCursor;
            //        rd3.SetDataSource(dtgeneral);
            //        crystalReportViewer1.ReportSource = rd3;
            //        rd2.SetDataSource(dtgeneral);
            //        crystalReportViewer2.ReportSource = rd2;
            //        for (i = 0; i < dtgeneral.Rows.Count; i++)
            //        {
            //            idcardcount += dtgeneral.Rows[i]["midcard"].ToString() + ",";
            //        }
            //        string folderLocation = "d:\\IDCard-DownLoad\\";
            //        if (!Directory.Exists(folderLocation)) { Directory.CreateDirectory(folderLocation); }
            //        if (checkword.Checked == false && chkall.Checked == true)
            //        {
            //            rd3.ExportToDisk(ExportFormatType.PortableDocFormat, folderLocation + "ALL-" + combocompcode.Text + " IDCard.pdf");
            //        }
            //        if (checkword.Checked == false && chkall.Checked == false)
            //        {
            //            if (dtgeneral.Rows.Count < 10)
            //            {
            //                rd3.ExportToDisk(ExportFormatType.PortableDocFormat, folderLocation + idcardcount + " IDCard.pdf");
            //            }
            //            else
            //            {
            //                rd3.ExportToDisk(ExportFormatType.PortableDocFormat, folderLocation + " IDCard.pdf");
            //            }
            //        }
            //        if (checkword.Checked == true && chkall.Checked == true)
            //        {
            //            rd3.ExportToDisk(ExportFormatType.WordForWindows, folderLocation + "ALL-" + combocompcode.Text + " IDCard.doc");
            //        }
            //        if (checkword.Checked == true && chkall.Checked == false)
            //        {
            //            if (dtgeneral.Rows.Count > 10)
            //            {
            //                rd3.ExportToDisk(ExportFormatType.WordForWindows, folderLocation + " IDCard.doc");
            //            }
            //            else
            //            {
            //                rd3.ExportToDisk(ExportFormatType.WordForWindows, folderLocation + idcardcount+ " IDCard.doc");

            //            }
            //        }
            //        News_Click(sender,e);
            //    }
            //    else
            //    {
            //        MessageBox.Show("No Data Found.");
            //    }
            //}
            //Cursor = Cursors.Default; chkall.Checked = false; checkword.Checked = false; txtsearch1.Text = "";
        }

       

        private void txtsearch1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int item0 = 0; listView2.Items.Clear();
                if (txtsearch1.Text.Length >= 1)
                {

                    foreach (ListViewItem item in listfilter.Items)
                    {
                        ListViewItem list = new ListViewItem();
                        if (listfilter.Items[item0].SubItems[4].ToString().Contains(txtsearch1.Text.ToUpper()) || listfilter.Items[item0].SubItems[5].ToString().Contains(txtsearch1.Text.ToUpper()))
                        {
                            list.Text = listfilter.Items[item0].SubItems[0].Text;
                            list.SubItems.Add(listfilter.Items[item0].SubItems[1].Text);
                            list.SubItems.Add(listfilter.Items[item0].SubItems[2].Text);
                            list.SubItems.Add(listfilter.Items[item0].SubItems[3].Text);
                            list.SubItems.Add(listfilter.Items[item0].SubItems[4].Text);
                            list.SubItems.Add(listfilter.Items[item0].SubItems[5].Text);
                            list.SubItems.Add(listfilter.Items[item0].SubItems[6].Text);
                            list.SubItems.Add(listfilter.Items[item0].SubItems[7].Text);
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
                    foreach (ListViewItem item in listfilter.Items)
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
                MessageBox.Show("---" + ex.ToString());
            }
        }




        private void flowLayoutPanel1_DoubleClick(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
     
            companyload(); 
        }

        private void butfront1_Click(object sender, EventArgs e)
        {
            if (flowLayoutPanel1.Controls.Count <= 12)
            {
                var orderedRows = from row in dtgeneral.AsEnumerable().Reverse() select row;
                reversedDt = orderedRows.CopyToDataTable();
                butfront_Click(sender, e);
            }
            else
            {
                MessageBox.Show("Six IdCard only Allowed.  Total Count  : " + flowLayoutPanel1.Controls.Count.ToString(), "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void butback2_Click(object sender, EventArgs e)
        {
            if (flowLayoutPanel1.Controls.Count <= 12)
            {
                var orderedRows = from row in dtgeneral.AsEnumerable().Reverse() select row;
                reversedDt = orderedRows.CopyToDataTable();

                butback_Click(sender, e);
            }
            else
            {
                MessageBox.Show("Six IdCard only Allowed. Total Count : " + flowLayoutPanel1.Controls.Count.ToString(), "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        

        private void Exit_Click_1(object sender, EventArgs e)
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
    }
}
