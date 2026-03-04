using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Windows.Forms;

namespace Pinnacle.Canteen
{
    public partial class Token : Form, ToolStripAccess
    {
      
        public Token()
        {
            InitializeComponent();
            Class.Users.IPADDRESS = GenFun.GetLocalIPAddress();
            Class.Users.CREATED = Convert.ToDateTime(System.DateTime.Now.ToString("dd-MMM-yyyy") + " " + System.DateTime.Now.ToLongTimeString()); 
            //this.panelprint.Hide();
            //dateTimePicker1.MinDate = DateTime.Today;
            //dateTimePicker1.MaxDate = DateTime.Now.AddDays(1);
            string s = Class.Users.CANTEENMENUNAME.ToString();
            string[] data = s.Split('\r');
            string[] data1 = data[1].Split('\n');
            string[] data2 = data[2].Split('\n');
            Class.Users.DateTimes =Convert.ToDateTime(data1[1].ToString());
            Class.Users.CANTEENMENUNAME = data[0].ToString();
           dateTimePicker1.Value= Convert.ToDateTime(data1[1].ToString()+" "+ data2[1].ToString());
            //if (System.DateTime.Now.ToString("dd-MM-yyyy") == dateTimePicker1.Value.ToString("dd-MM-yyyy"))
            //{ dateTimePicker1.Enabled = true; }
            //else
            //{
            //    dateTimePicker1.Enabled = false;
            //}


        }
        // private static Token _instance;
        Models.Master mas = new Models.Master();
        Models.UserRights sm = new Models.UserRights();
        Models.MenuName c = new Models.MenuName();
        Pinnacle.Models.MailModel obj = new Models.MailModel(); 
        QRCoder.QRCodeGenerator qc = new QRCoder.QRCodeGenerator();
        
       
        
        decimal amt = 0;
        public zkemkeeper.CZKEM axCZKEM1 = new zkemkeeper.CZKEM();
        private bool bIsConnected = false;//the boolean value identifies whether the device is connected
        private int iMachineNumber = 1;//the serial number of the device.After connecting the device ,this value will be changed.      
      
      
        private void Token_Load(object sender, EventArgs e)
        {
            try
            {
                panel1.BackColor = Class.Users.BackColors;
                this.BackColor = Class.Users.BackColors;
                this.Font = Class.Users.FontName;
                combooptions.SelectedIndex = 0;
                btntokenprint.BackColor = Class.Users.BackColors;
                btnexit.BackColor = Class.Users.BackColors;


                if (Convert.ToInt64(Class.Users.TOKENEMPID) > 0)
                {
                    string sel1 = "SELECT  A.HREMPLOYMASTID,A.FNAME,B.MIDCARD,c.compcode FROM HREMPLOYMAST A JOIN HREMPLOYDETAILS B ON A.HREMPLOYMASTID=B.HREMPLOYMASTID join gtcompmast c on c.gtcompmastid=a.compcode  WHERE B.MIDCARD='" + Class.Users.TOKENEMPID + "'  AND B.IDACTIVE='YES'";//AND c.gtcompmastid='" + Class.Users.COMPCODE + "'

                    DataSet ds1 = Utility.ExecuteSelectQuery(sel1, "HREMPLOYMAST");
                    DataTable dt1 = ds1.Tables["HREMPLOYMAST"];

                    txtempid.Text = dt1.Rows[0]["HREMPLOYMASTID"].ToString();

                    comboempname.DisplayMember = "FNAME";
                    comboempname.ValueMember = "HREMPLOYMASTID";

                    comboidcardno.DisplayMember = "MIDCARD";
                    comboidcardno.ValueMember = "MIDCARD";
                    comboempname.DataSource = dt1;
                    comboidcardno.DataSource = dt1;
                    comboemptype.SelectedIndex = 0;
                    lblcompcode2.Text = dt1.Rows[0]["compcode"].ToString();
                    lblempname2.Text = dt1.Rows[0]["FNAME"].ToString() + " ( " + dt1.Rows[0]["MIDCARD"].ToString() + " ) ";
                    //lblempid2.Text = dt1.Rows[0]["MIDCARD"].ToString();
                    lbldate2.Text = Convert.ToDateTime(dateTimePicker1.Value.ToString()).ToString("dd-MM-yyyy");
                    lblqty2.Text = txtQuantity.Value.ToString();
                    lblnoofdays2.Text = txtDays.Value.ToString(); ;
                    lblcompcode.Text = Class.Users.HCompName;
                    lbldatetime.Text = System.DateTime.Now.ToString();
                    string sel = "SELECT C.ASPTBLCANITEMMASID,C.ITEMCODE, C.ITEMNAME1,C.EMPLOYEECOST ,C.SPECIALCOST ,MAX(A.DOCDATE) AS DOCDATE,e.category FROM ASPTBLMENPER A JOIN ASPTBLMENPERDET B ON A.ASPTBLMENPERID=B.ASPTBLMENPERID     JOIN ASPTBLCANITEMMAS C ON C.ASPTBLCANITEMMASID=B.ITEMNAME1 AND C.ITEMCODE=B.ITEMCODE       JOIN ASPTBLUSERMAS D ON  D.USERID=A.USERNAME  AND D.COMPCODE=A.COMPCODE join asptblcancategorymas e on e.asptblcancategorymasid=b.category  WHERE  A.USERNAME='" + Class.Users.USERID + "' AND C.ITEMNAME1='" + Class.Users.CANTEENMENUNAME + "' AND B.ACTIVE='T'        GROUP BY C.ASPTBLCANITEMMASID,C.ITEMCODE, C.ITEMNAME1,C.EMPLOYEECOST ,C.SPECIALCOST,e.category";//A.COMPCODE='" + Class.Users.COMPCODE + "'  AND
                    DataSet ds = Utility.ExecuteSelectQuery(sel, "ASPTBLCANITEMMAS");
                    DataTable dt = ds.Tables["ASPTBLCANITEMMAS"];
                    comboitemcode.DisplayMember = "ITEMCODE";
                    comboitemcode.ValueMember = "ASPTBLCANITEMMASID";
                    comboitemname.DisplayMember = "ITEMNAME1";
                    comboitemname.ValueMember = "ASPTBLCANITEMMASID";
                    txtitemcost.Text = "";
                    lblempid2.Text = dt.Rows[0]["category"].ToString();
                    comboitemcode.DataSource = dt;
                    comboitemname.DataSource = dt;
                    lblitemname2.Text = dt.Rows[0]["ITEMNAME1"].ToString();
                    lbltoken2.Text = dt.Rows[0]["ASPTBLCANITEMMASID"].ToString();

                    Combooptions_SelectedIndexChanged(sender, e);
                    TxtDays_ValueChanged(sender, e);
                    Saves();
                    Class.Users.CANTEENMENUNAME = ""; Class.Users.TOKENEMPID = 0;
                    

                }
            }
            catch (Exception ex) { }
            finally
            {
                this.Close();
            }
        }

