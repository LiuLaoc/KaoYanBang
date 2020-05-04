using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace liulaoc.UI.Base
{
    public class UIFrame : UIBase
    {
        protected List<UIPanel> panelList = new List<UIPanel>();
        protected void AddPanel(UIPanel uiPanel)
        {
            panelList.Add(uiPanel);
        }
        protected void Start()
        {
            UIPanel[] uIPanels = transform.GetComponentsInChildren<UIPanel>();
            foreach (var uIPanel in uIPanels)
            {
                AddPanel(uIPanel);
            }
        }
        
    }
}