namespace EBMTest.Layouts
{
    partial class ConfigureWorkLayout
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
            this.lblB_Terminal_wordmode = new System.Windows.Forms.Label();
            this.pnlTerminalAddress = new EBMTest.Layouts.TerminalAddressLayout();
            this.pnlAddressType = new EBMTest.Layouts.AddressTypeLayout();
            this.cbBoxB_Terminal_wordmode = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // lblB_Terminal_wordmode
            // 
            this.lblB_Terminal_wordmode.AutoSize = true;
            this.lblB_Terminal_wordmode.Location = new System.Drawing.Point(10, 17);
            this.lblB_Terminal_wordmode.Name = "lblB_Terminal_wordmode";
            this.lblB_Terminal_wordmode.Size = new System.Drawing.Size(77, 12);
            this.lblB_Terminal_wordmode.TabIndex = 54;
            this.lblB_Terminal_wordmode.Text = "终端工作状态";
            // 
            // pnlTerminalAddress
            // 
            this.pnlTerminalAddress.Location = new System.Drawing.Point(5, 39);
            this.pnlTerminalAddress.Name = "pnlTerminalAddress";
            this.pnlTerminalAddress.Size = new System.Drawing.Size(595, 252);
            this.pnlTerminalAddress.TabIndex = 57;
            // 
            // pnlAddressType
            // 
            this.pnlAddressType.Location = new System.Drawing.Point(297, 6);
            this.pnlAddressType.Name = "pnlAddressType";
            this.pnlAddressType.Size = new System.Drawing.Size(287, 29);
            this.pnlAddressType.TabIndex = 76;
            // 
            // cbBoxB_Terminal_wordmode
            // 
            this.cbBoxB_Terminal_wordmode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBoxB_Terminal_wordmode.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbBoxB_Terminal_wordmode.FormattingEnabled = true;
            this.cbBoxB_Terminal_wordmode.Location = new System.Drawing.Point(93, 12);
            this.cbBoxB_Terminal_wordmode.Name = "cbBoxB_Terminal_wordmode";
            this.cbBoxB_Terminal_wordmode.Size = new System.Drawing.Size(198, 20);
            this.cbBoxB_Terminal_wordmode.TabIndex = 81;
            // 
            // ConfigureWorkLayout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbBoxB_Terminal_wordmode);
            this.Controls.Add(this.pnlAddressType);
            this.Controls.Add(this.pnlTerminalAddress);
            this.Controls.Add(this.lblB_Terminal_wordmode);
            this.DoubleBuffered = true;
            this.Name = "ConfigureWorkLayout";
            this.Size = new System.Drawing.Size(608, 291);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblB_Terminal_wordmode;
        private TerminalAddressLayout pnlTerminalAddress;
        private AddressTypeLayout pnlAddressType;
        private System.Windows.Forms.ComboBox cbBoxB_Terminal_wordmode;
    }
}
