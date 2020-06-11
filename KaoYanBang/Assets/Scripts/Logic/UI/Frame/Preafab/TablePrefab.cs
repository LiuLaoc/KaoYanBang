using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TablePrefab : MonoBehaviour
{
    #region view
    private GameObject recordPrefab;
    private RectTransform group;
    #endregion
    #region model
    private List<string> titles;//表头
    private List<List<string>> datas;
    #endregion
    public void Init(List<string> titles,List<List<string>> data)
    {
        this.titles = titles;
        BindView();
        UpdateView();
    }
    protected void BindView()
    {
        group = transform.Find("TableView").Find("Viewport").Find("Content").GetComponent<RectTransform>();
        recordPrefab = transform.Find("TableTitle").gameObject;
    }
    /// <summary>
    /// 每个表格需要继承，实现加载Record的方法
    /// </summary>
    protected virtual void UpdateView()
    {
        //加载表格数据
        foreach(var list in datas)
        {
            GameObject go = Instantiate(recordPrefab,group.transform);
            var record = go.GetComponent<RecordPrefab>();
            record.Init(list);
        }
    }
}
