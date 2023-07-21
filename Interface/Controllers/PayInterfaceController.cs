using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using LogisticsClientHelper;
using static LogisticsClientHelper.KFS_API_OUT_0001_REQUEST;

namespace Interface.Controllers
{
    public class PayInterfaceController : Controller
    {
        //#region 차세대API
        //public void ApiClientTest()
        //{
        //    #region API 처리 factory
        //    var url = "http://d-eai.kyowon.co.kr:10001" + ApiCommonConstant.GetApiUrl("EAI_ECLI1004"); //인터페이스ID값으로 URL 호출            
        //    FactoryController factory = new ConcreateApiFactory();
        //    IApiTypeFactory apiTypeFactory = factory.MakeApiTypeFactory("post"); //post, get  

        //    #region header 변동부분만 처리
        //    Header header = new Header();
        //    header.STD_ETXT_IF_ID = "EAI_ECLI1004"; //인터페이스ID값
        //    header.CHNL_MEDI_DV_CD = "1"; //TO-BE : 차세대관리시스템 개발부서에서 정해서 주기로함.
        //    header.STD_ETXT_AK_PRG_ID = HttpContext.Request.Url.AbsolutePath;
        //    header.LGN_USR_ID = User == null ? "NULL" : "123456789";
        //    #endregion

        //    #region body 처리
        //    EAI_ECLI1004 body = new EAI_ECLI1004(); //인터페이스ID값으로 body 객체 생성
        //    body.CNTR_CST_NO = "011416161";
        //    #endregion

        //    string response = apiTypeFactory.ApiAsync(url, body, header);
        //    var result = JsonConvert.DeserializeObject<EAI_ECLI1004_RESPONSE>(response); //인터페이스ID값_Response 객체명으로 response 객체 생성

        //    foreach (var item in result.body)
        //    {
        //        Console.WriteLine(item.MB_CST_NM);
        //    }
        //    #endregion
        //}
        //#endregion


        //#region KICC결제모듈

        //#region SMS결제
        //#region SMS발송요청
        //public ActionResult RequestKiccPayModule(directRegInfoRequest directRegInfo, directOrderInfoRequest directOrderInfo)
        //{
        //    try
        //    {
        //        #region API 처리 factory
        //        var url = PayCommonConstant.URL_API_REAL + PayCommonConstant.GetApiUrl("KICC_API0001");
        //        FactoryController factory = new ConcreateApiFactory();
        //        IApiTypeFactory apiTypeFactory = factory.MakeApiTypeFactory("post"); //post, get  

        //        #region body 처리
        //        KICC_API0001_REQUEST body = new KICC_API0001_REQUEST();

        //        #region 결제 기본정보
        //        body.directRegInfo.mallId = PayCommonConstant.KICC_MALLID_AICANDO_SMS;
        //        body.directRegInfo.regTxtype = PayCommonConstant.KICC_REGTYPE_SMS;
        //        body.directRegInfo.regSubtype = PayCommonConstant.KICC_REGSUBTYPE_PAY;     //결제요청 : KICC_REGSUBTYPE_PAY
        //        body.directRegInfo.amount = directRegInfo.amount;                          //결제총금액
        //        body.directRegInfo.currency = PayCommonConstant.KICC_CURRENCY_WON;
        //        body.directRegInfo.payCode = PayCommonConstant.KICC_PAYCODE_ALL;
        //        body.directRegInfo.rcvMobileNo = directRegInfo.rcvMobileNo;                //고객전화번호(-)없이
        //        body.directRegInfo.sndTelNo = directRegInfo.sndTelNo;                      //보내는사람전화번호(-)없이
        //        body.directRegInfo.mallName = "AICANDO CLASS " + directRegInfo.mallName;   //교실명    
        //        DateTime nextMonthFirstDay = DateTime.Now.AddDays(1 - DateTime.Now.Day);                
        //        body.directRegInfo.smsPayExpr = nextMonthFirstDay.AddMonths(1).AddDays(-1).ToString("yyyyMMdd") + "235959";
        //        body.directRegInfo.dispMsg = directRegInfo.dispMsg;                        //문구 (교원 아이캔두클래스 수강료(9월분) 결제)
        //        #endregion

