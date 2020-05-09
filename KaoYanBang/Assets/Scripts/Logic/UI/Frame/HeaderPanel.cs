using liulaoc.UI.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeaderPanel : UIPanel
{
    private Button backBtn;

    protected override void AddListener()
    {
        backBtn.onClick.AddListener(() =>
        {
            UIMgr.Instance.RemoveFrame();
        });
    }

    protected override void BindView()
    {
        backBtn = transform.Find("BackBtn").GetComponent<Button>();
    }
}
