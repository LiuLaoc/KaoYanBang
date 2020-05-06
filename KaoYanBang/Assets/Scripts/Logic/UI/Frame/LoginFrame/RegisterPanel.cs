using liulaoc.Net.Http;
using liulaoc.UI.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegisterPanel : UIPanel
{
    private InputField accountField;
    private InputField pwdField;
    private Button confirmRegisterBtn;
    protected override void AddListener()
    {
        confirmRegisterBtn.onClick.AddListener(OnRegister);
    }

    protected override void BindView()
    {
        accountField = transform.Find("AccountField").GetComponent<InputField>();
        pwdField = transform.Find("PwdField").GetComponent<InputField>();
        confirmRegisterBtn = transform.Find("ConfirmRegisterBtn").GetComponent<Button>();
    }
    #region
    private void OnRegister()
    {
        RegisterMsg msg = new RegisterMsg(accountField.text,pwdField.text);
        MsgManager.Instance.NetMsgCenter.NetRegister(msg, responds =>
         {
             if(responds.Result == RespondsResult.Succ)
             {
                 UIMgr.Instance.RemoveFrame();
             }
         });
    }
    #endregion
}
