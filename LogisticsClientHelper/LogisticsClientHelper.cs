using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace LogisticsClientHelper
{
    #region API
    public class LogisticsCommonConstant
    {
        public const string _Y = "Y";
        public const string _N = "N";
        public const string _BLANK = "";        

        /**
         * API 연동 정보
         * URL_API_DEV : 연동 개발 서버
         * URL_API_REAL : 연동 실서버 서버
         * */
        public const string URL_API_DEV = "http://10.1.104.8:5110";
        public const string URL_API_REAL = "https://kfs.kyowon.co.kr";

        public static readonly Hashtable HASH_URL_API = new Hashtable()
        {
            //인터페이스 ID로 정리
            //KFS_API_GOODS_0001 : 상품등록 및 수정
            {"KFS_API_GOODS_0001", "/wms/api/oms/prdt/RGSN_PRDT"},            
            //KFS_API_OUT_0001 : 출고등록
            {"KFS_API_OUT_0001", "/wms/api/oms/rls/RGSN_LRQNT_RLS"}
        };

        public static string GetApiUrl(string api)
        {
            return HASH_URL_API[api].ToString();
        }

        public const string KFS_API_WMS_CODE = "EU22042501";
        public const string KFS_API_HMSP_ID = "1111";

        public const string KFS_API_RESULT_FAIL_KFS = "정기주문등록실패 KFS : ";

        public const string ORDER_TYPE_CD_NEW = "2";
        public const string ORDER_TYPE_CD_ROUTINE = "1";
    }
    #endregion

    #region API REQUEST BODY, RESPONSE BODY

    #region 상품 KFS_API_GOODS_0001
    public class KFS_API_GOODS_0001_REQUEST
    {
        public string ADMN_CD { get; set; }
        public string PRDT_NM { get; set; }
        public string BZPT_PRDT_CD { get; set; }
        public string STT_DT { get; set; }
        public string END_DT { get; set; }
    }

    public class KFS_API_GOODS_0001_RESPONSE
    {
        public string resultCode { get; set; }
        public string resultMsg { get; set; }
    }
    #endregion

    #region 출고 KFS_API_OUT_0001
    public class KFS_API_OUT_0001_REQUEST
    {
        public string ADMN_CD { get; set; }                     //WMS고객번호
        public IList<RLS_LIST_REQUEST> RLS_LIST = new List<RLS_LIST_REQUEST>();
        public class RLS_LIST_REQUEST
        {
            public string ADMN_CD { get; set; }                 //WMS고객번호
            public string ORDR_NO { get; set; }                 //주문번호
            public string ORDR_DT { get; set; }                 //주문일자
            public string ORDR_TYP { get; set; }                //주문유형 (01 : 정상, 02 : 교환, 03 : A/S출고, 04 : 업무용, 05 : 환불, 10 : 복합)
            public string RLS_RSN_CD { get; set; }              //출고사유 (001 : 정상판매, 002 : 기타판매, 003 : A/S출고, 004 : 이체출고, 005 : 기타출고, 006 : 업무용출고)
            public string SNDG_NM { get; set; }                 //보내는사람
            public string RCIP_NM { get; set; }                 //받는사람
            public string RCIP_TLNO { get; set; }               //받는사람 연락처
            public string RCIP_ZPCD { get; set; }               //받는사람 우편번호
            public string RCIP_ADDR { get; set; }               //받는사람 배송지주소
            public string RCIP_ADDR_DTL { get; set; }           //받는사람 배송지주소 상세
            public string NOTE { get; set; }                    //배송요청사항
            public string HMSP_ID { get; set; }                 //쇼핑몰ID (1111 default)            
            public string BZPT_PRDT_CD { get; set; }            //고객사 상품코드
            public int QNT { get; set; }                        //수량
            public int AMT { get; set; }                        //금액            
        }
    }

    public class KFS_API_OUT_0001_RESPONSE
    {
        public string resultCode { get; set; }
        public string resultMsg { get; set; }  
        
        public ERR_LIST_RESPONSE ERR_LIST = new ERR_LIST_RESPONSE();
        public class ERR_LIST_RESPONSE
        {
            public string ADMN_CD { get; set; }                 //WMS고객번호
            public string ORDR_NO { get; set; }                 //주문번호
            public string BZPT_PRDT_CD { get; set; }            //고객사 상품코드
            public string ERR_01 { get; set; }                  //고객번호 오류 (O : 고객번호 조회불가, X : 정상)
            public string ERR_02 { get; set; }                  //주문번호 오류 (O : 주문번호 조회불가, X : 정상)
            public string ERR_03 { get; set; }                  //주문일자 오류 (O : 주문일자 조회불가, X : 정상)
            public string ERR_04 { get; set; }                  //수신/발신인 오류 (O : 수신인, 발신인 정보가 없는경우, X : 정상)
            public string ERR_05 { get; set; }                  //전화번호 오류 (O : 전화번호 없거나 10자리 미만인 경우, X : 정상)
            public string ERR_06 { get; set; }                  //우편번호 오류 (O : 우편번호 정보가 없거나 KFS 미등록인 경우 또는 일반/세트 상품코드가 중복인 경우, X : 정상)
            public string ERR_07 { get; set; }                  //배송지 오류 (O : 배송지정보 조회불가, X : 정상)
            public string ERR_08 { get; set; }                  //쇼핑몰ID 오류 (O : 쇼핑몰ID 정보가 없거나 KFS 미등록인 경우, X : 정상)
            public string ERR_09 { get; set; }                  //고객사 상품코드 오류 (O : 고객사 상품코드가 없거나 KFS 미등록인 경우 또는 일반/세트 상품코드가 중복인 경우, X : 정상)
            public string ERR_10 { get; set; }                  //수량 오류 (O : 수량 정보가 없거나 0이하인 경우, X : 정상)
            public string ERR_11 { get; set; }                  //유통기한오류 오류 (O : 유통기한이 날짜형식이 아니거나 등록 당일 이전인 경우, X : 정상)            
        }

        public DUP_LIST_RESPONSE DUP_LIST = new DUP_LIST_RESPONSE();
        public class DUP_LIST_RESPONSE
        {
            public string ORDR_NO { get; set; }                 //주문번호        
        }
    }
    #endregion

    #endregion

    public abstract class FactoryController
    {
        public abstract IApiTypeFactory MakeApiTypeFactory(string gubun);
    }

    public class ConcreateApiFactory : FactoryController
    {
        public override IApiTypeFactory MakeApiTypeFactory(string gubun)
        {
            switch (gubun)
            {
                case "post":
                    return new PostApiClientHelper(); //POST API
                case "get":
                    return new PostApiClientHelper(); //GET API TO-BE : 구현예정
                default:
                    return new PostApiClientHelper();
            }
        }
    }

    public interface IApiTypeFactory
    {
        string ApiAsync(string url, Object obj);
    }

    class PostApiClientHelper : Controller, IApiTypeFactory
    {
        static HttpClient client = new HttpClient();

        public string ApiAsync(string url, Object body)
        {
            try
            {
                #region api url 생성 및 parameter jsonString으로 변환                
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                string param = serializer.Serialize(body); //모든 request를 처리할 수 있도록 객체를 jsonstring으로 변환
                #endregion

                var res = HttpClientHelper.apiRequest(url, param);
                return HttpClientHelper.streamEncode(res);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }

    #region httpClientHelper
    public static class HttpClientHelper
    {
        /// <summary>
        /// GET 방식 webapi 호출
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string RequestGet(string url)
        {
            //WebRequest.CreateHttp(url).;
            using (var web = new WebClient())
            {
                web.Encoding = Encoding.UTF8;
                return web.DownloadString(url);
            }
        }

        /// <summary>
        /// POST방식 webapi 호출
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        public static string RequestPost(string url, string data, string contentType = "application/json")
        {
            using (var web = new WebClient())
            {
                web.Encoding = Encoding.UTF8;
                if (!string.IsNullOrWhiteSpace(contentType))
                {
                    web.Headers["Content-Type"] = contentType;
                }

                return web.UploadString(url, "POST", data);
            }
        }

        public static HttpWebResponse apiRequest(String url, String postData)
        {
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            var request = (HttpWebRequest)WebRequest.Create(url);

            Encoding euckr = Encoding.GetEncoding(65001);// 51949:  euc-kr    , 65001:utf-8
            var data = euckr.GetBytes(postData);

            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = data.Length;
            request.Timeout = 5000;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            var result = (HttpWebResponse)request.GetResponse();
            return result;
        }
        public static String apiGetRequest(String url)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "Get";
            request.Timeout = 5000;
            var result = (HttpWebResponse)request.GetResponse();

            Stream ReceiveStream = result.GetResponseStream();
            Encoding encode = Encoding.GetEncoding(65001); // 51949:  euc-kr    , 65001:utf-8

            string responseText = string.Empty;
            string reutrnText = string.Empty;
            using (StreamReader sr = new StreamReader(ReceiveStream))
            {
                responseText = sr.ReadToEnd();
            }

            char[] arr = responseText.ToArray();
            bool bln = true;
            foreach (char item in arr)
            {
                if (bln)
                {
                    if (item == '}')
                    {
                        reutrnText += item.ToString();
                        break;
                    }
                    else
                    {
                        reutrnText += item.ToString();
                    }
                }
            }
            return reutrnText;
        }
        public static String streamEncode(HttpWebResponse result)
        {
            Stream ReceiveStream = result.GetResponseStream();
            Encoding encode = Encoding.GetEncoding(65001); // 51949:  euc-kr    , 65001:utf-8

            StreamReader sr = new StreamReader(ReceiveStream, encode);
            string resultText = sr.ReadToEnd();
            return resultText;
        }
    }
    #endregion
}