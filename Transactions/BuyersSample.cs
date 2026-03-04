using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;
using Pinnacle.Models;

namespace Pinnacle.Transactions
{
    public partial class BuyersSample : Form, ToolStripAccess
    {
        Models.Master mas = new Models.Master();
        Models.UserRights sm = new Models.UserRights();
        private static BuyersSample _instance;
        Models.BuyerSample buy = new Models.BuyerSample();
        ListView listfilter = new ListView();
        Models.CrystalReportShow cr1 = new CrystalReportShow();
        public static BuyersSample Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new BuyersSample();
                GlobalVariables.CurrentForm = _instance; return _instance;
            }
        }

        OpenFileDialog open = new OpenFileDialog();
        byte[] bytes;
        byte[] qrbytes;
        string myString = "";
        int listid = 0;
        string chk = ""; int cnt = 0;
        int i = 0; string idcardcount = "";
        QRCoder.QRCodeGenerator qc = new QRCoder.QRCodeGenerator();
      
        ListView listfilter1 = new ListView();
        DataTable dtgeneral = new DataTable();
        ListView allip = new ListView();
        DataTable reversedDt; DataTable reversedDt1;
        DataTable reversedDt2;
        public BuyersSample()
        {
            InitializeComponent();
            Class.Users.ScreenName = "BuyersSample";
            GlobalVariables.HeaderName.Text = Class.Users.ScreenName; GlobalVariables.CurrentForm = this;
            Class.Users.IPADDRESS = GenFun.GetLocalIPAddress();
            Class.Users.CREATED = Convert.ToDateTime(System.DateTime.Now.ToString("dd-MMM-yyyy") + " " + System.DateTime.Now.ToLongTimeString());
            Class.Users.SysDate = Convert.ToString(System.DateTime.Now.ToString("dd-MM-yyyy"));
            Class.Users.SysTime = Convert.ToString(DateTime.Now.ToLongTimeString().ToString());
            dateTimePicker1.Value.ToString(Class.Users.SysDate); Class.Users.UserTime = 0;
   
        }
        public void ReadOnlys()
        {

        }
        private void BuyersSample_FormClosed(object sender, FormClosedEventArgs e)
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
                if (array[i] 
                    == ' ')
                {
                    if (char.IsLower(array[i + 1]))
                    {
                        array[i + 1] = char.ToUpper(array[i + 1]); // space is identified at Index i so next i+1 has to converted to Upper!!
                    }
                }
            }


            return new string(array);

        }






        private void BuyersSample_Load(object sender, EventArgs e)
        {
            BrandLoad(); BrandSearchLoad(); compcode(); listviewbuyqrcode.Items.Clear(); listfilter.Items.Clear();
            SeasonLoad(); DepartmentLoad(); GaugeLoad(); OrderPackLoad();
             CurrencyLoad();   SampleTypeLoad(); styleload();
        }

        private void SampleTypeLoad()
        {
            string sel0 = "SELECT A.ASPTBLSAMTYPEMASID,A.SAMPLETYPE FROM ASPTBLSAMTYPEMAS A WHERE A.ACTIVE='T'";
            DataSet ds0 = Utility.ExecuteSelectQuery(sel0, "ASPTBLSAMTYPEMAS");
            DataTable dt0 = ds0.Tables["ASPTBLSAMTYPEMAS"];
            combosampletype.DataSource = dt0;
            combosampletype.DisplayMember = "SAMPLETYPE";
            combosampletype.ValueMember = "ASPTBLSAMTYPEMASID";
        }

        private void Listviewbuyqrcode_ItemActivate(object sender, EventArgs e)
        {
            try
            {
                if (listviewbuyqrcode.Items.Count >= 0)
                {
                    Class.Users.UserTime = 0;
                    buy.ASPTBLBUYSAMID = Convert.ToInt32(listviewbuyqrcode.SelectedItems[0].SubItems[2].Text);
                    string sel0 = "SELECT A.ASPTBLBUYSAMID,  A.DATE2,B.COMPCODE,C.BRAND,A.AGFSAMPLE,E.SEASON,F.DEPARTMENT,O.CATEGORY,R.ASPTBLSTYMASID,Q.SAMPLETYPE,A.FABRIC,A.FABRICCONTENT,A.COUNTS, K.GG AS GAUGE,A.GSM, A.COLORNAME,P.ORDERPACKTYPE,A.SIZENAME,N.CURRENCYNAME,A.REMARKS,A.ACTIVE,A.GARMENTIMAGE,A.QRCODE,A.RISK1,A.RISK2,A.RISK3,A.RISK4,A.RISK5,A.FABRICCOMPLIANT,A.REMARKS,A.MFYEAR, A.RATE   FROM ASPTBLBUYSAM A JOIN GTCOMPMAST B ON A.COMPCODE=B.GTCOMPMASTID join asptblBRANDMAS C ON C.ASPTBLBRANDMASID=A.BRAND  JOIN ASPTBLSEASONMAS E ON E.ASPTBLSEASONMASID=A.SEASON JOIN ASPTBLSAMDEPTMAS F ON F.ASPTBLSAMDEPTMASID=A.DEPARTMENT  JOIN ASPTBLGGMAS K ON K.ASPTBLGGMASID=A.GAUGE    JOIN ASPTBLCURMAS N ON N.ASPTBLCURMASID=A.CURRENCYNAME JOIN ASPTBLSAMCATMAS O ON O.ASPTBLSAMCATMASID=A.CATEGORY  JOIN ASPTBLORDPACKMAS  P ON P.ASPTBLORDPACKMASID=A.ORDERPACKTYPE join ASPTBLSAMTYPEMAS Q ON Q.ASPTBLSAMTYPEMASID=A.SAMPLETYPE JOIN ASPTBLSTYMAS R ON R.STYLENAME=A.STYLENAME where A.ASPTBLBUYSAMID=" + Convert.ToInt64("0" + buy.ASPTBLBUYSAMID);
                    DataSet ds0 = Utility.ExecuteSelectQuery(sel0, "ASPTBLBUYSAM");
                    DataTable dt0 = ds0.Tables["ASPTBLBUYSAM"];
                    if (dt0 != null)
                    {
                        myString = "";

                        txtbuysamid.Text = dt0.Rows[0]["ASPTBLBUYSAMID"].ToString();
                        txtdate.Text = dt0.Rows[0]["DATE2"].ToString();
                        combocompcode.Text = dt0.Rows[0]["COMPCODE"].ToString();
                        combobrand.Text = dt0.Rows[0]["BRAND"].ToString();
                        txtcode.Text = dt0.Rows[0]["AGFSAMPLE"].ToString();
                        comboseason.Text = dt0.Rows[0]["SEASON"].ToString();
                        combodept.Text = dt0.Rows[0]["DEPARTMENT"].ToString();
                        combocategory.Text = dt0.Rows[0]["CATEGORY"].ToString();
                        combostyle.Text = dt0.Rows[0]["ASPTBLSTYMASID"].ToString();
                        combosampletype.Text = dt0.Rows[0]["SAMPLETYPE"].ToString();
                        combofabric.Text = dt0.Rows[0]["FABRIC"].ToString();
                        combofabcontent.Text = dt0.Rows[0]["FABRICCONTENT"].ToString();
                        combocounts.Text = dt0.Rows[0]["COUNTS"].ToString();
                        combogg.Text = dt0.Rows[0]["GAUGE"].ToString();
                        combogsm.Text = dt0.Rows[0]["GSM"].ToString();
                        combocolor.Text = dt0.Rows[0]["COLORNAME"].ToString();
                        combopacktype.Text = dt0.Rows[0]["ORDERPACKTYPE"].ToString();
                        combosize.Text = dt0.Rows[0]["SIZENAME"].ToString();
                        combocurrency.Text = dt0.Rows[0]["CURRENCYNAME"].ToString();
                        txtrisk1.Text = dt0.Rows[0]["RISK1"].ToString();
                        txtrisk2.Text = dt0.Rows[0]["RISK2"].ToString();
                        txtrisk3.Text = dt0.Rows[0]["RISK3"].ToString();
                        txtrisk4.Text = dt0.Rows[0]["RISK4"].ToString();
                        txtrisk5.Text = dt0.Rows[0]["RISK5"].ToString();
                        txtfabcompliant.Text = dt0.Rows[0]["FABRICCOMPLIANT"].ToString();
                        txtremarks.Text = dt0.Rows[0]["REMARKS"].ToString();
                        combomfyear.Text = dt0.Rows[0]["MFYEAR"].ToString();
                        if (dt0.Rows[0]["ACTIVE"].ToString() == "T") checkbuy.Checked = true; else checkbuy.Checked = false;
                        combocurrency.Text = dt0.Rows[0]["CURRENCYNAME"].ToString();

                        txtrate.Text = dt0.Rows[0]["RATE"].ToString();
                        bytes = (byte[])dt0.Rows[0]["GARMENTIMAGE"];
                        Image img = Models.Device.ByteArrayToImage(bytes);
                        picturegarmentimage.Image = img;
       
                        lblimagesize.Text = "Actual Size : " + bytes.Length / 1024 + "kb";
                        qrbytes = (byte[])dt0.Rows[0]["QRCODE"];
                        Image img1 = Models.Device.ByteArrayToImage(qrbytes);
                        picturegrcode.Image = img1;
                        QrcodeGenerate();
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }


            //string sel0 = "select  A.ITEMIMAGE  from asptblitemas a  where A.BRANDID='22'";
            //DataSet ds0 = Utility.ExecuteSelectQuery(sel0, "asptblitemas");
            //DataTable dt0 = ds0.Tables["asptblitemas"];
            //bytes = (byte[])dt0.Rows[0]["ITEMIMAGE"];
            //Image img = Models.Device.ByteArrayToImage(bytes);
            //picturegarmentimage.Image = img;
        }

        private void empty()
        {
            txtbuysamid.Text = ""; combocompcode.SelectedIndex = -1; Class.Users.UserTime = 0;
            combobrand.SelectedIndex = -1; combodept.Text = ""; combodept.SelectedIndex = -1;
            comboseason.SelectedIndex = -1; combocategory.Text = ""; combocategory.SelectedIndex = -1;
            combobrandsearch.Text = ""; combobrandsearch.SelectedIndex = -1;
            combostyle.SelectedIndex = -1; txtcode.Text = "";
            combosampletype.Text = "";
            combofabric.Text = "";
            combofabcontent.Text = "";
            combocounts.Text = "";
            combosize.Text = "";
            combogg.SelectedIndex = -1;
            combogsm.Text = "";txtcode.Text = "";
            combocolor.Text = ""; txtdate.Text = System.DateTime.Now.ToString("dd-MM-yyyy");
            combocurrency.SelectedIndex = -1; combocurrency.Text= "";txtrate.Text = ""; combopacktype.SelectedIndex = -1;
            bytes = null;
            qrbytes = null;
            txtrisk1.Text = ""; txtrisk2.Text = ""; txtrisk3.Text = ""; txtrisk4.Text = ""; txtrisk5.Text = ""; txtfabcompliant.Text = ""; txtremarks.Text = "";
            picturegarmentimage.Image = null;
            picturegrcode.Image = null; lblimagesize.Text = "";txtsearch.Text = "";
            if (dtgeneral.Rows.Count > 0)
            {
                reversedDt1.Rows.Clear(); reversedDt2.Rows.Clear(); dtgeneral.Rows.Clear();
            }
            combomfyear.Select(); FinyearLoad();
            if (Class.Users.HUserName == "VAIRAM") checkbuy.Enabled = true; else checkbuy.Enabled = false;
            reportshow();
            butheader.BackColor = Class.Users.BackColors;
            this.BackColor = Class.Users.BackColors;
            panel1.BackColor = Class.Users.BackColors;
            panel2.BackColor = Class.Users.BackColors;
            panel3.BackColor = Class.Users.BackColors;
            this.listviewbuyqrcode.Font = Class.Users.FontName;
            this.Font = Class.Users.FontName;
        }

        private void empty1()
        {
            txtbuysamid.Text = ""; combocompcode.SelectedIndex = -1;

            txtcode.Text = "";

            bytes = null;
            qrbytes = null;
            picturegarmentimage.Image = null;
            picturegrcode.Image = null; lblimagesize.Text = ""; txtsearch.Text = "";
            if (dtgeneral.Rows.Count > 0)
            {
                reversedDt1.Rows.Clear(); reversedDt2.Rows.Clear(); dtgeneral.Rows.Clear();
            }
            combomfyear.Select(); FinyearLoad();
            if (Class.Users.HUserName == "VAIRAM") checkbuy.Enabled = true; else checkbuy.Enabled = false;

        }
        public string ConvertImageToBase64(Image file)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                file.Save(memoryStream, file.RawFormat);
                byte[] imageBytes = memoryStream.ToArray();
                return Convert.ToBase64String(imageBytes);
            }
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

        //void CategoryLoad(string cat)
        //{
        //    string sel = "SELECT  a.ASPTBLSAMDEPTMASID,a.department    FROM  ASPTBLSAMDEPTMAS  a join asptblsamcatmas b on a.category=b.asptblsamcatmasid   WHERE a.ACTIVE='T' and B.category='" + cat + "' ";
        //    DataSet ds = Utility.ExecuteSelectQuery(sel, "ASPTBLSAMDEPTMAS");
        //    DataTable dt = ds.Tables["ASPTBLSAMDEPTMAS"];
        //    if (dt.Rows.Count > 0)
        //    {
        //        combodept.DataSource = dt;
        //        combodept.DisplayMember = "department";
        //        combodept.ValueMember = "ASPTBLSAMDEPTMASID";

        //    }
        //    else
        //    {
        //        combodept.DataSource = null;
        //        combodept.Text = "";
        //    }
        //}
     
        void CurrencyLoad(string cur)
        {
            string sel = "SELECT  a.ASPTBLCURMASID,a.SYMBOL    FROM  ASPTBLCURMAS  a   WHERE a.ACTIVE='T' and A.CURRENCYNAME='" + cur + "' ";
            DataSet ds = Utility.ExecuteSelectQuery(sel, "ASPTBLCURMAS");
            DataTable dt = ds.Tables["ASPTBLCURMAS"];
            if (dt.Rows.Count > 0)
            {
                lblsymbol.Text = "";
                lblsymbol.Text = dt.Rows[0]["SYMBOL"].ToString();

            }
            else
            {
                
                lblsymbol.Text = "";
            }
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
        public void BrandLoad()
        {
            string sel = " SELECT 0 AS ASPTBLBRANDMASID , '' AS BRAND FROM DUAL UNION ALL SELECT A.ASPTBLBRANDMASID,A.BRAND    FROM  ASPTBLBRANDMAS A   WHERE A.ACTIVE='T' ";
            DataSet ds = Utility.ExecuteSelectQuery(sel, "ASPTBLBRANDMAS");
            DataTable dt = ds.Tables["ASPTBLBRANDMAS"];
            combobrand.DataSource = dt;
            combobrand.DisplayMember = "BRAND";
            combobrand.ValueMember = "ASPTBLBRANDMASID";
          
            
        }
        public void BrandSearchLoad()
        {
            string sel = " SELECT 0 AS ASPTBLBRANDMASID , '' AS BRAND FROM DUAL UNION ALL SELECT DISTINCT A.ASPTBLBRANDMASID,  A.BRAND    FROM  ASPTBLBRANDMAS A   join asptblbuysam b on A.ASPTBLBRANDMASID=B.BRAND  WHERE A.ACTIVE='T' ";
            DataSet ds = Utility.ExecuteSelectQuery(sel, "ASPTBLBRANDMAS");
            DataTable dt = ds.Tables["ASPTBLBRANDMAS"];
          
            combobrandsearch.DataSource = dt;
            combobrandsearch.DisplayMember = "BRAND";
            combobrandsearch.ValueMember = "ASPTBLBRANDMASID";

        }

        public void OrderPackLoad()
        {
            string sel = " SELECT A.ASPTBLORDPACKMASid,A.ORDERPACKTYPE    FROM  ASPTBLORDPACKMAS A   WHERE A.ACTIVE='T' ";
            DataSet ds = Utility.ExecuteSelectQuery(sel, "ASPTBLORDPACKMAS");
            DataTable dt = ds.Tables["ASPTBLORDPACKMAS"];
            combopacktype.DataSource = dt;
            combopacktype.DisplayMember = "ORDERPACKTYPE";
            combopacktype.ValueMember = "ASPTBLORDPACKMASID";
        }
        public void SeasonLoad()
        {
            string sel = "SELECT A.ASPTBLSEASONMASID, A.SEASON    FROM  ASPTBLSEASONMAS  A   WHERE A.ACTIVE='T'";
            DataSet ds = Utility.ExecuteSelectQuery(sel, "ASPTBLSEASONMAS");
            DataTable dt = ds.Tables["ASPTBLSEASONMAS"];
            comboseason.DataSource = dt;
            comboseason.DisplayMember = "SEASON";
            comboseason.ValueMember = "ASPTBLSEASONMASID";
            
        }

        public void DepartmentLoad()
        {
            string sel = " SELECT  ASPTBLSAMDEPTMASID,DEPARTMENT    FROM  ASPTBLSAMDEPTMAS    WHERE ACTIVE='T' order by 1";
            DataSet ds = Utility.ExecuteSelectQuery(sel, "ASPTBLSAMDEPTMAS");
            DataTable dt = ds.Tables["ASPTBLSAMDEPTMAS"];
            combodept.DataSource = dt;
            combodept.DisplayMember = "DEPARTMENT";
            combodept.ValueMember = "ASPTBLSAMDEPTMASID";

        }

        public void CurrencyLoad()
        {
            string sel = " SELECT ASPTBLCURMASID,CURRENCYNAME    FROM  ASPTBLCURMAS    WHERE ACTIVE='T'";
            DataSet ds = Utility.ExecuteSelectQuery(sel, "ASPTBLCURMAS");
            DataTable dt = ds.Tables["ASPTBLCURMAS"];
            combocurrency.DataSource = dt;
            combocurrency.DisplayMember = "CURRENCYNAME";
            combocurrency.ValueMember = "ASPTBLCURMASID";

        }
        public void AutoGenerateLoad()
        {
            string sel3 = "SELECT MAX(TO_NUMBER(A.AGFSAMPLE))+1  AGFSAMPLE FROM  ASPTBLBUYSAM A   WHERE A.ACTIVE='T'";
            DataSet ds3 = Utility.ExecuteSelectQuery(sel3, "ASPTBLBUYSAM");
            DataTable dt3 = ds3.Tables["ASPTBLBUYSAM"];
            Int64 cnt = Convert.ToInt64("0" + dt3.Rows[0]["AGFSAMPLE"].ToString());
            if (cnt == 0)
            {
                string sel4 = "  SELECT MAX(A.AGFSAMPLE) AGFSAMPLE    FROM  ASPTBLINOUTMAS A   WHERE  f.COMPCODE='" + Class.Users.COMPCODE + "'";
                DataSet ds4 = Utility.ExecuteSelectQuery(sel4, "ASPTBLINOUTMAS");
                DataTable dt4 = ds4.Tables["ASPTBLINOUTMAS"];
                txtcode.Text = dt4.Rows[0]["AGFSAMPLE"].ToString();
                return;
            }
            else
            {

                txtcode.Text = Convert.ToInt64("0" + dt3.Rows[0]["AGFSAMPLE"].ToString()).ToString();

                return;
            }
        }
       
      private void styleload()
        {
            try
            {
                string sel1 = "SELECT ASPTBLSTYMASID,STYLENAME    FROM  ASPTBLSTYMAS    WHERE ACTIVE='T' ORDER BY 1";
                DataSet ds1 = Utility.ExecuteSelectQuery(sel1, "ASPTBLSTYMAS");
                DataTable dt1 = ds1.Tables["ASPTBLSTYMAS"];
                combostyle.DataSource = dt1;
                combostyle.DisplayMember = "STYLENAME";
                combostyle.ValueMember = "STYLENAME";
            }
            catch(Exception ex) { }
        }
      
        public void GaugeLoad()
        {
            string sel = "SELECT ASPTBLGGMASID,GG    FROM  ASPTBLGGMAS    WHERE ACTIVE='T' ORDER BY 1";
            DataSet ds = Utility.ExecuteSelectQuery(sel, "ASPTBLGGMAS");
            DataTable dt = ds.Tables["ASPTBLGGMAS"];
            combogg.DataSource = dt;
            combogg.DisplayMember = "GG";
            combogg.ValueMember = "ASPTBLGGMASID";


           
        }

      
        int j = 0;
        public void GridLoad()
        {
            try
            {


                listviewbuyqrcode.Items.Clear();

                string sel0 = "SELECT A.ASPTBLBUYSAMID,  A.DATE1,B.COMPCODE,C.BRAND,A.AGFSAMPLE,E.SEASON,F.DEPARTMENT,A.STYLENAME,A.FABRIC,A.COUNTS,K.GG AS GAUGE,A.GSM,A.COLORNAME,A.SIZENAME,A.RATE,A.REMARKS,A.ACTIVE,A.QRCODE  FROM ASPTBLBUYSAM A JOIN GTCOMPMAST B ON A.COMPCODE=B.GTCOMPMASTID join asptblBRANDMAS C ON C.ASPTBLBRANDMASID=A.BRAND  JOIN ASPTBLSEASONMAS E ON E.ASPTBLSEASONMASID=A.SEASON JOIN ASPTBLSAMDEPTMAS F ON F.ASPTBLSAMDEPTMASID=A.DEPARTMENT   JOIN ASPTBLGGMAS K ON K.ASPTBLGGMASID=A.GAUGE   order by a.ASPTBLBUYSAMID desc";
                DataSet ds0 = Utility.ExecuteSelectQuery(sel0, "ASPTBLBUYSAM");
                DataTable dt = ds0.Tables["ASPTBLBUYSAM"];

                if (dt.Rows.Count >= 0)
                {
                   
                    int ii = 0; j = 1; listviewbuyqrcode.BeginUpdate();
                    foreach (DataRow myRow in dt.Rows)
                    {
                        ListViewItem list = new ListViewItem();

                        list.SubItems.Add(j.ToString());
                        //bytes = (byte[])myRow["GARMENTIMAGE"];
                        //list.ImageIndex = ii;
                        //Image img = Models.Device.ByteArrayToImage(bytes);
                        //imgList.Images.Add(img);
                        list.SubItems.Add(myRow["ASPTBLBUYSAMID"].ToString());
                        list.SubItems.Add(myRow["AGFSAMPLE"].ToString());
                        list.SubItems.Add(myRow["DEPARTMENT"].ToString());
                        list.SubItems.Add(myRow["STYLENAME"].ToString());
                        list.SubItems.Add(myRow["COUNTS"].ToString());
                        list.SubItems.Add(myRow["FABRIC"].ToString());                       
                      
                        listviewbuyqrcode.Items.Add(list);   
                       
                        ii++; j++;
                        lbltotal.Text = "Total Rows Count:  " + listviewbuyqrcode.Items.Count.ToString();
                        bytes = null; 
                        listfilter.Items.Add((ListViewItem)list.Clone());
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

        public void dateload(string date)
        {
            try
            {

                listfilter.Items.Clear();
                listviewbuyqrcode.Items.Clear(); Class.Users.UserTime = 0;
                string sel0 = "SELECT A.ASPTBLBUYSAMID,  A.DATE1,B.COMPCODE,C.BRAND,A.AGFSAMPLE,E.SEASON,F.DEPARTMENT,A.STYLENAME,Q.SAMPLETYPE,A.FABRIC,A.COUNTS,K.GG AS GAUGE,A.GSM,A.COLORNAME,A.SIZENAME,A.RATE,A.REMARKS,A.ACTIVE,A.GARMENTIMAGE,A.QRCODE  FROM ASPTBLBUYSAM A JOIN GTCOMPMAST B ON A.COMPCODE=B.GTCOMPMASTID join asptblBRANDMAS C ON C.ASPTBLBRANDMASID=A.BRAND  JOIN ASPTBLSEASONMAS E ON E.ASPTBLSEASONMASID=A.SEASON JOIN ASPTBLSAMDEPTMAS F ON F.ASPTBLSAMDEPTMASID=A.DEPARTMENT  JOIN ASPTBLGGMAS K ON K.ASPTBLGGMASID=A.GAUGE   JOIN ASPTBLSAMTYPEMAS Q ON Q.ASPTBLSAMTYPEMASID=A.SAMPLETYPE where a.date1='" + date + "'  order by a.ASPTBLBUYSAMID desc";
                DataSet ds0 = Utility.ExecuteSelectQuery(sel0, "ASPTBLBUYSAM");
                DataTable dt = ds0.Tables["ASPTBLBUYSAM"];

                if (dt != null)
                {

                    int i = 0; int j = 1; listviewbuyqrcode.BeginUpdate();
                    foreach (DataRow myRow in dt.Rows)
                    {
                        ListViewItem list = new ListViewItem();
                        list.SubItems.Add(j.ToString());                       
                        list.SubItems.Add(myRow["ASPTBLBUYSAMID"].ToString());
                        list.SubItems.Add(myRow["AGFSAMPLE"].ToString());
                        list.SubItems.Add(myRow["DEPARTMENT"].ToString());
                        list.SubItems.Add(myRow["STYLENAME"].ToString());
                        list.SubItems.Add(myRow["COUNTS"].ToString());
                        list.SubItems.Add(myRow["FABRIC"].ToString());
                        bytes = (byte[])myRow["GARMENTIMAGE"];
                        Image img = Models.Device.ByteArrayToImage(bytes);
                        imgList.Images.Add(img);
                        list.ImageIndex = i;
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



                    lbltotal.Text = "No Data Found This Date  :" + dateTimePicker1.Value.ToString("dd-MM-yyyy") + "  Total Rows Count: " + listviewbuyqrcode.Items.Count.ToString();
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public void GridLoad(string brand)
        {
            try
            {
                if (brand != "")
                {
                    listfilter.Items.Clear();

                    listviewbuyqrcode.Items.Clear();
                    string sel0 = "SELECT A.ASPTBLBUYSAMID,  A.DATE1,B.COMPCODE,C.BRAND,A.AGFSAMPLE,E.SEASON,F.DEPARTMENT,A.STYLENAME,Q.SAMPLETYPE,A.FABRIC,A.COUNTS,K.GG AS GAUGE,A.GSM," +
                        "A.COLORNAME,A.SIZENAME,A.RATE,A.REMARKS,A.ACTIVE,A.QRCODE  FROM ASPTBLBUYSAM A JOIN GTCOMPMAST B ON A.COMPCODE=B.GTCOMPMASTID join asptblBRANDMAS C ON C.ASPTBLBRANDMASID=A.BRAND  JOIN ASPTBLSEASONMAS E ON E.ASPTBLSEASONMASID=A.SEASON JOIN ASPTBLSAMDEPTMAS F ON F.ASPTBLSAMDEPTMASID=A.DEPARTMENT  JOIN ASPTBLGGMAS K ON K.ASPTBLGGMASID=A.GAUGE   JOIN ASPTBLSAMTYPEMAS Q ON Q.ASPTBLSAMTYPEMASID=A.SAMPLETYPE  where c.brand='" + brand + "' order by a.ASPTBLBUYSAMID desc";
                    DataSet ds0 = Utility.ExecuteSelectQuery(sel0, "ASPTBLBUYSAM");
                    DataTable dt = ds0.Tables["ASPTBLBUYSAM"];

                    if (dt.Rows.Count > 0)
                    {

                        int i = 0; int j = 1; listviewbuyqrcode.BeginUpdate();
                        foreach (DataRow myRow in dt.Rows)
                        {
                            ListViewItem list = new ListViewItem();

                            list.SubItems.Add(j.ToString());
                            // list.SubItems.Add(j.ToString());
                            list.SubItems.Add(myRow["ASPTBLBUYSAMID"].ToString());
                            list.SubItems.Add(myRow["AGFSAMPLE"].ToString());
                            list.SubItems.Add(myRow["DEPARTMENT"].ToString());
                            list.SubItems.Add(myRow["STYLENAME"].ToString());
                            list.SubItems.Add(myRow["COUNTS"].ToString());
                            list.SubItems.Add(myRow["FABRIC"].ToString());
                            //bytes = (byte[])myRow["GARMENTIMAGE"];
                            //Image img = Models.Device.ByteArrayToImage(bytes);
                            //imgList.Images.Add(img);
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



                        lbltotal.Text = "No Data Found This Date  :" + dateTimePicker1.Value.ToString("dd-MM-yyyy") + "  Total Rows Count: " + listviewbuyqrcode.Items.Count.ToString();
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
        public void GridLoad(DateTime date)
        {
            try
            {
                listfilter.Items.Clear();

                listviewbuyqrcode.Items.Clear();

                string sel0 = "SELECT A.ASPTBLBUYSAMID,  A.DATE1,B.COMPCODE,C.BRAND,A.AGFSAMPLE,E.SEASON,F.DEPARTMENT,A.STYLENAME,A.SAMPLETYPE,A.FABRIC,A.COUNTS,K.GG AS GAUGE,A.GSM,A.COLORNAME,A.SIZENAME,A.RATE,A.REMARKS,A.ACTIVE,A.GARMENTIMAGE,A.QRCODE  FROM ASPTBLBUYSAM A JOIN GTCOMPMAST B ON A.COMPCODE=B.GTCOMPMASTID join asptblBRANDMAS C ON C.ASPTBLBRANDMASID=A.BRAND  JOIN ASPTBLSEASONMAS E ON E.ASPTBLSEASONMASID=A.SEASON JOIN ASPTBLSAMDEPTMAS F ON F.ASPTBLSAMDEPTMASID=A.DEPARTMENT  JOIN ASPTBLGGMAS K ON K.ASPTBLGGMASID=A.GAUGE   where  a.date1='" + date + "' order by a.ASPTBLBUYSAMID desc";
                DataSet ds0 = Utility.ExecuteSelectQuery(sel0, "ASPTBLBUYSAM");
                DataTable dt = ds0.Tables["ASPTBLBUYSAM"];

                if (dt.Rows.Count > 0)
                {

                    int i = 0; int j = 1; listviewbuyqrcode.BeginUpdate();
                    foreach (DataRow myRow in dt.Rows)
                    {
                        ListViewItem list = new ListViewItem();

                        list.SubItems.Add(j.ToString());
                        // list.SubItems.Add(j.ToString());
                        list.SubItems.Add(myRow["ASPTBLBUYSAMID"].ToString());
                        list.SubItems.Add(myRow["AGFSAMPLE"].ToString());
                        list.SubItems.Add(myRow["DEPARTMENT"].ToString());
                        list.SubItems.Add(myRow["STYLENAME"].ToString());
                        list.SubItems.Add(myRow["COUNTS"].ToString());
                        list.SubItems.Add(myRow["FABRIC"].ToString());
                        bytes = (byte[])myRow["GARMENTIMAGE"];
                        Image img = Models.Device.ByteArrayToImage(bytes);
                        imgList.Images.Add(img);
                        list.ImageIndex = i;
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



                    lbltotal.Text = "No Data Found This Date  :" + dateTimePicker1.Value.ToString("dd-MM-yyyy") + "  Total Rows Count: " + listviewbuyqrcode.Items.Count.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public void News()
        {
         //   dateload(dateTimePicker1.Value.ToString("dd-MM-yyyy"));
           empty();
            //crystalReportViewer3.ReportSource = null;
            //crystalReportViewer3.Refresh();
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

        private bool check()
        {
            if (combomfyear.Text == "") { combomfyear.BackColor = System.Drawing.Color.Beige; combomfyear.Select(); MessageBox.Show("combomfyear is Empty ", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Information); return false; }
            if (combocompcode.SelectedValue == null) { combocompcode.BackColor = System.Drawing.Color.Beige; combocompcode.Select(); MessageBox.Show("CompCode is Empty ", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Information); return false; }
            if (combobrand.SelectedValue == null) { combobrand.BackColor = System.Drawing.Color.Beige; combobrand.Select(); MessageBox.Show("Brand Name is Empty ", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Information); return false; }
            if (comboseason.SelectedValue == null) { comboseason.BackColor = System.Drawing.Color.Beige; comboseason.Select(); MessageBox.Show("SeaSon Name is Empty ", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Information); return false; }
            if (combodept.SelectedValue == null) { combodept.BackColor = System.Drawing.Color.Beige; combodept.Select(); MessageBox.Show("Department is Empty ", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Information); return false; }
            if (combocategory.SelectedValue == null) { combocategory.BackColor = System.Drawing.Color.Beige; combocategory.Select(); MessageBox.Show("Category is Empty ", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Information); return false; }
            if (Convert.ToString(combostyle.Text) == "") {combostyle.BackColor = System.Drawing.Color.Beige; combostyle.Select(); MessageBox.Show("StyleName is Empty ", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Information); return false; }
            if (combosampletype.SelectedValue == null) { combosampletype.BackColor = System.Drawing.Color.Beige; combosampletype.Select(); MessageBox.Show("Sample Type is Empty ", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Information); return false; }
            if (Convert.ToString(combofabric.Text) == "") {combocompcode.BackColor = System.Drawing.Color.Beige; combofabric.Select(); MessageBox.Show("Fabric Name is Empty ", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Information); return false; }
            if (combocounts.Text == "") {combocounts.BackColor = System.Drawing.Color.Beige; combocounts.Select(); MessageBox.Show("Counts is Empty ", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Information); return false; }
            if (combogg.SelectedValue == null) { combogg.BackColor = System.Drawing.Color.Beige; combogg.Select(); MessageBox.Show("Gauge is Empty ", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Information); return false; }
            if (combogsm.Text == "") { combogsm.BackColor = System.Drawing.Color.Beige; combogsm.Select(); MessageBox.Show("Gsm is Empty ", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Information); return false; }
            if (combocolor.Text == "") {combocolor.BackColor = System.Drawing.Color.Beige; combocolor.Select(); MessageBox.Show("ColorName is Empty ", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Information); return false; }
            if (combopacktype.SelectedValue == null) {combopacktype.BackColor = System.Drawing.Color.Beige; combopacktype.Select(); MessageBox.Show("Order Pack Type is Empty ", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Information); return false; }
            if (combosize.Text == "") {combosize.BackColor = System.Drawing.Color.Beige; combosize.Select(); MessageBox.Show("SizeName is Empty ", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Information); return false; }
            if (combocurrency.SelectedValue == null) {combocurrency.BackColor = System.Drawing.Color.Beige; combocurrency.Select(); MessageBox.Show("Currency is Empty ", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Information); return false; }
            if (picturegarmentimage.Image == null) { picturegarmentimage.Select(); MessageBox.Show("Garment Image is Empty ", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Information); return false; }
            return true;
        }
        public void Saves()
        {
            try
            {

                if (check())
                {
                   
                    if (bytes != null)
                    {
                        int grid = Convert.ToInt32("0" + txtbuysamid.Text);
                        OracleCommand ascmd;
                        if (checkbuy.Checked) chk = "T"; else chk = "F";

                        string sel = "SELECT ASPTBLBUYSAMID FROM  ASPTBLBUYSAM  where BRAND='" + combobrand.SelectedValue + "' AND SEASON='" + comboseason.SelectedValue + "'  AND DEPARTMENT='" + combodept.SelectedValue + "' AND AGFSAMPLE='" + txtcode.Text + "' AND STYLENAME='" + combostyle.Text + "' AND SAMPLETYPE='" + combosampletype.Text + "'  AND CATEGORY='" + combocategory.Text + "' AND FABRIC='" + combofabric.Text + "' AND FABRICCONTENT='" + combofabcontent.Text + "'  AND COUNTS='" + combocounts.Text + "' AND  GAUGE='" + combogg.Text + "' AND GSM='" + combogsm.Text + "' AND COLORNAME='" + combocolor.Text + "' AND ACTIVE='" + chk + "' AND RATE='" + txtrate.Text + "' AND CURRENCYNAME='" + combocurrency.SelectedValue + "' AND SIZENAME='" + combosize.Text + "'  AND  IMAGEBYTE=" + Convert.ToInt64(bytes.Length) + " AND COMPCODE=" + Class.Users.COMPCODE + " AND RISK1='" + txtrisk1.Text + "' AND RISK2='" + txtrisk2.Text + "' AND RISK3='" + txtrisk3.Text + "' AND RISK4='" + txtrisk4.Text + "' AND RISK5='" + txtrisk5.Text + "' AND FABRICCOMPLIANT='" + txtfabcompliant.Text + "' AND REMARKS='" + txtremarks.Text + "' and MFYEAR='" + combomfyear.Text + "'";
                        DataSet ds1 = Utility.ExecuteSelectQuery(sel, "ASPTBLBUYSAM");
                        DataTable dtselect = ds1.Tables["ASPTBLBUYSAM"];
                        if (dtselect.Rows.Count != 0)
                        {
                            MessageBox.Show("Child Record Found     :  " + "'" + txtcode.Text + "'", "Alert"); empty();
                        }
                        else if (dtselect.Rows.Count == 0 && grid == 0)
                        {
                            txtcode.Text = "";
                            AutoGenerateLoad();
                            QrcodeGenerate();
                            Utility.Connect();
                            string ins = "INSERT INTO ASPTBLBUYSAM (finyear,DATE1,BRAND,SEASON,DEPARTMENT,CATEGORY,AGFSAMPLE ,STYLENAME ,SAMPLETYPE ,FABRIC,FABRICCONTENT,COUNTS,GAUGE ,GSM ,SIZENAME,COLORNAME,ORDERPACKTYPE,GARMENTIMAGE,IMAGEBYTE,ACTIVE,QRCODE,RATE,GIMAGE,COMPCODE,RISK1,RISK2,RISK3,RISK4,RISK5,FABRICCOMPLIANT,REMARKS,MFYEAR,PCS,CURRENCYNAME, USERNAME ,  CREATEDBY,  CREATEDON,  MODIFIEDON,  IPADDRESS,OUTWARD) VALUES(:finyear,:DATE1,:BRAND,:SEASON,:DEPARTMENT,:CATEGORY,:BUYERCODE,:STYLENAME,:SAMPLETYPE,:FABRIC,:FABRICCONTENT,:COUNTS,:GAUGE,:GSM,:SIZENAME,:COLORNAME,:ORDERPACKTYPE,:GARMENTIMAGE,:IMAGEBYTE,:ACTIVE,:QRCODE,:RATE,:GIMAGE,:COMPCODE,:RISK1,:RISK2,:RISK3,:RISK4,:RISK5,:FABRICCOMPLIANT,:REMARKS,:MFYEAR,:PCS,:CURRENCYNAME, :USERNAME ,  :CREATEDBY,  :CREATEDON,  :MODIFIEDON,  :IPADDRESS,:OUTWARD)";
                            ascmd = new OracleCommand(ins, Utility.con);
                            ascmd.Parameters.Add(":finyear", combofinyear.SelectedValue);
                            ascmd.Parameters.Add(":DATE1", Convert.ToString(System.DateTime.Now.ToString("dd-MM-yyyy")));
                            ascmd.Parameters.Add(":BRAND", combobrand.SelectedValue);
                            ascmd.Parameters.Add(":SEASON", comboseason.SelectedValue);
                            ascmd.Parameters.Add(":DEPARTMENT", combodept.SelectedValue);
                            ascmd.Parameters.Add(":CATEGORY", combocategory.SelectedValue);
                            ascmd.Parameters.Add(":AGFSAMPLE", txtcode.Text);
                            ascmd.Parameters.Add(":STYLENAME", combostyle.Text);
                            ascmd.Parameters.Add(":SAMPLETYPE", combosampletype.SelectedValue);
                            ascmd.Parameters.Add(":FABRIC", combofabric.Text);
                            ascmd.Parameters.Add(":FABRICCONTENT", combofabcontent.Text);
                            ascmd.Parameters.Add(":COUNTS", combocounts.Text);
                            ascmd.Parameters.Add(":GAUGE", combogg.SelectedValue);
                            ascmd.Parameters.Add(":GSM", combogsm.Text);
                            ascmd.Parameters.Add(":SIZENAME", combosize.Text);
                            ascmd.Parameters.Add(":COLORNAME", combocolor.Text);
                            ascmd.Parameters.Add(":ORDERPACKTYPE", combopacktype.SelectedValue);
                            ascmd.Parameters.Add(":GARMENTIMAGE", bytes);
                            ascmd.Parameters.Add(":IMAGEBYTE", Convert.ToInt64("0" + bytes.Length));
                            ascmd.Parameters.Add(":ACTIVE", chk);
                            ascmd.Parameters.Add(":QRCODE", qrbytes);
                            ascmd.Parameters.Add(":RATE", txtrate.Text);
                            ascmd.Parameters.Add(":GIMAGE", myString);
                            ascmd.Parameters.Add(":COMPCODE", combocompcode.SelectedValue);
                            ascmd.Parameters.Add(":RISK1", txtrisk1.Text.Trim());
                            ascmd.Parameters.Add(":RISK2", txtrisk2.Text.Trim());
                            ascmd.Parameters.Add(":RISK3", txtrisk3.Text.Trim());
                            ascmd.Parameters.Add(":RISK4", txtrisk4.Text.Trim());
                            ascmd.Parameters.Add(":RISK5", txtrisk5.Text.Trim());
                            ascmd.Parameters.Add(":FABRICCOMPLIANT", txtfabcompliant.Text.Trim());
                            ascmd.Parameters.Add(":REMARKS", txtremarks.Text.Trim());
                            ascmd.Parameters.Add(":MFYEAR", combomfyear.Text);             
                            ascmd.Parameters.Add(":PCS", 1);
                            ascmd.Parameters.Add(":CURRENCYNAME", combocurrency.SelectedValue);
                            ascmd.Parameters.Add(":USERNAME", Class.Users.USERID);
                            ascmd.Parameters.Add(":CREATEDBY", Class.Users.HUserName);
                            ascmd.Parameters.Add(":CREATEDON", System.DateTime.Now.ToString());
                            ascmd.Parameters.Add(":MODIFIEDON", System.DateTime.Now.ToString());
                            ascmd.Parameters.Add(":IPADDRESS", Class.Users.IPADDRESS);
                            ascmd.Parameters.Add(":OUTWARD", "F");
                            ascmd.ExecuteNonQuery();
                            string sel1 = "SELECT max(ASPTBLBUYSAMID) as ASPTBLBUYSAMID FROM  ASPTBLBUYSAM  where COMPCODE=" + combocompcode.SelectedValue;
                            DataSet ds11 = Utility.ExecuteSelectQuery(sel1, "ASPTBLBUYSAM");
                            DataTable dtselect1 = ds11.Tables["ASPTBLBUYSAM"];
                            string ins2 = "update ASPTBLBUYSAM a set a.DATE2=to_Date('" + System.DateTime.Now.ToString("dd-MM-yyyy") + "','dd-MM-yyyy') where a.ASPTBLBUYSAMID=" + dtselect1.Rows[0]["ASPTBLBUYSAMID"].ToString();
                            Utility.ExecuteNonQuery(ins2);
                            MessageBox.Show("Record Saved Saved Successfully     :  " + "'" + txtcode.Text + "'", "Success");
                            dateload(dateTimePicker1.Value.ToString("dd-MM-yyyy")); empty();
                           
                        }
                        else
                        {
                            Utility.Connect();
                            string query = "UPDATE   ASPTBLBUYSAM SET BRAND=:BRAND,SEASON=:SEASON,DEPARTMENT=:DEPARTMENT,CATEGORY=:CATEGORY,AGFSAMPLE=:AGFSAMPLE,STYLENAME=:STYLENAME,SAMPLETYPE=:SAMPLETYPE,FABRIC=:FABRIC,FABRICCONTENT=:FABRICCONTENT,COUNTS=:COUNTS,GAUGE=:GAUGE,GSM=:GSM,SIZENAME=:SIZENAME,COLORNAME=:COLORNAME,ORDERPACKTYPE=:ORDERPACKTYPE,GARMENTIMAGE=:GARMENTIMAGE,IMAGEBYTE=:IMAGEBYTE,ACTIVE=:ACTIVE,QRCODE=:QRCODE,RATE=:RATE,COMPCODE=:COMPCODE,RISK1=:RISK1,RISK2=:RISK2,RISK3=:RISK3,RISK4=:RISK4,RISK5=:RISK5,FABRICCOMPLIANT=:FABRICCOMPLIANT,REMARKS=:REMARKS,MFYEAR=:MFYEAR,CURRENCYNAME=:CURRENCYNAME, USERNAME=:USERNAME ,  CREATEDBY=:CREATEDBY,MODIFIEDON=:MODIFIEDON,  IPADDRESS=:IPADDRESS,OUTWARD=:OUTWARD WHERE ASPTBLBUYSAMID=:ASPTBLBUYSAMID";
                            ascmd = new OracleCommand(query, Utility.con);
                            
                            ascmd.Parameters.Add(":BRAND", combobrand.SelectedValue);
                            ascmd.Parameters.Add(":SEASON", comboseason.SelectedValue);
                            ascmd.Parameters.Add(":DEPARTMENT", combodept.SelectedValue);
                            ascmd.Parameters.Add(":CATEGORY", combocategory.SelectedValue);
                            ascmd.Parameters.Add(":AGFSAMPLE", txtcode.Text);
                            ascmd.Parameters.Add(":STYLENAME", combostyle.Text);
                            ascmd.Parameters.Add(":SAMPLETYPE", combosampletype.SelectedValue);
                            ascmd.Parameters.Add(":FABRIC", combofabric.Text);
                            ascmd.Parameters.Add(":FABRICCONTENT", combofabcontent.Text);
                            ascmd.Parameters.Add(":COUNTS", combocounts.Text);
                            ascmd.Parameters.Add(":GAUGE", combogg.SelectedValue);
                            ascmd.Parameters.Add(":GSM", combogsm.Text);
                            ascmd.Parameters.Add(":SIZENAME", combosize.Text);
                            ascmd.Parameters.Add(":COLORNAME", combocolor.Text);
                            ascmd.Parameters.Add(":ORDERPACKTYPE", combopacktype.SelectedValue);
                            ascmd.Parameters.Add(":GARMENTIMAGE", bytes);
                            ascmd.Parameters.Add(":IMAGEBYTE", Convert.ToInt64("0" + bytes.Length));
                            ascmd.Parameters.Add(":ACTIVE", chk);
                            ascmd.Parameters.Add(":QRCODE", qrbytes);
                            ascmd.Parameters.Add(":RATE", txtrate.Text);
                            ascmd.Parameters.Add(":COMPCODE", combocompcode.SelectedValue);
                            ascmd.Parameters.Add(":RISK1", txtrisk1.Text.Trim());
                            ascmd.Parameters.Add(":RISK2", txtrisk2.Text.Trim());
                            ascmd.Parameters.Add(":RISK3", txtrisk3.Text.Trim());
                            ascmd.Parameters.Add(":RISK4", txtrisk4.Text.Trim());
                            ascmd.Parameters.Add(":RISK5", txtrisk5.Text.Trim());
                            ascmd.Parameters.Add(":FABRICCOMPLIANT", txtfabcompliant.Text.Trim());
                            ascmd.Parameters.Add(":REMARKS",txtremarks.Text.Trim());
                            ascmd.Parameters.Add(":MFYEAR", combomfyear.Text);
                            ascmd.Parameters.Add(":CURRENCYNAME", combocurrency.SelectedValue);
                            ascmd.Parameters.Add(":USERNAME", Class.Users.USERID);
                            ascmd.Parameters.Add(":CREATEDBY", Class.Users.HUserName);                           
                            ascmd.Parameters.Add(":MODIFIEDON", System.DateTime.Now.ToString());
                            ascmd.Parameters.Add(":IPADDRESS", Class.Users.IPADDRESS);
                            ascmd.Parameters.Add(":OUTWARD", "F");
                            ascmd.Parameters.Add(":ASPTBLBUYSAMID", grid);
                            ascmd.ExecuteNonQuery();
                            Utility.DisConnect();
                            dateload(dateTimePicker1.Value.ToString("dd-MM-yyyy"));
                            MessageBox.Show("Record Updated  Successfully      :  " + "'" + txtcode.Text + "'", "Success");
                            empty();
                        }

                        Cursor = Cursors.Default;
                    }
                    else
                    {
                        MessageBox.Show("pls select Garment Image", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        picturegarmentimage.Focus();

                    }
                }

            }
            catch (Exception EX)
            {
                MessageBox.Show(EX.ToString());
            }

        }

        private void butqrcreate_Click(object sender, EventArgs e)
        {

        }
        void QrcodeGenerate()
        {
            try
            {
               
                    Label lblqrheader = new Label(); string year1 = "";                 
                    year1 = txtdate.Text.Substring(6, 4).ToString();
                    lvgrcode.Font = new Font("Calibri", 8, FontStyle.Regular);

                    lvgrcode.Text = sm.Encrypt(txtcode.Text);

                    if (txtcode.Text != "")
                    {

                        var mydata = qc.CreateQrCode(lvgrcode.Text, QRCoder.QRCodeGenerator.ECCLevel.L);
                        var code = new QRCoder.QRCode(mydata);

                        picturegrcode.Image = code.GetGraphic(5, Color.Black, Color.White, true);
                        MemoryStream stream = new MemoryStream();
                        picturegrcode.Image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                        qrbytes = stream.ToArray();
                        string str = Encoding.Default.GetString(qrbytes);
                        myString = str;
                    }

                    else
                    {
                        MessageBox.Show("pls Enter Madatary Fields", "Error");
                    }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("error" + ex.ToString());
            }


        }
        private void Picturegarmentimage_Click(object sender, EventArgs e)
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
                        //  System.Text.Encoding enc = System.Text.Encoding.ASCII;
                        lblimagesize.Text = "Actual Size : " + bytes.Length/1024 + "kb";
                        if(Convert.ToInt64(bytes.Length/1024) > 100)
                        {
                            picturegarmentimage.Image = null;
                            MessageBox.Show("pls convert image size  below 100 kb.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Picturegrcode_Click(object sender, EventArgs e)
        {

            try
            {


                QrcodeGenerate();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }



       

        private void Panelimage_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }



        public void Prints()
        {
            try { 
            if (txtbuysamid.Text != "")
            {

                Report.BuyerSampleReport ssm = new Report.BuyerSampleReport(Convert.ToInt32(txtbuysamid.Text));
                ssm.Show();
                empty();
            }
            }
            catch (Exception EX)
            {
                MessageBox.Show("Prints");
            }
        }
        public void Imports()
        {
            Report.QrCode rq = new Report.QrCode();
            rq.Show();
        }

        private void RefreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dateload(dateTimePicker1.Value.ToString("dd-MM-yyyy"));
            // GridLoad();
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
            if (txtbuysamid.Text != "")
            {
                string sel1 = "select a.agfsample from asptblbuysaminw a where a.asptblbuysaminwid='" + txtcode.Text + "'";
                DataSet ds = Utility.ExecuteSelectQuery(sel1, "asptblbuysaminw");
                DataTable dt = ds.Tables["asptblbuysaminw"];
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("Child Record Found.Can Not Delete." + txtbuysamid.Text, " Alert Message ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }
                else
                {

                    string del = "delete from asptblbuysam where asptblbuysamid='" + Convert.ToInt64("0" + txtbuysamid.Text) + "'";
                    Utility.ExecuteNonQuery(del);
                    MessageBox.Show("Record Deleted Successfully " + txtbuysamid.Text, " Delete Message ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dateload(dateTimePicker1.Value.ToString("dd-MM-yyyy")); empty();
                }
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

       
        private void refreshToolStripMenuItem2_Click(object sender, EventArgs e)
        {
             CurrencyLoad(); OrderPackLoad();
           
            GridLoad();
        }

        private void refreshToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            GaugeLoad();
        }

        private void refreshToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            DepartmentLoad();
        }

        private void refreshToolStripMenuItem5_Click(object sender, EventArgs e)
        {

        }

        private void refreshToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            compcode(); BrandLoad();
        }

        private void refreshToolStripMenuItem6_Click(object sender, EventArgs e)
        {
            SeasonLoad();
        }

        private void refreshToolStripMenuItem7_Click(object sender, EventArgs e)
        {
            DepartmentLoad();
        }

        private  void txtsearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int item0 = 0;
                if (txtsearch.Text.Length > 0)
                {
                    listviewbuyqrcode.Items.Clear();
                    foreach (ListViewItem item in listfilter.Items)
                    {
                        ListViewItem list = new ListViewItem();
                        if (listfilter.Items[item0].SubItems[3].ToString().Contains(txtsearch.Text) || listfilter.Items[item0].SubItems[4].ToString().Contains(txtsearch.Text) || listfilter.Items[item0].SubItems[5].ToString().Contains(txtsearch.Text) || listfilter.Items[item0].SubItems[6].ToString().Contains(txtsearch.Text) || listfilter.Items[item0].SubItems[7].ToString().Contains(txtsearch.Text))
                        {
                            list.Text = listfilter.Items[item0].SubItems[0].Text;
                            list.SubItems.Add(listfilter.Items[item0].SubItems[1].Text);
                            list.SubItems.Add(listfilter.Items[item0].SubItems[2].Text);
                            list.SubItems.Add(listfilter.Items[item0].SubItems[3].Text);
                            list.SubItems.Add(listfilter.Items[item0].SubItems[4].Text);
                            list.SubItems.Add(listfilter.Items[item0].SubItems[5].Text);
                            list.SubItems.Add(listfilter.Items[item0].SubItems[6].Text);
                            list.SubItems.Add(listfilter.Items[item0].SubItems[7].Text);
                            if (item0 % 2 == 0)
                            {
                                list.BackColor = Color.White;
                            }
                            else
                            {
                                list.BackColor = Color.WhiteSmoke;
                            }
                            listviewbuyqrcode.Items.Add(list);
                        }
                        item0++;
                        lbltotal.Text = "Total Rows Count:  " + listviewbuyqrcode.Items.Count.ToString();
                    }
                }
                else
                {
                    try
                    {
                        listviewbuyqrcode.Items.Clear(); item0 = 0;
                        foreach (ListViewItem item in listfilter.Items)
                        {

                            if (item0 % 2 == 0)
                            {
                                item.BackColor = Color.White;
                            }
                            else
                            {
                                item.BackColor = Color.WhiteSmoke;
                            }
                            this.listviewbuyqrcode.Items.Add((ListViewItem)item.Clone());
                            item0++;
                            lbltotal.Text = "Total Rows Count:  " + listviewbuyqrcode.Items.Count.ToString();
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void combogsm_KeyPress(object sender, KeyPressEventArgs e)
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

  
        private void combofabcontent_KeyPress(object sender, KeyPressEventArgs e)
        {
            //  e.Handled = !(char.IsLetterOrDigit(e.KeyChar) || e.KeyChar == '/' || e.KeyChar == ' '  || e.KeyChar == '%' || e.KeyChar == '"' || e.KeyChar == (char)Keys.Back);
            //  e.Handled = !(char.IsLetterOrDigit(e.KeyChar) || e.KeyChar == '/' || e.KeyChar == ' '  || e.KeyChar == '"' || e.KeyChar == '(' || e.KeyChar == ')' || e.KeyChar == (char)Keys.Back);
            //  e.Handled = !(char.IsLetterOrDigit(e.KeyChar) || e.KeyChar == '/' || e.KeyChar == ' '  || e.KeyChar == (char)Keys.Back);
            //   e.Handled = !(char.IsLetterOrDigit(e.KeyChar) || e.KeyChar == '/' || e.KeyChar == ' '  || e.KeyChar == '%' || e.KeyChar == '"' || e.KeyChar == (char)Keys.Back);
            //  e.Handled = !(char.IsLetterOrDigit(e.KeyChar) || e.KeyChar == '-' || e.KeyChar == '"' || e.KeyChar == '(' || e.KeyChar == ')' || e.KeyChar == (char)Keys.Back);
            //e.Handled = !(char.IsLetterOrDigit(e.KeyChar) || e.KeyChar == '-' || e.KeyChar == ' ' || e.KeyChar == '%' || e.KeyChar == '.' || e.KeyChar == '"' || e.KeyChar == '(' || e.KeyChar == ')' || e.KeyChar == (char)Keys.Back);

        }

       

        private void butsearch_Click(object sender, EventArgs e)
        {
          
            dateload(dateTimePicker1.Value.ToString("dd-MM-yyyy"));
            Class.Users.UserTime = 0;
        }

       

        private void combocompcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                combobrand.Select();
            }
        }

        private void combobrand_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                comboseason.Select();
            }
        }

        private void combobuycode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                combodept.Select();
            }
        }

        private void comboseason_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                combodept.Select();


            }
        }

        private void combodept_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                combocategory.Select();


            }
        }

        private void combostyle_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                combosampletype.Select();


            }
        }

        private void comboSAMPLETYPE_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                combofabric.Select();


            }
        }

      

        private void combofabric_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                combofabcontent.Select();


            }
        }

        private void combofabcontent_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                combocounts.Select();

            }
        }

        private void combocounts_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                combogg.Select();

            }
        }

        private void combogg_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                combogsm.Select();


            }
        }

        private void combogsm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                combocolor.Select();

            }
        }

        private void combocolor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                combopacktype.Select();


            }
        }

        private void combosize_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                combocurrency.Select();


            }
        }

        private void combocurrency_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtrisk1.Select();


            }
        }

       

        private void combocategory_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                combostyle.Select();


            }
        }

        private void combocompcode_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            combocompcode.BackColor = Color.White;
        }

      

      
        private void comboseason_Click(object sender, EventArgs e)
        {
            comboseason.BackColor = Color.White;
        }

       

        private void combocategory_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            
            combocategory.BackColor = Color.White;
        }

        private void combostyle_Click(object sender, EventArgs e)
        {
            combostyle.BackColor = Color.White;
        }

        private void comboSAMPLETYPE_TextChanged(object sender, EventArgs e)
        {
            combosampletype.BackColor = Color.White;
        }

        private void combofabric_TextChanged(object sender, EventArgs e)
        {
            combofabric.BackColor = Color.White;
        }

        private void combofabcontent_TextChanged(object sender, EventArgs e)
        {
            combofabcontent.BackColor = Color.White;
        }

        private void combocounts_SelectedIndexChanged(object sender, EventArgs e)
        {
            combocounts.BackColor = Color.White;
        }

        private void combogsm_TextChanged(object sender, EventArgs e)
        {
            combogsm.BackColor = Color.White;
        }

        private void combocolor_SelectedIndexChanged(object sender, EventArgs e)
        {
            combocolor.BackColor = Color.White;
        }

        private void combosize_TextChanged(object sender, EventArgs e)
        {
            combosize.BackColor = Color.White;
        }

        private void combocurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            combocurrency.BackColor = Color.White;
            CurrencyLoad(combocurrency.Text);
           
        }

        private void combobrand_SelectedIndexChanged(object sender, EventArgs e)
        {
            combobrand.BackColor = Color.White;
        }

        private void combopacktype_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                combosize.Select();


            }
        }

    

        private void compcodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            compcode();
        }

        private void brandNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BrandLoad();
        }

        private void seasonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SeasonLoad();
        }

        private void departmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DepartmentLoad();
        }

      

        private void gGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GaugeLoad();
        }

       

        private void orderPackTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OrderPackLoad();
        }

        private void currencyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrencyLoad();
        }

        private void refreshToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            listviewbuyqrcode.Items.Clear(); listfilter.Items.Clear();
            GridLoad(); txtsearch.Select();
            Cursor = Cursors.Default; Class.Users.UserTime = 0;
        }

        private void combomfyear_SelectedIndexChanged(object sender, EventArgs e)
        {
            combomfyear.BackColor = Color.White;
        }

        private void combodept_SelectedIndexChanged(object sender, EventArgs e)
        {
            
                string sel0 = "SELECT  B.ASPTBLSAMCATMASID,B.CATEGORY    FROM  ASPTBLSAMDEPTMAS  A JOIN ASPTBLSAMCATMAS B ON A.ASPTBLSAMDEPTMASID=B.DEPARTMENT   WHERE A.ACTIVE='T' and A.DEPARTMENT='" + combodept.Text + "' ";
                DataSet ds0 = Utility.ExecuteSelectQuery(sel0, "ASPTBLSAMCATMAS");
                DataTable dt0 = ds0.Tables["ASPTBLSAMCATMAS"];
                if (dt0.Rows.Count > 0)
                {
                    combocategory.DataSource = dt0;
                    combocategory.DisplayMember = "CATEGORY";
                    combocategory.ValueMember = "ASPTBLSAMCATMASID";

                }
                else
                {
                    combocategory.DataSource = null;
                    combocategory.Text = "";
                }
          
        }
        // public CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer1;
       
        private void reportshow()
        {
          
            //this.cr1.crystalReportViewer1 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            //this.cr1.crystalReportViewer1.ActiveViewIndex = -1;
            //this.cr1.crystalReportViewer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            //| System.Windows.Forms.AnchorStyles.Left)
            //| System.Windows.Forms.AnchorStyles.Right)));
            //this.cr1.crystalReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            //this.cr1.crystalReportViewer1.Cursor = System.Windows.Forms.Cursors.Default;
            //this.cr1.crystalReportViewer1.Location = new System.Drawing.Point(6, 38);
            //this.cr1.crystalReportViewer1.Name = "crystalReportViewer1";
            //this.cr1.crystalReportViewer1.Size = new System.Drawing.Size(1280, 546);
            //this.cr1.crystalReportViewer1.TabIndex = 18;
            //this.cr1.crystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
           
            //this.tabPage2.Controls.Add(this.cr1.crystalReportViewer1);
        }

        private void butfront_Click(object sender, EventArgs e)
        {
            try
            {
               
                Class.Users.UserTime = 0;
                if (dtgeneral.Rows.Count >= 1)
                {
                    //var orderedRows = from row in dtgeneral.AsEnumerable() select row;
                    //reversedDt = orderedRows.CopyToDataTable();
                    //if (reversedDt.Rows.Count > 0)
                    //{
                    //    reversedDt2.Rows.Clear();
                    //    reversedDt1.Rows.Clear();
                    //}

                    //cnt = 0;

                    //foreach (DataRow dr in dtgeneral.Rows)
                    //{


                    //    if (cnt % 2 == 0)
                    //    {
                    //        reversedDt1.Rows.Add(dr.ItemArray);
                    //        reversedDt1.AcceptChanges();
                    //    }
                    //    else
                    //    {
                    //        reversedDt2.Rows.Add(dr.ItemArray);
                    //        reversedDt2.AcceptChanges();
                    //    }


                    //    cnt++;
                    //}
                    var orderedRows = from row in dtgeneral.AsEnumerable() select row;
                    reversedDt = orderedRows.CopyToDataTable();
                    if (reversedDt.Rows.Count > 0)
                    {
                        reversedDt2.Rows.Clear();
                        reversedDt1.Rows.Clear();
                    }

                    cnt = 0;

                    foreach (DataRow dr in dtgeneral.Rows)
                    {


                        if (cnt % 2 == 0)
                        {
                            reversedDt1.Rows.Add(dr.ItemArray);
                            reversedDt1.AcceptChanges();
                        }
                        else
                        {
                            reversedDt2.Rows.Add(dr.ItemArray);
                            reversedDt2.AcceptChanges();
                        }


                        cnt++;
                    }
                    if (reversedDt1.Rows.Count >= 2)
                    {
                        //F:\Pinnacle-Oracle55\Report\Sample\SampleCollectionMultiple.rpt
                        crystalReportViewer3.ReportSource = null;
                        CrystalDecisions.CrystalReports.Engine.ReportDocument reportdocument = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                        reportdocument.Load(Application.StartupPath + "\\Report\\Sample\\SampleCollectionMultiple.rpt");
                        reportdocument.Database.Tables["DataTable1"].SetDataSource(reversedDt1);
                        reportdocument.Database.Tables["DataTable11"].SetDataSource(reversedDt2);
                        crystalReportViewer3.ReportSource = reportdocument;
                        crystalReportViewer3.Refresh();




                    }
                    if (reversedDt1.Rows.Count <= 2)
                    {

                        crystalReportViewer3.ReportSource = null;
                        Report.Sample.SampleCollectionPrintFormate reportdocument = new Report.Sample.SampleCollectionPrintFormate();
                        reportdocument.Database.Tables["DataTable1"].SetDataSource(reversedDt1);
                        reportdocument.Database.Tables["DataTable11"].SetDataSource(reversedDt2);
                        crystalReportViewer3.ReportSource = reportdocument;
                        crystalReportViewer3.Refresh();
                       // reportdocument.PrintOptions.PrinterName = printDialog1.PrinterSettings.PrinterName;
                      //  reportdocument.PrintToPrinter(printDialog1.PrinterSettings.Copies, printDialog1.PrinterSettings.Collate, printDialog1.PrinterSettings.FromPage, printDialog1.PrinterSettings.ToPage);

                    }

                    //if (reversedDt1.Rows.Count > 2)
                    //{
                    //    CrystalDecisions.CrystalReports.Engine.ReportDocument reportdocument1 = new CrystalDecisions.CrystalReports.Engine.ReportDocument();

                    //    crystalReportViewer3.ReportSource = null;
                    //    reportdocument1.Load(Application.StartupPath + "\\Report\\Sample\\SampleCollectionMultiple.rpt");
                    //    reportdocument1.Database.Tables["DataTable11"].SetDataSource(reversedDt2);
                    //    crystalReportViewer3.ReportSource = reportdocument1;
                    //    crystalReportViewer3.Refresh();

                    //   // reportdocument.PrintOptions.PrinterName = printDialog1.PrinterSettings.PrinterName;
                    //   // reportdocument.PrintToPrinter(printDialog1.PrinterSettings.Copies, printDialog1.PrinterSettings.Collate, printDialog1.PrinterSettings.FromPage, printDialog1.PrinterSettings.ToPage);



                    //}
                    //if (reversedDt1.Rows.Count <= 2)
                    //{

                    //    crystalReportViewer3.ReportSource = null;
                    //     reportdocument.Load(Application.StartupPath + "\\Report\\Sample\\SampleCollectionUserControlCrystalReport.rpt");
                    //                           reportdocument.Database.Tables["DataTable11"].SetDataSource(reversedDt2);
                    //    crystalReportViewer3.ReportSource = reportdocument;
                    //    crystalReportViewer3.Refresh();
                    //    //reportdocument.PrintOptions.PrinterName = printDialog1.PrinterSettings.PrinterName;
                    //   // reportdocument.PrintToPrinter(printDialog1.PrinterSettings.Copies, printDialog1.PrinterSettings.Collate, printDialog1.PrinterSettings.FromPage, printDialog1.PrinterSettings.ToPage);

                    //}

                }
                else
                {
                    MessageBox.Show("Minimum TWO Sample should be Choose in Checkbox !.");
                    tabbuyer.SelectTab(tabbuyer1);
                }
            }
            catch (Exception EX)
            {
                MessageBox.Show("--------------------Install CrystalReport---------------------------" + EX.ToString(),"Install Crystal Report",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
        }

        private void listviewbuyqrcode_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            try
            {
                i = 0;
                Cursor = Cursors.WaitCursor;
                ListViewItem it2 = new ListViewItem();

                if (e.Item.Checked == true)
                {
                    e.Item.SubItems[0].Text = "T";
                    e.Item.BackColor = System.Drawing.SystemColors.MenuHighlight;
                    e.Item.ForeColor = Color.White;
                    it2.SubItems.Add(e.Item.SubItems[1].Text);
                    it2.SubItems.Add(e.Item.SubItems[2].Text);
                    it2.SubItems.Add(e.Item.SubItems[3].Text);
                    it2.SubItems.Add(e.Item.SubItems[4].Text);
                    it2.SubItems.Add(e.Item.SubItems[5].Text);
                    it2.SubItems.Add(e.Item.SubItems[6].Text);
                    it2.SubItems.Add(e.Item.SubItems[7].Text);
                    allip.Items.Add(it2);


                    this.listfilter1.Items.Add((ListViewItem)it2.Clone());
                    //-----------------------------------------------------------------------------------
                    try
                    {

                        string sel = "SELECT A.ASPTBLBUYSAMID,  A.DATE1,B.COMPCODE,C.BRAND,A.AGFSAMPLE,E.SEASON,F.DEPARTMENT || '(' ||  O.CATEGORY || ')' AS DEPARTMENT,Q.SAMPLETYPE AS CATEGORY,A.STYLENAME,A.FABRIC,A.FABRICCONTENT,A.COUNTS, K.GG AS GAUGE,A.GSM, A.COLORNAME,P.ORDERPACKTYPE,A.SIZENAME,N.CURRENCYNAME,A.REMARKS,A.ACTIVE,A.GARMENTIMAGE,A.QRCODE, A.RISK1,A.RISK2,A.RISK3,A.RISK4,A.RISK5,A.FABRICCOMPLIANT,A.REMARKS,A.MFYEAR    FROM ASPTBLBUYSAM A JOIN GTCOMPMAST B ON A.COMPCODE=B.GTCOMPMASTID  join asptblBRANDMAS C ON C.ASPTBLBRANDMASID=A.BRAND  JOIN ASPTBLSEASONMAS E ON E.ASPTBLSEASONMASID=A.SEASON JOIN ASPTBLSAMDEPTMAS F ON F.ASPTBLSAMDEPTMASID=A.DEPARTMENT     JOIN ASPTBLGGMAS K ON K.ASPTBLGGMASID=A.GAUGE      JOIN ASPTBLCURMAS N ON N.ASPTBLCURMASID=A.CURRENCYNAME JOIN ASPTBLSAMCATMAS O ON O.ASPTBLSAMCATMASID=A.CATEGORY  JOIN ASPTBLORDPACKMAS  P ON P.ASPTBLORDPACKMASID=A.ORDERPACKTYPE JOIN ASPTBLSAMTYPEMAS Q ON Q.ASPTBLSAMTYPEMASID=A.SAMPLETYPE where A.ACTIVE='T' AND  A.ASPTBLBUYSAMID=" + Convert.ToInt64("0" + e.Item.SubItems[2].Text);
                        DataSet ds2 = Utility.ExecuteSelectQuery(sel, "ASPTBLBUYSAM");
                        DataTable dt = ds2.Tables["ASPTBLBUYSAM"];

                        if (dtgeneral.Rows.Count == 0)
                        {
                            dtgeneral = dt.Clone();
                            reversedDt1 = dt.Clone();
                            reversedDt2 = dt.Clone();
                        }
                        foreach (DataRow row in dt.Rows)
                        {
                            dtgeneral.Rows.Add(row.ItemArray);

                        }


                        foreach (DataRow myRow in dt.Rows)
                        {

                            idcardcount += Convert.ToString(myRow["ASPTBLBUYSAMID"].ToString()) + ",";

                        }

                    }
                    catch (Exception ex)
                    {
                        Cursor = Cursors.Default;
                    }

                    //------------------------------------------------------------------------------------
                }
                if (e.Item.Checked == false && e.Item.SubItems[0].Text == "T")
                {
                    idcardcount = "";
                    e.Item.SubItems[0].Text = "F"; e.Item.BackColor = System.Drawing.SystemColors.ControlLightLight;
                    e.Item.ForeColor = Color.Black;
                    for (int c = 0; c < allip.Items.Count; c++)
                    {
                        DataRow dr = dtgeneral.Rows[i];
                        if (e.Item.SubItems[3].Text == allip.Items[c].SubItems[3].Text)
                        {


                            dr.Delete();
                            dtgeneral.AcceptChanges();


                            allip.Items[c].Remove();
                            e.Item.SubItems[0].Text = "";

                            c--;

                            Cursor = Cursors.Default;

                        }
                        idcardcount += Convert.ToString(allip.Items[c].SubItems[3].Text) + ",";

                    }

                }


            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                //   MessageBox.Show("       ---  " + e.Item.ToString() + "             ======================" + ex.ToString());

            }
            Cursor = Cursors.Default;
        }

        private void butprint_Click(object sender, EventArgs e)
        {
            try
            {
              
                reportshow(); Class.Users.UserTime = 0;
                if (dtgeneral.Rows.Count >=1)
                {
                    var orderedRows = from row in dtgeneral.AsEnumerable() select row;
                    reversedDt = orderedRows.CopyToDataTable();
                    if (reversedDt.Rows.Count > 0)
                    {
                        reversedDt2.Rows.Clear();
                        reversedDt1.Rows.Clear();
                    }

                    cnt = 0;

                    foreach (DataRow dr in dtgeneral.Rows)
                    {


                        if (cnt % 2 == 0)
                        {
                            reversedDt1.Rows.Add(dr.ItemArray);
                            reversedDt1.AcceptChanges();
                        }
                        else
                        {
                            reversedDt2.Rows.Add(dr.ItemArray);
                            reversedDt2.AcceptChanges();
                        }


                        cnt++;
                    }

                    if (reversedDt1.Rows.Count >= 1)
                    {
                       
                        CrystalDecisions.CrystalReports.Engine.ReportDocument reportdocument = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                        reportdocument.Load(Application.StartupPath + "\\Report\\Sample\\SampleCollectionUserControlCrystalReport.rpt");
                        reportdocument.Database.Tables["DataTable11"].SetDataSource(reversedDt2);
                        crystalReportViewer3.ReportSource = reportdocument;
                        crystalReportViewer3.Refresh();                       
                     //   reportdocument.PrintOptions.PrinterName = printDialog1.PrinterSettings.PrinterName;
                     //   reportdocument.PrintToPrinter(printDialog1.PrinterSettings.Copies, printDialog1.PrinterSettings.Collate, printDialog1.PrinterSettings.FromPage, printDialog1.PrinterSettings.ToPage);


                    }


                }
                else 
                {
                    MessageBox.Show("No Data Found !.");
                    tabbuyer.SelectTab(tabbuyer1);
                }
            }
            catch (Exception EX)
            {
                MessageBox.Show("butfront_Click" + EX.ToString());
            }

           
        }

        private void combobrandsearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (combobrandsearch.Text != "System.Data.DataRowView" && combobrandsearch.Text != "")
            {
                GridLoad(combobrandsearch.Text); Class.Users.UserTime = 0;
            }
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (listviewbuyqrcode.Items.Count > 0)
                {
                    Class.Users.UserTime = 0;
                    buy.ASPTBLBUYSAMID = Convert.ToInt64(listviewbuyqrcode.SelectedItems[0].SubItems[2].Text);
                    string sel0 = "SELECT   B.COMPCODE,C.BRAND,E.SEASON,F.DEPARTMENT,O.CATEGORY,A.STYLENAME,Q.SAMPLETYPE,A.FABRIC,A.FABRICCONTENT,A.COUNTS, K.GG AS GAUGE,A.GSM, A.COLORNAME," +
                        "P.ORDERPACKTYPE,A.SIZENAME,N.CURRENCYNAME,A.REMARKS,A.ACTIVE,A.GARMENTIMAGE,A.QRCODE,A.RISK1,A.RISK2,A.RISK3,A.RISK4,A.RISK5,A.FABRICCOMPLIANT,A.REMARKS,A.MFYEAR, A.RATE   FROM ASPTBLBUYSAM A JOIN GTCOMPMAST B ON A.COMPCODE=B.GTCOMPMASTID join asptblBRANDMAS C ON C.ASPTBLBRANDMASID=A.BRAND  JOIN ASPTBLSEASONMAS E ON E.ASPTBLSEASONMASID=A.SEASON JOIN ASPTBLSAMDEPTMAS F ON F.ASPTBLSAMDEPTMASID=A.DEPARTMENT  JOIN ASPTBLGGMAS K ON K.ASPTBLGGMASID=A.GAUGE    JOIN ASPTBLCURMAS N ON N.ASPTBLCURMASID=A.CURRENCYNAME JOIN ASPTBLSAMCATMAS O ON O.ASPTBLSAMCATMASID=A.CATEGORY  JOIN ASPTBLORDPACKMAS  P ON P.ASPTBLORDPACKMASID=A.ORDERPACKTYPE join ASPTBLSAMTYPEMAS Q ON Q.ASPTBLSAMTYPEMASID=A.SAMPLETYPE where A.ASPTBLBUYSAMID=" + Convert.ToInt64("0" + buy.ASPTBLBUYSAMID);
                    DataSet ds0 = Utility.ExecuteSelectQuery(sel0, "ASPTBLBUYSAM");
                    DataTable dt0 = ds0.Tables["ASPTBLBUYSAM"];
                    myString = "";

                    txtbuysamid.Text = "";txtcode.Text = "";
                   
                    combocompcode.Text = dt0.Rows[0]["COMPCODE"].ToString();
                    combobrand.Text = dt0.Rows[0]["BRAND"].ToString();
                 
                    comboseason.Text = dt0.Rows[0]["SEASON"].ToString();
                    combodept.Text = dt0.Rows[0]["DEPARTMENT"].ToString();
                    combocategory.Text = dt0.Rows[0]["CATEGORY"].ToString();
                    combostyle.Text = dt0.Rows[0]["STYLENAME"].ToString();
                    combosampletype.Text = dt0.Rows[0]["SAMPLETYPE"].ToString();
                    combofabric.Text = dt0.Rows[0]["FABRIC"].ToString();
                    combofabcontent.Text = dt0.Rows[0]["FABRICCONTENT"].ToString();
                    combocounts.Text = dt0.Rows[0]["COUNTS"].ToString();
                    combogg.Text = dt0.Rows[0]["GAUGE"].ToString();
                    combogsm.Text = dt0.Rows[0]["GSM"].ToString();
                    combocolor.Text = dt0.Rows[0]["COLORNAME"].ToString();
                    combopacktype.Text = dt0.Rows[0]["ORDERPACKTYPE"].ToString();
                    combosize.Text = dt0.Rows[0]["SIZENAME"].ToString();
                    combocurrency.Text = dt0.Rows[0]["CURRENCYNAME"].ToString();
                    txtrisk2.Text = dt0.Rows[0]["RISK2"].ToString();
                    txtrisk3.Text = dt0.Rows[0]["RISK3"].ToString();
                    txtrisk4.Text = dt0.Rows[0]["RISK4"].ToString();
                    txtrisk5.Text = dt0.Rows[0]["RISK5"].ToString();
                    txtfabcompliant.Text = dt0.Rows[0]["FABRICCOMPLIANT"].ToString();
                    txtremarks.Text = dt0.Rows[0]["REMARKS"].ToString();
                    combomfyear.Text = dt0.Rows[0]["MFYEAR"].ToString();
                    if (dt0.Rows[0]["ACTIVE"].ToString() == "T") checkbuy.Checked = true; else checkbuy.Checked = false;
                    combocurrency.Text = dt0.Rows[0]["CURRENCYNAME"].ToString();

                    txtrate.Text = dt0.Rows[0]["RATE"].ToString();
                    picturegrcode.Image = null;
                    picturegarmentimage.Image = null;
                }

            }
            catch (Exception ex)
            {
               
            }

        }

        
        private void sampleTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SampleTypeLoad();
        }

        private void tabbuyer1_Click(object sender, EventArgs e)
        {
            Class.Users.UserTime = 0;
        }

        public void Searchs(int id)
        {
            throw new NotImplementedException();
        }

        public void Deletes(int id)
        {
            throw new NotImplementedException();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (picturegarmentimage.Image != null)
            {
                Class.Users.TableName ="STYLE: "+ " +" +combostyle.Text+"   "+  " REF NO :   "+ txtcode.Text+ "  " +" BUYER:    "+ combodept.Text;
                Class.Users.StaticPicture = picturegarmentimage;
                Master.SampleCollection.PopUp pop = new Master.SampleCollection.PopUp();
                pop.Show();
            }
        }
    }
}
