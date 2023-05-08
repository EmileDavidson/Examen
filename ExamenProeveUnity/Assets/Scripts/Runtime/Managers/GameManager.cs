using Toolbox.Utils.Runtime;
using UnityEngine;
using UnityEngine.Events;

namespace Runtime.Managers
{
    public class GameManager : PersistentMonoSingleton<GameManager>
    {
        public GameObject pauseMenuCanvas;
        [SerializeField] private bool _isPaused = false;

        public UnityEvent onPlayPause;

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
                onPlayPause.Invoke();
            }
        }
    }
}