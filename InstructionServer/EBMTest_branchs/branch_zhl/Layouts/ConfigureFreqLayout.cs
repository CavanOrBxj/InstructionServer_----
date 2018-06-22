using EBMTable;
using System;
using System.Windows.Forms;

namespace EBMTest.Layouts
{
    public partial class ConfigureFreqLayout : UserControl
    {
        private EBMConfigure.MainFrequency Freq;

        public ConfigureFreqLayout()
        {
            InitializeComponent();
            Utils.ComboBoxHelper.InitFreqQAMType(cbBoxQAM);
        }

        public void InitData(object config)
        {
            Freq = config as EBMConfigure.MainFrequency;
            pnlAddressType.InitAddressType(Freq.B_Address_type);
            textFreq.Text = Convert.ToString(Freq.Freq, 10);
            cbBoxQAM.SelectedValue = Freq.QAM;
            textSymbolRate.Text = Freq.SymbolRate.ToString();
            pnlTerminalAddress.InitData(Freq.list_Terminal_Address);
        }

        public EBMConfigure.MainFrequency GetData()
        {
            try
            {
                if (Freq == null)
                {
                    Freq = new EBMConfigure.MainFrequency();
                }
                EBConfigureMainFrequency config = new EBConfigureMainFrequency();
                Freq.Configure = config;
                Freq.B_Address_type = pnlAddressType.GetAddressType();
                Freq.Freq = Convert.ToInt32(textFreq.Text.Trim());
                Freq.QAM = (short)cbBoxQAM.SelectedValue;
                Freq.SymbolRate = Convert.ToInt32(textSymbolRate.Text.Trim());
                Freq.list_Terminal_Address = pnlTerminalAddress.GetData();
                return Freq;
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
