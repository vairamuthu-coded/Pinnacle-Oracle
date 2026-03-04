using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pinnacle.TreeView
{
    public partial class NavigationMaster : Form,ToolStripAccess
    {
        private static NavigationMaster _instance;
        Models.Master mas = new Models.Master();
        Models.UserRights sm = new Models.UserRights();
        Models.Navigation c = new Models.Navigation();
        Models.MenuName m = new Models.MenuName();
        public static NavigationMaster Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new NavigationMaster();
                GlobalVariables.CurrentForm = _instance; return _instance;
            }
        }
        public void ReadOnlys()
        {

        }
        public NavigationMaster()
        {
            InitializeComponent(); 
            GlobalVariables.HeaderName.Text = Class.Users.ScreenName; GlobalVariables.CurrentForm = this;
          
            butheader.BackColor = Class.Users.BackColors;
            panel3.BackColor = Class.Users.BackColors;
            panel2.BackColor = Class.Users.BackColors;
            butfooter.BackColor = Class.Users.BackColors;
            mas.DatabaseCheck(checkdatabase);
        }
     
        public void GridLoad()
        {
            DataTable dt = c.select(combocompcode1.Text,combousername1.Text);
            if (dt.Rows.Count > 0)
            {
                dataGridView1.DataSource = dt;

                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    if (dt.Rows[i]["ACTIVE"].ToString() == "T") { dataGridView1.Rows[i].Cells[5].Value = true; } else { dataGridView1.Rows[i].Cells[5].Value = false; }
                   
                }
                lbltotal.Text = "Total Rows :  " + dataGridView1.Rows.Count.ToString();
            }
            else
            {
                dataGridView1.DataSource = dt;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dataGridView1.Rows[i].Cells[i].Value = false;
                }

               // MessageBox.Show("Screen Rights UnDefined", "Error");
            }
        }
        private void combocode()
        {
            DataTable dt1 = mas.findcomcode(Class.Users.HCompcode);
            if (dt1.Rows.Count > 0)
            {
                combocompcode.DisplayMember = "COMPCODE";
                combocompcode.ValueMember = "gtcompmastid";
                combocompcode.DataSource = dt1;
                
            }

            DataTable dt2 = mas.findcomcode(Class.Users.HCompcode);
            if (dt2.Rows.Count > 0)
            {
              
                combocompcode1.DisplayMember = "COMPCODE";
                combocompcode1.ValueMember = "gtcompmastid";
                combocompcode1.DataSource = dt2;
            }
            //combocompcode.SelectedIndex = -1;
            //combocompcode1.SelectedIndex = -1;
        }
        //private void combocode2()
        //{
        //    DataTable dt1 = mas.comcode1();
        //    if (dt1.Rows.Count > 0)
        //    {


        //        combocompcode1.DisplayMember = "COMPCODE";
        //        combocompcode1.ValueMember = "gtcompmastid";
        //        combocompcode1.DataSource = dt1;


        //    }
        //    combocompcode1.SelectedIndex = -1;

        //}
        private void menuname()
        {

            DataTable dt1 = m.menuname();
            if (dt1.Rows.Count > 0)
            {
                combomenunameid.DisplayMember = "MENUNAME";
                combomenunameid.ValueMember = "MENUNAMEID";
                combomenunameid.DataSource = dt1;

            }
            combomenunameid.SelectedIndex = -1;
        }

        private void NavigationMaster_Load(object sender, EventArgs e)
        {
            combocode(); 
            menuname(); empty();
            GridLoad();
            GlobalVariables.HeaderName.Text = Class.Users.ScreenName;
        }

        private void NavigationMaster_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }



        public void Saves()
        {


            try
            {
                if (combomenunameid.Text != null && txtmenuname.Text != null && txtnavurl.Text != null && txtparentmenuid.Text != null)
                {
                    c.MenuID = Convert.ToInt64("0" + txtmenuid.Text);
                    c.MenuNameID = Convert.ToInt64("0" + combomenunameid.SelectedValue);
                    c.MenuName = Convert.ToString(txtmenuname.Text);
                    c.NavURL = Convert.ToString(txtnavurl.Text);
                    c.ParentMenuID = Convert.ToInt64("0" + txtparentmenuid.Text);
                    c.CompCode = Convert.ToInt64("0" + combocompcode.SelectedValue);
                    c.UserName = Convert.ToInt64("0" + combousername.SelectedValue);

                    if (chk.Checked == true) c.Active = "T"; else c.Active = "F";
                    if (c.MenuNameID >= 0 && c.MenuName != null && c.NavURL != null && c.CompCode > 0 && c.UserName > 0)
                    {

                        DataTable dt = c.select(c.MenuName, c.NavURL, c.ParentMenuID, c.Active, c.MenuNameID, c.CompCode, c.UserName);

                        if (dt.Rows.Count != 0) { MessageBox.Show("Child Record Found.Can't Add Rows"); }
                        else if (dt.Rows.Count != 0 && c.MenuID == 0 || c.MenuID == 0)
                        {

                            c = new Models.Navigation(c.MenuName, c.NavURL, c.ParentMenuID, c.Active, c.MenuNameID, c.CompCode, c.UserName);
                            MessageBox.Show("Record Saved Successfully"); empty(); GridLoad();
                        }
                        else { c = new Models.Navigation(c.MenuName, c.NavURL, c.ParentMenuID, c.Active, c.MenuNameID, c.CompCode, c.UserName, c.MenuID); MessageBox.Show("Record Updated Sucessfully"); empty(); GridLoad(); }

                    }
                    else
                    {
                        MessageBox.Show("pls Enter the Mandatry Fields");
                    }
                }
                else
                {
                    MessageBox.Show("pls Enter the Mandatry Fields");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception" + ex.ToString());
            }
        }

       
        private void empty()
        {
            txtmenuid.Text = "";
            combomenunameid.SelectedIndex = -1;
            txtmenuname.Text = "";
            txtnavurl.Text = "";
            txtparentmenuid.Text = "";
            combocompcode.SelectedIndex = -1;
            combousername.SelectedIndex = -1;lbltotal.Text = "";
            butheader.BackColor = Class.Users.BackColors;
            panel3.BackColor = Class.Users.BackColors;
            panel2.BackColor = Class.Users.BackColors;
            butfooter.BackColor = Class.Users.BackColors;
            chk.Checked = false;
            txtmenuname.Select();
        }

        private void Combomenunameid_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (combomenunameid.SelectedIndex >= 0)
                {
                    Int64 s = Convert.ToInt64(combomenunameid.SelectedValue);
                    DataTable dt1 = m.menuname(s);
                    if (dt1.Rows.Count > 0)
                    {
                        txtparentmenuid.Text = dt1.Rows[0]["parentmenuid"].ToString();
                        txtmenuname.Text = dt1.Rows[0]["MENUNAME"].ToString();
                        txtnavurl.Text = dt1.Rows[0]["MENUNAME"].ToString();
                        chk.Checked = true;
                    }
                }
            }
            catch (Exception EX)
            {
                MessageBox.Show(EX.Message.ToString());
            }
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {

                    DataGridViewRow dv = dataGridView1.Rows[e.RowIndex];
                    txtmenuid.Text = dv.Cells["MENUID1"].Value.ToString();
                    combomenunameid.Text= dv.Cells["MENUNAME1"].Value.ToString();
                    txtmenuname.Text = dv.Cells["MENUNAME1"].Value.ToString();
                    txtnavurl.Text = dv.Cells["NAVURL"].Value.ToString();
                    txtparentmenuid.Text = dv.Cells["parentmenuid"].Value.ToString();                  
                    if (dv.Cells["ACTIVE"].Value.ToString() == "True")
                        chk.Checked = true;
                    else
                        chk.Checked = false;
                    DataTable dt = mas.comcode2(Convert.ToInt64(dv.Cells["gtcompmastid"].Value));

                    if (dt.Rows.Count > 0)
                    {
                      
                       // combocompcode.DisplayMember = "COMPCODE";
                      // combocompcode.ValueMember = "gtcompmastid";

                        combousername.DisplayMember = "USERNAME";
                        combousername.ValueMember = "USERID";


                        //combomenunameid.DisplayMember = "MENUNAME";
                        //combomenunameid.ValueMember = "MENUNAMEID";

                    }
                   // combocompcode.DataSource = dt;
                    combousername.DataSource = dt;
                   // combomenunameid.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Combocompcode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (combocompcode.SelectedIndex >= 0)
                {
                    Int64 s = Convert.ToInt64(combocompcode.SelectedValue);
                    DataTable dt1 = mas.comcode1(s);
                    if (dt1.Rows.Count > 0)
                    {
                        combousername.DisplayMember = "USERNAME";
                        combousername.ValueMember = "USERID";
                        combousername.DataSource = dt1;
                      

                    }
                   // combousername.SelectedIndex = -1;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        public void News()
        {
            empty();
        }
      

        private void Combocompcode1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt64(combocompcode1.SelectedValue) > 0)
                {
                    Int64 s = Convert.ToInt64(combocompcode1.SelectedValue);
                    DataTable dt1 = mas.comcode2(s);
                    if (dt1.Rows.Count > 0)
                    {
                        combousername1.DisplayMember = "USERNAME";
                        combousername1.ValueMember = "USERID";
                        combousername1.DataSource = dt1;


                    }
                //    combousername1.SelectedIndex = -1;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            //try
            //{
            //    if (combocompcode1.SelectedIndex >= 0)
            //    {

            //        Int64 s = Convert.ToInt64(combocompcode1.SelectedValue);
            //        DataTable dt2 = mas.comcode2(s);
            //        if (dt2.Rows.Count > 0)
            //        {
            //            combousername1.DisplayMember = "USERNAME";
            //            combousername1.ValueMember = "USERID";
            //            combousername1.DataSource = dt2;

            //        }
            //        combousername1.SelectedIndex = -1;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message.ToString());
            //}
        }

        private void Combousername1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt64(combousername1.SelectedValue) > 0)
                {


                    DataTable dt = c.select(combocompcode1.Text, combousername1.Text);
                    if (dt.Rows.Count >= 0)
                    {
                        dataGridView1.DataSource = dt;
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {

                            if (dt.Rows[i]["ACTIVE"].ToString() == "T") { dataGridView1.Rows[i].Cells[5].Value = true; } else { dataGridView1.Rows[i].Cells[5].Value = false; }

                        }
                        lbltotal.Text = "Total Rows :  " + dataGridView1.Rows.Count.ToString();
                    }
                    else
                    {

                        dataGridView1.Columns.Clear();
                        //  MessageBox.Show("Screen Rights UnDefined","Error");

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void MenuMasterRefreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            menuname();
            combocode(); GridLoad();
        }

        private void TabPage2_Click(object sender, EventArgs e)
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
           if(txtmenuid.Text != "")
            {
                string del = "delete from asptblnavigation where menuid=" + txtmenuid.Text;
                Utility.ExecuteNonQuery(del);
                MessageBox.Show("Record Deleted Successfully.");
                GridLoad();
            }
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
            
            empty();
            GlobalVariables.HeaderName.Text = "";
            GlobalVariables.TabCtrl.TabPages.RemoveAt(GlobalVariables.TabCtrl.SelectedIndex);

        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            this.dataGridView1.Rows[e.RowIndex].Cells["SNo"].Value = (e.RowIndex + 1).ToString();
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

        private void txtsearch_TextChanged(object sender, EventArgs e)
        {
            Utility.Connect();
            string sel = "select a.menuid as menuid1 ,d.menuname as menuname1,a.navurl,a.parentmenuid,a.active,a.menunameid as menunameid1,b.gtcompmastid ,c.userid from asptblnavigation a  join gtcompmast b on a.compcode = b.gtcompmastid  join asptblusermas c on c.userid = a.username   join asptblmenuname d on d.menunameid=a.menunameid  where b.compcode = '" + combocompcode1.Text + "' and c.username  = '" + combousername1.Text + "' and d.menuname like'%" + txtsearch.Text + "%'  order by 1";
            DataSet ds = Utility.ExecuteSelectQuery(sel, "asptblnavigation");
            DataTable dt1 = ds.Tables["asptblnavigation"];           
            dataGridView1.DataSource = dt1;
            Utility.DisConnect();
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = false;
        }
    }
}

