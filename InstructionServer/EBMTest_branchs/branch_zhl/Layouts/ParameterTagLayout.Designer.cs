namespace EBMTest.Layouts
{
    partial class ParameterTagLayout
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
            this.dgvParameterTag = new System.Windows.Forms.DataGridView();
            this.ColumnParameter = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnDel = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.cbBoxParameterTag = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvParameterTag)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvParameterTag
            // 
            this.dgvParameterTag.AllowUserToAddRows = false;
            this.dgvParameterTag.AllowUserToDeleteRows = false;
            this.dgvParameterTag.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvParameterTag.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvParameterTag.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvParameterTag.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvParameterTag.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvParameterTag.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnParameter});
            this.dgvParameterTag.Location = new System.Drawing.Point(1, 2);
            this.dgvParameterTag.MultiSelect = false;
            this.dgvParameterTag.Name = "dgvParameterTag";
            this.dgvParameterTag.ReadOnly = true;
            this.dgvParameterTag.RowHeadersVisible = false;
            this.dgvParameterTag.RowTemplate.Height = 23;
            this.dgvParameterTag.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvParameterTag.Size = new System.Drawing.Size(367, 244);
            this.dgvParameterTag.TabIndex = 8;
            // 
            // ColumnParameter
            // 
            this.ColumnParameter.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ColumnParameter.DataPropertyName = "Value";
            this.ColumnParameter.HeaderText = "参数";
            this.ColumnParameter.Name = "ColumnParameter";
            this.ColumnParameter.ReadOnly = true;
            this.ColumnParameter.Width = 54;
            // 
            // btnDel
            // 
            this.btnDel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDel.Location = new System.Drawing.Point(271, 252);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(94, 25);
            this.btnDel.TabIndex = 13;
            this.btnDel.Text = "删除选中参数";
            this.btnDel.UseVisualStyleBackColor = true;
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Location = new System.Drawing.Point(171, 252);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(94, 25);
            this.btnAdd.TabIndex = 12;
            this.btnAdd.Text = "添加参数";
            this.btnAdd.UseVisualStyleBackColor = true;
            // 
            // cbBoxParameterTag
            // 
            this.cbBoxParameterTag.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbBoxParameterTag.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBoxParameterTag.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbBoxParameterTag.FormattingEnabled = true;
            this.cbBoxParameterTag.Location = new System.Drawing.Point(7, 255);
            this.cbBoxParameterTag.Name = "cbBoxParameterTag";
            this.cbBoxParameterTag.Size = new System.Drawing.Size(156, 20);
            this.cbBoxParameterTag.TabIndex = 81;
            // 
            // ParameterTagLayout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbBoxParameterTag);
            this.Controls.Add(this.btnDel);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.dgvParameterTag);
            this.DoubleBuffered = true;
            this.Name = "ParameterTagLayout";
            this.Size = new System.Drawing.Size(371, 282);
            ((System.ComponentModel.ISupportInitialize)(this.dgvParameterTag)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvParameterTag;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnParameter;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.ComboBox cbBoxParameterTag;
    }
}
