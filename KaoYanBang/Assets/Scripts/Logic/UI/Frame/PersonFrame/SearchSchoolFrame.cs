using liulaoc.UI.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using POJO;
public class SearchSchoolFrame : UIFrame
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
    private List<School> allSchool;
    #endregion
    private void Awake()
    {
        GetAllMsg msg = new GetAllMsg();
        MsgManager.Instance.NetMsgCenter.NetGetAllSchool(msg,(respond)=> 
        {
            allSchool = JsonHelper.DeserializeObject<List<School>>(respond.data);
            if (allSchool == null || allSchool.Count == 0)
            {
                return;
            }
            foreach(var school in allSchool)
            {
                GameObject go = Instantiate(prefab, group);
                go.GetComponent<SchoolResultPrefab>().Init(school);
            }
            var line = allSchool.Count;
            var size = groupLayout.cellSize.y;
            group.sizeDelta = new Vector2(group.sizeDelta.x, line * size);
        });
        backBtn.onClick.AddListener(() =>
        {
            UIMgr.Instance.RemoveFrame();
        });
        confirmBtn.onClick.AddListener(() =>
        {
            for(int i=0;i<group.childCount;i++)
            {
                Destroy(group.transform.GetChild(i));
            }
            List<School> searchSchool = new List<School>();
            foreach(var school in allSchool)
            {
                if(school.school_name.Contains(searchIfd.text))
                {
                    searchSchool.Add(school);
                }
            }
            //显示学校
            foreach(var school in searchSchool)
            {
                GameObject go = Instantiate(prefab,group);
                go.GetComponent<SchoolResultPrefab>().Init(school);
            }
            var line = searchSchool.Count;
            var size = groupLayout.cellSize.y;
            group.sizeDelta = new Vector2(group.sizeDelta.x,line*size);
        });
    }
}
