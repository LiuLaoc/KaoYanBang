using liulaoc.UI.Base;
using POJO;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegulationFrame : UIFrame
{
    #region view
    [SerializeField] private Button backBtn;
    [SerializeField] private Text schoolTxt;
    [SerializeField] private Text titleTxt;
    [SerializeField] private Text contentTxt;
    [SerializeField] private RectTransform content;
    
    #endregion
    #region model
    private Invitation invitation;
    #endregion
    private void AddListener()
    {
        backBtn.onClick.AddListener(UIMgr.Instance.RemoveFrame);
    }
    public void Init(Invitation invitation)
    {
        AddListener();
        this.invitation = invitation;
        UpdateView();
    }

    private void UpdateView()
    {
        GetSchoolMsg msg = new GetSchoolMsg(invitation.school_id);
        MsgManager.Instance.NetMsgCenter.NetGetSchoolById(msg,(respond)=> 
        {
            var school = JsonHelper.DeserializeObject<List<School>>(respond.data);
            if(school!=null && school.Count!=0)
            {
                schoolTxt.text = school[0].school_name;
            }
            titleTxt.text = invitation.invitation_title;
            contentTxt.text = invitation.content;
            var x = content.sizeDelta.x;
            var y = contentTxt.preferredHeight;
            content.sizeDelta = new Vector2(x,y);
        });
    }
}
