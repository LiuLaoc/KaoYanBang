using UnityEngine;
using UnityEditor;
using UnityEditorClass = UnityEditor.Editor;
namespace liulaoc.DstarPathFinding.Editor.TileMap
{
    /// <summary>
    /// 编辑视图类
    /// </summary>
    [CustomEditor(typeof(EditTileMapsMgr)), CanEditMultipleObjects]
    public class EnemyRoadEditor : UnityEditorClass
    {
        GUILayoutOption[] style = new[] { GUILayout.Height(30), GUILayout.Width(100) };
        GUILayoutOption[] textStyle = new[] { GUILayout.Height(20), GUILayout.Width(30) };
        private string chapterStr;
        private string levelStr;
        private Vector2 scrollPosition;
        private static MapData map = null;
        private bool isSelectRoad = false;
        private int gridId = -1;
        public void Awake()
        {
            TileMapExtension.Init();
            chapterStr = TileMapExtension.chapter.ToString();
            levelStr = TileMapExtension.level.ToString();

        }
        private void OnSceneGUI()
        {
            Handles.BeginGUI();
            GUILayout.BeginVertical("道路编辑器", "window", new[] { GUILayout.Height(500), GUILayout.Width(120) });
            if (GUILayout.Button("保存数据"))
            {
                var str = TileMapExtension.maps.SaveMap();
                if (str != null)
                { 
                    Debug.Log("保存地图信息：" + str); 
                }
                else
                {
                    Debug.Log("地图信息保存失败");
                }
            }
            GUILayout.BeginHorizontal("关卡选择器", "window", new[] { GUILayout.Height(50), GUILayout.Width(120) });
            //关卡选择
            GUILayout.Label("章节", textStyle);
            chapterStr = GUILayout.TextField(chapterStr, textStyle);
            GUILayout.Label("关卡", textStyle);
            levelStr = GUILayout.TextField(levelStr, textStyle);
            if (GUILayout.Button("选择关卡"))
            {
                DataInit();
                TileMapExtension.chapter = int.Parse(chapterStr);
                TileMapExtension.level = int.Parse(levelStr);
                map = TileMapExtension.maps.SelectMap(TileMapExtension.chapter, TileMapExtension.level);
                TileMapExtension.InstitateTileMap();
            }
            GUILayout.EndHorizontal();
            //列出该关卡的所有道路
            GUILayout.BeginVertical("道路选择器", "window", new[] { GUILayout.Height(500), GUILayout.Width(120) });
            if (GUILayout.Button("新建道路"))
            {
                map.AddRoad();
            }
            if (GUILayout.Button("删除"))
            {
                map.RemoveRoad();
            }
            if (map != null)
            {
                scrollPosition = GUILayout.BeginScrollView(scrollPosition, true, true, new[] { GUILayout.Height(150), GUILayout.Width(200) });
                string[] array = new string[map.Roads.Keys.Count];
                for (int i = 0; i < array.Length; i++)
                {
                    array[i] = (i + 1).ToString();
                }
                //点击Toggle选择
                gridId = GUILayout.SelectionGrid(gridId, array, 4);
                GUILayout.EndScrollView();
                if (gridId != -1)
                {
                    GUILayout.BeginVertical("道路节点编辑器", "window", new[] { GUILayout.Height(500), GUILayout.Width(120) });
                    if (GUILayout.Button("添加道路节点"))
                    {
                        map.AddRoadPoint(gridId + 1, Vector3.zero);
                    }
                    if (GUILayout.Button("删除道路节点"))
                    {
                        map.RemoveRoadPoint(gridId + 1);
                    }
                    GUILayout.EndVertical();
                }
            }
            GUILayout.EndVertical();
            GUILayout.EndVertical();
            if (map != null && gridId != -1)
            {
                //显示物体
                var list = map.GetRoadPointList(gridId + 1);
                for (int i = 0; i < list.Count; i++)
                {
                    var position = list[i];
                    //显示路径点
                    Handles.color = Color.red;
                    Handles.Label(position + new Vector3(0, 1, 0), i.ToString());
                    //移动路径
                    var size = HandleUtility.GetHandleSize(position) * 0.1f;
                    var handlePos = Handles.Slider2D(
                        position, Vector3.forward,
                        Vector3.up, Vector3.right,
                        size, Handles.CubeHandleCap, Vector2.one);
                    list[i] = GetGridCenter(handlePos);
                    if (i > 0)
                    {
                        Handles.DrawLine(list[i - 1], list[i]);
                    }
                }
            }
            Handles.EndGUI();
        }
        private void OnDestroy()
        {

        }
        private void DataInit()
        {
            map = null;
            gridId = -1;
        }
        private Vector3 GetGridCenter(Vector3 pos)
        {
            var x = (int)pos.x + 0.5f;
            var y = (int)pos.y + 0.5f;
            var z = 0f;
            return new Vector3(x, y, z);
        }
    }

    public class PointEditor : UnityEditorClass
    {

    }

}