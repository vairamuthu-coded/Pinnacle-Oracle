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
    public partial class TokenCancel : Form, ToolStripAccess
    {
        public TokenCancel()
        {
            InitializeComponent();
            Class.Users.IPADDRESS = GenFun.GetLocalIPAddress();
            Class.Users.CREATED = Convert.ToDateTime(System.DateTime.Now.ToString("dd-MMM-yyyy") + " " + System.DateTime.Now.ToLongTimeString());
            usercheck(Class.Users.HCompcode, Class.Users.HUserName, Class.Users.ScreenName);
            
        }
        private static TokenCancel _instance;
        Models.Master mas = new Models.Master();
        Models.UserRights sm = new Models.UserRights();
        Models.MenuName c = new Models.MenuName();
         OpenFileDialog open = new OpenFileDialog();ListView listfilter = new ListView();
        public static TokenCancel Instance
        {
            get { if (_instance == null)
                    _instance = new TokenCancel();
                GlobalVariables.CurrentForm = _instance;
                return _instance; }

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

        private void TokenCancel_Load(object sender, EventArgs e)
        {
            GridLoad();txtTokenNumber.Select();
        }

        private void TxtTokenNumber_TextChanged(object sender, EventArgs e)
        {
            
        }
       public void GridLoad()
        {
            try
            {
                listcanitem.Items.Clear();
                listfilter.Items.Clear();


                    string sel1 = "  SELECT DISTINCT  C.ASPTBLCANTOKENID,C.TOKENNO,E.MIDCARD,D.HREMPLOYMASTID, D.FNAME,C.TOKENNOCANCEL,C.TOTALAMOUNT  FROM ASPTBLCANITEMMAS A JOIN ASPTBLCANCATEGORYMAS B ON A.CATEGORY = B.ASPTBLCANCATEGORYMASID  JOIN ASPTBLCANTOKEN C ON C.ITEMNAME1 = A.ASPTBLCANITEMMASID JOIN HREMPLOYMAST D ON D.HREMPLOYMASTID = C.EMPID AND D.COMPCODE=C.COMPCODE JOIN HREMPLOYDETAILS E ON D.HREMPLOYMASTID = E.HREMPLOYMASTID   where C.TOKENNOCANCEL = 'F' ORDER BY 1";
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
                        list.SubItems.Add(myRow["MIDCARD"].ToString());
                        list.SubItems.Add(myRow["HREMPLOYMASTID"].ToString());
                        list.SubItems.Add(myRow["FNAME"].ToString());
                        list.SubItems.Add(myRow["TOKENNOCANCEL"].ToString());
                        list.SubItems.Add(myRow["TOTALAMOUNT"].ToString());
                        if (mycount % 2 == 0)
                        {
                            list.BackColor = Color.WhiteSmoke;
                        }
                        else
                        {
                            list.BackColor = Color.White;
                        }
                        this.listfilter.Items.Add((ListViewItem)list.Clone());
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
            Saves();
        }

        private void Listcanitem_ItemActivate(object sender, EventArgs e)
        {
            try
            {
                if (listcanitem.Items.Count > 0)
                {
                    txttokennum.Text = listcanitem.SelectedItems[0].SubItems[1].Text;
                    txtTokenNumber.Text = listcanitem.SelectedItems[0].SubItems[2].Text;
                   
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Txtitemsearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int item0 = 0;
                if (txtitemsearch.Text.Length > 0)
                {
                    listcanitem.Items.Clear();

                    foreach (ListViewItem item in listfilter.Items)
                    {
                        ListViewItem list = new ListViewItem();
                        if (listfilter.Items[item0].SubItems[3].ToString().Contains(txtitemsearch.Text) || listfilter.Items[item0].SubItems[5].ToString().Contains(txtitemsearch.Text))
                        {

                            list.Text = listfilter.Items[item0].SubItems[0].Text;
                            list.SubItems.Add(listfilter.Items[item0].SubItems[1].Text);
                            list.SubItems.Add(listfilter.Items[item0].SubItems[2].Text);
                            list.SubItems.Add(listfilter.Items[item0].SubItems[3].Text);
                            list.SubItems.Add(listfilter.Items[item0].SubItems[4].Text);
                            list.SubItems.Add(listfilter.Items[item0].SubItems[5].Text);
                            list.SubItems.Add(listfilter.Items[item0].SubItems[6].Text);
                            list.SubItems.Add(listfilter.Items[item0].SubItems[7].Text);
                        
                          


                            if (item0 % 2 == 0)
                            {
                                list.BackColor = Color.White;

                            }
                            else
                            {
                                list.BackColor = Color.WhiteSmoke;

                            }



                            listcanitem.Items.Add(list);
                        }

                        item0++;
                    }
                    lblcanitemtotal.Text = "Total Rows    :" + listcanitem.Items.Count;
                }
                else
                {
                    GridLoad();
                    //try
                    //{
                    //    listcanitem.Items.Clear();
                    //    foreach (ListViewItem item in listfilter.Items)
                    //    {
                    //        this.listcanitem.Items.Add((ListViewItem)item.Clone());
                    //        item0++;
                    //    }
                    //    lblcanitemtotal.Text = "Total Rows    :" + listcanitem.Items.Count;
                    //}
                    //catch (Exception ex)
                    //{

                    //}
                }
            }
            catch (Exception ex)
            {
                // MessageBox.Show("---" + ex.ToString());
            }
            
            
        }

        public void News()
        {
            Class.Users.UserTime = 0;txtitemsearch.Text = "";
            txttokennum.Text = "";txtTokenNumber.Text = "";
           // this.Font = Class.Users.FontName;           
            Class.Users.LoginTime = 600;
            panel2.BackColor = Class.Users.BackColors;
            panel3.BackColor = Class.Users.BackColors; txtTokenNumber.Select();
        }

        public void Saves()
        {
            try
            {
                if (txtTokenNumber.Text.Length == 18)
                {
                   
                    Int64 tokenid = Convert.ToInt64("0" + txttokennum.Text);
                    string sel1 = "   SELECT A.ASPTBLCANTOKENID,A.MODIFIED FROM ASPTBLCANTOKEN A  WHERE A.TOKENNO='" + txtTokenNumber.Text + "' ";
                    DataSet ds1 = Utility.ExecuteSelectQuery(sel1, "ASPTBLCANTOKEN");
                    DataTable dt1 = ds1.Tables["ASPTBLCANTOKEN"];
                    if(dt1.Rows.Count > 0){
                        string sel4 = "   SELECT A.ASPTBLCANTOKENID,A.MODIFIED FROM ASPTBLCANTOKEN A  WHERE A.TOKENNO='" + txtTokenNumber.Text + "' and a.TOKENNOCANCEL='F'";
                        DataSet ds4 = Utility.ExecuteSelectQuery(sel4, "ASPTBLCANTOKEN");
                        DataTable dt4 = ds4.Tables["ASPTBLCANTOKEN"];

                        if (dt4.Rows.Count != 0)
                        {
                            MessageBox.Show("This Token already Cancelled. " + txtTokenNumber.Text + " \n Date : " + dt4.Rows[0]["MODIFIED"].ToString(), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                            txtTokenNumber.Text = ""; txtTokenNumber.Focus(); txttokennum.Text = ""; return;
                        }
                        else if (dt4.Rows.Count == 0 && Convert.ToInt32("0" + txttokennum.Text) == 0 || Convert.ToInt32("0" + txttokennum.Text) == 0)
                        {

                            string up = "UPDATE  ASPTBLCANTOKEN A SET A.TOKENNOCANCEL='F'   WHERE  A.TOKENNO='" + txtTokenNumber.Text + "' ";
                            Utility.ExecuteNonQuery(up); this.lblcancelalert.Show();
                            MessageBox.Show("This Token are Cancelled Successfully " + txtTokenNumber.Text, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtTokenNumber.Text = ""; txtTokenNumber.Focus(); txttokennum.Text = "";
                            GridLoad();

                        }
                        else
                        {
                            txtTokenNumber.Text = ""; txtTokenNumber.Focus();
                        }

                    }
                    else
                    {
                        MessageBox.Show("Invalid Token Number");
                    }
                   
                }
            }
            catch (Exception EX)
            {
                MessageBox.Show("Invalid" + EX.Message.ToString());
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
