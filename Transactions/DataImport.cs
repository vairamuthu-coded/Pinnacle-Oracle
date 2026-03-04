using System;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Pinnacle.Transactions
{
    public partial class DataImport : Form, ToolStripAccess
    {
        private static DataImport _instance;
        ListView listfilterdb = new ListView(); ListView listfilterslug = new ListView(); ListView listfilterexcel = new ListView();
        ListView tofilter = new ListView(); ListView allip3 = new ListView(); ListView allislug = new ListView(); ListView allip4 = new ListView();
        ListView COLUMNORDER = new ListView();
        string fromtable = ""; string fromslugtale = ""; string fromgridtotable = "";
        string totable = ""; string totablegrid = ""; string totableselect = ""; string totableupdate = ""; string totableupdategrid = ""; string totableid = "";
        int iIndex = 0; int i = 0; int j = 1; string update1 = null;
        Models.DataImportModel daimport = new Models.DataImportModel();
        int tablecount = -1; int tablecountslug = -1; string lastFive = ""; string grid = ""; string Details = ""; string Details1 = "";
        public DataImport()
        {
            InitializeComponent();
            Class.Users.IPADDRESS = GenFun.GetLocalIPAddress();
            string MODIFIED_BY = ", MODIFIED_BY ";
            string MODIFIED_ON = " MODIFIED_ON  ";
            string CREATED_BY = " CREATED_BY  ";
            string CREATED_ON = " CREATED_ON  ";
            string USERID = " USERID  ";
            string PROJECTID = " PROJECTID ";
            string IPADD = " IPADD ";

            string MODIFIED_BY1 = ",'" + Class.Users.HUserName + "'";
            string MODIFIED_ON1 = "TO_DATE('" + System.DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss") + "','DD-MM-YYYY hh:mi:ss')";
            string CREATED_BY1 = "'" + Class.Users.HUserName + "'";
            string CREATED_ON1 = "TO_DATE('" + System.DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss") + "','DD-MM-YYYY hh:mi:ss')";
            string USERID1 = "'" + Class.Users.USERID + "',";
            string PROJECTID1 = "'" + Class.Users.ProjectID + "',";
            string IPADD1 = "'" + Class.Users.IPADDRESS + "'";
            Details = MODIFIED_BY + "," + MODIFIED_ON + "," + CREATED_BY + "," + CREATED_ON + "," + USERID + "," + PROJECTID + "," + IPADD;
            Details1 = MODIFIED_BY1 + "," + MODIFIED_ON1 + "," + CREATED_BY1 + "," + CREATED_ON1 + "," + USERID1 + "" + PROJECTID1 + "" + IPADD1;
           
        }
        protected override bool ProcessCmdKey(ref Message message, Keys keys)
        {
            switch (keys)
            {
                case Keys.N | Keys.Control:
                    // ... Process Shift+Ctrl+Alt+B ...
                    News();
                    return true; // signal that we've processed this key
                case Keys.S | Keys.Control:
                    // ... Process Shift+Ctrl+Alt+B ...
                    Saves();
                    return true; // signal that we've processed this key
                case Keys.I | Keys.Control:
                    // ... Process Shift+Ctrl+Alt+B ...
                    Imports();
                    return true; // signal that we've processed this key
                case Keys.D | Keys.Control:
                    // ... Process Shift+Ctrl+Alt+B ...
                    DownLoads();
                    return true; // signal that we've processed this key
                
            }
            // run base implementation
            return base.ProcessCmdKey(ref message, keys);
        }
        public static DataImport Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new DataImport();
                GlobalVariables.CurrentForm = _instance;
                return _instance;

            }
        }
        private void DataImport_Load(object sender, EventArgs e)
        {
           
            lbldatabase.Text = Class.Users.DataBase;
            GridLoad(); GridLoad1();
            //   lbldatabase1.Text = Class.Users.DataBase;
        }
      
        private void slugTable(string d, string f, string p)
        {
            textMessagebox.Refresh();
            textMessagebox.Text = "SlugTable Function is Running...";
            if (Class.Users.TableName != "" || Class.Users.TableName != null)
            {
                Cursor.Current = Cursors.WaitCursor; 
                Class.Users.Description = ""; Class.Users.FieldName = ""; Class.Users.Prefix = "";
                Class.Users.Sequenceno = 0;
                dttbl2 = null; allislug.Items.Clear();

                if (d == "" && f == "" && p == "")
                {
                    DataTable dt2 = daimport.Maximumid(Class.Users.ProjectID, Class.Users.TableName, dataGridView2.Rows[0].Cells[2].FormattedValue.ToString(), dataGridView2.Rows[0].Cells[3].FormattedValue.ToString(), dataGridView2.Rows[0].Cells[3].FormattedValue.ToString());
                    if (Convert.ToInt64(dt2.Rows[0][Class.Users.TableName + "ID"].ToString())<=0 || dt2==null)
                    {
                        //if (Convert.ToInt64("0" + dt2.Rows[0][Class.Users.TableName + "ID"].ToString()) <= 0)
                        //{
                            Class.Users.Sequenceno = 14;
                        //}
                    }
                    else
                    {
                        dttbl2 = daimport.SlugData1(Class.Users.TableNameGrid, Class.Users.ProjectID);
                        Class.Users.Sequenceno = Convert.ToDouble(dt2.Rows[0]["LASTNO"].ToString());
                    }
                    dataGridView3.Rows[0].Cells[5].Value = Class.Users.Sequenceno.ToString();
                    Class.Users.Bisconnectclear = true;
                }
                if (d != "" && f != "" && p != "" && Class.Users.TableNameGrid != "" && Class.Users.TableNameGrid != null)
                {
                    dttbl2 = daimport.SlugData(Class.Users.ProjectID, Class.Users.TableNameGrid, d, f, p);
                    Class.Users.Sequenceno = Convert.ToDouble("0" + dttbl2.Rows[0]["LASTNO"].ToString());
                    DataTable MAXID = daimport.Maximumid(Class.Users.ProjectID, Class.Users.TableName, dataGridView2.Rows[0].Cells[2].FormattedValue.ToString(), dataGridView2.Rows[0].Cells[3].FormattedValue.ToString(), dataGridView2.Rows[0].Cells[3].FormattedValue.ToString());
                    if (MAXID.Rows[0][Class.Users.TableName + "ID"].ToString() != "")
                    {
                        if (Convert.ToInt64("0" + MAXID.Rows[0][Class.Users.TableName + "ID"].ToString()) > Class.Users.Sequenceno)
                        {
                            daimport.query = "update " + Class.Users.ProjectID + ".autogenerate A set A.LASTNO='" + MAXID.Rows[0][Class.Users.TableName + "ID"].ToString() + "' WHERE A.TX_VIEW_ID='" + Class.Users.TableNameGrid + "'  AND  A.DESCRIPTION='" + d + "' AND  A.FIELDNAME='" + f + "'  AND A.PREFIX='" + p + "'  AND A.PROJECTID='" + Class.Users.ProjectID + "'";// AND A.PROJECTID='" + Class.Users.ProjectID + "' AND " + Class.Users.TableNameGrid + " AND PREFIX='" + Class.Users.Prefix + "'";
                            Utility.ExecuteNonQuery(daimport.query);
                            dttbl2 = daimport.SlugData(Class.Users.ProjectID, Class.Users.TableNameGrid, d, f, p);
                        }
                    }
                  
                    Class.Users.Description = dttbl2.Rows[0]["DESCRIPTION"].ToString();
                    Class.Users.FieldName = dttbl2.Rows[0]["FIELDNAME"].ToString();
                    Class.Users.Prefix = dttbl2.Rows[0]["PREFIX"].ToString();
                    Class.Users.Sequenceno = Convert.ToDouble("0" + dttbl2.Rows[0]["LASTNO"].ToString());
                    lblmaxid.Text = Convert.ToInt64("0" + MAXID.Rows[0][Class.Users.TableName + "ID"].ToString()) + " Sequence NO : " + Class.Users.Sequenceno;

                    if (Class.Users.Sequenceno == 0)
                    {
                        DataTable dt2 = daimport.TableMax(Class.Users.TableName, Class.Users.ProjectID);
                        if (dt2.Rows[0]["LASTNO"].ToString() == "") { Class.Users.Sequenceno = 1; } else { Class.Users.Sequenceno = Convert.ToDouble(dt2.Rows[0]["LASTNO"].ToString()); }
                        dataGridView3.Rows[0].Cells[5].Value = Convert.ToDouble("0" + Class.Users.Sequenceno);
                    }
                    else
                    {
                        dataGridView3.Rows[0].Cells[5].Value = Convert.ToDouble("0" + dttbl2.Rows[0]["LASTNO"].ToString());
                    }
                    dataGridView3.Rows[0].Cells[6].Value = dttbl2.Rows[0]["ACTIVESEQUENCE"].ToString();
                    dataGridView3.Rows[0].Cells[7].Value = Class.Users.HUserName;
                    dataGridView3.Rows[0].Cells[8].Value = Class.Users.ProjectID;
                    dataGridView3.Rows[0].Cells[9].Value = dttbl2.Rows[0]["DUPLICATECTRL"].ToString();
                    dataGridView3.Rows[0].Cells[10].Value = dttbl2.Rows[0]["ZEROPADDING"].ToString();
                }

                if (dttbl2.Columns.Count > 0 || Class.Users.Sequenceno.ToString() != "")
                {
                    if (fromgridtotable == "" || fromgridtotable == null)
                    {
                        for (int j = 0; j < dttbl2.Columns.Count; j++)
                        {
                            allislug.Items.Add(dttbl2.Columns[j].ToString());
                            if (fromgridtotable == "")
                            {
                                fromgridtotable = dttbl2.Columns[j].ToString();
                            }
                            else
                            {
                                fromgridtotable += "," + dttbl2.Columns[j].ToString();
                            }
                           
                        }
                       
                    }
                }
                else
                {
                    Cursor = Cursors.Default;
                    MessageBox.Show("Slug Name Not Found .Parameters are invalid", dataGridView3.Columns[2].HeaderText + "=" + dataGridView3.Rows[0].Cells[2].Value + "==" + dataGridView3.Columns[3].HeaderText + "=" + dataGridView3.Rows[0].Cells[3].Value + "===" + dataGridView3.Columns[4].HeaderText + "=" + dataGridView3.Rows[0].Cells[4].Value + "  Invalid  ", MessageBoxButtons.OK, MessageBoxIcon.Information); ;

                }               
            }
            else
            {
                Cursor = Cursors.Default;
                MessageBox.Show("Pls Select Table Name  ","Invalid", MessageBoxButtons.OK, MessageBoxIcon.Information); ;
              
            }
            Cursor = Cursors.Default;
        }
      
        public void GridLoad1()
        {
            try
            {
                listView2.Items.Clear(); listfilterslug.Items.Clear(); int r = 1;
                string sel2 = "SELECT distinct A.TX_VIEW_ID  FROM " + Class.Users.ProjectID + ".AUTOGENERATE A where  A.PROJECTID='" + Class.Users.ProjectID + "' ORDER BY 1";
                DataSet ds2 = Utility.ExecuteSelectQuery(sel2, "AUTOGENERATE");
                DataTable dt2 = ds2.Tables["AUTOGENERATE"];

                int i = 0;
                foreach (DataRow myRow in dt2.Rows)
                {

                    ListViewItem list1 = new ListViewItem();                   
                    list1.Text = "";
                    list1.SubItems.Add(r.ToString());
                    list1.SubItems.Add(myRow["TX_VIEW_ID"].ToString());
                    list1.SubItems.Add("✖");
                    if (i % 2 == 0)
                    {
                        list1.BackColor = Color.White;
                    }
                    else
                    {
                        list1.BackColor = Color.WhiteSmoke;
                    }

                    this.listfilterslug.Items.Add((ListViewItem)list1.Clone());


                    listView2.Items.Add(list1);
                    i++;
                    r++;
                  

                }
                lblslug.Text = " Total Slug Count   :" + listView2.Items.Count;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public void GridLoad()
        {
            try
            {
                lvlogs.Items.Clear(); listfilterdb.Items.Clear(); int r = 1; lastFive = "";
                string sel1 = "select ' ' TABLE_NAME from  dual union all select A.TABLE_NAME from all_tables a where  SUBSTR(A.TABLE_NAME,0,1) NOT IN '$' and A.OWNER='" + Class.Users.ProjectID + "'  order by 1";
                DataSet ds = Utility.ExecuteSelectQuery(sel1, "all_tables");
                DataTable dt = ds.Tables["all_tables"];
                int i = 1;
                foreach (DataRow myRow in dt.Rows)
                {
                    ListViewItem list = new ListViewItem();
                    if (Convert.ToInt32(myRow["TABLE_NAME"].ToString().Length.ToString()) > 8)
                    {
                        var ID = myRow["TABLE_NAME"].ToString();
                        lastFive = ID.Substring(Convert.ToInt32(myRow["TABLE_NAME"].ToString().Length.ToString()) - 7); //myRow["TABLE_NAME"].ToString().Length.ToString().Substring(L - 5); // => "Tabs1"
                    }
                    if (lastFive == "ARCHIVE")
                    {
                    }
                    else
                    {
                        TableName.Items.Add(myRow["TABLE_NAME"].ToString());
                        list.Text = "";
                        list.SubItems.Add(r.ToString());
                        list.SubItems.Add(myRow["TABLE_NAME"].ToString());
                        list.SubItems.Add("✖");
                        if (r % 2 == 0)
                        {
                            list.BackColor = Color.White;
                        }
                        else
                        {
                            list.BackColor = Color.WhiteSmoke;
                        }
                        this.listfilterdb.Items.Add((ListViewItem)list.Clone());
                        lvlogs.Items.Add(list);
                        r++;
                    }

                }
                lblemptot.Text = " Total Table Count   :" + lvlogs.Items.Count;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        DataTable dttbl2 = new DataTable();
        public void TableGridLoad(string s)
        {
            try
            {
                textMessagebox.Refresh();
                textMessagebox.Text = "Grid Data Binding is Running...";
                if (s != "" || s==null)
                {
                    dttbl2.Columns.Clear();
                    dttbl2.Rows.Clear();
                    daimport.query = null;
                    daimport.query = "select * from " + Class.Users.ProjectID + "." + s + " ORDER BY 1";
                    daimport.ds = Utility.ExecuteSelectQuery(daimport.query, s);
                    dttbl2 = daimport.ds.Tables[s];
                  
                    TableColumn.DataSource = null; int j=0;
                    if (dttbl2.Columns.Count > 0)
                    {
                        for (j = 0; j < dttbl2.Columns.Count; j++)
                        {
                            textMessagebox.Refresh();
                            textMessagebox.Text = "Columns Added in Grid : " + dttbl2.Columns[j].ToString();

                            if (TableColumn.Items.Contains(dttbl2.Columns[j].ToString())) { }
                            else
                            {
                                TableColumn.Items.Add(dttbl2.Columns[j].ToString());
                                
                            }
                        }
                    }
                    else
                    {
                        for (j = 0; j < dttbl2.Columns.Count; j++)
                        {
                            textMessagebox.Refresh();
                            textMessagebox.Text = "Column Added in Grid : " + dttbl2.Columns[j].ToString();
                            if (TableColumn.Items.Contains(dttbl2.Columns[j].ToString())) { }
                            else
                            {
                                TableColumn.Items.Add(dttbl2.Columns[j].ToString());
                            }
                        }
                    }
                }
                dbcol = false;
            }
            catch (Exception EX) { }
        }


        public void News()
        {

            textMessagebox.Text = "";
            tablecount = -1; txtsearchtable.Text = ""; txtsearch.Text = ""; Class.Users.Bisconnectclear = false;
            totable = ""; totableselect = ""; totableupdate = ""; totableid = ""; chkall.Checked = false;
            /*listView1.Items.Clear();*/ lvlogs.Items.Clear(); lblprocessbar.Text = ""; lblprocessbar.Refresh();
            fromtable = ""; allip3.Items.Clear(); lbltablename1.Text = ""; lbltablename.Text = "";
            allislug.Items.Clear(); tablecountslug = -1; txtslugsearch.Text = "";
            allip3.Items.Clear(); totablegrid = "";
            if (dataGridView2.Rows.Count > 1)
            {
                daimport.GridRowRemove(dataGridView1);
               daimport.GridRowRemove(dataGridView2);
               
            }
            

            fromtable = ""; totable = ""; totableupdate = ""; totableselect = ""; TableColumn.Items.Clear(); TableName.Items.Clear();dataGridView3.Rows[0].Cells[1].Value = "";
            progressBar1.Value = 0; lblprocessbar.Text = ""; 
            Class.Users.TableName = null; Class.Users.TableNameGrid = null; Class.Users.TableName = ""; Class.Users.TableNameGrid = ""; 
            Class.Users.TableNameSubGrid = null; Class.Users.Prefix = null; Class.Users.Prefix = null;
            Class.Users.Description = null; Class.Users.Description = null;
            Class.Users.FieldName = null; Class.Users.FieldName = null;
            progressBar1.Minimum = 0; lblprocessbar.Text = ""; GridLoad(); GridLoad1();
            butheader.BackColor = Class.Users.BackColors;
            this.BackColor = Class.Users.BackColors;
            panel2.BackColor = Class.Users.BackColors;
            panel3.BackColor = Class.Users.BackColors;         
            panel4.BackColor = Class.Users.BackColors;
            panel6.BackColor = Class.Users.BackColors;
            dataGridView2.ColumnHeadersDefaultCellStyle.BackColor= Class.Users.BackColors;
            dataGridView3.ColumnHeadersDefaultCellStyle.BackColor = Class.Users.BackColors;
            panel1.BackColor = Class.Users.BackColors;
            
               
            
        }

        public void Saves()
        {

        }

        public void Prints()
        {

            ////string sel = "SELECT  DISTINCT  A.AUTOGENERATEID,A.TX_VIEW_ID  FROM " + Class.Users.ProjectID + ".AUTOGENERATE A  WHERE A.TX_VIEW_ID='" + Class.Users.TableNameGrid + "' AND A.PROJECTID='" + Class.Users.ProjectID + "' ";
            ////DataSet ds = Utility.ExecuteSelectQuery(sel, Class.Users.TableNameGrid);
            ////DataTable dt3 = ds.Tables[Class.Users.TableNameGrid];
            ////if (dt3.Rows.Count > 0)
            ////{
            ////    string sel1 = "select to_char(count(*)) as total  from PSSDEMO." + Class.Users.TableName;
            ////    DataSet ds1 = Utility.ExecuteSelectQuery(sel1, Class.Users.TableName);
            ////    DataTable dt1 = ds1.Tables[Class.Users.TableName];

            ////    string ins = "update " + Class.Users.ProjectID + ".autogenerate set TX_VIEW_ID='" + dt3.Rows[0]["TX_VIEW_ID"].ToString() + "',LASTNO='" + dt1.Rows[0]["total"].ToString() + "' where AUTOGENERATEID=" + dt3.Rows[0]["AUTOGENERATEID"].ToString();
            ////    Utility.ExecuteNonQuery(ins);


            ////}


        }
        public void Searchs()
        {

        }

        public void Deletes()
        {

        }

        public void Imports()
        {
            int l = 0; int k = 0; string sel = ""; int cnt = 0; Class.Users.Bisconnectclear = false;
            try
            {
              
                progressBar1.Visible = true; fromgridtotable = ""; Class.Users.Sequenceno = 0;
                lblprocessbar.Visible = true; string nongridid = ""; string nongridid1 = ""; string gridid = ""; string gridid1 = "";
                cnt = 0; int p = 0; int q = 0; update1 = "";
                int colcount = dataGridView1.Rows.Count; bool savefalse = false; Class.Users.UserTime = 0;
                if (Class.Users.TableName != "")
                {
                    if (dataGridView2.Rows.Count > 0)
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        double sequence = 0; string sequence1 = "";
                        slugTable(dataGridView3.Rows[0].Cells[2].FormattedValue.ToString(), dataGridView3.Rows[0].Cells[3].FormattedValue.ToString(), dataGridView3.Rows[0].Cells[4].FormattedValue.ToString());
                        if (colcount > 0)
                        {
                            if (fromtable == "" || fromtable == null)
                            {
                              
                                foreach (DataGridViewRow item in dataGridView2.Rows)
                                {
                                    if (fromtable == "")
                                    {
                                        fromtable = item.Cells[3].FormattedValue.ToString();                                      
                                    }
                                    else
                                    {
                                        fromtable += "," + item.Cells[3].FormattedValue.ToString();                                       
                                    }
                                    i++;
                                   
                                }
                            }

                            if (Class.Users.TableName != null)
                            {
                               
                                DataTable dtseq = daimport.FindSlug(Class.Users.TableName);
                                if (Convert.ToInt32("0" + dtseq.Rows[0]["CNT"].ToString()) == 0 || Class.Users.Bisconnectclear == true)
                                {
                                    if (Class.Users.TableNameGrid != "")
                                    {                                       
                                        DataTable dttbl2 = daimport.SlugData(Class.Users.ProjectID, Class.Users.TableNameGrid, Class.Users.Description, Class.Users.FieldName, Class.Users.Prefix);
                                        if (dttbl2.Rows[0]["LASTNO"].ToString() == "") { Class.Users.Sequenceno = 14; }
                                        else
                                        {
                                            Class.Users.Sequenceno = Convert.ToDouble(dttbl2.Rows[0]["LASTNO"].ToString());
                                        }
                                        sequence = Convert.ToInt64(dttbl2.Rows[0]["ZEROPADDING"].ToString());
                                        textMessagebox.Refresh();
                                        textMessagebox.Text = "Sequence No" + sequence;

                                    }
                                    else
                                    {
                                        sequence = Class.Users.Sequenceno;
                                        textMessagebox.Refresh();
                                        textMessagebox.Text = "Sequence No" + sequence;
                                    }
                                    if (Convert.ToDouble(sequence) >= 1 && Class.Users.Sequenceno == 14)
                                    {
                                        switch (sequence)
                                        {
                                            case 1:
                                                sequence1 = "1";
                                                break;
                                            case 2:

                                                sequence1 = "1";
                                                break;
                                            case 3:

                                                sequence1 = "101";
                                                break;
                                            case 4:

                                                sequence1 = "1001";
                                                break;
                                            case 5:
                                                sequence1 = "10001";
                                                break;
                                            case 6:
                                                sequence1 = "100001";
                                                break;
                                            case 7:
                                                sequence1 = "1000001";
                                                break;
                                            case 8:
                                                sequence1 = "10000001";
                                                break;
                                            case 9:

                                                sequence1 = "100000001";
                                                break;
                                            case 10:

                                                sequence1 = "1000000001";
                                                break;
                                            case 11:
                                                sequence1 = "10000000001";
                                                break;
                                            case 12:
                                                sequence1 = "100000000001";
                                                break;
                                            case 13:
                                                sequence1 = "1000000000001";
                                                break;
                                            case 14:
                                                sequence1 = "2000000000001";
                                                break;
                                            default:
                                                sequence1 = "0";
                                                break;
                                        }
                                        Class.Users.Sequenceno = Convert.ToDouble(sequence1);
                                    }                                   
                                }
                                if (Convert.ToInt32("0" + dtseq.Rows[0]["CNT"].ToString()) > 0)
                                {
                                    daimport.dropsequence(Class.Users.ProjectID, Class.Users.TableName);
                                    textMessagebox.Refresh(); lblmaxid.Refresh();
                                    textMessagebox.Text="Droped Sequence";
                                 
                                        daimport.CreateSequence(Class.Users.ProjectID, Class.Users.TableName, Class.Users.Sequenceno);
                                 
                                        textMessagebox.Text = "Created Sequence" + Class.Users.ProjectID + Class.Users.TableName + "SEQ" + " Sequence No :" + Class.Users.Sequenceno;
                                   
                                }
                                if (Convert.ToInt32("0" + dtseq.Rows[0]["CNT"].ToString()) == 0)
                                {
                                    textMessagebox.Refresh(); lblmaxid.Refresh();
                                  
                                        daimport.CreateSequence(Class.Users.ProjectID, Class.Users.TableName, Class.Users.Sequenceno);
                                  
                                    textMessagebox.Text = "Created Sequence" + Class.Users.ProjectID + Class.Users.TableName + "SEQ"+ " Sequence No :"+ Class.Users.Sequenceno;
                                    
                                }
                                DataTable dtseq0 = daimport.FindTrigger(Class.Users.TableName, Class.Users.ProjectID);
                                if (dtseq0 == null || dtseq0.Rows[0]["CNT"].ToString() == "0")
                                {
                                    textMessagebox.Refresh();
                                    daimport.CreateTrigger(Class.Users.TableName, Class.Users.ProjectID);
                                    textMessagebox.Refresh();
                                    textMessagebox.Text = "Created Trigger"+ Class.Users.ProjectID+ Class.Users.TableName+"TRI";
                                }
                            }

                            l = 0; int n = 0;
                            //progressBar1.Minimum = 0;
                            i = 0; DataTable dt1 = new DataTable();
                            //progressBar1.Maximum = dataGridView1.Rows.Count;
                            dt1.Rows.Clear(); k = 0; l = 0;int totcnt = dataGridView1.Rows.Count;
                            for (k = 0; k < dataGridView1.Rows.Count; k++)
                            {
                                Class.Users.UserTime = 0;
                                if (fromtable != "")
                                {
                                    for (l = 0; l < dataGridView1.Columns.Count; l++)
                                    {
                                        Cursor.Current = Cursors.WaitCursor;
                                        Class.Users.UserTime = 0;
                                        if (Class.Users.TableName != null)
                                        {
                                            if (totable == "" && Class.Users.TableName != null)
                                            {
                                                totable = "'" + dataGridView1.Rows[k].Cells[l].Value + "'";
                                                if (dataGridView2.Rows[0].Cells[5].FormattedValue.ToString().Trim() == "YES")
                                                {
                                                    DataTable dt2 = daimport.GridSelect(dataGridView2.Rows[l].Cells[2].FormattedValue.ToString().Trim(), dataGridView2.Rows[l].Cells[4].FormattedValue.ToString().Trim(), dataGridView1.Rows[k].Cells[l].FormattedValue.ToString().Trim());
                                                    if (dt2 == null || Convert.ToInt32(dt2.Rows.Count) <= 0)
                                                    {
                                                        textMessagebox.Refresh();
                                                        textMessagebox.Text = "No Data Found this Table   :'" + dataGridView2.Rows[l].Cells[2].FormattedValue + "' 's      " + " Field Name is " + dataGridView2.Rows[l].Cells[4].FormattedValue + "='" + dataGridView1.Rows[k].Cells[l].Value + "  " + "ROW  :  " + k.ToString() + "  COLUMN  :" + l.ToString() + "   Insert Query    : " + sel;
                                                       // MessageBox.Show("No Data Found this Table   :'" + dataGridView2.Rows[l].Cells[2].FormattedValue + "' 's      " + " Field Name is " + dataGridView2.Rows[l].Cells[4].FormattedValue + "='" + dataGridView1.Rows[k].Cells[l].Value + "  " + "ROW  :  " + k.ToString() + "  COLUMN  :" + l.ToString() + "   Insert Query    : " + sel + "    Excel  Data Invalid", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                        cnt = 0; colcount = 0; totable = ""; totableupdate = "";
                                                        cnt = k + 1; l = dataGridView1.Columns.Count;
                                                    }
                                                    if (dt2.Rows.Count >= 1)
                                                    {
                                                        string c = dt2.Rows[0][dataGridView2.Rows[l].Cells[2].FormattedValue + "ID"].ToString();
                                                        totable = "'" + c + "'";
                                                        nongridid = "  where  ";
                                                        nongridid1 = dataGridView2.Rows[l].Cells[3].FormattedValue.ToString();
                                                        totableupdate += " where " + dataGridView2.Rows[l].Cells[3].FormattedValue.ToString() + "='" + c + "'";
                                                       
                                                    }
                                                }
                                                if (dataGridView2.Rows[0].Cells[5].FormattedValue.ToString().Trim() == "")
                                                {
                                                    nongridid = "  where  ";
                                                    nongridid1 = dataGridView2.Rows[l].Cells[3].FormattedValue.ToString();
                                                    totableupdate += " where " + dataGridView2.Rows[l].Cells[4].FormattedValue.ToString() + "='" + dataGridView1.Rows[k].Cells[l].Value + "'";
                                                }
                                            }
                                            else
                                            {
                                                if (Class.Users.TableName != null)
                                                {
                                                    if (dataGridView1.Rows[k].Cells[l].Value.ToString().Trim().Length >= 10)
                                                    {                                                       
                                                        if (dataGridView1.Rows[k].Cells[l].Value.ToString().Substring(2, 1) == "&")//|| dataGridView1.Rows[k].Cells[l].Value.ToString().Substring(2, 1) == "/"
                                                        {
                                                            string Dates = dataGridView1.Rows[k].Cells[l].Value.ToString().Replace("&","-");
                                                            totable += ",to_date('" + Dates.Substring(0, 10) + "','dd-MM-yyyy')";
                                                        }
                                                        else
                                                        {
                                                            if (dataGridView2.Rows[l].Cells[2].FormattedValue.ToString().Trim() != "")// dataGridView1.Rows[l].Cells[2].FormattedValue.ToString() NOW CHANGES FOR ID IS NOT NULL
                                                            {
                                                                DataTable dt2 = daimport.GridSelect(dataGridView2.Rows[l].Cells[2].FormattedValue.ToString().Trim(), dataGridView2.Rows[l].Cells[4].FormattedValue.ToString().Trim(), dataGridView1.Rows[k].Cells[l].FormattedValue.ToString().Trim());
                                                                if (dt2 == null || Convert.ToInt32(dt2.Rows.Count) <= 0)
                                                                {
                                                                    Cursor = Cursors.Default; textMessagebox.Refresh();
                                                                    //MessageBox.Show("No Data Found this Table   :'" + dataGridView2.Rows[l].Cells[2].FormattedValue + "' 's      " + " Field Name is " + dataGridView2.Rows[l].Cells[4].FormattedValue + "='" + dataGridView1.Rows[k].Cells[l].Value + "  " + "ROW  :  " + k.ToString() + "  COLUMN  :" + l.ToString() + "   Insert Query    : " + daimport.query + "    Excel  Data Invalid", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                                    textMessagebox.Text = "No Data Found this Table   :'" + dataGridView2.Rows[l].Cells[2].FormattedValue + "' 's      " + " Field Name is " + dataGridView2.Rows[l].Cells[4].FormattedValue + "='" + dataGridView1.Rows[k].Cells[l].Value + "  " + "ROW  :  " + k.ToString() + "  COLUMN  :" + l.ToString() + "   Insert Query    : " + daimport.query + "    Excel  Data Invalid";
                                                                      cnt = 0; colcount = 0; totable = ""; totableupdate = "";
                                                                    cnt = k + 1; l = dataGridView1.Columns.Count;
                                                                }
                                                                if (dt2.Rows.Count >= 1)
                                                                {
                                                                    string c = dt2.Rows[0][dataGridView2.Rows[l].Cells[2].FormattedValue + "ID"].ToString();
                                                                    totable += ",'" + dt2.Rows[0][dataGridView2.Rows[l].Cells[2].FormattedValue + "ID"].ToString() + "'";
                                                                    daimport.query = "";
                                                                }
                                                            }
                                                            else
                                                            {
                                                                totable += ",'" + dataGridView1.Rows[k].Cells[l].Value.ToString() + "'";
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (dataGridView2.Rows[l].Cells[2].FormattedValue.ToString().Trim() != "")
                                                        {
                                                            daimport.query = "";
                                                            DataTable dt2 = daimport.GridSelect(dataGridView2.Rows[l].Cells[2].FormattedValue.ToString().Trim(), dataGridView2.Rows[l].Cells[4].FormattedValue.ToString().Trim(), dataGridView1.Rows[k].Cells[l].FormattedValue.ToString().Trim());
                                                            if (dt2 == null || Convert.ToInt32(dt2.Rows.Count) <= 0)
                                                            {
                                                                Cursor = Cursors.Default;
                                                                //MessageBox.Show("No Data Found this Table   :'" + dataGridView2.Rows[l].Cells[2].FormattedValue + "' 's      " + " Field Name is " + dataGridView2.Rows[l].Cells[4].FormattedValue + "='" + dataGridView1.Rows[k].Cells[l].Value + "  " + "ROW  :  " + k.ToString() + "  COLUMN  :" + l.ToString() + "   Insert Query    : " + daimport.query + "    Excel  Data Invalid", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                textMessagebox.Refresh();
                                                                textMessagebox.Text= "No Data Found this Table: '" + dataGridView2.Rows[l].Cells[2].FormattedValue + "' 's      " + " Field Name is " + dataGridView2.Rows[l].Cells[4].FormattedValue + "='" + dataGridView1.Rows[k].Cells[l].Value + "  " + "ROW: " + k.ToString() + "  COLUMN: " + l.ToString() + "   Insert Query    : " + daimport.query + "    Excel Data Invalid";
                                                                colcount = 0; totable = ""; totableupdate = "";
                                                                cnt = k + 1; l = dataGridView1.Columns.Count;
                                                            }
                                                            if (dt2.Rows.Count >= 1)
                                                            {
                                                                string c = dt2.Rows[0][dataGridView2.Rows[l].Cells[2].FormattedValue + "ID"].ToString();
                                                                totable += ",'" + dt2.Rows[0][dataGridView2.Rows[l].Cells[2].FormattedValue + "ID"].ToString() + "'";

                                                            }
                                                        }
                                                        else
                                                        {
                                                            totable += ",'" + dataGridView1.Rows[k].Cells[l].Value + "'";
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    Cursor = Cursors.Default;
                                                    MessageBox.Show("Table Name is Empty");
                                                }
                                            }
                                        }                                                                            
                                    }
                                }
                                if (totable != "" && fromtable != "" && Class.Users.TableName != null)
                                {
                                    update1 = totableupdate;
                                    daimport.query = "";
                                    DataTable dt3 =daimport.FindDuplicate(Class.Users.TableName,Class.Users.ProjectID,update1);
                                    if (dt3 == null)
                                    {
                                        Cursor = Cursors.Default;
                                        textMessagebox.Refresh();
                                        textMessagebox.Text = "No Data Found this Table   :'" + Class.Users.TableName + "' 's      " + " Field Name is  Empty. pls select  Correct Table name .    ROW  :  " + k.ToString() + "  COLUMN  :" + l.ToString() + "   Insert Query    : " + daimport.query + "    Excel  Data Invalid";
                                        //MessageBox.Show("No Data Found this Table   :'" + Class.Users.TableName + "' 's      " + " Field Name is  Empty. pls select  Correct Table name .    ROW  :  " + k.ToString() + "  COLUMN  :" + l.ToString() + "   Insert Query    : " + daimport.query + "    Excel  Data Invalid", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        totable = ""; totableselect = ""; totableupdate = ""; totableid = ""; nongridid = ""; nongridid1 = ""; totableupdategrid = "";
                                        cnt = 0; colcount = 0; cnt = k + 1; l = dataGridView1.Columns.Count;
                                        savefalse = true;
                                    }
                                    else if (dt3.Rows.Count != 0)
                                    {
                                       // progressBar1.Minimum = 0;
                                        totable = ""; totableselect = ""; totableupdate = ""; totableid = ""; nongridid = ""; nongridid1 = ""; totableupdategrid = "";
                                        cnt = 0; colcount = 0;
                                    }
                                    else if (dt3.Rows.Count == 0)
                                    {
                                        if (dataGridView2.Rows[0].Cells[5].FormattedValue.ToString() == "YES")
                                        {
                                            Cursor.Current = Cursors.Default;
                                            string ins = "insert into " + Class.Users.ProjectID + "." + Class.Users.TableName + "(" + fromtable + ")values(" + totable + ")";
                                            textMessagebox.Refresh();
                                            textMessagebox.Text = ins;
                                            Utility.ExecuteNonQuery(ins);                                            
                                            totable = ""; totableselect = ""; totableupdate = ""; totableid = ""; nongridid = ""; nongridid1 = ""; totableupdategrid = "";
                                            savefalse = true;
                                        }
                                        if (dataGridView2.Rows[0].Cells[5].FormattedValue.ToString() == "")
                                        {
                                            Cursor.Current = Cursors.Default;
                                            string ins = "insert into " + Class.Users.ProjectID + "." + Class.Users.TableName + "(" + fromtable + Details + ")values(" + totable + Details1 + ")";
                                            textMessagebox.Refresh();
                                            textMessagebox.Text = ins;
                                            Utility.ExecuteNonQuery(ins);                                             
                                            totable = ""; totableselect = ""; totableupdate = ""; totableid = ""; nongridid = ""; nongridid1 = ""; totableupdategrid = "";
                                            savefalse = true;
                                        }
                                    }
                                    else
                                    {
                                        totable = ""; totableselect = ""; totableupdate = ""; totableid = ""; nongridid = ""; nongridid1 = ""; totableupdategrid = "";
                                    }
                                    //decimal per = Convert.ToDecimal(100 / dataGridView1.Rows.Count) * (k + 1);
                                    //lblprocessbar.Text = "" + (per).ToString("N0") + " % '" + k.ToString() + "";
                                    lblprocessbar.Refresh();
                                    lblprocessbar.Text = "Total Rows :"+ totcnt+ " of "+ k.ToString();
                                    textMessagebox.Text = lblprocessbar.Text;
                                    lblprocessbar.Refresh(); textMessagebox.Refresh();
                                    //progressBar1.Value = k + 1;
                                }
                                cnt++;
                            }


                            if (savefalse == false)
                            {
                                MessageBox.Show("Child Record Found."); lblchild.Refresh();
                                lblchild.Text = "Child Record Found.";
                                textMessagebox.Refresh();
                                textMessagebox.Text = lblchild.Text;
                            }
                            if (savefalse == true)
                            {
                                MessageBox.Show("Record Saved Successfully." + dataGridView1.Rows.Count.ToString());
                                textMessagebox.Refresh();
                                textMessagebox.Text = "Record Saved Successfully." + dataGridView1.Rows.Count.ToString();
                            }
                        }

                    }
                    else
                    {
                        Cursor = Cursors.Default;
                        MessageBox.Show("No Data Found Excel Field. ", "Excel Column count: '" + dataGridView2.Rows.Count + "'", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                }
                if (Convert.ToString(Class.Users.TableName) != null && Class.Users.TableNameGrid != null && Class.Users.Prefix != null)
                {
                    if (allislug.Items.Count >= 1 && allislug.Items.Count > 0)
                    {
                        
                        if (dataGridView3.Rows[0].Cells[2].FormattedValue.ToString() != "")
                        {
                            totable = "";
                            DataTable dt2 = daimport.TableMax(Class.Users.TableName, Class.Users.ProjectID);
                            dataGridView3.Rows[0].Cells[5].Value = dt2.Rows[0]["LASTNO"].ToString();
                            daimport.query = null;
                            DataTable dt3 = daimport.SlugData(Class.Users.ProjectID, Class.Users.TableNameGrid, dataGridView3.Rows[0].Cells[2].FormattedValue.ToString(), dataGridView3.Rows[0].Cells[3].FormattedValue.ToString(), dataGridView3.Rows[0].Cells[4].FormattedValue.ToString());
                            if (dt3 == null || Convert.ToInt32(dt3.Rows.Count) <= 0)
                            {
                                MessageBox.Show("No data found in Parent Table.Pls Check Excel Data  :'" + dataGridView3.Rows[0].Cells[2].FormattedValue + "' 's      " + " Field Name is " + dataGridView3.Rows[0].Cells[3].FormattedValue + "='" + dataGridView3.Rows[0].Cells[1].Value + "        " + "ROW  :  " + k.ToString() + "  COLUMN  :" + l.ToString() + "   Insert Query    : " + sel + "    Excel  Data Invalid", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                fromtable = ""; totable = ""; totableupdate = ""; daimport.query = "";
                            }
                            if (dt3.Rows.Count > 0)
                            {
                                Cursor.Current = Cursors.WaitCursor;
                                for (k = 0; k < dataGridView3.Rows.Count - 1; k++)
                                {

                                    if (allislug.Items.Count > 0)
                                    {
                                        for (l = 0; l < dataGridView3.Columns.Count - 1; l++)
                                        {
                                            if (Class.Users.TableNameGrid != null && dataGridView3.Rows[0].Cells[1].FormattedValue.ToString() != "")
                                            {
                                                if (l == 0)
                                                {
                                                    totablegrid = allislug.Items[l].SubItems[0].Text;
                                                    totable = allislug.Items[l].SubItems[0].Text + " = '" + dt3.Rows[0]["AUTOGENERATEID"].ToString() + "'";
                                                }
                                                if (l >= 1)
                                                {
                                                   
                                                    totableupdate += "  " + allislug.Items[l].SubItems[0].Text + "='" + dataGridView3.Rows[k].Cells[l].Value + "' ,";
                                                }
                                            }
                                        }
                                    }
                                }
                                if (totable != "" && allislug.Items.Count > 0 && Class.Users.TableNameGrid != null)
                                {
                                    update1 = ""; int ss, sss;
                                    ss = Convert.ToInt32(totableupdate.Length.ToString());
                                    sss = Convert.ToInt32(ss - 1);
                                    update1 = totableupdate.Substring(0, sss).ToString();
                                    daimport.query = "update " + Class.Users.ProjectID + ".autogenerate set " + update1 + " where " + totable + " AND PREFIX='" + Class.Users.Prefix + "'";
                                    Utility.ExecuteNonQuery(daimport.query);
                                    totable = ""; fromgridtotable = ""; update1 = ""; //progressBar1.Minimum = 0;
                                }
                                
                                
                            }
                            else
                            {
                                Cursor = Cursors.Default;
                                MessageBox.Show(" Columns Field's  : " + dataGridView2.Columns.Count.ToString() + "   Table Columns Field's :   " + allislug.Items.Count.ToString(), "  Invalid  ", MessageBoxButtons.OK, MessageBoxIcon.Information); ;

                            }

                        }
                                        
                    }
                }
              
                Cursor = Cursors.Default; lblchild.Text = cnt.ToString();
                cnt = 0; colcount = 0; progressBar1.Minimum = 0; lblprocessbar.Text = "";
                fromtable = ""; totable = ""; totableupdate = ""; totableupdategrid = "";// News();
                DataTable DT = daimport.FindTrigger(Class.Users.TableName, Class.Users.ProjectID);
                if (DT.Rows.Count > 0)
                {
                    daimport.dropsequence(Class.Users.ProjectID, Class.Users.TableName);
                    daimport.DropTrigger(Class.Users.ProjectID, Class.Users.TableName);
                }
            }
            catch (Exception ex)
            {
               // Cursor = Cursors.Default;
                MessageBox.Show(ex.Message + "-----" + k.ToString() + "  COLUMN  :" + l.ToString() + "   Insert Query    : " + sel + "    pls Select Correct Table or Column", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                fromtable = ""; fromslugtale = "";
                totable = ""; totableupdate = ""; totableupdategrid = ""; cnt = k + 1; l = dataGridView1.Columns.Count;
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
            try
            {
                DataTable dtgridview = new DataTable();

                if (dataGridView1.Rows.Count > 0)
                {
                    daimport.GridRowRemove(dataGridView1);
                 
                }

                if (dataGridView1.Rows.Count <= 0)
                {
                    dtgridview.Rows.Clear(); dtgridview.Columns.Clear();
             
                    int i = 0;

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
                            path = @"provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties='Excel 8.0;HRD=Yes;IMEX=1';"; //for below excel 2007  
                        else
                            path = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties='Excel 12.0;HDR=Yes';"; //for above excel 2007  

                        OledbConn = new System.Data.OleDb.OleDbConnection(path);
                        string qry1 = "Select * from [Sheet1$]";
                        OledbAdapter = new OleDbDataAdapter(qry1, OledbConn);
                        OledbAdapter.Fill(dtgridview);
                        if (dtgridview.Rows.Count > 0)
                        {
                            //if (dataGridView1.Rows.Count > 1) { }
                            //else
                            //{
                            //    dataGridView2.Rows.Clear();
                            //}
                            dataGridView1.DataSource = dtgridview;
                            int k = 1;
                            if (dataGridView2.Rows.Count <= 1)
                            {
                                for (int j = 0; j < dtgridview.Columns.Count; j++)
                                {
                                    
                                    dataGridView2.Rows.Add();
                                    dataGridView2.Rows[j].Cells[1].Value = dtgridview.Columns[j].ToString();
                                    lblexelcolumn.Text = "Total Excel Columns Count : " + k.ToString();
                                    k++;

                                  
                                }
                            }
                        }
                       
                    }

                    int cnt = dataGridView1.Rows.Count;
                    lbldowncount.Text = "Total Excel Rows : " +  cnt.ToString();
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }
            tabControl1.SelectTab(tabPage5);
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    int item0 = 0;
            //    if (txtsearch.Text.Length > 0)
            //    {
            //        listView1.Items.Clear();

            //        foreach (ListViewItem item in listfilterdb.Items)
            //        {
            //            ListViewItem list = new ListViewItem();
            //            if (listfilterdb.Items[item0].SubItems[2].ToString().Contains(txtsearch.Text))
            //            {

            //                list.SubItems.Add(listfilterdb.Items[item0].SubItems[1].Text);
            //                list.SubItems.Add(listfilterdb.Items[item0].SubItems[2].Text);


            //                if (item0 % 2 == 0)
            //                {
            //                    list.BackColor = Color.White;

            //                }
            //                else
            //                {
            //                    list.BackColor = Color.WhiteSmoke;

            //                }



            //                listView1.Items.Add(list);
            //            }

            //            item0++;
            //        }
            //        lblemptot.Text = "Total Rows    :" + listView1.Items.Count;
            //    }
            //    else
            //    {
            //        //try
            //        //{
            //        //    listView1.Items.Clear();
            //        //    foreach (ListViewItem item in listfilterdb.Items)
            //        //    {
            //        //        this.listView1.Items.Add((ListViewItem)item.Clone());
            //        //        item0++;
            //        //    }
            //        //    lblemptot.Text = "Total Rows    :" + listView1.Items.Count;
            //        //}
            //        //catch (Exception ex)
            //        //{

            //        //}
            //    }
            //}
            //catch (Exception ex)
            //{
            //    // MessageBox.Show("---" + ex.ToString());
            //}
        }



        private void lvlogs_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            ListViewItem it = new ListViewItem();
            iIndex = lvlogs.Items.Count;
            Class.Users.UserTime = 0;
            if (e.Item.Checked == true)
            {
                tablecount++;
                if (tablecount > 0)
                {
                    tablecount = -1;
                    lbltablename.Text = ""; Class.Users.TableName = "";
                    lbltablename1.Text = ""; Class.Users.TableNameGrid = "";
                     lbltablename2.Text = ""; Class.Users.TableNameSubGrid = "";
                    for (i = 0; i < lvlogs.Items.Count; i++)
                    {
                        lvlogs.Items[i].SubItems[0].Text = "";

                        lvlogs.Items[i].Checked = false;
                    }

                    MessageBox.Show("Maximum 1 Tables only should be Choose.", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                   // listView1.Items.Clear();
                }
                e.Item.SubItems[3].Text = "✔";
                e.Item.SubItems[3].ForeColor = Color.Green;
                it.SubItems.Add(e.Item.SubItems[2].Text);
                it.SubItems.Add(e.Item.Checked.ToString());

                if (tablecount == 0)
                {

                    lbltablename.Text = ""; Class.Users.TableName = "";
                    Class.Users.TableName = e.Item.SubItems[2].Text;
                    lbltablename.Text = "Table Name :-  " + e.Item.SubItems[2].Text;
                    
                }
  

                if (iIndex % 2 == 0)
                {
                    lvlogs.BackColor = Color.White;
                }
                else
                {
                    lvlogs.BackColor = Color.WhiteSmoke;
                }
                allip4.Items.Add(it);
               
            }
            if (e.Item.Checked == false && e.Item.SubItems[3].Text == "✔")
            {
                //listView1.Items.Clear();
                e.Item.SubItems[3].Text = "✖";
                e.Item.SubItems[3].ForeColor = Color.DarkRed;
                e.Item.Checked = false;

                if (tablecount > 0)
                {
                    tablecount = -1;
                    lbltablename.Text = ""; Class.Users.TableName = "";
                    lbltablename1.Text = ""; Class.Users.TableNameGrid = "";
                    lbltablename2.Text = ""; Class.Users.TableNameSubGrid = "";
                    for (i = 0; i < lvlogs.Items.Count; i++)
                    {
                        lvlogs.Items[i].SubItems[0].Text = "";

                        lvlogs.Items[i].Checked = false;
                    }
                }

                allip4.Items.Clear();


            }
           
        }

        private void txtsearch_TextChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    int item0 = 0;
            //    if (txtsearch.Text.Length > 0)
            //    {
            //        listView1.Items.Clear();
            //        foreach (ListViewItem item in listfilterexcel.Items)
            //        {
            //            ListViewItem list = new ListViewItem();
            //            if (listfilterexcel.Items[item0].SubItems[2].ToString().Contains(txtsearch.Text))
            //            {
            //                list.Text = "";
            //                list.SubItems.Add(listfilterexcel.Items[item0].SubItems[1].Text);
            //                list.SubItems.Add(listfilterexcel.Items[item0].SubItems[2].Text);
            //                list.SubItems.Add(listfilterexcel.Items[item0].SubItems[3].Text);

            //                if (item0 % 2 == 0)
            //                {
            //                    list.BackColor = Color.White;

            //                }
            //                else
            //                {
            //                    list.BackColor = Color.WhiteSmoke;

            //                }



            //                listView1.Items.Add(list);
            //            }

            //            item0++;
            //        }
            //        lblemptot.Text = "Total Rows    :" + listView1.Items.Count;
            //    }
            //    else
            //    {
            //        try
            //        {
            //            listView1.Items.Clear();
            //            foreach (ListViewItem item in listfilterexcel.Items)
            //            {
            //                this.listView1.Items.Add((ListViewItem)item.Clone());
            //                item0++;
            //            }
            //            lblemptot.Text = "Total Rows    :" + listView1.Items.Count;
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

        private void butfindtablename_Click(object sender, EventArgs e)
        {
            tabControl4.SelectTab(tabFindTable);
        }

        private void listView2_ItemChecked(object sender, ItemCheckedEventArgs e)
        {

            ListViewItem it = new ListViewItem();
            iIndex = listView2.Items.Count;
            allislug.Items.Clear(); dataGridView3.Rows.Clear();          
            if (e.Item.Checked == true)
            {
                tablecountslug++;
                if (tablecountslug > 0)
                {
                    tablecountslug = -1;

                    lbltablename1.Text = ""; Class.Users.TableNameGrid = "";

                    lbltablename2.Text = ""; Class.Users.TableNameSubGrid = "";
                    for (i = 0; i < listView2.Items.Count; i++)
                    {
                        listView2.Items[i].SubItems[0].Text = "";

                        listView2.Items[i].Checked = false;
                    }
                   
                    // MessageBox.Show("Maximum 1 Tables only should be Choose.", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // Class.Users.TableNameGrid = dataGridView.Rows[0].Cells[1].FormattedValue.ToString();
                }
                e.Item.SubItems[3].Text = "✔";
                e.Item.SubItems[3].ForeColor = Color.Green;
                it.SubItems.Add(e.Item.SubItems[2].Text);
                it.SubItems.Add(e.Item.Checked.ToString());

                if (tablecountslug == 0)
                {

                    lbltablename1.Text = ""; Class.Users.TableNameGrid = "";
                    Class.Users.TableNameGrid = e.Item.SubItems[2].Text;
                    lbltablename1.Text = "Slug Name :-  " + e.Item.SubItems[2].Text;
                    //tabControl1.SelectTab(tabPage1);
                }

                if (iIndex % 2 == 0)
                {
                    listView2.BackColor = Color.White;
                }
                else
                {
                    listView2.BackColor = Color.WhiteSmoke;
                }

            }
            if (e.Item.Checked == false && e.Item.SubItems[3].Text == "✔")
            {

                e.Item.SubItems[3].Text = "✖";
                e.Item.SubItems[3].ForeColor = Color.DarkRed;
                e.Item.Checked = false;

                if (tablecountslug > 0)
                {
                    tablecountslug = -1;
                    lbltablename1.Text = ""; Class.Users.TableNameGrid = "";
                    for (i = 0; i < lvlogs.Items.Count; i++)
                    {
                        listView2.Items[i].SubItems[0].Text = "";

                        listView2.Items[i].Checked = false;
                    }
                }

            }

        }

        //public static void FromListView(DataTable table, ListView lvw)
        //{
        //    table.Clear();
        //    var columns = lvw.Columns.Count;

        //    foreach (ColumnHeader column in lvw.Columns)
        //        table.Columns.Add(column.Text);

        //    foreach (ListViewItem item in lvw.Items)
        //    {
        //        var cells = new object[columns];
        //        for (var i = 0; i < columns; i++)
        //            cells[i] = item.SubItems[i].Text;
        //        table.Rows.Add(cells);
        //    }
        //}
        private void butimport_Click(object sender, EventArgs e)
        {

        }
       

        private void listView1_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            //Class.Users.UserTime = 0;
            //if (e.Item.Checked == true)
            //{
            //    ListViewItem it = new ListViewItem();
            //    e.Item.SubItems[3].Text = "✔";
            //    e.Item.SubItems[3].ForeColor = Color.Green;
            //    it.SubItems.Add(e.Item.SubItems[2].Text);
            //    it.SubItems.Add(e.Item.Checked.ToString());
              

            //    allip3.Items.Add(it);
            //    lblfieldcount.Text = "Selected Table Columns Count  :  " + allip3.Items.Count;
            //}
            //if (e.Item.Checked == false && e.Item.SubItems[3].Text == "✔")
            //{
            //    e.Item.SubItems[3].Text = "✖";
            //    e.Item.SubItems[3].ForeColor = Color.DarkRed;
            //    e.Item.Checked = false;
            //    for (int c = 0; c < allip3.Items.Count; c++)
            //    {
            //        if (allip3.Items[c].SubItems[1].Text == e.Item.SubItems[2].Text)
            //        {
            //            allip3.Items[c].Remove();
            //            c--;
            //            lblfieldcount.Text = "Selected Table Columns Count  :  " + allip3.Items.Count;
            //        }
            //    }
            //}
        }



        private void tabControl1_TabIndexChanged(object sender, EventArgs e)
        {

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Class.Users.UserTime = 0;
                if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage1"])//your specific tabname
                {
                    Cursor.Current = Cursors.WaitCursor;
                    //if (listView1.Items.Count <= 0)
                    //{


                        string sel = ""; DataTable dttbl = new DataTable();

                        if (tablecount == 0)
                        {
                            daimport.dt = null;
                            //daimport.query = "select * from " + Class.Users.ProjectID + "." + Class.Users.TableName + " where " + Class.Users.TableName + "ID=(select count(1) from " + Class.Users.ProjectID + "." + Class.Users.TableName + ") order by 1";
                            daimport.query = "select * from " + Class.Users.ProjectID + "." + Class.Users.TableName + "";
                            daimport.ds = Utility.ExecuteSelectQuery(daimport.query, Class.Users.TableName);
                            daimport.dt = daimport.ds.Tables[Class.Users.TableName];
                        }

                        int i = 1;
                        if (daimport.dt != null)
                        {
                            
                            DBColumn.Items.Clear();
                        if (daimport.dt.Columns.Count > 0)
                        {                           
                            foreach (DataColumn myRow in daimport.dt.Columns)
                            {
                                DBColumn.Items.AddRange(myRow.ToString());
                            }
                        }
                        dataGridView3.Rows[0].Cells[1].Value = Class.Users.TableNameGrid;
                            daimport.query = ""; DataSet ds2 = new DataSet(); DataTable dt2 = new DataTable(); ;
                            dt2.Rows.Clear();
                            sel = "SELECT  DISTINCT A.DESCRIPTION FROM " + Class.Users.ProjectID + ".AUTOGENERATE A  WHERE A.TX_VIEW_ID='" + Class.Users.TableNameGrid + "' AND A.PROJECTID='" + Class.Users.ProjectID + "' ORDER BY 1 ";//  
                            ds2 = Utility.ExecuteSelectQuery(sel, "AUTOGENERATE");
                            dt2 = ds2.Tables["AUTOGENERATE"];
                            if (dt2.Rows.Count > 0)
                            {
                                Description.Items.Clear();
                                foreach (DataRow myRow in dt2.Rows)
                                {
                                    Description.Items.AddRange(myRow["DESCRIPTION"].ToString());
                                }
                            }
                            dt2.Rows.Clear();
                            sel = "SELECT  DISTINCT A.FIELDNAME FROM " + Class.Users.ProjectID + ".AUTOGENERATE A  WHERE  A.TX_VIEW_ID='" + Class.Users.TableNameGrid + "' AND   A.PROJECTID='" + Class.Users.ProjectID + "' ORDER BY 1 ";
                            ds2 = Utility.ExecuteSelectQuery(sel, "AUTOGENERATE");
                            dt2 = ds2.Tables["AUTOGENERATE"];
                            if (dt2.Rows.Count > 0)
                            {
                                FieldName.Items.Clear();
                                foreach (DataRow myRow in dt2.Rows)
                                {
                                    FieldName.Items.AddRange(myRow["FIELDNAME"].ToString());
                                }
                            }
                            dt2.Rows.Clear();


                        }

                    }
                    else
                    {

                    }
               // }

            }
            catch (Exception EX) { }
            Cursor = Cursors.Default;
            lblfieldcount.Text = "Total Column Count: " + dataGridView2.Rows.Count;
        }
   
        private void chkall_CheckedChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    int i = 0;
            //    if (chkall.Checked == true)
            //    {
            //        for (i = 0; i < listView1.Items.Count; i++)
            //        {
            //            listView1.Items[i].Checked = true;
            //        }
            //    }
            //    if (chkall.Checked == false)
            //    {
            //        for (i = 0; i < listView1.Items.Count; i++)
            //        {
            //            listView1.Items[i].Checked = false;
            //        }

            //        fromtable = ""; allip3.Items.Clear();

            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}
        }

        private void checkallgrid_CheckedChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    int i = 0;
            //    if (checkallgrid.Checked == true)
            //    {
            //        for (i = 0; i < listView4.Items.Count; i++)
            //        {
            //            listView4.Items[i].Checked = true;
            //        }
            //    }
            //    if (checkallgrid.Checked == false)
            //    {
            //        for (i = 0; i < listView4.Items.Count; i++)
            //        {
            //            listView4.Items[i].Checked = false;
            //        }

            //        fromgridtable = ""; allip3grid.Items.Clear();

            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}
        }

        private void listView4_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            //if (e.Item.Checked == true)
            //{
            //    ListViewItem it = new ListViewItem();
            //    e.Item.SubItems[3].Text = "✔";
            //    e.Item.SubItems[3].ForeColor = Color.Green;
            //    it.SubItems.Add(e.Item.SubItems[2].Text);
            //    it.SubItems.Add(e.Item.Checked.ToString());


            //    allip3grid.Items.Add(it);
            //}
            //if (e.Item.Checked == false && e.Item.SubItems[3].Text == "✔")
            //{
            //    e.Item.SubItems[3].Text = "✖";
            //    e.Item.SubItems[3].ForeColor = Color.DarkRed;
            //    e.Item.Checked = false;
            //    for (int c = 0; c < allip3grid.Items.Count; c++)
            //    {
            //        if (allip3grid.Items[c].SubItems[1].Text == e.Item.SubItems[2].Text)
            //        {
            //            allip3grid.Items[c].Remove();
            //            c--;
            //        }
            //    }
            //}
        }

        private void lvlogsgrid_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            //if (e.Item.Checked == true)
            //{
            //    ListViewItem it = new ListViewItem();

            //    e.Item.SubItems[3].Text = "✔";
            //    e.Item.SubItems[3].ForeColor = Color.Green;
            //   Class.Users.TableNameGrid = "";
            //    it.SubItems.Add(e.Item.SubItems[2].Text);
            //    it.SubItems.Add(e.Item.Checked.ToString());
            //    lbltablenamegrid.Text = "";
            //    lbltablenamegrid.Text = "Table Name :-  " + e.Item.SubItems[2].Text;              
            //    Class.Users.TableNameGrid = e.Item.SubItems[2].Text;
            //    string sel = "select * from " + e.Item.SubItems[2].Text + ";";
            //    DataSet ds = Utility.ExecuteSelectQuery(sel, Class.Users.TableNameGrid);
            //    DataTable dttbl = ds.Tables[Class.Users.TableNameGrid];
            //    int i = 1;
            //    for (int j = 0; j < dttbl.Columns.Count; j++)
            //    {
            //        ListViewItem ittbl = new ListViewItem();
            //        ittbl.Text = "";
            //        ittbl.SubItems.Add(i.ToString());
            //        ittbl.SubItems.Add(dttbl.Columns[j].ToString());
            //        ittbl.SubItems.Add("");

            //        if (i % 2 == 0)
            //        {
            //            ittbl.BackColor = Color.White;
            //        }
            //        else
            //        {
            //            ittbl.BackColor = Color.WhiteSmoke;
            //        }

            //        i++;


            //        listView4.Items.Add(ittbl);
            //    }
            //    // allip4.Items.Add(it);
            //}
            //if (e.Item.Checked == false)
            //{
            //    listView4.Items.Clear();
            //    e.Item.SubItems[3].Text = "";
            //    e.Item.SubItems[3].ForeColor = Color.DarkRed;
            //    e.Item.Checked = false;
            //    //for (int c = 0; c < allip4.Items.Count; c++)
            //    //{

            //    //    if (lvlogs.Items[c].SubItems[2].Text == e.Item.SubItems[2].Text)
            //    //    {
            //    //        allip4.Items[c].Remove();
            //    //        c--;
            //    //    }
            //    //}
            //}
        }

        private void butNonGridTable_Click(object sender, EventArgs e)
        {

        }
        // DataTable dtgridview1 = new DataTable();
        private void butGridTable_Click(object sender, EventArgs e)
        {
            //try
            //{

            //    dtgridview1.Rows.Clear(); dtgridview1.Columns.Clear();
            //    if (dataGridView2.Rows.Count > 0)
            //    {
            //        do
            //        {
            //            for (int i = 0; i < dataGridView2.Rows.Count; i++) { try { dataGridView2.Rows.RemoveAt(i); } catch (Exception) { } }
            //        }
            //        while (dataGridView2.Rows.Count > 0);

            //        listView3.Items.Clear();
            //    }

            //    if (dataGridView2.Rows.Count <= 0)
            //    {

            //        listView3.Items.Clear();

            //        int i = 0;


            //        System.Data.OleDb.OleDbConnection OledbConn;
            //        System.Data.OleDb.OleDbCommand OledbCmd;
            //        System.Data.OleDb.OleDbDataAdapter OledbAdapter;
            //        string filePath = string.Empty; string fileExt = string.Empty;
            //        OpenFileDialog file = new OpenFileDialog(); string path = "";
            //        if (file.ShowDialog() == System.Windows.Forms.DialogResult.OK) //if there is a file choosen by the user  
            //        {
            //            filePath = file.FileName; //get the path of the file  
            //            fileExt = Path.GetExtension(filePath); //get the file extension  
            //            if (fileExt.CompareTo(".xls") == 0)
            //                path = @"provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties='Excel 8.0;HRD=Yes;IMEX=1';"; //for below excel 2007  
            //            else
            //                path = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties='Excel 12.0;HDR=Yes';"; //for above excel 2007  

            //            OledbConn = new System.Data.OleDb.OleDbConnection(path);
            //            string qry1 = "Select * from [Sheet1$]";
            //            OledbAdapter = new OleDbDataAdapter(qry1, OledbConn);
            //            OledbAdapter.Fill(dtgridview1);
            //            if (dtgridview1.Rows.Count > 0)
            //            {
            //                dataGridView2.DataSource = dtgridview1;

            //                int k = 0;
            //                for (int j = 0; j < dtgridview1.Columns.Count; j++)
            //                {
            //                    ListViewItem itt = new ListViewItem();

            //                    itt.Text = "";
            //                    itt.SubItems.Add(k.ToString());
            //                    itt.SubItems.Add(dtgridview1.Columns[j].ToString());

            //                    if (j % 2 == 0)
            //                    {
            //                        itt.BackColor = Color.White;
            //                    }
            //                    else
            //                    {
            //                        itt.BackColor = Color.WhiteSmoke;
            //                    }

            //                    k++;


            //                    listView3.Items.Add(itt);
            //                }

            //            }
            //        }

            //        int cnt = dataGridView2.Rows.Count;

            //    }
            //}
            //catch (Exception ex)
            //{
            //    //MessageBox.Show(ex.ToString());
            //}
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {




        }

        private void txtsearchtable_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Class.Users.UserTime = 0;
                int item0 = 0; lvlogs.Items.Clear();
                if (txtsearchtable.Text.Length > 1)
                {


                    foreach (ListViewItem item in listfilterdb.Items)
                    {

                        if (listfilterdb.Items[item0].SubItems[2].ToString().Contains(txtsearchtable.Text))
                        {
                            ListViewItem list2 = new ListViewItem();
                            list2.Text = "";
                            list2.SubItems.Add(listfilterdb.Items[item0].SubItems[1].Text);
                            list2.SubItems.Add(listfilterdb.Items[item0].SubItems[2].Text);
                            list2.SubItems.Add(listfilterdb.Items[item0].SubItems[3].Text);
                            if (item0 % 2 == 0)
                            {
                                list2.BackColor = Color.White;

                            }
                            else
                            {
                                list2.BackColor = Color.WhiteSmoke;

                            }
                            lvlogs.Items.Add(list2);
                        }

                        item0++;
                    }
                    lblemptot.Text = "Total Rows    :" + lvlogs.Items.Count;
                }
                else
                {

                   
                        lvlogs.Items.Clear();
                        foreach (ListViewItem item in listfilterdb.Items)
                        {
                            this.lvlogs.Items.Add((ListViewItem)item.Clone());
                            item0++;
                        }
                        lblemptot.Text = "Total Rows    :" + lvlogs.Items.Count;
                    
                }
            }
            catch (Exception ex)
            {
                // MessageBox.Show("---" + ex.ToString());
            }
        }

        private void tableNameToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        bool dbcol = false;
        void GridColumnRefresh(DataGridView grid, int idx)
        {
            if (grid.Rows[idx].Cells[2].FormattedValue.ToString() != "")
            {
                int lines = idx;
                //lblfieldcount.Refresh();
                //lblfieldcount.Text = lines.ToString();
                if (lines % 2 == 0)
                {
                    grid.Rows[idx].DefaultCellStyle.BackColor = Color.AntiqueWhite;
                   
                }
                else
                {
                    grid.Rows[idx].DefaultCellStyle.BackColor = Color.LightPink;
                   
                }
                string tablenames = grid.Rows[idx].Cells[2].FormattedValue.ToString();
                TableGridLoad(tablenames);
                textMessagebox.Refresh();
            }

        }

        void GridCellClear(DataGridView grid, int idx)
        {
            if (grid.Rows[idx].Cells[4].FormattedValue.ToString() != "")
            {

                grid.Rows[idx].Cells[4].Value = string.Empty;
                celcrear = false;
            }
            else
            {

                grid.Rows[idx].Cells[3].Value = string.Empty;
                celcrear = false;
            }

        }
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Class.Users.UserTime = 0;
            if (dbcol == true)
            {
                if (dataGridView2.Columns[e.ColumnIndex].Name == "TableColumn")
                {
                    int IDX = e.RowIndex;
                    Cursor.Current = Cursors.WaitCursor;
                    
                    GridColumnRefresh(dataGridView2, IDX);
                    Cursor.Current = Cursors.Default;
                }
            }
            if (celcrear == true)
            {
                if (dataGridView2.Columns[e.ColumnIndex].Name == "TableColumn" || dataGridView2.Columns[e.ColumnIndex].Name == "DBColumn")
                {
                    Cursor.Current = Cursors.WaitCursor;
                    int IDX = e.RowIndex;
                    GridCellClear(dataGridView2, IDX);
                    Cursor.Current = Cursors.Default;
                    celcrear = false;
                }
            }
            //if (dataGridView2.Rows[e.RowIndex].Cells[2].FormattedValue.ToString() != "")
            //{
            //    int lines = e.RowIndex;
            //    //lblfieldcount.Refresh();
            //    //lblfieldcount.Text = lines.ToString();
            //    if (lines % 2 == 0)
            //    {
            //        dataGridView2.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.AntiqueWhite;

            //    }
            //    else
            //    {
            //        dataGridView2.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightPink;

            //    }
            //    string tablenames = dataGridView2.Rows[e.RowIndex].Cells[2].FormattedValue.ToString();
            //    TableGridLoad(tablenames);
            //}

        }
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tabPage5_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView2_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            //this.dataGridView2.Rows[e.RowIndex].Cells["SNo"].Value = (e.RowIndex + 1).ToString();
        }

        public void ReadOnlys()
        {
            throw new NotImplementedException();
        }

        private void txtslugsearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int item0 = 0; Class.Users.UserTime = 0;
                if (txtslugsearch.Text.Length > 0)
                {
                    listView2.Items.Clear();
                    foreach (ListViewItem item in listfilterslug.Items)
                    {
                        ListViewItem list = new ListViewItem();
                        if (listfilterslug.Items[item0].SubItems[2].ToString().Contains(txtslugsearch.Text))
                        {
                            list.Text = "";
                            list.SubItems.Add(listfilterslug.Items[item0].SubItems[1].Text);
                            list.SubItems.Add(listfilterslug.Items[item0].SubItems[2].Text);
                            list.SubItems.Add(listfilterslug.Items[item0].SubItems[3].Text);

                            if (item0 % 2 == 0)
                            {
                                list.BackColor = Color.White;

                            }
                            else
                            {
                                list.BackColor = Color.WhiteSmoke;

                            }



                            listView2.Items.Add(list);
                        }

                        item0++;
                    }
                    lblslug.Text = "Total Slug Rows    :" + listView2.Items.Count;
                }
                else
                {
                    try
                    {
                        listView2.Items.Clear();
                        foreach (ListViewItem item in listfilterslug.Items)
                        {
                            this.listView2.Items.Add((ListViewItem)item.Clone());
                            item0++;
                        }
                        lblslug.Text = "Total Rows    :" + listView2.Items.Count;
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("---" + ex.ToString());
            }
        }

        private void tabControl3_SelectedIndexChanged(object sender, EventArgs e)
        {

           
        }


        private void dataGridView3_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
           //this.dataGridView3.Rows[e.RowIndex].Cells["SNo"].Value = (e.RowIndex + 1).ToString();
        }

       

        private void dataGridView3_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                dataGridView3.Rows[0].Cells[4].Value = DBNull.Value;
                Prefix.DataSource = null;
                string sel = "SELECT  DISTINCT A.PREFIX FROM " + Class.Users.ProjectID + ".AUTOGENERATE A  WHERE   A.TX_VIEW_ID='" + Class.Users.TableNameGrid + "' AND  A.PROJECTID='" + Class.Users.ProjectID + "'  AND  A.DESCRIPTION='" + dataGridView3.Rows[0].Cells[2].FormattedValue.ToString() + "'  ORDER BY 1";
                DataSet ds2 = Utility.ExecuteSelectQuery(sel, "AUTOGENERATE");
                DataTable dt2 = ds2.Tables["AUTOGENERATE"];
                if (dt2.Rows.Count > 0)
                { 
                    Prefix.DataSource = dt2;
                    Prefix.DisplayMember = "PREFIX";
                    Prefix.ValueMember = "PREFIX";
                }
                if (Class.Users.TableNameGrid == "")
                {
                    Class.Users.TableNameGrid = dataGridView3.Rows[0].Cells[1].FormattedValue.ToString();
                }
            }
        }

        private void dataGridView2_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {


           
        }

        private void slugRefreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridLoad1();
        }

        private void tableNameToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            GridLoad();
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Class.Users.UserTime = 0;

                Cursor.Current = Cursors.WaitCursor;
                int i = 1;
                dataGridView3.Rows[0].Cells[1].Value = Class.Users.TableNameGrid;
               string sel = ""; DataSet ds2 = new DataSet(); DataTable dt2 = new DataTable(); 
                dt2.Rows.Clear();
                sel = "SELECT  DISTINCT A.DESCRIPTION FROM " + Class.Users.ProjectID + ".AUTOGENERATE A  WHERE A.TX_VIEW_ID='" + Class.Users.TableNameGrid + "' AND A.PROJECTID='" + Class.Users.ProjectID + "' ORDER BY 1 ";//  
                ds2 = Utility.ExecuteSelectQuery(sel, "AUTOGENERATE");
                dt2 = ds2.Tables["AUTOGENERATE"];
                if (dt2.Rows.Count > 0)
                {
                    Description.Items.Clear();
                    foreach (DataRow myRow in dt2.Rows)
                    {
                        Description.Items.AddRange(myRow["DESCRIPTION"].ToString());


                    }

                }
                dt2.Rows.Clear(); sel = "";
                 sel = "SELECT  DISTINCT A.FIELDNAME FROM " + Class.Users.ProjectID + ".AUTOGENERATE A  WHERE  A.TX_VIEW_ID='" + Class.Users.TableNameGrid + "' AND   A.PROJECTID='" + Class.Users.ProjectID + "' ORDER BY 1 ";
                ds2 = Utility.ExecuteSelectQuery(sel, "AUTOGENERATE");
                dt2 = ds2.Tables["AUTOGENERATE"];
                if (dt2.Rows.Count > 0)
                {
                    FieldName.Items.Clear();
                    foreach (DataRow myRow in dt2.Rows)
                    {
                        FieldName.Items.AddRange(myRow["FIELDNAME"].ToString());
                    }
                }
                dt2.Rows.Clear();




            }
            catch (Exception EX) { }
            Cursor = Cursors.Default;
        }

        public void Searchs(int id)
        {
            throw new NotImplementedException();
        }

        public void Deletes(int id)
        {
            throw new NotImplementedException();
        }

        private void listViewRefreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //listView1.Items.Clear();
            dataGridView2.Rows.Clear();
        }
      
        private void dBColumnRefresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dbcol = true;
        }

        private void lvlogs_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView2_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = false;
        }
        bool celcrear = false;
        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            celcrear = true;
        }
    }
}