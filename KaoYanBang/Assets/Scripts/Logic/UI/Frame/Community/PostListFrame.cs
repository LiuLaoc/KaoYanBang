using liulaoc.UI.Base;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PostFrameType
{
    Hot,
    Essential,
    Module,
}


public class PostListFrame : UIFrame
{
    #region view
    private Transform group;
    #endregion
    #region model
    private List<PostPrefab> allPost = new List<PostPrefab>();
    private PostFrameType type;
    private POJO.Subject module;
    private bool isinit = false;
    private int index = 1;
    #endregion
    private void Awake()
    {
        BindView();
        AddListener();
    }

    private void AddListener()
    {
        
    }

    private void BindView()
    {
        group = transform.Find("Scroll View").Find("Viewport").Find("Content");
    }

    private void OnEnable()
    {
        if(isinit)
        {
            UpdateView();
        }
    }
    private void OnDisable()
    {
        int count = group.childCount;
        for(int i=0;i<count;i++)
        {
            Destroy(group.GetChild(0).gameObject);
        }
        allPost.Clear();
        index = 1;
    }
    private void UpdateView()
    {
        switch (type)
        {
            case PostFrameType.Hot:
                GetInvitationMsg msg1 = new GetInvitationMsg();
                MsgManager.Instance.NetMsgCenter.NetGetInvitation(msg1, (respond) =>
                {
                    var list = JsonHelper.DeserializeObject<List<POJO.Invitation>>(respond.data);
                    foreach (var post in list)
                    {
                        var go = Instantiate(UIResourceMgr.Instance.Get("PostPrefab"), group);
                        var prefab = go.GetComponent<PostPrefab>();
                        prefab.Init(post);
                        allPost.Add(prefab);
                        group.GetComponent<RectTransform>().sizeDelta = new Vector2(1063, 433.59f * allPost.Count);
                    }
                });
                break;
            case PostFrameType.Essential:
                GetInvitationMsg msg2 = new GetInvitationMsg();
                MsgManager.Instance.NetMsgCenter.NetGetInvitation(msg2, (respond) =>
                {
                    var list = JsonHelper.DeserializeObject<List<POJO.Invitation>>(respond.data);
                    foreach (var post in list)
                    {
                        var go = Instantiate(UIResourceMgr.Instance.Get("PostPrefab"), group);
                        var prefab = go.GetComponent<PostPrefab>();
                        prefab.Init(post);
                        allPost.Add(prefab);
                        group.GetComponent<RectTransform>().sizeDelta = new Vector2(1063, 433.59f * allPost.Count);
                    }
                });
                break;
            case PostFrameType.Module:
                GetPlateInvitationMsg msg3 = new GetPlateInvitationMsg(module.subject_id);
                MsgManager.Instance.NetMsgCenter.NetGetPlateInvitation(msg3, (respond) =>
                {
                    var list = JsonHelper.DeserializeObject<List<POJO.Invitation>>(respond.data);
                    foreach (var post in list)
                    {
                        var go = Instantiate(UIResourceMgr.Instance.Get("PostPrefab"), group);
                        var prefab = go.GetComponent<PostPrefab>();
                        prefab.Init(post);
                        allPost.Add(prefab);
                        group.GetComponent<RectTransform>().sizeDelta = new Vector2(1063, 433.59f * allPost.Count);
                    }
                });
                break;
        }
    }
    public void Init(PostFrameType type,POJO.Subject sbj = null)
    {
        this.type = type;
        module = sbj;
        isinit = true;
        UpdateView();
    }

}
