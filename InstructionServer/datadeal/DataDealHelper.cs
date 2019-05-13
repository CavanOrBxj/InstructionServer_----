
using Apache.NMS;
using EBMTable;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Web.Script.Serialization;

namespace InstructionServer
{
    /// <summary>
    /// 数据缓存处理
    /// </summary>
    class DataDealHelper : IDisposable
    {

        public delegate void MyDelegate(object data);

        public static event MyDelegate MyEvent; //注意须关键字 static  

        public void Dispose()
        {
        }


        public void Serialize(IPrimitiveMap MsgMap)
        {
            string commandId = MsgMap["CommandID"].ToString();

            switch (commandId)
            {

                case "1":
                    AnalysisCertAuthData(MsgMap);
                    break;
                case "2":
                    //注意：2019.4月份后加入国标版TS指令  日常开停播也是接入这个接口 然后下面接口处理
                    AnalysisEBMIndexData(MsgMap);
                    break;
                case "3":
                    AnalysisEBMConfigureData(MsgMap);
                    break;
                case "4":
                    AnalysisDailyBroadcastData(MsgMap);
                    break;
                case "5":
                    AnalysisEBContentData(MsgMap);
                    break;
                case "6":
                    ChangeInputChannel(MsgMap);
                    break;
            }
        }


        private void AnalysisCertAuthData(IPrimitiveMap map)
        {
            string packetype = map["PACKETTYPE"].ToString();
            OperatorData op = new OperatorData();
            op.OperatorType = packetype;

            switch (packetype)
            {
                case "AddCertAuthData":

                    JObject obj = JsonConvert.DeserializeObject(map["CertAuthDataList"].ToString()) as JObject;
                    string str1 = (string)obj.GetValue("CertAuthDataList").ToString();
                    List<CertAuthTmp> AddCertAuthList = JsonConvert.DeserializeObject<List<CertAuthTmp>>(str1);
                    op.Data = (object)AddCertAuthList;
                    DataDealHelper.MyEvent(op);
                    break;
                case "ModifyCertAuthData":
                    JObject obj_modify = JsonConvert.DeserializeObject(map["CertAuthDataList"].ToString()) as JObject;
                    string str2 = (string)obj_modify.GetValue("CertAuthDataList").ToString();
                    List<CertAuthTmp> ModifyCertAuthList = JsonConvert.DeserializeObject<List<CertAuthTmp>>(str2);
                    op.Data = (object)ModifyCertAuthList;
                    DataDealHelper.MyEvent(op);
                    break;
                case "DelCertAuthData":
                    JObject obj_del = JsonConvert.DeserializeObject(map["CertAuthDataidList"].ToString()) as JObject;
                    string str5 = (string)obj_del.GetValue("CertAuthDataid").ToString();

                    op.Data = (object)str5;
                    DataDealHelper.MyEvent(op);

                    break;
                case "AddCertData":
                    JObject obj_addcert = JsonConvert.DeserializeObject(map["CertDataList"].ToString()) as JObject;
                    string str3 = (string)obj_addcert.GetValue("CertDataList").ToString();
                    List<CertTmp> AddCertList = JsonConvert.DeserializeObject<List<CertTmp>>(str3);
                    op.Data = (object)AddCertList;
                    DataDealHelper.MyEvent(op);
                    break;
                case "ModifyCertData":
                    JObject obj_modifycert = JsonConvert.DeserializeObject(map["CertDataList"].ToString()) as JObject;
                    string str4 = (string)obj_modifycert.GetValue("CertDataList").ToString();
                    List<CertTmp> ModifyCertList = JsonConvert.DeserializeObject<List<CertTmp>>(str4);
                    op.Data = (object)ModifyCertList;
                    DataDealHelper.MyEvent(op);
                    break;
                case "DelCertData":
                    JObject obj_delcert = JsonConvert.DeserializeObject(map["CertDataidList"].ToString()) as JObject;
                    string str6 = (string)obj_delcert.GetValue("CertDataid").ToString();
                    op.Data = (object)str6;
                    DataDealHelper.MyEvent(op);
                    break;
                case "SetCertRepeatTime":
                    break;
            }
        }

