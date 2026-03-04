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
    public partial class MenuNameMaster : Form,ToolStripAccess
    {
        private static MenuNameMaster _instance;
        Models.Master mas = new Models.Master();
        Models.UserRights sm = new Models.UserRights();
        Models.MenuName c = new Models.MenuName();
        public static MenuNameMaster Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new MenuNameMaster();
                GlobalVariables.CurrentForm = _instance; return _instance;
            }
        }
        public MenuNameMaster()
        {
            InitializeComponent();
            GlobalVariables.HeaderName.Text = Class.Users.ScreenName; GlobalVariables.CurrentForm = this;
            mas.DatabaseCheck(checkdatabase);
            butheader.BackColor = Class.Users.BackColors;
            panel3.BackColor = Class.Users.BackColors;
            panel2.BackColor = Class.Users.BackColors;
            butfooter.BackColor = Class.Users.BackColors;
        }
   
        public void GridLoad()
        {

            DataTable dt = c.select();
            if (dt.Rows.Count >= 0)
            {
                dataGridView1.DataSource = dt;

                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    if (dt.Rows[i]["ACTIVE"].ToString() == "T") { dataGridView1.Rows[i].Cells[4].Value = true; } else { dataGridView1.Rows[i].Cells[4].Value = false; }

                }

            }
            else
            {
                dataGridView1.DataSource = dt;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dataGridView1.Rows[i].Cells[i].Value = false;
                }

                MessageBox.Show("Screen Rights UnDefined", "Error");
            }
        }
        public void ReadOnlys()
        {

        }
        private void MenuNameMaster_Load(object sender, EventArgs e)
        {
            
        }

        private void MenuNameMaster_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ToolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }


        public void News()
        {
            empty();

        }
        public void Saves()
        {
            try
            {
                mas.DatabaseCheck(checkdatabase);
                c.MENUNAMEID = Convert.ToInt32("0" + txtmenunameid.Text);
                c.MENUNAME = Convert.ToString(txtmenuname.Text);
                c.PARENTMENUID = Convert.ToInt32("0" + comboparentmenuid.Text);
                c.CREATEBY = Class.Users.HUserName;
                c.IPADDRESS = GenFun.GetLocalIPAddress();
                c.CREATEON = Convert.ToString(System.DateTime.Now.ToString("dd-MMM-yyyy") + " " + System.DateTime.Now.ToLongTimeString());
                c.MODIFYON = Convert.ToString(System.DateTime.Now.ToString("dd-MMM-yyyy") + " " + System.DateTime.Now.ToLongTimeString());

                if (chk.Checked == true) c.ACTIVE = "T"; else c.ACTIVE = "F";
                DataTable dt = c.select(c.MENUNAME, c.ACTIVE, c.PARENTMENUID);
                if (dt.Rows.Count != 0) { MessageBox.Show("Child Record Found.Can't Add Rows"); }
                else if (dt.Rows.Count != 0 && c.MENUNAMEID == 0 || c.MENUNAMEID == 0) { c = new Models.MenuName(c.MENUNAME, c.ACTIVE, c.PARENTMENUID, c.CREATEON, c.CREATEBY, c.MODIFYON, c.IPADDRESS); MessageBox.Show("Record Saved Successfully"); GridLoad(); empty(); }
                else { c = new Models.MenuName(c.MENUNAME, c.ACTIVE, c.PARENTMENUID, c.CREATEBY, c.MODIFYON, c.IPADDRESS, c.MENUNAMEID); MessageBox.Show("Record Updated Sucessfully"); GridLoad(); empty(); }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception" + ex.ToString());
            }

        }
        private void empty()
        {
            txtmenunameid.Text = "";
            txtmenuname.Text = "";
            comboparentmenuid.SelectedIndex = -1;
            chk.Checked = false;
            GlobalVariables.HeaderName.Text = Class.Users.ScreenName; GlobalVariables.CurrentForm = this;
            butheader.Text = Class.Users.ScreenName;
            butheader.BackColor = Class.Users.BackColors;
            panel3.BackColor = Class.Users.BackColors;
            panel2.BackColor = Class.Users.BackColors;
            butfooter.BackColor = Class.Users.BackColors;
            txtmenuname.Select(); mas.DatabaseCheck(checkdatabase);
            GridLoad();
        }
        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {

                    DataGridViewRow dv = dataGridView1.Rows[e.RowIndex];

                    txtmenunameid.Text = dv.Cells["MENUNAMEID"].Value.ToString();
                    txtmenuname.Text = dv.Cells["MENUNAME"].Value.ToString();
                    comboparentmenuid.Text = dv.Cells["parentmenuid"].Value.ToString();
                    if (dv.Cells["ACTIVE"].Value.ToString() == "True")
                        chk.Checked = true;
                    else
                        chk.Checked = false;


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Comboparentmenuid_SelectedIndexChanged(object sender, EventArgs e)
        {
            chk.Checked = true;
        }

        private void Label3_Click(object sender, EventArgs e)
        {

        }

        private void Txtmenusearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
               
                if (txtmenusearch.Text != "")
                {

                    //string sel = " select a.menunameid,a.menuname,a.parentmenuid,a.active from asptblmenuname a   order by a.menunameid desc ";
                    //DataSet ds = Utility.ExecuteSelectQuery(sel, "asptblmenuname");
                    //DataTable dt = ds.Tables["asptblmenuname"];
                    mas.DatabaseCheck(checkdatabase);
                    string sel1 = "SELECT MENUNAMEID,MENUNAME,parentmenuid,ACTIVE FROM ASPTBLMENUNAME   WHERE MENUNAME LIKE'%" + txtmenusearch.Text + "%'   ORDER BY 1 ";
                    DataSet ds = Utility.ExecuteSelectQuery(sel1, "gtdeptdesgmast");
                    DataTable dt = ds.Tables["gtdeptdesgmast"];
                    if (dt.Rows.Count > 0)
                    {
                        dataGridView1.DataSource = dt;
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {

                            if (dt.Rows[i]["ACTIVE"].ToString() == "T") { dataGridView1.Rows[i].Cells[4].Value = true; } else { dataGridView1.Rows[i].Cells[4].Value = false; }

                        }
                        lbltotal.Text = "Total Rows  :  " + dataGridView1.Rows.Count.ToString();
                    }
                    else
                    {
                      //  MessageBox.Show("No Data Found");
                        txtmenusearch.Focus();
                    }

                }
                else
                {
                    dataload();
                }

            }
            catch (Exception ex)
            {
               // MessageBox.Show(ex.ToString());
            }
        }
        private void dataload()
        {
            try
            {
                DataTable dt = c.select();
                if (dt.Rows.Count > 0)
                {
                    dataGridView1.DataSource = dt;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        if (dt.Rows[i]["ACTIVE"].ToString() == "T") { dataGridView1.Rows[i].Cells[4].Value = true; } else { dataGridView1.Rows[i].Cells[4].Value = false; }

                    }
                    lbltotal.Text = "Total Rows  :  " + dataGridView1.Rows.Count.ToString();
                }
                else
                {
                    dataGridView1.DataSource = dt;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dataGridView1.Rows[i].Cells[i].Value = false;
                    }

                    MessageBox.Show("Screen Rights UnDefined", "Error");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

      

     

       
        public void Prints()
        {
          // 
        }

        public void Searchs()
        {
           //
        }

        public void Deletes()
        {
            if (txtmenunameid.Text != "")
            {
                string del = "delete from asptblmenuname where menunameid=" + txtmenunameid.Text;
                Utility.ExecuteNonQuery(del);
                MessageBox.Show("Record Deleted Successfully.");
                GridLoad();
            }
        }

        public void Imports()
        {
            //throw new NotImplementedException();
        }

        public void Pdfs()
        {
           //
        }

        public void ChangePasswords()
        {
           
        }

        public void DownLoads()
        {
           //throw new NotImplementedException();
        }

        public void ChangeSkins()
        {
            //throw new NotImplementedException();
        }

        public void Logins()
        {
            //throw new NotImplementedException();
        }

        public void GlobalSearchs()
        {
           //
        }

        public void TreeButtons()
        {
           //
        }

        public void Exit()
        {
            GlobalVariables.MdiPanel.Show();
            this.Hide();
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

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
    }
}
