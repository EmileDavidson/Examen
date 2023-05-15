using UnityEngine;

namespace Utilities.MethodExtensions
{
    public static class AudioSourceExtensions
    {
        public static void TogglePlaying(this AudioSource audioSource)
        {
            if (audioSource.isPlaying)
            {
                audioSource.Pause();
                return;
            }

            audioSource.Play();
        }
    }
}