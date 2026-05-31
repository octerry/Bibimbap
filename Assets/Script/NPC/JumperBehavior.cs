using UnityEngine;

public class JumperBehavior : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    private Vector3 _playerPosition = new Vector3(0f,0f);

    [SerializeField] private float _jumpWidth;
    [SerializeField] private float _jumpHeight;
    [SerializeField] private float _jumpWaitSeconds;
    private Rigidbody2D _rb;
    private float _startTime;
    private Vector2 _objectif = new Vector2(0f, 0f);
    private Vector2 _direction = new Vector2(0f,0f);
    
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (GlobalSettings.gameRunning)
        {
            _rb.simulated = true;
            
            _playerPosition = _player.transform.position;
            JumpCheck();
        }
        else
        {
            _rb.simulated = false;
        }
    }

    void JumpCheck()
    {
        _objectif.x = _playerPosition.x - transform.position.x;
        _objectif.y = _playerPosition.y - transform.position.y;
        
        _direction.x = _objectif.x;
        _direction.y = _objectif.y*2;
        
        if (_direction.x > _jumpWidth) _direction.x = _jumpWidth;
        if (_direction.y > _jumpHeight) _direction.y = _jumpHeight;

        if (_startTime < Time.time)
        {
            _rb.linearVelocityX = _direction.x;
            _rb.linearVelocityY = _direction.y;
            _startTime = Time.time + _jumpWaitSeconds;
        }
    }
}
