using Runtime.Managers;
using Toolbox.MethodExtensions;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

namespace Runtime.Customer.CustomerStates
{
    public class CustomerSpawnedState: CustomerStateBase
    {
        public override void OnStateStart()
        {
            var targetShelf = WorldManager.Instance.shelves.RandomItem();
            Controller.CurrentTargetShelf = targetShelf;
            
            Controller.Icon.sprite = Controller.CurrentTargetShelf.Item.Icon; 
            
            FinishState();
        }

        public override void FinishState()
        {
            Controller.State = CustomerState.WalkingToEntrance;
        }

        public CustomerSpawnedState(CustomerController controller) : base(controller)
        {
        }
    }
}