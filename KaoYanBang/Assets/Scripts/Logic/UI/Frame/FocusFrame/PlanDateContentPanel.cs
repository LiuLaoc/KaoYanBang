﻿using liulaoc.UI.Base;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanDateContentPanel : UIPanel
{
    #region view
    private Transform content;
    private Button todayEditBtn;
    #endregion
    protected override void AddListener()
    {
        todayEditBtn.onClick.AddListener(() =>
        {
            DateTime dateTime = DateTime.Now;
            string dtStr = dateTime.ToString("yyyy-MM-dd");
            if(!MainFrameModel.Instance.allPlan.ContainsKey(dtStr))
            {
                MainFrameModel.Instance.InitPlan(dtStr);
            }
            UIMgr.Instance.CreateFrame("PlanFrame");
            var frame = UIMgr.Instance.GetTopFrame() as PlanFrame;
            frame.Init(dtStr);
        });
    }

    protected override void BindView()
    {
        content = transform.Find("Scroll View").Find("Viewport").Find("Content");
        todayEditBtn = transform.Find("TodayEditBtn").GetComponent<Button>();
    }
    protected void UpdateView()
    {
        GetPlanMsg msg = new GetPlanMsg();
        MsgManager.Instance.NetMsgCenter.NetGetPlan(msg, (responds) =>
         {
             var list = JsonHelper.DeserializeObject<List<POJO.Plan>>(responds.data);
             if (list == null)
             {
                 return;
             }
             foreach(var plan in list)
             {
                 MainFrameModel.Instance.AddPlan(plan);
             }
             foreach(var plan in MainFrameModel.Instance.allPlan.Keys)
             {
                 var go = Instantiate(UIResourceMgr.Instance.Get("DatePlanPrefab"), content);
                 go.GetComponent<DatePlan>().Init(plan);
             }
         });
    }
    private void OnEnable()
    {
        UpdateView();
    }
    private void OnDisable()
    {
        while(content.childCount != 0)
        {
            Destroy(content.GetChild(0));
        }
    }
}
