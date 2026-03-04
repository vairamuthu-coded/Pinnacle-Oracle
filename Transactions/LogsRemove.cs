using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using zkemkeeper;
using System.Configuration;
using System.Net.NetworkInformation;

namespace Pinnacle.Transactions
{
    public partial class LogsRemove : Form, ToolStripAccess
    {
        private static LogsRemove _instance;
        Models.Master mas = new Models.Master();
        Models.UserRights sm = new Models.UserRights();

        private bool bIsConnected = false;//the boolean value identifies whether the device is connected
        private int iMachineNumber=1;//the serial number of the device.After connecting the device ,this value will be changed.      
        private static Int32 MyCount;


        zkemkeeper.CZKEMClass axCZKEM1 = new zkemkeeper.CZKEMClass();

        Models.Device dev = new Models.Device(); string systemuser = "";
        string strserialize = "";

        Models.DeviceCommunication user = new Models.DeviceCommunication();
        ListView allip = new ListView();
        ListView allip1 = new ListView();
        ListView allip2 = new ListView();
        ListView removeuserid = new ListView(); UICO uti = new UICO();
        ListView listfilter = new ListView();
        DataTable dtcard = new DataTable();
        public static LogsRemove Instance
        {

            get
            {
                if (_instance == null)
                    _instance = new LogsRemove();
                GlobalVariables.CurrentForm = _instance; return _instance;
            }

        }
        public LogsRemove()
        {

            InitializeComponent();

            DateTime dateForButton = DateTime.Now;
            Class.Users.IPADDRESS = GenFun.GetLocalIPAddress();
            frmdate.Value = dateForButton.AddDays(-1);
            GlobalVariables.HeaderName.Text = Class.Users.ScreenName;
          
            usercheck(Class.Users.HCompcode, Class.Users.HUserName, Class.Users.ScreenName);
            SecondtabControl2.SelectedTab = tab6Attlots;
           
            this.BackColor = Class.Users.BackColors;

            label10.BackColor = Class.Users.Color2;
            label12.BackColor = Class.Users.Color2;
            lblattsearch.BackColor = Class.Users.Color2;
            listviewattdown.Font = Class.Users.FontName;
            checkall.BackColor = Class.Users.BackColors;
            splitter1.BackColor= Class.Users.Color2;
        }

