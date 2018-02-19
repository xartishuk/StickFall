using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : SingletonMonoBehaviour<PlayerManager>
{

    #region Fields

    [SerializeField] Player _player;
    Player playerInstance;

    #endregion


    #region Properties

    public Player PlayerInstance
    {
        get
        {
            if (playerInstance == null)
                playerInstance = ObjectCreator.CreateObject(_player.gameObject, transform).GetComponent<Player>();
            return playerInstance;
        }
    }

    #endregion


    #region Unity lifecycle

    private void Awake()
    {
    }

    void Update()
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
    
}
