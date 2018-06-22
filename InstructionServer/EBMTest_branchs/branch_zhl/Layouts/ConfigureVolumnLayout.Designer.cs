namespace EBMTest.Layouts
{
    partial class ConfigureVolumnLayout
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
            this.textVolumn = new System.Windows.Forms.TextBox();
            this.lblVolumn = new System.Windows.Forms.Label();
            this.pnlTerminalAddress = new EBMTest.Layouts.TerminalAddressLayout();
            this.pnlAddressType = new EBMTest.Layouts.AddressTypeLayout();
            this.SuspendLayout();
            // 
            // textVolumn
            // 
            this.textVolumn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textVolumn.Location = new System.Drawing.Point(92, 11);
            this.textVolumn.Name = "textVolumn";
            this.textVolumn.Size = new System.Drawing.Size(189, 21);
            this.textVolumn.TabIndex = 66;
            this.textVolumn.Tag = "音量";
            // 
            // lblVolumn
            // 
            this.lblVolumn.AutoSize = true;
            this.lblVolumn.Location = new System.Drawing.Point(15, 15);
            this.lblVolumn.Name = "lblVolumn";
            this.lblVolumn.Size = new System.Drawing.Size(71, 12);
            this.lblVolumn.TabIndex = 67;
            this.lblVolumn.Text = "音量(0-100)";
            // 
            // pnlTerminalAddress
            // 
            this.pnlTerminalAddress.Location = new System.Drawing.Point(11, 42);
            this.pnlTerminalAddress.Name = "pnlTerminalAddress";
            this.pnlTerminalAddress.Size = new System.Drawing.Size(595, 252);
            this.pnlTerminalAddress.TabIndex = 73;
            // 
            // pnlAddressType
            // 
            this.pnlAddressType.Location = new System.Drawing.Point(292, 6);
            this.pnlAddressType.Name = "pnlAddressType";
            this.pnlAddressType.Size = new System.Drawing.Size(294, 29);
            this.pnlAddressType.TabIndex = 75;
            // 
            // ConfigureVolumnLayout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlAddressType);
            this.Controls.Add(this.textVolumn);
            this.Controls.Add(this.lblVolumn);
            this.Controls.Add(this.pnlTerminalAddress);
            this.DoubleBuffered = true;
            this.Name = "ConfigureVolumnLayout";
            this.Size = new System.Drawing.Size(620, 291);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox textVolumn;
        private System.Windows.Forms.Label lblVolumn;
        private TerminalAddressLayout pnlTerminalAddress;
        private AddressTypeLayout pnlAddressType;
    }
}
