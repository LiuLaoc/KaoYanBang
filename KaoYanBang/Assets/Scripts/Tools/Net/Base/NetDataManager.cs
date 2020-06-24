using liulaoc.Net.Http;
using POJO;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// 网络数据缓存中心
/// </summary>
public class NetDataManager : TMonoSingleton<NetDataManager>,IInitializable
{
    #region model
    private int nowScene;
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
        AddListener(ref MsgManager.Instance.NetMsgCenter.NetRegister, Method.Post, "User/regist");
        AddListener(ref MsgManager.Instance.NetMsgCenter.NetLogin, Method.Post, "User/login");
        AddListener(ref MsgManager.Instance.NetMsgCenter.NetGetUserById, Method.Post, "User/findbyid");
        AddListener(ref MsgManager.Instance.NetMsgCenter.NetGetUser, Method.Post, "User/findbyname");
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
        AddListener(ref MsgManager.Instance.NetMsgCenter.NetGetHotInvitation, Method.Post, "invitation/gethot");
        AddListener(ref MsgManager.Instance.NetMsgCenter.NetGetComment, Method.Post, "comment/getinvitationcomment");
        AddListener(ref MsgManager.Instance.NetMsgCenter.NetLike, Method.Post, "like/addlike");
        AddListener(ref MsgManager.Instance.NetMsgCenter.NetAddPost, Method.Post, "invitation/post");
        AddListener(ref MsgManager.Instance.NetMsgCenter.NetGetInvitation, Method.Post, "invitation/getinvitation");
        AddListener(ref MsgManager.Instance.NetMsgCenter.NetGetPlateInvitation, Method.Post, "invitation/getplateinvition");
        AddListener(ref MsgManager.Instance.NetMsgCenter.NetAddScan, Method.Post, "invitation/addscan");
        AddListener(ref MsgManager.Instance.NetMsgCenter.NetAddComment, Method.Post, "comment/addcomment");
        AddListener(ref MsgManager.Instance.NetMsgCenter.NetJudgeLike,Method.Post,"like/judgelike");
        AddListener(ref MsgManager.Instance.NetMsgCenter.NetGetLikeCount,Method.Post,"like/countlike");
        #endregion
        #region 计划模块
        AddListener(ref MsgManager.Instance.NetMsgCenter.NetGetPlan,Method.Post, "plan/getPlan");
        AddListener(ref MsgManager.Instance.NetMsgCenter.NetGetAllSbj, Method.Post, "subject/getallsubject");
        AddListener(ref MsgManager.Instance.NetMsgCenter.NetAddPlan, Method.Post, "plan/addplan");
        AddListener(ref MsgManager.Instance.NetMsgCenter.NetChangePlanStatus, Method.Post, "plan/changestatus");
        #endregion
        #region Info
        AddListener(ref MsgManager.Instance.NetMsgCenter.NetGetAllCarousels, Method.Post, "carousel/getAllCarousels");
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
        #region schoolScore
        AddListener(ref MsgManager.Instance.NetMsgCenter.NetGetScoreBySchoolId,Method.Post, "score/selectbyid");
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
                        try
                        {
                            callback(responds);
                        }
                        catch(Exception ex)
                        {
                            
                            Debug.Log("窗口已销毁");
                            if(nowScene == 0)
                            {
                                SceneManager.LoadScene(1);
                            }
                            else
                            {
                                SceneManager.LoadScene(0);
                            }
                        }
                    }
                }
            };
            HttpCenter.Instance.Send(httpRequest);
        };
    }
}
