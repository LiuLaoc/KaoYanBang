using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace liulaoc.UI.Base
{
    public class UIResourceMgr : TSingleton<UIResourceMgr>
    {
        public Dictionary<string, GameObject> UiDic { private set; get; } = new Dictionary<string, GameObject>();
        private UIResourceMgr()
        {
            GameObject[] frames = Resources.LoadAll<GameObject>("Frames");
            GameObject[] panels = Resources.LoadAll<GameObject>("Panels");
            foreach (var go in frames)
            {
                UiDic.Add(go.name, go);
            }
            foreach (var go in panels)
            {
                UiDic.Add(go.name, go);
            }
        }
        public GameObject Get(string str)
        {
            Debug.Log(str);
            return UiDic[str];
        }

    }
}