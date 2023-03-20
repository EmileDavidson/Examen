using JetBrains.Annotations;
using UnityEngine;

namespace Toolbox.Utils.Runtime
{
    public class PersistentMonoSingleton<T> : MonoBehaviour where T : PersistentMonoSingleton<T>
    {
        private static T _instance;
        private static GameObject _singletonContainer;
        
        /// <summary>
        /// The singleton instance referring to the class
        /// </summary>
        public static T Instance
        {
            get
            {
                if (_instance != null) return _instance;

                _instance = FindObjectOfType<T>();
                if (_instance != null) return _instance;

                GameObject singletonObject = new GameObject("PersistentSingleton: " + typeof(T).Name);
                singletonObject.transform.parent = GetOrCreateSingletonContainer.transform;
                _instance = singletonObject.AddComponent<T>();
                return _instance;
            }
        }

        /// <summary>
        /// Container for all singletons created if they didn't exists yet
        /// </summary>
        public static GameObject GetOrCreateSingletonContainer
        {
            get
            {
                if (_singletonContainer != null) return _singletonContainer;

                _singletonContainer = GameObject.Find("/PersistentSingletons");

                if (_singletonContainer != null) return _singletonContainer;

                _singletonContainer = new GameObject("PersistentSingletons");
                return _singletonContainer;
            }
        }

        /// <summary>
        /// start logic
        /// if the game starts and this component is not the instance of the static instance we remove it because a singleton can only have one 
        /// </summary>
        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                if (Application.isPlaying) Destroy(gameObject);
                else DestroyImmediate(gameObject);

                return;
            }

            _instance = this as T;
            DontDestroyOnLoad(_instance);
            Init();
        }

        /// <summary>
        /// Awake Initializing 
        /// </summary>
        [PublicAPI]
        public void Init()
        {
            
        }
    }
}