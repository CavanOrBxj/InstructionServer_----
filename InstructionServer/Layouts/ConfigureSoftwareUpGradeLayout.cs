using EBMTable;
using System.Windows.Forms;

namespace InstructionServer.Layouts
{
    public partial class ConfigureSoftwareUpGradeLayout : UserControl
    {
        private EBMConfigure.SoftwareUpGrade SoftwareUpGrade;

        public ConfigureSoftwareUpGradeLayout()
        {
            InitializeComponent();
            InitType();

            radioButtonDTMB.CheckedChanged += RadioButtonDTMB_CheckedChanged;
            radioButtonDVBC.CheckedChanged += RadioButtonDVBC_CheckedChanged;
            radioButtonDTMB.Checked = true;
            groupBoxDVBC.Enabled = false;
        }

        private void RadioButtonDVBC_CheckedChanged(object sender, System.EventArgs e)
        {
            groupBoxDVBC.Enabled = radioButtonDVBC.Checked;
        }

        private void RadioButtonDTMB_CheckedChanged(object sender, System.EventArgs e)
        {
            groupBoxDTMB.Enabled = radioButtonDTMB.Checked;
        }

        private void InitType()
        {
            Utils.ComboBoxHelper.InitUpGradeCarrMode(cbBoxB_CarrMode);
            Utils.ComboBoxHelper.InitUpGradeFHMode(cbBoxB_FHMode);
            Utils.ComboBoxHelper.InitUpGradeILMode(cbBoxB_ILMode);
            Utils.ComboBoxHelper.InitUpGradeMode(cbBoxB_Mode);
            //Utils.ComboBoxHelper.InitUpGradeDeviceType(cbBoxI_DeviceType);
            Utils.ComboBoxHelper.InitUpGradeModType(cbBoxDTMBB_ModType, Enums.DeviceOrderType.TDS_OFDM_DTMB);
            Utils.ComboBoxHelper.InitUpGradeRate(cbBoxDTMBI_Rate, Enums.DeviceOrderType.TDS_OFDM_DTMB);
            Utils.ComboBoxHelper.InitUpGradeModType(cbBoxDVBCB_ModType, Enums.DeviceOrderType.DVBC);
            Utils.ComboBoxHelper.InitUpGradeRate(cbBoxDVBCI_Rate, Enums.DeviceOrderType.DVBC);
        }

        public void InitData(object config)
        {
            SoftwareUpGrade = config as EBMConfigure.SoftwareUpGrade;
            cbBoxB_CarrMode.SelectedValue = SoftwareUpGrade.Configure.B_CarrMode;
            cbBoxB_FHMode.SelectedValue = SoftwareUpGrade.Configure.B_FHMode;
            cbBoxB_ILMode.SelectedValue = SoftwareUpGrade.Configure.B_ILMode;
            cbBoxB_Mode.SelectedValue = SoftwareUpGrade.Configure.B_Mode;
            cbBoxDTMBB_ModType.SelectedValue = SoftwareUpGrade.Configure.B_ModType;
            //cbBoxI_DeviceType.SelectedValue = SoftwareUpGrade.Configure.I_DeviceType;
            textI_DeviceType.Text = SoftwareUpGrade.I_DeviceType.ToString();
            textB_Pid.Text = SoftwareUpGrade.Configure.B_Pid.ToString();
            textI_Freq.Text = SoftwareUpGrade.Configure.I_Freq.ToString();
            textS_NewVersion.Text = SoftwareUpGrade.Configure.S_NewVersion;
            textS_OldVersion.Text = SoftwareUpGrade.Configure.S_OldVersion;
            if(SoftwareUpGrade.DeviceOrderType == Enums.DeviceOrderType.DVBC)
            {
                cbBoxDVBCB_ModType.SelectedValue = SoftwareUpGrade.B_ModType;
                cbBoxDVBCI_Rate.SelectedValue = SoftwareUpGrade.I_Rate;
            }
            else
            {
                cbBoxDTMBB_ModType.SelectedValue = SoftwareUpGrade.B_ModType;
                cbBoxDTMBI_Rate.SelectedValue = SoftwareUpGrade.I_Rate;
            }
            pnlAddressType.InitAddressType(SoftwareUpGrade.Configure.B_Address_type);
            pnlTerminalAddress.InitData(SoftwareUpGrade.Configure.list_Terminal_Address);
        }

        public EBMConfigure.SoftwareUpGrade GetData()
        {
            try
            {
                if (SoftwareUpGrade == null)
                {
                    SoftwareUpGrade = new EBMConfigure.SoftwareUpGrade();
                }
                EBConfigureSoftwareUpGrade config = new EBConfigureSoftwareUpGrade();
                config.B_Address_type = pnlAddressType.GetAddressType();
                config.list_Terminal_Address = pnlTerminalAddress.GetData();
                config.B_CarrMode = (byte)cbBoxB_CarrMode.SelectedValue;
                config.B_FHMode = (byte)cbBoxB_FHMode.SelectedValue;
                config.B_ILMode = (byte)cbBoxB_ILMode.SelectedValue;
                config.B_Mode = (byte)cbBoxB_Mode.SelectedValue;
                config.B_ModType = (byte)cbBoxDTMBB_ModType.SelectedValue;
                config.B_Pid = int.Parse(textB_Pid.Text.Trim());
                //config.I_DeviceType = (int)cbBoxI_DeviceType.SelectedValue;
                config.I_DeviceType = int.Parse(textI_DeviceType.Text.Trim());
                config.I_Freq = int.Parse(textI_Freq.Text.Trim());
                config.S_NewVersion = textS_NewVersion.Text.Trim();
                config.S_OldVersion = textS_OldVersion.Text.Trim();
                SoftwareUpGrade.Configure = config;
                SoftwareUpGrade.DeviceOrderType = radioButtonDTMB.Checked ? Enums.DeviceOrderType.TDS_OFDM_DTMB : Enums.DeviceOrderType.DVBC;
                if (SoftwareUpGrade.DeviceOrderType == Enums.DeviceOrderType.DVBC)
                {
                    SoftwareUpGrade.B_ModType = (byte)cbBoxDVBCB_ModType.SelectedValue;
                    SoftwareUpGrade.I_Rate = (int)cbBoxDVBCI_Rate.SelectedValue;
                }
                else
                {
                    SoftwareUpGrade.B_ModType = (byte)cbBoxDTMBB_ModType.SelectedValue;
                    SoftwareUpGrade.I_Rate = (int)cbBoxDTMBI_Rate.SelectedValue;
                }
                return SoftwareUpGrade;
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
            if(!Utils.NumberHelper.IsInt(textB_Pid.Text.Trim()))
            {
                MessageBox.Show("\"" + textB_Pid.Tag + "\"必须为整数，请检查并填写");
                return false;
            }
            if (!Utils.NumberHelper.IsInt(textI_Freq.Text.Trim()))
            {
                MessageBox.Show("\"" + textI_Freq.Tag + "\"必须为整数，请检查并填写");
                return false;
            }
            if (!Utils.NumberHelper.IsInt(textI_DeviceType.Text.Trim()))
            {
                MessageBox.Show("\"" + textI_DeviceType.Tag + "\"必须为整数，请检查并填写");
                return false;
            }
            return true;
        }


    }
}
