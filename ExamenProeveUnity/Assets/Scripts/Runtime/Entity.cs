using System;
using UnityEngine;

namespace Runtime
{
    public class Entity : MonoBehaviour
    {
        private Guid _uuid = Guid.NewGuid();

        public Guid Uuid => _uuid;
    }
}