using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HCLUtility
{
    /// <summary>
    /// Json消息实体
    /// </summary>
    public class MyJsonResultMessageEntity
    {
        public MyJsonResultMessageEntity()
        {
            Message = "服务器繁忙！";
        }
        private bool _IsSuccess=false;
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccess
        {
            get { 
                    return _IsSuccess; 
            }
            set { _IsSuccess = value; }
        }
        private string _Message;
        /// <summary>
        /// 返回的消息
        /// </summary>
        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
        }
        private string _MessageCode;
        /// <summary>
        /// 消息编码
        /// </summary>
        public string MessageCode
        {
            get { return _MessageCode; }
            set { _MessageCode = value; }
        }
        private string _BackUrl;
        /// <summary>
        /// 需要跳转的url
        /// </summary>
        public string BackUrl
        {
            get { return _BackUrl; }
            set { _BackUrl = value; }
        }
        private object _DataObj;
        /// <summary>
        /// 需要返回的数据实体
        /// </summary>
        public object DataObj
        {
            get { return _DataObj; }
            set { _DataObj = value; }
        }
        
    }
}