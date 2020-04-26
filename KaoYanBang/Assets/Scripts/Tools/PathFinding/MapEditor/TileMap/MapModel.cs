using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace liulaoc.DstarPathFinding.Editor.TileMap
{
    /// <summary>
    /// 所有地图的道路数据
    /// </summary>
    [SerializeField]
    public class AllMapData
    {
        public Dictionary<string, MapData> AllRoads;
        public string SaveMap()
        {
            string path = Application.streamingAssetsPath + "/Maps/Map.json";
            string str = JsonHelper.SerializeObject(AllRoads);
            UnityIOHelper.SaveToFile(str, path);
            return str;
        }
        /// <summary>
        /// 根据章节和关卡加载当前可选择道路
        /// </summary>
        /// <param name="chapter"></param>
        /// <param name="level"></param>
        public void LoadMap()
        {
            string path = Application.streamingAssetsPath + "/Maps/Map.json";
            AllRoads = JsonHelper.DeserializeObject<Dictionary<string, MapData>>(UnityIOHelper.ReadFromFile(path));
            if (AllRoads == null)
            {
                AllRoads = new Dictionary<string, MapData>();
            }
        }
        /// <summary>
        /// 用于初始化
        /// </summary>
        /// <param name="chapter"></param>
        /// <param name="level"></param>
        public void CheckInit(int chapter, int level)
        {
            string key = chapter.ToString() + '-' + level.ToString();
            if (!AllRoads.ContainsKey(key))
            {
                MapData newMap = new MapData(key);
                AllRoads.Add(key, newMap);
            }
        }

        //废弃原因：如果策划做了地图，那么这个地图一定需要编辑，因此不需要手工创建
        ///// <summary>
        ///// 创建一张地图
        ///// </summary>
        ///// <param name="chapter"></param>
        ///// <param name="level"></param>
        ///// <returns></returns>
        //public MapData CreateMap(int chapter, int level)
        //{
        //    string key = chapter.ToString() + '-' + level.ToString();
        //    MapData roads = new MapData();
        //    roads.chapterLevelKey = key;
        //    roads.Roads = new Dictionary<int, List<Vector3Serializer>>();
        //    if (AllRoads.ContainsKey(key))
        //    {
        //        throw new System.Exception("该地图已存在");
        //    }
        //    else
        //    {
        //        AllRoads.Add(key, roads);
        //        return roads;
        //    }
        //}

        /// <summary>
        /// 选择某条道路
        /// </summary>
        public MapData SelectMap(int chapter, int level)
        {
            if (AllRoads == null)
            {
                LoadMap();
            }
            string key = chapter.ToString() + '-' + level.ToString();
            if (AllRoads.ContainsKey(key))
            {
            }
            else
            {
                MapData newData = new MapData(key);
                AllRoads.Add(key, newData);
            }
            return AllRoads[key];
        }

    }

    /// <summary>
    /// 一个地图的道路数据
    /// </summary>
    [SerializeField]
    public class MapData
    {
        public string chapterLevelKey = "";
        public Dictionary<int, List<Vector3Serializer>> Roads;
        public MapData(string key)
        {
            chapterLevelKey = key;
            Roads = new Dictionary<int, List<Vector3Serializer>>();
        }
        public MapData()
        {

        }

        /// <summary>
        /// 返回该道路的路径
        /// </summary>
        /// <param name="roadIndex"></param>
        /// <returns></returns>
        public List<Vector3Serializer> SelectRoad(int roadIndex)
        {
            return Roads[roadIndex];
        }

        /// <summary>
        /// 新建一个道路,默认道路从1开始，并且递增至n
        /// </summary>
        public void AddRoad()
        {
            var count = Roads.Keys.Count;
            var index = count + 1;
            Roads.Add(index, new List<Vector3Serializer>());
        }
        /// <summary>
        /// 移除最上层的路径
        /// </summary>
        public void RemoveRoad()
        {
            var count = Roads.Keys.Count;
            if (Roads.ContainsKey(count))
            {
                Roads.Remove(count);
            }
        }
        /// <summary>
        /// 添加这个道路结点，默认第一个为起点，最后一个为终点
        /// </summary>
        public void AddRoadPoint(int roadIndex, Vector3 point)
        {
            Roads[roadIndex].Add(point);
        }
        /// <summary>
        /// 移除某个道路结点
        /// </summary>
        /// <param name="roadIndex">道路索引</param>
        /// <param name="pointIndex">道路结点索引</param>
        public void RemoveRoadPoint(int roadIndex)
        {
            var count = Roads[roadIndex].Count;
            Roads[roadIndex].RemoveAt(count - 1);
        }
        /// <summary>
        /// 移动某个结点
        /// </summary>
        /// <param name="roadIndex">道路索引</param>
        /// <param name="pointIndex">道路结点索引</param>
        /// <param name="point">结点位置信息</param>
        public void MoveRoadPoint(int roadIndex, int pointIndex, Vector3 point)
        {

        }
        /// <summary>
        /// 获取当前地图中，道路索引对应的路径列表
        /// </summary>
        /// <param name="roadIndex"></param>
        /// <returns></returns>
        public List<Vector3Serializer> GetRoadPointList(int roadIndex)
        {
            return Roads[roadIndex];
        }
    }
}