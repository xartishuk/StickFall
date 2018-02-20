using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ScreenState
{
    None  = 0,
    Menu  = 1,
    Pause = 2,
    Shop  = 3,
}
public enum GameState
{
    NotInGame   = 0,
    AwakeTap    = 1,
    ClickDown   = 2,
    ClickUp     = 3,
}

public class GameManager : SingletonMonoBehaviour<GameManager>
{

    #region Helpers

    [System.Serializable]
    struct ManagerInfo
    {
        public GameObject _prefab;
        public Transform _ancor;
    }

    #endregion


    #region Fields
    public static event System.Action OnStickStartGrow;
    public static event System.Action OnStickStopGrow;
    public static event System.Action OnStickStartFall;
    public static event System.Action OnStickStopFall;
    public static event System.Action OnPlayerStartMove;
    public static event System.Action OnPlayerStopMove;

    public static event System.Action OnGameStarted;
    public static event System.Action OnGameResumed;
    public static event System.Action OnGamePaused;

    [SerializeField]
    ManagerInfo _poolManager;

    [SerializeField]
    Camera _camera;

    [SerializeField]
    List<ManagerInfo> _coreManagers;

    [SerializeField]
    List<ManagerInfo> _managers;


    ScreenState _currentScreenState;
    GameState _currentGameState;
    ScreenState _previousScreenState;
    GameState _previousGameState;
    #endregion


    #region Properties

    public Camera MainCamera
    {
        get
        {
            return _camera;
        }
    }

    public ScreenState CurrentScreenState
    {
        get
        {
            return _currentScreenState;
        }
        set
        {
            _currentScreenState = value;
        }
    }

    public GameState CurrentGameState
    {
        get
        {
            return _currentGameState;
        }
        set
        {
            _currentGameState = value;
            Debug.Log(_currentGameState);
        }
    }

    #endregion


    #region Unity lifecycle

    protected override void Awake()
    {
        base.Awake();
        Instantiate(_poolManager._prefab, _poolManager._ancor);

        _managers.ForEach((manager) =>
        {
            ObjectCreator.CreateObject(manager._prefab, manager._ancor);
        });
    }

    private void Start()
    {
        StartGame();
    }


    private void Update()
    {
        List<Touch> touches = InputHelper.GetTouches();

        if (touches.Count > 0)
        {
            var touch = touches[0];
            CalculateTouches(touch.phase);
        }
    }

    #endregion


    #region Public methods

    public void StartGame()
    {
        PlayerManager.Instance.GeneratePlayer();
        LevelManager.Instance.GenerateStartLevel();
        CameraManager.Instance.Respawn();
        CurrentGameState = GameState.AwakeTap;
    }

    public void PlayerStarted()
    {
        if (OnPlayerStartMove != null)
        {
            OnPlayerStartMove();
        }
    }

    public void PlayerStoped()
    {
        if (OnPlayerStopMove != null)
        {
            OnPlayerStopMove();
        }

        CurrentGameState = GameState.AwakeTap;

        float distance = Random.Range(5f, 30f);
        float width = Random.Range(10f, 30f);

        LevelManager.Instance.GenerateNextPlatform(distance, width);
    }

    #endregion


    #region Private methods

    void CalculateTouches(TouchPhase phase)
    {
        switch (phase)
        {
            case TouchPhase.Began:
                if (CurrentGameState == GameState.AwakeTap)
                {
                    if (OnStickStartGrow != null)
                    {
                        OnStickStartGrow();
                    }
                    CurrentGameState = GameState.ClickDown;
                }
                break;
            case TouchPhase.Ended:
                if (CurrentGameState == GameState.ClickDown)
                {
                    if (OnStickStopGrow != null)
                    {
                        OnStickStopGrow();
                    }
                    CurrentGameState = GameState.ClickUp;

                    PlayerStarted();

                }
                break;
        }
    }

    #endregion
}
