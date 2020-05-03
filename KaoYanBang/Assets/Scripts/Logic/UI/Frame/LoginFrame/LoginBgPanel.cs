using liulaoc.UI.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginBgPanel : UIPanel
{
    #region view
    private Button userProtoBtn;
    private Button privacyProtoBtn;

    #endregion
    protected override void AddListener()
    {
        userProtoBtn.onClick.AddListener(() => 
        {
            UIMgr.Instance.CreateFrame("UseProtoFrame");
        });
        privacyProtoBtn.onClick.AddListener(() =>
        {
            UIMgr.Instance.CreateFrame("PrivacyProtoFrame");
        });
    }

    protected override void BindView()
    {
        userProtoBtn = transform.Find("UserProtoBtn").GetComponent<Button>();
        privacyProtoBtn = transform.Find("PrivacyProtoBtn").GetComponent<Button>();
    }
    protected override void Awake()
    {
        base.Awake();
    }
}
