using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TablePrefab : MonoBehaviour
{
    #region view
    [SerializeField] private GameObject recordPrefab;
    [SerializeField] private RectTransform group;
    [SerializeField]private RecordPrefab titlePrefab;
    #endregion
    #region model
    private List<string> titles;//表头
    private List<List<string>> datas;
    #endregion
    public void Init(List<string> titles,List<List<string>> data)
    {
        this.titles = titles;
        this.datas = data;
        UpdateView();
    }
    /// <summary>
    /// 每个表格需要继承，实现加载Record的方法
    /// </summary>
    protected virtual void UpdateView()
    {
        Debug.Log(titles.Count);
        //加载Title
        titlePrefab.Init(titles);
        Debug.Log(datas.Count);
        //加载表格数据
        foreach(var list in datas)
        {
            GameObject go = Instantiate(recordPrefab,group.transform);
            var record = go.GetComponent<RecordPrefab>();
            record.Init(list);
        }
        var x = 1716.1f;
        var titleY = 133.1f;
        var recordY = datas.Count * 133.1f;
        group.sizeDelta = new Vector2(x,titleY + recordY);
    }
}
