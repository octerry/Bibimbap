using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShootSystem : MonoBehaviour
{
    [SerializeField] private InputActionAsset Actions;
    private InputAction _actionTrigger, _actionAim;

    [SerializeField] private Weapons _weapons;

    [SerializeField] private GameObject _ammo;
    [SerializeField] private GameObject _closeAttack;
    [SerializeField] private float _ammoSpeed;
    [SerializeField] private float _ammoCooldown;
    private float _ammoTimeRef;
    private Vector2 _lastMousePos;
    private AmmoSystem.ammoType _currentAmmoType;
    
    private Camera _cam;
    private float _angle = 0;
    private Transform _shootCursor;
    private SpriteRenderer _shootImage;
    [SerializeField] private float _contactRange;

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
            
            switch (_currentAmmoType)
            {
                case AmmoSystem.ammoType.PopCorn:
                {

                    break;
                }
                case AmmoSystem.ammoType.RoquetteLauncher:
                {

                    break;
                }
                case AmmoSystem.ammoType.Yogurt:
                {

                    break;
                }
                case AmmoSystem.ammoType.Spaghetti:
                {

                    break;
                }
                case AmmoSystem.ammoType.Pomegranate:
                {
                    // On créé l'objet
                    GameObject bullet = Instantiate(_ammo);
                    AmmoSystem bulletSystem = bullet.GetComponent<AmmoSystem>();
                    
                    // On lui donne les paramètre qui correspondent
                    bulletSystem.currentAmmoType = AmmoSystem.ammoType.PotatoLauncher;
                    bulletSystem.explosion = true;
                    bulletSystem.explodeOnContact = false;
                    bulletSystem.duration = 4f;
            
                    // On le mets à la position du joueur
                    Vector3 newPos = bullet.transform.position;
                    newPos.x = transform.position.x + (direction.x/_ammoSpeed * transform.localScale.x);
                    newPos.y = transform.position.y + (direction.y/_ammoSpeed * transform.localScale.y);
                    bullet.transform.position = newPos;
            
                    // On lui donne de la vitesse
                    bullet.GetComponent<Rigidbody2D>().linearVelocity = direction;
                    
                    // On réinitialise le timer
                    _ammoTimeRef = Time.time + _ammoCooldown;
                    
                    break;
                }
                case AmmoSystem.ammoType.PotatoLauncher:
                {
                    GameObject bullet = Instantiate(_ammo);
                    AmmoSystem bulletSystem = bullet.GetComponent<AmmoSystem>();
                    
                    bulletSystem.currentAmmoType = AmmoSystem.ammoType.PotatoLauncher;
                    bulletSystem.explosion = true;
                    bulletSystem.explodeOnContact = true;
                    bulletSystem.duration = 30f;
            
                    Vector3 newPos = bullet.transform.position;
                    newPos.x = transform.position.x + (direction.x/_ammoSpeed * transform.localScale.x);
                    newPos.y = transform.position.y + (direction.y/_ammoSpeed * transform.localScale.y);
                    bullet.transform.position = newPos;
            
                    bullet.GetComponent<Rigidbody2D>().linearVelocity = direction;
                    _ammoTimeRef = Time.time + _ammoCooldown;
                    
                    break;
                }
                case AmmoSystem.ammoType.Toast:
                {
                    
                    break;
                }
                case AmmoSystem.ammoType.Starfruit:
                {

                    break;
                }
                case AmmoSystem.ammoType.Roquefort:
                {
                    
                    break;
                }
                case AmmoSystem.ammoType.Baguette:
                {
                    float maxBound = _angle + _contactRange / 2;
                    float minBound = _angle - _contactRange / 2;

                    GameObject attack = Instantiate(_closeAttack);
                    attack.transform.position = transform.position;
                    CloseAttack closeAttack = attack.GetComponent<CloseAttack>();
                    closeAttack.angle = _angle;
                    closeAttack.contactRange = _contactRange;
                    closeAttack.direction = direction;

                    break;
                }
            }
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
        switch (weaponType)
        {
            case AmmoSystem.ammoType.Empty : _shootImage.sprite = null; break;
            case AmmoSystem.ammoType.PopCorn : _shootImage.sprite = _weapons.weaponSprites[0]; break;
            case AmmoSystem.ammoType.RoquetteLauncher : _shootImage.sprite = _weapons.weaponSprites[1]; break;
            case AmmoSystem.ammoType.Yogurt : _shootImage.sprite = _weapons.weaponSprites[2]; break;
            case AmmoSystem.ammoType.Spaghetti : _shootImage.sprite = _weapons.weaponSprites[3]; break;
            case AmmoSystem.ammoType.Pomegranate : _shootImage.sprite = _weapons.weaponSprites[4]; break;
            case AmmoSystem.ammoType.PotatoLauncher : _shootImage.sprite = _weapons.weaponSprites[5]; break;
            case AmmoSystem.ammoType.Toast : _shootImage.sprite = _weapons.weaponSprites[6]; break;
            case AmmoSystem.ammoType.Starfruit : _shootImage.sprite = _weapons.weaponSprites[7]; break;
            case AmmoSystem.ammoType.Roquefort : _shootImage.sprite = _weapons.weaponSprites[8]; break;
            case AmmoSystem.ammoType.Baguette : _shootImage.sprite = _weapons.weaponSprites[9]; break;
        }
    }
}
