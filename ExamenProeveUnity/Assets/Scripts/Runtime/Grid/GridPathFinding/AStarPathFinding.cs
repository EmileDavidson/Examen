using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Runtime.Enums;
using Runtime.Managers;
using Toolbox.Attributes;
using UnityEngine;

namespace Runtime.Grid.GridPathFinding
{
    /// <summary>
    /// A* pathfinding
    /// Note: its only tested on a 2D grid but should work on 3D as well (untested)
    /// but since we don't need a 3d grid as of now i didn't take the time to test it on 3D since the setup is a bit of work.
    /// </summary>
    public class AStarPathFinding : MonoBehaviour
    {
        [SerializeField] private MyGrid myGrid;

        private const int MoveStraightCost = 10;
        private const int MoveDiagonalCost = 14;
        private PathFindingCost[] _costArray;

        private void Awake()
        {
            myGrid ??= WorldManager.Instance.worldGrid;
        }

        /// <summary>
        /// Find path from node to node (async) so the return value is a callback
        /// </summary>
        /// <param name="fromNode"></param>
        /// <param name="toNode"></param>
        /// <param name="onPathFound"></param>
        /// <param name="onPathNotFound"></param>
        /// <param name="settings"></param>
        /// <param name="grid"></param>
        [Button(Mode = ButtonMode.EnabledInPlayMode)]
        public void FindPath(GridNode fromNode, GridNode toNode, Action<Path> onPathFound, Action onPathNotFound,
            PathFindingSettings settings, MyGrid grid = null)
        {
            settings ??= new PathFindingSettings();
            grid ??= myGrid;
            FindPathAsync(fromNode, toNode, onPathFound, onPathNotFound, grid, settings);
        }

        /// <summary>
        /// Find path from node to node (async) so the return value is a callback
        /// </summary>
        /// <param name="fromNode"></param>
        /// <param name="toNode"></param>
        /// <param name="onPathFound"></param>
        /// <param name="onPathNotFound"></param>
        /// <param name="grid"></param>
        [Button(Mode = ButtonMode.EnabledInPlayMode)]
        public void FindPath(GridNode fromNode, GridNode toNode, Action<Path> onPathFound, Action onPathNotFound)
        {
            var settings = new PathFindingSettings();
            FindPathAsync(fromNode, toNode, onPathFound, onPathNotFound, myGrid, settings);
        }

        /// <summary>
        /// Find path from node to node (async) so the return value is a callback
        /// </summary>
        /// <param name="fromNode"></param>
        /// <param name="toNode"></param>
        /// <param name="onPathFound"></param>
        /// <param name="onPathNotFound"></param>
        /// <param name="grid"></param>
        [Button(Mode = ButtonMode.EnabledInPlayMode)]
        public void FindPath(GridNode fromNode, GridNode toNode, Action<Path> onPathFound, Action onPathNotFound,
            MyGrid grid)
        {
            var settings = new PathFindingSettings();
            grid ??= myGrid;
            FindPathAsync(fromNode, toNode, onPathFound, onPathNotFound, grid, settings);
        }

        /// <summary>
        /// Async path finding
        /// </summary>
        /// <returns></returns>
        private async void FindPathAsync(GridNode currentNodePos, GridNode requestedNodePos, Action<Path> onPathFound,
            Action onPathNotFound, MyGrid grid, PathFindingSettings settings = null)
        {
            settings ??= new PathFindingSettings();

            Task<Path> task = new Task<Path>(() => FindPath(currentNodePos, requestedNodePos, settings, grid));
            task.Start();
            task.Exception?.Handle(e =>
            {
                Debug.Log(e);
                return true;
            });

            var result = await task;
            if (result is null)
            {
                onPathNotFound?.Invoke();
                return;
            }

            onPathFound?.Invoke(result);
        }

