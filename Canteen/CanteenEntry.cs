using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Pinnacle.Canteen
{
    public partial class CanteenEntry : Form,ToolStripAccess
    {
        public CanteenEntry()
        {
            InitializeComponent();
            Class.Users.IPADDRESS = GenFun.GetLocalIPAddress();
            Class.Users.CREATED = Convert.ToDateTime(System.DateTime.Now.ToString("dd-MMM-yyyy") + " " + System.DateTime.Now.ToLongTimeString());
            usercheck(Class.Users.HCompcode, Class.Users.HUserName, Class.Users.ScreenName);
        }
        private static CanteenEntry _instance;
        Models.Master mas = new Models.Master();
        Models.UserRights sm = new Models.UserRights();
        Models.MenuName c = new Models.MenuName();
 
        public static CanteenEntry Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new CanteenEntry();
                GlobalVariables.CurrentForm = _instance;
                return _instance;
            }

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
                        if (dt1.Rows[r]["READONLY"].ToString() == "T") { GlobalVariables.ReadOnlys.Visible = true; } else { GlobalVariables.ReadOnlys.Visible = false; }
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



        private void CanteenEntry_Load(object sender, EventArgs e)
        {
            GridLoad();
            txtcantokenno.Select();
        }
     public  void GridLoad()
        {
            try
            {
                listcanitem.Items.Clear();



                string sel1 = "   SELECT DISTINCT  C.ASPTBLCANTOKENID,C.TOKENNO,D.IDCARDNO,C.EMPID, D.EMPNAME,C.TOKENNOCANCEL,A.EMPLOYEECOST FROM ASPTBLCANITEMMAS A JOIN ASPTBLCANCATEGORYMAS B ON A.CATEGORY = B.ASPTBLCANCATEGORYMASID  JOIN ASPTBLCANTOKEN C ON C.ITEMNAME1 = A.ASPTBLCANITEMMASID  JOIN ASPTBLEMP D ON D.ASPTBLEMPID = C.EMPID  WHERE  C.TOKENNOCANCEL='T' ORDER BY 1";
                DataSet ds1 = Utility.ExecuteSelectQuery(sel1, "ASPTBLCANTOKEN");
                DataTable dt1 = ds1.Tables["ASPTBLCANTOKEN"];
                if (dt1.Rows.Count > 0)
                {
                    int mycount = 1;
                    foreach (DataRow myRow in dt1.Rows)
                    {
                        ListViewItem list = new ListViewItem();
                        list.Text = mycount.ToString();
                        list.SubItems.Add(myRow["ASPTBLCANTOKENID"].ToString());
                        list.SubItems.Add(myRow["TOKENNO"].ToString());
                        list.SubItems.Add(myRow["IDCARDNO"].ToString());
                        list.SubItems.Add(myRow["EMPID"].ToString());
                        list.SubItems.Add(myRow["EMPNAME"].ToString());
                        list.SubItems.Add(myRow["TOKENNOCANCEL"].ToString());
                        list.SubItems.Add(myRow["EMPLOYEECOST"].ToString());
                        if (mycount % 2 == 0)
                        {
                            list.BackColor = Color.WhiteSmoke;
                        }
                        else
                        {
                            list.BackColor = Color.White;
                        }
                        listcanitem.Items.Add(list);
                        mycount++;
                    }
                    lblcanitemtotal.Text = "Total Rows    :" + listcanitem.Items.Count;

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Saves_Click(object sender, EventArgs e)
        {

        }

        private void txtcantokenno_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtcantokenno.Text.Length == 18)
                {

                    string sel4 = "   SELECT A.ASPTBLCANTOKENID, A.TOKENNO,A.TOKENNOCANCEL FROM ASPTBLCANTOKEN A  WHERE  A.TOKENNO='" + txtcantokenno.Text + "' AND A.TOKENNOCANCEL IS NULL ";
                    DataSet ds4 = Utility.ExecuteSelectQuery(sel4, "ASPTBLCANTOKEN");
                    DataTable dt4 = ds4.Tables["ASPTBLCANTOKEN"];
                    int cnt = dt4.Rows.Count;
                    if (dt4.Rows.Count == 0)
                    {
                        MessageBox.Show("This Token Number Invalid.  " + "  " + txtcantokenno.Text, " Invalid ", MessageBoxButtons.OK, MessageBoxIcon.Stop); txtcantokenno.Text = ""; txtcantokenno.Focus();
                    }
                    else if (dt4.Rows[0]["TOKENNOCANCEL"].ToString() == "F" || dt4.Rows[0]["TOKENNOCANCEL"].ToString() == "T")
                    {
                        MessageBox.Show("This Token Transaction already completed. " + txtcantokenno.Text + " \n Date : " + dt4.Rows[0]["MODIFIED"].ToString(), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                        txtcantokenno.Text = ""; txtcantokenno.Focus();
                    }
                    else
                    {
                        //DialogResult result1 = MessageBox.Show("Do You want to Cancel??\n' THIS TOKEN NO  :" + txtcantokenno.Text, "Canteen", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                        //if (result1.Equals(DialogResult.OK))
                        //{

                            string up = "UPDATE  ASPTBLCANTOKEN A SET A.TOKENNOCANCEL='T' , A.ASPTBLCANTOKENID=" + dt4.Rows[0]["ASPTBLCANTOKENID"].ToString() + "  WHERE  A.TOKENNO='" + txtcantokenno.Text + "' ";
                            Utility.ExecuteNonQuery(up);
                            MessageBox.Show("Transaction Completed.   "+ "  " + txtcantokenno.Text, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtcantokenno.Text = ""; txtcantokenno.Focus();

                            GridLoad();
                        //}
                        //else
                        //{
                        //    txtcantokenno.Text = ""; txtcantokenno.Focus();
                        //}


                    }
                }
            }
            catch (Exception EX)
            {
                MessageBox.Show("Invalid" + EX.Message.ToString());
            }
            
        }


        private void Listcanitem_ItemActivate(object sender, EventArgs e)
        {
            try
            {
                if (listcanitem.Items.Count > 0)
                {

                    txtcantokenno.Text = listcanitem.SelectedItems[0].SubItems[1].Text;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Txtitemsearch_TextChanged(object sender, EventArgs e)
        {
            if (txtitemsearch.Text.Length >= 1)
            {
                string sel1 = "  SELECT DISTINCT  C.ASPTBLCANTOKENID,C.TOKENNO,D.IDCARDNO,C.EMPID, D.EMPNAME,C.TOKENNOCANCEL,A.EMPLOYEECOST  FROM ASPTBLCANITEMMAS A JOIN ASPTBLCANCATEGORYMAS B ON A.CATEGORY = B.ASPTBLCANCATEGORYMASID  JOIN ASPTBLCANTOKEN C ON C.ITEMNAME1 = A.ASPTBLCANITEMMASID  JOIN ASPTBLEMP D ON D.ASPTBLEMPID = C.EMPID where  C.TOKENNOCANCEL='T' AND D.EMPNAME LIKE'%" + txtitemsearch.Text + "%' OR D.IDCARDNO LIKE'%" + txtitemsearch.Text + "%'  ORDER BY 1";
                DataSet ds1 = Utility.ExecuteSelectQuery(sel1, "ASPTBLCANTOKEN");
                DataTable dt1 = ds1.Tables["ASPTBLCANTOKEN"];
              
                if (dt1.Rows.Count > 0)
                {
                    int mycount = 1;
                   
                    foreach (DataRow myRow in dt1.Rows)
                    {

                        ListViewItem list = new ListViewItem();
                        list.Text = mycount.ToString();
                        list.SubItems.Add(myRow["ASPTBLCANTOKENID"].ToString());
                        list.SubItems.Add(myRow["TOKENNO"].ToString());
                        list.SubItems.Add(myRow["IDCARDNO"].ToString());
                        list.SubItems.Add(myRow["EMPID"].ToString());
                        list.SubItems.Add(myRow["EMPNAME"].ToString());
                        list.SubItems.Add(myRow["TOKENNOCANCEL"].ToString());
                        list.SubItems.Add(myRow["EMPLOYEECOST"].ToString());
                        listcanitem.Items.Add(list);
                        mycount++;
                    }
                    lblcanitemtotal.Text = "Total Rows    :" + listcanitem.Items.Count;

                }
                else
                {
                    listcanitem.Items.Clear();
                }
            }
            else
            {
                listcanitem.Items.Clear();
                GridLoad();
            }
        }

        private void News_Click(object sender, EventArgs e)
        {

        }

        public void News()
        {
            txtcantokenno.Text = "";

            //butheader.Text = Class.Users.ScreenName;
           // butheader.BackColor = Class.Users.BackColors;
           // butfooter.BackColor = Class.Users.BackColors;
            panel2.BackColor = Class.Users.BackColors;
            panel3.BackColor = Class.Users.BackColors;
        }

        public void Saves()
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
            throw new NotImplementedException();
        }

        public void Deletes(int id)
        {
            throw new NotImplementedException();
        }
    }
}
