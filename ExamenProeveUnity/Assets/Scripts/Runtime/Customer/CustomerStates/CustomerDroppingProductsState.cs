using System.Linq;
using Runtime.Environment;
using Runtime.Managers;
using UnityEngine;
using Utilities.MethodExtensions;
using Utilities.Other.Runtime.Timer;

namespace Runtime.Customer.CustomerStates
{
    public class CustomerDroppingProductsState : CustomerStateBase
    {
        private const int WaitTime = 15;
        private Timer _timer;
        private CashRegister _register;

        private bool readyToExit = false;


        public CustomerDroppingProductsState(CustomerController controller) : base(controller)
        {
        }

        public override void OnStateStart()
        {
            Controller.Movement.WantsToMove = false;
            if (Controller.Inventory.Items.IsEmpty())
            {
                FinishState();
                return;
            }

            _register = Controller.TargetCashRegister;
            int cashRegisterNodeIndex = Controller.ExitPath.pathNodeIndexes.First();
            Controller.Grid.GetNodeByIndex(cashRegisterNodeIndex).SetTempBlock(true, Controller.ID);
            _register.InstantiateProduct(Controller.Inventory.Items[0]);
            _timer = new Timer(WaitTime);

            _timer.onTimerUpdate.AddListener((value) => { Controller.TimeBar.Scale = value; });

            _timer.onTimerFinished.AddListener(() =>
            {
                Controller.EmojiType = Controller.EmojiSprites.GetPrevious(Controller.EmojiType);
                readyToExit = true;
            });

            _register.onProductScanned.AddListener(OnProductsScanned);
        }

        private void OnProductsScanned()
        {
            int cashRegisterNodeIndex = Controller.ExitPath.pathNodeIndexes.First();

            bool isEmpty = Controller.Inventory.Items.IsEmpty();
            bool willBeEmpty = Controller.Inventory.Items.Count - 1 <= 0;

            if (!isEmpty)
            {
                LevelManager.Instance.Money += Controller.Inventory.Items[0].SellPrice;
                Controller.Inventory.RemoveItem(0);
            }

            if (willBeEmpty)
            {
                Controller.Grid.GetNodeByIndex(cashRegisterNodeIndex).SetTempBlock(false, Controller.ID);
                Controller.TimeBar.HideBar();
                _timer.Cancel();

                readyToExit = true;
                return;
            }

            _register.InstantiateProduct(Controller.Inventory.Items[0]);
        }

        public override void OnStateUpdate()
        {
            base.OnStateUpdate();

            if (readyToExit && Controller.Grid.GetNodeFromWorldPosition(Controller.Hip.transform.position).Index ==
                Controller.TargetCashRegister.InteractionGridIndex)
            {
                FinishState();
            }

            if (_timer is null) return;
            _timer.Update(Time.deltaTime);
        }

        public override void FinishState()
        {
            Controller.TimeBar.HideBar();

            if (_register is not null && _register.onProductScanned is not null)
            {
                _register.onProductScanned?.RemoveListener(OnProductsScanned);
            }

            Controller.State = CustomerState.WalkingToExit;
        }

        protected override void HandleGrabbed()
        {
            base.HandleGrabbed();
            Controller.Movement.BlockingPoints.Add(Controller.TargetCashRegister.InteractionGridIndex);
            Controller.Grid.GetNodeByIndex(Controller.TargetCashRegister.InteractionGridIndex)
                .SetTempBlock(true, Controller.ID);
        }

        protected override void HandleReleased()
        {
            Controller.wasGrabbed = false;
            Controller.FindPathAfterGrabCoroutine(
                Controller.Grid.GetNodeByIndex(Controller.TargetCashRegister.InteractionGridIndex), true);
        }
    }
}