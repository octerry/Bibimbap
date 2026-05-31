using UnityEngine;
using UnityEngine.InputSystem;

public class UIBehavior : MonoBehaviour
{
    [SerializeField] private InputActionAsset Actions;
    private InputAction _menuAction;
    
    private Transform _menu;
    private bool _menuState;
    
    void Start()
    {
        _menu = transform.Find("Menu");
        
        Actions.Enable();
        _menuAction = Actions.FindAction("Menu");
        _menuAction.started += ToggleMenu;
    }

    void ToggleMenu(InputAction.CallbackContext phase)
    {
        _menuState = !_menuState;
        GlobalSettings.gameRunning = !_menuState;
        _menu.gameObject.SetActive(_menuState);
    }
}
