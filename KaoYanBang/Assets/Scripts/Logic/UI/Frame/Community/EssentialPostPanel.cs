using liulaoc.UI.Base;
using POJO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EssentialPostPanel : UIPanel
{
    #region
    private Transform group;
    private Button moreBtn;
    #endregion
    #region Model

    #endregion
    private void OnEnable()
    {
        UpdateView();
    }
    protected override void AddListener()
    {
        moreBtn.onClick.AddListener(() =>
        {
            UIMgr.Instance.CreateFrame("PostListFrame");
            var frame = UIMgr.Instance.GetTopFrame() as PostListFrame;
            frame.Init(PostFrameType.Essential);
        });
    }

    protected override void BindView()
    {
        group = transform.Find("PostGroup");
        moreBtn = transform.Find("MorePost").Find("Button").GetComponent<Button>();
    }
    protected void UpdateView()
    {
        ClearView();
        GetHotInvitationMsg msg = new GetHotInvitationMsg();
        MsgManager.Instance.NetMsgCenter.NetGetHotInvitation(msg, (responds) =>
        {
            List<Invitation> invitations = JsonHelper.DeserializeObject<List<Invitation>>(responds.data);
            int count = invitations.Count > 3 ? 3 : invitations.Count;
            for (int i = 0; i < count; i++)
            {
                var go = Instantiate(UIResourceMgr.Instance.Get("PostPrefab"), group);
                go.GetComponent<PostPrefab>().Init(invitations[i]);
            }
        });
    }
    private void ClearView()
    {
        int count = group.childCount;
        for (int i = 0; i < count; i++)
        {
            Destroy(group.GetChild(i).gameObject);
        }
    }
    private void OnDisable()
    {
        ClearView();
    }
}
