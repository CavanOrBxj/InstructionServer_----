namespace EBMTest.Layouts
{
    partial class DailyBroadcastOutSwitchLayout
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
            this.cbBoxB_Switch_status = new System.Windows.Forms.ComboBox();
            this.pnlAddressType = new EBMTest.Layouts.AddressTypeLayout();
            this.groupTerminalAddress = new System.Windows.Forms.GroupBox();
            this.pnlTerminalAddress = new EBMTest.Layouts.TerminalAddressLayout();
            this.lblB_Switch_status = new System.Windows.Forms.Label();
            this.pnlBroadcastInfo.SuspendLayout();
            this.groupTerminalAddress.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlBroadcastInfo
            // 
            this.pnlBroadcastInfo.Controls.Add(this.cbBoxB_Switch_status);
            this.pnlBroadcastInfo.Controls.Add(this.pnlAddressType);
            this.pnlBroadcastInfo.Controls.Add(this.groupTerminalAddress);
            this.pnlBroadcastInfo.Controls.Add(this.lblB_Switch_status);
            this.pnlBroadcastInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBroadcastInfo.Location = new System.Drawing.Point(0, 0);
            this.pnlBroadcastInfo.Name = "pnlBroadcastInfo";
            this.pnlBroadcastInfo.Size = new System.Drawing.Size(521, 372);
            this.pnlBroadcastInfo.TabIndex = 21;
            // 
            // cbBoxB_Switch_status
            // 
            this.cbBoxB_Switch_status.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBoxB_Switch_status.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbBoxB_Switch_status.FormattingEnabled = true;
            this.cbBoxB_Switch_status.Location = new System.Drawing.Point(151, 15);
            this.cbBoxB_Switch_status.Name = "cbBoxB_Switch_status";
            this.cbBoxB_Switch_status.Size = new System.Drawing.Size(245, 20);
            this.cbBoxB_Switch_status.TabIndex = 81;
            // 
            // pnlAddressType
            // 
            this.pnlAddressType.Location = new System.Drawing.Point(60, 41);
            this.pnlAddressType.Name = "pnlAddressType";
            this.pnlAddressType.Size = new System.Drawing.Size(336, 29);
            this.pnlAddressType.TabIndex = 78;
            // 
            // groupTerminalAddress
            // 
            this.groupTerminalAddress.Controls.Add(this.pnlTerminalAddress);
            this.groupTerminalAddress.Location = new System.Drawing.Point(3, 77);
            this.groupTerminalAddress.Name = "groupTerminalAddress";
            this.groupTerminalAddress.Size = new System.Drawing.Size(512, 291);
            this.groupTerminalAddress.TabIndex = 38;
            this.groupTerminalAddress.TabStop = false;
            this.groupTerminalAddress.Text = "要设置的终端编号地址列表";
            // 
            // pnlTerminalAddress
            // 
            this.pnlTerminalAddress.Location = new System.Drawing.Point(6, 20);
            this.pnlTerminalAddress.Name = "pnlTerminalAddress";
            this.pnlTerminalAddress.Size = new System.Drawing.Size(500, 265);
            this.pnlTerminalAddress.TabIndex = 0;
            // 
            // lblB_Switch_status
            // 
            this.lblB_Switch_status.AutoSize = true;
            this.lblB_Switch_status.Location = new System.Drawing.Point(66, 18);
            this.lblB_Switch_status.Name = "lblB_Switch_status";
            this.lblB_Switch_status.Size = new System.Drawing.Size(77, 12);
            this.lblB_Switch_status.TabIndex = 31;
            this.lblB_Switch_status.Text = "输出开启标识";
            // 
            // DailyBroadcastOutSwitchLayout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlBroadcastInfo);
            this.DoubleBuffered = true;
            this.Name = "DailyBroadcastOutSwitchLayout";
            this.Size = new System.Drawing.Size(521, 372);
            this.pnlBroadcastInfo.ResumeLayout(false);
            this.pnlBroadcastInfo.PerformLayout();
            this.groupTerminalAddress.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlBroadcastInfo;
        private System.Windows.Forms.Label lblB_Switch_status;
        private System.Windows.Forms.GroupBox groupTerminalAddress;
        private TerminalAddressLayout pnlTerminalAddress;
        private AddressTypeLayout pnlAddressType;
        private System.Windows.Forms.ComboBox cbBoxB_Switch_status;
    }
}
