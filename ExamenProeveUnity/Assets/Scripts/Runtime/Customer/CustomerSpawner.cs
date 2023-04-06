using Toolbox.Attributes;
using UnityEngine;

namespace Runtime.Customer
{
    public class CustomerSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject customerPrefab;
        [SerializeField] private Transform spawnPoint;
    
        [Button(Mode = ButtonMode.EnabledInPlayMode)]
        private void SpawnPrefab()
        {
            Instantiate(customerPrefab, spawnPoint.position, Quaternion.identity);
        }
    }
}
