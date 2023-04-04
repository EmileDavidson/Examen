using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Toolbox.Attributes;
using Toolbox.MethodExtensions;
using UnityEngine;
using UnityEngine.Events;

namespace Runtime.Grid.GridPathFinding
{
    /// <summary>
    /// A* pathfinding
    /// Note: its only tested on a 2D grid but should work on 3D as well (untested)
    /// but since we don't need a 3d grid as of now i didn't take the time to test it on 3D since the setup is a bit of work.
    /// </summary>
    public class PathFinding : MonoBehaviour
    {
        [SerializeField] private bool showGizmos = true;
        [SerializeField] private MyGrid myGrid;

        public UnityEvent<List<GridNode>> onPathFound = new();
        public UnityEvent onPathNotFound = new();

        private GridNode _currentNode;
        private GridNode _requestedNode;

        private const int MoveStraightCost = 10;
        private const int MoveDiagonalCost = 14;

        private List<GridNode> _openList;
        private List<GridNode> _closedList;
        private PathFindingCost[] _costArray;

        private bool _needsPath;
        private bool _isFindingPath;
        
        /// <summary>
        /// OnDisable is called when the script instance is being disabled.
        /// </summary>
        private void OnDisable()
        {
            myGrid.onGridChangedWithNode.RemoveListener(GridChangedUpdate);
        }

        /// <summary>
        /// OnEnable is called when the script instance is enabled
        /// </summary>
        private void OnEnable()
        {
            myGrid.onGridChangedWithNode.AddListener(GridChangedUpdate);
        }

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        private void Awake()
        {
            _openList = new List<GridNode>();
            _closedList = new List<GridNode>();
            Path = new List<GridNode>();
            
            onPathFound.AddListener((_) => { _isFindingPath = false;});
            onPathNotFound.AddListener(() => { _isFindingPath = false;});
        }

        /// <summary>
        /// Calculates the path from the current node to the requested node
        /// and subscribes to the grid changed event to update on path change.
        /// </summary>
        /// <param name="currentNode"></param>
        /// <param name="requestedNode"></param>
        [Button(Mode = ButtonMode.EnabledInPlayMode)]
        public void StartPathfinding(GridNode currentNode, GridNode requestedNode)
        {
            myGrid.onGridChangedWithNode.AddListener(GridChangedUpdate);
            _needsPath = true;
            _currentNode = currentNode;
            _requestedNode = requestedNode;
            FindPathAsync(currentNode.GridPosition, requestedNode.GridPosition, (_) => { });
        }

        /// <summary>
        /// Stops the pathfinding from updating on changes and clears the path
        /// </summary>
        [Button(Mode = ButtonMode.EnabledInPlayMode)]
        public void EndPathFinding()
        {
            myGrid.onGridChangedWithNode.RemoveListener(GridChangedUpdate);
            Path.Clear();
            _openList.Clear();
            _closedList.Clear();
            _needsPath = false;
            _isFindingPath = false;
        }

        /// <summary>
        /// Is triggered when the grid changes and updates the path if the changed node is in the path
        /// </summary>
        /// <param name="node"></param>
        private void GridChangedUpdate(GridNode node)
        {
            if (Path != null && !Path.Contains(node)) return;
            if (!_needsPath) return;

            Path = FindPath(_currentNode.GridPosition, _requestedNode.GridPosition);
        }

        /// <summary>
        /// Async path finding
        /// </summary>
        /// <returns></returns>
        private async void FindPathAsync(Vector3Int currentNodePos, Vector3Int requestedNodePos, Action<List<GridNode>> callback)
        {
            if (_isFindingPath)
            {
                return;
            }
            
            Task<List<GridNode>> task = new Task<List<GridNode>>(() => FindPath(currentNodePos, requestedNodePos));
            _isFindingPath = true;
            task.Start();
            task.Exception?.Handle(e =>
            {
                Debug.Log(e);
                return true;
            });
            var result = await task;
            callback.Invoke(result);
        }

