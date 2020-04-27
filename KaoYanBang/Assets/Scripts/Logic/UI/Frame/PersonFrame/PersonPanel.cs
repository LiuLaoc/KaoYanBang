using liulaoc.UI.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PersonPanel : UIPanel
{
    #region view
    private Image headImg;
    private Text nameTxt;
    private Text descriptionTxt;

    private Button setBtn;

    private Button postBtn;
    private Button commentBtn;
    private Text postTxt;
    private Text commentTxt;
    #endregion
    protected override void Awake()
    {
        base.Awake();
    }
    protected override void AddListener()
    {
        
    }

    protected override void BindView()
    {
        //personInfoBind
        var personInfo = transform.Find("PersonInfo");
        headImg = personInfo.Find("Head").GetComponent<Image>();
        nameTxt = personInfo.Find("Name").GetComponent<Text>();
        descriptionTxt = personInfo.Find("Description").GetComponent<Text>();
        //groupBind
        var group = transform.Find("Group");
        postBtn = group.Find("PostBtn").GetComponent<Button>();
        commentBtn = group.Find("CommentBtn").GetComponent<Button>();
        postTxt = postBtn.transform.Find("Text").GetComponent<Text>();
        commentTxt = commentBtn.transform.Find("Text").GetComponent<Text>();
        //bgBind
        setBtn = transform.Find("SetBtn").GetComponent<Button>();
    }
}
