using System;
using UnityEngine;

namespace Runtime
{
    public class PositionFollower : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private Vector3 offset;
        [SerializeField] private float smoothSpeed = 0.125f;
    
        [SerializeField] private bool trackX = true;
        [SerializeField] private bool trackY = true;
        [SerializeField] private bool trackZ = true;
        
        [SerializeField] private bool moveInstantly = true;
        
        [SerializeField] private float maxDistanceAway = 0.1f;

        private Transform _myTransform;

        private void Awake()
        {
            _myTransform = transform;
        }

        /// <summary>
        /// Follows the target's position.
        /// </summary>
        void FixedUpdate()
        {
            var position = _myTransform.position;
            
            float desiredX = trackX ? target.position.x + offset.x : transform.position.x;
            float desiredY = trackY ? target.position.y + offset.y : transform.position.y;
            float desiredZ = trackZ ? target.position.z + offset.z : transform.position.z;
            
            Vector3 desiredPosition = new Vector3(desiredX, desiredY, desiredZ);
            Vector3 smoothedPosition = Vector3.Lerp(position, desiredPosition, smoothSpeed);
            
            if (moveInstantly)
            {
                transform.position = desiredPosition;
                return;
            }
            
            transform.position = smoothedPosition;
        }

        /// <summary>
        /// The offset from the target's position.
        /// </summary>
        public Vector3 Offset
        {
            get => offset;
            set => offset = value;
        }
    }
}