using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Collections;
using System.Windows.Forms;
using System.Data;
//using MySql.Data.MySqlClient;

namespace Pinnacle
{
    //class Class1
    //{
    //    public static MySqlConnection con;
    //    private static MySqlCommand cmd;
    //    public static MySqlTransaction Trans;
    //    public static MySqlDataReader Rdr;
    //    public static MySqlDataAdapter da;
    //    static Class1()
    //    {
    //        try
    //        {


    //            try
    //            {
    //                string[] data = Class.Users.MySqlDataBase.Split(',');
    //                if (con != null)
    //                {
    //                    if (con.State == ConnectionState.Closed)
    //                    {
    //                        con = new MySqlConnection(data[0]);
    //                        con.Open();
    //                    }
    //                }
    //                else
    //                {
    //                    con = new MySqlConnection(data[0]);
    //                    con.Open();
    //                }
    //            }
    //            catch (Exception ex)
    //            {
    //                MessageBox.Show(ex.Message.ToString());
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //        }

    //    }
    //    public void Connect()
    //    {

    //        try
    //        {
    //            if (con.State == ConnectionState.Closed)
    //            {
    //                con.Open();
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //        }

    //    }
    //    public void DisConnect()
    //    {
    //        try
    //        {
    //            if (con.State == ConnectionState.Open)
    //            {
    //                con.Close();

    //            }

    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //        }
    //    }
    //    public static DataSet ExecuteSelectQuery(string query, string tblname)
    //    {
    //        DataSet ds = new DataSet();

    //        try
    //        {
    //            da = new MySqlDataAdapter(query, con);
    //            da.Fill(ds, tblname);
    //        }
    //        catch { }
    //        return ds;
    //    }
    //    public static bool ExecuteNonQuery(string query)
    //    {

    //        cmd = new MySqlCommand(query, con);
    //        cmd.ExecuteNonQuery();
    //        return true;
    //    }
    //    public static bool ExecuteNonQuery(string query, Byte[] s)
    //    {

    //        cmd = new MySqlCommand(query, con);
    //        cmd.Parameters.AddWithValue(":EMPIMAGE", s);
    //        cmd.ExecuteNonQuery();

    //        return true;
    //    }
    //    public static MySqlDataReader ExecuteReader(string query)
    //    {

    //        cmd = new MySqlCommand(query, con);
    //        MySqlDataReader dr = cmd.ExecuteReader();
    //        return dr;
    //    }
    //    public static object ExecuteScalar(string sql)
    //    {

    //        cmd = new MySqlCommand(sql, con);
    //        object scalarValue = cmd.ExecuteScalar();
    //        return scalarValue;

    //    }

    //    public static DataTable SQLQuery(string Sql, Hashtable ParamTable = null)
    //    {
    //        MySqlCommand CmdData = new MySqlCommand();
    //        // Warning!!! Optional parameters not supported
    //        MySqlDataAdapter Sda = new MySqlDataAdapter();
    //        DataSet Ds = new DataSet();

    //        CmdData.CommandText = Sql;
    //        CmdData.Connection = con;
    //        CmdData.Transaction = Trans;
    //        CmdData.CommandTimeout = 180;
    //        if (ParamTable != null)
    //        {
    //            foreach (DictionaryEntry DeData in ParamTable)
    //            {
    //                CmdData.Parameters.AddWithValue(DeData.Key.ToString(), DeData.Value);
    //            }

    //        }

    //        Sda = new MySqlDataAdapter(CmdData);
    //        Ds = new DataSet();
    //        Sda.Fill(Ds, "tabresult");
    //        DataTable SQLQuery = new DataTable();
    //        SQLQuery.Clear();
    //        SQLQuery = Ds.Tables["tabresult"];
    //        return SQLQuery;
    //    }


    //    public static void Load_ListCombo(object Sender, string Sql, string ValMem, string DisMem, Hashtable ParamTable = null, string DefValue = "", string DefValueOrder = "TOP", bool QueryUpdate = true)
    //    {
    //        // ''''' Load Combo / Listbox
    //        // Warning!!! Optional parameters not supported
    //        try
    //        {
    //            MySqlCommand CmdData = new MySqlCommand();

