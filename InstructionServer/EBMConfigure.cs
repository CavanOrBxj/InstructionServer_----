using ControlAstro.Utils;
using EBMTable;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using InstructionServer.Enums;

namespace InstructionServer
{
    public partial class EBMConfigure : Form
    {
        private BindingCollection<TimeService> TimeService_List;
        private BindingCollection<SetAddress> SetAddress_List;
        private BindingCollection<WorkMode> WorkMode_List;
        private BindingCollection<MainFrequency> MainFrequency_List;
        private BindingCollection<Reback> Reback_List;
        private BindingCollection<DefaltVolume> DefaltVolume_List;
        private BindingCollection<RebackPeriod> RebackPeriod_List;
        private BindingCollection<ContentMoniterRetback> ContentMoniterRetback_List;
        private BindingCollection<ContentRealMoniter> ContentRealMoniter_List;
        private BindingCollection<StatusRetback> StatusRetback_List;
        private BindingCollection<SoftwareUpGrade> SoftwareUpGrade_List;
        private BindingCollection<RdsConfig> RdsConfig_List;
        private BindingCollection<StatusRetbackGX> StatusRetbackGX_List;

        private BindingCollection<Configure> TotalConfig_List;

        private List<string> ebmId;
        private Timer timer;
        private DateTime oldTime;

        public EBMConfigure()
        {
            InitializeComponent();
            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;
            timer.Start();
            oldTime = DateTime.Now.AddMinutes(-2);

            InitDataGridView();
            InitEbmId();
            InitConfiureType();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            bool getSystemTime = false;
            DateTime date = DateTime.Now;
            try
            {
                foreach (TimeService time in TotalConfig_List.Where(q => q.B_Daily_cmd_tag == Utils.ComboBoxHelper.ConfigureTimeServiceTag))
                {
                    if (time.GetSystemTime)
                    {
                        time.TimeSer = date.ToString();
                        getSystemTime = true;
                    }
                }
                if (getSystemTime)
                {
                    int tick = 60;
                    bool succ = int.TryParse(textBoxSendTick.Text.Trim(), out tick);
                    if ((date - oldTime).TotalSeconds >= (succ ? tick : 60) && (MdiParent as EBMMain).IsStartStream && GetSendTimeSerConfigureCmd().Count > 0)
                    {
                        (MdiParent as EBMMain).EbMStream.EB_Configure_Table = GetConfigureTable(ref (MdiParent as EBMMain).EbMStream.EB_Configure_Table, true) ? (MdiParent as EBMMain).EbMStream.EB_Configure_Table : null;

                       // EBMMain
                      //  (MdiParent as EBMMain).EbMStream.SignatureCallbackRef = new EBMStream.SignatureCallBackDelegateRef(Calcle.SignatureFunc);//每次在 Initialization()之前调用
                        (MdiParent as EBMMain).EbMStream.Initialization();
                        (MdiParent as EBMMain).UpdateDataText();
                        oldTime = date;
                    }
                    dgvTimeService.Invalidate();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
            }
        }

        private void InitDataGridView()
        {
            if (TimeService_List == null) TimeService_List = new BindingCollection<TimeService>();
            if (SetAddress_List == null) SetAddress_List = new BindingCollection<SetAddress>();
            if (WorkMode_List == null) WorkMode_List = new BindingCollection<WorkMode>();
            if (MainFrequency_List == null) MainFrequency_List = new BindingCollection<MainFrequency>();
            if (Reback_List == null) Reback_List = new BindingCollection<Reback>();
            if (DefaltVolume_List == null) DefaltVolume_List = new BindingCollection<DefaltVolume>();
            if (RebackPeriod_List == null) RebackPeriod_List = new BindingCollection<RebackPeriod>();
            if (ContentMoniterRetback_List == null) ContentMoniterRetback_List = new BindingCollection<ContentMoniterRetback>();
            if (ContentRealMoniter_List == null) ContentRealMoniter_List = new BindingCollection<ContentRealMoniter>();
            if (StatusRetback_List == null) StatusRetback_List = new BindingCollection<StatusRetback>();
            if (SoftwareUpGrade_List == null) SoftwareUpGrade_List = new BindingCollection<SoftwareUpGrade>();
            if (RdsConfig_List == null) RdsConfig_List = new BindingCollection<RdsConfig>();
            if (StatusRetbackGX_List == null) StatusRetbackGX_List = new BindingCollection<StatusRetbackGX>();
            if (TotalConfig_List == null) TotalConfig_List = new BindingCollection<Configure>();

            try
            {
                var jo = TableData.TableDataHelper.ReadTable(Enums.TableType.Configure);
                if (jo != null)
                {
                    if (jo["0"] != null)
                        TimeService_List = JsonConvert.DeserializeObject<BindingCollection<TimeService>>(jo["0"].ToString());
                    foreach (var l in TimeService_List)
                    {
                        TotalConfig_List.Add(l);
                    }

                    if (jo["1"] != null)
                        SetAddress_List = JsonConvert.DeserializeObject<BindingCollection<SetAddress>>(jo["1"].ToString());
                    foreach (var l in SetAddress_List)
                    {
                        TotalConfig_List.Add(l);
                    }

                    if (jo["2"] != null)
                        WorkMode_List = JsonConvert.DeserializeObject<BindingCollection<WorkMode>>(jo["2"].ToString());
                    foreach (var l in WorkMode_List)
                    {
                        TotalConfig_List.Add(l);
                    }

                    if (jo["3"] != null)
                        MainFrequency_List = JsonConvert.DeserializeObject<BindingCollection<MainFrequency>>(jo["3"].ToString());
                    foreach (var l in MainFrequency_List)
                    {
                        TotalConfig_List.Add(l);
                    }

                    if (jo["4"] != null)
                        Reback_List = JsonConvert.DeserializeObject<BindingCollection<Reback>>(jo["4"].ToString());
                    foreach (var l in Reback_List)
                    {
                        TotalConfig_List.Add(l);
                    }

                    if (jo["5"] != null)
                        DefaltVolume_List = JsonConvert.DeserializeObject<BindingCollection<DefaltVolume>>(jo["5"].ToString());
                    foreach (var l in DefaltVolume_List)
                    {
                        TotalConfig_List.Add(l);
                    }

                    if (jo["6"] != null)
                        RebackPeriod_List = JsonConvert.DeserializeObject<BindingCollection<RebackPeriod>>(jo["6"].ToString());
                    foreach (var l in RebackPeriod_List)
                    {
                        TotalConfig_List.Add(l);
                    }

                    if (jo["7"] != null)
                        ContentMoniterRetback_List = JsonConvert.DeserializeObject<BindingCollection<ContentMoniterRetback>>(jo["7"].ToString());
                    foreach (var l in ContentMoniterRetback_List)
                    {
                        TotalConfig_List.Add(l);
                    }

                    if (jo["8"] != null)
                        ContentRealMoniter_List = JsonConvert.DeserializeObject<BindingCollection<ContentRealMoniter>>(jo["8"].ToString());
                    foreach (var l in ContentRealMoniter_List)
                    {
                        TotalConfig_List.Add(l);
                    }

                    if (jo["9"] != null)
                        StatusRetback_List = JsonConvert.DeserializeObject<BindingCollection<StatusRetback>>(jo["9"].ToString());
                    foreach (var l in StatusRetback_List)
                    {
                        TotalConfig_List.Add(l);
                    }

                    if (jo["10"] != null)
                        SoftwareUpGrade_List = JsonConvert.DeserializeObject<BindingCollection<SoftwareUpGrade>>(jo["10"].ToString());
                    foreach (var l in SoftwareUpGrade_List)
                    {
                        TotalConfig_List.Add(l);
                    }

                    if (jo["11"] != null)
                        RdsConfig_List = JsonConvert.DeserializeObject<BindingCollection<RdsConfig>>(jo["11"].ToString());
                    foreach (var l in RdsConfig_List)
                    {
                        TotalConfig_List.Add(l);
                    }

                    if (jo["12"] != null)
                        StatusRetbackGX_List = JsonConvert.DeserializeObject<BindingCollection<StatusRetbackGX>>(jo["12"].ToString());
                    foreach (var l in StatusRetbackGX_List)
                    {
                        TotalConfig_List.Add(l);
                    }
                }
            }
            catch
            {
                Console.WriteLine("读取配置列表失败");
            }
            dgvTimeService.AutoGenerateColumns = false;
            dgvSetAddress.AutoGenerateColumns = false;
            dgvWorkMode.AutoGenerateColumns = false;
            dgvMainFrequency.AutoGenerateColumns = false;
            dgvReback.AutoGenerateColumns = false;
            dgvVolumn.AutoGenerateColumns = false;
            dgvRebackPeriod.AutoGenerateColumns = false;
            dgvContentMoniterRetback.AutoGenerateColumns = false;
            dgvContentRealMoniter.AutoGenerateColumns = false;
            dgvStatusRetback.AutoGenerateColumns = false;
            dgvSoftwareUpGrade.AutoGenerateColumns = false;
            dgvRdsConfig.AutoGenerateColumns = false;
            dgvStatusRetbackGX.AutoGenerateColumns = false;
            dgvTotal.AutoGenerateColumns = false;
            dgvTotal.DataSource = TotalConfig_List;

            dgvTimeService.CellContentClick += DgvTimeService_CellContentClick;
            dgvWorkMode.CellContentClick += DgvWorkMode_CellContentClick;
            dgvMainFrequency.CellContentClick += DgvMainFrequency_CellContentClick;
            dgvReback.CellContentClick += DgvReback_CellContentClick;
            dgvVolumn.CellContentClick += DgvVolumn_CellContentClick;
            dgvRebackPeriod.CellContentClick += DgvRebackPeriod_CellContentClick;
            dgvContentMoniterRetback.CellContentClick += DgvContentMoniterRetback_CellContentClick;
            dgvContentRealMoniter.CellContentClick += DgvContentRealMoniter_CellContentClick;
            dgvStatusRetback.CellContentClick += DgvStatusRetback_CellContentClick;
            dgvSoftwareUpGrade.CellContentClick += DgvSoftwareUpGrade_CellContentClick;
            dgvRdsConfig.CellContentClick += DgvRdsConfig_CellContentClick;
            dgvStatusRetbackGX.CellContentClick += DgvStatusRetbackGX_CellContentClick;

            dgvSoftwareUpGrade.DataError += DgvSoftwareUpGrade_DataError;

            dgvTotal.TopLeftHeaderCell.Value = "指令序号";
            dgvTotal.TopLeftHeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void InitEbmId()
        {
            BindingCollection<EBMIndex.EBIndexEx> index = new BindingCollection<EBMIndex.EBIndexEx>();
            ebmId = new List<string>();
            var jo = TableData.TableDataHelper.ReadTable(Enums.TableType.Index);
            if (jo != null)
            {
                index = JsonConvert.DeserializeObject<BindingCollection<EBMIndex.EBIndexEx>>(jo["0"].ToString());
            }
            foreach (var dex in index)
            {
                ebmId.Add(dex.S_EBM_id);
            }
        }

        private void InitConfiureType()
        {
            Utils.ComboBoxHelper.InitAddressType(ColumnRetbackB_Address_type);
            Utils.ComboBoxHelper.InitAddressType(ColumnRealMoniterAddressType);
            Utils.ComboBoxHelper.InitAddressType(ColumnStatusRetAddressType);
            Utils.ComboBoxHelper.InitAddressType(ColumnFreqB_Address_type);
            Utils.ComboBoxHelper.InitAddressType(ColumnPeriodB_Address_type);
            Utils.ComboBoxHelper.InitAddressType(ColumnRebackB_Address_type);
            Utils.ComboBoxHelper.InitAddressType(ColumnVolumnB_Address_type);
            Utils.ComboBoxHelper.InitAddressType(ColumnWorkAddress_type);
            Utils.ComboBoxHelper.InitAddressType(ColumnSoftUpGradeAddress_type);
            Utils.ComboBoxHelper.InitAddressType(ColumnRdsConfigAddressType);
            Utils.ComboBoxHelper.InitAddressType(ColumnStatusGXAddressType);

           // Utils.ComboBoxHelper.InitRetbackModeType(ColumnRetback_mode);
            Utils.ComboBoxHelper.InitAudioRetbackModeType(ColumnRetback_mode);


            Utils.ComboBoxHelper.InitRealRetbackModeType(ColumnRealMonRetMode);

            Utils.ComboBoxHelper.InitFreqQAMType(ColumnFreqQAM);
            Utils.ComboBoxHelper.InitConfigRebackType(ColumnRebackB_reback_type);
            Utils.ComboBoxHelper.InitConfigWorkMode(ColumnWorkTerminal_wordmode);

            Utils.ComboBoxHelper.InitUpGradeCarrMode(ColumnCarrMode);
            Utils.ComboBoxHelper.InitUpGradeFHMode(ColumnFHMode);
            Utils.ComboBoxHelper.InitUpGradeILMode(ColumnILMode);
            Utils.ComboBoxHelper.InitUpGradeMode(ColumnMode);
            //Utils.ComboBoxHelper.InitUpGradeDeviceType(ColumnDeviceType);

            Utils.ComboBoxHelper.InitConfigRdsTerminalType(ColumnRdsTerminalType);
            Utils.ComboBoxHelper.InitGXTerminalRetbackType(ColumnTerminalRetbackType);

            Utils.ComboBoxHelper.InitConfigureType(ColumnOrderType);

          //  ColumnRetbackS_EBM_id.DataSource = ebmId;
            ColumnRealMonS_EBM_Id.DataSource = ebmId;

            checkBoxAllSel.Enabled = TotalConfig_List.Count > 0;
        }

        #region 各表点击事件
        private void DgvStatusRetbackGX_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                StatusRetbackGX config = (TotalConfig_List[dgvTotal.SelectedRows[0].Index] as StatusRetbackGX);
                if (e.ColumnIndex == ColumnStatusGXTerminalAddress.Index)
                {
                    EBMConfigureDetail form = new EBMConfigureDetail(0, config.Configure.list_Terminal_Address);
                    DialogResult result = form.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        config.Configure.list_Terminal_Address = form.GetData() as List<string>;
                    }
                    form.Dispose();
                }
            }
        }

