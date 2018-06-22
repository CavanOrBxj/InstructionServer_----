using EBMTable;
using System.Windows.Forms;

namespace EBMTest.Layouts
{
    public partial class ConfigureSetLayout : UserControl
    {
        private EBMConfigure.SetAddress Set;

        public ConfigureSetLayout()
        {
            InitializeComponent();
        }

        public void InitData(object config)
        {
            Set = config as EBMConfigure.SetAddress;
            textLogicAddress.Text = Set.S_Logic_address.ToString();
            textTerminalAddress.Text = Set.S_Phisical_address.ToString();
        }

        public EBMConfigure.SetAddress GetData()
        {
            try
            {
                if (Set == null)
                {
                    Set = new EBMConfigure.SetAddress();
                }
                EBConfigureSetAddress config = new EBConfigureSetAddress();
                Set.Configure = config;
                Set.S_Logic_address = textLogicAddress.Text.Trim();
                Set.S_Phisical_address = textTerminalAddress.Text.Trim();
                return Set;
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
