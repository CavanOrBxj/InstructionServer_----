using EBMTable;
using System.Windows.Forms;

namespace EBMTest.Layouts
{
    public partial class ConfigureStatusRetbackLayout : UserControl
    {
        private EBMConfigure.StatusRetback StatusRetback;

        public ConfigureStatusRetbackLayout()
        {
            InitializeComponent();
        }

        public void InitData(object config)
        {
            StatusRetback = config as EBMConfigure.StatusRetback;
            pnlParameterTag.InitData(StatusRetback.list_Parameter_tag);
            pnlTerminalAddress.InitData(StatusRetback.list_Terminal_Address);
            pnlAddressType.InitAddressType(StatusRetback.B_Address_type);
        }

        public EBMConfigure.StatusRetback GetData()
        {
            try
            {
                if (StatusRetback == null)
                {
                    StatusRetback = new EBMConfigure.StatusRetback();
                }
                EBConfigureStatusRetback config = new EBConfigureStatusRetback();
                config.list_Terminal_Address = pnlTerminalAddress.GetData();
                config.B_Address_type = pnlAddressType.GetAddressType();
                config.list_Parameter_tag = pnlParameterTag.GetData();
                StatusRetback.Configure = config;
                return StatusRetback;
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
