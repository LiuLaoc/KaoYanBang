using liulaoc.UI.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PersonPanel : UIPanel
{
    #region view
    private Image headImg;
    private Text nameTxt;
    private Text descriptionTxt;
    #endregion
    #region Model
    private string Name => NetDataManager.Instance.user.username;
    #endregion  
    protected override void Awake()
    {
        base.Awake();
    }
    protected override void AddListener()
    {
        
    }

    protected override void BindView()
    {
        //personInfoBind
        var personInfo = transform.Find("PersonInfo");
        headImg = personInfo.Find("Head").GetComponent<Image>();
        nameTxt = personInfo.Find("Name").GetComponent<Text>();
        descriptionTxt = personInfo.Find("Description").GetComponent<Text>();
    }
    protected void UpdateView()
    {
        nameTxt.text = Name;
    }
}
