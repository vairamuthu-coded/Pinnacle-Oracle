using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OracleClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OracleParameter = System.Data.OracleClient.OracleParameter;

namespace Pinnacle.Canteen
{
    public partial class CanteenCategoryMaster : Form,ToolStripAccess
    {
        public CanteenCategoryMaster()
        {
            InitializeComponent();
            usercheck(Class.Users.HCompcode, Class.Users.HUserName, Class.Users.ScreenName);
         
            Class.Users.IPADDRESS = GenFun.GetLocalIPAddress();
            Class.Users.CREATED = Convert.ToDateTime(System.DateTime.Now.ToString("dd/MM/yyyy") + " " + System.DateTime.Now.ToLongTimeString());
        }
        Utility _utility = new Utility();
        Models.Master mas = new Models.Master();
        Models.UserRights sm = new Models.UserRights();
        Models.MenuName c = new Models.MenuName(); byte[] bytes;
        OpenFileDialog open = new OpenFileDialog();
  
        private static CanteenCategoryMaster _instance;
        public static CanteenCategoryMaster Instance
        {
            get { if (_instance == null) _instance = new CanteenCategoryMaster(); GlobalVariables.CurrentForm = _instance; return _instance; }

        }


       
        public void usercheck(string s, string ss, string sss)
        {

            DataTable dt1 = sm.headerdropdowns(s, ss, sss);
            if (dt1.Rows.Count > 0)
            {
                if (dt1.Rows[0]["Menuname"].ToString() == Class.Users.ScreenName)
                {
                    for (int r = 0; r < dt1.Rows.Count; r++)
                    {

                        if (dt1.Rows[r]["NEWS"].ToString() == "T") { GlobalVariables.News.Visible = true; } else { GlobalVariables.News.Visible = false; }
                        if (dt1.Rows[r]["SAVES"].ToString() == "T") { GlobalVariables.Saves.Visible = true; } else { GlobalVariables.Saves.Visible = false; }
                        if (dt1.Rows[r]["PRINTS"].ToString() == "T") { GlobalVariables.Prints.Visible = true; } else { GlobalVariables.Prints.Visible = false; }
                        if (dt1.Rows[r]["READONLY"].ToString() == "T") { GlobalVariables.ReadOnlys.Visible = false; } else { GlobalVariables.ReadOnlys.Visible = false; }
                        if (dt1.Rows[r]["SEARCH"].ToString() == "T") { GlobalVariables.Searchs.Visible = true; } else { GlobalVariables.Searchs.Visible = false; }
                        if (dt1.Rows[r]["DELETES"].ToString() == "T") { GlobalVariables.Deletes.Visible = true; } else { GlobalVariables.Deletes.Visible = false; }
                        if (dt1.Rows[r]["TREEBUTTON"].ToString() == "T") { GlobalVariables.TreeButtons.Visible = true; } else { GlobalVariables.TreeButtons.Visible = false; }
                        if (dt1.Rows[r]["GLOBALSEARCH"].ToString() == "T") { GlobalVariables.GlobalSearchs.Visible = true; } else { GlobalVariables.GlobalSearchs.Visible = false; }
                        if (dt1.Rows[r]["LOGIN"].ToString() == "T") { GlobalVariables.Logins.Visible = true; } else { GlobalVariables.Logins.Visible = false; }
                        if (dt1.Rows[r]["CHANGEPASSWORD"].ToString() == "T") { GlobalVariables.ChangePasswords.Visible = true; } else { GlobalVariables.ChangePasswords.Visible = false; }
                        if (dt1.Rows[r]["CHANGESKIN"].ToString() == "T") { GlobalVariables.ChangeSkins.Visible = true; } else { GlobalVariables.ChangeSkins.Visible = false; }
                        if (dt1.Rows[r]["DOWNLOAD"].ToString() == "T") { GlobalVariables.DownLoads.Visible = true; } else { GlobalVariables.DownLoads.Visible = false; }
                        if (dt1.Rows[r]["Pdf"].ToString() == "T") { GlobalVariables.Pdfs.Visible = true; } else { GlobalVariables.Pdfs.Visible = false; }
                        if (dt1.Rows[r]["Imports"].ToString() == "T") { GlobalVariables.Imports.Visible = true; } else { GlobalVariables.Imports.Visible = false; }
                    }
                }


            }
            else
            {

                GlobalVariables.Toolstrip1.Enabled = false;
            }

        }
        private void News_Click(object sender, EventArgs e)
        {
            News();
        }

