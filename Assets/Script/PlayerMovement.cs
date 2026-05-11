using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private InputActionAsset Actions;
    private InputAction moveAction, jumpAction;

    [SerializeField] private GameObject canvas;
    private GameObject gameOverObject;

    [SerializeField] private float acceleration = 10f;
    [SerializeField] private float maxSpeed = 500f;
    [SerializeField] private float jump = 8f;

    private PlayerShootSystem _playerShoot;
    [SerializeField] private WeaponSpawn _weaponSpawn;

    private Rigidbody2D rb;
    private Collider2D groundCollider;
    private Vector2 direction;
    private bool moving = false;
    private bool jumping = false;
    
    void OnEnable()
    {
        Actions.Enable();
        moveAction = Actions.FindAction("Move");
        moveAction.performed += MoveCheck;
        moveAction.canceled += MoveCheck;

        jumpAction = Actions.FindAction("Jump");
        jumpAction.started += JumpCheck;
    }
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameOverObject = canvas.transform.Find("Gameoverscreen").gameObject;
        _playerShoot = GetComponent<PlayerShootSystem>();
    }

    void Update()
    {
        if (moving)
        {
            if (MathF.Abs(rb.linearVelocity.x) < maxSpeed || (direction.x>0 ^ rb.linearVelocityX>0))
                rb.linearVelocityX += acceleration * direction.x * Time.deltaTime;
        }

        if (jumping)
        {
            rb.linearVelocityY = jump * direction.y;
            jumping = false;
        }
    }
    
    void MoveCheck(InputAction.CallbackContext phase)
    {
        direction = phase.ReadValue<Vector2>();
        direction = Vector2.ClampMagnitude(direction, 1f);
        if(phase.canceled)
        {
            direction = Vector2.zero;
            moving = false;
        }
        else
        {
            moving = true;
        }
    }

    void JumpCheck(InputAction.CallbackContext phase)
    {
        direction.y = 1;
        jumping = true;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            gameOverObject.SetActive(true);
            Time.timeScale = 0;
        }

        if (collision.CompareTag("Weapon"))
        {
            _playerShoot.ChangeWeaponType(collision.GetComponent<SingleWeapon>().weaponType);
            _weaponSpawn.DestroySingleWeapon(collision.GetComponent<SingleWeapon>().WeaponId);
            Destroy(collision.gameObject);
        }
    }
}
