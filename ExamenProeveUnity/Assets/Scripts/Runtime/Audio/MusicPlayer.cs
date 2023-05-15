using System.Collections.Generic;
using Runtime.Managers;
using Unity.VisualScripting;
using UnityEngine;

namespace Runtime.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class MusicPlayer : MonoBehaviour
    {
        [SerializeField] private GameManager gameManager;
        [SerializeField] private List<AudioClip> musicClips = new();
    
        private AudioSource _audioSource;

        /// <summary>
        /// Initializes the music player. 
        /// </summary>
        private void Awake()
        {
            _audioSource = gameObject.GetOrAddComponent<AudioSource>();
            gameManager.onPlayPause.AddListener(OnGamePauseChanged);
        }

        /// <summary>
        /// When the game is paused or unpaused, the music will be paused or played.
        /// </summary>
        private void OnGamePauseChanged()
        {
            if (!gameManager.IsPaused)
            {
                PlayMusic();
                return;
            }
            PauseMusic();
        }

        /// <summary>
        /// Plays the active music clip if there is no active clip. get the first clip in the list. 
        /// </summary>
        private void PlayMusic()
        {
            bool hasAudioClips = musicClips.Count >= 1;
            bool hasActiveClip = _audioSource.clip is not null;

            if(hasAudioClips && !hasActiveClip) {
                _audioSource.clip = musicClips[0];
            }

            if(_audioSource.clip is null) return;
            _audioSource.Play();
        }

        /// <summary>
        /// Pauses the music clip.
        /// </summary>
        private void PauseMusic()
        {
            _audioSource.Pause();
        }
    }
}
