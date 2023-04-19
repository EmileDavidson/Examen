using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Toolbox.Attributes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

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

        public Path Path { get; private set; }

        public UnityEvent<Path> onPathFound = new();
        public UnityEvent onPathNotFound = new();
        public UnityEvent<Path> onNewPathFound = new();
        public UnityEvent onFindingNewPath = new();
        public UnityEvent<Path> onFindingNewPathFailed = new();

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
            myGrid ??= GetComponent<MyGrid>();
            myGrid ??= GameObject.Find("Floor")?
                .GetComponent<MyGrid>(); //TODO: FIX THIS SO ITS NOT A HARD DEPENDENCY TO THE FLOOR GAMEOBJECT
            _openList = new List<GridNode>();
            _closedList = new List<GridNode>();
            Path = new Path(new List<int>(), null, null);

            onPathFound.AddListener((_) => { _isFindingPath = false; });
            onPathNotFound.AddListener(() => { _isFindingPath = false; });
        }

        /// <summary>
        /// Calculates the path from the current node to the requested node
        /// and subscribes to the grid changed event to update on path change.
        /// </summary>
        /// <param name="currentNode"></param>
        /// <param name="requestedNode"></param>
        /// <param name="initialPathFound"></param>
        [Button(Mode = ButtonMode.EnabledInPlayMode)]
        public void StartPathfinding(GridNode currentNode, GridNode requestedNode, Action<Path> initialPathFound)
        {
            myGrid.onGridChangedWithNode.AddListener(GridChangedUpdate);
            _needsPath = true;
            Path.StartNode = currentNode;
            Path.EndNode = requestedNode;
            Path.CurrentIndex = 0;
            FindPathAsync(currentNode.GridPosition, requestedNode.GridPosition, initialPathFound.Invoke);
        }

        /// <summary>
        /// Stops the pathfinding from updating on changes and clears the path
        /// </summary>
        [Button(Mode = ButtonMode.EnabledInPlayMode)]
        public void EndPathFinding()
        {
            myGrid.onGridChangedWithNode.RemoveListener(GridChangedUpdate);
            Path.Reset();
            _openList.Clear();
            _closedList.Clear();
            _needsPath = false;
            _isFindingPath = false;
        }

        public void RecalculatePath(GridNode currentNode, Action<Path> calculationComplete,
            NeighbourValidationSettings settings = null)
        {
            settings ??= new NeighbourValidationSettings();
            if (_isFindingPath || !_needsPath) return;
            FindPathAsync(currentNode.GridPosition, Path.EndNode.GridPosition, (path) =>
            {
                this.Path = path;
                calculationComplete.Invoke(path);
            }, settings);
        }

        /// <summary>
        /// Is triggered when the grid changes and updates the path if the changed node is in the path
        /// </summary>
        /// <param name="node"></param>
        private void GridChangedUpdate(GridNode node)
        {
            if (!_needsPath) return;
            if (Path != null && !Path.PathNodes.Contains(node.Index)) return;
            if (Path == null)
            {
                Debug.LogError("Path is null but that should not happen here.");
                return;
            }

            var oldPath = Path.Copy();
            onFindingNewPath.Invoke();
            FindPathAsync(Path.StartNode.GridPosition, Path.EndNode.GridPosition, (foundPath) =>
            {
                if (foundPath is null) onFindingNewPathFailed.Invoke(oldPath);
                else onNewPathFound.Invoke(foundPath);
            });
        }

        /// <summary>
        /// Async path finding
        /// </summary>
        /// <returns></returns>
        private async void FindPathAsync(Vector3Int currentNodePos, Vector3Int requestedNodePos, Action<Path> callback, NeighbourValidationSettings settings = null)
        {
            settings ??= new NeighbourValidationSettings();

            if (_isFindingPath)
            {
                return;
            }

            Task<Path> task = new Task<Path>(() => FindPath(currentNodePos, requestedNodePos, settings));
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
        private Path FindPath(Vector3Int currentNodePos, Vector3Int requestedNodePos,
            NeighbourValidationSettings settings)
        {
            settings ??= new NeighbourValidationSettings();
            _isFindingPath = true;
            Path.Reset();

            Path.StartNode = myGrid.GetNodeByPosition(currentNodePos);
            Path.EndNode = myGrid.GetNodeByPosition(requestedNodePos);

            if (Path.StartNode == null || Path.EndNode == null)
            {
                onPathNotFound.Invoke();
                return Path;
            }

            if (currentNodePos == requestedNodePos)
            {
                onPathNotFound.Invoke();
                return Path;
            }

            _openList = new List<GridNode> { Path.StartNode };
            _closedList = new List<GridNode>();
            var startNodeIndex = Path.StartNode.Index;
            var endNodeIndex = Path.EndNode.Index;

            _costArray = new PathFindingCost[myGrid.Height * myGrid.Width * myGrid.Depth];

            GridHelper.Grid3dLoop(myGrid.Width, myGrid.Height, myGrid.Depth,
                (index, _) => { _costArray[index] = new PathFindingCost(int.MaxValue, 0, null); });


            _costArray[startNodeIndex].Gcost = 0;
            _costArray[startNodeIndex].Hcost = GetDistance(Path.StartNode, Path.EndNode);
            _costArray[startNodeIndex].Fcost = _costArray[startNodeIndex].Gcost + _costArray[startNodeIndex].Hcost;

            while (_openList.Count > 0)
            {
                GridNode currentNodeInOpenList = GetLowestFCostNode(_openList);
                _openList.Remove(currentNodeInOpenList);
                _closedList.Add(currentNodeInOpenList);

                if (currentNodeInOpenList.Index == endNodeIndex)
                {
                    Path = CalculatePath(Path.EndNode);
                    onPathFound.Invoke(Path);
                    return Path;
                }

                //remove all blocked nodes
                var neighbourList = myGrid.GetDirectNeighbourList(currentNodeInOpenList);

                neighbourList.RemoveAll(node => node.IsBlocked && !settings.CanBeBlocked);
                neighbourList.RemoveAll(node => node.IsLocationNode && node.Index != endNodeIndex && !settings.CanBeEndPoint);
                neighbourList.RemoveAll(node => node.IsTempBlocked && !settings.CanBeTempBlocked);

                foreach (GridNode neighbourNode in neighbourList)
                {
                    if (_closedList.Contains(neighbourNode)) continue;

                    int tentativeGCost = _costArray[currentNodeInOpenList.Index].Gcost +
                                         GetDistance(currentNodeInOpenList, neighbourNode);
                    if (tentativeGCost >= _costArray[neighbourNode.Index].Gcost) continue;
                    _costArray[neighbourNode.Index].CameFromNode = currentNodeInOpenList;
                    _costArray[neighbourNode.Index].Gcost = tentativeGCost;
                    _costArray[neighbourNode.Index].Hcost = GetDistance(neighbourNode, Path.EndNode);
                    _costArray[neighbourNode.Index].Fcost =
                        _costArray[neighbourNode.Index].Gcost + _costArray[neighbourNode.Index].Hcost;

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
        /// Calculates the path from the start node to the end node using the cameFromNode property
        /// and returns the path.
        /// </summary>
        /// <param name="endNode"></param>
        /// <returns></returns>
        private Path CalculatePath(GridNode endNode)
        {
            Path newPath = new Path(new List<int>(), Path.StartNode, Path.EndNode);
            newPath.PathNodes.Add(endNode.Index);
            PathFindingCost currentNode = _costArray[endNode.Index];
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

        /// <summary>
        /// Draws the path on the grid
        /// </summary>
        private void OnDrawGizmos()
        {
            if (Path == null) return;
            if (!showGizmos) return;

            foreach (int node in Path.PathNodes)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawCube(myGrid.GetWorldPositionOfNode(myGrid.GetNodeByIndex(node).GridPosition),
                    Vector3.one * .3f);
            }

            //draw the start and end node
            if (Path.StartNode == null || Path.EndNode == null) return;
            Gizmos.color = Color.red;
            Gizmos.DrawCube(myGrid.GetWorldPositionOfNode(Path.StartNode.GridPosition), Vector3.one * 1.1f);
            Gizmos.color = Color.blue;
            Gizmos.DrawCube(myGrid.GetWorldPositionOfNode(Path.EndNode.GridPosition), Vector3.one * 1.1f);
        }


        /// <summary>
        /// Gets the grid. Used for the pathfinding
        /// </summary>
        /// <returns></returns>
        public MyGrid GetGrid()
        {
            return myGrid;
        }
    }
}