namespace EBMTest
{
    partial class EBMCertAuthInfo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cbBoxType = new System.Windows.Forms.ComboBox();
            this.btnOpenFile = new ControlAstro.Controls.LabelButton();
            this.lblData = new System.Windows.Forms.Label();
            this.lblType = new System.Windows.Forms.Label();
            this.textData = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.richText = new System.Windows.Forms.RichTextBox();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // cbBoxType
            // 
            this.cbBoxType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBoxType.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbBoxType.FormattingEnabled = true;
            this.cbBoxType.Items.AddRange(new object[] {
            "文件",
            "文本"});
            this.cbBoxType.Location = new System.Drawing.Point(102, 27);
            this.cbBoxType.Name = "cbBoxType";
            this.cbBoxType.Size = new System.Drawing.Size(127, 20);
            this.cbBoxType.TabIndex = 26;
            this.cbBoxType.SelectedIndexChanged += new System.EventHandler(this.cbBoxType_SelectedIndexChanged);
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.btnOpenFile.Location = new System.Drawing.Point(484, 60);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(25, 22);
            this.btnOpenFile.TabIndex = 25;
            this.btnOpenFile.Text = "...";
            this.btnOpenFile.Visible = false;
            this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
            // 
            // lblData
            // 
            this.lblData.AutoSize = true;
            this.lblData.Location = new System.Drawing.Point(43, 63);
            this.lblData.Name = "lblData";
            this.lblData.Size = new System.Drawing.Size(53, 12);
            this.lblData.TabIndex = 24;
            this.lblData.Text = "数据内容";
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(19, 30);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(77, 12);
            this.lblType.TabIndex = 23;
            this.lblType.Text = "输入数据类型";
            // 
            // textData
            // 
            this.textData.Location = new System.Drawing.Point(102, 60);
            this.textData.Name = "textData";
            this.textData.ReadOnly = true;
            this.textData.Size = new System.Drawing.Size(376, 21);
            this.textData.TabIndex = 20;
            this.textData.Visible = false;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOK.Location = new System.Drawing.Point(321, 284);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(190, 34);
            this.btnOK.TabIndex = 56;
            this.btnOK.Text = "确        定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // richText
            // 
            this.richText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.richText.Location = new System.Drawing.Point(102, 60);
            this.richText.Name = "richText";
            this.richText.Size = new System.Drawing.Size(407, 214);
            this.richText.TabIndex = 57;
            this.richText.Text = "";
            this.richText.Visible = false;
            // 
            // EBMCertAuthInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 326);
            this.Controls.Add(this.richText);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.cbBoxType);
            this.Controls.Add(this.btnOpenFile);
            this.Controls.Add(this.lblData);
            this.Controls.Add(this.lblType);
            this.Controls.Add(this.textData);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EBMCertAuthInfo";
            this.ShowIcon = false;
            this.Text = "添加证书数据";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbBoxType;
        private ControlAstro.Controls.LabelButton btnOpenFile;
        private System.Windows.Forms.Label lblData;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.TextBox textData;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.RichTextBox richText;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
    }
}