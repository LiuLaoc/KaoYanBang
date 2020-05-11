using liulaoc.UI.Base;
using POJO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PostFrame : UIFrame
{
    #region view
    private InputField commentIfd;
    private Text titleTxt;
    private Button backBtn;
    private Text postContent;
    private Text commentNumTxt;
    private Text moduleTxt;
    private Transform CommentContentPanel;
    private Button commentBtn;
    #endregion
    #region Model
    public Invitation post { private set; get; }
    public List<Comment> allComment { private set; get; } = new List<Comment>();
    private List<CommentPrefab> allCommentPrefab { set; get; } = new List<CommentPrefab>();
    #endregion
    private void Awake()
    {
        BindView();
        AddListener();
    }
    private void AddListener()
    {
        backBtn.onClick.AddListener(() => UIMgr.Instance.RemoveFrame());
        commentBtn.onClick.AddListener(() =>
        {
            AddCommentMsg msg = new AddCommentMsg(post.invitation_id,NetDataManager.Instance.user.user_id,commentIfd.text);
            MsgManager.Instance.NetMsgCenter.NetAddComment(msg,(respond)=>
            {
                UpdateView();
                commentIfd.text = "";
            });
        });
    }
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
        commentBtn = transform.Find("FooterPanel").Find("CommentBtn").GetComponent<Button>();
        commentIfd = transform.Find("FooterPanel").Find("CommentIfd").GetComponent<InputField>();
    }
    public void Init(Invitation post)
    {
        this.post = post;
        AddScanMsg msg = new AddScanMsg(post.invitation_id);
        MsgManager.Instance.NetMsgCenter.NetAddScan(msg, (respond) =>
        {
            post.scan_number++;
            UpdateView();
        });
    }
    private void UpdateView()
    {
        ClearView();
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
        GetCommentMsg commentMsg = new GetCommentMsg(post.invitation_id);
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
    private void ClearView()
    {
        int count = CommentContentPanel.childCount;
        for(int i=0;i<count;i++)
        {
            Destroy(CommentContentPanel.GetChild(i).gameObject);
        }
        allCommentPrefab.Clear();
        allComment.Clear();
    }
}
