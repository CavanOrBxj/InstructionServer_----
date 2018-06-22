using EBMTable;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace EBMTest.Layouts
{
    public partial class ConfigureContentRealMoniterLayout : UserControl
    {
        private EBMConfigure.ContentRealMoniter ContentRealMoniter;

        public ConfigureContentRealMoniterLayout()
        {
            InitializeComponent();
            InitType();
        }

        private void InitType()
        {
            Utils.ComboBoxHelper.InitRealRetbackModeType(cbBoxRetback_mode);
        }

        public void InitEbmId(List<string> ebmId)
        {
            cbBoxEBMId.DataSource = ebmId;
        }

        public void InitData(object config)
        {
            ContentRealMoniter = config as EBMConfigure.ContentRealMoniter;
            txt_S_Server_addr.Text = ContentRealMoniter.S_Server_addr;
            txt_Moniter_time_duration.Text = ContentRealMoniter.Moniter_time_duration.ToString();
            pnlTerminalAddress.InitData(ContentRealMoniter.list_Terminal_Address);
            pnlAddressType.InitAddressType(ContentRealMoniter.B_Address_type);
        }

        public EBMConfigure.ContentRealMoniter GetData()
        {
            try
            {
                if (ContentRealMoniter == null)
                {
                    ContentRealMoniter = new EBMConfigure.ContentRealMoniter();
                }
                EBConfigureContentRealMoniter config = new EBConfigureContentRealMoniter();
                config.S_EBM_id = (string)cbBoxEBMId.SelectedValue;
                config.B_Address_type = pnlAddressType.GetAddressType();
                config.S_Server_addr = txt_S_Server_addr.Text;
                config.Retback_mode = (short)cbBoxRetback_mode.SelectedValue;
                config.list_Terminal_Address = pnlTerminalAddress.GetData();
                config.Moniter_time_duration = Convert.ToInt32(txt_Moniter_time_duration.Text.Trim());
                ContentRealMoniter.Configure = config;
                return ContentRealMoniter;
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
            return true;
        }
    }
}
