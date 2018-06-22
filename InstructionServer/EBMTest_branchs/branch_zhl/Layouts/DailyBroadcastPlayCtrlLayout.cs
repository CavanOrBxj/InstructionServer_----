using EBMTable;
using System;
using System.Windows.Forms;

namespace EBMTest.Layouts
{
    public partial class DailyBroadcastPlayCtrlLayout : UserControl
    {
        private DailyBroadcast.PlayCtrl DailyProgram { get; set; }

        public DailyBroadcastPlayCtrlLayout()
        {
            InitializeComponent();
        }

        public void InitData(DailyBroadcast.DailyProgram program)
        {
            DailyProgram = program as DailyBroadcast.PlayCtrl;
            textVolume.Text = DailyProgram.Volume.ToString();
            pnlAddressType.InitAddressType(DailyProgram.B_Address_type);
            pnlTerminalAddress.InitData(DailyProgram.list_Terminal_Address);
        }

        public DailyBroadcast.PlayCtrl GetData()
        {
            try
            {
                if (DailyProgram == null)
                {
                    DailyProgram = new DailyBroadcast.PlayCtrl();
                }
                DailyCmdPlayCtrl pro = new DailyCmdPlayCtrl();
                DailyProgram.Program = pro;
                DailyProgram.Volume = Convert.ToInt16(textVolume.Text.Trim());
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
