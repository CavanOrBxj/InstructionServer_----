namespace EBMTest.Layouts
{
    partial class TerminalAddressLayout
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
            this.dgvTerminalAddress = new System.Windows.Forms.DataGridView();
            this.textAddress = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnDel = new System.Windows.Forms.Button();
            this.ColumnTerminalAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTerminalAddress)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvTerminalAddress
            // 
            this.dgvTerminalAddress.AllowUserToAddRows = false;
            this.dgvTerminalAddress.AllowUserToDeleteRows = false;
            this.dgvTerminalAddress.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvTerminalAddress.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvTerminalAddress.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvTerminalAddress.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvTerminalAddress.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvTerminalAddress.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnTerminalAddress});
            this.dgvTerminalAddress.Location = new System.Drawing.Point(0, 0);
            this.dgvTerminalAddress.MultiSelect = false;
            this.dgvTerminalAddress.Name = "dgvTerminalAddress";
            this.dgvTerminalAddress.ReadOnly = true;
            this.dgvTerminalAddress.RowHeadersVisible = false;
            this.dgvTerminalAddress.RowTemplate.Height = 23;
            this.dgvTerminalAddress.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTerminalAddress.Size = new System.Drawing.Size(520, 261);
            this.dgvTerminalAddress.TabIndex = 7;
            // 
            // textAddress
            // 
            this.textAddress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textAddress.Location = new System.Drawing.Point(15, 276);
            this.textAddress.Name = "textAddress";
            this.textAddress.Size = new System.Drawing.Size(198, 21);
            this.textAddress.TabIndex = 8;
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Location = new System.Drawing.Point(236, 274);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(120, 25);
            this.btnAdd.TabIndex = 9;
            this.btnAdd.Text = "添加地址";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDel
            // 
            this.btnDel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDel.Location = new System.Drawing.Point(377, 273);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(120, 25);
            this.btnDel.TabIndex = 10;
            this.btnDel.Text = "删除当前选中地址";
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // ColumnTerminalAddress
            // 
            this.ColumnTerminalAddress.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColumnTerminalAddress.DataPropertyName = "Value";
            this.ColumnTerminalAddress.HeaderText = "终端编号地址";
            this.ColumnTerminalAddress.Name = "ColumnTerminalAddress";
            this.ColumnTerminalAddress.ReadOnly = true;
            // 
            // TerminalAddressLayout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnDel);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.textAddress);
            this.Controls.Add(this.dgvTerminalAddress);
            this.DoubleBuffered = true;
            this.Name = "TerminalAddressLayout";
            this.Size = new System.Drawing.Size(520, 311);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTerminalAddress)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvTerminalAddress;
        private System.Windows.Forms.TextBox textAddress;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnTerminalAddress;
    }
}
