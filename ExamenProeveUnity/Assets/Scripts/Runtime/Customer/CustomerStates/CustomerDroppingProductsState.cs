﻿using System.Linq;
using System.Threading.Tasks;
using Toolbox.MethodExtensions;
using Unity.VisualScripting.FullSerializer;
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

        //todo: currently just waits 10 seconds but should drop an item on the cash register drop spot if the previous item was scanned.
        //todo: but this is not implemented yet.

        public override void OnStateStart()
        {
            if (Controller.Inventory.Items.IsEmpty())
            {
                FinishState();
                return;
            }

            _register = Controller.TargetCashRegister;
            int cashRegisterNodeIndex = Controller.ExitPath.pathNodeIndexes.First();
            Controller.Grid.GetNodeByIndex(cashRegisterNodeIndex).SetTempBlock(true);
            _register.InstantiateProduct(Controller.Inventory.Items[0]);
            _timer = new Timer(WaitTime);

            _timer.onTimerUpdate.AddListener((value) => { Controller.TimeBar.Scale = value; });

            _timer.onTimerFinished.AddListener(() =>
            {
                Controller.Grid.GetNodeByIndex(cashRegisterNodeIndex).SetTempBlock(false);
                Controller.TimeBar.HideBar();

                FinishState();
            });

            _register.onProductScanned.AddListener(OnProductsScanned);
        }

        private void OnProductsScanned()
        {
            int cashRegisterNodeIndex = Controller.ExitPath.pathNodeIndexes.First();

            if (!Controller.Inventory.Items.IsEmpty())
            {
                GameManager.Instance.Money += Controller.Inventory.Items[0].Price;
                Controller.Inventory.RemoveItem(0);
            }

            if (Controller.Inventory.Items.IsEmpty())
            {
                Controller.Grid.GetNodeByIndex(cashRegisterNodeIndex).SetTempBlock(false);
                Controller.TimeBar.HideBar();
                _timer.Canceled = true;
                FinishState();
            }
            else
            {
                _register.InstantiateProduct(Controller.Inventory.Items[0]);
            }
        }

        public override void OnStateUpdate()
        {
            base.OnStateUpdate();
            _timer.Update(Time.deltaTime);
        }

        public override void FinishState()
        {
            _register.onProductScanned.RemoveListener(OnProductsScanned);
            Controller.State = CustomerState.WalkingToExit;
        }
    }
}