using Runtime.Dictonaries;
using Runtime.Enums;
using Runtime.Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Runtime.UserInterfaces
{
    public class PauseMenu : MonoBehaviour
    {
        public void QuitLevel()
        {
            SceneManager.LoadScene("MainMenu");
            PlayerManager.Instance.PlayerInputs.ForEach(playerInput =>
            {
                playerInput.SwitchCurrentActionMap(PlayerInputActionMapDict.Dict[PlayerInputActionMap.PlayerMovement]);
            });
            GameManager.Instance.IsPaused = false;
        }
    
        public void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            PlayerManager.Instance.PlayerInputs.ForEach(playerInput =>
            {
                playerInput.SwitchCurrentActionMap(PlayerInputActionMapDict.Dict[PlayerInputActionMap.PlayerMovement]);
            });
            GameManager.Instance.IsPaused = false;
        }
    }
}
