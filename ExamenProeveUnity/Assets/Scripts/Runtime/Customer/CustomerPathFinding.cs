using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CustomerPathFinding : MonoBehaviour
{
    [SerializeField] private Rigidbody hipRb;
    [SerializeField] private ConfigurableJoint hipJoint;

    private bool _doPathfinding = true;
    private List<PathNode> _path;
    private Grid<PathNode> _grid;
    private PathFinding _pathFinding;

    private List<Shelf> _shelves;
    private Shelf _targetShelf;
    private CustomerInventory _customerInventory;

    private Vector2Int _gridPosition;
    private Vector2Int _targetGridPosition;
    private Vector3 _moveDirection;

    private void Awake()
    {
        _shelves = new List<Shelf>(FindObjectsOfType<Shelf>());
        _customerInventory = GetComponent<CustomerInventory>();
        _pathFinding = new PathFinding(10, 10);
        _grid = _pathFinding.Grid;
        
        _pathFinding.BlockNode(0, 1, true);
        _pathFinding.BlockNode(1, 2, true);
        _pathFinding.BlockNode(2, 2, true);
        
        InvokeRepeating(nameof(ReCalculatePath), 1f, 1f);
    }

    private void Start()
    {
        if (_shelves is not null)
        {
            _targetShelf = _shelves[Random.Range(0, _shelves.Count)];
            _targetGridPosition = _targetShelf.InteractPosition;
        }
    }

    private void FixedUpdate()
    {
        if (_doPathfinding) PathFind();
    }

    private void PathFind()
    {
        var targetAngle = Mathf.Atan2(_moveDirection.z, _moveDirection.x) * Mathf.Rad2Deg;
        float rotationSpeed = (hipJoint.targetRotation.eulerAngles - new Vector3(0f, targetAngle, 0f)).magnitude;
        Vector3 playerPos = hipRb.gameObject.transform.position;
        
        _gridPosition = new Vector2Int(_grid.GetPositionInGrid(playerPos).X, _grid.GetPositionInGrid(playerPos).Y);
        
        if (_path is not null && _path.Count > 1)
        {
            PathNode nextNode = _path[1];
            _moveDirection = (new Vector3(nextNode.X, 0, nextNode.Y) - new Vector3(playerPos.x, 0, playerPos.z)).normalized;
            if (_gridPosition == new Vector2Int(nextNode.X, nextNode.Y))
            {
                ReCalculatePath();
            }
        }

        hipJoint.targetRotation = Quaternion.RotateTowards(hipJoint.targetRotation, Quaternion.Euler(0f, targetAngle, 0f), rotationSpeed * Time.deltaTime);
        hipRb.position += _moveDirection / 5f;

        if (_targetShelf is null) return;
        if (_gridPosition == _targetShelf.InteractPosition)
        {
            StartCoroutine(WaitAtShelf());
            _customerInventory.AddItem(_targetShelf.GrabItem());
            if (_customerInventory.GetInventory().Count < _customerInventory.maxInventorySize)
            {
                List<Shelf> potentialShelves = new List<Shelf>();
                foreach (Shelf shelf in _shelves)
                {
                    if (shelf.IsEmpty() || shelf == _targetShelf) continue;
                    potentialShelves.Add(shelf);
                }

                if (potentialShelves.Count >= 1)
                {
                    _targetShelf = potentialShelves[Random.Range(0, potentialShelves.Count)];
                    _targetGridPosition = _targetShelf.InteractPosition;
                }
                else
                {
                    _targetShelf = null;
                    _targetGridPosition = new Vector2Int(0, 0);
                }
            }
            else
            {
                _targetShelf = null;
                _targetGridPosition = new Vector2Int(0, 0);
            }
        }
    }

    public void ReCalculatePath()
    {
        _path = _pathFinding.FindPath(_gridPosition.x, _gridPosition.y, _targetGridPosition.x, _targetGridPosition.y);

        DrawPathDebug(_path);
    }

    private void DrawPathDebug(List<PathNode> pathNodes)
    {
        if (_path is null) return;
        foreach (PathNode pathNode in pathNodes)
        {
            if (_path.IndexOf(pathNode) + 1 >= pathNodes.Count) return;
            PathNode nextNode = pathNodes[pathNodes.IndexOf(pathNode) + 1];
                
            Debug.DrawLine(new Vector3(pathNode.X + 0.5f, 0, pathNode.Y + 0.5f), new Vector3(nextNode.X + 0.5f, 0, nextNode.Y + 0.5f), Color.blue, 1f);
        }
    }

    private IEnumerator WaitAtShelf()
    {
        _doPathfinding = false;
        Vector3 lookDirection = (_targetShelf.gameObject.transform.position - hipJoint.targetRotation.eulerAngles).normalized;
        hipJoint.targetRotation = Quaternion.LookRotation(lookDirection);
        hipRb.constraints = RigidbodyConstraints.FreezeAll;
        yield return new WaitForSeconds(4f);
        hipRb.constraints = RigidbodyConstraints.None;
        hipRb.constraints = RigidbodyConstraints.FreezePositionY;
        _doPathfinding = true;
    }
}