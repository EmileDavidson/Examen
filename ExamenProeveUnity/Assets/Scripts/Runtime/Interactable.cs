using UnityEngine;
using UnityEngine.Events;

namespace Runtime
{
    public class Interactable : MonoBehaviour
    {
        public UnityEvent onRightShoulderClicked = new();
        public UnityEvent onLeftShoulderClicked = new();
        public UnityEvent onInteractionClicked = new();
        
        public void OnRightShoulderClicked()
        {
            onRightShoulderClicked.Invoke();
        }

        public void OnLeftShoulderClicked()
        {   
            onLeftShoulderClicked.Invoke();
        }

        public void OnInteractionClicked()
        {
            onInteractionClicked.Invoke();
        }
    }
}