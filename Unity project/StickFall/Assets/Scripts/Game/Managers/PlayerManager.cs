using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : SingletonMonoBehaviour<PlayerManager>
{

    [SerializeField] Player _player;

    public Player PlayerInstance
    {
        get;
        private set;
    }
    private void Awake()
    {
        PlayerInstance = ObjectCreator.CreateObject(_player.gameObject, transform).GetComponent<Player>();
        //PlayerInstance.Init();
    }

    // Update is called once per frame
    void Update () {
		
	}
}
