using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Pinnacle.Models;
namespace Pinnacle.Transactions.Lovely
{
    public partial class MonthPermissionp : Form,ToolStripAccess
    {
        private static MonthPermissionp _instance;
        public MonthPermissionp()
        {
            InitializeComponent();
            GlobalVariables.HeaderName.Text = Class.Users.ScreenName; GlobalVariables.CurrentForm = this;


        }
        public static MonthPermissionp Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new MonthPermissionp();
                GlobalVariables.CurrentForm = _instance; return _instance;
            }
        }
        
       
      //  Report.Lovely.LovelySalarySlip1 rd = new Report.Lovely.LovelySalarySlip1();
        Models.Master mas = new Models.Master();
        Models.UserRights sm = new Models.UserRights();
        ListView listfilter = new ListView();
        ListView listfilter1 = new ListView(); DataTable dtgeneral = new DataTable();
        string idcardcount = ""; Byte[] bytes;
        ListView allip = new ListView();
        DataTable reversedDt; DataTable reversedDt1;
        DataTable reversedDt2; int i = 0; int j = 0;
        

        private void PaySlipPermission_Load(object sender, EventArgs e)
        {
        }
        public void Finyearload()
        {
            try
            {
            string sel0 = "  SELECT DISTINCT  GTFINANCIALYEARID,  FINYR AS  Finyear,CURRENTFINYR FROM  GTFINANCIALYEAR ";//WHERE E.CURRENTFINYR = 'T'";
                DataSet ds0 = Utility.ExecuteSelectQuery(sel0, "GTFINANCIALYEAR");
                DataTable dt = ds0.Tables["GTFINANCIALYEAR"];


                combofinyear.DisplayMember = "Finyear";
                combofinyear.ValueMember = "GTFINANCIALYEARID";
                combofinyear.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("companyload: " + ex.Message, " Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        public void companyload()
        {
            try
            {
                string sel0 = " SELECT DISTINCT B.GTCOMPMASTID,B.COMPCODE FROM  HREMPLOYMAST A JOIN GTCOMPMAST B ON A.COMPCODE=B.GTCOMPMASTID  JOIN HREMPLOYDETAILS C ON A.HREMPLOYMASTID=C.HREMPLOYMASTID JOIN GTFINANCIALYEAR E ON E.GTFINANCIALYEARID=C.FINYEAR  where  b.compcode='" + Class.Users.HCompcode + "' order by 1";
                DataSet ds0 = Utility.ExecuteSelectQuery(sel0, "hremploymast");
                DataTable dt = ds0.Tables["hremploymast"];

                combocompcode.DisplayMember = "COMPCODE";
                combocompcode.ValueMember = "GTCOMPMASTID";
                combocompcode.DataSource = dt;

            }
            catch (Exception ex)
            {
                MessageBox.Show("companyload: " + ex.Message, " Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        public void userload()
        {
            try
            {
                string sel0 = "SELECT 0 AS USERID,'' AS USERNAME  FROM DUAL   UNION ALL SELECT DISTINCT B.USERID, B.USERNAME FROM   GTCOMPMAST A  join asptblusermas B on B.COMPCODE=A.GTCOMPMASTID   where      A.compcode='" + Class.Users.HCompcode+"'  AND  NOT  B.username='VAIRAM'    order by 1";
                DataSet ds0 = Utility.ExecuteSelectQuery(sel0, "asptblusermas");
                DataTable dt = ds0.Tables["asptblusermas"];

                combouser.DisplayMember = "USERNAME";
                combouser.ValueMember = "USERID";
                combouser.DataSource = dt;

            }
            catch (Exception ex)
            {
                MessageBox.Show("userload: " + ex.Message, " Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        //public void companyload()
        //{
        //    try
        //    {
        //        string sel0 = "SELECT DISTINCT  E.GTFINANCIALYEARID,  E.FINYR AS  Finyear,B.GTCOMPMASTID,B.COMPCODE FROM  HREMPLOYMAST A JOIN GTCOMPMAST B ON A.COMPCODE=B.GTCOMPMASTID  JOIN HREMPLOYDETAILS C ON A.HREMPLOYMASTID=C.HREMPLOYMASTID JOIN GTFINANCIALYEAR E ON E.GTFINANCIALYEARID=C.FINYEAR   where  b.compcode='" + Class.Users.HCompcode + "' order by 2";
        //        DataSet ds0 = Utility.ExecuteSelectQuery(sel0, "hremploymast");
        //        DataTable dt = ds0.Tables["hremploymast"];

        //        combocompcode.DisplayMember = "COMPCODE";
        //        combocompcode.ValueMember = "GTCOMPMASTID";
        //        combocompcode.DataSource = dt;
        //        combofinyear.DisplayMember = "Finyear";
        //        combofinyear.ValueMember = "GTFINANCIALYEARID";
        //        combofinyear.DataSource = dt;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("companyload: " + ex.Message, " Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }

        //}
        public void News()
        {
           GlobalVariables.HeaderName.Text = Class.Users.ScreenName;

            GridLoad(); companyload(); Finyearload(); userload();
            panel2.BackColor = Class.Users.BackColors;          
            listView1.Font = Class.Users.FontName;
            listView2.Font = Class.Users.FontName;
            this.BackColor = Class.Users.BackColors;
            this.Font = Class.Users.FontName;
            Class.Users.UserTime = 0;
        }
       
        public void GridLoad()
        {
            int j = 0; listView1.Items.Clear(); DataTable dt = new DataTable();
            
                string sel0 = " select  distinct ''asptblpayslipperID, A.PAYPERIOD as PAYMONTH,A.FINYEAR,D.COMPCODE  from LOPPLhpayroll a  JOIN HREMPLOYMAST B ON A.EMPID=B.HREMPLOYMASTID  JOIN HREMPLOYDETAILS C ON C.HREMPLOYMASTID=B.HREMPLOYMASTID AND B.IDCARDNO=C.IDCARD AND C.IDACTIVE='YES' JOIN GTCOMPMAST D ON D.GTCOMPMASTID=B.COMPCODE   join asptblusermas e on E.COMPCODE=D.GTCOMPMASTID  and E.COMPCODE=B.COMPCODE   where  A.FINYEAR='" + combofinyear.Text + "' AND   D.compcode='" + Class.Users.HCompcode + "'  and E.username='" + Class.Users.HUserName + "' order by 3";
                DataSet ds0 = Utility.ExecuteSelectQuery(sel0, "LOPPLhpayroll");
                dt = ds0.Tables["LOPPLhpayroll"];
                        
            if (dt.Rows.Count > 0)
            {
               
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    ListViewItem list = new ListViewItem();
                    list.SubItems.Add(j.ToString());
                    list.SubItems.Add(dt.Rows[i]["PAYMONTH"].ToString());

                    list.SubItems.Add(dt.Rows[i]["asptblpayslipperID"].ToString());
                    list.SubItems.Add(dt.Rows[i]["FINYEAR"].ToString());

                    if (j % 2 == 0)
                    {
                        list.BackColor = Color.WhiteSmoke;

                    }
                    else
                    {
                        list.BackColor = Color.White;

                    }
                    listView1.Items.Add(list); j++;
                }
            }
            else
            {

                // MessageBox.Show("Screen Rights UnDefined", "Error");
            }
        }
        private void combocompcode_SelectedIndexChanged(object sender, EventArgs e)
        {
            //GridLoad();
        }
        int iIndex = 0;
        private void listView1_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            ListViewItem itt = new ListViewItem();
            iIndex = listView1.Items.Count;
            i = listView2.Items.Count;
            if (e.Item.Checked == true)
            {
                i = i + 1;
                itt.Text = "";
                itt.SubItems.Add(e.Item.SubItems[1].Text);
                itt.SubItems.Add(e.Item.SubItems[2].Text);
                itt.SubItems.Add(e.Item.SubItems[3].Text);
                itt.SubItems.Add(e.Item.SubItems[4].Text);
                if (i % 2 == 0)
                {
                    itt.BackColor = Color.White;
                }
                else
                {
                    itt.BackColor = Color.WhiteSmoke;
                }
                listView2.Items.Add(itt);
                iIndex++; i++;
            }
        }

        private void listView2_ItemActivate(object sender, EventArgs e)
        {
            if (listView2.Items.Count > 0)
            {

                int i = 0;

                for (i = 0; i < listView2.Items.Count; i++)
                {

                    if (listView2.Items[i].Selected)
                    {
                      
                        if (listView2.Items[i].SubItems[3].Text.Length > 1)
                        {
                            string del = "delete from  asptblpayslipper a  where  a.ASPTBLPAYSLIPPERID='" + listView2.Items[i].SubItems[3].Text + "'";
                            Utility.ExecuteNonQuery(del);
                        }
                        listView2.Items[i].Remove();
                        i--;
                    }
                    
                }


            }
            else
            {
                MessageBox.Show("No Data found in ListView");
            }

        }
       

        public void Saves()
        {
        
            try
            {
                Cursor = Cursors.WaitCursor; Class.Users.UserTime = 0;
                if (combofinyear.Text != "" && combocompcode.Text != "" &&  combouser.Text != "" && listView2.Items.Count > 0)
                {

                    foreach (ListViewItem item in listView2.Items)
                    {
                        string sel = " select   asptblpayslipperid  from  asptblpayslipper  where finyear='" + item.SubItems[4].Text + "'  and paymonth='" + item.SubItems[2].Text + "' and username='" + combouser.Text + "'";
                        DataSet ds = Utility.ExecuteSelectQuery(sel, "asptblpayslipper");
                        DataTable dt = ds.Tables["asptblpayslipper"];
                        if (dt.Rows.Count != 0) { }

                        else if (dt.Rows.Count != 0 && Convert.ToString(item.SubItems[3].Text) == "" || Convert.ToString(item.SubItems[3].Text) == "")
                        {
                            string ins = "insert into asptblpayslipper(paymonth,finyear,compcode,username)values('" + item.SubItems[2].Text + "','" + item.SubItems[4].Text + "','" + Class.Users.COMPCODE + "','" + combouser.Text + "')";
                            Utility.ExecuteNonQuery(ins);

                        }
                        else
                        {
                            string up = "update  asptblpayslipper set paymonth='" + item.SubItems[2].Text + "', finyear='" + item.SubItems[4].Text + "' ,username='" + combouser.Text + "' where  asptblpayslipperid='" + item.SubItems[3].Text + "'";
                            Utility.ExecuteNonQuery(up);
                        }

                    }

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
            Cursor = Cursors.Default; GridLoad1();
        }

      
        private void combofinyear_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridLoad();

          


        }
       private void GridLoad1()
        {
            listView2.Items.Clear();
            string sel1 = " select  distinct A.asptblpayslipperID, A.PAYMONTH,A.FINYEAR ,D.COMPCODE   from asptblpayslipper a   JOIN GTCOMPMAST D ON D.GTCOMPMASTID=A.COMPCODE       join asptblusermas e on E.COMPCODE=D.GTCOMPMASTID  AND E.USERNAME=A.USERNAME where  a.finyear='" + combofinyear.Text + "' and    D.compcode='" + combocompcode.Text + "' and    E.USERNAME='" + combouser.Text + "'    order by 3";
            DataSet ds1 = Utility.ExecuteSelectQuery(sel1, "asptblpayslipper");
            DataTable dt1 = ds1.Tables["asptblpayslipper"];


            foreach (DataRow myRow in dt1.Rows)
            {
                ListViewItem list = new ListViewItem();

                list.SubItems.Add(i.ToString());
                list.SubItems.Add(myRow["PAYMONTH"].ToString());
                list.SubItems.Add(myRow["asptblpayslipperID"].ToString());
                list.SubItems.Add(myRow["FINYEAR"].ToString());
                if (i % 2 == 0)
                {
                    list.BackColor = Color.White;

                }
                else
                {
                    list.BackColor = Color.WhiteSmoke;

                }
                listView2.Items.Add(list); i++;
            }

        }
       

        private void listView2_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            //try
            //{
            //    i = 0;
            //    if (e.Item.Checked == true)
            //    {
            //        //string del = "delete from  asptblpayslipper  where  asptblpayslipperid='" + e.Item.SubItems[3].Text + "'";
            //        //Utility.ExecuteNonQuery(del);
            //        //GridLoad();
            //        if (listView2.Items.Count > 0)
            //        {

            //            int i = 0;
            //            for (i = 0; i < listView2.Items.Count; i++)
            //            {

            //                if (listView2.Items[i].Selected)
            //                {
            //                    MessageBox.Show("UserID:   " + listView2.Items[i].SubItems[1].Text + "      Name:  " + listView2.Items[i].SubItems[2].Text, "Delete this Record");

            //                    listView2.Items[i].Remove();
            //                    i--;
            //                }
            //            }
            //        }
            //}
            //catch (Exception ex)
            //{
               

            //}
            
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
            Class.Users.UserTime = 0;
            GlobalVariables.HeaderName.Text = "";
            GlobalVariables.TabCtrl.TabPages.RemoveAt(GlobalVariables.TabCtrl.SelectedIndex);

        }

        private void combouser_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (combofinyear.Text != "" && combocompcode.Text != "" && combouser.Text != "")
            {
                Class.Users.UserTime = 0;
                GridLoad1();
            }
        }

        public void Searchs(int id)
        {
            throw new NotImplementedException();
        }

        public void Deletes(int id)
        {
            throw new NotImplementedException();
        }

        public void ReadOnlys()
        {
            throw new NotImplementedException();
        }
    }
}
