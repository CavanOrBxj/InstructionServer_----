namespace EBMTest.Layouts
{
    partial class ConfigureContentRealMoniterLayout
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.lab_Moniter_time_duration = new System.Windows.Forms.Label();
            this.txt_Moniter_time_duration = new System.Windows.Forms.TextBox();
            this.txt_S_Server_addr = new System.Windows.Forms.TextBox();
            this.lab_S_Server_addr = new System.Windows.Forms.Label();
            this.cbBoxEBMId = new System.Windows.Forms.ComboBox();
            this.lblEBMId = new System.Windows.Forms.Label();
            this.cbBoxRetback_mode = new System.Windows.Forms.ComboBox();
            this.lblRetback_mode = new System.Windows.Forms.Label();
            this.pnlAddressType = new EBMTest.Layouts.AddressTypeLayout();
            this.pnlTerminalAddress = new EBMTest.Layouts.TerminalAddressLayout();
            this.SuspendLayout();
            // 
            // lab_Moniter_time_duration
            // 
            this.lab_Moniter_time_duration.AutoSize = true;
            this.lab_Moniter_time_duration.Location = new System.Drawing.Point(345, 79);
            this.lab_Moniter_time_duration.Name = "lab_Moniter_time_duration";
            this.lab_Moniter_time_duration.Size = new System.Drawing.Size(77, 12);
            this.lab_Moniter_time_duration.TabIndex = 3;
            this.lab_Moniter_time_duration.Text = "监听时长(秒)";
            // 
            // txt_Moniter_time_duration
            // 
            this.txt_Moniter_time_duration.Location = new System.Drawing.Point(428, 75);
            this.txt_Moniter_time_duration.Name = "txt_Moniter_time_duration";
            this.txt_Moniter_time_duration.Size = new System.Drawing.Size(178, 21);
            this.txt_Moniter_time_duration.TabIndex = 9;
            this.txt_Moniter_time_duration.Tag = "监听时长";
            // 
            // txt_S_Server_addr
            // 
            this.txt_S_Server_addr.Location = new System.Drawing.Point(109, 74);
            this.txt_S_Server_addr.Name = "txt_S_Server_addr";
            this.txt_S_Server_addr.Size = new System.Drawing.Size(223, 21);
            this.txt_S_Server_addr.TabIndex = 13;
            this.txt_S_Server_addr.Tag = "服务器地址端口";
            // 
            // lab_S_Server_addr
            // 
            this.lab_S_Server_addr.AutoSize = true;
            this.lab_S_Server_addr.Location = new System.Drawing.Point(13, 78);
            this.lab_S_Server_addr.Name = "lab_S_Server_addr";
            this.lab_S_Server_addr.Size = new System.Drawing.Size(89, 12);
            this.lab_S_Server_addr.TabIndex = 12;
            this.lab_S_Server_addr.Text = "服务器地址端口";
            // 
            // cbBoxEBMId
            // 
            this.cbBoxEBMId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBoxEBMId.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbBoxEBMId.FormattingEnabled = true;
            this.cbBoxEBMId.Location = new System.Drawing.Point(109, 8);
            this.cbBoxEBMId.Name = "cbBoxEBMId";
            this.cbBoxEBMId.Size = new System.Drawing.Size(310, 20);
            this.cbBoxEBMId.TabIndex = 76;
            // 
            // lblEBMId
            // 
            this.lblEBMId.AutoSize = true;
            this.lblEBMId.Location = new System.Drawing.Point(13, 12);
            this.lblEBMId.Name = "lblEBMId";
            this.lblEBMId.Size = new System.Drawing.Size(89, 12);
            this.lblEBMId.TabIndex = 75;
            this.lblEBMId.Text = "应急广播事件ID";
            // 
            // cbBoxRetback_mode
            // 
            this.cbBoxRetback_mode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBoxRetback_mode.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbBoxRetback_mode.FormattingEnabled = true;
            this.cbBoxRetback_mode.Location = new System.Drawing.Point(109, 41);
            this.cbBoxRetback_mode.Name = "cbBoxRetback_mode";
            this.cbBoxRetback_mode.Size = new System.Drawing.Size(223, 20);
            this.cbBoxRetback_mode.TabIndex = 78;
            // 
            // lblRetback_mode
            // 
            this.lblRetback_mode.AutoSize = true;
            this.lblRetback_mode.Location = new System.Drawing.Point(13, 46);
            this.lblRetback_mode.Name = "lblRetback_mode";
            this.lblRetback_mode.Size = new System.Drawing.Size(53, 12);
            this.lblRetback_mode.TabIndex = 77;
            this.lblRetback_mode.Text = "回传方式";
            // 
            // pnlAddressType
            // 
            this.pnlAddressType.Location = new System.Drawing.Point(338, 40);
            this.pnlAddressType.Name = "pnlAddressType";
            this.pnlAddressType.Size = new System.Drawing.Size(268, 29);
            this.pnlAddressType.TabIndex = 79;
            // 
            // pnlTerminalAddress
            // 
            this.pnlTerminalAddress.Location = new System.Drawing.Point(11, 108);
            this.pnlTerminalAddress.Name = "pnlTerminalAddress";
            this.pnlTerminalAddress.Size = new System.Drawing.Size(606, 252);
            this.pnlTerminalAddress.TabIndex = 80;
            // 
            // ConfigureContentRealMoniterLayout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlTerminalAddress);
            this.Controls.Add(this.pnlAddressType);
            this.Controls.Add(this.cbBoxRetback_mode);
            this.Controls.Add(this.lblRetback_mode);
            this.Controls.Add(this.cbBoxEBMId);
            this.Controls.Add(this.lblEBMId);
            this.Controls.Add(this.txt_S_Server_addr);
            this.Controls.Add(this.lab_S_Server_addr);
            this.Controls.Add(this.txt_Moniter_time_duration);
            this.Controls.Add(this.lab_Moniter_time_duration);
            this.DoubleBuffered = true;
            this.Name = "ConfigureContentRealMoniterLayout";
            this.Size = new System.Drawing.Size(627, 356);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lab_Moniter_time_duration;
        private System.Windows.Forms.TextBox txt_Moniter_time_duration;
        private System.Windows.Forms.TextBox txt_S_Server_addr;
        private System.Windows.Forms.Label lab_S_Server_addr;
        private System.Windows.Forms.ComboBox cbBoxEBMId;
        private System.Windows.Forms.Label lblEBMId;
        private System.Windows.Forms.ComboBox cbBoxRetback_mode;
        private System.Windows.Forms.Label lblRetback_mode;
        private AddressTypeLayout pnlAddressType;
        private TerminalAddressLayout pnlTerminalAddress;
    }
}
