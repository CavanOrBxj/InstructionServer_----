namespace EBMTest.Layouts
{
    partial class EBMIndexDes
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
            this.textarB_discriptor = new System.Windows.Forms.TextBox();
            this.lblarB_discriptor = new System.Windows.Forms.Label();
            this.lblB_discriptor_tag = new System.Windows.Forms.Label();
            this.cbBoxB_discriptor_tag = new System.Windows.Forms.ComboBox();
            this.checkBoxUseOwnData = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // textarB_discriptor
            // 
            this.textarB_discriptor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textarB_discriptor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textarB_discriptor.Location = new System.Drawing.Point(115, 71);
            this.textarB_discriptor.Name = "textarB_discriptor";
            this.textarB_discriptor.Size = new System.Drawing.Size(276, 21);
            this.textarB_discriptor.TabIndex = 48;
            this.textarB_discriptor.Tag = "描述符数据";
            this.textarB_discriptor.Visible = false;
            // 
            // lblarB_discriptor
            // 
            this.lblarB_discriptor.Location = new System.Drawing.Point(11, 66);
            this.lblarB_discriptor.Name = "lblarB_discriptor";
            this.lblarB_discriptor.Size = new System.Drawing.Size(96, 31);
            this.lblarB_discriptor.TabIndex = 46;
            this.lblarB_discriptor.Text = "描述符数据(多个数据用,分隔)";
            this.lblarB_discriptor.Visible = false;
            // 
            // lblB_discriptor_tag
            // 
            this.lblB_discriptor_tag.AutoSize = true;
            this.lblB_discriptor_tag.Location = new System.Drawing.Point(11, 39);
            this.lblB_discriptor_tag.Name = "lblB_discriptor_tag";
            this.lblB_discriptor_tag.Size = new System.Drawing.Size(65, 12);
            this.lblB_discriptor_tag.TabIndex = 45;
            this.lblB_discriptor_tag.Text = "描述符标签";
            this.lblB_discriptor_tag.Visible = false;
            // 
            // cbBoxB_discriptor_tag
            // 
            this.cbBoxB_discriptor_tag.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbBoxB_discriptor_tag.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBoxB_discriptor_tag.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbBoxB_discriptor_tag.FormattingEnabled = true;
            this.cbBoxB_discriptor_tag.Location = new System.Drawing.Point(115, 36);
            this.cbBoxB_discriptor_tag.Name = "cbBoxB_discriptor_tag";
            this.cbBoxB_discriptor_tag.Size = new System.Drawing.Size(276, 20);
            this.cbBoxB_discriptor_tag.TabIndex = 81;
            this.cbBoxB_discriptor_tag.Visible = false;
            // 
            // checkBoxUseOwnData
            // 
            this.checkBoxUseOwnData.AutoSize = true;
            this.checkBoxUseOwnData.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxUseOwnData.Location = new System.Drawing.Point(9, 10);
            this.checkBoxUseOwnData.Name = "checkBoxUseOwnData";
            this.checkBoxUseOwnData.Size = new System.Drawing.Size(120, 16);
            this.checkBoxUseOwnData.TabIndex = 82;
            this.checkBoxUseOwnData.Text = "输入流信息描述符";
            this.checkBoxUseOwnData.UseVisualStyleBackColor = true;
            this.checkBoxUseOwnData.CheckedChanged += new System.EventHandler(this.checkBoxUseOwnData_CheckedChanged);
            // 
            // EBMIndexDes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.checkBoxUseOwnData);
            this.Controls.Add(this.cbBoxB_discriptor_tag);
            this.Controls.Add(this.textarB_discriptor);
            this.Controls.Add(this.lblarB_discriptor);
            this.Controls.Add(this.lblB_discriptor_tag);
            this.DoubleBuffered = true;
            this.Name = "EBMIndexDes";
            this.Size = new System.Drawing.Size(398, 102);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textarB_discriptor;
        private System.Windows.Forms.Label lblarB_discriptor;
        private System.Windows.Forms.Label lblB_discriptor_tag;
        private System.Windows.Forms.ComboBox cbBoxB_discriptor_tag;
        private System.Windows.Forms.CheckBox checkBoxUseOwnData;
    }
}
