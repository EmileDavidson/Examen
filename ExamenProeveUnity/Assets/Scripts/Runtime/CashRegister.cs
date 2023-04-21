using System;
using Runtime;
using Runtime.Grid.GridPathFinding;
using Runtime.Interfaces;
using UnityEngine;
using UnityEngine.Events;

public class CashRegister : MonoBehaviour, IGridable
{
    [Tooltip("The index of the grid node where the player should stand to interact with the shelf")]
    [field: SerializeField] public int gridIndex { get; set; }

    [SerializeField] private FixedPath exitPath;
    
    [SerializeField] private Transform dropOffAnchor;
    private Vector3 _dropOffSpot;

    private GameObject _productToBeScanned;

    public Vector3 DropOffSpot => _dropOffSpot;
    public int InteractionGridIndex => gridIndex;

    public FixedPath ExitPath => exitPath;

    public UnityEvent onScanned = new();
    public UnityEvent onProductScanned = new();

    private void Awake()
    {
        _dropOffSpot = dropOffAnchor.transform.position;
    }

    public void InstantiateProduct(ProductScriptableObject product)
    {
        _productToBeScanned = Instantiate(product.Prefab, _dropOffSpot, Quaternion.identity);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (!collision.gameObject.CompareTag("Player")) onScanned.Invoke();
        if (collision.gameObject != _productToBeScanned) return;
        
        onProductScanned.Invoke();
        
        DestroyScannedObject();
    }

    private void DestroyScannedObject()
    {
        if (_productToBeScanned is null) return;
        
        Destroy(_productToBeScanned.gameObject);
        _productToBeScanned = null;
    }
}
