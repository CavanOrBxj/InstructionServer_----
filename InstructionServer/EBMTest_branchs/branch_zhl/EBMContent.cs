using ControlAstro.Utils;
using EBMTable;
using EBMTest.Utils;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace EBMTest
{
    public partial class EBMContent : Form
    {
        private BindingCollection<EBContent> EBContent_List;

        public EBMContent()
        {
            InitializeComponent();
            dgvEBContent.AutoGenerateColumns = false;
            Utils.ComboBoxHelper.InitCodeCharacter(ColumnB_code_character_set);
            EBContent_List = new BindingCollection<EBContent>();
            dgvEBContent.DataSource = EBContent_List;
            InitEbmId();
            InitContent();
        }

        private void InitContent()
        {
            var jo = TableData.TableDataHelper.ReadTable(Enums.TableType.Content);
            if (jo != null)
            {
                EBContent_List = JsonConvert.DeserializeObject<BindingCollection<EBContent>>(jo["0"].ToString());
                //foreach (var l in EBContent_List)
                //{
                //    l.list_auxiliary_data.RemoveRange(l.list_auxiliary_data.Count / 2, l.list_auxiliary_data.Count / 2);
                //}
                dgvEBContent.DataSource = EBContent_List;
            }
        }

        private void InitEbmId()
        {
            BindingCollection<EBMIndex.EBIndexEx> index = new BindingCollection<EBMIndex.EBIndexEx>();
            List<string> ebmId = new List<string>();
            var jo = TableData.TableDataHelper.ReadTable(Enums.TableType.Index);
            if (jo != null)
            {
                index = JsonConvert.DeserializeObject<BindingCollection<EBMIndex.EBIndexEx>>(jo["0"].ToString());
                //foreach (var l in index)
                //{
                //    l.List_EBM_resource_code.RemoveRange(l.List_EBM_resource_code.Count / 2, l.List_EBM_resource_code.Count / 2);
                //}
            }
            foreach(var dex in index)
            {
                ebmId.Add(dex.S_EBM_id);
            }
            cbBoxEBMId.DataSource = ebmId;
        }

        public bool GetContentTable(ref EBContentTable oldTable)
        {
            try
            {
                List<MultilangualContent> listMulti = GetSendMultilangualContent();
                if (listMulti == null || listMulti.Count == 0)
                {
                    oldTable.list_multilangual_content.Clear();
                    return false;
                }
                if (oldTable == null)
                {
                    oldTable = new EBContentTable();
                    oldTable.Table_id = 0xfe;
                    oldTable.Table_id_extension = 0;
                }
                oldTable.list_multilangual_content = listMulti;
                oldTable.S_EBM_id = cbBoxEBMId.Text;
                oldTable.Repeat_times = pnlRepeatTimes.GetRepeatTimes();
                return true;
            }
            catch
            {
                return false;
            }
        }

        private List<MultilangualContent> GetSendMultilangualContent()
        {
            if (EBContent_List.Count == 0)
            {
                return null;
            }
            List<MultilangualContent> listMulti = new List<MultilangualContent>();
            foreach (var content in EBContent_List)
            {
                if (content.SendState)
                {
                    content.MultilangualContent.list_auxiliary_data = content.GetAuxiliaryData();
                    listMulti.Add(content.MultilangualContent);
                }
            }
            return listMulti;
        }

        private void MenuItemAdd_Click(object sender, System.EventArgs e)
        {
            AddIndex();
        }

        private void MenuItemInfo_Click(object sender, System.EventArgs e)
        {
            InfoIndex();
        }

        private void MenuItemUpdate_Click(object sender, System.EventArgs e)
        {
            UpdateIndex();
        }

        private void MenuItemDel_Click(object sender, System.EventArgs e)
        {
            DelContent();
        }

        private void MenuStripEBContent_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MenuItemInfo.Enabled = dgvEBContent.RowCount > 0;
            MenuItemDel.Enabled = dgvEBContent.RowCount > 0;
            MenuItemUpdate.Enabled = dgvEBContent.RowCount > 0;
            Point downPoint = dgvEBContent.PointToClient(MousePosition);
            var hit = dgvEBContent.HitTest(downPoint.X, downPoint.Y);
            if (hit.Type == DataGridViewHitTestType.Cell || hit.Type == DataGridViewHitTestType.RowHeader)
            {
                dgvEBContent.Rows[hit.RowIndex].Selected = true;
            }
        }

        private void btnAdd_Click(object sender, System.EventArgs e)
        {
            AddIndex();
        }

        private void DelContent()
        {
            if (dgvEBContent.SelectedRows.Count > 0)
            {
                EBContent_List.RemoveAt(dgvEBContent.SelectedRows[0].Index);
            }
            else
            {
                MessageBox.Show("未选中任何索引");
            }
        }

        private void AddIndex()
        {
            EBMContentInfo form = new EBMContentInfo(Enums.OperateType.Add);
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK && form.Content != null)
            {
                EBContent_List.Add(form.Content);
            }
            form.Dispose();
        }

        private void InfoIndex()
        {
            if (dgvEBContent.SelectedRows.Count > 0)
            {
                new EBMContentInfo(Enums.OperateType.Info, EBContent_List[dgvEBContent.SelectedRows[0].Index]).ShowDialog();
            }
            else
            {
                MessageBox.Show("未选中任何索引");
            }
        }

        private void UpdateIndex()
        {
            if (dgvEBContent.SelectedRows.Count > 0)
            {
                EBMContentInfo form = new EBMContentInfo(Enums.OperateType.Update, EBContent_List[dgvEBContent.SelectedRows[0].Index]);
                DialogResult result = form.ShowDialog();
                if (result == DialogResult.OK && form.Content != null)
                {
                    EBContent_List[dgvEBContent.SelectedRows[0].Index] = form.Content;
                }
                form.Dispose();
            }
            else
            {
                MessageBox.Show("未选中任何索引");
            }
        }

        private void dgvEBContent_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                if (e.ColumnIndex == ColumnSendState.Index)
                {
                    EBContent_List[e.RowIndex].SendState = !EBContent_List[e.RowIndex].SendState;
                    (MdiParent as EBMMain).InitStreamTable();
                }
                if (e.ColumnIndex == ColumnB_auxiliary_data.Index)
                {
                    EBMContentDetail detail = new EBMContentDetail(EBContent_List[e.RowIndex].list_auxiliary_data);
                    DialogResult result = detail.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        EBContent_List[e.RowIndex].list_auxiliary_data = detail.GetData();
                    }
                    detail.Dispose();
                }
            }
        }



        public class EBContent
        {
            public bool SendState { get; set; }

            public MultilangualContent MultilangualContent { get; set; }
            public byte B_code_character_set
            {
                get { return MultilangualContent.B_code_character_set; }
                set { MultilangualContent.B_code_character_set = value; }
            }
            public byte[] B_message_text
            {
                get { return MultilangualContent.B_message_text; }
                set { MultilangualContent.B_message_text = value; }
            }
            public string S_language_code
            {
                get { return MultilangualContent.S_language_code; }
                set { MultilangualContent.S_language_code = value; }
            }
            public string MessageText { get; set; }

            public List<Auxiliary> list_auxiliary_data { get; set; }

            public List<AuxiliaryData> GetAuxiliaryData()
            {
                List<AuxiliaryData> data = new List<AuxiliaryData>();
                for (int i = 0; i < list_auxiliary_data.Count; i++)
                {
                    data.Add(list_auxiliary_data[i].GetAuxData());
                }
                return data;
            }
        }

        public class Auxiliary
        {
            public AuxiliaryData GetAuxData()
            {
                AuxiliaryData data = new AuxiliaryData();
                data.B_auxiliary_data_type = Type;
                data.B_auxiliary_data = TableData.TableDataHelper.GetFileData(DisplayData);
                return data;
            }
            public byte Type { get; set; }
            public string DisplayData { get; set; }
            private string typeString;
            public string TypeString
            {
                get { return ComboBoxHelper.GetTypeStringValue(Enums.ParamType.ContentAuxiliaryData, Type); }
                set { typeString = value; }
            }
        }

        private void EBMContent_FormClosing(object sender, FormClosingEventArgs e)
        {
            TableData.TableDataHelper.WriteTable(Enums.TableType.Content, EBContent_List);
        }

        public void AppendDataText(string text)
        {
            richTextData.AppendText(text);
        }

        private void dgvEBContent_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            //MessageBox.Show(string.Format("列：{0} 行：{1} 数据输入异常，请检查，如需退出编辑请按Esc键", e.ColumnIndex, e.RowIndex));
            e.ThrowException = false;
        }
    }
}
