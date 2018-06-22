using EBMTable;
using System;
using System.Windows.Forms;

namespace EBMTest.Layouts
{
    public partial class EBMIndexDetChlDes : UserControl
    {

        public EBMIndexDetChlDes()
        {
            InitializeComponent();
            InitTDSDType();
        }

        private void InitTDSDType()
        {
            Utils.ComboBoxHelper.InitEBMDiscriptorTag(cbBoxB_descriptor_tag);
            Utils.ComboBoxHelper.InitEBMTDSDBModulation(cbBoxB_Modulation);
            Utils.ComboBoxHelper.InitEBMTDSDFEC(cbBoxFEC);
            Utils.ComboBoxHelper.InitEBMTDSDFrameHeaderMode(cbBoxB_Frame_header_mode);
            Utils.ComboBoxHelper.InitEBMTDSDInterleaveingMode(cbBoxB_Interleaveing_mode);
            Utils.ComboBoxHelper.InitEBMTDSDNumberOfSubcarrier(cbBoxB_Number_of_subcarrier);
            Utils.ComboBoxHelper.InitEBMTDSDOtherFrequencyFlag(cbBoxL_Other_frequency_flag);
            Utils.ComboBoxHelper.InitEBMTDSDSfnMfnFlag(cbBoxL_Sfn_mfn_flag);
            Utils.ComboBoxHelper.InitEBMCDSDModulation(cbBoxI_Modulation);
            Utils.ComboBoxHelper.InitEBMCDSDFECInner(cbBoxInner);
            Utils.ComboBoxHelper.InitEBMCDSDFECOuter(cbBoxOuter);
        }

        public void InitData(object des, bool canEdit = true)
        {
            Enabled = canEdit;
            if(des is Cable_delivery_system_descriptor)//有线传送系统描述符
            {
                Cable_delivery_system_descriptor cdsd = des as Cable_delivery_system_descriptor;
                cbBoxInner.SelectedValue = cdsd.B_FEC_inner;
                cbBoxOuter.SelectedValue = cdsd.B_FEC_outer;
                cbBoxI_Modulation.SelectedValue = cdsd.B_Modulation;
                textI_frequency.Text = cdsd.D_frequency.ToString();
                textI_Symbol_rate.Text = cdsd.D_Symbol_rate.ToString();
                cbBoxB_descriptor_tag.SelectedValue = Cable_delivery_system_descriptor.B_descriptor_tag;
                //cbBoxB_descriptor_tag.SelectedValue = 68;
            }
            else if(des is Terristrial_delivery_system_descriptor)//地面传送系统描述符
            {
                Terristrial_delivery_system_descriptor tdsd = des as Terristrial_delivery_system_descriptor;
                cbBoxFEC.SelectedValue = tdsd.B_FEC;
                cbBoxB_Frame_header_mode.SelectedValue = tdsd.B_Frame_header_mode;
                cbBoxB_Interleaveing_mode.SelectedValue = tdsd.B_Interleaveing_mode;
                cbBoxB_Modulation.SelectedValue = tdsd.B_Modulation;
                cbBoxB_Number_of_subcarrier.SelectedValue = tdsd.B_Number_of_subcarrier;
                textD_Centre_frequency.Text = tdsd.D_Centre_frequency.ToString();
                cbBoxL_Other_frequency_flag.SelectedValue = tdsd.L_Other_frequency_flag;
                cbBoxL_Sfn_mfn_flag.SelectedValue = tdsd.L_Sfn_mfn_flag;
                cbBoxB_descriptor_tag.SelectedValue = Terristrial_delivery_system_descriptor.B_descriptor_tag;
                //cbBoxB_descriptor_tag.SelectedValue = 90;
            }
        }

        public object GetData()
        {
            try
            {
                switch((byte)cbBoxB_descriptor_tag.SelectedValue)
                {
                    case 68://有线传送系统描述符
                        Cable_delivery_system_descriptor cdsd = new Cable_delivery_system_descriptor();
                        cdsd.B_FEC_inner = (byte)cbBoxInner.SelectedValue;
                        cdsd.B_FEC_outer = (byte)cbBoxOuter.SelectedValue;
                        cdsd.B_Modulation = (byte)cbBoxI_Modulation.SelectedValue;
                        cdsd.D_frequency = Convert.ToDouble(textI_frequency.Text.Trim());
                        cdsd.D_Symbol_rate = Convert.ToDouble(textI_Symbol_rate.Text.Trim());
                        return cdsd;
                    case 90://地面传送系统描述符
                        Terristrial_delivery_system_descriptor tdsd = new Terristrial_delivery_system_descriptor();
                        tdsd.B_FEC = (byte)cbBoxFEC.SelectedValue;
                        tdsd.B_Frame_header_mode = (byte)cbBoxB_Frame_header_mode.SelectedValue;
                        tdsd.B_Interleaveing_mode = (byte)cbBoxB_Interleaveing_mode.SelectedValue;
                        tdsd.B_Modulation = (byte)cbBoxB_Modulation.SelectedValue;
                        tdsd.B_Number_of_subcarrier = (byte)cbBoxB_Number_of_subcarrier.SelectedValue;
                        tdsd.D_Centre_frequency = Convert.ToDouble(textD_Centre_frequency.Text.Trim());
                        tdsd.L_Other_frequency_flag = (bool)cbBoxL_Other_frequency_flag.SelectedValue;
                        tdsd.L_Sfn_mfn_flag = (bool)cbBoxL_Sfn_mfn_flag.SelectedValue;
                        return tdsd;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public bool ValidatData()
        {
            switch ((byte)cbBoxB_descriptor_tag.SelectedValue)
            {
                case 68://有线传送系统描述符
                    foreach (Control c in tabPageCDSD.Controls)
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
                    break;
                case 90://地面传送系统描述符
                    foreach (Control c in tabPageTDSD.Controls)
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
                    break;
            }
            return true;
        }

        private void cbBoxB_descriptor_tag_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cbBoxB_descriptor_tag.SelectedValue == null) return;
            switch((byte)cbBoxB_descriptor_tag.SelectedValue)
            {
                case 68://有线传送系统描述符
                    tabDes.SelectedTab = tabPageCDSD;
                    break;
                case 90://地面传送系统描述符
                    tabDes.SelectedTab = tabPageTDSD;
                    break;
            }
        }

    }
}
