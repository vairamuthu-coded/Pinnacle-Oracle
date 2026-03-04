using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using zkemkeeper;
using System.Web.Script.Serialization;

namespace Pinnacle.Transactions
{

    public partial class DeviceCommunication : Form, ToolStripAccess
    {

        private static DeviceCommunication _instance; string[] spl;
        Models.Master mas = new Models.Master();
        Models.UserRights sm = new Models.UserRights(); Ping ping = new Ping(); PingReply reply1;
        Models.Device dev = new Models.Device(); string systemuser = "";
        string strserialize = ""; int j = 1; double totalrows = 0;
        //List<Models.DeviceCommunication> details = new List<Models.DeviceCommunication>();
        Models.DeviceCommunication user = new Models.DeviceCommunication();
        ListView allip = new ListView();
        ListView allip1 = new ListView();
        ListView allip2 = new ListView(); DataTable dtexcel = new DataTable();
        ListView removeuserid = new ListView(); DataTable DTROW = new DataTable();
        UICO uti = new UICO(); 
   
        ListView listfilter = new ListView();
        DataTable dtcard = new DataTable();
        zkemkeeper.CZKEM axCZKEM1 = new zkemkeeper.CZKEM();
        string[] data = Class.Users.ConString.Split(',');
        string combinepath = ""; string[] data1,data2;


    public static DeviceCommunication Instance
        {

            get
            {
                if (_instance == null)
                    _instance = new DeviceCommunication();
                GlobalVariables.CurrentForm = _instance; return _instance;
            }

        }


