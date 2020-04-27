using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace liulaoc.UI.Base
{

    public abstract class UIPanel : UIBase
    {
        /// <summary>
        /// 用于绑定View
        /// </summary>
        protected abstract void BindView();
        /// <summary>
        /// 用于添加监听
        /// </summary>
        protected abstract void AddListener();
        protected virtual void Awake()
        {
            BindView();
            AddListener();
        }
    }
}