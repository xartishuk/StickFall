using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    #region Fields

    [SerializeField] AnimationCurve mooveCurve;

    bool allowToMove = true;

    #endregion


    #region Unity lifecycle

    private void OnEnable()
    {
        GameManager.OnPlayerStartMove += GameManager_OnPlayerStartMove;
    }

    private void OnDisable()
    {
        GameManager.OnPlayerStartMove -= GameManager_OnPlayerStartMove;
    }

    #endregion


    #region Public methods

    public void Respawn()
    {
        transform.position = Vector3.zero;
    }

    #endregion

    #region Private methods

	private void Update ()
    {
        List<Touch> touches = InputHelper.GetTouches();
        if (touches.Count > 0 && allowToMove)
        {
            var a = touches[0];
            
            if (a.phase == TouchPhase.Began )
            {
               

            }
        }
	}

    #endregion

    #region Event handlers

    private void GameManager_OnPlayerStartMove()
    {
        float speed = 100;
        LevelPlatform nextPlatform = LevelManager.Instance.LastBlockForUser;
        float distance = Mathf.Abs((transform.position - nextPlatform.transform.position).z);

        float time = distance / speed;

        allowToMove = false;

        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        Vector2 position = nextPlatform.transform.position;
        transform.DOMove(position, time).SetEase(mooveCurve).OnComplete(() =>
        {
            allowToMove = true;

            GameManager.Instance.PlayerStoped();

        });
    }

    #endregion
}
