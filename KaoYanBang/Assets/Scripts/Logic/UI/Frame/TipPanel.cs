using liulaoc.UI.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipPanel : UIPanel
{
    #region view
    private Button backBtn;
    private Text contentTxt;
    #endregion
    protected override void AddListener()
    {
        backBtn.onClick.AddListener(() =>
        {
            Destroy(gameObject);
        });
    }

    protected override void BindView()
    {
        backBtn = transform.Find("BackBtn").GetComponent<Button>();
        contentTxt = transform.Find("MsgTxt").GetComponent<Text>();
    }
    public void Init(string content)
    {
        contentTxt.text = content;
    }
}
