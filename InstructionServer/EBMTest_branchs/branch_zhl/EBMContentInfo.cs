using EBMTable;
using EBMTest.Enums;
using System;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Linq;

namespace EBMTest
{
    public partial class EBMContentInfo : Form
    {
        private OperateType type;
        public EBMContent.EBContent Content { get; private set; }

        public EBMContentInfo(OperateType type) : this(type, null)
        { }

        public EBMContentInfo(OperateType type, EBMContent.EBContent content)
        {
            InitializeComponent();
            this.type = type;
            this.Content = content;
            InitCodeCharacter();
            if(type!= OperateType.Add)
            {
                InitData();
            }
            switch (type)
            {
                case OperateType.Add:
                    Text = "添加应急广播内容";
                    break;
                case OperateType.Info:
                    Text = "查看应急广播内容";
                    break;
                case OperateType.Update:
                    Text = "更新应急广播内容";
                    break;
            }
        }

        private void InitCodeCharacter()
        {
            Utils.ComboBoxHelper.InitCodeCharacter(cbBoxB_code_character_set);
        }

        private void InitData()
        {
            textB_message_text.Text = Content.MessageText.ToString();
            textS_language_code.Text = Content.S_language_code.ToString();
            pnlAuxiliaryData.InitData(Content.list_auxiliary_data);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
                switch (type)
                {
                    case OperateType.Add:
                        if (!ValidatData()) return;
                        Content = GetEBContent();
                        break;
                    case OperateType.Delet:
                        break;
                    case OperateType.Update:
                        Content = GetEBContent();
                        break;
                    case OperateType.Info:
                        break;
                }
            DialogResult = DialogResult.OK;
        }

        private EBMContent.EBContent GetEBContent()
        {
            try
            {
                if (Content == null)
                {
                    Content = new EBMContent.EBContent();
                }
                MultilangualContent multiContent = new MultilangualContent();
                multiContent.B_code_character_set = (byte)cbBoxB_code_character_set.SelectedValue;
                multiContent.S_language_code = textS_language_code.Text.Trim();
                multiContent.B_message_text = Encoding.GetEncoding("GB2312").GetBytes(textB_message_text.Text.Trim());
                multiContent.list_auxiliary_data = pnlAuxiliaryData.GetAuxiliaryData();
                Content.MultilangualContent = multiContent;
                Content.MessageText = textB_message_text.Text.Trim();
                return Content;
            }
            catch
            {
                return null;
            }
        }

        public bool ValidatData()
        {
            foreach (Control c in Controls)
            {
                if (c is TextBox)
                {
                    if (string.IsNullOrWhiteSpace(c.Text))
                    {
                        MessageBox.Show("\"" + c.Tag + "\"不允许为空，请检查并填写");
                        return false;
                    }
                }
            }
            return true;
        }

    }
}
