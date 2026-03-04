using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace Pinnacle.ReportFormate
{
    public partial class SampleCollectionReport : Form, ToolStripAccess
    {
        private static SampleCollectionReport _instance;
        public static SampleCollectionReport Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new SampleCollectionReport();
                GlobalVariables.CurrentForm = _instance; return _instance;
            }
        }
        public SampleCollectionReport()
        {
            InitializeComponent();
       
        }
        Report.Sample.StockReport rdstock = new Report.Sample.StockReport();
        Report.SampleCollectionReport rd = new Report.SampleCollectionReport();
        Report.Samplependingreport pen = new Report.Samplependingreport();
        Report.Sample.SampleCorrection sc = new Report.Sample.SampleCorrection ();
        Report.Sample.Brand_Category brandcate = new Report.Sample.Brand_Category();
        Report.SampleCollectionReport3 rchart = new Report.SampleCollectionReport3();
        Report.Sample.Brand_Category brand = new Report.Sample.Brand_Category();
        string path = "";
        string folderLocation = "d:\\SampleCollections-Download\\";
        private void SampleTypeLoad()
        {
            string sel0 = " SELECT 0 AS ASPTBLSAMTYPEMASID, 'ALL' AS SAMPLETYPE  FROM DUAL UNION ALL SELECT A.ASPTBLSAMTYPEMASID,A.SAMPLETYPE FROM ASPTBLSAMTYPEMAS A WHERE A.ACTIVE='T'";
            DataSet ds0 = Utility.ExecuteSelectQuery(sel0, "ASPTBLSAMTYPEMAS");
            DataTable dt0 = ds0.Tables["ASPTBLSAMTYPEMAS"];
            combosampletype.DataSource = dt0;
            combosampletype.DisplayMember = "SAMPLETYPE";
            combosampletype.ValueMember = "ASPTBLSAMTYPEMASID";
        }
        public void StyleLoad()
        {
            //string sel = "SELECT distinct A.asptblbuysamid,A.STYLENAME    FROM  asptblbuysam A    WHERE A.ACTIVE='T' AND A.BRAND='"+combobrand.Text+"' ";
            //DataSet ds = Utility.ExecuteSelectQuery(sel, "asptblbuysam");
            //DataTable dt = ds.Tables["asptblbuysam"];
            //combostyle.DataSource = dt;
            //combostyle.DisplayMember = "STYLENAME";
            //combostyle.ValueMember = "asptblbuysamid";

        }
        private void stockreport()
        {
            if (combocompcode.Text == "ALL") {
                // string sel0 = "SELECT A.ASPTBLBUYSAMID,A.DATE1,B.COMPCODE,b.compname,C.BRAND,A.AGFSAMPLE,E.SEASON || ' /' ||  A.MFYEAR || '' AS SEASON,F.DEPARTMENT || '(' ||  O.CATEGORY || ')' AS DEPARTMENT , O.CATEGORY,A.STYLENAME,A.SUBSTYLE,A.SAMPLETYPE, A.FABRIC,A.FABRICCONTENT,A.COUNTS, K.GG AS GAUGE,A.GSM, A.COLORNAME,P.ORDERPACKTYPE,A.SIZENAME,N.CURRENCYNAME,A.REMARKS, A.ACTIVE,Q.SAMPLETYPE AS RISK1,A.RISK2,A.RISK3,A.RISK4,A.RISK5,A.FABRICCOMPLIANT,A.REMARKS,A.MFYEAR    FROM ASPTBLBUYSAM A JOIN GTCOMPMAST B ON A.COMPCODE=B.GTCOMPMASTID  join asptblBRANDMAS C ON C.ASPTBLBRANDMASID=A.BRAND  JOIN ASPTBLSEASONMAS E ON E.ASPTBLSEASONMASID=A.SEASON JOIN ASPTBLSAMDEPTMAS F ON F.ASPTBLSAMDEPTMASID=A.DEPARTMENT     JOIN ASPTBLGGMAS K ON K.ASPTBLGGMASID=A.GAUGE      JOIN ASPTBLCURMAS N ON N.ASPTBLCURMASID=A.CURRENCYNAME  JOIN ASPTBLSAMCATMAS O ON O.ASPTBLSAMCATMASID=A.CATEGORY      JOIN ASPTBLORDPACKMAS  P ON P.ASPTBLORDPACKMASID=A.ORDERPACKTYPE JOIN ASPTBLSAMTYPEMAS Q ON Q.ASPTBLSAMTYPEMASID=A.SAMPLETYPE     where A.INWARD='T'    order by a.ASPTBLBUYSAMID desc ";
                string sel0 = "select   A.AGFSAMPLE,'STOCK ENTRY' AS COMPCODE, B.BRAND,D.SEASON, a.STYLENAME,E.DEPARTMENT,A.FABRIC || ' -- ' || A.FABRICCONTENT AS FABRIC, A.COLORNAME,A.SIZENAME  from    asptblbuysam A  JOIN ASPTBLBRANDMAS b ON b.ASPTBLBRANDMASID=A.BRAND   JOIN ASPTBLSEASONMAS d ON d.ASPTBLSEASONMASID=A.SEASON  JOIN ASPTBLSAMDEPTMAS e ON e.ASPTBLSAMDEPTMASID=a.DEPARTMENT   JOIN ASPTBLGGMAS f ON f.ASPTBLGGMASID=a.GAUGE   JOIN ASPTBLCURMAS g ON g.ASPTBLCURMASID=a.CURRENCYNAME    JOIN ASPTBLSAMCATMAS h ON h.ASPTBLSAMCATMASID=a.CATEGORY join ASPTBLORDPACKMAS i on i.ASPTBLORDPACKMASID=a.ORDERPACKTYPE join asptblsamtypemas j on j.ASPTBLSAMTYPEMASID=a.SAMPLETYPE  join gtcompmast k on k.GTCOMPMASTID=a.COMPCODE  WHERE   A.INWARD='T'   ORDER BY A.AGFSAMPLE DESC";
                DataSet ds0 = Utility.ExecuteSelectQuery(sel0, "ASPTBLBUYSAM");
                DataTable dt0 = ds0.Tables["ASPTBLBUYSAM"];
             
                rdstock.SetDataSource(dt0);
                crystalReportViewer1.ReportSource = null;
                crystalReportViewer1.ReportSource = rdstock;
                crystalReportViewer1.Refresh();
                lblcount.Refresh();
                lblcount.Text = "Total Stock Qty " + dt0.Rows.Count;
            }
            else
            {
                //string sel0 = "select DISTINCT  A.AGFSAMPLE,C.RACK,D.BIN,A.FABRIC, A.COLORNAME,A.SIZENAME,A.STYLENAME,E.BRAND  from asptblbuysaminw a  join  asptblbuysam b on a.ASPTBLBUYSAMID=b.ASPTBLBUYSAMID AND A.AGFSAMPLE=B.AGFSAMPLE AND A.BRAND=B.BRAND  AND A.SEASON=B.SEASON  AND A.DEPARTMENT=B.DEPARTMENT  AND A.CATEGORY=B.CATEGORY   AND A.STYLENAME=B.STYLENAME   AND A.SIZENAME=B.SIZENAME    AND A.COUNTS=B.COUNTS   AND A.COLORNAME=B.COLORNAME   AND A.FABRIC=B.FABRIC AND A.GSM=B.GSM JOIN ASPTBLRACKMAS C ON C.ASPTBLRACKMASID=A.RACK JOIN ASPTBLBINMAS D ON D.ASPTBLBINMASID=A.BIN JOIN ASPTBLBRANDMAS E ON E.ASPTBLBRANDMASID=A.BRAND  WHERE   A.OUTWARD='F'      ORDER BY A.AGFSAMPLE";
                //DataSet ds0 = Utility.ExecuteSelectQuery(sel0, "ASPTBLBUYSAM");
                // DataTable dt0 = ds0.Tables["ASPTBLBUYSAM"];
                //rd.SetDataSource(dt0);
                //crystalReportViewer1.ReportSource = null;
                //crystalReportViewer1.ReportSource = rd;
                //crystalReportViewer1.Refresh();
                //lblcount.Refresh();
                //lblcount.Text = "Total Stock Qty " + dt0.Rows.Count;
            }
        }
        private void Brandreport()
        {

           
            if (combobrand.Text != "" && combodept.Text != "")
            {
                string sel0 = "SELECT   B.COMPCODE,C.BRAND,   F.DEPARTMENT,O.CATEGORY      FROM ASPTBLBUYSAM A JOIN GTCOMPMAST B ON A.COMPCODE=B.GTCOMPMASTID join asptblBRANDMAS C ON C.ASPTBLBRANDMASID=A.BRAND  JOIN ASPTBLSEASONMAS E ON E.ASPTBLSEASONMASID=A.SEASON JOIN ASPTBLSAMDEPTMAS F ON F.ASPTBLSAMDEPTMASID=A.DEPARTMENT   JOIN ASPTBLGGMAS K ON K.ASPTBLGGMASID=A.GAUGE      JOIN ASPTBLCURMAS N ON N.ASPTBLCURMASID=A.CURRENCYNAME JOIN ASPTBLSAMCATMAS O ON O.ASPTBLSAMCATMASID=A.CATEGORY  JOIN ASPTBLORDPACKMAS  P ON P.ASPTBLORDPACKMASID=A.ORDERPACKTYPE   JOIN ASPTBLBUYSAMinw Q ON Q.AGFSAMPLE=A.AGFSAMPLE  where   Q.OUTWARD='T'  AND  f.department='" + combodept.Text + "'  AND  c.BRAND='" + combobrand.Text + "'     ORDER by 2,3";
                DataSet ds0 = Utility.ExecuteSelectQuery(sel0, "ASPTBLBUYSAM");
                DataTable dt0 = ds0.Tables["ASPTBLBUYSAM"];
                brand.SetDataSource(dt0);
                crystalReportViewer6.ReportSource = null;
                crystalReportViewer6.ReportSource = brand;
                crystalReportViewer6.Refresh();
                lblcount.Refresh();
                lblcount.Text = "Total Brand/Category Qty " + dt0.Rows.Count;

            }
            else
            {
                MessageBox.Show("pls select Brand and Category Field");
                crystalReportViewer6.ReportSource = null;
            }
        }
        private void BrandCategorydepartreport()
        {
            string sel0 = "SELECT   B.COMPCODE,C.BRAND,   F.DEPARTMENT AS CATEGORY     FROM ASPTBLBUYSAM A JOIN GTCOMPMAST B ON A.COMPCODE=B.GTCOMPMASTID join asptblBRANDMAS C ON C.ASPTBLBRANDMASID=A.BRAND  JOIN ASPTBLSEASONMAS E ON E.ASPTBLSEASONMASID=A.SEASON JOIN ASPTBLSAMDEPTMAS F ON F.ASPTBLSAMDEPTMASID=A.DEPARTMENT   JOIN ASPTBLGGMAS K ON K.ASPTBLGGMASID=A.GAUGE      JOIN ASPTBLCURMAS N ON N.ASPTBLCURMASID=A.CURRENCYNAME JOIN ASPTBLSAMCATMAS O ON O.ASPTBLSAMCATMASID=A.CATEGORY  JOIN ASPTBLORDPACKMAS  P ON P.ASPTBLORDPACKMASID=A.ORDERPACKTYPE   JOIN ASPTBLBUYSAMinw Q ON Q.AGFSAMPLE=A.AGFSAMPLE  where   Q.OUTWARD='T'   ORDER by 2,3";
            DataSet ds0 = Utility.ExecuteSelectQuery(sel0, "ASPTBLBUYSAM");
            DataTable dt0 = ds0.Tables["ASPTBLBUYSAM"];
            brand.SetDataSource(dt0);
            crystalReportViewer6.ReportSource = null;
            crystalReportViewer6.ReportSource = brand;
            crystalReportViewer6.Refresh();
            lblcount.Refresh();
            lblcount.Text = "CompCode : " + Class.Users.HCompcode + ",  Total Brand Qty " + dt0.Rows.Count;

        }
        private void outreport()
        {
            // string sel0 = "select DISTINCT  A.AGFSAMPLE, 'OUT WARD ENTRY' AS COMPCODE,A.FABRIC, A.COLORNAME,A.SIZENAME,A.STYLENAME,E.BRAND from ASPTBLBUYSAMOUT a  LEFT  join  asptblbuysam b on a.ASPTBLBUYSAMID = b.ASPTBLBUYSAMID AND A.AGFSAMPLE = B.AGFSAMPLE AND A.BRAND = B.BRAND     AND A.SEASON = B.SEASON  AND A.DEPARTMENT = B.DEPARTMENT  AND A.CATEGORY = B.CATEGORY   AND A.STYLENAME = B.STYLENAME    AND A.SIZENAME = B.SIZENAME        AND A.COUNTS = B.COUNTS   AND A.COLORNAME = B.COLORNAME   AND A.FABRIC = B.FABRIC AND A.GSM = B.GSM         JOIN ASPTBLRACKMAS C ON C.ASPTBLRACKMASID = A.RACK JOIN ASPTBLBINMAS D ON D.ASPTBLBINMASID = A.BIN       JOIN ASPTBLBRANDMAS E ON E.ASPTBLBRANDMASID = A.BRAND       AND E.ASPTBLBRANDMASID = B.BRAND   WHERE A.OUTWARD = 'T'      ORDER BY A.AGFSAMPLE DESC";
            string sel0 = "select   A.AGFSAMPLE,'OUTWARD ENTRY' AS COMPCODE, B.BRAND,D.SEASON, a.STYLENAME,e.DEPARTMENT,A.FABRIC || ' -- ' || A.FABRICCONTENT AS FABRIC, A.COLORNAME,A.SIZENAME  from    asptblbuysam A  JOIN ASPTBLBRANDMAS b ON b.ASPTBLBRANDMASID=A.BRAND   JOIN ASPTBLSEASONMAS d ON d.ASPTBLSEASONMASID=A.SEASON  JOIN ASPTBLSAMDEPTMAS e ON e.ASPTBLSAMDEPTMASID=a.DEPARTMENT   JOIN ASPTBLGGMAS f ON f.ASPTBLGGMASID=a.GAUGE   JOIN ASPTBLCURMAS g ON g.ASPTBLCURMASID=a.CURRENCYNAME    JOIN ASPTBLSAMCATMAS h ON h.ASPTBLSAMCATMASID=a.CATEGORY join ASPTBLORDPACKMAS i on i.ASPTBLORDPACKMASID=a.ORDERPACKTYPE join asptblsamtypemas j on j.ASPTBLSAMTYPEMASID=a.SAMPLETYPE  join gtcompmast k on k.GTCOMPMASTID=a.COMPCODE  WHERE   A.INWARD is null   ORDER BY A.AGFSAMPLE DESC";

            DataSet ds0 = Utility.ExecuteSelectQuery(sel0, "ASPTBLBUYSAM");
            DataTable dt0 = ds0.Tables["ASPTBLBUYSAM"];
            rdstock.SetDataSource(dt0);
            crystalReportViewer2.ReportSource = null;
            crystalReportViewer2.ReportSource = rdstock;
            crystalReportViewer2.Refresh(); lblcount.Refresh();
            lblcount.Text = "CompCode : " + Class.Users.HCompcode + ",  Total  Outward  Qty " + dt0.Rows.Count;

        }
        private void inwardreport()
        {
            //  string sel0 = "select  DISTINCT A.AGFSAMPLE ,'INWARD ENTRY' AS COMPCODE, B.DATE1,F.BRAND,G.SEASON ,K.CATEGORY,A.STYLENAME,A.FABRIC ,A.FABRICCONTENT,A.COUNTS, I.GG AS GAUGE,A.GSM, A.COLORNAME,B.ORDERPACKTYPE,A.SIZENAME,J.CURRENCYNAME, B.REMARKS,B.ACTIVE,B.RISK1,B.RISK2,B.RISK3,B.RISK4,B.RISK5,B.FABRICCOMPLIANT,B.REMARKS,B.MFYEAR  from asptblbuysaminw a  LEFT join  asptblbuysam b on a.ASPTBLBUYSAMID=b.ASPTBLBUYSAMID AND A.AGFSAMPLE=B.AGFSAMPLE  AND A.BRAND=B.BRAND   AND A.SEASON=B.SEASON AND A.DEPARTMENT=B.DEPARTMENT  AND A.CATEGORY=B.CATEGORY   AND A.STYLENAME=B.STYLENAME   AND A.SIZENAME=B.SIZENAME    AND A.COUNTS=B.COUNTS AND A.COLORNAME=B.COLORNAME   AND A.FABRIC=B.FABRIC AND A.GSM=B.GSM   JOIN ASPTBLRACKMAS C ON C.ASPTBLRACKMASID=A.RACK  JOIN ASPTBLBINMAS D ON D.ASPTBLBINMASID=A.BIN   JOIN ASPTBLBRANDMAS E ON E.ASPTBLBRANDMASID=A.BRAND AND E.ASPTBLBRANDMASID=B.BRAND    JOIN ASPTBLBRANDMAS F ON F.ASPTBLBRANDMASID = B.BRAND  AND F.ASPTBLBRANDMASID = A.BRAND    JOIN ASPTBLSEASONMAS G ON G.ASPTBLSEASONMASID=A.SEASON AND G.ASPTBLSEASONMASID=B.SEASON     JOIN ASPTBLSAMDEPTMAS H ON H.ASPTBLSAMDEPTMASID=B.DEPARTMENT     AND H.ASPTBLSAMDEPTMASID=A.DEPARTMENT   JOIN ASPTBLGGMAS I ON I.ASPTBLGGMASID=B.GAUGE   AND I.ASPTBLGGMASID=A.GAUGE    JOIN ASPTBLCURMAS J ON J.ASPTBLCURMASID=B.CURRENCYNAME     JOIN ASPTBLSAMCATMAS K ON K.ASPTBLSAMCATMASID=B.CATEGORY  AND K.ASPTBLSAMCATMASID=A.CATEGORY    WHERE   A.OUTWARD='F' AND A.INWARD='T'      ORDER BY A.AGFSAMPLE DESC";
            string sel0 = "select   A.AGFSAMPLE ,'INWARD ENTRY' AS COMPCODE, a.DATE1,b.BRAND,d.SEASON , e.DEPARTMENT, h.CATEGORY,a.STYLENAME,A.FABRIC , A.FABRICCONTENT,A.COUNTS, f.GG AS GAUGE,A.GSM, A.COLORNAME,i.ORDERPACKTYPE,A.SIZENAME,g.CURRENCYNAME, a.REMARKS,a.ACTIVE,a.RISK1,a.RISK2,a.RISK3,a.RISK4,a.RISK5,a.FABRICCOMPLIANT,a.MFYEAR   from    asptblbuysam A  JOIN ASPTBLBRANDMAS b ON b.ASPTBLBRANDMASID=A.BRAND   JOIN ASPTBLSEASONMAS d ON d.ASPTBLSEASONMASID=A.SEASON  JOIN ASPTBLSAMDEPTMAS e ON e.ASPTBLSAMDEPTMASID=a.DEPARTMENT   JOIN ASPTBLGGMAS f ON f.ASPTBLGGMASID=a.GAUGE   JOIN ASPTBLCURMAS g ON g.ASPTBLCURMASID=a.CURRENCYNAME    JOIN ASPTBLSAMCATMAS h ON h.ASPTBLSAMCATMASID=a.CATEGORY join ASPTBLORDPACKMAS i on i.ASPTBLORDPACKMASID=a.ORDERPACKTYPE WHERE   A.INWARD='T'   ORDER BY A.AGFSAMPLE DESC";
            DataSet ds0 = Utility.ExecuteSelectQuery(sel0, "ASPTBLBUYSAM");
            DataTable dt0 = ds0.Tables["ASPTBLBUYSAM"];
            if (dt0.Rows.Count > 0)
            {
               
                rd.SetDataSource(dt0);
                crystalReportViewer3.ReportSource = null;
                crystalReportViewer3.ReportSource = rd;
                crystalReportViewer3.Refresh(); lblcount.Refresh();
                lblcount.Text = "Total  Inward  Qty " + dt0.Rows.Count;
            }
            else
            {
                crystalReportViewer8.ReportSource = null;
            }
        }

        private void reportchart()
        {
            if (combocompcode.Text == "ALL")
            {
                string sel0 = "select  A.AGFSAMPLE,a.MFYEAR,  k.COMPCODE,k.compname,A.PCS    from    asptblbuysam A  JOIN ASPTBLBRANDMAS b ON b.ASPTBLBRANDMASID=A.BRAND   JOIN ASPTBLSEASONMAS d ON d.ASPTBLSEASONMASID=A.SEASON  JOIN ASPTBLSAMDEPTMAS e ON e.ASPTBLSAMDEPTMASID=a.DEPARTMENT   JOIN ASPTBLGGMAS f ON f.ASPTBLGGMASID=a.GAUGE   JOIN ASPTBLCURMAS g ON g.ASPTBLCURMASID=a.CURRENCYNAME    JOIN ASPTBLSAMCATMAS h ON h.ASPTBLSAMCATMASID=a.CATEGORY join ASPTBLORDPACKMAS i on i.ASPTBLORDPACKMASID=a.ORDERPACKTYPE join asptblsamtypemas j on j.ASPTBLSAMTYPEMASID=a.SAMPLETYPE  join gtcompmast k on k.GTCOMPMASTID=a.COMPCODE  WHERE   A.INWARD='T'   ORDER BY A.AGFSAMPLE DESC";
                DataSet ds0 = Utility.ExecuteSelectQuery(sel0, "ASPTBLBUYSAM");
                DataTable dt0 = ds0.Tables["ASPTBLBUYSAM"];
                if (dt0.Rows.Count > 0)
                {
                    rchart.SetDataSource(dt0);
                    crystalReportViewer8.ReportSource = null;
                    crystalReportViewer8.ReportSource = rchart;
                    crystalReportViewer8.Refresh();
                    lblcount.Text = "CompCode : " + Class.Users.HCompcode + ",    Qty " + dt0.Rows.Count;
                }
                else
                {
                    crystalReportViewer8.ReportSource = null;
                }
            }
            else
            {
                string sel0 = "select  A.AGFSAMPLE,a.MFYEAR,  k.COMPCODE,k.compname,A.PCS    from    asptblbuysam A  JOIN ASPTBLBRANDMAS b ON b.ASPTBLBRANDMASID=A.BRAND   JOIN ASPTBLSEASONMAS d ON d.ASPTBLSEASONMASID=A.SEASON  JOIN ASPTBLSAMDEPTMAS e ON e.ASPTBLSAMDEPTMASID=a.DEPARTMENT   JOIN ASPTBLGGMAS f ON f.ASPTBLGGMASID=a.GAUGE   JOIN ASPTBLCURMAS g ON g.ASPTBLCURMASID=a.CURRENCYNAME    JOIN ASPTBLSAMCATMAS h ON h.ASPTBLSAMCATMASID=a.CATEGORY join ASPTBLORDPACKMAS i on i.ASPTBLORDPACKMASID=a.ORDERPACKTYPE join asptblsamtypemas j on j.ASPTBLSAMTYPEMASID=a.SAMPLETYPE  join gtcompmast k on k.GTCOMPMASTID=a.COMPCODE  WHERE   A.INWARD='T'   and  k.compcode='" + combocompcode.Text + "'    ORDER BY A.AGFSAMPLE    ";

               // string sel0 = "SELECT A.MFYEAR,  B.COMPCODE,b.compname,A.PCS   FROM ASPTBLBUYSAM A JOIN GTCOMPMAST B ON A.COMPCODE=B.GTCOMPMASTID join asptblBRANDMAS C ON C.ASPTBLBRANDMASID=A.BRAND  JOIN ASPTBLSEASONMAS E ON E.ASPTBLSEASONMASID=A.SEASON JOIN ASPTBLSAMDEPTMAS F ON F.ASPTBLSAMDEPTMASID=A.DEPARTMENT   JOIN ASPTBLGGMAS K ON K.ASPTBLGGMASID=A.GAUGE      JOIN ASPTBLCURMAS N ON N.ASPTBLCURMASID=A.CURRENCYNAME JOIN ASPTBLSAMCATMAS O ON O.ASPTBLSAMCATMASID=A.CATEGORY  JOIN ASPTBLORDPACKMAS  P ON P.ASPTBLORDPACKMASID=A.ORDERPACKTYPE     where A.INWARD ='T' AND A.OUTWARD ='T' and b.compcode='" + combocompcode.Text + "' ORDER BY 1 ";
                DataSet ds0 = Utility.ExecuteSelectQuery(sel0, "ASPTBLBUYSAM");
                DataTable dt0 = ds0.Tables["ASPTBLBUYSAM"];
                if (dt0.Rows.Count > 0)
                {
                    rchart.SetDataSource(dt0);
                    crystalReportViewer8.ReportSource = null;
                    crystalReportViewer8.ReportSource = rchart;
                    crystalReportViewer8.Refresh();
                    lblcount.Text = "CompCode : " + Class.Users.HCompcode + ",    Qty " + dt0.Rows.Count;
                }
                else
                {
                    crystalReportViewer8.ReportSource = null;
                }

            }
        }

        private void Pendingreport()
        {

            //string sel0 = "SELECT  A.DATE1 as  DATE1,B.COMPCODE,b.compname,C.BRAND,A.AGFSAMPLE,A.INWARD    FROM ASPTBLBUYSAM A JOIN GTCOMPMAST B ON A.COMPCODE=B.GTCOMPMASTID join asptblBRANDMAS C ON C.ASPTBLBRANDMASID=A.BRAND      JOIN ASPTBLSEASONMAS E ON E.ASPTBLSEASONMASID=A.SEASON JOIN ASPTBLSAMDEPTMAS F ON F.ASPTBLSAMDEPTMASID=A.DEPARTMENT        JOIN ASPTBLGGMAS K ON K.ASPTBLGGMASID=A.GAUGE      JOIN ASPTBLCURMAS N ON N.ASPTBLCURMASID=A.CURRENCYNAME            JOIN ASPTBLSAMCATMAS O ON O.ASPTBLSAMCATMASID=A.CATEGORY  JOIN ASPTBLORDPACKMAS  P ON P.ASPTBLORDPACKMASID=A.ORDERPACKTYPE  WHERE  A.INWARD IS NULL AND B.COMPCODE='"+combocompcode.Text+"'  ORDER by a.AGFSAMPLE ASC";
            string sel0 = "select   A.AGFSAMPLE,'Inward Pending Entry' AS COMPCODE, B.BRAND,D.SEASON, a.STYLENAME,E.DEPARTMENT,A.FABRIC || ' -- ' || A.FABRICCONTENT AS FABRIC, A.COLORNAME,A.SIZENAME  from    asptblbuysam A  JOIN ASPTBLBRANDMAS b ON b.ASPTBLBRANDMASID=A.BRAND  JOIN ASPTBLSEASONMAS d ON d.ASPTBLSEASONMASID=A.SEASON  JOIN ASPTBLSAMDEPTMAS e ON e.ASPTBLSAMDEPTMASID=a.DEPARTMENT   JOIN ASPTBLGGMAS f ON f.ASPTBLGGMASID=a.GAUGE   JOIN ASPTBLCURMAS g ON g.ASPTBLCURMASID=a.CURRENCYNAME    JOIN ASPTBLSAMCATMAS h ON h.ASPTBLSAMCATMASID=a.CATEGORY join ASPTBLORDPACKMAS i on i.ASPTBLORDPACKMASID=a.ORDERPACKTYPE join asptblsamtypemas j on j.ASPTBLSAMTYPEMASID=a.SAMPLETYPE  join gtcompmast k on k.GTCOMPMASTID=a.COMPCODE  WHERE   A.INWARD is null   ORDER BY A.AGFSAMPLE DESC";
            DataSet ds0 = Utility.ExecuteSelectQuery(sel0, "ASPTBLBUYSAM");
            DataTable dt0 = ds0.Tables["ASPTBLBUYSAM"];

            rdstock.SetDataSource(dt0);
            crystalReportViewer5.ReportSource = null;
            crystalReportViewer5.ReportSource = rdstock;
            crystalReportViewer5.Refresh();
            lblcount.Refresh();
            lblcount.Text = "Inward Pending qty " + dt0.Rows.Count;
        }
        private void correctionreport()
        {
            string sel0 = "";

            
                sel0 = "SELECT A.AGFSAMPLE, B.COMPNAME,A.GARMENTIMAGE, A.REMARKS, A.FABRICCOMPLIANT , A.RISK1, A.RISK2, A.RISK3, A.RISK4, A.RISK5, A.RISK6  FROM ASPTBLBUYSAM A JOIN GTCOMPMAST B ON A.COMPCODE=B.GTCOMPMASTID WHERE A.REMARKS IS NOT NULL  OR A.FABRICCOMPLIANT IS NOT NULL  OR A.RISK1 IS NOT NULL  OR A.RISK2 IS NOT NULL  OR A.RISK3 IS NOT NULL  OR A.RISK4 IS NOT NULL  OR A.RISK5 IS NOT NULL   OR A.RISK6 IS NOT NULL  ORDER BY 1";

      
            
            DataSet ds0 = Utility.ExecuteSelectQuery(sel0, "ASPTBLBUYSAM");
            DataTable dt0 = ds0.Tables["ASPTBLBUYSAM"];
            if (dt0.Rows.Count > 0)
            {
                sc.SetDataSource(dt0);
                crystalReportViewer4.ReportSource = null;
                crystalReportViewer4.ReportSource = sc;
                crystalReportViewer4.Refresh(); lblcount.Refresh();
                lblcount.Text = " Qty " + dt0.Rows.Count;
               
            }
            else
            {
                MessageBox.Show("No Data Found.");

                crystalReportViewer4.ReportSource = null;

            }
        }
        private void combostyle_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void SampleCollectionReport_Load(object sender, EventArgs e)
        {

        


        }
        public void compcode()
        {
            string sel = " SELECT 0 AS GTCOMPMASTID, 'ALL' AS COMPCODE FROM DUAL UNION ALL SELECT A.GTCOMPMASTID,A.COMPCODE    FROM  GTCOMPMAST A    WHERE A.ACTIVE='T'  ";
            DataSet ds = Utility.ExecuteSelectQuery(sel, "GTCOMPMAST");
            DataTable dt = ds.Tables["GTCOMPMAST"];
            combocompcode.DataSource = dt;
            combocompcode.DisplayMember = "COMPCODE";
            combocompcode.ValueMember = "GTCOMPMASTID";
            combocompcode.Text = dt.Rows[1]["COMPCODE"].ToString();
        }




        public void BrandLoad()
        {

         
                string sel = " SELECT 0 AS ASPTBLBRANDMASID, 'ALL' AS BRAND  FROM DUAL UNION ALL SELECT distinct  A.ASPTBLBRANDMASID,A.BRAND   FROM  ASPTBLBRANDMAS A  JOIN asptblbuysam B ON A.ASPTBLBRANDMASID=B.brand  join gtcompmast c on C.GTCOMPMASTID=B.COMPCODE  join asptblsamdeptmas d on D.ASPTBLSAMDEPTMASID=B.DEPARTMENT    WHERE A.ACTIVE='T'  ";
                DataSet ds = Utility.ExecuteSelectQuery(sel, "ASPTBLBRANDMAS");
                DataTable dt = ds.Tables["ASPTBLBRANDMAS"];
                if (dt.Rows.Count > 0)
                {
                    combobrand.DataSource = null;
                    combobrand.DisplayMember = "BRAND";
                    combobrand.ValueMember = "ASPTBLBRANDMASID";
                    combobrand.DataSource = dt;
                   
                }
            
        }
        void styleLoad(string compcode)
        {
            DataTable dtstyle = new DataTable();
            //if (combocompcode.SelectedItem.ToString() == "ALL")
            //{
            //    string sel = " SELECT 0 AS asptblbuysamID, 'ALL' AS STYLENAME  FROM DUAL UNION ALL SELECT distinct  A.asptblbuysamID,A.STYLENAME   FROM   asptblbuysam A  join gtcompmast c on C.GTCOMPMASTID=A.COMPCODE      WHERE A.ACTIVE='T' ";
            //    DataSet ds = Utility.ExecuteSelectQuery(sel, "ASPTBLBRANDMAS");
            //    dtstyle = ds.Tables["ASPTBLBRANDMAS"];                
            //}
            //if (combocompcode.SelectedItem.ToString() != "ALL")
            //{                
                string sel = "SELECT distinct  A.asptblbuysamID,A.STYLENAME   FROM   asptblbuysam A    WHERE A.ACTIVE='T' ";
                DataSet ds = Utility.ExecuteSelectQuery(sel, "ASPTBLBRANDMAS");
                dtstyle = ds.Tables["ASPTBLBRANDMAS"];               
            //}
            if (dtstyle.Rows.Count > 0)
            {
                combostyle.DataSource = null;
                combostyle.DisplayMember = "STYLENAME";
                combostyle.ValueMember = "asptblbuysamID";
                combostyle.DataSource = dtstyle;

            }
        }
        public void CateGoryLoad(string dept)
        {
            //string sel = "select 0 as ASPTBLSAMCATMASID,'' category from dual union all  SELECT distinct E.ASPTBLSAMCATMASID,E.CATEGORY    FROM  ASPTBLBRANDMAS A  JOIN asptblbuysam B ON A.ASPTBLBRANDMASID=B.brand  join gtcompmast c on C.GTCOMPMASTID=B.COMPCODE join asptblsamdeptmas d on D.ASPTBLSAMDEPTMASID=B.DEPARTMENT  join asptblsamcatmas e on E.ASPTBLSAMCATMASID=B.CATEGORY     WHERE A.ACTIVE='T'   and D.DEPARTMENT='" + dept + "'";
            //DataSet ds = Utility.ExecuteSelectQuery(sel, "ASPTBLSAMDEPTMAS");
            //DataTable dt = ds.Tables["ASPTBLSAMDEPTMAS"];
            //if (dt != null)
            //{
            //    combocategory.DisplayMember = "category";
            //    combocategory.ValueMember = "ASPTBLSAMCATMASID";
            //    combocategory.DataSource = dt;
            //}
            //else
            //{
            //    combocategory.DataSource = null;
            //}
        }
        public void BrandLoad(string compcode, string brand)
        {
            string sel = "SELECT 0 AS ASPTBLSAMDEPTMASID, 'ALL' AS DEPARTMENT  FROM DUAL UNION ALL SELECT DISTINCT  D.ASPTBLSAMDEPTMASID,D.DEPARTMENT    FROM  ASPTBLBRANDMAS A   JOIN asptblbuysam B ON A.ASPTBLBRANDMASID=B.brand  join gtcompmast c on C.GTCOMPMASTID=B.COMPCODE join asptblsamdeptmas d on D.ASPTBLSAMDEPTMASID=B.DEPARTMENT  join asptblsamcatmas e on E.ASPTBLSAMCATMASID=B.CATEGORY     WHERE A.ACTIVE='T'    AND c.compcode='" + compcode + "' and a.BRAND='" + brand + "'";
            DataSet ds = Utility.ExecuteSelectQuery(sel, "ASPTBLSAMDEPTMAS");
            DataTable dt = ds.Tables["ASPTBLSAMDEPTMAS"];
            if (dt != null)
            {
                combodept.DataSource = null;
                combodept.DisplayMember = "DEPARTMENT";
                combodept.ValueMember = "ASPTBLSAMDEPTMASID";
                combodept.DataSource = dt;
            }
            else
            {
                combodept.DataSource = null;
            }
        }
        //public void BrandLoad(string brand, string dept,string cate)
        //{
        //    string sel0 = "SELECT A.ASPTBLBUYSAMID,  A.DATE1,B.COMPCODE,b.compname,C.BRAND,A.AGFSAMPLE,E.SEASON || ' /' ||  A.MFYEAR || '' AS SEASON,F.DEPARTMENT || '(' ||  O.CATEGORY || ')' AS DEPARTMENT ,O.CATEGORY,A.STYLENAME,A.SUBSTYLE,A.FABRIC,A.FABRICCONTENT,A.COUNTS, K.GG AS GAUGE,A.GSM, A.COLORNAME,P.ORDERPACKTYPE,A.SIZENAME,N.CURRENCYNAME,A.REMARKS,A.ACTIVE,A.RISK1,A.RISK2,A.RISK3,A.RISK4,A.RISK5,A.FABRICCOMPLIANT,A.REMARKS,A.MFYEAR    FROM ASPTBLBUYSAM A JOIN GTCOMPMAST B ON A.COMPCODE=B.GTCOMPMASTID join asptblBRANDMAS C ON C.ASPTBLBRANDMASID=A.BRAND  JOIN ASPTBLSEASONMAS E ON E.ASPTBLSEASONMASID=A.SEASON JOIN ASPTBLSAMDEPTMAS F ON F.ASPTBLSAMDEPTMASID=A.DEPARTMENT   JOIN ASPTBLGGMAS K ON K.ASPTBLGGMASID=A.GAUGE      JOIN ASPTBLCURMAS N ON N.ASPTBLCURMASID=A.CURRENCYNAME JOIN ASPTBLSAMCATMAS O ON O.ASPTBLSAMCATMASID=A.CATEGORY  JOIN ASPTBLORDPACKMAS  P ON P.ASPTBLORDPACKMASID=A.ORDERPACKTYPE where  a.active='T'   AND   C.BRAND='" + brand + "' AND   F.DEPARTMENT='" + dept + "'   order by a.ASPTBLBUYSAMID desc ";
        //    DataSet ds0 = Utility.ExecuteSelectQuery(sel0, "ASPTBLBUYSAM");
        //    DataTable dt0 = ds0.Tables["ASPTBLBUYSAM"];
        //    lblcount.Text = "Total  Row's: " + dt0.Rows.Count;
        //    path = "";
        //    path = "Brand/Deparment " + combobrand.Text + "CompCode  ";
        //    rd.SetDataSource(dt0);
        //    crystalReportViewer7.ReportSource = null;
        //    crystalReportViewer7.ReportSource = rd;
        //    crystalReportViewer7.Refresh();

        //}
        public void OrderPackLoad()
        {
            string sel = " SELECT 0 AS ASPTBLORDPACKMASID, 'ALL' AS ORDERPACKTYPE FROM DUAL UNION ALL SELECT 1 AS ASPTBLORDPACKMASID, 'INWARD' AS ORDERPACKTYPE FROM DUAL UNION ALL SELECT 2 AS ASPTBLORDPACKMASid, 'OUTWARD' AS ORDERPACKTYPE FROM DUAL UNION ALL SELECT A.ASPTBLORDPACKMASid,A.ORDERPACKTYPE    FROM  ASPTBLORDPACKMAS A   WHERE A.ACTIVE='T' ";
            DataSet ds = Utility.ExecuteSelectQuery(sel, "ASPTBLORDPACKMAS");
            DataTable dt = ds.Tables["ASPTBLORDPACKMAS"];
            combopacktype1.DataSource = dt;
            combopacktype1.DisplayMember = "ORDERPACKTYPE";
            combopacktype1.ValueMember = "ASPTBLORDPACKMASID";

        }
        public void SeasonLoad(string s)
        {
            DataTable dt=new DataTable();
            if (s == "ALL")
            {
                string sel = "SELECT 0 AS ASPTBLSEASONMASID, 'ALL' AS SEASON  FROM DUAL UNION ALL SELECT DISTINCT  A.ASPTBLSEASONMASID, A.SEASON      FROM  ASPTBLSEASONMAS  A  JOIN asptblbuysam B ON A.ASPTBLSEASONMASID=B.SEASON    join gtcompmast c on C.GTCOMPMASTID=B.COMPCODE         WHERE A.ACTIVE='T' ";
                DataSet ds = Utility.ExecuteSelectQuery(sel, "ASPTBLSEASONMAS");
                dt = ds.Tables["ASPTBLSEASONMAS"];
                if (dt.Rows.Count > 0)
                {
                    comboseason.DataSource = null;
                    comboseason.DataSource = dt;
                    comboseason.DisplayMember = "SEASON";
                    comboseason.ValueMember = "ASPTBLSEASONMASID";
                }
                else
                {
                    comboseason.DataSource = null;
                }
            }
            else
            {
                string sel = " SELECT 0 AS ASPTBLSEASONMASID, 'ALL' AS SEASON  FROM DUAL UNION ALL SELECT DISTINCT  A.ASPTBLSEASONMASID, A.SEASON      FROM  ASPTBLSEASONMAS  A  JOIN asptblbuysam B ON A.ASPTBLSEASONMASID=B.SEASON    join gtcompmast c on C.GTCOMPMASTID=B.COMPCODE         WHERE A.ACTIVE='T' and C.compcode='" + s + "' ";
                DataSet ds = Utility.ExecuteSelectQuery(sel, "ASPTBLSEASONMAS");
                dt = ds.Tables["ASPTBLSEASONMAS"];


                if (dt.Rows.Count > 0)
                {
                    comboseason.DataSource = null;
                    comboseason.DataSource = dt;
                    comboseason.DisplayMember = "SEASON";
                    comboseason.ValueMember = "ASPTBLSEASONMASID";
                }
                else
                {
                    comboseason.DataSource = null;
                }
            }
        }
       

        public void DepartmentLoad()
        {
           
        
        }

        public void FabricLoad()
        {
            

          
        }
        void empty()
        {
            combostyle.SelectedIndex = -1;      combosampletype.Text = "";   
       lblcount.Text = "";
 combobrand.SelectedIndex = -1; combopacktype1.SelectedIndex = -1; comboseason.SelectedIndex = -1; combodept.SelectedIndex = -1; 
            lblcount.Text = "";
            butheader.BackColor = Class.Users.BackColors;
            
            butfooter.BackColor = Class.Users.BackColors;

            butheader.BackColor = Class.Users.BackColors;
            crystalReportViewer1.ReportSource = null;
            crystalReportViewer1.Refresh();
            crystalReportViewer2.ReportSource = null;
            crystalReportViewer2.Refresh();
            crystalReportViewer3.ReportSource = null;
            crystalReportViewer3.Refresh();
            crystalReportViewer4.ReportSource = null;
            crystalReportViewer4.Refresh();
            crystalReportViewer5.ReportSource = null;
            crystalReportViewer5.Refresh();
            crystalReportViewer8.ReportSource = null;
            crystalReportViewer8.Refresh();
            crystalReportViewer9.ReportSource = null;
            crystalReportViewer9.Refresh(); this.Font = Class.Users.FontName; combocompcode.Text = "ALL";
        }
        public void News()
        {
           
          
            compcode(); FabricLoad(); DepartmentLoad();
            
            OrderPackLoad();
            BrandLoad(); SampleTypeLoad();
           
            empty();
        }

        public void Saves()
        {

        }

        public void Prints()
        {

        }

        public void Searchs()
        {

        }

        public void Deletes()
        {

        }

        public void ReadOnlys()
        {
        }

        public void Imports()
        {

        }
   
        public void Pdfs()
        {
            DialogResult result = MessageBox.Show("Do you want to  PDF Formate ??", "PRINT OUT", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (result.Equals(DialogResult.OK))
            {
                if (!Directory.Exists(folderLocation)) { Directory.CreateDirectory(folderLocation); }
                if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage1"])//your specific tabname
                {
                    rd.ExportToDisk(ExportFormatType.PortableDocFormat, folderLocation + "-" + path.ToString() + " SampleStock.pdf");

                    /////  rd.ExportToDisk(ExportFormatType.PortableDocFormat, "d:\\'" + path.ToString() + " " + Class.Users.HCompcode + "'SampleCollection.pdf");

                }
                if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage2"])//your specific tabname
                {
                    rd.ExportToDisk(ExportFormatType.PortableDocFormat, folderLocation + "-" + path.ToString() + " SampleOutWard.pdf");

                    /////  rd.ExportToDisk(ExportFormatType.PortableDocFormat, "d:\\'" + path.ToString() + " " + Class.Users.HCompcode + "'SampleCollection.pdf");

                }
                if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage3"])//your specific tabname
                {

                    //sc.ExportToDisk(ExportFormatType.WordForWindows, "d:\\'" + Class.Users.HCompcode + "'SampleComments.doc");

                    // sc.ExportToDisk(ExportFormatType.ExcelWorkbook, folderLocation + "-" + path.ToString() + " " + Class.Users.HCompcode + "'SampleComments.xls");
                    rd.ExportToDisk(ExportFormatType.PortableDocFormat, "d:\\'" + path + "'Inward.pdf");



                }
               
                if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage4"])//your specific tabname
                {
                    sc.ExportToDisk(ExportFormatType.PortableDocFormat, "d:\\'" + path + "'SampleComments.pdf");


                }
                if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage5"])//your specific tabname
                {
                    pen.ExportToDisk(ExportFormatType.PortableDocFormat, "d:\\'" + path + "'PendingInward.pdf");


                }
                if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage6"])//your specific tabname
                {
                    brand.ExportToDisk(ExportFormatType.PortableDocFormat, "d:\\'" + path + "'Brand/Depart/.pdf");

                }
                if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage7"])//your specific tabname
                {
                    brand.ExportToDisk(ExportFormatType.PortableDocFormat, "d:\\'" + path + "'Brand/Depart/Category.pdf");

                }
                if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage8"])//your specific tabname
                {
                    rchart.ExportToDisk(ExportFormatType.PortableDocFormat, "d:\\'" + path + "'ChartReport.pdf");

                }
                if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage9"])//your specific tabname
                {
                    brandcate.ExportToDisk(ExportFormatType.PortableDocFormat, "d:\\'" + path + "'GrossReport.pdf");

                }
                if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage10"])//your specific tabname
                {
                    rd.ExportToDisk(ExportFormatType.PortableDocFormat, "d:\\'" + path +combosampletype.Text+"'.pdf");

                }
            }
            else
            {

            }
        }

        public void ChangePasswords()
        {

        }

        public void DownLoads()
        {


            DialogResult result = MessageBox.Show("Do you want to  EXCEL Formate ??", "PRINT OUT", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (result.Equals(DialogResult.OK))
            {
                if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage1"])//your specific tabname
                {
                    if (!Directory.Exists(folderLocation)) { Directory.CreateDirectory(folderLocation); }

                    rd.ExportToDisk(ExportFormatType.ExcelWorkbook, folderLocation + "StockReport.xls");
                    return;
                }
                if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage2"])//your specific tabname
                {
                    if (!Directory.Exists(folderLocation)) { Directory.CreateDirectory(folderLocation); }

                    rd.ExportToDisk(ExportFormatType.ExcelWorkbook, folderLocation +  "OutwardReport.xls");
                    return;
                }
                if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage3"])//your specific tabname
                {
                    if (!Directory.Exists(folderLocation)) { Directory.CreateDirectory(folderLocation); }

                    rd.ExportToDisk(ExportFormatType.ExcelWorkbook, folderLocation + "InwardReport.xls");
                    return;


                }
                if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage4"])//your specific tabname
                {
                    if (!Directory.Exists(folderLocation)) { Directory.CreateDirectory(folderLocation); }
                    sc.ExportToDisk(ExportFormatType.WordForWindows, "d:\\'" + Class.Users.HCompcode + "'SampleComments.doc");

                    // sc.ExportToDisk(ExportFormatType.ExcelWorkbook, folderLocation + "-" + path.ToString() + " " + Class.Users.HCompcode + "'SampleComments.xls");
                    sc.ExportToDisk(ExportFormatType.PortableDocFormat, "d:\\'" + Class.Users.HCompcode + "'SampleComments.pdf");

                    return;

                }
                if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage5"])//your specific tabname
                {

                    if (!Directory.Exists(folderLocation)) { Directory.CreateDirectory(folderLocation); }

                    pen.ExportToDisk(ExportFormatType.ExcelWorkbook, folderLocation + Class.Users.HCompcode + "'PendingInwardReport.xls");
                }
                if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage6"])//your specific tabname
                {

                    if (!Directory.Exists(folderLocation)) { Directory.CreateDirectory(folderLocation); }

                    rd.ExportToDisk(ExportFormatType.ExcelWorkbook, folderLocation + "Brand-Depart.xls");
                }
                if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage7"])//your specific tabname
                {

                    if (!Directory.Exists(folderLocation)) { Directory.CreateDirectory(folderLocation); }

                    rd.ExportToDisk(ExportFormatType.ExcelWorkbook, folderLocation +"Brand-Depart-Category.xls");
                }
                if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage8"])//your specific tabname
                {
                    rchart.ExportToDisk(ExportFormatType.ExcelWorkbook, folderLocation+"'ChartReport.xls");
                }
                if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage9"])//your specific tabname
                {
                    brandcate.ExportToDisk(ExportFormatType.PortableDocFormat, folderLocation + "'GrossReport.xls");
                }
                    if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage10"])//your specific tabname
                {

                    if (!Directory.Exists(folderLocation)) { Directory.CreateDirectory(folderLocation); }

                    rd.ExportToDisk(ExportFormatType.ExcelWorkbook, folderLocation + combosampletype.Text + ".xls");
                }
                if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage11"])//your specific tabname
                {

                    if (!Directory.Exists(folderLocation)) { Directory.CreateDirectory(folderLocation); }

                    rd.ExportToDisk(ExportFormatType.ExcelWorkbook, folderLocation + comboseason.Text + ".xls");
                }
            }
            else
            {

            }
        }

        public void ChangeSkins()
        {

        }

        public void Logins()
        {

        }

        public void GlobalSearchs()
        {

        }

        public void TreeButtons()
        {

        }

        public void Exit()
        {
            GlobalVariables.MdiPanel.Show();
            News();
            GlobalVariables.HeaderName.Text = "";
            GlobalVariables.TabCtrl.TabPages.RemoveAt(GlobalVariables.TabCtrl.SelectedIndex);
            this.Hide();
        }

        public void GridLoad()
        {
            //string sel0 = "SELECT A.ASPTBLBUYSAMID,  A.DATE1,B.COMPCODE,b.compname,C.BRAND,A.AGFSAMPLE,E.SEASON || ' /' ||  A.MFYEAR || '' AS SEASON,F.DEPARTMENT || '(' ||  O.CATEGORY || ')' AS DEPARTMENT ,O.CATEGORY,A.STYLENAME,A.SUBSTYLE,A.FABRIC,A.FABRICCONTENT,A.COUNTS, K.GG AS GAUGE,A.GSM, A.COLORNAME,P.ORDERPACKTYPE,A.SIZENAME,N.CURRENCYNAME,A.REMARKS,A.ACTIVE,A.RISK1,A.RISK2,A.RISK3,A.RISK4,A.RISK5,A.FABRICCOMPLIANT,A.REMARKS,A.MFYEAR    FROM ASPTBLBUYSAM A JOIN GTCOMPMAST B ON A.COMPCODE=B.GTCOMPMASTID join asptblBRANDMAS C ON C.ASPTBLBRANDMASID=A.BRAND  JOIN ASPTBLSEASONMAS E ON E.ASPTBLSEASONMASID=A.SEASON JOIN ASPTBLSAMDEPTMAS F ON F.ASPTBLSAMDEPTMASID=A.DEPARTMENT   JOIN ASPTBLGGMAS K ON K.ASPTBLGGMASID=A.GAUGE      JOIN ASPTBLCURMAS N ON N.ASPTBLCURMASID=A.CURRENCYNAME JOIN ASPTBLSAMCATMAS O ON O.ASPTBLSAMCATMASID=A.CATEGORY  JOIN ASPTBLORDPACKMAS  P ON P.ASPTBLORDPACKMASID=A.ORDERPACKTYPE WHERE A.ACTIVE='T'   order by a.ASPTBLBUYSAMID desc ";

            //DataSet ds0 = Utility.ExecuteSelectQuery(sel0, "ASPTBLBUYSAM");
            //DataTable dt0 = ds0.Tables["ASPTBLBUYSAM"]; rd.SetDataSource(dt0);
            //crystalReportViewer1.ReportSource = null;
            //crystalReportViewer1.ReportSource = rd;
            //crystalReportViewer1.Refresh();
            //lblcount.Text = "Total Row's : " + dt0.Rows.Count;
        }


        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            outreport();
            tabControl1.SelectTab("tabPage2");
        }

        private void comboseason_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (comboseason.Text == "ALL")
            //{
            //    GridLoad();
            //}
            //if (comboseason.Text != "ALL" && Class.Users.HCompcode != "ALL")
            //{
            //    string sel0 = "SELECT A.ASPTBLBUYSAMID,  A.DATE1,B.COMPCODE,b.compname,C.BRAND,A.AGFSAMPLE,E.SEASON || ' /' ||  A.MFYEAR || '' AS SEASON,F.DEPARTMENT || '(' ||  O.CATEGORY || ')' AS DEPARTMENT ,O.CATEGORY,A.STYLENAME,A.SUBSTYLE,A.FABRIC,A.FABRICCONTENT,A.COUNTS, K.GG AS GAUGE,A.GSM, A.COLORNAME,P.ORDERPACKTYPE,A.SIZENAME,N.CURRENCYNAME,A.REMARKS,A.ACTIVE,A.RISK1,A.RISK2,A.RISK3,A.RISK4,A.RISK5,A.FABRICCOMPLIANT,A.REMARKS,A.MFYEAR    FROM ASPTBLBUYSAM A JOIN GTCOMPMAST B ON A.COMPCODE=B.GTCOMPMASTID join asptblBRANDMAS C ON C.ASPTBLBRANDMASID=A.BRAND  JOIN ASPTBLSEASONMAS E ON E.ASPTBLSEASONMASID=A.SEASON JOIN ASPTBLSAMDEPTMAS F ON F.ASPTBLSAMDEPTMASID=A.DEPARTMENT   JOIN ASPTBLGGMAS K ON K.ASPTBLGGMASID=A.GAUGE      JOIN ASPTBLCURMAS N ON N.ASPTBLCURMASID=A.CURRENCYNAME JOIN ASPTBLSAMCATMAS O ON O.ASPTBLSAMCATMASID=A.CATEGORY  JOIN ASPTBLORDPACKMAS  P ON P.ASPTBLORDPACKMASID=A.ORDERPACKTYPE where A.ACTIVE='T' AND  e.season='" + comboseason.Text + "' and b.compcode='" + Class.Users.HCompcode + "'  order by a.ASPTBLBUYSAMID desc ";

            //    DataSet ds0 = Utility.ExecuteSelectQuery(sel0, "ASPTBLBUYSAM");
            //    DataTable dt0 = ds0.Tables["ASPTBLBUYSAM"];
            //    lblcount.Text = "Season : " + comboseason.Text + ",  Total  Row's: " + dt0.Rows.Count;
            //    path = "";
            //    path = "Season " + comboseason.Text + "CompCode  ";
            //    rd.SetDataSource(dt0);
            //    crystalReportViewer1.ReportSource = null;
            //    crystalReportViewer1.ReportSource = rd;
            //    crystalReportViewer1.Refresh();
            //    lblcount.Text = "Season : " + comboseason.Text + ",  Total  Row's: " + dt0.Rows.Count; comboseason.Select();
            //}
            //if (comboseason.Text != "ALL" && Class.Users.HCompcode == "ALL")
            //{
            //    string sel0 = "SELECT A.ASPTBLBUYSAMID,  A.DATE1,B.COMPCODE,b.compname,C.BRAND,A.AGFSAMPLE,E.SEASON || ' /' ||  A.MFYEAR || '' AS SEASON,F.DEPARTMENT || '(' ||  O.CATEGORY || ')' AS DEPARTMENT ,O.CATEGORY,A.STYLENAME,A.SUBSTYLE,A.FABRIC,A.FABRICCONTENT,A.COUNTS, K.GG AS GAUGE,A.GSM, A.COLORNAME,P.ORDERPACKTYPE,A.SIZENAME,N.CURRENCYNAME,A.REMARKS,A.ACTIVE,A.RISK1,A.RISK2,A.RISK3,A.RISK4,A.RISK5,A.FABRICCOMPLIANT,A.REMARKS,A.MFYEAR    FROM ASPTBLBUYSAM A JOIN GTCOMPMAST B ON A.COMPCODE=B.GTCOMPMASTID join asptblBRANDMAS C ON C.ASPTBLBRANDMASID=A.BRAND  JOIN ASPTBLSEASONMAS E ON E.ASPTBLSEASONMASID=A.SEASON JOIN ASPTBLSAMDEPTMAS F ON F.ASPTBLSAMDEPTMASID=A.DEPARTMENT   JOIN ASPTBLGGMAS K ON K.ASPTBLGGMASID=A.GAUGE      JOIN ASPTBLCURMAS N ON N.ASPTBLCURMASID=A.CURRENCYNAME JOIN ASPTBLSAMCATMAS O ON O.ASPTBLSAMCATMASID=A.CATEGORY  JOIN ASPTBLORDPACKMAS  P ON P.ASPTBLORDPACKMASID=A.ORDERPACKTYPE where A.ACTIVE='T' AND  e.season='" + comboseason.Text + "'   order by a.ASPTBLBUYSAMID desc ";

            //    DataSet ds0 = Utility.ExecuteSelectQuery(sel0, "ASPTBLBUYSAM");
            //    DataTable dt0 = ds0.Tables["ASPTBLBUYSAM"];
            //    lblcount.Text = "Season : " + comboseason.Text + ",  Total  Row's: " + dt0.Rows.Count;
            //    path = "";
            //    path = "Season " + comboseason.Text + "CompCode  ";
            //    rd.SetDataSource(dt0);
            //    crystalReportViewer1.ReportSource = null;
            //    crystalReportViewer1.ReportSource = rd;
            //    crystalReportViewer1.Refresh(); comboseason.Select();

            //}
        }

        private void combodept_SelectedIndexChanged(object sender, EventArgs e)
        {
         
          

        }

        private void combopacktype_SelectedIndexChanged(object sender, EventArgs e)
        {
            butdatewise.Refresh();
            butdatewise.Text = "Click " + combopacktype1.Text;
            //if (combopacktype1.Text == "ALL")
            //{
            //    GridLoad();
            //}
            //if (combopacktype1.Text != "ALL")
            //{
            //    string sel0 = "SELECT A.ASPTBLBUYSAMID,  A.DATE1,B.COMPCODE,b.compname,C.BRAND,A.AGFSAMPLE,E.SEASON || ' /' ||  A.MFYEAR || '' AS SEASON,F.DEPARTMENT || '(' ||  O.CATEGORY || ')' AS DEPARTMENT ,O.CATEGORY,A.STYLENAME,A.SUBSTYLE,A.FABRIC,A.FABRICCONTENT,A.COUNTS, K.GG AS GAUGE,A.GSM, A.COLORNAME,P.ORDERPACKTYPE,A.SIZENAME,N.CURRENCYNAME,A.REMARKS,A.ACTIVE,A.RISK1,A.RISK2,A.RISK3,A.RISK4,A.RISK5,A.FABRICCOMPLIANT,A.REMARKS,A.MFYEAR    FROM ASPTBLBUYSAM A JOIN GTCOMPMAST B ON A.COMPCODE=B.GTCOMPMASTID join asptblBRANDMAS C ON C.ASPTBLBRANDMASID=A.BRAND  JOIN ASPTBLSEASONMAS E ON E.ASPTBLSEASONMASID=A.SEASON JOIN ASPTBLSAMDEPTMAS F ON F.ASPTBLSAMDEPTMASID=A.DEPARTMENT   JOIN ASPTBLGGMAS K ON K.ASPTBLGGMASID=A.GAUGE      JOIN ASPTBLCURMAS N ON N.ASPTBLCURMASID=A.CURRENCYNAME JOIN ASPTBLSAMCATMAS O ON O.ASPTBLSAMCATMASID=A.CATEGORY  JOIN ASPTBLORDPACKMAS  P ON P.ASPTBLORDPACKMASID=A.ORDERPACKTYPE where A.ACTIVE='T' AND  P.ORDERPACKTYPE='" + combopacktype1.Text + "' and b.compcode='" + Class.Users.HCompcode + "'  order by a.ASPTBLBUYSAMID desc ";

            //    DataSet ds0 = Utility.ExecuteSelectQuery(sel0, "ASPTBLBUYSAM");
            //    DataTable dt0 = ds0.Tables["ASPTBLBUYSAM"];
            //    lblcount.Text = "Order PackType : " + combopacktype1.Text + ",  Total  Row's: " + dt0.Rows.Count;
            //    path = "";
            //    path = "OrderPackType " + combopacktype1.Text + "CompCode  ";
            //    rd.SetDataSource(dt0);
            //    crystalReportViewer1.ReportSource = null;
            //    crystalReportViewer1.ReportSource = rd;
            //    crystalReportViewer1.Refresh(); combopacktype1.Select();
            //    butdatewise.Text = "Click " + combopacktype1.Text;
            //}
            //if (combopacktype1.Text != "ALL" && Class.Users.HCompcode == "ALL")
            //{
            //    string sel0 = "SELECT A.ASPTBLBUYSAMID,  A.DATE1,B.COMPCODE,b.compname,C.BRAND,A.AGFSAMPLE,E.SEASON || ' /' ||  A.MFYEAR || '' AS SEASON,F.DEPARTMENT || '(' ||  O.CATEGORY || ')' AS DEPARTMENT ,O.CATEGORY,A.STYLENAME,A.SUBSTYLE,A.FABRIC,A.FABRICCONTENT,A.COUNTS, K.GG AS GAUGE,A.GSM, A.COLORNAME,P.ORDERPACKTYPE,A.SIZENAME,N.CURRENCYNAME,A.REMARKS,A.ACTIVE,A.RISK1,A.RISK2,A.RISK3,A.RISK4,A.RISK5,A.FABRICCOMPLIANT,A.REMARKS,A.MFYEAR    FROM ASPTBLBUYSAM A JOIN GTCOMPMAST B ON A.COMPCODE=B.GTCOMPMASTID join asptblBRANDMAS C ON C.ASPTBLBRANDMASID=A.BRAND  JOIN ASPTBLSEASONMAS E ON E.ASPTBLSEASONMASID=A.SEASON JOIN ASPTBLSAMDEPTMAS F ON F.ASPTBLSAMDEPTMASID=A.DEPARTMENT   JOIN ASPTBLGGMAS K ON K.ASPTBLGGMASID=A.GAUGE      JOIN ASPTBLCURMAS N ON N.ASPTBLCURMASID=A.CURRENCYNAME JOIN ASPTBLSAMCATMAS O ON O.ASPTBLSAMCATMASID=A.CATEGORY  JOIN ASPTBLORDPACKMAS  P ON P.ASPTBLORDPACKMASID=A.ORDERPACKTYPE where A.ACTIVE='T' AND  P.ORDERPACKTYPE='" + combopacktype1.Text + "'  order by a.ASPTBLBUYSAMID desc ";

            //    DataSet ds0 = Utility.ExecuteSelectQuery(sel0, "ASPTBLBUYSAM");
            //    DataTable dt0 = ds0.Tables["ASPTBLBUYSAM"];
            //    rd.SetDataSource(dt0);
            //    crystalReportViewer1.ReportSource = null;
            //    crystalReportViewer1.ReportSource = rd;
            //    crystalReportViewer1.Refresh();
            //    lblcount.Text = "Order PackType : " + combopacktype1.Text + ",  Total  Row's: " + dt0.Rows.Count;
            //    path = ""; butdatewise.Text = "Click " + combopacktype1.Text;
            //    path = "OrderPackType " + combopacktype1.Text + "CompCode  "; combopacktype1.Select();
            //}
        }

        private void txtrefcode_TextChanged(object sender, EventArgs e)
        {
            //if (txtrefcode.Text.Length >= 5)
            //{
            //    string sel0 = "SELECT  A.DATE1,B.COMPCODE,b.compname,C.BRAND,A.AGFSAMPLE,E.SEASON || ' /' ||  A.MFYEAR || '' AS SEASON,F.DEPARTMENT || '(' ||  O.CATEGORY || ')' AS DEPARTMENT ,O.CATEGORY,A.STYLENAME,A.SUBSTYLE,A.FABRIC,A.FABRICCONTENT,A.COUNTS, K.GG AS GAUGE,A.GSM, A.COLORNAME,P.ORDERPACKTYPE,A.SIZENAME,N.CURRENCYNAME,A.REMARKS,A.ACTIVE,A.RISK1,A.RISK2,A.RISK3,A.RISK4,A.RISK5,A.FABRICCOMPLIANT,A.REMARKS,A.MFYEAR    FROM ASPTBLBUYSAM A JOIN GTCOMPMAST B ON A.COMPCODE=B.GTCOMPMASTID join asptblBRANDMAS C ON C.ASPTBLBRANDMASID=A.BRAND  JOIN ASPTBLSEASONMAS E ON E.ASPTBLSEASONMASID=A.SEASON JOIN ASPTBLSAMDEPTMAS F ON F.ASPTBLSAMDEPTMASID=A.DEPARTMENT   JOIN ASPTBLGGMAS K ON K.ASPTBLGGMASID=A.GAUGE      JOIN ASPTBLCURMAS N ON N.ASPTBLCURMASID=A.CURRENCYNAME JOIN ASPTBLSAMCATMAS O ON O.ASPTBLSAMCATMASID=A.CATEGORY  JOIN ASPTBLORDPACKMAS  P ON P.ASPTBLORDPACKMASID=A.ORDERPACKTYPE where  A.AGFSAMPLE='" + txtrefcode.Text + "'   order by a.ASPTBLBUYSAMID desc ";

            //    DataSet ds0 = Utility.ExecuteSelectQuery(sel0, "ASPTBLBUYSAM");
            //    DataTable dt0 = ds0.Tables["ASPTBLBUYSAM"]; rd.SetDataSource(dt0);
            //    crystalReportViewer1.ReportSource = null;
            //    crystalReportViewer1.ReportSource = rd;
            //    crystalReportViewer1.Refresh();

            //    crystalReportViewer2.ReportSource = null;
            //    crystalReportViewer2.ReportSource = rd;
            //    crystalReportViewer2.Refresh();
               
            //    lblcount.Text = "RefCode : " + txtrefcode.Text + ",  Total  Row's: " + dt0.Rows.Count;
            //    path = "";
            //    path = "RefCode " + txtrefcode.Text + "CompCode  "; txtrefcode.Select();

            //    correctionreport();
            //}
        }

        private void combocompcode_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void combobrand_SelectedIndexChanged(object sender, EventArgs e)
        {
            BrandLoad(combocompcode.Text,combobrand.Text);

        }

        private void combofabric_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Class.Users.UserTime = 0;
            if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage1"])//your specific tabname
            {
                stockreport();

          
            }
            if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage2"])//your specific tabname
            {
                lblcount.Text = "";
                    crystalReportViewer2.ReportSource = null; crystalReportViewer2.Refresh();
                outreport();
             
            }
            if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage3"])//your specific tabname
            {
                lblcount.Text = "";
                crystalReportViewer3.ReportSource = null; crystalReportViewer3.Refresh();
                inwardreport();
                
            }
            if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage4"])//your specific tabname
            {
                correctionreport();

            }
            if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage5"])//your specific tabname
            {
                Pendingreport();

            }
            if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage6"])//your specific tabname
            {
                butbrandstyle_Click(sender,e);

            }
          

            if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage8"])//your specific tabname
            {

                reportchart();

            }
            if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage10"])//your specific tabname
            {

                button1_Click(sender,e);

            }
            if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage9"])//your specific tabname
            {
                DataTable dt0 = new DataTable();
                if (combosampletype.Text != "")
                {
                    tabControl1.SelectedTab = tabPage9;
                   
                        string sel0 = "select  A.AGFSAMPLE ,k.cOMPCODE, a.DATE1,b.BRAND,d.SEASON , e.DEPARTMENT, h.CATEGORY,a.STYLENAME,A.FABRIC ,A.FABRICCONTENT,A.COUNTS, f.GG AS GAUGE,A.GSM, A.COLORNAME,i.ORDERPACKTYPE,A.SIZENAME,g.CURRENCYNAME, a.REMARKS,a.ACTIVE,a.RISK1,a.RISK2,a.RISK3,a.RISK4,a.RISK5,a.FABRICCOMPLIANT,a.MFYEAR   from    asptblbuysam A  JOIN ASPTBLBRANDMAS b ON b.ASPTBLBRANDMASID=A.BRAND  JOIN ASPTBLSEASONMAS d ON d.ASPTBLSEASONMASID=A.SEASON  JOIN ASPTBLSAMDEPTMAS e ON e.ASPTBLSAMDEPTMASID=a.DEPARTMENT   JOIN ASPTBLGGMAS f ON f.ASPTBLGGMASID=a.GAUGE   JOIN ASPTBLCURMAS g ON g.ASPTBLCURMASID=a.CURRENCYNAME    JOIN ASPTBLSAMCATMAS h ON h.ASPTBLSAMCATMASID=a.CATEGORY join ASPTBLORDPACKMAS i on i.ASPTBLORDPACKMASID=a.ORDERPACKTYPE join asptblsamtypemas j on j.ASPTBLSAMTYPEMASID=a.SAMPLETYPE  join gtcompmast k on k.GTCOMPMASTID=a.COMPCODE  WHERE   A.INWARD='T'   ORDER BY A.AGFSAMPLE DESC";
                        DataSet ds0 = Utility.ExecuteSelectQuery(sel0, "ASPTBLBUYSAM");
                        dt0 = ds0.Tables["ASPTBLBUYSAM"];
                        if (dt0.Rows.Count>0){ 
                        lblcount.Text = "GrossReport  : " + combocompcode.Text + ",  Total  Row's: " + dt0.Rows.Count;
                        path = "";
                        path = "GrossReport " + combocompcode.Text + "CompCode  ";
                        brandcate.SetDataSource(dt0);
                        crystalReportViewer9.ReportSource = null;
                        crystalReportViewer9.ReportSource = brandcate;
                        crystalReportViewer9.Refresh();
                        }
                        else
                        {
                            crystalReportViewer9.ReportSource = null;

                        }
                   
                }
            }

            if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage11"])//your specific tabname
            {
                DataTable dt0;
                if (comboseason.Text == "ALL")
                {

                    tabControl1.SelectedTab = tabPage11;
                    //  string sel0 = "SELECT A.ASPTBLBUYSAMID,  A.DATE1,B.COMPCODE,b.compname,C.BRAND,A.AGFSAMPLE,E.SEASON || ' /' ||  A.MFYEAR || '' AS SEASON,F.DEPARTMENT || '(' ||  O.CATEGORY || ')' AS DEPARTMENT ,q.SAMPLETYPE CATEGORY,A.STYLENAME,A.SUBSTYLE,A.FABRIC ,A.FABRICCONTENT,A.FABRICCONTENT,A.COUNTS, K.GG AS GAUGE,A.GSM, A.COLORNAME,P.ORDERPACKTYPE,A.SIZENAME,N.CURRENCYNAME,A.REMARKS,A.ACTIVE,A.RISK1,A.RISK2,A.RISK3,A.RISK4,A.RISK5,A.FABRICCOMPLIANT,A.REMARKS,A.MFYEAR    FROM ASPTBLBUYSAM A JOIN GTCOMPMAST B ON A.COMPCODE=B.GTCOMPMASTID join asptblBRANDMAS C ON C.ASPTBLBRANDMASID=A.BRAND  JOIN ASPTBLSEASONMAS E ON E.ASPTBLSEASONMASID=A.SEASON JOIN ASPTBLSAMDEPTMAS F ON F.ASPTBLSAMDEPTMASID=A.DEPARTMENT   JOIN ASPTBLGGMAS K ON K.ASPTBLGGMASID=A.GAUGE      JOIN ASPTBLCURMAS N ON N.ASPTBLCURMASID=A.CURRENCYNAME JOIN ASPTBLSAMCATMAS O ON O.ASPTBLSAMCATMASID=A.CATEGORY  JOIN ASPTBLORDPACKMAS  P ON P.ASPTBLORDPACKMASID=A.ORDERPACKTYPE join asptblsamtypemas q on q.asptblsamtypemasid=A.SAMPLETYPE    where A.INWARD='T'  ORDER by a.ASPTBLBUYSAMID desc ";
                    string sel0 = "select   A.AGFSAMPLE ,k.cOMPCODE, a.DATE1,b.BRAND,d.SEASON , e.DEPARTMENT, h.CATEGORY,a.STYLENAME,A.FABRIC ,A.FABRICCONTENT,A.COUNTS, f.GG AS GAUGE,A.GSM, A.COLORNAME,i.ORDERPACKTYPE,A.SIZENAME,g.CURRENCYNAME, a.REMARKS,a.ACTIVE,a.RISK1,a.RISK2,a.RISK3,a.RISK4,a.RISK5,a.FABRICCOMPLIANT,a.MFYEAR   from    asptblbuysam A  JOIN ASPTBLBRANDMAS b ON b.ASPTBLBRANDMASID=A.BRAND   JOIN ASPTBLSEASONMAS d ON d.ASPTBLSEASONMASID=A.SEASON  JOIN ASPTBLSAMDEPTMAS e ON e.ASPTBLSAMDEPTMASID=a.DEPARTMENT   JOIN ASPTBLGGMAS f ON f.ASPTBLGGMASID=a.GAUGE   JOIN ASPTBLCURMAS g ON g.ASPTBLCURMASID=a.CURRENCYNAME    JOIN ASPTBLSAMCATMAS h ON h.ASPTBLSAMCATMASID=a.CATEGORY join ASPTBLORDPACKMAS i on i.ASPTBLORDPACKMASID=a.ORDERPACKTYPE join asptblsamtypemas j on j.ASPTBLSAMTYPEMASID=a.SAMPLETYPE  join gtcompmast k on k.GTCOMPMASTID=a.COMPCODE  WHERE   A.INWARD='T'   ORDER BY A.AGFSAMPLE DESC";

                    DataSet ds0 = Utility.ExecuteSelectQuery(sel0, "ASPTBLBUYSAM");
                    dt0 = ds0.Tables["ASPTBLBUYSAM"];
                    if (dt0.Rows.Count > 0)
                    {

                        rd.SetDataSource(dt0);
                        crystalReportViewer11.ReportSource = null;
                        crystalReportViewer11.ReportSource = rd;
                        crystalReportViewer11.Refresh();
                        lblcount.Text = "Season : " + comboseason.Text + ",  Total  Row's: " + dt0.Rows.Count;
                        path = "";
                        path = "Season " + comboseason.Text + "CompCode  ";
                    }
                    if (dt0.Rows.Count < 0)
                    {
                        MessageBox.Show("pls select Season");
                        crystalReportViewer11.ReportSource = null;
                    }
                }
                if (comboseason.Text != "ALL")
                {

                    tabControl1.SelectedTab = tabPage11;
                    string sel0 = "select   A.AGFSAMPLE ,k.cOMPCODE, a.DATE1,b.BRAND,d.SEASON , e.DEPARTMENT, h.CATEGORY,c.STYLENAME,A.FABRIC ,A.FABRICCONTENT,A.COUNTS, f.GG AS GAUGE,A.GSM, A.COLORNAME,i.ORDERPACKTYPE,A.SIZENAME,g.CURRENCYNAME, a.REMARKS,a.ACTIVE,a.RISK1,a.RISK2,a.RISK3,a.RISK4,a.RISK5,a.FABRICCOMPLIANT,a.MFYEAR   from    asptblbuysam A  JOIN ASPTBLBRANDMAS b ON b.ASPTBLBRANDMASID=A.BRAND   JOIN ASPTBLSEASONMAS d ON d.ASPTBLSEASONMASID=A.SEASON  JOIN ASPTBLSAMDEPTMAS e ON e.ASPTBLSAMDEPTMASID=a.DEPARTMENT   JOIN ASPTBLGGMAS f ON f.ASPTBLGGMASID=a.GAUGE   JOIN ASPTBLCURMAS g ON g.ASPTBLCURMASID=a.CURRENCYNAME    JOIN ASPTBLSAMCATMAS h ON h.ASPTBLSAMCATMASID=a.CATEGORY join ASPTBLORDPACKMAS i on i.ASPTBLORDPACKMASID=a.ORDERPACKTYPE join asptblsamtypemas j on j.ASPTBLSAMTYPEMASID=a.SAMPLETYPE  join gtcompmast k on k.GTCOMPMASTID=a.COMPCODE  WHERE   A.INWARD='T'    and    d.season='" + comboseason.Text + "'   order by a.agfsample desc ";

                    DataSet ds0 = Utility.ExecuteSelectQuery(sel0, "ASPTBLBUYSAM");
                    dt0 = ds0.Tables["ASPTBLBUYSAM"];
                    if (dt0.Rows.Count > 0)
                    {

                        rd.SetDataSource(dt0);
                        crystalReportViewer11.ReportSource = null;
                        crystalReportViewer11.ReportSource = rd;
                        crystalReportViewer11.Refresh();
                        lblcount.Text = "Season : " + comboseason.Text + ",  Total  Row's: " + dt0.Rows.Count;
                        path = "";
                        path = "Season " + comboseason.Text + "CompCode  ";
                    }
                    if (dt0.Rows.Count < 0)
                    {
                        MessageBox.Show("pls select Season");
                        crystalReportViewer11.ReportSource = null;
                    }
                }
            }
        }

        private void butdatewise_Click(object sender, EventArgs e)
        {
            Class.Users.UserTime = 0; DataTable dt0 = new DataTable();


            if (combocompcode.Text == "ALL" && combopacktype1.Text == "OUTWARD")
            {
                tabControl1.SelectedTab = tabPage2; butdatewise.Refresh();
                butdatewise.Text = "Click " + combopacktype1.Text;
                string sel0 = "SELECT A.DATE2 AS DATE1,C.COMPCODE,C.compname,F.BRAND,B.AGFSAMPLE,G.SEASON || ' /' ||  B.MFYEAR || '' AS SEASON,R.DEPARTMENT || '(' ||  S.CATEGORY || ')' AS DEPARTMENT ,S.CATEGORY,B.STYLENAME,B.SUBSTYLE,B.FABRIC ,B.FABRICCONTENT,B.COUNTS, T.GG AS GAUGE,B.GSM, B.COLORNAME,U.ORDERPACKTYPE,B.SIZENAME,V.CURRENCYNAME,B.REMARKS,B.ACTIVE,B.RISK1,B.RISK2,B.RISK3,B.RISK4,B.RISK5,B.FABRICCOMPLIANT,B.REMARKS,B.MFYEAR  FROM ASPTBLBUYSAMOUT A JOIN ASPTBLBUYSAM B ON A.AGFSAMPLE=B.AGFSAMPLE   JOIN GTCOMPMAST C ON C.GTCOMPMASTID=A.COMPCODE    JOIN ASPTBLRACKMAS D ON D.ASPTBLRACKMASID=A.RACK   JOIN ASPTBLBINMAS E ON E.ASPTBLBINMASID=A.BIN JOIN ASPTBLBRANDMAS F ON F.ASPTBLBRANDMASID=A.BRAND         JOIN ASPTBLSEASONMAS G ON G.ASPTBLSEASONMASID=A.SEASON   JOIN ASPTBLSAMTYPEMAS Q ON Q.ASPTBLSAMTYPEMASID=B.SAMPLETYPE JOIN ASPTBLSAMDEPTMAS R ON R.ASPTBLSAMDEPTMASID=B.DEPARTMENT  JOIN ASPTBLSAMCATMAS S ON S.ASPTBLSAMCATMASID=B.CATEGORY  JOIN ASPTBLGGMAS T ON T.ASPTBLGGMASID=B.GAUGE   JOIN ASPTBLORDPACKMAS  U ON U.ASPTBLORDPACKMASID=B.ORDERPACKTYPE  JOIN ASPTBLCURMAS V ON V.ASPTBLCURMASID=B.CURRENCYNAME  WHERE A.OUTWARD='T' AND  a.date2 between TO_DATE('" + dateTimePicker1.Value.ToString("dd-MM-yyyy") + "','dd-MM-yyyy') and TO_DATE('" + dateTimePicker2.Value.ToString("dd-MM-yyyy") + "','dd-MM-yyyy')  ORDER by 1";
                DataSet ds0 = Utility.ExecuteSelectQuery(sel0, "ASPTBLBUYSAM");
                dt0 = ds0.Tables["ASPTBLBUYSAM"];
                path = "";
                path = dateTimePicker1.Value.ToString("dd-MM-yyyy");
                lblcount.Text = "Date : " + dateTimePicker1.Value.ToString("dd-MM-yyyy") + ",  Total  Row's: " + dt0.Rows.Count;
                if (dt0.Rows.Count > 0)
                {
                    rd.SetDataSource(dt0);
                    crystalReportViewer2.ReportSource = null; crystalReportViewer2.Refresh();
                    crystalReportViewer2.ReportSource = rd;
                   
                }
                else
                {
                    crystalReportViewer1.ReportSource = null; crystalReportViewer1.Refresh();
                }
                lblcount.Refresh();
                lblcount.Text = "CompCode : " + Class.Users.HCompcode + ",  Total Qty " + dt0.Rows.Count;
            }
            if (combocompcode.Text != "ALL" && combocompcode.Text != "" && combopacktype1.Text == "OUTWARD")
            {
                tabControl1.SelectedTab = tabPage2; butdatewise.Refresh();
                butdatewise.Text = "Click " + combopacktype1.Text;
                string sel0 = "SELECT A.DATE2 AS DATE1,C.COMPCODE,C.compname,F.BRAND,B.AGFSAMPLE,G.SEASON || ' /' ||  B.MFYEAR || '' AS SEASON,R.DEPARTMENT || '(' ||  S.CATEGORY || ')' AS DEPARTMENT ,S.CATEGORY,B.STYLENAME,B.SUBSTYLE,B.FABRIC ,B.FABRICCONTENT,B.COUNTS, T.GG AS GAUGE,B.GSM, B.COLORNAME,U.ORDERPACKTYPE,B.SIZENAME,V.CURRENCYNAME,B.REMARKS,B.ACTIVE,B.RISK1,B.RISK2,B.RISK3,B.RISK4,B.RISK5,B.FABRICCOMPLIANT,B.REMARKS,B.MFYEAR  FROM ASPTBLBUYSAMOUT A JOIN ASPTBLBUYSAM B ON A.AGFSAMPLE=B.AGFSAMPLE   JOIN GTCOMPMAST C ON C.GTCOMPMASTID=A.COMPCODE    JOIN ASPTBLRACKMAS D ON D.ASPTBLRACKMASID=A.RACK   JOIN ASPTBLBINMAS E ON E.ASPTBLBINMASID=A.BIN JOIN ASPTBLBRANDMAS F ON F.ASPTBLBRANDMASID=A.BRAND         JOIN ASPTBLSEASONMAS G ON G.ASPTBLSEASONMASID=A.SEASON   JOIN ASPTBLSAMTYPEMAS Q ON Q.ASPTBLSAMTYPEMASID=B.SAMPLETYPE JOIN ASPTBLSAMDEPTMAS R ON R.ASPTBLSAMDEPTMASID=B.DEPARTMENT  JOIN ASPTBLSAMCATMAS S ON S.ASPTBLSAMCATMASID=B.CATEGORY  JOIN ASPTBLGGMAS T ON T.ASPTBLGGMASID=B.GAUGE   JOIN ASPTBLORDPACKMAS  U ON U.ASPTBLORDPACKMASID=B.ORDERPACKTYPE  JOIN ASPTBLCURMAS V ON V.ASPTBLCURMASID=B.CURRENCYNAME  WHERE A.OUTWARD='T' AND  a.date2 between TO_DATE('" + dateTimePicker1.Value.ToString("dd-MM-yyyy") + "','dd-MM-yyyy') and TO_DATE('" + dateTimePicker2.Value.ToString("dd-MM-yyyy") + "','dd-MM-yyyy')    ORDER by 1";
                DataSet ds0 = Utility.ExecuteSelectQuery(sel0, "ASPTBLBUYSAM");
                dt0 = ds0.Tables["ASPTBLBUYSAM"];
                path = "";
                path = dateTimePicker1.Value.ToString("dd-MM-yyyy");
                lblcount.Text = "Date : " + dateTimePicker1.Value.ToString("dd-MM-yyyy") + ",  Total  Row's: " + dt0.Rows.Count;
                if (dt0.Rows.Count > 0)
                {
                    rd.SetDataSource(dt0);
                    crystalReportViewer2.ReportSource = null; crystalReportViewer2.Refresh();
                    crystalReportViewer2.ReportSource = rd;
                    
                }
                else
                {
                    crystalReportViewer1.ReportSource = null; crystalReportViewer1.Refresh();
                }
                lblcount.Refresh();
                lblcount.Text = "CompCode : " + Class.Users.HCompcode + ",  Total Qty " + dt0.Rows.Count;
            }
            if (combocompcode.Text != "ALL" && combocompcode.Text != "" && combopacktype1.Text == "INWARD")
            {
                tabControl1.SelectedTab = tabPage3; butdatewise.Refresh();
                butdatewise.Text = "Click " + combopacktype1.Text;
                string sel0 = "SELECT A.DATE2 AS DATE1,C.COMPCODE,C.compname,F.BRAND,B.AGFSAMPLE,G.SEASON || ' /' ||  B.MFYEAR || '' AS SEASON,R.DEPARTMENT || '(' ||  S.CATEGORY || ')' AS DEPARTMENT ,S.CATEGORY,B.STYLENAME,B.SUBSTYLE,B.FABRIC ,B.FABRICCONTENT,B.COUNTS, T.GG AS GAUGE,B.GSM, B.COLORNAME,U.ORDERPACKTYPE,B.SIZENAME,V.CURRENCYNAME,B.REMARKS,B.ACTIVE,B.RISK1,B.RISK2,B.RISK3,B.RISK4,B.RISK5,B.FABRICCOMPLIANT,B.REMARKS,B.MFYEAR  FROM ASPTBLBUYSAMINW A JOIN ASPTBLBUYSAM B ON A.AGFSAMPLE=B.AGFSAMPLE   JOIN GTCOMPMAST C ON C.GTCOMPMASTID=A.COMPCODE    JOIN ASPTBLRACKMAS D ON D.ASPTBLRACKMASID=A.RACK   JOIN ASPTBLBINMAS E ON E.ASPTBLBINMASID=A.BIN JOIN ASPTBLBRANDMAS F ON F.ASPTBLBRANDMASID=A.BRAND         JOIN ASPTBLSEASONMAS G ON G.ASPTBLSEASONMASID=A.SEASON   JOIN ASPTBLSAMTYPEMAS Q ON Q.ASPTBLSAMTYPEMASID=B.SAMPLETYPE JOIN ASPTBLSAMDEPTMAS R ON R.ASPTBLSAMDEPTMASID=B.DEPARTMENT  JOIN ASPTBLSAMCATMAS S ON S.ASPTBLSAMCATMASID=B.CATEGORY  JOIN ASPTBLGGMAS T ON T.ASPTBLGGMASID=B.GAUGE   JOIN ASPTBLORDPACKMAS  U ON U.ASPTBLORDPACKMASID=B.ORDERPACKTYPE  JOIN ASPTBLCURMAS V ON V.ASPTBLCURMASID=B.CURRENCYNAME where   a.date2 between TO_DATE('" + dateTimePicker1.Value.ToString("dd-MM-yyyy") + "','dd-MM-yyyy') and TO_DATE('" + dateTimePicker2.Value.ToString("dd-MM-yyyy") + "','dd-MM-yyyy')  and C.compcode='" + combocompcode.Text + "'  AND A.INWARD='T'  order by 1 ";
                DataSet ds0 = Utility.ExecuteSelectQuery(sel0, "ASPTBLBUYSAM");
                dt0 = ds0.Tables["ASPTBLBUYSAM"];
                path = "";
                path = dateTimePicker1.Value.ToString("dd-MM-yyyy");
                lblcount.Text = "CompCode : " + Class.Users.HCompcode + ",  Total  Row's: " + dt0.Rows.Count;
                if (dt0.Rows.Count > 0)
                {
                    rd.SetDataSource(dt0); crystalReportViewer3.Refresh();
                    crystalReportViewer3.ReportSource = null;
                    crystalReportViewer3.ReportSource = rd;
                    
                }
                else
                {
                    crystalReportViewer3.ReportSource = null; crystalReportViewer3.Refresh();
                    MessageBox.Show("No Data Found");
                }
                lblcount.Refresh();
                lblcount.Text = "CompCode : " + Class.Users.HCompcode + ",  Total  Qty " + dt0.Rows.Count;
            }
            if (combocompcode.Text == "ALL" && combopacktype1.Text == "INWARD")
            {
                tabControl1.SelectedTab = tabPage3;
                butdatewise.Refresh();
                butdatewise.Text = "Click " + combopacktype1.Text;
                string sel0 = "SELECT A.DATE2 AS DATE1,C.COMPCODE,C.compname,F.BRAND,B.AGFSAMPLE,G.SEASON || ' /' ||  B.MFYEAR || '' AS SEASON,R.DEPARTMENT || '(' ||  S.CATEGORY || ')' AS DEPARTMENT ,S.CATEGORY,B.STYLENAME,B.SUBSTYLE,B.FABRIC ,B.FABRICCONTENT,B.COUNTS, T.GG AS GAUGE,B.GSM, B.COLORNAME,U.ORDERPACKTYPE,B.SIZENAME,V.CURRENCYNAME,B.REMARKS,B.ACTIVE,B.RISK1,B.RISK2,B.RISK3,B.RISK4,B.RISK5,B.FABRICCOMPLIANT,B.REMARKS,B.MFYEAR  FROM ASPTBLBUYSAMINW A JOIN ASPTBLBUYSAM B ON A.AGFSAMPLE=B.AGFSAMPLE   JOIN GTCOMPMAST C ON C.GTCOMPMASTID=A.COMPCODE    JOIN ASPTBLRACKMAS D ON D.ASPTBLRACKMASID=A.RACK   JOIN ASPTBLBINMAS E ON E.ASPTBLBINMASID=A.BIN JOIN ASPTBLBRANDMAS F ON F.ASPTBLBRANDMASID=A.BRAND         JOIN ASPTBLSEASONMAS G ON G.ASPTBLSEASONMASID=A.SEASON   JOIN ASPTBLSAMTYPEMAS Q ON Q.ASPTBLSAMTYPEMASID=B.SAMPLETYPE JOIN ASPTBLSAMDEPTMAS R ON R.ASPTBLSAMDEPTMASID=B.DEPARTMENT  JOIN ASPTBLSAMCATMAS S ON S.ASPTBLSAMCATMASID=B.CATEGORY  JOIN ASPTBLGGMAS T ON T.ASPTBLGGMASID=B.GAUGE   JOIN ASPTBLORDPACKMAS  U ON U.ASPTBLORDPACKMASID=B.ORDERPACKTYPE  JOIN ASPTBLCURMAS V ON V.ASPTBLCURMASID=B.CURRENCYNAME where   a.date2 between TO_DATE('" + dateTimePicker1.Value.ToString("dd-MM-yyyy") + "','dd-MM-yyyy') and TO_DATE('" + dateTimePicker2.Value.ToString("dd-MM-yyyy") + "','dd-MM-yyyy')  AND A.INWARD='T'  order by 1 ";
                DataSet ds0 = Utility.ExecuteSelectQuery(sel0, "ASPTBLBUYSAM");
                dt0 = ds0.Tables["ASPTBLBUYSAM"];
                path = "";
                path = dateTimePicker1.Value.ToString("dd-MM-yyyy");
                lblcount.Text = "CompCode : " + Class.Users.HCompcode + ",  Total  Row's: " + dt0.Rows.Count;
                if (dt0.Rows.Count > 0)
                {
                    rd.SetDataSource(dt0);
                    crystalReportViewer3.ReportSource = null; crystalReportViewer3.Refresh();
                    crystalReportViewer3.ReportSource = rd;
                   
                }
                else
                {
                    crystalReportViewer3.ReportSource = null; crystalReportViewer3.Refresh();
                }
                lblcount.Refresh();
                lblcount.Text = "CompCode : " + Class.Users.HCompcode + ",  Total  Qty " + dt0.Rows.Count;
            }
            if (combopacktype1.Text == "BYJAMA SET")
            {
                tabControl1.SelectedTab = tabPage3;
  
                string sel0 = "SELECT A.DATE2 AS DATE1,C.COMPCODE,C.compname,F.BRAND,B.AGFSAMPLE,G.SEASON || ' /' ||  B.MFYEAR || '' AS SEASON,R.DEPARTMENT || '(' ||  S.CATEGORY || ')' AS DEPARTMENT ,S.CATEGORY,B.STYLENAME,B.SUBSTYLE,B.FABRIC ,B.FABRICCONTENT,B.COUNTS, T.GG AS GAUGE,B.GSM, B.COLORNAME,U.ORDERPACKTYPE,B.SIZENAME,V.CURRENCYNAME,B.REMARKS,B.ACTIVE,B.RISK1,B.RISK2,B.RISK3,B.RISK4,B.RISK5,B.FABRICCOMPLIANT,B.REMARKS,B.MFYEAR  FROM ASPTBLBUYSAMINW A JOIN ASPTBLBUYSAM B ON A.AGFSAMPLE=B.AGFSAMPLE   JOIN GTCOMPMAST C ON C.GTCOMPMASTID=A.COMPCODE    JOIN ASPTBLRACKMAS D ON D.ASPTBLRACKMASID=A.RACK   JOIN ASPTBLBINMAS E ON E.ASPTBLBINMASID=A.BIN JOIN ASPTBLBRANDMAS F ON F.ASPTBLBRANDMASID=A.BRAND         JOIN ASPTBLSEASONMAS G ON G.ASPTBLSEASONMASID=A.SEASON   JOIN ASPTBLSAMTYPEMAS Q ON Q.ASPTBLSAMTYPEMASID=B.SAMPLETYPE JOIN ASPTBLSAMDEPTMAS R ON R.ASPTBLSAMDEPTMASID=B.DEPARTMENT  JOIN ASPTBLSAMCATMAS S ON S.ASPTBLSAMCATMASID=B.CATEGORY  JOIN ASPTBLGGMAS T ON T.ASPTBLGGMASID=B.GAUGE   JOIN ASPTBLORDPACKMAS  U ON U.ASPTBLORDPACKMASID=B.ORDERPACKTYPE  JOIN ASPTBLCURMAS V ON V.ASPTBLCURMASID=B.CURRENCYNAME where   a.date2 between TO_DATE('" + dateTimePicker1.Value.ToString("dd-MM-yyyy") + "','dd-MM-yyyy') and TO_DATE('" + dateTimePicker2.Value.ToString("dd-MM-yyyy") + "','dd-MM-yyyy')   AND U.ORDERPACKTYPE='BYJAMA SET'   order by a.ASPTBLBUYSAMINWID desc ";
                DataSet ds0 = Utility.ExecuteSelectQuery(sel0, "ASPTBLBUYSAM");
                dt0 = ds0.Tables["ASPTBLBUYSAM"];
                path = "";
                path = dateTimePicker1.Value.ToString("dd-MM-yyyy");
                lblcount.Text = "CompCode : " + Class.Users.HCompcode + ",  Total  Row's: " + dt0.Rows.Count;
                if (dt0.Rows.Count > 0)
                {
                    rd.SetDataSource(dt0);
                    crystalReportViewer3.ReportSource = null; crystalReportViewer3.Refresh();
                    crystalReportViewer3.ReportSource = rd;
                   
                }
                else
                {
                    crystalReportViewer3.ReportSource = null; crystalReportViewer3.Refresh();
                }
                lblcount.Refresh();
                lblcount.Text = "CompCode : " + Class.Users.HCompcode + ",  Total  Qty " + dt0.Rows.Count;
            }
            if (combopacktype1.Text == "PCS")
            {
                tabControl1.SelectedTab = tabPage3; 
                string sel0 = "SELECT A.DATE2 AS DATE1,C.COMPCODE,C.compname,F.BRAND,B.AGFSAMPLE,G.SEASON || ' /' ||  B.MFYEAR || '' AS SEASON,R.DEPARTMENT || '(' ||  S.CATEGORY || ')' AS DEPARTMENT ,S.CATEGORY,B.STYLENAME,B.SUBSTYLE,B.FABRIC ,B.FABRICCONTENT,B.COUNTS, T.GG AS GAUGE,B.GSM, B.COLORNAME,U.ORDERPACKTYPE,B.SIZENAME,V.CURRENCYNAME,B.REMARKS,B.ACTIVE,B.RISK1,B.RISK2,B.RISK3,B.RISK4,B.RISK5,B.FABRICCOMPLIANT,B.REMARKS,B.MFYEAR  FROM ASPTBLBUYSAMINW A JOIN ASPTBLBUYSAM B ON A.AGFSAMPLE=B.AGFSAMPLE   JOIN GTCOMPMAST C ON C.GTCOMPMASTID=A.COMPCODE    JOIN ASPTBLRACKMAS D ON D.ASPTBLRACKMASID=A.RACK   JOIN ASPTBLBINMAS E ON E.ASPTBLBINMASID=A.BIN JOIN ASPTBLBRANDMAS F ON F.ASPTBLBRANDMASID=A.BRAND         JOIN ASPTBLSEASONMAS G ON G.ASPTBLSEASONMASID=A.SEASON   JOIN ASPTBLSAMTYPEMAS Q ON Q.ASPTBLSAMTYPEMASID=B.SAMPLETYPE JOIN ASPTBLSAMDEPTMAS R ON R.ASPTBLSAMDEPTMASID=B.DEPARTMENT  JOIN ASPTBLSAMCATMAS S ON S.ASPTBLSAMCATMASID=B.CATEGORY  JOIN ASPTBLGGMAS T ON T.ASPTBLGGMASID=B.GAUGE   JOIN ASPTBLORDPACKMAS  U ON U.ASPTBLORDPACKMASID=B.ORDERPACKTYPE  JOIN ASPTBLCURMAS V ON V.ASPTBLCURMASID=B.CURRENCYNAME where   a.date2 between TO_DATE('" + dateTimePicker1.Value.ToString("dd-MM-yyyy") + "','dd-MM-yyyy') and TO_DATE('" + dateTimePicker2.Value.ToString("dd-MM-yyyy") + "','dd-MM-yyyy')   AND U.ORDERPACKTYPE='PCS'   order by a.ASPTBLBUYSAMINWID desc ";
                DataSet ds0 = Utility.ExecuteSelectQuery(sel0, "ASPTBLBUYSAM");
                dt0 = ds0.Tables["ASPTBLBUYSAM"];
                path = "";
                path = dateTimePicker1.Value.ToString("dd-MM-yyyy");
                lblcount.Text = "CompCode : " + Class.Users.HCompcode + ",  Total  Row's: " + dt0.Rows.Count;
                if (dt0.Rows.Count > 0)
                {
                    rd.SetDataSource(dt0);
                    crystalReportViewer3.ReportSource = null; crystalReportViewer3.Refresh();
                    crystalReportViewer3.ReportSource = rd;
                  
                }
                else
                {
                    crystalReportViewer3.ReportSource = null; crystalReportViewer3.Refresh();
                }
                lblcount.Refresh();
                lblcount.Text = "CompCode : " + Class.Users.HCompcode + ",  Total  Qty " + dt0.Rows.Count;
            }
            if (combopacktype1.Text == "SET")
            {
                tabControl1.SelectedTab = tabPage3; 
                string sel0 = "SELECT A.DATE2 AS DATE1,C.COMPCODE,C.compname,F.BRAND,B.AGFSAMPLE,G.SEASON || ' /' ||  B.MFYEAR || '' AS SEASON,R.DEPARTMENT || '(' ||  S.CATEGORY || ')' AS DEPARTMENT ,S.CATEGORY,B.STYLENAME,B.SUBSTYLE,B.FABRIC ,B.FABRICCONTENT,B.COUNTS, T.GG AS GAUGE,B.GSM, B.COLORNAME,U.ORDERPACKTYPE,B.SIZENAME,V.CURRENCYNAME,B.REMARKS,B.ACTIVE,B.RISK1,B.RISK2,B.RISK3,B.RISK4,B.RISK5,B.FABRICCOMPLIANT,B.REMARKS,B.MFYEAR  FROM ASPTBLBUYSAMINW A JOIN ASPTBLBUYSAM B ON A.AGFSAMPLE=B.AGFSAMPLE   JOIN GTCOMPMAST C ON C.GTCOMPMASTID=A.COMPCODE    JOIN ASPTBLRACKMAS D ON D.ASPTBLRACKMASID=A.RACK   JOIN ASPTBLBINMAS E ON E.ASPTBLBINMASID=A.BIN JOIN ASPTBLBRANDMAS F ON F.ASPTBLBRANDMASID=A.BRAND         JOIN ASPTBLSEASONMAS G ON G.ASPTBLSEASONMASID=A.SEASON   JOIN ASPTBLSAMTYPEMAS Q ON Q.ASPTBLSAMTYPEMASID=B.SAMPLETYPE JOIN ASPTBLSAMDEPTMAS R ON R.ASPTBLSAMDEPTMASID=B.DEPARTMENT  JOIN ASPTBLSAMCATMAS S ON S.ASPTBLSAMCATMASID=B.CATEGORY  JOIN ASPTBLGGMAS T ON T.ASPTBLGGMASID=B.GAUGE   JOIN ASPTBLORDPACKMAS  U ON U.ASPTBLORDPACKMASID=B.ORDERPACKTYPE  JOIN ASPTBLCURMAS V ON V.ASPTBLCURMASID=B.CURRENCYNAME where   a.date2 between TO_DATE('" + dateTimePicker1.Value.ToString("dd-MM-yyyy") + "','dd-MM-yyyy') and TO_DATE('" + dateTimePicker2.Value.ToString("dd-MM-yyyy") + "','dd-MM-yyyy')   AND U.ORDERPACKTYPE='SET'   order by a.ASPTBLBUYSAMINWID desc ";
                DataSet ds0 = Utility.ExecuteSelectQuery(sel0, "ASPTBLBUYSAM");
                dt0 = ds0.Tables["ASPTBLBUYSAM"];
                path = "";
                path = dateTimePicker1.Value.ToString("dd-MM-yyyy");
                lblcount.Text = "CompCode : " + Class.Users.HCompcode + ",  Total  Row's: " + dt0.Rows.Count;
                if (dt0.Rows.Count > 0)
                {
                    rd.SetDataSource(dt0);
                    crystalReportViewer3.ReportSource = null; crystalReportViewer3.Refresh();
                    crystalReportViewer3.ReportSource = rd;
                   
                }
                else
                {
                    crystalReportViewer3.ReportSource = null; crystalReportViewer3.Refresh();
                }
                lblcount.Refresh();
                lblcount.Text = "CompCode : " + Class.Users.HCompcode + ",  Total  Qty " + dt0.Rows.Count;
            }
            if (combopacktype1.Text == "ALL")
            {
                tabControl1.SelectedTab = tabPage8; butdatewise.Refresh();
                butdatewise.Text = "Click " + combopacktype1.Text;
                string sel0 = "SELECT A.DATE2 AS DATE1,C.COMPCODE,C.compname,F.BRAND,B.AGFSAMPLE,G.SEASON || ' /' ||  B.MFYEAR || '' AS SEASON,R.DEPARTMENT || '(' ||  S.CATEGORY || ')' AS DEPARTMENT ,S.CATEGORY,B.STYLENAME,B.SUBSTYLE,B.FABRIC ,B.FABRICCONTENT,B.COUNTS, T.GG AS GAUGE,B.GSM, B.COLORNAME,U.ORDERPACKTYPE,B.SIZENAME,V.CURRENCYNAME,B.REMARKS,B.ACTIVE,B.RISK1,B.RISK2,B.RISK3,B.RISK4,B.RISK5,B.FABRICCOMPLIANT,B.REMARKS,B.MFYEAR  FROM ASPTBLBUYSAMINW A JOIN ASPTBLBUYSAM B ON A.AGFSAMPLE=B.AGFSAMPLE   JOIN GTCOMPMAST C ON C.GTCOMPMASTID=A.COMPCODE    JOIN ASPTBLRACKMAS D ON D.ASPTBLRACKMASID=A.RACK   JOIN ASPTBLBINMAS E ON E.ASPTBLBINMASID=A.BIN JOIN ASPTBLBRANDMAS F ON F.ASPTBLBRANDMASID=A.BRAND         JOIN ASPTBLSEASONMAS G ON G.ASPTBLSEASONMASID=A.SEASON   JOIN ASPTBLSAMTYPEMAS Q ON Q.ASPTBLSAMTYPEMASID=B.SAMPLETYPE JOIN ASPTBLSAMDEPTMAS R ON R.ASPTBLSAMDEPTMASID=B.DEPARTMENT  JOIN ASPTBLSAMCATMAS S ON S.ASPTBLSAMCATMASID=B.CATEGORY  JOIN ASPTBLGGMAS T ON T.ASPTBLGGMASID=B.GAUGE   JOIN ASPTBLORDPACKMAS  U ON U.ASPTBLORDPACKMASID=B.ORDERPACKTYPE  JOIN ASPTBLCURMAS V ON V.ASPTBLCURMASID=B.CURRENCYNAME where   a.date2 between TO_DATE('" + dateTimePicker1.Value.ToString("dd-MM-yyyy") + "','dd-MM-yyyy') and TO_DATE('" + dateTimePicker2.Value.ToString("dd-MM-yyyy") + "','dd-MM-yyyy')      order by 1 ";
                DataSet ds0 = Utility.ExecuteSelectQuery(sel0, "ASPTBLBUYSAM");
                dt0 = ds0.Tables["ASPTBLBUYSAM"];
                path = "";
                path = dateTimePicker1.Value.ToString("dd-MM-yyyy");
                lblcount.Text = "CompCode : " + Class.Users.HCompcode + ",  Total  Row's: " + dt0.Rows.Count;
                if (dt0.Rows.Count > 0)
                {
                    rd.SetDataSource(dt0);
                    crystalReportViewer8.ReportSource = null; crystalReportViewer8.Refresh();
                    crystalReportViewer8.ReportSource = rd;
                   
                }
                else
                {
                    crystalReportViewer8.ReportSource = null; crystalReportViewer8.Refresh();
                }
                lblcount.Refresh();
                lblcount.Text = "CompCode : " + Class.Users.HCompcode + ",  Total  Qty " + dt0.Rows.Count;
            }
        
            if (dt0 == null || dt0.Rows.Count <= 0)
            {
             
                crystalReportViewer1.ReportSource = null; crystalReportViewer1.Refresh();
            }
            
        }

        private void combogsm_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

      


        private void stockRefreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            stockreport(); tabControl1.SelectTab("tabPage1");
        }

        private void combocategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string sel0 = "SELECT A.ASPTBLBUYSAMID,  A.DATE1,B.COMPCODE,b.compname,C.BRAND,A.AGFSAMPLE,E.SEASON || ' /' ||  A.MFYEAR || '' AS SEASON,F.DEPARTMENT || '(' ||  O.CATEGORY || ')' AS DEPARTMENT ,O.CATEGORY,A.STYLENAME,A.SUBSTYLE,A.FABRIC,A.FABRICCONTENT,A.COUNTS, K.GG AS GAUGE,A.GSM, A.COLORNAME,P.ORDERPACKTYPE,A.SIZENAME,N.CURRENCYNAME,A.REMARKS,A.ACTIVE,A.RISK1,A.RISK2,A.RISK3,A.RISK4,A.RISK5,A.FABRICCOMPLIANT,A.REMARKS,A.MFYEAR    FROM ASPTBLBUYSAM A JOIN GTCOMPMAST B ON A.COMPCODE=B.GTCOMPMASTID join asptblBRANDMAS C ON C.ASPTBLBRANDMASID=A.BRAND  JOIN ASPTBLSEASONMAS E ON E.ASPTBLSEASONMASID=A.SEASON JOIN ASPTBLSAMDEPTMAS F ON F.ASPTBLSAMDEPTMASID=A.DEPARTMENT   JOIN ASPTBLGGMAS K ON K.ASPTBLGGMASID=A.GAUGE      JOIN ASPTBLCURMAS N ON N.ASPTBLCURMASID=A.CURRENCYNAME JOIN ASPTBLSAMCATMAS O ON O.ASPTBLSAMCATMASID=A.CATEGORY  JOIN ASPTBLORDPACKMAS  P ON P.ASPTBLORDPACKMASID=A.ORDERPACKTYPE where  a.active='T' AND a.date2 between TO_DATE('" + dateTimePicker1.Value.ToString("dd-MM-yyyy") + "','dd-MM-yyyy') and TO_DATE('" + dateTimePicker2.Value.ToString("dd-MM-yyyy") + "','dd-MM-yyyy')  AND   C.BRAND='" + combobrand.Text + "' AND   F.DEPARTMENT='" + combodept.Text + "' AND   O.CATEGORY='" + combocategory.Text + "'  order by a.ASPTBLBUYSAMID desc ";
            //DataSet ds0 = Utility.ExecuteSelectQuery(sel0, "ASPTBLBUYSAM");
            //DataTable dt0 = ds0.Tables["ASPTBLBUYSAM"];
            //lblcount.Text = "Category Name : " + combocategory.Text + ",  Total  Row's: " + dt0.Rows.Count;
            //path = "";
            //path = "Brand/Deparment,Category " + combocategory.Text + "CompCode  ";
            //rd.SetDataSource(dt0);
            //crystalReportViewer7.ReportSource = null;
            //crystalReportViewer7.ReportSource = rd;
            //crystalReportViewer7.Refresh();

        }

        private void butbranddeptcate_Click(object sender, EventArgs e)
        {
            DataTable dt0 = new DataTable();
          
           
        }

        private void butbrandstyle_Click(object sender, EventArgs e)
        {
            DataTable dt0 = new DataTable(); crystalReportViewer6.ReportSource = null; crystalReportViewer6.Refresh();
            tabControl1.SelectedTab = tabPage6;
            //if (combostyle.Text == "ALL")
            //{
            //    string sel0 = "select  DISTINCT A.AGFSAMPLE , B.DATE1,B.COMPCODE,F.BRAND,G.SEASON ,K.CATEGORY,A.STYLENAME,A.FABRIC ,A.FABRICCONTENT,A.COUNTS, I.GG AS GAUGE,A.GSM, A.COLORNAME,B.ORDERPACKTYPE,A.SIZENAME,J.CURRENCYNAME, B.REMARKS,B.ACTIVE,B.RISK1,B.RISK2,B.RISK3,B.RISK4,B.RISK5,B.FABRICCOMPLIANT,B.REMARKS,B.MFYEAR,'' RACK,'' BIN  from asptblbuysaminw a  LEFT join  asptblbuysam b on a.ASPTBLBUYSAMID=b.ASPTBLBUYSAMID AND A.AGFSAMPLE=B.AGFSAMPLE  AND A.BRAND=B.BRAND   AND A.SEASON=B.SEASON AND A.DEPARTMENT=B.DEPARTMENT  AND A.CATEGORY=B.CATEGORY   AND A.STYLENAME=B.STYLENAME   AND A.SIZENAME=B.SIZENAME    AND A.COUNTS=B.COUNTS AND A.COLORNAME=B.COLORNAME   AND A.FABRIC=B.FABRIC AND A.GSM=B.GSM   JOIN ASPTBLRACKMAS C ON C.ASPTBLRACKMASID=A.RACK  JOIN ASPTBLBINMAS D ON D.ASPTBLBINMASID=A.BIN   JOIN ASPTBLBRANDMAS E ON E.ASPTBLBRANDMASID=A.BRAND AND E.ASPTBLBRANDMASID=B.BRAND    JOIN ASPTBLBRANDMAS F ON F.ASPTBLBRANDMASID = B.BRAND  AND F.ASPTBLBRANDMASID = A.BRAND    JOIN ASPTBLSEASONMAS G ON G.ASPTBLSEASONMASID=A.SEASON AND G.ASPTBLSEASONMASID=B.SEASON     JOIN ASPTBLSAMDEPTMAS H ON H.ASPTBLSAMDEPTMASID=B.DEPARTMENT     AND H.ASPTBLSAMDEPTMASID=A.DEPARTMENT   JOIN ASPTBLGGMAS I ON I.ASPTBLGGMASID=B.GAUGE   AND I.ASPTBLGGMASID=A.GAUGE    JOIN ASPTBLCURMAS J ON J.ASPTBLCURMASID=B.CURRENCYNAME     JOIN ASPTBLSAMCATMAS K ON K.ASPTBLSAMCATMASID=B.CATEGORY  AND K.ASPTBLSAMCATMASID=A.CATEGORY    WHERE A.OUTWARD = 'F' AND A.INWARD = 'T'      ORDER BY A.AGFSAMPLE ";
            //    DataSet ds0 = Utility.ExecuteSelectQuery(sel0, "ASPTBLBUYSAM");
            //    dt0 = ds0.Tables["ASPTBLBUYSAM"];
            //    rd.SetDataSource(dt0);
            //    crystalReportViewer6.ReportSource = null;
            //    crystalReportViewer6.ReportSource = rd;
            //    crystalReportViewer6.Refresh();
            //    lblcount.Text = "Total  Row's: " + dt0.Rows.Count;
            //    path = "";
            //    path = "Style " + combostyle.Text + "CompCode  ";
            //}
            //if (combocompcode.Text != "ALL" && combostyle.Text != "ALL")
            //{
                //string sel0 = "SELECT A.ASPTBLBUYSAMID,  A.DATE1,B.COMPCODE,b.compname,C.BRAND,A.AGFSAMPLE,E.SEASON || ' /' ||  A.MFYEAR || '' AS SEASON,F.DEPARTMENT || '(' ||  O.CATEGORY || ')' AS DEPARTMENT ,Q.SAMPLETYPE CATEGORY,A.STYLENAME,A.SUBSTYLE,A.FABRIC ,A.FABRICCONTENT,A.FABRICCONTENT,A.COUNTS, K.GG AS GAUGE,A.GSM, A.COLORNAME,P.ORDERPACKTYPE,A.SIZENAME,N.CURRENCYNAME,A.REMARKS,A.ACTIVE,A.RISK1,A.RISK2,A.RISK3,A.RISK4,A.RISK5,A.FABRICCOMPLIANT,A.REMARKS,A.MFYEAR    FROM ASPTBLBUYSAM A JOIN GTCOMPMAST B ON A.COMPCODE=B.GTCOMPMASTID join asptblBRANDMAS C ON C.ASPTBLBRANDMASID=A.BRAND  JOIN ASPTBLSEASONMAS E ON E.ASPTBLSEASONMASID=A.SEASON JOIN ASPTBLSAMDEPTMAS F ON F.ASPTBLSAMDEPTMASID=A.DEPARTMENT   JOIN ASPTBLGGMAS K ON K.ASPTBLGGMASID=A.GAUGE      JOIN ASPTBLCURMAS N ON N.ASPTBLCURMASID=A.CURRENCYNAME JOIN ASPTBLSAMCATMAS O ON O.ASPTBLSAMCATMASID=A.CATEGORY  JOIN ASPTBLORDPACKMAS  P ON P.ASPTBLORDPACKMASID=A.ORDERPACKTYPE   join asptblsamtypemas q on q.asptblsamtypemasid=A.SAMPLETYPE   where A.INWARD='T'  AND  B.COMPCODE='" + combocompcode.Text + "' AND  a.stylename='" + combostyle.Text + "'  ORDER by a.ASPTBLBUYSAMID desc ";
                string sel0 = "select  DISTINCT A.AGFSAMPLE , B.DATE1,B.COMPCODE,F.BRAND,G.SEASON ,K.CATEGORY,A.STYLENAME,A.FABRIC ,A.FABRICCONTENT,A.COUNTS, I.GG AS GAUGE,A.GSM, A.COLORNAME,B.ORDERPACKTYPE,A.SIZENAME,J.CURRENCYNAME, B.REMARKS,B.ACTIVE,B.RISK1,B.RISK2,B.RISK3,B.RISK4,B.RISK5,B.FABRICCOMPLIANT,B.REMARKS,B.MFYEAR,'' RACK, ''BIN  from asptblbuysaminw a  LEFT join  asptblbuysam b on a.ASPTBLBUYSAMID=b.ASPTBLBUYSAMID AND A.AGFSAMPLE=B.AGFSAMPLE  AND A.BRAND=B.BRAND   AND A.SEASON=B.SEASON AND A.DEPARTMENT=B.DEPARTMENT  AND A.CATEGORY=B.CATEGORY   AND A.STYLENAME=B.STYLENAME   AND A.SIZENAME=B.SIZENAME    AND A.COUNTS=B.COUNTS AND A.COLORNAME=B.COLORNAME   AND A.FABRIC=B.FABRIC AND A.GSM=B.GSM   JOIN ASPTBLRACKMAS C ON C.ASPTBLRACKMASID=A.RACK  JOIN ASPTBLBINMAS D ON D.ASPTBLBINMASID=A.BIN   JOIN ASPTBLBRANDMAS E ON E.ASPTBLBRANDMASID=A.BRAND AND E.ASPTBLBRANDMASID=B.BRAND    JOIN ASPTBLBRANDMAS F ON F.ASPTBLBRANDMASID = B.BRAND  AND F.ASPTBLBRANDMASID = A.BRAND    JOIN ASPTBLSEASONMAS G ON G.ASPTBLSEASONMASID=A.SEASON AND G.ASPTBLSEASONMASID=B.SEASON     JOIN ASPTBLSAMDEPTMAS H ON H.ASPTBLSAMDEPTMASID=B.DEPARTMENT     AND H.ASPTBLSAMDEPTMASID=A.DEPARTMENT   JOIN ASPTBLGGMAS I ON I.ASPTBLGGMASID=B.GAUGE   AND I.ASPTBLGGMASID=A.GAUGE    JOIN ASPTBLCURMAS J ON J.ASPTBLCURMASID=B.CURRENCYNAME     JOIN ASPTBLSAMCATMAS K ON K.ASPTBLSAMCATMASID=B.CATEGORY  AND K.ASPTBLSAMCATMASID=A.CATEGORY    WHERE   a.stylename='" + combostyle.Text + "'    ORDER BY A.AGFSAMPLE ";

                DataSet ds0 = Utility.ExecuteSelectQuery(sel0, "ASPTBLBUYSAM");
                dt0 = ds0.Tables["ASPTBLBUYSAM"];
                rd.SetDataSource(dt0);
                crystalReportViewer6.ReportSource = null;
                crystalReportViewer6.ReportSource = rd;
                crystalReportViewer6.Refresh();
                lblcount.Text = "Style Name : " + combostyle.Text + ",  Total  Row's: " + dt0.Rows.Count;
                path = "";
                path = "Style " + combostyle.Text + "CompCode  ";
            //}
            if (dt0.Rows.Count < 0)
            {
                MessageBox.Show("pls select Brand and Style");
                crystalReportViewer6.ReportSource = null;
            }

        }

        public void Searchs(int id)
        {
            throw new NotImplementedException();
        }

        public void Deletes(int id)
        {
            throw new NotImplementedException();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable dt0 = new DataTable();
            if (combosampletype.Text != "ALL")
            {
                tabControl1.SelectedTab = tabPage10;

                string sel0 = "select distinct  A.AGFSAMPLE ,k.cOMPCODE, a.DATE1,b.BRAND,d.SEASON , e.DEPARTMENT, h.CATEGORY,c.STYLENAME,A.FABRIC ,A.FABRICCONTENT,A.COUNTS, f.GG AS GAUGE,A.GSM, A.COLORNAME,i.ORDERPACKTYPE,A.SIZENAME,g.CURRENCYNAME, a.REMARKS,a.ACTIVE,a.RISK1,a.RISK2,a.RISK3,a.RISK4,a.RISK5,a.FABRICCOMPLIANT,a.MFYEAR   from    asptblbuysam A  JOIN ASPTBLBRANDMAS b ON b.ASPTBLBRANDMASID=A.BRAND JOIN ASPTBLSTYMAS c ON c.STYLENAME=A.STYLENAME  JOIN ASPTBLSEASONMAS d ON d.ASPTBLSEASONMASID=A.SEASON  JOIN ASPTBLSAMDEPTMAS e ON e.ASPTBLSAMDEPTMASID=a.DEPARTMENT   JOIN ASPTBLGGMAS f ON f.ASPTBLGGMASID=a.GAUGE   JOIN ASPTBLCURMAS g ON g.ASPTBLCURMASID=a.CURRENCYNAME    JOIN ASPTBLSAMCATMAS h ON h.ASPTBLSAMCATMASID=a.CATEGORY join ASPTBLORDPACKMAS i on i.ASPTBLORDPACKMASID=a.ORDERPACKTYPE join asptblsamtypemas j on j.ASPTBLSAMTYPEMASID=a.SAMPLETYPE  join gtcompmast k on k.GTCOMPMASTID=a.COMPCODE  WHERE   A.INWARD='T'  and    j.sampletype='" + combosampletype.Text + "'   order by a.agfsample desc ";
                DataSet ds0 = Utility.ExecuteSelectQuery(sel0, "ASPTBLBUYSAM");
                dt0 = ds0.Tables["ASPTBLBUYSAM"];
                if (dt0.Rows.Count > 0)
                {
                    lblcount.Text = "Total  Row's: " + dt0.Rows.Count;
                    path = "";
                    path = "SampleType " + combocompcode.Text + "CompCode  ";
                    rd.SetDataSource(dt0);
                    crystalReportViewer10.ReportSource = null;
                    crystalReportViewer10.ReportSource = rd;
                    crystalReportViewer10.Refresh();
                }
                else
                {
                    crystalReportViewer10.ReportSource = null;
                    MessageBox.Show("No Data Found");
                }
            }
            if (combosampletype.Text == "ALL")
            {
                tabControl1.SelectedTab = tabPage10;

                string sel0 = "select distinct  A.AGFSAMPLE ,k.cOMPCODE, a.DATE1,b.BRAND,d.SEASON , e.DEPARTMENT, h.CATEGORY,c.STYLENAME,A.FABRIC ,A.FABRICCONTENT,A.COUNTS, f.GG AS GAUGE,A.GSM, A.COLORNAME,i.ORDERPACKTYPE,A.SIZENAME,g.CURRENCYNAME, a.REMARKS,a.ACTIVE,a.RISK1,a.RISK2,a.RISK3,a.RISK4,a.RISK5,a.FABRICCOMPLIANT,a.MFYEAR   from    asptblbuysam A  JOIN ASPTBLBRANDMAS b ON b.ASPTBLBRANDMASID=A.BRAND JOIN ASPTBLSTYMAS c ON c.STYLENAME=A.STYLENAME  JOIN ASPTBLSEASONMAS d ON d.ASPTBLSEASONMASID=A.SEASON  JOIN ASPTBLSAMDEPTMAS e ON e.ASPTBLSAMDEPTMASID=a.DEPARTMENT   JOIN ASPTBLGGMAS f ON f.ASPTBLGGMASID=a.GAUGE   JOIN ASPTBLCURMAS g ON g.ASPTBLCURMASID=a.CURRENCYNAME    JOIN ASPTBLSAMCATMAS h ON h.ASPTBLSAMCATMASID=a.CATEGORY join ASPTBLORDPACKMAS i on i.ASPTBLORDPACKMASID=a.ORDERPACKTYPE join asptblsamtypemas j on j.ASPTBLSAMTYPEMASID=a.SAMPLETYPE  join gtcompmast k on k.GTCOMPMASTID=a.COMPCODE  WHERE   A.INWARD='T'   ORDER BY A.AGFSAMPLE DESC";
                DataSet ds0 = Utility.ExecuteSelectQuery(sel0, "ASPTBLBUYSAM");
                dt0 = ds0.Tables["ASPTBLBUYSAM"];
                if (dt0.Rows.Count > 0)
                {
                    lblcount.Text = "Total  Row's: " + dt0.Rows.Count;
                    path = "";
                    path = "SampleType " + combocompcode.Text + "CompCode  ";
                    rd.SetDataSource(dt0);
                    crystalReportViewer10.ReportSource = null;
                    crystalReportViewer10.ReportSource = rd;
                    crystalReportViewer10.Refresh();
                }
                else
                {
                    crystalReportViewer10.ReportSource = null;
                    MessageBox.Show("No Data Found");
                }
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataTable dt0;
            if (comboseason.Text == "ALL")
            {

                tabControl1.SelectedTab = tabPage11;
                string sel0 = "";
                 sel0 = "select distinct  A.AGFSAMPLE ,k.cOMPCODE, a.DATE1,b.BRAND,d.SEASON , e.DEPARTMENT, h.CATEGORY,c.STYLENAME,A.FABRIC ,A.FABRICCONTENT,A.COUNTS, f.GG AS GAUGE,A.GSM, A.COLORNAME,i.ORDERPACKTYPE,A.SIZENAME,g.CURRENCYNAME, a.REMARKS,a.ACTIVE,a.RISK1,a.RISK2,a.RISK3,a.RISK4,a.RISK5,a.FABRICCOMPLIANT,a.MFYEAR   from    asptblbuysam A  JOIN ASPTBLBRANDMAS b ON b.ASPTBLBRANDMASID=A.BRAND JOIN ASPTBLSTYMAS c ON c.STYLENAME=A.STYLENAME  JOIN ASPTBLSEASONMAS d ON d.ASPTBLSEASONMASID=A.SEASON  JOIN ASPTBLSAMDEPTMAS e ON e.ASPTBLSAMDEPTMASID=a.DEPARTMENT   JOIN ASPTBLGGMAS f ON f.ASPTBLGGMASID=a.GAUGE   JOIN ASPTBLCURMAS g ON g.ASPTBLCURMASID=a.CURRENCYNAME    JOIN ASPTBLSAMCATMAS h ON h.ASPTBLSAMCATMASID=a.CATEGORY join ASPTBLORDPACKMAS i on i.ASPTBLORDPACKMASID=a.ORDERPACKTYPE join asptblsamtypemas j on j.ASPTBLSAMTYPEMASID=a.SAMPLETYPE  join gtcompmast k on k.GTCOMPMASTID=a.COMPCODE  WHERE   A.INWARD='T'   ORDER BY A.AGFSAMPLE DESC ";

                // string sel0 = "SELECT A.ASPTBLBUYSAMID,  A.DATE1,B.COMPCODE,b.compname,C.BRAND,A.AGFSAMPLE,E.SEASON || ' /' ||  A.MFYEAR || '' AS SEASON,F.DEPARTMENT || '(' ||  O.CATEGORY || ')' AS DEPARTMENT ,q.SAMPLETYPE CATEGORY,A.STYLENAME,A.SUBSTYLE,A.FABRIC ,A.FABRICCONTENT,A.FABRICCONTENT,A.COUNTS, K.GG AS GAUGE,A.GSM, A.COLORNAME,P.ORDERPACKTYPE,A.SIZENAME,N.CURRENCYNAME,A.REMARKS,A.ACTIVE,A.RISK1,A.RISK2,A.RISK3,A.RISK4,A.RISK5,A.FABRICCOMPLIANT,A.REMARKS,A.MFYEAR    FROM ASPTBLBUYSAM A JOIN GTCOMPMAST B ON A.COMPCODE=B.GTCOMPMASTID join asptblBRANDMAS C ON C.ASPTBLBRANDMASID=A.BRAND  JOIN ASPTBLSEASONMAS E ON E.ASPTBLSEASONMASID=A.SEASON JOIN ASPTBLSAMDEPTMAS F ON F.ASPTBLSAMDEPTMASID=A.DEPARTMENT   JOIN ASPTBLGGMAS K ON K.ASPTBLGGMASID=A.GAUGE      JOIN ASPTBLCURMAS N ON N.ASPTBLCURMASID=A.CURRENCYNAME JOIN ASPTBLSAMCATMAS O ON O.ASPTBLSAMCATMASID=A.CATEGORY  JOIN ASPTBLORDPACKMAS  P ON P.ASPTBLORDPACKMASID=A.ORDERPACKTYPE join asptblsamtypemas q on q.asptblsamtypemasid=A.SAMPLETYPE    where A.INWARD='T'  ORDER by a.ASPTBLBUYSAMID desc ";
                DataSet ds0 = Utility.ExecuteSelectQuery(sel0, "ASPTBLBUYSAM");
                dt0 = ds0.Tables["ASPTBLBUYSAM"];
                if (dt0.Rows.Count > 0)
                {

                    rd.SetDataSource(dt0);
                    crystalReportViewer11.ReportSource = null;
                    crystalReportViewer11.ReportSource = rd;
                    crystalReportViewer11.Refresh();
                    lblcount.Text = "Season : " + comboseason.Text + ",  Total  Row's: " + dt0.Rows.Count;
                    path = "";
                    path = "Season " + comboseason.Text + "CompCode  ";
                }
                if (dt0.Rows.Count < 0)
                {
                    MessageBox.Show("pls select Season");
                    crystalReportViewer11.ReportSource = null;
                }
            }
            if (comboseason.Text != "ALL")
            {
                tabControl1.SelectedTab = tabPage11;
                string sel0 = "select distinct  A.AGFSAMPLE ,k.cOMPCODE, a.DATE1,b.BRAND,d.SEASON , e.DEPARTMENT, h.CATEGORY,c.STYLENAME,A.FABRIC ,A.FABRICCONTENT,A.COUNTS, f.GG AS GAUGE,A.GSM, A.COLORNAME,i.ORDERPACKTYPE,A.SIZENAME,g.CURRENCYNAME, a.REMARKS,a.ACTIVE,a.RISK1,a.RISK2,a.RISK3,a.RISK4,a.RISK5,a.FABRICCOMPLIANT,a.MFYEAR   from    asptblbuysam A  JOIN ASPTBLBRANDMAS b ON b.ASPTBLBRANDMASID=A.BRAND JOIN ASPTBLSTYMAS c ON c.STYLENAME=A.STYLENAME  JOIN ASPTBLSEASONMAS d ON d.ASPTBLSEASONMASID=A.SEASON  JOIN ASPTBLSAMDEPTMAS e ON e.ASPTBLSAMDEPTMASID=a.DEPARTMENT   JOIN ASPTBLGGMAS f ON f.ASPTBLGGMASID=a.GAUGE   JOIN ASPTBLCURMAS g ON g.ASPTBLCURMASID=a.CURRENCYNAME    JOIN ASPTBLSAMCATMAS h ON h.ASPTBLSAMCATMASID=a.CATEGORY join ASPTBLORDPACKMAS i on i.ASPTBLORDPACKMASID=a.ORDERPACKTYPE join asptblsamtypemas j on j.ASPTBLSAMTYPEMASID=a.SAMPLETYPE  join gtcompmast k on k.GTCOMPMASTID=a.COMPCODE  WHERE   A.INWARD='T'  and   d.season='" + comboseason.Text + "'      ORDER BY A.AGFSAMPLE DESC";

                // string sel0 = "SELECT A.ASPTBLBUYSAMID,  A.DATE1,B.COMPCODE,b.compname,C.BRAND,A.AGFSAMPLE,E.SEASON || ' /' ||  A.MFYEAR || '' AS SEASON,F.DEPARTMENT || '(' ||  O.CATEGORY || ')' AS DEPARTMENT ,q.SAMPLETYPE CATEGORY,A.STYLENAME,A.SUBSTYLE,A.FABRIC ,A.FABRICCONTENT,A.FABRICCONTENT,A.COUNTS, K.GG AS GAUGE,A.GSM, A.COLORNAME,P.ORDERPACKTYPE,A.SIZENAME,N.CURRENCYNAME,A.REMARKS,A.ACTIVE,A.RISK1,A.RISK2,A.RISK3,A.RISK4,A.RISK5,A.FABRICCOMPLIANT,A.REMARKS,A.MFYEAR    FROM ASPTBLBUYSAM A JOIN GTCOMPMAST B ON A.COMPCODE=B.GTCOMPMASTID join asptblBRANDMAS C ON C.ASPTBLBRANDMASID=A.BRAND  JOIN ASPTBLSEASONMAS E ON E.ASPTBLSEASONMASID=A.SEASON JOIN ASPTBLSAMDEPTMAS F ON F.ASPTBLSAMDEPTMASID=A.DEPARTMENT   JOIN ASPTBLGGMAS K ON K.ASPTBLGGMASID=A.GAUGE      JOIN ASPTBLCURMAS N ON N.ASPTBLCURMASID=A.CURRENCYNAME JOIN ASPTBLSAMCATMAS O ON O.ASPTBLSAMCATMASID=A.CATEGORY  JOIN ASPTBLORDPACKMAS  P ON P.ASPTBLORDPACKMASID=A.ORDERPACKTYPE join asptblsamtypemas q on q.asptblsamtypemasid=A.SAMPLETYPE    where A.INWARD='T' AND  C.BRAND='" + combobrand.Text + "' AND  e.season='" + comboseason.Text + "'  ORDER by a.ASPTBLBUYSAMID desc ";
                DataSet ds0 = Utility.ExecuteSelectQuery(sel0, "ASPTBLBUYSAM");
                dt0 = ds0.Tables["ASPTBLBUYSAM"];
                if (dt0.Rows.Count > 0)
                {
                    rd.SetDataSource(dt0);
                    crystalReportViewer11.ReportSource = null;
                    crystalReportViewer11.ReportSource = rd;
                    crystalReportViewer11.Refresh();
                    lblcount.Text = "Season : " + comboseason.Text + ",  Total  Row's: " + dt0.Rows.Count;
                    path = "";
                    path = "Season " + comboseason.Text + "CompCode  ";
                }
                if (dt0.Rows.Count < 0)
                {
                    MessageBox.Show("pls select Season");
                    crystalReportViewer11.ReportSource = null;
                }
            }
        }

        private void combocompcode_TextChanged(object sender, EventArgs e)
        {
            if (combocompcode.Text != "System.Data.DataRowView")
            {
                styleLoad(combocompcode.Text);
                SeasonLoad(combocompcode.Text);

            }
        }
    }
}
