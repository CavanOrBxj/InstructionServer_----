using EBMTable;
using System;
using System.Windows.Forms;

namespace EBMTest.Layouts
{
    public partial class ConfigureWorkLayout : UserControl
    {
        private EBMConfigure.WorkMode Work;

        public ConfigureWorkLayout()
        {
            InitializeComponent();
            Utils.ComboBoxHelper.InitConfigWorkMode(cbBoxB_Terminal_wordmode);
        }

        public void InitData(object config)
        {
            Work = config as EBMConfigure.WorkMode;
            pnlAddressType.InitAddressType(Work.B_Address_type);
            cbBoxB_Terminal_wordmode.SelectedValue = Work.B_Terminal_wordmode;
            pnlTerminalAddress.InitData(Work.list_Terminal_Address);
        }

        public EBMConfigure.WorkMode GetData()
        {
            try
            {
                if (Work == null)
                {
                    Work = new EBMConfigure.WorkMode();
                }
                EBConfigureWorkMode config = new EBConfigureWorkMode();
                Work.Configure = config;
                Work.B_Address_type = pnlAddressType.GetAddressType();
                Work.B_Terminal_wordmode = (byte)cbBoxB_Terminal_wordmode.SelectedValue;
                Work.list_Terminal_Address = pnlTerminalAddress.GetData();
                return Work;
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
