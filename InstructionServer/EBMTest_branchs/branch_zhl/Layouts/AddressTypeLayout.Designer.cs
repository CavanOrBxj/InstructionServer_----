namespace EBMTest.Layouts
{
    partial class AddressTypeLayout
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
            this.cbBoxB_Address_type = new System.Windows.Forms.ComboBox();
            this.lblB_Address_type = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cbBoxB_Address_type
            // 
            this.cbBoxB_Address_type.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbBoxB_Address_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBoxB_Address_type.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbBoxB_Address_type.FormattingEnabled = true;
            this.cbBoxB_Address_type.Location = new System.Drawing.Point(91, 5);
            this.cbBoxB_Address_type.Name = "cbBoxB_Address_type";
            this.cbBoxB_Address_type.Size = new System.Drawing.Size(171, 20);
            this.cbBoxB_Address_type.TabIndex = 80;
            // 
            // lblB_Address_type
            // 
            this.lblB_Address_type.AutoSize = true;
            this.lblB_Address_type.Location = new System.Drawing.Point(6, 8);
            this.lblB_Address_type.Name = "lblB_Address_type";
            this.lblB_Address_type.Size = new System.Drawing.Size(77, 12);
            this.lblB_Address_type.TabIndex = 79;
            this.lblB_Address_type.Text = "终端地址类型";
            // 
            // AddressTypeLayout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbBoxB_Address_type);
            this.Controls.Add(this.lblB_Address_type);
            this.DoubleBuffered = true;
            this.Name = "AddressTypeLayout";
            this.Size = new System.Drawing.Size(262, 29);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbBoxB_Address_type;
        private System.Windows.Forms.Label lblB_Address_type;
    }
}