        /// <summary>
        /// Path finding algorithm (A*)
        /// </summary>
        /// <returns></returns>
        private List<GridNode> FindPath(Vector3Int currentNodePos, Vector3Int requestedNodePos)
        {
            _isFindingPath = true;
            Path.Clear();

            _currentNode = myGrid.GetNodeByPosition(currentNodePos);
            _requestedNode = myGrid.GetNodeByPosition(requestedNodePos);
            
            if(currentNodePos == requestedNodePos)
            {
                onPathNotFound.Invoke();
                return Path;
            }

            if (_currentNode == null || _requestedNode == null)
            {
                onPathNotFound.Invoke();
                return Path; 
            }

            _openList = new List<GridNode> { _currentNode };
            _closedList = new List<GridNode>();

            _costArray = new PathFindingCost[myGrid.Height * myGrid.Width * myGrid.Depth];

            GridHelper.Grid3dLoop(myGrid.Width, myGrid.Height, myGrid.Depth,
                (index, pos) => { _costArray[index] = new PathFindingCost(index, int.MaxValue, 0, null); });

            _costArray[_currentNode.Index].Gcost = 0;
            _costArray[_currentNode.Index].Hcost = GetDistance(_currentNode, _requestedNode);
            _costArray[_currentNode.Index].Fcost =
                _costArray[_currentNode.Index].Gcost + _costArray[_currentNode.Index].Hcost;

            while (_openList.Count > 0)
            {
                GridNode currentNodeInOpenList = GetLowestFCostNode(_openList);
                _openList.Remove(currentNodeInOpenList);
                _closedList.Add(currentNodeInOpenList);

                if (currentNodeInOpenList.Index == _requestedNode.Index)
                {
                    Path = CalculatePath(_requestedNode);
                    onPathFound.Invoke(Path);
                    return Path;
                }

                foreach (GridNode neighbourNode in GetNeighbourList(currentNodeInOpenList))
                {
                    if (_closedList.Contains(neighbourNode))
                    {
                        continue;
                    }

                    int tentativeGCost = _costArray[currentNodeInOpenList.Index].Gcost +
                                         GetDistance(currentNodeInOpenList, neighbourNode);
                    if (tentativeGCost >= _costArray[neighbourNode.Index].Gcost) continue;
                    _costArray[neighbourNode.Index].CameFromNode = currentNodeInOpenList;
                    _costArray[neighbourNode.Index].Gcost = tentativeGCost;
                    _costArray[neighbourNode.Index].Hcost = GetDistance(neighbourNode, _requestedNode);
                    _costArray[neighbourNode.Index].Fcost = _costArray[neighbourNode.Index].Gcost +
                                                            _costArray[neighbourNode.Index].Hcost;

                    if (!_openList.Contains(neighbourNode))
                    {
                        _openList.Add(neighbourNode);
                    }
                }
            }

            onPathNotFound.Invoke();
            return Path;
        }

