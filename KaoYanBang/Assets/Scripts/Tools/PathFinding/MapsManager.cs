using System.Collections.Generic;
using UnityEngine;

namespace liulaoc.DstarPathFinding
{
    public class MapsManager : TMonoSingleton<MapsManager>, IInitializable
    {
        //LevelKey-Map Dic
        protected Dictionary<string, Map> mapsDic;

        void IInitializable.Init()
        {
            //Todo:初始化
        }

        public Map this[string levelKey]
        {
            get
            {
                if(mapsDic.ContainsKey(levelKey))
                {
                    return mapsDic[levelKey];
                }
                else
                {
                    //Todo:加载

                    return mapsDic[levelKey];
                }
            }
        }
        /// <summary>
        /// 用于将地图加入缓存，如果地图已存在，则将原先的地图覆盖
        /// </summary>
        /// <param name="map"></param>
        protected void AddMap(Map map)
        {
            if(mapsDic.ContainsKey(map.LevelKey))
            {
                Debug.Log("重复加入地图，已重置："+map.LevelKey);
                return;
            }
            mapsDic.Add(map.LevelKey,map);
        }
    }
}