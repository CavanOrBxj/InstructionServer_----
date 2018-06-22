using EBMTable;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace EBMTest
{
    public partial class EBMMain : Form
    {
        private bool isAdminAccount = false;
        public bool AdminAccount
        {
            get { return isAdminAccount; }
            private set
            {
                if (isAdminAccount != value)
                {
                    isAdminAccount = value;
                    OnAdminAccountChanged();
                }
            }
        }
        public delegate void AdminAccountEventHandler(object sender, AdminAccountEventArgs e);
        public event AdminAccountEventHandler AdminAccountChanged;


        public bool IsStartStream { get; set; }
        bool isInitStream = false;
        EBMStream EbmStream;

        EBMIndex formIndex;
        EBMContent formContent;
        DailyBroadcast formDailyBroadcast;
        EBMConfigure formConfigure;
        EBMCertAuth formCertAuth;
        EBMStreamSet formStreamSet;

        EBIndexTable EB_Index_Table = new EBIndexTable();
        DailyBroadcastTable Daily_Broadcast_Table = new DailyBroadcastTable();
        //EBConfigureTable EB_Configure_Table;
        EBContentTable EB_Content_Table = new EBContentTable();
        EBCertAuthTable EB_CertAuth_Table = new EBCertAuthTable();


        public EBMStream EbMStream
        {
            get { return EbmStream; }
            set { EbmStream = value; }
        }

        public EBMMain()
        {
            InitializeComponent();
            UpdateFormTitle("");

            IsStartStream = false;
            EbmStream = new EBMStream();
            MenuItemTSSetting_Click(MenuItemTSSetting, EventArgs.Empty);
            formStreamSet.WindowState = FormWindowState.Minimized;

            InitEBStream();
            if (formIndex == null || formIndex.IsDisposed)
            {
                formIndex = new EBMIndex();
            }
            formIndex.MdiParent = this;
            formIndex.Visible = false;
            InitStreamTable();
        }

        private void OnAdminAccountChanged()
        {
            if (AdminAccountChanged != null)
            {
                AdminAccountChanged(this, new AdminAccountEventArgs(AdminAccount));
            }
            MenuItemEBContent.Visible = AdminAccount;
            MenuItemCertAuth.Visible = AdminAccount;
            MenuItemConfigure.Visible = AdminAccount;
            if (!AdminAccount)
            {
                if (formContent != null && formContent.Visible) formContent.Close();
                if (formConfigure != null && formConfigure.Visible) formConfigure.Close();
                if (formCertAuth != null && formCertAuth.Visible) formCertAuth.Close();
            }
        }

        public bool InitEBStream()
        {
            try
            {
                JObject jo = TableData.TableDataHelper.ReadConfig();
                if (jo != null)
                {
                    EbmStream.ElementaryPid = Convert.ToInt32(jo["ElementaryPid"].ToString());
                    EbmStream.Stream_id = Convert.ToInt32(jo["Stream_id"].ToString());
                    EbmStream.Program_id = Convert.ToInt32(jo["Program_id"].ToString());
                    EbmStream.PMT_Pid = Convert.ToInt32(jo["PMT_Pid"].ToString());
                    EbmStream.Section_length = Convert.ToInt32(jo["Section_length"].ToString());
                    EbmStream.sDestSockAddress = jo["sDestSockAddress"].ToString();
                    EbmStream.sLocalSockAddress = jo["sLocalSockAddress"].ToString();
                    EbmStream.Stream_BitRate = Convert.ToInt32(jo["Stream_BitRate"].ToString());
                }

                InitStreamTable();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void InitStreamTable()
        {
            //设置需要发送的表
            if (formDailyBroadcast != null)
            {
                EbmStream.Daily_Broadcast_Table = formDailyBroadcast.GetDailyBroadcastTable(ref Daily_Broadcast_Table) ? Daily_Broadcast_Table : null;
            }
            if (formIndex != null)
            {
                formIndex.GetEBIndexTable(ref EB_Index_Table);
                EbmStream.EB_Index_Table = EB_Index_Table;
            }
            //if (formConfigure != null)
            //{
            //    EbmStream.EB_Configure_Table = formConfigure.GetConfigureTable(ref EB_Configure_Table) ? EB_Configure_Table : null;
            //}
            if (formContent != null)
            {
                EbmStream.EB_Content_Table = formContent.GetContentTable(ref EB_Content_Table) ? EB_Content_Table : null;
            }
            if (formCertAuth != null)
            {
                EbmStream.EB_CertAuth_Table = formCertAuth.GetCertAuthTable(ref EB_CertAuth_Table) ? EB_CertAuth_Table : null;
            }

            EbmStream.Initialization();
            isInitStream = true;

            Thread thread = new Thread(UpdateDataText);
            thread.IsBackground = true;
            thread.Start();
        }

        public void UpdateDataText()
        {
            if (IsStartStream)
            {
                if (EbmStream.EB_Index_Table != null && formIndex != null && !formIndex.IsDisposed)
                {
                    StringBuilder sb = new StringBuilder(DateTime.Now.ToString() + "\n");
                    int num = 0;
                    EbmStream.EB_Index_Table.BuildEbIndexSection();
                    var body = EbmStream.EB_Index_Table.GetEbIndexSection(ref num);
                    for (int i = 0; i < body.Length; i++)
                    {
                        if (i != 0 && i % 16 == 0) sb.Append("\n");
                        sb.Append(body[i].ToString("X2").PadLeft(2, '0').ToUpper() + " ");
                    }
                    sb.Append("\n\n");
                    formIndex.BeginInvoke(new MethodInvoker(() =>
                    {
                        formIndex.AppendDataText(sb.ToString());
                    }));
                }

                if (EbmStream.EB_Content_Table != null && formContent != null && !formContent.IsDisposed)
                {
                    StringBuilder sb = new StringBuilder(DateTime.Now.ToString() + "\n");
                    int num = 0;
                    EbmStream.EB_Content_Table.BuildEBContentSection();
                    var body = EbmStream.EB_Content_Table.GetEBContentSection(ref num);
                    for (int i = 0; i < body.Length; i++)
                    {
                        if (i != 0 && i % 16 == 0) sb.Append("\n");
                        sb.Append(body[i].ToString("X2").PadLeft(2, '0').ToUpper() + " ");
                    }
                    sb.Append("\n\n");
                    formContent.BeginInvoke(new MethodInvoker(() =>
                    {
                        formContent.AppendDataText(sb.ToString());
                    }));
                }

                if (EbmStream.EB_Configure_Table != null && formConfigure != null && !formConfigure.IsDisposed)
                {
                    StringBuilder sb = new StringBuilder(DateTime.Now.ToString() + "\n");
                    int num = 0;
                    EbmStream.EB_Configure_Table.BuildEBConfigureSection();
                    var body = EbmStream.EB_Configure_Table.GetEBConfigureSection(ref num);
                    for (int i = 0; i < body.Length; i++)
                    {
                        if (i != 0 && i % 16 == 0) sb.Append("\n");
                        sb.Append(body[i].ToString("X2").PadLeft(2, '0').ToUpper() + " ");
                    }
                    sb.Append("\n\n");
                    formConfigure.BeginInvoke(new MethodInvoker(() =>
                    {
                        formConfigure.AppendDataText(sb.ToString());
                    }));
                }

                if (EbmStream.EB_CertAuth_Table != null && formCertAuth != null && !formCertAuth.IsDisposed)
                {
                    StringBuilder sb = new StringBuilder(DateTime.Now.ToString() + "\n");
                    int num = 0;
                    EbmStream.EB_CertAuth_Table.BuildEBCertAuthSection();
                    var body = EbmStream.EB_CertAuth_Table.GetEBCertAuthSection(ref num);
                    for (int i = 0; i < body.Length; i++)
                    {
                        if (i != 0 && i % 16 == 0) sb.Append("\n");
                        sb.Append(body[i].ToString("X2").PadLeft(2, '0').ToUpper() + " ");
                    }
                    sb.Append("\n\n");
                    formCertAuth.BeginInvoke(new MethodInvoker(() =>
                    {
                        formCertAuth.AppendDataText(sb.ToString());
                    }));
                }

                if (EbmStream.Daily_Broadcast_Table != null && formDailyBroadcast != null && !formDailyBroadcast.IsDisposed)
                {
                    StringBuilder sb = new StringBuilder(DateTime.Now.ToString() + "\n");
                    int num = 0;
                    EbmStream.Daily_Broadcast_Table.BuildDailyBroadcastSection();
                    var body = EbmStream.Daily_Broadcast_Table.GetDailyBroadcastSection(ref num);
                    for (int i = 0; i < body.Length; i++)
                    {
                        if (i != 0 && i % 16 == 0) sb.Append("\n");
                        sb.Append(body[i].ToString("X2").PadLeft(2, '0').ToUpper() + " ");
                    }
                    sb.Append("\n\n");
                    formDailyBroadcast.BeginInvoke(new MethodInvoker(() =>
                    {
                        formDailyBroadcast.AppendDataText(sb.ToString());
                    }));
                }
            }
        }

        #region MenuItem

        private void MenuItemStartStream_Click(object sender, EventArgs e)
        {
            InitEBStream();
            if (EbmStream != null && isInitStream && !IsStartStream)
            {
                EbmStream.StartStreaming(); 
                IsStartStream = true;
                if (formDailyBroadcast != null && !formDailyBroadcast.IsDisposed)
                {
                    formDailyBroadcast.InitColumnStop(true);
                }

                Thread thread = new Thread(UpdateDataText);
                thread.IsBackground = true;
                thread.Start();
            }
        }

        private void MenuItemStopStream_Click(object sender, EventArgs e)
        {
            if (EbmStream != null && IsStartStream)
            {
                EbmStream.StopStreaming();
                IsStartStream = false;
                if (formDailyBroadcast != null && !formDailyBroadcast.IsDisposed)
                {
                    formDailyBroadcast.InitColumnStop(false);
                }
            }
        }

        private void MenuItemEBIndex_Click(object sender, EventArgs e)
        {
            if (formIndex == null || formIndex.IsDisposed)
            {
                formIndex = new EBMIndex();
            }
            formIndex.MdiParent = this;
            formIndex.Show();
            if (formIndex.WindowState == FormWindowState.Minimized)
                formIndex.WindowState = FormWindowState.Normal;
            formIndex.BringToFront();
        }

        private void MenuItemEBContent_Click(object sender, EventArgs e)
        {
            if(formContent == null || formContent.IsDisposed)
            {
                formContent = new EBMContent();
            }
            formContent.MdiParent = this;
            formContent.Show();
            if (formContent.WindowState == FormWindowState.Minimized)
                formContent.WindowState = FormWindowState.Normal;
            formContent.BringToFront();
        }

        private void MenuItemDailyBroadcast_Click(object sender, EventArgs e)
        {
            if (formDailyBroadcast == null || formDailyBroadcast.IsDisposed)
            {
                formDailyBroadcast = new DailyBroadcast();
            }
            formDailyBroadcast.MdiParent = this;
            formDailyBroadcast.Show();
            if (formDailyBroadcast.WindowState == FormWindowState.Minimized)
                formDailyBroadcast.WindowState = FormWindowState.Normal;
            formDailyBroadcast.BringToFront();
        }

        private void MenuItemConfigure_Click(object sender, EventArgs e)
        {
            if (formConfigure == null || formConfigure.IsDisposed)
            {
                formConfigure = new EBMConfigure();
            }
            formConfigure.MdiParent = this;
            formConfigure.Show();
            if (formConfigure.WindowState == FormWindowState.Minimized)
                formConfigure.WindowState = FormWindowState.Normal;
            formConfigure.BringToFront();
        }

        private void MenuItemCertAuth_Click(object sender, EventArgs e)
        {
            if (formCertAuth == null || formCertAuth.IsDisposed)
            {
                formCertAuth = new EBMCertAuth();
            }
            formCertAuth.MdiParent = this;
            formCertAuth.Show();
            if (formCertAuth.WindowState == FormWindowState.Minimized)
                formCertAuth.WindowState = FormWindowState.Normal;
            formCertAuth.BringToFront();
        }

        private void MenuItemTSSetting_Click(object sender, EventArgs e)
        {
            if (formStreamSet == null || formStreamSet.IsDisposed)
            {
                formStreamSet = new EBMStreamSet();
            }
            formStreamSet.MdiParent = this;
            formStreamSet.Show();
            if (formStreamSet.WindowState == FormWindowState.Minimized)
                formStreamSet.WindowState = FormWindowState.Normal;
            formStreamSet.BringToFront();
        }

        private void Operate_ToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            MenuItemStartStream.Enabled = EbmStream != null && isInitStream && !IsStartStream;
            MenuItemStopStream.Enabled = EbmStream != null && IsStartStream;
        }

        private void MenuItemTileH_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void MenuItemTileV_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void MenuItemCascade_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void ReStartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!AdminAccount)
            {
                if (new EBMLogIn().ShowDialog() == DialogResult.OK)
                {
                    AdminAccount = true;
                }
            }
            else
            {
                if (MessageBox.Show(this, "是否退出管理员账户？", "温馨提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    AdminAccount = false;
                }
            }
        }

        #endregion

        private void EBMMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (EbmStream != null && IsStartStream)
            {
                EbmStream.StopStreaming();
                IsStartStream = false;
            }
        }

        private void MenuItemPlayVideo_Click(object sender, EventArgs e)
        {
            Forms.FormMediaStreamer form = new Forms.FormMediaStreamer();
            form.MdiParent = this;
            form.Show();
            form.BringToFront();
        }

        private void lblLogIn_DoubleClick(object sender, EventArgs e)
        {
            if (!AdminAccount)
            {
                if (new EBMLogIn().ShowDialog() == DialogResult.OK)
                {
                    AdminAccount = true;
                }
            }
            else
            {
                if (MessageBox.Show(this, "是否退出管理员账户？", "温馨提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    AdminAccount = false;
                }
            }
        }

        public void UpdateFormTitle(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                Text = string.Join(" - ", System.Configuration.ConfigurationManager.AppSettings["EBMTitle"], Application.ProductVersion);
            }
            else
            {
                Text = string.Join(" - ", System.Configuration.ConfigurationManager.AppSettings["EBMTitle"], Application.ProductVersion, text);
            }
        }
    }

}