        private void AttIPLoad()
        {
            string ss = Class.Users.HCompcode; listviewattdown.Items.Clear();
            Class.Users.Intimation = "PAYROLL";
            DataTable dt1 = dev.ToIp(ss); iIndex = 1;
            if (dt1.Rows.Count >= 1)
            {

                foreach (DataRow row in dt1.Rows)
                {
                    ListViewItem list3 = new ListViewItem();
                    list3.SubItems.Add("");
                    list3.SubItems.Add(row["MACIP"].ToString());
                    list3.SubItems.Add("------");
                    list3.SubItems.Add("");
                    list3.SubItems.Add("");
                    if (iIndex % 2 == 0)
                    {
                        list3.BackColor = Color.White;

                    }
                    else
                    {
                        list3.BackColor = Color.WhiteSmoke;

                    }
                    listviewattdown.Items.Add(list3);
                    iIndex++;
                }
            }
            else
            {
                MessageBox.Show("No data found", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
        public void usercheck(string s, string ss, string sss)
        {
            s = Class.Users.HCompcode;
            ss = Class.Users.HUserName;
            sss = Class.Users.ScreenName;
            DataTable dt1 = sm.headerdropdowns(s, ss, sss);
            if (dt1.Rows.Count > 0)
            {
                Class.Users.ValidCheck = false;
                if (dt1.Rows[0]["Menuname"].ToString() == Class.Users.ScreenName)
                {

                    for (int r = 0; r < dt1.Rows.Count; r++)
                    {

                        if (dt1.Rows[r]["NEWS"].ToString() == "T") { GlobalVariables.News.Visible = false;  } else { GlobalVariables.News.Visible = false;  }
                        if (dt1.Rows[r]["SAVES"].ToString() == "T") { GlobalVariables.Saves.Visible = false; Class.Users.ValidCheck = true; } else { GlobalVariables.Saves.Visible = false; Class.Users.ValidCheck = false; }


                    }
                }


            }
            else
            {

            }

        }



        private bool bIsConnected1 = false;
      
   
        //  private static Int32 ToIPCount;
        int dwInfo, dwValue = 1;
        Int32 NewLog = 0;
        string sdwEnrollNumber = "";
        string sName = "";
        string sPassword = "";
        int iPrivilege = 0;
        bool bEnabled = false;
        int idwFingerIndex = 0;
        string sTmpData = "";
        string sTmpData1 = "";
        int iTmpLength = 0;
        int iFlag = 0;
        string sEnabled = "";
        int idwVerifyMode = 0;
        int idwInOutMode = 0;
        int idwYear = 0;
        int idwMonth = 0;
        int idwDay = 0;
        int idwHour = 0;
        int idwMinute = 0;
        int idwSecond = 0;
        int idwWorkcode = 0;
        int idwErrorCode = 0;
        int iGLCount = 0;
        int iIndex = 0;
        string sUserID = "";
        int iFaceIndex = 50;//the only possible parameter value     
        int iLength = 0;
        string sCardnumber = "";
        string MacIP = "";
        int iBackupNumber = 1;
        byte[] bytes;


        DataTable DTROW = new DataTable();
        private void BtnConnect_Click(object sender, EventArgs e)
        {
                    
        string ccode = ""; Class.Users.Intimation = "PAYROLL";
        //      string sstring = ConfigurationManager.ConnectionStrings["ConnectionString1"].ConnectionString;
        //Class.Users.ConnectionString = sstring;
        //   string[] data = Class.Users.ConnectionString.Split(',');
        //    Class.Users.HCompcode = data[1];
           

            ccode = Class.Users.HCompcode; Class.Users.UserTime = 0;
            try
            {
                progressBar1.Value = 0;
                int k = 0;
                iIndex = 0;
                iGLCount = 0; listfilter.Items.Clear();
                if (allip1.Items.Count > 0)
                {
                    string ip = "";
                    string macno = "";
                    string mactype = "";
                    string mactype2 = "";
                    if (DTROW.Columns.Count <= 0)
                    {
                        DTROW.Columns.Add("MACHINENUMBER");
                        DTROW.Columns.Add("IPADDRESS");
                        DTROW.Columns.Add("ENROLLNO");
                        DTROW.Columns.Add("DATETIMERECORD");
                    }
                    int connectip = 0; bIsConnected1 = false;DTROW.Rows.Clear();
                    for (int j = 0; j < allip1.Items.Count; j++)
                    {
                        lblattcount.Refresh(); lblattcount.Refresh();
                        lblattcount.Text = "Connecting..." + allip1.Items[j].SubItems[2].Text + " Count:" + DTROW.Rows.Count.ToString();
                        if (allip1.Items[j].SubItems[6].Text == "True")
                        {
                            connectip = j + 1;
                            DataTable dt = dev.IPLOAD(Class.Users.HCompcode, allip1.Items[j].SubItems[2].Text);
                            int maxip = dt.Rows.Count;
                            int i = 0; Class.Users.UserTime = 0;
                            for (i = 0; i < maxip; i++)
                            {
                                ip = ""; macno = ""; mactype = ""; mactype2 = "";
                                Cursor = Cursors.WaitCursor;
                                bIsConnected = axCZKEM1.Connect_Net(dt.Rows[i]["MACIP"].ToString(), Convert.ToInt32(txtPort.Text));
                                ip = dt.Rows[i]["MACIP"].ToString();
                                macno = dt.Rows[i]["MACNO"].ToString();
                                mactype = dt.Rows[i]["MTYPE"].ToString();
                                mactype2 = dt.Rows[i]["MTYPE2"].ToString();
                                lblattcount.Refresh(); lblattcount.Refresh();
                                lblattcount.Text = "CONNECTED.  IPAddres:" + ip.ToString();
                                if (bIsConnected == true)
                                {
                                    axCZKEM1.EnableDevice(iMachineNumber, false);//disable the device
                                    int ccount = 1;
                                    while (axCZKEM1.SSR_GetGeneralLogData(iMachineNumber, out sdwEnrollNumber, out idwVerifyMode, out idwInOutMode, out idwYear, out idwMonth, out idwDay, out idwHour, out idwMinute, out idwSecond, ref idwWorkcode))//get records from the memory
                                    {
                                        string inputDate = idwDay + "-" + idwMonth + "-" + idwYear;
                                        if (Convert.ToDateTime(inputDate) >= frmdate.Value.Date && Convert.ToDateTime(inputDate) <= todate.Value.Date.AddDays(1).AddTicks(-1))
                                        {
                                            string id = sdwEnrollNumber.ToString().Length.ToString();
                                        int card = Convert.ToInt32(id.ToString());
                                        string idcard = sdwEnrollNumber;
                                        string sdate = idwDay.ToString().Length.ToString();
                                        int date = Convert.ToInt32(sdate.ToString());
                                        string ss;
                                        if (date < 2)
                                        {
                                            ss = "0" + idwDay.ToString();
                                        }
                                        else
                                        {
                                            ss = idwDay.ToString();
                                        }
                                        string smonth = idwMonth.ToString().Length.ToString();
                                        int month = Convert.ToInt32(smonth.ToString());
                                        string sss;
                                        if (month < 2)
                                        {
                                            sss = "0" + idwMonth.ToString();
                                        }
                                        else
                                        {
                                            sss = idwMonth.ToString();
                                        }
                                        string shour = idwHour.ToString().Length.ToString();
                                        int hour = Convert.ToInt32(shour.ToString());
                                        string h;
                                        if (hour < 2)
                                        {
                                            h = "0" + idwHour.ToString();
                                        }
                                        else
                                        {
                                            h = idwHour.ToString();
                                        }
                                        string sminits = idwMinute.ToString().Length.ToString();
                                        int minits = Convert.ToInt32(sminits.ToString());
                                        string m;
                                        if (minits < 2)
                                        {
                                            m = "0" + idwMinute.ToString();
                                        }
                                        else
                                        {
                                            m = idwMinute.ToString();
                                        }
                                        string sseconds = idwSecond.ToString().Length.ToString();
                                        int seconds = Convert.ToInt32(sseconds.ToString());
                                        string s;
                                        if (seconds < 2)
                                        {
                                            s = "0" + idwSecond.ToString();
                                        }
                                        else
                                        {
                                            s = idwSecond.ToString();
                                        }
                                        var dat = ss.ToString() + "-" + sss.ToString() + "-" + idwYear.ToString();
                                        string time = h.ToString() + ":" + m.ToString() + ":" + s.ToString();
                                        iGLCount++;

                                        DataRow dr = DTROW.NewRow();

                                        dr["MACHINENUMBER"] = ccount.ToString();
                                        dr["IPADDRESS"] = ip;
                                        dr["ENROLLNO"] = idcard;
                                        dr["DATETIMERECORD"] = dat + " " + time;
                                        DTROW.Rows.Add(dr); ccount++;

                                         }
                                    }
                                    lblattcount.Refresh();
                                    lblattcount.Text = "Logs Download Completed.  IPAddres:" + ip.ToString();

                                }
                                else
                                {
                                    lblattcount.Refresh();
                                    lblattcount.Text = "Dis-Connected..:" + ip.ToString();
                                    Cursor = Cursors.Default;
                                }
                                axCZKEM1.EnableDevice(iMachineNumber, true);//enable the device    
                                axCZKEM1.RefreshData(iMachineNumber);
                            }



                           // string combinepath = @"" + data[3] + ":/Att/" + Class.Users.HCompcode;
                            string combinepath = @"D:/Att/" + Class.Users.HCompcode;
                            Directory.CreateDirectory(combinepath);
                            Class.Users.ValidCheck = false;
                            string spath = combinepath + DateTime.Now.ToString("dd-MM-yyyy") + "-" + allip1.Items[j].SubItems[2].Text + ".txt";
                            if (DTROW.Rows.Count >= 1)
                            {


                                Class.Users.ValidCheck = true;

                                StreamWriter swExtLogFile = new StreamWriter(spath);
                                int ii = 0;
                                swExtLogFile.Write(Environment.NewLine);
                                swExtLogFile.WriteLine("Table IdCard Delete Details : - " + allip1.Items[j].SubItems[2].Text + " - " + Class.Users.HCompcode);
                                swExtLogFile.Write("============================");
                                swExtLogFile.Write(Environment.NewLine);
                                foreach (DataRow rows in DTROW.Rows)
                                {
                                    object[] array = rows.ItemArray;
                                    for (ii = 0; ii < array.Length - 1; ii++)
                                    {
                                        swExtLogFile.Write(" - " + array[ii].ToString() + " ");
                                    }
                                    swExtLogFile.WriteLine(array[ii].ToString());
                                }
                                DTROW.Rows.Add(allip1.Items[j].SubItems[2].Text, " DownLoaded Completed Successfully ", System.DateTime.Now.ToString());
                                DTROW.Rows.Add("", "", "");
                                swExtLogFile.Write("********END OF DATA*********" + System.DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss"));
                                swExtLogFile.Flush();
                                swExtLogFile.Close();
                                Class.Users.ValidCheck = true;
                            }
                            else
                            {


                                StreamWriter swExtLogFile = new StreamWriter(spath);
                                int ii = 0;
                                swExtLogFile.Write(Environment.NewLine);
                                swExtLogFile.WriteLine("No Record Found  : - " + allip1.Items[j].SubItems[2].Text + " - " + Class.Users.HCompcode);
                                swExtLogFile.Write("============================");
                                swExtLogFile.Write(Environment.NewLine);
                                foreach (DataRow rows in DTROW.Rows)
                                {
                                    object[] array = rows.ItemArray;
                                    for (ii = 0; ii < array.Length - 1; ii++)
                                    {
                                        swExtLogFile.Write(" - " + array[ii].ToString());
                                    }
                                    swExtLogFile.WriteLine(array[ii].ToString());
                                }
                                DTROW.Rows.Add(allip1.Items[j].SubItems[2].Text, " No Record Found ", System.DateTime.Now.ToString());
                                DTROW.Rows.Add("", "", "");
                                swExtLogFile.Write("********END OF DATA*********" + System.DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss"));
                                swExtLogFile.Flush();
                                swExtLogFile.Close();
                                Class.Users.ValidCheck = false;
                            }

                            ////if (Class.Users.ValidCheck == true)
                            ////{
                            ////    var confirmation = MessageBox.Show("Do you want to Delete ?", "Delete Machine Data " + allip1.Items[j].SubItems[2].Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                            ////    if (confirmation == DialogResult.OK)
                            ////    {

                            ////        if (allip1.Items[j].SubItems[2].Text != "")
                            ////        {
                            ////            MyCount = 0; Cursor = Cursors.WaitCursor;

                            ////            int idwErrorCode = 0;

                            ////            int iDataFlag = 1;
                            ////            bIsConnected = axCZKEM1.Connect_Net(allip1.Items[j].SubItems[2].Text, Convert.ToInt32(txtPort.Text));
                            ////            if (bIsConnected == true)
                            ////            {

                            ////                axCZKEM1.EnableDevice(iMachineNumber, false);//disable the device

                            ////                if (axCZKEM1.ClearData(iMachineNumber, iDataFlag))
                            ////                {
                            ////                    axCZKEM1.RefreshData(iMachineNumber);//the data in the device should be refreshed
                            ////                   // MessageBox.Show("Clear all the fingerprint templates!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ////                }
                            ////                else
                            ////                {
                            ////                    axCZKEM1.GetLastError(ref idwErrorCode);
                            ////                    MessageBox.Show("Operation failed,ErrorCode=" + idwErrorCode.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            ////                }
                            ////                axCZKEM1.EnableDevice(iMachineNumber, true);//enable the device
                            ////                lblattcount.Text = "Attendance Logs are Cleared From Machine  :" + MyCount.ToString() + " and Connected IP Addres   :" + "";

                            ////            }

                            ////        }
                            ////        Cursor = Cursors.Default;

                            ////        DTROW.Rows.Clear();
                            ////    }
                            ////}
                         
                        }
                    }
                }
                else
                {
                    lblattcount.Refresh(); Class.Users.Intimation = "";
                    lblattcount.Text = "Bio-Metric Device DisConnected.";
                }

                lblattcount.Refresh(); Cursor = Cursors.Default;
                lblattcount.Text = "Attendance Download Completed. " + DTROW.Rows.Count.ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }

            Class.Users.Intimation = "";
            allip1.Items.Clear();
            btnConnects.Refresh(); DTROW.Rows.Clear();
            btnConnects.Text = "Connect / Import";
            AttIPLoad();
              Cursor = Cursors.Default;

            


        }

        private void Attendance_Import_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }

       

        private void Exit_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ToolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void checkall_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                int i = 0, j = 1; progressBar1.Minimum = 0;
                progressBar1.Maximum = listviewattdown.Items.Count;
                if (checkall.Checked == true)
                {
                    foreach (ListViewItem item in listviewattdown.Items)
                    {
                        progressBar1.Value = Convert.ToInt32(item.Index + 1);
                        decimal per = Convert.ToDecimal(100 / GenFun.ToDecimal(listviewattdown.Items.Count)) * (item.Index + 1);
                        lblprogress1.Refresh();
                        item.Checked = true;

                        lblprogress1.Text = "Downloading : " + (per).ToString("N0") + " %";

                    }
                }
                if (checkall.Checked == false)
                {
                    progressBar1.Maximum = 0;
                    for (i = 0; i < listviewattdown.Items.Count; i++)
                    {
                        listviewattdown.Items[i].Checked = false;
                    }



                }
            }
            catch (Exception ex)
            {

            }
        }

        private void BtnConnects_Click(object sender, EventArgs e)
        {

        }

        private void Txtattlogssearch_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnConnects_Click_1(object sender, EventArgs e)
        {
           
             

            
        }

        public void News()
        {
   
            allip.Items.Clear();
            allip1.Items.Clear();
            allip2.Items.Clear();
            SecondtabControl2.Font = Class.Users.FontName;
          
            //lvLogs.ForeColor = Class.Users.ForeColors;
            butheader.BackColor = Class.Users.BackColors;
            panel3.BackColor = Class.Users.BackColors;
            
            label10.BackColor = Class.Users.Color2;
            label12.BackColor = Class.Users.Color2;
            lblattsearch.BackColor = Class.Users.Color2;
            label10.ForeColor = Class.Users.BackColors;
            label12.ForeColor = Class.Users.BackColors;
            lblattsearch.ForeColor = Class.Users.BackColors;
            this.BackColor = Class.Users.BackColors;



            listviewattdown.Font = Class.Users.FontName;
            checkall.Checked = false;

            listfilter.Items.Clear(); progressBar1.Value = 0; lblattcount.Text = ""; lblprogress1.Text = "";

  
            allip2.Items.Clear();

            allip.Items.Clear(); allip1.Items.Clear();

        }
        public void Saves()
        {
           // throw new NotImplementedException();
        }

        public void Prints()
        {
           // throw new NotImplementedException();
        }

        public void Searchs()
        {
            throw new NotImplementedException();
        }

        public void Searchs(int id)
        {
            throw new NotImplementedException();
        }

        public void Deletes(int id)
        {
            throw new NotImplementedException();
        }

        public void Deletes()
        {
            throw new NotImplementedException();
        }

        public void ReadOnlys()
        {
            throw new NotImplementedException();
        }

        public void Imports()
        {
            throw new NotImplementedException();
        }

        public void Pdfs()
        {
            throw new NotImplementedException();
        }

        public void ChangePasswords()
        {
            throw new NotImplementedException();
        }

        public void DownLoads()
        {
            throw new NotImplementedException();
        }

        public void ChangeSkins()
        {
            throw new NotImplementedException();
        }

        public void Logins()
        {
            throw new NotImplementedException();
        }

        public void GlobalSearchs()
        {
            throw new NotImplementedException();
        }

        public void TreeButtons()
        {
            throw new NotImplementedException();
        }

        public void Exit()
        {
            News();
            GlobalVariables.MdiPanel.Show();

            GlobalVariables.HeaderName.Text = "";
            GlobalVariables.TabCtrl.TabPages.RemoveAt(GlobalVariables.TabCtrl.SelectedIndex);
            this.Hide();
        }

        private void listviewattdown_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            try
            {

                Class.Users.UserTime = 0;
                ListViewItem it = new ListViewItem();

                if (e.Item.Checked == true)
                {
                    Class.Users.UserTime = 0;
                    Ping ping = new Ping();
                    PingReply reply = ping.Send(e.Item.SubItems[2].Text, 1000);
                    if (reply.Status.ToString() == "Success")
                        bIsConnected = true;
                    else bIsConnected = false; e.Item.SubItems[3].Text = "DisConnected";

                    if (bIsConnected == true)
                    {
                        e.Item.SubItems[3].Text = "Connected";
                        e.Item.SubItems[4].Text = "";
                        e.Item.SubItems[5].Text = Class.Users.Error;
                        it.SubItems.Add(e.Item.SubItems[1].Text);
                        it.SubItems.Add(e.Item.SubItems[2].Text);
                        it.SubItems.Add(e.Item.SubItems[3].Text);
                        it.SubItems.Add(e.Item.SubItems[4].Text);
                        it.SubItems.Add(e.Item.SubItems[5].Text);
                        it.SubItems.Add(e.Item.Checked.ToString());
                        allip1.Items.Add(it);
                        lblattcount.Refresh();
                        lblattcount.Text = "This IP   :" + e.Item.SubItems[2].Text + "     Connected.";
                        listviewattdown.Refresh();
                    }
                    else
                    {
                        e.Item.SubItems[4].Text = reply.Status.ToString();
                        e.Item.SubItems[5].Text = "NetWork Un-Available";
                        it.SubItems.Add(e.Item.SubItems[2].Text);
                        it.SubItems.Add(e.Item.SubItems[3].Text);
                        it.SubItems.Add(e.Item.SubItems[4].Text);
                        it.SubItems.Add(e.Item.SubItems[5].Text);
                        listviewattdown.Refresh();

                        lblattcount.Refresh();
                        lblattcount.Text = "This IP   :" + e.Item.SubItems[2].Text + "     DisConnected.";

                    }

                }
                if (e.Item.Checked == false && e.Item.SubItems[3].Text == "Connected")
                {

                    bIsConnected = false;

                    e.Item.SubItems[4].Text = "Cancelled";
                    e.Item.SubItems[5].Text = "Cancelled";
                    e.Item.SubItems[3].Text = "DisConnected";

                    for (int c = 0; c < allip1.Items.Count; c++)
                    {
                        if (e.Item.SubItems[2].Text == allip1.Items[c].SubItems[2].Text)
                        {
                            allip1.Items[c].Remove();
                            c--;

                        }
                    }

                    lblattcount.Refresh();
                    lblattcount.Text = "This IP   :" + e.Item.SubItems[2].Text + "     DisConnected.";

                    listviewattdown.Refresh();

                }

            }
            catch (Exception ex) { }
        }

        private void LogsRemove_Load(object sender, EventArgs e)
        {
            AttIPLoad();
        }

        private void LogsRemove_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AttIPLoad();
        }

