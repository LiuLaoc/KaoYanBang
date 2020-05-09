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
        commentDateTxt.text = comment.create_time;
        commentContentTxt.text = comment.content;
        likeTxt.text = comment.like_number.ToString();
    }
    private void Addlistener()
    {
        complainBtn.onClick.AddListener(() =>
        {

        });
        likeBtn.onClick.AddListener(() =>
        {
            LikeMsg msg = new LikeMsg(comment.comment_id, comment.comment_invitation, NetDataManager.Instance.user.user_id);
            MsgManager.Instance.NetMsgCenter.NetLike(msg, (respond) =>
             {
                 var num = int.Parse(likeTxt.text);
                 likeTxt.text = (num + 1).ToString();
             });
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
