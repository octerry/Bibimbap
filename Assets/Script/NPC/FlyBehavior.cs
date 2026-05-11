using System;
using UnityEngine;

public class FlyBehavior : MonoBehaviour
{
    [SerializeField] GameObject Player;
    private Vector3 playerPosition = new Vector3(0f,0f);

    [SerializeField] private float acceleration;
    [SerializeField] private float maxSpeed;
    private Vector2 objectif;
    private Vector2 movement;
    private float angle;
    private float speed;

    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        playerPosition = Player.transform.position;
        MoveCheck();
    }

    void MoveCheck()
    {
        objectif.x = playerPosition.x - transform.position.x;
        objectif.y = playerPosition.y - transform.position.y;

        speed = MathF.Abs( Mathf.Pow(objectif.x, 2) + Mathf.Pow(objectif.y, 2) );
        if (speed > acceleration)
        {
            speed = acceleration;
        }
        
        angle = Mathf.Atan2(objectif.y, objectif.x);
        
        rb.rotation = angle * Mathf.Rad2Deg;
        rb.linearVelocityX = speed * MathF.Cos(angle);
        rb.linearVelocityY = speed * MathF.Sin(angle);
    }
}
