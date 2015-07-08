using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Net.Http;
using System.IO;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;

namespace HCLUtility
{
    /// <summary>
    /// json 帮助类
    /// </summary>
    public class JsonHelper
    {
        /// <summary>
        /// 序列化发送JSON数据
        /// </summary>
        /// <param name="jms"></param>
        /// <returns></returns>
        public static string SendJson(MyJsonResultMessageEntity jms)
        {
            JavaScriptSerializer jsonSer = new JavaScriptSerializer();
            StringBuilder sb = new StringBuilder();
            jsonSer.Serialize(jms, sb);
            return sb.ToString();
        }
        /// <summary>
        /// 反序列化json数据
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static MyJsonResultMessageEntity JsonToEntity(string json)
        {
            JavaScriptSerializer jsonSer = new JavaScriptSerializer();
            MyJsonResultMessageEntity jms=jsonSer.Deserialize<MyJsonResultMessageEntity>(json);
            return jms;
        }
        /// <summary>
        /// 类型转JSON字符串
        /// </summary>
        /// <param name="obj">对象实体</param>
        /// <returns></returns>
        public static string JsonSerialize(Object obj)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string rel = serializer.Serialize(obj);
            return rel;
        }
      
        /// <summary>
        /// 响应 json数据
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static HttpResponseMessage toJson(Object obj)
        {
            String str;
            if (obj is String || obj is Char)
            {
                str = obj.ToString();
            }
            else
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                str = serializer.Serialize(obj);
            }
            HttpResponseMessage result = new HttpResponseMessage { Content = new StringContent(str, Encoding.GetEncoding("UTF-8"), "application/json") };
            return result;
        }
        /// <summary>
        /// 字符串转JSON
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="jsonString">字符串</param>
        /// <returns></returns>
        public static T JsonDeserialize<T>(string jsonString)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            T obj = (T)ser.ReadObject(ms);
            return obj;
        }
        /// <summary>
        /// Newtonsoft 序列
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string JsonNetConvert(Object obj)
        {
            string json = JsonConvert.SerializeObject(obj);
            return json;
        }
    }
}
