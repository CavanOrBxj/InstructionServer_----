using EBMTable;
using EBMTest.Enums;
using System;
using System.Windows.Forms;

namespace EBMTest
{
    public partial class EBMIndexInfo : Form
    {
        public EBMIndex.EBIndexEx EBIndex { get; private set; }
        public OperateType OperateType { get; private set; }
        private ToolTip tip;

        public EBMIndexInfo(OperateType type) : this(type, null)
        {
        }

        public EBMIndexInfo(OperateType type, EBMIndex.EBIndexEx ebIndex)
        {
            InitializeComponent();
            if (type != OperateType.Add)
            {
                EBIndex = ebIndex;
            }
            OperateType = type;
            tip = new ToolTip();
            tip.UseAnimation = true;
            tip.UseFading = true;
            tip.SetToolTip(btnAddMinute, "开始时间增加指定分钟数");
            tip.SetToolTip(btnGetSysTime, "获取系统时间");
            InitType();
            InitData();
        }

        private void InitType()
        {
            Utils.ComboBoxHelper.InitEBMLevel(cbBoxS_EBM_level);
            Utils.ComboBoxHelper.InitEBMClass(cbBoxS_EBM_class);
        }

        private void InitData()
        {
            if(OperateType != OperateType.Add)
            {
                pnlResourceCode.InitData(EBIndex.List_EBM_resource_code);

                textS_EBM_id.Text = EBIndex.S_EBM_id;
                textS_EBM_original_network_id.Text = EBIndex.S_EBM_original_network_id;
                timePickerS_EBM_start_time.Value = DateTime.Parse(EBIndex.S_EBM_start_time);
                timePickerS_EBM_end_time.Value = DateTime.Parse(EBIndex.S_EBM_end_time);
                textS_EBM_type.Text = EBIndex.S_EBM_type;
                cbBoxS_EBM_class.SelectedValue = EBIndex.S_EBM_class;
                cbBoxS_EBM_level.SelectedValue = EBIndex.S_EBM_level;

                checkBoxBL_details_channel_indicate.Checked = EBIndex.BL_details_channel_indicate;
                checkBoxDes.Checked = EBIndex.DesFlag;

                textS_details_channel_transport_stream_id.Text = EBIndex.S_details_channel_transport_stream_id;
                textS_details_channel_program_number.Text = EBIndex.S_details_channel_program_number;
                textS_details_channel_PCR_PID.Text = EBIndex.S_details_channel_PCR_PID;
                pnlDetChlDes.InitData(EBIndex.DeliverySystemDescriptor);
                pnlProgramStreamInfo.InitData(EBIndex.List_ProgramStreamInfo);
            }
            checkBoxBL_details_channel_indicate_CheckedChanged(checkBoxBL_details_channel_indicate, EventArgs.Empty);
            checkBoxDes_CheckedChanged(checkBoxDes, EventArgs.Empty);
            switch (OperateType)
            {
                case OperateType.Add:
                    Text = "添加索引信息";
                    break;
                case OperateType.Delet:
                    break;
                case OperateType.Update:
                    Text = "更新索引信息";
                    break;
                case OperateType.Info:
                    Text = "索引信息";
                    //foreach (var c in pnlEBMInfo.Controls)
                    //{
                    //    if (c is TextBox)
                    //    {
                    //        (c as TextBox).ReadOnly = true;
                    //    }
                    //    if (c is GroupBox)
                    //    {
                    //        foreach (var gc in (c as GroupBox).Controls)
                    //        {
                    //            if (gc is TextBox)
                    //            {
                    //                (gc as TextBox).ReadOnly = true;
                    //            }
                    //        }
                    //    }
                    //}
                    pnlEBMInfo.Enabled = false;
                    timePickerS_EBM_end_time.Enabled = false;
                    timePickerS_EBM_start_time.Enabled = false;
                    break;
            }
        }

        private void btnOK_Click(object sender, System.EventArgs e)
        {
            switch (OperateType)
            {
                case OperateType.Add:
                    if(!ValidatData() || (pnlDetChlDes.Enabled && !pnlDetChlDes.ValidatData())) return;
                    EBIndex = GetEBIndex();
                    break;
                case OperateType.Delet:
                    break;
                case OperateType.Update:
                    if (!ValidatData() || (pnlDetChlDes.Enabled && !pnlDetChlDes.ValidatData())) return;
                    EBIndex = GetEBIndex();
                    break;
                case OperateType.Info:
                    break;
            }
            DialogResult = DialogResult.OK;
        }

        private EBMIndex.EBIndexEx GetEBIndex()
        {
            try
            {
                EBMIndex.EBIndexEx index = new EBMIndex.EBIndexEx();
                index.SendState = EBIndex == null ? false : EBIndex.SendState;
                index.EBIndex = new EBIndex();
                index.S_EBM_id = textS_EBM_id.Text.Trim();
                index.S_EBM_original_network_id = textS_EBM_original_network_id.Text.Trim();
                index.S_EBM_start_time = timePickerS_EBM_start_time.Value.ToString("yyyy-MM-dd HH:mm:ss");
                index.S_EBM_end_time = timePickerS_EBM_end_time.Value.ToString("yyyy-MM-dd HH:mm:ss");
                index.S_EBM_type = textS_EBM_type.Text.Trim();
                index.S_EBM_class = (string)cbBoxS_EBM_class.SelectedValue;
                index.S_EBM_level = (string)cbBoxS_EBM_level.SelectedValue;

                index.List_EBM_resource_code = pnlResourceCode.GetData();
                index.BL_details_channel_indicate = checkBoxBL_details_channel_indicate.Checked;
                index.DesFlag = checkBoxDes.Checked;
                index.S_details_channel_transport_stream_id = textS_details_channel_transport_stream_id.Text.Trim();
                index.S_details_channel_program_number = textS_details_channel_program_number.Text.Trim();
                index.S_details_channel_PCR_PID = textS_details_channel_PCR_PID.Text.Trim();

                index.DeliverySystemDescriptor = pnlDetChlDes.GetData();
                index.List_ProgramStreamInfo = pnlProgramStreamInfo.GetData();
                index.NickName = EBIndex == null ? string.Empty : EBIndex.NickName;
                return index;
            }
            catch
            {
                return null;
            }
        }

        public bool ValidatData()
        {
            foreach (Control c in pnlEBMInfo.Controls)
            {
                if (c is TextBox && c.Enabled && c.Name != textAddMin.Name)
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

        private void checkBoxBL_details_channel_indicate_CheckedChanged(object sender, EventArgs e)
        {
                groupDes2.Enabled = checkBoxBL_details_channel_indicate.Checked;
                pnlDesInfo.Enabled = checkBoxBL_details_channel_indicate.Checked;
        }

        private void btnGetSysTime_Click(object sender, EventArgs e)
        {
            timePickerS_EBM_start_time.Value = DateTime.Now;
            timePickerS_EBM_end_time.Value = timePickerS_EBM_start_time.Value;
        }

        private void btnAddMinute_Click(object sender, EventArgs e)
        {
            double delay = 0;
            if(!string.IsNullOrWhiteSpace(textAddMin.Text.Trim()))
            {
                delay = Convert.ToInt32(textAddMin.Text.Trim());
            }
            timePickerS_EBM_end_time.Value = timePickerS_EBM_start_time.Value.AddMinutes(delay);
            textAddMin.Text = delay.ToString();
        }

        private void checkBoxDes_CheckedChanged(object sender, EventArgs e)
        {
            groupDes1.Enabled = checkBoxDes.Checked;
        }
    }
}
