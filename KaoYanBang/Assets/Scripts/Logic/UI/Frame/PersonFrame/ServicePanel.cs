using liulaoc.UI.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ServicePanel : UIPanel
{
    #region View
    private Button personInfoBtn;
    private Button myPostBtn;
    private Button myCommentBtn;
    private Button aboutUsBtn;
    private Button helpBtn;

    #endregion
    #region ModelReference

    #endregion
    protected override void AddListener()
    {
        personInfoBtn.onClick.AddListener(() =>
        {
            UIMgr.Instance.CreateFrame("PersonInfoFrame");
        });
        myPostBtn.onClick.AddListener(() =>
        {
            UIMgr.Instance.CreateFrame("MyPostFrame");
        });
        myCommentBtn.onClick.AddListener(() =>
        {
            UIMgr.Instance.CreateFrame("MyCommentFrame");
        });
        aboutUsBtn.onClick.AddListener(() =>
        {
            UIMgr.Instance.CreateFrame("AboutUsFrame");
        });
        helpBtn.onClick.AddListener(() =>
        {
            UIMgr.Instance.CreateFrame("HelpFrame");
        });
    }

    protected override void BindView()
    {
        var group = transform.Find("ServiceGroup");
        personInfoBtn = group.Find("PersonInfoBtn").GetComponent<Button>();
        myPostBtn = group.Find("MyPostBtn").GetComponent<Button>();
        myCommentBtn = group.Find("MyCommentBtn").GetComponent<Button>();
        aboutUsBtn = group.Find("AboutUsBtn").GetComponent<Button>();
        helpBtn = group.Find("HelpBtn").GetComponent<Button>();
    }
}
