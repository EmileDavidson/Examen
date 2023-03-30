using System;
using System.Collections.Generic;
using UnityEngine;

public class CustomerPathFinding : MonoBehaviour
{
    [SerializeField] private Grid<PathNode> _grid;
    [SerializeField] private List<PathNode> _path;
    
    [SerializeField] private Rigidbody hipRb;

    private void Awake()
    {
        InvokeRepeating(nameof(ReCalculatePath), 1f, 1f);
    }

    public void ReCalculatePath()
    {
        
    }

    private void Start()
    {
        PathFinding pathFinding = new PathFinding(10, 10);
        pathFinding.BlockNode(0, 1, true);
        pathFinding.BlockNode(1, 2, true);
        pathFinding.BlockNode(2, 2, true);
        
        _path = pathFinding.FindPath(0, 0, 3, 7);
        if (_path != null)
        {
            foreach (PathNode pathNode in _path)
            {
                if (_path.IndexOf(pathNode) + 1 >= _path.Count) return;
                PathNode nextNode = _path[_path.IndexOf(pathNode) + 1];
                
                Debug.Log("Currently at: " + pathNode.X + ", " + pathNode.Y);
                Debug.Log("Next node at: " + nextNode.X + ", " + nextNode.Y);
                
                Debug.DrawLine(new Vector3(pathNode.X + 0.5f, 0, pathNode.Y + 0.5f), new Vector3(nextNode.X + 0.5f, 0, nextNode.Y + 0.5f), Color.blue, 100f);
            }
        }
    }
}