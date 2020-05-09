using liulaoc.UI.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommunityFrame : UIFrame
{
    #region view
    private Button todayEditBtn;
    #endregion
    private void Awake()
    {
        todayEditBtn = transform.Find("TodayEditBtn").GetComponent<Button>();
        todayEditBtn.onClick.AddListener(() =>
        {
            UIMgr.Instance.CreateFrame("WritePostFrame");
        });
    }
}
