using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Runtime.Player
{
    /// <summary>
    /// Player input handler is the 'wrapper' for the PlayerInput class. it is used to handle the input events and invoke the UnityEvents.
    /// for other classes to use instead of keeping track of the 'string key names' and the 'InputAction.CallbackContext' objects.
    /// </summary>
    public class PlayerInputHandler : MonoBehaviour
    {
        [SerializeField] private PlayerInput playerInput;

        public UnityEvent<Vector2> onMoveValueChanged = new UnityEvent<Vector2>();
        public UnityEvent onMoveCanceled = new UnityEvent();

        private void Awake()
        {
            playerInput.actions["Movement"].performed += OnMoveValueUpdate;
            playerInput.actions["Movement"].canceled += OnMoveCanceled;
        }

        private void OnMoveValueUpdate(InputAction.CallbackContext context)
        {
            onMoveValueChanged.Invoke(context.ReadValue<Vector2>());
        }
        
        private void OnMoveCanceled(InputAction.CallbackContext context)
        {
            onMoveCanceled.Invoke();
        }
    }
}
