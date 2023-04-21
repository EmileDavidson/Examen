using System.Collections.Generic;
using Toolbox.Attributes;
using Toolbox.MethodExtensions;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(AudioSource))]
public class AudioPlayer : MonoBehaviour
{
    [SerializeField] private List<AudioClip> audioClips = new();

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = gameObject.GetOrAddComponent<AudioSource>();
    }

    [Button]
    public void PlayAudio(int audioIndex)
    {
        if (audioIndex > audioClips.Count - 1) return;
        _audioSource.PlayOneShot(audioClips[audioIndex]);
        print("played audio: " + audioClips[audioIndex].name);
    }

    public void PlayRandom()
    {
        if (audioClips.IsEmpty()) return;
        PlayAudio(Random.Range(0, audioClips.Count));
    }
}
