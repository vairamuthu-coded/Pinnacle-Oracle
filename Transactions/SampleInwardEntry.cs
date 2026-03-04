using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Pinnacle.Transactions
{
    public partial class SampleInwardEntry : Form,ToolStripAccess
    {
        Models.Master mas = new Models.Master();
        Models.UserRights sm = new Models.UserRights();
      
        Models.BuyerSample buy = new Models.BuyerSample();
        ListView listfilter = new ListView();
        private static SampleInwardEntry _instance;
        public static SampleInwardEntry Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new SampleInwardEntry();
                GlobalVariables.CurrentForm = _instance; return _instance;
            }
        }
        OpenFileDialog open = new OpenFileDialog();
        byte[] bytes;
        byte[] qrbytes;
        string myString = "";
        int listid = 0;
        string chk = "";
        int i = 0;
        QRCoder.QRCodeGenerator qc = new QRCoder.QRCodeGenerator();
        public SampleInwardEntry()
        {
            InitializeComponent();
            Class.Users.ScreenName = "SampleInwardEntry";
            GlobalVariables.HeaderName.Text = Class.Users.ScreenName; GlobalVariables.CurrentForm = this;
            Class.Users.IPADDRESS = GenFun.GetLocalIPAddress();
            Class.Users.CREATED = Convert.ToDateTime(System.DateTime.Now.ToString("dd-MMM-yyyy") + " " + System.DateTime.Now.ToLongTimeString());
            Class.Users.SysDate = Convert.ToString(System.DateTime.Now.ToString("dd-MM-yyyy"));
            Class.Users.SysTime = Convert.ToString(DateTime.Now.ToLongTimeString().ToString());
           
            butheader.BackColor = Class.Users.BackColors;
            panel1.BackColor = Class.Users.BackColors;
            panel2.BackColor = Class.Users.BackColors;
            butfooter.BackColor = Class.Users.BackColors;

        }
        public void FinyearLoad()
        {
            string sel = "select  a.GTFINANCIALYEARID,a.finyear from gtfinancialyear a  WHERE A.CURRENTFINYR='T' ";
            DataSet ds = Utility.ExecuteSelectQuery(sel, "gtfinancialyear");
            DataTable dt = ds.Tables["gtfinancialyear"];

            combofinyear.DisplayMember = "finyear";
            combofinyear.ValueMember = "GTFINANCIALYEARID";
            combofinyear.DataSource = dt;

        }
        public void ReadOnlys()
        {

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

                         if (dt1.Rows[r]["TREEBUTTON"].ToString() == "T") { GlobalVariables.TreeButtons.Visible = false; checkscan.Visible = true;  } else { GlobalVariables.News.Visible = false; checkscan.Visible = false; }
                    }
                }


            }
            else
            {

            }

        }
        private void SampleInwardEntry_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }
        public static string FirstCharToUpper(string s)
        {
            // Check for empty string.  
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            // Return char and concat substring.  
            return char.ToUpper(s[0]) + s.Substring(1);
        }
        public static string CapitalizeFirstLetters(string sValue)
        {



            char[] array = sValue.ToCharArray();

            if (array.Length >= 1)
            {
                if (char.IsLower(array[0]))
                {
                    array[0] = char.ToUpper(array[0]);
                }
            }

            for (int i = 1; i < array.Length; i++)
            {
                if (array[i] == ' ')
                {
                    if (char.IsLower(array[i + 1]))
                    {
                        array[i + 1] = char.ToUpper(array[i + 1]); // space is identified at Index i so next i+1 has to converted to Upper!!
                    }
                }
            }


            return new string(array);

        }






        private void SampleInwardEntry_Load(object sender, EventArgs e)
        {
            BrandLoad(); compcode();
            SeasonLoad(); DepartmentLoad(); rackload(); binload(); FinyearLoad();
            GridLoad(dateTimePicker1.Value.ToString("dd-MM-yyyy")); empty(); txtqrcode.BackColor = System.Drawing.Color.Beige;
            txtqrcode.Select();
        }
       void rackload()
        {
            string sel = " SELECT A.ASPTBLRACKMASID,A.RACK    FROM  ASPTBLRACKMAS A    WHERE A.ACTIVE='T' order by a.ASPTBLRACKMASID asc  ";
            DataSet ds = Utility.ExecuteSelectQuery(sel, "ASPTBLRACKMAS");
            DataTable dt = ds.Tables["ASPTBLRACKMAS"];
            comborack.DataSource = dt;
            comborack.DisplayMember = "RACK";
            comborack.ValueMember = "ASPTBLRACKMASID";
        }
        void binload()
        {
            string sel = " SELECT A.asptblbinmasid,A.bin    FROM  asptblbinmas A    WHERE A.ACTIVE='T'  order by a.asptblbinmasid asc ";
            DataSet ds = Utility.ExecuteSelectQuery(sel, "asptblbinmas");
            DataTable dt = ds.Tables["asptblbinmas"];
            combobin.DataSource = dt;
            combobin.DisplayMember = "bin";
            combobin.ValueMember = "asptblbinmasid";
        }
        private void Listviewbuyqrcode_ItemActivate(object sender, EventArgs e)
        {
            try
            {
                if (listviewbuyqrcode.Items.Count >= 0)
                {
                    txtbuysamid.Text= Convert.ToString(listviewbuyqrcode.SelectedItems[0].SubItems[3].Text);
                    string sel0 = "SELECT  A.ASPTBLBUYSAMID, AA.ASPTBLBUYSAMINWID,AA.INDATE, B.COMPCODE,C.BRAND,A.AGFSAMPLE, E.SEASON,F.DEPARTMENT,O.CATEGORY,A.STYLENAME,R.SAMPLETYPE,A.GARMENTIMAGE,A.ACTIVE,A.MFYEAR,P.RACK,Q.BIN,AA.INWARD,AA.INWARDACTIVE    FROM ASPTBLBUYSAMINW  AA JOIN ASPTBLBUYSAM  A ON A.AGFSAMPLE=AA.AGFSAMPLE  JOIN GTCOMPMAST B ON A.COMPCODE=B.GTCOMPMASTID join asptblBRANDMAS C ON C.ASPTBLBRANDMASID=A.BRAND  JOIN ASPTBLSEASONMAS E ON E.ASPTBLSEASONMASID=A.SEASON  JOIN ASPTBLSAMDEPTMAS F ON F.ASPTBLSAMDEPTMASID=A.DEPARTMENT  JOIN ASPTBLSAMCATMAS O ON O.ASPTBLSAMCATMASID=A.CATEGORY    JOIN ASPTBLRACKMAS P ON P.ASPTBLRACKMASID=AA.RACK JOIN ASPTBLBINMAS Q ON Q.ASPTBLBINMASID=AA.BIN JOIN ASPTBLSAMTYPEMAS R ON R.ASPTBLSAMTYPEMASID=A.SAMPLETYPE where AA.ASPTBLBUYSAMINWID=" + txtbuysamid.Text;
                    DataSet ds0 = Utility.ExecuteSelectQuery(sel0, "ASPTBLBUYSAM");
                    DataTable dt0 = ds0.Tables["ASPTBLBUYSAM"];
                    myString = "";

                    txtsaminwardid.Text = dt0.Rows[0]["ASPTBLBUYSAMINWID"].ToString();
                    txtdate.Text = dt0.Rows[0]["INDATE"].ToString();
                    combocompcode.Text = dt0.Rows[0]["COMPCODE"].ToString();
                    combobrand.Text = dt0.Rows[0]["BRAND"].ToString();
                    txtcode.Text = dt0.Rows[0]["AGFSAMPLE"].ToString();
                    comboseason.Text = dt0.Rows[0]["SEASON"].ToString();
                    combodept.Text = dt0.Rows[0]["DEPARTMENT"].ToString();
                    combocategory.Text = dt0.Rows[0]["CATEGORY"].ToString();
                    combostyle.Text = dt0.Rows[0]["STYLENAME"].ToString();
                    combosampletype.Text = dt0.Rows[0]["SAMPLETYPE"].ToString();
                    combosize.Text = dt0.Rows[0]["ASPTBLBUYSAMID"].ToString();
                    combocounts.Text = dt0.Rows[0]["ASPTBLBUYSAMID"].ToString();
                    combocolor.Text = dt0.Rows[0]["ASPTBLBUYSAMID"].ToString();                    
                    combomfyear.Text = dt0.Rows[0]["MFYEAR"].ToString();
                    if (dt0.Rows[0]["INWARD"].ToString() == "T") checkin.Checked = true; else checkin.Checked = false;
                    if (dt0.Rows[0]["INWARDACTIVE"].ToString() == "T") checkinactive.Checked = true; else checkinactive.Checked = false;
                    comborack.Text = dt0.Rows[0]["RACK"].ToString();
                    combobin.Text = dt0.Rows[0]["BIN"].ToString();
                    bytes = (byte[])dt0.Rows[0]["GARMENTIMAGE"];
                    Image img = Models.Device.ByteArrayToImage(bytes);
                    picturegarmentimage.Image = img; 


                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void empty()
        {
            txtsaminwardid.Text = ""; combocompcode.SelectedIndex = -1;txtdate.Text = ""; comborack.BackColor = Color.White;
            combobin.BackColor = Color.White;txtqrcode.Text = "";
            combobrand.SelectedIndex = -1; combodept.SelectedIndex = -1;
            comboseason.SelectedIndex = -1;txtprebin.Text = "";txtprerack.Text = "";
            combostyle.Text = ""; combomfyear.Text = "";combocategory.SelectedIndex = -1; combocategory.Text = ""; txtcode.Text = "";
            combosampletype.Text = ""; combosampletype.SelectedIndex = -1;  comborack.SelectedIndex= - 1;combobin.SelectedIndex= - 1;
            combosize.Text = "";
            combocounts.Text = "";
            combocolor.Text = "";
            txtbuysamid.Text = "";
            bytes = null;
            qrbytes = null; Class.Users.UserTime = 0;
            picturegarmentimage.Image = null;
            butheader.BackColor = Class.Users.BackColors;
            panel1.BackColor = Class.Users.BackColors;
            panel2.BackColor = Class.Users.BackColors;
            butfooter.BackColor = Class.Users.BackColors;
            readfalse(); txtqrcode.Select();

        }
       
        public void compcode()
        {
            string sel = " SELECT A.GTCOMPMASTID,A.COMPCODE    FROM  GTCOMPMAST A    WHERE A.ACTIVE='T'  ";
            DataSet ds = Utility.ExecuteSelectQuery(sel, "GTCOMPMAST");
            DataTable dt = ds.Tables["GTCOMPMAST"];
            combocompcode.DataSource = dt;
            combocompcode.DisplayMember = "COMPCODE";
            combocompcode.ValueMember = "GTCOMPMASTID";

        }

        void CategoryLoad(string dept)
        {
            string sel = " SELECT distinct b.asptblsamcatmasid,b.category    FROM  ASPTBLSAMDEPTMAS  a join asptblsamcatmas b on a.category=b.asptblsamcatmasid   WHERE a.ACTIVE='T' and a.department='" + dept + "' ";
            DataSet ds = Utility.ExecuteSelectQuery(sel, "asptblsamcatmas");
            DataTable dt = ds.Tables["asptblsamcatmas"];
            if (dt.Rows.Count > 0)
            {
                combocategory.DataSource = dt;
                combocategory.DisplayMember = "category";
                combocategory.ValueMember = "asptblsamcatmasid";
                combocategory.BackColor = Color.White;
            }
        }

        public void BrandLoad()
        {
            string sel = " SELECT A.ASPTBLBRANDMASID,A.BRAND    FROM  ASPTBLBRANDMAS A JOIN GTCOMPMAST B ON A.COMPCODE=B.GTCOMPMASTID   WHERE A.ACTIVE='T' ";
            DataSet ds = Utility.ExecuteSelectQuery(sel, "ASPTBLBRANDMAS");
            DataTable dt = ds.Tables["ASPTBLBRANDMAS"];
            combobrand.DataSource = dt;
            combobrand.DisplayMember = "BRAND";
            combobrand.ValueMember = "ASPTBLBRANDMASID";

        }
        public void SampleTypeLoad()
        {
            string sel0 = "SELECT A.ASPTBLSAMTYPEMASID,A.SAMPLETYPE FROM ASPTBLSAMTYPEMAS A WHERE A.ACTIVE='T'";
            DataSet ds0 = Utility.ExecuteSelectQuery(sel0, "ASPTBLSAMTYPEMAS");
            DataTable dt0 = ds0.Tables["ASPTBLSAMTYPEMAS"];
            combosampletype.DataSource = dt0;
            combosampletype.DisplayMember = "SAMPLETYPE";
            combosampletype.ValueMember = "ASPTBLSAMTYPEMASID";

        }


        public void SeasonLoad()
        {
            string sel = " SELECT A.ASPTBLSEASONMASID, A.SEASON    FROM  ASPTBLSEASONMAS  A JOIN GTCOMPMAST B ON A.COMPCODE=B.GTCOMPMASTID   WHERE A.ACTIVE='T'";
            DataSet ds = Utility.ExecuteSelectQuery(sel, "ASPTBLSEASONMAS");
            DataTable dt = ds.Tables["ASPTBLSEASONMAS"];
            comboseason.DataSource = dt;
            comboseason.DisplayMember = "SEASON";
            comboseason.ValueMember = "ASPTBLSEASONMASID";

        }

        public void DepartmentLoad()
        {
            string sel = " SELECT ASPTBLSAMDEPTMASID,DEPARTMENT    FROM  ASPTBLSAMDEPTMAS    WHERE ACTIVE='T'";
            DataSet ds = Utility.ExecuteSelectQuery(sel, "ASPTBLSAMDEPTMAS");
            DataTable dt = ds.Tables["ASPTBLSAMDEPTMAS"];
            combodept.DataSource = dt;
            combodept.DisplayMember = "DEPARTMENT";
            combodept.ValueMember = "ASPTBLSAMDEPTMASID";

        }

       
        //public void SAMPLETYPELoad(string sty)
        //{
        //    if (sty == "System.Data.DataRowView") { return; }
        //    else
        //    {
        //        string sel = "SELECT A.ASPTBLSUBSTYMASID,A.SAMPLETYPE    FROM  ASPTBLSUBSTYMAS A join ASPTBLSTYMAS B  ON  A.STYLENAME=B.ASPTBLSTYMASID  WHERE A.ACTIVE='T' AND B.STYLENAME='" + sty + "'";
        //        DataSet ds = Utility.ExecuteSelectQuery(sel, "ASPTBLSUBSTYMAS");
        //        DataTable dt = ds.Tables["ASPTBLSUBSTYMAS"];

        //        comboSAMPLETYPE.DataSource = dt;
        //        comboSAMPLETYPE.DisplayMember = "SAMPLETYPE";
        //        comboSAMPLETYPE.ValueMember = "ASPTBLSUBSTYMASID";
        //    }
        //}

        //public void FabricLoad()
        //{
        //    string sel = " SELECT ASPTBLFABMASID,FABRIC    FROM  ASPTBLFABMAS    WHERE ACTIVE='T'";
        //    DataSet ds = Utility.ExecuteSelectQuery(sel, "ASPTBLFABMAS");
        //    DataTable dt = ds.Tables["ASPTBLFABMAS"];
        //    combofabric.DataSource = dt;
        //    combofabric.DisplayMember = "FABRIC";
        //    combofabric.ValueMember = "ASPTBLFABMASID";

        //}
        //public void FabricContentLoad(string cont)
        //{
        //    if (cont == "System.Data.DataRowView") { return; }
        //    else
        //    {
        //        string sel = " SELECT ASPTBLFABMASID,FABRIC    FROM  ASPTBLFABMAS    WHERE ACTIVE='T' AND FABRIC='" + cont + "'";
        //        DataSet ds = Utility.ExecuteSelectQuery(sel, "ASPTBLFABMAS");
        //        DataTable dt = ds.Tables["ASPTBLFABMAS"];
        //        combofabcontent.DataSource = dt;
        //        combofabcontent.DisplayMember = "FABRIC";
        //        combofabcontent.ValueMember = "ASPTBLFABMASID";
        //    }
        //}
        public void CountsLoad()
        {
            //string sel = " SELECT GTCOUNTRYMASTID,COUNTS    FROM  GTCOUNTRYMAST    WHERE ACTIVE='T'";
            //DataSet ds = Utility.ExecuteSelectQuery(sel, "GTCOUNTRYMAST");
            //DataTable dt = ds.Tables["GTCOUNTRYMAST"];
            //combocounts.DataSource = dt;
            //combocounts.DisplayMember = "COUNTS";
            //combocounts.ValueMember = "GTCOUNTRYMASTID";

        }

       

        //public void GsmLoad()
        //{
        //    string sel = " SELECT ASPTBLGSMMASID,GSM    FROM  ASPTBLGSMMAS    WHERE ACTIVE='T'";
        //    DataSet ds = Utility.ExecuteSelectQuery(sel, "ASPTBLGSMMAS");
        //    DataTable dt = ds.Tables["ASPTBLGSMMAS"];
        //    combogsm.DataSource = dt;
        //    combogsm.DisplayMember = "GSM";
        //    combogsm.ValueMember = "ASPTBLGSMMASID";

        //}
        //public void SizeNameLoad()
        //{
        //    string sel = " SELECT ASPTBLSIZMASID,SIZENAME    FROM  ASPTBLSIZMAS    WHERE ACTIVE='T'";
        //    DataSet ds = Utility.ExecuteSelectQuery(sel, "ASPTBLSIZMAS");
        //    DataTable dt = ds.Tables["ASPTBLSIZMAS"];
        //    combosize.DataSource = dt;
        //    combosize.DisplayMember = "SIZENAME";
        //    combosize.ValueMember = "ASPTBLSIZMASID";

        //}

        public void ColorsLoad()
        {
            //string sel = " SELECT ASPTBLCOLMASID,COLORNAME    FROM  ASPTBLCOLMAS    WHERE ACTIVE='T'";
            //DataSet ds = Utility.ExecuteSelectQuery(sel, "ASPTBLCOLMAS");
            //DataTable dt = ds.Tables["ASPTBLCOLMAS"];
            //combocolor.DataSource = dt;
            //combocolor.DisplayMember = "COLORNAME";
            //combocolor.ValueMember = "ASPTBLCOLMASID";

        }
        public void GridLoad()
        {
            try
            {
                listviewbuyqrcode.Items.Clear(); listfilter.Items.Clear();


                string sel0 = "SELECT A.ASPTBLBUYSAMINWID,A.AGFSAMPLE,B.STYLENAME,Q.SAMPLETYPE,F.BRAND,G.SEASON, D.RACK,E.BIN,A.INDATE,B.GSM,B.COUNTS FROM ASPTBLBUYSAMINW A JOIN ASPTBLBUYSAM B ON A.AGFSAMPLE=B.AGFSAMPLE           JOIN GTCOMPMAST C ON C.GTCOMPMASTID=A.COMPCODE    JOIN ASPTBLRACKMAS D ON D.ASPTBLRACKMASID=A.RACK  JOIN ASPTBLBINMAS E ON E.ASPTBLBINMASID=A.BIN   JOIN ASPTBLBRANDMAS F ON F.ASPTBLBRANDMASID=A.BRAND  JOIN ASPTBLSEASONMAS G ON G.ASPTBLSEASONMASID=A.SEASON  JOIN ASPTBLSAMTYPEMAS Q ON Q.ASPTBLSAMTYPEMASID=B.SAMPLETYPE AND  Q.ASPTBLSAMTYPEMASID=A.SAMPLETYPE where  A.INWARD='T'  order by a.ASPTBLBUYSAMINWID DESC";
                DataSet ds0 = Utility.ExecuteSelectQuery(sel0, "ASPTBLBUYSAMINW");
                DataTable dt = ds0.Tables["ASPTBLBUYSAMINW"];
                if (dt.Rows.Count >= 0)
                {
                    int i = 0; int j = 1; //listviewbuyqrcode.BeginUpdate();
                    foreach (DataRow myRow in dt.Rows)
                    {
                        ListViewItem list = new ListViewItem();

                        list.SubItems.Add(j.ToString());
                        list.SubItems.Add(j.ToString());
                        list.SubItems.Add(myRow["ASPTBLBUYSAMINWID"].ToString());
                        list.SubItems.Add(myRow["AGFSAMPLE"].ToString());
                        list.SubItems.Add(myRow["BRAND"].ToString());
                        list.SubItems.Add(myRow["SEASON"].ToString());
                        list.SubItems.Add(myRow["STYLENAME"].ToString());
                        list.SubItems.Add(myRow["GSM"].ToString());
                        list.SubItems.Add(myRow["COUNTS"].ToString());
                        list.SubItems.Add(myRow["RACK"].ToString());
                        list.SubItems.Add(myRow["BIN"].ToString());
                        list.SubItems.Add(myRow["INDATE"].ToString());
                        //bytes = (byte[])myRow["GARMENTIMAGE"];
                        //Image img = Models.Device.ByteArrayToImage(bytes);
                        //imageList1.Images.Add(img);
                        //list.ImageIndex = i;
                        listviewbuyqrcode.Items.Add(list);
                        listfilter.Items.Add((ListViewItem)list.Clone());
                        if (j % 2 == 0)
                        {
                            list.BackColor = Color.White;
                        }
                        else
                        {
                            list.BackColor = Color.WhiteSmoke;
                        }
                        i++; j++;
                        lbltotal.Text = "Total Rows Count:  " + listviewbuyqrcode.Items.Count.ToString();
                        bytes = null;
                    }
                    listviewbuyqrcode.EndUpdate(); listviewbuyqrcode.Refresh();
                }
                else
                {

                    lbltotal.Text = "Total Rows Count:  " + listviewbuyqrcode.Items.Count.ToString();
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
        public void GridLoad(string date)
        {
            try
            {
                listviewbuyqrcode.Items.Clear(); listfilter.Items.Clear();
                string sel0 = "SELECT A.ASPTBLBUYSAMINWID,A.AGFSAMPLE,B.STYLENAME,B.SAMPLETYPE,F.BRAND,G.SEASON, D.RACK,E.BIN,A.INDATE,B.GSM,B.COUNTS FROM ASPTBLBUYSAMINW A JOIN ASPTBLBUYSAM B ON A.AGFSAMPLE=B.AGFSAMPLE           JOIN GTCOMPMAST C ON C.GTCOMPMASTID=A.COMPCODE    JOIN ASPTBLRACKMAS D ON D.ASPTBLRACKMASID=A.RACK  JOIN ASPTBLBINMAS E ON E.ASPTBLBINMASID=A.BIN   JOIN ASPTBLBRANDMAS F ON F.ASPTBLBRANDMASID=A.BRAND  JOIN ASPTBLSEASONMAS G ON G.ASPTBLSEASONMASID=A.SEASON  JOIN ASPTBLSAMTYPEMAS Q ON Q.ASPTBLSAMTYPEMASID=B.SAMPLETYPE AND  Q.ASPTBLSAMTYPEMASID=A.SAMPLETYPE where a.INDATE='" + date + "' AND A.INWARD='T'  order by a.ASPTBLBUYSAMINWID DESC";
                DataSet ds0 = Utility.ExecuteSelectQuery(sel0, "ASPTBLBUYSAMINW");
                DataTable dt = ds0.Tables["ASPTBLBUYSAMINW"];

                if (dt.Rows.Count >= 0)
                {

                    int i = 0; int j = 1; listviewbuyqrcode.BeginUpdate();
                    foreach (DataRow myRow in dt.Rows)
                    {
                        ListViewItem list = new ListViewItem();

                        list.SubItems.Add(j.ToString());
                        list.SubItems.Add(j.ToString());
                        list.SubItems.Add(myRow["ASPTBLBUYSAMINWID"].ToString());
                        list.SubItems.Add(myRow["AGFSAMPLE"].ToString());
                        list.SubItems.Add(myRow["BRAND"].ToString());
                        list.SubItems.Add(myRow["SEASON"].ToString());
                        list.SubItems.Add(myRow["STYLENAME"].ToString());
                        list.SubItems.Add(myRow["GSM"].ToString());
                        list.SubItems.Add(myRow["COUNTS"].ToString());
                        list.SubItems.Add(myRow["RACK"].ToString());
                        list.SubItems.Add(myRow["BIN"].ToString());
                        list.SubItems.Add(myRow["INDATE"].ToString());
                        //bytes = (byte[])myRow["GARMENTIMAGE"];
                        //Image img = Models.Device.ByteArrayToImage(bytes);
                        //imageList1.Images.Add(img);
                        //list.ImageIndex = i;
                        listviewbuyqrcode.Items.Add(list);
                        listfilter.Items.Add((ListViewItem)list.Clone());
                        if (j % 2 == 0)
                        {
                            list.BackColor = Color.White;

                        }
                        else
                        {
                            list.BackColor = Color.WhiteSmoke;


                        }
                        i++; j++;
                        lbltotal.Text = "Total Rows Count:  " + listviewbuyqrcode.Items.Count.ToString();
                        bytes = null;
                    }
                    listviewbuyqrcode.EndUpdate(); listviewbuyqrcode.Refresh();
                }
                else
                {

                    lbltotal.Text = "Total Rows Count:  " + listviewbuyqrcode.Items.Count.ToString();
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
       
        public void News()
        {
            empty();

        }
        static string BytesToStringConverted(byte[] bytes)
        {
            using (var stream = new MemoryStream(bytes))
            {
                using (var streamReader = new StreamReader(stream))
                {
                    return streamReader.ReadToEnd();
                }
            }

        }

        private void readfalse()
        {
            txtsaminwardid.Enabled = false;
            combocompcode.Enabled = false;
            combobrand.Enabled = false;
            combodept.Enabled = false;
            comboseason.Enabled = false; combocategory.Enabled = true;
            combostyle.Enabled = false; combocategory.Enabled = false;
            combosampletype.Enabled = false; combomfyear.Enabled = false; txtcode.Enabled = false;
        }
        private void readtrue()
        {
            txtsaminwardid.Enabled = true;
            combocompcode.Enabled = true;
            combobrand.Enabled = true;
            combodept.Enabled = true;
            comboseason.Enabled = true;
            combostyle.Enabled = true; txtcode.Enabled = true;
            combosampletype.Enabled = true; combomfyear.Enabled = true;
        }
        private bool check()
        {
            if (combocompcode.Text == "") { combocompcode.BackColor = System.Drawing.Color.Beige; combocompcode.Select(); MessageBox.Show("Reference Code is Empty ", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Information); return false; }
            if (combobrand.Text == "") { combobrand.BackColor = System.Drawing.Color.Beige; combobrand.Select(); MessageBox.Show("Reference Code is Empty ", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Information); return false; }
            if (combocategory.Text == "") { combocategory.BackColor = System.Drawing.Color.Beige; combocategory.Select(); MessageBox.Show("Reference Code  is Empty ", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Information); return false; }
            if (comboseason.Text == "") { comboseason.BackColor = System.Drawing.Color.Beige; comboseason.Select(); MessageBox.Show("Reference Code is Empty ", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Information); return false; }
            if (combodept.Text == "") { combodept.BackColor = System.Drawing.Color.Beige; combodept.Select(); MessageBox.Show("Reference Code is Empty ", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Information); return false; }
            if (combostyle.Text == "") { combostyle.BackColor = System.Drawing.Color.Beige; combostyle.Select(); MessageBox.Show("Reference Code is Empty ", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Information); return false; }
            if (comborack.SelectedValue == null) { comborack.BackColor = System.Drawing.Color.Beige; comborack.Select(); MessageBox.Show("Reference Code is Empty ", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Information); return false; }
            if (combobin.SelectedValue == null) { combobin.BackColor = System.Drawing.Color.Beige; combobin.Select(); MessageBox.Show("Reference Code is Empty ", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Information); return false; }
            return true;
        }
        public void Saves()
        {
            try
            {

                if (check())
                {

                    int grid = Convert.ToInt32("0" + txtbuysamid.Text);
                    int id = Convert.ToInt32("0" + txtsaminwardid.Text);
                    OracleCommand ascmd; string chk1 = "";
                    if (checkin.Checked) chk = "T"; else chk = "F"; if (checkinactive.Checked) chk1 = "T"; else chk1 = "F";

                    string sel = "SELECT ASPTBLBUYSAMINWID FROM  ASPTBLBUYSAMINW  where ASPTBLBUYSAMID='" + grid + "' AND AGFSAMPLE='" + txtcode.Text + "'  AND RACK='" + comborack.SelectedValue + "' AND BIN='" + combobin.SelectedValue + "' AND INWARD='" + chk + "' AND INWARDACTIVE='" + chk1 + "' AND OUTWARD='T'  ";
                    DataSet ds1 = Utility.ExecuteSelectQuery(sel, "ASPTBLBUYSAMINW");
                    DataTable dtselect = ds1.Tables["ASPTBLBUYSAMINW"];
                    if (dtselect.Rows.Count != 0)
                    {
                        MessageBox.Show("Child Record Found", "Exception"); empty(); return;
                    }
                    else if (dtselect.Rows.Count != 0 && Convert.ToInt32("0" + txtsaminwardid.Text) == 0 || Convert.ToInt32("0" + txtsaminwardid.Text) == 0)
                    {
                       // if (txtsaminwardid.Text == "")
                       // {

                            string sel0 = "SELECT C.ASPTBLBUYSAMID, D.COMPCODE,E.asptblbrandmasID,E.BRAND,c.AGFSAMPLE,F.SEASON,C.FABRIC,F.ASPTBLSEASONMASID,G.ASPTBLSAMDEPTMASID,J.ASPTBLSAMCATMASID,C.GAUGE,C.GSM,C.STYLENAME,Q.ASPTBLSAMTYPEMASID,C.COLORNAME,C.COUNTS,C.SIZENAME, C.GARMENTIMAGE,C.ACTIVE,C.MFYEAR ,C.INWARD  FROM      ASPTBLBUYSAM C  JOIN GTCOMPMAST D ON c.COMPCODE=D.GTCOMPMASTID   join asptblbrandmas E ON E.ASPTBLBRANDMASID=C.BRAND   JOIN ASPTBLSEASONMAS F ON F.ASPTBLSEASONMASID=C.SEASON    JOIN ASPTBLSAMDEPTMAS G ON G.ASPTBLSAMDEPTMASID=C.DEPARTMENT     JOIN ASPTBLSAMCATMAS J ON J.ASPTBLSAMCATMASID=C.CATEGORY  JOIN ASPTBLSAMTYPEMAS Q ON Q.ASPTBLSAMTYPEMASID=C.SAMPLETYPE  where C.agfsample=" + Convert.ToInt64("0" + txtcode.Text);
                            DataSet ds0 = Utility.ExecuteSelectQuery(sel0, "ASPTBLBUYSAM");
                            DataTable dt0 = ds0.Tables["ASPTBLBUYSAM"];
                            string ins = "insert into ASPTBLBUYSAMINW(finyear,ASPTBLBUYSAMID,BRAND,SEASON,DEPARTMENT,CATEGORY,FABRIC,GAUGE,GSM,STYLENAME,SAMPLETYPE,COUNTS,SIZENAME,COLORNAME,AGFSAMPLE,  RACK,  BIN,  INDATE ,  INWARD,  INWARDACTIVE,  COMPCODE ,  USERNAME ,  CREATEDBY,  CREATEDON,  MODIFIEDBY,  IPADDRESS,OUTWARD,PCS,DATE2)VALUES('" + combofinyear.SelectedValue + "','" + grid + "','" + dt0.Rows[0]["asptblbrandmasID"].ToString() + "','" + dt0.Rows[0]["ASPTBLSEASONMASID"].ToString() + "','" + dt0.Rows[0]["ASPTBLSAMDEPTMASID"].ToString() + "','"+dt0.Rows[0]["ASPTBLSAMCATMASID"].ToString()+"','" + dt0.Rows[0]["FABRIC"].ToString() + "','" + dt0.Rows[0]["GAUGE"].ToString() + "','" + dt0.Rows[0]["GSM"].ToString() + "','" + combostyle.Text + "','" + dt0.Rows[0]["ASPTBLSAMTYPEMASID"].ToString() + "','" + combocounts.Text + "','" + combosize.Text + "','" + combocolor.Text + "','" + txtcode.Text + "','" + comborack.SelectedValue + "','" + combobin.SelectedValue + "','" + txtdate.Text + "','" + chk + "','" + chk1 + "','" + combocompcode.SelectedValue + "','" + Class.Users.USERID + "','" + Class.Users.HUserName + "','" + System.DateTime.Now.ToString() + "','" + System.DateTime.Now.ToString() + "','" + Class.Users.IPADDRESS + "','T','1',to_Date('" + System.DateTime.Now.ToString("dd-MM-yyyy") + "','dd-MM-yyyy') )";
                            Utility.ExecuteNonQuery(ins);
                            string up = "update asptblbuysam set INWARD='T' ,DATE2=to_Date('" + System.DateTime.Now.ToString("dd-MM-yyyy") + "','dd-MM-yyyy') WHERE asptblbuysamid=" + grid;
                            Utility.ExecuteNonQuery(up);
                            MessageBox.Show("Record Saved Saved Successfully", "Success");
                        GridLoad(dateTimePicker1.Value.ToString("dd-MM-yyyy")); empty();
                        //}

                    

                        Cursor = Cursors.Default;

                    }
                    else
                    {
                        string sel0 = "SELECT C.ASPTBLBUYSAMID, D.COMPCODE,E.asptblbrandmasID,E.BRAND,c.AGFSAMPLE,F.SEASON,C.FABRIC,F.ASPTBLSEASONMASID,G.ASPTBLSAMDEPTMASID,J.ASPTBLSAMCATMASID,C.GAUGE,C.GSM,C.STYLENAME,Q.ASPTBLSAMTYPEMASID,C.COLORNAME,C.COUNTS,C.SIZENAME, C.GARMENTIMAGE,C.ACTIVE,C.MFYEAR ,C.INWARD  FROM      ASPTBLBUYSAM C  JOIN GTCOMPMAST D ON c.COMPCODE=D.GTCOMPMASTID   join asptblbrandmas E ON E.ASPTBLBRANDMASID=C.BRAND   JOIN ASPTBLSEASONMAS F ON F.ASPTBLSEASONMASID=C.SEASON    JOIN ASPTBLSAMDEPTMAS G ON G.ASPTBLSAMDEPTMASID=C.DEPARTMENT     JOIN ASPTBLSAMCATMAS J ON J.ASPTBLSAMCATMASID=C.CATEGORY   JOIN ASPTBLSAMTYPEMAS Q ON Q.ASPTBLSAMTYPEMASID=C.SAMPLETYPE  where C.agfsample=" + Convert.ToInt64("0" + txtcode.Text);
                        DataSet ds0 = Utility.ExecuteSelectQuery(sel0, "ASPTBLBUYSAM");
                        DataTable dt0 = ds0.Tables["ASPTBLBUYSAM"];
                        string up = "update asptblbuysaminw set ASPTBLBUYSAMID='" + grid + "',BRAND='" + dt0.Rows[0]["asptblbrandmasID"].ToString() + "',SEASON='" + dt0.Rows[0]["ASPTBLSEASONMASID"].ToString() + "',DEPARTMENT='" + dt0.Rows[0]["ASPTBLSAMDEPTMASID"].ToString() + "'," +
                            "CATEGORY='" + dt0.Rows[0]["ASPTBLSAMCATMASID"].ToString() + "',FABRIC='" + dt0.Rows[0]["FABRIC"].ToString() + "',GAUGE='" + dt0.Rows[0]["GAUGE"].ToString() + "',GSM='" + dt0.Rows[0]["GSM"].ToString() + "',STYLENAME='" + combostyle.Text + "',SAMPLETYPE='" + dt0.Rows[0]["ASPTBLSAMTYPEMASID"].ToString() + "',COUNTS='" + combocounts.Text + "',SIZENAME='" + combosize.Text + "',COLORNAME='" + combocolor.Text + "',AGFSAMPLE='" + txtcode.Text + "',RACK='" + comborack.SelectedValue + "',BIN='" + combobin.SelectedValue + "',INDATE='" + txtdate.Text + "',INWARD='" + chk + "',INWARDACTIVE='" + chk1 + "'," +
                            "COMPCODE='" + combocompcode.SelectedValue + "',USERNAME='" + Class.Users.USERID + "',CREATEDBY='" + Class.Users.HUserName + "',CREATEDON='" + System.DateTime.Now.ToString() + "',MODIFIEDBY='" + System.DateTime.Now.ToString() + "',IPADDRESS='" + Class.Users.IPADDRESS + "' WHERE ASPTBLBUYSAMINWID=" + id;
                        Utility.ExecuteNonQuery(up);
                       
                        GridLoad(dateTimePicker1.Value.ToString("dd-MM-yyyy")); empty(); Cursor = Cursors.Default;
                        MessageBox.Show("Record Updated  Successfully", "Success");
                    }

                }
            }
            catch (Exception EX)
            {
                //string selchek1 = "select a.ASPTBLSESSIONMASID   from  ASPTBLSESSIONMAS a join gtcompmast  b on a.compcode=b.gtcompmastid join asptblusermas c on  c.compcode = a.compcode AND C.COMPCODE=B.GTCOMPMASTID  and A.USERNAME=C.USERID   and B.compcode='" + Class.Users.HCompcode + "'      and C.username='" + Class.Users.HUserName + "' and C.pasword = '" + Class.Users.PWORD + "' ";//and A.SYSTEMDATE = to_date('" + System.DateTime.Now.ToString("dd-MM-yyyy") + "', 'dd-MM-yyyy') and  C.active='T'
                //DataSet dschk1 = Utility.ExecuteSelectQuery(selchek1, "ASPTBLSESSIONMAS");
                //DataTable dtchk1 = dschk1.Tables["ASPTBLSESSIONMAS"];

                //if (dtchk1.Rows.Count > 0)
                //{
                //    Class.Users.SessionID = Convert.ToInt32(dtchk1.Rows[0]["ASPTBLSESSIONMASID"].ToString());
                //    string del = "delete from  ASPTBLSESSIONMAS a where a.USERNAME='" + Class.Users.USERID + "' AND  a.PASWORD='" + Class.Users.PWORD + "'";
                //    Utility.ExecuteNonQuery(del);

                //    Application.Exit();
                //}
                //else
                //{
                //    return;
                //}
                //MessageBox.Show(EX.ToString());

            }

        }

       


        public void Prints()
        {
            if (txtsaminwardid.Text != "")
            {

                Report.BuyerSampleReport ssm = new Report.BuyerSampleReport(Convert.ToInt32(txtsaminwardid.Text));
                ssm.Show();

            }
        }
        public void Imports()
        {
            Report.QrCode rq = new Report.QrCode();
            rq.Show();
        }

        private void RefreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridLoad();
            empty(); Cursor = Cursors.Default;
            usercheck(Class.Users.HCompcode, Class.Users.HUserName, Class.Users.ScreenName);

        }

        public void Exit()
        {
            GlobalVariables.MdiPanel.Show();
            News();
            GlobalVariables.HeaderName.Text = "";
            GlobalVariables.TabCtrl.TabPages.RemoveAt(GlobalVariables.TabCtrl.SelectedIndex);
            this.Hide();
        }







        public void Searchs()
        {

        }

        public void Deletes()
        {
            if (txtsaminwardid.Text != "")
            {
                string sel1 = "select a.agfsample from asptblbuysamout a where a.asptblbuysaminwid='" + txtsaminwardid.Text + "'";
                DataSet ds = Utility.ExecuteSelectQuery(sel1, "asptblbuysaminw");
                DataTable dt = ds.Tables["asptblbuysaminw"];
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("Child Record Found.Can Not Delete." + txtcode.Text, " Alert Message ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }
                else
                {

                    string del = "delete from asptblbuysaminw where asptblbuysaminwid='" + Convert.ToInt64("0" + txtsaminwardid.Text) + "'";
                    Utility.ExecuteNonQuery(del);
                    string up = "update asptblbuysam set INWARD='' WHERE AGFSAMPLE=" + txtcode.Text;
                    Utility.ExecuteNonQuery(up);
                    MessageBox.Show("Record Deleted Successfully " + txtsaminwardid.Text, " Delete Message ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                   
                    GridLoad(); empty();
                }
            }
            else
            {
                MessageBox.Show("Invalid ", " Delete Message ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
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

       

       
        private void txtsearch_TextChanged(object sender, EventArgs e)
        {
            try
            {

                if (txtsearch.Text.Length >= 5)
                {
                    listviewbuyqrcode.Items.Clear(); listfilter.Items.Clear();
                    string sel0 = "SELECT A.ASPTBLBUYSAMINWID,A.AGFSAMPLE,B.STYLENAME,B.SAMPLETYPE,F.BRAND,G.SEASON, D.RACK,E.BIN,A.INDATE,B.GARMENTIMAGE,B.GSM,B.COUNTS FROM ASPTBLBUYSAMINW A JOIN ASPTBLBUYSAM B ON A.AGFSAMPLE=B.AGFSAMPLE           JOIN GTCOMPMAST C ON C.GTCOMPMASTID=A.COMPCODE    JOIN ASPTBLRACKMAS D ON D.ASPTBLRACKMASID=A.RACK  JOIN ASPTBLBINMAS E ON E.ASPTBLBINMASID=A.BIN   JOIN ASPTBLBRANDMAS F ON F.ASPTBLBRANDMASID=A.BRAND  JOIN ASPTBLSEASONMAS G ON G.ASPTBLSEASONMASID=A.SEASON  JOIN ASPTBLSAMTYPEMAS Q ON Q.ASPTBLSAMTYPEMASID=B.SAMPLETYPE AND  Q.ASPTBLSAMTYPEMASID=A.SAMPLETYPE where B.STYLENAME like'%" + txtsearch.Text + "%' OR a.agfsample like'%" + txtsearch.Text + "%' AND A.INWARD='T'  order by a.ASPTBLBUYSAMINWID DESC";
                    DataSet ds0 = Utility.ExecuteSelectQuery(sel0, "ASPTBLBUYSAMINW");
                    DataTable dt = ds0.Tables["ASPTBLBUYSAMINW"];

                    if (dt.Rows.Count >= 0)
                    {

                        int i = 0; int j = 1; listviewbuyqrcode.BeginUpdate();
                        foreach (DataRow myRow in dt.Rows)
                        {
                            ListViewItem list = new ListViewItem();

                            list.SubItems.Add(j.ToString());
                            list.SubItems.Add(j.ToString());
                            list.SubItems.Add(myRow["ASPTBLBUYSAMINWID"].ToString());
                            list.SubItems.Add(myRow["AGFSAMPLE"].ToString());
                            list.SubItems.Add(myRow["BRAND"].ToString());
                            list.SubItems.Add(myRow["SEASON"].ToString());
                            list.SubItems.Add(myRow["STYLENAME"].ToString());
                            list.SubItems.Add(myRow["GSM"].ToString());
                            list.SubItems.Add(myRow["COUNTS"].ToString());
                            list.SubItems.Add(myRow["RACK"].ToString());
                            list.SubItems.Add(myRow["BIN"].ToString());
                            list.SubItems.Add(myRow["INDATE"].ToString());
                            //bytes = (byte[])myRow["GARMENTIMAGE"];
                            //Image img = Models.Device.ByteArrayToImage(bytes);
                            //imageList1.Images.Add(img);
                            //list.ImageIndex = i;
                            listviewbuyqrcode.Items.Add(list);
                            listfilter.Items.Add((ListViewItem)list.Clone());
                            if (j % 2 == 0)
                            {
                                list.BackColor = Color.White;

                            }
                            else
                            {
                                list.BackColor = Color.WhiteSmoke;


                            }
                            i++; j++;
                            lbltotal.Text = "Total Rows Count:  " + listviewbuyqrcode.Items.Count.ToString();
                            bytes = null;
                        }
                        listviewbuyqrcode.EndUpdate(); listviewbuyqrcode.Refresh();
                    }
                    else
                    {

                        lbltotal.Text = "Total Rows Count:  " + listviewbuyqrcode.Items.Count.ToString();
                    }
                }
                else
                {
                    
                }
            }
            catch (Exception ex)
            {
                // MessageBox.Show("---" + ex.ToString());
            }
        }

        

       
       
       
        private void SampleInwardEntry_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control == true && e.KeyCode == Keys.S)
            {
                Saves();
            }

        }    
        private void comboseason_Click(object sender, EventArgs e)
        {
            comboseason.BackColor = Color.White;
        }
        string code = "";

        private void txtqrcode_TextChanged(object sender, EventArgs e)
        {
            DataTable dt0 = new DataTable();
            code = "";
            if (checkscan.Checked == true)
            {
                if (txtqrcode.Text.Length >= 5)
                {
                    string sel0 = "SELECT C.ASPTBLBUYSAMID, D.COMPCODE,E.BRAND,c.AGFSAMPLE,F.SEASON,G.DEPARTMENT,J.CATEGORY,C.STYLENAME,Q.SAMPLETYPE,C.COLORNAME,C.COUNTS,C.SIZENAME, C.GARMENTIMAGE,C.ACTIVE,C.MFYEAR ,C.INWARD  FROM      ASPTBLBUYSAM C  JOIN GTCOMPMAST D ON c.COMPCODE=D.GTCOMPMASTID   join asptblBRANDMAS E ON E.ASPTBLBRANDMASID=C.BRAND   JOIN ASPTBLSEASONMAS F ON F.ASPTBLSEASONMASID=C.SEASON    JOIN ASPTBLSAMDEPTMAS G ON G.ASPTBLSAMDEPTMASID=C.DEPARTMENT     JOIN ASPTBLSAMCATMAS J ON J.ASPTBLSAMCATMASID=C.CATEGORY  JOIN ASPTBLSAMTYPEMAS Q ON Q.ASPTBLSAMTYPEMASID=C.SAMPLETYPE      where C.agfsample=" + Convert.ToInt64("0" + txtqrcode.Text);
                    DataSet ds0 = Utility.ExecuteSelectQuery(sel0, "ASPTBLBUYSAM");
                    dt0 = ds0.Tables["ASPTBLBUYSAM"];
                    code = txtqrcode.Text;
                }
            }
            if (txtqrcode.Text.Length >= 12)
            {

                code = sm.Decrypt(txtqrcode.Text);

                string sel0 = "SELECT C.ASPTBLBUYSAMID, D.COMPCODE,E.BRAND,c.AGFSAMPLE,F.SEASON,G.DEPARTMENT,J.CATEGORY,C.STYLENAME,Q.SAMPLETYPE,C.COLORNAME,C.COUNTS,C.SIZENAME, C.GARMENTIMAGE,C.ACTIVE,C.MFYEAR ,C.INWARD  FROM      ASPTBLBUYSAM C  JOIN GTCOMPMAST D ON c.COMPCODE=D.GTCOMPMASTID   join asptblBRANDMAS E ON E.ASPTBLBRANDMASID=C.BRAND   JOIN ASPTBLSEASONMAS F ON F.ASPTBLSEASONMASID=C.SEASON    JOIN ASPTBLSAMDEPTMAS G ON G.ASPTBLSAMDEPTMASID=C.DEPARTMENT     JOIN ASPTBLSAMCATMAS J ON J.ASPTBLSAMCATMASID=C.CATEGORY  JOIN ASPTBLSAMTYPEMAS Q ON Q.ASPTBLSAMTYPEMASID=C.SAMPLETYPE      where C.agfsample=" + Convert.ToInt64("0" + code);
                DataSet ds0 = Utility.ExecuteSelectQuery(sel0, "ASPTBLBUYSAM");
                dt0 = ds0.Tables["ASPTBLBUYSAM"];

            }

            if (dt0.Rows.Count > 0)
            {
                if (dt0.Rows[0]["INWARD"].ToString() == "")
                {
                    myString = "";
                    txtbuysamid.Text = dt0.Rows[0]["ASPTBLBUYSAMID"].ToString();
                    txtdate.Text = System.DateTime.Now.ToString("dd-MM-yyyy");
                    combocompcode.Text = dt0.Rows[0]["COMPCODE"].ToString();
                    combobrand.Text = dt0.Rows[0]["BRAND"].ToString();
                    txtcode.Text = dt0.Rows[0]["AGFSAMPLE"].ToString();
                    comboseason.Text = dt0.Rows[0]["SEASON"].ToString();
                    combodept.Text = dt0.Rows[0]["DEPARTMENT"].ToString();
                    combocategory.Text = dt0.Rows[0]["CATEGORY"].ToString();
                    combostyle.Text = dt0.Rows[0]["STYLENAME"].ToString();
                    combosampletype.Text = dt0.Rows[0]["SAMPLETYPE"].ToString();
                    combosize.Text = dt0.Rows[0]["SIZENAME"].ToString();
                    combocounts.Text = dt0.Rows[0]["COUNTS"].ToString();
                    combocolor.Text = dt0.Rows[0]["COLORNAME"].ToString();
                    combomfyear.Text = dt0.Rows[0]["MFYEAR"].ToString();
                    bytes = (byte[])dt0.Rows[0]["GARMENTIMAGE"];
                    Image img = Models.Device.ByteArrayToImage(bytes);
                    picturegarmentimage.Image = img;
                    string sel3 = "SELECT max(B.ASPTBLBUYSAMINWID) AS ASPTBLBUYSAMINWID FROM    ASPTBLBUYSAMINW B  where B.agfsample=" + Convert.ToInt64("0" + code);
                    DataSet ds3 = Utility.ExecuteSelectQuery(sel3, "ASPTBLBUYSAMINW");
                    DataTable dt3 = ds3.Tables["ASPTBLBUYSAMINW"];
                    if (dt3.Rows.Count > 0)
                    {
                        string sel4 = "SELECT H.RACK,I.BIN,B.OUTWARD FROM    ASPTBLBUYSAMINW B JOIN  ASPTBLBUYSAM C ON C.AGFSAMPLE=B.AGFSAMPLE  JOIN GTCOMPMAST D ON B.COMPCODE=D.GTCOMPMASTID  join asptblBRANDMAS E ON E.ASPTBLBRANDMASID=C.BRAND   JOIN ASPTBLSEASONMAS F ON F.ASPTBLSEASONMASID=C.SEASON   JOIN ASPTBLSAMDEPTMAS G ON G.ASPTBLSAMDEPTMASID=C.DEPARTMENT     JOIN ASPTBLRACKMAS H ON H.ASPTBLRACKMASID=B.RACK JOIN ASPTBLBINMAS I ON I.ASPTBLBINMASID=B.BIN JOIN ASPTBLSAMCATMAS J ON J.ASPTBLSAMCATMASID=C.CATEGORY JOIN ASPTBLSAMTYPEMAS Q ON Q.ASPTBLSAMTYPEMASID=C.SAMPLETYPE AND  Q.ASPTBLSAMTYPEMASID=B.SAMPLETYPE  where B.ASPTBLBUYSAMINWID='" + dt3.Rows[0]["ASPTBLBUYSAMINWID"].ToString() + "'";
                        DataSet ds4 = Utility.ExecuteSelectQuery(sel4, "ASPTBLBUYSAMout");
                        DataTable dt4 = ds4.Tables["ASPTBLBUYSAMout"];

                        if (dt4.Rows.Count > 0)
                        {
                            txtprerack.Text = dt4.Rows[0]["RACK"].ToString();
                            txtprebin.Text = dt4.Rows[0]["BIN"].ToString();
                        }
                    }
                    txtqrcode.Text = ""; comborack.Select(); comborack.BackColor = Color.Yellow;
                }

                else
                {
                    if (code != "")
                    {
                        string sel2 = "SELECT distinct a.agfsample,a.outward FROM  ASPTBLBUYSAMout A where A.agfsample=" + Convert.ToInt64("0" + code);
                        DataSet ds2 = Utility.ExecuteSelectQuery(sel2, "ASPTBLBUYSAMout");
                        DataTable dt2 = ds2.Tables["ASPTBLBUYSAMout"];
                        if (Convert.ToInt64(dt2.Rows.Count) <= 0)
                        {
                            string sel1 = "SELECT C.RACK,D.BIN FROM  ASPTBLBUYSAMINW A JOIN ASPTBLBUYSAM B ON  A.AGFSAMPLE=B.AGFSAMPLE  JOIN ASPTBLRACKMAS C ON A.RACK=C.ASPTBLRACKMASID JOIN ASPTBLBINMAS D ON A.BIN=D.ASPTBLBINMASID where A.agfsample=" + Convert.ToInt64("0" + buy.ASPTBLBUYSAMID);
                            DataSet ds1 = Utility.ExecuteSelectQuery(sel1, "ASPTBLBUYSAMINW");
                            DataTable dt1 = ds1.Tables["ASPTBLBUYSAMINW"];
                            if (Convert.ToInt64(dt1.Rows.Count) > 0)
                            {
                                txtprerack.Text = dt1.Rows[0]["RACK"].ToString();
                                txtprebin.Text = dt1.Rows[0]["BIN"].ToString(); txtqrcode.Text = ""; comborack.Select(); comborack.BackColor = Color.Yellow;
                                MessageBox.Show("This Sample already  This RackNo :" + dt1.Rows[0]["RACK"].ToString() + "  BIN  : " + dt1.Rows[0]["BIN"].ToString());
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("");
                        MessageBox.Show("This Sample '" + code + "' already Saved", "" + txtqrcode.Text + ""); empty(); txtqrcode.Text = ""; txtqrcode.Select();
                    }
                }
            }
            //else
            //{
            //    MessageBox.Show("");
            //    MessageBox.Show("This Ref Code    :  '" + code + "' already  Saved. ", "" + txtqrcode.Text + ""); empty(); txtqrcode.Text = ""; txtqrcode.Select();

            //}


        }

        private void butsearch_Click_1(object sender, EventArgs e)
        {
            GridLoad(dateTimePicker1.Value.ToString("dd-MM-yyyy"));
        }

        private void txtqrcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (e.KeyChar >= '0' && e.KeyChar <= '9' || e.KeyChar == (char)Keys.Back) //The  character represents a backspace
            //{
            //    e.Handled = false; //Do not reject the input
            //}
            //else
            //{
            //    e.Handled = true; //Reject the input
            //}
        }

        private void butsearch_Click(object sender, EventArgs e)
        {
            GridLoad(dateTimePicker1.Value.ToString("dd-MM-yyyy"));

        }

        private void comborack_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comborack.SelectedValue != null)
            {
                comborack.BackColor = Color.White;
                combobin.BackColor = Color.Yellow;
            }
        }

        private void combobin_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (combobin.SelectedValue != null)
            {
                combobin.BackColor = Color.White;
            }
        }

        private void checkscan_CheckedChanged(object sender, EventArgs e)
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
    }
}