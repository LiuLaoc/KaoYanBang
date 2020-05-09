using liulaoc.Net.Http;
using liulaoc.UI.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NormalLoginPanel : UIPanel
{
    #region view
    private InputField accountField;
    private InputField pwdField;
    private Button confirmLoginBtn;
    private Button registerBtn;
    private Button findbackPwdBtn;
    private Button phoneLoginBtn;
    #endregion
    #region model

    #endregion
    protected override void Awake()
    {
        base.Awake();
    }
    protected override void BindView()
    {
        accountField = transform.Find("AccountField").GetComponent<InputField>();
        pwdField = transform.Find("PwdtField").GetComponent<InputField>();
        confirmLoginBtn = transform.Find("ConfirmLoginBtn").GetComponent<Button>();
        registerBtn = transform.Find("RegisterBtn").GetComponent<Button>();
        findbackPwdBtn = transform.Find("FindbackPwdBtn").GetComponent<Button>();
        phoneLoginBtn = transform.Find("PhoneLoginBtn").GetComponent<Button>();
    }
    protected override void AddListener()
    {
        registerBtn.onClick.AddListener(() => 
        {
            UIMgr.Instance.CreateFrame("RegisterFrame");
        });//注册按钮监听
        confirmLoginBtn.onClick.AddListener(() =>
        {
            LoginMsg msg = new LoginMsg(accountField.text,pwdField.text);
            MsgManager.Instance.NetMsgCenter.NetLogin(msg, (responds) =>
             {
                 if(responds.Result == RespondsResult.Succ)
                 {
                     UIMgr.Instance.CreateFrame("PersonalFrame");
                 }
             });
        });//登录按钮监听
    }
}
