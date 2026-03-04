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
    public partial class InOutSequenceMaster : Form,ToolStripAccess
    {
        public InOutSequenceMaster()
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

       
        private static InOutSequenceMaster _instance;
        Models.Master mas = new Models.Master();
        Models.UserRights sm = new Models.UserRights();
        byte[] bytes;
        public static InOutSequenceMaster Instance
        {
            get { if (_instance == null) _instance = new InOutSequenceMaster(); GlobalVariables.CurrentForm = _instance; return _instance; }

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
                if (checkOthersIN.Checked == true) { chkothersIN = "T"; } else { chkothersIN = "F"; checkOthersIN.Checked = false; }
                string chkothersout = "";
                if (checkOthersOut.Checked == true) { chkothersout = "T"; } else { chkothersout = "F"; checkOthersOut.Checked = false; }
                string chk = "";
                if (checkactive.Checked == true) { chk = "T"; } else { chk = "F"; checkactive.Checked = false; }
                string AGFIN = "";
                if (checkagfin.Checked == true) { AGFIN = "T"; } else { AGFIN = "F"; checkagfin.Checked = false; }
                string AGFOUT = "";
                if (checkagfout.Checked == true) { AGFOUT = "T"; } else { AGFOUT = "F"; checkagfout.Checked = false; }
                string FLFIN = "";
                if (checkflfin.Checked == true) { FLFIN = "T"; } else { FLFIN = "F"; checkflfin.Checked = false; }
                string FLFOUT = "";
                if (checkflfout.Checked == true) { FLFOUT = "T"; } else { FLFOUT = "F"; checkflfout.Checked = false; }
                string AGFMIN = "";
                if (checkagfmin.Checked == true) { AGFMIN = "T"; } else { AGFMIN = "F"; checkagfmin.Checked = false; }
                string AGFMOUT = "";
                if (checkagfmout.Checked == true) { AGFMOUT = "T"; } else { AGFMOUT = "F"; checkagfmout.Checked = false; }
                string FLFDIN = "";
                if (checkflfdin.Checked == true) { FLFDIN = "T"; } else { FLFDIN = "F"; checkflfdin.Checked = false; }
                string FLFDOUT = "";
                if (checkflfdout.Checked == true) { FLFDOUT = "T"; } else { FLFDOUT = "F"; checkflfdout.Checked = false; }

                string AGFCIN = "";
                if (checkagfcin.Checked == true) { AGFCIN = "T"; } else { AGFCIN = "F"; checkagfcin.Checked = false; }
                string AGFCOUT = "";
                if (checkagfcout.Checked == true) { AGFCOUT = "T"; } else { AGFCOUT = "F"; checkagfcout.Checked = false; }
                string AGFKIN = "";
                if (checkagfkin.Checked == true) { AGFKIN = "T"; } else { AGFKIN = "F"; checkagfkin.Checked = false; }
                string AGFKOUT = "";
                if (checkagfkout.Checked == true) { AGFKOUT = "T"; } else { AGFKOUT = "F"; checkagfkout.Checked = false; }

                string AGFPIN = "";
                if (checkagfpin.Checked == true) { AGFPIN = "T"; } else { AGFPIN = "F"; checkagfpin.Checked = false; }
                string AGFPOUT = "";
                if (checkagfpout.Checked == true) { AGFPOUT = "T"; } else { AGFPOUT = "F"; checkagfpout.Checked = false; }
                string AGFMGIIIN = "";
                if (checkagfmgiiin.Checked == true) { AGFMGIIIN = "T"; } else { AGFMGIIIN = "F"; checkagfmgiiin.Checked = false; }
                string AGFMGIIOUT = "";
                if (checkagfmgiiout.Checked == true) { AGFMGIIOUT = "T"; } else { AGFMGIIOUT = "F"; checkagfmgiiout.Checked = false; }
                string AGFSAMPLEACTIVE = "";
                if (checkagfsample.Checked == true) { AGFSAMPLEACTIVE = "T"; } else { AGFSAMPLEACTIVE = "F"; checkagfsample.Checked = false; }
                string AGFMGIISAMPLEACTIVE = "";
                if (checkagfmgiisample.Checked == true) { AGFMGIISAMPLEACTIVE = "T"; } else { AGFMGIISAMPLEACTIVE = "F"; checkagfmgiisample.Checked = false; }

                string VELIN = "";
                if (checkvelin.Checked == true) { VELIN = "T"; } else { VELIN = "F"; checkvelin.Checked = false; }
                string VELOUT = "";
                if (checkvelout.Checked == true) { VELOUT = "T"; } else { VELOUT = "F"; checkvelout.Checked = false; }

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
                    string sel = "  SELECT A.ASPTBLINOUTMASID    FROM  ASPTBLINOUTMAS A  WHERE A.ASPTBLINOUTMASID='" + txtintoutsequenceid.Text + "' AND A.ASPTBLINOUTMASID1='" + txtintoutsequenceid1.Text + "' and A.FINYEAR='" + combofinyear.SelectedValue + "' AND A.COMPCODE='" + combo_compcode.SelectedValue + "' and A.GATEDCNO='" + txtgatedcno.Text + "' AND  A.INWARDNO='" + txtinwardstart.Text + "' AND A.OUTWARDNO='" + txtoutwardstart.Text + "' AND A.FUELTOKEN='" + txtfuelno.Text + "' AND A.AGFIN='" + AGFIN + "' AND  A.AGFOUT='" + AGFOUT + "' AND  A.FLFIN='" + FLFIN + "' AND  A.FLFOUT='" + FLFOUT + "' AND  A.AGFMIN='" + AGFMIN + "' AND  A.AGFMOUT='" + AGFMOUT + "' AND  A.FLFDIN='" + FLFDIN + "' AND  A.FLFDOUT='" + FLFDOUT + "' AND AGFCIN='" + AGFCIN + "' AND AGFCOUT='" + AGFCOUT + "'AND AGFKIN='" + AGFKIN + "' AND AGFKOUT='" + AGFKOUT + "' AND AGFPIN='" + AGFPIN + "' AND AGFPOUT='" + AGFPOUT + "' AND AGFMGIIIN='" + AGFMGIIIN + "' AND AGFMGIIOUT='" + AGFMGIIOUT + "' AND AGFSAMPLE='" + txtagfsample.Text + "' AND AGFMGIISAMPLE='" + txtagfmgiisample.Text + "' AND AGFSAMPLEACTIVE='" + AGFSAMPLEACTIVE + "' AND AGFMGIISAMPLEACTIVE='" + AGFMGIISAMPLEACTIVE + "' AND  A.ACTIVE='" + chk + "' AND  A.VELIN='" + VELIN + "' AND  A.VELOUT='" + VELOUT + "' AND OTHERSIN='" + chkothersIN + "' AND OTHERSOUT='" + chkothersout + "'";
                    DataSet ds = Utility.ExecuteSelectQuery(sel, "ASPTBLINOUTMAS");
                    DataTable dt = ds.Tables["ASPTBLINOUTMAS"];
                    if (dt.Rows.Count != 0)
                    {
                        //string ins = "INSERT INTO ASPTBLINOUTMAS(ASPTBLINOUTMASID1 ,  COMPCODE   , FINYEAR,GATEDCNO,INWARDNO,OUTWARDNO,FUELTOKEN,AGFIN ,  AGFOUT  ,FLFIN     ,  FLFOUT ,  AGFMIN ,  AGFMOUT    ,  FLFDIN ,  FLFDOUT  , AGFCIN,AGFCOUT, AGFKIN,AGFKOUT,AGFPIN,AGFPOUT,AGFMGIIIN,AGFMGIIOUT,AGFSAMPLE,AGFMGIISAMPLE,AGFSAMPLEACTIVE,AGFMGIISAMPLEACTIVE, ACTIVE  , USERNAME , COMPCODE1, MODIFIED ,  CREATEDON,  IPADDRESS) " +
                        //       " VALUES('" + txtintoutsequenceid1.Text + "','" + combo_compcode.SelectedValue + "','" + combofinyear.SelectedValue + "','" + txtgatedcno.Text + "','" + txtinwardstart.Text + "','" + txtoutwardstart.Text + "','" + txtfuelno.Text + "','" + AGFIN + "', '" + AGFOUT + "','" + FLFIN + "','" + FLFOUT + "','" + AGFMIN + "','" + AGFMOUT + "','" + FLFDIN + "','" + FLFDOUT + "','" + AGFCIN + "', '" + AGFCOUT + "','" + AGFKIN + "','" + AGFKOUT + "','" + AGFPIN + "','" + AGFPOUT + "','" + AGFMGIIIN + "','" + AGFMGIIOUT + "','" + txtagfsample.Text + "','" + txtagfmgiisample.Text + "','" + AGFSAMPLEACTIVE + "','" + AGFMGIISAMPLEACTIVE + "','" + chk + "','" + Class.Users.USERID + "', '" + Class.Users.COMPCODE + "',to_date('" + Convert.ToDateTime(Class.Users.CREATED).ToString() + "', 'dd/MM/yyyy hh24:MI:SS'),to_date('" + Convert.ToDateTime(Class.Users.CREATED) + "', 'dd/MM/yyyy hh24:MI:SS'),'" + Class.Users.IPADDRESS + "')";


                        MessageBox.Show("Child Record Found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        empty();
                        return;
                    }
                    else if (dt.Rows.Count != 0 && Convert.ToInt64("0"+txtintoutsequenceid.Text) == 0 || Convert.ToInt64("0" + txtintoutsequenceid.Text) == 0)
                    {
                        string ins = "INSERT INTO ASPTBLINOUTMAS(ASPTBLINOUTMASID1 ,  COMPCODE   , FINYEAR,GATEDCNO,INWARDNO,OUTWARDNO,FUELTOKEN,AGFIN ,  AGFOUT  ,FLFIN     ,  FLFOUT ,  AGFMIN ,  AGFMOUT    ,  FLFDIN ,  FLFDOUT  , AGFCIN,AGFCOUT, AGFKIN,AGFKOUT,AGFPIN,AGFPOUT,AGFMGIIIN,AGFMGIIOUT,AGFSAMPLE,AGFMGIISAMPLE,AGFSAMPLEACTIVE,AGFMGIISAMPLEACTIVE, ACTIVE  , USERNAME , COMPCODE1, MODIFIED ,  CREATEDON,  IPADDRESS,VELIN,VELOUT,OTHERSIN,OTHERSOUT) " +
                            " VALUES('" + txtintoutsequenceid1.Text + "','" + combo_compcode.SelectedValue + "','" + combofinyear.SelectedValue + "','" + txtgatedcno.Text + "','" + txtinwardstart.Text + "','" + txtoutwardstart.Text + "','" + txtfuelno.Text + "','" + AGFIN + "', '" + AGFOUT + "','" + FLFIN + "','" + FLFOUT + "','" + AGFMIN + "','" + AGFMOUT + "','" + FLFDIN + "','" + FLFDOUT + "','" + AGFCIN + "', '" + AGFCOUT + "','" + AGFKIN + "','" + AGFKOUT + "','" + AGFPIN + "','" + AGFPOUT + "','" + AGFMGIIIN + "','" + AGFMGIIOUT + "','" + txtagfsample.Text + "','" + txtagfmgiisample.Text + "','" + AGFSAMPLEACTIVE + "','" + AGFMGIISAMPLEACTIVE + "','" + chk + "','" + Class.Users.USERID + "', '" + combo_compcode.SelectedValue + "',to_date('" + Convert.ToDateTime(Class.Users.CREATED).ToString() + "', 'dd/MM/yyyy hh24:MI:SS'),to_date('" + Convert.ToDateTime(Class.Users.CREATED) + "', 'dd/MM/yyyy hh24:MI:SS'),'" + Class.Users.IPADDRESS + "','"+VELIN+ "','" + VELOUT + "','"+chkothersIN+ "','" + chkothersout + "')";
                        Utility.ExecuteNonQuery(ins);
                        MessageBox.Show("Record Saved Successfully " + "        " + txtintoutsequenceid.Text, "Success Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        GridLoad(Class.Users.Finyear);
                        empty();

                    }
                    else
                    {
                        string up = "update  ASPTBLINOUTMAS  set   ASPTBLINOUTMASID1='" + txtintoutsequenceid1.Text + "' , FINYEAR='" + combofinyear.SelectedValue + "' , COMPCODE='" + combo_compcode.SelectedValue + "' , GATEDCNO='" + txtgatedcno.Text + "' ,  INWARDNO='" + txtinwardstart.Text + "' , OUTWARDNO='" + txtoutwardstart.Text + "', FUELTOKEN='" + txtfuelno.Text + "' ,AGFIN='" + AGFIN + "' ,  AGFOUT='" + AGFOUT + "' ,  FLFIN='" + FLFIN + "' ,  FLFOUT='" + FLFOUT + "' ,  AGFMIN='" + AGFMIN + "' ,  AGFMOUT='" + AGFMOUT + "' ,  FLFDIN='" + FLFDIN + "' ,  FLFDOUT='" + FLFDOUT + "' , AGFCIN='" + AGFCIN + "', AGFCOUT='" + AGFCOUT + "',AGFKIN='" + AGFKIN + "',AGFKOUT='" + AGFKOUT + "',AGFPIN='" + AGFPIN + "',AGFPOUT='" + AGFPOUT + "',AGFMGIIIN='" + AGFMGIIIN + "',AGFMGIIOUT='" + AGFMGIIOUT + "',   AGFSAMPLE='" + txtagfsample.Text + "' , AGFMGIISAMPLE='" + txtagfmgiisample.Text + "' , AGFSAMPLEACTIVE='" + AGFSAMPLEACTIVE + "' , AGFMGIISAMPLEACTIVE='" + AGFMGIISAMPLEACTIVE + "' ,ACTIVE='" + chk + "',USERNAME='" + Class.Users.USERID + "',COMPCODE1='" + combo_compcode.SelectedValue + "',MODIFIED=to_date('" + Convert.ToDateTime(Class.Users.CREATED).ToString() + "', 'dd/MM/yyyy hh24:MI:SS'),CREATEDON=to_date('" + Convert.ToDateTime(Class.Users.CREATED) + "', 'dd/MM/yyyy hh24:MI:SS'),IPADDRESS='" + Class.Users.IPADDRESS + "'  , VELIN='" + VELIN + "' ,VELOUT='" + VELOUT + "',OTHERSIN='"+chkothersIN+ "' ,OTHERSOUT='" + chkothersout + "' where ASPTBLINOUTMASID='" + txtintoutsequenceid.Text + "'";
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
            combofinyear.SelectedIndex = -1; combo_compcode.SelectedIndex = -1; txtinwardstart.Text = ""; txtoutwardstart.Text = ""; checkactive.Checked = false; checkagfin.Checked = false; checkagfout.Checked = false;
            checkflfdin.Checked = false; checkflfin.Checked = false; checkflfout.Checked = false; checkagfmin.Checked = false; checkagfmout.Checked = false;
            checkflfdin.Checked = false; checkflfdout.Checked = false;checkOthersIN.Checked = false;
            checkagfkin.Checked = false; checkagfkout.Checked = false; checkagfcin.Checked = false; checkagfcout.Checked = false;
            checkagfpin.Checked = false; checkagfpout.Checked = false; checkagfmgiiin.Checked = false; checkagfmgiiout.Checked = false;
            checkagfsample.Checked = false; checkagfmgiisample.Checked = false;checkagfkout.Checked = false;
            txtagfsample.Text = "";txtagfmgiisample.Text = "";
            butheader.BackColor = Class.Users.BackColors;
            butfooter.BackColor = Class.Users.BackColors;
            panel2.BackColor = Class.Users.BackColors;
            panel3.BackColor = Class.Users.BackColors;
        }
       public void GridLoad() {
            DataTable dt = new DataTable();
           
                dt = null; Class.Users.Intimation = "PAYROLL";
                string sel = "SELECT A.ASPTBLINOUTMASID,C.FINYR AS FINYEAR,B.COMPCODE,A.GATEDCNO,A.INWARDNO, A.OUTWARDNO,A.AGFSAMPLE,A.AGFMGIISAMPLE, A.ACTIVE  FROM  ASPTBLINOUTMAS A JOIN GTCOMPMAST B ON A.COMPCODE=B.GTCOMPMASTID   JOIN GTFINANCIALYEAR C ON A.FINYEAR=C.GTFINANCIALYEARID   ORDER BY A.ASPTBLINOUTMASID DESC";
                DataSet ds = Utility.ExecuteSelectQuery(sel, "ASPTBLINOUTMAS");
                dt = ds.Tables["ASPTBLINOUTMAS"];
           
            listView1.Items.Clear();
            if (dt.Rows.Count > 0)
            {
                int i = 1;
                foreach (DataRow myRow in dt.Rows)
                {
                    ListViewItem list = new ListViewItem();
                    list.Text = i.ToString();
                    list.SubItems.Add(myRow["ASPTBLINOUTMASID"].ToString());
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
            string sel = "SELECT A.ASPTBLINOUTMASID,C.FINYR AS FINYEAR,B.COMPCODE,A.GATEDCNO,A.INWARDNO, A.OUTWARDNO,A.AGFSAMPLE,A.AGFMGIISAMPLE,  A.ACTIVE  FROM  ASPTBLINOUTMAS A JOIN GTCOMPMAST B ON A.COMPCODE=B.GTCOMPMASTID   JOIN GTFINANCIALYEAR C ON A.FINYEAR=C.GTFINANCIALYEARID where c.finyr='"+ fin + "'  ORDER BY A.ASPTBLINOUTMASID DESC";
            DataSet ds = Utility.ExecuteSelectQuery(sel, "ASPTBLINOUTMAS");
            dt = ds.Tables["ASPTBLINOUTMAS"];
                listView1.Items.Clear();
                if (dt.Rows.Count > 0)
                {
                    int i = 1;
                    foreach (DataRow myRow in dt.Rows)
                    {
                        ListViewItem list = new ListViewItem();
                        list.Text = i.ToString();
                        list.SubItems.Add(myRow["ASPTBLINOUTMASID"].ToString());
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
                string sel = " SELECT A.ASPTBLINOUTMASID,A.ASPTBLINOUTMASID1,A.FINYEAR,A.COMPCODE,A.GATEDCNO,A.INWARDNO, A.OUTWARDNO,A.FUELTOKEN,A.AGFIN,A.AGFOUT,A.FLFIN,A.FLFOUT,A.AGFMIN,A.AGFMOUT,A.FLFDIN,A.FLFDOUT,A.AGFKIN,A.AGFKOUT,A.AGFPIN,A.AGFPOUT,A.AGFCIN,A.AGFCOUT,A.AGFMGIIIN,A.AGFMGIIOUT,A.AGFSAMPLEACTIVE,A.AGFMGIISAMPLEACTIVE,A.AGFSAMPLE,A.AGFMGIISAMPLE,A.VELIN,VELOUT, A.ACTIVE,A.OTHERSIN,A.OTHERSOUT    FROM  ASPTBLINOUTMAS A JOIN GTCOMPMAST B ON A.COMPCODE=B.GTCOMPMASTID  JOIN GTFINANCIALYEAR C ON A.FINYEAR=C.GTFINANCIALYEARID WHERE A.ASPTBLINOUTMASID='" + txtintoutsequenceid.Text + "' ";
                DataSet ds = Utility.ExecuteSelectQuery(sel, "ASPTBLINOUTMAS");
                DataTable dt = ds.Tables["ASPTBLINOUTMAS"];
                if (dt.Rows.Count > 0)
                {
                    txtintoutsequenceid.Text = Convert.ToString(dt.Rows[0]["ASPTBLINOUTMASID"].ToString());
                    txtintoutsequenceid1.Text = Convert.ToString(dt.Rows[0]["ASPTBLINOUTMASID1"].ToString());
                    combofinyear.SelectedValue = Convert.ToString(dt.Rows[0]["FINYEAR"].ToString());
                    combo_compcode.SelectedValue = Convert.ToString(dt.Rows[0]["COMPCODE"].ToString());
                    txtgatedcno.Text = Convert.ToString(dt.Rows[0]["GATEDCNO"].ToString());
                    txtinwardstart.Text = Convert.ToString(dt.Rows[0]["INWARDNO"].ToString());
                    txtoutwardstart.Text = Convert.ToString(dt.Rows[0]["OUTWARDNO"].ToString());
                    txtfuelno.Text = Convert.ToString(dt.Rows[0]["FUELTOKEN"].ToString());
                    txtagfsample.Text = Convert.ToString(dt.Rows[0]["AGFSAMPLE"].ToString());
                    txtagfmgiisample.Text = Convert.ToString(dt.Rows[0]["AGFMGIISAMPLE"].ToString());
                    if (dt.Rows[0]["AGFIN"].ToString() == "T") { checkagfin.Checked = true;checkagfin.ForeColor = Color.Red;  } else { checkagfin.Checked = false; checkagfin.ForeColor = Color.Black; }
                    if (dt.Rows[0]["AGFOUT"].ToString() == "T") { checkagfout.Checked = true; checkagfout.ForeColor = Color.Red; } else { checkagfout.Checked = false; checkagfout.ForeColor = Color.Black; }
                    if (dt.Rows[0]["FLFIN"].ToString() == "T") { checkflfin.Checked = true; checkflfin.ForeColor = Color.Red; } else { checkflfin.Checked = false; checkflfin.ForeColor = Color.Black; }
                    if (dt.Rows[0]["FLFOUT"].ToString() == "T") { checkflfout.Checked = true; checkflfout.ForeColor = Color.Red; } else { checkflfout.Checked = false; checkflfout.ForeColor = Color.Black; }
                    if (dt.Rows[0]["AGFMIN"].ToString() == "T") { checkagfmin.Checked = true; checkagfmin.ForeColor = Color.Red; } else { checkagfmin.Checked = false; checkagfmin.ForeColor = Color.Black; }
                    if (dt.Rows[0]["AGFMOUT"].ToString() == "T") { checkagfmout.Checked = true; checkagfmout.ForeColor = Color.Red; } else { checkagfmout.Checked = false; checkagfmout.ForeColor = Color.Black; }
                    if (dt.Rows[0]["FLFDIN"].ToString() == "T") { checkflfdin.Checked = true; checkflfdin.ForeColor = Color.Red; } else { checkflfdin.Checked = false; checkflfdin.ForeColor = Color.Black; }
                    if (dt.Rows[0]["FLFDOUT"].ToString() == "T") { checkflfdout.Checked = true; checkflfdout.ForeColor = Color.Red; } else { checkflfdout.Checked = false; checkflfdout.ForeColor = Color.Black; }
                    if (dt.Rows[0]["AGFKIN"].ToString() == "T") { checkagfkin.Checked = true; checkagfkin.ForeColor = Color.Red; } else { checkagfkin.Checked = false; checkagfkin.ForeColor = Color.Black; }
                    if (dt.Rows[0]["AGFKOUT"].ToString() == "T") { checkagfkout.Checked = true; checkagfkout.ForeColor = Color.Red; } else { checkagfkout.Checked = false; checkagfkout.ForeColor = Color.Black; }
                    if (dt.Rows[0]["AGFPIN"].ToString() == "T") { checkagfpin.Checked = true; checkagfpin.ForeColor = Color.Red; } else { checkagfpin.Checked = false; checkagfpin.ForeColor = Color.Black; }
                    if (dt.Rows[0]["AGFPOUT"].ToString() == "T") { checkagfpout.Checked = true; checkagfpout.ForeColor = Color.Red; } else { checkagfpout.Checked = false; checkagfpout.ForeColor = Color.Black; }
                    if (dt.Rows[0]["AGFCIN"].ToString() == "T") { checkagfcin.Checked = true; checkagfcin.ForeColor = Color.Red; } else { checkagfcin.Checked = false; checkagfcin.ForeColor = Color.Black; }
                    if (dt.Rows[0]["AGFCOUT"].ToString() == "T") { checkagfcout.Checked = true; checkagfcout.ForeColor = Color.Red; } else { checkagfcout.Checked = false; checkagfcout.ForeColor = Color.Black; }
                    if (dt.Rows[0]["AGFMGIIIN"].ToString() == "T") { checkagfmgiiin.Checked = true; checkagfmgiiin.ForeColor = Color.Red; } else { checkagfmgiiin.Checked = false; checkagfmgiiin.ForeColor = Color.Black; }
                    if (dt.Rows[0]["AGFMGIIOUT"].ToString() == "T") { checkagfmgiiout.Checked = true; checkagfmgiiout.ForeColor = Color.Red; } else { checkagfmgiiout.Checked = false; checkagfmgiiout.ForeColor = Color.Black; }
                    if (dt.Rows[0]["AGFSAMPLEACTIVE"].ToString() == "T") { checkagfsample.Checked = true; checkagfsample.ForeColor = Color.Red; } else { checkagfsample.Checked = false; checkagfsample.ForeColor = Color.Black; }
                    if (dt.Rows[0]["AGFMGIISAMPLEACTIVE"].ToString() == "T") { checkagfmgiisample.Checked = true; checkagfmgiisample.ForeColor = Color.Red; } else { checkagfmgiisample.Checked = false; checkagfmgiisample.ForeColor = Color.Black; }

                                        if (dt.Rows[0]["VELIN"].ToString() == "T") { checkvelin.Checked = true; checkvelin.ForeColor = Color.Red; } else { checkvelin.Checked = false; checkvelin.ForeColor = Color.Black; }

                    if (dt.Rows[0]["VELOUT"].ToString() == "T") { checkvelout.Checked = true; checkvelout.ForeColor = Color.Red; } else { checkvelout.Checked = false; checkvelout.ForeColor = Color.Black; }

                    if (dt.Rows[0]["ACTIVE"].ToString() == "T") { checkactive.Checked = true; checkactive.ForeColor = Color.Red; } else { checkactive.Checked = false; checkactive.ForeColor = Color.Black; }

                    if (dt.Rows[0]["OTHERSIN"].ToString() == "T") { checkOthersIN.Checked = true; checkOthersIN.ForeColor = Color.Red; } else { checkOthersIN.Checked = false; checkOthersIN.ForeColor = Color.Black; }
                    if (dt.Rows[0]["OTHERSOUT"].ToString() == "T") { checkOthersOut.Checked = true; checkOthersOut.ForeColor = Color.Red; } else { checkOthersOut.Checked = false; checkOthersOut.ForeColor = Color.Black; }

                 
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
                    string sel1 = "SELECT A.ASPTBLINOUTMASID,C.FINYR AS FINYEAR,B.COMPCODE,,A.GATEDCNO,A.INWARDNO, A.OUTWARDNO,A.ACTIVE    FROM  ASPTBLINOUTMAS A JOIN GTCOMPMAST B ON A.COMPCODE=B.GTCOMPMASTID  JOIN GTFINANCIALYEAR C ON A.FINYEAR=C.GTFINANCIALYEARID where B.COMPCODE LIKE'%" + txtsearch.Text.ToUpper() + "%' || A.GATEDCNO LIKE'%" + txtsearch.Text.ToUpper() + "%' || C.FINYR=LIKE'%" + txtsearch.Text.ToUpper() + "%'  ";
                    DataSet ds = Utility.ExecuteSelectQuery(sel1, "ASPTBLINOUTMAS");
                    DataTable dt = ds.Tables["ASPTBLINOUTMAS"];
                    if (dt.Rows.Count > 0)
                    {

                        foreach (DataRow myRow in dt.Rows)
                        {
                            ListViewItem list = new ListViewItem();
                            list.Text = iGLCount.ToString();
                            list.SubItems.Add(myRow["ASPTBLINOUTMASID"].ToString());
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
