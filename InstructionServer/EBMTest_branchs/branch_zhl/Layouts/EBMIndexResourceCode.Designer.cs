namespace EBMTest.Layouts
{
    partial class EBMIndexResourceCode
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
            this.textResourceCode = new System.Windows.Forms.TextBox();
            this.btnDel = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.dgvResourceCode = new System.Windows.Forms.DataGridView();
            this.ColumnResourceCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResourceCode)).BeginInit();
            this.SuspendLayout();
            // 
            // textResourceCode
            // 
            this.textResourceCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textResourceCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textResourceCode.Location = new System.Drawing.Point(3, 223);
            this.textResourceCode.Name = "textResourceCode";
            this.textResourceCode.Size = new System.Drawing.Size(208, 21);
            this.textResourceCode.TabIndex = 48;
            this.textResourceCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textList_EBM_resource_code_KeyPress);
            // 
            // btnDel
            // 
            this.btnDel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDel.Location = new System.Drawing.Point(333, 221);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(111, 25);
            this.btnDel.TabIndex = 50;
            this.btnDel.Text = "删除当前资源代码";
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.btnDelResourceCode_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Location = new System.Drawing.Point(217, 221);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(111, 25);
            this.btnAdd.TabIndex = 49;
            this.btnAdd.Text = "添加资源代码";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAddResourceCode_Click);
            // 
            // dgvResourceCode
            // 
            this.dgvResourceCode.AllowUserToAddRows = false;
            this.dgvResourceCode.AllowUserToDeleteRows = false;
            this.dgvResourceCode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvResourceCode.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvResourceCode.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvResourceCode.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvResourceCode.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvResourceCode.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnResourceCode});
            this.dgvResourceCode.Location = new System.Drawing.Point(3, 0);
            this.dgvResourceCode.MultiSelect = false;
            this.dgvResourceCode.Name = "dgvResourceCode";
            this.dgvResourceCode.ReadOnly = true;
            this.dgvResourceCode.RowHeadersVisible = false;
            this.dgvResourceCode.RowTemplate.Height = 23;
            this.dgvResourceCode.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvResourceCode.Size = new System.Drawing.Size(441, 215);
            this.dgvResourceCode.TabIndex = 51;
            // 
            // ColumnResourceCode
            // 
            this.ColumnResourceCode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColumnResourceCode.DataPropertyName = "Value";
            this.ColumnResourceCode.HeaderText = "终端编号地址";
            this.ColumnResourceCode.Name = "ColumnResourceCode";
            this.ColumnResourceCode.ReadOnly = true;
            // 
            // EBMIndexResourceCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvResourceCode);
            this.Controls.Add(this.textResourceCode);
            this.Controls.Add(this.btnDel);
            this.Controls.Add(this.btnAdd);
            this.DoubleBuffered = true;
            this.Name = "EBMIndexResourceCode";
            this.Size = new System.Drawing.Size(448, 251);
            ((System.ComponentModel.ISupportInitialize)(this.dgvResourceCode)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textResourceCode;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.DataGridView dgvResourceCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnResourceCode;
    }
}
