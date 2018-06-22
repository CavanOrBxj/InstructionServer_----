using System;
using System.Windows.Forms;

namespace EBMTest.Layouts
{
    public partial class RepeatTimesLayout : UserControl
    {
        private bool isConfig = false;
        public bool IsConfig
        {
            get { return isConfig; }
            set
            {
                if(isConfig!=value)
                {
                    isConfig = value;
                    Utils.ComboBoxHelper.InitRepeatTimesCombo(cbBoxRepeat, isConfig);
                    Width = value ? 115 : 235;
                }
            }
        }

        public RepeatTimesLayout()
        {
            InitializeComponent();
            InitTimesCombo();
        }

        private void InitTimesCombo()
        {
            Utils.ComboBoxHelper.InitRepeatTimesCombo(cbBoxRepeat);
        }

        private void cbBoxRepeat_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if ((int)cbBoxRepeat.SelectedValue == -1)
            {
                textRepeat.Text = "0";
                textRepeat.Visible = true;
            }
            else
            {
                textRepeat.Visible = false;
            }
        }

        public int GetRepeatTimes()
        {
            int times = 0;
            if ((int)cbBoxRepeat.SelectedValue != -1)
            {
                times = (int)cbBoxRepeat.SelectedValue;
            }
            else if ((int)cbBoxRepeat.SelectedValue == -1 && !string.IsNullOrWhiteSpace(textRepeat.Text))
            {
                times = int.Parse(textRepeat.Text.Trim());
            }
            else
            {
                times = 0;
            }
            return times;
        }

    }
}
