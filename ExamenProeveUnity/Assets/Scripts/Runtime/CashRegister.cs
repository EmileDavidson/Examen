using System;
using Runtime.Grid.GridPathFinding;
using Runtime.Interfaces;
using UnityEngine;

public class CashRegister : MonoBehaviour, IGridable
{
    [Tooltip("The index of the grid node where the player should stand to interact with the shelf")]
    [field: SerializeField] public int gridIndex { get; set; }

    [SerializeField] private FixedPath exitPath;
    
    [SerializeField] private Transform dropOffAnchor;
    private Vector3 _dropOffSpot;

    public Vector3 DropOffSpot => _dropOffSpot;
    public int InteractionGridIndex => gridIndex;

    public FixedPath ExitPath => exitPath;

    private void Awake()
    {
        _dropOffSpot = dropOffAnchor.transform.position;
    }

}
