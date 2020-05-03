using liulaoc.UI.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlatformLoginPanel : UIPanel
{
    #region view
    private Button QQLoginBtn;
    private Button weiXinLoginBtn;
    private Button xinLangLoginBtn;
    #endregion
    #region Model

    #endregion
    protected override void Awake()
    {
        base.Awake();
    }
    protected override void AddListener()
    {
        
    }
    protected override void BindView()
    {
        var group = transform.Find("PlatformLoginGroup");
        QQLoginBtn = group.Find("QQ").GetComponent<Button>();
        weiXinLoginBtn = group.Find("WeiXin").GetComponent<Button>();
        xinLangLoginBtn = group.Find("XinLang").GetComponent<Button>();
    }

}
