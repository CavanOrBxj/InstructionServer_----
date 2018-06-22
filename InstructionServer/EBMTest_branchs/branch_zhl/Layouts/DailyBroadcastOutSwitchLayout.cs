using EBMTable;
using System.Data;
using System.Windows.Forms;

namespace EBMTest.Layouts
{
    public partial class DailyBroadcastOutSwitchLayout : UserControl
    {
        private DailyBroadcast.OutSwitch DailyProgram { get; set; }

        public DailyBroadcastOutSwitchLayout()
        {
            InitializeComponent();
            InitOutSwitchType();
        }

        private void InitOutSwitchType()
        {
            Utils.ComboBoxHelper.InitOutSwitchType(cbBoxB_Switch_status);
        }

        public void InitData(DailyBroadcast.DailyProgram program)
        {
            DailyProgram = program as DailyBroadcast.OutSwitch;
            cbBoxB_Switch_status.SelectedValue = DailyProgram.B_Switch_status;
            pnlAddressType.InitAddressType(DailyProgram.B_Address_type);
            pnlTerminalAddress.InitData(DailyProgram.list_Terminal_Address);
        }

        public DailyBroadcast.OutSwitch GetData()
        {
            try
            {
                if (DailyProgram == null)
                {
                    DailyProgram = new DailyBroadcast.OutSwitch();
                }
                DailyProgram.Program = new DailyCmdOutSwitch();
                DailyProgram.B_Switch_status = (byte)cbBoxB_Switch_status.SelectedValue;
                DailyProgram.B_Address_type = pnlAddressType.GetAddressType();
                DailyProgram.list_Terminal_Address = pnlTerminalAddress.GetData();
                return DailyProgram;
            }
            catch
            {
                return null;
            }
        }

        public bool ValidatData()
        {
            foreach (Control c in pnlBroadcastInfo.Controls)
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
