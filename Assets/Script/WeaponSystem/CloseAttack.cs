using System;
using System.Collections.Generic;
using UnityEngine;

public class CloseAttack : MonoBehaviour
{
    private List<Transform> _closeEnnemies;
    public float angle;
    public float contactRange;
    public Vector2 direction;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _closeEnnemies = new List<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GlobalSettings.gameRunning)
        {
            foreach (var ennemy in _closeEnnemies)
            {
                float x = ennemy.transform.position.x - transform.position.x;
                float y = ennemy.transform.position.y - transform.position.y;

                float ennemyAngle = Mathf.Atan2(y, x);

                bool minBound = ennemyAngle > angle - contactRange / 2;
                bool maxBound = ennemyAngle < angle + contactRange / 2 ||
                                ennemyAngle < angle + contactRange / 2 + (Math.PI * 2);
            
                if (minBound && maxBound)
                {
                    ennemy.GetComponent<Creature>().Die(direction);
                }
            }
        }
    }
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            _closeEnnemies.Add(col.transform);
        }
    }
    
    private void OnTriggerExit2D(Collider2D col)
    {
        if (_closeEnnemies.Contains(col.transform))
        {
            _closeEnnemies.Remove(col.transform);
        }
    }
}
