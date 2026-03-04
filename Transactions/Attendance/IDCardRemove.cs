using System;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Windows.Forms;


namespace Pinnacle.Transactions.Attendance
{
    public partial class IDCardRemove : Form,ToolStripAccess
    {
        private static IDCardRemove _instance;
        zkemkeeper.CZKEMClass axCZKEM1 = new zkemkeeper.CZKEMClass();
        public static IDCardRemove Instance
        {

            get
            {
                if (_instance == null)
                    _instance = new IDCardRemove();
                GlobalVariables.CurrentForm = _instance; return _instance;
            }

        }
        public IDCardRemove()
        {
            InitializeComponent();
            butheader.BackColor = Class.Users.BackColors;
        }
        ListView allip = new ListView();
        ListView allip1 = new ListView();
        ListView allip2 = new ListView();
        ListView removeuserid = new ListView(); UICO uti = new UICO();
        ListView listfilter = new ListView();
        private bool bIsConnected = false;//the boolean value identifies whether the device is connected
        private int iMachineNumber = 1;//the serial number of the device.After connecting the device ,this value will be changed.
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
        int dwMachineNumber, dwEnrollNumber, dwEMachineNumber,dwBackupNumber = 0;
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
        DataTable dtgridview = new DataTable();
        public void News()
        {
            dtgridview.Rows.Clear(); dtgridview.Columns.Clear();
            Class.Users.Intimation = "PAYROLL";
            Class.Users.UserTime = 0;
            combo_RemoveIPload();
        }

        public void Saves()
        {
          
        }

        public void Prints()
        {
            
        }

        public void Searchs()
        {
           
        }

        public void Searchs(int id)
        {
            
        }

