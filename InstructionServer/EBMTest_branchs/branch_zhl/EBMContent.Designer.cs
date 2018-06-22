namespace EBMTest
{
    partial class EBMContent
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
            this.dgvEBContent = new System.Windows.Forms.DataGridView();
            this.ColumnSendState = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ColumnB_code_character_set = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.ColumnB_message_text = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnS_language_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnB_auxiliary_data = new System.Windows.Forms.DataGridViewButtonColumn();
            this.MenuStripEBContent = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MenuItemAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemDel = new System.Windows.Forms.ToolStripMenuItem();
            this.lbl = new System.Windows.Forms.Label();
            this.cbBoxEBMId = new System.Windows.Forms.ComboBox();
            this.lblEBMId = new System.Windows.Forms.Label();
            this.richTextData = new System.Windows.Forms.RichTextBox();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.pnlRepeatTimes = new EBMTest.Layouts.RepeatTimesLayout();
            this.btnAdd = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEBContent)).BeginInit();
            this.MenuStripEBContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvEBContent
            // 
            this.dgvEBContent.AllowUserToAddRows = false;
            this.dgvEBContent.AllowUserToDeleteRows = false;
            this.dgvEBContent.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvEBContent.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvEBContent.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvEBContent.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvEBContent.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnSendState,
            this.ColumnB_code_character_set,
            this.ColumnB_message_text,
            this.ColumnS_language_code,
            this.ColumnB_auxiliary_data});
            this.dgvEBContent.ContextMenuStrip = this.MenuStripEBContent;
            this.dgvEBContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvEBContent.Location = new System.Drawing.Point(0, 0);
            this.dgvEBContent.MultiSelect = false;
            this.dgvEBContent.Name = "dgvEBContent";
            this.dgvEBContent.RowHeadersVisible = false;
            this.dgvEBContent.RowTemplate.Height = 23;
            this.dgvEBContent.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvEBContent.Size = new System.Drawing.Size(722, 274);
            this.dgvEBContent.TabIndex = 4;
            this.dgvEBContent.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvEBContent_CellContentClick);
            this.dgvEBContent.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvEBContent_DataError);
            // 
            // ColumnSendState
            // 
            this.ColumnSendState.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ColumnSendState.DataPropertyName = "SendState";
            this.ColumnSendState.Frozen = true;
            this.ColumnSendState.HeaderText = "是否发送";
            this.ColumnSendState.Name = "ColumnSendState";
            this.ColumnSendState.Width = 59;
            // 
            // ColumnB_code_character_set
            // 
            this.ColumnB_code_character_set.DataPropertyName = "B_code_character_set";
            this.ColumnB_code_character_set.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ColumnB_code_character_set.Frozen = true;
            this.ColumnB_code_character_set.HeaderText = "编码字符集";
            this.ColumnB_code_character_set.Name = "ColumnB_code_character_set";
            // 
            // ColumnB_message_text
            // 
            this.ColumnB_message_text.DataPropertyName = "MessageText";
            this.ColumnB_message_text.HeaderText = "文本内容";
            this.ColumnB_message_text.Name = "ColumnB_message_text";
            // 
            // ColumnS_language_code
            // 
            this.ColumnS_language_code.DataPropertyName = "S_language_code";
            this.ColumnS_language_code.HeaderText = "语种代码";
            this.ColumnS_language_code.Name = "ColumnS_language_code";
            // 
            // ColumnB_auxiliary_data
            // 
            this.ColumnB_auxiliary_data.HeaderText = "辅助数据类型";
            this.ColumnB_auxiliary_data.Name = "ColumnB_auxiliary_data";
            this.ColumnB_auxiliary_data.Text = "编辑";
            this.ColumnB_auxiliary_data.UseColumnTextForButtonValue = true;
            // 
            // MenuStripEBContent
            // 
            this.MenuStripEBContent.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemAdd,
            this.MenuItemInfo,
            this.MenuItemUpdate,
            this.MenuItemDel});
            this.MenuStripEBContent.Name = "MenuStripEBIndex";
            this.MenuStripEBContent.Size = new System.Drawing.Size(153, 114);
            this.MenuStripEBContent.Opening += new System.ComponentModel.CancelEventHandler(this.MenuStripEBContent_Opening);
            // 
            // MenuItemAdd
            // 
            this.MenuItemAdd.Name = "MenuItemAdd";
            this.MenuItemAdd.Size = new System.Drawing.Size(152, 22);
            this.MenuItemAdd.Text = "添加内容";
            this.MenuItemAdd.Click += new System.EventHandler(this.MenuItemAdd_Click);
            // 
            // MenuItemInfo
            // 
            this.MenuItemInfo.Name = "MenuItemInfo";
            this.MenuItemInfo.Size = new System.Drawing.Size(152, 22);
            this.MenuItemInfo.Text = "查看内容";
            this.MenuItemInfo.Visible = false;
            this.MenuItemInfo.Click += new System.EventHandler(this.MenuItemInfo_Click);
            // 
            // MenuItemUpdate
            // 
            this.MenuItemUpdate.Name = "MenuItemUpdate";
            this.MenuItemUpdate.Size = new System.Drawing.Size(152, 22);
            this.MenuItemUpdate.Text = "编辑内容";
            this.MenuItemUpdate.Click += new System.EventHandler(this.MenuItemUpdate_Click);
            // 
            // MenuItemDel
            // 
            this.MenuItemDel.Name = "MenuItemDel";
            this.MenuItemDel.Size = new System.Drawing.Size(152, 22);
            this.MenuItemDel.Text = "删除内容";
            this.MenuItemDel.Click += new System.EventHandler(this.MenuItemDel_Click);
            // 
            // lbl
            // 
            this.lbl.AutoSize = true;
            this.lbl.Location = new System.Drawing.Point(392, 11);
            this.lbl.Name = "lbl";
            this.lbl.Size = new System.Drawing.Size(89, 12);
            this.lbl.TabIndex = 13;
            this.lbl.Text = "选择发送的次数";
            // 
            // cbBoxEBMId
            // 
            this.cbBoxEBMId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBoxEBMId.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbBoxEBMId.FormattingEnabled = true;
            this.cbBoxEBMId.Location = new System.Drawing.Point(175, 7);
            this.cbBoxEBMId.Name = "cbBoxEBMId";
            this.cbBoxEBMId.Size = new System.Drawing.Size(211, 20);
            this.cbBoxEBMId.TabIndex = 14;
            // 
            // lblEBMId
            // 
            this.lblEBMId.AutoSize = true;
            this.lblEBMId.Location = new System.Drawing.Point(92, 10);
            this.lblEBMId.Name = "lblEBMId";
            this.lblEBMId.Size = new System.Drawing.Size(77, 12);
            this.lblEBMId.TabIndex = 15;
            this.lblEBMId.Text = "对应索引表ID";
            // 
            // richTextData
            // 
            this.richTextData.BackColor = System.Drawing.SystemColors.Window;
            this.richTextData.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextData.Location = new System.Drawing.Point(0, 0);
            this.richTextData.Name = "richTextData";
            this.richTextData.ReadOnly = true;
            this.richTextData.Size = new System.Drawing.Size(722, 164);
            this.richTextData.TabIndex = 16;
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
            this.splitContainer.Panel1.Controls.Add(this.dgvEBContent);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.richTextData);
            this.splitContainer.Size = new System.Drawing.Size(722, 442);
            this.splitContainer.SplitterDistance = 274;
            this.splitContainer.TabIndex = 18;
            // 
            // pnlRepeatTimes
            // 
            this.pnlRepeatTimes.Location = new System.Drawing.Point(485, 3);
            this.pnlRepeatTimes.Name = "pnlRepeatTimes";
            this.pnlRepeatTimes.Size = new System.Drawing.Size(232, 25);
            this.pnlRepeatTimes.TabIndex = 12;
            // 
            // btnAdd
            // 
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Location = new System.Drawing.Point(8, 5);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(78, 23);
            this.btnAdd.TabIndex = 19;
            this.btnAdd.Text = "添加内容";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // EBMContent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(722, 479);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.lblEBMId);
            this.Controls.Add(this.cbBoxEBMId);
            this.Controls.Add(this.lbl);
            this.Controls.Add(this.pnlRepeatTimes);
            this.DoubleBuffered = true;
            this.Name = "EBMContent";
            this.ShowIcon = false;
            this.Text = "应急广播消息内容表";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EBMContent_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEBContent)).EndInit();
            this.MenuStripEBContent.ResumeLayout(false);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dgvEBContent;
        private System.Windows.Forms.Label lbl;
        private Layouts.RepeatTimesLayout pnlRepeatTimes;
        private System.Windows.Forms.ContextMenuStrip MenuStripEBContent;
        private System.Windows.Forms.ToolStripMenuItem MenuItemAdd;
        private System.Windows.Forms.ToolStripMenuItem MenuItemInfo;
        private System.Windows.Forms.ToolStripMenuItem MenuItemUpdate;
        private System.Windows.Forms.ToolStripMenuItem MenuItemDel;
        private System.Windows.Forms.ComboBox cbBoxEBMId;
        private System.Windows.Forms.Label lblEBMId;
        private System.Windows.Forms.RichTextBox richTextData;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ColumnSendState;
        private System.Windows.Forms.DataGridViewComboBoxColumn ColumnB_code_character_set;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnB_message_text;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnS_language_code;
        private System.Windows.Forms.DataGridViewButtonColumn ColumnB_auxiliary_data;
    }
}