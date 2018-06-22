namespace EBMTest.Layouts
{
    partial class ConfigureStatusRetbackLayout
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
            this.pnlTerminalAddress = new EBMTest.Layouts.TerminalAddressLayout();
            this.pnlAddressType = new EBMTest.Layouts.AddressTypeLayout();
            this.pnlParameterTag = new EBMTest.Layouts.ParameterTagLayout();
            this.SuspendLayout();
            // 
            // pnlTerminalAddress
            // 
            this.pnlTerminalAddress.Location = new System.Drawing.Point(381, 6);
            this.pnlTerminalAddress.Name = "pnlTerminalAddress";
            this.pnlTerminalAddress.Size = new System.Drawing.Size(501, 339);
            this.pnlTerminalAddress.TabIndex = 81;
            // 
            // pnlAddressType
            // 
            this.pnlAddressType.Location = new System.Drawing.Point(23, 10);
            this.pnlAddressType.Name = "pnlAddressType";
            this.pnlAddressType.Size = new System.Drawing.Size(318, 29);
            this.pnlAddressType.TabIndex = 80;
            // 
            // pnlParameterTag
            // 
            this.pnlParameterTag.Location = new System.Drawing.Point(6, 47);
            this.pnlParameterTag.Name = "pnlParameterTag";
            this.pnlParameterTag.Size = new System.Drawing.Size(369, 282);
            this.pnlParameterTag.TabIndex = 82;
            // 
            // ConfigureStatusRetbackLayout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlParameterTag);
            this.Controls.Add(this.pnlTerminalAddress);
            this.Controls.Add(this.pnlAddressType);
            this.DoubleBuffered = true;
            this.Name = "ConfigureStatusRetbackLayout";
            this.Size = new System.Drawing.Size(889, 340);
            this.ResumeLayout(false);

        }

        #endregion

        private AddressTypeLayout pnlAddressType;
        private TerminalAddressLayout pnlTerminalAddress;
        private ParameterTagLayout pnlParameterTag;
    }
}
