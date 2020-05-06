using liulaoc.Net;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MsgManager : TMonoSingleton<MsgManager>,IInitializable
{
    public NetMsgManager NetMsgCenter { get;private set; }
    public void Init()
    {
        NetMsgCenter = new NetMsgManager();
    }
}
