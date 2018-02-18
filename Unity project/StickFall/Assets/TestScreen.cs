using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestScreen : MonoBehaviour {

    [SerializeField] Button button;

    private void Start()
    {
        var a = button.GetComponent<Button>();
        a.onClick.AddListener(OnButtonClick);
    }


    Vector3 initial;
    void OnButtonClick()
    {
        Debug.Log("asdasd");
        var a = button.gameObject.GetComponent<RectTransform>();

        if (a.sizeDelta == new Vector2(100, 100))
        {

            a.sizeDelta = initial;
        }
        else
        {
            initial = a.sizeDelta;
            a.sizeDelta = new Vector2(100, 100);
        }

    }
    // Update is called once per frame
    void Update () {
		
	}
}