        public DeviceCommunication()
        {
            InitializeComponent(); Class.Users.Intimation = "PAYROLL";
            DateTime dateForButton = DateTime.Now; Class.Users.SessionID = 9; checkBox1.Text = Class.Users.SessionID.ToString() + "  Index"; ;
            Class.Users.IPADDRESS = GenFun.GetLocalIPAddress();
            frmdate.Value = dateForButton.AddDays(-1);
            GlobalVariables.HeaderName.Text = Class.Users.ScreenName;
            SecondtabControl2.TabPages.Remove(tab2RemovefrmMachine);
            usercheck(Class.Users.HCompcode, Class.Users.HUserName, Class.Users.ScreenName);
            SecondtabControl2.SelectedTab = tab6Attlots;
            butheader.BackColor = Class.Users.BackColors;
            this.BackColor = Class.Users.BackColors;
            panel3.BackColor = Class.Users.BackColors;
            panel2.BackColor = Class.Users.BackColors;
            splitter1.BackColor = Class.Users.BackColors;
            splitter2.BackColor = Class.Users.BackColors;
            splitter4.BackColor = Class.Users.BackColors;
            splitter3.BackColor = Class.Users.BackColors;
            splitter5.BackColor = Class.Users.BackColors;
            splitercardreader.BackColor = Class.Users.BackColors;
           
            label10.BackColor = Class.Users.BackColors;
            label12.BackColor = Class.Users.BackColors;
            atttotal.BackColor = Class.Users.BackColors;
            lblattsearch.BackColor = Class.Users.BackColors;
            checkremoveall.BackColor = Class.Users.BackColors;
            lblremovesearch.BackColor = Class.Users.BackColors;
            label15.BackColor = Class.Users.BackColors;
            label16.BackColor = Class.Users.BackColors;
           
            label27.BackColor = Class.Users.BackColors;
            label35.BackColor = Class.Users.BackColors;
            checkCard.BackColor = Class.Users.BackColors;
            label13.BackColor = Class.Users.BackColors;
            label14.BackColor = Class.Users.BackColors;
            checkface.BackColor = Class.Users.BackColors;
            checkallrows.BackColor = Class.Users.BackColors;
            lblsearch.BackColor = Class.Users.BackColors;
            data1 = data[0].Split('=');
            data2 = data1[6].Split(')');
          

            string drivePart = data[4].Trim().Trim(':');     // remove colon if present

             combinepath = Path.Combine(drivePart + @":\", "Att");         
            if (!Directory.Exists(combinepath))
            {
                Directory.CreateDirectory(combinepath);
            }         

            

        }

        public void usercheck(string s, string ss, string sss)
        {
            s = Class.Users.HCompcode;
            ss = Class.Users.HUserName;
            sss = Class.Users.ScreenName;

            DataTable dt1 = sm.headerdropdowns(s, ss, sss);

            if (dt1 == null || dt1.Rows.Count == 0)
                return;

            Class.Users.ValidCheck = false;

            // Find matching menu row (safer than assuming row 0)
            DataRow[] rows = dt1.Select($"Menuname = '{Class.Users.ScreenName}'");
            if (rows.Length == 0)
                return;

            foreach (DataRow row in rows)
            {
                // NEWS Permission
                bool hasNews = row["NEWS"].ToString() == "T";
                GlobalVariables.News.Visible = false; // Always hidden
                if (hasNews)
                {
                    SecondtabControl2.TabPages.Add(tab2RemovefrmMachine);
                    SecondtabControl2.SelectTab(tab2RemovefrmMachine);
                    SecondtabControl2.SelectTab(tab6Attlots);

                }
                else
                {
                    SecondtabControl2.TabPages.Remove(tab2RemovefrmMachine);                    
                    SecondtabControl2.TabPages.Remove(tab5CardReader);
                    SecondtabControl2.TabPages.Remove(tab6Attlots);
                }

                // SAVE Permission
                bool hasSave = row["SAVES"].ToString() == "T";
                GlobalVariables.Saves.Visible = false; // Always hidden
                Class.Users.ValidCheck = hasSave;

                // PRINT Permission
                bool hasPrint = row["prints"].ToString() == "T";
                GlobalVariables.Prints.Visible = false; // Always hidden
            }

           

        }



     
        private bool bIsConnected = false;//the boolean value identifies whether the device is connected
        private int iMachineNumber = 1;//the serial number of the device.After connecting the device ,this value will be changed.
        private static Int32 MyCount;
     
      
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
        int idwSecond = 0; int idwWorkcode1 = 0;
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


        //zkemkeeper.CZKEM axCZKEM1 = new zkemkeeper.CZKEM();

        private void DeviceCommunication_Load(object sender, EventArgs e)
        {

            try
            {
                DataTable dt = dev.FromIp(Class.Users.HCompcode);

                if (dt.Rows.Count > 0)
                {


                    comboMasterIp.DisplayMember = "MACIP";
                    comboMasterIp.ValueMember = "MACIP";
                    comboMasterIp.DataSource = dt;


                }
                else
                {
                    comboMasterIp.DataSource = null;
                }
                if (Class.Users.HUserName == "VAIRAM" || Class.Users.HUserName == "ADMIN") { } else { SecondtabControl2.TabPages.Remove(tab2RemovefrmMachine); }


            }
            catch (Exception EX)
            { }
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

     
        private void DeviceCommunication_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }



        private void BtnConnect_Click(object sender, EventArgs e)
        {
            Class.Users.UserTime = 0;
            
            dtexcel.Rows.Clear();
            if (dtexcel.Columns.Count <= 0)
            {
                dtexcel.Columns.Clear();
                dtexcel.Columns.Add("IPADDRESS");
                dtexcel.Columns.Add("DETAILS");
                dtexcel.Columns.Add("DATETIME");
            }
            try
            {
                
                if (comboMasterIp.Text.Trim() == "" || txtPort.Text.Trim() == "")
                {
                    MessageBox.Show("IP and Port cannot be null", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                
                listViewupload.Items.Clear();
                SecondtabControl2.SelectTab(tab1finger);
                listremovechecklistip.Items.Clear();
                lblprogress1.Text = "";
                listViewupload.Items.Clear();
                Class.Users.UserTime = 0;
                int idwErrorCode = 0;
                lblattcount.Text = "";
                checkremoveall.Checked = false;
                checkallrows.Checked = false;
                Cursor = Cursors.WaitCursor;

                string macip = "";

                if (btnConnect.Text == "Finger DisConnect")
                {
                    try { axCZKEM1.Disconnect(); } catch { }
                    bIsConnected = false;
                    btnConnect.Text = "Finger Download ??";
                    lblState.Text = "Current State:DisConnected";
                    Cursor = Cursors.Default;
                    return;
                }

                axCZKEM1.PullMode = 1;
                spl = comboMasterIp.Text.Trim().Split('/');
                if (Class.Users.HCompcode == "LOPPL") {
                    bIsConnected = axCZKEM1.Connect_Net(spl[0], Convert.ToInt32(txtPort.Text));
                }
                else
                {
                    bIsConnected = axCZKEM1.Connect_Net(comboMasterIp.Text, Convert.ToInt32(txtPort.Text));
                }

                if (!bIsConnected)
                {
                    axCZKEM1.GetLastError(ref idwErrorCode);
                    MessageBox.Show("Unable to connect the device,ErrorCode=" + idwErrorCode.ToString(), "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Cursor = Cursors.Default;
                    return;
                }

                // Connected - prepare UI and read data
                LvDownload.Items.Clear();
                Lvdownremove.Items.Clear();
                Lvdownall.Items.Clear();

                listfilter.Items.Clear();

                LvDownload.BeginUpdate();
                Lvdownall.BeginUpdate();
                lvCard.BeginUpdate();

                axCZKEM1.EnableDevice(iMachineNumber, false);
                Cursor = Cursors.WaitCursor;
                btnConnect.Text = "Finger DisConnect";
                lblState.Text = "Current State:Connected";

                axCZKEM1.ReadAllUserID(iMachineNumber);
                axCZKEM1.IsTFTMachine(iMachineNumber);
                axCZKEM1.ReadAllTemplate(iMachineNumber);

                int r = 1; int s = 0;
                // Outer loop: iterate all users loaded in memory

                while (axCZKEM1.SSR_GetAllUserInfo(iMachineNumber, out sdwEnrollNumber, out sName, out sPassword, out iPrivilege, out bEnabled))
                {
                    macip = comboMasterIp.Text;
                    for (idwFingerIndex = 0; idwFingerIndex <= Class.Users.SessionID; idwFingerIndex++)
                    {
                        // Reset per-template variables
                        string sss = "";
                        string ssss = "";
                        string card = "";
                        lblattcount.Refresh();
                        lblattcount.Text = $"IDCardNo : {sdwEnrollNumber}  Count : {r} --- {idwFingerIndex}";
                       
                        if (!axCZKEM1.GetUserTmpExStr(iMachineNumber, sdwEnrollNumber, idwFingerIndex,
                                out iFlag, out sTmpData, out iTmpLength))
                        {
                            // no template at this finger index; continue
                            continue;
                        }

                        // we have template data
                        ssss = sTmpData;

                        // try to get card number (if any)
                        axCZKEM1.GetStrCardNumber(out sCardnumber);
                        if (!string.IsNullOrEmpty(sCardnumber) && sCardnumber.Length >= 1)
                            card = sCardnumber;
                        else
                            card = "";

                        // Resolve display name: VAIRAM special-case DB lookup else sName
                        string displayName = ""; string sql = "";
                        if (displayName == "")
                        {
                            sql = "";
                            //sql = $"SELECT C.FNAME || '-' || E.MNNAME1 AS EMPNAME FROM GTCOMPMAST B JOIN HREMPLOYMAST C ON C.COMPCODE = B.GTCOMPMASTID JOIN HREMPLOYDETAILS D ON C.HREMPLOYMASTID = D.HREMPLOYMASTID JOIN GTDEPTDESGMAST E ON E.GTDEPTDESGMASTID = D.DEPTNAME WHERE D.IDACTIVE='YES' AND  D.MIDCARD='{sdwEnrollNumber.Trim()}' AND B.COMPCODE='" + Class.Users.HCompcode + "'   ORDER BY C.IDCARDNO DESC";
                            //DataTable dt = Utility.ExecuteSelectQuery(sql, "HREMPLOYDETAILS").Tables[0];
                            DataTable dt = dev.FindName(sdwEnrollNumber.Trim(), Class.Users.HCompcode);
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                displayName = dt.Rows[0]["EMPNAME"].ToString();
                            }
                            else
                            {
                                DataTable dt1 = new DataTable();
                                //sql = "";
                                //sql = $"SELECT C.FNAME || '-' || E.MNNAME1 AS EMPNAME FROM GTCOMPMAST B JOIN HREMPLOYMAST C ON C.COMPCODE = B.GTCOMPMASTID JOIN HREMPLOYDETAILS D ON C.HREMPLOYMASTID = D.HREMPLOYMASTID JOIN GTDEPTDESGMAST E ON E.GTDEPTDESGMASTID = D.DEPTNAME WHERE D.IDACTIVE='YES' AND  D.MIDCARD='{sdwEnrollNumber.Trim()}'  ORDER BY C.IDCARDNO DESC";
                                //DataTable dt1 = Utility.ExecuteSelectQuery(sql, "HREMPLOYDETAILS").Tables[0];
                                 dt1 = dev.FindName(sdwEnrollNumber.Trim(), Class.Users.HCompcode);
                                if (dt1 != null && dt1.Rows.Count > 0)
                                {
                                    displayName = dt1.Rows[0]["EMPNAME"].ToString();
                                }
                                else
                                {
                                    //    sql = "";
                                    //    sql = $"SELECT C.FNAME || '-' || E.MNNAME1 AS EMPNAME FROM GTCOMPMAST B JOIN HREMPLOYMAST C ON C.COMPCODE = B.GTCOMPMASTID JOIN HREMPLOYDETAILS D ON C.HREMPLOYMASTID = D.HREMPLOYMASTID JOIN GTDEPTDESGMAST E ON E.GTDEPTDESGMASTID = D.DEPTNAME WHERE D.IDACTIVE='NO' AND  D.MIDCARD='{sdwEnrollNumber.Trim()}' AND B.COMPCODE='" + Class.Users.HCompcode + "'   ORDER BY C.IDCARDNO DESC";
                                    //    dt1 = Utility.ExecuteSelectQuery(sql, "HREMPLOYDETAILS").Tables[0];
                                    dt1 = dev.FindName1(sdwEnrollNumber.Trim(), Class.Users.HCompcode);
                                }
                                if (dt1 != null && dt1.Rows.Count > 0)
                                {
                                    displayName = dt1.Rows[0]["EMPNAME"].ToString();
                                }
                                else
                                {
                                    //        sql = "";
                                    //        sql = $"SELECT C.FNAME || '-' || E.MNNAME1 AS EMPNAME FROM GTCOMPMAST B JOIN HREMPLOYMAST C ON C.COMPCODE = B.GTCOMPMASTID JOIN HREMPLOYDETAILS D ON C.HREMPLOYMASTID = D.HREMPLOYMASTID JOIN GTDEPTDESGMAST E ON E.GTDEPTDESGMASTID = D.DEPTNAME WHERE D.IDACTIVE='NO' AND  D.MIDCARD='{sdwEnrollNumber.Trim()}'   ORDER BY C.IDCARDNO DESC";
                                    //dt1 = Utility.ExecuteSelectQuery(sql, "HREMPLOYDETAILS").Tables[0];
                                    dt1 = dev.FindName1(sdwEnrollNumber.Trim(), Class.Users.HCompcode);
                                    if (dt1 != null && dt1.Rows.Count > 0)
                                    {
                                        displayName = dt1.Rows[0]["EMPNAME"].ToString();
                                    }
                                }
                                dtexcel.Rows.Add(comboMasterIp.Text, sdwEnrollNumber.ToString(), displayName + " - Index: " + idwFingerIndex);
                                displayName = "";
                            }                               
                           
                        }

                        // Build single row array once

                        string[] row = new string[] { "", r.ToString(), sdwEnrollNumber.ToString(), displayName, idwFingerIndex.ToString(), ssss, sss, card, iPrivilege.ToString(), sPassword, bEnabled ? "True" : "False", iFlag.ToString(), macip };

                        // Create ListViewItems for each ListView using the same row
                        ListViewItem itemMain = new ListViewItem(row);
                        ListViewItem itemAll = new ListViewItem(row);


                        // Add to listfilter (clone of main)
                        listfilter.Items.Add((ListViewItem)itemMain.Clone());

                        // Add to UI listviews
                        LvDownload.Items.Add(itemMain);

                        Lvdownall.Items.Add(itemAll);


                        // Alternate row coloring
                        Color bg = (r % 2 == 0) ? Color.White : Color.WhiteSmoke;
                        itemMain.BackColor = bg;
                        itemAll.BackColor = bg;


                        // Progress UI
                        lblprogress1.Text = "User ID:-" + sdwEnrollNumber.ToString();
                        // Avoid calling Refresh in tight loop; label update will appear after UI thread regains control

                        // increment counters
                        r++;
                        iIndex++;
                    } // end finger index loop
                } // end SSR_GetAllUserInfo loop

                // finalize
                LvDownload.EndUpdate();
                Lvdownall.EndUpdate();


                axCZKEM1.EnableDevice(iMachineNumber, true);
                Cursor = Cursors.Default;
                lblattcount.Text = "Total Employee Finger Rows Count  :" + LvDownload.Items.Count.ToString() + " and IP Addres   :" + comboMasterIp.Text;
                allip.Items.Clear(); allip1.Items.Clear();

                // refresh device data & cleanup
                listviewchecklistip.Items.Clear();
                combo_ToIPload();


                Cursor = Cursors.Default;
               
            }
            catch (Exception ex)
            {
                MessageBox.Show("Framework invaid", "Install Zkemkeeper.dll " + ex.ToString());
            }
            finally
            {
                if (dtexcel.Rows.Count > 0)
                {

                    StreamWriter swExtLogFile = new StreamWriter(combinepath + "/IDCard_Remove_Details.txt", true);
                    int ii = 1;
                    swExtLogFile.Write(Environment.NewLine);
                    swExtLogFile.WriteLine(Class.Users.HCompcode + " ACTIVE - 'NO' Employee  - Details . Pls Remove From Machine  :   ");
                    swExtLogFile.Write("============================");
                    swExtLogFile.Write(Environment.NewLine);

                    foreach (DataRow rows in dtexcel.Rows)
                    {

                        swExtLogFile.Write(rows[0].ToString() + "      " + rows[1].ToString() + "     " + rows[2].ToString() + "  ");
                        swExtLogFile.Write(Environment.NewLine);
                        ii++;
                    }

                    swExtLogFile.Write("Total Record Count   :" + ii.ToString() + "  ********END OF DATA*********");
                    swExtLogFile.Flush();
                    swExtLogFile.Close();
                }
                lblprogress1.Text = "";

                if (axCZKEM1.ClearAdministrators(iMachineNumber))
                {
                    axCZKEM1.RefreshData(iMachineNumber);
                 
                    bIsConnected = false;
                }
                
            }
        
            
        }
        private void AttIPLoad()
        {
            string ss = Class.Users.HCompcode;
            listviewattdown.Items.Clear();
            Class.Users.Intimation = "PAYROLL";

            DataTable dt1 = dev.ToIp(ss);
            btnConnect.Text = "Finger Download ??";
            lblState.Text = "Current State: DisConnected";

            if (dt1 == null || dt1.Rows.Count == 0)
            {
                //MessageBox.Show("No data found", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int iIndex = 1;

            foreach (DataRow row in dt1.Rows)
            {
                var listItem = new ListViewItem
                {
                    BackColor = (iIndex % 2 == 0) ? Color.White : Color.WhiteSmoke
                };

                // Main Text (first column)
                listItem.Text = "";  // Or row["something"] if needed

                // Subitems (remaining columns)
                listItem.SubItems.Add("");                         // 2nd column
                listItem.SubItems.Add(row["MACIP"].ToString());    // 3rd column
                listItem.SubItems.Add("------");                   // 4th column
                listItem.SubItems.Add("");                         // 5th column
                listItem.SubItems.Add("");                         // 6th column

                listviewattdown.Items.Add(listItem);
                iIndex++;
            }



        }
        private void combo_ToIPload()
        {
            listviewchecklistip1.Items.Clear();
            listviewchecklistip2.Items.Clear();
            listviewchecklistip.Items.Clear();
            listremovechecklistip.Items.Clear();

            string ss = Class.Users.HCompcode;
            Class.Users.Intimation = "PAYROLL";

            DataTable dt1 = dev.AllIp(Class.Users.HCompcode);

            if (dt1.Rows.Count > 0)
            {
                int index = 1;

                foreach (DataRow row in dt1.Rows)
                {
                    string ip = row["MACIP"].ToString();
                    bool isEvenRow = (index % 2 == 0);
                    Color rowColor = isEvenRow ? Color.White : Color.WhiteSmoke;

                    // Create row for each list
                    listviewchecklistip.Items.Add(CreateRow(ip, rowColor));
                    listremovechecklistip.Items.Add(CreateRow(ip, rowColor));
                    listviewchecklistip1.Items.Add(CreateRow(ip, rowColor));
                    listviewchecklistip2.Items.Add(CreateRow(ip, rowColor));

                    index++;
                }
            }
            else
            {
               // MessageBox.Show("No data found", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            // ------------------------------------------------------
            // REUSABLE METHOD (No duplication anymore)
            // ------------------------------------------------------
            ListViewItem CreateRow(string ip, Color bgColor)
            {
                var item = new ListViewItem();
                item.SubItems.Add(ip);
                item.SubItems.Add("------");
                item.BackColor = bgColor;
                return item;
            }



        }

        private void combo_RemoveIPload()
        {

           
            string ss = Class.Users.HCompcode;
            Class.Users.Intimation = "PAYROLL";

            DataTable dt1 = dev.AllIp(ss, comboMasterIp.Text);

            listremovechecklistip.Items.Clear();   // Always clear before load

            if (dt1.Rows.Count > 0)
            {
                int iIndex = 1;

                foreach (DataRow row in dt1.Rows)
                {
                    string ip = row["MACIP"].ToString();

                    ListViewItem item = new ListViewItem();
                    item.SubItems.Add(ip);
                    item.SubItems.Add("------");

                    // Alternating row color
                    item.BackColor = (iIndex % 2 == 0) ? Color.White : Color.WhiteSmoke;

                    listremovechecklistip.Items.Add(item);

                    iIndex++;
                }
            }
            else
            {
               // MessageBox.Show("No data found", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void faceip()
        {


            string ss = Class.Users.HCompcode;
            DataTable dt2 = dev.ToIp(ss);
            Class.Users.Intimation = "PAYROLL";
            
            if (dt2?.Rows.Count > 0)
            {
                // Common binding method
                void BindCombo(ComboBox combo, DataTable dt)
                {
                    combo.DisplayMember = "MACIP";
                    combo.ValueMember = "MACIP";
                    combo.DataSource = dt;
                    combo.SelectedIndex = -1;
                }

                // Bind all combos with dt2
                
                BindCombo(combocardreaderipbox, dt2);

                // Bind remove-ip from dt3
                DataTable dt3 = dev.AllIp();           
                BindCombo(comboremoveip, dt3);
            }
            else
            {
                // Clear all combos if dt2 is empty
                
                combocardreaderipbox.DataSource = null;
                comboremoveip.DataSource = null;
            }


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
            
            Class.Users.UserTime = 0;
          
            iIndex = listViewupload.Items.Count;
            progressBar1.Value = 0;

            if (e.Item.Checked)
            {
                ListViewItem itt = new ListViewItem();
                // Copy all subitems safely
                for (int i = 1; i < e.Item.SubItems.Count; i++)
                {
                    itt.SubItems.Add(e.Item.SubItems[i].Text);
                }

                // Alternating row color
                itt.BackColor = (iIndex % 2 == 0) ? Color.White : Color.WhiteSmoke;
                if (itt.SubItems[2].Text != "")
                {
                    listViewupload.Items.Add(itt);
                }
                iIndex++;
            }

 
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
            try
            {
                if (listViewupload.Items.Count > 0)
                {
                    var confirmation = MessageBox.Show("Do You want Delete this Record ?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (confirmation == DialogResult.Yes)
                    {
                        int i = 0;
                        for (i = 0; i < listViewupload.Items.Count; i++)
                        {

                            if (listViewupload.Items[i].Selected)
                            {

                                listViewupload.Items[i].Remove();
                                i--;
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("pls upload data from Master IP", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Btntransfer_Click(object sender, EventArgs e)
        {
            try
            {
                txtsearch.Text = "";
                Class.Users.UserTime = 0;


                if (listviewchecklistip.CheckedItems.Count == 0)
                {
                    MessageBox.Show("Machine not connected. Please send IP Address", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                DialogResult result = MessageBox.Show("Do you want to export Finger Index?", "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (result != DialogResult.OK) return;

                Cursor = Cursors.WaitCursor;

                for (int j = 0; j < allip.Items.Count; j++)
                {
                    

                    spl = null; string deviceIp = "";
                    if (Class.Users.HCompcode == "LOPPL")
                    {
                        spl = allip.Items[j].SubItems[1].Text.Trim().Split('/');
                        deviceIp = spl[0].ToString();
                    }
                    else
                    {

                        deviceIp = allip.Items[j].SubItems[1].Text.ToString();
                    }

                    if (deviceIp.Length <= 10)
                    {
                        MessageBox.Show("Invalid IP : " + deviceIp);
                        continue;
                    }

                    lblattcount.Text = "";
                    lblState.Text = "Current State: Connecting...";

                    bool connected = axCZKEM1.Connect_Net(deviceIp, Convert.ToInt32(txtPort.Text));
                    if (!connected)
                    {
                        MessageBox.Show("Machine Disconnected : " + deviceIp);
                        continue;
                    }

                    lblState.Text = "Current State: Connected";
                    lblattcount.Refresh();
                    lblattcount.Text = "Connected " + deviceIp;
                    axCZKEM1.EnableDevice(iMachineNumber, false);
                  
                    // Progress setup
                    int total = listViewupload.Items.Count;
                    progressBar1.Minimum = 0;
                    progressBar1.Maximum = total;

                    for (int i = 0; i < total; i++)
                    {
                        ListViewItem it = listViewupload.Items[i];                    
                        
                        // Read values
                        sdwEnrollNumber = it.SubItems[2].Text;
                        sName = it.SubItems[3].Text;
                        int fingerValue = Convert.ToInt32("0" + it.SubItems[4].Text);

                        // odd → 1, even → 2
                        idwFingerIndex = (fingerValue % 2 == 1) ? 1 : 2;                       
                       
                       
                        sTmpData = it.SubItems[5].Text;
                        sTmpData1 = it.SubItems[6].Text;
                        sCardnumber = it.SubItems[7].Text;
                        iPrivilege = Convert.ToInt32("0" + it.SubItems[8].Text);
                        sPassword = it.SubItems[9].Text;
                        sEnabled = it.SubItems[10].Text;
                        int iFlag = Convert.ToInt32("0" + it.SubItems[11].Text);

                        bEnabled = (sEnabled == "True");

                        // Upload User + Template
                        axCZKEM1.SetStrCardNumber(sCardnumber);
                        if (!axCZKEM1.SSR_SetUserInfo(iMachineNumber, sdwEnrollNumber, sName, sPassword, iPrivilege, bEnabled))
                        {
                            // Even if user exists → Still continue uploading fingerprint
                        }

                        axCZKEM1.SetUserTmpExStr(iMachineNumber, sdwEnrollNumber, idwFingerIndex, iFlag, sTmpData);

                        // UI Text
                        lblattcount.Text = $"Uploading fingerprint... {i + 1}/{total}   IP: {deviceIp}";
                        decimal per = ((i + 1) * 100) / total;
                        lblprogress1.Text = $"{per:N0}%  (ID: {sdwEnrollNumber})";
          
                        lblattcount.Refresh();
                        lblprogress1.Refresh();

                        progressBar1.Value = i + 1;
                    }

                    // Finalize upload
                    

                    lblattcount.Text = "Successfully uploaded fingerprints. Total: " + listViewupload.Items.Count + "  IP: " + deviceIp;
                    progressBar1.Value = 0; 
                }
            }
            catch
            {
                MessageBox.Show("Please connect device", "Error");
            }
            finally
            {
                Cursor = Cursors.Default;
                comboMasterIp.Enabled = true;
                btnConnect.Enabled = true; listViewupload.Items.Clear();allip.Items.Clear();
                lblState.Text = "Current State: Disconnected";
                axCZKEM1.BatchUpdate(iMachineNumber);
                if (axCZKEM1.ClearAdministrators(iMachineNumber))
                {
                    axCZKEM1.RefreshData(iMachineNumber);

                    bIsConnected = false;
                }
                axCZKEM1.EnableDevice(iMachineNumber, true);checkallrows.Checked = false;
            }

           
        }


      

     


        //Clear all the administrator privilege(not clear the administrators themselves)
        private void BtnClearAdministrators_Click(object sender, EventArgs e)
        {

            if (comboremoveip.Text != "")
            {
                MyCount = 0; bIsConnected = false; Cursor = Cursors.WaitCursor;
                bIsConnected = axCZKEM1.Connect_Net(comboremoveip.Text, Convert.ToInt32(txtPort.Text));
                if (bIsConnected == true)
                {
                    idwErrorCode = 0;

                    Cursor = Cursors.WaitCursor;
                    if (axCZKEM1.ClearAdministrators(iMachineNumber))
                    {
                        axCZKEM1.RefreshData(iMachineNumber);
                        Cursor = Cursors.Default;
                        //the data in the device should be refreshed
                        MessageBox.Show("Successfully clear administrator privilege from teiminal!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else
                    {
                        axCZKEM1.GetLastError(ref idwErrorCode); Cursor = Cursors.Default;
                        MessageBox.Show("Operation failed,ErrorCode=" + idwErrorCode.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            Cursor = Cursors.Default;
        }

        //Delete all the user information in the device,while the related fingerprint templates will be deleted either. 
        //(While the parameter DataFlag  of the Function "ClearData" is 5 )
        private void BtnClearDataUserInfo_Click(object sender, EventArgs e)
        {

            if (comboremoveip.SelectedIndex >= 0)
            {
                MyCount = 0;
                bIsConnected = false;
                Cursor = Cursors.WaitCursor;
                //------------------------Clear Data Start----------------------------//
                DialogResult result1 = MessageBox.Show("Do You want to Clear All Data from Machine??\n'   IP ADDRESS ---" + comboremoveip.Text + "'", "Attendance Logs", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (result1.Equals(DialogResult.OK))
                {
                    int idwErrorCode = 0;

                    int iDataFlag = 5;
                    bIsConnected = axCZKEM1.Connect_Net(comboremoveip.Text, Convert.ToInt32(txtPort.Text));
                    if (bIsConnected == true)
                    {

                        if (axCZKEM1.ClearData(iMachineNumber, iDataFlag))
                        {
                            axCZKEM1.RefreshData(iMachineNumber);//the data in the device should be refreshed
                            MessageBox.Show("Clear all the UserInfo data!", "Success");

                        }
                        else
                        {
                            axCZKEM1.GetLastError(ref idwErrorCode);
                            MessageBox.Show("Operation failed,ErrorCode=" + idwErrorCode.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    Cursor = Cursors.Default;
                    lblattcount.Text = "Employee All Details are Deleted from Machine  : and Connected IP Addres   :" + comboremoveip.Text;

                }
                //    ////---------------------Clear Data End--------------------------------//


            }
            Cursor = Cursors.Default;
        }

        //Clear all the fingerprint templates in the device(While the parameter DataFlag  of the Function "ClearData" is 2 )
        private void BtnClearDataTmps_Click(object sender, EventArgs e)
        {
            if (comboremoveip.Text != "")
            {
                MyCount = 0; Cursor = Cursors.WaitCursor;

                int idwErrorCode = 0;

                int iDataFlag = 1;


                DialogResult result1 = MessageBox.Show("Do You want to Clear from Machine??\n'    IP ADDRESS ---" + comboremoveip.Text + "'", "Attendance Logs", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (result1.Equals(DialogResult.OK))
                {
                    bIsConnected = axCZKEM1.Connect_Net(comboremoveip.Text, Convert.ToInt32(txtPort.Text));
                    if (bIsConnected == true)
                    {
                        axCZKEM1.EnableDevice(iMachineNumber, false);//disable the device
                        if (axCZKEM1.ClearGLog(iMachineNumber))
                        {
                            axCZKEM1.RefreshData(iMachineNumber);//the data in the device should be refreshed
                            MessageBox.Show("Clear all the fingerprint templates!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            axCZKEM1.GetLastError(ref idwErrorCode);
                            MessageBox.Show("Operation failed,ErrorCode=" + idwErrorCode.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        axCZKEM1.EnableDevice(iMachineNumber, true);//enable the device
                        lblattcount.Text = "Attendance Logs are Cleared From Machine  and Connected IP Addres   :" + comboremoveip.Text;

                    }
                }
            }
            Cursor = Cursors.Default;
        }




        public void News() {
            comboMasterIp.SelectedIndex = -1; butsingleuser.Visible = true;
            comboMasterIp.SelectedIndex = -1; lbRTShow.Visible = false;
            combocardreaderipbox.SelectedIndex = -1;
            atttotal.Text = "";
            allip.Items.Clear();
            allip1.Items.Clear();
            allip2.Items.Clear();
            //SecondtabControl2.Font = Class.Users.FontName;
            listViewupload.Items.Clear();
            LvDownload.Items.Clear();
            listviewchecklistip.Refresh();
            listviewchecklistip.ForeColor = Class.Users.ForeColors;
            //lvLogs.ForeColor = Class.Users.ForeColors;
            butheader.BackColor = Class.Users.BackColors;
            panel3.BackColor = Class.Users.BackColors;
            panel2.BackColor = Class.Users.BackColors;
            splitter1.BackColor = Class.Users.BackColors;
            splitter2.BackColor = Class.Users.BackColors;
            splitter4.BackColor = Class.Users.BackColors;
            splitter3.BackColor = Class.Users.BackColors;
            splitter5.BackColor = Class.Users.BackColors;
            splitercardreader.BackColor = Class.Users.BackColors;
           
            label10.BackColor = Class.Users.BackColors;
            label12.BackColor = Class.Users.BackColors;
            lblattsearch.BackColor = Class.Users.BackColors;
            checkremoveall.BackColor = Class.Users.BackColors;
            lblremovesearch.BackColor = Class.Users.BackColors;
            label15.BackColor = Class.Users.BackColors;
            label16.BackColor = Class.Users.BackColors;
            
            this.BackColor = Class.Users.BackColors;
            label27.BackColor = Class.Users.BackColors;
            label35.BackColor = Class.Users.BackColors;
            checkall.BackColor = Class.Users.BackColors;

            checkCard.BackColor = Class.Users.BackColors;
            label13.BackColor = Class.Users.BackColors;
            label14.BackColor = Class.Users.BackColors;
            checkface.BackColor = Class.Users.BackColors;
            //Lvdownall.Font = Class.Users.FontName;
            //LvDownload.Font = Class.Users.FontName;
            //LvDownload1.Font = Class.Users.FontName;
            //lvCard.Font = Class.Users.FontName;
            //LvDownload2.Font = Class.Users.FontName;
            //listviewattdown.Font = Class.Users.FontName;
            checkall.Checked = false;
            checkallrows.BackColor = Class.Users.BackColors;
            lblsearch.BackColor = Class.Users.BackColors;
            listfilter.Items.Clear(); progressBar1.Value = 0; lblattcount.Text = ""; lblprogress1.Text = ""; listviewchecklistip.Items.Clear(); listviewchecklistip1.Items.Clear(); listviewchecklistip2.Items.Clear();
            Lvdownremove.Items.Clear(); listviewattdown.Items.Clear(); listremovechecklistip.Items.Clear(); allip2.Items.Clear();
            Lvdownall.Items.Clear(); allip.Items.Clear(); allip1.Items.Clear();
            comboMasterIp.SelectedIndex = -1;
            faceip(); combo_ToIPload();
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
            try
            {
                LvDownload.Items.Clear();
                int index = 0;
                string search = txtsearch.Text.Trim();

                foreach (ListViewItem src in listfilter.Items)
                {
                    bool match = true;

                    if (search.Length > 0)
                    {
                        // Search only in column 2 & 3
                        match =
                            (src.SubItems[2].Text.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0) ||
                            (src.SubItems[3].Text.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0);
                    }

                    if (match)
                    {
                        ListViewItem newItem = (ListViewItem)src.Clone();

                        // Alternating row colors
                        newItem.BackColor = (index % 2 == 0) ? Color.White : Color.WhiteSmoke;

                        LvDownload.Items.Add(newItem);
                        index++;
                    }
                }
            }
            catch { }

           

        }

        private void BtnSetStrCardNumber_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                bIsConnected = axCZKEM1.Connect_Net(combocardreaderipbox.Text, Convert.ToInt32(txtPort.Text));
                if (combocardreaderipbox.SelectedIndex >= 0)
                {
                    if (bIsConnected == false)
                    {
                        Cursor = Cursors.Default;
                        MessageBox.Show("Please connect the device first!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (bIsConnected == true)
                    {
                        if (cbUserId_Card.Text.Trim() == "")
                        {
                            MessageBox.Show("UserID,Privilege,Cardnumber,Name must be inputted first!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            cbUserId_Card.Focus(); Cursor = Cursors.Default;
                            return;
                        }
                        idwErrorCode = 0;

                        sdwEnrollNumber = cbUserId_Card.Text;
                        sName = txtName.Text;
                        //idwFingerIndex = Convert.ToInt32(txtcardfindex.Text);
                        // sTmpData = txtcardfingerimage.Text;
                        // sTmpData1 = txtcardfaceimage.Text;
                        sCardnumber = txtCardnumber.Text.Trim();
                        //  iPrivilege = Convert.ToInt32("0" + cbPrivilegecard.Text);
                        //  sPassword = txtPassword.Text;
                        // idwFingerIndex = Convert.ToInt32(txtcardfindex.Text);
                        // if (chbEnabled.Checked) { bEnabled = true; } else { bEnabled = false; }
                        // Cursor = Cursors.WaitCursor;
                        axCZKEM1.EnableDevice(iMachineNumber, false);
                        // UICO uti = new UICO();
                        if (checkcard1.Checked == true)
                        {
                            sCardnumber = null; checkcard1.Checked = false;
                            axCZKEM1.SetStrCardNumber(sCardnumber);//Before you using function SetUserInfo,set the card number to make sure you can upload it to the device

                            if (axCZKEM1.SSR_SetUserInfo(iMachineNumber, sdwEnrollNumber, sName, sPassword, iPrivilege, bEnabled))//face templates are part of users' information
                            {
                                axCZKEM1.SetUserTmpExStr(iMachineNumber, sdwEnrollNumber, idwFingerIndex, iFlag, sTmpData);//upload templates information to the memory

                                axCZKEM1.SetUserFaceStr(iMachineNumber, sdwEnrollNumber, iFaceIndex, sTmpData1, iLength);//upload face templates information to the device

                                MessageBox.Show("SetUserInfo,UserID:" + sdwEnrollNumber + " Privilege:" + iPrivilege.ToString() + " Enabled:" + bEnabled.ToString(), "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);


                            }
                            else
                            {
                                axCZKEM1.GetLastError(ref idwErrorCode);
                                MessageBox.Show("Operation failed,ErrorCode=" + idwErrorCode.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                axCZKEM1.EnableDevice(iMachineNumber, true);
                                return;
                            }
                        }
                        if (checkfinger1.Checked == true)
                        {
                            sTmpData = ""; checkfinger1.Checked = false;
                            axCZKEM1.SetStrCardNumber(sCardnumber);//Before you using function SetUserInfo,set the card number to make sure you can upload it to the device

                            if (axCZKEM1.SSR_SetUserInfo(iMachineNumber, sdwEnrollNumber, sName, sPassword, iPrivilege, bEnabled))//face templates are part of users' information
                            {
                                axCZKEM1.SetUserTmpExStr(iMachineNumber, sdwEnrollNumber, idwFingerIndex, iFlag, sTmpData);//upload templates information to the memory

                                axCZKEM1.SetUserFaceStr(iMachineNumber, sdwEnrollNumber, iFaceIndex, sTmpData1, iLength);//upload face templates information to the device

                                MessageBox.Show("SetUserInfo,UserID:" + sdwEnrollNumber + " Privilege:" + iPrivilege.ToString() + " Enabled:" + bEnabled.ToString(), "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);


                            }
                            else
                            {
                                axCZKEM1.GetLastError(ref idwErrorCode);
                                MessageBox.Show("Operation failed,ErrorCode=" + idwErrorCode.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                axCZKEM1.EnableDevice(iMachineNumber, true);
                                return;
                            }
                        }
                        if (checkface1.Checked == true)
                        {
                            sTmpData1 = null; checkface1.Checked = false;
                            axCZKEM1.SetStrCardNumber(sCardnumber);//Before you using function SetUserInfo,set the card number to make sure you can upload it to the device

                            if (axCZKEM1.SSR_SetUserInfo(iMachineNumber, sdwEnrollNumber, sName, sPassword, iPrivilege, bEnabled))//face templates are part of users' information
                            {
                                axCZKEM1.SetUserTmpExStr(iMachineNumber, sdwEnrollNumber, idwFingerIndex, iFlag, sTmpData);//upload templates information to the memory

                                axCZKEM1.SetUserFaceStr(iMachineNumber, sdwEnrollNumber, iFaceIndex, sTmpData1, iLength);//upload face templates information to the device

                                MessageBox.Show("SetUserInfo,UserID:" + sdwEnrollNumber + " Privilege:" + iPrivilege.ToString() + " Enabled:" + bEnabled.ToString(), "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);


                            }
                            else
                            {
                                axCZKEM1.GetLastError(ref idwErrorCode);
                                MessageBox.Show("Operation failed,ErrorCode=" + idwErrorCode.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                axCZKEM1.EnableDevice(iMachineNumber, true);
                                return;
                            }
                        }

                        if (checkcard1.Checked == false && checkfinger1.Checked == false && checkface1.Checked == false)
                        {

                            axCZKEM1.SetStrCardNumber(sCardnumber);//Before you using function SetUserInfo,set the card number to make sure you can upload it to the device

                            if (axCZKEM1.SSR_SetUserInfo(iMachineNumber, sdwEnrollNumber, sName, sPassword, iPrivilege, bEnabled))//face templates are part of users' information
                            {
                                axCZKEM1.SetUserTmpExStr(iMachineNumber, sdwEnrollNumber, idwFingerIndex, iFlag, sTmpData);//upload templates information to the memory

                                axCZKEM1.SetUserFaceStr(iMachineNumber, sdwEnrollNumber, iFaceIndex, sTmpData1, iLength);//upload face templates information to the device



                            }
                            else
                            {
                                axCZKEM1.GetLastError(ref idwErrorCode);
                                MessageBox.Show("Operation failed,ErrorCode=" + idwErrorCode.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                axCZKEM1.EnableDevice(iMachineNumber, true);
                                return;
                            }

                        }
                        axCZKEM1.BatchUpdate(iMachineNumber);//upload all the information in the memory
                        if (axCZKEM1.ClearAdministrators(iMachineNumber))
                        {
                            axCZKEM1.RefreshData(iMachineNumber);

                            bIsConnected = false;
                        }

                        axCZKEM1.EnableDevice(iMachineNumber, true);

                        Cursor = Cursors.Default;
                        txtName.Text = ""; txtPassword.Text = ""; txtCardnumber.Text = ""; chbEnabled.Checked = false;
                    }
                }
                else
                {
                    Cursor = Cursors.Default;
                    MessageBox.Show("Pls Select Ip List");
                }
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                MessageBox.Show("-------" + sCardnumber + "--" + ex.ToString());
            }
            // BtnGetStrCardNumber_Click(sender, e);
        }


        private void BtnConnects_Click(object sender, EventArgs e)
        {
            atttotal.Text = "";
            string ccode = ""; Class.Users.Intimation = "PAYROLL"; lbRTShow.Visible = false; StreamWriter swExtLogFile;
            DataTable dtexcel = new DataTable();string binddata = "";
            ccode = Class.Users.HCompcode; Class.Users.UserTime = 0;
            dtexcel.Rows.Clear(); DTROW.Rows.Clear(); 
            dtexcel.Columns.Clear();
            dtexcel.Columns.Add("IPADDRESS");
            dtexcel.Columns.Add("DETAILS");
            dtexcel.Columns.Add("DATETIME");
            if (!Directory.Exists(combinepath))
            {
                Directory.CreateDirectory(combinepath);
            }
            if (DTROW.Columns.Count <= 0)
            {
                DTROW.Columns.Add("MACHINENUMBER");
                DTROW.Columns.Add("IPADDRESS");
                DTROW.Columns.Add("ENROLLNO");
                DTROW.Columns.Add("DATETIMERECORD");
            }
           
            string macno, mactype, mactype2, idcard = "", dates = "", months = "", h = "", m = "", s = "", dat = "", time = "", inputDate = "",ip = "";
            swExtLogFile = new StreamWriter(combinepath + "/Attendance.txt", true);
         
          totalrows = 0;
            spl = null; string deviceIp = "";          
            
            try
            {
                progressBar1.Value = 0;
                int k = 0, j = 0;
                iIndex = 0; DTROW.Rows.Clear();
                iGLCount = 0; listfilter.Items.Clear();
                if (reply1.Status.ToString() == "Success")
                {
                    if (allip1.Items.Count > 0)
                    {
                        ip = ""; macno = ""; mactype = ""; mactype2 = "";

                        int connectip = 0; bIsConnected = false;
                        for (j = 0; j < allip1.Items.Count; j++)
                        {
                            lblattcount.Refresh(); lblattcount.Refresh(); totalrows = 0;
                            lblattcount.Text = "Connecting..." + allip1.Items[j].SubItems[2].Text + " Count:" + DTROW.Rows.Count.ToString();
                            
                            if (allip1.Items[j].SubItems[6].Text == "True")
                            {
                                connectip = j + 1; DataTable dt;
                                spl = allip1.Items[j].SubItems[2].Text.Trim().Split('/');
                                if (Class.Users.HCompcode == "LOPPL") {
                                     dt = dev.IPLOAD(Class.Users.HCompcode, spl[0]);
                                } else {
                                     dt = dev.IPLOAD(Class.Users.HCompcode, allip1.Items[j].SubItems[2].Text);
                                }
                                if (dt.Rows.Count > 0)
                                {
                                    int maxip = dt.Rows.Count;
                                    int i = 0; Class.Users.UserTime = 0;
                                    if (maxip == 1)
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
                                            while (axCZKEM1.SSR_GetGeneralLogData(iMachineNumber, out sdwEnrollNumber, out idwVerifyMode, out idwInOutMode, out idwYear, out idwMonth, out idwDay, out idwHour, out idwMinute, out idwSecond, ref idwWorkcode))//get records from the memory
                                            {
                                                inputDate = "";
                                                inputDate = idwDay + "-" + idwMonth + "-" + idwYear;
                                                if (Convert.ToDateTime(inputDate) >= frmdate.Value.Date && Convert.ToDateTime(inputDate) <= todate.Value.Date.AddDays(1).AddTicks(-1))
                                                {
                                                    idcard = ""; dates = ""; months = ""; h = ""; s = ""; m = ""; dat = ""; time = "";
                                                    idcard = sdwEnrollNumber;
                                                    dates = idwDay.ToString().Length < 2 ? "0" + idwDay.ToString() : idwDay.ToString();
                                                    months = idwMonth.ToString().Length < 2 ? "0" + idwMonth.ToString() : idwMonth.ToString();
                                                    h = idwHour.ToString().Length < 2 ? "0" + idwHour.ToString() : idwHour.ToString();
                                                    m = idwMinute.ToString().Length < 2 ? "0" + idwMinute.ToString() : idwMinute.ToString();
                                                    s = idwSecond.ToString().Length < 2 ? "0" + idwSecond.ToString() : idwSecond.ToString();
                                                    dat = dates.ToString() + "-" + months.ToString() + "-" + idwYear.ToString();
                                                    time = h.ToString() + ":" + m.ToString() + ":" + s.ToString();
                                                    iGLCount++;
                                                    DataRow dr = DTROW.NewRow();
                                                    dr["MACHINENUMBER"] = macno;
                                                    dr["IPADDRESS"] = ip;
                                                    dr["ENROLLNO"] = idcard;
                                                    dr["DATETIMERECORD"] = dat + " " + time;
                                                    DTROW.Rows.Add(dr);                                                  
                                                }
                                                totalrows += 1;
                                            }
                                            lblattcount.Refresh();
                                            atttotal.Refresh();
                                            atttotal.Text = "Record Storage Capacity: Up to 1L Entries: This Machine '" + ip.ToString() + "'  Total Records :" + totalrows.ToString();
                                            if (DTROW.Rows.Count > 0)
                                            {
                                                lblattcount.Text = "Logs Download Completed.:  " + ip.ToString() + "  Records : "+DTROW.Rows.Count.ToString();
                                                dtexcel.Rows.Add(lblattcount.Text, " ", "");
                                               
                                                if (Class.Users.ValidCheck == true)
                                                {
                                                   
                                                    if (totalrows >= 99999)
                                                    {
                                                        DialogResult result1 = MessageBox.Show($"You have reached Max Att Logs this Biometric Device. Pls clear Logs ? (Max Logs: {totalrows.ToString()})\nIP ADDRESS --- {ip}", "Attendance Logs", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

                                                        if (result1 == DialogResult.OK && !string.IsNullOrEmpty(ip))
                                                        {
                                                            Cursor = Cursors.WaitCursor;
                                                            MyCount = 0;
                                                            int idwErrorCode = 0;
                                                          
                                                            bIsConnected = axCZKEM1.Connect_Net(ip, Convert.ToInt32(txtPort.Text));
                                                            if (bIsConnected)
                                                            {
                                                                axCZKEM1.EnableDevice(iMachineNumber, false);
                                                                if (axCZKEM1.ClearGLog(iMachineNumber))
                                                                {
                                                                    axCZKEM1.RefreshData(iMachineNumber);
                                                                    MessageBox.Show("All fingerprint logs cleared successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                                }
                                                                else
                                                                {
                                                                    axCZKEM1.GetLastError(ref idwErrorCode);
                                                                    MessageBox.Show($"Operation failed. ErrorCode = {idwErrorCode}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                }

                                                                axCZKEM1.EnableDevice(iMachineNumber, true);
                                                                lblattcount.Text = $"Attendance Logs cleared from machine IP Address: {comboremoveip.Text}";
                                                            }

                                                            Cursor = Cursors.Default;
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                lblattcount.Text = "No Data Found:" + ip.ToString();
                                                dtexcel.Rows.Add(lblattcount.Text, " ", "");
                                            }
                                            Cursor = Cursors.Default;

                                        }
                                        else
                                        {
                                            lblattcount.Refresh();
                                            lblattcount.Text = "Dis-Connected..:" + ip.ToString();
                                            Cursor = Cursors.Default;
                                            lblattcount.Refresh(); Cursor = Cursors.Default;
                                            dtexcel.Rows.Add(lblattcount.Text, " ", "");
                                        }
                                        axCZKEM1.EnableDevice(iMachineNumber, true);//enable the device    
                                        if (axCZKEM1.ClearAdministrators(iMachineNumber))
                                        {
                                            axCZKEM1.RefreshData(iMachineNumber);
                                            bIsConnected = false;
                                        }
                                    }
                                    else
                                    {
                                        lblattcount.Refresh();
                                        lblattcount.Text = "Total IP Counts : '" + dt.Rows.Count.ToString() + "'. Pls Remove from Master-Entry Screen ( Duplicate IP ) : " + allip1.Items[j].SubItems[2].Text;
                                        dtexcel.Rows.Add(lblattcount.Text + ",", ",");

                                        return;
                                    }
                                }
                                else
                                {
                                    lblattcount.Refresh();
                                    lblattcount.Text = "This IP not found in Oracle Table" + allip1.Items[j].SubItems[2].Text;
                                    dtexcel.Rows.Add(lblattcount.Text + ",", ",");


                                }
                            }
                            else
                            {
                                dtexcel.Rows.Add(lblattcount.Text + "," + allip1.Items[j].SubItems[2].Text + ",");

                            }
                        }

                        if (connectip == allip1.Items.Count)
                        {
                            Cursor = Cursors.WaitCursor;
                            if (DTROW.Rows.Count >= 1)
                            {
                                binddata = "SELECT distinct A.PRINTS    FROM ASPTBLUSERRIGHTS A JOIN GTCOMPMAST B ON A.COMPCODE=B.GTCOMPMASTID JOIN ASPTBLUSERMAS C ON C.USERID=A.USERNAME AND C.COMPCODE=A.COMPCODE  WHERE B.COMPCODE='" + Class.Users.HCompcode + "' AND A.MENUNAME='" + Class.Users.ScreenName + "'";

                                DataSet bindds = Utility.ExecuteSelectQuery(binddata, "ASPTBLUSERRIGHTS");
                                DataTable binddt = bindds.Tables["ASPTBLUSERRIGHTS"];
                                if (binddt.Rows[0]["PRINTS"].ToString() == "T")
                                {
                                    StreamWriter swExtLogFile1 = new StreamWriter(combinepath + "/Attendance-Logs.txt", true);
                                    int ii = 0;
                                    swExtLogFile1.Write(Environment.NewLine);
                                    swExtLogFile1.WriteLine(Class.Users.HCompcode + " Attendance - Details  :   ");
                                    swExtLogFile1.Write("============================");
                                    swExtLogFile1.Write(Environment.NewLine);
                                    foreach (DataRow rows in DTROW.Rows)
                                    {

                                        swExtLogFile1.Write(rows[1].ToString() + "      " + rows[2].ToString() + "     " + rows[3].ToString() + "  ");
                                        swExtLogFile1.Write(Environment.NewLine);
                                    }
                                    swExtLogFile1.Write("Total Record Count   :" + DTROW.Rows.Count.ToString() + "  ********END OF DATA*********");
                                    swExtLogFile1.Flush();
                                    swExtLogFile1.Close();
                                }
                                Class.Users.Intimation = "PAYROLL";
                                string tablePrefix = "";
                                string exec = "";

                                 lblattcount.Refresh(); lblattcount.Refresh();
                                lblattcount.Text = "Attendance Generation Undedr process (Procedure Script is Running... ); ";
                                
                                // CASE 1 : AGFMGII
                                if (Class.Users.HUnit == "AGFMGII")
                                {
                                    tablePrefix = Class.Users.HUnit;
                                    SaveAttLogs(tablePrefix, DTROW);
                                    exec = $"BEGIN ATTINSERT('{Class.Users.HUnit}', '{Class.Users.HUserName}'); END;";
                                    Utility.ExecuteNonQuery(exec);


                                }
                                // CASE 2 : HOSTEL / CANTEEN (with Sub procedure)
                                else if (Class.Users.HUnit.Equals("HOSTEL", StringComparison.OrdinalIgnoreCase) || Class.Users.HUnit.Equals("CANTEEN", StringComparison.OrdinalIgnoreCase))
                                {
                                    tablePrefix = Class.Users.HUnit;
                                    SaveAttLogs(tablePrefix, DTROW);
                                    exec = $"BEGIN {Class.Users.HUnitSub}ATTINSERT('{Class.Users.HUnit}', '{Class.Users.HUserName}'); END;";
                                    Utility.ExecuteNonQuery(exec);

                                }
                                // CASE 3 : ALL OTHER UNITS
                                else
                                {
                                    tablePrefix = Class.Users.HCompcode;
                                    SaveAttLogs(tablePrefix, DTROW);
                                    exec = $"BEGIN ATTINSERT('{Class.Users.HCompcode}', '{Class.Users.HUserName}'); END;";
                                    Utility.ExecuteNonQuery(exec);
                                }
                            }
                            else
                            {
                                lblattcount.Refresh(); Class.Users.Intimation = "";
                                lblattcount.Text = "No Data Found." + allip1.Items[j].SubItems[2].Text;
                            }
                            Cursor = Cursors.Default;
                            
                        }
                        

                    }
                    else
                    {
                        lblattcount.Refresh(); Class.Users.Intimation = "";
                        lblattcount.Text = "Pls select any IP in Listview ";               
                        dtexcel.Rows.Add(lblattcount.Text + ",", ",");
                    }

                }
                lblattcount.Refresh(); Cursor = Cursors.Default;
                lblattcount.Text = "Attendance Download Completed. " + DTROW.Rows.Count.ToString();

            }
            catch (Exception ex)
            {
                dtexcel.Rows.Add(binddata , ex.Message, "");    
            }
            finally
            {
                if (reply1.Status.ToString() == "TimedOut")
                {

                    dtexcel.Rows.Add("Connection String wrong .pls check Appliction config File. ('" + Class.Users.HCompcode + Environment.NewLine + "  This IP does't Connect in Network :" + data2[0].ToString() + " ", "", "");
                    swExtLogFile.Write(Environment.NewLine);
                    swExtLogFile.WriteLine(Class.Users.HCompcode + " CONNECTION-STRING - FAILD  :   ");
                    swExtLogFile.Write("======================================");
                    swExtLogFile.Write(Environment.NewLine);
                    swExtLogFile.Write(dtexcel.Rows[0]["IPADDRESS"].ToString());
                    swExtLogFile.Write(Environment.NewLine);
                    swExtLogFile.Write("**************************END OF DATA ********* ********* *********" + DateTime.Now.ToString());
                    swExtLogFile.Write(Environment.NewLine);
                    swExtLogFile.Flush();
                    swExtLogFile.Close();
                }
                else
                {
                    int ii = 0;
                  
                    swExtLogFile.Write(Environment.NewLine);
                    swExtLogFile.WriteLine(Class.Users.HCompcode + " Attendance - Details  :   ");
                    swExtLogFile.Write("============================");
                    swExtLogFile.Write(Environment.NewLine);
                    foreach (DataRow rows in dtexcel.Rows)
                    {
                        object[] array = rows.ItemArray;
                        for (ii = 0; ii < array.Length - 1; ii++)
                        {
                            swExtLogFile.Write(array[ii].ToString());
                            swExtLogFile.Write(Environment.NewLine);
                        }
                  
                    }
                    swExtLogFile.Write("**************************END OF DATA ********* ********* *********" + DateTime.Now.ToString());
                    swExtLogFile.Flush();
                    swExtLogFile.Close();
                }

                Class.Users.Intimation = "";
                AttIPLoad(); allip1.Items.Clear();
                btnConnects.Refresh(); DTROW.Rows.Clear();
                btnConnects.Text = "Connect / Import";
                lblState.Text = "Current State:DisConnected";
                Cursor = Cursors.Default;checkall.Checked = false;
                Utility.ExecuteNonQuery("commit");
                Utility.DisConnect();
            }

        }
        private void SaveAttLogs(string tablePrefix, DataTable dt)
        {
          
            user.SaveUsingOracleBulkCopy(tablePrefix + "TRS_ATTLOG", dt);
            user.SaveUsingOracleBulkCopy(tablePrefix + "TRS_TEMPATTLOG", dt);

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





        

        private void BtnDatabase_Click(object sender, EventArgs e)
        {

            
            
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



        private void BtnDelUserFace_Click(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first!", "Error");
                return;
            }

            
            int idwErrorCode = 0;

            string sUserID = "";
            int iFaceIndex = 50;
            Cursor = Cursors.WaitCursor;
            if (axCZKEM1.DelUserFace(iMachineNumber, sUserID, iFaceIndex))
            {
                axCZKEM1.RefreshData(iMachineNumber);
                MessageBox.Show("DelUserFace,UserID=" + sUserID, "Success");

            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                MessageBox.Show("Operation failed,ErrorCode=" + idwErrorCode.ToString(), "Error");
            }
            Cursor = Cursors.Default;
        }

        private void BtnGetUserFace_Click(object sender, EventArgs e)
        {

            int idwErrorCode = 0;

            string sUserID = "";
            int iFaceIndex = 50;//the only possible parameter value
            int iLength = 128 * 1024;//initialize the length(cannot be zero)
            byte[] byTmpData = new byte[iLength];
            Cursor = Cursors.WaitCursor;
            axCZKEM1.EnableDevice(iMachineNumber, false);

            if (axCZKEM1.GetUserFace(iMachineNumber, sUserID, iFaceIndex, ref byTmpData[0], ref iLength))
            {
                //Here you can manage the information of the face templates according to your request.(for example,you can sava them to the database)
                MessageBox.Show("GetUserFace,the  length of the bytes array byTmpData is " + iLength.ToString(), "Success");
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                MessageBox.Show("Operation failed,ErrorCode=" + idwErrorCode.ToString(), "Error");
            }

            axCZKEM1.EnableDevice(iMachineNumber, true);
            Cursor = Cursors.Default;
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
            //obj.DeleteAllEmpTmIFACE_FaceTm();
            MessageBox.Show("Successfully Data deleted from database");
        }





        private void cardemtpy()
        {
            txtCardnumber.Text = ""; cbUserId_Card.Text = ""; txtName.Text = ""; txtcardfindex.Text = ""; txtcardfingerimage.Text = "";
            txtcardfaceimage.Text = ""; chbEnabled.Checked = false;
        }
        private void BtnGetStrCardNumber_Click(object sender, EventArgs e)
        {

            if (combocardreaderipbox.SelectedIndex < 0)
            {
                MessageBox.Show("Please select IP Address from combobox");
                return;
            }

            Cursor = Cursors.WaitCursor;
            Class.Users.UserTime = 0;

            listfilter.Items.Clear();
            lvCard.Items.Clear();
            cardemtpy();

            btnConnect.Enabled = true;
            comboMasterIp.Enabled = true;
            btnConnect.Text = "Connect && Download ??";
            spl = null;
            spl = combocardreaderipbox.Text.Trim().Split('/');
            string deviceIp = spl[0].ToString();
            bIsConnected = axCZKEM1.Connect_Net(deviceIp, Convert.ToInt32(txtPort.Text));

            if (!bIsConnected)
            {
                Cursor = Cursors.Default;
                MessageBox.Show("Unable to connect device!");
                return;
            }

            lvCard.BeginUpdate();
            lblattcount.Text = "";
            lblState.Text = "Current State: Connected";

            axCZKEM1.EnableDevice(iMachineNumber, false);

            // Reset card data
            dtcard.Clear();
            dtcard.Columns.Clear();
            dtcard.Columns.Add("SNO");
            dtcard.Columns.Add("EmpID");
            dtcard.Columns.Add("EmpName");
            dtcard.Columns.Add("CardNo");

            iIndex = 1;

            string cardNum;

            // Fetch user list
            while (axCZKEM1.SSR_GetAllUserInfo(iMachineNumber,
                    out sdwEnrollNumber, out sName, out sPassword, out iPrivilege, out bEnabled))
            {
                cardNum = "";
                axCZKEM1.GetStrCardNumber(out cardNum);

                if (string.IsNullOrWhiteSpace(cardNum))
                    continue;

                // Build ListView row
                ListViewItem item = new ListViewItem(iIndex.ToString());
                item.SubItems.Add(iIndex.ToString());
                item.SubItems.Add(sdwEnrollNumber);
                item.SubItems.Add(sName.Trim().ToUpper());
                item.SubItems.Add(cardNum);

                // Add to UI lists
                lvCard.Items.Add(item);
                listfilter.Items.Add((ListViewItem)item.Clone());

                // Alternating color
                item.ForeColor = (iIndex % 2 == 0) ? Color.Black : Color.Red;

                // Add to DataTable
                dtcard.Rows.Add(iIndex, sdwEnrollNumber, sName, cardNum);
                lblattcount.Refresh();
                lblattcount.Text = "Batch Card Details Total No :" + lvCard.Items.Count.ToString();
                iIndex++;
            }

            lvCard.EndUpdate();

            axCZKEM1.EnableDevice(iMachineNumber, true);
            Cursor = Cursors.Default;

            lblattcount.Text = $"Total Batch Card Rows: {lvCard.Items.Count} | IP Address: {combocardreaderipbox.Text}";

            if (lvCard.Items.Count == 0)
            {
                MessageBox.Show("No Data Found");
            }

            lblState.Text = "Current State: DisConnected";

          
        }


        private void Txtfingertempsearch_TextChanged(object sender, EventArgs e)
        {


        }

    
        private void Txtcardreadersearch_TextChanged(object sender, EventArgs e)
        {

            try
            {
                lvCard.Items.Clear();
                string search = txtcardreadersearch.Text.Trim();

                foreach (ListViewItem item in listfilter.Items)
                {
                    // If search text exists → filter
                    if (search.Length > 0)
                    {
                        string col2 = item.SubItems[2].Text;
                        string col3 = item.SubItems[3].Text;

                        if (col2.IndexOf(search, StringComparison.OrdinalIgnoreCase)>=0 ||
                            col3.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            lvCard.Items.Add((ListViewItem)item.Clone());
                        }
                    }
                    else
                    {
                        // No search → load all
                        lvCard.Items.Add((ListViewItem)item.Clone());
                    }
                }
            }
            catch
            {
                // ignore
            }



        }




        private void LvCard_ItemActivate(object sender, EventArgs e)
        {
            try
            {
                cardemtpy();
                if (lvCard.Items.Count > 0)
                {
                    ListViewItem item1 = new ListViewItem();
                    item1.SubItems.Clear();
                    for (int c = 0; c < lvCard.SelectedItems[0].SubItems.Count; c++)
                    {
                        item1.SubItems.Add(lvCard.SelectedItems[0].SubItems[c].Text);


                    }

                    cbUserId_Card.Text = item1.SubItems[3].Text;
                    txtName.Text = item1.SubItems[4].Text;
                    txtCardnumber.Text = item1.SubItems[5].Text;




                }
            }
            catch (Exception ex)
            {
                // MessageBox.Show("---" + ex.ToString());
            }
        }





  
               

       





        private void Butcarddwnfrmdatabase_Click(object sender, EventArgs e)
        {
            if (dtcard.Rows.Count > 0)
            {
                progressBar1.Minimum = 0;
                progressBar1.Maximum = dtcard.Rows.Count;
                int i = 0, j = 0;
                using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Excel Workbook|*.xlsx", ValidateNames = true })
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                        Microsoft.Office.Interop.Excel.Workbook wb = app.Workbooks.Add(Microsoft.Office.Interop.Excel.XlSheetType.xlWorksheet);
                        Microsoft.Office.Interop.Excel.Worksheet ws = (Microsoft.Office.Interop.Excel.Worksheet)app.ActiveSheet;
                        app.Visible = false; int k = 1;
                        for (k = 1; k < dtcard.Columns.Count; k++)
                        {
                            ws.Cells[1, k] = dtcard.Columns[k].ColumnName;
                        }
                        i = 2; j = 0;
                        ws.Range["A1", "D1"].Interior.Color = Microsoft.Office.Interop.Excel.XlRgbColor.rgbNavy;
                        ws.Range["A1", "D1"].Font.Color = Microsoft.Office.Interop.Excel.XlRgbColor.rgbWhite;

                        int l = 1; k = 1;
                        foreach (ListViewItem item in lvCard.Items)
                        {
                            for (k = 1; k < item.SubItems.Count; k++)
                            {

                                ws.Cells[i, l] = item.SubItems[l].Text;


                                l++;
                            }
                            l = 1;
                            ws.Range["A" + i, "D" + i].Font.Color = Microsoft.Office.Interop.Excel.XlRgbColor.rgbBlack;
                            // ws.Range["A" + i, "D" + i].Interior.Color = Microsoft.Office.Interop.Excel.XlRgbColor.rgbNavy;
                            //ws.Range["G" + i, "G" + i].Font.Color = Microsoft.Office.Interop.Excel.XlRgbColor.rgbWhite;
                            // decimal per = Convert.ToDecimal(100 / GenFun.ToInt(dtcard.Rows.Count)) * (j + 1);
                            // lblprogress1.Text = "" + per.ToString("N0") +" %";

                            decimal per = Convert.ToDecimal(100 / GenFun.ToDecimal(dtcard.Rows.Count)) * (i + 1);
                            lblprogress1.Text = per.ToString("N0") + " %" + "ID Card No:-" + item.SubItems[l].Text;
                            lblprogress1.Refresh();
                            progressBar1.Value = j;
                            i++; j++;

                        }
                        ws.Range["A" + i, "D" + i].Interior.Color = Microsoft.Office.Interop.Excel.XlRgbColor.rgbNavy;

                        ws.Columns.AutoFit();
                        wb.SaveAs(sfd.FileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault, Type.Missing, true, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, (Microsoft.Office.Interop.Excel.XlSaveAsAccessMode)Microsoft.Office.Interop.Excel.XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing);
                        app.Quit();
                        MessageBox.Show("Completed"); progressBar1.Maximum = 0;
                    }
            }
            
        }





     
        private void Listviewchecklistip_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            try
            {
                Class.Users.UserTime = 0;
                if (listViewupload.Items.Count == 0 || e.Item == null)
                {
                    
                    return;
                }

                if (e.Item.Checked)
                    {
                        Cursor = Cursors.WaitCursor;
                        Class.Users.UserTime = 0;

                        try
                        {

                        spl = null; string deviceIp = "";
                        if (Class.Users.HCompcode == "LOPPL")
                        {
                            spl = e.Item.SubItems[1].Text.Trim().Split('/');
                            deviceIp = spl[0].ToString();
                            reply1 = ping.Send(deviceIp, 1000);
                        }
                        else
                        {

                            deviceIp = e.Item.SubItems[1].Text;
                            reply1 = ping.Send(deviceIp, 1000);
                        }
                        

                            if (reply1.Status == IPStatus.Success)
                            {
                                bIsConnected = true;
                                e.Item.SubItems[2].Text = "Connected";

                                // Add to allip
                                ListViewItem it2 = new ListViewItem();
                            it2.SubItems.Add(e.Item.SubItems[1].Text);
                            it2.SubItems.Add(e.Item.SubItems[2].Text);
                                it2.SubItems.Add(e.Item.Checked.ToString());
                                allip.Items.Add(it2);
                            }
                            else
                            {
                                bIsConnected = false;
                                e.Item.SubItems[2].Text = "DisConnected";
                            }
                        }
                        catch (Exception ex)
                        {
                            bIsConnected = false;
                            e.Item.SubItems[2].Text = "Error";
                            MessageBox.Show("Ping failed: " + ex.Message);
                        }

                        Cursor = Cursors.Default;
                    }
                    if(!e.Item.Checked && e.Item.SubItems[2].Text == "Connected")
                    {
                        Cursor = Cursors.WaitCursor;
                        bIsConnected = false;
                        e.Item.SubItems[2].Text = "DisConnected";

                        // Remove from allip
                        for (int c = 0; c < allip.Items.Count; c++)
                        {
                            if (allip.Items[c].SubItems[1].Text == e.Item.SubItems[1].Text)
                            {
                                allip.Items[c].Remove();
                                c--;
                            }
                        }

                        Cursor = Cursors.Default;
                    }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error with item: " + e.Item.Text + "\n" + ex.ToString());
            }

           
        }





        private void Butdownfrmdb_Click(object sender, EventArgs e)
        {

        }
        private void Lvdownall_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            
            Class.Users.UserTime = 0;

            iIndex = Lvdownremove.Items.Count;
            progressBar1.Value = 0;

            if (e.Item.Checked)
            {
                ListViewItem itt = new ListViewItem();
                // Copy all subitems safely
                for (int i = 1; i < e.Item.SubItems.Count; i++)
                {
                    itt.SubItems.Add(e.Item.SubItems[i].Text);
                }

                // Alternating row color
                itt.BackColor = (iIndex % 2 == 0) ? Color.White : Color.WhiteSmoke;
              
                    Lvdownremove.Items.Add(itt);
                lblattcount.Refresh();
                lblattcount.Text = "Remove  IDCards Details Total No :" + Lvdownremove.Items.Count.ToString();
                iIndex++;
            }
 

        }

        private void Lvdownall_ItemActivate(object sender, EventArgs e)
        {

        }

        private void Butremoveall_Click(object sender, EventArgs e)
        {

            Class.Users.UserTime = 0;
            Class.Users.Intimation = "PAYROLL";

            try
            {
                DialogResult result = MessageBox.Show(
                    "Do you want to remove Finger Index from Machine?",
                    "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

                if (result != DialogResult.OK)
                    return;

                if (Lvdownremove.Items.Count == 0)
                {
                    lblattcount.Text = "Please select data from ListView";
                    return;
                }

                Cursor = Cursors.WaitCursor;

                int total = Lvdownremove.Items.Count;
                int removedCount = 0;
                bool anySuccess = false;

                progressBar1.Minimum = 0;
                progressBar1.Maximum = total;
                progressBar1.Value = 0;

                List<string> ipList = new List<string>();

                // Load IPs from listview
                spl = null;
                string deviceIp = "";
                foreach (ListViewItem item in allip2.Items)
                {
                    if (Class.Users.HCompcode == "LOPPL")
                    {
                        spl = item.SubItems[1].Text.Trim().Split('/');
                         deviceIp = spl[0].ToString();
                        ipList.Add(deviceIp);
                    }
                    else
                    {
                        deviceIp = item.SubItems[1].Text;
                        if (!string.IsNullOrWhiteSpace(deviceIp))
                            ipList.Add(deviceIp);
                      
                    }
                }
              

                if (ipList.Count == 0 && !string.IsNullOrWhiteSpace(deviceIp))
                    ipList.Add(deviceIp);

                if (ipList.Count == 0)
                {
                    MessageBox.Show("No IP Address found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                foreach (string ip in ipList)
                {
                    bIsConnected = axCZKEM1.Connect_Net(ip, Convert.ToInt32(txtPort.Text));

                    if (!bIsConnected)
                    {
                        MessageBox.Show("Machine Disconnected: " + ip);
                        continue;
                    }

                    lblState.Text = "Current State: Connected";
                    axCZKEM1.EnableDevice(iMachineNumber, false);

                    for (int i = 0; i < total; i++)
                    {
                        string userId = Lvdownremove.Items[i].SubItems[2].Text;
                        int fingerIndex = Convert.ToInt32("0" + Lvdownremove.Items[i].SubItems[4].Text);

                        bool res = axCZKEM1.SSR_DeleteEnrollData(iMachineNumber, userId, 12);

                        if (res)
                        {
                            anySuccess = true;
                            removedCount++;
                        }
                        lblattcount.Refresh();
                        lblattcount.Text = ip.ToString()+ " Deleteing Under Process ...  USER : " + sUserID + " Count : " + i + " /" + total;
                        decimal per = (decimal)(i + 1) / total * 100;
                        lblprogress1.Text = $"{per:N0}%  |  ID Card No: {userId}";
                        lblprogress1.Refresh();

                        progressBar1.Value = i + 1;
                    }

                    if (axCZKEM1.ClearAdministrators(iMachineNumber))
                    {
                        axCZKEM1.RefreshData(iMachineNumber);

                        bIsConnected = false;
                    }
                    axCZKEM1.EnableDevice(iMachineNumber, true); lblattcount.Text = "";
                }

                Cursor = Cursors.Default;

                if (anySuccess)
                    MessageBox.Show("Data Removed Successfully. Total Count: " + removedCount, "Information");

                // Reset UI
                progressBar1.Value = 0;
                lblprogress1.Text = "";
                Lvdownremove.Items.Clear();
                allip2.Items.Clear();
                lblState.Text = "Current State: Disconnected";
            }
            catch
            {
                Cursor = Cursors.Default;
                MessageBox.Show("Please connect device", "Error");
            }
            finally
            {


                Class.Users.UserTime = 0; Class.Users.Intimation = "PAYROLL";
            }
           

        }


        private void Listremovechecklistip_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
                        try
            {
                Class.Users.UserTime = 0;
                string ip = "";
                if (Lvdownremove.Items.Count == 0 || e.Item == null)
                    return;
                // ---------- Item Checked ----------
                if (e.Item.Checked)
                {
                    Cursor = Cursors.WaitCursor;
                    //using (Ping ping = new Ping())
                    //{
                    //    PingReply reply = ping.Send(ip, 1000);
                    //    isConnected = reply.Status == IPStatus.Success;
                    //}
                    bool isConnected = false;
                    if (Class.Users.HCompcode == "LOPPL")
                    {
                        spl = e.Item.SubItems[1].Text.Trim().Split('/');
                        ip = spl[0].ToString();
                        reply1 = ping.Send(ip, 1000);
                        isConnected = reply1.Status == IPStatus.Success;
                    }
                    else
                    {
                        ip = e.Item.SubItems[1].Text;
                        reply1 = ping.Send(ip, 1000);
                        isConnected = reply1.Status == IPStatus.Success;
                    }
                    

               

                    e.Item.SubItems[2].Text = isConnected ? "Connected" : "DisConnected";
                    bIsConnected = isConnected;

                    if (isConnected)
                    {
                        // Create item with first column text
                        ListViewItem itt = new ListViewItem();
                        itt.SubItems.Add(e.Item.SubItems[1].Text);
                        itt.SubItems.Add(e.Item.SubItems[2].Text);
                        itt.SubItems.Add(e.Item.Checked.ToString());

                        allip2.Items.Add(itt);
                    }
                    else
                    {
                        //MessageBox.Show($"This IP : {ip} Not a BioMetric Machine.");
                    }

                    Cursor = Cursors.Default;
                }

                // ---------- Item Unchecked ----------
                else if (e.Item.SubItems[2].Text == "Connected")
                {
                    Cursor = Cursors.WaitCursor;

                    e.Item.SubItems[2].Text = "DisConnected";
                    bIsConnected = false;

                    // Remove matching IP from allip2
                    for (int i = allip2.Items.Count - 1; i >= 0; i--)
                    {
                        if (allip2.Items[i].SubItems[1].Text == e.Item.SubItems[1].Text)
                        {
                            allip2.Items[i].Remove();
                        }
                    }

                    Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error processing item : {ex.Message}");
            }

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

            Cursor = Cursors.WaitCursor;
            if (comboremoveip.Text == "")
            {
                MessageBox.Show("pls Select IPAddress", "Error"); Cursor = Cursors.Default;
                return;
            }
            bIsConnected = axCZKEM1.Connect_Net(comboremoveip.Text, Convert.ToInt32(txtPort.Text));
            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first!", "Error"); Cursor = Cursors.Default;
                return;
            }
            if (bIsConnected == true)
            {
                axCZKEM1.RestartDevice(MyCount);

                MyCount = 1;

            }
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

            try
            {
                if (Lvdownremove.Items.Count == 0)
                {
                    MessageBox.Show("Please upload data from Master IP", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var confirmation = MessageBox.Show("Do you want to delete the selected record(s)?",
                                                   "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (confirmation != DialogResult.Yes)
                    return;

                // Collect items to remove to avoid modifying collection while iterating
                var itemsToRemove = Lvdownremove.SelectedItems.Cast<ListViewItem>().ToList();

                if (itemsToRemove.Count == 0)
                {
                    MessageBox.Show("No record selected for deletion.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                foreach (var item in itemsToRemove)
                {
                    MessageBox.Show($"UserID: {item.SubItems[1].Text}    Name: {item.SubItems[2].Text}",
                                    "Deleting Record", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    Lvdownremove.Items.Remove(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while deleting record: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        private void Checkallrows_CheckedChanged(object sender, EventArgs e)
        {

            try
            {
                listViewupload.Items.Clear();
                allip.Items.Clear();
                if (checkallrows.Checked)
                {
                    int i = 0;
                    allip.Items.Clear();
                    foreach (ListViewItem src in LvDownload.Items)
                    {
                        // Clone original item (copies all subitems automatically)
                        if (src.SubItems[3].Text != "")
                        {
                            ListViewItem newItem = (ListViewItem)src.Clone();

                        // Apply row color

                       
                            newItem.BackColor = (i % 2 == 0) ? Class.Users.Color1 : Class.Users.Color2;
                            listViewupload.Items.Add(newItem);
                            lblattcount.Refresh();
                            lblattcount.Text ="Transfer IDCard Total NO :" +listViewupload.Items.Count.ToString();
                            i++;
                        }
                    }
                }
                else
                {
                    // Simply uncheck → clear selection and clear upload list
                    foreach (ListViewItem item in LvDownload.Items)
                        item.Selected = false;

                    listViewupload.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }


        private void Checkremoveall_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Lvdownremove.Items.Clear();
               
                if (checkremoveall.Checked)
                {
                    int i = 0;
                    allip2.Items.Clear();
                    foreach (ListViewItem item in Lvdownall.Items)
                    {
                        if (item.SubItems[3].Text != "" && Class.Users.IPADDRESS == "192.168.101.15")
                        {
                            ListViewItem item1 = (ListViewItem)item.Clone();
                            item1.BackColor = (i % 2 == 0) ? Class.Users.Color1 : Class.Users.Color2;
                            Lvdownremove.Items.Add(item1);
                            lblattcount.Refresh();
                            lblattcount.Text = "Remove  IDCards Details Total No :" + Lvdownremove.Items.Count.ToString();
                            i++;
                        }
                        if (item.SubItems[3].Text == "" && Class.Users.IPADDRESS != "192.168.101.15")
                        {                          
                          
                            ListViewItem item1 = (ListViewItem)item.Clone();
                            item1.BackColor = (i % 2 == 0) ? Class.Users.Color1 : Class.Users.Color2;
                            Lvdownremove.Items.Add(item1);
                            lblattcount.Refresh();
                            lblattcount.Text = "Remove  IDCards Details Total No :" + Lvdownremove.Items.Count.ToString();
                            i++;
                        }
                    }
                }
                else
                {
                    // Unselect all
                    foreach (ListViewItem item in Lvdownall.Items)
                    {
                        item.Selected = false;
                    }

                    Lvdownremove.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        private void Txtremovelog_TextChanged(object sender, EventArgs e)
        {

            try
            {
                Lvdownall.Items.Clear();
                int index = 0;
                string search = txtremovelog.Text.Trim();

                foreach (ListViewItem src in listfilter.Items)
                {
                    bool match = true;

                    if (search.Length > 0)
                    {
                        // Search only in column 2 & 3
                        match =
                            (src.SubItems[2].Text.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0) ||
                            (src.SubItems[3].Text.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0);
                    }

                    if (match)
                    {
                        ListViewItem newItem = (ListViewItem)src.Clone();

                        // Alternating row colors
                        newItem.BackColor = (index % 2 == 0) ? Color.White : Color.WhiteSmoke;

                        Lvdownall.Items.Add(newItem);
                        index++;
                    }
                }
            }
            catch { }


           

        }

        private void Listviewattdown_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            try
            {
                Class.Users.UserTime = 0;
                
                if (e.Item.Checked)
                {
                   
                       
                        // PingReply reply = ping.Send(e.Item.SubItems[2].Text, 1000);
                        if (Class.Users.HCompcode == "LOPPL")
                        {
                            spl = e.Item.SubItems[2].Text.Trim().Split('/');
                            reply1 = ping.Send(spl[0], 1000);
                        }
                        else
                        {
                            reply1 = ping.Send(e.Item.SubItems[2].Text, 1000);
                        }
                        bool connected = reply1.Status == IPStatus.Success;

                        e.Item.SubItems[3].Text = connected ? "Connected" : "DisConnected";
                        e.Item.SubItems[4].Text = connected ? "" : reply1.Status.ToString();
                        e.Item.SubItems[5].Text = connected ? Class.Users.Error : "Network Unavailable";

                        if (e.Item.SubItems[3].Text == "Connected")
                        {
                            // Create new ListViewItem for allip1
                            ListViewItem it = new ListViewItem();
                            it.SubItems.Add(e.Item.SubItems[1].Text);
                            it.SubItems.Add(e.Item.SubItems[2].Text);
                            it.SubItems.Add(e.Item.SubItems[3].Text);
                            it.SubItems.Add(e.Item.SubItems[4].Text);
                            it.SubItems.Add(e.Item.SubItems[5].Text);
                            it.SubItems.Add(e.Item.Checked.ToString());

                            allip1.Items.Add(it);

                            lblattcount.Text = $"This IP: {e.Item.SubItems[2].Text} Connected.";
                        }
                        else
                        {
                            lblattcount.Text = $"This IP: {e.Item.SubItems[2].Text} DisConnected.";
                        }
                    
                }
                else if (!e.Item.Checked && e.Item.SubItems[3].Text == "Connected")
                {
                   
                    bIsConnected = false;
                    e.Item.SubItems[3].Text = "DisConnected";
                    e.Item.SubItems[4].Text = "Cancelled";
                    e.Item.SubItems[5].Text = "Cancelled";
                   
                    // Remove from allip1
                    for (int c = allip1.Items.Count - 1; c >= 0; c--)
                    {
                        if (Class.Users.HCompcode == "LOPPL")
                        {
                            if (allip1.Items[c].SubItems[2].Text == spl[0])
                            {
                                allip1.Items[c].Remove();
                            }
                        }
                        else
                        {
                            if (allip1.Items[c].SubItems[2].Text == e.Item.SubItems[2].Text)
                            {
                                allip1.Items[c].Remove();
                            }
                        }
                    }

                    lblattcount.Text = $"This IP: {e.Item.SubItems[2].Text} DisConnected.";
                }

                listviewattdown.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error checking IP: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }



        private void ComboMasterIp_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboMasterIp.SelectedIndex >= 1)
            {
                // Freeze UI updates for better performance
                Lvdownall.BeginUpdate();
                Lvdownremove.BeginUpdate();
                listviewchecklistip.BeginUpdate();
                listviewchecklistip1.BeginUpdate();
                listviewchecklistip2.BeginUpdate();
                allip.BeginUpdate();
                allip1.BeginUpdate();txtfactransferesearch.Text = "";LvDownload2.Items.Clear();
                allip2.BeginUpdate();

                try
                {
                    progressBar1.Value = 0;

                    lblattcount.Text = "";
                    lblprogress1.Text = "";

                    // Clear all the lists in a clean way
                    Lvdownremove.Items.Clear();
                    Lvdownall.Items.Clear();
                    listviewchecklistip.Items.Clear();
                    listviewchecklistip1.Items.Clear();
                    listviewchecklistip2.Items.Clear();
                    listremovechecklistip.Items.Clear();
                    allip.Items.Clear();
                    allip1.Items.Clear();
                    allip2.Items.Clear(); btnfaceDownload.Text = "Face Download ??"; btnfaceDownload.Refresh();
                     bIsConnected = false; btncarddownload.Text = "Card Download ??"; btncarddownload.Refresh();
                    Class.Users.UserTime = 0; btnConnect.Text = "Finger Download ??"; btnConnect.Refresh();
                    lblState.Text = "Current State: DisConnected";

                }
                finally
                {
                    // Resume UI updates
                    Lvdownall.EndUpdate();
                    Lvdownremove.EndUpdate();
                    listviewchecklistip.EndUpdate();
                    listviewchecklistip1.EndUpdate();
                    listviewchecklistip2.EndUpdate();
                    allip.EndUpdate();
                    allip1.EndUpdate();
                    allip2.EndUpdate();
                }
            }


        }

        private void Comboremoveip_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboremoveip.Text != "")
            {
                Ping ping = new Ping();
                PingReply reply = ping.Send(comboremoveip.Text, 1000);
                if (reply.Status.ToString() == "Success")
                { }
                else {
                    lbRTShow.Refresh();
                    lbRTShow.Text = "Invalid NetWork" + comboremoveip.Text;

                }
            }
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
            try
            {
                string[] st = new string[lvCard.Columns.Count];
                DirectoryInfo di = new DirectoryInfo(@"c:\Pinnacle");
                if (di.Exists == false)
                    di.Create();
                StreamWriter sw = new StreamWriter(@"c:\Pinnacle\'" + Class.Users.HCompcode + "'TodayAttLogs.xls", false);
                sw.AutoFlush = true;
                for (int col = 0; col < lvCard.Columns.Count; col++)
                {
                    sw.Write("\t" + lvCard.Columns[col].Text.ToString());
                }

                int rowIndex = 1;
                int row = 0;
                string st1 = "";
                for (row = 0; row < lvCard.Items.Count; row++)
                {
                    if (rowIndex <= lvCard.Items.Count)

                        rowIndex++;
                    if (rowIndex == 2)
                    {
                        st1 = "\n";
                    }
                    else
                    {
                        st1 = "";
                    }
                    for (int col = 0; col < lvCard.Columns.Count; col++)
                    {
                        st1 = st1 + "\t" + "" + lvCard.Items[row].SubItems[col].Text.ToString();
                    }
                    sw.WriteLine(st1);
                }
                sw.Close();
                FileInfo fil = new FileInfo(@"c:\Pinnacle\'" + Class.Users.HCompcode + "'TodayAttLogs.xls");
                if (fil.Exists == true)
                    MessageBox.Show("DownLoad Completed. \n Folder-Name is :c:\\Pinnacle\\'" + Class.Users.HCompcode + "'TodayAttLogs.xls", "Export to Excel", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
            }
        }

        private void ListView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Btncarddownload_Click(object sender, EventArgs e)
        {
            dtexcel.Rows.Clear();
            if (dtexcel.Columns.Count <= 0)
            {
                dtexcel.Columns.Clear();
                dtexcel.Columns.Add("IPADDRESS");
                dtexcel.Columns.Add("DETAILS");
                dtexcel.Columns.Add("DATETIME");
            }
            try
            {
                // Basic validation
                if (string.IsNullOrWhiteSpace(comboMasterIp.Text) || string.IsNullOrWhiteSpace(txtPort.Text))
                {
                    MessageBox.Show("IP and Port cannot be null", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                Cursor = Cursors.WaitCursor; bIsConnected = false;

                SecondtabControl2.SelectedTab = tab2card;
                listViewupload1.Items.Clear();
                // Reset UI state

                allip.Items.Clear();
                allip1.Items.Clear();
                checkallrows.Checked = false;
                checkremoveall.Checked = false;
                listremovechecklistip.Items.Clear();
                checkCard.Checked = false;
                lblprogress1.Text = "";
                listviewchecklistip.Items.Clear();
                Lvdownremove.Items.Clear();
                LvDownload1.Items.Clear();
                Lvdownall.Items.Clear();

                listfilter.Items.Clear();
                lblattcount.Text = "";
                bIsConnected = false;
                // Toggle disconnect if already connected
                if (btncarddownload.Text == "Card DisConnect")
                {
                    try { axCZKEM1.Disconnect(); } catch { /* ignore */ }
                    bIsConnected = false;
                    btncarddownload.Text = "Card Download ??";
                    lblState.Text = "Current State:DisConnected";
                    Cursor = Cursors.Default;
                    return;
                }

                // Try connect
                axCZKEM1.PullMode = 1;
                spl = comboMasterIp.Text.Trim().Split('/');
                if (Class.Users.HCompcode == "LOPPL")
                {
                    bIsConnected = axCZKEM1.Connect_Net(spl[0], Convert.ToInt32(txtPort.Text));
                }
                else
                {
                    bIsConnected = axCZKEM1.Connect_Net(comboMasterIp.Text, Convert.ToInt32(txtPort.Text));
                }
                if (!bIsConnected)
                {
                    int lastErr = 0;
                    try { axCZKEM1.GetLastError(ref lastErr); } catch { }
                    MessageBox.Show("Unable to connect the device" + (lastErr > 0 ? $", ErrorCode={lastErr}" : ""), "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Cursor = Cursors.Default;
                    return;
                }

                // Connected - prepare device and UI
                btncarddownload.Text = "Card DisConnect";
                lblState.Text = "Current State:Connected";
                allip.Items.Add(comboMasterIp.Text.Trim());

                axCZKEM1.EnableDevice(iMachineNumber, false);
                axCZKEM1.ReadAllUserID(iMachineNumber);
                axCZKEM1.IsTFTMachine(iMachineNumber);
                axCZKEM1.ReadAllTemplate(iMachineNumber);

                // Prepare to collect data
                int r = 1; int s = 0;
                string macip = comboMasterIp.Text.Trim();

                LvDownload1.BeginUpdate();
                Lvdownall.BeginUpdate();
                string sql = "";

                // Iterate users from device
                while (axCZKEM1.SSR_GetAllUserInfo(iMachineNumber,
                          out sdwEnrollNumber, out sName, out sPassword,
                          out iPrivilege, out bEnabled))
                {
                    // Get card number (if any)
                    axCZKEM1.GetStrCardNumber(out sCardnumber);
                    string card = (sCardnumber?.Length >= 5) ? sCardnumber : "";

                    // get finger template (index 2 used previously)
                    //idwFingerIndex = 2;
                    sTmpData = "";
                    //if (axCZKEM1.GetUserTmpExStr(iMachineNumber, sdwEnrollNumber, idwFingerIndex, out iFlag, out sTmpData, out iTmpLength))
                    //{
                    //    // sTmpData populated
                    //}

                    // If no valid card skip this user
                    if (string.IsNullOrEmpty(card))
                        continue;

                    // Lookup employee name (only if available)
                    string empName = "";
                    if (empName == "")
                    {
                        //sql = "";
                        //sql = $"SELECT C.FNAME || '-' || E.MNNAME1 AS EMPNAME FROM GTCOMPMAST B JOIN HREMPLOYMAST C ON C.COMPCODE = B.GTCOMPMASTID JOIN HREMPLOYDETAILS D ON C.HREMPLOYMASTID = D.HREMPLOYMASTID JOIN GTDEPTDESGMAST E ON E.GTDEPTDESGMASTID = D.DEPTNAME WHERE D.IDACTIVE='YES' AND  D.MIDCARD='{sdwEnrollNumber.Trim()}' AND B.COMPCODE='" + Class.Users.HCompcode + "'   ORDER BY C.IDCARDNO DESC";
                        //DataTable dt = Utility.ExecuteSelectQuery(sql, "HREMPLOYDETAILS").Tables[0];
                        DataTable dt = dev.FindName(sdwEnrollNumber.Trim(), Class.Users.HCompcode);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            empName = dt.Rows[0]["EMPNAME"].ToString();
                        }
                        else
                        {
                            DataTable dt1;
                            //sql = "";
                            //sql = $"SELECT C.FNAME || '-' || E.MNNAME1 AS EMPNAME FROM GTCOMPMAST B JOIN HREMPLOYMAST C ON C.COMPCODE = B.GTCOMPMASTID JOIN HREMPLOYDETAILS D ON C.HREMPLOYMASTID = D.HREMPLOYMASTID JOIN GTDEPTDESGMAST E ON E.GTDEPTDESGMASTID = D.DEPTNAME WHERE D.IDACTIVE='YES' AND  D.MIDCARD='{sdwEnrollNumber.Trim()}'  ORDER BY C.IDCARDNO DESC";
                            //DataTable dt1 = Utility.ExecuteSelectQuery(sql, "HREMPLOYDETAILS").Tables[0];
                             dt1 = dev.FindName(sdwEnrollNumber.Trim(), Class.Users.HCompcode);
                            if (dt1 != null && dt1.Rows.Count > 0)
                            {
                                empName = dt1.Rows[0]["EMPNAME"].ToString();
                            }
                            else
                            {
                                //sql = "";
                                //sql = $"SELECT C.FNAME || '-' || E.MNNAME1 AS EMPNAME FROM GTCOMPMAST B JOIN HREMPLOYMAST C ON C.COMPCODE = B.GTCOMPMASTID JOIN HREMPLOYDETAILS D ON C.HREMPLOYMASTID = D.HREMPLOYMASTID JOIN GTDEPTDESGMAST E ON E.GTDEPTDESGMASTID = D.DEPTNAME WHERE D.IDACTIVE='NO' AND  D.MIDCARD='{sdwEnrollNumber.Trim()}' AND B.COMPCODE='" + Class.Users.HCompcode + "'   ORDER BY C.IDCARDNO DESC";
                                //dt1 = Utility.ExecuteSelectQuery(sql, "HREMPLOYDETAILS").Tables[0];
                                 dt1 = dev.FindName(sdwEnrollNumber.Trim(), Class.Users.HCompcode);
                                if (dt1 != null && dt1.Rows.Count > 0)
                                {
                                    empName = dt1.Rows[0]["EMPNAME"].ToString();
                                }
                                else
                                {
                                    //sql = "";
                                    //sql = $"SELECT C.FNAME || '-' || E.MNNAME1 AS EMPNAME FROM GTCOMPMAST B JOIN HREMPLOYMAST C ON C.COMPCODE = B.GTCOMPMASTID JOIN HREMPLOYDETAILS D ON C.HREMPLOYMASTID = D.HREMPLOYMASTID JOIN GTDEPTDESGMAST E ON E.GTDEPTDESGMASTID = D.DEPTNAME WHERE D.IDACTIVE='NO' AND  D.MIDCARD='{sdwEnrollNumber.Trim()}'   ORDER BY C.IDCARDNO DESC";
                                    //dt1 = Utility.ExecuteSelectQuery(sql, "HREMPLOYDETAILS").Tables[0];
                                     dt1 = dev.FindName(sdwEnrollNumber.Trim(), Class.Users.HCompcode);
                                    if (dt1 != null && dt1.Rows.Count > 0)
                                    {
                                        empName = dt1.Rows[0]["EMPNAME"].ToString();
                                    }
                                }
                                dtexcel.Rows.Add(comboMasterIp.Text, sdwEnrollNumber.ToString(), empName + " - BatchCard No: " + sCardnumber);
                                empName = "";
                            }

                        }
                    }


                    // Build a single row once
                    string[] row = new string[] { "", r.ToString(), sdwEnrollNumber, empName.ToUpper(), idwFingerIndex.ToString(), sTmpData, "", card, iPrivilege.ToString(), sPassword, (bEnabled ? "True" : "False"), iFlag.ToString(), macip };

                    // Add items to listviews
                    var item1 = new ListViewItem(row);
                    var item2 = new ListViewItem(row);


                    Color bg = (r % 2 == 0) ? Color.White : Color.WhiteSmoke;
                    item1.BackColor = item2.BackColor = bg;

                    LvDownload1.Items.Add(item1);
                    Lvdownall.Items.Add(item2);


                    // For filter list keep a clone (so separate instances)
                    listfilter.Items.Add((ListViewItem)item1.Clone());

                    // Update progress label
                    lblprogress1.Text = $"{r} : ID Card No: {sdwEnrollNumber}";
                    lblprogress1.Refresh();
                    r++;
                }
                LvDownload1.EndUpdate();
                Lvdownall.EndUpdate();
                axCZKEM1.EnableDevice(iMachineNumber, true);
                lblattcount.Text = $"Total Employee Card Rows Count : {LvDownload1.Items.Count}  IP : {comboMasterIp.Text}";
                allip1.Items.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("FrameWork Invalid. Install Zkemkeeper.dll\n\n" + ex.Message, "Error");
            }
            finally
            {
                if (axCZKEM1.ClearAdministrators(iMachineNumber))
                {
                    axCZKEM1.RefreshData(iMachineNumber);
                    bIsConnected = false;
                }
                combo_ToIPload();
                Cursor = Cursors.Default;
               
                btncarddownload.Text = "Card Download ??";
                lblState.Text = "Current State: DisConnected";
                if (dtexcel.Rows.Count > 0)
                {

                    StreamWriter swExtLogFile = new StreamWriter(combinepath + "/IDCard_Remove_Details.txt", true);
                    int ii = 1;
                    swExtLogFile.Write(Environment.NewLine);
                    swExtLogFile.WriteLine(Class.Users.HCompcode + " ACTIVE - 'NO' Employee  - Details . Pls Remove From Machine  :   ");
                    swExtLogFile.Write("============================");
                    swExtLogFile.Write(Environment.NewLine);

                    foreach (DataRow rows in dtexcel.Rows)
                    {
                        swExtLogFile.Write(rows[0].ToString() + "      " + rows[1].ToString() + "     " + rows[2].ToString() + "  ");
                        swExtLogFile.Write(Environment.NewLine);
                        ii++;
                    }

                    swExtLogFile.Write("Total Record Count   :" + ii.ToString() + "  ********END OF DATA*********");
                    swExtLogFile.Flush();
                    swExtLogFile.Close();
                }

            }

            lblprogress1.Text = "";
           

        }

        private void BtnfaceDownload_Click(object sender, EventArgs e)
        {
            int totalCount = 0;
            int faceCount = 0;
            try
            {
                dtexcel.Rows.Clear(); Class.Users.UserTime = 0;Cursor = Cursors.WaitCursor;
                if (dtexcel.Columns.Count <= 0)
                {
                    dtexcel.Columns.Clear();
                    dtexcel.Columns.Add("IPADDRESS");
                    dtexcel.Columns.Add("DETAILS");
                    dtexcel.Columns.Add("DATETIME");
                }
                

                if (string.IsNullOrWhiteSpace(comboMasterIp.Text) ||
                    string.IsNullOrWhiteSpace(txtPort.Text))
                {
                    MessageBox.Show("IP and Port cannot be null",
                                    "Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                    return;
                }

                // ---------- UI Reset ----------
                LvDownload2.Items.Clear();
                Lvdownall.Items.Clear();
                listfilter.Items.Clear();
                listremovechecklistip.Items.Clear();

                SecondtabControl2.SelectTab(tab3face);
                lblprogress1.Text = "";
                lblattcount.Text = "";
                checkallrows.Checked = false;
                checkremoveall.Checked = false;

                Cursor = Cursors.WaitCursor;
                Class.Users.UserTime = 0;

                // ---------- Disconnect ----------
                if (btnfaceDownload.Text == "Face DisConnect")
                {
                    axCZKEM1.Disconnect();
                    btnfaceDownload.Text = "Face Download ??";
                    lblState.Text = "Current State:DisConnected";
                    return;
                }

                // ---------- Connect ----------
                axCZKEM1.PullMode = 1;
                spl = comboMasterIp.Text.Trim().Split('/');
                if (Class.Users.HCompcode == "LOPPL")
                {
                    bIsConnected = axCZKEM1.Connect_Net(spl[0], Convert.ToInt32(txtPort.Text));
                }
                else
                {
                    bIsConnected = axCZKEM1.Connect_Net(comboMasterIp.Text, Convert.ToInt32(txtPort.Text));
                }

                if (!bIsConnected)
                {
                    MessageBox.Show("Unable to connect device",
                                    "Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                    return;
                }

                btnfaceDownload.Text = "Face DisConnect";
                lblState.Text = "Current State:Connected";

                axCZKEM1.EnableDevice(iMachineNumber, false);

                // ---------- Read Device Data ----------
                axCZKEM1.ReadAllUserID(iMachineNumber);
                axCZKEM1.ReadAllTemplate(iMachineNumber);



                // ---------- Cache Employee Names ----------

                                
                while (axCZKEM1.SSR_GetAllUserInfo(iMachineNumber, out sUserID, out sName, out sPassword, out iPrivilege, out bEnabled))
                {

                    totalCount++;
                    lblattcount.Text = $"ID: {sUserID}  Count: {totalCount}  Face Count: {faceCount}";
                    lblattcount.Refresh();

                    sTmpData = "";
                    if (axCZKEM1.GetUserFaceStr(iMachineNumber, sUserID, iFaceIndex, ref sTmpData, ref iLength))
                    {

                        faceCount++;
                        axCZKEM1.GetStrCardNumber(out sCardnumber);
                        string[] row = { "", faceCount.ToString(), sUserID, sName, idwFingerIndex.ToString(), "", sTmpData, sCardnumber, iPrivilege.ToString(), sPassword, bEnabled ? "True" : "False", iFlag.ToString(), comboMasterIp.Text };
                        ListViewItem itemMain = new ListViewItem(row);
                        ListViewItem itemremove = new ListViewItem(row);
                        LvDownload.Items.Add(itemremove);
                        LvDownload2.Items.Add(itemMain);
                        listfilter.Items.Add((ListViewItem)itemMain.Clone());

                    }
                    else
                    {
                        dtexcel.Rows.Add(comboMasterIp.Text, sUserID.ToString() + "   :  Name :" + sName, " - Index: " + iFaceIndex);

                    }
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                listviewchecklistip2.Items.Clear();
             
                lblattcount.Text =
                       $"ID: {sUserID}  Count: {totalCount}  Face Count: {faceCount}";
                lblattcount.Refresh();
                combo_ToIPload();
                Cursor = Cursors.Default;
                if (dtexcel.Rows.Count > 0)
                {

                    StreamWriter swExtLogFile = new StreamWriter(combinepath + "/Face_Details.txt", true);
                    int ii = 1;
                    swExtLogFile.Write(Environment.NewLine);
                    swExtLogFile.WriteLine(Class.Users.HCompcode + " Face  Unable to find Employee  - Details . Pls Upload To Machine  :   ");
                    swExtLogFile.Write("============================");
                    swExtLogFile.Write(Environment.NewLine);

                    foreach (DataRow rows in dtexcel.Rows)
                    {

                        swExtLogFile.Write(rows[0].ToString() + "      " + rows[1].ToString() + "     " + rows[2].ToString() + "  ");
                        swExtLogFile.Write(Environment.NewLine);
                        ii++;
                    }

                    swExtLogFile.Write("Total Record Count   :" + ii.ToString() + "  ********END OF DATA*********");
                    swExtLogFile.Flush();
                    swExtLogFile.Close();
                }
                lblprogress1.Text = "";

                if (axCZKEM1.ClearAdministrators(iMachineNumber))
                {
                    axCZKEM1.RefreshData(iMachineNumber);

                    bIsConnected = false;
                }
            }


            

        }

        private void CheckCard_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                listViewupload1.Items.Clear();
                int i = 1;
                if (checkCard.Checked)
                {
                    allip.Items.Clear();
                    foreach (ListViewItem src in LvDownload1.Items)
                    {
                        if (src.SubItems[3].Text != "")
                        {
                            ListViewItem newItem = (ListViewItem)src.Clone();

                            // Apply row color
                            newItem.BackColor = (i % 2 == 0) ? Class.Users.Color1 : Class.Users.Color2;


                            listViewupload1.Items.Add(newItem);

                            lblattcount.Refresh();
                            lblattcount.Text = "Transfer Card & IDCards Total No :" + listViewupload1.Items.Count.ToString();
                            i++;
                        }
                    }
                    //foreach (ListViewItem srcItem in LvDownload1.Items)
                    //{
                    //    ListViewItem newItem = new ListViewItem();

                    //    // Copy all subitems except 0th column
                    //    for (int c = 1; c < srcItem.SubItems.Count; c++)
                    //    {
                    //        newItem.SubItems.Add(srcItem.SubItems[c].Text);
                    //    }

                    //    listViewupload1.Items.Add(newItem);
                    //}
                }
                else
                {
                    // Uncheck mode → No copy
                    foreach (ListViewItem item in LvDownload1.Items)
                    {
                        item.Selected = false;
                    }
                    listViewupload1.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            //try
            //{
            //    int i = 0;
            //    if (checkCard.Checked == true)
            //    {
            //        listViewupload1.Items.Clear();
            //        foreach (ListViewItem item in LvDownload1.Items)
            //        {
            //            item.Selected = true;



            //            ListViewItem item1 = new ListViewItem();
            //            for (int c = 1; c < LvDownload1.SelectedItems[i].SubItems.Count; c++)
            //            {
            //                item1.SubItems.Add(LvDownload1.SelectedItems[i].SubItems[c].Text);

            //            }

            //            listViewupload1.Items.Add(item1);
            //            i++;
            //        }
            //    }
            //    else
            //    {
            //        foreach (ListViewItem item in LvDownload1.Items)
            //        {
            //            item.Selected = false;

            //        }
            //        listViewupload1.Items.Clear();


            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}
        }

        private void Checkface_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                listViewupload2.Items.Clear();
                allip.Items.Clear();
                if (checkface.Checked)
                {
                    int i = 0;
                    allip.Items.Clear();
                    foreach (ListViewItem src in LvDownload2.Items)
                    {
                        // Clone original item (copies all subitems automatically)
                        if (src.SubItems[3].Text != "")
                        {
                            ListViewItem newItem = (ListViewItem)src.Clone();

                            // Apply row color


                            newItem.BackColor = (i % 2 == 0) ? Class.Users.Color1 : Class.Users.Color2;
                            listViewupload2.Items.Add(newItem);
                            lblattcount.Refresh();
                            lblattcount.Text = "Transfer IDCard Total NO :" + listViewupload2.Items.Count.ToString();
                            i++;
                        }
                    }
                }
                else
                {
                    // Simply uncheck → clear selection and clear upload list
                    foreach (ListViewItem item in LvDownload2.Items)
                        item.Selected = false;

                    listViewupload2.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            //try
            //{
            //    listViewupload2.Items.Clear();

            //    if (checkface.Checked)
            //    {
            //        int i = 0;

            //        foreach (ListViewItem src in LvDownload2.Items)
            //        {
            //            // Clone original item (copies all subitems automatically)
            //            if (src.SubItems[3].Text != "")
            //            {
            //                ListViewItem newItem = (ListViewItem)src.Clone();

            //                // Apply row color


            //                newItem.BackColor = (i % 2 == 0) ? Class.Users.Color1 : Class.Users.Color2;
            //                listViewupload2.Items.Add(newItem);
            //                lblattcount.Refresh();
            //                lblattcount.Text = "Transfer IDCard Total NO :" + listViewupload.Items.Count.ToString();
            //                i++;
            //            }
            //        }
            //    }
            //    else
            //    {
            //        // Simply uncheck → clear selection and clear upload list
            //        foreach (ListViewItem item in LvDownload2.Items)
            //            item.Selected = false;

            //        listViewupload2.Items.Clear();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}


            //try
            //{
            //    listViewupload2.Items.Clear();

            //    if (!checkface.Checked)
            //        return;

            //    foreach (ListViewItem srcItem in LvDownload2.Items)
            //    {
            //        // Create new item with first column
            //        ListViewItem newItem = new ListViewItem();

            //        // Copy remaining columns
            //        for (int c = 1; c < srcItem.SubItems.Count; c++)
            //        {
            //            newItem.SubItems.Add(srcItem.SubItems[c].Text);
            //        }

            //        listViewupload2.Items.Add(newItem);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}

        }
        private void LvDownload1_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            
            try
            {
               
                if (e.Item.Checked)
                {
                    progressBar1.Value = 0;
                    allip.Items.Clear();

                    // Create new item with FIRST COLUMN text
                    ListViewItem itt = new ListViewItem((listViewupload1.Items.Count + 1).ToString());

                    // Now add remaining columns
                    for (int s = 1; s <= 12; s++)
                    {
                        itt.SubItems.Add(e.Item.SubItems[s].Text);
                    }

                    // Alternating row color
                    if (listViewupload1.Items.Count % 2 == 0)
                        itt.BackColor = Color.White;
                    else
                        itt.BackColor = Color.WhiteSmoke;

                    // Add item
                    if (itt.SubItems[3].Text != "")
                    {
                        listViewupload1.Items.Add(itt);
                    }

                    // Update label
                    lblattcount.Text =
                        "Total Employee Finger Rows Count : " +
                        listViewupload1.Items.Count +
                        " and IP Address : " +
                        comboMasterIp.Text;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

           
        }
        private void LvDownload1_ItemActivate(object sender, EventArgs e)
        {
           
        }

        private void LvDownload2_ItemActivate(object sender, EventArgs e)
        {
           
        }

        private void Butcardtransfer_Click(object sender, EventArgs e)
        {
            try
            {
                txtcardtransfer.Text = "";
                lblprogress1.Text = "";
               
                if (listViewupload1.Items.Count == 0)
                {
                    MessageBox.Show("Transfer Listview data not found. ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                DialogResult result = MessageBox.Show("Do you want to export Card Index?", "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (result != DialogResult.OK) return;

                Cursor = Cursors.WaitCursor;
                if (allip.Items.Count > 0)
                {
                    for (int j = 0; j < allip.Items.Count; j++)
                    {
                        string deviceIp = "";
                        if (Class.Users.HCompcode == "LOPPL")
                        {
                            spl = allip.Items[j].SubItems[1].Text.Trim().Split('/');
                            deviceIp = spl.ToString();
                            //reply1 = ping.Send(deviceIp, 1000);
                            //isConnected = reply1.Status == IPStatus.Success;
                        }
                        else
                        {
                            deviceIp = allip.Items[j].SubItems[1].Text;
                           // reply1 = ping.Send(deviceIp, 1000);
                            //isConnected = reply1.Status == IPStatus.Success;
                        }


       

                        if (deviceIp.Length <= 10)
                        {
                            MessageBox.Show("Invalid IP : " + deviceIp);
                            continue;
                        }

                        lblattcount.Text = "";
                        lblprogress1.Text = "";

                        bool connected = axCZKEM1.Connect_Net(deviceIp, Convert.ToInt32(txtPort.Text));
                        if (!connected)
                        {
                            MessageBox.Show("Machine Disconnected: " + deviceIp);
                            continue;
                        }

                        lblState.Text = "Current State: Connected";

                        axCZKEM1.EnableDevice(iMachineNumber, false);

                        int total = listViewupload1.Items.Count;
                        progressBar1.Minimum = 0;
                        progressBar1.Maximum = total;

                        for (int i = 0; i < total; i++)
                        {
                            ListViewItem row = listViewupload1.Items[i];

                            sdwEnrollNumber = row.SubItems[2].Text;
                            sName = row.SubItems[3].Text.Trim();
                            int fingerValue = Convert.ToInt32("0" + row.SubItems[7].Text);

                            // odd → 1, even → 2
                            idwFingerIndex = (fingerValue % 2 == 1) ? 1 : 2;

                            // idwFingerIndex = Convert.ToInt32("0" + row.SubItems[].Text.Trim());  ;                     // Fixed card index
                            sCardnumber = row.SubItems[7].Text.Trim();
                            sPassword = txtPassword.Text;
                            iPrivilege = Convert.ToInt32("0" + cbPrivilegecard.Text);

                            bEnabled = (sEnabled == "True");
                            sTmpData = "NO=Finger";                 // Disable fingerprint

                            // Upload card user info
                            axCZKEM1.SetStrCardNumber(sCardnumber);
                            axCZKEM1.SSR_SetUserInfo(iMachineNumber, sdwEnrollNumber, sName, sPassword, iPrivilege, bEnabled);

                            // Upload dummy fingerprint data
                            axCZKEM1.SetUserTmpExStr(iMachineNumber, sdwEnrollNumber, idwFingerIndex, 1, sTmpData);
                            lblattcount.Refresh();
                            // UI update
                            decimal per = ((i + 1) * 100M) / total;
                            lblprogress1.Text = $"{per:N0}%";
                            progressBar1.Value = i + 1;

                            lblattcount.Text = $"Uploading Card Data {i + 1}/{total}  IP: {deviceIp}";
                            lblprogress1.Refresh();
                        }

                        // Finalize upload
                        axCZKEM1.BatchUpdate(iMachineNumber);
                        if (axCZKEM1.ClearAdministrators(iMachineNumber))
                        {
                            axCZKEM1.RefreshData(iMachineNumber);

                            bIsConnected = false;
                        }
                   
                        axCZKEM1.EnableDevice(iMachineNumber, true);

                        MessageBox.Show("Card Index uploaded successfully. Total: "          + listViewupload1.Items.Count          + "   IP: " + deviceIp, "Success");

                        progressBar1.Value = 0;
                    }
                }
                else
                {
                   
                    MessageBox.Show("Please connect the device", "Error");
                }
            }
            catch
            {
                MessageBox.Show("Please connect the device", "Error");
            }
            finally
            {
                Cursor = Cursors.Default;
                comboMasterIp.Enabled = true;
                btncarddownload.Enabled = true;
                btncarddownload.Text = "Card Download ??";
                lblState.Text = "Current State: DisConnected";
                listViewupload1.Items.Clear(); 
                Class.Users.UserTime = 0;
                Class.Users.Intimation = "PAYROLL";

            }

           
        }

        private void Listviewchecklistip1_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            try
            {
                string ip = "";
                if (listViewupload1.Items.Count == 0 || e.Item == null)
                    return;

                // When item is checked -----------------------------------------------------
                if (e.Item.Checked)
                {
                    Cursor = Cursors.WaitCursor;
                    Class.Users.UserTime = 0;
                    
                    //PingReply reply = new Ping().Send(e.Item.SubItems[1].Text, 1000);

                    //bool connected = reply.Status == IPStatus.Success;

                    bool isConnected = false;
                    if (Class.Users.HCompcode == "LOPPL")
                    {
                        spl = e.Item.SubItems[1].Text.Trim().Split('/');
                        ip = spl[0].ToString();
                        reply1 = ping.Send(ip, 1000);
                        isConnected = reply1.Status == IPStatus.Success;
                    }
                    else
                    {
                        ip = e.Item.SubItems[1].Text;
                        reply1 = ping.Send(ip, 1000);
                        isConnected = reply1.Status == IPStatus.Success;
                    }




                    e.Item.SubItems[2].Text = isConnected ? "Connected" : "DisConnected";
                    //e.Item.SubItems[2].Text = isConnected ? "Connected" : "DisConnected";

                    if (isConnected)
                    {
                        // Build row correctly with first column
                        ListViewItem it2 = new ListViewItem();
                        
                        it2.SubItems.Add(e.Item.SubItems[1].Text); // IP
                        it2.SubItems.Add(e.Item.SubItems[2].Text);              
                        it2.SubItems.Add(e.Item.Checked.ToString());
                        allip.Items.Add(it2);
                    }

                    Cursor = Cursors.Default;
                   
                }

                // When item is UNchecked ---------------------------------------------------
                if (!e.Item.Checked && e.Item.SubItems[2].Text == "Connected")
                {
                    Cursor = Cursors.WaitCursor;
                    e.Item.SubItems[2].Text = "DisConnected";

                    string ipToRemove = e.Item.SubItems[1].Text;

                    // Remove safely (reverse loop)
                    for (int i = allip.Items.Count - 1; i >= 0; i--)
                    {
                        if (allip.Items[i].SubItems[1].Text == ipToRemove)
                        {
                            allip.Items.RemoveAt(i);
                        }
                    }

                    Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

                   }

        private void Butfacetransfer_Click(object sender, EventArgs e)
        {
            try
            {
                txtfactransferesearch.Text = "";
                Class.Users.UserTime = 0;

                // Validate there is at least one IP to process
                if (allip == null || allip.Items.Count == 0)
                {
                    MessageBox.Show("Machine not connected. Please send IP Address", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var result = MessageBox.Show("Do you want to export Face Index??", "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (result != DialogResult.OK) return;

                Cursor = Cursors.WaitCursor;
                comboMasterIp.Enabled = false;
                btnfaceDownload.Enabled = false;
                lblState.Text = "Current State:Processing";

                // iterate each IP in the allip list
                for (int j = 0; j < allip.Items.Count; j++)
                {
                   
                    spl = null; string deviceIp = "";
                    if (Class.Users.HCompcode == "LOPPL")
                    {
                        spl = allip.Items[j].SubItems[1].Text.Trim().Split('/');
                        deviceIp = spl[0].ToString();
                    }
                    else
                    {

                        deviceIp = allip.Items[j].SubItems[1].Text.ToString();
                    }

                  
                   // string deviceIp = allip.Items[j].SubItems.Count > 1 ? allip.Items[j].SubItems[1].Text : null;
                    if (string.IsNullOrWhiteSpace(deviceIp) || deviceIp.Length <= 10)
                        continue; // skip invalid IPs

                    bool connected = axCZKEM1.Connect_Net(deviceIp, Convert.ToInt32(txtPort.Text));
                    if (!connected)
                    {
                        MessageBox.Show($"Machine DisConnected: {deviceIp}", "Warning");
                        continue;
                    }

                    try
                    {
                        lblState.Text = "Current State:Connected";
                        axCZKEM1.EnableDevice(iMachineNumber, false);

                        int total = Math.Max(1, listViewupload2.Items.Count);
                        progressBar1.Minimum = 0;
                        progressBar1.Maximum = total;
                        progressBar1.Value = 0;

                        for (int i = 0; i < listViewupload2.Items.Count; i++)
                        {
                         
                            sdwEnrollNumber = listViewupload2.Items[i].SubItems[2].Text; 
                            sName = listViewupload2.Items[i].SubItems[3].Text;                           
                            idwFingerIndex = Convert.ToInt32(listViewupload2.Items[i].SubItems[4].Text);
                            sTmpData = listViewupload2.Items[i].SubItems[6].Text;   // face template
                            sTmpData1 = sTmpData;                                   // duplicate by original logic
                            sCardnumber = listViewupload2.Items[i].SubItems[7].Text;
                            iPrivilege = Convert.ToInt32("0" + listViewupload2.Items[i].SubItems[8].Text);
                            sPassword = listViewupload2.Items[i].SubItems[9].Text;
                            sEnabled = listViewupload2.Items[i].SubItems[10].Text;
                            bEnabled = string.Equals(sEnabled, "True", StringComparison.OrdinalIgnoreCase);

                            axCZKEM1.SetStrCardNumber(sCardnumber);

                            bool setUser = axCZKEM1.SSR_SetUserInfo(iMachineNumber, sdwEnrollNumber, sName, sPassword, iPrivilege, bEnabled);
                           
                            if (setUser)
                            {
                                // SDK: set face template
                                axCZKEM1.SetUserFaceStr(iMachineNumber, sdwEnrollNumber, idwFingerIndex, sTmpData, iLength);
                            }
                            
                            sTmpData = "";
                            // progress UI
                            decimal per = ((i + 1) * 100) / total;
                            lblprogress1.Text = $"{per:N0} %";
                            lblprogress1.Refresh();
                            progressBar1.Value = i + 1;
                            lblattcount.Text = $"Uploading face {i + 1}/{total}  IP: {deviceIp}";
                            lblattcount.Refresh();
                        }

                        // commit to device
                        axCZKEM1.BatchUpdate(iMachineNumber);
                        if (axCZKEM1.ClearAdministrators(iMachineNumber))
                        {
                            axCZKEM1.RefreshData(iMachineNumber);

                            bIsConnected = false;
                        }
                     
                        MessageBox.Show($"Successfully uploaded face templates. Total: {listViewupload2.Items.Count}  IP: {deviceIp}", "Success");
                    }
                    catch (Exception exInner)
                    {
                        MessageBox.Show($"Error while uploading to {deviceIp}: {exInner.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        try { axCZKEM1.EnableDevice(iMachineNumber, true); } catch { }
                        try { axCZKEM1.Disconnect(); } catch { } // optionally disconnect after each device
                        Cursor = Cursors.Default;
                    }
                }

                // final UI reset
                lblState.Text = "Current State:DisConnected";
                listviewchecklistip2.Items.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please connect device. " + ex.Message, "Error");
            }
            finally
            {
                listViewupload2.Items.Clear();
                Lvdownremove.Items.Clear();
                comboMasterIp.Enabled = true;
                btnfaceDownload.Enabled = true;
                Cursor = Cursors.Default;
                bIsConnected = false;
                progressBar1.Value = 0;
                lblprogress1.Text = "";
                lblattcount.Text = "";
            }

            //try
            //{
            //    txtfactransferesearch.Text = ""; Class.Users.UserTime = 0;
            //    if (listviewchecklistip2.CheckedItems.Count >= 0)
            //    {

            //        DialogResult result = MessageBox.Show("Do You want to Export  Face Index??", "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            //        if (result.Equals(DialogResult.OK))
            //        {
            //            for (int j = 0; j < allip.Items.Count; j++)
            //            {

            //                if (allip.Items.Count >= 0)
            //                {

            //                    Cursor = Cursors.WaitCursor;
            //                    if (allip.Items[j].SubItems[1].Text.Length > 10)
            //                    {
            //                        lblattcount.Text = ""; bIsConnected = false;
            //                        bIsConnected = axCZKEM1.Connect_Net(allip.Items[j].SubItems[1].Text, Convert.ToInt32(txtPort.Text));
            //                        if (bIsConnected == true)
            //                        {

            //                            lblState.Text = "Current State:Connected";
            //                            int idwErrorCode = 0;
            //                            int iFlag = 1;

            //                            axCZKEM1.EnableDevice(iMachineNumber, false);

            //                            int i = 0;
            //                            UICO uti = new UICO();
            //                            progressBar1.Minimum = 0;
            //                            progressBar1.Maximum = listViewupload2.Items.Count;
            //                            for (i = 0; i < listViewupload2.Items.Count; i++)
            //                            {

            //                                //string ss = "";
            //                                //string finger = "";
            //                                //string face = "";

            //                                string c = "";
            //                                sdwEnrollNumber = listViewupload2.Items[i].SubItems[1].Text;
            //                                sName = listViewupload2.Items[i].SubItems[2].Text;
            //                                idwFingerIndex = Convert.ToInt32(iFaceIndex);
            //                                sTmpData = listViewupload2.Items[i].SubItems[5].Text;
            //                                sTmpData1 = listViewupload2.Items[i].SubItems[5].Text;
            //                                sCardnumber = listViewupload2.Items[i].SubItems[6].Text;
            //                                iPrivilege = Convert.ToInt32(listViewupload2.Items[i].SubItems[7].Text);
            //                                sPassword = listViewupload2.Items[i].SubItems[8].Text;
            //                                sEnabled = listViewupload2.Items[i].SubItems[9].Text;
            //                                //iFlag = Convert.ToInt32("0" + listViewupload2.Items[i].SubItems[9].Text);
            //                                MacIP = allip.Items[j].SubItems[1].Text;    

            //                                axCZKEM1.SetStrCardNumber(sCardnumber);//Before you using function SetUserInfo,set the card number to make sure you can upload it to the device
            //                                if (axCZKEM1.SSR_SetUserInfo(iMachineNumber, sUserID, sName, sPassword, iPrivilege, bEnabled))//face templates are part of users' information
            //                                {
            //                                    axCZKEM1.SetUserFaceStr(iMachineNumber, sUserID, iFaceIndex, sTmpData1, iLength);//upload face templates information to the device
            //                                    lblattcount.Text = "Total Employee Finger Rows Count  : " + listViewupload2.Items.Count.ToString() + " and IP Addres   :" + allip.Items[j].SubItems[1].Text;
            //                                    decimal per = Convert.ToDecimal(100 / GenFun.ToDecimal(listViewupload2.Items.Count)) * (i + 1);
            //                                    lblprogress1.Text = per.ToString("N0") + " %";
            //                                    lblprogress1.Refresh();
            //                                    progressBar1.Value = i + 1;
            //                                }                                     
            //                                else
            //                                {
            //                                    decimal per = Convert.ToDecimal(100 / GenFun.ToDecimal(listViewupload2.Items.Count)) * (i + 1);
            //                                    lblprogress1.Text = per.ToString("N0") + " %";
            //                                    lblprogress1.Refresh();
            //                                    progressBar1.Value = i + 1;                                           
            //                                    Cursor = Cursors.Default;
            //                                    axCZKEM1.EnableDevice(iMachineNumber, true);                                         

            //                                }
            //                            }

            //                            axCZKEM1.BatchUpdate(iMachineNumber);//upload all the information in the memory
            //                            axCZKEM1.RefreshData(iMachineNumber);//the data in the device should be refreshed
            //                            axCZKEM1.EnableDevice(iMachineNumber, true);


            //                            MessageBox.Show("Successfully upload fingerprint, " + "total:" + listViewupload2.Items.Count.ToString() + "IP      :" + allip.Items[j].SubItems[1].Text, "Success");
            //                            Cursor = Cursors.Default;
            //                            progressBar1.Value = 0;
            //                        }
            //                        else
            //                        {
            //                            MessageBox.Show("Machine DisConnected" + allip.Items[j].SubItems[1].Text);
            //                        }

            //                    }
            //                    //j--;
            //                }
            //                else
            //                {
            //                    Cursor = Cursors.Default;
            //                    MessageBox.Show("Invalid");
            //                }


            //                Cursor = Cursors.Default;
            //            }
            //        }

            //    }
            //    else
            //    {
            //        Cursor = Cursors.Default;
            //        MessageBox.Show("Machine not connected.pls send IP Address", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            //    }
            //    //Cursor = Cursors.Default;
            //    comboMasterIp.Enabled = true;

            //    btnfaceDownload.Enabled = true;
            //    lblState.Text = "Current State:DisConnected";
            //    listviewchecklistip2.Items.Clear(); 
            //}
            //catch (Exception ex)
            //{
            //    Cursor = Cursors.Default;
            //    MessageBox.Show("pls Connect Device", "error");
            //}
            //listViewupload2.Items.Clear(); Lvdownremove.Items.Clear();
        }

        private void Listviewchecklistip2_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            try
            {
                Class.Users.UserTime = 0;
                if (listViewupload2.Items.Count == 0 || e.Item == null)
                    return;

                if (e.Item.Checked)
                {
                    Cursor = Cursors.WaitCursor;
                    Class.Users.UserTime = 0;
                    spl = null; string deviceIp = "";
                    if (Class.Users.HCompcode == "LOPPL")
                    {
                        spl = e.Item.SubItems[1].Text.Trim().Split('/');
                        deviceIp = spl[0].ToString();
                        reply1 = ping.Send(deviceIp, 1000);
                    }
                    else
                    {

                        deviceIp = e.Item.SubItems[1].Text;
                        reply1 = ping.Send(deviceIp, 1000);
                    }
                   
                  

                    bool connected = reply1.Status == IPStatus.Success;

                    e.Item.SubItems[2].Text = connected ? "Connected" : "DisConnected";

                    if (connected)
                    {
                        // Always set first column (TEXT) before adding SubItems
                        ListViewItem it2 = new ListViewItem();
                        it2.SubItems.Add(e.Item.SubItems[1].Text);
                        it2.SubItems.Add(e.Item.SubItems[2].Text);
                        it2.SubItems.Add(e.Item.Checked.ToString());

                        allip.Items.Add(it2);
                    }

                    Cursor = Cursors.Default;
                }
                else
                {
                    // UNCHECK event
                    if (e.Item.SubItems[2].Text == "Connected")
                    {
                        Cursor = Cursors.WaitCursor;
                        e.Item.SubItems[2].Text = "DisConnected";

                        if (listviewchecklistip2.SelectedItems.Count > 0)
                        {
                            string selectedIP = e.Item.SubItems[1].Text;

                            for (int c = 0; c < allip.Items.Count; c++)
                            {
                                if (allip.Items[c].SubItems[1].Text == selectedIP)
                                {
                                    allip.Items[c].Remove();
                                    c--;
                                }
                            }
                        }

                        Cursor = Cursors.Default;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString());
            }

        }

        private void Txtcardtransfer_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int item0 = 0;
                Class.Users.UserTime = 0;
                LvDownload1.BeginUpdate();
                LvDownload1.Items.Clear();

                string search = txtcardtransfer.Text.Trim();
                bool hasSearch = search.Length >= 1;

                foreach (ListViewItem item in listfilter.Items)
                {
                    bool match = true;

                    if (hasSearch)
                    {
                        string col2 = item.SubItems[2].Text;
                        string col3 = item.SubItems[3].Text;

                        match = col2.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0
                             || col3.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0;
                    }

                    if (match)
                    {
                        ListViewItem list = new ListViewItem();

                        list.Text = item0.ToString();

                        // Copy all subitems (from 1 to 12)
                        for (int i = 1; i <= 12; i++)
                        {
                            if (i < item.SubItems.Count)
                                list.SubItems.Add(item.SubItems[i].Text);
                            else
                                list.SubItems.Add("");
                        }

                        // Alternating color
                        list.BackColor = (item0 % 2 == 0) ? Color.White : Color.WhiteSmoke;

                        LvDownload1.Items.Add(list);
                    }

                    item0++;
                }
            }
            catch (Exception ex)
            {
                // optional logging
            }
            finally
            {
                LvDownload1.EndUpdate();
            }

            //try
            //{

            //    int item0 = 0; Class.Users.UserTime = 0;
            //    if (txtcardtransfer.Text.Length >= 1)
            //    {
            //         LvDownload1.Items.Clear();

            //        foreach (ListViewItem item in listfilter.Items)
            //        {

            //            if (listfilter.Items[item0].SubItems[2].ToString().Contains(txtcardtransfer.Text) || listfilter.Items[item0].SubItems[3].ToString().Contains(txtcardtransfer.Text))
            //            {

            //                ListViewItem list = new ListViewItem();


            //                list.Text = item0.ToString();
            //                // list.SubItems.Add(listfilter.Items[item0].SubItems[1].Text);
            //                list.SubItems.Add(listfilter.Items[item0].SubItems[1].Text);
            //                list.SubItems.Add(listfilter.Items[item0].SubItems[2].Text);
            //                list.SubItems.Add(listfilter.Items[item0].SubItems[3].Text);
            //                list.SubItems.Add(listfilter.Items[item0].SubItems[4].Text);
            //                list.SubItems.Add(listfilter.Items[item0].SubItems[5].Text);
            //                list.SubItems.Add(listfilter.Items[item0].SubItems[6].Text);
            //                list.SubItems.Add(listfilter.Items[item0].SubItems[7].Text);
            //                list.SubItems.Add(listfilter.Items[item0].SubItems[8].Text);
            //                list.SubItems.Add(listfilter.Items[item0].SubItems[9].Text);
            //                list.SubItems.Add(listfilter.Items[item0].SubItems[10].Text);
            //                list.SubItems.Add(listfilter.Items[item0].SubItems[11].Text);
            //                list.SubItems.Add(listfilter.Items[item0].SubItems[12].Text);
            //                if (item0 % 2 == 0)
            //                {
            //                    list.BackColor = Color.White;

            //                }
            //                else
            //                {
            //                    list.BackColor = Color.WhiteSmoke;

            //                }
            //                LvDownload1.Items.Add(list);

            //            }
            //            item0++; 

            //        }

            //    }
            //    else
            //    {
            //        ListView ll = new ListView();
            //        LvDownload1.Items.Clear();
            //        foreach (ListViewItem item in listfilter.Items)
            //        {


            //            this.LvDownload1.Items.Add((ListViewItem)item.Clone());

            //            if (item0 % 2 == 0)
            //            {
            //                item.BackColor = Color.White;

            //            }
            //            else
            //            {
            //                item.BackColor = Color.WhiteSmoke;

            //            }

            //            item0++;
            //        }

            //    }


            //}
            //catch (Exception ex)
            //{
            //    //MessageBox.Show("---" + ex.ToString());
            //}


        }

        private void Txtfactransferesearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                LvDownload2.BeginUpdate();
                LvDownload2.Items.Clear();

                string search = txtfactransferesearch.Text.Trim();
                bool hasSearch = search.Length >= 1;

                foreach (ListViewItem src in listfilter.Items)
                {
                    bool match = true;

                    if (hasSearch)
                    {
                        string col0 = src.SubItems[1].Text;
                        string col1 = src.SubItems[2].Text;

                        match = col0.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0
                             || col1.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0;
                    }

                    if (match)
                    {
                        LvDownload2.Items.Add((ListViewItem)src.Clone());
                    }
                }
            }
            catch (Exception ex)
            {
                // Log or show error if needed
            }
            finally
            {
                LvDownload2.EndUpdate();
            }


            //try
            //{

            //    int item0 = 0;
            //    if (txtfactransferesearch.Text.Length >= 1)
            //    {
            //        LvDownload2.Items.Clear();
            //        foreach (ListViewItem item in listfilter.Items)
            //        {

            //            if (listfilter.Items[item0].SubItems[0].ToString().Contains(txtfactransferesearch.Text) || listfilter.Items[item0].SubItems[1].ToString().Contains(txtfactransferesearch.Text))
            //            {

            //                ListViewItem list = new ListViewItem();
            //                list.SubItems.Add(listfilter.Items[item0].SubItems[1].Text);
            //                list.SubItems.Add(listfilter.Items[item0].SubItems[2].Text);
            //                list.SubItems.Add(listfilter.Items[item0].SubItems[3].Text);
            //                list.SubItems.Add(listfilter.Items[item0].SubItems[4].Text);
            //                list.SubItems.Add(listfilter.Items[item0].SubItems[5].Text);
            //                list.SubItems.Add(listfilter.Items[item0].SubItems[6].Text);
            //                list.SubItems.Add(listfilter.Items[item0].SubItems[7].Text);
            //                list.SubItems.Add(listfilter.Items[item0].SubItems[8].Text);
            //                list.SubItems.Add(listfilter.Items[item0].SubItems[9].Text);
            //                LvDownload2.Items.Add(list);


            //            }
            //            item0++;
            //        }

            //    }
            //    else
            //    {
            //        ListView ll = new ListView();
            //        LvDownload2.Items.Clear();
            //        foreach (ListViewItem item in listfilter.Items)
            //        {


            //            this.LvDownload2.Items.Add((ListViewItem)item.Clone());



            //            item0++;
            //        }

            //    }


            //}
            //catch (Exception ex)
            //{
            //    //MessageBox.Show("---" + ex.ToString());
            //}
        }

        private void Tab5CardReader_Click(object sender, EventArgs e)
        {

        }

        private void listViewupload1_ItemActivate(object sender, EventArgs e)
        {
            try
            {
                if (listViewupload1.Items.Count > 0)
                {
                    var confirmation = MessageBox.Show("Do You want Delete this Record ?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (confirmation == DialogResult.Yes)
                    {
                        int i = 0;
                        for (i = 0; i < listViewupload1.Items.Count; i++)
                        {

                            if (listViewupload1.Items[i].Selected)
                            {
                               
                                listViewupload1.Items[i].Remove();
                                i--;
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("pls upload data from Master IP", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void listViewupload2_ItemActivate(object sender, EventArgs e)
        {
            try
            {
                if (listViewupload2.Items.Count > 0)
                {
                    var confirmation = MessageBox.Show("Do You want Delete this Record ?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (confirmation == DialogResult.Yes)
                    {
                        int i = 0;
                        for (i = 0; i < listViewupload2.Items.Count; i++)
                        {

                            if (listViewupload2.Items[i].Selected)
                            {
                                MessageBox.Show("UserID:   " + listViewupload2.Items[i].SubItems[1].Text + "      Name:  " + listViewupload2.Items[i].SubItems[2].Text, "Delete this Record");

                                listViewupload2.Items[i].Remove();
                                i--;
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("pls upload data from Master IP", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

     
       
        private void GetDeviceTime_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            if (comboremoveip.Text == "")
            {
                MessageBox.Show("pls Select IPAddress", "Error"); Cursor = Cursors.Default;
                return;
            }
            bIsConnected = axCZKEM1.Connect_Net(comboremoveip.Text, Convert.ToInt32(txtPort.Text));
            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first!", "Error"); Cursor = Cursors.Default;
                return;
            }
            if (bIsConnected == true)
            {
                if (axCZKEM1.GetDeviceTime(iMachineNumber, ref idwYear, ref idwMonth, ref idwDay, ref idwHour, ref idwMinute, ref idwSecond))
                {
                    labeltime.Text = "";

                    labeltime.Text = "Now Machine Date Time  " + comboremoveip.Text + "   --    " + idwDay + "-" + idwMonth + "-" + idwYear + "   -  " + idwHour + "-" + idwMinute + "-" + idwSecond;
                    axCZKEM1.Disconnect();
                }
            }
            Cursor = Cursors.Default;
        }



        private void poweroffdevice_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            if (comboremoveip.Text == "")
            {
                MessageBox.Show("pls Select IPAddress", "Error"); Cursor = Cursors.Default;
                return;
            }
            bIsConnected = axCZKEM1.Connect_Net(comboremoveip.Text, Convert.ToInt32(txtPort.Text));
            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first!", "Error"); Cursor = Cursors.Default;
                return;
            }
            if (bIsConnected == true)
            {
                axCZKEM1.PowerOffDevice(iMachineNumber); axCZKEM1.Disconnect();
                //  MessageBox.Show(dd.ToString());
            }
            Cursor = Cursors.Default;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            axCZKEM1.PowerOnAllDevice();
            axCZKEM1.Disconnect();
            Cursor = Cursors.Default;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor; Class.Users.UserTime = 0;
            if (comboremoveip.Text == "")
            {
                MessageBox.Show("pls Select IPAddress", "Error"); Cursor = Cursors.Default;
                return;
            }
            bIsConnected = axCZKEM1.Connect_Net(comboremoveip.Text, Convert.ToInt32(txtPort.Text));
            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first!", "Error"); Cursor = Cursors.Default;
                return;
            }
            if (bIsConnected == true)
            {
                axCZKEM1.SetDeviceTime(iMachineNumber);


                labeltime.Text = "Now Machine Date Time  " + comboremoveip.Text + "   --    " + System.DateTime.Now.ToString();
                axCZKEM1.Disconnect();


            }
            Cursor = Cursors.Default;
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
            using (SaveFileDialog std = new SaveFileDialog() { Filter = "Excel Workbook|*.xls", ValidateNames = true })
                if (std.ShowDialog() == DialogResult.OK)
                {
                    Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                    Microsoft.Office.Interop.Excel.Workbook wb = app.Workbooks.Add(Microsoft.Office.Interop.Excel.XlSheetType.xlWorksheet);
                    Microsoft.Office.Interop.Excel.Worksheet ws = (Microsoft.Office.Interop.Excel.Worksheet)app.ActiveSheet;
                    app.Visible = false;
                    ws.Cells[1, 1] = "UserID";
                    ws.Cells[1, 2] = "EmpName";
                    int i = 2;
                    if (LvDownload.Items.Count > 0)
                    {
                        foreach (ListViewItem item in LvDownload.Items)
                        {
                            ws.Cells[i, 1] = item.SubItems[2].Text;
                            ws.Cells[i, 2] = item.SubItems[3].Text;
                            i++;
                        }
                    }
                    if (LvDownload2.Items.Count > 0)
                    {
                        foreach (ListViewItem item in LvDownload2.Items)
                        {
                            ws.Cells[i, 1] = item.SubItems[2].Text;
                            ws.Cells[i, 2] = item.SubItems[3].Text;
                            i++;
                        }
                    }
                    if (LvDownload1.Items.Count > 0)
                    {

                        foreach (ListViewItem item in LvDownload1.Items)
                        {
                            ws.Cells[i, 1] = item.SubItems[2].Text;
                            ws.Cells[i, 2] = item.SubItems[3].Text;
                            i++;
                        }
                    }
                    wb.SaveAs(std.FileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault, Type.Missing, true, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, (Microsoft.Office.Interop.Excel.XlSaveAsAccessMode)Microsoft.Office.Interop.Excel.XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing);
                    app.Quit();
                    MessageBox.Show("Excel Data Imported Successfully", "Information");
                }

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
            Cursor = Cursors.WaitCursor;
            bIsConnected = axCZKEM1.Connect_Net(comboremoveip.Text, Convert.ToInt32(txtPort.Text));
            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device IP!", "Error"); Cursor = Cursors.Default;
                Cursor = Cursors.Default;
            }
            if (bIsConnected == true)
            {
                axCZKEM1.SetDeviceIP(iMachineNumber, txtremovesearch.Text);

                axCZKEM1.Disconnect();
                MessageBox.Show("Device IP Changed...!" + txtremovesearch.Text, "");

            }
            Cursor = Cursors.Default;

        }

        private void butsetcardno_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            bIsConnected = axCZKEM1.Connect_Net(comboremoveip.Text, Convert.ToInt32(txtPort.Text));
            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device IP!", "Error"); Cursor = Cursors.Default;
                Cursor = Cursors.Default;
            }
            if (bIsConnected == true)
            {
                string[] DATA = txtremovesearch.Text.Split(',');
                axCZKEM1.SSR_SetUserInfo(1, DATA[0].Trim(), DATA[1].Trim(), "", 1, true);
                axCZKEM1.SetStrCardNumber(DATA[2].Trim());
                axCZKEM1.Disconnect();
                MessageBox.Show("User Addred...!" + txtremovesearch.Text, "");
                txtremovesearch.Text = "";
            }
            Cursor = Cursors.Default;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //////try
            //////{
            //////    string selmax = $"SELECT MAX(a.ATTLOGID) AS ATTLOGID FROM {Class.Users.HCompcode}TRS_ATTLOG a WHERE a.DATETIMERECORD='"+ frmdate.Text + "'";
            //////    Int32 maxid = Convert.ToInt32("0" + Utility.ExecuteScalar(selmax);

            //////    string sel01 = $"SELECT ATTLOGID, jsondata FROM sklTRS_TEMPATTLOG WHERE ATTLOGID="+ maxid;
            //////    DataSet ds01 =  Utility.ExecuteSelectQuery(sel01, "sklTRS_TEMPATTLOG");
            //////    DataTable dt01 = ds01.Tables["sklTRS_TEMPATTLOG"];

            //////    if (dt01 != null && dt01.Rows.Count > 0)
            //////    {
            //////        Models.DeviceCommunication user = new Models.DeviceCommunication();
            //////        bytes = (byte[])dt01.Rows[0]["jsondata"];
            //////        user.JSONDATA = Models.Device.BytesToString(bytes);

            //////        JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            //////        List<Models.DeviceCommunication> listEmployee = javaScriptSerializer.Deserialize<List<Models.DeviceCommunication>>(user.JSONDATA);

            //////        DataTable dt11 = ConvertToDatatable(listEmployee);

            //////        if (dt11.Rows.Count > 0)
            //////        {
            //////            Cursor = Cursors.WaitCursor;
            //////            lvLogs.BeginUpdate();

            //////            int i = 1;
            //////            foreach (DataRow myRow in dt11.Rows)
            //////            {
            //////                ListViewItem list = new ListViewItem(i.ToString()); // First column
            //////                list.SubItems.Add(myRow["ENROLLNO"].ToString());
            //////                list.SubItems.Add(myRow["IPADDRESS"].ToString());
            //////                list.SubItems.Add(""); // Empty column
            //////                list.SubItems.Add(myRow["DATETIMERECORD"].ToString());
            //////                list.SubItems.Add(myRow["DATETIMERECORD"].ToString());
            //////                list.SubItems.Add(myRow["MACHINENUMBER"].ToString());
            //////                list.SubItems.Add(myRow["MACHINENUMBER"].ToString());

            //////                // Alternating row colors
            //////                list.BackColor = (i % 2 == 0) ? Color.White : Color.WhiteSmoke;

            //////                lvLogs.Items.Add(list);
            //////                listfilter.Items.Add((ListViewItem)list.Clone());
            //////                i++;
            //////            }

            //////            lvLogs.EndUpdate();
            //////            lblattcount.Text = $"Total Count: {lvLogs.Items.Count}";
            //////            Cursor = Cursors.Default;
            //////        }
            //////    }
            //////}
            //////catch (Exception ex)
            //////{
            //////    MessageBox.Show("Error loading logs: " + ex.Message);
            //////}

        }

        private void checkall_CheckedChanged(object sender, EventArgs e)
        {

            try
            {
                int i = 0, j = 1; progressBar1.Minimum = 0;
                progressBar1.Maximum = listviewattdown.Items.Count; 
                if (checkall.Checked == true)
                {
                    allip1.Items.Clear();
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


        //When you have enrolled a new user,this event will be triggered.
        private void axCZKEM1_OnNewUser(int iEnrollNumber)
        {
            lbRTShow.Items.Add("RTEvent OnNewUser Has been Triggered...");
            lbRTShow.Items.Add("...NewUserID=" + iEnrollNumber.ToString());
        }

        //When you swipe a card to the device, this event will be triggered to show you the number of the card.
        private void axCZKEM1_OnHIDNum(int iCardNumber)
        {
            lbRTShow.Items.Add("RTEvent OnHIDNum Has been Triggered...");
            lbRTShow.Items.Add("...Cardnumber=" + iCardNumber.ToString());
        }

        //When you have emptyed the Mifare card,this event will be triggered.
        private void axCZKEM1_OnEmptyCard(int iActionResult)
        {
            lbRTShow.Items.Add("RTEvent OnEmptyCard Has been Triggered...");
            if (iActionResult == 0)
            {
                lbRTShow.Items.Add("...Empty Mifare Card OK");
            }
            else
            {
                lbRTShow.Items.Add("...Empty Failed");
            }
        }

        //When you have written into the Mifare card ,this event will be triggered.
        private void axCZKEM1_OnWriteCard(int iEnrollNumber, int iActionResult, int iLength)
        {
            lbRTShow.Items.Add("RTEvent OnWriteCard Has been Triggered...");
            if (iActionResult == 0)
            {
                lbRTShow.Items.Add("...Write Mifare Card OK");
                lbRTShow.Items.Add("...EnrollNumber=" + iEnrollNumber.ToString());
                lbRTShow.Items.Add("...TmpLength=" + iLength.ToString());
            }
            else
            {
                lbRTShow.Items.Add("...Write Failed");
            }
        }

        //After you swipe your card to the device,this event will be triggered.
        //If your card passes the verification,the return value  will be user id, or else the value will be -1
        private void axCZKEM1_OnVerify(int iUserID)
        {
            lbRTShow.Items.Add("RTEvent OnVerify Has been Triggered,Verifying...");
            if (iUserID != -1)
            {
                lbRTShow.Items.Add("Verified OK,the UserID is " + iUserID.ToString());
            }
            else
            {
                lbRTShow.Items.Add("Verified Failed... ");
            }
        }

        //If your card passes the verification,this event will be triggered
        private void axCZKEM1_OnAttTransaction(int iEnrollNumber, int iIsInValid, int iAttState, int iVerifyMethod, int iYear, int iMonth, int iDay, int iHour, int iMinute, int iSecond)
        {
            lbRTShow.Items.Add("RTEvent OnAttTrasaction Has been Triggered,Verified OK");
            lbRTShow.Items.Add("...UserID:" + iEnrollNumber.ToString());
            lbRTShow.Items.Add("...isInvalid:" + iIsInValid.ToString());
            lbRTShow.Items.Add("...attState:" + iAttState.ToString());
            lbRTShow.Items.Add("...VerifyMethod:" + iVerifyMethod.ToString());
            lbRTShow.Items.Add("...Time:" + iYear.ToString() + "-" + iMonth.ToString() + "-" + iDay.ToString() + " " + " " + iHour.ToString() + ":" + iMinute.ToString() + ":" + iSecond.ToString());

            string sName = "";
            string sPassword = "";
            int iPrivilege = 0;
            bool bEnabled = false;
            string sCardnumber = "";

            axCZKEM1.ReadAllUserID(iMachineNumber);
            while (axCZKEM1.GetUserInfo(iMachineNumber, iEnrollNumber, ref sName, ref sPassword, ref iPrivilege, ref bEnabled))//get user information from memory
            {
                if (axCZKEM1.GetStrCardNumber(out sCardnumber))//get the card number from the memory
                {
                    lbRTShow.Items.Add("...Cardnumber on device:" + sCardnumber);
                    return;
                }
            }
        }

        //If your fingerprint(or your card) passes the verification,this event will be triggered
        private void axCZKEM1_OnAttTransactionEx(string sEnrollNumber, int iIsInValid, int iAttState, int iVerifyMethod, int iYear, int iMonth, int iDay, int iHour, int iMinute, int iSecond, int iWorkCode)
        {
            lbRTShow.Items.Add("RTEvent OnAttTrasactionEx Has been Triggered,Verified OK");
            lbRTShow.Items.Add("...UserID:" + sEnrollNumber);
            lbRTShow.Items.Add("...isInvalid:" + iIsInValid.ToString());
            lbRTShow.Items.Add("...attState:" + iAttState.ToString());
            lbRTShow.Items.Add("...VerifyMethod:" + iVerifyMethod.ToString());
            lbRTShow.Items.Add("...Workcode:" + iWorkCode.ToString());//the difference between the event OnAttTransaction and OnAttTransactionEx
            lbRTShow.Items.Add("...Time:" + iYear.ToString() + "-" + iMonth.ToString() + "-" + iDay.ToString() + " " + iHour.ToString() + ":" + iMinute.ToString() + ":" + iSecond.ToString());

            string sName = "";
            string sPassword = "";
            int iPrivilege = 0;
            bool bEnabled = false;
            string sCardnumber = "";

            int idwEnrollNumber = Convert.ToInt32(sEnrollNumber);
            axCZKEM1.ReadAllUserID(iMachineNumber);
            while (axCZKEM1.GetUserInfo(iMachineNumber, idwEnrollNumber, ref sName, ref sPassword, ref iPrivilege, ref bEnabled))//get user information from memory
            {
                if (axCZKEM1.GetStrCardNumber(out sCardnumber))//get the card number from the memory
                {
                    return;
                }
            }
        }

        private void LvDownload2_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
         

            ListViewItem itt = new ListViewItem();
            Class.Users.UserTime = 0;

            iIndex = LvDownload2.Items.Count;
            progressBar1.Value = 0;

            if (e.Item.Checked)
            {
                // Copy all subitems safely
                for (int i = 1; i < e.Item.SubItems.Count; i++)
                {
                    itt.SubItems.Add(e.Item.SubItems[i].Text);
                }

                // Alternating row color
                itt.BackColor = (iIndex % 2 == 0) ? Color.White : Color.WhiteSmoke;
                if (itt.SubItems[6].Text != "")
                {
                    listViewupload2.Items.Add(itt);
                }
                iIndex++;
            }


        }

        private void tab6Attlots_Click(object sender, EventArgs e)
        {

        }

        private void butmachinereset_Click(object sender, EventArgs e)
        {
            string ccode = "";
            Class.Users.Intimation = "PAYROLL";
            ccode = Class.Users.HCompcode;
            Class.Users.UserTime = 0;
            DTROW.Rows.Clear();
            totalrows = 0;
            try
            {
                progressBar1.Value = 0;
                listfilter.Items.Clear();

                // Ensure schema
                if (DTROW.Columns.Count == 0)
                {
                    DTROW.Columns.Add("MACHINENUMBER");
                    DTROW.Columns.Add("IPADDRESS");
                    DTROW.Columns.Add("ENROLLNO");
                    DTROW.Columns.Add("DATETIMERECORD");
                }

                Cursor = Cursors.WaitCursor;
                lblattcount.Text = "";
                spl = null; string deviceIp = "";
                if (Class.Users.HCompcode == "LOPPL")
                {
                    spl = comboremoveip.Text.Trim().Split('/');
                    deviceIp = spl[0].ToString();
                }
                else
                {
                    deviceIp = comboremoveip.Text.ToString();
                }
                // Connect
                 bIsConnected = false;
                 bIsConnected = axCZKEM1.Connect_Net(deviceIp, Convert.ToInt32(txtPort.Text));

                lblattcount.Text = $"CONNECTED. IPAddress: {deviceIp}";

                if (bIsConnected)
                {
                    DTROW.Rows.Clear();

                    axCZKEM1.EnableDevice(iMachineNumber, false);//disable the device                               
                    while (axCZKEM1.SSR_GetGeneralLogData(iMachineNumber, out sdwEnrollNumber, out idwVerifyMode, out idwInOutMode, out idwYear, out idwMonth, out idwDay, out idwHour, out idwMinute, out idwSecond, ref idwWorkcode))//get records from the memory
                    {
                        string dateStr = $"{idwDay.ToString().PadLeft(2, '0')}-{idwMonth.ToString().PadLeft(2, '0')}-{idwYear}";
                        string timeStr = $"{idwHour.ToString().PadLeft(2, '0')}:{idwMinute.ToString().PadLeft(2, '0')}:{idwSecond.ToString().PadLeft(2, '0')}";
                     
                        
                            DataRow dr = DTROW.NewRow();
                            dr["MACHINENUMBER"] = iMachineNumber;
                            dr["IPADDRESS"] = deviceIp;
                            dr["ENROLLNO"] = sdwEnrollNumber;
                            dr["DATETIMERECORD"] = $"{dateStr} {timeStr}";
                            DTROW.Rows.Add(dr);
                         
                     
                        totalrows += 1;
                    }
                    // Update UI
                    lbltotemp.Text = totalrows.ToString();
                    label17.Text = DTROW.Rows.Count.ToString();

                    if (DTROW.Rows.Count > 0)
                    {
                        lblfrmdate.Text = DTROW.Rows[0]["DATETIMERECORD"].ToString();
                        lbltodate.Text = DTROW.Rows[DTROW.Rows.Count - 1]["DATETIMERECORD"].ToString();
                    }

                    axCZKEM1.EnableDevice(iMachineNumber, true);
                    if (axCZKEM1.ClearAdministrators(iMachineNumber))
                    {
                        axCZKEM1.RefreshData(iMachineNumber);

                        bIsConnected = false;
                    }
                

                    lblattcount.Text = $"Attendance Download Completed. {totalrows}";
                }
                else
                {
                    lblfrmdate.Text ="DisConnected"+ deviceIp.ToString();
                    lbltodate.Text = ""; lblfrmdate.Text = "";todate.Text = ""; totalrows = 0;
                }
            }
            catch (Exception ex)
            {
                lblattcount.Text = totalrows == 0
                    ? $"No Data found in Machine: {ex}"
                    : $"Install Zkemkeeper DLL: {ex}";
            }

            Class.Users.Intimation = "";
            AttIPLoad();
            allip1.Items.Clear();

            btnConnects.Text = "Connect / Import";
            lblState.Text = "Current State: DisConnected";
            Cursor = Cursors.Default;
            DTROW.Rows.Clear();

           
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void butsingleuser_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(comboMasterIp.Text))
            {
               
                if (!string.IsNullOrWhiteSpace(txtfactransferesearch.Text))
                {
                    Class.Users.UserTime = 0; Cursor = Cursors.WaitCursor;
                    spl = null; string deviceIp = "";
                    if (Class.Users.HCompcode == "LOPPL")
                    {
                        spl =comboMasterIp.Text.Trim().Split('/');
                        deviceIp = spl[0].ToString();
                    }
                    else
                    {
                        
                        deviceIp=comboMasterIp.Text.ToString();
                    }

                    
                   
                    bIsConnected = axCZKEM1.Connect_Net(deviceIp, Convert.ToInt32(txtPort.Text));
                    if (bIsConnected && txtfactransferesearch.Text != "")
                    {
                        axCZKEM1.EnableDevice(iMachineNumber, false);
                        LvDownload2.Items.Clear(); Lvdownall.Items.Clear();
                        listViewupload2.Items.Clear();
                        sUserID = txtfactransferesearch.Text;
                        axCZKEM1.ReadAllUserID(iMachineNumber);
                        lblattcount.Text = $"IDCardNo : {sUserID}  Count : 1";
                        lblattcount.Refresh(); string sql = "";

                        if (axCZKEM1.GetUserFaceStr(iMachineNumber, sUserID, iFaceIndex, ref sTmpData, ref iLength))
                        {

                            sql = $"SELECT C.FNAME || '-' || E.MNNAME1 AS EMPNAME FROM GTCOMPMAST B JOIN HREMPLOYMAST C ON C.COMPCODE = B.GTCOMPMASTID JOIN HREMPLOYDETAILS D ON C.HREMPLOYMASTID = D.HREMPLOYMASTID JOIN GTDEPTDESGMAST E ON E.GTDEPTDESGMASTID = D.DEPTNAME WHERE D.IDACTIVE='YES' AND  D.MIDCARD='{sUserID.Trim()}' AND B.COMPCODE='" + Class.Users.HCompcode + "'   ORDER BY C.IDCARDNO DESC";
                            DataTable dt = Utility.ExecuteSelectQuery(sql, "HREMPLOYDETAILS").Tables[0];
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                sName = dt.Rows[0]["EMPNAME"].ToString();

                            }
                            else
                            {
                                sql = ""; sName = "";
                                 sql = $"SELECT C.FNAME || '-' || E.MNNAME1 AS EMPNAME FROM GTCOMPMAST B JOIN HREMPLOYMAST C ON C.COMPCODE = B.GTCOMPMASTID JOIN HREMPLOYDETAILS D ON C.HREMPLOYMASTID = D.HREMPLOYMASTID JOIN GTDEPTDESGMAST E ON E.GTDEPTDESGMASTID = D.DEPTNAME WHERE D.IDACTIVE='YES' AND  D.MIDCARD='{sUserID.Trim()}'  ORDER BY C.IDCARDNO DESC";
                                DataTable dt1 = Utility.ExecuteSelectQuery(sql, "HREMPLOYDETAILS").Tables[0];
                                if (dt1 != null && dt1.Rows.Count > 0)
                                {
                                    sName = dt1.Rows[0]["EMPNAME"].ToString();
                                }
                            }
                            string[] row = { "", j.ToString(), sUserID, sName, idwFingerIndex.ToString(), "", sTmpData, sCardnumber, iPrivilege.ToString(), sPassword, bEnabled ? "True" : "False", iFlag.ToString(), comboMasterIp.Text };

                            LvDownload2.Items.Add(new ListViewItem(row));
                            Lvdownall.Items.Add(new ListViewItem(row));
                            j++;
                        }
                        else
                        {
                            lblattcount.Text = "Face Index not found";
                            lblattcount.Refresh();
                        }


                        axCZKEM1.EnableDevice(iMachineNumber, true);
                    }
                }
                else
                {
                    txtfactransferesearch.Focus(); Class.Users.UserTime = 0; Cursor = Cursors.Default;
                    MessageBox.Show("Please Enter IdCardNo");
                }
            }
            else
            {
                MessageBox.Show("Please select IP Address"); Class.Users.UserTime = 0; Cursor = Cursors.Default;
            }
            combo_ToIPload();
            Cursor = Cursors.Default;
            bIsConnected = false;

        }

        private void singleUserSearchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            butsingleuser.Visible = true;
        }

        private void cbUserId_Card_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;  // Block key
            }
        }

        private void txtcardfindex_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;  // Block key
            }
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                Class.Users.SessionID = 2;
                checkBox1.Text = Class.Users.SessionID.ToString()+ "  Index";
            }
            else
            {
                Class.Users.SessionID = 9;
                checkBox1.Text = Class.Users.SessionID.ToString() + "  Index";
            }
        }

        private void txtfactransferesearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;  // Block key
            }
        }
    }

}
