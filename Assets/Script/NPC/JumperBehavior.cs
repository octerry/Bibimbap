using UnityEngine;

public class JumperBehavior : MonoBehaviour
{
    [SerializeField] GameObject Player;
    private Vector3 playerPosition = new Vector3(0f,0f);

    [SerializeField] private float jumpWidth;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float jumpWaitSeconds;
    private Rigidbody2D rb;
    private float startTime;
    private Vector2 objectif = new Vector2(0f, 0f);
    private Vector2 direction = new Vector2(0f,0f);
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        playerPosition = Player.transform.position;
        JumpCheck();
    }

    void JumpCheck()
    {
        objectif.x = playerPosition.x - transform.position.x;
        objectif.y = playerPosition.y - transform.position.y;
        
        direction.x = objectif.x;
        direction.y = objectif.y*2;
        
        if (direction.x > jumpWidth) direction.x = jumpWidth;
        if (direction.y > jumpHeight) direction.y = jumpHeight;

        if (startTime < Time.time)
        {
            rb.linearVelocityX = direction.x;
            rb.linearVelocityY = direction.y;
            startTime = Time.time + jumpWaitSeconds;
        }
    }
}
