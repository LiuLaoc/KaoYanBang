using liulaoc.UI.Base;
using POJO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PostFrame : UIFrame
{
    #region view
    private Text titleTxt;
    private Button backBtn;
    private Text postContent;
    private Text commentNumTxt;
    private Text moduleTxt;
    private Transform CommentContentPanel;
    #endregion
    #region Model
    public Invitation post { private set; get; }
    #endregion
    private void BindView()
    {
        var headerPanel = transform.Find("HeaderPanel");
        titleTxt = headerPanel.Find("Title").GetComponent<Text>();
        backBtn = headerPanel.Find("BackBtn").GetComponent<Button>();
        var post = transform.Find("Scroll View").Find("Viewport").Find("Content").Find("PostContent");
        postContent = post.Find("PostContent").GetComponent<Text>();
        commentNumTxt = post.Find("CommentNumTxt").GetComponent<Text>();
        moduleTxt = post.Find("ModuleImg").Find("ModuleTxt").GetComponent<Text>();
        CommentContentPanel = transform.Find("Scroll View").Find("Viewport").Find("Content").Find("CommentContentPanel");
    }
    public void Init(Invitation post)
    {
        this.post = post;
    }
    private void UpdateView()
    {
        titleTxt.text = post.invitation_title;
        postContent.text = post.content;
        //moduleTxt.text = post.plate;
        commentNumTxt.text = post.scan_number.ToString();

    }
}
