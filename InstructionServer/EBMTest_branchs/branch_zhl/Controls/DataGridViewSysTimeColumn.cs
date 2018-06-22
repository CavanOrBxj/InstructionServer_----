using System;
using System.Windows.Forms;

namespace EBMTest.Controls
{
    public class DataGridViewSysTimeColumn : DataGridViewColumn
    {
        public DataGridViewSysTimeColumn() : base(new DataGridViewSysTimeCell())
        {
        }

        public override DataGridViewCell CellTemplate
        {
            get
            {
                return base.CellTemplate;
            }
            set
            {
                if (value != null && !value.GetType().IsAssignableFrom(typeof(DataGridViewSysTimeCell)))
                {
                    throw new InvalidCastException("不是DataGridViewSysTimeCell");
                }
                base.CellTemplate = value;
            }
        }

        public override object Clone()
        {
            DataGridViewSysTimeColumn col = (DataGridViewSysTimeColumn)base.Clone();
            return col;
        }
    }

    public class DataGridViewSysTimeCell : DataGridViewTextBoxCell
    {
        public DataGridViewSysTimeCell()
        {
        }

        public override void InitializeEditingControl(int rowIndex, object initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
        {
            base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);

            DataGridViewColumn dgvColumn = this.OwningColumn;
            if (dgvColumn is DataGridViewSysTimeColumn)
            {
                DataGridViewSysTimeEditingControl ctl = DataGridView.EditingControl as DataGridViewSysTimeEditingControl;
                if (ctl == null) return;
                ctl.Text = initialFormattedValue.ToString();
                ctl.TBText = initialFormattedValue.ToString();
            }
        }

        public override Type EditType
        {
            get
            {
                return typeof(DataGridViewSysTimeEditingControl);
            }
        }

        public override Type ValueType
        {
            get
            {
                return typeof(string);
            }
        }

        public override object DefaultNewRowValue
        {
            get
            {
                return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }
        }

    }

    public class DataGridViewSysTimeEditingControl : UserControl, IDataGridViewEditingControl
    {
        protected int rowIndex;
        protected DataGridView dataGridView;
        protected bool valueChanged = false;
        private TextBox tb;
        private Button btn;
        private ToolTip tip;

        public string TBText
        {
            get { return tb.Text; }
            set { tb.Text = value; }
        }

        public DataGridViewSysTimeEditingControl()
        {
            TabStop = false;
            AddControl();
        }

        private void AddControl()
        {
            tip = new ToolTip();

            tb = new TextBox();
            tb.Width = ClientRectangle.Width - 25;
            tb.Location = new System.Drawing.Point(1, (ClientRectangle.Height - tb.Height) / 2 + ClientRectangle.Height / 30);
            tb.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            tb.Text = Text;
            tb.TextChanged += Tb_TextChanged;

            btn = new Button();
            btn.Size = new System.Drawing.Size(20, 20);
            btn.Location = new System.Drawing.Point(ClientRectangle.Width - 22, (ClientRectangle.Height - btn.Height) / 2);
            btn.Anchor = AnchorStyles.Right;
            btn.Click += Btn_Click;
            tip.SetToolTip(btn, "获取系统时间");

            Controls.Add(tb);
            Controls.Add(btn);
        }

        private void Tb_TextChanged(object sender, EventArgs e)
        {
            Text = tb.Text;
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            tb.Text = Text;
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            NotifyDataGridViewOfValueChange();
        }

        private void NotifyDataGridViewOfValueChange()
        {
            valueChanged = true;
            dataGridView.NotifyCurrentCellDirty(true);
        }

        #region IDataGridViewEditingControl接口的實現
        /// <summary>
        /// 獲取或設置儲存格所在的DataGridView
        /// </summary>
        public DataGridView EditingControlDataGridView
        {
            get
            {
                return dataGridView;
            }
            set
            {
                dataGridView = value;
            }
        }

        /// <summary>
        /// 獲取或設置儲存格格式化後的值
        /// </summary>
        public object EditingControlFormattedValue
        {
            set
            {
                Text = value.ToString();
                NotifyDataGridViewOfValueChange();
            }
            get
            {
                return this.Text;
            }

        }

        /// <summary>
        /// 取得或設定值，指出每當值變更時儲存格內容是否需要重新調整位置。
        /// </summary>
        public virtual bool RepositionEditingControlOnValueChange
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// 取得或設定儲存格所在行
        /// </summary>
        public int EditingControlRowIndex
        {
            get
            {
                return this.rowIndex;
            }
            set
            {
                this.rowIndex = value;
            }
        }

        /// <summary>
        /// 取得或設定值，指出編輯控制項的值是否與裝載儲存格的值不同。
        /// </summary>
        public bool EditingControlValueChanged
        {
            get
            {
                return valueChanged;
            }
            set
            {
                this.valueChanged = value;
            }
        }

        /// <summary>
        /// 在Cell被編輯的時候光標顯示
        /// </summary>
        public Cursor EditingPanelCursor
        {
            get
            {
                return Cursors.Default;
            }
        }

        /// <summary>
        /// 擷取儲存格的格式化後的值。
        /// </summary>
        /// <param name="context">錯誤上下文</param>
        /// <returns></returns>
        public virtual object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context)
        {
            return Text;
        }

        /// <summary>
        /// 準備目前所選的儲存格來編輯。
        /// </summary>
        /// <param name="selectAll"></param>
        public void PrepareEditingControlForEdit(bool selectAll)
        {
        }

        /// <summary>
        /// 變更控制項的使用者介面 (UI)，使其與指定的儲存格樣式一致。
        /// </summary>
        /// <param name="dataGridViewCellStyle"></param>
        public void ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle)
        {
            Font = dataGridViewCellStyle.Font;
            ForeColor = dataGridViewCellStyle.ForeColor;
        }

        /// <summary>
        /// 判斷指定的按鍵是否為編輯控制項應該處理的標準輸入按鍵，或是 System.Windows.Forms.DataGridView 應該處理的特殊按鍵。
        /// </summary>
        /// <param name="keyData"></param>
        /// <param name="dataGridViewWantsInputKey"></param>
        /// <returns></returns>
        public bool EditingControlWantsInputKey(Keys keyData, bool dataGridViewWantsInputKey)
        {
            switch (keyData & Keys.KeyCode)
            {
                case Keys.Left:
                case Keys.Up:
                case Keys.Down:
                case Keys.Right:
                case Keys.Home:
                case Keys.End:
                case Keys.PageDown:
                case Keys.PageUp:
                    return true;
                default:
                    return false;
            }
        }

        #endregion

    }
}
