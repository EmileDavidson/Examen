using System.Collections.Generic;
using Runtime.Customer.CustomerStates;
using Runtime.Grid;
using Runtime.Grid.GridPathFinding;
using Toolbox.MethodExtensions;
using UnityEngine;

namespace Runtime.Customer
{
    /// <summary>
    /// CustomerController controls the customer behaviour
    /// should it walk towards somewhere? should it stand still?
    /// should it be angry? should it be happy? and so on.
    /// </summary>
    public class CustomerController : MonoBehaviour
    {
        [SerializeField] private CustomerState state = CustomerState.Spawned;
        [SerializeField] private CustomerInventory inventory;
        [SerializeField] private CustomerMovement movement;
        [SerializeField] private GameObject playerHip = null;

        public MyGrid Grid { get; private set; }
        public PathFinding PathFinding { get; private set; }
        
        public FixedPath EntryPath { get; private set; }
        public FixedPath ExitPath { get; private set; }
        
        private CashRegister TargetCashRegister { get; set; }
        private readonly Dictionary<CustomerState, CustomerStateBase> _states = new();
        
        private void Awake()
        {
            if (WorldManager.Instance.entryPaths.IsEmpty() || WorldManager.Instance.exitPaths.IsEmpty())
            {
                Debug.LogError("No entry or exit paths found");
            }

            _states.Add(CustomerState.Spawned, new CustomerSpawnedState(this));
            _states.Add(CustomerState.WalkingToEntrance, new CustomerWalkingToEntranceState(this));
            _states.Add(CustomerState.WalkingToProducts, new CustomerWalkingToProductState(this));
            _states.Add(CustomerState.GettingProducts, new CustomerGettingProductState(this));
            _states.Add(CustomerState.WalkingToCheckout, new CustomerWalkingToCheckoutState(this));
            _states.Add(CustomerState.DroppingProducts, new CustomerDroppingProductsState(this));
            _states.Add(CustomerState.FinishingShopping, new CustomerFinishedShoppingState(this));
            _states.Add(CustomerState.WalkingToExit, new CustomerWalkingToExitState(this));

            Grid = WorldManager.Instance.worldGrid;

            PathFinding = GetComponent<PathFinding>();

            TargetCashRegister = WorldManager.Instance.checkouts.RandomItem();
            EntryPath ??= WorldManager.Instance.entryPaths.RandomItem();
            ExitPath ??= TargetCashRegister.ExitPath;


            inventory ??= GetComponent<CustomerInventory>();
            movement ??= GetComponent<CustomerMovement>();
        }

        private void Start()
        {
            _states[state].OnStateStart();
        }

        private void Update()
        {
            _states[state].OnStateUpdate();
        }

        public void DestroyThisCustomer()
        {
            Destroy(this.gameObject);
        }
        
        #region getters & setters

        public CustomerState State
        {
            get => state;
            set
            {
                if (state == value) return;
                state = value;
                _states[state].OnStateStart();
            }
        }

        public CustomerInventory Inventory => inventory;

        public CustomerMovement Movement => movement;

        public GameObject PlayerHip => playerHip;
        
        #endregion // getters & setters
    }
}