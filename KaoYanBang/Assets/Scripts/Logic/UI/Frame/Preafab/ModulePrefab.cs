using liulaoc.UI.Base;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModulePrefab : MonoBehaviour
{
    #region view
    private Button enterBtn;
    private Text sbjTxt;
    #endregion
    #region model
    private POJO.Subject subject;
    private bool isInit = false;
    #endregion

    private void AddListener()
    {
        enterBtn.onClick.AddListener(() =>
        {
            UIMgr.Instance.CreateFrame("PostListFrame");
            var frame = UIMgr.Instance.GetTopFrame() as PostListFrame;
            frame.Init(PostFrameType.Module,subject);
        });
    }

    private void BindView()
    {
        enterBtn = transform.Find("EnterBtn").GetComponent<Button>();
        sbjTxt = transform.Find("Text").GetComponent<Text>();
    }

    public void Init(POJO.Subject sbj)
    {
        isInit = true;
        this.subject = sbj;
        BindView();
        AddListener();
        UpdateView();
    }

    private void UpdateView()
    {
        if(ResourceMgr.Instance.SubjectSprites.ContainsKey(subject.subject_name))
        {
            enterBtn.image.sprite = ResourceMgr.Instance.SubjectSprites[subject.subject_name];
        }
        else
        {
            Debug.LogError("不存在" + subject.subject_name);
        }
        sbjTxt.text = subject.subject_name;
    }
    private void OnEnable()
    {
        if(isInit)
        {
            UpdateView();
        }
    }
}
