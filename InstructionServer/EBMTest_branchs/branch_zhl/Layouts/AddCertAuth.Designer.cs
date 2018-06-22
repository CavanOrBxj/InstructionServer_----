namespace EBMTest.Layouts
{
    partial class AddCertAuth
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
            this.btnAdd = new System.Windows.Forms.Button();
            this.pnlAdd = new System.Windows.Forms.Panel();
            this.btnCertAuth = new System.Windows.Forms.Button();
            this.btnCert = new System.Windows.Forms.Button();
            this.pnlAdd.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.FlatAppearance.BorderSize = 0;
            this.btnAdd.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightGray;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Location = new System.Drawing.Point(0, 0);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(160, 23);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = "添加证书授权信息";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Paint += new System.Windows.Forms.PaintEventHandler(this.btnAdd_Paint);
            this.btnAdd.MouseEnter += new System.EventHandler(this.btnAdd_MouseEnter);
            this.btnAdd.MouseLeave += new System.EventHandler(this.btnAdd_MouseLeave);
            // 
            // pnlAdd
            // 
            this.pnlAdd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlAdd.BackColor = System.Drawing.SystemColors.Window;
            this.pnlAdd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlAdd.Controls.Add(this.btnCertAuth);
            this.pnlAdd.Controls.Add(this.btnCert);
            this.pnlAdd.Location = new System.Drawing.Point(0, 23);
            this.pnlAdd.Name = "pnlAdd";
            this.pnlAdd.Size = new System.Drawing.Size(160, 62);
            this.pnlAdd.TabIndex = 4;
            this.pnlAdd.Leave += new System.EventHandler(this.pnlAdd_Leave);
            // 
            // btnCertAuth
            // 
            this.btnCertAuth.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCertAuth.FlatAppearance.BorderSize = 0;
            this.btnCertAuth.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnCertAuth.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCertAuth.Location = new System.Drawing.Point(-1, 32);
            this.btnCertAuth.Name = "btnCertAuth";
            this.btnCertAuth.Size = new System.Drawing.Size(160, 23);
            this.btnCertAuth.TabIndex = 3;
            this.btnCertAuth.Text = "证书授权信息";
            this.btnCertAuth.UseVisualStyleBackColor = true;
            this.btnCertAuth.Click += new System.EventHandler(this.btnCertAuth_Click);
            // 
            // btnCert
            // 
            this.btnCert.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCert.FlatAppearance.BorderSize = 0;
            this.btnCert.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnCert.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCert.Location = new System.Drawing.Point(-1, 3);
            this.btnCert.Name = "btnCert";
            this.btnCert.Size = new System.Drawing.Size(160, 23);
            this.btnCert.TabIndex = 2;
            this.btnCert.Text = "证书信息";
            this.btnCert.UseVisualStyleBackColor = true;
            this.btnCert.Click += new System.EventHandler(this.btnCert_Click);
            // 
            // AddCertAuth
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.pnlAdd);
            this.Name = "AddCertAuth";
            this.Size = new System.Drawing.Size(160, 85);
            this.pnlAdd.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Panel pnlAdd;
        private System.Windows.Forms.Button btnCertAuth;
        private System.Windows.Forms.Button btnCert;
    }
}
