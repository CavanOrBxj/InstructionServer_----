using ControlAstro.Utils;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace EBMTest.Layouts
{
    public partial class TerminalAddressLayout : UserControl
    {
        private BindingCollection<Address> bindList;
        private ToolTip tip;

        public TerminalAddressLayout()
        {
            InitializeComponent();
            dgvTerminalAddress.AutoGenerateColumns = false;
            bindList = new BindingCollection<Address>();
            dgvTerminalAddress.DataSource = bindList;
            tip = new ToolTip();
            textAddress.KeyPress += TextAddress_KeyPress;
        }

        private void TextAddress_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 8)
            {
                e.Handled = false;
                return;
            }
            if (textAddress.TextLength > 17)
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

        public void InitData(List<string> list, bool canEdit = true)
        {
            btnAdd.Enabled = canEdit;
            btnDel.Enabled = canEdit;
            if (list != null)
            {
                foreach (var s in list)
                {
                    bindList.Add(new Address { Value = s, });
                }
            }
            dgvTerminalAddress.DataSource = bindList;
        }

        public List<string> GetData()
        {
            try
            {
                List<string> list = new List<string>();
                foreach(var v in bindList)
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textAddress.Text))
            {
                bindList.Add(new Address { Value = textAddress.Text.Trim(), });
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if(dgvTerminalAddress.SelectedRows.Count==0)
            {
                tip.Show("未选中任何地址", this, btnDel.Location.X, btnDel.Location.Y - 30, 2000);
            }
            else
            {
                bindList.RemoveAt(dgvTerminalAddress.SelectedRows[0].Index);
            }
        }


        private class Address
        {
            public string Value { get; set; }
        }

    }
}
