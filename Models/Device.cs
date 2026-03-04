using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OracleClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace Pinnacle.Models
{
    class Device
    {
        public DataTable FromIp(string s)
        {
            string sel = ""; DataTable dt1=new DataTable();
            if (Class.Users.HUnit != "HOSTEL" && Class.Users.HCompcode == "AGF" || Class.Users.HUnit != "HOSTEL" && Class.Users.HCompcode == "AGFM" || Class.Users.HUnit != "HOSTEL" && Class.Users.HCompcode == "AGFMGII" || Class.Users.HUnit != "HOSTEL" && Class.Users.HCompcode == "AGFC" || Class.Users.HUnit != "HOSTEL" && Class.Users.HCompcode == "AGFP" || Class.Users.HUnit != "HOSTEL" && Class.Users.HCompcode == "AGFK" || Class.Users.HUnit != "HOSTEL" && Class.Users.HCompcode == "FLF" || Class.Users.HUnit != "HOSTEL" && Class.Users.HCompcode == "VEL" || Class.Users.HUnit != "HOSTEL" && Class.Users.HCompcode == "FLFD")
            {
                sel = "SELECT DISTINCT  '----' AS MACIP FROM DUAL UNION SELECT  distinct C.MACIP  FROM  ASPTBLMACHINEMAS A    JOIN   GTCOMPMAST B ON B.GTCOMPMASTID = A.COMPCODE    JOIN ASPTBLMACIP C ON C.ASPTBLMACIPID=A.IPADDRESS   AND C.ACTIVE='T'    JOIN  ASPTBLUSERMAS D ON D.USERID=A.WARDENNAME    WHERE  B.COMPCODE='" + s + "'   ORDER BY 1";
                DataSet ds = Utility.ExecuteSelectQuery(sel, "ASPTBLMACIP");
                 dt1 = ds.Tables["ASPTBLMACIP"];
            }
            if (Class.Users.HUnit == "HOSTEL" || Class.Users.HUnit == "Hostel")
            {
                sel = "SELECT DISTINCT  '----' AS MACIP FROM DUAL UNION SELECT DISTINCT  '' AS MACIP FROM DUAL UNION SELECT   B.MACIP FROM HRMACIPENTRY A JOIN HRMACIPENTRYDET B ON A.HRMACIPENTRYID = B.HRMACIPENTRYID  JOIN GTCOMPMAST C ON C.GTCOMPMASTID = A.COMPCODE    ORDER BY  MACIP DESC";
                DataSet ds = Utility.ExecuteSelectQuery(sel, "HRMACIPENTRY");
                dt1 = ds.Tables["HRMACIPENTRY"];
            }
            if (dt1.Rows.Count <= 0)
            {
                if (Class.Users.HUnit == "LOPPL")
                {
                    sel = "SELECT DISTINCT  '----' AS MACIP    FROM DUAL UNION SELECT   B.MACIP || '/' || B.NOTE AS MACIP FROM HRMACIPENTRY A JOIN HRMACIPENTRYDET B ON A.HRMACIPENTRYID = B.HRMACIPENTRYID  JOIN GTCOMPMAST C ON C.GTCOMPMASTID = A.COMPCODE  WHERE  B.CURMAC = 'YES' AND C.COMPCODE= '" + s + "'  ORDER BY  MACIP DESC";
                    DataSet ds = Utility.ExecuteSelectQuery(sel, "HRMACIPENTRY");
                    dt1 = ds.Tables["HRMACIPENTRY"];
                }
                else
                {
                    sel = "SELECT DISTINCT  '----' AS MACIP FROM DUAL UNION SELECT   B.MACIP FROM HRMACIPENTRY A JOIN HRMACIPENTRYDET B ON A.HRMACIPENTRYID = B.HRMACIPENTRYID  JOIN GTCOMPMAST C ON C.GTCOMPMASTID = A.COMPCODE  WHERE  B.CURMAC = 'YES' AND C.COMPCODE= '" + s + "'  ORDER BY  MACIP DESC";
                    DataSet ds = Utility.ExecuteSelectQuery(sel, "HRMACIPENTRY");
                    dt1 = ds.Tables["HRMACIPENTRY"];
                }
            }

            return dt1;

        }
        public DataTable FromIp()
        {
            string sel = ""; DataTable dt1 = new DataTable();
            if (Class.Users.HUnit != "HOSTEL" && Class.Users.HCompcode == "AGF" || Class.Users.HUnit != "HOSTEL" && Class.Users.HCompcode == "AGFM" || Class.Users.HUnit != "HOSTEL" && Class.Users.HCompcode == "AGFMGII" || Class.Users.HUnit != "HOSTEL" && Class.Users.HCompcode == "AGFC" || Class.Users.HUnit != "HOSTEL" && Class.Users.HCompcode == "AGFP" || Class.Users.HUnit != "HOSTEL" && Class.Users.HCompcode == "AGFK" || Class.Users.HUnit != "HOSTEL" && Class.Users.HCompcode == "FLF" || Class.Users.HUnit != "HOSTEL" && Class.Users.HCompcode == "VEL" || Class.Users.HUnit != "HOSTEL" && Class.Users.HCompcode == "FLFD")
            {
                sel = "SELECT C.ASPTBLMACIPID, C.MACIP  FROM  ASPTBLMACHINEMAS A    JOIN   GTCOMPMAST B ON B.GTCOMPMASTID = A.COMPCODE    JOIN ASPTBLMACIP C ON C.ASPTBLMACIPID=A.IPADDRESS   AND C.ACTIVE='T'    JOIN  ASPTBLUSERMAS D ON D.USERID=A.WARDENNAME    WHERE  B.COMPCODE='" + Class.Users.HCompcode + "' AND  D.USERNAME='" + Class.Users.HUserName + "'   ORDER BY 2";
                DataSet ds1 = Utility.ExecuteSelectQuery(sel, "ASPTBLMACIP");
                dt1 = ds1.Tables["ASPTBLMACIP"];
            }
            if (Class.Users.HUnit == "HOSTEL" || Class.Users.HUnit == "Hostel")
            {
                sel = "SELECT B.HRMACIPENTRYDETID,B.MACIP FROM HRMACIPENTRY A JOIN HRMACIPENTRYDET B ON A.HRMACIPENTRYID = B.HRMACIPENTRYID  JOIN GTCOMPMAST C ON C.GTCOMPMASTID = A.COMPCODE  WHERE B.DEFAULTYN = 'NO' AND B.CURMAC = 'Hostel'  ORDER BY 2";
                DataSet ds1 = Utility.ExecuteSelectQuery(sel, "HRMACIPENTRY");
                dt1 = ds1.Tables["HRMACIPENTRY"];
            }
            if (dt1.Rows.Count <= 0)
            {

                if (Class.Users.HUnit == "LOPPL")
                {
                    sel = "SELECT B.HRMACIPENTRYDETID,B.MACIP  || '/' || B.NOTE AS MACIP FROM HRMACIPENTRY A JOIN HRMACIPENTRYDET B ON A.HRMACIPENTRYID = B.HRMACIPENTRYID  JOIN GTCOMPMAST C ON C.GTCOMPMASTID = A.COMPCODE  WHERE  B.CURMAC = 'YES' AND C.COMPCODE='" + Class.Users.HCompcode + "'   ORDER BY 2";
                    DataSet ds1 = Utility.ExecuteSelectQuery(sel, "HRMACIPENTRY");
                    dt1 = ds1.Tables["HRMACIPENTRY"];
                }
                else
                {
                    sel = "SELECT B.HRMACIPENTRYDETID,B.MACIP FROM HRMACIPENTRY A JOIN HRMACIPENTRYDET B ON A.HRMACIPENTRYID = B.HRMACIPENTRYID  JOIN GTCOMPMAST C ON C.GTCOMPMASTID = A.COMPCODE  WHERE B.DEFAULTYN = 'NO' AND B.CURMAC = 'YES'  ORDER BY 2";
                    DataSet ds1 = Utility.ExecuteSelectQuery(sel, "HRMACIPENTRY");
                    dt1 = ds1.Tables["HRMACIPENTRY"];
                }
            }
            return dt1;

        }
        public DataTable AllIp(string compcode)
        {
            
            string sel = ""; DataTable dt1 = new DataTable();
            if (Class.Users.HUnit != "HOSTEL" && Class.Users.HCompcode == "AGF" || Class.Users.HUnit != "HOSTEL" && Class.Users.HCompcode == "AGFM" || Class.Users.HUnit != "HOSTEL" && Class.Users.HCompcode == "AGFMGII" || Class.Users.HUnit != "HOSTEL" && Class.Users.HCompcode == "AGFC" || Class.Users.HUnit != "HOSTEL" && Class.Users.HCompcode == "AGFP" || Class.Users.HUnit != "HOSTEL" && Class.Users.HCompcode == "AGFK" || Class.Users.HUnit != "HOSTEL" && Class.Users.HCompcode == "FLF" || Class.Users.HUnit != "HOSTEL" && Class.Users.HCompcode == "VEL" || Class.Users.HUnit != "HOSTEL" && Class.Users.HCompcode == "FLFD")
            {
                sel = "SELECT distinct C.MACIP  FROM  ASPTBLMACHINEMAS A    JOIN   GTCOMPMAST B ON B.GTCOMPMASTID = A.COMPCODE    JOIN ASPTBLMACIP C ON C.ASPTBLMACIPID=A.IPADDRESS   AND C.ACTIVE='T'    JOIN  ASPTBLUSERMAS D ON D.USERID=A.WARDENNAME  where b.compcode='" + compcode + "'    ORDER BY 1";
                DataSet ds1 = Utility.ExecuteSelectQuery(sel, "ASPTBLMACIP");
                dt1 = ds1.Tables["ASPTBLMACIP"];
            }
            if (Class.Users.HUnit == "HOSTEL" || Class.Users.HUnit == "Hostel")
            {
                sel = "SELECT distinct B.MACIP FROM HRMACIPENTRY A JOIN HRMACIPENTRYDET B ON A.HRMACIPENTRYID = B.HRMACIPENTRYID  JOIN GTCOMPMAST C ON C.GTCOMPMASTID = A.COMPCODE  WHERE B.DEFAULTYN = 'NO' AND B.CURMAC = 'Hostel' and c.compcode='" + compcode + "'  ORDER BY 1";
                DataSet ds1 = Utility.ExecuteSelectQuery(sel, "HRMACIPENTRY");
                dt1 = ds1.Tables["HRMACIPENTRY"];
            }
            if (dt1.Rows.Count <= 0)
            {
                if (Class.Users.HUnit == "LOPPL")
                {
                    sel = "SELECT distinct B.MACIP  || '/' || B.NOTE AS MACIP FROM HRMACIPENTRY A JOIN HRMACIPENTRYDET B ON A.HRMACIPENTRYID = B.HRMACIPENTRYID  JOIN GTCOMPMAST C ON C.GTCOMPMASTID = A.COMPCODE  WHERE  B.CURMAC = 'YES' and c.compcode='" + compcode + "' ORDER BY 1";
                    DataSet ds1 = Utility.ExecuteSelectQuery(sel, "HRMACIPENTRY");
                    dt1 = ds1.Tables["HRMACIPENTRY"];
                }
                else
                {
                    sel = "SELECT distinct B.MACIP FROM HRMACIPENTRY A JOIN HRMACIPENTRYDET B ON A.HRMACIPENTRYID = B.HRMACIPENTRYID  JOIN GTCOMPMAST C ON C.GTCOMPMASTID = A.COMPCODE  WHERE B.DEFAULTYN = 'NO' AND B.CURMAC = 'YES' and c.compcode='" + compcode + "' ORDER BY 1";
                    DataSet ds1 = Utility.ExecuteSelectQuery(sel, "HRMACIPENTRY");
                    dt1 = ds1.Tables["HRMACIPENTRY"];
                }
            }
            return dt1;


        }
        public DataTable AllIp()
        {
            string sel = ""; DataTable dt1=new DataTable();
            if (Class.Users.HUnit != "HOSTEL" && Class.Users.HCompcode == "AGF" || Class.Users.HUnit != "HOSTEL" && Class.Users.HCompcode == "AGFM" || Class.Users.HUnit != "HOSTEL" && Class.Users.HCompcode == "AGFMGII" || Class.Users.HUnit != "HOSTEL" && Class.Users.HCompcode == "AGFC" || Class.Users.HUnit != "HOSTEL" && Class.Users.HCompcode == "AGFP" || Class.Users.HUnit != "HOSTEL" && Class.Users.HCompcode == "AGFK" || Class.Users.HUnit != "HOSTEL" && Class.Users.HCompcode == "FLF" || Class.Users.HUnit != "HOSTEL" && Class.Users.HCompcode == "VEL" || Class.Users.HUnit != "HOSTEL" && Class.Users.HCompcode == "FLFD")
            {
                sel = "SELECT distinct C.MACIP  FROM  ASPTBLMACHINEMAS A    JOIN   GTCOMPMAST B ON B.GTCOMPMASTID = A.COMPCODE    JOIN ASPTBLMACIP C ON C.ASPTBLMACIPID=A.IPADDRESS   AND C.ACTIVE='T'    JOIN  ASPTBLUSERMAS D ON D.USERID=A.WARDENNAME      ORDER BY 1";
                DataSet ds1 = Utility.ExecuteSelectQuery(sel, "ASPTBLMACIP");
                dt1 = ds1.Tables["ASPTBLMACIP"];
            }
            if (Class.Users.HUnit == "HOSTEL" || Class.Users.HUnit == "Hostel")
            {
                sel = "SELECT distinct B.MACIP FROM HRMACIPENTRY A JOIN HRMACIPENTRYDET B ON A.HRMACIPENTRYID = B.HRMACIPENTRYID  JOIN GTCOMPMAST C ON C.GTCOMPMASTID = A.COMPCODE  WHERE B.DEFAULTYN = 'NO' AND B.CURMAC = 'Hostel'  ORDER BY 1";
                DataSet ds1 = Utility.ExecuteSelectQuery(sel, "HRMACIPENTRY");
                dt1 = ds1.Tables["HRMACIPENTRY"];
            }
            if(dt1.Rows.Count<=0)
            {
                if (Class.Users.HUnit == "LOPPL")
                {
                    sel = "SELECT distinct B.MACIP  || '/' || B.NOTE AS MACIP FROM HRMACIPENTRY A JOIN HRMACIPENTRYDET B ON A.HRMACIPENTRYID = B.HRMACIPENTRYID  JOIN GTCOMPMAST C ON C.GTCOMPMASTID = A.COMPCODE  WHERE  B.CURMAC = 'YES'  ORDER BY 1";
                    DataSet ds1 = Utility.ExecuteSelectQuery(sel, "HRMACIPENTRY");
                    dt1 = ds1.Tables["HRMACIPENTRY"];
                }
                else
                {
                    sel = "SELECT distinct B.MACIP FROM HRMACIPENTRY A JOIN HRMACIPENTRYDET B ON A.HRMACIPENTRYID = B.HRMACIPENTRYID  JOIN GTCOMPMAST C ON C.GTCOMPMASTID = A.COMPCODE  WHERE B.DEFAULTYN = 'NO' AND B.CURMAC = 'YES'  ORDER BY 1";
                    DataSet ds1 = Utility.ExecuteSelectQuery(sel, "HRMACIPENTRY");
                    dt1 = ds1.Tables["HRMACIPENTRY"];
                }
            }

            return dt1;


        }
        public DataTable AllIp(string s, string ss)
        {
            string sel = ""; DataTable dt1 = new DataTable();
            if (Class.Users.HUnit != "HOSTEL" && Class.Users.HCompcode == "AGF" || Class.Users.HUnit != "HOSTEL" && Class.Users.HCompcode == "AGFM" || Class.Users.HUnit != "HOSTEL" && Class.Users.HCompcode == "AGFMGII" || Class.Users.HUnit != "HOSTEL" && Class.Users.HCompcode == "AGFC" || Class.Users.HUnit != "HOSTEL" && Class.Users.HCompcode == "AGFP" || Class.Users.HUnit != "HOSTEL" && Class.Users.HCompcode == "AGFK" || Class.Users.HUnit != "HOSTEL" && Class.Users.HCompcode == "FLF" || Class.Users.HUnit != "HOSTEL" && Class.Users.HCompcode == "VEL" || Class.Users.HUnit != "HOSTEL" && Class.Users.HCompcode == "FLFD")
            {
                sel = " SELECT C.ASPTBLMACIPID, C.MACIP,c.MACIP,c.MACNO,c.MTYPE,c.MTYPE2  FROM  ASPTBLMACHINEMAS A    JOIN   GTCOMPMAST B ON B.GTCOMPMASTID = A.COMPCODE    JOIN ASPTBLMACIP C ON C.ASPTBLMACIPID=A.IPADDRESS   AND C.ACTIVE='T'    JOIN  ASPTBLUSERMAS D ON D.USERID=A.WARDENNAME    and C.MACIP NOT IN( SELECT D.MACIP FROM ASPTBLMACIP D WHERE D.MACIP='" + ss + "' ) WHERE  B.COMPCODE='" + s + "' AND  D.USERNAME='" + Class.Users.HUserName + "'  AND A.ACTIVE='T'  ORDER BY 2";
                DataSet ds1 = Utility.ExecuteSelectQuery(sel, "ASPTBLMACIP");
                dt1 = ds1.Tables["ASPTBLMACIP"];
            }
            if (Class.Users.HUnit == "HOSTEL" || Class.Users.HUnit == "Hostel")
            {
                sel = "SELECT B.HRMACIPENTRYDETID,B.MACIP FROM HRMACIPENTRY A JOIN HRMACIPENTRYDET B ON A.HRMACIPENTRYID = B.HRMACIPENTRYID  JOIN GTCOMPMAST C ON C.GTCOMPMASTID = A.COMPCODE  WHERE B.DEFAULTYN = 'NO' AND B.CURMAC = 'Hostel'  AND B.MACIP NOT IN( SELECT D.MACIP FROM HRMACIPENTRYDET D WHERE D.MACIP='" + ss + "' ) ORDER BY 2";
                DataSet ds1 = Utility.ExecuteSelectQuery(sel, "HRMACIPENTRY");
                dt1 = ds1.Tables["HRMACIPENTRY"];
            }
            if (dt1.Rows.Count <= 0)
            {
                if (Class.Users.HUnit == "LOPPL")
                {
                    sel = "SELECT B.HRMACIPENTRYDETID,B.MACIP  || '/' || B.NOTE AS MACIP FROM HRMACIPENTRY A JOIN HRMACIPENTRYDET B ON A.HRMACIPENTRYID = B.HRMACIPENTRYID  JOIN GTCOMPMAST C ON C.GTCOMPMASTID = A.COMPCODE  WHERE  B.CURMAC = 'YES' AND C.COMPCODE='" + s + "'  AND B.MACIP NOT IN( SELECT D.MACIP FROM HRMACIPENTRYDET D WHERE D.MACIP='" + ss + "' ) ORDER BY 2";
                    DataSet ds1 = Utility.ExecuteSelectQuery(sel, "HRMACIPENTRY");
                    dt1 = ds1.Tables["HRMACIPENTRY"];
                }
                else
                {


                    sel = "SELECT B.HRMACIPENTRYDETID,B.MACIP FROM HRMACIPENTRY A JOIN HRMACIPENTRYDET B ON A.HRMACIPENTRYID = B.HRMACIPENTRYID  JOIN GTCOMPMAST C ON C.GTCOMPMASTID = A.COMPCODE  WHERE B.DEFAULTYN = 'NO' AND B.CURMAC = 'YES' AND C.COMPCODE='" + s + "'  AND B.MACIP NOT IN( SELECT D.MACIP FROM HRMACIPENTRYDET D WHERE D.MACIP='" + ss + "' ) ORDER BY 2";
                    DataSet ds1 = Utility.ExecuteSelectQuery(sel, "HRMACIPENTRY");
                    dt1 = ds1.Tables["HRMACIPENTRY"];
                }
            }

            return dt1;
            
        }

        internal DataTable FindName(string id,string hUnit)
        {
            string sql = ""; DataTable dt;
            if (hUnit == "LOPPL")
            {
                sql = "";
                sql = $"SELECT C.FNAME  || '-' || E.MNAME AS EMPNAME FROM GTCOMPMAST B JOIN HREMPLOYMAST C ON C.COMPCODE = B.GTCOMPMASTID JOIN HREMPLOYDETAILS D ON C.HREMPLOYMASTID = D.HREMPLOYMASTID JOIN GTDEPTDESGMAST E ON E.GTDEPTDESGMASTID = D.DEPTNAME WHERE D.IDACTIVE='YES' AND  D.OLDIDNO='{id}' AND B.COMPCODE='" + hUnit + "'   ORDER BY C.IDCARDNO DESC";
                dt = Utility.ExecuteSelectQuery(sql, "HREMPLOYDETAILS").Tables[0];
                if (dt != null && dt.Rows.Count > 0)
                {
                    return dt;
                }
                else
                {
                    sql = ""; 
                    sql = $"SELECT C.FNAME || '-' || E.MNAME AS EMPNAME FROM GTCOMPMAST B JOIN HREMPLOYMAST C ON C.COMPCODE = B.GTCOMPMASTID JOIN HREMPLOYDETAILS D ON C.HREMPLOYMASTID = D.HREMPLOYMASTID JOIN GTDEPTDESGMAST E ON E.GTDEPTDESGMASTID = D.DEPTNAME WHERE D.IDACTIVE='YES' AND  D.OLDIDNO='{id}'  ORDER BY C.IDCARDNO DESC";
                    dt = Utility.ExecuteSelectQuery(sql, "HREMPLOYDETAILS").Tables[0];
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        return dt;
                    }
                    else
                    {
                        sql = "";
                        sql = $"SELECT C.FNAME || '-' || E.MNAME AS EMPNAME FROM GTCOMPMAST B JOIN HREMPLOYMAST C ON C.COMPCODE = B.GTCOMPMASTID JOIN HREMPLOYDETAILS D ON C.HREMPLOYMASTID = D.HREMPLOYMASTID JOIN GTDEPTDESGMAST E ON E.GTDEPTDESGMASTID = D.DEPTNAME WHERE D.IDACTIVE='NO' AND  D.OLDIDNO='{id}' AND B.COMPCODE='" + hUnit + "'   ORDER BY C.IDCARDNO DESC";
                        dt = Utility.ExecuteSelectQuery(sql, "HREMPLOYDETAILS").Tables[0];
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            return dt;
                        }
                        else
                        {
                            sql = "";
                            sql = $"SELECT C.FNAME || '-' || E.MNAME AS EMPNAME FROM GTCOMPMAST B JOIN HREMPLOYMAST C ON C.COMPCODE = B.GTCOMPMASTID JOIN HREMPLOYDETAILS D ON C.HREMPLOYMASTID = D.HREMPLOYMASTID JOIN GTDEPTDESGMAST E ON E.GTDEPTDESGMASTID = D.DEPTNAME WHERE D.IDACTIVE='NO' AND  D.OLDIDNO='{id}'   ORDER BY C.IDCARDNO DESC";
                            dt = Utility.ExecuteSelectQuery(sql, "HREMPLOYDETAILS").Tables[0];
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                return dt;
                            }
                        }

                    }

                }
            }
            else
            {
                
                    sql = "";
                    sql = $"SELECT C.FNAME || '-' || E.MNNAME1 AS EMPNAME FROM GTCOMPMAST B JOIN HREMPLOYMAST C ON C.COMPCODE = B.GTCOMPMASTID JOIN HREMPLOYDETAILS D ON C.HREMPLOYMASTID = D.HREMPLOYMASTID JOIN GTDEPTDESGMAST E ON E.GTDEPTDESGMASTID = D.DEPTNAME WHERE D.IDACTIVE='YES' AND  D.MIDCARD='{id}' AND B.COMPCODE='" + hUnit + "'   ORDER BY C.IDCARDNO DESC";
                    dt = Utility.ExecuteSelectQuery(sql, "HREMPLOYDETAILS").Tables[0];
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        return dt;
                    }
                    else
                    {
                        sql = "";
                        sql = $"SELECT C.FNAME || '-' || E.MNNAME1 AS EMPNAME FROM GTCOMPMAST B JOIN HREMPLOYMAST C ON C.COMPCODE = B.GTCOMPMASTID JOIN HREMPLOYDETAILS D ON C.HREMPLOYMASTID = D.HREMPLOYMASTID JOIN GTDEPTDESGMAST E ON E.GTDEPTDESGMASTID = D.DEPTNAME WHERE D.IDACTIVE='YES' AND  D.MIDCARD='{id}'  ORDER BY C.IDCARDNO DESC";
                        dt = Utility.ExecuteSelectQuery(sql, "HREMPLOYDETAILS").Tables[0];
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            return dt;
                        }
                        else
                        {
                            sql = "";
                            sql = $"SELECT C.FNAME || '-' || E.MNNAME1 AS EMPNAME FROM GTCOMPMAST B JOIN HREMPLOYMAST C ON C.COMPCODE = B.GTCOMPMASTID JOIN HREMPLOYDETAILS D ON C.HREMPLOYMASTID = D.HREMPLOYMASTID JOIN GTDEPTDESGMAST E ON E.GTDEPTDESGMASTID = D.DEPTNAME WHERE D.IDACTIVE='NO' AND  D.MIDCARD='{id}' AND B.COMPCODE='" + hUnit + "'   ORDER BY C.IDCARDNO DESC";
                            dt = Utility.ExecuteSelectQuery(sql, "HREMPLOYDETAILS").Tables[0];
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                return dt;
                            }
                            else
                            {
                                sql = "";
                                sql = $"SELECT C.FNAME || '-' || E.MNNAME1 AS EMPNAME FROM GTCOMPMAST B JOIN HREMPLOYMAST C ON C.COMPCODE = B.GTCOMPMASTID JOIN HREMPLOYDETAILS D ON C.HREMPLOYMASTID = D.HREMPLOYMASTID JOIN GTDEPTDESGMAST E ON E.GTDEPTDESGMASTID = D.DEPTNAME WHERE D.IDACTIVE='NO' AND  D.MIDCARD='{id}'   ORDER BY C.IDCARDNO DESC";
                                dt = Utility.ExecuteSelectQuery(sql, "HREMPLOYDETAILS").Tables[0];
                                if (dt != null && dt.Rows.Count > 0)
                                {
                                    return dt;
                                }
                            }

                        }

                    }
                }
           
            return dt;
        }

        internal DataTable FindName1(string id, string hUnit)
        {
            string sql = ""; DataTable dt; 
            if (hUnit == "LOPPL")
            {
                sql = "";
                sql = $"SELECT C.FNAME || '-' || E.MNAME AS EMPNAME FROM GTCOMPMAST B JOIN HREMPLOYMAST C ON C.COMPCODE = B.GTCOMPMASTID JOIN HREMPLOYDETAILS D ON C.HREMPLOYMASTID = D.HREMPLOYMASTID JOIN GTDEPTDESGMAST E ON E.GTDEPTDESGMASTID = D.DEPTNAME WHERE D.IDACTIVE='YES' AND  D.OLDIDNO='{id}' AND B.COMPCODE='" + hUnit + "'   ORDER BY C.IDCARDNO DESC";
                 dt = Utility.ExecuteSelectQuery(sql, "HREMPLOYDETAILS").Tables[0];
                if (dt != null && dt.Rows.Count > 0)
                {
                    return dt;
                }
                else
                {
                    sql = "";
                    sql = $"SELECT C.FNAME || '-' || E.MNAME AS EMPNAME FROM GTCOMPMAST B JOIN HREMPLOYMAST C ON C.COMPCODE = B.GTCOMPMASTID JOIN HREMPLOYDETAILS D ON C.HREMPLOYMASTID = D.HREMPLOYMASTID JOIN GTDEPTDESGMAST E ON E.GTDEPTDESGMASTID = D.DEPTNAME WHERE D.IDACTIVE='YES' AND  D.OLDIDNO='{id}'  ORDER BY C.IDCARDNO DESC";
                    dt = Utility.ExecuteSelectQuery(sql, "HREMPLOYDETAILS").Tables[0];
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        return dt;
                    }
                    else
                    {
                        sql = "";
                        sql = $"SELECT C.FNAME || '-' || E.MNAME AS EMPNAME FROM GTCOMPMAST B JOIN HREMPLOYMAST C ON C.COMPCODE = B.GTCOMPMASTID JOIN HREMPLOYDETAILS D ON C.HREMPLOYMASTID = D.HREMPLOYMASTID JOIN GTDEPTDESGMAST E ON E.GTDEPTDESGMASTID = D.DEPTNAME WHERE D.IDACTIVE='NO' AND  D.OLDIDNO='{id}' AND B.COMPCODE='" + hUnit + "'   ORDER BY C.IDCARDNO DESC";
                        dt = Utility.ExecuteSelectQuery(sql, "HREMPLOYDETAILS").Tables[0];
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            return dt;
                        }
                        else
                        {
                            sql = "";
                            sql = $"SELECT C.FNAME || '-' || E.MNAME AS EMPNAME FROM GTCOMPMAST B JOIN HREMPLOYMAST C ON C.COMPCODE = B.GTCOMPMASTID JOIN HREMPLOYDETAILS D ON C.HREMPLOYMASTID = D.HREMPLOYMASTID JOIN GTDEPTDESGMAST E ON E.GTDEPTDESGMASTID = D.DEPTNAME WHERE D.IDACTIVE='NO' AND  D.OLDIDNO='{id}'   ORDER BY C.IDCARDNO DESC";
                            dt = Utility.ExecuteSelectQuery(sql, "HREMPLOYDETAILS").Tables[0];
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                return dt ;
                            }
                        }
                       
                    }

                }
            }
            else
            {

                sql = "";
                sql = $"SELECT C.FNAME || '-' || E.MNNAME1 AS EMPNAME FROM GTCOMPMAST B JOIN HREMPLOYMAST C ON C.COMPCODE = B.GTCOMPMASTID JOIN HREMPLOYDETAILS D ON C.HREMPLOYMASTID = D.HREMPLOYMASTID JOIN GTDEPTDESGMAST E ON E.GTDEPTDESGMASTID = D.DEPTNAME WHERE D.IDACTIVE='YES' AND  D.MIDCARD='{id}' AND B.COMPCODE='" + hUnit + "'   ORDER BY C.IDCARDNO DESC";
                dt = Utility.ExecuteSelectQuery(sql, "HREMPLOYDETAILS").Tables[0];
                if (dt != null && dt.Rows.Count > 0)
                {
                    return dt;
                }
                else
                {
                    sql = "";
                    sql = $"SELECT C.FNAME || '-' || E.MNNAME1 AS EMPNAME FROM GTCOMPMAST B JOIN HREMPLOYMAST C ON C.COMPCODE = B.GTCOMPMASTID JOIN HREMPLOYDETAILS D ON C.HREMPLOYMASTID = D.HREMPLOYMASTID JOIN GTDEPTDESGMAST E ON E.GTDEPTDESGMASTID = D.DEPTNAME WHERE D.IDACTIVE='YES' AND  D.MIDCARD='{id}'  ORDER BY C.IDCARDNO DESC";
                    dt = Utility.ExecuteSelectQuery(sql, "HREMPLOYDETAILS").Tables[0];
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        return dt;
                    }
                    else
                    {
                        sql = "";
                        sql = $"SELECT C.FNAME || '-' || E.MNNAME1 AS EMPNAME FROM GTCOMPMAST B JOIN HREMPLOYMAST C ON C.COMPCODE = B.GTCOMPMASTID JOIN HREMPLOYDETAILS D ON C.HREMPLOYMASTID = D.HREMPLOYMASTID JOIN GTDEPTDESGMAST E ON E.GTDEPTDESGMASTID = D.DEPTNAME WHERE D.IDACTIVE='NO' AND  D.MIDCARD='{id}' AND B.COMPCODE='" + hUnit + "'   ORDER BY C.IDCARDNO DESC";
                        dt = Utility.ExecuteSelectQuery(sql, "HREMPLOYDETAILS").Tables[0];
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            return dt;
                        }
                        else
                        {
                            sql = "";
                            sql = $"SELECT C.FNAME || '-' || E.MNNAME1 AS EMPNAME FROM GTCOMPMAST B JOIN HREMPLOYMAST C ON C.COMPCODE = B.GTCOMPMASTID JOIN HREMPLOYDETAILS D ON C.HREMPLOYMASTID = D.HREMPLOYMASTID JOIN GTDEPTDESGMAST E ON E.GTDEPTDESGMASTID = D.DEPTNAME WHERE D.IDACTIVE='NO' AND  D.MIDCARD='{id}'   ORDER BY C.IDCARDNO DESC";
                            dt = Utility.ExecuteSelectQuery(sql, "HREMPLOYDETAILS").Tables[0];
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                return dt;
                            }
                        }

                    }
                }

                }
                return dt;
        }

        public  DataTable IPLOAD(string s, string ss)
        {
            string sel = ""; DataTable dt1 = new DataTable();
            if (Class.Users.HUnit != "HOSTEL" && Class.Users.HCompcode == "AGF" || Class.Users.HUnit != "HOSTEL" && Class.Users.HCompcode == "AGFM" || Class.Users.HUnit != "HOSTEL" && Class.Users.HCompcode == "AGFMGII" || Class.Users.HUnit != "HOSTEL" && Class.Users.HCompcode == "AGFC" || Class.Users.HUnit != "HOSTEL" && Class.Users.HCompcode == "AGFP" || Class.Users.HUnit != "HOSTEL" && Class.Users.HCompcode == "AGFK" || Class.Users.HUnit != "HOSTEL" && Class.Users.HCompcode == "FLF" || Class.Users.HUnit != "HOSTEL" && Class.Users.HCompcode == "VEL" || Class.Users.HUnit != "HOSTEL" && Class.Users.HCompcode == "FLFD")
            {
                sel = "SELECT C.ASPTBLMACIPID, C.MACIP,c.MACIP,c.MACNO,c.MTYPE,c.MTYPE2  FROM  ASPTBLMACHINEMAS A    JOIN   GTCOMPMAST B ON B.GTCOMPMASTID = A.COMPCODE     JOIN ASPTBLMACIP C ON C.ASPTBLMACIPID=A.IPADDRESS   AND C.ACTIVE='T'    JOIN  ASPTBLUSERMAS D ON D.USERID=A.WARDENNAME    WHERE  B.COMPCODE='" + s + "' AND  D.USERNAME='" + Class.Users.HUserName + "' and C.MACIP='" + ss + "' AND A.ACTIVE='T'   ORDER BY 2";
                DataSet ds1 = Utility.ExecuteSelectQuery(sel, "ASPTBLMACIP");
                dt1 = ds1.Tables["ASPTBLMACIP"];
            }
            if (Class.Users.HUnit == "HOSTEL" || Class.Users.HUnit == "Hostel")
            {
                sel = "SELECT B.MACIP,B.MACNO,B.MTYPE,B.MTYPE2 FROM HRMACIPENTRY A JOIN HRMACIPENTRYDET B ON A.HRMACIPENTRYID = B.HRMACIPENTRYID JOIN GTCOMPMAST C ON C.GTCOMPMASTID = A.COMPCODE  WHERE  B.DEFAULTYN = 'NO' AND B.CURMAC = 'Hostel'   AND B.MACIP='" + ss + "'";
                DataSet ds1 = Utility.ExecuteSelectQuery(sel, "HRMACIPENTRYDET");
                dt1 = ds1.Tables["HRMACIPENTRYDET"];
            }
            if (Class.Users.HUnit == "CANTEEN" || Class.Users.HUnit == "Canteen")
            {
                sel = "SELECT B.MACIP,B.MACNO,B.MTYPE,B.MTYPE2 FROM HRMACIPENTRY A JOIN HRMACIPENTRYDET B ON A.HRMACIPENTRYID = B.HRMACIPENTRYID JOIN GTCOMPMAST C ON C.GTCOMPMASTID = A.COMPCODE  WHERE  B.DEFAULTYN = 'NO' AND B.CURMAC = '"+ Class.Users.HUnit + "'   AND B.MACIP='" + ss + "'";
                DataSet ds1 = Utility.ExecuteSelectQuery(sel, "HRMACIPENTRYDET");
                dt1 = ds1.Tables["HRMACIPENTRYDET"];
            }
            if (dt1.Rows.Count <= 0)
            {
                if (Class.Users.HUnit == "LOPPL")
                {
                    sel = "SELECT B.MACIP,B.MACNO,B.MTYPE,B.MTYPE2 FROM HRMACIPENTRY A JOIN HRMACIPENTRYDET B ON A.HRMACIPENTRYID = B.HRMACIPENTRYID JOIN GTCOMPMAST C ON C.GTCOMPMASTID = A.COMPCODE  WHERE B.CURMAC = 'YES'   AND C.COMPCODE='" + s + "' AND B.MACIP='" + ss + "'";
                    DataSet ds1 = Utility.ExecuteSelectQuery(sel, "HRMACIPENTRYDET");
                    dt1 = ds1.Tables["HRMACIPENTRYDET"];
                }
                else
                {
                    sel = "SELECT B.MACIP,B.MACNO,B.MTYPE,B.MTYPE2 FROM HRMACIPENTRY A JOIN HRMACIPENTRYDET B ON A.HRMACIPENTRYID = B.HRMACIPENTRYID JOIN GTCOMPMAST C ON C.GTCOMPMASTID = A.COMPCODE  WHERE B.DEFAULTYN = 'NO' AND  B.CURMAC = 'YES'   AND C.COMPCODE='" + s + "' AND B.MACIP='" + ss + "'";
                    DataSet ds1 = Utility.ExecuteSelectQuery(sel, "HRMACIPENTRYDET");
                    dt1 = ds1.Tables["HRMACIPENTRYDET"];
                }
            }

            return dt1;

        }

        public DataTable ToIp(string s)
        {
            string sel = ""; DataTable dt1 = new DataTable();Class.Users.Intimation = "PAYROLL";
            if (Class.Users.HUnit != "HOSTEL" && Class.Users.HCompcode == "AGF" || Class.Users.HUnit != "HOSTEL" && Class.Users.HCompcode == "AGFM" || Class.Users.HUnit != "HOSTEL" && Class.Users.HCompcode == "AGFMGII" || Class.Users.HUnit != "HOSTEL" && Class.Users.HCompcode == "AGFC" || Class.Users.HUnit != "HOSTEL" && Class.Users.HCompcode == "AGFP" || Class.Users.HUnit != "HOSTEL" && Class.Users.HCompcode == "AGFK" || Class.Users.HUnit != "HOSTEL" && Class.Users.HCompcode == "FLF" || Class.Users.HUnit != "HOSTEL" && Class.Users.HCompcode == "VEL"  || Class.Users.HUnit != "HOSTEL" && Class.Users.HCompcode == "FLFD")
            {
                sel = "SELECT C.ASPTBLMACIPID AS HRMACIPENTRYDETID, C.MACIP,c.MACIP,c.MACNO,c.MTYPE,c.MTYPE2  FROM  ASPTBLMACHINEMAS A    JOIN   GTCOMPMAST B ON B.GTCOMPMASTID = A.COMPCODE    JOIN ASPTBLMACIP C ON C.ASPTBLMACIPID=A.IPADDRESS   AND C.ACTIVE='T'    JOIN  ASPTBLUSERMAS D ON D.USERID=A.WARDENNAME  WHERE  B.COMPCODE='" + s + "' AND  D.USERNAME='" + Class.Users.HUserName + "' AND A.ACTIVE='T'   ORDER BY 2";
                DataSet ds1 = Utility.ExecuteSelectQuery(sel, "ASPTBLMACIP");
                dt1 = ds1.Tables["ASPTBLMACIP"];
            }
            if (Class.Users.HUnit == "HOSTEL" || Class.Users.HUnit == "Hostel")
            {
                sel = "SELECT B.HRMACIPENTRYDETID,B.MACIP FROM HRMACIPENTRY A JOIN HRMACIPENTRYDET B ON A.HRMACIPENTRYID = B.HRMACIPENTRYID  JOIN GTCOMPMAST C ON C.GTCOMPMASTID = A.COMPCODE  WHERE B.DEFAULTYN = 'NO' AND B.CURMAC = 'Hostel'   ORDER BY 2";
                DataSet ds1 = Utility.ExecuteSelectQuery(sel, "HRMACIPENTRYDET");
                dt1 = ds1.Tables["HRMACIPENTRYDET"];
            }
            
            if (Class.Users.HUnit == "CANTEEN" || Class.Users.HUnit == "Canteen")
            {
                sel = "SELECT B.HRMACIPENTRYDETID,B.MACIP FROM HRMACIPENTRY A JOIN HRMACIPENTRYDET B ON A.HRMACIPENTRYID = B.HRMACIPENTRYID  JOIN GTCOMPMAST C ON C.GTCOMPMASTID = A.COMPCODE  WHERE B.DEFAULTYN = 'NO' AND B.CURMAC = '" + Class.Users.HUnit + "' AND C.COMPCODE='" + Class.Users.HCompcode + "'  ORDER BY 2";
                DataSet ds1 = Utility.ExecuteSelectQuery(sel, "HRMACIPENTRYDET");
                dt1 = ds1.Tables["HRMACIPENTRYDET"];
            }
            if (dt1.Rows.Count <= 0)
            {
                if (Class.Users.HUnit == "LOPPL")
                {
                    sel = "SELECT B.HRMACIPENTRYDETID,B.MACIP  || '/' || B.NOTE AS MACIP FROM HRMACIPENTRY A JOIN HRMACIPENTRYDET B ON A.HRMACIPENTRYID = B.HRMACIPENTRYID  JOIN GTCOMPMAST C ON C.GTCOMPMASTID = A.COMPCODE  WHERE  B.CURMAC = 'YES' AND C.COMPCODE='" + s + "'  ORDER BY 2";
                    DataSet ds1 = Utility.ExecuteSelectQuery(sel, "HRMACIPENTRYDET");
                    dt1 = ds1.Tables["HRMACIPENTRYDET"];
                }
                else
                {
                    sel = "SELECT B.HRMACIPENTRYDETID,B.MACIP FROM HRMACIPENTRY A JOIN HRMACIPENTRYDET B ON A.HRMACIPENTRYID = B.HRMACIPENTRYID  JOIN GTCOMPMAST C ON C.GTCOMPMASTID = A.COMPCODE  WHERE B.DEFAULTYN = 'NO' AND B.CURMAC = 'YES' AND C.COMPCODE='" + s + "'  ORDER BY 2";
                    DataSet ds1 = Utility.ExecuteSelectQuery(sel, "HRMACIPENTRYDET");
                    dt1 = ds1.Tables["HRMACIPENTRYDET"];
                }
            }

            return dt1;
        }
        public DataTable fingerindex(string s)
        {
            string sel = "SELECT distinct  a.FINGER_INDEX FROM  TFTDevice a where A.CURMAC='T'  AND a.user_id='" + s + "' order by 1 ";
            DataSet ds1 = Utility.ExecuteSelectQuery(sel, "TFTDevice");
            DataTable dt1 = ds1.Tables["TFTDevice"];
            return dt1;
        }
       
        public DataTable userid()
        {
            string sel = "SELECT distinct a.USER_ID FROM  TFTDevice a  order by 1 ";
            DataSet ds1 = Utility.ExecuteSelectQuery(sel, "TFTDevice");
            DataTable dt1 = ds1.Tables["TFTDevice"];
            return dt1;
        }
        public static byte[] ImageToByteArray(PictureBox imageIn)
        {
            var ms = new MemoryStream();
            imageIn.Image.Save(ms, imageIn.Image.RawFormat);
            return ms.ToArray();
        }
        public static Image ByteArrayToImage1(byte[] byteArrayIn)
        {
            System.Drawing.ImageConverter converter = new System.Drawing.ImageConverter();
            Image img = (Image)converter.ConvertFrom(byteArrayIn);

            return img;
        }
        public static Image ByteArrayToImage(byte[] byteArrayIn)
        {
            var ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }

        public static string BytesToString(byte[] bytes)
        {
            using (MemoryStream stream = new MemoryStream(bytes))
            {
                using (StreamReader streamReader = new StreamReader(stream))
                {
                    return streamReader.ReadToEnd();
                }
            }
        }

        //internal static byte[] ImageToByteArray(Bitmap img)
        //{
           
        //}

        //public byte[] ImageAArray(System.Drawing.Image imagen)
        //{
        //    System.IO.MemoryStream ms = new System.IO.MemoryStream();
        //    imagen.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
        //    return ms.ToArray();
        //}
        //public System.Drawing.Image ArrayAImage(byte[] ArrBite)
        //{
        //    System.IO.MemoryStream ms = new System.IO.MemoryStream(ArrBite);
        //    System.Drawing.Image returnImage = System.Drawing.Image.FromStream(ms);
        //    return returnImage;
        //}
    }
}
