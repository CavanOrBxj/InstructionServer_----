using ControlAstro.Utils;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace EBMTest.Layouts
{
    public partial class EBMIndexResourceCode : UserControl
    {
        private BindingCollection<Address> bindList;
        private ToolTip tip;

        public EBMIndexResourceCode()
        {
            InitializeComponent();
            dgvResourceCode.AutoGenerateColumns = false;
            bindList = new BindingCollection<Address>();
            dgvResourceCode.DataSource = bindList;
            tip = new ToolTip();
        }

        public void InitData(List<string> code_list, bool canEdit = true)
        {
            btnAdd.Enabled = canEdit;
            btnDel.Enabled = canEdit;
            foreach (var s in code_list)
            {
                bindList.Add(new Address { Value = s, });
            }
            dgvResourceCode.DataSource = bindList;
        }

        public List<string> GetData()
        {
            try
            {
                List<string> list = new List<string>();
                foreach (var v in bindList)
                {
                    list.Add(v.Value);
                }
                return list;
            }
            catch
            {
                return null;
            }
        }

        private void btnAddResourceCode_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textResourceCode.Text))
            {
                bindList.Add(new Address { Value = textResourceCode.Text.Trim(), });
            }
        }

        private void btnDelResourceCode_Click(object sender, EventArgs e)
        {
            if (dgvResourceCode.SelectedRows.Count == 0)
            {
                tip.Show("未选中任何地址", this, btnDel.Location.X, btnDel.Location.Y - 30, 2000);
            }
            else
            {
                bindList.RemoveAt(dgvResourceCode.SelectedRows[0].Index);
            }
        }

        private void textList_EBM_resource_code_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 8)
            {
                e.Handled = false;
                return;
            }
            if (textResourceCode.TextLength > 17)
            {
                e.Handled = true;
                return;
            }
            if ((e.KeyChar >= '0' && e.KeyChar <= '9'))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private class Address
        {
            public string Value { get; set; }
        }

    }
}
