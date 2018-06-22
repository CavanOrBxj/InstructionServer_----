﻿namespace InstructionServer.Layouts
{
    partial class DailyBroadcastRdsTransferLayout
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
            this.pnlBroadcastInfo = new System.Windows.Forms.Panel();
            this.lblTip = new System.Windows.Forms.Label();
            this.textRdsData = new System.Windows.Forms.TextBox();
            this.lblRdsData = new System.Windows.Forms.Label();
            this.cbBoxB_Rds_terminal_type = new System.Windows.Forms.ComboBox();
            this.pnlAddressType = new InstructionServer.Layouts.AddressTypeLayout();
            this.groupTerminalAddress = new System.Windows.Forms.GroupBox();
            this.pnlTerminalAddress = new InstructionServer.Layouts.TerminalAddressLayout();
            this.lblB_Rds_terminal_type = new System.Windows.Forms.Label();
            this.pnlBroadcastInfo.SuspendLayout();
            this.groupTerminalAddress.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlBroadcastInfo
            // 
            this.pnlBroadcastInfo.Controls.Add(this.lblTip);
            this.pnlBroadcastInfo.Controls.Add(this.textRdsData);
            this.pnlBroadcastInfo.Controls.Add(this.lblRdsData);
            this.pnlBroadcastInfo.Controls.Add(this.cbBoxB_Rds_terminal_type);
            this.pnlBroadcastInfo.Controls.Add(this.pnlAddressType);
            this.pnlBroadcastInfo.Controls.Add(this.groupTerminalAddress);
            this.pnlBroadcastInfo.Controls.Add(this.lblB_Rds_terminal_type);
            this.pnlBroadcastInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBroadcastInfo.Location = new System.Drawing.Point(0, 0);
            this.pnlBroadcastInfo.Name = "pnlBroadcastInfo";
            this.pnlBroadcastInfo.Size = new System.Drawing.Size(630, 395);
            this.pnlBroadcastInfo.TabIndex = 22;
            // 
            // lblTip
            // 
            this.lblTip.AutoSize = true;
            this.lblTip.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTip.ForeColor = System.Drawing.Color.DimGray;
            this.lblTip.Location = new System.Drawing.Point(100, 70);
            this.lblTip.Name = "lblTip";
            this.lblTip.Padding = new System.Windows.Forms.Padding(3);
            this.lblTip.Size = new System.Drawing.Size(367, 20);
            this.lblTip.TabIndex = 84;
            this.lblTip.Text = "提示：Rds数据按十六进制输入，多个数据用,或空格分隔(如AA FF)";
            // 
            // textRdsData
            // 
            this.textRdsData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textRdsData.Location = new System.Drawing.Point(101, 43);
            this.textRdsData.Name = "textRdsData";
            this.textRdsData.Size = new System.Drawing.Size(499, 21);
            this.textRdsData.TabIndex = 83;
            this.textRdsData.Tag = "音量";
            // 
            // lblRdsData
            // 
            this.lblRdsData.AutoSize = true;
            this.lblRdsData.Location = new System.Drawing.Point(16, 46);
            this.lblRdsData.Name = "lblRdsData";
            this.lblRdsData.Size = new System.Drawing.Size(47, 12);
            this.lblRdsData.TabIndex = 82;
            this.lblRdsData.Text = "Rds数据";
            // 
            // cbBoxB_Rds_terminal_type
            // 
            this.cbBoxB_Rds_terminal_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBoxB_Rds_terminal_type.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbBoxB_Rds_terminal_type.FormattingEnabled = true;
            this.cbBoxB_Rds_terminal_type.Location = new System.Drawing.Point(386, 14);
            this.cbBoxB_Rds_terminal_type.Name = "cbBoxB_Rds_terminal_type";
            this.cbBoxB_Rds_terminal_type.Size = new System.Drawing.Size(214, 20);
            this.cbBoxB_Rds_terminal_type.TabIndex = 81;
            // 
            // pnlAddressType
            // 
            this.pnlAddressType.Location = new System.Drawing.Point(9, 8);
            this.pnlAddressType.Name = "pnlAddressType";
            this.pnlAddressType.Size = new System.Drawing.Size(253, 29);
            this.pnlAddressType.TabIndex = 78;
            // 
            // groupTerminalAddress
            // 
            this.groupTerminalAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupTerminalAddress.Controls.Add(this.pnlTerminalAddress);
            this.groupTerminalAddress.Location = new System.Drawing.Point(3, 102);
            this.groupTerminalAddress.Name = "groupTerminalAddress";
            this.groupTerminalAddress.Size = new System.Drawing.Size(622, 291);
            this.groupTerminalAddress.TabIndex = 38;
            this.groupTerminalAddress.TabStop = false;
            this.groupTerminalAddress.Text = "要设置的终端编号地址列表";
            // 
            // pnlTerminalAddress
            // 
            this.pnlTerminalAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlTerminalAddress.Location = new System.Drawing.Point(6, 20);
            this.pnlTerminalAddress.Name = "pnlTerminalAddress";
            this.pnlTerminalAddress.Size = new System.Drawing.Size(610, 265);
            this.pnlTerminalAddress.TabIndex = 0;
            // 
            // lblB_Rds_terminal_type
            // 
            this.lblB_Rds_terminal_type.AutoSize = true;
            this.lblB_Rds_terminal_type.Location = new System.Drawing.Point(284, 18);
            this.lblB_Rds_terminal_type.Name = "lblB_Rds_terminal_type";
            this.lblB_Rds_terminal_type.Size = new System.Drawing.Size(95, 12);
            this.lblB_Rds_terminal_type.TabIndex = 31;
            this.lblB_Rds_terminal_type.Text = "Rds覆盖终端类型";
            // 
            // DailyBroadcastRdsTransferLayout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlBroadcastInfo);
            this.Name = "DailyBroadcastRdsTransferLayout";
            this.Size = new System.Drawing.Size(630, 395);
            this.pnlBroadcastInfo.ResumeLayout(false);
            this.pnlBroadcastInfo.PerformLayout();
            this.groupTerminalAddress.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlBroadcastInfo;
        private System.Windows.Forms.ComboBox cbBoxB_Rds_terminal_type;
        private AddressTypeLayout pnlAddressType;
        private System.Windows.Forms.GroupBox groupTerminalAddress;
        private TerminalAddressLayout pnlTerminalAddress;
        private System.Windows.Forms.Label lblB_Rds_terminal_type;
        private System.Windows.Forms.TextBox textRdsData;
        private System.Windows.Forms.Label lblRdsData;
        private System.Windows.Forms.Label lblTip;
    }
}
