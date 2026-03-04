using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Pinnacle.UserControls
{
    public partial class UCListView : UserControl
    {
        public UCListView()
        {
            InitializeComponent();
        }
        public void Load_Details()
        {
            try
            {
                TxtSearch.Text = "";
                DGLoadDetails.Columns.Clear();
                if (GlobalVariables.SearchQuery != "")
                {
                    Utility.Load_DataGrid(DGLoadDetails, GlobalVariables.SearchQuery);
                    HideColumns();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error..!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HideColumns()
        {
            try
            {
                if (GlobalVariables.HideCols != null)
                {
                    foreach (String Str in GlobalVariables.HideCols)
                    {
                        DGLoadDetails.Columns[Str].Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error..!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DGLoadDetails_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                //((ToolStripAccess)GlobalVariables.CurrentForm).Searchs(Convert.ToInt32(DGLoadDetails.Rows[e.RowIndex].Cells["ID"].Value));
            }
        }

        private void DGLoadDetails_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (DGLoadDetails.CurrentCell != null)
            {
                if (e.KeyCode == Keys.Enter && DGLoadDetails.CurrentCell.RowIndex != -1 && DGLoadDetails.Columns[DGLoadDetails.CurrentCell.ColumnIndex].Name != "ChkSelect")
                {
                    ((ToolStripAccess)GlobalVariables.CurrentForm).Searchs(Convert.ToInt32(DGLoadDetails.Rows[DGLoadDetails.CurrentCell.RowIndex].Cells["ID"].Value));
                }
            }
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (DGLoadDetails.CurrentCell != null)
                {
                    int ColIdx = DGLoadDetails.CurrentCell.ColumnIndex;
                    ((DataTable)DGLoadDetails.DataSource).DefaultView.RowFilter = String.Format("Convert([{0}], System.String) LIKE '{1}%'", DGLoadDetails.Columns[DGLoadDetails.CurrentCell.ColumnIndex].Name, TxtSearch.Text.ToString());
                    if (DGLoadDetails.RowCount > 0)
                    {
                        DGLoadDetails.CurrentCell = DGLoadDetails[ColIdx, 0];
                    }
                }
                else
                {
                    ((DataTable)DGLoadDetails.DataSource).DefaultView.RowFilter = null;
                }
            }
            catch { }
        }

        private void DGLoadDetails_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            DGLoadDetails.ClearSelection();
        }

        private void DGLoadDetails_KeyPress(object sender, KeyPressEventArgs e)
        {
            char key = (char)e.KeyChar;
            if (e.KeyChar == 8 && TxtSearch.Text.ToString().Length >= 1)
            {
                TxtSearch.Text = TxtSearch.Text.ToString().Substring(0, TxtSearch.Text.ToString().Length - 1);
            }
            else if (char.IsLetterOrDigit(key) || char.IsWhiteSpace(key))
            {
                TxtSearch.Text += e.KeyChar;
            }
        }

        private void CMnuEdit_Click(object sender, EventArgs e)
        {
            if (DGLoadDetails.CurrentCell != null)
            {
                ((ToolStripAccess)GlobalVariables.CurrentForm).Searchs(Convert.ToInt32(DGLoadDetails.Rows[DGLoadDetails.CurrentCell.RowIndex].Cells["ID"].Value));
            }
        }

        private void CMnuDelete_Click(object sender, EventArgs e)
        {
            if (DGLoadDetails.CurrentCell != null)
            {
                ((ToolStripAccess)GlobalVariables.CurrentForm).Deletes(Convert.ToInt32(DGLoadDetails.Rows[DGLoadDetails.CurrentCell.RowIndex].Cells["ID"].Value));
            }
        }

        private void CMnuPreview_Click(object sender, EventArgs e)
        {
          
        }
    }
}
