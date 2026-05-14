using System;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class CameraBigArea : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _moveDuration;

    [SerializeField] private BackgroundBehavior _backgroundBehavior;
    private Vector2 _arenaSize;
    
    private Camera _mainCam;
    
    void Start()
    {
        _mainCam = Camera.main;
        _arenaSize = _backgroundBehavior.arenaSize;
        
        Debug.Log(_arenaSize);
        
        Vector3 newPosition = transform.position;
        newPosition = _player.position;
        newPosition.z = -100;
        transform.position = newPosition;
        
        Debug.Log(transform.position);
    }

    void Update()
    {
        // Go to player position
        Vector3 newPosition = transform.position;
        
        newPosition.x = Mathf.Lerp(transform.position.x, _player.position.x, _moveDuration * Time.deltaTime);
        newPosition.y = Mathf.Lerp(transform.position.y, _player.position.y, _moveDuration * Time.deltaTime);
        newPosition.z = -100;
        transform.position = newPosition;
        
        // Stay In
        float height = 2f * _mainCam.orthographicSize;
        float width = height * _mainCam.aspect;

        float left = _arenaSize.x / 2;
        float right = - (_arenaSize.x/2);
        float top = _arenaSize.y / 2;
        float bottom = - (_arenaSize.y/2);
        
        if (_mainCam.transform.position.x < left + width/2)
        {
            Vector3 newPos = _mainCam.transform.position;
            newPos.x = left + width/2;
            _mainCam.transform.position = newPos;
            Debug.Log("GAUCHE");
        }
        
        if (_mainCam.transform.position.x > right - width/2)
        {
            Vector3 newPos = _mainCam.transform.position;
            newPos.x = right - width/2;
            _mainCam.transform.position = newPos;
            Debug.Log("DROITE");
        }
        
        if (_mainCam.transform.position.y < top + height/2)
        {
            Vector3 newPos = _mainCam.transform.position;
            newPos.y = top + height/2;
            _mainCam.transform.position = newPos;
            Debug.Log("HAUT");
        }
        
        if (_mainCam.transform.position.y > bottom - height/2)
        {
            Vector3 newPos = _mainCam.transform.position;
            newPos.y = bottom - height/2;
            _mainCam.transform.position = newPos;
            Debug.Log("BAS");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(Vector2.zero,1f);
        Gizmos.DrawWireCube(Vector2.zero, _arenaSize);
    }
}
