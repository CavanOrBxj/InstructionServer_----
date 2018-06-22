using ControlAstro.Utils;
using EBMTable;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace EBMTest.Layouts
{
    public partial class EBMIndexProgramStreamInfoLayout : UserControl
    {
        private BindingCollection<ProStreamInfo> bindList;
        private bool canEdit;

        public EBMIndexProgramStreamInfoLayout()
        {
            InitializeComponent();
            dgvProgramStreamInfo.AutoGenerateColumns = false;
            bindList = new BindingCollection<ProStreamInfo>();
            dgvProgramStreamInfo.DataSource = bindList;
            InitType();
        }

        private void InitType()
        {
            Utils.ComboBoxHelper.InitEBMStreamType(cbBoxB_stream_type);
            Utils.ComboBoxHelper.InitEBMStreamType(ColumnStreamType);
        }

        public void InitData(List<ProgramStreamInfo> list, bool canEdit = true)
        {
            this.canEdit = canEdit;
            btnAdd.Enabled = canEdit;
            btnDel.Enabled = canEdit;
            dgvProgramStreamInfo.ReadOnly = !canEdit;
            ColumnDes2.Text = canEdit ? "编辑" : "查看";
            for (int i = 0; i < list.Count; i++)
            {
                bindList.Add(new ProStreamInfo { Info = list[i] });
            }
            dgvProgramStreamInfo.DataSource = bindList;
        }

        public List<ProgramStreamInfo> GetData()
        {
            List<ProgramStreamInfo> list = new List<ProgramStreamInfo>();
            for (int i = 0; i < bindList.Count; i++)
            {
                list.Add(bindList[i].Info);
            }
            return list;
        }

        private bool ValidatData()
        {
            foreach (Control c in groupBoxEdit.Controls)
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidatData()) return;
            ProStreamInfo pro = new ProStreamInfo();
            pro.Info = new ProgramStreamInfo
            {
                B_stream_type = (byte)cbBoxB_stream_type.SelectedValue,
                S_elementary_PID = textS_elementary_PID.Text.Trim(),
                Descriptor2 = pnlIndexDes2.GetData(),
            };
            bindList.Add(pro);
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (dgvProgramStreamInfo.SelectedRows.Count > 0)
            {
                bindList.RemoveAt(dgvProgramStreamInfo.SelectedRows[0].Index);
            }
        }

        private void dgvProgramStreamInfo_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == ColumnDes2.Index && e.RowIndex >= 0)
            {
                EBMIndexDetail detail = new EBMIndexDetail(3, bindList[e.RowIndex].Descriptor2, canEdit);
                DialogResult result = detail.ShowDialog();
                if (result == DialogResult.OK)
                {
                    bindList[e.RowIndex].Descriptor2 = detail.GetData() as StdDescriptor;
                }
                detail.Dispose();
            }
        }

        public class ProStreamInfo
        {
            public ProgramStreamInfo Info { get; set; }
            public byte B_stream_type
            {
                get { return Info.B_stream_type; }
                set { Info.B_stream_type = value; }
            }
            public StdDescriptor Descriptor2
            {
                get { return Info.Descriptor2; }
                set { Info.Descriptor2 = value; }
            }
            public string S_elementary_PID
            {
                get { return Info.S_elementary_PID; }
                set { Info.S_elementary_PID = value; }
            }
        }

    }
}
