using System;
using UnityEngine;

public class PlayDialogue : MonoBehaviour
{
    [SerializeField] private AudioClip[] _dialogues;
    private int _currentDialogue = 0;
    private AudioSource _audioSource;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = _dialogues[0];
        _audioSource.volume = GlobalSettings.NarratorVolume * GlobalSettings.GlobalVolume;
        _audioSource.Play();
    }

    public void NextDialogue()
    {
        _currentDialogue++;
        _audioSource.clip = _dialogues[_currentDialogue];
        _audioSource.Play();
    }

    private void Update()
    {
        _audioSource.volume = GlobalSettings.NarratorVolume * GlobalSettings.GlobalVolume;
    }
}
