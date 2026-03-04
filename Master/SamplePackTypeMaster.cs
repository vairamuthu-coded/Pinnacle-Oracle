using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Pinnacle.Master
{
    public partial class SamplePackTypeMaster : Form,ToolStripAccess
    {
        private static SamplePackTypeMaster _instance;
        Models.Master mas = new Models.Master();
        Models.UserRights sm = new Models.UserRights();
        ListView listfilter = new ListView();
        public static SamplePackTypeMaster Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new SamplePackTypeMaster();

                GlobalVariables.CurrentForm = _instance; return _instance;
            }
        }
        public SamplePackTypeMaster()
        {
           
            InitializeComponent();
            usercheck(Class.Users.HCompcode, Class.Users.HUserName, Class.Users.ScreenName);
            Class.Users.IPADDRESS = GenFun.GetLocalIPAddress();
            Class.Users.CREATED = Convert.ToDateTime(System.DateTime.Now.ToString("dd-MM-yyyy") + " " + System.DateTime.Now.ToLongTimeString());
            butheader.BackColor = Class.Users.BackColors;
            panel3.BackColor = Class.Users.BackColors;
            panel2.BackColor = Class.Users.BackColors;
            this.BackColor = Class.Users.BackColors;
            Class.Users.UserTime = 0;
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

            }

        }
        private void SamplePackTypeMaster_Load(object sender, EventArgs e)
        {
            GridLoad(); empty();
        }
        private bool ValidateContact(TextBox s)
        {
            if (!Regex.Match(s.Text, "^[A-Z][a-zA-Z]*$").Success)
            {

                s.Text.Remove(s.Text.Length - 1);

            }
            return true;
        }

        public void Saves()
        {
            try
            {
                if (txtpacktype.Text != "")
                {
                  
                   

                        string chk = "";
                        if (checkactive.Checked == true) { chk = "T"; } else { chk = "F"; checkactive.Checked = false; }
                        string sel = "select ASPTBLORDPACKMASID    from  ASPTBLORDPACKMAS    WHERE ORDERPACKTYPE='" + txtpacktype.Text + "' and active='" + chk + "' ";
                        DataSet ds = Utility.ExecuteSelectQuery(sel, "ASPTBLORDPACKMAS");
                        DataTable dt = ds.Tables["ASPTBLORDPACKMAS"];
                        if (dt.Rows.Count != 0)
                        {
                            MessageBox.Show("Child Record Found " + " Alert " + txtpacktype.Text, "Alert", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); empty();
                        }
                        else if (dt.Rows.Count != 0 && Convert.ToInt32("0" + txtpacktypeid.Text) == 0 || Convert.ToInt32("0" + txtpacktypeid.Text) == 0)
                        {
                            string ins = "insert into ASPTBLORDPACKMAS(ORDERPACKTYPE,active,createdby,modifiedby,ipaddress)  VALUES('" + txtpacktype.Text.ToUpper() + "','" + chk + "','" + Class.Users.HUserName + "','" + Class.Users.HUserName + "','" + Class.Users.IPADDRESS + "' )";
                            Utility.ExecuteNonQuery(ins);
                            MessageBox.Show("Record Saved Successfully " + txtpacktype.Text, " Success Message ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            GridLoad(); empty();
                        }
                        else
                        {
                            string up = "update  ASPTBLORDPACKMAS  set   ORDERPACKTYPE='" + txtpacktype.Text.ToUpper() + "' , active='" + chk + "' , modifiedby='" + Class.Users.HUserName + "',ipaddress='" + Class.Users.IPADDRESS + "' where ASPTBLORDPACKMASID='" + txtpacktypeid.Text + "'";
                            Utility.ExecuteNonQuery(up);
                            MessageBox.Show("Record Updated Successfully " + txtpacktype.Text, " Update Message ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            GridLoad();
                            empty();
                        }


                }
                else
                {
                    MessageBox.Show("'Order PackType  is Wrong'  Empty not Allowed  ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Order PackType " + "        " + ex.ToString(), "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void SamplePackTypeMaster_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }



        public void News()
        {
            GridLoad();
            empty();
        }
        private void empty()
        {
            txtpacktypeid.Text = "";
            txtpacktype.Text = "";
                listView1.Items.Clear(); Class.Users.UserTime = 0;
            butheader.BackColor = Class.Users.BackColors;
            panel3.BackColor = Class.Users.BackColors;
            panel2.BackColor = Class.Users.BackColors;
            this.BackColor = Class.Users.BackColors;
            txtpacktype.Select();
        }
        public void GridLoad()
        {
            try
            {
                listView1.Items.Clear();
                string sel1 = " select a.ASPTBLORDPACKMASID, a.ORDERPACKTYPE , a.active  from  ASPTBLORDPACKMAS a   order by 1";
                DataSet ds = Utility.ExecuteSelectQuery(sel1, "ASPTBLORDPACKMAS");
                DataTable dt = ds.Tables["ASPTBLORDPACKMAS"];
                if (dt.Rows.Count > 0)
                {
                    int i = 1;
                    foreach (DataRow myRow in dt.Rows)
                    {
                        ListViewItem list = new ListViewItem();
                        list.Text = i.ToString();
                        list.SubItems.Add(myRow["ASPTBLORDPACKMASID"].ToString());
                        list.SubItems.Add(myRow["ORDERPACKTYPE"].ToString());
                        list.SubItems.Add(myRow["active"].ToString());
                        listView1.Items.Add(list);
                        this.listfilter.Items.Add((ListViewItem)list.Clone());
                        if (i % 2 == 0)
                        {
                            list.BackColor = Color.White;
                        }
                        else
                        {
                            list.BackColor = Color.WhiteSmoke;
                        }
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
                listView1.Items.Clear(); Class.Users.UserTime = 0;

                if (listView1.Items.Count > 0)
                {

                    txtpacktypeid.Text = listView1.SelectedItems[0].SubItems[1].Text;
                    string sel1 = " select a.ASPTBLORDPACKMASID, a.ORDERPACKTYPE , a.active    from  ASPTBLORDPACKMAS a    where a.ASPTBLORDPACKMASID=" + txtpacktypeid.Text;
                    DataSet ds = Utility.ExecuteSelectQuery(sel1, "ASPTBLORDPACKMAS");
                    DataTable dt = ds.Tables["ASPTBLORDPACKMAS"];

                    if (dt.Rows.Count > 0)
                    {
                        txtpacktypeid.Text = Convert.ToString(dt.Rows[0]["ASPTBLORDPACKMASID"].ToString());
                        txtpacktype.Text = Convert.ToString(dt.Rows[0]["ORDERPACKTYPE"].ToString());
                        if (dt.Rows[0]["active"].ToString() == "T") { checkactive.Checked = true; } else { checkactive.Checked = true; checkactive.Checked = false; }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void Search()
        {

        }

        public void Deletes()
        {
            if (txtpacktypeid.Text != "")
            {
                string sel1 = "select a.ASPTBLORDPACKMASID from ASPTBLORDPACKMAS a  where a.ASPTBLORDPACKMASID='" + txtpacktypeid.Text + "'";
                DataSet ds = Utility.ExecuteSelectQuery(sel1, "ASPTBLORDPACKMAS");
                DataTable dt = ds.Tables["ASPTBLORDPACKMAS"];
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("Child Record Found.Can Not Delete." + txtpacktype.Text, " Alert Message ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                }
                else
                {

                    string del = "delete from ASPTBLORDPACKMAS where ASPTBLORDPACKMASID='" + Convert.ToInt64("0" + txtpacktypeid.Text) + "'";
                    Utility.ExecuteNonQuery(del);
                    MessageBox.Show("Record Deleted Successfully " + txtpacktype.Text, " Delete Message ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    GridLoad(); empty();
                }
            }
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

        public void Exit()
        {
            GlobalVariables.MdiPanel.Show();
            News();
            GlobalVariables.HeaderName.Text = "";
            GlobalVariables.TabCtrl.TabPages.RemoveAt(GlobalVariables.TabCtrl.SelectedIndex);
            this.Hide();
        }

        private void txtsearch_TextChanged(object sender, EventArgs e)
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
                        if (listfilter.Items[item0].SubItems[2].ToString().Contains(txtsearch.Text))
                        {
                            list.Text = listfilter.Items[item0].SubItems[0].Text;
                            list.SubItems.Add(listfilter.Items[item0].SubItems[1].Text);
                            list.SubItems.Add(listfilter.Items[item0].SubItems[2].Text);
                            list.SubItems.Add(listfilter.Items[item0].SubItems[3].Text);
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

        private void txtpacktype_KeyPress(object sender, KeyPressEventArgs e)
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

