using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using liulaoc.Net.Http;

namespace liulaoc.Net
{
    public class NetMsgManager
    {
        #region 个人
        public Action<RegisterMsg, Action<HttpResponds>> NetRegister;
        public Action<LoginMsg, Action<HttpResponds>> NetLogin;
        public Action<GetUserMsg, Action<HttpResponds>> NetGetUser;
        public Action<GetMyInvitationMsg, Action<HttpResponds>> NetGetMyInvitation;
        public Action<GetMyCommentMsg, Action<HttpResponds>> NetGetMyComment;
        public Action<GetUserByIdMsg, Action<HttpResponds>> NetGetUserById;
        #endregion
        #region Community
        public Action<GetHotInvitationMsg, Action<HttpResponds>> NetGetHotInvitation;
        public Action<GetCommentMsg, Action<HttpResponds>> NetGetComment;
        public Action<LikeMsg, Action<HttpResponds>> NetLike;
        public Action<AddPostMsg, Action<HttpResponds>> NetAddPost;

        #endregion
        #region Plan
        public Action<GetPlanMsg, Action<HttpResponds>> NetGetPlan;
        public Action<GetAllSubjectMsg, Action<HttpResponds>> NetGetAllSbj;
        public Action<AddPlanMsg, Action<HttpResponds>> NetAddPlan;
        #endregion
    }

}