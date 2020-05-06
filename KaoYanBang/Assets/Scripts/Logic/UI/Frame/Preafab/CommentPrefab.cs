using liulaoc.UI.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommentPrefab : MonoBehaviour
{
    #region view
    private Text titleTxt;
    private Text dateTxt;
    private Text postContentTxt;
    private Text commentContentTxt;
    private Text commentNumTxt;
    private Button enterBtn;
    #endregion
    #region 
    private int commentId;
    #endregion
    private void Awake()
    {
        BindView();
    }
    public void Init(int commentId)
    {
        this.commentId = commentId;
        UpdateView();
    }
    private void UpdateView()
    {
        
    }

    private void BindView()
    {
        titleTxt = transform.Find("TitleTxt").GetComponent<Text>();
        dateTxt = transform.Find("DateTxt").GetComponent<Text>();
        postContentTxt = transform.Find("PostContentTxt").GetComponent<Text>();
        commentContentTxt = transform.Find("CommentContentTxt").GetComponent<Text>();
        commentNumTxt = transform.Find("CommentNumTxt").GetComponent<Text>();
        enterBtn = GetComponent<Button>();
    }
}
