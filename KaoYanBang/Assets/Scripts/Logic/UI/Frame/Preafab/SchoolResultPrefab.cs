using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using POJO;
using liulaoc.UI.Base;

public class SchoolResultPrefab : MonoBehaviour
{
    #region view
    [SerializeField] private Button selectBtn;
    [SerializeField] private Button unSelectImg;
    [SerializeField] private Text schoolNameTxt;
    #endregion
    #region model
    private School school;
    #endregion
    public void Init(School school)
    {
        this.school = school;
        schoolNameTxt.text = school.school_name;
        bool isSelectSchool = NetDataManager.Instance.user.school_id == school.school_id;
        if (isSelectSchool)
        {
            unSelectImg.gameObject.SetActive(false);
        }
        else
        {
            selectBtn.gameObject.SetActive(false);
        }
        UIMsgCenter.Instance.SelectSchool += OnSelectSchool;
        unSelectImg.onClick.AddListener(() =>
        {
            ChangeSchoolIdMsg msg = new ChangeSchoolIdMsg(school.school_id,NetDataManager.Instance.user.user_id);
            MsgManager.Instance.NetMsgCenter.NetChangeSchoolId(msg,(respond)=> 
            {
                unSelectImg.gameObject.SetActive(false);
                selectBtn.gameObject.SetActive(false);
                UIMsgCenter.Instance.SelectSchool(school.school_id);
            });
        });
    }
    private void OnSelectSchool(int school_id)
    {
        if(school_id != school.school_id)
        {
            unSelectImg.gameObject.SetActive(true);
            selectBtn.gameObject.SetActive(false);
        }
        else
        {
            NetDataManager.Instance.user.school_id = school_id;
            unSelectImg.gameObject.SetActive(false);
            selectBtn.gameObject.SetActive(true);
            selectBtn.onClick.RemoveAllListeners();
        }
    }
}
