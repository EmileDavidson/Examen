using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Runtime
{
    public class SoundSettings : MonoBehaviour
    {
        [SerializeField] private AudioMixer masterMixer;
    
        [SerializeField] private Slider masterSlider;
        [SerializeField] private Slider backgroundSlider;
        [SerializeField] private Slider effectsSlider;
    
    
        //Keys
        private const string MasterVolumeKey = "MasterVolume";
        private const string BackgroundVolumeKey = "BackgroundVolume";
        private const string EffectsVolumeKey = "EffectsVolume";

        private void Awake()
        {
            //if no player prefs are set, set them to 100
            if (!PlayerPrefs.HasKey(MasterVolumeKey))
            {
                PlayerPrefs.SetFloat(MasterVolumeKey, 100);
            }
            if (!PlayerPrefs.HasKey(BackgroundVolumeKey))
            {
                PlayerPrefs.SetFloat(BackgroundVolumeKey, 100);
            }
            if (!PlayerPrefs.HasKey(EffectsVolumeKey))
            {
                PlayerPrefs.SetFloat(EffectsVolumeKey, 100);
            }
        
            masterSlider.onValueChanged.AddListener(SetMasterVolume);
            backgroundSlider.onValueChanged.AddListener(SetBackgroundVolume);
            effectsSlider.onValueChanged.AddListener(SetEffectsVolume);
        
            UpdateAllVolumes();
        }

        public void SetMasterVolume(float value)
        {
            if (value < 1)
            {
                value = .001f;
            }
        
            PlayerPrefs.SetFloat(MasterVolumeKey, value);
            masterMixer.SetFloat(MasterVolumeKey, Mathf.Log10(value / 100)  * 20);
            RefreshSliders();
        }

        private void SetBackgroundVolume(float value)
        {
            if (value < 1)
            {
                value = .001f;
            }
        
            PlayerPrefs.SetFloat(BackgroundVolumeKey, value);
            masterMixer.SetFloat(BackgroundVolumeKey, Mathf.Log10(value / 100)  * 20);
            RefreshSliders();
        }

        private void SetEffectsVolume(float value)
        {
            if (value < 1)
            {
                value = .001f;
            }
        
            PlayerPrefs.SetFloat(EffectsVolumeKey, value);
            masterMixer.SetFloat(EffectsVolumeKey, Mathf.Log10(value / 100)  * 20);
            RefreshSliders();
        }

        private void UpdateAllVolumes()
        {
            SetMasterVolume(PlayerPrefs.GetFloat(MasterVolumeKey));
            SetBackgroundVolume(PlayerPrefs.GetFloat(BackgroundVolumeKey));
            SetEffectsVolume(PlayerPrefs.GetFloat(EffectsVolumeKey));
        
            RefreshSliders();
        }

        private void RefreshSliders()
        {
            masterSlider.SetValueWithoutNotify(PlayerPrefs.GetFloat(MasterVolumeKey));  
            backgroundSlider.SetValueWithoutNotify(PlayerPrefs.GetFloat(BackgroundVolumeKey));
            effectsSlider.SetValueWithoutNotify(PlayerPrefs.GetFloat(EffectsVolumeKey));
        }

    }
}
