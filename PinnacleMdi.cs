using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pinnacle
{
    public partial class PinnacleMdi : Form,ToolStripAccess
    {

        int mid = 0; int mid1 = 0; string systemuser = ""; int sessioncount = 0;
        Image img; 
           
        public PinnacleMdi()
        {
            InitializeComponent();
            //if (Class.Users.HUserName == "VAIRAM") { checkdatabase.Visible = true; } else { checkdatabase.Visible = false; }
            Class.Users.CREATED = Convert.ToDateTime(System.DateTime.Now.ToString("dd-MM-yyyy") + " " + System.DateTime.Now.ToLongTimeString());
            tabControl1.TabPages.Remove(Reports);
            tabControl1.TabPages.Remove(Masters);
            tabControl1.TabPages.Remove(Transactions);
            tabControl1.TabPages.Remove(Hostel);
            tabControl1.TabPages.Remove(Fuel);
            tabControl1.TabPages.Remove(Canteen);
            tabControl1.TabPages.Remove(TreeView);
            tabControl1.TabPages.Remove(Registration);
            tabControl1.TabPages.Remove(GatePass);
            systemuser = Environment.UserName;
            mdipanel.Show();
        }

        Models.UserRights sm = new Models.UserRights();
        List<Models.UserRights> usr = new List<Models.UserRights>();
        private void showform(Form frm)
        {
            frm.MdiParent = this;
            frm.Dock = DockStyle.Fill;
            frm.Show();
            frm.Activate();
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
           
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        ToolStripMenuItem MnuStripItem = new ToolStripMenuItem();
        TreeNode mainNode = new TreeNode();
        DataTable dtlist = new DataTable();
        int param =0;
       
        List<string> listfilter = new List<string>(); List<string> uniqueList = new List<string>();
       
        private void timer2_Tick(object sender, EventArgs e)
        {
            if (lblMarquee1.Left < 0 && (Math.Abs(lblMarquee1.Left) > lblMarquee1.Width))           
                lblMarquee1.Left = panel1.Width;
            lblMarquee1.Left -= 5;
           
        }
        private void PinnacleMdi_Load(object sender, EventArgs e)
        {
            try
            {
                lblMarquee1.Text = "Welcome to " + Class.Users.HCompName.ToString();
                timer2.Enabled = true;
                paneltree.Visible = false; toolStripStatusLabel3.Text = Class.Users.UserTime.ToString();
                PinnacleMdi.ActiveForm.Text = Class.Users.HCompName.ToString() + "  UserName: " + Class.Users.HUserName.ToString() + "  ProjectName  :" + Class.Users.ProjectID + " - " + Class.Users.HostelName;

                systemuser = Environment.UserName;
                treeload();
                combosearchload(); img = null;
                DataTable dtCC = Utility.SQLQuery("SELECT LOGO AS EMPIMAGE FROM EDOCIMAGE WHERE IMGNAME = 'COMPLOGO' AND COMPANYID ='" + Class.Users.HCompcode + "' ");
                if (dtCC.Rows[0]["EMPIMAGE"].ToString() != "")
                {
                    foreach (DataRow myRow in dtCC.Rows)
                    {

                        if (myRow["EMPIMAGE"].ToString() != "")
                        {
                            byte[] bytes = (byte[])myRow["EMPIMAGE"];
                            img = Models.Device.ByteArrayToImage(bytes);
                            pictureBox1.BackgroundImage = img;

                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            //timer1.Enabled = true;
            //uniqueList.Clear();           
            //GlobalVariables.MenuStrip1 = this.MainMenuStrip;
            //GlobalVariables.Toolstrip1 = this.toolStrip1;
            //GlobalVariables.News = this.News;
            //GlobalVariables.Saves = this.Saves;
            //GlobalVariables.Prints = this.Prints;
            //GlobalVariables.Searchs = this.Searchs;
            //GlobalVariables.Deletes = this.Deletes;
            //GlobalVariables.ReadOnlys = this.ReadOnlys;
            //GlobalVariables.MdiPanel = this.mdipanel;
            //GlobalVariables.Imports = this.Imports;
            //GlobalVariables.Pdfs = this.Pdfs;
            //GlobalVariables.DownLoads = this.DownLoads;
            //GlobalVariables.ChangeSkins = this.ChangeSkins;
            //GlobalVariables.ChangePasswords = this.ChangePasswords;
            //GlobalVariables.Logins = this.Logins;
            //GlobalVariables.GlobalSearchs = this.GlobalSearchs;
            //GlobalVariables.TreeButtons = this.TreeButtons;
            //GlobalVariables.HeaderName = this.lblheader;
            //GlobalVariables.Exit = this.Exit;       
            //GlobalVariables.MasterForm = this;
            // GlobalVariables.TabCtrl = this.TabCtrl;
            //combosearch.Select();
            //GlobalVariables.Toolstrip1.Visible = false;
            //TabCtrl.Visible = false;
            //tabControl1.Visible = true;
            //  string sel = "select distinct A.menuid, A.MENUNAME from asptbluserrights  a  join asptblusermas b on A.USERNAME=B.USERID join gtcompmast c on C.GTCOMPMASTID=A.COMPCODE and C.GTCOMPMASTID=B.COMPCODE   where  A.PARENTMENUID=1 AND A.ACTIVE='T'AND B.USERNAME='" + Class.Users.HUserName+ "' AND C.COMPCODE='" + Class.Users.HCompcode + "'  order by 1";
            //    DataSet ds = Utility.ExecuteSelectQuery(sel, "asptblmenuname");
            //DataTable dt = ds.Tables["asptblmenuname"];
            //if (dt.Rows.Count > 0)
            //{
            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {

            //        if ("Masters" == dt.Rows[i]["MENUNAME"].ToString())
            //        {


            //            tabControl1.TabPages.Add(Masters);


            //        }

            //        if ("Transactions" == dt.Rows[i]["MENUNAME"].ToString())
            //        {

            //            tabControl1.TabPages.Add(Transactions);


            //        }

            //        if ("Fuel" == dt.Rows[i]["MENUNAME"].ToString())
            //        {

            //            Class.Users.Intimation = "PAYROLL";
            //            tabControl1.TabPages.Add(Fuel);

            //        }

            //        if ("Hostel" == dt.Rows[i]["MENUNAME"].ToString())
            //        {

            //            Class.Users.Intimation = "PAYROLL";
            //            tabControl1.TabPages.Add(Hostel);


            //        }

            //        if ("Canteen" == dt.Rows[i]["MENUNAME"].ToString())
            //        {


            //            tabControl1.TabPages.Add(Canteen);


            //        }


            //        if ("TreeView" == dt.Rows[i]["MENUNAME"].ToString())
            //        {

            //            tabControl1.TabPages.Add(TreeView);


            //        }
            //        if ("Reports" == dt.Rows[i]["MENUNAME"].ToString())
            //        {
            //            tabControl1.TabPages.Add(Reports);
            //        }
            //        if ("Registration" == dt.Rows[i]["MENUNAME"].ToString())
            //        {
            //            tabControl1.TabPages.Add(Registration);
            //        }
            //    }
            //}

            //Backcolor3_Click(sender,e);
            try
            {
                lblMarquee1.Text = $"Welcome to {Class.Users.HCompName}";
                timer2.Enabled = true;

                paneltree.Visible = false;
                toolStripStatusLabel3.Text = Class.Users.UserTime.ToString();

                //PinnacleMdi.ActiveForm.Text =
                //    $"{Class.Users.HCompName}  UserName: {Class.Users.HUserName}  - {Class.Users.HostelName}";

                systemuser = Environment.UserName;

                treeload();
                combosearchload();

               // img = null;

               
                    //   DataTable dtCC = Utility.SQLQuery($"SELECT LOGO AS EMPIMAGE FROM EDOCIMAGE WHERE IMGNAME='COMPLOGO' AND COMPANYID='{Class.Users.HCompcode}'"); ;

                    //if (dtCC.Rows.Count > 0)
                    //{
                    //    foreach (DataRow row in dtCC.Rows)
                    //    {
                    //        if (row["EMPIMAGE"] != DBNull.Value)
                    //        {
                    //            byte[] bytes = (byte[])row["EMPIMAGE"];
                    //            img = Models.Device.ByteArrayToImage(bytes);
                    //            pictureBox1.BackgroundImage = img;
                    //        }
                    //    }
                    //}

               
                timer1.Enabled = true;
                uniqueList.Clear();

                // Save global variables
                GlobalVariables.MenuStrip1 = this.MainMenuStrip;
                GlobalVariables.Toolstrip1 = this.toolStrip1;
                GlobalVariables.News = this.News;
                GlobalVariables.Saves = this.Saves;
                GlobalVariables.Prints = this.Prints;
                GlobalVariables.Searchs = this.Searchs;
                GlobalVariables.Deletes = this.Deletes;
                GlobalVariables.ReadOnlys = this.ReadOnlys;
                GlobalVariables.MdiPanel = this.mdipanel;
                GlobalVariables.Imports = this.Imports;
                GlobalVariables.Pdfs = this.Pdfs;
                GlobalVariables.DownLoads = this.DownLoads;
                GlobalVariables.ChangeSkins = this.ChangeSkins;
                GlobalVariables.ChangePasswords = this.ChangePasswords;
                GlobalVariables.Logins = this.Logins;
                GlobalVariables.GlobalSearchs = this.GlobalSearchs;
                GlobalVariables.TreeButtons = this.TreeButtons;
                GlobalVariables.HeaderName = this.lblheader;
                GlobalVariables.Exit = this.Exit;
                GlobalVariables.MasterForm = this;
                GlobalVariables.TabCtrl = this.TabCtrl;

                combosearch.Select();

                GlobalVariables.Toolstrip1.Visible = false;
                TabCtrl.Visible = false;
                tabControl1.Visible = true;

                // Load user menu rights
                string sel =
                    "select distinct A.menuid, A.MENUNAME " +
                    "from asptbluserrights A " +
                    "join asptblusermas B on A.USERNAME = B.USERID " +
                    "join gtcompmast C on C.GTCOMPMASTID = A.COMPCODE and C.GTCOMPMASTID = B.COMPCODE " +
                    $"where A.PARENTMENUID=1 AND A.ACTIVE='T' AND B.USERNAME='{Class.Users.HUserName}' " +
                    $"AND C.COMPCODE='{Class.Users.HCompcode}' order by 1";

                DataSet ds = Utility.ExecuteSelectQuery(sel, "asptblmenuname");
                DataTable dt = ds.Tables["asptblmenuname"];

                if (dt.Rows.Count > 0)
                {
                    // mapping menu name → tab page
                    Dictionary<string, TabPage> menuMap = new Dictionary<string, TabPage>()
        {
            { "Masters", Masters },
            { "Transactions", Transactions },
            { "Fuel", Fuel },
            { "Hostel", Hostel },
            { "Canteen", Canteen },
            { "TreeView", TreeView },
            { "Reports", Reports },
            { "Registration", Registration },
             { "GatePass", GatePass }
        };

                    foreach (DataRow row in dt.Rows)
                    {
                        string menuName = row["MENUNAME"].ToString();

                        // Special cases
                        if (menuName == "Fuel" || menuName == "Hostel" || menuName == "GatePass")
                            Class.Users.Intimation = "PAYROLL";

                        if (menuMap.ContainsKey(menuName))
                            tabControl1.TabPages.Add(menuMap[menuName]);
                    }
                }

                Backcolor3_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading application: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }


        int i = 0;

        private void combosearchload()
        {
                      DataTable dt1 = sm.headerdropdowns();
            combosearch.DisplayMember = "menuname";
            combosearch.ValueMember = "menuname";
            combosearch.DataSource = dt1;
            combosearch.Text = ""; combosearch.SelectedIndex = -1;
        }
        public void usercheck(string s, string ss, string sss)
        {
            //try
            //{
            //    DataTable dt1 = sm.headerdropdowns(s, ss, sss);
            //    int cnt = dt1.Rows.Count;
            //    if (cnt >= 1)
            //    {
            //        if (dt1.Rows[0]["Menuname"].ToString() == Class.Users.ScreenName)
            //        {
            //            for (int r = 0; r < dt1.Rows.Count; r++)
            //            {
            //                if (dt1.Rows[r]["NEWS"].ToString() == "T") { GlobalVariables.News.Visible = true; } else { GlobalVariables.News.Visible = false; }
            //                if (dt1.Rows[r]["SAVES"].ToString() == "T") { GlobalVariables.Saves.Visible = true; } else { GlobalVariables.Saves.Visible = false; }
            //                if (dt1.Rows[r]["PRINTS"].ToString() == "T") { GlobalVariables.Prints.Visible = true; } else { GlobalVariables.Prints.Visible = false; }
            //                if (dt1.Rows[r]["READONLY"].ToString() == "T") { GlobalVariables.ReadOnlys.Visible = false; this.Enabled = true; } else { this.Enabled = false; }
            //                if (dt1.Rows[r]["SEARCH"].ToString() == "T") { GlobalVariables.Searchs.Visible = true; } else { GlobalVariables.Searchs.Visible = false; }
            //                if (dt1.Rows[r]["DELETES"].ToString() == "T") { GlobalVariables.Deletes.Visible = true; } else { GlobalVariables.Deletes.Visible = false; }
            //                if (dt1.Rows[r]["TREEBUTTON"].ToString() == "T") { GlobalVariables.TreeButtons.Visible = true; } else { GlobalVariables.TreeButtons.Visible = false; }
            //                if (dt1.Rows[r]["GLOBALSEARCH"].ToString() == "T") { GlobalVariables.GlobalSearchs.Visible = true; } else { GlobalVariables.GlobalSearchs.Visible = false; }
            //                if (dt1.Rows[r]["LOGIN"].ToString() == "T") { GlobalVariables.Logins.Visible = true; } else { GlobalVariables.Logins.Visible = false; }
            //                if (dt1.Rows[r]["CHANGEPASSWORD"].ToString() == "T") { GlobalVariables.ChangePasswords.Visible = true; } else { GlobalVariables.ChangePasswords.Visible = false; }
            //                if (dt1.Rows[r]["CHANGESKIN"].ToString() == "T") { GlobalVariables.ChangeSkins.Visible = true; } else { GlobalVariables.ChangeSkins.Visible = false; }
            //                if (dt1.Rows[r]["DOWNLOAD"].ToString() == "T") { GlobalVariables.DownLoads.Visible = true; } else { GlobalVariables.DownLoads.Visible = false; }
            //                if (dt1.Rows[r]["Pdf"].ToString() == "T") { GlobalVariables.Pdfs.Visible = true; } else { GlobalVariables.Pdfs.Visible = false; }
            //                if (dt1.Rows[r]["Imports"].ToString() == "T") { GlobalVariables.Imports.Visible = true; } else { GlobalVariables.Imports.Visible = false; }

            //            }
            //        }
            //    }
            //    else
            //    {
            //        MessageBox.Show("No Screen Rights Defined .Pls Contact Your Administrator...");
            //    }
            //}catch(Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            try
            {
                DataTable dt1 = sm.headerdropdowns(s, ss, sss);

                if (dt1.Rows.Count == 0)
                {
                    MessageBox.Show("No Screen Rights Defined. Please contact your Administrator...");
                    return;
                }

                // ScreenName match check
                if (dt1.Rows[0]["Menuname"].ToString() != Class.Users.ScreenName)
                    return;

                foreach (DataRow row in dt1.Rows)
                {
                    SetVisibility(GlobalVariables.News, row["NEWS"]);
                    SetVisibility(GlobalVariables.Saves, row["SAVES"]);
                    SetVisibility(GlobalVariables.Prints, row["PRINTS"]);
                    SetVisibility(GlobalVariables.Searchs, row["SEARCH"]);
                    SetVisibility(GlobalVariables.Deletes, row["DELETES"]);
                    SetVisibility(GlobalVariables.TreeButtons, row["TREEBUTTON"]);
                    SetVisibility(GlobalVariables.GlobalSearchs, row["GLOBALSEARCH"]);
                    SetVisibility(GlobalVariables.Logins, row["LOGIN"]);
                    SetVisibility(GlobalVariables.ChangePasswords, row["CHANGEPASSWORD"]);
                    SetVisibility(GlobalVariables.ChangeSkins, row["CHANGESKIN"]);
                    SetVisibility(GlobalVariables.DownLoads, row["DOWNLOAD"]);
                    SetVisibility(GlobalVariables.Pdfs, row["PDF"]);
                    SetVisibility(GlobalVariables.Imports, row["IMPORTS"]);

                    // READONLY: special case
                    if (row["READONLY"].ToString() == "T")
                    {
                        GlobalVariables.ReadOnlys.Visible = false;
                        this.Enabled = true;
                    }
                    else
                    {
                        this.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void SetVisibility(ToolStripButton ctrl, object flag)
        {
            ctrl.Visible = (flag?.ToString() == "T");
        }

        private void pop1(int param) => LoadMenuItems(param, flowLayoutPanel5);
        private void pop2(int param) => LoadMenuItems(param, flowLayoutPanel6);
        private void pop3(int param) => LoadMenuItems(param, flowLayoutPanel7);
        private void pop4(int param) => LoadMenuItems(param, flowLayoutPanel8);
        private void pop5(int param) => LoadMenuItems(param, flowLayoutPanel9);
        private void pop6(int param) => LoadMenuItems(param, flowLayoutPanel10);
        private void pop7(int param) => LoadMenuItems(param, flowLayoutPanel11);
        private void pop8(int param) => LoadMenuItems(param, flowLayoutPanel12);
        private void pop9(int param) => LoadMenuItems(param, flowLayoutPanel13);
        private void LoadMenuItems(int param, FlowLayoutPanel panel)
        {
            DataTable dt = sm.headerdropdowns(Class.Users.HCompcode, Class.Users.HUserName, param);

            panel.Controls.Clear();

            CustomControl[] items = new CustomControl[dt.Rows.Count];
            int i = 0;

            foreach (DataRow myRow in dt.Rows)
            {
                items[i] = new CustomControl();
                items[i].menuname.Text = myRow["menuname"].ToString();

                panel.Controls.Add(items[i]);

                // apply colors
                items[i].iconbackground.BackColor = Class.Users.BackColors;
                items[i].menuname.ForeColor = Class.Users.BackColors;

                // click event
                items[i].menuname.Click += Menuname_Click;

                // user image logic
                if (img != null)
                {
                    pictureBox1.Image = img;
                    items[i].userimage = img;
                }
                else
                {
                    if (Class.Users.HUnitSub == "pssagfsample")
                    {
                        items[i].userimage = Pinnacle.Properties.Resources.Anugraha_logo;
                        pictureBox1.Image = Pinnacle.Properties.Resources.Anugraha_logo;
                    }
                    else
                    {
                        items[i].userimage = Pinnacle.Properties.Resources.pinacle;
                        pictureBox1.Image = Pinnacle.Properties.Resources.pinacle;
                    }
                }

                i++;
            }
        }

       
        string[] s;
        private void Menuname_Click(object sender, EventArgs e)
        {
            Class.Users.ScreenName = "";
            s = sender.ToString().Split(',');
            sender = s[1].Substring(7).TrimEnd();
            ChildClick(sender, e);
        }

        private void Username_Click(object sender, EventArgs e)
        {
            Class.Users.ScreenName = "";
           
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            mdipanel.Show();

        }
        Int64 parentID = 0; TreeNode parentNode = null;
        private void treeload()
        {
            string sel = "select a.userrightsid, a.menuid,  a.menuname ,a.parentmenuid  from  asptbluserrights a  join gtcompmast b on b.gtcompmastid=a.compcode join asptblusermas c on c.userid=a.username where  b.compcode='" + Class.Users.HCompcode + "'      and c.username='" + Class.Users.HUserName + "'  and  a.active='T' and a.parentmenuid = 1 order by 1";// and a.parentmenuid = 1
            DataSet ds = Utility.ExecuteSelectQuery(sel, "asptbluserrights");
            DataTable dt = ds.Tables["asptbluserrights"];
            foreach (DataRow dr in dt.Rows)
            {


                parentNode = treeView1.Nodes.Add(dr["menuname"].ToString());
                PopulateTreeView(Convert.ToInt64(dr["menuid"].ToString()), parentNode);

            }
        }

        private void PopulateTreeView(Int64 parentId, TreeNode parentNode)
        {
            string sel = "  select a.menuid, a.menuname ,a.parentmenuid  from   asptbluserrights a  join gtcompmast b on b.gtcompmastid=a.compcode join asptblusermas c on c.userid=a.username  join asptblnavigation d on d.menuid = a.menuid where  b.compcode='" + Class.Users.HCompcode + "'      and c.username='" + Class.Users.HUserName + "'  and  a.parentmenuid='" + parentId + "' and  a.active='T' order by 1";
            DataSet ds = Utility.ExecuteSelectQuery(sel, "asptbluserrights");
            DataTable dtchildc = ds.Tables["asptbluserrights"];
            // DataTable dtchildc = sm.headerdropdowns(Class.Users.HCompcode, Class.Users.HUserName, parentId);

            TreeNode childNode;
            foreach (DataRow dr in dtchildc.Rows)
            {
                Models.UserRights sm1 = new Models.UserRights();
                if (parentNode == null)
                {
                    childNode = treeView1.Nodes.Add(dr["menuname"].ToString());

                }
                else
                {
                    childNode = parentNode.Nodes.Add(dr["menuname"].ToString());

                }
                PopulateTreeView(Convert.ToInt32("0" + dr["menuid"].ToString()), childNode);
                sm1.MenuName = dr["menuname"].ToString();
                usr.Add(sm1);

            }
        }
       
       

        public void SubMenu(ToolStripMenuItem mnu, int midd)
        {
            //string sel = "  select a.menuid, a.menuname ,a.parentmenuid  from   asptbluserrights a  join gtcompmast b on b.gtcompmastid=a.compcode join asptblusermas c on c.userid=a.username  join asptblnavigation d on d.menuid = a.menuid where  b.compcode='" + Class.Users.HCompcode + "'      and c.username='" + Class.Users.HUserName + "'  and  a.parentmenuid='" + midd + "' and  a.active='T' order by 1";
            //DataSet ds = Utility.ExecuteSelectQuery(sel, "asptbluserrights");
            //DataTable dtchild = ds.Tables["asptbluserrights"];

            //for (int j = 0; j < dtchild.Rows.Count; j++)
            //{
            //    ToolStripMenuItem SSMenu = new ToolStripMenuItem(dtchild.Rows[j]["menuname"].ToString(), null, new EventHandler(ChildClick));
            //    mnu.DropDownItems.Add(SSMenu);


            //}

        }

        public void SubMenu1(TreeNode mnu, int midd)
        {
           
            string sel = "  select a.menuid, a.menuname ,a.parentmenuid  from   asptbluserrights a  join gtcompmast b on b.gtcompmastid=a.compcode join asptblusermas c on c.userid=a.username  join asptblnavigation d on d.menuid = a.menuid where  b.compcode='" + Class.Users.HCompcode + "'      and c.username='" + Class.Users.HUserName + "'  and  a.parentmenuid='" + midd + "' and  a.active='T' order by 1";
            DataSet ds = Utility.ExecuteSelectQuery(sel, "asptbluserrights");
            DataTable dtchild = ds.Tables["asptbluserrights"];
            for (int j = 0; j < dtchild.Rows.Count; j++)
            {               
                TreeNode SSMenu = new TreeNode(dtchild.Rows[j]["menuname"].ToString());
                mnu.Nodes.Add(SSMenu);

            }
        }
        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {

            sender = e.Node.Text;
            ChildClick(sender, e);
        }
       
        int listcount = 0;

        private DataTable ConvertListToDataTable()
        {

            List<string> uniqueList = listfilter.Distinct().ToList();
            dtlist.Rows.Clear();
            dtlist.Columns.Clear();
            if (dtlist.Rows.Count == 0)
            {
                dtlist.Columns.Add("MenuName", typeof(string));
            }

            foreach (string str in uniqueList)
            {
                DataRow row = dtlist.NewRow();
                row["MenuName"] = str;
                dtlist.Rows.Add(row);
            }


            return dtlist;
        }
        //static DataTable ConvertListToDataTable(List<Models.UserRights> list)
        //{

        //    DataTable table = new DataTable();
        //    table.Columns.Add("MenuName", typeof(string));
        //    foreach (var dvd in list)
        //    {
        //        var row = table.NewRow();
        //        row["MenuName"] = dvd.MenuName;               
        //        table.Rows.Add(row);               
        //    }           
        //    return table;
        //}
        string screen1 = "";
        private void button5_Click(object sender, EventArgs e)
        {

            butprevious_Click(sender,e);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            butnext_Click(sender,e);
        }



        private void ChildClick(object sender, EventArgs e)
        {

            try
            {
                mdipanel.Hide();
                Class.Users.ScreenName = "";
                Class.Users.ScreenName = sender.ToString();
                GlobalVariables.TabCtrl.Visible = true;
                usercheck(Class.Users.HCompcode, Class.Users.HUserName, Class.Users.ScreenName);
                uniqueList.Add(sender.ToString().Trim());
                GlobalVariables.HeaderName.Text = sender.ToString();
                Class.Users.UserTime = 0;
                GlobalVariables.Toolstrip1.Visible = true;
                Class.Users.Intimation = "";
                //}

                switch (sender.ToString().Trim())
                {

                    case "EmployeeMaster":
                        if (Class.Users.HCompcode != "LOPPL")
                        {
                            CommonFunctions.ShowPopUpForm(Master.EmployeeMaster.Instance, this); button1.Show(); return;
                        }
                        else
                        {
                            CommonFunctions.ShowPopUpForm(Master.Lovely.EmployeeMaster.Instance, this); button1.Show(); return;
                        }

                    case "SalarySlip":
                        CommonFunctions.ShowPopUpForm(Pinnacle.Transactions.Lovely.SalarySlip.Instance, this); button1.Show(); return;
                    case "MonthPermission":
                        CommonFunctions.ShowPopUpForm(Pinnacle.Transactions.Lovely.MonthPermission.Instance, this); button1.Show(); return;
                    case "ALLSalarySlip":
                        CommonFunctions.ShowPopUpForm(Pinnacle.Transactions.Lovely.ALLSalarySlip.Instance, this); button1.Show(); return;
                    case "MachineMasters":
                        CommonFunctions.ShowPopUpForm(Pinnacle.Transactions.MachineMasters.Instance, this); button1.Show(); return;

                    case "ResponsePersonMaster":
                        CommonFunctions.ShowPopUpForm(Master.SampleCollection.ResponsePersonMaster.Instance, this); button1.Show(); return;
                    case "AboutMaster":
                        CommonFunctions.ShowPopUpForm(Pinnacle.Registration.AboutMaster.Instance, this); button1.Show(); return;

                    case "GenerateMaster":
                        CommonFunctions.ShowPopUpForm(Pinnacle.Registration.GenerateMaster.Instance, this); button1.Show(); return;

                    case "RegistrationMaster":
                        CommonFunctions.ShowPopUpForm(Pinnacle.Registration.RegistrationMaster.Instance, this); button1.Show(); return;

                    case "CountryMaster":
                        CommonFunctions.ShowPopUpForm(Master.CountryMaster.Instance, this);
                        break;
                    case "StateMaster":
                        CommonFunctions.ShowPopUpForm(Master.StateMaster.Instance, this);
                        break;
                    case "CityMaster":
                        CommonFunctions.ShowPopUpForm(Master.CityMaster.Instance, this);
                        break;
                    case "CompanyMaster":
                        CommonFunctions.ShowPopUpForm(Master.CompanyMaster.Instance, this);
                        break;
                    case "ReConciliation":
                        CommonFunctions.ShowPopUpForm(Pinnacle.Transactions.Tally.ReConciliation.Instance, this);
                        break;
                        

                    case "PartyMaster":
                        CommonFunctions.ShowPopUpForm(Master.PartyMaster.Instance, this); button1.Show(); return;
                    case "IDCardRemove":
                        CommonFunctions.ShowPopUpForm(Pinnacle.Transactions.Attendance.IDCardRemove.Instance, this); button1.Show(); return;


                    case "DashBoard":
                        CommonFunctions.ShowPopUpForm(Master.DashBoard.Instance, this); button1.Show(); return;

                    case "FinYearMaster":
                        CommonFunctions.ShowPopUpForm(Master.FinYearMaster.Instance, this); button1.Show(); return;

                    case "InOutSequenceMaster":
                        CommonFunctions.ShowPopUpForm(Master.InOutSequenceMaster.Instance, this); button1.Show(); return;

                    case "SequenceMaster":

                        CommonFunctions.ShowPopUpForm(Master.SequenceMaster.Instance, this); button1.Show(); return;

                    case "SeasonMaster":
                        CommonFunctions.ShowPopUpForm(Master.SeasonMaster.Instance, this); button1.Show(); return;

                    case "BrandMaster":
                        CommonFunctions.ShowPopUpForm(Master.BrandMaster.Instance, this); button1.Show(); return;

                    case "SizeMaster":
                        CommonFunctions.ShowPopUpForm(Master.SizeMaster.Instance, this); button1.Show(); return;

                    case "StyleMaster":
                        CommonFunctions.ShowPopUpForm(Master.StyleMaster.Instance, this); button1.Show(); return;

                    case "GsmMaster":
                        CommonFunctions.ShowPopUpForm(Master.GsmMaster.Instance, this); button1.Show(); return;

                    case "SampleDepartmentMaster":
                        CommonFunctions.ShowPopUpForm(Master.SampleDepartmentMaster.Instance, this); button1.Show(); return;
                    case "SampleReport":
                        CommonFunctions.ShowPopUpForm(ReportFormate.AGF.SampleReport.Instance, this); button1.Show(); return;

                    case "FabricMaster":
                        CommonFunctions.ShowPopUpForm(Master.FabricMaster.Instance, this); button1.Show(); return;

                    case "SubStyleMaster":
                        //  showform(Master.SubStyleMaster.Instance); break;
                        CommonFunctions.ShowPopUpForm(Master.SubStyleMaster.Instance, this); button1.Show(); return;

                    case "CountsMaster":
                        // showform(Master.CountsMaster.Instance); break;
                        CommonFunctions.ShowPopUpForm(Master.CountsMaster.Instance, this); button1.Show(); return;

                    case "GaugeMaster":
                        // showform(Master.GaugeMaster.Instance); break;
                        CommonFunctions.ShowPopUpForm(Master.GaugeMaster.Instance, this); button1.Show(); return;

                    case "BuyerMaster":
                        CommonFunctions.ShowPopUpForm(Master.BuyerMaster.Instance, this); button1.Show(); return;

                    case "ColorMaster":
                        CommonFunctions.ShowPopUpForm(Master.ColorMaster.Instance, this); button1.Show(); return;

                    case "CurrencyMaster":
                        CommonFunctions.ShowPopUpForm(Master.CurrencyMaster.Instance, this); button1.Show(); return;

                    case "SampleCategoryMaster":
                        CommonFunctions.ShowPopUpForm(Master.SampleCategoryMaster.Instance, this); button1.Show(); return;

                    case "SamplePackTypeMaster":
                        CommonFunctions.ShowPopUpForm(Master.SamplePackTypeMaster.Instance, this); button1.Show(); return;

                    case "RackMaster":
                        CommonFunctions.ShowPopUpForm(Master.RackMaster.Instance, this); button1.Show(); return;

                    case "BinMaster":
                        CommonFunctions.ShowPopUpForm(Master.BinMaster.Instance, this); button1.Show(); return;

                    case "SampleTypeMaster":
                        CommonFunctions.ShowPopUpForm(Master.SampleTypeMaster.Instance, this); button1.Show(); return;

                    case "SampleInwardEntry":
                        CommonFunctions.ShowPopUpForm(Pinnacle.Transactions.SampleInwardEntry.Instance, this); button1.Show(); return;

                   
                    case "SampleIssueEntry":
                        CommonFunctions.ShowPopUpForm(Pinnacle.Transactions.SampleIssueEntry.Instance, this); button1.Show(); return;


                    case "VehicleTableMatch":
                        CommonFunctions.ShowPopUpForm(ReportFormate.VehicleTableMatch.Instance, this); button1.Show(); return;

                    case "IPEntry":
                        Class.Users.Intimation = "PAYROLL";
                        CommonFunctions.ShowPopUpForm(Master.IPEntry.Instance, this); button1.Show(); return;
                    case "MonthMaster":
                        CommonFunctions.ShowPopUpForm(Pinnacle.Hostel.MonthMaster.Instance, this);
                        button1.Show(); return;
                    case "GatePassManual":
                        Class.Users.Intimation = "PAYROLL";
                        CommonFunctions.ShowPopUpForm(Pinnacle.Hostel.GatePassManual.Instance, this); button1.Show(); return;

                    case "BuyersSample":
                        CommonFunctions.ShowPopUpForm(Pinnacle.Transactions.BuyersSample.Instance, this); button1.Show(); return;
                    case "LogsRemove":
                        Class.Users.Intimation = "PAYROLL";
                        CommonFunctions.ShowPopUpForm(Pinnacle.Transactions.LogsRemove.Instance, this); button1.Show(); return;

                    case "DeviceCommunication":
                        Class.Users.Intimation = "PAYROLL";
                        CommonFunctions.ShowPopUpForm(Pinnacle.Transactions.DeviceCommunication.Instance, this); button1.Show(); return;
                    case "Canten Token Details":
                        Class.Users.Intimation = "PAYROLL";
                        CommonFunctions.ShowPopUpForm(Pinnacle.Canteen.FK.CantenTokenDetails.Instance, this); button1.Show(); return;

                    case "SecurityInventry":
                        CommonFunctions.ShowPopUpForm(Pinnacle.Transactions.SecurityInventry.Instance, this); button1.Show(); return;

                    case "SecurityDelivery":
                        CommonFunctions.ShowPopUpForm(Pinnacle.Transactions.SecurityDelivery.Instance, this); button1.Show(); return;



                    case "IDCardPrintDetails":
                        CommonFunctions.ShowPopUpForm(Pinnacle.Transactions.IDCardPrintDetails.Instance, this); button1.Show(); return;
                    case "DataTransfer":
                        CommonFunctions.ShowPopUpForm(Pinnacle.Transactions.DataTransfer.Instance, this); button1.Show(); return;

                    ////---------------------------------------//
                    case "UserMaster":
                        CommonFunctions.ShowPopUpForm(Pinnacle.TreeView.UserMaster.Instance, this); button1.Show(); return;

                    case "TreeViewMaster":
                        CommonFunctions.ShowPopUpForm(Pinnacle.TreeView.TreeViewMaster.Instance, this); button1.Show(); return;

                    case "MenuNameMaster":
                        CommonFunctions.ShowPopUpForm(Pinnacle.TreeView.MenuNameMaster.Instance, this); button1.Show(); return;

                    case "UserRights":
                        CommonFunctions.ShowPopUpForm(Pinnacle.TreeView.UserRights.Instance, this); button1.Show(); return;

                    case "NavigationMaster":

                        CommonFunctions.ShowPopUpForm(Pinnacle.TreeView.NavigationMaster.Instance, this); button1.Show(); return;

                    case "DataImport":
                        CommonFunctions.ShowPopUpForm(Pinnacle.Transactions.DataImport.Instance, this); button1.Show(); return;

                    case "Canteen Item Master":
                        CommonFunctions.ShowPopUpForm(Pinnacle.Canteen.CanteenItemMaster.Instance, this); button1.Show(); return;

                    case "NHDayEntry":

                        CommonFunctions.ShowPopUpForm(Pinnacle.Hostel.NHDayEntry.Instance, this); button1.Show(); return;

                    case "GatePass":
                        Class.Users.Intimation = "PAYROLL";
                        CommonFunctions.ShowPopUpForm(Pinnacle.Hostel.GatePass.Instance, this); button1.Show(); return;

                    case "MachineMaster":
                        Class.Users.Intimation = "PAYROLL";
                        CommonFunctions.ShowPopUpForm(Pinnacle.Hostel.MachineMaster.Instance, this); button1.Show(); return;

                    case "ReasonMaster":
                        Class.Users.Intimation = "PAYROLL";
                        CommonFunctions.ShowPopUpForm(Pinnacle.Hostel.ReasonMaster.Instance, this); button1.Show(); return;

                    case "HostelMaster":
                        Class.Users.Intimation = "PAYROLL";
                        CommonFunctions.ShowPopUpForm(Pinnacle.Hostel.HostelMaster.Instance, this); button1.Show(); return;

                    case "Canteen Menu Item":
                        CommonFunctions.ShowPopUpForm(Pinnacle.Canteen.CanteenMenuItem.Instance, this); button1.Show(); return;

                    case "Item Permission Details":
                        CommonFunctions.ShowPopUpForm(Pinnacle.Canteen.ItemPermissionDetails.Instance, this); button1.Show(); return;

                    case "Canteen Category Master":
                        CommonFunctions.ShowPopUpForm(Pinnacle.Canteen.CanteenCategoryMaster.Instance, this); button1.Show(); return;

                    case "Token Cancel":
                        CommonFunctions.ShowPopUpForm(Pinnacle.Canteen.TokenCancel.Instance, this); button1.Show(); return;


                    case "Canteen Entry":
                        CommonFunctions.ShowPopUpForm(Pinnacle.Canteen.CanteenEntry.Instance, this); button1.Show(); return;

                    case "QrCode":
                        CommonFunctions.ShowPopUpForm(Report.QrCode.Instance, this); button1.Show(); return;

                    case "Item Master":
                        CommonFunctions.ShowPopUpForm(Pinnacle.Canteen.ItemMaster.Instance, this); button1.Show(); return;

                    case "FuelToken":
                        Class.Users.Intimation = "PAYROLL";
                        CommonFunctions.ShowPopUpForm(Pinnacle.Fuel.FuelToken.Instance, this); button1.Show(); return;

                    case "VehicleMaster":
                        Class.Users.Intimation = "PAYROLL";
                        CommonFunctions.ShowPopUpForm(Pinnacle.Fuel.VehicleMaster.Instance, this); button1.Show(); return;

                    case "PetrolBunk":
                        Class.Users.Intimation = "PAYROLL";
                        CommonFunctions.ShowPopUpForm(Pinnacle.Fuel.PetrolBunk.Instance, this); button1.Show(); return;

                    case "WeighBridge":
                        //showform(Fuel.WeighBridge.Instance);  break;
                        CommonFunctions.ShowPopUpForm(Pinnacle.Fuel.WeighBridge.Instance, this); button1.Show(); return;

                    case "FuelRateMaster":
                        Class.Users.Intimation = "PAYROLL";
                        CommonFunctions.ShowPopUpForm(Pinnacle.Fuel.FuelRateMaster.Instance, this); button1.Show(); return;

                    case "CompanyWiseSecurityInventry":
                        // showform(Transactions.CompanyWiseSecurityInventry.Instance);  break;
                        CommonFunctions.ShowPopUpForm(Pinnacle.Transactions.CompanyWiseSecurityInventry.Instance, this); button1.Show(); return;

                    case "OutPassLiveReport":
                        Class.Users.Intimation = "PAYROLL";
                        CommonFunctions.ShowPopUpForm(ReportFormate.OutPassLiveReport.Instance, this); button1.Show(); return;

                    case "PendingOutPassReport":
                        Class.Users.Intimation = "PAYROLL";
                        CommonFunctions.ShowPopUpForm(ReportFormate.PendingOutPassReport.Instance, this); button1.Show(); return;

                    case "MonthWiseFuelReport":
                        Class.Users.Intimation = "PAYROLL";
                        CommonFunctions.ShowPopUpForm(ReportFormate.MonthWiseFuelReport.Instance, this); button1.Show(); return;

                    case "FuelAverageReport":
                        Class.Users.Intimation = "PAYROLL";
                        CommonFunctions.ShowPopUpForm(ReportFormate.FuelAverageReport.Instance, this); button1.Show(); return;

                    case "FuelComparisonReport":
                        Class.Users.Intimation = "PAYROLL";
                        CommonFunctions.ShowPopUpForm(ReportFormate.FuelComparisonReport.Instance, this); button1.Show(); return;

                    case "TokenReport":
                        Class.Users.Intimation = "PAYROLL";
                        CommonFunctions.ShowPopUpForm(ReportFormate.TokenReport.Instance, this); button1.Show(); return;

                    case "MovementReport":
                        Class.Users.Intimation = "PAYROLL";
                        CommonFunctions.ShowPopUpForm(ReportFormate.MovementReport.Instance, this); button1.Show(); return;

                    case "SampleCollectionReport":
                        CommonFunctions.ShowPopUpForm(ReportFormate.SampleCollectionReport.Instance, this); button1.Show(); return;


                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("ChildClick" + ex.ToString());
            }
        }


   

        private void MnuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            

        }

        private void PinnacleMdi_MouseEnter(object sender, EventArgs e)
        {
            this.BackColor = Color.Silver;
        }

        private void PinnacleMdi_MouseLeave(object sender, EventArgs e)
        {
            this.BackColor = Color.White;
          
        }
        int co = 0;

        private void Timer1_Tick(object sender, EventArgs e)
        {
            // Update status label with current date and time
            toolStripStatusLabel1.Text = $"  Date  : {DateTime.Now.ToShortDateString()}        Time  : {DateTime.Now.ToLongTimeString()}";

            // Toggle marquee color and picture box visibility
            bool toggle = co == 0;
            lblMarquee1.ForeColor = toggle ? Color.White : Color.Silver;
            pictureBox1.Visible = toggle;
            Class.Users.UserTime += 1;
            co = toggle ? 1 : 0;

            // Update user time label
            toolStripStatusLabel3.Text = Class.Users.UserTime.ToString();

            // Check if user time exceeded login time
            if (Class.Users.UserTime > Class.Users.LoginTime && GlobalVariables.CurrentForm != null)
            {
                if (GlobalVariables.CurrentForm is ToolStripAccess tsa)
                {
                    tsa.News();
                }
            }

            //toolStripStatusLabel1.Text = "  Date  :" + System.DateTime.Now.ToShortDateString() + "        Time  : " + System.DateTime.Now.ToLongTimeString();
            //if (co == 0)
            //{
            //    lblMarquee1.ForeColor = Color.White; 
            //    pictureBox1.Show(); Class.Users.UserTime += 1;
            //    co = 1; 
            //}
            //else
            //{
            //    lblMarquee1.ForeColor = Color.Silver; 
            //    pictureBox1.Hide(); Class.Users.UserTime += 1;
            //    co = 0; 
            //}

            //toolStripStatusLabel3.Text = Class.Users.UserTime.ToString();
            //if (Class.Users.UserTime > Class.Users.LoginTime)
            //{
            //    if (GlobalVariables.CurrentForm != null)
            //    {

            //        ((ToolStripAccess)GlobalVariables.CurrentForm).News();

            //    }

            //}

        }
        private void PinnacleMdi_FormClosed(object sender, FormClosedEventArgs e)
        {

            if (Class.Users.HCompcode != "" && Class.Users.HUserName != "")
            {
             
                Application.Exit();
               

            }
        }


        private void Button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
            button1.Hide();
        }


        private void RefreshToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            treeView1.Nodes.Clear();
            treeload(); combosearchload();
            sm.headerdropdowns(Class.Users.HCompcode, Class.Users.HUserName);
            Backcolor5_Click(sender, e);
        }




        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void buntreepanel_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            if (paneltree.Visible == true)
            {
                paneltree.Visible = false;
            }
            else
            {
                paneltree.Visible = true;
            }
        }

        private void btnpanel_Click(object sender, EventArgs e)
        {
            paneltree.Visible = false;
        }

    

        private void btnrefresh_Click(object sender, EventArgs e)
        {
            treeView1.Nodes.Clear(); 
            PinnacleMdi_Load(sender, e);
        }

        private void buttreeclose_Click(object sender, EventArgs e)
        {
            paneltree.Visible = false; 
            
        }

        private void toolStriptreeopen_Click(object sender, EventArgs e)
        {
            paneltree.Visible = true; 
            
                


           
           
        }

       
        private bool NodeFiltering(TreeNode Nodo, string Texto)
        {
            bool resultado = false;

            if (Nodo.Nodes.Count == 0)
            {
                if (Nodo.Text.Substring(0,1).ToUpper().Contains(Texto.Substring(0,1).ToUpper()))
                {
                    resultado = true;
                }
                else
                {
                    Nodo.Remove();
                }
            }
            else
            {
                for (int i = Nodo.Nodes.Count; i > 0; i--)
                {
                    if (NodeFiltering(Nodo.Nodes[i - 1], Texto))
                        resultado = true;
                }

                if (!resultado)
                    Nodo.Remove();
            }

            return resultado;
        }
        internal class ListItem
        {
        }

        private void buttreesearch_Click(object sender, EventArgs e)
        {

        }

        private void txtpanelsearch_TextChanged(object sender, EventArgs e)
        {

        }

        private void refreshToolStripMenuItem1_Click(object sender, EventArgs e)
        {
           
        }
       
        private void butprevious_Click(object sender, EventArgs e)
        {
            
            
            try
            {
                listfilter = uniqueList.Distinct().ToList();
               // if (listfilter.Count > 1)
                //{
                    dtlist = ConvertListToDataTable();
                    DataTable dt = dtlist.Copy();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["MenuName"].ToString() == screen1)
                        {
                            listcount++;
                            break;
                        }
                    }
                    if (dt.Rows.Count > listcount)
                    {
                        sender = dt.Rows[listcount]["MenuName"].ToString();
                        screen1 = dt.Rows[listcount]["MenuName"].ToString();                       
                        ChildClick(sender, e);
                    }
                    else
                    {
                        MessageBox.Show("No Screen Available in the Index  :" + listcount.ToString(), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void butnext_Click(object sender, EventArgs e)
        {
            try
            {
                listfilter = uniqueList.Distinct().ToList();
               // if (listfilter.Count > 1)
               // {

                    dtlist = ConvertListToDataTable();
                    DataTable dt = dtlist.Copy();
                    if (listcount >= 1)
                    {
                        listcount--;
                        sender = dt.Rows[listcount]["MenuName"].ToString();
                        screen1 = dt.Rows[listcount]["MenuName"].ToString();
                       
                        ChildClick(sender, e);
                    }
                    else
                    {
                        MessageBox.Show("No Screen Available in the Index  :" + listcount.ToString(), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void News_Click(object sender, EventArgs e)
        {
            if (GlobalVariables.CurrentForm != null)
            {
                ((ToolStripAccess)GlobalVariables.CurrentForm).News();
                Class.Users.UserTime = 0;
            }
        }

        private void Saves_Click(object sender, EventArgs e)
        {
            if (GlobalVariables.CurrentForm != null)
            {
                Class.Users.UserTime = 0;
                ((ToolStripAccess)GlobalVariables.CurrentForm).Saves();
            }
        }

        private void Deletes_Click(object sender, EventArgs e)
        {
            if (GlobalVariables.CurrentForm != null)
            {
                Class.Users.UserTime = 0;
                ((ToolStripAccess)GlobalVariables.CurrentForm).Deletes();
            }
        }

        private void Searchs_Click(object sender, EventArgs e)
        {
            if (GlobalVariables.CurrentForm != null)
            {
                Class.Users.UserTime = 0;
                ((ToolStripAccess)GlobalVariables.CurrentForm).Searchs();
            }
        }

        private void Prints_Click(object sender, EventArgs e)
        {
            if (GlobalVariables.CurrentForm != null)
            {
                Class.Users.UserTime = 0;
                ((ToolStripAccess)GlobalVariables.CurrentForm).Prints();
            }
        }

        private void Imports_Click(object sender, EventArgs e)
        {
            if (GlobalVariables.CurrentForm != null)
            {
                Class.Users.UserTime = 0;
                ((ToolStripAccess)GlobalVariables.CurrentForm).Imports();
            }
        }

        private void Pdfs_Click(object sender, EventArgs e)
        {
            if (GlobalVariables.CurrentForm != null)
            {
                Class.Users.UserTime = 0;
                ((ToolStripAccess)GlobalVariables.CurrentForm).Pdfs();
            }
        }

        private void DownLoads_Click(object sender, EventArgs e)
        {
            if (GlobalVariables.CurrentForm != null)
            {
                Class.Users.UserTime = 0;
                ((ToolStripAccess)GlobalVariables.CurrentForm).DownLoads();
            }
        }

        private void ChangeSkins_Click(object sender, EventArgs e)
        {
            if (GlobalVariables.CurrentForm != null)
            {
                Class.Users.UserTime = 0;
                ((ToolStripAccess)GlobalVariables.CurrentForm).ChangeSkins();
            }
        }

        private void Logins_Click(object sender, EventArgs e)
        {
            if (GlobalVariables.CurrentForm != null)
            {
                Class.Users.UserTime = 0;
                ((ToolStripAccess)GlobalVariables.CurrentForm).Logins();
            }
        }

        private void ChangePasswords_Click(object sender, EventArgs e)
        {
            if (GlobalVariables.CurrentForm != null)
            {
                Class.Users.UserTime = 0;
                ((ToolStripAccess)GlobalVariables.CurrentForm).ChangePasswords();
            }
        }

        private void GlobalSearchs_Click(object sender, EventArgs e)
        {
            if (GlobalVariables.CurrentForm != null)
            {
                Class.Users.UserTime = 0;
                ((ToolStripAccess)GlobalVariables.CurrentForm).GlobalSearchs();
            }
        }

        private void TreeButtons_Click(object sender, EventArgs e)
        {
            if (GlobalVariables.CurrentForm != null)
            {
                Class.Users.UserTime = 0;
                ((ToolStripAccess)GlobalVariables.CurrentForm).TreeButtons();
            }
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            if (GlobalVariables.CurrentForm != null)
            {
                Class.Users.UserTime = 0;
                ((ToolStripAccess)GlobalVariables.CurrentForm).Exit();
                if (GlobalVariables.TabCtrl.TabPages.Count <= 0)
                {

                    GlobalVariables.TabCtrl.Visible = false;
                    GlobalVariables.Toolstrip1.Visible = false;
                }
                else
                {
                    GlobalVariables.TabCtrl.Visible = true;
                    GlobalVariables.Toolstrip1.Visible = true; 
                     ((ToolStripAccess)GlobalVariables.CurrentForm).News();
                }

            }
            else
            {
                Class.Users.UserTime = 0;
                MessageBox.Show(lblheader.Text+ "  UnAvailable ","This Screen Not Generate this Project");
            }
            if (Class.Users.ColorID == "Backcolor1_Click")
            {
                Backcolor1_Click(sender, e);
            }
            if (Class.Users.ColorID == "Backcolor2_Click")
            {
                Backcolor2_Click(sender, e);
            }
            if (Class.Users.ColorID == "Backcolor3_Click")
            {
                Backcolor3_Click(sender, e);
            }
            if (Class.Users.ColorID == "Backcolor4_Click")
            {
                Backcolor4_Click(sender, e);
            }
            if (Class.Users.ColorID == "Backcolor5_Click")
            {
                Backcolor5_Click(sender, e);
            }
        }

      

        private void ReadOnlys_Click(object sender, EventArgs e)
        {
            if (GlobalVariables.CurrentForm != null)
            {
                Class.Users.UserTime = 0;
                ((ToolStripAccess)GlobalVariables.CurrentForm).ReadOnlys();
            }
        }

        private void combosearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (combosearch.SelectedIndex >=1 )
            {
                Class.Users.UserTime = 0;
                sender = combosearch.Text;
                ChildClick(sender, e);
                combosearch.Select();
                combosearch.SelectedIndex = -1;
                
                
            }
        }
       
        private void button2_Click(object sender, EventArgs e)
        {
            if (paneltree.Visible == true) { paneltree.Visible = false; paneltree.Width = 0; }
            else
            {
                paneltree.Width = 294;
                paneltree.Visible = true;
            } //toolStriptreeopen.Visible = false;
           // button2.Visible = false; 
           
        }

        void ToolStripAccess.News()
        {
           
        }

        void ToolStripAccess.Saves()
        {
            //throw new NotImplementedException();
        }

        void ToolStripAccess.Prints()
        {
            //throw new NotImplementedException();
        }

        void ToolStripAccess.Searchs()
        {
            //throw new NotImplementedException();
        }

        void ToolStripAccess.Deletes()
        {
           // throw new NotImplementedException();
        }

        void ToolStripAccess.ReadOnlys()
        {
           // throw new NotImplementedException();
        }

        void ToolStripAccess.Imports()
        {
           // throw new NotImplementedException();
        }

        void ToolStripAccess.Pdfs()
        {
            //throw new NotImplementedException();
        }

        void ToolStripAccess.ChangePasswords()
        {
          //  throw new NotImplementedException();
        }

        void ToolStripAccess.DownLoads()
        {
            //throw new NotImplementedException();
        }

        void ToolStripAccess.ChangeSkins()
        {
           // throw new NotImplementedException();
        }

        void ToolStripAccess.Logins()
        {
            //throw new NotImplementedException();
        }

        void ToolStripAccess.GlobalSearchs()
        {
           // throw new NotImplementedException();
        }

        void ToolStripAccess.TreeButtons()
        {
            //throw new NotImplementedException();
        }

        void ToolStripAccess.Exit()
        {
            Class.Users.UserTime = 0;
        }

        public void GridLoad()
        {
            //throw new NotImplementedException();
        }

        private void Gbuttonok_Click(object sender, EventArgs e)
        {
           
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            var form2 = new PinnacleMdi();
            form2.Closed += (s, args) => this.Close();
            LoginForm lf = new LoginForm();
            lf.Show();

        }

        private void toolStripComboBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void GlobalCompcode1_SelectedIndexChanged(object sender, EventArgs e)
        {
           // Class.Users.HCompcode = GlobalCompcode1.Text;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Class.Users.UserTime = 0;
            //if (tabControl1.SelectedTab == tabControl1.TabPages["Masters"])
            //{
            //    string sel = "select distinct A.menuid, A.MENUNAME from asptbluserrights  a  join asptblusermas b on A.USERNAME=B.USERID join gtcompmast c on C.GTCOMPMASTID=A.COMPCODE and C.GTCOMPMASTID=B.COMPCODE   where  A.PARENTMENUID=1 AND A.ACTIVE='T'AND B.USERNAME='" + Class.Users.HUserName + "' AND C.COMPCODE='" + Class.Users.HCompcode + "' and a.menuname='"+ tabControl1.SelectedTab.Text.Trim() + "'  order by 1";
            //    DataSet ds = Utility.ExecuteSelectQuery(sel, "asptblmenuname");
            //    DataTable dt = ds.Tables["asptblmenuname"];
            //    param = Convert.ToInt32("0"+dt.Rows[0]["menuid"].ToString());
            //    pop1(param);
            //}


            //if (tabControl1.SelectedTab == tabControl1.TabPages["Transactions"])
            //{
            //    string sel = "select distinct A.menuid, A.MENUNAME from asptbluserrights  a  join asptblusermas b on A.USERNAME=B.USERID join gtcompmast c on C.GTCOMPMASTID=A.COMPCODE and C.GTCOMPMASTID=B.COMPCODE   where  A.PARENTMENUID=1 AND A.ACTIVE='T'AND B.USERNAME='" + Class.Users.HUserName + "' AND C.COMPCODE='" + Class.Users.HCompcode + "' and a.menuname='" + tabControl1.SelectedTab.Text.Trim() + "'  order by 1";
            //    DataSet ds = Utility.ExecuteSelectQuery(sel, "asptblmenuname");
            //    DataTable dt = ds.Tables["asptblmenuname"];
            //    param = Convert.ToInt32("0" + dt.Rows[0]["menuid"].ToString());
            //    pop2(param);
            //}

            //if (tabControl1.SelectedTab == tabControl1.TabPages["Hostel"])
            //{
            //    string sel = "select distinct A.menuid, A.MENUNAME from asptbluserrights  a  join asptblusermas b on A.USERNAME=B.USERID join gtcompmast c on C.GTCOMPMASTID=A.COMPCODE and C.GTCOMPMASTID=B.COMPCODE   where  A.PARENTMENUID=1 AND A.ACTIVE='T'AND B.USERNAME='" + Class.Users.HUserName + "' AND C.COMPCODE='" + Class.Users.HCompcode + "' and a.menuname='" + tabControl1.SelectedTab.Name.Trim() + "'  order by 1";
            //    DataSet ds = Utility.ExecuteSelectQuery(sel, "asptblmenuname");
            //    DataTable dt = ds.Tables["asptblmenuname"];
            //    param = Convert.ToInt32("0" + dt.Rows[0]["menuid"].ToString());
            //    pop3(param);
            //}

            //if (tabControl1.SelectedTab == tabControl1.TabPages["Fuel"])
            //{
            //    string sel = "select distinct A.menuid, A.MENUNAME from asptbluserrights  a  join asptblusermas b on A.USERNAME=B.USERID join gtcompmast c on C.GTCOMPMASTID=A.COMPCODE and C.GTCOMPMASTID=B.COMPCODE   where  A.PARENTMENUID=1 AND A.ACTIVE='T'AND B.USERNAME='" + Class.Users.HUserName + "' AND C.COMPCODE='" + Class.Users.HCompcode + "' and a.menuname='" + tabControl1.SelectedTab.Text.Trim() + "'  order by 1";
            //    DataSet ds = Utility.ExecuteSelectQuery(sel, "asptblmenuname");
            //    DataTable dt = ds.Tables["asptblmenuname"];
            //    param = Convert.ToInt32("0" + dt.Rows[0]["menuid"].ToString());
            //    pop4(param);
            //}


            //if (tabControl1.SelectedTab == tabControl1.TabPages["Canteen"])
            //{
            //    string sel = "select distinct A.menuid, A.MENUNAME from asptbluserrights  a  join asptblusermas b on A.USERNAME=B.USERID join gtcompmast c on C.GTCOMPMASTID=A.COMPCODE and C.GTCOMPMASTID=B.COMPCODE   where  A.PARENTMENUID=1 AND A.ACTIVE='T'AND B.USERNAME='" + Class.Users.HUserName + "' AND C.COMPCODE='" + Class.Users.HCompcode + "' and a.menuname='" + tabControl1.SelectedTab.Text.Trim() + "'  order by 1";
            //    DataSet ds = Utility.ExecuteSelectQuery(sel, "asptblmenuname");
            //    DataTable dt = ds.Tables["asptblmenuname"];
            //    param = Convert.ToInt32("0" + dt.Rows[0]["menuid"].ToString());
            //    pop5(param);
            //}
            ////if (tabControl1.SelectedTab == tabControl1.TabPages["GatePass"])
            ////{
            ////    string sel = "select distinct A.menuid, A.MENUNAME from asptbluserrights  a  join asptblusermas b on A.USERNAME=B.USERID join gtcompmast c on C.GTCOMPMASTID=A.COMPCODE and C.GTCOMPMASTID=B.COMPCODE   where  A.PARENTMENUID=1 AND A.ACTIVE='T'AND B.USERNAME='" + Class.Users.HUserName + "' AND C.COMPCODE='" + Class.Users.HCompcode + "' and a.menuname='" + tabControl1.SelectedTab.Text.Trim() + "'  order by 1";
            ////    DataSet ds = Utility.ExecuteSelectQuery(sel, "asptblmenuname");
            ////    DataTable dt = ds.Tables["asptblmenuname"];
            ////    param = Convert.ToInt32("0" + dt.Rows[0]["menuid"].ToString());
            ////    pop5(param);
            ////}

            //if (tabControl1.SelectedTab == tabControl1.TabPages["Reports"])
            //{

            //        string sel = "select distinct A.menuid, A.MENUNAME from asptbluserrights  a  join asptblusermas b on A.USERNAME=B.USERID join gtcompmast c on C.GTCOMPMASTID=A.COMPCODE and C.GTCOMPMASTID=B.COMPCODE   where  A.PARENTMENUID=1 AND A.ACTIVE='T'AND B.USERNAME='" + Class.Users.HUserName + "' AND C.COMPCODE='" + Class.Users.HCompcode + "' and a.menuname='" + tabControl1.SelectedTab.Text.Trim() + "'  order by 1";
            //        DataSet ds = Utility.ExecuteSelectQuery(sel, "asptblmenuname");
            //        DataTable dt = ds.Tables["asptblmenuname"];
            //        param = Convert.ToInt32("0" + dt.Rows[0]["menuid"].ToString());
            //        pop6(param); 



            //}
            //if (tabControl1.SelectedTab == tabControl1.TabPages["TreeView"])
            //{
            //    string sel = "select distinct A.menuid, A.MENUNAME from asptbluserrights  a  join asptblusermas b on A.USERNAME=B.USERID join gtcompmast c on C.GTCOMPMASTID=A.COMPCODE and C.GTCOMPMASTID=B.COMPCODE   where  A.PARENTMENUID=1 AND A.ACTIVE='T'AND B.USERNAME='" + Class.Users.HUserName + "' AND C.COMPCODE='" + Class.Users.HCompcode + "' and a.menuname='" + tabControl1.SelectedTab.Text + "'  order by 1";
            //    DataSet ds = Utility.ExecuteSelectQuery(sel, "asptblmenuname");
            //    DataTable dt = ds.Tables["asptblmenuname"];
            //    param = Convert.ToInt32("0" + dt.Rows[0]["menuid"].ToString());
            //    pop7(param);
            //}
            //if (tabControl1.SelectedTab == tabControl1.TabPages["Registration"])
            //{
            //    string sel = "select distinct A.menuid, A.MENUNAME from asptbluserrights  a  join asptblusermas b on A.USERNAME=B.USERID join gtcompmast c on C.GTCOMPMASTID=A.COMPCODE and C.GTCOMPMASTID=B.COMPCODE   where  A.PARENTMENUID=1 AND A.ACTIVE='T'AND B.USERNAME='" + Class.Users.HUserName + "' AND C.COMPCODE='" + Class.Users.HCompcode + "' and a.menuname='" + tabControl1.SelectedTab.Text + "'  order by 1";
            //    DataSet ds = Utility.ExecuteSelectQuery(sel, "asptblmenuname");
            //    DataTable dt = ds.Tables["asptblmenuname"];
            //    param = Convert.ToInt32("0" + dt.Rows[0]["menuid"].ToString());

            //     pop8(param);



            //}
            Class.Users.UserTime = 0;

            // Map each tab to its corresponding pop method
            var tabActions = new Dictionary<string, Action<int>>()
{
    { "Masters", pop1 },
    { "Transactions", pop2 },
    { "Hostel", pop3 },
    { "Fuel", pop4 },
    { "Canteen", pop5 },
    { "Reports", pop6 },
    { "TreeView", pop7 },
    { "Registration", pop8 },
    { "GatePass", pop9 }
};
            
            // Get the selected tab
            var selectedTab = tabControl1.SelectedTab;
            if (selectedTab != null)
            {
                string tabName = selectedTab.Name; // use Name if consistent, or Text
                if (tabActions.TryGetValue(tabName, out Action<int> action))
                {
                    // Common SQL query
                    string sel = $@"
            SELECT DISTINCT A.menuid, A.MENUNAME
            FROM asptbluserrights A
            JOIN asptblusermas B ON A.USERNAME = B.USERID
            JOIN gtcompmast C ON C.GTCOMPMASTID = A.COMPCODE AND C.GTCOMPMASTID = B.COMPCODE
            WHERE A.PARENTMENUID = 1 
              AND A.ACTIVE = 'T'
              AND B.USERNAME = '{Class.Users.HUserName}'
              AND C.COMPCODE = '{Class.Users.HCompcode}'
              AND A.MENUNAME = '{selectedTab.Text.Trim()}'
            ORDER BY 1";

                    DataSet ds = Utility.ExecuteSelectQuery(sel, "asptblmenuname");
                    DataTable dt = ds.Tables["asptblmenuname"];

                    if (dt.Rows.Count > 0)
                    {
                        int param = Convert.ToInt32("0" + dt.Rows[0]["menuid"].ToString());
                        action(param); // call the corresponding pop method
                    }
                }
            }

        }



        private void colors1_Load(object sender, EventArgs e)
        {
            colors1.backcolor1.Click += Backcolor1_Click;
            colors1.backcolor2.Click += Backcolor2_Click;
            colors1.backcolor3.Click += Backcolor3_Click;
            colors1.backcolor4.Click += Backcolor4_Click;
            colors1.backcolor5.Click += Backcolor5_Click;
            
        }       
     

        private void Backcolor1_Click(object sender, EventArgs e)
        {
            lblMarquee1.ForeColor = Color.White;
            lbldashboard.ForeColor = Color.White;
            butrightborder.BackColor = colors1.backcolor1.BackColor;
            this.Masters.ForeColor = colors1.backcolor1.ForeColor;
            this.panelheader.BackColor = colors1.backcolor1.BackColor;
             this.statusStrip.BackColor = colors1.backcolor1.BackColor;
            Class.Users.BackColors = colors1.backcolor1.BackColor;
            this.butfooter.BackColor = colors1.backcolor1.BackColor;
            Class.Users.FontName = colors1.backcolor1.Font;
            Class.Users.ForeColors = colors1.backcolor1.ForeColor;
            Class.Users.ColorID = "Backcolor1_Click";
            tabControl1_SelectedIndexChanged(sender, e);
            News_Click(sender, e);
        }
        private void Backcolor2_Click(object sender, EventArgs e)
        {
            lblMarquee1.ForeColor = Color.White;
            lbldashboard.ForeColor = Color.White; 
            butrightborder.BackColor = colors1.backcolor2.BackColor;
            this.Masters.ForeColor = colors1.backcolor2.ForeColor;
            Class.Users.FontName = colors1.backcolor1.Font;
            this.panelheader.BackColor = colors1.backcolor2.BackColor;
            this.statusStrip.BackColor = colors1.backcolor2.BackColor;
            Class.Users.BackColors= colors1.backcolor2.BackColor;
            this.butfooter.BackColor = colors1.backcolor2.BackColor;
            Class.Users.ForeColors = colors1.backcolor1.ForeColor;
            Class.Users.ColorID = "Backcolor2_Click";
            tabControl1_SelectedIndexChanged(sender, e);
            News_Click(sender, e);
        }
        private void Backcolor3_Click(object sender, EventArgs e)
        {
            lblMarquee1.ForeColor = Color.White;
            lbldashboard.ForeColor = Color.White; 
            butrightborder.BackColor = colors1.backcolor3.BackColor;
            this.Masters.ForeColor = colors1.backcolor3.BackColor;
            this.panelheader.BackColor = colors1.backcolor3.BackColor;
            this.statusStrip.BackColor = colors1.backcolor3.BackColor;
            Class.Users.BackColors = colors1.backcolor3.BackColor;
            this.butfooter.BackColor = colors1.backcolor3.BackColor;
            Class.Users.FontName = colors1.backcolor1.Font;
            Class.Users.ForeColors = colors1.backcolor1.ForeColor;
            Class.Users.ColorID = "Backcolor3_Click";
            tabControl1_SelectedIndexChanged(sender, e);
            News_Click(sender, e);
        }
        private void Backcolor4_Click(object sender, EventArgs e)
        {
            lblMarquee1.ForeColor = Color.White;
            lbldashboard.ForeColor = Color.White; 
            butrightborder.BackColor = colors1.backcolor4.BackColor;
            this.Masters.ForeColor = colors1.backcolor4.ForeColor;
            this.panelheader.BackColor = colors1.backcolor4.BackColor;
             this.statusStrip.BackColor = colors1.backcolor4.BackColor;
            Class.Users.BackColors = colors1.backcolor4.BackColor;
            this.butfooter.BackColor = colors1.backcolor4.BackColor;
            Class.Users.FontName = colors1.backcolor1.Font;
            Class.Users.ForeColors = colors1.backcolor4.ForeColor;
            Class.Users.ColorID = "Backcolor4_Click";
            tabControl1_SelectedIndexChanged(sender, e);
            News_Click(sender, e);
        }
        private void Backcolor5_Click(object sender, EventArgs e)
        {
            lblMarquee1.ForeColor = Color.White;
            lbldashboard.ForeColor = Color.White;
            butrightborder.BackColor = colors1.backcolor5.BackColor;
            this.Masters.ForeColor = colors1.backcolor5.ForeColor;
            this.panelheader.BackColor = colors1.backcolor5.BackColor;
            this.statusStrip.BackColor = colors1.backcolor5.BackColor;
            Class.Users.BackColors = colors1.backcolor5.BackColor;
            this.butfooter.BackColor = colors1.backcolor5.BackColor;
            Class.Users.FontName = colors1.backcolor5.Font;
            Class.Users.ForeColors = colors1.backcolor5.ForeColor;
            Class.Users.ColorID = "Backcolor5_Click";
            tabControl1_SelectedIndexChanged(sender, e);
            News_Click(sender, e);
        }

        private void TabCtrl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TabCtrl.TabCount > 0)
            {
                GlobalVariables.CurrentForm = TabCtrl.SelectedTab.Controls.OfType<Form>().First();
                Class.Users.UserTime = 0;
                string[] s;
                s = GlobalVariables.CurrentForm.ToString().Split(','); 
                Class.Users.ScreenName = s[1].Substring(7).TrimEnd();
                usercheck(Class.Users.HCompcode,Class.Users.HUserName,Class.Users.ScreenName);
                GlobalVariables.HeaderName.Text = TabCtrl.SelectedTab.Text;
                Class.Users.ScreenName = TabCtrl.SelectedTab.Text;
               
            }
        }

        void ToolStripAccess.Searchs(int id)
        {
            throw new NotImplementedException();
        }

        void ToolStripAccess.Deletes(int id)
        {
            throw new NotImplementedException();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblMarquee2_Click(object sender, EventArgs e)
        {

        }

        private void TabCtrl_TabIndexChanged(object sender, EventArgs e)
        {
            MessageBox.Show("D");
        }

        //private void checkdatabase_CheckedChanged(object sender, EventArgs e)
        //{
        //    Models.Master mas = new Models.Master();
        //    mas.DatabaseCheck(checkdatabase);
        //}
    }
}
