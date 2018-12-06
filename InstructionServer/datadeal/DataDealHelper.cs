
using Apache.NMS;
using EBMTable;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
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
                    tmp.S_EBM_id = map["S_EBM_id"].ToString().Substring(0, 30);
                    #region   特殊处理EBM_ID 凑到30个长度
                    //if (map["S_EBM_id"].ToString().Length < 30)
                    //{
                    //    tmp.IndexItemID = map["S_EBM_id"].ToString() + "000000000000";
                    //}
                    //else
                    //{
                    //    tmp.S_EBM_id = map["S_EBM_id"].ToString().Substring(0, 30);
                    //}
                    #endregion



                    tmp.S_EBM_original_network_id = (SingletonInfo.GetInstance().OriginalNetworkId + 1).ToString();
                    tmp.S_EBM_start_time = map["S_EBM_start_time"].ToString();
                    tmp.S_EBM_end_time = map["S_EBM_end_time"].ToString();
                    tmp.S_EBM_type = map["S_EBM_type"].ToString();
                    tmp.S_EBM_class = map["S_EBM_class"].ToString();
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

                            if (item.B_stream_type=="84")
                            {
                                item.B_stream_type = "03";
                            }
                        }
                        tmp.List_ProgramStreamInfo = objs;

                    }
                    op.Data = (object)tmp;
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
                    op.Data = map["IndexItemID"].ToString();
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
                            op.Data = listWM;
                            break;
                        case "4"://锁定频率设置
                            JsonstructureDeal(ref data);
                            List<MainFrequency_> listMF = Serializer.Deserialize<List<MainFrequency_>>(data);
                            op.Data = listMF;
                            break;
                        case "5"://回传方式设置
                            JsonstructureDeal(ref data);
                            //又要特殊处理

                            string tmp1 = data.Replace("\"S_reback_address_backup\":,", "\"S_reback_address_backup\":\"null\",");
                            string tmp2 = tmp1.Replace("\"I_reback_port_Backup\":,", "\"I_reback_port_Backup\":0,");
                            List<Reback_> listRB = Serializer.Deserialize<List<Reback_>>(tmp2);
                            op.Data = listRB;
                            break;
                        case "6"://默认音量设置
                            JsonstructureDeal(ref data);
                            List<DefaltVolume_> listDV = Serializer.Deserialize<List<DefaltVolume_>>(data);
                            op.Data = listDV;
                            break;
                        case "7"://回传周期设置
                            JsonstructureDeal(ref data);
                            List<RebackPeriod_> listRP = Serializer.Deserialize<List<RebackPeriod_>>(data);
                            op.Data = listRP;
                            break;
                        case "104"://启动内容检测指令
                            JsonstructureDeal(ref data);
                            List<ContentMoniterRetback_> listCMR = Serializer.Deserialize<List<ContentMoniterRetback_>>(data);
                            op.Data = listCMR;
                            break;
                        case "105"://启动内容监测实时监听指令
                            JsonstructureDeal(ref data);
                            if (SingletonInfo.GetInstance().IsGXProtocol)
                            {
                                List<ContentRealMoniterGX_> listCRMGX = Serializer.Deserialize<List<ContentRealMoniterGX_>>(data);
                                op.Data = listCRMGX;
                            }
                            else
                            {
                                List<ContentRealMoniter_> listCRM = Serializer.Deserialize<List<ContentRealMoniter_>>(data);
                                op.Data = listCRM;
                            }
                            break;
                        case "106"://终端工作状态查询
                            JsonstructureDeal(ref data);

                              // data.IndexOf(',')
                            List<StatusRetback_> listSR = Serializer.Deserialize<List<StatusRetback_>>(data);
                            op.Data = listSR;
                            break;
                        case "240"://终端固件升级
                            JsonstructureDeal(ref data);
                            List<SoftwareUpGrade_> listSUG = Serializer.Deserialize<List<SoftwareUpGrade_>>(data);
                            op.Data = listSUG;
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

        public void TestFunc1()
        {
            string packetype = "DelDailyBroadcast";
            OperatorData op = new OperatorData();
            op.OperatorType = packetype;
            op.ModuleType = "1";
            JavaScriptSerializer Serializer = new JavaScriptSerializer();

            switch (packetype)
            {
                case "DelDailyBroadcast":
                    string ItemList = "72076";
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
                    if (cert.Tag == 1)
                    {
                        list.Add(Encoding.GetEncoding("GB2312").GetBytes(cert.Cert_data));
                    }
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
                    if (cert.Tag == 1)
                    {
                        list.Add(Encoding.GetEncoding("GB2312").GetBytes(cert.CertAuth_data));
                    }
                }
            }
            return list;
        }
    }
}
