using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Collections;
using System.Windows.Forms;
using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace Pinnacle
{

    public class Utility
    {
        public static OracleConnection con;
        private static OracleCommand cmd;
        public static OracleTransaction Trans;
        public static OracleDataReader Rdr;
        public static OracleDataAdapter da;
        public static string constring = "";
        public static string old = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        static Utility()
        {
            try
            {

                string[] data1; string[] data;
                try
                {
                    if (Class.Users.Intimation == "PAYROLL") {
                         data1 = Class.Users.ConString.Split(',');
                        if (con != null)
                        {
                            if (con.State == ConnectionState.Closed)
                            {
                                con = new OracleConnection(data1[0]);
                                con.Open();
                                constring = data1[0].ToString();
                            }
                        }
                        else
                        {
                            data = Class.Users.ConString.Split(',');
                            con = new OracleConnection(data[0]);
                            con.Open();


                        }
                    }
                    else
                    {
                        Class.Users.Intimation = "";
                        Class.Users.ConString1 = old;
                        data = Class.Users.ConString1.Split(',');
                        if (con != null)
                        {
                            if (con.State == ConnectionState.Closed)
                            {
                                con = new OracleConnection(data[0]);
                                con.Open();
                                constring = data[0].ToString();
                            }
                        }
                        else
                        {
                            con = new OracleConnection(data[0]);
                            con.Open();


                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public static OracleConnection Connect()
        {
            try
            {
                string[] data1; string[] data;
                if (Class.Users.Intimation == "PAYROLL")
                {
                    data1 = Class.Users.ConString.Split(',');
                    if (con != null)
                    {
                        if (con.State == ConnectionState.Closed)
                        {
                            con = new OracleConnection(data1[0]);
                            con.Open();
                            constring = data1[0].ToString();
                        }
                    }
                    else
                    {
                        data1 = Class.Users.ConString.Split(',');
                        con = new OracleConnection(data1[0]);
                        con.Open();


                    }
                }
                else
                {
                    Class.Users.ConString1 = old;
                    data = Class.Users.ConString1.Split(',');
                    if (con != null)
                    {
                        if (con.State == ConnectionState.Closed)
                        {
                            con = new OracleConnection(data[0]);
                            con.Open();
                            constring = data[0].ToString();
                        }
                    }
                    else
                    {
                        con = new OracleConnection(data[0]);
                        con.Open();


                    }
                }
             
                //if (Class.Users.Intimation != "PAYROLL")
                //{
                //    Class.Users.ConString1 = old;
                //    string[] data1= Class.Users.ConString1.Split(',');
                //    if (con != null)
                //    {
                //        if (con.State == ConnectionState.Closed)
                //        {
                //            con = new OracleConnection(data1[0]);
                //            con.Open();
                //            constring = data1[0].ToString();
                //        }
                //    }


                //}
                //else
                //{
                //    string[] data = Class.Users.ConString.Split(',');
                //    if (con != null)
                //    {
                //        if (con.State == ConnectionState.Closed)
                //        {
                //            con = new OracleConnection(data[0]);
                //            con.Open();
                //            constring = data[0].ToString();
                //        }
                //    }
                //    //if (con.State == ConnectionState.Closed)
                //    //{
                //    //    con.Open();
                //    //}
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return con;
        }

        public static OracleConnection DisConnect()
        {
            try
            {
                
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();

                    }
              

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return con;
        }
     
       
        public static DataSet ExecuteSelectQuery(string query, string tblname)
        {
             DataSet ds = new DataSet();

            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    Connect();
                }
                    da = new OracleDataAdapter(query, con);
                    da.Fill(ds, tblname);
            }
            catch { }
            DisConnect();
            return ds;
        }
        public static DataSet ExecuteSelectQuery(string query, string tblname, Dictionary<string, object> parameters)
        {
            DataSet ds = new DataSet();

            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    Connect();
                }

                using (OracleCommand cmd = new OracleCommand(query, con))
                {
                    // Add parameters safely
                    if (parameters != null)
                    {
                        foreach (var param in parameters)
                        {
                            cmd.Parameters.Add(new OracleParameter(param.Key, param.Value ?? DBNull.Value));
                        }
                    }

                    using (OracleDataAdapter da = new OracleDataAdapter(cmd))
                    {
                        da.Fill(ds, tblname);
                    }
                }
            }
            catch (Exception ex)
            {
                // Show or log the exception for debugging
                MessageBox.Show("Database error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                DisConnect();
            }

            return ds;
        }
        public static int ExecuteNonQuery(string sql, System.Data.OracleClient.OracleParameter[] parameters)
        {
            int result = 0;

            using (OracleConnection con = new OracleConnection(constring))
            {
                using (OracleCommand cmd = new OracleCommand(sql, con))
                {
                    cmd.CommandType = CommandType.Text;

                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }

                    con.Open();
                    result = cmd.ExecuteNonQuery();
                }
            }

            return result;
        }

        public static bool ExecuteNonQuery(string query)
        {
            if (con.State == ConnectionState.Closed)
            {
                Connect();
            } 
            cmd = new OracleCommand(query, con);
            cmd.ExecuteNonQuery();
            DisConnect();
            return true;
        }

        //public static bool ExecuteNonQuery1(string query)
        //{
        //    if (con1.State == ConnectionState.Closed)
        //    {
        //        Connect1();
        //    }
        //    cmd = new OracleCommand(query, con1);
        //    cmd.ExecuteNonQuery();
        //    DisConnect1();
        //    return true;
        //}

        public static bool ExecuteNonQuery(string query, Byte[] s)
        {
            if (con.State == ConnectionState.Closed)
            {
                Connect();
            }
            cmd = new OracleCommand(query, con);
            cmd.Parameters.Add(":EMPIMAGE", s);
            cmd.ExecuteNonQuery();
            DisConnect();
            return true;
        }
        public static bool ExecuteNonQuery(string query,string par, Byte[] s)
        {
            if (con.State == ConnectionState.Closed)
            {
                Connect();
            }
            cmd = new OracleCommand(query, con);
            cmd.Parameters.Add(par, s);
            cmd.ExecuteNonQuery();
            DisConnect();
            return true;
        }
        public static OracleDataReader ExecuteReader(string query)
        {
            if (con.State == ConnectionState.Closed)
            {
                Connect();
            }
            cmd = new OracleCommand(query, con);
            OracleDataReader dr = cmd.ExecuteReader();
            DisConnect();
            return dr;
        }
        public static object ExecuteScalar(string sql)
        {
            if (con.State == ConnectionState.Closed)
            {
                Connect();
            }
            cmd = new OracleCommand(sql, con);
            object scalarValue = cmd.ExecuteScalar();
            DisConnect();
            return scalarValue;

        }

        public static DataTable SQLQuery(string Sql, Hashtable ParamTable = null)
        {
            OracleCommand CmdData = new OracleCommand();
            // Warning!!! Optional parameters not supported
            OracleDataAdapter Sda = new OracleDataAdapter();
            DataSet Ds = new DataSet();
            if (con.State == ConnectionState.Closed)
            {
                Connect();
            }
            CmdData.CommandText = Sql;
            CmdData.Connection = con;
            CmdData.Transaction = Trans;
            CmdData.CommandTimeout = 180;
            if (ParamTable != null)
            {
                foreach (DictionaryEntry DeData in ParamTable)
                {
                    CmdData.Parameters.Add(DeData.Key.ToString(), DeData.Value);
                }

            }

            Sda = new OracleDataAdapter(CmdData);
            Ds = new DataSet();
            Sda.Fill(Ds, "tabresult");
            DataTable SQLQuery = new DataTable();
            SQLQuery.Clear();
            SQLQuery = Ds.Tables["tabresult"];
            DisConnect();
            return SQLQuery;
        }


        public static void Load_ListCombo(object Sender, string Sql, string ValMem, string DisMem, Hashtable ParamTable = null, string DefValue = "", string DefValueOrder = "TOP", bool QueryUpdate = true)
        {
            // ''''' Load Combo / Listbox
            // Warning!!! Optional parameters not supported
            try
            {
                OracleCommand CmdData = new OracleCommand();
                if (con.State == ConnectionState.Closed)
                {
                    Connect();
                }
                CmdData.CommandText = Sql;
                CmdData.Connection = con;
                if (ParamTable != null)
                {
                    foreach (DictionaryEntry DeData in ParamTable)
                    {
                        CmdData.Parameters.Add(DeData.Key.ToString(), DeData.Value);
                    }

                }

                OracleDataAdapter Sda = new OracleDataAdapter(CmdData);
                DataTable Dt = new DataTable();
                Sda.Fill(Dt);
                if (DefValue != "")
                {
                    DataRow Row;
                    Row = Dt.NewRow();
                    Row[ValMem] = 0;
                    Row[DisMem] = DefValue;
                    if (DefValueOrder.ToUpper() == "BOTTOM")
                    {
                        if (Dt.Rows.Count + 1 > 0)
                        {
                            Dt.Rows.InsertAt(Row, Dt.Rows.Count + 1);
                        }
                        else
                        {
                            Dt.Rows.InsertAt(Row, 0);
                        }
                    }
                    else
                    {
                        Dt.Rows.InsertAt(Row, 0);
                    }
                }
                ((ComboBox)Sender).DisplayMember = DisMem;
                ((ComboBox)Sender).ValueMember = ValMem;
                ((ComboBox)Sender).DataSource = Dt;
                if (((ComboBox)Sender).DropDownStyle == ComboBoxStyle.DropDown)
                {
                    if (DefValue != "")
                    {
                        ((ComboBox)Sender).SelectedIndex = 0;
                    }
                    else
                    {
                        ((ComboBox)Sender).SelectedIndex = -1;
                    }
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                DisConnect();
            }

        }
        public static void Load_DataGrid(object Sender, string Sql, Hashtable ParamTable = null)
        {
            OracleCommand CmdData = new OracleCommand();
            // Warning!!! Optional parameters not supported
            OracleDataAdapter Sda = new OracleDataAdapter();
            DataSet Ds = new DataSet();
            DataView Dv = new DataView();
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    Connect();
                }
                CmdData.CommandText = Sql;
                CmdData.Connection = con;
                CmdData.CommandTimeout = 180;
                if (ParamTable != null)
                {
                    foreach (DictionaryEntry DeData in ParamTable)
                    {
                        CmdData.Parameters.Add(DeData.Key.ToString(), DeData.Value);
                    }

                }

                Sda = new OracleDataAdapter(CmdData);
                Ds = new DataSet();
                Sda.Fill(Ds, "tabresult");
                ((DataGridView)(Sender)).DataSource = Ds.Tables[0];
            }
            catch (Exception ex)
            {
            }
            finally
            {
                DisConnect();
            }

        }


        public static void SQLNonQuery(string Sql, Hashtable ParamTable = null)
        {
            OracleCommand CmdData = new OracleCommand();
            // Warning!!! Optional parameters not supported
            if (con.State == ConnectionState.Closed)
            {
                Connect();
            }
            CmdData.CommandText = Sql;
            CmdData.Connection = con;
            CmdData.Transaction = Trans;
            CmdData.CommandTimeout = 180;
            if (ParamTable != null)
            {
                foreach (DictionaryEntry DeData in ParamTable)
                {
                    CmdData.Parameters.Add(DeData.Key.ToString().Replace('@', ':'), DeData.Value);
                }
                CmdData.BindByName = true;
            }

            CmdData.ExecuteNonQuery();
            DisConnect();
        }
        public static object SQLScalar(string Sql, Hashtable ParamTable = null)
        {
            OracleCommand CmdData = new OracleCommand();
            if (con.State == ConnectionState.Closed)
            {
                Connect();
            }
            CmdData.CommandText = Sql;
            CmdData.Connection = con;
            CmdData.Transaction = Trans;
            if (ParamTable != null)
            {
                foreach (DictionaryEntry DeData in ParamTable)
                {
                    CmdData.Parameters.Add(DeData.Key.ToString(), DeData.Value);
                }

            }
            DisConnect();
            return CmdData.ExecuteScalar();

        }

        public static OracleDataReader SQLReader(string Sql, Hashtable ParamTable = null)
        {
            OracleCommand CmdData = new OracleCommand();
            // Warning!!! Optional parameters not supported

            if (Rdr != null)
            {
                Rdr.Close();
            }

            CmdData.CommandText = Sql;
            CmdData.Connection = con;
            if (ParamTable != null)
            {
                foreach (DictionaryEntry DeData in ParamTable)
                {
                    CmdData.Parameters.Add(DeData.Key.ToString(), DeData.Value);
                }

            }
            Rdr = CmdData.ExecuteReader();
            return Rdr;
        }
    }
}