using Unity.VisualScripting.FullSerializer;
using UnityEngine;

namespace Runtime.Grid.GridPathFinding
{
    public class NeighbourValidationSettings
    {
        public bool CanBeTempBlocked;
        public bool CanBeBlocked;
        public bool CanBeEndPoint;
        
        public NeighbourValidationSettings(bool canBeTempBlocked = true, bool canBeBlocked = false, bool canBeEndPoint = true)
        {
            CanBeTempBlocked = canBeTempBlocked;
            CanBeBlocked = canBeBlocked;
            CanBeEndPoint = canBeEndPoint;
        }
    }
}