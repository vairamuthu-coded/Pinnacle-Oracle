using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Net.Mail;
namespace Pinnacle.Canteen
{
    public partial class ItemPermissionDetails : Form,ToolStripAccess
    {
        private static ItemPermissionDetails _instance;
        DateTimePicker from_datedtp = new DateTimePicker();
        DateTimePicker to_datedtp = new DateTimePicker();
        Rectangle rectangle; Models.ItemPermissionDetails c = new Models.ItemPermissionDetails();
        Pinnacle.Models.MailModel obj = new Models.MailModel();
        public static ItemPermissionDetails Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ItemPermissionDetails();
                return _instance;
            }
        }
        public ItemPermissionDetails()
        {
            InitializeComponent(); panelmail.Visible = false;
            usercheck(Class.Users.HCompcode, Class.Users.HUserName, Class.Users.ScreenName);
            Class.Users.Intimation = "PAYROLL";
            Class.Users.IPADDRESS = GenFun.GetLocalIPAddress();Class.Users.LoginTime = 300;

            Class.Users.CREATED = Convert.ToDateTime(System.DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss"));
            Class.Users.SysDate = Convert.ToString(System.DateTime.Now.ToString("dd-MM-yyyy"));
            Class.Users.SysTime = Convert.ToString(DateTime.Now.ToLongTimeString().ToString());

            tabControl1.SelectTab(tabPagedel1);
            this.BackColor = Class.Users.BackColors;

            to_datedtp.Visible = false;
            to_datedtp.Format = DateTimePickerFormat.Custom; to_datedtp.CustomFormat = "dd-MM-yyyy";
            dataGridView1.Controls.Add(to_datedtp);
            to_datedtp.TextChanged += to_dateDtp_TextChanged;

            from_datedtp.Visible = false;
            from_datedtp.Format = DateTimePickerFormat.Custom; to_datedtp.CustomFormat = "dd-MM-yyyy";
            dataGridView1.Controls.Add(from_datedtp);
            from_datedtp.TextChanged += From_datedtp_TextChanged; 

        }
        private void OGSendMailToUser()
        {
            try
            {
                string sel0 = "SELECT  A.MAILID,B.COMPCODE   FROM  CANTEENMAILLIS A   JOIN GTCOMPMAST B ON A.COMPCODE =B.COMPCODE    WHERE B.compcode='" + combocompcode.Text + "'";
                DataSet ds0 = Utility.ExecuteSelectQuery(sel0, "CANTEENMAILLIS");
                DataTable dt0 = ds0.Tables["CANTEENMAILLIS"];
                if (dt0.Rows.Count > 0)
                {
                    panelmail.Visible = true;
                    int j = 0;
                    foreach (DataRow row1 in dt0.Rows)
                    {
                        

                        obj.Pass = System.Configuration.ConfigurationManager.AppSettings["OGPassword"].ToString();
                        obj.From = System.Configuration.ConfigurationManager.AppSettings["FROMMAIL"].ToString();
                        obj.To = row1[0].ToString();
                        obj.Port = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["PORT"].ToString());
                        obj.Host = System.Configuration.ConfigurationManager.AppSettings["HOST"].ToString();
                        MailMessage mm = new MailMessage();
                        SmtpClient sc = new SmtpClient();
                        mm.From = new MailAddress(obj.From);
                        mm.To.Add(obj.To);
                
                        mm.Subject = "ToDay Canteen Menu  ( " + dateTimePicker1.Value.ToString().Substring(0,10) + " ) Details for your reference";
                        string html1 = "<table><tr style='background-color:whitesmoke;border: 1px solid #ccc;font-size: 9pt;color:black;text-align:left;'><th>Dear all</th><tr/><tr style='background-color:SlateGray;border: 1px solid #ccc;color:white;font-size: 16pt'><th>" + mm.Subject + "</th><tr/></table> ";
                        string html = "<br><table cellpadding='5' cellspacing='0' style='border: 1px solid #ccc;font-size: 9pt;font-family:roboto;width:69%' box-shadow: 0 4px 10px 0 rgba(0,0,0,0.2),0 4px 20px 0 rgba(0,0,0,0.19)>";
                       
                        string html2 = "<br><table><tr><th>Thanks </th><tr/><tr><th> IT TEAM </th><tr/></table>";
                       
                        //Adding HeaderRow.
                        html += "<tr>";
                        //foreach (ListViewItem item in listView2.Items)
                        // {
                        html += "<th style='background-color:SlateGray;border: 1px solid #ccc;color:white'> S.No </th>";
                        html += "<th style='background-color:SlateGray;border: 1px solid #ccc;color:white'>Menu Items </th>";
                        html += "<th style='background-color:SlateGray;border: 1px solid #ccc;color:white'> Start Date </th>";
                        html += "<th style='background-color:SlateGray;border: 1px solid #ccc;color:white'> Start Time </th>";
                        html += "<th style='background-color:SlateGray;border: 1px solid #ccc;color:white'> End Date </th>";
                        html += "<th style='background-color:SlateGray;border: 1px solid #ccc;color:white'> End Time </th>";
                        //}
                        html += "</tr>";
                        int rowindex1 = 1;
                        
                        //Adding DataRow.
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (row.Cells[3].EditedFormattedValue.ToString() != "")
                            {
                                html += "<tr>";
                                html += "<td style='width:3%;border: 1px solid #ccc'>" + rowindex1.ToString() + "</td>";
                                html += "<td style='width:25%;border: 1px solid #ccc'>" + row.Cells[3].EditedFormattedValue.ToString()+"  -  " + row.Cells[6].EditedFormattedValue + "</td>";
                                html += "<td style='width:17%;border: 1px solid #ccc'>" + row.Cells[7].EditedFormattedValue.ToString().Substring(0,10) + "</td>";
                                html += "<td style='width:12%;border: 1px solid #ccc'>" + row.Cells[8].EditedFormattedValue + "</td>";
                                html += "<td style='width:18%;border: 1px solid #ccc'>" + row.Cells[9].EditedFormattedValue.ToString().Substring(0,10) + "</td>";
                                html += "<td style='width:10%;border: 1px solid #ccc'>" + row.Cells[10].EditedFormattedValue + "</td>";

                                html += "</tr>";
                                rowindex1++;
                            }
                        }
                        j++;
                        labelmail.Refresh();
                        labelmail.Text = "Sending E-Mail to " + obj.To + "  ****";
                        //Table end.
                        html += "</table>";

                        //File.WriteAllText(@"E:\Files\DataGridView.htm", html);
                        mm.IsBodyHtml = true;
                        mm.Body = html1 + html + html2; sc.Port = 587;
                        sc.Credentials = new System.Net.NetworkCredential(obj.From, obj.Pass);
                        sc.EnableSsl = true;
                        sc.Send(mm);
                    }

                }
                else
                {
                    panelmail.Visible = false;
                }
            }
            catch (Exception EX)
            {
                MessageBox.Show(EX.ToString());
            }
            panelmail.Visible = false;
        }
        private void From_datedtp_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.CurrentCell.Value = from_datedtp.Value.ToString("dd-MM-yyyy").Substring(0, 10);
        }

        private void to_dateDtp_TextChanged(object sender, EventArgs e)
        {
            try
            {
                
                dataGridView1.CurrentCell.Value = to_datedtp.Value.ToString("dd-MM-yyyy").Substring(0, 10);
             

            }
            catch (Exception ex) { }




        }


   
     
        private string readserialvalue;
        decimal listview2totalweight = 0;
        Models.Validate va = new Models.Validate();
        Models.Master mas = new Models.Master();
        Models.UserRights sm = new Models.UserRights();
        
        string dat = ""; int i = 0; int j = 0;
        PinnacleMdi mdi = new PinnacleMdi();
        bool validprint = false;
        ListView listfilter = new ListView();
       
        public void usercheck(string s, string ss, string sss)
        {
            try
            {
                DataTable dt1 = sm.headerdropdowns(s, ss, sss);
                if (dt1.Rows.Count > 0)
                {
                    if (dt1.Rows[0]["Menuname"].ToString() == Class.Users.ScreenName)
                    {
                       
                    }


                }
                else
                {
                    MessageBox.Show("Invalid");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("usercheck: " + ex.Message, " Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void DayShuffleEntry_Load(object sender, EventArgs e)
        {
           
            this.KeyPreview = true;
     
          
        }
        public void News()
        {
            empty();
            holidaycateload(); companyload(); GridLoad(); ItemnameLoad();
            tabControl1.SelectTab(tabPagedel1);  autonumberload(); combocompcode.Select();
         

        }

      
        public void companyload()
        {
            try
            {
                string sel0 = "SELECT DISTINCT B.GTCOMPMASTID,B.COMPCODE FROM  HREMPLOYMAST A JOIN GTCOMPMAST B ON A.COMPCODE=B.GTCOMPMASTID  JOIN HREMPLOYDETAILS C ON A.HREMPLOYMASTID=C.HREMPLOYMASTID JOIN GTFINANCIALYEAR E ON E.GTFINANCIALYEARID=C.FINYEAR   order by 1";
                DataSet ds0 = Utility.ExecuteSelectQuery(sel0, "hremploymast");
                DataTable dt = ds0.Tables["hremploymast"];

                combocompcode.DisplayMember = "COMPCODE";
                combocompcode.ValueMember = "GTCOMPMASTID";
                combocompcode.DataSource = dt;
                combounit.DisplayMember = "COMPCODE";
                combounit.ValueMember = "GTCOMPMASTID";
                combounit.DataSource = dt;
                

            }
            catch (Exception ex)
            {
                MessageBox.Show("companyload: " + ex.Message, " Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        //string sel = "select b.gtcompmastid,b.compname from  gtcompmast b where b.ptransaction = 'COMPANY' and b.compcode ='" + combocompcode.Text + "'; ";
        //DataSet ds = Utility.ExecuteSelectQuery(sel, "Asptblmenper");
        //DataTable dt = ds.Tables["Asptblmenper"];
        //int cnt = dt.Rows.Count;

        //combocompname.DisplayMember = "compname";
        //        combocompname.ValueMember = "gtcompmastid";
        //        combocompname.DataSource = dt;

        //        try
        //        {
        //            string sel1 = "select distinct  a.month from Asptblmenperdet a order by 1;";
        //DataSet ds1 = Utility.ExecuteSelectQuery(sel1, "Asptblmenperdet");
        //DataTable dt1 = ds1.Tables["Asptblmenperdet"];

        //combomonth.DisplayMember = "month";
        //            combomonth.ValueMember = "month";
        //            combomonth.DataSource = dt1;

        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show("combocompcode_SelectedIndexChanged: " + ex.Message, " Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        }

        public void autonumberload()
        {
            try
            {
                //txtAsptblmenper1id.Text = ""; txtdocid.Text = "";
                if (Convert.ToInt64("0" + combocompcode.SelectedValue) > 0)
                {
                    string sel = "select max(ASPTBLMENPERID)+1 as id from ASPTBLMENPER ";
                    DataSet ds = Utility.ExecuteSelectQuery(sel, "ASPTBLMENPER");
                    DataTable dt = ds.Tables["ASPTBLMENPER"];
                    string sel1 = "select b.gtcompmastid, b.compname from  gtcompmast b  where  b.compcode='" + combocompcode.Text + "'";
                    DataSet ds1 = Utility.ExecuteSelectQuery(sel1, "gtcompmast");
                    DataTable dt1 = ds1.Tables["gtcompmast"];
                    combocompname.DisplayMember = "compname";
                    combocompname.ValueMember = "gtcompmastid";
                    combocompname.DataSource = dt1;
                    int cnt = dt.Rows.Count;
                    if (dt.Rows[0]["id"].ToString() == "")
                    {
                        //string sel2 = "SELECT MAX(A.ASPTBLMENPERID) AS ASPTBLMENPERID  FROM Asptblmenper A JOIN GTCOMPMAST B ON A.COMPCODE=B.GTCOMPMASTID where  b.compcode='" + combocompcode.Text + "'";
                        //DataSet ds2 = Utility.ExecuteSelectQuery(sel2, "gtcompmast");
                        //DataTable dt2 = ds2.Tables["gtcompmast"];
                        //if (dt2.Rows.Count > 0) {
                        //    txtdocid.Text = combocompcode.Text + "/" + Class.Users.Finyear + "/" + dt.Rows[0]["ASPTBLMENPERID"].ToString();
                        //    txtmenper1id.Text = dt.Rows[0]["id"].ToString();
                        //}
                        //else
                        //{
                            txtdocid.Text = combocompcode.Text + "/" + Class.Users.Finyear + "/" + 1;
                            txtmenper1id.Text = "1";
                        //}
                    }
                    else
                    {

                        txtdocid.Text = combocompcode.Text + "/" + Class.Users.Finyear + "/" + dt.Rows[0]["id"].ToString();
                        txtmenper1id.Text = dt.Rows[0]["id"].ToString();

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("autonumberload: " + ex.Message, " Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

      
        public void holidaycateload()
        {
            //try
            //{
            //    string sel = "select a.asptblhrholidaycatmasid,a.holidaycategory from asptblhrholidaycatmas a where a.active='T';";
            //    DataSet ds = Utility.ExecuteSelectQuery(sel, "asptblhrholidaycatmas");
            //    DataTable dt = ds.Tables["asptblhrholidaycatmas"];

            //    HOLIDAYCATEGORY.DisplayMember = "holidaycategory";
            //    HOLIDAYCATEGORY.ValueMember = "asptblhrholidaycatmasid";
            //    HOLIDAYCATEGORY.DataSource = dt;
               
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("holidaycateload: " + ex.Message, " Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}

        }
        bool isvalid = false;

        private bool Checks()
        {
            Models.Validate va = new Models.Validate();
           int rowcount = 0;
            rowcount = dataGridView1.Rows.Count - 1;
            if (combocompcode.Text == "")
            {
                MessageBox.Show("CompCode is Empty." + combocompcode.Text, " Success Message ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                combocompcode.Select();
                return false;

            }
            if (Convert.ToInt64("0" + combousername.SelectedValue) <= 0)
            {
                MessageBox.Show("Username is Empty." + combousername.Text, " Success Message ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                combousername.Select();
                return false;

            }



            foreach (DataGridViewRow row in dataGridView1.Rows)
            {

                

                if (Convert.ToString(row.Cells["ITEMNAME1"].EditedFormattedValue.ToString()) != null && Convert.ToString(row.Cells["ITEMNAME1"].EditedFormattedValue.ToString()) != "" &&  Convert.ToString(row.Cells[7].EditedFormattedValue.ToString()) == "" && Convert.ToString(row.Cells[8].EditedFormattedValue.ToString()) == "" && Convert.ToString(row.Cells[9].EditedFormattedValue.ToString()) == "" )
                {
                    if (Convert.ToString(row.Cells[6].EditedFormattedValue.ToString()) == "")
                    {
                        MessageBox.Show("Invalid Category " + row.Cells[6].EditedFormattedValue.ToString());
                        row.Cells[6].Selected = true;
                        row.Cells[6].Value = null;
                        return false;
                    }
                    if (Convert.ToString(row.Cells[10].EditedFormattedValue.ToString()) == "")
                    {
                        MessageBox.Show("Invalid ToTime " + row.Cells[10].EditedFormattedValue.ToString());
                        row.Cells[10].Selected = true;
                        row.Cells[10].Value = null;
                        return false;
                    }
                    string ss = row.Cells[8].EditedFormattedValue.ToString().Substring(0, 3);
                    string[] data = ss.Split(':');
                    if (Convert.ToInt32(data[0]) > 100)
                    {
                        MessageBox.Show("Invalid Time Formate " + row.Cells[8].EditedFormattedValue.ToString());
                        row.Cells[8].Selected = true;
                        row.Cells[8].Value = null;
                        return false;
                    }
                    if (Convert.ToString(row.Cells[8].EditedFormattedValue.ToString()) == "")
                    {

                        MessageBox.Show("Invalid ToTime Formate " + row.Cells[8].EditedFormattedValue.ToString());

                        row.Cells[8].Selected = true;

                        return false;
                    }
                    string sss = row.Cells[10].EditedFormattedValue.ToString().Substring(0, 3);
                    string[] data1 = sss.Split(':');
                    if (Convert.ToInt32(data1[0]) > 100)
                    {
                        MessageBox.Show("Invalid Time Formate  " + row.Cells[10].EditedFormattedValue.ToString());
                        row.Cells[10].Selected = true; 
                        row.Cells[10].Value = null; return false; 
                    }
                    //if (row.Cells[8].EditedFormattedValue.ToString().Length <= 8)
                    //{
                    //    MessageBox.Show("Invalid Time Formate " + row.Cells[8].EditedFormattedValue.ToString());

                    //    row.Cells[8].Selected = true; return false;
                    //}
                   
                }


            }
            Cursor = Cursors.Default;
            return true;
        }
        public void Saves()
        {
            Int64 maxid = 0; Int64 j = 0; 
           
             try
            {
             Cursor= Cursors.WaitCursor;
                isvalid = Checks();

                if (isvalid == true)
                {
                    Models.ItemPermission c1 = new Models.ItemPermission();
                    Models.Validate va = new Models.Validate();
                    Cursor = Cursors.WaitCursor;
                    if (txtmenperid.Text == "") { autonumberload(); c1.Asptblmenper1id = Convert.ToInt64("0" + txtmenper1id.Text); c1.Asptblmenperid = 0; txtmenperid.Text = ""; }
                    else { c1.Asptblmenperid = Convert.ToInt64("0" + txtmenperid.Text); c1.Asptblmenper1id = Convert.ToInt64("0" + txtmenper1id.Text); }
                    c1.Finyear = System.DateTime.Now.Year.ToString();
                    c1.Compcode = Convert.ToInt64("0" + combocompcode.SelectedValue);
                    c1.Docid = Convert.ToString(txtdocid.Text);

                    if (checkactive.Checked == true)
                        c1.Active = "T";
                    else
                        c1.Active = "F";
                    c1.Fromdate = Convert.ToDateTime(dateTimePicker1.Value.ToString().Substring(0, 10));
                    c1.Fromtime = Convert.ToDateTime(dateTimePicker1.Value.ToString().Substring(11, 8));
                    c1.Compcode1 = Convert.ToInt64(combocompcode.SelectedValue);
                    c1.Username = Convert.ToInt64("0" + combousername.SelectedValue);
                    c1.Createdby = Convert.ToString(Class.Users.HUserName);
                    c1.Createdon = Convert.ToString(System.DateTime.Now.ToLongTimeString());
                    c1.Modified = Class.Users.HUserName;
                    c1.Ipaddress = Class.Users.IPADDRESS;
                    c1.Systemtime = Convert.ToDateTime(System.DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss"));
                    string sel = "select Asptblmenperid    from  Asptblmenper   WHERE  Asptblmenper1id='" + c1.Asptblmenper1id + "'  and compcode='" + c1.Compcode + "' and USERNAME='" + c1.Username + "'  and  FROMDATE=TO_DATE('" + c1.Fromdate.ToString("dd-MM-yyyy").Substring(0, 10) + "','dd-MM-yyyy') and active='" + c1.Active + "'";
                    DataSet ds = Utility.ExecuteSelectQuery(sel, "Asptblmenper");
                    DataTable dt = ds.Tables["Asptblmenper"];
                    if (dt.Rows.Count != 0)
                    {

                    }
                    else if (dt.Rows.Count != 0 && Convert.ToInt32("0" + txtmenperid.Text) == 0 || Convert.ToInt32("0" + txtmenperid.Text) == 0)
                    {
                        string ins = "insert into Asptblmenper(Asptblmenper1id,finyear,Compcode,docid,FROMDATE,FROMTIME,active,username,createdby,createdon,modifiedby,ipaddress,Systemtime,DOCDATE) values('" + c1.Asptblmenper1id + "','" + c1.Finyear + "','" + c1.Compcode + "','" + c1.Docid + "',TO_DATE('" + c1.Fromdate.ToString("dd-MM-yyyy").Substring(0, 10) + "','dd-MM-yyyy'),'" + c1.Fromtime.ToString().Substring(11, 8) + "','" + c1.Active + "','" + combousername.SelectedValue + "','" + Class.Users.HUserName + "','" + dateTimePicker1.Value.ToString("dd-MM-yyy hh24:mi:ss") + "','" + Class.Users.CREATED + "','" + Class.Users.IPADDRESS + "',to_date('" + System.DateTime.Now.ToString() + "','dd-MM-yyyy hh24:mi:ss'),TO_DATE('" + dateTimePicker1.Value.ToString("dd-MM-yyy") + "','dd-MM-yyyy'))";
                        Utility.ExecuteNonQuery(ins);
                        string sel2 = "select max(Asptblmenperid) as Asptblmenperid   from  Asptblmenper";
                        DataSet ds2 = Utility.ExecuteSelectQuery(sel2, "Asptblmenper");
                        DataTable dt2 = ds2.Tables["Asptblmenper"]; maxid = 0;
                        maxid = Convert.ToInt64(dt2.Rows[0]["Asptblmenperid"].ToString());
                    }
                    else
                    {
                        string up = "update  Asptblmenper  set Asptblmenper1id='" + c1.Asptblmenper1id + "' , finyear='" + c1.Finyear + "' , Compcode='" + c1.Compcode + "' ,  Docid='" + c1.Docid + "'  , FROMDATE=TO_DATE('" + c1.Fromdate.ToString("dd-MM-yyyy").Substring(0, 10) + "','dd-MM-yyyy'), Fromtime='" + c1.Fromtime.ToString().Substring(11, 8) + "', username='" + c1.Username + "',createdby='" + Class.Users.HUserName + "', modifiedby='" + Class.Users.CREATED + "',ipaddress='" + Class.Users.IPADDRESS + "'  , systemtime=to_date('" + c1.Systemtime + "','dd-MM-yyyy hh24:mi:ss'),DOCDATE=TO_DATE('" + dateTimePicker1.Value.ToString("dd-MM-yyy") + "','dd-MM-yyyy') where Asptblmenperid='" + c1.Asptblmenperid + "'";
                        Utility.ExecuteNonQuery(up);


                    }
                    int i = 0;
                    //Models.ItemPermissionDetails c = new Models.ItemPermissionDetails();
                    int cc = dataGridView1.Rows.Count - 1;

                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {



                        if (Convert.ToString(row.Cells["ITEMNAME1"].EditedFormattedValue).ToString() != "" && Convert.ToString(row.Cells["CATEGORY"].EditedFormattedValue).ToString() != "" && Convert.ToString(row.Cells["TODATE"].EditedFormattedValue) != "" && Convert.ToString(row.Cells["TOTIME"].EditedFormattedValue.ToString()) != "")
                        {
                            if (txtmenperid.Text == "")
                            {
                                c.Asptblmenperdetid = 0;
                                c.Asptblmenperid = Convert.ToInt64("0" + maxid);
                            }
                            else
                            {
                                c.Asptblmenperdetid = Convert.ToInt64("0" + row.Cells["ASPTBLMENPERDETID"].EditedFormattedValue);
                                c.Asptblmenperid = Convert.ToInt64("0" + txtmenperid.Text);
                            }

                            c.Asptblcanitemmasid = Convert.ToInt64("0" + row.Cells["ASPTBLCANITEMMASID"].EditedFormattedValue);
                            c.Itemcode = Convert.ToString(row.Cells["ITEMCODE"].EditedFormattedValue.ToString());


                            string sel2 = " SELECT DISTINCT  A.ASPTBLCANCATEGORYMASID  FROM ASPTBLCANCATEGORYMAS A    WHERE A.CATEGORY='" + row.Cells["CATEGORY"].EditedFormattedValue.ToString() + "'  ";
                            DataSet ds2 = Utility.ExecuteSelectQuery(sel2, "ASPTBLCANITEMMAS");
                            DataTable dt2 = ds2.Tables["ASPTBLCANITEMMAS"];
                            if (dt2.Rows.Count > 0)
                            {
                                c.Category = Convert.ToInt64(dt2.Rows[0]["ASPTBLCANCATEGORYMASID"].ToString());
                            }
                            if (Convert.ToInt64(row.Cells["ASPTBLCANITEMMASID"].Value.ToString()) > 0)
                            {
                                c.Itemname1 = Convert.ToInt64(row.Cells["ASPTBLCANITEMMASID"].Value.ToString());
                            }
                            else
                            {
                                c.Itemname1 = Convert.ToInt64(row.Cells["ITEMNAME1"].Value.ToString());
                            }
                            c.Fromdate = Convert.ToDateTime(row.Cells["FROMDATE"].EditedFormattedValue.ToString());
                            c.Fromtime = Convert.ToDateTime(row.Cells["FROMTIME"].EditedFormattedValue.ToString());
                            c.Todate = Convert.ToDateTime(row.Cells["TODATE"].EditedFormattedValue.ToString());
                            c.Totime = Convert.ToDateTime(row.Cells["Totime"].EditedFormattedValue.ToString());
                            c.Active = Convert.ToString('T');
                            c.Notes = Convert.ToString(row.Cells["notes"].EditedFormattedValue.ToString());
                            string sel1 = "select Asptblmenperdetid    from  Asptblmenperdet   where Itemname1='" + c.Itemname1 + "'  and Fromdate=to_date('" + c.Fromdate.ToString("dd-MM-yyyy").Substring(0, 10) + "','dd-MM-yyyy') and FROMTIME='" + c.Fromtime.ToString().Substring(11, 8) + "'  and Todate=to_date('" + c.Todate.ToString("dd-MM-yyyy").Substring(0, 10) + "','dd-MM-yyyy') and Totime='" + c.Totime.ToString().Substring(11, 8) + "' and notes='" + c.Notes + "' and Asptblmenperid='" + c.Asptblmenperid + "'";
                            DataSet ds1 = Utility.ExecuteSelectQuery(sel1, "Asptblmenperdet");
                            DataTable dt1 = ds1.Tables["Asptblmenperdet"];
                            if (dt1.Rows.Count != 0)
                            {
                             
                                tabControl1.SelectTab(tabPagedel2); 
                            }
                            else if (dt1.Rows.Count != 0 && Convert.ToInt64("0" + c.Asptblmenperdetid) == 0 || Convert.ToInt64("0" + c.Asptblmenperdetid) == 0)
                            {
                                string ins1 = "insert into Asptblmenperdet(ASPTBLMENPERID,ASPTBLCANITEMMASID,ITEMCODE,ITEMNAME1,CATEGORY,FROMDATE,FROMTIME,TODATE,TOTIME,ACTIVE,notes,systemtime,docdate) values('" + c.Asptblmenperid + "' ,'" + c.Asptblcanitemmasid + "' ,'" + c.Itemcode + "' ,'" + c.Itemname1 + "','" + c.Category + "',to_date('" + c.Fromdate.ToString("dd-MM-yyyy").Substring(0, 10) + "','dd-MM-yyyy'),'" + c.Fromtime.ToString().Substring(11, 8) + "',to_date('" + c.Todate.ToString("dd-MM-yyyy").Substring(0, 10) + "','dd-MM-yyyy'),'" + c.Totime.ToString().Substring(11, 8) + "','" + c.Active + "','" + c.Notes + "',to_date('" + c1.Systemtime + "','dd-MM-yyyy hh24:mi:ss'),TO_DATE('" + dateTimePicker1.Value.ToString("dd-MM-yyy") + "','dd-MM-yyyy') )";
                                Utility.ExecuteNonQuery(ins1); 
                            }
                            else
                            {
                                string up1 = "update  Asptblmenperdet  set Asptblmenperid='" + c.Asptblmenperid + "' , Asptblcanitemmasid='" + c.Asptblcanitemmasid + "', Itemcode='" + c.Itemcode + "' ,  Itemname1='" + c.Itemname1 + "' , Category='" + c.Category + "'  ,FROMDATE=to_date('" + c.Fromdate.ToString("dd-MM-yyyy").Substring(0, 10) + "','dd-MM-yyyy'),FROMTIME='" + c.Fromtime.ToString().Substring(11, 8) + "', Todate=to_date('" + c.Todate.ToString("dd-MM-yyyy").Substring(0, 10) + "','dd-MM-yyyy') , Totime='" + c.Totime.ToString().Substring(11, 8) + "' , Active='" + c.Active + "' , notes='" + c.Notes + "' ,systemtime=to_date('" + c1.Systemtime + "','dd-MM-yyyy hh24:mi:ss'),docdate=TO_DATE('" + dateTimePicker1.Value.ToString("dd-MM-yyy") + "','dd-MM-yyyy')  where Asptblmenperdetid='" + c.Asptblmenperdetid + "'";
                                Utility.ExecuteNonQuery(up1); 
                            }
                        }
                      
                    }
                    if (txtmenperid.Text == "" )
                    {
                        OGSendMailToUser();
                        MessageBox.Show("Record Saved Successfully " + txtdocid.Text, " Success Message ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        GridLoad(); empty();
                        tabControl1.SelectTab(tabPagedel2); Cursor = Cursors.Default;
                    }
                    if (txtmenperid.Text != "" )
                    {
                       OGSendMailToUser();
                        MessageBox.Show("Record Updated Successfully " + txtdocid.Text, " Update Message ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        GridLoad(); empty();
                        tabControl1.SelectTab(tabPagedel2); Cursor = Cursors.Default;
                    }

                }
                
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
            }
            Cursor = Cursors.Default;
        }

        private void RawMaterialEntry_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }



        public void userload(Int64 CC)
        {

            try
            {
                if (CC > 0)
                {
                    string sel0 = "SELECT DISTINCT B.USERID, B.USERNAME FROM   GTCOMPMAST A  join asptblusermas B on B.COMPCODE=A.GTCOMPMASTID   where      A.GTCOMPMASTID='" + CC + "'      order by 1";
                    DataSet ds0 = Utility.ExecuteSelectQuery(sel0, "asptblusermas");
                    DataTable dt = ds0.Tables["asptblusermas"];

                    combouser.DisplayMember = "USERNAME";
                    combouser.ValueMember = "USERID";
                    combouser.DataSource = dt;
                    combousername.DisplayMember = "USERNAME";
                    combousername.ValueMember = "USERID";
                    combousername.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("userload: " + ex.Message, " Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void ItemnameLoad()
        {
            try
            {
                string sel1 = "  SELECT  0 AS ASPTBLCANITEMMASID, '' AS ITEMNAME1 FROM  DUAL UNION ALL SELECT A.ASPTBLCANITEMMASID,A.ITEMNAME1 FROM  ASPTBLCANITEMMAS A   WHERE A.ACTIVE='T'    ORDER BY 1";
                DataSet ds = Utility.ExecuteSelectQuery(sel1, "ASPTBLCANITEMMAS");
                DataTable dt1 = ds.Tables["ASPTBLCANITEMMAS"];

                ITEMNAME1.DisplayMember = "ITEMNAME1";
                ITEMNAME1.ValueMember = "ASPTBLCANITEMMASID";
                ITEMNAME1.DataSource = dt1;
                if (dt1.Rows.Count > 0)
                {
                    string sel2 = "  SELECT DISTINCT A.ASPTBLCANCATEGORYMASID,A.CATEGORY FROM  ASPTBLCANCATEGORYMAS A   WHERE A.ACTIVE='T'    ORDER BY 1";

                    DataSet ds2 = Utility.ExecuteSelectQuery(sel2, "ASPTBLCANCATEGORYMAS");
                    DataTable dt2 = ds2.Tables["ASPTBLCANCATEGORYMAS"];

                    CATEGORY.DisplayMember = "CATEGORY";
                    CATEGORY.ValueMember = "ASPTBLCANCATEGORYMASID";
                    CATEGORY.DataSource = dt2;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("companyload: " + ex.Message, " Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void empty()
        {
            txtmenperid.Text = ""; combocompcode.SelectedIndex = -1;combocompcode.Text = ""; combounit.SelectedIndex = -1; combounit.Text = "";
            combocompname.SelectedIndex = -1; combocompname.Text = ""; combocompcode.Enabled = true; dateTimePicker1.Value = System.DateTime.Now;
            combounit.SelectedIndex = -1;combouser.SelectedIndex = -1;
            this.txtmenperid.Enabled = false; combousername.SelectedIndex= - 1;
            listfilter.Items.Clear();
          //  txtAsptblmenper1id.Text = "";           
            txtdocid.Text = ""; Cursor = Cursors.Default;
            dataGridView1.AllowUserToAddRows = true;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Class.Users.BackColors;   
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView1.EnableHeadersVisualStyles = false;
            do
            {
                int i = 0;
                for (i = 0; i < dataGridView1.Rows.Count; i++) { try { dataGridView1.Rows.RemoveAt(i); } catch (Exception) { } }

            }
            while (dataGridView1.Rows.Count > 1);



        }

        public void GridLoad(string date)
        {

            try
            {
                if (Convert.ToInt64("0" + combocompcode.SelectedValue) > 0 && Convert.ToInt64("0" + combouser.SelectedValue) > 0)
                {
                    int j = 1;

                    Class.Users.LoginTime = 300;
                    var currentDateTime = DateTime.Now; listView1.Items.Clear();
                    var currentTimeAlone = new TimeSpan(currentDateTime.Hour, currentDateTime.Minute, currentDateTime.Second);
                    string sel0 = "SELECT A.ASPTBLMENPERID,A.ASPTBLMENPER1ID,A.DOCID,B.COMPCODE,B.COMPNAME,C.USERNAME, A.ACTIVE,A.FROMDATE  FROM  ASPTBLMENPER A    JOIN GTCOMPMAST B ON B.GTCOMPMASTID=A.COMPCODE JOIN ASPTBLUSERMAS C ON C.COMPCODE=A.COMPCODE  AND C.USERID=A.USERNAME  WHERE  B.GTCOMPMASTID='" + combounit.SelectedValue + "'  AND C.USERID='" + combouser.SelectedValue + "'  and A.FROMDATE=to_Date('" + dateTimePicker3.Value.ToString().Substring(0, 10) + "','dd-MM-yyyy')   ORDER BY 1";
                    DataSet ds0 = Utility.ExecuteSelectQuery(sel0, "asptblmenper");
                    DataTable dt0 = ds0.Tables["asptblmenper"];
                    if (dt0.Rows.Count > 0)
                    {
                        foreach (DataRow myRow in dt0.Rows)
                        {
                            ListViewItem list = new ListViewItem();
                            list.SubItems.Add(j.ToString());
                            list.SubItems.Add(myRow["ASPTBLMENPERID"].ToString());
                            list.SubItems.Add(myRow["ASPTBLMENPER1ID"].ToString());
                            list.SubItems.Add(myRow["DOCID"].ToString());
                            list.SubItems.Add(myRow["COMPCODE"].ToString());
                            list.SubItems.Add(myRow["USERNAME"].ToString());
                            list.SubItems.Add(myRow["FROMDATE"].ToString());
                            list.SubItems.Add(myRow["ACTIVE"].ToString());
                            if (i % 2 == 0)
                            {
                                list.BackColor = Color.White;

                            }
                            else
                            {
                                list.BackColor = Color.WhiteSmoke;

                            }
                            listView1.Items.Add(list); i++; j++;
                        }
                    }
                }
            }
            catch (Exception ex) { }
        }
        public void GridLoad()
        {
            try
            {
               
                    int j = 1; listView1.Items.Clear(); 

                    string sel1 = "SELECT A.ASPTBLMENPERID,A.ASPTBLMENPER1ID,A.DOCID,docdate, B.COMPCODE,B.COMPNAME,C.USERNAME, A.ACTIVE,A.FROMDATE  FROM  ASPTBLMENPER A    JOIN GTCOMPMAST B ON B.GTCOMPMASTID=A.COMPCODE JOIN ASPTBLUSERMAS C ON C.COMPCODE=A.COMPCODE  AND C.USERID=A.USERNAME     ORDER BY a.ASPTBLMENPERID desc";
                    DataSet ds = Utility.ExecuteSelectQuery(sel1, "ASPTBLCANITEMMAS");
                    DataTable dt1 = ds.Tables["ASPTBLCANITEMMAS"];
                if (dt1.Rows.Count > 0)
                {
                    foreach (DataRow myRow in dt1.Rows)
                    {
                        ListViewItem list = new ListViewItem();
                        list.SubItems.Add(j.ToString());
                        list.SubItems.Add(myRow["ASPTBLMENPERID"].ToString());
                        list.SubItems.Add(myRow["ASPTBLMENPER1ID"].ToString());
                        list.SubItems.Add(myRow["DOCID"].ToString());
                        list.SubItems.Add(myRow["COMPCODE"].ToString());
                        list.SubItems.Add(myRow["USERNAME"].ToString());
                        list.SubItems.Add(myRow["docdate"].ToString());
                        list.SubItems.Add(myRow["ACTIVE"].ToString());
                        if (i % 2 == 0)
                        {
                            list.BackColor = Color.White;

                        }
                        else
                        {
                            list.BackColor = Color.WhiteSmoke;

                        }
                        listView1.Items.Add(list); i++; j++;
                    }

                }
            }
            catch (Exception ex) { }
        }
        private void ListView1_ItemActivate(object sender, EventArgs e)
        {
            try
            {
               // empty();
                if (listView1.Items.Count > 0)
                {
              
                    txtmenperid.Text = listView1.SelectedItems[0].SubItems[2].Text;
                    string sel1 = "SELECT A.ASPTBLMENPERID,A.ASPTBLMENPER1ID,A.DOCID,B.GTCOMPMASTID,B.COMPNAME,a.USERNAME, A.ACTIVE,to_char(A.FROMDATE) as FROMDATE  FROM  ASPTBLMENPER A    JOIN GTCOMPMAST B ON B.GTCOMPMASTID=A.COMPCODE JOIN ASPTBLUSERMAS C ON C.COMPCODE=A.COMPCODE  AND C.USERID=A.USERNAME   WHERE  a.ASPTBLMENPERID='" + txtmenperid.Text+ "'    ORDER BY 1";
                    DataSet ds = Utility.ExecuteSelectQuery(sel1, "ASPTBLCANITEMMAS");
                    DataTable dt = ds.Tables["ASPTBLCANITEMMAS"];
                    int i = 1;
                    if (dt.Rows.Count > 0)
                    {
            
   
                        txtmenperid.Text = dt.Rows[0]["ASPTBLMENPERID"].ToString();
                        txtmenper1id.Text = Convert.ToString(dt.Rows[0]["ASPTBLMENPER1ID"].ToString());                      
                        combocompcode.SelectedValue = Convert.ToString(dt.Rows[0]["GTCOMPMASTID"].ToString());
                        combousername.SelectedValue = Convert.ToString(dt.Rows[0]["username"].ToString());
                        combocompname.Text = Convert.ToString(dt.Rows[0]["compname"].ToString());
                        txtdocid.Text = Convert.ToString(dt.Rows[0]["Docid"].ToString());
                        dateTimePicker1.Text = dt.Rows[0]["FROMDATE"].ToString();
                        if (dt.Rows[0]["ACTIVE"].ToString() == "T") { checkactive.Checked = true; } else { checkactive.Checked = false; }
                        string sel2 = "SELECT a.ASPTBLMENPERDETID, a.ASPTBLMENPERID,C.ASPTBLCANITEMMASID,C.ITEMNAME1,a.ITEMCODE,D.CATEGORY,A.FROMDATE, a.FROMTIME,a.TODATE, a.TOTIME,a.NOTES  FROM ASPTBLMENPERDET  a  JOIN ASPTBLCANITEMMAS C ON C.ASPTBLCANITEMMASID=a.ITEMNAME1 join ASPTBLCANCATEGORYMAS d on d.ASPTBLCANCATEGORYMASID=A.CATEGORY WHERE  a.ASPTBLMENPERID='" + txtmenperid.Text + "' ORDER BY 1 ";
                        DataSet ds1 = Utility.ExecuteSelectQuery(sel2, "ASPTBLCANITEMMAS");
                        DataTable dt1 = ds1.Tables["ASPTBLCANITEMMAS"];
                        if (dt1.Rows.Count > 0)
                        {
                           
                            
                            dataGridView1.Rows.Clear();
                           
                            for (i = 0; i < dt1.Rows.Count; i++)
                            {
                                dataGridView1.Rows.Add();


                                dataGridView1.Rows[i].Cells[1].Value = Convert.ToInt64("0" + dt1.Rows[i]["ASPTBLMENPERDETID"].ToString());
                                dataGridView1.Rows[i].Cells[2].Value = Convert.ToInt64("0" + dt1.Rows[i]["ASPTBLMENPERID"].ToString());
                                dataGridView1.Rows[i].Cells[3].Value = Convert.ToString(dt1.Rows[i]["ITEMNAME1"].ToString());
                                dataGridView1.Rows[i].Cells[4].Value = Convert.ToInt64(dt1.Rows[i]["ASPTBLCANITEMMASID"].ToString());
                                dataGridView1.Rows[i].Cells[5].Value = Convert.ToString(dt1.Rows[i]["ITEMCODE"].ToString());
                                dataGridView1.Rows[i].Cells[6].Value = Convert.ToString(dt1.Rows[i]["CATEGORY"].ToString());
                                dataGridView1.Rows[i].Cells[7].Value = dt1.Rows[i]["FROMDATE"].ToString();
                                dataGridView1.Rows[i].Cells[8].Value = dt1.Rows[i]["FROMTIME"].ToString();
                                dataGridView1.Rows[i].Cells[9].Value = dt1.Rows[i]["TODATE"].ToString();
                                dataGridView1.Rows[i].Cells[10].Value = dt1.Rows[i]["TOTIME"].ToString();
                                dataGridView1.Rows[i].Cells[11].Value = dt1.Rows[i]["NOTES"].ToString();



                            }


                        }
                        
                           
                    }
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                tabControl1.SelectTab(tabPagedel1);
            }
        }

        private void Txtsearch_TextChanged(object sender, EventArgs e)
        {

            try
            {
                int item0 = 0; listView1.Items.Clear();
                if (txtsearch.Text.Length >= 1)
                {
                   
                    foreach (ListViewItem item in listfilter.Items)
                    {
                        ListViewItem list = new ListViewItem();
                        if (listfilter.Items[item0].SubItems[4].ToString().Contains(txtsearch.Text.ToUpper()) || listfilter.Items[item0].SubItems[5].ToString().Contains(txtsearch.Text) || listfilter.Items[item0].SubItems[6].ToString().Contains(txtsearch.Text))
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
                            listView1.Items.Add(list);


                        }
                        item0++;
                    }
                    lbltotal.Text = "Total Count: " + listView1.Items.Count;
                }
                else
                {
                    ListView ll = new ListView();
                    listView1.Items.Clear();

                    foreach (ListViewItem item in listfilter.Items)
                    {

                        this.listView1.Items.Add((ListViewItem)item.Clone());

                        item0++;
                    }
                    lbltotal.Text = "Total Count: " + listView1.Items.Count;
                }


            }
            catch (Exception ex)
            {
                //MessageBox.Show("---" + ex.ToString());
            }
           

        }



    

        private void refreshToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            GridLoad(); holidaycateload(); 
            companyload(); combocompcode.Select();
            
            usercheck(Class.Users.HCompcode, Class.Users.HUserName, Class.Users.ScreenName); companyload();
        }

        

        private void Searchs_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(tabPagedel2);
          
        }

        public void Deletes()
        {
            try
            {
                if (txtmenperid.Text != "")
                {
                    var confirmation = MessageBox.Show("Do You want  Delete this Record ?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (confirmation == DialogResult.Yes)
                    {

                      
                        string del1 = "delete from asptblmenper where compcode='" + combocompcode.SelectedValue + "'  and username='" + combouser.SelectedValue + "' and  Asptblmenperid='" + txtmenperid.Text + "'";
                        Utility.ExecuteNonQuery(del1);
                        string del = "delete from asptblmenperdet where   Asptblmenperid='" + txtmenperid.Text + "'";
                        Utility.ExecuteNonQuery(del);
                        MessageBox.Show("Record Deleted Successfully " + txtmenperid.Text, " Delete Message ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        GridLoad(); empty();
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Deletes_Click: " + ex.Message, " Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    
       
   
        private void combocompcode_SelectedIndexChanged(object sender, EventArgs e)
        {
            userload(Convert.ToInt64("0"+combocompcode.SelectedValue));
        }
        bool valid = false;
        //private void txtvechileno_TextChanged(object sender, EventArgs e)
        //{
        //    try
        //    {

        //        Models.Validate va = new Models.Validate();
        //        valid = va.IsStringNumberic(txtvechileno.Text);
        //        if (valid == false)
        //        {
        //            MessageBox.Show("Special Charecters not allowed", "Informmation", MessageBoxButtons.OK, MessageBoxIcon.Error); return;
        //            txtvechileno.Select();
        //            return;
        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("txtvechileno_TextChanged" + ex.Message);
        //    }
        //}

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabControl1.TabPages["tabPagedel1"])//your specific tabname
            {

                //   combocompcode_SelectedIndexChanged(sender, e);
                Class.Users.LoginTime = 300;
                combocompcode.Select();

            }
            if (tabControl1.SelectedTab == tabControl1.TabPages["tabPagedel2"])//your specific tabname
            {

                //   combocompcode_SelectedIndexChanged(sender, e);
                Class.Users.LoginTime = 300;
                combocompcode.Select();

            }
        }

     
      


       

    

        //private void txtbags_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    e.Handled = !(char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back);

        //}

        //private void txtsecondweight_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (e.KeyChar >= '0' && e.KeyChar <= '9' || e.KeyChar == '.' || e.KeyChar == (char)Keys.Back) //The  character represents a backspace
        //    {
        //        e.Handled = false; //Do not reject the input
        //    }
        //    else
        //    {
        //        e.Handled = true; //Reject the input
        //    }
        //}

    
        //private void txtvechileno_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    e.Handled = !(char.IsLetterOrDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back);
        //}

     

       

        private void combocompcode_SelectedIndexChanged_1(object sender, EventArgs e)
        {
           
        }
        private void dataGridView1_CellValuePushed(object sender, DataGridViewCellValueEventArgs e)
        {

        }
        string day1 = "";
        string month1 = "";
        string year1 = "";
        int ind = 0; string griddelrow = "";
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                
                if (e.ColumnIndex == 7)
                {
                    if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null) {

                    }
                    else
                    {
                        from_datedtp.Value = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                    }
                    from_datedtp.Visible = true; to_datedtp.Visible = false;               
                  
                    rectangle = dataGridView1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                    from_datedtp.Size = new Size(rectangle.Width, rectangle.Height);
                    from_datedtp.Location = new Point(rectangle.X, rectangle.Y);
                    ind = e.RowIndex;
                }
                if (e.ColumnIndex == 8)
                {
                    from_datedtp.Visible = false;
                }

                if (e.ColumnIndex == 9)
                {
                    if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null)
                    {

                    }
                    else
                    {
                        to_datedtp.Value = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                    }
                    to_datedtp.Visible = true;
                    rectangle = dataGridView1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                    to_datedtp.Size = new Size(rectangle.Width, rectangle.Height);
                    to_datedtp.Location = new Point(rectangle.X, rectangle.Y);                    
                    ind = e.RowIndex; from_datedtp.Visible = false;
                }
                if (e.ColumnIndex == 10)
                {
                    to_datedtp.Visible = false;
                    from_datedtp.Visible = false;
                }

                
                if (e.ColumnIndex == 9)
                {

                    string ss = dataGridView1.Rows[e.RowIndex].Cells[8].EditedFormattedValue.ToString().Substring(0, 3);
                    string[] data = ss.Split(':');
                    if (Convert.ToInt32(data[0]) > 100)
                    {
                        MessageBox.Show("Invalid Time Formate " + dataGridView1.Rows[e.RowIndex].Cells[8].EditedFormattedValue.ToString());
                        dataGridView1.Rows[e.RowIndex].Cells[8].Selected = true; from_datedtp.Visible = false; to_datedtp.Visible = false;
                        dataGridView1.Rows[e.RowIndex].Cells[8].Value = null;
                    }



                }


                if (e.ColumnIndex == 11)
                {
                    string sss = dataGridView1.Rows[e.RowIndex].Cells[10].EditedFormattedValue.ToString().Substring(0, 3);
                    string[] data1 = sss.Split(':');
                    if (Convert.ToInt32(data1[0]) > 100)
                    {
                        MessageBox.Show("Invalid Time Formate " + dataGridView1.Rows[e.RowIndex].Cells[10].EditedFormattedValue.ToString());
                        dataGridView1.Rows[e.RowIndex].Cells[10].Selected = true; to_datedtp.Visible = false;
                        dataGridView1.Rows[e.RowIndex].Cells[10].Value = null;
                    }
                    to_datedtp.Visible = false;
                    from_datedtp.Visible = false;
                }

                if (txtmenperid.Text != "" && Convert.ToInt64("0"+dataGridView1.Rows[e.RowIndex].Cells[1].Value)>0)
                {
                    griddelrow = "";
                    griddelrow = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();

                }
                dataGridView1.BeginEdit(true); Class.Users.LoginTime = 300;
            }
            catch (Exception ex) { }
           
        }

       


        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            this.dataGridView1.Rows[e.RowIndex].Cells["SNo"].Value = (e.RowIndex + 1).ToString();
        }

        private void dataGridView1_Scroll(object sender, ScrollEventArgs e)
        {
            try
            {
               // dtp.Visible = false;
            }
            catch(Exception ex) { }
        }

        private void rowDeleteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {

                foreach (DataGridViewCell oneCell in dataGridView1.SelectedCells)
                {
                    if (oneCell.Selected)
                    {

                        if (txtmenperid.Text != "")
                        {
                            var confirmation = MessageBox.Show("Do You want Delete this Record ?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                            if (confirmation == DialogResult.Yes)
                            {
                                if (griddelrow != "")
                                {
                                    string del1 = "UPDATE  ASPTBLMENPERDET SET  ACTIVE='F'  Where  ASPTBLMENPERDETID='" + griddelrow + "'";
                                    Utility.ExecuteNonQuery(del1); Class.Users.LoginTime = 300;
                                    dataGridView1.Rows.RemoveAt(oneCell.RowIndex);
                                    griddelrow = "";
                                }
                                else
                                {
                                    dataGridView1.Rows.RemoveAt(oneCell.RowIndex);
                                }
                            }
                        }
                        else
                        {
                            dataGridView1.Rows.RemoveAt(oneCell.RowIndex);
                           
                        }
                    }
                }

            }
            catch (Exception EX)
            {
                // MessageBox.Show("dataGridView1_CellContentClick" + EX.Message.ToString());
            }
        }

       
        private void NHDayEntry_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control == true && e.KeyCode == Keys.S)
            {
                Saves();
            }
        }

        private void combomonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
        //public DateTime AddMinutes(int value);
        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 3)
                {
                    
                    string sel1 = "  SELECT A.ASPTBLCANITEMMASID,A.ITEMCODE FROM  ASPTBLCANITEMMAS A    WHERE A.ACTIVE='T'  AND A.ITEMNAME1='" + dataGridView1.Rows[e.RowIndex].Cells[3].EditedFormattedValue.ToString() + "'  ORDER BY 1";
                    DataSet ds = Utility.ExecuteSelectQuery(sel1, "ASPTBLCANITEMMAS");
                    DataTable dt1 = ds.Tables["ASPTBLCANITEMMAS"];
                    if (dt1.Rows.Count > 0)
                    {
                        dataGridView1.Rows[e.RowIndex].Cells[4].Value = dt1.Rows[0]["ASPTBLCANITEMMASID"].ToString();
                        dataGridView1.Rows[e.RowIndex].Cells[5].Value = dt1.Rows[0]["ITEMCODE"].ToString();
                        dataGridView1.Rows[e.RowIndex].Cells[7].Value = from_datedtp.Value;
                        dataGridView1.Rows[e.RowIndex].Cells[9].Value = to_datedtp.Value;
                        dataGridView1.Rows[e.RowIndex].Cells[8].Value = dateTimePicker1.Value;
                        dataGridView1.Rows[e.RowIndex].Cells[10].Value = dateTimePicker1.Value;
                        int h1, SYS = 0;
                        string m1, se1 = "";
                        DateTime dateTime = new DateTime();
                        dateTime = Convert.ToDateTime(dateTimePicker3.Value.ToString().Substring(11, 8));
                       // if (dateTime.Hour > 0)
                      //  {

                            h1 = dateTime.Hour + 2 + SYS;
                            m1 = "30";
                            se1 = "00";
                            dataGridView1.Rows[e.RowIndex].Cells[8].Value = h1 + ":" + m1 + ":" + se1;
                            dataGridView1.Rows[e.RowIndex].Cells[10].Value = h1+2 + ":" + m1 + ":" + se1;
                            to_datedtp.Visible = false; from_datedtp.Visible = false;
                       // }
                    }

                }
            }catch(Exception ex) { }
        }
        private void dataGridView1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (e.KeyChar >= '0' && e.KeyChar <= '9' || e.KeyChar == '.' || e.KeyChar == (char)Keys.Back)
            //{
            //    e.Handled = false;
                
            //}
            //else
            //{
            //    e.Handled = true;
            //}
        }
        private void combounit_SelectedIndexChanged(object sender, EventArgs e)
        {
         
        }

        private void DownLoads_Click(object sender, EventArgs e)
        {

        }


       

        public void Prints()
        {
            throw new NotImplementedException();
        }

        public void Searchs()
        {
            throw new NotImplementedException();
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

        public void Imports()
        {
            throw new NotImplementedException();
        }

        public void Pdfs()
        {
            throw new NotImplementedException();
        }

        public void ChangePasswords()
        {
            throw new NotImplementedException();
        }

        public void DownLoads()
        {
            throw new NotImplementedException();
        }

        public void ChangeSkins()
        {
            throw new NotImplementedException();
        }

        public void Logins()
        {
            throw new NotImplementedException();
        }

        public void GlobalSearchs()
        {
            throw new NotImplementedException();
        }

        public void TreeButtons()
        {
            throw new NotImplementedException();
        }

        public void Exit()
        {
            GlobalVariables.MdiPanel.Show();
            this.Hide();
            empty();
            GlobalVariables.HeaderName.Text = "";
            GlobalVariables.TabCtrl.TabPages.RemoveAt(GlobalVariables.TabCtrl.SelectedIndex);

        }

        private void combocompcode_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtmenperid.Text == "" && Convert.ToInt64("0" + combocompcode.SelectedValue) > 0)
                {

                    autonumberload();


                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
           
          if(dataGridView1.Columns[dataGridView1.CurrentCell.ColumnIndex].Name == "FROMTIME")
            {
                e.Control.KeyPress += Control_KeyPress2; ; from_datedtp.Visible = false;
            }
            if (dataGridView1.Columns[dataGridView1.CurrentCell.ColumnIndex].Name == "TOTIME")
            {
                e.Control.KeyPress += Control_KeyPress; to_datedtp.Visible = false;
            }
            if (dataGridView1.Columns[dataGridView1.CurrentCell.ColumnIndex].Name == "NOTES")
            {
                e.Control.KeyPress += Control_KeyPress1;
                to_datedtp.Visible = false;
            }
        }

        private void Control_KeyPress2(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= '0' && e.KeyChar <= '9' || e.KeyChar == ':' || e.KeyChar == (char)Keys.Back) //The  character represents a backspace
            {
                e.Handled = false; //Do not reject the input

            }
            else
            {
                e.Handled = true; //Reject the input
            }
        }

        private void Control_KeyPress1(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 'A' && e.KeyChar <= 'Z' || e.KeyChar >= 'a' && e.KeyChar <= 'z' || e.KeyChar >= '0' && e.KeyChar <= '9' || e.KeyChar == ' ' || e.KeyChar == (char)Keys.Back) //The  character represents a backspace
            {
                e.Handled = false; //Do not reject the input

            }
            else
            {
                e.Handled = true; //Reject the input
            }
        }

        private void Control_KeyPress(object sender, KeyPressEventArgs e)
        {
       
            if (e.KeyChar >= '0' && e.KeyChar <= '9' || e.KeyChar == ':' || e.KeyChar == (char)Keys.Back) //The  character represents a backspace
            {
                e.Handled = false; //Do not reject the input

            }
            else
            {
                e.Handled = true; //Reject the input
            }
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = false;
        }

        private void butdatewise_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt64("0" + combounit.SelectedValue) > 0 && Convert.ToInt64("0" + combouser.SelectedValue) > 0)
            {
                GridLoad(dateTimePicker3.Value.ToString().Substring(0, 10));

            }
        }

        private void combouser_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt64("0" + combounit.SelectedValue) > 0 && Convert.ToInt64("0" + combouser.SelectedValue) > 0)
                {
                    int j = 1;

                    Class.Users.LoginTime = 300;
                    var currentDateTime = DateTime.Now; listView1.Items.Clear();
                    var currentTimeAlone = new TimeSpan(currentDateTime.Hour, currentDateTime.Minute, currentDateTime.Second);
                    string sel0 = "SELECT A.ASPTBLMENPERID,A.ASPTBLMENPER1ID,A.DOCID,B.COMPCODE,B.COMPNAME,C.USERNAME, A.ACTIVE,A.FROMDATE  FROM  ASPTBLMENPER A    JOIN GTCOMPMAST B ON B.GTCOMPMASTID=A.COMPCODE JOIN ASPTBLUSERMAS C ON C.COMPCODE=A.COMPCODE  AND C.USERID=A.USERNAME  WHERE  B.GTCOMPMASTID='" + combounit.SelectedValue + "'  AND C.USERID='" + combouser.SelectedValue + "'     ORDER BY A.ASPTBLMENPERID DESC";
                    DataSet ds0 = Utility.ExecuteSelectQuery(sel0, "asptblmenper");
                    DataTable dt0 = ds0.Tables["asptblmenper"];

                    foreach (DataRow myRow in dt0.Rows)
                    {
                        ListViewItem list = new ListViewItem();
                        list.SubItems.Add(j.ToString());
                        list.SubItems.Add(myRow["ASPTBLMENPERID"].ToString());
                        list.SubItems.Add(myRow["ASPTBLMENPER1ID"].ToString());
                        list.SubItems.Add(myRow["DOCID"].ToString());
                        list.SubItems.Add(myRow["COMPCODE"].ToString());
                        list.SubItems.Add(myRow["USERNAME"].ToString());
                        list.SubItems.Add(myRow["FROMDATE"].ToString());
                        list.SubItems.Add(myRow["ACTIVE"].ToString());
                        if (i % 2 == 0)
                        {
                            list.BackColor = Color.White;

                        }
                        else
                        {
                            list.BackColor = Color.WhiteSmoke;

                        }
                        listView1.Items.Add(list); i++; j++;
                    }
                }
            }
            catch (Exception ex) { }
        }

          private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                string checkdata = "";
                if (e.ColumnIndex == 8 && e.Value != null)
                {
                  
                    checkdata = e.Value.ToString().Substring(0, 2);
                    if (Convert.ToInt32(checkdata) < 10) {
                        string ss = checkdata;
                    }
                    else
                    {
                        checkdata = "";
                        checkdata = e.Value.ToString().Substring(0, 6);
                        e.Value = DateTime.ParseExact(Convert.ToInt32(checkdata).ToString(), "HHmmss", CultureInfo.InvariantCulture).ToString("HH:mm:ss");
                    }
                    e.FormattingApplied = true;



                }
                if (e.ColumnIndex == 10 && e.Value != null)
                {
                    checkdata = e.Value.ToString().Substring(0, 2);
                    if (Convert.ToInt32(checkdata) < 10)
                    {
                        string ss = checkdata;
                    }
                    else
                    {
                        checkdata = "";
                        checkdata = e.Value.ToString().Substring(0, 6);
                        e.Value = DateTime.ParseExact(Convert.ToInt32(checkdata).ToString(), "HHmmss", CultureInfo.InvariantCulture).ToString("HH:mm:ss");
                    }
                    e.FormattingApplied = true;

                }

            }
            catch(Exception ex) { }
        }

        private void refreshToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.txtmenperid.Enabled = true; txtmenperid.Text = ""; txtmenper1id.Text = "";

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ItemnameLoad();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            ListView1_ItemActivate(sender, e);
            txtmenperid.Text = ""; txtmenper1id.Text = "";txtdocid.Text = "";
        }
    }
}
