using EBMTable;
using System;
using System.Data;
using System.Windows.Forms;

namespace EBMTest.Layouts
{
    public partial class ConfigureRebackLayout : UserControl
    {
        private EBMConfigure.Reback Reback;

        public ConfigureRebackLayout()
        {
            InitializeComponent();
            InitType();
        }

        private void InitType()
        {
            Utils.ComboBoxHelper.InitConfigRebackType(cbBoxB_reback_type);
        }

        public void InitData(object config)
        {
            Reback = config as EBMConfigure.Reback;
            pnlAddressType.InitAddressType(Reback.B_Address_type);
            cbBoxB_reback_type.SelectedValue = Reback.B_reback_type;
            textS_reback_address.Text = Reback.S_reback_address;
            pnlTerminalAddress.InitData(Reback.list_Terminal_Address);
        }

        public EBMConfigure.Reback GetData()
        {
            try
            {
                if (Reback == null)
                {
                    Reback = new EBMConfigure.Reback();
                }
                EBConfigureReback config = new EBConfigureReback();
                Reback.Configure = config;
                Reback.B_Address_type = pnlAddressType.GetAddressType();
                Reback.B_reback_type = (byte)cbBoxB_reback_type.SelectedValue;
                Reback.S_reback_address = textS_reback_address.Text.Trim();
                Reback.list_Terminal_Address = pnlTerminalAddress.GetData();
                return Reback;
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
