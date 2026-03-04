using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using Oracle.ManagedDataAccess.Client;
using System.Data.OleDb;

namespace Pinnacle.Master
{
    public partial class EmployeeMaster : Form,ToolStripAccess
    {
        private static EmployeeMaster _instance;
        Models.Master mas = new Models.Master();
        Models.Employee em = new Models.Employee();
        Models.UserRights sm = new Models.UserRights();
        byte[] bytes; byte[] Signbytes; string PATTH1 = "";
        OpenFileDialog open = new OpenFileDialog(); Int64 myString = 0; int i = 0;
        Utility uti = new Utility(); ListView listfilter = new ListView();
        bool datagridviewfalse = false;
        public static EmployeeMaster Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new EmployeeMaster(); 
                GlobalVariables.CurrentForm = _instance; return _instance;
            }
        }
        public void ReadOnlys()
        {

        }
        public EmployeeMaster()
        {
            InitializeComponent();

            Class.Users.IPADDRESS = GenFun.GetLocalIPAddress();
            Class.Users.CREATED = Convert.ToDateTime(System.DateTime.Now.ToString("dd-MMM-yyyy") + " " + System.DateTime.Now.ToLongTimeString());
            Class.Users.SysDate = Convert.ToString(System.DateTime.Now.ToString("dd/MM/yyyy"));
            Class.Users.SysTime = Convert.ToString(DateTime.Now.ToLongTimeString().ToString());
            GlobalVariables.HeaderName.Text = Class.Users.ScreenName; 
            usercheck(Class.Users.HCompcode, Class.Users.HUserName, Class.Users.ScreenName);
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
                        if (dt1.Rows[r]["TREEBUTTON"].ToString() == "T") {  empcompcode.Enabled = true; comboidcardno.Enabled = true; txtmidcard.Enabled = true; PictureBox1.Enabled = true; } else {  empcompcode.Enabled = false; comboidcardno.Enabled = false; txtmidcard.Enabled = false; PictureBox1.Enabled = false; }
                    }
                }
            }
            else
            {
                MessageBox.Show("Invalid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void EmployeeMaster_Load(object sender, EventArgs e)
        {


            //  string sel = "SELECT DISTINCT  C.GTCOMPMASTID,C.COMPCODE,c.COMPNAME FROM    HREMPLOYMAST    A   JOIN HREMPLOYDETAILS B ON A.HREMPLOYMASTID=B.HREMPLOYMASTID AND B.HOSTEL='YES' AND A.IDCARDNO=B.IDCARD JOIN   GTCOMPMAST C ON C.GTCOMPMASTID = A.COMPCODE      JOIN GTDEPTDESGMAST  E ON E.GTDEPTDESGMASTID =B.DEPTNAME     JOIN HOSTELLIVEDATA F ON F.COMPCODE=C.COMPCODE AND F.IDCARDNO=A.IDCARDNO AND F.EMPLOYEENAME=A.FNAME order by 2";
            string sel = "SELECT DISTINCT  C.GTCOMPMASTID,C.COMPCODE,c.COMPNAME FROM    HREMPLOYMAST    A   JOIN HREMPLOYDETAILS B ON A.HREMPLOYMASTID=B.HREMPLOYMASTID  AND A.IDCARDNO=B.IDCARD JOIN   GTCOMPMAST C ON C.GTCOMPMASTID = A.COMPCODE      JOIN GTDEPTDESGMAST  E ON E.GTDEPTDESGMASTID =B.DEPTNAME    order by 2";
            DataSet ds = Utility.ExecuteSelectQuery(sel, "hremploymast");
            DataTable dt1 = ds.Tables["hremploymast"];
            if (dt1.Rows.Count > 0)
            {

                empcompcode.DisplayMember = "COMPCODE";
                empcompcode.ValueMember = "GTCOMPMASTID";
                empcompcode.DataSource = dt1;

                empcompname.DisplayMember = "COMPNAME";
                empcompname.ValueMember = "gtcompmastid";
                empcompname.DataSource = dt1;

                combocompcode.DisplayMember = "COMPCODE";
                combocompcode.ValueMember = "gtcompmastid";
                combocompcode.DataSource = dt1;

            }
            else
            {
                empcompcode.DataSource = null;
            }

            //DataTable dt2 = mas.dept();
            //if (dt2.Rows.Count > 0)
            //{
            //    combodept.DisplayMember = "DEPARTMENT";
            //    combodept.ValueMember = "gtdeptdesgmastid";
            //    combodept.DataSource = dt2;

            //}
            //combodept.SelectedIndex = -1;
            GridLoad(); empty();
            GlobalVariables.HeaderName.Text = Class.Users.ScreenName;

        }

        bool ch = false; int rowcount = 0;
        private bool Checks()
        {

            if (PictureBox1.Image == null)
            {
                MessageBox.Show("Image is empty");
                this.PictureBox1.Focus();
                this.PictureBox1.Select(); return false;
            }

            if (empcompcode.Text == "")
            {
                MessageBox.Show("CompCode is empty");
                this.Focus();
                this.empcompcode.Select(); return false;
            }
            if (comboidcardno.Text == "")
            {
                MessageBox.Show("Emp Name is empty");
                this.Focus();
                this.comboidcardno.Select(); return false;

            }
           
            ch = true;
            return ch;

        }

        private bool Checks1()
        {
            int CNT = dataGridView1.Rows.Count;
            if (CNT > 0)
            {
                i = 0; string s = "";
                for (i = 0; i < CNT; i++)
                {
                  s = dataGridView1.Rows[i].Cells[0].Value.ToString();
                    Image img = Image.FromFile(@s.ToString());
                    PictureBox1.Image = img;
                    empcompcode.Text = Convert.ToString(dataGridView1.Rows[i].Cells[1].Value);
                    comboidcardno.Text = Convert.ToString(dataGridView1.Rows[i].Cells[2].Value);
                    if (empcompcode.Text == "")
                    {
                        MessageBox.Show("CompCode is empty");
                        this.empcompcode.Select(); empty(); return false;

                    }

                    if (comboidcardno.Text == "")
                    {
                        MessageBox.Show("Emp Name is empty");
                        this.comboidcardno.Select(); empty(); return false;

                    }
                    if (PictureBox1 == null)
                    {
                        MessageBox.Show("UPLOAD IMAGE");
                        this.PictureBox1.Select(); return false;
                    }
                }
                
            }
            return true;
        }
        public void Saves()
        {

            try
            {
                string s = "";
                OracleCommand ascmd = new OracleCommand();
                int CNT = dataGridView1.Rows.Count;
               
                if (CNT > 0)
                {
                    if (Checks1() == true)
                    {


                        for (int i = 0; i < CNT; i++)
                        {

                            s = ""; 
                             s = dataGridView1.Rows[i].Cells[0].Value.ToString();
                            Image img = Image.FromFile(@s.ToString());
                           // Image img3 = Image.FromFile(@s.ToString());
                            empcompcode.Text = Convert.ToString(dataGridView1.Rows[i].Cells[1].Value);
                            comboidcardno.Text = Convert.ToString(dataGridView1.Rows[i].Cells[2].Value);
                            txtidcardno.Text = Convert.ToString(dataGridView1.Rows[i].Cells[2].Value);
                            PictureBox1.Image = img;
                            bytes = Models.Device.ImageToByteArray(PictureBox1);
                            //pictureBox3.Image = img3;
                           // Signbytes = Models.Device.ImageToByteArray(pictureBox3);
                            em.ASPTBLEMPID = Convert.ToInt64("0" + txtempid.Text);                          

                            if (Convert.ToInt64(comboempname.SelectedValue) > 0 && Convert.ToInt64(txtidcardno.Text) > 0 && comboempname.Text != "" && PictureBox1.Image != null)
                            {
                                
                                em.COMPCODE = Convert.ToInt64("0" + empcompcode.SelectedValue);
                                em.COMNAME = Convert.ToInt64("0" + empcompcode.SelectedValue);
                                em.EMPNAME = Convert.ToString(comboempname.Text);
                                em.EMPID = Convert.ToInt64(comboempname.SelectedValue);
                                if (txtlastname.Text == "") { em.LASTNAME = Convert.ToString(comboempname.Text.ToUpper()); }
                                else
                                {
                                    em.LASTNAME = Convert.ToString(txtlastname.Text.ToUpper());
                                }
                                em.ADDRESS = Convert.ToString(txtaddress.Text);
                                if (radiomale.Checked == true) { em.GENDER = "T"; } else { radiofemale.Checked = false; em.GENDER = "F"; }
                                em.EMPLOYEETYPE = Convert.ToString(comboEmpType.Text);
                                em.DATEOFBIRTH = Convert.ToString(txtdateofbirth.Text);
                                em.DEPARTMENT = Convert.ToInt64("0" + combodept.SelectedValue);
                                em.DATEOFJOIN = Convert.ToString(txtdateofjoin.Text);
                                em.IDCARDNO = Convert.ToInt64("0" + txtidcardno.Text);
                                em.CONTACT = Convert.ToString(txtcontactno.Text);
                                em.BLOODGROUP = Convert.ToString(combobroup.Text);
                                if (checkactive.Checked == true) em.ACTIVE = "T"; else em.ACTIVE = "F";
                                em.USERNAME = Convert.ToInt64("0" + Class.Users.USERID);
                                em.IPADDRESS = Class.Users.IPADDRESS;
                                em.CREATEDON = Convert.ToString(Class.Users.CREATED);
                                em.MODIFIEDON = Convert.ToString(Class.Users.CREATED);
                                em.bytes = bytes;
                                em.image = Convert.ToInt64("0" + bytes.Length);
                                //  DataTable dt = em.select(em.COMPCODE, em.EMPID, em.LASTNAME, em.GENDER, em.DATEOFBIRTH, em.ADDRESS, em.DEPARTMENT, em.DATEOFJOIN, em.IDCARDNO, em.ACTIVE, em.image);
                                DataTable dt = em.select(em.COMPCODE, em.EMPID, em.IDCARDNO, em.ACTIVE, em.image);
                                if (dt.Rows.Count != 0)
                                {

                                }
                                else if (dt.Rows.Count != 0 && em.ASPTBLEMPID == 0 || em.ASPTBLEMPID == 0)
                                {
                                    string ins = "INSERT INTO ASPTBLEMP(COMPCODE,COMNAME,EMPNAME,EMPID,LASTNAME,GENDER,DATEOFBIRTH,ADDRESS,DEPARTMENT,DATEOFJOIN,IDCARDNO,ACTIVE,USERNAME,IPADDRESS,CREATEDBY," +
                                        "CREATEDON,MODIFIEDON,EMPIMAGE,IMAGEBYTES)VALUES(" + em.COMPCODE + "," + em.COMNAME + ",'" + em.EMPNAME + "','" + em.EMPID + "','" + em.LASTNAME + "','" + em.GENDER + "','" + em.DATEOFBIRTH + "','" + em.ADDRESS + "'," + em.DEPARTMENT + ",'" + em.DATEOFJOIN + "'," + em.IDCARDNO + ",'" + em.ACTIVE + "'," + em.USERNAME + ",'" + em.IPADDRESS + "','" + em.CREATEDON + "','" + em.CREATEDON + "','" + em.MODIFIEDON + "',:EMPIMAGE,'" + em.image + "')";
                                    Utility.ExecuteNonQuery(ins, bytes);

                                }
                                else
                                {

                                    string query = "UPDATE   ASPTBLEMP SET COMPCODE=" + em.COMPCODE + ",COMNAME=" + em.COMNAME + ",EMPNAME='" + em.EMPNAME + "',EMPID='" + em.EMPID + "',LASTNAME='" + em.LASTNAME + "' ,GENDER='" + em.GENDER + "',DATEOFBIRTH='" + em.DATEOFBIRTH + "',ADDRESS='" + em.ADDRESS + "',DEPARTMENT=" + em.DEPARTMENT + ",DATEOFJOIN='" + em.DATEOFJOIN + "' ,IDCARDNO=" + em.IDCARDNO + ",ACTIVE='" + em.ACTIVE + "' ,USERNAME=" + em.USERNAME + ",IPADDRESS='" + em.IPADDRESS + "',CREATEDBY='" + em.IDCARDNO + "',MODIFIEDON='" + em.MODIFIEDON + "',EMPIMAGE=:EMPIMAGE,IMAGEBYTES='" + em.image + "' WHERE ASPTBLEMPID=" + em.ASPTBLEMPID;
                                    Utility.ExecuteNonQuery(query, bytes);

                                }
                            }
                            else
                            {
                               
                                MessageBox.Show("CompCopde is Wrong.     " + Convert.ToString(dataGridView1.Rows[i].Cells[1].Value), "" + Convert.ToString(dataGridView1.Rows[i].Cells[2].Value) + "     Invalid");
                            }
                            //}
                            //else
                            //{
                            //    MessageBox.Show("Wrong IDCard  : " + comboidcardno.Text, "Invalid"); 
                            //}
                        }
                       
                            MessageBox.Show("Record Saved Saved Successfully", "Success"); GridLoad(); empty();
                       

                    }
                    else
                    {
                        MessageBox.Show("INVALID", "ERROR"); return;
                    }
                    return;
                }
                else
                {

                    if (Checks() == true)
                    {

                        if (Convert.ToInt64(comboempname.SelectedValue) > 0 && Convert.ToInt64(txtidcardno.Text) > 0 && PictureBox1.Image != null)
                        {
                            em.ASPTBLEMPID = Convert.ToInt64("0" + txtempid.Text);
                            em.COMPCODE = Convert.ToInt64("0" + empcompcode.SelectedValue);
                            em.COMNAME = Convert.ToInt64("0" + empcompcode.SelectedValue);
                            em.EMPNAME = Convert.ToString(comboempname.Text);
                            em.EMPID = Convert.ToInt64("0" + comboempname.SelectedValue.ToString());
                            em.LASTNAME = Convert.ToString(txtlastname.Text.ToUpper());
                            em.ADDRESS = Convert.ToString(txtaddress.Text);
                            if (radiomale.Checked == true) { em.GENDER = "T"; } else { radiofemale.Checked = false; em.GENDER = "F"; }
                            em.EMPLOYEETYPE = Convert.ToString(comboEmpType.Text);
                            em.DATEOFBIRTH = Convert.ToString(txtdateofbirth.Text);
                            em.DEPARTMENT = Convert.ToInt64("0" + combodept.SelectedValue);
                            em.DATEOFJOIN = Convert.ToString(txtdateofjoin.Text);
                            em.IDCARDNO = Convert.ToInt64("0" + comboidcardno.Text);
                            em.CONTACT = Convert.ToString(txtcontactno.Text);
                            em.BLOODGROUP = Convert.ToString(combobroup.Text);
                            if (checkactive.Checked) em.ACTIVE = "T"; else em.ACTIVE = "F";
                            em.USERNAME = Convert.ToInt64("0" + Class.Users.USERID);
                            em.IPADDRESS = Class.Users.IPADDRESS;
                            em.CREATEDON = Convert.ToString(Class.Users.CREATED);
                            em.MODIFIEDON = Convert.ToString(Class.Users.CREATED);

                            em.bytes = bytes;
                            em.image = Convert.ToInt64("0" + bytes.Length);
                            DataTable dt = em.select(em.COMPCODE, em.EMPID, em.LASTNAME, em.GENDER, em.DATEOFBIRTH, em.ADDRESS, em.DEPARTMENT, em.DATEOFJOIN, em.IDCARDNO, em.ACTIVE, em.image,em.EMPLOYEETYPE);
                            if (dt.Rows.Count != 0)
                            {
                                MessageBox.Show("Child Record Found", "Exception"); empty();
                            }
                            else if (dt.Rows.Count != 0 && em.ASPTBLEMPID == 0 || em.ASPTBLEMPID == 0)
                            {
                                string ins = "INSERT INTO ASPTBLEMP(COMPCODE,COMNAME,EMPNAME,EMPID,LASTNAME,GENDER,DATEOFBIRTH,ADDRESS,DEPARTMENT,DATEOFJOIN,IDCARDNO,ACTIVE,USERNAME,IPADDRESS,CREATEDBY," +
                                    "CREATEDON,MODIFIEDON,EMPIMAGE,IMAGEBYTES,EMPLOYEETYPE)VALUES(" + em.COMPCODE + "," + em.COMNAME + ",'" + em.EMPNAME + "','" + em.EMPID + "','" + em.LASTNAME + "','" + em.GENDER + "','" + em.DATEOFBIRTH + "','" + em.ADDRESS + "'," + em.DEPARTMENT + ",'" + em.DATEOFJOIN + "'," + em.IDCARDNO + ",'" + em.ACTIVE + "'," + em.USERNAME + ",'" + em.IPADDRESS + "','" + em.CREATEDON + "','" + em.CREATEDON + "','" + em.MODIFIEDON + "',:EMPIMAGE,'" + em.image + "','"+em.EMPLOYEETYPE+"')";
                                Utility.ExecuteNonQuery(ins, bytes);
                                MessageBox.Show("Record Saved Saved Successfully", "Success"); GridLoad(); empty();
                            }
                            else
                            {

                                string query = "UPDATE   ASPTBLEMP SET COMPCODE=" + em.COMPCODE + ",COMNAME=" + em.COMNAME + ",EMPNAME='" + em.EMPNAME + "',EMPID='" + em.EMPID + "',LASTNAME='" + em.LASTNAME + "' ,GENDER='" + em.GENDER + "',DATEOFBIRTH='" + em.DATEOFBIRTH + "',ADDRESS='" + em.ADDRESS + "', DEPARTMENT=" + em.DEPARTMENT + ",DATEOFJOIN='" + em.DATEOFJOIN + "' ,IDCARDNO=" + em.IDCARDNO + ",ACTIVE='" + em.ACTIVE + "' ,USERNAME=" + em.USERNAME + ",IPADDRESS='" + em.IPADDRESS + "',CREATEDBY='" + em.IDCARDNO + "',MODIFIEDON='" + em.MODIFIEDON + "',EMPIMAGE=:EMPIMAGE,IMAGEBYTES='" + em.image + "', EMPLOYEETYPE='" + em.EMPLOYEETYPE + "' WHERE ASPTBLEMPID=" + em.ASPTBLEMPID;
                                Utility.ExecuteNonQuery(query, bytes);
                                MessageBox.Show("Record Updated  Successfully", "Success"); GridLoad(); empty();
                            }

                        }
                    }
                    else
                    {
                        MessageBox.Show("CompCopde is Wrong.     " + Convert.ToString(comboidcardno.Text), "" + Convert.ToString(combocompcode.Text) + "     Invalid");
                    }
                }
                

            }
            catch (Exception EX)
            {
                MessageBox.Show("UPLOAD IMAGE");
            }
        }

        private void EmployeeMaster_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }

        public void News()
        {
            empty(); empcompcode.Enabled = true;
            comboidcardno.Enabled = true;
            txtmidcard.Enabled = true;
            this.PictureBox1.Enabled = true; tabControl1.SelectTab(tabPage3);
        }


        private void PictureBox1_Click(object sender, EventArgs e)
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
                        System.Text.Encoding enc = System.Text.Encoding.ASCII;
                        myString = Convert.ToInt64("0" + bytes.Length);

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        private void Empcompcode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (empcompcode.SelectedValue != null)
                {

                    if (empcompcode.Text != "")
                    {
                        string selemp = "SELECT B.MIDCARD,A.HREMPLOYMASTID,CONCAT(a.fname ,concat('-',B.MIDCARD) ) as EMPNAME,E.GTDEPTDESGMASTID,E.MNNAME1 FROM    HREMPLOYMAST    A   JOIN HREMPLOYDETAILS B ON A.HREMPLOYMASTID=B.HREMPLOYMASTID  AND A.IDCARDNO=B.IDCARD  JOIN   GTCOMPMAST C ON C.GTCOMPMASTID = A.COMPCODE      JOIN GTDEPTDESGMAST  E ON E.GTDEPTDESGMASTID =B.DEPTNAME    WHERE C.COMPCODE='" + empcompcode.Text + "'   AND B.IDACTIVE='YES'  ORDER BY 1";// 
                        DataSet dsemp = Utility.ExecuteSelectQuery(selemp, "hremploymast");
                        DataTable dtemp = dsemp.Tables["hremploymast"];
                        if (dtemp.Rows.Count > 0)
                        {
                            comboidcardno.DisplayMember = "MIDCARD";
                            comboidcardno.ValueMember = "hremploymastid";
                            comboidcardno.DataSource = dtemp;
                            comboidcardno.Text = ""; comboidcardno.SelectedIndex = -1;
                            comboempname.DisplayMember = "empname";
                            comboempname.ValueMember = "hremploymastid";
                            comboempname.DataSource = dtemp;
                            comboempname.Text = ""; comboempname.SelectedIndex = -1;

                            combodept.DisplayMember = "DISPNAME";
                            combodept.ValueMember = "GTDEPTDESGMASTID";
                            combodept.DataSource = dtemp;
                            combodept.Text = ""; combodept.SelectedIndex = -1;
                            txtidcardno.Text = "";

                            txtidcardno.Text = "";
                            txtlastname.Text = "";
                            txtdateofbirth.Text = "";
                            txtdateofjoin.Text = "";
                            radiofemale.Checked = false; radiomale.Checked = false;
                            string selemp1 = "select COUNT(A.hremploymastid) AS cnt from  HREMPLOYMAST    A   JOIN HREMPLOYDETAILS B ON A.HREMPLOYMASTID=B.HREMPLOYMASTID  AND A.IDCARDNO=B.IDCARD  JOIN   GTCOMPMAST C ON C.GTCOMPMASTID = A.COMPCODE      JOIN GTDEPTDESGMAST  E ON E.GTDEPTDESGMASTID =B.DEPTNAME     WHERE C.COMPCODE='" + empcompcode.Text + "'  AND B.IDACTIVE='YES' ";
                            DataSet dsemp1 = Utility.ExecuteSelectQuery(selemp1, "hremploymast");
                            DataTable dtemp1 = dsemp1.Tables["hremploymast"];
                            lbltotidcardno.Text = "";
                            lbltotidcardno.Text = "Employee Count :" + dtemp1.Rows[0]["cnt"].ToString();
                        }
                        else
                        {
                            lbltotidcardno.Text = " Employee Count : 0";
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }



        private void Lvlogs_ItemActivate(object sender, EventArgs e)
        {
      
            try
            {
               // Class.Users.UserTime = 0;
                if (lvlogsSSS.Items.Count > 0)
                {

                    PictureBox1.Image = null; bytes = null;
                    em.ASPTBLEMPID = Convert.ToInt64("0" + lvlogsSSS.SelectedItems[0].SubItems[1].Text);

                    string sel1 = "SELECT A.ASPTBLEMPID, B.COMPCODE ,B.COMPNAME,A.EMPNAME,A.LASTNAME ,A.ADDRESS,A.GENDER,A.DATEOFBIRTH,A.IDCARDNO,C.MNNAME1 AS DEPARTMENT,A.DATEOFJOIN ,A.CONTACT,A.BLOODGROUP,A.ACTIVE,A.EMPIMAGE,A.EMPLOYEETYPE FROM  ASPTBLEMP A JOIN   GTCOMPMAST B ON B.gtcompmastid = A.COMPCODE JOIN gtdeptdesgmast C ON C.gtdeptdesgmastid = A.DEPARTMENT WHERE A.ASPTBLEMPID=" + em.ASPTBLEMPID;
                    DataSet ds = Utility.ExecuteSelectQuery(sel1, "ASPTBLEMP");
                    DataTable dt = ds.Tables["ASPTBLEMP"];
                    foreach (DataRow myRow in dt.Rows)
                    {
                        txtempid.Text = Convert.ToString(myRow["ASPTBLEMPID"].ToString());
                        empcompcode.Text = Convert.ToString(myRow["COMPCODE"].ToString());
                        empcompname.Text = Convert.ToString(myRow["COMPNAME"].ToString());
                        comboempname.Text = Convert.ToString(myRow["EMPNAME"].ToString());
                        txtlastname.Text = Convert.ToString(myRow["LASTNAME"].ToString());
                        txtaddress.Text = Convert.ToString(myRow["ADDRESS"].ToString());
                        if (myRow["GENDER"].ToString() == "T") radiomale.Checked = true;
                        else radiofemale.Checked = true;
                        txtdateofbirth.Text = Convert.ToString(myRow["DATEOFBIRTH"].ToString());
                        combodept.Text = Convert.ToString(myRow["DEPARTMENT"].ToString());
                        txtdateofjoin.Text = Convert.ToString(myRow["DATEOFJOIN"].ToString());
                        comboidcardno.Text= Convert.ToString(myRow["IDCARDNO"].ToString());
                        txtidcardno.Text = Convert.ToString(myRow["IDCARDNO"].ToString());
                        txtcontactno.Text = Convert.ToString(myRow["CONTACT"].ToString());
                        txtmidcard.Text = Convert.ToString(myRow["IDCARDNO"].ToString());
                        combobroup.Text = Convert.ToString(myRow["BLOODGROUP"].ToString());
                        if (myRow["ACTIVE"].ToString() == "T") checkactive.Checked = true; else checkactive.Checked = false;
                        if (myRow["EMPIMAGE"].ToString() != "")
                        {
                            bytes = (byte[])myRow["EMPIMAGE"];
                            Image img = Models.Device.ByteArrayToImage(bytes);
                            PictureBox1.Image = img;
                        }
                        empcompcode.Enabled = false;
                        comboidcardno.Enabled = false;
                        txtmidcard.Enabled = false;



                        comboEmpType.Text = Convert.ToString(myRow["EMPLOYEETYPE"].ToString());

                        tabControl1.SelectTab(tabPage1);

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
          
        }
        private void butview_Click(object sender, EventArgs e)
        {

            try
            {
                //Class.Users.UserTime = 0;
                lvlogsSSS.Items.Clear(); int r = 1; listfilter.Items.Clear();
                string sel1 = "SELECT A.ASPTBLEMPID,B.COMPCODE,A.EMPNAME,A.IDCARDNO ,A.GENDER,C.MNNAME1 AS DEPARTMENT,A.CONTACT,A.BLOODGROUP,A.EMPLOYEETYPE,A.ACTIVE   FROM  ASPTBLEMP A   JOIN   GTCOMPMAST B ON B.gtcompmastid = A.COMPCODE JOIN gtdeptdesgmast C ON C.gtdeptdesgmastid = A.DEPARTMENT where B.COMPCODE='" + combocompcode.Text + "'  order by 1";
                DataSet ds = Utility.ExecuteSelectQuery(sel1, "ASPTBLEMP");
                DataTable dt = ds.Tables["ASPTBLEMP"];
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow myRow in dt.Rows)
                    {
                        ListViewItem list = new ListViewItem();
                        list.Text = r.ToString();
                        list.SubItems.Add(myRow["ASPTBLEMPID"].ToString());
                        list.SubItems.Add(myRow["COMPCODE"].ToString());
                        list.SubItems.Add(myRow["EMPNAME"].ToString());
                        list.SubItems.Add(myRow["IDCARDNO"].ToString());
                        if (myRow["GENDER"].ToString() == "T") list.SubItems.Add("T"); else list.SubItems.Add("F");

                        list.SubItems.Add(myRow["DEPARTMENT"].ToString());

                        list.SubItems.Add(myRow["CONTACT"].ToString());
                        list.SubItems.Add(myRow["BLOODGROUP"].ToString());
                        list.SubItems.Add(myRow["EMPLOYEETYPE"].ToString());

                        if (myRow["ACTIVE"].ToString() == "T") list.SubItems.Add("T"); else list.SubItems.Add("F");
                        if (r % 2 == 0)
                        {
                            list.BackColor = Color.White;
                        }
                        else
                        {
                            list.BackColor = Color.WhiteSmoke;
                        }
                        r++;
                        this.listfilter.Items.Add((ListViewItem)list.Clone());
                        lvlogsSSS.Items.Add(list);
                    }

                    lblemptot.Text = "Total Rows    :" + lvlogsSSS.Items.Count;
                }
                else
                {
                    lblemptot.Text = "Total Rows  : 0";
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }

        }
        public void GridLoad()
        {
            try
            {
                lvlogsSSS.Items.Clear(); listfilter.Items.Clear(); int r = 1;
                string sel1 = "SELECT A.ASPTBLEMPID,B.COMPCODE,A.EMPNAME,A.IDCARDNO ,A.GENDER,C.MNAME AS DEPARTMENT,A.CONTACT,A.BLOODGROUP,A.EMPLOYEETYPE,A.ACTIVE   FROM  ASPTBLEMP A   JOIN   GTCOMPMAST B ON B.gtcompmastid = A.COMPCODE JOIN gtdeptdesgmast C ON C.gtdeptdesgmastid = A.DEPARTMENT   order by 1";
                DataSet ds = Utility.ExecuteSelectQuery(sel1, "ASPTBLEMP");
                DataTable dt = ds.Tables["ASPTBLEMP"];
                if(dt==null || dt.Rows.Count < 0)
                {
                    return;
                }
               
                    foreach (DataRow myRow in dt.Rows)
                {
                    ListViewItem list = new ListViewItem();
                    list.Text = r.ToString();
                    list.SubItems.Add(myRow["ASPTBLEMPID"].ToString());
                    list.SubItems.Add(myRow["COMPCODE"].ToString());
                    list.SubItems.Add(myRow["EMPNAME"].ToString());
                    list.SubItems.Add(myRow["IDCARDNO"].ToString());
                    if (myRow["GENDER"].ToString() == "T") list.SubItems.Add("T"); else list.SubItems.Add("F");

                    list.SubItems.Add(myRow["DEPARTMENT"].ToString());

                    list.SubItems.Add(myRow["CONTACT"].ToString());
                    list.SubItems.Add(myRow["BLOODGROUP"].ToString());
                    list.SubItems.Add(myRow["EMPLOYEETYPE"].ToString());

                    if (myRow["ACTIVE"].ToString() == "T") list.SubItems.Add("T"); else list.SubItems.Add("F");
                    if (r % 2 == 0)
                    {
                        list.BackColor = Color.White;
                    }
                    else
                    {
                        list.BackColor = Color.WhiteSmoke;
                    }
                    r++;
                    this.listfilter.Items.Add((ListViewItem)list.Clone());
                    lvlogsSSS.Items.Add(list);


                }
                lblemptot.Text = "Total Rows    :" + lvlogsSSS.Items.Count;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void empty()
        {
            txtempid.Text = "";
            txtempid.Text = "";
            empcompcode.Text = ""; empcompcode.SelectedIndex = -1; empcompname.Text = ""; empcompname.SelectedIndex = -1;
            empcompname.Text = ""; comboempname.Text = ""; comboempname.SelectedIndex = -1;
            comboidcardno.Text = ""; comboidcardno.SelectedIndex = -1;
            txtlastname.Text = "";
            txtaddress.Text = "";
            combocompcode.Text = "";
            radiomale.Checked = true;
            txtdateofbirth.Text = "";
            combodept.Text = "";
            txtdateofjoin.Text = ""; pictureBox2.Image = null; lblmidcard.Text = ""; lblempid.Text = "";
            txtidcardno.Text = "";
            txtcontactno.Text = ""; //listfilter.Items.Clear();
            progressBar1.Minimum = 0; lblprogress1.Text = ""; progressBar1.Value = 0;
            checkactive.Checked = true; txtmidcard.Text = "";
            bytes = null;
            PictureBox1.Image = null;
            do
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++) { try { dataGridView1.Rows.RemoveAt(i); } catch (Exception) { } }
            }
            while (dataGridView1.Rows.Count > 0);
            dataGridView1.Columns.Clear();
        }

        private void Txtsearch_TextChanged(object sender, EventArgs e)
        {

            try
            {
                int item0 = 0;
                if (txtsearch.Text.Length > 0)
                {
                    lvlogsSSS.Items.Clear();

                    foreach (ListViewItem item in listfilter.Items)
                    {
                        ListViewItem list = new ListViewItem();
                        if (listfilter.Items[item0].SubItems[2].ToString().Contains(txtsearch.Text) || listfilter.Items[item0].SubItems[3].ToString().Contains(txtsearch.Text))
                        {

                            list.Text = listfilter.Items[item0].SubItems[0].Text;
                            list.SubItems.Add(listfilter.Items[item0].SubItems[1].Text);
                            list.SubItems.Add(listfilter.Items[item0].SubItems[2].Text);
                            list.SubItems.Add(listfilter.Items[item0].SubItems[3].Text);
                            list.SubItems.Add(listfilter.Items[item0].SubItems[4].Text);
                            list.SubItems.Add(listfilter.Items[item0].SubItems[5].Text);
                            list.SubItems.Add(listfilter.Items[item0].SubItems[6].Text);
                            list.SubItems.Add(listfilter.Items[item0].SubItems[7].Text);
                            list.SubItems.Add(listfilter.Items[item0].SubItems[8].Text);
                            list.SubItems.Add(listfilter.Items[item0].SubItems[9].Text);


                            if (item0 % 2 == 0)
                            {
                                list.BackColor = Color.White;

                            }
                            else
                            {
                                list.BackColor = Color.WhiteSmoke;

                            }



                            lvlogsSSS.Items.Add(list);
                        }

                        item0++;
                    }
                    lblemptot.Text = "Total Rows    :" + lvlogsSSS.Items.Count;
                }
                else
                {
                    try
                    {
                        lvlogsSSS.Items.Clear();
                        foreach (ListViewItem item in listfilter.Items)
                        {
                            this.lvlogsSSS.Items.Add((ListViewItem)item.Clone());
                            item0++;
                        }
                        lblemptot.Text = "Total Rows    :" + lvlogsSSS.Items.Count;
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                // MessageBox.Show("---" + ex.ToString());
            }

            //try
            //{
            //    if (txtsearch.Text != null)
            //    {
            //        lvlogs.Items.Clear();


            //        string sel1 = "SELECT A.ASPTBLEMPID,B.COMPCODE,A.EMPNAME,A.IDCARDNO ,A.GENDER,A.DEPARTMENT,A.CONTACT,A.BLOODGROUP,A.EMPLOYEETYPE,A.ACTIVE  FROM ASPTBLEMP A JOIN GTCOMPMAST B ON A.COMPCODE=B.gtcompmastid JOIN gtdeptdesgmast C ON C.gtdeptdesgmastid = A.DEPARTMENT   WHERE A.EMPNAME LIKE'%" + txtsearch.Text + "%' OR A.IDCARDNO LIKE'%" + txtsearch.Text + "%' OR  A.GENDER LIKE'%" + txtsearch.Text + "%' OR  A.DEPARTMENT LIKE'%" + txtsearch.Text + "%' OR A.BLOODGROUP LIKE'%" + txtsearch.Text + "%'  OR  A.ACTIVE LIKE'%" + txtsearch.Text + "%'  ORDER BY 1 ";
            //        DataSet ds = Utility.ExecuteSelectQuery(sel1, "ASPTBLEMP");
            //        DataTable dt = ds.Tables["ASPTBLEMP"];
            //        if (dt.Rows.Count > 0)
            //        {
            //            foreach (DataRow myRow in dt.Rows)
            //            {
            //                ListViewItem list = new ListViewItem();
            //                list.SubItems.Add(myRow["ASPTBLEMPID"].ToString());
            //                list.SubItems.Add(myRow["COMPCODE"].ToString());                          
            //                list.SubItems.Add(myRow["EMPNAME"].ToString());
            //                list.SubItems.Add(myRow["IDCARDNO"].ToString());                          
            //                if (myRow["GENDER"].ToString() == "T") list.SubItems.Add("T"); else list.SubItems.Add("F");                         
            //                list.SubItems.Add(myRow["DEPARTMENT"].ToString());                      
            //                list.SubItems.Add(myRow["CONTACT"].ToString());
            //                list.SubItems.Add(myRow["BLOODGROUP"].ToString());
            //                list.SubItems.Add(myRow["EMPLOYEETYPE"].ToString());
            //                if (myRow["ACTIVE"].ToString() == "T") list.SubItems.Add("T"); else list.SubItems.Add("F");

            //                lvlogs.Items.Add(list);


            //            }
            //            lblemptot.Text = "Total Rows    :" + lvlogs.Items.Count;
            //        }
            //        else
            //        {
            //            MessageBox.Show("No Data Found");
            //            lblemptot.Text = "Total Rows Count:  " + lvlogs.Items.Count.ToString();
            //        }

            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}

        }

        private void Butimageclear_Click(object sender, EventArgs e)
        {
            PictureBox1.Image = null; bytes = null;
        }

        private void Txtcontactno_TextChanged(object sender, EventArgs e)
        {

        }

        private void Txtidcardno_TextChanged(object sender, EventArgs e)
        {

        }

        private void Combodept_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Exit_Click(object sender, EventArgs e)
        {
            this.Hide();

        }

        private void Combobroup_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void RefreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EmployeeMaster_Load(sender, e);
        }

        public void Deletes()
        {

            if (txtempid.Text != "")
            {
                string sel1 = "DELETE  FROM ASPTBLEMP A  WHERE A.ASPTBLEMPID=" + txtempid.Text;
                Utility.ExecuteNonQuery(sel1); GridLoad(); MessageBox.Show("Record Deleted");
                empty(); GridLoad();
            }
            else
            {
                if (datagridviewfalse == true)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    int j = 1;
                    progressBar1.Maximum = dataGridView1.Rows.Count;
                    progressBar1.Minimum = 0;
                    for (i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        row = dataGridView1.Rows[i];

                        if (Convert.ToBoolean(row.Cells[4].Value) == true)
                        {
                            dataGridView1.Rows.Remove(row);
                            i--;
                            j++;
                            decimal per = Convert.ToDecimal(100 / GenFun.ToDecimal(dataGridView1.Rows.Count)) * (j + 1);
                            lblprogress1.Text = j.ToString() + "    Downloading : " + (per).ToString("N0") + " %";
                           // label48.Text= dataGridView1.Rows[i].Cells[3].ToString();
                            lblprogress1.Refresh();
                            j = j + 1;
                            progressBar1.Value = Convert.ToInt32(j);
                        }
                    }
                   // int cnt = dataGridView1.Rows.Count - 1;
                    //label48.Text = "Total Count  :" + cnt.ToString();
                    //lblprogress1.Text = "Total Count  :" + cnt.ToString();
                    //progressBar1.Value = 0;
                }
                else { MessageBox.Show("Invalid Selection"); }
               
            }

        }

        private void txtmidcard_TextChanged(object sender, EventArgs e)
        {
            try
            {
             //   Class.Users.UserTime = 0;
                if (txtmidcard.Text.Length > 2  && empcompcode.Text != "")
                {

                    //string sel0 = "SELECT A.IDCARDNO FROM  ASPTBLEMP A JOIN   GTCOMPMAST B ON B.gtcompmastid = A.COMPCODE JOIN gtdeptdesgmast C ON C.gtdeptdesgmastid = A.DEPARTMENT WHERE A.IDCARDNO=" + comboidcardno.Text;
                    //DataSet ds0 = Utility.ExecuteSelectQuery(sel0, "ASPTBLEMP");
                    //DataTable dt0 = ds0.Tables["ASPTBLEMP"];
                    //if (dt0.Rows.Count > 0) { MessageBox.Show("This IDCard Already Saved"); empty(); return; }
                    //else
                    //{
                    string selemp = "SELECT B.MIDCARD,A.HREMPLOYMASTID,CONCAT(a.fname ,concat('-',B.MIDCARD) ) as EMPNAME,E.GTDEPTDESGMASTID,E.MNNAME1,A.FATHERNAME,A.DOB,B.DOJ,A.GENDER,F.CADD AS ADDRESS,F.MOBNO FROM    HREMPLOYMAST    A   JOIN HREMPLOYDETAILS B ON A.HREMPLOYMASTID=B.HREMPLOYMASTID AND B.IDACTIVE='YES' AND A.IDCARDNO=B.IDCARD  JOIN   GTCOMPMAST C ON C.GTCOMPMASTID = A.COMPCODE  JOIN GTDEPTDESGMAST  E ON E.GTDEPTDESGMASTID = B.DEPTNAME   JOIN HRECONTACTDETAILS F ON E.COMPCODE = A.COMPCODE AND E.COMPCODE = C.GTCOMPMASTID   AND F.HREMPLOYMASTID = A.HREMPLOYMASTID AND F.HREMPLOYMASTID = B.HREMPLOYMASTID     WHERE C.COMPCODE='" + empcompcode.Text + "' AND  B.MIDCARD='" + txtmidcard.Text + "'";

                    DataSet dsemp = Utility.ExecuteSelectQuery(selemp, "hremploymast");
                    DataTable dtemp = dsemp.Tables["hremploymast"];
                    if (dtemp.Rows.Count > 0)
                    {
                        comboidcardno.Text = dtemp.Rows[0]["MIDCARD"].ToString();

                        comboempname.Text = dtemp.Rows[0]["EMPNAME"].ToString();
                        comboempname.SelectedValue = dtemp.Rows[0]["HREMPLOYMASTID"].ToString();
                        combodept.Text = dtemp.Rows[0]["MNNAME1"].ToString();

                        txtlastname.Text = dtemp.Rows[0]["FATHERNAME"].ToString();
                        txtidcardno.Text = dtemp.Rows[0]["MIDCARD"].ToString();

                        txtdateofbirth.Text = dtemp.Rows[0]["DOB"].ToString();
                        txtdateofjoin.Text = dtemp.Rows[0]["DOJ"].ToString();
                        txtaddress.Text = dtemp.Rows[0]["ADDRESS"].ToString();
                        txtcontactno.Text = dtemp.Rows[0]["MOBNO"].ToString();
                        if (dtemp.Rows[0]["GENDER"].ToString() == "MALE")
                        {
                            radiomale.Checked = true;
                        }
                        else { radiofemale.Checked = true; }
                    }
                    else
                    {

                        //  empcompcode.Text = ""; empcompcode.SelectedIndex = -1; 
                        empcompname.Text = ""; empcompname.SelectedIndex = -1;
                        empcompname.Text = ""; comboempname.Text = ""; comboempname.SelectedIndex = -1;
                        comboidcardno.Text = ""; comboidcardno.SelectedIndex = -1;
                        txtlastname.Text = "";
                        txtaddress.Text = "";
                        combocompcode.Text = "";
                        radiomale.Checked = true;
                        txtdateofbirth.Text = "";
                        combodept.Text = "";
                        txtdateofjoin.Text = "";
                        txtidcardno.Text = "";
                        txtcontactno.Text = "";

                        checkactive.Checked = true;
                        bytes = null;
                        PictureBox1.Image = null;

                    }


                }
                if (txtempid.Text != "" && txtmidcard.Text.Length > 2)
                {
                    string sel1 = "SELECT A.ASPTBLEMPID, B.COMPCODE ,B.COMPNAME,A.EMPNAME,A.LASTNAME ,A.ADDRESS,A.GENDER,A.DATEOFBIRTH,A.IDCARDNO,C.MNNAME1 AS DEPARTMENT,A.DATEOFJOIN ,A.CONTACT,A.BLOODGROUP,A.ACTIVE,A.EMPIMAGE,A.EMPLOYEETYPE FROM  ASPTBLEMP A JOIN   GTCOMPMAST B ON B.gtcompmastid = A.COMPCODE JOIN gtdeptdesgmast C ON C.gtdeptdesgmastid = A.DEPARTMENT WHERE A.IDCARDNO=" + txtmidcard.Text;
                    DataSet ds = Utility.ExecuteSelectQuery(sel1, "ASPTBLEMP");
                    DataTable dt = ds.Tables["ASPTBLEMP"];
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow myRow in dt.Rows)
                        {
                            txtempid.Text = Convert.ToString(myRow["ASPTBLEMPID"].ToString());
                            empcompcode.Text = Convert.ToString(myRow["COMPCODE"].ToString());
                            empcompname.Text = Convert.ToString(myRow["COMPNAME"].ToString());
                            comboempname.Text = Convert.ToString(myRow["EMPNAME"].ToString());
                            txtlastname.Text = Convert.ToString(myRow["LASTNAME"].ToString());
                            txtaddress.Text = Convert.ToString(myRow["ADDRESS"].ToString());
                            if (myRow["GENDER"].ToString() == "T") radiomale.Checked = true;
                            else radiofemale.Checked = true;
                            txtdateofbirth.Text = Convert.ToString(myRow["DATEOFBIRTH"].ToString());
                            combodept.Text = Convert.ToString(myRow["DEPARTMENT"].ToString());
                            txtdateofjoin.Text = Convert.ToString(myRow["DATEOFJOIN"].ToString());
                            txtidcardno.Text = Convert.ToString(myRow["IDCARDNO"].ToString());
                            txtcontactno.Text = Convert.ToString(myRow["CONTACT"].ToString());
                            txtmidcard.Text = Convert.ToString(myRow["IDCARDNO"].ToString());
                            combobroup.Text = Convert.ToString(myRow["BLOODGROUP"].ToString());
                            if (myRow["ACTIVE"].ToString() == "T") checkactive.Checked = true; else checkactive.Checked = false;
                            if (myRow["EMPIMAGE"].ToString() != "")
                            {

                                bytes = (byte[])myRow["EMPIMAGE"];
                                Image img = Models.Device.ByteArrayToImage(bytes);
                                PictureBox1.Image = img;


                            }
                            else
                            {
                                PictureBox1.Image = null;
                            }
                        }
                    }
                    else
                    {
                        empcompcode.Text = ""; empcompcode.SelectedIndex = -1; empcompname.Text = ""; empcompname.SelectedIndex = -1;
                        empcompname.Text = ""; comboempname.Text = ""; comboempname.SelectedIndex = -1;
                        comboidcardno.Text = ""; comboidcardno.SelectedIndex = -1;
                        txtlastname.Text = "";
                        txtaddress.Text = "";
                        combocompcode.Text = "";
                        radiomale.Checked = true;
                        txtdateofbirth.Text = "";
                        combodept.Text = "";
                        txtdateofjoin.Text = "";
                        txtidcardno.Text = "";
                        txtcontactno.Text = "";

                        checkactive.Checked = true;
                        bytes = null;
                        PictureBox1.Image = null;

                    }
                }
                else
                {
                    //Empcompcode_SelectedIndexChanged(sender, e);
                    // MessageBox.Show("CompCode is Empty");empcompcode.Select();
                    PictureBox1.Image = null;
                }
                //}
            }
            catch (Exception ex) { }
        }

        private void comboidcardno_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                if (empcompcode.Text != null && comboidcardno.Text != "")
                {
                    //string sel1 = "SELECT A.IDCARDNO FROM  ASPTBLEMP A JOIN   GTCOMPMAST B ON B.gtcompmastid = A.COMPCODE JOIN gtdeptdesgmast C ON C.gtdeptdesgmastid = A.DEPARTMENT WHERE A.IDCARDNO=" + comboidcardno.Text;
                    //DataSet ds = Utility.ExecuteSelectQuery(sel1, "ASPTBLEMP");
                    //DataTable dt = ds.Tables["ASPTBLEMP"];
                    //if (dt.Rows.Count > 0) { MessageBox.Show("This IDCard Already Saved");empty();return; }
                    //else
                    //{

                    string selemp = "SELECT B.MIDCARD,A.HREMPLOYMASTID,CONCAT(a.fname ,concat('-',B.MIDCARD) ) as EMPNAME,E.GTDEPTDESGMASTID,E.MNNAME1,A.FATHERNAME,A.DOB,B.DOJ,A.GENDER,F.CADD AS ADDRESS,F.MOBNO FROM    HREMPLOYMAST    A   JOIN HREMPLOYDETAILS B ON A.HREMPLOYMASTID=B.HREMPLOYMASTID AND B.IDACTIVE='YES' AND A.IDCARDNO=B.IDCARD  JOIN   GTCOMPMAST C ON C.GTCOMPMASTID = A.COMPCODE  JOIN GTDEPTDESGMAST  E ON E.GTDEPTDESGMASTID = B.DEPTNAME   JOIN HRECONTACTDETAILS F ON E.COMPCODE = A.COMPCODE AND E.COMPCODE = C.GTCOMPMASTID   AND F.HREMPLOYMASTID = A.HREMPLOYMASTID AND F.HREMPLOYMASTID = B.HREMPLOYMASTID    WHERE C.COMPCODE='" + empcompcode.Text + "' AND  B.MIDCARD='" + comboidcardno.Text + "'";
                    DataSet dsemp = Utility.ExecuteSelectQuery(selemp, "hremploymast");
                    DataTable dtemp = dsemp.Tables["hremploymast"];

                    comboempname.DisplayMember = "EMPNAME";
                    comboempname.ValueMember = "HREMPLOYMASTID";
                    comboempname.DataSource = dtemp;


                    combodept.DisplayMember = "MNNAME1";
                    combodept.ValueMember = "GTDEPTDESGMASTID";
                    combodept.DataSource = dtemp;

                    txtlastname.Text = dtemp.Rows[0]["FATHERNAME"].ToString();
                    txtidcardno.Text = dtemp.Rows[0]["MIDCARD"].ToString();
                    txtdateofbirth.Text = dtemp.Rows[0]["DOB"].ToString();
                    txtdateofjoin.Text = dtemp.Rows[0]["DOJ"].ToString();
                    txtaddress.Text= dtemp.Rows[0]["ADDRESS"].ToString();
                    txtcontactno.Text= dtemp.Rows[0]["MOBNO"].ToString();
                    if (dtemp.Rows[0]["GENDER"].ToString() == "MALE")
                    {
                        radiomale.Checked = true;
                    }
                    else { radiofemale.Checked = true; }
                }
                //}

            }
            catch (Exception ex) { }
        }

        private void lblemptot_Click(object sender, EventArgs e)
        {

        }

        private void combocompcode_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void refreshToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            EmployeeMaster_Load(sender, e);
        }

        private void Searchs_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(tabPage2);
        }





        public void DownLoads()
        {
            try
            {
                if (dataGridView1.Rows.Count > 0)
                {
                    do
                    {
                        for (int i = 0; i < dataGridView1.Rows.Count; i++) { try { dataGridView1.Rows.RemoveAt(i); } catch (Exception) { } }
                    }
                    while (dataGridView1.Rows.Count > 0);
                    dataGridView1.Columns.Clear();
                }
                if (dataGridView1.Rows.Count <= 0)
                {
                    DataTable dt = new DataTable(); int i = 0; tabControl1.SelectTab(tabPage3);
                    dt.Rows.Clear(); progressBar1.Minimum = 0; progressBar1.Value = 0; lblprogress1.Text = "";


                    System.Data.OleDb.OleDbConnection OledbConn;
                    System.Data.OleDb.OleDbCommand OledbCmd;
                    System.Data.OleDb.OleDbDataAdapter OledbAdapter;
                    string filePath = string.Empty; string fileExt = string.Empty;
                    OpenFileDialog file = new OpenFileDialog(); string path = "";
                    if (file.ShowDialog() == System.Windows.Forms.DialogResult.OK) //if there is a file choosen by the user  
                    {
                        filePath = file.FileName; //get the path of the file  
                        fileExt = Path.GetExtension(filePath); //get the file extension  
                        if (fileExt.CompareTo(".xls") == 0)
                            path = @"provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties='Excel 8.0;HRD=No;IMEX=1';"; //for below excel 2007  
                        else
                            path = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties='Excel 12.0;HDR=Yes';"; //for above excel 2007  

                        OledbConn = new System.Data.OleDb.OleDbConnection(path);
                        string qry1 = "Select * from [Sheet1$]";
                        OledbAdapter = new OleDbDataAdapter(qry1, OledbConn);

                        OledbAdapter.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            dataGridView1.DataSource = dt;
                            Image img;
                            DataGridViewImageColumn imageCol = new DataGridViewImageColumn();                          
                            imageCol.HeaderText = "Photo";
                            imageCol.ImageLayout = DataGridViewImageCellLayout.Zoom;
                            dataGridView1.Columns.Add(imageCol);
                            dataGridView1.Columns[0].Width = 500;
                            DataGridViewCheckBoxColumn imageCol1 = new DataGridViewCheckBoxColumn();
                          
                        
                            imageCol1.HeaderText = "Delete";
                            dataGridView1.Columns.Add(imageCol1);
                            string s = "";

                            progressBar1.Maximum = dt.Rows.Count;
                            foreach (DataRow dr in dt.Rows)
                            {
                                if (dr[0].ToString() != "")
                                {
                                    s = "";
                                    s = @dr[0].ToString();
                                    img = Image.FromFile(s.ToString());

                                    dataGridView1.Rows[i].Cells[0].Value = s.ToString();
                                    dataGridView1.Rows[i].Cells[3].Value = img;                                    
                                    decimal per = Convert.ToDecimal(100 / GenFun.ToDecimal(dataGridView1.Rows.Count)) * (i + 1);
                                    lblprogress1.Text = i.ToString() + "    Downloading : " + (per).ToString("N0") + " %" + "    " + dt.Rows[i]["idcardno"].ToString();
                                    lblprogress1.Refresh();
                                    i = i + 1;
                                    progressBar1.Value = Convert.ToInt32(i);
                                }
                                else
                                {
                                    MessageBox.Show("null" + dr[0].ToString());
                                }

                            }
                        }
                    }
                   
                    int cnt = dataGridView1.Rows.Count - 1;
                    label48.Text = "Total Count  :" + cnt.ToString();
                }
            }
            catch (Exception ex)
            {

            }
        }
        //privatevoidDeleteRowFromGrid(int id)
        //{
        //    try
        //    {
        //        conx.Open(); //Open Connection  
        //        SqlCommandcmd = newSqlCommand("DELETE FROM Customers WHERE CustomerID = '" + id + "'", conx); //Here, we specify query with {Id} parameter which allow us to delete rows from Db.  
        //        int Result = cmd.ExecuteNonQuery(); // Execute Query for deleting all rows selected from DataGridView  
        //        conx.Close(); // Close Connection  
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //    finally
        //    {
        //        conx.Close();
        //    }
        //}
        //Delete Button  
        private void button1_Click(object sender, EventArgs e)
        {
           
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 4) { }
                else
                {
                    pictureBox2.Image = dataGridView1.Rows[e.RowIndex].Cells[3].Value as Image;
                    lblempid.Text = "CompCode:-" + dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                    lblmidcard.Text = "MIDCardNo:-" + dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void dataGridView1_MouseHover(object sender, EventArgs e)
        {
          
        }

        private void dataGridView1_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4 && dataGridView1.CurrentCell is DataGridViewCheckBoxCell)
            {
                bool isChecked = (bool)dataGridView1[e.ColumnIndex, e.RowIndex].EditedFormattedValue;
                if (isChecked == true)
                {
                    DataGridViewCellStyle CellStyle = new DataGridViewCellStyle();
                    CellStyle.BackColor = Color.Red;
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style = CellStyle;
                    datagridviewfalse = true;
                }
                else
                {
                    DataGridViewCellStyle CellStyle = new DataGridViewCellStyle();
                    CellStyle.BackColor = Color.White;
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style = CellStyle;
                }
                dataGridView1.EndEdit();
            }
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

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            try
            {
                bytes = null; //Class.Users.UserTime = 0;
                PictureBox p = sender as PictureBox;
                if (p != null)
                {
                    open.Filter = "(*.jpg;*.jpeg;*.bmp;)| *.jpg; *.jpeg; *.bmp";
                    if (open.ShowDialog() == DialogResult.OK)
                    {

                        p.Image = new Bitmap(open.FileName);
                        Signbytes = Models.Device.ImageToByteArray(p);
                       

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
           // Class.Users.UserTime = 0;
        }

        public void Searchs(int id)
        {
            throw new NotImplementedException();
        }

        public void Deletes(int id)
        {
            throw new NotImplementedException();
        }

        private void listView1_ItemActivate(object sender, EventArgs e)
        {
            MessageBox.Show("DDDD");
        }
    }
}
