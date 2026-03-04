using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using Oracle.ManagedDataAccess.Client;
using System.Data.OleDb;

namespace Pinnacle.Master.Lovely
{
    public partial class EmployeeMaster : Form,ToolStripAccess
    {
        private static EmployeeMaster _instance;
        Models.Master mas = new Models.Master();
        Models.Lovely.Employee em = new Models.Lovely.Employee();
        Models.UserRights sm = new Models.UserRights();
       byte[] bytes; byte[] bytesinname; byte[] bytesintdesign; byte[] Sbytes; Int64 myString = 0; string PATTH1 = "";
        OpenFileDialog open = new OpenFileDialog(); int i = 0;
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

        public EmployeeMaster()
        {
            InitializeComponent();
            usercheck(Class.Users.HCompcode, Class.Users.HUserName, Class.Users.ScreenName);

            Class.Users.IPADDRESS = GenFun.GetLocalIPAddress();
            Class.Users.CREATED = Convert.ToDateTime(System.DateTime.Now.ToString("dd-MMM-yyyy") + " " + System.DateTime.Now.ToLongTimeString());
            Class.Users.SysDate = Convert.ToString(System.DateTime.Now.ToString("dd/MM/yyyy"));
            Class.Users.SysTime = Convert.ToString(DateTime.Now.ToLongTimeString().ToString());
            GlobalVariables.HeaderName.Text = Class.Users.ScreenName; GlobalVariables.CurrentForm = this; tabControl1.TabPages.Remove(tabPage3);
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


                        //if (dt1.Rows[r]["NEWS"].ToString() == "T") { GlobalVariables.News.Visible = true; } else { GlobalVariables.News.Visible = false; }
                        //if (dt1.Rows[r]["SAVES"].ToString() == "T") { GlobalVariables.Saves.Visible = true; } else { GlobalVariables.Saves.Visible = false; }
                        //if (dt1.Rows[r]["PRINTS"].ToString() == "T") { GlobalVariables.Prints.Visible = true; } else { GlobalVariables.Prints.Visible = false; }
                        if (dt1.Rows[r]["READONLY"].ToString() == "T") {this.Enabled = true; comboidcardno.Enabled = true; } else { this.Enabled = true; comboidcardno.Enabled = false;  }
                        //if (dt1.Rows[r]["SEARCH"].ToString() == "T") { GlobalVariables.Searchs.Visible = true; } else { GlobalVariables.Searchs.Visible = false; }
                        //if (dt1.Rows[r]["DELETES"].ToString() == "T") { GlobalVariables.Deletes.Visible = true; } else { GlobalVariables.Deletes.Visible = false; }
                        if (dt1.Rows[r]["TREEBUTTON"].ToString() == "T") {PictureBox1.Enabled = true;  } else { PictureBox1.Enabled = false;  }
                        //if (dt1.Rows[r]["GLOBALSEARCH"].ToString() == "T") { GlobalVariables.GlobalSearchs.Visible = true; } else { GlobalVariables.GlobalSearchs.Visible = false; }
                        //if (dt1.Rows[r]["LOGIN"].ToString() == "T") { GlobalVariables.Logins.Visible = true; } else { GlobalVariables.Logins.Visible = false; }
                        //if (dt1.Rows[r]["CHANGEPASSWORD"].ToString() == "T") { GlobalVariables.ChangePasswords.Visible = true; } else { GlobalVariables.ChangePasswords.Visible = false; }
                        //if (dt1.Rows[r]["CHANGESKIN"].ToString() == "T") { GlobalVariables.ChangeSkins.Visible = true; } else { GlobalVariables.ChangeSkins.Visible = false; }
                        //if (dt1.Rows[r]["DOWNLOAD"].ToString() == "T") { GlobalVariables.DownLoads.Visible = true; } else { GlobalVariables.DownLoads.Visible = false; }
                        //if (dt1.Rows[r]["Pdf"].ToString() == "T") { GlobalVariables.Pdfs.Visible = true; } else { GlobalVariables.Pdfs.Visible = false; }
                        //if (dt1.Rows[r]["Imports"].ToString() == "T") { GlobalVariables.Imports.Visible = true; } else { GlobalVariables.Imports.Visible = false; }

                    }
                }


            }
            else
            {

            }

        }
        void listcompcodeload()
        {
            string sel = "SELECT DISTINCT  0 GTCOMPMASTID,'WITHOUT SIGNATURE' COMPCODE,'' COMPNAME  from dual union all SELECT DISTINCT  C.GTCOMPMASTID,C.COMPCODE,c.COMPNAME FROM    HREMPLOYMAST    A   JOIN HREMPLOYDETAILS B ON A.HREMPLOYMASTID=B.HREMPLOYMASTID  AND A.IDCARDNO=B.IDCARD JOIN   GTCOMPMAST C ON C.GTCOMPMASTID = A.COMPCODE      JOIN GTDEPTDESGMAST  E ON E.GTDEPTDESGMASTID =B.DEPTNAME    order by 2";
            DataSet ds = Utility.ExecuteSelectQuery(sel, "hremploymast");
            DataTable dt1 = ds.Tables["hremploymast"];
            if (dt1.Rows.Count > 0)
            {

            

                combocompcode.DisplayMember = "COMPCODE";
                combocompcode.ValueMember = "gtcompmastid";
                combocompcode.DataSource = dt1;

            }
            else
            {
                combocompcode.DataSource = null;
            }
        }
        private void EmployeeMaster_Load(object sender, EventArgs e)
        {


           

        }

        bool ch = false; int rowcount = 0;
        private bool Checks()
        {

            if (pictureBox3.Image == null)
            {
                MessageBox.Show("Signature Field is empty");
                this.pictureBox3.Focus();
                this.pictureBox3.Select(); return false;
            }

            if (empcompcode.Text == "")
            {
                MessageBox.Show("CompCode is empty");
                this.Focus();
                this.empcompcode.Select(); return false;
            }
            if (comboidcardno.Text == "" || comboidcardno.Text == null)
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
                    if (pictureBox3 == null)
                    {
                        MessageBox.Show("UPLOAD IMAGE");
                        this.pictureBox3.Select(); return false;
                    }
                    
                }

            }
            return true;
        }
        public void Saves()
        {


            try
            {
                string s = ""; DataTable dtcheck = new DataTable();
                OracleCommand ascmd = new OracleCommand();
                int CNT = dataGridView1.Rows.Count;
                //string sel0 = "SELECT A.IDCARDNO FROM  ASPTBLEMP A  WHERE A.IDCARDNO=" + txtmidcard.Text;
                //DataSet ds0 = Utility.ExecuteSelectQuery(sel0, "ASPTBLEMP");
                //DataTable dt0 = ds0.Tables["ASPTBLEMP"];
                //if (dt0.Rows.Count <= 0)
                //{

                    if (Checks() == true)
                    {

                        dtcheck.Rows.Clear();
                        //if (Convert.ToInt64(comboempname.SelectedValue) > 0 && Convert.ToInt64(txtidcardno.Text) > 0 && pictureBox3.Image != null)
                        //{
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
                            em.bytename = txtnameintamil.Text;
                            em.bytedesig = txtdesigtamil.Text;
                            if (checkactive.Checked == true)
                            {
                                em.ACTIVE = "T";

                                dtcheck = em.select(em.ACTIVE);
                                em.ACTIVE = "F";
                                if (Convert.ToInt64(dtcheck.Rows[0]["ACTIVE"].ToString()) > 1)
                                {
                                    checkactive.Checked = false; em.ACTIVE = "F";
                                }
                                if (Convert.ToInt64(dtcheck.Rows[0]["ACTIVE"].ToString()) <= 0)
                                {
                                    checkactive.Checked = true;
                                    em.ACTIVE = "T";
                                }
                            }
                            else
                            {
                                checkactive.Checked = false; em.ACTIVE = "F";
                            }



                            em.USERNAME = Convert.ToInt64("0" + Class.Users.USERID);
                            em.IPADDRESS = Class.Users.IPADDRESS;
                            em.CREATEDON = Convert.ToString(Class.Users.CREATED);
                            em.MODIFIEDON = Convert.ToString(Class.Users.CREATED);
                            if (bytesinname == null)
                            {
                                em.imageName = 1;
                            }
                            else
                            {
                                em.imageDeign = Convert.ToInt64("0" + bytesintdesign.Length.ToString());
                            }
                            if (bytesintdesign == null)
                            {
                                em.imageDeign = 1;
                            }
                            else { em.imageDeign = Convert.ToInt64("0" + bytesintdesign.Length.ToString()); }

                            em.bytesign = Sbytes;
                            em.image = Convert.ToInt64("0" + Sbytes.Length);
                            em.imagesign = Convert.ToInt64("0" + Sbytes.Length);




                            DataTable dt = em.select(em.COMPCODE, em.ASPTBLEMPID, em.IDCARDNO, em.ACTIVE, em.imagesign, em.imageName, em.imageDeign);
                            if (dt.Rows.Count != 0)
                            {
                                MessageBox.Show("Child Record Found", "Exception"); empty();
                            }
                            else if (dt.Rows.Count != 0 && em.ASPTBLEMPID == 0 || em.ASPTBLEMPID == 0)
                            {
                        //   string ins = "INSERT INTO ASPTBLEMP (COMPCODE,COMNAME,EMPNAME,EMPID,LASTNAME,GENDER,DATEOFBIRTH,ADDRESS,DEPARTMENT,DATEOFJOIN,IDCARDNO,ACTIVE,USERNAME,IPADDRESS,CREATEDBY,CREATEDON,MODIFIEDON,SIGNATURE,IMAGEBYTES,BYTESNAME,BYTESDESIGN,NAMEBYTES,DESIGNBYTES) VALUES('"+em.COMPCODE+ "','" + em.COMNAME + "','" + em.EMPNAME + "','" + em.EMPID + "','" + em.LASTNAME + "','" + em.GENDER + "',:DATEOFBIRTH,:ADDRESS,:DEPARTMENT,:DATEOFJOIN,:IDCARDNO,:ACTIVE,:USERNAME,:IPADDRESS,:CREATEDBY,:CREATEDON,:MODIFIEDON,:SIGNATURE,:IMAGEBYTES,:BYTESNAME,:BYTESDESIGN,:NAMEBYTES,:DESIGNBYTES)";
                        string ins = "INSERT INTO ASPTBLEMP (COMPCODE,COMNAME,EMPNAME,EMPID,LASTNAME,GENDER,DATEOFBIRTH,ADDRESS,DEPARTMENT,DATEOFJOIN,IDCARDNO,ACTIVE,USERNAME,IPADDRESS,CREATEDBY,CREATEDON,MODIFIEDON,BYTESNAME,BYTESDESIGN,NAMEBYTES,DESIGNBYTES,SIGNATURE,IMAGEBYTES) VALUES('" + em.COMPCODE + "','" + em.COMNAME + "','" + em.EMPNAME + "','" + em.EMPID + "','" + em.LASTNAME + "','" + em.GENDER + "','" + em.DATEOFBIRTH + "','" + em.ADDRESS + "','" + em.DEPARTMENT + "','" + em.DATEOFJOIN + "','" + em.IDCARDNO + "','" + em.ACTIVE + "','" + em.USERNAME + "','" + em.IPADDRESS + "','" + em.CREATEDBY + "','" + em.CREATEDON + "','" + em.MODIFIEDON + "','" + em.bytename + "','" + em.bytedesig + "','" + em.imageName + "','" + em.imageDeign + "',:SIGNATURE,:IMAGEBYTES)";

                        ExecuteNonQuery1(ins, Sbytes, bytes);
         
                        MessageBox.Show("Record Saved Saved Successfully", "Success"); GridLoad(); empty();
                            }
                            else
                            {
                        string UP = "UPDATE   ASPTBLEMP SET COMPCODE='"+ em.COMPCODE + "',COMNAME='" + em.COMNAME + "',EMPNAME='" + em.EMPNAME + "',EMPID='" + em.EMPID + "',LASTNAME='" + em.LASTNAME + "',GENDER='" + em.GENDER + "',DATEOFBIRTH='" + em.DATEOFBIRTH + "',ADDRESS='" + em.ADDRESS + "',DEPARTMENT='" + em.DEPARTMENT + "',DATEOFJOIN='" + em.DATEOFJOIN + "',IDCARDNO='" + em.IDCARDNO + "',ACTIVE='" + em.ACTIVE + "',USERNAME='" + em.USERNAME + "',IPADDRESS='" + em.IPADDRESS + "',CREATEDBY='" + em.CREATEDBY + "',CREATEDON='" + em.CREATEDON + "',MODIFIEDON='" + em.MODIFIEDON + "'" +
                            " ,BYTESNAME='" + em.bytename + "',BYTESDESIGN='" + em.bytedesig + "' ,NAMEBYTES='" + em.imageName + "',DESIGNBYTES='" + em.imageDeign + "' WHERE ASPTBLEMPID='" + em.ASPTBLEMPID + "'";
                        ExecuteNonQuery1(UP, Sbytes, bytes); 
                       
                        MessageBox.Show("Record Updated  Successfully", "Success"); GridLoad(); empty();
                            }

                        }
                    //}
                //}
                //else
                //{
                //    MessageBox.Show("This IDCardNO already Saved"+ comboidcardno.Text);empty();
                //}
            }
            catch (Exception EX)
            {
                MessageBox.Show(EX.ToString());
            }
           
        }
        public static bool ExecuteNonQuery1(string query, Byte[] SIGNATURE, Byte[] IMAGEBYTES)
        {
            OracleCommand cmd = new OracleCommand();
            if (Utility.con.State == ConnectionState.Closed)
            {
                Utility.Connect();
            }
            cmd = new OracleCommand(query, Utility.con);
            cmd.Parameters.Add(":SIGNATURE", SIGNATURE);
            cmd.Parameters.Add(":IMAGEBYTES", IMAGEBYTES);
            cmd.ExecuteNonQuery();
            Utility.DisConnect();
            return true;
        }

        private void EmployeeMaster_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }

        public void News()
        {
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
            }
            else
            {
                empcompcode.DataSource = null;
            }
            listcompcodeload();
            GridLoad(); 
            GlobalVariables.HeaderName.Text = Class.Users.ScreenName;
            empty(); empcompcode.Enabled = true;           
            txtmidcard.Enabled = true; this.pictureBox3.Enabled = true;
            Class.Users.UserTime = 0;
            tabControl1.SelectTab(tabPage1);
          
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

        private void PictureBox3_Click(object sender, EventArgs e)
        {
            try
            {
                Sbytes = null;pictureBox3.Image = null;
                PictureBox p = sender as PictureBox;
                if (p != null)
                {
                    open.Filter = "(*.jpg;*.jpeg;*.bmp;)| *.jpg; *.jpeg; *.bmp";
                    if (open.ShowDialog() == DialogResult.OK)
                    {

                        p.Image = new Bitmap(open.FileName);
                        Sbytes = Models.Device.ImageToByteArray(p);
                        System.Text.Encoding enc = System.Text.Encoding.ASCII;
                        myString = Convert.ToInt64("0" + Sbytes.Length);
                        lblsize.Text = "Image Size : " + Sbytes.Length / 1024 + "kb";
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
                        string selemp = "SELECT B.IDCARD,A.HREMPLOYMASTID,CONCAT(a.fname ,concat('-',B.IDCARD) ) as EMPNAME,E.GTDEPTDESGMASTID,E.DISPNAME FROM    HREMPLOYMAST    A   JOIN HREMPLOYDETAILS B ON A.HREMPLOYMASTID=B.HREMPLOYMASTID  AND A.IDCARDNO=B.IDCARD  JOIN   GTCOMPMAST C ON C.GTCOMPMASTID = A.COMPCODE      JOIN GTDEPTDESGMAST  E ON E.GTDEPTDESGMASTID =B.DEPTNAME    WHERE C.COMPCODE='" + empcompcode.Text + "'   ORDER BY 1";// 
                        DataSet dsemp = Utility.ExecuteSelectQuery(selemp, "hremploymast");
                        DataTable dtemp = dsemp.Tables["hremploymast"];

                        comboidcardno.DisplayMember = "IDCARD";
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
                        string selemp1 = "select COUNT(A.hremploymastid) AS cnt from  HREMPLOYMAST    A   JOIN HREMPLOYDETAILS B ON A.HREMPLOYMASTID=B.HREMPLOYMASTID  AND A.IDCARDNO=B.IDCARD  JOIN   GTCOMPMAST C ON C.GTCOMPMASTID = A.COMPCODE      JOIN GTDEPTDESGMAST  E ON E.GTDEPTDESGMASTID =B.DEPTNAME     WHERE C.COMPCODE='" + empcompcode.Text + "'";
                        DataSet dsemp1 = Utility.ExecuteSelectQuery(selemp1, "hremploymast");
                        DataTable dtemp1 = dsemp1.Tables["hremploymast"];


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
                empty(); DataTable dt = new DataTable();
            
                if (lvlogs.Items.Count > 0)
                {
                    if (combocompcode.Text == "WITHOUT SIGNATURE")
                    {
                        em.ASPTBLEMPID = Convert.ToInt64("0" + lvlogs.SelectedItems[0].SubItems[4].Text);
                        string sel1 = "SELECT  X.ASPTBLEMPID, X.COMPCODE ,X.COMPNAME,X.EMPNAME,X.LASTNAME ,X.ADDRESS,X.GENDER,X.DATEOFBIRTH,X.IDCARDNO,X.DEPARTMENT,X.DATEOFJOIN , X.CONTACT,  X.BLOODGROUP,X.ACTIVE,X.EMPLOYEETYPE,X.SIGNATURE  FROM(  SELECT  A.ASPTBLEMPID, B.COMPCODE ,B.COMPNAME,D.FNAME EMPNAME,NULL LASTNAME ,NULL ADDRESS, A.GENDER,A.DATEOFBIRTH, E.IDCARD IDCARDNO,C.DISPNAME AS DEPARTMENT,A.DATEOFJOIN ,  A.CONTACT,A.BLOODGROUP,A.ACTIVE,A.EMPLOYEETYPE,A.SIGNATURE   FROM   HREMPLOYMAST D  JOIN HREMPLOYDETAILS E ON D.HREMPLOYMASTID=E.HREMPLOYMASTID  JOIN   GTCOMPMAST B ON B.GTCOMPMASTID = D.COMPCODE    JOIN GTDEPTDESGMAST C ON C.GTDEPTDESGMASTID = E.DEPTNAME  LEFT JOIN ASPTBLEMP A ON A.EMPID =  E.HREMPLOYMASTID   ) X  WHERE   X.SIGNATURE IS   NULL AND X.EMPNAME IS NOT NULL  AND  X.IDCARDNO=" + em.ASPTBLEMPID;
                        DataSet ds = Utility.ExecuteSelectQuery(sel1, "ASPTBLEMP");
                        dt = ds.Tables["ASPTBLEMP"];
                        if (dt != null)
                        {
                            foreach (DataRow myRow in dt.Rows)
                            {

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




                                empcompcode.Enabled = false;
                                comboidcardno.Enabled = false;



                                comboEmpType.Text = Convert.ToString(myRow["EMPLOYEETYPE"].ToString());

                                tabControl1.SelectTab(tabPage1);

                            }
                        }
                    }
                   else
                    {

                        em.ASPTBLEMPID = Convert.ToInt64("0" + lvlogs.SelectedItems[0].SubItems[1].Text);
                        PictureBox1.Image = null; bytes = null; Sbytes = null;
                      

                        string sel1 = "SELECT A.ASPTBLEMPID, B.COMPCODE ,B.COMPNAME,A.EMPNAME,A.LASTNAME ,A.ADDRESS,A.GENDER,A.DATEOFBIRTH,A.IDCARDNO,C.DISPNAME AS DEPARTMENT,A.DATEOFJOIN ,A.CONTACT,A.BLOODGROUP,A.ACTIVE,A.EMPLOYEETYPE, A.SIGNATURE,A.BYTESNAME,A.BYTESDESIGN FROM  ASPTBLEMP A JOIN   GTCOMPMAST B ON B.gtcompmastid = A.COMPCODE JOIN gtdeptdesgmast C ON C.gtdeptdesgmastid = A.DEPARTMENT WHERE A.ASPTBLEMPID=" + em.ASPTBLEMPID;
                        DataSet ds = Utility.ExecuteSelectQuery(sel1, "ASPTBLEMP");
                        dt = ds.Tables["ASPTBLEMP"];
                        if (dt != null)
                        {
                            foreach (DataRow myRow in dt.Rows)
                            {
                                txtempid.Text = Convert.ToString(myRow["ASPTBLEMPID"].ToString());
                                empcompcode.Text = Convert.ToString(myRow["COMPCODE"].ToString());
                                empcompname.Text = Convert.ToString(myRow["COMPNAME"].ToString());
                                comboempname.Text = Convert.ToString(myRow["EMPNAME"].ToString());
                            //    txtlastname.Text = Convert.ToString(myRow["LASTNAME"].ToString());
                               // txtaddress.Text = Convert.ToString(myRow["ADDRESS"].ToString());
                                if (myRow["GENDER"].ToString() == "T") radiomale.Checked = true;
                                else radiofemale.Checked = true;
                                txtdateofbirth.Text = Convert.ToString(myRow["DATEOFBIRTH"].ToString());
                                combodept.Text = Convert.ToString(myRow["DEPARTMENT"].ToString());
                                txtdateofjoin.Text = Convert.ToString(myRow["DATEOFJOIN"].ToString());
                                txtidcardno.Text = Convert.ToString(myRow["IDCARDNO"].ToString());
                                txtcontactno.Text = Convert.ToString(myRow["CONTACT"].ToString());
                                txtmidcard.Text = Convert.ToString(myRow["IDCARDNO"].ToString());
                                combobroup.Text = Convert.ToString(myRow["BLOODGROUP"].ToString());
                                txtnameintamil.Text = Convert.ToString(myRow["BYTESNAME"].ToString());
                                txtdesigtamil.Text = Convert.ToString(myRow["BYTESDESIGN"].ToString());
                                if (myRow["ACTIVE"].ToString() == "T") checkactive.Checked = true; else checkactive.Checked = false;
                                if (myRow["SIGNATURE"].ToString() != "")
                                {
                                    Sbytes = (byte[])myRow["SIGNATURE"];
                                    Image img1 = Models.Device.ByteArrayToImage(Sbytes);
                                    pictureBox3.Image = img1;
                                    lblsize.Text = "Image Size : " + Sbytes.Length / 1024 + "kb";
                                }



                                empcompcode.Enabled = false;
                                comboidcardno.Enabled = false;




                                comboEmpType.Text = Convert.ToString(myRow["EMPLOYEETYPE"].ToString());

                                tabControl1.SelectTab(tabPage1);

                            }
                        }
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
                lvlogs.Items.Clear(); int r = 1; listfilter.Items.Clear(); DataTable dt=new DataTable();
                if (combocompcode.Text == "WITHOUT SIGNATURE")
                {
                    string sel0 = "SELECT  X.ASPTBLEMPID, X.COMPCODE ,X.COMPNAME,X.EMPNAME,X.LASTNAME ,X.ADDRESS,X.GENDER,X.DATEOFBIRTH,X.IDCARDNO,X.DEPARTMENT,X.DATEOFJOIN , X.CONTACT, X.BLOODGROUP,X.ACTIVE,X.EMPLOYEETYPE,X.SIGNATURE  FROM(  SELECT  A.ASPTBLEMPID, B.COMPCODE ,B.COMPNAME,D.FNAME EMPNAME,NULL LASTNAME ,NULL ADDRESS,A.GENDER,A.DATEOFBIRTH, E.IDCARD IDCARDNO,C.DISPNAME AS DEPARTMENT,A.DATEOFJOIN ,  A.CONTACT,A.BLOODGROUP,A.ACTIVE,A.EMPLOYEETYPE,A.SIGNATURE  FROM   HREMPLOYMAST D  JOIN HREMPLOYDETAILS E ON D.HREMPLOYMASTID=E.HREMPLOYMASTID  JOIN   GTCOMPMAST B ON B.GTCOMPMASTID = D.COMPCODE   JOIN GTDEPTDESGMAST C ON C.GTDEPTDESGMASTID = E.DEPTNAME  LEFT JOIN ASPTBLEMP A ON A.EMPID =  E.HREMPLOYMASTID     ) X WHERE   X.SIGNATURE IS   NULL AND X.EMPNAME IS NOT NULL order by IDCARDNO DESC";
                    DataSet ds0 = Utility.ExecuteSelectQuery(sel0, "HREMPLOYMAST");
                    dt = ds0.Tables["HREMPLOYMAST"];
                }
               else
                {
                    string sel1 = "SELECT A.ASPTBLEMPID,B.COMPCODE,A.EMPNAME,A.IDCARDNO ,A.GENDER,C.DISPNAME AS DEPARTMENT,A.CONTACT,A.BLOODGROUP,A.EMPLOYEETYPE,A.ACTIVE   FROM  ASPTBLEMP A   JOIN   GTCOMPMAST B ON B.gtcompmastid = A.COMPCODE JOIN gtdeptdesgmast C ON C.gtdeptdesgmastid = A.DEPARTMENT where B.COMPCODE='" + combocompcode.Text + "'  order by 1";
                    DataSet ds = Utility.ExecuteSelectQuery(sel1, "ASPTBLEMP");
                    dt = ds.Tables["ASPTBLEMP"];
                   
                }
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
                        lvlogs.Items.Add(list);


                    }
                    lblemptot.Text = "Total Rows    :" + lvlogs.Items.Count;
                }
                else
                {
                    MessageBox.Show("No Data Found");
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
                lvlogs.Items.Clear(); listfilter.Items.Clear(); int r = 1;
                string sel1 = "SELECT A.ASPTBLEMPID,B.COMPCODE,A.EMPNAME,A.IDCARDNO ,A.GENDER,C.MNAME AS DEPARTMENT,A.CONTACT,A.BLOODGROUP,A.EMPLOYEETYPE,A.ACTIVE   FROM  ASPTBLEMP A   JOIN   GTCOMPMAST B ON B.gtcompmastid = A.COMPCODE JOIN gtdeptdesgmast C ON C.gtdeptdesgmastid = A.DEPARTMENT   order by a.ASPTBLEMPID desc";
                DataSet ds = Utility.ExecuteSelectQuery(sel1, "ASPTBLEMP");
                DataTable dt = ds.Tables["ASPTBLEMP"];
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
                    //list.SubItems.Add(myRow["EMPLOYEETYPE"].ToString());

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
                    lvlogs.Items.Add(list);


                }
                lblemptot.Text = "Total Rows    :" + lvlogs.Items.Count;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void empty()
        {
            txtempid.Text = "";           
            comboempname.Text = ""; 
            empcompname.Text = "";
            comboidcardno.Text = ""; comboidcardno.SelectedIndex = -1;
            txtlastname.Text = "";
            txtaddress.Text = "";
           // combocompcode.Text = "";
            radiomale.Checked = false; checkactive.Checked = false;
            txtdateofbirth.Text = "";
            combodept.Text = "";
            txtdateofjoin.Text = ""; pictureBox2.Image = null; lblmidcard.Text = ""; lblempid.Text = "";
            txtidcardno.Text = "";
            txtcontactno.Text = ""; comboEmpType.Text = ""; comboEmpType.SelectedIndex = -1; //listfilter.Items.Clear();
            progressBar1.Minimum = 0; lblprogress1.Text = ""; progressBar1.Value = 0;txtdesigtamil.Text =""; txtnameintamil.Text = "";
            checkactive.Checked = false; txtmidcard.Text = "";
            bytes = null;
            PictureBox1.Image = null;
            pictureBox3.Image = null;
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
                    lvlogs.Items.Clear();

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



                            lvlogs.Items.Add(list);
                        }

                        item0++;
                    }
                    lblemptot.Text = "Total Rows    :" + lvlogs.Items.Count;
                }
                else
                {
                    try
                    {
                        lvlogs.Items.Clear();
                        foreach (ListViewItem item in listfilter.Items)
                        {
                            this.lvlogs.Items.Add((ListViewItem)item.Clone());
                            item0++;
                        }
                        lblemptot.Text = "Total Rows    :" + lvlogs.Items.Count;
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
            PictureBox1.Image = null; bytes = null; pictureBox3.Image = null; Sbytes = null;
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

      
        private void Combobroup_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void RefreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EmployeeMaster_Load(sender, e); usercheck(Class.Users.HCompcode, Class.Users.HUserName, Class.Users.ScreenName);

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
                if (txtmidcard.Text != "")
                {
                    string sel0 = "SELECT a.ASPTBLEMPid, A.IDCARDNO FROM  ASPTBLEMP A  WHERE A.IDCARDNO=" + txtmidcard.Text;
                    DataSet ds0 = Utility.ExecuteSelectQuery(sel0, "ASPTBLEMP");
                    DataTable dt0 = ds0.Tables["ASPTBLEMP"];
                    if (dt0.Rows.Count <= 0 || txtempid.Text != "")
                    {
                        label14.Refresh();
                        label14.Text = "IDCardNo*";
                        if (txtmidcard.Text.Length > 0 && empcompcode.Text != "")
                        {


                            //if (dt0.Rows.Count > 0) { MessageBox.Show("This IDCard Already Saved"); empty(); return; }
                            //else
                            //{
                            string selemp = "SELECT B.IDCARD,A.HREMPLOYMASTID,CONCAT(a.fname ,concat('-',B.IDCARD) ) as EMPNAME,E.GTDEPTDESGMASTID,E.DISPNAME,A.FATHERNAME,A.DOB,B.DOJ,A.GENDER,F.CADD AS ADDRESS,F.MOBNO,B.OIDNO,a.emptype FROM    HREMPLOYMAST    A   JOIN HREMPLOYDETAILS B ON A.HREMPLOYMASTID=B.HREMPLOYMASTID  AND A.IDCARDNO=B.IDCARD  JOIN   GTCOMPMAST C ON C.GTCOMPMASTID = A.COMPCODE  JOIN GTDEPTDESGMAST  E ON E.GTDEPTDESGMASTID = B.DEPTNAME   JOIN HRECONTACTDETAILS F ON E.COMPCODE = A.COMPCODE AND E.COMPCODE = C.GTCOMPMASTID   AND F.HREMPLOYMASTID = A.HREMPLOYMASTID AND F.HREMPLOYMASTID = B.HREMPLOYMASTID     WHERE C.COMPCODE='" + empcompcode.Text + "' AND  B.IDCARD='" + txtmidcard.Text + "'";

                            DataSet dsemp = Utility.ExecuteSelectQuery(selemp, "hremploymast");
                            DataTable dtemp = dsemp.Tables["hremploymast"];
                            if (dtemp.Rows.Count > 0)
                            {
                                comboidcardno.Text = dtemp.Rows[0]["IDCARD"].ToString();

                                comboempname.Text = dtemp.Rows[0]["EMPNAME"].ToString();
                                comboempname.SelectedValue = dtemp.Rows[0]["HREMPLOYMASTID"].ToString();
                                combodept.Text = dtemp.Rows[0]["DISPNAME"].ToString();

                                txtlastname.Text = dtemp.Rows[0]["FATHERNAME"].ToString();
                                txtidcardno.Text = dtemp.Rows[0]["OIDNO"].ToString();

                                txtdateofbirth.Text = dtemp.Rows[0]["DOB"].ToString();
                                txtdateofjoin.Text = dtemp.Rows[0]["DOJ"].ToString();
                                txtaddress.Text = dtemp.Rows[0]["ADDRESS"].ToString();
                                txtcontactno.Text = dtemp.Rows[0]["MOBNO"].ToString();
                                if (dtemp.Rows[0]["GENDER"].ToString() == "MALE")
                                {
                                    radiomale.Checked = true;
                                }
                                else { radiofemale.Checked = true; }
                                comboEmpType.Text = dtemp.Rows[0]["emptype"].ToString();

                            }
                            else
                            {

                                empcompcode.SelectedIndex = 0;
                                // empcompname.Text = ""; empcompname.SelectedIndex = -1;
                                //empcompname.Text = ""; comboempname.Text = ""; comboempname.SelectedIndex = -1;
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
                                comboEmpType.Text = "";
                                checkactive.Checked = false;
                                bytes = null; Sbytes = null;
                                PictureBox1.Image = null;
                                pictureBox3.Image = null;
                            }


                        }
                        if (txtempid.Text != "" && txtmidcard.Text.Length > 0)
                        {
                            string sel1 = "SELECT A.ASPTBLEMPID, B.COMPCODE ,B.COMPNAME,A.EMPNAME,A.LASTNAME ,A.ADDRESS,A.GENDER,A.DATEOFBIRTH,A.IDCARDNO,C.DISPNAME AS DEPARTMENT,A.DATEOFJOIN ,A.CONTACT,A.BLOODGROUP,A.ACTIVE,A.EMPLOYEETYPE,A.SIGNATURE FROM  ASPTBLEMP A JOIN   GTCOMPMAST B ON B.gtcompmastid = A.COMPCODE JOIN gtdeptdesgmast C ON C.gtdeptdesgmastid = A.DEPARTMENT WHERE A.IDCARDNO=" + txtmidcard.Text;
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
                                    if (myRow["SIGNATURE"].ToString() != "")
                                    {

                                        //bytes = (byte[])myRow["EMPIMAGE"];
                                        //Image img = Models.Device.ByteArrayToImage(bytes);
                                        //PictureBox1.Image = img;
                                        Sbytes = (byte[])myRow["SIGNATURE"];
                                        Image img1 = Models.Device.ByteArrayToImage(Sbytes);
                                        pictureBox3.Image = img1;
                                    }
                                    else
                                    {
                                        PictureBox1.Image = null; pictureBox3.Image = null;
                                    }
                                }
                            }
                            else
                            {
                                //   empcompcode.Text = ""; empcompcode.SelectedIndex = -1; empcompname.Text = ""; empcompname.SelectedIndex = -1;
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
                                bytes = null; Sbytes = null;
                                PictureBox1.Image = null;
                                pictureBox3.Image = null;

                            }
                        }
                        else
                        {
                            //Empcompcode_SelectedIndexChanged(sender, e);
                            // MessageBox.Show("CompCode is Empty");empcompcode.Select();
                            PictureBox1.Image = null; pictureBox3.Image = null;
                        }
                        //}
                    }
                    else
                    {
                        
                            label14.Refresh();
                            label14.Text = "This IDCard  : " + txtmidcard.Text + "  Already saved in EmployeeMaster."; 
                        
                    }
                }
            }
            catch (Exception ex) { }
        }

        private void comboidcardno_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (comboidcardno.Text != "")
                {
                    string sel0 = "SELECT  A.IDCARDNO FROM  ASPTBLEMP A  WHERE A.IDCARDNO=" + comboidcardno.Text;
                    DataSet ds0 = Utility.ExecuteSelectQuery(sel0, "ASPTBLEMP");
                    DataTable dt0 = ds0.Tables["ASPTBLEMP"];
                    if (dt0.Rows.Count <= 0)
                    {
                        label14.Refresh();
                        label14.Text = "IDCardNo*";
                        if (empcompcode.Text != null && comboidcardno.Text != "" && dt0 != null)
                        {
                            //em.bytename = txtnameintamil.Text;
                            //em.bytedesig = txtdesigtamil.Text;
                            //if (bytesinname == null)
                            //{
                            //    em.imageName = 1;
                            //}
                            //else
                            //{
                            //    em.imageDeign = Convert.ToInt64("0" + bytesintdesign.Length.ToString());
                            //}
                            //if (bytesintdesign == null)
                            //{
                            //    em.imageDeign = 1;
                            //}
                            //else { em.imageDeign = Convert.ToInt64("0" + bytesintdesign.Length.ToString()); }

                            //em.bytesign = Sbytes;
                            //em.image = Convert.ToInt64("0" + Sbytes.Length);
                            //em.imagesign = Convert.ToInt64("0" + Sbytes.Length);

                            //DataTable dtemp = em.select(em.COMPCODE, em.ASPTBLEMPID, em.IDCARDNO, em.ACTIVE, em.imagesign, em.imageName, em.imageDeign);


                            string selemp = "SELECT B.IDCARD,A.HREMPLOYMASTID,CONCAT(a.fname ,concat('-',B.IDCARD) ) as EMPNAME,E.GTDEPTDESGMASTID,E.DISPNAME,A.FATHERNAME,A.DOB,B.DOJ,A.GENDER,F.CADD AS ADDRESS,F.MOBNO,B.OIDNO,a.emptype FROM    HREMPLOYMAST    A   JOIN HREMPLOYDETAILS B ON A.HREMPLOYMASTID=B.HREMPLOYMASTID AND A.IDCARDNO=B.IDCARD  JOIN   GTCOMPMAST C ON C.GTCOMPMASTID = A.COMPCODE  JOIN GTDEPTDESGMAST  E ON E.GTDEPTDESGMASTID = B.DEPTNAME   JOIN HRECONTACTDETAILS F ON E.COMPCODE = A.COMPCODE AND E.COMPCODE = C.GTCOMPMASTID   AND F.HREMPLOYMASTID = A.HREMPLOYMASTID AND F.HREMPLOYMASTID = B.HREMPLOYMASTID    WHERE C.COMPCODE='" + empcompcode.Text + "' AND  B.IDCARD='" + comboidcardno.Text + "'";
                            DataSet dsemp = Utility.ExecuteSelectQuery(selemp, "hremploymast");
                            DataTable dtemp = dsemp.Tables["hremploymast"];

                            comboempname.DisplayMember = "EMPNAME";
                            comboempname.ValueMember = "HREMPLOYMASTID";
                            comboempname.DataSource = dtemp;


                            combodept.DisplayMember = "DISPNAME";
                            combodept.ValueMember = "GTDEPTDESGMASTID";
                            combodept.DataSource = dtemp;

                            txtlastname.Text = dtemp.Rows[0]["FATHERNAME"].ToString();
                            txtidcardno.Text = dtemp.Rows[0]["OIDNO"].ToString();
                            txtdateofbirth.Text = dtemp.Rows[0]["DOB"].ToString();
                            txtdateofjoin.Text = dtemp.Rows[0]["DOJ"].ToString();
                            txtaddress.Text = dtemp.Rows[0]["ADDRESS"].ToString();
                            txtcontactno.Text = dtemp.Rows[0]["MOBNO"].ToString();
                            if (dtemp.Rows[0]["GENDER"].ToString() == "MALE")
                            {
                                radiomale.Checked = true;
                            }
                            else { radiofemale.Checked = true; }
                            //comboEmpType.DisplayMember = "emptype";
                            //comboEmpType.ValueMember = "HREMPLOYMASTID";
                            //comboEmpType.DataSource = dtemp;
                            comboEmpType.Text = dtemp.Rows[0]["emptype"].ToString();

                        }
                    }
                    else
                    {
                        
                            label14.Refresh();
                            label14.Text = "This IDCard  : " + comboidcardno.Text + "  Already saved in EmployeeMaster.";
                            
                    }
                }
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
            EmployeeMaster_Load(sender, e); usercheck(Class.Users.HCompcode, Class.Users.HUserName, Class.Users.ScreenName);

        }

        private void Searchs_Click(object sender, EventArgs e)
        {
           // tabControl1.SelectTab(tabPage2);
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
                    lblmidcard.Text = "IDCARDNo:-" + dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
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
            this.Hide();
            GlobalVariables.MdiPanel.Show();
            empty();
            GlobalVariables.HeaderName.Text = "";
            GlobalVariables.TabCtrl.TabPages.RemoveAt(GlobalVariables.TabCtrl.SelectedIndex);
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
                    DataTable dt = new DataTable(); int i = 0;// tabControl1.SelectTab(tabPage3);
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

        private void checkactive_CheckedChanged(object sender, EventArgs e)
        {
           

            //    DataTable dt = new DataTable();
            //    dt.Rows.Clear();
            //if (Convert.ToInt64(comboempname.SelectedValue) > 0 && Convert.ToInt64(txtidcardno.Text) > 0)
            //{

            //    if (checkactive.Checked) em.ACTIVE = "T"; else em.ACTIVE = "F";

            //    dt = em.select(em.ACTIVE);
            //    if (dt.Rows.Count >= 2)
            //    {                    
            //        checkactive.Checked = false;
            //    }
            //}
            
        }

        private void txtmidcard_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= '0' && e.KeyChar <= '9' || e.KeyChar == (char)Keys.Back) //The  character represents a backspace
            {
                e.Handled = false; //Do not reject the input
            }
            else
            {
                e.Handled = true; //Reject the input
            }
        }

      

        private void button2_Click(object sender, EventArgs e)
        {
           
        }

      

        private void pictureBox4inTamil_Click(object sender, EventArgs e)
        {
            
        }

        private void tabControl1_TabIndexChanged(object sender, EventArgs e)
        {
          
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage2"])//your specific tabname
            {
                //tabControl1.TabPages.Remove(tabPage3);
                txtsearch.Select();
            }
            if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage1"])//your specific tabname
            {
                //tabControl1.TabPages.Remove(tabPage3);
                combocompcode.Select();
            }
        }

        private void txtnameintamil_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboempname_SelectedIndexChanged(object sender, EventArgs e)
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

        public void ReadOnlys()
        {
            throw new NotImplementedException();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
    }
}
