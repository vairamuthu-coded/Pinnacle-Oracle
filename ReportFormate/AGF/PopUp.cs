using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Pinnacle.ReportFormate.AGF
{
    public partial class PopUp : Form
    {
        OpenFileDialog open = new OpenFileDialog();
        byte[] bytes;
        public PopUp()
        {
            InitializeComponent();
            string sel0 = "SELECT GARMENTIMAGE   FROM ASPTBLBUYSAM A JOIN GTCOMPMAST B ON A.COMPCODE=B.GTCOMPMASTID join asptblBRANDMAS C ON C.ASPTBLBRANDMASID=A.BRAND  JOIN ASPTBLSEASONMAS E ON E.ASPTBLSEASONMASID=A.SEASON JOIN ASPTBLSAMDEPTMAS F ON F.ASPTBLSAMDEPTMASID=A.DEPARTMENT  JOIN ASPTBLGGMAS K ON K.ASPTBLGGMASID=A.GAUGE    JOIN ASPTBLCURMAS N ON N.ASPTBLCURMASID=A.CURRENCYNAME JOIN ASPTBLSAMCATMAS O ON O.ASPTBLSAMCATMASID=A.CATEGORY  JOIN ASPTBLORDPACKMAS  P ON P.ASPTBLORDPACKMASID=A.ORDERPACKTYPE join ASPTBLSAMTYPEMAS Q ON Q.ASPTBLSAMTYPEMASID=A.SAMPLETYPE where A.AGFSAMPLE=" + Convert.ToInt64("0" + Class.Users.Paramid);
            DataSet ds0 = Utility.ExecuteSelectQuery(sel0, "ASPTBLBUYSAM");
            DataTable dt0 = ds0.Tables["ASPTBLBUYSAM"];
            if (dt0 != null)
            {
               
                bytes = (byte[])dt0.Rows[0]["GARMENTIMAGE"];
                Image img = Models.Device.ByteArrayToImage(bytes);
                picturegarmentimage.Image = img;

            }
        }

        private void PopUp_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Hide();
        }
    }
}