        private void AnalysisEBMIndexData(IPrimitiveMap map)
        {
            string packetype = map["PACKETTYPE"].ToString();
            OperatorData op = new OperatorData();
            op.OperatorType = packetype;

            switch (packetype)
            {
                case "AddEBMIndex":
                    EBMIndexTmp tmp = new EBMIndexTmp();
                    tmp.BL_details_channel_indicate = map["BL_details_channel_indicate"].ToString();
                    tmp.IndexItemID = map["IndexItemID"].ToString();                

                    tmp.S_EBM_original_network_id = (SingletonInfo.GetInstance().OriginalNetworkId + 1).ToString();
                    tmp.S_EBM_start_time = map["S_EBM_start_time"].ToString();
                    tmp.S_EBM_end_time = map["S_EBM_end_time"].ToString();
                    tmp.S_EBM_type = map["S_EBM_type"].ToString();
                    if (!SingletonInfo.GetInstance().IsGXProtocol)
                    {
                        tmp.S_EBM_class = map["S_EBM_class"].ToString();//20190411修改
                        tmp.S_EBM_id = "F" + BBSHelper.CreateEBM_ID(); 
                    }
                    else
                    {
                        tmp.S_EBM_class = map["S_EBM_class"].ToString();
                        tmp.S_EBM_id = map["S_EBM_id"].ToString().Substring(0, 30);
                    }
                    tmp.S_EBM_level = map["S_EBM_level"].ToString();
                    tmp.List_EBM_resource_code = map["List_EBM_resource_code"].ToString();
                  
                    tmp.DesFlag = map["DesFlag"].ToString();
                    tmp.S_details_channel_transport_stream_id = map["S_details_channel_transport_stream_id"].ToString();
                    tmp.S_details_channel_program_number = map["S_details_channel_program_number"].ToString();
                    tmp.S_details_channel_PCR_PID = map["S_details_channel_PCR_PID"].ToString();

                    tmp.DeliverySystemDescriptor = new object();
                    if (tmp.DesFlag == "true")
                    {
                        string data = map["DeliverySystemDescriptor"].ToString();
                        string pp1 = data.Substring(1);
                        string dd1 = pp1.Substring(0, pp1.Length - 1);
                        if (data.Contains("B_FEC_inner"))//有线传送系统描述符
                        {
                            tmp.DeliverySystemDescriptor = (object)JsonConvert.DeserializeObject<CableDeliverySystemDescriptortmp>(dd1);
                            tmp.descriptor_tag = 68;
                        }
                        else
                        {
                            //地面传送系统描述符
                            tmp.DeliverySystemDescriptor = (object)JsonConvert.DeserializeObject<TerristrialDeliverySystemDescriptortmp>(dd1);
                            tmp.descriptor_tag = 90;
                        }
                    }
                    tmp.List_ProgramStreamInfo = new List<ProgramStreamInfotmp>();

                    if (tmp.BL_details_channel_indicate == "true")
                    {
                        string data = map["List_ProgramStreamInfo"].ToString();


                        JavaScriptSerializer Serializer = new JavaScriptSerializer();

                        List<ProgramStreamInfotmp> objs = Serializer.Deserialize<List<ProgramStreamInfotmp>>(data);

                        foreach (ProgramStreamInfotmp item in objs)
                        {
                            //if (item.Descriptor2 != null)
                            //{
                            //    //dynamic a = item.Descriptor2;
                            //    //Descriptor2 niemi = new Descriptor2();
                            //    //string[] pp = ((string)a[0]["B_descriptor"]).Split(' ');
                            //    //List<byte> byteList = new List<byte>();

                            //    //foreach (var it in pp)
                            //    //{
                            //    //    if (it != "")
                            //    //    {
                            //    //        byteList.Add((byte)Convert.ToInt32(it));
                            //    //    }

                            //    //}

                            //    //niemi.B_descriptor = byteList.ToArray();


                            //    //if (a[0]["B_descriptor_tag"] != "")
                            //    //{
                            //    //    niemi.B_descriptor_tag = (byte)a[0]["B_descriptor_tag"];
                            //    //}

                            //    //item.Descriptor2 = niemi;

                            //}
                            //else
                            //{

                            //    #region  特殊处理 Descriptor2  不能为空 当BL_details_channel_indicate==true时  20180524
                            //    //  dynamic a = item.Descriptor2;
                            //    Descriptor2 niemi = new Descriptor2();
                            //    //niemi.B_descriptor = new byte[]{0,0};
                            //    //niemi.B_descriptor_tag = (byte)1;


                            //    niemi.B_descriptor = new byte[] { 0 };
                            //    niemi.B_descriptor_tag = (byte)3;
                            //    item.Descriptor2 = niemi;
                            //    #endregion
                            //}


                            //修改于20180530
                            item.Descriptor2 = null;
                            //测试注释20181227
                          
                            if (!SingletonInfo.GetInstance().IsGXProtocol)
                            {
                                //item.B_stream_type = "00";//20190420  原来值
                                item.B_stream_type = "03";//修改于20190420  工信对接  
                            }
                            else
                            {
                                if (item.B_stream_type == "84")
                                {
                                    item.B_stream_type = "03";
                                }
                            }
                        }
                        tmp.List_ProgramStreamInfo = objs;

                    }



                    if (SingletonInfo.GetInstance().IsGXProtocol)
                    {
                        op.Data = (object)tmp;
                    }
                    else
                    {
                        if (tmp.S_EBM_class == "0005")
                        {
                            op.OperatorType = "AddDailyBroadcast";
                            ChangeProgram_ pp = new ChangeProgram_();

                            pp.Program = new DailyCmdChangeProgram();
                            pp.ItemID = tmp.IndexItemID;
                            pp.Program.NetID = (short)0;
                            pp.Program.TSID = (short)0;
                            pp.Program.ServiceID = (short)0;
                            pp.Program.PCR_PID = 0;
                            if (tmp.BL_details_channel_indicate == "true")
                            {

                                List<ProgramStreamInfotmp> List_ProgramStreamInfotmp = new List<ProgramStreamInfotmp>();//S_elementary_PID 中有“，”时，临时加入项
                                int List_ProgramStreamInfoLength = tmp.List_ProgramStreamInfo.Count;//详情频道节目流信息列表长度
                                for (int i = 0; i < List_ProgramStreamInfoLength; i++)
                                {
                                    string S_elementary_PID = tmp.List_ProgramStreamInfo[i].S_elementary_PID;

                                    if (S_elementary_PID.Contains(","))
                                    {
                                        string[] pidarray = S_elementary_PID.Split(',');
                                        pp.Program.Program_PID = (short)Convert.ToInt32(pidarray[0]);
                                        pp.Program.Stream_Type = 0x03;
                                    }
                                    else
                                    {
                                        //string[] pidarray = S_elementary_PID.Split(',');
                                        pp.Program.Program_PID = (short)Convert.ToInt32(S_elementary_PID);
                                        pp.Program.Stream_Type = 0x03;
                                    }
                                }
                            }



                            pp.Program.Priority = (short)0;
                            pp.Program.Volume = (short)255;



                            DateTime dd = Convert.ToDateTime(tmp.S_EBM_end_time);
                            pp.Program.EndTime = dd;


                            pp.Program.B_Address_type = (byte)1;



                            string[] List_EBM_resource_codeArray = tmp.List_EBM_resource_code.Split(',');

                            for (int i = 0; i < List_EBM_resource_codeArray.Length; i++)
                            {
                                int resource_code_length = List_EBM_resource_codeArray[i].Length;


                                //20180525 陈良要求修改特殊处理
                                switch (resource_code_length)
                                {
                                    case 18:
                                        break;
                                    case 23:

                                        if (SingletonInfo.GetInstance().IsGXProtocol)
                                        {
                                            string tt = List_EBM_resource_codeArray[i].Substring(1);
                                            string tt1 = tt.Substring(0, tt.Length - 4);
                                            List_EBM_resource_codeArray[i] = tt1;
                                        }
                                        
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
                            pp.Program.list_Terminal_Address = new List<string>(List_EBM_resource_codeArray);
                            pp.Program.S_cmd_id = "F" + BBSHelper.CreateEBM_ID();
                            List<ChangeProgram_> listCP = new List<ChangeProgram_>();
                            listCP.Add(pp);
                            op.ModuleType = "1";
                            op.Data = listCP;
                        }
                        else
                        {
                            op.Data = (object)tmp;
                        }
                    }
                    DataDealHelper.MyEvent(op);
                    break;
                case "AddAreaEBMIndex":
                case "DelAreaEBMIndex":
                    ModifyEBMIndex Mebm = new ModifyEBMIndex();
                    Mebm.IndexItemID = map["IndexItemID"].ToString();
                    Mebm.Data = map["List_EBM_resource_code"].ToString();
                    op.Data = (object)Mebm;
                    DataDealHelper.MyEvent(op);
                    break;
                case "DelEBMIndex":
                    if (SingletonInfo.GetInstance().IsGXProtocol)
                    {
                        op.Data = map["IndexItemID"].ToString().Split('~')[0];
                    }
                    else
                    {
                        //国标协议

                        string[] str = map["IndexItemID"].ToString().Split('~');
                        string IndexItemIDstr = str[0] ;

                        string broadcasttype = str[1];
                        if (broadcasttype == "0005")
                        {
                            //日常
                            op.ModuleType = "2";
                            op.OperatorType = "DelDailyBroadcast";
                            op.Data = IndexItemIDstr;
                        }
                        else
                        {
                            //应急  还是走索引表
                            op.Data = IndexItemIDstr;
                        }
                    }
                    DataDealHelper.MyEvent(op);
                    break;
            }
        }

        private void AnalysisEBMConfigureData(IPrimitiveMap map)
        {
            string packetype = map["PACKETTYPE"].ToString();
            OperatorData op = new OperatorData();
            op.OperatorType = packetype;
            op.ModuleType = map["Cmdtag"].ToString();
            JavaScriptSerializer Serializer = new JavaScriptSerializer();

            switch (packetype)
            {
                case "AddEBMConfigure":
                case "ModifyEBMConfigure":
                    string data = map["data"].ToString();
                    switch (op.ModuleType)
                    {
                        case "1"://时间校准
                            JsonstructureDeal(ref data);
                            //  List<TimeService_> listTS = Serializer.Deserialize<List<TimeService_>>(data);
                            //因为有ValueType缘故 不能反序列化
                            string[] dataArray = data.Split(',');
                            TimeService_ pp = new TimeService_();
                            foreach (var item in dataArray)
                            {
                                if (item.Contains("ItemID"))
                                {
                                    pp.ItemID = item.Split(':')[1];
                                }

                                if (item.Contains("B_Daily_cmd_tag"))
                                {
                                    // pp.B_Daily_cmd_tag = Convert.ToByte(item.Split(':')[1]);
                                }
                                if (item.Contains("Configure"))
                                {
                                    pp.Configure = new EBConfigureTimeService();

                                    string timestr = item.Substring(26);
                                    string time = timestr.Substring(0, timestr.Length - 2);
                                    DateTime dd = Convert.ToDateTime(time);
                                    pp.Configure.Real_time = dd;
                                    // pp.Configure.Real_time = item.Split(':')[1].Split(':')[1].TrimEnd('}');
                                }

                                if (item.Contains("GetSystemTime"))
                                {
                                   // pp.GetSystemTime = item.Split(':')[1] == "true" ? true : false;

                                    //按照陈良需求 修改为固定取系统时间  
                                    pp.GetSystemTime = true;
                                }

                                if (item.Contains("SendTick"))
                                {
                                    string qq = item.Split(':')[1];
                                    string ww = qq.Substring(0, qq.Length - 2);

                                    if (Convert.ToInt32(ww) < 60)
                                    {
                                        pp.SendTick = 60;

                                    }
                                    else
                                    {
                                        pp.SendTick = Convert.ToInt32(ww);
                                    }
                                   
                                }
                            }
                            List<TimeService_> listTS = new List<TimeService_>();
                            
                            listTS.Add(pp);
                            
                            op.Data = listTS;
                            break;
                        case "2"://2区域码设置
                            JsonstructureDeal(ref data);

                            List<SetAddress_> listSA = Serializer.Deserialize<List<SetAddress_>>(data);
                            
                            op.Data = listSA;
                            break;
                        case "3"://工作模式设置
                            JsonstructureDeal(ref data);
                            List<WorkMode_> listWM = Serializer.Deserialize<List<WorkMode_>>(data);

                            if (!SingletonInfo.GetInstance().IsGXProtocol)
                            {

                                for (int i = 0; i < listWM.Count; i++)
                                {
                                    for (int j = 0; j < listWM[i].Configure.list_Terminal_Address.Count; j++)
                                    {
                                        if (listWM[i].Configure.list_Terminal_Address[j].Length == 12)
                                        {
                                            listWM[i].Configure.list_Terminal_Address[j] = "6" + listWM[i].Configure.list_Terminal_Address[j] + "0314000000";
                                        }
                                    }
                                }
                            }
                            else
                            {
                                for (int i = 0; i < listWM.Count; i++)
                                {
                                    for (int j = 0; j < listWM[i].Configure.list_Terminal_Address.Count; j++)
                                    {
                                        if (listWM[i].Configure.list_Terminal_Address[j].Length==12)
                                        {
                                            listWM[i].Configure.list_Terminal_Address[j] = "0612" + listWM[i].Configure.list_Terminal_Address[j] + "00";
                                        }
                                    }
                                }
                            }

                            op.Data = listWM;
                            break;
                        case "4"://锁定频率设置
                            JsonstructureDeal(ref data);
                            List<MainFrequency_> listMF = Serializer.Deserialize<List<MainFrequency_>>(data);
                            if (!SingletonInfo.GetInstance().IsGXProtocol)
                            {

                                for (int i = 0; i < listMF.Count; i++)
                                {
                                    for (int j = 0; j < listMF[i].Configure.list_Terminal_Address.Count; j++)
                                    {
                                        if (listMF[i].Configure.list_Terminal_Address[j].Length==12)
                                        {
                                            listMF[i].Configure.list_Terminal_Address[j] = "6" + listMF[i].Configure.list_Terminal_Address[j] + "0314000000";
                                        }
                                    }
                                }
                            }
                            else
                            {
                                for (int i = 0; i < listMF.Count; i++)
                                {
                                    for (int j = 0; j < listMF[i].Configure.list_Terminal_Address.Count; j++)
                                    {
                                        if (listMF[i].Configure.list_Terminal_Address[j].Length==12)
                                        {
                                            listMF[i].Configure.list_Terminal_Address[j] = "0612" + listMF[i].Configure.list_Terminal_Address[j] + "00";
                                        }
                                    }
                                }
                            }

                            op.Data = listMF;
                            break;
                        case "5"://回传方式设置
                            JsonstructureDeal(ref data);
                            //又要特殊处理

                            string tmp1 = data.Replace("\"S_reback_address_backup\":,", "\"S_reback_address_backup\":\"null\",");
                            string tmp2 = tmp1.Replace("\"I_reback_port_Backup\":,", "\"I_reback_port_Backup\":0,");
                           
                            if (!SingletonInfo.GetInstance().IsGXProtocol)
                            {
                                List<Reback_Nation> listRB = Serializer.Deserialize<List<Reback_Nation>>(tmp2);


                                int lo1 = tmp2.IndexOf("I_reback_port") + 14;
                                int lo2 = tmp2.IndexOf("I_reback_port_Backup") - 3;
                                int cha = lo2 - lo1;

                                string port = tmp2.Substring(lo1 + 1, cha);

                              
                                for (int i = 0; i < listRB.Count; i++)
                                {
                                    for (int j = 0; j < listRB[i].Configure.list_Terminal_Address.Count; j++)
                                    {
                                        if (listRB[i].Configure.list_Terminal_Address[j].Length == 12)
                                        {
                                            listRB[i].Configure.list_Terminal_Address[j] = "6" + listRB[i].Configure.list_Terminal_Address[j] + "0314000000";
                                        }
                                    }

                                    listRB[i].Configure.S_reback_address += ":"+port;
                                }
                                op.Data = listRB;
                            }
                            else
                            {
                                List<Reback_> listRB = Serializer.Deserialize<List<Reback_>>(tmp2);
                                for (int i = 0; i < listRB.Count; i++)
                                {
                                    for (int j = 0; j < listRB[i].Configure.list_Terminal_Address.Count; j++)
                                    {
                                        if (listRB[i].Configure.list_Terminal_Address[j].Length==12)
                                        {
                                            listRB[i].Configure.list_Terminal_Address[j] = "0612" + listRB[i].Configure.list_Terminal_Address[j] + "00";
                                        }
                                    }
                                }
                                op.Data = listRB;
                            }
                          
                            break;
                        case "6"://默认音量设置
                            JsonstructureDeal(ref data);
                            List<DefaltVolume_> listDV = Serializer.Deserialize<List<DefaltVolume_>>(data);
                            
                            if (!SingletonInfo.GetInstance().IsGXProtocol)
                            {
                                for (int i = 0; i < listDV.Count; i++)
                                {
                                    for (int j = 0; j < listDV[i].Configure.list_Terminal_Address.Count; j++)
                                    {
                                        if (listDV[i].Configure.list_Terminal_Address[j].Length == 12)
                                        {
                                            listDV[i].Configure.list_Terminal_Address[j] = "6" + listDV[i].Configure.list_Terminal_Address[j] + "0314000000";
                                        }
                                    }
                                }
                            }
                            else
                            {
                                for (int i = 0; i < listDV.Count; i++)
                                {
                                    for (int j = 0; j < listDV[i].Configure.list_Terminal_Address.Count; j++)
                                    {
                                        if (listDV[i].Configure.list_Terminal_Address[j].Length==12)
                                        {
                                            listDV[i].Configure.list_Terminal_Address[j] = "0612" + listDV[i].Configure.list_Terminal_Address[j] + "00";
                                        }
                                    }
                                }
                            }

                            op.Data = listDV;
                            break;
                        case "7"://回传周期设置
                            JsonstructureDeal(ref data);
                            List<RebackPeriod_> listRP = Serializer.Deserialize<List<RebackPeriod_>>(data);
                            if (!SingletonInfo.GetInstance().IsGXProtocol)
                            {

                                for (int i = 0; i < listRP.Count; i++)
                                {
                                    for (int j = 0; j < listRP[i].Configure.list_Terminal_Address.Count; j++)
                                    {
                                        if (listRP[i].Configure.list_Terminal_Address[j].Length == 12)
                                        {
                                            listRP[i].Configure.list_Terminal_Address[j] = "6" + listRP[i].Configure.list_Terminal_Address[j] + "0314000000";
                                        }
                                    }
                                }
                            }
                            else
                            {
                                for (int i = 0; i < listRP.Count; i++)
                                {
                                    for (int j = 0; j < listRP[i].Configure.list_Terminal_Address.Count; j++)
                                    {
                                        if (listRP[i].Configure.list_Terminal_Address[j].Length==12)
                                        {
                                            listRP[i].Configure.list_Terminal_Address[j] = "0612" + listRP[i].Configure.list_Terminal_Address[j] + "00";
                                        }
                                    }
                                }
                            }

                            op.Data = listRP;
                            break;
                        case "104"://启动内容检测指令
                            JsonstructureDeal(ref data);
                            List<ContentMoniterRetback_> listCMR = Serializer.Deserialize<List<ContentMoniterRetback_>>(data);

                            if (!SingletonInfo.GetInstance().IsGXProtocol)
                            {
                                for (int i = 0; i < listCMR.Count; i++)
                                {
                                    for (int j = 0; j < listCMR[i].Configure.list_Terminal_Address.Count; j++)
                                    {
                                        if (listCMR[i].Configure.list_Terminal_Address[j].Length == 12)
                                        {
                                            listCMR[i].Configure.list_Terminal_Address[j] = "6" + listCMR[i].Configure.list_Terminal_Address[j] + "0314000000";
                                        }
                                    }
                                }
                            }
                            else
                            {
                                for (int i = 0; i < listCMR.Count; i++)
                                {
                                    for (int j = 0; j < listCMR[i].Configure.list_Terminal_Address.Count; j++)
                                    {
                                        if (listCMR[i].Configure.list_Terminal_Address[j].Length==12)
                                        {
                                            listCMR[i].Configure.list_Terminal_Address[j] = "0612" + listCMR[i].Configure.list_Terminal_Address[j] + "00";
                                        }
                                    }
                                }
                            }

                            op.Data = listCMR;
                            break;
                        case "105"://启动内容监测实时监听指令
                            JsonstructureDeal(ref data);
                            //if (!SingletonInfo.GetInstance().IsGXProtocol)
                            //{
                            //    List<ContentRealMoniter_> listCRM = Serializer.Deserialize<List<ContentRealMoniter_>>(data);

                            //    for (int i = 0; i < listCRM.Count; i++)
                            //    {
                            //        for (int j = 0; j < listCRM[i].Configure.list_Terminal_Address.Count; j++)
                            //        {
                            //            if (listCRM[i].Configure.list_Terminal_Address[j].Length==12)
                            //            {
                            //                listCRM[i].Configure.list_Terminal_Address[j] = "6" + listCRM[i].Configure.list_Terminal_Address[j] + "0314000000";
                            //            }
                            //        }
                            //    }
                            //    op.Data = listCRM;
                            //}
                            //else
                            //{
                            //    List<ContentRealMoniterGX_> listCRMGX = Serializer.Deserialize<List<ContentRealMoniterGX_>>(data);
                            //    for (int i = 0; i < listCRMGX.Count; i++)
                            //    {
                            //        for (int j = 0; j < listCRMGX[i].Configure.list_Terminal_Address.Count; j++)
                            //        {
                            //            if (listCRMGX[i].Configure.list_Terminal_Address[j].Length==12)
                            //            {
                            //                listCRMGX[i].Configure.list_Terminal_Address[j] = "0612" + listCRMGX[i].Configure.list_Terminal_Address[j] + "00";
                            //            }
                            //        }
                            //    }
                            //    op.Data = listCRMGX;
                            //}

                            //国标版本的实时监听用的广西协议  20190413
                            List<ContentRealMoniterGX_> listCRMGX = Serializer.Deserialize<List<ContentRealMoniterGX_>>(data);
                            for (int i = 0; i < listCRMGX.Count; i++)
                            {
                                for (int j = 0; j < listCRMGX[i].Configure.list_Terminal_Address.Count; j++)
                                {
                                    if (listCRMGX[i].Configure.list_Terminal_Address[j].Length == 12)
                                    {
                                        //   listCRMGX[i].Configure.list_Terminal_Address[j] = "F6" + listCRMGX[i].Configure.list_Terminal_Address[j] + "0314000000";
                                        listCRMGX[i].Configure.list_Terminal_Address[j] = "6" + listCRMGX[i].Configure.list_Terminal_Address[j] + "0314000000";
                                    }
                                }
                            }
                            op.Data = listCRMGX;
                            break;
                        case "106"://终端工作状态查询
                            JsonstructureDeal(ref data);
                            List<StatusRetbackGX_> listSR = Serializer.Deserialize<List<StatusRetbackGX_>>(data);//貌似广西没有这个功能

                            if (!SingletonInfo.GetInstance().IsGXProtocol)
                            {
                                for (int i = 0; i < listSR.Count; i++)
                                {
                                    for (int j = 0; j < listSR[i].Configure.list_Terminal_Address.Count; j++)
                                    {
                                        if (listSR[i].Configure.list_Terminal_Address[j].Length == 12)
                                        {
                                            listSR[i].Configure.list_Terminal_Address[j] = "6" + listSR[i].Configure.list_Terminal_Address[j] + "0314000000";
                                        }
                                    }
                                }
                            }
                            else
                            {
                                for (int i = 0; i < listSR.Count; i++)
                                {
                                    for (int j = 0; j < listSR[i].Configure.list_Terminal_Address.Count; j++)
                                    {
                                        if (listSR[i].Configure.list_Terminal_Address[j].Length==12)
                                        {
                                            listSR[i].Configure.list_Terminal_Address[j] = "0612" + listSR[i].Configure.list_Terminal_Address[j] + "00";
                                        }
                                    }
                                }
                            }

                            op.Data = listSR;
                            break;
                        case "240"://终端固件升级
                            JsonstructureDeal(ref data);
                            List<SoftwareUpGrade_> listSUG = Serializer.Deserialize<List<SoftwareUpGrade_>>(data);

                            if (!SingletonInfo.GetInstance().IsGXProtocol)
                            {

                                for (int i = 0; i < listSUG.Count; i++)
                                {
                                    for (int j = 0; j < listSUG[i].Configure.list_Terminal_Address.Count; j++)
                                    {
                                        if (listSUG[i].Configure.list_Terminal_Address[j].Length == 12)
                                        {
                                            listSUG[i].Configure.list_Terminal_Address[j] = "6" + listSUG[i].Configure.list_Terminal_Address[j] + "0314000000";
                                        }
                                    }
                                }
                            }
                            else
                            {
                                for (int i = 0; i < listSUG.Count; i++)
                                {
                                    for (int j = 0; j < listSUG[i].Configure.list_Terminal_Address.Count; j++)
                                    {
                                        if (listSUG[i].Configure.list_Terminal_Address[j].Length==12)
                                        {
                                            listSUG[i].Configure.list_Terminal_Address[j] = "0612" + listSUG[i].Configure.list_Terminal_Address[j] + "00";
                                        }
                                    }
                                }
                            }


                            op.Data = listSUG;
                            break;

                        case "300":
                            //国标 终端状态查询指令

                            JsonstructureDeal(ref data);
                            //又要特殊处理

                            string tmp3 = data.Replace("\"S_reback_address_backup\":,", "\"S_reback_address_backup\":\"null\",");
                            string tmp4 = tmp3.Replace("\"I_reback_port_Backup\":,", "\"I_reback_port_Backup\":0,");

                            List<Reback_Nation_add> listRBa = Serializer.Deserialize<List<Reback_Nation_add>>(tmp4);
                            for (int i = 0; i < listRBa.Count; i++)
                            {
                                for (int j = 0; j < listRBa[i].Configure.list_Terminal_Address.Count; j++)
                                {
                                    if (listRBa[i].Configure.list_Terminal_Address[j].Length == 12)
                                    {
                                        listRBa[i].Configure.list_Terminal_Address[j] = "6" + listRBa[i].Configure.list_Terminal_Address[j] + "0314000000";
                                    }
                                }
                            }
                            // op.Data = listRB;
                            listRBa[0].query_code_List = new List<string>();
                            //string ppo = "[{"ItemID":3,"B_Daily_cmd_tag":300,"Configure":{"B_reback_type":2,"S_reback_address":"192.168.100.100","S_reback_address_backup":"","I_reback_port":7202,"I_reback_port_Backup":,"B_Address_type":1,"list_Terminal_Address":["532625000000"],"query_code":["01,02,03,04,05,06,07,08,09,0A,0B,0C,0D,0E,0F,10,11,12"]}}]";

                            int local = data.IndexOf("query_code");
                            string tmp22 = data.Substring(local + 14);
                            string tmp33 = tmp22.Substring(0, tmp22.Length - 5);
                            string[] query_codes = tmp33.Split(',');

                            #region 必须要查物理码 
                            bool exists = ((IList)query_codes).Contains("5");
                            if (!exists)
                            {
                                listRBa[0].query_code_List.Add("5");
                            }
                            #endregion
                            foreach (string item in query_codes)
                            {
                                listRBa[0].query_code_List.Add(item);
                            }
                            listRBa[0].query_code_List.Sort();



                            List<StatusRetback_> listSRB = new List<StatusRetback_>();
                            foreach (Reback_Nation_add item in listRBa)
                            {
                                StatusRetback_ sr = new StatusRetback_();
                                sr.Configure = new EBConfigureStatusRetback();
                                sr.Configure.B_Address_type = 0x01;
                                sr.Configure.list_Parameter_tag = new List<byte>();


                                List<byte> list_Parameter_tag_tmp = new List<byte>();
                                foreach (string  ee in item.query_code_List)
                                {
                                    list_Parameter_tag_tmp.Add((byte)Convert.ToInt32(ee,16));
                                }

                                if (list_Parameter_tag_tmp.Count>18)
                                {
                                    list_Parameter_tag_tmp.RemoveAt(list_Parameter_tag_tmp.Count-1);
                                }
                                sr.Configure.list_Parameter_tag = list_Parameter_tag_tmp;
                                sr.Configure.list_Terminal_Address = item.Configure.list_Terminal_Address;
                                sr.ItemID = item.ItemID;

                                listSRB.Add(sr);
                            }
                            op.ModuleType = "300";
                            op.Data = listSRB;
                            break;
                    

                case "8"://RDS配置
                           // JsonstructureDeal(ref data);
                              data=data.Substring(18,data.Length-18);  //特殊处理  刘工发送的json字符异常
                              List<RdsConfig_> listRC = Serializer.Deserialize<List<RdsConfig_>>(data);
                              string textRdsData = "";
                              foreach (var item in data.Split(','))
                              {
                                  if (item.Contains("RdsDataText"))
                                    {
                                        textRdsData = item;
                                    }
                              }
                              textRdsData = textRdsData.Substring(15);
                              textRdsData = textRdsData.Substring(0, textRdsData.Length-4);
                               listRC[0].Configure.Br_Rds_data = Utils.ArrayHelper.String2Bytes(textRdsData);
                               listRC[0].RdsDataText = textRdsData;

                            if (!SingletonInfo.GetInstance().IsGXProtocol)
                            {
                                for (int i = 0; i < listRC.Count; i++)
                                {
                                    for (int j = 0; j < listRC[i].Configure.list_Terminal_Address.Count; j++)
                                    {
                                        if (listRC[i].Configure.list_Terminal_Address[j].Length == 12)
                                        {
                                            listRC[i].Configure.list_Terminal_Address[j] = "6" + listRC[i].Configure.list_Terminal_Address[j] + "0314000000";
                                        }
                                    }
                                }
                            }
                            else
                            {
                                for (int i = 0; i < listRC.Count; i++)
                                {
                                    for (int j = 0; j < listRC[i].Configure.list_Terminal_Address.Count; j++)
                                    {
                                        if (listRC[i].Configure.list_Terminal_Address[j].Length==12)
                                        {
                                            listRC[i].Configure.list_Terminal_Address[j] = "0612" + listRC[i].Configure.list_Terminal_Address[j] + "00";
                                        }
                                    }
                                }
                            }

                            op.Data = listRC;
                            break;
                    }

                    break;

                case "DelEBMConfigure":
                    string ItemList = map["ItemIDList"].ToString();
                    op.Data = ItemList;
                    break;
            }
            DataDealHelper.MyEvent(op);
        }

        /// <summary>
        /// Json结构规范化
        /// </summary>
        /// <param name="data"></param>
        private void JsonstructureDeal(ref  string data)
        {
            int loacal = data.IndexOf('[');
            data = data.Substring(loacal, data.Length - loacal - 2);

        }

        private void AnalysisDailyBroadcastData(IPrimitiveMap map)
        {
            string packetype = map["PACKETTYPE"].ToString();
            OperatorData op = new OperatorData();
            op.OperatorType = packetype;
            op.ModuleType = map["Cmdtag"].ToString();
            JavaScriptSerializer Serializer = new JavaScriptSerializer();

            switch (packetype)
            {
                case "AddDailyBroadcast":
                case "ModifyDailyBroadcast":
                    string data = map["data"].ToString();
                    switch (op.ModuleType)
                    {
                        case "1"://节目切播
                            JsonstructureDeal(ref data);
                            ////因为有ValueType缘故 不能反序列化
                            string[] dataArray = data.Split(',');
                            ChangeProgram_ pp = new ChangeProgram_();

                            pp.Program = new DailyCmdChangeProgram();


                            foreach (var item in dataArray)
                            {
                                if (item.Contains("ItemID"))
                                {
                                    pp.ItemID = item.Split(':')[1];
                                }

                                if (item.Contains("B_Daily_cmd_tag"))
                                {
                                    // pp.B_Daily_cmd_tag = Convert.ToByte(item.Split(':')[1]);
                                }
                                if (item.Contains("Program"))
                                {
                                    pp.Program.NetID = (short)Convert.ToInt32(item.Split(':')[2]);
                                }
                                if (item.Contains("TSID"))
                                {
                                    pp.Program.TSID = (short)Convert.ToInt32(item.Split(':')[1]);
                                }

                                if (item.Contains("ServiceID"))
                                {
                                    pp.Program.ServiceID = (short)Convert.ToInt32(item.Split(':')[1]);
                                }

                                if (item.Contains("PCR_PID"))
                                {
                                    pp.Program.ServiceID = (short)Convert.ToInt32(item.Split(':')[1]);
                                }

                                if (item.Contains("Program_PID"))
                                {
                                    pp.Program.Program_PID = (short)Convert.ToInt32(item.Split(':')[1]);
                                }

                                if (item.Contains("Priority"))
                                {
                                    pp.Program.Priority = (short)Convert.ToInt32(item.Split(':')[1]);
                                }

                                if (item.Contains("Volume"))
                                {
                                    pp.Program.Volume = (short)Convert.ToInt32(item.Split(':')[1]);
                                }

                                if (item.Contains("EndTime"))
                                {
                                    DateTime dd = Convert.ToDateTime(item.Split(':')[1]);
                                    pp.Program.EndTime = dd;
                                }
                                if (item.Contains("B_Address_type"))
                                {
                                    pp.Program.B_Address_type = (byte)Convert.ToInt32(item.Split(':')[1]);
                                }

                                if (item.Contains("list_Terminal_Address"))
                                {
                                    string tmp = item.Split(':')[1].Substring(1);
                                    string tmp2 = tmp.Substring(0, tmp.Length - 4);
                                    string[] array = tmp2.Split(',');

                                    pp.Program.list_Terminal_Address = new List<string>(array);
                                }
                            }
                            
                            List<ChangeProgram_> listCP = new List<ChangeProgram_>();
                            listCP.Add(pp);

                            if (!SingletonInfo.GetInstance().IsGXProtocol)
                            {
                                foreach (ChangeProgram_ item in listCP)
                                {
                                    for (int i = 0; i < item.Program.list_Terminal_Address.Count; i++)
                                    {
                                        item.Program.list_Terminal_Address[i] = "F6" + item.Program.list_Terminal_Address[i].Substring(0, 12) + "0314000000";
                                    }
                                }
                            }
                            op.Data = listCP;
                            break;
                        case "3"://播放控制
                            JsonstructureDeal(ref data);
                            List<PlayCtrl_> listPC = Serializer.Deserialize<List<PlayCtrl_>>(data);

                            if (!SingletonInfo.GetInstance().IsGXProtocol)
                            {
                                foreach (PlayCtrl_ item in listPC)
                                {
                                    for (int i = 0; i < item.Program.list_Terminal_Address.Count; i++)
                                    {
                                        item.Program.list_Terminal_Address[i] = "F6" + item.Program.list_Terminal_Address[i].Substring(0, 12) + "0314000000";
                                    }
                                }
                            }


                            op.Data = listPC;
                            break;
                        case "4"://输出控制
                            JsonstructureDeal(ref data);
                            List<OutSwitch_> listOS = Serializer.Deserialize<List<OutSwitch_>>(data);
                            if (!SingletonInfo.GetInstance().IsGXProtocol)
                            {
                                foreach (OutSwitch_ item in listOS)
                                {
                                    for (int i = 0; i < item.Program.list_Terminal_Address.Count; i++)
                                    {

                                        if (item.Program.list_Terminal_Address[i].Length==23)
                                        {
                                            item.Program.list_Terminal_Address[i] = item.Program.list_Terminal_Address[i];
                                        }
                                      //  item.Program.list_Terminal_Address[i] = "F6" + item.Program.list_Terminal_Address[i].Substring(0, 12) + "0314000000";
                                    }
                                }
                            }

                            op.Data = listOS;
                            break;
                        case "5"://RDS编码数据透传
                            JsonstructureDeal(ref data);
                            List<RdsTransfer_> listRT = Serializer.Deserialize<List<RdsTransfer_>>(data);

                            if (!SingletonInfo.GetInstance().IsGXProtocol)
                            {
                                foreach (RdsTransfer_ item in listRT)
                                {
                                    for (int i = 0; i < item.Program.list_Terminal_Address.Count; i++)
                                    {
                                        item.Program.list_Terminal_Address[i] = "F6" + item.Program.list_Terminal_Address[i].Substring(0, 12) + "0314000000";
                                    }
                                }
                            }

                            op.Data = listRT;
                            break;

                    }

                    break;

                case "DelDailyBroadcast":
                    string ItemList = map["ItemIDList"].ToString();
                    op.Data = ItemList;
                    break;
            }
            DataDealHelper.MyEvent(op);
        }


        public void TestFunc()
        {
            string packetype = "AddDailyBroadcast";
            OperatorData op = new OperatorData();
            op.OperatorType = packetype;
            op.ModuleType ="1";
            JavaScriptSerializer Serializer = new JavaScriptSerializer();

            switch (packetype)
            {
                case "AddDailyBroadcast":
                case "ModifyDailyBroadcast":
                    string data ="[{\"ItemID\":72076,\"B_Daily_cmd_tag\":1,\"Program\":{\"NetID\":1,\"TSID\":1,\"ServiceID\":1,\"PCR_PID\":1,\"Program_PID\":4013,\"Priority\":4,\"Volume\":1,\"EndTime\":\"2019/5/27 17:35:09\",\"B_Address_type\":1,\"list_Terminal_Address\":[\"634152310000\"]}}]";
                    switch (op.ModuleType)
                    {
                        case "1"://节目切播
                           // JsonstructureDeal(ref data);
                            ////因为有ValueType缘故 不能反序列化
                            string[] dataArray = data.Split(',');
                            ChangeProgram_ pp = new ChangeProgram_();

                            pp.Program = new DailyCmdChangeProgram();


                            foreach (var item in dataArray)
                            {
                                if (item.Contains("ItemID"))
                                {
                                    pp.ItemID = item.Split(':')[1];
                                }

                                if (item.Contains("B_Daily_cmd_tag"))
                                {
                                    // pp.B_Daily_cmd_tag = Convert.ToByte(item.Split(':')[1]);
                                }
                                if (item.Contains("Program") && !item.Contains("Program_PID"))
                                {
                                    pp.Program.NetID = (short)Convert.ToInt32(item.Split(':')[2]);
                                }
                                if (item.Contains("TSID"))
                                {
                                    pp.Program.TSID = (short)Convert.ToInt32(item.Split(':')[1]);
                                }

                                if (item.Contains("ServiceID"))
                                {
                                    pp.Program.ServiceID = (short)Convert.ToInt32(item.Split(':')[1]);
                                }

                                if (item.Contains("PCR_PID"))
                                {
                                    pp.Program.PCR_PID = (short)Convert.ToInt32(item.Split(':')[1]);
                                }

                                if (item.Contains("Program_PID"))
                                {
                                    pp.Program.Program_PID = (short)Convert.ToInt32(item.Split(':')[1]);
                                }

                                if (item.Contains("Priority"))
                                {
                                    pp.Program.Priority = (short)Convert.ToInt32(item.Split(':')[1]);
                                }

                                if (item.Contains("Volume"))
                                {
                                    pp.Program.Volume = (short)Convert.ToInt32(item.Split(':')[1]);
                                }

                                if (item.Contains("EndTime"))
                                {

                                   string tmp= item.Substring(10);

                                   string tmp1 = tmp.Substring(1);
                                   string tmp2 = tmp1.Substring(0,tmp1.Length-1);
                                   DateTime dd = Convert.ToDateTime(tmp2);
                                    pp.Program.EndTime = dd;
                                }
                                if (item.Contains("B_Address_type"))
                                {
                                    pp.Program.B_Address_type = (byte)Convert.ToInt32(item.Split(':')[1]);
                                }

                                if (item.Contains("list_Terminal_Address"))
                                {
                                    string tmp = item.Split(':')[1].Substring(1);
                                    string tmp2 = tmp.Substring(0, tmp.Length - 4);

                                    string tmp3 = tmp2.Substring(1);

                                    string tmp4 = tmp3.Substring(0, tmp3.Length - 1);
                                    string[] array = tmp4.Split(',');

                                    int arraylength = array.Length;

                                    if (SingletonInfo.GetInstance().IsGXProtocol)
                                    {
                                        for (int i = 0; i < arraylength; i++)
                                        {
                                            array[i] = array[i] + "000000";
                                        }
                                    }
                                  

                                    pp.Program.list_Terminal_Address = new List<string>(array);
                                }
                            }
                            List<ChangeProgram_> listCP = new List<ChangeProgram_>();
                            listCP.Add(pp);

                            op.Data = listCP;
                            break;
                        case "3"://播放控制
                            JsonstructureDeal(ref data);
                            List<PlayCtrl_> listPC = Serializer.Deserialize<List<PlayCtrl_>>(data);
                            op.Data = listPC;
                            break;
                        case "4"://输出控制
                            JsonstructureDeal(ref data);
                            List<OutSwitch_> listOS = Serializer.Deserialize<List<OutSwitch_>>(data);
                            op.Data = listOS;
                            break;
                        case "5"://RDS编码数据透传
                            JsonstructureDeal(ref data);
                            List<RdsTransfer_> listRT = Serializer.Deserialize<List<RdsTransfer_>>(data);
                            op.Data = listRT;
                            break;
                    }
                    break;

                case "DelDailyBroadcast":
                    string ItemList = "72080";
                    op.Data = ItemList;
                    break;
            }
            DataDealHelper.MyEvent(op);
        }


        private void AnalysisEBContentData(IPrimitiveMap map)
        {
            string packetype = map["PACKETTYPE"].ToString();
            OperatorData op = new OperatorData();
            op.OperatorType = packetype;
            JavaScriptSerializer Serializer = new JavaScriptSerializer();

            switch (packetype)
            {
                case "AddEBContent":

                case "ModifyEBContent":
                    string dataAdd = map["data"].ToString();
                    JsonstructureDeal(ref dataAdd);
                    List<EBMID_Content> listEBContent_ = Serializer.Deserialize<List<EBMID_Content>>(dataAdd);

                    if (!SingletonInfo.GetInstance().IsGXProtocol)
                    {
                        for (int i = 0; i < listEBContent_.Count; i++)
                        {
                            listEBContent_[i].EBM_ID = BBSHelper.CreateEBM_ID();
                        }
                    }
                    op.Data = (object)listEBContent_;
                    break;

                case "DelDailyBroadcast":
                    string ItemList = map["ItemIDList"].ToString();
                    op.Data = ItemList;
                    break;
            }
            DataDealHelper.MyEvent(op);
        }

        private void ChangeInputChannel(IPrimitiveMap map)
        {
            string packetype = map["PACKETTYPE"].ToString();
            OperatorData op = new OperatorData();
            op.OperatorType = packetype;
            switch (packetype)
            {
                case "ChangeInutChannel":
                    CountyplatformChangechannel changechannelinfo = new CountyplatformChangechannel();
                    changechannelinfo.inputchannel = map["inputchannel"].ToString().Trim();
                    changechannelinfo.PhysicalCode =map["PhysicalCode"].ToString().Trim();
                    //changechannelinfo.PhysicalCode = "192.168.3.233:5005";
                    changechannelinfo.ResourceCode= map["ResourceCode"].ToString().Trim();
                    op.Data = (object)changechannelinfo;
                    break;
            }
            DataDealHelper.MyEvent(op);
        }

        public List<byte[]> GetSendCert(List<Cert_> certList)
        {
            if (certList.Count == 0)
            {
                return null;
            }
            List<byte[]> list = new List<byte[]>();
            foreach (var cert in certList)
            {
                if (cert.SendState)
                {
                    //if (cert.Tag == 1)
                    //{
                    //    list.Add(Encoding.GetEncoding("GB2312").GetBytes(cert.Cert_data));
                    //}
                    List<byte> Cert_dataList = new List<byte>();
                    string[] pp = cert.Cert_data.Trim().Split(' ');
                    foreach (string item in pp)
                    {
                        Cert_dataList.Add((byte)Convert.ToInt32(item,16));
                    }

                    list.Add(Cert_dataList.ToArray());
                }
            }
            return list;
        }


        public List<EBIndex> GetSendEBMIndex(List<EBMIndex_> EBIndex_List)
        {
            if (EBIndex_List.Count == 0)
            {
                return null;
            }
            List<EBIndex> list = new List<EBIndex>();
            foreach (var index in EBIndex_List)
            {
                if (index.SendState)
                {
                    list.Add(index.EBIndex);
                    if (!index.DesFlag)
                    {
                        list[list.Count - 1].DetlChlDescriptor = null;
                    }
                }
            }
            return list;
        }

        public List<byte[]> GetSendCertAuth(List<CertAuth_> certAuthList)
        {
            if (certAuthList.Count == 0)
            {
                return null;
            }
            List<byte[]> list = new List<byte[]>();
            foreach (var cert in certAuthList)
            {
                if (cert.SendState)
                {
                    //if (cert.Tag == 1)
                    //{
                    //    list.Add(Encoding.GetEncoding("GB2312").GetBytes(cert.CertAuth_data));
                    //}

                    List<byte> dataList = new List<byte>();
                    string[] pp = cert.CertAuth_data.Trim().Split(' ');
                    foreach (string item in pp)
                    {
                        dataList.Add((byte)Convert.ToInt32(item, 16));
                    }

                    list.Add(dataList.ToArray());
                }
            }
            return list;
        }
    }
}
