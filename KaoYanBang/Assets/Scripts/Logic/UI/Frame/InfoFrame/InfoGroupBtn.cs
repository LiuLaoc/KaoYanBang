using liulaoc.UI.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoGroupBtn : UIPanel
{
    #region view
    private Button gradeBtn;
    private Button reportBtn;
    private Button subjectBtn;
    private Button regradeBtn;
    #endregion
    protected override void AddListener()
    {
        gradeBtn.onClick.AddListener(() =>
        {
            UIMgr.Instance.CreateFrame("");
        });
        reportBtn.onClick.AddListener(() =>
        {
            UIMgr.Instance.CreateFrame("");
        });
        subjectBtn.onClick.AddListener(() =>
        {
            UIMgr.Instance.CreateFrame("");
        });
        regradeBtn.onClick.AddListener(() =>
        {
            UIMgr.Instance.CreateFrame("");
        });
    }

    protected override void BindView()
    {
        gradeBtn = transform.Find("Grade").Find("GradeBtn").GetComponent<Button>();
        reportBtn = transform.Find("Report").Find("ReportBtn").GetComponent<Button>();
        subjectBtn = transform.Find("Subject").Find("SubjectBtn").GetComponent<Button>();
        regradeBtn = transform.Find("ReGrade").Find("ReGradeBtn").GetComponent<Button>();
    }
}
