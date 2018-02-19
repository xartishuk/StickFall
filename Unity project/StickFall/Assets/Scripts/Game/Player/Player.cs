using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    #region Fields

    [SerializeField] AnimationCurve mooveCurve;

    bool allowToMove = true;

    #endregion


    #region Public methods

    public void Respawn()
    {
        transform.position = Vector3.zero;
    }

    #endregion

    #region Private methods

	private void Update ()
    {
        List<Touch> touches = InputHelper.GetTouches();
        if (touches.Count > 0 && allowToMove)
        {
            var a = touches[0];
            
            if (a.phase == TouchPhase.Began )
            {
                allowToMove = false;

                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                Vector2 position = GameManager.Instance.MainCamera.ScreenToWorldPoint(a.position);
                transform.DOMove(position, 0.3f).SetEase(mooveCurve).OnComplete(() =>
                {
                    allowToMove = true;
                });

            }
        }
	}

    #endregion


}
