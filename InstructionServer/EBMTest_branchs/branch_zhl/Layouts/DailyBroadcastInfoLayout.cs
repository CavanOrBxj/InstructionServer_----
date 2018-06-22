using EBMTable;
using System;
using System.Windows.Forms;

namespace EBMTest.Layouts
{
    public partial class DailyBroadcastInfoLayout : UserControl
    {
        private DailyBroadcast.ChangeProgram DailyProgram { get; set; }

        public DailyBroadcastInfoLayout()
        {
            InitializeComponent();
        }

        public void InitData(DailyBroadcast.DailyProgram program)
        {
            DailyProgram = program as DailyBroadcast.ChangeProgram;
            textNetID.Text = DailyProgram.NetID.ToString();
            textTSID.Text = DailyProgram.TSID.ToString();
            textServiceID.Text = DailyProgram.ServiceID.ToString();
            textPCR_PID.Text = DailyProgram.PCR_PID.ToString();
            textPriority.Text = DailyProgram.Priority.ToString();
            textProgram_PID.Text = DailyProgram.Program_PID.ToString();
            textVolume.Text = DailyProgram.Volume.ToString();
            timeEndTime.Text = DailyProgram.EndTime;
            pnlAddressType.InitAddressType(DailyProgram.B_Address_type);
            pnlTerminalAddress.InitData(DailyProgram.list_Terminal_Address);
        }

        public DailyBroadcast.ChangeProgram GetData()
        {
            try
            {
                if (DailyProgram == null)
                {
                    DailyProgram = new DailyBroadcast.ChangeProgram();
                }
                DailyCmdChangeProgram pro = new DailyCmdChangeProgram();
                DailyProgram.Program = pro;
                DailyProgram.NetID = Convert.ToInt16(textNetID.Text.Trim());
                DailyProgram.TSID = Convert.ToInt16(textTSID.Text.Trim());
                DailyProgram.ServiceID = Convert.ToInt16(textServiceID.Text.Trim());
                DailyProgram.PCR_PID = Convert.ToInt32(textPCR_PID.Text.Trim());
                DailyProgram.Priority = Convert.ToInt16(textPriority.Text.Trim());
                DailyProgram.Program_PID = Convert.ToInt16(textProgram_PID.Text.Trim());
                DailyProgram.Volume = Convert.ToInt16(textVolume.Text.Trim());
                DailyProgram.EndTime = timeEndTime.Text.Trim();
                DailyProgram.B_Address_type = pnlAddressType.GetAddressType();
                DailyProgram.list_Terminal_Address = pnlTerminalAddress.GetData();
                if(string.IsNullOrWhiteSpace(DailyProgram.BroadcastStatus))
                {
                    DailyProgram.BroadcastStatus = "已停止";
                }
                return DailyProgram;
            }
            catch
            {
                return null;
            }
        }

        public bool ValidatData()
        {
            foreach(Control c in pnlBroadcastInfo.Controls)
            {
                if(c is TextBox)
                {
                    if(string.IsNullOrWhiteSpace(c.Text))
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
