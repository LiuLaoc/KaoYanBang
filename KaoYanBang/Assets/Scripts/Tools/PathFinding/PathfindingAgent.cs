using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace liulaoc.DstarPathFinding
{
    public class PathfindingAgent : IDisposable
    {
        Node[,] nodes;
        State[,] states = null;

        PriorityQueue<State> openList;
        float km;
        State start;
        State goal;
        State last;

        public PathfindingAgent(string levelKey)
        {
            nodes = PathfindingGraphManager.Instance.GetGraphByIndex(levelKey);
            openList = new PriorityQueue<State>(nodes.GetLength(0) * nodes.GetLength(1));
        }

        public void ResetMap(string levelKey)
        {
            nodes = PathfindingGraphManager.Instance.GetGraphByIndex(levelKey);
            openList = new PriorityQueue<State>(nodes.GetLength(0) * nodes.GetLength(1));
        }

        public void Initialize(Vector2Int startPos, Vector2Int goalPos)
        {
            openList.Clear();
            km = 0;
            InitStates();

            //var sp = GetActualMapPos(startPos);
            //var gp = GetActualMapPos(goalPos);
            var sp = startPos;
            var gp = goalPos;

            start = states[sp.x, sp.y];
            last = start;

            goal = states[gp.x, gp.y];
            goal.rhs = 0;
            goal.k = (Heuristic(start, goal), 0);
            openList.Push(goal);
            Print();
        }

        //public void SetVirtualObstacle(List<Vector2Int> pos)
        //{
        //    foreach (var p in pos)
        //    {
        //        states[p.x, p.y].c = 100;
        //    }
        //}

        public Vector3? GetNextPosition()
        {
            //Print();
            var min = float.PositiveInfinity;
            var minCost = float.PositiveInfinity;
            var results = new List<State>();

            foreach (var s in GetSucc(start))
            {
                var c = Cost(start, s);
                var g = c + s.g;
                if (g <= min && !float.IsPositiveInfinity(g))
                {
                    if (g < min || c < minCost)
                    {
                        results.Clear();
                        min = g;
                        minCost = c;
                    }

                    if (c > minCost) continue;

                    results.Add(s);
                }
            }

            if (results.Count == 0) return null;
            start = results[UnityEngine.Random.Range(0, results.Count)];
            return nodes[start.MapPos.x, start.MapPos.y].Position;
        }

        public void ComputeShortestPath()
        {
            while (openList.Peek() != null && State.CompareKey(openList.Peek().k, CalculateKey(start)) < 0 || start.rhs > start.g)
            {
                // 获取优先队列第一个元素但不出队
                var u = openList.Peek();
                // 记录旧key
                var k_old = u.k;
                // 计算新key
                var k_new = CalculateKey(u);

                if (State.CompareKey(k_old, k_new) < 0)
                {
                    // 更新优先队列第一个元素的key
                    u.k = k_new;
                    openList.Update(0);
                }
                // 过度一致，表示通过该节点的代价变小，展开该节点，计算该节点对前继节点的影响
                else if (u.g > u.rhs)
                {
                    u.g = u.rhs;
                    openList.Pop();
                    foreach (var s in GetPred(u))
                    {
                        if (!s.Equals(goal) && !float.IsInfinity(Cost(s)))
                        {
                            s.rhs = Mathf.Min(s.rhs, Cost(s, u) + u.g);
                            UpdateState(s);
                        }
                    }
                }
                // 不一致，表示通过该节点需要更大代价
                else
                {
                    var g_old = u.g;
                    u.g = float.PositiveInfinity;
                    var list = new List<State>(GetPred(u));
                    list.Add(u);
                    foreach (var s in list)
                    {
                        if (s.rhs == Cost(s, u) + g_old && !s.Equals(goal))
                        {
                            s.rhs = GetMinSuccValue(s);
                        }
                        UpdateState(s);
                    }
                }
            }
        }

        public void RefreshPath(List<(Vector2Int pos, float newCost)> changedStates)
        {
            km += Heuristic(last, start);
            last = start;

            // TODO: 应该可以优化
            foreach (var state in changedStates)
            {
                var v = states[state.pos.x, state.pos.y];
                v.c = state.newCost;
                foreach (var u in GetPred(v))
                {
                    if (u.Equals(goal) || float.IsInfinity(Cost(u))) continue;

                    u.rhs = GetMinSuccValue(u);
                    UpdateState(u);
                }
            }
            ComputeShortestPath();
        }

        //List<Vector2Int> GetActualMapPos(Vector2Int pos)
        //{
        //    var results = new List<Vector2Int>();
        //    for (var x = 0; x < gridSize; x++)
        //    {
        //        for (var y = 0; y < gridSize; y++)
        //        {
        //            var val = new Vector2Int(pos.x - x, pos.y - y);
        //            if (val.x >= 0 && val.y >= 0 && val.x < nodes.GetLength(0) && val.y < nodes.GetLength(1))
        //            {
        //                results.Add(val);
        //            }
        //        }
        //    }
        //    return results;
        //}

        void InitStates()
        {
            if (states == null)
            {
                states = new State[nodes.GetLength(0), nodes.GetLength(1)];
                for (var x = 0; x < nodes.GetLength(0); x++)
                {
                    for (var y = 0; y < nodes.GetLength(1); y++)
                    {
                        states[x, y] = new State(new Vector2Int(x, y));
                    }
                }
            }
            else
            {
                foreach (var state in states)
                {
                    state.Reset();
                }
            }
        }

        void UpdateState(State s)
        {
            if (s.g != s.rhs)
            {
                s.k = CalculateKey(s);
                var index = openList.IndexOf(s);
                if (index == -1)
                {
                    openList.Push(s);
                }
                else
                {
                    openList.Update(index);
                }
            }
            else if (s.g == s.rhs)
            {
                openList.Remove(s);
            }
        }

        (float, float) CalculateKey(State s)
        {
            var min = Mathf.Min(s.g, s.rhs);
            return (min + Heuristic(s, start) + km, min);
        }

        float Heuristic(State a, State b)
        {
            return Mathf.Abs(a.MapPos.x - b.MapPos.x) + Mathf.Abs(a.MapPos.y - b.MapPos.y);
        }

        // 计算a到b的cost
        float Cost(State a, State b)
        {
            if (a.Equals(b)) return 0;

            return Cost(b) + Heuristic(a, b);
        }

        float Cost(State s)
        {
            // cost存在两个地方，一个是地图固有的cost（node.Cost），一个是可动态修改的cost（state.c）
            // 当state.c为NaN，使用node.Cost，否则使用state.c
            return float.IsNaN(s.c) ? nodes[s.MapPos.x, s.MapPos.y].Cost : s.c;
        }

        // 获取前继节点
        State[] GetPred(State s)
        {
            return GetNeighbor(s);
        }

        // 获取后继节点
        State[] GetSucc(State s)
        {
            return GetNeighbor(s);
        }

        float GetMinSuccValue(State s)
        {
            var min = float.PositiveInfinity;
            foreach (var s_ in GetSucc(s))
            {
                var val = Cost(s, s_) + s_.g;
                if (val < min)
                    min = val;
            }
            return min;
        }

        State[] GetNeighbor(State s)
        {
            if (s.neighbors == null)
            {
                s.neighbors = new List<State>();
                foreach (var neighbor in nodes[s.MapPos.x, s.MapPos.y].Neighbors)
                {
                    var pos = s.MapPos + neighbor;
                    s.neighbors.Add(states[pos.x, pos.y]);
                }
            }
            return s.neighbors.ToArray();
        }

        public void Print()
        {
            string log = "";
            for (int x = 0; x < states.GetLength(0); x++)
            {
                for (int y = 0; y < states.GetLength(1); y++)
                {
                    log += string.Format("{0},{1}\t",
                    float.IsInfinity(states[x, y].g) ? -1 : states[x, y].g,
                    float.IsInfinity(states[x, y].rhs) ? -1 : states[x, y].rhs);
                }
                log += "\n";
            }
            //Debug.Log(log);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
