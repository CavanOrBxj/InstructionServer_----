namespace EBMTest
{
    partial class EBMContentDetail
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
            this.pnlAuxiliaryData = new EBMTest.Layouts.ContentAuxiliaryDataLayout();
            this.btnOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // pnlAuxiliaryData
            // 
            this.pnlAuxiliaryData.Location = new System.Drawing.Point(0, 0);
            this.pnlAuxiliaryData.Name = "pnlAuxiliaryData";
            this.pnlAuxiliaryData.Size = new System.Drawing.Size(514, 304);
            this.pnlAuxiliaryData.TabIndex = 0;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOK.Location = new System.Drawing.Point(312, 307);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(190, 34);
            this.btnOK.TabIndex = 49;
            this.btnOK.Text = "确        定";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // EBMContentDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(514, 349);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.pnlAuxiliaryData);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EBMContentDetail";
            this.Text = "辅助内容";
            this.ResumeLayout(false);

        }

        #endregion

        private Layouts.ContentAuxiliaryDataLayout pnlAuxiliaryData;
        private System.Windows.Forms.Button btnOK;
    }
}