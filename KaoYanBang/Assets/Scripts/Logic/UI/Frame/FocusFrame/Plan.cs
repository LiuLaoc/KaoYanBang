using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Plan : MonoBehaviour
{
    #region model
    #endregion
    #region view
    private Button finishBtn;
    private Text tagTxt;
    private Text contentTxt;
    #endregion
    private void Awake()
    {
        BindView();
        AddListener();
        UpdateView();
    }
    protected void BindView()
    {
        finishBtn = transform.Find("FinishBtn").GetComponent<Button>();
        tagTxt = transform.Find("TagTxt").GetComponent<Text>();
        contentTxt = transform.Find("ContentTxt").GetComponent<Text>();
    }
    protected void AddListener()
    {

    }
    protected void UpdateView()
    {

    }
}
