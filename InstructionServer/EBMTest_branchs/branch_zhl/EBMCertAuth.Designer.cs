namespace EBMTest
{
    partial class EBMCertAuth
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lbl = new System.Windows.Forms.Label();
            this.MenuStripEBCertAuth = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MenuItemAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemDel = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.dgvEBCertAuth = new System.Windows.Forms.DataGridView();
            this.ColumnAuthSendState = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ColumnCertAuth_data = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvEBCert = new System.Windows.Forms.DataGridView();
            this.ColumnCertSendState = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ColumnCert_data = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.richTextData = new System.Windows.Forms.RichTextBox();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AddCertAuthBtn = new EBMTest.Layouts.AddCertAuth();
            this.pnlRepeatTimes = new EBMTest.Layouts.RepeatTimesLayout();
            this.MenuStripEBCertAuth.SuspendLayout();
            this.tableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEBCertAuth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEBCert)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl
            // 
            this.lbl.AutoSize = true;
            this.lbl.Location = new System.Drawing.Point(164, 10);
            this.lbl.Name = "lbl";
            this.lbl.Size = new System.Drawing.Size(89, 12);
            this.lbl.TabIndex = 11;
            this.lbl.Text = "选择发送的次数";
            // 
            // MenuStripEBCertAuth
            // 
            this.MenuStripEBCertAuth.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemAdd,
            this.MenuItemInfo,
            this.MenuItemUpdate,
            this.MenuItemDel});
            this.MenuStripEBCertAuth.Name = "MenuStripEBIndex";
            this.MenuStripEBCertAuth.Size = new System.Drawing.Size(125, 92);
            this.MenuStripEBCertAuth.Opening += new System.ComponentModel.CancelEventHandler(this.MenuStripEBCertAuth_Opening);
            // 
            // MenuItemAdd
            // 
            this.MenuItemAdd.Name = "MenuItemAdd";
            this.MenuItemAdd.Size = new System.Drawing.Size(124, 22);
            this.MenuItemAdd.Text = "添加证书";
            this.MenuItemAdd.Click += new System.EventHandler(this.MenuItemAdd_Click);
            // 
            // MenuItemInfo
            // 
            this.MenuItemInfo.Name = "MenuItemInfo";
            this.MenuItemInfo.Size = new System.Drawing.Size(124, 22);
            this.MenuItemInfo.Text = "查看证书";
            this.MenuItemInfo.Visible = false;
            this.MenuItemInfo.Click += new System.EventHandler(this.MenuItemInfo_Click);
            // 
            // MenuItemUpdate
            // 
            this.MenuItemUpdate.Name = "MenuItemUpdate";
            this.MenuItemUpdate.Size = new System.Drawing.Size(124, 22);
            this.MenuItemUpdate.Text = "编辑证书";
            this.MenuItemUpdate.Click += new System.EventHandler(this.MenuItemUpdate_Click);
            // 
            // MenuItemDel
            // 
            this.MenuItemDel.Name = "MenuItemDel";
            this.MenuItemDel.Size = new System.Drawing.Size(124, 22);
            this.MenuItemDel.Text = "删除证书";
            this.MenuItemDel.Click += new System.EventHandler(this.MenuItemDel_Click);
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 1;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel.Controls.Add(this.dgvEBCertAuth, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.dgvEBCert, 0, 1);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 2;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 48.52941F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 51.47059F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(732, 408);
            this.tableLayoutPanel.TabIndex = 15;
            // 
            // dgvEBCertAuth
            // 
            this.dgvEBCertAuth.AllowUserToAddRows = false;
            this.dgvEBCertAuth.AllowUserToDeleteRows = false;
            this.dgvEBCertAuth.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvEBCertAuth.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvEBCertAuth.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvEBCertAuth.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvEBCertAuth.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnAuthSendState,
            this.ColumnCertAuth_data});
            this.dgvEBCertAuth.ContextMenuStrip = this.MenuStripEBCertAuth;
            this.dgvEBCertAuth.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvEBCertAuth.Location = new System.Drawing.Point(3, 3);
            this.dgvEBCertAuth.MultiSelect = false;
            this.dgvEBCertAuth.Name = "dgvEBCertAuth";
            this.dgvEBCertAuth.RowHeadersVisible = false;
            this.dgvEBCertAuth.RowTemplate.Height = 23;
            this.dgvEBCertAuth.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvEBCertAuth.Size = new System.Drawing.Size(726, 191);
            this.dgvEBCertAuth.TabIndex = 16;
            // 
            // ColumnAuthSendState
            // 
            this.ColumnAuthSendState.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ColumnAuthSendState.DataPropertyName = "SendState";
            this.ColumnAuthSendState.Frozen = true;
            this.ColumnAuthSendState.HeaderText = "是否发送";
            this.ColumnAuthSendState.Name = "ColumnAuthSendState";
            this.ColumnAuthSendState.Width = 59;
            // 
            // ColumnCertAuth_data
            // 
            this.ColumnCertAuth_data.DataPropertyName = "Cert_data";
            this.ColumnCertAuth_data.HeaderText = "应急广播证书授权列表数据";
            this.ColumnCertAuth_data.Name = "ColumnCertAuth_data";
            this.ColumnCertAuth_data.Width = 200;
            // 
            // dgvEBCert
            // 
            this.dgvEBCert.AllowUserToAddRows = false;
            this.dgvEBCert.AllowUserToDeleteRows = false;
            this.dgvEBCert.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvEBCert.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvEBCert.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvEBCert.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvEBCert.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnCertSendState,
            this.ColumnCert_data});
            this.dgvEBCert.ContextMenuStrip = this.MenuStripEBCertAuth;
            this.dgvEBCert.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvEBCert.Location = new System.Drawing.Point(3, 200);
            this.dgvEBCert.MultiSelect = false;
            this.dgvEBCert.Name = "dgvEBCert";
            this.dgvEBCert.RowHeadersVisible = false;
            this.dgvEBCert.RowTemplate.Height = 23;
            this.dgvEBCert.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvEBCert.Size = new System.Drawing.Size(726, 205);
            this.dgvEBCert.TabIndex = 5;
            this.dgvEBCert.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvEBCert_CellContentClick);
            this.dgvEBCert.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvEBCertDataError);
            // 
            // ColumnCertSendState
            // 
            this.ColumnCertSendState.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ColumnCertSendState.DataPropertyName = "SendState";
            this.ColumnCertSendState.Frozen = true;
            this.ColumnCertSendState.HeaderText = "是否发送";
            this.ColumnCertSendState.Name = "ColumnCertSendState";
            this.ColumnCertSendState.Width = 59;
            // 
            // ColumnCert_data
            // 
            this.ColumnCert_data.DataPropertyName = "Cert_data";
            this.ColumnCert_data.HeaderText = "应急广播证书数据";
            this.ColumnCert_data.Name = "ColumnCert_data";
            this.ColumnCert_data.Width = 200;
            // 
            // richTextData
            // 
            this.richTextData.BackColor = System.Drawing.SystemColors.Window;
            this.richTextData.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextData.Location = new System.Drawing.Point(0, 0);
            this.richTextData.Name = "richTextData";
            this.richTextData.ReadOnly = true;
            this.richTextData.Size = new System.Drawing.Size(732, 175);
            this.richTextData.TabIndex = 16;
            this.richTextData.Text = "";
            // 
            // splitContainer
            // 
            this.splitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer.Location = new System.Drawing.Point(0, 35);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.tableLayoutPanel);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.richTextData);
            this.splitContainer.Size = new System.Drawing.Size(732, 587);
            this.splitContainer.SplitterDistance = 408;
            this.splitContainer.TabIndex = 19;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Cert_data";
            this.dataGridViewTextBoxColumn1.HeaderText = "应急广播证书数据";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Width = 200;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "Cert_data";
            this.dataGridViewTextBoxColumn2.HeaderText = "应急广播证书授权列表数据";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 200;
            // 
            // AddCertAuthBtn
            // 
            this.AddCertAuthBtn.Location = new System.Drawing.Point(3, 6);
            this.AddCertAuthBtn.Name = "AddCertAuthBtn";
            this.AddCertAuthBtn.Size = new System.Drawing.Size(144, 23);
            this.AddCertAuthBtn.TabIndex = 20;
            this.AddCertAuthBtn.CertClick += new System.EventHandler(this.AddCertAuthBtn_CertClick);
            this.AddCertAuthBtn.CertAuthClick += new System.EventHandler(this.AddCertAuthBtn_CertAuthClick);
            // 
            // pnlRepeatTimes
            // 
            this.pnlRepeatTimes.Location = new System.Drawing.Point(260, 2);
            this.pnlRepeatTimes.Name = "pnlRepeatTimes";
            this.pnlRepeatTimes.Size = new System.Drawing.Size(262, 25);
            this.pnlRepeatTimes.TabIndex = 0;
            // 
            // EBMCertAuth
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(733, 624);
            this.Controls.Add(this.AddCertAuthBtn);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.lbl);
            this.Controls.Add(this.pnlRepeatTimes);
            this.DoubleBuffered = true;
            this.Name = "EBMCertAuth";
            this.ShowIcon = false;
            this.Text = "应急广播证书授权协议";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EBMCertAuth_FormClosing);
            this.MenuStripEBCertAuth.ResumeLayout(false);
            this.tableLayoutPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEBCertAuth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEBCert)).EndInit();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Layouts.RepeatTimesLayout pnlRepeatTimes;
        private System.Windows.Forms.Label lbl;
        private System.Windows.Forms.ContextMenuStrip MenuStripEBCertAuth;
        private System.Windows.Forms.ToolStripMenuItem MenuItemAdd;
        private System.Windows.Forms.ToolStripMenuItem MenuItemInfo;
        private System.Windows.Forms.ToolStripMenuItem MenuItemUpdate;
        private System.Windows.Forms.ToolStripMenuItem MenuItemDel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.RichTextBox richTextData;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private Layouts.AddCertAuth AddCertAuthBtn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCert_data;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ColumnCertSendState;
        private System.Windows.Forms.DataGridView dgvEBCert;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCertAuth_data;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ColumnAuthSendState;
        private System.Windows.Forms.DataGridView dgvEBCertAuth;
    }
}