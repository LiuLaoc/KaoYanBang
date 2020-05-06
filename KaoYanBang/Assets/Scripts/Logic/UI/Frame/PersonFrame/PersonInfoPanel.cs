﻿using liulaoc.UI.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PersonInfoPanel : UIPanel
{
    #region view
    private Image headImg;
    private Text nameTxt;
    private Text phoneTxt;
    private Text pwdTxt;
    private Button pwdBtn;
    private Button phoneBtn;
    private Button nameBtn;
    #endregion
    #region model
    private string Name => NetDataManager.Instance.user.username;
    private string Phone => NetDataManager.Instance.user.phone;
    private string Password => NetDataManager.Instance.user.password;
    #endregion
    protected override void AddListener()
    {
        nameBtn.onClick.AddListener(() =>
        {

        });
        phoneBtn.onClick.AddListener(() =>
        {

        });
        pwdBtn.onClick.AddListener(() =>
        {

        });
    }

    protected override void BindView()
    {
        headImg = transform.Find("Head").Find("HeadImg").GetComponent<Image>();
        nameTxt = transform.Find("UserName").Find("NameTxt").GetComponent<Text>();
        phoneTxt = transform.Find("Phone").Find("PhoneTxt").GetComponent<Text>();
        pwdTxt = transform.Find("PassWord").Find("PwdTxt").GetComponent<Text>();
        nameBtn = transform.Find("UserName").GetComponent<Button>();
        phoneBtn = transform.Find("Phone").GetComponent<Button>();
        pwdBtn = transform.Find("Password").GetComponent<Button>();
    }
    protected void UpdateView()
    {
        nameTxt.text = Name;
        phoneTxt.text = Phone;
        pwdTxt.text = Password;
    }
}