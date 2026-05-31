using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Transform _pj;
    private Animator _animator;
    
    [SerializeField] private InputActionAsset Actions;
    private InputAction _moveAction, _jumpAction;

    [SerializeField] private GameObject _canvas;
    private GameObject _gameOverObject;

    [SerializeField] private float _acceleration = 10f;
    [SerializeField] private float _maxSpeed = 500f;
    [SerializeField] private float _jumpHeight = 8f;

    private PlayerShootSystem _playerShoot;
    [SerializeField] private WeaponSpawn _weaponSpawn;
    
    private Rigidbody2D _rb;
    private Collider2D _mainCollider;
    private Vector2 _direction;
    private bool _movingX = false;
    private bool _movingY = false;
    private bool _isGrounded = false;
    private int _doubleJumpsRemaining = 2;
    private int _doubleJumpMax = 2;
    private float _raycastHeight;
    
    void OnEnable()
    {
        Actions.Enable();
        _moveAction = Actions.FindAction("Move");
        _moveAction.performed += MoveCheck;
        _moveAction.canceled += MoveCheck;

        _jumpAction = Actions.FindAction("Jump");
        _jumpAction.started += JumpCheck;
    }
    
    void Start()
    {
        _pj = transform.Find("PJ");
        _animator = _pj.GetComponent<Animator>();
        
        _rb = GetComponent<Rigidbody2D>();
        _mainCollider = GetComponent<Collider2D>();
        _raycastHeight = _mainCollider.bounds.size.y/2 + 1f;
        
        bool hit = Physics2D.Raycast(transform.position, Vector2.down, _raycastHeight, LayerMask.GetMask("Ground"));
        if (hit)
        {
            _animator.SetBool("isGrounded", true);
            _isGrounded = true;
        }
        
        _gameOverObject = _canvas.transform.Find("Gameoverscreen").gameObject;
        _playerShoot = GetComponent<PlayerShootSystem>();
    }

    void Update()
    {
        _animator.SetBool("isRunning", _movingX);

        bool hit = Physics2D.Raycast(transform.position, Vector2.down, _raycastHeight, LayerMask.GetMask("Ground"));
        Debug.DrawRay(transform.position, Vector2.down * _raycastHeight, Color.red);
        
        if (_movingX)
        {
            if (MathF.Abs(_rb.linearVelocity.x) < _maxSpeed || (_direction.x>0 ^ _rb.linearVelocityX>0))
                _rb.linearVelocityX += _acceleration * _direction.x * Time.deltaTime;
        }

        if (!hit && _isGrounded) // Quand le joueur quitte le sol
        {
            _animator.SetBool("isGrounded", false);
            _isGrounded = false;
        }

        if (hit && !_isGrounded) // Quand le joueur touche le sol
        {
            _animator.SetBool("isGrounded", true);
            _isGrounded = true;
            _doubleJumpsRemaining = _doubleJumpMax;
        }
    }
    
    void MoveCheck(InputAction.CallbackContext phase)
    {
        _direction = phase.ReadValue<Vector2>();
        _direction = Vector2.ClampMagnitude(_direction, 1f);
        if(phase.canceled || (MathF.Abs(_direction.x) < .1 && MathF.Abs(_direction.y) < .1))
        {
            _direction = Vector2.zero;
            _movingX = false;
        }
        else
        {
            _movingX = false;
            _movingY = false;
            if ( MathF.Abs(_direction.x) > .1 )
            {
                _movingX = true;
                
                int directionVectorX = (int)(_direction.x / Mathf.Abs(_direction.x));
                
                Vector3 newScale = _pj.transform.localScale;
                newScale.x = MathF.Abs(newScale.x) * directionVectorX;
                _pj.transform.localScale = newScale;
            }
            if ( MathF.Abs(_direction.y) > .1 )
            {
                _movingY = true;
            }
        }
    }

    void JumpCheck(InputAction.CallbackContext phase)
    {
        if (_isGrounded || _doubleJumpsRemaining >= 0)
        {
            _animator.SetTrigger("jump");
            _direction.y = 1;
            _rb.linearVelocityY = _jumpHeight * _direction.y;

            if (!_isGrounded) _doubleJumpsRemaining--;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            _gameOverObject.SetActive(true);
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