        /// <summary>
        /// returns a list of all the neighbour nodes
        /// </summary>
        /// <param name="currentNode"></param>
        /// <returns></returns>
        private List<GridNode> GetNeighbourList(GridNode currentNode)
        {
            List<GridNode> neighbourList = new List<GridNode>();

            //left part
            neighbourList.AddIfNotNull(myGrid.GetNodeByPosition(currentNode.GridPosition + new Vector3Int(-1, 0, 0)));
            neighbourList.AddIfNotNull(myGrid.GetNodeByPosition(currentNode.GridPosition + new Vector3Int(-1, 0, 1)));
            neighbourList.AddIfNotNull(myGrid.GetNodeByPosition(currentNode.GridPosition + new Vector3Int(-1, 0, -1)));
            neighbourList.AddIfNotNull(myGrid.GetNodeByPosition(currentNode.GridPosition + new Vector3Int(-1, 1, 1)));
            neighbourList.AddIfNotNull(myGrid.GetNodeByPosition(currentNode.GridPosition + new Vector3Int(-1, -1, 1)));
            neighbourList.AddIfNotNull(myGrid.GetNodeByPosition(currentNode.GridPosition + new Vector3Int(-1, 1, -1)));
            neighbourList.AddIfNotNull(myGrid.GetNodeByPosition(currentNode.GridPosition + new Vector3Int(-1, -1, -1)));
            neighbourList.AddIfNotNull(myGrid.GetNodeByPosition(currentNode.GridPosition + new Vector3Int(-1, 1, 0)));
            neighbourList.AddIfNotNull(myGrid.GetNodeByPosition(currentNode.GridPosition + new Vector3Int(-1, -1, 0)));

            //right 
            neighbourList.AddIfNotNull(myGrid.GetNodeByPosition(currentNode.GridPosition + new Vector3Int(1, 0, 0)));
            neighbourList.AddIfNotNull(myGrid.GetNodeByPosition(currentNode.GridPosition + new Vector3Int(1, 0, 1)));
            neighbourList.AddIfNotNull(myGrid.GetNodeByPosition(currentNode.GridPosition + new Vector3Int(1, 0, -1)));
            neighbourList.AddIfNotNull(myGrid.GetNodeByPosition(currentNode.GridPosition + new Vector3Int(1, 1, 1)));
            neighbourList.AddIfNotNull(myGrid.GetNodeByPosition(currentNode.GridPosition + new Vector3Int(1, -1, 1)));
            neighbourList.AddIfNotNull(myGrid.GetNodeByPosition(currentNode.GridPosition + new Vector3Int(1, 1, -1)));
            neighbourList.AddIfNotNull(myGrid.GetNodeByPosition(currentNode.GridPosition + new Vector3Int(1, -1, -1)));
            neighbourList.AddIfNotNull(myGrid.GetNodeByPosition(currentNode.GridPosition + new Vector3Int(1, 1, 0)));
            neighbourList.AddIfNotNull(myGrid.GetNodeByPosition(currentNode.GridPosition + new Vector3Int(1, -1, 0)));

            //center part
            neighbourList.AddIfNotNull(myGrid.GetNodeByPosition(currentNode.GridPosition + new Vector3Int(0, 0, 1)));
            neighbourList.AddIfNotNull(myGrid.GetNodeByPosition(currentNode.GridPosition + new Vector3Int(0, 1, 1)));
            neighbourList.AddIfNotNull(myGrid.GetNodeByPosition(currentNode.GridPosition + new Vector3Int(0, -1, 1)));
            neighbourList.AddIfNotNull(myGrid.GetNodeByPosition(currentNode.GridPosition + new Vector3Int(0, 1, 0)));
            neighbourList.AddIfNotNull(myGrid.GetNodeByPosition(currentNode.GridPosition + new Vector3Int(0, -1, 0)));
            neighbourList.AddIfNotNull(myGrid.GetNodeByPosition(currentNode.GridPosition + new Vector3Int(0, 0, -1)));
            neighbourList.AddIfNotNull(myGrid.GetNodeByPosition(currentNode.GridPosition + new Vector3Int(0, 1, -1)));
            neighbourList.AddIfNotNull(myGrid.GetNodeByPosition(currentNode.GridPosition + new Vector3Int(0, -1, -1)));

            //remove all blocked nodes
            neighbourList.RemoveAll(node => node.IsBlocked);

            return neighbourList;
        }

        /// <summary>
        /// Calculates the path from the start node to the end node using the cameFromNode property
        /// and returns the path.
        /// </summary>
        /// <param name="endNode"></param>
        /// <returns></returns>
        private List<GridNode> CalculatePath(GridNode endNode)
        {
            List<GridNode> path = new List<GridNode>();
            path.Add(endNode);
            PathFindingCost currentNode = _costArray[endNode.Index];
            while (currentNode.CameFromNode != null)
            {
                path.Add(currentNode.CameFromNode);
                currentNode = _costArray[currentNode.CameFromNode.Index];
            }

            path.Reverse();

            return path;
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

        /// <summary>
        /// Draws the path on the grid
        /// </summary>
        private void OnDrawGizmos()
        {
            if (Path == null) return;
            if (!showGizmos) return;

            foreach (var node in Path)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawCube(node.GridPosition + myGrid.PivotPoint, Vector3.one * .1f);
            }
        }

        public MyGrid GetGrid()
        {
            return myGrid;
        }
        
        public List<GridNode> Path { get; private set; }
    }
}