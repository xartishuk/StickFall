using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour 
{
	#region Public methods

	public void PlayPressed()
	{
		SceneManager.LoadScene("Main");
	}

	public void ExitPressed()
	{
		Application.Quit();
	}

	#endregion
}
