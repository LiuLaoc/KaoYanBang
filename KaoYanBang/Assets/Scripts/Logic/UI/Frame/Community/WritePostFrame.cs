using liulaoc.UI.Base;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WritePostFrame : UIFrame
{
    #region view
    private Button backBtn;
    private Button confirmBtn;
    private Dropdown dropDown;
    private InputField titleIfd;
    private InputField contentIfd;
    #endregion
    #region model
    private int isSelect;
    #endregion
    private void Awake()
    {
        BindView();
        AddListener();
    }

    private void BindView()
    {
        backBtn = transform.Find("HeaderPanel").Find("BackBtn").GetComponent<Button>();
        confirmBtn = transform.Find("HeaderPanel").Find("ConfirmBtn").GetComponent<Button>();
        var group = transform.Find("ContentListPanel").Find("CommentPrefab");
        dropDown = group.Find("Dropdown").GetComponent<Dropdown>();
        titleIfd = group.Find("PostTitle").GetComponent<InputField>();
        contentIfd = group.Find("PostContent").GetComponent<InputField>();
    }

    private void AddListener()
    {
        GetAllMsg msg = new GetAllMsg();
        MsgManager.Instance.NetMsgCenter.NetGetAllSbj(msg, (respond) =>
         {
             var list = JsonHelper.DeserializeObject <List<POJO.Subject>>(respond.data);
             List<string> optionList = new List<string>();
             foreach(var sbj in list)
             {
                 optionList.Add(sbj.subject_name);
             }
             dropDown.AddOptions(optionList);
         });
        dropDown.onValueChanged.AddListener((index) =>
        {
            isSelect = index;
        });
        backBtn.onClick.AddListener(() =>
        {
            UIMgr.Instance.RemoveFrame();
        });
        confirmBtn.onClick.AddListener(() =>
        {
            if(dropDown.options.Count == 0 || titleIfd.text == "" || contentIfd.text == "")
            {
                MsgManager.Instance.GlobalMsgManager.ShowErrorPanel("标签、标题、内容请勿为空");
                return; 
            }
            AddPostMsg postMsg = new AddPostMsg(contentIfd.text,titleIfd.text,isSelect,NetDataManager.Instance.user.user_id,(int)InvitationType.Invitation,0);
            MsgManager.Instance.NetMsgCenter.NetAddPost(postMsg, (respond) =>
             {
                 UIMgr.Instance.RemoveFrame();
             });
        });
    }
    private void UpdateView()
    {
        GetAllMsg msg = new GetAllMsg();
        MsgManager.Instance.NetMsgCenter.NetGetAllSbj(msg, (respond) =>
         {
             var list = JsonHelper.DeserializeObject<List<POJO.Subject>>(respond.data);
             List<string> optionList = new List<string>();
             foreach(var sbj in list)
             {
                 optionList.Add(sbj.subject_name);
             }
             dropDown.options.Clear();
             dropDown.AddOptions(optionList);
         });
    }
    private void OnEnable()
    {
        UpdateView();
    }
}
