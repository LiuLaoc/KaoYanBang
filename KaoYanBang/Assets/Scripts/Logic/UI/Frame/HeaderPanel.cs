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
        throw new System.NotImplementedException();
    }

    protected override void BindView()
    {
        backBtn = transform.Find("BackBtn").GetComponent<Button>();
    }
}
