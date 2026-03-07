using System;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Pinnacle.Transactions.Tally
{
    public partial class ReConciliation : Form, ToolStripAccess
    {
        private static ReConciliation _instance;
        ListView listfilterred = new ListView(); Models.Master mas = new Models.Master();
        ListView listfilterscreen = new ListView();
        ListView listfilterred1 = new ListView(); 
        ListView listfilterscreen1 = new ListView();
        ListView listfilterdb = new ListView(); ListView listfilterslug = new ListView(); ListView listfilterexcel = new ListView();
        ListView tofilter = new ListView(); ListView allip3 = new ListView(); ListView allislug = new ListView(); ListView allip4 = new ListView();
        ListView COLUMNORDER = new ListView();
        string tableName = "MY_" + Class.Users.HCompcode;
        string tableName1 = "TO_" + Class.Users.HCompcode;
        DateTime dateForButton = DateTime.Now;
      
        string schema = Class.Users.ProjectID;
        string CleanColumn(string name)
        {
            return name.ToUpper()
                       .Replace("#", "")
                       .Replace(" ", "")
                       .Replace(".", "")
                       .Replace("-", "")
                       .Replace("/", "")
                       .Trim();
        }
        int i = 0; int j = 1; string update1 = null;
        Models.Tally.TransactionCompenstate daimport = new Models.Tally.TransactionCompenstate();
        string Details = ""; string Details1 = "";
        string matchfield = "", matchfield1 = "";
        public ReConciliation()
        {
            InitializeComponent();
            DateTime today = DateTime.Today;
            frmdate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1); ;
            DateTime endOfMonth = new DateTime(today.Year,today.Month, DateTime.DaysInMonth(today.Year, today.Month));
            butmycompany.Text = Class.Users.HCompcode;
            todate.Value = endOfMonth;
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
            GlobalVariables.DownLoads.Text = "UpLoad";
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
        public static ReConciliation Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ReConciliation();
                GlobalVariables.CurrentForm = _instance;
                return _instance;

            }
        }
        private void TranactionCompenstate_Load(object sender, EventArgs e)
        {


        }
        public void TableGridLoad(string s)
        {
            try
            {
                

                    daimport.query = null;
                    dttbl2 = null;
                    daimport.query = "select * from " + Class.Users.ProjectID + "." + s + " ORDER BY 1";
                    daimport.ds = Utility.ExecuteSelectQuery(daimport.query, s);
                    dttbl2 = daimport.ds.Tables[s];
                    DBColumn.Items.Clear(); int j = 0; matchfield = ""; fromquery = "";
                    if (dttbl2.Columns.Count > 0)
                    {

                        i = 1;
                        for (j = 0; j < dttbl2.Columns.Count; j++)
                        {

                            if (DBColumn.Items.Contains(dttbl2.Columns[j].ToString()))
                            {
                                MessageBox.Show(dttbl2.Columns[j].ToString());
                            }
                            else
                            {
                                DBColumn.Items.Add(dttbl2.Columns[j].ToString());
                                if (dttbl2.Columns.Count == i)
                                {
                                    matchfield += "A." + dttbl2.Columns[j].ToString().Replace(" ", "") + "";
                                    fromquery += "X." + dttbl2.Columns[j].ToString().Replace(" ", "") + "";

                                }
                                if (dttbl2.Columns.Count != i)
                                {
                                    matchfield += "A." + dttbl2.Columns[j].ToString().Replace(" ", "") + ",";
                                    fromquery += "X." + dttbl2.Columns[j].ToString().Replace(" ", "") + ",";
                                }
                                i++;
                            }
                        }
                    }
                    else
                    {
                        DBColumn.Items.Clear(); matchfield = "";
                        i = 1;
                        for (j = 0; j < dttbl2.Columns.Count; j++)
                        {

                            if (DBColumn.Items.Contains(dttbl2.Columns[j].ToString()))
                            {
                                MessageBox.Show(dttbl2.Columns[j].ToString());
                            }
                            else
                            {
                                DBColumn.Items.Add(dttbl2.Columns[j].ToString());
                                if (dttbl2.Columns.Count == i)
                                {
                                    matchfield += "A." + dttbl2.Columns[j].ToString().Replace(" ", "") + "";

                                }
                                if (dttbl2.Columns.Count != i)
                                {
                                    matchfield += "A." + dttbl2.Columns[j].ToString().Replace(" ", "") + ",";
                                }
                                i++;
                            }
                            //matchfield += "A.refno,";
                        }
                    }
                
                ///dbcol = false;
            }
            catch (Exception EX) { }
        }
        public void TableGridLoad1(string s)
        {
            try
            {
                
                    dttbl2.Columns.Clear();
                    dttbl2.Rows.Clear();
                    daimport.query = null;
                daimport.query = "select * from " + Class.Users.ProjectID + "." + s + " ORDER BY 1";
                daimport.ds = Utility.ExecuteSelectQuery(daimport.query, s);
                dttbl2 = daimport.ds.Tables[s];
                    if (dttbl2 != null)
                    {
                        //textBox1.Text = "";
                        TableColumn.Items.Clear(); int j = 0; matchfield1 = "";

                        i = 1; matchfield1 = ""; toquery = "";
                        for (j = 0; j < dttbl2.Columns.Count; j++)
                        {

                            if (TableColumn.Items.Contains(dttbl2.Columns[j].ToString()))
                            {
                                MessageBox.Show(dttbl2.Columns[j].ToString());
                            }
                            else
                            {
                                TableColumn.Items.Add(dttbl2.Columns[j].ToString());
                                if (dttbl2.Columns.Count == i)
                                {
                                    matchfield1 += "A." + CleanColumn(dttbl2.Columns[j].ToString()) + "";
                                    toquery += "X." + CleanColumn(dttbl2.Columns[j].ToString()) + "";

                                }
                                if (dttbl2.Columns.Count != i)
                                {
                                    matchfield1 += "A." + CleanColumn(dttbl2.Columns[j].ToString()) + ",";
                                    toquery += "X." + CleanColumn(dttbl2.Columns[j].ToString()) + ",";
                                }
                                i++;
                            }
                        }

                    }
                    else
                    {
                        textBox1.Refresh();textBox1.Text = "No Data Found in Customer / Supplier ";
                       
                    }

                

            }
            catch (Exception EX) { }
        }
        public DataTable DataGridViewToDataTable(DataGridView dgv)
        {
            DataTable dt = new DataTable();

            // Add Columns
            foreach (DataGridViewColumn column in dgv.Columns)
            {
                dt.Columns.Add(column.HeaderText);
            }

            // Add Rows
            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (!row.IsNewRow)
                {
                    DataRow dr = dt.NewRow();
                        
                    for (int i = 0; i < dgv.Columns.Count; i++)
                    {
                        dr[i] = row.Cells[i].Value;
                    }

                    dt.Rows.Add(dr);
                }
            }

            return dt;
        }
        public void GridLoad()
        {
            try
            {
                string checkTable = $"SELECT TRIGGER_NAME, TABLE_NAME, STATUS FROM USER_TRIGGERS  WHERE TABLE_NAME = '{tableName.ToUpper()}'";

                DataSet ds = Utility.ExecuteSelectQuery(checkTable, tableName);
                DataTable dt = ds.Tables[tableName];

                if (dt.Rows.Count > 0)
                {
                    listfilterdb.Items.Clear();
                    dataGridView2.Rows.Clear();

                    string sel2 = $"SELECT * FROM {tableName}";
                    DataSet ds2 = Utility.ExecuteSelectQuery(sel2, tableName);
                    DataTable dt2 = ds2.Tables[tableName];

                    if (dt2.Rows.Count > 0)
                    {
                        int k = 1;

                        for (int j = 0; j < dt2.Columns.Count; j++)
                        {
                            dataGridView2.Rows.Add();

                            if (j == 0)
                                FromTable1.Items.Add(tableName1);

                            dataGridView2.Rows[j].Cells[0].Value = k.ToString();
                            dataGridView2.Rows[j].Cells[1].Value = CleanColumn(dt2.Columns[j].ColumnName);

                            if (j == 0)
                            {
                                dataGridView2.Rows[j].Cells[3].Value = "DATEID";
                                dataGridView2.Rows[j].Cells[4].Value = "DATEID";
                            }
                            else if (j == 1)
                            {
                                dataGridView2.Rows[j].Cells[3].Value = "DATE1";
                                dataGridView2.Rows[j].Cells[4].Value = "DATE1";
                            }
                            else if (j == 3)
                            {
                                dataGridView2.Rows[j].Cells[3].Value = "TOKENNO";
                                dataGridView2.Rows[j].Cells[4].Value = "INDNO";
                            }

                            else if (j == 4)
                            {
                                dataGridView2.Rows[j].Cells[3].Value = "VEHICLENO";
                                dataGridView2.Rows[j].Cells[4].Value = "VEHICLE";
                            }

                            dataGridView2.Rows[j].DefaultCellStyle.BackColor =j % 2 == 0 ? Color.WhiteSmoke : Color.White;

                            k++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

           
        }
        DataTable dttbl2 = new DataTable();        
        public void News()
        {

            GlobalVariables.DownLoads.Text = "UpLoad"; Class.Users.Intimation = "PAYROLL";
            Class.Users.Bisconnectclear = false;
            allip3.Items.Clear(); butmycompany.Text = "From Company"; buttosupplier.Text = "To Company";
            allislug.Items.Clear();
            allip3.Items.Clear(); progressBar1.Maximum = 0;
            Class.Users.TableName = null; Class.Users.TableNameGrid = null; Class.Users.TableName = ""; Class.Users.TableNameGrid = "";
            Class.Users.TableNameSubGrid = null; Class.Users.Prefix = null; Class.Users.Prefix = null;
            Class.Users.Description = null; Class.Users.Description = null;
            Class.Users.FieldName = null; Class.Users.FieldName = null;
            butheader.BackColor = Class.Users.BackColors;
            this.BackColor = Class.Users.BackColors;
            panel2.BackColor = Class.Users.BackColors;
            panel3.BackColor = Class.Users.BackColors;
            listView1.Font = Class.Users.FontName; listView2.Font = Class.Users.FontName;
            Class.Users.TableName = null; Class.Users.TableNameGrid = null; Class.Users.TableName = ""; Class.Users.TableNameGrid = "";
            Class.Users.TableNameSubGrid = null; Class.Users.Prefix = null; Class.Users.Prefix = null;
            Class.Users.Description = null; Class.Users.Description = null;
            Class.Users.FieldName = null; Class.Users.FieldName = null;
            dataGridView2.ColumnHeadersDefaultCellStyle.BackColor = Class.Users.BackColors;
            if (dataGridView2.Rows.Count > 1)
            {
                daimport.GridRowRemove(dataGridView1);
                daimport.GridRowRemove(dataGridView2);
            }
            GridLoad(); TableLoad();
            bunkfind();
            TableGridLoad(tableName);
            TableGridLoad1(tableName1);
            listView1.Items.Clear();
            listView2.Items.Clear();

            Class.Users.CompCode1 = "";

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

        void bunkfind()
        {
            

            DataTable dt = mas.bunkfind(Class.Users.HCompcode);
            if (dt.Rows.Count == 1)
            {
                combobunk.DisplayMember = "BUNKNAME";
                combobunk.ValueMember = "ASPTBLPETMASID";
                combobunk.DataSource = dt; combobunk.Enabled = false;
            }
            else
            {
                combobunk.DisplayMember = "BUNKNAME";
                combobunk.ValueMember = "ASPTBLPETMASID";
                combobunk.DataSource = dt; combobunk.Enabled = true;
            }
        }
        public void GridView(DataGridView dataGridView1, Button myTablename)
        {

            string MON = "";
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
                       // fileExt = Path.GetExtension(filePath);
                        DataTable dt0;
                        if (!string.IsNullOrEmpty(filePath))
                        {
                            string ext = Path.GetExtension(filePath).ToLower();
                            if (ext == ".xls")

                                dt0 = Class.Master.ReadExcel(filePath, ".xls");
                            else
                                dt0 = Class.Master.ImportExcelToDataTable(filePath);

                            dataGridView3.DataSource = dt0;
                        }
                        else
                        {
                            mas.pop("Excel file doesn't contain", Class.Users.Paramid.ToString(), "");
                        }
                       






                    }

                    int cnt = dataGridView1.Rows.Count;
                    lbldowncount.Text = "Total Excel Rows : " + cnt.ToString();

                }

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }

            Models.Validate val = new Models.Validate();
            
                if (dataGridView3.Rows.Count > 0 && Class.Users.HCompcode != "")
                {
                    if (dataGridView3.Columns.Count >= 7)
                    {
                        listView2.Items.Clear();
                        string frmtable = "", frmrow = "", frmdata = ""; int i = 0;
                        foreach (DataGridViewColumn row in dataGridView3.Columns)
                        {

                            if (i == 0)
                            {
                                if (row.HeaderText.ToLower().Replace("#", "").Trim().Replace(" ", "") == "date")
                                {
                                    frmtable += CleanColumn(row.HeaderText) + "id  INTEGER  primary key";
                                    frmtable += "," + CleanColumn(row.HeaderText) + "1  date";
                                    frmrow += CleanColumn(row.HeaderText)+"1";
                                }
                                else
                                {
                                    MessageBox.Show("First Column Should be Date Column in XL. Pls Change in XL ");
                                    return;
                                }
                            }
                            if (i >= 1)
                            {

                                frmtable += "," + CleanColumn(row.HeaderText) + "  varchar2(100) default null";
                                frmrow += "," + CleanColumn(row.HeaderText);

                            }
                            i++;

                        }
                        if (frmtable != "" && Class.Users.HCompcode != "")
                        {

                            string cre = "";
                            string tf = "SELECT table_name FROM user_tables WHERE table_name = 'TO_" + Class.Users.HCompcode + "'";
                            DataSet ds = Utility.ExecuteSelectQuery(tf, "TO_" + Class.Users.HCompcode);
                            DataTable dt = ds.Tables["TO_AGF"];
                            if (dt.Rows.Count > 0)
                            {
                                cre = "DROP SEQUENCE  " + Class.Users.ProjectID + ".TO_" + Class.Users.HCompcode + "SEQ";
                                Utility.ExecuteNonQuery(cre);
                                cre = "";
                                cre = "DROP TRIGGER  " + Class.Users.ProjectID + ".TO_" + Class.Users.HCompcode + "TRI";
                                Utility.ExecuteNonQuery(cre);
                                cre = "";
                                cre = "DROP TABLE  " + Class.Users.ProjectID + ".TO_" + Class.Users.HCompcode + "";
                                Utility.ExecuteNonQuery(cre);
                            }


                            cre = "";
                            cre = "CREATE TABLE  " + Class.Users.ProjectID + ".TO_" + Class.Users.HCompcode + " (" + frmtable + ")";
                            Utility.ExecuteNonQuery(cre);
                            string tf1 = "SELECT * FROM TO_" + Class.Users.HCompcode;
                            DataSet ds1 = Utility.ExecuteSelectQuery(tf1, "TO_" + Class.Users.HCompcode);
                            DataTable dt1 = ds1.Tables["TO_AGF"];
                            if (dt1.Columns.Count > 0)
                            {
                                cre = "";
                                cre = "CREATE SEQUENCE " + Class.Users.ProjectID + ".TO_" + Class.Users.HCompcode + "SEQ    START WITH 1   MAXVALUE 99999999   MINVALUE 1   NOCYCLE NOCACHE   NOORDER ";
                                Utility.ExecuteNonQuery(cre);

                                cre = "";
                                cre = "CREATE OR REPLACE TRIGGER " + Class.Users.ProjectID + ".TO_" + Class.Users.HCompcode + "TRI BEFORE INSERT ON " + Class.Users.ProjectID + ".TO_" + Class.Users.HCompcode + " REFERENCING NEW AS NEW OLD AS OLD FOR EACH ROW DECLARE " + dt1.Columns[0].ToString() + " INTEGER; BEGIN " + dt1.Columns[0].ToString() + ":= 0;   SELECT TO_" + Class.Users.HCompcode + "SEQ.NEXTVAL INTO " + dt1.Columns[0].ToString() + " FROM DUAL;   :NEW." + dt1.Columns[0].ToString() + ":= " + dt1.Columns[0].ToString() + "; END TO_" + Class.Users.HCompcode + "TRI;";
                                Utility.ExecuteNonQuery(cre);
                            }
                            i = 0; int j = 0; int k = 1;

                            foreach (DataGridViewRow row in dataGridView1.Rows)
                            {
                                if (row.Cells[0].Value.ToString() != "")
                                {
                                    k = 1;
                                    foreach (DataGridViewColumn col in dataGridView1.Columns)
                                    {
                                        if (k == 1)
                                        {
                                            frmdata += "to_Date('" + row.Cells[i + j].Value + "', 'dd-MM-yyyy'),";
                                        }
                                        else if (k == row.Cells.Count)
                                        {
                                            frmdata += "'" + row.Cells[i + j].Value + "'";
                                        }
                                        else
                                        {
                                            frmdata += "'" + row.Cells[i + j].Value + "',";
                                        }
                                        i++; k++;
                                    }
                                    string ins = "";
                                    ins = "INSERT INTO TO_" + Class.Users.HCompcode + " (" + frmrow + ") VALUES(" + frmdata + ");";
                                    Utility.ExecuteNonQuery(ins);

                                    frmdata = "";
                                     i = 0; k++;
                                }
                            }
                            frmtable = "";

                            do
                            {
                                for (i = 0; i < dataGridView1.Rows.Count; i++) { try { dataGridView1.Rows.RemoveAt(i); } catch (Exception) { } }
                            }
                            while (dataGridView1.Rows.Count > 0);
                            GridLoad();

                            MessageBox.Show(Class.Users.CompCode1.ToString() + "  Data Imported Successfully", "");
                            tabControl1.SelectTab(tabPage2);


                        }
                        else
                        {
                            MessageBox.Show("No Data Found  in frmtable");
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Minimum Excel Column should be 7. ", "7 Columns In Excel ");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("No Data Found");
                    return;
                }
            
        }
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Class.Users.UserTime = 0;
            int IDX = e.RowIndex;textBox1.Text = "";

            if (dataGridView2.Columns[e.ColumnIndex].Name == "Show")
            {
     
                try
                {
                    tabControl1.SelectTab(tabPage1);
                    listfilterred.Items.Clear(); listfilterscreen.Items.Clear();
                    listfilterred1.Items.Clear(); listfilterscreen1.Items.Clear();
                    Class.Users.UserTime = 0; 
                
                    listView1.Items.Clear(); listView2.Items.Clear();
                    int i = 0; int gridcount = 1; Class.Users.TableNameGrid = ""; Class.Users.DocID = "";
                    Class.Users.UniqueID = "";  Class.Users.TableNameSubGrid = "";
                    Class.Users.DocID = dataGridView2.Rows[0].Cells[3].EditedFormattedValue.ToString();
                    Class.Users.UniqueID = dataGridView2.Rows[0].Cells[4].EditedFormattedValue.ToString();
                    if (dataGridView2.Rows.Count>0)
                    {
                        Class.Users.CompCode1 = "";
                        Class.Users.CompCode1 = tableName1;
                      
                        foreach (DataGridViewRow gridrow in dataGridView2.Rows)
                        {

                            if (gridrow.Cells[4].Value != null && gridrow.Cells[5].Value != null)
                            {

                                string col1 = CleanColumn(gridrow.Cells[3].Value.ToString());
                                string col2 = CleanColumn(gridrow.Cells[4].Value.ToString());

                                Class.Users.TableNameGrid += $" A.{col1} = B.{col2} AND ";
                                Class.Users.TableNameSubGrid += $" B.{col1} = A.{col2} AND ";
                            }

                        }
                        if (Class.Users.TableNameGrid != "")
                        {
                           
                            update1 = "";
                            update1 = Class.Users.TableNameGrid.Remove(Class.Users.TableNameGrid.Length - 4);
                            Class.Users.Query = "";
                            string sel0 = $"SELECT DISTINCT  {matchfield}, CASE WHEN A.TOKENNO = B.INDNO then 'No' else 'Yes' end STS from { tableName} A  LEFT JOIN {tableName1}  B  ON " + update1 + "; ";
                            textBox1.Text += sel0;                         
                            update1 = "";
                            update1 = Class.Users.TableNameSubGrid.Remove(Class.Users.TableNameSubGrid.Length - 4);

                            Class.Users.Query = $"SELECT DISTINCT  { matchfield1}, CASE WHEN B.TOKENNO = A.INDNO then 'No' else 'Yes' end STS from { tableName1} A  LEFT JOIN {tableName}  B  ON " + update1 + " ;";
                           // DataSet ds1 = Utility.ExecuteSelectQuery(Class.Users.Query, tableName1);
                           //DataTable dt1 = ds1.Tables[tableName1];
                            textBox1.Text += Class.Users.Query.ToString();
                            update1 = ""; Class.Users.Query = "";
                            string[]  split = textBox1.Text.Split(';');
                            i = 1;
                            if (split[0].Length > 0)
                            {
                                listView1.Items.Clear(); listfilterred.Items.Clear(); listfilterscreen.Items.Clear();
                             fdebitamt = 0; fcreditamt = 0;
                            tdebitamt = 0; tcreditamt = 0;
                              
                               
                                string sel = "select distinct " + fromquery + ",x.sts from(" + split[0] + ") x  order by  4";
                                listviewxl3(sel, tableName, tableName1, listView1);

                                
                            }
                            else
                            {
                                MessageBox.Show("No Data Found in MyCompany");
                            }
                            if (split[1].Length > 0)
                            {
                                listView2.Items.Clear(); listfilterred1.Items.Clear(); listfilterscreen1.Items.Clear();
                             tdebitamt = 0; tcreditamt = 0;
                                i = 1;
                                
                                  
                                    string sel = "select distinct " + toquery + ",x.sts from(" + split[1] + ") x  order by  6";
                                    listviewxl4(sel, tableName, tableName1, listView2);

                                    
                            }
                            else
                            {
                                MessageBox.Show("No Data Found in Customer or Supplier");

                            }

                        }
                    }
                    else
                    {
                        MessageBox.Show("Pls Select Matching Fields");
                    }
                }
                catch (Exception ex) { ex.ToString(); }
            }
            if (IDX == 0 && dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].FormattedValue != null)
            {


                Cursor.Current = Cursors.WaitCursor;
                GridColumnRefresh(dataGridView2, IDX);
                Cursor.Current = Cursors.Default;



            }
            


          
            if (IDX >= 1)
            {
                dataGridView2.Rows[e.RowIndex].Cells[2].ReadOnly = true;
            }
            if (celcrear == true)
            {

                Class.Users.Indexer = IDX;
                GridCellClear(dataGridView2, IDX);
                celcrear = false;

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

            //        foreach (ListViewItem item in listfilterdB.Items)
            //        {
            //            ListViewItem list = new ListViewItem();
            //            if (listfilterdB.Items[item0].SubItems[2].ToString().Contains(txtsearch.Text))
            //            {

            //                list.SubItems.Add(listfilterdB.Items[item0].SubItems[1].Text);
            //                list.SubItems.Add(listfilterdB.Items[item0].SubItems[2].Text);


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
            //        //    foreach (ListViewItem item in listfilterdB.Items)
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

        }

        private void listView2_ItemChecked(object sender, ItemCheckedEventArgs e)
        {


        }

      
        void list1Green(string s, string tbl)
        {
            try
            {
                Class.Users.UserTime = 0;
                int i = 0; int gridcount = 1, k = 0, m = 0; string unmatched = "";
                hed = null; hed = s.Split(',');
                string[] hed1 = fromquery.Split(',');
                string[] split = textBox1.Text.Split(';');
                Class.Users.Query = "";
                Class.Users.Query = s; DataTable dt = null;
                DataSet ds = Utility.ExecuteSelectQuery(Class.Users.Query, tbl);
                dt = ds.Tables[tbl];
                i = 1; int j = 0;
                if (dt != null && tbl != "")
                {
                    fdebitamt = 0; fcreditamt = 0;
                   tdebitamt = 0; tcreditamt = 0;
                    i = 1;

                    if (dt.Rows.Count > 0)
                    {
                        progressBar1.Minimum = 0;
                        progressBar1.Maximum = dt.Rows.Count;
                        i = 0;
                        using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Excel Workbook|*.xlsx", ValidateNames = true })
                            if (sfd.ShowDialog() == DialogResult.OK)
                            {
                                Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                                Microsoft.Office.Interop.Excel.Workbook wb = app.Workbooks.Add(Microsoft.Office.Interop.Excel.XlSheetType.xlWorksheet);
                                Microsoft.Office.Interop.Excel.Worksheet ws = (Microsoft.Office.Interop.Excel.Worksheet)app.ActiveSheet;
                                app.Visible = false;
                                for (k = 1; k < hed1.Length; k++)
                                {
                                    ws.Cells[1, k] = hed1[k].Replace("x.", "").ToUpper();
                                }
                                i = 2; j = 0;
                                ws.Range["A1", "H1"].Interior.Color = Microsoft.Office.Interop.Excel.XlRgbColor.rgbNavy;
                                ws.Range["A1", "H1"].Font.Color = Microsoft.Office.Interop.Excel.XlRgbColor.rgbWhite;

                                k = 1; int l = 1;
                                foreach (ListViewItem item in listfilterscreen.Items)
                                {
                                    for (k = 1; k < item.SubItems.Count; k++)
                                    {
                                        ws.Cells[i, l] = item.SubItems[l].Text;
                                        l++;
                                    }
                                    l = 1;
                                    ws.Range["A" + i, "H" + i].Font.Color = Microsoft.Office.Interop.Excel.XlRgbColor.rgbGreen;
                                    decimal per = Convert.ToDecimal(100 / GenFun.ToInt(dt.Rows.Count)) * (j + 1);
                                    lblprogress1.Text = "" + (per).ToString("N0") + " %";
                                    lblprogress1.Refresh();
                                    progressBar1.Value = j;
                                    i++;j++;

                                }
                                ws.Range["A" + i, "H" + i].Interior.Color = Microsoft.Office.Interop.Excel.XlRgbColor.rgbNavy;
                                ws.Columns.AutoFit();
                                wb.SaveAs(sfd.FileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault, Type.Missing, true, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, (Microsoft.Office.Interop.Excel.XlSaveAsAccessMode)Microsoft.Office.Interop.Excel.XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing);
                                app.Quit();
                                MessageBox.Show("Completed"); progressBar1.Maximum = 0;
                            }
                    }
                }
                else
                {
                    MessageBox.Show("No Data Found in MyCompany");
                }

            }

            catch (Exception ex) { ex.ToString(); }


        }
        void List1Red(string s,string tbl)
        {
            try
            {
                Class.Users.UserTime = 0;
                int i = 0; int gridcount = 1, k = 0, m = 0; string unmatched = "";              
                hed = null; hed = s.Split(',');
                string[] hed1 = fromquery.Split(',');
                string[] split = textBox1.Text.Split(';');
                Class.Users.Query = "";
                Class.Users.Query = s; DataTable dt = null;
                DataSet ds = Utility.ExecuteSelectQuery(Class.Users.Query, tbl);
                dt = ds.Tables[tbl];
                i = 1; int j = 0;
                if (dt != null && tbl != "")
                {
              fdebitamt = 0; fcreditamt = 0;
           tdebitamt = 0; tcreditamt = 0;
                    i = 1;

                    if (dt.Rows.Count > 0)
                    {
                        progressBar1.Minimum = 0;
                        progressBar1.Maximum = dt.Rows.Count;
                        i = 0;
                        using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Excel Workbook|*.xlsx", ValidateNames = true })
                            if (sfd.ShowDialog() == DialogResult.OK)
                            {
                                Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                                Microsoft.Office.Interop.Excel.Workbook wb = app.Workbooks.Add(Microsoft.Office.Interop.Excel.XlSheetType.xlWorksheet);
                                Microsoft.Office.Interop.Excel.Worksheet ws = (Microsoft.Office.Interop.Excel.Worksheet)app.ActiveSheet;
                                app.Visible = false;
                                for (k = 1; k < hed1.Length; k++)
                                {
                                    ws.Cells[1, k] = hed1[k].Replace("x.", "").ToUpper();
                                }
                                i = 2; j = 0;
                                ws.Range["A1", "H1"].Interior.Color = Microsoft.Office.Interop.Excel.XlRgbColor.rgbNavy;
                                ws.Range["A1", "H1"].Font.Color = Microsoft.Office.Interop.Excel.XlRgbColor.rgbWhite;

                                k = 1; int l = 1;
                                foreach (ListViewItem item in listfilterred.Items)
                                {
                                    for (k = 1; k < item.SubItems.Count; k++)
                                    {
                                        ws.Cells[i, l] = item.SubItems[l].Text;
                                        l++;
                                    }
                                    l = 1;
                                    ws.Range["A" + i, "H" + i].Font.Color = Microsoft.Office.Interop.Excel.XlRgbColor.rgbRed;
                                    ws.Range["F" + i, "F" + i].Interior.Color = Microsoft.Office.Interop.Excel.XlRgbColor.rgbNavy;
                                    ws.Range["F" + i, "F" + i].Font.Color = Microsoft.Office.Interop.Excel.XlRgbColor.rgbWhite;

                                    decimal per = Convert.ToDecimal(100 / GenFun.ToInt(dt.Rows.Count)) * (j + 1);
                                    lblprogress1.Text = "" + (per).ToString("N0") + " %";
                                    lblprogress1.Refresh();
                                    progressBar1.Value = j;
                                    i++; j++;

                                }
                                ws.Range["A" + i, "H" + i].Interior.Color = Microsoft.Office.Interop.Excel.XlRgbColor.rgbNavy;
                                ws.Columns.AutoFit();
                                wb.SaveAs(sfd.FileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault, Type.Missing, true, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, (Microsoft.Office.Interop.Excel.XlSaveAsAccessMode)Microsoft.Office.Interop.Excel.XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing);
                                app.Quit();
                                MessageBox.Show("Completed"); progressBar1.Maximum = 0;
                            }
                    }
                }
                else
                {
                    MessageBox.Show("No Data Found in MyCompany");
                }

            }

            catch (Exception ex) { ex.ToString(); }

        }
        void list1All(string s, string tbl, string tbl2)
        {
            try
            {
                Class.Users.UserTime = 0;
                int i = 0; int gridcount = 1, k = 0, m = 0; string unmatched = "";
               
                hed = null; hed = s.Split(',');
                string[] hed1 = toquery.Split(','); string[] hed2 = fromquery.Split(',');
                string[] split = textBox1.Text.Split(';');
                Class.Users.Query = "";
                Class.Users.Query = s;
                DataSet ds = Utility.ExecuteSelectQuery(Class.Users.Query, tbl);
                DataTable dt = ds.Tables[tbl];
                i = 1; int j = 0;
                if (dt != null && tbl != "")
                {                   
                   
                    i = 1;k = 0;
                    foreach (DataRow row in dt.Rows)
                    {                       

                        if (row["sts"].ToString() == "No")
                        {
                            if (row.ItemArray[5].ToString() == "")
                            {
                                unmatched = "No Data Found ,";
                            }
                            if (Convert.ToDateTime(row.ItemArray[1].ToString()).ToString("yyyy-MM-dd").Substring(0, 10) != "")
                            {
                                DataTable dt2 = null;
                                dt2 = checkdata(hed1[1].ToString(), tbl2, Convert.ToDateTime(dt.Rows[k]["date1"].ToString()).ToString("yyyy-MM-dd"));
                                if (dt2.Rows.Count>0) {  }
                                else { unmatched += hed1[1].ToString().Replace("x.", "") + ","; }
                            }
                            if (row.ItemArray[5].ToString() != "")
                            {
                                Class.Users.Query = ""; DataTable dt2=null;
                                if (hed1.Length == 8)
                                {                                   
                                    dt2 = checkdata(hed1[4].ToString(), tbl2,dt.Rows[k]["vchno"].ToString());
                                    if (dt2.Rows.Count>0) {  }
                                    else  { unmatched += hed1[4].ToString().Replace("x.", "") + ","; }
                                }
                                else
                                {                                    
                                    dt2 = checkdata(hed1[3].ToString(), tbl2, dt.Rows[k]["vchno"].ToString());
                                    if (dt2.Rows.Count>0) {  }
                                    else { unmatched += hed1[3].ToString().Replace("x.", "") + ","; }
                                }
                            }
                            if (row.ItemArray[6].ToString() != "")
                            {
                                Class.Users.Query = ""; DataTable dt2;
                                if (hed1.Length == 8)
                                {
                                     dt2 = checkdata(hed1[6].ToString(), dt.Rows[k]["debit"].ToString(), tbl2,hed1[4].ToString(),dt.Rows[k]["vchno"].ToString());
                                    if (dt2.Rows.Count>0) {  }
                                    else { unmatched += hed1[6].ToString().Replace("x.", "") + ","; }
                                }
                                else
                                {
                                    dt2 = checkdata(hed1[7].ToString(), dt.Rows[k]["debit"].ToString(), tbl2, hed1[3].ToString(), dt.Rows[k]["vchno"].ToString());
                                    if (dt2.Rows.Count>0) {  }
                                    else { unmatched += hed1[7].ToString().Replace("x.", "") + ","; }
                                }
                            }
                            if (row.ItemArray[7].ToString() != "")
                            {
                                Class.Users.Query = "";
                                DataTable dt2;
                                if (hed1.Length == 8)
                                {
                                    dt2 = checkdata(hed1[7].ToString(), dt.Rows[k]["credit"].ToString(), tbl2, hed1[4].ToString(), dt.Rows[k]["vchno"].ToString());

                                    if (dt2.Rows.Count>0) {  }
                                    else { unmatched += hed1[7].ToString().Replace("x.", "") + ","; }
                                }
                                else
                                {
                                    dt2 = checkdata(hed1[8].ToString(), dt.Rows[k]["credit"].ToString(), tbl2, hed1[3].ToString(), dt.Rows[k]["vchno"].ToString());

                                    if (dt2.Rows.Count>0) {  }
                                    else { unmatched += hed1[8].ToString().Replace("x.", "") + ""; }
                                }
                            }
                            //dt.Rows[k]["unmatched"] = unmatched;
                           
                            unmatched = "";
                           

                        }
                       
                        Class.Users.Query = ""; unmatched = "";
                        decimal per = Convert.ToDecimal(100 / GenFun.ToInt(dt.Rows.Count)) * (j + 1);
                        lblprogress1.Text = "" + (per).ToString("N0") + " %";
                        lblprogress1.Refresh();
                        progressBar1.Value = j;
                        i++; k++;

                  
                    }
                    if (dt.Rows.Count > 0)
                    {
                        progressBar1.Minimum = 0;
                        progressBar1.Maximum = dt.Rows.Count;
                        i = 0;
                        using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Excel Workbook|*.xlsx", ValidateNames = true })
                            if (sfd.ShowDialog() == DialogResult.OK)
                            {
                                Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                                Microsoft.Office.Interop.Excel.Workbook wb = app.Workbooks.Add(Microsoft.Office.Interop.Excel.XlSheetType.xlWorksheet);
                                Microsoft.Office.Interop.Excel.Worksheet ws = (Microsoft.Office.Interop.Excel.Worksheet)app.ActiveSheet;
                                app.Visible = false;
                                for (k = 1; k < hed2.Length; k++)
                                {
                                    ws.Cells[1, k] = hed2[k].Replace("x.", "").ToUpper();
                                }
                                i = 2; j = 0;
                                ws.Range["A1", "I1"].Interior.Color = Microsoft.Office.Interop.Excel.XlRgbColor.rgbNavy;
                                ws.Range["A1", "I1"].Font.Color = Microsoft.Office.Interop.Excel.XlRgbColor.rgbWhite;

                                k = 1; int l = 1;
                                foreach (DataRow item in dt.Rows)
                                {                                    
                                    for (k = 1; k < item.ItemArray.Length; k++)
                                    {
                                        if (item["sts"].ToString() == "No")
                                        {

                                            ws.Range["A" + i, "I" + i].Font.Color = Microsoft.Office.Interop.Excel.XlRgbColor.rgbRed;
                                            ws.Range["H" + i, "H" + i].Interior.Color = Microsoft.Office.Interop.Excel.XlRgbColor.rgbHotPink;
                                            ws.Range["H" + i, "H" + i].Font.Color = Microsoft.Office.Interop.Excel.XlRgbColor.rgbWhite;

                                            ws.Cells[i, l] = item[l].ToString();
                                            l++;
                                        }
                                        else
                                        {
                                            ws.Cells[i, l] = item[l].ToString();
                                            l++;

                                            ws.Range["A" + i, "I" + i].Font.Color = Microsoft.Office.Interop.Excel.XlRgbColor.rgbGreen;
                                           
                                        }

                                    }
                                    //ws.Range["E" + i, "E" + i].Interior.Color = Microsoft.Office.Interop.Excel.XlRgbColor.rgbHotPink;
                                    //ws.Range["E" + i, "E" + i].Font.Color = Microsoft.Office.Interop.Excel.XlRgbColor.rgbWhite;

                                    l = 1;



                                    decimal per = Convert.ToDecimal(100 / GenFun.ToInt(dt.Rows.Count)) * (j + 1);
                                    lblprogress1.Text = "" + (per).ToString("N0") + " %";
                                    lblprogress1.Refresh();

                                    progressBar1.Value = j;
                                    i++; j++;

                                }
                                ws.Range["A" + i, "J" + i].Interior.Color = Microsoft.Office.Interop.Excel.XlRgbColor.rgbNavy;
                                ws.Columns.AutoFit();
                                wb.SaveAs(sfd.FileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault, Type.Missing, true, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, (Microsoft.Office.Interop.Excel.XlSaveAsAccessMode)Microsoft.Office.Interop.Excel.XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing);
                                app.Quit(); Cursor.Current = Cursors.Default;
                                MessageBox.Show("Excel Download Completed"); progressBar1.Maximum = 0;
                                
                            }
                    }
                }
                else
                {
                    MessageBox.Show("No Data Found in MyCompany");
                    Cursor.Current = Cursors.Default;
                }

            }

            catch (Exception ex) { ex.ToString(); }

        }
        void list2Green(string s, string tbl)
        {
            try
            {
                Class.Users.UserTime = 0;
                int i = 0; int gridcount = 1, k = 0, m = 0; string unmatched = "";
                hed = null; hed = s.Split(',');
                string[] hed1 = toquery.Split(',');
                string[] split = textBox1.Text.Split(';');
                Class.Users.Query = "";
                Class.Users.Query = s; DataTable dt = null;
                DataSet ds = Utility.ExecuteSelectQuery(Class.Users.Query, tbl);
                dt = ds.Tables[tbl];
                i = 1; int j = 0;
                if (dt != null && tbl != "")
                {
 fdebitamt = 0; fcreditamt = 0;
                   tdebitamt = 0; tcreditamt = 0;
                    i = 1;
                    //if (dt.Rows.Count > 0)
                    //{
                    //    progressBar1.Minimum = 0;
                    //    progressBar1.Maximum = dt.Rows.Count;
                    //    i = 0;
                    //    using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Excel Workbook|*.xlsx", ValidateNames = true })
                    //        if (sfd.ShowDialog() == DialogResult.OK)
                    //        {
                    //            Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                    //            Microsoft.Office.Interop.Excel.Workbook wb = app.Workbooks.Add(Microsoft.Office.Interop.Excel.XlSheetType.xlWorksheet);
                    //            Microsoft.Office.Interop.Excel.Worksheet ws = (Microsoft.Office.Interop.Excel.Worksheet)app.ActiveSheet;
                    //            app.Visible = false;
                    //            for (k = 1; k < hed1.Length; k++)
                    //            {
                    //                ws.Cells[1, k] = hed1[k].Replace("x.", "").ToUpper();
                    //            }
                    //            i = 2; j = 0;
                    //            ws.Range["A1", "J1"].Interior.Color = Microsoft.Office.Interop.Excel.XlRgbColor.rgbNavy;
                    //            ws.Range["A1", "J1"].Font.Color = Microsoft.Office.Interop.Excel.XlRgbColor.rgbWhite;

                    //            k = 1; int l = 1;
                    //            foreach (DataRow item in dt.Rows)
                    //            {
                    //                for (k = 1; k < item.ItemArray.Length; k++)
                    //                {
                    //                    if (item["sts"].ToString() == "No")
                    //                    {
                    //                        ws.Range["A" + i, "J" + i].Font.Color = Microsoft.Office.Interop.Excel.XlRgbColor.rgbRed;

                    //                        ws.Cells[i, l] = item[l].ToString();
                    //                        l++;
                    //                    }
                    //                    else
                    //                    {
                    //                        ws.Range["A" + i, "J" + i].Font.Color = Microsoft.Office.Interop.Excel.XlRgbColor.rgbGreen;

                    //                        ws.Cells[i, l] = item[l].ToString();
                    //                        l++;
                    //                    }
                    //                }
                    //                l = 1;
                    //                decimal per = Convert.ToDecimal(100 / GenFun.ToInt(dt.Rows.Count)) * (j + 1);
                    //                lblprogress1.Text = "" + (per).ToString("N0") + " %";
                    //                lblprogress1.Refresh();
                    //                progressBar1.Value = j;
                    //                i++; j++;

                    //            }
                    //            ws.Range["A" + i, "J" + i].Interior.Color = Microsoft.Office.Interop.Excel.XlRgbColor.rgbNavy;
                    //            ws.Columns.AutoFit();
                    //            wb.SaveAs(sfd.FileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault, Type.Missing, true, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, (Microsoft.Office.Interop.Excel.XlSaveAsAccessMode)Microsoft.Office.Interop.Excel.XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing);
                    //            app.Quit();
                    //            MessageBox.Show("Completed"); progressBar1.Maximum = 0;
                    //        }
                    //}
                    if (dt.Rows.Count > 0)
                    {
                        progressBar1.Minimum = 0;
                        progressBar1.Maximum = dt.Rows.Count;
                        i = 0;
                        using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Excel Workbook|*.xlsx", ValidateNames = true })
                            if (sfd.ShowDialog() == DialogResult.OK)
                            {
                                Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                                Microsoft.Office.Interop.Excel.Workbook wb = app.Workbooks.Add(Microsoft.Office.Interop.Excel.XlSheetType.xlWorksheet);
                                Microsoft.Office.Interop.Excel.Worksheet ws = (Microsoft.Office.Interop.Excel.Worksheet)app.ActiveSheet;
                                app.Visible = false;
                                for (k = 1; k < hed1.Length; k++)
                                {
                                    ws.Cells[1, k] = hed1[k].Replace("x.", "").ToUpper();
                                }
                                i = 2; j = 0;
                                ws.Range["A1", "I1"].Interior.Color = Microsoft.Office.Interop.Excel.XlRgbColor.rgbNavy;
                                ws.Range["A1", "I1"].Font.Color = Microsoft.Office.Interop.Excel.XlRgbColor.rgbWhite;

                                k = 1; int l = 1;
                                foreach (ListViewItem item in listfilterscreen1.Items)
                                {
                                    for (k = 1; k < item.SubItems.Count; k++)
                                    {
                                        ws.Cells[i, l] = item.SubItems[l].Text;
                                        l++;
                                    }
                                    l = 1;
                                    ws.Range["A" + i, "I" + i].Font.Color = Microsoft.Office.Interop.Excel.XlRgbColor.rgbGreen;
                                    decimal per = Convert.ToDecimal(100 / GenFun.ToInt(dt.Rows.Count)) * (j + 1);
                                    lblprogress1.Text = "" + (per).ToString("N0") + " %";
                                    lblprogress1.Refresh();
                                    progressBar1.Value = j;
                                    i++;j++;

                                }
                                ws.Range["A" + i, "I" + i].Interior.Color = Microsoft.Office.Interop.Excel.XlRgbColor.rgbNavy;
                                ws.Columns.AutoFit();
                                wb.SaveAs(sfd.FileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault, Type.Missing, true, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, (Microsoft.Office.Interop.Excel.XlSaveAsAccessMode)Microsoft.Office.Interop.Excel.XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing);
                                app.Quit();
                                MessageBox.Show("Completed"); progressBar1.Maximum = 0;
                            }
                    }
                }
                else
                {
                    MessageBox.Show("No Data Found in MyCompany");
                }

            }

            catch (Exception ex) { ex.ToString(); }


        }
        void List2Red(string s, string tbl)
        {
            try
            {
                Class.Users.UserTime = 0;
                int i = 0; int gridcount = 1, k = 0, m = 0; string unmatched = "";
                hed = null; hed = s.Split(',');
                string[] hed1 = toquery.Split(',');
                string[] split = textBox1.Text.Split(';');
                Class.Users.Query = "";
                Class.Users.Query = s; DataTable dt = null;
                DataSet ds = Utility.ExecuteSelectQuery(Class.Users.Query, tbl);
                dt = ds.Tables[tbl];
                i = 1; int j = 0;
                if (dt != null && tbl != "")
                {
             fdebitamt = 0; fcreditamt = 0;
                    tdebitamt = 0; tcreditamt = 0;
                    i = 1;

                    if (dt.Rows.Count > 0)
                    {
                        progressBar1.Minimum = 0;
                        progressBar1.Maximum = dt.Rows.Count;
                        i = 0;
                        using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Excel Workbook|*.xlsx", ValidateNames = true })
                            if (sfd.ShowDialog() == DialogResult.OK)
                            {
                                Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                                Microsoft.Office.Interop.Excel.Workbook wb = app.Workbooks.Add(Microsoft.Office.Interop.Excel.XlSheetType.xlWorksheet);
                                Microsoft.Office.Interop.Excel.Worksheet ws = (Microsoft.Office.Interop.Excel.Worksheet)app.ActiveSheet;
                                app.Visible = false;
                                for (k = 1; k < hed1.Length; k++)
                                {
                                    ws.Cells[1, k] = hed1[k].Replace("x.", "").ToUpper();
                                }
                                i = 2; j = 0;
                                ws.Range["A1", "I1"].Interior.Color = Microsoft.Office.Interop.Excel.XlRgbColor.rgbNavy;
                                ws.Range["A1", "I1"].Font.Color = Microsoft.Office.Interop.Excel.XlRgbColor.rgbWhite;

                                k = 1; int l = 1;
                                foreach (ListViewItem item in listfilterred1.Items)
                                {
                                    for (k = 1; k < item.SubItems.Count; k++)
                                    {
                                        ws.Cells[i, l] = item.SubItems[l].Text;
                                        l++;
                                    }
                                    l = 1;
                                    ws.Range["A" + i, "I" + i].Font.Color = Microsoft.Office.Interop.Excel.XlRgbColor.rgbRed;
                                    ws.Range["G" + i, "G" + i].Interior.Color = Microsoft.Office.Interop.Excel.XlRgbColor.rgbNavy;
                                    ws.Range["G" + i, "G" + i].Font.Color = Microsoft.Office.Interop.Excel.XlRgbColor.rgbWhite;
                                    decimal per = Convert.ToDecimal(100 / GenFun.ToInt(dt.Rows.Count)) * (j + 1);
                                    lblprogress1.Text = "" + (per).ToString("N0") + " %";
                                    lblprogress1.Refresh();
                                    progressBar1.Value = j;
                                    i++;j++;

                                }
                                ws.Range["A" + i, "I" + i].Interior.Color = Microsoft.Office.Interop.Excel.XlRgbColor.rgbNavy;
                                ws.Columns.AutoFit();
                                wb.SaveAs(sfd.FileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault, Type.Missing, true, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, (Microsoft.Office.Interop.Excel.XlSaveAsAccessMode)Microsoft.Office.Interop.Excel.XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing);
                                app.Quit();
                                MessageBox.Show("Completed"); progressBar1.Maximum = 0;
                            }
                    }
                }
                else
                {
                    MessageBox.Show("No Data Found in MyCompany");
                }

            }

            catch (Exception ex) { ex.ToString(); }

        }
        void list2All(string s, string tbl, string tbl2)
        {
            try
            {
                Class.Users.UserTime = 0; Cursor.Current = Cursors.WaitCursor;
                int i = 0; int gridcount = 1, k = 0, m = 0; string unmatched = "";
                hed = null; hed = s.Split(',');
                string[] hed1 = fromquery.Split(','); string[] hed2 = toquery.Split(',');
                string[] split = textBox1.Text.Split(';');
                Class.Users.Query = "";
                Class.Users.Query = s; DataTable dt = null;
                DataSet ds = Utility.ExecuteSelectQuery(Class.Users.Query, tbl);
                dt = ds.Tables[tbl];
                i = 1; int j = 0;
                if (dt != null && tbl != "")
                {
                    i = 1;
                    foreach (DataRow row in dt.Rows)
                    {
                        unmatched = "";
                        if (row["sts"].ToString() == "No")
                        {
                            if (row.ItemArray[5].ToString() == "")
                            {
                                unmatched = "No Data Found ,";
                            }
                            if (Convert.ToDateTime(row.ItemArray[1].ToString()).ToString("yyyy-MM-dd").Substring(0, 10) != "")
                            {
                                DataTable dt2 = null;
                                dt2 = checkdata(hed1[1].ToString(), tbl, Convert.ToDateTime(dt.Rows[k]["date1"].ToString()).ToString("yyyy-MM-dd"));
                                if (dt2.Rows.Count>0)
                                { }
                                else { unmatched += hed1[1].ToString().Replace("x.", "") + ","; }

                            }
                            if (row.ItemArray[5].ToString() != "")
                            {
                                Class.Users.Query = ""; DataTable dt2 = null;
                                if (hed1.Length == 8)
                                {
                                    dt2 = checkdata(hed1[4].ToString(), tbl, dt.Rows[k]["refno"].ToString());
                                    if (dt2.Rows.Count > 0)
                                    { }
                                    else { unmatched += hed1[4].ToString().Replace("x.", "") + ","; }
                                }
                                else
                                {
                                    dt2 = checkdata(hed1[5].ToString(), tbl, dt.Rows[k]["refno"].ToString());
                                    if (dt2.Rows.Count > 0){ }  else { unmatched += hed1[5].ToString().Replace("x.", "") + ","; }
                                }
                            }
                            if (dt.Rows[k]["debit"].ToString() != "")
                            {
                                Class.Users.Query = ""; DataTable dt2 = null;
                                if (hed1.Length == 8)
                                {
                                    dt2 = checkdata(hed1[5].ToString(), dt.Rows[j]["debit"].ToString(), tbl, hed1[4].ToString(), dt.Rows[k]["refno"].ToString());
                                    if (dt2.Rows.Count > 0) {  }
                                    else { unmatched += hed1[5].ToString().Replace("x.", "") + ","; }
                                }
                                else
                                {
                                    dt2 = checkdata(hed1[6].ToString(), dt.Rows[k]["debit"].ToString(), tbl, hed1[5].ToString(), dt.Rows[k]["refno"].ToString());
                                    if (dt2.Rows.Count > 0) {  }
                                    else { unmatched += hed1[6].ToString().Replace("x.", "") + ","; }
                                }
                            }
                            if (dt.Rows[k]["credit"].ToString() != "")
                            {
                                Class.Users.Query = "";
                                DataTable dt2=null;
                                    if (hed1.Length == 8)
                                    {
                                        dt2 = checkdata(hed1[6].ToString(), dt.Rows[k]["credit"].ToString(), tbl, hed1[4].ToString(), dt.Rows[k]["refno"].ToString());
                                        if (dt2.Rows.Count > 0) { }
                                        else { unmatched += hed1[6].ToString().Replace("x.", "") + ""; }
                                       
                                    }
                                    else
                                    {
                                        dt2 = checkdata(hed1[7].ToString(), dt.Rows[k]["credit"].ToString(), tbl, hed1[5].ToString(), dt.Rows[k]["refno"].ToString());
                                        if (dt2.Rows.Count > 0) {  }
                                        else { unmatched += hed1[7].ToString().Replace("x.", "") + ""; }
                                        
                                    }
                            }

                           
                        }                   
                        
                           // dt.Rows[k]["unmatched"] = unmatched;
                           
                        

                                    
                        Class.Users.Query = ""; unmatched = "";
                        decimal per = Convert.ToDecimal(100 / GenFun.ToInt(dt.Rows.Count)) * (j + 1);
                        lblprogress1.Text = "" + (per).ToString("N0") + " %";
                        lblprogress1.Refresh();
                        progressBar1.Value = j;
                        i++; k++;                      
                    }
                    if (dt.Rows.Count > 0)
                    {
                        progressBar1.Minimum = 0;
                        progressBar1.Maximum = dt.Rows.Count;
                        i = 0;
                        using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Excel Workbook|*.xlsx", ValidateNames = true })
                            if (sfd.ShowDialog() == DialogResult.OK)
                            {
                                Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                                Microsoft.Office.Interop.Excel.Workbook wb = app.Workbooks.Add(Microsoft.Office.Interop.Excel.XlSheetType.xlWorksheet);
                                Microsoft.Office.Interop.Excel.Worksheet ws = (Microsoft.Office.Interop.Excel.Worksheet)app.ActiveSheet;
                                app.Visible = false;
                                for (k = 1; k < hed2.Length; k++)
                                {
                                    ws.Cells[1, k] = hed2[k].Replace("x.", "").ToUpper();
                                }
                                i = 2; j = 0;
                                ws.Range["A1", "J1"].Interior.Color = Microsoft.Office.Interop.Excel.XlRgbColor.rgbNavy;
                                ws.Range["A1", "J1"].Font.Color = Microsoft.Office.Interop.Excel.XlRgbColor.rgbWhite;

                                k = 1; int l = 1;
                                foreach (DataRow item in dt.Rows)
                                {
                                    for (k = 1; k < item.ItemArray.Length; k++)
                                    {
                                        if (item["sts"].ToString() == "No")
                                        {
                                            ws.Range["A" + i, "J" + i].Font.Color = Microsoft.Office.Interop.Excel.XlRgbColor.rgbRed;
                                            ws.Range["I" + i, "I" + i].Interior.Color = Microsoft.Office.Interop.Excel.XlRgbColor.rgbHotPink;
                                            ws.Range["I" + i, "I" + i].Font.Color = Microsoft.Office.Interop.Excel.XlRgbColor.rgbWhite;

                                            ws.Cells[i, l] = item[l].ToString();
                                            l++;
                                        }
                                        else
                                        {
                                            ws.Range["A" + i, "J" + i].Font.Color = Microsoft.Office.Interop.Excel.XlRgbColor.rgbGreen;
                                            ws.Cells[i, l] = item[l].ToString();
                                            l++;
                                        }
                                    }
                                  
                                    l = 1;
                                    decimal per = Convert.ToDecimal(100 / GenFun.ToInt(dt.Rows.Count)) * (j + 1);
                                    lblprogress1.Text = "" + (per).ToString("N0") + " %";
                                    lblprogress1.Refresh();
                                    progressBar1.Value = j;
                                    i++; j++;

                                }
                                ws.Range["A" + i, "J" + i].Interior.Color = Microsoft.Office.Interop.Excel.XlRgbColor.rgbNavy;
                                ws.Columns.AutoFit();
                                wb.SaveAs(sfd.FileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault, Type.Missing, true, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, (Microsoft.Office.Interop.Excel.XlSaveAsAccessMode)Microsoft.Office.Interop.Excel.XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing);
                                app.Quit();
                                MessageBox.Show("Completed"); progressBar1.Maximum = 0; Cursor.Current = Cursors.Default;
                            }
                    }
                }
                else
                {
                    MessageBox.Show("No Data Found in MyCompany"); Cursor.Current = Cursors.Default;
                }

            }

            catch (Exception ex) { ex.ToString(); Cursor.Current = Cursors.Default; }
            Cursor.Current = Cursors.Default;

        }
        private DataTable checkdata(string param1,string tbl,string val)
        {

           string qry = "select distinct " + param1 + " from " + tbl + " x where " + param1 + "='" + val + "';";
            DataSet ds0 = Utility.ExecuteSelectQuery(qry, tbl);
            DataTable dt0 = ds0.Tables[tbl];
            return dt0;
        }
        private DataTable checkdata(string param1,string val1, string tbl, string param2,string val2)
        {

            string qry = "select distinct " + param1 + " from " + tbl + " x where " + param2 + "='" + val2 + "' and " + param1 + "='" + val1 + "';";
            DataSet ds0 = Utility.ExecuteSelectQuery(qry, tbl);
            DataTable dt0 = ds0.Tables[tbl];
            return dt0;
        }
        void listviewxl3(string s, string tbl, string tbl2,ListView listview)
        {
            try
            {
                Class.Users.UserTime = 0; listview.BeginUpdate();
                int i = 0; int gridcount = 1, k = 0, m = 0; string unmatched = "";
                listview.Items.Clear(); listview.Columns.Clear();
                hed = null; hed = s.Split(',');
                string[] hed1 = toquery.Split(','); string[] hed2 = fromquery.Split(',');
                string[] split = textBox1.Text.Split(';');
                Class.Users.Query = "";
                Class.Users.Query = s;
                DataSet ds = Utility.ExecuteSelectQuery(Class.Users.Query, tbl);
                DataTable dt = ds.Tables[tbl];
                i = 1; int j = 0;
                if (dt.Rows.Count>0 && tbl != "")
                {
                    listfilterred.Items.Clear(); listfilterscreen.Items.Clear();
                    fdebitamt = 0; fcreditamt = 0;
             tdebitamt = 0; tcreditamt = 0;
                    i = 0;
                    foreach (string heders in hed2)
                    {
                        listview.Columns.Add(heders.ToString().Replace("x.","").ToUpper());
                        listview.Columns[i].Width =    i == 0 ? 30 :i ==4 ? 120 : i == 6 ? 250 : 80;                       
                        i++;
                    }
                    i = 1;
                        foreach (DataRow row in dt.Rows)
                    {
                        ListViewItem list = new ListViewItem();
                        int c = 0;
                        for (c = 0; c < dt.Columns.Count; c++)
                        {
                            if (c == 0) { list.Text = i.ToString(); }
                            else
                            {
                                list.SubItems.Add(row[c].ToString());
                            }
                            
                        }
                        if (row["STS"].ToString() == "No")
                        {
                            list.ForeColor = Color.Red;
                            list.Font = new Font(list.Font, FontStyle.Bold);
                        }
                        else
                        {
                            list.ForeColor = Color.Black;
                            list.Font = new Font(list.Font, FontStyle.Regular);
                        }
                        fdebitamt += Convert.ToDecimal("0");
                        fcreditamt += Convert.ToDecimal("0");
                  
                        listview.Items.Add(list);
                        unmatched = "";
          
                        Class.Users.Query = ""; unmatched = "";                      
                        decimal per = ((decimal)(j + 1) / dt.Rows.Count) * 100;
                        lblprogress1.Text = "" + (per).ToString("N0") + " %";
                        lblprogress1.Refresh();
                        progressBar1.Value = j; progressBar1.Maximum = dt.Rows.Count;
                        i++; k++;

                       
                    }
                    
                }
                else
                {
                    MessageBox.Show("No Data Found in MyCompany");
                }

            }

            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally { listview.EndUpdate(); }

        }
        void listviewxl4(string s, string tbl, string tbl2,ListView listView)
        {
            try
            {
                Class.Users.UserTime = 0; 
                int i = 1; int  k = 0, m = 0; 
                listView.Items.Clear(); listView.Columns.Clear(); listView.BeginUpdate();
                hed = null; hed = s.Split(',');
                string[] hed1 = fromquery.Split(','); string[] hed2 = toquery.Split(',');
                string[] split = textBox1.Text.Split(';');
                Class.Users.Query = "";
                Class.Users.Query = s; DataTable dt = null;
                DataSet ds = Utility.ExecuteSelectQuery(Class.Users.Query, tbl2);
                dt = ds.Tables[tbl2];
                i = 1; int j = 0;
                if (dt.Rows.Count>0 && tbl != "")
                {
                    listfilterred1.Items.Clear(); listfilterscreen1.Items.Clear();
 fdebitamt = 0; fcreditamt = 0;
                   tdebitamt = 0; tcreditamt = 0;
                    i = 0;
                    foreach (string heders in hed2)
                    {
                        listView.Columns.Add(heders.ToString().Replace("x.", "").ToUpper());
                        listView.Columns[i].Width = i == 0 ? 30 : i == 4 ? 100 : i == 6 ? 150 : 100;
                        i++;
                    }
                    i = 1;
                    foreach (DataRow row in dt.Rows)
                    {
                        ListViewItem list = new ListViewItem();

                        int c = 0;
                        for (c = 0; c < dt.Columns.Count; c++)
                        {
                            if (c == 0) { list.Text = i.ToString(); }
                            else
                            {
                                list.SubItems.Add(row[c].ToString());
                            }

                        }

                        if (row["STS"].ToString() == "No")
                        {
                            list.ForeColor = Color.Red;                            
                            list.Font = new Font(list.Font.FontFamily, 10, FontStyle.Bold);
                        }
                        else
                        {
                            list.ForeColor = Color.Black;
                            list.Font = new Font(list.Font, FontStyle.Regular);
                        }
                        unmatched = "";
                        fdebitamt += Convert.ToDecimal("0" + row["Debit"].ToString());
                        fcreditamt += Convert.ToDecimal("0" + row["Credit"].ToString());
                        listView.Items.Add(list);i++;
                    }

                    Class.Users.Query = ""; unmatched = "";

                    decimal per = ((decimal)(k + 1) / dt.Rows.Count) * 100;
                    lblprogress1.Text = "" + (per).ToString("N0") + " %";
                    lblprogress1.Refresh();
                    progressBar1.Value = j; progressBar1.Maximum = dt.Rows.Count;
                   
                     k++;


                }

            }
            catch (Exception ex) { MessageBox.Show("From Table and To Table  Column MisMatch  . "+ex.Message); }
            finally { listView.EndUpdate(); }

        }
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
        }



        private void tabControl1_TabIndexChanged(object sender, EventArgs e)
        {

        }
        void TableLoad()
        {
            //if (buttosupplier.Text != "")
            //{
            //    string sel1 = "show  tables  where Tables_in_" + Class.Users.ProjectID + " like'%my_%' ";
            //    DataSet ds = Utility.ExecuteSelectQuery(sel1, buttosupplier.Text.ToLower().Trim());
            //    DataTable dt = ds.Tables[buttosupplier.Text.ToLower().Trim()];
            //    combodrop.DataSource = null;
            //    if (dt.Rows.Count > 0 || dt != null)
            //    {

            //        combodrop.DataSource = dt;
            //        combodrop.DisplayMember = "Tables_in_" + Class.Users.ProjectID + "";
            //        combodrop.ValueMember = "Tables_in_" + Class.Users.ProjectID + "";
            //    }
            //}
        }
        decimal fdebitamt, fcreditamt, tdebitamt, tcreditamt = 0;
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {


            if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage2"])
            {
                if (Class.Users.IPADDRESS == "192.168.101.15")
                {
                    textBox1.Visible = true;
                }
                else
                {
                    textBox1.Visible = false;
                }
            }
            //if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage2"])
            //{
            //    Class.Users.CompCode1 = "";
            //}
            //if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage1"])
            //{
            //    Class.Users.CompCode1 = "";
            //}
            //if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage5"])
            //{
            //    Class.Users.CompCode1 = "";
            //}
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

        }

        private void button2_Click_1(object sender, EventArgs e)
        {




        }

        private void txtsearchtable_TextChanged(object sender, EventArgs e)
        {

        }

        private void tableNameToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        // bool dbcol = false;
        void GridColumnRefresh(DataGridView grid, int idx)
        {

            if (grid.Rows[idx].Cells[2].FormattedValue.ToString() != "" && grid.Columns[2].Name == "FromTable1")
            {
                int lines = idx;
                //lblfieldcount.Refresh();
                //lblfieldcount.Text = lines.ToString();
                if (lines % 2 == 0)
                {
                    grid.Rows[idx].DefaultCellStyle.BackColor = Color.AntiqueWhite;
                    // Class.Users.TableName = grid.Rows[idx].Cells[2].FormattedValue.ToString();

                }
                else
                {
                    grid.Rows[idx].DefaultCellStyle.BackColor = Color.LightPink;
                    //Class.Users.TableNameGrid = grid.Rows[idx].Cells[2].FormattedValue.ToString();

                }

                TableGridLoad(tableName);
                TableGridLoad1(tableName1);
            }

        }

        void GridCellClear(DataGridView grid, int idx)
        {
            //if (grid.Rows[idx].Cells[4].FormattedValue.ToString() != "")
            //{

            grid.Rows[idx].Cells[5].Value = string.Empty;
            grid.Rows[idx].Cells[4].Value = string.Empty;
            celcrear = false;
            //}
            //else
            //{

            //    grid.Rows[idx].Cells[3].Value = string.Empty;
            //    celcrear = false;
            //}

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
            //if (e.ColumnIndex == 2)
            //{
            //    dataGridView3.Rows[0].Cells[4].Value = DBNull.Value;
            //    Prefix.DataSource = null;
            //    string sel = "SELECT  DISTINCT A.PREFIX FROM " + Class.Users.ProjectID + ".AUTOGENERATE A  WHERE   A.TX_VIEW_ID='" + Class.Users.TableNameGrid + "' AND  A.PROJECTID='" + Class.Users.ProjectID + "'  AND  A.DESCRIPTION='" + dataGridView3.Rows[0].Cells[2].FormattedValue.ToString() + "'  ORDER BY 1";
            //    DataSet ds2 = Utility.ExecuteSelectQuery(sel, "AUTOGENERATE");
            //    DataTable dt2 = ds2.Tables["AUTOGENERATE"];
            //    if (dt2.Rows.Count > 0)
            //    {
            //        Prefix.DataSource = dt2;
            //        Prefix.DisplayMember = "PREFIX";
            //        Prefix.ValueMember = "PREFIX";
            //    }
            //    if (Class.Users.TableNameGrid == "")
            //    {
            //        Class.Users.TableNameGrid = dataGridView3.Rows[0].Cells[1].FormattedValue.ToString();
            //    }
            //}
        }

        private void dataGridView2_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {



        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Class.Users.UserTime = 0;





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

        }

        private void dBColumnRefresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //dbcol = true;
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

        private void dataGridView2_DataError_1(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void butsubmit_Click(object sender, EventArgs e)
        {

        }

        private void FilterRedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listfilterred.Items.Count > 0)
            {

                MessageBox.Show("MyCompany- UnMatched Rows are : " + listfilterred.Items.Count.ToString());

            }
        }

        private void FilterGreenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listfilterscreen.Items.Count > 0)
            {

                MessageBox.Show("MyCompany- Matched Rows are : " + listfilterscreen.Items.Count.ToString());
            }
        }

        private void filterRedToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (listfilterred1.Items.Count > 0)
            {

                MessageBox.Show("Customer or Supplier -UnMatched Rows are : " + listfilterred1.Items.Count.ToString());

            }
        }

        private void filterGreenToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (listfilterscreen1.Items.Count > 0)
            {

                MessageBox.Show("Customer or Supplier - Matched Rows are : " + listfilterscreen1.Items.Count.ToString());

            }
        }
      
       
        string toquery = "", fromquery = "";

       
        private void exportToXLRedToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (dataGridView2.Rows[0].Cells[2].EditedFormattedValue.ToString() != "")
            {
                string[] split = textBox1.Text.Split(';');
                string sel = "select " + toquery + ",x.sts from(" + split[1] + ") x  where x.sts='No'";
                List2Red(sel,"to_" + dataGridView2.Rows[0].Cells[2].EditedFormattedValue.ToString());
            }
            else { MessageBox.Show("No data Found", "Null"); }
        }

        private void exportToXLGreenToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (dataGridView2.Rows[0].Cells[2].EditedFormattedValue.ToString() != "")
            {
                string[] split = textBox1.Text.Split(';');
                string sel = "select " + toquery + ",x.sts from(" + split[1] + ") x  where x.sts='Yes'";
                list2Green(sel, "to_" + dataGridView2.Rows[0].Cells[2].EditedFormattedValue.ToString());
            }
            else { MessageBox.Show("No data Found", "Null"); }
        }
        private void allToolStripMenuItem1_Click(object sender, EventArgs e)
        {
           
            if (dataGridView2.Rows[0].Cells[2].EditedFormattedValue.ToString() != "")
            {
                Cursor.Current = Cursors.WaitCursor;
                string[] split = textBox1.Text.Split(';');
                string sel = "select " + toquery + ",x.sts from(" + split[1] + ") x";
                list2All(sel, "my_" + dataGridView2.Rows[0].Cells[2].EditedFormattedValue.ToString(), "to_" + dataGridView2.Rows[0].Cells[2].EditedFormattedValue.ToString());
                Cursor.Current = Cursors.Default;
            }
            else { MessageBox.Show("No data Found", "Null"); }
         
        }
        private void exportToXLRedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView2.Rows[0].Cells[2].EditedFormattedValue.ToString() != "")
            {
                string[] split = textBox1.Text.Split(';');
                string sel = "select " + fromquery + ",x.sts from(" + split[0] + ") x  where x.sts='No'";
                List1Red(sel, "my_" + dataGridView2.Rows[0].Cells[2].EditedFormattedValue.ToString());

            }
            else { MessageBox.Show("No data Found", "Null"); }
        }

        private void exportToXLGreenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView2.Rows[0].Cells[2].EditedFormattedValue.ToString() != "")
            {
                string[] split = textBox1.Text.Split(';');
                string sel = "select " + fromquery + ",x.sts from(" + split[0] + ") x  where x.sts='Yes'";
                list1Green(sel, "my_" + dataGridView2.Rows[0].Cells[2].EditedFormattedValue.ToString());
            }           
        }
        private void allToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView2.Rows[0].Cells[2].EditedFormattedValue.ToString() != "")
            {
                string[] split = textBox1.Text.Split(';');
                string sel = "select " + fromquery + ",x.sts from(" + split[0] + ") x";
                list1All(sel, "my_" + dataGridView2.Rows[0].Cells[2].EditedFormattedValue.ToString(), "to_" + dataGridView2.Rows[0].Cells[2].EditedFormattedValue.ToString());

            }
            else { MessageBox.Show("No data Found","Null"); }
        }
        string[] hed;
        private void unMachedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            

            //try
            //{
                
            //    Class.Users.UserTime = 0;

            //    int i = 0; int gridcount = 1, k = 0; string unmatched = "";
                
            //    hed = null; hed = fromquery.Split(',');
            //    string[] split = textBox1.Text.Split(';');               
            //    Class.Users.Query = "";
            //    Class.Users.Query = "select " + fromquery + ",x.sts from(" + split[0] + ") x where x.sts='No'";
            //    DataSet ds = Utility.ExecuteSelectQuery(Class.Users.Query, "my_" + dataGridView2.Rows[0].Cells[2].EditedFormattedValue.ToString());
            //    DataTable dt = ds.Tables["my_"+ dataGridView2.Rows[0].Cells[2].EditedFormattedValue.ToString()];
            //    i = 1; int j = 0; 
            //    if (dt != null && dataGridView2.Rows[0].Cells[2].EditedFormattedValue.ToString() != "")
            //    {
            //         listfilterred.Items.Clear(); listfilterscreen.Items.Clear();
            //        lblFromcreditamount.Text = ""; lblFromdebitamount.Text = ""; fdebitamt = 0; fcreditamt = 0;
            //        lblTocreditamount.Text = ""; lblTodebitamount.Text = ""; tdebitamt = 0; tcreditamt = 0;
            //        i = 1;
            //        foreach (DataRow row in dt.Rows)
            //        {
                       
            //            lblFromcreditamount.Refresh(); lblFromdebitamount.Refresh();                     
            //            Class.Users.Query = ""; unmatched = "";              
            //            if (row.ItemArray[5].ToString() == "")
            //            {
            //                unmatched = "No Data Found ,";
            //            }
            //            if (Convert.ToDateTime(row.ItemArray[1].ToString()).ToString("yyyy-MM-dd").Substring(0,10) != "")
            //            {
            //                Class.Users.Query = "";
            //                Class.Users.Query = "select distinct A.date1 from to_" + dataGridView2.Rows[0].Cells[2].EditedFormattedValue.ToString() + " a where A.date1='" + Convert.ToDateTime(dt.Rows[j]["date1"].ToString()).ToString("yyyy-MM-dd") + "';";
            //                DataSet ds2 = Utility.ExecuteSelectQuery(Class.Users.Query, "to_" + dataGridView2.Rows[0].Cells[2].EditedFormattedValue.ToString());
            //                DataTable dt2 = ds2.Tables["to_" + dataGridView2.Rows[0].Cells[2].EditedFormattedValue.ToString()];
            //                if (dt2.Rows.Count > 0) { }
            //                else
            //                {
            //                    unmatched += "date,";
            //                }
            //            }
            //            if (row.ItemArray[5].ToString() != "")
            //            {
            //              Class.Users.Query = "";
            //                Class.Users.Query = "select distinct A.refno from to_" + dataGridView2.Rows[0].Cells[2].EditedFormattedValue.ToString() + " a where  A.refno='" + dt.Rows[j]["vchno"].ToString() + "';";
            //                DataSet ds2 = Utility.ExecuteSelectQuery(Class.Users.Query, "to_" + dataGridView2.Rows[0].Cells[2].EditedFormattedValue.ToString());
            //                DataTable dt2 = ds2.Tables["to_" + dataGridView2.Rows[0].Cells[2].EditedFormattedValue.ToString()];
            //                if (dt2.Rows.Count > 0) { }
            //                else
            //                {
            //                    unmatched += "vchno,";
            //                }
            //            }
            //            if (row.ItemArray[7].ToString() != "")
            //            {
            //                Class.Users.Query = "";
            //                Class.Users.Query = "select distinct A.Debit from to_" + dataGridView2.Rows[0].Cells[2].EditedFormattedValue.ToString()+" a where A.refno='" + dt.Rows[j]["vchno"].ToString() + "' and A.debit='" + dt.Rows[j]["debit"].ToString() + "'";
            //                DataSet ds2 = Utility.ExecuteSelectQuery(Class.Users.Query, "to_" + dataGridView2.Rows[0].Cells[2].EditedFormattedValue.ToString());
            //                DataTable dt2 = ds2.Tables["to_" + dataGridView2.Rows[0].Cells[2].EditedFormattedValue.ToString()];
            //                if (dt2.Rows.Count > 0) { }
            //                else
            //                {
            //                    unmatched += "debit,";
            //                }
            //            }
            //            if (row.ItemArray[8].ToString() != "")
            //            {
            //                Class.Users.Query = "";
            //                Class.Users.Query = "select distinct A.credit from to_" + dataGridView2.Rows[0].Cells[2].EditedFormattedValue.ToString() + " a where A.refno='" + dt.Rows[j]["vchno"].ToString() + "' and A.debit='" + dt.Rows[j]["debit"].ToString() + "' and A.credit='" + dt.Rows[j]["credit"].ToString() + "'";
            //                DataSet ds2 = Utility.ExecuteSelectQuery(Class.Users.Query, "to_" + dataGridView2.Rows[0].Cells[2].EditedFormattedValue.ToString());
            //                DataTable dt2 = ds2.Tables["to_" + dataGridView2.Rows[0].Cells[2].EditedFormattedValue.ToString()];
            //                if (dt2.Rows.Count > 0) { }
            //                else
            //                {
            //                    unmatched += "credit";
            //                }
            //            }
            //            dt.Rows[j]["unmatched"] = unmatched;
                       
            //            unmatched = "";
                       
            //            fdebitamt += Convert.ToDecimal("0" + row["Debit"].ToString());
            //            fcreditamt += Convert.ToDecimal("0" + row["Credit"].ToString());                                     
                       
            //            i++; j++;
            //        }
            //        if (dt.Rows.Count>0)
            //        {
            //            progressBar1.Minimum = 0;
            //            progressBar1.Maximum = dt.Rows.Count;
            //            i = 0;
            //            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Excel Workbook|*.xlsx", ValidateNames = true })
            //                if (sfd.ShowDialog() == DialogResult.OK)
            //                {
            //                    Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
            //                    Microsoft.Office.Interop.Excel.Workbook wb = app.Workbooks.Add(Microsoft.Office.Interop.Excel.XlSheetType.xlWorksheet);
            //                    Microsoft.Office.Interop.Excel.Worksheet ws = (Microsoft.Office.Interop.Excel.Worksheet)app.ActiveSheet;
            //                    app.Visible = false;
            //                    for (k = 1; k < hed.Length; k++)
            //                    {
            //                        ws.Cells[1, k] = hed[k].Replace("x.","").ToUpper();
            //                    }                               
            //                    i = 2;  j = 0;
            //                    ws.Range["A1", "J1"].Interior.Color = Microsoft.Office.Interop.Excel.XlRgbColor.rgbNavy;
            //                    ws.Range["A1", "J1"].Font.Color = Microsoft.Office.Interop.Excel.XlRgbColor.rgbWhite;

            //                    k = 1; int l = 1;
            //                    foreach (DataRow item in dt.Rows)
            //                    {
            //                        for (k = 1; k < item.ItemArray.Length; k++)
            //                        {
            //                            ws.Cells[i, l] = item[l].ToString();
            //                            l++;
            //                            //ws.Cells[i, 1] = item.ItemArray[1].ToString().Substring(0, 10);
            //                        }
            //                        l = 1;
                                    
            //                        ws.Range["A" + i, "J" + i].Font.Color = Microsoft.Office.Interop.Excel.XlRgbColor.rgbRed;
                                   
                                  
            //                        decimal per = Convert.ToDecimal(100 / GenFun.ToInt(dt.Rows.Count)) * (j + 1);
            //                        lblprogress1.Text = "" + (per).ToString("N0") + " %";
            //                        lblprogress1.Refresh();

            //                        progressBar1.Value = j;
            //                        i++; j++;

            //                    }
            //                    ws.Range["A" + i, "J" + i].Interior.Color = Microsoft.Office.Interop.Excel.XlRgbColor.rgbNavy;
            //                    ws.Columns.AutoFit();
            //                    wb.SaveAs(sfd.FileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault, Type.Missing, true, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, (Microsoft.Office.Interop.Excel.XlSaveAsAccessMode)Microsoft.Office.Interop.Excel.XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing);
            //                    app.Quit();
            //                    MessageBox.Show("Completed"); progressBar1.Maximum = 0;
            //                }
            //        }
            //    }
            //    else
            //    {
            //        MessageBox.Show("No Data Found in MyCompany");
            //    }

            //}

            //catch (Exception ex) { ex.ToString(); }
        }

        string unmatched = "";
        private void unMatchedToolStripMenuItem_Click(object sender, EventArgs e)
        {            

            //try
            //{
              
            //    Class.Users.UserTime = 0;

                
            //    int i = 0; int gridcount = 1;



            //    string[] split = textBox1.Text.Split(';');
            //    Class.Users.Query = "";
            //    Class.Users.Query = "select " + toquery + ",x.sts from(" + split[1] + ") x where x.sts='No'";
            //    DataSet ds = Utility.ExecuteSelectQuery(Class.Users.Query, "to_" + dataGridView2.Rows[0].Cells[2].EditedFormattedValue.ToString());
            //    DataTable dt = ds.Tables["to_" + dataGridView2.Rows[0].Cells[2].EditedFormattedValue.ToString()];
            //    i = 1; int j = 0,k=0;
            //    string[] hed;
            //    hed = null; hed = toquery.Split(',');
               
            //     if (dt != null && dataGridView2.Rows[0].Cells[2].EditedFormattedValue.ToString() != "")
            //    {
            //        listfilterred.Items.Clear(); listfilterscreen.Items.Clear();
            //        lblFromcreditamount.Text = ""; lblFromdebitamount.Text = ""; fdebitamt = 0; fcreditamt = 0;
            //        lblTocreditamount.Text = ""; lblTodebitamount.Text = ""; tdebitamt = 0; tcreditamt = 0;
            //        i = 1;
            //        foreach (DataRow row in dt.Rows)
            //        {

            //            lblFromcreditamount.Refresh(); lblFromdebitamount.Refresh();
            //            Class.Users.Query = ""; unmatched = "";
            //            if (dt.Rows[j].ItemArray[2].ToString()  == "" || dt.Rows[j].ItemArray[3].ToString() == "")
            //            {
            //                unmatched = "No Data Found ,";

            //            }
            //            if (dt.Rows[j].ItemArray[1].ToString().Substring(0, 10) != "")
            //            {
            //                Class.Users.Query = "";
            //                Class.Users.Query = "select distinct A.date1 from my_" + dataGridView2.Rows[0].Cells[2].EditedFormattedValue.ToString() + " a where A.date1='" + Convert.ToDateTime(dt.Rows[j]["date1"].ToString()).ToString("yyyy-MM-dd") + "';";
            //                DataSet ds2 = Utility.ExecuteSelectQuery(Class.Users.Query, "my_" + dataGridView2.Rows[0].Cells[2].EditedFormattedValue.ToString());
            //                DataTable dt2 = ds2.Tables["my_" + dataGridView2.Rows[0].Cells[2].EditedFormattedValue.ToString()];
            //                if (dt2.Rows.Count > 0) { }
            //                else
            //                {
            //                    unmatched += "date,";
            //                }
            //            }
            //            if (dt.Rows[j].ItemArray[3].ToString().ToString() != "")
            //            {
            //                Class.Users.Query = "";
            //                Class.Users.Query = "select distinct A.vchno from my_" + dataGridView2.Rows[0].Cells[2].EditedFormattedValue.ToString() + " a where  A.vchno='" + dt.Rows[j]["refno"].ToString() + "';";
            //                DataSet ds2 = Utility.ExecuteSelectQuery(Class.Users.Query, "my_" + dataGridView2.Rows[0].Cells[2].EditedFormattedValue.ToString());
            //                DataTable dt2 = ds2.Tables["my_" + dataGridView2.Rows[0].Cells[2].EditedFormattedValue.ToString()];
            //                if (dt2.Rows.Count > 0) { }
            //                else
            //                {
            //                    unmatched += "refno,";
            //                }
            //            }
            //            if (dt.Rows[j]["debit"].ToString() != "")
            //            {
            //                Class.Users.Query = "";
            //                Class.Users.Query = "select distinct A.Debit from my_" + dataGridView2.Rows[0].Cells[2].EditedFormattedValue.ToString() + " a where A.vchno='" + dt.Rows[j]["refno"].ToString() + "' and A.debit='" + dt.Rows[j]["debit"].ToString() + "'";
            //                DataSet ds2 = Utility.ExecuteSelectQuery(Class.Users.Query, "my_" + dataGridView2.Rows[0].Cells[2].EditedFormattedValue.ToString());
            //                DataTable dt2 = ds2.Tables["my_" + dataGridView2.Rows[0].Cells[2].EditedFormattedValue.ToString()];
            //                if (dt2.Rows.Count > 0) { }
            //                else
            //                {
            //                    unmatched += "debit,";
            //                }
            //            }
            //            if (dt.Rows[j]["credit"].ToString() != "")
            //            {
            //                Class.Users.Query = "";
            //                Class.Users.Query = "select distinct A.credit from my_" + dataGridView2.Rows[0].Cells[2].EditedFormattedValue.ToString() + " a where A.vchno='" + dt.Rows[j]["refno"].ToString() + "' and A.debit='" + dt.Rows[j]["debit"].ToString() + "' and A.credit='" + dt.Rows[j]["credit"].ToString() + "'";
            //                DataSet ds2 = Utility.ExecuteSelectQuery(Class.Users.Query, "my_" + dataGridView2.Rows[0].Cells[2].EditedFormattedValue.ToString());
            //                DataTable dt2 = ds2.Tables["my_" + dataGridView2.Rows[0].Cells[2].EditedFormattedValue.ToString()];
            //                if (dt2.Rows.Count > 0) { }
            //                else
            //                {
            //                    unmatched += "credit";
            //                }
            //            }
            //            dt.Rows[j]["unmatched"] = unmatched;

            //            unmatched = "";

            //            fdebitamt += Convert.ToDecimal("0" + row["Debit"].ToString());
            //            fcreditamt += Convert.ToDecimal("0" + row["Credit"].ToString());


            //            i++; j++;
            //        }
            //        if (dt.Rows.Count > 0)
            //        {
            //            progressBar1.Minimum = 0;
            //            progressBar1.Maximum = dt.Rows.Count;
            //            i = 0;
            //            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Excel Workbook|*.xlsx", ValidateNames = true })
            //                if (sfd.ShowDialog() == DialogResult.OK)
            //                {
            //                    Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
            //                    Microsoft.Office.Interop.Excel.Workbook wb = app.Workbooks.Add(Microsoft.Office.Interop.Excel.XlSheetType.xlWorksheet);
            //                    Microsoft.Office.Interop.Excel.Worksheet ws = (Microsoft.Office.Interop.Excel.Worksheet)app.ActiveSheet;
            //                    app.Visible = false;
            //                    for (k = 1; k < hed.Length; k++)
            //                    {
            //                        ws.Cells[1, k] = hed[k].Replace("x.", "").ToUpper() ;
            //                        //ws.Cells[1, i] = "Date";
            //                    }
            //                    i = 2; j = 0;
            //                    ws.Range["A1", "J1"].Interior.Color = Microsoft.Office.Interop.Excel.XlRgbColor.rgbNavy;
            //                    ws.Range["A1", "J1"].Font.Color = Microsoft.Office.Interop.Excel.XlRgbColor.rgbWhite;
            //                    k = 1;int l = 1;
            //                    foreach (DataRow item in dt.Rows)
            //                    {
            //                        for (k = 1; k < item.ItemArray.Length; k++)
            //                        {                                       
            //                            ws.Cells[i, l] = item[l].ToString();
            //                            l++;
            //                            //ws.Cells[i, 1] = item.ItemArray[1].ToString().Substring(0, 10);
            //                        }
            //                        l =1;
                                   
            //                        ws.Range["A" + i, "J" + i].Font.Color = Microsoft.Office.Interop.Excel.XlRgbColor.rgbRed;

            //                       // k++;
            //                        decimal per = Convert.ToDecimal(100 / GenFun.ToInt(dt.Rows.Count)) * (j + 1);
            //                        lblprogress1.Text = "" + (per).ToString("N0") + " %";
            //                        lblprogress1.Refresh();

            //                        progressBar1.Value = j;
            //                        i++; j++;

            //                    }
            //                    ws.Range["A" + i, "J" + i].Interior.Color = Microsoft.Office.Interop.Excel.XlRgbColor.rgbNavy;
            //                    ws.Columns.AutoFit();
            //                    wb.SaveAs(sfd.FileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault, Type.Missing, true, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, (Microsoft.Office.Interop.Excel.XlSaveAsAccessMode)Microsoft.Office.Interop.Excel.XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing);
            //                    app.Quit();
            //                    MessageBox.Show("Completed"); progressBar1.Maximum = 0;
            //                }
            //        }
            //    }
            //    else
            //    {
            //        MessageBox.Show("No Data Found in MyCompany");
            //    }

            //}

            //catch (Exception ex) { ex.ToString(); }
        }

        private void all2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (dataGridView2.Rows[0].Cells[2].EditedFormattedValue.ToString() != "")
            //{
              
               
            //    string[] split = textBox1.Text.Split(';');
            //    string sel = "select " + fromquery + ",x.sts from(" + split[0] + ") x";
               
            //}
            //else { MessageBox.Show("No data Found", "Null"); }
        }
        private void pDFToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkFuel_CheckedChanged(object sender, EventArgs e)
        {

            tabControl1.SelectTab(tabPage1);
            if (checkFuel.Checked == true)
            {
                string sel = "SELECT X.DATEID,X.DATE1,X.COMPCODE,X.TOKENNO,X.VEHICLENO,X.VEHTYPE,X.EMPNAME,X.DEPT,X.ITEMNAME,X.LITRES,X.FUELRATE2,X.BUNKNAME,X.TOTAL,X.STS FROM(SELECT DISTINCT  A.DATEID,A.DATE1,A.COMPCODE,A.TOKENNO,A.VEHICLENO,A.VEHTYPE,A.EMPNAME,A.DEPT,A.ITEMNAME,A.LITRES,A.FUELRATE2,A.BUNKNAME,A.TOTAL, CASE WHEN A.TOKENNO = B.INDNO THEN 'No' ELSE 'Yes' END STS FROM MY_AGF A  LEFT JOIN TO_AGF B  ON  A.DATEID = B.DATEID AND  A.DATE1 = B.DATE1 AND  A.VEHICLENO = B.VEHICLE AND  A.TOKENNO = B.INDNO AND  A.FUELRATE2 = B.RATE ) X  ORDER BY  4";
                listviewxl3(sel, tableName, tableName1, listView1);
                string sel0 = "select X.DATEID,X.DATE1,X.PARTICULARS,X.VCHTYPE,X.VCHNO,X.INDNO,X.VEHICLE,X.FUELQTY,X.RATE,X.LUB,X.DEBIT,X.CREDIT,x.sts from( SELECT DISTINCT  A.DATEID,A.DATE1,A.PARTICULARS,A.VCHTYPE,A.VCHNO,A.INDNO,A.VEHICLE,A.FUELQTY,A.RATE,A.LUB,A.DEBIT,A.CREDIT, CASE WHEN B.TOKENNO = A.INDNO then 'No' else 'Yes' end STS from TO_AGF A  LEFT JOIN MY_AGF B  ON A.DATEID = B.DATEID AND B.DATE1 = A.DATE1 AND  A.VEHICLE = B.VEHICLENO  AND  B.TOKENNO = A.INDNO AND  B.FUELRATE2 = A.RATE  ) x  order by  6";
                listviewxl4(sel0, tableName, tableName1, listView2);
            }
            else
            {
                listView1.Items.Clear(); listView2.Items.Clear();
            }
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }


        private void butmycompany_Click(object sender, EventArgs e)
        {
            Class.Users.Intimation = "PAYROLL";

            if (dataGridView1.Rows.Count <= 0)
            {

                string sel2 = "SELECT TO_CHAR(X.TOKENDATE,'DD-MM-YY') AS DATE1, X.COMPCODE,X.TOKENNO,X.VEHICLENO, X.VEHTYPE ,X.FNAME as EMPNAME, X.DEPT,  X.ITEMNAME,TO_NUMBER(X.LITRES) AS LITRES,ROUND(X.FUELRATE2,2) AS FUELRATE2 ,X.BUNKNAME,  ROUND(SUM(X.LITRES*X.FUELRATE2),2) AS TOTAL FROM  (   SELECT A.ASPTBLVEHTOKENID, D.COMPCODE, substr(A.TOKENNO,11,10) as tokenno, A.TOKENDATE,  (   SELECT  AA.VEHICLENO   FROM HRVEHMAST AA  JOIN HRVEHTYPEMAST AB on AA.VEHICLETYPE=AB.HRVEHTYPEMASTID     JOIN GTCOMPMAST AC ON AC.GTCOMPMASTID=AA.COMPCODE    WHERE  AA.HRVEHMASTID=A.VEHICLENO          UNION ALL            SELECT  BA.VEHICLENO FROM ASPTBLVEHMAS BA      JOIN GTCOMPMAST BC ON BC.GTCOMPMASTID=BA.COMPCODE   WHERE  BA.ASPTBLVEHMASID=A.VEHICLENO     ) AS VEHICLENO , ( SELECT  AB.VEHTYPE   FROM HRVEHMAST AA  JOIN HRVEHTYPEMAST Ab on AA.VEHICLETYPE=AB.HRVEHTYPEMASTID          JOIN GTCOMPMAST AC ON AC.GTCOMPMASTID=AA.COMPCODE   WHERE  AA.HRVEHMASTID=A.VEHICLENO      UNION ALL       SELECT  BA.VCATEGORY as VEHTYPE FROM ASPTBLVEHMAS BA   JOIN GTCOMPMAST BC ON BC.GTCOMPMASTID=BA.COMPCODE  WHERE  BA.ASPTBLVEHMASID=A.VEHICLENO     ) AS VEHTYPE ,CONCAT(E.fname, concat('-', F.MIDCARD))  AS FNAME, J.MNNAME1 AS  DEPT,  H.ITEMNAME, CASE B.LITRES  WHEN  'FULL' THEN '0'    ELSE B.LITRES END as LITRES,    (SELECT max(AA.FUELRATE2) AS FUELRATE2       FROM ASPTBLFUELRATEMASDET AA JOIN GTCOMPMAST AB  ON AA.COMPCODE = AB.GTCOMPMASTID JOIN GTGENITEMMAST AC ON AC.GTGENITEMMASTID = AA.ITEMNAME  WHERE AB.COMPCODE = D.COMPCODE AND AC.GTGENITEMMASTID = AA.ITEMNAME AND AA.BUNKNAME=A.BUNKNAME  AND AC.ITEMNAME = H.ITEMNAME  AND AA.ASPTBLFUELRATEMASDETID = ( SELECT MAX(ZAA.ASPTBLFUELRATEMASDETID) FROM ASPTBLFUELRATEMASDET ZAA JOIN GTCOMPMAST ZAB  ON ZAA.COMPCODE = ZAB.GTCOMPMASTID JOIN GTGENITEMMAST ZAC ON ZAC.GTGENITEMMASTID = ZAA.ITEMNAME  WHERE ZAB.COMPCODE = AB.COMPCODE AND ZAA.BUNKNAME=A.BUNKNAME  AND ZAC.ITEMNAME = AC.ITEMNAME  AND ZAA.COMPCODE=D.GTCOMPMASTID AND ZAA.FUELDATE <= A.TOKENDATE  )  )  AS FUELRATE2,I.BUNKNAME,  A.REMARKS  FROM  ASPTBLVEHTOKEN A     JOIN ASPTBLVEHTOKENDET B ON A.ASPTBLVEHTOKENID=B.ASPTBLVEHTOKENID   AND A.TOKENCANCEL='F'   JOIN GTCOMPMAST D ON D.GTCOMPMASTID=A.COMPCODE  JOIN HREMPLOYMAST E ON E.HREMPLOYMASTID=A.EMPNAME       JOIN hremploydetails F ON F.HREMPLOYMASTID=E.HREMPLOYMASTID  join HRVEHTYPEMAST G on G.HRVEHTYPEMASTid=A.VEHICLETYPE JOIN GTGENITEMMAST H ON H.GTGENITEMMASTID=B.ITEMNAME  JOIN ASPTBLPETMAS I ON I.COMPCODE=D.GTCOMPMASTID AND I.COMPCODE=A.COMPCODE   AND  I.ASPTBLPETMASID=A.BUNKNAME join GTDEPTDESGMAST j on   J.GTDEPTDESGMASTID=F.DEPTNAME ) X WHERE X.COMPCODE='" + Class.Users.HCompcode + "' AND   X.TOKENDATE between TO_DATE('" + frmdate.Value.ToString("dd-MM-yyyy") + "','dd-MM-yyyy') AND TO_DATE('" + todate.Value.ToString("dd-MM-yyyy") + "','dd-MM-yyyy')  AND X.BUNKNAME='" + combobunk.Text + "' GROUP BY X.TOKENDATE, X.COMPCODE,X.TOKENNO,X.VEHICLENO, X.VEHTYPE ,X.FNAME, X.DEPT,  X.ITEMNAME,X.LITRES, X.FUELRATE2,X.BUNKNAME,X.REMARKS ORDER BY 1";
                DataSet ds2 = Utility.ExecuteSelectQuery(sel2, "ASPTBLVEHTOKEN");
                DataTable dt2 = ds2.Tables["ASPTBLVEHTOKEN"];

                dataGridView1.DataSource = dt2;


                Models.Validate val = new Models.Validate();
              
                if (dataGridView1.Rows.Count > 0 && Class.Users.HCompcode != "")
                {
                    if (dataGridView1.Columns.Count >= 7)
                    {
                        listView2.Items.Clear();
                        string frmtable = "", frmrow = "", frmdata = ""; int i = 0;

                        if (dataGridView1.Rows.Count == 0)
                        {
                            MessageBox.Show("No Data Found");
                            return;
                        }

                        if (dataGridView1.Columns.Count < 7)
                        {
                            MessageBox.Show("Minimum Excel Column should be 7");
                            return;
                        }

                        if (string.IsNullOrEmpty(Class.Users.HCompcode))
                        {
                            MessageBox.Show("Company Code Missing");
                            return;
                        }

                        string tableName = "MY_" + Class.Users.HCompcode;
                        string schema = Class.Users.ProjectID;

                        listView2.Items.Clear();

                        string columnDef = "";
                        string columnList = "";

                        foreach (DataGridViewColumn col in dataGridView1.Columns)
                        {
                            string colName = CleanColumn(col.HeaderText);

                            if (i == 0)
                            {
                                

                                columnDef += "dateid INTEGER PRIMARY KEY, date1 DATE";
                                columnList += colName;
                            }
                            else
                            {
                                columnDef += "," + colName + " VARCHAR2(100)";
                                columnList += "," + colName;
                            }

                            i++;
                        }

                        string checkTable = $"SELECT TRIGGER_NAME, TABLE_NAME, STATUS FROM USER_TRIGGERS a where A.TRIGGER_NAME='{tableName}TRI'";
                        DataSet ds = Utility.ExecuteSelectQuery(checkTable, tableName);

                        if (ds.Tables[tableName].Rows.Count > 0)
                        {
                            Utility.ExecuteNonQuery($"DROP TRIGGER {schema}.{tableName}TRI");
                            Utility.ExecuteNonQuery($"DROP SEQUENCE {schema}.{tableName}SEQ");
                            Utility.ExecuteNonQuery($"DROP TABLE {schema}.{tableName}");
                        }

                        Utility.ExecuteNonQuery($"CREATE TABLE {schema}.{tableName} ({columnDef})");
                        Utility.ExecuteNonQuery($@"CREATE SEQUENCE {schema}.{tableName}SEQ START WITH 1 INCREMENT BY 1 NOMAXVALUE");
                        Utility.ExecuteNonQuery($@"CREATE OR REPLACE TRIGGER {schema}.{tableName}TRI BEFORE INSERT ON {schema}.{tableName} FOR EACH ROW BEGIN SELECT {tableName}SEQ.NEXTVAL INTO :NEW.DATEID FROM DUAL; END;");

                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (row.Cells[0].Value == null) continue;

                            string values = "";

                            for (int j = 0; j < row.Cells.Count; j++)
                            {
                                if (j == 0)
                                {
                                    DateTime dt = Convert.ToDateTime(row.Cells[j].Value);
                                    values += $"TO_DATE('{dt:dd-MM-yyyy}','dd-MM-yyyy'),";
                                }
                                else if (j == row.Cells.Count - 1)
                                {
                                    values += $"'{row.Cells[j].Value.ToString().Replace("'", "''")}'";
                                }
                                else
                                {
                                    values += $"'{row.Cells[j].Value.ToString().Replace("'", "''")}',";
                                }
                            }

                            string insert = $"INSERT INTO {tableName} ({columnList}) VALUES({values})";

                            Utility.ExecuteNonQuery(insert);
                        }

                    }
                    else
                    {
                        MessageBox.Show("Minimum Excel Column should be 7. ", "7 Columns In Excel ");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("No Data Found");
                    return;
                }
            }
            else
            {
               
                dataGridView1.DataSource = null;
                dataGridView1.Columns.Clear();
            }
        }


        private void buttosupplier_Click(object sender, EventArgs e)
        {

            try
            {
                OpenFileDialog file = new OpenFileDialog();
                file.Filter = "Excel Files|*.xls;*.xlsx";
                DataTable dt0=new DataTable();
                if (file.ShowDialog() == DialogResult.OK)
                {
                   string filePath = file.FileName; //get the path of the file  
                                           
                   
                    if (!string.IsNullOrEmpty(filePath))
                    {
                        string ext = Path.GetExtension(filePath).ToLower();
                        if (ext == ".xls")

                            dt0 = Class.Master.ReadExcel(filePath, ".xls");
                        else
                            dt0 = Class.Master.ImportExcelToDataTable(filePath);

                        dataGridView3.DataSource = dt0;
                    }
                    else
                    {
                        mas.pop("Excel file doesn't contain", Class.Users.Paramid.ToString(), "");
                    }
                    

                    lbldowncount.Text = "Total Excel Rows : " + dt0.Rows.Count;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                GridLoad();
            
                tabControl1.SelectTab(tabPage2);
            }


            Models.Validate val = new Models.Validate();
          
            if (dataGridView3.Rows.Count > 0 && Class.Users.HCompcode != "")
            {
                if (dataGridView3.Columns.Count >= 7)
                {
                    listView2.Items.Clear();
                    //string frmtable = "", frmrow = "", frmdata = ""; 
                    int i = 0;

                    if (dataGridView3.Rows.Count == 0)
                    {
                        MessageBox.Show("No Data Found");
                        return;
                    }

                    if (dataGridView3.Columns.Count < 7)
                    {
                        MessageBox.Show("Minimum Excel Column should be 7");
                        return;
                    }

                    if (string.IsNullOrEmpty(Class.Users.HCompcode))
                    {
                        MessageBox.Show("Company Code Missing");
                        return;
                    }

                
                    listView2.Items.Clear();

                    string columnDef = "";
                    string columnList = "";

                    foreach (DataGridViewColumn col in dataGridView3.Columns)
                    {
                        string colName = CleanColumn(col.HeaderText);

                        if (i == 0)
                        {
                            //if (colName != "date")
                            //{
                            //    MessageBox.Show("First Column Should be Date Column in Excel");
                            //    return;
                            //}

                            columnDef += "dateid INTEGER PRIMARY KEY, date1 DATE";
                            columnList += colName+"1";
                        }
                        else
                        {
                            columnDef += "," + colName + " VARCHAR2(100)";
                            columnList += "," + colName;
                        }

                        i++;
                    }

                    string checkTable = $"SELECT TRIGGER_NAME, TABLE_NAME, STATUS FROM USER_TRIGGERS a where A.TRIGGER_NAME='{tableName1}TRI'";
                    DataSet ds = Utility.ExecuteSelectQuery(checkTable, tableName1);

                    if (ds.Tables[tableName1].Rows.Count > 0)
                    {
                        Utility.ExecuteNonQuery($"DROP TRIGGER {schema}.{tableName1}TRI");
                        Utility.ExecuteNonQuery($"DROP SEQUENCE {schema}.{tableName1}SEQ");
                        Utility.ExecuteNonQuery($"DROP TABLE {schema}.{tableName1}");
                    }

                    Utility.ExecuteNonQuery($"CREATE TABLE {schema}.{tableName1} ({columnDef})");
                    Utility.ExecuteNonQuery($@"CREATE SEQUENCE {schema}.{tableName1}SEQ START WITH 1 INCREMENT BY 1 NOMAXVALUE");
                    Utility.ExecuteNonQuery($@"CREATE OR REPLACE TRIGGER {schema}.{tableName1}TRI BEFORE INSERT ON {schema}.{tableName1} FOR EACH ROW BEGIN SELECT {tableName1}SEQ.NEXTVAL INTO :NEW.DATEID FROM DUAL; END;");

                    foreach (DataGridViewRow row in dataGridView3.Rows)
                    {
                        if (row.Cells[0].Value == null) continue;

                        string values = "";

                        for (int j = 0; j < row.Cells.Count; j++)
                        {
                            if (j == 0)
                            {
                                DateTime dt = Convert.ToDateTime(row.Cells[j].Value);
                                values += $"TO_DATE('{dt:dd-MM-yyyy}','dd-MM-yyyy'),";
                            }
                            else if (j == row.Cells.Count - 1)
                            {
                                values += $"'{row.Cells[j].Value.ToString().Replace("'", "''")}'";
                            }
                            else
                            {
                                values += $"'{row.Cells[j].Value.ToString().Replace("'", "''")}',";
                            }
                        }

                        string insert = $"INSERT INTO {tableName1} ({columnList}) VALUES({values})";

                        Utility.ExecuteNonQuery(insert);
                    }

                }
                else
                {
                    MessageBox.Show("Minimum Excel Column should be 7. ", "7 Columns In Excel ");
                    return;
                }
            }
            else
            {
                MessageBox.Show("No Data Found");
                return;
            }


        }
        private void button2_Click_2(object sender, EventArgs e)
        {

        }
    }
}