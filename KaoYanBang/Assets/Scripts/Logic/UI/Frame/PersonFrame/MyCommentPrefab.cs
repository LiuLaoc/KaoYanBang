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
    private Comment comment;
    private Invitation post;
    #endregion
    public void Init(Comment comment)
    {
        this.comment = comment;
        UpdateView();
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
            frame.Init(post);
        });
    }
    private void UpdateView()
    {
        GetInvitationMsg msg = new GetInvitationMsg();
        MsgManager.Instance.NetMsgCenter.NetGetInvitation(msg, (respond) =>
         {
             var list = JsonHelper.DeserializeObject<List<Invitation>>(respond.data);
             foreach(var post in list)
             {
                 if(post.invitation_id == comment.comment_invitation)
                 {
                     this.post = post;
                     titleTxt.text = post.invitation_title;
                     postContentTxt.text = post.content;
                     GetCommentMsg commentMsg = new GetCommentMsg(comment.comment_invitation);
                     MsgManager.Instance.NetMsgCenter.NetGetComment(commentMsg, (responds) =>
                      {
                          var commentList = JsonHelper.DeserializeObject<List<Comment>>(responds.data);
                          commentNumTxt.text = commentList.Count.ToString();
                      });
                     break;
                 }
             }
         });
        dateTxt.text = comment.create_time;
        commentContentTxt.text = comment.content;
    }
}
