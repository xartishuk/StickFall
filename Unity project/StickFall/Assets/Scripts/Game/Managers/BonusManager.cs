using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusManager : SingletonMonoBehaviour<BonusManager>
{

    #region Fields

    [SerializeField] BonusItem _bonusItemPrefab;

    List<BonusItem> bonusesOnGame;

    #endregion


    #region Properties

    #endregion


    #region Unity lifecycle

    protected override void Awake()
    {
        base.Awake();
        bonusesOnGame = new List<BonusItem>();
    }

    private void OnEnable()
    {
        BonusItem.OnPickedBonus += BonusItem_OnPickedBonus;
    }

    private void OnDisable()
    {
        BonusItem.OnPickedBonus -= BonusItem_OnPickedBonus;
    }

    #endregion


    #region Pubic methods

    public void SpawnBonus(Vector3 spawnPosition)
    {
        BonusItem spawnedBonusItem = ObjectCreator.CreateObject(_bonusItemPrefab.gameObject, transform, 5).GetComponent<BonusItem>();
        bonusesOnGame.Add(spawnedBonusItem);
        spawnedBonusItem.transform.position = spawnPosition;
        spawnedBonusItem.Init(2);
    }

    #endregion

    #region Event handlers

    private void BonusItem_OnPickedBonus(BonusItem obj)
    {
        BonusItem findedBonus = bonusesOnGame.Find((obj1) => obj1 == obj);
        bonusesOnGame.Remove(findedBonus);
        findedBonus.gameObject.ReturnToPool();
    }

    #endregion

}
