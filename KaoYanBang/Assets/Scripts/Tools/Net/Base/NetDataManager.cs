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
                    else
                    {
                        MsgManager.Instance.GlobalMsgManager.ShowErrorPanel("注册账号失败");
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
                    else
                    {
                        MsgManager.Instance.GlobalMsgManager.ShowErrorPanel("登录失败");
                    }
                }
            };
            HttpCenter.Instance.Send(httpRequest);
        };//登录
        MsgManager.Instance.NetMsgCenter.NetGetUser += (request, callbcak) =>
        {
            HttpRequest httpRequest = new HttpRequest()
            {
                Msg = request,
                HttpMethod = Method.Post,
                Url = HttpCenter.path + "User/findbyname",
                Handler = (responds) =>
                {
                    if (responds.Result == RespondsResult.Succ)
                    {
                        user = JsonHelper.DeserializeObject<User>(responds.data);
                        callbcak(responds);
                    }
                }
            };
            HttpCenter.Instance.Send(httpRequest);
        };//通过用户名获取用户信息
        #endregion
        #region 个人界面
        MsgManager.Instance.NetMsgCenter.NetGetMyInvitation += (request, callbcak) =>
        {
            HttpRequest httpRequest = new HttpRequest()
            {
                Msg = request,
                HttpMethod = Method.Post,
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
        MsgManager.Instance.NetMsgCenter.NetGetMyComment += (request, callbcak) =>
        {
            HttpRequest httpRequest = new HttpRequest()
            {
                Msg = request,
                HttpMethod = Method.Post,
                Url = HttpCenter.path + "User/getcomment",
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
        #region Community 模块
        MsgManager.Instance.NetMsgCenter.NetGetHotInvitation += (request, callbcak) =>
        {
            HttpRequest httpRequest = new HttpRequest()
            {
                Msg = request,
                HttpMethod = Method.Post,
                Url = HttpCenter.path + "invitation/gethot",
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
        #region 计划模块
        MsgManager.Instance.NetMsgCenter.NetGetPlan += (request, callbcak) =>
        {
            HttpRequest httpRequest = new HttpRequest()
            {
                Msg = request,
                HttpMethod = Method.Post,
                Url = HttpCenter.path + "plan/getPlan",
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
        MsgManager.Instance.NetMsgCenter.NetGetAllSbj += (request, callbcak) =>
        {
            HttpRequest httpRequest = new HttpRequest()
            {
                Msg = request,
                HttpMethod = Method.Post,
                Url = HttpCenter.path + "subject/getallsubject",
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
        MsgManager.Instance.NetMsgCenter.NetAddPlan += (request, callbcak) =>
        {
            HttpRequest httpRequest = new HttpRequest()
            {
                Msg = request,
                HttpMethod = Method.Post,
                Url = HttpCenter.path + "plan/addplan",
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
