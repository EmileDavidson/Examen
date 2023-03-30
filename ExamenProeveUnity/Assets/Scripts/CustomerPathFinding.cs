using System;
using System.Collections.Generic;
using UnityEngine;

public class CustomerPathFinding : MonoBehaviour
{
    [SerializeField] private List<PathNode> _path;
    
    [SerializeField] private Rigidbody hipRb;
    [SerializeField] private ConfigurableJoint hipJoint;

    private Grid<PathNode> _grid;
    private PathFinding _pathFinding;

    private Vector3 _moveDirection;

    private void Awake()
    {
        _pathFinding = new PathFinding(10, 10);
        _grid = _pathFinding.Grid;
        
        _pathFinding.BlockNode(0, 1, true);
        _pathFinding.BlockNode(1, 2, true);
        _pathFinding.BlockNode(2, 2, true);
        
        InvokeRepeating(nameof(ReCalculatePath), 1f, 1f);
    }

    private void FixedUpdate()
    {
        // hipRb.velocity = Vector3.zero;
        var targetAngle = Mathf.Atan2(_moveDirection.z, _moveDirection.x) * Mathf.Rad2Deg;
        float rotationSpeed = (hipJoint.targetRotation.eulerAngles - new Vector3(0f, targetAngle, 0f)).magnitude;
        hipJoint.targetRotation = Quaternion.RotateTowards(hipJoint.targetRotation, Quaternion.Euler(0f, targetAngle, 0f), rotationSpeed);
        hipRb.position += _moveDirection / 5f;
    }

    public void ReCalculatePath()
    {
        Vector3 playerPos = hipRb.gameObject.transform.position;
        Vector2Int gridPosition = new Vector2Int(_grid.GetPositionInGrid(playerPos).X, _grid.GetPositionInGrid(playerPos).Y);

        _path = _pathFinding.FindPath(gridPosition.x, gridPosition.y, 3, 7);
        
        if (_path == null) return;
        
        DrawPathDebug(_path);

        if (_path.Count > 1)
        {
            PathNode nextNode = _path[1];
            _moveDirection = (new Vector3(nextNode.X, 0, nextNode.Y) - new Vector3(playerPos.x, 0, playerPos.z)).normalized;
        }
    }

    private void DrawPathDebug(List<PathNode> pathNodes)
    {
        foreach (PathNode pathNode in pathNodes)
        {
            if (_path.IndexOf(pathNode) + 1 >= pathNodes.Count) return;
            PathNode nextNode = pathNodes[pathNodes.IndexOf(pathNode) + 1];
                
            Debug.DrawLine(new Vector3(pathNode.X + 0.5f, 0, pathNode.Y + 0.5f), new Vector3(nextNode.X + 0.5f, 0, nextNode.Y + 0.5f), Color.blue, 1f);
        }
    }
}