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

        private CertAuthGlobal_ _CertAuthGlobal;
        private EBMIndexGlobal_ _EBMIndexGlobal;
        private EBMConfigureGlobal_ _EBMConfigureGlobal;
        private DailyBroadcastGlobal_ _DailyBroadcastGlobal;
        private EBContentGlobal_ _EBContentGlobal;
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
                SingletonInfo.GetInstance().cramblertype = ini.ReadValue("Scrambler", "ScramblerType");
                SingletonInfo.GetInstance().IsGXProtocol = ini.ReadValue("ProtocolType", "ProtocolType") == "1" ? true : false;//“1”表示广西协议 2表示国标

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
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
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
                m_mq.uri = "tcp://" + ini.ReadValue("MQ", "MQIP") + ":" + ini.ReadValue("MQ", "MQPORT");
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
            int interval = SingletonInfo.GetInstance().TimerInterval*1000;
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
                SingleTimeServerSend(DateTime.Now);  
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
            IsStartStream = false;
            EbmStream = new EBMStream();
            InitTable();
            InitEBStream();
            calcel = new Calcle();
            calcel.MyEvent += new Calcle.MyDelegate(NetErrorDeal);
            InitStreamTableNew();
            InitTCPServer();


            Gtoken = new object();
            ConnectMQServer();
            dataHelper = new DataDealHelper();

            dataHelperreal = new DataHelper();

            _CertAuthGlobal = new CertAuthGlobal_();
            _EBMIndexGlobal = new EBMIndexGlobal_();
            _EBMConfigureGlobal = new EBMConfigureGlobal_();
            _DailyBroadcastGlobal = new DailyBroadcastGlobal_();
            _EBContentGlobal = new EBContentGlobal_();
            DataDealHelper.MyEvent += new DataDealHelper.MyDelegate(GlobalDataDeal);
            ProcessBegin();
            InitTimer();   // 测试注释  避免调试干扰  20180806
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
                if (_EBMIndexGlobal.ListEbIndex != null)
                {
                    string[] IndexItemIDArray = IndexItemIDstr.Split(',');
                    foreach (string  item in IndexItemIDArray)
                    {
                        List<EBMIndex_> tmp = _EBMIndexGlobal.ListEbIndex.FindAll(s => s.IndexItemID.StartsWith(item));
                        if (tmp!=null)
                        {
                            foreach (var ite in tmp)
                            {
                                _EBMIndexGlobal.ListEbIndex.Remove(ite);
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
                if (_EBMIndexGlobal.ListEbIndex != null)
                {
                    //去同向
                    EBMIndex_ tmp = _EBMIndexGlobal.ListEbIndex.Find(s => s.IndexItemID.Equals(modify.IndexItemID));
                    if (tmp != null)
                    {
                        string[] resource_code = modify.Data.Split(',');
                        foreach (string item in resource_code)
                        {
                            tmp.List_EBM_resource_code.Add(item);
                        }
                    }
                }
                EbmStream.StopStreaming();
                EbmStream.StartStreaming();
                EbmStream.EB_Index_Table = GetEBIndexTable(ref EB_Index_Table) ? EB_Index_Table : null;
                EbmStream.SignatureCallbackRef = new EBMStream.SignatureCallBackDelegateRef(calcel.SignatureFunc);
                EbMStream.Initialization();
            }
            UpdateDataTextNew((object)1);
        }


        private void DelAreaEBMIndex2Global(ModifyEBMIndex modify)
        {
            lock (Gtoken)
            {
                if (_EBMIndexGlobal.ListEbIndex != null)
                {
                    //去同向
                    EBMIndex_ tmp = _EBMIndexGlobal.ListEbIndex.Find(s => s.IndexItemID.Equals(modify.IndexItemID));
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
            UpdateDataTextNew((object)1);
        }

        private void DealEBMIndex2Global(EBMIndexTmp EBMIndex)
        {

            try
            {
                lock (Gtoken)
                {
                    if (_EBMIndexGlobal.ListEbIndex != null)
                    {
                        //去同向
                        EBMIndex_ tmp = _EBMIndexGlobal.ListEbIndex.Find(s => s.IndexItemID.Equals(EBMIndex.IndexItemID));
                        if (tmp != null)
                        {
                            _EBMIndexGlobal.ListEbIndex.Remove(tmp);
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
                    ///

                    string[] List_EBM_resource_codeArray = EBMIndex.List_EBM_resource_code.Split(',');
                    //gan
                    if (SingletonInfo.GetInstance().IsGXProtocol)
                    {
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
                                    List_EBM_resource_codeArray[i] = "0612"+List_EBM_resource_codeArray[i] + "00";
                                    break;
                            }
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

                    if (index.DesFlag)
                        index.DeliverySystemDescriptor = GetDataDSD(EBMIndex.DeliverySystemDescriptor, EBMIndex.descriptor_tag);
                    if (index.BL_details_channel_indicate)
                    {

                        List<ProgramStreamInfotmp> List_ProgramStreamInfotmp = new List<ProgramStreamInfotmp>();//S_elementary_PID 中有“，”时，临时加入项
                        int List_ProgramStreamInfoLength = EBMIndex.List_ProgramStreamInfo.Count;//详情频道节目流信息列表长度
                        for (int i = 0; i < List_ProgramStreamInfoLength; i++)
                        {
                            string S_elementary_PID = EBMIndex.List_ProgramStreamInfo[i].S_elementary_PID;

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

                    if (_EBMIndexGlobal.ListEbIndex == null)
                    {
                        _EBMIndexGlobal.ListEbIndex = new List<EBMIndex_>();
                    }
                    _EBMIndexGlobal.ListEbIndex.Add(index);
                    EbmStream.EB_Index_Table = GetEBIndexTable(ref EB_Index_Table) ? EB_Index_Table : null;
                    EbMStream.Initialization();
                }
              //  UpdateDataTextNew((object)1);
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
                            List<Reback_> listRB = (List<Reback_>)op.Data;
                            DealReback(listRB);
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
                            if (SingletonInfo.GetInstance().IsGXProtocol)
                            {
                                List<ContentRealMoniterGX_> listCRMGX = (List<ContentRealMoniterGX_>)op.Data;
                                DealContentRealMoniterGX(listCRMGX);
                            }
                            else
                            {
                                List<ContentRealMoniter_> listCRM = (List<ContentRealMoniter_>)op.Data;
                                DealContentRealMoniter(listCRM);
                            }
                            
                            break;
                        case "106":
                            List<StatusRetback_> listSR = (List<StatusRetback_>)op.Data;
                            DealStatusRetback(listSR);
                            break;
                        case "240":
                            List<SoftwareUpGrade_> listSUG = (List<SoftwareUpGrade_>)op.Data;
                            DealSoftwareUpGrade(listSUG);
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
                if (_EBMConfigureGlobal.ListTimeService != null)
                {
                    //去同项
                    foreach (TimeService_ item in listTS)
                    {
                        TimeService_ tmp = _EBMConfigureGlobal.ListTimeService.Find(s => s.ItemID.Equals(item.ItemID));
                        if (tmp != null)
                        {
                            _EBMConfigureGlobal.ListTimeService.Remove(tmp);
                        }
                    }
                }
                else
                {
                    _EBMConfigureGlobal.ListTimeService = new List<TimeService_>();
                }

                //增新项
                foreach (TimeService_ item in listTS)
                {
                    _EBMConfigureGlobal.ListTimeService.Add(item);
                }
             
                EbmStream.EB_Configure_Table = GetConfigureTable(ref EB_Configure_Table, false) ? EB_Configure_Table : null;


                EbMStream.Initialization();
            }
            UpdateDataTextNew((object)3);
            #region 删除记录 
            foreach (TimeService_ item in listTS)
            {
                _EBMConfigureGlobal.ListTimeService.Remove(item);
            }
            #endregion
        }

        private void DealSetAddress(List<SetAddress_> listSA)
        {
            lock (Gtoken)
            {
                if (_EBMConfigureGlobal.ListSetAddress != null)
                {
                    //去同项
                    foreach (SetAddress_ item in listSA)
                    {
                        SetAddress_ tmp = _EBMConfigureGlobal.ListSetAddress.Find(s => s.ItemID.Equals(item.ItemID));
                        if (tmp != null)
                        {
                            _EBMConfigureGlobal.ListSetAddress.Remove(tmp);
                        }
                    }
                }
                else
                {
                    _EBMConfigureGlobal.ListSetAddress = new List<SetAddress_>();
                }
                //增新项
                foreach (SetAddress_ item in listSA)
                {
                    _EBMConfigureGlobal.ListSetAddress.Add(item);
                }

                EbmStream.EB_Configure_Table = GetConfigureTable(ref EB_Configure_Table, false) ? EB_Configure_Table : null;
                EbMStream.Initialization();
            }
            UpdateDataTextNew((object)3);

            #region  删除记录

            foreach (SetAddress_ item in listSA)
            {
                _EBMConfigureGlobal.ListSetAddress.Remove(item);
            }
            #endregion
        }

        private void DealWorkMode(List<WorkMode_> listWM)
        {
            lock (Gtoken)
            {
                if (_EBMConfigureGlobal.ListWorkMode != null)
                {
                    //去同项
                    foreach (WorkMode_ item in listWM)
                    {
                        WorkMode_ tmp = _EBMConfigureGlobal.ListWorkMode.Find(s => s.ItemID.Equals(item.ItemID));
                        if (tmp != null)
                        {
                            _EBMConfigureGlobal.ListWorkMode.Remove(tmp);
                        }
                    }
                }
                else
                {
                    _EBMConfigureGlobal.ListWorkMode = new List<WorkMode_>();
                }

                //增新项
                foreach (WorkMode_ item in listWM)
                {
                    _EBMConfigureGlobal.ListWorkMode.Add(item);
                }

                EbmStream.EB_Configure_Table = GetConfigureTable(ref EB_Configure_Table, false) ? EB_Configure_Table : null;
                EbMStream.Initialization();
            }
            UpdateDataTextNew((object)3);

            #region 删除记录
            foreach (WorkMode_ item in listWM)
            {
                _EBMConfigureGlobal.ListWorkMode.Remove(item);
            }

            #endregion
        }

        private void DealMainFrequency(List<MainFrequency_> listMF)
        {

            lock (Gtoken)
            {
                if (_EBMConfigureGlobal.ListMainFrequency != null)
                {
                    //去同项
                    foreach (MainFrequency_ item in listMF)
                    {
                        MainFrequency_ tmp = _EBMConfigureGlobal.ListMainFrequency.Find(s => s.ItemID.Equals(item.ItemID));
                        if (tmp != null)
                        {
                            _EBMConfigureGlobal.ListMainFrequency.Remove(tmp);
                        }
                    }
                }
                else
                {
                    _EBMConfigureGlobal.ListMainFrequency = new List<MainFrequency_>();
                }

                //增新项
                foreach (MainFrequency_ item in listMF)
                {
                    _EBMConfigureGlobal.ListMainFrequency.Add(item);
                }

                EbmStream.EB_Configure_Table = GetConfigureTable(ref EB_Configure_Table, false) ? EB_Configure_Table : null;
                EbMStream.Initialization();
            }
            UpdateDataTextNew((object)3);

            #region 删除记录
            foreach (MainFrequency_ item in listMF)
            {
                _EBMConfigureGlobal.ListMainFrequency.Remove(item);
            }
            #endregion
        }

        private void DealReback(List<Reback_> listRB)
        {

            lock (Gtoken)
            {
                if (_EBMConfigureGlobal.ListReback != null)
                {
                    //去同项
                    foreach (Reback_ item in listRB)
                    {
                        Reback_ tmp = _EBMConfigureGlobal.ListReback.Find(s => s.ItemID.Equals(item.ItemID));
                        if (tmp != null)
                        {
                            _EBMConfigureGlobal.ListReback.Remove(tmp);
                        }

                    }

                }
                else
                {
                    _EBMConfigureGlobal.ListReback = new List<Reback_>();
                }
                //增新项
                foreach (Reback_ item in listRB)
                {
                    _EBMConfigureGlobal.ListReback.Add(item);
                }

                EbmStream.EB_Configure_Table = GetConfigureTable(ref EB_Configure_Table, false) ? EB_Configure_Table : null;
                EbMStream.Initialization();
            }
            UpdateDataTextNew((object)3);
            #region 删除记录
            //增新项
            foreach (Reback_ item in listRB)
            {
                _EBMConfigureGlobal.ListReback.Remove(item);
            }
            #endregion
        }

        private void DealDefaltVolume(List<DefaltVolume_> listDV)
        {

            lock (Gtoken)
            {
                if (_EBMConfigureGlobal.ListDefaltVolume != null)
                {
                    //去同项
                    foreach (DefaltVolume_ item in listDV)
                    {
                        DefaltVolume_ tmp = _EBMConfigureGlobal.ListDefaltVolume.Find(s => s.ItemID.Equals(item.ItemID));
                        if (tmp != null)
                        {
                            _EBMConfigureGlobal.ListDefaltVolume.Remove(tmp);
                        }

                    }

                }
                else
                {
                    _EBMConfigureGlobal.ListDefaltVolume = new List<DefaltVolume_>();
                }
                //增新项
                foreach (DefaltVolume_ item in listDV)
                {
                    _EBMConfigureGlobal.ListDefaltVolume.Add(item);
                }

                EbmStream.EB_Configure_Table = GetConfigureTable(ref EB_Configure_Table, false) ? EB_Configure_Table : null;
                EbMStream.Initialization();
            }
            UpdateDataTextNew((object)3);

            #region 删除记录
            foreach (DefaltVolume_ item in listDV)
            {
                _EBMConfigureGlobal.ListDefaltVolume.Remove(item);
            }
            #endregion
        }

        private void DealRebackPeriod(List<RebackPeriod_> listRP)
        {

            lock (Gtoken)
            {
                if (_EBMConfigureGlobal.ListRebackPeriod != null)
                {
                    //去同项
                    foreach (RebackPeriod_ item in listRP)
                    {
                        RebackPeriod_ tmp = _EBMConfigureGlobal.ListRebackPeriod.Find(s => s.ItemID.Equals(item.ItemID));
                        if (tmp != null)
                        {
                            _EBMConfigureGlobal.ListRebackPeriod.Remove(tmp);
                        }

                    }

                }
                else
                {
                    _EBMConfigureGlobal.ListRebackPeriod = new List<RebackPeriod_>();
                }
                //增新项
                foreach (RebackPeriod_ item in listRP)
                {
                    _EBMConfigureGlobal.ListRebackPeriod.Add(item);
                }

                EbmStream.EB_Configure_Table = GetConfigureTable(ref EB_Configure_Table, false) ? EB_Configure_Table : null;
                EbMStream.Initialization();
            }
            UpdateDataTextNew((object)3);
            #region 删除记录
            foreach (RebackPeriod_ item in listRP)
            {
                _EBMConfigureGlobal.ListRebackPeriod.Remove(item);
            }
            #endregion
        }

        private void DealContentMoniterRetback(List<ContentMoniterRetback_> listCMR)
        {

            lock (Gtoken)
            {
                if (_EBMConfigureGlobal.ListContentMoniterRetback != null)
                {
                    //去同项
                    foreach (ContentMoniterRetback_ item in listCMR)
                    {
                        ContentMoniterRetback_ tmp = _EBMConfigureGlobal.ListContentMoniterRetback.Find(s => s.ItemID.Equals(item.ItemID));
                        if (tmp != null)
                        {
                            _EBMConfigureGlobal.ListContentMoniterRetback.Remove(tmp);
                        }

                    }

                }
                else
                {
                    _EBMConfigureGlobal.ListContentMoniterRetback = new List<ContentMoniterRetback_>();
                }

                //增新项
                foreach (ContentMoniterRetback_ item in listCMR)
                {
                    _EBMConfigureGlobal.ListContentMoniterRetback.Add(item);
                }

                EbmStream.EB_Configure_Table = GetConfigureTable(ref EB_Configure_Table, false) ? EB_Configure_Table : null;
                EbMStream.Initialization();
            }
            UpdateDataTextNew((object)3);
            #region 删除记录

            foreach (ContentMoniterRetback_ item in listCMR)
            {
                _EBMConfigureGlobal.ListContentMoniterRetback.Remove(item);
            }
            #endregion
        }

        private void DealContentRealMoniter(List<ContentRealMoniter_> listCRM)
        {

            lock (Gtoken)
            {
                if (_EBMConfigureGlobal.ListContentRealMoniter != null)
                {
                    //去同项
                    foreach (ContentRealMoniter_ item in listCRM)
                    {
                        ContentRealMoniter_ tmp = _EBMConfigureGlobal.ListContentRealMoniter.Find(s => s.ItemID.Equals(item.ItemID));
                        if (tmp != null)
                        {
                            _EBMConfigureGlobal.ListContentRealMoniter.Remove(tmp);
                        }

                    }

                }
                else
                {
                    _EBMConfigureGlobal.ListContentRealMoniter = new List<ContentRealMoniter_>();
                }
                //增新项
                foreach (ContentRealMoniter_ item in listCRM)
                {
                    _EBMConfigureGlobal.ListContentRealMoniter.Add(item);
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
                if (_EBMConfigureGlobal.ListContentRealMoniterGX != null)
                {
                    //去同项
                    foreach (ContentRealMoniterGX_ item in listCRM)
                    {
                        ContentRealMoniterGX_ tmp = _EBMConfigureGlobal.ListContentRealMoniterGX.Find(s => s.ItemID.Equals(item.ItemID));
                        if (tmp != null)
                        {
                            _EBMConfigureGlobal.ListContentRealMoniterGX.Remove(tmp);
                        }

                    }

                }
                else
                {
                    _EBMConfigureGlobal.ListContentRealMoniterGX = new List<ContentRealMoniterGX_>();
                }
                //增新项
                foreach (ContentRealMoniterGX_ item in listCRM)
                {
                    _EBMConfigureGlobal.ListContentRealMoniterGX.Add(item);
                }

                EbmStream.EB_Configure_Table = GetConfigureTable(ref EB_Configure_Table, false) ? EB_Configure_Table : null;
                EbMStream.Initialization();
            }
            UpdateDataTextNew((object)3);

            //增新项
            foreach (ContentRealMoniterGX_ item in listCRM)
            {
                _EBMConfigureGlobal.ListContentRealMoniterGX.Remove(item);
            }
        }

        private void DealStatusRetback(List<StatusRetback_> listSR)
        {

            lock (Gtoken)
            {
                if (_EBMConfigureGlobal.ListStatusRetback != null)
                {
                    //去同项
                    foreach (StatusRetback_ item in listSR)
                    {
                        StatusRetback_ tmp = _EBMConfigureGlobal.ListStatusRetback.Find(s => s.ItemID.Equals(item.ItemID));
                        if (tmp != null)
                        {
                            _EBMConfigureGlobal.ListStatusRetback.Remove(tmp);
                        }

                    }

                }
                else
                {
                    _EBMConfigureGlobal.ListStatusRetback = new List<StatusRetback_>();
                }
                //增新项
                foreach (StatusRetback_ item in listSR)
                {
                    _EBMConfigureGlobal.ListStatusRetback.Add(item);
                }

                EbmStream.EB_Configure_Table = GetConfigureTable(ref EB_Configure_Table, false) ? EB_Configure_Table : null;
                EbMStream.Initialization();
            }
            UpdateDataTextNew((object)3);
        }

        private void DealSoftwareUpGrade(List<SoftwareUpGrade_> listSUG)
        {

            lock (Gtoken)
            {
                if (_EBMConfigureGlobal.ListSoftwareUpGrade != null)
                {
                    //去同项
                    foreach (SoftwareUpGrade_ item in listSUG)
                    {
                        SoftwareUpGrade_ tmp = _EBMConfigureGlobal.ListSoftwareUpGrade.Find(s => s.ItemID.Equals(item.ItemID));
                        if (tmp != null)
                        {
                            _EBMConfigureGlobal.ListSoftwareUpGrade.Remove(tmp);
                        }

                    }

                }
                else
                {
                    _EBMConfigureGlobal.ListSoftwareUpGrade = new List<SoftwareUpGrade_>();
                }

                //增新项
                foreach (SoftwareUpGrade_ item in listSUG)
                {
                    _EBMConfigureGlobal.ListSoftwareUpGrade.Add(item);
                }

                EbmStream.EB_Configure_Table = GetConfigureTable(ref EB_Configure_Table, false) ? EB_Configure_Table : null;
                EbMStream.Initialization();
            }
            UpdateDataTextNew((object)3);

            #region 删除记录

            foreach (SoftwareUpGrade_ item in listSUG)
            {
                _EBMConfigureGlobal.ListSoftwareUpGrade.Remove(item);
            }
            #endregion
        }

        private void DealRdsConfig(List<RdsConfig_> listRC)
        {

            lock (Gtoken)
            {
                if (_EBMConfigureGlobal.ListRdsConfig != null)
                {
                    //去同项
                    foreach (RdsConfig_ item in listRC)
                    {
                        RdsConfig_ tmp = _EBMConfigureGlobal.ListRdsConfig.Find(s => s.ItemID.Equals(item.ItemID));
                        if (tmp != null)
                        {
                            _EBMConfigureGlobal.ListRdsConfig.Remove(tmp);
                        }
                    }
                }
                else
                {

                    _EBMConfigureGlobal.ListRdsConfig = new List<RdsConfig_>();
                }
                //增新项
                foreach (RdsConfig_ item in listRC)
                {
                    _EBMConfigureGlobal.ListRdsConfig.Add(item);
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
                        if (_EBMConfigureGlobal.ListTimeService != null)
                        {
                            if (DelItemList.Count > 0)
                            {
                                foreach (string item in DelItemList)
                                {
                                    TimeService_ tmp = _EBMConfigureGlobal.ListTimeService.Find(s => s.ItemID.Equals(item));
                                    _EBMConfigureGlobal.ListTimeService.Remove(tmp);
                                }
                            }
                        }
                    }
                    break;
                case "2":
                    lock (Gtoken)
                    {
                        if (_EBMConfigureGlobal.ListSetAddress != null)
                        {
                            if (DelItemList.Count > 0)
                            {
                                foreach (string item in DelItemList)
                                {
                                    SetAddress_ tmp = _EBMConfigureGlobal.ListSetAddress.Find(s => s.ItemID.Equals(item));
                                    _EBMConfigureGlobal.ListSetAddress.Remove(tmp);
                                }
                            }
                        }

                    }
                    break;
                case "3":
                    lock (Gtoken)
                    {
                        if (_EBMConfigureGlobal.ListWorkMode != null)
                        {
                            if (DelItemList.Count > 0)
                            {
                                foreach (string item in DelItemList)
                                {
                                    WorkMode_ tmp = _EBMConfigureGlobal.ListWorkMode.Find(s => s.ItemID.Equals(item));
                                    _EBMConfigureGlobal.ListWorkMode.Remove(tmp);
                                }
                            }
                        }

                    }
                    break;
                case "4":
                    lock (Gtoken)
                    {
                        if (_EBMConfigureGlobal.ListMainFrequency != null)
                        {
                            if (DelItemList.Count > 0)
                            {
                                foreach (string item in DelItemList)
                                {
                                    MainFrequency_ tmp = _EBMConfigureGlobal.ListMainFrequency.Find(s => s.ItemID.Equals(item));
                                    _EBMConfigureGlobal.ListMainFrequency.Remove(tmp);
                                }
                            }
                        }

                    }
                    break;
                case "5":
                    lock (Gtoken)
                    {
                        if (_EBMConfigureGlobal.ListReback != null)
                        {
                            if (DelItemList.Count > 0)
                            {
                                foreach (string item in DelItemList)
                                {
                                    Reback_ tmp = _EBMConfigureGlobal.ListReback.Find(s => s.ItemID.Equals(item));
                                    _EBMConfigureGlobal.ListReback.Remove(tmp);
                                }
                            }
                        }

                    }
                    break;
                case "6":
                    lock (Gtoken)
                    {
                        if (_EBMConfigureGlobal.ListDefaltVolume != null)
                        {
                            if (DelItemList.Count > 0)
                            {
                                foreach (string item in DelItemList)
                                {
                                    DefaltVolume_ tmp = _EBMConfigureGlobal.ListDefaltVolume.Find(s => s.ItemID.Equals(item));
                                    _EBMConfigureGlobal.ListDefaltVolume.Remove(tmp);
                                }
                            }
                        }

                    }
                    break;
                case "7":
                    lock (Gtoken)
                    {
                        if (_EBMConfigureGlobal.ListRebackPeriod != null)
                        {
                            if (DelItemList.Count > 0)
                            {
                                foreach (string item in DelItemList)
                                {
                                    RebackPeriod_ tmp = _EBMConfigureGlobal.ListRebackPeriod.Find(s => s.ItemID.Equals(item));
                                    _EBMConfigureGlobal.ListRebackPeriod.Remove(tmp);
                                }
                            }
                        }

                    }
                    break;
                case "104":
                    lock (Gtoken)
                    {
                        if (_EBMConfigureGlobal.ListContentMoniterRetback != null)
                        {
                            if (DelItemList.Count > 0)
                            {
                                foreach (string item in DelItemList)
                                {
                                    ContentMoniterRetback_ tmp = _EBMConfigureGlobal.ListContentMoniterRetback.Find(s => s.ItemID.Equals(item));
                                    _EBMConfigureGlobal.ListContentMoniterRetback.Remove(tmp);
                                }
                            }
                        }

                    }
                    break;
                case "105":
                    lock (Gtoken)
                    {
                        if (_EBMConfigureGlobal.ListContentRealMoniter != null)
                        {
                            if (DelItemList.Count > 0)
                            {
                                foreach (string item in DelItemList)
                                {
                                    ContentRealMoniter_ tmp = _EBMConfigureGlobal.ListContentRealMoniter.Find(s => s.ItemID.Equals(item));
                                    _EBMConfigureGlobal.ListContentRealMoniter.Remove(tmp);
                                }
                            }
                        }

                    }
                    break;
                case "106":
                    lock (Gtoken)
                    {
                        if (_EBMConfigureGlobal.ListStatusRetback != null)
                        {
                            if (DelItemList.Count > 0)
                            {
                                foreach (string item in DelItemList)
                                {
                                    StatusRetback_ tmp = _EBMConfigureGlobal.ListStatusRetback.Find(s => s.ItemID.Equals(item));
                                    _EBMConfigureGlobal.ListStatusRetback.Remove(tmp);
                                }
                            }
                        }

                    }
                    break;
                case "240":
                    lock (Gtoken)
                    {
                        if (_EBMConfigureGlobal.ListSoftwareUpGrade != null)
                        {
                            if (DelItemList.Count > 0)
                            {
                                foreach (string item in DelItemList)
                                {
                                    SoftwareUpGrade_ tmp = _EBMConfigureGlobal.ListSoftwareUpGrade.Find(s => s.ItemID.Equals(item));
                                    _EBMConfigureGlobal.ListSoftwareUpGrade.Remove(tmp);
                                }
                            }
                        }

                    }
                    break;
                case "8":
                    lock (Gtoken)
                    {
                        if (_EBMConfigureGlobal.ListRdsConfig != null)
                        {
                            if (DelItemList.Count > 0)
                            {
                                foreach (string item in DelItemList)
                                {
                                    RdsConfig_ tmp = _EBMConfigureGlobal.ListRdsConfig.Find(s => s.ItemID.Equals(item));
                                    _EBMConfigureGlobal.ListRdsConfig.Remove(tmp);
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
                if (_DailyBroadcastGlobal.ListChangeProgram != null)
                {
                    //去同项
                    foreach (ChangeProgram_ item in listCP)
                    {
                        ChangeProgram_ tmp = _DailyBroadcastGlobal.ListChangeProgram.Find(s => s.ItemID.Equals(item.ItemID));
                        if (tmp != null)
                        {
                            _DailyBroadcastGlobal.ListChangeProgram.Remove(tmp);
                        }

                    }

                }
                else
                {
                    _DailyBroadcastGlobal.ListChangeProgram = new List<ChangeProgram_>();
                }
                //增新项
                foreach (ChangeProgram_ item in listCP)
                {
                    _DailyBroadcastGlobal.ListChangeProgram.Add(item);
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
                if (_DailyBroadcastGlobal.ListPlayCtrl != null)
                {
                    //去同项
                    foreach (PlayCtrl_ item in listPC)
                    {
                        PlayCtrl_ tmp = _DailyBroadcastGlobal.ListPlayCtrl.Find(s => s.ItemID.Equals(item.ItemID));
                        if (tmp != null)
                        {
                            _DailyBroadcastGlobal.ListPlayCtrl.Remove(tmp);
                        }

                    }

                }
                else
                {
                    _DailyBroadcastGlobal.ListPlayCtrl = new List<PlayCtrl_>();
                }

                //增新项
                foreach (PlayCtrl_ item in listPC)
                {
                    _DailyBroadcastGlobal.ListPlayCtrl.Add(item);
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
                if (_DailyBroadcastGlobal.ListChangeProgram != null)
                {
                    //去同项
                    foreach (OutSwitch_ item in listOS)
                    {
                        OutSwitch_ tmp = _DailyBroadcastGlobal.ListOutSwitch.Find(s => s.ItemID.Equals(item.ItemID));
                        if (tmp != null)
                        {
                            _DailyBroadcastGlobal.ListOutSwitch.Remove(tmp);
                        }

                    }

                }
                else
                {
                    _DailyBroadcastGlobal.ListOutSwitch = new List<OutSwitch_>();
                }

                //增新项
                foreach (OutSwitch_ item in listOS)
                {
                    _DailyBroadcastGlobal.ListOutSwitch.Add(item);
                }

                EbmStream.Daily_Broadcast_Table = GetDailyBroadcastTable(ref Daily_Broadcast_Table) ? Daily_Broadcast_Table : null;
                EbMStream.Initialization();
            }
            UpdateDataTextNew((object)4);
        }

        private void DealRdsTransfer(List<RdsTransfer_> listRT)
        {

            lock (Gtoken)
            {
                if (_DailyBroadcastGlobal.ListRdsTransfer != null)
                {
                    //去同项
                    foreach (RdsTransfer_ item in listRT)
                    {
                        RdsTransfer_ tmp = _DailyBroadcastGlobal.ListRdsTransfer.Find(s => s.ItemID.Equals(item.ItemID));
                        if (tmp != null)
                        {
                            _DailyBroadcastGlobal.ListRdsTransfer.Remove(tmp);
                        }

                    }

                }
                else
                {
                    _DailyBroadcastGlobal.ListRdsTransfer = new List<RdsTransfer_>();
                }

                //增新项
                foreach (RdsTransfer_ item in listRT)
                {
                    _DailyBroadcastGlobal.ListRdsTransfer.Add(item);
                }

                EbmStream.Daily_Broadcast_Table = GetDailyBroadcastTable(ref Daily_Broadcast_Table) ? Daily_Broadcast_Table : null;
                EbMStream.Initialization();
            }
            UpdateDataTextNew((object)4);
        }

        private void DelDailyBroadcast2Global(OperatorData op)
        {
            List<string> DelItemList = new List<string>(op.Data.ToString().Split(','));
            switch (op.ModuleType)
            {
                case "1":
                    lock (Gtoken)
                    {
                        if (_DailyBroadcastGlobal.ListChangeProgram  != null)
                        {
                            if (DelItemList.Count > 0)
                            {
                                foreach (string item in DelItemList)
                                {
                                    ChangeProgram_ tmp = _DailyBroadcastGlobal.ListChangeProgram.Find(s => s.ItemID.Equals(item));
                                    _DailyBroadcastGlobal.ListChangeProgram.Remove(tmp);
                                }
                            }
                        }

                    }
                    break;
                case "3":
                    lock (Gtoken)
                    {
                        if (_DailyBroadcastGlobal.ListPlayCtrl != null)
                        {
                            if (DelItemList.Count > 0)
                            {
                                foreach (string item in DelItemList)
                                {
                                    PlayCtrl_ tmp = _DailyBroadcastGlobal.ListPlayCtrl.Find(s => s.ItemID.Equals(item));
                                    _DailyBroadcastGlobal.ListPlayCtrl.Remove(tmp);
                                }
                            }
                        }

                    }
                    break;
                case "4":
                    lock (Gtoken)
                    {
                        if (_DailyBroadcastGlobal.ListOutSwitch != null)
                        {
                            if (DelItemList.Count > 0)
                            {
                                foreach (string item in DelItemList)
                                {
                                    OutSwitch_ tmp = _DailyBroadcastGlobal.ListOutSwitch.Find(s => s.ItemID.Equals(item));
                                    _DailyBroadcastGlobal.ListOutSwitch.Remove(tmp);
                                }
                            }
                        }

                    }
                    break;
                case "5":
                    lock (Gtoken)
                    {
                        if (_DailyBroadcastGlobal.ListRdsTransfer != null)
                        {
                            if (DelItemList.Count > 0)
                            {
                                foreach (string item in DelItemList)
                                {
                                    RdsTransfer_ tmp = _DailyBroadcastGlobal.ListRdsTransfer.Find(s => s.ItemID.Equals(item));
                                    _DailyBroadcastGlobal.ListRdsTransfer.Remove(tmp);
                                }
                            }
                        }

                    }
                    break;

            }
            EbmStream.Daily_Broadcast_Table = GetDailyBroadcastTable(ref Daily_Broadcast_Table) ? Daily_Broadcast_Table : null;
            EbMStream.Initialization();
            UpdateDataTextNew((object)4);
        }
        #endregion

        #region CertAuth 数据处理
        private void DelCertAuth2Global(string delstr)
        {
            List<string> DelCertAuthDataIdList = new List<string>(delstr.Split(','));//string[] 转List<string>
            lock (Gtoken)
            {
                if (_CertAuthGlobal.list_CertAuth != null)
                {
                    //去同向
                    if (DelCertAuthDataIdList.Count > 0)
                    {
                        foreach (string item in DelCertAuthDataIdList)
                        {
                            CertAuth_ tmp = _CertAuthGlobal.list_CertAuth.Find(s => s.CertAuthDataId.Equals(item));
                            _CertAuthGlobal.list_CertAuth.Remove(tmp);
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
                if (_CertAuthGlobal.list_Cert != null)
                {
                    //去同向
                    if (DelCertDataIdList.Count > 0)
                    {
                        foreach (string item in DelCertDataIdList)
                        {
                            Cert_ tmp = _CertAuthGlobal.list_Cert.Find(s => s.CertDataId.Equals(item));
                            _CertAuthGlobal.list_Cert.Remove(tmp);
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
                if (_CertAuthGlobal.list_CertAuth != null)
                {
                    //查同项
                    foreach (CertAuthTmp item in AddCertAuthList)
                    {
                        foreach (var itemGlobal in _CertAuthGlobal.list_CertAuth)
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
                            CertAuth_ tmp = _CertAuthGlobal.list_CertAuth.Find(s => s.CertAuthDataId.Equals(item));
                            _CertAuthGlobal.list_CertAuth.Remove(tmp);
                        }
                    }
                }

                //增加新项

                foreach (CertAuthTmp item in AddCertAuthList)
                {
                    CertAuth_ add = new CertAuth_();
                    add.CertAuthDataId = item.CertAuthDataid;
                    add.CertAuth_data = item.CertAuthDataHexStr;
                    add.SendState = true;
                    add.Tag = 1;
                    _CertAuthGlobal.list_CertAuth.Add(add);
                }
           
                EbmStream.EB_CertAuth_Table = GetCertAuthTable(ref EB_CertAuth_Table) ? EB_CertAuth_Table : null;
               EbMStream.Initialization();
            }
            UpdateDataTextNew((object)2);
        }

        private void DealCert2Global(List<CertTmp> AddCertList)
        {
            List<string> DelCertDataIdList = new List<string>();
            lock (Gtoken)
            {
                if (_CertAuthGlobal.list_Cert != null)
                {
                    //查同项
                    foreach (CertTmp item in AddCertList)
                    {
                        foreach (var itemGlobal in _CertAuthGlobal.list_Cert)
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
                            Cert_ tmp = _CertAuthGlobal.list_Cert.Find(s => s.CertDataId.Equals(item));
                            _CertAuthGlobal.list_Cert.Remove(tmp);
                        }
                    }
                }
                else
                {
                    _CertAuthGlobal.list_Cert = new List<Cert_>();
                }

                //增加新项

                foreach (CertTmp item in AddCertList)
                {
                    Cert_ add = new Cert_();
                    add.CertDataId = item.CertDataid;
                    add.Cert_data = item.CertDataHexStr;
                    add.SendState = true;
                    add.Tag = 1;
                    _CertAuthGlobal.list_Cert.Add(add);
                }
                EbmStream.EB_CertAuth_Table = GetCertAuthTable(ref EB_CertAuth_Table) ? EB_CertAuth_Table : null;
                EbMStream.Initialization();
            }
            UpdateDataTextNew((object)2);
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
                        if (_EBContentGlobal.ListEBContent != null)
                        {
                            //去同项
                            foreach (EBMID_Content item in listEBContent_)
                            {
                                EBMID_Content tmp = _EBContentGlobal.ListEBContent.Find(s => s.EBM_ID.Equals(item.EBM_ID));
                                if (tmp != null)
                                {
                                    _EBContentGlobal.ListEBContent.Remove(tmp);
                                }
                            }
                        }
                        else
                        {
                            _EBContentGlobal.ListEBContent = new List<EBMID_Content>();
                        }
                        //增新项
                        foreach (EBMID_Content item in listEBContent_)
                        {
                            _EBContentGlobal.ListEBContent.Add(item);
                        }
                  
                        EbmStream.list_EB_Content_Table = GetlistContentTable(ref list_EB_Content_Table) ? list_EB_Content_Table : null;
                        EbMStream.Initialization();
                    }
                    UpdateDataTextNew((object)5);


                    #region 删除记录
                    foreach (EBMID_Content item in listEBContent_)
                    {
                        _EBContentGlobal.ListEBContent.Remove(item);
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
                if (_EBContentGlobal.ListEBContent != null)
                {
                    if (DelItemList.Count > 0)
                    {
                        foreach (string item in DelItemList)
                        {
                            EBMID_Content tmp = _EBContentGlobal.ListEBContent.Find(s => s.EBM_ID.Equals(item));
                            _EBContentGlobal.ListEBContent.Remove(tmp);
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
            
            IPEndPoint ie = new IPEndPoint(IPAddress.Parse(ip), port);  //临时方案
            OnorOFFResponse res = SwitchChannel(channelId, ie);
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
                oldTable.list_Cert_data = dataHelper.GetSendCert(_CertAuthGlobal.list_Cert);
                oldTable.list_CertAuth_data = dataHelper.GetSendCertAuth(_CertAuthGlobal.list_CertAuth);
                oldTable.Repeat_times = 0;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool GetEBIndexTable(ref EBIndexTable oldTable)
        {
            try
            {
                List<EBIndex> listEbIndex = dataHelper.GetSendEBMIndex(_EBMIndexGlobal.ListEbIndex);
                oldTable.ListEbIndex = listEbIndex;
              //  oldTable.Repeat_times = _EBMIndexGlobal.Repeat_times;
                oldTable.Repeat_times = 0;//重复发送
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

                foreach (EBMID_Content item in _EBContentGlobal.ListEBContent)
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

            if (_EBMConfigureGlobal.ListReback!=null)
            {
                foreach (var item in _EBMConfigureGlobal.ListReback)
                {
                    TotalConfig_List.Add(item);
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
                BindingCollection<DailyProgram> TotalConfig_List = GetDailyBroadcastCollection(_DailyBroadcastGlobal);
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
                TotalConfig_List = GetConfigureCollection(_EBMConfigureGlobal);
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
                                cmd.Add((d as Reback_).Configure.GetCmd());
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
                                if (SingletonInfo.GetInstance().IsGXProtocol)
                                {
                                    cmd.Add((d as ContentRealMoniterGX_).Configure.GetCmd());

                                }
                                else
                                {
                                    cmd.Add((d as ContentRealMoniter_).Configure.GetCmd());
                                }

                              
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
                BindingCollection<Configure> TotalConfig_List = GetConfigureCollection(_EBMConfigureGlobal);
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
                _CertAuthGlobal.list_Cert = JsonConvert.DeserializeObject<List<Cert_>>(joCertAuth["CertList"].ToString());
                _CertAuthGlobal.list_CertAuth = JsonConvert.DeserializeObject<List<CertAuth_>>(joCertAuth["CertAuthList"].ToString());
                _CertAuthGlobal.Repeat_times = Convert.ToInt32(joCertAuth["RepeatTimes"].ToString());
            }
            else
            {

                _CertAuthGlobal.list_Cert = new List<Cert_>();
                _CertAuthGlobal.list_CertAuth = new List<CertAuth_>();
                _CertAuthGlobal.Repeat_times = 3;
            }

            var joEBMIndex = TableData.TableDataHelper.ReadTable(Enums.TableType.Index);//从配置文件中读取数据
            if (joEBMIndex != null)
            {

                _EBMIndexGlobal.ListEbIndex = JsonConvert.DeserializeObject<List<EBMIndex_>>(joEBMIndex["0"].ToString());

                foreach (var l in _EBMIndexGlobal.ListEbIndex)
                {  //这是什么操作？
                    l.List_EBM_resource_code.RemoveRange(l.List_EBM_resource_code.Count / 2, l.List_EBM_resource_code.Count / 2);
                    l.List_ProgramStreamInfo.RemoveRange(l.List_ProgramStreamInfo.Count / 2, l.List_ProgramStreamInfo.Count / 2);
                }
                _EBMIndexGlobal.Repeat_times = 3;
            }
            else
            {
                _EBMIndexGlobal.ListEbIndex = new List<EBMIndex_>();
                _EBMIndexGlobal.Repeat_times = 3;
            }


            var joConfigure = TableData.TableDataHelper.ReadTable(Enums.TableType.Configure);//从配置文件中读取数据
            if (joConfigure != null)
            {
                _EBMConfigureGlobal.ListContentMoniterRetback = JsonConvert.DeserializeObject<List<ContentMoniterRetback_>>(joConfigure["0"].ToString());
                _EBMConfigureGlobal.ListContentRealMoniter = JsonConvert.DeserializeObject<List<ContentRealMoniter_>>(joConfigure["1"].ToString());
                _EBMConfigureGlobal.ListDefaltVolume = JsonConvert.DeserializeObject<List<DefaltVolume_>>(joConfigure["2"].ToString());
                _EBMConfigureGlobal.ListMainFrequency = JsonConvert.DeserializeObject<List<MainFrequency_>>(joConfigure["3"].ToString());
                _EBMConfigureGlobal.ListRdsConfig = JsonConvert.DeserializeObject<List<RdsConfig_>>(joConfigure["4"].ToString());
                _EBMConfigureGlobal.ListReback = JsonConvert.DeserializeObject<List<Reback_>>(joConfigure["5"].ToString());
                _EBMConfigureGlobal.ListRebackPeriod = JsonConvert.DeserializeObject<List<RebackPeriod_>>(joConfigure["6"].ToString());
                _EBMConfigureGlobal.ListSetAddress = JsonConvert.DeserializeObject<List<SetAddress_>>(joConfigure["7"].ToString());
                _EBMConfigureGlobal.ListSoftwareUpGrade = JsonConvert.DeserializeObject<List<SoftwareUpGrade_>>(joConfigure["8"].ToString());
                _EBMConfigureGlobal.ListStatusRetback = JsonConvert.DeserializeObject<List<StatusRetback_>>(joConfigure["9"].ToString());
                _EBMConfigureGlobal.ListTimeService = JsonConvert.DeserializeObject<List<TimeService_>>(joConfigure["10"].ToString());
                _EBMConfigureGlobal.ListWorkMode = JsonConvert.DeserializeObject<List<WorkMode_>>(joConfigure["11"].ToString());

                _EBMConfigureGlobal.Repeat_times = 3;
            }
            else
            {
                _EBMConfigureGlobal.ListContentMoniterRetback = new List<ContentMoniterRetback_>();
                _EBMConfigureGlobal.ListContentRealMoniter = new List<ContentRealMoniter_>();
                _EBMConfigureGlobal.ListDefaltVolume = new List<DefaltVolume_>();
                _EBMConfigureGlobal.ListMainFrequency = new List<MainFrequency_>();
                _EBMConfigureGlobal.ListRdsConfig = new List<RdsConfig_>();
                _EBMConfigureGlobal.ListReback = new List<Reback_>();
                _EBMConfigureGlobal.ListRebackPeriod = new List<RebackPeriod_>();
                _EBMConfigureGlobal.ListSetAddress = new List<SetAddress_>();
                _EBMConfigureGlobal.ListSoftwareUpGrade = new List<SoftwareUpGrade_>();
                _EBMConfigureGlobal.ListStatusRetback = new List<StatusRetback_>();
                _EBMConfigureGlobal.ListTimeService = new List<TimeService_>();
                _EBMConfigureGlobal.ListWorkMode = new List<WorkMode_>();
                _EBMIndexGlobal.Repeat_times = 3;
            }


            var joDailyBroadcast = TableData.TableDataHelper.ReadTable(Enums.TableType.DailyBroadcast);//从配置文件中读取数据
            if (joDailyBroadcast != null)
            {
                _DailyBroadcastGlobal.ListChangeProgram = JsonConvert.DeserializeObject<List<ChangeProgram_>>(joDailyBroadcast["0"].ToString());
                _DailyBroadcastGlobal.ListPlayCtrl = JsonConvert.DeserializeObject<List<PlayCtrl_>>(joDailyBroadcast["1"].ToString());

                _DailyBroadcastGlobal.ListOutSwitch = JsonConvert.DeserializeObject<List<OutSwitch_>>(joDailyBroadcast["2"].ToString());

                _DailyBroadcastGlobal.ListRdsTransfer = JsonConvert.DeserializeObject<List<RdsTransfer_>>(joDailyBroadcast["3"].ToString());

                _DailyBroadcastGlobal.Repeat_times = 3;
            }
            else
            {
                _DailyBroadcastGlobal.ListChangeProgram = new List<ChangeProgram_>();
                _DailyBroadcastGlobal.ListPlayCtrl = new List<PlayCtrl_>();
                _DailyBroadcastGlobal.ListOutSwitch = new List<OutSwitch_>();
                _DailyBroadcastGlobal.ListRdsTransfer = new List<RdsTransfer_>();

                _DailyBroadcastGlobal.Repeat_times = 3;
            }


            var joEBContent = TableData.TableDataHelper.ReadTable(Enums.TableType.Content);//从配置文件中读取数据
            if (joEBContent != null)
            {
                _EBContentGlobal.ListEBContent = JsonConvert.DeserializeObject<List<EBMID_Content>>(joDailyBroadcast["0"].ToString());
                _EBContentGlobal.Repeat_times = 3;
            }
            else
            {
                _EBContentGlobal.ListEBContent = new List<EBMID_Content>();
                _EBContentGlobal.Repeat_times = 3;
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
                InitStreamTableNew();
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

            #region 启用 广西还是国标协议
           
            EbmStream.EB_Index_Table.ProtocolGX = SingletonInfo.GetInstance().IsGXProtocol;
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
            ini.WriteValue("EBMInfo", "IsCASet", "0");
            if (EbmStream != null && IsStartStream)
            {
                EbmStream.StopStreaming();
               // CloseScrambler();
                
                IsStartStream = false;
                SaveTmpData();
            }
            ini.WriteValue("EBMInfo", "SignCounter", SingletonInfo.GetInstance().InlayCA.SignCounter.ToString());
            ini.WriteValue("EBM", "ebm_id_behind", SingletonInfo.GetInstance().ebm_id_behind);
            ini.WriteValue("EBM", "ebm_id_count", SingletonInfo.GetInstance().ebm_id_count.ToString());
           
        }

        private void SaveTmpData()
        {
            TableData.TableDataHelper.WriteTable(Enums.TableType.CertAuth,_CertAuthGlobal.list_Cert ,_CertAuthGlobal.list_CertAuth );
            TableData.TableDataHelper.WriteTable(Enums.TableType.Index, _EBMIndexGlobal.ListEbIndex);
            TableData.TableDataHelper.WriteTable(Enums.TableType.Configure,
               _EBMConfigureGlobal.ListTimeService, _EBMConfigureGlobal.ListSetAddress, _EBMConfigureGlobal.ListWorkMode, _EBMConfigureGlobal.ListMainFrequency,_EBMConfigureGlobal.ListReback ,_EBMConfigureGlobal.ListDefaltVolume ,
             _EBMConfigureGlobal.ListRebackPeriod, _EBMConfigureGlobal.ListContentMoniterRetback, _EBMConfigureGlobal.ListContentRealMoniter, _EBMConfigureGlobal .ListStatusRetback ,_EBMConfigureGlobal.ListSoftwareUpGrade ,
              _EBMConfigureGlobal.ListRdsConfig);


            TableData.TableDataHelper.WriteTable(Enums.TableType.Content,_EBContentGlobal.ListEBContent);

            TableData.TableDataHelper.WriteTable(Enums.TableType.DailyBroadcast, _DailyBroadcastGlobal.ListChangeProgram, _DailyBroadcastGlobal.ListPlayCtrl, _DailyBroadcastGlobal.ListOutSwitch, _DailyBroadcastGlobal.ListRdsTransfer);

        }


        private OnorOFFResponse SwitchChannel(int channelID,IPEndPoint ie)
        {

            OnorOFFBroadcast tt = new OnorOFFBroadcast();
            tt.ebm_class = "4";
            tt.ebm_id = SingletonInfo.GetInstance().tcpsend.CreateEBM_ID();
            tt.ebm_level = "2";
            tt.ebm_type = "00000";
            tt.end_time = DateTime.Now.AddHours(5).ToString("yyyy-MM-dd HH:mm:ss");
            tt.start_time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            tt.power_switch = "3";// 1开播  2停播   3切换通道
            tt.volume = "80";
            tt.resource_code_type = "1";
            tt.resource_codeList = new List<string>();
            tt.resource_codeList.Add("000000000000000000");
            tt.input_channel_id = channelID;
            OnorOFFResponse resopnse = (OnorOFFResponse)SingletonInfo.GetInstance().tcpsend.SendTCPCommnand(tt, 0x04, ie);
            return resopnse;

        }

    }

}


