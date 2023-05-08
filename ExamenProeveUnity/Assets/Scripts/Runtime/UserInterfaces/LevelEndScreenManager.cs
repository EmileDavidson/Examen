using UnityEngine;
using UnityEngine.SceneManagement;

namespace Runtime.UserInterfaces
{
    public class LevelEndScreenManager : MonoBehaviour
    {
        public void GotoMainScreen()
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}