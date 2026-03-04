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
    public partial class UserRights : Form,ToolStripAccess
    {
        private static UserRights _instance;
        Models.Master mas = new Models.Master();
        Models.UserRights sm = new Models.UserRights();
        Models.Device dev = new Models.Device();
        private readonly object sender;

        public static UserRights Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new UserRights();
                GlobalVariables.CurrentForm = _instance; return _instance;
            }
        }
        public void ReadOnlys()
        {

        }
        public UserRights()
        {
            InitializeComponent();
           // usercheck(Class.Users.HCompcode, Class.Users.HUserName, Class.Users.ScreenName);
            GlobalVariables.HeaderName.Text = Class.Users.ScreenName; GlobalVariables.CurrentForm = this;
            butheader.Text = Class.Users.ScreenName;
            panel2.BackColor = Class.Users.BackColors;         
            butheader.BackColor = Class.Users.BackColors;
            butfooter.BackColor = Class.Users.BackColors;
            dataGridView1.ForeColor = Class.Users.BackColors;
            mas.DatabaseCheck(checkdatabase);
        }

       
        private void comcode()
        {
            DataTable dt = mas.comcode1();
            if (dt.Rows.Count >= 0)
            {
                combocompcode.DisplayMember = "COMPCODE";
                combocompcode.ValueMember = "gtcompmastid";
                combocompcode.DataSource = dt;


            }
            else
            {
                combocompcode.DataSource = null;
            }
           

        }

      
    

      

        public void GridLoad()
        {
            try
            {
                DataTable dt1 = sm.headerdropdowns(combocompcode.Text, combousername.Text);

                if (dt1.Rows.Count > 0)
                {
                    dataGridView1.DataSource = dt1;
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {

                        //dataGridView1.Rows[i].Cells[1].Value = Convert.ToInt64("0" + dt1.Rows[i]["USERRIGHTSID"].ToString());
                        //dataGridView1.Rows[i].Cells[2].Value = Convert.ToInt64("0" + dt1.Rows[i]["MENUID"].ToString());
                        //dataGridView1.Rows[i].Cells[3].Value = Convert.ToString(dt1.Rows[i]["MENUNAME"].ToString());
                        //dataGridView1.Rows[i].Cells[4].Value = Convert.ToString(dt1.Rows[i]["NAVURL"].ToString());
                        //dataGridView1.Rows[i].Cells[5].Value = Convert.ToInt64("0" + dt1.Rows[i]["parentmenuid"].ToString());
                        if (dt1.Rows[i]["ACTIVE"].ToString() == "T") { dataGridView1.Rows[i].Cells[6].Value = true; } else { dataGridView1.Rows[i].Cells[6].Value = false; }
                        if (dt1.Rows[i]["NEW1"].ToString() == "T") { dataGridView1.Rows[i].Cells[7].Value = true; } else { dataGridView1.Rows[i].Cells[7].Value = false; }
                        if (dt1.Rows[i]["SAVE1"].ToString() == "T") { dataGridView1.Rows[i].Cells[8].Value = true; } else { dataGridView1.Rows[i].Cells[8].Value = false; }
                        if (dt1.Rows[i]["PRINT1"].ToString() == "T") { dataGridView1.Rows[i].Cells[9].Value = true; } else { dataGridView1.Rows[i].Cells[9].Value = false; }
                        if (dt1.Rows[i]["READONLY"].ToString() == "T") { dataGridView1.Rows[i].Cells[10].Value = true; } else { dataGridView1.Rows[i].Cells[10].Value = false; }
                        if (dt1.Rows[i]["SEARCH"].ToString() == "T") { dataGridView1.Rows[i].Cells[11].Value = true; } else { dataGridView1.Rows[i].Cells[11].Value = false; }
                        if (dt1.Rows[i]["DELETE1"].ToString() == "T") { dataGridView1.Rows[i].Cells[12].Value = true; } else { dataGridView1.Rows[i].Cells[12].Value = false; }
                        if (dt1.Rows[i]["TREEBUTTON"].ToString() == "T") { dataGridView1.Rows[i].Cells[13].Value = true; } else { dataGridView1.Rows[i].Cells[13].Value = false; }
                        if (dt1.Rows[i]["GLOBALSEARCH"].ToString() == "T") { dataGridView1.Rows[i].Cells[14].Value = true; } else { dataGridView1.Rows[i].Cells[14].Value = false; }
                        if (dt1.Rows[i]["LOGIN"].ToString() == "T") { dataGridView1.Rows[i].Cells[15].Value = true; } else { dataGridView1.Rows[i].Cells[15].Value = false; }
                        if (dt1.Rows[i]["CHANGEPASSWORD"].ToString() == "T") { dataGridView1.Rows[i].Cells[16].Value = true; } else { dataGridView1.Rows[i].Cells[16].Value = false; }
                        if (dt1.Rows[i]["CHANGESKIN"].ToString() == "T") { dataGridView1.Rows[i].Cells[17].Value = true; } else { dataGridView1.Rows[i].Cells[17].Value = false; }
                        if (dt1.Rows[i]["DOWNLOAD"].ToString() == "T") { dataGridView1.Rows[i].Cells[18].Value = true; } else { dataGridView1.Rows[i].Cells[18].Value = false; }
                        if (dt1.Rows[i]["CONTACT"].ToString() == "T") { dataGridView1.Rows[i].Cells[19].Value = true; } else { dataGridView1.Rows[i].Cells[19].Value = false; }
                        if (dt1.Rows[i]["PDF"].ToString() == "T") { dataGridView1.Rows[i].Cells[20].Value = true; } else { dataGridView1.Rows[i].Cells[20].Value = false; }
                        if (dt1.Rows[i]["IMPORT1"].ToString() == "T") { dataGridView1.Rows[i].Cells[21].Value = true; } else { dataGridView1.Rows[i].Cells[21].Value = false; }

                      
                        //dataGridView1.Rows[i].Cells[22].Value = Convert.ToString(dt1.Rows[i]["gtcompmastid"].ToString());
                        //dataGridView1.Rows[i].Cells[23].Value = Convert.ToString(dt1.Rows[i]["USERID"].ToString());
                    }

                }
                else
                {
                    dataGridView1.DataSource = dt1;
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        dataGridView1.Rows[i].Cells[i].Value = false;
                    }

                   // MessageBox.Show("Data not Found", "Error");
                }
            }
            catch (Exception ex)
            {
               MessageBox.Show(ex.Message.ToString());
            }
        }
        private void UserRights_Load(object sender, EventArgs e)
        {
            try
            {
                comcode();
                combousername.SelectedIndex = -1; combocompcode.SelectedIndex = -1;
                combocompcode.Select();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void UserRights_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }



        public void News()
        {



            panel2.BackColor = Class.Users.BackColors;
            butheader.BackColor = Class.Users.BackColors;
            butfooter.BackColor = Class.Users.BackColors;
            dataGridView1.ForeColor = Class.Users.BackColors;
            GlobalVariables.HeaderName.Text = "";

            checkReadony.Checked = false; checksearch.Checked = false; checkDelete.Checked = false; checkSave.Checked = false; checkNew.Checked = false; checkactive.Checked = false;
            GridLoad();
        }

        private void Combousername_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                if (combousername.SelectedIndex >= 0)
                {
                    mas.DatabaseCheck(checkdatabase);
                    DataTable dt1 = sm.headerdropdowns(combocompcode.Text,combousername.Text);
                    if (dt1.Rows.Count > 0)
                    {
                        dataGridView1.DataSource = dt1;
                        for (int i = 0; i < dt1.Rows.Count; i++)
                        {

                            ////dataGridView1.Rows[i].Cells[1].Value = Convert.ToInt64("0" + dt1.Rows[i]["USERRIGHTSID"].ToString());
                            ////dataGridView1.Rows[i].Cells[2].Value = Convert.ToInt64("0" + dt1.Rows[i]["MENUID"].ToString());
                            ////dataGridView1.Rows[i].Cells[3].Value = Convert.ToString(dt1.Rows[i]["MENUNAME"].ToString());
                            ////dataGridView1.Rows[i].Cells[4].Value = Convert.ToString(dt1.Rows[i]["NAVURL"].ToString());
                            ////dataGridView1.Rows[i].Cells[5].Value = Convert.ToInt64("0" + dt1.Rows[i]["parentmenuid"].ToString());
                            ///

                            if (dt1.Rows[i]["ACTIVE"].ToString() == "T") { dataGridView1.Rows[i].Cells[6].Value = true; } else { dataGridView1.Rows[i].Cells[6].Value = false; }
                            if (dt1.Rows[i]["NEW1"].ToString() == "T") { dataGridView1.Rows[i].Cells[7].Value = true; } else { dataGridView1.Rows[i].Cells[7].Value = false; }
                            if (dt1.Rows[i]["SAVE1"].ToString() == "T") { dataGridView1.Rows[i].Cells[8].Value = true; } else { dataGridView1.Rows[i].Cells[8].Value = false; }
                            if (dt1.Rows[i]["PRINT1"].ToString() == "T") { dataGridView1.Rows[i].Cells[9].Value = true; } else { dataGridView1.Rows[i].Cells[9].Value = false; }
                            if (dt1.Rows[i]["READONLY"].ToString() == "T") { dataGridView1.Rows[i].Cells[10].Value = true; } else { dataGridView1.Rows[i].Cells[10].Value = false; }
                            if (dt1.Rows[i]["SEARCH"].ToString() == "T") { dataGridView1.Rows[i].Cells[11].Value = true; } else { dataGridView1.Rows[i].Cells[11].Value = false; }
                            if (dt1.Rows[i]["DELETE1"].ToString() == "T") { dataGridView1.Rows[i].Cells[12].Value = true; } else { dataGridView1.Rows[i].Cells[12].Value = false; }
                            if (dt1.Rows[i]["TREEBUTTON"].ToString() == "T") { dataGridView1.Rows[i].Cells[13].Value = true; } else { dataGridView1.Rows[i].Cells[13].Value = false; }
                            if (dt1.Rows[i]["GLOBALSEARCH"].ToString() == "T") { dataGridView1.Rows[i].Cells[14].Value = true; } else { dataGridView1.Rows[i].Cells[14].Value = false; }
                            if (dt1.Rows[i]["LOGIN"].ToString() == "T") { dataGridView1.Rows[i].Cells[15].Value = true; } else { dataGridView1.Rows[i].Cells[15].Value = false; }
                            if (dt1.Rows[i]["CHANGEPASSWORD"].ToString() == "T") { dataGridView1.Rows[i].Cells[16].Value = true; } else { dataGridView1.Rows[i].Cells[16].Value = false; }
                            if (dt1.Rows[i]["CHANGESKIN"].ToString() == "T") { dataGridView1.Rows[i].Cells[17].Value = true; } else { dataGridView1.Rows[i].Cells[17].Value = false; }
                            if (dt1.Rows[i]["DOWNLOAD"].ToString() == "T") { dataGridView1.Rows[i].Cells[18].Value = true; } else { dataGridView1.Rows[i].Cells[18].Value = false; }
                            if (dt1.Rows[i]["CONTACT"].ToString() == "T") { dataGridView1.Rows[i].Cells[19].Value = true; } else { dataGridView1.Rows[i].Cells[19].Value = false; }
                            if (dt1.Rows[i]["PDF"].ToString() == "T") { dataGridView1.Rows[i].Cells[20].Value = true; } else { dataGridView1.Rows[i].Cells[20].Value = false; }
                            if (dt1.Rows[i]["IMPORT1"].ToString() == "T") { dataGridView1.Rows[i].Cells[21].Value = true; } else { dataGridView1.Rows[i].Cells[21].Value = false; }


                            //if (dt1.Rows[i]["ACTIVE"].ToString() == "T") { dataGridView1.Rows[i].Cells[5].Value = true; } else { dataGridView1.Rows[i].Cells[5].Value = false; }
                            //if (dt1.Rows[i]["NEW1"].ToString() == "T") { dataGridView1.Rows[i].Cells[6].Value = true; } else { dataGridView1.Rows[i].Cells[6].Value = false; }
                            //if (dt1.Rows[i]["SAVE1"].ToString() == "T") { dataGridView1.Rows[i].Cells[7].Value = true; } else { dataGridView1.Rows[i].Cells[7].Value = false; }
                            //if (dt1.Rows[i]["PRINT1"].ToString() == "T") { dataGridView1.Rows[i].Cells[8].Value = true; } else { dataGridView1.Rows[i].Cells[8].Value = false; }
                            //if (dt1.Rows[i]["READONLY"].ToString() == "T") { dataGridView1.Rows[i].Cells[9].Value = true; } else { dataGridView1.Rows[i].Cells[9].Value = false; }
                            //if (dt1.Rows[i]["SEARCH"].ToString() == "T") { dataGridView1.Rows[i].Cells[10].Value = true; } else { dataGridView1.Rows[i].Cells[10].Value = false; }
                            //if (dt1.Rows[i]["DELETE1"].ToString() == "T") { dataGridView1.Rows[i].Cells[11].Value = true; } else { dataGridView1.Rows[i].Cells[11].Value = false; }
                            //if (dt1.Rows[i]["TREEBUTTON"].ToString() == "T") { dataGridView1.Rows[i].Cells[12].Value = true; } else { dataGridView1.Rows[i].Cells[12].Value = false; }
                           
                            //    if (dt1.Rows[i]["GLOBALSEARCH"].ToString() == "T") { dataGridView1.Rows[i].Cells[13].Value = true; } else { dataGridView1.Rows[i].Cells[13].Value = false; }
                           
                            //if (dt1.Rows[i]["LOGIN"].ToString() == "T") { dataGridView1.Rows[i].Cells[14].Value = true; } else { dataGridView1.Rows[i].Cells[14].Value = false; }
                            //if (dt1.Rows[i]["CHANGEPASSWORD"].ToString() == "T") { dataGridView1.Rows[i].Cells[15].Value = true; } else { dataGridView1.Rows[i].Cells[15].Value = false; }
                            //if (dt1.Rows[i]["CHANGESKIN"].ToString() == "T") { dataGridView1.Rows[i].Cells[16].Value = true; } else { dataGridView1.Rows[i].Cells[16].Value = false; }
                            //if (dt1.Rows[i]["DOWNLOAD"].ToString() == "T") { dataGridView1.Rows[i].Cells[17].Value = true; } else { dataGridView1.Rows[i].Cells[17].Value = false; }
                            //if (dt1.Rows[i]["CONTACT"].ToString() == "T") { dataGridView1.Rows[i].Cells[18].Value = true; } else { dataGridView1.Rows[i].Cells[18].Value = false; }
                            //if (dt1.Rows[i]["PDF"].ToString() == "T") { dataGridView1.Rows[i].Cells[19].Value = true; } else { dataGridView1.Rows[i].Cells[19].Value = false; }
                            //if (dt1.Rows[i]["IMPORT1"].ToString() == "T") { dataGridView1.Rows[i].Cells[20].Value = true; } else { dataGridView1.Rows[i].Cells[20].Value = false; }
                          
                        }

                    }
                    else
                    {
                        dataGridView1.DataSource = dt1;
                        for (int i = 0; i < dt1.Rows.Count; i++)
                        {
                            dataGridView1.Rows[i].Cells[i].Value = false;
                        }

                        MessageBox.Show("Screen Rights UnDefined", "Error");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
           
        }

        private void Combocompcode_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            try
            {
                if (combocompcode.SelectedIndex >= 0)
                {

                    mas.DatabaseCheck(checkdatabase);
                    Int64 s = Convert.ToInt64(combocompcode.SelectedValue);
                    DataTable dt1 = mas.comcode1(s);
                    if (dt1.Rows.Count > 0)
                    {
                        combousername.DisplayMember = "USERNAME";
                        combousername.ValueMember = "USERID";
                        combousername.DataSource = dt1;

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
           this.combousername.Focus();
        }

        public void Saves()
        {
            try
            {
                if (combocompcode.Text != "" && combousername.Text != "")
                {
                    mas.DatabaseCheck(checkdatabase);
                    Models.UserRights c = new Models.UserRights();
                    int cc = dataGridView1.Rows.Count;
                    for (int i = 0; i < cc; i++)
                    {
                        
                        c.UserRightsID = Convert.ToInt64("0" + dataGridView1.Rows[i].Cells[1].Value.ToString());
                        c.MenuID = Convert.ToInt64("0" + dataGridView1.Rows[i].Cells[2].Value.ToString());
                        c.MenuName = Convert.ToString(dataGridView1.Rows[i].Cells[3].Value.ToString());
                        c.NavURL = Convert.ToString(dataGridView1.Rows[i].Cells[4].Value.ToString());
                        c.ParentMenuID = Convert.ToInt64("0" + dataGridView1.Rows[i].Cells[5].Value.ToString());
                        if (dataGridView1.Rows[i].Cells[6].Value.ToString() == "True") c.Active = "T"; else c.Active = "F";
                        if (dataGridView1.Rows[i].Cells[7].Value.ToString() == "True") c.News = "T"; else c.News = "F";
                        if (dataGridView1.Rows[i].Cells[8].Value.ToString() == "True") c.Save = "T"; else c.Save = "F";
                        if (dataGridView1.Rows[i].Cells[9].Value.ToString() == "True") c.Print = "T"; else c.Print = "F";
                        if (dataGridView1.Rows[i].Cells[10].Value.ToString() == "True") c.ReadOnly = "T"; else c.ReadOnly = "F";
                        if (dataGridView1.Rows[i].Cells[11].Value.ToString() == "True") c.Search = "T"; else c.Search = "F";
                        if (dataGridView1.Rows[i].Cells[12].Value.ToString() == "True") c.Delete = "T"; else c.Delete = "F";
                        if (dataGridView1.Rows[i].Cells[13].Value.ToString() == "True") c.TreeButton = "T"; else c.TreeButton = "F";
                        if (dataGridView1.Rows[i].Cells[14].Value.ToString() == "True") c.GlobalSearch = "T"; else c.GlobalSearch = "F";
                        if (dataGridView1.Rows[i].Cells[15].Value.ToString() == "True") c.Login = "T"; else c.Login = "F";
                        if (dataGridView1.Rows[i].Cells[16].Value.ToString() == "True") c.ChangePassword = "T"; else c.ChangePassword = "F";
                        if (dataGridView1.Rows[i].Cells[17].Value.ToString() == "True") c.ChangeSkin = "T"; else c.ChangeSkin = "F";
                        if (dataGridView1.Rows[i].Cells[18].Value.ToString() == "True") c.DownLoad = "T"; else c.DownLoad = "F";
                        if (dataGridView1.Rows[i].Cells[19].Value.ToString() == "True") c.Contact = "T"; else c.Contact = "F";
                        if (dataGridView1.Rows[i].Cells[20].Value.ToString() == "True") c.Pdf = "T"; else c.Pdf = "F";
                        if (dataGridView1.Rows[i].Cells[21].Value.ToString() == "True") c.Imports = "T"; else c.Imports = "F";
                        c.CompCode = Convert.ToInt64("0" + combocompcode.SelectedValue);
                        c.UserName = Convert.ToInt64("0" + combousername.SelectedValue);
                        c.Sno = i;
                        c.IpAddress = GenFun.GetLocalIPAddress();
                        c.Createdby = Class.Users.HUserName;
                        c.Createdon = Convert.ToDateTime(System.DateTime.Now.ToString("dd-MMM-yyyy") + " " + System.DateTime.Now.ToLongTimeString());
                        c.Modified = Convert.ToDateTime(System.DateTime.Now.ToString("dd-MMM-yyyy") + " " + System.DateTime.Now.ToLongTimeString());


                        if (Convert.ToInt64(c.ParentMenuID) >= 0)
                        {
                            mas.DatabaseCheck(checkdatabase);
                            DataTable dt3 = c.select(c.MenuID, c.MenuName,c.NavURL, c.ParentMenuID, c.Active, c.News, c.Save, c.Print, c.ReadOnly, c.Search, c.Delete, c.CompCode, c.UserName, c.TreeButton, c.GlobalSearch, c.Login, c.ChangePassword, c.ChangeSkin, c.DownLoad, c.Contact, c.Pdf, c.Imports);
                            if (dt3.Rows.Count != 0) { }
                            else if (dt3.Rows.Count != 0 && c.UserRightsID == 0 || c.UserRightsID == 0)
                            {
                                c = new Models.UserRights(c.MenuID, c.MenuName, c.NavURL, c.ParentMenuID, c.Active, c.News, c.Save, c.Print, c.ReadOnly, c.Search, c.Delete, c.CompCode, c.UserName, c.TreeButton, c.GlobalSearch, c.Login, c.ChangePassword, c.ChangeSkin, c.DownLoad, c.Contact, c.Pdf, c.Imports, c.Createdon, c.Modified, c.Sno, c.IpAddress);
                            }
                            else
                            {
                                c = new Models.UserRights(c.MenuID, c.MenuName, c.NavURL, c.ParentMenuID, c.Active, c.News, c.Save, c.Print, c.ReadOnly, c.Search, c.Delete, c.CompCode, c.UserName, c.TreeButton, c.GlobalSearch, c.Login, c.ChangePassword, c.ChangeSkin, c.DownLoad, c.Contact, c.Pdf, c.Imports, c.Modified, c.UserRightsID);
                            }
                        }
                        else
                        {
                            MessageBox.Show("invlaid");
                        }
                    }



                    MessageBox.Show("Record Saved Successfully.");
                    News();
                }
                else
                {
                    MessageBox.Show("Pls select CompCode and UserName");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }

        private void Txtuserrightssearch_TextChanged(object sender, EventArgs e)
        {
            try
            {

                if (combocompcode.Text != "" || combousername.Text != "" && txtuserrightssearch.Text != "")
                {
                    
                    string sel1 = "SELECT A.USERRIGHTSID,A.MENUID,A.MENUNAME,A.NAVURL ,A.parentmenuid,A.ACTIVE,A.NEWS AS NEW1,A.SAVES AS SAVE1,A.PRINTS AS PRINT1,A.READONLY,A.SEARCH,A.DELETES AS DELETE1,A.TREEBUTTON,A.GLOBALSEARCH,A.LOGIN,A.CHANGEPASSWORD,A.CHANGESKIN,A.DOWNLOAD,A.CONTACT,A.PDF,A.IMPORTS AS IMPORT1,B.gtcompmastid,C.USERID FROM ASPTBLUSERRIGHTS A JOIN GTCOMPMAST B ON   B.gtcompmastid = A.COMPCODE JOIN asptblusermas C ON C.USERID = A.USERNAME " +
                        "WHERE B.COMPCODE = '" +combocompcode.Text + "' AND C.USERNAME = '" + combousername.Text + "'AND A.MENUNAME LIKE'%" + txtuserrightssearch.Text + "%'  AND A.MENUNAME LIKE'%" + txtuserrightssearch.Text + "%'  ORDER BY 2,1 ";
                    DataSet ds = Utility.ExecuteSelectQuery(sel1, "ASPTBLUSERRIGHTS");
                    DataTable dt1 = ds.Tables["ASPTBLUSERRIGHTS"];
                    if (dt1.Rows.Count > 0)
                    {
                        dataGridView1.DataSource = dt1;

                        for (int i = 0; i < dt1.Rows.Count; i++)
                        {
                            if (dt1.Rows[i]["ACTIVE"].ToString() == "T") { dataGridView1.Rows[i].Cells[6].Value = true; } else { dataGridView1.Rows[i].Cells[6].Value = false; }
                            if (dt1.Rows[i]["NEW1"].ToString() == "T") { dataGridView1.Rows[i].Cells[7].Value = true; } else { dataGridView1.Rows[i].Cells[7].Value = false; }
                            if (dt1.Rows[i]["SAVE1"].ToString() == "T") { dataGridView1.Rows[i].Cells[8].Value = true; } else { dataGridView1.Rows[i].Cells[8].Value = false; }
                            if (dt1.Rows[i]["PRINT1"].ToString() == "T") { dataGridView1.Rows[i].Cells[9].Value = true; } else { dataGridView1.Rows[i].Cells[9].Value = false; }
                            if (dt1.Rows[i]["READONLY"].ToString() == "T") { dataGridView1.Rows[i].Cells[10].Value = true; } else { dataGridView1.Rows[i].Cells[10].Value = false; }
                            if (dt1.Rows[i]["SEARCH"].ToString() == "T") { dataGridView1.Rows[i].Cells[11].Value = true; } else { dataGridView1.Rows[i].Cells[11].Value = false; }
                            if (dt1.Rows[i]["DELETE1"].ToString() == "T") { dataGridView1.Rows[i].Cells[12].Value = true; } else { dataGridView1.Rows[i].Cells[12].Value = false; }
                            if (dt1.Rows[i]["TREEBUTTON"].ToString() == "T") { dataGridView1.Rows[i].Cells[13].Value = true; } else { dataGridView1.Rows[i].Cells[13].Value = false; }
                            if (dt1.Rows[i]["GLOBALSEARCH"].ToString() == "T") { dataGridView1.Rows[i].Cells[14].Value = true; } else { dataGridView1.Rows[i].Cells[14].Value = false; }
                            if (dt1.Rows[i]["LOGIN"].ToString() == "T") { dataGridView1.Rows[i].Cells[15].Value = true; } else { dataGridView1.Rows[i].Cells[15].Value = false; }
                            if (dt1.Rows[i]["CHANGEPASSWORD"].ToString() == "T") { dataGridView1.Rows[i].Cells[16].Value = true; } else { dataGridView1.Rows[i].Cells[16].Value = false; }
                            if (dt1.Rows[i]["CHANGESKIN"].ToString() == "T") { dataGridView1.Rows[i].Cells[17].Value = true; } else { dataGridView1.Rows[i].Cells[17].Value = false; }
                            if (dt1.Rows[i]["DOWNLOAD"].ToString() == "T") { dataGridView1.Rows[i].Cells[18].Value = true; } else { dataGridView1.Rows[i].Cells[18].Value = false; }
                            if (dt1.Rows[i]["CONTACT"].ToString() == "T") { dataGridView1.Rows[i].Cells[19].Value = true; } else { dataGridView1.Rows[i].Cells[19].Value = false; }
                            if (dt1.Rows[i]["PDF"].ToString() == "T") { dataGridView1.Rows[i].Cells[20].Value = true; } else { dataGridView1.Rows[i].Cells[20].Value = false; }
                            if (dt1.Rows[i]["IMPORT1"].ToString() == "T") { dataGridView1.Rows[i].Cells[21].Value = true; } else { dataGridView1.Rows[i].Cells[21].Value = false; }

                        }


                    }
                    else
                    {
                       
                        txtuserrightssearch.Focus();
                    }

                }
                else
                {
                    GridLoad();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
          
        }

        private void UserRightsRefreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Txtuserrightssearch_TextChanged(sender,e);
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void CompcodeRefreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            comcode();
        }

        private void checkall_CheckedChanged(object sender, EventArgs e)
        {

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
            this.Hide();
            GlobalVariables.MdiPanel.Show();
           
            News();
            GlobalVariables.HeaderName.Text = "";
            GlobalVariables.TabCtrl.TabPages.RemoveAt(GlobalVariables.TabCtrl.SelectedIndex);

        }

        public void usercheck(string s, string ss, string sss)
        {
            
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            this.dataGridView1.Rows[e.RowIndex].Cells["SNo"].Value = (e.RowIndex + 1).ToString();
        }

        private void checkactive_CheckedChanged(object sender, EventArgs e)
        {
            if (checkactive.Checked == true)
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    row.Cells[6].Value = true;
                }
            }
            else
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    row.Cells[6].Value = false;
                }
            }
        }
       

        private void checkNew_CheckedChanged(object sender, EventArgs e)
        {
            if (checkNew.Checked == true)
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    row.Cells[7].Value = true;
                }
            }
            else
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    row.Cells[7].Value = false;
                }
            }
        }

        private void checkSave_CheckedChanged(object sender, EventArgs e)
        {
            if (checkSave.Checked == true)
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    row.Cells[8].Value = true;
                }
            }
            else
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    row.Cells[8].Value = false;
                }
            }
        }

        private void checkDelete_CheckedChanged(object sender, EventArgs e)
        {
            if (checkDelete.Checked == true)
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    row.Cells[12].Value = true;
                }
            }
            else
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    row.Cells[12].Value = false;
                }
            }
        }
      

        private void checksearch_CheckedChanged(object sender, EventArgs e)
        {
            if (checksearch.Checked == true)
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    row.Cells[11].Value = true;
                }
            }
            else
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    row.Cells[11].Value = false;
                }
            }
        }

        private void checkReadony_CheckedChanged(object sender, EventArgs e)
        {
            if (checkReadony.Checked == true)
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    row.Cells[10].Value = true;
                }
            }
            else
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    row.Cells[10].Value = false;
                }
            }
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
    }
}
