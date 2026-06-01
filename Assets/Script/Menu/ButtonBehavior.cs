using System;
using UnityEngine;
using UnityEngine.UI;

public class ButtonBehavior : MonoBehaviour
{
    [SerializeField] private Button _pveButton;
    [SerializeField] private Button _pvpButton;
    [SerializeField] private Button _disableNarratorButton;
    [SerializeField] private Button _enableNarratorButton;
    [SerializeField] private Button _startButton;

    [SerializeField] private PlayMusic _music;
    [SerializeField] private Animator _rightTheaterCurtain;
    [SerializeField] private Animator _leftTheaterCurtain;

    private bool _outroStarted;

    private bool _pveSelected;
    
    void Start()
    {
        _pveButton.onClick.AddListener(OnPveClick);
        // _pvpButton.onClick.AddListener(OnPvpClick);
        _disableNarratorButton.onClick.AddListener(OnDisableNarratorClick);
        _enableNarratorButton.onClick.AddListener(OnEnableNarratorClick);
        _startButton.onClick.AddListener(OnStartClick);
        PlaySound.instance.PlayByType(PlaySound.SoundType.Curtain, transform.position, 0);
    }

    private void Update()
    {
        if (_pveSelected && _outroStarted && !_music.IsPlaying()) GlobalSettings.LaunchPve();
    }

    private void OnPveClick()
    {
        _pveSelected = true;
        
        _pveButton.gameObject.SetActive(false);
        _pvpButton.gameObject.SetActive(false);
        _disableNarratorButton.gameObject.SetActive(true);
        _enableNarratorButton.gameObject.SetActive(true);
    }

    // private void OnPvpClick()
    // {
    //     _pveSelected = false;
    //
    // _pveButton.gameObject.SetActive(false);
    // _pvpButton.gameObject.SetActive(false);
    // _disableNarratorButton.gameObject.SetActive(true);
    // _enableNarratorButton.gameObject.SetActive(true);
    // }

    private void OnDisableNarratorClick()
    {
        GlobalSettings.NarratorVolume = 0;
        
        _disableNarratorButton.gameObject.SetActive(false);
        _enableNarratorButton.gameObject.SetActive(false);
        _startButton.gameObject.SetActive(true);
    }

    private void OnEnableNarratorClick()
    {
        // On garde la valeur de base de NarratorVolume donc on touche à rien
        
        _disableNarratorButton.gameObject.SetActive(false);
        _enableNarratorButton.gameObject.SetActive(false);
        _startButton.gameObject.SetActive(true);
    }

    private void OnStartClick()
    {
        PlaySound.instance.PlayByType(PlaySound.SoundType.Curtain, transform.position, 1);
        _leftTheaterCurtain.ResetTrigger("open");
        _leftTheaterCurtain.SetTrigger("close");
        _leftTheaterCurtain.ResetTrigger("open");
        _rightTheaterCurtain.SetTrigger("close");
        _outroStarted = true;
        _music.LaunchOutro();
    }
}
