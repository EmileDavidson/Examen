using System;
using System.Collections.Generic;
using System.Linq;
using Toolbox.Attributes;
using Toolbox.MethodExtensions;
using UnityEngine;

namespace Runtime.Grid.GridPathFinding
{
    public class FixedPath : MonoBehaviour
    {
        [SerializeField] private Color gizmosColor = Color.green;
        [SerializeField] private bool drawGizmos = true;
        [SerializeField] private MyGrid grid;
    
        private Path _path;
        public List<int> pathNodeIndexes = new List<int>();

        [Button]
        private void Start()
        {
            List<GridNode> pathNodes = grid.GetNodesFromIndexes(pathNodeIndexes);
            _path = new Path(pathNodes, pathNodes[0], pathNodes.Last());
        }

        private void OnDrawGizmos()
        {
            if (!drawGizmos) return;

            GridHelper.Grid3dLoop(grid.Width, grid.Height, grid.Depth, (index, gridPos) =>
            {
                if (!pathNodeIndexes.Contains(index)) return;
                Gizmos.color = gizmosColor;
                Gizmos.DrawSphere(grid.GetWorldPositionOfNode(gridPos), 0.5f);
            });
        }

        public Path Path => _path;
    }
}
