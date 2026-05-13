using System;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class CameraStayIn : MonoBehaviour
{
    [SerializeField] private Vector2 _center;
    [SerializeField] private float _width;
    [SerializeField] private float _height;

    private Camera _mainCam;
    
    void Start()
    {
        _mainCam = Camera.main;
    }

    void Update()
    {
        float height = 2f * _mainCam.orthographicSize;
        float width = height * _mainCam.aspect;

        float left = _center.x - _width / 2;
        float right = _center.x + _width / 2;
        float top = _center.y - _width / 2;
        float bottom = _center.y + _width / 2;

        if (_mainCam.transform.position.x - width < left)
        {
            Vector2 newPos = _mainCam.transform.position;
            newPos.x = left + width/2;
            _mainCam.transform.position = newPos;
        }

        if (_mainCam.transform.position.x + width > right)
        {
            Vector2 newPos = _mainCam.transform.position;
            newPos.x = right - width/2;
            _mainCam.transform.position = newPos;
        }

        if (_mainCam.transform.position.y - height < top)
        {
            Vector2 newPos = _mainCam.transform.position;
            newPos.x = bottom + height/2;
            _mainCam.transform.position = newPos;
        }
        
        if (_mainCam.transform.position.y + height > bottom)
        {
            Vector2 newPos = _mainCam.transform.position;
            newPos.x = bottom - height/2;
            _mainCam.transform.position = newPos;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_center,1f);
        Gizmos.DrawWireCube(_center, new Vector2(_width,_height));
    }
}
