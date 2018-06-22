using EBMTest.Enums;
using System;
using System.Windows.Forms;

namespace EBMTest
{
    public partial class EBMCertAuthInfo : Form
    {
        private OperateType type;
        private bool isCert;
        public EBMCertAuth.Cert CertData { get; private set; }

        public EBMCertAuthInfo(OperateType type, bool isCert) : this(type, isCert, null)
        {
        }

        public EBMCertAuthInfo(OperateType type, bool isCert, EBMCertAuth.Cert cert)
        {
            InitializeComponent();
            this.type = type;
            this.CertData = cert ?? new EBMCertAuth.Cert();
            this.isCert = isCert;
            switch (type)
            {
                case OperateType.Add:
                    Text = !isCert ? "添加应急广播授权列表数据" : "添加应急广播证书数据";
                    break;
                case OperateType.Info:
                    Text = !isCert ? "查看应急广播授权列表数据" : "查看应急广播证书数据";
                    break;
                case OperateType.Update:
                    Text = !isCert ? "更新应急广播授权列表数据" : "更新应急广播证书数据";
                    break;
            }
            cbBoxType.SelectedIndex = 0;
            if (type != OperateType.Add)
            {
                InitData();
            }
        }

        private void InitData()
        {
            cbBoxType.SelectedIndex = CertData.Tag;
            if (CertData.Tag == 0)
            {
                textData.Text = CertData.Cert_data;
            }
            else
            {
                richText.Text = CertData.Cert_data;
            }
        }

        private void cbBoxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbBoxType.SelectedIndex == 0)
            {
                textData.Visible = true;
                btnOpenFile.Visible = true;
                richText.Visible = false;
                Height = 190;
            }
            else if(cbBoxType.SelectedIndex == 1)
            {
                textData.Visible = false;
                btnOpenFile.Visible = false;
                richText.Visible = true;
                Height = 365;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            CertData.Tag = cbBoxType.SelectedIndex;
            if (CertData.Tag == 0)
            {
                if (string.IsNullOrWhiteSpace(textData.Text))
                {
                    CertData = null;
                    return;
                }
                CertData.Cert_data = textData.Text.Trim();
            }
            else if (CertData.Tag == 1)
            {
                if (string.IsNullOrWhiteSpace(richText.Text))
                {
                    CertData = null;
                    return;
                }
                CertData.Cert_data = richText.Text.Trim();
            }
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                textData.Text = openFileDialog.FileName;
            }
        }

    }
}
