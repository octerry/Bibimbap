using UnityEngine;

public class CrawlerBehavior : MonoBehaviour
{
    private Transform _mobCrawler;
    private Animator _animator;
    
    [SerializeField] GameObject Player;
    private Vector3 playerPosition = new Vector3(0f,0f);

    [SerializeField] private float acceleration;
    [SerializeField] private float maxSpeed;
    private Vector2 movement;

    private Rigidbody2D rb;
    
    void Start()
    {
        _mobCrawler = transform.Find("Mob_Crawler");
        _animator = _mobCrawler.GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        playerPosition = Player.transform.position;
        MoveCheck();

        _animator.SetBool("isRunning",Mathf.Abs(rb.linearVelocityX) > 1);
        float headDirection = rb.linearVelocityX / Mathf.Abs(rb.linearVelocityX);
        
        Vector3 newScale = transform.localScale;
        newScale.x = -Mathf.Abs(newScale.x) * headDirection;
        transform.localScale = newScale;
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
