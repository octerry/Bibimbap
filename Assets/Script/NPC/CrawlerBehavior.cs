using UnityEngine;

public class CrawlerBehavior : MonoBehaviour
{
    [SerializeField] GameObject Player;
    private Vector3 playerPosition = new Vector3(0f,0f);

    [SerializeField] private float acceleration;
    [SerializeField] private float maxSpeed;
    private Vector2 movement;

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
        if (playerPosition.x < transform.position.x)
        {
            movement.x = -acceleration;
        }
        else if (playerPosition.x > transform.position.x)
        {
            movement.x = acceleration;
        }
        else
        {
            movement.x = 0f;
        }

        if (Mathf.Abs(rb.linearVelocityX) < maxSpeed || ( movement.x>0 ^ rb.linearVelocityX>0 ))
            rb.linearVelocityX += movement.x * Time.deltaTime;
    }
}
