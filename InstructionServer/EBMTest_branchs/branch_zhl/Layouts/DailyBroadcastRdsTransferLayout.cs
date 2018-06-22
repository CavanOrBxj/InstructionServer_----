using System;
using System.Text;
using System.Windows.Forms;

namespace EBMTest.Layouts
{
    public partial class DailyBroadcastRdsTransferLayout : UserControl
    {
        private DailyBroadcast.RdsTransfer DailyProgram { get; set; }

        public DailyBroadcastRdsTransferLayout()
        {
            InitializeComponent();
            Size = new System.Drawing.Size(630, 375);
            Utils.ComboBoxHelper.InitRdsTransferDailyType(cbBoxB_Rds_terminal_type);
        }

        public void InitData(DailyBroadcast.DailyProgram program)
        {
            DailyProgram = program as DailyBroadcast.RdsTransfer;
            cbBoxB_Rds_terminal_type.SelectedValue = DailyProgram.B_Rds_terminal_type;
            StringBuilder data = new StringBuilder();
            for (int i = 0; i < DailyProgram.Br_Rds_data.Length; i++)
            {
                data.Append(Convert.ToString(DailyProgram.Br_Rds_data[i], 10) + ",");
            }
            textRdsData.Text = data.ToString();
            pnlAddressType.InitAddressType(DailyProgram.B_Address_type);
            pnlTerminalAddress.InitData(DailyProgram.list_Terminal_Address);
        }

        public DailyBroadcast.RdsTransfer GetData()
        {
            try
            {
                if (DailyProgram == null)
                {
                    DailyProgram = new DailyBroadcast.RdsTransfer();
                }
                DailyProgram.Program = new EBMTable.DailyCmdRdsTransfer();
                DailyProgram.B_Rds_terminal_type = (byte)cbBoxB_Rds_terminal_type.SelectedValue;
                string rdsData = textRdsData.Text.Trim().Replace('，', ',');
                string[] data = rdsData.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                byte[] arB_byte = new byte[data.Length];
                for (int i = 0; i < data.Length; i++)
                {
                    arB_byte[i] = Convert.ToByte(data[i]);
                }
                if (arB_byte.Length == 0)
                {
                    arB_byte = new byte[1];
                    arB_byte[0] = 0;
                }
                DailyProgram.Br_Rds_data = arB_byte;
                DailyProgram.RdsDataText = rdsData;
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
                if (c is TextBox && c != textRdsData)
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
