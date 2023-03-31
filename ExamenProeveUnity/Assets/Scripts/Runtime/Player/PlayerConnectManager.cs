using UnityEngine;
using UnityEngine.InputSystem;

namespace Runtime.Player
{
    /// <summary>
    /// PlayerConnectManager manages the connection of the players
    /// it will be used to manage to players when they connect and disconnect
    /// </summary>
    public class PlayerConnectManager : MonoBehaviour
    {
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
        }
        
        /// <summary>
        /// When a player connects we 'set it up' here.
        /// </summary>
        /// <param name="input"></param>
        private void OnDeviceConnect(PlayerInput input)
        {
            //we make the player persistent so we can switch to a game scene without losing the player
            DontDestroyOnLoad(input.gameObject);
        }
    }
}
