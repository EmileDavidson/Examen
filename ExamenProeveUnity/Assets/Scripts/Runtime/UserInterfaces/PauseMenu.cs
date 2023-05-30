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
            foreach (var input in PlayerManager.Instance.GetInputs(true))
            {
                if (!input.TryGetComponent<PlayerInputEvents>(out var eventComp)) continue;
                eventComp.onRightShoulder.AddListener(RestartGame);
                eventComp.onLeftShoulder.AddListener(QuitLevel);
            }
        }

        private void OnDisable()
        {
            foreach (var input in PlayerManager.Instance.GetInputs())
            {
                if (!input.TryGetComponent<PlayerInputEvents>(out var eventComp)) continue;
                eventComp.onRightShoulder.RemoveListener(RestartGame);
                eventComp.onLeftShoulder.RemoveListener(QuitLevel);
            }
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
