using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecordPrefab : MonoBehaviour
{
    #region model
    #endregion
    #region view
    private List<Text> allTexts = new List<Text>();
    #endregion
    public void Init(List<string> showDatas)
    {
        var count = transform.childCount;
        for(int i=0;i<count;i++)
        {
            allTexts.Add(transform.GetChild(i).GetComponent<Text>());
        }

        var id = 0;
        foreach(var str in showDatas)
        {
            allTexts[id].text = str;
            id++;
        }
    }
}
