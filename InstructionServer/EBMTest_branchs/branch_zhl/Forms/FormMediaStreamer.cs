using ControlAstro.Controls;
using Microsoft.Win32.SafeHandles;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Pipes;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace EBMTest.Forms
{
    public partial class FormMediaStreamer : Form
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern SafeWaitHandle CreateEvent(IntPtr lpSecurityAttributes, bool isManualReset, bool initialState, string name);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SetEvent(SafeWaitHandle handle);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool ResetEvent(SafeWaitHandle handle);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int CloseHandle(SafeWaitHandle handle);

        private SafeWaitHandle EventHandle;
        private const string MediaStreamerPath = @"CCPlayer\cppPlayer.exe";
        private NamedPipeServerStream pipeServer;
        private ToolTipEx tooltip;

        private string patternIP = @"^(([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])\.){3}([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])$";
        private string patternPort = @"^([0-9]|[1-9]\d|[1-9]\d{2}|[1-9]\d{3}|[1-5]\d{4}|6[0-4]\d{3}|65[0-4]\d{2}|655[0-2]\d|6553[0-5])$";

        public FormMediaStreamer()
        {
            InitializeComponent();
            tooltip = new ToolTipEx();
            tooltip.BackColor = Color.White;
            InitMediaConfig();
        }

        private void InitMediaConfig()
        {
            object mediaConfig;
            TableData.TableDataHelper.ReadAppConfig("MediaStreamer", out mediaConfig);
            if (mediaConfig != null)
            {
                try
                {
                    JObject mediaJo = JObject.Parse(mediaConfig.ToString());
                    textPath.Text = mediaJo["MediaPath"].ToString();
                    textIP.Text = mediaJo["MediaIP"].ToString();
                    textPort.Text = mediaJo["MediaPort"].ToString();
                    textAudioPID.Text = mediaJo["MediaAudioPID"].ToString();
                    textVideoPID.Text = mediaJo["MediaVideoPID"].ToString();
                    textVideoRate.Text = mediaJo["MediaVideoRate"].ToString();
                    textAudioRate.Text = mediaJo["MediaAudioRate"].ToString();
                }
                catch { }
            }
        }

        /// <summary>
        /// 开启文件推流
        /// </summary>
        private void OpenMediaStreamer(object param)
        {
            try
            {
                string[] args = param as string[];
                string argument = string.Join(" ", args);
                pipeServer = new NamedPipeServerStream(args[6]);
                EventHandle = CreateEvent(IntPtr.Zero, true, true, args[4]);
                ResetEvent(EventHandle);

                string file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, MediaStreamerPath);
                string path = Path.GetDirectoryName(file);
                Process process = new Process();
                process.StartInfo.FileName = "cppPlayer.exe";
                process.StartInfo.WorkingDirectory = path;
                process.StartInfo.Arguments = argument;
                process.Start();

                Thread.Sleep(500);
                if (Process.GetProcessesByName("cppPlayer").Length > 0)
                {
                    Invoke(new MethodInvoker(() =>
                    {
                        SizeF size = CreateGraphics().MeasureString("推流成功", SystemFonts.DefaultFont);
                        size.Width += 50;
                        size.Height += 30;
                        tooltip.Size = size.ToSize();
                        tooltip.Show("推流成功", this, (Width - tooltip.Size.Width) / 2, (Height - tooltip.Size.Height) / 2, 2000);
                        btnOK.Text = "停止播放音频";
                    }));
                }

                Thread.Sleep(1500);

                try
                {
                    pipeServer.WaitForConnection();
                    using (StreamReader sr = new StreamReader(pipeServer))
                    {
                        string line;
                        while ((line = sr.ReadLine()) != "-1000")
                        {
                            //Console.WriteLine(line);
                        }
                    }
                }
                catch { }
                SetEvent(EventHandle);
                pipeServer.Close();
                CloseHandle(EventHandle);
                Thread.Sleep(200);
                StartMediaStream();
            }
            catch
            {
            }
        }

        private void Process_Exited(object sender, EventArgs e)
        {
            Process p = sender as Process;
            p.Close();
        }

        private void lblFile_Click(object sender, EventArgs e)
        {
            if(fileDialog.ShowDialog() == DialogResult.OK)
            {
                textPath.Text = fileDialog.FileName.Replace('\\', '/');
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (btnOK.Text == "开始播放音频")
            {
                if (!ValidateMediaParam()) return;
                StartMediaStream();
                if (MdiParent != null)
                {
                    (MdiParent as EBMMain).UpdateFormTitle(textIP.Text.Trim() + ":" + textPort.Text.Trim());
                }
                SaveStreamConfig();
            }
            else
            {
                if (EventHandle != null)
                {
                    SetEvent(EventHandle);
                }
                btnOK.Text = "开始播放音频";
                if (MdiParent != null)
                {
                    (MdiParent as EBMMain).UpdateFormTitle("");
                }
            }
        }

        private void StartMediaStream()
        {
            string pipeName = Guid.NewGuid().ToString();
            string eventName = "Global\\" + pipeName;
            string[] param = new string[]
            {
                    Uri.EscapeUriString("file:///" + textPath.Text.Trim().Replace('\\', '/')),
                    textIP.Text.Trim(),
                    textPort.Text.Trim(),
                    textAudioPID.Text.Trim(),
                    eventName,
                    textVideoPID.Text.Trim(),
                    pipeName,
                    textVideoRate.Text.Trim(),
                    textAudioRate.Text.Trim(),
            };
            Thread thread = new Thread(new ParameterizedThreadStart(OpenMediaStreamer));
            thread.Start(param);
        }

        private bool ValidateMediaParam()
        {
            foreach (Control c in Controls)
            {
                if (c is TextBox && c.Name != "textOrder")
                {
                    if (string.IsNullOrWhiteSpace((c as TextBox).Text))
                    {
                        MessageBox.Show(c.Tag.ToString() + "未填写");
                        return false;
                    }
                }
            }
            if (!Regex.IsMatch(textIP.Text.Trim(), patternIP))
            {
                MessageBox.Show("IP地址有误，请重新填写");
                return false;
            }
            if (!Regex.IsMatch(textPort.Text.Trim(), patternPort))
            {
                MessageBox.Show("端口有误，请重新填写");
                return false;
            }
            return true;
        }

        private void FormMediaStreamer_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveStreamConfig();
            try
            {
                if (EventHandle != null)
                {
                    SetEvent(EventHandle);
                    CloseHandle(EventHandle);
                }
            }
            catch { }
            if (pipeServer != null)
            {
                if (pipeServer.IsConnected) pipeServer.Disconnect();
                pipeServer.Close();
                pipeServer.Dispose();
                pipeServer = null;
            }
            tooltip.Dispose();
        }

        private void SaveStreamConfig()
        {
            JObject mediaJo = new JObject();
            mediaJo["MediaPath"] = textPath.Text.Trim();
            mediaJo["MediaIP"] = textIP.Text.Trim();
            mediaJo["MediaPort"] = textPort.Text.Trim();
            mediaJo["MediaAudioPID"] = textAudioPID.Text.Trim();
            mediaJo["MediaVideoPID"] = textVideoPID.Text.Trim();
            mediaJo["MediaVideoRate"] = textVideoRate.Text.Trim();
            mediaJo["MediaAudioRate"] = textAudioRate.Text.Trim();
            TableData.TableDataHelper.WriteAppConfig("MediaStreamer", mediaJo);
        }

        private void FormMediaStreamer_Paint(object sender, PaintEventArgs e)
        {
            PaintLblLine(lblFileInfo, e.Graphics);
            PaintLblLine(lblOrderInfo, e.Graphics);
        }

        private void PaintLblLine(Control lbl, Graphics g)
        {
            Point pointStart = new Point(10, lbl.Height / 2 + lbl.Location.Y);
            Point pointStop = new Point(Width - 20, lbl.Height / 2 + lbl.Location.Y);
            g.DrawLine(Pens.Gray, pointStart, pointStop);
        }

        private void btnSendOrder_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrWhiteSpace(textOrder.Text))
            {
                try
                {
                    if (pipeServer.IsConnected)
                    {
                        using (StreamWriter sw = new StreamWriter(pipeServer))
                        {
                            sw.WriteLine(textOrder.Text.Trim());
                            sw.Flush();
                        }
                    }
                }
                catch
                {

                }
            }
        }
    }
}
