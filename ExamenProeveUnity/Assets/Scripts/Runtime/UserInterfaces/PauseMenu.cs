using System;
using Runtime.Dictonaries;
using Runtime.Enums;
using Runtime.Managers;
using Runtime.Player;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Runtime.UserInterfaces
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] private PlayerInputManager playerInputManager;
        
        private void OnEnable()
        {
            playerInputManager.playerJoinedEvent.AddListener((PlayerInput input) =>
            {
                if (!input.TryGetComponent<PlayerInputEvents>(out var eventComp)) return;
                eventComp.onRightShoulder.AddListener(RestartGame);
                eventComp.onLeftShoulder.AddListener(QuitLevel);
            });
        }

        public void QuitLevel()
        {
            if (!GameManager.Instance.IsPaused) return;
            
            SceneManager.LoadScene("MainMenu");
            PlayerManager.Instance.PlayerInputs.ForEach(playerInput =>
            {
                playerInput.SwitchCurrentActionMap(PlayerInputActionMapDict.Dict[PlayerInputActionMap.PlayerMovement]);
            });
            GameManager.Instance.IsPaused = false;
        }
    
        public void RestartGame()
        {
            if (!GameManager.Instance.IsPaused) return;
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            PlayerManager.Instance.PlayerInputs.ForEach(playerInput =>
            {
                playerInput.SwitchCurrentActionMap(PlayerInputActionMapDict.Dict[PlayerInputActionMap.PlayerMovement]);
            });
            GameManager.Instance.IsPaused = false;
        }
    }
}
