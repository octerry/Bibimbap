using UnityEngine;

public class Parallax : MonoBehaviour
{
    private Camera _mainCam;
    [SerializeField] private float _distance;
    private Transform[] _layers;
    private Vector2[] _initialLayout;
    
    void Start()
    {
        _mainCam = Camera.main;
        _layers = new Transform[transform.childCount];
        _initialLayout = new Vector2[transform.childCount];
        
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            _layers[i] = child;
            _initialLayout[i] = child.transform.position;
        }
    }

    void Update()
    {
        for (int i = 0; i < _layers.Length; i++)
        {
            Vector3 newPos = _layers[i].transform.position;
            newPos.x = _mainCam.transform.position.x / (_distance / (i+1));
            newPos.y = _mainCam.transform.position.y / (_distance / (i+1));
            _layers[i].transform.position = newPos;
        }
    }
}
