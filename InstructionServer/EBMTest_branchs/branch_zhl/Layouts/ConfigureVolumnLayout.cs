using EBMTable;
using System;
using System.Windows.Forms;

namespace EBMTest.Layouts
{
    public partial class ConfigureVolumnLayout : UserControl
    {
        private EBMConfigure.DefaltVolume Volumn;

        public ConfigureVolumnLayout()
        {
            InitializeComponent();
        }

        public void InitData(object config)
        {
            Volumn = config as EBMConfigure.DefaltVolume;
            pnlAddressType.InitAddressType(Volumn.B_Address_type);
            pnlTerminalAddress.InitData(Volumn.list_Terminal_Address);
        }

        public EBMConfigure.DefaltVolume GetData()
        {
            try
            {
                if (Volumn == null)
                {
                    Volumn = new EBMConfigure.DefaltVolume();
                }
                EBConigureDefaltVolume config = new EBConigureDefaltVolume();
                Volumn.Configure = config;
                Volumn.B_Address_type = pnlAddressType.GetAddressType();
                Volumn.Column = Convert.ToInt16(textVolumn.Text.Trim());
                Volumn.list_Terminal_Address = pnlTerminalAddress.GetData();
                return Volumn;
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
