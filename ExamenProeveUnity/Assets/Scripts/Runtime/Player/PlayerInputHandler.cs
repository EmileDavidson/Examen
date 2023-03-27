using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Runtime.Player
{
    public class PlayerInputHandler : MonoBehaviour
    {
        [SerializeField] private PlayerInput playerInput;

        public UnityEvent<Vector2> onMoveValueChanged = new UnityEvent<Vector2>();
        public UnityEvent onMoveStopped = new UnityEvent();

        private void Awake()
        {
            playerInput.actions["Movement"].performed += OnMoveValueUpdate;
            playerInput.actions["Movement"].canceled += OnMoveStopped;
        }

        private void OnMoveValueUpdate(InputAction.CallbackContext context)
        {
            onMoveValueChanged.Invoke(context.ReadValue<Vector2>());
        }
        
        private void OnMoveStopped(InputAction.CallbackContext context)
        {
            onMoveStopped.Invoke();
        }
    }
}
