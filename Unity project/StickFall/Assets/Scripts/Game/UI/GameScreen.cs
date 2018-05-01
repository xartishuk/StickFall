using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScreen : ScreenGeneral
{

    [SerializeField] Text _scoreText;
    [SerializeField] Text _moneyText;

    private void OnEnable()
    {
        CommonStats.OnMoneyValueChanged += CommonStats_OnMoneyValueChanged;
    }

    private void OnDisable()
    {
        CommonStats.OnMoneyValueChanged -= CommonStats_OnMoneyValueChanged;
    }

    private void CommonStats_OnMoneyValueChanged()
    {
        _moneyText.text = string.Format("{0}$", CommonStats.Money);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnPauseClick()
    {
        GUIManager.Instance.Show(ScreenType.PauseScreen);
    }
}
