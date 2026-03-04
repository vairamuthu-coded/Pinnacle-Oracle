using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Pinnacle.Master
{
    public partial class FinYearMaster : Form,ToolStripAccess
    {
        private static FinYearMaster _instance;
        Models.Master mas = new Models.Master();
        Models.Employee em = new Models.Employee();
        Models.UserRights sm = new Models.UserRights();
        PinnacleMdi mdi = new PinnacleMdi();
        public FinYearMaster()
        {
            InitializeComponent();
            //  usercheck(Class.Users.HCompcode, Class.Users.HUserName, Class.Users.ScreenName);
            GlobalVariables.HeaderName.Text = Class.Users.ScreenName; GlobalVariables.CurrentForm = this;
            Class.Users.IPADDRESS = GenFun.GetLocalIPAddress();
            Class.Users.CREATED = Convert.ToDateTime(System.DateTime.Now.ToString("dd/MM/yyyy") + " " + System.DateTime.Now.ToLongTimeString());
        
        }

     
        public static FinYearMaster Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new FinYearMaster();
                GlobalVariables.CurrentForm = _instance; return _instance;
            }
        }

       
        //public void usercheck(string s, string ss, string sss)
        //{

        //    DataTable dt1 = sm.headerdropdowns(s, ss, sss);
        //    if (dt1.Rows.Count > 0)
        //    {
        //        if (dt1.Rows[0]["Menuname"].ToString() == Class.Users.ScreenName)
        //        {
        //            for (int r = 0; r < dt1.Rows.Count; r++)
        //            {

        //                if (dt1.Rows[r]["NEWS"].ToString() == "T") { this.News.Visible = true; } else { this.News.Visible = false; }
        //                if (dt1.Rows[r]["SAVES"].ToString() == "T") { this.Saves.Visible = true; } else { this.Saves.Visible = false; }
        //                if (dt1.Rows[r]["PRINTS"].ToString() == "T") { this.Prints.Visible = true; } else { this.Prints.Visible = false; }
        //                if (dt1.Rows[r]["READONLY"].ToString() == "T") { this.Enabled = true; } else { this.Enabled = false; }
        //                if (dt1.Rows[r]["SEARCH"].ToString() == "T") { this.Searchs.Visible = true; } else { this.Searchs.Visible = false; }
        //                if (dt1.Rows[r]["DELETES"].ToString() == "T") { this.Deletes.Visible = true; } else { this.Deletes.Visible = false; }
        //                if (dt1.Rows[r]["TREEBUTTON"].ToString() == "T") { this.TreeButtons.Visible = true; } else { this.TreeButtons.Visible = false; }
        //                if (dt1.Rows[r]["GLOBALSEARCH"].ToString() == "T") { this.GlobalSearchs.Visible = true; } else { this.GlobalSearchs.Visible = false; }
        //                if (dt1.Rows[r]["LOGIN"].ToString() == "T") { this.Logins.Visible = true; } else { this.Logins.Visible = false; }
        //                if (dt1.Rows[r]["CHANGEPASSWORD"].ToString() == "T") { this.ChangePasswords.Visible = true; } else { this.ChangePasswords.Visible = false; }
        //                if (dt1.Rows[r]["CHANGESKIN"].ToString() == "T") { ChangeSkins.Visible = true; } else { ChangeSkins.Visible = false; }
        //                if (dt1.Rows[r]["DOWNLOAD"].ToString() == "T") { this.DownLoads.Visible = true; } else { this.DownLoads.Visible = false; }
        //                if (dt1.Rows[r]["Pdf"].ToString() == "T") { this.Pdfs.Visible = true; } else { this.Pdfs.Visible = false; }
        //                if (dt1.Rows[r]["Imports"].ToString() == "T") { this.Imports.Visible = true; } else { this.Imports.Visible = false; }

        //            }
        //        }


        //    }
        //    else
        //    {
        //        MessageBox.Show("Invalid");
        //    }

        //}
        private void Txttodate_ValueChanged(object sender, EventArgs e)
        {

            DateTime StartingDate = Convert.ToDateTime(txtfromdate.Value.Date.ToString());
            DateTime EndingDate = Convert.ToDateTime(txttodate.Value.Date.ToString());
             TimeSpan countdays  = EndingDate.Subtract(StartingDate);
            int totaldays = Convert.ToInt32(countdays.Days);
            if (totaldays == 364 || totaldays == 365)
            {
                txttotaldays.Text = totaldays.ToString();
                txtfinyear.Text = StartingDate.ToString("yy") + "-" + EndingDate.ToString("yy");
            }
            else
            {
                txttotaldays.Text = "";
            }
           
        }
        private List<DateTime> GetDateRange(DateTime StartingDate, DateTime EndingDate)
        {
            if (StartingDate > EndingDate)
            {
                return null;
            }
            List<DateTime> rv = new List<DateTime>();
            DateTime tmpDate = StartingDate;
            do
            {
                rv.Add(tmpDate);
                tmpDate = tmpDate.AddDays(1);
            } while (tmpDate <= EndingDate);
            return rv;
        }

        private void FinYearMaster_Load(object sender, EventArgs e)
        {
            News();
          
        }
        
      
       public void GridLoad()
        {
            try
            {
                listView1.Items.Clear();
                string sel1 = "select a.gtfinancialyearid,a.finyr AS FINYEAR, a.startdate as fromdate,A.enddate TODATE ,A.CURRENTFINYR  as CURRENTFINYR from asptblfinmas a  order by 1";
                DataSet ds = Utility.ExecuteSelectQuery(sel1, "asptblfinmas");
                DataTable dt = ds.Tables["asptblfinmas"];
                if (dt.Rows.Count > 0)
                {
                    int i = 1;
                    foreach (DataRow myRow in dt.Rows)
                    {
                        ListViewItem list = new ListViewItem();
                        list.SubItems.Add(i.ToString());
                        list.SubItems.Add(myRow["gtfinancialyearid"].ToString());
                        list.SubItems.Add(myRow["FINYEAR"].ToString());
                        list.SubItems.Add(myRow["FROMDATE"].ToString());
                        list.SubItems.Add(myRow["TODATE"].ToString());
                        list.SubItems.Add(myRow["CURRENTFINYR"].ToString());                           
                       // list.SubItems.Add(myRow["COMPCODE"].ToString());
                       // list.SubItems.Add(myRow["TOTALDAYS"].ToString());
                        listView1.Items.Add(list);
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

        private void TextBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void Txtfromdate_ValueChanged(object sender, EventArgs e)
        {
            DateTime StartingDate = Convert.ToDateTime(txtfromdate.Value.Date.ToString());
            DateTime EndingDate = Convert.ToDateTime(txttodate.Value.Date.ToString());
            TimeSpan countdays = EndingDate.Subtract(StartingDate);
            int totaldays = Convert.ToInt32(countdays.Days);
            if (totaldays == 364 || totaldays == 365)
            {
                txttotaldays.Text = totaldays.ToString();
                txtfinyear.Text = StartingDate.ToString("yy") + "-" + EndingDate.ToString("yy");
            }
            else
            {
                txttotaldays.Text = "";
            }
        }

        public void Saves()
        {
           
            try
            {


                if (txttotaldays.Text == "")
                {
                    MessageBox.Show("'Total Days Empty'  Empty not Allowed  ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txttotaldays.Focus(); return;
                }
                if (txtfinyear.Text == "")
                {
                    MessageBox.Show("'FinYear is empty'  Empty not Allowed  ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtfinyear.Focus(); return;
                }
                if (txttotaldays.Text != "" && txtfinyear.Text != "")
                {
                    string chk = "";
                    if (radiocurrent.Checked == true) { chk = "T"; } else { chk = "F"; radioclosed.Checked = false; }
                    string sel = "select a.gtfinancialyearid    from  asptblfinmas a    WHERE A.FINYR='" + txtfinyear.Text + "' AND  A.CURRENTFINYR='" + chk + "' ";
                    DataSet ds = Utility.ExecuteSelectQuery(sel, "asptblfinmas");
                    DataTable dt = ds.Tables["asptblfinmas"];
                    if (dt.Rows.Count != 0)
                    {
                        MessageBox.Show("Child Record Found " + "        " + txtfinyear.Text, "Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else if (dt.Rows.Count != 0 && Convert.ToInt64("0" + txtfinyearid.Text) == 0 || Convert.ToInt64("0" + txtfinyearid.Text) == 0)
                    {

                        string ins = "INSERT INTO asptblfinmas(FINYR,FINYRIDENTIFIER,startdate,  enddate,  CURRENTFINYR,  TOTALDAYS,  USERID,  MODIFIED_ON,  CREATED_ON,IPADDRESS)  VALUES('" + txtfinyear.Text + "','" + txtfinyear.Text + "',to_date('" + Convert.ToDateTime(txtfromdate.Value).ToString("dd-MM-yyyy") + "', 'dd-MM-yyyy'),to_date('" + Convert.ToDateTime(txttodate.Value).ToString("dd-MM-yyyy") + "', 'dd-MM-yyyy'),'" + chk + "','" + txttotaldays.Text + "','" + Class.Users.USERID + "',to_date('" + Convert.ToDateTime(System.DateTime.Now.ToString()).ToString("dd-MM-yyyy") + "', 'dd-MM-yyyy'),to_date('" + Convert.ToDateTime(System.DateTime.Now.ToString()).ToString("dd-MM-yyyy") + "', 'dd-MM-yyyy'),'" + Class.Users.IPADDRESS + "' )";
                        Utility.ExecuteNonQuery(ins);
                        MessageBox.Show("Record Saved Successfully " + "        " + txtfinyear.Text, "Success Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        GridLoad(); empty();
                    }
                    else
                    {
                        string up = "UPDATE  asptblfinmas A SET   A.FINYR='" + txtfinyear.Text + "' ,A.FINYRIDENTIFIER='" + txtfinyear.Text + "' , A.startdate=to_date('" + Convert.ToDateTime(txtfromdate.Value).ToString("dd-MM-yyyy") + "', 'dd-MM-yyyy'),  A.enddate=to_date('" + Convert.ToDateTime(txttodate.Value).ToString("dd-MM-yyyy") + "', 'dd-MM-yyyy') , A.CURRENTFINYR='" + chk + "' ,A.TOTALDAYS=" + txttotaldays.Text + " ,A.userid=" + Class.Users.USERID + ",A.MODIFIED_ON=to_date('" + Convert.ToDateTime(System.DateTime.Now.ToString()).ToString("dd-MM-yyyy") + "', 'dd-MM-yyyy'),A.IpAddress='" + Class.Users.IPADDRESS + "' WHERE A.gtfinancialyearid=" + txtfinyearid.Text;
                        Utility.ExecuteNonQuery(up);
                        MessageBox.Show("Record Updated Successfully " + "        " + txtfinyear.Text, "Update Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        GridLoad();
                        empty();
                    }

                }

            }
            catch (Exception ex)
            {

                MessageBox.Show("FinYear " + "        " + ex.ToString(), "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void empty()
        {
            txtfinyearid.Text = "";
            txtfinyear.Text = "";
            txtfromdate.Text = null;
            txttodate.Text = "";
            txttotaldays.Text = "";

          
        }

      

        private void ListView1_ItemActivate(object sender, EventArgs e)
        {
            try
            {
                if (listView1.Items.Count > 0)
                {

                    txtfinyearid.Text = listView1.SelectedItems[0].SubItems[2].Text;
                    string sel1 = " select a.gtfinancialyearid, a.finyr as finyear, a.startdate as fromdate,a.enddate as todate,a.CURRENTFINYR as CURRENTFINYR from asptblfinmas a  where a.gtfinancialyearid=" + txtfinyearid.Text;
                    DataSet ds = Utility.ExecuteSelectQuery(sel1, "asptblfinmas");
                    DataTable dt = ds.Tables["asptblfinmas"];

                    if (dt.Rows.Count > 0)
                    {
                        txtfinyearid.Text = Convert.ToString(dt.Rows[0]["gtfinancialyearid"].ToString());
                        txtfinyear.Text = Convert.ToString(dt.Rows[0]["FINYEAR"].ToString());
                        txtfromdate.Text = Convert.ToString(dt.Rows[0]["FROMDATE"].ToString());
                        txttodate.Text = Convert.ToString(dt.Rows[0]["TODATE"].ToString());
                       // txttotaldays.Text = Convert.ToString(dt.Rows[0]["TOTALDAYS"].ToString());
                        if (dt.Rows[0]["CURRENTFINYR"].ToString() == "T") { radiocurrent.Checked = true; } else { radioclosed.Checked = true; radiocurrent.Checked = false; }

                      // combo_compcode.Text = dt.Rows[0]["compcode"].ToString();
                      // combo_compcode.ValueMember = dt.Rows[0]["gtcompmastid"].ToString();
                        //combo_compcode.DataSource = dt;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       

        private void ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
           
        }

        public void News()
        {
            GridLoad(); empty();
        }

       

        public void Prints()
        {
            throw new NotImplementedException();
        }

        public void Searchs()
        {
            throw new NotImplementedException();
        }

        public void Deletes()
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

            this.Hide(); GlobalVariables.MdiPanel.Show(); GlobalVariables.HeaderName.Text = "";
            // GlobalVariables.CurrentForm = null;
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
