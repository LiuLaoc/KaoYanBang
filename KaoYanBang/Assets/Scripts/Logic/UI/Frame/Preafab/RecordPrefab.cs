using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecordPrefab : MonoBehaviour
{
    #region view
    [SerializeField] private List<Text> allTexts = new List<Text>();
    #endregion
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
