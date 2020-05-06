using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using liulaoc.Net.Http;

namespace liulaoc.Net
{
    public class NetMsgManager
    {
        public Action<RegisterMsg, Action<HttpResponds>> NetRegister;
        public Action<LoginMsg, Action<HttpResponds>> NetLogin;
        public Action<GetMyInvitationMsg, Action<HttpResponds>> NetGetMyInvitation;
    }

}