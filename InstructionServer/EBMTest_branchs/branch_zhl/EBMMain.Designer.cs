namespace EBMTest
{
    partial class EBMMain
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EBMMain));
            this.menuStripOption = new System.Windows.Forms.MenuStrip();
            this.Option_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemEBIndex = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemEBContent = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemConfigure = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemCertAuth = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemDailyBroadcast = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuItemTSSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemPlayVideo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuItemMdiChildLayout = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemTileH = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemTileV = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemCascade = new System.Windows.Forms.ToolStripMenuItem();
            this.Operate_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemStartStream = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemStopStream = new System.Windows.Forms.ToolStripMenuItem();
            this.ReStartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblText = new System.Windows.Forms.Label();
            this.lblLogIn = new System.Windows.Forms.Label();
            this.menuStripOption.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStripOption
            // 
            this.menuStripOption.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Option_ToolStripMenuItem,
            this.Operate_ToolStripMenuItem,
            this.ReStartToolStripMenuItem});
            this.menuStripOption.Location = new System.Drawing.Point(0, 0);
            this.menuStripOption.Name = "menuStripOption";
            this.menuStripOption.Size = new System.Drawing.Size(899, 25);
            this.menuStripOption.TabIndex = 0;
            this.menuStripOption.Text = "选项";
            // 
            // Option_ToolStripMenuItem
            // 
            this.Option_ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemEBIndex,
            this.MenuItemEBContent,
            this.MenuItemConfigure,
            this.MenuItemCertAuth,
            this.MenuItemDailyBroadcast,
            this.toolStripSeparator1,
            this.MenuItemTSSetting,
            this.MenuItemPlayVideo,
            this.toolStripSeparator2,
            this.MenuItemMdiChildLayout});
            this.Option_ToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.White;
            this.Option_ToolStripMenuItem.Name = "Option_ToolStripMenuItem";
            this.Option_ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.Option_ToolStripMenuItem.Text = "选项";
            // 
            // MenuItemEBIndex
            // 
            this.MenuItemEBIndex.Name = "MenuItemEBIndex";
            this.MenuItemEBIndex.Size = new System.Drawing.Size(196, 22);
            this.MenuItemEBIndex.Text = "应急广播消息索引表";
            this.MenuItemEBIndex.Click += new System.EventHandler(this.MenuItemEBIndex_Click);
            // 
            // MenuItemEBContent
            // 
            this.MenuItemEBContent.Name = "MenuItemEBContent";
            this.MenuItemEBContent.Size = new System.Drawing.Size(196, 22);
            this.MenuItemEBContent.Text = "应急广播消息内容表";
            this.MenuItemEBContent.Visible = false;
            this.MenuItemEBContent.Click += new System.EventHandler(this.MenuItemEBContent_Click);
            // 
            // MenuItemConfigure
            // 
            this.MenuItemConfigure.Name = "MenuItemConfigure";
            this.MenuItemConfigure.Size = new System.Drawing.Size(196, 22);
            this.MenuItemConfigure.Text = "应急广播管理配置表";
            this.MenuItemConfigure.Visible = false;
            this.MenuItemConfigure.Click += new System.EventHandler(this.MenuItemConfigure_Click);
            // 
            // MenuItemCertAuth
            // 
            this.MenuItemCertAuth.Name = "MenuItemCertAuth";
            this.MenuItemCertAuth.Size = new System.Drawing.Size(196, 22);
            this.MenuItemCertAuth.Text = "应急广播证书授权协议";
            this.MenuItemCertAuth.Visible = false;
            this.MenuItemCertAuth.Click += new System.EventHandler(this.MenuItemCertAuth_Click);
            // 
            // MenuItemDailyBroadcast
            // 
            this.MenuItemDailyBroadcast.Name = "MenuItemDailyBroadcast";
            this.MenuItemDailyBroadcast.Size = new System.Drawing.Size(196, 22);
            this.MenuItemDailyBroadcast.Text = "日常广播节目传输表";
            this.MenuItemDailyBroadcast.Click += new System.EventHandler(this.MenuItemDailyBroadcast_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(193, 6);
            // 
            // MenuItemTSSetting
            // 
            this.MenuItemTSSetting.Name = "MenuItemTSSetting";
            this.MenuItemTSSetting.Size = new System.Drawing.Size(196, 22);
            this.MenuItemTSSetting.Text = "流设置";
            this.MenuItemTSSetting.Click += new System.EventHandler(this.MenuItemTSSetting_Click);
            // 
            // MenuItemPlayVideo
            // 
            this.MenuItemPlayVideo.Name = "MenuItemPlayVideo";
            this.MenuItemPlayVideo.Size = new System.Drawing.Size(196, 22);
            this.MenuItemPlayVideo.Text = "播放音频";
            this.MenuItemPlayVideo.Click += new System.EventHandler(this.MenuItemPlayVideo_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(193, 6);
            // 
            // MenuItemMdiChildLayout
            // 
            this.MenuItemMdiChildLayout.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemTileH,
            this.MenuItemTileV,
            this.MenuItemCascade});
            this.MenuItemMdiChildLayout.Name = "MenuItemMdiChildLayout";
            this.MenuItemMdiChildLayout.Size = new System.Drawing.Size(196, 22);
            this.MenuItemMdiChildLayout.Text = "子窗体布局";
            // 
            // MenuItemTileH
            // 
            this.MenuItemTileH.Name = "MenuItemTileH";
            this.MenuItemTileH.Size = new System.Drawing.Size(124, 22);
            this.MenuItemTileH.Text = "水平排布";
            this.MenuItemTileH.Click += new System.EventHandler(this.MenuItemTileH_Click);
            // 
            // MenuItemTileV
            // 
            this.MenuItemTileV.Name = "MenuItemTileV";
            this.MenuItemTileV.Size = new System.Drawing.Size(124, 22);
            this.MenuItemTileV.Text = "垂直排布";
            this.MenuItemTileV.Click += new System.EventHandler(this.MenuItemTileV_Click);
            // 
            // MenuItemCascade
            // 
            this.MenuItemCascade.Name = "MenuItemCascade";
            this.MenuItemCascade.Size = new System.Drawing.Size(124, 22);
            this.MenuItemCascade.Text = "层叠排布";
            this.MenuItemCascade.Click += new System.EventHandler(this.MenuItemCascade_Click);
            // 
            // Operate_ToolStripMenuItem
            // 
            this.Operate_ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemStartStream,
            this.MenuItemStopStream});
            this.Operate_ToolStripMenuItem.Name = "Operate_ToolStripMenuItem";
            this.Operate_ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.Operate_ToolStripMenuItem.Text = "操作";
            this.Operate_ToolStripMenuItem.DropDownOpening += new System.EventHandler(this.Operate_ToolStripMenuItem_DropDownOpening);
            // 
            // MenuItemStartStream
            // 
            this.MenuItemStartStream.Enabled = false;
            this.MenuItemStartStream.Name = "MenuItemStartStream";
            this.MenuItemStartStream.Size = new System.Drawing.Size(124, 22);
            this.MenuItemStartStream.Text = "启动发送";
            this.MenuItemStartStream.Click += new System.EventHandler(this.MenuItemStartStream_Click);
            // 
            // MenuItemStopStream
            // 
            this.MenuItemStopStream.Enabled = false;
            this.MenuItemStopStream.Name = "MenuItemStopStream";
            this.MenuItemStopStream.Size = new System.Drawing.Size(124, 22);
            this.MenuItemStopStream.Text = "停止发送";
            this.MenuItemStopStream.Click += new System.EventHandler(this.MenuItemStopStream_Click);
            // 
            // ReStartToolStripMenuItem
            // 
            this.ReStartToolStripMenuItem.Name = "ReStartToolStripMenuItem";
            this.ReStartToolStripMenuItem.Size = new System.Drawing.Size(76, 21);
            this.ReStartToolStripMenuItem.Text = "              ";
            this.ReStartToolStripMenuItem.Visible = false;
            this.ReStartToolStripMenuItem.Click += new System.EventHandler(this.ReStartToolStripMenuItem_Click);
            // 
            // lblText
            // 
            this.lblText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblText.AutoSize = true;
            this.lblText.BackColor = System.Drawing.Color.Transparent;
            this.lblText.ForeColor = System.Drawing.Color.Red;
            this.lblText.Location = new System.Drawing.Point(734, 488);
            this.lblText.Name = "lblText";
            this.lblText.Size = new System.Drawing.Size(161, 12);
            this.lblText.TabIndex = 2;
            this.lblText.Text = "提示：所有数据按十进制输入";
            // 
            // lblLogIn
            // 
            this.lblLogIn.Location = new System.Drawing.Point(129, 4);
            this.lblLogIn.Name = "lblLogIn";
            this.lblLogIn.Size = new System.Drawing.Size(83, 17);
            this.lblLogIn.TabIndex = 4;
            this.lblLogIn.Text = "             ";
            this.lblLogIn.DoubleClick += new System.EventHandler(this.lblLogIn_DoubleClick);
            // 
            // EBMMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(899, 503);
            this.Controls.Add(this.lblLogIn);
            this.Controls.Add(this.lblText);
            this.Controls.Add(this.menuStripOption);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStripOption;
            this.MinimumSize = new System.Drawing.Size(800, 500);
            this.Name = "EBMMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EBMTest";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EBMMain_FormClosing);
            this.menuStripOption.ResumeLayout(false);
            this.menuStripOption.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStripOption;
        private System.Windows.Forms.ToolStripMenuItem Option_ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MenuItemEBIndex;
        private System.Windows.Forms.ToolStripMenuItem MenuItemEBContent;
        private System.Windows.Forms.ToolStripMenuItem Operate_ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MenuItemStartStream;
        private System.Windows.Forms.ToolStripMenuItem MenuItemStopStream;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem MenuItemMdiChildLayout;
        private System.Windows.Forms.ToolStripMenuItem MenuItemTileH;
        private System.Windows.Forms.ToolStripMenuItem MenuItemTileV;
        private System.Windows.Forms.ToolStripMenuItem MenuItemDailyBroadcast;
        private System.Windows.Forms.ToolStripMenuItem MenuItemTSSetting;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem MenuItemConfigure;
        private System.Windows.Forms.ToolStripMenuItem MenuItemCertAuth;
        private System.Windows.Forms.ToolStripMenuItem MenuItemCascade;
        private System.Windows.Forms.ToolStripMenuItem ReStartToolStripMenuItem;
        private System.Windows.Forms.Label lblText;
        private System.Windows.Forms.ToolStripMenuItem MenuItemPlayVideo;
        private System.Windows.Forms.Label lblLogIn;
    }
}

