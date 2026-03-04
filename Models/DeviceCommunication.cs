using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Windows.Forms;

namespace Pinnacle.Models
{
    class DeviceCommunication
    {
        public Int32 ATTLOGID { get; set; }
        public String MACHINENUMBER { get; set; }
        public String IPADDRESS { get; set; }
        public String ENROLLNO { get; set; }
        public String DATETIMERECORD { get; set; }
        public String JSONDATA { get; set; }
     
        public IEnumerable <DeviceCommunication> UserDetails { get; set; }
       
        public DeviceCommunication(int ATTLOGID, string MACHINENUMBER, string IPADDRESS, string ENROLLNO, string DATETIMERECORD)
        {
            this.ATTLOGID = ATTLOGID;
            this.MACHINENUMBER = MACHINENUMBER;
            this.IPADDRESS = IPADDRESS;
            this.ENROLLNO = ENROLLNO;
            this.DATETIMERECORD = DATETIMERECORD;
    
        }

        public DeviceCommunication()
        {
        }
        public void GetAllCustomers(List<DeviceCommunication> UserDetails1)
        {
           
           
        }


        public void SaveUsingOracleBulkCopy(string destinationTable, DataTable dt)
        {
            try
            {
                //using (OracleConnection con = Utility.Connect())

                using (OracleBulkCopy bulkCopy = new OracleBulkCopy(Utility.Connect(), OracleBulkCopyOptions.Default))
                {
                    bulkCopy.DestinationTableName = destinationTable;
                    bulkCopy.BulkCopyTimeout = 0; // unlimited (optional)

                    // Auto map columns
                    foreach (DataColumn col in dt.Columns)
                    {
                        bulkCopy.ColumnMappings.Add(col.ColumnName, col.ColumnName);
                    }

                    bulkCopy.WriteToServer(dt);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Bulk Copy Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       
        
        public void GetAllCustomers(string strserialize)
        {
            
        }
    }
}
