using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pinnacle.Transactions
{
    public partial class DataTransfer : Form, ToolStripAccess
    {
        
        private static DataTransfer _instance;

        public DataTransfer()
        {
            InitializeComponent();
            lbloracledatabase.Text  ="Oracle Database  : "+ Class.Users.DataBase;
            lblsqldatabase.Text = "MySql Database  : " + Class.Users.MySqlDataBase;
            butheader.BackColor = Class.Users.BackColors;

            butfooter.BackColor = Class.Users.BackColors;
        }
        public static DataTransfer Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new DataTransfer();
                GlobalVariables.CurrentForm = _instance;
                return _instance;
            }
        }
        public void News()
        {
            butheader.BackColor = Class.Users.BackColors;
          
            butfooter.BackColor = Class.Users.BackColors;
        }

        public void Saves()
        {
            throw new NotImplementedException();
        }

        public void Prints()
        {
            throw new NotImplementedException();
        }

        public void Searchs()
        {
            throw new NotImplementedException();
        }

        public void Deletes()
        {
            throw new NotImplementedException();
        }

        public void ReadOnlys()
        {
            throw new NotImplementedException();
        }

        public void Imports()
        {
            throw new NotImplementedException();
        }

        public void Pdfs()
        {
            throw new NotImplementedException();
        }

        public void ChangePasswords()
        {
            throw new NotImplementedException();
        }

        public void DownLoads()
        {
            throw new NotImplementedException();
        }

        public void ChangeSkins()
        {
            throw new NotImplementedException();
        }

        public void Logins()
        {
            throw new NotImplementedException();
        }

        public void GlobalSearchs()
        {
            throw new NotImplementedException();
        }

        public void TreeButtons()
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        private void DataTransfer_Load(object sender, EventArgs e)
        {

        }

        private void lbltablename_Click(object sender, EventArgs e)
        {

        }

        private void lbloracledatabase_Click(object sender, EventArgs e)
        {

        }

        public void Searchs(int id)
        {
            throw new NotImplementedException();
        }

        public void Deletes(int id)
        {
            throw new NotImplementedException();
        }
    }
}
