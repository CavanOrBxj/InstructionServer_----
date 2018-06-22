namespace EBMTest.Layouts
{
    partial class ConfigurePeriodLayout
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
            this.textreback_period = new System.Windows.Forms.TextBox();
            this.lblreback_period = new System.Windows.Forms.Label();
            this.pnlTerminalAddress = new EBMTest.Layouts.TerminalAddressLayout();
            this.pnlAddressType = new EBMTest.Layouts.AddressTypeLayout();
            this.SuspendLayout();
            // 
            // textreback_period
            // 
            this.textreback_period.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textreback_period.Location = new System.Drawing.Point(95, 13);
            this.textreback_period.Name = "textreback_period";
            this.textreback_period.Size = new System.Drawing.Size(189, 21);
            this.textreback_period.TabIndex = 66;
            this.textreback_period.Tag = "回传周期";
            // 
            // lblreback_period
            // 
            this.lblreback_period.AutoSize = true;
            this.lblreback_period.Location = new System.Drawing.Point(11, 18);
            this.lblreback_period.Name = "lblreback_period";
            this.lblreback_period.Size = new System.Drawing.Size(77, 12);
            this.lblreback_period.TabIndex = 67;
            this.lblreback_period.Text = "回传周期(秒)";
            // 
            // pnlTerminalAddress
            // 
            this.pnlTerminalAddress.Location = new System.Drawing.Point(7, 48);
            this.pnlTerminalAddress.Name = "pnlTerminalAddress";
            this.pnlTerminalAddress.Size = new System.Drawing.Size(595, 252);
            this.pnlTerminalAddress.TabIndex = 73;
            // 
            // pnlAddressType
            // 
            this.pnlAddressType.Location = new System.Drawing.Point(303, 9);
            this.pnlAddressType.Name = "pnlAddressType";
            this.pnlAddressType.Size = new System.Drawing.Size(287, 29);
            this.pnlAddressType.TabIndex = 74;
            // 
            // ConfigurePeriodLayout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlAddressType);
            this.Controls.Add(this.textreback_period);
            this.Controls.Add(this.lblreback_period);
            this.Controls.Add(this.pnlTerminalAddress);
            this.DoubleBuffered = true;
            this.Name = "ConfigurePeriodLayout";
            this.Size = new System.Drawing.Size(612, 300);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox textreback_period;
        private System.Windows.Forms.Label lblreback_period;
        private TerminalAddressLayout pnlTerminalAddress;
        private AddressTypeLayout pnlAddressType;
    }
}
