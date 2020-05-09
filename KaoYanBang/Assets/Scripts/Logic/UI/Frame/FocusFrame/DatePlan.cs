using liulaoc.UI.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DatePlan : MonoBehaviour
{
    #region view
    private Text dateTxt;
    private Text numTxt;
    private Button enterBtn;
    #endregion
    #region model
    private string createDate;
    private bool isInit;
    #endregion
    private void AddListener()
    {
        enterBtn.onClick.AddListener(() =>
        {
            UIMgr.Instance.CreateFrame("PlanFrame");
            var frame = UIMgr.Instance.GetTopFrame() as PlanFrame;
            frame.Init(createDate);
        });
    }
    private void BindView()
    {
        dateTxt = transform.Find("DateTxt").GetComponent<Text>();
        numTxt = transform.Find("NumTxt").GetComponent<Text>();
        enterBtn = GetComponent<Button>();
    }
    private void Awake()
    {
        BindView();
        AddListener();
    }
    private void UpdateView()
    {
        dateTxt.text = createDate;
        var list = MainFrameModel.Instance.allPlan[createDate];
        int allNum = 0;
        int finishNum = 0;
        foreach(var plan in list)
        {
            if(plan.plan_status == 1)
            {
                finishNum++;
            }
            allNum++;
        }
        var num = finishNum.ToString() + "/" + allNum.ToString();
        numTxt.text = num;
    }
    public void Init(string createDate)
    {
        this.createDate = createDate;
        isInit = true;
        UpdateView();
    }
    private void OnEnable()
    {
        if(isInit)
        {
            UpdateView();
        }
    }
}
