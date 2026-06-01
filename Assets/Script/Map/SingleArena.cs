using System;
using Unity.VisualScripting;
using UnityEngine;

public class SingleArena : MonoBehaviour
{
    [SerializeField] private PlayMusic _music;
    public bool IsPlaying { get; private set; }
    [SerializeField] private Transform _ennemies;
    public Transform[] spawnpoints;
    public Vector2 bounds;
    [SerializeField] private Vector2 _center;

    private void Update()
    {
        IsPlaying = _music.IsPlaying();
    }
    
    public void LaunchOutroMusic()
    {
        _music.LaunchOutro();
    }

    public void ActivateEnnemies()
    {
        _ennemies.gameObject.SetActive(true);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_center, bounds);
        Gizmos.DrawWireSphere(_center, 1f);
    }
}
