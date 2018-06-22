namespace InstructionServer.Layouts
{
    partial class ConfigureStatusRetbackGXLayout
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
            this.textI_retback_period = new System.Windows.Forms.TextBox();
            this.lblI_retback_period = new System.Windows.Forms.Label();
            this.cbBoxB_Terminal_Retback_Type = new System.Windows.Forms.ComboBox();
            this.pnlAddressType = new InstructionServer.Layouts.AddressTypeLayout();
            this.lblB_Terminal_Retback_Type = new System.Windows.Forms.Label();
            this.pnlTerminalAddress = new InstructionServer.Layouts.TerminalAddressLayout();
            this.SuspendLayout();
            // 
            // textI_retback_period
            // 
            this.textI_retback_period.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textI_retback_period.Location = new System.Drawing.Point(433, 21);
            this.textI_retback_period.Name = "textI_retback_period";
            this.textI_retback_period.Size = new System.Drawing.Size(134, 21);
            this.textI_retback_period.TabIndex = 96;
            this.textI_retback_period.Tag = "回传周期(秒)";
            // 
            // lblI_retback_period
            // 
            this.lblI_retback_period.AutoSize = true;
            this.lblI_retback_period.Location = new System.Drawing.Point(350, 24);
            this.lblI_retback_period.Name = "lblI_retback_period";
            this.lblI_retback_period.Size = new System.Drawing.Size(77, 12);
            this.lblI_retback_period.TabIndex = 95;
            this.lblI_retback_period.Text = "回传周期(秒)";
            // 
            // cbBoxB_Terminal_Retback_Type
            // 
            this.cbBoxB_Terminal_Retback_Type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBoxB_Terminal_Retback_Type.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbBoxB_Terminal_Retback_Type.FormattingEnabled = true;
            this.cbBoxB_Terminal_Retback_Type.Location = new System.Drawing.Point(106, 21);
            this.cbBoxB_Terminal_Retback_Type.Name = "cbBoxB_Terminal_Retback_Type";
            this.cbBoxB_Terminal_Retback_Type.Size = new System.Drawing.Size(234, 20);
            this.cbBoxB_Terminal_Retback_Type.TabIndex = 94;
            // 
            // pnlAddressType
            // 
            this.pnlAddressType.Location = new System.Drawing.Point(12, 60);
            this.pnlAddressType.Name = "pnlAddressType";
            this.pnlAddressType.Size = new System.Drawing.Size(328, 29);
            this.pnlAddressType.TabIndex = 93;
            // 
            // lblB_Terminal_Retback_Type
            // 
            this.lblB_Terminal_Retback_Type.AutoSize = true;
            this.lblB_Terminal_Retback_Type.Location = new System.Drawing.Point(16, 24);
            this.lblB_Terminal_Retback_Type.Name = "lblB_Terminal_Retback_Type";
            this.lblB_Terminal_Retback_Type.Size = new System.Drawing.Size(77, 12);
            this.lblB_Terminal_Retback_Type.TabIndex = 92;
            this.lblB_Terminal_Retback_Type.Text = "状态回传模式";
            // 
            // pnlTerminalAddress
            // 
            this.pnlTerminalAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlTerminalAddress.Location = new System.Drawing.Point(18, 105);
            this.pnlTerminalAddress.Name = "pnlTerminalAddress";
            this.pnlTerminalAddress.Size = new System.Drawing.Size(549, 265);
            this.pnlTerminalAddress.TabIndex = 91;
            // 
            // ConfigureStatusRetbackGXLayout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.textI_retback_period);
            this.Controls.Add(this.lblI_retback_period);
            this.Controls.Add(this.cbBoxB_Terminal_Retback_Type);
            this.Controls.Add(this.pnlAddressType);
            this.Controls.Add(this.lblB_Terminal_Retback_Type);
            this.Controls.Add(this.pnlTerminalAddress);
            this.Name = "ConfigureStatusRetbackGXLayout";
            this.Size = new System.Drawing.Size(579, 370);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textI_retback_period;
        private System.Windows.Forms.Label lblI_retback_period;
        private System.Windows.Forms.ComboBox cbBoxB_Terminal_Retback_Type;
        private AddressTypeLayout pnlAddressType;
        private System.Windows.Forms.Label lblB_Terminal_Retback_Type;
        private TerminalAddressLayout pnlTerminalAddress;
    }
}
