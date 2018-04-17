using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalixebleObject : BaseObject
{

    [SerializeField] float absCameraSpeed;
    [SerializeField] GameObject prefab;

    [SerializeField] List<GameObject> objectsOnScreen = new List<GameObject>();

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
    bool NeedInsert = false;
    bool NeedHide = false;

    private void Update()
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
        PlayerManager.OnPlayerChangedPosition += PlayerManager_OnPlayerChangedPosition;
        CameraManager.OnCameraPositionChanged += CameraManager_OnCameraPositionChanged;
        GameManager.OnGameOver += GameManager_OnGameOver;
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

    private void OnDisable()
    {
        PlayerManager.OnPlayerChangedPosition -= PlayerManager_OnPlayerChangedPosition;
        CameraManager.OnCameraPositionChanged -= CameraManager_OnCameraPositionChanged;
        GameManager.OnGameOver -= GameManager_OnGameOver;
    }

    private void PlayerManager_OnPlayerChangedPosition(Vector2 offset)
    {
        transform.position += (Vector3.right * offset.x) * absCameraSpeed;
    }

    private void CameraManager_OnCameraPositionChanged(Vector2 offset)
    {
        if (RightBackgroundPosition < CameraManager.Instance.CameraRightBottomPosition.x)
        {
            NeedInsert = true;
        }
        if (LeftBackgroundPosition < CameraManager.Instance.CameraLeftTopPosition.x)
        {
            NeedHide = true;
        }
    }
    

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


}
