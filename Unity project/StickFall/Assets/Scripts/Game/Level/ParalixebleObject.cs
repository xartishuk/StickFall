using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalixebleObject : BaseObject
{
    #region Fields

    [SerializeField] float absSpeed;
    [SerializeField] float offset;
    [SerializeField] GameObject prefab;
    [SerializeField] bool isCameraFollow;

    [SerializeField] List<GameObject> objectsOnScreen = new List<GameObject>();

    bool NeedInsert = false;
    bool NeedHide = false;

    #endregion


    #region Properties
    float RightBackgroundPosition
    {
        get
        {
            if (objectsOnScreen.Count <= 0)
            {
                return transform.position.x;
            }
            float position = objectsOnScreen[objectsOnScreen.Count - 1].transform.position.x + Width * 0.5f;
            return position;
        }
    }


    float LeftBackgroundPosition
    {
        get
        {
            if (objectsOnScreen.Count <= 0f)
            {
                return transform.position.x;
            }
            float position = objectsOnScreen[0].transform.position.x + Width * 0.5f;
            return position;
        }
    }

    public float Width
    {
        get;
        private set;
    }

    #endregion


    #region Unity lifecycle

    public void CustomUpdate(float deltaTime)
    {
        if (NeedInsert)
        {
            Add();
            NeedInsert = false;
        }
        if (NeedHide)
        {
            Hide();
            NeedHide = false;
        }
    }


    private void OnEnable()
    {
        if(isCameraFollow)
        {
            CameraManager.OnCameraPositionChanged += ChangePositionByOffsetEvent;
        }
        else
        {
            PlayerManager.OnPlayerChangedPosition += ChangePositionByOffsetEvent;
        }
        CameraManager.OnCameraPositionChanged += CameraManager_OnCameraPositionChanged;
        GameManager.OnGameOver += GameManager_OnGameOver;
    }


    private void OnDisable()
    {
        if (isCameraFollow)
        {
            CameraManager.OnCameraPositionChanged += ChangePositionByOffsetEvent;
        }
        else
        {
            PlayerManager.OnPlayerChangedPosition += ChangePositionByOffsetEvent;
        }
        CameraManager.OnCameraPositionChanged -= CameraManager_OnCameraPositionChanged;
        GameManager.OnGameOver -= GameManager_OnGameOver;
    }

    #endregion

    
    #region Public methods

    public void Init()
    {
        var obj = ObjectCreator.CreateObject(prefab, transform.transform).gameObject;
        obj.transform.localPosition = Vector3.zero;
        obj.SetActive(true);
        Width = obj.GetComponent<MeshRenderer>().bounds.size.x;
        obj.ReturnToPool();

        do
        {
            Add();
        } while (RightBackgroundPosition < CameraManager.Instance.CameraRightBottomPosition.x);

    }

    public void Add()
    {
        var obj = ObjectCreator.CreateObject(prefab, transform.transform).gameObject; obj.transform.localPosition = Vector3.zero;
        if (objectsOnScreen.Count <= 0)
        {
            obj.transform.localPosition = Vector3.zero;
        }
        else
        {
            obj.transform.localPosition = objectsOnScreen[objectsOnScreen.Count - 1].transform.localPosition + new Vector3(Width, 0f, 0f);
        }

        objectsOnScreen.Add(obj);
    }


    public void Hide()
    {
        objectsOnScreen[0].ReturnToPool();
        objectsOnScreen.RemoveAt(0);
    }

    #endregion


    #region Event handlers

    private void ChangePositionByOffsetEvent(Vector2 offset)
    {
        transform.position += (Vector3.right * offset.x) * absSpeed;
    }

    private void CameraManager_OnCameraPositionChanged(Vector2 offset)
    {
        if (RightBackgroundPosition + this.offset < CameraManager.Instance.CameraRightBottomPosition.x)
        {
            NeedInsert = true;
        }
        if (LeftBackgroundPosition < CameraManager.Instance.CameraLeftTopPosition.x)
        {
            NeedHide = true;
        }
    }

    private void GameManager_OnGameOver()
    {
        for (int i = 0; i < objectsOnScreen.Count; i++)
        {
            objectsOnScreen[i].ReturnToPool();
        }

        objectsOnScreen.Clear();

        transform.position = new Vector3(0f, transform.position.y, transform.position.z);

        do
        {
            Add();
        } while (RightBackgroundPosition < CameraManager.Instance.CameraRightBottomPosition.x);
    }

    #endregion

}
