using System;
using System.Drawing;
using System.Windows.Forms;

namespace InstructionServer.Layouts
{
    public partial class AddConfigureBtn : UserControl
    {
        private Timer timer;
        private Timer checkOutTimer;

        public event EventHandler ConfigureTimeServiceClick;
        public event EventHandler ConfigureSetAddressClick;
        public event EventHandler ConfigureWorkModeClick;
        public event EventHandler ConfigureMainFreqClick;
        public event EventHandler ConfigureRebackClick;
        public event EventHandler ConfigureDefaltVolumnClick;
        public event EventHandler ConfigurePeriodClick;
        public event EventHandler ConfigureContentMoniterRetbackClick;
        public event EventHandler ConfigureRealMoniterClick;
        public event EventHandler ConfigureStatusRetbackClick;
        public event EventHandler ConfigureSoftwareUpGradeClick;
        public event EventHandler ConfigureRdsConfigClick;
        public event EventHandler ConfigureStatusRetbackGXClick;

        private int lineLenth = 5;
        private const int FINALHEIGHT = 402;

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

        public AddConfigureBtn()
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

            this.btnAdd.Click += new EventHandler(this.btnAdd_Click);
            this.btnAdd.Paint += new PaintEventHandler(this.btnAdd_Paint);
            //this.btnAdd.MouseEnter += new System.EventHandler(this.btnAdd_MouseEnter);
            this.btnAdd.MouseLeave += new EventHandler(this.btnAdd_MouseLeave);
            this.pnlAdd.MouseLeave += new EventHandler(this.pnlAdd_MouseLeave);
        }

        private void CheckOutTimer_Tick(object sender, EventArgs e)
        {
            if (!ClientRectangle.Contains(PointToClient(MousePosition)))
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
                Height = Height + 35 > FINALHEIGHT ? FINALHEIGHT : Height + 35;
                if (Height == FINALHEIGHT) timer.Stop();
            }
            else
            {
                Height = Height - 35 < 23 ? 23 : Height - 35;
                if (Height == 23) timer.Stop();
            }
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            UpOrDown = true;
            timer.Start();
            checkOutTimer.Start();
        }

        private void btnAdd_MouseEnter(object sender, EventArgs e)
        {
            UpOrDown = true;
            timer.Start();
            checkOutTimer.Start();
        }

        private void btnAdd_MouseLeave(object sender, EventArgs e)
        {
            if (!ClientRectangle.Contains(PointToClient(MousePosition)))
            {
                UpOrDown = false;
                timer.Start();
            }
        }

        private void pnlAdd_MouseLeave(object sender, EventArgs e)
        {
            if (!ClientRectangle.Contains(PointToClient(MousePosition)))
            {
                UpOrDown = false;
                timer.Start();
            }
        }

        private void btnTimeService_Click(object sender, EventArgs e)
        {
            UpOrDown = false;
            timer.Start();
            if (ConfigureTimeServiceClick != null) ConfigureTimeServiceClick(sender, e);
        }

        private void btnSetAddress_Click(object sender, EventArgs e)
        {
            UpOrDown = false;
            timer.Start();
            if (ConfigureSetAddressClick != null) ConfigureSetAddressClick(sender, e);
        }

        private void btnWorkMode_Click(object sender, EventArgs e)
        {
            UpOrDown = false;
            timer.Start();
            if (ConfigureWorkModeClick != null) ConfigureWorkModeClick(sender, e);
        }

        private void btnMainFreq_Click(object sender, EventArgs e)
        {
            UpOrDown = false;
            timer.Start();
            if (ConfigureMainFreqClick != null) ConfigureMainFreqClick(sender, e);
        }

        private void btnReback_Click(object sender, EventArgs e)
        {
            UpOrDown = false;
            timer.Start();
            if (ConfigureRebackClick != null) ConfigureRebackClick(sender, e);
        }

        private void btnDefaltVolumn_Click(object sender, EventArgs e)
        {
            UpOrDown = false;
            timer.Start();
            if (ConfigureDefaltVolumnClick != null) ConfigureDefaltVolumnClick(sender, e);
        }

        private void btnPeriod_Click(object sender, EventArgs e)
        {
            UpOrDown = false;
            timer.Start();
            if (ConfigurePeriodClick != null) ConfigurePeriodClick(sender, e);
        }

        private void btnContentMoniterRetback_Click(object sender, EventArgs e)
        {
            UpOrDown = false;
            timer.Start();
            if (ConfigureContentMoniterRetbackClick != null) ConfigureContentMoniterRetbackClick(sender, e);
        }

        private void btnRealMoniter_Click(object sender, EventArgs e)
        {
            UpOrDown = false;
            timer.Start();
            if (ConfigureRealMoniterClick != null) ConfigureRealMoniterClick(sender, e);
        }

        private void btnStatusRetback_Click(object sender, EventArgs e)
        {
            UpOrDown = false;
            timer.Start();
            if (ConfigureStatusRetbackClick != null) ConfigureStatusRetbackClick(sender, e);
        }

        private void btnSoftwareUpGrade_Click(object sender, EventArgs e)
        {
            UpOrDown = false;
            timer.Start();
            if (ConfigureSoftwareUpGradeClick != null) ConfigureSoftwareUpGradeClick(sender, e);
        }

        private void btnRdsConfig_Click(object sender, EventArgs e)
        {
            UpOrDown = false;
            timer.Start();
            if (ConfigureRdsConfigClick != null) ConfigureRdsConfigClick(sender, e);
        }

        private void btnStatusRetbackGX_Click(object sender, EventArgs e)
        {
            UpOrDown = false;
            timer.Start();
            if (ConfigureStatusRetbackGXClick != null) ConfigureStatusRetbackGXClick(sender, e);
        }
    }
}
