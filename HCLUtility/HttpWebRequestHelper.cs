using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HCLUtility
{
    /// <summary>
    /// WebHttpRequest GET POST请求类
    /// </summary>
    public class HttpWebRequestHelper
    {
       /// <summary>
       /// webhttprequest请求
       /// </summary>
       /// <param name="url">请求地址</param>
       /// <param name="param">请求参数</param>
       /// <param name="encode">请求编码格式utf-8/gb2312</param>
       /// <param name="method">请求方式.GET/POST</param>
       /// <returns></returns>
        private static string WebHttpUrl(string url, string param,string encode,string method)
        {
            string info = "";
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);
            byte[] bs = null;
            Encoding encoding = null;
            if (encode.ToLower() == "gb2312")
            {
                encoding= Encoding.GetEncoding("GB2312");
                bs = Encoding.GetEncoding("GB2312").GetBytes(param);
            }
            if (encode.ToLower() == "utf-8")
            {
                encoding = Encoding.UTF8;
                bs = Encoding.UTF8.GetBytes(param);
            }

            try
            {
                string responseData = String.Empty;
                req.Method = method;
                req.ContentType = "application/x-www-form-urlencoded";
                req.ContentLength = bs.Length;
                using (Stream reqStream = req.GetRequestStream())
                {
                    reqStream.Write(bs, 0, bs.Length);
                    reqStream.Close();
                }
                using (HttpWebResponse response = (HttpWebResponse)req.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream(), encoding))
                    {
                        responseData = reader.ReadToEnd().ToString();
                    }
                    info = responseData;
                }
            }
            catch (Exception ex)
            {
                info = "";
                LogHelper.SaveFileLog(ex, "/Log/WebHttpError/");
            }
            return info;
        }

        //随机生成验证码
        private static char[] constant ={
            '0','1','2','3','4','5','6','7','8','9'};
        /// <summary>
        /// 随机生成数字验证码
        /// </summary>
        /// <param name="Length">验证码长度</param>
        /// <returns></returns>
        public static string GenerateRandom(int Length)
        {
            System.Text.StringBuilder newRandom = new System.Text.StringBuilder(10);
            Random rd = new Random();
            for (int i = 0; i < Length; i++)
            {
                newRandom.Append(constant[rd.Next(10)]);
            }
            return newRandom.ToString();
        }
        /// <summary>
        /// 生成随机数(大写字母+数字)
        /// </summary>
        /// <param name="length">长度</param>
        /// <returns></returns>
        public static string RandomChars(int length)
        {
            var now = DateTime.Now.ToString("yyyyMMdd");
            string lowerChars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string tmpStr = "";
            int iRandNum;
            Random rnd = new Random();
            for (int i = 0; i < length; i++)
            {
                iRandNum = rnd.Next(lowerChars.Length);
                tmpStr += lowerChars[iRandNum];
            }
            return tmpStr;
        }
    }
}