        private void DgvRdsConfig_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                RdsConfig config = (TotalConfig_List[dgvTotal.SelectedRows[0].Index] as RdsConfig);
                if (e.ColumnIndex == ColumnRdsConfigTerminalAddress.Index)
                {
                    EBMConfigureDetail form = new EBMConfigureDetail(0, config.Configure.list_Terminal_Address);
                    DialogResult result = form.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        config.Configure.list_Terminal_Address = form.GetData() as List<string>;
                    }
                    form.Dispose();
                }
            }
        }

        private void DgvSoftwareUpGrade_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                SoftwareUpGrade config = (TotalConfig_List[dgvTotal.SelectedRows[0].Index] as SoftwareUpGrade);
                if (e.ColumnIndex == ColumnSoftUpGradeTerminal_Address.Index)
                {
                    EBMConfigureDetail form = new EBMConfigureDetail(0, config.Configure.list_Terminal_Address);
                    DialogResult result = form.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        config.Configure.list_Terminal_Address = form.GetData() as List<string>;
                    }
                    form.Dispose();
                }
            }
        }

        private void DgvStatusRetback_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                StatusRetback config = (TotalConfig_List[dgvTotal.SelectedRows[0].Index] as StatusRetback);
                if (e.ColumnIndex == ColumnList_Parameter_tag.Index)
                {
                    EBMConfigureDetail form = new EBMConfigureDetail(1, config.Configure.list_Parameter_tag);
                    DialogResult result = form.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        config.Configure.list_Parameter_tag = form.GetData() as List<byte>;
                    }
                    form.Dispose();
                }
                if (e.ColumnIndex == ColumnStatusRetList_Terminal_Address.Index)
                {
                    EBMConfigureDetail form = new EBMConfigureDetail(0, config.Configure.list_Terminal_Address);
                    DialogResult result = form.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        config.Configure.list_Terminal_Address = form.GetData() as List<string>;
                    }
                    form.Dispose();
                }
            }
        }

        private void DgvContentRealMoniter_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                ContentRealMoniter config = (TotalConfig_List[dgvTotal.SelectedRows[0].Index] as ContentRealMoniter);
                if (e.ColumnIndex == ColumnRealMoniterAddress.Index)
                {
                    EBMConfigureDetail form = new EBMConfigureDetail(0, config.Configure.list_Terminal_Address);
                    DialogResult result = form.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        config.Configure.list_Terminal_Address = form.GetData() as List<string>;
                    }
                    form.Dispose();
                }
            }
        }

        private void DgvContentMoniterRetback_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                ContentMoniterRetback config = (TotalConfig_List[dgvTotal.SelectedRows[0].Index] as ContentMoniterRetback);
                if (e.ColumnIndex == ColumnRetBackListAddress.Index)
                {
                    EBMConfigureDetail form = new EBMConfigureDetail(0, config.Configure.list_Terminal_Address);
                    DialogResult result = form.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        config.Configure.list_Terminal_Address = form.GetData() as List<string>;
                    }
                    form.Dispose();
                }
            }
        }

        private void DgvRebackPeriod_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                RebackPeriod config = (TotalConfig_List[dgvTotal.SelectedRows[0].Index] as RebackPeriod);
                if (e.ColumnIndex == ColumnPeriodListAddress.Index)
                {
                    EBMConfigureDetail form = new EBMConfigureDetail(0, config.Configure.list_Terminal_Address);
                    DialogResult result = form.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        config.Configure.list_Terminal_Address = form.GetData() as List<string>;
                    }
                    form.Dispose();
                }
            }
        }

        private void DgvVolumn_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                DefaltVolume config = (TotalConfig_List[dgvTotal.SelectedRows[0].Index] as DefaltVolume);
                if (e.ColumnIndex == ColumnVolumnListAddress.Index)
                {
                    EBMConfigureDetail form = new EBMConfigureDetail(0, config.Configure.list_Terminal_Address);
                    DialogResult result = form.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        config.Configure.list_Terminal_Address = form.GetData() as List<string>;
                    }
                    form.Dispose();
                }
            }
        }

        private void DgvReback_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                Reback config = (TotalConfig_List[dgvTotal.SelectedRows[0].Index] as Reback);
                if (e.ColumnIndex == ColumnRebackListAddress.Index)
                {
                    EBMConfigureDetail form = new EBMConfigureDetail(0, config.Configure.list_Terminal_Address);
                    DialogResult result = form.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        config.Configure.list_Terminal_Address = form.GetData() as List<string>;
                    }
                    form.Dispose();
                }
            }
        }

        private void DgvMainFrequency_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                MainFrequency config = (TotalConfig_List[dgvTotal.SelectedRows[0].Index] as MainFrequency);
                if (e.ColumnIndex == ColumnFreqTerminalAddress.Index)
                {
                    EBMConfigureDetail form = new EBMConfigureDetail(0, config.Configure.list_Terminal_Address);
                    DialogResult result = form.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        config.Configure.list_Terminal_Address = form.GetData() as List<string>;
                    }
                    form.Dispose();
                }
            }
        }

        private void DgvWorkMode_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                WorkMode config = (TotalConfig_List[dgvTotal.SelectedRows[0].Index] as WorkMode);
                if (e.ColumnIndex == ColumnWorkTerminalAddress.Index)
                {
                    EBMConfigureDetail form = new EBMConfigureDetail(0, config.Configure.list_Terminal_Address);
                    DialogResult result = form.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        config.Configure.list_Terminal_Address = form.GetData() as List<string>;
                    }
                    form.Dispose();
                }
            }
        }

        private void DgvTimeService_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                TimeService config = (TotalConfig_List[dgvTotal.SelectedRows[0].Index] as TimeService);
                if (e.ColumnIndex == ColumnGetSystemTime.Index)
                {
                    config.GetSystemTime = !config.GetSystemTime;
                }
            }
        }

        #endregion

        private List<ConfigureCmd> GetSendConfigureCmd()
        {
            try
            {
                List<ConfigureCmd> cmd = new List<ConfigureCmd>();
                foreach (var d in TotalConfig_List)
                {
                    if (d.SendState)
                    {
                        switch (d.B_Daily_cmd_tag)
                        {
                            case Utils.ComboBoxHelper.ConfigureTimeServiceTag:
                                cmd.Add((d as TimeService).Configure.GetCmd());
                                break;
                            case Utils.ComboBoxHelper.ConfigureSetAddressTag:
                                cmd.Add((d as SetAddress).Configure.GetCmd());
                                break;
                            case Utils.ComboBoxHelper.ConfigureWorkModeTag:
                                cmd.Add((d as WorkMode).Configure.GetCmd());
                                break;
                            case Utils.ComboBoxHelper.ConfigureMainFrequencyTag:
                                cmd.Add((d as MainFrequency).Configure.GetCmd());
                                break;
                            case Utils.ComboBoxHelper.ConfigureRebackTag:
                                cmd.Add((d as Reback).Configure.GetCmd());
                                break;
                            case Utils.ComboBoxHelper.ConfigureDefaltVolumeTag:
                                cmd.Add((d as DefaltVolume).Configure.GetCmd());
                                break;
                            case Utils.ComboBoxHelper.ConfigureRebackPeriodTag:
                                cmd.Add((d as RebackPeriod).Configure.GetCmd());
                                break;
                            case Utils.ComboBoxHelper.ConfigureContentMoniterRetbackTag:
                                cmd.Add((d as ContentMoniterRetback).Configure.GetCmd());
                                break;
                            case Utils.ComboBoxHelper.ConfigureContentRealMoniterTag:
                                cmd.Add((d as ContentRealMoniter).Configure.GetCmd());
                                break;
                            case Utils.ComboBoxHelper.ConfigureStatusRetbackTag:
                                cmd.Add((d as StatusRetback).Configure.GetCmd());
                                break;
                            case Utils.ComboBoxHelper.ConfigureSoftwareUpGradeTag:
                                cmd.Add((d as SoftwareUpGrade).Configure.GetCmd());
                                break;
                            case Utils.ComboBoxHelper.ConfigureRdsConfigTag:
                                cmd.Add((d as RdsConfig).Configure.GetCmd());
                                break;
                            case Utils.ComboBoxHelper.ConfigureStatusRetbackGXTag:
                                cmd.Add((d as StatusRetbackGX).Configure.GetCmd());
                                break;
                        }
                    }
                }
                return cmd;
            }
            catch
            {
                return null;
            }
        }

        private List<ConfigureCmd> GetSendTimeSerConfigureCmd()
        {
            try
            {
                List<ConfigureCmd> cmd = new List<ConfigureCmd>();
                foreach (var d in TotalConfig_List)
                {
                    if (d.SendState && d.B_Daily_cmd_tag == Utils.ComboBoxHelper.ConfigureTimeServiceTag)
                    {
                        cmd.Add((d as TimeService).Configure.GetCmd());
                    }
                }
                return cmd;
            }
            catch
            {
                return null;
            }
        }

        public bool GetConfigureTable(ref EBConfigureTable oldTable, bool isTimeSend)
        {
            try
            {
                List<ConfigureCmd> configureCmd = isTimeSend ? GetSendTimeSerConfigureCmd() : GetSendConfigureCmd();
                if (configureCmd == null || configureCmd.Count == 0)
                {
                    if (oldTable != null) oldTable.list_configure_cmd = null;
                    return false;
                }
                if (oldTable == null)
                {
                    oldTable = new EBConfigureTable();
                    oldTable.Table_id = 0xfb;
                    oldTable.Table_id_extension = 0;
                }
                oldTable.list_configure_cmd = configureCmd;
                oldTable.Repeat_times = pnlRepeatTimes.GetRepeatTimes();// pnlRepeatTimes.GetRepeatTimes() + 1;
                return true;
            }
            catch
            {
                return false;
            }
        }

        #region 内建类

        public class TimeService : Configure
        {
            public override byte B_Daily_cmd_tag { get { return Utils.ComboBoxHelper.ConfigureTimeServiceTag; } }
            public EBConfigureTimeService Configure { get; set; }
            public string TimeSer
            {
                get { return Configure.Real_time.ToString(); }
                set { Configure.Real_time = Convert.ToDateTime(value); }
            }
            public bool GetSystemTime { get; set; }
            private int sendTick = 60;
            public int SendTick
            {
                get { return sendTick; }
                set { sendTick = value; }
            }
            public override string Summary
            {
                get
                {
                    return string.Join("/", TimeSer);
                }
            }
        }

        public class SetAddress : Configure
        {
            public override byte B_Daily_cmd_tag { get { return Utils.ComboBoxHelper.ConfigureSetAddressTag; } }
            public EBConfigureSetAddress Configure { get; set; }
            public string S_Logic_address
            {
                get { return Configure.S_Logic_address; }
                set { Configure.S_Logic_address = value; }
            }
            public string S_Phisical_address
            {
                get { return Configure.S_Phisical_address; }
                set { Configure.S_Phisical_address = value; }
            }
            public override string Summary
            {
                get
                {
                    return string.Join("/", S_Phisical_address, S_Logic_address);
                }
            }
        }

        public class WorkMode : Configure
        {
            public override byte B_Daily_cmd_tag { get { return Utils.ComboBoxHelper.ConfigureWorkModeTag; } }
            public EBConfigureWorkMode Configure { get; set; }
            public byte B_Address_type
            {
                get { return Configure.B_Address_type; }
                set { Configure.B_Address_type = value; }
            }
            public byte B_Terminal_wordmode
            {
                get { return Configure.B_Terminal_wordmode; }
                set { Configure.B_Terminal_wordmode = value; }
            }
            public override string Summary
            {
                get
                {
                    return string.Join("/", Utils.ComboBoxHelper.GetTypeStringValue(Enums.ParamType.ConfigWorkMode, B_Terminal_wordmode),
                        Utils.ComboBoxHelper.GetTypeStringValue(Enums.ParamType.AddressType, B_Address_type),
                        Configure.list_Terminal_Address.Count > 1 ? Configure.list_Terminal_Address[0] + "..." : "...");
                }
            }
        }

        public class MainFrequency : Configure
        {
            public override byte B_Daily_cmd_tag { get { return Utils.ComboBoxHelper.ConfigureMainFrequencyTag; } }
            public EBConfigureMainFrequency Configure { get; set; }
            public byte B_Address_type
            {
                get { return Configure.B_Address_type; }
                set { Configure.B_Address_type = value; }
            }
            public int Freq
            {
                get { return Configure.Freq; }
                set { Configure.Freq = value; }
            }
            public short QAM
            {
                get { return Configure.QAM; }
                set { Configure.QAM = value; }
            }
            public int SymbolRate
            {
                get { return Configure.SymbolRate; }
                set { Configure.SymbolRate = value; }
            }
            public override string Summary
            {
                get
                {
                    return string.Join("/", Freq, SymbolRate, Utils.ComboBoxHelper.GetTypeStringValue(Enums.ParamType.ConfigFreqQAM,QAM),
                        Utils.ComboBoxHelper.GetTypeStringValue(Enums.ParamType.AddressType, B_Address_type), "...");
                }
            }
        }

        public class Reback : Configure
        {
            public override byte B_Daily_cmd_tag { get { return Utils.ComboBoxHelper.ConfigureRebackTag; } }
            public EBConfigureReback Configure { get; set; }
            public byte B_Address_type
            {
                get { return Configure.B_Address_type; }
                set { Configure.B_Address_type = value; }
            }
            public byte B_reback_type
            {
                get { return Configure.B_reback_type; }
                set { Configure.B_reback_type = value; }
            }
            public string S_reback_address
            {
                get { return Configure.S_reback_address; }
                set { Configure.S_reback_address = value; }
            }
            public override string Summary
            {
                get
                {
                    return string.Join("/", Utils.ComboBoxHelper.GetTypeStringValue(Enums.ParamType.ConfigRebackType, B_reback_type), S_reback_address,
                        Utils.ComboBoxHelper.GetTypeStringValue(Enums.ParamType.AddressType, B_Address_type), "...");
                }
            }
        }

        public class DefaltVolume : Configure
        {
            public override byte B_Daily_cmd_tag { get { return Utils.ComboBoxHelper.ConfigureDefaltVolumeTag; } }
            public EBConigureDefaltVolume Configure { get; set; }
            public byte B_Address_type
            {
                get { return Configure.B_Address_type; }
                set { Configure.B_Address_type = value; }
            }
            public short Column
            {
                get { return Configure.Column; }
                set { Configure.Column = value; }
            }
            public override string Summary
            {
                get
                {
                    return string.Join("/", Column, Utils.ComboBoxHelper.GetTypeStringValue(Enums.ParamType.AddressType, B_Address_type),
                        Configure.list_Terminal_Address.Count > 1 ? Configure.list_Terminal_Address[0] + "..." : "...");
                }
            }
        }

        public class RebackPeriod : Configure
        {
            public override byte B_Daily_cmd_tag { get { return Utils.ComboBoxHelper.ConfigureRebackPeriodTag; } }

            public EBConfigureRebackPeriod Configure { get; set; }
            public byte B_Address_type
            {
                get { return Configure.B_Address_type; }
                set { Configure.B_Address_type = value; }
            }
            public int reback_period
            {
                get { return Configure.reback_period; }
                set { Configure.reback_period = value; }
            }
            public override string Summary
            {
                get
                {
                    return string.Join("/", reback_period, Utils.ComboBoxHelper.GetTypeStringValue(Enums.ParamType.AddressType, B_Address_type),
                        Configure.list_Terminal_Address.Count > 1 ? Configure.list_Terminal_Address[0] + "..." : "...");
                }
            }
        }

        public class ContentMoniterRetback : Configure
        {
            public override byte B_Daily_cmd_tag { get { return Utils.ComboBoxHelper.ConfigureContentMoniterRetbackTag; } }
            public EBConfigureContentMoniterRetbackGX Configure { get; set; }

          
            public int Start_package_index
            {
                get { return Configure.Start_package_index; }
                set { Configure.Start_package_index = value; }
            }
            public string S_Reback_serverIP
            {
                get { return Configure.S_Audio_reback_serverip; }
                set { Configure.S_Audio_reback_serverip = value; }
            }

            public int I_Reback_PORT
            {
                get { return Configure.I_Audio_reback_port; }
                set { Configure.I_Audio_reback_port = value; }
            }

            public string S_File_id
            {
                get { return Configure.S_File_id; }
                set { Configure.S_File_id = value; }
            }
            //public byte B_AudioRetback_mode
            //{
            //    get { return Configure.B_Audio_reback_mod; }
            //    set { Configure.B_Audio_reback_mod = value; }
            //}

            public int B_AudioRetback_mode
            {
                get { return (int)Configure.B_Audio_reback_mod; }
                set { Configure.B_Audio_reback_mod =(byte)value; }
            }
            public byte B_Address_type
            {
                get { return Configure.B_Address_type; }
                set { Configure.B_Address_type = value; }
            }
            public override string Summary
            {
                get
                {
                    return string.Join("/", S_Reback_serverIP, I_Reback_PORT, S_File_id, Utils.ComboBoxHelper.GetTypeStringValue(Enums.ParamType.ConfigRetbackMode, B_AudioRetback_mode), Start_package_index,
                        Utils.ComboBoxHelper.GetTypeStringValue(Enums.ParamType.AddressType, B_Address_type), "...");
                }
            }
        }

        public class ContentRealMoniter : Configure
        {
            public override byte B_Daily_cmd_tag { get { return Utils.ComboBoxHelper.ConfigureContentRealMoniterTag; } }
            public EBConfigureContentRealMoniter Configure { get; set; }
            public byte B_Address_type
            {
                get { return Configure.B_Address_type; }
                set { Configure.B_Address_type = value; }
            }
            public string S_EBM_id
            {
                get { return Configure.S_EBM_id; }
                set { Configure.S_EBM_id = value; }
            }
            public string S_Server_addr
            {
                get { return Configure.S_Server_addr; }
                set { Configure.S_Server_addr = value; }
            }
            public short Retback_mode
            {
                get { return Configure.Retback_mode; }
                set { Configure.Retback_mode = value; }
            }
            public int Moniter_time_duration
            {
                get { return Configure.Moniter_time_duration; }
                set { Configure.Moniter_time_duration = value; }
            }
            public override string Summary
            {
                get
                {
                    return string.Join("/", S_EBM_id, Utils.ComboBoxHelper.GetTypeStringValue(Enums.ParamType.ConfigRetbackMode, Retback_mode), Moniter_time_duration, S_Server_addr,
                        Utils.ComboBoxHelper.GetTypeStringValue(Enums.ParamType.AddressType, B_Address_type), "...");
                }
            }
        }

        public class StatusRetback : Configure
        {
            public override byte B_Daily_cmd_tag { get { return Utils.ComboBoxHelper.ConfigureStatusRetbackTag; } }
            public EBConfigureStatusRetback Configure { get; set; }
            public byte B_Address_type
            {
                get { return Configure.B_Address_type; }
                set { Configure.B_Address_type = value; }
            }
            public override string Summary
            {
                get
                {
                    return string.Join("/", Utils.ComboBoxHelper.GetTypeStringValue(Enums.ParamType.AddressType, B_Address_type),
                        Configure.list_Parameter_tag.Count > 0 ? Utils.ComboBoxHelper.GetTypeStringValue(Enums.ParamType.ConfigStatusParameterTag, Configure.list_Parameter_tag[0]) + "..." : "...");
                }
            }
        }

        public class SoftwareUpGrade : Configure
        {
            public override byte B_Daily_cmd_tag
            {
                get
                {
                    return Utils.ComboBoxHelper.ConfigureSoftwareUpGradeTag;
                }
            }
            public EBConfigureSoftwareUpGrade Configure { get; set; }
            public byte B_CarrMode
            {
                get { return Configure.B_CarrMode; }
                set { Configure.B_CarrMode = value; }
            }
            public byte B_FHMode
            {
                get { return Configure.B_FHMode; }
                set { Configure.B_FHMode = value; }
            }
            public byte B_ILMode
            {
                get { return Configure.B_ILMode; }
                set { Configure.B_ILMode = value; }
            }
            public byte B_Mode
            {
                get { return Configure.B_Mode; }
                set { Configure.B_Mode = value; }
            }
            public byte B_ModType
            {
                get { return Configure.B_ModType; }
                set { Configure.B_ModType = value; }
            }
            public int B_Pid
            {
                get { return Configure.B_Pid; }
                set { Configure.B_Pid = value; }
            }
            public int I_DeviceType
            {
                get { return Configure.I_DeviceType; }
                set { Configure.I_DeviceType = value; }
            }
            public int I_Freq
            {
                get { return Configure.I_Freq; }
                set { Configure.I_Freq = value; }
            }
            public int I_Rate
            {
                get { return Configure.I_Rate; }
                set { Configure.I_Rate = value; }
            }
            public string S_NewVersion
            {
                get { return Configure.S_NewVersion; }
                set { Configure.S_NewVersion = value; }
            }
            public string S_OldVersion
            {
                get { return Configure.S_OldVersion; }
                set { Configure.S_OldVersion = value; }
            }
            public byte B_Address_type
            {
                get { return Configure.B_Address_type; }
                set { Configure.B_Address_type = value; }
            }
            public Enums.DeviceOrderType DeviceOrderType { get; set; }
            public override string Summary
            {
                get
                {
                    return string.Join("/", Utils.ComboBoxHelper.GetTypeStringValue(Enums.ParamType.ConfigUpGradeCarrMode, Configure.B_CarrMode),
                        Utils.ComboBoxHelper.GetTypeStringValue(Enums.ParamType.ConfigUpGradeFHMode, Configure.B_FHMode),
                        Utils.ComboBoxHelper.GetTypeStringValue(Enums.ParamType.ConfigUpGradeILMode, Configure.B_ILMode),
                        Utils.ComboBoxHelper.GetTypeStringValue(Enums.ParamType.ConfigUpGradeMode, Configure.B_Mode),
                        Configure.B_Pid, Configure.I_Freq, Configure.I_Rate, Configure.S_NewVersion, "...");
                }
            }
        }

        public class RdsConfig : Configure
        {
            public override byte B_Daily_cmd_tag
            {
                get
                {
                    return Utils.ComboBoxHelper.ConfigureRdsConfigTag;
                }
            }
            public EBConfigureRdsConfig Configure { get; set; }
            public byte B_Rds_terminal_type
            {
                get { return Configure.B_Rds_terminal_type; }
                set { Configure.B_Rds_terminal_type = value; }
            }
            public byte B_Address_type
            {
                get { return Configure.B_Address_type; }
                set { Configure.B_Address_type = value; }
            }
            public string RdsDataText
            {
                get { return Utils.ArrayHelper.Bytes2String(Configure.Br_Rds_data); }
                set
                {
                    var bytes = Utils.ArrayHelper.String2Bytes(value);
                    if (bytes == null)
                    {
                        MessageBox.Show("输入数据有误，请重新输入。数据按十六进制输入，多个数据用,或空格分隔(如AA FF)", "错误",
                            MessageBoxButtons.OK);
                    }
                    else
                    {
                        Configure.Br_Rds_data = bytes;
                    }
                }
            }
            public override string Summary
            {
                get
                {
                    return string.Join("/", Utils.ComboBoxHelper.GetTypeStringValue(Enums.ParamType.ConfigRdsTerminalType, B_Rds_terminal_type),
                        RdsDataText, Utils.ComboBoxHelper.GetTypeStringValue(Enums.ParamType.AddressType, B_Address_type), "...");
                }
            }
        }

        public class StatusRetbackGX : Configure
        {
            public override byte B_Daily_cmd_tag
            {
                get
                {
                    return Utils.ComboBoxHelper.ConfigureStatusRetbackGXTag;
                }
            }
            public EBConfigureStatusRetbackGX Configure { get; set; }
            public byte B_Terminal_Retback_Type
            {
                get { return Configure.B_Terminal_Retback_Type; }
                set { Configure.B_Terminal_Retback_Type = value; }
            }
            public int I_retback_period
            {
                get { return Configure.I_retback_period; }
                set { Configure.I_retback_period = value; }
            }
            public byte B_Address_type
            {
                get { return Configure.B_Address_type; }
                set { Configure.B_Address_type = value; }
            }
            public override string Summary
            {
                get
                {
                    return string.Join("/", Utils.ComboBoxHelper.GetTypeStringValue(Enums.ParamType.ConfigTerminalRetbackType, B_Terminal_Retback_Type),
                        I_retback_period, Utils.ComboBoxHelper.GetTypeStringValue(Enums.ParamType.AddressType, B_Address_type), "...");
                }
            }
        }

        public abstract class Configure
        {
            public abstract string Summary { get; }
            public abstract byte B_Daily_cmd_tag { get; }
            public bool SendState { get; set; }
        }

        #endregion

        private void MenuStripConfigure_Opening(object sender, CancelEventArgs e)
        {
            MenuItemInfo.Enabled = dgvTotal.RowCount > 0;
            MenuItemDel.Enabled = dgvTotal.RowCount > 0;
            Point downPoint = dgvTotal.PointToClient(MousePosition);
            var hit = dgvTotal.HitTest(downPoint.X, downPoint.Y);
            if (hit.Type == DataGridViewHitTestType.Cell || hit.Type == DataGridViewHitTestType.RowHeader)
            {
                dgvTotal.Rows[hit.RowIndex].Selected = true;
            }
        }

        private void MenuItemInfo_Click(object sender, EventArgs e)
        {
            InfoConfig();
        }

        private void MenuItemDel_Click(object sender, EventArgs e)
        {
            DelConfig();
        }

        #region 增删改

        private void DelConfig()
        {
            if (dgvTotal.SelectedRows.Count > 0)
            {
                byte cmdTag = (dgvTotal.SelectedRows[0].DataBoundItem as Configure).B_Daily_cmd_tag;
                TotalConfig_List.RemoveAt(dgvTotal.SelectedRows[0].Index);
                switch (cmdTag)
                {
                    case Utils.ComboBoxHelper.ConfigureTimeServiceTag:
                        dgvTimeService.DataSource = null;
                        break;
                    case Utils.ComboBoxHelper.ConfigureSetAddressTag:
                        dgvSetAddress.DataSource = null;
                        break;
                    case Utils.ComboBoxHelper.ConfigureWorkModeTag:
                        dgvWorkMode.DataSource = null;
                        break;
                    case Utils.ComboBoxHelper.ConfigureMainFrequencyTag:
                        dgvMainFrequency.DataSource = null;
                        break;
                    case Utils.ComboBoxHelper.ConfigureRebackTag:
                        dgvReback.DataSource = null;
                        break;
                    case Utils.ComboBoxHelper.ConfigureDefaltVolumeTag:
                        dgvVolumn.DataSource = null;
                        break;
                    case Utils.ComboBoxHelper.ConfigureRebackPeriodTag:
                        dgvRebackPeriod.DataSource = null;
                        break;
                    case Utils.ComboBoxHelper.ConfigureContentMoniterRetbackTag:
                        dgvContentMoniterRetback.DataSource = null;
                        break;
                    case Utils.ComboBoxHelper.ConfigureContentRealMoniterTag:
                        dgvContentRealMoniter.DataSource = null;
                        break;
                    case Utils.ComboBoxHelper.ConfigureStatusRetbackTag:
                        dgvStatusRetback.DataSource = null;
                        break;
                    case Utils.ComboBoxHelper.ConfigureSoftwareUpGradeTag:
                        dgvSoftwareUpGrade.DataSource = null;
                        break;
                    case Utils.ComboBoxHelper.ConfigureRdsConfigTag:
                        dgvRdsConfig.DataSource = null;
                        break;
                    case Utils.ComboBoxHelper.ConfigureStatusRetbackGXTag:
                        dgvStatusRetbackGX.DataSource = null;
                        break;
                }
            }
            else
            {
                MessageBox.Show("未选中任何索引");
            }
            checkBoxAllSel.Enabled = TotalConfig_List.Count > 0;
        }

        private void AddConfig(byte tag)
        {
            bool isAdd = false; //是否有新增的数据
            switch (tag)
            {
                case Utils.ComboBoxHelper.ConfigureTimeServiceTag:
                    EBMConfigureInfo infoTime = new EBMConfigureInfo(Enums.OperateType.Add, tag);
                    DialogResult resultTime = infoTime.ShowDialog();
                    if (resultTime == DialogResult.OK && infoTime.Configure != null)
                    {
                        TotalConfig_List.Add(infoTime.Configure);
                        isAdd = true;
                    }
                    infoTime.Dispose();
                    break;
                case Utils.ComboBoxHelper.ConfigureSetAddressTag:
                    EBMConfigureInfo infoSet = new EBMConfigureInfo(Enums.OperateType.Add, tag);
                    DialogResult resultSet = infoSet.ShowDialog();
                    if (resultSet == DialogResult.OK && infoSet.Configure != null)
                    {
                        TotalConfig_List.Add(infoSet.Configure);
                        isAdd = true;
                    }
                    infoSet.Dispose();
                    break;
                case Utils.ComboBoxHelper.ConfigureWorkModeTag:
                    EBMConfigureInfo infoWork = new EBMConfigureInfo(Enums.OperateType.Add, tag);
                    DialogResult resultWork = infoWork.ShowDialog();
                    if (resultWork == DialogResult.OK && infoWork.Configure != null)
                    {
                        TotalConfig_List.Add(infoWork.Configure);
                        isAdd = true;
                    }
                    infoWork.Dispose();
                    break;
                case Utils.ComboBoxHelper.ConfigureMainFrequencyTag:
                    EBMConfigureInfo infoFreq = new EBMConfigureInfo(Enums.OperateType.Add, tag);
                    DialogResult resultFreq = infoFreq.ShowDialog();
                    if (resultFreq == DialogResult.OK && infoFreq.Configure != null)
                    {
                        TotalConfig_List.Add(infoFreq.Configure);
                        isAdd = true;
                    }
                    infoFreq.Dispose();
                    break;
                case Utils.ComboBoxHelper.ConfigureRebackTag:
                    EBMConfigureInfo infoReback = new EBMConfigureInfo(Enums.OperateType.Add, tag);
                    DialogResult resultReback = infoReback.ShowDialog();
                    if (resultReback == DialogResult.OK && infoReback.Configure != null)
                    {
                        TotalConfig_List.Add(infoReback.Configure);
                        isAdd = true;
                    }
                    infoReback.Dispose();
                    break;
                case Utils.ComboBoxHelper.ConfigureDefaltVolumeTag:
                    EBMConfigureInfo infoVolumn = new EBMConfigureInfo(Enums.OperateType.Add, tag);
                    DialogResult resultVolumn = infoVolumn.ShowDialog();
                    if (resultVolumn == DialogResult.OK && infoVolumn.Configure != null)
                    {
                        TotalConfig_List.Add(infoVolumn.Configure);
                        isAdd = true;
                    }
                    infoVolumn.Dispose();
                    break;
                case Utils.ComboBoxHelper.ConfigureRebackPeriodTag:
                    EBMConfigureInfo infoPeriod = new EBMConfigureInfo(Enums.OperateType.Add, tag);
                    DialogResult resultPeriod = infoPeriod.ShowDialog();
                    if (resultPeriod == DialogResult.OK && infoPeriod.Configure != null)
                    {
                        TotalConfig_List.Add(infoPeriod.Configure);
                        isAdd = true;
                    }
                    infoPeriod.Dispose();
                    break;
                case Utils.ComboBoxHelper.ConfigureContentMoniterRetbackTag:
                    EBMConfigureInfo infoContentRetback = new EBMConfigureInfo(Enums.OperateType.Add, tag, ebmId);
                    DialogResult resultContentRetback = infoContentRetback.ShowDialog();
                    if (resultContentRetback == DialogResult.OK && infoContentRetback.Configure != null)
                    {
                        TotalConfig_List.Add(infoContentRetback.Configure);
                        isAdd = true;
                    }
                    infoContentRetback.Dispose();
                    break;
                case Utils.ComboBoxHelper.ConfigureContentRealMoniterTag:
                    EBMConfigureInfo infoRealMoniter = new EBMConfigureInfo(Enums.OperateType.Add, tag, ebmId);
                    DialogResult resultRealMoniter = infoRealMoniter.ShowDialog();
                    if (resultRealMoniter == DialogResult.OK && infoRealMoniter.Configure != null)
                    {
                        TotalConfig_List.Add(infoRealMoniter.Configure);
                        isAdd = true;
                    }
                    infoRealMoniter.Dispose();
                    break;
                case Utils.ComboBoxHelper.ConfigureStatusRetbackTag:
                    EBMConfigureInfo infoStatusRetback = new EBMConfigureInfo(Enums.OperateType.Add, tag);
                    DialogResult resultStatusRetback = infoStatusRetback.ShowDialog();
                    if (resultStatusRetback == DialogResult.OK && infoStatusRetback.Configure != null)
                    {
                        TotalConfig_List.Add(infoStatusRetback.Configure);
                        isAdd = true;
                    }
                    infoStatusRetback.Dispose();
                    break;
                case Utils.ComboBoxHelper.ConfigureSoftwareUpGradeTag:
                    EBMConfigureInfo infoSoftwareUpGrade = new EBMConfigureInfo(Enums.OperateType.Add, tag);
                    DialogResult resultSoftwareUpGrade = infoSoftwareUpGrade.ShowDialog();
                    if (resultSoftwareUpGrade == DialogResult.OK && infoSoftwareUpGrade.Configure != null)
                    {
                        TotalConfig_List.Add(infoSoftwareUpGrade.Configure);
                        isAdd = true;
                    }
                    infoSoftwareUpGrade.Dispose();
                    break;
                case Utils.ComboBoxHelper.ConfigureRdsConfigTag:
                    EBMConfigureInfo infoRdsConfig = new EBMConfigureInfo(Enums.OperateType.Add, tag);
                    DialogResult resultRdsConfig = infoRdsConfig.ShowDialog();
                    if (resultRdsConfig == DialogResult.OK && infoRdsConfig.Configure != null)
                    {
                        TotalConfig_List.Add(infoRdsConfig.Configure);
                        isAdd = true;
                    }
                    infoRdsConfig.Dispose();
                    break;
                case Utils.ComboBoxHelper.ConfigureStatusRetbackGXTag:
                    EBMConfigureInfo infoStatusRetbackGX = new EBMConfigureInfo(Enums.OperateType.Add, tag);
                    DialogResult resultStatusRetbackGX = infoStatusRetbackGX.ShowDialog();
                    if (resultStatusRetbackGX == DialogResult.OK && infoStatusRetbackGX.Configure != null)
                    {
                        TotalConfig_List.Add(infoStatusRetbackGX.Configure);
                        isAdd = true;
                    }
                    infoStatusRetbackGX.Dispose();
                    break;
            }
            if (isAdd)
            {
                TotalConfig_List.Sort(TypeDescriptor.GetProperties(typeof(Configure)).Find("B_Daily_cmd_tag", false), ListSortDirection.Ascending);
            }
            checkBoxAllSel.Enabled = TotalConfig_List.Count > 0;
        }

        private void InfoConfig()
        {
            if (dgvTotal.SelectedRows.Count > 0)
            {
                new EBMConfigureInfo(Enums.OperateType.Info, TotalConfig_List[dgvTotal.SelectedRows[0].Index].B_Daily_cmd_tag,
                    TotalConfig_List[dgvTotal.SelectedRows[0].Index]).ShowDialog();
            }
            else
            {
                MessageBox.Show("未选中任何索引");
            }
        }

        #endregion

        private void EBMConfigure_FormClosing(object sender, FormClosingEventArgs e)
        {
            TimeService_List.Clear();
            SetAddress_List.Clear();
            WorkMode_List.Clear();
            MainFrequency_List.Clear();
            Reback_List.Clear();
            DefaltVolume_List.Clear();
            RebackPeriod_List.Clear();
            ContentMoniterRetback_List.Clear();
            ContentRealMoniter_List.Clear();
            StatusRetback_List.Clear();
            SoftwareUpGrade_List.Clear();
            RdsConfig_List.Clear();
            StatusRetbackGX_List.Clear();
            foreach (var daily in TotalConfig_List)
            {
                switch (daily.B_Daily_cmd_tag)
                {
                    case Utils.ComboBoxHelper.ConfigureTimeServiceTag:
                        TimeService_List.Add(daily as TimeService);
                        break;
                    case Utils.ComboBoxHelper.ConfigureSetAddressTag:
                        SetAddress_List.Add(daily as SetAddress);
                        break;
                    case Utils.ComboBoxHelper.ConfigureWorkModeTag:
                        WorkMode_List.Add(daily as WorkMode);
                        break;
                    case Utils.ComboBoxHelper.ConfigureMainFrequencyTag:
                        MainFrequency_List.Add(daily as MainFrequency);
                        break;
                    case Utils.ComboBoxHelper.ConfigureRebackTag:
                        Reback_List.Add(daily as Reback);
                        break;
                    case Utils.ComboBoxHelper.ConfigureDefaltVolumeTag:
                        DefaltVolume_List.Add(daily as DefaltVolume);
                        break;
                    case Utils.ComboBoxHelper.ConfigureRebackPeriodTag:
                        RebackPeriod_List.Add(daily as RebackPeriod);
                        break;
                    case Utils.ComboBoxHelper.ConfigureContentMoniterRetbackTag:
                        ContentMoniterRetback_List.Add(daily as ContentMoniterRetback);
                        break;
                    case Utils.ComboBoxHelper.ConfigureContentRealMoniterTag:
                        ContentRealMoniter_List.Add(daily as ContentRealMoniter);
                        break;
                    case Utils.ComboBoxHelper.ConfigureStatusRetbackTag:
                        StatusRetback_List.Add(daily as StatusRetback);
                        break;
                    case Utils.ComboBoxHelper.ConfigureSoftwareUpGradeTag:
                        SoftwareUpGrade_List.Add(daily as SoftwareUpGrade);
                        break;
                    case Utils.ComboBoxHelper.ConfigureRdsConfigTag:
                        RdsConfig_List.Add(daily as RdsConfig);
                        break;
                    case Utils.ComboBoxHelper.ConfigureStatusRetbackGXTag:
                        StatusRetbackGX_List.Add(daily as StatusRetbackGX);
                        break;
                }
            }
            TableData.TableDataHelper.WriteTable(Enums.TableType.Configure,
                TimeService_List, SetAddress_List, WorkMode_List, MainFrequency_List, Reback_List, DefaltVolume_List,
                RebackPeriod_List, ContentMoniterRetback_List, ContentRealMoniter_List, StatusRetback_List, SoftwareUpGrade_List,
                RdsConfig_List, StatusRetbackGX_List);
            timer.Stop();
        }

        public void AppendDataText(string text)
        {
            richTextData.AppendText(text);
        }

        private void dgvTotal_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvTotal.SelectedRows.Count > 0)
            {
                switch (TotalConfig_List[dgvTotal.SelectedRows[0].Index].B_Daily_cmd_tag)
                {
                    case Utils.ComboBoxHelper.ConfigureTimeServiceTag:
                        tabConfigure.SelectedTab = pageTimeService;
                        dgvTimeService.DataSource = new List<TimeService> { TotalConfig_List[dgvTotal.SelectedRows[0].Index] as TimeService };
                        break;
                    case Utils.ComboBoxHelper.ConfigureSetAddressTag:
                        tabConfigure.SelectedTab = pageSetAddress;
                        dgvSetAddress.DataSource = new List<SetAddress> { TotalConfig_List[dgvTotal.SelectedRows[0].Index] as SetAddress };
                        break;
                    case Utils.ComboBoxHelper.ConfigureWorkModeTag:
                        tabConfigure.SelectedTab = pageWorkMode;
                        dgvWorkMode.DataSource = new List<WorkMode> { TotalConfig_List[dgvTotal.SelectedRows[0].Index] as WorkMode };
                        break;
                    case Utils.ComboBoxHelper.ConfigureMainFrequencyTag:
                        tabConfigure.SelectedTab = pageMainFrequency;
                        dgvMainFrequency.DataSource = new List<MainFrequency> { TotalConfig_List[dgvTotal.SelectedRows[0].Index] as MainFrequency };
                        break;
                    case Utils.ComboBoxHelper.ConfigureRebackTag:
                        tabConfigure.SelectedTab = pageReback;
                        dgvReback.DataSource = new List<Reback> { TotalConfig_List[dgvTotal.SelectedRows[0].Index] as Reback };
                        break;
                    case Utils.ComboBoxHelper.ConfigureDefaltVolumeTag:
                        tabConfigure.SelectedTab = pageDefaltVolume;
                        dgvVolumn.DataSource = new List<DefaltVolume> { TotalConfig_List[dgvTotal.SelectedRows[0].Index] as DefaltVolume };
                        break;
                    case Utils.ComboBoxHelper.ConfigureRebackPeriodTag:
                        tabConfigure.SelectedTab = pageRebackPeriod;
                        dgvRebackPeriod.DataSource = new List<RebackPeriod> { TotalConfig_List[dgvTotal.SelectedRows[0].Index] as RebackPeriod };
                        break;
                    case Utils.ComboBoxHelper.ConfigureContentMoniterRetbackTag:
                        tabConfigure.SelectedTab = pageContentMoniterReback;
                        dgvContentMoniterRetback.DataSource = new List<ContentMoniterRetback> { TotalConfig_List[dgvTotal.SelectedRows[0].Index] as ContentMoniterRetback };
                        break;
                    case Utils.ComboBoxHelper.ConfigureContentRealMoniterTag:
                        tabConfigure.SelectedTab = pageContentRealMoniter;
                        dgvContentRealMoniter.DataSource = new List<ContentRealMoniter> { TotalConfig_List[dgvTotal.SelectedRows[0].Index] as ContentRealMoniter };
                        break;
                    case Utils.ComboBoxHelper.ConfigureStatusRetbackTag:
                        tabConfigure.SelectedTab = pageStatusRetback;
                        dgvStatusRetback.DataSource = new List<StatusRetback> { TotalConfig_List[dgvTotal.SelectedRows[0].Index] as StatusRetback };
                        break;
                    case Utils.ComboBoxHelper.ConfigureSoftwareUpGradeTag:
                        tabConfigure.SelectedTab = pageSoftwareUpGrade;
                        dgvSoftwareUpGrade.DataSource = null;
                        var softData = new List<SoftwareUpGrade> { TotalConfig_List[dgvTotal.SelectedRows[0].Index] as SoftwareUpGrade };
                        InitDgvSoftwareUpGrade(softData[0].DeviceOrderType);
                        dgvSoftwareUpGrade.DataSource = softData;
                        break;
                    case Utils.ComboBoxHelper.ConfigureRdsConfigTag:
                        tabConfigure.SelectedTab = pageRdsConfig;
                        dgvRdsConfig.DataSource = new List<RdsConfig> { TotalConfig_List[dgvTotal.SelectedRows[0].Index] as RdsConfig };
                        break;
                    case Utils.ComboBoxHelper.ConfigureStatusRetbackGXTag:
                        tabConfigure.SelectedTab = pageStatusRetbackGX;
                        dgvStatusRetbackGX.DataSource = new List<StatusRetbackGX> { TotalConfig_List[dgvTotal.SelectedRows[0].Index] as StatusRetbackGX };
                        break;
                }
            }
        }

        private void dgvTotal_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            Rectangle headerRect = e.RowBounds;
            headerRect.Width = dgvTotal.RowHeadersWidth;
            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(System.Globalization.CultureInfo.CurrentUICulture),
                e.InheritedRowStyle.Font, headerRect, e.InheritedRowStyle.ForeColor,
                TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter);
        }

        private void dgvTotal_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                Configure config = dgvTotal.Rows[e.RowIndex].DataBoundItem as Configure;
                if (e.ColumnIndex == ColumnTotalSendState.Index)
                {
                    config.SendState = !config.SendState;
                    //(MdiParent as EBMMain).InitStreamTable();
                }
            }
        }

        #region 添加按钮点击事件

        private void AddConfigureBtn_ConfigureContentMoniterRetbackClick(object sender, EventArgs e)
        {
            AddConfig(Utils.ComboBoxHelper.ConfigureContentMoniterRetbackTag);
        }

        private void AddConfigureBtn_ConfigureDefaltVolumnClick(object sender, EventArgs e)
        {
            AddConfig(Utils.ComboBoxHelper.ConfigureDefaltVolumeTag);
        }

        private void AddConfigureBtn_ConfigureMainFreqClick(object sender, EventArgs e)
        {
            AddConfig(Utils.ComboBoxHelper.ConfigureMainFrequencyTag);
        }

        private void AddConfigureBtn_ConfigurePeriodClick(object sender, EventArgs e)
        {
            AddConfig(Utils.ComboBoxHelper.ConfigureRebackPeriodTag);
        }

        private void AddConfigureBtn_ConfigureRealMoniterClick(object sender, EventArgs e)
        {
            AddConfig(Utils.ComboBoxHelper.ConfigureContentRealMoniterTag);
        }

        private void AddConfigureBtn_ConfigureRebackClick(object sender, EventArgs e)
        {
            AddConfig(Utils.ComboBoxHelper.ConfigureRebackTag);
        }

        private void AddConfigureBtn_ConfigureSetAddressClick(object sender, EventArgs e)
        {
            AddConfig(Utils.ComboBoxHelper.ConfigureSetAddressTag);
        }

        private void AddConfigureBtn_ConfigureStatusRetbackClick(object sender, EventArgs e)
        {
            AddConfig(Utils.ComboBoxHelper.ConfigureStatusRetbackTag);
        }

        private void AddConfigureBtn_ConfigureTimeServiceClick(object sender, EventArgs e)
        {
            AddConfig(Utils.ComboBoxHelper.ConfigureTimeServiceTag);
        }

        private void AddConfigureBtn_ConfigureWorkModeClick(object sender, EventArgs e)
        {
            AddConfig(Utils.ComboBoxHelper.ConfigureWorkModeTag);
        }

        private void AddConfigureBtn_ConfigureSoftwareUpGradeClick(object sender, EventArgs e)
        {
            AddConfig(Utils.ComboBoxHelper.ConfigureSoftwareUpGradeTag);
        }

        private void AddConfigureBtn_ConfigureRdsConfigClick(object sender, EventArgs e)
        {
            AddConfig(Utils.ComboBoxHelper.ConfigureRdsConfigTag);
        }

        private void AddConfigureBtn_ConfigureStatusRetbackGXClick(object sender, EventArgs e)
        {
            AddConfig(Utils.ComboBoxHelper.ConfigureStatusRetbackGXTag);
        }

        private void MenuItemTimeService_Click(object sender, EventArgs e)
        {
            AddConfig(Utils.ComboBoxHelper.ConfigureTimeServiceTag);
        }

        private void MenuItemSetAddress_Click(object sender, EventArgs e)
        {
            AddConfig(Utils.ComboBoxHelper.ConfigureSetAddressTag);
        }

        private void MenuItemWorkMode_Click(object sender, EventArgs e)
        {
            AddConfig(Utils.ComboBoxHelper.ConfigureWorkModeTag);
        }

        private void MenuItemMainFreq_Click(object sender, EventArgs e)
        {
            AddConfig(Utils.ComboBoxHelper.ConfigureMainFrequencyTag);
        }

        private void MenuItemReback_Click(object sender, EventArgs e)
        {
            AddConfig(Utils.ComboBoxHelper.ConfigureRebackTag);
        }

        private void MenuItemDefaultVolumn_Click(object sender, EventArgs e)
        {
            AddConfig(Utils.ComboBoxHelper.ConfigureDefaltVolumeTag);
        }

        private void MenuItemRebackPeriod_Click(object sender, EventArgs e)
        {
            AddConfig(Utils.ComboBoxHelper.ConfigureRebackPeriodTag);
        }

        private void MenuItemContentMoniterRetback_Click(object sender, EventArgs e)
        {
            AddConfig(Utils.ComboBoxHelper.ConfigureContentMoniterRetbackTag);
        }

        private void MenuItemContentRealMoniter_Click(object sender, EventArgs e)
        {
            AddConfig(Utils.ComboBoxHelper.ConfigureContentRealMoniterTag);
        }

        private void MenuItemStatusRetback_Click(object sender, EventArgs e)
        {
            AddConfig(Utils.ComboBoxHelper.ConfigureStatusRetbackTag);
        }

        private void MenuItemSoftwareUpGrade_Click(object sender, EventArgs e)
        {
            AddConfig(Utils.ComboBoxHelper.ConfigureSoftwareUpGradeTag);
        }

        private void MenuItemRdsConfig_Click(object sender, EventArgs e)
        {
            AddConfig(Utils.ComboBoxHelper.ConfigureRdsConfigTag);
        }

        private void MenuItemStatusRetbackGX_Click(object sender, EventArgs e)
        {
            AddConfig(Utils.ComboBoxHelper.ConfigureStatusRetbackGXTag);
        }

        #endregion

        private void dgvContentMoniterRetback_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            //MessageBox.Show(string.Format("列：{0} 行：{1} 数据输入异常，请检查，如需退出编辑请按Esc键", e.ColumnIndex, e.RowIndex));
            e.ThrowException = false;
        }

        private void dgvContentRealMoniter_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            //MessageBox.Show(string.Format("列：{0} 行：{1} 数据输入异常，请检查，如需退出编辑请按Esc键", e.ColumnIndex, e.RowIndex));
            e.ThrowException = false;
        }

        private void checkBoxAllSel_CheckedChanged(object sender, EventArgs e)
        {
            foreach (var config in TotalConfig_List)
            {
                config.SendState = checkBoxAllSel.Checked;
            }
            dgvTotal.Invalidate(dgvTotal.GetColumnDisplayRectangle(ColumnTotalSendState.Index, true));
            //(MdiParent as EBMMain).InitStreamTable();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if ((MdiParent as EBMMain).IsStartStream)
            {
                btnSend.Enabled = false;
                if (GetSendConfigureCmd().Count > 0)
                {
                    (MdiParent as EBMMain).EbMStream.EB_Configure_Table = GetConfigureTable(ref (MdiParent as EBMMain).EbMStream.EB_Configure_Table, false) ? (MdiParent as EBMMain).EbMStream.EB_Configure_Table : null;
                  //  (MdiParent as EBMMain).EbMStream.SignatureCallbackRef = new EBMStream.SignatureCallBackDelegateRef(Calcle.SignatureFunc);//每次在 Initialization()之前调用
                    (MdiParent as EBMMain).EbMStream.Initialization();
                    (MdiParent as EBMMain).UpdateDataText();
                }
                btnSend.Enabled = true;
            }
            else
            {
                MessageBox.Show(this, "应急广播数据流未启动，启动后再试", "提示");
            }
        }

        private void DgvSoftwareUpGrade_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            //防止出现异常提示框，可进行相关错误处理
        }

        /// <summary>
        /// 初始化SoftwareUpGrade表
        /// </summary>
        /// <param name="deviceOrderType"></param>
        private void InitDgvSoftwareUpGrade(DeviceOrderType deviceOrderType)
        {
            Utils.ComboBoxHelper.InitUpGradeModType(ColumnModType, deviceOrderType);
            Utils.ComboBoxHelper.InitUpGradeRate(ColumnRate, deviceOrderType);
            if(deviceOrderType == DeviceOrderType.DVBC)
            {
                ColumnCarrMode.Visible = false;
                ColumnFHMode.Visible = false;
                ColumnILMode.Visible = false;
            }
            else
            {
                ColumnCarrMode.Visible = true;
                ColumnFHMode.Visible = true;
                ColumnILMode.Visible = true;
            }
        }

    }
}
