using EBMTable;
using System.Windows.Forms;

namespace InstructionServer.Layouts
{
    public partial class ConfigureRdsConfigLayout : UserControl
    {
        private EBMConfigure.RdsConfig RdsConfig;

        public ConfigureRdsConfigLayout()
        {
            InitializeComponent();
            Utils.ComboBoxHelper.InitConfigRdsTerminalType(cbBoxB_Rds_terminal_type);
        }

        public void InitData(object config)
        {
            RdsConfig = config as EBMConfigure.RdsConfig;
            cbBoxB_Rds_terminal_type.SelectedValue = RdsConfig.B_Rds_terminal_type;
            string data = Utils.ArrayHelper.Bytes2String(RdsConfig.Configure.Br_Rds_data);
            textRdsData.Text = data;
            pnlAddressType.InitAddressType(RdsConfig.B_Address_type);
            pnlTerminalAddress.InitData(RdsConfig.Configure.list_Terminal_Address);
        }

        public EBMConfigure.RdsConfig GetData()
        {
            try
            {
                if (RdsConfig == null)
                {
                    RdsConfig = new EBMConfigure.RdsConfig();
                }
                EBConfigureRdsConfig config = new EBConfigureRdsConfig();
                config.Br_Rds_data = Utils.ArrayHelper.String2Bytes(textRdsData.Text.Trim());
                config.B_Rds_terminal_type = (byte)cbBoxB_Rds_terminal_type.SelectedValue;
                config.B_Address_type = pnlAddressType.GetAddressType();
                config.list_Terminal_Address = pnlTerminalAddress.GetData();
                RdsConfig.Configure = config;
                return RdsConfig;
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
                if (c is TextBox && c != textRdsData)
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
