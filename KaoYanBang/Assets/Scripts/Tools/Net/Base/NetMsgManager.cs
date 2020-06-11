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
        public Action<ChangeSchoolIdMsg, Action<HttpResponds>> NetChangeSchoolId;
        public Action<ChangeSubjectIdMsg, Action<HttpResponds>> NetChangeSubjectId;
        #endregion
        #region Community
        public Action<GetHotInvitationMsg, Action<HttpResponds>> NetGetHotInvitation;
        public Action<GetCommentMsg, Action<HttpResponds>> NetGetComment;
        public Action<LikeMsg, Action<HttpResponds>> NetLike;
        public Action<AddPostMsg, Action<HttpResponds>> NetAddPost;
        public Action<GetInvitationMsg, Action<HttpResponds>> NetGetInvitation;
        public Action<GetPlateInvitationMsg, Action<HttpResponds>> NetGetPlateInvitation;
        public Action<AddScanMsg, Action<HttpResponds>> NetAddScan;
        public Action<AddCommentMsg, Action<HttpResponds>> NetAddComment;
        public Action<JudgeLikeMsg, Action<HttpResponds>> NetJudgeLike;
        public Action<GetLikeCountMsg, Action<HttpResponds>> NetGetLikeCount;
        #endregion
        #region Plan
        public Action<GetPlanMsg, Action<HttpResponds>> NetGetPlan;
        public Action<GetAllMsg, Action<HttpResponds>> NetGetAllSbj;
        public Action<AddPlanMsg, Action<HttpResponds>> NetAddPlan;
        public Action<ChangePlanStatusMsg, Action<HttpResponds>> NetChangePlanStatus;
        #endregion
        #region Info
        public Action<GetAllCarouselsMsg, Action<HttpResponds>> NetGetAllCarousels;
        #endregion
        #region 学校
        public Action<GetAllMsg, Action<HttpResponds>> NetGetAllSchool;
        public Action<GetSchoolMsg, Action<HttpResponds>> NetGetSchoolById;
        #endregion
        #region Invitation
        public Action<GetInvitationBySchoolMsg, Action<HttpResponds>> NetGetInvitationBySchool;
        #endregion
        #region Subject
        public Action<GetSubjectByIdMsg, Action<HttpResponds>> NetGetSubjectById;
        #endregion
        #region score
        public Action<GetScoreBySchoolIdMsg, Action<HttpResponds>> NetGetScoreBySchoolId;
        #endregion
    }

}