using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Networking;
namespace liulaoc.Net.Http
{
    public class HttpCenter : TMonoSingleton<HttpCenter>, IInitializable
    {
        [SerializeField] public static string path = "http://118.178.184.69:4396/";
        public string token = "";
        public Queue<HttpRequest> RequestQueue;
        public Dictionary<string, bool> isAsyncing = new Dictionary<string, bool>();//防止重复提交数据
        public void Init()
        {
            RequestQueue = new Queue<HttpRequest>();
        }
        public void Send(HttpRequest request)
        {
            if (request.IsAsync)//如果该请求是需要阻塞接下来的网络请求
            {
                if (!isAsyncing.ContainsKey(request.MsgName))
                {
                    isAsyncing.Add(request.MsgName, false);
                }
                if (isAsyncing[request.MsgName])
                {
                    return;
                }
                else
                {
                    isAsyncing[request.MsgName] = true;
                    RequestQueue.Enqueue(request);
                }
            }
            else//不阻塞，直接发送
            {
                RequestQueue.Enqueue(request);
            }
        }
        /// <summary>
        /// 优化:直接通过Update控制，可做帧控制优化
        /// </summary>
        private void Update()
        {
            DealQueue();
        }
        private void DealQueue()
        {
            if (RequestQueue == null || RequestQueue?.Count == 0)
                return;
            HttpRequest request = RequestQueue.Peek();
            if (request == null)
                return;
            switch (request.Result)
            {
                case HttpResult.Wait:
                    request.Result = HttpResult.UnReceive;
                    DealRequest(request);
                    break;
                case HttpResult.Succ:
                    request.Handler(request.Responds);
                    isAsyncing[request.MsgName] = false;
                    RequestQueue.Dequeue();
                    break;
                case HttpResult.ServerError:
                    if (request.retransmission > 0)
                    {
                        //重新发送
                        request.retransmission--;
                        request.Result = HttpResult.UnReceive;
                        DealRequest(request);
                    }
                    else
                    {
                        request.retransmission--;
                        request.Handler(request.Responds);
                        isAsyncing[request.MsgName] = false;
                        RequestQueue.Dequeue();
                    }
                    break;
                default:
                    break;
            }
        }
        private void DealRequest(HttpRequest request)
        {
            switch (request.HttpMethod)
            {
                case Method.Get:
                    StartCoroutine(StartGet(request));
                    break;
                case Method.Post:
                    StartCoroutine(StartPost(request));
                    break;
                case Method.Put:
                    StartCoroutine(StartPut(request));
                    break;
                case Method.Delete:
                    StartCoroutine(StartDelete(request));
                    break;
                default:
                    break;
            }
        }
        private IEnumerator StartGet(HttpRequest request)
        {
            var url = request.Url + "?";
            //反射用来填充Url
            Type type = Type.GetType(request.MsgName);
            var Msg = Convert.ChangeType(request.Msg, type);
            PropertyInfo[] properties = Msg.GetType().GetProperties();
            for (int i = 0; i < properties.Length; i++)
            {
                url += $"{properties[i].Name}={properties[i].GetValue(Msg)}";
                if (i != properties.Length - 1)
                    url += "&";
            }
            request.Url = url;
            using (UnityWebRequest www = UnityWebRequest.Get(request.Url))
            {
                www.certificateHandler = new AcceptAllCertificatesSignedWithASpecificKeyPublicKey();
                www.downloadHandler = new DownloadHandlerBuffer();
                www.SetRequestHeader("Content-Type", "application/json");
                www.SetRequestHeader("token", token);
                yield return www.SendWebRequest();
                DealResult(www, request);
            }
        }
        private IEnumerator StartPost(HttpRequest request)
        {
            Type type = Type.GetType(request.MsgName);
            PropertyInfo[] properties = type.GetProperties();
            var Msg = Convert.ChangeType(request.Msg, type);
            string json = JsonHelper.SerializeObject(Msg);
            using (UnityWebRequest www = UnityWebRequest.Put(request.Url, json))
            {
                www.method = UnityWebRequest.kHttpVerbPOST;
                www.certificateHandler = new AcceptAllCertificatesSignedWithASpecificKeyPublicKey();
                www.downloadHandler = new DownloadHandlerBuffer();
                www.SetRequestHeader("Content-Type", "application/json");
                if(token != null && token != "")
                {
                    www.SetRequestHeader("token", token);
                }
                yield return www.SendWebRequest();
                Debug.Log(request.Url);
                DealResult(www, request);
                Debug.Log(www.responseCode + " " + request.Responds.data);
            }
        }
        private IEnumerator StartPut(HttpRequest request)
        {
            Type type = Type.GetType(request.MsgName);
            PropertyInfo[] properties = type.GetProperties();
            var Msg = Convert.ChangeType(request.Msg, type);
            string json = JsonHelper.SerializeObject(Msg);
            using (UnityWebRequest www = UnityWebRequest.Put(request.Url, json))
            {
                www.certificateHandler = new AcceptAllCertificatesSignedWithASpecificKeyPublicKey();
                www.downloadHandler = new DownloadHandlerBuffer();
                www.SetRequestHeader("Content-Type", "application/json");
                www.SetRequestHeader("token", token);
                yield return www.SendWebRequest();
                DealResult(www, request);
            }
        }
        private IEnumerator StartDelete(HttpRequest request)
        {
            var url = request.Url + "?";
            //反射用来填充Url
            Type type = Type.GetType(request.MsgName);
            var Msg = Convert.ChangeType(request.Msg, type);
            PropertyInfo[] properties = Msg.GetType().GetProperties();
            for (int i = 0; i < properties.Length; i++)
            {
                url += $"{properties[i].Name}={properties[i].GetValue(Msg)}";
                if (i != properties.Length - 1)
                    url += "&";
            }
            request.Url = url;
            using (UnityWebRequest www = UnityWebRequest.Delete(request.Url))
            {
                www.certificateHandler = new AcceptAllCertificatesSignedWithASpecificKeyPublicKey();
                www.downloadHandler = new DownloadHandlerBuffer();
                www.SetRequestHeader("Content-Type", "application/json");
                www.SetRequestHeader("token", token);
                yield return www.SendWebRequest();
                DealResult(www, request);
            }
        }

        private void DealResult(UnityWebRequest www,HttpRequest request)
        {
            if (www.responseCode == 500)
            {
                request.Result = HttpResult.ServerError;
                request.Responds.Result = RespondsResult.Fail;
                request.Responds.data = www.downloadHandler.text;
            }
            else if (IsOk(www.responseCode))
            {
                request.Responds.data = www.downloadHandler.text;
                request.Result = HttpResult.Succ;
                request.Responds.Result = RespondsResult.Succ;
            }
            else
            {
                request.Result = HttpResult.ServerError;
                request.Responds.Result = RespondsResult.Fail;
                request.Responds.data = www.downloadHandler.text;
            }
            request.Responds.token = www.GetResponseHeader("token");
        }

        private bool IsOk(long code)
        {
            if (code == 200 || code == 201)
            {
                return true;
            }
            return false;
        }
    }
}