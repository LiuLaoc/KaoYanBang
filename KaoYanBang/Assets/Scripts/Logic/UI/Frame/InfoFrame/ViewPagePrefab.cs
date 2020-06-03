using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewPagePrefab : MonoBehaviour
{
    #region view
    private Image viewImg;
    private Button enterBtn;
    #endregion
    #region model
    private Carousel carousel;
    #endregion
    public void Init(Carousel car)
    {
        carousel = car;
        BindView();
        AddListener();
        Base64ToImg(viewImg);
    }
    private void BindView()
    {
        viewImg = GetComponent<Image>();
        enterBtn = GetComponent<Button>();
    }

    private void AddListener()
    {
        enterBtn.onClick.AddListener(()=> 
        {
            WWW www = new WWW(carousel.url);
            Application.OpenURL(www.url);
        });
    }
    private void Base64ToImg(Image imgComponent)
    {
        string base64 = carousel.content;
        byte[] bytes = Convert.FromBase64String(base64);
        Texture2D tex2D = new Texture2D(100, 100);
        tex2D.LoadImage(bytes);
        Sprite s = Sprite.Create(tex2D, new Rect(0, 0, tex2D.width, tex2D.height), new Vector2(0.5f, 0.5f));
        imgComponent.sprite = s;
        Resources.UnloadUnusedAssets();
    }
}
