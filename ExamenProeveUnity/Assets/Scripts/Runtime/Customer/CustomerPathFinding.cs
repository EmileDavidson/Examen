// using System.Collections;
// using System.Collections.Generic;
// using Runtime.Grid;
// using Runtime.Grid.GridPathFinding;
// using Toolbox.MethodExtensions;
// using UnityEngine;
// using Grid = Runtime.Grid.Grid;
// using Random = UnityEngine.Random;
//
// public class CustomerPathFinding : MonoBehaviour
// {
//     [SerializeField] private Rigidbody hipRb;
//     [SerializeField] private ConfigurableJoint hipJoint;
//     [SerializeField] private Grid grid;
//
//     private List<GridNode> _path;
//
//     [SerializeField] private bool _doPathfinding = true;
//     private PathFinding _pathFinding;
//
//     private List<CashRegister> _cashRegisters;
//     private List<Shelf> _shelves;
//
//     private Shelf _targetShelf;
//
//     private Vector3Int _gridPosition;
//     private Vector3Int _targetGridPosition;
//     private Vector3 _moveDirection;
//
//     private void Awake()
//     {
//         _cashRegisters = new List<CashRegister>(FindObjectsOfType<CashRegister>());
//         _shelves = new List<Shelf>(FindObjectsOfType<Shelf>());
//         _pathFinding = new PathFinding(grid);
//
//
//         // InvokeRepeating(nameof(ReCalculatePath), 1f, 1f);
//     }
//
//     private void Start()
//     {
//         //temp block nodes this script should not be responsible for this
//         grid.BlockNodeByPosition(new Vector3Int(0, 0, 1));
//         grid.BlockNodeByPosition(new Vector3Int(1, 0, 2));
//         grid.BlockNodeByPosition(new Vector3Int(2, 0, 3));
//
//         if (_shelves is null)
//         {
//             Debug.LogWarning("No shelves found");
//             return;
//         }
//
//         _targetShelf = _shelves[Random.Range(0, _shelves.Count)];
//         _targetGridPosition = _targetShelf.InteractPosition;
//         
//         InvokeRepeating(nameof(ReCalculatePath), 1f, 1f);
//     }
//
//     private void FixedUpdate()
//     {
//         if (_doPathfinding) PathFind();
//     }
//
//     private void PathFind()
//     {
//         var targetAngle = Mathf.Atan2(_moveDirection.z, _moveDirection.x) * Mathf.Rad2Deg;
//         float rotationSpeed = (hipJoint.targetRotation.eulerAngles - new Vector3(0f, targetAngle, 0f)).magnitude;
//         Vector3 npcPos = hipRb.gameObject.transform.position;
//
//         //get position in grid from player pos 
//         GridNode playerNode = grid.GetNodeFromWorldPosition(npcPos);
//         _gridPosition = playerNode.GridPosition;
//
//         if (_path is not null && _path.IsNotEmpty())
//         {
//             GridNode nextNode = _path[1];
//             _moveDirection = (new Vector3(nextNode.GridPosition.x, 0, nextNode.GridPosition.z) - new Vector3(npcPos.x, 0, npcPos.z)).normalized;
//             if (_gridPosition == new Vector3Int(nextNode.GridPosition.x, 0, nextNode.GridPosition.z))
//             {
//                 ReCalculatePath();
//             }
//         }
//
//         hipJoint.targetRotation = Quaternion.RotateTowards(hipJoint.targetRotation, Quaternion.Euler(0f, targetAngle, 0f), rotationSpeed * Time.deltaTime);
//         hipRb.position += _moveDirection / 5f;
//
//         if (_gridPosition != _targetGridPosition) return;
//         if (_targetShelf is not null)
//         {
//             if (_gridPosition != _targetShelf.InteractPosition) return;
//             StartCoroutine(WaitAtPosition());
//
//             _targetShelf = null;
//             _targetGridPosition = _cashRegisters[0].InteractPosition;
//         }
//         else if (_gridPosition == _cashRegisters[0].InteractPosition)
//         {
//             //todo: at final position (cash register) what now?
//         }
//     }
//
//     public void ReCalculatePath()
//     {
//         _path = _pathFinding.FindPath(new Vector3Int(_gridPosition.x, 0,_gridPosition.z), new Vector3Int(_targetGridPosition.x, 0,_targetGridPosition.z));
// print(_path.Count);
//         DrawPathDebug(_path);
//     }
//
//     private void DrawPathDebug(List<GridNode> pathNodes)
//     {
//         return;
//         if (_path is null) return;
//         foreach (GridNode pathNode in pathNodes)
//         {
//             if (_path.IndexOf(pathNode) + 1 >= pathNodes.Count) return;
//             GridNode nextNode = pathNodes[pathNodes.IndexOf(pathNode) + 1];
//
//             Debug.DrawLine(new Vector3(pathNode.GridPosition.x + 0.5f, 0, pathNode.GridPosition.z + 0.5f),
//                 new Vector3(nextNode.GridPosition.x + 0.5f, 0, nextNode.GridPosition.z + 0.5f), Color.blue, 1f);
//         }
//     }
//
//     private IEnumerator WaitAtPosition()
//     {
//         _doPathfinding = false;
//         Vector3 lookDirection = (_targetShelf.gameObject.transform.position - hipJoint.targetRotation.eulerAngles)
//             .normalized;
//         hipJoint.targetRotation = Quaternion.LookRotation(lookDirection);
//         hipRb.constraints = RigidbodyConstraints.FreezeAll;
//         yield return new WaitForSeconds(4f);
//         hipRb.constraints = RigidbodyConstraints.None;
//         hipRb.constraints = RigidbodyConstraints.FreezePositionY;
//         _doPathfinding = true;
//     }
// }