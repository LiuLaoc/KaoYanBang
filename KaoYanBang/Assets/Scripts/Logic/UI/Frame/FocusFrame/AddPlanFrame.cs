using liulaoc.UI.Base;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddPlanFrame : UIFrame
{
    #region view
    private Button backBtn;
    private Button confirmBtn;
    private Dropdown tagDropdown;
    private InputField planContent;
    #endregion
    #region Model
    private int selectIndex;
    #endregion
    private void Awake()
    {
        BindView();
        AddListener();
        UpdateView();
    }
    protected void AddListener()
    {
        backBtn.onClick.AddListener(() =>
        {
            UIMgr.Instance.RemoveFrame();
        });
        tagDropdown.onValueChanged.AddListener((index) =>
        {
            selectIndex = index;
        });
        confirmBtn.onClick.AddListener(() =>
        {
            if(tagDropdown.options.Count == 0)
            {
                MsgManager.Instance.GlobalMsgManager.ShowErrorPanel("没有选择标签");
                return;
            }
            var subName = tagDropdown.options[selectIndex];
            AddPlanMsg msg = new AddPlanMsg(DateTime.Now.ToString(), planContent.text, selectIndex, NetDataManager.Instance.user.user_id);
            MsgManager.Instance.NetMsgCenter.NetAddPlan(msg, (responds) =>
             {
                 MainFrameModel.Instance.AddPlan(JsonHelper.DeserializeObject<POJO.Plan>(responds.data));
                 UIMgr.Instance.RemoveFrame();
             });
        });
    }

    protected void BindView()
    {
        var headerPanel = transform.Find("HeaderPanel");
        backBtn = headerPanel.Find("BackBtn").GetComponent<Button>();
        confirmBtn = headerPanel.Find("ConfirmBtn").GetComponent<Button>();
        var tagPanel = transform.Find("TagPanel");
        tagDropdown = tagPanel.Find("TagDropdown").GetComponent<Dropdown>();
        planContent = transform.Find("ContentPanel").Find("PlanContent").GetComponent<InputField>();
    }
    private void UpdateView()
    {
        GetAllSubjectMsg msg = new GetAllSubjectMsg();
        MsgManager.Instance.NetMsgCenter.NetGetAllSbj(msg, (responds) =>
         {
             var list = JsonHelper.DeserializeObject<List<POJO.Subject>>(responds.data);
             List<string> optionList = new List<string>();
             foreach(var sbj in list)
             {
                 optionList.Add(sbj.subject_name);
             }
             tagDropdown.ClearOptions();
             tagDropdown.AddOptions(optionList);
         });
    }
}
