using ControlAstro.Utils;
using EBMTable;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace EBMTest
{
    public partial class EBMIndex : Form
    {
        private BindingCollection<EBIndexEx> EBIndex_List;
        private bool isFirstInit = true;

        public EBMIndex()
        {
            InitializeComponent();
            dgvEBIndex.AutoGenerateColumns = false;
            EBIndex_List = new BindingCollection<EBIndexEx>();
            dgvEBIndex.DataSource = EBIndex_List;

            InitIndexType();
            InitEBIndex();
            CheckIndex();
            dgvEBIndex.DataBindingComplete += DgvEBIndex_DataBindingComplete;

            Load += EBMIndex_Load;
        }

        private void EBMIndex_Load(object sender, EventArgs e)
        {
            (MdiParent as EBMMain).AdminAccountChanged += EBMIndex_AdminAccountChanged;
            SetAdminLimit((MdiParent as EBMMain).AdminAccount);
        }

        private void EBMIndex_AdminAccountChanged(object sender, AdminAccountEventArgs e)
        {
            SetAdminLimit(e.AdminAccount);
        }

        private void SetAdminLimit(bool admin)
        {
            MenuItemAdd.Visible = admin;
            MenuItemDel.Visible = admin;
            MenuItemUpdate.Visible = admin;
            btnAdd.Visible = admin;
            dgvEBIndex.ReadOnly = !admin;
            ColumnEBMResourceCode.Text = admin ? "编辑" : "查看";
            ColumnDes1.Text = admin ? "编辑" : "查看";
            ColumnDes2.Text = admin ? "编辑" : "查看";
        }

        private void CheckIndex()
        {
            object firstInit = null;
            TableData.TableDataHelper.ReadAppConfig("IndexFirstInit", out firstInit);
            isFirstInit = firstInit == null ? true : Convert.ToBoolean(firstInit);
            if (!isFirstInit) return;
            foreach (var index in EBIndex_List)
            {
                if (!Regex.IsMatch(index.S_EBM_class, "^[01]{4}$"))
                {
                    index.S_EBM_class = "0001";
                }
                if (!Regex.IsMatch(index.S_EBM_level, @"^(?:0{1}\d|1[1-5])$"))
                {
                    index.S_EBM_level = "00";
                }
            }
            isFirstInit = false;
            TableData.TableDataHelper.WriteAppConfig("IndexFirstInit", isFirstInit);
        }

        private void InitIndexType()
        {
            Utils.ComboBoxHelper.InitEBMLevel(ColumnEBMLevel);
            Utils.ComboBoxHelper.InitEBMClass(ColumnEBMClass);
        }

        private void InitEBIndex()
        {
            try
            {
                var jo = TableData.TableDataHelper.ReadTable(Enums.TableType.Index);
                if (jo != null)
                {
                    EBIndex_List = JsonConvert.DeserializeObject<BindingCollection<EBIndexEx>>(jo["0"].ToString());
                    foreach (var l in EBIndex_List)
                    {
                        l.List_EBM_resource_code.RemoveRange(l.List_EBM_resource_code.Count / 2, l.List_EBM_resource_code.Count / 2);
                        l.List_ProgramStreamInfo.RemoveRange(l.List_ProgramStreamInfo.Count / 2, l.List_ProgramStreamInfo.Count / 2);
                    }
                    dgvEBIndex.DataSource = EBIndex_List;
                    return;
                }

#if DEBUG
                //创建索引表
                EBIndex ebIndex = new EBIndex();

                ebIndex.S_EBM_id = "00000000000000000000000000" + 0.ToString("D4");
                ebIndex.S_EBM_original_network_id = "01";
                ebIndex.S_EBM_start_time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//"2017-01-01 08:00:00";
                ebIndex.S_EBM_end_time = DateTime.Now.AddHours(1).ToString("yyyy-MM-dd HH:mm:ss");//"2017-01-01 08:30:00";
                ebIndex.S_EBM_type = "19000";
                ebIndex.S_EBM_class = "0001";
                ebIndex.S_EBM_level = "01";

                List<string> list_ebm_resource_code = new List<string>(); //资源码
                list_ebm_resource_code.Add("12000000000000" + 0.ToString("D4"));
                ebIndex.List_EBM_resource_code = list_ebm_resource_code;

                ebIndex.BL_details_channel_indicate = true;
                ebIndex.S_details_channel_transport_stream_id = "02";
                ebIndex.S_details_channel_program_number = "02";
                ebIndex.S_details_channel_PCR_PID = "8191";

                //DetailChannelDescript detlchdiscrt = new DetailChannelDescript();
                //detlchdiscrt.B_descriptor_tag = 0x44;
                //detlchdiscrt.I_descriptor_length = 11;
                //detlchdiscrt.I_FEC_inner = 0;
                //detlchdiscrt.I_frequency = 0x6420000;
                //detlchdiscrt.I_FEC_outer = 0;
                //detlchdiscrt.I_Modulation = 0x03;
                //detlchdiscrt.I_Symbol_rate = 0x68750;
                //ebIndex.DetlChlDescriptor = detlchdiscrt;

                //ebIndex.B_stream_type = 0x03;
                //ebIndex.S_elementary_PID = (0 + 5000).ToString("D4");

                //StdDescriptor discriptor = new StdDescriptor();
                //discriptor.B_discriptor_tag = 0x03;
                //discriptor.arB_discriptor = new byte[2];
                //discriptor.arB_discriptor[0] = 0xe8;
                //ebIndex.Descriptor2 = discriptor;

                EBIndex_List.Add(new EBIndexEx { SendState = false, EBIndex = ebIndex, });
#endif
            }
            catch
            {
            }
        }

        private void AddIndex()
        {
            EBMIndexInfo indexForm = new EBMIndexInfo(Enums.OperateType.Add);
            DialogResult result = indexForm.ShowDialog();
            if (result == DialogResult.OK && indexForm.EBIndex != null)
            {
                EBIndex_List.Add(indexForm.EBIndex);
            }
            indexForm.Dispose();
        }

        private void InfoIndex()
        {
            if (dgvEBIndex.SelectedRows.Count > 0)
            {
                new EBMIndexInfo(Enums.OperateType.Info, EBIndex_List[dgvEBIndex.SelectedRows[0].Index]).ShowDialog();
            }
            else
            {
                MessageBox.Show("未选中任何索引");
            }
        }

        private void DelIndex()
        {
            if (dgvEBIndex.SelectedRows.Count > 0)
            {
                EBIndex_List.RemoveAt(dgvEBIndex.SelectedRows[0].Index);
            }
            else
            {
                MessageBox.Show("未选中任何索引");
            }
        }

        private void UpdateIndex()
        {
            if (dgvEBIndex.SelectedRows.Count > 0)
            {
                EBMIndexInfo indexForm = new EBMIndexInfo(Enums.OperateType.Update, EBIndex_List[dgvEBIndex.SelectedRows[0].Index]);
                DialogResult result = indexForm.ShowDialog();
                if (result == DialogResult.OK && indexForm.EBIndex != null)
                {
                    EBIndex_List[dgvEBIndex.SelectedRows[0].Index] = indexForm.EBIndex;
                }
                indexForm.Dispose();
            }
            else
            {
                MessageBox.Show("未选中任何索引");
            }
        }

        private void MenuItemInfo_Click(object sender, EventArgs e)
        {
            InfoIndex();
        }

        private void MenuItemAdd_Click(object sender, EventArgs e)
        {
            AddIndex();
        }

        private void MenuItemDel_Click(object sender, EventArgs e)
        {
            DelIndex();
        }

        private void MenuItemUpdate_Click(object sender, EventArgs e)
        {
            UpdateIndex();
        }

        private void MenuStripEBIndex_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MenuItemInfo.Enabled = dgvEBIndex.RowCount > 0;
            MenuItemDel.Enabled = dgvEBIndex.RowCount > 0;
            MenuItemUpdate.Enabled = dgvEBIndex.RowCount > 0;
            Point downPoint = dgvEBIndex.PointToClient(MousePosition);
            var hit = dgvEBIndex.HitTest(downPoint.X, downPoint.Y);
            if (hit.Type == DataGridViewHitTestType.Cell)
            {
                dgvEBIndex.Rows[hit.RowIndex].Selected = true;
            }
        }

        private void dgvEBIndex_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                if (e.ColumnIndex == ColumnSendState.Index)
                {
                    EBIndex_List[e.RowIndex].SendState = !EBIndex_List[e.RowIndex].SendState;
                    (MdiParent as EBMMain).InitStreamTable();
                }
                if (e.ColumnIndex == ColumnDetailChlIndicate.Index && (MdiParent as EBMMain).AdminAccount)
                {
                    EBIndex_List[e.RowIndex].BL_details_channel_indicate = !EBIndex_List[e.RowIndex].BL_details_channel_indicate;
                    for (int i = ColumnChlStreamId.Index; i < ColumnDesFlag.Index; i++)
                    {
                        dgvEBIndex[i, e.RowIndex].ReadOnly = !EBIndex_List[e.RowIndex].BL_details_channel_indicate;
                    }
                }
                if(e.ColumnIndex == ColumnDesFlag.Index && (MdiParent as EBMMain).AdminAccount)
                {
                    EBIndex_List[e.RowIndex].DesFlag = !EBIndex_List[e.RowIndex].DesFlag;
                    for (int i = ColumnDesFlag.Index; i < dgvEBIndex.ColumnCount; i++)
                    {
                        dgvEBIndex[i, e.RowIndex].ReadOnly = !EBIndex_List[e.RowIndex].DesFlag;
                    }
                }
                if (e.ColumnIndex == ColumnEBMResourceCode.Index)
                {
                    EBMIndexDetail detail = new EBMIndexDetail(0, EBIndex_List[e.RowIndex].List_EBM_resource_code, (MdiParent as EBMMain).AdminAccount);
                    DialogResult result = detail.ShowDialog();
                    if (result == DialogResult.OK && (MdiParent as EBMMain).AdminAccount)
                    {
                        EBIndex_List[e.RowIndex].List_EBM_resource_code = detail.GetData() as List<string>;
                    }
                    detail.Dispose();
                }
                if (e.ColumnIndex == ColumnDes1.Index && EBIndex_List[e.RowIndex].DesFlag)
                {
                    EBMIndexDetail detail = new EBMIndexDetail(1, EBIndex_List[e.RowIndex].DeliverySystemDescriptor, (MdiParent as EBMMain).AdminAccount);
                    DialogResult result = detail.ShowDialog();
                    if (result == DialogResult.OK && (MdiParent as EBMMain).AdminAccount)
                    {
                        object data = detail.GetData();
                        EBIndex_List[e.RowIndex].DeliverySystemDescriptor = detail.GetData();
                    }
                    detail.Dispose();
                }
                if (e.ColumnIndex == ColumnDes2.Index && EBIndex_List[e.RowIndex].BL_details_channel_indicate)
                {
                    EBMIndexDetail detail = new EBMIndexDetail(2, EBIndex_List[e.RowIndex].List_ProgramStreamInfo, (MdiParent as EBMMain).AdminAccount);
                    DialogResult result = detail.ShowDialog();
                    if (result == DialogResult.OK && (MdiParent as EBMMain).AdminAccount)
                    {
                        EBIndex_List[e.RowIndex].List_ProgramStreamInfo = detail.GetData() as List<ProgramStreamInfo>;
                    }
                    detail.Dispose();
                }
            }
        }

        private void DgvEBIndex_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            for (int j = 0; j < dgvEBIndex.RowCount; j++)
            {
                for (int i = ColumnChlStreamId.Index; i < ColumnDesFlag.Index; i++)
                {
                    dgvEBIndex[i, j].ReadOnly = !EBIndex_List[j].BL_details_channel_indicate;
                }
                for (int i = ColumnDesFlag.Index; i < dgvEBIndex.ColumnCount; i++)
                {
                    dgvEBIndex[i, j].ReadOnly = !EBIndex_List[j].DesFlag;
                }
            }
        }

        private List<EBIndex> GetEBMIndex()
        {
            if (EBIndex_List.Count == 0)
            {
                return null;
            }
            List<EBIndex> list = new List<EBIndex>();
            foreach (var index in EBIndex_List)
            {
                list.Add(index.EBIndex);
            }
            return list;
        }

        private List<EBIndex> GetSendEBMIndex()
        {
            if (EBIndex_List.Count == 0)
            {
                return null;
            }
            List<EBIndex> list = new List<EBIndex>();
            foreach (var index in EBIndex_List)
            {
                if (index.SendState)
                {
                    list.Add(index.EBIndex);
                    if(!index.DesFlag)
                    {
                        list[list.Count - 1].DetlChlDescriptor = null;
                    }
                }
            }
            return list;
        }

        public bool GetEBIndexTable(ref EBIndexTable oldTable)
        {
            try
            {
                if (oldTable == null)
                {
                    oldTable = new EBIndexTable();
                    oldTable.Table_id = 0xfd;
                    oldTable.Table_id_extension = 0;
                }
                List<EBIndex> listEbIndex = GetSendEBMIndex();
                oldTable.ListEbIndex = listEbIndex;
                if (listEbIndex == null || listEbIndex.Count == 0)
                {
                    oldTable.ListEbIndex = null;
                }
                oldTable.Repeat_times = pnlRepeatTimes.GetRepeatTimes();
                return true;
            }
            catch
            {
                return false;
            }
        }


        public class EBIndexEx
        {
            public bool SendState { get; set; }
            public string NickName { get; set; }
            public EBIndex EBIndex { get; set; }
            public bool BL_details_channel_indicate
            {
                get { return EBIndex.BL_details_channel_indicate; }
                set { EBIndex.BL_details_channel_indicate = value; }
            }
            public bool DesFlag { get; set; }
            private Cable_delivery_system_descriptor cdsd;
            private Terristrial_delivery_system_descriptor tdsd;
            public Cable_delivery_system_descriptor CDSDDescriptor
            {
                get { return cdsd; }
                set
                {
                    cdsd = value;
                    if (value != null) DetlChlDescriptor = value.GetDescriptor();
                }
            }
            public Terristrial_delivery_system_descriptor TDSDDescriptor
            {
                get { return tdsd; }
                set
                {
                    tdsd = value;
                    if (value != null) DetlChlDescriptor = value.GetDescriptor();
                }
            }
            public object DeliverySystemDescriptor
            {
                get
                {
                    if(CDSDDescriptor !=null)
                    {
                        return CDSDDescriptor;
                    }
                    else if(TDSDDescriptor!=null)
                    {
                        return TDSDDescriptor;
                    }
                    return null;
                }
                set
                {
                    if(value is Cable_delivery_system_descriptor)
                    {
                        CDSDDescriptor = value as Cable_delivery_system_descriptor;
                        TDSDDescriptor = null;
                    }
                    else if (value is Terristrial_delivery_system_descriptor)
                    {
                        TDSDDescriptor = value as Terristrial_delivery_system_descriptor;
                        CDSDDescriptor = null;
                    }
                }
            }
            public StdDescriptor DetlChlDescriptor
            {
                get { return EBIndex.DetlChlDescriptor; }
                set { EBIndex.DetlChlDescriptor = value; }
            }
            public List<ProgramStreamInfo> List_ProgramStreamInfo
            {
                get { return EBIndex.list_ProgramStreamInfo; }
                set { EBIndex.list_ProgramStreamInfo = value; }
            }
            public List<string> List_EBM_resource_code
            {
                get { return EBIndex.List_EBM_resource_code; }
                set { EBIndex.List_EBM_resource_code = value; }
            }
            public string S_details_channel_PCR_PID
            {
                get { return EBIndex.S_details_channel_PCR_PID; }
                set { EBIndex.S_details_channel_PCR_PID = value; }
            }
            public string S_details_channel_program_number
            {
                get { return EBIndex.S_details_channel_program_number; }
                set { EBIndex.S_details_channel_program_number = value; }
            }
            public string S_details_channel_transport_stream_id
            {
                get { return EBIndex.S_details_channel_transport_stream_id; }
                set { EBIndex.S_details_channel_transport_stream_id = value; }
            }
            public string S_EBM_class
            {
                get { return EBIndex.S_EBM_class; }
                set { EBIndex.S_EBM_class = value; }
            }
            public string S_EBM_end_time
            {
                get { return EBIndex.S_EBM_end_time; }
                set { EBIndex.S_EBM_end_time = value; }
            }
            public string S_EBM_id
            {
                get { return EBIndex.S_EBM_id; }
                set { EBIndex.S_EBM_id = value; }
            }
            public string S_EBM_level
            {
                get { return EBIndex.S_EBM_level; }
                set { EBIndex.S_EBM_level = value; }
            }
            public string S_EBM_original_network_id
            {
                get { return EBIndex.S_EBM_original_network_id; }
                set { EBIndex.S_EBM_original_network_id = value; }
            }
            public string S_EBM_start_time
            {
                get { return EBIndex.S_EBM_start_time; }
                set { EBIndex.S_EBM_start_time = value; }
            }
            public string S_EBM_type
            {
                get { return EBIndex.S_EBM_type; }
                set { EBIndex.S_EBM_type = value; }
            }
        }

        private void EBMIndex_FormClosing(object sender, FormClosingEventArgs e)
        {
            TableData.TableDataHelper.WriteTable(Enums.TableType.Index, EBIndex_List);
        }

        public void AppendDataText(string text)
        {
            richTextData.AppendText(text);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddIndex();
        }

        private void dgvEBIndex_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            //MessageBox.Show(string.Format("列：{0} 行：{1} 数据输入异常，请检查，如需退出编辑请按Esc键", e.ColumnIndex, e.RowIndex));
            e.ThrowException = false;
        }

        private void dgvEBIndex_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex == ColumnEBMEndTime.Index && e.RowIndex >= 0)
            {
                (dgvEBIndex[e.ColumnIndex, e.RowIndex] as Controls.DataGridViewSysTimeDelayCell).OldTime = Convert.ToDateTime(EBIndex_List[e.RowIndex].S_EBM_start_time);
            }
        }

    }
}
