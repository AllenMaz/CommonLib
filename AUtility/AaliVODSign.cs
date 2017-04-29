using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace AUtility
{
    public class AaliVODSign
    {
        private static string _AccessKey = "";
        private static string _AccessSecret = "";
        private static Dictionary<string, string> _requestParams;

        public static string Sign()
        {

            const string Http_Method = "GET";
            const String SEPARATOR = "&";
            const String EQUAL = "=";

            //公共参数
            _requestParams = PublicParams();
            // 加入方法特有参数
            _requestParams.Add("Action", "GetVideoList");

            var sortParams = from objDic in _requestParams orderby objDic.Key select objDic;

            //构造规范化字符串
            StringBuilder canonicalizedQueryString = new StringBuilder();
            foreach (KeyValuePair<string, string> dic in sortParams)
            {

                canonicalizedQueryString
                    .Append(SEPARATOR)
                    .Append(percentEncode(dic.Key))
                    .Append(EQUAL)
                    .Append(percentEncode(dic.Value));
            }


            //构造用于计算签名的字符串
            // 生成stringToSign字符
            var stringToSign = new StringBuilder();
            stringToSign.Append(Http_Method).Append(SEPARATOR);
            stringToSign.Append(percentEncode("/")).Append(SEPARATOR);
            stringToSign.Append(percentEncode(canonicalizedQueryString.ToString().Substring(1)));

            HMACSHA1 hmacsha1 = new HMACSHA1();
            hmacsha1.Key = Encoding.UTF8.GetBytes(_AccessSecret + SEPARATOR);
            byte[] dataBuffer = Encoding.UTF8.GetBytes(stringToSign.ToString());
            byte[] hashBytes = hmacsha1.ComputeHash(dataBuffer);
            var signature = Convert.ToBase64String(hashBytes);

            signature = percentEncode(signature);
            return signature;
        }

        const String ENCODE_TYPE = "UTF-8";

        /// <summary>
        /// URL编码要转换成大写，否则签名会不通过（真他妈坑，官方文档不说，害老子搞了半天）
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static String percentEncode(String value)
        {
            if (value == null) return null;

            StringBuilder builder = new StringBuilder();
            foreach (char c in value)
            {
                if (HttpUtility.UrlEncode(c.ToString()).Length > 1)
                {
                    builder.Append(HttpUtility.UrlEncode(c.ToString()).ToUpper())
                        .Replace("+", "%20").Replace("*", "%2A").Replace("%7E", "~");
                }
                else
                {
                    builder.Append(c);
                }
            }


            return builder.ToString();
        }

        /// <summary>
        /// 公共参数
        /// </summary>
        /// <returns></returns>
        private static Dictionary<string, string> PublicParams()
        {
            Dictionary<string, string> paramDic = new Dictionary<string, string>();

            paramDic.Add("Version", "2017-03-21");
            paramDic.Add("AccessKeyId", _AccessKey); //此处请替换成您自己的AccessKeyId
            var timeStamp = DateTime.UtcNow.ToString("yyyy-MM-dd'T'HH:mm:ss'Z'");
            paramDic.Add("Timestamp", timeStamp);//此处将时间戳固定只是测试需要，这样此示例中生成的签名值就不会变，方便您对比验证，可变时间戳的生成需要用下边这句替换
            //paramDic.Add("Timestamp", formatIso8601Date(new Date()));
            paramDic.Add("SignatureMethod", "HMAC-SHA1"); //签名方式，目前支持HMAC-SHA1
            paramDic.Add("SignatureVersion", "1.0"); //签名算法版本，目前版本是1.0。
            paramDic.Add("SignatureNonce", Guid.NewGuid().ToString());
            paramDic.Add("Format", "JSON");

            return paramDic;
        }

        public static string ToUrl(string baseUrl)
        {

            string requestUrl = baseUrl;

            foreach (var key in _requestParams.Keys)
            {
                if (requestUrl.IndexOf("?") > -1)
                {

                    requestUrl += "&" + key + "=" + _requestParams[key];
                }
                else
                {
                    requestUrl += "?" + key + "=" + _requestParams[key];
                }
            }

            return requestUrl;
        }
    }
}
