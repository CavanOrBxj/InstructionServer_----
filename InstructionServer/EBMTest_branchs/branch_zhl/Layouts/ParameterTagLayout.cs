using ControlAstro.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace EBMTest.Layouts
{
    public partial class ParameterTagLayout : UserControl
    {
        private BindingCollection<Parameter> bindList;
        private ToolTip tip;

        public ParameterTagLayout()
        {
            InitializeComponent();
            dgvParameterTag.AutoGenerateColumns = false;
            bindList = new BindingCollection<Parameter>();
            dgvParameterTag.DataSource = bindList;
            tip = new ToolTip();
            InitParaType();
        }

        private void InitParaType()
        {
            Utils.ComboBoxHelper.InitParameterType(cbBoxParameterTag);
        }

        public void InitData(List<byte> list)
        {
            if (list != null)
            {
                foreach (var s in list)
                {
                    bindList.Add(new Parameter { Value = s, });
                }
            }
            dgvParameterTag.DataSource = bindList;
        }

        public List<byte> GetData()
        {
            try
            {
                List<byte> list = new List<byte>();
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            bindList.Add(new Parameter { Value = (byte)cbBoxParameterTag.SelectedValue, });
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (dgvParameterTag.SelectedRows.Count == 0)
            {
                tip.Show("未选中任何地址", this, btnDel.Location.X, btnDel.Location.Y - 30, 2000);
            }
            else
            {
                bindList.RemoveAt(dgvParameterTag.SelectedRows[0].Index);
            }
        }

        private class Parameter
        {
            public byte Value { get; set; }
        }
    }
}
