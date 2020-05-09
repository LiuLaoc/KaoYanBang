using liulaoc.UI.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FooterPanel : UIPanel
{
    #region view
    private Toggle focusBtn;
    private Toggle infoBtn;
    private Toggle communityBtn;
    private Toggle personalBtn;
    private ToggleGroup group;
    #endregion
    #region model

    #endregion
    protected override void AddListener()
    {
        focusBtn.onValueChanged.AddListener((bool isSelect) =>
        {
            MainFrameModel.Instance.ToggleIndex = 1;
            UIMgr.Instance.RemoveFrame();
            UIMgr.Instance.CreateFrame("FocusFrame");
        });
        infoBtn.onValueChanged.AddListener((bool isSelect) =>
        {
            MainFrameModel.Instance.ToggleIndex = 2;
            UIMgr.Instance.RemoveFrame();
            UIMgr.Instance.CreateFrame("InfoFrame");
        });
        communityBtn.onValueChanged.AddListener((bool isSelect) =>
        {
            MainFrameModel.Instance.ToggleIndex = 3;
            UIMgr.Instance.RemoveFrame();
            UIMgr.Instance.CreateFrame("CommunityFrame");
        });
        personalBtn.onValueChanged.AddListener((bool isSelect) =>
        {
            MainFrameModel.Instance.ToggleIndex = 4;
            UIMgr.Instance.RemoveFrame();
            UIMgr.Instance.CreateFrame("PersonalFrame");
        });
    }

    protected override void BindView()
    {
        group = transform.Find("Group").GetComponent<ToggleGroup>();
        focusBtn = group.transform.Find("FocusBtn").GetComponent<Toggle>();
        infoBtn = group.transform.Find("InfoBtn").GetComponent<Toggle>();
        communityBtn = group.transform.Find("CommunityBtn").GetComponent<Toggle>();
        personalBtn = group.transform.Find("PersonalBtn").GetComponent<Toggle>();
        switch(MainFrameModel.Instance.ToggleIndex)
        {
            case 1:
                focusBtn.Select();
                break;
            case 2:
                infoBtn.Select();
                break;
            case 3:
                communityBtn.Select();
                break;
            case 4:
                personalBtn.Select();
                break;
        }
    }
}
