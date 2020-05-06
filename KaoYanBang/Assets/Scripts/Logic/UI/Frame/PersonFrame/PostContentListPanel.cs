using liulaoc.UI.Base;
using POJO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostContentListPanel : UIPanel
{
    #region view
    private Transform group;
    #endregion
    #region Model
    private List<Invitation> myInvitations;
    #endregion
    protected override void AddListener()
    {

    }

    protected override void BindView()
    {
        group = transform.Find("Scroll View").Find("Viewport").Find("Group");
        UpdateView();
    }
    protected void UpdateView()
    {
        //获取我发布的Invitation
        //实例化
    }
}
