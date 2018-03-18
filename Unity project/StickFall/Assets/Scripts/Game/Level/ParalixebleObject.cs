using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalixebleObject : BaseObject
{

    [SerializeField] float absCameraSpeed;

    private void OnEnable()
    {
        CameraManager.OnCameraPositionChanged += CameraManager_OnCameraPositionChanged;
        GameManager.OnGameOver += GameManager_OnGameOver;
    }

    private void GameManager_OnGameOver()
    {
        transform.position = Vector3.zero;
    }

    private void OnDisable()
    {
        CameraManager.OnCameraPositionChanged += CameraManager_OnCameraPositionChanged;
        GameManager.OnGameOver -= GameManager_OnGameOver;
    }


    private void CameraManager_OnCameraPositionChanged(Vector3 obj)
    {
        var a = CameraManager.Instance.CurrentCameraDeltaVector;
        transform.position += a * absCameraSpeed;
    }
    
}
