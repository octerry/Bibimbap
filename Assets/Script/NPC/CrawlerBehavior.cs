using UnityEngine;

public class CrawlerBehavior : MonoBehaviour
{
    private Transform _mobCrawler;
    private Animator _animator;
    
    [SerializeField] private GameObject _player;
    private Vector3 _playerPosition = new Vector3(0f,0f);

    [SerializeField] private float _acceleration;
    [SerializeField] private float _maxSpeed;
    private Vector2 _movement;

    private Rigidbody2D _rb;
    
    void Start()
    {
        _mobCrawler = transform.Find("Mob_Crawler");
        _animator = _mobCrawler.GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (GlobalSettings.gameRunning)
        {
            _rb.simulated = true;
            
            _playerPosition = _player.transform.position;
            MoveCheck();

            _animator.SetBool("isRunning",Mathf.Abs(_rb.linearVelocityX) > 1);
            float headDirection = _rb.linearVelocityX / Mathf.Abs(_rb.linearVelocityX);
        
            Vector3 newScale = transform.localScale;
            newScale.x = -Mathf.Abs(newScale.x) * headDirection;
            transform.localScale = newScale;
        }
        else
        {
            _rb.simulated = false;
        }
    }

    void MoveCheck()
    {
        if (_playerPosition.x < transform.position.x)
        {
            _movement.x = -_acceleration;
        }
        else if (_playerPosition.x > transform.position.x)
        {
            _movement.x = _acceleration;
        }
        else
        {
            _movement.x = 0f;
        }

        if (Mathf.Abs(_rb.linearVelocityX) < _maxSpeed || ( _movement.x>0 ^ _rb.linearVelocityX>0 ))
            _rb.linearVelocityX += _movement.x * Time.deltaTime;
    }
}
