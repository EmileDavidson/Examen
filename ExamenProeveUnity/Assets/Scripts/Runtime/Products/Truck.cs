using Runtime;
using Toolbox.Attributes;
using UnityEngine;
using UnityEngine.Events;

public class Truck : MonoBehaviour
{
    [SerializeField] private int speed;
    [SerializeField] private int playersNeeded = 2;

    [SerializeField] private Vector3 startPos;
    [SerializeField] private Vector3 endPos;
    private Vector3 targetPos;

    public UnityEvent onArrive;
    public UnityEvent onDoorsOpened;
    public UnityEvent onDepart;

    private bool _atDestination = true;
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
            Depart();
        }
    }

    [Button]
    public void Arrive()
    {
        onArrive.Invoke();
        targetPos = endPos;
        _atDestination = false;
    }

    [Button]
    public void Depart()
    {
        onDepart.Invoke();
        targetPos = startPos;
        _atDestination = false;
    }

    private void Update()
    {
        if (_atDestination) return;
        if (Vector3.Distance(transform.position, targetPos) <= 1)
        {
            _atDestination = true;
            return;
        }
        
        Vector3 direction = (targetPos - transform.position).normalized;
        gameObject.transform.position += direction * speed * Time.deltaTime;
    }
}
