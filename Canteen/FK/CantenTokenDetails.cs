using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;


namespace Pinnacle.Canteen.FK
{

    public partial class CantenTokenDetails : Form, ToolStripAccess
    {

        private static CantenTokenDetails _instance;
        Models.Master mas = new Models.Master();
        Models.UserRights sm = new Models.UserRights();
        Models.Device dev = new Models.Device(); string systemuser = "";
        string strserialize = "";
        //List<Models.DeviceCommunication> details = new List<Models.DeviceCommunication>();
        Models.DeviceCommunication user = new Models.DeviceCommunication();
        ListView allip = new ListView();
        ListView allip1 = new ListView();
        ListView allip2 = new ListView();
        ListView removeuserid = new ListView(); UICO uti = new UICO();
        ListView listfilter = new ListView();
        DataTable dtcard = new DataTable();
        public static CantenTokenDetails Instance
        {

            get
            {
                if (_instance == null)
                    _instance = new CantenTokenDetails();
                GlobalVariables.CurrentForm = _instance; return _instance;
            }

        }


        public CantenTokenDetails()
        {
            InitializeComponent();
            DateTime dateForButton = DateTime.Now;
            frmdate.Value = dateForButton.AddDays(-1);
            GlobalVariables.HeaderName.Text = Class.Users.ScreenName;
        
            usercheck(Class.Users.HCompcode, Class.Users.HUserName, Class.Users.ScreenName);
            SecondtabControl2.SelectedTab = tab6Attlots;
            butheader.BackColor = Class.Users.BackColors;
            this.BackColor = Class.Users.BackColors;
            panel3.BackColor = Class.Users.BackColors;
          
            splitter1.BackColor = Class.Users.BackColors;
      

           
            label10.BackColor = Class.Users.BackColors;
            label12.BackColor = Class.Users.BackColors;
            lblattsearch.BackColor = Class.Users.BackColors;
           
           
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

                        if (dt1.Rows[r]["NEWS"].ToString() == "T") { GlobalVariables.News.Visible = false;} else { GlobalVariables.News.Visible = false;  }
                        if (dt1.Rows[r]["SAVES"].ToString() == "T") { GlobalVariables.Saves.Visible = false; Class.Users.ValidCheck = true; } else { GlobalVariables.Saves.Visible = false; Class.Users.ValidCheck = false; }


                    }
                }


            }
            else
            {

            }

        }



        private bool bIsConnected1 = false;
        private bool bIsConnected = false;//the boolean value identifies whether the device is connected
        private int iMachineNumber = 0;//the serial number of the device.After connecting the device ,this value will be changed.
        private static Int32 MyCount;
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
        int iBackupNumber = 0;
        byte[] bytes;
        zkemkeeper.CZKEMClass axCZKEM1 = new zkemkeeper.CZKEMClass();
        private void DeviceCommunication_Load(object sender, EventArgs e)
        {

            
        }

        static DataTable ConvertToDatatable(List<Models.DeviceCommunication> list)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("ATTLOGID");
            dt.Columns.Add("MACHINENUMBER");
            dt.Columns.Add("IPADDRESS");
            dt.Columns.Add("ENROLLNO");
            dt.Columns.Add("DATETIMERECORD");

            foreach (var item in list)
            {
                var row = dt.NewRow();

                row["ATTLOGID"] = item.ATTLOGID;
                row["MACHINENUMBER"] = item.MACHINENUMBER;
                row["IPADDRESS"] = item.IPADDRESS;
                row["ENROLLNO"] = item.ENROLLNO;
                row["DATETIMERECORD"] = item.DATETIMERECORD;


                dt.Rows.Add(row);
            }

            return dt;
        }

        //private void Exit_Click(object sender, EventArgs e)
        //{



        //    this.Hide();

        //}

        private void DeviceCommunication_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }



        private void BtnConnect_Click(object sender, EventArgs e)
        {
          
        }
        private void AttIPLoad()
        {
            Class.Users.Intimation = "PAYROLL";
            string ss = Class.Users.HCompcode; listviewattdown.Items.Clear();
           string sel = "SELECT B.HRMACIPENTRYDETID,B.MACIP FROM HRMACIPENTRY A JOIN HRMACIPENTRYDET B ON A.HRMACIPENTRYID = B.HRMACIPENTRYID  JOIN GTCOMPMAST C ON C.GTCOMPMASTID = A.COMPCODE  WHERE B.DEFAULTYN = 'NO' AND B.CURMAC ='"+Class.Users.HUnit+"' AND C.COMPCODE='" + Class.Users.HCompcode + "'  ORDER BY 2";
            DataSet ds1 = Utility.ExecuteSelectQuery(sel, "HRMACIPENTRYDET");
          DataTable  dt1 = ds1.Tables["HRMACIPENTRYDET"];
            // DataTable dt1 = dev.ToIp(ss); iIndex = 1;
            if (dt1.Rows.Count >= 0)
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
        private void combo_ToIPload()
        {
            //listviewchecklistip1.Items.Clear();
            //listviewchecklistip2.Items.Clear(); Class.Users.Intimation = "PAYROLL";
            //string ss = Class.Users.HCompcode;
            //DataTable dt1 = dev.AllIp();

            //if (dt1.Rows.Count > 0)
            //{
            //    iIndex = 1;
            //    foreach (DataRow row in dt1.Rows)
            //    {

            //        ListViewItem list = new ListViewItem();
            //        ListViewItem list2 = new ListViewItem();
            //        list.SubItems.Add(row["MACIP"].ToString());
            //        list.SubItems.Add("------");


            //        ListViewItem listremove = new ListViewItem();


            //        listremove.SubItems.Add(row["MACIP"].ToString());
            //        listremove.SubItems.Add("------");


            //        ListViewItem listcard = new ListViewItem();
            //        listcard.SubItems.Add(row["MACIP"].ToString());
            //        listcard.SubItems.Add("------");


            //        ListViewItem listface = new ListViewItem();
            //        listface.SubItems.Add(row["MACIP"].ToString());
            //        listface.SubItems.Add("------");

            //        if (iIndex % 2 == 0)
            //        {
            //            list.BackColor = Color.White;
            //            listremove.BackColor = Color.White;
            //            listcard.BackColor = Color.White;
            //            listface.BackColor = Color.White;
            //        }
            //        else
            //        {
            //            list.BackColor = Color.WhiteSmoke;
            //            listremove.BackColor = Color.WhiteSmoke;
            //            listcard.BackColor = Color.WhiteSmoke;
            //            listface.BackColor = Color.WhiteSmoke;

            //        }
            //        listviewchecklistip.Items.Add(list);
           
            //        listviewchecklistip1.Items.Add(listcard);
            //        listviewchecklistip2.Items.Add(listface);
            //        iIndex++;
            //    }
            //    //ListViewItem listremove1 = new ListViewItem();
            //    //if (comboMasterIp.Text != "")
            //    //{
            //    //    listremove1.SubItems.Add(comboMasterIp.Text);
            //    //    listremove1.SubItems.Add("Connected");
            //    //    listremove1.SubItems.Add("True");
            //    //    allip2.Items.Add(listremove1);
            //    //}
            //}
            //else
            //{
            //    MessageBox.Show("No data found", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void combo_RemoveIPload()
        {

         
        }

        private void faceip()
        {

         
        }


        private void Contextallitemcheck_Click(object sender, EventArgs e)
        {
            try
            {


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void LvDownload_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
        
        }
        private void LvDownload_ItemActivate(object sender, EventArgs e)
        {
            //try
            //{
            //    if (LvDownload.Items.Count > 0)
            //    {
            //        ListViewItem item1 = new ListViewItem(); iIndex = listViewupload.Items.Count;
            //        for (int c = 0; c < LvDownload.SelectedItems[0].SubItems.Count; c++)
            //        {
            //            item1.SubItems.Add(LvDownload.SelectedItems[0].SubItems[c].Text);
            //        }

            //        if (iIndex % 2 == 0)
            //        {
            //            item1.BackColor = Color.White;
            //        }
            //        else
            //        {
            //            item1.BackColor = Color.WhiteSmoke;
            //        }
            //        iIndex++;
            //        listViewupload.Items.Add(item1);
            //    }


            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}
        }
        private void Contextallitemcheck_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void ListViewupload_ItemActivate(object sender, EventArgs e)
        {
            
        }

        private void Btntransfer_Click(object sender, EventArgs e)
        {
       
        }


        private void BtnDeleteEnrollData_Click(object sender, EventArgs e)
        {
           
        }

        //Delete a certain user's fingerprint template of specified index
        //You shuold input the the user id and the fingerprint index you will delete
        //The difference between the two functions "SSR_DelUserTmpExt" and "SSR_DelUserTmp" is that the former supports 24 bits' user id.

        private void BtnSSR_DelUserTmpExt_Click(object sender, EventArgs e)
        {
            try
            {
                if (bIsConnected == false)
                {
                    MessageBox.Show("Please connect the device first!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
              
            }
            catch (Exception ex)
            {
                MessageBox.Show("--" + ex.ToString());
            }
        }


        //Clear all the administrator privilege(not clear the administrators themselves)
        private void BtnClearAdministrators_Click(object sender, EventArgs e)
        {

        
            Cursor = Cursors.Default;
        }

        //Delete all the user information in the device,while the related fingerprint templates will be deleted either. 
        //(While the parameter DataFlag  of the Function "ClearData" is 5 )
        private void BtnClearDataUserInfo_Click(object sender, EventArgs e)
        {

         
        }

        //Clear all the fingerprint templates in the device(While the parameter DataFlag  of the Function "ClearData" is 2 )
        private void BtnClearDataTmps_Click(object sender, EventArgs e)
        {
          
            Cursor = Cursors.Default;
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
         
            splitter1.BackColor = Class.Users.BackColors;
           
            label10.BackColor = Class.Users.BackColors;
            label12.BackColor = Class.Users.BackColors;
            lblattsearch.BackColor = Class.Users.BackColors;

            
          
            this.BackColor = Class.Users.BackColors;

      
            checkall.BackColor = Class.Users.BackColors;

         
            listviewattdown.Font = Class.Users.FontName;
            checkall.Checked = false;
         
            listfilter.Items.Clear(); progressBar1.Value = 0; lblattcount.Text = ""; lblprogress1.Text = ""; 
 listviewattdown.Items.Clear();  allip2.Items.Clear();
    allip.Items.Clear(); allip1.Items.Clear();
         
            faceip();
            AttIPLoad();


        }

        public void Saves()
        {

            // Btntransfer_Click(sender, e);
        }

        class Data
        {
            public string Id { get; set; }
            public string Name { get; set; }
        }
        private void Txtsearch_TextChanged(object sender, EventArgs e)
        {
            

        }

        private void BtnSetStrCardNumber_Click(object sender, EventArgs e)
        {
          
        }

        DataTable DTROW = new DataTable();
        private void BtnConnects_Click(object sender, EventArgs e)
        {
            string ccode = ""; Class.Users.Intimation = "PAYROLL";
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
                        DTROW.Columns.Add("INOUTMODE");
                        DTROW.Columns.Add("VERIFYMODE");
                        DTROW.Columns.Add("WORKCODE");
                    }
                    int connectip = 0; bIsConnected1 = false;
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
                            string lpszProductCode = "";
                            for (i = 0; i < maxip; i++)
                            {
                                ip = ""; macno = ""; mactype = ""; mactype2 = "";
                                Cursor = Cursors.WaitCursor;
                                bIsConnected = axCZKEM1.Connect_Net(dt.Rows[i]["MACIP"].ToString(),4370);
                                ip = dt.Rows[i]["MACIP"].ToString();
                                macno = dt.Rows[i]["MACNO"].ToString();
                                mactype = dt.Rows[i]["MTYPE"].ToString();
                                mactype2 = dt.Rows[i]["MTYPE2"].ToString();
                                lblattcount.Refresh(); lblattcount.Refresh();
                                lblattcount.Text = "CONNECTED.  IPAddres:" + ip.ToString();
                                if (bIsConnected == true)
                                {
                                    axCZKEM1.EnableDevice(iMachineNumber, false);//disable the device

                                    while (axCZKEM1.SSR_GetGeneralLogData(iMachineNumber, out sdwEnrollNumber, out idwVerifyMode, out idwInOutMode, out idwYear, out idwMonth, out idwDay, out idwHour, out idwMinute,
                                        out idwSecond, ref idwWorkcode))//get records from the memory
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
                                            dr["MACHINENUMBER"] =Convert.ToInt32("0" +iMachineNumber.ToString());
                                            dr["IPADDRESS"] = ip;
                                            dr["ENROLLNO"] = idcard;
                                            dr["DATETIMERECORD"] = dat + " " + time;
                                            dr["INOUTMODE"] = Convert.ToInt32("0" + idwInOutMode.ToString());
                                            dr["VERIFYMODE"] =Convert.ToInt32("0"+idwVerifyMode.ToString());
                                            dr["WORKCODE"] = Convert.ToInt32("0" + idwVerifyMode.ToString());
                                            
                                            DTROW.Rows.Add(dr);
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
                        }
                    }

                    if (connectip == allip1.Items.Count)
                    {
                        if (DTROW.Rows.Count >= 1)
                        {
                            Cursor = Cursors.WaitCursor;
                            Class.Users.Intimation = "PAYROLL";
                            ///user.SaveUsingOracleBulkCopy2(Class.Users.HUnit + "ATT", DTROW);
                            user.SaveUsingOracleBulkCopy(Class.Users.HUnit + "TRS_ATTLOG", DTROW);
                            user.SaveUsingOracleBulkCopy(Class.Users.HUnit + "TRS_TEMPATTLOG", DTROW);
                            string exec = "BEGIN  CANATTINSERT('" + Class.Users.HUnit + "','" + Class.Users.HUserName + "'); END;";
                            Utility.ExecuteNonQuery(exec);
                            Cursor = Cursors.Default;
                        }
                        else
                        {
                            lblattcount.Refresh(); Class.Users.Intimation = "";
                            lblattcount.Text = "No Data Found.";
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

                lblattcount.Refresh();
                lblattcount.Text = "Install Zkemkeeper Dll :" + ex.ToString();

            }

            Class.Users.Intimation = "";
            AttIPLoad(); allip1.Items.Clear();
            btnConnects.Refresh(); DTROW.Rows.Clear();
            btnConnects.Text = "Connect / Import";
          
            Cursor = Cursors.Default;

        }

        private void BtnGetDeviceStatus_Click(object sender, EventArgs e)
        {

        }
        // combocardreaderipbox
        private void Txtattlogssearch_TextChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    int item0 = 0;

            //    if (txtattlogssearch.Text.Length >= 2)
            //    {
            //        lvLogs.Items.Clear(); 
            //        foreach (ListViewItem item in listfilter.Items)
            //        {
            //            ListViewItem list = new ListViewItem();
            //            if (listfilter.Items[item0].SubItems[2].ToString().Contains(txtattlogssearch.Text)|| listfilter.Items[item0].SubItems[3].ToString().Contains(txtattlogssearch.Text))
            //            {


            //                list.Text = item.SubItems[0].Text;
            //                list.SubItems.Add(item.SubItems[1].Text);
            //                list.SubItems.Add(item.SubItems[2].Text);
            //                list.SubItems.Add(item.SubItems[3].Text);
            //                list.SubItems.Add(item.SubItems[4].Text);
            //                list.SubItems.Add(item.SubItems[5].Text);
            //                list.SubItems.Add(item.SubItems[6].Text);
            //                list.SubItems.Add(item.SubItems[7].Text);
            //                list.SubItems.Add(item.SubItems[8].Text);
            //                list.SubItems.Add(item.SubItems[9].Text);
            //                if (item0 % 2 == 0)
            //                {
            //                    list.BackColor = Color.White;

            //                }
            //                else
            //                {
            //                    list.BackColor = Color.WhiteSmoke;


            //                }
            //                lvLogs.Items.Add(list);
            //                lblattcount.Refresh();
            //                lblattcount.Text = "Total  " + "    Rows Count  :" + lvLogs.Items.Count.ToString() + " and IP Addres   :" + txtsearch.Text.ToString();


            //            }
            //            item0++;

            //        }
            //        Cursor = Cursors.Default;
            //    }
            //    else
            //    {
            //        ListView ll = new ListView();
            //        lvLogs.Items.Clear();
            //        foreach (ListViewItem item in listfilter.Items)
            //        {




            //            if (item0 % 2 == 0)
            //            {
            //                item.BackColor = Color.White;

            //            }
            //            else
            //            {
            //                item.BackColor = Color.WhiteSmoke;


            //            }
            //            this.lvLogs.Items.Add((ListViewItem)item.Clone());
            //            item0++;
            //            lblattcount.Refresh();
            //            lblattcount.Text = "Total  " + "    Rows Count  :" + lvLogs.Items.Count.ToString() + " and IP Addres   :" + txtsearch.Text.ToString();

            //        }

            //    }


            //}
            //catch (Exception ex)
            //{
            //    //MessageBox.Show("---" + ex.ToString());
            //}



        }



        private void BtnDownloadTmp_Click(object sender, EventArgs e)
        {


        }
        private void BtnDownLoadFace_Click(object sender, EventArgs e)
        {

            //if (combofaceboxip.SelectedIndex >= 0)
            //{
            //    Cursor = Cursors.WaitCursor; lblattcount.Text = "";
            //    bIsConnected = axCZKEM1.Connect_Net(combofaceboxip.Text, Convert.ToInt32(txtPort.Text));
            //    if (bIsConnected == true)
            //    {
            //        listfilter.Items.Clear();
            //        lblState.Text = "Current State:Connected";
            //        lvFace.Items.Clear();
            //        lvFace.BeginUpdate();
            //        Cursor = Cursors.WaitCursor;
            //        axCZKEM1.EnableDevice(iMachineNumber, false);
            //        axCZKEM1.ReadAllUserID(iMachineNumber);//read all the user information to the memory

            //        while (axCZKEM1.SSR_GetAllUserInfo(iMachineNumber, out sUserID, out sName, out sPassword, out iPrivilege, out bEnabled))//get all the users' information from the memory
            //        {

            //            if (axCZKEM1.GetUserFaceStr(iMachineNumber, sUserID, iFaceIndex, ref sTmpData, ref iLength))//get the face templates from the memory
            //            {
            //                ListViewItem list = new ListViewItem();
            //                list.Text = sUserID;
            //                list.SubItems.Add(sName.ToUpper());
            //                list.SubItems.Add(iFaceIndex.ToString());
            //                list.SubItems.Add(sTmpData.ToString());
            //                list.SubItems.Add(iPrivilege.ToString());
            //                list.SubItems.Add(sPassword);

            //                if (bEnabled == true)
            //                {
            //                    list.SubItems.Add("True");
            //                }
            //                else
            //                {
            //                    list.SubItems.Add("False");
            //                }
            //                list.SubItems.Add(iLength.ToString());
            //                lvFace.Items.Add(list);
            //                this.listfilter.Items.Add((ListViewItem)list.Clone());
            //            }
            //        }
            //        axCZKEM1.EnableDevice(iMachineNumber, true);
            //        lvFace.EndUpdate();
            //        if (lvFace.Items.Count == 0)
            //        {
            //            MessageBox.Show("No Data Found");
            //        }
            //        Cursor = Cursors.Default;
            //        lblattcount.Text = "Total Employee Face Rows Count  :" + lvFace.Items.Count.ToString() + " and IP Addres   :" + comboMasterIp.Text;
            //    }
            //    else
            //    {

            //        axCZKEM1.GetLastError(ref idwErrorCode);
            //        MessageBox.Show("Unable to connect the device,ErrorCode=" + idwErrorCode.ToString(), "Error");

            //    }
            //}
            //else
            //{
            //    MessageBox.Show("Pls select IP Address from Combobox");
            //}
            //Cursor = Cursors.Default;
            //lblState.Text = "Current State:DisConnected";
        }


        private void btnUploadTmp_Click(object sender, EventArgs e)
        {
           
        }


        private void BtnDatabase_Click(object sender, EventArgs e)
        {
            //if (Class.Users.HCompcode == "AGF")
            //{
            //    try
            //    {


            //        listViewFingerTemp.Items.Clear(); lblattcount.Text = "";

            //        //axCZKEM1.EnableDevice(iMachineNumber, false);

            //        // select data from database
            //        UICO uti = new UICO();
            //        DataTable dt = uti.UploadDataTFT(combofingerboxip.Text);
            //        if (dt.Rows.Count > 0)
            //        {
            //            // start select data from database to upload in listview
            //            for (int i = 0; i < dt.Rows.Count; i++)
            //            {
            //                sdwEnrollNumber = string.IsNullOrEmpty(dt.Rows[i]["User_Id"].ToString()) ? " " : dt.Rows[i]["User_Id"].ToString();
            //                sName = string.IsNullOrEmpty(dt.Rows[i]["Name"].ToString()) ? " " : dt.Rows[i]["Name"].ToString();
            //                idwFingerIndex = string.IsNullOrEmpty(dt.Rows[i]["Finger_Index"].ToString()) ? 0 : Convert.ToInt32(dt.Rows[i]["Finger_Index"].ToString());
            //                sTmpData = string.IsNullOrEmpty(dt.Rows[i]["Finger_Image"].ToString()) ? " " : dt.Rows[i]["Finger_Image"].ToString();
            //                iPrivilege = string.IsNullOrEmpty(dt.Rows[i]["Privilege"].ToString()) ? 0 : Convert.ToInt32(dt.Rows[i]["Privilege"].ToString());
            //                sPassword = string.IsNullOrEmpty(dt.Rows[i]["Passwords"].ToString()) ? null : dt.Rows[i]["Passwords"].ToString();
            //                if (dt.Rows[i]["Enabled"].ToString() == "True")
            //                {
            //                    sEnabled = "True";
            //                }
            //                else
            //                {
            //                    sEnabled = "False";
            //                }
            //                int iFlag = Convert.ToInt32(dt.Rows[i]["Flag"].ToString());

            //                ListViewItem list = new ListViewItem();
            //                list.Text = sdwEnrollNumber.ToString();
            //                list.SubItems.Add(sName.ToUpper());
            //                list.SubItems.Add(idwFingerIndex.ToString());
            //                list.SubItems.Add(sTmpData);
            //                list.SubItems.Add(iPrivilege.ToString());
            //                list.SubItems.Add(sPassword);
            //                list.SubItems.Add(sEnabled.ToString());
            //                list.SubItems.Add(iFlag.ToString());
            //                listViewFingerTemp.Items.Add(list);
            //                lblattcount.Text = "Total Rows Count:  " + listViewFingerTemp.Items.Count.ToString();

            //            }
            //        }
            //        else
            //        {
            //            MessageBox.Show("No Data Found");
            //            lblattcount.Text = "Total Rows Count:  " + listViewFingerTemp.Items.Count.ToString();
            //        }


            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show("-----" + ex.ToString());
            //    }
            //}
        }

        private void DeleteFpTm_Click(object sender, EventArgs e)
        {

            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first!", "Error");
                return;
            }

            //UICO obj = new UICO();
            //obj.DeleteAllEmpTmTFT();
            MessageBox.Show("Successfully Data deleted from database");
        }



        private void Button2_Click(object sender, EventArgs e)
        {

        }

        private void BtnDelUserFace_Click(object sender, EventArgs e)
        {
            
        }

        private void BtnGetUserFace_Click(object sender, EventArgs e)
        {

           
        }



        private void BtnClearAdmin_Click(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first", "Error");
                return;
            }
            int idwErrorCode = 0;
            Cursor = Cursors.WaitCursor;
            if (axCZKEM1.ClearAdministrators(iMachineNumber))
            {
                axCZKEM1.RefreshData(iMachineNumber);//the data in the device should be refreshed
                MessageBox.Show("Successfully clear administrator privilege from teiminal!", "Success");


            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                MessageBox.Show("Operation failed,ErrorCode=" + idwErrorCode.ToString(), "Error");
            }
            Cursor = Cursors.Default;
        }

        private void BtnDeleteFaceTm_Click(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first!", "Error");
                return;
            }

            //UICO obj = new UICO();
           // obj.DeleteAllEmpTmIFACE_FaceTm();
            MessageBox.Show("Successfully Data deleted from database");
        }





        private void cardemtpy()
        {
          
        }
        private void BtnGetStrCardNumber_Click(object sender, EventArgs e)
        {
           
        }


        private void Txtfingertempsearch_TextChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    int item0 = 0;
            //    if (txtfingertempsearch.Text.Length >= 1)
            //    {
            //        listViewFingerTemp.Items.Clear();
            //        foreach (ListViewItem item in listfilter.Items)
            //        {
            //            ListViewItem list = new ListViewItem();
            //            if (listfilter.Items[item0].SubItems[0].ToString().Contains(txtfingertempsearch.Text) || listfilter.Items[item0].SubItems[1].ToString().Contains(txtfingertempsearch.Text))
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
            //                list.SubItems.Add(listfilter.Items[item0].SubItems[9].Text);
            //                listViewFingerTemp.Items.Add(list);


            //            }
            //            item0++;
            //        }

            //    }
            //    else
            //    {
            //        ListView ll = new ListView();
            //        listViewFingerTemp.Items.Clear();
            //        foreach (ListViewItem item in listfilter.Items)
            //        {


            //            this.listViewFingerTemp.Items.Add((ListViewItem)item.Clone());



            //            item0++;
            //        }

            //    }


            //}
            //catch (Exception ex)
            //{
            //    //MessageBox.Show("---" + ex.ToString());
            //}

        }

        private void Txtfacetempsearch_TextChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    int item0 = 0;
            //    if (txtfacetempsearch.Text.Length >= 1)
            //    {
            //        lvFace.Items.Clear();
            //        foreach (ListViewItem item in listfilter.Items)
            //        {
            //            ListViewItem list = new ListViewItem();
            //            if (listfilter.Items[item0].SubItems[0].ToString().Contains(txtfacetempsearch.Text) || listfilter.Items[item0].SubItems[1].ToString().Contains(txtfacetempsearch.Text))
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
            //                list.SubItems.Add(listfilter.Items[item0].SubItems[9].Text);
            //                lvFace.Items.Add(list);


            //            }
            //            item0++;
            //        }

            //    }
            //    else
            //    {
            //        ListView ll = new ListView();
            //        lvFace.Items.Clear();
            //        foreach (ListViewItem item in listfilter.Items)
            //        {


            //            this.lvFace.Items.Add((ListViewItem)item.Clone());



            //            item0++;
            //        }

            //    }


            //}
            //catch (Exception ex)
            //{
            //    //MessageBox.Show("---" + ex.ToString());
            //}


        }

        private void Txtcardreadersearch_TextChanged(object sender, EventArgs e)
        {
           
            //try
            //{
            //    int i;
            //    if (txtcardreadersearch.Text.Length > 1)
            //    {

            //        for (i = 0; i < lvCard.Items.Count; i++)
            //        {

            //            if (lvCard.Items[i].SubItems[0].ToString().Contains(txtcardreadersearch.Text) || lvCard.Items[i].SubItems[1].ToString().Contains(txtcardreadersearch.Text) || lvCard.Items[i].SubItems[5].ToString().Contains(txtcardreadersearch.Text))
            //            {
            //                lvCard.Items[i].BackColor = Color.Navy;
            //                lvCard.Items[i].ForeColor = Color.White;
            //            }
            //            else
            //            {
            //                lvCard.Items[i].ForeColor = Color.Blue;
            //                lvCard.Items[i].BackColor = Color.Gainsboro;
            //            }

            //        }
            //    }
            //    else
            //    {
            //        try
            //        {
            //            i = 0;
            //            for (i = 0; i < lvCard.Items.Count; i++)
            //            {
            //                lvCard.Items[i].ForeColor = Color.Blue;
            //                lvCard.Items[i].BackColor = Color.Gainsboro;
            //            }
            //        }
            //        catch (Exception ex)
            //        {

            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("---" + ex.ToString());
            //}


        }




        private void LvCard_ItemActivate(object sender, EventArgs e)
        {
           
        }





        private void Butloaddata_Click(object sender, EventArgs e)
        {
           
        }

        private void Lvremove_ItemActivate(object sender, EventArgs e)
        {


         
        }





        private void Butcarddwnfrmdatabase_Click(object sender, EventArgs e)
        {
           
        }





        private void Txtremovesearch_TextChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    int i;
            //    if (txtremovesearch.Text.Length > 1)
            //    {

            //        for (i = 0; i < lvremove.Items.Count; i++)
            //        {

            //            if (lvremove.Items[i].SubItems[0].ToString().Contains(txtremovesearch.Text) || lvremove.Items[i].SubItems[1].ToString().Contains(txtremovesearch.Text))
            //            {
            //                lvremove.Items[i].BackColor = Color.Navy;
            //                lvremove.Items[i].ForeColor = Color.White;
            //            }
            //            else
            //            {
            //                lvremove.Items[i].ForeColor = Color.Blue;
            //                lvremove.Items[i].BackColor = Color.Gainsboro;
            //            }

            //        }
            //    }
            //    else
            //    {
            //        try
            //        {
            //            i = 0;
            //            for (i = 0; i < lvremove.Items.Count; i++)
            //            {
            //                lvremove.Items[i].ForeColor = Color.Blue;
            //                lvremove.Items[i].BackColor = Color.Gainsboro;
            //            }

            //        }
            //        catch (Exception ex)
            //        {

            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("---" + ex.ToString());
            //}

        }



        private void Listviewchecklistip_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            

        }





        private void Butdownfrmdb_Click(object sender, EventArgs e)
        {

        }
        private void Lvdownall_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
           
        }

        private void Lvdownall_ItemActivate(object sender, EventArgs e)
        {

        }

        private void Butremoveall_Click(object sender, EventArgs e)
        {

        


        }


        private void Listremovechecklistip_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            

        }

        private void Butfacedownload_Click(object sender, EventArgs e)
        {
            //if (Class.Users.HCompcode == "AGF")
            //{
            //    comboMasterIp.SelectedIndex = -1;
            //    comboMasterIp.Enabled = false;
            //    btnConnect.Enabled = false;
            //    lvFace.Items.Clear();
            //    UICO objupload = new UICO();

            //    DataTable dt = objupload.UploadDataIFACE_FingerTm(combofaceboxip.Text);
            //    if (dt.Rows.Count > 0)
            //    {
            //        Cursor = Cursors.WaitCursor;
            //        for (int i = 0; i < dt.Rows.Count; i++)
            //        {
            //            sdwEnrollNumber = string.IsNullOrEmpty(dt.Rows[i]["User_Id"].ToString()) ? " " : dt.Rows[i]["User_Id"].ToString();
            //            sName = string.IsNullOrEmpty(dt.Rows[i]["Name"].ToString()) ? " " : dt.Rows[i]["Name"].ToString();
            //            iFaceIndex = string.IsNullOrEmpty(dt.Rows[i]["Face_Index"].ToString()) ? 0 : Convert.ToInt32(dt.Rows[i]["Face_Index"].ToString());
            //            sTmpData = string.IsNullOrEmpty(dt.Rows[i]["Face_Image"].ToString()) ? " " : dt.Rows[i]["Face_Image"].ToString();
            //            iPrivilege = string.IsNullOrEmpty(dt.Rows[i]["Privilege"].ToString()) ? 0 : Convert.ToInt32(dt.Rows[i]["Privilege"].ToString());
            //            sPassword = string.IsNullOrEmpty(dt.Rows[i]["Passwords"].ToString()) ? null : dt.Rows[i]["Passwords"].ToString();
            //            sEnabled = string.IsNullOrEmpty(dt.Rows[i]["Enabled"].ToString()) ? " " : dt.Rows[i]["Enabled"].ToString();
            //            iLength = string.IsNullOrEmpty(dt.Rows[i]["Face_Length"].ToString()) ? 0 : Convert.ToInt32(dt.Rows[i]["Face_Length"].ToString());
            //            ListViewItem list = new ListViewItem();
            //            list.Text = sdwEnrollNumber;
            //            list.SubItems.Add(sName);
            //            list.SubItems.Add(iFaceIndex.ToString());
            //            list.SubItems.Add(sTmpData.ToString());
            //            list.SubItems.Add(iPrivilege.ToString());
            //            list.SubItems.Add(sPassword);

            //            if (sEnabled == "True")
            //            {
            //                list.SubItems.Add("True");
            //            }
            //            else
            //            {
            //                list.SubItems.Add("False");
            //            }
            //            list.SubItems.Add(iLength.ToString());

            //            lvFace.Items.Add(list);
            //        }

            //        Cursor = Cursors.Default;
            //        MessageBox.Show("Successfully upload Face templates , " + "total:" + dt.Rows.Count.ToString(), "Success");
            //    }
            //}
        }

        private void Butreset_Click(object sender, EventArgs e)
        {

       
            Cursor = Cursors.Default;



        }



        private void Combofaceboxip_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (combofaceboxip.SelectedIndex > 0)
            //{
            //    comboMasterIp.SelectedIndex = -1;
            //    btnConnect.Enabled = true;
            //    comboMasterIp.Enabled = true;
            //    btnConnect.Refresh();
            //    btnConnect.Text = "Connect && Download ??";
            //    bIsConnected = false;
            //}
        }



        private void Lvdownremove_ItemActivate(object sender, EventArgs e)
        {
           
        }


        private void Checkallrows_CheckedChanged(object sender, EventArgs e)
        {

           
        }


        private void Checkremoveall_CheckedChanged(object sender, EventArgs e)
        {
            
        }


        private void Txtremovelog_TextChanged(object sender, EventArgs e)
        {
           

        }

        private void Listviewattdown_ItemChecked(object sender, ItemCheckedEventArgs e)
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



        private void ComboMasterIp_SelectedIndexChanged(object sender, EventArgs e)
        {
          

        }

        private void Comboremoveip_SelectedIndexChanged(object sender, EventArgs e)
        {


        }

        private void RefreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeviceCommunication_Load(sender, e); Class.Users.UserTime = 0;
        }

        public void Exit()
        {
            News();
            GlobalVariables.MdiPanel.Show();

            GlobalVariables.HeaderName.Text = "";
            GlobalVariables.TabCtrl.TabPages.RemoveAt(GlobalVariables.TabCtrl.SelectedIndex);
            this.Hide();
        }

        private void RefreshToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                //Cursor = Cursors.WaitCursor;
                //allip.Items.Clear(); allip1.Items.Clear(); allip2.Items.Clear();
                //listviewchecklistip.Items.Clear();
                //listremovechecklistip.Items.Clear(); 
                //listviewattdown.Items.Clear();
                //lvLogs.Items.Clear();comboMasterIp.SelectedIndex = -1;
                //AttIPLoad();
                //axCZKEM1.Disconnect();
                //bIsConnected = false;
                //btnConnect.Text = "Connect && Download ??";
                //lblState.Text = "Current State:DisConnected";
                //Cursor = Cursors.Default;
                News();
            }
            catch (Exception EX)
            {
                MessageBox.Show(EX.Message.ToString());
            }
        }

        private void Listviewuplaod_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void DownLoads_Click(object sender, EventArgs e)
        {

            Export2Excel();
        }

        private void Export2Excel()
        {
            //try
            //{
            //    string[] st = new string[lvLogs.Columns.Count];
            //    DirectoryInfo di = new DirectoryInfo(@"c:\Pinnacle");
            //    if (di.Exists == false)
            //        di.Create();
            //    StreamWriter sw = new StreamWriter(@"c:\Pinnacle\'" + Class.Users.HCompcode + "'TodayAttLogs.xls", false);
            //    sw.AutoFlush = true;
            //    for (int col = 0; col < lvLogs.Columns.Count; col++)
            //    {
            //        sw.Write("\t" + lvLogs.Columns[col].Text.ToString());
            //    }

            //    int rowIndex = 1;
            //    int row = 0;
            //    string st1 = "";
            //    for (row = 0; row < lvLogs.Items.Count; row++)
            //    {
            //        if (rowIndex <= lvLogs.Items.Count)

            //            rowIndex++;
            //        if (rowIndex == 2)
            //        {
            //            st1 = "\n";
            //        }
            //        else
            //        {
            //            st1 = ""; 
            //        }
            //        for (int col = 0; col < lvLogs.Columns.Count; col++)
            //        {
            //            st1 = st1 + "\t" + "" + lvLogs.Items[row].SubItems[col].Text.ToString();
            //        }
            //        sw.WriteLine(st1);
            //    }
            //    sw.Close();
            //    FileInfo fil = new FileInfo(@"c:\Pinnacle\'" + Class.Users.HCompcode + "'TodayAttLogs.xls");
            //    if (fil.Exists == true)
            //        MessageBox.Show("DownLoad Completed. \n Folder-Name is :c:\\Pinnacle\\'" + Class.Users.HCompcode + "'TodayAttLogs.xls","Export to Excel", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            //catch (Exception ex)
            //{
            //}
        }

        private void ListView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Btncarddownload_Click(object sender, EventArgs e)
        {
          
        }

        private void BtnfaceDownload_Click(object sender, EventArgs e)
        {
           
            combo_ToIPload();
            Cursor = Cursors.Default; bIsConnected = false;
        }

        private void CheckCard_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void Checkface_CheckedChanged(object sender, EventArgs e)
        {
          
        }
        private void LvDownload1_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
           
        }
        private void LvDownload1_ItemActivate(object sender, EventArgs e)
        {
            //try
            //{
            //    iIndex = listViewupload1.Items.Count;
            //    if (LvDownload1.Items.Count > 0)
            //    {
            //        ListViewItem item1 = new ListViewItem();
            //        for (int c = 0; c < LvDownload1.SelectedItems[0].SubItems.Count; c++)
            //        {
            //            item1.SubItems.Add(LvDownload1.SelectedItems[0].SubItems[c].Text);
            //            // item1.BackColor = Color.WhiteSmoke;


            //        }
            //        if (iIndex % 2 == 0)
            //        {
            //            item1.BackColor = Color.White;
            //        }
            //        else
            //        {
            //            item1.BackColor = Color.WhiteSmoke;
            //        }
            //        iIndex++;
            //        listViewupload1.Items.Add(item1);
            //    }


            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}
        }

        private void LvDownload2_ItemActivate(object sender, EventArgs e)
        {
            //try
            //{
            //    if (LvDownload2.Items.Count > 0)
            //    {
            //        ListViewItem itemlv2 = new ListViewItem();
            //        for (int c = 0; c < LvDownload2.SelectedItems[0].SubItems.Count; c++)
            //        {
            //            itemlv2.SubItems.Add(LvDownload2.SelectedItems[0].SubItems[c].Text);

            //                itemlv2.BackColor = Color.WhiteSmoke;


            //        }
            //        listViewupload2.Items.Add(itemlv2);
            //    }


            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}
        }

        private void Butcardtransfer_Click(object sender, EventArgs e)
        {
         
        }

        private void Listviewchecklistip1_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
          
        }

        private void Butfacetransfer_Click(object sender, EventArgs e)
        {
          
        }

        private void Listviewchecklistip2_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            
        }

        private void Txtcardtransfer_TextChanged(object sender, EventArgs e)
        {
          


        }

        private void Txtfactransferesearch_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void Tab5CardReader_Click(object sender, EventArgs e)
        {

        }

        private void listViewupload1_ItemActivate(object sender, EventArgs e)
        {
         
        }

        private void listViewupload2_ItemActivate(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
         
        }

        private void GetDeviceTime_Click(object sender, EventArgs e)
        {
         
        }



        private void poweroffdevice_Click(object sender, EventArgs e)
        {
           
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            axCZKEM1.PowerOnAllDevice();



            Cursor = Cursors.Default; return;
        }

        private void button3_Click(object sender, EventArgs e)
        {
          
        }

        private void BtnDatabase_Click1_Click(object sender, EventArgs e)
        {

        }

        private void Searchs_Click(object sender, EventArgs e)
        {

        }

        private void txtfingertempsearch_TextChanged_1(object sender, EventArgs e)
        {

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



        public void GridLoad()
        {

        }

        public void ReadOnlys()
        {
            throw new NotImplementedException();
        }

        private void excelToolStripMenuItem_Click(object sender, EventArgs e)
        {
       

        }

        private void SecondtabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Class.Users.UserTime = 0; Class.Users.Intimation = "PAYROLL";
        }

        public void Searchs(int id)
        {
            throw new NotImplementedException();
        }

        public void Deletes(int id)
        {
            throw new NotImplementedException();
        }

        private void butsetdeviceip_Click(object sender, EventArgs e)
        {
          

        }

        private void butsetcardno_Click(object sender, EventArgs e)
        {
          
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //string selmax = "select MAX(a.ATTLOGID) AS ATTLOGID from " + Class.Users.HCompcode + "TRS_ATTLOG a where a.DATETIMERECORD='"+frmdate.Text+"'";
            //Int32 maxid = Convert.ToInt32("0" + Utility.ExecuteScalar(selmax));
            //string sel01 = "SELECT ATTLOGID,jsondata FROM sklTRS_TEMPATTLOG WHERE ATTLOGID =" + maxid;
            //DataSet ds01 = Utility.ExecuteSelectQuery(sel01, "sklTRS_TEMPATTLOG");
            //DataTable dt01 = ds01.Tables["sklTRS_TEMPATTLOG"];
            //if (dt01 != null)
            //{
            //    Models.DeviceCommunication user = new Models.DeviceCommunication();
            //    bytes = (byte[])dt01.Rows[0]["jsondata"];
            //    user.JSONDATA = Models.Device.BytesToString(bytes);
            //    JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            //    List<Models.DeviceCommunication> listEmployee = (List<Models.DeviceCommunication>)javaScriptSerializer.Deserialize(user.JSONDATA, typeof(List<Models.DeviceCommunication>));
            //    DataTable dt11 = ConvertToDatatable(listEmployee);
            //    if (dt11.Rows.Count > 0)
            //    {
            //        int i = 1; Cursor = Cursors.WaitCursor;
            //        foreach (DataRow myRow in dt11.Rows)
            //        {
            //            ListViewItem list = new ListViewItem();
            //            list.SubItems.Add(i.ToString());
            //            list.SubItems.Add(myRow["ENROLLNO"].ToString());
            //            list.SubItems.Add(myRow["IPADDRESS"].ToString());
            //            list.SubItems.Add("");
            //            list.SubItems.Add(myRow["DATETIMERECORD"].ToString());
            //            list.SubItems.Add(myRow["DATETIMERECORD"].ToString());
            //            list.SubItems.Add(myRow["MACHINENUMBER"].ToString());
            //            list.SubItems.Add(myRow["MACHINENUMBER"].ToString());
            //            if (i % 2 == 0)
            //            {
            //                list.BackColor = Color.White;

            //            }
            //            else
            //            {
            //                list.BackColor = Color.WhiteSmoke;

            //            }
            //            lvLogs.Items.Add(list);
            //            this.listfilter.Items.Add((ListViewItem)list.Clone());
            //            i++;
            //            lvLogs.Refresh();
            //            lblattcount.Refresh();
            //            lblattcount.Text = "Total Count    :" + lvLogs.Items.Count;
            //        }
            //    }
            //    Cursor = Cursors.Default;
            //}
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
        #region RealTime Events

        //When you have enrolled a new user,this event will be triggered.
        private void axCZKEM1_OnNewUser(int iEnrollNumber)
        {
          
        }

        //When you swipe a card to the device, this event will be triggered to show you the number of the card.
        private void axCZKEM1_OnHIDNum(int iCardNumber)
        {
           
        }

        //When you have emptyed the Mifare card,this event will be triggered.
        private void axCZKEM1_OnEmptyCard(int iActionResult)
        {
           
        }

        //When you have written into the Mifare card ,this event will be triggered.
        private void axCZKEM1_OnWriteCard(int iEnrollNumber, int iActionResult, int iLength)
        {
            
        }

        //After you swipe your card to the device,this event will be triggered.
        //If your card passes the verification,the return value  will be user id, or else the value will be -1
        private void axCZKEM1_OnVerify(int iUserID)
        {
           
        }

        //If your card passes the verification,this event will be triggered
        private void axCZKEM1_OnAttTransaction(int iEnrollNumber, int iIsInValid, int iAttState, int iVerifyMethod, int iYear, int iMonth, int iDay, int iHour, int iMinute, int iSecond)
        {
           
        }

        //If your fingerprint(or your card) passes the verification,this event will be triggered
        private void axCZKEM1_OnAttTransactionEx(string sEnrollNumber, int iIsInValid, int iAttState, int iVerifyMethod, int iYear, int iMonth, int iDay, int iHour, int iMinute, int iSecond, int iWorkCode)
        {
          
        }

        private void LvDownload2_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
          
        }

        private void tab6Attlots_Click(object sender, EventArgs e)
        {

        }

        private void butmachinereset_Click(object sender, EventArgs e)
        {
           
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void rtTimer_Tick(object sender, EventArgs e)
        {
            if (axCZKEM1.ReadRTLog(iMachineNumber))
            {
                while (axCZKEM1.GetRTLog(iMachineNumber))
                {
                    ;
                }
            }
        }

        #endregion
    }

}

