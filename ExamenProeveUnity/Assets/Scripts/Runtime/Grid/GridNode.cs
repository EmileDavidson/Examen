using System;
using UnityEngine;

namespace Runtime.Grid
{
    /// <summary>
    /// GridNode is the object each node in the grid is made of.
    /// </summary>
    [Serializable]
    public class GridNode
    {
        [field: SerializeField] public bool IsBlocked { get; set; }
        [field: SerializeField] public  Vector3Int GridPosition {get; set;}
        [field: SerializeField] public  int Index {get; set;}

        public GridNode(int index, int x, int y, int z, bool blockedNode = false)
        {
            GridPosition = new Vector3Int(x, y, z);
            Index = index;
            this.IsBlocked = blockedNode;
        }

        public void SetBlocked(bool doBlock)
        {
            IsBlocked = doBlock;
        }
    }
}