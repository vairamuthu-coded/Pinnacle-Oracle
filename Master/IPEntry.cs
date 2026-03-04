using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using Pinnacle.Models;
namespace Pinnacle.Master
{
    public partial class IPEntry : Form, ToolStripAccess
    {
        private static IPEntry _instance;
        public IPEntry()
        {
            InitializeComponent();
            Class.Users.IPADDRESS = GenFun.GetLocalIPAddress();
            GlobalVariables.HeaderName.Text = Class.Users.ScreenName;  Class.Users.UserTime = 0;
            butheader.BackColor = Class.Users.BackColors;
            panel3.BackColor = Class.Users.BackColors;
            panel2.BackColor = Class.Users.BackColors;
            butfooter.BackColor = Class.Users.BackColors;
        }

      

        public static IPEntry Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new IPEntry();
                GlobalVariables.CurrentForm = _instance;
                return _instance;
            }
        }
        Models.Master mas = new Models.Master(); ListView listfilter = new ListView();
        Models.UserRights sm = new Models.UserRights();
        private void MachineIPEntry_Load(object sender, EventArgs e)
        {
            GlobalVariables.HeaderName.Text = Class.Users.ScreenName;
            
        }
        void LoadUser()
        {
            string sel = "SELECT 0 GTCOMPMASTID, 'ALL' COMPCODE FROM DUAL UNION select  DISTINCT A.GTCOMPMASTID, a.compcode  from  gtcompmast a join asptblusermas b on a.gtcompmastid = b.compcode     order by 1 ";
            DataSet ds = Utility.ExecuteSelectQuery(sel, "gtcompmast");
            DataTable dt = ds.Tables["gtcompmast"];
            string sel1 = "SELECT 0 GTCOMPMASTID, '' COMPCODE FROM DUAL UNION select  DISTINCT A.GTCOMPMASTID, a.compcode  from  gtcompmast a join asptblusermas b on a.gtcompmastid = b.compcode     order by 1 ";
            DataSet ds1=Utility.ExecuteSelectQuery(sel1, "gtcompmast");
            DataTable dt1 = ds1.Tables["gtcompmast"];

            combocompcode.DisplayMember = "COMPCODE";
            combocompcode.ValueMember = "GTCOMPMASTID";
            combocompcode.DataSource = dt;

            combo_compcode.DisplayMember = "COMPCODE";
            combo_compcode.ValueMember = "GTCOMPMASTID";
            combo_compcode.DataSource = dt1;
        }
        void LoadUser(string s)
        {
            try
            {
                listView1.Items.Clear(); listfilter.Items.Clear(); int i = 1;
                DataTable dt = new DataTable();
               
                    string sel1 = "SELECT  A.ASPTBLMACIPID, A.MACIP ,A.MACNO,A.MTYPE,A.MTYPE2,b.compcode  , A.ACTIVE    FROM  ASPTBLMACIP   A join gtcompmast b on A.COMPCODE=B.GTCOMPMASTID where b.compcode='" + s.Trim()+ "' order by 2";
                    DataSet ds = Utility.ExecuteSelectQuery(sel1, "ASPTBLMACIP");
                    dt = ds.Tables["ASPTBLMACIP"];
                
                if (dt.Rows.Count > 0)
                {

                    foreach (DataRow myRow in dt.Rows)
                    {
                        ListViewItem list = new ListViewItem();
                        list.SubItems.Add(i.ToString());
                        list.SubItems.Add(myRow["ASPTBLMACIPID"].ToString());
                        list.SubItems.Add(myRow["MACIP"].ToString());
                        list.SubItems.Add(myRow["MACNO"].ToString());
                        list.SubItems.Add(myRow["MTYPE"].ToString());
                        list.SubItems.Add(myRow["MTYPE2"].ToString());
                        list.SubItems.Add(myRow["compcode"].ToString());
                        list.SubItems.Add(myRow["ACTIVE"].ToString());
                        if (i % 2 == 0)
                        {
                            list.BackColor = Color.White;

                        }
                        else
                        {
                            list.BackColor = Color.WhiteSmoke;

                        }
                        listView1.Items.Add(list);
                        this.listfilter.Items.Add((ListViewItem)list.Clone());
                        i++;
                    }
                    lbltotal.Text = "Total Count    :" + listView1.Items.Count;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private bool ValidateContact(TextBox s)
        {
            if (!Regex.Match(s.Text, "^[0-9.][0-9.]*$").Success)
            {

                s.Text.Remove(s.Text.Length - 1);

            }
            return true;
        }

       public void Saves() { 
            try
            {
                if (txtMACIP.Text != "" && txtmachineno.Text != "" && combomactype.Text != "" && combomactype2.Text != "" && Convert.ToInt64("0"+combo_compcode.SelectedValue)>0)
                {



                    string chk = "";
                    if (checkactive.Checked == true) { chk = "T"; } else { chk = "F"; checkactive.Checked = false; }
                    string sel = "select ASPTBLMACIPID    from  ASPTBLMACIP    WHERE MACIP='" + txtMACIP.Text + "'   and active='" + chk + "' AND  MACNO='" + txtmachineno.Text + "' AND MTYPE='" + combomactype.Text + "' AND MTYPE2='" + combomactype2.Text + "' AND  compcode='" + combo_compcode.SelectedValue + "'  ";
                    DataSet ds = Utility.ExecuteSelectQuery(sel, "ASPTBLMACIP");
                    DataTable dt = ds.Tables["ASPTBLMACIP"];
                    if (dt.Rows.Count != 0)
                    {
                        MessageBox.Show("Child Record Found " + " Alert " + txtMACIP.Text, "Alert", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); empty();
                    }
                    else if (dt.Rows.Count != 0 && Convert.ToInt32("0" + txtMACIPid.Text) == 0 || Convert.ToInt32("0" + txtMACIPid.Text) == 0)
                    {
                        string ins = "insert into ASPTBLMACIP(MACIP,ACTIVE,CREATEDON,MODIFIEDON,IPADDRESS,MACNO,MTYPE,MTYPE2,compcode,username)  VALUES('" + txtMACIP.Text.ToUpper() + "','" + chk + "','" + Class.Users.HUserName + "','" + System.DateTime.Now.ToString() + "','" + Class.Users.IPADDRESS + "','" + txtmachineno.Text + "','" + combomactype.Text + "','" + combomactype2.Text + "' ,'" + combo_compcode.SelectedValue + "','" + Class.Users.HUserName + "' )";
                        Utility.ExecuteNonQuery(ins);
                        MessageBox.Show("Record Saved Successfully " + txtMACIP.Text, " Success Message ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        GridLoad(); empty();
                    }
                    else
                    {
                        string up = "update  ASPTBLMACIP  set   MACIP='" + txtMACIP.Text.ToUpper() + "' ,  active='" + chk + "' , modifiedon='" + System.DateTime.Now.ToString() + "',ipaddress='" + Class.Users.IPADDRESS + "', MACNO='" + txtmachineno.Text + "',MTYPE='" + combomactype.Text + "',MTYPE2='" + combomactype2.Text + "'  ,  compcode='" + combo_compcode.SelectedValue + "' where ASPTBLMACIPid='" + txtMACIPid.Text + "'";
                        Utility.ExecuteNonQuery(up);
                        MessageBox.Show("Record Updated Successfully " + txtMACIP.Text, " Update Message ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        GridLoad();
                        empty();
                    }


                }
                else
                {
                    MessageBox.Show("Pls select all Field " + "        ", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("MACIP " + "        " + ex.ToString(), "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void MachineIPEntry_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }



        public void News()
        {
           
            empty(); GridLoad();
            try
            {
                //DataTable dt = mas.findcomcode(Class.Users.HCompcode, Class.Users.HUserName);
                //if (dt.Rows.Count > 0)
                //{
                //    combo_compcode.DisplayMember = "COMPCODE";
                //    combo_compcode.ValueMember = "GTCOMPMASTID";
                //    combo_compcode.DataSource = dt;

                //}


                //combo_compcode.SelectedIndex = -1;
                LoadUser();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Data Source Not Connected" + ex.Message);
            }
        }
        private void empty()
        {
            txtMACIPid.Text = "";
            txtMACIP.Text = "";
            butheader.BackColor = Class.Users.BackColors;
            panel3.BackColor = Class.Users.BackColors;
            panel2.BackColor = Class.Users.BackColors;
            butfooter.BackColor = Class.Users.BackColors;

            //combo_compcode.Text = "";combouser.Text = "";
            //combomactype.Text = "";combomactype2.Text = "";txtmachineno.Text = "";
        }
        public void GridLoad()
        {
            try
            {
                listView1.Items.Clear(); listfilter.Items.Clear(); int i = 1;
                DataTable dt = new DataTable();
              
               
                    string sel1 = "SELECT  A.ASPTBLMACIPID, A.MACIP ,A.MACNO,A.MTYPE,A.MTYPE2,b.compcode  , A.ACTIVE    FROM  ASPTBLMACIP   A join gtcompmast b on A.COMPCODE=B.GTCOMPMASTID where b.compcode='" + combocompcode.Text + "'  order by 2";
                    DataSet ds = Utility.ExecuteSelectQuery(sel1, "ASPTBLMACIP");
                    dt = ds.Tables["ASPTBLMACIP"];
              
                if (dt.Rows.Count > 0)
                {

                    foreach (DataRow myRow in dt.Rows)
                    {
                        ListViewItem list = new ListViewItem();
                        list.SubItems.Add(i.ToString());
                        list.SubItems.Add(myRow["ASPTBLMACIPID"].ToString());
                        list.SubItems.Add(myRow["MACIP"].ToString());
                        list.SubItems.Add(myRow["MACNO"].ToString());
                        list.SubItems.Add(myRow["MTYPE"].ToString());
                        list.SubItems.Add(myRow["MTYPE2"].ToString());
                        list.SubItems.Add(myRow["compcode"].ToString());
                        list.SubItems.Add(myRow["ACTIVE"].ToString());
                        if (i % 2 == 0)
                        {
                            list.BackColor = Color.White;

                        }
                        else
                        {
                            list.BackColor = Color.WhiteSmoke;

                        }
                        listView1.Items.Add(list);
                        this.listfilter.Items.Add((ListViewItem)list.Clone());
                        i++;
                    }
                    lbltotal.Text = "Total Count    :" + listView1.Items.Count;
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

                    txtMACIPid.Text = listView1.SelectedItems[0].SubItems[2].Text;
                    string sel1 = " SELECT  A.ASPTBLMACIPID, A.MACIP ,A.MACNO,A.MTYPE,A.MTYPE2 ,b.compcode, A.ACTIVE    FROM  ASPTBLMACIP   A  join gtcompmast b on a.compcode=b.gtcompmastid  where a.ASPTBLMACIPID=" + txtMACIPid.Text;
                    DataSet ds = Utility.ExecuteSelectQuery(sel1, "ASPTBLMACIP");
                    DataTable dt = ds.Tables["ASPTBLMACIP"];

                    if (dt.Rows.Count > 0)
                    {
                        txtMACIPid.Text = Convert.ToString(dt.Rows[0]["ASPTBLMACIPID"].ToString());
                        txtMACIP.Text = Convert.ToString(dt.Rows[0]["MACIP"].ToString());                    
                        txtmachineno.Text = Convert.ToString(dt.Rows[0]["MACNO"].ToString());
                        combomactype.Text = Convert.ToString(dt.Rows[0]["MTYPE"].ToString());
                        combomactype2.Text = Convert.ToString(dt.Rows[0]["MTYPE2"].ToString());
                        combo_compcode.Text = Convert.ToString(dt.Rows[0]["compcode"].ToString());
                        if (dt.Rows[0]["active"].ToString() == "T") { checkactive.Checked = true; } else { checkactive.Checked = true; checkactive.Checked = false; }


                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            txtMACIP.Focus();
        }
        private void comboUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dt; Class.Users.UserTime = 0;
              
                if (combocompcode.Text != "")
                {
                    int i = 1;
                    if (combocompcode.Text == "ALL" && comboUser.Text == "ALL")
                    {
                        string sel1 = "SELECT  A.ASPTBLMACIPID, A.MACIP ,A.MACNO,A.MTYPE,A.MTYPE2,b.compcode  , A.ACTIVE    FROM  ASPTBLMACIP   A join gtcompmast b on A.COMPCODE=B.GTCOMPMASTID order by 2,6";
                        DataSet ds = Utility.ExecuteSelectQuery(sel1, "ASPTBLMACIP");
                        dt = ds.Tables["ASPTBLMACIP"];
                    }
                    else
                    {
                        string sel1 = "SELECT  A.ASPTBLMACIPID, A.MACIP ,A.MACNO,A.MTYPE,A.MTYPE2,b.compcode  , A.ACTIVE    FROM  ASPTBLMACIP   A join gtcompmast b on A.COMPCODE=B.GTCOMPMASTID  JOIN ASPTBLUSERMAS C ON C.COMPCODE=B.GTCOMPMASTID AND C.COMPCODE=A.COMPCODE   where b.compcode='" + combocompcode.Text + "' and  C.USERNAME='" + comboUser.Text + "' order by 2";
                        DataSet ds = Utility.ExecuteSelectQuery(sel1, "ASPTBLMACIP");
                        dt = ds.Tables["ASPTBLMACIP"];
                    }
                    if (dt.Rows.Count > 0)
                    {
                        listView1.Items.Clear();
                       
                        foreach (DataRow myRow in dt.Rows)
                        {
                            ListViewItem list = new ListViewItem();
                            list.SubItems.Add(i.ToString());
                            list.SubItems.Add(myRow["ASPTBLMACIPid"].ToString());
                            list.SubItems.Add(myRow["MACIP"].ToString());
                            list.SubItems.Add(myRow["MACNO"].ToString());
                            list.SubItems.Add(myRow["MTYPE"].ToString());
                            list.SubItems.Add(myRow["MTYPE2"].ToString());
                            list.SubItems.Add(myRow["compcode"].ToString());
                            list.SubItems.Add(myRow["ACTIVE"].ToString());
                            if (i % 2 == 0)
                            {
                                list.BackColor = Color.White;

                            }
                            else
                            {
                                list.BackColor = Color.WhiteSmoke;

                            }
                            listView1.Items.Add(list);
                            // this.listfilter.Items.Add((ListViewItem)list.Clone());
                            i++;
                        }
                        lbltotal.Text = "Total Count    :" + listView1.Items.Count;
                    }
                    else
                    {
                        listView1.Items.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Txtsearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtsearch.Text.ToUpper() != "")
                {
                    listView1.Items.Clear(); int iGLCount = 1;
                    string sel1 = " SELECT  A.ASPTBLMACIPID, A.MACIP ,A.MACNO,A.MTYPE,A.MTYPE2 ,b.compcode, A.ACTIVE    FROM  ASPTBLMACIP   A  join gtcompmast b on a.compcode=b.gtcompmastid  where a.MACIP LIKE'%" + txtsearch.Text.ToUpper() + "%' ORDER BY 2,6 ";
                    DataSet ds = Utility.ExecuteSelectQuery(sel1, "ASPTBLMACIP");
                    DataTable dt = ds.Tables["ASPTBLMACIP"];
                    if (dt.Rows.Count > 0)
                    {

                        foreach (DataRow myRow in dt.Rows)
                        {
                            ListViewItem list = new ListViewItem();
                            list.SubItems.Add(iGLCount.ToString());
                            list.SubItems.Add(myRow["ASPTBLMACIPID"].ToString());
                            list.SubItems.Add(myRow["MACIP"].ToString());
                            list.SubItems.Add(myRow["MACNO"].ToString());
                            list.SubItems.Add(myRow["MTYPE"].ToString());
                            list.SubItems.Add(myRow["MTYPE2"].ToString());
                            list.SubItems.Add(myRow["compcode"].ToString());
                            list.SubItems.Add(myRow["ACTIVE"].ToString());
                            listView1.Items.Add(list);
                            iGLCount++;
                        }
                        lbltotal.Text = "Total Count    :" + listView1.Items.Count;
                    }
                    else
                    {
                        listView1.Items.Clear();
                    }
                }
                else
                {

                    listView1.Items.Clear();
                    GridLoad();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        public void Prints()
        {
           // 
        }

        public void Searchs()
        {
            //
        }

        public void Deletes()
        {

            if (txtMACIPid.Text != "")
            {
                DialogResult result = MessageBox.Show("Do You want to Delete Record : ", "Delete", MessageBoxButtons.OKCancel);
                if (result.Equals(DialogResult.OK))
                {
                    string sel1 = "select a.ASPTBLMACIPID from ASPTBLMACIP a  where a.ASPTBLMACIPID='" + txtMACIPid.Text + "'";
                    DataSet ds = Utility.ExecuteSelectQuery(sel1, "ASPTBLMACIP");
                    DataTable dt = ds.Tables["ASPTBLMACIP"];
                    if (dt.Rows.Count > 0)
                    {

                        string del = "delete from ASPTBLMACIP where ASPTBLMACIPID='" + Convert.ToInt64("0" + txtMACIPid.Text) + "'";
                        Utility.ExecuteNonQuery(del);
                        MessageBox.Show("Record Deleted Successfully " + txtMACIP.Text, " Delete Message ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        GridLoad(); empty();
                    }
                }
            }
        }

        public void Imports()
        {
            
        }

        public void Pdfs()
        {
           // 
        }

        public void ChangePasswords()
        {
           // 
        }

        public void DownLoads()
        {
           // 
        }

        public void ChangeSkins()
        {
          //  
        }

        public void Logins()
        {
           // 
        }

        public void GlobalSearchs()
        {
           // 
        }

        public void TreeButtons()
        {
          //  
        }

        public void Exit()
        {
            GlobalVariables.MdiPanel.Show();
            News();
            GlobalVariables.HeaderName.Text = "";
            GlobalVariables.TabCtrl.TabPages.RemoveAt(GlobalVariables.TabCtrl.SelectedIndex);
            this.Hide();
        }

        public void ReadOnlys()
        {
            throw new NotImplementedException();
        }

        private void combo_compcode_SelectedIndexChanged(object sender, EventArgs e)
        {
            ////try
            ////{
            ////    if (combo_compcode.SelectedIndex >= 0)
            ////    {

            ////        Int64 s = Convert.ToInt64(combo_compcode.SelectedValue);
            ////        DataTable dt1 = mas.comcode1(s);
            ////        if (dt1.Rows.Count > 0)
            ////        {
            ////            combouser.DisplayMember = "USERNAME";
            ////            combouser.ValueMember = "userid";
            ////            combouser.DataSource = dt1;

            ////        }
            ////        combouser.SelectedIndex = -1;
            ////    }
            ////}
            ////catch (Exception ex)
            ////{
            ////    MessageBox.Show(ex.Message.ToString());
            ////}
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void combo_compcode_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void tabControl1_Click(object sender, EventArgs e)
        {
            Class.Users.UserTime = 0;
        }

        private void lblsearch_Click(object sender, EventArgs e)
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

        private void combocompcode_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadUser(combocompcode.Text);
        }
    }
}
