namespace EBMTest.Layouts
{
    partial class AddConfigureBtn
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
            this.btnStatusRetback = new System.Windows.Forms.Button();
            this.btnRealMoniter = new System.Windows.Forms.Button();
            this.btnContentMoniterRetback = new System.Windows.Forms.Button();
            this.btnPeriod = new System.Windows.Forms.Button();
            this.btnDefaltVolumn = new System.Windows.Forms.Button();
            this.btnReback = new System.Windows.Forms.Button();
            this.btnMainFreq = new System.Windows.Forms.Button();
            this.btnWorkMode = new System.Windows.Forms.Button();
            this.btnSetAddress = new System.Windows.Forms.Button();
            this.btnTimeService = new System.Windows.Forms.Button();
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
            this.btnAdd.Size = new System.Drawing.Size(180, 23);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = "添加管理配置";
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
            this.pnlAdd.Controls.Add(this.btnStatusRetback);
            this.pnlAdd.Controls.Add(this.btnRealMoniter);
            this.pnlAdd.Controls.Add(this.btnContentMoniterRetback);
            this.pnlAdd.Controls.Add(this.btnPeriod);
            this.pnlAdd.Controls.Add(this.btnDefaltVolumn);
            this.pnlAdd.Controls.Add(this.btnReback);
            this.pnlAdd.Controls.Add(this.btnMainFreq);
            this.pnlAdd.Controls.Add(this.btnWorkMode);
            this.pnlAdd.Controls.Add(this.btnSetAddress);
            this.pnlAdd.Controls.Add(this.btnTimeService);
            this.pnlAdd.Location = new System.Drawing.Point(0, 23);
            this.pnlAdd.Name = "pnlAdd";
            this.pnlAdd.Size = new System.Drawing.Size(180, 292);
            this.pnlAdd.TabIndex = 4;
            this.pnlAdd.MouseLeave += new System.EventHandler(this.pnlAdd_MouseLeave);
            // 
            // btnStatusRetback
            // 
            this.btnStatusRetback.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStatusRetback.FlatAppearance.BorderSize = 0;
            this.btnStatusRetback.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnStatusRetback.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStatusRetback.Location = new System.Drawing.Point(-1, 264);
            this.btnStatusRetback.Name = "btnStatusRetback";
            this.btnStatusRetback.Size = new System.Drawing.Size(180, 23);
            this.btnStatusRetback.TabIndex = 11;
            this.btnStatusRetback.Text = "终端工作状态查询指令";
            this.btnStatusRetback.UseVisualStyleBackColor = true;
            this.btnStatusRetback.Click += new System.EventHandler(this.btnStatusRetback_Click);
            // 
            // btnRealMoniter
            // 
            this.btnRealMoniter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRealMoniter.FlatAppearance.BorderSize = 0;
            this.btnRealMoniter.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnRealMoniter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRealMoniter.Location = new System.Drawing.Point(-1, 235);
            this.btnRealMoniter.Name = "btnRealMoniter";
            this.btnRealMoniter.Size = new System.Drawing.Size(180, 23);
            this.btnRealMoniter.TabIndex = 10;
            this.btnRealMoniter.Text = "启动内容监测实时监听指令";
            this.btnRealMoniter.UseVisualStyleBackColor = true;
            this.btnRealMoniter.Click += new System.EventHandler(this.btnRealMoniter_Click);
            // 
            // btnContentMoniterRetback
            // 
            this.btnContentMoniterRetback.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnContentMoniterRetback.FlatAppearance.BorderSize = 0;
            this.btnContentMoniterRetback.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnContentMoniterRetback.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnContentMoniterRetback.Location = new System.Drawing.Point(-1, 206);
            this.btnContentMoniterRetback.Name = "btnContentMoniterRetback";
            this.btnContentMoniterRetback.Size = new System.Drawing.Size(180, 23);
            this.btnContentMoniterRetback.TabIndex = 9;
            this.btnContentMoniterRetback.Text = "启动内容监测回传指令";
            this.btnContentMoniterRetback.UseVisualStyleBackColor = true;
            this.btnContentMoniterRetback.Click += new System.EventHandler(this.btnContentMoniterRetback_Click);
            // 
            // btnPeriod
            // 
            this.btnPeriod.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPeriod.FlatAppearance.BorderSize = 0;
            this.btnPeriod.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnPeriod.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPeriod.Location = new System.Drawing.Point(-1, 177);
            this.btnPeriod.Name = "btnPeriod";
            this.btnPeriod.Size = new System.Drawing.Size(180, 23);
            this.btnPeriod.TabIndex = 8;
            this.btnPeriod.Text = "回传周期指令";
            this.btnPeriod.UseVisualStyleBackColor = true;
            this.btnPeriod.Click += new System.EventHandler(this.btnPeriod_Click);
            // 
            // btnDefaltVolumn
            // 
            this.btnDefaltVolumn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDefaltVolumn.FlatAppearance.BorderSize = 0;
            this.btnDefaltVolumn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnDefaltVolumn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDefaltVolumn.Location = new System.Drawing.Point(-1, 148);
            this.btnDefaltVolumn.Name = "btnDefaltVolumn";
            this.btnDefaltVolumn.Size = new System.Drawing.Size(180, 23);
            this.btnDefaltVolumn.TabIndex = 7;
            this.btnDefaltVolumn.Text = "设置默认音量指令";
            this.btnDefaltVolumn.UseVisualStyleBackColor = true;
            this.btnDefaltVolumn.Click += new System.EventHandler(this.btnDefaltVolumn_Click);
            // 
            // btnReback
            // 
            this.btnReback.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReback.FlatAppearance.BorderSize = 0;
            this.btnReback.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnReback.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReback.Location = new System.Drawing.Point(-1, 119);
            this.btnReback.Name = "btnReback";
            this.btnReback.Size = new System.Drawing.Size(180, 23);
            this.btnReback.TabIndex = 6;
            this.btnReback.Text = "设置回传方式指令";
            this.btnReback.UseVisualStyleBackColor = true;
            this.btnReback.Click += new System.EventHandler(this.btnReback_Click);
            // 
            // btnMainFreq
            // 
            this.btnMainFreq.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMainFreq.FlatAppearance.BorderSize = 0;
            this.btnMainFreq.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnMainFreq.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMainFreq.Location = new System.Drawing.Point(-1, 90);
            this.btnMainFreq.Name = "btnMainFreq";
            this.btnMainFreq.Size = new System.Drawing.Size(180, 23);
            this.btnMainFreq.TabIndex = 5;
            this.btnMainFreq.Text = "锁定频率设置指令";
            this.btnMainFreq.UseVisualStyleBackColor = true;
            this.btnMainFreq.Click += new System.EventHandler(this.btnMainFreq_Click);
            // 
            // btnWorkMode
            // 
            this.btnWorkMode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnWorkMode.FlatAppearance.BorderSize = 0;
            this.btnWorkMode.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnWorkMode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnWorkMode.Location = new System.Drawing.Point(-1, 61);
            this.btnWorkMode.Name = "btnWorkMode";
            this.btnWorkMode.Size = new System.Drawing.Size(180, 23);
            this.btnWorkMode.TabIndex = 4;
            this.btnWorkMode.Text = "工作模式设置指令";
            this.btnWorkMode.UseVisualStyleBackColor = true;
            this.btnWorkMode.Click += new System.EventHandler(this.btnWorkMode_Click);
            // 
            // btnSetAddress
            // 
            this.btnSetAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSetAddress.FlatAppearance.BorderSize = 0;
            this.btnSetAddress.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnSetAddress.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSetAddress.Location = new System.Drawing.Point(-1, 32);
            this.btnSetAddress.Name = "btnSetAddress";
            this.btnSetAddress.Size = new System.Drawing.Size(180, 23);
            this.btnSetAddress.TabIndex = 3;
            this.btnSetAddress.Text = "区域码设置指令";
            this.btnSetAddress.UseVisualStyleBackColor = true;
            this.btnSetAddress.Click += new System.EventHandler(this.btnSetAddress_Click);
            // 
            // btnTimeService
            // 
            this.btnTimeService.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTimeService.FlatAppearance.BorderSize = 0;
            this.btnTimeService.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnTimeService.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTimeService.Location = new System.Drawing.Point(-1, 3);
            this.btnTimeService.Name = "btnTimeService";
            this.btnTimeService.Size = new System.Drawing.Size(180, 23);
            this.btnTimeService.TabIndex = 2;
            this.btnTimeService.Text = "时间校准指令";
            this.btnTimeService.UseVisualStyleBackColor = true;
            this.btnTimeService.Click += new System.EventHandler(this.btnTimeService_Click);
            // 
            // AddConfigureBtn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.pnlAdd);
            this.DoubleBuffered = true;
            this.Name = "AddConfigureBtn";
            this.Size = new System.Drawing.Size(180, 315);
            this.pnlAdd.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Panel pnlAdd;
        private System.Windows.Forms.Button btnWorkMode;
        private System.Windows.Forms.Button btnSetAddress;
        private System.Windows.Forms.Button btnTimeService;
        private System.Windows.Forms.Button btnPeriod;
        private System.Windows.Forms.Button btnDefaltVolumn;
        private System.Windows.Forms.Button btnReback;
        private System.Windows.Forms.Button btnMainFreq;
        private System.Windows.Forms.Button btnStatusRetback;
        private System.Windows.Forms.Button btnRealMoniter;
        private System.Windows.Forms.Button btnContentMoniterRetback;
    }
}
