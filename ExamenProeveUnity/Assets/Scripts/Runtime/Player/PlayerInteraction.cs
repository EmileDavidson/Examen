using System.Collections.Generic;
using System.Linq;
using Runtime;
using UnityEngine;
using UnityEngine.InputSystem;
using Utilities.MethodExtensions;

public class PlayerInteraction : MonoBehaviour
{
    private List<Interactable> _interactables = new();
    private Interactable _activeInteractable;
    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<Interactable>(out var interactable)) return;
        _interactables.Add(interactable);
        InteractableListChange();
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.TryGetComponent<Interactable>(out var interactable)) return;
        _interactables.Remove(interactable);
        InteractableListChange();
    }

    private void InteractableListChange()
    {
        if (_interactables.IsEmpty())
        {
            _activeInteractable = null;
            return;
        }
        _interactables = _interactables.Where(interactable => interactable != null).ToList();
        
         _activeInteractable = _interactables
            .OrderBy(element => element.transform.position.DistanceXZ(this.transform.position))?.First();

    }
    
    private void OnRightShoulder(InputValue value)
    {
        if (!value.isPressed) return;
        if (_activeInteractable is null) return;
        _activeInteractable.onRightShoulderClicked.Invoke();
    }

    private void OnLeftShoulder(InputValue value)
    {
        if (!value.isPressed) return;
        if (_activeInteractable is null) return;
        _activeInteractable.onLeftShoulderClicked.Invoke();
    }

    private void OnInteractionButton(InputValue inputValue)
    {
        if (!inputValue.isPressed) return;
        if (_activeInteractable is null) return;
        _activeInteractable.onInteractionClicked.Invoke();

    }
}
