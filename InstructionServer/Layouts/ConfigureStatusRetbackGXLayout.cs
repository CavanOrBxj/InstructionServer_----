using EBMTable;
using System.Windows.Forms;

namespace InstructionServer.Layouts
{
    public partial class ConfigureStatusRetbackGXLayout : UserControl
    {
        private EBMConfigure.StatusRetbackGX StatusRetbackGX;

        public ConfigureStatusRetbackGXLayout()
        {
            InitializeComponent();
            Utils.ComboBoxHelper.InitGXTerminalRetbackType(cbBoxB_Terminal_Retback_Type);
        }

        public void InitData(object config)
        {
            StatusRetbackGX = config as EBMConfigure.StatusRetbackGX;
            cbBoxB_Terminal_Retback_Type.SelectedValue = StatusRetbackGX.B_Terminal_Retback_Type;
            textI_retback_period.Text = StatusRetbackGX.I_retback_period.ToString();
            pnlAddressType.InitAddressType(StatusRetbackGX.B_Address_type);
            pnlTerminalAddress.InitData(StatusRetbackGX.Configure.list_Terminal_Address);
        }

        public EBMConfigure.StatusRetbackGX GetData()
        {
            try
            {
                if (StatusRetbackGX == null)
                {
                    StatusRetbackGX = new EBMConfigure.StatusRetbackGX();
                }
                EBConfigureStatusRetbackGX config = new EBConfigureStatusRetbackGX();
                config.B_Terminal_Retback_Type = (byte)cbBoxB_Terminal_Retback_Type.SelectedValue;
                config.I_retback_period = int.Parse(textI_retback_period.Text.Trim());
                config.B_Address_type = pnlAddressType.GetAddressType();
                config.list_Terminal_Address = pnlTerminalAddress.GetData();
                StatusRetbackGX.Configure = config;
                return StatusRetbackGX;
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
            if(!Utils.NumberHelper.IsInt(textI_retback_period.Text.Trim()))
            {
                MessageBox.Show("\"" + textI_retback_period.Tag + "\"必须为整数，请检查并修改");
                return false;
            }
            return true;
        }
    }
}
