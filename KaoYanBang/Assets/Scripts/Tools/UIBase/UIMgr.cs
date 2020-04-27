using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace liulaoc.UI.Base
{

    public class UIMgr : TMonoSingleton<UIMgr>, IInitializable
    {
        [SerializeField] private string initFrameName;
        private Transform UIRoot;
        private Stack<UIFrame> UIStack = new Stack<UIFrame>();
        public void Init()
        {
            UIRoot = transform.Find("UIRoot");
            CreateFrame("StartFrame");
        }
        public void CreateFrame(string uiName)
        {
            if (UIStack.Count != 0)
            {
                UIStack.Peek().gameObject.SetActive(false);
            }

            GameObject go = Instantiate(UIResourceMgr.Instance.UiDic[uiName], UIRoot);
            var frame = go.GetComponent<UIFrame>();
            UIStack.Push(frame);
        }
        /// <summary>
        /// 返回上一级Frame
        /// </summary>
        public void RemoveFrame()
        {
            if (UIStack.Count <= 1)
                return;
            GameObject go = UIStack.Pop().gameObject;
            Destroy(go);
            UIStack.Peek().gameObject.SetActive(true);
        }
        /// <summary>
        /// 获取当前页面
        /// </summary>
        /// <returns></returns>
        public UIFrame GetTopFrame()
        {
            return UIStack.Peek();
        }


    }
}