        private void Saves_Click(object sender, EventArgs e)
        {
            Saves();
        }
        public void GridLoad()
        {
            try
            {
                listcategory.Items.Clear();
                string sel1 = "  SELECT A.ASPTBLCANCATEGORYMASID,  A.Category,A.ACTIVE  FROM  ASPTBLCANCATEGORYMAS A   ORDER BY 1";
                DataSet ds1 = Utility.ExecuteSelectQuery(sel1, "ASPTBLCANCATEGORYMAS");
                DataTable dt1 = ds1.Tables["ASPTBLCANCATEGORYMAS"];
                if (dt1.Rows.Count > 0)
                {
                    int mycount = 1;
                    foreach (DataRow myRow in dt1.Rows)
                    {
                        ListViewItem list = new ListViewItem();
                        list.Text = mycount.ToString();
                        list.SubItems.Add(myRow["ASPTBLCANCATEGORYMASID"].ToString());
                        list.SubItems.Add(myRow["Category"].ToString());                       
                        list.SubItems.Add(myRow["ACTIVE"].ToString());
                        if (mycount % 2 == 0)
                        {
                            list.BackColor = Color.White;
                        }
                        else
                        {
                            list.BackColor = Color.WhiteSmoke;
                        }
                      
                        listcategory.Items.Add(list);
                        mycount++;
                    }
                    lblcatetotal.Text = "Total Rows    :" + listcategory.Items.Count;
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
   

        private void Txtcancategorysearch_Click(object sender, EventArgs e)
        {
            try
            {
                listcategory.Items.Clear();
                if (txtcancategorysearch.Text != "")
                {
                    int iGLCount = 1;
                    string sel1 = "  SELECT  A.ASPTBLCANCATEGORYMASID,A.Category,A.ACTIVE FROM   ASPTBLCANCATEGORYMAS A WHERE A.Category LIKE'%" + txtcancategorysearch.Text + "%'  or A.ACTIVE LIKE'%" + txtcancategorysearch.Text + "%'";
                    DataSet ds = Utility.ExecuteSelectQuery(sel1, "ASPTBLCANCATEGORYMAS");
                    DataTable dt = ds.Tables["ASPTBLCANCATEGORYMAS"];
                    if (dt.Rows.Count > 0)
                    {

                        foreach (DataRow myRow in dt.Rows)
                        {
                            ListViewItem list = new ListViewItem();
                            list.Text = iGLCount.ToString();
                            list.SubItems.Add(myRow["ASPTBLCANCATEGORYMASID"].ToString());                         
                            list.SubItems.Add(myRow["Category"].ToString());
                            list.SubItems.Add(myRow["ACTIVE"].ToString());
                            if (iGLCount % 2 == 0)
                            {
                                list.BackColor = Color.WhiteSmoke;
                            }
                            else
                            {
                                list.BackColor = Color.White;
                            }
                            listcategory.Items.Add(list);
                            iGLCount++;
                        }
                        lblcatetotal.Text = "Total Count    :" + listcategory.Items.Count;
                    }
                }
                else
                {
                    GridLoad();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Listcategory_ItemActivate(object sender, EventArgs e)
        {
            try
            {
                if (listcategory.Items.Count > 0)
                {
                  
                    txtcategoryid.Text = listcategory.SelectedItems[0].SubItems[1].Text;
                    string sel1 = " SELECT A.ASPTBLCANCATEGORYMASID,A.Category,A.ACTIVE  FROM  ASPTBLCANCATEGORYMAS A WHERE A.ASPTBLCANCATEGORYMASID=" + txtcategoryid.Text;
                    DataSet ds = Utility.ExecuteSelectQuery(sel1, "ASPTBLCANCATEGORYMAS");
                    DataTable dt = ds.Tables["ASPTBLCANCATEGORYMAS"];
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow myRow in dt.Rows)
                        {
                            txtcategoryid.Text = Convert.ToString(myRow["ASPTBLCANCATEGORYMASID"].ToString());

                            txtcategory.Text = Convert.ToString(myRow["Category"].ToString());
                            //if (myRow["CATEGORYIMAGE"].ToString() != "")
                            //{

                            //    bytes = (byte[])myRow["CATEGORYIMAGE"];
                            //    Image img = Models.Device.ByteArrayToImage(bytes);
                            //    pictureitem.Image = img;


                            //}
                            if (dt.Rows[0]["ACTIVE"].ToString() == "T") { checkactive.Checked = true; } else { checkactive.Checked = false; }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

       

        private void CanteenCategoryMaster_Load(object sender, EventArgs e)
        {
            txtcategory.Select();
        }

        private void Pictureitem_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    bytes = null;
            //    PictureBox p = sender as PictureBox;
            //    if (p != null)
            //    {
            //        open.Filter = "(*.jpg;*.jpeg;*.bmp;)| *.jpg; *.jpeg; *.bmp";
            //        if (open.ShowDialog() == DialogResult.OK)
            //        {

            //            p.Image = new Bitmap(open.FileName);
            //            bytes = Models.Device.ImageToByteArray(p);                      

            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}
        }

        public void News()
        {
            txtcategoryid.Text = "";
            txtcategory.Text = "";
            checkactive.Checked = true;        
            this.Font = Class.Users.FontName;     
            panel2.BackColor = Class.Users.BackColors;
            panel3.BackColor = Class.Users.BackColors;
            txtcategory.Select(); 
            GridLoad();
        }

        public void Saves()
        {
            try
            {
                string chk = "";
                if (txtcategory.Text != "")
                {
                    if (checkactive.Checked == true) { chk = "T"; } else { chk = "F"; }
                    string sel = "select A.ASPTBLCANCATEGORYMASID FROM ASPTBLCANCATEGORYMAS A  WHERE  A.CATEGORY='" + txtcategory.Text.ToUpper().Trim() + "'  AND A.ACTIVE='" + chk + "' ";
                    DataSet ds = Utility.ExecuteSelectQuery(sel, "ASPTBLCANCATEGORYMAS");
                    DataTable dt = ds.Tables["ASPTBLCANCATEGORYMAS"];
                    if (dt.Rows.Count != 0)
                    {
                        MessageBox.Show("Child Record Found    :" + txtcategory.Text, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else if (dt.Rows.Count == 0 && Convert.ToInt32("0" + txtcategoryid.Text) == 0 || Convert.ToInt32("0" + txtcategoryid.Text) == 0)
                    {

                        string sql = @"INSERT INTO ASPTBLCANCATEGORYMAS(CATEGORY,ACTIVE,USERNAME,MODIFIED,CREATEDON,IPADDRESS)VALUES(    :CATEGORY,    :ACTIVE,    :USERNAME,    :MODIFIED,    :CREATEDON,    :IPADDRESS)";
                        OracleParameter[] parameters =
                        {
                        new OracleParameter("CATEGORY", OracleDbType.Varchar2)
                        { Value = txtcategory.Text.Trim().ToUpper() },

                        new OracleParameter("ACTIVE", OracleDbType.Int32)
                        { Value = chk },

                        new OracleParameter("USERNAME", OracleDbType.Int32)
                        { Value = Class.Users.USERID },

                        new OracleParameter("IPADDRESS", OracleDbType.Varchar2)
                        { Value = Class.Users.IPADDRESS }
                       };
                        
                        int result = Utility.ExecuteNonQuery(sql,parameters);

                        if (result > 0)
                            MessageBox.Show("Saved Successfully");
                        else
                            MessageBox.Show("Save Failed");
                    }


                
                else
                    {
                        string up = "UPDATE ASPTBLCANCATEGORYMAS SET   CATEGORY='" + txtcategory.Text.Trim().ToUpper() + "',ACTIVE='" + chk + "', USERNAME=" + Class.Users.USERID + ",  MODIFIED='" + Convert.ToString(Class.Users.CREATED).ToString() + "',IPADDRESS='" + Class.Users.IPADDRESS + "' WHERE  ASPTBLCANCATEGORYMASID=" + txtcategoryid.Text;
                        Utility.ExecuteNonQuery(up);
                        MessageBox.Show("Record Updated      :" + txtcategory.Text, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    GridLoad(); News();
                }
                else
                {
                    MessageBox.Show("Pls Enter Mandatory Fields");
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        public void Prints()
        {
          
        }

        public void Searchs()
        {
           
        }

        public void Deletes()
        {
            if (txtcategoryid.Text != "")
            {
                string sel1 = "select B.ASPTBLCANCATEGORYMASid from ASPTBLCANITEMMAS a join ASPTBLCANCATEGORYMAS b on a.category=b.ASPTBLCANCATEGORYMASid where B.ASPTBLCANCATEGORYMASid='" + txtcategoryid.Text + "'";
                DataSet ds = Utility.ExecuteSelectQuery(sel1, "ASPTBLCANITEMMAS");
                DataTable dt = ds.Tables["ASPTBLCANITEMMAS"];
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("Child Record Found.Can Not Delete." + txtcategoryid.Text, " Alert Message ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else
                {
                    string sel2 = "DELETE  FROM ASPTBLCANCATEGORYMAS WHERE ASPTBLCANCATEGORYMASid=" + txtcategoryid.Text;
                    Utility.ExecuteNonQuery(sel2); 
                    MessageBox.Show("Record Deleted Successfully " + txtcategoryid.Text, " Delete Message ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    GridLoad(); News();
                }
            }
           

        }

        public void ReadOnlys()
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
            News();
            GlobalVariables.HeaderName.Text = "";
            GlobalVariables.TabCtrl.TabPages.RemoveAt(GlobalVariables.TabCtrl.SelectedIndex);
            this.Hide();

        }

        public void Searchs(int id)
        {
           
        }

        public void Deletes(int id)
        {
           
        }

        private void txtcategory_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtcategory_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 'a' && e.KeyChar <= 'z' || e.KeyChar >= 'A' && e.KeyChar <= 'Z' || e.KeyChar == (char)Keys.Back) //The  character represents a backspace
            {
                e.Handled = false; //Do not reject the input
            }
            else
            {
                e.Handled = true; //Reject the input
            }
        }
    }
}