        //        #region 결제 주문정보
        //        body.directOrderInfo.shopOrderNo = directOrderInfo.shopOrderNo;            //주문번호
        //        body.directOrderInfo.goodsAmount = directOrderInfo.goodsAmount;            //금액
        //        body.directOrderInfo.customerId = directOrderInfo.customerId;              //고객id
        //        body.directOrderInfo.customerName = directOrderInfo.customerName;          //고객명 
        //        body.directOrderInfo.goodsName = directOrderInfo.goodsName;                //상품명
        //        body.directOrderInfo.value1 = directOrderInfo.value1;                      //기타1
        //        body.directOrderInfo.value2 = directOrderInfo.value2;                      //기타2
        //        body.directOrderInfo.value3 = directOrderInfo.value3;                      //기타3
        //        #endregion
        //        #endregion

        //        string response = apiTypeFactory.ApiAsync(url, body);
        //        var result = JsonConvert.DeserializeObject<KICC_API0001_RESPONSE>(response); //인터페이스ID값_Response 객체명으로 response 객체 생성

        //        #region
        //        if (result.resCd == "0000")
        //        {
        //            //TO-DO : 해당 거래건 요청상태로 변경로직 추가
        //        }
        //        #endregion

        //        return Json(result.resCd, JsonRequestBehavior.AllowGet);
        //        #endregion                
        //    }
        //    catch (Exception e)
        //    {
        //        //return 결제오류
        //        return Json(e.Message, JsonRequestBehavior.AllowGet);
        //    }
        //}
        //#endregion

        //#region SMS발송요청취소
        //public ActionResult RequestKiccPayCancelModule(KICC_API0002_REQUEST directCancelInfo)
        //{
        //    try
        //    {
        //        #region API 처리 factory
        //        var url = PayCommonConstant.URL_API_REAL + PayCommonConstant.GetApiUrl("KICC_API0002");
        //        FactoryController factory = new ConcreateApiFactory();
        //        IApiTypeFactory apiTypeFactory = factory.MakeApiTypeFactory("post"); //post, get  

        //        #region body 처리
        //        KICC_API0002_REQUEST body = new KICC_API0002_REQUEST();

        //        #region 결제 기본정보
        //        body.mallId = PayCommonConstant.KICC_MALLID_AICANDO_SMS;
        //        body.reviseTypeCode = PayCommonConstant.KICC_REVISETYPECODE_CANCEL;
        //        body.reviseSubTypeCode = PayCommonConstant.KICC_REVISESUBTYPECODE_CANCEL;
        //        body.pgCno = directCancelInfo.pgCno;                                    //결제요청시 return 받은 pgCno
        //        body.clientIp = System.Web.HttpContext.Current.Request.UserHostAddress;
        //        body.clientId = System.Web.HttpContext.Current.Request.UserHostAddress; //user정보
        //        #endregion
        //        #endregion

        //        string response = apiTypeFactory.ApiAsync(url, body);
        //        var result = JsonConvert.DeserializeObject<KICC_API0002_RESPONSE>(response); //인터페이스ID값_Response 객체명으로 response 객체 생성

        //        #region
        //        if (result.resCd == "0000")
        //        {
        //            //TO-DO : 해당 거래건 요청취소상태로 변경로직 추가
        //        }
        //        #endregion

        //        return Json(result.resCd, JsonRequestBehavior.AllowGet);
        //        #endregion                
        //    }
        //    catch (Exception e)
        //    {
        //        //return 결제오류
        //        return Json(e.Message, JsonRequestBehavior.AllowGet);
        //    }
        //}
        //#endregion
        //#endregion

        //#region 결제창결제
        //#region 결제창요청
        //public ActionResult RequestKiccAccountPayModule(KICC_API0003_REQUEST request, orderInfoRequest orderInfo, directOrderInfoRequest directOrderInfo)
        //{
        //    try
        //    {
        //        #region API 처리 factory
        //        var url = PayCommonConstant.URL_API_REAL + PayCommonConstant.GetApiUrl("KICC_API0003");
        //        FactoryController factory = new ConcreateApiFactory();
        //        IApiTypeFactory apiTypeFactory = factory.MakeApiTypeFactory("post"); //post, get  

