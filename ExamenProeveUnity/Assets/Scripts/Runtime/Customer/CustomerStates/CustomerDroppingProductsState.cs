using System.Linq;
using Runtime.Managers;
using Toolbox.MethodExtensions;
using UnityEngine;
using Utilities.Other.Runtime;

namespace Runtime.Customer.CustomerStates
{
    public class CustomerDroppingProductsState : CustomerStateBase
    {
        private const int WaitTime = 10;
        private Timer _timer;
        private CashRegister _register;

        public CustomerDroppingProductsState(CustomerController controller) : base(controller)
        {
        }

        public override void OnStateStart()
        {
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
                Controller.Grid.GetNodeByIndex(cashRegisterNodeIndex).SetTempBlock(false, Controller.ID);
                Controller.TimeBar.HideBar();

                Controller.EmojiType = Controller.EmojiSprites.GetPrevious(Controller.EmojiType);

                FinishState();
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
                _timer.Canceled = true;
                FinishState();
                return;
            }

            _register.InstantiateProduct(Controller.Inventory.Items[0]);
        }

        public override void OnStateUpdate()
        {
            base.OnStateUpdate();
            if (_timer is null) return;
            _timer.Update(Time.deltaTime);
        }

        public override void FinishState()
        {
            if (_register is not null && _register.onProductScanned is not null)
            {
                _register.onProductScanned?.RemoveListener(OnProductsScanned);
            }

            Controller.State = CustomerState.WalkingToExit;
        }
    }
}