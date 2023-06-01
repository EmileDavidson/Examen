using Runtime;
using Runtime.Managers;
using Toolbox.Attributes;
using UnityEngine;
using UnityEngine.Events;

public class Truck : MonoBehaviour
{
    [SerializeField] private int speed;
    [SerializeField] private int playersNeeded = 2;
    [SerializeField] private bool usePlayerCount = true; 

    [SerializeField] private Vector3 startPos;
    [SerializeField] private Vector3 endPos;
    [SerializeField] private ProductOrdering productOrdering;
    private Vector3 _targetPos;

    private bool _atDestination = true;
    private int _playersGrabbing;
    private Grabbable _grabbable;
    
    public UnityEvent onArrive;
    public UnityEvent onDoorsOpened;
    public UnityEvent onDepart;
    
    private void Awake()
    {
        _grabbable = GetComponent<Grabbable>();
        _grabbable.OnGrabbed.AddListener(GrabUpdate);
        _grabbable.OnReleased.AddListener(GrabUpdate);
        productOrdering.onDeliveryDone.AddListener(Depart);
        
        playersNeeded = (usePlayerCount) ? PlayerManager.Instance.Players.Count : playersNeeded;
    }
    

    private void GrabUpdate()
    {
        _playersGrabbing = _grabbable.GrabbedBy.Count;
        if (_playersGrabbing >= playersNeeded)
        {
            onDoorsOpened.Invoke();
        }
    }

    [Button]
    public void Arrive()
    {
        onArrive.Invoke();
        _targetPos = endPos;
        _atDestination = false;
    }

    [Button]
    public void Depart()
    {
        onDepart.Invoke();
        _targetPos = startPos;
        _atDestination = false;

        _grabbable.CanBeGrabbed = false;
    }

    private void Update()
    {
        if (_atDestination) return;
        if (Vector3.Distance(transform.position, _targetPos) <= 1)
        {
            _grabbable.enabled = true;
            _atDestination = true;
            _grabbable.CanBeGrabbed = true;
            return;
        }
        
        //todo: should use physics (rigidbody) instead of transform for movement 
        //todo: so that it pushes players out of the way
        _grabbable.enabled = false;
        Vector3 direction = (_targetPos - transform.position).normalized;
        gameObject.transform.position += direction * speed * Time.deltaTime;
    }
}
