using liulaoc.UI.Base;
using POJO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubjectSearchPrefab : MonoBehaviour
{
    #region view
    [SerializeField] private Button selectBtn;
    [SerializeField] private Button unSelectImg;
    [SerializeField] private Text subjectNameTxt;
    #endregion
    #region model
    private Subject subject;
    #endregion
    public void Init(Subject subject)
    {
        this.subject = subject;
        subjectNameTxt.text = subject.subject_name;
        bool isSelectSchool = NetDataManager.Instance.user.subject_id == subject.subject_id;
        if (isSelectSchool)
        {
            unSelectImg.gameObject.SetActive(false);
        }
        else
        {
            selectBtn.gameObject.SetActive(false);
        }
        UIMsgCenter.Instance.SelectSubject += OnSelectSbj;
        unSelectImg.onClick.AddListener(() =>
        {
            ChangeSubjectIdMsg msg = new ChangeSubjectIdMsg(subject.subject_id, NetDataManager.Instance.user.user_id);
            MsgManager.Instance.NetMsgCenter.NetChangeSubjectId(msg, (respond) =>
            {
                unSelectImg.gameObject.SetActive(false);
                selectBtn.gameObject.SetActive(false);
                UIMsgCenter.Instance.SelectSubject(subject.subject_id);
            });
        });
    }
    private void OnSelectSbj(int subject_id)
    {
        if (subject_id != subject.subject_id)
        {
            unSelectImg.gameObject.SetActive(true);
            selectBtn.gameObject.SetActive(false);
        }
        else
        {
            NetDataManager.Instance.user.subject_id = subject_id;
            unSelectImg.gameObject.SetActive(false);
            selectBtn.gameObject.SetActive(true);
            selectBtn.onClick.RemoveAllListeners();
        }
    }
}