        private void logsRemoveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                for (int j = 0; j < allip1.Items.Count; j++)
                {
                    var confirmation = MessageBox.Show("Do you want to Delete ?", "Delete Machine Data " + allip1.Items[j].SubItems[2].Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    if (confirmation == DialogResult.OK)
                    {

                        if (allip1.Items[j].SubItems[2].Text != "")
                        {
                            MyCount = 0; Cursor = Cursors.WaitCursor;

                            int idwErrorCode = 0;

                            int iDataFlag = 1;
                            bIsConnected = axCZKEM1.Connect_Net(allip1.Items[j].SubItems[2].Text, Convert.ToInt32(txtPort.Text));
                            if (bIsConnected == true)
                            {

                                axCZKEM1.EnableDevice(iMachineNumber, false);//disable the device

                                if (axCZKEM1.ClearData(iMachineNumber, iDataFlag))
                                {
                                    axCZKEM1.RefreshData(iMachineNumber);//the data in the device should be refreshed
                                                                         // MessageBox.Show("Clear all the fingerprint templates!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    axCZKEM1.GetLastError(ref idwErrorCode);
                                    MessageBox.Show("Operation failed,ErrorCode=" + idwErrorCode.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                axCZKEM1.EnableDevice(iMachineNumber, true);//enable the device
                                lblattcount.Text = "Attendance Logs are Cleared From Machine  :" + MyCount.ToString() + " and Connected IP Addres   :" + "";

                            }

                        }
                        Cursor = Cursors.Default;

                        DTROW.Rows.Clear();
                    }
                }
            }
            catch(Exception ex)
            {

            }
            finally
            {
                AttIPLoad();
            }
        }

        public void GridLoad()
        {
            throw new NotImplementedException();
        }
    }
}
