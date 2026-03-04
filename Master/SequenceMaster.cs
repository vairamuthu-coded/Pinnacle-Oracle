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
    public partial class SequenceMaster : Form,ToolStripAccess
    {
        public SequenceMaster()
        {
            InitializeComponent();
         
            Class.Users.IPADDRESS = GenFun.GetLocalIPAddress();
            Class.Users.CREATED = Convert.ToDateTime(System.DateTime.Now.ToString("dd/MM/yyyy") + " " + System.DateTime.Now.ToLongTimeString());
            Class.Users.SysDate = Convert.ToString(System.DateTime.Now.ToString("dd/MM/yyyy"));
            Class.Users.SysTime = Convert.ToString(DateTime.Now.ToLongTimeString().ToString());
            GlobalVariables.HeaderName.Text = Class.Users.ScreenName; GlobalVariables.CurrentForm = this;
            butheader.BackColor = Class.Users.BackColors;
            butfooter.BackColor = Class.Users.BackColors;
            panel2.BackColor = Class.Users.BackColors;
            panel3.BackColor = Class.Users.BackColors;
            Class.Users.UserTime = 0; Class.Users.Intimation = "PAYROLL";
        }

       
        private static SequenceMaster _instance;
        Models.Master mas = new Models.Master();
        Models.UserRights sm = new Models.UserRights();
        byte[] bytes;
        public static SequenceMaster Instance
        {
            get { if (_instance == null) _instance = new SequenceMaster(); GlobalVariables.CurrentForm = _instance; return _instance; }

        }



    
        public void News()
        {
            empty(); GridLoad(Class.Users.Finyear);
        }
        public void ReadOnlys()
        {

        }
        public void Saves()
        {
            try
            {
                
                string chkothersIN = "";
                if (checkIN.Checked == true) { chkothersIN = "T"; } else { chkothersIN = "F"; checkIN.Checked = false; }
                string chkothersout = "";
                if (CheckOut.Checked == true) { chkothersout = "T"; } else { chkothersout = "F"; CheckOut.Checked = false; }
                string chk = "";
                if (checkactive.Checked == true) { chk = "T"; } else { chk = "F"; checkactive.Checked = false; }
               

                if (Convert.ToInt64("0" + txtinwardstart.Text) >= 1)
                {
                    txtgatedcno.Text = combofinyear.Text + "/" + combo_compcode.Text + "/" + txtinwardstart.Text;
                    txtintoutsequenceid1.Text = "";
                    txtintoutsequenceid1.Text = txtinwardstart.Text;
                 
                }

                if (Convert.ToInt64("0"+txtoutwardstart.Text) >= 1)
                {
                    txtgatedcno.Text = combofinyear.Text + "/" + combo_compcode.Text + "/" + txtoutwardstart.Text;
                     txtintoutsequenceid1.Text = "";
                    txtintoutsequenceid1.Text = txtoutwardstart.Text;
                 
                }
                if (combofinyear.SelectedValue != null && combo_compcode.SelectedValue != null  || txtinwardstart.Text != "" || txtoutwardstart.Text != "" )
                {
                    string sel0 = "  SELECT A." + combo_compcode.Text + "IN    FROM  ASPTBLSEQMAS A  WHERE   A.COMPCODE='" + combo_compcode.SelectedValue + "'";
                    DataSet ds0 = Utility.ExecuteSelectQuery(sel0, "ASPTBLSEQMAS");
                    DataTable dt0 = ds0.Tables["ASPTBLSEQMAS"];
                    if (dt0 ==null)
                    {
                        string altertable = "alter table ASPTBLSEQMAS add " + combo_compcode.Text + "IN  VARCHAR2(25)";
                        Utility.ExecuteNonQuery(altertable);
                        string altertable1 = "alter table ASPTBLSEQMAS add " + combo_compcode.Text + "OUT  VARCHAR2(25)";
                        Utility.ExecuteNonQuery(altertable1);
                        string altertable2 = "alter table ASPTBLSEQMAS add " + combo_compcode.Text + "SAMPLE  VARCHAR2(25)";
                        Utility.ExecuteNonQuery(altertable2);
                    }

                    string sel = "  SELECT A.ASPTBLSEQMASID    FROM  ASPTBLSEQMAS A  WHERE  A.FINYEAR='" + combofinyear.SelectedValue + "' AND A.COMPCODE='" + combo_compcode.SelectedValue + "' and A.GATEDCNO='" + txtgatedcno.Text + "' AND  A.INWARDNO='" + txtinwardstart.Text + "' AND A.OUTWARDNO='" + txtoutwardstart.Text + "' AND A.FUELTOKEN='" + txtfuelno.Text + "' AND A."+combo_compcode.Text+"IN='" + chkothersIN + "' AND A." + combo_compcode.Text + "OUT='" + chkothersout + "'    AND  A.ACTIVE='" + chk + "'  AND  A." + combo_compcode.Text + "SAMPLE='" + txtagfsample.Text + "'";
                    DataSet ds = Utility.ExecuteSelectQuery(sel, "ASPTBLSEQMAS");
                    DataTable dt = ds.Tables["ASPTBLSEQMAS"];
                    if (dt.Rows.Count != 0)
                    {
                        
                        MessageBox.Show("Child Record Found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        empty();
                        return;
                    }
                    else if (dt.Rows.Count != 0 && Convert.ToInt64("0"+txtintoutsequenceid.Text) == 0 || Convert.ToInt64("0" + txtintoutsequenceid.Text) == 0)
                    {
                        string ins = "INSERT INTO ASPTBLSEQMAS(ASPTBLSEQMASID1 ,  COMPCODE   , FINYEAR,GATEDCNO,INWARDNO,OUTWARDNO,FUELTOKEN," + combo_compcode.Text + "SAMPLE," + combo_compcode.Text + "IN ,  " + combo_compcode.Text + "OUT  , ACTIVE  , USERNAME , COMPCODE1, MODIFIED ,  CREATEDON,  IPADDRESS) " +
                            " VALUES('" + txtintoutsequenceid1.Text + "','" + combo_compcode.SelectedValue + "','" + combofinyear.SelectedValue + "','" + txtgatedcno.Text + "','" + txtinwardstart.Text + "','" + txtoutwardstart.Text + "','" + txtfuelno.Text + "','" + txtagfsample.Text + "','" + chkothersIN +"','" + chkothersout + "','" + chk + "','" + Class.Users.USERID + "', '" + combo_compcode.SelectedValue + "',to_date('" + Convert.ToDateTime(Class.Users.CREATED).ToString() + "', 'dd/MM/yyyy hh24:MI:SS'),to_date('" + Convert.ToDateTime(Class.Users.CREATED) + "', 'dd/MM/yyyy hh24:MI:SS'),'" + Class.Users.IPADDRESS + "')";
                        Utility.ExecuteNonQuery(ins);
                        MessageBox.Show("Record Saved Successfully " + "        " + txtintoutsequenceid.Text, "Success Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        GridLoad(Class.Users.Finyear);
                        empty();

                    }
                    else
                    {
                        string up = "update  ASPTBLSEQMAS  set   ASPTBLSEQMASID1='" + txtintoutsequenceid1.Text + "' , FINYEAR='" + combofinyear.SelectedValue + "' , COMPCODE='" + combo_compcode.SelectedValue + "' , GATEDCNO='" + txtgatedcno.Text + "' ,  INWARDNO='" + txtinwardstart.Text + "' , OUTWARDNO='" + txtoutwardstart.Text + "', FUELTOKEN='" + txtfuelno.Text + "' ," + combo_compcode.Text + "SAMPLE='"+txtagfsample.Text+"'," + combo_compcode.Text + "IN='" + chkothersIN + "' ,  " + combo_compcode.Text + "OUT='" + chkothersout + "', ACTIVE='" + chk + "',USERNAME='" + Class.Users.USERID + "',COMPCODE1='" + combo_compcode.SelectedValue + "',MODIFIED=to_date('" + Convert.ToDateTime(Class.Users.CREATED).ToString() + "', 'dd/MM/yyyy hh24:MI:SS'),CREATEDON=to_date('" + Convert.ToDateTime(Class.Users.CREATED) + "', 'dd/MM/yyyy hh24:MI:SS'),IPADDRESS='" + Class.Users.IPADDRESS + "'   where ASPTBLSEQMASID='" + txtintoutsequenceid.Text + "'";
                        Utility.ExecuteNonQuery(up);
                        MessageBox.Show("Record Updated Successfully " + txtintoutsequenceid.Text, " Update Message ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        GridLoad(Class.Users.Finyear);
                        empty();
                    }
                }
                else
                {
                    MessageBox.Show("'Sequence Field'  Empty not Allowed  ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    combofinyear.Focus();

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Sequence" + "        " + ex.ToString(), "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                combofinyear.Focus();
            }
           
        }
        void empty()
        {
            Class.Users.UserTime = 0; Class.Users.Intimation = "PAYROLL";
            txtgatedcno.Text = ""; txtintoutsequenceid.Text = ""; txtintoutsequenceid1.Text = "";txtfuelno.Text = "";
            combofinyear.SelectedIndex = -1; combo_compcode.SelectedIndex = -1; txtinwardstart.Text = ""; txtoutwardstart.Text = ""; checkactive.Checked = false;
            txtagfsample.Text = "";
            butheader.BackColor = Class.Users.BackColors;
            butfooter.BackColor = Class.Users.BackColors;
            panel2.BackColor = Class.Users.BackColors;
            panel3.BackColor = Class.Users.BackColors;
        }
       public void GridLoad() {
            DataTable dt = new DataTable();
           
                dt = null; Class.Users.Intimation = "PAYROLL";
                string sel = "SELECT A.ASPTBLSEQMASID,C.FINYR AS FINYEAR,B.COMPCODE,A.GATEDCNO,A.INWARDNO, A.OUTWARDNO,A.ACTIVE  FROM  ASPTBLSEQMAS A JOIN GTCOMPMAST B ON A.COMPCODE=B.GTCOMPMASTID   JOIN GTFINANCIALYEAR C ON A.FINYEAR=C.GTFINANCIALYEARID   ORDER BY A.ASPTBLSEQMASID DESC";
                DataSet ds = Utility.ExecuteSelectQuery(sel, "ASPTBLSEQMAS");
                dt = ds.Tables["ASPTBLSEQMAS"];
           
            listView1.Items.Clear();
            if (dt.Rows.Count > 0)
            {
                int i = 1;
                foreach (DataRow myRow in dt.Rows)
                {
                    ListViewItem list = new ListViewItem();
                    list.Text = i.ToString();
                    list.SubItems.Add(myRow["ASPTBLSEQMASID"].ToString());
                    list.SubItems.Add(myRow["FINYEAR"].ToString());
                    list.SubItems.Add(myRow["COMPCODE"].ToString());
                    list.SubItems.Add(myRow["GATEDCNO"].ToString());
                    list.SubItems.Add(myRow["INWARDNO"].ToString());
                    list.SubItems.Add(myRow["OUTWARDNO"].ToString());                  
                    list.SubItems.Add(myRow["ACTIVE"].ToString());
                    listView1.Items.Add(list);
                    i++;
                }
                lbltotal.Text = "Total Count    :" + listView1.Items.Count;
            }
        }
        public void GridLoad(string fin)
        {
            DataTable dt = new DataTable();
            if (checktrue.Checked == false)
            {
                dt = null; Class.Users.Intimation = "PAYROLL";
            string sel = "SELECT A.ASPTBLSEQMASID,C.FINYR AS FINYEAR,B.COMPCODE,A.GATEDCNO,A.INWARDNO, A.OUTWARDNO,A.ACTIVE  FROM  ASPTBLSEQMAS A JOIN GTCOMPMAST B ON A.COMPCODE=B.GTCOMPMASTID   JOIN GTFINANCIALYEAR C ON A.FINYEAR=C.GTFINANCIALYEARID where c.finyr='"+ fin + "'  ORDER BY A.ASPTBLSEQMASID DESC";
            DataSet ds = Utility.ExecuteSelectQuery(sel, "ASPTBLSEQMAS");
            dt = ds.Tables["ASPTBLSEQMAS"];
                listView1.Items.Clear();
                if (dt.Rows.Count > 0)
                {
                    int i = 1;
                    foreach (DataRow myRow in dt.Rows)
                    {
                        ListViewItem list = new ListViewItem();
                        list.Text = i.ToString();
                        list.SubItems.Add(myRow["ASPTBLSEQMASID"].ToString());
                        list.SubItems.Add(myRow["FINYEAR"].ToString());
                        list.SubItems.Add(myRow["COMPCODE"].ToString());
                        list.SubItems.Add(myRow["GATEDCNO"].ToString());
                        list.SubItems.Add(myRow["INWARDNO"].ToString());
                        list.SubItems.Add(myRow["OUTWARDNO"].ToString());
           
                        list.SubItems.Add(myRow["ACTIVE"].ToString());
                        listView1.Items.Add(list);
                        i++;
                    }
                    lbltotal.Text = "Total Count    :" + listView1.Items.Count;
                }
            }
            if (checktrue.Checked == true)
            {
                listView1.Items.Clear();
                GridLoad();
            }
           
        }
        private void listView1_ItemActivate(object sender, EventArgs e)
        {
            if (listView1.Items.Count > 0)
            {
                Class.Users.UserTime = 0;
              
                txtintoutsequenceid.Text = listView1.SelectedItems[0].SubItems[1].Text;
                string sel = " SELECT A.ASPTBLSEQMASID,A.ASPTBLSEQMASID1,A.FINYEAR,A.COMPCODE,A.GATEDCNO,A.INWARDNO, A.OUTWARDNO,A.FUELTOKEN,A.ACTIVE FROM  ASPTBLSEQMAS A JOIN GTCOMPMAST B ON A.COMPCODE=B.GTCOMPMASTID  JOIN GTFINANCIALYEAR C ON A.FINYEAR=C.GTFINANCIALYEARID WHERE A.ASPTBLSEQMASID='" + txtintoutsequenceid.Text + "' ";
                DataSet ds = Utility.ExecuteSelectQuery(sel, "ASPTBLSEQMAS");
                DataTable dt = ds.Tables["ASPTBLSEQMAS"];
                if (dt.Rows.Count > 0)
                {
                    txtintoutsequenceid.Text = Convert.ToString(dt.Rows[0]["ASPTBLSEQMASID"].ToString());
                    txtintoutsequenceid1.Text = Convert.ToString(dt.Rows[0]["ASPTBLSEQMASID1"].ToString());
                    combofinyear.SelectedValue = Convert.ToString(dt.Rows[0]["FINYEAR"].ToString());
                    combo_compcode.SelectedValue = Convert.ToString(dt.Rows[0]["COMPCODE"].ToString());
                    txtgatedcno.Text = Convert.ToString(dt.Rows[0]["GATEDCNO"].ToString());
                    txtinwardstart.Text = Convert.ToString(dt.Rows[0]["INWARDNO"].ToString());
                    txtoutwardstart.Text = Convert.ToString(dt.Rows[0]["OUTWARDNO"].ToString());
                    txtfuelno.Text = Convert.ToString(dt.Rows[0]["FUELTOKEN"].ToString());
                 
                    string sel0 = " SELECT " + combo_compcode.Text.ToString() + "SAMPLE," + combo_compcode.Text.ToString() + "IN," + combo_compcode.Text.ToString() + "OUT  FROM  ASPTBLSEQMAS A JOIN GTCOMPMAST B ON A.COMPCODE=B.GTCOMPMASTID  JOIN GTFINANCIALYEAR C ON A.FINYEAR=C.GTFINANCIALYEARID WHERE A.ASPTBLSEQMASID='" + txtintoutsequenceid.Text + "' ";
                    DataSet ds0 = Utility.ExecuteSelectQuery(sel0, "ASPTBLSEQMAS");
                    DataTable dt0 = ds0.Tables["ASPTBLSEQMAS"];
                    if (dt0.Rows.Count > 0)
                    {
                        txtagfsample.Text = dt0.Rows[0][combo_compcode.Text + "SAMPLE"].ToString();

                        if (dt0.Rows[0][combo_compcode.Text + "IN"].ToString() == "T") { checkIN.Checked = true; checkIN.ForeColor = Color.Red; } else { checkIN.Checked = false; checkIN.ForeColor = Color.Black; }
                        if (dt0.Rows[0][combo_compcode.Text + "OUT"].ToString() == "T") { CheckOut.Checked = true; CheckOut.ForeColor = Color.Red; } else { checkactive.Checked = false; CheckOut.ForeColor = Color.Black; }
                    }
                    if (dt.Rows[0]["ACTIVE"].ToString() == "T") { checkactive.Checked = true; checkactive.ForeColor = Color.Red; } else { checkactive.Checked = false; checkactive.ForeColor = Color.Black; }

         
                 
                }
            }
        }

        private void txtsearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Class.Users.UserTime = 0;
                if (txtsearch.Text.ToUpper() != "")
                {
                    listView1.Items.Clear(); int iGLCount = 1;
                    string sel1 = "SELECT A.ASPTBLSEQMASID,C.FINYR AS FINYEAR,B.COMPCODE,,A.GATEDCNO,A.INWARDNO, A.OUTWARDNO,A.ACTIVE    FROM  ASPTBLSEQMAS A JOIN GTCOMPMAST B ON A.COMPCODE=B.GTCOMPMASTID  JOIN GTFINANCIALYEAR C ON A.FINYEAR=C.GTFINANCIALYEARID where B.COMPCODE LIKE'%" + txtsearch.Text.ToUpper() + "%' || A.GATEDCNO LIKE'%" + txtsearch.Text.ToUpper() + "%' || C.FINYR=LIKE'%" + txtsearch.Text.ToUpper() + "%'  ";
                    DataSet ds = Utility.ExecuteSelectQuery(sel1, "ASPTBLSEQMAS");
                    DataTable dt = ds.Tables["ASPTBLSEQMAS"];
                    if (dt.Rows.Count > 0)
                    {

                        foreach (DataRow myRow in dt.Rows)
                        {
                            ListViewItem list = new ListViewItem();
                            list.Text = iGLCount.ToString();
                            list.SubItems.Add(myRow["ASPTBLSEQMASID"].ToString());
                            list.SubItems.Add(myRow["FINYEAR"].ToString());
                            list.SubItems.Add(myRow["COMPCODE"].ToString());
                            list.SubItems.Add(myRow["GATEDCNO"].ToString());
                            list.SubItems.Add(myRow["INWARDNO"].ToString());
                            list.SubItems.Add(myRow["OUTWARDNO"].ToString());
                            list.SubItems.Add(myRow["ACTIVE"].ToString());
                            listView1.Items.Add(list);
                            iGLCount++;
                        }
                        lbltotal.Text = "Total Count    :" + listView1.Items.Count;
                    }
                    else
                    {
                        listView1.Items.Clear();
                    }
                }
                else
                {

                    listView1.Items.Clear();
                    GridLoad();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void finyear()
        {

            DataTable dt = mas.finyear();
            combofinyear.ValueMember = "gtfinancialyearid";
            combofinyear.DisplayMember = "finyear";
            combofinyear.DataSource = dt;
            DataTable dt1 = mas.Loginfinyear(Class.Users.HCompcode);
            combofinyearsearch.ValueMember = "gtfinancialyearid";
            combofinyearsearch.DisplayMember = "finyear";
            combofinyearsearch.DataSource = dt1;

            Class.Users.Finyear = dt.Rows[0]["finyear"].ToString();
        }
        private void InOutSequenceMaster_Load(object sender, EventArgs e)
        {
            GridLoad(); finyear();
           
            DataTable dt1 = mas.comcode1();           
            if (dt1.Rows.Count >= 0)
            {


                combo_compcode.DisplayMember = "COMPCODE";
                combo_compcode.ValueMember = "gtcompmastid";
                combo_compcode.DataSource = dt1;

              
            }
            else
            {
                combo_compcode.DataSource = null;
            }
            empty();
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridLoad();
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
            GlobalVariables.MdiPanel.Show();
            News();
            GlobalVariables.HeaderName.Text = "";
            GlobalVariables.TabCtrl.TabPages.RemoveAt(GlobalVariables.TabCtrl.SelectedIndex);
            this.Hide();
        }

        private void checktrue_CheckedChanged(object sender, EventArgs e)
        {
            GridLoad(combofinyearsearch.Text);
        }

        public void Searchs(int id)
        {
            throw new NotImplementedException();
        }

        public void Deletes(int id)
        {
            throw new NotImplementedException();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridLoad(combofinyearsearch.Text);
        }

        private void combofinyear_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtoutwardstart_TextChanged(object sender, EventArgs e)
        {
            //if (txtoutwardstart.Text != "")
            //{
            //    txtgatedcno.Text = combofinyear.Text + "/" + combo_compcode.Text + "/" + txtoutwardstart.Text;
            //}
        }

        private void txtinwardstart_TextChanged(object sender, EventArgs e)
        {
            //if (txtinwardstart.Text != "")
            //{
            //    txtgatedcno.Text = combofinyear.Text + "/" + combo_compcode.Text + "/" + txtinwardstart.Text;
            //}
        }
    }
}
