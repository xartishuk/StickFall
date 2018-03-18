using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraManager : SingletonMonoBehaviour<CameraManager>
{
    private Camera _camera;

    public static event System.Action<Vector3> OnCameraPositionChanged;

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
            return (float)_camera.pixelWidth / (float)1080;
        }
    }

    protected override void Awake()
    {
        base.Awake();
        _camera = Camera.main;
        _camera.orthographicSize = 960;//_camera.pixelHeight * 0.5f;
    }

    private void OnEnable()
    {
        prevCameraPosition = _camera.transform.position;
        currentCameraPosition = _camera.transform.position;
        GameManager.OnPlayerStopMove += GameManager_OnPlayerStopMove;
    }
    private void OnDisable()
    {
        GameManager.OnPlayerStopMove -= GameManager_OnPlayerStopMove;
    }

    public void Respawn()
    {
        _camera.transform.position = new Vector3((PlayerManager.Instance.PlayerInstance.transform.position).x + 5, _camera.transform.position.y, _camera.transform.position.z);

        prevCameraPosition = _camera.transform.position;
        currentCameraPosition = _camera.transform.position;
        OnChangedCameraPosition();
    }

    Vector3 prevCameraPosition;
    Vector3 currentCameraPosition;

    void OnChangedCameraPosition()
    {
        prevCameraPosition = currentCameraPosition;
        currentCameraPosition = _camera.transform.position;

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
        _camera.transform.DOMove(new Vector3((PlayerManager.Instance.PlayerInstance.transform.position).x + 5, _camera.transform.position.y, _camera.transform.position.z), 0.2f).OnUpdate(() =>
        {
            OnChangedCameraPosition();
        });
    }
}
