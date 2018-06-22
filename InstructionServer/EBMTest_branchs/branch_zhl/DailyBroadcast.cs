using ControlAstro.Utils;
using EBMTable;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace EBMTest
{
    public partial class DailyBroadcast : Form
    {
        private BindingCollection<ChangeProgram> ChangeProgram_List;
        //private BindingCollection<StopPorgram> StopPorgram_List;
        private BindingCollection<PlayCtrl> PlayCtrl_List;
        private BindingCollection<OutSwitch> OutSwitch_List;
        private BindingCollection<RdsTransfer> RdsTransfer_List;

        private BindingCollection<DailyProgram> TotalConfig_List;

        private const byte ChangeProgramTag = 1;
        private const byte StopProgramTag = 2;
        private const byte PlayCtrlTag = 3;
        private const byte OutSwitchTag = 4;
        private const byte RdsTransferTag = 5;

        public DailyBroadcast()
        {
            InitializeComponent();

            InitProgram();
            InitBroadcastType();

            Load += DailyBroadcast_Load; ;
        }

        private void DailyBroadcast_Load(object sender, EventArgs e)
        {
            (MdiParent as EBMMain).AdminAccountChanged += DailyBroadcast_AdminAccountChanged; ;
            SetAdminLimit((MdiParent as EBMMain).AdminAccount);
        }

        private void DailyBroadcast_AdminAccountChanged(object sender, AdminAccountEventArgs e)
        {
            SetAdminLimit(e.AdminAccount);
        }

        private void SetAdminLimit(bool admin)
        {
            MenuItemAdd.Visible = admin;
            MenuItemDel.Visible = admin;
            //MenuItemInfo.Visible = admin;
            dgvChangeProgram.ReadOnly = !admin;
            dgvOutSwitch.ReadOnly = !admin;
            dgvPlayCtrl.ReadOnly = !admin;
            dgvRdsTransfer.ReadOnly = !admin;
            AddDailyBroadcastBtn.Visible = admin;
            lblBlank.Visible = admin;
            Columnlist_Terminal_Address.Text = admin ? "编辑" : "查看";
            ColumnOutSwitchTerminalAddress.Text = admin ? "编辑" : "查看";
            ColumnPlayCtrlTerminalAddress.Text = admin ? "编辑" : "查看";
            ColumnRdsAddressList.Text = admin ? "编辑" : "查看";
        }

        private void InitBroadcastType()
        {
            Utils.ComboBoxHelper.InitBroadcastType(ColumnOrderType);

            Utils.ComboBoxHelper.InitAddressType(ColumnB_Address_type);
            Utils.ComboBoxHelper.InitAddressType(ColumnStopProgramAddressType);
            Utils.ComboBoxHelper.InitAddressType(ColumnPlayCtrlB_Address_type);
            Utils.ComboBoxHelper.InitAddressType(ColumnOutSwitchB_Address_type);
            Utils.ComboBoxHelper.InitAddressType(ColumnRdsAddressType);

            Utils.ComboBoxHelper.InitOutSwitchType(ColumnOutSwitchB_Switch_status);
            Utils.ComboBoxHelper.InitRdsTransferDailyType(ColumnRdsDaily);

            checkBoxAllSel.Enabled = TotalConfig_List.Count > 0;
        }

        private void InitProgram()
        {
            dgvChangeProgram.AutoGenerateColumns = false;
            dgvStopProgram.AutoGenerateColumns = false;
            dgvPlayCtrl.AutoGenerateColumns = false;
            dgvOutSwitch.AutoGenerateColumns = false;
            dgvRdsTransfer.AutoGenerateColumns = false;
            dgvTotal.AutoGenerateColumns = false;
            if (TotalConfig_List == null) TotalConfig_List = new BindingCollection<DailyProgram>();
            if (ChangeProgram_List == null) ChangeProgram_List = new BindingCollection<ChangeProgram>();
            if (PlayCtrl_List == null) PlayCtrl_List = new BindingCollection<PlayCtrl>();
            if (OutSwitch_List == null) OutSwitch_List = new BindingCollection<OutSwitch>();
            if (RdsTransfer_List == null) RdsTransfer_List = new BindingCollection<RdsTransfer>();

            dgvTotal.TopLeftHeaderCell.Value = "指令序号";
            dgvTotal.TopLeftHeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            try
            {
                JObject jo = TableData.TableDataHelper.ReadTable(Enums.TableType.DailyBroadcast);
                if (jo != null)
                {
                    try
                    {
                        ChangeProgram_List = JsonConvert.DeserializeObject<BindingCollection<ChangeProgram>>(jo["0"].ToString());
                        PlayCtrl_List = JsonConvert.DeserializeObject<BindingCollection<PlayCtrl>>(jo["1"].ToString());
                        OutSwitch_List = JsonConvert.DeserializeObject<BindingCollection<OutSwitch>>(jo["2"].ToString());
                        RdsTransfer_List = JsonConvert.DeserializeObject<BindingCollection<RdsTransfer>>(jo["3"].ToString());
                    }
                    catch
                    {
                    }
                    foreach (var l in ChangeProgram_List)
                    {
                        l.list_Terminal_Address.RemoveRange(l.list_Terminal_Address.Count / 2, l.list_Terminal_Address.Count / 2);
                        l.BroadcastStatus = "已停止";
                        TotalConfig_List.Add(l);
                    }
                    foreach (var l in PlayCtrl_List)
                    {
                        l.list_Terminal_Address.RemoveRange(l.list_Terminal_Address.Count / 2, l.list_Terminal_Address.Count / 2);
                        TotalConfig_List.Add(l);
                    }
                    foreach (var l in OutSwitch_List)
                    {
                        l.list_Terminal_Address.RemoveRange(l.list_Terminal_Address.Count / 2, l.list_Terminal_Address.Count / 2);
                        TotalConfig_List.Add(l);
                    }
                    foreach (var l in RdsTransfer_List)
                    {
                        l.list_Terminal_Address.RemoveRange(l.list_Terminal_Address.Count / 2, l.list_Terminal_Address.Count / 2);
                        TotalConfig_List.Add(l);
                    }
                    dgvTotal.DataSource = TotalConfig_List;
                    return;
                }

#if DEBUG
                DailyCmdChangeProgram dailyChangeProgram = new DailyCmdChangeProgram();
                dailyChangeProgram.NetID = 1;
                dailyChangeProgram.TSID = 1;
                dailyChangeProgram.ServiceID = 1;
                dailyChangeProgram.PCR_PID = 0x1fff;
                dailyChangeProgram.Program_PID = 5001;
                dailyChangeProgram.Priority = 1;
                dailyChangeProgram.Volume = 100;
                dailyChangeProgram.EndTime = new DateTime(2017, 6, 12, 17, 0, 0);
                dailyChangeProgram.B_Address_type = 1;
                dailyChangeProgram.list_Terminal_Address = new List<string>();
                dailyChangeProgram.list_Terminal_Address.Add("120000000000000001");

                ChangeProgram_List.Add(new ChangeProgram
                {
                    Program = dailyChangeProgram,
                });
#endif
            }
            catch
            {
                File.Delete(ConfigurationManager.AppSettings["DailyBroadcastPath"]);
                TotalConfig_List = new BindingCollection<DailyProgram>();
                dgvTotal.DataSource = TotalConfig_List;
                if (ChangeProgram_List == null) ChangeProgram_List = new BindingCollection<ChangeProgram>();
                if (PlayCtrl_List == null) PlayCtrl_List = new BindingCollection<PlayCtrl>();
                if (OutSwitch_List == null) OutSwitch_List = new BindingCollection<OutSwitch>();
            }
        }

        private List<DailyCmd> GetSendDailyCmd()
        {
            try
            {
                List<DailyCmd> daily = new List<DailyCmd>();
                foreach (var d in TotalConfig_List)
                {
                    if (d.SendState)
                    {
                        switch(d.B_Daily_cmd_tag)
                        {
                            case ChangeProgramTag:
                                daily.Add((d as ChangeProgram).Program.GetCmd());
                                break;
                            case PlayCtrlTag:
                                daily.Add((d as PlayCtrl).Program.GetCmd());
                                break;
                            case OutSwitchTag:
                                daily.Add((d as OutSwitch).Program.GetCmd());
                                break;
                            case RdsTransferTag:
                                daily.Add((d as RdsTransfer).Program.GetCmd());
                                break;
                        }
                    }
                }
                //switch ((byte)cbBoxBroadcast.SelectedValue)
                //{
                //    case 1:
                //        foreach (var d in ChangeProgram_List)
                //        {
                //            if (d.SendState)
                //            {
                //                daily.Add(d.Program.GetCmd());
                //            }
                //        }
                //        break;
                //    case 2:
                //        foreach (var d in StopPorgram_List)
                //        {
                //            if (d.SendState)
                //            {
                //                daily.Add(d.Program.GetCmd());
                //            }
                //        }
                //        break;
                //    case 3:
                //        foreach (var d in PlayCtrl_List)
                //        {
                //            if (d.SendState)
                //            {
                //                daily.Add(d.Program.GetCmd());
                //            }
                //        }
                //        break;
                //    case 4:
                //        foreach (var d in OutSwitch_List)
                //        {
                //            if (d.SendState)
                //            {
                //                daily.Add(d.Program.GetCmd());
                //            }
                //        }
                //        break;
                //}

                return daily;
            }
            catch
            {
                return null;
            }
        }

        public bool GetDailyBroadcastTable(ref DailyBroadcastTable oldTable)
        {
            try
            {
                List<DailyCmd> daily = GetSendDailyCmd();
                if (daily == null || daily.Count == 0)
                {
                    if (oldTable != null) oldTable.list_daily_cmd = null;
                    return false;
                }
                if (oldTable == null)
                {
                    oldTable = new DailyBroadcastTable();
                    oldTable.Table_id = 0xfa;
                    oldTable.Table_id_extension = 0;
                }
                oldTable.list_daily_cmd = daily;
                oldTable.Repeat_times = pnlRepeatTimes.GetRepeatTimes();
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void dgvChangeProgram_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                ChangeProgram pro = (TotalConfig_List[dgvTotal.SelectedRows[0].Index] as ChangeProgram);
                if (e.ColumnIndex == Columnlist_Terminal_Address.Index)
                {
                    DailyBroadcastTerAddressInfo detail = new DailyBroadcastTerAddressInfo(pro.list_Terminal_Address, (MdiParent as EBMMain).AdminAccount);
                    DialogResult result = detail.ShowDialog();
                    if (result == DialogResult.OK && (MdiParent as EBMMain).AdminAccount)
                    {
                        pro.list_Terminal_Address = detail.GetData() as List<string>;
                    }
                    detail.Dispose();
                }
                if(e.ColumnIndex == ColumnShouldStop.Index)
                {
                    ChanngeProgramState(e, pro);
                }
            }
        }

        private void ChanngeProgramState(DataGridViewCellEventArgs e, ChangeProgram pro)
        {
            if ((MdiParent as EBMMain).IsStartStream)
            {
                if ((string)dgvChangeProgram[e.ColumnIndex, e.RowIndex].Value == "停止广播")
                {
                    DailyBroadcastTable table = (MdiParent as EBMMain).EbMStream.Daily_Broadcast_Table;
                    if (table == null) GetDailyBroadcastTable(ref table);
                    List<DailyCmd> list = table.list_daily_cmd;
                    foreach (var l in list)
                    {
                        if (l.B_Daily_cmd_tag == 1)
                        {
                            if ((short)((l.Br_Daily_cmd_char[8] << 8) + l.Br_Daily_cmd_char[9]) == pro.Program_PID)
                            {
                                list.Remove(l);
                                break;
                            }
                        }
                    }
                    DailyCmdProgramStop stop = new DailyCmdProgramStop
                    {
                        B_Address_type = pro.B_Address_type,
                        list_Terminal_Address = pro.list_Terminal_Address,
                        NetID = pro.NetID,
                        PCR_PID = Convert.ToInt16(pro.PCR_PID),
                        Program_PID = pro.Program_PID,
                        ServiceID = pro.ServiceID,
                        TSID = pro.TSID,
                    };
                    list.Add(stop.GetCmd());
                    table.list_daily_cmd = list;
                    (MdiParent as EBMMain).EbMStream.Daily_Broadcast_Table = table;
                    (MdiParent as EBMMain).EbMStream.Initialization();
                    (MdiParent as EBMMain).UpdateDataText();
                    dgvChangeProgram[e.ColumnIndex, e.RowIndex].Value = "开始广播";
                    pro.BroadcastStatus = "已停止";
                }
                else if ((string)dgvChangeProgram[e.ColumnIndex, e.RowIndex].Value == "开始广播")
                {
                    pro.SendState = true;
                    DailyBroadcastTable table = (MdiParent as EBMMain).EbMStream.Daily_Broadcast_Table;
                    if (table == null) GetDailyBroadcastTable(ref table);
                    List<DailyCmd> list = table.list_daily_cmd;
                    foreach (var l in list)
                    {
                        if (l.B_Daily_cmd_tag == 2)
                        {
                            if ((short)((l.Br_Daily_cmd_char[8] << 8) + l.Br_Daily_cmd_char[9]) == pro.Program_PID)
                            {
                                list.Remove(l);
                                break;
                            }
                        }
                    }
                    list.Add(pro.Program.GetCmd());
                    table.list_daily_cmd = list;
                    (MdiParent as EBMMain).EbMStream.Daily_Broadcast_Table = table;
                    (MdiParent as EBMMain).EbMStream.Initialization();
                    dgvChangeProgram[e.ColumnIndex, e.RowIndex].Value = "停止广播";
                    pro.BroadcastStatus = "正在广播";
                }
            }
            dgvChangeProgram.Invalidate(dgvChangeProgram.GetRowDisplayRectangle(e.RowIndex, true));
        }

        private void dgvPlayCtrl_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                PlayCtrl pro = (TotalConfig_List[dgvTotal.SelectedRows[0].Index] as PlayCtrl);
                if (e.ColumnIndex == ColumnPlayCtrlTerminalAddress.Index)
                {
                    DailyBroadcastTerAddressInfo detail = new DailyBroadcastTerAddressInfo(pro.list_Terminal_Address, (MdiParent as EBMMain).AdminAccount);
                    DialogResult result = detail.ShowDialog();
                    if (result == DialogResult.OK && (MdiParent as EBMMain).AdminAccount)
                    {
                        pro.list_Terminal_Address = detail.GetData() as List<string>;
                    }
                    detail.Dispose();
                    dgvPlayCtrl.Invalidate();
                }
            }
        }

        private void dgvOutSwitch_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                OutSwitch pro = (TotalConfig_List[dgvTotal.SelectedRows[0].Index] as OutSwitch);
                if (e.ColumnIndex == ColumnOutSwitchTerminalAddress.Index)
                {
                    DailyBroadcastTerAddressInfo detail = new DailyBroadcastTerAddressInfo(pro.list_Terminal_Address, (MdiParent as EBMMain).AdminAccount);
                    DialogResult result = detail.ShowDialog();
                    if (result == DialogResult.OK && (MdiParent as EBMMain).AdminAccount)
                    {
                        pro.list_Terminal_Address = detail.GetData() as List<string>;
                    }
                    detail.Dispose();
                    dgvOutSwitch.Invalidate();
                }
            }
        }

        private void dgvRdsTransfer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                RdsTransfer pro = (TotalConfig_List[dgvTotal.SelectedRows[0].Index] as RdsTransfer);
                if (e.ColumnIndex == ColumnRdsAddressList.Index)
                {
                    DailyBroadcastTerAddressInfo detail = new DailyBroadcastTerAddressInfo(pro.list_Terminal_Address, (MdiParent as EBMMain).AdminAccount);
                    DialogResult result = detail.ShowDialog();
                    if (result == DialogResult.OK && (MdiParent as EBMMain).AdminAccount)
                    {
                        pro.list_Terminal_Address = detail.GetData() as List<string>;
                    }
                    detail.Dispose();
                    dgvRdsTransfer.Invalidate();
                }
            }
        }

        private void MenuStripDailyBroadcast_Opening(object sender, CancelEventArgs e)
        {
            //MenuItemInfo.Enabled = dgvTotal.RowCount > 0;
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
            InfoDaily();
        }

        private void MenuItemDel_Click(object sender, EventArgs e)
        {
            DelDaily();
        }

        private void ToolStripMenuItemChangeProgram_Click(object sender, EventArgs e)
        {
            AddDaily(ChangeProgramTag);
        }

        private void ToolStripMenuItemPlayCtrl_Click(object sender, EventArgs e)
        {
            AddDaily(PlayCtrlTag);
        }

        private void ToolStripMenuItemOutSwitch_Click(object sender, EventArgs e)
        {
            AddDaily(OutSwitchTag);
        }

        private void ToolStripMenuItemRdsTransfer_Click(object sender, EventArgs e)
        {
            AddDaily(RdsTransferTag);
        }

        #region 增删

        private void InfoDaily()
        {
            if (dgvTotal.SelectedRows.Count > 0)
            {
                new DailyBroadcastInfo(Enums.OperateType.Info, TotalConfig_List[dgvTotal.SelectedRows[0].Index].B_Daily_cmd_tag,
                    TotalConfig_List[dgvTotal.SelectedRows[0].Index]).ShowDialog();
            }
            else
            {
                MessageBox.Show("未选中任何索引");
            }
        }

        private void AddDaily(byte tag)
        {
            switch (tag)
            {
                case ChangeProgramTag:
                    DailyBroadcastInfo infoChange = new DailyBroadcastInfo(Enums.OperateType.Add, tag);
                    DialogResult resultChange = infoChange.ShowDialog();
                    if (resultChange == DialogResult.OK && infoChange.Program != null)
                    {
                        TotalConfig_List.Add(infoChange.Program);
                    }
                    infoChange.Dispose();
                    break;
                case StopProgramTag:
                    DailyBroadcastInfo infoStop = new DailyBroadcastInfo(Enums.OperateType.Add, tag);
                    DialogResult resultStop = infoStop.ShowDialog();
                    if (resultStop == DialogResult.OK && infoStop.Program != null)
                    {
                        TotalConfig_List.Add(infoStop.Program);
                    }
                    infoStop.Dispose();
                    break;
                case PlayCtrlTag:
                    DailyBroadcastInfo infoPlay = new DailyBroadcastInfo(Enums.OperateType.Add, tag);
                    DialogResult resultPlay = infoPlay.ShowDialog();
                    if (resultPlay == DialogResult.OK && infoPlay.Program != null)
                    {
                        TotalConfig_List.Add(infoPlay.Program);
                    }
                    infoPlay.Dispose();
                    break;
                case OutSwitchTag:
                    DailyBroadcastInfo infoOut = new DailyBroadcastInfo(Enums.OperateType.Add, tag);
                    DialogResult resultOut = infoOut.ShowDialog();
                    if (resultOut == DialogResult.OK && infoOut.Program != null)
                    {
                        TotalConfig_List.Add(infoOut.Program);
                    }
                    infoOut.Dispose();
                    break;
                case RdsTransferTag:
                    DailyBroadcastInfo infoRds = new DailyBroadcastInfo(Enums.OperateType.Add, tag);
                    DialogResult resultRds = infoRds.ShowDialog();
                    if (resultRds == DialogResult.OK && infoRds.Program != null)
                    {
                        TotalConfig_List.Add(infoRds.Program);
                    }
                    infoRds.Dispose();
                    break;
            }
            TotalConfig_List.Sort(TypeDescriptor.GetProperties(typeof(DailyProgram)).Find("B_Daily_cmd_tag", false), ListSortDirection.Ascending);
            checkBoxAllSel.Enabled = TotalConfig_List.Count > 0;
        }

        private void DelDaily()
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

        #endregion

        #region 内建类

        [Serializable]
        public class ChangeProgram : DailyProgram
        {
            public override byte B_Daily_cmd_tag { get { return ChangeProgramTag; } }
            public DailyCmdChangeProgram Program { get; set; }
            public byte B_Address_type
            {
                get { return Program.B_Address_type; }
                set { Program.B_Address_type = value; }
            }
            public string EndTime
            {
                get { return Program.EndTime.ToString(); }
                set { Program.EndTime = DateTime.Parse(value); }
            }
            public List<string> list_Terminal_Address
            {
                get { return Program.list_Terminal_Address; }
                set { Program.list_Terminal_Address = value; }
            }
            public short NetID
            {
                get { return Program.NetID; }
                set { Program.NetID = value; }
            }
            public int PCR_PID
            {
                get { return Program.PCR_PID; }
                set { Program.PCR_PID = value; }
            }
            public short Priority
            {
                get { return Program.Priority; }
                set { Program.Priority = value; }
            }
            public short Program_PID
            {
                get { return Program.Program_PID; }
                set { Program.Program_PID = value; }
            }
            public short ServiceID
            {
                get { return Program.ServiceID; }
                set { Program.ServiceID = value; }
            }
            public short TSID
            {
                get { return Program.TSID; }
                set { Program.TSID = value; }
            }
            public short Volume
            {
                get { return Program.Volume; }
                set { Program.Volume = value; }
            }
            public override string Summary
            {
                get
                {
                    return string.Join("/", NetID, TSID, ServiceID, PCR_PID, Program_PID, Priority, Volume, EndTime, 
                        Utils.ComboBoxHelper.GetTypeStringValue(Enums.ParamType.AddressType, B_Address_type), "...");
                }
            }
            public string BroadcastStatus { get; set; }
        }

        [Serializable]
        public class StopPorgram : DailyProgram
        {
            public override byte B_Daily_cmd_tag { get { return StopProgramTag; } }
            public DailyCmdProgramStop Program { get; set; }
            public byte B_Address_type
            {
                get { return Program.B_Address_type; }
                set { Program.B_Address_type = value; }
            }
            public List<string> list_Terminal_Address
            {
                get { return Program.list_Terminal_Address; }
                set { Program.list_Terminal_Address = value; }
            }
            public short NetID
            {
                get { return Program.NetID; }
                set { Program.NetID = value; }
            }
            public short PCR_PID
            {
                get { return Program.PCR_PID; }
                set { Program.PCR_PID = value; }
            }
            public short Program_PID
            {
                get { return Program.Program_PID; }
                set { Program.Program_PID = value; }
            }
            public short ServiceID
            {
                get { return Program.ServiceID; }
                set { Program.ServiceID = value; }
            }
            public short TSID
            {
                get { return Program.TSID; }
                set { Program.TSID = value; }
            }
            public override string Summary
            {
                get
                {
                    return string.Join("/", NetID, TSID, ServiceID, PCR_PID, Program_PID,
                        Utils.ComboBoxHelper.GetTypeStringValue(Enums.ParamType.AddressType, B_Address_type));
                }
            }
        }

        [Serializable]
        public class OutSwitch : DailyProgram
        {
            public override byte B_Daily_cmd_tag { get { return OutSwitchTag; } }
            public DailyCmdOutSwitch Program { get; set; }
            public byte B_Address_type
            {
                get { return Program.B_Address_type; }
                set { Program.B_Address_type = value; }
            }
            public List<string> list_Terminal_Address
            {
                get { return Program.list_Terminal_Address; }
                set { Program.list_Terminal_Address = value; }
            }
            public byte B_Switch_status
            {
                get { return Program.B_Switch_status; }
                set { Program.B_Switch_status = value; }
            }
            public override string Summary
            {
                get
                {
                    return string.Join("/", B_Switch_status, Utils.ComboBoxHelper.GetTypeStringValue(Enums.ParamType.AddressType, B_Address_type),
                        list_Terminal_Address.Count > 0 ? list_Terminal_Address[0] + "..." : "...");
                }
            }
        }

        [Serializable]
        public class PlayCtrl : DailyProgram
        {
            public override byte B_Daily_cmd_tag { get { return PlayCtrlTag; } }
            public DailyCmdPlayCtrl Program { get; set; }
            public byte B_Address_type
            {
                get { return Program.B_Address_type; }
                set { Program.B_Address_type = value; }
            }
            public List<string> list_Terminal_Address
            {
                get { return Program.list_Terminal_Address; }
                set { Program.list_Terminal_Address = value; }
            }
            public short Volume
            {
                get { return Program.Volume; }
                set { Program.Volume = value; }
            }
            public override string Summary
            {
                get
                {
                    return string.Join("/", Volume, Utils.ComboBoxHelper.GetTypeStringValue(Enums.ParamType.AddressType, B_Address_type),
                        list_Terminal_Address.Count > 1 ? list_Terminal_Address[0] + "..." : "...");
                }
            }
        }

        [Serializable]
        public class RdsTransfer : DailyProgram
        {
            public override byte B_Daily_cmd_tag { get { return RdsTransferTag; } }
            public DailyCmdRdsTransfer Program { get; set; }
            public byte B_Address_type
            {
                get { return Program.B_Address_type; }
                set { Program.B_Address_type = value; }
            }
            public List<string> list_Terminal_Address
            {
                get { return Program.list_Terminal_Address; }
                set { Program.list_Terminal_Address = value; }
            }
            public byte B_Rds_terminal_type
            {
                get { return Program.B_Rds_terminal_type; }
                set { Program.B_Rds_terminal_type = value; }
            }
            public byte[] Br_Rds_data
            {
                get { return Program.Br_Rds_data; }
                set { Program.Br_Rds_data = value; }
            }
            public string RdsDataText { get; set; }
            public override string Summary
            {
                get
                {
                    return string.Join("/", Utils.ComboBoxHelper.GetTypeStringValue(Enums.ParamType.RdsTransferDaily, B_Rds_terminal_type), RdsDataText,
                        Utils.ComboBoxHelper.GetTypeStringValue(Enums.ParamType.AddressType, B_Address_type),
                        list_Terminal_Address.Count > 1 ? list_Terminal_Address[0] + "..." : "...");
                }
            }
        }

        [Serializable]
        public abstract class DailyProgram
        {
            public abstract string Summary { get; }
            public abstract byte B_Daily_cmd_tag { get; }
            public bool SendState { get; set; }
        }

        #endregion

        private void DailyBroadcast_FormClosing(object sender, FormClosingEventArgs e)
        {
            ChangeProgram_List.Clear();
            PlayCtrl_List.Clear();
            OutSwitch_List.Clear();
            RdsTransfer_List.Clear();
            foreach(var daily in TotalConfig_List)
            {
                switch(daily.B_Daily_cmd_tag)
                {
                    case ChangeProgramTag:
                        ChangeProgram_List.Add(daily as ChangeProgram);
                        break;
                    case PlayCtrlTag:
                        PlayCtrl_List.Add(daily as PlayCtrl);
                        break;
                    case OutSwitchTag:
                        OutSwitch_List.Add(daily as OutSwitch);
                        break;
                    case RdsTransferTag:
                        RdsTransfer_List.Add(daily as RdsTransfer);
                        break;
                }
            }
            TableData.TableDataHelper.WriteTable(Enums.TableType.DailyBroadcast,
                ChangeProgram_List, PlayCtrl_List, OutSwitch_List, RdsTransfer_List);
        }

        public void InitColumnStop(bool isStart)
        {
            for (int i = 0; i < dgvChangeProgram.RowCount; i++)
            {
                if ((dgvChangeProgram.Rows[i].DataBoundItem as ChangeProgram).SendState && isStart)
                {
                    dgvChangeProgram[ColumnShouldStop.Index, i].Value = "停止广播";
                    (dgvChangeProgram.Rows[i].DataBoundItem as ChangeProgram).BroadcastStatus = "正在广播";
                }
                else
                {
                    dgvChangeProgram[ColumnShouldStop.Index, i].Value = "开始广播";
                    (dgvChangeProgram.Rows[i].DataBoundItem as ChangeProgram).BroadcastStatus = "已停止";
                }
            }
            dgvChangeProgram.Invalidate();
        }

        public void AppendDataText(string text)
        {
            richTextData.AppendText(text);
        }

        private void dgvChangeProgram_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if(e.ColumnIndex == ColumnEndTime.Index)
            {
                MessageBox.Show("请按时间格式输出内容，如2000-01-01 00:00:00或2000/01/01 00:00:00");
            }
        }

        private void dgvRdsTransfer_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
        }

        private void dgvTotal_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            Rectangle headerRect = e.RowBounds;
            headerRect.Width = dgvTotal.RowHeadersWidth;
            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(System.Globalization.CultureInfo.CurrentUICulture),
                e.InheritedRowStyle.Font, headerRect, e.InheritedRowStyle.ForeColor,
                TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter);
        }

        private void dgvTotal_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvTotal.SelectedRows.Count > 0)
            {
                switch (TotalConfig_List[dgvTotal.SelectedRows[0].Index].B_Daily_cmd_tag)
                {
                    case ChangeProgramTag:
                        tabBroadcast.SelectedTab = pageChangeProgram;
                        dgvChangeProgram.DataSource = new List<ChangeProgram> { TotalConfig_List[dgvTotal.SelectedRows[0].Index] as ChangeProgram };
                        if (dgvChangeProgram[ColumnShouldStop.Index, 0].Value == null)
                        {
                            dgvChangeProgram[ColumnShouldStop.Index, 0].Value = (TotalConfig_List[dgvTotal.SelectedRows[0].Index] as ChangeProgram).BroadcastStatus == "已停止" ? "开始广播" : "停止广播";
                        }
                        break;
                    case StopProgramTag:
                        tabBroadcast.SelectedTab = pageProgramStop;
                        dgvStopProgram.DataSource = new List<StopPorgram> { TotalConfig_List[dgvTotal.SelectedRows[0].Index] as StopPorgram };
                        break;
                    case PlayCtrlTag:
                        tabBroadcast.SelectedTab = pagePlayCtrl;
                        dgvPlayCtrl.DataSource = new List<PlayCtrl> { TotalConfig_List[dgvTotal.SelectedRows[0].Index] as PlayCtrl };
                        break;
                    case OutSwitchTag:
                        tabBroadcast.SelectedTab = pageOutSwitch;
                        dgvOutSwitch.DataSource = new List<OutSwitch> { TotalConfig_List[dgvTotal.SelectedRows[0].Index] as OutSwitch };
                        break;
                    case RdsTransferTag:
                        tabBroadcast.SelectedTab = pageRdsTransfer;
                        dgvRdsTransfer.DataSource = new List<RdsTransfer> { TotalConfig_List[dgvTotal.SelectedRows[0].Index] as RdsTransfer };
                        break;
                }
            }
        }

        private void dgvTotal_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                DailyProgram pro = dgvTotal.Rows[e.RowIndex].DataBoundItem as DailyProgram;
                if (e.ColumnIndex == ColumnTotalSendState.Index)
                {
                    pro.SendState = !pro.SendState;
                    (MdiParent as EBMMain).InitStreamTable();
                }
            }
        }

        private void btnAdd_DailyChangeProgramClick(object sender, EventArgs e)
        {
            AddDaily(ChangeProgramTag);
        }

        private void btnAdd_DailyOutSwitchClick(object sender, EventArgs e)
        {
            AddDaily(OutSwitchTag);
        }

        private void btnAdd_DailyPlayCtrlClick(object sender, EventArgs e)
        {
            AddDaily(PlayCtrlTag);
        }

        private void btnAdd_DailyRdsTransferClick(object sender, EventArgs e)
        {
            AddDaily(RdsTransferTag);
        }

        private void checkBoxAllSel_CheckedChanged(object sender, EventArgs e)
        {
            foreach(var daily in TotalConfig_List)
            {
                daily.SendState = checkBoxAllSel.Checked;
            }
            dgvTotal.Invalidate(dgvTotal.GetColumnDisplayRectangle(ColumnTotalSendState.Index, true));
            (MdiParent as EBMMain).InitStreamTable();
        }

    }
}
