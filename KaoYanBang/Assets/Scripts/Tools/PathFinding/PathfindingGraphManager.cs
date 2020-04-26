using System.Collections.Generic;
using UnityEngine;
namespace liulaoc.DstarPathFinding
{
    public class PathfindingGraphManager : TMonoSingleton<PathfindingGraphManager>, IInitializable
    {
        Dictionary<string, Node[,]> graphs = new Dictionary<string, Node[,]>();

        public void AddGraph(Map map)
        {
            InitGraph(map.LevelKey, map.MapGrid);
        }

        public void InitGraph(string levelKey, GridType[,] tiles)
        {
            if (graphs.ContainsKey(levelKey)) return;
            var graphSize = new Vector2Int(tiles.GetLength(0), tiles.GetLength(1));
            var graphNodes = new Node[graphSize.x, graphSize.y];
            for (int x = 0; x < graphSize.x; x++)
            {
                for (int y = 0; y < graphSize.y; y++)
                {
                    float c;
                    if (tiles[x, y] == GridType.Road || tiles[x, y] == GridType.None || tiles[x, y] == GridType.Teleporter)
                    {
                        c = 0f;
                    }
                    else
                    {
                        c = float.PositiveInfinity;
                    }
                    //var c = (tiles[x, y] == GridType.Road ||tiles[x,y] == GridType.None) ?0 : float.PositiveInfinity;
                    graphNodes[x, y] = new Node(new Vector2Int(x, y), MapsManager.Instance[levelKey].GetGridCenterWorldPos(x, y), graphSize, c);

                }
            }
            graphs.Add(levelKey, graphNodes);
        }
        public Node[,] GetGraphByIndex(string levelKey)
        {
            return GetGraph(levelKey);
        }

        // TODO: 可用多线程优化
        public Node[,] GetGraph(string levelKey)
        {
            if (!graphs.ContainsKey(levelKey))
            {
                var graph = graphs[levelKey];

                var graphSize = new Vector2Int(graph.GetLength(0), graph.GetLength(1));
                var graphNodes = new Node[graphSize.x, graphSize.y];

                for (int x = 0; x < graphSize.x; x++)
                {
                    for (int y = 0; y < graphSize.y; y++)
                    {
                        var isRoad = true;
                        for (int i = 0; i < graphSize.x; i++)
                        {
                            for (int j = 0; j < graphSize.y; j++)
                            {
                                if (float.IsPositiveInfinity(graph[x + i, y + j].Cost))
                                {
                                    isRoad = false;
                                    break;
                                }
                            }
                            if (!isRoad) break;
                        }
                        var c = isRoad ? 0 : float.PositiveInfinity;

                        var position = graph[x, y].Position;
                        //if (gridSize % 2 == 0)
                        //{
                        //    position = (graph[x, y].Position + graph[x + gridSize - 1, y + gridSize - 1].Position) / 2;
                        //}
                        //else
                        //{
                        //    position = graph[x + gridSize / 2, y + gridSize / 2].Position;
                        //}

                        graphNodes[x, y] = new Node(new Vector2Int(x, y), position, graphSize, c);
                    }
                }
                graphs.Add(levelKey, graphNodes);
            }
            return graphs[levelKey];
        }

        public void Init()
        {
            throw new System.NotImplementedException();
        }
    }
}