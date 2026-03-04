using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace Pinnacle.Models
{
   public class MenuName
    {
        public string msg { get; set; }
        public Int64 MENUNAMEID { get; set; }
        public string MENUNAME { get; set; }
        public string ACTIVE { get; set; }
        public bool chk { get; set; }
        public Int64 PARENTMENUID { get; set; }

        public string CREATEON { get; set; }
        public string CREATEBY { get; set; }
        public string MODIFYON { get; set; }
        public string IPADDRESS { get; set; }
      
        public DataTable select(string MENUNAME, string ACTIVE, Int64 PARENTMENUID)
        {
          
            this.MENUNAME = MENUNAME;
            this.ACTIVE = ACTIVE;
            this.PARENTMENUID = PARENTMENUID;
            string sel = "select * from asptblmenuname a where  a.menuname='" + MENUNAME + "' and a.active='" + ACTIVE + "' and a.parentmenuid='" + PARENTMENUID + "'   ";
            DataSet ds = Utility.ExecuteSelectQuery(sel, "asptblmenuname");
            DataTable dt = ds.Tables["asptblmenuname"];
            return dt;
        }
        //public MenuName(string MENUNAME, string ACTIVE, int PARENTMENUID,string createby,string modifyon,string ipaddress,int menunameid)
        //{
        //    this.MENUNAMEID = menunameid;
        //    this.MENUNAME = MENUNAME;
        //    this.ACTIVE = ACTIVE;
        //    this.PARENTMENUID = PARENTMENUID;           
        //    this.CREATEBY = createby;
        //    this.MODIFYON = modifyon;
        //    this.IPADDRESS = ipaddress;
        //    string up = "UPDATE  ASPTBLMENUNAME A SET   A.MENUNAME='" + MENUNAME + "' , A.ACTIVE='" + ACTIVE + "' , A.PARENTMENUID=" + PARENTMENUID + ",A.CREATEBY='"+CREATEBY+"',A.MODIFYON='"+MODIFYON+"',A.IPADDRESS='"+IPADDRESS+"' WHERE A.MENUNAMEID=" + MENUNAMEID;
        //    Utility.ExecuteNonQuery(up);

        //}

        public MenuName(string MENUNAME, string ACTIVE, Int64 PARENTMENUID, string createon, string createby, string modifyon, string ipaddress)
        {
            this.MENUNAME = MENUNAME;
            this.ACTIVE = ACTIVE;
            this.PARENTMENUID = PARENTMENUID;
            this.CREATEON = createon;
            this.CREATEBY = createby;
            this.MODIFYON = modifyon;
            this.IPADDRESS = ipaddress;
            string ins = "insert into asptblmenuname a (a.menuname,a.active,a.parentmenuid,a.createon,a.createby,a.modifyon,a.ipaddress) values('" + MENUNAME + "','" + ACTIVE + "', " + PARENTMENUID + ",'" + CREATEON + "','" + CREATEBY + "','" + MODIFYON + "','" + IPADDRESS + "')";
            Utility.ExecuteNonQuery(ins);
        }

        public MenuName()
        {
        }

        public MenuName(string mENUNAME, string aCTIVE, Int64 pARENTMENUID, string cREATEBY, string mODIFYON, string iPADDRESS, Int64 mENUNAMEID)
        {
            MENUNAME = mENUNAME;
            ACTIVE = aCTIVE;
            PARENTMENUID = pARENTMENUID;
            CREATEBY = cREATEBY;
            MODIFYON = mODIFYON;
            IPADDRESS = iPADDRESS;
            MENUNAMEID = mENUNAMEID;
            string up = "update  asptblmenuname a set   a.menuname='" + MENUNAME + "' , a.active='" + ACTIVE + "' , a.parentmenuid=" + PARENTMENUID + ",a.createby='" + CREATEBY + "',a.modifyon='" + MODIFYON + "',a.ipaddress='" + IPADDRESS + "' where a.menunameid=" + MENUNAMEID;
            Utility.ExecuteNonQuery(up);
        }

        public DataTable select()
        {
            string sel = " select a.menunameid,a.menuname,a.parentmenuid,a.active from asptblmenuname a   order by a.menunameid desc ";
            DataSet ds = Utility.ExecuteSelectQuery(sel, "asptblmenuname");
            DataTable dt = ds.Tables["asptblmenuname"];
            return dt;
        }
        internal DataTable menuname()
        {


            string sel = "select a.menunameid,a.menuname  from asptblmenuname a order by 1";
            DataSet ds = Utility.ExecuteSelectQuery(sel, "asptblmenuname");
            DataTable dt1 = ds.Tables["asptblmenuname"];
            return dt1;
        }
        internal DataTable menuname(Int64 s)
        {


            string sel = "select a.parentmenuid,a.menuname  from asptblmenuname a where a.menunameid=" + s;
            DataSet ds = Utility.ExecuteSelectQuery(sel, "asptblmenuname");
            DataTable dt1 = ds.Tables["asptblmenuname"];
            return dt1;
        }
    }
}
