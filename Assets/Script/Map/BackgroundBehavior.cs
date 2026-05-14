using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class BackgroundBehavior : MonoBehaviour
{
    [SerializeField] private float _gizmoSize = 1f;
    public Vector2 arenaSize = new Vector2(10, 10);
    private Camera _mainCam;
    private Transform _bgImage;
    private SpriteRenderer _bgImageSpriteRenderer;
    private SpriteRenderer _bgSprite;
    private float _factorX = 1f;
    private float _factorY = 1f;

    private Vector2 _imageTL;
    private Vector2 _imageTR;
    private Vector2 _imageBR;
    private Vector2 _imageBL;
    private Vector2 _arenaTL;
    private Vector2 _arenaTR;
    private Vector2 _arenaBR;
    private Vector2 _arenaBL;
    
    private SpriteShapeController _topPart;
    private SpriteShapeController _bottomPart;
    private SpriteShapeController _leftPart;
    private SpriteShapeController _rightPart;
    
    void Start()
    {
        // ratio entre caméra, arène et image de fond
        _mainCam = Camera.main;
        _bgImage = transform.GetChild(0);
        _bgImageSpriteRenderer = _bgImage.GetComponent<SpriteRenderer>();
        _bgSprite = _bgImage.GetComponent<SpriteRenderer>();
        
        float halfHeight = _mainCam.orthographicSize;
        float fullHeight = halfHeight * 2f;
        
        _factorX = _bgSprite.bounds.size.x / arenaSize.x;
        _factorY = _bgSprite.bounds.size.y / arenaSize.y;

        _arenaTL = new Vector2(arenaSize.x/2, arenaSize.y/2);
        _arenaTR = new Vector2(- (arenaSize.x/2), arenaSize.y/2);
        _arenaBR = new Vector2(- (arenaSize.x/2), - (arenaSize.y/2) );
        _arenaBL = new Vector2(arenaSize.x/2, - (arenaSize.y/2) );
        
        // SpriteShape
        _topPart = transform.Find("TopPart").GetComponent<SpriteShapeController>();
        _bottomPart = transform.Find("BottomPart").GetComponent<SpriteShapeController>();
        _leftPart = transform.Find("LeftPart").GetComponent<SpriteShapeController>();
        _rightPart = transform.Find("RightPart").GetComponent<SpriteShapeController>();
        
        _topPart.spline.SetPosition(2, _arenaTR);
        _topPart.spline.SetPosition(3, _arenaTL);
        _rightPart.spline.SetPosition(2, _arenaTR);
        _rightPart.spline.SetPosition(3, _arenaBR);
        _bottomPart.spline.SetPosition(2, _arenaBR);
        _bottomPart.spline.SetPosition(3, _arenaBL);
        _leftPart.spline.SetPosition(2, _arenaBL);
        _leftPart.spline.SetPosition(3, _arenaTL);
    }

    void Update()
    {
        Vector3 newPosition = _bgImage.position;
        newPosition.x = _mainCam.transform.position.x * _factorX;
        newPosition.y = _mainCam.transform.position.y * _factorY;
        _bgImage.position = newPosition;

        Vector2 imageScale = _bgImageSpriteRenderer.bounds.size;
        
        _imageTL = new Vector2(_bgImage.position.x + imageScale.x / 2, _bgImage.position.y + imageScale.y / 2);
        _imageTR = new Vector2(_bgImage.position.x - imageScale.x / 2, _bgImage.position.y + imageScale.y / 2);
        _imageBR = new Vector2(_bgImage.position.x - imageScale.x / 2, _bgImage.position.y - imageScale.y / 2);
        _imageBL = new Vector2(_bgImage.position.x + imageScale.x / 2, _bgImage.position.y - imageScale.y / 2);
        
        _topPart.spline.SetPosition(0, _imageTL);
        _topPart.spline.SetPosition(1, _imageTR);
        
        _rightPart.spline.SetPosition(0, _imageBR);
        _rightPart.spline.SetPosition(1, _imageTR);
        
        _bottomPart.spline.SetPosition(0, _imageBL);
        _bottomPart.spline.SetPosition(1, _imageBR);
        
        _leftPart.spline.SetPosition(0, _imageTL);
        _leftPart.spline.SetPosition(1, _imageBL);
        
        _topPart.RefreshSpriteShape(); 
        _rightPart.RefreshSpriteShape();
        _bottomPart.RefreshSpriteShape();
        _leftPart.RefreshSpriteShape();
    }

    void OnDrawGizmos()
    {
        _bgImage = transform.GetChild(0);
        _bgSprite = _bgImage.GetComponent<SpriteRenderer>();
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _gizmoSize);
        Gizmos.DrawWireCube(transform.position, arenaSize);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, arenaSize - new Vector2(_bgSprite.bounds.size.x,_bgSprite.bounds.size.y)/2);
    }
}
