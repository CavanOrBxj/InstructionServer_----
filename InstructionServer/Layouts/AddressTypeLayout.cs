using System.Windows.Forms;

namespace InstructionServer.Layouts
{
    public partial class AddressTypeLayout : UserControl
    {
        public AddressTypeLayout()
        {
            InitializeComponent();
            InitAddressType();
        }

        private void InitAddressType()
        {
            Utils.ComboBoxHelper.InitAddressType(cbBoxB_Address_type);
        }

        public byte GetAddressType()
        {
            return (byte)cbBoxB_Address_type.SelectedValue;
        }

        public void InitAddressType(byte type)
        {
            cbBoxB_Address_type.SelectedValue = type;
        }

    }
}
