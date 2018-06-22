namespace EBMTest
{
    partial class DailyBroadcastInfo
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
            this.pnlStopProgram = new EBMTest.Layouts.DailyBroadcastStopProgramLayout();
            this.pnlPlayCtrl = new EBMTest.Layouts.DailyBroadcastPlayCtrlLayout();
            this.pnlOutSwitch = new EBMTest.Layouts.DailyBroadcastOutSwitchLayout();
            this.pnlChangeProgram = new EBMTest.Layouts.DailyBroadcastInfoLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOK.Location = new System.Drawing.Point(708, 390);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(190, 34);
            this.btnOK.TabIndex = 47;
            this.btnOK.Text = "确        定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // pnlStopProgram
            // 
            this.pnlStopProgram.Location = new System.Drawing.Point(0, 0);
            this.pnlStopProgram.Name = "pnlStopProgram";
            this.pnlStopProgram.Size = new System.Drawing.Size(902, 305);
            this.pnlStopProgram.TabIndex = 55;
            this.pnlStopProgram.Visible = false;
            // 
            // pnlPlayCtrl
            // 
            this.pnlPlayCtrl.Location = new System.Drawing.Point(0, 0);
            this.pnlPlayCtrl.Name = "pnlPlayCtrl";
            this.pnlPlayCtrl.Size = new System.Drawing.Size(519, 370);
            this.pnlPlayCtrl.TabIndex = 54;
            this.pnlPlayCtrl.Visible = false;
            // 
            // pnlOutSwitch
            // 
            this.pnlOutSwitch.Location = new System.Drawing.Point(0, 0);
            this.pnlOutSwitch.Name = "pnlOutSwitch";
            this.pnlOutSwitch.Size = new System.Drawing.Size(521, 372);
            this.pnlOutSwitch.TabIndex = 53;
            this.pnlOutSwitch.Visible = false;
            // 
            // pnlChangeProgram
            // 
            this.pnlChangeProgram.Location = new System.Drawing.Point(0, 0);
            this.pnlChangeProgram.Name = "pnlChangeProgram";
            this.pnlChangeProgram.Size = new System.Drawing.Size(922, 338);
            this.pnlChangeProgram.TabIndex = 52;
            this.pnlChangeProgram.Visible = false;
            // 
            // DailyBroadcastInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(910, 432);
            this.Controls.Add(this.pnlStopProgram);
            this.Controls.Add(this.pnlPlayCtrl);
            this.Controls.Add(this.pnlOutSwitch);
            this.Controls.Add(this.pnlChangeProgram);
            this.Controls.Add(this.btnOK);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DailyBroadcastInfo";
            this.ShowIcon = false;
            this.Text = "日常广播指令信息";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnOK;
        private Layouts.DailyBroadcastInfoLayout pnlChangeProgram;
        private Layouts.DailyBroadcastOutSwitchLayout pnlOutSwitch;
        private Layouts.DailyBroadcastPlayCtrlLayout pnlPlayCtrl;
        private Layouts.DailyBroadcastStopProgramLayout pnlStopProgram;
    }
}