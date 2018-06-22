using ControlAstro.Utils;
using EBMTable;
using EBMTest.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace EBMTest.Layouts
{
    public partial class ContentAuxiliaryDataLayout : UserControl
    {
        private BindingCollection<EBMContent.Auxiliary> bindList;
        private ToolTip tip;

        public ContentAuxiliaryDataLayout()
        {
            InitializeComponent();
            dgvAuxiliaryData.AutoGenerateColumns = false;
            bindList = new BindingCollection<EBMContent.Auxiliary>();
            dgvAuxiliaryData.DataSource = bindList;
            tip = new ToolTip();
            InitType();   
        }

        public void InitData(List<EBMContent.Auxiliary> list)
        {
            foreach (var data in list)
            {
                bindList.Add(data);
            }
            dgvAuxiliaryData.DataSource = bindList;
        }

        public List<EBMContent.Auxiliary> GetData()
        {
            try
            {
                List<EBMContent.Auxiliary> list = new List<EBMContent.Auxiliary>();
                foreach (var v in bindList)
                {
                    list.Add(v);
                }
                return list;
            }
            catch
            {
                return null;
            }
        }

        public List<AuxiliaryData> GetAuxiliaryData()
        {
            try
            {
                List<AuxiliaryData> list = new List<AuxiliaryData>();
                foreach (var v in bindList)
                {
                    list.Add(v.GetAuxData());
                }
                return list;
            }
            catch
            {
                return null;
            }
        }

        private void InitType()
        {
            ComboBoxHelper.InitAuxiliaryDataType(cbBoxType);
        }



        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (bindList.Count > 1)
            {
                tip.Show("最多可添加两条辅助数据", this, btnAdd.Location.X, btnAdd.Location.Y - 30, 3000);
                return;
            }
            byte type = (byte)cbBoxType.SelectedValue;
            if (bindList.Count == 1)
            {
                if (!((bindList[0].Type >= 1 && bindList[0].Type <= 2) && (type >= 41 && type <= 43)) &&
                        !((bindList[0].Type >= 41 && bindList[0].Type <= 43) && (type >= 1 && type <= 2)))
                {
                    tip.Show("只能添加一个音视频文件或一个音频文件加一张图片或一张图片", this, btnAdd.Location.X, btnAdd.Location.Y - 30, 3000);
                    return;
                }
                //if ((bindList[0].Type >= 1 && bindList[0].Type <= 23) && (bindList[1].Type >= 1 && bindList[1].Type <= 23) ||
                //    (bindList[0].Type >= 21 && bindList[0].Type <= 43) && (bindList[1].Type >= 21 && bindList[1].Type <= 43))
                //{
                //    tip.Show("只能添加一个音视频文件或一个音频文件加一张图片或一张图片", this, btnAdd.Location.X, btnAdd.Location.Y - 30, 3000);
                //    return;
                //}
            }
            if (!string.IsNullOrWhiteSpace(textAuxiliaryData.Text) && cbBoxType.SelectedIndex >= 0)
            {
                FileInfo file = new FileInfo(textAuxiliaryData.Text.Trim());
                if (file.Length > 1048576)
                {
                    MessageBox.Show("添加文件限制大小为1MB");
                    return;
                }
                bindList.Add(new EBMContent.Auxiliary
                {
                    DisplayData = Path.GetFileName(textAuxiliaryData.Text.Trim()),
                    Type = type,
                });
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (dgvAuxiliaryData.SelectedRows.Count == 0)
            {
                tip.Show("未选中任何地址", this, btnDel.Location.X, btnDel.Location.Y - 30, 2000);
            }
            else
            {
                bindList.RemoveAt(dgvAuxiliaryData.SelectedRows[0].Index);
            }
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                textAuxiliaryData.Text = openFileDialog.FileName;
            }
        }

    }
}
