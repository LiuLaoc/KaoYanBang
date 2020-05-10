using liulaoc.UI.Base;
using POJO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotMoudlePanel : UIPanel
{
    #region
    private Transform group;
    private Button moreBtn;
    #endregion
    protected override void AddListener()
    {
        moreBtn.onClick.AddListener(() =>
        {
            UIMgr.Instance.CreateFrame("PostListFrame");
            var frame = UIMgr.Instance.GetTopFrame() as PostListFrame;
            frame.Init(PostFrameType.Module);
        });
    }

    protected override void BindView()
    {
        group = transform.Find("Group");
        moreBtn = transform.Find("MoreMoudle").Find("Button").GetComponent<Button>();
    }
    protected void UpdateView()
    {
        GetAllSubjectMsg msg = new GetAllSubjectMsg();
        MsgManager.Instance.NetMsgCenter.NetGetAllSbj(msg, (respond) =>
        {
            var list = JsonHelper.DeserializeObject<List<POJO.Subject>>(respond.data);
            int count = list.Count > 3 ? 3 : list.Count;
            for(int i=0;i<count;i++)
            {
                var go = Instantiate(UIResourceMgr.Instance.Get("ModulePrefab"),group);
                var prefab = go.GetComponent<ModulePrefab>();
                prefab.Init(list[i]);
            }
        });
    }
    private void OnEnable()
    {
        UpdateView();
    }
    private void OnDisable()
    {
        int count = group.childCount;
        for(int i=0;i<count;i++)
        {
            Destroy(group.GetChild(i).gameObject);
        }
    }
}
