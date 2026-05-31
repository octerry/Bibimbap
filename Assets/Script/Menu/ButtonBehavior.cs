using UnityEngine;
using UnityEngine.UI;

public class ButtonBehavior : MonoBehaviour
{
    [SerializeField] private Button _pveButton;
    [SerializeField] private Button _pvpButton;
    [SerializeField] private Button _disableNarratorButton;
    [SerializeField] private Button _enableNarratorButton;
    [SerializeField] private Button _startButton;

    private bool _pveSelected;
    
    void Start()
    {
        _pveButton.onClick.AddListener(OnPveClick);
        // _pvpButton.onClick.AddListener(OnPvpClick);
        _disableNarratorButton.onClick.AddListener(OnDisableNarratorClick);
        _enableNarratorButton.onClick.AddListener(OnEnableNarratorClick);
        _startButton.onClick.AddListener(OnStartClick);
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
        GlobalSettings.narratorEnabled = false;
        
        _disableNarratorButton.gameObject.SetActive(false);
        _enableNarratorButton.gameObject.SetActive(false);
        _startButton.gameObject.SetActive(true);
    }

    private void OnEnableNarratorClick()
    {
        GlobalSettings.narratorEnabled = true;
        
        _disableNarratorButton.gameObject.SetActive(false);
        _enableNarratorButton.gameObject.SetActive(false);
        _startButton.gameObject.SetActive(true);
    }

    private void OnStartClick()
    {
        if (_pveSelected) GlobalSettings.LaunchPve();
    }
}
