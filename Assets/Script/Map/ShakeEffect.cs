using Unity.Mathematics.Geometry;
using UnityEngine;

public class ShakeEffect : MonoBehaviour
{
    public enum MovementType
    {
        Linear,
        Random
    }
    
    [SerializeField] private AnimationCurve _movementCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    [SerializeField] private AnimationCurve _globalCurve = AnimationCurve.Linear(0,0,1,1);
    private float _animationProgress = 1f;
    private float _curveProgress = 1f;
    private float _globalProgress = 0f;
    [SerializeField] private float _singleAnimationDuration = 0.5f;
    
    [SerializeField] private int _maxItterations = 3;
    
    [SerializeField] private float _intensity = 4f;
    [SerializeField] private MovementType _movementPath;
    [SerializeField] private Vector3 _initialDirection = new Vector3(1f,1f,0f);
    private Vector3 _initialPosition;
    private Vector3 _nextDirection;
    private Vector3 _targetPosition;

    [ContextMenu("Play")]
    void PlayAnimation()
    {
        float globalLevel = _globalCurve.Evaluate(_globalProgress);
        _nextDirection = _initialDirection * globalLevel;
        _curveProgress = 1f;
        _initialPosition = transform.position;
        
        for (int i = 0; i < _maxItterations; i++)
        {
            _globalProgress = _globalCurve.Evaluate(i * Time.deltaTime);
            
            if (_curveProgress >= 1f)
            {
                _targetPosition = transform.position + _nextDirection;
                _animationProgress = 0;
                _curveProgress = 0f;
                _targetPosition = _initialPosition + _nextDirection;
                i--;

                if (_movementPath == MovementType.Linear)
                {
                    float major = Mathf.Max(_nextDirection.x, _nextDirection.y);
                    Vector3 newDirection = _nextDirection;
                    newDirection.x = _nextDirection.x / major;
                    newDirection.y = _nextDirection.y / major;
                    newDirection.x *= _intensity;
                    newDirection.y *= _intensity;
                    _nextDirection = - newDirection;
                }
            }
            else
            {
                while (_curveProgress < 1f)
                {
                    float positionRangeX = _targetPosition.x - transform.position.x;
                    float positionRangeY = _targetPosition.y - transform.position.y;
                
                    _curveProgress = _movementCurve.Evaluate(_animationProgress);
                    _animationProgress += Time.deltaTime / _singleAnimationDuration;

                    float positionLevelX = (positionRangeX * _curveProgress) + transform.position.x;
                    float positionLevelY = (positionRangeY * _curveProgress) + transform.position.y;
                
                    Vector3 newPosition = transform.position;
                    newPosition.x = positionLevelX;
                    newPosition.y = positionLevelY;
                    transform.position = newPosition;
                }
            }
        }
    }

    [ContextMenu("Stop")]
    void StopAnimation()
    {
        _animationProgress = 1f;
    }
}
