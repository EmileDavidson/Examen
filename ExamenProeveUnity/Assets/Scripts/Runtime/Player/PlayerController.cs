using Runtime.Dictonaries;
using Runtime.Enums;
using Runtime.Managers;
using Toolbox.Attributes;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Runtime.Player
{
    /// <summary>
    /// Player controller handles the 'main' things like going in to a menu or out of a menu.
    /// but also what ActionMap it should use.
    /// </summary>
    public class PlayerController : MonoBehaviour
    {
        [Button]
        private void SwitchToActionMap(PlayerInputActionMap map)
        {
            PlayerManager.Instance.PlayerInputs.ForEach(playerInput =>
            {
                playerInput.SwitchCurrentActionMap(PlayerInputActionMapDict.Dict[map]);
            });
        }

        private void OnOpenMenu(InputValue inputValue)
        {
            SwitchToActionMap(PlayerInputActionMap.MenuController);
            GameManager.Instance.IsPaused = true;
        }

        private void OnCloseMenu(InputValue inputValue)
        {
            SwitchToActionMap(PlayerInputActionMap.PlayerMovement);
            GameManager.Instance.IsPaused = false;
        }
    }
}