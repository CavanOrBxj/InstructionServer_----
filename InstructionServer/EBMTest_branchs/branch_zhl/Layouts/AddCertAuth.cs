using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EBMTest.Layouts
{
    public partial class AddCertAuth : UserControl
    {
        private Timer timer;
        private Timer checkOutTimer;

        public event EventHandler CertClick;
        public event EventHandler CertAuthClick;

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

        public AddCertAuth()
        {
            InitializeComponent();
            Size = btnAdd.Size;
            btnAdd.Padding = new Padding(0, 0, 8, 0);

            timer = new Timer();
            timer.Interval = 10;
            timer.Tick += timer_Tick;

            checkOutTimer = new Timer();
            checkOutTimer.Interval = 1000;
            checkOutTimer.Tick += CheckOutTimer_Tick;
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
                Height = Height + 10 > 85 ? 85 : Height + 10;
                if (Height == 85) timer.Stop();
            }
            else
            {
                Height = Height - 10 < 23 ? 23 : Height - 10;
                if (Height == 23) timer.Stop();
            }
        }

        private void btnCert_Click(object sender, EventArgs e)
        {
            if (CertClick != null) CertClick(sender, e);
        }

        private void btnCertAuth_Click(object sender, EventArgs e)
        {
            if (CertAuthClick != null) CertAuthClick(sender, e);
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
                    e.Graphics.DrawLines(new Pen(Color.Black, 1.7f),
                        new Point[] {
                        new Point((int)(textRight), btnAdd.Height - (btnAdd.Height - lineLenth) / 2),
                        new Point((int)(textRight + lineLenth), (btnAdd.Height - lineLenth) / 2),
                        new Point((int)(textRight + 2 * lineLenth), btnAdd.Height - (btnAdd.Height - lineLenth) / 2),
                        });
                }
                else
                {
                    e.Graphics.DrawLines(new Pen(Color.Black, 1.7f),
                        new Point[] {
                        new Point((int)(textRight), (btnAdd.Height - lineLenth) / 2),
                        new Point((int)(textRight + lineLenth), btnAdd.Height - (btnAdd.Height - lineLenth) / 2),
                        new Point((int)(textRight + 2 * lineLenth), (btnAdd.Height - lineLenth) / 2),
                        });
                }
            }
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

        private void pnlAdd_Leave(object sender, EventArgs e)
        {
            if (!ClientRectangle.Contains(PointToClient(MousePosition)))
            {
                UpOrDown = false;
                timer.Start();
            }
        }
    }
}
