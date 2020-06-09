using liulaoc.Net.Http;
using POJO;
using System;
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
    //点赞缓存
    public Dictionary<int, bool> couldLike;
    //浏览量缓存
    public Dictionary<int, bool> couldScan;
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
                        HttpCenter.Instance.token = responds.token;
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
        MsgManager.Instance.NetMsgCenter.NetGetUserById += (request, callback) =>
        {
            HttpRequest httpRequest = new HttpRequest()
            {
                Msg = request,
                HttpMethod = Method.Post,
                Url = HttpCenter.path + "User/findbyid",
                Handler = (responds) =>
                {
                    if (responds.Result == RespondsResult.Succ)
                    {
                        callback(responds);
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
        AddListener(ref MsgManager.Instance.NetMsgCenter.NetChangeSchoolId, Method.Post, "User/changeschoolid");
        AddListener(ref MsgManager.Instance.NetMsgCenter.NetChangeSubjectId, Method.Post, "User/changesubjectid");
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
        #region Community
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
        MsgManager.Instance.NetMsgCenter.NetGetComment += (request, callbcak) =>
        {
            HttpRequest httpRequest = new HttpRequest()
            {
                Msg = request,
                HttpMethod = Method.Post,
                Url = HttpCenter.path + "comment/getinvitationcomment",
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
        MsgManager.Instance.NetMsgCenter.NetLike += (request, callbcak) =>
        {
            HttpRequest httpRequest = new HttpRequest()
            {
                Msg = request,
                HttpMethod = Method.Post,
                Url = HttpCenter.path + "like/addlike",
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
        MsgManager.Instance.NetMsgCenter.NetAddPost += (request, callbcak) =>
        {
            HttpRequest httpRequest = new HttpRequest()
            {
                Msg = request,
                HttpMethod = Method.Post,
                Url = HttpCenter.path + "invitation/post",
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
        MsgManager.Instance.NetMsgCenter.NetGetInvitation += (request, callbcak) =>
        {
            HttpRequest httpRequest = new HttpRequest()
            {
                Msg = request,
                HttpMethod = Method.Post,
                Url = HttpCenter.path + "invitation/getinvitation",
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
        MsgManager.Instance.NetMsgCenter.NetGetPlateInvitation += (request, callbcak) =>
        {
            HttpRequest httpRequest = new HttpRequest()
            {
                Msg = request,
                HttpMethod = Method.Post,
                Url = HttpCenter.path + "invitation/getplateinvition",
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
        MsgManager.Instance.NetMsgCenter.NetAddScan += (request, callbcak) =>
        {
            HttpRequest httpRequest = new HttpRequest()
            {
                Msg = request,
                HttpMethod = Method.Post,
                Url = HttpCenter.path + "invitation/addscan",
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
        MsgManager.Instance.NetMsgCenter.NetAddComment += (request, callbcak) =>
        {
            HttpRequest httpRequest = new HttpRequest()
            {
                Msg = request,
                HttpMethod = Method.Post,
                Url = HttpCenter.path + "comment/addcomment",
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
        MsgManager.Instance.NetMsgCenter.NetChangePlanStatus += (request, callbcak) =>
        {
            HttpRequest httpRequest = new HttpRequest()
            {
                Msg = request,
                HttpMethod = Method.Post,
                Url = HttpCenter.path + "plan/changestatus",
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
        #region Info
        MsgManager.Instance.NetMsgCenter.NetGetAllCarousels += (request, callbcak) =>
        {
            HttpRequest httpRequest = new HttpRequest()
            {
                Msg = request,
                HttpMethod = Method.Post,
                Url = HttpCenter.path + "carousel/getAllCarousels",
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
        #region School
        AddListener(ref MsgManager.Instance.NetMsgCenter.NetGetAllSchool, Method.Post, "school/getallschool");
        AddListener(ref MsgManager.Instance.NetMsgCenter.NetGetSchoolById, Method.Post, "school/getschool");
        #endregion
        #region invitation
        AddListener(ref MsgManager.Instance.NetMsgCenter.NetGetInvitationBySchool,Method.Post,"invitation/getinvitationbyschool");
        #endregion
        #region subject
        AddListener(ref MsgManager.Instance.NetMsgCenter.NetGetSubjectById,Method.Post,"subject/getsubjectbyid");
        #endregion
    }
    private void AddListener<T>(ref Action<T,Action<HttpResponds>> registerEvent,Method methodType,string url) where T:BaseMsg
    {
        registerEvent += (request, callback) =>
        {
            HttpRequest httpRequest = new HttpRequest()
            {
                Msg = request,
                HttpMethod = Method.Post,
                Url = HttpCenter.path + url,
                Handler = (responds) =>
                {
                    if (responds.Result == RespondsResult.Succ)
                    {
                        callback(responds);
                    }
                }
            };
            HttpCenter.Instance.Send(httpRequest);
        };
    }
}
