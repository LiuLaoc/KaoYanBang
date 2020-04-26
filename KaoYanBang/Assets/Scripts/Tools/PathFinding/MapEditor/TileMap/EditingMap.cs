using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace liulaoc.DstarPathFinding.Editor
{
    public abstract class EditingMap : MonoBehaviour
    {
        protected enum EditType
        {
            None,
            Create,
            Edit
        }
        protected EditType editType;
        protected Transform startPoint;
        protected Transform endPoint;
        protected Map map = null;
        protected BoxCollider box;
        protected void Start()
        {
            box = GameObject.CreatePrimitive(PrimitiveType.Cube).GetComponent<BoxCollider>();
            Debug.Log("按E设置左键橡皮擦，按P设置右键传送点，按R设置左键便捷，按T设置左键建筑");
            Init();
        }
        protected void Update()
        {
            if (editType == EditType.Create)
            {
                map.StartPos = startPoint.position;
                map.EndPos = endPoint.position;
            }
            if (editType == EditType.Edit)
            {
                EditOperationUpdate();
            }
        }
        private void OnDrawGizmos()
        {
            if (map == null || !Application.isPlaying)
                return;
            int width = map.Width;
            int height = map.Height;
            float y = map.StartPos.y;
            Gizmos.color = Color.black;
            Vector3 leftPoint = map.StartPos;
            Vector3 rightPoint = new Vector3(map.StartPos.x + map.Width, map.StartPos.y, map.StartPos.z);
            while (leftPoint.z <= map.StartPos.z + map.Height)
            {
                Gizmos.DrawLine(leftPoint, rightPoint);
                leftPoint.z += map.GridSize;
                rightPoint.z += map.GridSize;
            }
            Vector3 upPoint = new Vector3(map.StartPos.x, map.StartPos.y, map.StartPos.z + map.Height);
            Vector3 downPoint = map.StartPos;
            while (upPoint.x <= map.StartPos.x + map.Width)
            {
                Gizmos.DrawLine(upPoint, downPoint);
                upPoint.x += map.GridSize;
                downPoint.x += map.GridSize;
            }
            if (width < 0 || height < 0)
            {
                Debug.Log("起点应放在左下角，终点放在左上角");
                return;
            }
            if (map.MapGrid.GetLength(0) != width || map.MapGrid.GetLength(1) != height)
            {
                map.MapGrid = new GridType[width, height];
            }
            if (editType != EditType.None) ShowGrids();
        }
        private void ShowGrids()
        {
            float y = map.StartPos.y;
            for (int i = 0; i < map.Width; i++)
            {
                for (int j = 0; j < map.Height; j++)
                {
                    float ri = map.StartPos.x + i;
                    float rj = map.StartPos.z + j;
                    switch (map.MapGrid[i, j])
                    {
                        case GridType.None:
                            break;
                        case GridType.Boundary:
                            Gizmos.color = Color.red;
                            Gizmos.DrawLine(new Vector3(ri, y, rj), new Vector3(ri + 1, y, rj + 1));
                            Gizmos.DrawLine(new Vector3(ri, y, rj + 1), new Vector3(ri + 1, y, rj));
                            break;
                        case GridType.Teleporter:
                            Gizmos.color = Color.blue;
                            Gizmos.DrawLine(new Vector3(ri, y, rj), new Vector3(ri + 1, y, rj + 1));
                            Gizmos.DrawLine(new Vector3(ri, y, rj + 1), new Vector3(ri + 1, y, rj));
                            break;
                        case GridType.Building:
                            Gizmos.color = Color.yellow;
                            Gizmos.DrawLine(new Vector3(ri, y, rj), new Vector3(ri + 1, y, rj + 1));
                            Gizmos.DrawLine(new Vector3(ri, y, rj + 1), new Vector3(ri + 1, y, rj));
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        #region 回调
        protected abstract void InitMap(Transform start, Transform end);
        protected abstract void LoadMap();
        protected abstract void EditOperationUpdate();
        protected abstract void Init();
        #endregion
    }
}