using liulaoc.UI.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewpagerPanel : UIPanel
{
    #region 参数设置
    [SerializeField]private float showTime = 2.0f;
    #endregion
    #region view
    private RectTransform showTrans;//显示区域
    #endregion
    #region model
    private int nowImgIndex=0;
    private List<RectTransform> protected override void BindView()
    {
        throw new System.NotImplementedException();
    }

    protected override void AddListener()
    {
        throw new System.NotImplementedException();
    }
    #endregion
    #region prefabResource
    #endregion


}
