using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace liulaoc.DstarPathFinding.Editor.TileMap
{

    /// <summary>
    /// 静态地图操作、数据类
    /// </summary>
    public class TileMapExtension : MonoBehaviour
    {
        public static int chapter;//当前选择的章节
        public static int level;//当前选择的关卡
        public static bool couldChangeLevel = false;
        public static string GetChapterLevelStr => chapter.ToString() + '-' + level.ToString();

        public static Dictionary<string, GameObject> AllTileMap = new Dictionary<string, GameObject>();
        public static GameObject nowTileMap;
        static bool isInit = false;
        public static AllMapData maps = new AllMapData();
        public static void Init()
        {
            if (!isInit)
            {
                maps.LoadMap();
                var allTileMap = Resources.LoadAll<GameObject>("TileMap");
                foreach (var obj in allTileMap)
                {
                    if (AllTileMap.ContainsKey(obj.name))
                    {
                        Debug.LogError("检查Resource目录下的Tilemap是否有重复文件：" + obj.name);
                    }
                    else
                    {
                        AllTileMap.Add(obj.name, obj);
                    }
                }
                isInit = true;
            }
        }
        public static bool IsChapterLevelExist()
        {
            string chapterLevel = chapter.ToString() + '-' + level.ToString();
            if (AllTileMap.ContainsKey(chapterLevel))
            {
                Debug.Log(chapterLevel + "关卡存在，请等待生成");
                return true;
            }
            else
            {
                Debug.LogError(chapterLevel + "该关卡不存在");
                return false;
            }
        }
        public static void InstitateTileMap()
        {
            if (IsChapterLevelExist())
            {
                if (nowTileMap != null)
                {
                    DestroyImmediate(nowTileMap);
                }
                nowTileMap = Instantiate(AllTileMap[GetChapterLevelStr]);
                nowTileMap.transform.Find("Layer1").GetComponent<TilemapRenderer>().enabled = true;
            }
        }
        public static void ClearCache()
        {   
            AllTileMap.Clear();
            isInit = false;
        }
    }
}