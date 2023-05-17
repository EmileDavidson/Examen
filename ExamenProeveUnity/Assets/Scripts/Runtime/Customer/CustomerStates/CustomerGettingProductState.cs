using UnityEngine;
using Utilities.Other.Runtime.Timer;

namespace Runtime.Customer.CustomerStates
{
    public class CustomerGettingProductState : CustomerStateBase
    {
        private const int WaitTime = 1;
        private Timer _timer;

        public override void OnStateStart()
        {
            Controller.Movement.WantsToMove = false;
            _timer = new Timer(WaitTime);
            Controller.Grid.GetNodeByIndex(Controller.CurrentTargetShelf.InteractionGridIndex).SetTempBlock(true, Controller.ID);

            _timer.onTimerUpdate.AddListener((value) => { Controller.TimeBar.Scale = value; });

            _timer.onTimerFinished.AddListener(() =>
            {
                Controller.Grid.GetNodeByIndex(Controller.CurrentTargetShelf.InteractionGridIndex).SetTempBlock(false, Controller.ID);
                Controller.TimeBar.HideBar();

                var removedProduct = Controller.CurrentTargetShelf.RemoveItem();
                var item = Controller.CurrentTargetShelf.Item;

                if (!removedProduct || item is null)
                {
                    Controller.EmojiType = Controller.EmojiSprites.GetPrevious(Controller.EmojiType, 2);
                    FinishState();
                    return;
                }

                Controller.Inventory.AddItem(item);
                Controller.EmojiType = Controller.EmojiSprites.GetNext(Controller.EmojiType);

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