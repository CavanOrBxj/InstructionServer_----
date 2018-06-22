namespace EBMTest.Layouts
{
    partial class ContentAuxiliaryDataLayout
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnDel = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.textAuxiliaryData = new System.Windows.Forms.TextBox();
            this.dgvAuxiliaryData = new System.Windows.Forms.DataGridView();
            this.ColumnType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnData = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblType = new System.Windows.Forms.Label();
            this.lblData = new System.Windows.Forms.Label();
            this.btnOpenFile = new ControlAstro.Controls.LabelButton();
            this.cbBoxType = new System.Windows.Forms.ComboBox();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAuxiliaryData)).BeginInit();
            this.SuspendLayout();
            // 
            // btnDel
            // 
            this.btnDel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDel.Location = new System.Drawing.Point(376, 270);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(120, 25);
            this.btnDel.TabIndex = 14;
            this.btnDel.Text = "删除当前选中数据";
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Location = new System.Drawing.Point(376, 240);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(120, 25);
            this.btnAdd.TabIndex = 13;
            this.btnAdd.Text = "添加辅助数据";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // textAuxiliaryData
            // 
            this.textAuxiliaryData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textAuxiliaryData.Location = new System.Drawing.Point(101, 273);
            this.textAuxiliaryData.Name = "textAuxiliaryData";
            this.textAuxiliaryData.ReadOnly = true;
            this.textAuxiliaryData.Size = new System.Drawing.Size(216, 21);
            this.textAuxiliaryData.TabIndex = 12;
            // 
            // dgvAuxiliaryData
            // 
            this.dgvAuxiliaryData.AllowUserToAddRows = false;
            this.dgvAuxiliaryData.AllowUserToDeleteRows = false;
            this.dgvAuxiliaryData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvAuxiliaryData.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvAuxiliaryData.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvAuxiliaryData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvAuxiliaryData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvAuxiliaryData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnType,
            this.ColumnData});
            this.dgvAuxiliaryData.Location = new System.Drawing.Point(3, 3);
            this.dgvAuxiliaryData.MultiSelect = false;
            this.dgvAuxiliaryData.Name = "dgvAuxiliaryData";
            this.dgvAuxiliaryData.RowHeadersVisible = false;
            this.dgvAuxiliaryData.RowTemplate.Height = 23;
            this.dgvAuxiliaryData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAuxiliaryData.Size = new System.Drawing.Size(508, 231);
            this.dgvAuxiliaryData.TabIndex = 11;
            // 
            // ColumnType
            // 
            this.ColumnType.DataPropertyName = "TypeString";
            this.ColumnType.Frozen = true;
            this.ColumnType.HeaderText = "辅助数据类型";
            this.ColumnType.Name = "ColumnType";
            this.ColumnType.ReadOnly = true;
            // 
            // ColumnData
            // 
            this.ColumnData.DataPropertyName = "DisplayData";
            this.ColumnData.HeaderText = "辅助数据";
            this.ColumnData.Name = "ColumnData";
            this.ColumnData.ReadOnly = true;
            // 
            // lblType
            // 
            this.lblType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(18, 247);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(77, 12);
            this.lblType.TabIndex = 16;
            this.lblType.Text = "辅助数据类型";
            // 
            // lblData
            // 
            this.lblData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblData.AutoSize = true;
            this.lblData.Location = new System.Drawing.Point(18, 278);
            this.lblData.Name = "lblData";
            this.lblData.Size = new System.Drawing.Size(53, 12);
            this.lblData.TabIndex = 17;
            this.lblData.Text = "辅助数据";
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOpenFile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.btnOpenFile.Location = new System.Drawing.Point(323, 272);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(25, 22);
            this.btnOpenFile.TabIndex = 18;
            this.btnOpenFile.Text = "...";
            this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
            // 
            // cbBoxType
            // 
            this.cbBoxType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbBoxType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBoxType.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbBoxType.FormattingEnabled = true;
            this.cbBoxType.Location = new System.Drawing.Point(101, 244);
            this.cbBoxType.Name = "cbBoxType";
            this.cbBoxType.Size = new System.Drawing.Size(247, 20);
            this.cbBoxType.TabIndex = 19;
            // 
            // ContentAuxiliaryDataLayout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbBoxType);
            this.Controls.Add(this.btnOpenFile);
            this.Controls.Add(this.lblData);
            this.Controls.Add(this.lblType);
            this.Controls.Add(this.btnDel);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.textAuxiliaryData);
            this.Controls.Add(this.dgvAuxiliaryData);
            this.DoubleBuffered = true;
            this.Name = "ContentAuxiliaryDataLayout";
            this.Size = new System.Drawing.Size(514, 304);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAuxiliaryData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TextBox textAuxiliaryData;
        private System.Windows.Forms.DataGridView dgvAuxiliaryData;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.Label lblData;
        private ControlAstro.Controls.LabelButton btnOpenFile;
        private System.Windows.Forms.ComboBox cbBoxType;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnType;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnData;
    }
}
