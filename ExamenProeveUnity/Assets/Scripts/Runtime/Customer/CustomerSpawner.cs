using System.Collections;
using Runtime.Customer.CustomerStates;
using Runtime.Managers;
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
        [Button]
        public void Spawn()
        {
            Instantiate(customerPrefab, spawnPoint.position, Quaternion.identity);
        }

        [Button]
        public void BatchSpawn(int spawnAmount)
        {
            StartCoroutine(TimedBatchSpawnCoroutine(spawnAmount));
        }

        private IEnumerator TimedBatchSpawnCoroutine(int spawnAmount, float spawnDelay = 1f)
        {
            for (int i = 0; i < spawnAmount; i++)
            {
                Spawn();
                yield return new WaitForSeconds(spawnDelay);
            }
        }
    }
}
