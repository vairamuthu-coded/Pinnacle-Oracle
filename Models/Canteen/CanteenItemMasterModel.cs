using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pinnacle.Models.Canteen
{
    class CanteenItemMasterModel : CommonClass
    {
        private Int64 asptblcantokenid;
        private Int64 compcode;
        private Int64 finyear;
        private Int64 empid;
        private Int64 empname;
        private Int64 idcardno;
        private string itemcode;
        private Int64 itemname1;
        private Int64 itemcost;
        private Int64 itemqty;
        private Int64 noofdays;
        private Int64 totalamount;
        private string tokenno;
        private string tokennocancel;
        private string employeetype;
        private string tokenoption;
        private decimal employeecost;
        private decimal contractorcost;
        private decimal specialcost;
        private DateTime tokendate;
        private string tokentime;
        private DateTime systemdate;


        public Int64 Asptblcantokenid { get { return asptblcantokenid; } set { asptblcantokenid = value; } }
        public Int64 Compcode { get { return compcode; } set { compcode = value; } }
        public Int64 Finyear { get { return finyear; } set { finyear = value; } }
        public Int64 Empid { get { return empid; } set { empid = value; } }
        public Int64 Empname { get { return empname; } set { empname = value; } }
        public Int64 Idcardno { get { return idcardno; } set { idcardno = value; } }
        public string Itemcode { get { return itemcode; } set { itemcode = value; } }
        public Int64 Itemname1 { get { return itemname1; } set { itemname1 = value; } }
        public Int64 Itemcost { get { return itemcost; } set { itemcost = value; } }
        public Int64 Itemqty { get { return itemqty; } set { itemqty = value; } }
        public Int64 Noofdays { get { return noofdays; } set { noofdays = value; } }
        public long Totalamount { get { return totalamount; } set { totalamount = value; } }
        public string Tokenno { get { return tokenno; } set { tokenno = value; } }
        public string Tokennocancel { get { return tokennocancel; } set { tokennocancel = value; } }
        public string Employeetype { get { return employeetype; } set { employeetype = value; } }
        public string Tokenoption { get { return tokenoption; } set { tokenoption = value; } }
        public decimal Employeecost { get { return employeecost; } set { employeecost = value; } }
        public decimal Contractorcost { get { return contractorcost; } set { contractorcost = value; } }
        public decimal Specialcost { get { return specialcost; } set { specialcost = value; } }
        public DateTime Tokendate { get { return tokendate; } set { tokendate = value; } }
        public string Tokentime { get { return tokentime; } set { tokentime = value; } }
        public DateTime Systemdate { get { return systemdate; } set { systemdate = value; } }



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
