using System.Windows.Forms;

namespace InstructionServer.Layouts
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
            string data = Utils.ArrayHelper.Bytes2String(DailyProgram.Program.Br_Rds_data);
            textRdsData.Text = data;
            pnlAddressType.InitAddressType(DailyProgram.B_Address_type);
            pnlTerminalAddress.InitData(DailyProgram.Program.list_Terminal_Address);
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
                string rdsData = textRdsData.Text.Trim().Replace('，', ',').Replace(",", " ");
                byte[] arB_byte = Utils.ArrayHelper.String2Bytes(rdsData);
                DailyProgram.Program.Br_Rds_data = arB_byte;
                DailyProgram.B_Address_type = pnlAddressType.GetAddressType();
                DailyProgram.Program.list_Terminal_Address = pnlTerminalAddress.GetData();
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
