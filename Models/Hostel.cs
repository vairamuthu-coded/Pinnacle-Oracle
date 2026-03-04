using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace Pinnacle.Models
{
    class Hostel
    {
        public DataTable HostelName()
        {
            string sel3 = "SELECT  '' AS HOSTELNAME FROM DUAL  UNION ALL SELECT  'MANUAL PASS' AS HOSTELNAME FROM DUAL  UNION ALL SELECT DISTINCT  A.HOSTELNAME FROM ASPTBLHOSTELGATEPASS A where A.HOSTELNAME not in 'AGF'  and A.HOSTELNAME not in 'AGFM'     and A.HOSTELNAME not in 'AGFC'  and A.HOSTELNAME not in 'AGFP'  AND   A.HOSTELNAME not in 'AGFMGII'  AND  A.HOSTELNAME not in 'FLF'  and A.HOSTELNAME not in 'FLFD'   and A.HOSTELNAME not in 'MENS HOTEL' and A.HOSTELNAME not in 'BOYS HOSTEL' ";
            DataSet ds3 = Utility.ExecuteSelectQuery(sel3, "ASPTBLHOSTELGATEPASS");
           DataTable dt3 = ds3.Tables["ASPTBLHOSTELGATEPASS"];
            return dt3;
        }
        public DataTable HostelCompcode()
        {
            string sel3 = "SELECT  '' AS  COMPCODE FROM DUAL   UNION ALL SELECT DISTINCT  A.HOSTELNAME AS COMPCODE FROM ASPTBLHOSTELGATEPASS A  where  A.HOSTELNAME  not in 'WOMENS HOSTEL' and A.HOSTELNAME not in 'WORKING GENTS HOSTEL'   and A.HOSTELNAME not in 'MENS HOTEL'    and A.HOSTELNAME not in 'BOYS HOSTEL'    and A.HOSTELNAME not in 'GENTS STAFF HOSTEL'";
            DataSet ds3 = Utility.ExecuteSelectQuery(sel3, "ASPTBLHOSTELGATEPASS");
            DataTable dt3 = ds3.Tables["ASPTBLHOSTELGATEPASS"];
            return dt3;
        }
    }

}
