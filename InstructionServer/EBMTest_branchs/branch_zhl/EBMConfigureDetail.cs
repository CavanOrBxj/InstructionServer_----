using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace EBMTest
{
    public partial class EBMConfigureDetail : Form
    {
        private int tag;
        private object configInfo;

        public EBMConfigureDetail(int tag, object configInfo)
        {
            InitializeComponent();
            this.tag = tag;
            this.configInfo = configInfo;
            InitPanelLayout();
        }

        private void InitPanelLayout()
        {
            switch (tag)
            {
                case 0:
                    Text = "终端编号地址";
                    Size = new Size(pnlTerminalAddress.Width + 25, pnlTerminalAddress.Height + 110);
                    pnlTerminalAddress.Visible = true;
                    if (configInfo != null)
                    {
                        pnlTerminalAddress.InitData(configInfo as List<string>);
                    }
                    break;
                case 1:
                    Text = "终端工作状态参数";
                    Size = new Size(pnlParameterTag.Width + 25, pnlParameterTag.Height + 110);
                    pnlParameterTag.Visible = true;
                    if (configInfo != null)
                    {
                        pnlParameterTag.InitData(configInfo as List<byte>);
                    }
                    break;
            }
        }

        public object GetData()
        {
            object data = null;
            switch (tag)
            {
                case 0:
                    data = pnlTerminalAddress.GetData();
                    break;
                case 1:
                    data = pnlParameterTag.GetData();
                    break;
            }
            return data;
        }

        private void btnOK_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

    }
}
