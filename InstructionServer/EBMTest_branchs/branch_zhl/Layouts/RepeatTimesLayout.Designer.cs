namespace EBMTest.Layouts
{
    partial class RepeatTimesLayout
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
            this.textRepeat = new System.Windows.Forms.TextBox();
            this.cbBoxRepeat = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // textRepeat
            // 
            this.textRepeat.Location = new System.Drawing.Point(127, 4);
            this.textRepeat.Name = "textRepeat";
            this.textRepeat.Size = new System.Drawing.Size(97, 21);
            this.textRepeat.TabIndex = 17;
            this.textRepeat.Visible = false;
            // 
            // cbBoxRepeat
            // 
            this.cbBoxRepeat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBoxRepeat.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbBoxRepeat.FormattingEnabled = true;
            this.cbBoxRepeat.Items.AddRange(new object[] {
            "重复发送",
            "1",
            "2",
            "3",
            "自定义次数"});
            this.cbBoxRepeat.Location = new System.Drawing.Point(3, 4);
            this.cbBoxRepeat.Name = "cbBoxRepeat";
            this.cbBoxRepeat.Size = new System.Drawing.Size(108, 20);
            this.cbBoxRepeat.TabIndex = 16;
            this.cbBoxRepeat.SelectionChangeCommitted += new System.EventHandler(this.cbBoxRepeat_SelectionChangeCommitted);
            // 
            // RepeatTimesLayout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.textRepeat);
            this.Controls.Add(this.cbBoxRepeat);
            this.DoubleBuffered = true;
            this.Name = "RepeatTimesLayout";
            this.Size = new System.Drawing.Size(235, 30);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textRepeat;
        private System.Windows.Forms.ComboBox cbBoxRepeat;
    }
}
