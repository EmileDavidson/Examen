using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Runtime
{
    /// <summary>
    /// This class is responsible for the sound settings in the game and is used to set the volume of the master, background and effects
    /// This class also makes sure that the volume is saved in the player prefs and updated using the sliders in the settings menu
    /// </summary>
    public class SoundSettings : MonoBehaviour
    {
        [SerializeField] private AudioMixer masterMixer;
        [SerializeField] private Slider masterSlider;
        [SerializeField] private Slider backgroundSlider;
        [SerializeField] private Slider effectsSlider;

        private const string MasterVolumeKey = "MasterVolume";
        private const string BackgroundVolumeKey = "BackgroundVolume";
        private const string EffectsVolumeKey = "EffectsVolume";

        /// <summary>
        /// Awake makes sure that all slider values exist in the player prefs and links all events to their respective functions
        /// </summary>
        private void Awake()
        {
            if (!PlayerPrefs.HasKey(MasterVolumeKey)) PlayerPrefs.SetFloat(MasterVolumeKey, 100);
            if (!PlayerPrefs.HasKey(BackgroundVolumeKey)) PlayerPrefs.SetFloat(BackgroundVolumeKey, 100);
            if (!PlayerPrefs.HasKey(EffectsVolumeKey)) PlayerPrefs.SetFloat(EffectsVolumeKey, 100);

            masterSlider.onValueChanged.AddListener(SetMasterVolume);
            backgroundSlider.onValueChanged.AddListener(SetBackgroundVolume);
            effectsSlider.onValueChanged.AddListener(SetEffectsVolume);

            UpdateAllVolumes();
        }

        /// <summary>
        /// Sets the master volume to the given value
        /// </summary>
        /// <param name="value"></param>
        public void SetMasterVolume(float value)
        {
            if (value < 1)
            {
                value = .001f;
            }

            PlayerPrefs.SetFloat(MasterVolumeKey, value);
            masterMixer.SetFloat(MasterVolumeKey, Mathf.Log10(value / 100) * 20);
            RefreshSliders();
        }

        /// <summary>
        /// Sets the background volume to the given value
        /// </summary>
        /// <param name="value"></param>
        private void SetBackgroundVolume(float value)
        {
            if (value < 1)
            {
                value = .001f;
            }

            PlayerPrefs.SetFloat(BackgroundVolumeKey, value);
            masterMixer.SetFloat(BackgroundVolumeKey, Mathf.Log10(value / 100) * 20);
            RefreshSliders();
        }

        /// <summary>
        /// Sets the effects volume to the given value
        /// </summary>
        /// <param name="value"></param>
        private void SetEffectsVolume(float value)
        {
            if (value < 1)
            {
                value = .001f;
            }

            PlayerPrefs.SetFloat(EffectsVolumeKey, value);
            masterMixer.SetFloat(EffectsVolumeKey, Mathf.Log10(value / 100) * 20);
            RefreshSliders();
        }

        /// <summary>
        /// Update the mixer values to the player prefs values and refresh the sliders
        /// </summary>
        private void UpdateAllVolumes()
        {
            SetMasterVolume(PlayerPrefs.GetFloat(MasterVolumeKey));
            SetBackgroundVolume(PlayerPrefs.GetFloat(BackgroundVolumeKey));
            SetEffectsVolume(PlayerPrefs.GetFloat(EffectsVolumeKey));

            RefreshSliders();
        }

        /// <summary>
        /// Refreshes the sliders to the player prefs values
        /// </summary>
        private void RefreshSliders()
        {
            masterSlider.SetValueWithoutNotify(PlayerPrefs.GetFloat(MasterVolumeKey));
            backgroundSlider.SetValueWithoutNotify(PlayerPrefs.GetFloat(BackgroundVolumeKey));
            effectsSlider.SetValueWithoutNotify(PlayerPrefs.GetFloat(EffectsVolumeKey));
        }
    }
}