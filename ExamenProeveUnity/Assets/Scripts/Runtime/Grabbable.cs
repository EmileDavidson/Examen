using Runtime.Interfaces;
using UnityEngine;
using UnityEngine.Events;

public class Grabbable : MonoBehaviour, IGrabbable
{
    [SerializeField] private bool snapToPivot = false;
    
    public readonly UnityEvent OnGrabbed = new UnityEvent();
    public readonly UnityEvent OnReleased = new UnityEvent();

    private bool _isGrabbed = false;
    public bool IsGrabbed => _isGrabbed;

    private void Awake()
    {
        _isGrabbed = false;
        
        OnGrabbed?.AddListener(() =>
        {
            _isGrabbed = true;
        });
        OnReleased?.AddListener(() =>
        {
            _isGrabbed = false;
        });
    }

    public bool SnapToPivot => snapToPivot;
}
