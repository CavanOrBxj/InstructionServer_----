using EBMTable;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace EBMTest.Layouts
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
            Utils.ComboBoxHelper.InitRetbackModeType(cbBoxRetback_mode);
        }

        public void InitEbmId(List<string> ebmId)
        {
            cbBoxEBMId.DataSource = ebmId;
        }

        public void InitData(object config)
        {
            ContentMoniterRetback = config as EBMConfigure.ContentMoniterRetback;
            textFileId.Text = ContentMoniterRetback.S_File_id;
            textStart_package_index.Text = ContentMoniterRetback.Start_package_index.ToString();
            pnlTerminalAddress.InitData(ContentMoniterRetback.list_Terminal_Address);
            pnlAddressType.InitAddressType(ContentMoniterRetback.B_Address_type);
        }

        public EBMConfigure.ContentMoniterRetback GetData()
        {
            try
            {
                if (ContentMoniterRetback == null)
                {
                    ContentMoniterRetback = new EBMConfigure.ContentMoniterRetback();
                }
                EBConfigureContentMoniterRetback config = new EBConfigureContentMoniterRetback();
                config.S_EBM_id = (string)cbBoxEBMId.SelectedValue;
                config.B_Address_type = pnlAddressType.GetAddressType();
                config.S_File_id = textFileId.Text.Trim();
                config.Retback_mode = (short)cbBoxRetback_mode.SelectedValue;
                config.list_Terminal_Address = pnlTerminalAddress.GetData();
                config.Start_package_index = Convert.ToInt32(textStart_package_index.Text.Trim());
                ContentMoniterRetback.Configure = config;
                return ContentMoniterRetback;
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
