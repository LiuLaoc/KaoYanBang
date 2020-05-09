using liulaoc.Net.Http;
using POJO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 网络数据缓存中心
/// </summary>
public class NetDataManager : TMonoSingleton<NetDataManager>,IInitializable
{
    #region model
    public User user;
    public Dictionary<int, Invitation> myInvitation;
    #endregion
    public void Init()
    {
        AddListener();
    }
    public void InitMyInvitation()
    {

    }
    public void AddListener()
    {
        #region 账号模块
        MsgManager.Instance.NetMsgCenter.NetRegister += (request,callback) => 
        {
            HttpRequest httpRequest = new HttpRequest()
            {
                Msg = request,
                HttpMethod = Method.Post,
                Url = HttpCenter.path + "User/regist",
                Handler = (responds) =>
                {
                    if(responds.Result == RespondsResult.Succ)
                    {
                        user = new POJO.User()
                        {
                            username = request.username,
                            password = request.password,
                        };
                        callback(responds);
                    }
                }
            };
            HttpCenter.Instance.Send(httpRequest);
        };//注册
        MsgManager.Instance.NetMsgCenter.NetLogin += (request, callback) =>
        {
            HttpRequest httpRequest = new HttpRequest()
            {
                Msg = request,
                HttpMethod = Method.Post,
                Url = HttpCenter.path + "User/login",
                Handler = (responds)=>
                {
                    if (responds.Result == RespondsResult.Succ)
                    {
                        callback(responds);
                    }
                }
            };
            HttpCenter.Instance.Send(httpRequest);
        };//登录
        MsgManager.Instance.NetMsgCenter.NetGetMyInvitation += (request, callbcak) =>
        {
            HttpRequest httpRequest = new HttpRequest()
            {
                Msg = request,
                HttpMethod = Method.Get,
                Url = HttpCenter.path + "User/login",
                Handler = (responds) =>
                {
                    if (responds.Result == RespondsResult.Succ)
                    {
                        callbcak(responds);
                    }
                }
            };
            HttpCenter.Instance.Send(httpRequest);
        };
        #endregion
    }
}
