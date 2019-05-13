using Apache.NMS;
using Apache.NMS.ActiveMQ.Commands;
using ControlAstro.Utils;
using EBMTable;
using EBSignature;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace InstructionServer
{
    public partial class EBMMain : Form
    {
        private bool isAdminAccount = false;
        private Object Gtoken = null; //用于锁住
        public static MQ m_mq;
        private IoServer iocp = new IoServer(10, 2048);
        public bool IsStartStream { get; set; }
        bool isInitStream = false;
        EBMStream EbmStream;
        EBIndexTable EB_Index_Table = new EBIndexTable();
        DailyBroadcastTable Daily_Broadcast_Table = new DailyBroadcastTable();
        EBConfigureTable EB_Configure_Table=new EBConfigureTable();
        EBContentTable EB_Content_Table = new EBContentTable();
        EBCertAuthTable EB_CertAuth_Table = new EBCertAuthTable();
        List<EBContentTable> list_EB_Content_Table = new List<EBContentTable>();
        Thread thread;    

        public EBMStream EbMStream
        {
            get { return EbmStream; }
            set { EbmStream = value; }
        }

        public static Calcle calcel;
        private IMessageConsumer m_consumer; //消费者
        private bool isConn = false; //是否已与MQ服务器正常连接
        public static IniFiles ini;
        private DataDealHelper dataHelper;
        public DataHelper dataHelperreal;       
        public delegate void LogAppendDelegate(Color color, string text);
        public System.Timers.Timer timer;
        private int TcpReceivePort = 0;

        public EBMMain()
        {
            try
            {
                InitializeComponent();
                Load += EBMMain_Load; 
            }
            catch (Exception ex)
            {
                throw;
            }
            
        }

        private void InitConfig()
        {
            try
            {
                SingletonInfo.GetInstance().SignatureType= ini.ReadValue("Scrambler", "SignatureType") == "1" ? true : false;//
                SingletonInfo.GetInstance().cramblertype = ini.ReadValue("Scrambler", "ScramblerType");
                SingletonInfo.GetInstance().IsGXProtocol = ini.ReadValue("ProtocolType", "ProtocolType") == "1" ? true : false;//-

                #region AddCertInfo
                SingletonInfo.GetInstance().IsUseAddCert = ini.ReadValue("AddCertInfo", "IsUseAddCert") == "1" ? true : false;//“1”表示使用增加的证书 2表示不使用增加证书信息
                SingletonInfo.GetInstance().Cert_SN = ini.ReadValue("AddCertInfo", "Cert_SN");
                SingletonInfo.GetInstance().PriKey = ini.ReadValue("AddCertInfo", "PriKey");
                SingletonInfo.GetInstance().PubKey = ini.ReadValue("AddCertInfo", "PubKey");

                EBCert tmp = new EBCert();
                tmp.Cert_sn = SingletonInfo.GetInstance().Cert_SN;
                tmp.PriKey = SingletonInfo.GetInstance().PriKey;
                tmp.PubKey = SingletonInfo.GetInstance().PubKey;
                SingletonInfo.GetInstance().Cert_Index = SingletonInfo.GetInstance().InlayCA.AddEBCert(tmp);
                #endregion

                TcpReceivePort = Convert.ToInt32(ini.ReadValue("TCP", "ReceivePort"));
                SingletonInfo.GetInstance().LocalHost = ini.ReadValue("LocalHost", "IP");

                SingletonInfo.GetInstance().ebm_id_front = ini.ReadValue("EBM", "ebm_id_front");
                SingletonInfo.GetInstance().ebm_id_behind = ini.ReadValue("EBM", "ebm_id_behind");
                SingletonInfo.GetInstance().ebm_id_count = Convert.ToInt32(ini.ReadValue("EBM", "ebm_id_count"));

                SingletonInfo.GetInstance().TimerInterval= Convert.ToInt32(ini.ReadValue("Timer", "Interval"));

                SingletonInfo.GetInstance().IsUseCAInfo = ini.ReadValue("EBMInfo", "IsUseCA") == "1" ? true : false;//修改于20181226
               // SingletonInfo.GetInstance().InlayCA.SignCounter= Convert.ToInt32(ini.ReadValue("EBMInfo", "SignCounter"));//修改于20181226
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        private void InitUsbPwsSupport()
        {
            if (!SingletonInfo.GetInstance().SignatureType)
            {
                try
                {
                    int nReturn = SingletonInfo.GetInstance().usb.USB_OpenDevice(ref SingletonInfo.GetInstance().phDeviceHandle);
                    if (nReturn != 0)
                    {
                        MessageBox.Show("密码器打开失败！");
                        MessageBox.Show(nReturn.ToString());
                    }
                }
                catch (Exception em)
                {
                    MessageBox.Show("密码器打开失败：" + em.Message);
                }
            }

        }

        /// <summary> 
        /// 追加显示文本 
        /// </summary> 
        /// <param name="color">文本颜色</param> 
        /// <param name="text">显示文本</param> 
        public void LogAppend(Color color, string text)
        {
            try
            {
                richTextData.AppendText("\n");
                richTextData.SelectionColor = color;
                richTextData.AppendText(text);
            }
            catch (Exception ex)
            {

                LogHelper.WriteLog(typeof(EBMMain), ex.ToString());
            }
       
        }

        public void LogMessage(Color co,string text)
        {
            try
            {
                LogAppendDelegate la = new LogAppendDelegate(LogAppend);
                richTextData.Invoke(la, co, text);
            }
            catch (Exception ex)
            {

                LogHelper.WriteLog(typeof(EBMMain), ex.ToString());
            }
        } 


        /// <summary>
        /// 初始化MQ信息
        /// </summary>
        private void ConnectMQServer()
        {
            try
            {
                m_mq = new MQ();
                m_mq.uri = "failover:tcp://" + ini.ReadValue("MQ", "MQIP") + ":" + ini.ReadValue("MQ", "MQPORT");
                m_mq.username = "admin";
                m_mq.password = "admin";
                m_mq.Start();
                isConn = true;
                m_consumer = m_mq.CreateConsumer(true, ini.ReadValue("MQ", "TopicName"));
                m_consumer.Listener += new MessageListener(consumer_listener); 
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(typeof(EBMMain), ex.ToString());
            }
        
        }


        /// <summary>
        /// 启动TCP服务
        /// </summary>
        private void InitTCPServer()
        {
            iocp.Start(TcpReceivePort);
            iocp.mainForm = this;
        }

        private void consumer_listener(IMessage message)
        {
            try
            {
                LogHelper.WriteLog(typeof(EBMMain), "收到MQ指令！");
                dataHelper.Serialize(message.Properties);
            }
            catch (Exception ex)
            {
               // this.m_consumer.Close();    测试注释  20180805
            }
        }

        private void InitTimer()
        {
            //设置定时间隔(毫秒为单位)
            int interval = SingletonInfo.GetInstance().TimerInterval*1000*60;
            timer = new System.Timers.Timer(interval);
            //设置执行一次（false）还是一直执行(true)
            timer.AutoReset = true;
            //设置是否执行System.Timers.Timer.Elapsed事件
            timer.Enabled = true;
            //绑定Elapsed事件
            timer.Elapsed += new System.Timers.ElapsedEventHandler(TimerUp);
        }

         /// <summary>
         /// Timer类执行定时到点事件
         /// </summary>
         /// <param name="sender"></param>
         /// <param name="e"></param>
        private void TimerUp(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                SingleTimeServerSend(DateTime.Now.AddSeconds(10));  
            }
            catch (Exception ex)
            {

            }
        }

        private void SingleTimeServerSend(DateTime time)
        {
            List<TimeService_> listTS = new List<TimeService_>();
            TimeService_ select = new TimeService_();

            System.Guid guid = new Guid();
            guid = Guid.NewGuid();
            select.ItemID = guid.ToString();
            select.Configure = new EBConfigureTimeService();

            select.Configure.Real_time = time;

            select.GetSystemTime = true;
            select.SendTick = 60;
            listTS.Add(select);
            DealTimeService(listTS);
        
        }

        void EBMMain_Load(object sender, EventArgs e)
        {
            CheckIniConfig();
            InitConfig();
            InitUsbPwsSupport();
            IsStartStream = false;
            EbmStream = new EBMStream();
           
            InitTable();
         //   InitEBStream();
            calcel = new Calcle();
            calcel.MyEvent += new Calcle.MyDelegate(NetErrorDeal);
           // InitStreamTableNew();//1    测试注释  20190411
            InitTCPServer();
            Gtoken = new object();
            ConnectMQServer();
            dataHelper = new DataDealHelper();
            dataHelperreal = new DataHelper();
      
            DataDealHelper.MyEvent += new DataDealHelper.MyDelegate(GlobalDataDeal);
            ProcessBegin();
            InitTimer();   // 测试注释  避免调试干扰  20180806
            this.Text = "TS指令服务_V" + Application.ProductVersion;
        }

        private void GlobalDataDeal(object obj)
        {
            try
            {
                OperatorData op = (OperatorData)obj;
                switch (op.OperatorType)
                {
                    case "AddCertAuthData":
                        List<CertAuthTmp> AddCertAuthList = (List<CertAuthTmp>)op.Data;
                        DealCertAuth2Global(AddCertAuthList);
                        break;
                    case "ModifyCertAuthData":
                        List<CertAuthTmp> ModifyCertAuthList = (List<CertAuthTmp>)op.Data;
                        DealCertAuth2Global(ModifyCertAuthList);
                        break;
                    case "DelCertAuthData":
                        DelCertAuth2Global((string)op.Data);
                        break;
                    case "AddCertData":
                        List<CertTmp> AddCertList = (List<CertTmp>)op.Data;
                        DealCert2Global(AddCertList);
                        break;
                    case "ModifyCertData":
                        List<CertTmp> ModifyCertList = (List<CertTmp>)op.Data;
                        DealCert2Global(ModifyCertList);
                        break;
                    case "DelCertData":
                        DelCert2Global((string)op.Data);
                        break;
                    case "SetCertRepeatTime":
                        break;
                    case "AddEBMIndex":
                        DealEBMIndex2Global((EBMIndexTmp)op.Data);
                        break;
                        
                    case "AddAreaEBMIndex":
                        AddAreaEBMIndex2Global((ModifyEBMIndex)op.Data);
                        break;
                    case "DelAreaEBMIndex":
                        DelAreaEBMIndex2Global((ModifyEBMIndex)op.Data);
                        break;
                    case "DelEBMIndex":
                        DelEBMIndex2Global((string)op.Data);
                        break;
                        
                    case "AddEBMConfigure":
                    case "ModifyEBMConfigure":
                    case "DelEBMConfigure":
                        DealEBMConfigure2Global(op);
                        break;

                    case "AddDailyBroadcast":
                    case "ModifyDailyBroadcast":
                    case "DelDailyBroadcast":
                        DealDailyBroadcast2Global(op);
                        break;

                    case "AddEBContent":
                    case "ModifyEBContent":
                    case "DelEBContent":
                        DealEBContent2Global(op);
                        break;

                    case "ChangeInutChannel":
                        DealChangeInputChannel(op);
                        break;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(typeof(EBMMain), ex.ToString());
            }
        }

        #region  EBMIndex 数据处理
        private void DelEBMIndex2Global(string IndexItemIDstr)
        {
            lock (Gtoken)
            {
                if (SingletonInfo.GetInstance()._EBMIndexGlobal.ListEbIndex != null)
                {
                    string[] IndexItemIDArray = IndexItemIDstr.Split(',');
                    foreach (string  item in IndexItemIDArray)
                    {
                        List<EBMIndex_> tmp = SingletonInfo.GetInstance()._EBMIndexGlobal.ListEbIndex.FindAll(s => s.IndexItemID.StartsWith(item));
                        if (tmp!=null)
                        {
                            foreach (var ite in tmp)
                            {
                                SingletonInfo.GetInstance()._EBMIndexGlobal.ListEbIndex.Remove(ite);
                            }
                          
                        }
                    }
                }
                EbmStream.EB_Index_Table = GetEBIndexTable(ref EB_Index_Table) ? EB_Index_Table : null;
                EbMStream.Initialization();
            }
           // UpdateDataTextNew((object)1);
         //   GC.Collect();
        }

        private void AddAreaEBMIndex2Global(ModifyEBMIndex modify)
        {
            lock (Gtoken)
            {
                if (SingletonInfo.GetInstance()._EBMIndexGlobal.ListEbIndex != null)
                {
                    //去同向
                    EBMIndex_ tmp = SingletonInfo.GetInstance()._EBMIndexGlobal.ListEbIndex.Find(s => s.IndexItemID.Equals(modify.IndexItemID));
                    if (tmp != null)
                    {
                        string[] resource_code = modify.Data.Split(',');
                        foreach (string item in resource_code)
                        {
                          //  tmp.List_EBM_resource_code.Add(item);  注释于20190225
                            tmp.List_EBM_resource_code.Remove(item);
                        }
                    }
                }
                EbmStream.StopStreaming();
                EbmStream.StartStreaming();

               
                EbmStream.EB_Index_Table = GetEBIndexTable(ref EB_Index_Table) ? EB_Index_Table : null;
                EbmStream.SignatureCallbackRef = new EBMStream.SignatureCallBackDelegateRef(calcel.SignatureFunc);
                EbMStream.Initialization();
            }
         //   UpdateDataTextNew((object)1);
        }

        private void DelAreaEBMIndex2Global(ModifyEBMIndex modify)
        {
            lock (Gtoken)
            {
                if (SingletonInfo.GetInstance()._EBMIndexGlobal.ListEbIndex != null)
                {
                    //去同向
                    EBMIndex_ tmp = SingletonInfo.GetInstance()._EBMIndexGlobal.ListEbIndex.Find(s => s.IndexItemID.Equals(modify.IndexItemID));
                    if (tmp != null)
                    {
                        string[] resource_code = modify.Data.Split(',');
                        foreach (string item in resource_code)
                        {
                            tmp.List_EBM_resource_code.Remove(item); 
                        }
                    }
                }
                EbmStream.EB_Index_Table = GetEBIndexTable(ref EB_Index_Table) ? EB_Index_Table : null;
                EbmStream.SignatureCallbackRef = new EBMStream.SignatureCallBackDelegateRef(calcel.SignatureFunc);
                EbMStream.Initialization();
            }
           // UpdateDataTextNew((object)1);
        }

        private void DealEBMIndex2Global(EBMIndexTmp EBMIndex)
        {

            try
            {
                lock (Gtoken)
                {
                    if (SingletonInfo.GetInstance()._EBMIndexGlobal.ListEbIndex != null)
                    {
                        //去同向
                        EBMIndex_ tmp = SingletonInfo.GetInstance()._EBMIndexGlobal.ListEbIndex.Find(s => s.IndexItemID.Equals(EBMIndex.IndexItemID));
                        if (tmp != null)
                        {
                            SingletonInfo.GetInstance()._EBMIndexGlobal.ListEbIndex.Remove(tmp);
                        }
                    }
                    //增加新项
                    EBMIndex_ index = new EBMIndex_();

                    index.SendState = true;
                    index.EBIndex = new EBIndex();
                    index.EBIndex.ProtocolGX = SingletonInfo.GetInstance().IsGXProtocol;
                    index.S_EBM_id = EBMIndex.S_EBM_id;
                    index.S_EBM_original_network_id = EBMIndex.S_EBM_original_network_id;
                    index.S_EBM_start_time = EBMIndex.S_EBM_start_time;
                    index.S_EBM_end_time = EBMIndex.S_EBM_end_time;
                    index.S_EBM_type = EBMIndex.S_EBM_type;
                    index.S_EBM_class = EBMIndex.S_EBM_class;
                    index.S_EBM_level = EBMIndex.S_EBM_level;
                    index.IndexItemID = EBMIndex.IndexItemID;
                    index.List_EBM_resource_code = new List<string>();

                    ///注：通讯库不支持 List的Add模式 
                    string[] List_EBM_resource_codeArray = EBMIndex.List_EBM_resource_code.Split(',');

                    for (int i = 0; i < List_EBM_resource_codeArray.Length; i++)
                    {
                        int resource_code_length = List_EBM_resource_codeArray[i].Length;


                        //20180525 陈良要求修改特殊处理
                        switch (resource_code_length)
                        {
                            case 18:
                                break;
                            case 23:
                                string tt = List_EBM_resource_codeArray[i].Substring(1);
                                string tt1 = tt.Substring(0, tt.Length - 4);
                                List_EBM_resource_codeArray[i] = tt1;
                                break;
                            case 12:
                                if (SingletonInfo.GetInstance().IsGXProtocol)
                                {
                                    List_EBM_resource_codeArray[i] = "0612" + List_EBM_resource_codeArray[i] + "00";
                                }
                                else
                                {
                                    List_EBM_resource_codeArray[i] = "6" + List_EBM_resource_codeArray[i] + "0314000000";
                                }

                                break;
                        }
                    }
                    

                    index.List_EBM_resource_code = new List<string>(List_EBM_resource_codeArray);
                    //foreach (string item in EBMIndex.List_EBM_resource_code.Split(','))
                    //{
                    //    index.List_EBM_resource_code.Add(item);
                    //}

                    index.BL_details_channel_indicate = EBMIndex.BL_details_channel_indicate == "true" ? true : false;
                    index.DesFlag = EBMIndex.DesFlag == "true" ? true : false;
                    index.S_details_channel_transport_stream_id = EBMIndex.S_details_channel_transport_stream_id;
                    index.S_details_channel_program_number = EBMIndex.S_details_channel_program_number;
                    index.S_details_channel_PCR_PID = EBMIndex.S_details_channel_PCR_PID;

                    //if (index.DesFlag)
                    //    index.DeliverySystemDescriptor = GetDataDSD(EBMIndex.DeliverySystemDescriptor, EBMIndex.descriptor_tag);
                    if (index.BL_details_channel_indicate)
                    {

                        List<ProgramStreamInfotmp> List_ProgramStreamInfotmp = new List<ProgramStreamInfotmp>();//S_elementary_PID 中有“，”时，临时加入项
                        int List_ProgramStreamInfoLength = EBMIndex.List_ProgramStreamInfo.Count;//详情频道节目流信息列表长度
                        for (int i = 0; i < List_ProgramStreamInfoLength; i++)
                        {
                            string S_elementary_PID = EBMIndex.List_ProgramStreamInfo[i].S_elementary_PID;
                            //国标协议
                            if (S_elementary_PID.Contains(","))
                            {
                                string[] pidarray = S_elementary_PID.Split(',');
                                EBMIndex.List_ProgramStreamInfo[i].S_elementary_PID = pidarray[0];
                                EBMIndex.List_ProgramStreamInfo[i].B_stream_type = "3";
                                ProgramStreamInfotmp add = new ProgramStreamInfotmp();
                                add.B_stream_type = "1";
                                add.Descriptor2 = null;
                                //add.Descriptor2
                                add.S_elementary_PID = pidarray[1];
                                List_ProgramStreamInfotmp.Add(add);
                            }
                        }

                        if (List_ProgramStreamInfotmp.Count > 0)
                        {
                            foreach (var item in List_ProgramStreamInfotmp)
                            {
                                EBMIndex.List_ProgramStreamInfo.Add(item);
                            }
                        }
                        index.List_ProgramStreamInfo = GetDataPSI(EBMIndex.List_ProgramStreamInfo);
                    }

                    index.NickName = "";

                    if (SingletonInfo.GetInstance()._EBMIndexGlobal.ListEbIndex == null)
                    {
                        SingletonInfo.GetInstance()._EBMIndexGlobal.ListEbIndex = new List<EBMIndex_>();
                    }
                    SingletonInfo.GetInstance()._EBMIndexGlobal.ListEbIndex.Add(index);

                    #region  测试添加 20190411
                    EbmStream.StopStreaming();
                    Thread.Sleep(500);
                    EbmStream.StartStreaming();
                    #endregion

                    EbmStream.EB_Index_Table = GetEBIndexTable(ref EB_Index_Table) ? EB_Index_Table : null;
                    EbMStream.Initialization();
                }
              // UpdateDataTextNew((object)1);
              //  GC.Collect();
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(typeof(EBMMain), ex.ToString()); 
            }
        }

        public List<ProgramStreamInfo> GetDataPSI(List<ProgramStreamInfotmp> input)
        {
            List<ProgramStreamInfo> list = new List<ProgramStreamInfo>();

            foreach (ProgramStreamInfotmp item in input)
            {
                ProgramStreamInfo tmp = new ProgramStreamInfo();
                tmp.B_stream_type = (byte)Convert.ToInt32(item.B_stream_type);
                tmp.S_elementary_PID = item.S_elementary_PID;
                tmp.Descriptor2 = new StdDescriptor();

                //添加于20180531
                tmp.Descriptor2 = null; 




               // Descriptor2 descriptor2 = new Descriptor2();
                //if (item.Descriptor2 == null)
                //{
                //    descriptor2.B_descriptor_tag = (byte)1;
                //    descriptor2.B_descriptor = new byte[] { 0 };
                //}
                //else
                //{
                //      dynamic a = item.Descriptor2;
                //      descriptor2.B_descriptor_tag = ((byte)Convert.ToInt32(a[0]["B_descriptor_tag"]));
                //      descriptor2.B_descriptor = new byte[] { ((byte)Convert.ToInt32(a[0]["B_descriptor"])) };
                //}
                //if (descriptor2!=null)
                //{
                //    if (descriptor2.B_descriptor_tag == null && descriptor2.B_descriptor == null)
                //    {
                //        descriptor2 = null;
                //    }
                //    else
                //    {
                //        tmp.Descriptor2.B_descriptor_tag = (byte)Convert.ToInt32(descriptor2.B_descriptor_tag);

                //        //string[] descriptors = descriptor2.B_descriptor.Split(' ');
                //        //List<byte> array = new List<byte>();
                //        //foreach (string ite in descriptors)
                //        //{
                //        //    array.Add((byte)Convert.ToInt32(ite, 16));
                //        //}
                //        //tmp.Descriptor2.Br_descriptor = array.ToArray();

                //        tmp.Descriptor2.Br_descriptor = descriptor2.B_descriptor;
                //    }
                
                
                //}



                
                list.Add(tmp);
            }

            return list;
        }

        private object GetDataDSD(object input, int type)
        {
            switch (type)
            {
                case 68://有线传送系统描述符
                    Cable_delivery_system_descriptor cdsd = new Cable_delivery_system_descriptor();
                    CableDeliverySystemDescriptortmp tmp = (CableDeliverySystemDescriptortmp)input;
                    cdsd.B_FEC_inner = (byte)Convert.ToInt32(tmp.B_FEC_inner);
                    cdsd.B_FEC_outer = (byte)Convert.ToInt32(tmp.B_FEC_outer);
                    cdsd.B_Modulation = (byte)Convert.ToInt32(tmp.B_Modulation);
                    cdsd.D_frequency = Convert.ToDouble(tmp.D_frequency);
                    cdsd.D_Symbol_rate = Convert.ToDouble(tmp.D_Symbol_rate);
                    return cdsd;
                case 90://地面传送系统描述符
                    Terristrial_delivery_system_descriptor tdsd = new Terristrial_delivery_system_descriptor();
                    TerristrialDeliverySystemDescriptortmp tmp1 = (TerristrialDeliverySystemDescriptortmp)input;
                    tdsd.B_FEC = (byte)Convert.ToInt32(tmp1.B_FEC);
                    tdsd.B_Frame_header_mode = (byte)Convert.ToInt32(tmp1.B_Frame_header_mode);
                    tdsd.B_Interleaveing_mode = (byte)Convert.ToInt32(tmp1.B_Interleaveing_mode);
                    tdsd.B_Modulation = (byte)Convert.ToInt32(tmp1.B_Modulation);
                    tdsd.B_Number_of_subcarrier = (byte)Convert.ToInt32(tmp1.B_Number_of_subcarrier); ;
                    tdsd.D_Centre_frequency = Convert.ToDouble(tmp1.D_Centre_frequency);
                    tdsd.L_Other_frequency_flag = tmp1.L_Other_frequency_flag == "true" ? true : false;
                    tdsd.L_Sfn_mfn_flag = tmp1.L_Sfn_mfn_flag == "true" ? true : false;
                    return tdsd;
            }
            return null;
        }
        #endregion 

        #region EBMConfigure 数据处理
        private void DealEBMConfigure2Global(OperatorData op)
        {
            switch (op.OperatorType)
            {
                case "AddEBMConfigure":

                case "ModifyEBMConfigure":
                    switch (op.ModuleType)
                    {
                        case "1":
                            List<TimeService_> listTS = (List<TimeService_>)op.Data;
                            DealTimeService(listTS);

                            break;
                        case "2":
                            List<SetAddress_> listSA = (List<SetAddress_>)op.Data;
                            DealSetAddress(listSA);
                            break;
                        case "3":
                            List<WorkMode_> listWM = (List<WorkMode_>)op.Data;
                            DealWorkMode(listWM);
                            break;
                        case "4":
                            List<MainFrequency_> listMF = (List<MainFrequency_>)op.Data;
                            DealMainFrequency(listMF);
                            break;
                        case "5":
                            if (SingletonInfo.GetInstance().IsGXProtocol)
                            {
                                List<Reback_> listRB = (List<Reback_>)op.Data;
                                DealReback(listRB);
                            }
                            else
                            {
                                List<Reback_Nation> listRB = (List<Reback_Nation>)op.Data;
                                DealReback_Nation(listRB);
                            }
                           
                            break;
                        case "6":
                            List<DefaltVolume_> listDV = (List<DefaltVolume_>)op.Data;
                            DealDefaltVolume(listDV);
                            break;
                        case "7":
                            List<RebackPeriod_> listRP = (List<RebackPeriod_>)op.Data;
                            DealRebackPeriod(listRP);
                            break;
                        case "104":
                            List<ContentMoniterRetback_> listCMR = (List<ContentMoniterRetback_>)op.Data;
                            DealContentMoniterRetback(listCMR);
                            break;
                        case "105":
                            //if (SingletonInfo.GetInstance().IsGXProtocol)
                            //{
                            //    List<ContentRealMoniterGX_> listCRMGX = (List<ContentRealMoniterGX_>)op.Data;
                            //    DealContentRealMoniterGX(listCRMGX);
                            //}
                            //else
                            //{
                            //    List<ContentRealMoniter_> listCRM = (List<ContentRealMoniter_>)op.Data;
                            //    DealContentRealMoniter(listCRM);
                            //}

                            //国标版本也用广西的协议

                              List<ContentRealMoniterGX_> listCRMGX = (List<ContentRealMoniterGX_>)op.Data;
                              DealContentRealMoniterGX(listCRMGX);
                            break;
                        case "106":
                            //此为广西回传参数查询  实际没用
                            List<StatusRetback_> listSR = (List<StatusRetback_>)op.Data;
                            DealStatusRetback(listSR);
                            break;
                        case "240":
                            List<SoftwareUpGrade_> listSUG = (List<SoftwareUpGrade_>)op.Data;
                            DealSoftwareUpGrade(listSUG);
                            break;
                        case "300":
                            List<StatusRetback_> listSRB = (List<StatusRetback_>)op.Data;
                            DealStatusRetback(listSRB);
                            break;
                        case "8":
                            List<RdsConfig_> listRC = (List<RdsConfig_>)op.Data;
                            DealRdsConfig(listRC);
                            break;
                    }
                    break;
                case "DelEBMConfigure":
                    DelEBMConfigure2Global(op);
                    break;
            }

        }

        private void DealTimeService(List<TimeService_> listTS)
        {
            lock (Gtoken)
            {
                if (SingletonInfo.GetInstance()._EBMConfigureGlobal.ListTimeService != null)
                {
                    //去同项
                    foreach (TimeService_ item in listTS)
                    {
                        TimeService_ tmp = SingletonInfo.GetInstance()._EBMConfigureGlobal.ListTimeService.Find(s => s.ItemID.Equals(item.ItemID));
                        if (tmp != null)
                        {
                            SingletonInfo.GetInstance()._EBMConfigureGlobal.ListTimeService.Remove(tmp);
                        }
                    }
                }
                else
                {
                    SingletonInfo.GetInstance()._EBMConfigureGlobal.ListTimeService = new List<TimeService_>();
                }

                //增新项
                foreach (TimeService_ item in listTS)
                {
                    SingletonInfo.GetInstance()._EBMConfigureGlobal.ListTimeService.Add(item);
                }
             
                EbmStream.EB_Configure_Table = GetConfigureTable(ref EB_Configure_Table, false) ? EB_Configure_Table : null;


                EbMStream.Initialization();
            }
            UpdateDataTextNew((object)3);
            #region 删除记录 
            foreach (TimeService_ item in listTS)
            {
                SingletonInfo.GetInstance()._EBMConfigureGlobal.ListTimeService.Remove(item);
            }
            #endregion
        }

        private void DealSetAddress(List<SetAddress_> listSA)
        {
            lock (Gtoken)
            {
                if (SingletonInfo.GetInstance()._EBMConfigureGlobal.ListSetAddress != null)
                {
                    //去同项
                    foreach (SetAddress_ item in listSA)
                    {
                        SetAddress_ tmp = SingletonInfo.GetInstance()._EBMConfigureGlobal.ListSetAddress.Find(s => s.ItemID.Equals(item.ItemID));
                        if (tmp != null)
                        {
                            SingletonInfo.GetInstance()._EBMConfigureGlobal.ListSetAddress.Remove(tmp);
                        }
                    }
                }
                else
                {
                    SingletonInfo.GetInstance()._EBMConfigureGlobal.ListSetAddress = new List<SetAddress_>();
                }
                //增新项
                foreach (SetAddress_ item in listSA)
                {
                    SingletonInfo.GetInstance()._EBMConfigureGlobal.ListSetAddress.Add(item);
                }

                EbmStream.EB_Configure_Table = GetConfigureTable(ref EB_Configure_Table, false) ? EB_Configure_Table : null;
                EbMStream.Initialization();
            }
            UpdateDataTextNew((object)3);

            #region  删除记录

            foreach (SetAddress_ item in listSA)
            {
                SingletonInfo.GetInstance()._EBMConfigureGlobal.ListSetAddress.Remove(item);
            }
            #endregion
        }

        private void DealWorkMode(List<WorkMode_> listWM)
        {
            lock (Gtoken)
            {
                if (SingletonInfo.GetInstance()._EBMConfigureGlobal.ListWorkMode != null)
                {
                    //去同项
                    foreach (WorkMode_ item in listWM)
                    {
                        WorkMode_ tmp = SingletonInfo.GetInstance()._EBMConfigureGlobal.ListWorkMode.Find(s => s.ItemID.Equals(item.ItemID));
                        if (tmp != null)
                        {
                            SingletonInfo.GetInstance()._EBMConfigureGlobal.ListWorkMode.Remove(tmp);
                        }
                    }
                }
                else
                {
                    SingletonInfo.GetInstance()._EBMConfigureGlobal.ListWorkMode = new List<WorkMode_>();
                }

                //增新项
                foreach (WorkMode_ item in listWM)
                {
                    SingletonInfo.GetInstance()._EBMConfigureGlobal.ListWorkMode.Add(item);
                }

                EbmStream.EB_Configure_Table = GetConfigureTable(ref EB_Configure_Table, false) ? EB_Configure_Table : null;
                EbMStream.Initialization();
            }
            UpdateDataTextNew((object)3);

            #region 删除记录
            foreach (WorkMode_ item in listWM)
            {
                SingletonInfo.GetInstance()._EBMConfigureGlobal.ListWorkMode.Remove(item);
            }

            #endregion
        }

        private void DealMainFrequency(List<MainFrequency_> listMF)
        {

            lock (Gtoken)
            {
                if (SingletonInfo.GetInstance()._EBMConfigureGlobal.ListMainFrequency != null)
                {
                    //去同项
                    foreach (MainFrequency_ item in listMF)
                    {
                        MainFrequency_ tmp = SingletonInfo.GetInstance()._EBMConfigureGlobal.ListMainFrequency.Find(s => s.ItemID.Equals(item.ItemID));
                        if (tmp != null)
                        {
                            SingletonInfo.GetInstance()._EBMConfigureGlobal.ListMainFrequency.Remove(tmp);
                        }
                    }
                }
                else
                {
                    SingletonInfo.GetInstance()._EBMConfigureGlobal.ListMainFrequency = new List<MainFrequency_>();
                }

                //增新项
                foreach (MainFrequency_ item in listMF)
                {
                    SingletonInfo.GetInstance()._EBMConfigureGlobal.ListMainFrequency.Add(item);
                }

                EbmStream.EB_Configure_Table = GetConfigureTable(ref EB_Configure_Table, false) ? EB_Configure_Table : null;
                EbMStream.Initialization();
            }
            UpdateDataTextNew((object)3);

            #region 删除记录
            foreach (MainFrequency_ item in listMF)
            {
                SingletonInfo.GetInstance()._EBMConfigureGlobal.ListMainFrequency.Remove(item);
            }
            #endregion
        }

        private void DealReback(List<Reback_> listRB)
        {

            lock (Gtoken)
            {
                if (SingletonInfo.GetInstance()._EBMConfigureGlobal.ListReback != null)
                {
                    //去同项
                    foreach (Reback_ item in listRB)
                    {
                        Reback_ tmp = SingletonInfo.GetInstance()._EBMConfigureGlobal.ListReback.Find(s => s.ItemID.Equals(item.ItemID));
                        if (tmp != null)
                        {
                            SingletonInfo.GetInstance()._EBMConfigureGlobal.ListReback.Remove(tmp);
                        }
                    }
                }
                else
                {
                    SingletonInfo.GetInstance()._EBMConfigureGlobal.ListReback = new List<Reback_>();
                }
                //增新项
                foreach (Reback_ item in listRB)
                {
                    SingletonInfo.GetInstance()._EBMConfigureGlobal.ListReback.Add(item);
                }
                EbmStream.EB_Configure_Table = GetConfigureTable(ref EB_Configure_Table, false) ? EB_Configure_Table : null;
                EbMStream.Initialization();
            }
            UpdateDataTextNew((object)3);
            #region 删除记录
            //增新项
            foreach (Reback_ item in listRB)
            {
                SingletonInfo.GetInstance()._EBMConfigureGlobal.ListReback.Remove(item);
            }
            #endregion
        }


        private void DealReback_Nation(List<Reback_Nation> listRB)
        {

            lock (Gtoken)
            {
                if (SingletonInfo.GetInstance()._EBMConfigureGlobal.ListReback != null)
                {
                    //去同项
                    foreach (Reback_Nation item in listRB)
                    {
                        Reback_Nation tmp = SingletonInfo.GetInstance()._EBMConfigureGlobal.ListReback_Nation.Find(s => s.ItemID.Equals(item.ItemID));
                        if (tmp != null)
                        {
                            SingletonInfo.GetInstance()._EBMConfigureGlobal.ListReback_Nation.Remove(tmp);
                        }
                    }
                }
                else
                {
                    SingletonInfo.GetInstance()._EBMConfigureGlobal.ListReback_Nation = new List<Reback_Nation>();
                }
                //增新项
                foreach (Reback_Nation item in listRB)
                {
                    SingletonInfo.GetInstance()._EBMConfigureGlobal.ListReback_Nation.Add(item);
                }

                EbmStream.EB_Configure_Table = GetConfigureTable(ref EB_Configure_Table, false) ? EB_Configure_Table : null;
                EbMStream.Initialization();
            }
            UpdateDataTextNew((object)3);
            #region 删除记录
            //增新项
            foreach (Reback_Nation item in listRB)
            {
                SingletonInfo.GetInstance()._EBMConfigureGlobal.ListReback_Nation.Remove(item);
            }
            #endregion
        }

        private void DealDefaltVolume(List<DefaltVolume_> listDV)
        {
            lock (Gtoken)
            {
                if (SingletonInfo.GetInstance()._EBMConfigureGlobal.ListDefaltVolume != null)
                {
                    //去同项
                    foreach (DefaltVolume_ item in listDV)
                    {
                        DefaltVolume_ tmp = SingletonInfo.GetInstance()._EBMConfigureGlobal.ListDefaltVolume.Find(s => s.ItemID.Equals(item.ItemID));
                        if (tmp != null)
                        {
                            SingletonInfo.GetInstance()._EBMConfigureGlobal.ListDefaltVolume.Remove(tmp);
                        }
                    }
                }
                else
                {
                    SingletonInfo.GetInstance()._EBMConfigureGlobal.ListDefaltVolume = new List<DefaltVolume_>();
                }
                //增新项
                foreach (DefaltVolume_ item in listDV)
                {
                    SingletonInfo.GetInstance()._EBMConfigureGlobal.ListDefaltVolume.Add(item);
                }

                EbmStream.EB_Configure_Table = GetConfigureTable(ref EB_Configure_Table, false) ? EB_Configure_Table : null;
                EbMStream.Initialization();
            }
            UpdateDataTextNew((object)3);
            #region 删除记录
            foreach (DefaltVolume_ item in listDV)
            {
                SingletonInfo.GetInstance()._EBMConfigureGlobal.ListDefaltVolume.Remove(item);
            }
            #endregion
        }

        private void DealRebackPeriod(List<RebackPeriod_> listRP)
        {
            lock (Gtoken)
            {
                if (SingletonInfo.GetInstance()._EBMConfigureGlobal.ListRebackPeriod != null)
                {
                    //去同项
                    foreach (RebackPeriod_ item in listRP)
                    {
                        RebackPeriod_ tmp = SingletonInfo.GetInstance()._EBMConfigureGlobal.ListRebackPeriod.Find(s => s.ItemID.Equals(item.ItemID));
                        if (tmp != null)
                        {
                            SingletonInfo.GetInstance()._EBMConfigureGlobal.ListRebackPeriod.Remove(tmp);
                        }
                    }
                }
                else
                {
                    SingletonInfo.GetInstance()._EBMConfigureGlobal.ListRebackPeriod = new List<RebackPeriod_>();
                }
                //增新项
                foreach (RebackPeriod_ item in listRP)
                {
                    SingletonInfo.GetInstance()._EBMConfigureGlobal.ListRebackPeriod.Add(item);
                }
                EbmStream.EB_Configure_Table = GetConfigureTable(ref EB_Configure_Table, false) ? EB_Configure_Table : null;
                EbMStream.Initialization();
            }
            UpdateDataTextNew((object)3);
            #region 删除记录
            foreach (RebackPeriod_ item in listRP)
            {
                SingletonInfo.GetInstance()._EBMConfigureGlobal.ListRebackPeriod.Remove(item);
            }
            #endregion
        }

        private void DealContentMoniterRetback(List<ContentMoniterRetback_> listCMR)
        {
            lock (Gtoken)
            {
                if (SingletonInfo.GetInstance()._EBMConfigureGlobal.ListContentMoniterRetback != null)
                {
                    //去同项
                    foreach (ContentMoniterRetback_ item in listCMR)
                    {
                        ContentMoniterRetback_ tmp = SingletonInfo.GetInstance()._EBMConfigureGlobal.ListContentMoniterRetback.Find(s => s.ItemID.Equals(item.ItemID));
                        if (tmp != null)
                        {
                            SingletonInfo.GetInstance()._EBMConfigureGlobal.ListContentMoniterRetback.Remove(tmp);
                        }
                    }
                }
                else
                {
                    SingletonInfo.GetInstance()._EBMConfigureGlobal.ListContentMoniterRetback = new List<ContentMoniterRetback_>();
                }
                //增新项
                foreach (ContentMoniterRetback_ item in listCMR)
                {
                    SingletonInfo.GetInstance()._EBMConfigureGlobal.ListContentMoniterRetback.Add(item);
                }
                EbmStream.EB_Configure_Table = GetConfigureTable(ref EB_Configure_Table, false) ? EB_Configure_Table : null;
                EbMStream.Initialization();
            }
            UpdateDataTextNew((object)3);
            #region 删除记录

            foreach (ContentMoniterRetback_ item in listCMR)
            {
                SingletonInfo.GetInstance()._EBMConfigureGlobal.ListContentMoniterRetback.Remove(item);
            }
            #endregion
        }

        private void DealContentRealMoniter(List<ContentRealMoniter_> listCRM)
        {
            lock (Gtoken)
            {
                if (SingletonInfo.GetInstance()._EBMConfigureGlobal.ListContentRealMoniter != null)
                {
                    //去同项
                    foreach (ContentRealMoniter_ item in listCRM)
                    {
                        ContentRealMoniter_ tmp = SingletonInfo.GetInstance()._EBMConfigureGlobal.ListContentRealMoniter.Find(s => s.ItemID.Equals(item.ItemID));
                        if (tmp != null)
                        {
                            SingletonInfo.GetInstance()._EBMConfigureGlobal.ListContentRealMoniter.Remove(tmp);
                        }
                    }
                }
                else
                {
                    SingletonInfo.GetInstance()._EBMConfigureGlobal.ListContentRealMoniter = new List<ContentRealMoniter_>();
                }
                //增新项
                foreach (ContentRealMoniter_ item in listCRM)
                {
                    SingletonInfo.GetInstance()._EBMConfigureGlobal.ListContentRealMoniter.Add(item);
                }

                EbmStream.EB_Configure_Table = GetConfigureTable(ref EB_Configure_Table, false) ? EB_Configure_Table : null;
                EbMStream.Initialization();
            }
            UpdateDataTextNew((object)3);
        }

        private void DealContentRealMoniterGX(List<ContentRealMoniterGX_> listCRM)
        {
            lock (Gtoken)
            {
                if (SingletonInfo.GetInstance()._EBMConfigureGlobal.ListContentRealMoniterGX != null)
                {
                    //去同项
                    foreach (ContentRealMoniterGX_ item in listCRM)
                    {
                        ContentRealMoniterGX_ tmp = SingletonInfo.GetInstance()._EBMConfigureGlobal.ListContentRealMoniterGX.Find(s => s.ItemID.Equals(item.ItemID));
                        if (tmp != null)
                        {
                            SingletonInfo.GetInstance()._EBMConfigureGlobal.ListContentRealMoniterGX.Remove(tmp);
                        }
                    }
                }
                else
                {
                    SingletonInfo.GetInstance()._EBMConfigureGlobal.ListContentRealMoniterGX = new List<ContentRealMoniterGX_>();
                }
                //增新项
                foreach (ContentRealMoniterGX_ item in listCRM)
                {
                    SingletonInfo.GetInstance()._EBMConfigureGlobal.ListContentRealMoniterGX.Add(item);
                }

                EbmStream.EB_Configure_Table = GetConfigureTable(ref EB_Configure_Table, false) ? EB_Configure_Table : null;
                EbMStream.Initialization();
            }
            UpdateDataTextNew((object)3);

            //增新项
            foreach (ContentRealMoniterGX_ item in listCRM)
            {
                SingletonInfo.GetInstance()._EBMConfigureGlobal.ListContentRealMoniterGX.Remove(item);
            }
        }

        private void DealStatusRetback(List<StatusRetback_> listSR)
        {
            lock (Gtoken)
            {
                if (SingletonInfo.GetInstance()._EBMConfigureGlobal.ListStatusRetback != null)
                {
                    //去同项
                    foreach (StatusRetback_ item in listSR)
                    {
                        StatusRetback_ tmp = SingletonInfo.GetInstance()._EBMConfigureGlobal.ListStatusRetback.Find(s => s.ItemID.Equals(item.ItemID));
                        if (tmp != null)
                        {
                            SingletonInfo.GetInstance()._EBMConfigureGlobal.ListStatusRetback.Remove(tmp);
                        }
                    }
                }
                else
                {
                    SingletonInfo.GetInstance()._EBMConfigureGlobal.ListStatusRetback = new List<StatusRetback_>();
                }
                //增新项
                foreach (StatusRetback_ item in listSR)
                {
                    SingletonInfo.GetInstance()._EBMConfigureGlobal.ListStatusRetback.Add(item);
                }

                EbmStream.EB_Configure_Table = GetConfigureTable(ref EB_Configure_Table, false) ? EB_Configure_Table : null;
                EbMStream.Initialization();
            }
            UpdateDataTextNew((object)3);

            #region 删除记录

            //foreach (SoftwareUpGrade_ item in listSUG)
            //{
            //    SingletonInfo.GetInstance()._EBMConfigureGlobal.ListSoftwareUpGrade.Remove(item);
            //}
            #endregion
        }

        private void DealSoftwareUpGrade(List<SoftwareUpGrade_> listSUG)
        {

            lock (Gtoken)
            {
                if (SingletonInfo.GetInstance()._EBMConfigureGlobal.ListSoftwareUpGrade != null)
                {
                    //去同项
                    foreach (SoftwareUpGrade_ item in listSUG)
                    {
                        SoftwareUpGrade_ tmp = SingletonInfo.GetInstance()._EBMConfigureGlobal.ListSoftwareUpGrade.Find(s => s.ItemID.Equals(item.ItemID));
                        if (tmp != null)
                        {
                            SingletonInfo.GetInstance()._EBMConfigureGlobal.ListSoftwareUpGrade.Remove(tmp);
                        }
                    }
                }
                else
                {
                    SingletonInfo.GetInstance()._EBMConfigureGlobal.ListSoftwareUpGrade = new List<SoftwareUpGrade_>();
                }

                //增新项
                foreach (SoftwareUpGrade_ item in listSUG)
                {
                    SingletonInfo.GetInstance()._EBMConfigureGlobal.ListSoftwareUpGrade.Add(item);
                }

                EbmStream.EB_Configure_Table = GetConfigureTable(ref EB_Configure_Table, false) ? EB_Configure_Table : null;
                EbMStream.Initialization();
            }
            UpdateDataTextNew((object)3);

            #region 删除记录

            foreach (SoftwareUpGrade_ item in listSUG)
            {
                SingletonInfo.GetInstance()._EBMConfigureGlobal.ListSoftwareUpGrade.Remove(item);
            }
            #endregion
        }

        private void DealRdsConfig(List<RdsConfig_> listRC)
        {

            lock (Gtoken)
            {
                if (SingletonInfo.GetInstance()._EBMConfigureGlobal.ListRdsConfig != null)
                {
                    //去同项
                    foreach (RdsConfig_ item in listRC)
                    {
                        RdsConfig_ tmp = SingletonInfo.GetInstance()._EBMConfigureGlobal.ListRdsConfig.Find(s => s.ItemID.Equals(item.ItemID));
                        if (tmp != null)
                        {
                            SingletonInfo.GetInstance()._EBMConfigureGlobal.ListRdsConfig.Remove(tmp);
                        }
                    }
                }
                else
                {
                    SingletonInfo.GetInstance()._EBMConfigureGlobal.ListRdsConfig = new List<RdsConfig_>();
                }
                //增新项
                foreach (RdsConfig_ item in listRC)
                {
                    SingletonInfo.GetInstance()._EBMConfigureGlobal.ListRdsConfig.Add(item);
                }

                EbmStream.EB_Configure_Table = GetConfigureTable(ref EB_Configure_Table, false) ? EB_Configure_Table : null;
                EbMStream.Initialization();
            }
            UpdateDataTextNew((object)3);
            SaveTmpData();
        }

        private void DelEBMConfigure2Global(OperatorData op)
        {
            List<string> DelItemList = new List<string>(op.Data.ToString().Split(','));
            switch (op.ModuleType)
            { 
                case "1":
                    lock (Gtoken)
                    {
                        if (SingletonInfo.GetInstance()._EBMConfigureGlobal.ListTimeService != null)
                        {
                            if (DelItemList.Count > 0)
                            {
                                foreach (string item in DelItemList)
                                {
                                    TimeService_ tmp = SingletonInfo.GetInstance()._EBMConfigureGlobal.ListTimeService.Find(s => s.ItemID.Equals(item));
                                    SingletonInfo.GetInstance()._EBMConfigureGlobal.ListTimeService.Remove(tmp);
                                }
                            }
                        }
                    }
                    break;
                case "2":
                    lock (Gtoken)
                    {
                        if (SingletonInfo.GetInstance()._EBMConfigureGlobal.ListSetAddress != null)
                        {
                            if (DelItemList.Count > 0)
                            {
                                foreach (string item in DelItemList)
                                {
                                    SetAddress_ tmp = SingletonInfo.GetInstance()._EBMConfigureGlobal.ListSetAddress.Find(s => s.ItemID.Equals(item));
                                    SingletonInfo.GetInstance()._EBMConfigureGlobal.ListSetAddress.Remove(tmp);
                                }
                            }
                        }

                    }
                    break;
                case "3":
                    lock (Gtoken)
                    {
                        if (SingletonInfo.GetInstance()._EBMConfigureGlobal.ListWorkMode != null)
                        {
                            if (DelItemList.Count > 0)
                            {
                                foreach (string item in DelItemList)
                                {
                                    WorkMode_ tmp = SingletonInfo.GetInstance()._EBMConfigureGlobal.ListWorkMode.Find(s => s.ItemID.Equals(item));
                                    SingletonInfo.GetInstance()._EBMConfigureGlobal.ListWorkMode.Remove(tmp);
                                }
                            }
                        }

                    }
                    break;
                case "4":
                    lock (Gtoken)
                    {
                        if (SingletonInfo.GetInstance()._EBMConfigureGlobal.ListMainFrequency != null)
                        {
                            if (DelItemList.Count > 0)
                            {
                                foreach (string item in DelItemList)
                                {
                                    MainFrequency_ tmp = SingletonInfo.GetInstance()._EBMConfigureGlobal.ListMainFrequency.Find(s => s.ItemID.Equals(item));
                                    SingletonInfo.GetInstance()._EBMConfigureGlobal.ListMainFrequency.Remove(tmp);
                                }
                            }
                        }

                    }
                    break;
                case "5":
                    lock (Gtoken)
                    {
                        if (SingletonInfo.GetInstance().IsGXProtocol)
                        {
                            if (SingletonInfo.GetInstance()._EBMConfigureGlobal.ListReback != null)
                            {
                                if (DelItemList.Count > 0)
                                {
                                    foreach (string item in DelItemList)
                                    {
                                        Reback_ tmp = SingletonInfo.GetInstance()._EBMConfigureGlobal.ListReback.Find(s => s.ItemID.Equals(item));
                                        SingletonInfo.GetInstance()._EBMConfigureGlobal.ListReback.Remove(tmp);
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (SingletonInfo.GetInstance()._EBMConfigureGlobal.ListReback_Nation != null)
                            {
                                if (DelItemList.Count > 0)
                                {
                                    foreach (string item in DelItemList)
                                    {
                                        Reback_Nation tmp = SingletonInfo.GetInstance()._EBMConfigureGlobal.ListReback_Nation.Find(s => s.ItemID.Equals(item));
                                        SingletonInfo.GetInstance()._EBMConfigureGlobal.ListReback_Nation.Remove(tmp);
                                    }
                                }
                            }
                        }
                     

                    }
                    break;
                case "6":
                    lock (Gtoken)
                    {
                        if (SingletonInfo.GetInstance()._EBMConfigureGlobal.ListDefaltVolume != null)
                        {
                            if (DelItemList.Count > 0)
                            {
                                foreach (string item in DelItemList)
                                {
                                    DefaltVolume_ tmp = SingletonInfo.GetInstance()._EBMConfigureGlobal.ListDefaltVolume.Find(s => s.ItemID.Equals(item));
                                    SingletonInfo.GetInstance()._EBMConfigureGlobal.ListDefaltVolume.Remove(tmp);
                                }
                            }
                        }

                    }
                    break;
                case "7":
                    lock (Gtoken)
                    {
                        if (SingletonInfo.GetInstance()._EBMConfigureGlobal.ListRebackPeriod != null)
                        {
                            if (DelItemList.Count > 0)
                            {
                                foreach (string item in DelItemList)
                                {
                                    RebackPeriod_ tmp = SingletonInfo.GetInstance()._EBMConfigureGlobal.ListRebackPeriod.Find(s => s.ItemID.Equals(item));
                                    SingletonInfo.GetInstance()._EBMConfigureGlobal.ListRebackPeriod.Remove(tmp);
                                }
                            }
                        }

                    }
                    break;
                case "104":
                    lock (Gtoken)
                    {
                        if (SingletonInfo.GetInstance()._EBMConfigureGlobal.ListContentMoniterRetback != null)
                        {
                            if (DelItemList.Count > 0)
                            {
                                foreach (string item in DelItemList)
                                {
                                    ContentMoniterRetback_ tmp = SingletonInfo.GetInstance()._EBMConfigureGlobal.ListContentMoniterRetback.Find(s => s.ItemID.Equals(item));
                                    SingletonInfo.GetInstance()._EBMConfigureGlobal.ListContentMoniterRetback.Remove(tmp);
                                }
                            }
                        }

                    }
                    break;
                case "105":
                    lock (Gtoken)
                    {
                        if (SingletonInfo.GetInstance()._EBMConfigureGlobal.ListContentRealMoniter != null)
                        {
                            if (DelItemList.Count > 0)
                            {
                                foreach (string item in DelItemList)
                                {
                                    ContentRealMoniter_ tmp = SingletonInfo.GetInstance()._EBMConfigureGlobal.ListContentRealMoniter.Find(s => s.ItemID.Equals(item));
                                    SingletonInfo.GetInstance()._EBMConfigureGlobal.ListContentRealMoniter.Remove(tmp);
                                }
                            }
                        }

                    }
                    break;
                case "106":
                    lock (Gtoken)
                    {
                        if (SingletonInfo.GetInstance()._EBMConfigureGlobal.ListStatusRetback != null)
                        {
                            if (DelItemList.Count > 0)
                            {
                                foreach (string item in DelItemList)
                                {
                                    StatusRetback_ tmp = SingletonInfo.GetInstance()._EBMConfigureGlobal.ListStatusRetback.Find(s => s.ItemID.Equals(item));
                                    SingletonInfo.GetInstance()._EBMConfigureGlobal.ListStatusRetback.Remove(tmp);
                                }
                            }
                        }

                    }
                    break;
                case "240":
                    lock (Gtoken)
                    {
                        if (SingletonInfo.GetInstance()._EBMConfigureGlobal.ListSoftwareUpGrade != null)
                        {
                            if (DelItemList.Count > 0)
                            {
                                foreach (string item in DelItemList)
                                {
                                    SoftwareUpGrade_ tmp = SingletonInfo.GetInstance()._EBMConfigureGlobal.ListSoftwareUpGrade.Find(s => s.ItemID.Equals(item));
                                    SingletonInfo.GetInstance()._EBMConfigureGlobal.ListSoftwareUpGrade.Remove(tmp);
                                }
                            }
                        }

                    }
                    break;
                case "8":
                    lock (Gtoken)
                    {
                        if (SingletonInfo.GetInstance()._EBMConfigureGlobal.ListRdsConfig != null)
                        {
                            if (DelItemList.Count > 0)
                            {
                                foreach (string item in DelItemList)
                                {
                                    RdsConfig_ tmp = SingletonInfo.GetInstance()._EBMConfigureGlobal.ListRdsConfig.Find(s => s.ItemID.Equals(item));
                                    SingletonInfo.GetInstance()._EBMConfigureGlobal.ListRdsConfig.Remove(tmp);
                                }
                            }
                        }

                    }
                    break;
            }
            EbmStream.EB_Configure_Table = GetConfigureTable(ref EB_Configure_Table, false) ? EB_Configure_Table : null;
            UpdateDataTextNew((object)3);
        }

        #endregion
     
        #region  DailyBroadcast数据处理

        private void DealDailyBroadcast2Global(OperatorData op)
        {
            switch (op.OperatorType)
            {
                case "AddDailyBroadcast":

                case "ModifyDailyBroadcast":
                    switch (op.ModuleType)
                    {
                        case "1":
                            List<ChangeProgram_> listCP = (List<ChangeProgram_>)op.Data;
                            DealChangeProgram(listCP);

                            break;
                        case "3":
                            List<PlayCtrl_> listPC = (List<PlayCtrl_>)op.Data;
                            DealPlayCtrl(listPC);
                            break;
                        case "4":
                            List<OutSwitch_> listOS = (List<OutSwitch_>)op.Data;
                            DealOutSwitch(listOS);
                            break;
                        case "5":
                            List<RdsTransfer_> listRT = (List<RdsTransfer_>)op.Data;
                            DealRdsTransfer(listRT);
                            break;
                    }
                    break;
                case "DelDailyBroadcast":
                    DelDailyBroadcast2Global(op);
                    break;
            }
        }

        private void DealChangeProgram(List<ChangeProgram_> listCP)
        {

            lock (Gtoken)
            {
                if (SingletonInfo.GetInstance()._DailyBroadcastGlobal.ListChangeProgram != null)
                {
                    //去同项
                    foreach (ChangeProgram_ item in listCP)
                    {
                        ChangeProgram_ tmp = SingletonInfo.GetInstance()._DailyBroadcastGlobal.ListChangeProgram.Find(s => s.ItemID.Equals(item.ItemID));
                        if (tmp != null)
                        {
                            SingletonInfo.GetInstance()._DailyBroadcastGlobal.ListChangeProgram.Remove(tmp);
                        }

                    }

                }
                else
                {
                    SingletonInfo.GetInstance()._DailyBroadcastGlobal.ListChangeProgram = new List<ChangeProgram_>();
                }
                //增新项
                foreach (ChangeProgram_ item in listCP)
                {
                    SingletonInfo.GetInstance()._DailyBroadcastGlobal.ListChangeProgram.Add(item);
                }
           
                EbmStream.Daily_Broadcast_Table = GetDailyBroadcastTable(ref Daily_Broadcast_Table) ? Daily_Broadcast_Table : null;
                EbMStream.Initialization();
            }
            UpdateDataTextNew((object)4);
        }

        private void DealPlayCtrl(List<PlayCtrl_> listPC)
        {

            lock (Gtoken)
            {
                if (SingletonInfo.GetInstance()._DailyBroadcastGlobal.ListPlayCtrl != null)
                {
                    //去同项
                    foreach (PlayCtrl_ item in listPC)
                    {
                        PlayCtrl_ tmp = SingletonInfo.GetInstance()._DailyBroadcastGlobal.ListPlayCtrl.Find(s => s.ItemID.Equals(item.ItemID));
                        if (tmp != null)
                        {
                            SingletonInfo.GetInstance()._DailyBroadcastGlobal.ListPlayCtrl.Remove(tmp);
                        }

                    }

                }
                else
                {
                    SingletonInfo.GetInstance()._DailyBroadcastGlobal.ListPlayCtrl = new List<PlayCtrl_>();
                }

                //增新项
                foreach (PlayCtrl_ item in listPC)
                {
                    SingletonInfo.GetInstance()._DailyBroadcastGlobal.ListPlayCtrl.Add(item);
                }

                EbmStream.Daily_Broadcast_Table = GetDailyBroadcastTable(ref Daily_Broadcast_Table) ? Daily_Broadcast_Table : null;
                EbMStream.Initialization();
            }
            UpdateDataTextNew((object)4);
        }

        private void DealOutSwitch(List<OutSwitch_> listOS)
        {

            lock (Gtoken)
            {
                if (SingletonInfo.GetInstance()._DailyBroadcastGlobal.ListOutSwitch != null)
                {
                    //去同项
                    foreach (OutSwitch_ item in listOS)
                    {
                        OutSwitch_ tmp = SingletonInfo.GetInstance()._DailyBroadcastGlobal.ListOutSwitch.Find(s => s.ItemID.Equals(item.ItemID));
                        if (tmp != null)
                        {
                            SingletonInfo.GetInstance()._DailyBroadcastGlobal.ListOutSwitch.Remove(tmp);
                        }

                    }

                }
                else
                {
                    SingletonInfo.GetInstance()._DailyBroadcastGlobal.ListOutSwitch = new List<OutSwitch_>();
                }

                //增新项
                foreach (OutSwitch_ item in listOS)
                {
                    SingletonInfo.GetInstance()._DailyBroadcastGlobal.ListOutSwitch.Add(item);
                }

                EbmStream.Daily_Broadcast_Table = GetDailyBroadcastTable(ref Daily_Broadcast_Table) ? Daily_Broadcast_Table : null;
                EbMStream.Initialization();
            }
            UpdateDataTextNew((object)4);

            #region 删除记录 
            foreach (OutSwitch_ item in listOS)
            {
                SingletonInfo.GetInstance()._DailyBroadcastGlobal.ListOutSwitch.Remove(item);
            }
            #endregion
        }

        private void DealRdsTransfer(List<RdsTransfer_> listRT)
        {

            lock (Gtoken)
            {
                if (SingletonInfo.GetInstance()._DailyBroadcastGlobal.ListRdsTransfer != null)
                {
                    //去同项
                    foreach (RdsTransfer_ item in listRT)
                    {
                        RdsTransfer_ tmp = SingletonInfo.GetInstance()._DailyBroadcastGlobal.ListRdsTransfer.Find(s => s.ItemID.Equals(item.ItemID));
                        if (tmp != null)
                        {
                            SingletonInfo.GetInstance()._DailyBroadcastGlobal.ListRdsTransfer.Remove(tmp);
                        }

                    }

                }
                else
                {
                    SingletonInfo.GetInstance()._DailyBroadcastGlobal.ListRdsTransfer = new List<RdsTransfer_>();
                }

                //增新项
                foreach (RdsTransfer_ item in listRT)
                {
                    SingletonInfo.GetInstance()._DailyBroadcastGlobal.ListRdsTransfer.Add(item);
                }

                EbmStream.Daily_Broadcast_Table = GetDailyBroadcastTable(ref Daily_Broadcast_Table) ? Daily_Broadcast_Table : null;
                EbMStream.Initialization();
            }
            UpdateDataTextNew((object)4);
        }

        private void DelDailyBroadcast2Global(OperatorData op)
        {
            List<string> DelItemList = new List<string>(op.Data.ToString().Split(','));

            StopPorgram_ delone = new StopPorgram_();
            switch (op.ModuleType)
            {
                //case "1":
                //    lock (Gtoken)
                //    {
                //        if (SingletonInfo.GetInstance()._DailyBroadcastGlobal.ListChangeProgram  != null)
                //        {
                //            if (DelItemList.Count > 0)
                //            {
                //                foreach (string item in DelItemList)
                //                {
                //                    ChangeProgram_ tmp = SingletonInfo.GetInstance()._DailyBroadcastGlobal.ListChangeProgram.Find(s => s.ItemID.Equals(item));
                //                    SingletonInfo.GetInstance()._DailyBroadcastGlobal.ListChangeProgram.Remove(tmp);
                //                }
                //            }
                //        }

                //    }
                //    break;
                case "2":
                    lock (Gtoken)
                    {
                        if (SingletonInfo.GetInstance()._DailyBroadcastGlobal.ListChangeProgram != null)
                        {
                            if (DelItemList.Count > 0)
                            {
                                foreach (string item in DelItemList)
                                {
                                    ChangeProgram_ tmp = SingletonInfo.GetInstance()._DailyBroadcastGlobal.ListChangeProgram.Find(s => s.ItemID.Equals(item));

                                    StopPorgram_ addstopone = new StopPorgram_();
                                    addstopone.ItemID = tmp.ItemID;
                                    addstopone.Program = new DailyCmdProgramStop();
                                    addstopone.Program.NetID = tmp.Program.NetID;
                                    addstopone.Program.TSID = tmp.Program.TSID;
                                    addstopone.Program.ServiceID = tmp.Program.ServiceID;
                                    addstopone.Program.PCR_PID = (short)tmp.Program.PCR_PID;
                                    addstopone.Program.Program_PID = (short)tmp.Program.Program_PID;
                                    addstopone.Program.list_Terminal_Address = tmp.Program.list_Terminal_Address;
                                    addstopone.Program.S_cmd_id = tmp.Program.S_cmd_id;

                                    SingletonInfo.GetInstance()._DailyBroadcastGlobal.ListChangeProgram.Remove(tmp);



                                    if (SingletonInfo.GetInstance()._DailyBroadcastGlobal.ListStopPorgram==null)
                                    {
                                        SingletonInfo.GetInstance()._DailyBroadcastGlobal.ListStopPorgram = new List<StopPorgram_>();
                                    }

                                    SingletonInfo.GetInstance()._DailyBroadcastGlobal.ListStopPorgram.Add(addstopone);
                                    delone = addstopone;
                                }
                            }
                        }

                    }
                    break;
                //case "3":
                //    lock (Gtoken)
                //    {
                //        if (SingletonInfo.GetInstance()._DailyBroadcastGlobal.ListPlayCtrl != null)
                //        {
                //            if (DelItemList.Count > 0)
                //            {
                //                foreach (string item in DelItemList)
                //                {
                //                    PlayCtrl_ tmp = SingletonInfo.GetInstance()._DailyBroadcastGlobal.ListPlayCtrl.Find(s => s.ItemID.Equals(item));
                //                    SingletonInfo.GetInstance()._DailyBroadcastGlobal.ListPlayCtrl.Remove(tmp);
                //                }
                //            }
                //        }

                //    }
                //    break;
                //case "4":
                //    lock (Gtoken)
                //    {
                //        if (SingletonInfo.GetInstance()._DailyBroadcastGlobal.ListOutSwitch != null)
                //        {
                //            if (DelItemList.Count > 0)
                //            {
                //                foreach (string item in DelItemList)
                //                {
                //                    OutSwitch_ tmp = SingletonInfo.GetInstance()._DailyBroadcastGlobal.ListOutSwitch.Find(s => s.ItemID.Equals(item));
                //                    SingletonInfo.GetInstance()._DailyBroadcastGlobal.ListOutSwitch.Remove(tmp);
                //                }
                //            }
                //        }

                //    }
                //    break;
                //case "5":
                //    lock (Gtoken)
                //    {
                //        if (SingletonInfo.GetInstance()._DailyBroadcastGlobal.ListRdsTransfer != null)
                //        {
                //            if (DelItemList.Count > 0)
                //            {
                //                foreach (string item in DelItemList)
                //                {
                //                    RdsTransfer_ tmp = SingletonInfo.GetInstance()._DailyBroadcastGlobal.ListRdsTransfer.Find(s => s.ItemID.Equals(item));
                //                    SingletonInfo.GetInstance()._DailyBroadcastGlobal.ListRdsTransfer.Remove(tmp);
                //                }
                //            }
                //        }

                //    }
                //    break;

            }
            EbmStream.Daily_Broadcast_Table = GetDailyBroadcastTable(ref Daily_Broadcast_Table) ? Daily_Broadcast_Table : null;
            EbMStream.Initialization();
            UpdateDataTextNew((object)4);


            #region  删除停播记录

            Thread.Sleep(2000);
            StopPorgram_ tmpdd = SingletonInfo.GetInstance()._DailyBroadcastGlobal.ListStopPorgram.Find(s => s.ItemID.Equals(delone.ItemID));
            SingletonInfo.GetInstance()._DailyBroadcastGlobal.ListStopPorgram.Remove(tmpdd);

            EbmStream.Daily_Broadcast_Table = GetDailyBroadcastTable(ref Daily_Broadcast_Table) ? Daily_Broadcast_Table : null;
            EbMStream.Initialization();

            #endregion


        }
        #endregion

        #region CertAuth 数据处理
        private void DelCertAuth2Global(string delstr)
        {
            List<string> DelCertAuthDataIdList = new List<string>(delstr.Split(','));//string[] 转List<string>
            lock (Gtoken)
            {
                if (SingletonInfo.GetInstance()._CertAuthGlobal.list_CertAuth != null)
                {
                    //去同向
                    if (DelCertAuthDataIdList.Count > 0)
                    {
                        foreach (string item in DelCertAuthDataIdList)
                        {
                            CertAuth_ tmp = SingletonInfo.GetInstance()._CertAuthGlobal.list_CertAuth.Find(s => s.CertAuthDataId.Equals(item));
                            SingletonInfo.GetInstance()._CertAuthGlobal.list_CertAuth.Remove(tmp);
                        }
                    }
                }
             
                EbmStream.EB_CertAuth_Table = GetCertAuthTable(ref EB_CertAuth_Table) ? EB_CertAuth_Table : null;
                EbMStream.Initialization();
            }
            UpdateDataTextNew((object)2);
        }

        private void DelCert2Global(string delstr)
        {
            List<string> DelCertDataIdList = new List<string>(delstr.Split(','));//string[] 转List<string>
            lock (Gtoken)
            {
                if (SingletonInfo.GetInstance()._CertAuthGlobal.list_Cert != null)
                {
                    //去同向
                    if (DelCertDataIdList.Count > 0)
                    {
                        foreach (string item in DelCertDataIdList)
                        {
                            Cert_ tmp = SingletonInfo.GetInstance()._CertAuthGlobal.list_Cert.Find(s => s.CertDataId.Equals(item));
                            SingletonInfo.GetInstance()._CertAuthGlobal.list_Cert.Remove(tmp);
                        }
                    }
                }

                EbmStream.EB_CertAuth_Table = GetCertAuthTable(ref EB_CertAuth_Table) ? EB_CertAuth_Table : null;
                EbMStream.Initialization();
            }
            UpdateDataTextNew((object)2);
        }

        private void DealCertAuth2Global(List<CertAuthTmp> AddCertAuthList)
        {
            List<string> DelCertAuthDataIdList = new List<string>();
            lock (Gtoken)
            {
                if (SingletonInfo.GetInstance()._CertAuthGlobal.list_CertAuth != null)
                {
                    //查同项
                    foreach (CertAuthTmp item in AddCertAuthList)
                    {
                        foreach (var itemGlobal in SingletonInfo.GetInstance()._CertAuthGlobal.list_CertAuth)
                        {
                            if (itemGlobal.CertAuthDataId == item.CertAuthDataid)
                            {
                                DelCertAuthDataIdList.Add(itemGlobal.CertAuthDataId);
                            }
                        }
                    }
                    //去同向
                    if (DelCertAuthDataIdList.Count > 0)
                    {
                        foreach (string item in DelCertAuthDataIdList)
                        {
                            CertAuth_ tmp = SingletonInfo.GetInstance()._CertAuthGlobal.list_CertAuth.Find(s => s.CertAuthDataId.Equals(item));
                            SingletonInfo.GetInstance()._CertAuthGlobal.list_CertAuth.Remove(tmp);
                        }
                    }
                }
                else
                {
                    SingletonInfo.GetInstance()._CertAuthGlobal.list_CertAuth = new List<CertAuth_>();
                }

                //增加新项

                foreach (CertAuthTmp item in AddCertAuthList)
                {
                    CertAuth_ add = new CertAuth_();
                    add.CertAuthDataId = item.CertAuthDataid;
                    add.CertAuth_data = item.CertAuthDataHexStr;
                    add.SendState = true;
                    add.Tag = 1;
                    SingletonInfo.GetInstance()._CertAuthGlobal.list_CertAuth.Add(add);
                }
           
               EbmStream.EB_CertAuth_Table = GetCertAuthTable(ref EB_CertAuth_Table) ? EB_CertAuth_Table : null;
               EbMStream.Initialization();
            }
            UpdateDataTextNew((object)2);


            #region 删除记录
            SingletonInfo.GetInstance()._CertAuthGlobal.list_Cert.Clear();
            SingletonInfo.GetInstance()._CertAuthGlobal.list_CertAuth.Clear();
            #endregion
        }

        private void DealCert2Global(List<CertTmp> AddCertList)
        {
            List<string> DelCertDataIdList = new List<string>();
            lock (Gtoken)
            {
                if (SingletonInfo.GetInstance()._CertAuthGlobal.list_Cert != null)
                {
                    //查同项
                    foreach (CertTmp item in AddCertList)
                    {
                        foreach (var itemGlobal in SingletonInfo.GetInstance()._CertAuthGlobal.list_Cert)
                        {
                            if (itemGlobal.CertDataId == item.CertDataid)
                            {
                                DelCertDataIdList.Add(itemGlobal.CertDataId);
                            }
                        }
                    }
                    //去同向
                    if (DelCertDataIdList.Count > 0)
                    {
                        foreach (string item in DelCertDataIdList)
                        {
                            Cert_ tmp = SingletonInfo.GetInstance()._CertAuthGlobal.list_Cert.Find(s => s.CertDataId.Equals(item));
                            SingletonInfo.GetInstance()._CertAuthGlobal.list_Cert.Remove(tmp);
                        }
                    }
                }
                else
                {
                    SingletonInfo.GetInstance()._CertAuthGlobal.list_Cert = new List<Cert_>();
                }

                //增加新项
                foreach (CertTmp item in AddCertList)
                {
                    Cert_ add = new Cert_();
                    add.CertDataId = item.CertDataid;
                    add.Cert_data = item.CertDataHexStr;
                    add.SendState = true;
                    add.Tag = 1;
                    SingletonInfo.GetInstance()._CertAuthGlobal.list_Cert.Add(add);
                }
                //EbmStream.EB_CertAuth_Table = GetCertAuthTable(ref EB_CertAuth_Table) ? EB_CertAuth_Table : null;
               // EbMStream.Initialization();
            }
           // UpdateDataTextNew((object)2);

            //#region 删除记录
            //SingletonInfo.GetInstance()._CertAuthGlobal.list_Cert.Clear();
            //SingletonInfo.GetInstance()._CertAuthGlobal.list_CertAuth.Clear();
            //#endregion
        }
        #endregion

        #region EBContent 数据处理
        private void DealEBContent2Global(OperatorData op)
        {
            switch (op.OperatorType)
            {
                case "AddEBContent":

                case "ModifyEBContent":

                    List<EBMID_Content> listEBContent_ = (List<EBMID_Content>)op.Data;

                    lock (Gtoken)
                    {
                        if (SingletonInfo.GetInstance()._EBContentGlobal.ListEBContent != null)
                        {
                            //去同项
                            foreach (EBMID_Content item in listEBContent_)
                            {
                                EBMID_Content tmp = SingletonInfo.GetInstance()._EBContentGlobal.ListEBContent.Find(s => s.EBM_ID.Equals(item.EBM_ID));
                                if (tmp != null)
                                {
                                    SingletonInfo.GetInstance()._EBContentGlobal.ListEBContent.Remove(tmp);
                                }
                            }
                        }
                        else
                        {
                            SingletonInfo.GetInstance()._EBContentGlobal.ListEBContent = new List<EBMID_Content>();
                        }
                        //增新项
                        foreach (EBMID_Content item in listEBContent_)
                        {
                            SingletonInfo.GetInstance()._EBContentGlobal.ListEBContent.Add(item);
                        }
                  
                        EbmStream.list_EB_Content_Table = GetlistContentTable(ref list_EB_Content_Table) ? list_EB_Content_Table : null;
                        EbMStream.Initialization();
                    }
                    UpdateDataTextNew((object)5);


                    #region 删除记录
                    foreach (EBMID_Content item in listEBContent_)
                    {
                        SingletonInfo.GetInstance()._EBContentGlobal.ListEBContent.Remove(item);
                    }
                    #endregion
                    GC.Collect();

                    break;
                case "DelEBContent":
                    DelEBContent2Global(op);
                    break;
            }
        }

        private void DelEBContent2Global(OperatorData op)
        {
            List<string> DelItemList = new List<string>(op.Data.ToString().Split(','));

            lock (Gtoken)
            {
                if (SingletonInfo.GetInstance()._EBContentGlobal.ListEBContent != null)
                {
                    if (DelItemList.Count > 0)
                    {
                        foreach (string item in DelItemList)
                        {
                            EBMID_Content tmp = SingletonInfo.GetInstance()._EBContentGlobal.ListEBContent.Find(s => s.EBM_ID.Equals(item));
                            SingletonInfo.GetInstance()._EBContentGlobal.ListEBContent.Remove(tmp);
                        }
                    }
                }
            }
            EbmStream.list_EB_Content_Table = GetlistContentTable(ref list_EB_Content_Table) ? list_EB_Content_Table : null;
            EbMStream.Initialization();
            UpdateDataTextNew((object)5);
        }
        #endregion

        #region  广西县平台切换通道数据处理
        private void DealChangeInputChannel(OperatorData op)
        {

            CountyplatformChangechannel changechannelinfo = (CountyplatformChangechannel)op.Data;

            string ip = changechannelinfo.PhysicalCode.Split(':')[0];
            int port = Convert.ToInt32(changechannelinfo.PhysicalCode.Split(':')[1]);

            int channelId = Convert.ToInt32(changechannelinfo.inputchannel);

            string resourcecode = changechannelinfo.ResourceCode;


            IPEndPoint ie = new IPEndPoint(IPAddress.Parse(ip), port);  //临时方案
            OnorOFFResponse res = SwitchChannel(channelId, resourcecode, ie);
            //if (SingletonInfo.GetInstance().PhysicalCode2IPDic.ContainsKey(changechannelinfo.PhysicalCode))
            //{
            //    ie = SingletonInfo.GetInstance().PhysicalCode2IPDic[changechannelinfo.PhysicalCode];
            //          OnorOFFResponse res = SwitchChannel(1,);
            //}
             


        }
        #endregion

        public bool GetCertAuthTable(ref EBCertAuthTable oldTable)
        {
            try
            {
                oldTable.list_Cert_data = dataHelper.GetSendCert(SingletonInfo.GetInstance()._CertAuthGlobal.list_Cert);
                oldTable.list_CertAuth_data = dataHelper.GetSendCertAuth(SingletonInfo.GetInstance()._CertAuthGlobal.list_CertAuth);
                oldTable.Repeat_times = 0;
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public bool GetEBIndexTable(ref EBIndexTable oldTable)
        {
            try
            {
                List<EBIndex> listEbIndex = dataHelper.GetSendEBMIndex(SingletonInfo.GetInstance()._EBMIndexGlobal.ListEbIndex);
                oldTable.ListEbIndex = listEbIndex;
                if (SingletonInfo.GetInstance().IsGXProtocol)
                {
                    oldTable.Repeat_times = 0;//广西是否重复发  待定 20190411
                }
                else
                {
                    oldTable.Repeat_times = 1;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool GetConfigureTable(ref EBConfigureTable oldTable, bool isTimeSend)
        {
            try
            {
                List<ConfigureCmd> configureCmd = isTimeSend ? GetSendTimeSerConfigureCmd() : GetSendConfigureCmd();
                if (configureCmd == null || configureCmd.Count == 0)
                {
                    if (oldTable != null) oldTable.list_configure_cmd = null;
                    return false;
                }
                if (oldTable == null)
                {
                    oldTable = new EBConfigureTable();
                    oldTable.Table_id = 0xfb;
                    oldTable.Table_id_extension = 0;
                }
                oldTable.list_configure_cmd = configureCmd;
                oldTable.Repeat_times =1;
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public bool GetDailyBroadcastTable(ref DailyBroadcastTable oldTable)
        {
            try
            {
                List<DailyCmd> daily = GetSendDailyCmd();
                if (daily == null || daily.Count == 0)
                {
                    if (oldTable != null) oldTable.list_daily_cmd = null;
                    return false;
                }
                //if (oldTable == null)
                //{
                //    oldTable = new DailyBroadcastTable();
                //    oldTable.Table_id = 0xfa;
                //    oldTable.Table_id_extension = 0;
                //}
                oldTable.list_daily_cmd = daily;
                oldTable.Repeat_times =0;//表示重复发送
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool GetlistContentTable(ref  List<EBContentTable> oldTableList)
        {
            try
            {
                oldTableList.Clear();

                foreach (EBMID_Content item in SingletonInfo.GetInstance()._EBContentGlobal.ListEBContent)
                {
                    EBContentTable pp = new EBContentTable();
                    pp.ProtocolGX = SingletonInfo.GetInstance().IsGXProtocol;
                    List<MultilangualContent> listMulti = GetSendMultilangualContentNew(item.MultilangualContentList);
                    if (listMulti == null || listMulti.Count == 0)
                    {
                        // return false;
                        continue;
                    }
                    pp.list_multilangual_content = listMulti;
                    pp.S_EBM_id = item.EBM_ID;
                    pp.Repeat_times =0;//表示重复发送
                    pp.Table_id = 0xfe;
                    pp.Table_id_extension = 0;
                    oldTableList.Add(pp);
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
                return false;
            }
        }

        private List<MultilangualContent> GetSendMultilangualContentNew(List<MultilangualContent_> EBContent_List)
        {
            if (EBContent_List.Count == 0)
            {
                return null;
            }
            List<MultilangualContent> listMulti = new List<MultilangualContent>();
            foreach (var content in EBContent_List)
            {
                MultilangualContent tmp = new MultilangualContent();
                tmp.B_code_character_set = Convert.ToByte(content.B_code_character_set);
                tmp.B_message_text = Encoding.GetEncoding("GB2312").GetBytes(content.B_message_text); 
                tmp.list_auxiliary_data=Getlist_auxiliary_data(content.list_auxiliary_data);
                tmp.S_agency_name=content.S_agency_name;
                tmp.S_language_code=content.S_language_code;
                listMulti.Add(tmp);
            }
            return listMulti;
        }

        private List<AuxiliaryData> Getlist_auxiliary_data(List<AuxiliaryData_> list_auxiliary_data)
        {
            List<AuxiliaryData> tmp = new List<AuxiliaryData>();

            foreach (AuxiliaryData_ item in list_auxiliary_data)
            {
                AuxiliaryData sing = new AuxiliaryData();
                sing.B_auxiliary_data_type = Convert.ToByte(item.Type);
                string[] strs = item.DisplayData.Split(' ');
                List<byte> strbytelist = new List<byte>();
                foreach (var it in strs)
                {
                    strbytelist.Add((byte) Convert.ToInt32(it, 16));
                }
                sing.B_auxiliary_data = strbytelist.ToArray();

                tmp.Add(sing);

            }

            return tmp;
        }

        private BindingCollection<Configure> GetConfigureCollection(EBMConfigureGlobal_ _EBMConfigureGlobal)
        {
            BindingCollection<Configure> TotalConfig_List = new BindingCollection<Configure>();
            if (_EBMConfigureGlobal.ListTimeService!=null)
            {
                foreach (var item in _EBMConfigureGlobal.ListTimeService)
                {
                    TotalConfig_List.Add(item);
                }
            }

            if (_EBMConfigureGlobal.ListSetAddress!=null)
            {
                foreach (var item in _EBMConfigureGlobal.ListSetAddress)
                {
                    TotalConfig_List.Add(item);
                }
            }

            if (_EBMConfigureGlobal.ListWorkMode!=null)
            {
                foreach (var item in _EBMConfigureGlobal.ListWorkMode)
                {
                    TotalConfig_List.Add(item);
                }
            }

            if (_EBMConfigureGlobal.ListMainFrequency!=null)
            {
                foreach (var item in _EBMConfigureGlobal.ListMainFrequency)
                {
                    TotalConfig_List.Add(item);
                }
            }

            if (SingletonInfo.GetInstance().IsGXProtocol)
            {
                if (_EBMConfigureGlobal.ListReback != null)
                {
                    foreach (var item in _EBMConfigureGlobal.ListReback)
                    {
                        TotalConfig_List.Add(item);
                    }
                }
            }
            else
            {
                if (_EBMConfigureGlobal.ListReback_Nation != null)
                {
                    foreach (var item in _EBMConfigureGlobal.ListReback_Nation)
                    {
                        TotalConfig_List.Add(item);
                    }
                }
            }
         

            if (_EBMConfigureGlobal.ListDefaltVolume!=null)
            {
                foreach (var item in _EBMConfigureGlobal.ListDefaltVolume)
                {
                    TotalConfig_List.Add(item);
                }
            }

            if (_EBMConfigureGlobal.ListRebackPeriod!=null)
            {
                foreach (var item in _EBMConfigureGlobal.ListRebackPeriod)
                {
                    TotalConfig_List.Add(item);
                }
            }

            if (_EBMConfigureGlobal.ListContentMoniterRetback!=null)
            {
                foreach (var item in _EBMConfigureGlobal.ListContentMoniterRetback)
                {
                    TotalConfig_List.Add(item);
                }
            }

            if (_EBMConfigureGlobal.ListContentRealMoniter!=null)
            {
                foreach (var item in _EBMConfigureGlobal.ListContentRealMoniter)
                {
                    TotalConfig_List.Add(item);
                }
            }

            if (_EBMConfigureGlobal.ListContentRealMoniterGX!=null)
            {
                foreach (var item in _EBMConfigureGlobal.ListContentRealMoniterGX)
                {
                    TotalConfig_List.Add(item);
                }
            
            }

            if (_EBMConfigureGlobal.ListStatusRetback!=null)
            {
                foreach (var item in _EBMConfigureGlobal.ListStatusRetback)
                {
                    TotalConfig_List.Add(item);
                }
            }

            if (_EBMConfigureGlobal.ListSoftwareUpGrade!=null)
            {
                foreach (var item in _EBMConfigureGlobal.ListSoftwareUpGrade)
                {
                    TotalConfig_List.Add(item);
                }
            }

            if (_EBMConfigureGlobal.ListRdsConfig!=null)
            {
                foreach (var item in _EBMConfigureGlobal.ListRdsConfig)
                {
                    TotalConfig_List.Add(item);
                }
            }
          
            return TotalConfig_List;
        }

        private BindingCollection<DailyProgram> GetDailyBroadcastCollection(DailyBroadcastGlobal_ _DailyBroadcastGlobal)
        {
            BindingCollection<DailyProgram> TotalConfig_List = new BindingCollection<DailyProgram>();

            if (_DailyBroadcastGlobal.ListChangeProgram != null)
            {
                foreach (var item in _DailyBroadcastGlobal.ListChangeProgram)
                {
                    TotalConfig_List.Add(item);
                }
            }


            if (_DailyBroadcastGlobal.ListStopPorgram != null)
            {
                foreach (var item in _DailyBroadcastGlobal.ListStopPorgram)
                {
                    TotalConfig_List.Add(item);
                }
            }

            if (_DailyBroadcastGlobal.ListPlayCtrl != null)
            {
                foreach (var item in _DailyBroadcastGlobal.ListPlayCtrl)
                {
                    TotalConfig_List.Add(item);
                }

            }


            if (_DailyBroadcastGlobal.ListOutSwitch!=null)
            {
                foreach (var item in _DailyBroadcastGlobal.ListOutSwitch)
                {
                    TotalConfig_List.Add(item);
                }
            }

            if (_DailyBroadcastGlobal.ListRdsTransfer!=null)
            {

                foreach (var item in _DailyBroadcastGlobal.ListRdsTransfer)
                {
                    TotalConfig_List.Add(item);
                }
            }
            return TotalConfig_List;
        }

        private List<DailyCmd> GetSendDailyCmd()
        {
            try
            {
                BindingCollection<DailyProgram> TotalConfig_List = GetDailyBroadcastCollection(SingletonInfo.GetInstance()._DailyBroadcastGlobal);
                List<DailyCmd> daily = new List<DailyCmd>();

                if (TotalConfig_List != null)
                {
                    foreach (var d in TotalConfig_List)
                    {
                        switch (d.B_Daily_cmd_tag)
                        {
                            case Utils.ComboBoxHelper.ChangeProgramTag:
                            
                                daily.Add((d as ChangeProgram_).Program.GetCmd());
                                break;

                            case Utils.ComboBoxHelper.StopProgramTag:

                                daily.Add((d as StopPorgram_).Program.GetCmd());
                                break;

                            case Utils.ComboBoxHelper.PlayCtrlTag:
                                daily.Add((d as PlayCtrl_).Program.GetCmd());
                                break;
                            case Utils.ComboBoxHelper.OutSwitchTag:
                                daily.Add((d as OutSwitch_).Program.GetCmd());
                                break;
                            case Utils.ComboBoxHelper.RdsTransferTag:
                                daily.Add((d as RdsTransfer_).Program.GetCmd());
                                break;
                        }
                    }
                }

                return daily;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        private List<ConfigureCmd> GetSendConfigureCmd()
        {
            try
            {
                BindingCollection<Configure> TotalConfig_List;
                TotalConfig_List = GetConfigureCollection(SingletonInfo.GetInstance()._EBMConfigureGlobal);
                List<ConfigureCmd> cmd = new List<ConfigureCmd>();
                foreach (var d in TotalConfig_List)
                {
                        switch (d.B_Daily_cmd_tag)
                        {
                            case Utils.ComboBoxHelper.ConfigureTimeServiceTag:
                                cmd.Add((d as TimeService_).Configure.GetCmd());
                                break;
                            case Utils.ComboBoxHelper.ConfigureSetAddressTag:
                                if (SingletonInfo.GetInstance().IsGXProtocol)
                                {

                                    cmd.Add((d as SetAddress_).Configure.GetCmdGX());
                                }
                                else
                                {
                                    cmd.Add((d as SetAddress_).Configure.GetCmd());
                                }
                                
                                break;
                            case Utils.ComboBoxHelper.ConfigureWorkModeTag:
                                if (SingletonInfo.GetInstance().IsGXProtocol)
                                {
                                    cmd.Add((d as WorkMode_).Configure.GetCmdGX());
                                }
                                else
                                {
                                    cmd.Add((d as WorkMode_).Configure.GetCmd());
                                }
                                
                                break;
                            case Utils.ComboBoxHelper.ConfigureMainFrequencyTag:
                                if(SingletonInfo.GetInstance().IsGXProtocol)
                                {
                                    cmd.Add((d as MainFrequency_).Configure.GetCmdGX());
                                }
                                else
                                {
                                    cmd.Add((d as MainFrequency_).Configure.GetCmd());
                                }
                              
                                break;
                            case Utils.ComboBoxHelper.ConfigureRebackTag:

                            #region 注意这里需要特殊处理  201901014
                            if (SingletonInfo.GetInstance().IsGXProtocol)
                            {
                                cmd.Add((d as Reback_).Configure.GetCmd());
                            }
                            else
                            {
                                cmd.Add((d as Reback_Nation).Configure.GetCmd());
                            }
                            #endregion
                          
                                break;
                            case Utils.ComboBoxHelper.ConfigureDefaltVolumeTag:

                                if(SingletonInfo.GetInstance().IsGXProtocol)
                                {
                                    cmd.Add((d as DefaltVolume_).Configure.GetCmdGX());
                                }
                                else
                                {
                                    cmd.Add((d as DefaltVolume_).Configure.GetCmd());
                                }
                               
                                break;
                            case Utils.ComboBoxHelper.ConfigureRebackPeriodTag:

                                if(SingletonInfo.GetInstance().IsGXProtocol)
                                {
                                 cmd.Add((d as RebackPeriod_).Configure.GetCmdGX());
                                }
                                else
                                {
                                 cmd.Add((d as RebackPeriod_).Configure.GetCmd());
                                }
                               
                                break;
                            case Utils.ComboBoxHelper.ConfigureContentMoniterRetbackTag:
                                cmd.Add((d as ContentMoniterRetback_).Configure.GetCmd());
                                break;
                            case Utils.ComboBoxHelper.ConfigureContentRealMoniterTag:
                            //if (SingletonInfo.GetInstance().IsGXProtocol)
                            //{
                            //    cmd.Add((d as ContentRealMoniterGX_).Configure.GetCmd());

                            //}
                            //else
                            //{
                            //    cmd.Add((d as ContentRealMoniter_).Configure.GetCmd());
                            //}

                            //国标版本采用广西协议
                            // cmd.Add((d as ContentRealMoniterGX_).Configure.GetCmd());
                            cmd.Add((d as ContentRealMoniterGX_).Configure.GetCmdTN());
                            break;
                            case Utils.ComboBoxHelper.ConfigureStatusRetbackTag:
                                cmd.Add((d as StatusRetback_).Configure.GetCmd());
                                break;
                            case Utils.ComboBoxHelper.ConfigureSoftwareUpGradeTag:
                                cmd.Add((d as SoftwareUpGrade_).Configure.GetCmd());
                                break;
                            case Utils.ComboBoxHelper.ConfigureRdsConfigTag:
                                cmd.Add((d as RdsConfig_).Configure.GetCmd());
                                break;
                        case Utils.ComboBoxHelper.ConfigureStatusRetbackNationTag:
                            cmd.Add((d as StatusRetback_).Configure.GetCmd());
                            break;
                    }
                    
                }
                return cmd;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        private List<ConfigureCmd> GetSendTimeSerConfigureCmd()
        {
            try
            {
                BindingCollection<Configure> TotalConfig_List = GetConfigureCollection(SingletonInfo.GetInstance()._EBMConfigureGlobal);
                List<ConfigureCmd> cmd = new List<ConfigureCmd>();
                foreach (var d in TotalConfig_List)
                {
                    //if (d.SendState && d.B_Daily_cmd_tag == Utils.ComboBoxHelper.ConfigureTimeServiceTag)
                    //{
                    //    cmd.Add((d as TimeService_).Configure.GetCmd());
                    //}
                }
                return cmd;
            }
            catch
            {
                return null;
            }
        }

        private void InitAllGlobalData()
        {
            var joCertAuth = TableData.TableDataHelper.ReadTable(Enums.TableType.CertAuth);//从配置文件中读取数据
            if (joCertAuth != null)
            {
                SingletonInfo.GetInstance()._CertAuthGlobal.list_Cert = JsonConvert.DeserializeObject<List<Cert_>>(joCertAuth["CertList"].ToString());
                SingletonInfo.GetInstance()._CertAuthGlobal.list_CertAuth = JsonConvert.DeserializeObject<List<CertAuth_>>(joCertAuth["CertAuthList"].ToString());
                SingletonInfo.GetInstance()._CertAuthGlobal.Repeat_times = Convert.ToInt32(joCertAuth["RepeatTimes"].ToString());
            }
            else
            {

                SingletonInfo.GetInstance()._CertAuthGlobal.list_Cert = new List<Cert_>();
                SingletonInfo.GetInstance()._CertAuthGlobal.list_CertAuth = new List<CertAuth_>();
                SingletonInfo.GetInstance()._CertAuthGlobal.Repeat_times = 3;
            }

            var joEBMIndex = TableData.TableDataHelper.ReadTable(Enums.TableType.Index);//从配置文件中读取数据
            if (joEBMIndex != null)
            {

                SingletonInfo.GetInstance()._EBMIndexGlobal.ListEbIndex = JsonConvert.DeserializeObject<List<EBMIndex_>>(joEBMIndex["0"].ToString());

                foreach (var l in SingletonInfo.GetInstance()._EBMIndexGlobal.ListEbIndex)
                {  //这是什么操作？
                    l.List_EBM_resource_code.RemoveRange(l.List_EBM_resource_code.Count / 2, l.List_EBM_resource_code.Count / 2);
                    l.List_ProgramStreamInfo.RemoveRange(l.List_ProgramStreamInfo.Count / 2, l.List_ProgramStreamInfo.Count / 2);
                }
                SingletonInfo.GetInstance()._EBMIndexGlobal.Repeat_times = 3;
            }
            else
            {
                SingletonInfo.GetInstance()._EBMIndexGlobal.ListEbIndex = new List<EBMIndex_>();
                SingletonInfo.GetInstance()._EBMIndexGlobal.Repeat_times = 3;
            }


            var joConfigure = TableData.TableDataHelper.ReadTable(Enums.TableType.Configure);//从配置文件中读取数据
            if (joConfigure != null)
            {
                SingletonInfo.GetInstance()._EBMConfigureGlobal.ListContentMoniterRetback = JsonConvert.DeserializeObject<List<ContentMoniterRetback_>>(joConfigure["0"].ToString());
                SingletonInfo.GetInstance()._EBMConfigureGlobal.ListContentRealMoniter = JsonConvert.DeserializeObject<List<ContentRealMoniter_>>(joConfigure["1"].ToString());
                SingletonInfo.GetInstance()._EBMConfigureGlobal.ListDefaltVolume = JsonConvert.DeserializeObject<List<DefaltVolume_>>(joConfigure["2"].ToString());
                SingletonInfo.GetInstance()._EBMConfigureGlobal.ListMainFrequency = JsonConvert.DeserializeObject<List<MainFrequency_>>(joConfigure["3"].ToString());
                SingletonInfo.GetInstance()._EBMConfigureGlobal.ListRdsConfig = JsonConvert.DeserializeObject<List<RdsConfig_>>(joConfigure["4"].ToString());
                if (SingletonInfo.GetInstance().IsGXProtocol)
                {
                    SingletonInfo.GetInstance()._EBMConfigureGlobal.ListReback = JsonConvert.DeserializeObject<List<Reback_>>(joConfigure["5"].ToString());
                }
                else
                {
                    SingletonInfo.GetInstance()._EBMConfigureGlobal.ListReback_Nation = JsonConvert.DeserializeObject<List<Reback_Nation>>(joConfigure["5"].ToString());
                }

                SingletonInfo.GetInstance()._EBMConfigureGlobal.ListRebackPeriod = JsonConvert.DeserializeObject<List<RebackPeriod_>>(joConfigure["6"].ToString());
                SingletonInfo.GetInstance()._EBMConfigureGlobal.ListSetAddress = JsonConvert.DeserializeObject<List<SetAddress_>>(joConfigure["7"].ToString());
                SingletonInfo.GetInstance()._EBMConfigureGlobal.ListSoftwareUpGrade = JsonConvert.DeserializeObject<List<SoftwareUpGrade_>>(joConfigure["8"].ToString());
                SingletonInfo.GetInstance()._EBMConfigureGlobal.ListStatusRetback = JsonConvert.DeserializeObject<List<StatusRetback_>>(joConfigure["9"].ToString());
                SingletonInfo.GetInstance()._EBMConfigureGlobal.ListTimeService = JsonConvert.DeserializeObject<List<TimeService_>>(joConfigure["10"].ToString());
                SingletonInfo.GetInstance()._EBMConfigureGlobal.ListWorkMode = JsonConvert.DeserializeObject<List<WorkMode_>>(joConfigure["11"].ToString());

                SingletonInfo.GetInstance()._EBMConfigureGlobal.Repeat_times = 3;
            }
            else
            {
                SingletonInfo.GetInstance()._EBMConfigureGlobal.ListContentMoniterRetback = new List<ContentMoniterRetback_>();
                SingletonInfo.GetInstance()._EBMConfigureGlobal.ListContentRealMoniter = new List<ContentRealMoniter_>();
                SingletonInfo.GetInstance()._EBMConfigureGlobal.ListDefaltVolume = new List<DefaltVolume_>();
                SingletonInfo.GetInstance()._EBMConfigureGlobal.ListMainFrequency = new List<MainFrequency_>();
                SingletonInfo.GetInstance()._EBMConfigureGlobal.ListRdsConfig = new List<RdsConfig_>();
                if (SingletonInfo.GetInstance().IsGXProtocol)
                {
                    SingletonInfo.GetInstance()._EBMConfigureGlobal.ListReback = new List<Reback_>();
                }
                else
                {
                    SingletonInfo.GetInstance()._EBMConfigureGlobal.ListReback_Nation = new List<Reback_Nation>();
                }

                SingletonInfo.GetInstance()._EBMConfigureGlobal.ListRebackPeriod = new List<RebackPeriod_>();
                SingletonInfo.GetInstance()._EBMConfigureGlobal.ListSetAddress = new List<SetAddress_>();
                SingletonInfo.GetInstance()._EBMConfigureGlobal.ListSoftwareUpGrade = new List<SoftwareUpGrade_>();
                SingletonInfo.GetInstance()._EBMConfigureGlobal.ListStatusRetback = new List<StatusRetback_>();
                SingletonInfo.GetInstance()._EBMConfigureGlobal.ListTimeService = new List<TimeService_>();
                SingletonInfo.GetInstance()._EBMConfigureGlobal.ListWorkMode = new List<WorkMode_>();
                SingletonInfo.GetInstance()._EBMIndexGlobal.Repeat_times = 3;
            }


            var joDailyBroadcast = TableData.TableDataHelper.ReadTable(Enums.TableType.DailyBroadcast);//从配置文件中读取数据
            if (joDailyBroadcast != null)
            {
                SingletonInfo.GetInstance()._DailyBroadcastGlobal.ListChangeProgram = JsonConvert.DeserializeObject<List<ChangeProgram_>>(joDailyBroadcast["0"].ToString());
                SingletonInfo.GetInstance()._DailyBroadcastGlobal.ListPlayCtrl = JsonConvert.DeserializeObject<List<PlayCtrl_>>(joDailyBroadcast["1"].ToString());

                SingletonInfo.GetInstance()._DailyBroadcastGlobal.ListOutSwitch = JsonConvert.DeserializeObject<List<OutSwitch_>>(joDailyBroadcast["2"].ToString());

                SingletonInfo.GetInstance()._DailyBroadcastGlobal.ListRdsTransfer = JsonConvert.DeserializeObject<List<RdsTransfer_>>(joDailyBroadcast["3"].ToString());

                SingletonInfo.GetInstance()._DailyBroadcastGlobal.Repeat_times = 3;
            }
            else
            {
                SingletonInfo.GetInstance()._DailyBroadcastGlobal.ListChangeProgram = new List<ChangeProgram_>();
                SingletonInfo.GetInstance()._DailyBroadcastGlobal.ListPlayCtrl = new List<PlayCtrl_>();
                SingletonInfo.GetInstance()._DailyBroadcastGlobal.ListOutSwitch = new List<OutSwitch_>();
                SingletonInfo.GetInstance()._DailyBroadcastGlobal.ListRdsTransfer = new List<RdsTransfer_>();

                SingletonInfo.GetInstance()._DailyBroadcastGlobal.Repeat_times = 3;
            }


            var joEBContent = TableData.TableDataHelper.ReadTable(Enums.TableType.Content);//从配置文件中读取数据
            if (joEBContent != null)
            {
                SingletonInfo.GetInstance()._EBContentGlobal.ListEBContent = JsonConvert.DeserializeObject<List<EBMID_Content>>(joDailyBroadcast["0"].ToString());
                SingletonInfo.GetInstance()._EBContentGlobal.Repeat_times = 3;
            }
            else
            {
                SingletonInfo.GetInstance()._EBContentGlobal.ListEBContent = new List<EBMID_Content>();
                SingletonInfo.GetInstance()._EBContentGlobal.Repeat_times = 3;
            }

        }

        /// <summary>
        /// 检查配置文件能否正常打开
        /// </summary>
        private bool CheckIniConfig()
        {
            try
            {
                string iniPath = Path.Combine(Application.StartupPath, "InstructionServer.ini");
                ini = new IniFiles(iniPath);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(typeof(EBMMain), "配置文件打开失败");
                return false;
            }
            return true;
        }

        public bool InitEBStream()
        {
            try
            {
               // JObject jo = TableData.TableDataHelper.ReadConfig();
               // if (jo != null)
               // {
                    EbmStream.ElementaryPid = Convert.ToInt32(ini.ReadValue("TSSendInfo", "ElementaryPid"));
                    EbmStream.Stream_id = Convert.ToInt32(ini.ReadValue("TSSendInfo", "Stream_id"));
                    EbmStream.Program_id = Convert.ToInt32(ini.ReadValue("TSSendInfo", "Program_id"));
                    EbmStream.PMT_Pid = Convert.ToInt32(ini.ReadValue("TSSendInfo", "PMT_Pid"));
                    EbmStream.Section_length = Convert.ToInt32(ini.ReadValue("TSSendInfo", "Section_length"));
                    EbmStream.sDestSockAddress = ini.ReadValue("TSSendInfo", "sDestSockAddress");
                    EbmStream.sLocalSockAddress = ini.ReadValue("TSSendInfo", "sLocalSockAddress");
                    EbmStream.Stream_BitRate = Convert.ToInt32(ini.ReadValue("TSSendInfo", "Stream_BitRate"));
               // }

               // InitStreamTable();
                InitStreamTableNew();//2
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        private void InitTable()
        {
            EB_Index_Table.Table_id = 0xfd;
            EB_Index_Table.Table_id_extension = 0;
            EB_Content_Table.Table_id = 0xfe;
            EB_Content_Table.Table_id_extension = 0;
            Daily_Broadcast_Table.Table_id = 0xfa;
            Daily_Broadcast_Table.Table_id_extension = 0;
            EB_CertAuth_Table.Table_id = 0xfc;
            EB_CertAuth_Table.Table_id_extension = 0;
            EB_Configure_Table.Table_id = 0xfb;
            EB_Configure_Table.Table_id_extension = 0;
            EbmStream.EB_Index_Table = EB_Index_Table;
            EbmStream.EB_CertAuth_Table = EB_CertAuth_Table;


            #region 启用 广西还是国标协议

            EbmStream.EB_Index_Table.ProtocolGX = SingletonInfo.GetInstance().IsGXProtocol;
            EbmStream.EB_CertAuth_Table.ProtocolGX = SingletonInfo.GetInstance().IsGXProtocol;
            #endregion 
        }

        public void InitStreamTableNew()
        {
            //设置需要发送的表
         
            
               GetEBIndexTable(ref EB_Index_Table);
                EbmStream.EB_Index_Table = EB_Index_Table;
          
                EbmStream.EB_CertAuth_Table =GetCertAuthTable(ref EB_CertAuth_Table) ? EB_CertAuth_Table : null;

                EbmStream.EB_Configure_Table = GetConfigureTable(ref EB_Configure_Table,false) ? EB_Configure_Table : null;

                EbmStream.Daily_Broadcast_Table = GetDailyBroadcastTable(ref Daily_Broadcast_Table) ? Daily_Broadcast_Table : null;

            if (SingletonInfo.GetInstance().IsUseCAInfo)
            {
                EbmStream.SignatureCallbackRef = new EBMStream.SignatureCallBackDelegateRef(calcel.SignatureFunc);//每次在 Initialization()之前调用
            }
            else
            {
                EbmStream.SignatureCallbackRef = null;
            }
            EbmStream.Initialization();
            isInitStream = true;


            //注释于20180531
            //if (thread != null)
            //{
            //    thread.Abort();
            //}
            //thread = new Thread(UpdateDataTextNew);
            //thread.IsBackground = true;
            //thread.Start();

        }

        public void NetErrorDeal()
        {
            this.Invoke(new Action(() => 
            {
                SingletonInfo.GetInstance().OpenScramblerReturn = 2;//表示打开密码器的预制状态
              //  MenuItemStopStream_Click(null, null);
                MessageBox.Show("网络异常,数据发送停止！");
            }));
        
        }

        /// <summary>
        /// 更新屏幕打印  
        /// </summary>
        /// <param name="tag"></param> 0表示打印所有功能 1表示打印Index 2表示打印CertAuth
        public void UpdateDataTextNew(object tag)
        {
            try
            {
                if (IsStartStream)
                {

                    int type = (int)tag;
                    switch (type)
                    {
                        case 0:
                            EB_IndexScreenPrint();
                            EB_CertAuthScreenPrint();
                            break;
                        case 1:
                            EB_IndexScreenPrint();//在服务器上运行导致奔溃
                            break;
                        case 2:
                            EB_CertAuthScreenPrint();
                            break;
                        case 3:
                            EB_ConfigureScreenPrint();
                            break;
                        case 4:
                            EB_DailyBroadcastScreenPrint();
                            break;
                        case 5:
                            EB_ContentScreenPrint();
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(typeof(EBMMain), ex.ToString());
            }
        }

        private void EB_IndexScreenPrint()
        {
            if (EbmStream.EB_Index_Table != null)
            {
                StringBuilder sb = new StringBuilder(DateTime.Now.ToString() + "\n");
                int num = 0;
                EbmStream.EB_Index_Table.BuildEbIndexSection();
                byte[] body=new byte[] { };
                do
                {
                    Thread.Sleep(800);
                    body = EbmStream.EB_Index_Table.GetEbIndexSection(ref num);
                }

                while (EbmStream.EB_Index_Table.Completed == false);

                if (body != null)
                {
                    for (int i = 0; i < body.Length; i++)
                    {
                        if (i != 0 && i % 16 == 0) sb.Append("\n");
                        sb.Append(body[i].ToString("X2").PadLeft(2, '0').ToUpper() + " ");
                    }
                    sb.Append("\n\n");
                }

                LogMessage(Color.Black, sb.ToString());
            }
        }

        private void EB_CertAuthScreenPrint()
        {
            try
            {
                if (EbmStream.EB_CertAuth_Table != null)
                {
                    StringBuilder sb = new StringBuilder(DateTime.Now.ToString() + "\n");
                    int num = 0;
                    EbmStream.EB_CertAuth_Table.BuildEBCertAuthSection();
                    //  var body = EbmStream.EB_CertAuth_Table.GetEBCertAuthSection(ref num);

                    byte[] body;
                    do
                    {
                        Thread.Sleep(500);
                        body = EbmStream.EB_CertAuth_Table.GetEBCertAuthSection(ref num);
                    }

                    while (EbmStream.EB_CertAuth_Table.Completed == false);


                    for (int i = 0; i < body.Length; i++)
                    {
                        if (i != 0 && i % 16 == 0) sb.Append("\n");
                        sb.Append(body[i].ToString("X2").PadLeft(2, '0').ToUpper() + " ");
                    }
                    sb.Append("\n\n");
                    LogMessage(Color.Red, sb.ToString());
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(typeof(EBMMain), ex.ToString());
            }
         
        }

        private void EB_ConfigureScreenPrint()
        {
            try
            {
                if (EbmStream.EB_Configure_Table != null)
                {
                    StringBuilder sb = new StringBuilder(DateTime.Now.ToString() + "\n");
                    int num = 0;
                    EbmStream.EB_Configure_Table.BuildEBConfigureSection();

                    byte[] body;
                    do
                    {
                        Thread.Sleep(500);
                        body = EbmStream.EB_Configure_Table.GetEBConfigureSection(ref num);
                    }

                    while (EbmStream.EB_Configure_Table.Completed == false);


                    for (int i = 0; i < body.Length; i++)
                    {
                        if (i != 0 && i % 16 == 0) sb.Append("\n");
                        sb.Append(body[i].ToString("X2").PadLeft(2, '0').ToUpper() + " ");
                    }
                    sb.Append("\n\n");
                    LogMessage(Color.DarkGreen, sb.ToString());
                }
            }
            catch (Exception ex)
            {

                LogHelper.WriteLog(typeof(EBMMain), ex.ToString());
            }
            
        }

        private void EB_DailyBroadcastScreenPrint()
        {
            try
            {
                if (EbmStream.Daily_Broadcast_Table != null)
                {
                    StringBuilder sb = new StringBuilder(DateTime.Now.ToString() + "\n");
                    int num = 0;
                    EbmStream.Daily_Broadcast_Table.BuildDailyBroadcastSection();

                    byte[] body;
                    do
                    {
                        Thread.Sleep(500);
                        body = EbmStream.Daily_Broadcast_Table.GetDailyBroadcastSection(ref num);
                    }

                    while (EbmStream.Daily_Broadcast_Table.Completed == false);


                    for (int i = 0; i < body.Length; i++)
                    {
                        if (i != 0 && i % 16 == 0) sb.Append("\n");
                        sb.Append(body[i].ToString("X2").PadLeft(2, '0').ToUpper() + " ");
                    }
                    sb.Append("\n\n");
                    LogMessage(Color.DarkOrange, sb.ToString());
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(typeof(EBMMain), ex.ToString());
            }
        }


        private void EB_ContentScreenPrint()
        {
            try
            {
                if (EbmStream.list_EB_Content_Table != null)
                {

                    foreach (var item in EbmStream.list_EB_Content_Table)
                    {
                        StringBuilder sb = new StringBuilder(DateTime.Now.ToString() + "\n");
                        int num = 0;
                        item.BuildEBContentSection();

                        byte[] body;
                        do
                        {
                            Thread.Sleep(500);
                            body = item.GetEBContentSection(ref num);
                        }

                        while (item.Completed == false);

                        if (body != null)
                        {
                            for (int i = 0; i < body.Length; i++)
                            {
                                if (i != 0 && i % 16 == 0) sb.Append("\n");
                                sb.Append(body[i].ToString("X2").PadLeft(2, '0').ToUpper() + " ");
                            }
                            sb.Append("\n\n");
                        }

                        LogMessage(Color.DarkBlue, sb.ToString());
                    }

                }
            }
            catch (Exception ex)
            {

                LogHelper.WriteLog(typeof(EBMMain), ex.ToString());
            }
           

        }

        #region MenuItem
        /// <summary>
        /// 启动发送
        /// </summary>
        private void ProcessBegin()
        {
            try
            {
                InitEBStream();
                if (EbmStream != null && isInitStream && !IsStartStream)
                {
                    //发送数据
                    EbmStream.StartStreaming();  
                    IsStartStream = true;
                    //if (formDailyBroadcast != null && !formDailyBroadcast.IsDisposed)
                    //{
                    //    formDailyBroadcast.InitColumnStop(true);
                    //}
                    //Thread thread = new Thread(UpdateDataTextNew);
                    //thread.IsBackground = true;
                    //thread.Start(); //测试注释  20180621
                }

                SingletonInfo.GetInstance().IsStartSend = true;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(typeof(EBMMain), "启动发送失败："+ex.ToString());
            }
        
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
        #endregion

        private void EBMMain_FormClosing(object sender, FormClosingEventArgs e)
        {
          
            if (EbmStream != null && IsStartStream)
            {
                EbmStream.StopStreaming();
                // CloseScrambler();

                if (!SingletonInfo.GetInstance().SignatureType)
                {
                    int nDeviceHandle = (int) SingletonInfo.GetInstance().phDeviceHandle;
                    int nReturn =SingletonInfo.GetInstance().usb.USB_CloseDevice(ref nDeviceHandle);
                }
                
                IsStartStream = false;
                SaveTmpData();
            }
            ini.WriteValue("EBMInfo", "SignCounter", SingletonInfo.GetInstance().InlayCA.SignCounter.ToString());
            ini.WriteValue("EBM", "ebm_id_behind", SingletonInfo.GetInstance().ebm_id_behind);
            ini.WriteValue("EBM", "ebm_id_count", SingletonInfo.GetInstance().ebm_id_count.ToString());
           
        }

        private void SaveTmpData()
        {
            TableData.TableDataHelper.WriteTable(Enums.TableType.CertAuth, SingletonInfo.GetInstance()._CertAuthGlobal.list_Cert , SingletonInfo.GetInstance()._CertAuthGlobal.list_CertAuth );
            TableData.TableDataHelper.WriteTable(Enums.TableType.Index, SingletonInfo.GetInstance()._EBMIndexGlobal.ListEbIndex);
            TableData.TableDataHelper.WriteTable(Enums.TableType.Configure,
               SingletonInfo.GetInstance()._EBMConfigureGlobal.ListTimeService, SingletonInfo.GetInstance()._EBMConfigureGlobal.ListSetAddress, SingletonInfo.GetInstance()._EBMConfigureGlobal.ListWorkMode, SingletonInfo.GetInstance()._EBMConfigureGlobal.ListMainFrequency, SingletonInfo.GetInstance()._EBMConfigureGlobal.ListReback , SingletonInfo.GetInstance()._EBMConfigureGlobal.ListDefaltVolume ,
             SingletonInfo.GetInstance()._EBMConfigureGlobal.ListRebackPeriod, SingletonInfo.GetInstance()._EBMConfigureGlobal.ListContentMoniterRetback, SingletonInfo.GetInstance()._EBMConfigureGlobal.ListContentRealMoniter, SingletonInfo.GetInstance()._EBMConfigureGlobal .ListStatusRetback , SingletonInfo.GetInstance()._EBMConfigureGlobal.ListSoftwareUpGrade ,
              SingletonInfo.GetInstance()._EBMConfigureGlobal.ListRdsConfig);


            TableData.TableDataHelper.WriteTable(Enums.TableType.Content, SingletonInfo.GetInstance()._EBContentGlobal.ListEBContent);

            TableData.TableDataHelper.WriteTable(Enums.TableType.DailyBroadcast, SingletonInfo.GetInstance()._DailyBroadcastGlobal.ListChangeProgram, SingletonInfo.GetInstance()._DailyBroadcastGlobal.ListPlayCtrl, SingletonInfo.GetInstance()._DailyBroadcastGlobal.ListOutSwitch, SingletonInfo.GetInstance()._DailyBroadcastGlobal.ListRdsTransfer);

        }


        private OnorOFFResponse SwitchChannel(int channelID,string ResourceCode,IPEndPoint ie)
        {
            OnorOFFBroadcast tt = new OnorOFFBroadcast();
            tt.ebm_class = "4";
            tt.ebm_id = BBSHelper.CreateEBM_ID();
            tt.ebm_level = "2";
            tt.ebm_type = "00000";
            tt.end_time = DateTime.Now.AddHours(5).ToString("yyyy-MM-dd HH:mm:ss");
            tt.start_time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            tt.power_switch = "3";// 1开播  2停播   3切换通道
            tt.volume = "80";
            tt.resource_code_type = "1";
            tt.resource_codeList = new List<string>();
            tt.resource_codeList.Add(ResourceCode);
            tt.input_channel_id = channelID;
          
            OnorOFFResponse resopnse = (OnorOFFResponse)SingletonInfo.GetInstance().tcpsend.SendTCPCommnand(tt, 0x04, ie);
            return resopnse;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string auth = "00 00 00 00 28 39 00 01 00 00 36 01 00 02 00 00 00 00 00 01 42 20 5a 22 7e 39 a0 1d 35 18 61 b4 a7 86 48 8b ee 55 37 c2 59 8c 21 d9 20 5a 28 10 24 f3 af 8b 3c 2a e2 5b 8b 91 aa d3 3b 1d c0 09 c6 0a d6 56 59 cf 20 ee 7f 42 3e 15 fd 46 59 a9 62 07 d9 d4";
            string cert = "10 00 00 00 00 00 01 00 00 36 01 00 02 48 09 04 A9 FC 3D A9 55 6A 93 1F E0 EA 73 07 36 82 38 0C A2 87 34 8A 1A 47 DE 55 93 57 99 E8 78 32 87 DC 5C 1B 0D 52 51 61 73 DC 85 AC 05 05 D4 B7 4D 9B C3 C1 07 CD B9 29 E3 73 1B 22 A8 43 8E 19 17 D6 2E 59 7E EB C5 DC B9 B0 B9 95 59 DD E5 E8 47 4A 22 31 E2 E1 38 2F B4 ED 95 CC 97 4F FA 85 E2 45 B7 FD FA CE B4 D0 01 4A 3B 23 EF F7 8C 3A 17 62 8B 3B D0 59 9C CE 07 FA 54 99 7B D7 81 D4 78";

            List<CertAuthTmp> AddCertAuthList = new List<CertAuthTmp>();
            List<CertTmp> AddCertList = new List<CertTmp>();

            CertAuthTmp pp = new CertAuthTmp();
            pp.CertAuthDataid = "1";
            pp.CertAuthDataHexStr = auth;
            pp.isSend = "1";
            AddCertAuthList.Add(pp);

            DealCertAuth2Global(AddCertAuthList);



            CertTmp pp1 = new CertTmp();
            pp1.CertDataid = "1";
            pp1.CertDataHexStr = cert;
            pp1.isSend = "1";
            AddCertList.Add(pp1);

            DealCert2Global(AddCertList);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string auth = "00 00 00 00 02 00 ff 01 00 00 36 01 00 02 00 00 00 00 00 01 04 08 3b 11 68 89 d9 48 d3 a1 23 da e6 d7 00 8f 9f c2 c5 cd 1d af a4 48 be 67 41 27 85 0b 2b 83 29 65 84 c7 db 03 b7 0c 24 c4 18 ed db 0a 65 ac eb ae c9 29 29 88 98 ea 74 b4 68 8e 3e b6 ab e6";
            string cert = "10 00 00 00 00 00 01 00 00 36 01 00 02 48 09 04 A9 FC 3D A9 55 6A 93 1F E0 EA 73 07 36 82 38 0C A2 87 34 8A 1A 47 DE 55 93 57 99 E8 78 32 87 DC 5C 1B 0D 52 51 61 73 DC 85 AC 05 05 D4 B7 4D 9B C3 C1 07 CD B9 29 E3 73 1B 22 A8 43 8E 19 17 D6 2E 59 7E EB C5 DC B9 B0 B9 95 59 DD E5 E8 47 4A 22 31 E2 E1 38 2F B4 ED 95 CC 97 4F FA 85 E2 45 B7 FD FA CE B4 D0 01 4A 3B 23 EF F7 8C 3A 17 62 8B 3B D0 59 9C CE 07 FA 54 99 7B D7 81 D4 78";

            List<CertAuthTmp> AddCertAuthList = new List<CertAuthTmp>();
            List<CertTmp> AddCertList = new List<CertTmp>();


            CertTmp pp1 = new CertTmp();
            pp1.CertDataid = "1";
            pp1.CertDataHexStr = cert;
            pp1.isSend = "1";
            AddCertList.Add(pp1);

            DealCert2Global(AddCertList);


            CertAuthTmp pp = new CertAuthTmp();
            pp.CertAuthDataid = "1";
            pp.CertAuthDataHexStr = auth;
            pp.isSend = "1";
            AddCertAuthList.Add(pp);

            DealCertAuth2Global(AddCertAuthList);



            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string auth = "00 00 00 00 15 55 01 01 00 00 36 01 00 02 00 00 00 00 00 01 61 e1 b2 88 a1 15 a5 28 de c0 fb 6f 53 97 f7 fd 41 f1 30 e2 5c e1 7d e4 9e a1 69 5f a0 bb 58 7a 45 0d f1 ba 1f bd 78 3d 36 15 b1 21 8d c4 7b e3 bf e9 3b da d3 23 59 f0 51 b3 99 ad 07 df a1 5e";
            string cert = "10 00 00 00 00 00 01 00 00 36 01 00 02 48 09 04 A9 FC 3D A9 55 6A 93 1F E0 EA 73 07 36 82 38 0C A2 87 34 8A 1A 47 DE 55 93 57 99 E8 78 32 87 DC 5C 1B 0D 52 51 61 73 DC 85 AC 05 05 D4 B7 4D 9B C3 C1 07 CD B9 29 E3 73 1B 22 A8 43 8E 19 17 D6 2E 59 7E EB C5 DC B9 B0 B9 95 59 DD E5 E8 47 4A 22 31 E2 E1 38 2F B4 ED 95 CC 97 4F FA 85 E2 45 B7 FD FA CE B4 D0 01 4A 3B 23 EF F7 8C 3A 17 62 8B 3B D0 59 9C CE 07 FA 54 99 7B D7 81 D4 78";

            List<CertAuthTmp> AddCertAuthList = new List<CertAuthTmp>();
            List<CertTmp> AddCertList = new List<CertTmp>();

            CertAuthTmp pp = new CertAuthTmp();
            pp.CertAuthDataid = "1";
            pp.CertAuthDataHexStr = auth;
            pp.isSend = "1";
            AddCertAuthList.Add(pp);

            DealCertAuth2Global(AddCertAuthList);



            CertTmp pp1 = new CertTmp();
            pp1.CertDataid = "1";
            pp1.CertDataHexStr = cert;
            pp1.isSend = "1";
            AddCertList.Add(pp1);

            DealCert2Global(AddCertList);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                string auth = "00 00 00 00 5e 6a 00 05 00 00 36 01 00 02 00 00 00 00 5e 6b 00 00 00 00 5e 6a 00 00 00 00 5e 69 00 00 00 00 5e 68 00 00 00 00 00 01 2c e2 fe 14 64 49 d1 83 70 49 bb de 10 dd a8 1d 12 99 f3 d4 30 79 ac 16 8a 99 5a 47 91 a2 1a 23 16 6a 46 e3 77 09 8a 23 8f fb 7b de c8 00 fc ef b7 4f 8e 06 88 27 34 d2 4b 04 a8 05 47 0c b7 78";


                string cert1 = "10 00 00 00 00 00 01 00 00 36 01 00 02 48 09 04 A9 FC 3D A9 55 6A 93 1F E0 EA 73 07 36 82 38 0C A2 87 34 8A 1A 47 DE 55 93 57 99 E8 78 32 87 DC 5C 1B 0D 52 51 61 73 DC 85 AC 05 05 D4 B7 4D 9B C3 C1 07 CD B9 29 E3 73 1B 22 A8 43 8E 19 17 D6 2E 59 7E EB C5 DC B9 B0 B9 95 59 DD E5 E8 47 4A 22 31 E2 E1 38 2F B4 ED 95 CC 97 4F FA 85 E2 45 B7 FD FA CE B4 D0 01 4A 3B 23 EF F7 8C 3A 17 62 8B 3B D0 59 9C CE 07 FA 54 99 7B D7 81 D4 78";

                string cert2 = "10 00 00 00 00 00 01 00 00 00 00 5E 6B 48 09 58 01 42 56 5B 1F 12 62 82 B7 6C A5 24 E8 75 7B 7C 2E 36 2A 90 BF 5A 9B 74 FC 06 2F 8C 7E A8 9E 66 24 3F 0F AA 57 C0 37 81 BE 32 44 03 05 78 CF D4 DC 94 C1 B2 F5 5D 58 66 04 81 9D B8 67 B2 87 42 2D E7 61 73 76 68 1F 65 C2 D5 37 83 23 C9 88 96 8A 77 9A AC D3 4C ED 73 61 04 B7 22 E9 FB 7A 1E 6F 1A 81 F1 7E 66 2C D5 C6 35 85 C2 3D 26 84 CA AF FC B7 94 4D AA 68 AA C3 B8 E5 13 59 AA 06";

                string cert3 = "10 00 00 00 00 00 01 00 00 00 00 5E 6A 48 09 56 BC 7F 0C BF 98 35 0A 2F DC F8 6C EC 24 F9 F2 07 70 72 A0 1A 0C 2C D7 67 75 2D D3 C0 78 83 0F FF 28 63 58 86 EC 04 59 04 38 2E 23 E1 D8 FF 1A 41 F6 89 93 D0 9D 18 1B 15 B4 F7 C0 C9 CE 3F EC F2 58 F5 CE 9D 83 DC 38 34 B0 92 25 51 8C E4 14 23 AD C6 B7 D2 7E E2 F9 B0 7C 52 BF 86 E2 9E E3 73 5A 8D 8B 1F CD C1 09 DA 6B 9C 0F 66 4F 24 44 6C FF 02 B5 98 9F B2 A7 46 20 A4 A7 98 82 F9 2C";

                string cert4 = "10 00 00 00 00 00 01 00 00 00 00 5E 69 48 09 33 E8 54 F9 16 29 8A 59 15 4E 78 9F A1 0C 38 7E CC 3C 02 B1 88 E3 74 A5 B8 AD A9 2B DE DC 18 51 16 61 05 9B AA E7 B5 EE 14 CC AD 0A CF 56 82 74 11 A2 C3 B8 0E FD 81 57 5F BF 4F 19 AD 60 7D 66 28 F8 21 07 A0 FA 4D 69 33 21 22 90 FC E1 E6 76 45 DA 1F FC ED 91 33 3F 05 32 BD 66 21 E1 4E 5B E2 9A 40 A6 3E CD D2 B5 1D 7F 26 36 18 33 3B 58 C2 F3 00 25 4B 2D F9 A6 ED 81 03 CF 5D 08 15 20";


                string cert5 = "10 00 00 00 00 00 01 00 00 00 00 5E 68 48 09 8F 11 B0 85 80 1F 16 D1 C8 52 35 A1 B4 5E 14 C9 B2 33 40 E5 4E 4E 28 F0 F7 7C 30 E9 07 52 32 74 54 99 E2 30 26 88 14 3E 5F A2 9A 7C D8 4D 03 54 C8 F6 F7 05 67 9D 9E 0F AE AF B1 05 9E 64 73 23 5D E1 2F F8 2B 9A 43 A9 63 98 CB 6E 15 64 64 0C 26 F2 02 CF 4E F2 D0 6F 0E 29 AF 99 57 7E 7E 43 C6 DE F9 FE 52 7E B4 D3 04 49 B8 78 62 BE 7B 6F 73 5B 3A 15 3F 51 1D 6F 67 7A 48 7B 31 68 18 2E";
                List<CertAuthTmp> AddCertAuthList = new List<CertAuthTmp>();
                List<CertTmp> AddCertList = new List<CertTmp>();


                CertTmp pp1 = new CertTmp();
                pp1.CertDataid = "1";
                pp1.CertDataHexStr = cert1;
                pp1.isSend = "1";
                AddCertList.Add(pp1);

                CertTmp pp2 = new CertTmp();
                pp2.CertDataid = "2";
                pp2.CertDataHexStr = cert2;
                pp2.isSend = "1";
                AddCertList.Add(pp2);


                CertTmp pp3 = new CertTmp();
                pp3.CertDataid = "3";
                pp3.CertDataHexStr = cert3;
                pp3.isSend = "1";
                AddCertList.Add(pp3);


                CertTmp pp4 = new CertTmp();
                pp4.CertDataid = "4";
                pp4.CertDataHexStr = cert4;
                pp4.isSend = "1";
                AddCertList.Add(pp4);

                CertTmp pp5 = new CertTmp();
                pp5.CertDataid = "5";
                pp5.CertDataHexStr = cert5;
                pp5.isSend = "1";
                AddCertList.Add(pp5);

                DealCert2Global(AddCertList);


                CertAuthTmp pp = new CertAuthTmp();
                pp.CertAuthDataid = "5";
                pp.CertAuthDataHexStr = auth;
                pp.isSend = "1";
                AddCertAuthList.Add(pp);

                DealCertAuth2Global(AddCertAuthList);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string pp = "fe fd 01 00 00 00 00 07 01 01 00 8b f4 36 07 81 00 00 00 00 00 00 00 00 00 01 f0 00 00 00 00 00 00 00 00 00 00 00 02 00 12 f4 36 07 81 00 00 00 00 00 00 00 00 20 19 04 17 00 84";
            List<byte> input = new List<byte>();
            string[] pps = pp.Split(' ');
            foreach (string item in pps)
            {
                input.Add((byte)Convert.ToInt32(item,16));
            }

            byte[] signa = new byte[74];
            int random = 9;
            calcel.SignatureFunc(input.ToArray(), input.Count,ref random,ref signa);

            string rr = "";
            foreach (byte item in signa)
            {
                rr += item.ToString("X2") + " ";
            }
            string rrq = "1";
        }

        private void button17_Click(object sender, EventArgs e)
        {
            try
            {
                string auth = "00 00 00 00 00 c8 ff 05 00 00 36 01 00 02 00 00 00 00 5e 6b 00 00 00 00 5e 6a 00 00 00 00 5e 69 00 00 00 00 5e 68 00 00 00 00 00 01 4f ab 80 66 eb 54 c4 4e 52 da 2c 8a f6 45 d3 0b 35 37 a4 d1 3c 20 df 8d 13 fc 55 0f e7 e7 57 06 8e 99 6f b7 b7 e5 df 98 bc a1 80 a6 0d 66 0c a8 08 52 6f 13 48 a9 52 c4 e3 5a 25 a5 fe c9 89 7f";


                string cert1 = "10 00 00 00 00 00 01 00 00 36 01 00 02 48 09 04 A9 FC 3D A9 55 6A 93 1F E0 EA 73 07 36 82 38 0C A2 87 34 8A 1A 47 DE 55 93 57 99 E8 78 32 87 DC 5C 1B 0D 52 51 61 73 DC 85 AC 05 05 D4 B7 4D 9B C3 C1 07 CD B9 29 E3 73 1B 22 A8 43 8E 19 17 D6 2E 59 7E EB C5 DC B9 B0 B9 95 59 DD E5 E8 47 4A 22 31 E2 E1 38 2F B4 ED 95 CC 97 4F FA 85 E2 45 B7 FD FA CE B4 D0 01 4A 3B 23 EF F7 8C 3A 17 62 8B 3B D0 59 9C CE 07 FA 54 99 7B D7 81 D4 78";

                string cert2 = "10 00 00 00 00 00 01 00 00 00 00 5E 6B 48 09 58 01 42 56 5B 1F 12 62 82 B7 6C A5 24 E8 75 7B 7C 2E 36 2A 90 BF 5A 9B 74 FC 06 2F 8C 7E A8 9E 66 24 3F 0F AA 57 C0 37 81 BE 32 44 03 05 78 CF D4 DC 94 C1 B2 F5 5D 58 66 04 81 9D B8 67 B2 87 42 2D E7 61 73 76 68 1F 65 C2 D5 37 83 23 C9 88 96 8A 77 9A AC D3 4C ED 73 61 04 B7 22 E9 FB 7A 1E 6F 1A 81 F1 7E 66 2C D5 C6 35 85 C2 3D 26 84 CA AF FC B7 94 4D AA 68 AA C3 B8 E5 13 59 AA 06";

                string cert3 = "10 00 00 00 00 00 01 00 00 00 00 5E 6A 48 09 56 BC 7F 0C BF 98 35 0A 2F DC F8 6C EC 24 F9 F2 07 70 72 A0 1A 0C 2C D7 67 75 2D D3 C0 78 83 0F FF 28 63 58 86 EC 04 59 04 38 2E 23 E1 D8 FF 1A 41 F6 89 93 D0 9D 18 1B 15 B4 F7 C0 C9 CE 3F EC F2 58 F5 CE 9D 83 DC 38 34 B0 92 25 51 8C E4 14 23 AD C6 B7 D2 7E E2 F9 B0 7C 52 BF 86 E2 9E E3 73 5A 8D 8B 1F CD C1 09 DA 6B 9C 0F 66 4F 24 44 6C FF 02 B5 98 9F B2 A7 46 20 A4 A7 98 82 F9 2C";

                string cert4 = "10 00 00 00 00 00 01 00 00 00 00 5E 69 48 09 33 E8 54 F9 16 29 8A 59 15 4E 78 9F A1 0C 38 7E CC 3C 02 B1 88 E3 74 A5 B8 AD A9 2B DE DC 18 51 16 61 05 9B AA E7 B5 EE 14 CC AD 0A CF 56 82 74 11 A2 C3 B8 0E FD 81 57 5F BF 4F 19 AD 60 7D 66 28 F8 21 07 A0 FA 4D 69 33 21 22 90 FC E1 E6 76 45 DA 1F FC ED 91 33 3F 05 32 BD 66 21 E1 4E 5B E2 9A 40 A6 3E CD D2 B5 1D 7F 26 36 18 33 3B 58 C2 F3 00 25 4B 2D F9 A6 ED 81 03 CF 5D 08 15 20";


                string cert5 = "10 00 00 00 00 00 01 00 00 00 00 5E 68 48 09 8F 11 B0 85 80 1F 16 D1 C8 52 35 A1 B4 5E 14 C9 B2 33 40 E5 4E 4E 28 F0 F7 7C 30 E9 07 52 32 74 54 99 E2 30 26 88 14 3E 5F A2 9A 7C D8 4D 03 54 C8 F6 F7 05 67 9D 9E 0F AE AF B1 05 9E 64 73 23 5D E1 2F F8 2B 9A 43 A9 63 98 CB 6E 15 64 64 0C 26 F2 02 CF 4E F2 D0 6F 0E 29 AF 99 57 7E 7E 43 C6 DE F9 FE 52 7E B4 D3 04 49 B8 78 62 BE 7B 6F 73 5B 3A 15 3F 51 1D 6F 67 7A 48 7B 31 68 18 2E";
                List<CertAuthTmp> AddCertAuthList = new List<CertAuthTmp>();
                List<CertTmp> AddCertList = new List<CertTmp>();


                CertTmp pp1 = new CertTmp();
                pp1.CertDataid = "1";
                pp1.CertDataHexStr = cert1;
                pp1.isSend = "1";
                AddCertList.Add(pp1);

                CertTmp pp2 = new CertTmp();
                pp2.CertDataid = "2";
                pp2.CertDataHexStr = cert2;
                pp2.isSend = "1";
                AddCertList.Add(pp2);


                CertTmp pp3 = new CertTmp();
                pp3.CertDataid = "3";
                pp3.CertDataHexStr = cert3;
                pp3.isSend = "1";
                AddCertList.Add(pp3);


                CertTmp pp4 = new CertTmp();
                pp4.CertDataid = "4";
                pp4.CertDataHexStr = cert4;
                pp4.isSend = "1";
                AddCertList.Add(pp4);

                CertTmp pp5 = new CertTmp();
                pp5.CertDataid = "5";
                pp5.CertDataHexStr = cert5;
                pp5.isSend = "1";
                AddCertList.Add(pp5);

                DealCert2Global(AddCertList);


                CertAuthTmp pp = new CertAuthTmp();
                pp.CertAuthDataid = "1";
                pp.CertAuthDataHexStr = auth;
                pp.isSend = "1";
                AddCertAuthList.Add(pp);

                DealCertAuth2Global(AddCertAuthList);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            try
            {
                string auth = "00 00 00 00 13 0d 00 05 00 00 36 01 00 02 00 00 00 00 5e 6b 00 00 00 00 5e 6a 00 00 00 00 5e 69 00 00 00 00 5e 68 00 00 00 00 00 01 0b 95 9f 97 95 29 fa ed 27 93 31 ac 13 09 4b 80 49 8e 66 5f f9 03 b2 ae a9 42 5c ea 5a 28 4f 4e af 20 78 29 30 fd 85 f3 32 e5 97 b9 56 b0 37 a8 e9 cb 68 99 ad b0 ce 96 3f d5 41 59 04 0f d6 88";


                string cert1 = "10 00 00 00 00 00 01 00 00 36 01 00 02 48 09 04 A9 FC 3D A9 55 6A 93 1F E0 EA 73 07 36 82 38 0C A2 87 34 8A 1A 47 DE 55 93 57 99 E8 78 32 87 DC 5C 1B 0D 52 51 61 73 DC 85 AC 05 05 D4 B7 4D 9B C3 C1 07 CD B9 29 E3 73 1B 22 A8 43 8E 19 17 D6 2E 59 7E EB C5 DC B9 B0 B9 95 59 DD E5 E8 47 4A 22 31 E2 E1 38 2F B4 ED 95 CC 97 4F FA 85 E2 45 B7 FD FA CE B4 D0 01 4A 3B 23 EF F7 8C 3A 17 62 8B 3B D0 59 9C CE 07 FA 54 99 7B D7 81 D4 78";

                string cert2 = "10 00 00 00 00 00 01 00 00 00 00 5E 6B 48 09 58 01 42 56 5B 1F 12 62 82 B7 6C A5 24 E8 75 7B 7C 2E 36 2A 90 BF 5A 9B 74 FC 06 2F 8C 7E A8 9E 66 24 3F 0F AA 57 C0 37 81 BE 32 44 03 05 78 CF D4 DC 94 C1 B2 F5 5D 58 66 04 81 9D B8 67 B2 87 42 2D E7 61 73 76 68 1F 65 C2 D5 37 83 23 C9 88 96 8A 77 9A AC D3 4C ED 73 61 04 B7 22 E9 FB 7A 1E 6F 1A 81 F1 7E 66 2C D5 C6 35 85 C2 3D 26 84 CA AF FC B7 94 4D AA 68 AA C3 B8 E5 13 59 AA 06";

                string cert3 = "10 00 00 00 00 00 01 00 00 00 00 5E 6A 48 09 56 BC 7F 0C BF 98 35 0A 2F DC F8 6C EC 24 F9 F2 07 70 72 A0 1A 0C 2C D7 67 75 2D D3 C0 78 83 0F FF 28 63 58 86 EC 04 59 04 38 2E 23 E1 D8 FF 1A 41 F6 89 93 D0 9D 18 1B 15 B4 F7 C0 C9 CE 3F EC F2 58 F5 CE 9D 83 DC 38 34 B0 92 25 51 8C E4 14 23 AD C6 B7 D2 7E E2 F9 B0 7C 52 BF 86 E2 9E E3 73 5A 8D 8B 1F CD C1 09 DA 6B 9C 0F 66 4F 24 44 6C FF 02 B5 98 9F B2 A7 46 20 A4 A7 98 82 F9 2C";

                string cert4 = "10 00 00 00 00 00 01 00 00 00 00 5E 69 48 09 33 E8 54 F9 16 29 8A 59 15 4E 78 9F A1 0C 38 7E CC 3C 02 B1 88 E3 74 A5 B8 AD A9 2B DE DC 18 51 16 61 05 9B AA E7 B5 EE 14 CC AD 0A CF 56 82 74 11 A2 C3 B8 0E FD 81 57 5F BF 4F 19 AD 60 7D 66 28 F8 21 07 A0 FA 4D 69 33 21 22 90 FC E1 E6 76 45 DA 1F FC ED 91 33 3F 05 32 BD 66 21 E1 4E 5B E2 9A 40 A6 3E CD D2 B5 1D 7F 26 36 18 33 3B 58 C2 F3 00 25 4B 2D F9 A6 ED 81 03 CF 5D 08 15 20";


                string cert5 = "10 00 00 00 00 00 01 00 00 00 00 5E 68 48 09 8F 11 B0 85 80 1F 16 D1 C8 52 35 A1 B4 5E 14 C9 B2 33 40 E5 4E 4E 28 F0 F7 7C 30 E9 07 52 32 74 54 99 E2 30 26 88 14 3E 5F A2 9A 7C D8 4D 03 54 C8 F6 F7 05 67 9D 9E 0F AE AF B1 05 9E 64 73 23 5D E1 2F F8 2B 9A 43 A9 63 98 CB 6E 15 64 64 0C 26 F2 02 CF 4E F2 D0 6F 0E 29 AF 99 57 7E 7E 43 C6 DE F9 FE 52 7E B4 D3 04 49 B8 78 62 BE 7B 6F 73 5B 3A 15 3F 51 1D 6F 67 7A 48 7B 31 68 18 2E";
                List<CertAuthTmp> AddCertAuthList = new List<CertAuthTmp>();
                List<CertTmp> AddCertList = new List<CertTmp>();


                CertTmp pp1 = new CertTmp();
                pp1.CertDataid = "1";
                pp1.CertDataHexStr = cert1;
                pp1.isSend = "1";
                AddCertList.Add(pp1);

                CertTmp pp2 = new CertTmp();
                pp2.CertDataid = "2";
                pp2.CertDataHexStr = cert2;
                pp2.isSend = "1";
                AddCertList.Add(pp2);


                CertTmp pp3 = new CertTmp();
                pp3.CertDataid = "3";
                pp3.CertDataHexStr = cert3;
                pp3.isSend = "1";
                AddCertList.Add(pp3);


                CertTmp pp4 = new CertTmp();
                pp4.CertDataid = "4";
                pp4.CertDataHexStr = cert4;
                pp4.isSend = "1";
                AddCertList.Add(pp4);

                CertTmp pp5 = new CertTmp();
                pp5.CertDataid = "5";
                pp5.CertDataHexStr = cert5;
                pp5.isSend = "1";
                AddCertList.Add(pp5);

                DealCert2Global(AddCertList);


                CertAuthTmp pp = new CertAuthTmp();
                pp.CertAuthDataid = "5";
                pp.CertAuthDataHexStr = auth;
                pp.isSend = "1";
                AddCertAuthList.Add(pp);

                DealCertAuth2Global(AddCertAuthList);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                string auth = "00 00 00 00 13 0c 00 05 00 00 36 01 00 02 00 00 00 00 5e 6b 00 00 00 00 5e 6a 00 00 00 00 5e 69 00 00 00 00 5e 68 00 00 00 00 00 01 37 b4 43 62 58 93 8f 09 57 97 8c c9 7f cc a4 ea 22 11 f7 12 7d d6 9e c7 43 3c fb 93 68 ae 32 89 1e 3d 50 76 93 42 c1 22 48 36 b7 68 32 16 6c 04 13 66 ce 34 59 c9 2b c6 91 a7 e7 c3 9c b1 07 69";


                string cert1 = "10 00 00 00 00 00 01 00 00 36 01 00 02 48 09 04 A9 FC 3D A9 55 6A 93 1F E0 EA 73 07 36 82 38 0C A2 87 34 8A 1A 47 DE 55 93 57 99 E8 78 32 87 DC 5C 1B 0D 52 51 61 73 DC 85 AC 05 05 D4 B7 4D 9B C3 C1 07 CD B9 29 E3 73 1B 22 A8 43 8E 19 17 D6 2E 59 7E EB C5 DC B9 B0 B9 95 59 DD E5 E8 47 4A 22 31 E2 E1 38 2F B4 ED 95 CC 97 4F FA 85 E2 45 B7 FD FA CE B4 D0 01 4A 3B 23 EF F7 8C 3A 17 62 8B 3B D0 59 9C CE 07 FA 54 99 7B D7 81 D4 78";

                string cert2 = "10 00 00 00 00 00 01 00 00 00 00 5E 6B 48 09 58 01 42 56 5B 1F 12 62 82 B7 6C A5 24 E8 75 7B 7C 2E 36 2A 90 BF 5A 9B 74 FC 06 2F 8C 7E A8 9E 66 24 3F 0F AA 57 C0 37 81 BE 32 44 03 05 78 CF D4 DC 94 C1 B2 F5 5D 58 66 04 81 9D B8 67 B2 87 42 2D E7 61 73 76 68 1F 65 C2 D5 37 83 23 C9 88 96 8A 77 9A AC D3 4C ED 73 61 04 B7 22 E9 FB 7A 1E 6F 1A 81 F1 7E 66 2C D5 C6 35 85 C2 3D 26 84 CA AF FC B7 94 4D AA 68 AA C3 B8 E5 13 59 AA 06";

                string cert3 = "10 00 00 00 00 00 01 00 00 00 00 5E 6A 48 09 56 BC 7F 0C BF 98 35 0A 2F DC F8 6C EC 24 F9 F2 07 70 72 A0 1A 0C 2C D7 67 75 2D D3 C0 78 83 0F FF 28 63 58 86 EC 04 59 04 38 2E 23 E1 D8 FF 1A 41 F6 89 93 D0 9D 18 1B 15 B4 F7 C0 C9 CE 3F EC F2 58 F5 CE 9D 83 DC 38 34 B0 92 25 51 8C E4 14 23 AD C6 B7 D2 7E E2 F9 B0 7C 52 BF 86 E2 9E E3 73 5A 8D 8B 1F CD C1 09 DA 6B 9C 0F 66 4F 24 44 6C FF 02 B5 98 9F B2 A7 46 20 A4 A7 98 82 F9 2C";

                string cert4 = "10 00 00 00 00 00 01 00 00 00 00 5E 69 48 09 33 E8 54 F9 16 29 8A 59 15 4E 78 9F A1 0C 38 7E CC 3C 02 B1 88 E3 74 A5 B8 AD A9 2B DE DC 18 51 16 61 05 9B AA E7 B5 EE 14 CC AD 0A CF 56 82 74 11 A2 C3 B8 0E FD 81 57 5F BF 4F 19 AD 60 7D 66 28 F8 21 07 A0 FA 4D 69 33 21 22 90 FC E1 E6 76 45 DA 1F FC ED 91 33 3F 05 32 BD 66 21 E1 4E 5B E2 9A 40 A6 3E CD D2 B5 1D 7F 26 36 18 33 3B 58 C2 F3 00 25 4B 2D F9 A6 ED 81 03 CF 5D 08 15 20";


                string cert5 = "10 00 00 00 00 00 01 00 00 00 00 5E 68 48 09 8F 11 B0 85 80 1F 16 D1 C8 52 35 A1 B4 5E 14 C9 B2 33 40 E5 4E 4E 28 F0 F7 7C 30 E9 07 52 32 74 54 99 E2 30 26 88 14 3E 5F A2 9A 7C D8 4D 03 54 C8 F6 F7 05 67 9D 9E 0F AE AF B1 05 9E 64 73 23 5D E1 2F F8 2B 9A 43 A9 63 98 CB 6E 15 64 64 0C 26 F2 02 CF 4E F2 D0 6F 0E 29 AF 99 57 7E 7E 43 C6 DE F9 FE 52 7E B4 D3 04 49 B8 78 62 BE 7B 6F 73 5B 3A 15 3F 51 1D 6F 67 7A 48 7B 31 68 18 2E";
                List<CertAuthTmp> AddCertAuthList = new List<CertAuthTmp>();
                List<CertTmp> AddCertList = new List<CertTmp>();


                CertTmp pp1 = new CertTmp();
                pp1.CertDataid = "1";
                pp1.CertDataHexStr = cert1;
                pp1.isSend = "1";
                AddCertList.Add(pp1);

                CertTmp pp2 = new CertTmp();
                pp2.CertDataid = "2";
                pp2.CertDataHexStr = cert2;
                pp2.isSend = "1";
                AddCertList.Add(pp2);


                CertTmp pp3 = new CertTmp();
                pp3.CertDataid = "3";
                pp3.CertDataHexStr = cert3;
                pp3.isSend = "1";
                AddCertList.Add(pp3);


                CertTmp pp4 = new CertTmp();
                pp4.CertDataid = "4";
                pp4.CertDataHexStr = cert4;
                pp4.isSend = "1";
                AddCertList.Add(pp4);

                CertTmp pp5 = new CertTmp();
                pp5.CertDataid = "5";
                pp5.CertDataHexStr = cert5;
                pp5.isSend = "1";
                AddCertList.Add(pp5);

                DealCert2Global(AddCertList);


                CertAuthTmp pp = new CertAuthTmp();
                pp.CertAuthDataid = "5";
                pp.CertAuthDataHexStr = auth;
                pp.isSend = "1";
                AddCertAuthList.Add(pp);

                DealCertAuth2Global(AddCertAuthList);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                string auth = "00 00 00 00 13 0b 00 05 00 00 36 01 00 02 00 00 00 00 5e 6b 00 00 00 00 5e 6a 00 00 00 00 5e 69 00 00 00 00 5e 68 00 00 00 00 00 01 fb 70 18 ab 58 82 f8 c6 e9 09 d0 ca b2 f9 db a6 6c 41 0e b4 bd 34 c5 70 69 6a 86 24 21 8b de cb c6 be f5 67 79 5e 3e e7 a8 b9 11 cb e3 b2 15 ef 8e e7 86 9f 59 43 17 1e 9e 2a a8 00 7d ac ad e6";


                string cert1 = "10 00 00 00 00 00 01 00 00 36 01 00 02 48 09 04 A9 FC 3D A9 55 6A 93 1F E0 EA 73 07 36 82 38 0C A2 87 34 8A 1A 47 DE 55 93 57 99 E8 78 32 87 DC 5C 1B 0D 52 51 61 73 DC 85 AC 05 05 D4 B7 4D 9B C3 C1 07 CD B9 29 E3 73 1B 22 A8 43 8E 19 17 D6 2E 59 7E EB C5 DC B9 B0 B9 95 59 DD E5 E8 47 4A 22 31 E2 E1 38 2F B4 ED 95 CC 97 4F FA 85 E2 45 B7 FD FA CE B4 D0 01 4A 3B 23 EF F7 8C 3A 17 62 8B 3B D0 59 9C CE 07 FA 54 99 7B D7 81 D4 78";

                string cert2 = "10 00 00 00 00 00 01 00 00 00 00 5E 6B 48 09 58 01 42 56 5B 1F 12 62 82 B7 6C A5 24 E8 75 7B 7C 2E 36 2A 90 BF 5A 9B 74 FC 06 2F 8C 7E A8 9E 66 24 3F 0F AA 57 C0 37 81 BE 32 44 03 05 78 CF D4 DC 94 C1 B2 F5 5D 58 66 04 81 9D B8 67 B2 87 42 2D E7 61 73 76 68 1F 65 C2 D5 37 83 23 C9 88 96 8A 77 9A AC D3 4C ED 73 61 04 B7 22 E9 FB 7A 1E 6F 1A 81 F1 7E 66 2C D5 C6 35 85 C2 3D 26 84 CA AF FC B7 94 4D AA 68 AA C3 B8 E5 13 59 AA 06";

                string cert3 = "10 00 00 00 00 00 01 00 00 00 00 5E 6A 48 09 56 BC 7F 0C BF 98 35 0A 2F DC F8 6C EC 24 F9 F2 07 70 72 A0 1A 0C 2C D7 67 75 2D D3 C0 78 83 0F FF 28 63 58 86 EC 04 59 04 38 2E 23 E1 D8 FF 1A 41 F6 89 93 D0 9D 18 1B 15 B4 F7 C0 C9 CE 3F EC F2 58 F5 CE 9D 83 DC 38 34 B0 92 25 51 8C E4 14 23 AD C6 B7 D2 7E E2 F9 B0 7C 52 BF 86 E2 9E E3 73 5A 8D 8B 1F CD C1 09 DA 6B 9C 0F 66 4F 24 44 6C FF 02 B5 98 9F B2 A7 46 20 A4 A7 98 82 F9 2C";

                string cert4 = "10 00 00 00 00 00 01 00 00 00 00 5E 69 48 09 33 E8 54 F9 16 29 8A 59 15 4E 78 9F A1 0C 38 7E CC 3C 02 B1 88 E3 74 A5 B8 AD A9 2B DE DC 18 51 16 61 05 9B AA E7 B5 EE 14 CC AD 0A CF 56 82 74 11 A2 C3 B8 0E FD 81 57 5F BF 4F 19 AD 60 7D 66 28 F8 21 07 A0 FA 4D 69 33 21 22 90 FC E1 E6 76 45 DA 1F FC ED 91 33 3F 05 32 BD 66 21 E1 4E 5B E2 9A 40 A6 3E CD D2 B5 1D 7F 26 36 18 33 3B 58 C2 F3 00 25 4B 2D F9 A6 ED 81 03 CF 5D 08 15 20";


                string cert5 = "10 00 00 00 00 00 01 00 00 00 00 5E 68 48 09 8F 11 B0 85 80 1F 16 D1 C8 52 35 A1 B4 5E 14 C9 B2 33 40 E5 4E 4E 28 F0 F7 7C 30 E9 07 52 32 74 54 99 E2 30 26 88 14 3E 5F A2 9A 7C D8 4D 03 54 C8 F6 F7 05 67 9D 9E 0F AE AF B1 05 9E 64 73 23 5D E1 2F F8 2B 9A 43 A9 63 98 CB 6E 15 64 64 0C 26 F2 02 CF 4E F2 D0 6F 0E 29 AF 99 57 7E 7E 43 C6 DE F9 FE 52 7E B4 D3 04 49 B8 78 62 BE 7B 6F 73 5B 3A 15 3F 51 1D 6F 67 7A 48 7B 31 68 18 2E";
                List<CertAuthTmp> AddCertAuthList = new List<CertAuthTmp>();
                List<CertTmp> AddCertList = new List<CertTmp>();


                CertTmp pp1 = new CertTmp();
                pp1.CertDataid = "1";
                pp1.CertDataHexStr = cert1;
                pp1.isSend = "1";
                AddCertList.Add(pp1);

                CertTmp pp2 = new CertTmp();
                pp2.CertDataid = "2";
                pp2.CertDataHexStr = cert2;
                pp2.isSend = "1";
                AddCertList.Add(pp2);


                CertTmp pp3 = new CertTmp();
                pp3.CertDataid = "3";
                pp3.CertDataHexStr = cert3;
                pp3.isSend = "1";
                AddCertList.Add(pp3);


                CertTmp pp4 = new CertTmp();
                pp4.CertDataid = "4";
                pp4.CertDataHexStr = cert4;
                pp4.isSend = "1";
                AddCertList.Add(pp4);

                CertTmp pp5 = new CertTmp();
                pp5.CertDataid = "5";
                pp5.CertDataHexStr = cert5;
                pp5.isSend = "1";
                AddCertList.Add(pp5);

                DealCert2Global(AddCertList);


                CertAuthTmp pp = new CertAuthTmp();
                pp.CertAuthDataid = "5";
                pp.CertAuthDataHexStr = auth;
                pp.isSend = "1";
                AddCertAuthList.Add(pp);

                DealCertAuth2Global(AddCertAuthList);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                string auth = "00 00 00 00 13 0a 00 05 00 00 36 01 00 02 00 00 00 00 5e 6b 00 00 00 00 5e 6a 00 00 00 00 5e 69 00 00 00 00 5e 68 00 00 00 00 00 01 5a 3f 4e 25 83 13 1d 26 ef a5 f5 4e 5e 9c 26 a0 33 53 40 f8 05 15 8b f7 2d e2 a7 92 81 00 30 02 2d 89 89 7a af 61 e5 f2 c2 11 ae 9f 45 dc 32 92 f7 69 cf 8f 23 18 ce b2 93 36 1a 0b c4 09 a3 c0";


                string cert1 = "10 00 00 00 00 00 01 00 00 36 01 00 02 48 09 04 A9 FC 3D A9 55 6A 93 1F E0 EA 73 07 36 82 38 0C A2 87 34 8A 1A 47 DE 55 93 57 99 E8 78 32 87 DC 5C 1B 0D 52 51 61 73 DC 85 AC 05 05 D4 B7 4D 9B C3 C1 07 CD B9 29 E3 73 1B 22 A8 43 8E 19 17 D6 2E 59 7E EB C5 DC B9 B0 B9 95 59 DD E5 E8 47 4A 22 31 E2 E1 38 2F B4 ED 95 CC 97 4F FA 85 E2 45 B7 FD FA CE B4 D0 01 4A 3B 23 EF F7 8C 3A 17 62 8B 3B D0 59 9C CE 07 FA 54 99 7B D7 81 D4 78";

                string cert2 = "10 00 00 00 00 00 01 00 00 00 00 5E 6B 48 09 58 01 42 56 5B 1F 12 62 82 B7 6C A5 24 E8 75 7B 7C 2E 36 2A 90 BF 5A 9B 74 FC 06 2F 8C 7E A8 9E 66 24 3F 0F AA 57 C0 37 81 BE 32 44 03 05 78 CF D4 DC 94 C1 B2 F5 5D 58 66 04 81 9D B8 67 B2 87 42 2D E7 61 73 76 68 1F 65 C2 D5 37 83 23 C9 88 96 8A 77 9A AC D3 4C ED 73 61 04 B7 22 E9 FB 7A 1E 6F 1A 81 F1 7E 66 2C D5 C6 35 85 C2 3D 26 84 CA AF FC B7 94 4D AA 68 AA C3 B8 E5 13 59 AA 06";

                string cert3 = "10 00 00 00 00 00 01 00 00 00 00 5E 6A 48 09 56 BC 7F 0C BF 98 35 0A 2F DC F8 6C EC 24 F9 F2 07 70 72 A0 1A 0C 2C D7 67 75 2D D3 C0 78 83 0F FF 28 63 58 86 EC 04 59 04 38 2E 23 E1 D8 FF 1A 41 F6 89 93 D0 9D 18 1B 15 B4 F7 C0 C9 CE 3F EC F2 58 F5 CE 9D 83 DC 38 34 B0 92 25 51 8C E4 14 23 AD C6 B7 D2 7E E2 F9 B0 7C 52 BF 86 E2 9E E3 73 5A 8D 8B 1F CD C1 09 DA 6B 9C 0F 66 4F 24 44 6C FF 02 B5 98 9F B2 A7 46 20 A4 A7 98 82 F9 2C";

                string cert4 = "10 00 00 00 00 00 01 00 00 00 00 5E 69 48 09 33 E8 54 F9 16 29 8A 59 15 4E 78 9F A1 0C 38 7E CC 3C 02 B1 88 E3 74 A5 B8 AD A9 2B DE DC 18 51 16 61 05 9B AA E7 B5 EE 14 CC AD 0A CF 56 82 74 11 A2 C3 B8 0E FD 81 57 5F BF 4F 19 AD 60 7D 66 28 F8 21 07 A0 FA 4D 69 33 21 22 90 FC E1 E6 76 45 DA 1F FC ED 91 33 3F 05 32 BD 66 21 E1 4E 5B E2 9A 40 A6 3E CD D2 B5 1D 7F 26 36 18 33 3B 58 C2 F3 00 25 4B 2D F9 A6 ED 81 03 CF 5D 08 15 20";


                string cert5 = "10 00 00 00 00 00 01 00 00 00 00 5E 68 48 09 8F 11 B0 85 80 1F 16 D1 C8 52 35 A1 B4 5E 14 C9 B2 33 40 E5 4E 4E 28 F0 F7 7C 30 E9 07 52 32 74 54 99 E2 30 26 88 14 3E 5F A2 9A 7C D8 4D 03 54 C8 F6 F7 05 67 9D 9E 0F AE AF B1 05 9E 64 73 23 5D E1 2F F8 2B 9A 43 A9 63 98 CB 6E 15 64 64 0C 26 F2 02 CF 4E F2 D0 6F 0E 29 AF 99 57 7E 7E 43 C6 DE F9 FE 52 7E B4 D3 04 49 B8 78 62 BE 7B 6F 73 5B 3A 15 3F 51 1D 6F 67 7A 48 7B 31 68 18 2E";
                List<CertAuthTmp> AddCertAuthList = new List<CertAuthTmp>();
                List<CertTmp> AddCertList = new List<CertTmp>();


                CertTmp pp1 = new CertTmp();
                pp1.CertDataid = "1";
                pp1.CertDataHexStr = cert1;
                pp1.isSend = "1";
                AddCertList.Add(pp1);

                CertTmp pp2 = new CertTmp();
                pp2.CertDataid = "2";
                pp2.CertDataHexStr = cert2;
                pp2.isSend = "1";
                AddCertList.Add(pp2);


                CertTmp pp3 = new CertTmp();
                pp3.CertDataid = "3";
                pp3.CertDataHexStr = cert3;
                pp3.isSend = "1";
                AddCertList.Add(pp3);


                CertTmp pp4 = new CertTmp();
                pp4.CertDataid = "4";
                pp4.CertDataHexStr = cert4;
                pp4.isSend = "1";
                AddCertList.Add(pp4);

                CertTmp pp5 = new CertTmp();
                pp5.CertDataid = "5";
                pp5.CertDataHexStr = cert5;
                pp5.isSend = "1";
                AddCertList.Add(pp5);

                DealCert2Global(AddCertList);


                CertAuthTmp pp = new CertAuthTmp();
                pp.CertAuthDataid = "5";
                pp.CertAuthDataHexStr = auth;
                pp.isSend = "1";
                AddCertAuthList.Add(pp);

                DealCertAuth2Global(AddCertAuthList);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                string auth = "00 00 00 00 5e 62 00 05 00 00 36 01 00 02 00 00 00 00 5e 6b 00 00 00 00 5e 6a 00 00 00 00 5e 69 00 00 00 00 5e 68 00 00 00 00 00 01 be 8f 80 f4 48 5d bb 77 5e 03 f4 59 15 94 34 c5 a5 02 98 c7 19 72 71 e3 cf 54 d4 c2 78 eb 12 82 3d a8 43 69 1a 9b 00 03 01 75 b2 5a 98 30 07 cb 48 fa 07 60 f3 c8 70 57 f4 7c 12 de 35 57 44 ed";


                string cert1 = "10 00 00 00 00 00 01 00 00 36 01 00 02 48 09 04 A9 FC 3D A9 55 6A 93 1F E0 EA 73 07 36 82 38 0C A2 87 34 8A 1A 47 DE 55 93 57 99 E8 78 32 87 DC 5C 1B 0D 52 51 61 73 DC 85 AC 05 05 D4 B7 4D 9B C3 C1 07 CD B9 29 E3 73 1B 22 A8 43 8E 19 17 D6 2E 59 7E EB C5 DC B9 B0 B9 95 59 DD E5 E8 47 4A 22 31 E2 E1 38 2F B4 ED 95 CC 97 4F FA 85 E2 45 B7 FD FA CE B4 D0 01 4A 3B 23 EF F7 8C 3A 17 62 8B 3B D0 59 9C CE 07 FA 54 99 7B D7 81 D4 78";

                string cert2 = "10 00 00 00 00 00 01 00 00 00 00 5E 6B 48 09 58 01 42 56 5B 1F 12 62 82 B7 6C A5 24 E8 75 7B 7C 2E 36 2A 90 BF 5A 9B 74 FC 06 2F 8C 7E A8 9E 66 24 3F 0F AA 57 C0 37 81 BE 32 44 03 05 78 CF D4 DC 94 C1 B2 F5 5D 58 66 04 81 9D B8 67 B2 87 42 2D E7 61 73 76 68 1F 65 C2 D5 37 83 23 C9 88 96 8A 77 9A AC D3 4C ED 73 61 04 B7 22 E9 FB 7A 1E 6F 1A 81 F1 7E 66 2C D5 C6 35 85 C2 3D 26 84 CA AF FC B7 94 4D AA 68 AA C3 B8 E5 13 59 AA 06";

                string cert3 = "10 00 00 00 00 00 01 00 00 00 00 5E 6A 48 09 56 BC 7F 0C BF 98 35 0A 2F DC F8 6C EC 24 F9 F2 07 70 72 A0 1A 0C 2C D7 67 75 2D D3 C0 78 83 0F FF 28 63 58 86 EC 04 59 04 38 2E 23 E1 D8 FF 1A 41 F6 89 93 D0 9D 18 1B 15 B4 F7 C0 C9 CE 3F EC F2 58 F5 CE 9D 83 DC 38 34 B0 92 25 51 8C E4 14 23 AD C6 B7 D2 7E E2 F9 B0 7C 52 BF 86 E2 9E E3 73 5A 8D 8B 1F CD C1 09 DA 6B 9C 0F 66 4F 24 44 6C FF 02 B5 98 9F B2 A7 46 20 A4 A7 98 82 F9 2C";

                string cert4 = "10 00 00 00 00 00 01 00 00 00 00 5E 69 48 09 33 E8 54 F9 16 29 8A 59 15 4E 78 9F A1 0C 38 7E CC 3C 02 B1 88 E3 74 A5 B8 AD A9 2B DE DC 18 51 16 61 05 9B AA E7 B5 EE 14 CC AD 0A CF 56 82 74 11 A2 C3 B8 0E FD 81 57 5F BF 4F 19 AD 60 7D 66 28 F8 21 07 A0 FA 4D 69 33 21 22 90 FC E1 E6 76 45 DA 1F FC ED 91 33 3F 05 32 BD 66 21 E1 4E 5B E2 9A 40 A6 3E CD D2 B5 1D 7F 26 36 18 33 3B 58 C2 F3 00 25 4B 2D F9 A6 ED 81 03 CF 5D 08 15 20";


                string cert5 = "10 00 00 00 00 00 01 00 00 00 00 5E 68 48 09 8F 11 B0 85 80 1F 16 D1 C8 52 35 A1 B4 5E 14 C9 B2 33 40 E5 4E 4E 28 F0 F7 7C 30 E9 07 52 32 74 54 99 E2 30 26 88 14 3E 5F A2 9A 7C D8 4D 03 54 C8 F6 F7 05 67 9D 9E 0F AE AF B1 05 9E 64 73 23 5D E1 2F F8 2B 9A 43 A9 63 98 CB 6E 15 64 64 0C 26 F2 02 CF 4E F2 D0 6F 0E 29 AF 99 57 7E 7E 43 C6 DE F9 FE 52 7E B4 D3 04 49 B8 78 62 BE 7B 6F 73 5B 3A 15 3F 51 1D 6F 67 7A 48 7B 31 68 18 2E";
                List<CertAuthTmp> AddCertAuthList = new List<CertAuthTmp>();
                List<CertTmp> AddCertList = new List<CertTmp>();


                CertTmp pp1 = new CertTmp();
                pp1.CertDataid = "1";
                pp1.CertDataHexStr = cert1;
                pp1.isSend = "1";
                AddCertList.Add(pp1);

                CertTmp pp2 = new CertTmp();
                pp2.CertDataid = "2";
                pp2.CertDataHexStr = cert2;
                pp2.isSend = "1";
                AddCertList.Add(pp2);


                CertTmp pp3 = new CertTmp();
                pp3.CertDataid = "3";
                pp3.CertDataHexStr = cert3;
                pp3.isSend = "1";
                AddCertList.Add(pp3);


                CertTmp pp4 = new CertTmp();
                pp4.CertDataid = "4";
                pp4.CertDataHexStr = cert4;
                pp4.isSend = "1";
                AddCertList.Add(pp4);

                CertTmp pp5 = new CertTmp();
                pp5.CertDataid = "5";
                pp5.CertDataHexStr = cert5;
                pp5.isSend = "1";
                AddCertList.Add(pp5);

                DealCert2Global(AddCertList);


                CertAuthTmp pp = new CertAuthTmp();
                pp.CertAuthDataid = "5";
                pp.CertAuthDataHexStr = auth;
                pp.isSend = "1";
                AddCertAuthList.Add(pp);

                DealCertAuth2Global(AddCertAuthList);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                string auth = "00 00 00 00 5e 63 00 05 00 00 36 01 00 02 00 00 00 00 5e 6b 00 00 00 00 5e 6a 00 00 00 00 5e 69 00 00 00 00 5e 68 00 00 00 00 00 01 60 cf 2c fd 21 c7 62 69 13 b1 8d fb 78 94 9d f0 51 9b 34 97 70 5d 7f d8 da 62 1f 07 27 b8 49 82 7d 2b e5 64 a7 af 75 8c 51 36 cc cf 4d db 21 4c 82 2c 90 e0 fa bd 0e 53 40 84 ad 5e 14 cf 81 55";


                string cert1 = "10 00 00 00 00 00 01 00 00 36 01 00 02 48 09 04 A9 FC 3D A9 55 6A 93 1F E0 EA 73 07 36 82 38 0C A2 87 34 8A 1A 47 DE 55 93 57 99 E8 78 32 87 DC 5C 1B 0D 52 51 61 73 DC 85 AC 05 05 D4 B7 4D 9B C3 C1 07 CD B9 29 E3 73 1B 22 A8 43 8E 19 17 D6 2E 59 7E EB C5 DC B9 B0 B9 95 59 DD E5 E8 47 4A 22 31 E2 E1 38 2F B4 ED 95 CC 97 4F FA 85 E2 45 B7 FD FA CE B4 D0 01 4A 3B 23 EF F7 8C 3A 17 62 8B 3B D0 59 9C CE 07 FA 54 99 7B D7 81 D4 78";

                string cert2 = "10 00 00 00 00 00 01 00 00 00 00 5E 6B 48 09 58 01 42 56 5B 1F 12 62 82 B7 6C A5 24 E8 75 7B 7C 2E 36 2A 90 BF 5A 9B 74 FC 06 2F 8C 7E A8 9E 66 24 3F 0F AA 57 C0 37 81 BE 32 44 03 05 78 CF D4 DC 94 C1 B2 F5 5D 58 66 04 81 9D B8 67 B2 87 42 2D E7 61 73 76 68 1F 65 C2 D5 37 83 23 C9 88 96 8A 77 9A AC D3 4C ED 73 61 04 B7 22 E9 FB 7A 1E 6F 1A 81 F1 7E 66 2C D5 C6 35 85 C2 3D 26 84 CA AF FC B7 94 4D AA 68 AA C3 B8 E5 13 59 AA 06";

                string cert3 = "10 00 00 00 00 00 01 00 00 00 00 5E 6A 48 09 56 BC 7F 0C BF 98 35 0A 2F DC F8 6C EC 24 F9 F2 07 70 72 A0 1A 0C 2C D7 67 75 2D D3 C0 78 83 0F FF 28 63 58 86 EC 04 59 04 38 2E 23 E1 D8 FF 1A 41 F6 89 93 D0 9D 18 1B 15 B4 F7 C0 C9 CE 3F EC F2 58 F5 CE 9D 83 DC 38 34 B0 92 25 51 8C E4 14 23 AD C6 B7 D2 7E E2 F9 B0 7C 52 BF 86 E2 9E E3 73 5A 8D 8B 1F CD C1 09 DA 6B 9C 0F 66 4F 24 44 6C FF 02 B5 98 9F B2 A7 46 20 A4 A7 98 82 F9 2C";

                string cert4 = "10 00 00 00 00 00 01 00 00 00 00 5E 69 48 09 33 E8 54 F9 16 29 8A 59 15 4E 78 9F A1 0C 38 7E CC 3C 02 B1 88 E3 74 A5 B8 AD A9 2B DE DC 18 51 16 61 05 9B AA E7 B5 EE 14 CC AD 0A CF 56 82 74 11 A2 C3 B8 0E FD 81 57 5F BF 4F 19 AD 60 7D 66 28 F8 21 07 A0 FA 4D 69 33 21 22 90 FC E1 E6 76 45 DA 1F FC ED 91 33 3F 05 32 BD 66 21 E1 4E 5B E2 9A 40 A6 3E CD D2 B5 1D 7F 26 36 18 33 3B 58 C2 F3 00 25 4B 2D F9 A6 ED 81 03 CF 5D 08 15 20";


                string cert5 = "10 00 00 00 00 00 01 00 00 00 00 5E 68 48 09 8F 11 B0 85 80 1F 16 D1 C8 52 35 A1 B4 5E 14 C9 B2 33 40 E5 4E 4E 28 F0 F7 7C 30 E9 07 52 32 74 54 99 E2 30 26 88 14 3E 5F A2 9A 7C D8 4D 03 54 C8 F6 F7 05 67 9D 9E 0F AE AF B1 05 9E 64 73 23 5D E1 2F F8 2B 9A 43 A9 63 98 CB 6E 15 64 64 0C 26 F2 02 CF 4E F2 D0 6F 0E 29 AF 99 57 7E 7E 43 C6 DE F9 FE 52 7E B4 D3 04 49 B8 78 62 BE 7B 6F 73 5B 3A 15 3F 51 1D 6F 67 7A 48 7B 31 68 18 2E";
                List<CertAuthTmp> AddCertAuthList = new List<CertAuthTmp>();
                List<CertTmp> AddCertList = new List<CertTmp>();


                CertTmp pp1 = new CertTmp();
                pp1.CertDataid = "1";
                pp1.CertDataHexStr = cert1;
                pp1.isSend = "1";
                AddCertList.Add(pp1);

                CertTmp pp2 = new CertTmp();
                pp2.CertDataid = "2";
                pp2.CertDataHexStr = cert2;
                pp2.isSend = "1";
                AddCertList.Add(pp2);


                CertTmp pp3 = new CertTmp();
                pp3.CertDataid = "3";
                pp3.CertDataHexStr = cert3;
                pp3.isSend = "1";
                AddCertList.Add(pp3);


                CertTmp pp4 = new CertTmp();
                pp4.CertDataid = "4";
                pp4.CertDataHexStr = cert4;
                pp4.isSend = "1";
                AddCertList.Add(pp4);

                CertTmp pp5 = new CertTmp();
                pp5.CertDataid = "5";
                pp5.CertDataHexStr = cert5;
                pp5.isSend = "1";
                AddCertList.Add(pp5);

                DealCert2Global(AddCertList);


                CertAuthTmp pp = new CertAuthTmp();
                pp.CertAuthDataid = "5";
                pp.CertAuthDataHexStr = auth;
                pp.isSend = "1";
                AddCertAuthList.Add(pp);

                DealCertAuth2Global(AddCertAuthList);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
                string auth = "00 00 00 00 5e 64 00 05 00 00 36 01 00 02 00 00 00 00 5e 6b 00 00 00 00 5e 6a 00 00 00 00 5e 69 00 00 00 00 5e 68 00 00 00 00 00 01 94 ac 50 71 05 b3 a5 cb cb b9 28 28 cb b3 18 77 c9 4d 8a 35 8d 24 50 c5 6a 42 79 2c ed 38 da e6 24 a1 66 2e 29 f4 ab 90 76 aa 5b 26 bf c9 b4 66 17 5a 33 ff 4e 81 94 85 e9 fc 08 66 38 94 7f f7";


                string cert1 = "10 00 00 00 00 00 01 00 00 36 01 00 02 48 09 04 A9 FC 3D A9 55 6A 93 1F E0 EA 73 07 36 82 38 0C A2 87 34 8A 1A 47 DE 55 93 57 99 E8 78 32 87 DC 5C 1B 0D 52 51 61 73 DC 85 AC 05 05 D4 B7 4D 9B C3 C1 07 CD B9 29 E3 73 1B 22 A8 43 8E 19 17 D6 2E 59 7E EB C5 DC B9 B0 B9 95 59 DD E5 E8 47 4A 22 31 E2 E1 38 2F B4 ED 95 CC 97 4F FA 85 E2 45 B7 FD FA CE B4 D0 01 4A 3B 23 EF F7 8C 3A 17 62 8B 3B D0 59 9C CE 07 FA 54 99 7B D7 81 D4 78";

                string cert2 = "10 00 00 00 00 00 01 00 00 00 00 5E 6B 48 09 58 01 42 56 5B 1F 12 62 82 B7 6C A5 24 E8 75 7B 7C 2E 36 2A 90 BF 5A 9B 74 FC 06 2F 8C 7E A8 9E 66 24 3F 0F AA 57 C0 37 81 BE 32 44 03 05 78 CF D4 DC 94 C1 B2 F5 5D 58 66 04 81 9D B8 67 B2 87 42 2D E7 61 73 76 68 1F 65 C2 D5 37 83 23 C9 88 96 8A 77 9A AC D3 4C ED 73 61 04 B7 22 E9 FB 7A 1E 6F 1A 81 F1 7E 66 2C D5 C6 35 85 C2 3D 26 84 CA AF FC B7 94 4D AA 68 AA C3 B8 E5 13 59 AA 06";

                string cert3 = "10 00 00 00 00 00 01 00 00 00 00 5E 6A 48 09 56 BC 7F 0C BF 98 35 0A 2F DC F8 6C EC 24 F9 F2 07 70 72 A0 1A 0C 2C D7 67 75 2D D3 C0 78 83 0F FF 28 63 58 86 EC 04 59 04 38 2E 23 E1 D8 FF 1A 41 F6 89 93 D0 9D 18 1B 15 B4 F7 C0 C9 CE 3F EC F2 58 F5 CE 9D 83 DC 38 34 B0 92 25 51 8C E4 14 23 AD C6 B7 D2 7E E2 F9 B0 7C 52 BF 86 E2 9E E3 73 5A 8D 8B 1F CD C1 09 DA 6B 9C 0F 66 4F 24 44 6C FF 02 B5 98 9F B2 A7 46 20 A4 A7 98 82 F9 2C";

                string cert4 = "10 00 00 00 00 00 01 00 00 00 00 5E 69 48 09 33 E8 54 F9 16 29 8A 59 15 4E 78 9F A1 0C 38 7E CC 3C 02 B1 88 E3 74 A5 B8 AD A9 2B DE DC 18 51 16 61 05 9B AA E7 B5 EE 14 CC AD 0A CF 56 82 74 11 A2 C3 B8 0E FD 81 57 5F BF 4F 19 AD 60 7D 66 28 F8 21 07 A0 FA 4D 69 33 21 22 90 FC E1 E6 76 45 DA 1F FC ED 91 33 3F 05 32 BD 66 21 E1 4E 5B E2 9A 40 A6 3E CD D2 B5 1D 7F 26 36 18 33 3B 58 C2 F3 00 25 4B 2D F9 A6 ED 81 03 CF 5D 08 15 20";


                string cert5 = "10 00 00 00 00 00 01 00 00 00 00 5E 68 48 09 8F 11 B0 85 80 1F 16 D1 C8 52 35 A1 B4 5E 14 C9 B2 33 40 E5 4E 4E 28 F0 F7 7C 30 E9 07 52 32 74 54 99 E2 30 26 88 14 3E 5F A2 9A 7C D8 4D 03 54 C8 F6 F7 05 67 9D 9E 0F AE AF B1 05 9E 64 73 23 5D E1 2F F8 2B 9A 43 A9 63 98 CB 6E 15 64 64 0C 26 F2 02 CF 4E F2 D0 6F 0E 29 AF 99 57 7E 7E 43 C6 DE F9 FE 52 7E B4 D3 04 49 B8 78 62 BE 7B 6F 73 5B 3A 15 3F 51 1D 6F 67 7A 48 7B 31 68 18 2E";
                List<CertAuthTmp> AddCertAuthList = new List<CertAuthTmp>();
                List<CertTmp> AddCertList = new List<CertTmp>();


                CertTmp pp1 = new CertTmp();
                pp1.CertDataid = "1";
                pp1.CertDataHexStr = cert1;
                pp1.isSend = "1";
                AddCertList.Add(pp1);

                CertTmp pp2 = new CertTmp();
                pp2.CertDataid = "2";
                pp2.CertDataHexStr = cert2;
                pp2.isSend = "1";
                AddCertList.Add(pp2);


                CertTmp pp3 = new CertTmp();
                pp3.CertDataid = "3";
                pp3.CertDataHexStr = cert3;
                pp3.isSend = "1";
                AddCertList.Add(pp3);


                CertTmp pp4 = new CertTmp();
                pp4.CertDataid = "4";
                pp4.CertDataHexStr = cert4;
                pp4.isSend = "1";
                AddCertList.Add(pp4);

                CertTmp pp5 = new CertTmp();
                pp5.CertDataid = "5";
                pp5.CertDataHexStr = cert5;
                pp5.isSend = "1";
                AddCertList.Add(pp5);

                DealCert2Global(AddCertList);


                CertAuthTmp pp = new CertAuthTmp();
                pp.CertAuthDataid = "5";
                pp.CertAuthDataHexStr = auth;
                pp.isSend = "1";
                AddCertAuthList.Add(pp);

                DealCertAuth2Global(AddCertAuthList);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            try
            {
                string auth = "00 00 00 00 5e 65 00 05 00 00 36 01 00 02 00 00 00 00 5e 6b 00 00 00 00 5e 6a 00 00 00 00 5e 69 00 00 00 00 5e 68 00 00 00 00 00 01 65 28 98 25 97 df 42 1a 76 50 3a 98 a1 37 ba 51 18 61 94 64 9f bb 5e b5 e8 8a 42 4b 45 eb 34 94 48 17 50 da 93 c8 ac 98 8c d2 0c 9a 21 0e 47 b8 b7 18 ca 09 10 43 7d 27 da 0d 02 fb 4a 1f 56 4b";


                string cert1 = "10 00 00 00 00 00 01 00 00 36 01 00 02 48 09 04 A9 FC 3D A9 55 6A 93 1F E0 EA 73 07 36 82 38 0C A2 87 34 8A 1A 47 DE 55 93 57 99 E8 78 32 87 DC 5C 1B 0D 52 51 61 73 DC 85 AC 05 05 D4 B7 4D 9B C3 C1 07 CD B9 29 E3 73 1B 22 A8 43 8E 19 17 D6 2E 59 7E EB C5 DC B9 B0 B9 95 59 DD E5 E8 47 4A 22 31 E2 E1 38 2F B4 ED 95 CC 97 4F FA 85 E2 45 B7 FD FA CE B4 D0 01 4A 3B 23 EF F7 8C 3A 17 62 8B 3B D0 59 9C CE 07 FA 54 99 7B D7 81 D4 78";

                string cert2 = "10 00 00 00 00 00 01 00 00 00 00 5E 6B 48 09 58 01 42 56 5B 1F 12 62 82 B7 6C A5 24 E8 75 7B 7C 2E 36 2A 90 BF 5A 9B 74 FC 06 2F 8C 7E A8 9E 66 24 3F 0F AA 57 C0 37 81 BE 32 44 03 05 78 CF D4 DC 94 C1 B2 F5 5D 58 66 04 81 9D B8 67 B2 87 42 2D E7 61 73 76 68 1F 65 C2 D5 37 83 23 C9 88 96 8A 77 9A AC D3 4C ED 73 61 04 B7 22 E9 FB 7A 1E 6F 1A 81 F1 7E 66 2C D5 C6 35 85 C2 3D 26 84 CA AF FC B7 94 4D AA 68 AA C3 B8 E5 13 59 AA 06";

                string cert3 = "10 00 00 00 00 00 01 00 00 00 00 5E 6A 48 09 56 BC 7F 0C BF 98 35 0A 2F DC F8 6C EC 24 F9 F2 07 70 72 A0 1A 0C 2C D7 67 75 2D D3 C0 78 83 0F FF 28 63 58 86 EC 04 59 04 38 2E 23 E1 D8 FF 1A 41 F6 89 93 D0 9D 18 1B 15 B4 F7 C0 C9 CE 3F EC F2 58 F5 CE 9D 83 DC 38 34 B0 92 25 51 8C E4 14 23 AD C6 B7 D2 7E E2 F9 B0 7C 52 BF 86 E2 9E E3 73 5A 8D 8B 1F CD C1 09 DA 6B 9C 0F 66 4F 24 44 6C FF 02 B5 98 9F B2 A7 46 20 A4 A7 98 82 F9 2C";

                string cert4 = "10 00 00 00 00 00 01 00 00 00 00 5E 69 48 09 33 E8 54 F9 16 29 8A 59 15 4E 78 9F A1 0C 38 7E CC 3C 02 B1 88 E3 74 A5 B8 AD A9 2B DE DC 18 51 16 61 05 9B AA E7 B5 EE 14 CC AD 0A CF 56 82 74 11 A2 C3 B8 0E FD 81 57 5F BF 4F 19 AD 60 7D 66 28 F8 21 07 A0 FA 4D 69 33 21 22 90 FC E1 E6 76 45 DA 1F FC ED 91 33 3F 05 32 BD 66 21 E1 4E 5B E2 9A 40 A6 3E CD D2 B5 1D 7F 26 36 18 33 3B 58 C2 F3 00 25 4B 2D F9 A6 ED 81 03 CF 5D 08 15 20";


                string cert5 = "10 00 00 00 00 00 01 00 00 00 00 5E 68 48 09 8F 11 B0 85 80 1F 16 D1 C8 52 35 A1 B4 5E 14 C9 B2 33 40 E5 4E 4E 28 F0 F7 7C 30 E9 07 52 32 74 54 99 E2 30 26 88 14 3E 5F A2 9A 7C D8 4D 03 54 C8 F6 F7 05 67 9D 9E 0F AE AF B1 05 9E 64 73 23 5D E1 2F F8 2B 9A 43 A9 63 98 CB 6E 15 64 64 0C 26 F2 02 CF 4E F2 D0 6F 0E 29 AF 99 57 7E 7E 43 C6 DE F9 FE 52 7E B4 D3 04 49 B8 78 62 BE 7B 6F 73 5B 3A 15 3F 51 1D 6F 67 7A 48 7B 31 68 18 2E";
                List<CertAuthTmp> AddCertAuthList = new List<CertAuthTmp>();
                List<CertTmp> AddCertList = new List<CertTmp>();


                CertTmp pp1 = new CertTmp();
                pp1.CertDataid = "1";
                pp1.CertDataHexStr = cert1;
                pp1.isSend = "1";
                AddCertList.Add(pp1);

                CertTmp pp2 = new CertTmp();
                pp2.CertDataid = "2";
                pp2.CertDataHexStr = cert2;
                pp2.isSend = "1";
                AddCertList.Add(pp2);


                CertTmp pp3 = new CertTmp();
                pp3.CertDataid = "3";
                pp3.CertDataHexStr = cert3;
                pp3.isSend = "1";
                AddCertList.Add(pp3);


                CertTmp pp4 = new CertTmp();
                pp4.CertDataid = "4";
                pp4.CertDataHexStr = cert4;
                pp4.isSend = "1";
                AddCertList.Add(pp4);

                CertTmp pp5 = new CertTmp();
                pp5.CertDataid = "5";
                pp5.CertDataHexStr = cert5;
                pp5.isSend = "1";
                AddCertList.Add(pp5);

                DealCert2Global(AddCertList);


                CertAuthTmp pp = new CertAuthTmp();
                pp.CertAuthDataid = "5";
                pp.CertAuthDataHexStr = auth;
                pp.isSend = "1";
                AddCertAuthList.Add(pp);

                DealCertAuth2Global(AddCertAuthList);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            try
            {
                string auth = "00 00 00 00 5e 66 00 05 00 00 36 01 00 02 00 00 00 00 5e 6b 00 00 00 00 5e 6a 00 00 00 00 5e 69 00 00 00 00 5e 68 00 00 00 00 00 01 ba d4 08 8f d9 f1 3c 2f 3a 8c 4e 4c 6e a3 18 ec b0 18 1c 2a 49 20 6c eb b8 05 7d 16 53 11 09 62 1b 6b 3e 12 4c e7 10 fc 40 52 90 2b 42 34 3d 02 84 a8 6f 3e df 8e 87 59 ad e8 e3 3a 51 99 d1 2b";


                string cert1 = "10 00 00 00 00 00 01 00 00 36 01 00 02 48 09 04 A9 FC 3D A9 55 6A 93 1F E0 EA 73 07 36 82 38 0C A2 87 34 8A 1A 47 DE 55 93 57 99 E8 78 32 87 DC 5C 1B 0D 52 51 61 73 DC 85 AC 05 05 D4 B7 4D 9B C3 C1 07 CD B9 29 E3 73 1B 22 A8 43 8E 19 17 D6 2E 59 7E EB C5 DC B9 B0 B9 95 59 DD E5 E8 47 4A 22 31 E2 E1 38 2F B4 ED 95 CC 97 4F FA 85 E2 45 B7 FD FA CE B4 D0 01 4A 3B 23 EF F7 8C 3A 17 62 8B 3B D0 59 9C CE 07 FA 54 99 7B D7 81 D4 78";

                string cert2 = "10 00 00 00 00 00 01 00 00 00 00 5E 6B 48 09 58 01 42 56 5B 1F 12 62 82 B7 6C A5 24 E8 75 7B 7C 2E 36 2A 90 BF 5A 9B 74 FC 06 2F 8C 7E A8 9E 66 24 3F 0F AA 57 C0 37 81 BE 32 44 03 05 78 CF D4 DC 94 C1 B2 F5 5D 58 66 04 81 9D B8 67 B2 87 42 2D E7 61 73 76 68 1F 65 C2 D5 37 83 23 C9 88 96 8A 77 9A AC D3 4C ED 73 61 04 B7 22 E9 FB 7A 1E 6F 1A 81 F1 7E 66 2C D5 C6 35 85 C2 3D 26 84 CA AF FC B7 94 4D AA 68 AA C3 B8 E5 13 59 AA 06";

                string cert3 = "10 00 00 00 00 00 01 00 00 00 00 5E 6A 48 09 56 BC 7F 0C BF 98 35 0A 2F DC F8 6C EC 24 F9 F2 07 70 72 A0 1A 0C 2C D7 67 75 2D D3 C0 78 83 0F FF 28 63 58 86 EC 04 59 04 38 2E 23 E1 D8 FF 1A 41 F6 89 93 D0 9D 18 1B 15 B4 F7 C0 C9 CE 3F EC F2 58 F5 CE 9D 83 DC 38 34 B0 92 25 51 8C E4 14 23 AD C6 B7 D2 7E E2 F9 B0 7C 52 BF 86 E2 9E E3 73 5A 8D 8B 1F CD C1 09 DA 6B 9C 0F 66 4F 24 44 6C FF 02 B5 98 9F B2 A7 46 20 A4 A7 98 82 F9 2C";

                string cert4 = "10 00 00 00 00 00 01 00 00 00 00 5E 69 48 09 33 E8 54 F9 16 29 8A 59 15 4E 78 9F A1 0C 38 7E CC 3C 02 B1 88 E3 74 A5 B8 AD A9 2B DE DC 18 51 16 61 05 9B AA E7 B5 EE 14 CC AD 0A CF 56 82 74 11 A2 C3 B8 0E FD 81 57 5F BF 4F 19 AD 60 7D 66 28 F8 21 07 A0 FA 4D 69 33 21 22 90 FC E1 E6 76 45 DA 1F FC ED 91 33 3F 05 32 BD 66 21 E1 4E 5B E2 9A 40 A6 3E CD D2 B5 1D 7F 26 36 18 33 3B 58 C2 F3 00 25 4B 2D F9 A6 ED 81 03 CF 5D 08 15 20";


                string cert5 = "10 00 00 00 00 00 01 00 00 00 00 5E 68 48 09 8F 11 B0 85 80 1F 16 D1 C8 52 35 A1 B4 5E 14 C9 B2 33 40 E5 4E 4E 28 F0 F7 7C 30 E9 07 52 32 74 54 99 E2 30 26 88 14 3E 5F A2 9A 7C D8 4D 03 54 C8 F6 F7 05 67 9D 9E 0F AE AF B1 05 9E 64 73 23 5D E1 2F F8 2B 9A 43 A9 63 98 CB 6E 15 64 64 0C 26 F2 02 CF 4E F2 D0 6F 0E 29 AF 99 57 7E 7E 43 C6 DE F9 FE 52 7E B4 D3 04 49 B8 78 62 BE 7B 6F 73 5B 3A 15 3F 51 1D 6F 67 7A 48 7B 31 68 18 2E";
                List<CertAuthTmp> AddCertAuthList = new List<CertAuthTmp>();
                List<CertTmp> AddCertList = new List<CertTmp>();


                CertTmp pp1 = new CertTmp();
                pp1.CertDataid = "1";
                pp1.CertDataHexStr = cert1;
                pp1.isSend = "1";
                AddCertList.Add(pp1);

                CertTmp pp2 = new CertTmp();
                pp2.CertDataid = "2";
                pp2.CertDataHexStr = cert2;
                pp2.isSend = "1";
                AddCertList.Add(pp2);


                CertTmp pp3 = new CertTmp();
                pp3.CertDataid = "3";
                pp3.CertDataHexStr = cert3;
                pp3.isSend = "1";
                AddCertList.Add(pp3);


                CertTmp pp4 = new CertTmp();
                pp4.CertDataid = "4";
                pp4.CertDataHexStr = cert4;
                pp4.isSend = "1";
                AddCertList.Add(pp4);

                CertTmp pp5 = new CertTmp();
                pp5.CertDataid = "5";
                pp5.CertDataHexStr = cert5;
                pp5.isSend = "1";
                AddCertList.Add(pp5);

                DealCert2Global(AddCertList);


                CertAuthTmp pp = new CertAuthTmp();
                pp.CertAuthDataid = "5";
                pp.CertAuthDataHexStr = auth;
                pp.isSend = "1";
                AddCertAuthList.Add(pp);

                DealCertAuth2Global(AddCertAuthList);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            try
            {
                string auth = "00 00 00 00 5e 67 00 05 00 00 36 01 00 02 00 00 00 00 5e 6b 00 00 00 00 5e 6a 00 00 00 00 5e 69 00 00 00 00 5e 68 00 00 00 00 00 01 44 29 e1 06 f5 40 0d 62 01 d6 41 38 f8 ea 93 30 14 72 e8 60 68 f7 1f f0 01 f0 7b 8c 05 80 e0 d5 de 8d e8 3f 8c 07 4b 23 0d 4d 2b 5d 8f 5b e3 7c c5 dc 72 e3 63 f7 c2 80 36 3e 02 80 70 9b 33 80";


                string cert1 = "10 00 00 00 00 00 01 00 00 36 01 00 02 48 09 04 A9 FC 3D A9 55 6A 93 1F E0 EA 73 07 36 82 38 0C A2 87 34 8A 1A 47 DE 55 93 57 99 E8 78 32 87 DC 5C 1B 0D 52 51 61 73 DC 85 AC 05 05 D4 B7 4D 9B C3 C1 07 CD B9 29 E3 73 1B 22 A8 43 8E 19 17 D6 2E 59 7E EB C5 DC B9 B0 B9 95 59 DD E5 E8 47 4A 22 31 E2 E1 38 2F B4 ED 95 CC 97 4F FA 85 E2 45 B7 FD FA CE B4 D0 01 4A 3B 23 EF F7 8C 3A 17 62 8B 3B D0 59 9C CE 07 FA 54 99 7B D7 81 D4 78";

                string cert2 = "10 00 00 00 00 00 01 00 00 00 00 5E 6B 48 09 58 01 42 56 5B 1F 12 62 82 B7 6C A5 24 E8 75 7B 7C 2E 36 2A 90 BF 5A 9B 74 FC 06 2F 8C 7E A8 9E 66 24 3F 0F AA 57 C0 37 81 BE 32 44 03 05 78 CF D4 DC 94 C1 B2 F5 5D 58 66 04 81 9D B8 67 B2 87 42 2D E7 61 73 76 68 1F 65 C2 D5 37 83 23 C9 88 96 8A 77 9A AC D3 4C ED 73 61 04 B7 22 E9 FB 7A 1E 6F 1A 81 F1 7E 66 2C D5 C6 35 85 C2 3D 26 84 CA AF FC B7 94 4D AA 68 AA C3 B8 E5 13 59 AA 06";

                string cert3 = "10 00 00 00 00 00 01 00 00 00 00 5E 6A 48 09 56 BC 7F 0C BF 98 35 0A 2F DC F8 6C EC 24 F9 F2 07 70 72 A0 1A 0C 2C D7 67 75 2D D3 C0 78 83 0F FF 28 63 58 86 EC 04 59 04 38 2E 23 E1 D8 FF 1A 41 F6 89 93 D0 9D 18 1B 15 B4 F7 C0 C9 CE 3F EC F2 58 F5 CE 9D 83 DC 38 34 B0 92 25 51 8C E4 14 23 AD C6 B7 D2 7E E2 F9 B0 7C 52 BF 86 E2 9E E3 73 5A 8D 8B 1F CD C1 09 DA 6B 9C 0F 66 4F 24 44 6C FF 02 B5 98 9F B2 A7 46 20 A4 A7 98 82 F9 2C";

                string cert4 = "10 00 00 00 00 00 01 00 00 00 00 5E 69 48 09 33 E8 54 F9 16 29 8A 59 15 4E 78 9F A1 0C 38 7E CC 3C 02 B1 88 E3 74 A5 B8 AD A9 2B DE DC 18 51 16 61 05 9B AA E7 B5 EE 14 CC AD 0A CF 56 82 74 11 A2 C3 B8 0E FD 81 57 5F BF 4F 19 AD 60 7D 66 28 F8 21 07 A0 FA 4D 69 33 21 22 90 FC E1 E6 76 45 DA 1F FC ED 91 33 3F 05 32 BD 66 21 E1 4E 5B E2 9A 40 A6 3E CD D2 B5 1D 7F 26 36 18 33 3B 58 C2 F3 00 25 4B 2D F9 A6 ED 81 03 CF 5D 08 15 20";


                string cert5 = "10 00 00 00 00 00 01 00 00 00 00 5E 68 48 09 8F 11 B0 85 80 1F 16 D1 C8 52 35 A1 B4 5E 14 C9 B2 33 40 E5 4E 4E 28 F0 F7 7C 30 E9 07 52 32 74 54 99 E2 30 26 88 14 3E 5F A2 9A 7C D8 4D 03 54 C8 F6 F7 05 67 9D 9E 0F AE AF B1 05 9E 64 73 23 5D E1 2F F8 2B 9A 43 A9 63 98 CB 6E 15 64 64 0C 26 F2 02 CF 4E F2 D0 6F 0E 29 AF 99 57 7E 7E 43 C6 DE F9 FE 52 7E B4 D3 04 49 B8 78 62 BE 7B 6F 73 5B 3A 15 3F 51 1D 6F 67 7A 48 7B 31 68 18 2E";
                List<CertAuthTmp> AddCertAuthList = new List<CertAuthTmp>();
                List<CertTmp> AddCertList = new List<CertTmp>();


                CertTmp pp1 = new CertTmp();
                pp1.CertDataid = "1";
                pp1.CertDataHexStr = cert1;
                pp1.isSend = "1";
                AddCertList.Add(pp1);

                CertTmp pp2 = new CertTmp();
                pp2.CertDataid = "2";
                pp2.CertDataHexStr = cert2;
                pp2.isSend = "1";
                AddCertList.Add(pp2);


                CertTmp pp3 = new CertTmp();
                pp3.CertDataid = "3";
                pp3.CertDataHexStr = cert3;
                pp3.isSend = "1";
                AddCertList.Add(pp3);


                CertTmp pp4 = new CertTmp();
                pp4.CertDataid = "4";
                pp4.CertDataHexStr = cert4;
                pp4.isSend = "1";
                AddCertList.Add(pp4);

                CertTmp pp5 = new CertTmp();
                pp5.CertDataid = "5";
                pp5.CertDataHexStr = cert5;
                pp5.isSend = "1";
                AddCertList.Add(pp5);

                DealCert2Global(AddCertList);


                CertAuthTmp pp = new CertAuthTmp();
                pp.CertAuthDataid = "5";
                pp.CertAuthDataHexStr = auth;
                pp.isSend = "1";
                AddCertAuthList.Add(pp);

                DealCertAuth2Global(AddCertAuthList);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            try
            {
                string auth = "00 00 00 00 5e 68 00 05 00 00 36 01 00 02 00 00 00 00 5e 6b 00 00 00 00 5e 6a 00 00 00 00 5e 69 00 00 00 00 5e 68 00 00 00 00 00 01 d3 27 14 08 27 f2 53 2f fc 6e a1 ad dc 02 c7 82 c8 b4 ea ca b2 0c c1 43 b1 30 e8 6e dd ae bf 6e 6d 9f da c4 f5 26 5f ec d9 30 f0 e6 4d 0c eb f4 51 c7 9e b3 2e 05 6d 47 ba d2 30 f0 b0 b3 82 54";


                string cert1 = "10 00 00 00 00 00 01 00 00 36 01 00 02 48 09 04 A9 FC 3D A9 55 6A 93 1F E0 EA 73 07 36 82 38 0C A2 87 34 8A 1A 47 DE 55 93 57 99 E8 78 32 87 DC 5C 1B 0D 52 51 61 73 DC 85 AC 05 05 D4 B7 4D 9B C3 C1 07 CD B9 29 E3 73 1B 22 A8 43 8E 19 17 D6 2E 59 7E EB C5 DC B9 B0 B9 95 59 DD E5 E8 47 4A 22 31 E2 E1 38 2F B4 ED 95 CC 97 4F FA 85 E2 45 B7 FD FA CE B4 D0 01 4A 3B 23 EF F7 8C 3A 17 62 8B 3B D0 59 9C CE 07 FA 54 99 7B D7 81 D4 78";

                string cert2 = "10 00 00 00 00 00 01 00 00 00 00 5E 6B 48 09 58 01 42 56 5B 1F 12 62 82 B7 6C A5 24 E8 75 7B 7C 2E 36 2A 90 BF 5A 9B 74 FC 06 2F 8C 7E A8 9E 66 24 3F 0F AA 57 C0 37 81 BE 32 44 03 05 78 CF D4 DC 94 C1 B2 F5 5D 58 66 04 81 9D B8 67 B2 87 42 2D E7 61 73 76 68 1F 65 C2 D5 37 83 23 C9 88 96 8A 77 9A AC D3 4C ED 73 61 04 B7 22 E9 FB 7A 1E 6F 1A 81 F1 7E 66 2C D5 C6 35 85 C2 3D 26 84 CA AF FC B7 94 4D AA 68 AA C3 B8 E5 13 59 AA 06";

                string cert3 = "10 00 00 00 00 00 01 00 00 00 00 5E 6A 48 09 56 BC 7F 0C BF 98 35 0A 2F DC F8 6C EC 24 F9 F2 07 70 72 A0 1A 0C 2C D7 67 75 2D D3 C0 78 83 0F FF 28 63 58 86 EC 04 59 04 38 2E 23 E1 D8 FF 1A 41 F6 89 93 D0 9D 18 1B 15 B4 F7 C0 C9 CE 3F EC F2 58 F5 CE 9D 83 DC 38 34 B0 92 25 51 8C E4 14 23 AD C6 B7 D2 7E E2 F9 B0 7C 52 BF 86 E2 9E E3 73 5A 8D 8B 1F CD C1 09 DA 6B 9C 0F 66 4F 24 44 6C FF 02 B5 98 9F B2 A7 46 20 A4 A7 98 82 F9 2C";

                string cert4 = "10 00 00 00 00 00 01 00 00 00 00 5E 69 48 09 33 E8 54 F9 16 29 8A 59 15 4E 78 9F A1 0C 38 7E CC 3C 02 B1 88 E3 74 A5 B8 AD A9 2B DE DC 18 51 16 61 05 9B AA E7 B5 EE 14 CC AD 0A CF 56 82 74 11 A2 C3 B8 0E FD 81 57 5F BF 4F 19 AD 60 7D 66 28 F8 21 07 A0 FA 4D 69 33 21 22 90 FC E1 E6 76 45 DA 1F FC ED 91 33 3F 05 32 BD 66 21 E1 4E 5B E2 9A 40 A6 3E CD D2 B5 1D 7F 26 36 18 33 3B 58 C2 F3 00 25 4B 2D F9 A6 ED 81 03 CF 5D 08 15 20";


                string cert5 = "10 00 00 00 00 00 01 00 00 00 00 5E 68 48 09 8F 11 B0 85 80 1F 16 D1 C8 52 35 A1 B4 5E 14 C9 B2 33 40 E5 4E 4E 28 F0 F7 7C 30 E9 07 52 32 74 54 99 E2 30 26 88 14 3E 5F A2 9A 7C D8 4D 03 54 C8 F6 F7 05 67 9D 9E 0F AE AF B1 05 9E 64 73 23 5D E1 2F F8 2B 9A 43 A9 63 98 CB 6E 15 64 64 0C 26 F2 02 CF 4E F2 D0 6F 0E 29 AF 99 57 7E 7E 43 C6 DE F9 FE 52 7E B4 D3 04 49 B8 78 62 BE 7B 6F 73 5B 3A 15 3F 51 1D 6F 67 7A 48 7B 31 68 18 2E";
                List<CertAuthTmp> AddCertAuthList = new List<CertAuthTmp>();
                List<CertTmp> AddCertList = new List<CertTmp>();


                CertTmp pp1 = new CertTmp();
                pp1.CertDataid = "1";
                pp1.CertDataHexStr = cert1;
                pp1.isSend = "1";
                AddCertList.Add(pp1);

                CertTmp pp2 = new CertTmp();
                pp2.CertDataid = "2";
                pp2.CertDataHexStr = cert2;
                pp2.isSend = "1";
                AddCertList.Add(pp2);


                CertTmp pp3 = new CertTmp();
                pp3.CertDataid = "3";
                pp3.CertDataHexStr = cert3;
                pp3.isSend = "1";
                AddCertList.Add(pp3);


                CertTmp pp4 = new CertTmp();
                pp4.CertDataid = "4";
                pp4.CertDataHexStr = cert4;
                pp4.isSend = "1";
                AddCertList.Add(pp4);

                CertTmp pp5 = new CertTmp();
                pp5.CertDataid = "5";
                pp5.CertDataHexStr = cert5;
                pp5.isSend = "1";
                AddCertList.Add(pp5);

                DealCert2Global(AddCertList);


                CertAuthTmp pp = new CertAuthTmp();
                pp.CertAuthDataid = "5";
                pp.CertAuthDataHexStr = auth;
                pp.isSend = "1";
                AddCertAuthList.Add(pp);

                DealCertAuth2Global(AddCertAuthList);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            try
            {
                string auth = "00 00 00 00 5e 69 00 05 00 00 36 01 00 02 00 00 00 00 5e 6b 00 00 00 00 5e 6a 00 00 00 00 5e 69 00 00 00 00 5e 68 00 00 00 00 00 01 b0 31 db 75 b1 00 17 54 50 40 03 0b fd f3 7d dc 82 3a 6f 0d c9 0f 06 2c 40 ca 43 9c 6a b7 22 ac 7e 90 45 fc af c0 b8 1c 17 cf ec 80 85 12 f0 69 f2 8e b1 a4 9e 91 1b 4a 11 44 ca 8b 7d 37 3c 71";


                string cert1 = "10 00 00 00 00 00 01 00 00 36 01 00 02 48 09 04 A9 FC 3D A9 55 6A 93 1F E0 EA 73 07 36 82 38 0C A2 87 34 8A 1A 47 DE 55 93 57 99 E8 78 32 87 DC 5C 1B 0D 52 51 61 73 DC 85 AC 05 05 D4 B7 4D 9B C3 C1 07 CD B9 29 E3 73 1B 22 A8 43 8E 19 17 D6 2E 59 7E EB C5 DC B9 B0 B9 95 59 DD E5 E8 47 4A 22 31 E2 E1 38 2F B4 ED 95 CC 97 4F FA 85 E2 45 B7 FD FA CE B4 D0 01 4A 3B 23 EF F7 8C 3A 17 62 8B 3B D0 59 9C CE 07 FA 54 99 7B D7 81 D4 78";

                string cert2 = "10 00 00 00 00 00 01 00 00 00 00 5E 6B 48 09 58 01 42 56 5B 1F 12 62 82 B7 6C A5 24 E8 75 7B 7C 2E 36 2A 90 BF 5A 9B 74 FC 06 2F 8C 7E A8 9E 66 24 3F 0F AA 57 C0 37 81 BE 32 44 03 05 78 CF D4 DC 94 C1 B2 F5 5D 58 66 04 81 9D B8 67 B2 87 42 2D E7 61 73 76 68 1F 65 C2 D5 37 83 23 C9 88 96 8A 77 9A AC D3 4C ED 73 61 04 B7 22 E9 FB 7A 1E 6F 1A 81 F1 7E 66 2C D5 C6 35 85 C2 3D 26 84 CA AF FC B7 94 4D AA 68 AA C3 B8 E5 13 59 AA 06";

                string cert3 = "10 00 00 00 00 00 01 00 00 00 00 5E 6A 48 09 56 BC 7F 0C BF 98 35 0A 2F DC F8 6C EC 24 F9 F2 07 70 72 A0 1A 0C 2C D7 67 75 2D D3 C0 78 83 0F FF 28 63 58 86 EC 04 59 04 38 2E 23 E1 D8 FF 1A 41 F6 89 93 D0 9D 18 1B 15 B4 F7 C0 C9 CE 3F EC F2 58 F5 CE 9D 83 DC 38 34 B0 92 25 51 8C E4 14 23 AD C6 B7 D2 7E E2 F9 B0 7C 52 BF 86 E2 9E E3 73 5A 8D 8B 1F CD C1 09 DA 6B 9C 0F 66 4F 24 44 6C FF 02 B5 98 9F B2 A7 46 20 A4 A7 98 82 F9 2C";

                string cert4 = "10 00 00 00 00 00 01 00 00 00 00 5E 69 48 09 33 E8 54 F9 16 29 8A 59 15 4E 78 9F A1 0C 38 7E CC 3C 02 B1 88 E3 74 A5 B8 AD A9 2B DE DC 18 51 16 61 05 9B AA E7 B5 EE 14 CC AD 0A CF 56 82 74 11 A2 C3 B8 0E FD 81 57 5F BF 4F 19 AD 60 7D 66 28 F8 21 07 A0 FA 4D 69 33 21 22 90 FC E1 E6 76 45 DA 1F FC ED 91 33 3F 05 32 BD 66 21 E1 4E 5B E2 9A 40 A6 3E CD D2 B5 1D 7F 26 36 18 33 3B 58 C2 F3 00 25 4B 2D F9 A6 ED 81 03 CF 5D 08 15 20";


                string cert5 = "10 00 00 00 00 00 01 00 00 00 00 5E 68 48 09 8F 11 B0 85 80 1F 16 D1 C8 52 35 A1 B4 5E 14 C9 B2 33 40 E5 4E 4E 28 F0 F7 7C 30 E9 07 52 32 74 54 99 E2 30 26 88 14 3E 5F A2 9A 7C D8 4D 03 54 C8 F6 F7 05 67 9D 9E 0F AE AF B1 05 9E 64 73 23 5D E1 2F F8 2B 9A 43 A9 63 98 CB 6E 15 64 64 0C 26 F2 02 CF 4E F2 D0 6F 0E 29 AF 99 57 7E 7E 43 C6 DE F9 FE 52 7E B4 D3 04 49 B8 78 62 BE 7B 6F 73 5B 3A 15 3F 51 1D 6F 67 7A 48 7B 31 68 18 2E";
                List<CertAuthTmp> AddCertAuthList = new List<CertAuthTmp>();
                List<CertTmp> AddCertList = new List<CertTmp>();


                CertTmp pp1 = new CertTmp();
                pp1.CertDataid = "1";
                pp1.CertDataHexStr = cert1;
                pp1.isSend = "1";
                AddCertList.Add(pp1);

                CertTmp pp2 = new CertTmp();
                pp2.CertDataid = "2";
                pp2.CertDataHexStr = cert2;
                pp2.isSend = "1";
                AddCertList.Add(pp2);


                CertTmp pp3 = new CertTmp();
                pp3.CertDataid = "3";
                pp3.CertDataHexStr = cert3;
                pp3.isSend = "1";
                AddCertList.Add(pp3);


                CertTmp pp4 = new CertTmp();
                pp4.CertDataid = "4";
                pp4.CertDataHexStr = cert4;
                pp4.isSend = "1";
                AddCertList.Add(pp4);

                CertTmp pp5 = new CertTmp();
                pp5.CertDataid = "5";
                pp5.CertDataHexStr = cert5;
                pp5.isSend = "1";
                AddCertList.Add(pp5);

                DealCert2Global(AddCertList);


                CertAuthTmp pp = new CertAuthTmp();
                pp.CertAuthDataid = "5";
                pp.CertAuthDataHexStr = auth;
                pp.isSend = "1";
                AddCertAuthList.Add(pp);

                DealCertAuth2Global(AddCertAuthList);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            try
            {
                string auth = "00 00 00 00 5e 6b 00 05 00 00 36 01 00 02 00 00 00 00 5e 6b 00 00 00 00 5e 6a 00 00 00 00 5e 69 00 00 00 00 5e 68 00 00 00 00 00 01 6c 8f d7 1d e5 53 8d 79 16 f7 04 d5 ac 73 aa e7 27 c7 11 a3 47 9b bb 24 93 ff 9f 88 dc 3c 33 5a 2e 66 65 38 10 59 90 c8 15 b2 5b ca 3a 54 12 0d 52 24 3a 22 36 ef fd d3 60 c4 5a 28 c8 c5 87 47";


                string cert1 = "10 00 00 00 00 00 01 00 00 36 01 00 02 48 09 04 A9 FC 3D A9 55 6A 93 1F E0 EA 73 07 36 82 38 0C A2 87 34 8A 1A 47 DE 55 93 57 99 E8 78 32 87 DC 5C 1B 0D 52 51 61 73 DC 85 AC 05 05 D4 B7 4D 9B C3 C1 07 CD B9 29 E3 73 1B 22 A8 43 8E 19 17 D6 2E 59 7E EB C5 DC B9 B0 B9 95 59 DD E5 E8 47 4A 22 31 E2 E1 38 2F B4 ED 95 CC 97 4F FA 85 E2 45 B7 FD FA CE B4 D0 01 4A 3B 23 EF F7 8C 3A 17 62 8B 3B D0 59 9C CE 07 FA 54 99 7B D7 81 D4 78";

                string cert2 = "10 00 00 00 00 00 01 00 00 00 00 5E 6B 48 09 58 01 42 56 5B 1F 12 62 82 B7 6C A5 24 E8 75 7B 7C 2E 36 2A 90 BF 5A 9B 74 FC 06 2F 8C 7E A8 9E 66 24 3F 0F AA 57 C0 37 81 BE 32 44 03 05 78 CF D4 DC 94 C1 B2 F5 5D 58 66 04 81 9D B8 67 B2 87 42 2D E7 61 73 76 68 1F 65 C2 D5 37 83 23 C9 88 96 8A 77 9A AC D3 4C ED 73 61 04 B7 22 E9 FB 7A 1E 6F 1A 81 F1 7E 66 2C D5 C6 35 85 C2 3D 26 84 CA AF FC B7 94 4D AA 68 AA C3 B8 E5 13 59 AA 06";

                string cert3 = "10 00 00 00 00 00 01 00 00 00 00 5E 6A 48 09 56 BC 7F 0C BF 98 35 0A 2F DC F8 6C EC 24 F9 F2 07 70 72 A0 1A 0C 2C D7 67 75 2D D3 C0 78 83 0F FF 28 63 58 86 EC 04 59 04 38 2E 23 E1 D8 FF 1A 41 F6 89 93 D0 9D 18 1B 15 B4 F7 C0 C9 CE 3F EC F2 58 F5 CE 9D 83 DC 38 34 B0 92 25 51 8C E4 14 23 AD C6 B7 D2 7E E2 F9 B0 7C 52 BF 86 E2 9E E3 73 5A 8D 8B 1F CD C1 09 DA 6B 9C 0F 66 4F 24 44 6C FF 02 B5 98 9F B2 A7 46 20 A4 A7 98 82 F9 2C";

                string cert4 = "10 00 00 00 00 00 01 00 00 00 00 5E 69 48 09 33 E8 54 F9 16 29 8A 59 15 4E 78 9F A1 0C 38 7E CC 3C 02 B1 88 E3 74 A5 B8 AD A9 2B DE DC 18 51 16 61 05 9B AA E7 B5 EE 14 CC AD 0A CF 56 82 74 11 A2 C3 B8 0E FD 81 57 5F BF 4F 19 AD 60 7D 66 28 F8 21 07 A0 FA 4D 69 33 21 22 90 FC E1 E6 76 45 DA 1F FC ED 91 33 3F 05 32 BD 66 21 E1 4E 5B E2 9A 40 A6 3E CD D2 B5 1D 7F 26 36 18 33 3B 58 C2 F3 00 25 4B 2D F9 A6 ED 81 03 CF 5D 08 15 20";


                string cert5 = "10 00 00 00 00 00 01 00 00 00 00 5E 68 48 09 8F 11 B0 85 80 1F 16 D1 C8 52 35 A1 B4 5E 14 C9 B2 33 40 E5 4E 4E 28 F0 F7 7C 30 E9 07 52 32 74 54 99 E2 30 26 88 14 3E 5F A2 9A 7C D8 4D 03 54 C8 F6 F7 05 67 9D 9E 0F AE AF B1 05 9E 64 73 23 5D E1 2F F8 2B 9A 43 A9 63 98 CB 6E 15 64 64 0C 26 F2 02 CF 4E F2 D0 6F 0E 29 AF 99 57 7E 7E 43 C6 DE F9 FE 52 7E B4 D3 04 49 B8 78 62 BE 7B 6F 73 5B 3A 15 3F 51 1D 6F 67 7A 48 7B 31 68 18 2E";
                List<CertAuthTmp> AddCertAuthList = new List<CertAuthTmp>();
                List<CertTmp> AddCertList = new List<CertTmp>();


                CertTmp pp1 = new CertTmp();
                pp1.CertDataid = "1";
                pp1.CertDataHexStr = cert1;
                pp1.isSend = "1";
                AddCertList.Add(pp1);

                CertTmp pp2 = new CertTmp();
                pp2.CertDataid = "2";
                pp2.CertDataHexStr = cert2;
                pp2.isSend = "1";
                AddCertList.Add(pp2);


                CertTmp pp3 = new CertTmp();
                pp3.CertDataid = "3";
                pp3.CertDataHexStr = cert3;
                pp3.isSend = "1";
                AddCertList.Add(pp3);


                CertTmp pp4 = new CertTmp();
                pp4.CertDataid = "4";
                pp4.CertDataHexStr = cert4;
                pp4.isSend = "1";
                AddCertList.Add(pp4);

                CertTmp pp5 = new CertTmp();
                pp5.CertDataid = "5";
                pp5.CertDataHexStr = cert5;
                pp5.isSend = "1";
                AddCertList.Add(pp5);

                DealCert2Global(AddCertList);


                CertAuthTmp pp = new CertAuthTmp();
                pp.CertAuthDataid = "5";
                pp.CertAuthDataHexStr = auth;
                pp.isSend = "1";
                AddCertAuthList.Add(pp);

                DealCertAuth2Global(AddCertAuthList);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            try
            {
                string auth = "00 00 00 00 06 13 00 05 00 00 36 01 00 02 00 00 00 00 5e 6b 00 00 00 00 5e 6a 00 00 00 00 5e 69 00 00 00 00 5e 68 00 00 00 00 00 01 4f ee 33 68 a6 4a b7 d1 41 ab ec 72 6d d7 73 d8 79 69 23 d2 bd 5f 6a 41 98 c8 3d 87 a7 a1 5c 16 f1 c7 82 88 a0 20 42 50 80 d7 79 7d c5 17 7b 4c 61 93 b7 98 10 ef 7c 2b a2 c0 3d 9e df 8d 36 e3";


                string cert1 = "10 00 00 00 00 00 01 00 00 36 01 00 02 48 09 04 A9 FC 3D A9 55 6A 93 1F E0 EA 73 07 36 82 38 0C A2 87 34 8A 1A 47 DE 55 93 57 99 E8 78 32 87 DC 5C 1B 0D 52 51 61 73 DC 85 AC 05 05 D4 B7 4D 9B C3 C1 07 CD B9 29 E3 73 1B 22 A8 43 8E 19 17 D6 2E 59 7E EB C5 DC B9 B0 B9 95 59 DD E5 E8 47 4A 22 31 E2 E1 38 2F B4 ED 95 CC 97 4F FA 85 E2 45 B7 FD FA CE B4 D0 01 4A 3B 23 EF F7 8C 3A 17 62 8B 3B D0 59 9C CE 07 FA 54 99 7B D7 81 D4 78";

                string cert2 = "10 00 00 00 00 00 01 00 00 00 00 5E 6B 48 09 58 01 42 56 5B 1F 12 62 82 B7 6C A5 24 E8 75 7B 7C 2E 36 2A 90 BF 5A 9B 74 FC 06 2F 8C 7E A8 9E 66 24 3F 0F AA 57 C0 37 81 BE 32 44 03 05 78 CF D4 DC 94 C1 B2 F5 5D 58 66 04 81 9D B8 67 B2 87 42 2D E7 61 73 76 68 1F 65 C2 D5 37 83 23 C9 88 96 8A 77 9A AC D3 4C ED 73 61 04 B7 22 E9 FB 7A 1E 6F 1A 81 F1 7E 66 2C D5 C6 35 85 C2 3D 26 84 CA AF FC B7 94 4D AA 68 AA C3 B8 E5 13 59 AA 06";

                string cert3 = "10 00 00 00 00 00 01 00 00 00 00 5E 6A 48 09 56 BC 7F 0C BF 98 35 0A 2F DC F8 6C EC 24 F9 F2 07 70 72 A0 1A 0C 2C D7 67 75 2D D3 C0 78 83 0F FF 28 63 58 86 EC 04 59 04 38 2E 23 E1 D8 FF 1A 41 F6 89 93 D0 9D 18 1B 15 B4 F7 C0 C9 CE 3F EC F2 58 F5 CE 9D 83 DC 38 34 B0 92 25 51 8C E4 14 23 AD C6 B7 D2 7E E2 F9 B0 7C 52 BF 86 E2 9E E3 73 5A 8D 8B 1F CD C1 09 DA 6B 9C 0F 66 4F 24 44 6C FF 02 B5 98 9F B2 A7 46 20 A4 A7 98 82 F9 2C";

                string cert4 = "10 00 00 00 00 00 01 00 00 00 00 5E 69 48 09 33 E8 54 F9 16 29 8A 59 15 4E 78 9F A1 0C 38 7E CC 3C 02 B1 88 E3 74 A5 B8 AD A9 2B DE DC 18 51 16 61 05 9B AA E7 B5 EE 14 CC AD 0A CF 56 82 74 11 A2 C3 B8 0E FD 81 57 5F BF 4F 19 AD 60 7D 66 28 F8 21 07 A0 FA 4D 69 33 21 22 90 FC E1 E6 76 45 DA 1F FC ED 91 33 3F 05 32 BD 66 21 E1 4E 5B E2 9A 40 A6 3E CD D2 B5 1D 7F 26 36 18 33 3B 58 C2 F3 00 25 4B 2D F9 A6 ED 81 03 CF 5D 08 15 20";


                string cert5 = "10 00 00 00 00 00 01 00 00 00 00 5E 68 48 09 8F 11 B0 85 80 1F 16 D1 C8 52 35 A1 B4 5E 14 C9 B2 33 40 E5 4E 4E 28 F0 F7 7C 30 E9 07 52 32 74 54 99 E2 30 26 88 14 3E 5F A2 9A 7C D8 4D 03 54 C8 F6 F7 05 67 9D 9E 0F AE AF B1 05 9E 64 73 23 5D E1 2F F8 2B 9A 43 A9 63 98 CB 6E 15 64 64 0C 26 F2 02 CF 4E F2 D0 6F 0E 29 AF 99 57 7E 7E 43 C6 DE F9 FE 52 7E B4 D3 04 49 B8 78 62 BE 7B 6F 73 5B 3A 15 3F 51 1D 6F 67 7A 48 7B 31 68 18 2E";
                List<CertAuthTmp> AddCertAuthList = new List<CertAuthTmp>();
                List<CertTmp> AddCertList = new List<CertTmp>();


                CertTmp pp1 = new CertTmp();
                pp1.CertDataid = "1";
                pp1.CertDataHexStr = cert1;
                pp1.isSend = "1";
                AddCertList.Add(pp1);

                CertTmp pp2 = new CertTmp();
                pp2.CertDataid = "2";
                pp2.CertDataHexStr = cert2;
                pp2.isSend = "1";
                AddCertList.Add(pp2);


                CertTmp pp3 = new CertTmp();
                pp3.CertDataid = "3";
                pp3.CertDataHexStr = cert3;
                pp3.isSend = "1";
                AddCertList.Add(pp3);


                CertTmp pp4 = new CertTmp();
                pp4.CertDataid = "4";
                pp4.CertDataHexStr = cert4;
                pp4.isSend = "1";
                AddCertList.Add(pp4);

                CertTmp pp5 = new CertTmp();
                pp5.CertDataid = "5";
                pp5.CertDataHexStr = cert5;
                pp5.isSend = "1";
                AddCertList.Add(pp5);

                DealCert2Global(AddCertList);


                CertAuthTmp pp = new CertAuthTmp();
                pp.CertAuthDataid = "5";
                pp.CertAuthDataHexStr = auth;
                pp.isSend = "1";
                AddCertAuthList.Add(pp);

                DealCertAuth2Global(AddCertAuthList);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            try
            {
                string auth = "00 00 00 00 00 53 00 05 00 00 36 01 00 02 00 00 00 00 5e 6b 00 00 00 00 5e 6a 00 00 00 00 5e 69 00 00 00 00 5e 68 00 00 00 00 00 01 91 c0 d8 a5 db 04 81 6c 35 b8 52 1c bc 09 b7 0f c6 5b b8 d3 77 e0 cc 8d 40 79 7f a8 68 1a 9a 5f 34 49 b1 15 b1 91 1f a5 bd d7 b8 4b 40 df 40 d2 a7 06 9a 24 27 31 b5 89 a0 7a 02 1c f1 43 0d 22";


                string cert1 = "10 00 00 00 00 00 01 00 00 36 01 00 02 48 09 04 A9 FC 3D A9 55 6A 93 1F E0 EA 73 07 36 82 38 0C A2 87 34 8A 1A 47 DE 55 93 57 99 E8 78 32 87 DC 5C 1B 0D 52 51 61 73 DC 85 AC 05 05 D4 B7 4D 9B C3 C1 07 CD B9 29 E3 73 1B 22 A8 43 8E 19 17 D6 2E 59 7E EB C5 DC B9 B0 B9 95 59 DD E5 E8 47 4A 22 31 E2 E1 38 2F B4 ED 95 CC 97 4F FA 85 E2 45 B7 FD FA CE B4 D0 01 4A 3B 23 EF F7 8C 3A 17 62 8B 3B D0 59 9C CE 07 FA 54 99 7B D7 81 D4 78";

                string cert2 = "10 00 00 00 00 00 01 00 00 00 00 5E 6B 48 09 58 01 42 56 5B 1F 12 62 82 B7 6C A5 24 E8 75 7B 7C 2E 36 2A 90 BF 5A 9B 74 FC 06 2F 8C 7E A8 9E 66 24 3F 0F AA 57 C0 37 81 BE 32 44 03 05 78 CF D4 DC 94 C1 B2 F5 5D 58 66 04 81 9D B8 67 B2 87 42 2D E7 61 73 76 68 1F 65 C2 D5 37 83 23 C9 88 96 8A 77 9A AC D3 4C ED 73 61 04 B7 22 E9 FB 7A 1E 6F 1A 81 F1 7E 66 2C D5 C6 35 85 C2 3D 26 84 CA AF FC B7 94 4D AA 68 AA C3 B8 E5 13 59 AA 06";

                string cert3 = "10 00 00 00 00 00 01 00 00 00 00 5E 6A 48 09 56 BC 7F 0C BF 98 35 0A 2F DC F8 6C EC 24 F9 F2 07 70 72 A0 1A 0C 2C D7 67 75 2D D3 C0 78 83 0F FF 28 63 58 86 EC 04 59 04 38 2E 23 E1 D8 FF 1A 41 F6 89 93 D0 9D 18 1B 15 B4 F7 C0 C9 CE 3F EC F2 58 F5 CE 9D 83 DC 38 34 B0 92 25 51 8C E4 14 23 AD C6 B7 D2 7E E2 F9 B0 7C 52 BF 86 E2 9E E3 73 5A 8D 8B 1F CD C1 09 DA 6B 9C 0F 66 4F 24 44 6C FF 02 B5 98 9F B2 A7 46 20 A4 A7 98 82 F9 2C";

                string cert4 = "10 00 00 00 00 00 01 00 00 00 00 5E 69 48 09 33 E8 54 F9 16 29 8A 59 15 4E 78 9F A1 0C 38 7E CC 3C 02 B1 88 E3 74 A5 B8 AD A9 2B DE DC 18 51 16 61 05 9B AA E7 B5 EE 14 CC AD 0A CF 56 82 74 11 A2 C3 B8 0E FD 81 57 5F BF 4F 19 AD 60 7D 66 28 F8 21 07 A0 FA 4D 69 33 21 22 90 FC E1 E6 76 45 DA 1F FC ED 91 33 3F 05 32 BD 66 21 E1 4E 5B E2 9A 40 A6 3E CD D2 B5 1D 7F 26 36 18 33 3B 58 C2 F3 00 25 4B 2D F9 A6 ED 81 03 CF 5D 08 15 20";


                string cert5 = "10 00 00 00 00 00 01 00 00 00 00 5E 68 48 09 8F 11 B0 85 80 1F 16 D1 C8 52 35 A1 B4 5E 14 C9 B2 33 40 E5 4E 4E 28 F0 F7 7C 30 E9 07 52 32 74 54 99 E2 30 26 88 14 3E 5F A2 9A 7C D8 4D 03 54 C8 F6 F7 05 67 9D 9E 0F AE AF B1 05 9E 64 73 23 5D E1 2F F8 2B 9A 43 A9 63 98 CB 6E 15 64 64 0C 26 F2 02 CF 4E F2 D0 6F 0E 29 AF 99 57 7E 7E 43 C6 DE F9 FE 52 7E B4 D3 04 49 B8 78 62 BE 7B 6F 73 5B 3A 15 3F 51 1D 6F 67 7A 48 7B 31 68 18 2E";
                List<CertAuthTmp> AddCertAuthList = new List<CertAuthTmp>();
                List<CertTmp> AddCertList = new List<CertTmp>();


                CertTmp pp1 = new CertTmp();
                pp1.CertDataid = "1";
                pp1.CertDataHexStr = cert1;
                pp1.isSend = "1";
                AddCertList.Add(pp1);

                CertTmp pp2 = new CertTmp();
                pp2.CertDataid = "2";
                pp2.CertDataHexStr = cert2;
                pp2.isSend = "1";
                AddCertList.Add(pp2);


                CertTmp pp3 = new CertTmp();
                pp3.CertDataid = "3";
                pp3.CertDataHexStr = cert3;
                pp3.isSend = "1";
                AddCertList.Add(pp3);


                CertTmp pp4 = new CertTmp();
                pp4.CertDataid = "4";
                pp4.CertDataHexStr = cert4;
                pp4.isSend = "1";
                AddCertList.Add(pp4);

                CertTmp pp5 = new CertTmp();
                pp5.CertDataid = "5";
                pp5.CertDataHexStr = cert5;
                pp5.isSend = "1";
                AddCertList.Add(pp5);

                DealCert2Global(AddCertList);


                CertAuthTmp pp = new CertAuthTmp();
                pp.CertAuthDataid = "5";
                pp.CertAuthDataHexStr = auth;
                pp.isSend = "1";
                AddCertAuthList.Add(pp);

                DealCertAuth2Global(AddCertAuthList);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }

}


