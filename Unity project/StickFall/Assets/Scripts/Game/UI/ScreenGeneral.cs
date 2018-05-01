using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenGeneral : MonoBehaviour {
    [System.Serializable]
    public enum ScreenType
    {
        None        =   0,
        MainMenu    = 1,
        GameScreen = 2,
        PauseScreen = 3,
    }

    [SerializeField] ScreenType _type;

    public ScreenType Type
    {
        get
        {
            return _type;
        }
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void SetCamera(Camera camera)
    {
        GetComponentInChildren<Canvas>().worldCamera = camera;
    }
}
