using EBSignature;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace InstructionServer
{
   public  class Calcle
    {
         public delegate void MyDelegate();
         public event MyDelegate MyEvent;

       /// <summary>
       /// 签名函数
       /// </summary>
       /// <param name="pdatabuf"></param>
       /// <param name="datalen"></param>
       /// <param name="random"></param>
       /// <param name="signature"></param>
         public void SignatureFunc(byte[] pdatabuf, int datalen, ref int random, ref byte[] signature)
         {

             try
             {
                 byte[] strSignture = new byte[100];
                 byte[] pucSignature = pdatabuf;
                 //   int nDeviceHandle = 0;


                 if (SingletonInfo.GetInstance().IsUseAddCert)
                 {
                     SingletonInfo.GetInstance().InlayCA.EbMsgSign(pdatabuf, datalen, ref random, ref signature, SingletonInfo.GetInstance().Cert_Index);

                 }
                 else
                 {      //目前暂用平台签名  20180524
                     SingletonInfo.GetInstance().InlayCA.EbMsgSign(pdatabuf, datalen, ref random, ref signature, 2);
                 }
                
                 
                 //switch (SingletonInfo.GetInstance().cramblertype)
                 //{
                 //    case "sjj1313":

                 //         nDeviceHandle = SingletonInfo.GetInstance().DeviceHandle_sjj1313;
                 //        string eturn = SingletonInfo.GetInstance().crambler1313.Platform_CalculateSingature_String(nDeviceHandle, 1, pucSignature, pucSignature.Length, ref strSignture);
                 //        strSignture.CopyTo(signature, 0);
                 //        if (eturn == "NetError")
                 //        {
                 //            LogHelper.WriteLog(typeof(Calcle), "天安加密器网络错误");
                 //        }
                 //        break;

                 //    case "sjj1507":
                 //         nDeviceHandle = SingletonInfo.GetInstance().DeviceHandle_sjj1507;
                 //        string eturn1 = SingletonInfo.GetInstance().crambler1507.Platform_CalculateSingature_String(nDeviceHandle, 1, pucSignature, pucSignature.Length, ref strSignture);
                 //        strSignture.CopyTo(signature, 0);
                 //        if (eturn1 == "NetError")
                 //        {
                 //            LogHelper.WriteLog(typeof(Calcle), "天安加密器网络错误");
                 //        }
                 //        break;
                 //}

                 string strData = null;
                 for (int i = 0; i < pucSignature.Length; i++)
                 {
                     strData += " " + pucSignature[i].ToString("X2");
                 }
                 // LogRecord.WriteLogFile("原文：" + strData);
                 string strData2 = null;
                 for (int i = 0; i < signature.Length; i++)
                 {
                     strData2 += " " + signature[i].ToString("X2");
                 }
                 //  LogRecord.WriteLogFile("签名数据：" + strData2);


              
             }
             catch (Exception ex)
             {
                 
                 throw;
             }
             

         }
    }
}
