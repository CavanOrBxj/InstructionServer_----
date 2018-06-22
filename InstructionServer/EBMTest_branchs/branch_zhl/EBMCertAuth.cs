using ControlAstro.Utils;
using EBMTable;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace EBMTest
{
    public partial class EBMCertAuth : Form
    {
        private BindingCollection<Cert> certList;
        private BindingCollection<Cert> certAuthList;
        private bool tag; //判断点击的是哪个表

        public EBMCertAuth()
        {
            InitializeComponent();
            dgvEBCert.AutoGenerateColumns = false;
            dgvEBCertAuth.AutoGenerateColumns = false;
            InitCert();
            if (certList == null)
            {
                certList = new BindingCollection<Cert>();
            }
            dgvEBCert.DataSource = certList;
            if (certAuthList == null)
            {
                certAuthList = new BindingCollection<Cert>();
            }
            dgvEBCertAuth.DataSource = certAuthList;
        }

        private void InitCert()
        {
            var jo = TableData.TableDataHelper.ReadTable(Enums.TableType.CertAuth);
            if (jo != null)
            {
                certList = JsonConvert.DeserializeObject<BindingCollection<Cert>>(jo["0"].ToString());
                certAuthList = JsonConvert.DeserializeObject<BindingCollection<Cert>>(jo["1"].ToString());
            }
        }

        public List<byte[]> GetSendCert()
        {
            if (certList.Count == 0)
            {
                return null;
            }
            List<byte[]> list = new List<byte[]>();
            foreach (var cert in certList)
            {
                if (cert.SendState)
                {
                    if(cert.Tag == 0)
                    {
                        list.Add(TableData.TableDataHelper.GetFileData(cert.Cert_data));
                    }
                    else if(cert.Tag == 1)
                    {
                        list.Add(Encoding.GetEncoding("GB2312").GetBytes(cert.Cert_data));
                    }
                }
            }
            return list;
        }

        public List<byte[]> GetSendCertAuth()
        {
            if (certAuthList.Count == 0)
            {
                return null;
            }
            List<byte[]> list = new List<byte[]>();
            foreach (var cert in certAuthList)
            {
                if (cert.SendState)
                {
                    if (cert.Tag == 0)
                    {
                        list.Add(TableData.TableDataHelper.GetFileData(cert.Cert_data));
                    }
                    else if (cert.Tag == 1)
                    {
                        list.Add(Encoding.GetEncoding("GB2312").GetBytes(cert.Cert_data));
                    }
                }
            }
            return list;
        }

        public bool GetCertAuthTable(ref EBCertAuthTable oldTable)
        {
            try
            {
                if ((GetSendCert() == null || GetSendCert().Count == 0) &&
                    (GetSendCertAuth() == null || GetSendCertAuth().Count == 0))
                {
                    if (oldTable != null)
                    {
                        oldTable.list_Cert_data = null;
                        oldTable.list_CertAuth_data = null;
                    }
                    return false;
                }
                if (oldTable == null)
                {
                    oldTable = new EBCertAuthTable();
                    oldTable.Table_id = 0xfc;
                    oldTable.Table_id_extension = 0;
                }
                oldTable.list_Cert_data = GetSendCert();
                oldTable.list_CertAuth_data = GetSendCertAuth();
                oldTable.Repeat_times = pnlRepeatTimes.GetRepeatTimes();
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void MenuStripEBCertAuth_Opening(object sender, CancelEventArgs e)
        {
            Point p = PointToClient(MousePosition);
            if (dgvEBCert.Bounds.Contains(p))
            {
                tag = true;
                MenuItemInfo.Enabled = dgvEBCert.RowCount > 0;
                MenuItemDel.Enabled = dgvEBCert.RowCount > 0;
                MenuItemUpdate.Enabled = dgvEBCert.RowCount > 0;
                Point downPoint = dgvEBCert.PointToClient(MousePosition);
                var hit = dgvEBCert.HitTest(downPoint.X, downPoint.Y);
                if (hit.Type == DataGridViewHitTestType.Cell || hit.Type == DataGridViewHitTestType.RowHeader)
                {
                    dgvEBCert.Rows[hit.RowIndex].Selected = true;
                }
            }
            else if (dgvEBCertAuth.Bounds.Contains(p))
            {
                tag = false;
                MenuItemInfo.Enabled = dgvEBCertAuth.RowCount > 0;
                MenuItemDel.Enabled = dgvEBCertAuth.RowCount > 0;
                MenuItemUpdate.Enabled = dgvEBCertAuth.RowCount > 0;
                Point downPoint = dgvEBCertAuth.PointToClient(MousePosition);
                var hit = dgvEBCertAuth.HitTest(downPoint.X, downPoint.Y);
                if (hit.Type == DataGridViewHitTestType.Cell || hit.Type == DataGridViewHitTestType.RowHeader)
                {
                    dgvEBCertAuth.Rows[hit.RowIndex].Selected = true;
                }
            }
        }

        private void MenuItemAdd_Click(object sender, EventArgs e)
        {
            AddCertAuth(tag);
        }

        private void MenuItemInfo_Click(object sender, EventArgs e)
        {
            InfoCertAuth();
        }

        private void MenuItemDel_Click(object sender, EventArgs e)
        {
            DelCertAuth();
        }

        private void MenuItemUpdate_Click(object sender, EventArgs e)
        {
            UpdateCertAuth();
        }

        private void AddCertAuth(bool tag)
        {
            EBMCertAuthInfo form = new EBMCertAuthInfo(Enums.OperateType.Add, tag);
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK && form.CertData != null)
            {
                if (tag)
                {
                    certList.Add(form.CertData);
                }
                else
                {
                    certAuthList.Add(form.CertData);
                }
            }
            form.Dispose();
        }

        private void DelCertAuth()
        {
            if (tag)
            {
                if (dgvEBCert.SelectedRows.Count > 0)
                {
                    certList.RemoveAt(dgvEBCert.SelectedRows[0].Index);
                }
                else
                {
                    MessageBox.Show("未选中任何索引");
                }
            }
            else
            {
                if (dgvEBCertAuth.SelectedRows.Count > 0)
                {
                    certAuthList.RemoveAt(dgvEBCertAuth.SelectedRows[0].Index);
                }
                else
                {
                    MessageBox.Show("未选中任何索引");
                }
            }
        }

        private void UpdateCertAuth()
        {
            if ((tag && dgvEBCert.SelectedRows.Count > 0) || (!tag && dgvEBCertAuth.SelectedRows.Count > 0))
            {
                EBMCertAuthInfo form = new EBMCertAuthInfo(Enums.OperateType.Update, tag,
                    tag ? certList[dgvEBCert.SelectedRows[0].Index] : certAuthList[dgvEBCertAuth.SelectedRows[0].Index]);
                DialogResult result = form.ShowDialog();
                if (result == DialogResult.OK && form.CertData != null)
                {
                    if (tag)
                    {
                        certList[dgvEBCert.SelectedRows[0].Index] = form.CertData;
                    }
                    else
                    {
                        certAuthList[dgvEBCertAuth.SelectedRows[0].Index] = form.CertData;
                    }
                }
                form.Dispose();
            }
            else
            {
                MessageBox.Show("未选中任何索引");
            }
        }

        private void InfoCertAuth()
        {
            if ((tag && dgvEBCert.SelectedRows.Count > 0) || (!tag && dgvEBCertAuth.SelectedRows.Count > 0))
            {
                EBMCertAuthInfo form = new EBMCertAuthInfo(Enums.OperateType.Info, tag,
                    tag ? certList[dgvEBCert.SelectedRows[0].Index] : certAuthList[dgvEBCertAuth.SelectedRows[0].Index]);
                DialogResult result = form.ShowDialog();
            }
            else
            {
                MessageBox.Show("未选中任何索引");
            }
        }

        private void dgvEBCertAuth_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                if (e.ColumnIndex == ColumnAuthSendState.Index)
                {
                    certAuthList[e.RowIndex].SendState = !certAuthList[e.RowIndex].SendState;
                    (MdiParent as EBMMain).InitStreamTable();
                }
            }
        }

        private void dgvEBCert_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                if (e.ColumnIndex == ColumnCertSendState.Index)
                {
                    certList[e.RowIndex].SendState = !certList[e.RowIndex].SendState;
                    (MdiParent as EBMMain).InitStreamTable();
                }
            }
        }

        private void EBMCertAuth_FormClosing(object sender, FormClosingEventArgs e)
        {
            TableData.TableDataHelper.WriteTable(Enums.TableType.CertAuth, certList, certAuthList);
        }


        public class Cert
        {
            public bool SendState { get; set; }
            public string Cert_data { get; set; }
            public int Tag { get; set; }  //数据类型
        }

        public void AppendDataText(string text)
        {
            richTextData.AppendText(text);
        }

        private void dgvEBCertDataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            //MessageBox.Show(string.Format("列：{0} 行：{1} 数据输入异常，请检查，如需退出编辑请按Esc键", e.ColumnIndex, e.RowIndex));
            e.ThrowException = false;
        }

        private void AddCertAuthBtn_CertClick(object sender, EventArgs e)
        {
            AddCertAuth(true);
        }

        private void AddCertAuthBtn_CertAuthClick(object sender, EventArgs e)
        {
            AddCertAuth(false);
        }
    }
}
