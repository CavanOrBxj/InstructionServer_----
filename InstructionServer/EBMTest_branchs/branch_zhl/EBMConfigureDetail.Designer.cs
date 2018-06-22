namespace EBMTest
{
    partial class EBMConfigureDetail
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
            this.pnlTerminalAddress = new EBMTest.Layouts.TerminalAddressLayout();
            this.btnOK = new System.Windows.Forms.Button();
            this.pnlParameterTag = new EBMTest.Layouts.ParameterTagLayout();
            this.SuspendLayout();
            // 
            // pnlTerminalAddress
            // 
            this.pnlTerminalAddress.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlTerminalAddress.Location = new System.Drawing.Point(5, 8);
            this.pnlTerminalAddress.Name = "pnlTerminalAddress";
            this.pnlTerminalAddress.Size = new System.Drawing.Size(545, 330);
            this.pnlTerminalAddress.TabIndex = 0;
            this.pnlTerminalAddress.Visible = false;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOK.Location = new System.Drawing.Point(355, 344);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(190, 34);
            this.btnOK.TabIndex = 48;
            this.btnOK.Text = "确        定";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // pnlParameterTag
            // 
            this.pnlParameterTag.Location = new System.Drawing.Point(5, 8);
            this.pnlParameterTag.Name = "pnlParameterTag";
            this.pnlParameterTag.Size = new System.Drawing.Size(384, 278);
            this.pnlParameterTag.TabIndex = 49;
            this.pnlParameterTag.Visible = false;
            // 
            // EBMConfigureDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(556, 388);
            this.Controls.Add(this.pnlParameterTag);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.pnlTerminalAddress);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EBMConfigureDetail";
            this.Text = "配置终端编号地址";
            this.ResumeLayout(false);

        }

        #endregion

        private Layouts.TerminalAddressLayout pnlTerminalAddress;
        private System.Windows.Forms.Button btnOK;
        private Layouts.ParameterTagLayout pnlParameterTag;
    }
}