        //        #region body 처리
        //        KICC_API0003_REQUEST body = new KICC_API0003_REQUEST();

        //        #region 결제 기본정보
        //        body.mallId = PayCommonConstant.KICC_MALLID_AICANDO_WINDOW;
        //        body.payMethodTypeCode = request.payMethodTypeCode;                        //결제수단 (11 : 신용카드, 21 : 계좌이체, 22 : 가상계좌, 31 : 휴대폰, 50 : 선불결제, 60 : 간편결제, 81 : 배치인증)
        //        body.currency = PayCommonConstant.KICC_CURRENCY_WON;
        //        body.amount = request.amount;                                              //결제총금액                
        //        body.clientTypeCode = PayCommonConstant.KICC_WINDOW_MODE_00;
        //        body.returnUrl = PayCommonConstant.KICC_WINDOW_RETURN_URL;
        //        body.shopOrderNo = request.shopOrderNo;                                    //주문번호
        //        body.deviceTypeCode = PayCommonConstant.KICC_WINDOW_DEVICE_TYPE_PC;        //디바이스타입 (KICC_WINDOW_DEVICE_TYPE_PC : pc, KICC_WINDOW_DEVICE_TYPE_MOBILE : mobile)                
        //        #endregion

        //        #region 결제 주문정보                
        //        body.orderInfo.goodsName = orderInfo.goodsName;                            //상품명
        //        #endregion

        //        #region 결제 방법정보                
        //        DateTime nextMonthFirstDay = DateTime.Now.AddDays(1 - DateTime.Now.Day);
        //        body.payMethodInfo.virtualAccountMethodInfo.expiryDate = nextMonthFirstDay.AddMonths(1).AddDays(-1).ToString("yyyyMMdd");
        //        body.payMethodInfo.virtualAccountMethodInfo.expiryTime = "235959";
        //        #endregion

        //        #region 기타정보
        //        body.shopValueInfo.value1 = request.payMethodTypeCode;                     //결제수단으로 분기위해 사용
        //        #endregion
        //        #endregion

        //        string response = apiTypeFactory.ApiAsync(url, body);
        //        var result = JsonConvert.DeserializeObject<KICC_API0003_RESPONSE>(response); //인터페이스ID값_Response 객체명으로 response 객체 생성

        //        return Json(result.authPageUrl, JsonRequestBehavior.AllowGet);               //해당 authPageUrl 팝업으로 오픈
        //        #endregion                
        //    }
        //    catch (Exception e)
        //    {
        //        //return 결제오류
        //        return Json(e.Message, JsonRequestBehavior.AllowGet);
        //    }
        //}
        //#endregion

        //#region 결제창 처리후 RETURN
        //public ActionResult KiccPayReturn(KICC_API0003_RETURNURL_RESPONSE request)
        //{
        //    try
        //    {
        //        #region API 처리 factory
        //        var url = PayCommonConstant.URL_API_REAL + PayCommonConstant.GetApiUrl("KICC_API0004");
        //        FactoryController factory = new ConcreateApiFactory();
        //        IApiTypeFactory apiTypeFactory = factory.MakeApiTypeFactory("post"); //post, get  

        //        #region body 처리
        //        KICC_API0004_REQUEST body = new KICC_API0004_REQUEST();

        //        #region 결제 기본정보
        //        body.mallId = PayCommonConstant.KICC_MALLID_AICANDO_WINDOW;
        //        body.shopTransactionId = "KYOWONAICANDOPAYSEQ_" + request.shopOrderNo + DateTime.Now.ToString("yyyyMMddhhmmss");
        //        body.authorizationId = request.authorizationId;
        //        body.shopOrderNo = request.shopOrderNo;
        //        body.approvalReqDate = DateTime.Now.ToString("yyyyMMdd");
        //        #endregion
        //        #endregion

