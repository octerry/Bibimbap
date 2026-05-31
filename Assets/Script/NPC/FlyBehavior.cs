using System;
using UnityEngine;

public class FlyBehavior : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    private Vector3 _playerPosition = new Vector3(0f,0f);

    [SerializeField] private float _acceleration;
    [SerializeField] private float _maxSpeed;
    private Vector2 _objectif;
    private Vector2 _movement;
    private float _angle;
    private float _speed;

    private Rigidbody2D _rb;
    
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
            MoveCheck();
        }
        else
        {
            _rb.simulated = false;
        }
    }

    void MoveCheck()
    {
        _objectif.x = _playerPosition.x - transform.position.x;
        _objectif.y = _playerPosition.y - transform.position.y;

        _speed = MathF.Abs( Mathf.Pow(_objectif.x, 2) + Mathf.Pow(_objectif.y, 2) );
        if (_speed > _acceleration)
        {
            _speed = _acceleration;
        }
        
        _angle = Mathf.Atan2(_objectif.y, _objectif.x);
        
        _rb.rotation = _angle * Mathf.Rad2Deg;
        _rb.linearVelocityX = _speed * MathF.Cos(_angle);
        _rb.linearVelocityY = _speed * MathF.Sin(_angle);
    }
}
