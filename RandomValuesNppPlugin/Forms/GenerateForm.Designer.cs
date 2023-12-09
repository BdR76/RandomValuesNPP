namespace RandomValuesNppPlugin
{
    partial class GenerateForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cmbOutputType = new System.Windows.Forms.ComboBox();
            this.lblOutputType = new System.Windows.Forms.Label();
            this.lblAmount = new System.Windows.Forms.Label();
            this.gridRandomValues = new System.Windows.Forms.DataGridView();
            this.colDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDataType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.colMask = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRange = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOptions = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOptionsBtn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.ctxmnuGrid = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuitemMoveUp = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitemMoveDown = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitemSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuitemAddRow = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitemDelRow = new System.Windows.Forms.ToolStripMenuItem();
            this.btnMoveUp = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnMoveDown = new System.Windows.Forms.Button();
            this.lblTablename = new System.Windows.Forms.Label();
            this.txtTablename = new System.Windows.Forms.TextBox();
            this.pnlSQL = new System.Windows.Forms.Panel();
            this.cmbSQLtype = new System.Windows.Forms.ComboBox();
            this.numBatch = new System.Windows.Forms.NumericUpDown();
            this.lblBatch = new System.Windows.Forms.Label();
            this.lblSQLtype = new System.Windows.Forms.Label();
            this.numAmount = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.gridRandomValues)).BeginInit();
            this.ctxmnuGrid.SuspendLayout();
            this.pnlSQL.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numBatch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAmount)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(665, 310);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(571, 310);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // cmbOutputType
            // 
            this.cmbOutputType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmbOutputType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOutputType.FormattingEnabled = true;
            this.cmbOutputType.Items.AddRange(new object[] {
            "Comma separated (CSV)",
            "Tab separated",
            "Semi-colon separated",
            "SQL",
            "XML",
            "JSON"});
            this.cmbOutputType.Location = new System.Drawing.Point(82, 285);
            this.cmbOutputType.Name = "cmbOutputType";
            this.cmbOutputType.Size = new System.Drawing.Size(178, 21);
            this.cmbOutputType.TabIndex = 7;
            this.cmbOutputType.SelectedIndexChanged += new System.EventHandler(this.cmbOutputType_SelectedIndexChanged);
            // 
            // lblOutputType
            // 
            this.lblOutputType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblOutputType.AutoSize = true;
            this.lblOutputType.Location = new System.Drawing.Point(14, 288);
            this.lblOutputType.Name = "lblOutputType";
            this.lblOutputType.Size = new System.Drawing.Size(62, 13);
            this.lblOutputType.TabIndex = 6;
            this.lblOutputType.Text = "Output type";
            // 
            // lblAmount
            // 
            this.lblAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblAmount.AutoSize = true;
            this.lblAmount.Location = new System.Drawing.Point(14, 316);
            this.lblAmount.Name = "lblAmount";
            this.lblAmount.Size = new System.Drawing.Size(43, 13);
            this.lblAmount.TabIndex = 2;
            this.lblAmount.Text = "Amount";
            // 
            // gridRandomValues
            // 
            this.gridRandomValues.AllowUserToResizeRows = false;
            this.gridRandomValues.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridRandomValues.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridRandomValues.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colDescription,
            this.colDataType,
            this.colMask,
            this.colRange,
            this.colOptions,
            this.colOptionsBtn});
            this.gridRandomValues.ContextMenuStrip = this.ctxmnuGrid;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.NullValue = "\"\"";
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridRandomValues.DefaultCellStyle = dataGridViewCellStyle1;
            this.gridRandomValues.EnableHeadersVisualStyles = false;
            this.gridRandomValues.Location = new System.Drawing.Point(14, 12);
            this.gridRandomValues.Name = "gridRandomValues";
            this.gridRandomValues.RowHeadersVisible = false;
            this.gridRandomValues.Size = new System.Drawing.Size(697, 257);
            this.gridRandomValues.TabIndex = 6;
            this.gridRandomValues.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridRandomValues_CellContentClick);
            // 
            // colDescription
            // 
            this.colDescription.DataPropertyName = "Description";
            this.colDescription.HeaderText = "Description";
            this.colDescription.Name = "colDescription";
            this.colDescription.Width = 192;
            // 
            // colDataType
            // 
            this.colDataType.DataPropertyName = "DataType";
            this.colDataType.HeaderText = "Data type";
            this.colDataType.Name = "colDataType";
            this.colDataType.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colDataType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colDataType.Width = 88;
            // 
            // colMask
            // 
            this.colMask.DataPropertyName = "Mask";
            this.colMask.HeaderText = "Mask";
            this.colMask.Name = "colMask";
            this.colMask.Width = 128;
            // 
            // colRange
            // 
            this.colRange.DataPropertyName = "Range";
            this.colRange.HeaderText = "Range";
            this.colRange.Name = "colRange";
            this.colRange.Width = 200;
            // 
            // colOptions
            // 
            this.colOptions.DataPropertyName = "Options";
            this.colOptions.HeaderText = "Options (hidden)";
            this.colOptions.Name = "colOptions";
            this.colOptions.Visible = false;
            // 
            // colOptionsBtn
            // 
            this.colOptionsBtn.HeaderText = "Options";
            this.colOptionsBtn.Name = "colOptionsBtn";
            this.colOptionsBtn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colOptionsBtn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colOptionsBtn.Text = "...";
            this.colOptionsBtn.UseColumnTextForButtonValue = true;
            this.colOptionsBtn.Width = 48;
            // 
            // ctxmnuGrid
            // 
            this.ctxmnuGrid.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuitemMoveUp,
            this.menuitemMoveDown,
            this.menuitemSeparator1,
            this.menuitemAddRow,
            this.menuitemDelRow});
            this.ctxmnuGrid.Name = "ctxmnuGrid";
            this.ctxmnuGrid.Size = new System.Drawing.Size(161, 98);
            // 
            // menuitemMoveUp
            // 
            this.menuitemMoveUp.Name = "menuitemMoveUp";
            this.menuitemMoveUp.Size = new System.Drawing.Size(160, 22);
            this.menuitemMoveUp.Tag = "0";
            this.menuitemMoveUp.Text = "Move row up";
            this.menuitemMoveUp.Click += new System.EventHandler(this.menuitemMoveRow_Click);
            // 
            // menuitemMoveDown
            // 
            this.menuitemMoveDown.Name = "menuitemMoveDown";
            this.menuitemMoveDown.Size = new System.Drawing.Size(160, 22);
            this.menuitemMoveDown.Tag = "1";
            this.menuitemMoveDown.Text = "Move row down";
            this.menuitemMoveDown.Click += new System.EventHandler(this.menuitemMoveRow_Click);
            // 
            // menuitemSeparator1
            // 
            this.menuitemSeparator1.Name = "menuitemSeparator1";
            this.menuitemSeparator1.Size = new System.Drawing.Size(157, 6);
            // 
            // menuitemAddRow
            // 
            this.menuitemAddRow.Name = "menuitemAddRow";
            this.menuitemAddRow.Size = new System.Drawing.Size(160, 22);
            this.menuitemAddRow.Text = "Add row";
            this.menuitemAddRow.Click += new System.EventHandler(this.menuitemAddRow_Click);
            // 
            // menuitemDelRow
            // 
            this.menuitemDelRow.Name = "menuitemDelRow";
            this.menuitemDelRow.Size = new System.Drawing.Size(160, 22);
            this.menuitemDelRow.Text = "Remove row";
            this.menuitemDelRow.Click += new System.EventHandler(this.menuitemDelRow_Click);
            // 
            // btnMoveUp
            // 
            this.btnMoveUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMoveUp.Image = global::RandomValuesNppPlugin.Properties.Resources.btn_move_up;
            this.btnMoveUp.Location = new System.Drawing.Point(717, 12);
            this.btnMoveUp.Name = "btnMoveUp";
            this.btnMoveUp.Size = new System.Drawing.Size(26, 24);
            this.btnMoveUp.TabIndex = 2;
            this.btnMoveUp.UseVisualStyleBackColor = true;
            this.btnMoveUp.Click += new System.EventHandler(this.btnMoveUp_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Image = global::RandomValuesNppPlugin.Properties.Resources.btn_add;
            this.btnAdd.Location = new System.Drawing.Point(717, 42);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(26, 24);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.Image = global::RandomValuesNppPlugin.Properties.Resources.btn_delete;
            this.btnDelete.Location = new System.Drawing.Point(717, 72);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(26, 24);
            this.btnDelete.TabIndex = 4;
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnMoveDown
            // 
            this.btnMoveDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMoveDown.Image = global::RandomValuesNppPlugin.Properties.Resources.btn_move_down;
            this.btnMoveDown.Location = new System.Drawing.Point(717, 102);
            this.btnMoveDown.Name = "btnMoveDown";
            this.btnMoveDown.Size = new System.Drawing.Size(26, 24);
            this.btnMoveDown.TabIndex = 5;
            this.btnMoveDown.UseVisualStyleBackColor = true;
            this.btnMoveDown.Click += new System.EventHandler(this.btnMoveDown_Click);
            // 
            // lblTablename
            // 
            this.lblTablename.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTablename.AutoSize = true;
            this.lblTablename.Location = new System.Drawing.Point(280, 288);
            this.lblTablename.Name = "lblTablename";
            this.lblTablename.Size = new System.Drawing.Size(60, 13);
            this.lblTablename.TabIndex = 2;
            this.lblTablename.Text = "Tablename";
            // 
            // txtTablename
            // 
            this.txtTablename.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtTablename.Location = new System.Drawing.Point(346, 285);
            this.txtTablename.Name = "txtTablename";
            this.txtTablename.Size = new System.Drawing.Size(174, 20);
            this.txtTablename.TabIndex = 8;
            this.txtTablename.Text = "Tablename";
            // 
            // pnlSQL
            // 
            this.pnlSQL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pnlSQL.Controls.Add(this.cmbSQLtype);
            this.pnlSQL.Controls.Add(this.numBatch);
            this.pnlSQL.Controls.Add(this.lblBatch);
            this.pnlSQL.Controls.Add(this.lblSQLtype);
            this.pnlSQL.Location = new System.Drawing.Point(152, 312);
            this.pnlSQL.Name = "pnlSQL";
            this.pnlSQL.Size = new System.Drawing.Size(367, 29);
            this.pnlSQL.TabIndex = 10;
            // 
            // cmbSQLtype
            // 
            this.cmbSQLtype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSQLtype.FormattingEnabled = true;
            this.cmbSQLtype.Items.AddRange(new object[] {
            "MySQL",
            "MS-SQL",
            "PostgreSQL"});
            this.cmbSQLtype.Location = new System.Drawing.Point(194, 0);
            this.cmbSQLtype.Name = "cmbSQLtype";
            this.cmbSQLtype.Size = new System.Drawing.Size(92, 21);
            this.cmbSQLtype.TabIndex = 12;
            // 
            // numBatch
            // 
            this.numBatch.Location = new System.Drawing.Point(44, 1);
            this.numBatch.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numBatch.Name = "numBatch";
            this.numBatch.Size = new System.Drawing.Size(64, 20);
            this.numBatch.TabIndex = 11;
            this.numBatch.Value = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            // 
            // lblBatch
            // 
            this.lblBatch.AutoSize = true;
            this.lblBatch.Location = new System.Drawing.Point(0, 4);
            this.lblBatch.Name = "lblBatch";
            this.lblBatch.Size = new System.Drawing.Size(43, 13);
            this.lblBatch.TabIndex = 4;
            this.lblBatch.Text = "/ Batch";
            // 
            // lblSQLtype
            // 
            this.lblSQLtype.AutoSize = true;
            this.lblSQLtype.Location = new System.Drawing.Point(128, 4);
            this.lblSQLtype.Name = "lblSQLtype";
            this.lblSQLtype.Size = new System.Drawing.Size(51, 13);
            this.lblSQLtype.TabIndex = 0;
            this.lblSQLtype.Text = "SQL type";
            // 
            // numAmount
            // 
            this.numAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.numAmount.Location = new System.Drawing.Point(82, 313);
            this.numAmount.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numAmount.Name = "numAmount";
            this.numAmount.Size = new System.Drawing.Size(64, 20);
            this.numAmount.TabIndex = 9;
            this.numAmount.Value = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            // 
            // GenerateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(752, 345);
            this.Controls.Add(this.numAmount);
            this.Controls.Add(this.pnlSQL);
            this.Controls.Add(this.btnMoveDown);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnMoveUp);
            this.Controls.Add(this.gridRandomValues);
            this.Controls.Add(this.lblOutputType);
            this.Controls.Add(this.cmbOutputType);
            this.Controls.Add(this.txtTablename);
            this.Controls.Add(this.lblTablename);
            this.Controls.Add(this.lblAmount);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "GenerateForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Generate random values";
            ((System.ComponentModel.ISupportInitialize)(this.gridRandomValues)).EndInit();
            this.ctxmnuGrid.ResumeLayout(false);
            this.pnlSQL.ResumeLayout(false);
            this.pnlSQL.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numBatch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAmount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnMoveUp;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnMoveDown;
        private System.Windows.Forms.DataGridView gridRandomValues;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDescription;
        private System.Windows.Forms.DataGridViewComboBoxColumn colDataType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMask;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRange;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOptions;
        private System.Windows.Forms.DataGridViewButtonColumn colOptionsBtn;
        private System.Windows.Forms.ContextMenuStrip ctxmnuGrid;
        private System.Windows.Forms.ToolStripMenuItem menuitemMoveUp;
        private System.Windows.Forms.ToolStripMenuItem menuitemMoveDown;
        private System.Windows.Forms.ToolStripSeparator menuitemSeparator1;
        private System.Windows.Forms.ToolStripMenuItem menuitemAddRow;
        private System.Windows.Forms.ToolStripMenuItem menuitemDelRow;
        private System.Windows.Forms.Label lblOutputType;
        private System.Windows.Forms.ComboBox cmbOutputType;
        private System.Windows.Forms.Label lblTablename;
        private System.Windows.Forms.TextBox txtTablename;
        private System.Windows.Forms.Label lblAmount;
        private System.Windows.Forms.NumericUpDown numAmount;
        private System.Windows.Forms.Panel pnlSQL;
        private System.Windows.Forms.Label lblBatch;
        private System.Windows.Forms.NumericUpDown numBatch;
        private System.Windows.Forms.Label lblSQLtype;
        private System.Windows.Forms.ComboBox cmbSQLtype;
    }
}