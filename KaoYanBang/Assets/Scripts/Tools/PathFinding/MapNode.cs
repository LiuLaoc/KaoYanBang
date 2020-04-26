using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace liulaoc.DstarPathFinding
{
    public class Node
    {
        public Vector3 Position { get; private set; }
        public float Cost { get; private set; }
        public List<Vector2Int> Neighbors { get; private set; }

        public Node(Vector2Int mapPos, Vector3 position, Vector2Int mapSize, float cost)
        {
            Position = position;
            Cost = cost;
            Neighbors = new List<Vector2Int>(4);

            if (mapPos.x > 0) { Neighbors.Add(Vector2Int.left); }
            if (mapPos.y > 0) { Neighbors.Add(Vector2Int.down); }
            if (mapPos.x < mapSize.x - 1) { Neighbors.Add(Vector2Int.right); }
            if (mapPos.y < mapSize.y - 1) { Neighbors.Add(Vector2Int.up); }
        }
    }

    public class State : IComparable
    {
        // 当前节点到终点的代价，从优先级队列中出列时与rhs比较并重新设置一致性
        public float g;
        // 意义与g类似，但任何地形的变化先影响rhs，g只在出队时改变
        public float rhs;
        // 起点到当前节点的启发值
        public float h;
        // 当前节点的cost
        public float c;
        public (float, float) k;
        public List<State> neighbors = null;

        public Vector2Int MapPos { get; private set; }

        public State(Vector2Int mapPos)
        {
            MapPos = mapPos;
            Reset();
        }

        public void Reset()
        {
            g = rhs = float.PositiveInfinity;
            c = float.NaN;
        }

        public int CompareTo(object obj)
        {
            var s = obj as State;

            return CompareKey(k, s.k);
        }

        public static int CompareKey((float, float) k1, (float, float) k2)
        {
            if (k1.Item1 < k2.Item1) return -1;
            else if (k1.Item1 > k2.Item1) return 1;

            if (k1.Item2 < k2.Item2) return -1;
            else if (k1.Item2 > k2.Item2) return 1;

            return 0;
        }
    }

}
