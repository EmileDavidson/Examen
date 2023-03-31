using System.Collections.Generic;
using TMPro;
using Toolbox.MethodExtensions;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Runtime.UserInterfaces.MainMenu
{
    /// <summary>
    /// PlayerConnectionRenderer renders the connection status of the players
    /// </summary>
    public class PlayerConnectionRenderer : MonoBehaviour
    {
        [SerializeField] private List<TMP_Text> connectionTexts = new List<TMP_Text>();
        [SerializeField] private PlayerInputManager playerInputManager;

        private void Awake()
        {
            playerInputManager ??= GetComponent<PlayerInputManager>();
            if (playerInputManager == null)
            {
                Debug.LogWarning("Player Input Manager not found");
                return;
            }
            
            playerInputManager.playerJoinedEvent.AddListener(OnDeviceConnect);
            playerInputManager.playerLeftEvent.AddListener(OnDeviceDisconnect);
        }

        private void OnDeviceConnect(PlayerInput input)
        {
            int index = input.playerIndex;
            var device = input.devices[0]?.device;
            if (!connectionTexts.ContainsSlot(index))
            {
                Debug.LogWarning("Not enough connection texts to display all players");
                return;
            }
            connectionTexts[index].text = device?.displayName ?? "Unknown";
        }
        
        private void OnDeviceDisconnect(PlayerInput input)
        {
            int index = input.playerIndex;
            if (!connectionTexts.ContainsSlot(index))
            {
                Debug.LogWarning("Could not change connection text because the index is out of range");
                return;
            }
            connectionTexts[index].text = "Disconnected";
        }
    }
}