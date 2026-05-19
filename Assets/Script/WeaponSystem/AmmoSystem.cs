using UnityEngine;

public class AmmoSystem : MonoBehaviour
{
    public enum ammoType
    {
        Empty = 0,
        PopCorn = 1,
        RoquetteLauncher = 2,
        Yogurt = 3,
        Spaghetti = 4,
        Pomegranate = 5, //Grenade
        PotatoLauncher = 6,
        Toast = 7, //Tartine
        Starfruit = 8, //Carambole
        Roquefort = 9,
        Baguette = 10,
    }
    
    public float duration;
    public bool explodeOnContact;
    public bool explosion;
    [SerializeField] GameObject _explosionObject;
    public ammoType currentAmmoType;
    
    private float _spawnTime;
    private SpriteRenderer _spriteRenderer;
    
    void Start()
    {
        _spawnTime = Time.time;
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if ( (Time.time - _spawnTime) >= duration)
        {
            if (explosion && !explodeOnContact)
            {
                GameObject explosion = Instantiate(_explosionObject);
                explosion.transform.position = transform.position;
            }
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
                GameObject explosion = Instantiate(_explosionObject);
                explosion.transform.position = transform.position;
            }
            Destroy(gameObject);
        };
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (explosion && explodeOnContact)
        {
            GameObject explosion = Instantiate(_explosionObject);
            explosion.transform.position = transform.position;
            Destroy(gameObject);
        }
    }
}
