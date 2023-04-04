using System;
using System.Collections.Generic;
using System.Linq;
using Toolbox.Attributes;
using Toolbox.MethodExtensions;
using UnityEngine;
using UnityEngine.Events;

namespace Runtime.Grid
{
    [Serializable]
    public class MyGrid : MonoBehaviour
    {
        [SerializeField] private Color gridColor = Color.blue;
        [SerializeField] private Vector3 pivotPoint = new Vector3(0, 0, 0);

        [SerializeField, Min(0)] private int width = 0;
        [SerializeField, Min(0)] private int height = 0;
        [SerializeField, Min(0)] private int depth = 0;

        public List<GridNode> nodes = new List<GridNode>();
        public UnityEvent onResetGrid = new UnityEvent();
        public UnityEvent onGridChanged = new UnityEvent();
        public UnityEvent<GridNode> onGridChangedWithNode = new UnityEvent<GridNode>();

        public int Width => width;
        public int Height => height;
        public int Depth => depth;

        public Vector3 PivotPoint => pivotPoint;

        private void Awake()
        {
            if (!nodes.IsEmpty() && nodes != null) return;
            GenerateGrid();
        }

        /// <summary>
        /// resets the grid and creates new one 
        /// </summary>
        /// <returns></returns>
        [Button(Mode = ButtonMode.EnabledInPlayMode)]
        public MyGrid GenerateGrid()
        {
            ResetGrid();
            GridHelper.Grid3dLoop(width, height, depth, (index, gridPos) =>
            {
                GridNode node = new GridNode(index, gridPos.x, gridPos.y, gridPos.z, false);
                nodes.Add(node);
            });
            return this;
        }

        /// <summary>
        /// Resets the grid by clearing everything to default values.
        /// </summary>
        /// <returns></returns>
        [Button(Mode = ButtonMode.EnabledInPlayMode)]
        public MyGrid ResetGrid()
        {
            onResetGrid.Invoke();
            nodes.Clear();
            return this;
        }

        /// <summary>
        /// Draw a blue grid in the scene view. 
        /// </summary>
        private void OnDrawGizmos()
        {
            Gizmos.color = gridColor;
            GridHelper.Grid3dLoop(width, height, depth, (index, gridPos) =>
            {
                Vector3 size = Vector3.one;
                if (width == 1) size.x = .1f;
                if (height == 1) size.y = .1f;
                if (depth == 1) size.z = .1f;
                Gizmos.DrawWireCube(gridPos + pivotPoint, size);
            });

            //draw blocked nodes in red
            if (nodes.IsEmpty()) return;
            Gizmos.color = Color.cyan;
            foreach (var node in nodes.Where(node => node.IsBlocked))
            {
                Gizmos.DrawCube(node.GridPosition + PivotPoint, Vector3.one);
            }
        }

        /// <summary>
        /// Gets the node at the given list index.
        /// </summary>
        /// <param name="index">list index</param>
        /// <returns></returns>
        public GridNode GetNodeByIndex(int index)
        {
            return nodes[index];
        }

        /// <summary>
        /// Gets the index of the node at the given grid position in the grid. as out parameter.
        /// returns if successful or not.
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool TryGetIndexByPosition(Vector3Int pos, out int index)
        {
            var node = nodes.FirstOrDefault(node => node.GridPosition == pos);
            index = node?.Index ?? 0;
            return node != null;
        }

        /// <summary>
        /// Gets the node at the given grid position.
        /// </summary>
        /// <param name="position">the grid position</param>
        /// <returns></returns>
        public GridNode GetNodeByPosition(Vector3Int position)
        {
            return nodes.FirstOrDefault(node => node.GridPosition == position);
        }

        /// <summary>
        /// Gets the node from given world position by converting it to grid position. (rounding)
        /// </summary>
        /// <param name="position">world position</param>
        /// <returns></returns>
        public GridNode GetNodeFromWorldPosition(Vector3 position)
        {
            var x = Mathf.RoundToInt(position.x - pivotPoint.x);
            var z = Mathf.RoundToInt(position.z - pivotPoint.z);

            return GetNodeByPosition(new Vector3Int(x, 0, z));
        }

        /// <summary>
        /// Blocks node by position
        /// by getting the index and calling BlockNodeByIndex method<br></br><br></br>
        /// [dev tip] this is so that when we need to change something we only need to change on method
        /// </summary>
        /// <param name="position">the world position</param>
        public void BlockNodeByPosition(Vector3Int position)
        {
            var node = nodes.FirstOrDefault(node => node.GridPosition == position);
            if (node == null) return;
            BlockNodeByIndex(node.Index);
            onGridChanged.Invoke();
            onGridChangedWithNode.Invoke(node);
        }

        /// <summary>
        /// Blocks node by index
        /// </summary>
        /// <param name="index">list index</param>
        [Button]
        public void BlockNodeByIndex(int index)
        {
            nodes[index]?.SetBlocked(true);
            onGridChanged.Invoke();
        }
    }
}