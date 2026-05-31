using System;
using UnityEngine;

public class PlayDialogue : MonoBehaviour
{
    [SerializeField] private AudioClip[] _dialogues;
    private AudioSource _audioSource;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = _dialogues[0];
        _audioSource.Play();
    }
}
