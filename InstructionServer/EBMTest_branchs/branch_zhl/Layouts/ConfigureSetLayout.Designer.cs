namespace EBMTest.Layouts
{
    partial class ConfigureSetLayout
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
            this.textTerminalAddress = new System.Windows.Forms.TextBox();
            this.textLogicAddress = new System.Windows.Forms.TextBox();
            this.lblTerminalAddress = new System.Windows.Forms.Label();
            this.lblLogicAddress = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textTerminalAddress
            // 
            this.textTerminalAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textTerminalAddress.Location = new System.Drawing.Point(77, 41);
            this.textTerminalAddress.Name = "textTerminalAddress";
            this.textTerminalAddress.Size = new System.Drawing.Size(210, 21);
            this.textTerminalAddress.TabIndex = 56;
            this.textTerminalAddress.Tag = "终端地址";
            // 
            // textLogicAddress
            // 
            this.textLogicAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textLogicAddress.Location = new System.Drawing.Point(77, 9);
            this.textLogicAddress.Name = "textLogicAddress";
            this.textLogicAddress.Size = new System.Drawing.Size(210, 21);
            this.textLogicAddress.TabIndex = 55;
            this.textLogicAddress.Tag = "逻辑地址";
            // 
            // lblTerminalAddress
            // 
            this.lblTerminalAddress.AutoSize = true;
            this.lblTerminalAddress.Location = new System.Drawing.Point(18, 45);
            this.lblTerminalAddress.Name = "lblTerminalAddress";
            this.lblTerminalAddress.Size = new System.Drawing.Size(53, 12);
            this.lblTerminalAddress.TabIndex = 54;
            this.lblTerminalAddress.Text = "终端地址";
            // 
            // lblLogicAddress
            // 
            this.lblLogicAddress.AutoSize = true;
            this.lblLogicAddress.Location = new System.Drawing.Point(18, 13);
            this.lblLogicAddress.Name = "lblLogicAddress";
            this.lblLogicAddress.Size = new System.Drawing.Size(53, 12);
            this.lblLogicAddress.TabIndex = 53;
            this.lblLogicAddress.Text = "逻辑地址";
            // 
            // ConfigureSetLayout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.textTerminalAddress);
            this.Controls.Add(this.textLogicAddress);
            this.Controls.Add(this.lblTerminalAddress);
            this.Controls.Add(this.lblLogicAddress);
            this.DoubleBuffered = true;
            this.Name = "ConfigureSetLayout";
            this.Size = new System.Drawing.Size(312, 72);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textTerminalAddress;
        private System.Windows.Forms.TextBox textLogicAddress;
        private System.Windows.Forms.Label lblTerminalAddress;
        private System.Windows.Forms.Label lblLogicAddress;
    }
}
