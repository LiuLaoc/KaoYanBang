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

    }
    #endregion
}
