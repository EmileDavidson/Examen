using System.Collections.Generic;
using Toolbox.Attributes;
using UnityEngine;
using Utilities.MethodExtensions;
using Random = UnityEngine.Random;

namespace Runtime.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioPlayer : MonoBehaviour
    {
        [SerializeField] private List<AudioClip> audioClips = new();

        private AudioSource _audioSource;

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        private void Awake()
        {
            _audioSource = gameObject.GetOrAddComponent<AudioSource>();
        }

        /// <summary>
        /// Plays the audio clip at the given index.
        /// if the index is out of range, nothing happens.
        /// </summary>
        /// <param name="audioIndex"></param>
        [Button]
        public void PlayAudio(int audioIndex)
        {
            if (audioIndex > audioClips.Count - 1) return;
            _audioSource.PlayOneShot(audioClips[audioIndex]);
        }

        /// <summary>
        /// Plays a random audio clip from the list. if there are no items play nothing.
        /// </summary>
        public void PlayRandom()
        {
            if (audioClips.IsEmpty()) return;
            PlayAudio(Random.Range(0, audioClips.Count));
        }

        /// <summary>
        /// Toggles playing the audio source. (paused, playing) 
        /// </summary>
        public void TogglePlaying()
        {
            _audioSource.TogglePlaying();
        }
    }
}