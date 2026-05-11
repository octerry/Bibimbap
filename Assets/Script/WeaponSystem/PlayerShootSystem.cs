using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShootSystem : MonoBehaviour
{
    [SerializeField] private InputActionAsset Actions;
    private InputAction _actionTrigger, _actionAim;

    [SerializeField] private GameObject _ammo;
    [SerializeField] private float _ammoSpeed;
    [SerializeField] private float _ammoCooldown;
    private float _ammoTimeRef;
    private Vector2 _lastMousePos;
    private AmmoSystem.ammoType _currentAmmoType;
    
    private Camera _cam;
    private float _angle = 0;
    [SerializeField] private Sprite[] _weaponSprites;
    private Transform _shootCursor;
    private SpriteRenderer _shootImage;

    void OnEnable()
    {
        Actions.Enable();
        _actionTrigger = Actions.FindAction("Trigger");
        _actionTrigger.started += TriggerCheck;
        _actionTrigger.canceled += TriggerCheck;
        
        _actionAim = Actions.FindAction("Aim");
        _actionAim.performed += AimCheck;
    }
    
    void Start()
    {
        _cam = Camera.main;
        _shootCursor = transform.Find("ShootCursor");
        _shootImage = _shootCursor.Find("ShootImage").GetComponent<SpriteRenderer>();
        _ammoTimeRef = Time.time - _ammoCooldown;
    }

    void Update()
    {
        _shootCursor.rotation = Quaternion.Euler(0, 0, _angle * Mathf.Rad2Deg);

        if (_ammoTimeRef > Time.time)
        {
            _shootCursor.gameObject.SetActive(false);
        }
        else
        {
            _shootCursor.gameObject.SetActive(true);
        }
    }
    
    void OnGUI()
    { 
        // Je l'ai piqué à la docu Unity
        Vector3 point = new Vector3();
        Event   currentEvent = Event.current;
        Vector2 mousePos = new Vector2();

        // Get the mouse position from Event.
        // Note that the y position from Event is inverted.
        mousePos.x = currentEvent.mousePosition.x;
        mousePos.y = _cam.pixelHeight - currentEvent.mousePosition.y;

        if (mousePos != _lastMousePos)
        {
            Cursor.visible = true;
            
            point = _cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, _cam.nearClipPlane));
        
            float x = point.x - transform.position.x;
            float y = point.y - transform.position.y;
        
            _angle = MathF.Atan2(y, x); 
            
            _lastMousePos = mousePos;
        }
    }

    void TriggerCheck(InputAction.CallbackContext phase)
    {
        if (!phase.canceled && !(_ammoTimeRef > Time.time))
        {
            Vector2 direction = new Vector2();
            direction.x = MathF.Cos(_angle) * _ammoSpeed;
            direction.y = MathF.Sin(_angle) * _ammoSpeed;
            
            GameObject bullet = Instantiate(_ammo);
            
            Vector3 newPos = bullet.transform.position;
            newPos.x = transform.position.x + (direction.x/_ammoSpeed * transform.localScale.x);
            newPos.y = transform.position.y + (direction.y/_ammoSpeed * transform.localScale.y);
            bullet.transform.position = newPos;
            
            bullet.transform.GetComponent<Rigidbody2D>().linearVelocity = direction;
            _ammoTimeRef = Time.time + _ammoCooldown;
        }
    }

    void AimCheck(InputAction.CallbackContext phase)
    {
        Cursor.visible = false;
        
        Vector2 direction = phase.ReadValue<Vector2>();
        
        _angle = Mathf.Atan2(direction.y, direction.x);
    }

    public void ChangeWeaponType(AmmoSystem.ammoType weaponType)
    {
        _currentAmmoType = weaponType;
        _shootImage.sprite = _weaponSprites[(int)weaponType];
    }
}
