using System.Collections;
using System.Collections.Generic;
using UnityEngine;



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
    [SerializeField]
    ManagerInfo _poolManager;

    [SerializeField]
    Camera _camera;

    [SerializeField]
    List<ManagerInfo> _coreManagers;

    [SerializeField]
    List<ManagerInfo> _managers;

    #endregion


    #region Properties

    public Camera MainCamera
    {
        get
        {
            return _camera;
        }
    }

    #endregion


    #region Unity lifecycle

    protected override void Awake()
    {
        base.Awake();

        Debug.Log("GM AWAKE");
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

    #endregion


    #region Public methods

    public void StartGame()
    {
        PlayerManager.Instance.GeneratePlayer();
        LevelManager.Instance.GenerateStartLevel();

    }

    #endregion
}
