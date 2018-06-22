namespace EBMTest.Layouts
{
    partial class DailyBroadcastPlayCtrlLayout
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
            this.pnlAddressType = new EBMTest.Layouts.AddressTypeLayout();
            this.groupTerminalAddress = new System.Windows.Forms.GroupBox();
            this.pnlTerminalAddress = new EBMTest.Layouts.TerminalAddressLayout();
            this.textVolume = new System.Windows.Forms.TextBox();
            this.lblVolume = new System.Windows.Forms.Label();
            this.pnlBroadcastInfo.SuspendLayout();
            this.groupTerminalAddress.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlBroadcastInfo
            // 
            this.pnlBroadcastInfo.Controls.Add(this.pnlAddressType);
            this.pnlBroadcastInfo.Controls.Add(this.groupTerminalAddress);
            this.pnlBroadcastInfo.Controls.Add(this.textVolume);
            this.pnlBroadcastInfo.Controls.Add(this.lblVolume);
            this.pnlBroadcastInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBroadcastInfo.Location = new System.Drawing.Point(0, 0);
            this.pnlBroadcastInfo.Name = "pnlBroadcastInfo";
            this.pnlBroadcastInfo.Size = new System.Drawing.Size(519, 370);
            this.pnlBroadcastInfo.TabIndex = 21;
            // 
            // pnlAddressType
            // 
            this.pnlAddressType.Location = new System.Drawing.Point(59, 41);
            this.pnlAddressType.Name = "pnlAddressType";
            this.pnlAddressType.Size = new System.Drawing.Size(336, 29);
            this.pnlAddressType.TabIndex = 79;
            // 
            // groupTerminalAddress
            // 
            this.groupTerminalAddress.Controls.Add(this.pnlTerminalAddress);
            this.groupTerminalAddress.Location = new System.Drawing.Point(3, 75);
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
            // textVolume
            // 
            this.textVolume.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textVolume.Location = new System.Drawing.Point(149, 14);
            this.textVolume.Name = "textVolume";
            this.textVolume.Size = new System.Drawing.Size(246, 21);
            this.textVolume.TabIndex = 30;
            this.textVolume.Tag = "音量";
            // 
            // lblVolume
            // 
            this.lblVolume.AutoSize = true;
            this.lblVolume.Location = new System.Drawing.Point(66, 18);
            this.lblVolume.Name = "lblVolume";
            this.lblVolume.Size = new System.Drawing.Size(71, 12);
            this.lblVolume.TabIndex = 29;
            this.lblVolume.Text = "音量(0-100)";
            // 
            // DailyBroadcastPlayCtrlLayout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlBroadcastInfo);
            this.DoubleBuffered = true;
            this.Name = "DailyBroadcastPlayCtrlLayout";
            this.Size = new System.Drawing.Size(519, 370);
            this.pnlBroadcastInfo.ResumeLayout(false);
            this.pnlBroadcastInfo.PerformLayout();
            this.groupTerminalAddress.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlBroadcastInfo;
        private System.Windows.Forms.TextBox textVolume;
        private System.Windows.Forms.Label lblVolume;
        private System.Windows.Forms.GroupBox groupTerminalAddress;
        private TerminalAddressLayout pnlTerminalAddress;
        private AddressTypeLayout pnlAddressType;
    }
}
