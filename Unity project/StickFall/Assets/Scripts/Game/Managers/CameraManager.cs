﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraManager : SingletonMonoBehaviour<CameraManager>
{
    private Camera _camera;

    protected override void Awake()
    {
        base.Awake();
        _camera = Camera.main;
    }

    private void OnEnable()
    {
        GameManager.OnPlayerStopMove += GameManager_OnPlayerStopMove;
    }
    private void OnDisable()
    {
        GameManager.OnPlayerStopMove -= GameManager_OnPlayerStopMove;
    }

    public void Respawn()
    {
        _camera.transform.position = new Vector3((PlayerManager.Instance.PlayerInstance.transform.position).x + 5, _camera.transform.position.y, _camera.transform.position.z);

    }

    private void GameManager_OnPlayerStopMove()
    {
        _camera.transform.DOMove(new Vector3((PlayerManager.Instance.PlayerInstance.transform.position).x + 5, _camera.transform.position.y, _camera.transform.position.z), 0.2f);
    }
}