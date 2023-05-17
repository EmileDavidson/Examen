using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AmplifyShaderEditor;
using Runtime.Customer.CustomerStates;
using Runtime.Enums;
using Runtime.Environment;
using Runtime.Grid;
using Runtime.Grid.GridPathFinding;
using Runtime.Managers;
using Runtime.UserInterfaces.Utils;
using Toolbox.Attributes;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Utilities.MethodExtensions;
using Utilities.Other;
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
        [SerializeField] private readonly Guid _id = Guid.NewGuid();
        [SerializeField] private CustomerState state = CustomerState.Spawned;
        [SerializeField] private CustomerInventory inventory;
        [SerializeField] private CustomerMovement movement;
        [SerializeField] private GameObject hip = null;
        [SerializeField] private BarHandler timeBar;

        [FormerlySerializedAs("sprites")] [SerializeField]
        private EmojiSprites emojiSprites;

        [SerializeField] private Image icon;

        public MyGrid Grid { get; private set; }
        public AStarPathFinding AStarPathFinding { get; private set; }

        public FixedPath EntryPath { get; private set; }
        public FixedPath ExitPath { get; private set; }
        public Shelf CurrentTargetShelf { get; set; }
        public CashRegister TargetCashRegister { get; set; }

        private readonly Dictionary<CustomerState, CustomerStateBase> _states = new();

        private List<Grabbable> _myGrabbablePoints = new();
        private int grabbedPoints = 0;
        private bool wasGrabbed = false;
        private EmojiType _emojiType = EmojiType.Neutral;
        private Rigidbody hipRb;


        private void Awake()
        {
            if (WorldManager.Instance.entryPaths.IsEmpty() || WorldManager.Instance.exitPaths.IsEmpty())
            {
                Debug.LogError("No entry or exit paths found");
            }
            
            hipRb = hip.GetComponent<Rigidbody>();

            _states.Add(CustomerState.Spawned, new CustomerSpawnedState(this));
            _states.Add(CustomerState.WalkingToEntrance, new CustomerWalkingToEntranceState(this));
            _states.Add(CustomerState.WalkingToProducts, new CustomerWalkingToProductState(this));
            _states.Add(CustomerState.GettingProducts, new CustomerGettingProductState(this));
            _states.Add(CustomerState.WalkingToCheckout, new CustomerWalkingToCheckoutState(this));
            _states.Add(CustomerState.DroppingProducts, new CustomerDroppingProductsState(this));
            _states.Add(CustomerState.WalkingToExit, new CustomerWalkingToExitState(this));

            Grid = WorldManager.Instance.worldGrid;

            AStarPathFinding = GetComponent<AStarPathFinding>();

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
                foreach (var movementBlockingPoint in movement.BlockingPoints)
                {
                    Grid.GetNodeByIndex(movementBlockingPoint).SetTempBlock(false, ID);
                }

                movement.BlockingPoints.Clear();
                movement.CanMove = false;
                wasGrabbed = true;
            }

            //stopped being grabbed
            if (!IsBeingGrabbed() && wasGrabbed)
            {
                wasGrabbed = false;
                if (movement.WantsToMove == false) return;
                StartCoroutine(FindPathAfterGrab());
            }
        }

        //no longer grabbed so keep checking the velocity and if it's 0 then get the path
        public IEnumerator FindPathAfterGrab()
        {
            yield return new WaitUntil(() => hipRb.velocity.magnitude <= .5);
            var fromNode = Grid.GetNodeFromWorldPosition(hip.transform.position);
            var toNode = movement.Path?.EndNode;
            
            AStarPathFinding.FindPath(fromNode, toNode, (path) =>
            {
                movement.Path = path;
                movement.CanMove = true;
            }, () => {});
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

        public GameObject Hip => hip;

        public BarHandler TimeBar => timeBar;

        public Image Icon => icon;

        public EmojiSprites EmojiSprites => emojiSprites;

        public bool IsGrabbed => grabbedPoints > 0;

        public Guid ID => _id;

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