    //            CmdData.CommandText = Sql;
    //            CmdData.Connection = con;
    //            if (ParamTable != null)
    //            {
    //                foreach (DictionaryEntry DeData in ParamTable)
    //                {
    //                    CmdData.Parameters.AddWithValue(DeData.Key.ToString(), DeData.Value);
    //                }

    //            }

    //            MySqlDataAdapter Sda = new MySqlDataAdapter(CmdData);
    //            DataTable Dt = new DataTable();
    //            Sda.Fill(Dt);
    //            if (DefValue != "")
    //            {
    //                DataRow Row;
    //                Row = Dt.NewRow();
    //                Row[ValMem] = 0;
    //                Row[DisMem] = DefValue;
    //                if (DefValueOrder.ToUpper() == "BOTTOM")
    //                {
    //                    if (Dt.Rows.Count + 1 > 0)
    //                    {
    //                        Dt.Rows.InsertAt(Row, Dt.Rows.Count + 1);
    //                    }
    //                    else
    //                    {
    //                        Dt.Rows.InsertAt(Row, 0);
    //                    }
    //                }
    //                else
    //                {
    //                    Dt.Rows.InsertAt(Row, 0);
    //                }
    //            }
    //            ((ComboBox)Sender).DisplayMember = DisMem;
    //            ((ComboBox)Sender).ValueMember = ValMem;
    //            ((ComboBox)Sender).DataSource = Dt;
    //            if (((ComboBox)Sender).DropDownStyle == ComboBoxStyle.DropDown)
    //            {
    //                if (DefValue != "")
    //                {
    //                    ((ComboBox)Sender).SelectedIndex = 0;
    //                }
    //                else
    //                {
    //                    ((ComboBox)Sender).SelectedIndex = -1;
    //                }
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //        }
    //        finally
    //        {

    //        }

    //    }
    //    public static void Load_DataGrid(object Sender, string Sql, Hashtable ParamTable = null)
    //    {
    //        MySqlCommand CmdData = new MySqlCommand();
    //        // Warning!!! Optional parameters not supported
    //        MySqlDataAdapter Sda = new MySqlDataAdapter();
    //        DataSet Ds = new DataSet();
    //        DataView Dv = new DataView();
    //        try
    //        {

    //            CmdData.CommandText = Sql;
    //            CmdData.Connection = con;
    //            CmdData.CommandTimeout = 180;
    //            if (ParamTable != null)
    //            {
    //                foreach (DictionaryEntry DeData in ParamTable)
    //                {
    //                    CmdData.Parameters.AddWithValue(DeData.Key.ToString(), DeData.Value);
    //                }

    //            }

    //            Sda = new MySqlDataAdapter(CmdData);
    //            Ds = new DataSet();
    //            Sda.Fill(Ds, "tabresult");
    //            ((DataGridView)(Sender)).DataSource = Ds.Tables[0];
    //        }
    //        catch (Exception ex)
    //        {
    //        }
    //        finally
    //        {

    //        }

    //    }


    //    //public static void SQLNonQuery(string Sql, Hashtable ParamTable = null)
    //    //{
    //    //    MySqlCommand CmdData = new MySqlCommand();
    //    //    // Warning!!! Optional parameters not supported

    //    //    CmdData.CommandText = Sql;
    //    //    CmdData.Connection = con;
    //    //    CmdData.Transaction = Trans;
    //    //    CmdData.CommandTimeout = 180;
    //    //    if (ParamTable != null)
    //    //    {
    //    //        foreach (DictionaryEntry DeData in ParamTable)
    //    //        {
    //    //            CmdData.Parameters.AddWithValue(DeData.Key.ToString().Replace('@', ':'), DeData.Value);
    //    //        }
    //    //        CmdData.BindByName = true;
    //    //    }

