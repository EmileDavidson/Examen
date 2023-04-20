using Toolbox.Utils.Runtime;
using UnityEngine;

namespace Runtime.Managers
{
    public class GameManager : PersistentMonoSingleton<GameManager>
    {
        public GameObject pauseMenuCanvas;
        [SerializeField] private bool _isPaused = false;

        protected override void Awake()
        {
            base.Awake();
            IsPaused = false;
        }


        public bool IsPaused
        {
            get => _isPaused;
            set
            {
                pauseMenuCanvas.SetActive(value);
                _isPaused = value;
            }
        }
    }
}