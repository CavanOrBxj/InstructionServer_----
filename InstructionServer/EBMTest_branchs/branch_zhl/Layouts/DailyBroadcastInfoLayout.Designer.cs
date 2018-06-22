namespace EBMTest.Layouts
{
    partial class DailyBroadcastInfoLayout
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
            this.timeEndTime = new System.Windows.Forms.DateTimePicker();
            this.groupTerminalAddress = new System.Windows.Forms.GroupBox();
            this.pnlTerminalAddress = new EBMTest.Layouts.TerminalAddressLayout();
            this.lblEndTime = new System.Windows.Forms.Label();
            this.textVolume = new System.Windows.Forms.TextBox();
            this.lblVolume = new System.Windows.Forms.Label();
            this.textPriority = new System.Windows.Forms.TextBox();
            this.lblPriority = new System.Windows.Forms.Label();
            this.textProgram_PID = new System.Windows.Forms.TextBox();
            this.lblProgram_PID = new System.Windows.Forms.Label();
            this.textPCR_PID = new System.Windows.Forms.TextBox();
            this.lblPCR_PID = new System.Windows.Forms.Label();
            this.textServiceID = new System.Windows.Forms.TextBox();
            this.lblServiceID = new System.Windows.Forms.Label();
            this.textTSID = new System.Windows.Forms.TextBox();
            this.lblTSID = new System.Windows.Forms.Label();
            this.textNetID = new System.Windows.Forms.TextBox();
            this.lblNetID = new System.Windows.Forms.Label();
            this.pnlBroadcastInfo.SuspendLayout();
            this.groupTerminalAddress.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlBroadcastInfo
            // 
            this.pnlBroadcastInfo.Controls.Add(this.pnlAddressType);
            this.pnlBroadcastInfo.Controls.Add(this.timeEndTime);
            this.pnlBroadcastInfo.Controls.Add(this.groupTerminalAddress);
            this.pnlBroadcastInfo.Controls.Add(this.lblEndTime);
            this.pnlBroadcastInfo.Controls.Add(this.textVolume);
            this.pnlBroadcastInfo.Controls.Add(this.lblVolume);
            this.pnlBroadcastInfo.Controls.Add(this.textPriority);
            this.pnlBroadcastInfo.Controls.Add(this.lblPriority);
            this.pnlBroadcastInfo.Controls.Add(this.textProgram_PID);
            this.pnlBroadcastInfo.Controls.Add(this.lblProgram_PID);
            this.pnlBroadcastInfo.Controls.Add(this.textPCR_PID);
            this.pnlBroadcastInfo.Controls.Add(this.lblPCR_PID);
            this.pnlBroadcastInfo.Controls.Add(this.textServiceID);
            this.pnlBroadcastInfo.Controls.Add(this.lblServiceID);
            this.pnlBroadcastInfo.Controls.Add(this.textTSID);
            this.pnlBroadcastInfo.Controls.Add(this.lblTSID);
            this.pnlBroadcastInfo.Controls.Add(this.textNetID);
            this.pnlBroadcastInfo.Controls.Add(this.lblNetID);
            this.pnlBroadcastInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBroadcastInfo.Location = new System.Drawing.Point(0, 0);
            this.pnlBroadcastInfo.Name = "pnlBroadcastInfo";
            this.pnlBroadcastInfo.Size = new System.Drawing.Size(922, 338);
            this.pnlBroadcastInfo.TabIndex = 20;
            // 
            // pnlAddressType
            // 
            this.pnlAddressType.Location = new System.Drawing.Point(44, 291);
            this.pnlAddressType.Name = "pnlAddressType";
            this.pnlAddressType.Size = new System.Drawing.Size(329, 29);
            this.pnlAddressType.TabIndex = 77;
            // 
            // timeEndTime
            // 
            this.timeEndTime.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.timeEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.timeEndTime.Location = new System.Drawing.Point(135, 253);
            this.timeEndTime.Name = "timeEndTime";
            this.timeEndTime.Size = new System.Drawing.Size(238, 21);
            this.timeEndTime.TabIndex = 38;
            // 
            // groupTerminalAddress
            // 
            this.groupTerminalAddress.Controls.Add(this.pnlTerminalAddress);
            this.groupTerminalAddress.Location = new System.Drawing.Point(389, 17);
            this.groupTerminalAddress.Name = "groupTerminalAddress";
            this.groupTerminalAddress.Size = new System.Drawing.Size(512, 291);
            this.groupTerminalAddress.TabIndex = 37;
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
            // lblEndTime
            // 
            this.lblEndTime.AutoSize = true;
            this.lblEndTime.Location = new System.Drawing.Point(25, 257);
            this.lblEndTime.Name = "lblEndTime";
            this.lblEndTime.Size = new System.Drawing.Size(101, 12);
            this.lblEndTime.TabIndex = 31;
            this.lblEndTime.Text = "日常广播结束时间";
            // 
            // textVolume
            // 
            this.textVolume.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textVolume.Location = new System.Drawing.Point(135, 218);
            this.textVolume.Name = "textVolume";
            this.textVolume.Size = new System.Drawing.Size(238, 21);
            this.textVolume.TabIndex = 30;
            this.textVolume.Tag = "音量";
            // 
            // lblVolume
            // 
            this.lblVolume.AutoSize = true;
            this.lblVolume.Location = new System.Drawing.Point(25, 222);
            this.lblVolume.Name = "lblVolume";
            this.lblVolume.Size = new System.Drawing.Size(71, 12);
            this.lblVolume.TabIndex = 29;
            this.lblVolume.Text = "音量(0-100)";
            // 
            // textPriority
            // 
            this.textPriority.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textPriority.Location = new System.Drawing.Point(135, 185);
            this.textPriority.Name = "textPriority";
            this.textPriority.Size = new System.Drawing.Size(238, 21);
            this.textPriority.TabIndex = 28;
            this.textPriority.Tag = "节目优先级";
            // 
            // lblPriority
            // 
            this.lblPriority.AutoSize = true;
            this.lblPriority.Location = new System.Drawing.Point(25, 189);
            this.lblPriority.Name = "lblPriority";
            this.lblPriority.Size = new System.Drawing.Size(65, 12);
            this.lblPriority.TabIndex = 27;
            this.lblPriority.Text = "节目优先级";
            // 
            // textProgram_PID
            // 
            this.textProgram_PID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textProgram_PID.Location = new System.Drawing.Point(135, 151);
            this.textProgram_PID.Name = "textProgram_PID";
            this.textProgram_PID.Size = new System.Drawing.Size(238, 21);
            this.textProgram_PID.TabIndex = 26;
            this.textProgram_PID.Tag = "当前节目ID";
            // 
            // lblProgram_PID
            // 
            this.lblProgram_PID.AutoSize = true;
            this.lblProgram_PID.Location = new System.Drawing.Point(25, 155);
            this.lblProgram_PID.Name = "lblProgram_PID";
            this.lblProgram_PID.Size = new System.Drawing.Size(65, 12);
            this.lblProgram_PID.TabIndex = 25;
            this.lblProgram_PID.Text = "当前节目ID";
            // 
            // textPCR_PID
            // 
            this.textPCR_PID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textPCR_PID.Location = new System.Drawing.Point(135, 118);
            this.textPCR_PID.Name = "textPCR_PID";
            this.textPCR_PID.Size = new System.Drawing.Size(238, 21);
            this.textPCR_PID.TabIndex = 24;
            this.textPCR_PID.Tag = "PCR_PID";
            // 
            // lblPCR_PID
            // 
            this.lblPCR_PID.AutoSize = true;
            this.lblPCR_PID.Location = new System.Drawing.Point(25, 122);
            this.lblPCR_PID.Name = "lblPCR_PID";
            this.lblPCR_PID.Size = new System.Drawing.Size(47, 12);
            this.lblPCR_PID.TabIndex = 23;
            this.lblPCR_PID.Text = "PCR_PID";
            // 
            // textServiceID
            // 
            this.textServiceID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textServiceID.Location = new System.Drawing.Point(135, 84);
            this.textServiceID.Name = "textServiceID";
            this.textServiceID.Size = new System.Drawing.Size(238, 21);
            this.textServiceID.TabIndex = 22;
            this.textServiceID.Tag = "节目服务ID";
            // 
            // lblServiceID
            // 
            this.lblServiceID.AutoSize = true;
            this.lblServiceID.Location = new System.Drawing.Point(25, 88);
            this.lblServiceID.Name = "lblServiceID";
            this.lblServiceID.Size = new System.Drawing.Size(65, 12);
            this.lblServiceID.TabIndex = 21;
            this.lblServiceID.Text = "节目服务ID";
            // 
            // textTSID
            // 
            this.textTSID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textTSID.Location = new System.Drawing.Point(135, 50);
            this.textTSID.Name = "textTSID";
            this.textTSID.Size = new System.Drawing.Size(238, 21);
            this.textTSID.TabIndex = 20;
            this.textTSID.Tag = "传输流ID";
            // 
            // lblTSID
            // 
            this.lblTSID.AutoSize = true;
            this.lblTSID.Location = new System.Drawing.Point(25, 54);
            this.lblTSID.Name = "lblTSID";
            this.lblTSID.Size = new System.Drawing.Size(53, 12);
            this.lblTSID.TabIndex = 19;
            this.lblTSID.Text = "传输流ID";
            // 
            // textNetID
            // 
            this.textNetID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textNetID.Location = new System.Drawing.Point(135, 17);
            this.textNetID.Name = "textNetID";
            this.textNetID.Size = new System.Drawing.Size(238, 21);
            this.textNetID.TabIndex = 18;
            this.textNetID.Tag = "原始网络标识";
            // 
            // lblNetID
            // 
            this.lblNetID.AutoSize = true;
            this.lblNetID.Location = new System.Drawing.Point(25, 21);
            this.lblNetID.Name = "lblNetID";
            this.lblNetID.Size = new System.Drawing.Size(89, 12);
            this.lblNetID.TabIndex = 17;
            this.lblNetID.Text = "原始网络标识ID";
            // 
            // DailyBroadcastInfoLayout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlBroadcastInfo);
            this.DoubleBuffered = true;
            this.Name = "DailyBroadcastInfoLayout";
            this.Size = new System.Drawing.Size(922, 338);
            this.pnlBroadcastInfo.ResumeLayout(false);
            this.pnlBroadcastInfo.PerformLayout();
            this.groupTerminalAddress.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlBroadcastInfo;
        private System.Windows.Forms.GroupBox groupTerminalAddress;
        private System.Windows.Forms.Label lblEndTime;
        private System.Windows.Forms.TextBox textVolume;
        private System.Windows.Forms.Label lblVolume;
        private System.Windows.Forms.TextBox textPriority;
        private System.Windows.Forms.Label lblPriority;
        private System.Windows.Forms.TextBox textProgram_PID;
        private System.Windows.Forms.Label lblProgram_PID;
        private System.Windows.Forms.TextBox textPCR_PID;
        private System.Windows.Forms.Label lblPCR_PID;
        private System.Windows.Forms.TextBox textServiceID;
        private System.Windows.Forms.Label lblServiceID;
        private System.Windows.Forms.TextBox textTSID;
        private System.Windows.Forms.Label lblTSID;
        private System.Windows.Forms.TextBox textNetID;
        private System.Windows.Forms.Label lblNetID;
        private TerminalAddressLayout pnlTerminalAddress;
        private System.Windows.Forms.DateTimePicker timeEndTime;
        private AddressTypeLayout pnlAddressType;
    }
}
