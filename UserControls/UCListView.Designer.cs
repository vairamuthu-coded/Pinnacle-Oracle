
namespace Pinnacle.UserControls
{
    partial class UCListView
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.CMnuOptions = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.CMnuEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.CMnuDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.CMnuPreview = new System.Windows.Forms.ToolStripMenuItem();
            this.TxtSearch = new Pinnacle.UCTextBox();
            this.DGLoadDetails = new Pinnacle.UCDataGridView();
            this.CMnuOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGLoadDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // CMnuOptions
            // 
            this.CMnuOptions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CMnuEdit,
            this.CMnuDelete,
            this.CMnuPreview});
            this.CMnuOptions.Name = "CMnuOptions";
            this.CMnuOptions.Size = new System.Drawing.Size(116, 70);
            // 
            // CMnuEdit
            // 
            this.CMnuEdit.Name = "CMnuEdit";
            this.CMnuEdit.Size = new System.Drawing.Size(115, 22);
            this.CMnuEdit.Text = "Edit";
            this.CMnuEdit.Click += new System.EventHandler(this.CMnuEdit_Click);
            // 
            // CMnuDelete
            // 
            this.CMnuDelete.Name = "CMnuDelete";
            this.CMnuDelete.Size = new System.Drawing.Size(115, 22);
            this.CMnuDelete.Text = "Delete";
            this.CMnuDelete.Click += new System.EventHandler(this.CMnuDelete_Click);
            // 
            // CMnuPreview
            // 
            this.CMnuPreview.Name = "CMnuPreview";
            this.CMnuPreview.Size = new System.Drawing.Size(115, 22);
            this.CMnuPreview.Text = "Preview";
            this.CMnuPreview.Click += new System.EventHandler(this.CMnuPreview_Click);
            // 
            // TxtSearch
            // 
            this.TxtSearch.DataSource = null;
            this.TxtSearch.DataType = "String";
            this.TxtSearch.DisplayMember = null;
            this.TxtSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.TxtSearch.Enabled = false;
            this.TxtSearch.EnterKeyMoveNextTab = true;
            this.TxtSearch.ErrProvider = null;
            this.TxtSearch.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtSearch.HidePopColumns = null;
            this.TxtSearch.Location = new System.Drawing.Point(0, 0);
            this.TxtSearch.Name = "TxtSearch";
            this.TxtSearch.Size = new System.Drawing.Size(290, 23);
            this.TxtSearch.TabIndex = 2;
            this.TxtSearch.ValidateType = null;
            this.TxtSearch.ValueMember = null;
            this.TxtSearch.TextChanged += new System.EventHandler(this.TxtSearch_TextChanged);
            // 
            // DGLoadDetails
            // 
            this.DGLoadDetails.AllowUserToAddRows = false;
            this.DGLoadDetails.AllowUserToDeleteRows = false;
            this.DGLoadDetails.AllowUserToResizeRows = false;
            this.DGLoadDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DGLoadDetails.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.DGLoadDetails.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(223)))), ((int)(((byte)(247)))));
            this.DGLoadDetails.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DGLoadDetails.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(82)))), ((int)(((byte)(156)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Green;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGLoadDetails.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.DGLoadDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGLoadDetails.ContextMenuStrip = this.CMnuOptions;
            this.DGLoadDetails.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.DGLoadDetails.EnableHeadersVisualStyles = false;
            this.DGLoadDetails.Location = new System.Drawing.Point(0, 23);
            this.DGLoadDetails.MergeCells = false;
            this.DGLoadDetails.Name = "DGLoadDetails";
            this.DGLoadDetails.ReadOnly = true;
            this.DGLoadDetails.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(82)))), ((int)(((byte)(156)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Green;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGLoadDetails.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.DGLoadDetails.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToFirstHeader;
            this.DGLoadDetails.RowIndicater = false;
            this.DGLoadDetails.ShowEditingIcon = false;
            this.DGLoadDetails.ShowRowNumber = true;
            this.DGLoadDetails.Size = new System.Drawing.Size(287, 280);
            this.DGLoadDetails.TabIndex = 3;
            this.DGLoadDetails.UserDeleteRow = true;
            this.DGLoadDetails.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGLoadDetails_CellContentDoubleClick);
            this.DGLoadDetails.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGLoadDetails_CellLeave);
            this.DGLoadDetails.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DGLoadDetails_KeyPress);
            this.DGLoadDetails.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.DGLoadDetails_PreviewKeyDown);
            // 
            // ListView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(223)))), ((int)(((byte)(247)))));
            this.Controls.Add(this.DGLoadDetails);
            this.Controls.Add(this.TxtSearch);
            this.Name = "ListView";
            this.Size = new System.Drawing.Size(290, 303);
            this.CMnuOptions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DGLoadDetails)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UCTextBox TxtSearch;
        private System.Windows.Forms.ContextMenuStrip CMnuOptions;
        private System.Windows.Forms.ToolStripMenuItem CMnuEdit;
        private System.Windows.Forms.ToolStripMenuItem CMnuDelete;
        private System.Windows.Forms.ToolStripMenuItem CMnuPreview;
        private UCDataGridView DGLoadDetails;
    }
}
