using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Pinnacle.Models
{
    class Employee
    {

        public Int64 ASPTBLEMPID { get; set; }
        public Int64 COMPCODE { get; set; }
        public Int64 COMNAME { get; set; }
        public string EMPNAME { get; set; }
        public Int64 EMPID { get; set; }
        public string LASTNAME { get; set; }
        public string ADDRESS { get; set; }
        public string GENDER { get; set; }
        public string DATEOFBIRTH { get; set; }
        public Int64 DEPARTMENT { get; set; }
        public string DATEOFJOIN { get; set; }
        public Int64 IDCARDNO { get; set; }
        public string CONTACT { get; set; }
        public string BLOODGROUP { get; set; }
        public string EMPLOYEETYPE { get; set; }
        public string ACTIVE { get; set; }
        public Int64 USERNAME { get; set; }
        public string IPADDRESS { get; set; }       
        public string CREATEDON { get; set; }
        public string MODIFIEDON { get; set; }
        public byte[] bytes { get; set; }
        public Int64 image { get; set; }
        public string ObjectId { get; set; }
        public string eformId { get; set; }
        public string fileContent { get; set; }
        public string fileName { get; set; }
        internal DataTable select(long cOMPCODE, Int64 EMPID, string lASTNAME,  string gENDER, string dATEOFBIRTH,string aDDRESS, long dEPARTMENT ,string dATEOFJOIN,long iDCARDNO, string aCTIVE,Int64 image,string eMPLOYEETYPE)
        {
          
            string sel = "select ASPTBLEMPID from ASPTBLEMP where COMPCODE=" + cOMPCODE + " AND EMPID ='" + EMPID + "' and LASTNAME='" + lASTNAME + "' and GENDER='" + gENDER + "' and DATEOFBIRTH='" + dATEOFBIRTH + "' and ADDRESS='" + aDDRESS + "'  and DEPARTMENT='" + dEPARTMENT + "' and DATEOFJOIN ='" + dATEOFJOIN + "' and IDCARDNO =" + iDCARDNO + "  and ACTIVE ='" + aCTIVE + "' and IMAGEBYTES ='" + image + "' and EMPLOYEETYPE='"+ eMPLOYEETYPE + "' ";
            DataSet ds = Utility.ExecuteSelectQuery(sel, "ASPTBLEMP");
            DataTable dt = ds.Tables["ASPTBLEMP"];
            return dt;
        }

        internal DataTable select(long cOMPCODE, string eMPNAME,long iDCARDNO)
        {

            string sel = "select ASPTBLEMPID from ASPTBLEMP where COMPCODE=" + cOMPCODE + " AND EMPNAME ='" + eMPNAME + "' AND IDCARDNO ='" + iDCARDNO + "'";
            DataSet ds = Utility.ExecuteSelectQuery(sel, "ASPTBLEMP");
            DataTable dt = ds.Tables["ASPTBLEMP"];
            return dt;
        }

        internal DataTable select(long cOMPCODE, long eMPID, long iDCARDNO, string aCTIVE, long image)
        {

            string sel = "select ASPTBLEMPID from ASPTBLEMP where COMPCODE=" + cOMPCODE + " AND EMPID ='" + EMPID + "'  and IDCARDNO =" + iDCARDNO + "  and ACTIVE ='" + aCTIVE + "' and IMAGEBYTES ='" + image + "' ";
            DataSet ds = Utility.ExecuteSelectQuery(sel, "ASPTBLEMP");
            DataTable dt = ds.Tables["ASPTBLEMP"];
            return dt;
        }
    }
}
