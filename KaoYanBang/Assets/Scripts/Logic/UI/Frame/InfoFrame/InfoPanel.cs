using liulaoc.UI.Base;
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
        toogles[2].onValueChanged.AddListener((value) =>
        {
            if (value)
            {
                selectIndex = 2;
                UpdateView();
            }
        });
    }

    protected override void BindView()
    {
        group = transform.Find("Scroll View").Find("Viewport").Find("HotTopicGroup");
        selectToogle = transform.Find("SelectToogle").GetComponent<ToggleGroup>();
        groupTrans = group.GetComponent<RectTransform>();
        //加载所有Prefab
    }
    protected void UpdateView()
    {
        //更新可滑动区间大小
        float sizeY = 400f;
        float sizeX = 1063f;
        int lines = 0;
        switch(selectIndex)
        {
            case 1:
                sizeX = PostPrefab.GetComponent<RectTransform>().rect.width;    
                sizeY = PostPrefab.GetComponent<RectTransform>().rect.height;
                lines = AllPostList.Count;
                break;
            case 2:
                sizeX = InfoPrefab.GetComponent<RectTransform>().rect.width;
                sizeY = InfoPrefab.GetComponent<RectTransform>().rect.height;
                lines = AllInfoList.Count;
                break;
            case 3:
                sizeX = InfoPrefab.GetComponent<RectTransform>().rect.width;
                sizeY = InfoPrefab.GetComponent<RectTransform>().rect.height;
                lines = AllInfoList.Count;
                break;
        }
        groupTrans.sizeDelta = new Vector2(sizeX,sizeY*lines);
        return;
    }
}
