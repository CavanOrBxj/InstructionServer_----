namespace EBMTest
{
    partial class EBMIndexDetail
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
            this.btnOK = new System.Windows.Forms.Button();
            this.pnlDes2 = new EBMTest.Layouts.EBMIndexDes();
            this.pnlProgramStreamInfo = new EBMTest.Layouts.EBMIndexProgramStreamInfoLayout();
            this.pnlDetChlDes = new EBMTest.Layouts.EBMIndexDetChlDes();
            this.pnlResourceCode = new EBMTest.Layouts.EBMIndexResourceCode();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOK.Location = new System.Drawing.Point(377, 404);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(190, 34);
            this.btnOK.TabIndex = 48;
            this.btnOK.Text = "确        定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // pnlDes2
            // 
            this.pnlDes2.Location = new System.Drawing.Point(11, 7);
            this.pnlDes2.Name = "pnlDes2";
            this.pnlDes2.Size = new System.Drawing.Size(394, 102);
            this.pnlDes2.TabIndex = 52;
            this.pnlDes2.Visible = false;
            // 
            // pnlProgramStreamInfo
            // 
            this.pnlProgramStreamInfo.Location = new System.Drawing.Point(7, 1);
            this.pnlProgramStreamInfo.Name = "pnlProgramStreamInfo";
            this.pnlProgramStreamInfo.Size = new System.Drawing.Size(518, 382);
            this.pnlProgramStreamInfo.TabIndex = 51;
            this.pnlProgramStreamInfo.Visible = false;
            // 
            // pnlDetChlDes
            // 
            this.pnlDetChlDes.Location = new System.Drawing.Point(4, 7);
            this.pnlDetChlDes.Name = "pnlDetChlDes";
            this.pnlDetChlDes.Size = new System.Drawing.Size(420, 318);
            this.pnlDetChlDes.TabIndex = 50;
            this.pnlDetChlDes.Visible = false;
            // 
            // pnlResourceCode
            // 
            this.pnlResourceCode.Location = new System.Drawing.Point(4, 7);
            this.pnlResourceCode.Name = "pnlResourceCode";
            this.pnlResourceCode.Size = new System.Drawing.Size(448, 226);
            this.pnlResourceCode.TabIndex = 2;
            this.pnlResourceCode.Visible = false;
            // 
            // EBMIndexDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 451);
            this.Controls.Add(this.pnlDes2);
            this.Controls.Add(this.pnlProgramStreamInfo);
            this.Controls.Add(this.pnlDetChlDes);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.pnlResourceCode);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EBMIndexDetail";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.ResumeLayout(false);

        }

        #endregion
        private Layouts.EBMIndexResourceCode pnlResourceCode;
        private System.Windows.Forms.Button btnOK;
        private Layouts.EBMIndexDetChlDes pnlDetChlDes;
        private Layouts.EBMIndexProgramStreamInfoLayout pnlProgramStreamInfo;
        private Layouts.EBMIndexDes pnlDes2;
    }
}