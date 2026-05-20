using UnityEngine;

public class Creature : MonoBehaviour
{
    public void Die(Vector2 direction)
    {
        Destroy(gameObject);
    }
}
