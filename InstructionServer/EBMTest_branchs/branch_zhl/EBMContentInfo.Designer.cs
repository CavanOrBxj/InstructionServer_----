namespace EBMTest
{
    partial class EBMContentInfo
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
            this.lblB_message_text = new System.Windows.Forms.Label();
            this.lblB_code_character_set = new System.Windows.Forms.Label();
            this.textS_language_code = new System.Windows.Forms.TextBox();
            this.lblS_language_code = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.cbBoxB_code_character_set = new System.Windows.Forms.ComboBox();
            this.pnlAuxiliaryData = new EBMTest.Layouts.ContentAuxiliaryDataLayout();
            this.textB_message_text = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // lblB_message_text
            // 
            this.lblB_message_text.AutoSize = true;
            this.lblB_message_text.Location = new System.Drawing.Point(33, 66);
            this.lblB_message_text.Name = "lblB_message_text";
            this.lblB_message_text.Size = new System.Drawing.Size(53, 12);
            this.lblB_message_text.TabIndex = 50;
            this.lblB_message_text.Text = "文本内容";
            // 
            // lblB_code_character_set
            // 
            this.lblB_code_character_set.AutoSize = true;
            this.lblB_code_character_set.Location = new System.Drawing.Point(21, 12);
            this.lblB_code_character_set.Name = "lblB_code_character_set";
            this.lblB_code_character_set.Size = new System.Drawing.Size(65, 12);
            this.lblB_code_character_set.TabIndex = 49;
            this.lblB_code_character_set.Text = "编码字符集";
            // 
            // textS_language_code
            // 
            this.textS_language_code.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textS_language_code.Location = new System.Drawing.Point(101, 35);
            this.textS_language_code.Name = "textS_language_code";
            this.textS_language_code.Size = new System.Drawing.Size(396, 21);
            this.textS_language_code.TabIndex = 3;
            this.textS_language_code.Tag = "语种代码";
            this.textS_language_code.Text = "zho";
            // 
            // lblS_language_code
            // 
            this.lblS_language_code.AutoSize = true;
            this.lblS_language_code.Location = new System.Drawing.Point(33, 39);
            this.lblS_language_code.Name = "lblS_language_code";
            this.lblS_language_code.Size = new System.Drawing.Size(53, 12);
            this.lblS_language_code.TabIndex = 53;
            this.lblS_language_code.Text = "语种代码";
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOK.Location = new System.Drawing.Point(325, 463);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(190, 34);
            this.btnOK.TabIndex = 55;
            this.btnOK.Text = "确        定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // cbBoxB_code_character_set
            // 
            this.cbBoxB_code_character_set.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBoxB_code_character_set.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbBoxB_code_character_set.FormattingEnabled = true;
            this.cbBoxB_code_character_set.Location = new System.Drawing.Point(101, 8);
            this.cbBoxB_code_character_set.Name = "cbBoxB_code_character_set";
            this.cbBoxB_code_character_set.Size = new System.Drawing.Size(229, 20);
            this.cbBoxB_code_character_set.TabIndex = 1;
            // 
            // pnlAuxiliaryData
            // 
            this.pnlAuxiliaryData.Location = new System.Drawing.Point(7, 153);
            this.pnlAuxiliaryData.Name = "pnlAuxiliaryData";
            this.pnlAuxiliaryData.Size = new System.Drawing.Size(514, 304);
            this.pnlAuxiliaryData.TabIndex = 0;
            // 
            // textB_message_text
            // 
            this.textB_message_text.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textB_message_text.Location = new System.Drawing.Point(101, 63);
            this.textB_message_text.Name = "textB_message_text";
            this.textB_message_text.Size = new System.Drawing.Size(396, 84);
            this.textB_message_text.TabIndex = 56;
            this.textB_message_text.Text = "";
            // 
            // EBMContentInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(530, 505);
            this.Controls.Add(this.textB_message_text);
            this.Controls.Add(this.cbBoxB_code_character_set);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.textS_language_code);
            this.Controls.Add(this.lblS_language_code);
            this.Controls.Add(this.lblB_message_text);
            this.Controls.Add(this.lblB_code_character_set);
            this.Controls.Add(this.pnlAuxiliaryData);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EBMContentInfo";
            this.ShowIcon = false;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Layouts.ContentAuxiliaryDataLayout pnlAuxiliaryData;
        private System.Windows.Forms.Label lblB_message_text;
        private System.Windows.Forms.Label lblB_code_character_set;
        private System.Windows.Forms.TextBox textS_language_code;
        private System.Windows.Forms.Label lblS_language_code;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.ComboBox cbBoxB_code_character_set;
        private System.Windows.Forms.RichTextBox textB_message_text;
    }
}