using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TablePrefab : MonoBehaviour
{
    #region view
    private RectTransform group;
    #endregion
    #region model
    private List<>
    #endregion
    protected void Awake()
    {
        BindView();
    }

    protected void BindView()
    {
        group = transform.Find("TableView").Find("Viewport").Find("Content").GetComponent<RectTransform>();
    }
    protected void UpdateView()
    {
        //加载表格数据
    }
    protected void BindDataBase()
    {

    }
}
