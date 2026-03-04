using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Pinnacle.Hostel
{
    public partial class MachineMaster : Form,ToolStripAccess
    {
        public MachineMaster()
        {
            InitializeComponent();
      
           
            Class.Users.IPADDRESS = GenFun.GetLocalIPAddress();
            Class.Users.CREATED = Convert.ToDateTime(System.DateTime.Now.ToString("dd-MMM-yyyy") + " " + System.DateTime.Now.ToLongTimeString());
            Class.Users.SysDate = Convert.ToString(System.DateTime.Now.ToString("dd/MM/yyyy"));
            Class.Users.SysTime = Convert.ToString(DateTime.Now.ToLongTimeString().ToString());
            GlobalVariables.HeaderName.Text = Class.Users.ScreenName; GlobalVariables.CurrentForm = this;
            butheader.BackColor = Class.Users.BackColors;
            this.BackColor = Class.Users.BackColors;
            panel2.BackColor = Class.Users.BackColors; Class.Users.UserTime = 0;
            panel3.BackColor = Class.Users.BackColors;
            if (Class.Users.HUnitSub == "CANTEEN" || Class.Users.HUnitSub == "Canteen")
            {
                tableLayoutPanel1.Visible = false;
            }
            else
            {
                tableLayoutPanel1.Visible = true;
            }
        }

        ListView listfilter = new ListView();

        private static MachineMaster _instance;
        Models.Master mas = new Models.Master();
        Models.Employee em = new Models.Employee();
        Models.UserRights sm = new Models.UserRights();
     
        PinnacleMdi mdi = new PinnacleMdi();
        public static MachineMaster Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new MachineMaster();
                GlobalVariables.CurrentForm = _instance; return _instance;
            }
        }


        public void ReadOnlys()
        { }
        public void News()
        {
            txtmachineid.Text = "";
            combo_compcode.Text = ""; txtsessiontime.Text = ""; comboMTYPE2.Text = ""; comboMTYPE2.SelectedIndex = -1;
            comboipaddress.Text = "";
            combowardenname.Text = "";
            checkactive.Checked = true; 
            radioBoysHostel.Checked = true; 
            butheader.BackColor = Class.Users.BackColors;
            this.BackColor = Class.Users.BackColors;
            panel2.BackColor = Class.Users.BackColors; 
            panel3.BackColor = Class.Users.BackColors; LoadUser(); GridLoad(); this.combo_compcode.Select();


        }
        private void userload()
        {
           

            //string sel1 = "SELECT distinct  D.USERNAME   FROM  ASPTBLMACHINEMAS A   JOIN   GTCOMPMAST B ON B.GTCOMPMASTID = A.COMPCODE    JOIN ASPTBLMACIP C ON C.ASPTBLMACIPID=A.IPADDRESS   AND C.ACTIVE='T'  JOIN  ASPTBLUSERMAS D ON D.USERID=A.WARDENNAME  ORDER BY 4";
            //DataSet ds = Utility.ExecuteSelectQuery(sel1, "ASPTBLMACHINEMAS");
            //DataTable dt = ds.Tables["ASPTBLMACHINEMAS"];
            //if (dt != null)
            //{
            //    comboUser.DataSource = dt;
            //    comboUser.DisplayMember = "USERNAME";
            //    comboUser.ValueMember = "USERNAME";
            //}
        }
        private void combo_compcode1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = mas.username(combo_compcode1.Text); Class.Users.UserTime = 0;
            if (dt.Rows.Count > 0)
            {
                comboUser.DataSource = dt;
                comboUser.DisplayMember = "USERNAME";
                comboUser.ValueMember = "USERNAME";
            }
            else
            {
                comboUser.DataSource = null;
                comboUser.Text = ""; comboUser.SelectedIndex = -1; 
            }
        }
        //private void hostelload()
        //{
        //    try
        //    {
        //        string sel3 = " select DISTINCT  A.HOSTELNAME from HOSTELLIVEDATA A  ORDER BY 1";
        //        DataSet ds3 = Utility.ExecuteSelectQuery(sel3, "HOSTELLIVEDATA");
        //        DataTable dt3 = ds3.Tables["HOSTELLIVEDATA"];
        //        if (dt3.Rows.Count > 0)
        //        {
        //            int i = 1;
        //            foreach (DataRow myRow in dt3.Rows)
        //            {
        //                ListViewItem list = new ListViewItem();
        //                list.SubItems.Add(i.ToString());
        //                list.SubItems.Add(myRow["HOSTELNAME"].ToString());

        //                listView1.Items.Add(list);

        //                i++;
        //            }

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Data Source Not Connected" + ex.Message);
        //    }
        //}
        public void Saves()
        {
            try
            {
                if (combo_compcode.Text != "" && combowardenname.Text != "" && comboipaddress.Text != "" && comboMTYPE2.Text != "" && comboipaddress.SelectedValue.ToString() != "")
                {
                    string chk = "";
                 
                    string hostel="", agfchk="", velchk = "", flfchk ="", flfdchk="", agfmchk = "",agfmgiichk="",agfcchk = "", agfpchk="", agfkchk = "",velu2="",others="";

                    if (checkactive.Checked == true) { chk = "T"; } else { chk = "F"; }

                    if (radioAGF.Checked == true) { agfchk = "AGF"; hostel = "AGF"; } else { agfchk = "";  }
                    if (radioFLF.Checked == true) { flfchk = "FLF"; hostel = "FLF"; } else { flfchk = "";  }
                    if (radioVEL.Checked == true) { velchk = "VEL"; hostel = "VEL"; } else { velchk = ""; }
                    if (radioFLFD.Checked == true) { flfdchk = "FLFD"; hostel = "FLFD"; } else { flfdchk = "";  }
                    if (radioAGFM.Checked == true) { agfmchk = "AGFM"; hostel = "AGFM"; } else { agfmchk = "";  }
                    if (radioAGFMGII.Checked == true) { agfmgiichk = "AGFMGII"; hostel = "AGFMGII"; } else { agfmgiichk = ""; }
                    if (radioAGFC.Checked == true) { agfcchk = "AGFC"; hostel = "AGFC"; } else { agfcchk = ""; }
                    if (radioAGFP.Checked == true) { agfpchk = "AGFP"; hostel = "AGFP"; } else { agfpchk = ""; }
                    if (radioAGFK.Checked == true) { agfkchk = "AGFK"; hostel = "AGFK"; } else { agfkchk = ""; }
                    if (radioBoysHostel.Checked == true) { hostel = "WORKING GENTS HOSTEL"; } else {   }
                    if (radioGirlsHostel.Checked == true) { hostel = "WOMENS HOSTEL"; } else { }
                    if (radiosecurity.Checked == true) { hostel = "SECURITY"; } else { }
                    if (radioVELU2.Checked == true) { velu2 = "VELU2"; } else { velu2 = ""; }
                    if (radioOthers.Checked == true) { others = "OTHERS"; radioAGF.Checked = false; radioFLF.Checked = false;
                        radioVEL.Checked = false; radioFLFD.Checked = false; radioAGFM.Checked = false;
                        radioAGFMGII.Checked = false; radioAGFC.Checked = false; radioAGFP.Checked = false;
                        radioAGFK.Checked = false; radioBoysHostel.Checked = false; radiosecurity.Checked = false;
                        radioVELU2.Checked = false;
                    } else { others = ""; }
                    Class.Users.Intimation = "PAYROLL";
                    if (radioOthers.Checked == true) {

                        string sel = "select A.ASPTBLMACHINEMASID,a.username FROM ASPTBLMACHINEMAS A  WHERE A.COMPCODE=" + combo_compcode.SelectedValue + " AND A.WARDENNAME=" + combowardenname.SelectedValue + " AND A.IPADDRESS=" + comboipaddress.SelectedValue + " AND A.HOSTELNAME='" + hostel + "'  AND A.ACTIVE='" + chk + "' AND A.OTHERS='" + others + "'  AND A.MACIP='" + comboipaddress.Text + "'  AND A.MTYPE2='" + comboMTYPE2.Text + "' and A.SESSIONTIME='" + txtsessiontime.Text + "'";
                        DataSet ds = Utility.ExecuteSelectQuery(sel, "ASPTBLMACHINEMAS");
                        DataTable dt = ds.Tables["ASPTBLMACHINEMAS"];
                        if (dt.Rows.Count != 0)
                        {
                            MessageBox.Show("Child Record Found     :" + hostel, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else if (dt.Rows.Count == 0 && Convert.ToInt32("0" + txtmachineid.Text) == 0 || Convert.ToInt32("0" + txtmachineid.Text) == 0)
                        {
                            string ins = "INSERT INTO ASPTBLMACHINEMAS(COMPCODE,WARDENNAME,IPADDRESS,HOSTELNAME,ACTIVE,OTHERS,USERNAME,  MODIFIED,  CREATEDON,  IPADDRESS1,MACIP,MTYPE2,SESSIONTIME)VALUES(" + combo_compcode.SelectedValue + "," + combowardenname.SelectedValue + "," + comboipaddress.SelectedValue + ",'" + hostel + "','" + chk + "','" + others + "'," + Class.Users.USERID + ",to_date('" + Convert.ToDateTime(Class.Users.CREATED).ToString() + "', 'dd/MM/yyyy hh24:MI:SS'),to_date('" + Convert.ToDateTime(Class.Users.CREATED).ToString() + "', 'dd/MM/yyyy hh24:MI:SS'),'" + Class.Users.IPADDRESS + "' ,'" + comboipaddress.Text + "','" + comboMTYPE2.Text + "','" + txtsessiontime.Text + "' )";
                            Utility.ExecuteNonQuery(ins);

                            MessageBox.Show("Record Saved Successfully    :" + combowardenname.Text, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                        else
                        {
                            string up = "UPDATE ASPTBLMACHINEMAS SET COMPCODE=" + combo_compcode.SelectedValue + ", WARDENNAME=" + combowardenname.SelectedValue + ",IPADDRESS=" + comboipaddress.SelectedValue + ", HOSTELNAME='" + hostel + "',ACTIVE='" + chk + "', OTHERS='" + others + "' , USERNAME=" + Class.Users.USERID + ",  MODIFIED=to_date('" + Convert.ToDateTime(Class.Users.CREATED).ToString() + "', 'dd/MM/yyyy hh24:MI:SS'),IPADDRESS1='" + Class.Users.IPADDRESS + "' , MACIP='" + comboipaddress.Text + "',MTYPE2='" + comboMTYPE2.Text + "'  , SESSIONTIME='" + txtsessiontime.Text + "' WHERE  ASPTBLMACHINEMASID=" + txtmachineid.Text;
                            Utility.ExecuteNonQuery(up);
                            string up1 = "update asptblusermas set SESSIONTIME='" + txtsessiontime.Text + "' where   userid='" + combowardenname.SelectedValue + "'";
                            Utility.ExecuteNonQuery(up1);
                            MessageBox.Show("Record Updated     :" + combowardenname.Text, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        string sel = "select A.ASPTBLMACHINEMASID,a.username FROM ASPTBLMACHINEMAS A  WHERE A.COMPCODE=" + combo_compcode.SelectedValue + " AND A.WARDENNAME=" + combowardenname.SelectedValue + " AND A.IPADDRESS=" + comboipaddress.SelectedValue + " AND A.HOSTELNAME='" + hostel + "'  AND A.ACTIVE='" + chk + "' AND A.AGF='" + agfchk + "' AND A.FLF='" + flfchk + "' AND A.VEL='" + velchk + "' AND A.FLFD='" + flfdchk + "' AND A.AGFM='" + agfmchk + "' AND A.AGFMGII='" + agfmgiichk + "' AND A.AGFC='" + agfcchk + "' AND A.MACIP='" + comboipaddress.Text + "' AND A.AGFP='" + agfpchk + "' AND A.AGFK='" + agfkchk + "' AND A.VELU2='" + velu2 + "' AND A.MTYPE2='" + comboMTYPE2.Text + "' and A.SESSIONTIME='" + txtsessiontime.Text + "'";
                        DataSet ds = Utility.ExecuteSelectQuery(sel, "ASPTBLMACHINEMAS");
                        DataTable dt = ds.Tables["ASPTBLMACHINEMAS"];
                        if (dt.Rows.Count != 0)
                        {
                            MessageBox.Show("Child Record Found     :" + hostel, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else if (dt.Rows.Count == 0 && Convert.ToInt32("0" + txtmachineid.Text) == 0 || Convert.ToInt32("0" + txtmachineid.Text) == 0)
                        {
                            string ins = "INSERT INTO ASPTBLMACHINEMAS(COMPCODE,WARDENNAME,IPADDRESS,HOSTELNAME,ACTIVE,AGF,FLF,VEL,FLFD,AGFM,AGFMGII,AGFC, AGFP,AGFK,VELU2,USERNAME,  MODIFIED,  CREATEDON,  IPADDRESS1,MACIP,MTYPE2,SESSIONTIME)VALUES(" + combo_compcode.SelectedValue + "," + combowardenname.SelectedValue + "," + comboipaddress.SelectedValue + ",'" + hostel + "','" + chk + "','" + agfchk + "','" + flfchk + "','" + velchk + "','" + flfdchk + "','" + agfmchk + "','" + agfmgiichk + "','" + agfcchk + "','" + agfpchk + "', '" + agfkchk + "','" + velu2 + "'," + Class.Users.USERID + ",to_date('" + Convert.ToDateTime(Class.Users.CREATED).ToString() + "', 'dd/MM/yyyy hh24:MI:SS'),to_date('" + Convert.ToDateTime(Class.Users.CREATED).ToString() + "', 'dd/MM/yyyy hh24:MI:SS'),'" + Class.Users.IPADDRESS + "' ,'" + comboipaddress.Text + "','" + comboMTYPE2.Text + "','" + txtsessiontime.Text + "' )";
                            Utility.ExecuteNonQuery(ins);

                            MessageBox.Show("Record Saved Successfully    :" + combowardenname.Text, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                        else
                        {
                            string up = "UPDATE ASPTBLMACHINEMAS SET COMPCODE=" + combo_compcode.SelectedValue + ", WARDENNAME=" + combowardenname.SelectedValue + ",IPADDRESS=" + comboipaddress.SelectedValue + ", HOSTELNAME='" + hostel + "',ACTIVE='" + chk + "', AGF='" + agfchk + "' , FLF='" + flfchk + "' ,VEL='" + velchk + "', FLFD='" + flfdchk + "' , AGFM='" + agfmchk + "', AGFMGII='" + agfmgiichk + "',AGFC='" + agfcchk + "', AGFP='" + agfpchk + "',AGFK='" + agfkchk + "',VELU2='" + velu2 + "', USERNAME=" + Class.Users.USERID + ",  MODIFIED=to_date('" + Convert.ToDateTime(Class.Users.CREATED).ToString() + "', 'dd/MM/yyyy hh24:MI:SS'),IPADDRESS1='" + Class.Users.IPADDRESS + "' , MACIP='" + comboipaddress.Text + "',MTYPE2='" + comboMTYPE2.Text + "'  , SESSIONTIME='" + txtsessiontime.Text + "' WHERE  ASPTBLMACHINEMASID=" + txtmachineid.Text;
                            Utility.ExecuteNonQuery(up);
                            string up1 = "update asptblusermas set SESSIONTIME='" + txtsessiontime.Text + "' where   userid='" + combowardenname.SelectedValue + "'";
                            Utility.ExecuteNonQuery(up1);
                            MessageBox.Show("Record Updated     :" + combowardenname.Text, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                        GridLoad();
                   
                    News();
                }
                else
                {
                    MessageBox.Show("PLS Enter Mandatary Fields");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

      public  void GridLoad()
        {
            try
            {
                int i = 1;
                listmachine.Items.Clear();DataTable dt = new DataTable(); listfilter.Items.Clear();
                if (Class.Users.HUserName == "VAIRAM")
                {
                    string sel1 = "SELECT A.ASPTBLMACHINEMASID, B.COMPCODE , D.USERNAME AS WARDENNAME,C.MACIP  AS IPADDRESS ,A.HOSTELNAME,A.MTYPE2,D.SESSIONTIME,A.ACTIVE   FROM  ASPTBLMACHINEMAS A   JOIN   GTCOMPMAST B ON B.GTCOMPMASTID = A.COMPCODE    JOIN ASPTBLMACIP C ON C.ASPTBLMACIPID=A.IPADDRESS   AND C.ACTIVE='T'  JOIN  ASPTBLUSERMAS D ON D.USERID=A.WARDENNAME  ORDER BY 2,4";
                    DataSet ds = Utility.ExecuteSelectQuery(sel1, "ASPTBLMACHINEMAS");
                    dt = ds.Tables["ASPTBLMACHINEMAS"];

                }
                else
                {
                    string sel1 = "SELECT A.ASPTBLMACHINEMASID, B.COMPCODE , D.USERNAME AS WARDENNAME,C.MACIP  AS IPADDRESS ,A.HOSTELNAME,A.MTYPE2,D.SESSIONTIME, A.ACTIVE   FROM  ASPTBLMACHINEMAS A   JOIN   GTCOMPMAST B ON B.GTCOMPMASTID = A.COMPCODE    JOIN ASPTBLMACIP C ON C.ASPTBLMACIPID=A.IPADDRESS  AND C.ACTIVE='T'   JOIN  ASPTBLUSERMAS D ON D.USERID=A.WARDENNAME  AND D.COMPCODE=B.GTCOMPMASTID  WHERE B.COMPCODE='" + Class.Users.HCompcode + "' AND D.USERNAME='" + Class.Users.HUserName + "' ORDER BY 4";
                    DataSet ds = Utility.ExecuteSelectQuery(sel1, "ASPTBLMACHINEMAS");
                    dt = ds.Tables["ASPTBLMACHINEMAS"];
                }

                if (dt != null)
                {

                    foreach (DataRow myRow in dt.Rows)
                    {
                        ListViewItem list = new ListViewItem();
                        list.SubItems.Add(myRow["ASPTBLMACHINEMASID"].ToString());
                        list.SubItems.Add(myRow["COMPCODE"].ToString());
                        list.SubItems.Add(myRow["WARDENNAME"].ToString());
                        list.SubItems.Add(myRow["IPADDRESS"].ToString());
                        list.SubItems.Add(myRow["HOSTELNAME"].ToString());
                        list.SubItems.Add(myRow["MTYPE2"].ToString());
                        list.SubItems.Add(myRow["SESSIONTIME"].ToString());
                        list.SubItems.Add(myRow["ACTIVE"].ToString());
                        if (i % 2 == 0)
                        {
                            list.BackColor = Color.White;
                        }
                        else
                        {
                            list.BackColor = Color.WhiteSmoke;
                        }
                        if (myRow["MTYPE2"].ToString() == "TRANSER IP")
                        {
                            list.BackColor = this.BackColor;
                            list.ForeColor = Color.White;
                        }
                        if (myRow["MTYPE2"].ToString() == "OUTPASS")
                        {
                            list.BackColor = Color.Navy;
                            list.ForeColor = Color.White;
                        }
                        this.listfilter.Items.Add((ListViewItem)list.Clone());
                        listmachine.Items.Add(list);i++;
                    }
                    lbltotal.Text = "Total Count    :" + listmachine.Items.Count;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void comboUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                listmachine.Items.Clear(); DataTable dt = new DataTable(); listfilter.Items.Clear();
                //if (Class.Users.HUserName == "VAIRAM")
                //{
                //    string sel1 = "SELECT A.ASPTBLMACHINEMASID, B.COMPCODE , D.USERNAME AS WARDENNAME,C.MACIP  AS IPADDRESS ,A.HOSTELNAME,C.MTYPE2,  A.ACTIVE   FROM  ASPTBLMACHINEMAS A   JOIN   GTCOMPMAST B ON B.GTCOMPMASTID = A.COMPCODE    JOIN ASPTBLMACIP C ON C.ASPTBLMACIPID=A.IPADDRESS   AND C.ACTIVE='T'  JOIN  ASPTBLUSERMAS D ON D.USERID=A.WARDENNAME ORDER BY 4";
                //    DataSet ds = Utility.ExecuteSelectQuery(sel1, "ASPTBLMACHINEMAS");
                //    dt = ds.Tables["ASPTBLMACHINEMAS"];
                //}
                //else
                //{
                if (Convert.ToString(comboUser.SelectedValue)  != "System.Data.DataRowView" && Convert.ToInt64("0" + combo_compcode1.SelectedValue) > 0)
                {
                    string sel1 = "SELECT A.ASPTBLMACHINEMASID, B.COMPCODE , D.USERNAME AS WARDENNAME,C.MACIP  AS IPADDRESS ,A.HOSTELNAME,A.ACTIVE,A.MTYPE2,D.SESSIONTIME   FROM  ASPTBLMACHINEMAS A   JOIN   GTCOMPMAST B ON B.GTCOMPMASTID = A.COMPCODE    JOIN ASPTBLMACIP C ON C.ASPTBLMACIPID=A.IPADDRESS  AND C.ACTIVE='T'   JOIN  ASPTBLUSERMAS D ON D.USERID=A.WARDENNAME  AND D.COMPCODE=B.GTCOMPMASTID  WHERE B.COMPCODE='" + combo_compcode1.Text + "' AND D.USERNAME='" + comboUser.Text + "' ORDER BY 4";
                    DataSet ds = Utility.ExecuteSelectQuery(sel1, "ASPTBLMACHINEMAS");
                    dt = ds.Tables["ASPTBLMACHINEMAS"];
                    // }
                    int i = 1;
                    if (dt.Rows.Count > 0)
                    {

                        foreach (DataRow myRow in dt.Rows)
                        {
                            ListViewItem list = new ListViewItem();
                            list.SubItems.Add(myRow["ASPTBLMACHINEMASID"].ToString());
                            list.SubItems.Add(myRow["COMPCODE"].ToString());
                            list.SubItems.Add(myRow["WARDENNAME"].ToString());
                            list.SubItems.Add(myRow["IPADDRESS"].ToString());
                            list.SubItems.Add(myRow["HOSTELNAME"].ToString());
                            list.SubItems.Add(myRow["MTYPE2"].ToString());
                            list.SubItems.Add(myRow["SESSIONTIME"].ToString());
                            list.SubItems.Add(myRow["ACTIVE"].ToString());
                            if (i % 2 == 0)
                            {
                                list.BackColor = Color.White;
                            }
                            else
                            {
                                list.BackColor = Color.WhiteSmoke;
                            }
                            if (myRow["MTYPE2"].ToString() == "TRANSER IP")
                            {
                                list.BackColor = Color.Navy;
                                list.ForeColor = Color.White;
                            }
                            if (myRow["MTYPE2"].ToString() == "OUTPASS")
                            {
                                list.BackColor = Color.Blue;
                                list.ForeColor = Color.White;
                            }
                            this.listfilter.Items.Add((ListViewItem)list.Clone());
                            listmachine.Items.Add(list);
                            i++;
                        }
                        lbltotal.Text = "Total Count    :" + listmachine.Items.Count;
                    }
                }
            }
            catch (Exception ex)
            {
                
            }
        }
        void LoadUser()
        {
            DataTable dt;

            string sel = "select  DISTINCT A.GTCOMPMASTID, a.compcode  from  gtcompmast a join asptblusermas b on a.gtcompmastid = b.compcode  WHERE B.ACTIVE='T'  order by 1 ";
            DataSet ds = Utility.ExecuteSelectQuery(sel, "gtcompmast");
            dt = ds.Tables["gtcompmast"];

            if (dt.Rows.Count > 0)
            {
                combo_compcode.DisplayMember = "COMPCODE";
                combo_compcode.ValueMember = "GTCOMPMASTID";
                combo_compcode.DataSource = dt;
                combo_compcode1.DisplayMember = "COMPCODE";
                combo_compcode1.ValueMember = "GTCOMPMASTID";
                combo_compcode1.DataSource = dt;
            }
        }
        private void MachineMaster_Load(object sender, EventArgs e)
        {
            
       
        }
        void ipload(Int64 com, Int64 user)
        {
            if (Convert.ToInt64(com) > 0 &&  Convert.ToInt64("0"+ user)>0)
            {
                string sel1 = "SELECT DISTINCT  D.ASPTBLMACIPID ,D.MACIP FROM ASPTBLMACIP D  JOIN GTCOMPMAST B ON D.COMPCODE=B.GTCOMPMASTID JOIN ASPTBLUSERMAS E ON E.USERNAME=D.USERNAME  AND B.GTCOMPMASTID=E.COMPCODE  WHERE D.ACTIVE='T'  AND  e.COMPCODE='" + com + "' AND  e.USERID='" + user + "'  ORDER BY 2 ";//AND  D.COMPCODE='" + Class.Users.HCompcode + "' AND  C.USERNAME='" + Class.Users.HUserName + "'
                DataSet ds1 = Utility.ExecuteSelectQuery(sel1, "ASPTBLMACIP");
                DataTable dt1 = ds1.Tables["ASPTBLMACIP"];

                if (dt1.Rows.Count > 0)
                {


                    comboipaddress.DisplayMember = "MACIP";
                    comboipaddress.ValueMember = "ASPTBLMACIPID";
                    comboipaddress.DataSource = dt1;


                }
            }
           
        }
        private void Listmachine_ItemActivate(object sender, EventArgs e)
        {
            try
            {
                if (listmachine.Items.Count > 0)
                {
                    Class.Users.UserTime = 0;
                    txtmachineid.Text = listmachine.SelectedItems[0].SubItems[1].Text;

                    string sel1 = " SELECT A.ASPTBLMACHINEMASID, B.COMPCODE , D.USERNAME AS WARDENNAME,C.MACIP AS ASPTBLMACIPID ,A.HOSTELNAME,A.AGF,A.FLF,A.VEL,A.FLFD,A.AGFM,A.AGFMGII, A.AGFC,A.AGFP,A.AGFK,A.VELU2,A.MTYPE2,D.SESSIONTIME,A.ACTIVE,A.OTHERS   FROM  ASPTBLMACHINEMAS A   JOIN   GTCOMPMAST B ON B.GTCOMPMASTID = A.COMPCODE  JOIN ASPTBLMACIP C ON C.ASPTBLMACIPID=A.IPADDRESS   JOIN  asptblusermas D ON D.userid=A.WARDENNAME   WHERE A.ASPTBLMACHINEMASID=" + txtmachineid.Text;
                    DataSet ds1 = Utility.ExecuteSelectQuery(sel1, "ASPTBLMACHINEMAS");//C.EMPNAME,C.IDCARDNO,
                    DataTable dt = ds1.Tables["ASPTBLMACHINEMAS"];
                    if (dt.Rows.Count > 0) 
                    {
                        txtmachineid.Text = Convert.ToString(dt.Rows[0]["ASPTBLMACHINEMASID"].ToString());
                        combo_compcode.Text = Convert.ToString(dt.Rows[0]["COMPCODE"].ToString());
                        combowardenname.Text = Convert.ToString(dt.Rows[0]["WARDENNAME"].ToString());
                        comboipaddress.Text = Convert.ToString(dt.Rows[0]["ASPTBLMACIPID"].ToString());
                        if (dt.Rows[0]["HOSTELNAME"].ToString() == "WORKING GENTS HOSTEL") { radioBoysHostel.Checked = true; }

                        if (dt.Rows[0]["AGF"].ToString() == "AGF") { radioAGF.Checked = true; } else { radioAGF.Checked = false; }
                        if (dt.Rows[0]["FLF"].ToString() == "FLF") { radioFLF.Checked = true; } else { radioFLF.Checked = false; }
                        if (dt.Rows[0]["VEL"].ToString() == "VEL") { radioVEL.Checked = true; } else { radioVEL.Checked = false; }

                        if (dt.Rows[0]["FLFD"].ToString() == "FLFD") { radioFLFD.Checked = true; } else { radioFLFD.Checked = false; }
                        if (dt.Rows[0]["AGFM"].ToString() == "AGFM") { radioAGFM.Checked = true; } else { radioAGFM.Checked = false; }
                        if (dt.Rows[0]["AGFMGII"].ToString() == "AGFMGII") { radioAGFMGII.Checked = true; } else { radioAGFMGII.Checked = false; }
                        if (dt.Rows[0]["AGFC"].ToString() == "AGFC") { radioAGFC.Checked = true; } else { radioAGFC.Checked = false; }
                        if (dt.Rows[0]["AGFP"].ToString() == "AGFP") { radioAGFP.Checked = true; } else { radioAGFP.Checked = false; }
                        if (dt.Rows[0]["AGFK"].ToString() == "AGFK") { radioAGFK.Checked = true; } else { radioAGFK.Checked = false; }
                        if (dt.Rows[0]["VELU2"].ToString() == "VELU2") { radioVELU2.Checked = true; } else { radioVELU2.Checked = false; }
                        if (dt.Rows[0]["HOSTELNAME"].ToString() == "WOMENS HOSTEL") { radioGirlsHostel.Checked = true; }
                        if (dt.Rows[0]["OTHERS"].ToString() == "OTHERS") { radioOthers.Checked = true; } else { radioOthers.Checked = false; }
                        comboMTYPE2.Text = Convert.ToString(dt.Rows[0]["MTYPE2"].ToString());
                    
                        if (dt.Rows[0]["ACTIVE"].ToString() == "T") { checkactive.Checked = true; } else { checkactive.Checked = false; }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Combo_compcode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
               
                if (Convert.ToInt64("0"+combo_compcode.SelectedValue) >0)
                {
                  

                    Int64 s = Convert.ToInt64(combo_compcode.SelectedValue);
                    DataTable dt1 = mas.comcode1(s);
                    if (dt1.Rows.Count > 0)
                    {
                        combowardenname.DisplayMember = "USERNAME";
                        combowardenname.ValueMember = "userid";
                        combowardenname.DataSource = dt1;

                    }
                   
                    combowardenname.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void WardenRefreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt64("0" + combo_compcode.SelectedValue) > 0)
                {

                    Int64 s = Convert.ToInt64(combo_compcode.SelectedValue);
                    DataTable dt1 = mas.comcode1(s);
                    if (dt1.Rows.Count > 0)
                    {
                        combowardenname.DisplayMember = "USERNAME";
                        combowardenname.ValueMember = "userid";
                        combowardenname.DataSource = dt1;


                    }
                    combowardenname.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }


           
        }
        void ipload(string s)
        {
            DataTable dt1;
            try
            {

                string sel2 = "SELECT DISTINCT  D.ASPTBLMACIPID ,D.MACIP FROM ASPTBLMACIP D  JOIN GTCOMPMAST B ON D.COMPCODE=B.GTCOMPMASTID   WHERE D.ACTIVE='T' AND B.COMPCODE='" + s + "'  ORDER BY 2 ";//AND  D.COMPCODE='" + Class.Users.HCompcode + "' AND  C.USERNAME='" + Class.Users.HUserName + "'
                DataSet ds2 = Utility.ExecuteSelectQuery(sel2, "ASPTBLMACIP");
                dt1 = ds2.Tables["ASPTBLMACIP"];


                if (dt1.Rows.Count >= 0)
                {


                    comboipaddress.DisplayMember = "MACIP";
                    comboipaddress.ValueMember = "ASPTBLMACIPID";
                    comboipaddress.DataSource = dt1;


                }
                else
                {
                    comboipaddress.DataSource = null;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void IPRefreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ipload(combo_compcode.Text);
        }

        private void CompCodeRefreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    DataTable dt = mas.findcomcode(Class.Users.HCompcode, Class.Users.HUserName);
            //    if (dt.Rows.Count > 0)
            //    {
            //        combo_compcode.DisplayMember = "COMPCODE";
            //        combo_compcode.ValueMember = "GTCOMPMASTID";
            //        combo_compcode.DataSource = dt;

            LoadUser();
            //    }
            //}
            //catch (Exception ex)
            //{ }
        }

        private void Txtmachinesearch_TextChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    if (txtmachinesearch.Text != "")
            //    {
            //        listmachine.Items.Clear(); int iGLCount = 1;
            //        string sel1 = " SELECT A.ASPTBLMACHINEMASID, B.COMPCODE , D.USERNAME AS WARDENNAME,C.MACIP as IPADDRESS,A.HOSTELNAME,A.ACTIVE    FROM  ASPTBLMACHINEMAS A   JOIN   GTCOMPMAST B ON B.GTCOMPMASTID = A.COMPCODE    JOIN HRMACIPENTRYDET C ON C.HRMACIPENTRYDETID=A.IPADDRESS  JOIN  asptblusermas D ON D.userid=A.WARDENNAME  AND D.COMPCODE=B.GTCOMPMASTID WHERE B.COMPCODE='" + Class.Users.HCompcode + "' AND  D.USERNAME LIKE'%" + txtmachinesearch.Text + "%'  OR A.HOSTELNAME LIKE'%" + txtmachinesearch.Text + "%'  OR C.MACIP LIKE'%" + txtmachinesearch.Text + "%'  OR A.ACTIVE LIKE'%" + txtmachinesearch.Text + "%'";
            //        DataSet ds = Utility.ExecuteSelectQuery(sel1, "ASPTBLMACHINEMAS");
            //        DataTable dt = ds.Tables["ASPTBLMACHINEMAS"];
            //        if (dt.Rows.Count > 0)
            //        {

            //            foreach (DataRow myRow in dt.Rows)
            //            {
            //                ListViewItem list = new ListViewItem();
            //                list.Text = iGLCount.ToString();
            //                list.SubItems.Add(myRow["ASPTBLMACHINEMASID"].ToString());
            //                list.SubItems.Add(myRow["COMPCODE"].ToString());
            //                list.SubItems.Add(myRow["WARDENNAME"].ToString());
            //                list.SubItems.Add(myRow["HOSTELNAME"].ToString());
            //                list.SubItems.Add(myRow["IPADDRESS"].ToString());
            //                list.SubItems.Add(myRow["ACTIVE"].ToString());
            //                listmachine.Items.Add(list);
            //                iGLCount++;
            //            }
            //            lbltotal.Text = "Total Count    :" + listmachine.Items.Count;
            //        }
            //        else
            //        {
            //            listmachine.Items.Clear();
            //        }
            //    }
            //    else
            //    {
            //        listmachine.Items.Clear();
            //        GridLoad();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            try
            {
                listmachine.Items.Clear(); DataTable dt = new DataTable(); listfilter.Items.Clear();
                if (Class.Users.HUserName == "VAIRAM")
                {
                    string sel1 = "SELECT A.ASPTBLMACHINEMASID, B.COMPCODE , D.USERNAME AS WARDENNAME,C.MACIP  AS IPADDRESS ,A.HOSTELNAME,C.MTYPE2,  A.ACTIVE   FROM  ASPTBLMACHINEMAS A   JOIN   GTCOMPMAST B ON B.GTCOMPMASTID = A.COMPCODE    JOIN ASPTBLMACIP C ON C.ASPTBLMACIPID=A.IPADDRESS   AND C.ACTIVE='T'  JOIN  ASPTBLUSERMAS D ON D.USERID=A.WARDENNAME ORDER BY 4";
                    DataSet ds = Utility.ExecuteSelectQuery(sel1, "ASPTBLMACHINEMAS");
                    dt = ds.Tables["ASPTBLMACHINEMAS"];
                }
                else
                {
                    string sel1 = "SELECT A.ASPTBLMACHINEMASID, B.COMPCODE , D.USERNAME AS WARDENNAME,C.MACIP  AS IPADDRESS ,A.HOSTELNAME,A.ACTIVE,A.MTYPE2,D.SESSIONTIME   FROM  ASPTBLMACHINEMAS A   JOIN   GTCOMPMAST B ON B.GTCOMPMASTID = A.COMPCODE    JOIN ASPTBLMACIP C ON C.ASPTBLMACIPID=A.IPADDRESS  AND C.ACTIVE='T'   JOIN  ASPTBLUSERMAS D ON D.USERID=A.WARDENNAME  AND D.COMPCODE=B.GTCOMPMASTID  WHERE B.COMPCODE='" + combo_compcode1.Text + "' AND  D.USERNAME='" + comboUser.Text + "'  OR A.HOSTELNAME LIKE'%" + txtmachinesearch.Text + "%'  OR C.MACIP LIKE'%" + txtmachinesearch.Text + "%'  OR A.ACTIVE LIKE'%" + txtmachinesearch.Text + "%' ORDER BY 4";
                    DataSet ds = Utility.ExecuteSelectQuery(sel1, "ASPTBLMACHINEMAS");
                    dt = ds.Tables["ASPTBLMACHINEMAS"];
                }
                if (dt.Rows.Count > 0)
                {

                    foreach (DataRow myRow in dt.Rows)
                    {
                        ListViewItem list = new ListViewItem();
                        list.SubItems.Add(myRow["ASPTBLMACHINEMASID"].ToString());
                        list.SubItems.Add(myRow["COMPCODE"].ToString());
                        list.SubItems.Add(myRow["WARDENNAME"].ToString());
                        list.SubItems.Add(myRow["IPADDRESS"].ToString());
                        list.SubItems.Add(myRow["HOSTELNAME"].ToString());
                        list.SubItems.Add(myRow["MTYPE2"].ToString());
                        list.SubItems.Add(myRow["SESSIONTIME"].ToString());
                        list.SubItems.Add(myRow["ACTIVE"].ToString());
                       // this.listfilter.Items.Add((ListViewItem)list.Clone());
                        listmachine.Items.Add(list);
                    }
                    lbltotal.Text = "Total Count    :" + listmachine.Items.Count;
                }
                else
                {
                    listmachine.Items.Clear();
                           GridLoad();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Exit()
        {
            GlobalVariables.MdiPanel.Show();
            this.Hide();
            News();
            GlobalVariables.HeaderName.Text = "";
            GlobalVariables.TabCtrl.TabPages.RemoveAt(GlobalVariables.TabCtrl.SelectedIndex);

        }

        public void Deletes()
        {

            string sel1 = "DELETE  FROM ASPTBLMACHINEMAS WHERE ASPTBLMACHINEMASID=" + txtmachineid.Text;
            Utility.ExecuteNonQuery(sel1); GridLoad();MessageBox.Show("Record Deleted"); News();
        }

        public void Prints()
        {
           
        }

        public void Searchs()
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

        private void butok_Click(object sender, EventArgs e)
        {
          
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridLoad();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {
            Class.Users.UserTime = 0;
        }

        private void combowardenname_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ipload(Convert.ToInt64("0" + combo_compcode.SelectedValue), Convert.ToInt64("0" + comboUser.SelectedValue));
            Class.Users.UserTime = 0;
            if (Convert.ToInt64("0" + combo_compcode.SelectedValue) > 0 && Convert.ToInt64("0" + combowardenname.SelectedValue) > 0)
            {
                string sel = "select  b.sessiontime from  gtcompmast  a join asptblusermas b on  a.gtcompmastid=b.compcode  where  a.compcode='" + combo_compcode.Text + "' and b.username='" + combowardenname.Text + "'";
                DataSet ds = Utility.ExecuteSelectQuery(sel, "gtcompmast");
                DataTable dt = ds.Tables["gtcompmast"];
                if (dt.Rows.Count > 0)
                {
                    txtsessiontime.Text = dt.Rows[0]["sessiontime"].ToString();
                }
                else
                {
                    txtsessiontime.Text = "";
                }
                ipload(Convert.ToInt64("0" + combo_compcode.SelectedValue), Convert.ToInt64("0" + combowardenname.SelectedValue));
            }
        }

        private void sessionEnableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtsessiontime.ReadOnly = false;
        }

        private void comboipaddress_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public void Searchs(int id)
        {
            throw new NotImplementedException();
        }

        public void Deletes(int id)
        {
            throw new NotImplementedException();
        }

        private void comboMTYPE2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
