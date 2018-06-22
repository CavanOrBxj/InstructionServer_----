using System;
using System.Windows.Forms;

namespace EBMTest
{
    public partial class EBMLogIn : Form
    {
        public EBMLogIn()
        {
            InitializeComponent();
            textPwd.KeyPress += TextPwd_KeyPress;
        }

        private void TextPwd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r' || e.KeyChar == '\n')
            {
                btnOK_Click(this, EventArgs.Empty);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if(textPwd.Text == "tuners2012")
            {
                lblText.Visible = false;
                DialogResult = DialogResult.OK;
            }
            else
            {
                lblText.Visible = true;
            }
        }
    }
}
