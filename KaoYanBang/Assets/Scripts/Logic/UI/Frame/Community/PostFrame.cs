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
    public List<Comment> allComment { private set; get; }
    private List<CommentPrefab> allCommentPrefab { set; get; }
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
        commentNumTxt.text = post.scan_number.ToString();
        GetAllSubjectMsg msg = new GetAllSubjectMsg();
        MsgManager.Instance.NetMsgCenter.NetGetAllSbj(msg, (respond) =>
         {
             var list = JsonHelper.DeserializeObject<List<Subject>>(respond.data);
             foreach(var sbj in list)
             {
                 if(sbj.subject_id == post.plate)
                 {
                     moduleTxt.text = sbj.subject_name;
                 }
             }
         });
        //Comment
        GetCommentMsg commentMsg = new GetCommentMsg(post.plate);
        MsgManager.Instance.NetMsgCenter.NetGetComment(commentMsg, (respond) =>
         {
             allComment = JsonHelper.DeserializeObject<List<Comment>>(respond.data);
             foreach(var comment in allComment)
             {
                 var go = Instantiate(UIResourceMgr.Instance.Get("CommentPrefab"),CommentContentPanel);
                 var prefab = go.GetComponent<CommentPrefab>();
                 prefab.Init(comment);
                 allCommentPrefab.Add(prefab);
             }
             CommentContentPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(1080, 768.9f*allCommentPrefab.Count);
         });
    }
}
