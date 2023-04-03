using System;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

namespace Runtime.Grid.GridPathFinding
{
    [SerializeField]
    public class PathFinding
    {
        private const int _moveStraightCost = 10;
        private const int _moveDiagonalCost = 14;

        private Grid _grid;
        private List<GridNode> _openList;
        private List<GridNode> _closedList;
        
        public PathFinding(Grid grid)
        {
            _grid = grid;
        }

        public List<GridNode> FindPath(Vector3Int currentGridPosition, Vector3Int gotoGridPosition)
        {
            GridNode startNode = _grid.GetNodeByPosition(currentGridPosition);
            GridNode endNode = _grid.GetNodeByPosition(gotoGridPosition);

            if (startNode == null || endNode == null)
            {
                Debug.LogWarning("Tried to calculate a path to or from a node that doesn't exist");
                return null;
            }

            _openList = new List<GridNode> { startNode };
            _closedList = new List<GridNode>();

            for (int gridX = 0; gridX < _grid.Width; gridX++)
            {
                for (int gridZ = 0; gridZ < _grid.Height; gridZ++)
                {
                    GridNode gridNode = _grid.GetNodeByPosition(new Vector3Int(gridX, 0, gridZ));
                    if (gridNode is null) return null;
                    gridNode.gCost = int.MaxValue;
                    gridNode.CalculateFCost();
                    gridNode.cameFromNode = null;
                }
            }

            startNode.gCost = 0;
            startNode.hCost = CalculateDistanceCost(startNode, endNode);
            startNode.CalculateFCost();

            while (_openList.Count > 0)
            {
                GridNode currentNode = GetLowestFCostNode(_openList);

                if (currentNode == endNode)
                {
                    return CalculatePath(endNode);
                }

                _openList.Remove(currentNode);
                _closedList.Add(currentNode);

                foreach (GridNode neighbourNode in GetNeighbourNodeList(currentNode))
                {
                    if (_closedList.Contains(neighbourNode)) continue;
                    if (neighbourNode.IsBlocked) continue;

                    int tentativeGCost = currentNode.gCost + CalculateDistanceCost(currentNode, neighbourNode);
                    if (tentativeGCost < neighbourNode.gCost)
                    {
                        neighbourNode.cameFromNode = currentNode;
                        neighbourNode.gCost = tentativeGCost;
                        neighbourNode.hCost = CalculateDistanceCost(neighbourNode, endNode);
                        neighbourNode.CalculateFCost();

                        if (!_openList.Contains(neighbourNode))
                        {
                            _openList.Add(neighbourNode);
                        }
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Returns a list of all neighbouring nodes around a specific pathnode in all 8 directions
        /// </summary>
        private List<GridNode> GetNeighbourNodeList(GridNode currentNode)
        {
            List<GridNode> neighbourList = new List<GridNode>();

            if (currentNode.GridPosition.x - 1 >= 0)
            {
                //left
                neighbourList.Add(GetNode(currentNode.GridPosition.x - 1, currentNode.GridPosition.z));
                //left down
                if (currentNode.GridPosition.z - 1 >= 0) neighbourList.Add(GetNode(currentNode.GridPosition.x - 1, currentNode.GridPosition.z - 1));
                //left up
                if (currentNode.GridPosition.z + 1 < _grid.Height) neighbourList.Add(GetNode(currentNode.GridPosition.x - 1, currentNode.GridPosition.z + 1));
            }

            if (currentNode.GridPosition.x + 1 < _grid.Width)
            {
                //right
                neighbourList.Add(GetNode(currentNode.GridPosition.x + 1, currentNode.GridPosition.z));
                //right down
                if (currentNode.GridPosition.z - 1 >= 0) neighbourList.Add(GetNode(currentNode.GridPosition.x + 1, currentNode.GridPosition.z - 1));
                //right up
                if (currentNode.GridPosition.z + 1 < _grid.Height) neighbourList.Add(GetNode(currentNode.GridPosition.x + 1, currentNode.GridPosition.z + 1));
            }

            //bottom
            if (currentNode.GridPosition.z - 1 >= 0) neighbourList.Add(GetNode(currentNode.GridPosition.x, currentNode.GridPosition.z - 1));
            //top
            if (currentNode.GridPosition.z + 1 < _grid.Height) neighbourList.Add(GetNode(currentNode.GridPosition.x, currentNode.GridPosition.z + 1));

            return neighbourList;
        }

        private List<GridNode> CalculatePath(GridNode endNode)
        {
            List<GridNode> path = new List<GridNode>();
            path.Add(endNode);
            GridNode currentNode = endNode;
            while (currentNode.cameFromNode != null)
            {
                path.Add(currentNode.cameFromNode);
                currentNode = currentNode.cameFromNode;
            }

            path.Reverse();

            return path;
        }

        private int CalculateDistanceCost(GridNode a, GridNode b)
        {
            int xDistance = Math.Abs(a.GridPosition.x - b.GridPosition.x);
            int yDistance = Math.Abs(a.GridPosition.y - b.GridPosition.y);
            int remaining = Math.Abs(xDistance - yDistance);
            return _moveDiagonalCost * Mathf.Min(xDistance, yDistance) + _moveStraightCost * remaining;
        }

        private GridNode GetLowestFCostNode(List<GridNode> pathNodeList)
        {
            GridNode lowestFCostNode = pathNodeList[0];
            for (int i = 1; i < pathNodeList.Count; i++)
            {
                if (pathNodeList[i].fCost < lowestFCostNode.fCost)
                {
                    lowestFCostNode = pathNodeList[i];
                }
            }

            return lowestFCostNode;
        }

        private GridNode GetNode(int x, int z)
        {
            return _grid.GetNodeByPosition(new Vector3Int(x, 0, z));
        }

        public void BlockNode(int x, int z, bool blockNode)
        {
            _grid.GetNodeByPosition(new Vector3Int(x, 0,z)).SetBlocked(blockNode);
        }
    }
}