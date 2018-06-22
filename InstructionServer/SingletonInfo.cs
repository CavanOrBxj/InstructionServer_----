using System.Threading;
using System.Collections.Generic;
using System.Data;
using EBSignature;

namespace InstructionServer
{
    public class SingletonInfo
    {
        private static SingletonInfo _singleton;
        public int DeviceHandle_sjj1507;
        public int DeviceHandle_sjj1313;

        public sjj1507 crambler1507;
        public sjj1313 crambler1313;

        public int scramblernum;//记录当前所使用的是哪种CA 江南天安-1 江南科友-2     内置CA-5

        public bool ischecksignature;//记录当前是否需要签名检测

        public int OpenScramblerReturn; //天安密码器打开情况   0表示成功 

        public int InlayCAType;//内置CA的类型  1表示EbMSGCASignature  2表示EbMSGPLSignature

        public bool IsUseCAInfo;//表明是否启用CA  true表示启用  false表示不启用

        public EbmSignature InlayCA;

        public bool IsStartSend;//是否已经启动发送

        public string cramblertype;

        public int OriginalNetworkId;//应急广播原始网络标识符 0-65535

        public bool IsGXProtocol;//表明是否是广西协议

        public bool IsUseAddCert;//是否使用增加的证书
        public string Cert_SN;//增加的证书编号
        public string PriKey;//增加证书的私钥
        public string PubKey;//增加证书的公钥

        public int Cert_Index;//证书索引

     

        private SingletonInfo()                                                                 
        {
            scramblernum = 0;
            DeviceHandle_sjj1507 = 0;
            DeviceHandle_sjj1313 = 0;
            ischecksignature = false;
            OpenScramblerReturn = 2;
            InlayCAType = 0;
            IsUseCAInfo = true; //默认启用CA
            InlayCA=new EbmSignature();
            crambler1507 = new sjj1507();
            crambler1313 = new sjj1313();
            IsStartSend = false;
            cramblertype = "";
            OriginalNetworkId = 0;//是否需要保存？   20180328

            IsGXProtocol = false;

            IsUseAddCert = false;
            Cert_SN = "";
            PriKey = "";
            PubKey = "";
            Cert_Index = 0;

        }

        public static SingletonInfo GetInstance()
        {
            if (_singleton == null)
            {
                Interlocked.CompareExchange(ref _singleton, new SingletonInfo(), null);
            }
            return _singleton;
        }




    }
}