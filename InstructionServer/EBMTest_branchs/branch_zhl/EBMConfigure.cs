using ControlAstro.Utils;
using EBMTable;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace EBMTest
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

        private BindingCollection<Configure> TotalConfig_List;

        private List<string> ebmId;
        private Timer timer;
        private DateTime oldTime;

        private const byte TimeServiceTag = 1;
        private const byte SetAddressTag = 2;
        private const byte WorkModeTag = 3;
        private const byte MainFrequencyTag = 4;
        private const byte RebackTag = 5;
        private const byte DefaltVolumeTag = 6;
        private const byte RebackPeriodTag = 7;
        private const byte ContentMoniterRetbackTag = 8;
        private const byte ContentRealMoniterTag = 9;
        private const byte StatusRetbackTag = 11;

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
            foreach (TimeService time in TotalConfig_List.Where(q => q.B_Daily_cmd_tag == TimeServiceTag))
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
                    (MdiParent as EBMMain).EbMStream.Initialization();
                    (MdiParent as EBMMain).UpdateDataText();
                    oldTime = date;
                }
                dgvTimeService.Invalidate();
            }
        }

        private void InitDataGridView()
        {
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
            dgvTotal.AutoGenerateColumns = false;
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

            if (TotalConfig_List == null) TotalConfig_List = new BindingCollection<Configure>();

            try
            {
                var jo = TableData.TableDataHelper.ReadTable(Enums.TableType.Configure);
                if (jo != null)
                {
                    TimeService_List = JsonConvert.DeserializeObject<BindingCollection<TimeService>>(jo["0"].ToString());
                    SetAddress_List = JsonConvert.DeserializeObject<BindingCollection<SetAddress>>(jo["1"].ToString());
                    WorkMode_List = JsonConvert.DeserializeObject<BindingCollection<WorkMode>>(jo["2"].ToString());
                    MainFrequency_List = JsonConvert.DeserializeObject<BindingCollection<MainFrequency>>(jo["3"].ToString());
                    Reback_List = JsonConvert.DeserializeObject<BindingCollection<Reback>>(jo["4"].ToString());
                    DefaltVolume_List = JsonConvert.DeserializeObject<BindingCollection<DefaltVolume>>(jo["5"].ToString());
                    RebackPeriod_List = JsonConvert.DeserializeObject<BindingCollection<RebackPeriod>>(jo["6"].ToString());
                    ContentMoniterRetback_List = JsonConvert.DeserializeObject<BindingCollection<ContentMoniterRetback>>(jo["7"].ToString());
                    ContentRealMoniter_List = JsonConvert.DeserializeObject<BindingCollection<ContentRealMoniter>>(jo["8"].ToString());
                    StatusRetback_List = JsonConvert.DeserializeObject<BindingCollection<StatusRetback>>(jo["9"].ToString());
                    foreach (var l in TimeService_List)
                    {
                        TotalConfig_List.Add(l);
                    }
                    foreach (var l in SetAddress_List)
                    {
                        TotalConfig_List.Add(l);
                    }
                    foreach (var l in WorkMode_List)
                    {
                        l.list_Terminal_Address.RemoveRange(l.list_Terminal_Address.Count / 2, l.list_Terminal_Address.Count / 2);
                        TotalConfig_List.Add(l);
                    }
                    foreach (var l in MainFrequency_List)
                    {
                        l.list_Terminal_Address.RemoveRange(l.list_Terminal_Address.Count / 2, l.list_Terminal_Address.Count / 2);
                        TotalConfig_List.Add(l);
                    }
                    foreach (var l in Reback_List)
                    {
                        l.list_Terminal_Address.RemoveRange(l.list_Terminal_Address.Count / 2, l.list_Terminal_Address.Count / 2);
                        TotalConfig_List.Add(l);
                    }
                    foreach (var l in DefaltVolume_List)
                    {
                        l.list_Terminal_Address.RemoveRange(l.list_Terminal_Address.Count / 2, l.list_Terminal_Address.Count / 2);
                        TotalConfig_List.Add(l);
                    }
                    foreach (var l in RebackPeriod_List)
                    {
                        l.list_Terminal_Address.RemoveRange(l.list_Terminal_Address.Count / 2, l.list_Terminal_Address.Count / 2);
                        TotalConfig_List.Add(l);
                    }
                    foreach (var l in ContentMoniterRetback_List)
                    {
                        l.list_Terminal_Address.RemoveRange(l.list_Terminal_Address.Count / 2, l.list_Terminal_Address.Count / 2);
                        TotalConfig_List.Add(l);
                    }
                    foreach (var l in ContentRealMoniter_List)
                    {
                        l.list_Terminal_Address.RemoveRange(l.list_Terminal_Address.Count / 2, l.list_Terminal_Address.Count / 2);
                        TotalConfig_List.Add(l);
                    }
                    foreach (var l in StatusRetback_List)
                    {
                        l.list_Terminal_Address.RemoveRange(l.list_Terminal_Address.Count / 2, l.list_Terminal_Address.Count / 2);
                        TotalConfig_List.Add(l);
                    }
                }
            }
            catch
            {
            }
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

            Utils.ComboBoxHelper.InitRetbackModeType(ColumnRetback_mode);
            Utils.ComboBoxHelper.InitRealRetbackModeType(ColumnRealMonRetMode);

            Utils.ComboBoxHelper.InitFreqQAMType(ColumnFreqQAM);
            Utils.ComboBoxHelper.InitConfigRebackType(ColumnRebackB_reback_type);
            Utils.ComboBoxHelper.InitConfigWorkMode(ColumnWorkTerminal_wordmode);

            Utils.ComboBoxHelper.InitConfigureType(ColumnOrderType);

            ColumnRetbackS_EBM_id.DataSource = ebmId;
            ColumnRealMonS_EBM_Id.DataSource = ebmId;

            checkBoxAllSel.Enabled = TotalConfig_List.Count > 0;
        }

        private void DgvStatusRetback_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                StatusRetback config = (TotalConfig_List[dgvTotal.SelectedRows[0].Index] as StatusRetback);
                if (e.ColumnIndex == ColumnList_Parameter_tag.Index)
                {
                    EBMConfigureDetail form = new EBMConfigureDetail(1, config.list_Terminal_Address);
                    DialogResult result = form.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        config.list_Parameter_tag = form.GetData() as List<byte>;
                    }
                    form.Dispose();
                }
                if (e.ColumnIndex == ColumnStatusRetList_Terminal_Address.Index)
                {
                    EBMConfigureDetail form = new EBMConfigureDetail(0, config.list_Terminal_Address);
                    DialogResult result = form.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        config.list_Terminal_Address = form.GetData() as List<string>;
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
                    EBMConfigureDetail form = new EBMConfigureDetail(0, config.list_Terminal_Address);
                    DialogResult result = form.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        config.list_Terminal_Address = form.GetData() as List<string>;
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
                    EBMConfigureDetail form = new EBMConfigureDetail(0, config.list_Terminal_Address);
                    DialogResult result = form.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        config.list_Terminal_Address = form.GetData() as List<string>;
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
                    EBMConfigureDetail form = new EBMConfigureDetail(0, config.list_Terminal_Address);
                    DialogResult result = form.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        config.list_Terminal_Address = form.GetData() as List<string>;
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
                    EBMConfigureDetail form = new EBMConfigureDetail(0, config.list_Terminal_Address);
                    DialogResult result = form.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        config.list_Terminal_Address = form.GetData() as List<string>;
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
                    EBMConfigureDetail form = new EBMConfigureDetail(0, config.list_Terminal_Address);
                    DialogResult result = form.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        config.list_Terminal_Address = form.GetData() as List<string>;
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
                    EBMConfigureDetail form = new EBMConfigureDetail(0, config.list_Terminal_Address);
                    DialogResult result = form.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        config.list_Terminal_Address = form.GetData() as List<string>;
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
                    EBMConfigureDetail form = new EBMConfigureDetail(0, config.list_Terminal_Address);
                    DialogResult result = form.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        config.list_Terminal_Address = form.GetData() as List<string>;
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
                            case TimeServiceTag:
                                cmd.Add((d as TimeService).Configure.GetCmd());
                                break;
                            case SetAddressTag:
                                cmd.Add((d as SetAddress).Configure.GetCmd());
                                break;
                            case WorkModeTag:
                                cmd.Add((d as WorkMode).Configure.GetCmd());
                                break;
                            case MainFrequencyTag:
                                cmd.Add((d as WorkMode).Configure.GetCmd());
                                break;
                            case RebackTag:
                                cmd.Add((d as WorkMode).Configure.GetCmd());
                                break;
                            case DefaltVolumeTag:
                                cmd.Add((d as WorkMode).Configure.GetCmd());
                                break;
                            case RebackPeriodTag:
                                cmd.Add((d as WorkMode).Configure.GetCmd());
                                break;
                            case ContentMoniterRetbackTag:
                                cmd.Add((d as WorkMode).Configure.GetCmd());
                                break;
                            case ContentRealMoniterTag:
                                cmd.Add((d as WorkMode).Configure.GetCmd());
                                break;
                            case StatusRetbackTag:
                                cmd.Add((d as WorkMode).Configure.GetCmd());
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
                    if (d.SendState && d.B_Daily_cmd_tag == TimeServiceTag)
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
                oldTable.Repeat_times = pnlRepeatTimes.GetRepeatTimes() + 1;
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
            public override byte B_Daily_cmd_tag { get { return 1; } }
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
            public override byte B_Daily_cmd_tag { get { return 2; } }
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
            public override byte B_Daily_cmd_tag { get { return 3; } }
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
            public List<string> list_Terminal_Address
            {
                get { return Configure.list_Terminal_Address; }
                set { Configure.list_Terminal_Address = value; }
            }
            public override string Summary
            {
                get
                {
                    return string.Join("/", Utils.ComboBoxHelper.GetTypeStringValue(Enums.ParamType.ConfigWorkMode, B_Terminal_wordmode),
                        Utils.ComboBoxHelper.GetTypeStringValue(Enums.ParamType.AddressType, B_Address_type),
                        list_Terminal_Address.Count > 1 ? list_Terminal_Address[0] + "..." : "...");
                }
            }
        }

        public class MainFrequency : Configure
        {
            public override byte B_Daily_cmd_tag { get { return 4; } }
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
            public List<string> list_Terminal_Address
            {
                get { return Configure.list_Terminal_Address; }
                set { Configure.list_Terminal_Address = value; }
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
            public override byte B_Daily_cmd_tag { get { return 5; } }
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
            public List<string> list_Terminal_Address
            {
                get { return Configure.list_Terminal_Address; }
                set { Configure.list_Terminal_Address = value; }
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
            public override byte B_Daily_cmd_tag { get { return 6; } }
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
            public List<string> list_Terminal_Address
            {
                get { return Configure.list_Terminal_Address; }
                set { Configure.list_Terminal_Address = value; }
            }
            public override string Summary
            {
                get
                {
                    return string.Join("/", Column, Utils.ComboBoxHelper.GetTypeStringValue(Enums.ParamType.AddressType, B_Address_type),
                        list_Terminal_Address.Count > 1 ? list_Terminal_Address[0] + "..." : "...");
                }
            }
        }

        public class RebackPeriod : Configure
        {
            public override byte B_Daily_cmd_tag { get { return 7; } }

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
            public List<string> list_Terminal_Address
            {
                get { return Configure.list_Terminal_Address; }
                set { Configure.list_Terminal_Address = value; }
            }
            public override string Summary
            {
                get
                {
                    return string.Join("/", reback_period, Utils.ComboBoxHelper.GetTypeStringValue(Enums.ParamType.AddressType, B_Address_type), 
                        list_Terminal_Address.Count > 1 ? list_Terminal_Address[0] + "..." : "...");
                }
            }
        }

        public class ContentMoniterRetback : Configure
        {
            public override byte B_Daily_cmd_tag { get { return 8; } }
            public EBConfigureContentMoniterRetback Configure { get; set; }
            public int Start_package_index
            {
                get { return Configure.Start_package_index; }
                set { Configure.Start_package_index = value; }
            }
            public string S_EBM_id
            {
                get { return Configure.S_EBM_id; }
                set { Configure.S_EBM_id = value; }
            }
            public string S_File_id
            {
                get { return Configure.S_File_id; }
                set { Configure.S_File_id = value; }
            }
            public short Retback_mode
            {
                get { return Configure.Retback_mode; }
                set { Configure.Retback_mode = value; }
            }
            public byte B_Address_type
            {
                get { return Configure.B_Address_type; }
                set { Configure.B_Address_type = value; }
            }
            public List<string> list_Terminal_Address
            {
                get { return Configure.list_Terminal_Address; }
                set { Configure.list_Terminal_Address = value; }
            }
            public override string Summary
            {
                get
                {
                    return string.Join("/", S_EBM_id, S_File_id, Utils.ComboBoxHelper.GetTypeStringValue(Enums.ParamType.ConfigRetbackMode, Retback_mode), Start_package_index,
                        Utils.ComboBoxHelper.GetTypeStringValue(Enums.ParamType.AddressType, B_Address_type), "...");
                }
            }
        }

        public class ContentRealMoniter : Configure
        {
            public override byte B_Daily_cmd_tag { get { return 9; } }
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
            public List<string> list_Terminal_Address
            {
                get { return Configure.list_Terminal_Address; }
                set { Configure.list_Terminal_Address = value; }
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
            public override byte B_Daily_cmd_tag { get { return 11; } }
            public EBConfigureStatusRetback Configure { get; set; }
            public byte B_Address_type
            {
                get { return Configure.B_Address_type; }
                set { Configure.B_Address_type = value; }
            }
            public List<byte> list_Parameter_tag
            {
                get { return Configure.list_Parameter_tag; }
                set { Configure.list_Parameter_tag = value; }
            }
            public List<string> list_Terminal_Address
            {
                get { return Configure.list_Terminal_Address; }
                set { Configure.list_Terminal_Address = value; }
            }
            public override string Summary
            {
                get
                {
                    return string.Join("/", Utils.ComboBoxHelper.GetTypeStringValue(Enums.ParamType.AddressType, B_Address_type),
                        list_Parameter_tag.Count > 0 ? Utils.ComboBoxHelper.GetTypeStringValue(Enums.ParamType.ConfigStatusParameterTag, list_Parameter_tag[0]) + "..." : "...");
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
                TotalConfig_List.RemoveAt(dgvTotal.SelectedRows[0].Index);
            }
            else
            {
                MessageBox.Show("未选中任何索引");
            }
            checkBoxAllSel.Enabled = TotalConfig_List.Count > 0;
        }

        private void AddConfig(byte tag)
        {
            switch (tag)
            {
                case TimeServiceTag:
                    EBMConfigureInfo infoTime = new EBMConfigureInfo(Enums.OperateType.Add, tag);
                    DialogResult resultTime = infoTime.ShowDialog();
                    if (resultTime == DialogResult.OK && infoTime.Configure != null)
                    {
                        TotalConfig_List.Add(infoTime.Configure);
                    }
                    infoTime.Dispose();
                    break;
                case SetAddressTag:
                    EBMConfigureInfo infoSet = new EBMConfigureInfo(Enums.OperateType.Add, tag);
                    DialogResult resultSet = infoSet.ShowDialog();
                    if (resultSet == DialogResult.OK && infoSet.Configure != null)
                    {
                        TotalConfig_List.Add(infoSet.Configure);
                    }
                    infoSet.Dispose();
                    break;
                case WorkModeTag:
                    EBMConfigureInfo infoWork = new EBMConfigureInfo(Enums.OperateType.Add, tag);
                    DialogResult resultWork = infoWork.ShowDialog();
                    if (resultWork == DialogResult.OK && infoWork.Configure != null)
                    {
                        TotalConfig_List.Add(infoWork.Configure);
                    }
                    infoWork.Dispose();
                    break;
                case MainFrequencyTag:
                    EBMConfigureInfo infoFreq = new EBMConfigureInfo(Enums.OperateType.Add, tag);
                    DialogResult resultFreq = infoFreq.ShowDialog();
                    if (resultFreq == DialogResult.OK && infoFreq.Configure != null)
                    {
                        TotalConfig_List.Add(infoFreq.Configure);
                    }
                    infoFreq.Dispose();
                    break;
                case RebackTag:
                    EBMConfigureInfo infoReback = new EBMConfigureInfo(Enums.OperateType.Add, tag);
                    DialogResult resultReback = infoReback.ShowDialog();
                    if (resultReback == DialogResult.OK && infoReback.Configure != null)
                    {
                        TotalConfig_List.Add(infoReback.Configure);
                    }
                    infoReback.Dispose();
                    break;
                case DefaltVolumeTag:
                    EBMConfigureInfo infoVolumn = new EBMConfigureInfo(Enums.OperateType.Add, tag);
                    DialogResult resultVolumn = infoVolumn.ShowDialog();
                    if (resultVolumn == DialogResult.OK && infoVolumn.Configure != null)
                    {
                        TotalConfig_List.Add(infoVolumn.Configure);
                    }
                    infoVolumn.Dispose();
                    break;
                case RebackPeriodTag:
                    EBMConfigureInfo infoPeriod = new EBMConfigureInfo(Enums.OperateType.Add, tag);
                    DialogResult resultPeriod = infoPeriod.ShowDialog();
                    if (resultPeriod == DialogResult.OK && infoPeriod.Configure != null)
                    {
                        TotalConfig_List.Add(infoPeriod.Configure);
                    }
                    infoPeriod.Dispose();
                    break;
                case ContentMoniterRetbackTag:
                    EBMConfigureInfo infoContentRetback = new EBMConfigureInfo(Enums.OperateType.Add, tag, ebmId);
                    DialogResult resultContentRetback = infoContentRetback.ShowDialog();
                    if (resultContentRetback == DialogResult.OK && infoContentRetback.Configure != null)
                    {
                        TotalConfig_List.Add(infoContentRetback.Configure);
                    }
                    infoContentRetback.Dispose();
                    break;
                case ContentRealMoniterTag:
                    EBMConfigureInfo infoRealMoniter = new EBMConfigureInfo(Enums.OperateType.Add, tag, ebmId);
                    DialogResult resultRealMoniter = infoRealMoniter.ShowDialog();
                    if (resultRealMoniter == DialogResult.OK && infoRealMoniter.Configure != null)
                    {
                        TotalConfig_List.Add(infoRealMoniter.Configure);
                    }
                    infoRealMoniter.Dispose();
                    break;
                case StatusRetbackTag:
                    EBMConfigureInfo infoStatusRetback = new EBMConfigureInfo(Enums.OperateType.Add, tag);
                    DialogResult resultStatusRetback = infoStatusRetback.ShowDialog();
                    if (resultStatusRetback == DialogResult.OK && infoStatusRetback.Configure != null)
                    {
                        TotalConfig_List.Add(infoStatusRetback.Configure);
                    }
                    infoStatusRetback.Dispose();
                    break;
            }
            TotalConfig_List.Sort(TypeDescriptor.GetProperties(typeof(Configure)).Find("B_Daily_cmd_tag", false), ListSortDirection.Ascending);
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
            foreach (var daily in TotalConfig_List)
            {
                switch (daily.B_Daily_cmd_tag)
                {
                    case TimeServiceTag:
                        TimeService_List.Add(daily as TimeService);
                        break;
                    case SetAddressTag:
                        SetAddress_List.Add(daily as SetAddress);
                        break;
                    case WorkModeTag:
                        WorkMode_List.Add(daily as WorkMode);
                        break;
                    case MainFrequencyTag:
                        MainFrequency_List.Add(daily as MainFrequency);
                        break;
                    case RebackTag:
                        Reback_List.Add(daily as Reback);
                        break;
                    case DefaltVolumeTag:
                        DefaltVolume_List.Add(daily as DefaltVolume);
                        break;
                    case RebackPeriodTag:
                        RebackPeriod_List.Add(daily as RebackPeriod);
                        break;
                    case ContentMoniterRetbackTag:
                        ContentMoniterRetback_List.Add(daily as ContentMoniterRetback);
                        break;
                    case ContentRealMoniterTag:
                        ContentRealMoniter_List.Add(daily as ContentRealMoniter);
                        break;
                    case StatusRetbackTag:
                        StatusRetback_List.Add(daily as StatusRetback);
                        break;
                }
            }
            TableData.TableDataHelper.WriteTable(Enums.TableType.Configure,
                TimeService_List, SetAddress_List, WorkMode_List, MainFrequency_List, Reback_List, DefaltVolume_List,
                RebackPeriod_List, ContentMoniterRetback_List, ContentRealMoniter_List, StatusRetback_List);
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
                    case TimeServiceTag:
                        tabConfigure.SelectedTab = pageTimeService;
                        dgvTimeService.DataSource = new List<TimeService> { TotalConfig_List[dgvTotal.SelectedRows[0].Index] as TimeService };
                        break;
                    case SetAddressTag:
                        tabConfigure.SelectedTab = pageSetAddress;
                        dgvSetAddress.DataSource = new List<SetAddress> { TotalConfig_List[dgvTotal.SelectedRows[0].Index] as SetAddress };
                        break;
                    case WorkModeTag:
                        tabConfigure.SelectedTab = pageWorkMode;
                        dgvWorkMode.DataSource = new List<WorkMode> { TotalConfig_List[dgvTotal.SelectedRows[0].Index] as WorkMode };
                        break;
                    case MainFrequencyTag:
                        tabConfigure.SelectedTab = pageMainFrequency;
                        dgvMainFrequency.DataSource = new List<MainFrequency> { TotalConfig_List[dgvTotal.SelectedRows[0].Index] as MainFrequency };
                        break;
                    case RebackTag:
                        tabConfigure.SelectedTab = pageReback;
                        dgvReback.DataSource = new List<Reback> { TotalConfig_List[dgvTotal.SelectedRows[0].Index] as Reback };
                        break;
                    case DefaltVolumeTag:
                        tabConfigure.SelectedTab = pageDefaltVolume;
                        dgvVolumn.DataSource = new List<DefaltVolume> { TotalConfig_List[dgvTotal.SelectedRows[0].Index] as DefaltVolume };
                        break;
                    case RebackPeriodTag:
                        tabConfigure.SelectedTab = pageRebackPeriod;
                        dgvRebackPeriod.DataSource = new List<RebackPeriod> { TotalConfig_List[dgvTotal.SelectedRows[0].Index] as RebackPeriod };
                        break;
                    case ContentMoniterRetbackTag:
                        tabConfigure.SelectedTab = pageContentMoniterReback;
                        dgvContentMoniterRetback.DataSource = new List<ContentMoniterRetback> { TotalConfig_List[dgvTotal.SelectedRows[0].Index] as ContentMoniterRetback };
                        break;
                    case ContentRealMoniterTag:
                        tabConfigure.SelectedTab = pageContentRealMoniter;
                        dgvContentRealMoniter.DataSource = new List<ContentRealMoniter> { TotalConfig_List[dgvTotal.SelectedRows[0].Index] as ContentRealMoniter };
                        break;
                    case StatusRetbackTag:
                        tabConfigure.SelectedTab = pageStatusRetback;
                        dgvStatusRetback.DataSource = new List<StatusRetback> { TotalConfig_List[dgvTotal.SelectedRows[0].Index] as StatusRetback };
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
            AddConfig(ContentMoniterRetbackTag);
        }

        private void AddConfigureBtn_ConfigureDefaltVolumnClick(object sender, EventArgs e)
        {
            AddConfig(DefaltVolumeTag);
        }

        private void AddConfigureBtn_ConfigureMainFreqClick(object sender, EventArgs e)
        {
            AddConfig(MainFrequencyTag);
        }

        private void AddConfigureBtn_ConfigurePeriodClick(object sender, EventArgs e)
        {
            AddConfig(RebackPeriodTag);
        }

        private void AddConfigureBtn_ConfigureRealMoniterClick(object sender, EventArgs e)
        {
            AddConfig(ContentRealMoniterTag);
        }

        private void AddConfigureBtn_ConfigureRebackClick(object sender, EventArgs e)
        {
            AddConfig(RebackTag);
        }

        private void AddConfigureBtn_ConfigureSetAddressClick(object sender, EventArgs e)
        {
            AddConfig(SetAddressTag);
        }

        private void AddConfigureBtn_ConfigureStatusRetbackClick(object sender, EventArgs e)
        {
            AddConfig(StatusRetbackTag);
        }

        private void AddConfigureBtn_ConfigureTimeServiceClick(object sender, EventArgs e)
        {
            AddConfig(TimeServiceTag);
        }

        private void AddConfigureBtn_ConfigureWorkModeClick(object sender, EventArgs e)
        {
            AddConfig(WorkModeTag);
        }

        private void MenuItemTimeService_Click(object sender, EventArgs e)
        {
            AddConfig(TimeServiceTag);
        }

        private void MenuItemSetAddress_Click(object sender, EventArgs e)
        {
            AddConfig(SetAddressTag);
        }

        private void MenuItemWorkMode_Click(object sender, EventArgs e)
        {
            AddConfig(WorkModeTag);
        }

        private void MenuItemMainFreq_Click(object sender, EventArgs e)
        {
            AddConfig(MainFrequencyTag);
        }

        private void MenuItemReback_Click(object sender, EventArgs e)
        {
            AddConfig(RebackTag);
        }

        private void MenuItemDefaultVolumn_Click(object sender, EventArgs e)
        {
            AddConfig(DefaltVolumeTag);
        }

        private void MenuItemRebackPeriod_Click(object sender, EventArgs e)
        {
            AddConfig(RebackPeriodTag);
        }

        private void MenuItemContentMoniterRetback_Click(object sender, EventArgs e)
        {
            AddConfig(ContentMoniterRetbackTag);
        }

        private void MenuItemContentRealMoniter_Click(object sender, EventArgs e)
        {
            AddConfig(ContentRealMoniterTag);
        }

        private void MenuItemStatusRetback_Click(object sender, EventArgs e)
        {
            AddConfig(StatusRetbackTag);
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
                if (GetSendConfigureCmd().Count > 0)
                {
                    (MdiParent as EBMMain).EbMStream.EB_Configure_Table = GetConfigureTable(ref (MdiParent as EBMMain).EbMStream.EB_Configure_Table, false) ? (MdiParent as EBMMain).EbMStream.EB_Configure_Table : null;
                    (MdiParent as EBMMain).EbMStream.Initialization();
                    (MdiParent as EBMMain).UpdateDataText();
                }
            }
            else
            {
                MessageBox.Show(this, "应急广播数据流未启动，启动后再试", "提示");
            }
        }

    }
}
