using liulaoc.UI.Base;
using POJO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleFrame : UIFrame
{
    #region view
    [SerializeField]private RectTransform content;
    [SerializeField] private GameObject prefab;
    #endregion
    private void Awake()
    {
        GetAllMsg msg = new GetAllMsg();
        MsgManager.Instance.NetMsgCenter.NetGetAllSbj(msg, (respond) =>
         {
             var sbjList = JsonHelper.DeserializeObject<List<Subject>>(respond.data);
             foreach(var sbj in sbjList)
             {
                 var go = Instantiate(prefab,content);
                 go.GetComponent<ModulePrefab>().Init(sbj);
             }
             var x = 1064f;
             var y = prefab.GetComponent<RectTransform>().rect.height;
             var line = (sbjList.Count / 2)+1;
             var allY = line * (y + 48.57f);
             content.sizeDelta = new Vector2(x,allY);
         });
    }
}
