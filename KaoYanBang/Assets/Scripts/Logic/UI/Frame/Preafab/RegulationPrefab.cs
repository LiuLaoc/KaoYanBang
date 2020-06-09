using POJO;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegulationPrefab : MonoBehaviour
{
    #region view
    [SerializeField]private Text titleTxt;
    [SerializeField] private Text contentTxt;
    [SerializeField] private Text dateTxt;
    [SerializeField] private Button enterBtn;
    #endregion
    #region
    private Invitation post;
    #endregion
    public void Init(Invitation post)
    {
        this.post = post;
        UpdateView();
    }

    private void UpdateView()
    {
        titleTxt.text = post.invitation_title;
        contentTxt.text = post.content;
        dateTxt.text = post.create_time;
    }
}
