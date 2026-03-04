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
    public partial class ItemMaster : Form,ToolStripAccess
    {
        public ItemMaster()
        {
            InitializeComponent();
            //  usercheck(Class.Users.HCompcode, Class.Users.HUserName, Class.Users.ScreenName);
            Class.Users.IPADDRESS = GenFun.GetLocalIPAddress();


        }
        private static ItemMaster _instance;
        Models.Master mas = new Models.Master();
        Models.UserRights sm = new Models.UserRights();
        Models.MenuName c = new Models.MenuName();
        byte[]bytes; OpenFileDialog open = new OpenFileDialog();
        Int64 TOTALBYTES = 0;
        public static ItemMaster Instance
        {
            get { if (_instance == null) _instance = new ItemMaster(); 
                GlobalVariables.CurrentForm = _instance; return _instance; }

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
       

        
       public void GridLoad()
        {
            try
            {


                int iGLCount = 1;
                string sel1 = "  SELECT A.ASPTBLCANITEMMASID,  A.ITEMCODE,A.ITEMNAME1, A.EMPLOYEECOST,A.CONTRACTORCOST,A.ACTIVE  FROM  ASPTBLCANITEMMAS A   order by a.ASPTBLCANITEMMASID desc";

                DataSet ds = Utility.ExecuteSelectQuery(sel1, "ASPTBLCANITEMMAS");
                DataTable dt = ds.Tables["ASPTBLCANITEMMAS"];
                if (dt.Rows.Count > 0)
                {
                    listcanitem.Items.Clear();
                    foreach (DataRow myRow in dt.Rows)
                    {
                        ListViewItem list = new ListViewItem();
                        list.Text = iGLCount.ToString();
                        list.SubItems.Add(myRow["ASPTBLCANITEMMASID"].ToString());
                        list.SubItems.Add(myRow["ITEMCODE"].ToString());
                        list.SubItems.Add(myRow["ITEMNAME1"].ToString());           
                        list.SubItems.Add(myRow["EMPLOYEECOST"].ToString());
                        list.SubItems.Add(myRow["CONTRACTORCOST"].ToString());
                        list.SubItems.Add(myRow["ACTIVE"].ToString());
                        if (iGLCount % 2 == 0) { list.BackColor = Class.Users.Color1; } else { list.BackColor = Class.Users.Color2; };
                        listcanitem.Items.Add(list);
                        iGLCount++;
                    }
                    lblcanitemtotal.Text = "Total Count    :" + listcanitem.Items.Count;
                }
                else
                {
                    listcanitem.Items.Clear();

                   

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            //try
            //{
            //    listcanitem.Items.Clear();
            //    string sel1 = "  SELECT A.ASPTBLCANITEMMASID,  A.ITEMCODE,A.ITEMNAME1, B.CATEGORY,A.EMPLOYEECOST, A.CONTRACTORCOST,A.ACTIVE  FROM  ASPTBLCANITEMMAS A  JOIN ASPTBLCANCATEGORYMAS B ON A.CATEGORY=B.ASPTBLCANCATEGORYMASID WHERE B.ACTIVE='T' ORDER BY 1";
            //    DataSet ds1 = Utility.ExecuteSelectQuery(sel1, "ASPTBLCANITEMMAS");
            //    DataTable dt1 = ds1.Tables["ASPTBLCANITEMMAS"];
            //    if (dt1.Rows.Count > 0)
            //    {
            //        int mycount = 1;
            //        foreach (DataRow myRow in dt1.Rows)
            //        {
            //            ListViewItem list = new ListViewItem();
            //            list.Text = mycount.ToString();
            //            list.SubItems.Add(myRow["ASPTBLCANITEMMASID"].ToString());
            //            list.SubItems.Add(myRow["ITEMCODE"].ToString());
            //            list.SubItems.Add(myRow["ITEMNAME1"].ToString());
            //            list.SubItems.Add(myRow["CATEGORY"].ToString());

            //                list.SubItems.Add(myRow["EMPLOYEECOST"].ToString());
            //            list.SubItems.Add(myRow["CONTRACTORCOST"].ToString());                       
            //            list.SubItems.Add(myRow["ACTIVE"].ToString());
            //            if (mycount % 2 == 0)
            //            {
            //                list.BackColor = Color.WhiteSmoke;
            //            }
            //            else
            //            {
            //                list.BackColor = Color.White;
            //            }
            //            listcanitem.Items.Add(list);
            //            mycount++;
            //        }
            //        lblcanitemtotal.Text = "Total Rows    :" + listcanitem.Items.Count;

            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }


        private void Txtitemsearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
               
                if (txtitemsearch.Text != "")
                {
                    int iGLCount = 1;
                    string sel1 = "  SELECT A.ASPTBLCANITEMMASID,  A.ITEMCODE,A.ITEMNAME1, A.EMPLOYEECOST,A.CONTRACTORCOST,A.ACTIVE  FROM  ASPTBLCANITEMMAS A  WHERE A.ITEMNAME1 LIKE'%" + txtitemsearch.Text + "%'  or B.CATEGORY LIKE'%" + txtitemsearch.Text + "%'  OR A.ACTIVE LIKE'%" + txtitemsearch.Text + "%'";
                    DataSet ds = Utility.ExecuteSelectQuery(sel1, "ASPTBLCANITEMMAS");
                    DataTable dt = ds.Tables["ASPTBLCANITEMMAS"];
                    if (dt.Rows.Count > 0)
                    {
                        listcanitem.Items.Clear();
                        foreach (DataRow myRow in dt.Rows)
                        {
                            ListViewItem list = new ListViewItem();
                            list.Text = iGLCount.ToString();
                            list.SubItems.Add(myRow["ASPTBLCANITEMMASID"].ToString());
                            list.SubItems.Add(myRow["ITEMCODE"].ToString());
                            list.SubItems.Add(myRow["ITEMNAME1"].ToString());                                 
                          
                            list.SubItems.Add(myRow["EMPLOYEECOST"].ToString());
                            list.SubItems.Add(myRow["CONTRACTORCOST"].ToString());
                            list.SubItems.Add(myRow["ACTIVE"].ToString());
                            listcanitem.Items.Add(list);
                            iGLCount++;
                        }
                        lblcanitemtotal.Text = "Total Count    :" + listcanitem.Items.Count;
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void listcanitem_ItemActivate(object sender, EventArgs e)
        {
            try
            {
                if (listcanitem.Items.Count > 0)
                {
                    pictureitem.Image = null;
                    txtitemid.Text = listcanitem.SelectedItems[0].SubItems[1].Text;
                    string sel1 = "SELECT A.ASPTBLCANITEMMASID,  A.ITEMCODE,A.ITEMNAME1,A.EMPLOYEECOST,A.CONTRACTORCOST,A.ITEMIMAGE,A.ACTIVE,specialcost,A.ITEMDATE  FROM  ASPTBLCANITEMMAS A    WHERE A.ASPTBLCANITEMMASID=" + txtitemid.Text;
                    DataSet ds = Utility.ExecuteSelectQuery(sel1, "ASPTBLCANITEMMAS");
                    DataTable dt = ds.Tables["ASPTBLCANITEMMAS"];
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow myRow in dt.Rows)
                        {
                            txtitemid.Text = Convert.ToString(myRow["ASPTBLCANITEMMASID"].ToString());

                            txtitemcode.Text = Convert.ToString(myRow["ITEMCODE"].ToString());
                            txtitemname.Text = Convert.ToString(myRow["ITEMNAME1"].ToString());
                           
                            txtempcost.Text = Convert.ToString(myRow["EMPLOYEECOST"].ToString());
                            txtcontcost.Text = Convert.ToString(myRow["CONTRACTORCOST"].ToString());
                            txtspecialcost.Text = Convert.ToString(myRow["specialcost"].ToString());
                            if (myRow["ITEMIMAGE"].ToString() != "")
                            {

                                bytes = (byte[])myRow["ITEMIMAGE"];
                                Image img = Models.Device.ByteArrayToImage(bytes);
                                pictureitem.Image = img;


                            }
                            if (dt.Rows[0]["ACTIVE"].ToString() == "T") { checkactive.Checked = true; } else { checkactive.Checked = false; }
                            dateTimePicker2.Text = Convert.ToString(dt.Rows[0]["ITEMDATE"].ToString());
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void ItemMaster_Load(object sender, EventArgs e)
        {
            
        }

        private void Pictureitem_Click(object sender, EventArgs e)
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
                        TOTALBYTES = bytes.Length;

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Label7_Click(object sender, EventArgs e)
        {

        }

        private void Saves_Click(object sender, EventArgs e)
        {
           
            Saves();
        }

        //private void Combocategory_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (txtitemid.Text == "" && Convert.ToInt64("0" + combocategory.SelectedValue) > 0)
        //        {
        //            string sel2 = "  SELECT A.ASPTBLCANITEMMASID,  A.ITEMCODE,A.ITEMNAME1 FROM  ASPTBLCANITEMMAS A  WHERE  a.itemname1='" + txtitemname.Text.ToUpper() + "' AND A.category='" + combocategory.SelectedValue + "'  order by 2";
        //            DataSet ds2 = Utility.ExecuteSelectQuery(sel2, "ASPTBLCANITEMMAS");
        //            DataTable dt2 = ds2.Tables["ASPTBLCANITEMMAS"];
        //            if (dt2.Rows.Count > 0)
        //            {
        //                if (dt2.Rows[0]["ITEMNAME1"].ToString() == txtitemname.Text.ToUpper())
        //                {
        //                    combocategory.Text = ""; txtitemcode.Text = ""; pictureitem.Image = null;
        //                    MessageBox.Show("Duplicate Item " + txtitemname.Text.ToUpper());
        //                    txtitemname.Text = ""; txtitemname.Select();
        //                    return;
        //                }
        //            }
        //            else
        //            {

        //                string sel = "select MAX(A.ASPTBLCANITEMMASID)+1 AS  ID FROM ASPTBLCANITEMMAS A ";
        //                DataSet ds = Utility.ExecuteSelectQuery(sel, "ASPTBLCANITEMMAS");
        //                DataTable dt = ds.Tables["ASPTBLCANITEMMAS"];
        //                if (dt.Rows[0]["ID"].ToString() == "" && txtitemid.Text == "")
        //                {
        //                    txtitemcode.Text = combocategory.Text + 1;
        //                }
        //                else
        //                {
        //                    txtitemcode.Text = combocategory.Text + dt.Rows[0]["ID"].ToString();
        //                }


        //            }
        //        }
        //        else
        //        {
        //            string sel = "select A.ASPTBLCANITEMMASID  FROM ASPTBLCANITEMMAS A  WHERE  a.ASPTBLCANITEMMASid='" + txtitemid.Text + "' ";
        //            DataSet ds = Utility.ExecuteSelectQuery(sel, "ASPTBLCANITEMMAS");
        //            DataTable dt = ds.Tables["ASPTBLCANITEMMAS"];
        //            if (dt.Rows.Count > 0)
        //            {
        //                txtitemcode.Text = combocategory.Text + dt.Rows[0]["ASPTBLCANITEMMASID"].ToString();

        //            }
        //        }
        //    }
        //    catch (Exception EX)
        //    {
               
        //    }
        //}

        private void Txtitemcost_TextChanged(object sender, EventArgs e)
        {

        }

        private void Label6_Click(object sender, EventArgs e)
        {

        }

        private void Checkactive_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Label4_Click(object sender, EventArgs e)
        {

        }

        private void Txtitemname_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void Label2_Click(object sender, EventArgs e)
        {

        }

        private void Txtitemid_TextChanged(object sender, EventArgs e)
        {

        }

        public void News()
        {
            txtitemid.Text = ""; 
            txtitemcode.Text = "";
            txtitemname.Text = "";
            txtcontcost.Text = "";txtspecialcost.Text = "";
            txtempcost.Text = "";
            checkactive.Checked = true;
            pictureitem.Image = null; 
            this.Font= Class.Users.FontName;

            this.BackColor= Class.Users.BackColors;
            panel2.BackColor = Class.Users.BackColors;
            panel3.BackColor = Class.Users.BackColors;

            GridLoad();

        }

        public void Saves()
        {
            try
            {

                if (txtitemid.Text == "" && txtitemname.Text != "")
                {
                    txtitemcode.Text = "";
                    string sel2 = "  SELECT A.ASPTBLCANITEMMASID,  A.ITEMCODE,A.ITEMNAME1 FROM  ASPTBLCANITEMMAS A  WHERE  a.itemname1='" + txtitemname.Text.ToUpper() + "'  order by 2";
                    DataSet ds2 = Utility.ExecuteSelectQuery(sel2, "ASPTBLCANITEMMAS");
                    DataTable dt2 = ds2.Tables["ASPTBLCANITEMMAS"];
                    if (dt2.Rows.Count > 0)
                    {
                        if (dt2.Rows[0]["ITEMNAME1"].ToString() == txtitemname.Text.ToUpper())
                        {
                             txtitemcode.Text = ""; pictureitem.Image = null;
                            MessageBox.Show("Duplicate Item " + txtitemname.Text.ToUpper());
                            txtitemname.Text = ""; txtitemname.Select();
                            return;
                        }
                    }
                    else
                    {

                        string sel = "select MAX(A.ASPTBLCANITEMMASID)+1 AS  ID FROM ASPTBLCANITEMMAS A ";
                        DataSet ds = Utility.ExecuteSelectQuery(sel, "ASPTBLCANITEMMAS");
                        DataTable dt = ds.Tables["ASPTBLCANITEMMAS"];
                        if (dt.Rows[0]["ID"].ToString() == "")
                        {
                            txtitemcode.Text =  "1";
                        }
                        else
                        {
                            txtitemcode.Text =  dt.Rows[0]["ID"].ToString();
                        }




                    }
                }     
                
                string chk = "";
                DateTime dtmonth = DateTime.Today;
                string thisMonth = dtmonth.ToString("MMMM");
                if (txtitemname.Text == "" || txtitemname.Text == null)
                {
                    MessageBox.Show("Invalid  Items  : " + txtitemname.Text.ToString());
                    txtitemname.Select();
                }
                if (txtitemcode.Text !="" && txtempcost.Text != "")
                {
                    if (checkactive.Checked == true) { chk = "T"; } else { chk = "F"; }
                        string sel = "select A.ASPTBLCANITEMMASID FROM ASPTBLCANITEMMAS A  WHERE  A.ITEMCODE='" + txtitemcode.Text + "' AND  A.ITEMNAME1='" + txtitemname.Text + "' AND A.EMPLOYEECOST='" + txtempcost.Text + "' AND A.CONTRACTORCOST='" + txtcontcost.Text + "' and a.specialcost='" + txtspecialcost.Text + "' AND  A.ACTIVE='" + chk + "'  AND  A.ITEMBYTES='" + Convert.ToInt64("0"+ TOTALBYTES) + "' ";
                        DataSet ds = Utility.ExecuteSelectQuery(sel, "ASPTBLCANITEMMAS");//and a.ITEMBYTES='" + Convert.ToInt64("0"+bytes.Length) + "'
                        DataTable dt = ds.Tables["ASPTBLCANITEMMAS"];
                        if (dt.Rows.Count != 0)
                        {
                            MessageBox.Show("Child Record Found    :" + txtitemname.Text, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                    else if (dt.Rows.Count == 0 && Convert.ToInt32("0" + txtitemid.Text) == 0 || Convert.ToInt32("0" + txtitemid.Text) == 0)
                    {

                        string ins = "INSERT INTO ASPTBLCANITEMMAS(ITEMCODE,ITEMNAME1,EMPLOYEECOST,CONTRACTORCOST,ITEMCOST,ACTIVE, MODIFIED,CREATEDON,IPADDRESS,specialcost,itemdate,month,COMPCODE)" +
                            "VALUES('" + txtitemcode.Text.ToUpper().Trim() + "','" + txtitemname.Text.ToUpper().Trim() + "','" + txtempcost.Text + "','" + txtcontcost.Text + "','" + txtempcost.Text + "','" + chk + "','" + Convert.ToString(Class.Users.CREATED) + "','" + Convert.ToString(Class.Users.CREATED) + "','" + Class.Users.IPADDRESS + "','" + txtspecialcost.Text + "',to_date('" + dateTimePicker2.Text + "', 'dd-MM-yyyy'),'" + thisMonth + "','"+Class.Users.COMPCODE+ "')";
                        Utility.ExecuteNonQuery(ins);

                        if (pictureitem.Image != null)
                        {
                            string sel2 = "select max(A.ASPTBLCANITEMMASID) as ASPTBLCANITEMMASID FROM ASPTBLCANITEMMAS A";
                            DataSet ds2 = Utility.ExecuteSelectQuery(sel2, "ASPTBLCANITEMMAS");//and a.ITEMBYTES='" + Convert.ToInt64("0"+bytes.Length) + "'
                            DataTable dt2 = ds2.Tables["ASPTBLCANITEMMAS"];

                            string up = "UPDATE ASPTBLCANITEMMAS SET  ITEMIMAGE=:EMPIMAGE where    ASPTBLCANITEMMASID='" + dt2.Rows[0]["ASPTBLCANITEMMASID"].ToString() + "'" ;
                            Utility.ExecuteNonQuery(up, bytes);
                        }
                        MessageBox.Show("Record Saved Successfully     :" + txtitemname.Text, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                       
                    }
                    else
                    {
                        if (pictureitem.Image != null)
                        {  
                            string up = "UPDATE ASPTBLCANITEMMAS SET  ITEMIMAGE=:EMPIMAGE where    ASPTBLCANITEMMASID='" + txtitemid.Text + "'";
                            Utility.ExecuteNonQuery(up, bytes);
                        }
                        string up1 = "UPDATE ASPTBLCANITEMMAS SET    ITEMCODE='" + txtitemcode.Text.ToUpper().Trim() + "', ITEMNAME1='" + txtitemname.Text.ToUpper().Trim() + "',EMPLOYEECOST='" + txtempcost.Text + "',CONTRACTORCOST='" + txtcontcost.Text + "',specialcost='" + txtspecialcost.Text + "', ITEMCOST='"+ txtempcost.Text + "', ACTIVE='" + chk + "', MODIFIED='" + Convert.ToString(Class.Users.CREATED).ToString() + "',IPADDRESS='" + Class.Users.IPADDRESS + "',itemdate=to_date('" + dateTimePicker2.Text + "', 'dd-MM-yyyy'),month='" + thisMonth + "',COMPCODE='"+Class.Users.COMPCODE+"' WHERE  ASPTBLCANITEMMASID=" + txtitemid.Text;
                        Utility.ExecuteNonQuery(up1);
                        MessageBox.Show("Record Updated      :" + txtitemname.Text, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    GridLoad();
                    News();
                }
                else
                {
                    MessageBox.Show("Invalid  Items  : "+ txtitemname.Text.ToString());
                    txtitemname.Select();
                }
            }
            catch (Exception ex)
            {
                
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
            if (txtitemid.Text != "")
            {
                string sel1 = "select B.ASPTBLCANITEMMASID from asptblmenper  a join ASPTBLCANITEMMAS b on a.ASPTBLCANITEMMASID=b.ASPTBLCANITEMMASID where B.ASPTBLCANITEMMASID='" + txtitemid.Text + "'";
                DataSet ds = Utility.ExecuteSelectQuery(sel1, "ASPTBLCANITEMMAS");
                DataTable dt = ds.Tables["ASPTBLCANITEMMAS"];
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("Child Record Found.Can Not Delete." + txtitemid.Text, " Alert Message ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                }
                else
                {

                    string sel2 = "DELETE  FROM ASPTBLCANITEMMAS WHERE ASPTBLCANITEMMASID=" + txtitemid.Text;
                    Utility.ExecuteNonQuery(sel2);
                    MessageBox.Show("Record Deleted Successfully " + txtitemid.Text, " Delete Message ", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
            //GlobalVariables.MdiPanel.Show();
            //News();
            //GlobalVariables.HeaderName.Text = "";
            //GlobalVariables.TabCtrl.TabPages.RemoveAt(GlobalVariables.TabCtrl.SelectedIndex);
            //this.Hide();
            News();
            this.Hide(); 
            GlobalVariables.MdiPanel.Show();
            GlobalVariables.TabCtrl.TabPages.RemoveAt(GlobalVariables.TabCtrl.SelectedIndex);
           
            GlobalVariables.HeaderName.Text = "";
        

        }

        public void Searchs(int id)
        {
            throw new NotImplementedException();
        }

        public void Deletes(int id)
        {
            throw new NotImplementedException();
        }

        private void searchcombocategory_SelectedIndexChanged(object sender, EventArgs e)
        {
          
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void txtcontcost_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= '0' && e.KeyChar <= '9' || e.KeyChar == '.' || e.KeyChar == (char)Keys.Back) //The  character represents a backspace
            {
                e.Handled = false; //Do not reject the input
            }
            else
            {
                e.Handled = true; //Reject the input
            }
        }

        private void txtspecialcost_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= '0' && e.KeyChar <= '9' || e.KeyChar == '.' || e.KeyChar == (char)Keys.Back) //The  character represents a backspace
            {
                e.Handled = false; //Do not reject the input
            }
            else
            {
                e.Handled = true; //Reject the input
            }
        }

        private void txtempcost_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= '0' && e.KeyChar <= '9' || e.KeyChar == '.' || e.KeyChar == (char)Keys.Back) //The  character represents a backspace
            {
                e.Handled = false; //Do not reject the input
            }
            else
            {
                e.Handled = true; //Reject the input
            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void txtitemname_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 'A' && e.KeyChar <= 'Z' || e.KeyChar >= 'a' && e.KeyChar <= 'z' || e.KeyChar == (char)Keys.Back) //The  character represents a backspace
            {
                e.Handled = false; //Do not reject the input
            }
            else
            {
                e.Handled = true; //Reject the input
            }
        }

        private void butDatewise_Click(object sender, EventArgs e)
        {
            //try
            //{


            //    int iGLCount = 1;
            //   // string sel1 = "  SELECT A.ASPTBLCANITEMMASID,  A.ITEMCODE,A.ITEMNAME1, B.CATEGORY,A.EMPLOYEECOST,A.CONTRACTORCOST,A.ACTIVE  FROM  ASPTBLCANITEMMAS A  JOIN ASPTBLCANCATEGORYMAS B ON A.CATEGORY=B.ASPTBLCANCATEGORYMASID JOIN GTCOMPMAST C ON C.GTCOMPMASTID=A.COMPCODE   JOIN ASPTBLUSERMAS D ON D.COMPCODE=A.COMPCODE  AND D.USERID=A.USERNAME WHERE  C.COMPCODE='" + Class.Users.HCompcode + "' and  d.userid='" + Class.Users.USERID + "' AND A.ITEMDATE = to_date('" + dateTimePicker1.Text + "', 'dd-MM-yyyy')  order by 2";
            //    string sel1 = "  SELECT A.ASPTBLCANITEMMASID,  A.ITEMCODE,A.ITEMNAME1, B.CATEGORY,A.EMPLOYEECOST,A.CONTRACTORCOST,A.ACTIVE  FROM  ASPTBLCANITEMMAS A  JOIN ASPTBLCANCATEGORYMAS B ON A.CATEGORY=B.ASPTBLCANCATEGORYMASID JOIN GTCOMPMAST C ON C.GTCOMPMASTID=A.COMPCODE   JOIN ASPTBLUSERMAS D ON D.COMPCODE=A.COMPCODE  AND D.USERID=A.USERNAME WHERE  C.COMPCODE='" + Class.Users.HCompcode + "' and  d.userid='" + Class.Users.USERID + "'  order by 2";

            //    DataSet ds = Utility.ExecuteSelectQuery(sel1, "ASPTBLCANITEMMAS");
            //    DataTable dt = ds.Tables["ASPTBLCANITEMMAS"];
            //    if (dt.Rows.Count > 0)
            //    {
            //        listcanitem.Items.Clear();
            //        foreach (DataRow myRow in dt.Rows)
            //        {
            //            ListViewItem list = new ListViewItem();
            //            list.Text = iGLCount.ToString();
            //            list.SubItems.Add(myRow["ASPTBLCANITEMMASID"].ToString());
            //            list.SubItems.Add(myRow["ITEMCODE"].ToString());
            //            list.SubItems.Add(myRow["ITEMNAME1"].ToString());
            //            list.SubItems.Add(myRow["CATEGORY"].ToString());

            //            list.SubItems.Add(myRow["EMPLOYEECOST"].ToString());
            //            list.SubItems.Add(myRow["CONTRACTORCOST"].ToString());
            //            list.SubItems.Add(myRow["ACTIVE"].ToString());
            //            if (iGLCount % 2 == 0) { list.BackColor = Class.Users.Color1; } else { list.BackColor = Class.Users.Color2; };
            //            listcanitem.Items.Add(list);
            //            iGLCount++;
            //        }
            //        lblcanitemtotal.Text = "Total Count    :" + listcanitem.Items.Count;
            //    }
            //    else
            //    {
            //        listcanitem.Items.Clear();

            //        listcanitem.Items.Clear();
       
            //    }

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }

        private void txtempcost_TextChanged(object sender, EventArgs e)
        {
           
        }
    }
}
