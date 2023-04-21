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
            if (gameManager.IsPaused) PauseMusic();
            else PlayMusic();
        });
    }

    private void PlayMusic()
    {
        if (_audioSource.clip is null)
        {
            if (musicClips.Count >= 1) _audioSource.clip = musicClips[0];
        }
        _audioSource.Play();
    }

    private void PauseMusic()
    {
        _audioSource.Pause();
    }
}
