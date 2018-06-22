namespace EBMTest.Forms
{
    partial class FormMediaStreamer
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
            this.lblVideoRate = new System.Windows.Forms.Label();
            this.textVideoRate = new System.Windows.Forms.TextBox();
            this.lblVideoPID = new System.Windows.Forms.Label();
            this.textVideoPID = new System.Windows.Forms.TextBox();
            this.lblAudioPID = new System.Windows.Forms.Label();
            this.lblRemotePoint = new System.Windows.Forms.Label();
            this.lblPath = new System.Windows.Forms.Label();
            this.textAudioPID = new System.Windows.Forms.TextBox();
            this.textIP = new System.Windows.Forms.TextBox();
            this.textPath = new System.Windows.Forms.TextBox();
            this.lblAudioRate = new System.Windows.Forms.Label();
            this.textAudioRate = new System.Windows.Forms.TextBox();
            this.lblFile = new ControlAstro.Controls.LabelButton();
            this.textPort = new System.Windows.Forms.TextBox();
            this.lbl = new System.Windows.Forms.Label();
            this.fileDialog = new System.Windows.Forms.OpenFileDialog();
            this.btnSendOrder = new System.Windows.Forms.Button();
            this.lblOrder = new System.Windows.Forms.Label();
            this.textOrder = new System.Windows.Forms.TextBox();
            this.lblFileInfo = new System.Windows.Forms.Label();
            this.lblOrderInfo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOK.Location = new System.Drawing.Point(180, 260);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(190, 34);
            this.btnOK.TabIndex = 98;
            this.btnOK.Text = "开始播放音频";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // lblVideoRate
            // 
            this.lblVideoRate.AutoSize = true;
            this.lblVideoRate.Location = new System.Drawing.Point(14, 190);
            this.lblVideoRate.Name = "lblVideoRate";
            this.lblVideoRate.Size = new System.Drawing.Size(89, 12);
            this.lblVideoRate.TabIndex = 97;
            this.lblVideoRate.Text = "视频码率(kbps)";
            // 
            // textVideoRate
            // 
            this.textVideoRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textVideoRate.Location = new System.Drawing.Point(109, 186);
            this.textVideoRate.Name = "textVideoRate";
            this.textVideoRate.Size = new System.Drawing.Size(261, 21);
            this.textVideoRate.TabIndex = 86;
            this.textVideoRate.Tag = "视频码率";
            // 
            // lblVideoPID
            // 
            this.lblVideoPID.AutoSize = true;
            this.lblVideoPID.Location = new System.Drawing.Point(56, 153);
            this.lblVideoPID.Name = "lblVideoPID";
            this.lblVideoPID.Size = new System.Drawing.Size(47, 12);
            this.lblVideoPID.TabIndex = 96;
            this.lblVideoPID.Text = "视频PID";
            // 
            // textVideoPID
            // 
            this.textVideoPID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textVideoPID.Location = new System.Drawing.Point(109, 149);
            this.textVideoPID.Name = "textVideoPID";
            this.textVideoPID.Size = new System.Drawing.Size(261, 21);
            this.textVideoPID.TabIndex = 85;
            this.textVideoPID.Tag = "视频PID";
            // 
            // lblAudioPID
            // 
            this.lblAudioPID.AutoSize = true;
            this.lblAudioPID.Location = new System.Drawing.Point(56, 117);
            this.lblAudioPID.Name = "lblAudioPID";
            this.lblAudioPID.Size = new System.Drawing.Size(47, 12);
            this.lblAudioPID.TabIndex = 95;
            this.lblAudioPID.Text = "音频PID";
            // 
            // lblRemotePoint
            // 
            this.lblRemotePoint.AutoSize = true;
            this.lblRemotePoint.Location = new System.Drawing.Point(50, 81);
            this.lblRemotePoint.Name = "lblRemotePoint";
            this.lblRemotePoint.Size = new System.Drawing.Size(53, 12);
            this.lblRemotePoint.TabIndex = 94;
            this.lblRemotePoint.Text = "IP和端口";
            // 
            // lblPath
            // 
            this.lblPath.AutoSize = true;
            this.lblPath.Location = new System.Drawing.Point(50, 46);
            this.lblPath.Name = "lblPath";
            this.lblPath.Size = new System.Drawing.Size(53, 12);
            this.lblPath.TabIndex = 93;
            this.lblPath.Text = "文件路径";
            // 
            // textAudioPID
            // 
            this.textAudioPID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textAudioPID.Location = new System.Drawing.Point(109, 113);
            this.textAudioPID.Name = "textAudioPID";
            this.textAudioPID.Size = new System.Drawing.Size(261, 21);
            this.textAudioPID.TabIndex = 84;
            this.textAudioPID.Tag = "音频PID";
            // 
            // textIP
            // 
            this.textIP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textIP.Location = new System.Drawing.Point(109, 77);
            this.textIP.Name = "textIP";
            this.textIP.Size = new System.Drawing.Size(167, 21);
            this.textIP.TabIndex = 2;
            this.textIP.Tag = "IP";
            // 
            // textPath
            // 
            this.textPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textPath.Location = new System.Drawing.Point(109, 42);
            this.textPath.Name = "textPath";
            this.textPath.Size = new System.Drawing.Size(230, 21);
            this.textPath.TabIndex = 1;
            this.textPath.Tag = "文件路径";
            // 
            // lblAudioRate
            // 
            this.lblAudioRate.AutoSize = true;
            this.lblAudioRate.Location = new System.Drawing.Point(14, 224);
            this.lblAudioRate.Name = "lblAudioRate";
            this.lblAudioRate.Size = new System.Drawing.Size(89, 12);
            this.lblAudioRate.TabIndex = 90;
            this.lblAudioRate.Text = "音频码率(kbps)";
            // 
            // textAudioRate
            // 
            this.textAudioRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textAudioRate.Location = new System.Drawing.Point(109, 220);
            this.textAudioRate.Name = "textAudioRate";
            this.textAudioRate.Size = new System.Drawing.Size(261, 21);
            this.textAudioRate.TabIndex = 87;
            this.textAudioRate.Tag = "音频码率";
            // 
            // lblFile
            // 
            this.lblFile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblFile.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.lblFile.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblFile.Font = new System.Drawing.Font("宋体", 9F);
            this.lblFile.Location = new System.Drawing.Point(345, 43);
            this.lblFile.Name = "lblFile";
            this.lblFile.Size = new System.Drawing.Size(25, 20);
            this.lblFile.TabIndex = 99;
            this.lblFile.Text = "...";
            this.lblFile.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.lblFile.Click += new System.EventHandler(this.lblFile_Click);
            // 
            // textPort
            // 
            this.textPort.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textPort.Location = new System.Drawing.Point(301, 77);
            this.textPort.Name = "textPort";
            this.textPort.Size = new System.Drawing.Size(69, 21);
            this.textPort.TabIndex = 3;
            this.textPort.Tag = "端口";
            // 
            // lbl
            // 
            this.lbl.AutoSize = true;
            this.lbl.Location = new System.Drawing.Point(283, 81);
            this.lbl.Name = "lbl";
            this.lbl.Size = new System.Drawing.Size(11, 12);
            this.lbl.TabIndex = 101;
            this.lbl.Text = ":";
            // 
            // btnSendOrder
            // 
            this.btnSendOrder.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSendOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSendOrder.Location = new System.Drawing.Point(180, 386);
            this.btnSendOrder.Name = "btnSendOrder";
            this.btnSendOrder.Size = new System.Drawing.Size(190, 34);
            this.btnSendOrder.TabIndex = 102;
            this.btnSendOrder.Text = "发送指令";
            this.btnSendOrder.UseVisualStyleBackColor = true;
            this.btnSendOrder.Visible = false;
            this.btnSendOrder.Click += new System.EventHandler(this.btnSendOrder_Click);
            // 
            // lblOrder
            // 
            this.lblOrder.AutoSize = true;
            this.lblOrder.Location = new System.Drawing.Point(50, 351);
            this.lblOrder.Name = "lblOrder";
            this.lblOrder.Size = new System.Drawing.Size(53, 12);
            this.lblOrder.TabIndex = 104;
            this.lblOrder.Text = "指令内容";
            this.lblOrder.Visible = false;
            // 
            // textOrder
            // 
            this.textOrder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textOrder.Location = new System.Drawing.Point(109, 347);
            this.textOrder.Name = "textOrder";
            this.textOrder.Size = new System.Drawing.Size(261, 21);
            this.textOrder.TabIndex = 101;
            this.textOrder.Tag = "";
            this.textOrder.Visible = false;
            // 
            // lblFileInfo
            // 
            this.lblFileInfo.Location = new System.Drawing.Point(167, 15);
            this.lblFileInfo.Name = "lblFileInfo";
            this.lblFileInfo.Size = new System.Drawing.Size(60, 12);
            this.lblFileInfo.TabIndex = 105;
            this.lblFileInfo.Text = "参数";
            this.lblFileInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblOrderInfo
            // 
            this.lblOrderInfo.Location = new System.Drawing.Point(167, 314);
            this.lblOrderInfo.Name = "lblOrderInfo";
            this.lblOrderInfo.Size = new System.Drawing.Size(60, 12);
            this.lblOrderInfo.TabIndex = 106;
            this.lblOrderInfo.Text = "指令信息";
            this.lblOrderInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblOrderInfo.Visible = false;
            // 
            // FormMediaStreamer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(393, 308);
            this.Controls.Add(this.lblOrderInfo);
            this.Controls.Add(this.lblFileInfo);
            this.Controls.Add(this.lblOrder);
            this.Controls.Add(this.textOrder);
            this.Controls.Add(this.btnSendOrder);
            this.Controls.Add(this.lbl);
            this.Controls.Add(this.textPort);
            this.Controls.Add(this.lblFile);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lblVideoRate);
            this.Controls.Add(this.textVideoRate);
            this.Controls.Add(this.lblVideoPID);
            this.Controls.Add(this.textVideoPID);
            this.Controls.Add(this.lblAudioPID);
            this.Controls.Add(this.lblRemotePoint);
            this.Controls.Add(this.lblPath);
            this.Controls.Add(this.textAudioPID);
            this.Controls.Add(this.textIP);
            this.Controls.Add(this.textPath);
            this.Controls.Add(this.lblAudioRate);
            this.Controls.Add(this.textAudioRate);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormMediaStreamer";
            this.ShowIcon = false;
            this.Text = "音频播放";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMediaStreamer_FormClosing);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FormMediaStreamer_Paint);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label lblVideoRate;
        private System.Windows.Forms.TextBox textVideoRate;
        private System.Windows.Forms.Label lblVideoPID;
        private System.Windows.Forms.TextBox textVideoPID;
        private System.Windows.Forms.Label lblAudioPID;
        private System.Windows.Forms.Label lblRemotePoint;
        private System.Windows.Forms.Label lblPath;
        private System.Windows.Forms.TextBox textAudioPID;
        private System.Windows.Forms.TextBox textIP;
        private System.Windows.Forms.TextBox textPath;
        private System.Windows.Forms.Label lblAudioRate;
        private System.Windows.Forms.TextBox textAudioRate;
        private ControlAstro.Controls.LabelButton lblFile;
        private System.Windows.Forms.TextBox textPort;
        private System.Windows.Forms.Label lbl;
        private System.Windows.Forms.OpenFileDialog fileDialog;
        private System.Windows.Forms.Button btnSendOrder;
        private System.Windows.Forms.Label lblOrder;
        private System.Windows.Forms.TextBox textOrder;
        private System.Windows.Forms.Label lblFileInfo;
        private System.Windows.Forms.Label lblOrderInfo;
    }
}