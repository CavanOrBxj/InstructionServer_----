using EBMTable;
using EBSignature;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace InstructionServer
{
    public partial class EBMCASet : Form
    {
       // public static IConfig cf = ConfigFile.Instanse;
       public delegate void CASetDelegate();

        public event CASetDelegate CASetEvent;

       // EbmSignature InlayCA ;

        string InlayCAPBK = "200000000000010000000000424605F778F3148CC2A3200D4A67F55675A4795D3358D3F57F884FE484E07EA926C12584BE7A8B3479392E7779402AF017FDED5945F12E37BFE1C97A6EFF64D4712D59480D4CAB2551A45873C007590D05E58DC5783A040EDFCF7F6D5500F8689274EE9BF8E6BFFBFAC0B13A94E89A49433BFA82138D0DE5E03D89C0EEEB5C61F05979";
        string InLayPLPBK = "300000000000010000000000424605F778F3148CC2A3200D4A67F55675A4795D3358D3F57F884FE484E07EA926C12584BE7A8B3479392E7779402AF017FDED5945F12E37BFE1C97A6EFF64D4712D59480D4CAB2551A45873C007590D05E58DC5783A040EDFCF7F6D5500F8689274EE9BF8E6BFFBFAC0B13A94E89A49433BFA82138D0DE5E03D89C0EEEB5C61F05979";
        string TIANANPBK = "100000000000010000000000424605F778F3148CC2A3200D4A67F55675A4795D3358D3F57F884FE484E07EA926C12584BE7A8B3479392E7779402AF017FDED5945F12E37BFE1C97A6EFF64D4712D59480D4CAB2551A45873C007590D05E58DC5783A040EDFCF7F6D5500F8689274EE9BF8E6BFFBFAC0B13A94E89A49433BFA82138D0DE5E03D89C0EEEB5C61F05979";
       // public delegate void CASetDelegate();

      //  public CASetDelegate ff;
        public EBMCASet()
        {
            InitializeComponent();
           // InlayCA = new EbmSignature();
            this.Load += EBMCASet_Load;

      
        }

        void EBMCASet_Load(object sender, EventArgs e)
        {
            Dictionary<object, object> dict = new Dictionary<object, object>();
            dict.Add("江南天安", 1);
            dict.Add("内置CA",5);
            ComboBind.Binding(cmbCAname, dict);


            EBCert tmp =SingletonInfo.GetInstance().InlayCA.GetEBCert(0);//0表示CA_CERT
            InlayCAPBK = tmp.Cert;

            EBCert tmp1 = SingletonInfo.GetInstance().InlayCA.GetEBCert(1);//1表示PL_CERT
            InLayPLPBK = tmp1.Cert;
         



            chbUseSignature.Checked =EBMMain.ini.ReadValue("EBMInfo", "IsUseCA").ToString() == "1" ? true : false;



            cmbCAname.SelectedValue = Convert.ToInt32(EBMMain.ini.ReadValue("EBMInfo", "CAtype").ToString());


            if (Convert.ToInt32(cmbCAname.SelectedValue) == 5) //内置CA
            {
                panelInlayCA.Visible = true;
              
              if (EBMMain.ini.ReadValue("EBMInfo", "InlayCA").ToString() == "1")
                {
                    chbplatformsignature.Checked = true;
                    textPublicKey.Text = InLayPLPBK;
                }
                else
                {
                    chbCAsignature.Checked = true;
                    textPublicKey.Text = InlayCAPBK; 

                }
            }
            else
            {
                panelInlayCA.Visible = false;
            }
           chbCheckSignature.Checked = EBMMain.ini.ReadValue("EBMInfo", "CheckSignature").ToString() == "1" ? true : false;
        }



        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(cmbCAname.SelectedValue) == 5)
                 {

                     if (chbplatformsignature.Checked == false && chbCAsignature.Checked == false)
                     {
                         MessageBox.Show("请勾选平台证书签名/CA签名！");
                         return;
                     }

                     if (chbplatformsignature.Checked)
                     {
                         SingletonInfo.GetInstance().InlayCAType = 2;
                     }
                     else
                     {
                         SingletonInfo.GetInstance().InlayCAType = 1;
                     }
                 }

                if (chbCheckSignature.Checked)
                {
                    SingletonInfo.GetInstance().ischecksignature = true;
                }
                else
                {
                    SingletonInfo.GetInstance().ischecksignature = false;
                }         

                if (SingletonInfo.GetInstance().OpenScramblerReturn != 0)
                {
                    SingletonInfo.GetInstance().scramblernum = Convert.ToInt32(cmbCAname.SelectedValue);

                    if (CASetEvent != null)
                        CASetEvent();//引发事件
                }

                SingletonInfo.GetInstance().IsUseCAInfo = chbUseSignature.Checked; //是否启用签名

                EBMMain.ini.WriteValue("EBMInfo", "IsCAInfoSet", "1");
                EBMMain.ini.WriteValue("EBMInfo", "IsUseCA", chbUseSignature.Checked ? "1" : "0");
                if (!chbUseSignature.Checked)
                {
                   //初始化  
                }

                EBMMain.ini.WriteValue("EBMInfo", "CAtype", Convert.ToInt32(cmbCAname.SelectedValue).ToString());
                if (Convert.ToInt32(cmbCAname.SelectedValue)==5)
                     {
                         if (chbplatformsignature.Checked)//平台签名
                         {
                             EBMMain.ini.WriteValue("EBMInfo", "InlayCA", "0");
                         }
                         else//CA签名
                         {
                             EBMMain.ini.WriteValue("EBMInfo", "InlayCA", "1");
                         }
                     }

                EBMMain.ini.WriteValue("EBMInfo", "CheckSignature", chbCheckSignature.Checked ? "1" : "0");
                EBMMain.ini.WriteValue("EBMInfo", "IsCASet", "1");
                Close();
            }
            catch (Exception ex)
            {
                
                throw;
            }
        }

        private void EBMStreamSet_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Close();
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        private void cmbCAname_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(cmbCAname.SelectedValue) == 1) //江南天安
                {
                    textPublicKey.Text = TIANANPBK;
                    panelInlayCA.Visible = false;
                }
                else   //内置CA
                {
                    panelInlayCA.Visible = true;
                    textPublicKey.Text = "";
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }


        private void chbplatformsignature_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chbplatformsignature.Checked == true)
                {

                    textPublicKey.Text =InLayPLPBK ;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void chbCAsignature_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chbCAsignature.Checked == true)
                {

                    textPublicKey.Text = InlayCAPBK;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void chbUseSignature_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                //(MdiParent as EBMMain).EbMStream.EB_CertAuth_Table = GetCertAuthTable(ref (MdiParent as EBMMain).EbMStream.EB_CertAuth_Table) ? (MdiParent as EBMMain).EbMStream.EB_CertAuth_Table : null;

                (MdiParent as EBMMain).EbMStream.Initialization();
            }
            catch (Exception)
            {
                
                throw;
            }
        }

    }
}
