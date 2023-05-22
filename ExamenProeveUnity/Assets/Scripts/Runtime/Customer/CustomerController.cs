using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Runtime.Customer.CustomerStates;
using Runtime.Enums;
using Runtime.Environment;
using Runtime.Grid;
using Runtime.Grid.GridPathFinding;
using Runtime.Managers;
using Runtime.UserInterfaces.Utils;
using UnityEngine;
using UnityEngine.UI;
using Utilities.MethodExtensions;
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
        private readonly Guid _id = Guid.NewGuid();
        
        [SerializeField] private CustomerState state = CustomerState.Spawned;
        [SerializeField] private CustomerInventory inventory;
        [SerializeField] private CustomerMovement movement;
        [SerializeField] private GameObject hip;
        [SerializeField] private BarHandler timeBar;
        [SerializeField] private Image icon;
        [SerializeField] private EmojiSprites emojiSprites;
        
        [SerializeField] private MyGrid grid;
        [SerializeField] public AStarPathFinding aStarPathFinding;

        public FixedPath EntryPath { get; private set; }
        public FixedPath ExitPath { get; private set; }
        public CashRegister TargetCashRegister { get; private set; }
        public Shelf CurrentTargetShelf { get; set; }
        public bool wasGrabbed = false;


        private readonly Dictionary<CustomerState, CustomerStateBase> _states = new();
        private List<Grabbable> _myGrabbablePoints = new();
        private int _grabbedPoints = 0;
        private EmojiType _emojiType = EmojiType.Neutral;
        private Rigidbody _hipRb;

        private Coroutine _currentCoroutine;

        private void Awake()
        {
            if (WorldManager.Instance.entryPaths.IsEmpty() || WorldManager.Instance.exitPaths.IsEmpty())
            {
                Debug.LogError("No entry or exit paths found");
            }

            _hipRb = hip.GetComponent<Rigidbody>();

            _states.Add(CustomerState.Spawned, new CustomerSpawnedState(this));
            _states.Add(CustomerState.WalkingToEntrance, new CustomerWalkingToEntranceState(this));
            _states.Add(CustomerState.WalkingToProducts, new CustomerWalkingToProductState(this));
            _states.Add(CustomerState.GettingProducts, new CustomerGettingProductState(this));
            _states.Add(CustomerState.WalkingToCheckout, new CustomerWalkingToCheckoutState(this));
            _states.Add(CustomerState.DroppingProducts, new CustomerDroppingProductsState(this));
            _states.Add(CustomerState.WalkingToExit, new CustomerWalkingToExitState(this));

            grid = WorldManager.Instance.worldGrid;

            aStarPathFinding = GetComponent<AStarPathFinding>();

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
                    _grabbedPoints++;
                    _states[state].OnGrabbed();
                });
                grabbable.OnReleased.AddListener(() =>
                {
                    _grabbedPoints--;
                    _states[state].OnReleased();
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

        //no longer grabbed so keep checking the velocity and if it's 0 then get the path
        private IEnumerator FindPathAfterGrab(GridNode toNode = null)
        {
            yield return new WaitUntil(() => _hipRb.velocity.magnitude <= .5);
            var fromNode = grid.GetNodeFromWorldPosition(hip.transform.position);
            if (toNode == null)
            {
                toNode = movement.Path?.EndNode;
            }
            

            aStarPathFinding.FindPath(fromNode, toNode, (path) =>
            {
                movement.Path = path;
                movement.CanMove = true;
                _currentCoroutine = null;
            }, () =>
            {
                print("Failed to find path between node: " + fromNode + " and node: " + toNode);
                _currentCoroutine = null;
            });
        }

        public void FindPathAfterGrabCoroutine(GridNode toNode = null)
        {
            if(_currentCoroutine != null)
            {
                StopCoroutine(_currentCoroutine);
            }
            _currentCoroutine = StartCoroutine(FindPathAfterGrab(toNode));
        }


        public bool IsBeingGrabbed()
        {
            return _grabbedPoints > 0;
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

        public GameObject Hip => hip;

        public BarHandler TimeBar => timeBar;

        public Image Icon => icon;

        public EmojiSprites EmojiSprites => emojiSprites;

        public bool IsGrabbed => _grabbedPoints > 0;

        public Guid ID => _id;

        public MyGrid Grid => grid;

        public EmojiType EmojiType
        {
            get => _emojiType;
            set
            {
                icon.sprite = emojiSprites.GetSprite(value);
                _emojiType = value;
            }
        }

        #endregion // getters & setters
    }
}