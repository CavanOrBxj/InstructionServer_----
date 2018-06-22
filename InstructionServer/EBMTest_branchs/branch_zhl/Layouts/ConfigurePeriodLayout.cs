using EBMTable;
using System;
using System.Windows.Forms;

namespace EBMTest.Layouts
{
    public partial class ConfigurePeriodLayout : UserControl
    {
        private EBMConfigure.RebackPeriod RebackPeriod;

        public ConfigurePeriodLayout()
        {
            InitializeComponent();
        }

        public void InitData(object config)
        {
            RebackPeriod = config as EBMConfigure.RebackPeriod;
            pnlAddressType.InitAddressType(RebackPeriod.B_Address_type);
            textreback_period.Text = RebackPeriod.reback_period.ToString();
            pnlTerminalAddress.InitData(RebackPeriod.list_Terminal_Address);
        }

        public EBMConfigure.RebackPeriod GetData()
        {
            try
            {
                if (RebackPeriod == null)
                {
                    RebackPeriod = new EBMConfigure.RebackPeriod();
                }
                EBConfigureRebackPeriod config = new EBConfigureRebackPeriod();
                RebackPeriod.Configure = config;
                RebackPeriod.B_Address_type = pnlAddressType.GetAddressType();
                RebackPeriod.reback_period = Convert.ToInt32(textreback_period.Text.Trim());
                RebackPeriod.list_Terminal_Address = pnlTerminalAddress.GetData();
                return RebackPeriod;
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
