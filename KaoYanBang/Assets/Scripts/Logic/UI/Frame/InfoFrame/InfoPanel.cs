using liulaoc.UI.Base;
using POJO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanel : UIPanel
{
    #region view
    [SerializeField]private Toggle[] toogles;
    
    [SerializeField]private GameObject PostPrefab;
    [SerializeField]private GameObject InfoPrefab;
    private ToggleGroup selectToogle;
    private Transform group;
    private RectTransform groupTrans;
    [SerializeField]private GridLayoutGroup groupLayout;
    #endregion
    #region model
    private List<GameObject> AllPostList = new List<GameObject>();
    private List<GameObject> AllInfoList = new List<GameObject>();
    private int selectIndex = 0;
    #endregion
    protected override void AddListener()
    {
        toogles[0].onValueChanged.AddListener((value) =>
        {
            if(value)
            {
                selectIndex = 0;
                UpdateView();
            }
        });
        toogles[1].onValueChanged.AddListener((value) =>
        {
            if (value)
            {
                selectIndex = 1;
                UpdateView();
            }
        });
    }

    protected override void BindView()
    {
        group = transform.Find("Scroll View").Find("Viewport").Find("SchoolInvitations");
        selectToogle = transform.Find("SelectToogle").GetComponent<ToggleGroup>();
        groupTrans = group.GetComponent<RectTransform>();
    }
    protected void UpdateView()
    {
        //更新可滑动区间大小
        float sizeY = 400f;
        float sizeX = 1063f;
        int lines = 0;
        bool isPost = selectIndex == 0;
        foreach (var item in AllPostList)
        {
            item.SetActive(isPost);
        }
        foreach (var item in AllInfoList)
        {
            item.SetActive(!isPost);
        }
        switch (selectIndex)
        {
            case 0:
                sizeX = PostPrefab.GetComponent<RectTransform>().rect.width;    
                sizeY = PostPrefab.GetComponent<RectTransform>().rect.height;
                lines = AllPostList.Count;
                break;
            case 1:
                sizeX = InfoPrefab.GetComponent<RectTransform>().rect.width;
                sizeY = InfoPrefab.GetComponent<RectTransform>().rect.height;
                lines = AllInfoList.Count;
                break;
        }
        groupLayout.cellSize = new Vector2(sizeX,sizeY);
        groupTrans.sizeDelta = new Vector2(sizeX,sizeY*lines);
        return;
    }
    private void OnDisable()
    {
        foreach(var item in AllPostList)
        {
            Destroy(item);
        }
        AllPostList.Clear();
        foreach(var item in AllInfoList)
        {
            Destroy(item);
        }
        AllInfoList.Clear();
    }
    private void OnEnable()
    {
        //加载所有Prefab
        GetInvitationBySchoolMsg msg = new GetInvitationBySchoolMsg(NetDataManager.Instance.user.school_id);
        MsgManager.Instance.NetMsgCenter.NetGetInvitationBySchool(msg, (respond) =>
        {
            var posts = JsonHelper.DeserializeObject<List<Invitation>>(respond.data);
            foreach (var post in posts)
            {
                if (post.invitation_type == (int)InvitationType.Invitation)
                {
                    GameObject go = Instantiate(PostPrefab, group);
                    go.GetComponent<PostPrefab>().Init(post);
                    AllPostList.Add(go);
                }
                if (post.invitation_type == (int)InvitationType.Regulation)
                {
                    GameObject go = Instantiate(InfoPrefab, group);
                    go.GetComponent<RegulationPrefab>().Init(post);
                    AllInfoList.Add(go);
                }
            }
            UpdateView();
        });
    }
}
