using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewPagePrefab : MonoBehaviour
{
    private Image viewImg;
    private void Awake()
    {
        BindView();
        AddListener();
    }

    private void BindView()
    {
        viewImg = GetComponent<Image>();
    }

    private void AddListener()
    {
        
    }
}
