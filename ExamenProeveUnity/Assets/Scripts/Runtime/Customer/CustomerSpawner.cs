using Toolbox.Attributes;
using UnityEngine;

namespace Runtime.Customer
{
    /// <summary>
    /// Handles customer spawning and customer setup if needed.
    /// </summary>
    public class CustomerSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject customerPrefab;
        [SerializeField] private Transform spawnPoint;
    
        /// <summary>
        /// Spawns a customer at the spawn point.
        /// </summary>
        [Button(Mode = ButtonMode.EnabledInPlayMode)]
        private void SpawnPrefab()
        {
            Instantiate(customerPrefab, spawnPoint.position, Quaternion.identity);
        }
    }
}
