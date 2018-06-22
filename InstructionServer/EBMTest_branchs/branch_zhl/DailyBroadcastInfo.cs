using EBMTest.Enums;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace EBMTest
{
    public partial class DailyBroadcastInfo : Form
    {
        private byte tag;
        private OperateType type;
        private Layouts.DailyBroadcastRdsTransferLayout pnlRdsTransfer;

        public DailyBroadcast.DailyProgram Program { get; private set; }

        public DailyBroadcastInfo(OperateType type, byte tag) : this(type, tag, null)
        { }

        public DailyBroadcastInfo(OperateType type, byte tag, DailyBroadcast.DailyProgram program)
        {
            InitializeComponent();
            pnlRdsTransfer = new Layouts.DailyBroadcastRdsTransferLayout();
            pnlRdsTransfer.Location = Point.Empty;
            pnlRdsTransfer.Visible = false;
            Controls.Add(pnlRdsTransfer);

            this.tag = tag;
            this.type = type;
            this.Program = program;
            InitPanelLayout();
        }

        private void InitPanelLayout()
        {
            switch(type)
            {
                case OperateType.Add:
                    Text = "添加日常广播";
                    break;
                case OperateType.Info:
                    Text = "查看日常广播";
                    //pnlChangeProgram.Enabled = false;
                    break;
                case OperateType.Update:
                    Text = "更新日常广播";
                    break;
            }
            switch(tag)
            {
                case 1:
                    Size = new Size(pnlChangeProgram.Width + 18, pnlChangeProgram.Height + 90);
                    pnlChangeProgram.Visible = true;
                    if (type != OperateType.Add && Program != null)
                    {
                        pnlChangeProgram.InitData(Program);
                    }
                    break;
                case 2:
                    Size = new Size(pnlStopProgram.Width + 18, pnlStopProgram.Height + 90);
                    pnlStopProgram.Visible = true;
                    if (type != OperateType.Add && Program != null)
                    {
                        pnlStopProgram.InitData(Program);
                    }
                    break;
                case 3:
                    Size = new Size(pnlPlayCtrl.Width + 18, pnlPlayCtrl.Height + 90);
                    pnlPlayCtrl.Visible = true;
                    if (type != OperateType.Add && Program != null)
                    {
                        pnlPlayCtrl.InitData(Program);
                    }
                    break;
                case 4:
                    Size = new Size(pnlOutSwitch.Width + 18, pnlOutSwitch.Height + 90);
                    pnlOutSwitch.Visible = true;
                    if (type != OperateType.Add && Program != null)
                    {
                        pnlOutSwitch.InitData(Program);
                    }
                    break;
                case 5:
                    Size = new Size(pnlRdsTransfer.Width + 18, pnlRdsTransfer.Height + 90);
                    pnlRdsTransfer.Visible = true;
                    if (type != OperateType.Add && Program != null)
                    {
                        pnlRdsTransfer.InitData(Program);
                    }
                    break;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            switch (tag)
            {
                case 1:
                    if (!pnlChangeProgram.ValidatData()) return;
                    Program = pnlChangeProgram.GetData();
                    break;
                case 2:
                    if (!pnlStopProgram.ValidatData()) return;
                    Program = pnlStopProgram.GetData();
                    break;
                case 3:
                    if (!pnlPlayCtrl.ValidatData()) return;
                    Program = pnlPlayCtrl.GetData();
                    break;
                case 4:
                    if (!pnlOutSwitch.ValidatData()) return;
                    Program = pnlOutSwitch.GetData();
                    break;
                case 5:
                    if (!pnlRdsTransfer.ValidatData()) return;
                    Program = pnlRdsTransfer.GetData();
                    break;
            }
            DialogResult = DialogResult.OK;
        }

    }
}
