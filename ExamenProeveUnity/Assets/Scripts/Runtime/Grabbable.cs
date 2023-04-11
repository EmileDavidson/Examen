using Runtime.Interfaces;
using UnityEngine;
using UnityEngine.Events;

public class Grabbable : MonoBehaviour, IGrabbable
{
    public UnityEvent onGrabbed = new UnityEvent();
    public UnityEvent onReleased = new UnityEvent();
}
