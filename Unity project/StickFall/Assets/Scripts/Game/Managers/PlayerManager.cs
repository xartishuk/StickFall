using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : SingletonMonoBehaviour<PlayerManager>
{
    public static event System.Action<Vector2> OnPlayerChangedPosition;

    #region Fields

    [SerializeField] List<Player> _personages;
    Player playerInstance;

    PersonageType currentPersonage = PersonageType.Default;

    #endregion


    #region Properties

    public Player PlayerInstance
    {
        get
        {
            if (playerInstance == null)
            {
                GameObject currentPerson = _personages.Find(obj => obj.Type == currentPersonage).gameObject;
                playerInstance = ObjectCreator.CreateObject(currentPerson, transform).GetComponent<Player>();
            }
            return playerInstance;
        }
    }

    #endregion


    #region Unity lifecycle

    private void OnEnable()
    {
        Player.OnPlayerChangedPosition += PlayerInstance_OnPlayerChangedPosition;
    }


    private void OnDisable()
    {
        Player.OnPlayerChangedPosition -= PlayerInstance_OnPlayerChangedPosition;
    }


    private void Awake()
    {
    }

    private void Update()
    {
    }


    #endregion


    #region Public methods

    public void GeneratePlayer()
    {
        PlayerInstance.gameObject.SetActive(true);
        PlayerInstance.Respawn();
    }

    public void HidePlayer()
    {
        PlayerInstance.gameObject.SetActive(false);
    }

    #endregion

    #region Private methods



    #endregion


    private void PlayerInstance_OnPlayerChangedPosition(Vector2 offset)
    {
        if(OnPlayerChangedPosition != null)
        {
            OnPlayerChangedPosition(offset);
        }
    }
}
