﻿using System.Threading.Tasks;
using Runtime.Enums;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using Utilities.ScriptableObjects;
using Timer = Utilities.Other.Runtime.Timer;

namespace Runtime.Customer.CustomerStates
{
    public class CustomerGettingProductState : CustomerStateBase
    {
        private const int WaitTime = 1;
        private Timer _timer;

        public override void OnStateStart()
        {
            _timer = new Timer(WaitTime);
            Controller.Grid.GetNodeByIndex(Controller.CurrentTargetShelf.InteractionGridIndex).SetTempBlock(true);

            _timer.onTimerUpdate.AddListener((value) => { Controller.TimeBar.Scale = value; });

            _timer.onTimerFinished.AddListener(() =>
            {
                Controller.Grid.GetNodeByIndex(Controller.CurrentTargetShelf.InteractionGridIndex).SetTempBlock(false);
                Controller.TimeBar.HideBar();

                var removedProduct = Controller.CurrentTargetShelf.RemoveItem();
                var item = Controller.CurrentTargetShelf.Item;
                
                if (!removedProduct || item is null)
                {
                    Controller.Icon.sprite = Controller.Sprites.GetSprite(SpriteType.Sad);
                    FinishState();
                    return;
                }

                Controller.Inventory.AddItem(item);
                Controller.Icon.sprite = Controller.Sprites.GetSprite(SpriteType.Happy);

                FinishState();
            });
        }

        public override void OnStateUpdate()
        {
            base.OnStateUpdate();
            _timer.Update(Time.deltaTime);
        }

        public override void FinishState()
        {
            Controller.State = CustomerState.WalkingToCheckout;
        }

        public CustomerGettingProductState(CustomerController controller) : base(controller)
        {
        }
    }
}