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
    private Transform group;
    private RectTransform groupTrans;
    #endregion
    #region model
    private List<GameObject> AllPostList = new List<GameObject>();
    private List<GameObject> AllInfoList = new List<GameObject>();
    #endregion
    protected override void AddListener()
    {
        toogles[0].onValueChanged.AddListener((value) =>
        {
            if(value)
            {

            }
        });
        toogles[1].onValueChanged.AddListener((value) =>
        {
            if (value)
            {

            }
        });
        toogles[2].onValueChanged.AddListener((value) =>
        {
            if (value)
            {

            }
        });
    }

    protected override void BindView()
    {
        group = transform.Find("Scroll View").Find("Viewport").Find("HotTopicGroup");
        groupTrans = group.GetComponent<RectTransform>();
        //加载所有Prefab
    }
    protected void UpdateView()
    {

    }
}
