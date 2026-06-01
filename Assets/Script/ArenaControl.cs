using UnityEngine;
using UnityEngine.InputSystem;

public class ArenaControl : MonoBehaviour
{
    [SerializeField] private InputActionAsset _actions;
    private InputAction _cheatAction;
    
    [SerializeField] private Transform _ennemiesParent;
    private Camera _mainCam;
    private CameraBigArea _mainCameraBigArea;
    private float _bigAreaCameraHeight;
    private float _staticCameraZoom;

    [SerializeField] private Vector2 _staticAreaCenter;
    [SerializeField] private float _staticAreaHeight;

    [SerializeField] private Transform[] _arenas;
    private Transform _leftBorder;
    private SpriteRenderer _leftBorderSpriteRenderer;
    private float _leftBorderStartPosition;
    private Transform _rightBorder;
    private SpriteRenderer _rightBorderSpriteRenderer;
    private float _rightBorderStartPosition;
    private Transform _topBorder;
    private SpriteRenderer _topBorderSpriteRenderer;
    private float _topBorderStartPosition;
    private Transform _bottomBorder;
    private SpriteRenderer _bottomBorderSpriteRenderer;
    private float _bottomBorderStartPosition;

    private bool _transitionningToStatic;
    [SerializeField] private AnimationCurve _animationCurve;
    [SerializeField] private float _transitionTime;
    private float _transitionMarker;

    private bool _arenaTransitionning;
    [SerializeField] private Animator _leftTheaterCurtain;
    [SerializeField] private Animator _rightTheaterCurtain;
    
    private CameraBigArea.CameraState _cameraState = CameraBigArea.CameraState.BigArea;
    
    void OnEnable()
    {
        if (GlobalSettings.gameRunning)
        {
            _actions.Enable();
            _cheatAction = _actions.FindAction("CheatCode");
            _cheatAction.started += CheatCheck;
        }
    }
    
    void Start()
    {
        _mainCam = Camera.main;
        _mainCameraBigArea = _mainCam.GetComponent<CameraBigArea>();
        _bigAreaCameraHeight = 2f * _mainCam.orthographicSize;
        _staticCameraZoom = _staticAreaHeight / _bigAreaCameraHeight;

        _leftBorder = _arenas[0].Find("Borders").Find("LeftBorder");
        _rightBorder = _arenas[0].Find("Borders").Find("RightBorder");
        _topBorder = _arenas[0].Find("Borders").Find("TopBorder");
        _bottomBorder = _arenas[0].Find("Borders").Find("BottomBorder");
        
        _leftBorderSpriteRenderer = _leftBorder.GetComponent<SpriteRenderer>();
        _rightBorderSpriteRenderer = _rightBorder.GetComponent<SpriteRenderer>();
        _topBorderSpriteRenderer = _topBorder.GetComponent<SpriteRenderer>();
        _bottomBorderSpriteRenderer = _bottomBorder.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (_ennemiesParent.childCount <= 0 && !_transitionningToStatic && _cameraState != CameraBigArea.CameraState.Static)
        {
            // _mainCameraBigArea.SwitchToStatic(_staticAreaCenter,_staticCameraZoom + 0.8f);
            // _cameraState = CameraBigArea.CameraState.Static;
            // _transitionningToStatic = true;
            // _transitionMarker = 0;

            // _leftBorderStartPosition = _leftBorder.position.x;
            // _rightBorderStartPosition = _rightBorder.position.x;
            // _topBorderStartPosition = _topBorder.position.y;
            // _bottomBorderStartPosition = _bottomBorder.position.y;
            
            // Pour le jouer qu'une fois
            if(!_arenaTransitionning) PlaySound.instance.PlayByType(PlaySound.SoundType.Curtain, transform.position, 1);
            _arenaTransitionning = true;
            _leftTheaterCurtain.SetTrigger("close");
            _rightTheaterCurtain.SetTrigger("close");
        }

        if (_transitionningToStatic)
        {
            _transitionMarker += Time.deltaTime / _transitionTime;
            float transitionProgression = _animationCurve.Evaluate(_transitionMarker);
            
            float width = _staticAreaHeight * _mainCam.aspect / 2;

            float left = -width + _staticAreaCenter.x;
            float right = width + _staticAreaCenter.x;
            float top = _staticAreaHeight/2 + _staticAreaCenter.y;
            float bottom = - (_staticAreaHeight/2) + _staticAreaCenter.y;

            if (transitionProgression >= 1)
            {
                Vector3 newPosition = _leftBorder.position;
                newPosition.x = left - _leftBorder.localScale.x/2;
                _leftBorder.position = newPosition;
                
                newPosition = _rightBorder.position;
                newPosition.x = right + _rightBorder.localScale.x/2;
                _rightBorder.position = newPosition;
                
                newPosition = _topBorder.position;
                newPosition.y = top - _topBorder.localScale.y/2;
                _topBorder.position = newPosition;
                
                newPosition = _bottomBorder.position;
                newPosition.y = bottom - _bottomBorder.localScale.y/2;
                _bottomBorder.position = newPosition;

                _transitionningToStatic = false;
            }
            else
            {
                Vector3 newPosition = _leftBorder.position;
                float nextPosition = (left - _leftBorder.localScale.x/2) - _leftBorderStartPosition;
                newPosition.x = (nextPosition * transitionProgression) + _leftBorderStartPosition;
                _leftBorder.position = newPosition;
                
                newPosition = _rightBorder.position;
                nextPosition = (right + _rightBorder.localScale.x/2) - _rightBorderStartPosition;
                newPosition.x = (nextPosition * transitionProgression) + _rightBorderStartPosition;
                _rightBorder.position = newPosition;
                
                newPosition = _topBorder.position;
                nextPosition = (top - _topBorder.localScale.y/2) - _topBorderStartPosition;
                newPosition.y = (nextPosition * transitionProgression) + _topBorderStartPosition;
                _topBorder.position = newPosition;
                
                newPosition = _bottomBorder.position;
                nextPosition = (bottom - _bottomBorder.localScale.y/2) - _bottomBorderStartPosition;
                newPosition.y = (nextPosition * transitionProgression) + _bottomBorderStartPosition;
                _bottomBorder.position = newPosition;
            }
        }

        if (_arenaTransitionning)
        {
            _leftTheaterCurtain.GetCurrentAnimatorStateInfo(0).IsName("close");
        }
    }

    private void CheatCheck(InputAction.CallbackContext phase)
    {
        if (_ennemiesParent.childCount >= 0)
        {
            for (int i = 0; i < _ennemiesParent.childCount; i++)
            {
                GameObject child = _ennemiesParent.GetChild(i).gameObject;
                Destroy(child);
            }
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(_staticAreaCenter, 1f);
        Gizmos.DrawWireCube(_staticAreaCenter, new Vector3(_staticAreaHeight * Camera.main.aspect, _staticAreaHeight,1f));
    }
}
