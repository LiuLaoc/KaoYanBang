using liulaoc.UI.Base;
using POJO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReGradeFrame : UIFrame
{
    #region view
    [SerializeField] private Button backBtn;
    [SerializeField] private TablePrefab tablePrefab;
    #endregion
    #region
    int count = 0;
    int maxCount =0;
    #endregion
    #region model
    List<string> title = new List<string>()
            {
                "年份","专业","总分","政治","英语","专业课1","专业课2"
            };
    List<List<string>> scoreDatas = new List<List<string>>();
    #endregion
    private void Awake()
    {
        AddListener();
        GetScoreBySchoolIdMsg msg = new GetScoreBySchoolIdMsg(NetDataManager.Instance.user.school_id);
        MsgManager.Instance.NetMsgCenter.NetGetScoreBySchoolId(msg, (respond) =>
         {
             var scores = JsonHelper.DeserializeObject<List<SchoolScore>>(respond.data);
             maxCount = scores.Count;
             foreach(var score in scores)
             {
                 List<string> scoreData = new List<string>();
                 scoreData.Add(score.year);
                 GetSubjectByIdMsg sbjMsg = new GetSubjectByIdMsg(score.subject);
                 MsgManager.Instance.NetMsgCenter.NetGetSubjectById(sbjMsg, (responds) =>
                 {
                     var sbj = JsonHelper.DeserializeObject<Subject>(responds.data);
                     scoreData.Add(sbj.subject_name);
                     scoreData.Add(score.total_points.ToString()) ;
                     scoreData.Add(score.politics.ToString());
                     scoreData.Add(score.english.ToString()) ;
                     scoreData.Add(score.profession1.ToString());
                     scoreData.Add(score.profession2.ToString());
                     scoreDatas.Add(scoreData);
                     count++;
                 });
             }
         });

    }
    private void AddListener()
    {
        backBtn.onClick.AddListener(UIMgr.Instance.RemoveFrame);
    }
    private IEnumerator InitAsync()
    {
        while(count < maxCount)
        {
            yield return null;
        }
        tablePrefab.Init(title,scoreDatas);
    }
}
