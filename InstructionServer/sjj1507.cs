using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;

namespace InstructionServer
{
    public class sjj1507
    {
        [DllImport(@".\sjj1507\libTassYJGBCmd_SJJ1507.dll", EntryPoint = "OpenDevice", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int OpenDevice(ref System.IntPtr phDeviceHandle);

        [DllImport(@".\sjj1507\libTassYJGBCmd_SJJ1507.dll", EntryPoint = "CloseDevice", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int CloseDevice(ref int phDeviceHandle);

        //导入证书
        [DllImport(@".\sjj1507\libTassYJGBCmd_SJJ1507.dll", EntryPoint = "ImportTrustedCert", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int ImportTrustedCert(ref int phDeviceHandle, StringBuilder strcertPath);

        //使用设备私钥计算数据签名
        [DllImport(@".\sjj1507\libTassYJGBCmd_SJJ1507.dll", EntryPoint = "GenerateSignatureWithDevicePrivateKey", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int GenerateSignatureWithDevicePrivateKey(ref int phDeviceHandle, int nDataType, byte[] inputData, int nDataLength, byte[] pucCounter, byte[] pucSignCerSn, byte[] pucSignature);

        //使用设备私钥计算数据签名(字符串模式)
        [DllImport(@".\sjj1507\libTassYJGBCmd_SJJ1507.dll", EntryPoint = "GenerateSignatureWithDevicePrivateKey_String", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int GenerateSignatureWithDevicePrivateKey_string(ref int phDeviceHandle, int nDataType, string pcData, byte[] pcResult);

        //使用证书验证数据签名
        [DllImport(@".\sjj1507\libTassYJGBCmd_SJJ1507.dll", EntryPoint = "VerifySignatureWithTrustedCert", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern bool VerifySignatureWithTrustedCert(ref int phDeviceHandle, int nDataType, byte[] pucData, int nDataLength, byte[] pucCounter, byte[] pucSignCerSn, byte[] pucSignature);

        //使用证书验证数据签名（字符串）
        [DllImport(@".\sjj1507\libTassYJGBCmd_SJJ1507.dll", EntryPoint = "VerifySignatureWithTrustedCert_String", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int VerifySignatureWithTrustedCert_String(ref int phDeviceHandle, int nDataType, byte[] pucData);

        //计算数据摘要
        [DllImport(@".\sjj1507\libTassYJGBCmd_SJJ1507.dll", EntryPoint = "CalcHash", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int CalcHash(ref int phDeviceHandle, int nHashAlg, byte[] pucData, int nDataLength, byte[] pucHash, ref int pnHashLength);

        //生成签名
        [DllImport(@".\sjj1507\libTassYJGBCmd_SJJ1507.dll", EntryPoint = "Platform_CalculateSignature", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int Platform_CalculateSignature(ref int phDeviceHandle, int nDataType, byte[] pucData, int nDataLength, byte[] pcSignature, ref int pnSignatureLength);


         //网络连接判断
        [DllImport("wininet.dll")]
        public extern static bool InternetGetConnectedState(out int connectionDescription, int reservedValue);


        public int OpenScrambler(ref System.IntPtr phDeviceHandle)
        {
            try
            {

                int I = 0;
                bool state = InternetGetConnectedState(out I, 0);
                int nReturn;
                if (state)
                {
                    nReturn = OpenDevice(ref phDeviceHandle);
                }
                else
                {
                    nReturn = -255;//网卡异常
                }
                return nReturn;
            }
            catch (Exception ex)
            {
                
                throw;
            }
           
        }

        public int ImportTrustedCert(ref int phDeviceHandle)
        {
            StringBuilder strSrc = new StringBuilder("C:\\Windows\\windows_x32\\data");
            int nReturn = ImportTrustedCert(ref phDeviceHandle, strSrc);
            return nReturn;
        }

        public int CloseScrambler(ref int phDeviceHandle)
        {
            int nReturn = CloseDevice(ref phDeviceHandle);
            return nReturn;
        }

        public string GenerateSignatureWithDevicePrivateKey(ref int phDeviceHandle, byte[] strpcData, int size, ref string strSignture, ref string strpucCounter, ref string strpucSignCerSn)
        {
            //签名
            byte[] Signture = new byte[64];
            byte[] pucCounter = new byte[4];
            byte[] pucSignCerSn = new byte[6];

            int nResult = GenerateSignatureWithDevicePrivateKey(ref phDeviceHandle, 1, strpcData, size, pucCounter, pucSignCerSn, Signture);

            //string PassWard = Convert.ToBase64String(Signture  pucCounter pucSignCerSn);
            strSignture = Convert.ToBase64String(Signture);
            strpucCounter = Convert.ToBase64String(pucCounter);
            strpucSignCerSn = Convert.ToBase64String(pucSignCerSn);

            //byte[] a = Signture;
            //byte[] b = pucCounter;
            //byte[] c = pucSignCerSn;
            //List<byte> d = new List<byte>(a);
            //d.AddRange(b);
            //c
            byte[] a = new byte[Signture.Length + pucCounter.Length + pucSignCerSn.Length];
            Array.Copy(pucCounter, a, pucCounter.Length);
            Array.Copy(pucSignCerSn, 0, a, pucCounter.Length, pucSignCerSn.Length);
            Array.Copy(Signture, 0, a, pucCounter.Length + pucSignCerSn.Length, Signture.Length);
            //Array.Copy(strpucSignCerSn,a,)
            string Fp = Convert.ToBase64String(a);
            return Fp;
        }

        public int GenerateSignatureWithDevicePrivateKey_String_old(ref int phDeviceHandle, string strpcData, ref string strSignture)
        {
            //签名
            byte[] Signture = new byte[200];
            int nResult = GenerateSignatureWithDevicePrivateKey_string(ref phDeviceHandle, 1, strpcData, Signture);
            strSignture = Encoding.Default.GetString(Signture);
            return nResult;
        }
        //验签
        public bool VerifySignatureWithTrustedCert_old(ref int phDeviceHandle, byte[] strpcData, int nDataLength, byte[] pucCounter, byte[] pucSignCerSn, byte[] pucSignature)
        {
            bool nResult = VerifySignatureWithTrustedCert(ref phDeviceHandle, 1, strpcData, nDataLength, pucCounter, pucSignCerSn, pucSignature);
            return nResult;
        }

        public string Platform_CalculateSingature_String(int PhDeviceHandle, int DataType, byte[] strpcData, int size, ref byte[] strSingBody)
        {
            string CalculateSignaturestr = string.Empty;
            try
            {

                //需增加内置CA 增加条件判断   20171130




                byte[] pucCounter = new byte[100]; //注意 返回的签名数据是100个字节 定长
                int pucSignCerSn = 100;
                int nResule = Platform_CalculateSignature(ref PhDeviceHandle, 1, strpcData, size, pucCounter, ref pucSignCerSn);
                int DSA = nResule;

                if (nResule == 0) 
                {
                    CalculateSignaturestr = Encoding.Default.GetString(pucCounter);
                    byte[] bytes = Convert.FromBase64String(CalculateSignaturestr);
                    bytes.CopyTo(strSingBody, 0);
                }
                else if (nResule == 16779073)  //签名异常 可能原因 网络堵塞 断网情况下 返回的值为16779073
                { 
                   //触发提示主界面  网络异常
                    CalculateSignaturestr = "NetError";
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            return CalculateSignaturestr;
        }


        public static string DecodeBase64(Encoding encode, string result)
        {
            string decode = "";
            byte[] bytes = Convert.FromBase64String(result);
            try
            {
                decode = encode.GetString(bytes);
            }
            catch
            {
                decode = result;
            }
            return decode;
        }

        public string CalcHash_int(int phDeviceHandle, int nHashAlg, byte[] pucData, int nDataLength, ref string strHsahBody)
        {
            string CalculateSignaturestr = string.Empty;
            try
            {
                byte[] pucHash = new byte[32];//注意  返回的摘要长度是32个字节

                int pnHashLength = 100;
                int nResule = CalcHash(ref phDeviceHandle, 1, pucData, nDataLength, pucHash, ref pnHashLength);
               // string str = Convert.ToBase64String(pucHash);
                CalculateSignaturestr = Encoding.Default.GetString(pucHash);

            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            return CalculateSignaturestr;
        }
    }
}
