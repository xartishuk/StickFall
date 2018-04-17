using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraManager : SingletonMonoBehaviour<CameraManager>
{
    [SerializeField] BoxCollider2D _cameraCollider;
    
    private Camera _camera = null;
    private Camera Camera
    {
        get
        {
            if(_camera == null)
            {
                _camera = Camera.main;
            }
            return _camera;
        }
    }


    public static event System.Action<Vector2> OnCameraPositionChanged;

    public Vector3 CurrentCameraDeltaVector
    {
        get
        {
            return currentCameraPosition - prevCameraPosition;
        }
    }

    public float GUIScaler
    {
        get
        {
            return (float)Camera.pixelWidth / (float)1080;
        }
    }

    public Vector3 CameraRightBottomPosition
    {
        get
        {
            return Camera.ScreenToWorldPoint(new Vector3(Camera.pixelWidth, 0));
        }
    }

    public Vector3 CameraLeftTopPosition
    {
        get
        {
            return Camera.ScreenToWorldPoint(new Vector3(0, Camera.pixelHeight));
        }
    }

    protected override void Awake()
    {
        base.Awake();
        Camera.orthographicSize = 960;//_camera.pixelHeight * 0.5f;
    }

    private void OnEnable()
    {
        prevCameraPosition = Camera.transform.position;
        currentCameraPosition = Camera.transform.position;
        GameManager.OnPlayerStopMove += GameManager_OnPlayerStopMove;
    }


    private void OnDisable()
    {
        GameManager.OnPlayerStopMove -= GameManager_OnPlayerStopMove;
    }

    private void Update()
    {
        //var bounds = _cameraCollider.bounds;
        //bounds.center = _camera.transform.position;
        //bounds.extents = _camera.pixelRect.size * 0.5f;
        _cameraCollider.size = new Vector2(Camera.orthographicSize, Camera.orthographicSize / Camera.aspect);
        _cameraCollider.transform.position = Camera.transform.position;
    }

    public void Respawn()
    {
        Camera.transform.position = new Vector3((PlayerManager.Instance.PlayerInstance.transform.position).x + 5, Camera.transform.position.y, Camera.transform.position.z);

        prevCameraPosition = Camera.transform.position;
        currentCameraPosition = Camera.transform.position;
        OnChangedCameraPosition();
    }

    Vector3 prevCameraPosition;
    Vector3 currentCameraPosition;

    void OnChangedCameraPosition()
    {
        prevCameraPosition = currentCameraPosition;
        currentCameraPosition = Camera.transform.position;

        if (prevCameraPosition != currentCameraPosition)
        {
            if (OnCameraPositionChanged != null)
            {
                OnCameraPositionChanged(currentCameraPosition);
            }
        }
    }

    private void GameManager_OnPlayerStopMove()
    {
        Camera.transform.DOMove(new Vector3((PlayerManager.Instance.PlayerInstance.transform.position).x + 5, Camera.transform.position.y, Camera.transform.position.z), 0.2f).OnUpdate(() =>
        {
            OnChangedCameraPosition();
        });
    }
}
