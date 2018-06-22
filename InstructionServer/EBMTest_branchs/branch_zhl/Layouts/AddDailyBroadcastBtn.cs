using System;
using System.Drawing;
using System.Windows.Forms;

namespace EBMTest.Layouts
{
    public partial class AddDailyBroadcastBtn : UserControl
    {
        private Timer timer;
        private Timer checkOutTimer;

        public event EventHandler DailyChangeProgramClick;
        public event EventHandler DailyPlayCtrlClick;
        public event EventHandler DailyOutSwitchClick;
        public event EventHandler DailyRdsTransferClick;

        private int lineLenth = 5;

        private bool upOrDown = false;
        private bool UpOrDown
        {
            get { return upOrDown; }
            set
            {
                upOrDown = value;
                btnAdd.Invalidate();
            }
        }

        public AddDailyBroadcastBtn()
        {
            InitializeComponent();
            Size = btnAdd.Size;
            btnAdd.Padding = new Padding(0, 0, 8, 0);

            timer = new Timer();
            timer.Interval = 5;
            timer.Tick += timer_Tick;

            checkOutTimer = new Timer();
            checkOutTimer.Interval = 1000;
            checkOutTimer.Tick += CheckOutTimer_Tick;
        }

        private void CheckOutTimer_Tick(object sender, EventArgs e)
        {
            if(!ClientRectangle.Contains(PointToClient(MousePosition)))
            {
                UpOrDown = false;
                timer.Start();
                checkOutTimer.Stop();
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (UpOrDown)
            {
                Height = Height + 10 > 143 ? 143 : Height + 10;
                if (Height == 143) timer.Stop();
            }
            else
            {
                Height = Height - 10 < 23 ? 23 : Height - 10;
                if (Height == 23) timer.Stop();
            }
        }

        private void OnChangeProgramClick(object sender, EventArgs e)
        {
            if (DailyChangeProgramClick != null) DailyChangeProgramClick(sender, e);
        }

        private void OnPlayCtrlClick(object sender, EventArgs e)
        {
            if (DailyPlayCtrlClick != null) DailyPlayCtrlClick(sender, e);
        }

        private void OnOutSwitchClick(object sender, EventArgs e)
        {
            if (DailyOutSwitchClick != null) DailyOutSwitchClick(sender, e);
        }

        private void OnRdsTransferClick(object sender, EventArgs e)
        {
            if (DailyRdsTransferClick != null) DailyRdsTransferClick(sender, e);
        }

        private void btnChangeProgram_Click(object sender, EventArgs e)
        {
            UpOrDown = false;
            timer.Start();
            OnChangeProgramClick(sender, e);
        }

        private void btnPlayCtrl_Click(object sender, EventArgs e)
        {
            UpOrDown = false;
            timer.Start();
            OnPlayCtrlClick(sender, e);
        }

        private void btnOutSwitch_Click(object sender, EventArgs e)
        {
            UpOrDown = false;
            timer.Start();
            OnOutSwitchClick(sender, e);
        }

        private void btnRdsTransfer_Click(object sender, EventArgs e)
        {
            UpOrDown = false;
            timer.Start();
            OnRdsTransferClick(sender, e);
        }

        private void pnlAdd_MouseLeave(object sender, EventArgs e)
        {
            if (!pnlAdd.ClientRectangle.Contains(PointToClient(MousePosition)) &&
               !btnAdd.ClientRectangle.Contains(PointToClient(MousePosition)))
            {
                UpOrDown = false;
                timer.Start();
            }
        }

        private void btnAdd_MouseLeave(object sender, EventArgs e)
        {
            if (!pnlAdd.ClientRectangle.Contains(PointToClient(MousePosition)) &&
                !btnAdd.ClientRectangle.Contains(PointToClient(MousePosition)))
            {
                UpOrDown = false;
                timer.Start();
            }
        }

        private void btnAdd_MouseEnter(object sender, EventArgs e)
        {
            UpOrDown = true;
            timer.Start();
            checkOutTimer.Start();
        }

        private void btnAdd_Paint(object sender, PaintEventArgs e)
        {
            SizeF textSize = e.Graphics.MeasureString(btnAdd.Text, btnAdd.Font);
            float textRight = btnAdd.Width - (btnAdd.Width - textSize.Width) / 2 - 5;
            if (btnAdd.Width > textRight + lineLenth)
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                if (UpOrDown)
                {
                    e.Graphics.DrawLines(new Pen(Color.Black, 1.8f),
                        new Point[] {
                        new Point((int)(textRight), btnAdd.Height - (btnAdd.Height - lineLenth) / 2),
                        new Point((int)(textRight + lineLenth), (btnAdd.Height - lineLenth) / 2),
                        new Point((int)(textRight + 2 * lineLenth), btnAdd.Height - (btnAdd.Height - lineLenth) / 2),
                        });
                }
                else
                {
                    e.Graphics.DrawLines(new Pen(Color.Black, 1.8f),
                        new Point[] {
                        new Point((int)(textRight), (btnAdd.Height - lineLenth) / 2),
                        new Point((int)(textRight + lineLenth), btnAdd.Height - (btnAdd.Height - lineLenth) / 2),
                        new Point((int)(textRight + 2 * lineLenth), (btnAdd.Height - lineLenth) / 2),
                        });
                }
            }
        }

    }
}
