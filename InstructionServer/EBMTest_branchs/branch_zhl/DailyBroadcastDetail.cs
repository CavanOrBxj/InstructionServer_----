using System.Collections.Generic;
using System.Windows.Forms;

namespace EBMTest
{
    public partial class DailyBroadcastTerAddressInfo : Form
    {
        public DailyBroadcastTerAddressInfo(List<string> address_list, bool canEdit)
        {
            InitializeComponent();
            if (address_list != null)
            {
                pnlTerminalAddress.InitData(address_list, canEdit);
            }
        }

        public List<string> GetData()
        {
            return pnlTerminalAddress.GetData();
        }
    }
}
