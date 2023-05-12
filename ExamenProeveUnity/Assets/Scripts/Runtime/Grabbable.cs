using System;
using System.Collections.Generic;
using Runtime;
using Runtime.Interfaces;
using UnityEngine;
using UnityEngine.Events;

public class Grabbable : MonoBehaviour, IGrabbable
{
    [SerializeField] private bool snapToPivot = false;
    
    public readonly UnityEvent OnGrabbed = new UnityEvent();
    public readonly UnityEvent OnReleased = new UnityEvent();

    private List<Guid> _grabbedBy = new();

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

    public void AddGrabbedBy(Guid entity)
    {
        if (_grabbedBy.Contains(entity)) return;
        _grabbedBy.Add(entity);
    }
    
    public void RemoveGrabbedBy(Guid entity)
    {
        _grabbedBy.Remove(entity);
    }

    public bool SnapToPivot => snapToPivot;

    public List<Guid> GrabbedBy => _grabbedBy;
}
