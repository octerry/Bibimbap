using System;
using UnityEngine;
using UnityEngine.UI;

public class CreditScroll : MonoBehaviour
{
    [SerializeField] private float _speed;
    private float _height;
    private float _canvasHeight;

    private void Start()
    {
        _height = GetComponent<RectTransform>().rect.height;
        _canvasHeight = transform.parent.GetComponent<RectTransform>().rect.height;
    }

    void Update()
    {
        if (transform.position.y < _height/2)
        {
            Vector3 newPos = transform.position;
            newPos.y += _speed * Time.deltaTime;
            transform.position = newPos;
        }
    }
}