        /// <summary>
        /// Path finding algorithm (A*)
        /// </summary>
        /// <returns></returns>
        private Path FindPath([NotNull] GridNode fromNode, [NotNull] GridNode toNode, [NotNull] PathFindingSettings settings, MyGrid grid)
        {
            List<GridNode> openList = new List<GridNode> { fromNode };
            List<GridNode> closedList = new List<GridNode>();

            if (fromNode is null || toNode is null)
            {
                return null;
            }

            var startNodeIndex = fromNode.Index;
            var endNodeIndex = toNode.Index;

            _costArray = new PathFindingCost[grid.GetTotalNodeCount()];

            GridHelper.Grid3dLoop(grid.Width, grid.Height, grid.Depth, (index, _) =>
            {
                _costArray[index] = new PathFindingCost(int.MaxValue, 0, null);
            });

            _costArray[startNodeIndex].Gcost = 0;
            _costArray[startNodeIndex].Hcost = GetDistance(fromNode, toNode);
            _costArray[startNodeIndex].Fcost = _costArray[startNodeIndex].Gcost + _costArray[startNodeIndex].Hcost;

            while (openList.Count > 0)
            {
                GridNode currentNodeInOpenList = GetLowestFCostNode(openList);
                openList.Remove(currentNodeInOpenList);
                closedList.Add(currentNodeInOpenList);

                if (currentNodeInOpenList.Index == endNodeIndex)
                {
                    return CalculatePath(fromNode, toNode);
                }

                //handle settings here
                var neighbourList = grid.GetDirectNeighbourList(currentNodeInOpenList);

                if (!settings.CanBeBlocked) neighbourList.RemoveAll(element => element.IsBlocked);
                if (!settings.CanBeEndPoint)
                    neighbourList.RemoveAll(element => element.IsLocationNode && element.Index != endNodeIndex);
                if (!settings.CanBeTempBlocked) neighbourList.RemoveAll(element => element.IsTempBlocked);

                foreach (GridNode neighbourNode in neighbourList)
                {
                    if (closedList.Contains(neighbourNode)) continue;

                    int tentativeGCost = _costArray[currentNodeInOpenList.Index].Gcost +
                                         GetDistance(currentNodeInOpenList, neighbourNode);
                    if (tentativeGCost >= _costArray[neighbourNode.Index].Gcost) continue;
                    _costArray[neighbourNode.Index].CameFromNode = currentNodeInOpenList;
                    _costArray[neighbourNode.Index].Gcost = tentativeGCost;
                    _costArray[neighbourNode.Index].Hcost = GetDistance(neighbourNode, toNode);
                    _costArray[neighbourNode.Index].Fcost =
                        _costArray[neighbourNode.Index].Gcost + _costArray[neighbourNode.Index].Hcost;

                    if (!openList.Contains(neighbourNode))
                    {
                        openList.Add(neighbourNode);
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Calculates the path from the start node to the end node using the cameFromNode property
        /// and returns the path.
        /// </summary>
        /// <param name="fromNode"></param>
        /// <param name="toNode"></param>
        /// <returns></returns>
        private Path CalculatePath(GridNode fromNode, GridNode toNode)
        {
            Path newPath = new Path(new List<int>(), fromNode, toNode);
            newPath.PathNodes.Add(toNode.Index);
            PathFindingCost currentNode = _costArray[toNode.Index];
            while (currentNode.CameFromNode != null)
            {
                newPath.PathNodes.Add(currentNode.CameFromNode.Index);
                currentNode = _costArray[currentNode.CameFromNode.Index];
            }

            newPath.PathNodes.Reverse();

            return newPath;
        }

        /// <summary>
        /// Get the lowest FCost node from the given list and returns it.
        /// </summary>
        /// <param name="pathNodeList"></param>
        /// <returns></returns>
        private GridNode GetLowestFCostNode(List<GridNode> pathNodeList)
        {
            PathFindingCost lowestFCostNode = _costArray[pathNodeList[0].Index];
            int lowestFCostIndex = 0;
            for (int i = 1; i < pathNodeList.Count; i++)
            {
                if (_costArray[i].Fcost >= lowestFCostNode.Fcost) continue;
                lowestFCostNode = _costArray[i];
                lowestFCostIndex = i;
            }

            return pathNodeList[lowestFCostIndex];
        }


        /// <summary>
        /// Gets the distance between two nodes
        /// </summary>
        /// <param name="nodeA"></param>
        /// <param name="nodeB"></param>
        /// <returns></returns>
        private int GetDistance(GridNode nodeA, GridNode nodeB)
        {
            int dstX = Mathf.Abs(nodeA.GridPosition.x - nodeB.GridPosition.x);
            int dstY = Mathf.Abs(nodeA.GridPosition.y - nodeB.GridPosition.y);
            int dstZ = Mathf.Abs(nodeA.GridPosition.z - nodeB.GridPosition.z);

            return dstX > dstY
                ? MoveDiagonalCost * dstY + MoveStraightCost * (dstX - dstY) + MoveStraightCost * dstZ
                : MoveDiagonalCost * dstX + MoveStraightCost * (dstY - dstX) + MoveStraightCost * dstZ;
        }
    }
}