using liulaoc.UI.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanel : UIPanel
{
    #region view
    [SerializeField]private Toggle[] toogles;
    private Transform group;
    private RectTransform groupTrans;
    #endregion
    #region model

    #endregion
    protected override void AddListener()
    {
        
    }

    protected override void BindView()
    {
        group = transform.Find("Scroll View").Find("Viewport").Find("HotTopicGroup");
        groupTrans = group.GetComponent<RectTransform>();

    }
}
