using Unity.VisualScripting;
using UnityEngine;

public class BackgroundBehavior : MonoBehaviour
{
    [SerializeField] private float _gizmoSize = 1f;
    [SerializeField] private Vector2 _arenaSize = new Vector2(10, 10);
    private Camera _mainCam;
    private Transform _bgImage;
    private SpriteRenderer _bgSprite;
    private float _factorX = 1f;
    private float _factorY = 1f;
    
    void Start()
    {
        _mainCam = Camera.main;
        _bgImage = transform.GetChild(0);
        _bgSprite = _bgImage.GetComponent<SpriteRenderer>();
        
        float halfHeight = _mainCam.orthographicSize;
        float fullHeight = halfHeight * 2f;
        float fullWidth = fullHeight * _mainCam.aspect;
        
        _factorX = _bgSprite.bounds.size.x / _arenaSize.x;
        _factorY = _bgSprite.bounds.size.y / _arenaSize.y;
        
        // trapezoid
        Mesh mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        Vector3[] vertices = new Vector3[]
        { 
            new Vector3(_arenaSize.x/2, _arenaSize.y/2, 0f), // Bottom Right
            new Vector3(-_arenaSize.x/2, _arenaSize.y/2, 0f), // Bottom Left
            new Vector3(transform.position.x - _bgSprite.bounds.size.x/2, transform.position.y - _bgSprite.bounds.size.y/2, 0f),// Top Right
            new Vector3(transform.position.x + _bgSprite.bounds.size.x/2, transform.position.y - _bgSprite.bounds.size.y/2, 0f) // Top Left
        };

        // Two triangles: 0-1-2 and 0-2-3
        int[] triangles = new int[]
        {
            0, 1, 2,
            0, 2, 3
        };

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        
        // Recalculate normals for proper lighting
        mesh.RecalculateNormals();
    }

    void Update()
    {
        Vector3 newPosition = _bgImage.position;
        newPosition.x = _mainCam.transform.position.x * _factorX;
        newPosition.y = _mainCam.transform.position.y * _factorY;
        _bgImage.position = newPosition;
    }

    void OnDrawGizmos()
    {
        _bgImage = transform.GetChild(0);
        _bgSprite = _bgImage.GetComponent<SpriteRenderer>();
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _gizmoSize);
        Gizmos.DrawWireCube(transform.position, _arenaSize);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, _arenaSize - new Vector2(_bgSprite.bounds.size.x,_bgSprite.bounds.size.y)/2);
    }
}