    //    //    CmdData.ExecuteNonQuery();
    //    //}
    //    public static void SQLNonQuery(string Sql, Hashtable ParamTable = null)
    //    {
    //        MySqlCommand CmdData = new MySqlCommand();
    //        CmdData.CommandText = Sql;
    //        CmdData.Connection = con;
    //        CmdData.Transaction = Trans;
    //        CmdData.CommandTimeout = 180;
    //        if (ParamTable != null)
    //        {
    //            foreach (DictionaryEntry DeData in ParamTable)
    //            {
    //                CmdData.Parameters.AddWithValue(DeData.Key.ToString(), DeData.Value);
    //            }
    //        }
    //        CmdData.ExecuteNonQuery();
    //    }
    //    public static object SQLScalar(string Sql, Hashtable ParamTable = null)
    //    {
    //        MySqlCommand CmdData = new MySqlCommand();
    //        // Warning!!! Optional parameters not supported

    //        CmdData.CommandText = Sql;
    //        CmdData.Connection = con;
    //        CmdData.Transaction = Trans;
    //        if (ParamTable != null)
    //        {
    //            foreach (DictionaryEntry DeData in ParamTable)
    //            {
    //                CmdData.Parameters.AddWithValue(DeData.Key.ToString(), DeData.Value);
    //            }

    //        }

    //        return CmdData.ExecuteScalar();
    //    }

    //    public static MySqlDataReader SQLReader(string Sql, Hashtable ParamTable = null)
    //    {
    //        MySqlCommand CmdData = new MySqlCommand();
    //        // Warning!!! Optional parameters not supported

    //        if (Rdr != null)
    //        {
    //            Rdr.Close();
    //        }

    //        CmdData.CommandText = Sql;
    //        CmdData.Connection = con;
    //        if (ParamTable != null)
    //        {
    //            foreach (DictionaryEntry DeData in ParamTable)
    //            {
    //                CmdData.Parameters.AddWithValue(DeData.Key.ToString(), DeData.Value);
    //            }

    //        }
    //        Rdr = CmdData.ExecuteReader();
    //        return Rdr;
    //    }

    //    //-------------------------------------------------------------------------------------------
    //    //private static string s = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
    //    //public static OracleConnection con;
    //    //private static OracleCommand cmd;
    //    //public static OracleTransaction Trans;
    //    //public static OracleDataReader Rdr;
    //    //public static OracleDataAdapter da;
    //    //static Utility()
    //    //{
    //    //    try
    //    //    {


    //    //        try
    //    //        {
    //    //            string[] data = s.Split(',');
    //    //            if (con != null)
    //    //            {
    //    //                if (con.State == ConnectionState.Closed)
    //    //                {
    //    //                    con = new OracleConnection(data[0]);
    //    //                    con.Open();
    //    //                }
    //    //            }
    //    //            else
    //    //            {
    //    //                con = new OracleConnection(data[0]);
    //    //                con.Open();
    //    //            }
    //    //        }
    //    //        catch (Exception ex)
    //    //        {
    //    //            MessageBox.Show(ex.Message.ToString());
    //    //        }
    //    //    }
    //    //    catch (Exception ex)
    //    //    {
    //    //        throw ex;
    //    //    }

    //    //}
    //    //public void Connect()
    //    //{

    //    //    try
    //    //    {
    //    //        if (con.State == ConnectionState.Closed)
    //    //        {
    //    //            con.Open();
    //    //        }
    //    //    }
    //    //    catch (Exception ex)
    //    //    {
    //    //        throw ex;
    //    //    }

    //    //}
    //    //public void DisConnect()
    //    //{
    //    //    try
    //    //    {
    //    //        if (con.State == ConnectionState.Open)
    //    //        {
    //    //            con.Close();

    //    //        }

    //    //    }
    //    //    catch (Exception ex)
    //    //    {
    //    //        throw ex;
    //    //    }
    //    //}
    //    //public static DataSet ExecuteSelectQuery(string query, string tblname)
    //    //{
    //    //    DataSet ds = new DataSet();

    //    //    try
    //    //    {
    //    //        da = new OracleDataAdapter(query, con);
    //    //        da.Fill(ds, tblname);
    //    //    }
    //    //    catch { }
    //    //    return ds;
    //    //}
    //    //public static bool ExecuteNonQuery(string query)
    //    //{
    //    //    cmd = new OracleCommand(query, con);
    //    //    cmd.ExecuteNonQuery();
    //    //    return true;
    //    //}
    //    //public static bool ExecuteNonQuery(string query,Byte[] s)
    //    //{

