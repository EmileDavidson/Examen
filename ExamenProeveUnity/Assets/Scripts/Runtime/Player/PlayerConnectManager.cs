using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Runtime.Player
{
    /// <summary>
    /// PlayerConnectManager manages the connection of the players
    /// it will be used to manage to players when they connect and disconnect
    /// </summary>
    public class PlayerConnectManager : MonoBehaviour
    {
        [SerializeField] private PlayerInputManager playerInputManager;

        [SerializeField] private Color[] colors = new Color[2]
        {
            Color.red,
            Color.blue
        };
        
        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
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
            DontDestroyOnLoad(input.gameObject);
            
            //todo: we only have one canvas with an image underneath the player for now so we can just set the color
            //todo: but we should probably have a better way of doing this for if things change in the future.
            var indicator = input.gameObject.GetComponentInChildren<Image>();
            if (indicator == null) return;
            indicator.color = colors[input.playerIndex];
        }
    }
}
