using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace liulaoc.Net.Http
{
    public enum Method
    {
        Get,
        Post,
        Put,
        Delete
    }

    public enum HttpResult
    {
        Wait,//等待发送
        UnReceive,//等待接收
        Succ,//成功
        Unauth,//为授权
        ServerError,//服务器出现错误，需要重传
    }


    /// <summary>
    /// HttpMethod
    /// Msg
    /// Url
    /// </summary>
    public class HttpRequest
    {
        private string url;
        /// <summary>
        /// 协议名称
        /// </summary>
        public string MsgName { get; private set; }
        /// <summary>
        /// 协议类型：包括Get、Put、Post、Delete
        /// </summary>
        public Method HttpMethod { get; set; }
        /// <summary>
        /// 是否阻塞，默认为false
        /// </summary>
        public bool IsAsync { get; set; } = false;
        private BaseMsg _msg;
        public BaseMsg Msg
        {
            get
            {
                return _msg;
            }
            set
            {
                MsgName = value.GetType().ToString();
                _msg = value;
            }
        }
        //失败重发次数
        public int retransmission = 1;
        /// <summary>
        /// 网络Url，只传/api之后路径，不传XXXXXXX/api/……
        /// sample:Level/Unlock
        /// </summary>
        public string Url { get { return url; } set { url = HttpCenter.path + value; } }
        public HttpResult Result { get; set; }
        public Action<HttpResponds> Handler { get; set; }
        public HttpResponds Responds { get; set; } = new HttpResponds();
    }
}