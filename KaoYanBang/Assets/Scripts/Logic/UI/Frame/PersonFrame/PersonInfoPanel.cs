using liulaoc.UI.Base;
using POJO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PersonInfoPanel : UIPanel
{
    #region view
    private Image headImg;
    private Text nameTxt;
    private Text phoneTxt;
    private Text pwdTxt;
    private Button pwdBtn;
    private Button phoneBtn;
    private Button nameBtn;
    private Button schoolBtn;
    private Button sbjBtn;
    private Text schoolTxt;
    private Text subjectTxt;
    #endregion
    #region model
    private string Name => NetDataManager.Instance.user.username;
    private string Phone => NetDataManager.Instance.user.phone;
    private string Password => NetDataManager.Instance.user.password;
    #endregion
    protected override void AddListener()
    {
        nameBtn.onClick.AddListener(() =>
        {

        });
        phoneBtn.onClick.AddListener(() =>
        {

        });
        pwdBtn.onClick.AddListener(() =>
        {

        });
        schoolBtn.onClick.AddListener(() =>
        {
            UIMgr.Instance.CreateFrame("SearchSchoolFrame");
        });
        sbjBtn.onClick.AddListener(() =>
        {
            UIMgr.Instance.CreateFrame("SearchSubjectFrame");
        });
    }

    protected override void BindView()
    {
        var group = transform.Find("Group");
        headImg = group.Find("Head").Find("HeadImg").GetComponent<Image>();
        nameTxt = group.Find("UserName").Find("NameTxt").GetComponent<Text>();
        phoneTxt = group.Find("Phone").Find("PhoneTxt").GetComponent<Text>();
        pwdTxt = group.Find("Password").Find("PwdTxt").GetComponent<Text>();
        schoolTxt = group.Find("School").Find("SchoolTxt").GetComponent<Text>();
        subjectTxt = group.Find("Subject").Find("SubjectTxt").GetComponent<Text>();
        nameBtn = group.Find("UserName").GetComponent<Button>();
        phoneBtn = group.Find("Phone").GetComponent<Button>();
        pwdBtn = group.Find("Password").GetComponent<Button>();
        schoolBtn = group.Find("School").GetComponent<Button>();
        sbjBtn = group.Find("Subject").GetComponent<Button>();
        UpdateView();
    }
    private void OnEnable()
    {
        UpdateView();
    }
    protected void UpdateView()
    {
        nameTxt.text = Name;
        phoneTxt.text = Phone;
        pwdTxt.text = Password;
        GetSchoolMsg msg = new GetSchoolMsg(NetDataManager.Instance.user.school_id);
        MsgManager.Instance.NetMsgCenter.NetGetSchoolById(msg, (respond) =>
         {
             var school = JsonHelper.DeserializeObject<List<School>>(respond.data);
             if(school != null && school.Count != 0)
             {
                 schoolTxt.text = school[0].school_name;
             }
         });
        GetSubjectByIdMsg sbjMsg = new GetSubjectByIdMsg(NetDataManager.Instance.user.subject_id);
        MsgManager.Instance.NetMsgCenter.NetGetSubjectById(sbjMsg,(respond)=> 
        {
            var sbj = JsonHelper.DeserializeObject<Subject>(respond.data);
            if(sbj!=null)
            {
                subjectTxt.text = sbj.subject_name;
            }
        });
    }
}
