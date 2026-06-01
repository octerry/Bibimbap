using System;
using UnityEngine;

public class ExplosionSystem : MonoBehaviour
{
    [SerializeField] private float _explosionCountdown = 3;
    [SerializeField] private float _knockBack = 10f;
    private float _startTime;
    private Renderer _renderer;
    private Collider2D _coll;
    [SerializeField] private float _lethalDistance = 3f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _startTime = Time.time;
        _renderer = GetComponent<Renderer>();
        _coll = GetComponent<Collider2D>();
        
        // On joue le son d'explosion
        PlaySound.instance.PlayByType(PlaySound.SoundType.Explosion, transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (GlobalSettings.gameRunning)
        {
            if (Time.time > _explosionCountdown + _startTime)
            {
                Destroy(_renderer);
                Destroy(_coll);
            }

            if (Time.time > _explosionCountdown + _startTime + 1)
            {
                Destroy(gameObject);
            }
        }
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Rigidbody2D>() != null)
        {
            float cx = transform.position.x;
            float cy = transform.position.y;
            float px = collision.transform.position.x;
            float py = collision.transform.position.y;

            float angle = Mathf.Atan2(py - cy, px - cx);
            float m = Mathf.Sqrt(Mathf.Pow(px - cx, 2) + Mathf.Pow(py - cy, 2));

            if (m < _lethalDistance)
            {
                if (!collision.CompareTag("Player"))
                {
                    Destroy(collision.gameObject);
                }
            }
            
            float directionX = Mathf.Cos(angle);
            float directionY = Mathf.Sin(angle);
            float forceX = directionX * (_knockBack / m);
            float forceY = directionY * (_knockBack / m);
            
            collision.GetComponent<Rigidbody2D>().linearVelocityX = forceX;
            collision.GetComponent<Rigidbody2D>().linearVelocityY = forceY;
        };
    }
}
