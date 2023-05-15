using Toolbox.Attributes;
using UnityEngine;

namespace Utilities.Other.Runtime
{
    public class LookAtCamera : MonoBehaviour
    {
        [SerializeField] private bool runOnAwake = true;
        [SerializeField] private bool runOnUpdate = true;
        [SerializeField] private Camera mainCamera;

        public bool lookX;
        public bool lookY;
        public bool lookZ;

        /// <summary>
        /// Tries to get the main camera if it's null it will log an error and destroy itself
        /// if it it found one it will run the look function if bool is true
        /// </summary>
        private void Awake()
        {
            if (mainCamera == null) mainCamera = Camera.main;
            if (mainCamera != null)
            {
                if (runOnAwake) Look();
                return;
            }

            Debug.LogError("Camera not found");
            Destroy(this);

        }

        /// <summary>
        /// Runs on update if bool is true 
        /// </summary>
        private void Update()
        {
            if (!runOnUpdate) return;
            Look();
        }

        /// <summary>
        /// Rotate to look at camera x,y,z if bool is true
        /// </summary>
        [Button]
        private void Look()
        {
            //rotate to look at camera x,y,z if bool is true
            transform.rotation = Quaternion.Euler(
                lookX ? mainCamera.transform.rotation.eulerAngles.x : transform.rotation.eulerAngles.x,
                lookY ? mainCamera.transform.rotation.eulerAngles.y : transform.rotation.eulerAngles.y,
                lookZ ? mainCamera.transform.rotation.eulerAngles.z : transform.rotation.eulerAngles.z
            );
        }
    }
}