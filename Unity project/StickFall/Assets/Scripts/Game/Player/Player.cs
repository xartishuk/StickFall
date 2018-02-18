using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    AnimationCurve mooveCurve;

	void Start ()
    {
        //PoolManager.Instance.PoolForObject();
	}

    bool allowToMove = true;
	void Update ()
    {

        List<Touch> touches = InputHelper.GetTouches();
        if (touches.Count > 0 && allowToMove)
        {
            var a = touches[0];
            if (a.phase == TouchPhase.Began)
            {
                allowToMove = false;
                Vector2 position = GameManager.Instance.MainCamera.ScreenToWorldPoint(a.position);
                transform.DOMove(position, 0.3f).SetEase(mooveCurve).OnComplete(() =>
                {
                    allowToMove = true;
                });
            }
            //Debug.Log("1");
        }
        else
        {
            //Debug.Log("0");
        }
	}
}
