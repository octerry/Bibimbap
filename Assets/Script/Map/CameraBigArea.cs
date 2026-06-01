using System;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class CameraBigArea : MonoBehaviour
{
    public enum CameraState
    {
        BigArea,
        Static,
        Transitionning
    }
    
    [SerializeField] private Transform _player;
    [SerializeField] private float _moveDuration;

    [SerializeField] private SingleArena _singleArena1;
    [NonSerialized] public Vector2 _arenaSize;
    [NonSerialized] public Vector2 _arenaCenter;
    
    private Camera _mainCam;
    private CameraState _cameraState = CameraState.BigArea;

    private Vector2 _staticPosition;
    private float _staticScale;
    private Vector2 _startPosition;
    private float _startScale;

    [SerializeField] private AnimationCurve _transitionAnimationCurve;
    private float _transitionMarker;
    [SerializeField] private float _staticTransitionDuration = 5;
    private bool _transitionning = false;
    
    void Start()
    {
        _mainCam = Camera.main;
        _arenaSize = _singleArena1.bounds;
        
        Vector3 newPosition = transform.position;
        newPosition = _player.position;
        newPosition.z = -100;
        transform.position = newPosition;
    }

    void Update()
    {
        if (GlobalSettings.gameRunning)
        {
            if (_cameraState == CameraState.BigArea)
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

                float right = _arenaSize.x / 2;
                float left = - (_arenaSize.x/2);
                float top = _arenaSize.y / 2;
                float bottom = - (_arenaSize.y/2);
            
                if (_mainCam.transform.position.x < left + width/2) // à gauche
                {
                    Vector3 newPos = _mainCam.transform.position;
                    newPos.x = left + width/2;
                    _mainCam.transform.position = newPos;
                }
            
                if (_mainCam.transform.position.x > right - width/2) // à droite
                {
                    Vector3 newPos = _mainCam.transform.position;
                    newPos.x = right - width/2;
                    _mainCam.transform.position = newPos;
                }
            
                if (_mainCam.transform.position.y < bottom + height/2) // en bas
                {
                    Vector3 newPos = _mainCam.transform.position;
                    newPos.y = bottom + height/2;
                    _mainCam.transform.position = newPos;
                }
            
                if (_mainCam.transform.position.y > top - height/2) // en haut
                {
                    Vector3 newPos = _mainCam.transform.position;
                    newPos.y = top - height/2;
                    _mainCam.transform.position = newPos;
                }
            }

            if (_cameraState == CameraState.Static && _transitionning)
            {
                
                _transitionMarker += Time.deltaTime / _staticTransitionDuration;
                float transitionProgress = _transitionAnimationCurve.Evaluate(_transitionMarker);
                
                Debug.Log(_transitionMarker);
                
                if (transitionProgress >= 1) _transitionning = false;
                
                Vector3 nextPosition;
                nextPosition.x = _staticPosition.x - _startPosition.x;
                nextPosition.y = _staticPosition.y - _staticPosition.y;
                
                Vector3 newPosition = transform.position;
                newPosition.x = (nextPosition.x * transitionProgress) + _startPosition.x;
                newPosition.y = (nextPosition.y * transitionProgress) + _staticPosition.y;
                transform.position = newPosition;

                float nextScale = _staticScale - _startScale;
                _mainCam.orthographicSize = (nextScale * transitionProgress) + _startScale;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(Vector2.zero,1f);
        Gizmos.DrawWireCube(Vector2.zero, _arenaSize);
    }

    public void SwitchToStatic(Vector2 center, float zoom)
    {
        _cameraState = CameraState.Static;
        _transitionMarker = 0;
        _transitionning = true;
        _staticPosition = center;
        _staticScale = zoom * _mainCam.orthographicSize;
        _startPosition = transform.position;
        _startScale = _mainCam.orthographicSize;
    }
}
