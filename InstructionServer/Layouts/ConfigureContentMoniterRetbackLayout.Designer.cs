namespace InstructionServer.Layouts
{
    partial class ConfigureContentMoniterRetbackLayout
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
            this.textFileId = new System.Windows.Forms.TextBox();
            this.lblFileId = new System.Windows.Forms.Label();
            this.textStart_package_index = new System.Windows.Forms.TextBox();
            this.lblAudioRetback_mode = new System.Windows.Forms.Label();
            this.lblStart_package_index = new System.Windows.Forms.Label();
            this.cbBoxAudioRetback_mode = new System.Windows.Forms.ComboBox();
            this.pnlTerminalAddress = new InstructionServer.Layouts.TerminalAddressLayout();
            this.pnlAddressType = new InstructionServer.Layouts.AddressTypeLayout();
            this.textreback_serverip = new System.Windows.Forms.TextBox();
            this.lblrebackserverip = new System.Windows.Forms.Label();
            this.textreback_port = new System.Windows.Forms.TextBox();
            this.lblrebackport = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textFileId
            // 
            this.textFileId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textFileId.Location = new System.Drawing.Point(106, 44);
            this.textFileId.MaxLength = 33;
            this.textFileId.Name = "textFileId";
            this.textFileId.Size = new System.Drawing.Size(223, 21);
            this.textFileId.TabIndex = 69;
            this.textFileId.Tag = "回传文件名";
            // 
            // lblFileId
            // 
            this.lblFileId.AutoSize = true;
            this.lblFileId.Location = new System.Drawing.Point(12, 49);
            this.lblFileId.Name = "lblFileId";
            this.lblFileId.Size = new System.Drawing.Size(65, 12);
            this.lblFileId.TabIndex = 73;
            this.lblFileId.Text = "回传文件名";
            // 
            // textStart_package_index
            // 
            this.textStart_package_index.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textStart_package_index.Location = new System.Drawing.Point(444, 44);
            this.textStart_package_index.Name = "textStart_package_index";
            this.textStart_package_index.Size = new System.Drawing.Size(159, 21);
            this.textStart_package_index.TabIndex = 70;
            this.textStart_package_index.Tag = "起始传输包的序号";
            // 
            // lblAudioRetback_mode
            // 
            this.lblAudioRetback_mode.AutoSize = true;
            this.lblAudioRetback_mode.Location = new System.Drawing.Point(12, 18);
            this.lblAudioRetback_mode.Name = "lblAudioRetback_mode";
            this.lblAudioRetback_mode.Size = new System.Drawing.Size(77, 12);
            this.lblAudioRetback_mode.TabIndex = 72;
            this.lblAudioRetback_mode.Text = "音频回传方式";
            // 
            // lblStart_package_index
            // 
            this.lblStart_package_index.AutoSize = true;
            this.lblStart_package_index.Location = new System.Drawing.Point(346, 47);
            this.lblStart_package_index.Name = "lblStart_package_index";
            this.lblStart_package_index.Size = new System.Drawing.Size(89, 12);
            this.lblStart_package_index.TabIndex = 71;
            this.lblStart_package_index.Text = "起始传输包序号";
            // 
            // cbBoxAudioRetback_mode
            // 
            this.cbBoxAudioRetback_mode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBoxAudioRetback_mode.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbBoxAudioRetback_mode.FormattingEnabled = true;
            this.cbBoxAudioRetback_mode.Location = new System.Drawing.Point(106, 13);
            this.cbBoxAudioRetback_mode.Name = "cbBoxAudioRetback_mode";
            this.cbBoxAudioRetback_mode.Size = new System.Drawing.Size(223, 20);
            this.cbBoxAudioRetback_mode.TabIndex = 75;
            // 
            // pnlTerminalAddress
            // 
            this.pnlTerminalAddress.Location = new System.Drawing.Point(8, 115);
            this.pnlTerminalAddress.Name = "pnlTerminalAddress";
            this.pnlTerminalAddress.Size = new System.Drawing.Size(595, 239);
            this.pnlTerminalAddress.TabIndex = 63;
            // 
            // pnlAddressType
            // 
            this.pnlAddressType.Location = new System.Drawing.Point(347, 9);
            this.pnlAddressType.Name = "pnlAddressType";
            this.pnlAddressType.Size = new System.Drawing.Size(255, 29);
            this.pnlAddressType.TabIndex = 76;
            // 
            // textreback_serverip
            // 
            this.textreback_serverip.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textreback_serverip.Location = new System.Drawing.Point(106, 76);
            this.textreback_serverip.Name = "textreback_serverip";
            this.textreback_serverip.Size = new System.Drawing.Size(223, 21);
            this.textreback_serverip.TabIndex = 77;
            this.textreback_serverip.Tag = "回传文件名";
            // 
            // lblrebackserverip
            // 
            this.lblrebackserverip.AutoSize = true;
            this.lblrebackserverip.Location = new System.Drawing.Point(12, 81);
            this.lblrebackserverip.Name = "lblrebackserverip";
            this.lblrebackserverip.Size = new System.Drawing.Size(53, 12);
            this.lblrebackserverip.TabIndex = 78;
            this.lblrebackserverip.Text = "服务器IP";
            // 
            // textreback_port
            // 
            this.textreback_port.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textreback_port.Location = new System.Drawing.Point(444, 76);
            this.textreback_port.Name = "textreback_port";
            this.textreback_port.Size = new System.Drawing.Size(159, 21);
            this.textreback_port.TabIndex = 79;
            this.textreback_port.Tag = "起始传输包的序号";
            // 
            // lblrebackport
            // 
            this.lblrebackport.AutoSize = true;
            this.lblrebackport.Location = new System.Drawing.Point(346, 79);
            this.lblrebackport.Name = "lblrebackport";
            this.lblrebackport.Size = new System.Drawing.Size(65, 12);
            this.lblrebackport.TabIndex = 80;
            this.lblrebackport.Text = "服务器端口";
            // 
            // ConfigureContentMoniterRetbackLayout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.textreback_port);
            this.Controls.Add(this.lblrebackport);
            this.Controls.Add(this.textreback_serverip);
            this.Controls.Add(this.lblrebackserverip);
            this.Controls.Add(this.pnlAddressType);
            this.Controls.Add(this.cbBoxAudioRetback_mode);
            this.Controls.Add(this.textFileId);
            this.Controls.Add(this.lblFileId);
            this.Controls.Add(this.textStart_package_index);
            this.Controls.Add(this.lblAudioRetback_mode);
            this.Controls.Add(this.lblStart_package_index);
            this.Controls.Add(this.pnlTerminalAddress);
            this.DoubleBuffered = true;
            this.Name = "ConfigureContentMoniterRetbackLayout";
            this.Size = new System.Drawing.Size(612, 353);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TerminalAddressLayout pnlTerminalAddress;
        private System.Windows.Forms.TextBox textFileId;
        private System.Windows.Forms.Label lblFileId;
        private System.Windows.Forms.TextBox textStart_package_index;
        private System.Windows.Forms.Label lblAudioRetback_mode;
        private System.Windows.Forms.Label lblStart_package_index;
        private System.Windows.Forms.ComboBox cbBoxAudioRetback_mode;
        private AddressTypeLayout pnlAddressType;
        private System.Windows.Forms.TextBox textreback_serverip;
        private System.Windows.Forms.Label lblrebackserverip;
        private System.Windows.Forms.TextBox textreback_port;
        private System.Windows.Forms.Label lblrebackport;
    }
}
