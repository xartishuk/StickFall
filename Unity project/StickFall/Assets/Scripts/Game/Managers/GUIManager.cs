using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIManager : SingletonMonoBehaviour<GUIManager>
{
    [SerializeField] Camera _GUICamera;

    [SerializeField] List<ScreenGeneral> _screensPrefabs;

    List<ScreenGeneral> screens;

    // Use this for initialization

    private void Awake()
    {
        screens = new List<ScreenGeneral>();
        _screensPrefabs.ForEach((obj) =>
        {
            ScreenGeneral screen = GameObject.Instantiate<ScreenGeneral>(obj, transform);
            screen.gameObject.SetActive(false);
            screen.SetCamera(_GUICamera);
            screens.Add(screen);
        });
    }

    void Start () {
		
	}

    public void Show(ScreenGeneral.ScreenType screentType)
    {
        screens.ForEach((obj)=>
        {
            if (obj.Type == screentType)
            {
                obj.Show();
            }
            else
            {
                obj.Hide();
            }
        });
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
