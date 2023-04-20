using System.Collections.Generic;
using Runtime.Enums;

namespace Runtime.Dictonaries
{
    public class PlayerInputActionMapDict
    {
        public static readonly Dictionary<PlayerInputActionMap, string> Dict = new Dictionary<PlayerInputActionMap, string>()
        {
            {PlayerInputActionMap.PlayerMovement, "PlayerMovement"},
            {PlayerInputActionMap.MenuController, "MenuController"},
        };
    }
}