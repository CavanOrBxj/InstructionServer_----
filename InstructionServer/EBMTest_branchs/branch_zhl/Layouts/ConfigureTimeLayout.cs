using EBMTable;
using System;
using System.Windows.Forms;

namespace EBMTest.Layouts
{
    public partial class ConfigureTimeLayout : UserControl
    {
        private EBMConfigure.TimeService Time;

        public ConfigureTimeLayout()
        {
            InitializeComponent();
        }

        public void InitData(object config)
        {
            Time = config as EBMConfigure.TimeService;
            checkBox.Checked = Time.GetSystemTime;
            DateTime time = Convert.ToDateTime(Time.TimeSer);
            dateTimePicker.Value = time;
        }

        public EBMConfigure.TimeService GetData()
        {
            try
            {
                if (Time == null)
                {
                    Time = new EBMConfigure.TimeService();
                }
                EBConfigureTimeService config = new EBConfigureTimeService();
                Time.Configure = config;
                Time.TimeSer = dateTimePicker.Text;
                Time.GetSystemTime = checkBox.Checked;
                return Time;
            }
            catch
            {
                return null;
            }
        }

        public bool ValidatData()
        {
            foreach (Control c in Controls)
            {
                if (c is TextBox)
                {
                    if (string.IsNullOrWhiteSpace(c.Text))
                    {
                        MessageBox.Show("\"时间\"不允许为空，请检查并填写");
                        return false;
                    }
                }
            }
            return true;
        }

        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox.Checked)
            {
                DateTime time = DateTime.Now;
                dateTimePicker.Value = time;
            }
        }
    }
}
