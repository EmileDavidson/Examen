using Unity.VisualScripting.FullSerializer;
using UnityEngine;

namespace Runtime.Grid.GridPathFinding
{
    public class PathFindingSettings
    {
        public readonly bool CanBeTempBlocked;
        public readonly bool CanBeBlocked;
        public readonly bool CanBeEndPoint;
        
        public PathFindingSettings(bool canBeTempBlocked = true, bool canBeBlocked = false, bool canBeEndPoint = true)
        {
            CanBeTempBlocked = canBeTempBlocked;
            CanBeBlocked = canBeBlocked;
            CanBeEndPoint = canBeEndPoint;
        }
    }
}