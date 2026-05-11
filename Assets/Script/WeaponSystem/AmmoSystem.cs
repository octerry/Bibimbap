using UnityEngine;

public class AmmoSystem : MonoBehaviour
{
    public enum ammoType
    {
        PopCorn = 0,
        RoquetteLauncher = 1,
        Yogurt = 2,
        Spaghetti = 3,
        Pomegranate = 4, //Grenade
        PotatoLauncher = 5,
        Toast = 6, //Tartine
        Starfruit = 7, //Carambole
        Roquefort = 8,
        Baguette = 9,
    }
    
    
    [SerializeField] int duration;
    [SerializeField] bool explosion;
    [SerializeField] GameObject explosionObject;
    float spawnTime;
    
    void Start()
    {
        spawnTime = Time.time;
    }

    void Update()
    {
        if ( (Time.time - spawnTime) >= duration)
        {
            Destroy(gameObject);
        }
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            if (explosion)
            {
                GameObject explosion = Instantiate(explosionObject);
                explosion.transform.position = transform.position;
            }
            Destroy(gameObject);
        };
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (explosion)
        {
            GameObject explosion = Instantiate(explosionObject);
            explosion.transform.position = transform.position;
            Destroy(gameObject);
        }
    }
}
