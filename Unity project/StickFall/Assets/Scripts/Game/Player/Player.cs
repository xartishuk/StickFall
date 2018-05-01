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

public enum StopPlayerAt
{
    None = 0,
    Platform = 1,
    Stick = 2,
}

public class Player : BaseObject
{

    #region Events

    public static event System.Action<Vector2> OnPlayerChangedPosition;

    #endregion

    #region Fields

    [SerializeField] PersonageType _type;

    StopPlayerAt stopPlayerAt;

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
    Vector3 prevPosition;
    private void Update()
    {
        if(prevPosition != transform.position)
        {
            if (OnPlayerChangedPosition != null)
            {
                OnPlayerChangedPosition(transform.position - prevPosition);
            }
            prevPosition = transform.position;
        }
    }

    #endregion


    #region Public methods

    public void Respawn()
    {
        transform.localPosition = Vector3.zero;
        prevPosition = transform.position;
    }

    #endregion
    

    #region Event handlers

    private void GameManager_OnPlayerStartMove()
    {
        float speed = 900;

        LevelPlatform nextPlatform = LevelManager.Instance.LastBlockForUser;
        Vector3 endStickPosition = LevelManager.Instance.CurrentBlockForUser.StickController.EndStickPosition;
        Vector3 perfectPosition = LevelManager.Instance.LastBlockForUser.PerfectPosition;
        Vector2 perfectSize = LevelManager.Instance.LastBlockForUser.PerfectSize;

        if (nextPlatform.transform.position.x - nextPlatform.Width * 0.5f > endStickPosition.x)
        {
            Debug.Log("Nedohod");
            stopPlayerAt = StopPlayerAt.Stick;
        }
        else if (nextPlatform.transform.position.x + nextPlatform.Width * 0.5f < endStickPosition.x)
        {
            Debug.Log("Perehod");
            stopPlayerAt = StopPlayerAt.Stick;
        }
        else
        {
            Debug.Log("StopAtPlatform");
            stopPlayerAt = StopPlayerAt.Platform;

            if(endStickPosition.x < perfectPosition.x + perfectSize.x && endStickPosition.x > perfectPosition.x - perfectSize.x)
            {
                GameManager.Instance.PerfectStick();
                nextPlatform.Perfect();
            }

        }
            

        float distance = 0f;
        switch (stopPlayerAt)
        {
            case StopPlayerAt.Platform:
                distance = Mathf.Abs((transform.position - nextPlatform.transform.position).x);
                break;

            case StopPlayerAt.Stick:
                distance = Mathf.Abs((transform.position - endStickPosition).x);
                break;

        }
        

        float time = distance / speed;

        allowToMove = false;

        GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        Vector2 position = new Vector3(transform.position.x + distance, transform.position.y, transform.position.z);
        transform.DOMove(position, time).OnComplete(() =>
        {
            if(stopPlayerAt == StopPlayerAt.Stick)
            {
                GameManager.Instance.TryKillPlayer();
            }
            else
            {
                allowToMove = true;

                GameManager.Instance.PlayerStoped();
            }

        });
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        ColliderReporter colliderReporter = collision.GetComponent<ColliderReporter>();
        if(colliderReporter != null)
        {
            if(colliderReporter.ColliderType == ColliderType.BonusCollider)
            {
                BonusItem bonusItem = ((BonusItem)colliderReporter.ColliderHandler);

                int currentBonusValue = bonusItem.Value;

                bonusItem.PickBonus();
                BonusWherePicked(currentBonusValue);
            }
        }
    }

    #endregion

    void BonusWherePicked(int bonusValue)
    {
        CommonStats.Money += bonusValue;
    }
}
