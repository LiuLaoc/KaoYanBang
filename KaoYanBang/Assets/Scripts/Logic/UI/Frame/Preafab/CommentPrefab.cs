using liulaoc.UI.Base;
using POJO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommentPrefab : MonoBehaviour
{
    #region view
    private Text userNameTxt;
    private Text commentDateTxt;
    private Text commentContentTxt;
    private Text commentNumTxt;
    private Text likeTxt;
    private Button complainBtn;
    private Button likeBtn;
    private Button commentBtn;
    #endregion
    #region Model
    private Comment comment;
    private bool isLike = false;
    #endregion
    private void Awake()
    {
        BindView();
        Addlistener();
    }
    public void Init(Comment comment)
    {
        this.comment = comment;
        UpdateView();
    }
    private void UpdateView()
    {
        GetUserByIdMsg msg = new GetUserByIdMsg(comment.comment_user);
        MsgManager.Instance.NetMsgCenter.NetGetUserById(msg, (respond) =>
        {
            User user = JsonHelper.DeserializeObject<User>(respond.data);
            userNameTxt.text = user.username;
        });
        JudgeLikeMsg likeMsg = new JudgeLikeMsg(comment.comment_id,NetDataManager.Instance.user.user_id);
        MsgManager.Instance.NetMsgCenter.NetJudgeLike(likeMsg, (respond) =>
         {
             isLike = bool.Parse(respond.data);
             if(isLike)
             {
                 likeBtn.image.color = new Color(255f,255f,255f);
             }
             else
             {
                 likeBtn.image.color = new Color(0, 0, 0);
             }
         });
        commentDateTxt.text = comment.create_time;
        commentContentTxt.text = comment.content;
        GetLikeCountMsg likeCountMsg = new GetLikeCountMsg(comment.comment_id);
        MsgManager.Instance.NetMsgCenter.NetGetLikeCount(likeCountMsg, (respond) =>
         {
             likeTxt.text = respond.data;
         });
    }
    private void Addlistener()
    {
        complainBtn.onClick.AddListener(() =>
        {

        });
        likeBtn.onClick.AddListener(() =>
        {
            if(!isLike)
            {
                LikeMsg msg = new LikeMsg(comment.comment_id, comment.comment_invitation, NetDataManager.Instance.user.user_id);
                MsgManager.Instance.NetMsgCenter.NetLike(msg, (respond) =>
                {
                    var num = int.Parse(likeTxt.text);
                    likeTxt.text = (num + 1).ToString();
                    isLike = true;
                    UpdateView();
                });
            }
            else
            {
                LikeMsg msg = new LikeMsg(comment.comment_id, comment.comment_invitation, NetDataManager.Instance.user.user_id);
                MsgManager.Instance.NetMsgCenter.NetLike(msg, (respond) =>
                {
                    var num = int.Parse(likeTxt.text);
                    likeTxt.text = (num - 1).ToString();
                    isLike = false;
                    UpdateView();
                });
            }
        });
        commentBtn.onClick.AddListener(() =>
        {

        });
    }

    private void BindView()
    {
        userNameTxt = transform.Find("UserNameTxt").GetComponent<Text>();
        commentDateTxt = transform.Find("CommentDateTxt").GetComponent<Text>();
        commentContentTxt = transform.Find("ContentTxt").GetComponent<Text>();
        complainBtn = transform.Find("ComplainBtn").GetComponent<Button>();
        var footGroup = transform.Find("FootGroup");
        likeBtn = footGroup.Find("LikeBtn").GetComponent<Button>();
        likeTxt = likeBtn.transform.Find("LikeTxt").GetComponent<Text>();
        commentBtn = footGroup.Find("CommentBtn").GetComponent<Button>();
    }
}
