namespace InstructionServer.Layouts
{
    partial class ConfigureRdsConfigLayout
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
            this.pnlTerminalAddress = new InstructionServer.Layouts.TerminalAddressLayout();
            this.lblTip = new System.Windows.Forms.Label();
            this.textRdsData = new System.Windows.Forms.TextBox();
            this.lblRdsData = new System.Windows.Forms.Label();
            this.cbBoxB_Rds_terminal_type = new System.Windows.Forms.ComboBox();
            this.pnlAddressType = new InstructionServer.Layouts.AddressTypeLayout();
            this.lblB_Rds_terminal_type = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // pnlTerminalAddress
            // 
            this.pnlTerminalAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlTerminalAddress.Location = new System.Drawing.Point(17, 147);
            this.pnlTerminalAddress.Name = "pnlTerminalAddress";
            this.pnlTerminalAddress.Size = new System.Drawing.Size(612, 265);
            this.pnlTerminalAddress.TabIndex = 0;
            // 
            // lblTip
            // 
            this.lblTip.AutoSize = true;
            this.lblTip.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTip.ForeColor = System.Drawing.Color.DimGray;
            this.lblTip.Location = new System.Drawing.Point(130, 80);
            this.lblTip.Name = "lblTip";
            this.lblTip.Padding = new System.Windows.Forms.Padding(3);
            this.lblTip.Size = new System.Drawing.Size(367, 20);
            this.lblTip.TabIndex = 90;
            this.lblTip.Text = "提示：Rds数据按十六进制输入，多个数据用,或空格分隔(如AA FF)";
            // 
            // textRdsData
            // 
            this.textRdsData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textRdsData.Location = new System.Drawing.Point(130, 56);
            this.textRdsData.Name = "textRdsData";
            this.textRdsData.Size = new System.Drawing.Size(499, 21);
            this.textRdsData.TabIndex = 89;
            this.textRdsData.Tag = "Rds数据";
            // 
            // lblRdsData
            // 
            this.lblRdsData.AutoSize = true;
            this.lblRdsData.Location = new System.Drawing.Point(72, 58);
            this.lblRdsData.Name = "lblRdsData";
            this.lblRdsData.Size = new System.Drawing.Size(47, 12);
            this.lblRdsData.TabIndex = 88;
            this.lblRdsData.Text = "Rds数据";
            // 
            // cbBoxB_Rds_terminal_type
            // 
            this.cbBoxB_Rds_terminal_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBoxB_Rds_terminal_type.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbBoxB_Rds_terminal_type.FormattingEnabled = true;
            this.cbBoxB_Rds_terminal_type.Location = new System.Drawing.Point(130, 20);
            this.cbBoxB_Rds_terminal_type.Name = "cbBoxB_Rds_terminal_type";
            this.cbBoxB_Rds_terminal_type.Size = new System.Drawing.Size(214, 20);
            this.cbBoxB_Rds_terminal_type.TabIndex = 87;
            // 
            // pnlAddressType
            // 
            this.pnlAddressType.Location = new System.Drawing.Point(36, 113);
            this.pnlAddressType.Name = "pnlAddressType";
            this.pnlAddressType.Size = new System.Drawing.Size(308, 29);
            this.pnlAddressType.TabIndex = 86;
            // 
            // lblB_Rds_terminal_type
            // 
            this.lblB_Rds_terminal_type.AutoSize = true;
            this.lblB_Rds_terminal_type.Location = new System.Drawing.Point(24, 23);
            this.lblB_Rds_terminal_type.Name = "lblB_Rds_terminal_type";
            this.lblB_Rds_terminal_type.Size = new System.Drawing.Size(95, 12);
            this.lblB_Rds_terminal_type.TabIndex = 85;
            this.lblB_Rds_terminal_type.Text = "Rds覆盖终端类型";
            // 
            // ConfigureRdsConfigLayout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblTip);
            this.Controls.Add(this.textRdsData);
            this.Controls.Add(this.lblRdsData);
            this.Controls.Add(this.cbBoxB_Rds_terminal_type);
            this.Controls.Add(this.pnlAddressType);
            this.Controls.Add(this.lblB_Rds_terminal_type);
            this.Controls.Add(this.pnlTerminalAddress);
            this.Name = "ConfigureRdsConfigLayout";
            this.Size = new System.Drawing.Size(643, 415);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TerminalAddressLayout pnlTerminalAddress;
        private System.Windows.Forms.Label lblTip;
        private System.Windows.Forms.TextBox textRdsData;
        private System.Windows.Forms.Label lblRdsData;
        private System.Windows.Forms.ComboBox cbBoxB_Rds_terminal_type;
        private AddressTypeLayout pnlAddressType;
        private System.Windows.Forms.Label lblB_Rds_terminal_type;
    }
}
