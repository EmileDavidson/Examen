using Runtime.Managers;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Runtime.UserInterfaces.MainMenu
{
    public class GameStartInterface : MonoBehaviour
    {
        [SerializeField] private int minimalPlayerCount = 2;
        [SerializeField] private PlayerInputManager playerInputManager;
        [SerializeField] private Button startButton;
        
        private void Awake()
        {
            playerInputManager ??= GetComponent<PlayerInputManager>();
            startButton.onClick?.AddListener(StartButtonPressed);
        }

        /// <summary>
        /// Start button pressed is called when the start button is pressed
        /// and will switch to the game scene if enough players are connected
        /// </summary>
        public void StartButtonPressed()
        {
            if (GameManager.Instance.IsPaused) return;
            if (playerInputManager is null) return;
            if(playerInputManager.playerCount < minimalPlayerCount)
            {
                return;
            }
            playerInputManager.DisableJoining();
            startButton.interactable = false;
            
            //switch scene
            SceneManager.LoadScene("BasicStoreScene");
        }
    }
}