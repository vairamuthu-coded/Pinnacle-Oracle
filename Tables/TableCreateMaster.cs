using System;
using System.Data;
using System.Windows.Forms;
using System.Configuration;
using Oracle.ManagedDataAccess.Client;
using Pinnacle.Models;

namespace Pinnacle.Tables
{
    public partial class butdroptable : Form
    {
        public butdroptable()
        {
            InitializeComponent();

        }
        private void Butcreate_Click(object sender, EventArgs e)
        {
            try
            {
                Class.Users.UserTime = 0;
                DialogResult result1 = MessageBox.Show("Do You want to Create Table in '" + Class.Users.COMPCODE + "'  Database??\n' ", "Lovely - MYSQL - DATABASE", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (result1.Equals(DialogResult.OK))
                {
                    string sel = "select  GTCOMPMASTID  from  " + Class.Users.ProjectID + ".GTCOMPMAST  where compcode='" + Class.Users.HCompcode + "'";
                    DataSet ds = Utility.ExecuteSelectQuery(sel, "GTCOMPMAST");
                    DataTable dt = ds.Tables["GTCOMPMAST"];
                    Class.Users.COMPCODE = Convert.ToInt64(dt.Rows[0]["GTCOMPMASTID"].ToString());
                    Class.Users.IPADDRESS = GenFun.GetLocalIPAddress();
                    if (Class.Users.COMPCODE > 1)
                    {
                        string sel1 = "select  asptblregistrationid  from  " + Class.Users.ProjectID + ".asptblregistration";
                        DataSet ds1 = Utility.ExecuteSelectQuery(sel1, "asptblregistration");
                        DataTable dt1 = ds1.Tables["asptblregistration"];
                        if (dt1 == null)
                        {
                            Cursor = Cursors.WaitCursor; Class.Users.UserTime = 0;
                            string tab = "";
                            tab = "" +
                            //"CREATE TABLE " + Class.Users.ProjectID + ".ASPTBLSESSIONMAS (   ASPTBLSESSIONMASID  INTEGER,   COMPCODE  INTEGER,   USERNAME INTEGER,   PASWORD VARCHAR2(100 BYTE),   SYSTEMDATE DATE,    OSUSER VARCHAR2(50 BYTE),SID INTEGER,SERIAL INTEGER )?" +
                            //"CREATE SEQUENCE " + Class.Users.ProjectID + ".ASPTBLSESSIONMASSEQ    START WITH 1   MAXVALUE 99999999   MINVALUE 1   NOCYCLE   NOCACHE   NOORDER?" +
                            //"CREATE OR REPLACE TRIGGER " + Class.Users.ProjectID + ".ASPTBLSESSIONMASTRI BEFORE INSERT ON ASPTBLSESSIONMAS REFERENCING NEW AS NEW OLD AS OLD FOR EACH ROW DECLARE ASPTBLSESSIONMASID INTEGER; BEGIN ASPTBLSESSIONMASID:= 0;   SELECT ASPTBLSESSIONMASSEQ.NEXTVAL INTO ASPTBLSESSIONMASID FROM DUAL;   :NEW.ASPTBLSESSIONMASID:= ASPTBLSESSIONMASID;END ASPTBLSESSIONMASTRI;?" +
                            //"CREATE TABLE " + Class.Users.ProjectID + ".ASPTBLUSERMAS(  USERID   INTEGER PRIMARY  KEY NOT NULL,  FINYEAR INTEGER,  COMPCODE   INTEGER,  EMPNO   VARCHAR2(50 BYTE),  DEPT INTEGER,  USERNAME   VARCHAR2(50 BYTE),  PASWORD VARCHAR2(50 BYTE),  ACTIVE  VARCHAR2(1 BYTE),  IPADDRES   VARCHAR2(50 BYTE),  CREATEDON  VARCHAR2(50 BYTE),  GATENAME   VARCHAR2(50 BYTE),  EMPNAME INTEGER,  NEWPASSWORD   VARCHAR2(100 BYTE),sessiontime VARCHAR2(10 BYTE) )?" +
                            //"CREATE SEQUENCE " + Class.Users.ProjectID + ".ASPTBLUSERMASSEQ START WITH 1  MAXVALUE 9999999999999  MINVALUE 0 NOCYCLE NOCACHE   NOORDER?" +
                            //"CREATE OR REPLACE TRIGGER " + Class.Users.ProjectID + ".ASPTBLUSERMASTRI BEFORE INSERT ON asptblusermas REFERENCING NEW AS NEW OLD AS OLD FOR EACH ROW DECLARE USERID  INTEGER; BEGIN USERID:= 0; SELECT " + Class.Users.ProjectID + ".asptblusermasSEQ.NEXTVAL INTO USERID FROM DUAL;:NEW.USERID:= USERID; END asptblusermasTRI;?" +
                            // "INSERT Into " + Class.Users.ProjectID + ".ASPTBLUSERMAS (FINYEAR,  COMPCODE,  EMPNAME ,  DEPT ,  USERNAME ,GATENAME,  PASWORD ,  ACTIVE ,  IPADDRES,Createdon,sessiontime)VALUES('5000000000001','" + Class.Users.COMPCODE + "' ,'1','1','ADMIN','21-22/GJI/','kkQ25us9uHw=','T','" + Class.Users.IPADDRESS + "','28/01/2021 11:13:18','1800')? " +
                            //"CREATE TABLE " + Class.Users.ProjectID + ".ASPTBLMENUNAME(MENUNAMEID  INTEGER  primary key not null,  MENUNAME VARCHAR2(50 BYTE),  ACTIVE   VARCHAR2(5 BYTE),  PARENTMENUID   INTEGER,  CREATEON VARCHAR2(50 BYTE),  CREATEBY VARCHAR2(50 BYTE),  MODIFYON VARCHAR2(50 BYTE),  IPADDRESS   VARCHAR2(50 BYTE))?" +
                            //"CREATE SEQUENCE " + Class.Users.ProjectID + ".ASPTBLMENUNAMESEQ START WITH 1  MAXVALUE 9999999999999  MINVALUE 0 NOCYCLE NOCACHE   NOORDER ?" +
                            //"CREATE OR REPLACE TRIGGER " + Class.Users.ProjectID + ".ASPTBLMENUNAMETRI BEFORE INSERT ON ASPTBLMENUNAME REFERENCING NEW AS NEW OLD AS OLD FOR EACH ROW DECLARE MENUNAMEID  INTEGER; BEGIN  MENUNAMEID:= 0; SELECT " + Class.Users.ProjectID + ".ASPTBLMENUNAMESEQ.NEXTVAL INTO MENUNAMEID FROM DUAL; :NEW.MENUNAMEID:= MENUNAMEID; END ASPTBLMENUNAMETRI;?" +
                            // "CREATE TABLE " + Class.Users.ProjectID + ".ASPINVENTRY(INVENTRYID  INTEGER,  QRCODE      VARCHAR2(100 BYTE),  SYSTEMDATE  VARCHAR2(50 BYTE),  SYSTEMTIME  VARCHAR2(50 BYTE),  COMPCODE    INTEGER,  USERNAME    INTEGER,  MODIFIED    DATE,  CREATEDON   DATE,  IPADDRESS   VARCHAR2(100 BYTE),  GATENAME    VARCHAR2(50 BYTE),  CATEGORY    VARCHAR2(5 BYTE),  GATEDCNO    VARCHAR2(50 BYTE),  FINYEAR     INTEGER,  PASSMISSED  VARCHAR2(1 BYTE))?" +
                            //"CREATE SEQUENCE " + Class.Users.ProjectID + ".ASPINVENTRYSEQ START WITH 1  MAXVALUE 9999999999999  MINVALUE 0 NOCYCLE NOCACHE   NOORDER ?" +
                            //"CREATE OR REPLACE TRIGGER " + Class.Users.ProjectID + ".ASPINVENTRYTRI BEFORE INSERT ON ASPINVENTRY REFERENCING NEW AS NEW OLD AS OLD FOR EACH ROW DECLARE INVENTRYID  INTEGER; BEGIN  INVENTRYID:= 0; SELECT " + Class.Users.ProjectID + ".ASPINVENTRYSEQ.NEXTVAL INTO INVENTRYID FROM DUAL; :NEW.INVENTRYID:= INVENTRYID; END ASPINVENTRYTRI;?" +
                            // "CREATE TABLE " + Class.Users.ProjectID + ".ASPTBLMACIP(ASPTBLMACIPID  INTEGER,  MACIP          VARCHAR2(50 BYTE),  ACTIVE         VARCHAR2(1 BYTE),  USERNAME       VARCHAR2(50 BYTE),  MODIFIEDON     VARCHAR2(25 BYTE),  CREATEDBY      VARCHAR2(50 BYTE),  CREATEDON      VARCHAR2(25 BYTE),  IPADDRESS      VARCHAR2(50 BYTE),  COMPCODE       INTEGER,  MACNO          INTEGER,   MTYPE          VARCHAR2(10 BYTE),   MTYPE2         VARCHAR2(50 BYTE),   AGF            VARCHAR2(50 BYTE))?" +
                            //"CREATE SEQUENCE " + Class.Users.ProjectID + ".ASPTBLMACIPSEQ START WITH 88  MAXVALUE 9999999  MINVALUE 0  NOCYCLE  NOCACHE  NOORDER ?" +
                            //"CREATE OR REPLACE TRIGGER " + Class.Users.ProjectID + ".ASPTBLMACIPTRI BEFORE INSERT ON ASPTBLMACIP REFERENCING NEW AS NEW OLD AS OLD FOR EACH ROW DECLARE ASPTBLMACIPID NUMERIC; BEGIN ASPTBLMACIPID := 0;   SELECT " + Class.Users.ProjectID + ".ASPTBLMACIPSEQ.NEXTVAL INTO ASPTBLMACIPID FROM DUAL;    :NEW.ASPTBLMACIPID:= ASPTBLMACIPID;END ASPTBLMACIPTRI;?" +
                            //"CREATE TABLE " + Class.Users.ProjectID + ".ASPTBLMACHINEMAS(ASPTBLMACHINEMASID  INTEGER,  COMPCODE            INTEGER,  WARDENNAME          INTEGER,  IPADDRESS           INTEGER,  HOSTELNAME          VARCHAR2(50 BYTE),  ACTIVE              VARCHAR2(1 BYTE),  USERNAME            INTEGER,  MODIFIED            VARCHAR2(50 BYTE),  CREATEDON           VARCHAR2(50 BYTE),  IPADDRESS1          VARCHAR2(100 BYTE),  AGF  VARCHAR2(50 BYTE),  FLF  VARCHAR2(50 BYTE),  FLFD VARCHAR2(50 BYTE),  AGFM VARCHAR2(50 BYTE),  AGFMGII             VARCHAR2(10 BYTE),  AGFC VARCHAR2(50 BYTE),  MACIP VARCHAR2(50 BYTE),  AGFP VARCHAR2(50 BYTE),  MTYPE2              VARCHAR2(30 BYTE),  AGFK VARCHAR2(30 BYTE),  SESSIONTIME         VARCHAR2(20 BYTE),  VEL  VARCHAR2(50 BYTE))?" +
                            //"CREATE SEQUENCE " + Class.Users.ProjectID + ".ASPTBLMACHINEMASSEQ  START WITH 1  MAXVALUE 99999999  MINVALUE 1  NOCYCLE  NOCACHE  NOORDER ?" +
                            //"CREATE OR REPLACE TRIGGER " + Class.Users.ProjectID + ".ASPTBLMACHINEMASTRI BEFORE INSERT ON ASPTBLMACHINEMAS REFERENCING NEW AS NEW OLD AS OLD FOR EACH ROW DECLARE ASPTBLMACHINEMASID INTEGER;BEGIN ASPTBLMACHINEMASID:= 0;   SELECT ASPTBLMACHINEMASSEQ.NEXTVAL INTO ASPTBLMACHINEMASID FROM DUAL;   :NEW.ASPTBLMACHINEMASID:= ASPTBLMACHINEMASID;END ASPTBLMACHINEMASTRI;?" +
                            //"CREATE TABLE " + Class.Users.ProjectID + ".ASPTBLCANCATEGORYMAS(ASPTBLCANCATEGORYMASID  INTEGER,  CATEGORY                VARCHAR2(100 BYTE),  CATEGORYIMAGE           BLOB,  ACTIVE                  VARCHAR2(1 BYTE),  USERNAME                INTEGER,  MODIFIED                VARCHAR2(50 BYTE),  CREATEDON               VARCHAR2(50 BYTE),  IPADDRESS               VARCHAR2(50 BYTE))?" +
                            //"CREATE SEQUENCE " + Class.Users.ProjectID + ".ASPTBLCANCATEGORYMASSEQ  START WITH 1  MAXVALUE 99999999999  MINVALUE 1  NOCYCLE  NOCACHE   NOORDER ?" +
                            //"CREATE OR REPLACE TRIGGER " + Class.Users.ProjectID + ".ASPTBLCANCATEGORYMASTRI BEFORE INSERT ON ASPTBLCANCATEGORYMAS REFERENCING NEW AS NEW OLD AS OLD FOR EACH ROW DECLARE ASPTBLCANCATEGORYMASID NUMERIC; BEGIN ASPTBLCANCATEGORYMASID := 0;   SELECT " + Class.Users.ProjectID + ".ASPTBLCANCATEGORYMASSEQ.NEXTVAL INTO ASPTBLCANCATEGORYMASID FROM DUAL;   :NEW.ASPTBLCANCATEGORYMASID:= ASPTBLCANCATEGORYMASID; END ASPTBLCANCATEGORYMASTRI;?" +
                            //"CREATE TABLE " + Class.Users.ProjectID + ".ASPTBLCANITEMMAS(  ASPTBLCANITEMMASID  INTEGER    NOT NULL,  ITEMCODE  VARCHAR2(100 BYTE),  ITEMNAME1    VARCHAR2(100 BYTE),  CATEGORY  INTEGER,  ITEMCOST INTEGER,  ACTIVE              VARCHAR2(1 BYTE),  USERNAME            INTEGER,  MODIFIED            VARCHAR2(50 BYTE),  CREATEDON           VARCHAR2(50 BYTE),  IPADDRESS           VARCHAR2(50 BYTE),  EMPLOYEECOST        NUMBER,  CONTRACTORCOST      NUMBER,  SPECIALCOST         NUMBER,  ITEMDATE            DATE,  MONTH               VARCHAR2(20 BYTE),  COMPCODE            INTEGER)?" +
                            //"CREATE SEQUENCE " + Class.Users.ProjectID + ".ASPTBLCANITEMMASSEQ  START WITH 1  MAXVALUE 99999999999  MINVALUE 1  NOCYCLE  NOCACHE   NOORDER ?" +
                            //"CREATE OR REPLACE TRIGGER " + Class.Users.ProjectID + ".ASPTBLCANITEMMASTRI BEFORE INSERT ON ASPTBLCANITEMMAS REFERENCING NEW AS NEW OLD AS OLD FOR EACH ROW DECLARE ASPTBLCANITEMMASID NUMERIC;BEGIN ASPTBLCANITEMMASID := 0;   SELECT " + Class.Users.ProjectID + ".ASPTBLCANITEMMASSEQ.NEXTVAL INTO ASPTBLCANITEMMASID FROM DUAL;   :NEW.ASPTBLCANITEMMASID:= ASPTBLCANITEMMASID;END ASPTBLCANITEMMASTRI;?" +
                            //"CREATE TABLE " + Class.Users.ProjectID + ".ASPTBLMENPER(  ASPTBLMENPERID      INTEGER  NOT NULL,  ASPTBLCANITEMMASID  INTEGER  NOT NULL,  ITEMCODE    VARCHAR2(100 BYTE),  ITEMNAME1   VARCHAR2(100 BYTE),  CATEGORY    INTEGER,  FROMDATE            VARCHAR2(50 BYTE),  FROMTIME            VARCHAR2(50 BYTE),  TODATE              VARCHAR2(50 BYTE),  TOTIME              VARCHAR2(50 BYTE),  ACTIVE              VARCHAR2(1 BYTE),  USERNAME            INTEGER,  MODIFIED            VARCHAR2(50 BYTE),  CREATEDON           VARCHAR2(50 BYTE),  IPADDRESS           VARCHAR2(50 BYTE),  COMPCODE            INTEGER,  FINYEAR             VARCHAR2(50 BYTE) )?" +
                            //"CREATE SEQUENCE " + Class.Users.ProjectID + ".ASPTBLMENPERSEQ  START WITH 1  MAXVALUE 99999999999  MINVALUE 1  NOCYCLE  NOCACHE   NOORDER ?" +
                            //"CREATE OR REPLACE TRIGGER " + Class.Users.ProjectID + ".ASPTBLMENPERTRI BEFORE INSERT ON ASPTBLMENPER REFERENCING NEW AS NEW OLD AS OLD FOR EACH ROW DECLARE ASPTBLMENPERID NUMERIC;BEGIN ASPTBLMENPERID := 0;   SELECT " + Class.Users.ProjectID + ".ASPTBLMENPERSEQ.NEXTVAL INTO ASPTBLMENPERID FROM DUAL;    :NEW.ASPTBLMENPERID:= ASPTBLMENPERID;END ASPTBLMENPERTRI;?" +
                            //"CREATE TABLE " + Class.Users.ProjectID + ".ASPTBLEMP( ASPTBLEMPID  INTEGER NOT NULL,  COMPCODE      INTEGER,  COMNAME       INTEGER,  EMPID INTEGER,  EMPNAME       VARCHAR2(100 BYTE),  LASTNAME      VARCHAR2(100 BYTE),  ADDRESS       VARCHAR2(500 BYTE),  GENDER        VARCHAR2(10 BYTE),  DATEOFBIRTH   VARCHAR2(50 BYTE),  DEPARTMENT    INTEGER,  DATEOFJOIN    VARCHAR2(20 BYTE),  CONTACT       VARCHAR2(50 BYTE),  BLOODGROUP    VARCHAR2(20 BYTE),  ACTIVE VARCHAR2(1 BYTE),  USERNAME      VARCHAR2(50 BYTE),  IPADDRESS     VARCHAR2(30 BYTE),  CREATEDBY     VARCHAR2(50 BYTE),  CREATEDON     VARCHAR2(30 BYTE),  MODIFIEDON    VARCHAR2(30 BYTE),  IDCARDNO      INTEGER,  INTIME        VARCHAR2(50 BYTE),  OUTIME        VARCHAR2(50 BYTE),  EMPIMAGE      BLOB,  EMPLOYEETYPE  VARCHAR2(50 BYTE),  IMAGEBYTES    INTEGER)?" +
                            //"CREATE SEQUENCE " + Class.Users.ProjectID + ".ASPTBLEMPSEQ  START WITH 1  MAXVALUE 99999999999  MINVALUE 1  NOCYCLE  NOCACHE   NOORDER ?" +
                            //"CREATE OR REPLACE TRIGGER " + Class.Users.ProjectID + ".ASPTBLEMPTRI BEFORE INSERT ON ASPTBLEMP REFERENCING NEW AS NEW OLD AS OLD FOR EACH ROW DECLARE ASPTBLEMPID NUMERIC; BEGIN ASPTBLEMPID := 0;   SELECT " + Class.Users.ProjectID + ".ASPTBLEMPSEQ.NEXTVAL INTO ASPTBLEMPID FROM DUAL;    :NEW.ASPTBLEMPID:= ASPTBLEMPID;END ASPTBLEMPTRI;?" +
                            // "CREATE TABLE " + Class.Users.ProjectID + ".ASPTBLCANTOKEN(  ASPTBLCANTOKENID  INTEGER,  COMPCODE          INTEGER,  FINYEAR           VARCHAR2(20 BYTE),  EMPID             INTEGER,  EMPNAME           INTEGER,  IDCARDNO          INTEGER,  ITEMCODE          INTEGER,  ITEMNAME1         INTEGER,  ITEMCOST          INTEGER,  ITEMQTY           INTEGER,  NOOFDAYS          INTEGER,  TOTALAMOUNT       INTEGER,  ACTIVE            VARCHAR2(1 BYTE),  USERNAME          INTEGER,  MODIFIED          VARCHAR2(50 BYTE),  CREATEDON         VARCHAR2(50 BYTE),  IPADDRESS         VARCHAR2(50 BYTE),  TOKENNO           VARCHAR2(50 BYTE),  TOKENNOCANCEL     VARCHAR2(10 BYTE),  EMPLOYEETYPE      VARCHAR2(50 BYTE),  TOKENOPTION       VARCHAR2(50 BYTE),  EMPLOYEECOST      NUMBER,  CONTRACTORCOST    NUMBER,  SPECIALCOST       NUMBER,  TOKENDATE         DATE)?" +
                            //"CREATE SEQUENCE " + Class.Users.ProjectID + ".ASPTBLCANTOKENSEQ  START WITH 1  MAXVALUE 99999999999  MINVALUE 1  NOCYCLE  NOCACHE   NOORDER ?" +
                            //"CREATE OR REPLACE TRIGGER " + Class.Users.ProjectID + ".ASPTBLCANTOKENTRI BEFORE INSERT ON ASPTBLCANTOKEN REFERENCING NEW AS NEW OLD AS OLD FOR EACH ROW DECLARE ASPTBLCANTOKENID NUMERIC;BEGIN ASPTBLCANTOKENID := 0;   SELECT " + Class.Users.ProjectID + ".ASPTBLCANTOKENSEQ.NEXTVAL INTO ASPTBLCANTOKENID FROM DUAL;   :NEW.ASPTBLCANTOKENID:= ASPTBLCANTOKENID; END ASPTBLCANTOKENTRI;?" +
                            "INSERT INTO " + Class.Users.ProjectID + ".ASPTBLMENUNAME  (MENUNAME,ACTIVE,PARENTMENUID,CREATEON,CREATEBY,MODIFYON,IPADDRESS) VALUES('TIPL','T', 0,'28-Jan-2021 11:26:19','','28-Jan-2021 11:26:19','" + Class.Users.IPADDRESS + "')? " +
                            "INSERT INTO " + Class.Users.ProjectID + ".ASPTBLMENUNAME  (MENUNAME,ACTIVE,PARENTMENUID,CREATEON,CREATEBY,MODIFYON,IPADDRESS) VALUES('Masters','T', 1,'28-Jan-2021 11:26:19','','28-Jan-2021 11:26:19','" + Class.Users.IPADDRESS + "')? " +
                            "INSERT INTO " + Class.Users.ProjectID + ".ASPTBLMENUNAME  (MENUNAME,ACTIVE,PARENTMENUID,CREATEON,CREATEBY,MODIFYON,IPADDRESS) VALUES('Transactions','T', 1,'28-Jan-2021 11:26:19','','28-Jan-2021 11:26:19','" + Class.Users.IPADDRESS + "')? " +
                            "INSERT INTO " + Class.Users.ProjectID + ".ASPTBLMENUNAME  (MENUNAME,ACTIVE,PARENTMENUID,CREATEON,CREATEBY,MODIFYON,IPADDRESS) VALUES('TreeView','T', 1,'28-Jan-2021 11:26:19','','28-Jan-2021 11:26:19','" + Class.Users.IPADDRESS + "')? " +
                            "INSERT INTO " + Class.Users.ProjectID + ".ASPTBLMENUNAME  (MENUNAME,ACTIVE,PARENTMENUID,CREATEON,CREATEBY,MODIFYON,IPADDRESS) VALUES('Reports','T', 1,'28-Jan-2021 11:26:19','','28-Jan-2021 11:26:19','" + Class.Users.IPADDRESS + "')? " +
                            "INSERT INTO " + Class.Users.ProjectID + ".ASPTBLMENUNAME  (MENUNAME,ACTIVE,PARENTMENUID,CREATEON,CREATEBY,MODIFYON,IPADDRESS) VALUES('Registration','T', 1,'28-Jan-2021 11:26:19','','28-Jan-2021 11:26:19','" + Class.Users.IPADDRESS + "')? " +
                            "INSERT INTO " + Class.Users.ProjectID + ".ASPTBLMENUNAME  (MENUNAME,ACTIVE,PARENTMENUID,CREATEON,CREATEBY,MODIFYON,IPADDRESS) VALUES('MenuNameMaster','T', 4,'28-Jan-2021 11:26:19','','28-Jan-2021 11:26:19','" + Class.Users.IPADDRESS + "')? " +
                            "INSERT INTO " + Class.Users.ProjectID + ".ASPTBLMENUNAME  (MENUNAME,ACTIVE,PARENTMENUID,CREATEON,CREATEBY,MODIFYON,IPADDRESS) VALUES('UserMaster','T', 4,'28-Jan-2021 11:26:19','','28-Jan-2021 11:26:19','" + Class.Users.IPADDRESS + "')? " +
                            "INSERT INTO " + Class.Users.ProjectID + ".ASPTBLMENUNAME  (MENUNAME,ACTIVE,PARENTMENUID,CREATEON,CREATEBY,MODIFYON,IPADDRESS) VALUES('NavigationMaster','T', 4,'28-Jan-2021 11:26:19','','28-Jan-2021 11:26:19','" + Class.Users.IPADDRESS + "')? " +
                            "INSERT INTO " + Class.Users.ProjectID + ".ASPTBLMENUNAME  (MENUNAME,ACTIVE,PARENTMENUID,CREATEON,CREATEBY,MODIFYON,IPADDRESS) VALUES('TreeViewMaster','T', 4,'28-Jan-2021 11:26:19','','28-Jan-2021 11:26:19','" + Class.Users.IPADDRESS + "')? " +
                            "INSERT INTO " + Class.Users.ProjectID + ".ASPTBLMENUNAME  (MENUNAME,ACTIVE,PARENTMENUID,CREATEON,CREATEBY,MODIFYON,IPADDRESS) VALUES('UserRights','T', 4,'28-Jan-2021 11:26:19','','28-Jan-2021 11:26:19','" + Class.Users.IPADDRESS + "')?" +
                            "INSERT INTO " + Class.Users.ProjectID + ".ASPTBLMENUNAME  (MENUNAME,ACTIVE,PARENTMENUID,CREATEON,CREATEBY,MODIFYON,IPADDRESS) VALUES('DataImport','T', 3,'28-Jan-2021 11:26:19','','28-Jan-2021 11:26:19','" + Class.Users.IPADDRESS + "')?" +
                            "INSERT INTO " + Class.Users.ProjectID + ".ASPTBLMENUNAME  (MENUNAME,ACTIVE,PARENTMENUID,CREATEON,CREATEBY,MODIFYON,IPADDRESS) VALUES('DeviceCommunication','T', 3,'28-Jan-2021 11:26:19','','28-Jan-2021 11:26:19','" + Class.Users.IPADDRESS + "')?" +

                            "INSERT INTO " + Class.Users.ProjectID + ".ASPTBLMENUNAME  (MENUNAME,ACTIVE,PARENTMENUID,CREATEON,CREATEBY,MODIFYON,IPADDRESS) VALUES('UserMaster','T', 4,'28-Jan-2021 11:26:19','','28-Jan-2021 11:26:19','" + Class.Users.IPADDRESS + "')? " +
                            "INSERT INTO " + Class.Users.ProjectID + ".ASPTBLMENUNAME  (MENUNAME,ACTIVE,PARENTMENUID,CREATEON,CREATEBY,MODIFYON,IPADDRESS) VALUES('NavigationMaster','T', 4,'28-Jan-2021 11:26:19','','28-Jan-2021 11:26:19','" + Class.Users.IPADDRESS + "')? " +
                            "INSERT INTO " + Class.Users.ProjectID + ".ASPTBLMENUNAME  (MENUNAME,ACTIVE,PARENTMENUID,CREATEON,CREATEBY,MODIFYON,IPADDRESS) VALUES('TreeViewMaster','T', 4,'28-Jan-2021 11:26:19','','28-Jan-2021 11:26:19','" + Class.Users.IPADDRESS + "')? " +
                            "INSERT INTO " + Class.Users.ProjectID + ".ASPTBLMENUNAME  (MENUNAME,ACTIVE,PARENTMENUID,CREATEON,CREATEBY,MODIFYON,IPADDRESS) VALUES('UserRights','T', 4,'28-Jan-2021 11:26:19','','28-Jan-2021 11:26:19','" + Class.Users.IPADDRESS + "')?" +
                            "INSERT INTO " + Class.Users.ProjectID + ".ASPTBLMENUNAME  (MENUNAME,ACTIVE,PARENTMENUID,CREATEON,CREATEBY,MODIFYON,IPADDRESS) VALUES('DataImport','T', 3,'28-Jan-2021 11:26:19','','28-Jan-2021 11:26:19','" + Class.Users.IPADDRESS + "')?" +
                            "INSERT INTO " + Class.Users.ProjectID + ".ASPTBLMENUNAME  (MENUNAME,ACTIVE,PARENTMENUID,CREATEON,CREATEBY,MODIFYON,IPADDRESS) VALUES('DeviceCommunication','T', 3,'28-Jan-2021 11:26:19','','28-Jan-2021 11:26:19','" + Class.Users.IPADDRESS + "')?" +

                            "CREATE TABLE " + Class.Users.ProjectID + ".ASPTBLNAVIGATION (MENUID  INTEGER,  MENUNAME   VARCHAR2(100 BYTE),  NAVURL   VARCHAR2(200 BYTE),  PARENTMENUID   INTEGER,  ACTIVE   VARCHAR2(1 BYTE),  MENUNAMEID  INTEGER,  COMPCODE  INTEGER,  USERNAME   INTEGER,FOREIGN KEY(USERNAME) REFERENCES asptblusermas (USERID) )?" +
                            "CREATE SEQUENCE " + Class.Users.ProjectID + ".ASPTBLNAVIGATIONSEQ START WITH 1  MAXVALUE 9999999999999  MINVALUE 0 NOCYCLE NOCACHE   NOORDER?" +
                            "CREATE OR REPLACE TRIGGER " + Class.Users.ProjectID + ".ASPTBLNAVIGATIONTRI BEFORE INSERT ON ASPTBLNAVIGATION REFERENCING NEW AS NEW OLD AS OLD FOR EACH ROW DECLARE MENUID  INTEGER;BEGIN MENUID:= 0;SELECT " + Class.Users.ProjectID + ".ASPTBLNAVIGATIONSEQ.NEXTVAL INTO MENUID FROM DUAL;:NEW.MENUID:= MENUID; END ASPTBLNAVIGATIONTRI;? " +
                            "INSERT INTO  " + Class.Users.ProjectID + ".ASPTBLNAVIGATION  (MENUNAME,NAVURL,PARENTMENUID,ACTIVE,MENUNAMEID,COMPCODE,USERNAME)VALUES( 'TIPL' , 'TIPL' , 0 , 'T' , 1 ,'" + Class.Users.COMPCODE + "',1)? " +
                            "INSERT INTO  " + Class.Users.ProjectID + ".ASPTBLNAVIGATION  (MENUNAME,NAVURL,PARENTMENUID,ACTIVE,MENUNAMEID,COMPCODE,USERNAME)VALUES( 'Masters' , 'Masters' , 1 , 'T' , 2 ,'" + Class.Users.COMPCODE + "',1)? " +
                            "INSERT INTO  " + Class.Users.ProjectID + ".ASPTBLNAVIGATION  (MENUNAME,NAVURL,PARENTMENUID,ACTIVE,MENUNAMEID,COMPCODE,USERNAME)VALUES( 'Transactions' , 'Transactions' , 1 , 'T' , 3 ,'" + Class.Users.COMPCODE + "',1)? " +
                            "INSERT INTO  " + Class.Users.ProjectID + ".ASPTBLNAVIGATION  (MENUNAME,NAVURL,PARENTMENUID,ACTIVE,MENUNAMEID,COMPCODE,USERNAME)VALUES( 'TreeView' , 'TreeView' , 1 , 'T' ,4 ,'" + Class.Users.COMPCODE + "',1)? " +
                            "INSERT INTO  " + Class.Users.ProjectID + ".ASPTBLNAVIGATION  (MENUNAME,NAVURL,PARENTMENUID,ACTIVE,MENUNAMEID,COMPCODE,USERNAME)VALUES( 'Reports' , 'Transactions' , 1 , 'T' , 5 ,'" + Class.Users.COMPCODE + "',1)? " +
                            "INSERT INTO  " + Class.Users.ProjectID + ".ASPTBLNAVIGATION  (MENUNAME,NAVURL,PARENTMENUID,ACTIVE,MENUNAMEID,COMPCODE,USERNAME)VALUES( 'Registration' , 'TreeView' , 1 , 'T' , 6 ,'" + Class.Users.COMPCODE + "',1)? " +
                            "INSERT INTO  " + Class.Users.ProjectID + ".ASPTBLNAVIGATION  (MENUNAME,NAVURL,PARENTMENUID,ACTIVE,MENUNAMEID,COMPCODE,USERNAME)VALUES( 'MenuNameMaster' , 'TreeView' , 4 , 'T' ,7,'" + Class.Users.COMPCODE + "',1)? " +
                            "INSERT INTO  " + Class.Users.ProjectID + ".ASPTBLNAVIGATION  (MENUNAME,NAVURL,PARENTMENUID,ACTIVE,MENUNAMEID,COMPCODE,USERNAME)VALUES( 'UserMaster' , 'TreeView' , 4 , 'T' , 8 ,'" + Class.Users.COMPCODE + "',1)? " +
                            "INSERT INTO  " + Class.Users.ProjectID + ".ASPTBLNAVIGATION  (MENUNAME,NAVURL,PARENTMENUID,ACTIVE,MENUNAMEID,COMPCODE,USERNAME)VALUES( 'NavigationMaster' , 'TreeView' , 4 , 'T' , 9 ,'" + Class.Users.COMPCODE + "',1)? " +
                            "INSERT INTO  " + Class.Users.ProjectID + ".ASPTBLNAVIGATION  (MENUNAME,NAVURL,PARENTMENUID,ACTIVE,MENUNAMEID,COMPCODE,USERNAME)VALUES( 'TreeViewMaster' , 'TreeView' , 4 , 'T' , 10 ,'" + Class.Users.COMPCODE + "',1)? " +
                            "INSERT INTO  " + Class.Users.ProjectID + ".ASPTBLNAVIGATION  (MENUNAME,NAVURL,PARENTMENUID,ACTIVE,MENUNAMEID,COMPCODE,USERNAME)VALUES( 'UserRights' , 'TreeView' , 4 , 'T' , 11 ,'" + Class.Users.COMPCODE + "',1)? " +
                            "INSERT INTO  " + Class.Users.ProjectID + ".ASPTBLNAVIGATION  (MENUNAME,NAVURL,PARENTMENUID,ACTIVE,MENUNAMEID,COMPCODE,USERNAME)VALUES( 'DataImport' , 'Transactions' , 3 , 'T' , 12 ,'" + Class.Users.COMPCODE + "',1)? " +
                            "INSERT INTO  " + Class.Users.ProjectID + ".ASPTBLNAVIGATION  (MENUNAME,NAVURL,PARENTMENUID,ACTIVE,MENUNAMEID,COMPCODE,USERNAME)VALUES( 'DeviceCommunication' , 'Transactions' , 3 , 'T' , 13 ,'" + Class.Users.COMPCODE + "',1)? " +
                            "CREATE TABLE " + Class.Users.ProjectID + ".ASPTBLUSERRIGHTS  (USERRIGHTSID  INTEGER  NOT NULL,  MENUID  INTEGER NOT NULL,  MENUNAME  VARCHAR2(100 BYTE),  NAVURL  VARCHAR2(100 BYTE),  PARENTMENUID  INTEGER,  ACTIVE  VARCHAR2(1 BYTE),  SAVES  VARCHAR2(1 BYTE),  PRINTS  VARCHAR2(1 BYTE),  READONLY   VARCHAR2(1 BYTE),  SEARCH  VARCHAR2(1 BYTE),  DELETES VARCHAR2(1 BYTE),  USERNAME   INTEGER NOT NULL,  COMPCODE   INTEGER NOT NULL,  PASSWORDS  VARCHAR2(50 BYTE),  SNO  INTEGER,  IPADDRESS  VARCHAR2(30 BYTE),  CREATEDBY  VARCHAR2(30 BYTE),  CREATEDON DATE,  MODIFIEDON    DATE,  NEWS VARCHAR2(10 BYTE),  TREEBUTTON VARCHAR2(50 BYTE),  GLOBALSEARCH  VARCHAR2(50 BYTE),  LOGIN   VARCHAR2(50 BYTE),  CHANGEPASSWORD   VARCHAR2(50 BYTE),  CHANGESKIN VARCHAR2(50 BYTE),  DOWNLOAD   VARCHAR2(50 BYTE),  CONTACT VARCHAR2(50 BYTE),  PDF   VARCHAR2(50 BYTE),  IMPORTS VARCHAR2(50 BYTE),FOREIGN KEY(username) REFERENCES asptblusermas (userid) )?" +
                            "CREATE SEQUENCE " + Class.Users.ProjectID + ".ASPTBLUSERRIGHTSSEQ START WITH 1  MAXVALUE 999999  MINVALUE 0 NOCYCLE NOCACHE   NOORDER?" +
                            "CREATE OR REPLACE TRIGGER " + Class.Users.ProjectID + ".ASPTBLUSERRIGHTSTRI BEFORE INSERT ON ASPTBLUSERRIGHTS REFERENCING NEW AS NEW OLD AS OLD FOR EACH ROW DECLARE USERRIGHTSID  INTEGER; BEGIN  USERRIGHTSID:= 0; SELECT " + Class.Users.ProjectID + ".ASPTBLUSERRIGHTSSEQ.NEXTVAL INTO USERRIGHTSID FROM DUAL; :NEW.USERRIGHTSID:= USERRIGHTSID; END ASPTBLUSERRIGHTSTRI;? " +
                            "INSERT INTO " + Class.Users.ProjectID + ".ASPTBLUSERRIGHTS(MENUID ,  MENUNAME,  NAVURL,  PARENTMENUID ,  ACTIVE,News, SAVES, PRINTS,  READONLY ,  SEARCH ,  DELETES,COMPCODE,USERNAME,TREEBUTTON,GLOBALSEARCH,LOGIN,CHANGEPASSWORD, CHANGESKIN,DOWNLOAD,CONTACT,Pdf,Imports,CREATEDON,MODIFIEDON,SNO,IPADDRESS ) values(1,'TIPL','TIPL',0,'T','T','T','F','F','F','F', '" + Class.Users.COMPCODE + "' ,1,'F','F','F','F','F','F','F','F','F',to_date('28/01/2021 11:49:36', 'dd/MM/yyyy hh24:MI:SS'),to_date('28/01/2021 11:49:36', 'dd/MM/yyyy hh24:MI:SS'),0,'" + Class.Users.IPADDRESS + "')? " +
                            "INSERT INTO " + Class.Users.ProjectID + ".ASPTBLUSERRIGHTS(MENUID ,  MENUNAME,  NAVURL,  PARENTMENUID ,  ACTIVE,NEWS, SAVES, PRINTS,  READONLY ,  SEARCH ,  DELETES,COMPCODE,USERNAME,TREEBUTTON,GLOBALSEARCH,LOGIN,CHANGEPASSWORD, CHANGESKIN,DOWNLOAD,CONTACT,PDF,IMPORTS,CREATEDON,MODIFIEDON,SNO,IPADDRESS ) VALUES(2,'Masters','Masters',1,'T','T','T','F','F','F','F', '" + Class.Users.COMPCODE + "' ,1,'F','F','F','F','F','F','F','F','F',TO_DATE('28/01/2021 11:49:59', 'dd/MM/yyyy hh24:MI:SS'),TO_DATE('28/01/2021 11:49:59', 'dd/MM/yyyy hh24:MI:SS'),0,'" + Class.Users.IPADDRESS + "')? " +
                            "INSERT INTO " + Class.Users.ProjectID + ".ASPTBLUSERRIGHTS(MENUID ,  MENUNAME,  NAVURL,  PARENTMENUID ,  ACTIVE,NEWS, SAVES, PRINTS,  READONLY ,  SEARCH ,  DELETES,COMPCODE,USERNAME,TREEBUTTON,GLOBALSEARCH,LOGIN,CHANGEPASSWORD, CHANGESKIN,DOWNLOAD,CONTACT,PDF,IMPORTS,CREATEDON,MODIFIEDON,SNO,IPADDRESS ) VALUES(3,'Transactions','Transactions',1,'T','T','T','F','F','F','F', '" + Class.Users.COMPCODE + "' ,1,'F','F','F','F','F','F','F','F','F',TO_DATE('28/01/2021 11:49:59', 'dd/MM/yyyy hh24:MI:SS'),TO_DATE('28/01/2021 11:49:59', 'dd/MM/yyyy hh24:MI:SS'),0,'" + Class.Users.IPADDRESS + "')? " +
                            "INSERT INTO " + Class.Users.ProjectID + ".ASPTBLUSERRIGHTS(MENUID ,  MENUNAME,  NAVURL,  PARENTMENUID ,  ACTIVE,NEWS, SAVES, PRINTS,  READONLY ,  SEARCH ,  DELETES,COMPCODE,USERNAME,TREEBUTTON,GLOBALSEARCH,LOGIN,CHANGEPASSWORD, CHANGESKIN,DOWNLOAD,CONTACT,PDF,IMPORTS,CREATEDON,MODIFIEDON,SNO,IPADDRESS ) VALUES(4,'TreeView','TreeView',1,'T','T','T','F','T','F','F', '" + Class.Users.COMPCODE + "' ,1,'F','F','F','F','F','F','F','F','F',TO_DATE('28/01/2021 11:51:08', 'dd/MM/yyyy hh24:MI:SS'),TO_DATE('28/01/2021 11:51:08', 'dd/MM/yyyy hh24:MI:SS'),0,'" + Class.Users.IPADDRESS + "')? " +
                            "INSERT INTO " + Class.Users.ProjectID + ".ASPTBLUSERRIGHTS(MENUID ,  MENUNAME,  NAVURL,  PARENTMENUID ,  ACTIVE,NEWS, SAVES, PRINTS,  READONLY ,  SEARCH ,  DELETES,COMPCODE,USERNAME,TREEBUTTON,GLOBALSEARCH,LOGIN,CHANGEPASSWORD, CHANGESKIN,DOWNLOAD,CONTACT,PDF,IMPORTS,CREATEDON,MODIFIEDON,SNO,IPADDRESS ) VALUES(5,'Reports','Reports',1,'T','T','T','F','T','F','F', '" + Class.Users.COMPCODE + "' ,1,'F','F','F','F','F','F','F','F','F',TO_DATE('28/01/2021 11:49:59', 'dd/MM/yyyy hh24:MI:SS'),TO_DATE('28/01/2021 11:49:59', 'dd/MM/yyyy hh24:MI:SS'),0,'" + Class.Users.IPADDRESS + "')? " +
                            "INSERT INTO " + Class.Users.ProjectID + ".ASPTBLUSERRIGHTS(MENUID ,  MENUNAME,  NAVURL,  PARENTMENUID ,  ACTIVE,NEWS, SAVES, PRINTS,  READONLY ,  SEARCH ,  DELETES,COMPCODE,USERNAME,TREEBUTTON,GLOBALSEARCH,LOGIN,CHANGEPASSWORD, CHANGESKIN,DOWNLOAD,CONTACT,PDF,IMPORTS,CREATEDON,MODIFIEDON,SNO,IPADDRESS ) VALUES(6,'Registration','Registration',1,'T','T','T','F','F','F','F', '" + Class.Users.COMPCODE + "' ,1,'F','F','F','F','F','F','F','F','F',TO_DATE('28/01/2021 11:51:08', 'dd/MM/yyyy hh24:MI:SS'),TO_DATE('28/01/2021 11:51:08', 'dd/MM/yyyy hh24:MI:SS'),0,'" + Class.Users.IPADDRESS + "')? " +
                            "INSERT INTO " + Class.Users.ProjectID + ".ASPTBLUSERRIGHTS(MENUID ,  MENUNAME,  NAVURL,  PARENTMENUID ,  ACTIVE,News, SAVES, PRINTS,  READONLY ,  SEARCH ,  DELETES,COMPCODE,USERNAME,TREEBUTTON,GLOBALSEARCH,LOGIN,CHANGEPASSWORD, CHANGESKIN,DOWNLOAD,CONTACT,Pdf,Imports,CREATEDON,MODIFIEDON,SNO,IPADDRESS ) values(7,'MenuNameMaster','TreeView',4,'T','T','T','F','T','F','F', '" + Class.Users.COMPCODE + "' ,1,'F','F','F','F','F','F','F','F','F',to_date('28/01/2021 11:49:36', 'dd/MM/yyyy hh24:MI:SS'),to_date('28/01/2021 11:49:36', 'dd/MM/yyyy hh24:MI:SS'),0,'" + Class.Users.IPADDRESS + "')? " +
                            "INSERT INTO " + Class.Users.ProjectID + ".ASPTBLUSERRIGHTS(MENUID ,  MENUNAME,  NAVURL,  PARENTMENUID ,  ACTIVE,NEWS, SAVES, PRINTS,  READONLY ,  SEARCH ,  DELETES,COMPCODE,USERNAME,TREEBUTTON,GLOBALSEARCH,LOGIN,CHANGEPASSWORD, CHANGESKIN,DOWNLOAD,CONTACT,PDF,IMPORTS,CREATEDON,MODIFIEDON,SNO,IPADDRESS ) VALUES(8,'UserMaster','TreeView',4,'T','T','T','T','T','T','T', '" + Class.Users.COMPCODE + "' ,1,'F','F','F','F','F','F','F','F','F',TO_DATE('28/01/2021 11:49:59', 'dd/MM/yyyy hh24:MI:SS'),TO_DATE('28/01/2021 11:49:59', 'dd/MM/yyyy hh24:MI:SS'),0,'" + Class.Users.IPADDRESS + "')? " +
                            "INSERT INTO " + Class.Users.ProjectID + ".ASPTBLUSERRIGHTS(MENUID ,  MENUNAME,  NAVURL,  PARENTMENUID ,  ACTIVE,NEWS, SAVES, PRINTS,  READONLY ,  SEARCH ,  DELETES,COMPCODE,USERNAME,TREEBUTTON,GLOBALSEARCH,LOGIN,CHANGEPASSWORD, CHANGESKIN,DOWNLOAD,CONTACT,PDF,IMPORTS,CREATEDON,MODIFIEDON,SNO,IPADDRESS ) VALUES(9,'NavigationMaster','TreeView',4,'T','T','T','T','T','F','F', '" + Class.Users.COMPCODE + "' ,1,'F','F','F','F','F','F','F','F','F',TO_DATE('28/01/2021 11:49:59', 'dd/MM/yyyy hh24:MI:SS'),TO_DATE('28/01/2021 11:49:59', 'dd/MM/yyyy hh24:MI:SS'),0,'" + Class.Users.IPADDRESS + "')? " +
                            "INSERT INTO " + Class.Users.ProjectID + ".ASPTBLUSERRIGHTS(MENUID ,  MENUNAME,  NAVURL,  PARENTMENUID ,  ACTIVE,News, SAVES, PRINTS,  READONLY ,  SEARCH ,  DELETES,COMPCODE,USERNAME,TREEBUTTON,GLOBALSEARCH,LOGIN,CHANGEPASSWORD, CHANGESKIN,DOWNLOAD,CONTACT,Pdf,Imports,CREATEDON,MODIFIEDON,SNO,IPADDRESS ) values(10,'TreeViewMaster','TreeView',4,'T','T','T','T','T','F','F', '" + Class.Users.COMPCODE + "' ,1,'F','F','F','F','F','F','F','F','F',to_date('28/01/2021 11:49:36', 'dd/MM/yyyy hh24:MI:SS'),to_date('28/01/2021 11:49:36', 'dd/MM/yyyy hh24:MI:SS'),0,'" + Class.Users.IPADDRESS + "')? " +
                            "INSERT INTO " + Class.Users.ProjectID + ".ASPTBLUSERRIGHTS(MENUID ,  MENUNAME,  NAVURL,  PARENTMENUID ,  ACTIVE,NEWS, SAVES, PRINTS,  READONLY ,  SEARCH ,  DELETES,COMPCODE,USERNAME,TREEBUTTON,GLOBALSEARCH,LOGIN,CHANGEPASSWORD, CHANGESKIN,DOWNLOAD,CONTACT,PDF,IMPORTS,CREATEDON,MODIFIEDON,SNO,IPADDRESS ) VALUES(11,'UserRights','TreeView',4,'T','T','T','T','T','T','T', '" + Class.Users.COMPCODE + "' ,1,'F','F','F','F','F','F','F','F','F',TO_DATE('28/01/2021 11:49:59', 'dd/MM/yyyy hh24:MI:SS'),TO_DATE('28/01/2021 11:49:59', 'dd/MM/yyyy hh24:MI:SS'),0,'" + Class.Users.IPADDRESS + "')? " +
                            "INSERT INTO " + Class.Users.ProjectID + ".ASPTBLUSERRIGHTS(MENUID ,  MENUNAME,  NAVURL,  PARENTMENUID ,  ACTIVE,NEWS, SAVES, PRINTS,  READONLY ,  SEARCH ,  DELETES,COMPCODE,USERNAME,TREEBUTTON,GLOBALSEARCH,LOGIN,CHANGEPASSWORD, CHANGESKIN,DOWNLOAD,CONTACT,PDF,IMPORTS,CREATEDON,MODIFIEDON,SNO,IPADDRESS ) VALUES(12,'DataImport','Transactions',3,'T','T','T','T','T','T','T', '" + Class.Users.COMPCODE + "' ,1,'F','F','F','F','F','F','F','F','F',TO_DATE('28/01/2021 11:49:59', 'dd/MM/yyyy hh24:MI:SS'),TO_DATE('28/01/2021 11:49:59', 'dd/MM/yyyy hh24:MI:SS'),0,'" + Class.Users.IPADDRESS + "')? " +
                            "INSERT INTO " + Class.Users.ProjectID + ".ASPTBLUSERRIGHTS(MENUID ,  MENUNAME,  NAVURL,  PARENTMENUID ,  ACTIVE,NEWS, SAVES, PRINTS,  READONLY ,  SEARCH ,  DELETES,COMPCODE,USERNAME,TREEBUTTON,GLOBALSEARCH,LOGIN,CHANGEPASSWORD, CHANGESKIN,DOWNLOAD,CONTACT,PDF,IMPORTS,CREATEDON,MODIFIEDON,SNO,IPADDRESS ) VALUES(13,'DeviceCommunication','Transactions',3,'T','T','T','T','T','T','T', '" + Class.Users.COMPCODE + "' ,1,'F','F','F','F','F','F','F','F','F',TO_DATE('28/01/2021 11:49:59', 'dd/MM/yyyy hh24:MI:SS'),TO_DATE('28/01/2021 11:49:59', 'dd/MM/yyyy hh24:MI:SS'),0,'" + Class.Users.IPADDRESS + "')? " +
                            "create table " + Class.Users.ProjectID + ".asptblregistration(asptblregistrationid int primary key,finyear  VARCHAR2(50), fromdate VARCHAR2(50),todate VARCHAR2(50),productid  VARCHAR2(250),  licensetype   VARCHAR2(50),  noofdays  int , productkeys   VARCHAR2(250) ,days int, Active  VARCHAR2(1), compcode  int , username  int , ipaddress  VARCHAR2(50), createdon   VARCHAR2(50),  createdby  VARCHAR2(50),  modifiedon  VARCHAR2(50))?" +
                            "CREATE SEQUENCE " + Class.Users.ProjectID + ".asptblregistrationseq START WITH 1  MAXVALUE 999999  MINVALUE 0 NOCYCLE NOCACHE   NOORDER?" +
                            "CREATE OR REPLACE TRIGGER " + Class.Users.ProjectID + ".asptblregistrationtri BEFORE INSERT ON asptblregistration REFERENCING NEW AS NEW OLD AS OLD FOR EACH ROW DECLARE asptblregistrationid  INTEGER; BEGIN  asptblregistrationid:= 0; SELECT " + Class.Users.ProjectID + ".asptblregistrationseq.NEXTVAL INTO asptblregistrationid FROM DUAL; :NEW.asptblregistrationid:= asptblregistrationid; END asptblregistrationtri?";
                            progressBar1.Visible = true; lblprogress1.Visible = true; label1.Visible = true;
                            string[] words = tab.Split('?');
                            for (int i = 0; i < words.Length; i++)
                            {
                                Class.Users.UserTime = 0;
                                string del = words[i].ToString().Trim();
                                Utility.ExecuteNonQuery(del);
                                label1.Text = words[i].ToString();
                                decimal per = Convert.ToDecimal(100 / Convert.ToDecimal(words.Length)) * (i + 1);
                                lblprogress1.Refresh();
                                progressBar1.Value = (int)per;
                                lblprogress1.Text = (int)per + "%";
                                label1.Refresh();
                            }
                            Cursor = Cursors.Default;
                            MessageBox.Show("Created Successfully", "Tables", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                        else
                        {
                            MessageBox.Show("Already Table Created.pls Contact your Administrator", "Contact Administrator", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid CompCode", "pls Check Database Connection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;

                MessageBox.Show(ex.ToString(), "Tables", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string tab = "";
            tab = "" +           
             "DROP TRIGGER " + Class.Users.ProjectID + ".ASPTBLUSERMASTRI?" +
             "DROP SEQUENCE " + Class.Users.ProjectID + ".ASPTBLUSERMASSEQ?" +
             "DROP TABLE " + Class.Users.ProjectID + ".ASPTBLUSERMAS CASCADE CONSTRAINTS?" +
             "DROP TRIGGER " + Class.Users.ProjectID + ".ASPTBLMENUNAMETRI?" +
             "DROP SEQUENCE " + Class.Users.ProjectID + ".ASPTBLMENUNAMESEQ?" +
             "DROP TABLE " + Class.Users.ProjectID + ".ASPTBLMENUNAME CASCADE CONSTRAINTS?" +
             "DROP TRIGGER " + Class.Users.ProjectID + ".ASPTBLNAVIGATIONTRI?" +
             "DROP SEQUENCE " + Class.Users.ProjectID + ".ASPTBLNAVIGATIONseq?" +
             "DROP TABLE " + Class.Users.ProjectID + ".ASPTBLNAVIGATION CASCADE CONSTRAINTS?" +
             "DROP TRIGGER " + Class.Users.ProjectID + ".ASPTBLUSERRIGHTSTRI?" +
             "DROP SEQUENCE " + Class.Users.ProjectID + ".ASPTBLUSERRIGHTSSEQ?" +
             "DROP TABLE " + Class.Users.ProjectID + ".ASPTBLUSERRIGHTS CASCADE CONSTRAINTS?" +
             "DROP TRIGGER " + Class.Users.ProjectID + ".asptblregistrationTRI?" +
             "DROP SEQUENCE " + Class.Users.ProjectID + ".asptblregistrationSEQ?" +
             "DROP TABLE " + Class.Users.ProjectID + ".asptblregistration";
            progressBar1.Visible = true; lblprogress1.Visible = true; label1.Visible = true;
            string[] words = tab.Split('?');
            for (int i = 0; i < words.Length; i++)
            {
                Class.Users.UserTime = 0;
                string del = words[i].ToString().Trim();
                Utility.ExecuteNonQuery(del);

                label1.Text = words[i].ToString();
                decimal per = Convert.ToDecimal(100 / Convert.ToDecimal(words.Length)) * (i + 1);
                lblprogress1.Refresh();
                progressBar1.Value = (int)per;
                lblprogress1.Text = (int)per + "%";
                label1.Refresh();
            }
            Cursor = Cursors.Default;
            MessageBox.Show("Invalid", "Tables", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
