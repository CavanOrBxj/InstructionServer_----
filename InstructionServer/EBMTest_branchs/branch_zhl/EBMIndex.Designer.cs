namespace EBMTest
{
    partial class EBMIndex
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
            this.dgvEBIndex = new System.Windows.Forms.DataGridView();
            this.MenuStripEBIndex = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MenuItemAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemDel = new System.Windows.Forms.ToolStripMenuItem();
            this.richTextData = new System.Windows.Forms.RichTextBox();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewSysTimeColumn1 = new EBMTest.Controls.DataGridViewSysTimeColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnAdd = new System.Windows.Forms.Button();
            this.lbl = new System.Windows.Forms.Label();
            this.pnlRepeatTimes = new EBMTest.Layouts.RepeatTimesLayout();
            this.ColumnSendState = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ColumnNickName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnNetId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnStartTime = new EBMTest.Controls.DataGridViewSysTimeColumn();
            this.ColumnEBMEndTime = new EBMTest.Controls.DataGridViewSysTimeDelayColumn();
            this.ColumnEBMType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnEBMClass = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.ColumnEBMLevel = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.ColumnEBMResourceCode = new System.Windows.Forms.DataGridViewButtonColumn();
            this.ColumnDetailChlIndicate = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ColumnChlStreamId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnProgramNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnChl_PCR_PID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnDes2 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.ColumnDesFlag = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ColumnDes1 = new System.Windows.Forms.DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEBIndex)).BeginInit();
            this.MenuStripEBIndex.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvEBIndex
            // 
            this.dgvEBIndex.AllowUserToAddRows = false;
            this.dgvEBIndex.AllowUserToDeleteRows = false;
            this.dgvEBIndex.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvEBIndex.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvEBIndex.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvEBIndex.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvEBIndex.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnSendState,
            this.ColumnNickName,
            this.ColumnId,
            this.ColumnNetId,
            this.ColumnStartTime,
            this.ColumnEBMEndTime,
            this.ColumnEBMType,
            this.ColumnEBMClass,
            this.ColumnEBMLevel,
            this.ColumnEBMResourceCode,
            this.ColumnDetailChlIndicate,
            this.ColumnChlStreamId,
            this.ColumnProgramNumber,
            this.ColumnChl_PCR_PID,
            this.ColumnDes2,
            this.ColumnDesFlag,
            this.ColumnDes1});
            this.dgvEBIndex.ContextMenuStrip = this.MenuStripEBIndex;
            this.dgvEBIndex.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvEBIndex.Location = new System.Drawing.Point(0, 0);
            this.dgvEBIndex.Margin = new System.Windows.Forms.Padding(0);
            this.dgvEBIndex.MultiSelect = false;
            this.dgvEBIndex.Name = "dgvEBIndex";
            this.dgvEBIndex.RowHeadersVisible = false;
            this.dgvEBIndex.RowTemplate.Height = 23;
            this.dgvEBIndex.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvEBIndex.Size = new System.Drawing.Size(742, 319);
            this.dgvEBIndex.TabIndex = 4;
            this.dgvEBIndex.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgvEBIndex_CellBeginEdit);
            this.dgvEBIndex.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvEBIndex_CellContentClick);
            this.dgvEBIndex.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvEBIndex_DataError);
            // 
            // MenuStripEBIndex
            // 
            this.MenuStripEBIndex.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemAdd,
            this.MenuItemInfo,
            this.MenuItemUpdate,
            this.MenuItemDel});
            this.MenuStripEBIndex.Name = "MenuStripEBIndex";
            this.MenuStripEBIndex.Size = new System.Drawing.Size(125, 92);
            this.MenuStripEBIndex.Opening += new System.ComponentModel.CancelEventHandler(this.MenuStripEBIndex_Opening);
            // 
            // MenuItemAdd
            // 
            this.MenuItemAdd.Name = "MenuItemAdd";
            this.MenuItemAdd.Size = new System.Drawing.Size(124, 22);
            this.MenuItemAdd.Text = "添加索引";
            this.MenuItemAdd.Click += new System.EventHandler(this.MenuItemAdd_Click);
            // 
            // MenuItemInfo
            // 
            this.MenuItemInfo.Name = "MenuItemInfo";
            this.MenuItemInfo.Size = new System.Drawing.Size(124, 22);
            this.MenuItemInfo.Text = "查看索引";
            this.MenuItemInfo.Click += new System.EventHandler(this.MenuItemInfo_Click);
            // 
            // MenuItemUpdate
            // 
            this.MenuItemUpdate.Name = "MenuItemUpdate";
            this.MenuItemUpdate.Size = new System.Drawing.Size(124, 22);
            this.MenuItemUpdate.Text = "编辑索引";
            this.MenuItemUpdate.Click += new System.EventHandler(this.MenuItemUpdate_Click);
            // 
            // MenuItemDel
            // 
            this.MenuItemDel.Name = "MenuItemDel";
            this.MenuItemDel.Size = new System.Drawing.Size(124, 22);
            this.MenuItemDel.Text = "删除索引";
            this.MenuItemDel.Click += new System.EventHandler(this.MenuItemDel_Click);
            // 
            // richTextData
            // 
            this.richTextData.BackColor = System.Drawing.SystemColors.Window;
            this.richTextData.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextData.Location = new System.Drawing.Point(0, 0);
            this.richTextData.Margin = new System.Windows.Forms.Padding(0);
            this.richTextData.Name = "richTextData";
            this.richTextData.ReadOnly = true;
            this.richTextData.Size = new System.Drawing.Size(742, 191);
            this.richTextData.TabIndex = 14;
            this.richTextData.Text = "";
            // 
            // splitContainer
            // 
            this.splitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer.Location = new System.Drawing.Point(0, 34);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.dgvEBIndex);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.richTextData);
            this.splitContainer.Size = new System.Drawing.Size(742, 514);
            this.splitContainer.SplitterDistance = 319;
            this.splitContainer.TabIndex = 17;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "S_EBM_id";
            this.dataGridViewTextBoxColumn1.Frozen = true;
            this.dataGridViewTextBoxColumn1.HeaderText = "消息标识符ID";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "S_EBM_original_network_id";
            this.dataGridViewTextBoxColumn2.HeaderText = "原始网络标识符ID";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewSysTimeColumn1
            // 
            this.dataGridViewSysTimeColumn1.DataPropertyName = "S_EBM_start_time";
            this.dataGridViewSysTimeColumn1.HeaderText = "消息开始时间";
            this.dataGridViewSysTimeColumn1.Name = "dataGridViewSysTimeColumn1";
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "S_EBM_start_time";
            this.dataGridViewTextBoxColumn3.HeaderText = "消息开始时间";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "S_EBM_end_time";
            this.dataGridViewTextBoxColumn4.HeaderText = "消息结束时间";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "S_EBM_type";
            this.dataGridViewTextBoxColumn5.HeaderText = "消息类别";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "S_details_channel_transport_stream_id";
            this.dataGridViewTextBoxColumn6.HeaderText = "传输流标识符";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.DataPropertyName = "S_details_channel_program_number";
            this.dataGridViewTextBoxColumn7.HeaderText = "节目号";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.DataPropertyName = "S_details_channel_PCR_PID";
            this.dataGridViewTextBoxColumn8.HeaderText = "详情频道PCR标识";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnAdd);
            this.flowLayoutPanel1.Controls.Add(this.lbl);
            this.flowLayoutPanel1.Controls.Add(this.pnlRepeatTimes);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(7, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(476, 30);
            this.flowLayoutPanel1.TabIndex = 18;
            // 
            // btnAdd
            // 
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Location = new System.Drawing.Point(3, 6);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(78, 23);
            this.btnAdd.TabIndex = 25;
            this.btnAdd.Text = "添加索引";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // lbl
            // 
            this.lbl.AutoSize = true;
            this.lbl.Location = new System.Drawing.Point(99, 11);
            this.lbl.Margin = new System.Windows.Forms.Padding(15, 11, 3, 0);
            this.lbl.Name = "lbl";
            this.lbl.Size = new System.Drawing.Size(89, 12);
            this.lbl.TabIndex = 24;
            this.lbl.Text = "选择发送的次数";
            // 
            // pnlRepeatTimes
            // 
            this.pnlRepeatTimes.IsConfig = false;
            this.pnlRepeatTimes.Location = new System.Drawing.Point(195, 3);
            this.pnlRepeatTimes.Margin = new System.Windows.Forms.Padding(4, 3, 3, 3);
            this.pnlRepeatTimes.Name = "pnlRepeatTimes";
            this.pnlRepeatTimes.Size = new System.Drawing.Size(262, 25);
            this.pnlRepeatTimes.TabIndex = 23;
            // 
            // ColumnSendState
            // 
            this.ColumnSendState.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ColumnSendState.DataPropertyName = "SendState";
            this.ColumnSendState.FalseValue = "";
            this.ColumnSendState.Frozen = true;
            this.ColumnSendState.HeaderText = "是否发送";
            this.ColumnSendState.Name = "ColumnSendState";
            this.ColumnSendState.TrueValue = "";
            this.ColumnSendState.Width = 59;
            // 
            // ColumnNickName
            // 
            this.ColumnNickName.DataPropertyName = "NickName";
            this.ColumnNickName.Frozen = true;
            this.ColumnNickName.HeaderText = "自定义名称";
            this.ColumnNickName.Name = "ColumnNickName";
            // 
            // ColumnId
            // 
            this.ColumnId.DataPropertyName = "S_EBM_id";
            this.ColumnId.Frozen = true;
            this.ColumnId.HeaderText = "消息标识符ID";
            this.ColumnId.Name = "ColumnId";
            // 
            // ColumnNetId
            // 
            this.ColumnNetId.DataPropertyName = "S_EBM_original_network_id";
            this.ColumnNetId.HeaderText = "原始网络标识符ID";
            this.ColumnNetId.Name = "ColumnNetId";
            // 
            // ColumnStartTime
            // 
            this.ColumnStartTime.DataPropertyName = "S_EBM_start_time";
            this.ColumnStartTime.HeaderText = "消息开始时间";
            this.ColumnStartTime.Name = "ColumnStartTime";
            // 
            // ColumnEBMEndTime
            // 
            this.ColumnEBMEndTime.DataPropertyName = "S_EBM_end_time";
            this.ColumnEBMEndTime.HeaderText = "消息结束时间";
            this.ColumnEBMEndTime.Name = "ColumnEBMEndTime";
            // 
            // ColumnEBMType
            // 
            this.ColumnEBMType.DataPropertyName = "S_EBM_type";
            this.ColumnEBMType.HeaderText = "消息类别";
            this.ColumnEBMType.Name = "ColumnEBMType";
            // 
            // ColumnEBMClass
            // 
            this.ColumnEBMClass.DataPropertyName = "S_EBM_class";
            this.ColumnEBMClass.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ColumnEBMClass.HeaderText = "消息分类";
            this.ColumnEBMClass.Name = "ColumnEBMClass";
            // 
            // ColumnEBMLevel
            // 
            this.ColumnEBMLevel.DataPropertyName = "S_EBM_level";
            this.ColumnEBMLevel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ColumnEBMLevel.HeaderText = "消息级别";
            this.ColumnEBMLevel.Name = "ColumnEBMLevel";
            // 
            // ColumnEBMResourceCode
            // 
            this.ColumnEBMResourceCode.HeaderText = "消息覆盖资源代码";
            this.ColumnEBMResourceCode.Name = "ColumnEBMResourceCode";
            this.ColumnEBMResourceCode.Text = "编辑";
            this.ColumnEBMResourceCode.UseColumnTextForButtonValue = true;
            // 
            // ColumnDetailChlIndicate
            // 
            this.ColumnDetailChlIndicate.DataPropertyName = "BL_details_channel_indicate";
            this.ColumnDetailChlIndicate.HeaderText = "详情频道有无标识";
            this.ColumnDetailChlIndicate.Name = "ColumnDetailChlIndicate";
            // 
            // ColumnChlStreamId
            // 
            this.ColumnChlStreamId.DataPropertyName = "S_details_channel_transport_stream_id";
            this.ColumnChlStreamId.HeaderText = "传输流标识符";
            this.ColumnChlStreamId.Name = "ColumnChlStreamId";
            // 
            // ColumnProgramNumber
            // 
            this.ColumnProgramNumber.DataPropertyName = "S_details_channel_program_number";
            this.ColumnProgramNumber.HeaderText = "节目号";
            this.ColumnProgramNumber.Name = "ColumnProgramNumber";
            // 
            // ColumnChl_PCR_PID
            // 
            this.ColumnChl_PCR_PID.DataPropertyName = "S_details_channel_PCR_PID";
            this.ColumnChl_PCR_PID.HeaderText = "详情频道PCR标识";
            this.ColumnChl_PCR_PID.Name = "ColumnChl_PCR_PID";
            // 
            // ColumnDes2
            // 
            this.ColumnDes2.HeaderText = "详情频道节目流信息列表";
            this.ColumnDes2.Name = "ColumnDes2";
            this.ColumnDes2.Text = "编辑";
            this.ColumnDes2.UseColumnTextForButtonValue = true;
            // 
            // ColumnDesFlag
            // 
            this.ColumnDesFlag.DataPropertyName = "DesFlag";
            this.ColumnDesFlag.HeaderText = "详情频道描述符标识";
            this.ColumnDesFlag.Name = "ColumnDesFlag";
            // 
            // ColumnDes1
            // 
            this.ColumnDes1.HeaderText = "详情频道描述符";
            this.ColumnDes1.Name = "ColumnDes1";
            this.ColumnDes1.Text = "编辑";
            this.ColumnDes1.UseColumnTextForButtonValue = true;
            // 
            // EBMIndex
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(743, 550);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.splitContainer);
            this.DoubleBuffered = true;
            this.Name = "EBMIndex";
            this.ShowIcon = false;
            this.Text = "应急广播消息索引表";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EBMIndex_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEBIndex)).EndInit();
            this.MenuStripEBIndex.ResumeLayout(false);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView dgvEBIndex;
        private System.Windows.Forms.ContextMenuStrip MenuStripEBIndex;
        private System.Windows.Forms.ToolStripMenuItem MenuItemInfo;
        private System.Windows.Forms.ToolStripMenuItem MenuItemAdd;
        private System.Windows.Forms.ToolStripMenuItem MenuItemDel;
        private System.Windows.Forms.ToolStripMenuItem MenuItemUpdate;
        private System.Windows.Forms.RichTextBox richTextData;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private Controls.DataGridViewSysTimeColumn dataGridViewSysTimeColumn1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label lbl;
        private Layouts.RepeatTimesLayout pnlRepeatTimes;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ColumnSendState;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnNickName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnNetId;
        private Controls.DataGridViewSysTimeColumn ColumnStartTime;
        private Controls.DataGridViewSysTimeDelayColumn ColumnEBMEndTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnEBMType;
        private System.Windows.Forms.DataGridViewComboBoxColumn ColumnEBMClass;
        private System.Windows.Forms.DataGridViewComboBoxColumn ColumnEBMLevel;
        private System.Windows.Forms.DataGridViewButtonColumn ColumnEBMResourceCode;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ColumnDetailChlIndicate;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnChlStreamId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnProgramNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnChl_PCR_PID;
        private System.Windows.Forms.DataGridViewButtonColumn ColumnDes2;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ColumnDesFlag;
        private System.Windows.Forms.DataGridViewButtonColumn ColumnDes1;
    }
}