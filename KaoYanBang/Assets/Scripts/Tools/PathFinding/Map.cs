using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace liulaoc.DstarPathFinding
{
    public enum GridType
    {
        None,       //空地
        Boundary,   //边界
        Road,       //道路
        Building,    //建筑
        Teleporter, //传送点
    }
    [Serializable]
    public class Map
    {
        #region 数据
        public string LevelKey;
        //起始点、结束点
        public Vector3Serializer StartPos { set; get; }
        public Vector3Serializer EndPos { set; get; }
        public GridType[,] MapGrid;
        public float GridSize = 1.0f;
        public int Width { get => (int)(EndPos.x - StartPos.x); }
        public int Height { get => (int)(EndPos.z - StartPos.z); }
        #endregion
        public Map(float gridSize = 1.0f)
        {
            GridSize = gridSize;
        }
        public Map()
        {

        }
        #region 对外接口
        /// <summary>
        /// 地图格子中心点的世界坐标->中心点
        /// 格子中的任意一点修正为中心
        /// </summary>
        /// <param name="pos">物体世界坐标</param>
        /// <returns></returns>
        public Vector3 GetGridCenterWorldPos(Vector3 pos)
        {
            int x = (int)(pos.x - StartPos.x);
            int z = (int)(pos.z - StartPos.z);
            return new Vector3(StartPos.x + x + 0.5f, StartPos.y, StartPos.z + z + 0.5f);
        }
        /// <summary>
        /// 地图格子中心点的世界坐标->中心点
        /// 通过二维数组坐标-获取中心点
        /// </summary>
        /// <param name="x"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        public Vector3 GetGridCenterWorldPos(int x, int z)
        {
            return new Vector3(StartPos.x + x + 0.5f, StartPos.y, StartPos.z + z + 0.5f);
        }
        /// <summary>
        /// 二维数组坐标->中心点
        /// </summary>
        /// <param name="x"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        public Vector3 GetGridCenterWorldPos(Vector2Int pos)
        {
            return new Vector3(StartPos.x + pos.x + 0.5f, StartPos.y, StartPos.z + pos.y + 0.5f);
        }
        /// <summary>
        /// 世界坐标->中心点(x,y)
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public Vector2Int GetIndexByWorldPos(Vector3 pos)
        {
            int x = (int)(pos.x - StartPos.x);
            int z = (int)(pos.z - StartPos.z);
            return new Vector2Int(x, z);
        }
        /// <summary>
        /// 世界坐标(x,z)->中心点(x,y)
        /// </summary>
        /// <param name="x"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        public Vector2Int GetIndexByWorldPos(float x, float z)
        {
            int xx = (int)(x - StartPos.x);
            int zz = (int)(z - StartPos.z);
            return new Vector2Int(xx, zz);
        }
        private int[] judgeArray = { -1, 0, 1, 0, 0, -1, 0, 1 };
        /// <summary>
        /// 判断该格子及其周围是否可以行走
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool AroundPosCouldWalk(int x, int y)
        {
            if (!IsGridCouldWalk(x, y)) return false;
            for (int i = 0; i < judgeArray.Length; i += 2)
            {
                if (IsGridCouldWalk(x + judgeArray[i], y + judgeArray[i + 1]))
                {
                    return true;
                }
            }
            return false;
        }
        public bool AroundPosCouldWalk(int x, int y, out Vector2Int aroundPos)
        {
            aroundPos = new Vector2Int(-1, -1);
            if (!IsGridCouldWalk(x, y)) return false;
            for (int i = 0; i < judgeArray.Length; i += 2)
            {
                if (IsGridCouldWalk(x + judgeArray[i], y + judgeArray[i + 1]))
                {
                    aroundPos.x = x + judgeArray[i];
                    aroundPos.y = y + judgeArray[i + 1];
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 判断该格子是否可走
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool IsGridCouldWalk(int x, int y)
        {
            if (!IsValidGrid(x, y))
            {
                return false;
            }
            var type = MapGrid[x, y];
            if (type == GridType.None || type == GridType.Road || type == GridType.Teleporter)
            {
                return true;
            }
            else
            {
                return false;
            }


        }
        #endregion

        /// <summary>
        /// 判断该方格是否合法：越界
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private bool IsValidGrid(int x, int y)
        {
            if (x < 0 || x >= Width || y < 0 || y >= Height)
            {
                return false;
            }
            return true;
        }
    }
    [SerializeField]
    public class Vector3Serializer
    {
        public float x;
        public float y;
        public float z;
        public Vector3Serializer(float rX, float rY, float rZ)
        {
            x = rX;
            y = rY;
            z = rZ;
        }
        public static implicit operator Vector3(Vector3Serializer vValue)
        {
            return new Vector3(vValue.x, vValue.y, vValue.z);
        }
        public static implicit operator Vector3Serializer(Vector3 vValue)
        {
            return new Vector3Serializer(vValue.x, vValue.y, vValue.z);
        }

    }
}