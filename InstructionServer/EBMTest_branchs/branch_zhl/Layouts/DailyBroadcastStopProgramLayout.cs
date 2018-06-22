using EBMTable;
using System;
using System.Windows.Forms;

namespace EBMTest.Layouts
{
    public partial class DailyBroadcastStopProgramLayout : UserControl
    {
        private DailyBroadcast.StopPorgram DailyProgram { get; set; }

        public DailyBroadcastStopProgramLayout()
        {
            InitializeComponent();
        }

        public void InitData(DailyBroadcast.DailyProgram program)
        {
            DailyProgram = program as DailyBroadcast.StopPorgram;
            textNetID.Text = DailyProgram.NetID.ToString();
            textTSID.Text = DailyProgram.TSID.ToString();
            textServiceID.Text = DailyProgram.ServiceID.ToString();
            textPCR_PID.Text = DailyProgram.PCR_PID.ToString();
            textProgram_PID.Text = DailyProgram.Program_PID.ToString();
            pnlAddressType.InitAddressType(DailyProgram.B_Address_type);
            pnlTerminalAddress.InitData(DailyProgram.list_Terminal_Address);
        }

        public DailyBroadcast.StopPorgram GetData()
        {
            try
            {
                if (DailyProgram == null)
                {
                    DailyProgram = new DailyBroadcast.StopPorgram();
                }
                DailyCmdProgramStop pro = new DailyCmdProgramStop();
                DailyProgram.Program = pro;
                DailyProgram.NetID = Convert.ToInt16(textNetID.Text.Trim());
                DailyProgram.TSID = Convert.ToInt16(textTSID.Text.Trim());
                DailyProgram.ServiceID = Convert.ToInt16(textServiceID.Text.Trim());
                DailyProgram.PCR_PID = Convert.ToInt16(textPCR_PID.Text.Trim());
                DailyProgram.Program_PID = Convert.ToInt16(textProgram_PID.Text.Trim());
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
