using System;
using UnityEngine;

public class ImageCredits : MonoBehaviour
{
    [SerializeField] private Vector2 _endPosition;
    private Vector2 _startPosition;
    [SerializeField] private float _duration;
    [SerializeField] private AnimationCurve _curve;
    private float _progress;
    private Vector2 _range;
    
    void Start()
    {
        _startPosition = transform.position;
        _endPosition.x += transform.parent.GetComponent<RectTransform>().rect.width / 2;
        _range = _endPosition - _startPosition;
    }

    void Update()
    {
        _progress += Time.deltaTime / _duration;
        float animationProgress = _curve.Evaluate(_progress);

        if (animationProgress < 1)
        {
            Vector2 newPos = transform.position;
            newPos.x = (_range.x * animationProgress) + _startPosition.x;
            newPos.y = (_range.y * animationProgress) + _startPosition.y;
            transform.position = newPos;
        }
        else
        {
            transform.position = _endPosition;
        }
    }

    private void OnDrawGizmos()
    {
        Rect newRect = GetComponent<RectTransform>().rect;
        
        Vector2 centerStartPosition = _startPosition;
        centerStartPosition.x += newRect.width/2;
        centerStartPosition.y -= newRect.height/2;

        Vector2 centerEndPosition = _endPosition;
        centerEndPosition.x += newRect.width/2;
        centerEndPosition.y -= newRect.height/2;
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(centerStartPosition, 50f);
        Gizmos.DrawWireSphere(centerEndPosition, 50f);
        Gizmos.DrawLine(centerStartPosition, centerEndPosition);
        Gizmos.DrawWireCube(centerEndPosition, new Vector2(newRect.width, newRect.height));
    }
}
