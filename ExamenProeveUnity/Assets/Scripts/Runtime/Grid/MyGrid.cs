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
        [SerializeField] private bool drawGizmos = true;
        [SerializeField] private int debugIndex = -1;
        [SerializeField] private Color gridColor = Color.blue;

        [SerializeField, Tooltip("The nodes that need to be blocked on generate.")]
        private List<int> blockedNodesOnGenerate = new List<int>();

        [SerializeField] private Vector3 pivotPoint = new Vector3(0, 0, 0);
        [SerializeField] private int nodeSize = 2;

        //create a vector3 with a min value of 0
        [SerializeField, Min(0)] private Vector3Int gridSize = new();

        [HideInInspector] public List<GridNode> nodes = new List<GridNode>();
        public UnityEvent onResetGrid = new UnityEvent();
        public UnityEvent onGridChanged = new UnityEvent();
        public UnityEvent<GridNode> onGridChangedWithNode = new UnityEvent<GridNode>();

        public int Width => gridSize.x;
        public int Height => gridSize.y;
        public int Depth => gridSize.z;

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
        [Button]
        public MyGrid GenerateGrid()
        {
            ResetGrid();
            GridHelper.Grid3dLoop(Width, Height, Depth, (index, gridPos) =>
            {
                GridNode node = new GridNode(index, gridPos.x, gridPos.y, gridPos.z,
                    blockedNodesOnGenerate.Contains(index));
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
            if (!drawGizmos) return;

            Gizmos.color = gridColor;
            GridHelper.Grid3dLoop(Width, Height, Depth, (index, gridPos) =>
            {
                Vector3 size = (Vector3.one * nodeSize);
                if (Width == 1) size.x = .1f;
                if (Height == 1) size.y = .1f;
                if (Depth == 1) size.z = .1f;
                Gizmos.DrawWireCube(GetWorldPositionOfNode(gridPos), size);

                if (index == debugIndex || blockedNodesOnGenerate.Contains(index))
                {
                    Gizmos.color = (index == debugIndex) ? Color.magenta : Color.yellow;
                    Gizmos.DrawCube(GetWorldPositionOfNode(gridPos), size);
                    Gizmos.color = gridColor;
                }
            });

            //draw blocked nodes in red
            if (nodes.IsEmpty()) return;
            Gizmos.color = Color.cyan;
            foreach (var node in nodes.Where(node => node.IsBlocked))
            {
                Gizmos.DrawCube(GetWorldPositionOfNode(node.GridPosition), Vector3.one);
            }

            Gizmos.color = Color.blue;
            foreach (var node in nodes.Where(node => node.IsTempBlocked))
            {
                Gizmos.DrawCube(GetWorldPositionOfNode(node.GridPosition), Vector3.one);
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
            var x = Mathf.RoundToInt((position.x - pivotPoint.x) / nodeSize);
            var z = Mathf.RoundToInt((position.z - pivotPoint.z) / nodeSize);

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
            onGridChangedWithNode.Invoke(nodes[index]);
        }

        public Vector3 GetWorldPositionOfNode(Vector3Int gridPos)
        {
            return new Vector3(gridPos.x, gridPos.y, gridPos.z) * nodeSize + pivotPoint;
        }

        /// <summary>
        /// returns a list of all the direct neighbour nodes
        /// with direct we means the nodes that are directly next to the current node (so only one value changed in the vector) 
        /// </summary>
        /// <param name="currentNode"></param>
        /// <returns></returns>
        public List<GridNode> GetDirectNeighbourList(GridNode currentNode)
        {
            List<GridNode> neighbourList = new List<GridNode>();

            neighbourList.AddIfNotNull(GetNodeByPosition(currentNode.GridPosition + new Vector3Int(-1, 0, 0))); //left
            neighbourList.AddIfNotNull(GetNodeByPosition(currentNode.GridPosition + new Vector3Int(1, 0, 0))); //right
            neighbourList.AddIfNotNull(GetNodeByPosition(currentNode.GridPosition + new Vector3Int(0, 0, 1))); //forward
            neighbourList.AddIfNotNull(GetNodeByPosition(currentNode.GridPosition + new Vector3Int(0, 1, 0))); //upward
            neighbourList.AddIfNotNull(
                GetNodeByPosition(currentNode.GridPosition + new Vector3Int(0, 0, -1))); //backward
            neighbourList.AddIfNotNull(
                GetNodeByPosition(currentNode.GridPosition + new Vector3Int(0, -1, 0))); //downward

            return neighbourList;
        }
    }
}