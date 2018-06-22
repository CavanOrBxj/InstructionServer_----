using EBMTable;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace EBMTest
{
    public partial class EBMIndexDetail : Form
    {
        private bool canEdit;
        private int tag;
        private object indexInfo;

        public EBMIndexDetail(int tag, object indexInfo, bool canEdit)
        {
            InitializeComponent();
            this.tag = tag;
            this.canEdit = canEdit;
            this.indexInfo = indexInfo;
            InitPanelLayout();
        }

        private void InitPanelLayout()
        {
            switch (tag)
            {
                case 0:
                    Text = "消息覆盖资源代码";
                    Size = new Size(pnlResourceCode.Width + 25, pnlResourceCode.Height + 110);
                    pnlResourceCode.Visible = true;
                    if (indexInfo != null)
                    {
                        pnlResourceCode.InitData(indexInfo as List<string>, canEdit);
                    }
                    break;
                case 1:
                    Text = "详情频道描述符";
                    Size = new Size(pnlDetChlDes.Width + 25, pnlDetChlDes.Height + 110);
                    pnlDetChlDes.Visible = true;
                    if (indexInfo != null)
                    {
                        pnlDetChlDes.InitData(indexInfo, canEdit);
                    }
                    break;
                case 2:
                    Text = "详情频道节目流信息列表";
                    Size = new Size(pnlProgramStreamInfo.Width + 25, pnlProgramStreamInfo.Height + 110);
                    pnlProgramStreamInfo.Visible = true;
                    if (indexInfo != null)
                    {
                        pnlProgramStreamInfo.InitData(indexInfo as List<ProgramStreamInfo>, canEdit);
                    }
                    break;
                case 3:
                    Text = "2类节目描述符";
                    Size = new Size(pnlDes2.Width + 25, pnlDes2.Height + 110);
                    pnlDes2.Visible = true;
                    pnlDes2.Enabled = canEdit;
                    if (indexInfo != null)
                    {
                        pnlDes2.InitData(indexInfo as StdDescriptor, canEdit);
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
                    data = pnlResourceCode.GetData();
                    break;
                case 1:
                    data = pnlDetChlDes.GetData();
                    break;
                case 2:
                    data = pnlProgramStreamInfo.GetData();
                    break;
                case 3:
                    data = pnlDes2.GetData();
                    break;
            }
            return data;
        }

        private void btnOK_Click(object sender, System.EventArgs e)
        {
            switch (tag)
            {
                case 0:
                    break;
                case 1:
                    if (!pnlDetChlDes.ValidatData()) return;
                    break;
                case 2:
                    //if (!pnlProgramStreamInfo.ValidatData()) return;
                    break;
                case 3:
                    if (!pnlDes2.ValidatData()) return;
                    break;
            }
            DialogResult = DialogResult.OK;
        }

    }
}