        public void Deletes(int id)
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
            try
            {

                dtgridview.Rows.Clear(); dtgridview.Columns.Clear();
                int i = 0;
                System.Data.OleDb.OleDbConnection OledbConn;
                System.Data.OleDb.OleDbCommand OledbCmd;
                System.Data.OleDb.OleDbDataAdapter OledbAdapter;
                string filePath = string.Empty;
                string fileExt = string.Empty;
                OpenFileDialog file = new OpenFileDialog();
                string path = "";
                if (file.ShowDialog() == System.Windows.Forms.DialogResult.OK) //if there is a file choosen by the user  
                {
                    filePath = file.FileName; //get the path of the file  
                    fileExt = Path.GetExtension(filePath); //get the file extension  
                    if (fileExt.CompareTo(".xls") == 0)
                        

                    path = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=yourfile.xlsx;Extended Properties=`Excel 12.0 Xml; HDR = Yes`;"; //for below excel 2007  
                    else
                        path = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties=`Excel 12.0;HDR=NO`;"; //for above excel 2007  
                    OledbConn = new System.Data.OleDb.OleDbConnection(path);
                    OledbConn.Open();
                    string qry1 = "Select * from [Sheet1$]";
                    OledbAdapter = new OleDbDataAdapter(qry1, OledbConn);
                    OledbAdapter.Fill(dtgridview);
                    if (dtgridview.Rows.Count > 0)
                    {
                        dataGridView1.DataSource = dtgridview;

                    }

                }




            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
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
            News();
            GlobalVariables.MdiPanel.Show();

            GlobalVariables.HeaderName.Text = "";
            GlobalVariables.TabCtrl.TabPages.RemoveAt(GlobalVariables.TabCtrl.SelectedIndex);
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void butconnect_Click(object sender, EventArgs e)
        {
          
            try
            {
                Class.Users.Intimation = "PAYROLL";
                lblprogress1.Text = "";label1.Text = "";progressBar1.Maximum = 0;
                DialogResult result = MessageBox.Show("Do You want to Remove from Machine Finger Index??", "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (result.Equals(DialogResult.OK))
                {
                    bIsConnected = false;
         
                    Class.Users.UserTime = 0;
                    if (listremovechecklistip.Items.Count > 0 && dataGridView1.Rows.Count > 0)
                    {
                        iMachineNumber = 10; int iDataFlag = 5; int iBackupNumber = 10;
                        Cursor = Cursors.WaitCursor; bool va = false; int cnt = 0;
                        for (int j = 0; j < allip2.Items.Count; j++)
                        {
                            if (allip2.Items.Count > 0)
                            {
                                label1.Refresh(); label1.Refresh();
                                label1.Text = "Under Processing..." + allip2.Items[j].SubItems[1].Text;
                              
                                if (allip2.Items[j].SubItems[1].Text.Length > 0)
                                {

                                    progressBar1.Minimum = 0; axCZKEM1.EnableDevice(iMachineNumber, false);
                                    progressBar1.Maximum = dataGridView1.Rows.Count - 1; lblprogress1.Text = "";
                                    bIsConnected = axCZKEM1.Connect_Net(allip2.Items[j].SubItems[1].Text, 4370);

                                    if (bIsConnected == true)
                                    {
                                        label1.Refresh();
                                        label1.Text = "Connected..." + allip2.Items[j].SubItems[1].Text;
                                        decimal per = 0;
                                        for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                                        {
                                            idwErrorCode = 0;
                                            Class.Users.UserTime = 0;
                                            dwEnrollNumber = Convert.ToInt32("0" + dataGridView1.Rows[i].Cells[0].Value.ToString());
                                            sUserID = Convert.ToString(dataGridView1.Rows[i].Cells[0].Value.ToString());
                                            idwFingerIndex = Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value.ToString());
                                           
                                            dwMachineNumber = 10; dwMachineNumber = 10; dwBackupNumber = 12;
                                            // if (axCZKEM1.SSR_DeleteEnrollData(dwMachineNumber, sUserID, dwBackupNumber))
                                            //{
                                            //axCZKEM1.RefreshData(iMachineNumber);
                                            bool res = axCZKEM1.SSR_DeleteEnrollData(iMachineNumber, sUserID, 12);
                                            per = Convert.ToDecimal(100 / GenFun.ToDecimal(dataGridView1.Rows.Count - 1)) * (i + 1);
                                                lblprogress1.Text = per.ToString("N0") + " %" + "ID Card No:-" + sUserID;
                                                va = true;
                                                lblprogress1.Refresh();
                                                progressBar1.Value = i + 1;
                                                cnt = +1;

                                            //}
                                            //else
                                            //{
                                            //    per = Convert.ToDecimal(100 / GenFun.ToDecimal(dataGridView1.Rows.Count - 1)) * (i + 1);
                                            //    lblprogress1.Text = per.ToString("N0") + " %" + "ID Card No:-" + sUserID;
                                            //    va = true;
                                            //    lblprogress1.Refresh();
                                            //    progressBar1.Value = i + 1;
                                            //    cnt = +1;
                                            //    label1.Refresh();
                                            //    label1.Text = "Invalid IDCard"+ sUserID;
                                            //}

                                        }


                                    }
                                    else
                                    {
                                        label1.Refresh();
                                        label1.Text = "Invalid..." + allip2.Items[j].SubItems[1].Text;

                                        Cursor = Cursors.Default;
                                    }

                                }

                            }
                            else
                            {
                                label1.Refresh(); label1.Refresh();
                                label1.Text = "Invalid IP..." + allip2.Items[j].SubItems[1].Text;

                                Cursor = Cursors.Default;
                                MessageBox.Show("Invalid");
                            }
                        }
                        if (allip2.Items.Count == 0)
                        {
                            Cursor = Cursors.Default;
                            label1.Refresh();
                            label1.Text = "IP List doesn't Connected..";
                            MessageBox.Show("IP list is empty.pls select any Ip from Listview", "Information", MessageBoxButtons.OK, MessageBoxIcon.Question);
                            return;
                        }
                        axCZKEM1.EnableDevice(iMachineNumber, true);

                        if (bIsConnected == true)
                        {
                            axCZKEM1.RefreshData(iMachineNumber);//the data in the device should be refreshed }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid", "Information", MessageBoxButtons.OK, MessageBoxIcon.Question);

                    }
                    Cursor = Cursors.Default;
                    label1.Refresh();
                    label1.Text = "Deleted Completed..";
                    allip2.Items.Clear();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void listviewchecklistip_ItemActivate(object sender, EventArgs e)
        {
           
        }
        //private void combo_ToIPload()
        //{
        //    listviewchecklistip1.Items.Clear();
        //    listviewchecklistip2.Items.Clear();
        //    string ss = Class.Users.HCompcode;
        //    DataTable dt1 = dev.AllIp(ss, comboMasterIp.Text);

        //    if (dt1.Rows.Count > 0)
        //    {
        //        iIndex = 1;
        //        foreach (DataRow row in dt1.Rows)
        //        {

        //            ListViewItem list = new ListViewItem();
        //            ListViewItem list2 = new ListViewItem();
        //            list.SubItems.Add(row["MACIP"].ToString());
        //            list.SubItems.Add("------");


        //            ListViewItem listremove = new ListViewItem();


        //            listremove.SubItems.Add(row["MACIP"].ToString());
        //            listremove.SubItems.Add("------");


        //            ListViewItem listcard = new ListViewItem();
        //            listcard.SubItems.Add(row["MACIP"].ToString());
        //            listcard.SubItems.Add("------");


        //            ListViewItem listface = new ListViewItem();
        //            listface.SubItems.Add(row["MACIP"].ToString());
        //            listface.SubItems.Add("------");

        //            if (iIndex % 2 == 0)
        //            {
        //                list.BackColor = Color.White;
        //                listremove.BackColor = Color.White;
        //                listcard.BackColor = Color.White;
        //                listface.BackColor = Color.White;
        //            }
        //            else
        //            {
        //                list.BackColor = Color.WhiteSmoke;
        //                listremove.BackColor = Color.WhiteSmoke;
        //                listcard.BackColor = Color.WhiteSmoke;
        //                listface.BackColor = Color.WhiteSmoke;

        //            }
        //            listremovechecklistip.Items.Add(list);
        //            listremovechecklistip.Items.Add(listremove);
        //            listviewchecklistip1.Items.Add(listcard);
        //            listviewchecklistip2.Items.Add(listface);
        //            iIndex++;
        //        }
        //        ListViewItem listremove1 = new ListViewItem();
        //        if (comboMasterIp.Text != "")
        //        {
        //            listremove1.SubItems.Add(comboMasterIp.Text);
        //            listremove1.SubItems.Add("Connected");
        //            listremove1.SubItems.Add("True");
        //            allip2.Items.Add(listremove1);
        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show("No data found", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}
        Models.Device dev = new Models.Device();
        private void combo_RemoveIPload()
        {
            listremovechecklistip.Items.Clear();
            string ss = Class.Users.HCompcode;
            //string sel = "SELECT distinct B.MACIP FROM HRMACIPENTRY A JOIN HRMACIPENTRYDET B ON A.HRMACIPENTRYID = B.HRMACIPENTRYID JOIN GTCOMPMAST C ON C.GTCOMPMASTID = A.COMPCODE  WHERE B.DEFAULTYN = 'NO'  and c.compcode='" + Class.Users.HCompcode + "' ORDER BY 1";
            //DataSet ds = Utility.ExecuteSelectQuery(sel, "HRMACIPENTRY");
            //DataTable dt1 = ds.Tables["HRMACIPENTRY"];
            DataTable dt1 = dev.ToIp(ss);
            if (dt1.Rows.Count >= 0)
            {
                Class.Users.Intimation = "PAYROLL";
                ListViewItem listremove1 = new ListViewItem(); iIndex = 1;
                foreach (DataRow row in dt1.Rows)
                {
                    ListViewItem listremove = new ListViewItem();
                    listremove.SubItems.Add(row["MACIP"].ToString());
                    listremove.SubItems.Add("------");
                    listremovechecklistip.Items.Add(listremove);
                    if (iIndex % 2 == 0)
                    {
                        listremove.BackColor = Color.White;

                    }
                    else
                    {
                        listremove.BackColor = Color.WhiteSmoke;

                    }

                    iIndex++;
                }

            }
            else
            {
                MessageBox.Show("No data found", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void listviewchecklistip_ItemCheck(object sender, ItemCheckEventArgs e)
        {
           
        }

        private void listremovechecklistip_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            try
            {
                if (listremovechecklistip.Items.Count >= 0)
                {
                    Class.Users.UserTime = 0; label1.Text = "";
                    Class.Users.Intimation = "PAYROLL";
                    ListViewItem itt = new ListViewItem();

                    if (e.Item.Checked == true)
                    {

                        Cursor = Cursors.WaitCursor;
                        Class.Users.UserTime = 0;
                        Ping ping = new Ping();
                        PingReply reply = ping.Send(e.Item.SubItems[1].Text, 1000);
                        if (reply.Status.ToString() == "Success")
                        {
                            bIsConnected = true; e.Item.SubItems[2].Text = "Connected";
                        }
                        else { bIsConnected = false; e.Item.SubItems[2].Text = "DisConnected"; }
                        if (bIsConnected == true)
                        {


                            itt.SubItems.Add(e.Item.SubItems[1].Text);
                            itt.SubItems.Add(e.Item.SubItems[2].Text);
                            itt.SubItems.Add(e.Item.Checked.ToString());
                            allip2.Items.Add(itt);

                        }
                        else
                        {
                            MessageBox.Show("This IP   :" + e.Item.SubItems[1].Text + "     Not a BioMetric Machine.");
                        }
                        Cursor = Cursors.Default;
                    }
                    if (e.Item.Checked == false && e.Item.SubItems[2].Text == "Connected")
                    {
                        Cursor = Cursors.WaitCursor;
                        bIsConnected = false;


                        e.Item.SubItems[2].Text = "DisConnected";
                        //itt.SubItems.Add(e.Item.SubItems[1].Text);
                        //itt.SubItems.Add(e.Item.SubItems[2].Text);
                        //itt.SubItems.Add(e.Item.Checked.ToString());
                        //allip2.Items.Add(itt);
                        for (int c = 0; c < allip2.Items.Count; c++)
                        {
                            if (listremovechecklistip.SelectedItems[0].SubItems[1].Text == allip2.Items[c].SubItems[1].Text)
                            {
                                allip2.Items[c].Remove();
                                c--;
                            }
                        }
                        Cursor = Cursors.Default;
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("       ---  " + e.Item.ToString() + "             ======================" + ex.ToString()); }
        }

        public void GridLoad()
        {
            
        }
    }
}
