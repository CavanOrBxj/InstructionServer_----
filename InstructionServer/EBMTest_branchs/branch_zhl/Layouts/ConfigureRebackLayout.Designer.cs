namespace EBMTest.Layouts
{
    partial class ConfigureRebackLayout
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
            this.textS_reback_address = new System.Windows.Forms.TextBox();
            this.lblS_reback_address = new System.Windows.Forms.Label();
            this.lblB_reback_type = new System.Windows.Forms.Label();
            this.pnlTerminalAddress = new EBMTest.Layouts.TerminalAddressLayout();
            this.cbBoxB_reback_type = new System.Windows.Forms.ComboBox();
            this.pnlAddressType = new EBMTest.Layouts.AddressTypeLayout();
            this.SuspendLayout();
            // 
            // textS_reback_address
            // 
            this.textS_reback_address.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textS_reback_address.Location = new System.Drawing.Point(102, 41);
            this.textS_reback_address.Name = "textS_reback_address";
            this.textS_reback_address.Size = new System.Drawing.Size(251, 21);
            this.textS_reback_address.TabIndex = 69;
            this.textS_reback_address.Tag = "回传地址";
            // 
            // lblS_reback_address
            // 
            this.lblS_reback_address.AutoSize = true;
            this.lblS_reback_address.Location = new System.Drawing.Point(36, 45);
            this.lblS_reback_address.Name = "lblS_reback_address";
            this.lblS_reback_address.Size = new System.Drawing.Size(53, 12);
            this.lblS_reback_address.TabIndex = 74;
            this.lblS_reback_address.Text = "回传地址";
            // 
            // lblB_reback_type
            // 
            this.lblB_reback_type.AutoSize = true;
            this.lblB_reback_type.Location = new System.Drawing.Point(36, 16);
            this.lblB_reback_type.Name = "lblB_reback_type";
            this.lblB_reback_type.Size = new System.Drawing.Size(53, 12);
            this.lblB_reback_type.TabIndex = 67;
            this.lblB_reback_type.Text = "回传方式";
            // 
            // pnlTerminalAddress
            // 
            this.pnlTerminalAddress.Location = new System.Drawing.Point(13, 73);
            this.pnlTerminalAddress.Name = "pnlTerminalAddress";
            this.pnlTerminalAddress.Size = new System.Drawing.Size(595, 252);
            this.pnlTerminalAddress.TabIndex = 73;
            // 
            // cbBoxB_reback_type
            // 
            this.cbBoxB_reback_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBoxB_reback_type.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbBoxB_reback_type.FormattingEnabled = true;
            this.cbBoxB_reback_type.Location = new System.Drawing.Point(102, 13);
            this.cbBoxB_reback_type.Name = "cbBoxB_reback_type";
            this.cbBoxB_reback_type.Size = new System.Drawing.Size(251, 20);
            this.cbBoxB_reback_type.TabIndex = 75;
            // 
            // pnlAddressType
            // 
            this.pnlAddressType.Location = new System.Drawing.Point(359, 9);
            this.pnlAddressType.Name = "pnlAddressType";
            this.pnlAddressType.Size = new System.Drawing.Size(242, 29);
            this.pnlAddressType.TabIndex = 76;
            // 
            // ConfigureRebackLayout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlAddressType);
            this.Controls.Add(this.cbBoxB_reback_type);
            this.Controls.Add(this.textS_reback_address);
            this.Controls.Add(this.lblS_reback_address);
            this.Controls.Add(this.lblB_reback_type);
            this.Controls.Add(this.pnlTerminalAddress);
            this.DoubleBuffered = true;
            this.Name = "ConfigureRebackLayout";
            this.Size = new System.Drawing.Size(621, 325);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textS_reback_address;
        private System.Windows.Forms.Label lblS_reback_address;
        private System.Windows.Forms.Label lblB_reback_type;
        private TerminalAddressLayout pnlTerminalAddress;
        private System.Windows.Forms.ComboBox cbBoxB_reback_type;
        private AddressTypeLayout pnlAddressType;
    }
}
