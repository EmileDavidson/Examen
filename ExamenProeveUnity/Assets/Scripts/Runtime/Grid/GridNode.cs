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
        public Vector3Int GridPosition { get; set; }
        public int Index { get; set;  }
        public bool IsBlocked { get; set; }

        public int gCost;
        public int hCost;
        public int fCost;
        
        public GridNode cameFromNode;
        
        public GridNode(int index, int x, int y, int z, bool blockedNode = false)
        {
            GridPosition = new Vector3Int(x, y, z);
            Index = index;
            this.IsBlocked = blockedNode;
        }

        public void CalculateFCost()
        {
            fCost = gCost + hCost;
        }

        public void SetBlocked(bool doBlock)
        {
            IsBlocked = doBlock;
        }
    }
}