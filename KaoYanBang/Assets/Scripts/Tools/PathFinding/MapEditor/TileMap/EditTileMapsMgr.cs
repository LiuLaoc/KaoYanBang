using liulaoc.DstarPathFinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditTileMapsMgr : MapsManager
{
    private Dictionary<string, GameObject> AllTiles;
    //void IInitializable.Init()
    //{
    //    //Todo:初始化
    //    var list = GameLevelManager.Instance.GetAllChapterLevel();
    //    foreach(var chapLevel in list)
    //    {
    //        AllTiles.Add(chapLevel,Resources.Load<GameObject>("TileMap/"+chapLevel));
    //    }
    //    foreach(var chapLevel in list)
    //    {
    //        string mapData = UnityIOHelper.ReadFromFile(Application.streamingAssetsPath+"Maps/"+chapLevel);
    //        AddMap(JsonHelper.DeserializeObject<Map>(mapData));
    //    }
    //}
}
