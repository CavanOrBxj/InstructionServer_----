using EBMTable;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace EBMTest
{
    public partial class EBMStreamSet : Form
    {
        private string pattern = @"^(([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])\.){3}([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5]):([0-9]|[1-9]\d|[1-9]\d{2}|[1-9]\d{3}|[1-5]\d{4}|6[0-4]\d{3}|65[0-4]\d{2}|655[0-2]\d|6553[0-5])$";

        public EBMStreamSet()
        {
            InitializeComponent();
            InitConfig();
        }

        private void InitConfig()
        {
            JObject jo = TableData.TableDataHelper.ReadConfig();
            if (jo != null)
            {
                textElementaryPid.Text = jo["ElementaryPid"].ToString();
                textStream_id.Text = jo["Stream_id"].ToString();
                textProgram_id.Text = jo["Program_id"].ToString();
                textPMT_Pid.Text = jo["PMT_Pid"].ToString();
                textSection_length.Text = jo["Section_length"].ToString();
                textsDestSockAddress.Text = jo["sDestSockAddress"].ToString();
                textsLocalSockAddress.Text = jo["sLocalSockAddress"].ToString();
                textStream_BitRate.Text = jo["Stream_BitRate"].ToString();
            }
            else
            {
                textElementaryPid.Text = "33";
                textStream_id.Text = "1";
                textProgram_id.Text = "1";
                textPMT_Pid.Text = "48";
                textSection_length.Text = "4096";
                textsDestSockAddress.Text = "192.168.4.118:8002";
                textsLocalSockAddress.Text = "127.0.0.1:0";
                textStream_BitRate.Text = "30000";
                bool isSaveConfig = TableData.TableDataHelper.WriteConfig(GetEBMStream());
                if (!isSaveConfig)
                    Utils.EBMLogHelper.Error(GetType().Name, "配置写入失败");
            }
        }

        private EBMStream GetEBMStream()
        {
            EBMStream stream = new EBMStream();
            stream.ElementaryPid = Convert.ToInt32(textElementaryPid.Text.Trim());
            stream.PMT_Pid = Convert.ToInt32(textPMT_Pid.Text.Trim());
            stream.Program_id = Convert.ToInt32(textProgram_id.Text.Trim());
            stream.sDestSockAddress = textsDestSockAddress.Text.Trim();
            stream.Section_length = Convert.ToInt32(textSection_length.Text.Trim());
            stream.sLocalSockAddress = textsLocalSockAddress.Text.Trim();
            stream.Stream_BitRate = Convert.ToInt32(textStream_BitRate.Text.Trim());
            stream.Stream_id = Convert.ToInt32(textStream_id.Text.Trim());
            return stream;
        }

        public void GetEBMStream(ref EBMStream stream)
        {
            stream.ElementaryPid = Convert.ToInt32(textElementaryPid.Text.Trim());
            stream.PMT_Pid = Convert.ToInt32(textPMT_Pid.Text.Trim());
            stream.Program_id = Convert.ToInt32(textProgram_id.Text.Trim());
            stream.sDestSockAddress = textsDestSockAddress.Text.Trim();
            stream.Section_length = Convert.ToInt32(textSection_length.Text.Trim());
            stream.sLocalSockAddress = textsLocalSockAddress.Text.Trim();
            stream.Stream_BitRate = Convert.ToInt32(textStream_BitRate.Text.Trim());
            stream.Stream_id = Convert.ToInt32(textStream_id.Text.Trim());
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (ValidatData())
            {
                bool isSaveConfig = TableData.TableDataHelper.WriteConfig(GetEBMStream());
                if (!isSaveConfig)
                    Utils.EBMLogHelper.Error(GetType().Name, "配置写入失败");
            }
        }

        private void EBMStreamSet_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ValidatData())
            {
                bool isSaveConfig = TableData.TableDataHelper.WriteConfig(GetEBMStream());
                if (!isSaveConfig)
                    Utils.EBMLogHelper.Error(GetType().Name, "配置写入失败");
            }
            else
            {
                e.Cancel = true;
            }
        }

        private bool ValidatData()
        {
            var matched1 = Regex.IsMatch(textsDestSockAddress.Text.Trim(), pattern);
            var matched2 = Regex.IsMatch(textsLocalSockAddress.Text.Trim(), pattern);
            if(matched1 && matched2)
            {
                return true;
            }
            else
            {
                MessageBox.Show("IP地址和端口请按如下格式输入：x.x.x.x:x");
                return false;
            }
        }

    }
}
