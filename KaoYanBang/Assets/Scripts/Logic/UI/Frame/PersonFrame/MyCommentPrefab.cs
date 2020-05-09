using liulaoc.UI.Base;
using POJO;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyCommentPrefab : MonoBehaviour
{
    #region view
    private Text titleTxt;
    private Text dateTxt;
    private Text postContentTxt;
    private Text commentContentTxt;
    private Text commentNumTxt;
    private Button enterBtn;
    #endregion
    #region model
    #endregion
    public void Init(Comment comment)
    {

    }
    private void Awake()
    {
        BindView();
        AddListener();
    }

    private void BindView()
    {
        titleTxt = transform.Find("TitleTxt").GetComponent<Text>();
        dateTxt = transform.Find("DateTxt").GetComponent<Text>();
        postContentTxt = transform.Find("PostContentTxt").GetComponent<Text>();
        commentContentTxt = transform.Find("CommentContentTxt").GetComponent<Text>();
        commentNumTxt = transform.Find("CommentNumTxt").GetComponent<Text>();
        enterBtn = transform.GetComponent<Button>();
    }

    private void AddListener()
    {
        enterBtn.onClick.AddListener(() =>
        {
            UIMgr.Instance.CreateFrame("PostFrame");
            var frame = UIMgr.Instance.GetTopFrame() as PostFrame;
        });
    }
}
