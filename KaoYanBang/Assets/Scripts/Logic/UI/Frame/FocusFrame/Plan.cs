using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Plan : MonoBehaviour
{
    #region model
    private POJO.Plan plan;
    private bool isInit = false;
    #endregion
    #region view
    private Button finishBtn;
    private Text tagTxt;
    private Text contentTxt;
    private Text finishTxt;
    #endregion
    private void Awake()
    {
        BindView();
        AddListener();
    }
    public void Init(POJO.Plan plan)
    {
        this.plan = plan;
        isInit = true;
        UpdateView();
    }
    protected void BindView()
    {
        finishBtn = transform.Find("FinishBtn").GetComponent<Button>();
        tagTxt = transform.Find("TagTxt").GetComponent<Text>();
        contentTxt = transform.Find("ContentTxt").GetComponent<Text>();
        finishTxt = transform.Find("FinishBtn").Find("FinishTxt").GetComponent<Text>();
    }
    protected void AddListener()
    {
        finishBtn.onClick.AddListener(() =>
        {
            int planStatus = plan.plan_status == 0 ?  1: 0;
            ChangePlanStatusMsg msg = new ChangePlanStatusMsg(planStatus, plan.plan_id);
            MsgManager.Instance.NetMsgCenter.NetChangePlanStatus(msg, (respond) =>
             {
                 plan.plan_status = planStatus;
                 UpdateView();
             });
        });
    }
    protected void UpdateView()
    {
        //更新TagTxt
        GetAllSubjectMsg msg = new GetAllSubjectMsg();
        MsgManager.Instance.NetMsgCenter.NetGetAllSbj(msg, (respond) =>
         {
             var list = JsonHelper.DeserializeObject<List<POJO.Subject>>(respond.data);
             foreach(var sbj in list)
             {
                 if(sbj.subject_id == plan.plan_type)
                 {
                     tagTxt.text = sbj.subject_name;
                 }
             }
         });
        contentTxt.text = plan.plan_content;
        if(plan.plan_status == 0)
        {
            finishTxt.text = "";
        }
        else
        {
            finishTxt.text = "√";
        }

    }
    private void OnEnable()
    {
        if(isInit)
        {
            UpdateView();
        }
    }
}
