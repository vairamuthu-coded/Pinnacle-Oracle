using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pinnacle.Master
{
    public partial class CompanyMaster : Form, ToolStripAccess
    {
        private static CompanyMaster _instance;
        Models.Master mas = new Models.Master();
        Models.UserRights sm = new Models.UserRights();
        ListView listfilter = new ListView();
     
        public static CompanyMaster Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new CompanyMaster();
                GlobalVariables.CurrentForm = _instance; return _instance;
            }
        }
        public CompanyMaster()
        {
            InitializeComponent();
            usercheck(Class.Users.HCompcode, Class.Users.HUserName, Class.Users.ScreenName);
            Class.Users.IPADDRESS = GenFun.GetLocalIPAddress();
            Class.Users.CREATED = Convert.ToDateTime(System.DateTime.Now.ToString("dd-MMM-yyyy") + " " + System.DateTime.Now.ToLongTimeString());
            Class.Users.SysDate = Convert.ToString(System.DateTime.Now.ToString("dd/MM/yyyy"));
            Class.Users.SysTime = Convert.ToString(DateTime.Now.ToLongTimeString().ToString());
            this.BackColor = Class.Users.BackColors;
            butheader.BackColor = Class.Users.BackColors;
            panel2.BackColor = Class.Users.BackColors;
            panel3.BackColor = Class.Users.BackColors;
        }
     
        public void usercheck(string s, string ss, string sss)
        {

            DataTable dt1 = sm.headerdropdowns(s, ss, sss);
            if (dt1.Rows.Count > 0)
            {
                if (dt1.Rows[0]["Menuname"].ToString() == Class.Users.ScreenName)
                {
                    //for (int r = 0; r < dt1.Rows.Count; r++)
                    //{

                    //    if (dt1.Rows[r]["NEWS"].ToString() == "T") { this.News.Visible = true; } else { this.News.Visible = false; }
                    //    if (dt1.Rows[r]["SAVES"].ToString() == "T") { this.Saves.Visible = true; } else { this.Saves.Visible = false; }
                    //    if (dt1.Rows[r]["PRINTS"].ToString() == "T") { this.Prints.Visible = true; } else { this.Prints.Visible = false; }
                    //    if (dt1.Rows[r]["READONLY"].ToString() == "T") { this.Enabled = true; } else { this.Enabled = false; }
                    //    if (dt1.Rows[r]["SEARCH"].ToString() == "T") { this.Searchs.Visible = true; } else { this.Searchs.Visible = false; }
                    //    if (dt1.Rows[r]["DELETES"].ToString() == "T") { this.Deletes.Visible = true; } else { this.Deletes.Visible = false; }
                    //    if (dt1.Rows[r]["TREEBUTTON"].ToString() == "T") { this.TreeButtons.Visible = true; } else { this.TreeButtons.Visible = false; }
                    //    if (dt1.Rows[r]["GLOBALSEARCH"].ToString() == "T") { this.GlobalSearchs.Visible = true; } else { this.GlobalSearchs.Visible = false; }
                    //    if (dt1.Rows[r]["LOGIN"].ToString() == "T") { this.Logins.Visible = true; } else { this.Logins.Visible = false; }
                    //    if (dt1.Rows[r]["CHANGEPASSWORD"].ToString() == "T") { this.ChangePasswords.Visible = true; } else { this.ChangePasswords.Visible = false; }
                    //    if (dt1.Rows[r]["CHANGESKIN"].ToString() == "T") { ChangeSkins.Visible = true; } else { ChangeSkins.Visible = false; }
                    //    if (dt1.Rows[r]["DOWNLOAD"].ToString() == "T") { this.DownLoads.Visible = true; } else { this.DownLoads.Visible = false; }
                    //    if (dt1.Rows[r]["Pdf"].ToString() == "T") { this.Pdfs.Visible = true; } else { this.Pdfs.Visible = false; }
                    //    if (dt1.Rows[r]["Imports"].ToString() == "T") { this.Imports.Visible = true; } else { this.Imports.Visible = false; }

                    //}
                }


            }
            else
            {
                MessageBox.Show("Invalid");
            }

        }
        private void CompanyMaster_Load(object sender, EventArgs e)
        {
            News();
        }

        public void compid(string s)
        {
            string sel = "select max(a.GTCOMPMASTID1) as id  from  GTCOMPMAST a  where a.compcode='" + s.ToUpper() + "' ";
            DataSet ds = Utility.ExecuteSelectQuery(sel, "GTCOMPMAST");
            DataTable dt = ds.Tables["GTCOMPMAST"];
            Int64 partycount = 1;
            if (dt.Rows.Count >= 1)
            {
                txtcompid1.Text = "";
                Int64 cc = Convert.ToInt64("0" + dt.Rows[0]["id"].ToString()) + partycount;
                txtcompid1.Text = cc.ToString();
            }
            else
            {
                txtcompid1.Text = "1";
            }
        }
        public DataTable state(Int64 s)
        {
            string sel = "select b.GTSTATEMASTID,b.statename from  GTCITYMAST a  join GTSTATEMAST b on a.state=b.GTSTATEMASTID   join GTCOUNTRYMAST c on b.country=c.GTCOUNTRYMASTID   where b.active='T' and a.GTCITYMASTID='" + s + "' ";
            DataSet ds = Utility.ExecuteSelectQuery(sel, "GTSTATEMAST");
            DataTable dt = ds.Tables["GTSTATEMAST"];          
            return dt;
        }
        public DataTable city()
        {
            string sel = "select a.GTCITYMASTID,a.cityname from  GTCITYMAST a  join GTSTATEMAST b on a.state=b.GTSTATEMASTID   join GTCOUNTRYMAST c on b.country=c.GTCOUNTRYMASTID   where a.active='T'";
            DataSet ds = Utility.ExecuteSelectQuery(sel, "GTCITYMAST");
            DataTable dt = ds.Tables["GTCITYMAST"];
            combocity.DataSource = dt;
            combocity.DisplayMember = "cityname";
            combocity.ValueMember = "GTCITYMASTID";
            return dt;
        }
        public DataTable country(Int64 s)
        {
            string sel1 = "SELECT  b.GTCOUNTRYMASTID,b.countryname from GTSTATEMAST a join GTCOUNTRYMAST b on a.country=b.GTCOUNTRYMASTID where a.GTSTATEMASTID='" + s + "' ";
            DataSet ds = Utility.ExecuteSelectQuery(sel1, "GTSTATEMAST");
            DataTable dt = ds.Tables["GTSTATEMAST"];

            return dt;
        }
        public void Saves()
        {
            try
            {

                if (txtcompcode.Text == "")
                {
                    MessageBox.Show("'CompCode  is Empty'  Empty not Allowed  ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); this.txtcompcode.Focus();
                    return;
                }
                if (txtcompname.Text == "")
                {
                    MessageBox.Show("'CompName  is Empty'  Empty not Allowed  ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); this.txtcompname.Focus();
                    return;
                }
                if (combocity.SelectedValue == null)
                {
                    MessageBox.Show("'City Name  is Empty'  Empty not Allowed  ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); this.combocity.Focus();
                    return;
                }
                if (combostate.SelectedValue == null)
                {
                    MessageBox.Show("'State Name  is Empty'  Empty not Allowed  ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); this.combostate.Focus();
                    return;
                }
                if (combocountry.SelectedValue == null)
                {
                    MessageBox.Show("'Contry Name  is Empty'  Empty not Allowed  ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); this.combocountry.Focus(); return;
                }
                if (txtaddress.Text == "")
                {
                    MessageBox.Show("'Address  is Empty'  Empty not Allowed  ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); this.txtaddress.Focus(); return;
                }
                
                else
                {
                    Models.Validate va = new Models.Validate();
                    if (va.IsString(txtcompcode.Text) == true)
                    {
                        string chk = "", chkcustomer = ""; ; 
                        if (checkactive.Checked == true) { chk = "T"; } else { chk = "F"; checkactive.Checked = false; }
                        if (checkcustomer.Checked == true) { chkcustomer = "PARTY"; } else { chkcustomer = "COMPANY"; checkcustomer.Checked = false; }
                        string sel = "select GTCOMPMASTID    from  GTCOMPMAST   WHERE compcode='" + txtcompcode.Text + "' and compname='" + txtcompname.Text + "' and city='" + combocity.SelectedValue + "' and state='" + combostate.SelectedValue + "' and country='" + combocountry.SelectedValue + "' and address='" + txtaddress.Text + "' and pincode='" + txtpincode.Text + "' and panno='" + txtpanno.Text + "' and tinno='" + txttinno.Text + "' and gstno='" + txtgstno.Text + "' and gstdate='" + txtgstdate.Text + "' and phoneno='" + txtphoneno.Text + "' and email='" + txtemail.Text + "' and website='" + txtwebsite.Text + "'  and contactname='" + txtcontact.Text + "' and ptransaction='" + chkcustomer + "' and active='" + chk + "' and modifiedby='" + Class.Users.HUserName + "' and ipaddress='" + Class.Users.IPADDRESS + "' and GTCOMPMASTID='" + txtcompid.Text + "'";
                        DataSet ds = Utility.ExecuteSelectQuery(sel, "GTCOMPMAST");
                        DataTable dt = ds.Tables["GTCOMPMAST"];
                        if (dt.Rows.Count != 0)
                        {
                            MessageBox.Show("Child Record Found " + " Alert " + txtcompid.Text, "Alert", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); empty();
                        }
                        //////else if (dt.Rows.Count != 0 && Convert.ToInt64("0" + txtcompid.Text) == 0 || Convert.ToInt64("0" + txtcompid.Text) == 0)
                        //////{
                        //////    string ins = "insert into GTCOMPMAST(GTCOMPMASTID1,compcode,compname,city,state,country,address,pincode,panno,tinno,gstno,gstdate,phoneno,email,website,contactname,active,ptransaction,createdby,modifiedby,ipaddress)  VALUES('" + txtcompid1.Text + "','" + txtcompcode.Text.ToUpper() + "','" + txtcompname.Text.ToUpper() + "', '" + combocity.SelectedValue + "', '" + combostate.SelectedValue + "','" + combocountry.SelectedValue + "' ,'" + txtaddress.Text.ToUpper() + "','" + Convert.ToInt64("0" + txtpincode.Text).ToString() + "','" + txtpanno.Text + "','" + txttinno.Text.ToUpper() + "','" + txtgstno.Text.ToUpper() + "','" + txtgstdate.Text + "','" + txtphoneno.Text + "','" + txtemail.Text.ToUpper() + "','" + txtwebsite.Text.ToUpper() + "','" + txtcontact.Text.ToUpper() + "','" + chk + "', '" + chkcustomer + "' , '" + Class.Users.HUserName + "','" + Class.Users.HUserName + "','" + Class.Users.IPADDRESS + "')";
                        //////    Utility.ExecuteNonQuery(ins);
                        //////    MessageBox.Show("Record Saved Successfully " + txtcompid.Text, " Success Message ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //////    GridLoad(); empty();
                        //////}
                        else
                        {
                            string up = "update  GTCOMPMAST  set COMPANYLOGO=:EMPIMAGE where  GTCOMPMASTID='" + txtcompid.Text + "'";
                            Utility.ExecuteNonQuery(up, bytes);
                            MessageBox.Show("Record Updated Successfully " + txtcompid.Text, " Update Message ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            GridLoad();
                            empty();
                        }

                    }
                    else
                    {
                        MessageBox.Show("Invalid Data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;

                    }
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show("Error " + "        " + ex.ToString(), "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void CompanyMaster_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }

        public void Exit()
        {
            GlobalVariables.MdiPanel.Show();
            News();
            GlobalVariables.HeaderName.Text = "";
            GlobalVariables.TabCtrl.TabPages.RemoveAt(GlobalVariables.TabCtrl.SelectedIndex);
            this.Hide();
        }

        public void News()
        {

            GridLoad(); city(); empty();
        }
        private void empty()
        {
            txtcompid.Text = "";
            txtcompid1.Text = "";txtcompcode.Select();
            combostate.SelectedIndex = -1; combostate.Text = "";
            combocountry.SelectedIndex = -1; combocountry.Text = ""; txtsearch.Text = "";
            checkactive.Checked = true;
            checkcustomer.Checked = false;
            txtcompcode.Text = "";
            txtcompname.Text = "";
            txtaddress.Text = "";
            txtpincode.Text = "";
            txtpanno.Text = "";
            txttinno.Text = "";
            txtgstno.Text = "";
            txtgstdate.Text = "";
            txtphoneno.Text = "";
            txtemail.Text = "";
            txtwebsite.Text = "";
            txtcontact.Text = "";
            this.BackColor = Class.Users.BackColors;
            butheader.BackColor = Class.Users.BackColors;
            panel2.BackColor = Class.Users.BackColors;
            panel3.BackColor = Class.Users.BackColors;
        }
        public void GridLoad()
        {
            try
            {
                listView1.Items.Clear();listfilter.Items.Clear();
                string sel1 = "select a.GTCOMPMASTID,a.compcode,a.compname,b.cityname as city,c.statename as state,D.countryname as  country,a.address,  a.pincode,a.panno,a.tinno,  a.gstno,'' as gstdate,  a.phoneno, a.email,  a.website,  a.active,a.ptransaction from GTCOMPMAST a join GTCITYMAST b on a.city = b.GTCITYMASTID  join GTSTATEMAST c on a.state = c.GTSTATEMASTID join GTCOUNTRYMAST d on a.country = d.GTCOUNTRYMASTID order by 1";
                DataSet ds = Utility.ExecuteSelectQuery(sel1, "GTCOMPMAST");
                DataTable dt = ds.Tables["GTCOMPMAST"];
                if (dt.Rows.Count > 0)
                {
                    int i = 1;
                    foreach (DataRow myRow in dt.Rows)
                    {
                        ListViewItem list = new ListViewItem();
                        list.SubItems.Add(i.ToString());
                        list.SubItems.Add(myRow["GTCOMPMASTID"].ToString());                       
                        list.SubItems.Add(myRow["compcode"].ToString());
                        list.SubItems.Add(myRow["compname"].ToString());
                        list.SubItems.Add(myRow["city"].ToString());
                        list.SubItems.Add(myRow["state"].ToString());
                        list.SubItems.Add(myRow["active"].ToString());
                        list.SubItems.Add(myRow["ptransaction"].ToString());
                        list.SubItems.Add(myRow["pincode"].ToString());
                        list.SubItems.Add(myRow["panno"].ToString());
                        list.SubItems.Add(myRow["tinno"].ToString());
                        list.SubItems.Add(myRow["gstno"].ToString());
                        list.SubItems.Add(myRow["phoneno"].ToString());
                        listfilter.Items.Add((ListViewItem)list.Clone());
                        listView1.Items.Add(list);
                        i++;
                    }
                    lbltotal.Text = "Total Count: " + listView1.Items.Count;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void ListView1_ItemActivate(object sender, EventArgs e)
        {
            try
            {
                Class.Users.UserTime = 0;
                if (listView1.Items.Count > 0)
                {
                   
                    txtcompid.Text = listView1.SelectedItems[0].SubItems[2].Text;
                    //txtcompid1.Text = listView1.SelectedItems[0].SubItems[2].Text;

                    string sel1 = "select a.GTCOMPMASTID,a.compcode,a.compname,b.cityname as city,c.statename as state,D.countryname as  country,a.address,  a.pincode,a.panno,a.tinno,  a.gstno,'' as gstdate,  a.phoneno, a.email,  a.website,  a.active,a.ptransaction from GTCOMPMAST a join GTCITYMAST b on a.city = b.GTCITYMASTID  join GTSTATEMAST c on a.state = c.GTSTATEMASTID join GTCOUNTRYMAST d on a.country = d.GTCOUNTRYMASTID   where a.GTCOMPMASTID='" + txtcompid.Text + "'";
                    DataSet ds = Utility.ExecuteSelectQuery(sel1, "GTCOMPMAST");
                    DataTable dt = ds.Tables["GTCOMPMAST"];

                    if (dt.Rows.Count > 0)
                    {
                       // txtcompid.Text = "";
                        txtcompid.Text = Convert.ToString(dt.Rows[0]["GTCOMPMASTID"].ToString());
                        txtcompid1.Text = Convert.ToString(dt.Rows[0]["GTCOMPMASTID"].ToString());
                        txtcompcode.Text = Convert.ToString(dt.Rows[0]["compcode"].ToString());
                        txtcompname.Text = Convert.ToString(dt.Rows[0]["compname"].ToString());
                        combocity.Text = Convert.ToString(dt.Rows[0]["city"].ToString());
                        combostate.Text = Convert.ToString(dt.Rows[0]["state"].ToString());
                        combocountry.Text = Convert.ToString(dt.Rows[0]["country"].ToString());
                        txtaddress.Text = Convert.ToString(dt.Rows[0]["address"].ToString());
                        txtpincode.Text = Convert.ToString(dt.Rows[0]["pincode"].ToString());
                        txtpanno.Text = Convert.ToString(dt.Rows[0]["panno"].ToString());
                        txttinno.Text = Convert.ToString(dt.Rows[0]["tinno"].ToString());
                        txtgstno.Text = Convert.ToString(dt.Rows[0]["gstno"].ToString());
                        txtgstdate.Text = Convert.ToString(dt.Rows[0]["gstdate"].ToString());
                        txtphoneno.Text = Convert.ToString(dt.Rows[0]["phoneno"].ToString());
                        txtemail.Text = Convert.ToString(dt.Rows[0]["email"].ToString());
                        txtwebsite.Text = Convert.ToString(dt.Rows[0]["website"].ToString());                      
                       
                        if (dt.Rows[0]["active"].ToString() == "T") { checkactive.Checked = true; } else { checkactive.Checked = true; checkactive.Checked = false; }
                        if (dt.Rows[0]["ptransaction"].ToString() == "PARTY") { checkcustomer.Checked = true; } else {checkcustomer.Checked = false; }

                      
                        Combocity_SelectedIndexChanged(sender, e);
                        Combostate_SelectedIndexChanged(sender, e);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Txtsearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int item0 = 0;
                if (txtsearch.Text.Length > 0)
                {
                    listView1.Items.Clear();
                    foreach (ListViewItem item in listfilter.Items)
                    {
                        ListViewItem list = new ListViewItem();
                        if (listfilter.Items[item0].SubItems[4].ToString().Contains(txtsearch.Text))
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
                            list.SubItems.Add(listfilter.Items[item0].SubItems[10].Text);
                            list.SubItems.Add(listfilter.Items[item0].SubItems[11].Text);
                            list.SubItems.Add(listfilter.Items[item0].SubItems[12].Text);
                            list.SubItems.Add(listfilter.Items[item0].SubItems[13].Text);
                            if (item0 % 2 == 0)
                            {
                                list.BackColor = Color.White;
                            }
                            else
                            {
                                list.BackColor = Color.WhiteSmoke;
                            }
                            listView1.Items.Add(list);
                        }
                        item0++;
                    }
                }
                else
                {
                    try
                    {
                        listView1.Items.Clear(); item0 = 0;
                        foreach (ListViewItem item in listfilter.Items)
                        {
                            this.listView1.Items.Add((ListViewItem)item.Clone());
                            if (item0 % 2 == 0)
                            {
                                item.BackColor = Color.White;
                            }
                            else
                            {
                                item.BackColor = Color.WhiteSmoke;
                            }
                            item0++;
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                // MessageBox.Show("---" + ex.ToString());
            }
        }

        private void Combostate_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = country(Convert.ToInt64(combostate.SelectedValue));
                combocountry.DataSource = dt;
                combocountry.DisplayMember = "countryname";
                combocountry.ValueMember = "GTCOUNTRYMASTID";
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
            }



        }    
        private void StateRefreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Combostate_SelectedIndexChanged(sender, e);
        }

        private void Combocity_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = state(Convert.ToInt64(combocity.SelectedValue));
                combostate.DataSource = dt;
                combostate.DisplayMember = "statename";
                combostate.ValueMember = "GTSTATEMASTID";
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
            }
        }

        private void Combostate_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = country(Convert.ToInt64(combostate.SelectedValue));
                combocountry.DataSource = dt;
                combocountry.DisplayMember = "countryname";
                combocountry.ValueMember = "GTCOUNTRYMASTID";
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
            }
        }

        private void Txtcompname_TextChanged(object sender, EventArgs e)
        {

        }

        private void Txtcompcode_TextChanged(object sender, EventArgs e)
        {
            if (txtcompid.Text == "")
            {
                if (txtcompcode.Text.Length >= 3)
                {
                    compid(txtcompcode.Text);
                }
            }
        }

        public void Deletes()
        {
            if (txtcompid.Text != "")
            {
                string sel1 = "select a.GTCOMPMASTID from GTCOMPMAST a join asptblrawmaterial b on a.GTCOMPMASTID=b.compname where a.GTCOMPMASTID='" + txtcompid.Text + "'";
                DataSet ds = Utility.ExecuteSelectQuery(sel1, "GTCOMPMAST");
                DataTable dt = ds.Tables["GTCOMPMAST"];
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("Child Record Found.Can Not Delete." + txtcompcode.Text, " Alert Message ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                }
                else
                {

                    string del = "delete from GTCOMPMAST where GTCOMPMASTID='" + Convert.ToInt64("0" + txtcompid.Text) + "'";
                    Utility.ExecuteNonQuery(del);
                    MessageBox.Show("Record Deleted Successfully " + txtcompcode.Text, " Delete Message ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    GridLoad(); empty();
                }
            }
        }

        private void txtcompcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtcompname.Focus();
            }
        }

        private void txtcompname_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                combodivision.Select();
            }
        }

        private void combodivision_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                combocity.Select();
            }
        }

        private void combocity_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                combostate.Select();
            }
        }

        private void combostate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtaddress.Select();
            }
        }

        private void txtaddress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtpincode.Select();
            }
        }

        private void txtpincode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtpanno.Select();
            }
        }

        private void txtpanno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txttinno.Select();
            }
        }

        private void txttinno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtgstno.Select();
            }
        }

        private void txtgstno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtgstdate.Select();
            }
        }

        private void txtgstdate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtphoneno.Select();
            }
        }

        private void txtphoneno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtemail.Select();
            }
        }

        private void txtemail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtwebsite.Select();
            }
        }

        private void txtwebsite_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtcontact.Select();

            }
        }

        private void txtcompcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back);
        }

        private void txtcompname_KeyPress(object sender, KeyPressEventArgs e)
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

        private void tabControl1_Click(object sender, EventArgs e)
        {
            Class.Users.UserTime = 0;
        }

        public void Searchs(int id)
        {
            throw new NotImplementedException();
        }

        public void Deletes(int id)
        {
            throw new NotImplementedException();
        }
        byte[] bytes; byte[] Signbytes; string PATTH1 = ""; Int64 myString = 0;
        OpenFileDialog open = new OpenFileDialog();
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            try
            {
                bytes = null;
                PictureBox p = sender as PictureBox;
                if (p != null)
                {
                    open.Filter = "(*.jpg;*.jpeg;*.bmp;)| *.jpg; *.jpeg; *.bmp";
                    if (open.ShowDialog() == DialogResult.OK)
                    {

                        p.Image = new Bitmap(open.FileName);
                        bytes = Models.Device.ImageToByteArray(p);
                        System.Text.Encoding enc = System.Text.Encoding.ASCII;
                        myString = Convert.ToInt64("0" + bytes.Length);

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
