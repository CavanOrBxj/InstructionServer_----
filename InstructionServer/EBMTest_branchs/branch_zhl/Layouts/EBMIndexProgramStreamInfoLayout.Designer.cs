namespace EBMTest.Layouts
{
    partial class EBMIndexProgramStreamInfoLayout
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
            this.dgvProgramStreamInfo = new System.Windows.Forms.DataGridView();
            this.ColumnStreamType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.ColumnElementaryPID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnDes2 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.groupBoxEdit = new System.Windows.Forms.GroupBox();
            this.btnDel = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.pnlIndexDes2 = new EBMTest.Layouts.EBMIndexDes();
            this.cbBoxB_stream_type = new System.Windows.Forms.ComboBox();
            this.textS_elementary_PID = new System.Windows.Forms.TextBox();
            this.lblS_elementary_PID = new System.Windows.Forms.Label();
            this.lblB_stream_type = new System.Windows.Forms.Label();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProgramStreamInfo)).BeginInit();
            this.groupBoxEdit.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvProgramStreamInfo
            // 
            this.dgvProgramStreamInfo.AllowUserToAddRows = false;
            this.dgvProgramStreamInfo.AllowUserToDeleteRows = false;
            this.dgvProgramStreamInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvProgramStreamInfo.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvProgramStreamInfo.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvProgramStreamInfo.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvProgramStreamInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvProgramStreamInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnStreamType,
            this.ColumnElementaryPID,
            this.ColumnDes2});
            this.dgvProgramStreamInfo.Location = new System.Drawing.Point(4, 5);
            this.dgvProgramStreamInfo.MultiSelect = false;
            this.dgvProgramStreamInfo.Name = "dgvProgramStreamInfo";
            this.dgvProgramStreamInfo.RowHeadersVisible = false;
            this.dgvProgramStreamInfo.RowTemplate.Height = 23;
            this.dgvProgramStreamInfo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProgramStreamInfo.Size = new System.Drawing.Size(460, 185);
            this.dgvProgramStreamInfo.TabIndex = 83;
            this.dgvProgramStreamInfo.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProgramStreamInfo_CellContentClick);
            // 
            // ColumnStreamType
            // 
            this.ColumnStreamType.DataPropertyName = "B_stream_type";
            this.ColumnStreamType.HeaderText = "流类型";
            this.ColumnStreamType.Name = "ColumnStreamType";
            // 
            // ColumnElementaryPID
            // 
            this.ColumnElementaryPID.DataPropertyName = "S_elementary_PID";
            this.ColumnElementaryPID.HeaderText = "详情频道基本PID";
            this.ColumnElementaryPID.Name = "ColumnElementaryPID";
            // 
            // ColumnDes2
            // 
            this.ColumnDes2.HeaderText = "详情频道2类元素描述符";
            this.ColumnDes2.Name = "ColumnDes2";
            this.ColumnDes2.Text = "编辑";
            this.ColumnDes2.UseColumnTextForButtonValue = true;
            // 
            // groupBoxEdit
            // 
            this.groupBoxEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxEdit.Controls.Add(this.btnDel);
            this.groupBoxEdit.Controls.Add(this.btnAdd);
            this.groupBoxEdit.Controls.Add(this.pnlIndexDes2);
            this.groupBoxEdit.Controls.Add(this.cbBoxB_stream_type);
            this.groupBoxEdit.Controls.Add(this.textS_elementary_PID);
            this.groupBoxEdit.Controls.Add(this.lblS_elementary_PID);
            this.groupBoxEdit.Controls.Add(this.lblB_stream_type);
            this.groupBoxEdit.Location = new System.Drawing.Point(4, 202);
            this.groupBoxEdit.Name = "groupBoxEdit";
            this.groupBoxEdit.Size = new System.Drawing.Size(460, 214);
            this.groupBoxEdit.TabIndex = 84;
            this.groupBoxEdit.TabStop = false;
            this.groupBoxEdit.Text = "编辑流信息";
            // 
            // btnDel
            // 
            this.btnDel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDel.Location = new System.Drawing.Point(245, 179);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(189, 28);
            this.btnDel.TabIndex = 97;
            this.btnDel.Text = "删除当前选中节目流信息";
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Location = new System.Drawing.Point(16, 179);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(189, 28);
            this.btnAdd.TabIndex = 96;
            this.btnAdd.Text = "添加详情频道节目流信息";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // pnlIndexDes2
            // 
            this.pnlIndexDes2.Location = new System.Drawing.Point(16, 73);
            this.pnlIndexDes2.Name = "pnlIndexDes2";
            this.pnlIndexDes2.Size = new System.Drawing.Size(428, 102);
            this.pnlIndexDes2.TabIndex = 95;
            // 
            // cbBoxB_stream_type
            // 
            this.cbBoxB_stream_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBoxB_stream_type.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbBoxB_stream_type.FormattingEnabled = true;
            this.cbBoxB_stream_type.Items.AddRange(new object[] {
            "是",
            "否"});
            this.cbBoxB_stream_type.Location = new System.Drawing.Point(134, 21);
            this.cbBoxB_stream_type.Name = "cbBoxB_stream_type";
            this.cbBoxB_stream_type.Size = new System.Drawing.Size(295, 20);
            this.cbBoxB_stream_type.TabIndex = 94;
            // 
            // textS_elementary_PID
            // 
            this.textS_elementary_PID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textS_elementary_PID.Location = new System.Drawing.Point(132, 50);
            this.textS_elementary_PID.Name = "textS_elementary_PID";
            this.textS_elementary_PID.Size = new System.Drawing.Size(295, 21);
            this.textS_elementary_PID.TabIndex = 93;
            this.textS_elementary_PID.Tag = "基本PID";
            // 
            // lblS_elementary_PID
            // 
            this.lblS_elementary_PID.AutoSize = true;
            this.lblS_elementary_PID.Location = new System.Drawing.Point(29, 52);
            this.lblS_elementary_PID.Name = "lblS_elementary_PID";
            this.lblS_elementary_PID.Size = new System.Drawing.Size(47, 12);
            this.lblS_elementary_PID.TabIndex = 92;
            this.lblS_elementary_PID.Text = "基本PID";
            // 
            // lblB_stream_type
            // 
            this.lblB_stream_type.AutoSize = true;
            this.lblB_stream_type.Location = new System.Drawing.Point(29, 22);
            this.lblB_stream_type.Name = "lblB_stream_type";
            this.lblB_stream_type.Size = new System.Drawing.Size(41, 12);
            this.lblB_stream_type.TabIndex = 91;
            this.lblB_stream_type.Text = "流类型";
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "S_elementary_PID";
            this.dataGridViewTextBoxColumn1.HeaderText = "详情频道基本PID";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // EBMIndexProgramStreamInfoLayout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxEdit);
            this.Controls.Add(this.dgvProgramStreamInfo);
            this.DoubleBuffered = true;
            this.Name = "EBMIndexProgramStreamInfoLayout";
            this.Size = new System.Drawing.Size(469, 416);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProgramStreamInfo)).EndInit();
            this.groupBoxEdit.ResumeLayout(false);
            this.groupBoxEdit.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvProgramStreamInfo;
        private System.Windows.Forms.DataGridViewComboBoxColumn ColumnStreamType;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnElementaryPID;
        private System.Windows.Forms.DataGridViewButtonColumn ColumnDes2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.GroupBox groupBoxEdit;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.Button btnAdd;
        private EBMIndexDes pnlIndexDes2;
        private System.Windows.Forms.ComboBox cbBoxB_stream_type;
        private System.Windows.Forms.TextBox textS_elementary_PID;
        private System.Windows.Forms.Label lblS_elementary_PID;
        private System.Windows.Forms.Label lblB_stream_type;
    }
}
