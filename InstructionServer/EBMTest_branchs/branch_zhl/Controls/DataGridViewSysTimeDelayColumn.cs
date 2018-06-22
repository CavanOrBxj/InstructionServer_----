using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EBMTest.Controls
{
    public class DataGridViewSysTimeDelayColumn : DataGridViewColumn
    {
        public DataGridViewSysTimeDelayColumn() : base(new DataGridViewSysTimeDelayCell())
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
                if (value != null && !value.GetType().IsAssignableFrom(typeof(DataGridViewSysTimeDelayCell)))
                {
                    throw new InvalidCastException("不是DataGridViewSysTimeDelayCell");
                }
                base.CellTemplate = value;
            }
        }

        public override object Clone()
        {
            DataGridViewSysTimeDelayColumn col = (DataGridViewSysTimeDelayColumn)base.Clone();
            return col;
        }
    }

    public class DataGridViewSysTimeDelayCell : DataGridViewTextBoxCell
    {
        private DataGridViewSysTimeDelayEditingControl ctl;
        private DateTime oldTime;

        public DataGridViewSysTimeDelayCell()
        {
        }

        public override void InitializeEditingControl(int rowIndex, object initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
        {
            base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);

            DataGridViewColumn dgvColumn = this.OwningColumn;
            if (dgvColumn is DataGridViewSysTimeDelayColumn)
            {
                ctl = DataGridView.EditingControl as DataGridViewSysTimeDelayEditingControl;
                if (ctl == null) return;
                ctl.Text = initialFormattedValue.ToString();
                if (ctl.OldTime != oldTime)
                {
                    ctl.OldTime = oldTime;
                }
            }
        }

        public DateTime OldTime
        {
            get { return ctl == null ? DateTime.Now : ctl.OldTime; }
            set
            {
                if (ctl != null) ctl.OldTime = value;
                oldTime = value;
            }
        }

        public override Type EditType
        {
            get
            {
                return typeof(DataGridViewSysTimeDelayEditingControl);
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

    public class DataGridViewSysTimeDelayEditingControl : UserControl, IDataGridViewEditingControl
    {
        protected int rowIndex;
        protected DataGridView dataGridView;
        protected bool valueChanged = false;
        private TextBox tb;
        private TextBox tbDelay;
        private Button btn;
        private ToolTip tip;
        public DateTime OldTime { get; set; }

        public DataGridViewSysTimeDelayEditingControl()
        {
            TabStop = false;
            OldTime = DateTime.Now;
            AddControl();
        }

        private void AddControl()
        {
            tip = new ToolTip();

            tb = new TextBox();
            tb.Width = ClientRectangle.Width - 100;
            tb.Location = new System.Drawing.Point(1, (ClientRectangle.Height - tb.Height) / 2 + ClientRectangle.Height / 30);
            tb.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            tb.Text = Text;
            tb.BorderStyle = BorderStyle.None;

            tbDelay = new TextBox();
            tbDelay.Width = 35;
            tbDelay.Location = new System.Drawing.Point(ClientRectangle.Width - 98, (ClientRectangle.Height - tbDelay.Height) / 2);
            tbDelay.Anchor = AnchorStyles.Right;
            tbDelay.Text = "0";
            tbDelay.BorderStyle = BorderStyle.FixedSingle;
            tbDelay.KeyPress += TbDelay_KeyPress;

            btn = new Button();
            btn.Size = new System.Drawing.Size(60, 23);
            btn.Location = new System.Drawing.Point(ClientRectangle.Width - 62, (ClientRectangle.Height - btn.Height) / 2);
            btn.Anchor = AnchorStyles.Right;
            btn.Text = "延时:分";
            btn.Click += Btn_Click;
            tip.SetToolTip(btn, "将开始时间加上指定分钟数");

            Controls.Add(tb);
            Controls.Add(tbDelay);
            Controls.Add(btn);
        }

        private void TbDelay_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 8)
            {
                e.Handled = false;
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

        private void Btn_Click(object sender, EventArgs e)
        {
            double delay = 0;
            if(!string.IsNullOrWhiteSpace(tbDelay.Text))
            {
                delay = Convert.ToDouble(tbDelay.Text.Trim());
            }
            Text = OldTime.AddMinutes(delay).ToString("yyyy-MM-dd HH:mm:ss");
            tbDelay.Text = delay.ToString();
        }

        protected override void OnTextChanged(EventArgs e)
        {
            tb.Text = Text;
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
