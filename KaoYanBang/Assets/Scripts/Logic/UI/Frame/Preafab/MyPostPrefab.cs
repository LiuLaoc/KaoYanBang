using liulaoc.UI.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyPostPrefab : MonoBehaviour
{
    #region
    private Text titleTxt;
    private Text dateTxt;
    private Text contentTxt;
    private Text viewNumTxt;
    private Button enterBtn;
    #endregion
    #region model
    private POJO.Invitation post;
    private bool isInit;
    #endregion
    public void Init(POJO.Invitation post)
    {
        isInit = true;
        this.post = post;
        UpdateView();
    }
    protected void BindView()
    {
        titleTxt = transform.Find("Title").GetComponent<Text>();
        dateTxt = transform.Find("Date").GetComponent<Text>();
        contentTxt = transform.Find("Content").GetComponent<Text>();
        viewNumTxt = transform.Find("CommentNum").GetComponent<Text>();
        enterBtn = transform.GetComponent<Button>();
    }
    protected void AddListener()
    {
        enterBtn.onClick.AddListener(() =>
        {
            //加载PostFrame
            UIMgr.Instance.CreateFrame("PostFrame");
            var frame = UIMgr.Instance.GetTopFrame() as PostFrame;
            frame.Init(post);
        });
    }
    protected void UpdateView()
    {
        titleTxt.text = post.invitation_title;
        dateTxt.text = post.create_time;
        contentTxt.text = post.content;
        viewNumTxt.text = post.scan_number.ToString();
    }
    private void Awake()
    {
        BindView();
        AddListener();
    }
    private void OnEnable()
    {
        if(isInit)
        {
            UpdateView();
        }
    }
}
