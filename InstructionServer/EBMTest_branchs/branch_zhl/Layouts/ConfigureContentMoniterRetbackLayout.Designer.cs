namespace EBMTest.Layouts
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
            this.lblEBMId = new System.Windows.Forms.Label();
            this.textStart_package_index = new System.Windows.Forms.TextBox();
            this.lblRetback_mode = new System.Windows.Forms.Label();
            this.lblStart_package_index = new System.Windows.Forms.Label();
            this.cbBoxEBMId = new System.Windows.Forms.ComboBox();
            this.cbBoxRetback_mode = new System.Windows.Forms.ComboBox();
            this.pnlTerminalAddress = new EBMTest.Layouts.TerminalAddressLayout();
            this.pnlAddressType = new EBMTest.Layouts.AddressTypeLayout();
            this.SuspendLayout();
            // 
            // textFileId
            // 
            this.textFileId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textFileId.Location = new System.Drawing.Point(106, 70);
            this.textFileId.Name = "textFileId";
            this.textFileId.Size = new System.Drawing.Size(223, 21);
            this.textFileId.TabIndex = 69;
            this.textFileId.Tag = "回传文件名";
            // 
            // lblFileId
            // 
            this.lblFileId.AutoSize = true;
            this.lblFileId.Location = new System.Drawing.Point(12, 75);
            this.lblFileId.Name = "lblFileId";
            this.lblFileId.Size = new System.Drawing.Size(65, 12);
            this.lblFileId.TabIndex = 73;
            this.lblFileId.Text = "回传文件名";
            // 
            // lblEBMId
            // 
            this.lblEBMId.AutoSize = true;
            this.lblEBMId.Location = new System.Drawing.Point(12, 14);
            this.lblEBMId.Name = "lblEBMId";
            this.lblEBMId.Size = new System.Drawing.Size(89, 12);
            this.lblEBMId.TabIndex = 67;
            this.lblEBMId.Text = "应急广播事件ID";
            // 
            // textStart_package_index
            // 
            this.textStart_package_index.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textStart_package_index.Location = new System.Drawing.Point(444, 70);
            this.textStart_package_index.Name = "textStart_package_index";
            this.textStart_package_index.Size = new System.Drawing.Size(159, 21);
            this.textStart_package_index.TabIndex = 70;
            this.textStart_package_index.Tag = "起始传输包的序号";
            // 
            // lblRetback_mode
            // 
            this.lblRetback_mode.AutoSize = true;
            this.lblRetback_mode.Location = new System.Drawing.Point(12, 44);
            this.lblRetback_mode.Name = "lblRetback_mode";
            this.lblRetback_mode.Size = new System.Drawing.Size(53, 12);
            this.lblRetback_mode.TabIndex = 72;
            this.lblRetback_mode.Text = "回传方式";
            // 
            // lblStart_package_index
            // 
            this.lblStart_package_index.AutoSize = true;
            this.lblStart_package_index.Location = new System.Drawing.Point(346, 73);
            this.lblStart_package_index.Name = "lblStart_package_index";
            this.lblStart_package_index.Size = new System.Drawing.Size(89, 12);
            this.lblStart_package_index.TabIndex = 71;
            this.lblStart_package_index.Text = "起始传输包序号";
            // 
            // cbBoxEBMId
            // 
            this.cbBoxEBMId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBoxEBMId.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbBoxEBMId.FormattingEnabled = true;
            this.cbBoxEBMId.Location = new System.Drawing.Point(106, 11);
            this.cbBoxEBMId.Name = "cbBoxEBMId";
            this.cbBoxEBMId.Size = new System.Drawing.Size(310, 20);
            this.cbBoxEBMId.TabIndex = 74;
            // 
            // cbBoxRetback_mode
            // 
            this.cbBoxRetback_mode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBoxRetback_mode.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbBoxRetback_mode.FormattingEnabled = true;
            this.cbBoxRetback_mode.Location = new System.Drawing.Point(106, 39);
            this.cbBoxRetback_mode.Name = "cbBoxRetback_mode";
            this.cbBoxRetback_mode.Size = new System.Drawing.Size(223, 20);
            this.cbBoxRetback_mode.TabIndex = 75;
            // 
            // pnlTerminalAddress
            // 
            this.pnlTerminalAddress.Location = new System.Drawing.Point(8, 102);
            this.pnlTerminalAddress.Name = "pnlTerminalAddress";
            this.pnlTerminalAddress.Size = new System.Drawing.Size(595, 252);
            this.pnlTerminalAddress.TabIndex = 63;
            // 
            // pnlAddressType
            // 
            this.pnlAddressType.Location = new System.Drawing.Point(351, 35);
            this.pnlAddressType.Name = "pnlAddressType";
            this.pnlAddressType.Size = new System.Drawing.Size(255, 29);
            this.pnlAddressType.TabIndex = 76;
            // 
            // ConfigureContentMoniterRetbackLayout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlAddressType);
            this.Controls.Add(this.cbBoxRetback_mode);
            this.Controls.Add(this.cbBoxEBMId);
            this.Controls.Add(this.textFileId);
            this.Controls.Add(this.lblFileId);
            this.Controls.Add(this.lblEBMId);
            this.Controls.Add(this.textStart_package_index);
            this.Controls.Add(this.lblRetback_mode);
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
        private System.Windows.Forms.Label lblEBMId;
        private System.Windows.Forms.TextBox textStart_package_index;
        private System.Windows.Forms.Label lblRetback_mode;
        private System.Windows.Forms.Label lblStart_package_index;
        private System.Windows.Forms.ComboBox cbBoxEBMId;
        private System.Windows.Forms.ComboBox cbBoxRetback_mode;
        private AddressTypeLayout pnlAddressType;
    }
}