        //        if (request.resCd == "0000")
        //        {
        //            string response = apiTypeFactory.ApiAsync(url, body);
        //            var result = JsonConvert.DeserializeObject<KICC_API0004_RESPONSE>(response); //인터페이스ID값_Response 객체명으로 response 객체 생성

        //            #region
        //            if (result.resCd == "0000")
        //            {
        //                //TO-DO : 가상계좌 결제요청 후 문자 발송
        //                return Content("<script>opener.location.reload(true); self.close();</script>");
        //            }
        //            else
        //            {
        //                return Content("<script>alert('가상계좌 발급에 실패하였습니다." + result.resMsg + "');opener.location.reload(true); self.close();</script>");
        //            }
        //            #endregion
        //        }
        //        else
        //        {
        //            return Content("<script>alert('인증에 실패하였습니다. " + request.resCd + " : " + request.resMsg + "');opener.location.reload(true); self.close();</script>");
        //        }

        //        #endregion
        //    }
        //    catch (Exception e)
        //    {
        //        return Content("<script>alert('e : " + e.Message + "');opener.location.reload(true); self.close();</script>");
        //    }
        //}
        //#endregion
        //#endregion

        //#region KICC결제 공통
        //#region KICC 결제 NOTI
        //public string KiccPayComplete(KICC_API_NOTI request)
        //{
        //    try
        //    {
        //        if (request.res_cd == "0000")
        //        {
        //            #region                
        //            //TO-DO : 실결제 후 결제완료처리 result는 우리 db 처리 후 응답값
        //            //if (result != null && result.resCode == "0000")
        //            //{
        //            //    return PayCommonConstant.KICC_RESULT_SUCCESS;
        //            //}
        //            //else
        //            //{
        //            //    return PayCommonConstant.KICC_RESULT_FAIL;
        //            //}
        //            #endregion

        //            return PayCommonConstant.KICC_RESULT_SUCCESS; //TO-DO : 위 region 처리 후 삭제
        //        }
        //        else
        //        {
        //            return PayCommonConstant.KICC_RESULT_FAIL;
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        return e.Message;
        //    }
        //}
        //#endregion

        //#region KICC 결제 취소/환불
        //public ActionResult KiccPayCancel(KICC_API0005_REQUEST request, refundInfoRequest refundInfo)
        //{
        //    try
        //    {
        //        #region API 처리 factory
        //        var url = PayCommonConstant.URL_API_REAL + PayCommonConstant.GetApiUrl("KICC_API0005");
        //        FactoryController factory = new ConcreateApiFactory();
        //        IApiTypeFactory apiTypeFactory = factory.MakeApiTypeFactory("post"); //post, get  

        //        #region body 처리
        //        KICC_API0005_REQUEST body = new KICC_API0005_REQUEST();

        //        #region 환불 기본정보
        //        body.mallId = PayCommonConstant.KICC_MALLID_AICANDO_SMS;                //상점아이디(KICC_MALLID_AICANDO_SMS : SMS결제 취소, KICC_MALLID_AICANDO_WINDOW : 결제창결제 취소)
        //        body.shopTransactionId = "KYOWONAICANDOPAYCANCELSEQ_" + request.pgCno + DateTime.Now.ToString("yyyyMMddhhmmss");
        //        body.pgCno = request.pgCno;                                             //원거래번호
        //        body.reviseTypeCode = PayCommonConstant.KICC_REVISETYPECODE_REFUND;
        //        body.amount = request.amount;                                           //취소금액
        //        body.clientIp = System.Web.HttpContext.Current.Request.UserHostAddress;
        //        body.clientId = System.Web.HttpContext.Current.Request.UserHostAddress; //user정보
        //        body.msgAuthValue = HmacText.CreateHash(request.pgCno + "|" + body.shopTransactionId, PayCommonConstant.KICC_PAY_CANCEL_SECRETKEY_SMS); //암복화키 (SMS결제취소/환불 : KICC_PAY_CANCEL_SECRETKEY_SMS, 결제창결제취소/환불 : KICC_PAY_CANCEL_SECRETKEY_WINDOW)
        //        body.cancelReqDate = DateTime.Now.ToString("yyyyMMdd");
        //        #endregion

