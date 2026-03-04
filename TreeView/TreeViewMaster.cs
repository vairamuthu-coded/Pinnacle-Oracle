using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pinnacle.TreeView
{
    public partial class TreeViewMaster : Form,ToolStripAccess
    {
        private static TreeViewMaster _instance;
        Models.Master mas = new Models.Master();
        Models.UserRights sm = new Models.UserRights();
        Models.Navigation c = new Models.Navigation();
        Models.MenuName m = new Models.MenuName();
        public static TreeViewMaster Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new TreeViewMaster();
                GlobalVariables.CurrentForm = _instance; return _instance;
            }
        }
        public void ReadOnlys()
        {

        }
        public TreeViewMaster()
        {
            InitializeComponent();
       GlobalVariables.CurrentForm = this;
            GlobalVariables.HeaderName.Text = Class.Users.ScreenName;
            butheader.Text= Class.Users.ScreenName;
            butheader.BackColor = Class.Users.BackColors;  
            butfooter.BackColor = Class.Users.BackColors;
            panel1.BackColor = Class.Users.BackColors;
            mas.DatabaseCheck(checkdatabase);
        }
        ListView listfilter = new ListView();
        public void GridLoad()
        {
            listView1.Items.Clear(); listfilter.Items.Clear();
            string sel = "SELECT  distinct ''USERRIGHTSID,A.MENUID,A.MENUNAME,A.NAVURL ,A.parentmenuid,A.ACTIVE,'' SAVES,'' PRINTS,''READONLY,''SEARCH,'' DELETES,''NEWS,''TREEBUTTON,''GLOBALSEARCH,''LOGIN, ''CHANGEPASSWORD,''CHANGESKIN,''DOWNLOAD,''CONTACT,''PDF,''IMPORTS,B.COMPCODE,C.USERNAME   FROM ASPTBLNAVIGATION A JOIN GTCOMPMAST B ON   B.gtcompmastid=A.COMPCODE   JOIN asptblusermas C ON C.USERID=A.USERNAME   JOIN ASPTBLMENUNAME D ON A.MENUNAMEID = A.MENUNAMEID AND D.MENUNAME=A.MENUNAME  AND A.ACTIVE='T' ORDER BY 2";
            DataSet ds = Utility.ExecuteSelectQuery(sel, "ASPTBLNAVIGATION");
            DataTable dt = ds.Tables["ASPTBLNAVIGATION"];
            if (dt.Rows.Count > 0)
            {
             
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                   
                    ListViewItem list = new ListViewItem();
                    list.Text = dt.Rows[i]["USERRIGHTSID"].ToString();
                    list.SubItems.Add(dt.Rows[i]["MENUID"].ToString());                   
                    list.SubItems.Add(dt.Rows[i]["MENUNAME"].ToString());
                    list.SubItems.Add(dt.Rows[i]["NAVURL"].ToString());
                    list.SubItems.Add(dt.Rows[i]["parentmenuid"].ToString());
                    list.SubItems.Add(dt.Rows[i]["ACTIVE"].ToString());
                    list.SubItems.Add(dt.Rows[i]["SAVES"].ToString());
                    list.SubItems.Add(dt.Rows[i]["PRINTS"].ToString());
                    list.SubItems.Add(dt.Rows[i]["READONLY"].ToString());
                    list.SubItems.Add(dt.Rows[i]["SEARCH"].ToString());
                    list.SubItems.Add(dt.Rows[i]["DELETES"].ToString());
                    list.SubItems.Add(dt.Rows[i]["NEWS"].ToString());
                    list.SubItems.Add(dt.Rows[i]["TREEBUTTON"].ToString());
                    list.SubItems.Add(dt.Rows[i]["GLOBALSEARCH"].ToString());
                    list.SubItems.Add(dt.Rows[i]["CHANGEPASSWORD"].ToString());
                    list.SubItems.Add(dt.Rows[i]["LOGIN"].ToString());
                    list.SubItems.Add(dt.Rows[i]["CHANGESKIN"].ToString());
                    list.SubItems.Add(dt.Rows[i]["DOWNLOAD"].ToString());
                    list.SubItems.Add(dt.Rows[i]["CONTACT"].ToString());
                    list.SubItems.Add(dt.Rows[i]["PDF"].ToString());
                    list.SubItems.Add(dt.Rows[i]["IMPORTS"].ToString());
                    list.SubItems.Add(dt.Rows[i]["COMPCODE"].ToString());
                    list.SubItems.Add(dt.Rows[i]["USERNAME"].ToString());
                    list.BackColor = i % 2 == 0 ? Class.Users.Color1 : Class.Users.Color2;
                    listfilter.Items.Add((ListViewItem)list.Clone());
                    listView1.Items.Add(list);
                  
                }
            }
        }
        private void txtsearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int item0 = 0; int i = 1;
                if (txtsearch.Text.Length > 0)
                {
                    listView1.Items.Clear();
                    foreach (ListViewItem item in listfilter.Items)
                    {
                        ListViewItem list = new ListViewItem();
                        if (item.SubItems[3].ToString().Contains(txtsearch.Text))
                        {
                            list.Text = i.ToString();
                            list.SubItems.Add(item.SubItems[2].Text);
                            list.SubItems.Add(item.SubItems[3].Text);
                            list.SubItems.Add(item.SubItems[4].Text);
                            list.SubItems.Add(item.SubItems[5].Text);
                            list.SubItems.Add(item.SubItems[6].Text);
                            list.SubItems.Add(item.SubItems[7].Text);
                            list.SubItems.Add(item.SubItems[8].Text);
                            list.SubItems.Add(item.SubItems[9].Text);
                            list.SubItems.Add(item.SubItems[10].Text);
                            list.SubItems.Add(item.SubItems[11].Text);
                            list.SubItems.Add(item.SubItems[12].Text);
                            list.SubItems.Add(item.SubItems[13].Text);
                            list.SubItems.Add(item.SubItems[14].Text);
                            list.SubItems.Add(item.SubItems[15].Text);
                            list.SubItems.Add(item.SubItems[16].Text);
                            list.SubItems.Add(item.SubItems[17].Text);
                            list.SubItems.Add(item.SubItems[18].Text);
                            list.SubItems.Add(item.SubItems[19].Text);
                            list.SubItems.Add(item.SubItems[20].Text);
                            list.SubItems.Add(item.SubItems[21].Text);
                            list.SubItems.Add(item.SubItems[22].Text);

                            list.BackColor = i % 2 == 0 ? Class.Users.Color1 : Class.Users.Color2;

                            listView1.Items.Add(list);
                        }
                        item0++;
                    }
                }
                else
                {
                    GridLoad();
                }
            }
            catch (Exception ex)
            {
                // MessageBox.Show("---" + ex.ToString());
            }
        }
        private void combocode()
        {
            DataTable dt1 = mas.comcode1();
            if (dt1.Rows.Count > 0)
            {
                combocompcode.DisplayMember = "COMPCODE";
                combocompcode.ValueMember = "gtcompmastid";
                combocompcode.DataSource = dt1;

            }
           combocompcode.SelectedIndex = -1;
        }
        private void TreeViewMaster_Load(object sender, EventArgs e)
        {
            GridLoad();
            combocode(); GlobalVariables.HeaderName.Text = Class.Users.ScreenName;
        }

        private void TreeViewMaster_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }

    


        public void Saves() { 
            try
            {
                Cursor = Cursors.WaitCursor;
                Models.UserRights c = new Models.UserRights();
                mas.DatabaseCheck(checkdatabase);
                if (combocompcode.Text !="" && combousername.Text != "" && listView2.Items.Count >= 1)
                {

                    foreach (ListViewItem item in listView2.Items)
                    {


                        c.UserRightsID = Convert.ToInt32("0" + item.SubItems[1].Text);
                        if (c.UserRightsID == 0)
                        {
                            c.MenuID = Convert.ToInt32(item.SubItems[2].Text);
                            c.MenuName = Convert.ToString(item.SubItems[3].Text);
                            c.NavURL = Convert.ToString(item.SubItems[4].Text);
                              c.ParentMenuID = Convert.ToInt32(item.SubItems[5].Text);
                            if (item.SubItems[6].Text == "T") c.Active = "T"; else c.Active = "F";
                            if (item.SubItems[7].Text == "T") c.News = "T"; else c.News = "F";
                            if (item.SubItems[8].Text == "T") c.Save = "T"; else c.Save = "F";
                            if (item.SubItems[9].Text == "T") c.Print = "T"; else c.Print = "F";
                            if (item.SubItems[10].Text == "T") c.ReadOnly = "T"; else c.ReadOnly = "F";
                            if (item.SubItems[11].Text == "T") c.Search = "T"; else c.Search = "F";
                            if (item.SubItems[12].Text == "T") c.Delete = "T"; else c.Delete = "F";
                            if (item.SubItems[13].Text == "T") c.TreeButton = "T"; else c.TreeButton = "F";
                            if (item.SubItems[14].Text == "T") c.GlobalSearch = "T"; else c.GlobalSearch = "F";
                            if (item.SubItems[15].Text == "T") c.Login = "T"; else c.Login = "F";
                            if (item.SubItems[16].Text == "T") c.ChangePassword = "T"; else c.ChangePassword = "F";
                            if (item.SubItems[17].Text == "T") c.ChangeSkin = "T"; else c.ChangeSkin = "F";
                            if (item.SubItems[18].Text == "T") c.DownLoad = "T"; else c.DownLoad = "F";
                            if (item.SubItems[19].Text == "T") c.Contact = "T"; else c.Contact = "F";
                            if (item.SubItems[20].Text == "T") c.Pdf = "T"; else c.Pdf = "F";
                            if (item.SubItems[21].Text == "T") c.Imports = "T"; else c.Imports = "F";
                            c.CompCode = Convert.ToInt64("0" + combocompcode.SelectedValue);
                            c.UserName = Convert.ToInt64("0" + combousername.SelectedValue);

                            c.IpAddress = GenFun.GetLocalIPAddress();
                            c.Createdby = Class.Users.HUserName;
                            c.Createdon = Convert.ToDateTime(System.DateTime.Now.ToString("dd-MM-yyyy") + " " + System.DateTime.Now.ToLongTimeString());
                            c.Modified = Convert.ToDateTime(System.DateTime.Now.ToString("dd-MM-yyyy") + " " + System.DateTime.Now.ToLongTimeString());
                            mas.DatabaseCheck(checkdatabase);
                            DataTable dt3 = c.select(c.MenuID, c.MenuName,   c.ParentMenuID, c.CompCode, c.UserName);
                            if (dt3.Rows.Count != 0) { }
                            else if (dt3.Rows.Count != 0 && c.UserRightsID == 0 || c.UserRightsID == 0)
                            {

                                c = new Models.UserRights(c.MenuID, c.MenuName, c.NavURL,  c.ParentMenuID, c.Active, c.News, c.Save, c.Print, c.ReadOnly, c.Search, c.Delete, c.CompCode, c.UserName, c.TreeButton, c.GlobalSearch, c.Login, c.ChangePassword, c.ChangeSkin, c.DownLoad, c.Contact, c.Pdf, c.Imports, c.Createdon, c.Modified, c.Sno, c.IpAddress);
                             
                            }
                            else
                            {
                                c = new Models.UserRights(c.MenuID, c.MenuName, c.NavURL,   c.ParentMenuID, c.Active, c.News, c.Save, c.Print, c.ReadOnly, c.Search, c.Delete, c.CompCode, c.UserName, c.TreeButton, c.GlobalSearch, c.Login, c.ChangePassword, c.ChangeSkin, c.DownLoad, c.Contact, c.Pdf, c.Imports, c.Modified, c.UserRightsID);
                              
                            }


                        }
                       
                    }
                    listView2.Items.Clear();
                    MessageBox.Show("Record Saved Successfully.  ");
                }
                else
                {
                    MessageBox.Show("pls select Compcode and UserName");
                }
            }            
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            Cursor = Cursors.Default;
        }

        private void Combocompcode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (combocompcode.SelectedIndex >=0)
                {
                    listView2.Items.Clear(); chkall.Checked = false;
                    mas.DatabaseCheck(checkdatabase);
                    Int64 s = Convert.ToInt64(combocompcode.SelectedValue);
                    DataTable dt1 = mas.comcode1(s);
                    if (dt1.Rows.Count > 0)
                    {
                        combousername.DisplayMember = "USERNAME";
                        combousername.ValueMember = "USERID";
                        combousername.DataSource = dt1;
                    }
                    combousername.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

       public void News() { 
            listView2.Items.Clear();           
            chkall.Checked = false;
            butheader.BackColor = Class.Users.BackColors;
            butfooter.BackColor = Class.Users.BackColors;
            panel1.BackColor = Class.Users.BackColors;
            listView2.ForeColor= Class.Users.BackColors; 

        }

        private void ListView1_ItemActivate(object sender, EventArgs e)
        {
            try
            {
                if (listView1.Items.Count > 0)
                {
                    ListViewItem item1 = new ListViewItem();
                    for (int c = 0; c < listView1.SelectedItems[0].SubItems.Count; c++)
                    {
                        item1.SubItems.Add(listView1.SelectedItems[0].SubItems[c].Text);      
                    }
                    listView2.Items.Add(item1);

                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void ListView2_ItemActivate(object sender, EventArgs e)
        {
            try
            {
                if (listView2.Items.Count > 0)
                {


                    int i = 0;

                    for (i = 0; i < listView2.Items.Count; i++)
                    {

                        if (listView2.Items[i].Selected)
                        {

                            listView2.Items[i].Remove();
                            i--;
                        }
                    }

                }
                else
                {
                    MessageBox.Show("pls upload data from Master IP", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message.ToString()); }

        }

        private void Chkall_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                int i = 0;
                if (chkall.Checked == true)
                {
                    listView2.Items.Clear();
                    foreach (ListViewItem item in listView1.Items)
                    {
                        item.Selected = true;
                        ListViewItem item1 = new ListViewItem();
                        for (int c = 0; c < listView1.SelectedItems[i].SubItems.Count; c++)
                        {
                           
                            item1.SubItems.Add(listView1.SelectedItems[i].SubItems[c].Text);

                        }

                        listView2.Items.Add(item1);
                        i++;
                    }
                }
                else
                {
                    foreach (ListViewItem item in listView2.Items)
                    {
                        item.Selected = false;

                    }
                    listView2.Items.Clear();
                    

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Combousername_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (combousername.SelectedIndex >= 0)
            {
                News(); mas.DatabaseCheck(checkdatabase);
                listView2.Items.Clear(); chkall.Checked = false;
                string sel = " SELECT A.USERRIGHTSID,D.MENUID,D.MENUNAME,D.NAVURL ,A.parentmenuid,A.ACTIVE,A.SAVES,A.PRINTS,A.READONLY,A.SEARCH,A.DELETES,A.NEWS,A.TREEBUTTON,A.GLOBALSEARCH,A.LOGIN,A.CHANGEPASSWORD,A.CHANGESKIN,A.DOWNLOAD,A.CONTACT,A.PDF,A.IMPORTS,B.COMPCODE,C.USERNAME FROM ASPTBLUSERRIGHTS A JOIN GTCOMPMAST B ON   B.gtcompmastid = A.COMPCODE JOIN asptblusermas C ON C.USERID = A.USERNAME JOIN ASPTBLNAVIGATION D ON D.MENUID=A.MENUID  WHERE B.COMPCODE = '" + combocompcode.Text + "' AND C.USERNAME = '" + combousername.Text + "' order by 2";
                DataSet ds = Utility.ExecuteSelectQuery(sel, "ASPTBLUSERRIGHTS");
                DataTable dt = ds.Tables["ASPTBLUSERRIGHTS"];
                if (dt.Rows.Count > 0)
                {

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        ListViewItem list = new ListViewItem();
                        list.SubItems.Add(dt.Rows[i]["USERRIGHTSID"].ToString());
                        list.SubItems.Add(dt.Rows[i]["MENUID"].ToString());                     
                        list.SubItems.Add(dt.Rows[i]["MENUNAME"].ToString());
                        list.SubItems.Add(dt.Rows[i]["NAVURL"].ToString());
                        list.SubItems.Add(dt.Rows[i]["parentmenuid"].ToString());
                        list.SubItems.Add(dt.Rows[i]["ACTIVE"].ToString());
                        list.SubItems.Add(dt.Rows[i]["SAVES"].ToString());
                        list.SubItems.Add(dt.Rows[i]["PRINTS"].ToString());
                        list.SubItems.Add(dt.Rows[i]["READONLY"].ToString());
                        list.SubItems.Add(dt.Rows[i]["SEARCH"].ToString());
                        list.SubItems.Add(dt.Rows[i]["DELETES"].ToString());
                        list.SubItems.Add(dt.Rows[i]["NEWS"].ToString());
                        list.SubItems.Add(dt.Rows[i]["TREEBUTTON"].ToString());
                        list.SubItems.Add(dt.Rows[i]["GLOBALSEARCH"].ToString());
                        list.SubItems.Add(dt.Rows[i]["CHANGEPASSWORD"].ToString());
                        list.SubItems.Add(dt.Rows[i]["LOGIN"].ToString());
                        list.SubItems.Add(dt.Rows[i]["CHANGESKIN"].ToString());
                        list.SubItems.Add(dt.Rows[i]["DOWNLOAD"].ToString());
                        list.SubItems.Add(dt.Rows[i]["CONTACT"].ToString());
                        list.SubItems.Add(dt.Rows[i]["PDF"].ToString());
                        list.SubItems.Add(dt.Rows[i]["IMPORTS"].ToString());
                        list.SubItems.Add(dt.Rows[i]["COMPCODE"].ToString());
                        list.SubItems.Add(dt.Rows[i]["USERNAME"].ToString());
                        if (i % 2 == 0)
                        {
                            list.BackColor = Color.White;

                        }
                        else
                        {
                            list.BackColor = Color.WhiteSmoke;


                        }
                        listView2.Items.Add(list);

                    }
                }
                else
                {
                    listView2.Items.Clear();
                   // MessageBox.Show("Screen Rights UnDefined", "Error");
                }
            }
        }

        private void RefreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridLoad();
        }

        private void UserNameRefreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Combocompcode_SelectedIndexChanged(sender,e);
        }

        private void CompCodeRefreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            combocode();
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
            this.Hide();
            News();
            GlobalVariables.HeaderName.Text = "";
            GlobalVariables.TabCtrl.TabPages.RemoveAt(GlobalVariables.TabCtrl.SelectedIndex);

        }

        public void Searchs(int EditID)
        {
            throw new NotImplementedException();
        }

        public void Deletes(int id)
        {
            throw new NotImplementedException();
        }

        private void checkdatabase_CheckedChanged(object sender, EventArgs e)
        {
            mas.DatabaseCheck(checkdatabase);
        }

        private void listView1_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            
        }
    }
}
