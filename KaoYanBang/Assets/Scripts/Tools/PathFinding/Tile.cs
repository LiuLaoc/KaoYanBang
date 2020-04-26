using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
namespace liulaoc.DstarPathFinding
{

    public enum TileRotateType
    {
        Self = 1,//Tile必须为n*n，原点为正方形中心，即普通x、y坐标系
        Anchor = 2//Tile为n*m，需要设置锚点，方格用二维数组描述
    }
    public enum TileDir
    {
        Right,
        Down,
        Left,
        Up
    }
    public class Tile
    {
        public string LevelKey { get; set; }
        private readonly float TileSize = 0.5f;
        public Vector2Int Anchor { get; private set; }//锚点位于数组原坐标
        public List<Vector2Int> AllNodes { get; private set; }//结点相对坐标(每个结点都坐标-锚点坐标)
        public TileDir Dir { get; private set; }
        public TileRotateType rotateType { get; private set; }
        /// <summary>
        /// 有锚点的初始化
        /// </summary>
        /// <param name="anchor"></param>
        /// <param name="_rotateType"></param>
        /// <param name="allnodes"></param>
        public Tile(Vector2Int anchor, TileRotateType _rotateType, List<int> allnodes)
        {
            AllNodes = new List<Vector2Int>();
            Anchor = anchor;
            rotateType = _rotateType;
            for (int i = 0; i < allnodes.Count; i += 2)
            {
                AllNodes.Add(new Vector2Int(allnodes[i], allnodes[i + 1]));
            }
        }
        /// <summary>
        /// 锚点为自身的初始化
        /// </summary>
        /// <param name="_rotateType"></param>
        /// <param name="allnodes"></param>
        public Tile(TileRotateType _rotateType, List<int> allnodes)
        {
            AllNodes = new List<Vector2Int>();
            rotateType = _rotateType;
            for (int i = 0; i < allnodes.Count; i += 2)
            {
                AllNodes.Add(new Vector2Int(allnodes[i], allnodes[i + 1]));
            }
        }
        /// <summary>
        /// 将Tile顺时针旋转
        /// </summary>
        public void Rotate()
        {
            Dir = (TileDir)(((int)Dir + 1) % 3);
            switch (rotateType)
            {
                case TileRotateType.Anchor:
                    RotateAnchor();
                    break;
                case TileRotateType.Self:
                    RotateSelf();
                    break;
            }
        }
        private void RotateAnchor()
        {
            //变换坐标系，改为以Anchor为原点
            for (int i = 0; i < AllNodes.Count; i++)
            {
                AllNodes[i] = new Vector2Int(AllNodes[i].x - Anchor.x, AllNodes[i].y - Anchor.y);
            }
            for (int i = 0; i < AllNodes.Count; i++)
            {
                AllNodes[i] = new Vector2Int(AllNodes[i].y, -AllNodes[i].x);
            }
        }
        private void RotateSelf()
        {
            for (int i = 0; i < AllNodes.Count; i++)
            {
                AllNodes[i] = new Vector2Int(AllNodes[i].y, -AllNodes[i].x);
            }
        }
        public void PrintAllNodes()
        {
            Debug.Log(rotateType + ":" + Dir);
            foreach (Vector2Int node in AllNodes)
            {
                Debug.Log(node);
            }
        }
        /// <summary>
        /// 设置方向
        /// </summary>
        /// <param name="dir"></param>
        public void SetDir(TileDir dir)
        {
            int count = Dir - dir;
            for (int i = 0; i < count; i++)
            {
                Rotate();
            }
        }
        /// <summary>
        /// 获取当前所有网格在Map中的坐标索引
        /// </summary>
        /// <param name="x">postion.x</param>
        /// <param name="y">position.z</param>
        /// <returns></returns>
        public List<Vector2Int> GetAllNodesRealPos(float x, float y)
        {
            //对x、y进行初始点处理
            List<Vector2Int> allnodes = new List<Vector2Int>();
            switch (rotateType)
            {
                case TileRotateType.Anchor:
                    allnodes = GetAllNodesRealPos_Grid(x, y);
                    break;
                case TileRotateType.Self:
                    allnodes = GetAllNodesRealPos_Point(x, y);
                    break;
            }
            return allnodes;
        }
        private List<Vector2Int> GetAllNodesRealPos_Grid(float x, float y)
        {
            Vector2Int goPos = MapsManager.Instance[LevelKey].GetIndexByWorldPos(new Vector3(x, 0, y));
            List<Vector2Int> allnodes = new List<Vector2Int>();
            foreach (Vector2Int pos in AllNodes)
            {
                Vector2Int newPos = new Vector2Int(pos.x + goPos.x, pos.y + goPos.y);
                allnodes.Add(newPos);
            }
            return allnodes;
        }
        private List<Vector2Int> GetAllNodesRealPos_Point(float x, float y)
        {
            List<Vector2Int> allnodes = new List<Vector2Int>();
            foreach (Vector2Int node in AllNodes)
            {
                Vector3 pos = new Vector3((float)node.x / 2 + x, 0, (float)node.y / 2 + y);
                Vector2Int newNode = MapsManager.Instance[LevelKey].GetIndexByWorldPos(pos);
                allnodes.Add(newNode);
            }
            return allnodes;
        }
    }
}