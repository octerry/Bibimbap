using System;
using UnityEngine;

public class PlayMusic : MonoBehaviour
{
    [SerializeField] private AudioClip _musicIntro;
    [SerializeField] private AudioClip _musicLoop;
    [SerializeField] private AudioClip _musicOutro;
    private AudioSource _audioSource;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = GlobalSettings.MusicVolume * GlobalSettings.GlobalVolume;
        if (_musicIntro) 
        {
            _audioSource.clip = _musicIntro;
            _audioSource.Play();
        }
    }

    private void Update()
    {
        _audioSource.volume = GlobalSettings.MusicVolume * GlobalSettings.GlobalVolume;
        if (!_audioSource.isPlaying)
        {
            _audioSource.clip = _musicLoop;
            _audioSource.Play();
        }
    }
}