        //        //#region 환불 은행정보
        //        //body.refundInfo.refundBankCode = refundInfo.refundBankCode;             //환불계좌 은행코드 (별첨가이드 대표은행코드 확인)
        //        //body.refundInfo.refundAccountNo = refundInfo.refundAccountNo;           //환불계좌 계좌번호
        //        //body.refundInfo.refundDepositName = refundInfo.refundDepositName;       //환불계좌 예금주명
        //        //#endregion
        //        #endregion

        //        string response = apiTypeFactory.ApiAsync(url, body);
        //        var result = JsonConvert.DeserializeObject<KICC_API0005_RESPONSE>(response); //인터페이스ID값_Response 객체명으로 response 객체 생성

        //        #region
        //        if (result.resCd == "0000")
        //        {
        //            //TO-DO : 해당 거래건 요청취소상태로 변경로직 추가
        //        }
        //        #endregion

        //        return Json(result.resCd, JsonRequestBehavior.AllowGet);
        //        #endregion  
        //    }
        //    catch (Exception e)
        //    {
        //        return Json(e.Message, JsonRequestBehavior.AllowGet);
        //    }
        //}
        //#endregion
        //#endregion

        //#endregion

        #region KFS API연동    
        #region 단품상품 등록 및 수정
        public ActionResult RequestKFSAPIGOODS0001(KFS_API_GOODS_0001_REQUEST request)
        {
            try
            {
                #region API 처리 factory
                var url = LogisticsCommonConstant.URL_API_DEV + LogisticsCommonConstant.GetApiUrl("KFS_API_GOODS_0001"); //개발 : URL_API_DEV, 운영 : URL_API_REAL로 변경
                FactoryController factory = new ConcreateApiFactory();
                IApiTypeFactory apiTypeFactory = factory.MakeApiTypeFactory("post"); //post, get  

                #region body 처리
                KFS_API_GOODS_0001_REQUEST body = new KFS_API_GOODS_0001_REQUEST();                
                body.ADMN_CD = LogisticsCommonConstant.KFS_API_WMS_CODE;
                body.PRDT_NM = request.PRDT_NM;
                body.BZPT_PRDT_CD = request.BZPT_PRDT_CD;
                body.STT_DT = request.STT_DT;
                body.END_DT = request.END_DT;
                #endregion

                string response = apiTypeFactory.ApiAsync(url, body);
                var result = JsonConvert.DeserializeObject<KFS_API_GOODS_0001_RESPONSE>(response); //인터페이스ID값_Response 객체명으로 response 객체 생성

                #region
                if (result.resultCode == "0000")
                {
                    //TO-DO : 등록정상 로직처리
                }
                #endregion

                return Json(result.resultCode, JsonRequestBehavior.AllowGet);
                #endregion                
            }
            catch (Exception e)
            {
                //return 결제오류
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region 대량 출고 등록
        public ActionResult RequestKFSAPIOUT0001(List<RLS_LIST_REQUEST> request)
        {
            try
            {
                #region API 처리 factory
                var url = LogisticsCommonConstant.URL_API_DEV + LogisticsCommonConstant.GetApiUrl("KFS_API_OUT_0001");  //개발 : URL_API_DEV, 운영 : URL_API_REAL로 변경
                FactoryController factory = new ConcreateApiFactory();
                IApiTypeFactory apiTypeFactory = factory.MakeApiTypeFactory("post"); //post, get  

                #region body 처리
                KFS_API_OUT_0001_REQUEST body = new KFS_API_OUT_0001_REQUEST();
                body.ADMN_CD = LogisticsCommonConstant.KFS_API_WMS_CODE;
                #endregion

                #region 주문리스트                         
                foreach (var item in request)
                {
                    RLS_LIST_REQUEST bodyOrder = new RLS_LIST_REQUEST();
                    bodyOrder.ADMN_CD = LogisticsCommonConstant.KFS_API_WMS_CODE;
                    bodyOrder.ORDR_NO = LogisticsCommonConstant.KFS_API_WMS_CODE + item.ORDR_NO;
                    bodyOrder.ORDR_DT = item.ORDR_DT;                                                       //yyyymmdd
                    bodyOrder.ORDR_TYP = item.ORDR_TYP;                                                     //주문유형 (01 : 정상, 02 : 교환, 03 : A/S출고, 04 : 업무용, 05 : 환불, 10 : 복합)
                    bodyOrder.RLS_RSN_CD = item.RLS_RSN_CD;                                                 //출고사유 (001 : 정상판매, 002 : 기타판매, 003 : A/S출고, 004 : 이체출고, 005 : 기타출고, 006 : 업무용출고)
                    bodyOrder.SNDG_NM = item.SNDG_NM;                                                       //보내는사람
                    bodyOrder.RCIP_NM = item.RCIP_NM;                                                       //받는사람
                    bodyOrder.RCIP_TLNO = item.RCIP_TLNO;                                                   //받는사람 연락처
                    bodyOrder.RCIP_ZPCD = item.RCIP_ZPCD;                                                   //우편번호
                    bodyOrder.RCIP_ADDR = item.RCIP_ADDR;                                                   //배송지 주소
                    bodyOrder.RCIP_ADDR_DTL = item.RCIP_ADDR_DTL;                                           //배송지 상세주소
                    bodyOrder.NOTE = item.NOTE;                                                             //배송요청사항
                    bodyOrder.HMSP_ID = LogisticsCommonConstant.KFS_API_HMSP_ID;
                    bodyOrder.BZPT_PRDT_CD = LogisticsCommonConstant.KFS_API_WMS_CODE + item.BZPT_PRDT_CD;  //상품코드 (goods_id)
                    bodyOrder.QNT = item.QNT;                                                               //수량
                    bodyOrder.AMT = item.AMT;                                                               //금액                
                    body.RLS_LIST.Add(bodyOrder);
                }

                //API검증용
                //for (int i = 0; i < 2; i++)
                //{
                //    RLS_LIST_REQUEST bodyOrder = new RLS_LIST_REQUEST();

                //    bodyOrder.ADMN_CD = LogisticsCommonConstant.KFS_API_WMS_CODE;
                //    bodyOrder.ORDR_NO = LogisticsCommonConstant.KFS_API_WMS_CODE + Convert.ToString(i) + "6522246";
                //    bodyOrder.ORDR_DT = "20230714";
                //    bodyOrder.ORDR_TYP = "01";
                //    bodyOrder.RLS_RSN_CD = "001";
                //    bodyOrder.SNDG_NM = "강테스트" + Convert.ToString(i);
                //    bodyOrder.RCIP_NM = "최테스트" + Convert.ToString(i);
                //    bodyOrder.RCIP_TLNO = "010-6424-2361";
                //    bodyOrder.RCIP_ZPCD = "01430";
                //    bodyOrder.RCIP_ADDR = "서울특별시 도봉구 쌍문동 59";
                //    bodyOrder.RCIP_ADDR_DTL = "100동 100호";
                //    bodyOrder.NOTE = "";
                //    bodyOrder.HMSP_ID = LogisticsCommonConstant.KFS_API_HMSP_ID;
                //    bodyOrder.BZPT_PRDT_CD = LogisticsCommonConstant.KFS_API_WMS_CODE + (i == 0 ? "5000000047" : "5000000031");  //상품코드 (goods_id)
                //    bodyOrder.QNT = 1;                                                                                           //수량
                //    bodyOrder.AMT = 10;                                                                                          //금액                    

                //    body.RLS_LIST.Add(bodyOrder);
                //}
                #endregion

                string response = apiTypeFactory.ApiAsync(url, body);
                var result = JsonConvert.DeserializeObject<KFS_API_OUT_0001_RESPONSE>(response); //인터페이스ID값_Response 객체명으로 response 객체 생성

                #region
                if (result.resultCode == "0000")
                {
                    //TO-DO : 등록정상 로직처리
                }
                #endregion

                return Json(result.resultCode, JsonRequestBehavior.AllowGet);
                #endregion                
            }
            catch (Exception e)
            {
                //return 결제오류
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
        #endregion        
    }
}