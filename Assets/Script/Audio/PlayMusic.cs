using System;
using UnityEngine;

public class PlayMusic : MonoBehaviour
{
    [SerializeField] private AudioClip _musicIntro;
    [SerializeField] private AudioClip _musicLoop;
    [SerializeField] private AudioClip _musicOutro;
    private AudioSource _audioSource;
    private bool _loop = true;

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
        if (!_audioSource.isPlaying && _loop)
        {
            _audioSource.clip = _musicLoop;
            _audioSource.Play();
        }
    }

    public void LaunchOutro()
    {
        _audioSource.clip = _musicOutro;
        _audioSource.Play();
        _loop = false;
    }

    public bool IsPlaying()
    {
        return _audioSource.isPlaying;
    }
}
