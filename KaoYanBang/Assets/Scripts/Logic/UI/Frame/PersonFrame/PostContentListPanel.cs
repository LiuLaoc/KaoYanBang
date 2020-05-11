using liulaoc.UI.Base;
using POJO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostContentListPanel : UIPanel
{
    #region view
    private Transform group;
    #endregion
    #region Model
    private List<Invitation> myInvitations;
    private int index = 0;
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
        ClearView();
        //获取我发布的Invitation
        GetMyInvitationMsg msg = new GetMyInvitationMsg(NetDataManager.Instance.user.user_id,index);
        MsgManager.Instance.NetMsgCenter.NetGetMyInvitation(msg, (responds) =>
         {
             var list = JsonHelper.DeserializeObject<List<Invitation>>(responds.data);
             foreach(var invitation in list)
             {
                 var go = Instantiate(UIResourceMgr.Instance.Get("MyPostPrefab"), group);
                 var prefab = go.GetComponent<MyPostPrefab>();
                 prefab.Init(invitation);
             }
         });
    }
    private void OnGetMyInvitation()
    {

    }
    private void ClearView()
    {
        int count = group.childCount;
        for(int i=0;i<count;i++)
        {
            Destroy(group.GetChild(i).gameObject);
        }
    }
}
