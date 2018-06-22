namespace EBMTest.Layouts
{
    partial class ConfigureFreqLayout
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
            this.pnlTerminalAddress = new EBMTest.Layouts.TerminalAddressLayout();
            this.textSymbolRate = new System.Windows.Forms.TextBox();
            this.lblSymbolRate = new System.Windows.Forms.Label();
            this.textFreq = new System.Windows.Forms.TextBox();
            this.lblFreq = new System.Windows.Forms.Label();
            this.lblQAM = new System.Windows.Forms.Label();
            this.pnlAddressType = new EBMTest.Layouts.AddressTypeLayout();
            this.cbBoxQAM = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // pnlTerminalAddress
            // 
            this.pnlTerminalAddress.Location = new System.Drawing.Point(9, 73);
            this.pnlTerminalAddress.Name = "pnlTerminalAddress";
            this.pnlTerminalAddress.Size = new System.Drawing.Size(595, 252);
            this.pnlTerminalAddress.TabIndex = 62;
            // 
            // textSymbolRate
            // 
            this.textSymbolRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textSymbolRate.Location = new System.Drawing.Point(402, 10);
            this.textSymbolRate.Name = "textSymbolRate";
            this.textSymbolRate.Size = new System.Drawing.Size(192, 21);
            this.textSymbolRate.TabIndex = 2;
            this.textSymbolRate.Tag = "符号率";
            // 
            // lblSymbolRate
            // 
            this.lblSymbolRate.AutoSize = true;
            this.lblSymbolRate.Location = new System.Drawing.Point(310, 14);
            this.lblSymbolRate.Name = "lblSymbolRate";
            this.lblSymbolRate.Size = new System.Drawing.Size(89, 12);
            this.lblSymbolRate.TabIndex = 59;
            this.lblSymbolRate.Text = "符号率（Kbps）";
            // 
            // textFreq
            // 
            this.textFreq.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textFreq.Location = new System.Drawing.Point(106, 11);
            this.textFreq.Name = "textFreq";
            this.textFreq.Size = new System.Drawing.Size(189, 21);
            this.textFreq.TabIndex = 1;
            this.textFreq.Tag = "主频率";
            // 
            // lblFreq
            // 
            this.lblFreq.AutoSize = true;
            this.lblFreq.Location = new System.Drawing.Point(21, 15);
            this.lblFreq.Name = "lblFreq";
            this.lblFreq.Size = new System.Drawing.Size(83, 12);
            this.lblFreq.TabIndex = 1;
            this.lblFreq.Text = "主频率（KHZ）";
            // 
            // lblQAM
            // 
            this.lblQAM.AutoSize = true;
            this.lblQAM.Location = new System.Drawing.Point(21, 46);
            this.lblQAM.Name = "lblQAM";
            this.lblQAM.Size = new System.Drawing.Size(35, 12);
            this.lblQAM.TabIndex = 65;
            this.lblQAM.Text = "QAM值";
            // 
            // pnlAddressType
            // 
            this.pnlAddressType.Location = new System.Drawing.Point(311, 36);
            this.pnlAddressType.Name = "pnlAddressType";
            this.pnlAddressType.Size = new System.Drawing.Size(282, 29);
            this.pnlAddressType.TabIndex = 66;
            // 
            // cbBoxQAM
            // 
            this.cbBoxQAM.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbBoxQAM.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBoxQAM.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbBoxQAM.FormattingEnabled = true;
            this.cbBoxQAM.Location = new System.Drawing.Point(106, 42);
            this.cbBoxQAM.Name = "cbBoxQAM";
            this.cbBoxQAM.Size = new System.Drawing.Size(189, 20);
            this.cbBoxQAM.TabIndex = 81;
            // 
            // ConfigureFreqLayout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbBoxQAM);
            this.Controls.Add(this.pnlAddressType);
            this.Controls.Add(this.lblQAM);
            this.Controls.Add(this.textFreq);
            this.Controls.Add(this.lblFreq);
            this.Controls.Add(this.pnlTerminalAddress);
            this.Controls.Add(this.textSymbolRate);
            this.Controls.Add(this.lblSymbolRate);
            this.DoubleBuffered = true;
            this.Name = "ConfigureFreqLayout";
            this.Size = new System.Drawing.Size(614, 324);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TerminalAddressLayout pnlTerminalAddress;
        private System.Windows.Forms.TextBox textSymbolRate;
        private System.Windows.Forms.Label lblSymbolRate;
        private System.Windows.Forms.TextBox textFreq;
        private System.Windows.Forms.Label lblFreq;
        private System.Windows.Forms.Label lblQAM;
        private AddressTypeLayout pnlAddressType;
        private System.Windows.Forms.ComboBox cbBoxQAM;
    }
}
