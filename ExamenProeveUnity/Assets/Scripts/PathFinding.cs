using System;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding
{
    private const int _moveStraightCost = 10;
    private const int _moveDiagonalCost = 14;
    
    private Grid<PathNode> _grid;
    private List<PathNode> _openList;
    private List<PathNode> _closedList;

    public PathFinding(int width, int height)
    {
        _grid = new Grid<PathNode>(width, height, 1f, Vector3.zero, (x, y) => new PathNode(x, y));
    }

    public List<PathNode> FindPath(int startX, int startY, int endX, int endY)
    {
        PathNode startNode = _grid.GetGridObject(startX, startY);
        PathNode endNode = _grid.GetGridObject(endX, endY);

        _openList = new List<PathNode> {startNode};
        _closedList = new List<PathNode>();

        for (int x = 0; x < _grid.Width; x++)
        {
            for (int y = 0; y < _grid.Height; y++)
            {
                PathNode pathNode = _grid.GetGridObject(x, y);
                pathNode.gCost = int.MaxValue;
                pathNode.CalculateFCost();
                pathNode.cameFromNode = null;
            }
        }

        startNode.gCost = 0;
        startNode.hCost = CalculateDistanceCost(startNode, endNode);
        startNode.CalculateFCost();

        while (_openList.Count > 0)
        {
            PathNode currentNode = GetLowestFCostNode(_openList);
            
            if (currentNode == endNode)
            {
                return CalculatePath(endNode);
            }

            _openList.Remove(currentNode);
            _closedList.Add(currentNode);

            foreach (PathNode neighbourNode in GetNeighbourNodeList(currentNode))
            {
                if (_closedList.Contains(neighbourNode)) continue;
                if (neighbourNode.IsBlocked()) continue;

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
    private List<PathNode> GetNeighbourNodeList(PathNode currentNode)
    {
        List<PathNode> NeighbourList = new List<PathNode>();
        
        if (currentNode.X - 1 >= 0)
        {
            //left
            NeighbourList.Add(GetNode(currentNode.X - 1, currentNode.Y));
            //left down
            if (currentNode.Y - 1 >= 0) NeighbourList.Add(GetNode(currentNode.X - 1, currentNode.Y - 1));
            //left up
            if (currentNode.Y + 1 < _grid.Height) NeighbourList.Add(GetNode(currentNode.X - 1, currentNode.Y + 1));
        }
        if (currentNode.X + 1 < _grid.Width)
        {
            //right
            NeighbourList.Add(GetNode(currentNode.X + 1, currentNode.Y));
            //right down
            if (currentNode.Y - 1 >= 0) NeighbourList.Add(GetNode(currentNode.X + 1, currentNode.Y - 1));
            //right up
            if (currentNode.Y + 1 < _grid.Height) NeighbourList.Add(GetNode(currentNode.X + 1, currentNode.Y + 1));
        }
        //bottom
        if (currentNode.Y - 1 >= 0) NeighbourList.Add(GetNode(currentNode.X, currentNode.Y - 1));
        //top
        if (currentNode.Y + 1 < _grid.Height) NeighbourList.Add(GetNode(currentNode.X, currentNode.Y + 1));
        
        return NeighbourList;
    }

    private List<PathNode> CalculatePath(PathNode endNode)
    {
        List<PathNode> path = new List<PathNode>();
        path.Add(endNode);
        PathNode currentNode = endNode;
        while (currentNode.cameFromNode != null)
        {
            path.Add(currentNode.cameFromNode);
            currentNode = currentNode.cameFromNode;
        }
        path.Reverse();
        
        return path;
    }

    private int CalculateDistanceCost(PathNode a, PathNode b)
    {
        int xDistance = Math.Abs(a.X - b.X);
        int yDistance = Math.Abs(a.Y - b.Y);
        int remaining = Math.Abs(xDistance - yDistance);
        return _moveDiagonalCost * Mathf.Min(xDistance, yDistance) + _moveStraightCost * remaining;
    }

    private PathNode GetLowestFCostNode(List<PathNode> pathNodeList)
    {
        PathNode lowestFCostNode = pathNodeList[0];
        for (int i = 1; i < pathNodeList.Count; i++)
        {
            if (pathNodeList[i].fCost < lowestFCostNode.fCost)
            {
                lowestFCostNode = pathNodeList[i];
            }
        }
        return lowestFCostNode;
    }

    private PathNode GetNode(int x, int y)
    {
        return _grid.GetGridObject(x, y);
    }

    public void BlockNode(int x, int y, bool blockNode)
    {
        _grid.GetGridObject(x, y).SetBlocked(blockNode);
    }
}
