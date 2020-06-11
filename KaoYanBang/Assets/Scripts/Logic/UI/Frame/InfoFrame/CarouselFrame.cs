using liulaoc.UI.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarouselFrame : UIFrame
{
    #region view
    [SerializeField] private Button backBtn;
    [SerializeField] private Text titleTxt;
    [SerializeField] private Text contentTxt;
    [SerializeField] private RectTransform content;
    #endregion
    #region model
    private Carousel carousel;
    #endregion
    public void Init(Carousel carousel)
    {
        backBtn.onClick.AddListener(UIMgr.Instance.RemoveFrame);
        this.carousel = carousel;
        UpdateView();
    }
    private void UpdateView()
    {
        titleTxt.text = carousel.title;
        contentTxt.text = carousel.content;
        var x = content.sizeDelta.x;
        var y = contentTxt.preferredHeight;
        content.sizeDelta = new Vector2(x,y);
    }
}
