using System.Collections.Generic;
using System.Linq;
using Toolbox.Attributes;
using UnityEngine;
using Utilities.MethodExtensions;

namespace Utilities.Other.Runtime
{
    /// <summary>
    /// Sets the Physics.IgnoreCollision for all colliders found or requested
    /// the ignore is run both ways so if you have 2 colliders it will ignore both ways.
    /// </summary>
    public class IgnoreSelfCollision : MonoBehaviour
    {
        [SerializeField] private bool includeChildren;
        [SerializeField] private List<Collider> collidersToIgnore;
        [SerializeField] private bool clearOnAwake;

        private void Awake()
        {
            GetColliders(clearOnAwake);
            HandleIgnoreList();
        }

        [Button]
        private void GetColliders(bool clearOldList = false)
        {
            if (clearOldList) collidersToIgnore.Clear();

            var foundColliders = includeChildren ? GetComponentsInChildren<Collider>() : GetComponents<Collider>();
            foreach (var foundCollider in foundColliders)
            {
                if (collidersToIgnore.Contains(foundCollider)) continue;
                collidersToIgnore.Add(foundCollider);
            }
        }

        [Button(Mode = ButtonMode.EnabledInPlayMode)]
        private void HandleIgnoreList()
        {
            if (collidersToIgnore is null || collidersToIgnore.IsEmpty()) return;

            foreach (var col in collidersToIgnore)
            {
                foreach (var otherCollider in collidersToIgnore)
                {
                    if (col == otherCollider) continue;
                    Physics.IgnoreCollision(col, otherCollider, true);
                }
            }
        }
    }
}