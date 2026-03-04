using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pinnacle.Master
{
    public partial class SampleCategoryMaster : Form,ToolStripAccess
    {
        private static SampleCategoryMaster _instance;
        Models.Master mas = new Models.Master();
        Models.UserRights sm = new Models.UserRights();
        ListView listfilter = new ListView();
        public static SampleCategoryMaster Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new SampleCategoryMaster();

                GlobalVariables.CurrentForm = _instance; return _instance;
            }
        }
        public SampleCategoryMaster()
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
        public void DepartLoad()
        {
            string sel = " SELECT A.ASPTBLSAMDEPTMASID,A.DEPARTMENT    FROM  ASPTBLSAMDEPTMAS A    WHERE A.ACTIVE='T'  ";
            DataSet ds = Utility.ExecuteSelectQuery(sel, "ASPTBLSAMDEPTMAS");
            DataTable dt = ds.Tables["ASPTBLSAMDEPTMAS"];
            combodepart.DataSource = dt;
            combodepart.DisplayMember = "DEPARTMENT";
            combodepart.ValueMember = "ASPTBLSAMDEPTMASID";

        }
        private void SampleDepartmentMaster_Load(object sender, EventArgs e)
        {
            GridLoad(); DepartLoad(); txtcategory.Select(); combodepart.SelectedIndex = -1;
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
                if (txtcategory.Text != "" && combodepart.Text != "")
                {
                  
                   

                        string chk = "";
                        if (checkactive.Checked == true) { chk = "T"; } else { chk = "F"; checkactive.Checked = false; }
                        string sel = "select ASPTBLSAMCATMASID    from  ASPTBLSAMCATMAS    WHERE DEPARTMENT='" + combodepart.SelectedValue + "' AND CATEGORY='"+ txtcategory.Text+ "' and active='" + chk + "' ";
                        DataSet ds = Utility.ExecuteSelectQuery(sel, "ASPTBLSAMCATMAS");
                        DataTable dt = ds.Tables["ASPTBLSAMCATMAS"];
                        if (dt.Rows.Count != 0)
                        {
                            MessageBox.Show("Child Record Found " + " Alert " + txtcategory.Text, "Alert", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); empty();
                        }
                        else if (dt.Rows.Count != 0 && Convert.ToInt32("0" + txtsamcatid.Text) == 0 || Convert.ToInt32("0" + txtsamcatid.Text) == 0)
                        {
                            string ins = "insert into ASPTBLSAMCATMAS(CATEGORY,DEPARTMENT,active,createdby,modifiedby,ipaddress)  VALUES('" + txtcategory.Text.ToUpper() + "','" + combodepart.SelectedValue + "','" + chk + "','" + Class.Users.HUserName + "','" + Class.Users.HUserName + "','" + Class.Users.IPADDRESS + "' )";
                            Utility.ExecuteNonQuery(ins);
                            MessageBox.Show("Record Saved Successfully " + txtcategory.Text, " Success Message ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            GridLoad(); empty();
                        }
                        else
                        {
                            string up = "update  ASPTBLSAMCATMAS  set   CATEGORY='" + txtcategory.Text.ToUpper() + "' ,DEPARTMENT='" + combodepart.SelectedValue + "', active='" + chk + "' , modifiedby='" + Class.Users.HUserName + "',ipaddress='" + Class.Users.IPADDRESS + "' where ASPTBLSAMCATMASID='" + txtsamcatid.Text + "'";
                            Utility.ExecuteNonQuery(up);
                            MessageBox.Show("Record Updated Successfully " + txtcategory.Text, " Update Message ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            GridLoad();
                            empty();
                        }

                    
                  
                }
                else
                {
                    MessageBox.Show("'Departnebt  is Wrong'  Empty not Allowed  ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Departnebt " + "        " + ex.ToString(), "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void SampleDepartmentMaster_FormClosed(object sender, FormClosedEventArgs e)
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
            txtsamcatid.Text = "";
            txtcategory.Text = "";combodepart.Text = "";
            txtcategory.Select(); Class.Users.UserTime = 0;
            butheader.BackColor = Class.Users.BackColors;
            panel3.BackColor = Class.Users.BackColors;
            panel2.BackColor = Class.Users.BackColors;
            this.BackColor = Class.Users.BackColors;
          
        }
        public void GridLoad()
        {
            try
            {
                listView1.Items.Clear();listfilter.Items.Clear();
                string sel1 = " SELECT B.ASPTBLSAMCATMASid, B.CATEGORY , A.DEPARTMENT ,A.ACTIVE  FROM  ASPTBLSAMDEPTMAS A JOIN  ASPTBLSAMCATMAS B ON B.DEPARTMENT=A.ASPTBLSAMDEPTMASID    ORDER BY 1";
                DataSet ds = Utility.ExecuteSelectQuery(sel1, "ASPTBLSAMDEPTMAS");
                DataTable dt = ds.Tables["ASPTBLSAMDEPTMAS"];
                if (dt.Rows.Count > 0)
                {
                    int i = 1;
                    foreach (DataRow myRow in dt.Rows)
                    {
                        ListViewItem list = new ListViewItem();
                        list.SubItems.Add(i.ToString());
                        list.SubItems.Add(myRow["ASPTBLSAMCATMASid"].ToString());
                        list.SubItems.Add(myRow["CATEGORY"].ToString());
                        list.SubItems.Add(myRow["DEPARTMENT"].ToString());
                        list.SubItems.Add(myRow["active"].ToString());
                        listView1.Items.Add(list);
                        listfilter.Items.Add((ListViewItem)list.Clone());
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

                    txtsamcatid.Text = listView1.SelectedItems[0].SubItems[2].Text;
                    string sel1 = "SELECT B.ASPTBLSAMCATMASID, B.CATEGORY ,A.DEPARTMENT , A.ACTIVE  FROM  ASPTBLSAMDEPTMAS A JOIN  ASPTBLSAMCATMAS B ON B.DEPARTMENT=A.ASPTBLSAMDEPTMASID        where B.ASPTBLSAMCATMASID=" + txtsamcatid.Text;
                    DataSet ds = Utility.ExecuteSelectQuery(sel1, "ASPTBLSAMDEPTMAS");
                    DataTable dt = ds.Tables["ASPTBLSAMDEPTMAS"];

                    if (dt.Rows.Count > 0)
                    {
                        txtsamcatid.Text = Convert.ToString(dt.Rows[0]["ASPTBLSAMCATMASID"].ToString());
                        txtcategory.Text = Convert.ToString(dt.Rows[0]["CATEGORY"].ToString());
                        combodepart.Text = Convert.ToString(dt.Rows[0]["DEPARTMENT"].ToString());
                        if (dt.Rows[0]["active"].ToString() == "T") { checkactive.Checked = true; } else { checkactive.Checked = true; checkactive.Checked = false; }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void Searchs()
        {
           
        }

        public void Deletes()
        {
            if (txtsamcatid.Text != "")
            {
                string sel1 = "select a.ASPTBLSAMCATMASID from ASPTBLSAMCATMAS a join ASPTBLBUYSAM b on a.ASPTBLSAMCATMASID=b.CATEGORY where a.ASPTBLSAMCATMASID='" + txtsamcatid.Text + "'";
                DataSet ds = Utility.ExecuteSelectQuery(sel1, "ASPTBLSAMCATMAS");
                DataTable dt = ds.Tables["ASPTBLSAMCATMAS"];
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("Child Record Found.Can Not Delete." + txtcategory.Text, " Alert Message ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                }
                else
                {

                    string del = "delete from ASPTBLSAMCATMAS where ASPTBLSAMCATMASID='" + Convert.ToInt64("0" + txtsamcatid.Text) + "'";
                    Utility.ExecuteNonQuery(del);
                    MessageBox.Show("Record Deleted Successfully " + txtcategory.Text, " Delete Message ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    GridLoad(); empty();
                }
            }
        }

        public void Prints()
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
                        if (item.SubItems[3].ToString().Contains(txtsearch.Text))
                        {
                            list.SubItems.Add(item.SubItems[0].Text);
                            list.SubItems.Add(item.SubItems[1].Text);
                            list.SubItems.Add(item.SubItems[2].Text);
                            list.SubItems.Add(item.SubItems[3].Text);
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
                    foreach (ListViewItem item in listfilter.Items)
                    {
                        ListViewItem list = new ListViewItem();

                        list.SubItems.Add(item.SubItems[0].Text);
                        list.SubItems.Add(item.SubItems[1].Text);
                        list.SubItems.Add(item.SubItems[2].Text);
                        list.SubItems.Add(item.SubItems[3].Text);
                        if (item0 % 2 == 0)
                        {
                            list.BackColor = Color.White;
                        }
                        else
                        {
                            list.BackColor = Color.WhiteSmoke;
                        }
                        listView1.Items.Add(list);

                        item0++;
                    }
                }
            }
            catch (Exception ex)
            {
                // MessageBox.Show("---" + ex.ToString());
            }
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DepartLoad();
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
