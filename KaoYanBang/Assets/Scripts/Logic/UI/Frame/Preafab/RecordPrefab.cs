using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecordPrefab : MonoBehaviour
{
    #region model
    #endregion
    #region view
    private List<Text> allTexts;
    #endregion
    private void Awake()
    {
        var allTxt = GetComponentsInChildren<Text>();
        foreach(var txt in allTexts)
        {
            allTexts.Add(txt);
        }
    }
    public void Init(List<string> showDatas)
    {
        var id = 0;
        foreach(var str in showDatas)
        {
            allTexts[id].text = str;
            id++;
        }
    }
}
