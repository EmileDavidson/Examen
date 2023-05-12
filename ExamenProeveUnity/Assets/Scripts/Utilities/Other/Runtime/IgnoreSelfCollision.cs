using System;
using Toolbox.Attributes;
using Toolbox.MethodExtensions;
using UnityEngine;

namespace Utilities.Other.Runtime
{
    public class IgnoreSelfCollision : MonoBehaviour
    {
        [SerializeField] private bool includeChildren;
        [SerializeField] private Collider[] collidersToIgnore;

        private void Awake()
        {
            collidersToIgnore = includeChildren ? GetComponentsInChildren<Collider>() : GetComponents<Collider>();

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