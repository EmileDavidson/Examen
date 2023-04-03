using System;
using System.Collections.Generic;
using System.Linq;
using Toolbox.MethodExtensions;
using UnityEngine;
using UnityEngine.Events;

namespace Runtime.Grid
{
    [Serializable]
    public class Grid : MonoBehaviour
    {
        [SerializeField] private Vector3 pivotPoint = new Vector3(0,0,0);
        
        [SerializeField, Min(0)] private int width = 0;
        [SerializeField, Min(0)] private int height = 0;
        
        public List<GridNode> nodes = new List<GridNode>();
        public UnityEvent onResetGrid = new UnityEvent();
        
        public int Width => width;
        public int Height => height;

        private void Awake()
        {
            GenerateGrid();
        }

        /// <summary>
        /// resets the grid and creates new one 
        /// </summary>
        /// <returns></returns>
        public Grid GenerateGrid()
        {
            ResetGrid();
            for (int gridX = 0; gridX < height; gridX++)
            {
                for (int gridZ = 0; gridZ < width; gridZ++)
                {
                    int index = gridX + width * gridZ;

                    GridNode node = new GridNode(index, gridX, 0, gridZ);
                    nodes.Add(node);
                }
            }
            return this;
        }
        
        /// <summary>
        /// Resets the grid by clearing everything to default values.
        /// </summary>
        /// <returns></returns>
        public Grid ResetGrid()
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
            Gizmos.color = Color.red;
            for (int gridX = 0; gridX < width; gridX++)
            {
                for (int gridZ = 0; gridZ < height; gridZ++)
                {
                    Gizmos.DrawLine(new Vector3(gridX + pivotPoint.x, 0 + pivotPoint.y, gridZ + pivotPoint.z),
                        new Vector3(gridX + 1 + pivotPoint.x, 0 + pivotPoint.y, gridZ + pivotPoint.z));

                    Gizmos.DrawLine(new Vector3(gridX + pivotPoint.x, 0 + pivotPoint.y, gridZ + pivotPoint.z),
                        new Vector3(gridX + pivotPoint.x, 0 + pivotPoint.y, gridZ + 1 + pivotPoint.z));
                }
            }
            
            Gizmos.DrawLine(pivotPoint + new Vector3(width, 0,0), pivotPoint + new Vector3(width, 0,height));
            Gizmos.DrawLine(pivotPoint + new Vector3(width, 0,height), pivotPoint + new Vector3(0, 0,height));
            
            //draw blocked nodes in red
            if (nodes.IsEmpty()) return;
            Gizmos.color = Color.cyan;
            foreach (var node in nodes.Where(node => node.IsBlocked))
            {
                Gizmos.DrawCube(node.GridPosition + pivotPoint + new Vector3(.5f, 0, -.5f), new Vector3(.4f, .4f, .4f));
            }
        }

        public GridNode GetNodeByIndex(int index)
        {
            return nodes[index];
        }
        
        public GridNode GetNodeByPosition(Vector3Int position)
        {
            return nodes.FirstOrDefault(node => node.GridPosition == position);
        }
        
        public GridNode GetNodeFromWorldPosition(Vector3 position)
        {
            //calculate the node from a vector3 position
            int x = Mathf.RoundToInt(position.x - pivotPoint.x);
            int z = Mathf.RoundToInt(position.z - pivotPoint.z);
            return GetNodeByPosition(new Vector3Int(x, 0, z));
        }

        public void BlockNodeByPosition(Vector3Int position)
        {
            nodes.FirstOrDefault(node => node.GridPosition == position)?.SetBlocked(true);;
        }

        public void BlockNodeByIndex(int index)
        {
            nodes[index]?.SetBlocked(true);
        }
    }
}
