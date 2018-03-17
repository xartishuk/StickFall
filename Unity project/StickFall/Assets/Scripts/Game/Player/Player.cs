using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


[System.Serializable]
public enum PersonageType
{
    None = 0,
    Default = 1,
}

public class Player : BaseObject
{
    #region Fields
    
    [SerializeField] PersonageType _type;

    bool allowToMove = true;

    #endregion

    #region Properties

    public PersonageType Type
    {
        get
        {
            return _type;
        }
    }

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
        transform.localPosition = Vector3.zero;
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
        Vector2 position = new Vector3(nextPlatform.transform.position.x, transform.position.y, transform.position.z);
        transform.DOMove(position, time).OnComplete(() =>
        {
            allowToMove = true;

            GameManager.Instance.PlayerStoped();

        });
    }

    #endregion
}
