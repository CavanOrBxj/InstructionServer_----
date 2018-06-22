namespace EBMTest.Layouts
{
    partial class AddDailyBroadcastBtn
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnAdd = new System.Windows.Forms.Button();
            this.pnlAdd = new System.Windows.Forms.Panel();
            this.btnOutSwitch = new System.Windows.Forms.Button();
            this.btnPlayCtrl = new System.Windows.Forms.Button();
            this.btnChangeProgram = new System.Windows.Forms.Button();
            this.btnRdsTransfer = new System.Windows.Forms.Button();
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
            this.btnAdd.Size = new System.Drawing.Size(128, 23);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "添加日常广播";
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
            this.pnlAdd.Controls.Add(this.btnRdsTransfer);
            this.pnlAdd.Controls.Add(this.btnOutSwitch);
            this.pnlAdd.Controls.Add(this.btnPlayCtrl);
            this.pnlAdd.Controls.Add(this.btnChangeProgram);
            this.pnlAdd.Location = new System.Drawing.Point(0, 23);
            this.pnlAdd.Name = "pnlAdd";
            this.pnlAdd.Size = new System.Drawing.Size(128, 120);
            this.pnlAdd.TabIndex = 2;
            this.pnlAdd.MouseLeave += new System.EventHandler(this.pnlAdd_MouseLeave);
            // 
            // btnOutSwitch
            // 
            this.btnOutSwitch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOutSwitch.FlatAppearance.BorderSize = 0;
            this.btnOutSwitch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnOutSwitch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOutSwitch.Location = new System.Drawing.Point(-1, 61);
            this.btnOutSwitch.Name = "btnOutSwitch";
            this.btnOutSwitch.Size = new System.Drawing.Size(128, 23);
            this.btnOutSwitch.TabIndex = 4;
            this.btnOutSwitch.Text = "输出控制指令";
            this.btnOutSwitch.UseVisualStyleBackColor = true;
            this.btnOutSwitch.Click += new System.EventHandler(this.btnOutSwitch_Click);
            // 
            // btnPlayCtrl
            // 
            this.btnPlayCtrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPlayCtrl.FlatAppearance.BorderSize = 0;
            this.btnPlayCtrl.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnPlayCtrl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPlayCtrl.Location = new System.Drawing.Point(-1, 32);
            this.btnPlayCtrl.Name = "btnPlayCtrl";
            this.btnPlayCtrl.Size = new System.Drawing.Size(128, 23);
            this.btnPlayCtrl.TabIndex = 3;
            this.btnPlayCtrl.Text = "播放控制指令";
            this.btnPlayCtrl.UseVisualStyleBackColor = true;
            this.btnPlayCtrl.Click += new System.EventHandler(this.btnPlayCtrl_Click);
            // 
            // btnChangeProgram
            // 
            this.btnChangeProgram.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChangeProgram.FlatAppearance.BorderSize = 0;
            this.btnChangeProgram.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnChangeProgram.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChangeProgram.Location = new System.Drawing.Point(-1, 3);
            this.btnChangeProgram.Name = "btnChangeProgram";
            this.btnChangeProgram.Size = new System.Drawing.Size(128, 23);
            this.btnChangeProgram.TabIndex = 2;
            this.btnChangeProgram.Text = "节目切播指令";
            this.btnChangeProgram.UseVisualStyleBackColor = true;
            this.btnChangeProgram.Click += new System.EventHandler(this.btnChangeProgram_Click);
            // 
            // btnRdsTransfer
            // 
            this.btnRdsTransfer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRdsTransfer.FlatAppearance.BorderSize = 0;
            this.btnRdsTransfer.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnRdsTransfer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRdsTransfer.Location = new System.Drawing.Point(-1, 91);
            this.btnRdsTransfer.Name = "btnRdsTransfer";
            this.btnRdsTransfer.Size = new System.Drawing.Size(128, 23);
            this.btnRdsTransfer.TabIndex = 5;
            this.btnRdsTransfer.Text = "RDS编码数据透传";
            this.btnRdsTransfer.UseVisualStyleBackColor = true;
            this.btnRdsTransfer.Click += new System.EventHandler(this.btnRdsTransfer_Click);
            // 
            // AddDailyBroadcastBtn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.pnlAdd);
            this.DoubleBuffered = true;
            this.Name = "AddDailyBroadcastBtn";
            this.Size = new System.Drawing.Size(128, 143);
            this.pnlAdd.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Panel pnlAdd;
        private System.Windows.Forms.Button btnChangeProgram;
        private System.Windows.Forms.Button btnOutSwitch;
        private System.Windows.Forms.Button btnPlayCtrl;
        private System.Windows.Forms.Button btnRdsTransfer;
    }
}
