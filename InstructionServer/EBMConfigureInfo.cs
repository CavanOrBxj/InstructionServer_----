using InstructionServer.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace InstructionServer
{
    public partial class EBMConfigureInfo : Form
    {
        private byte tag; //指令类型
        private OperateType type;
        public EBMConfigure.Configure Configure { get; private set; }

        public EBMConfigureInfo(OperateType type, byte tag, List<string> ebmId = null) : this(type, tag, null, ebmId)
        { }

        public EBMConfigureInfo(OperateType type, byte tag) : this(type, tag, null)
        { }

        public EBMConfigureInfo(OperateType type, byte tag, EBMConfigure.Configure config, List<string> ebmId = null)
        {
            InitializeComponent();
            this.tag = tag;
            this.type = type;
            this.Configure = config;
            InitPanelLayout(ebmId);
        }

        private void InitPanelLayout(List<string> ebmId = null)
        {
            switch (type)
            {
                case OperateType.Add:
                    Text = "添加应急广播配置";
                    break;
                case OperateType.Info:
                    Text = "查看应急广播配置";
                    break;
                case OperateType.Update:
                    Text = "更新应急广播配置";
                    break;
            }
            switch (tag)
            {
                case Utils.ComboBoxHelper.ConfigureTimeServiceTag:
                    Size = new Size(pnlTime.Width + 18, pnlTime.Height + 90);
                    pnlTime.Visible = true;
                    if (type != OperateType.Add && Configure != null)
                    {
                        pnlTime.InitData(Configure);
                    }
                    break;
                case Utils.ComboBoxHelper.ConfigureSetAddressTag:
                    Size = new Size(pnlSet.Width + 18, pnlSet.Height + 90);
                    pnlSet.Visible = true;
                    if (type != OperateType.Add && Configure != null)
                    {
                        pnlSet.InitData(Configure);
                    }
                    break;
                case Utils.ComboBoxHelper.ConfigureWorkModeTag:
                    Size = new Size(pnlWork.Width + 18, pnlWork.Height + 90);
                    pnlWork.Visible = true;
                    if (type != OperateType.Add && Configure != null)
                    {
                        pnlWork.InitData(Configure);
                    }
                    break;
                case Utils.ComboBoxHelper.ConfigureMainFrequencyTag:
                    Size = new Size(pnlFreq.Width + 18, pnlFreq.Height + 90);
                    pnlFreq.Visible = true;
                    if (type != OperateType.Add && Configure != null)
                    {
                        pnlFreq.InitData(Configure);
                    }
                    break;
                case Utils.ComboBoxHelper.ConfigureRebackTag:
                    Size = new Size(pnlReback.Width + 18, pnlReback.Height + 90);
                    pnlReback.Visible = true;
                    if (type != OperateType.Add && Configure != null)
                    {
                        pnlReback.InitData(Configure);
                    }
                    break;
                case Utils.ComboBoxHelper.ConfigureDefaltVolumeTag:
                    Size = new Size(pnlVolumn.Width + 18, pnlVolumn.Height + 90);
                    pnlVolumn.Visible = true;
                    if (type != OperateType.Add && Configure != null)
                    {
                        pnlVolumn.InitData(Configure);
                    }
                    break;
                case Utils.ComboBoxHelper.ConfigureRebackPeriodTag:
                    Size = new Size(pnlPeriod.Width + 18, pnlPeriod.Height + 90);
                    pnlPeriod.Visible = true;
                    if (type != OperateType.Add && Configure != null)
                    {
                        pnlPeriod.InitData(Configure);
                    }
                    break;
                case Utils.ComboBoxHelper.ConfigureContentMoniterRetbackTag:
                    Size = new Size(pnlContentMoniterRetback.Width + 18, pnlContentMoniterRetback.Height + 90);
                    pnlContentMoniterRetback.Visible = true;
                   // pnlContentMoniterRetback.InitEbmId(ebmId);
                    if (type != OperateType.Add && Configure != null)
                    {
                        pnlContentMoniterRetback.InitData(Configure);
                    }
                    break;
                case Utils.ComboBoxHelper.ConfigureContentRealMoniterTag:
                    Size = new Size(pnlContentRealMoniter.Width + 18, pnlContentRealMoniter.Height + 90);
                    pnlContentRealMoniter.Visible = true;
                    pnlContentRealMoniter.InitEbmId(ebmId);
                    if (type != OperateType.Add && Configure != null)
                    {
                        pnlContentRealMoniter.InitData(Configure);
                    }
                    break;
                case Utils.ComboBoxHelper.ConfigureStatusRetbackTag:
                    Size = new Size(pnlStatusRetback.Width + 18, pnlStatusRetback.Height + 90);
                    pnlStatusRetback.Visible = true;
                    if (type != OperateType.Add && Configure != null)
                    {
                        pnlStatusRetback.InitData(Configure);
                    }
                    break;
                case Utils.ComboBoxHelper.ConfigureSoftwareUpGradeTag:
                    Size = new Size(pnlSoftwareUpGrade.Width + 18, pnlSoftwareUpGrade.Height + 90);
                    pnlSoftwareUpGrade.Visible = true;
                    if (type != OperateType.Add && Configure != null)
                    {
                        pnlSoftwareUpGrade.InitData(Configure);
                    }
                    break;
                case Utils.ComboBoxHelper.ConfigureRdsConfigTag:
                    Size = new Size(pnlRdsConfig.Width + 18, pnlRdsConfig.Height + 90);
                    pnlRdsConfig.Visible = true;
                    if (type != OperateType.Add && Configure != null)
                    {
                        pnlRdsConfig.InitData(Configure);
                    }
                    break;
                case Utils.ComboBoxHelper.ConfigureStatusRetbackGXTag:
                    Size = new Size(pnlStatusRetbackGX.Width + 18, pnlStatusRetbackGX.Height + 90);
                    pnlStatusRetbackGX.Visible = true;
                    if (type != OperateType.Add && Configure != null)
                    {
                        pnlStatusRetbackGX.InitData(Configure);
                    }
                    break;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            switch (tag)
            {
                case Utils.ComboBoxHelper.ConfigureTimeServiceTag:
                    if (!pnlTime.ValidatData()) return;
                    Configure = pnlTime.GetData();
                    break;
                case Utils.ComboBoxHelper.ConfigureSetAddressTag:
                    if (!pnlSet.ValidatData()) return;
                    Configure = pnlSet.GetData();
                    break;
                case Utils.ComboBoxHelper.ConfigureWorkModeTag:
                    if (!pnlWork.ValidatData()) return;
                    Configure = pnlWork.GetData();
                    break;
                case Utils.ComboBoxHelper.ConfigureMainFrequencyTag:
                    if (!pnlFreq.ValidatData()) return;
                    Configure = pnlFreq.GetData();
                    break;
                case Utils.ComboBoxHelper.ConfigureRebackTag:
                    if (!pnlReback.ValidatData()) return;
                    Configure = pnlReback.GetData();
                    break;
                case Utils.ComboBoxHelper.ConfigureDefaltVolumeTag:
                    if (!pnlVolumn.ValidatData()) return;
                    Configure = pnlVolumn.GetData();
                    break;
                case Utils.ComboBoxHelper.ConfigureRebackPeriodTag:
                    if (!pnlPeriod.ValidatData()) return;
                    Configure = pnlPeriod.GetData();
                    break;
                case Utils.ComboBoxHelper.ConfigureContentMoniterRetbackTag:
                    if (!pnlContentMoniterRetback.ValidatData()) return;
                    Configure = pnlContentMoniterRetback.GetData();
                    break;
                case Utils.ComboBoxHelper.ConfigureContentRealMoniterTag:
                    if (!pnlContentRealMoniter.ValidatData()) return;
                    Configure = pnlContentRealMoniter.GetData();
                    break;
                case Utils.ComboBoxHelper.ConfigureStatusRetbackTag:
                    if (!pnlStatusRetback.ValidatData()) return;
                    Configure = pnlStatusRetback.GetData();
                    break;
                case Utils.ComboBoxHelper.ConfigureSoftwareUpGradeTag:
                    if (!pnlSoftwareUpGrade.ValidatData()) return;
                    Configure = pnlSoftwareUpGrade.GetData();
                    break;
                case Utils.ComboBoxHelper.ConfigureRdsConfigTag:
                    if (!pnlRdsConfig.ValidatData()) return;
                    Configure = pnlRdsConfig.GetData();
                    break;
                case Utils.ComboBoxHelper.ConfigureStatusRetbackGXTag:
                    if (!pnlStatusRetbackGX.ValidatData()) return;
                    Configure = pnlStatusRetbackGX.GetData();
                    break;
            }
            DialogResult = DialogResult.OK;
        }

    }
}