    //    //    cmd = new OracleCommand(query, con);
    //    //    cmd.Parameters.Add(":EMPIMAGE", s);
    //    //    cmd.ExecuteNonQuery();

    //    //    return true;
    //    //}
    //    //public static OracleDataReader ExecuteReader(string query)
    //    //{

    //    //    cmd = new OracleCommand(query, con);
    //    //    OracleDataReader dr = cmd.ExecuteReader();
    //    //    return dr;
    //    //}
    //    //public static object ExecuteScalar(string sql)
    //    //{

    //    //    cmd = new OracleCommand(sql, con);
    //    //    object scalarValue = cmd.ExecuteScalar();
    //    //    return scalarValue;

    //    //}

    //    //public static DataTable SQLQuery(string Sql, Hashtable ParamTable = null)
    //    //{
    //    //    OracleCommand CmdData = new OracleCommand();
    //    //    // Warning!!! Optional parameters not supported
    //    //    OracleDataAdapter Sda = new OracleDataAdapter();
    //    //    DataSet Ds = new DataSet();

    //    //    CmdData.CommandText = Sql;
    //    //    CmdData.Connection = con;
    //    //    CmdData.Transaction = Trans;
    //    //    CmdData.CommandTimeout = 180;
    //    //    if (ParamTable != null)
    //    //    {
    //    //        foreach (DictionaryEntry DeData in ParamTable)
    //    //        {
    //    //            CmdData.Parameters.Add(DeData.Key.ToString(), DeData.Value);
    //    //        }

    //    //    }

    //    //    Sda = new OracleDataAdapter(CmdData);
    //    //    Ds = new DataSet();
    //    //    Sda.Fill(Ds, "tabresult");
    //    //    DataTable SQLQuery = new DataTable();
    //    //    SQLQuery.Clear();
    //    //    SQLQuery = Ds.Tables["tabresult"];
    //    //    return SQLQuery;
    //    //}


    //    //public static void Load_ListCombo(object Sender, string Sql, string ValMem, string DisMem, Hashtable ParamTable = null, string DefValue = "", string DefValueOrder = "TOP", bool QueryUpdate = true)
    //    //{
    //    //    // ''''' Load Combo / Listbox
    //    //    // Warning!!! Optional parameters not supported
    //    //    try
    //    //    {
    //    //        OracleCommand CmdData = new OracleCommand();

    //    //        CmdData.CommandText = Sql;
    //    //        CmdData.Connection = con;
    //    //        if (ParamTable != null)
    //    //        {
    //    //            foreach (DictionaryEntry DeData in ParamTable)
    //    //            {
    //    //                CmdData.Parameters.Add(DeData.Key.ToString(), DeData.Value);
    //    //            }

    //    //        }

    //    //        OracleDataAdapter Sda = new OracleDataAdapter(CmdData);
    //    //        DataTable Dt = new DataTable();
    //    //        Sda.Fill(Dt);
    //    //        if (DefValue != "")
    //    //        {
    //    //            DataRow Row;
    //    //            Row = Dt.NewRow();
    //    //            Row[ValMem] = 0;
    //    //            Row[DisMem] = DefValue;
    //    //            if (DefValueOrder.ToUpper() == "BOTTOM")
    //    //            {
    //    //                if (Dt.Rows.Count + 1 > 0)
    //    //                {
    //    //                    Dt.Rows.InsertAt(Row, Dt.Rows.Count + 1);
    //    //                }
    //    //                else
    //    //                {
    //    //                    Dt.Rows.InsertAt(Row, 0);
    //    //                }
    //    //            }
    //    //            else
    //    //            {
    //    //                Dt.Rows.InsertAt(Row, 0);
    //    //            }
    //    //        }
    //    //        ((ComboBox)Sender).DisplayMember = DisMem;
    //    //        ((ComboBox)Sender).ValueMember = ValMem;
    //    //        ((ComboBox)Sender).DataSource = Dt;
    //    //        if (((ComboBox)Sender).DropDownStyle == ComboBoxStyle.DropDown)
    //    //        {
    //    //            if (DefValue != "")
    //    //            {
    //    //                ((ComboBox)Sender).SelectedIndex = 0;
    //    //            }
    //    //            else
    //    //            {
    //    //                ((ComboBox)Sender).SelectedIndex = -1;
    //    //            }
    //    //        }
    //    //    }
    //    //    catch (Exception ex)
    //    //    {
    //    //    }
    //    //    finally
    //    //    {

