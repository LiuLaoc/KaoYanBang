using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Table/Tables Path Info")]
public class TablesInfo : TSingletonScriptableObject<TablesInfo>
{
    public List<string> tablePaths = new List<string>();
}
