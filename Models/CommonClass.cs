using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pinnacle
{

    public class Employee
    {
        public void EmployeeDetails()
        {
            
        }
        public void EMS() {
           
        }
    }
    public class Trainee : BaseEmployee
    {
        public override void Salary()
        {
           
        }

        public override void Work()
        {
            
        }
    }
    public class TL : BaseEmployee
    {
        public override void Salary()
        {
            
        }

        public override void Work()
        {
            
        }
    }
    public class Manager : BaseEmployee, IFireoption
    {
        public void FireEmployee()
        {
            
        }

        public override void Salary()
        {
            throw new System.NotImplementedException();
        }

        public override void Work()
        {
            throw new System.NotImplementedException();
        }
    }



    public abstract class BaseEmployee
    {
        public void EmployeeDetails()
        {
            
        }
        public void EMS()
        {
            
        }
        public abstract void Work();
        public abstract void Salary();

    }

    public interface IFireoption
    {
        public void FireEmployee();
    }
    //work - same function name but different implementation
    //salary - same funtion name but differnt implementation

    public abstract class CommonClass
    {

        private string active;
        private Int64 username;
        private string createdby;
        private string createdon;
        private string modifiedon;
        private string modifiedby;

        private string ipaddress { get; set; }
        private Int64 compcode1 { get; set; }
        public string Active { get { return active; } set { active = value; } }
        public Int64 Username { get { return username; } set { username = value; } }
        public Int64 Compcode1 { get { return compcode1; } set { compcode1 = value; } }
        public string Modified { get { return modifiedby; } set { modifiedby = value; } }
        public string Modifiedon { get { return modifiedon; } set { modifiedon = value; } }
        public string Createdon { get { return createdon; } set { createdon = value; } }
        public string Createdby { get { return createdby; } set { createdby = value; } }
        public string Ipaddress { get { return ipaddress; } set { ipaddress = value; } }

        public abstract Task<DataTable> SelectCommond();
        public abstract Task<DataTable> SelectCommond(Int64 id);
        public abstract Task InsertCommond();
        public abstract Task UpdateCommond();
        public abstract Task<DataTable> GridLoad();
        public abstract Task<DataTable> GridLoad(Int64 id);
        public abstract Task DeleteCommond(Int64 id);


    }
   
}
