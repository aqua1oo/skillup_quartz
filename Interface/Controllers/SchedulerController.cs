using IBatisNet.DataMapper;
using Interface.Entity;
using LogisticsClientHelper;
using Newtonsoft.Json;
using Quartz;
using Quartz.Impl;
using Spring.Context;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Mvc;
using log4net;
using static LogisticsClientHelper.KFS_API_OUT_0001_REQUEST;

namespace Interface.Controllers
{
    public class ScheduleOrderRoutineController : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            Job job = new Job("ai_order"); //정기주문
            job.Execute();
        }
    }

    public class ScheduleOrderNewController : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            Job job = new Job("ai_order_new"); //신규주문
            job.Execute();
        }
    }

    public class JobScheduler
    {
        public static void Start()
        {
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Start();
            IJobDetail CampainDelJob = JobBuilder.Create<ScheduleOrderRoutineController>().Build();
            //IJobDetail CampainDelJob2 = JobBuilder.Create<ScheduleOrderNewController>().Build();

            DateTimeOffset startTime = DateBuilder.NextGivenSecondDate(null, 15);
            ICronTrigger trigger = (ICronTrigger)TriggerBuilder.Create()
                                                          .WithIdentity("trigger1", "group1")
                                                          .WithCronSchedule(GetEveryMonth(26, 02, 00)) //매달 26일 오전 2시
                                                          .StartAt(startTime)
                                                          .Build();
            //1분마다
            //ITrigger trigger2 = TriggerBuilder.Create().WithDailyTimeIntervalSchedule(s => s.WithIntervalInMinutes(1)).Build();

            //매일 00시 10분에 실행
            //ITrigger trigger = TriggerBuilder.Create().WithDailyTimeIntervalSchedule(s => s.WithIntervalInHours(24).OnEveryDay().StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(00, 50))).Build();            
            //1시간마다
            //ITrigger trigger = TriggerBuilder.Create().WithDailyTimeIntervalSchedule(s => s.WithIntervalInHours(1)).Build();

            scheduler.ScheduleJob(CampainDelJob, trigger);
            //scheduler.ScheduleJob(CampainDelJob2, trigger2);
        }

        public static string GetEveryMonth(int date, int hour, int minute)
        {
            // 1. day 검사
            if (date > 30 && date < 1)
                return null;
            // 2.월, 시, 분 검사
            if (hour > 24 || hour < 1 || minute > 59 || minute < 0)
                return null;
            //15 1 1 * * 명령어 => 매월 1일 새벽 1:15
            string result = string.Empty;
            result += "0 ";
            result += minute.ToString();
            result += " ";
            result += hour.ToString();
            result += " ";
            result += date.ToString();
            result += " ";
            result += "1/1";
            result += " ? *";
            return result;
        }

    }

    public class Job : Controller
    {
        string jobName;
        public Job (string jobName)
        {
            this.jobName = jobName;
        }

        public void Execute()
        {            
            try
            {
                FactoryController factory = new ConcreateFactory();
                IMakeClassFactory kiccDataFactory = factory.MakeClassFactory(jobName);
                kiccDataFactory.Proc(jobName == "ai_order_new" ? LogisticsCommonConstant.ORDER_TYPE_CD_NEW : LogisticsCommonConstant.ORDER_TYPE_CD_ROUTINE); //정기주문
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        #region factory 패턴을 통하여 CLASS 생성
        public abstract class FactoryController
        {
            public abstract IMakeClassFactory MakeClassFactory(string gubun);
        }

        public class ConcreateFactory : FactoryController
        {
            public override IMakeClassFactory MakeClassFactory(string gubun)
            {
                switch (gubun)
                {
                    case "ai_order":
                        return new MakeAIClassOrderFactory(); //KFS 정기주문 
                    case "ai_order_new":
                        return new MakeAIClassOrderFactory(); 
                    default:
                        return new MakeAIClassOrderFactory();
                }
            }
        }

        public interface IMakeClassFactory
        {
            void Proc(string orderTypeCd);
        }

        #region AICANDO에서 사용하는 메서드
        public class MakeAIClassOrderFactory : IMakeClassFactory
        {
            private static readonly ILog log = LogManager.GetLogger(typeof(MakeAIClassOrderFactory));

            public void Proc(string orderTypeCd)
            {                
                try
                {
                    #region API 처리 factory
                    var url = LogisticsCommonConstant.URL_API_DEV + LogisticsCommonConstant.GetApiUrl("KFS_API_OUT_0001");  //개발 : URL_API_DEV, 운영 : URL_API_REAL로 변경
                    LogisticsClientHelper.FactoryController factory = new ConcreateApiFactory();
                    IApiTypeFactory apiTypeFactory = factory.MakeApiTypeFactory("post"); //post, get  

                    #region body 처리
                    KFS_API_OUT_0001_REQUEST body = new KFS_API_OUT_0001_REQUEST();
                    body.ADMN_CD = LogisticsCommonConstant.KFS_API_WMS_CODE;
                    #endregion

                    Hashtable htParam = new Hashtable()
                    {
                        {"organ_inf_cd", LogisticsCommonConstant._BLANK},
                        {"order_id", LogisticsCommonConstant._BLANK},
                        {"order_type_cd", orderTypeCd}
                    };

                    IList<Kfs> dataList = Mapper.Instance().QueryForList<Kfs>("KfsOrderSendList", htParam);

                    if (dataList.Count > 0)
                    {
                        #region 주문리스트                         
                        foreach (var item in dataList)
                        {
                            RLS_LIST_REQUEST bodyOrder = new RLS_LIST_REQUEST();
                            bodyOrder.ADMN_CD = LogisticsCommonConstant.KFS_API_WMS_CODE;
                            bodyOrder.ORDR_NO = LogisticsCommonConstant.KFS_API_WMS_CODE + item.ordr_no;
                            bodyOrder.ORDR_DT = item.ordr_dt;                                                       //yyyymmdd
                            bodyOrder.ORDR_TYP = item.ordr_typ;                                                     //주문유형 (01 : 정상, 02 : 교환, 03 : A/S출고, 04 : 업무용, 05 : 환불, 10 : 복합)
                            bodyOrder.RLS_RSN_CD = item.rls_rsn_cd;                                                 //출고사유 (001 : 정상판매, 002 : 기타판매, 003 : A/S출고, 004 : 이체출고, 005 : 기타출고, 006 : 업무용출고)
                            bodyOrder.SNDG_NM = item.sndg_nm;                                                       //보내는사람
                            bodyOrder.RCIP_NM = item.rcip_nm;                                                       //받는사람
                            bodyOrder.RCIP_TLNO = item.rcip_tlno;                                                   //받는사람 연락처
                            bodyOrder.RCIP_ZPCD = item.rcip_zpcd;                                                   //우편번호
                            bodyOrder.RCIP_ADDR = item.rcip_addr;                                                   //배송지 주소
                            bodyOrder.RCIP_ADDR_DTL = item.rcip_addr_dtl;                                           //배송지 상세주소
                            bodyOrder.NOTE = item.note;                                                             //배송요청사항
                            bodyOrder.HMSP_ID = LogisticsCommonConstant.KFS_API_HMSP_ID;
                            bodyOrder.BZPT_PRDT_CD = LogisticsCommonConstant.KFS_API_WMS_CODE + item.bzpt_prdt_cd;  //상품코드 (goods_id)
                            bodyOrder.QNT = item.qnt;                                                               //수량
                            bodyOrder.AMT = item.amt;                                                               //금액
                            body.RLS_LIST.Add(bodyOrder);
                        }
                        #endregion

                        string response = apiTypeFactory.ApiAsync(url, body);
                        var result = JsonConvert.DeserializeObject<KFS_API_OUT_0001_RESPONSE>(response); //인터페이스ID값_Response 객체명으로 response 객체 생성

                        int updateResult = 0;
                        #region
                        if (result.resultCode == "0000")
                        {
                            foreach (var item in dataList)
                            {
                                Hashtable htParam2 = new Hashtable()
                                {
                                    {"order_id", item.ordr_no}
                                };

                                updateResult += Mapper.Instance().QueryForObject<int>("KfsOrderSendUpdate", htParam2);
                            }                            
                        }
                        #endregion

                        log.Error(DateTime.Now.ToString("yyyyMMdd") + LogisticsCommonConstant._BLANK + LogisticsCommonConstant.KFS_API_RESULT_FAIL_KFS + result.resultCode + LogisticsCommonConstant._BLANK + result.resultMsg + LogisticsCommonConstant._BLANK + dataList.Count + LogisticsCommonConstant._BLANK + updateResult);
                    }
                    #endregion                                                
                }
                catch (Exception e)
                {
                    log.Error(e.Message);
                }           
            }
        }
        #endregion

        #endregion
    }
}