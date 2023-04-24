using System.Collections.Generic;
using System.Linq;
using Runtime;
using Toolbox.MethodExtensions;
using UnityEngine;
using UnityEngine.InputSystem;

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
            .OrderBy(element => DistanceXZ(element.transform.position, this.transform.position))?.First();

    }
    
    private void OnRightShoulder(InputValue value)
    {
        if (!value.isPressed) return;
        if (_activeInteractable is null) return;
        _activeInteractable.OnRightShoulderClicked();
    }

    private void OnLeftShoulder(InputValue value)
    {
        if (!value.isPressed) return;
        if (_activeInteractable is null) return;
        _activeInteractable.OnLeftShoulderClicked();
    }

    private void OnInteractionButton(InputValue inputValue)
    {
        if (!inputValue.isPressed) return;
        if (_activeInteractable is null) return;
        _activeInteractable.OnInteractionClicked();

    }
    
    //todo: move this to helper class or method extensions
    public float DistanceXZ(Vector3 first, Vector3 second)
    {
        Vector2 firstVector2 = new Vector2(first.x, first.z);
        Vector2 secondVector2 = new Vector2(second.x, second.z);

        return Vector2.Distance(firstVector2, secondVector2);
    }
}
