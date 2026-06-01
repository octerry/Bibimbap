using System;
using Unity.VisualScripting;
using UnityEngine;

public class SingleArena : MonoBehaviour
{
    public PlayMusic music;
    [SerializeField] private Transform _ennemies;
    public Transform[] spawnpoints;
    public Vector2 bounds;
    public Vector2 center;

    public Transform playerSpawnpoint;

    public void ActivateEnnemies()
    {
        _ennemies.gameObject.SetActive(true);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, bounds);
        Gizmos.DrawWireSphere(center, 1f);
    }

    public int EnnemiesNumber()
    {
        return _ennemies.childCount;
    }
}
