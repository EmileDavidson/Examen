using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Truck : MonoBehaviour
{
    [SerializeField] private int playersNeeded = 2;
    
    public UnityEvent onDoorsOpened;

    private int _playersGrabbing;

    private Grabbable _grabbable;
    
    private void Awake()
    {
        _grabbable = GetComponent<Grabbable>();
        _grabbable.OnGrabbed.AddListener(GrabUpdate);
        _grabbable.OnReleased.AddListener(GrabUpdate);
    }

    private void GrabUpdate()
    {
        _playersGrabbing = _grabbable.GrabbedBy.Count;
        if (_playersGrabbing >= playersNeeded)
        {
            onDoorsOpened.Invoke();
        }
    }
}
