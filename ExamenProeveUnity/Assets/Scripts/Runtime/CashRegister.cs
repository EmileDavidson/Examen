using System;
using UnityEngine;

public class CashRegister : MonoBehaviour
{
    [SerializeField] private Vector2Int interactPosition;
    [SerializeField] private GameObject dropOffAnchor;
    private Vector3 _dropOffSpot;

    public Vector2Int InteractPosition => interactPosition;
    public Vector3 DropOffSpot => _dropOffSpot;

    private void Awake()
    {
        _dropOffSpot = dropOffAnchor.transform.position;
    }
}
