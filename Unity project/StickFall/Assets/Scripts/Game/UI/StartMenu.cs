using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : ScreenGeneral 
{
    #region Fields

    [SerializeField] Text _moneyText;

    string moneyString = "{0}$";
    #endregion



    #region Public methods

    private void OnEnable()
    {
        _moneyText.text = string.Format(moneyString, CommonStats.Money);
    }

    public void PlayPressed()
	{
        GameManager.Instance.StartGame();

	}

	public void ExitPressed()
	{
		Application.Quit();
	}

	#endregion
}
