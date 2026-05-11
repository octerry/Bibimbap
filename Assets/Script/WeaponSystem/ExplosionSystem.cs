using System;
using UnityEngine;

public class ExplosionSystem : MonoBehaviour
{
    [SerializeField] float explosionCountdown = 3;
    [SerializeField] private float knockBack = 10f;
    private float startTime;
    Renderer renderer;
    Collider2D coll;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startTime = Time.time;
        renderer = GetComponent<Renderer>();
        coll = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > explosionCountdown + startTime)
        {
            Destroy(renderer);
            Destroy(coll);
        }

        if (Time.time > explosionCountdown + startTime + 3)
        {
            Destroy(gameObject);
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

            float directionX = (px - cx) / Mathf.Abs(px - cx);
            float directionY = (py - cy) / Mathf.Abs(py - cy);
            float forceX = directionX * (knockBack / Mathf.Abs(px - cx));
            float forceY = directionY * (knockBack / Mathf.Abs(py - cy));
            
            collision.GetComponent<Rigidbody2D>().linearVelocityX = forceX;
            collision.GetComponent<Rigidbody2D>().linearVelocityY = forceY;
        };
    }
}
