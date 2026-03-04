using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pinnacle.Models
{
    public  class ItemPermission: CommonClass
    {
        Int64 asptblmenperid;
        Int64 asptblmenper1id;
        string docid;
        Int64 compcode;
        Int64 asptblcanitemmasid;
        string itemcode;
        Int64 itemname1;
        Int64 category;
        DateTime fromdate;
        DateTime fromtime;
        DateTime todate;
        DateTime totime;
        string finyear;
        DateTime systemtime;
       
        
      public Int64 Asptblmenperid { get { return asptblmenperid; }set { asptblmenperid = value; } }
        public Int64 Asptblmenper1id { get { return asptblmenper1id; } set { asptblmenper1id = value; } }
        public string Docid { get { return docid; } set { docid = value; } }
        public Int64 Compcode { get { return compcode; } set { compcode = value; } }
        public Int64 Asptblcanitemmasid { get { return asptblcanitemmasid; } set { asptblcanitemmasid = value; } }
        public string Itemcode { get { return itemcode; } set { itemcode = value; } }
        public Int64 Itemname1 { get { return itemname1; } set { itemname1 = value; } }
        public Int64 Category { get { return category; } set { category = value; } }
        public DateTime Fromdate { get { return fromdate; } set { fromdate = value; } }
        public DateTime Fromtime { get { return fromtime; } set { fromtime = value; } }
        public DateTime Todate { get { return todate; } set { todate = value; } }
        public DateTime Totime { get { return totime; } set { totime = value; } }     
        public string Finyear { get { return finyear; } set { finyear = value; } }
        public DateTime Systemtime { get { return systemtime; } set { systemtime = value; } }

        public override Task DeleteCommond(long id)
        {
            throw new NotImplementedException();
        }

        public override Task<DataTable> GridLoad()
        {
            throw new NotImplementedException();
        }

        public override Task<DataTable> GridLoad(long id)
        {
            throw new NotImplementedException();
        }

        public override Task InsertCommond()
        {
            throw new NotImplementedException();
        }

        public override Task<DataTable> SelectCommond()
        {
            throw new NotImplementedException();
        }

        public override Task<DataTable> SelectCommond(long id)
        {
            throw new NotImplementedException();
        }

        public override Task UpdateCommond()
        {
            throw new NotImplementedException();
        }
    }
    public class ItemPermissionDetails: ItemPermission
    {
        Int64 asptblmenperdetid;
        string notes;

        public Int64 Asptblmenperdetid { get { return asptblmenperdetid; } set { asptblmenperdetid = value; } }       
        public string Notes { get { return notes; } set { notes = value; } }

        public override Task DeleteCommond(long id)
        {
            throw new NotImplementedException();
        }

        public override Task<DataTable> GridLoad()
        {
            throw new NotImplementedException();
        }

        public override Task<DataTable> GridLoad(long id)
        {
            throw new NotImplementedException();
        }

        public override Task InsertCommond()
        {
            throw new NotImplementedException();
        }

        public override Task<DataTable> SelectCommond()
        {
            throw new NotImplementedException();
        }

        public override Task<DataTable> SelectCommond(long id)
        {
            throw new NotImplementedException();
        }

        public override Task UpdateCommond()
        {
            throw new NotImplementedException();
        }
    }
}
