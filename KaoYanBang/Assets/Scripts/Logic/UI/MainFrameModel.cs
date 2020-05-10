using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainFrameModel : TMonoSingleton<MainFrameModel>,IInitializable
{
    public int ToggleIndex { get; set; }
    public Dictionary<string, List<POJO.Plan>> allPlan { get;private set; }
    public void Init()
    {
        ToggleIndex = 4;
        allPlan = new Dictionary<string, List<POJO.Plan>>();
    }
    public void AddPlan(POJO.Plan plan)
    {
        DateTime dt = Convert.ToDateTime(plan.date);
        var dtStr = dt.ToString("yyyy-MM-dd");
        if (allPlan.ContainsKey(dtStr))
        {
            allPlan[dtStr].Add(plan);
        }
        else
        {
            allPlan.Add(dtStr, new List<POJO.Plan>());
            allPlan[dtStr].Add(plan);
        }
    }
    public void InitPlan(string dateTime)
    {
        allPlan.Add(dateTime, new List<POJO.Plan>());
    }
}
