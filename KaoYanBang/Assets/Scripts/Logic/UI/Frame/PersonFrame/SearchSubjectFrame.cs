using liulaoc.UI.Base;
using POJO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SearchSubjectFrame : UIFrame
{
    #region view
    [SerializeField] private Text searchFdTip;
    [SerializeField] private InputField searchIfd;
    [SerializeField] private Button confirmBtn;
    [SerializeField] private Button backBtn;
    [SerializeField] private RectTransform group;
    [SerializeField] private GridLayoutGroup groupLayout;
    [SerializeField] private GameObject prefab;
    #endregion
    #region model
    private List<Subject> allSubject;
    #endregion
    private void Awake()
    {
        GetAllMsg msg = new GetAllMsg();
        MsgManager.Instance.NetMsgCenter.NetGetAllSbj(msg, (respond) =>
        {
            allSubject = JsonHelper.DeserializeObject<List<Subject>>(respond.data);
            if (allSubject == null || allSubject.Count == 0)
            {
                return;
            }
            foreach (var subject in allSubject)
            {
                GameObject go = Instantiate(prefab, group);
                go.GetComponent<SubjectSearchPrefab>().Init(subject);
            }
            var line = allSubject.Count;
            var size = groupLayout.cellSize.y;
            group.sizeDelta = new Vector2(group.sizeDelta.x, line * size);
        });
        backBtn.onClick.AddListener(() =>
        {
            UIMgr.Instance.RemoveFrame();
        });
        confirmBtn.onClick.AddListener(() =>
        {
            for (int i = 0; i < group.childCount; i++)
            {
                Destroy(group.transform.GetChild(i));
            }
            List<Subject> searchSchool = new List<Subject>();
            foreach (var subject in allSubject)
            {
                if (subject.subject_name.Contains(searchIfd.text))
                {
                    searchSchool.Add(subject);
                }
            }
            //显示学校
            foreach (var subject in searchSchool)
            {
                GameObject go = Instantiate(prefab, group);
                go.GetComponent<SubjectSearchPrefab>().Init(subject);
            }
            var line = searchSchool.Count;
            var size = groupLayout.cellSize.y;
            group.sizeDelta = new Vector2(group.sizeDelta.x, line * size);
        });
    }
}