        private void Token_FormClosed(object sender, FormClosedEventArgs e)
        {
            // _instance = null;
            this.Dispose();
           
        }

        public static decimal Sum(decimal num1, decimal num2)
        {
            Decimal total;
            total = num1 * num2;
            return total;
        }
        public static decimal add(decimal num1, decimal num2)
        {
            decimal total;
            total = num1 + num2;
            return total;
        }
        public static decimal add(decimal num1, decimal num2, decimal num3)
        {
            decimal total;
            total = num1 + num2+num3;
            return total;
        }
        public static decimal Sum(decimal num1, decimal num2,decimal num3)
        {
            decimal total;
            total = num1 * num2 * num3;
            return total;
        }
        //private void TxtQuantity_TextChanged(object sender, EventArgs e)
        //{
         
        //}

        //private void TxtDays_TextChanged(object sender, EventArgs e)
        //{
          
        //}
        private void TxtQuantity_ValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtQuantity.Value)<=999)
            {
     
                string sel1 = "SELECT  A.HREMPLOYMASTID,A.FNAME,B.MIDCARD FROM HREMPLOYMAST A JOIN HREMPLOYDETAILS B ON A.HREMPLOYMASTID=B.HREMPLOYMASTID   WHERE B.MIDCARD='" + Class.Users.TOKENEMPID + "' AND B.IDACTIVE='YES'";

                DataSet ds1 = Utility.ExecuteSelectQuery(sel1, "HREMPLOYMAST");
                DataTable dt1 = ds1.Tables["HREMPLOYMAST"];
                if (dt1.Rows.Count > 0)
                {
                    string sel = "SELECT C.ASPTBLCANITEMMASID,C.ITEMCODE, C.ITEMNAME1,C.EMPLOYEECOST,C.SPECIALCOST ,MAX(A.TODATE) AS TODATE,e.category FROM ASPTBLMENPER A JOIN ASPTBLMENPERDET B ON A.ASPTBLMENPERID=B.ASPTBLMENPERID     JOIN ASPTBLCANITEMMAS C ON C.ASPTBLCANITEMMASID=B.ITEMNAME1 AND C.ITEMCODE=B.ITEMCODE   JOIN ASPTBLUSERMAS D ON  D.USERID=A.USERNAME  AND D.COMPCODE=A.COMPCODE join asptblcancategorymas e on e.ASPTBLCANCATEGORYMASID=b.category  WHERE A.COMPCODE='" + Class.Users.COMPCODE + "'  AND A.USERNAME='" + Class.Users.USERID + "' AND C.ITEMNAME1='" + Class.Users.CANTEENMENUNAME + "' AND B.ACTIVE='T'        GROUP BY C.ASPTBLCANITEMMASID,C.ITEMCODE, C.ITEMNAME1,C.EMPLOYEECOST,C.SPECIALCOST,e.category";

                    DataSet ds = Utility.ExecuteSelectQuery(sel, "ASPTBLCANITEMMAS");
                    DataTable dt = ds.Tables["ASPTBLCANITEMMAS"];
                    if (dt.Rows.Count > 0)
                    {
                      
                            if (System.DateTime.Now.ToString("dd-MM-yyyy") == dateTimePicker1.Value.ToString("dd-MM-yyyy"))
                            {
                                decimal t1 = add(Convert.ToDecimal("0" + dt.Rows[0]["EMPLOYEECOST"].ToString()), Convert.ToDecimal("0" + dt.Rows[0]["SPECIALCOST"].ToString()));
                                decimal totamt = Sum(Convert.ToDecimal("0" + txtQuantity.Value), t1, Convert.ToDecimal("0" + txtDays.Value));

                               // lblitemcost.Text = "Rate : " + totamt;
                                lblqty2.Text = txtQuantity.Value + " / Rate: " + t1;
                                obj.Amount = lblqty2.Text;
                            lblempid2.Text = dt.Rows[0]["category"].ToString();
                                txtTotalAmount.Text = totamt.ToString(); txtitemcost.Text = t1.ToString();
                            }
                            else
                            {
                                decimal totamt = Sum(Convert.ToDecimal("0" + dt.Rows[0]["EMPLOYEECOST"].ToString()), Convert.ToDecimal("0" + txtQuantity.Value), Convert.ToDecimal("0" + txtDays.Value));
                                //lblitemcost.Text = "Rate : " + totamt;
                                lblqty2.Text = txtQuantity.Value + " / Rate: " + dt.Rows[0]["EMPLOYEECOST"].ToString();
                                obj.Amount = lblqty2.Text;
                            lblempid2.Text = dt.Rows[0]["category"].ToString();
                            txtTotalAmount.Text = totamt.ToString(); txtitemcost.Text = dt.Rows[0]["EMPLOYEECOST"].ToString();
                            }
                        
                    }

                }


                lblqty2.Text = txtQuantity.Value.ToString();
                lblnoofdays2.Text = txtDays.Value.ToString();
                Combooptions_SelectedIndexChanged(sender, e);
            }
            else
            {
                txtQuantity.Value = 1;
                Combooptions_SelectedIndexChanged(sender, e);
            }
           
        }
        private void TxtDays_ValueChanged(object sender, EventArgs e)
        {
            try { 
            string sel1 = "SELECT  A.HREMPLOYMASTID,A.FNAME,B.MIDCARD FROM HREMPLOYMAST A JOIN HREMPLOYDETAILS B ON A.HREMPLOYMASTID=B.HREMPLOYMASTID   WHERE B.MIDCARD='" + Class.Users.TOKENEMPID + "' AND B.IDACTIVE='YES'";
            DataSet ds1 = Utility.ExecuteSelectQuery(sel1, "HREMPLOYMAST");
            DataTable dt1 = ds1.Tables["HREMPLOYMAST"];
                if (dt1.Rows.Count > 0)
                {

                    string sel = "SELECT C.ASPTBLCANITEMMASID,C.ITEMCODE, C.ITEMNAME1,C.EMPLOYEECOST ,C.SPECIALCOST,to_date('" + System.DateTime.Now.ToString("dd-MM-yyyy") + "', 'dd-MM-yyyy') AS TODATE ,e.category        FROM ASPTBLMENPER A JOIN ASPTBLMENPERDET B ON A.ASPTBLMENPERID = B.ASPTBLMENPERID            JOIN ASPTBLCANITEMMAS C ON C.ASPTBLCANITEMMASID = B.ITEMNAME1 AND C.ITEMCODE = B.ITEMCODE  JOIN ASPTBLUSERMAS D ON  D.USERID = A.USERNAME  AND D.COMPCODE = A.COMPCODE    join asptblcancategorymas e on e.ASPTBLCANCATEGORYMASID=b.category   WHERE A.COMPCODE='" + Class.Users.COMPCODE + "'  AND A.USERNAME='" + Class.Users.USERID + "' AND C.ITEMNAME1='" + Class.Users.CANTEENMENUNAME + "' AND A.ACTIVE = 'T'";
                    DataSet ds = Utility.ExecuteSelectQuery(sel, "ASPTBLCANITEMMAS");
                        DataTable dt = ds.Tables["ASPTBLCANITEMMAS"];
                        if (dt.Rows.Count > 0)
                        {

                            if (System.DateTime.Now.ToString("dd-MM-yyyy") == dateTimePicker1.Value.ToString("dd-MM-yyyy"))
                            {
                                decimal t1 = add(Convert.ToDecimal("0" + dt.Rows[0]["EMPLOYEECOST"].ToString()), Convert.ToDecimal("0" + dt.Rows[0]["SPECIALCOST"].ToString()));
                                decimal totamt = Sum(Convert.ToDecimal("0" + txtQuantity.Value), t1, Convert.ToDecimal("0" + txtDays.Value));

                                //lblitemcost.Text = "Rate : " + totamt;
                                lblqty2.Text = txtQuantity.Value + " / Rate: " + t1;
                                obj.Amount = lblqty2.Text;
                            lblempid2.Text = dt.Rows[0]["category"].ToString();
                            txtitemcost.Text = t1.ToString();
                                txtTotalAmount.Text = totamt.ToString();
                            }
                            else
                            {
                                decimal totamt = Sum(Convert.ToDecimal("0" + dt.Rows[0]["EMPLOYEECOST"].ToString()), Convert.ToDecimal("0" + txtQuantity.Value), Convert.ToDecimal("0" + txtDays.Value));
                                //lblitemcost.Text = "Rate : " + totamt;
                                lblqty2.Text = txtQuantity.Value + " / Rate: " + dt.Rows[0]["EMPLOYEECOST"].ToString();
                                obj.Amount = lblqty2.Text;
                            lblempid2.Text = dt.Rows[0]["category"].ToString();
                            txtTotalAmount.Text = totamt.ToString(); txtitemcost.Text = dt.Rows[0]["EMPLOYEECOST"].ToString();
                            }
                      
                    }
                   
                }
            }
            catch (Exception ex) { }
        }



        private void Btntokenprint_Click(object sender, EventArgs e)
        {
            try { 
            if (combooptions.SelectedItem != null && combooptions.Text != "")
            {
                if (txtQuantity.Value >= 1 && txtDays.Value >= 1 && combooptions.SelectedItem != null)
                {
                    Combooptions_SelectedIndexChanged(sender,e);
                    TxtDays_ValueChanged(sender, e); 
                        Saves();
                    Class.Users.CANTEENMENUNAME = ""; Class.Users.TOKENEMPID = 0;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Invalid Token", "Info", MessageBoxButtons.OK, MessageBoxIcon.Question);
             

                    GlobalVariables.MdiPanel.Show();
                    GlobalVariables.HeaderName.Text = "";

                    GlobalVariables.TabCtrl.TabPages.RemoveAt(GlobalVariables.TabCtrl.SelectedIndex);
                    this.Hide();
                    CommonFunctions.ShowPopUpForm(Canteen.CanteenItemMaster.Instance, this);
                }
            }
            else
            {
                MessageBox.Show("Pls Select Token Option", "Info", MessageBoxButtons.OK, MessageBoxIcon.Question);
                this.combooptions.Focus();
            }
            }
            catch (Exception ex) { }

        }

        private void Btnexit_Click(object sender, EventArgs e)
        {

            this.Dispose();
            //GlobalVariables.MdiPanel.Show();
            //GlobalVariables.TabCtrl.TabPages.RemoveAt(GlobalVariables.TabCtrl.SelectedIndex);
            //News();
            //GlobalVariables.HeaderName.Text = "";
        }

       

        private void Butok_Click(object sender, EventArgs e)
        {
            Saves();

        }

        private void PrintDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {
                e.Graphics.DrawString(lblheading.Text.ToUpper(), new Font("roboto", 12, FontStyle.Bold), Brushes.DarkBlue, 43, 32);

                e.Graphics.DrawString(lblcompcode1.Text.ToUpper(), new Font("roboto", 9, FontStyle.Bold), Brushes.DarkBlue, 16, 63);
                e.Graphics.DrawString(lblcompcode2.Text.ToUpper(), new Font("roboto", 9, FontStyle.Bold), Brushes.DarkBlue, 105, 63);

                e.Graphics.DrawString(lbltoken1.Text.ToUpper(), new Font("roboto", 9, FontStyle.Bold), Brushes.DarkBlue, 16, 80);
                e.Graphics.DrawString(lbltoken2.Text.ToUpper(), new Font("roboto", 9, FontStyle.Bold), Brushes.DarkBlue, 105, 80);

      

                e.Graphics.DrawString(lblempname1.Text.ToUpper(), new Font("roboto", 9, FontStyle.Bold), Brushes.DarkBlue, 16, 95);
                e.Graphics.DrawString(lblempname2.Text.ToUpper(), new Font("roboto", 9, FontStyle.Bold), Brushes.DarkBlue, 105, 95);

                e.Graphics.DrawString(lblempid1.Text.ToUpper(), new Font("roboto", 9, FontStyle.Bold), Brushes.DarkBlue, 16, 112);
                e.Graphics.DrawString(lblempid2.Text.ToUpper(), new Font("roboto", 9, FontStyle.Bold), Brushes.DarkBlue, 105, 112);

                e.Graphics.DrawString(lblitemname1.Text.ToUpper(), new Font("roboto", 9, FontStyle.Bold), Brushes.DarkBlue, 16, 128);
                e.Graphics.DrawString(lblitemname2.Text.ToUpper(), new Font("roboto", 9, FontStyle.Bold), Brushes.DarkBlue, 105, 128);

                e.Graphics.DrawString(lbldate1.Text.ToUpper(), new Font("roboto", 9, FontStyle.Bold), Brushes.DarkBlue, 16, 144);
                e.Graphics.DrawString(lbldate2.Text.ToUpper(), new Font("roboto", 9, FontStyle.Bold), Brushes.DarkBlue, 105, 144);


                //e.Graphics.DrawString(lblnoofdays1.Text.ToUpper(), new Font("Arial", 9, FontStyle.Bold), Brushes.DarkBlue, 16, 144);
                //e.Graphics.DrawString(lblnoofdays2.Text, new Font("Arial", 9, FontStyle.Bold), Brushes.DarkBlue, 105, 143);

                //e.Graphics.DrawString(lblitemcost.Text.ToUpper(), new Font("Arial", 12, FontStyle.Bold), Brushes.DarkBlue, 72, 164);
                //e.Graphics.DrawImage(pictureBox1.Image, 62, 190, pictureBox1.Width, pictureBox1.Height);


                e.Graphics.DrawString(lblcompcode.Text.ToUpper(), new Font("roboto", 8, FontStyle.Regular), Brushes.DarkBlue, 16, 180);
                e.Graphics.DrawString(lbldatetime.Text, new Font("roboto", 8, FontStyle.Bold), Brushes.DarkBlue, 16, 200);

                //e.Graphics.DrawString(lblheading.Text.ToUpper(), new Font("roboto", 12, FontStyle.Bold), Brushes.DarkBlue, 46, 35);
                //e.Graphics.DrawString(lblcompcode1.Text.ToUpper(), new Font("roboto", 9, FontStyle.Bold), Brushes.DarkBlue, 16, 63);
                //e.Graphics.DrawString(lblcompcode2.Text.ToUpper(), new Font("roboto", 9, FontStyle.Bold), Brushes.DarkBlue, 105, 63);
                //e.Graphics.DrawString(lbltoken1.Text.ToUpper(), new Font("roboto", 9, FontStyle.Bold), Brushes.DarkBlue, 16, 80);
                //e.Graphics.DrawString(lbltoken2.Text.ToUpper(), new Font("roboto", 9, FontStyle.Bold), Brushes.DarkBlue, 105, 80);   

                //e.Graphics.DrawString(lblempname1.Text.ToUpper(), new Font("roboto", 9, FontStyle.Bold), Brushes.DarkBlue, 16, 105);
                //e.Graphics.DrawString(lblempname2.Text.ToUpper(), new Font("roboto", 9, FontStyle.Bold), Brushes.DarkBlue, 105, 105);

                //e.Graphics.DrawString(lblitemname1.Text.ToUpper(), new Font("roboto", 9, FontStyle.Bold), Brushes.DarkBlue, 16, 128);
                //e.Graphics.DrawString(lblitemname2.Text.ToUpper(), new Font("roboto", 9, FontStyle.Bold), Brushes.DarkBlue, 105, 128);

                //e.Graphics.DrawString(lblcompcode.Text.ToUpper(), new Font("roboto", 8, FontStyle.Bold), Brushes.DarkBlue, 16, 120);
               // e.Graphics.DrawString(lbldatetime.Text, new Font("roboto", 8, FontStyle.Bold), Brushes.DarkBlue, 16, 140);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                this.Dispose();
            }
        }

     
        private void Btncancel_Click(object sender, EventArgs e)
        {
          //  this.panelprint.Hide();
        }

      
        private void Combooptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            try { 
           
                txtDays.Enabled = true;
       
              
              

                if (Convert.ToInt64(Class.Users.TOKENEMPID) > 0)
            {
                string sel1 = "SELECT  A.HREMPLOYMASTID,A.FNAME,B.MIDCARD FROM HREMPLOYMAST A JOIN HREMPLOYDETAILS B ON A.HREMPLOYMASTID=B.HREMPLOYMASTID   WHERE B.MIDCARD='" + Class.Users.TOKENEMPID + "' AND B.IDACTIVE='YES'";
                DataSet ds1 = Utility.ExecuteSelectQuery(sel1, "HREMPLOYMAST");
                DataTable dt1 = ds1.Tables["HREMPLOYMAST"];

                txtempid.Text = dt1.Rows[0]["HREMPLOYMASTID"].ToString();
                comboempname.DisplayMember = "FNAME";
                comboempname.ValueMember = "HREMPLOYMASTID";

                comboidcardno.DisplayMember = "MIDCARD";
                comboidcardno.ValueMember = "MIDCARD";
                comboempname.DataSource = dt1;
                comboidcardno.DataSource = dt1;

                                       string sel = "SELECT DISTINCT C.ASPTBLCANITEMMASID,C.ITEMCODE, C.ITEMNAME1,C.EMPLOYEECOST ,C.SPECIALCOST,to_date('" + System.DateTime.Now.ToString("dd-MM-yyyy") + "', 'dd-MM-yyyy') AS TODATE  ,e.category       FROM ASPTBLMENPER A JOIN ASPTBLMENPERDET B ON A.ASPTBLMENPERID = B.ASPTBLMENPERID            JOIN ASPTBLCANITEMMAS C ON C.ASPTBLCANITEMMASID = B.ITEMNAME1 AND C.ITEMCODE = B.ITEMCODE JOIN ASPTBLUSERMAS D ON  D.USERID = A.USERNAME  AND D.COMPCODE = A.COMPCODE   join asptblcancategorymas e on e.ASPTBLCANCATEGORYMASID=b.category    WHERE A.COMPCODE='" + Class.Users.COMPCODE + "'  AND A.USERNAME='" + Class.Users.USERID + "' AND C.ITEMNAME1='" + Class.Users.CANTEENMENUNAME + "' AND A.ACTIVE = 'T'";
                DataSet ds = Utility.ExecuteSelectQuery(sel, "ASPTBLCANITEMMAS");
                DataTable dt = ds.Tables["ASPTBLCANITEMMAS"];
                    if (dt.Rows.Count > 0)
                    {
                        comboitemcode.DisplayMember = "ITEMCODE";
                        comboitemcode.ValueMember = "ASPTBLCANITEMMASID";
                        comboitemname.DisplayMember = "ITEMNAME1";
                        comboitemname.ValueMember = "ASPTBLCANITEMMASID";
                        txtitemcost.Text = "";
                        decimal V1 = Convert.ToDecimal("0" + dt.Rows[0]["EMPLOYEECOST"].ToString());
                        txtempcost.Text = V1.ToString();
                        lblempid2.Text = dt.Rows[0]["category"].ToString();
                        decimal V3 = Convert.ToDecimal("0" + dt.Rows[0]["SPECIALCOST"].ToString());
                        txtspecialcost.Text = V3.ToString();
                        comboitemcode.DataSource = dt;
                        comboitemname.DataSource = dt;
                        if (dt.Rows.Count > 0)
                        {

                            if (System.DateTime.Now.ToString("dd-MM-yyyy") == dateTimePicker1.Value.ToString("dd-MM-yyyy").Substring(0,10))
                            {
                                decimal t1 = add(Convert.ToDecimal("0" + dt.Rows[0]["EMPLOYEECOST"].ToString()), Convert.ToDecimal("0" + dt.Rows[0]["SPECIALCOST"].ToString()));
                                decimal totamt = Sum(Convert.ToDecimal("0" + txtQuantity.Value), t1, Convert.ToDecimal("0" + txtDays.Value));
                          
                                lblqty2.Text = txtQuantity.Value + " / Rate: " + t1;
                                obj.Amount = lblqty2.Text;
                                txtTotalAmount.Text = totamt.ToString();
                            }
                            else
                            {
                                decimal totamt = Sum(Convert.ToDecimal("0" + dt.Rows[0]["EMPLOYEECOST"].ToString()), Convert.ToDecimal("0" + txtQuantity.Value), Convert.ToDecimal("0" + txtDays.Value));
                               
                                lblqty2.Text = txtQuantity.Value + " / Rate: " + dt.Rows[0]["EMPLOYEECOST"].ToString();
                                obj.Amount = lblqty2.Text;
                                txtTotalAmount.Text = totamt.ToString();
                            }

                        }


                    }

             
                //if (dt1.Rows[0]["EMPIMAGE"].ToString() != "")
                //{

                //    Byte[] bytes = (byte[])dt1.Rows[0]["EMPIMAGE"];
                //    Image img1 = Models.Device.ByteArrayToImage(bytes);
                //    pictureBox2.Image = img1;


                //}
            }
            else
            {
              

            }
            txtQuantity.Select();
            }
            catch (Exception ex) { }
        }

        public void News()
        {
            combooptions.SelectedIndex = 0; txtempcost.Text = "";
            txtcontractconst.Text = ""; txtQuantity.Select();
            txtspecialcost.Text = ""; btntokenprint.BackColor = Class.Users.BackColors;
            btnexit.BackColor = Class.Users.BackColors;

        }
        public void Saves()
        {
            //try
            //{

            //    TimeSpan totaltime;
            //    var currentDateTime = DateTime.Now;
            //    var currentTimeAlone = new TimeSpan(currentDateTime.Hour, currentDateTime.Minute, currentDateTime.Second);
            //    int count = Convert.ToInt32(txtQuantity.Value) * Convert.ToInt32(txtDays.Value);
            //    string token = System.DateTime.Now.Year + "/" + Class.Users.HCompcode + "CAN";
               
            //    decimal totamt = 0;
            //    string chk = ""; if (checkactive.Checked == true) { chk = "T"; } else { chk = "F"; }
              
            //    try
            //    {
            //        //if (combooptions.Text == "SINGLE")
            //       // {
                       
            //            for (int i = 0; i < count; i++)
            //            {
                        
            //                totamt = 0;
            //                string sel2 = "select max(A.ASPTBLCANTOKENID)+1 id FROM ASPTBLCANTOKEN A ";
            //                DataSet ds2 = Utility.ExecuteSelectQuery(sel2, "ASPTBLCANTOKEN");
            //                DataTable dt2 = ds2.Tables["ASPTBLCANTOKEN"];
            //            if (System.DateTime.Now.ToString("dd-MM-yyyy") == dateTimePicker1.Value.ToString("dd-MM-yyyy").Substring(0,10))
            //            {
            //                string ins = "INSERT INTO ASPTBLCANTOKEN(TOKENNO,EMPID,EMPNAME,IDCARDNO,ITEMCODE,ITEMNAME1,ITEMCOST,ITEMQTY,NOOFDAYS ,TOTALAMOUNT,ACTIVE  ,USERNAME,MODIFIED,CREATEDON,IPADDRESS,TOKENNOCANCEL,EMPLOYEETYPE,TOKENOPTION,COMPCODE,FINYEAR,EMPLOYEECOST,SPECIALCOST,tokendate,TOKENTIME,SYSTEMDATE,TOKEN_FROMTIME,category)VALUES('" + token + "/" + dt2.Rows[0]["id"].ToString() + "'," + txtempid.Text + "," + comboempname.SelectedValue + "," + comboidcardno.Text + "," + comboitemcode.SelectedValue + "," + comboitemname.SelectedValue + "," + txtitemcost.Text + "," + txtQuantity.Text + "," + txtDays.Text + "," + txtTotalAmount.Text + ",'" + chk + "'," + Class.Users.USERID + ",'" + Convert.ToString(Class.Users.CREATED) + "','" + Convert.ToString(Class.Users.CREATED) + "','" + Class.Users.IPADDRESS + "','T','" + Class.Users.UniqueID + "','" + combooptions.Text + "','" + Class.Users.COMPCODE + "','" + Class.Users.Finyear + "','" + txtempcost.Text + "','" + txtspecialcost.Text + "',TO_DATE('" + dateTimePicker1.Value.ToString("dd-MM-yyyy") + "','dd-MM-yyyy'),'" + dateTimePicker1.Value.ToString("hh:mm:ss") + "',TO_DATE('" + System.DateTime.Now.ToString() + "','dd-MM-yyyy hh24:mi:ss'),'" + dateTimePicker1.Value.ToString("hh:mm:ss") + "','" + lblempid2.Text + "')";
            //                Utility.ExecuteNonQuery(ins);
            //            }
            //            else
            //            {
            //                string ins = "INSERT INTO ASPTBLCANTOKEN(TOKENNO,EMPID,EMPNAME,IDCARDNO,ITEMCODE,ITEMNAME1,ITEMCOST,ITEMQTY,NOOFDAYS ,TOTALAMOUNT,ACTIVE  ,USERNAME,MODIFIED,CREATEDON,IPADDRESS,TOKENNOCANCEL,EMPLOYEETYPE,TOKENOPTION,COMPCODE,FINYEAR,EMPLOYEECOST,SPECIALCOST,tokendate,TOKENTIME,SYSTEMDATE,TOKEN_FROMTIME,category)VALUES('" + token + "/" + dt2.Rows[0]["id"].ToString() + "'," + txtempid.Text + "," + comboempname.SelectedValue + "," + comboidcardno.Text + "," + comboitemcode.SelectedValue + "," + comboitemname.SelectedValue + "," + txtitemcost.Text + "," + txtQuantity.Text + "," + txtDays.Text + "," + txtTotalAmount.Text + ",'" + chk + "'," + Class.Users.USERID + ",'" + Convert.ToString(Class.Users.CREATED) + "','" + Convert.ToString(Class.Users.CREATED) + "','" + Class.Users.IPADDRESS + "','T','" + Class.Users.UniqueID + "','" + combooptions.Text + "','" + Class.Users.COMPCODE + "','" + Class.Users.Finyear + "','" + txtempcost.Text + "','0',TO_DATE('" + dateTimePicker1.Value.ToString("dd-MM-yyyy") + "','dd-MM-yyyy'),'" + dateTimePicker1.Value.ToString("hh:mm:ss") + "',TO_DATE('" + System.DateTime.Now.ToString() + "','dd-MM-yyyy hh24:mi:ss'),'" + dateTimePicker1.Value.ToString("hh:mm:ss") + "','" + lblempid2.Text + "')";
            //                Utility.ExecuteNonQuery(ins);
            //            }
            //                string sel3 = "select max(A.ASPTBLCANTOKENID) id3 FROM ASPTBLCANTOKEN A ";
            //                DataSet ds3 = Utility.ExecuteSelectQuery(sel3, "ASPTBLCANTOKEN");
            //                DataTable dt3 = ds3.Tables["ASPTBLCANTOKEN"];

            //            //string sel = "SELECT A.ASPTBLMENPERDETID,   B.DOCDATE,E.CATEGORY,C.ITEMNAME1 ,C.EMPLOYEECOST,C.SPECIALCOST,c.ITEMIMAGE,A.FROMTIME FROM ASPTBLMENPERDET  A JOIN ASPTBLMENPER B ON A.ASPTBLMENPERID=B.ASPTBLMENPERID  JOIN ASPTBLCANITEMMAS C ON C.ASPTBLCANITEMMASID=A.ITEMNAME1 JOIN ASPTBLUSERMAS D ON D.COMPCODE=B.COMPCODE AND D.USERID=B.USERNAME  JOIN ASPTBLCANCATEGORYMAS E ON E.ASPTBLCANCATEGORYMASID = A.CATEGORY WHERE A.ACTIVE='T'  AND C.ACTIVE='T'  AND D.COMPCODE='" + Class.Users.COMPCODE + "'  AND D.USERID='" + Class.Users.USERID + "'  AND B.DOCDATE BETWEEN TO_DATE(SYSDATE) AND TO_DATE(SYSDATE+(SELECT DAYS FROM CANITEMDISPLAYDAYS))    AND TO_TIMESTAMP(TO_CHAR(SYSDATE,'DD/MM/YY HH24:MI:SS'),'DD/MM/YY HH24:MI:SS')  BETWEEN TO_TIMESTAMP(A.FROMDATE||' '||A.FROMTIME,'DD/MM/YY HH24:MI:SS') AND TO_TIMESTAMP(A.TODATE||' '||A.TOTIME,'DD/MM/YY HH24:MI:SS')   ORDER BY 2,1";


            //            string sel4 = "SELECT  A.TOKENNO, B.HREMPLOYMASTID,B.FNAME,D.MIDCARD,C.ITEMCODE,C.ITEMNAME1, C.EMPLOYEECOST,A.ITEMQTY ,A.NOOFDAYS,A.TOTALAMOUNT,C.SPECIALCOST,E.COMPCODE FROM ASPTBLCANTOKEN A   JOIN HREMPLOYMAST B ON  A.EMPID = B.HREMPLOYMASTID  JOIN ASPTBLCANITEMMAS C ON C.ASPTBLCANITEMMASID = A.ITEMNAME1 JOIN HREMPLOYDETAILS D ON B.HREMPLOYMASTID=D.HREMPLOYMASTID JOIN GTCOMPMAST E ON E.GTCOMPMASTID=B.COMPCODE   WHERE A.ASPTBLCANTOKENID=" + dt3.Rows[0]["id3"].ToString();
            //                DataSet ds4 = Utility.ExecuteSelectQuery(sel4, "ASPTBLCANTOKEN");
            //                DataTable dt4 = ds4.Tables["ASPTBLCANTOKEN"];
            //                lbltoken2.Text = dt4.Rows[0]["TOKENNO"].ToString();
            //               // lblempid2.Text = dt4.Rows[0]["MIDCARD"].ToString();
            //            lblcompcode2.Text = dt4.Rows[0]["COMPCODE"].ToString();
            //            lblempname2.Text = dt4.Rows[0]["FNAME"].ToString().ToUpper() + " ( " + dt4.Rows[0]["MIDCARD"].ToString() + " ) ";
            //            lblitemname2.Text = dt4.Rows[0]["ITEMNAME1"].ToString();
            //            lbldate2.Text =Convert.ToString(dateTimePicker1.Value.ToString().Substring(0,10));
            //            obj.ID = dt4.Rows[0]["TOKENNO"].ToString(); 
            //                obj.VisitorName= dt4.Rows[0]["FNAME"].ToString();
            //                obj.Company = Class.Users.HCompName;
            //                obj.MobileNo = dt4.Rows[0]["MIDCARD"].ToString();
            //                obj.Purpose = "Lunch Purpose";
            //                if (dt4.Rows.Count > 0)
            //                {
                               
                                
            //                        if (System.DateTime.Now.ToString("dd-MM-yyyy") == dateTimePicker1.Value.ToString("dd-MM-yyyy").Substring(0,10))
            //                        {
            //                            decimal t1 = add(Convert.ToDecimal("0" + dt4.Rows[0]["EMPLOYEECOST"].ToString()), Convert.ToDecimal("0" + dt4.Rows[0]["SPECIALCOST"].ToString()));
            //                             totamt = Sum(Convert.ToDecimal("0" + txtQuantity.Value), t1, Convert.ToDecimal("0" + txtDays.Value));
                               
            //                            //lblitemcost.Text = "Rate : " + totamt;
            //                            lblqty2.Text = txtQuantity.Value + " / Rate: " + t1;
            //                            obj.Amount = lblqty2.Text;
            //                            txtTotalAmount.Text = totamt.ToString();
            //                        }
            //                        else
            //                        {
            //                             totamt = Sum(Convert.ToDecimal("0" + dt4.Rows[0]["EMPLOYEECOST"].ToString()), Convert.ToDecimal("0" + txtQuantity.Value), Convert.ToDecimal("0" + txtDays.Value));
                              
            //                    lblqty2.Text = txtQuantity.Value + " / Rate: " + dt4.Rows[0]["EMPLOYEECOST"].ToString();
            //                            obj.Amount = lblqty2.Text;
            //                            txtTotalAmount.Text = totamt.ToString();
            //                        }
                               
            //                }
                       
            //                lblnoofdays2.Text = txtDays.Value + "/" + count + "  " + "No's";
            //                lblcompcode.Text = Class.Users.HCompName;
            //                lbldatetime.Text = Convert.ToString(Class.Users.CREATED);
            //                var mydata = qc.CreateQrCode(lbltoken2.Text, QRCoder.QRCodeGenerator.ECCLevel.L);
            //                var code = new QRCoder.QRCode(mydata);
            //                pictureBox1.Image = code.GetGraphic(50, Color.Black, Color.White, true);

                 

            //                //this.panelprint.Refresh();
            //                //this.panelprint.Show();

            //            printDocument1.PrinterSettings = printDialog1.PrinterSettings;
            //            printDocument1.Print();

            //        }

            //        string sel0 = "SELECT  MIN(A.FROMDATE)  AS FROMDATE1, MIN(A.FROMTIME) AS FROMTIME1,MIN(A.TODATE) AS TODATE1, MIN(A.TOTIME) AS TOTIME1,MIN(A.SYSTEMTIME) AS SYSTEMTIME FROM ASPTBLMENPERDET  A JOIN ASPTBLMENPER B ON A.ASPTBLMENPERID=B.ASPTBLMENPERID  JOIN ASPTBLCANITEMMAS C ON C.ASPTBLCANITEMMASID=A.ITEMNAME1 JOIN ASPTBLUSERMAS D ON D.COMPCODE=B.COMPCODE AND D.USERID=B.USERNAME  WHERE A.ACTIVE='T'  AND C.ACTIVE='T'  AND D.COMPCODE='" + Class.Users.COMPCODE + "'  AND D.USERID='" + Class.Users.USERID + "' AND A.TODATE=TO_DATE('" + System.DateTime.Now.ToString("dd-MM-yyyy").Substring(0, 10) + "','dd-MM-yyyy')   ORDER BY 1";//AND A.TODATE=TO_DATE('"+dateTimePicker1.Value.ToString("dd-MM-yyyy")+"','dd-MM-yyyy')
            //        DataSet ds0 = Utility.ExecuteSelectQuery(sel0, "ASPTBLCANITEMMAS");
            //        DataTable dt0 = ds0.Tables["ASPTBLCANITEMMAS"];

                   
            //        if (dt0.Rows.Count > 0 && dt0.Rows[0]["totime1"].ToString() != "")
            //        {
            //            TimeSpan fromtime1 = TimeSpan.Parse(System.DateTime.Now.ToString("HH:mm:ss"));
            //            TimeSpan totime1 = TimeSpan.Parse(dt0.Rows[0]["totime1"].ToString());
            //            TimeSpan differ = totime1.Subtract(currentTimeAlone);
            //            int h1 = differ.Hours * 60; int m1 = differ.Minutes * 60; int se1 = differ.Seconds;
            //            int h2 = h1 + m1 + se1;                      
            //            Class.Users.LoginTime = Convert.ToInt64(h2); Class.Users.UserTime = 1;
            //        }
                   
     
            //    }

            //    catch (Exception ex)
            //    {
                    
            //    }
            //    finally
            //    {

            //        DataTable dt = Utility.SQLQuery("SELECT  C.MACIP  FROM  ASPTBLMACHINEMAS A   JOIN   GTCOMPMAST B ON B.GTCOMPMASTID = A.COMPCODE    JOIN ASPTBLMACIP C ON C.ASPTBLMACIPID = A.IPADDRESS  AND C.ACTIVE = 'T'   JOIN  ASPTBLUSERMAS D ON D.USERID = A.WARDENNAME  AND D.COMPCODE = B.GTCOMPMASTID  WHERE B.COMPCODE = '" + Class.Users.HCompcode + "' AND D.USERNAME = '" + Class.Users.HUserName + "' AND A.ACTIVE='T'  ORDER BY 1 ");
            //        bIsConnected = axCZKEM1.Connect_Net(dt.Rows[0]["MACIP"].ToString(), Convert.ToInt32(4370));
            //        if (bIsConnected == true)
            //        {
            //            axCZKEM1.EnableDevice(iMachineNumber, false);//disable the device


            //            axCZKEM1.EnableDevice(iMachineNumber, false);//disable the device
            //            if (axCZKEM1.ClearGLog(iMachineNumber))
            //            {
            //                axCZKEM1.RefreshData(iMachineNumber);//the data in the device should be refreshed
            //            }

            //            axCZKEM1.EnableDevice(iMachineNumber, true);//enable the device

            //        }

            //        Class.Users.UniqueID = ""; txtempcost.Text = "";
            //        txtcontractconst.Text = "";
            //        txtspecialcost.Text = "";
            //    }


            //}
            //catch (Exception ex)
            //{
                
            //}
            
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

        public void ReadOnlys()
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
           
            this.Hide();

        }

        public void GridLoad()
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

        private void dateTimePicker1_Validating(object sender, CancelEventArgs e)
        {
            
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void dateTimePicker1_ValueChanged_1(object sender, EventArgs e)
        {
            combooptions.SelectedIndex = 0;
            if (System.DateTime.Now.ToString("dd-MM-yyyy") == dateTimePicker1.Value.ToString("dd-MM-yyyy"))
            {
                txtDays.Maximum = 2; txtDays.Enabled = true;
            }
            else { txtDays.Value = 1; txtDays.Enabled = false; }
        }

        private void txtDays_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= '1' && e.KeyChar <= '2' || e.KeyChar == (char)Keys.Back) //The  character represents a backspace
            {
                e.Handled = false; //Do not reject the input
            }
            else
            {
                e.Handled = true; //Reject the input
            }
        }

        private void txtQuantity_KeyPress(object sender, KeyPressEventArgs e)
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

        private void comboitemname_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
