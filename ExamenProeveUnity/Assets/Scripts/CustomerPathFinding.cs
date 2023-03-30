using System.Collections.Generic;
using UnityEngine;

public class CustomerPathFinding : MonoBehaviour
{
    [SerializeField] private List<PathNode> _path;
    
    [SerializeField] private Rigidbody hipRb;

    private Grid<PathNode> _grid;
    private PathFinding _pathFinding;

    private void Awake()
    {
        _pathFinding = new PathFinding(10, 10);
        _grid = _pathFinding.Grid;
        InvokeRepeating(nameof(ReCalculatePath), 1f, 1f);
    }

    public void ReCalculatePath()
    {
        Vector3 playerPos = hipRb.gameObject.transform.position;
        Vector2 gridPosition = new Vector2(_grid.GetPositionInGrid(playerPos).X, _grid.GetPositionInGrid(playerPos).Y);
        print(gridPosition);
    }

    private void Start()
    {
        _pathFinding.BlockNode(0, 1, true);
        _pathFinding.BlockNode(1, 2, true);
        _pathFinding.BlockNode(2, 2, true);
        
        _path = _pathFinding.FindPath(0, 0, 3, 7);
        if (_path != null)
        {
            foreach (PathNode pathNode in _path)
            {
                if (_path.IndexOf(pathNode) + 1 >= _path.Count) return;
                PathNode nextNode = _path[_path.IndexOf(pathNode) + 1];
                
                // Debug.Log("Currently at: " + pathNode.X + ", " + pathNode.Y);
                // Debug.Log("Next node at: " + nextNode.X + ", " + nextNode.Y);
                
                Debug.DrawLine(new Vector3(pathNode.X + 0.5f, 0, pathNode.Y + 0.5f), new Vector3(nextNode.X + 0.5f, 0, nextNode.Y + 0.5f), Color.blue, 100f);
            }
        }
    }
}