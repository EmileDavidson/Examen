using System;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

namespace Runtime.Grid
{
    /// <summary>
    /// GridNode is the object each node in the grid is made of.
    /// </summary>
    [Serializable]
    public class GridNode
    {
        /// <summary>
        /// Blocked nodes are nodes that are generally not unblocked  
        /// </summary>
        [field: SerializeField]
        public bool IsBlocked { get; private set; }

        /// <summary>
        /// TempBlocked nodes are nodes that are blocked for a short period of time 
        /// </summary>
        [field: SerializeField]
        public bool IsTempBlocked { get; private set; }
        
        /// <summary>
        /// isLocationNode is not a walkable node but is a 'end location' for pathfinding
        /// </summary>
        [field: SerializeField]
        public bool IsLocationNode { get; private set; }

        /// <summary>
        /// The grid position of the node
        /// </summary>
        [field: SerializeField]
        public Vector3Int GridPosition { get; set; }

        /// <summary>
        /// Index of the grid nodes in the grid 
        /// </summary>
        [field: SerializeField]
        public int Index { get; set; }

        [field: SerializeField]
        public List<string> TempBlockedBy { get; set; } = new List<string>();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="index"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="blockedNode"></param>
        public GridNode(int index, int x, int y, int z, bool blockedNode = false)
        {
            GridPosition = new Vector3Int(x, y, z);
            Index = index;
            this.IsBlocked = blockedNode;
        }

        /// <summary>
        /// Set the node to be blocked
        /// </summary>
        /// <param name="doBlock"></param>
        public void SetBlocked(bool doBlock)
        {
            IsBlocked = doBlock;
        }


        /// <summary>
        /// Set the tempBlocked to given value.
        /// </summary>
        /// <param name="doTempBlock"></param>
        public void SetTempBlock(bool doTempBlock, Guid id)
        {
            //remove or add the id to the list
            if(doTempBlock && !TempBlockedBy.Contains(id.ToString())) TempBlockedBy.Add(id.ToString());
            else if(!doTempBlock) TempBlockedBy.Remove(id.ToString());
            
            //set the value if we want to unblock it check if it is not blocked by another 
            if (doTempBlock)
            {
                IsTempBlocked = true;
                return;
            }
            
            IsTempBlocked = TempBlockedBy.Count > 0;
        }
        
        /// <summary>
        /// set the node locationNode value to value 
        /// </summary>
        /// <param name="isLocationNode"></param>
        public void SetLocationNode(bool isLocationNode)
        {
            IsLocationNode = isLocationNode;
        }

        public bool IsBlockedBy(Guid id)
        {
            return TempBlockedBy.Contains(id.ToString());
        }

    }
}