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
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.gridRandomValues = new System.Windows.Forms.DataGridView();
            this.ctxmnuGrid = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuitemMoveUp = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitemMoveDown = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitemSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuitemAddRow = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitemDelRow = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitemSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuitemAddExample = new System.Windows.Forms.ToolStripMenuItem();
            this.colDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDataType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.colMask = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRange = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOptions = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOptionsBtn = new System.Windows.Forms.DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridRandomValues)).BeginInit();
            this.ctxmnuGrid.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(617, 226);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 0;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(523, 226);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
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
            this.cmbOutputType.Location = new System.Drawing.Point(121, 202);
            this.cmbOutputType.Name = "cmbOutputType";
            this.cmbOutputType.Size = new System.Drawing.Size(164, 21);
            this.cmbOutputType.TabIndex = 5;
            // 
            // lblOutputType
            // 
            this.lblOutputType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblOutputType.AutoSize = true;
            this.lblOutputType.Location = new System.Drawing.Point(18, 205);
            this.lblOutputType.Name = "lblOutputType";
            this.lblOutputType.Size = new System.Drawing.Size(62, 13);
            this.lblOutputType.TabIndex = 6;
            this.lblOutputType.Text = "Output type";
            // 
            // lblAmount
            // 
            this.lblAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblAmount.AutoSize = true;
            this.lblAmount.Location = new System.Drawing.Point(18, 229);
            this.lblAmount.Name = "lblAmount";
            this.lblAmount.Size = new System.Drawing.Size(43, 13);
            this.lblAmount.TabIndex = 2;
            this.lblAmount.Text = "Amount";
            // 
            // txtAmount
            // 
            this.txtAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtAmount.Location = new System.Drawing.Point(121, 229);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(54, 20);
            this.txtAmount.TabIndex = 3;
            this.txtAmount.Text = "100";
            this.txtAmount.Validating += new System.ComponentModel.CancelEventHandler(this.txtAmount_Validating);
            // 
            // gridRandomValues
            // 
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
            this.gridRandomValues.Size = new System.Drawing.Size(678, 173);
            this.gridRandomValues.TabIndex = 7;
            this.gridRandomValues.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridRandomValues_CellContentClick);
            // 
            // ctxmnuGrid
            // 
            this.ctxmnuGrid.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuitemMoveUp,
            this.menuitemMoveDown,
            this.menuitemSeparator1,
            this.menuitemAddRow,
            this.menuitemDelRow,
            this.menuitemSeparator2,
            this.menuitemAddExample});
            this.ctxmnuGrid.Name = "ctxmnuGrid";
            this.ctxmnuGrid.Size = new System.Drawing.Size(168, 126);
            this.ctxmnuGrid.Opening += new System.ComponentModel.CancelEventHandler(this.ctxmnuGrid_Opening);
            // 
            // menuitemMoveUp
            // 
            this.menuitemMoveUp.Name = "menuitemMoveUp";
            this.menuitemMoveUp.Size = new System.Drawing.Size(167, 22);
            this.menuitemMoveUp.Tag = "0";
            this.menuitemMoveUp.Text = "Move row up";
            this.menuitemMoveUp.Click += new System.EventHandler(this.menuitemMoveRow_Click);
            // 
            // menuitemMoveDown
            // 
            this.menuitemMoveDown.Name = "menuitemMoveDown";
            this.menuitemMoveDown.Size = new System.Drawing.Size(167, 22);
            this.menuitemMoveDown.Tag = "1";
            this.menuitemMoveDown.Text = "Move row down";
            this.menuitemMoveDown.Click += new System.EventHandler(this.menuitemMoveRow_Click);
            // 
            // menuitemSeparator1
            // 
            this.menuitemSeparator1.Name = "menuitemSeparator1";
            this.menuitemSeparator1.Size = new System.Drawing.Size(164, 6);
            // 
            // menuitemAddRow
            // 
            this.menuitemAddRow.Name = "menuitemAddRow";
            this.menuitemAddRow.Size = new System.Drawing.Size(167, 22);
            this.menuitemAddRow.Text = "Add row";
            this.menuitemAddRow.Click += new System.EventHandler(this.menuitemAddRow_Click);
            // 
            // menuitemDelRow
            // 
            this.menuitemDelRow.Name = "menuitemDelRow";
            this.menuitemDelRow.Size = new System.Drawing.Size(167, 22);
            this.menuitemDelRow.Text = "Remove row";
            this.menuitemDelRow.Click += new System.EventHandler(this.menuitemDelRow_Click);
            // 
            // menuitemSeparator2
            // 
            this.menuitemSeparator2.Name = "menuitemSeparator2";
            this.menuitemSeparator2.Size = new System.Drawing.Size(164, 6);
            // 
            // menuitemAddExample
            // 
            this.menuitemAddExample.Name = "menuitemAddExample";
            this.menuitemAddExample.Size = new System.Drawing.Size(167, 22);
            this.menuitemAddExample.Text = "Add example row";
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
            this.colOptionsBtn.Width = 48;
            // 
            // GenerateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(704, 261);
            this.Controls.Add(this.gridRandomValues);
            this.Controls.Add(this.lblOutputType);
            this.Controls.Add(this.cmbOutputType);
            this.Controls.Add(this.txtAmount);
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
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ComboBox cmbOutputType;
        private System.Windows.Forms.Label lblOutputType;
        private System.Windows.Forms.Label lblAmount;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.DataGridView gridRandomValues;
        private System.Windows.Forms.ContextMenuStrip ctxmnuGrid;
        private System.Windows.Forms.ToolStripMenuItem menuitemMoveUp;
        private System.Windows.Forms.ToolStripMenuItem menuitemMoveDown;
        private System.Windows.Forms.ToolStripSeparator menuitemSeparator1;
        private System.Windows.Forms.ToolStripMenuItem menuitemAddRow;
        private System.Windows.Forms.ToolStripMenuItem menuitemDelRow;
        private System.Windows.Forms.ToolStripSeparator menuitemSeparator2;
        private System.Windows.Forms.ToolStripMenuItem menuitemAddExample;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDescription;
        private System.Windows.Forms.DataGridViewComboBoxColumn colDataType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMask;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRange;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOptions;
        private System.Windows.Forms.DataGridViewButtonColumn colOptionsBtn;
    }
}