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
    public partial class SampleIssueEntry : Form,ToolStripAccess
    {
        Models.Master mas = new Models.Master();
        Models.UserRights sm = new Models.UserRights();
        private static SampleIssueEntry _instance;
        Models.BuyerSample buy = new Models.BuyerSample();
        ListView listfilter = new ListView();
        //11754
        public static SampleIssueEntry Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new SampleIssueEntry();
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
        public SampleIssueEntry()
        {
            InitializeComponent();
            Class.Users.ScreenName = "SampleIssueEntry";
            GlobalVariables.HeaderName.Text = Class.Users.ScreenName; GlobalVariables.CurrentForm = this;
            Class.Users.IPADDRESS = GenFun.GetLocalIPAddress();
            Class.Users.CREATED = Convert.ToDateTime(System.DateTime.Now.ToString("dd-MMM-yyyy") + " " + System.DateTime.Now.ToLongTimeString());
            Class.Users.SysDate = Convert.ToString(System.DateTime.Now.ToString("dd-MM-yyyy"));
            Class.Users.SysTime = Convert.ToString(DateTime.Now.ToLongTimeString().ToString());
           
            butheader.BackColor = Class.Users.BackColors;
            butfooter.BackColor = Class.Users.BackColors;
            panel1.BackColor = Class.Users.BackColors;
            panel2.BackColor = Class.Users.BackColors;
        }
        public void ReadOnlys()
        {

        }
        public void BrandSearchLoad()
        {
            string sel = " SELECT 0 AS ASPTBLBRANDMASID , '' AS BRAND FROM DUAL UNION ALL SELECT DISTINCT A.ASPTBLBRANDMASID,  A.BRAND    FROM  ASPTBLBRANDMAS A   join asptblbuysam b on A.ASPTBLBRANDMASID=B.BRAND  WHERE A.ACTIVE='T' ";
            DataSet ds = Utility.ExecuteSelectQuery(sel, "ASPTBLBRANDMAS");
            DataTable dt = ds.Tables["ASPTBLBRANDMAS"];

            combobuyer.DataSource = dt;
            combobuyer.DisplayMember = "BRAND";
            combobuyer.ValueMember = "ASPTBLBRANDMASID";

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
        public void ResponsePersonLoad()
        {
            string sel = "select a.asptblresmasid,A.RESONSEPERSON from asptblresmas a where a.active='T' order by a.asptblresmasid desc";
            DataSet ds = Utility.ExecuteSelectQuery(sel, "asptblresmas");
            DataTable dt = ds.Tables["asptblresmas"];

            txtreceive.DisplayMember = "RESONSEPERSON";
            txtreceive.ValueMember = "asptblresmasid";
            txtreceive.DataSource = dt;

        }
        private void SampleIssueEntry_FormClosed(object sender, FormClosedEventArgs e)
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






        private void SampleIssueEntry_Load(object sender, EventArgs e)
        {
            BrandLoad(); compcode(); BrandSearchLoad();
            BrandLoad(); compcode(); SampleTypeLoad(); FinyearLoad(); ResponsePersonLoad();
            SeasonLoad(); DepartmentLoad(); rackload(); binload(); GridLoad(dateTimePicker1.Value.ToString("dd-MM-yyyy")); empty();  txtqrcode.BackColor = System.Drawing.Color.Beige;
            txtqrcode.Select(); Class.Users.UserTime = 0;
        }
        void rackload()
        {
            string sel = " SELECT A.ASPTBLRACKMASID,A.RACK    FROM  ASPTBLRACKMAS A    WHERE A.ACTIVE='T'  ";
            DataSet ds = Utility.ExecuteSelectQuery(sel, "ASPTBLRACKMAS");
            DataTable dt = ds.Tables["ASPTBLRACKMAS"];
            comborack.DataSource = dt;
            comborack.DisplayMember = "RACK";
            comborack.ValueMember = "ASPTBLRACKMASID";
        }
        void binload()
        {
            string sel = " SELECT A.asptblbinmasid,A.bin    FROM  asptblbinmas A    WHERE A.ACTIVE='T'  ";
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
                    Class.Users.UserTime = 0;
                    txtsamissueid.Text = Convert.ToString(listviewbuyqrcode.SelectedItems[0].SubItems[3].Text);
                    string sel0 = "SELECT  AA.ASPTBLBUYSAMOUTID,AA.ASPTBLBUYSAMINWID, AA.ASPTBLBUYSAMID,AA.OUTDATE, B.COMPCODE,C.BRAND,A.AGFSAMPLE, E.SEASON,F.DEPARTMENT,O.CATEGORY,A.STYLENAME,R.SAMPLETYPE,A.GARMENTIMAGE,A.ACTIVE,A.MFYEAR,P.RACK,AA.remarks, Q.BIN,AA.OUTWARD,AA.OUTWARDACTIVE,S.RESONSEPERSON AS RECEIVER,A.COUNTS,A.SIZENAME,A.COLORNAME ,T.BRAND AS BRAND1   FROM ASPTBLBUYSAMOUT  AA JOIN ASPTBLBUYSAM  A ON A.AGFSAMPLE=AA.AGFSAMPLE   JOIN GTCOMPMAST B ON A.COMPCODE=B.GTCOMPMASTID join asptblBRANDMAS C ON C.ASPTBLBRANDMASID=A.BRAND  JOIN ASPTBLSEASONMAS E ON E.ASPTBLSEASONMASID=A.SEASON    JOIN ASPTBLSAMDEPTMAS F ON F.ASPTBLSAMDEPTMASID=A.DEPARTMENT  JOIN ASPTBLSAMCATMAS O ON O.ASPTBLSAMCATMASID=A.CATEGORY        JOIN ASPTBLRACKMAS P ON P.ASPTBLRACKMASID=AA.RACK JOIN ASPTBLBINMAS Q ON Q.ASPTBLBINMASID=AA.BIN  JOIN ASPTBLSAMTYPEMAS R ON R.ASPTBLSAMTYPEMASID = A.SAMPLETYPE JOIN ASPTBLRESMAS  S ON S.ASPTBLRESMASID=AA.RECEIVER JOIN ASPTBLBRANDMAS T ON T.ASPTBLBRANDMASID=S.BRAND  where AA.ASPTBLBUYSAMOUTID=" + txtsamissueid.Text;
                    DataSet ds0 = Utility.ExecuteSelectQuery(sel0, "ASPTBLBUYSAM");
                    DataTable dt0 = ds0.Tables["ASPTBLBUYSAM"];
                    myString = "";

                    txtsaminwardid.Text = dt0.Rows[0]["ASPTBLBUYSAMINWID"].ToString();
                    txtbuysamid.Text = dt0.Rows[0]["ASPTBLBUYSAMID"].ToString();
                    txtdate.Text = dt0.Rows[0]["OUTDATE"].ToString();
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
                    txtremarks.Text = dt0.Rows[0]["remarks"].ToString();
                    if (dt0.Rows[0]["OUTWARD"].ToString() == "T") checkout.Checked = true; else checkout.Checked = false;
                    if (dt0.Rows[0]["OUTWARDACTIVE"].ToString() == "T") checkoutactive.Checked = true; else checkoutactive.Checked = false;

                    comborack.Text = dt0.Rows[0]["RACK"].ToString();
                    combobin.Text = dt0.Rows[0]["BIN"].ToString();
                    bytes = (byte[])dt0.Rows[0]["GARMENTIMAGE"];
                    Image img = Models.Device.ByteArrayToImage(bytes);
                    picturegarmentimage.Image = img;
                    combobuyer.Text= dt0.Rows[0]["brand1"].ToString();
                    txtreceive.Text = dt0.Rows[0]["RECEIVER"].ToString();
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }

        }

        private void empty()
        {
            Class.Users.UserTime = 0;txtremarks.Text = "";combobuyer.Text = "";
            txtsaminwardid.Text = ""; combocompcode.SelectedIndex = -1; txtdate.Text = "";
            combobrand.SelectedIndex = -1; combodept.SelectedIndex = -1;
            comboseason.SelectedIndex = -1; 
            combostyle.Text = ""; combomfyear.Text = ""; combocategory.SelectedIndex = -1; combocategory.Text = ""; txtcode.Text = "";
            combosampletype.Text = ""; comborack.SelectedIndex = -1; combobin.SelectedIndex = -1;
            txtbuysamid.Text = "";
            txtsaminwardid.Text = "";
            combosize.Text = "";
            combocounts.Text = "";
            combocolor.Text = "";
            txtsamissueid.Text = ""; combosampletype.Text = ""; combosampletype.SelectedIndex = -1;
             bytes = null;
            qrbytes = null;txtreceive.Text = "";
            picturegarmentimage.Image = null;
            txtreceive.BackColor = Color.White;
            butheader.BackColor = Class.Users.BackColors;
            butfooter.BackColor = Class.Users.BackColors;
            panel1.BackColor = Class.Users.BackColors;
            panel2.BackColor = Class.Users.BackColors;
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


        //public void SubStyleLoad(string sty)
        //{
        //    if (sty == "System.Data.DataRowView") { return; }
        //    else
        //    {
        //        string sel = "SELECT A.ASPTBLSUBSTYMASID,A.SUBSTYLE    FROM  ASPTBLSUBSTYMAS A join ASPTBLSTYMAS B  ON  A.STYLENAME=B.ASPTBLSTYMASID  WHERE A.ACTIVE='T' AND B.STYLENAME='" + sty + "'";
        //        DataSet ds = Utility.ExecuteSelectQuery(sel, "ASPTBLSUBSTYMAS");
        //        DataTable dt = ds.Tables["ASPTBLSUBSTYMAS"];

        //        combosubstyle.DataSource = dt;
        //        combosubstyle.DisplayMember = "SUBSTYLE";
        //        combosubstyle.ValueMember = "ASPTBLSUBSTYMASID";
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


                string sel0 = "SELECT A.ASPTBLBUYSAMOUTID,A.AGFSAMPLE,F.BRAND,B.STYLENAME,Q.SAMPLETYPE,g.season ,D.RACK,E.BIN,A.OUTDATE FROM ASPTBLBUYSAMOUT A JOIN ASPTBLBUYSAM B ON A.AGFSAMPLE=B.AGFSAMPLE   JOIN GTCOMPMAST C ON C.GTCOMPMASTID=A.COMPCODE    JOIN ASPTBLRACKMAS D ON D.ASPTBLRACKMASID=A.RACK  JOIN ASPTBLBINMAS E ON E.ASPTBLBINMASID=A.BIN JOIN ASPTBLBRANDMAS F ON F.ASPTBLBRANDMASID=A.BRAND         JOIN ASPTBLSEASONMAS G ON G.ASPTBLSEASONMASID=A.SEASON  JOIN ASPTBLSAMTYPEMAS Q ON Q.ASPTBLSAMTYPEMASID=B.SAMPLETYPE where A.OUTWARD='T'  order by a.ASPTBLBUYSAMOUTID desc";
                DataSet ds0 = Utility.ExecuteSelectQuery(sel0, "ASPTBLBUYSAMOUT");
                DataTable dt = ds0.Tables["ASPTBLBUYSAMOUT"];
                if (dt.Rows.Count >= 0)
                {
                    int i = 0; int j = 1; listviewbuyqrcode.BeginUpdate();
                    foreach (DataRow myRow in dt.Rows)
                    {
                        ListViewItem list = new ListViewItem();
                        list.SubItems.Add(j.ToString());
                        list.SubItems.Add(j.ToString());
                        list.SubItems.Add(myRow["ASPTBLBUYSAMOUTID"].ToString());
                        list.SubItems.Add(myRow["AGFSAMPLE"].ToString());
                        list.SubItems.Add(myRow["BRAND"].ToString());
                        list.SubItems.Add(myRow["STYLENAME"].ToString());
                        list.SubItems.Add(myRow["SEASON"].ToString());      
                        list.SubItems.Add(myRow["SAMPLETYPE"].ToString());
                        list.SubItems.Add(myRow["RACK"].ToString());
                        list.SubItems.Add(myRow["BIN"].ToString());
                        list.SubItems.Add(myRow["OUTDATE"].ToString());
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
                string sel0 = "SELECT A.ASPTBLBUYSAMOUTID,A.AGFSAMPLE,F.BRAND,B.STYLENAME,Q.SAMPLETYPE,g.season ,D.RACK,E.BIN,A.OUTDATE,I.RESONSEPERSON FROM ASPTBLBUYSAMOUT A JOIN ASPTBLBUYSAM B ON A.AGFSAMPLE=B.AGFSAMPLE   JOIN GTCOMPMAST C ON C.GTCOMPMASTID=A.COMPCODE    JOIN ASPTBLRACKMAS D ON D.ASPTBLRACKMASID=A.RACK  JOIN ASPTBLBINMAS E ON E.ASPTBLBINMASID=A.BIN JOIN ASPTBLBRANDMAS F ON F.ASPTBLBRANDMASID=A.BRAND         JOIN ASPTBLSEASONMAS G ON G.ASPTBLSEASONMASID=A.SEASON  JOIN ASPTBLSAMTYPEMAS Q ON Q.ASPTBLSAMTYPEMASID=B.SAMPLETYPE   join ASPTBLRESMAS i on I.ASPTBLRESMASID=A.RECEIVER where a.OUTDATE='" + date + "' AND A.OUTWARD='T'   order by a.ASPTBLBUYSAMOUTID desc";

                DataSet ds0 = Utility.ExecuteSelectQuery(sel0, "ASPTBLBUYSAMOUT");
                DataTable dt = ds0.Tables["ASPTBLBUYSAMOUT"];

                if (dt.Rows.Count >= 0)
                {

                    int i = 0; int j = 1;
                    foreach (DataRow myRow in dt.Rows)
                    {
                        ListViewItem list = new ListViewItem();

                        list.SubItems.Add(j.ToString());
                        list.SubItems.Add(j.ToString());
                        list.SubItems.Add(myRow["ASPTBLBUYSAMOUTID"].ToString());
                        list.SubItems.Add(myRow["AGFSAMPLE"].ToString());
                        list.SubItems.Add(myRow["BRAND"].ToString());
                        list.SubItems.Add(myRow["STYLENAME"].ToString());
                        list.SubItems.Add(myRow["SEASON"].ToString());

                        list.SubItems.Add(myRow["SAMPLETYPE"].ToString());
                        list.SubItems.Add(myRow["RACK"].ToString());
                        list.SubItems.Add(myRow["BIN"].ToString());
                        list.SubItems.Add(myRow["OUTDATE"].ToString());
                       list.SubItems.Add(myRow["RESONSEPERSON"].ToString());
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
            comborack.Enabled = false; combobin.Enabled = false;
        }
        private void readtrue()
        {
            txtsaminwardid.Enabled = true;
            combocompcode.Enabled = true;
            combobrand.Enabled = true;
            combodept.Enabled = true;
            comboseason.Enabled = true;
            combostyle.Enabled = true; txtcode.Enabled = true;
            combosampletype.Enabled = true; combomfyear.Enabled = true; comborack.Enabled = true; combobin.Enabled = true;
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
        private bool check()
        {
            if (combocompcode.Text == "") { combocompcode.BackColor = System.Drawing.Color.Beige; combocompcode.Select(); MessageBox.Show("Reference  Code  is Empty ", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Information); return false; }
            if (combobrand.Text == "") { combobrand.BackColor = System.Drawing.Color.Beige; combobrand.Select(); MessageBox.Show("Reference  Code  is Empty ", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Information); return false; }
            if (combocategory.Text == "") { combocategory.BackColor = System.Drawing.Color.Beige; combocategory.Select(); MessageBox.Show("Reference  Code  is Empty ", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Information); return false; }
            if (comboseason.Text == "") { comboseason.BackColor = System.Drawing.Color.Beige; comboseason.Select(); MessageBox.Show("Reference  Code is Empty ", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Information); return false; }
            if (combodept.Text == "") { combodept.BackColor = System.Drawing.Color.Beige; combodept.Select(); MessageBox.Show("Reference  Code  is Empty ", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Information); return false; }
            if (combostyle.Text == "") { combostyle.BackColor = System.Drawing.Color.Beige; combostyle.Select(); MessageBox.Show("Reference  Code  is Empty ", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Information); return false; }
            if (comborack.Text == "") { comborack.BackColor = System.Drawing.Color.Beige; comborack.Select(); MessageBox.Show("Reference  Code  is Empty ", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Information); return false; }
            if (combobin.Text == "") { combobin.BackColor = System.Drawing.Color.Beige; combobin.Select(); MessageBox.Show("Reference  Code  is Empty ", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Information); return false; }
            if (txtreceive.Text == "") { txtreceive.BackColor = System.Drawing.Color.Beige; txtreceive.Focus(); MessageBox.Show("Receiver Name is Empty ", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Information); return false; }
            if (combobuyer.Text == "") { combobuyer.BackColor = System.Drawing.Color.Beige; combobuyer.Focus(); MessageBox.Show("Buyer Name is Empty ", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Information); return false; }

            //if (txtremarks.Text == "" || txtremarks.Text == null) { txtreceive.BackColor = System.Drawing.Color.Beige; txtremarks.Focus(); MessageBox.Show("Remarks is Empty ", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Information); return false; }

            return true;
        }
        public void Saves()
        {
            try
            {

                if (check())
                {

                    int buysamid = Convert.ToInt32("0" + txtbuysamid.Text);
                    int inid = Convert.ToInt32("0" + txtsaminwardid.Text);
                    int id = Convert.ToInt32("0" + txtsamissueid.Text);
                    OracleCommand ascmd; string chk1 = "";
                    if (checkout.Checked) chk = "T"; else chk = "F"; if (checkoutactive.Checked) chk1 = "T"; else chk1 = "F";
                  
                    if(txtsamissueid.Text == "") {

                        string sel0 = "SELECT '" + buysamid + "'  as ASPTBLBUYSAMID,'" + inid+ "'  as  ASPTBLBUYSAMINWID,E.asptblbrandmasID AS BRAND," +
                            "F.ASPTBLSEASONMASID AS SEASON,G.ASPTBLSAMDEPTMASID AS DEPARTMENT,J.ASPTBLSAMCATMASID AS CATEGORY,C.FABRIC,C.GAUGE,C.GSM," +
                            "C.STYLENAME,Q.ASPTBLSAMTYPEMASID AS SAMPLETYPE,C.COUNTS,C.SIZENAME,C.COLORNAME,c.AGFSAMPLE," +
                            "C.SUBSTYLE, C.GARMENTIMAGE,C.ACTIVE,C.MFYEAR ,C.INWARD, D.COMPCODE  FROM   " +
                            "   ASPTBLBUYSAM C  JOIN GTCOMPMAST D ON c.COMPCODE=D.GTCOMPMASTID   join asptblbrandmas E ON E.ASPTBLBRANDMASID=C.BRAND  " +
                            " JOIN ASPTBLSEASONMAS F ON F.ASPTBLSEASONMASID=C.SEASON    JOIN ASPTBLSAMDEPTMAS G ON G.ASPTBLSAMDEPTMASID=C.DEPARTMENT     JOIN ASPTBLSAMCATMAS J ON J.ASPTBLSAMCATMASID=C.CATEGORY  JOIN ASPTBLSAMTYPEMAS Q ON Q.ASPTBLSAMTYPEMASID=C.SAMPLETYPE   where C.agfsample=" + Convert.ToInt64("0" + txtcode.Text);
                        DataSet ds0 = Utility.ExecuteSelectQuery(sel0, "ASPTBLBUYSAM");
                         DataTable dt0 = ds0.Tables["ASPTBLBUYSAM"];
                        if (dt0.Rows.Count > 0)
                        {
                            string ins = "insert into ASPTBLBUYSAMOUT(finyear,ASPTBLBUYSAMID,ASPTBLBUYSAMINWID,BRAND,SEASON,DEPARTMENT,CATEGORY,FABRIC," +
                             "GAUGE,GSM,STYLENAME,SAMPLETYPE,COUNTS,SIZENAME,COLORNAME,AGFSAMPLE,  RACK,  BIN,  OUTDATE ,  OUTWARD,  OUTWARDACTIVE, " +
                             " COMPCODE ,  USERNAME ,  CREATEDBY,  CREATEDON,  MODIFIEDBY,  IPADDRESS,RECEIVER,PCS,DATE2,remarks,brand1)VALUES('" + combofinyear.SelectedValue + "','" + dt0.Rows[0]["ASPTBLBUYSAMID"].ToString() + "','" + dt0.Rows[0]["ASPTBLBUYSAMINWID"].ToString() + "','" + dt0.Rows[0]["BRAND"].ToString() + "','" + dt0.Rows[0]["SEASON"].ToString() + "','" + dt0.Rows[0]["DEPARTMENT"].ToString() + "','" + dt0.Rows[0]["CATEGORY"].ToString() + "','" + dt0.Rows[0]["FABRIC"].ToString() + "','" + dt0.Rows[0]["GAUGE"].ToString() + "','" + dt0.Rows[0]["GSM"].ToString() + "','" + combostyle.Text + "','" + dt0.Rows[0]["SAMPLETYPE"].ToString() + "','" + combocounts.Text + "','" + combosize.Text + "','" + combocolor.Text + "','" + txtcode.Text + "','" + comborack.SelectedValue + "','" + combobin.SelectedValue + "','" + txtdate.Text + "','" + chk + "','" + chk1 + "','" + combocompcode.SelectedValue + "','" + Class.Users.USERID + "','" + Class.Users.HUserName + "','" + System.DateTime.Now.ToString() + "','" + System.DateTime.Now.ToString() + "','" + Class.Users.IPADDRESS + "','" + txtreceive.SelectedValue + "','1',to_Date('" + System.DateTime.Now.ToString("dd-MM-yyyy") + "','dd-MM-yyyy'),'"+combobuyer.Text+"   "+txtremarks.Text.Trim().ToUpper()+"','"+combobuyer.SelectedValue+"')";
                            Utility.ExecuteNonQuery(ins);
                            string up = "update asptblbuysam set INWARD='' WHERE AGFSAMPLE=" + txtcode.Text;
                            Utility.ExecuteNonQuery(up);
                            string sel = "SELECT MAX(ASPTBLBUYSAMINWID) AS ASPTBLBUYSAMINWID FROM  ASPTBLBUYSAMINW  where    AGFSAMPLE='" + txtcode.Text + "'";
                            DataSet ds1 = Utility.ExecuteSelectQuery(sel, "ASPTBLBUYSAMINW");
                            DataTable dtselect = ds1.Tables["ASPTBLBUYSAMINW"];
                            if (dtselect.Rows.Count > 0)
                            {
                                string up1 = "update ASPTBLBUYSAMINW set OUTWARD='F' WHERE ASPTBLBUYSAMINWID='" + dtselect.Rows[0]["ASPTBLBUYSAMINWID"].ToString() + "'";
                                Utility.ExecuteNonQuery(up1);

                                MessageBox.Show("Record Saved Saved Successfully", "Success");
                                GridLoad(txtdate.Text); empty();
                            }
                        }
                    }
                    else
                    {
                        string up = "UPDATE   ASPTBLBUYSAMOUT SET SAMPLETYPE='" + combosampletype.SelectedValue + "', RECEIVER='" + txtreceive.SelectedValue + "',brand1='" + combobuyer.SelectedValue + "', remarks='" + combobuyer.Text+"  "+txtremarks.Text.Trim().ToUpper() + "' WHERE ASPTBLBUYSAMOUTID='" + txtsamissueid.Text + "'";
                        Utility.ExecuteNonQuery(up);
                        //string up = "UPDATE   ASPTBLBUYSAMOUT SET SAMPLETYPE=:SAMPLETYPE, RECEIVER=:RECEIVER WHERE ASPTBLBUYSAMOUTID=:ASPTBLBUYSAMOUTID";
                        //ascmd = new OracleCommand(up, Utility.con);


                        //ascmd.Parameters.Add(":SAMPLETYPE", combosampletype.SelectedValue);
                        //ascmd.Parameters.Add(":RECEIVER", txtreceive.Text);
                        //ascmd.Parameters.Add(":ASPTBLBUYSAMOUTID", txtsamissueid.Text);
                        //ascmd.ExecuteNonQuery();

                        string up1 = "update asptblbuysam set INWARD='',OUTWARD='T' WHERE AGFSAMPLE=" + txtcode.Text;
                        Utility.ExecuteNonQuery(up1);
                        string up2 = "update asptblbuysamINW set OUTWARD='F' WHERE asptblbuysamINWID=" + txtsamissueid.Text;
                        Utility.ExecuteNonQuery(up2);
                        MessageBox.Show("Record Updated Saved Successfully", "Success"); GridLoad(txtdate.Text); empty();

                    }

                        Cursor = Cursors.Default;

                }

            }
            catch (Exception EX)
            {
                
                MessageBox.Show(EX.ToString());
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
            Cursor = Cursors.WaitCursor;
            GridLoad(); ResponsePersonLoad();
             Cursor = Cursors.Default;
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
            if (txtsamissueid.Text != "")
            {
               

                    string del = "delete from ASPTBLBUYSAMOUT where ASPTBLBUYSAMOUTID='" + Convert.ToInt64("0" + txtsamissueid.Text) + "'";
                    Utility.ExecuteNonQuery(del);
                string up = "update asptblbuysam set INWARD='' WHERE AGFSAMPLE=" + txtcode.Text;
                Utility.ExecuteNonQuery(up);
                MessageBox.Show("Record Deleted Successfully " + txtsamissueid.Text, " Delete Message ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                GridLoad(); empty();
               
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
                    string sel0 = "SELECT A.ASPTBLBUYSAMOUTID,A.AGFSAMPLE,F.BRAND,B.STYLENAME,Q.SAMPLETYPE,g.season ,D.RACK,E.BIN,A.OUTDATE,B.GARMENTIMAGE,B.GSM,B.COUNTS,I.RESONSEPERSON FROM ASPTBLBUYSAMOUT A JOIN ASPTBLBUYSAM B ON A.AGFSAMPLE=B.AGFSAMPLE   JOIN GTCOMPMAST C ON C.GTCOMPMASTID=A.COMPCODE    JOIN ASPTBLRACKMAS D ON D.ASPTBLRACKMASID=A.RACK  JOIN ASPTBLBINMAS E ON E.ASPTBLBINMASID=A.BIN JOIN ASPTBLBRANDMAS F ON F.ASPTBLBRANDMASID=A.BRAND         JOIN ASPTBLSEASONMAS G ON G.ASPTBLSEASONMASID=A.SEASON  JOIN ASPTBLSAMTYPEMAS Q ON Q.ASPTBLSAMTYPEMASID=B.SAMPLETYPE   join ASPTBLRESMAS i on I.ASPTBLRESMASID=A.RECEIVER where  B.STYLENAME like'%" + txtsearch.Text + "%' OR a.agfsample like'%" + txtsearch.Text + "%' AND A.OUTWARD='T'  or i.RESONSEPERSON like'%" + txtsearch.Text + "%'  order by a.ASPTBLBUYSAMOUTID desc";
                    DataSet ds0 = Utility.ExecuteSelectQuery(sel0, "ASPTBLBUYSAMINW");
                    DataTable dt = ds0.Tables["ASPTBLBUYSAMINW"];
                    if (dt.Rows.Count >= 0)
                    {

                        int i = 0; int j = 1;
                        foreach (DataRow myRow in dt.Rows)
                        {
                            ListViewItem list = new ListViewItem();

                            list.SubItems.Add(j.ToString());
                            list.SubItems.Add(j.ToString());
                            list.SubItems.Add(myRow["ASPTBLBUYSAMOUTID"].ToString());
                            list.SubItems.Add(myRow["AGFSAMPLE"].ToString());
                            list.SubItems.Add(myRow["BRAND"].ToString());
                            list.SubItems.Add(myRow["STYLENAME"].ToString());
                            list.SubItems.Add(myRow["SEASON"].ToString());

                            list.SubItems.Add(myRow["SAMPLETYPE"].ToString());
                            list.SubItems.Add(myRow["RACK"].ToString());
                            list.SubItems.Add(myRow["BIN"].ToString());
                            list.SubItems.Add(myRow["OUTDATE"].ToString());
                            list.SubItems.Add(myRow["RESONSEPERSON"].ToString());
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
                        GridLoad(dateTimePicker1.Value.ToString("dd-MM-yyyy"));
                        lbltotal.Text = "Total Rows Count:  " + listviewbuyqrcode.Items.Count.ToString();
                    }
                }
                else
                {
                  
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            //try
            //{
            //    int item0 = 0;
            //    if (txtsearch.Text.Length > 0)
            //    {
            //        listviewbuyqrcode.Items.Clear();
            //        foreach (ListViewItem item in listfilter.Items)
            //        {
            //            ListViewItem list = new ListViewItem();
            //            if (listfilter.Items[item0].SubItems[3].ToString().Contains(txtsearch.Text) || listfilter.Items[item0].SubItems[4].ToString().Contains(txtsearch.Text) || listfilter.Items[item0].SubItems[5].ToString().Contains(txtsearch.Text) || listfilter.Items[item0].SubItems[6].ToString().Contains(txtsearch.Text))
            //            {
            //                list.Text = listfilter.Items[item0].SubItems[0].Text;
            //                list.SubItems.Add(listfilter.Items[item0].SubItems[1].Text);
            //                list.SubItems.Add(listfilter.Items[item0].SubItems[2].Text);
            //                list.SubItems.Add(listfilter.Items[item0].SubItems[3].Text);
            //                list.SubItems.Add(listfilter.Items[item0].SubItems[4].Text);
            //                list.SubItems.Add(listfilter.Items[item0].SubItems[5].Text);
            //                list.SubItems.Add(listfilter.Items[item0].SubItems[6].Text);
            //                list.SubItems.Add(listfilter.Items[item0].SubItems[7].Text);
            //                list.SubItems.Add(listfilter.Items[item0].SubItems[8].Text);
            //                if (item0 % 2 == 0)
            //                {
            //                    list.BackColor = Color.White;
            //                }
            //                else
            //                {
            //                    list.BackColor = Color.WhiteSmoke;
            //                }
            //                listviewbuyqrcode.Items.Add(list);
            //            }
            //            item0++;
            //            lbltotal.Text = "Total Rows Count:  " + listviewbuyqrcode.Items.Count.ToString();
            //        }
            //    }
            //    else
            //    {
            //        try
            //        {
            //            listviewbuyqrcode.Items.Clear(); item0 = 0;
            //            foreach (ListViewItem item in listfilter.Items)
            //            {
            //                this.listviewbuyqrcode.Items.Add((ListViewItem)item.Clone());
            //                if (item0 % 2 == 0)
            //                {
            //                    item.BackColor = Color.White;
            //                }
            //                else
            //                {
            //                    item.BackColor = Color.WhiteSmoke;
            //                }
            //                item0++;
            //            }
            //        }
            //        catch (Exception ex)
            //        {

            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    // MessageBox.Show("---" + ex.ToString());
            //}
        }





        private void butsearch_Click(object sender, EventArgs e)
        {
             GridLoad(dateTimePicker1.Value.ToString("dd-MM-yyyy"));
            
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
        
        private void butsearch_Click_1(object sender, EventArgs e)
        {
            GridLoad(dateTimePicker1.Text);
        }

        private void refreshToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            GridLoad();
        }

        private void txtqrcode_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtreceive_Click(object sender, EventArgs e)
        {
            
        }

        private void txtreceive_TextChanged(object sender, EventArgs e)
        {
            //if (txtreceive.Text != "") { txtreceive.BackColor = Color.White; }
            //else { }
        }

        private void txtqrcode_TextChanged(object sender, EventArgs e)
        {
            if (txtqrcode.Text.Length >= 12)
            {
                string code = sm.Decrypt(txtqrcode.Text);
                empty();


                string sel1 = "SELECT max(B.ASPTBLBUYSAMINWID) as ASPTBLBUYSAMINWID  FROM    ASPTBLBUYSAMINW B JOIN  ASPTBLBUYSAM C ON C.AGFSAMPLE=B.AGFSAMPLE  JOIN GTCOMPMAST D ON B.COMPCODE=D.GTCOMPMASTID  join asptblBRANDMAS E ON E.ASPTBLBRANDMASID=C.BRAND   JOIN ASPTBLSEASONMAS F ON F.ASPTBLSEASONMASID=C.SEASON   JOIN ASPTBLSAMDEPTMAS G ON G.ASPTBLSAMDEPTMASID=C.DEPARTMENT     JOIN ASPTBLRACKMAS H ON H.ASPTBLRACKMASID=B.RACK JOIN ASPTBLBINMAS I ON I.ASPTBLBINMASID=B.BIN JOIN ASPTBLSAMCATMAS J ON J.ASPTBLSAMCATMASID=C.CATEGORY where B.agfsample=" + Convert.ToInt64("0" + code);
                DataSet ds1 = Utility.ExecuteSelectQuery(sel1, "ASPTBLBUYSAMINW");
                DataTable dt1 = ds1.Tables["ASPTBLBUYSAMINW"];
                if (dt1.Rows.Count > 0)
                {

                    string sel0 = "SELECT B.ASPTBLBUYSAMINWID,C.ASPTBLBUYSAMID, D.COMPCODE,E.BRAND,B.AGFSAMPLE,F.SEASON,G.DEPARTMENT,J.CATEGORY,C.STYLENAME,C.SUBSTYLE,C.GARMENTIMAGE,C.ACTIVE,C.MFYEAR ,H.RACK,I.BIN,B.OUTWARD,C.SIZENAME,C.COUNTS,C.COLORNAME FROM    ASPTBLBUYSAMINW B JOIN  ASPTBLBUYSAM C ON C.AGFSAMPLE=B.AGFSAMPLE  JOIN GTCOMPMAST D ON B.COMPCODE=D.GTCOMPMASTID  join asptblBRANDMAS E ON E.ASPTBLBRANDMASID=C.BRAND   JOIN ASPTBLSEASONMAS F ON F.ASPTBLSEASONMASID=C.SEASON   JOIN ASPTBLSAMDEPTMAS G ON G.ASPTBLSAMDEPTMASID=C.DEPARTMENT     JOIN ASPTBLRACKMAS H ON H.ASPTBLRACKMASID=B.RACK JOIN ASPTBLBINMAS I ON I.ASPTBLBINMASID=B.BIN JOIN ASPTBLSAMCATMAS J ON J.ASPTBLSAMCATMASID=C.CATEGORY where B.ASPTBLBUYSAMINWID='" + dt1.Rows[0]["ASPTBLBUYSAMINWID"].ToString() + "'";
                    DataSet ds0 = Utility.ExecuteSelectQuery(sel0, "ASPTBLBUYSAMINW");
                    DataTable dt0 = ds0.Tables["ASPTBLBUYSAMINW"];

                    if (Convert.ToInt32(dt0.Rows.Count) > 0)
                    {
                        if (dt0.Rows[0]["OUTWARD"].ToString() == "T")
                        {
                            myString = "";
                            txtbuysamid.Text = dt0.Rows[0]["ASPTBLBUYSAMID"].ToString();
                            txtsaminwardid.Text = dt0.Rows[0]["ASPTBLBUYSAMINWID"].ToString();
                            txtdate.Text = System.DateTime.Now.ToString("dd-MM-yyyy");
                            combocompcode.Text = dt0.Rows[0]["COMPCODE"].ToString();
                            combobrand.Text = dt0.Rows[0]["BRAND"].ToString();
                            txtcode.Text = dt0.Rows[0]["AGFSAMPLE"].ToString();
                            comboseason.Text = dt0.Rows[0]["SEASON"].ToString();
                            combodept.Text = dt0.Rows[0]["DEPARTMENT"].ToString();
                            combocategory.Text = dt0.Rows[0]["CATEGORY"].ToString();
                            combostyle.Text = dt0.Rows[0]["STYLENAME"].ToString();
                            combosampletype.Text = dt0.Rows[0]["SUBSTYLE"].ToString();
                            combocounts.Text = dt0.Rows[0]["COUNTS"].ToString();
                            combosize.Text = dt0.Rows[0]["SIZENAME"].ToString();
                            combocolor.Text = dt0.Rows[0]["COLORNAME"].ToString();

                            combomfyear.Text = dt0.Rows[0]["MFYEAR"].ToString();
                            bytes = (byte[])dt0.Rows[0]["GARMENTIMAGE"];
                            Image img = Models.Device.ByteArrayToImage(bytes);
                            comborack.Text = dt0.Rows[0]["RACK"].ToString();
                            combobin.Text = dt0.Rows[0]["BIN"].ToString();
                            picturegarmentimage.Image = img; txtqrcode.Text = ""; combobuyer.Select(); combobuyer.BackColor = Color.Yellow;


                        }
                        else
                        {
                            MessageBox.Show("");

                            MessageBox.Show("This Sample already Saved. Ref Code  :" + code); txtqrcode.Text = ""; txtqrcode.Select();

                        }
                    }
                    else
                    {
                        MessageBox.Show("");
                        MessageBox.Show("No Data Found .Please Inward this Ref Code  :" + code); txtqrcode.Text = ""; txtqrcode.Select();

                    }
                    return;
                }
                else if (dt1.Rows.Count <= 0)
                {
                    string sel2 = "SELECT B.ASPTBLBUYSAMINWID,C.ASPTBLBUYSAMID, D.COMPCODE,E.BRAND,B.AGFSAMPLE,F.SEASON,G.DEPARTMENT,J.CATEGORY,C.STYLENAME,C.SUBSTYLE,C.GARMENTIMAGE,C.ACTIVE,C.MFYEAR ,H.RACK,I.BIN,B.OUTWARD FROM    ASPTBLBUYSAMINW B JOIN  ASPTBLBUYSAM C ON C.AGFSAMPLE=B.AGFSAMPLE  JOIN GTCOMPMAST D ON B.COMPCODE=D.GTCOMPMASTID  join asptblBRANDMAS E ON E.ASPTBLBRANDMASID=C.BRAND   JOIN ASPTBLSEASONMAS F ON F.ASPTBLSEASONMASID=C.SEASON   JOIN ASPTBLSAMDEPTMAS G ON G.ASPTBLSAMDEPTMASID=C.DEPARTMENT     JOIN ASPTBLRACKMAS H ON H.ASPTBLRACKMASID=B.RACK JOIN ASPTBLBINMAS I ON I.ASPTBLBINMASID=B.BIN JOIN ASPTBLSAMCATMAS J ON J.ASPTBLSAMCATMASID=C.CATEGORY where B.AGFSAMPLE='" + code + "'";
                    DataSet ds2 = Utility.ExecuteSelectQuery(sel2, "ASPTBLBUYSAMINW");
                    DataTable dt2 = ds2.Tables["ASPTBLBUYSAMINW"];
                    if (Convert.ToInt32(dt2.Rows.Count) > 0)
                    {
                        if (dt2.Rows[0]["OUTWARD"].ToString() == "T")
                        {
                            myString = "";
                            txtbuysamid.Text = dt2.Rows[0]["ASPTBLBUYSAMID"].ToString();
                            txtsaminwardid.Text = dt2.Rows[0]["ASPTBLBUYSAMINWID"].ToString();
                            txtdate.Text = Convert.ToString(Class.Users.SysDate);
                            combocompcode.Text = dt2.Rows[0]["COMPCODE"].ToString();
                            combobrand.Text = dt2.Rows[0]["BRAND"].ToString();
                            txtcode.Text = dt2.Rows[0]["AGFSAMPLE"].ToString();
                            comboseason.Text = dt2.Rows[0]["SEASON"].ToString();
                            combodept.Text = dt2.Rows[0]["DEPARTMENT"].ToString();
                            combocategory.Text = dt2.Rows[0]["CATEGORY"].ToString();
                            combostyle.Text = dt2.Rows[0]["STYLENAME"].ToString();
                            combosampletype.Text = dt2.Rows[0]["SUBSTYLE"].ToString();
                            combomfyear.Text = dt2.Rows[0]["MFYEAR"].ToString();
                            bytes = (byte[])dt2.Rows[0]["GARMENTIMAGE"];
                            Image img = Models.Device.ByteArrayToImage(bytes);
                            comborack.Text = dt2.Rows[0]["RACK"].ToString();
                            combobin.Text = dt2.Rows[0]["BIN"].ToString();
                            picturegarmentimage.Image = img; txtqrcode.Text = ""; txtreceive.BackColor = Color.Yellow;


                        }
                        else
                        {
                            MessageBox.Show("");
                            MessageBox.Show("This Sample already Saved. Ref Code  :" + code); txtqrcode.Text = ""; txtqrcode.Select();

                        }
                    }
                    return;
                }
                else
                {
                    MessageBox.Show("");
                    MessageBox.Show("No Data Found .Please Inward this Ref Code  :" + code); txtqrcode.Text = ""; txtqrcode.Select();

                }

                //else
                //{

                //    string sel2 = "SELECT distinct a.agfsample,a.outward FROM  ASPTBLBUYSAMout A where A.agfsample=" + Convert.ToInt64("0" + txtcode.Text);
                //    DataSet ds2 = Utility.ExecuteSelectQuery(sel2, "ASPTBLBUYSAMout");
                //    DataTable dt2 = ds2.Tables["ASPTBLBUYSAMout"];
                //    if (Convert.ToInt64(dt2.Rows.Count)<= 0)
                //    {
                //        string sel1 = "SELECT C.RACK,D.BIN FROM  ASPTBLBUYSAMOUT A JOIN ASPTBLBUYSAM B ON  A.AGFSAMPLE=B.AGFSAMPLE  JOIN ASPTBLRACKMAS C ON A.RACK=C.ASPTBLRACKMASID JOIN ASPTBLBINMAS D ON A.BIN=D.ASPTBLBINMASID where A.agfsample=" + Convert.ToInt64("0" + buy.ASPTBLBUYSAMID);
                //        DataSet ds1 = Utility.ExecuteSelectQuery(sel1, "ASPTBLBUYSAMOUT");
                //        DataTable dt1 = ds1.Tables["ASPTBLBUYSAMOUT"];
                //        txtprerack.Text = dt1.Rows[0]["RACK"].ToString();
                //        txtprebin.Text = dt1.Rows[0]["BIN"].ToString(); txtqrcode.Text = ""; txtqrcode.Select();
                //        MessageBox.Show("This Sample already alert This RackNo :" + dt1.Rows[0]["RACK"].ToString() + "  BIN  : " + dt1.Rows[0]["BIN"].ToString());
                //    }
                //}

            }

        }

        private void tabbuyer1_Click(object sender, EventArgs e)
        {
            Class.Users.UserTime = 0;
        }

        private void picturegarmentimage_Click(object sender, EventArgs e)
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

        private void txtreceive_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txtreceive_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back);

        }

        private void combobuyer_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(combobuyer.SelectedValue) > 0)
                {
                    string sel = "select   A.RESONSEPERSON, A.ASPTBLRESMASID  from ASPTBLRESMAS a join asptblbrandmas b  on A.BRANd=B.ASPTBLBRANDMASID  WHERE A.ACTIVE='T' and  B.ASPTBLBRANDMASID='" + combobuyer.SelectedValue + "' order by 1 ";
                    DataSet ds = Utility.ExecuteSelectQuery(sel, "ASPTBLBRANDMAS");
                    DataTable dt = ds.Tables["ASPTBLBRANDMAS"];

                    if (dt.Rows.Count > 0)
                    {
                        txtreceive.DisplayMember = "RESONSEPERSON";
                        txtreceive.ValueMember = "ASPTBLRESMASID";
                        txtreceive.DataSource = dt;
                    }
                    else
                    {
                        txtreceive.DataSource = null;
                    }
                }

            }
            catch (Exception ex) { }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            BrandSearchLoad();
            ResponsePersonLoad();
        }
    }
}