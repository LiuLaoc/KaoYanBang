using liulaoc.UI.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCommentPanel : UIPanel
{
    #region view
    private Transform group;
    #endregion
    #region Model
    private List<POJO.Invitation> myInvitations;
    private int index = 1;
    #endregion
    protected override void AddListener()
    {

    }

    protected override void BindView()
    {
        group = transform.Find("Scroll View").Find("Viewport").Find("Group");
        UpdateView();
    }
    protected void UpdateView()
    {
        //获取我发布的Invitation
        GetMyCommentMsg msg = new GetMyCommentMsg(NetDataManager.Instance.user.user_id, index);
        MsgManager.Instance.NetMsgCenter.NetGetMyComment(msg, (responds) =>
        {
            var list = JsonHelper.DeserializeObject<List<POJO.Comment>>(responds.data);
            foreach (var comment in list)
            {
                var go = Instantiate(UIResourceMgr.Instance.Get("MyCommentPrefab"), group);
                var prefab = go.GetComponent<MyCommentPrefab>();
                prefab.Init(comment);
            }
        });
        //实例化
    }
    private void OnGetMyInvitation()
    {

    }
}

