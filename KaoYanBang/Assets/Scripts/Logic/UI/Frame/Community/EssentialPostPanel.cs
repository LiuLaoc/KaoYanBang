using liulaoc.UI.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssentialPostPanel : UIPanel
{
    #region
    private Transform group;
    #endregion
    #region Model

    #endregion
    private void OnEnable()
    {
        UpdateView();
    }
    protected override void AddListener()
    {

    }

    protected override void BindView()
    {
        group = transform.Find("PostGroup");
    }
    protected void UpdateView()
    {
        GetHotInvitationMsg msg = new GetHotInvitationMsg();
        MsgManager.Instance.NetMsgCenter.NetGetHotInvitation(msg, (responds) =>
        {
            List<POJO.Invitation> invitations = JsonHelper.DeserializeObject<List<POJO.Invitation>>(responds.data);
            foreach (var invitation in invitations)
            {
                var go = Instantiate(UIResourceMgr.Instance.Get("PostPrefab"), group);
                go.GetComponent<PostPrefab>().Init(invitation);
            }
        });
    }
}
