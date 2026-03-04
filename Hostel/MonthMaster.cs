using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Pinnacle.Hostel
{
    public partial class MonthMaster : Form,ToolStripAccess
    {
        private static MonthMaster _instance;
        public static MonthMaster Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new MonthMaster();
                return _instance;
            }
        }
        public MonthMaster()
        {
            InitializeComponent();
            usercheck(Class.Users.HCompcode, Class.Users.HUserName, Class.Users.ScreenName);
          
            Class.Users.IPADDRESS = GenFun.GetLocalIPAddress();
            Class.Users.CREATED = Convert.ToDateTime(System.DateTime.Now.ToString("dd-MMM-yyyy") + " " + System.DateTime.Now.ToLongTimeString());
            Class.Users.SysDate = Convert.ToString(System.DateTime.Now.ToString("dd-MM-yyyy"));
            Class.Users.SysTime = Convert.ToString(DateTime.Now.ToLongTimeString().ToString());
            combofinyear.Text = System.DateTime.Now.Year.ToString();
            butheader.BackColor = Class.Users.BackColors;
            this.BackColor = Class.Users.BackColors;
            panel8.BackColor = Class.Users.BackColors;
            panel3.BackColor = Class.Users.BackColors;
        }
        private string griddelrow="";
        decimal listview2totalweight = 0;
        Models.Validate va = new Models.Validate();
        Models.Master mas = new Models.Master();
        Models.UserRights sm = new Models.UserRights();
   
       
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
            GridLoad();
            companyload();
            companyloadListview();
          
            txtsearch.Select(); dateTimePicker1.Value = System.DateTime.Now.AddDays(0);
        }
        public void News()
        {
            empty();
            GridLoad();
       
            autonumberload();


        }

      

        public void companyload()
        {
            try
            {
                string sel = "select a.gtcompmastid,a.compcode, a.compname from  gtcompmast a  where a.active='T' and a.ptransaction='COMPANY' order by 2";
                DataSet ds = Utility.ExecuteSelectQuery(sel, "gtcompmast");
                DataTable dt = ds.Tables["gtcompmast"];

                combocompcode.DisplayMember = "compcode";
                combocompcode.ValueMember = "gtcompmastid";
                combocompcode.DataSource = dt;
              
                combocompname.DisplayMember = "compname";
                combocompname.ValueMember = "gtcompmastid";
                combocompname.DataSource = dt;

                combocodereport.DisplayMember = "compcode";
                combocodereport.ValueMember = "gtcompmastid";
                combocodereport.DataSource = dt;


                

                combocompcode.SelectedIndex = -1; combocompcode.Text = "";
                combocompname.SelectedIndex = -1; combocompname.Text = "";


            }
            catch (Exception ex)
            {
                MessageBox.Show("companyload: " + ex.Message, " Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        public void companyloadListview()
        {
            try
            {
                string sel = "select distinct a.gtcompmastid,a.compcode, a.compname from  gtcompmast a join hrdayshf b on a.gtcompmastid=b.compcode order by 2";
                DataSet ds = Utility.ExecuteSelectQuery(sel, "gtcompmast");
                DataTable dt = ds.Tables["gtcompmast"];


                combocompcode1.DisplayMember = "compcode";
                combocompcode1.ValueMember = "gtcompmastid";
                combocompcode1.DataSource = dt;


            }
            catch (Exception ex)
            {
                MessageBox.Show("companyload: " + ex.Message, " Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        public void autonumberload()
        {
            string sel1 = "select b.gtcompmastid, b.compname from  gtcompmast b  where b.ptransaction='COMPANY'  and b.compcode='" + combocompcode.Text + "' ";
            DataSet ds1 = Utility.ExecuteSelectQuery(sel1, "gtcompmast");
            DataTable dt1 = ds1.Tables["gtcompmast"];
            combocompname.DisplayMember = "compname";
            combocompname.ValueMember = "gtcompmastid";
            combocompname.DataSource = dt1;
            //try
            //{
            //    string sel = "select max(a.hrdayshfid1)+1 as id,b.compname from hrdayshf a join gtcompmast b on a.compcode=b.gtcompmastid  where b.ptransaction='COMPANY'  and a.finyear='" + combofinyear.Text + "' and b.compcode='" + combocompcode.Text + "' group by B.COMPNAME";
            //    DataSet ds = Utility.ExecuteSelectQuery(sel, "hrdayshf");
            //    DataTable dt = ds.Tables["hrdayshf"];
            //    int cnt = Convert.ToInt32("0" + dt.Rows.Count);
            //    if (dt.Rows[0]["id"].ToString() == "")
            //    {
            //        string sel1 = "select b.gtcompmastid, b.compname from  gtcompmast b  where b.ptransaction='COMPANY'  and b.compcode='" + combocompcode.Text + "' ";
            //        DataSet ds1 = Utility.ExecuteSelectQuery(sel1, "gtcompmast");
            //        DataTable dt1 = ds1.Tables["gtcompmast"];
            //        combocompname.DisplayMember = "compname";
            //        combocompname.ValueMember = "gtcompmastid";
            //        combocompname.DataSource = dt1;
            //        txtdocid.Text = combocompcode.Text + "/" + Class.Users.Finyear + "/" + 1;
            //        txthrdayshfid1.Text = "1";

            //    }
            //    else
            //    {

            //        txtdocid.Text = combocompcode.Text + "/" + Class.Users.Finyear + "/" + dt.Rows[0]["id"].ToString();
            //        txthrdayshfid1.Text = dt.Rows[0]["id"].ToString();
            //        combocompname.DisplayMember = "compname";
            //        combocompname.ValueMember = "gtcompmastid";
            //        combocompname.DataSource = dt;
            //    }
            //}
            //catch (Exception ex)
            //{
            //  //  MessageBox.Show("autonumberload: " + ex.Message, " Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }




        public void Saves()
        {
            Int64 maxid = 0; Int64 j = 0;

            try
            {
                if (combocompcode.Text == "")
                {
                    MessageBox.Show("CompCode is Empty." + combocompcode.Text, " Success Message ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    combocompcode.Select();
                    return;

                }
                if (combomonth.Text == "")
                {
                    MessageBox.Show("combomonth is Empty." + combomonth.Text, " Success Message ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    combomonth.Select();
                    return;

                }
                if (combofinyear.Text == "")
                {
                    MessageBox.Show("'combofinyear Field is Empty'  Empty not Allowed  ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.combofinyear.Select();

                    return;
                }
                if (combofinyear.Text != "" && combocompcode.Text != "" && combomonth.Text != "")
                {
                    Models.DayShuffleModel c1 = new Models.DayShuffleModel();
                    c1.active = "";
                    Models.Validate va = new Models.Validate();

                    c1.hrdayshfid = Convert.ToInt64("0"+txthrdayshfid.Text);
                    c1.finyear = Convert.ToString(combofinyear.Text);
                    c1.compcode = Convert.ToInt64("0" + combocompcode.SelectedValue);
                    c1.compname = Convert.ToInt64("0" + combocompcode.SelectedValue);
                    c1.docid = Convert.ToString(txtdocid.Text);
                    c1.date =Convert.ToDateTime(dateTimePicker1.Value.ToString("dd-MM-yyyy")).ToString("dd-MM-yyyy");
                    c1.month = combomonth.Text;
                    if (checkactive.Checked == true)
                        c1.active = "T";
                    else
                        c1.active = "F";

                    c1.compcode1 = Convert.ToInt64(Class.Users.COMPCODE);
                    c1.username = Convert.ToInt64(Class.Users.USERID);
                    c1.createdby = Convert.ToString(Class.Users.HUserName);
                    c1.createdon = Convert.ToDateTime(System.DateTime.Now.ToLongTimeString());
                    c1.modifiedby = Class.Users.HUserName;
                    c1.ipAddress = Class.Users.IPADDRESS;

                        string sel = "select hrdayshfid    from  hrdayshf   WHERE  hrdayshfid1='" + c1.hrdayshfid1 + "' and finyear='" + c1.finyear + "' and compcode='" + c1.compcode + "' and compname='" + c1.compname + "' and docid='" + c1.docid + "'  and date1='" + c1.date + "' and month='" + c1.month + "'and active='" + c1.active + "'";
                        DataSet ds = Utility.ExecuteSelectQuery(sel, "hrdayshf");
                        DataTable dt = ds.Tables["hrdayshf"];
                        if (dt.Rows.Count != 0)
                        {

                        }
                        else if (dt.Rows.Count != 0 && Convert.ToInt64("0" + txthrdayshfid.Text) == 0 || Convert.ToInt64("0" + txthrdayshfid.Text) == 0)
                        {
                            string ins = "insert into hrdayshf(hrdayshfid1,finyear,compcode,compname,docid,date1,month,active,compcode1,username,createdby,createdon,modifiedby,ipaddress) values('" + c1.hrdayshfid1 + "','" + c1.finyear + "','" + c1.compcode + "','" + c1.compcode + "','" + c1.docid + "',to_date('" + c1.date + "','dd-MM-yyyy'),'" + c1.month + "','" + c1.active + "','" + Class.Users.COMPCODE + "','" + Class.Users.USERID + "','" + Class.Users.HUserName + "','" + dateTimePicker1.Value.ToString() + "','" + Class.Users.CREATED + "','" + Class.Users.IPADDRESS + "')";
                            Utility.ExecuteNonQuery(ins);
                            string sel2 = "select max(hrdayshfid) as hrdayshfid   from  hrdayshf   WHERE  finyear='" + c1.finyear + "' and compcode='" + c1.compcode + "' ";
                            DataSet ds2 = Utility.ExecuteSelectQuery(sel2, "hrdayshf");
                            DataTable dt2 = ds2.Tables["hrdayshf"]; maxid = 0;
                            maxid = Convert.ToInt64(dt2.Rows[0]["hrdayshfid"].ToString());
                        }
                        else
                        {
                            string up = "update  hrdayshf  set hrdayshfid1='" + c1.hrdayshfid1 + "' , finyear='" + c1.finyear + "' , compcode='" + c1.compcode + "' , compname='" + c1.compcode + "' , docid='" + c1.docid + "'  , date1=to_date('" + c1.date + "','dd-MM-yyyy'), month='" + c1.month + "',active='" + c1.active + "', compcode1='" + Class.Users.COMPCODE + "',  username='" + Class.Users.USERID + "',createdby='" + Class.Users.HUserName + "', modifiedby='" + Class.Users.CREATED + "',ipaddress='" + Class.Users.IPADDRESS + "' where hrdayshfid='" + c1.hrdayshfid + "'";
                            Utility.ExecuteNonQuery(up);
                            maxid = 0;
                            maxid = Convert.ToInt64(txthrdayshfid.Text);

                        }
                        int i = 0;
                        Models.DayShuffleDetailModel c = new Models.DayShuffleDetailModel();
                        if (txthrdayshfid.Text == "")
                        {
                            int cc = dataGridView2.Rows.Count - 1;
                        if (cc >= 1)
                        {
                            foreach (DataGridViewRow row in dataGridView1.Rows)
                            {
                                if (Convert.ToString(row.Cells["fromdate"].Value) != "" && Convert.ToString(row.Cells["todate"].Value) != "")
                                {

                                    if (txthrdayshfid.Text == "") { c.hrdayshfid = Convert.ToInt64("0" + maxid); c.hrdayshfid1 = Convert.ToInt64("0" + txthrdayshfid1.Text); }
                                    else { c.hrdayshfid = Convert.ToInt64("0" + txthrdayshfid.Text); c.hrdayshfid1 = Convert.ToInt64("0" + txthrdayshfid1.Text); }
                                    c.hrdayshfid = Convert.ToInt64(maxid);
                                    c.hrdayshfid1 = Convert.ToInt64(maxid);
                                    c.compcode = Convert.ToInt64(combocompcode.SelectedValue);     
                                    c.fromdate = Convert.ToString(row.Cells["fromdate"].Value.ToString());
                                    c.todate = Convert.ToString(row.Cells["todate"].Value.ToString());
                                    c.notes = Convert.ToString(row.Cells["notes"].Value);

                                    string sel1 = "select hrdayshfdetid    from  hrdayshfDet   where  hrdayshfid='" + c.hrdayshfid + "'and hrdayshfid1='" + c.hrdayshfid1 + "'and compcode='" + c.compcode + "' and  fromdate=to_date('" + c.fromdate + "','dd-MM-yyyy') and todate=to_date('" + c.todate + "','dd-MM-yyyy') and notes='" + c.notes + "'";
                                    DataSet ds1 = Utility.ExecuteSelectQuery(sel1, "hrdayshfDet");
                                    DataTable dt1 = ds1.Tables["hrdayshfDet"];
                                    if (dt1.Rows.Count != 0)
                                    {
                                        tabControl1.SelectTab(tabPagedel2);
                                    }
                                    else if (dt1.Rows.Count != 0 && txthrdayshfid.Text == "" || Convert.ToInt64("0" + c.hrdayshfdetid) == 0)
                                    {

                                        string ins1 = "insert into hrdayshfDet(hrdayshfid,hrdayshfid1,compcode,fromdate,todate,notes) values('" + c.hrdayshfid + "' ,'" + c.hrdayshfid1 + "' ,'" + c.compcode + "' ,to_date('" + c.fromdate + "','dd-MM-yyyy'),to_date('" + c.todate + "','dd-MM-yyyy'),'" + c.notes + "' )";
                                        Utility.ExecuteNonQuery(ins1);
                                    }
                                    else
                                    {
                                        string up1 = "update  hrdayshfDet  set hrdayshfid='" + c.hrdayshfid + "',hrdayshfid1='" + c.hrdayshfid1 + "',compcode='" + c.compcode + "',fromdate=to_date('" + c.fromdate + "','dd-MM-yyyy'),todate=to_date('" + c.todate + "','dd-MM-yyyy'),notes='" + c.notes + "'  where hrdayshfdetid='" + c.hrdayshfdetid + "'";
                                        Utility.ExecuteNonQuery(up1);
                                    }

                                }

                            }
                        }
                        }
                        if (txthrdayshfid.Text != "")
                        {

                            foreach (DataGridViewRow row in dataGridView1.Rows)
                            {
                                if (Convert.ToString(row.Cells["fromdate"].Value) != "" && Convert.ToString(row.Cells["todate"].Value) != "")
                                {
                                    if (txthrdayshfid.Text == "") { c.hrdayshfid = Convert.ToInt64("0" + maxid); c.hrdayshfid1 = Convert.ToInt64("0" + txthrdayshfid1.Text); }
                                    else { c.hrdayshfid = Convert.ToInt64("0" + txthrdayshfid.Text); c.hrdayshfid1 = Convert.ToInt64("0" + txthrdayshfid1.Text); }
                                    c.hrdayshfdetid = Convert.ToInt64("0" + row.Cells["hrdayshfdetid"].Value);
                                    c.compcode = Class.Users.COMPCODE;
                                    c.fromdate = Convert.ToString(row.Cells["fromdate"].Value.ToString());
                                     c.todate = Convert.ToString(row.Cells["todate"].Value.ToString());
                                      c.notes = Convert.ToString(row.Cells["notes"].Value);
                                    string sel1 = "select hrdayshfdetid    from  hrdayshfDet   where  hrdayshfid='" + c.hrdayshfid + "'and hrdayshfid1='" + c.hrdayshfid1 + "'and compcode='" + c.compcode + "' and  fromdate=to_date('" + c.fromdate + "','dd-MM-yyyy') and todate=to_date('" + c.todate + "','dd-MM-yyyy') and notes='" + c.notes + "'";
                                    DataSet ds1 = Utility.ExecuteSelectQuery(sel1, "hrdayshfDet");
                                    DataTable dt1 = ds1.Tables["hrdayshfDet"];
                                    if (dt1.Rows.Count != 0)
                                    {
                                        tabControl1.SelectTab(tabPagedel2);
                                    }
                                    else if (dt1.Rows.Count != 0 && Convert.ToInt64("0" + c.hrdayshfdetid) == 0 || Convert.ToInt64("0" + c.hrdayshfdetid) == 0)
                                    {

                                        string ins1 = "insert into hrdayshfDet(hrdayshfid,hrdayshfid1,compcode,fromdate,todate,notes) values('" + c1.hrdayshfid + "' ,'" + c1.hrdayshfid1 + "' ,'" + c1.compcode + "' ,to_date('" + c.fromdate + "','dd-MM-yyyy'),to_date('" + c.todate + "','dd-MM-yyyy'),'" + c.notes + "' )";
                                        Utility.ExecuteNonQuery(ins1);
                                    }
                                    else
                                    {
                                        string up1 = "update  hrdayshfDet  set hrdayshfid='" + c1.hrdayshfid + "',hrdayshfid1='" + c1.hrdayshfid1 + "',compcode='" + c1.compcode + "' , fromdate=to_date('" + c.fromdate + "','dd-MM-yyyy') , todate=to_date('" + c.todate + "','dd-MM-yyyy'),notes='" + c.notes + "'  where hrdayshfdetid='" + c.hrdayshfdetid + "'";
                                        Utility.ExecuteNonQuery(up1);
                                    }
                                }
                            }
                        }
                        if (txthrdayshfid.Text == "")
                        {
                            MessageBox.Show("Record Saved Successfully " + txtdocid.Text, " Success Message ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        News();

                        }
                        else
                        {
                            MessageBox.Show("Record Updated Successfully " + txtdocid.Text, " Update Message ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        News();

                        }
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Saves_Click " + "        " + ex.ToString(), "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void RawMaterialEntry_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }

       

        private void empty()
        {
            txthrdayshfid.Text = ""; txthrdayshfid1.Text = ""; combocompcode.SelectedIndex = -1; combocompcode.Text = "";
            combomonth.Text = ""; combomonth.SelectedIndex = -1;
            combofinyear.Text = System.DateTime.Now.Year.ToString();
            butheader.BackColor = Class.Users.BackColors;
            this.BackColor = Class.Users.BackColors;
            panel8.BackColor = Class.Users.BackColors;
            panel3.BackColor = Class.Users.BackColors;
            combocompname.SelectedIndex = -1; combocompname.Text = "";           
            dateTimePicker1.Value = System.DateTime.Now;
            txtdocid.Text = "";
            dataGridView1.AllowUserToAddRows = true;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Class.Users.BackColors;
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView1.EnableHeadersVisualStyles = false;
          
           //foreach(DataGridViewRow row in dataGridView1.Rows)
           // {
           //     row.Cells["fromdate"].Value = "";
           //     row.Cells["todate"].Value = "";
           //     row.Cells["notes"].Value = "";

           // }
            while (1 <= dataGridView1.Rows.Count - 1)
            {
                dataGridView1.Rows.RemoveAt(dataGridView1.Rows.Count - 2);
            }
            while (1 < dataGridView2.Rows.Count - 1)
            {
                dataGridView2.Rows.RemoveAt(dataGridView2.Rows.Count - 2);
            }
            if (listView1.Items.Count > 0)
            {
                listView1.Items[0].Selected = true;
            }
        }
        public void GridLoad()
        {
            try
            {
                listView1.Items.Clear(); listfilter.Items.Clear(); //date1_format('" + date1TimePicker3.Value.ToString("yyyy-MM-dd") + "', '%Y-%m-%d')
                string sel1 = "select a.hrdayshfid,a.docid,a.finyear,c.compcode,a.date1,a.month, a.active from hrdayshf a   join gtcompmast c on a.compname=c.gtcompmastid  order by 1";
                DataSet ds = Utility.ExecuteSelectQuery(sel1, "hrdayshf");
                DataTable dt = ds.Tables["hrdayshf"];
                if (dt.Rows.Count > 0)
                {
                    int i = 1;
                    foreach (DataRow myRow in dt.Rows)
                    {
                        ListViewItem list = new ListViewItem();
                        list.SubItems.Add(i.ToString());
                        list.SubItems.Add(myRow["hrdayshfid"].ToString());
                        list.SubItems.Add(myRow["docid"].ToString());
                        list.SubItems.Add(myRow["finyear"].ToString());
                        list.SubItems.Add(myRow["compcode"].ToString());
                        list.SubItems.Add(myRow["date1"].ToString());
                        list.SubItems.Add(myRow["month"].ToString());
                        list.SubItems.Add(myRow["active"].ToString());
                        this.listfilter.Items.Add((ListViewItem)list.Clone());
                        listView1.Items.Add(list);
                        i++;
                    }
                    lbltotal.Text = "Total Count: " + listView1.Items.Count;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("GridLoad: " + ex.Message, " Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ListView1_ItemActivate(object sender, EventArgs e)
        {
            try
            {
                
                if (listView1.Items.Count > 0)
                {

                    txthrdayshfid.Text = listView1.SelectedItems[0].SubItems[2].Text;
                    string sel1 = "select a.hrdayshfid, a.hrdayshfid1,a.finyear,c.gtcompmastid as compcode,c.compname,a.docid,a.date1,a.month, a.active from hrdayshf a  join gtcompmast c on a.compname=c.gtcompmastid  where a.hrdayshfid='" + txthrdayshfid.Text + "'";
                    DataSet ds = Utility.ExecuteSelectQuery(sel1, "hrdayshf");
                    DataTable dt = ds.Tables["hrdayshf"];
                    int i = 1;
                    if (dt.Rows.Count > 0)
                    {
                        txthrdayshfid.Text = Convert.ToString(dt.Rows[0]["hrdayshfid"].ToString());
                        txthrdayshfid1.Text = Convert.ToString(dt.Rows[0]["hrdayshfid1"].ToString());
                        combofinyear.Text = Convert.ToString(dt.Rows[0]["finyear"].ToString());
                        combocompcode.SelectedValue = Convert.ToString(dt.Rows[0]["compcode"].ToString());
                        combocompname.Text = Convert.ToString(dt.Rows[0]["compname"].ToString());
                        txtdocid.Text = Convert.ToString(dt.Rows[0]["docid"].ToString());
                        dateTimePicker1.Text = Convert.ToString(dt.Rows[0]["date1"].ToString());
                        combomonth.Text = Convert.ToString(dt.Rows[0]["month"].ToString());
                        if (dt.Rows[0]["active"].ToString() == "T") { checkactive.Checked = true; } else { checkactive.Checked = false; }
                        string sel2 = "select a.hrdayshfdetid, a.hrdayshfid,a.hrdayshfid1,a.compcode,to_date(a.fromdate) as fromdate,to_date(a.todate) as todate,a.notes from hrdayshfDet a join hrdayshf b on a.hrdayshfid = b.hrdayshfid join gtcompmast d on a.compcode = d.gtcompmastid where a.hrdayshfid='" + txthrdayshfid.Text + "'";
                        DataSet ds2 = Utility.ExecuteSelectQuery(sel2, "hrdayshfDet");
                        DataTable dt1 = ds2.Tables["hrdayshfDet"];
                        if (dt1.Rows.Count > 0)
                        {
                           

                            for (i = 0; i < dt1.Rows.Count; i++)
                            {
                                dataGridView1.Rows.Add();
                               
                                    dataGridView1.Rows[i].Cells[1].Value = Convert.ToInt64("0" + dt1.Rows[i]["hrdayshfdetid"].ToString());
                                    dataGridView1.Rows[i].Cells[2].Value = Convert.ToInt64("0" + dt1.Rows[i]["hrdayshfid"].ToString());
                                    dataGridView1.Rows[i].Cells[3].Value = Convert.ToInt64("0" + dt1.Rows[i]["hrdayshfid1"].ToString());
                                    dataGridView1.Rows[i].Cells[4].Value = Convert.ToInt64("0" + dt1.Rows[i]["compcode"].ToString());
                                    dataGridView1.Rows[i].Cells[5].Value = Convert.ToDateTime(dt1.Rows[i]["fromdate"].ToString()).ToString("dd-MM-yyyy");
                                    dataGridView1.Rows[i].Cells[6].Value = Convert.ToDateTime(dt1.Rows[i]["todate"].ToString()).ToString("dd-MM-yyyy");
                                dataGridView1.Rows[i].Cells[7].Value = dt1.Rows[i]["notes"].ToString();

                                
                            }


                        }
                        tabControl1.SelectTab(tabPagedel1);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ListView1_ItemActivate: " + ex.Message, " Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        if (listfilter.Items[item0].SubItems[5].ToString().Contains(txtsearch.Text) || listfilter.Items[item0].SubItems[7].ToString().Contains(txtsearch.Text))
                        {


                            list.Text = listfilter.Items[item0].SubItems[0].Text;
                            list.SubItems.Add(listfilter.Items[item0].SubItems[1].Text);
                            list.SubItems.Add(listfilter.Items[item0].SubItems[2].Text);
                            list.SubItems.Add(listfilter.Items[item0].SubItems[3].Text);
                            list.SubItems.Add(listfilter.Items[item0].SubItems[4].Text);
                            list.SubItems.Add(listfilter.Items[item0].SubItems[5].Text);
                            list.SubItems.Add(listfilter.Items[item0].SubItems[6].Text);
                            list.SubItems.Add(listfilter.Items[item0].SubItems[7].Text);
                            listView1.Items.Add(list);


                        }
                        item0++;
                    }
                    lbltotal.Text = "Total Count: " + listView1.Items.Count;
                }
                //else
                //{
                //    ListView ll = new ListView();
                //    listView1.Items.Clear();

                //    foreach (ListViewItem item in listfilter.Items)
                //    {

                //        this.listView1.Items.Add((ListViewItem)item.Clone());

                //        item0++;
                //    }
                //    lbltotal.Text = "Total Count: " + listView1.Items.Count;
                //}


            }
            catch (Exception ex)
            {
                //MessageBox.Show("---" + ex.ToString());
            }


        }



        private void RefreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
        }

        private void ContextMenuStrip2_Click(object sender, EventArgs e)
        {
           
        }

        private void refreshToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            GridLoad();
            companyload(); combocompcode.Select();

            usercheck(Class.Users.HCompcode, Class.Users.HUserName, Class.Users.ScreenName); companyload();
        }



        private void Searchs_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(tabPagedel2);

        }

        private void Deletes_Click(object sender, EventArgs e)
        {
            try
            {
                if (txthrdayshfid.Text != "")
                {
                    var confirmation = MessageBox.Show("Do You want Delete this Record ?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (confirmation == DialogResult.Yes)
                    {


                        string del1 = "delete from hrdayshf where compcode='" + combocompcode.SelectedValue + "' and  hrdayshfid='" + txthrdayshfid.Text + "';";
                        Utility.ExecuteNonQuery(del1);
                        string del = "delete from hrdayshfDet where compcode='" + combocompcode.SelectedValue + "' and  hrdayshfdetid='" + txthrdayshfid.Text + "';";
                        Utility.ExecuteNonQuery(del);
                        MessageBox.Show("Record Deleted Successfully " + txthrdayshfid.Text, " Delete Message ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        GridLoad(); empty();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Deletes_Click: " + ex.Message, " Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Prints_Click(object sender, EventArgs e)
        {

        }






        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabControl1.TabPages["tabPagedel1"])//your specific tabname
            {

                combocompcode_SelectedIndexChanged(sender,e);


            }
            if (tabControl1.SelectedTab == tabControl1.TabPages["tabPagedel2"])//your specific tabname
            {
                txtsearch.Select();
                while (1 <= dataGridView1.Rows.Count - 1)
                {
                    dataGridView1.Rows.RemoveAt(dataGridView1.Rows.Count - 2);
                }
            }
        }


    


       
      
        //private void txtfirstweight_KeyPress(object sender, KeyPressEventArgs e)
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





        private void combocompcode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txthrdayshfid.Text == "")
            {
                autonumberload();
            }

        }



        // on text change of dtp, assign back to cell


        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            this.dataGridView1.Rows[e.RowIndex].Cells["SNo"].Value = (e.RowIndex + 1).ToString();
        }


        private void rowDeleteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {

                foreach (DataGridViewCell oneCell in dataGridView1.SelectedCells)
                {
                    if (oneCell.Selected)
                    {

                        if (txthrdayshfid.Text != "")
                        {
                            var confirmation = MessageBox.Show("Do You want Delete this Record ?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                            if (confirmation == DialogResult.Yes)
                            {
                                if (griddelrow != "")
                                {
                                    string del1 = "delete from  hrdayshfDet     Where  hrdayshfdetid='" + griddelrow + "';";
                                    Utility.ExecuteNonQuery(del1);

                                    griddelrow = "";
                                }
                                else
                                {
                                    dataGridView1.Rows.RemoveAt(oneCell.RowIndex);
                                }
                            }
                        }
                    }
                }

            }
            catch (Exception EX)
            {
                // MessageBox.Show("dataGridView1_CellContentClick" + EX.Message.ToString());
            }

        }
        public DataTable ReadExcel(string fileName, string fileExt)
        {
            string conn = string.Empty;
            DataTable dtexcel = new DataTable();
            if (fileExt.CompareTo(".xls") == 0)
                conn = @"provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName + ";Extended Properties='Excel 8.0;HRD=Yes;IMEX=1';"; //for below excel 2007  
            else
                conn = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Extended Properties='Excel 12.0;HDR=NO';"; //for above excel 2007  
            using (System.Data.OleDb.OleDbConnection con = new OleDbConnection(conn))
            {
                try
                {
                    System.Data.OleDb.OleDbDataAdapter oleAdpt = new OleDbDataAdapter("select * from [Sheet1$]", con); //here we read data from sheet1  
                    oleAdpt.Fill(dtexcel); //fill excel data into dataTable  
                }
                catch { }
            }
            return dtexcel;
        }

        private void DownLoads_Click(object sender, EventArgs e)
        {
            string filePath = string.Empty;
            string fileExt = string.Empty;
            OpenFileDialog file = new OpenFileDialog(); //open dialog to choose file  
            if (file.ShowDialog() == System.Windows.Forms.DialogResult.OK) //if there is a file choosen by the user  
            {
                filePath = file.FileName; //get the path of the file  
                fileExt = Path.GetExtension(filePath); //get the file extension  
                if (fileExt.CompareTo(".xls") == 0 || fileExt.CompareTo(".xlsx") == 0)
                {
                    try
                    {
                        DataTable dtExcel = new DataTable();
                        dtExcel = ReadExcel(filePath, fileExt); //read excel file  
                        dataGridView2.Visible = true;
                        dataGridView2.DataSource = dtExcel;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                }
                else
                {
                    MessageBox.Show("Please choose .xls or .xlsx file only.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error); //custom messageBox to show error  
                }
            }
             }

     

        private void fromdatedateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

       
        private void todatedateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void rowDeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                foreach (DataGridViewCell oneCell in dataGridView1.SelectedCells)
                {
                    if (oneCell.Selected)
                    {

                        if (txthrdayshfid.Text != "")
                        {
                            var confirmation = MessageBox.Show("Do You want Delete this Record ?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                            if (confirmation == DialogResult.Yes)
                            {
                                if (griddelrow != "")
                                {
                                    string del1 = "delete from  hrdayshfDet     Where  hrdayshfDetid='" + griddelrow + "';";
                                    Utility.ExecuteNonQuery(del1);
                                    dataGridView1.Rows.RemoveAt(Convert.ToInt32(griddelrow));
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //if(txthrdayshfid.Text != "")
            //{
            //    griddelrow = "";
            //    griddelrow = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            //}
        }

        private void refreshToolStripMenuItem_Click_2(object sender, EventArgs e)
        {
            usercheck(Class.Users.HCompcode, Class.Users.HUserName, Class.Users.ScreenName);
            companyload();
        }
       

        private void combocompcode1_SelectedIndexChanged(object sender, EventArgs e)
        {
            

        }

        private void comboPayPeriod1_SelectedIndexChanged(object sender, EventArgs e)
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

        public void Deletes()
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
            throw new NotImplementedException();
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }








        //private void rowDeleteToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (listView2.Items.Count > 0)
        //        {
        //            var confirmation = MessageBox.Show("Do You want Delete this Record ?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

        //            if (confirmation == DialogResult.Yes)
        //            {
        //                int i = 0; decimal d1 = Convert.ToDecimal("0" + txtproductweight.Text);
        //                decimal d2 = 0, d3 = 0;
        //                for (i = 0; i < listView2.Items.Count; i++)
        //                {

        //                    if (listView2.Items[i].Selected)
        //                    {
        //                        if (Convert.ToInt64("0" + listView2.Items[i].SubItems[2].Text) > 1)
        //                        {
        //                            string del1 = "delete from ASPTBLNHDAYDET where hrdayshfdetid='" + listView2.Items[i].SubItems[2].Text + "';";
        //                            Utility.ExecuteNonQuery(del1);
        //                        }
        //                        MessageBox.Show("Index of Row:   " + listView2.Items[i].SubItems[1].Text + "      Name:  " + listView2.Items[i].SubItems[5].Text + "Deleted Successfully", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                        d2 = Convert.ToDecimal("0" + listView2.Items[i].SubItems[7].Text);
        //                        d3 = d1 - d2;

        //                        listView2.Items[i].Remove();
        //                        i--;
        //                    }
        //                    txtproductweight.Text = d3.ToString();
        //                    lbltotalfooterkgs.Text = d3.ToString();
        //                    listview2totalweight = Convert.ToDecimal("0" + d3.ToString()); comboproduct.SelectedIndex = -1;
        //                }
        //                if (listView2.Items.Count == 0)
        //                {
        //                    comboproduct.SelectedIndex = -1;
        //                    txtproductweight.Text = ""; txtdifference.Text = ""; lbltotalfooterkgs.Text = ""; lbltotal1.Text = "Total Count: ";
        //                }


        //            }
        //        }
        //        else
        //        {
        //            MessageBox.Show("If you want to Remove,Double Click a Specific Row in ListView.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString());
        //    }
        //    //try
        //    //{
        //    //    if (listView2.Items.Count >= 0)
        //    //    {
        //    //        var confirmation = MessageBox.Show("Do You want Delete this Record ?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

        //    //        if (confirmation == DialogResult.Yes)
        //    //        {
        //    //            int i = 0;
        //    //            for (i = 0; i < listView2.Items.Count; i++)
        //    //            {

        //    //                if (listView2.Items[i].Selected)
        //    //                {
        //    //                    if (Convert.ToInt64("0" + listView2.Items[i].SubItems[2].Text) > 1)
        //    //                    {
        //    //                        string del1 = "delete from ASPTBLNHDAYDET where hrdayshfdetid='" + listView2.Items[i].SubItems[2].Text + "';";
        //    //                        Utility.ExecuteNonQuery(del1);
        //    //                    }
        //    //                    MessageBox.Show("S.No:   " + listView2.Items[i].SubItems[1].Text + "      Row ID:  " + listView2.Items[i].SubItems[2].Text, "Delete this Record");
        //    //                    listView2.Items[i].Remove();
        //    //                    i--;
        //    //                }
        //    //            }
        //    //        }
        //    //    }
        //    //    else
        //    //    {
        //    //        MessageBox.Show("If you want to Remove,Double Click a Specific Row in ListView.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        //    //    }
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    MessageBox.Show(ex.ToString());
        //    //}
        //}



    }
}
