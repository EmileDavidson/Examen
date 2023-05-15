using System.Collections.Generic;
using Toolbox.Attributes;
using UnityEngine;

namespace Utilities.Other.Runtime
{
    public class ResolverIteratorChanger : MonoBehaviour
    {
        [SerializeField] private bool includeChildren;
        [SerializeField] private bool clearOnAwake;
        [SerializeField] private int newIterationCount = 200;
        [SerializeField] private List<Rigidbody> rigidBodiesToChange = new List<Rigidbody>();
   
        private void Awake()
        {
            GetRigidBodies(clearOnAwake);
            HandleRigidbodies();
        }

        [Button(Mode = ButtonMode.EnabledInPlayMode)]
        private void HandleRigidbodies()
        {
            if (rigidBodiesToChange is null || rigidBodiesToChange.Count <= 0) return;
            foreach (var rigid in rigidBodiesToChange)
            {
                rigid.solverIterations = newIterationCount;
            }
        }

        [Button]
        private void GetRigidBodies(bool clearOldList = false)
        {
            if(clearOldList) rigidBodiesToChange.Clear();
        
            var foundBodies = includeChildren ? GetComponentsInChildren<Rigidbody>() : GetComponents<Rigidbody>();
            foreach (var foundBody in foundBodies)
            {
                if(rigidBodiesToChange.Contains(foundBody)) continue;
                rigidBodiesToChange.Add(foundBody);
            }
        }
    }
}