////////string del = "DELETE FROM " + Class.Users.HCompcode + "TRS_ATTLOG   WHERE ENROLLNO ='" + Convert.ToString(item.SubItems[2].Text) + "' AND TO_CHAR(TO_TIMESTAMP(DATETIMERECORD,'DD-MM-YYYY HH24:MI:SS'),'DD-MM-YYYY HH24:MI:SS') = TO_CHAR(TO_TIMESTAMP('" + Convert.ToDateTime(item.SubItems[5].Text) + "','DD-MM-YYYY HH24:MI:SS'),'DD-MM-YYYY HH24:MI:SS') AND IPAddress ='" + Convert.ToString(item.SubItems[3].Text) + "'";
////////Utility.ExecuteNonQuery(del);
////////string ins = "INSERT INTO " + Class.Users.HCompcode + "TRS_TEMPATTLOG(MACHINENUMBER, IPADDRESS, ENROLLNO, VERIFYMODE, INOUTMODE, WORKCODE, DATETIMERECORD)VALUES(" + item.SubItems[8].Text + ",'" + item.SubItems[3].Text + "'," + item.SubItems[2].Text + "," + 0 + "," + 0 + "," + 0 + ",'" + item.SubItems[5].Text + "')";
////////Utility.ExecuteNonQuery(ins);
////////string ins1 = "INSERT INTO " + Class.Users.HCompcode + "TRS_ATTLOG(MACHINENUMBER, IPADDRESS, ENROLLNO, VERIFYMODE, INOUTMODE, WORKCODE, DATETIMERECORD)VALUES(" + item.SubItems[8].Text + ",'" + item.SubItems[3].Text + "'," + item.SubItems[2].Text + "," + 0 + "," + 0 + "," + 0 + ",'" + item.SubItems[5].Text + "')";
////////Utility.ExecuteNonQuery(ins1);
//////// decimal per = Convert.ToDecimal(100 / GenFun.ToDecimal(lvLogs.Items.Count)) * (item.Index + 1);
//////// lblprogress1.Text = "Downloading : " + (per).ToString("N0") + " %";
//////// lblprogress1.Refresh();
////////progressBar1.Value = Convert.ToInt32(item.Index + 1);