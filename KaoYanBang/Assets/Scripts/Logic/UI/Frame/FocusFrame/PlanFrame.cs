using liulaoc.UI.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanFrame : UIFrame
{
    #region view
    private Transform group;
    private Button addPlanBtn;
    private Button backBtn;
    #endregion
    #region model
    private string createDate;
    private bool isInit = false;
    #endregion
    private void Awake()
    {
        BindView();
        AddListeners();
    }
    public void Init(string createDate)
    {
        isInit = true;
        this.createDate = createDate;
        UpdateView();
    }
    private void BindView()
    {
        group = transform.Find("PlanGroupPanel").Find("Scroll View").Find("Viewport").Find("Content");
        addPlanBtn = transform.Find("AddPlanBtn").GetComponent<Button>();
        var planHeaderPanel = transform.Find("PlanHeaderPanel");
        backBtn = planHeaderPanel.Find("BackBtn").GetComponent<Button>();
    }
    public void AddListeners()
    {
        addPlanBtn.onClick.AddListener(() =>
        {
            UIMgr.Instance.CreateFrame("AddPlanFrame");
        });
        backBtn.onClick.AddListener(() =>
        {
            UIMgr.Instance.RemoveFrame();
        });
    }
    private void UpdateView()
    {
        var list = MainFrameModel.Instance.allPlan[createDate];
        if (list == null)
        {
            Debug.Log("没有该天计划");
            return;
        }
        foreach(var plan in list)
        {
            var go = Instantiate(UIResourceMgr.Instance.Get("Plan"),group);
            go.GetComponent<Plan>().Init(plan);
        }
    }
    private void OnDisable()
    {
        if(group.childCount != 0)
        {
            Destroy(group.GetChild(0));
        }
    }
    private void OnEnable()
    {
        if(isInit)
        {
            UpdateView();
        }
    }
}
