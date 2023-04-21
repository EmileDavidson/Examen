using System;
using System.Collections.Generic;
using System.Linq;
using Runtime.Customer.CustomerStates;
using Runtime.Enums;
using Runtime.Grid;
using Runtime.Grid.GridPathFinding;
using Runtime.Managers;
using Runtime.UserInterfaces.Utils;
using Toolbox.MethodExtensions;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Utilities.ScriptableObjects;

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
        [SerializeField] private BarHandler timeBar;
        [SerializeField] private Sprites sprites;

        [SerializeField] private Image icon;

        public MyGrid Grid { get; private set; }
        public PathFinding PathFinding { get; private set; }

        public FixedPath EntryPath { get; private set; }
        public FixedPath ExitPath { get; private set; }
        public Shelf CurrentTargetShelf { get; set; }
        public CashRegister TargetCashRegister { get; set; }

        private readonly Dictionary<CustomerState, CustomerStateBase> _states = new();

        private List<Grabbable> _myGrabbablePoints = new();
        private int grabbedPoints = 0;
        private bool wasGrabbed = false;


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
            _states.Add(CustomerState.WalkingToExit, new CustomerWalkingToExitState(this));

            Grid = WorldManager.Instance.worldGrid;

            PathFinding = GetComponent<PathFinding>();

            TargetCashRegister = WorldManager.Instance.checkouts.RandomItem();
            EntryPath ??= WorldManager.Instance.entryPaths.RandomItem();
            ExitPath ??= TargetCashRegister.ExitPath;

            inventory ??= GetComponent<CustomerInventory>();
            movement ??= GetComponent<CustomerMovement>();
            _myGrabbablePoints = GetComponentsInChildren<Grabbable>().ToList();

            foreach (var grabbable in _myGrabbablePoints)
            {
                grabbable.OnGrabbed.AddListener(() =>
                {
                    grabbedPoints++;
                    GrabValueChanged();
                });
                grabbable.OnReleased.AddListener(() =>
                {
                    grabbedPoints--;
                    GrabValueChanged();
                });
            }
        }

        private void Start()
        {
            _states[state].OnStateStart();
        }

        private void Update()
        {
            _states[state].OnStateUpdate();
        }

        private void GrabValueChanged()
        {
            //started to be grabbed
            if (IsBeingGrabbed() && !wasGrabbed)
            {
                //release all temp blocked nodes from used path
                movement.CanMove = false;
                wasGrabbed = true;
            }

            //stopped being grabbed
            if (!IsBeingGrabbed() && wasGrabbed)
            {
                wasGrabbed = false;
                if (movement.Path.PathType == PathType.Fixed)
                {
                    movement.CanMove = true;
                    return;
                }

                PathFinding.RecalculatePath(Grid.GetNodeFromWorldPosition(playerHip.transform.position), (path) =>
                {
                    movement.Path = path;
                    movement.CanMove = true;
                });
            }
        }

        private bool IsBeingGrabbed()
        {
            return grabbedPoints > 0;
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

        public BarHandler TimeBar => timeBar;

        public Image Icon => icon;

        public Sprites Sprites => sprites;

        public bool IsGrabbed => grabbedPoints > 0;

        #endregion // getters & setters
    }
}