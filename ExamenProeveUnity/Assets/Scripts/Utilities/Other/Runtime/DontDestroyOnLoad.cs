using UnityEngine;

namespace Utilities.Other.Runtime
{
    /// <summary>
    /// Sets don't destroy on load at awake that's it.
    /// </summary>
    public class DontDestroyOnLoad : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
 