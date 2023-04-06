using System;
using System.Linq;
using Runtime.Grid;
using Runtime.Grid.GridPathFinding;
using Toolbox.MethodExtensions;
using UnityEngine;
using UnityEngine.Serialization;

namespace Runtime.Customer
{
    /// <summary>
    /// CustomerController controls the customer behaviour
    /// should it walk towards somewhere? should it stand still?
    /// should it be angry? should it be happy? and so on. 
    /// </summary>
    public class CustomerController : MonoBehaviour
    {
        [SerializeField] private GameObject playerHip;
        [SerializeField] private CustomerState state = CustomerState.Spawned;
        [SerializeField] private CustomerInventory inventory;
        [SerializeField] private CustomerMovement movement;
        
        private MyGrid _grid;
        private Shelf TargetShelf { get; set; }
        private CashRegister TargetCashRegister { get; set; }

        private FixedPath _entryPath;
        private FixedPath _exitPath;
        
        private PathFinding _pathFinding;

        
        private void Awake()
        {
            if (WorldManager.Instance.entryPaths.IsEmpty() || WorldManager.Instance.exitPaths.IsEmpty())
            {
                Debug.LogError("No entry or exit paths found");
            }
            
            _grid = WorldManager.Instance.worldGrid;
            
            _pathFinding = GetComponent<PathFinding>();
            
            _entryPath = WorldManager.Instance.entryPaths.RandomItem();
            _exitPath = WorldManager.Instance.exitPaths.RandomItem();
            
            var finalNodeIndex = _entryPath.Path.PathNodes.Last();
            var startNodeIndex = _entryPath.Path.PathNodes.First();
            
            TargetShelf = WorldManager.Instance.shelves.RandomItem();
            TargetCashRegister = WorldManager.Instance.checkouts.RandomItem();

            _pathFinding.StartPathfinding(_grid.GetNodeByIndex(finalNodeIndex), _grid.nodes[TargetShelf.InteractionGridIndex]);

            inventory ??= GetComponent<CustomerInventory>();
            movement ??= GetComponent<CustomerMovement>();
        }

        private void Start()
        {
            state = CustomerState.WalkingToEntrance;
            movement.Path = _entryPath.Path.Copy();    ;
            movement.onDestinationReached.AddListener(ReachedTarget);
        }

        private void ReachedTarget()
        {
            if (state == CustomerState.WalkingToEntrance)
            {
                state = CustomerState.WalkingToProducts;
                movement.Path = _pathFinding.Path;
                return;
            }
            
            if (state == CustomerState.WalkingToProducts)
            {
                state = CustomerState.WalkingToCheckout;
                _pathFinding.EndPathFinding();
                var playerGridPos = _grid.GetNodeFromWorldPosition(playerHip.transform.position);
                movement.Path = null;
                _pathFinding.StartPathfinding(playerGridPos, _grid.nodes[TargetCashRegister.InteractionGridIndex], (path) =>
                {
                    movement.Path = path;
                });
                return;
            }
        }
    }
}