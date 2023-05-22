using UnityEngine;

namespace Runtime.Customer.CustomerStates
{
    public abstract class CustomerStateBase
    {
        [field: SerializeField] public CustomerController Controller { get; set; }

        /// <summary>
        /// When the state is entered, this method is called.
        /// </summary>
        public virtual void OnStateStart()
        {
        }

        /// <summary>
        /// Gets called every frame while the state is active
        /// </summary>
        public virtual void OnStateUpdate()
        {
        }

        /// <summary>
        /// Should be called to finish the state.
        /// </summary>
        public abstract void FinishState();

        /// <summary>
        /// When the customer is grabbed, this method is called.
        /// </summary>
        public virtual void OnGrabbed()
        {
            if (!Controller.IsBeingGrabbed() || Controller.wasGrabbed) return;
            HandleGrabbed();
        }

        /// <summary>
        /// When the customer is released, this method is called.
        /// </summary>
        public virtual void OnReleased()
        {
            if (Controller.IsBeingGrabbed() || !Controller.wasGrabbed) return;
            HandleReleased();
        }

        protected virtual void HandleReleased()
        {
            Controller.wasGrabbed = false;
            if (Controller.Movement.WantsToMove == false) return;
            Controller.FindPathAfterGrabCoroutine();
        }

        protected virtual void HandleGrabbed()
        {
            foreach (var movementBlockingPoint in Controller.Movement.BlockingPoints)
            {
                Controller.Grid.GetNodeByIndex(movementBlockingPoint).SetTempBlock(false, Controller.ID);
            }

            Controller.Movement.BlockingPoints.Clear();
            Controller.Movement.CanMove = false;
            Controller.wasGrabbed = true;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="controller"></param>
        protected CustomerStateBase(CustomerController controller)
        {
            Controller = controller;
        }
    }
}