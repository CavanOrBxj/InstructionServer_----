using EBMTable;
using System;
using System.Windows.Forms;

namespace EBMTest.Layouts
{
    public partial class EBMIndexDes : UserControl
    {
        public EBMIndexDes()
        {
            InitializeComponent();
            Utils.ComboBoxHelper.InitEBMDiscriptorTag2(cbBoxB_discriptor_tag);
        }

        public void InitData(StdDescriptor des, bool canEdit = true)
        {
            Enabled = canEdit;
            try
            {
                cbBoxB_discriptor_tag.SelectedValue = des.B_descriptor_tag;
                string ar = string.Empty;
                for (int i = 0; i < des.Br_descriptor.Length; i++)
                {
                    ar += Convert.ToString(des.Br_descriptor[i], 10) + ",";
                }
                textarB_discriptor.Text = ar;
                if (des != null)
                {
                    checkBoxUseOwnData.Checked = true;
                    checkBoxUseOwnData_CheckedChanged(checkBoxUseOwnData, EventArgs.Empty);
                }
            }
            catch
            {
            }
        }

        public StdDescriptor GetData()
        {
            try
            {
                if (!checkBoxUseOwnData.Checked)
                {
                    return null;
                }
                string descriptor = textarB_discriptor.Text.Trim().Replace('，', ',');
                string[] arB_desdata = descriptor.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                byte[] arB_byte = new byte[arB_desdata.Length];
                for (int i = 0; i < arB_desdata.Length; i++)
                {
                    arB_byte[i] = Convert.ToByte(arB_desdata[i]);
                }
                if(arB_byte.Length==0)
                {
                    arB_byte = new byte[1];
                    arB_byte[0] = 0;
                }
                StdDescriptor Descriptor2 = new StdDescriptor
                {
                    B_descriptor_tag = (byte)cbBoxB_discriptor_tag.SelectedValue,
                    Br_descriptor = arB_byte,
                };
                return Descriptor2;
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
                if (c is TextBox && c.Visible)
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

        private void checkBoxUseOwnData_CheckedChanged(object sender, EventArgs e)
        {
            lblarB_discriptor.Visible = checkBoxUseOwnData.Checked;
            textarB_discriptor.Visible = checkBoxUseOwnData.Checked;
            lblB_discriptor_tag.Visible = checkBoxUseOwnData.Checked;
            cbBoxB_discriptor_tag.Visible = checkBoxUseOwnData.Checked;
        }
    }
}
