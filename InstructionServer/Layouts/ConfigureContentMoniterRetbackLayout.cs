using EBMTable;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace InstructionServer.Layouts
{
    public partial class ConfigureContentMoniterRetbackLayout : UserControl
    {
        private EBMConfigure.ContentMoniterRetback ContentMoniterRetback;

        public ConfigureContentMoniterRetbackLayout()
        {
            InitializeComponent();
            InitType();
        }

        private void InitType()
        {
            Utils.ComboBoxHelper.InitRetbackModeType(cbBoxAudioRetback_mode);
        }

        public void InitData(object config)
        {
            ContentMoniterRetback = config as EBMConfigure.ContentMoniterRetback;
            textFileId.Text = ContentMoniterRetback.S_File_id;
            textStart_package_index.Text = ContentMoniterRetback.Start_package_index.ToString();
            pnlTerminalAddress.InitData(ContentMoniterRetback.Configure.list_Terminal_Address);
            pnlAddressType.InitAddressType(ContentMoniterRetback.B_Address_type);
         
            textreback_port.Text = ContentMoniterRetback.I_Reback_PORT.ToString();
            textreback_serverip.Text = ContentMoniterRetback.S_Reback_serverIP;
        }

        public EBMConfigure.ContentMoniterRetback GetData()
        {
            try
            {
                if (ContentMoniterRetback == null)
                {
                    ContentMoniterRetback = new EBMConfigure.ContentMoniterRetback();
                }
                EBConfigureContentMoniterRetbackGX config = new EBConfigureContentMoniterRetbackGX();

                config.I_Audio_reback_port = Convert.ToInt32(textreback_port.Text);
                config.S_Audio_reback_serverip = textreback_serverip.Text;
                config.B_Address_type = pnlAddressType.GetAddressType();
                config.S_File_id = textFileId.Text.Trim();
                config.B_Audio_reback_mod = (byte)Convert.ToInt32(cbBoxAudioRetback_mode.SelectedValue);
                config.list_Terminal_Address = pnlTerminalAddress.GetData();
                config.Start_package_index = Convert.ToInt32(textStart_package_index.Text.Trim());
                ContentMoniterRetback.Configure = config;
                return ContentMoniterRetback;
            }
            catch(Exception ex)
            {

                Console.Write(ex.ToString());
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
