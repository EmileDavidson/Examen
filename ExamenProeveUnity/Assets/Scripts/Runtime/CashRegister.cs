using System;
using Runtime.Grid.GridPathFinding;
using UnityEngine;

public class CashRegister : MonoBehaviour
{
    [Tooltip("The index of the grid node where the player should stand to interact with the shelf")]
    [SerializeField] private int gridIndex;

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
