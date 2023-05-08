using Runtime.Customer;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Runtime.Managers
{
    [ExecuteInEditMode]
    public class LevelFinishManager : MonoBehaviour
    {
        [SerializeField] private GameObject endScreenCanvas;

        private bool _lastCustomersSpawned = false;

        public void onLastCustomersSpawned()
        {
            _lastCustomersSpawned = true;
        }

        private void Awake()
        {
            endScreenCanvas.SetActive(false);
            CustomersManager.Instance.onCustomerRemoved.AddListener(HandleLevelFinished);
        }

        private void HandleLevelFinished()
        {
            if (!_lastCustomersSpawned) return;
            if (!IsLevelFinished()) return;

            endScreenCanvas.SetActive(true);
        }

        private bool IsLevelFinished()
        {
            //check if there are still customers in the store
            return !CustomersManager.Instance.AreCustomersInStore();
        }
    }
}