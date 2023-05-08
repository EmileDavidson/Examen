using System.Collections;
using System.Collections.Generic;
using Runtime.Managers;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private List<AudioClip> musicClips = new();
    
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = gameObject.GetOrAddComponent<AudioSource>();
        gameManager.onPlayPause.AddListener(() =>
        {
            if (gameManager.IsPaused)
            {
                PauseMusic();
                return;
            }
            PlayMusic();
        });
    }

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

    private void PauseMusic()
    {
        _audioSource.Pause();
    }
}
