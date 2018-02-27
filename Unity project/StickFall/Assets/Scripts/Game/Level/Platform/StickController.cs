using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickController : BaseObject 
{

    [SerializeField] BoxCollider2D _mainCollider;
    [SerializeField] GameObject _stick;
    float _growSpeed = 45;
    float _fallSpeed = 0.7f;


    bool isAllowToGrow;

    void OnEnable()
    {
        _stick.transform.localScale = Vector3.one;
        _stick.transform.localRotation = new Quaternion(0f, 0f, 0f, 0f);
    }

	// Use this for initialization
	void Start () {
		
	}
	
    protected override void CustomUpdate(float deltaTime)
    {
        base.CustomUpdate(deltaTime);

        if (isAllowToGrow)
        {
            _stick.transform.localScale += new Vector3(0f, _growSpeed * deltaTime, 0f);
        }
    }


    public void StartGrow()
    {
        isAllowToGrow = true;
    }

    public void StopGrow()
    {
        isAllowToGrow = false;

        GameManager.Instance.StickStartFall();
    }

    public void FallStick()
    {
        _stick.transform.DORotate(new Vector3(0f, 0f, 270f), LevelManager.Instance.FallStickDuaration).SetEase(LevelManager.Instance.FallStickCurve).OnComplete(() =>
            {
                GameManager.Instance.StickStopFall();

            }
        );

    }
}
