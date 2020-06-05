using liulaoc.UI.Base;
using POJO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PostPrefab : MonoBehaviour
{
    #region view
    private Button enterBtn;
    private Image headerImg;
    private Text nameTxt;
    private Text titleTxt;
    private Text contentTxt;
    private Text dateTxt;
    private Text tagTxt;
    private Text commentNumTxt;
    #endregion
    #region model
    private Invitation invitation;
    private bool isCommnuty;
    #endregion
    private void Awake()
    {
        BindView();
        AddListeners();
    }
    private void BindView()
    {
        enterBtn = GetComponent<Button>();
        headerImg = transform.Find("HeaderImg").GetComponent<Image>();
        nameTxt = transform.Find("NameTxt").GetComponent<Text>();
        titleTxt = transform.Find("TitleTxt").GetComponent<Text>();
        contentTxt = transform.Find("ContentTxt").GetComponent<Text>();
        dateTxt = transform.Find("DateTxt").GetComponent<Text>();
        commentNumTxt = transform.Find("CommentNumTxt").GetComponent<Text>();
    }
    private void AddListeners()
    {
        enterBtn.onClick.AddListener(()=> 
        {
            UIMgr.Instance.CreateFrame("PostFrame");
            var frame = UIMgr.Instance.GetTopFrame() as PostFrame;
            frame.Init(invitation);
        });
    }
    public void Init(Invitation invitation,bool isCommunity = true)
    {
        this.invitation = invitation;
        this.isCommnuty = isCommunity;
        UpdateView();
    }
    private void UpdateView()
    {
        GetUserByIdMsg userMsg = new GetUserByIdMsg(invitation.post_user);
        MsgManager.Instance.NetMsgCenter.NetGetUserById(userMsg, (respond) =>
        {
            var user = JsonHelper.DeserializeObject<User>(respond.data);
            nameTxt.text = user.username;
        });
        titleTxt.text = "标题 " + invitation.invitation_title;
        contentTxt.text = "内容 " + invitation.content;
        dateTxt.text = invitation.create_time;
        if(isCommnuty)
        {
            tagTxt.text = "#";
        }
        else
        {
            tagTxt.text = "#";
        }
        GetCommentMsg msg = new GetCommentMsg(invitation.invitation_id);
        MsgManager.Instance.NetMsgCenter.NetGetComment(msg,(respond)=> 
        {
            var list = JsonHelper.DeserializeObject<List<Comment>>(respond.data);
            commentNumTxt.text = list.Count.ToString();
        });
    }
}
