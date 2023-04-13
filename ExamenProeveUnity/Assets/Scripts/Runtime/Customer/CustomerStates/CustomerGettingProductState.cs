using System.Threading.Tasks;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

namespace Runtime.Customer.CustomerStates
{
    public class CustomerGettingProductState : CustomerStateBase
    {
        private const int WaitTime = 4000;

        public override async void OnStateStart()
        {
            Controller.Grid.GetNodeByIndex(Controller.CurrentTargetShelf.InteractionGridIndex).SetTempBlock(true);
            await Task.Delay(WaitTime);
            Controller.Grid.GetNodeByIndex(Controller.CurrentTargetShelf.InteractionGridIndex).SetTempBlock(false);

            var removedProduct = Controller.CurrentTargetShelf.RemoveItem();
            if (!removedProduct) FinishState();

            var item = Controller.CurrentTargetShelf.Item;
            if (item == null) FinishState();
            Controller.Inventory.AddItem(item);

            FinishState();
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