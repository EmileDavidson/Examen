using System.Linq;
using System.Threading.Tasks;
using Toolbox.MethodExtensions;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

namespace Runtime.Customer.CustomerStates
{
    public class CustomerDroppingProductsState : CustomerStateBase
    {
        private const int WaitTime = 4;
        private Timer _timer; 
        
        public CustomerDroppingProductsState(CustomerController controller) : base(controller)
        {
        }

        //todo: currently just waits 4 seconds but should drop an item on the cash register drop spot if the previous item was scanned.
        //todo: but this is not implemented yet.
        
        public override  void OnStateStart()
        {
            if (Controller.Inventory.Items.IsEmpty())
            {
                FinishState();
                return;
            }
            
            int cashRegisterNodeIndex = Controller.ExitPath.pathNodeIndexes.First();
            Controller.Grid.GetNodeByIndex(cashRegisterNodeIndex).SetTempBlock(true);
            _timer = new Timer(WaitTime);

            _timer.onTimerUpdate.AddListener((value) =>
            {
                Controller.TimeBar.Scale = value;
            });

            _timer.onTimerFinished.AddListener(() =>
            {
                Controller.Grid.GetNodeByIndex(cashRegisterNodeIndex).SetTempBlock(false);
                Controller.TimeBar.HideBar();

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
            Controller.State = CustomerState.FinishingShopping;
        }
    }
}