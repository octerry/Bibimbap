using System;
using UnityEngine;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{
    [SerializeField] private Slider _globalVolumeSlider;
    [SerializeField] private Slider _soundFxVolumeSlider;
    [SerializeField] private Slider _narratorVolumeSlider;
    [SerializeField] private Slider _musicVolumeSlider;

    private void Start()
    {
        _globalVolumeSlider.onValueChanged.AddListener(GlobalChange);
        _globalVolumeSlider.value = GlobalSettings.GlobalVolume;
        
        _soundFxVolumeSlider.onValueChanged.AddListener(SoundFxChange);
        _soundFxVolumeSlider.value = GlobalSettings.SoundFxVolume;
        
        _narratorVolumeSlider.onValueChanged.AddListener(NarratorChange);
        _narratorVolumeSlider.value = GlobalSettings.NarratorVolume;
        
        _musicVolumeSlider.onValueChanged.AddListener(MusicChange);
        _musicVolumeSlider.value = GlobalSettings.MusicVolume;
    }

    void GlobalChange(float value)
    {
        GlobalSettings.SetVolume(GlobalSettings.SoundGroup.Global, value);
    }
    
    void SoundFxChange(float value)
    {
        GlobalSettings.SetVolume(GlobalSettings.SoundGroup.SoundFX, value);
    }
    
    void NarratorChange(float value)
    {
        GlobalSettings.SetVolume(GlobalSettings.SoundGroup.Narrator, value);   
    }
    
    void MusicChange(float value)
    {
        GlobalSettings.SetVolume(GlobalSettings.SoundGroup.Music, value);   
    }
}
