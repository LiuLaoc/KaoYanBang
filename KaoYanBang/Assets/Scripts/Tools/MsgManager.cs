using liulaoc.Net;
using liulaoc.UI.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MsgManager : TMonoSingleton<MsgManager>,IInitializable
{
    public NetMsgManager NetMsgCenter { get;private set; }
    public GlobalMsgManager GlobalMsgManager { get; private set; }
    public void Init()
    {
        NetMsgCenter = new NetMsgManager();
        GlobalMsgManager = new GlobalMsgManager();
        AddGlobalListener();
    }
    public void AddGlobalListener()
    {
        GlobalMsgManager.ShowErrorPanel += (content) =>
        {
            var go = Instantiate(UIResourceMgr.Instance.Get("TipPanel"),UIMgr.Instance.UIRoot);
            go.GetComponent<TipPanel>().Init(content);
        };
    }
}