    //    //    }

    //    //}
    //    //public static void Load_DataGrid(object Sender, string Sql, Hashtable ParamTable = null)
    //    //{
    //    //    OracleCommand CmdData = new OracleCommand();
    //    //    // Warning!!! Optional parameters not supported
    //    //    OracleDataAdapter Sda = new OracleDataAdapter();
    //    //    DataSet Ds = new DataSet();
    //    //    DataView Dv = new DataView();
    //    //    try
    //    //    {

    //    //        CmdData.CommandText = Sql;
    //    //        CmdData.Connection = con;
    //    //        CmdData.CommandTimeout = 180;
    //    //        if (ParamTable != null)
    //    //        {
    //    //            foreach (DictionaryEntry DeData in ParamTable)
    //    //            {
    //    //                CmdData.Parameters.Add(DeData.Key.ToString(), DeData.Value);
    //    //            }

    //    //        }

    //    //        Sda = new OracleDataAdapter(CmdData);
    //    //        Ds = new DataSet();
    //    //        Sda.Fill(Ds, "tabresult");
    //    //        ((DataGridView)(Sender)).DataSource = Ds.Tables[0];
    //    //    }
    //    //    catch (Exception ex)
    //    //    {
    //    //    }
    //    //    finally
    //    //    {

    //    //    }

    //    //}


    //    //public static void SQLNonQuery(string Sql, Hashtable ParamTable = null)
    //    //{
    //    //    OracleCommand CmdData = new OracleCommand();
    //    //    // Warning!!! Optional parameters not supported

    //    //    CmdData.CommandText = Sql;
    //    //    CmdData.Connection = con;
    //    //    CmdData.Transaction = Trans;
    //    //    CmdData.CommandTimeout = 180;
    //    //    if (ParamTable != null)
    //    //    {
    //    //        foreach (DictionaryEntry DeData in ParamTable)
    //    //        {
    //    //            CmdData.Parameters.Add(DeData.Key.ToString().Replace('@', ':'), DeData.Value);
    //    //        }
    //    //        CmdData.BindByName = true;
    //    //    }

    //    //    CmdData.ExecuteNonQuery();
    //    //}
    //    //public static object SQLScalar(string Sql, Hashtable ParamTable = null)
    //    //{
    //    //    OracleCommand CmdData = new OracleCommand();
    //    //    // Warning!!! Optional parameters not supported

    //    //    CmdData.CommandText = Sql;
    //    //    CmdData.Connection = con;
    //    //    CmdData.Transaction = Trans;
    //    //    if (ParamTable != null)
    //    //    {
    //    //        foreach (DictionaryEntry DeData in ParamTable)
    //    //        {
    //    //            CmdData.Parameters.Add(DeData.Key.ToString(), DeData.Value);
    //    //        }

    //    //    }

    //    //    return CmdData.ExecuteScalar();
    //    //}

    //    //public static OracleDataReader SQLReader(string Sql, Hashtable ParamTable = null)
    //    //{
    //    //    OracleCommand CmdData = new OracleCommand();
    //    //    // Warning!!! Optional parameters not supported

    //    //    if (Rdr != null)
    //    //    {
    //    //        Rdr.Close();
    //    //    }

    //    //    CmdData.CommandText = Sql;
    //    //    CmdData.Connection = con;
    //    //    if (ParamTable != null)
    //    //    {
    //    //        foreach (DictionaryEntry DeData in ParamTable)
    //    //        {
    //    //            CmdData.Parameters.Add(DeData.Key.ToString(), DeData.Value);
    //    //        }

    //    //    }
    //    //    Rdr = CmdData.ExecuteReader();
    //    //    return Rdr;
    //    //}
    //}
}
